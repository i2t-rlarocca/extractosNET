Imports Sorteos.Helpers
Public Class frmConfiguracion


    Private Sub btnIniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIniciar.Click
        Try
            Dim ProcesoBo As New ProccessBO
            Dim msj As String = ""
            Dim lRuta As String = Application.StartupPath

            If Not ProcesoBo.IniciarProceso(lRuta, "Publicador", "Publicador.exe", True, msj) Then
                MsgBox(msj, MsgBoxStyle.Information)
                Exit Sub
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub btnDetener_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnDetener.Click
        Try
            Dim ProcesoBo As New ProccessBO
            Dim msj As String = ""
            If Not ProcesoBo.DetenerProceso("Publicador", msj) Then
                MsgBox(msj, MsgBoxStyle.Information)
                Exit Sub
            End If
            MsgBox("se ha detenido el proceso, para limpiar el ico en la barra situie el mouse sobreel icono")
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
End Class
