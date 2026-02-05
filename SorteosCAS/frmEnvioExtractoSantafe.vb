Imports sorteos.bussiness
Imports Sorteos.Helpers
Imports Sorteos.Extractos
Imports Sorteos.Entities

Public Class frmEnvioExtractoSantafe
    Public vidPgmConcurso As Long
    Public vidPgmSorteo As Long
    Public vEnviarExtractoOficial As Boolean = False
    Public vEnviarEncriptado As Boolean = False


    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Dispose()
        Me.Close()
    End Sub

    Private Sub btnEnviar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Dim _destinatarios As String = ""
        Dim _path As String = ""
        PtbEnvio.Visible = False
        lblenviomail.Visible = False
        If Me.txtDestinatarios.Text.Trim = "" Then
            MsgBox("La lista de destinatarios debe contener al menos una dirección de mail.", MsgBoxStyle.Information, MDIContenedor.Text)
            txtDestinatarios.Focus()
            Exit Sub
        End If
        _destinatarios = txtDestinatarios.Text
        PtbEsperando.Visible = True
        Me.Refresh()
        Me.Cursor = Cursors.WaitCursor
        btnEnviar.Enabled = False

        Try
            Dim _pdf As String = ""
            Dim falloExtractoLocal As Boolean = False
            Dim sBO As New PgmSorteoBO
            Dim oSorteo As PgmSorteo

            oSorteo = sBO.getPgmSorteo(vidPgmSorteo)

            If oSorteo.idEstadoPgmConcurso = 50 And Me.vEnviarExtractoOficial Then
                '** extracto local
                Try
                    Dim ds As New ExtractoReporte
                    ds.GenerarExtractoLocal(vidPgmSorteo, _pdf, General.PathInformes)
                Catch ex As Exception
                    falloExtractoLocal = True
                End Try
            End If

            If oSorteo.idEstadoPgmConcurso < 50 Or falloExtractoLocal Or (Not Me.vEnviarExtractoOficial) Then
                _path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14)
                If Not GeneralBO.GenerarPdfExtractoSF(vidPgmConcurso, _pdf, _path, IIf(General.Jurisdiccion = "S", False, True)) Then
                    PtbEnvio.Visible = True
                    lblenviomail.Visible = True
                    PtbEnvio.Image = My.Resources.Imagenes.DELETE
                    lblenviomail.Text = "Envío de Mail Falló"
                    PtbEsperando.Visible = False
                    Exit Sub
                End If
            End If
            'envio por mail el pdf con el reporte

            GeneralBO.EnviarMail(_pdf, _destinatarios)
            PtbEnvio.Visible = True
            lblenviomail.Visible = True
            PtbEnvio.Image = My.Resources.Imagenes.ImagenAceptar
            lblenviomail.Text = "Envío de Mail Ok"
            btnEnviar.Enabled = False
            PtbEsperando.Visible = False

        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & ex.Message)
            PtbEsperando.Visible = False
            PtbEnvio.Visible = True
            lblenviomail.Visible = True
            PtbEnvio.Image = My.Resources.Imagenes.DELETE
            lblenviomail.Text = "Envío de Mail Falló"
        Finally
            Me.Cursor = Cursors.Default
            btnEnviar.Enabled = True
        End Try
    End Sub

    Private Sub frmEnvioExtractoSantafe_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim destinatarios As String = General.ListaEnvioExtracto
        txtDestinatarios.Text = destinatarios
        PtbEnvio.Visible = False
        lblenviomail.Visible = False
    End Sub
End Class