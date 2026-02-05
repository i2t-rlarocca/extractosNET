Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Data

    Public Class PozoDAL

        Public Function getPozo(ByVal idJuego As Int16, ByVal nroSorteo As Integer) As List(Of Pozo)

            Dim ls As New List(Of Pozo)
            Dim o As New Pozo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT p.idPremio, p.de_premio as nombrePremio, ps2.idPgmSorteo, pz.importe_pozo, pz.importe_pozo_rec, pz.apuestas " _
                    & " FROM pgmsorteo ps2 " _
                    & " inner join premio p on ps2.idjuego = p.idjuego " _
                    & " LEFT JOIN (select p.*, ps.nrosorteo from pgmsorteo_pozos p " _
                    & " inner join pgmsorteo ps on ps.idpgmsorteo = p.idpgmsorteo " _
                    & " where ps.idJuego = @idJuego and ps.nroSorteo = @nroSorteo) pz on p.idPremio = pz.idPremio and ps2.idpgmsorteo = pz.idpgmsorteo " _
                    & " WHERE ps2.idJuego = @idJuego and ps2.nroSorteo = @nroSorteo and p.habilitado=1 and p.carga_pozo=1 " _
                    & " order by p.idPremio "


                cm.CommandText = vsql
                cm.Parameters.Clear()
                'If idJuego = 30 Then
                '    Dim a As String = ""
                'End If
                cm.Parameters.AddWithValue("@idJuego", idJuego)
                cm.Parameters.AddWithValue("@nroSorteo", nroSorteo)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Pozo
                    Load(o, r)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPozoTotal(ByVal idJuego As Int16, ByVal nroSorteo As Integer, ByRef idmodalidad As Integer, ByRef tot_pozo As Double, ByRef tot_pozo_rec As Double, ByRef tot_apu As Int64) As Boolean

            Dim ls As New List(Of Pozo)
            Dim o As New Pozo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT ps2.idPgmSorteo, case ps2.idjuego when 4 then 401001 when 13 then 1301001 when 30 then 3001001 else 0 end as idmodalidad, sum(coalesce(pz.importe_pozo,0)) as importe_pozo, sum(coalesce(pz.importe_pozo_rec,0)) as importe_pozo_rec, coalesce(pz2.apuestas,0) as apuestas " _
                    & " FROM pgmsorteo ps2 " _
                    & " inner join premio p on ps2.idjuego = p.idjuego " _
                    & " left join (select * from pgmsorteo_tot_apuestas_v where idpgmsorteo = @idJuego * 1000000 + @nroSorteo) pz2 on ps2.idpgmsorteo = pz2.idpgmsorteo " _
                    & " LEFT JOIN (select p.*, ps.nrosorteo from pgmsorteo_pozos p " _
                    & " inner join pgmsorteo ps on ps.idpgmsorteo = p.idpgmsorteo " _
                    & " where ps.idJuego = @idJuego and ps.nroSorteo = @nroSorteo) pz on p.idPremio = pz.idPremio and ps2.idpgmsorteo = pz.idpgmsorteo " _
                    & " WHERE ps2.idJuego = @idJuego and ps2.nroSorteo = @nroSorteo and p.habilitado=1 and p.carga_pozo=1 " _
                    & " group by ps2.idPgmSorteo, ps2.idjuego, coalesce(pz2.apuestas,0)"


                cm.CommandText = vsql
                cm.Parameters.Clear()
                'If idJuego = 30 Then
                '    Dim a As String = ""
                'End If
                cm.Parameters.AddWithValue("@idJuego", idJuego)
                cm.Parameters.AddWithValue("@nroSorteo", nroSorteo)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    idmodalidad = r("idmodalidad")
                    tot_pozo = Es_Nulo(Of Double)(r("importe_pozo"), 0)
                    tot_pozo_rec = Es_Nulo(Of Double)(r("importe_pozo_rec"), 0)
                    tot_apu = Es_Nulo(Of Double)(r("apuestas"), 0)
                Next

                Return True

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function Load(ByRef o As Pozo, ByRef dr As DataRow) As Boolean

            Try
                o.idPgmsorteo = dr("idPgmSorteo")
                o.idPremio = dr("idPremio")
                o.importe = Es_Nulo(Of Double)(dr("importe_pozo"), 0)
                o.nombrePremio = dr("nombrePremio")
                o.importeRec = Es_Nulo(Of Double)(dr("importe_pozo_rec"), 0)
                o.apuestas = Es_Nulo(Of Double)(dr("apuestas"), 0)
                Load = True

            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function setPozo(ByVal oPozo As Pozo) As Boolean
            Dim total As Int16
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' determina la operación a realizar
                vsql = " SELECT count(*) FROM pgmsorteo_pozos WHERE idPgmSorteo = @idPgmSorteo AND idPremio = @idPremio "
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", oPozo.idPgmsorteo)
                cm.Parameters.AddWithValue("@idPremio", oPozo.idPremio)
                total = cm.ExecuteScalar()

                If total = 0 Then
                    vsql = " INSERT INTO pgmsorteo_pozos (idPgmSorteo, idPremio, importe_pozo, importe_pozo_rec, apuestas) values (@idPgmSorteo, @idPremio, @importe, @importeRec, @apuestas) "
                Else
                    vsql = " UPDATE pgmsorteo_pozos SET importe_pozo = @importe, importe_pozo_rec = @importeRec, apuestas = @apuestas WHERE idPgmSorteo = @idPgmSorteo AND idPremio = @idpremio "
                End If

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", oPozo.idPgmsorteo)
                cm.Parameters.AddWithValue("@idPremio", oPozo.idPremio)
                cm.Parameters.AddWithValue("@importe", oPozo.importe)
                cm.Parameters.AddWithValue("@importeRec", oPozo.importeRec)
                cm.Parameters.AddWithValue("@apuestas", oPozo.Apuestas)
                cm.ExecuteNonQuery()

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function getPozoDT(ByVal idPgmConcurso As Integer) As DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select p.idPremio, p.idJuego, m.de_modalidad, p.de_premio, sp.importe_pozo " _
                     & " from pgmSorteo_Pozos sp " _
                     & " inner join premio p on sp.idPremio = p.idPremio " _
                     & " inner join modalidad m on p.idJuego = m.idJuego and p.idModalidad = m.idModalidad " _
                     & " inner join pgmSorteo s on sp.idPgmSorteo = s.idPgmSorteo " _
                     & " where s.idPgmSorteo = @idPgmConcurso "

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmConcurso ", idPgmConcurso)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                Return dt

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function BorraPozo(ByVal idpgmsorteo As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "delete from pgmSorteo_Pozos "
                vsql = vsql & " where idPgmSorteo = @idPgmSorteo "
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo ", idpgmsorteo)
                cm.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function ControlUsuarioPozoSugerido(ByVal pUsuario As String, ByVal pPwd As String) As Boolean

        End Function

        Public Function setPozoSugerido(ByVal ImportePozo As Double, ByVal idUsuario As Long, ByVal idjuego As Integer, ByVal fechaModificacion As DateTime) As ListaOrdenada(Of cJuegoSorteo)

            Dim ls As New ListaOrdenada(Of cJuegoSorteo)
            Dim o As New cJuegoSorteo

            Dim dr As SqlDataReader
            Dim dt As New DataTable

            Dim cm As SqlCommand = New SqlCommand
            Dim msgRet As String = ""

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                If ImportePozo <= 0 Then
                    Throw New Exception(" setPozoSugerido: se recibió un valor de Importe menor o igual a cero. No se guardará el valor de pozo estimado.")
                    Return Nothing
                End If
                '**** carga tablas historizas
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspActualizaPozoSugerido"
                cm.CommandTimeout = 40

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idjuego", idjuego)
                cm.Parameters.AddWithValue("@importe", ImportePozo)
                cm.Parameters.AddWithValue("@idusuario", idUsuario)
                cm.Parameters.AddWithValue("@fechaMod", fechaModificacion)

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()

                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return Nothing
                End If
                If dr.HasRows Then
                    dt.Load(dr)
                    For Each r As DataRow In dt.Rows
                        o = New cJuegoSorteo
                        o.idPgmSorteo = r("idpgmsorteo")
                        ls.Add(o)
                    Next
                End If
                dr.Close()
                Return ls

            Catch ex As Exception
                Throw New Exception(" setPozoSugerido: " & ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function setPozoSugerido(ByVal ImportePozo As Double, ByVal idUsuario As Long, ByVal idjuego As Long, ByVal fechaModificacion As Date) As Boolean
            Dim total As Integer
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' determina la operación a realizar
                vsql = " SELECT count(*) FROM juego_pozosugerido WHERE idjuego =" & idjuego
                cm.CommandText = vsql
                total = cm.ExecuteScalar()

                If total = 0 Then
                    vsql = " INSERT INTO juego_pozosugerido (idjuego, importe, idusuariomodificacion,ultimamodificacion) values (" & idjuego & "," & ImportePozo & "," & idUsuario & ", @fecha) "
                Else
                    vsql = " UPDATE juego_pozosugerido SET importe =@importe ,idusuariomodificacion=" & idUsuario & ",ultimamodificacion=@fecha WHERE idjuego =" & idjuego
                End If

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@fecha", fechaModificacion)
                cm.Parameters.AddWithValue("@importe", ImportePozo)
                cm.ExecuteNonQuery()

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        '**22/10/2012
        Public Function getPozoSugerido(ByVal idjuego As Long, Optional ByRef fechaModificacion As DateTime = Nothing) As Double
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim pozo As Double = 0.0
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                fechaModificacion = "01/01/1999"
                ' obtien el pozo sugerido poara el juego indicado
                vsql = " SELECT coalesce(importe,0) as pozo ,ultimamodificacion as fecha FROM juego_pozosugerido WHERE idjuego =" & idjuego
                cm.CommandText = vsql
                dr = cm.ExecuteReader()

                If dr.HasRows Then
                    dr.Read()
                    pozo = dr("pozo")
                    fechaModificacion = dr("fecha")
                End If
                dr.Close()
                Return pozo

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return pozo
            End Try
        End Function

        Public Function getUsuarioPozoSugerido(ByVal idjuego As Long) As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim usuario As String = ""
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' obtien el pozo sugerido poara el juego indicado
                vsql = " SELECT coalesce(usuario,'') as usuario FROM juego_pozosugerido a"
                vsql = vsql & " inner join usuarios b on a.idusuariomodificacion=b.idusuario "
                vsql = vsql & "  WHERE idjuego = " & idjuego
                cm.CommandText = vsql
                dr = cm.ExecuteReader()

                If dr.HasRows Then
                    dr.Read()
                    usuario = dr("usuario")
                End If
                dr.Close()
                Return usuario

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function setParametrosPozoEstimadoProximoSorteo(ByVal idpgmsorteo As Integer, ByVal apu_miercoles As String, ByVal apu_domingo As String, ByVal porc_apu_total_revancha As String, ByVal porc_apu_total_ss As String, ByVal porc_valor_apu_tradicional As String, ByVal porc_valor_apu_revancha As String, ByVal porc_valor_apu_ss As String, ByVal pozo_extra As String, ByVal pozo_adicional As String, ByVal minimoasegurado_tradicional As String, ByVal minimoasegurado_segunda As String, ByVal minimoasegurado_revancha As String, ByVal valorapuesta_tradicional As String, ByVal valorapuesta_revancha As String, ByVal valorapuesta_ss As String, ByVal porc_1premiotradicional As String, ByVal porc_2premiotradicional As String, ByVal porc_3premiotradicional As String, ByVal porc_estimulotradicional As String, ByVal porc_1premiorevancha As String, ByVal porc_estimulorevancha As String, ByVal porc_1premioSS As String, ByVal porc_estimuloSS As String, ByRef msj As String) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspInsertarParametrosPozoProximoSorteo"
                cm.Parameters.Clear()

            
                'los porcentajes vienen como enteros por eso se dividen por 100,ej 2450=24.50
                cm.Parameters.AddWithValue("@idPgmsorteo", idpgmsorteo)
                cm.Parameters.AddWithValue("@apu_miercoles", CDec(apu_miercoles / 100))
                cm.Parameters.AddWithValue("@apu_domingo", CDec(apu_domingo / 100))
                cm.Parameters.AddWithValue("@porc_apu_total_revancha", CDec(porc_apu_total_revancha / 100))
                cm.Parameters.AddWithValue("@porc_apu_total_ss", CDec(porc_apu_total_ss / 100))
                cm.Parameters.AddWithValue("@porc_valor_apu_tradicional", CDec(porc_valor_apu_tradicional / 100))
                cm.Parameters.AddWithValue("@porc_valor_apu_revancha", CDec(porc_valor_apu_revancha / 100))
                cm.Parameters.AddWithValue("@porc_valor_apu_ss", CDec(porc_valor_apu_ss / 100))
                cm.Parameters.AddWithValue("@pozo_extra", CDec(pozo_extra))
                cm.Parameters.AddWithValue("@pozo_adicional", CDec(pozo_adicional))
                cm.Parameters.AddWithValue("@porc_1premio_tradicional", CDec(porc_1premiotradicional / 100))
                cm.Parameters.AddWithValue("@porc_2premio_tradicional", CDec(porc_2premiotradicional / 100))
                cm.Parameters.AddWithValue("@porc_3premio_tradicional", CDec(porc_3premiotradicional / 100))
                cm.Parameters.AddWithValue("@porc_estimulo_tradicional", CDec(porc_estimulotradicional / 100))
                cm.Parameters.AddWithValue("@porc_1premio_revancha", CDec(porc_1premiorevancha / 100))
                cm.Parameters.AddWithValue("@porc_estimulo_revancha", CDec(porc_estimulorevancha / 100))
                cm.Parameters.AddWithValue("@porc_1premio_ss", CDec(porc_1premioSS / 100))
                cm.Parameters.AddWithValue("@porc_estimulo_ss", CDec(porc_estimuloSS / 100))
                cm.Parameters.AddWithValue("@minimo_asegurado_tradicional", CDec(minimoasegurado_tradicional))
                cm.Parameters.AddWithValue("@minimo_asegurado_segunda", CDec(minimoasegurado_segunda))
                cm.Parameters.AddWithValue("@minimo_asegurado_revancha", CDec(minimoasegurado_revancha))
                cm.Parameters.AddWithValue("@valor_apuesta_tradicional", valorapuesta_tradicional)
                cm.Parameters.AddWithValue("@valor_apuesta_revancha", valorapuesta_revancha)
                cm.Parameters.AddWithValue("@valor_apuesta_ss", (valorapuesta_ss))
                cm.Parameters.AddWithValue("@msgRet", msj)
                
                dr = cm.ExecuteReader()
                dr.Close()
                

                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function getParametrosPozoEstimadoProximoSorteo(ByRef idpgmsorteo As Integer, ByRef apu_miercoles As String, ByRef apu_domingo As String, ByRef porc_apu_total_revancha As String, ByRef porc_apu_total_ss As String, ByRef porc_valor_apu_tradicional As String, ByRef porc_valor_apu_revancha As String, ByRef porc_valor_apu_ss As String, ByRef pozo_extra As String, ByRef pozo_adicional As String, ByRef minimoasegurado_tradicional As String, ByRef minimoasegurado_segunda As String, ByRef minimoasegurado_revancha As String, ByRef valorapuesta_tradicional As String, ByRef valorapuesta_revancha As String, ByRef valorapuesta_ss As String, ByRef porc_1premiotradicional As String, ByRef porc_2premiotradicional As String, ByRef porc_3premiotradicional As String, ByRef porc_estimulotradicional As String, ByRef porc_1premiorevancha As String, ByRef porc_estimulorevancha As String, ByRef porc_1premioSS As String, ByRef porc_estimuloSS As String) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspObtenerParametrosPozoProximo"
                

                dr = cm.ExecuteReader()
                idpgmsorteo = -1
                If dr.HasRows Then
                    dr.Read()
                    idpgmsorteo = Es_Nulo(Of Integer)(dr("idpgmsorteo"), 0)
                    apu_miercoles = Es_Nulo(Of Double)(dr("miercoles_domingo"), 0)
                    apu_domingo = Es_Nulo(Of Double)(dr("domingo_miercoles"), 0)
                    porc_apu_total_revancha = Es_Nulo(Of Double)(dr("porc_apu_revancha"), 0)
                    porc_apu_total_ss = Es_Nulo(Of Double)(dr("porc_apu_ss"), 0)
                    porc_valor_apu_tradicional = Es_Nulo(Of Double)(dr("porc_valorapu_tradicional"), 0)
                    porc_valor_apu_revancha = Es_Nulo(Of Double)(dr("porc_valorapu_revancha"), 0)
                    porc_valor_apu_ss = Es_Nulo(Of Double)(dr("porc_valorapu_ss"), 0)

                    pozo_extra = Es_Nulo(Of Double)(dr("pozo_extra"), 0)
                    pozo_adicional = Es_Nulo(Of Double)(dr("pozo_adicional"), 0)

                    minimoasegurado_tradicional = Es_Nulo(Of Double)(dr("minimo_tradicional"), 0)
                    minimoasegurado_segunda = Es_Nulo(Of Double)(dr("minimo_segunda"), 0)
                    minimoasegurado_revancha = Es_Nulo(Of Double)(dr("minimo_revancha"), 0)

                    valorapuesta_tradicional = Es_Nulo(Of Double)(dr("valorapu_tradicional"), 0)
                    valorapuesta_revancha = Es_Nulo(Of Double)(dr("valorapu_revancha"), 0)
                    valorapuesta_ss = Es_Nulo(Of Double)(dr("valorapu_ss"), 0)

                    porc_1premiotradicional = Es_Nulo(Of Double)(dr("p1premio_tradicional"), 0)
                    porc_2premiotradicional = Es_Nulo(Of Double)(dr("p2premio_tradicional"), 0)
                    porc_3premiotradicional = Es_Nulo(Of Double)(dr("p3premio_tradicional"), 0)
                    porc_estimulotradicional = Es_Nulo(Of Double)(dr("estimulo_tradicional"), 0)
                    porc_1premiorevancha = Es_Nulo(Of Double)(dr("p1premio_revancha"), 0)
                    porc_estimulorevancha = Es_Nulo(Of Double)(dr("estimulo_revancha"), 0)
                    porc_1premioSS = Es_Nulo(Of Double)(dr("p1premio_ss"), 0)
                    porc_estimuloSS = Es_Nulo(Of Double)(dr("estimulo_ss"), 0)

                End If
                dr.Close()

                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
    End Class
End Namespace