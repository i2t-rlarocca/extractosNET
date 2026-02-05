Imports sorteos.helpers
Imports libEntities.Entities
Imports sorteos.bussiness
Imports System.IO
Imports System.Security.Cryptography
Imports System.Xml
Imports System.Text

Public Class FrmGenerarArchivoExtracto
    Public voSorteo As New PgmSorteo
    Public vNroSorteo As Long
    Public vPath As String = ""
    Public vSoloJurLocal As Boolean = False

    Private Sub FrmGenerarArchivoExtracto_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub

    Private Sub FrmGenerarArchivoExtracto_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim boJuego As New JuegoBO
        Dim lstJuegos As ListaOrdenada(Of Juego)
        Dim idjuegos As String = "2,3,8,49,26,50"
        Dim carpeta As String = General.CarpetaArchivosBoldt

        Me.Location = New System.Drawing.Point(0, 0)
        
        lstJuegos = boJuego.getJuego
        cmbJuego.DataSource = lstJuegos
        cmbJuego.ValueMember = "IdJuego"
        cmbJuego.DisplayMember = "Jue_Desc"
        cmbJuego.Refresh()

        Me.txtCarpeta.Text = carpeta
        Me.txtCarpetaInterJ.Text = General.CarpetaExtractoBoltConfirmado

        LblGenerarArchivo.Text = ""
        LblGenerarArchivo.Visible = False
        LblGenerarArchivoInterJ.Text = ""
        LblGenerarArchivoInterJ.Visible = False

        PtbGenerar.Visible = False
        PtbGenerarInterJ.Visible = False

        '        If General.Extr_Interjur <> "S" Or Not voSorteo.ConfirmadoParcial Then
        If (General.Extr_Interjur <> "S" Or (Not boJuego.getJuego(cmbJuego.SelectedValue).GeneraExtractoUnif)) Then
            chkInterJ.Checked = False
            grbInterJ.Enabled = False
        End If

    End Sub

    Private Sub btnGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim path As String = ""
        Dim path2 As String = ""
        Dim nombrePaq As String = ""

        Dim boSorteo As New PgmSorteoBO
        Dim oArchivoBold As New ArchivoBoldtBO

        ' ***** valida las entradas **************
        If cmbJuego.SelectedValue = 0 Or txtNroSorteo.Text = "" Then
            txtNroSorteo.Focus()
            MsgBox("Indique número de sorteo y juego a generar.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End If
        If Not IsNumeric(Me.txtNroSorteo.Text) Then
            MsgBox("El número de sorteo debe ser un número.", MsgBoxStyle.Information, MDIContenedor.Text)
            txtNroSorteo.Focus()
            Exit Sub
        End If

        If Me.txtCarpeta.Text.Trim = "" Then
            MsgBox("Elija una carpeta para la generación del archivo", MsgBoxStyle.Information, MDIContenedor.Text)
            txtNroSorteo.Focus()
            Exit Sub
        End If
        vNroSorteo = txtNroSorteo.Text
        voSorteo.idPgmSorteo = boSorteo.getPgmSorteoId(cmbJuego.SelectedValue, txtNroSorteo.Text)
        voSorteo = boSorteo.getPgmSorteo(voSorteo.idPgmSorteo)
        If voSorteo.idPgmSorteo = 0 Then
            MsgBox("No existe el número de sorteo para el juego indicado.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End If
        ' ***************************************

        path = txtCarpeta.Text
        path2 = txtCarpetaInterJ.Text

        PtbEsperando1.Visible = False
        PtbEsperando2.Visible = False
        PtbGenerar.Visible = False
        PtbGenerarInterJ.Visible = False
        LblGenerarArchivo.Visible = False
        LblGenerarArchivoInterJ.Visible = False
        btnGenerar.Enabled = False

        'Me.Refresh()
        Me.Cursor = Cursors.WaitCursor

        If chkPropio.Checked Then
            Try
                PtbEsperando1.Visible = True
                oArchivoBold.GenerarArchivoExtracto(voSorteo, path, vSoloJurLocal)
                PtbEsperando1.Visible = False
                LblGenerarArchivo.Text = "Archivo en formato Propio generado satisfactoriamente."
                LblGenerarArchivo.Visible = True
                PtbGenerar.Image = My.Resources.Imagenes.ImagenAceptar
                PtbGenerar.Visible = True
            Catch exP As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Me.Text & " fmt Propio -> " & exP.Message)
                PtbGenerar.Visible = True
                LblGenerarArchivo.Visible = True
                PtbGenerar.Image = My.Resources.Imagenes.DELETE
                LblGenerarArchivo.Text = "Generación de Archivo en formato Propio Falló"
            End Try
        End If
        If chkInterJ.Checked And (voSorteo.ConfirmadoParcial Or voSorteo.idEstadoPgmConcurso = 50) Then
            Try
                PtbEsperando2.Visible = True

                oArchivoBold.GenerarArchivoExtractoInterJ(voSorteo, path2, nombrePaq)
                PtbEsperando2.Visible = False
                LblGenerarArchivoInterJ.Text = "Archivo en formato Interjurisdiccional generado satisfactoriamente."
                LblGenerarArchivoInterJ.Visible = True
                PtbGenerarInterJ.Image = My.Resources.Imagenes.ImagenAceptar
                PtbGenerarInterJ.Visible = True
            Catch exJ As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " - " & Me.Text & " fmt InterJurisdiccional -> " & exJ.Message)
                PtbGenerarInterJ.Visible = True
                LblGenerarArchivoInterJ.Visible = True
                PtbGenerarInterJ.Image = My.Resources.Imagenes.DELETE
                LblGenerarArchivoInterJ.Text = "Generación de Archivo en formato Interjurisdiccional Falló: " & exJ.Message
            End Try
        End If
        PtbEsperando1.Visible = False
        PtbEsperando2.Visible = False
        btnGenerar.Enabled = True

        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            Me.Close()
            Me.Dispose()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnbuscarCarpeta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCarpeta.Click
        Try
            CDCarpetaDestino.Description = "Seleccione carpeta destino"

            Dim ret As DialogResult = CDCarpetaDestino.ShowDialog() ' abre el diálogo  
            ' si se presionó el botón aceptar ...  
            If ret = Windows.Forms.DialogResult.OK Then
                Me.txtCarpeta.Text = CDCarpetaDestino.SelectedPath
            End If
            CDCarpetaDestino.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub btnbuscarCarpetaInterJ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCarpetaInterJ.Click
        Try
            CDCarpetaDestino.Description = "Seleccione carpeta destino"

            Dim ret2 As DialogResult = CDCarpetaDestino.ShowDialog() ' abre el diálogo  
            ' si se presionó el botón aceptar ...  
            If ret2 = Windows.Forms.DialogResult.OK Then
                Me.txtCarpetaInterJ.Text = CDCarpetaDestino.SelectedPath
            End If
            CDCarpetaDestino.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, MDIContenedor.Text)
        End Try
    End Sub
End Class