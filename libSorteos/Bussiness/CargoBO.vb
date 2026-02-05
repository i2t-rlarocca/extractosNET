Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Bussiness

    Public Class CargoBO
        Public Function GetCargos() As ListaOrdenada(Of Cargo)
            Try
                Dim listaCargos As ListaOrdenada(Of Cargo)
                Dim oDal As New Data.CargoDAL

                listaCargos = oDal.GetCargos()
                Return listaCargos

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

    End Class
End Namespace
