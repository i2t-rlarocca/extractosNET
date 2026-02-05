Imports Sorteos.Bussiness
Imports libEntities.Entities
Imports Sorteos.Data
Imports Sorteos.Helpers

Public Class FrmMensajeConfirmacionReversion
    Public _nombreJuegoARevertir As String
    Public _nroSorteoARevertir As Integer
    Public _nombreJuegoUltimo As String
    Public _nroSorteoUltimo As Integer
    Public _titulo As String = ""
    Public _accion As String = ""

    Private Sub FrmMensajeConfirmacionReversion_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _accion = ""
        Try
            Dim pgmsorteoBO As New PgmSorteoBO
            lblTitulo.Text = _titulo
            txtJuegoARevertir.Text = "Juego:  " & _nombreJuegoARevertir
            txtNroSorteoARevertir.Text = "Sorteo: " & _nroSorteoARevertir

            txtUltimoJuegoConfirmado.Text = "Juego:  " & _nombreJuegoUltimo
            txtUltimoNroSorteoConfirmado.Text = "Sorteo: " & _nroSorteoUltimo

        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnContinuar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnContinuar.Click
        Try
            Me._accion = "CONTINUAR"
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            Me._accion = "CANCELAR"
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub txtcopias_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.btnContinuar.Focus()
        Else
            General.SoloNumeros(sender, e)

        End If
    End Sub

    Private Sub txtcopias_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)

    End Sub

    Private Sub Label3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Label3.Click

    End Sub
End Class