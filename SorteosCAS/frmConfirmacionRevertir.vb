Public Class frmConfirmacionRevertir
    Public _Modalidad As Integer = -1
    Public _Cancelado As Boolean = False
    Public _NombreExtraccion As String
    Private Sub RadioRevertirEstado_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioRevertirEstado.Click
        _Modalidad = 1
    End Sub

    Private Sub RadioRevertirNumeros_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles RadioRevertirNumeros.Click
        _Modalidad = 2
    End Sub

    Private Sub btnAceptar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAceptar.Click
        Try
            _Cancelado = False
            If _Modalidad <= 0 Then
                MsgBox("Debe elegir una modalidad de reversión", MsgBoxStyle.Information)
                Exit Sub
            End If
            Select Case _Modalidad
                Case 1 'revertir solo el estado
                    If MsgBox("Se revertirá solo el estado de '" & _NombreExtraccion & "' pero se conservarán los números sorteados. ¿Desea continuar?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then
                        _Cancelado = True
                    End If
                Case 2
                    If MsgBox("Se revertirá completamente '" & _NombreExtraccion & "'. ¿Desea continuar?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then
                        _Cancelado = True
                    End If
                Case Else
                    _Cancelado = True
            End Select
            Me.Close()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information)

        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        _Cancelado = True
        Me.Close()
    End Sub

End Class