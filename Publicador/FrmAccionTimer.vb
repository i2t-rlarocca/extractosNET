Public Class FrmAccionTimer
    Public vAccion As Integer = -1
    Public leyenda As String = ""
    
    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        vAccion = 0
        Me.Close()
        Me.Dispose()

    End Sub

    Private Sub btnAvisarMastarde_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAvisarMastarde.Click
        vAccion = 1
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub FrmAccionTimer_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            txtleyenda.Text = leyenda
        Catch ex As Exception

        End Try

    End Sub
End Class