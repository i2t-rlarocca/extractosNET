Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Data

    Public Class PremioDAL

        Public Function getPremio(ByVal idJuego As Int16, ByVal nroSorteo As Integer) As List(Of Premio)

            Dim ls As New List(Of Premio)
            Dim o As New Premio

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' ''vsql = " SELECT p.idPremio, p.de_premio as nombrePremio, ps2.idPgmSorteo, coalesce(sor.importe_pozo, 0)importe_pozo, " _
                ' ''    & " cant_ganadores, importe_premio, vacante, secuencia " _
                ' ''    & " FROM pgmsorteo ps2 " _
                ' ''    & " inner join premio p on ps2.idjuego = p.idjuego " _
                ' ''    & " left join  premio_sorteo sor on ps2.idPgmSorteo = sor.idPgmSorteo and p.idPremio = sor.idPremio " _
                ' ''    & " WHERE(ps2.idJuego = @idJuego And ps2.nroSorteo = @nroSorteo) " _
                ' ''    & " order by p.idPremio "

                '** consulta premio 
                '** el premio 3001004 pertenece al estimulo de PF que no se carga en ABM de premios
                vsql = " SELECT p.idPremio, p.de_premio as nombrePremio, ps2.idPgmSorteo, coalesce(sor.importe_pozo, pz.importe_pozo,0)importe_pozo, " _
                    & " cant_ganadores, importe_premio, vacante, secuencia,p.requiere_aciertos,coalesce(sor.cant_aciertos,p.aciertos_por_def,'99')aciertos_por_def,habilitado,carga_pozo, Destino_Web_Aciertos,Destino_Web_pozo,Destino_Web_premio,Destino_Web_ganadores" _
                    & " FROM pgmsorteo ps2 " _
                    & " inner join premio p on ps2.idjuego = p.idjuego " _
                    & " LEFT JOIN (select p.*, ps.nrosorteo from pgmsorteo_pozos p " _
                    & " inner join pgmsorteo ps on ps.idpgmsorteo = p.idpgmsorteo " _
                    & " where ps.idJuego = @idJuego and ps.nroSorteo = @nroSorteo) pz on p.idPremio = pz.idPremio and ps2.idpgmsorteo = pz.idpgmsorteo " _
                    & " left join  premio_sorteo sor on ps2.idPgmSorteo = sor.idPgmSorteo and p.idPremio = sor.idPremio " _
                    & " WHERE ps2.idJuego = @idJuego and ps2.nroSorteo = @nroSorteo  and p.habilitado=1" _
                    & " order by p.idPremio "
                '**

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idJuego", idJuego)
                cm.Parameters.AddWithValue("@nroSorteo", nroSorteo)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Premio
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

        Public Function setPremio(ByVal oPremio As Premio) As Boolean
            Dim total As Int16
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' determina la operación a realizar
                vsql = " SELECT count(*) FROM premio_sorteo WHERE idPgmSorteo = @idPgmSorteo AND idPremio = @idPremio "
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", oPremio.idPgmsorteo)
                cm.Parameters.AddWithValue("@idPremio", oPremio.idPremio)
                total = cm.ExecuteScalar()

                If total = 0 Then
                    vsql = " INSERT INTO premio_sorteo (idPgmSorteo, idPremio, importe_pozo, cant_ganadores, importe_premio, vacante, secuencia,cant_aciertos) " _
                         & " values (@idPgmSorteo, @idPremio, @importe_pozo, @cant_ganadores, @importe_premio, @vacante, @secuencia,@cant_aciertos) "
                Else
                    vsql = " UPDATE premio_sorteo " _
                         & " SET importe_pozo = @importe_pozo, cant_ganadores = @cant_ganadores, importe_premio = @importe_premio, vacante = @vacante ,cant_aciertos=@cant_aciertos" _
                         & " WHERE idPgmSorteo = @idPgmSorteo AND idPremio = @idPremio "

                End If

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", oPremio.idPgmsorteo)
                cm.Parameters.AddWithValue("@idPremio", oPremio.idPremio)
                cm.Parameters.AddWithValue("@importe_pozo", oPremio.importePozo)
                cm.Parameters.AddWithValue("@cant_ganadores", oPremio.cantGanadores)
                cm.Parameters.AddWithValue("@importe_premio", oPremio.importePremio)
                cm.Parameters.AddWithValue("@vacante", oPremio.vacante)
                cm.Parameters.AddWithValue("@secuencia", 0)
                If oPremio.AciertosPorDef.Trim = "" Then
                    cm.Parameters.AddWithValue("@cant_aciertos", DBNull.Value)
                Else
                    cm.Parameters.AddWithValue("@cant_aciertos", oPremio.AciertosPorDef)
                End If
                cm.ExecuteNonQuery()

                ''If (oPremio.idPremio = 405005 Or oPremio.idPremio = 1305005) Then
                ''    vsql = " update extracto_qnl set progres = @progres where IdLoteria = 'S' and IdPgmSorteo = @IdPgmSorteo "

                ''    cm.CommandText = vsql
                ''    cm.Parameters.Clear()

                ''End If
                ' si se trata de la quiniela actualiza el campo progresión de la tabla correspondiente
                If Mid(oPremio.idPremio, 1, 2) = "50" Then
                    vsql = " update extracto_qnl set progres = @progres where IdLoteria = 'S' and IdPgmSorteo = @IdPgmSorteo "

                    cm.CommandText = vsql
                    cm.Parameters.Clear()
                    cm.Parameters.AddWithValue("@progres", oPremio.cantGanadores) ' se utiliza esta propiedad pero es el número
                    cm.Parameters.AddWithValue("@idPgmSorteo", oPremio.idPgmsorteo)

                    cm.ExecuteNonQuery()

                    Return True
                End If


            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Shared Function Load(ByRef o As Premio, ByRef dr As DataRow) As Boolean
            Try
                o.idPgmsorteo = dr("idPgmSorteo")
                o.idPremio = dr("idPremio")
                o.importePozo = Es_Nulo(Of Double)(dr("importe_pozo"), 0)
                o.cantGanadores = Es_Nulo(Of Integer)(dr("cant_ganadores"), 0)
                o.importePremio = Es_Nulo(Of Double)(dr("importe_premio"), 0)
                o.vacante = Es_Nulo(Of String)(dr("vacante"), 0)
                o.NombrePremio = Es_Nulo(Of String)(dr("nombrepremio"), "")

                o.Habilitado = Es_Nulo(Of Boolean)(dr("habilitado"), 0)
                o.RequiereAciertos = Es_Nulo(Of Boolean)(dr("requiere_aciertos"), 0)
                o.AciertosPorDef = dr("aciertos_por_def")
                o.CargaPozo = Es_Nulo(Of Boolean)(dr("carga_pozo"), 0)
                o.Destino_Web_Aciertos = Es_Nulo(Of String)(dr("Destino_Web_Aciertos"), "")
                o.Destino_Web_Ganadores = Es_Nulo(Of String)(dr("Destino_Web_Ganadores"), "")
                o.Destino_Web_Pozo = Es_Nulo(Of String)(dr("Destino_Web_Pozo"), "")
                o.Destino_Web_Premio = Es_Nulo(Of String)(dr("Destino_Web_Premio"), "")
                Load = True

            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getDescripcionPremio(ByVal idPremio As Integer) As String
            Dim descripcion As String
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' determina la operación a realizar
                vsql = " SELECT de_premio + ' en ' + ltrim(rtrim(m.de_modalidad)) FROM premio pr " & _
                       " inner join modalidad m on pr.idmodalidad = m.idmodalidad and pr.idjuego = m.idjuego " & _
                       " WHERE idPremio = @idPremio "
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPremio", idPremio)
                descripcion = cm.ExecuteScalar()

                Return descripcion

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return ""
            End Try
        End Function
        Public Function BorraPremioAdicional(ByVal opremio As Premio) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " delete FROM premio_sorteo WHERE idPgmSorteo = @idPgmSorteo AND idPremio in (405001,405002,405003,405004,405005) "
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", opremio.idPgmsorteo)
                cm.ExecuteNonQuery()
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try

        End Function

        Public Sub ObtieneDatosPremio(ByVal idPremio As Integer, ByRef CantAciertos As Integer, ByRef RequierAciertos As Integer, ByRef NombrePremio As String, ByVal Juego As Integer, ByVal Sorteo As Long, Optional ByRef Adic_Tipo As Integer = 1)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                CantAciertos = 0
                RequierAciertos = 0
                NombrePremio = ""
                ' determina la operación a realizar
                ' vsql = " select requiere_aciertos,coalesce(aciertos_por_def,0) as aciertos, de_premio as nombrepremio from premio where habilitado =1 and idpremio=" & idPremio
                vsql = "select requiere_aciertos,coalesce(aciertos_por_def,0) as aciertos, de_premio as nombrepremio, coalesce(ad.tipo,0) as tipo "
                vsql = vsql & " from premio pgm "
                vsql = vsql & " left join (select * from pgmsorteo_adic where idpgmsorteo = " & (Juego * 1000000 + Sorteo) & ") ad on ad.idpgmsorteo / 1000000 = pgm.idpremio / 100000 "
                vsql = vsql & " where habilitado =1 and idpremio=" & idPremio
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    CantAciertos = dr("aciertos")
                    RequierAciertos = dr("requiere_aciertos")
                    NombrePremio = dr("nombrepremio")
                    Adic_Tipo = dr("tipo")
                End If
                dr.Close()

            Catch ex As Exception
                Throw New Exception(ex.Message)

            End Try
        End Sub

        Public Function Tiene_Ganadores_sorteo(ByVal idpgmsorteo As Integer, ByVal idPremio As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                Tiene_Ganadores_sorteo = False
                ' determina la operación a realizar
                vsql = " select coalesce(cant_ganadores,0) as ganadores  from   premio_sorteo p where idpgmsorteo=" & idpgmsorteo & " and idpremio=" & idPremio
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    If dr("ganadores") > 0 Then
                        Tiene_Ganadores_sorteo = True
                    Else
                        Tiene_Ganadores_sorteo = False
                    End If
                End If
                dr.Close()

            Catch ex As Exception
                Throw New Exception(ex.Message)

            End Try
        End Function

        Public Function GuardarPozoEstimadoJuego(ByVal idjuego As Long, ByVal PozoEstimado As Double, ByVal idusuario As Long) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Dim dr As SqlDataReader
            Dim total As Integer = 0
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                ' determina la operación a realizar
                vsql = " SELECT count(*) FROM Juego_pozosugerido WHERE idjuego=" & idjuego
                cm.CommandText = vsql
                total = cm.ExecuteScalar()

                If total = 0 Then
                    vsql = " INSERT INTO Juego_pozosugerido VALUES (" & idjuego & ",@importe," & idusuario & ",@fecha)"
                Else
                    vsql = " UPDATE Juego_pozosugerido set importe=@importe ,ultimamodificacion=@fecha  WHERE idjuego=" & idjuego
                End If

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@fecha", Now)
                cm.Parameters.AddWithValue("@importe", PozoEstimado)
                cm.ExecuteNonQuery()

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try

        End Function
        Public Function InsertarPremiosdesdeWS(ByVal idpgmsorteo)
            Dim cm As SqlCommand = New SqlCommand

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "volcarPremiosWS"
                cm.CommandTimeout = 30
                cm.Parameters.AddWithValue("@idpgmsorteo", idpgmsorteo)
                cm.Parameters("@idpgmsorteo").Direction = ParameterDirection.Input
                cm.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                FileSystemHelper.Log("crearprogramasorteo:" & ex.Message)
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function


    End Class
End Namespace
