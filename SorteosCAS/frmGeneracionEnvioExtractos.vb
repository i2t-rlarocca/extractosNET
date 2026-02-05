Imports Sorteos.Helpers
Imports Sorteos.Bussiness
Imports Sorteos.Entities

Public Class frmGeneracionEnvioExtractos
    Public voSorteo As PgmSorteo
    Public vidPgmConcurso As Long
    Public vNroSorteo As Long
    Public vNombreSorteo As String
    Private Sub frmGeneracionEnvioExtractos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim destinatarios As String = General.ListaEnvioExtracto
        Dim carpeta As String = General.CarpetaArchivosBoldt
        txtDestinatarios.Text = destinatarios
        txtCarpeta.Text = carpeta
        lblTituloConfirmado.Text = "EL SORTEO NUMERO - " & vNroSorteo & " -  CORRESPONDIENTE A - " & vNombreSorteo.ToUpper & " -  HA SIDO CONFIRMADO CON ÉXITO."
    End Sub



    Private Sub btnbuscarCarpeta_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            CDCarpetaDestino.Description = "Seleccione carpeta destino"

            Dim ret As DialogResult = CDCarpetaDestino.ShowDialog() ' abre el diálogo  
            ' si se presionó el botón aceptar ...  
            If ret = Windows.Forms.DialogResult.OK Then
                Me.txtCarpeta.Text = CDCarpetaDestino.SelectedPath
            End If
            CDCarpetaDestino.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub





    

    Private Sub btnEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Dim _destinatarios As String = ""
        Dim _path As String = ""
        PtbEnvio.Visible = False
        lblenviomail.Visible = False
        If Me.txtDestinatarios.Text.Trim = "" Then
            MsgBox("La lista de destinatarios debe contener al menos una dirección de mail.")
            txtDestinatarios.Focus()
            Exit Sub
        End If
        _destinatarios = txtDestinatarios.Text
        PtbEsperando.Visible = True
        lblenviomail.Visible = True
        Me.Refresh()
        Me.Cursor = Cursors.WaitCursor
        btnEnviar.Enabled = False

        Try

            Dim _pdf As String = ""
            _path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14)
            If Not GeneralBO.GenerarPdfExtractoSF(vidPgmConcurso, _pdf, _path) Then
                PtbEnvio.Visible = True
                lblenviomail.Visible = True
                PtbEnvio.Image = My.Resources.Imagenes.DELETE
                PtbEsperando.Visible = False
                lblenviomail.Text = "Envío de Mail Falló"
                Exit Sub
            End If
            'envio por mail el pdf con el reporte
            GeneralBO.EnviarMail(_pdf, _destinatarios)
            'System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15))
            PtbEsperando.Visible = False
            PtbEnvio.Visible = True
            lblenviomail.Visible = True
            PtbEnvio.Image = My.Resources.Imagenes.ImagenAceptar
            lblenviomail.Text = "Envío de Mail Ok"
            btnEnviar.Enabled = False
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & ex.Message)
            'MsgBox("Problema al enviar extracto: " & ex.Message, MsgBoxStyle.Critical)
            PtbEsperando.Visible = False
            PtbEnvio.Visible = True
            lblenviomail.Visible = True
            PtbEnvio.Image = My.Resources.Imagenes.DELETE
            lblenviomail.Text = "Envío de Mail Falló"
        Finally
            PtbEsperando.Visible = False
            Me.Cursor = Cursors.Default
            btnEnviar.Enabled = True


        End Try
    End Sub

    Private Sub btnGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGenerar.Click
        Dim path As String = ""
        Dim oArchivoBold As New ArchivoBoldtBO
        path = txtCarpeta.Text
        PtbGenerar.Visible = False
        LblGenerarArchivo.Visible = False
        PtbEsperando1.Visible = True
        Me.Refresh()
        Me.Cursor = Cursors.WaitCursor
        btnGenerar.Enabled = False

        Try
            oArchivoBold.GenerarArchivoExtracto(voSorteo, path)
            'MsgBox("Se ha generado el archivo de extracto con éxito")
            PtbGenerar.Visible = True
            LblGenerarArchivo.Visible = True
            PtbGenerar.Image = My.Resources.Imagenes.ImagenAceptar
            PtbEsperando1.Visible = False
            LblGenerarArchivo.Text = "Generación de Archivo OK"
            btnGenerar.Enabled = False
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & ex.Message)
            PtbGenerar.Visible = True
            LblGenerarArchivo.Visible = True
            PtbGenerar.Image = My.Resources.Imagenes.DELETE
            LblGenerarArchivo.Text = "Generación de Archivo Falló"
        Finally
            PtbEsperando1.Visible = False
            Me.Cursor = Cursors.Default
            btnGenerar.Enabled = True
        End Try
    End Sub

    Private Sub btnEnviaryGenerar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviaryGenerar.Click
        Dim _path As String = ""
        Dim oArchivoBold As New ArchivoBoldtBO
        Dim _destinatarios As String = ""
        Dim _carpeta As String = ""
        Dim _pdf As String = ""

        'realiza controles
        If Me.txtDestinatarios.Text.Trim = "" Then
            MsgBox("La lista de destinatarios debe contener al menos una dirección de mail.")
            txtDestinatarios.Focus()
            Exit Sub
        End If
        _destinatarios = txtDestinatarios.Text

        If Me.txtCarpeta.Text.Trim = "" Then
            MsgBox("Elija una carpeta para la generación del archivo")
            txtCarpeta.Focus()
            Exit Sub
        End If
        _carpeta = txtCarpeta.Text

        btnEnviaryGenerar.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        '*** Envio de extracto por mail******
        Try


            Try
                _path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14)
                If Not GeneralBO.GenerarPdfExtractoSF(vidPgmConcurso, _pdf, _path) Then
                    PtbEnvio.Visible = True
                    lblenviomail.Visible = True
                    PtbEnvio.Image = My.Resources.Imagenes.DELETE
                    PtbEsperando.Visible = False
                    lblenviomail.Text = "Envío de Mail Falló"
                    Exit Sub
                End If
                'envio por mail el pdf con el reporte
                GeneralBO.EnviarMail(_pdf, _destinatarios)
                PtbEsperando.Visible = False
                PtbEnvio.Visible = True
                lblenviomail.Visible = True
                PtbEnvio.Image = My.Resources.Imagenes.ImagenAceptar
                lblenviomail.Text = "Envío de Mail Ok"
                btnEnviar.Enabled = False
            Catch ex As Exception
                FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " Envio de extracto por mail -> " & ex.Message)
                PtbEsperando.Visible = False
                PtbEnvio.Visible = True
                lblenviomail.Visible = True
                PtbEnvio.Image = My.Resources.Imagenes.DELETE
                lblenviomail.Text = "Envío de Mail Falló"
            Finally
                PtbEsperando.Visible = False
                Me.Cursor = Cursors.Default
                btnEnviar.Enabled = True

            End Try
            '**** fin Envio extracto por mail****
            '*** Generacion de archivo para boldt*********
            Try
                PtbGenerar.Visible = False
                LblGenerarArchivo.Visible = False
                PtbEsperando1.Visible = True
                Me.Refresh()
                btnGenerar.Enabled = False
                oArchivoBold.GenerarArchivoExtracto(voSorteo, _carpeta)
                PtbGenerar.Visible = True
                LblGenerarArchivo.Visible = True
                PtbGenerar.Image = My.Resources.Imagenes.ImagenAceptar
                PtbEsperando1.Visible = False
                LblGenerarArchivo.Text = "Generación de Archivo OK"
                btnGenerar.Enabled = False
            Catch ex As Exception
                FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " Generacion de archivos Boldt -> " & ex.Message)
                PtbGenerar.Visible = True
                LblGenerarArchivo.Visible = True
                PtbGenerar.Image = My.Resources.Imagenes.DELETE
                LblGenerarArchivo.Text = "Generación de Archivo Falló"
            Finally
                PtbEsperando1.Visible = False
            End Try
            '*** fin de generacion de arhivos para boldt************
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & ex.Message)
            Me.Cursor = Cursors.Default
        Finally
            btnEnviaryGenerar.Enabled = True
        End Try

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Dispose()
        Me.Close()
    End Sub
End Class