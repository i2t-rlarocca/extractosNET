Imports sorteos.bussiness
Imports libEntities.Entities
Imports sorteos.helpers
Public Class frmGeneracionExtractoBoldt
    Public voSorteo As PgmSorteo
    Public vNroSorteo As Long
    Public vNombreSorteo As String = ""
    Public vPath As String = ""
    Public vSoloJurLocal As Boolean

    Private Sub frmGeneracionExtractoBoldt_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        lbltitulo.Text = "EL SORTEO NÚMERO - " & vNroSorteo & " -  CORRESPONDIENTE A  - " & vNombreSorteo.ToUpper & vbCrLf & " -  HA SIDO CONFIRMADO CON ÉXITO."
        txtCarpeta.Text = IIf(vPath.Trim <> "", vPath, General.CarpetaArchivosBoldt)
        'txtCarpetaInterJ.Text = General.CarpetaExtractoBoltConfirmado

        LblGenerarArchivo.Text = ""
        LblGenerarArchivo.Visible = False
        LblGenerarArchivoInterJ.Text = ""
        LblGenerarArchivoInterJ.Visible = False

        PtbGenerar.Visible = False
        PtbGenerarInterJ.Visible = False
        '
        grbInterJ.Enabled = False
        If General.Extr_Interjur <> "S" Or Not voSorteo.ConfirmadoParcial Then
            grbInterJ.Enabled = False
        End If
    End Sub

    Private Sub btnGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim path As String = ""
        Dim path2 As String = ""
        Dim nombrePaq As String = ""

        Dim oArchivoBold As New ArchivoBoldtBO
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
        '       If chkInterJ.Checked And voSorteo.ConfirmadoParcial Then
        'Try
        'PtbEsperando2.Visible = True

        'oArchivoBold.GenerarArchivoExtractoInterJ(voSorteo, path2, nombrePaq)
        'PtbEsperando2.Visible = False
        'LblGenerarArchivoInterJ.Text = "Archivo en formato Interjurisdiccional generado satisfactoriamente."
        'LblGenerarArchivoInterJ.Visible = True
        'PtbGenerarInterJ.Image = My.Resources.Imagenes.ImagenAceptar
        'PtbGenerarInterJ.Visible = True
        'Catch exJ As Exception
        'FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Me.Text & " fmt InterJurisdiccional -> " & exJ.Message)
        'PtbGenerarInterJ.Visible = True
        'LblGenerarArchivoInterJ.Visible = True
        'PtbGenerarInterJ.Image = My.Resources.Imagenes.DELETE
        'LblGenerarArchivoInterJ.Text = "Generación de Archivo en formato Interjurisdiccional Falló"
        'End Try
        'End If
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

    Private Sub btnbuscarCarpetaInterJ_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscarCarpeta.Click
        Try
            CDCarpetaDestino.Description = "Seleccione carpeta destino"

            Dim ret As DialogResult = CDCarpetaDestino.ShowDialog() ' abre el diálogo  
            ' si se presionó el botón aceptar ...  
            If ret = Windows.Forms.DialogResult.OK Then
                Me.txtCarpetaInterJ.Text = CDCarpetaDestino.SelectedPath
            End If
            CDCarpetaDestino.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, MDIContenedor.Text)
        End Try
    End Sub

End Class