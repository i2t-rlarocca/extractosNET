Imports Sorteos.Bussiness
Imports libEntities.Entities
Imports Sorteos.Data
Imports Sorteos.Helpers

Public Class FrmMensajeComparacionExtractos
    Public _idpgmconcurso As Integer
    Public _idpgmsorteo As Integer



    Private Sub FrmMensajeComparacionExtractos_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Dim pgmsorteoBO As New PgmSorteoBO
            Dim msj As String = ""
            msj = pgmsorteoBO.MostrarListadoDiferencias(_idpgmconcurso, _idpgmsorteo)
            txtmensaje.Text = msj
            txtmensaje.ReadOnly = True


            Me.txtcopias.Text = 2
        Catch ex As Exception

        End Try
    End Sub

    Private Sub btnContinuar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinuar.Click
        Try
            Dim pgmsorteoBO As New PgmSorteoBO
            Dim msj As String = ""
            Dim destino As String = "0"
            Dim path As String = ""
            If Me.txtcopias.Text.Trim = "" Then
                txtcopias.Text = 1
            End If
            If Not pgmsorteoBO.GenerarListadoDifCuad(_idpgmconcurso, _idpgmsorteo, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, destino, path, txtcopias.Text, msj) Then
                MsgBox(msj, MsgBoxStyle.Information)
            End If
            Me.Close()
        Catch ex As Exception

        End Try
    End Sub

    Private Sub txtcopias_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtcopias.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.btnContinuar.Focus()
        Else
            General.SoloNumeros(sender, e)

        End If
    End Sub

    Private Sub txtcopias_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtcopias.TextChanged

    End Sub
End Class