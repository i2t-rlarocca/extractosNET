Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class ExtractoLoteria
        Public Shared Function GetExtracto(ByVal idLoteria As String, _
                             ByVal idJuego As String, _
                             ByVal numeroSorteo As Integer, _
                             ByVal fecha As String, _
                             ByVal recuperarSorteo As Boolean) As ExtractoEntities.ExtractoLoteria

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                Dim _Extracto As ExtractoEntities.Extracto
                Dim _ExtractoLoteria As New ExtractoEntities.ExtractoLoteria

                _Extracto = Extracto.GetExtracto(idLoteria, idJuego, numeroSorteo, True)

                _ExtractoLoteria.CantidadCifras = _Extracto.CantidadCifras
                _ExtractoLoteria.HoraSorteoOrigen = _Extracto.HoraSorteoOrigen
                _ExtractoLoteria.Id = _Extracto.Id
                _ExtractoLoteria.Numeros = _Extracto.Numeros
                _ExtractoLoteria.NumeroSorteoOrigen = _Extracto.NumeroSorteoOrigen
                _ExtractoLoteria.Sorteo = _Extracto.Sorteo

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From ext_lotchica " & _
                                 "Where ext_loteria = '" & idLoteria & "' " & _
                                 " And jue_id = '" & idJuego & "' " & _
                                 " And ext_sorteo = " & numeroSorteo

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                Return _ExtractoLoteria

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
            Dim dr As SqlDataReader

            Dim ds As New DataSet
            Dim dt_terminacion As New DataTable
            Dim dt_Extracto As New DataTable

            Try
                Dim _ExtractoLoteria As New ExtractoEntities.ExtractoLoteria

                dt_Extracto = Extracto.GetExtractoDT(idLoteria, idJuego, idPgmSorteo)
                dt_Extracto.TableName = "Extracto_Loteria"

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                'cm.CommandText = "Select * From ext_loteria" & _
                '                 " Where ext_loteria = '" & idLoteria & "' " & _
                '                 " And jue_id = '" & idJuego & "' " & _
                '                 " And ext_sorteo = " & numeroSorteo

                'dr = cm.ExecuteReader()
                'dt_terminacion.Load(dr)
                dt_terminacion = Extracto.getPremios_LocalDT(idLoteria, idJuego, idPgmSorteo)
                dt_terminacion.TableName = "Extracto_Terminaciones"


                ds.Tables.Add(dt_Extracto)
                ds.Tables.Add(dt_terminacion)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace

