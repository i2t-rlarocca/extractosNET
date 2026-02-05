Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Data

    Public Class SueldoDAL
        Public Function getSueldo(ByVal idJuego As Int16, ByVal idPgmSorteo As Integer) As List(Of Sueldo)
            Dim ls As New List(Of Sueldo)
            Dim o As New Sueldo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select idPgmSorteo, idPremio, orden, LTRIM(RTRIM(SUBSTRING(COALESCE(cupon,' '), 1, CASE WHEN CHARINDEX(':',COALESCE(cupon,' ')) > 3 THEN CHARINDEX(':',COALESCE(cupon,' ')) -3 ELSE LEN(COALESCE(cupon,' ')) END))) AS CUPON, agencia, localidad, importe_premio, provincia, razsoc, apuesta " _
                         & " from premio_sueldo_br " _
                         & " where idPgmSorteo = @idPgmSorteo order by orden "

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Sueldo
                    Load(o, r)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function setSueldo(ByVal oSueldo As Sueldo) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " insert into premio_sueldo_br (idPgmSorteo, idPremio, orden, cupon, agencia, localidad, importe_premio, provincia, razsoc, apuesta, secuencia, id_prov, ocr_prv, premio) " _
                     & " values (@idPgmSorteo, @idPremio, @orden, @cupon, @agencia, @localidad, @importe_premio, @provincia, @razsoc, @apuesta, @secuencia, @id_prov, @ocr_prv, @premio) "
                
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", oSueldo.idPgmsorteo)
                cm.Parameters.AddWithValue("@idPremio", oSueldo.idPremio)
                cm.Parameters.AddWithValue("@orden ", oSueldo.orden)
                cm.Parameters.AddWithValue("@cupon", oSueldo.cupon)
                cm.Parameters.AddWithValue("@agencia", oSueldo.agencia)
                cm.Parameters.AddWithValue("@localidad", oSueldo.localidad)
                cm.Parameters.AddWithValue("@importe_premio", oSueldo.importePremio)
                cm.Parameters.AddWithValue("@provincia", oSueldo.provincia)
                cm.Parameters.AddWithValue("@razsoc", oSueldo.razonSocial)
                cm.Parameters.AddWithValue("@apuesta", oSueldo.apuesta)
                cm.Parameters.AddWithValue("@secuencia", oSueldo.orden)
                cm.Parameters.AddWithValue("@id_prov", 0)
                cm.Parameters.AddWithValue("@ocr_prv", "")
                cm.Parameters.AddWithValue("@premio", "SUELDO " & oSueldo.orden & " BRINCO")
                cm.ExecuteNonQuery()

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function eliminarSueldo(ByVal idPgmSorteo As Double) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " delete from premio_sueldo_br where idPgmSorteo = @idPgmSorteo "

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.ExecuteNonQuery()

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function Load(ByRef o As Sueldo, ByRef dr As DataRow) As Boolean
            Try
                o.idPgmsorteo = dr("idPgmSorteo")
                o.idPremio = dr("idPremio")
                o.orden = dr("orden")
                o.cupon = dr("cupon")
                o.agencia = dr("agencia")
                o.localidad = dr("localidad")
                o.importePremio = dr("importe_premio")
                o.provincia = dr("provincia")
                o.razonSocial = dr("razsoc")
                o.apuesta = dr("apuesta")

                Load = True

            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace
