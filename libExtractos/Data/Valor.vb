Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData
    Public Class Valor
        Public Shared Function GetValores(ByVal juego As String, ByVal sorteo As String) As String

            Dim o As New ExtractoEntities.Sorteo
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim Valores As String = ""
            Dim valor As String = ""
            Dim sent As String = ""

            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text


                sent = " SELECT va.idjuego, " & sorteo & " AS ext_sorteo, va.idmodalidad, "
                sent += " convert(varchar,convert(int,round(case when coalesce(a.vap_valapu,0)= 0 then va.vap_valapu else a.vap_valapu end,0)))  AS mod_valapu "
                sent += " FROM valor_apuesta AS va "
                sent += " LEFT JOIN (select *  from valor_apuesta_sorteo vs  where  vs.idpgmsorteo = " & (juego * 1000000 + sorteo) & " ) AS a "
                sent += " ON (va.idvalorapuesta = a.idvalorapuesta)"
                sent += " WHERE (((va.idjuego)= " & juego & " )); "

                cm.CommandText = sent '"Select * From valor_apuesta_sorteo Where jue_id = @idJuego  and ext_sorteo = @numeroSorteo order by mod_id "
                cm.Parameters.AddWithValue("@idJuego", juego)
                cm.Parameters.AddWithValue("@numeroSorteo", sorteo)


                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    If Valores = "" Then
                        valor = r("mod_valapu")
                        Valores += valor
                    Else
                        valor = r("mod_valapu")
                        Valores += "-" & valor
                    End If
                Next

                Return Valores

            Catch ex As Exception
                Valores = ""
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace
