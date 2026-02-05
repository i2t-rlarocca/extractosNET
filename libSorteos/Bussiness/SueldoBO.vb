Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Bussiness

    Public Class SueldoBO

        Public Function getSueldo(ByVal idJuego As Int32, ByVal idPgmSorteo As Long) As List(Of Sueldo)
            Try
                Dim listaSueldo As List(Of Sueldo)
                Dim oDal As New Data.SueldoDAL

                listaSueldo = oDal.getSueldo(idJuego, idPgmSorteo)
                Return listaSueldo

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function setSueldo(ByVal oSueldo As Sueldo) As Boolean
            Try
                Dim oDal As New Data.SueldoDAL

                Return oDal.setSueldo(oSueldo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function eliminarSueldo(ByVal idPgmSorteo As Double) As Boolean
            Try
                Dim oDal As New Data.SueldoDAL

                Return oDal.eliminarSueldo(idPgmSorteo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function validasueldo(ByVal oSueldo As Sueldo, ByRef msgRet As String) As Boolean
            Try
                msgRet = ""

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

    End Class
End Namespace
