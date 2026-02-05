Imports Sorteos.Bussiness
Imports Sorteos.Helpers
Imports libEntities.Entities
Imports System.IO
Imports System.Security.Cryptography

Public Class frmPozoSugerido
    Dim usuarioBO As New UsuarioBO
    Dim ousuario As New cUsuario
    Dim bopozo As New PozoBO
    Private height_original As Integer = Me.Height
    Private height_login As Integer = 175

    Private Sub btnIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIngresar.Click
        Dim pwd As String
        Dim fecha As DateTime
        Dim pozo As Double
        Try
            pwd = ObtenerMD5(Me.txtpwd.Text.ToLower)
            ousuario = usuarioBO.getUsuario(Me.txtUsuario.Text, pwd)
            If Not ousuario Is Nothing Then
                If ousuario.PozoEstimadoPFHabilitado Then
                    pozo = bopozo.getPozoSugerido(30, fecha)
                    TxtAnterior.Text = IIf((pozo = 0), "", pozo)
                    txtfecha.Text = IIf((fecha.ToString("dd/MM/yyyy")) = "01/01/1999", "", fecha.ToString("dd/MM/yyyy"))
                    Me.GrbLogin.Enabled = False
                    Me.Height = height_original '312
                    Btncancelarpozo.Enabled = True
                    btnIngresar.Enabled = False
                    txtPozosugerido.Enabled = True
                    txtPozosugerido.Focus()
                Else
                    lblerror.Text = "Usuario no habilitado."
                    lblerror.Visible = True
                    If txtUsuario.Enabled Then txtUsuario.Focus()
                End If
            Else
                lblerror.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try

    End Sub

    Private Sub Btncancelarlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btncancelarlogin.Click
        Me.Close()
    End Sub


    Private Sub btnGuardarPozo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnGuardarPozo.Click
        Try
            Dim oPgmSorteoBO As New PgmSorteoBO
            Dim pozoBO As New PozoBO
            Dim separadordec As String = ""
            Dim pozo As Double = 0

            'se  obtiene el separaddor decimal d ela configuracion regional para poder formatear correctamente el pozo
            separadordec = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
            If separadordec = "." Then
                pozo = txtPozosugerido.Text.Replace(",", ".")
            Else
                pozo = txtPozosugerido.Text.Replace(".", ",")
            End If

            If Not IsNumeric(pozo) Then
                MsgBox("El pozo debe ser un valor numérico mayor que cero.", MsgBoxStyle.Information, MDIContenedor.Text)
                txtPozosugerido.Focus()
                Exit Sub
            End If
            If pozo <= 0 Then
                MsgBox("El pozo debe ser un valor mayor que cero.", MsgBoxStyle.Information, MDIContenedor.Text)
                txtPozosugerido.Focus()
                Exit Sub
            End If
            ' primero actualizo en la BD del sorteador
            Dim oJuegoSorteosActualizarEnWeb As New ListaOrdenada(Of cJuegoSorteo)
            oJuegoSorteosActualizarEnWeb = pozoBO.setPozoSugerido(pozo, ousuario.IdUsuario, 30, Now)
            ' despues veo si tengo que actualizar la web
            If oJuegoSorteosActualizarEnWeb IsNot Nothing Then
                ' Debo actualizar los pozos en web, de los sorteos devueltos en la lista
                Dim oPgmSorteo As New PgmSorteo
                Try
                    Me.Cursor = Cursors.WaitCursor
                    Me.Refresh()
                    For Each o As cJuegoSorteo In oJuegoSorteosActualizarEnWeb
                        oPgmSorteo = oPgmSorteoBO.getPgmSorteo(o.idPgmSorteo)
                        oPgmSorteoBO.ActualizarSorteoWeb(oPgmSorteo, oPgmSorteo.fechaHoraPrescripcion, oPgmSorteo.fechaHoraProximo, oPgmSorteo.fechaHoraIniReal, oPgmSorteo.fechaHora, oPgmSorteo.fechaHoraFinReal, oPgmSorteo.PozoEstimado)
                    Next
                    Me.Cursor = Cursors.Default
                    Me.Refresh()
                Catch ex As Exception
                End Try
            End If
            MsgBox("El pozo sugerido se ha guardado con éxito.", MsgBoxStyle.Information, MDIContenedor.Text)
            If MDIContenedor.formPremios IsNot Nothing Then
                Try
                    MDIContenedor.formPremios.Close()
                    MDIContenedor.formPremios.Dispose()
                Catch ex As Exception

                End Try
            End If
            If MDIContenedor.formFinalizar IsNot Nothing Then
                Try
                    MDIContenedor.formFinalizar.Close()
                    MDIContenedor.formFinalizar.Dispose()
                Catch ex As Exception

                End Try
            End If
            Me.Close()
            Me.Dispose()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub frmPozoSugerido_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub

    Private Sub frmPozoSugerido_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Location = New System.Drawing.Point(0, 0)
        Me.height_original = Me.Height
        Me.Height = height_login
    End Sub
    Private Function ObtenerMD5(ByVal fichero As String) As String
        Dim md5 As New MD5CryptoServiceProvider
        Dim byteHash() As Byte, ret As String = Nothing
        Try
            byteHash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(fichero))
            md5.Clear()
            For Each bs As Byte In byteHash
                ret &= bs.ToString("x2")
            Next
            Return ret
        Catch ex As Exception
        End Try
    End Function


    Private Sub Btncancelarpozo_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Btncancelarpozo.Click
        Me.Close()
    End Sub
End Class