Imports Sorteos.Helpers
Imports Sorteos.Bussiness
Imports libEntities.Entities
Imports System.IO
Imports System.Security.Cryptography
Public Class Frmlogin
    Dim usuarioBO As New UsuarioBO
    Dim ousuario As New cUsuario
    Private Sub btnIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIngresar.Click
        ingresar()
    End Sub
    Private Sub Ingresar()
        Dim pwd As String
        Try
            pwd = ObtenerMD5(Me.txtpwd.Text.ToLower)
            ousuario = usuarioBO.getUsuario(Me.txtUsuario.Text, pwd)
            If Not ousuario Is Nothing Then
                If ousuario.LoginHabilitado Then
                    'actualiza el ultimo acceso
                    usuarioBO.ActualizaUltimoAcceso(ousuario.IdUsuario)
                    lblerror.Visible = True
                    Me.Visible = False
                    Me.Hide()
                    MDIContenedor.usuarioAutenticado = ousuario
                    MDIContenedor.Show()
                    'Me.Visible = False
                    'Me.Hide()
                Else
                    lblerror.Text = "Usuario no habilitado."
                    lblerror.Visible = True
                End If
            Else
                lblerror.Text = "Usuario y/o contraseña incorrectos"
                lblerror.Visible = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

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
            Return Nothing
        End Try
    End Function

    Private Sub Btncancelarlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        MDIContenedor.usuarioAutenticado = Nothing
        Me.Close()
    End Sub


    Private Sub Frmlogin_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If General.LoginHabilitado = "N" Then
            Me.txtpwd.Text = "anulacion"
            Me.txtUsuario.Text = "Sistema"
            Ingresar()
            'Me.Visible = False
            'Me.Hide()
        End If

    End Sub

    
End Class