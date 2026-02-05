Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class ExtractoPoceada
        Public Shared Function GetExtracto(ByVal idLoteria As String, _
                            ByVal idJuego As String, _
                            ByVal numeroSorteo As Integer, _
                            ByVal fecha As String, _
                            ByVal recuperarSorteo As Boolean) As ExtractoEntities.ExtractoPoceada

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                Dim _Extracto As ExtractoEntities.Extracto
                Dim _ExtractoPoceada As New ExtractoEntities.ExtractoPoceada

                _Extracto = Extracto.GetExtracto(idLoteria, idJuego, numeroSorteo, True)

                _ExtractoPoceada.CantidadCifras = _Extracto.CantidadCifras
                _ExtractoPoceada.HoraSorteoOrigen = _Extracto.HoraSorteoOrigen
                _ExtractoPoceada.Id = _Extracto.Id
                _ExtractoPoceada.Numeros = _Extracto.Numeros
                _ExtractoPoceada.NumeroSorteoOrigen = _Extracto.NumeroSorteoOrigen
                _ExtractoPoceada.Sorteo = _Extracto.Sorteo

                Return _ExtractoPoceada

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
            Dim dt_premio As New DataTable
            Dim dt_sueldo As New DataTable
            Dim dt_Extracto As New DataTable
            Dim sql As String = ""
            Try
                Dim _ExtractoPoceada As New ExtractoEntities.ExtractoPoceada

                dt_Extracto = Extracto.GetExtractoDT(idLoteria, idJuego, idPgmSorteo)
                dt_Extracto.TableName = "Extracto_Poceada"

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                'cm.CommandText = "Select p.*, IIf(IsNull(vas.mod_valapu) Or vas.mod_valapu= 0,va.mod_valapu,vas.mod_valapu) AS mod_valapu From (( ext_poceadafederal p" & _
                '                 " left join  valor_apuesta_sorteo vas on p.jue_id = vas.jue_id and  p.ext_sorteo = vas.ext_sorteo )" & _
                '                 " left join  valor_apuesta va on p.jue_id = va.jue_id )" & _
                '                 " Where p.ext_loteria = '" & idLoteria & "' " & _
                '                 "  And p.jue_id = '" & idJuego & "' " & _
                '                 "  And p.ext_sorteo = " & numeroSorteo

                dt_premio = Extracto.getPremios_LocalDT(idLoteria, idJuego, idPgmSorteo)
                dt_premio.TableName = "Extracto_Poceada_Premios"

                ds.Tables.Add(dt_Extracto)
                ds.Tables.Add(dt_premio)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace

