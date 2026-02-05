Imports Microsoft.VisualBasic

Namespace ExtractoBussiness
    Public Class JuegoBO
        Public Shared Function GetJuego(ByVal id As String) As ExtractoEntities.Juego

            Try
                Return ExtractoData.Juego.GetJuego(id)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace
