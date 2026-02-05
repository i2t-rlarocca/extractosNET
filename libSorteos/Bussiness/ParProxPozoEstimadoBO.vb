Imports Sorteos.Helpers
Imports Sorteos.Data
Imports libEntities.Entities

Namespace Bussiness

    Public Class ParProxPozoEstimadoBO

        Public Function getParProxPozoEstimado(ByVal idPgmSorteo As Integer) As ParProxPozoEstimado
            Dim oDal As New ParProxPozoEstimadoDAL
            Dim o As New ParProxPozoEstimado
            Try
                o = oDal.getParProxPozoEstimado(idPgmSorteo)
            Catch ex As Exception
                o = Nothing
                FileSystemHelper.Log("getParProxPozoEstimado - Excepción: " & ex.Message)
                Throw New Exception("getParProxPozoEstimado - Excepción: " & ex.Message)
            End Try
            Return o
        End Function

        Public Function setParProxPozoEstimado(ByVal oPar As ParProxPozoEstimado) As Boolean
            Dim oDal As New ParProxPozoEstimadoDAL
            Dim res As Boolean
            Try
                res = oDal.setParProxPozoEstimado(oPar)
            Catch ex As Exception
                res = False
                FileSystemHelper.Log("setParProxPozoEstimado - Excepción: " & ex.Message)
                Throw New Exception("setParProxPozoEstimado - Excepción: " & ex.Message)
            End Try
            Return res
        End Function

    End Class

End Namespace

