Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class ExtractoTombola
        Public Shared Function GetExtracto(ByVal idLoteria As String, _
                                    ByVal idJuego As String, _
                                    ByVal numeroSorteo As Integer, _
                                    ByVal fecha As String, _
                                    ByVal recuperarSorteo As Boolean) As ExtractoEntities.ExtractoTombola

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                Dim _Extracto As ExtractoEntities.Extracto
                Dim _ExtractoTombola As New ExtractoEntities.ExtractoTombola

                _Extracto = Extracto.GetExtracto(idLoteria, idJuego, numeroSorteo, True)

                _ExtractoTombola.CantidadCifras = _Extracto.CantidadCifras
                _ExtractoTombola.HoraSorteoOrigen = _Extracto.HoraSorteoOrigen
                _ExtractoTombola.Id = _Extracto.Id
                _ExtractoTombola.Numeros = _Extracto.Numeros
                _ExtractoTombola.NumeroSorteoOrigen = _Extracto.NumeroSorteoOrigen
                _ExtractoTombola.Sorteo = _Extracto.Sorteo

                Return _ExtractoTombola

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Shared Function GetExtractoDT(ByVal idLoteria As String, _
                                           ByVal idJuego As String, _
                                           ByVal idPgmSorteo As Long, _
                                           ByVal fecha As String, _
                                           ByVal recuperarSorteo As Boolean) As DataSet

            Dim cm As SqlCommand = New SqlCommand

            Dim ds As New DataSet
            Dim dt_premio As New DataTable
            Dim dt_sueldo As New DataTable
            Dim dt_Extracto As New DataTable

            Try
                Dim _ExtractoTombola As New ExtractoEntities.ExtractoTombola

                dt_Extracto = Extracto.GetExtractoDT(idLoteria, idJuego, idPgmSorteo)
                dt_Extracto.TableName = "Extracto_Tombola"

                ds.Tables.Add(dt_Extracto)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace

