Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Bussiness


    Public Class LoteriaBO
        Public Function getLoterias() As ListaOrdenada(Of Loteria)
            Dim oDal As New Data.LoteriaDAL
            Dim ls As New ListaOrdenada(Of Loteria)
            Try
                ls = oDal.getLoterias
                Return ls
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function getLoterias(ByVal pidLoteria As Char) As Loteria
            Dim oDal As New Data.LoteriaDAL
            Try
                Return oDal.getLoteria(pidLoteria)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace