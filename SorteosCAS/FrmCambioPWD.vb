Imports Sorteos.Helpers
Imports Sorteos.Bussiness
Imports libEntities.Entities
Imports System.IO
Imports System.Security.Cryptography

Public Class FrmCambioPWD

    Dim usuarioBO As New UsuarioBO
    Dim ousuario As New cUsuario

    Private Sub btnIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIngresar.Click
        Ingresar()
    End Sub

    Private Sub Ingresar()
        Dim pwd As String
        Dim pwdN As String
        Try
            pwd = ObtenerMD5(Me.txtpwd.Text.ToLower)
            ousuario = usuarioBO.getUsuario(Me.txtUsuario.Text, pwd)
            If Not ousuario Is Nothing Then
                If txtPwdN.Text.Trim = "" Then
                    lblerror.Text = "La contraseña nueva no puede ser vacía."
                    lblerror.Visible = True
                    txtPwdN.Focus()
                    txtPwdN.SelectAll()
                    Exit Sub
                End If
                If txtPwdN2.Text.Trim = "" Then
                    lblerror.Text = "Debe reingresar la nueva contraseña."
                    lblerror.Visible = True
                    txtPwdN2.Focus()
                    txtPwdN2.SelectAll()
                    Exit Sub
                End If
                If txtPwdN.Text <> txtPwdN2.Text Then
                    lblerror.Text = "La contraseña nueva no coincide con el reingreso."
                    lblerror.Visible = True
                    txtPwdN.Focus()
                    txtPwdN.SelectAll()
                    Exit Sub
                End If
                If txtpwd.Text = txtPwdN.Text Then
                    lblerror.Text = "La contraseña nueva debe ser diferente a la actual."
                    lblerror.Visible = True
                    txtPwdN.Focus()
                    txtPwdN.SelectAll()
                    Exit Sub
                End If
                lblerror.Text = ""
                'actualiza la contrasenia
                pwdN = ObtenerMD5(Me.txtPwdN.Text.ToLower)
                usuarioBO.ActualizaContrasenia(ousuario.IdUsuario, pwdN)
                MsgBox("Contraseña actualizada satisfactoriamente.", MsgBoxStyle.Information, MDIContenedor.Text)
                Me.Close()
                Try
                    Me.Dispose()
                Catch ex As Exception
                End Try
            Else
                lblerror.Text = "Usuario y/o contraseña incorrectos. Verifique y vuelva a ingresar."
                lblerror.Visible = True
                txtUsuario.Focus()
                txtUsuario.SelectAll()
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

    Private Sub BtnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnCancelar.Click
        Me.Close()
    End Sub

    Private Sub FrmCambioPWD_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub

End Class