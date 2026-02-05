Imports Microsoft.VisualBasic

Namespace ExtractoBussiness

    Public Class Sorteo

        Public Shared Function GetSorteo(ByVal id As Long) As ExtractoEntities.Sorteo

            Try
                Return ExtractoData.Sorteo.GetSorteo(id)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

       
    End Class
End Namespace