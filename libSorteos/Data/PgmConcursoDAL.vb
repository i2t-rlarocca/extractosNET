Imports System.Data
Imports System.Data.SqlClient
Imports libEntities.Entities
Imports Sorteos.Helpers


Namespace Data


    Public Class PgmConcursoDAL

        Public Function Persistir(ByVal o As PgmConcurso) As Boolean
            Try

                Return True
            Catch ex As Exception

                Return False
            End Try

        End Function

        Public Function getPgmConcursoNoIniciadoOIniciado(ByVal fechahora As DateTime) As ListaOrdenada(Of PgmConcurso)
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New ListaOrdenada(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                        & " FROM " _
                        & " PgmConcurso p1 " _
                        & " inner join concurso_v p2 on p1.idconcurso=p2.idconcurso " _
                        & " where p1.idestadoPgmconcurso in (10,20) " _
                        & " and  abs(datediff(minute,p1.fechahora,@fecha)) =(SELECT min(abs(datediff(minute,pgmconcurso.fechahora,@fecha)))as minimo FROM pgmconcurso) " _
                        & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad " _
                        & " order by  p1.fechahora"
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fecha", fechahora)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next

                Return listaPC

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                FileSystemHelper.Log(" PgmConcursoDAL:getPgmConcursoNoIniciadoOIniciado - Excepcion: " & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcurso(ByVal fechaHora As DateTime) As List(Of PgmConcurso)
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                        & " FROM " _
                        & " PgmConcurso p1 " _
                        & " inner join concurso_v p2 on p1.idconcurso=p2.idconcurso " _
                        & " where  abs(datediff(minute,p1.fechahora,@fecha)) =(SELECT min(abs(datediff(minute,pgmconcurso.fechahora,@fecha)))as minimo FROM pgmconcurso) " _
                        & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad " _
                        & " order by  p1.fechahora "

                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fecha", fechaHora)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next

                Return listaPC

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcurso(ByVal id As Long) As PgmConcurso
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                        & " FROM " _
                        & " PgmConcurso p1 " _
                        & " inner join concurso_v p2 on p1.idconcurso=p2.idconcurso " _
                        & " where  idPgmConcurso = @id " _
                        & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad  "
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@id", id)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Load(oPc, r, True)
                Next

                Return oPc

            Catch ex As Exception
                Try
                    If Not dr.IsClosed Then dr.Close()
                Catch ex2 As Exception
                End Try
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcursoDT(ByVal id As Int32) As DataTable
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String


            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select c.descripcion nombreConc, pc.fechaHora fechaHoraConc, j.jue_desc descSorteoRector, ps.nroSorteo nroSorteoRector " _
                     & " from PgmConcurso pc " _
                     & " inner join concurso c on pc.idConcurso = c.idConcurso " _
                     & " inner join PgmSorteo ps on pc.idPgmSorteoPrincipal = ps.idPgmSorteo " _
                     & " inner join Juego j on ps.idJuego = j.idJuego " _
                     & " where pc.idPgmConcurso = @id "

                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@id", id)

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


        Public Shared Function Load(ByRef o As PgmConcurso, _
                                    ByRef dr As DataRow, _
                                    ByVal recuperarObjComponentes As Boolean) As Boolean

            Try
                Dim oC As New ConcursoDAL
                Dim oS As New PgmSorteoDAL
                Dim oE As New ExtraccionesDAL
                Dim oG As New GeneralDAL
                Dim _nombreConcurso() As String
                o.idPgmConcurso = dr("idpgmconcurso")
                o.fechaHora = dr("fechahora")
                o.fechaHoraFinReal = dr("fechaHoraFinReal")
                o.fechaHoraIniReal = dr("fechaHoraIniReal")
                o.Escribano = dr("escribano")
                o.Operador = dr("operador")
                o.localidad = dr("localidad")
                o.concurso = oC.getConcurso(dr("idConcurso"))
                'o.nombre = o.concurso.Nombre & "-" & dr("idPgmSorteoPrincipal")
                o.idPgmSorteoPrincipal = dr("idPgmSorteoPrincipal")
                o.estadoPgmConcurso = dr("idEstadoPgmConcurso")
                o.PgmSorteos = oS.getPgmSorteos(dr("idpgmconcurso"), dr("idConcurso"))
                '** se toma el  1 nrosorteo ya que viene ordenado por orden
                _nombreConcurso = o.concurso.Nombre.Split("-") 'el nombre viene de la forma 'idconcurso-nombreconcurso'
                'o.nombre = _nombreConcurso(0) & "-" & o.PgmSorteos(0).nroSorteo & "-" & _nombreConcurso(1)
                o.nombre = _nombreConcurso(1) & "   -   " & o.PgmSorteos(0).nroSorteo '& " (" & _nombreConcurso(0) & ")"
                o.MetodosIngreso = oG.getMetodosIngreso
                o.MetodosIngresoJurisdicciones = oG.getMetodosIngresoJurisdicciones

                o.Extracciones = oE.getExtraccionesCabs(o.idPgmConcurso, o.concurso.ModeloExtracciones)

                Load = True
            Catch ex As Exception
                FileSystemHelper.Log(" PgmConcursoDAL:LOAD - Excepcion: " & ex.Message)
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function Iniciar(ByVal oPC As PgmConcurso) As Boolean

            Dim cm As SqlCommand = New SqlCommand
            Dim idExtraccionesCabSig As Integer = -1
            Dim msgRet As String = ""

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspConcursoIniciar"

                '@idPgmConcurso int,  -- Entrada
                '@usu char(20), -- Entrada
                '@idEstadoPgmConcurso tinyint, -- Salida
                '@msgRet varchar(1024) -- Salida
                cm.Parameters.AddWithValue("@idPgmConcurso", oPC.idPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@usu", oPC.idPgmConcurso)
                cm.Parameters("@usu").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@idEstadoPgmConcurso", SqlDbType.Int, 1024))
                cm.Parameters("@idEstadoPgmConcurso").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output
                cm.ExecuteNonQuery()

                msgRet = cm.Parameters("@msgRet").Value

                If msgRet.Trim <> "" Then
                    Throw New Exception(msgRet)
                End If

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function


        Public Function Finalizar(ByVal pidPgmConcurso As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim idExtraccionesCabSig As Integer = -1
            Dim msgRet As String = ""
            Dim horaFinConcurso As DateTime
            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspFinalizarExtracciones"

                '@idPgmConcurso int, -- Entrada oblig
                '@HoraFinConcurso datetime, -- Salida. valor por defecto null
                '@msgRet varchar(1024) -- Salida

                cm.Parameters.AddWithValue("@idPgmConcurso", pidPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@HoraFinConcurso", horaFinConcurso)
                cm.Parameters("@HoraFinConcurso").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output
                cm.ExecuteNonQuery()

                horaFinConcurso = IIf(cm.Parameters("@HoraFinConcurso").SqlValue.isnull, Nothing, cm.Parameters("@HoraFinConcurso").Value)
                msgRet = cm.Parameters("@msgRet").Value


                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                End If

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try


        End Function

        Public Function RevertirExtracciones(ByVal pidPgmConcurso As Integer, ByVal pUsuario As String, ByVal pIdExtraccionesCAB As Integer, Optional ByVal modalidad As Integer = -1, Optional ByVal pBorraPozo As Integer = 0) As Integer


            Dim cm As SqlCommand = New SqlCommand
            'Dim dr As SqlDataReader
            Dim idExtraccionesCabSig As Integer = -1
            Dim msgRet As String = ""
            Dim idEstadoPgmConcurso As Integer
            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                'se cambia el nombre del SP a uspextraccionesRevertir
                'cm.CommandText = "uspConcursoRevertir"
                cm.CommandText = "uspExtraccionesRevertir"

                '@idPgmConcurso int, -- Entrada oblig
                '@HoraFinConcurso datetime, -- Salida. valor por defecto null
                '@msgRet varchar(1024) -- Salida

                cm.Parameters.AddWithValue("@idPgmConcurso", pidPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                If pIdExtraccionesCAB = -1 Then
                    cm.Parameters.AddWithValue("@idExtraccionesCab", DBNull.Value)
                Else
                    cm.Parameters.AddWithValue("@idExtraccionesCab", pIdExtraccionesCAB)
                End If

                cm.Parameters("@idExtraccionesCab").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@usu", pUsuario)
                cm.Parameters("@usu").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idExtraccionesCabSig", idExtraccionesCabSig)
                cm.Parameters("@idExtraccionesCabSig").Direction = ParameterDirection.Output

                cm.Parameters.AddWithValue("@idEstadoPgmConcurso", idEstadoPgmConcurso)
                cm.Parameters("@idEstadoPgmConcurso").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                'Modalidad: 1 actualiza estado pero no borra detalle de las extracciones sig,2 borra el detalle de las extracciones sig
                If modalidad = -1 Then ' si no se envia el parametro borra detalle
                    modalidad = 2
                    cm.Parameters.AddWithValue("@modalidad", modalidad)
                Else
                    cm.Parameters.AddWithValue("@modalidad", modalidad)
                End If
                'se borra el pozo de la tabla premio_sorteo cuadnos e revierte todo el concurso para que se muestren correctamente al ingresar al ABM de premios
                cm.Parameters.AddWithValue("@borrapozo", pBorraPozo)
                cm.Parameters("@borrapozo").Direction = ParameterDirection.Input

                cm.ExecuteNonQuery()

                idExtraccionesCabSig = IIf(cm.Parameters("@idExtraccionesCabSig").SqlValue.isnull, Nothing, cm.Parameters("@idExtraccionesCabSig").Value)
                idEstadoPgmConcurso = IIf(cm.Parameters("@idEstadoPgmConcurso").SqlValue.isnull, Nothing, cm.Parameters("@idEstadoPgmConcurso").Value)
                msgRet = cm.Parameters("@msgRet").Value


                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                End If

                Return idExtraccionesCabSig

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return -1
            End Try


        End Function

        Public Function getPgmConcursoIniciadooFinalizado() As List(Of PgmConcurso)
            Dim oPc As PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                        & " FROM " _
                        & " PgmConcurso p1 " _
                        & " inner join concurso p2 on p1.idconcurso=p2.idconcurso " _
                        & " where  p1.idestadoPgmConcurso in(20,30,40) " _
                        & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idconcurso,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad   order by p1.idestadoPgmConcurso"

                cm.CommandText = vsql


                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next
                Return listaPC

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcursoIniciadooFinalizado(ByVal fechaHora As DateTime) As List(Of PgmConcurso)
            Dim oPc As PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                        & " FROM " _
                        & " PgmConcurso p1 " _
                        & " inner join concurso_v p2 on p1.idconcurso=p2.idconcurso " _
                         & " where  abs(datediff(minute,p1.fechahora,@fecha)) =(SELECT min(abs(datediff(minute,pgmconcurso.fechahora,@fecha)))as minimo FROM pgmconcurso)  and p1.idestadoPgmConcurso in(20,30,40) " _
                        & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idconcurso,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad   order by p1.idestadoPgmConcurso"

                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fecha", fechaHora)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next
                Return listaPC

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function ActualizarEstadoConcurso(ByVal pIdPgmConcurso As Integer, ByVal pEstado As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim Transaccion As SqlTransaction = Nothing
            Try
                ActualizarEstadoConcurso = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                Transaccion = cm.Connection.BeginTransaction
                vsql = " UPDATE pgmconcurso SET idEstadoPgmConcurso=" & pEstado & " WHERE idpgmconcurso= " & pIdPgmConcurso
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dr.Close()
                dr = Nothing
                Transaccion.Commit()
                ActualizarEstadoConcurso = True
            Catch ex As Exception
                ActualizarEstadoConcurso = False
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function getPgmConcursoQuiniela() As List(Of PgmConcurso)
            Dim oPc As PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                    & " FROM pgmconcurso p1 " _
                    & " inner join pgmsorteo p2 on p2.idpgmconcurso=p1.idpgmconcurso" _
                    & " where p2.idestadopgmconcurso in(20,30,40) and p2.idjuego in(select idjuego from juego where jue_habi = 'S' and id_agr_juego = 1)" _
                        & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idconcurso,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad   order by p1.idestadoPgmConcurso"

                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next
                Return listaPC

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function ObtenerPgmSorteoQuiniela(ByVal opgmConcurso As PgmConcurso) As PgmSorteo
            Dim opgmsorteo As PgmSorteo
            Dim pgmSorteoBO As New PgmSorteoDAL

            ObtenerPgmSorteoQuiniela = Nothing
            For Each opgmsorteo In opgmConcurso.PgmSorteos
                If opgmsorteo.idAgrJuego = 1 Then
                    Return pgmSorteoBO.getPgmSorteo(opgmsorteo.idPgmSorteo)
                    Exit Function
                End If
            Next
        End Function

        Public Function ObtenerDatosListado(ByVal pidPgmConcurso As Integer) As DataTable
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspObtenerListado"

                cm.Parameters.Clear()

                cm.Parameters.AddWithValue("@idPgmConcurso", pidPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                Return dt

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function ObtenerDatosListadoDifCuad(ByVal pidPgmConcurso As Integer, ByVal pidPgmSorteo As Integer, ByRef visualizar As String) As DataTable
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspObtenerListadoDifCuad"

                cm.Parameters.Clear()

                cm.Parameters.AddWithValue("@idPgmConcurso", pidPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idPgmSorteo", pidPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@visualizar", visualizar)
                cm.Parameters("@visualizar").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)

                dr.Close()
                dt.TableName = "Table1"
                visualizar = cm.Parameters("@visualizar").Value
                'dt.WriteXmlSchema("D:\Visual2008\SorteosCAS\DEV\libExtractos\INFORMES_IAFAS\ListadoDifCuad.xml")
                Return dt

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcursoFinalizado() As List(Of PgmConcurso)
            Dim oPc As PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                        & " FROM " _
                        & " PgmConcurso p1 " _
                        & " inner join concurso p2 on p1.idconcurso=p2.idconcurso " _
                        & " where  p1.idestadoPgmConcurso in(40) " _
                        & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idconcurso,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad   order by p1.idestadoPgmConcurso"

                cm.CommandText = vsql


                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next
                Return listaPC

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function getPgmConcursoFinalizado(ByVal fechahora As DateTime, Optional ByVal soloUltimo As Boolean = False) As ListaOrdenada(Of PgmConcurso)
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New ListaOrdenada(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                If soloUltimo Then
                    vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                            & " FROM " _
                            & " PgmConcurso p1 " _
                            & " inner join concurso_v p2 on p1.idconcurso=p2.idconcurso " _
                            & " where p1.idestadoPgmconcurso in (40) " _
                            & "  and p1.fechahora=(select max(fechahora) as fechahora from pgmconcurso where idestadopgmconcurso = 40) group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad " _
                            & " order by  p1.fechahora desc"
                Else
                    vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                            & " FROM " _
                            & " PgmConcurso p1 " _
                            & " inner join concurso_v p2 on p1.idconcurso=p2.idconcurso " _
                            & " where p1.idestadoPgmconcurso in (40) " _
                            & "  and p1.fechahora>=dateadd(day," & General.DiasSorteosAnteriores * -1 & ",getdate()) group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad " _
                            & " order by  p1.fechahora desc"
                    '& " and  abs(datediff(minute,p1.fechahora,@fecha)) =(SELECT min(abs(datediff(minute,pgmconcurso.fechahora,@fecha)))as minimo FROM pgmconcurso where idEstadopgmconcurso = 40) " _
                End If
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fecha", fechahora)


                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next

                Return listaPC

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function setPgmConcursoEstado(ByVal idPgmConcurso As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspActualizaEstadoConcurso"
                cm.CommandTimeout = 30
                cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input
                
                cm.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                FileSystemHelper.Log("setpgmconcursoEstado:" & ex.Message)
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function ObtenerDatosExtraccionesCAB(ByVal pidPgmConcurso As Integer) As DataTable
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "ObtenerExtraccionesConcurso"

                cm.Parameters.Clear()

                cm.Parameters.AddWithValue("@idPgmConcurso", pidPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

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
        Public Function ObtenerDatosJuegos(ByVal pidPgmConcurso As Integer) As DataTable
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "DetalleJuegosParametros"

                cm.Parameters.Clear()

                cm.Parameters.AddWithValue("@idPgmConcurso", pidPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

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
        Public Function getPgmConcursoFinalizadoConPremios(ByVal fechahora As DateTime) As ListaOrdenada(Of PgmConcurso)
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New ListaOrdenada(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad "
                vsql = vsql & " FROM "
                vsql = vsql & " PgmConcurso p1 "
                vsql = vsql & " inner join concurso_v p2 on p1.idconcurso=p2.idconcurso "
                vsql = vsql & " inner join premio pr on pr.idjuego=p2.idjuego "
                vsql = vsql & " where p1.idestadoPgmconcurso > 10 " 'in (40) "
                vsql = vsql & "  and  abs(datediff(minute,p1.fechahora,@fecha)) =(SELECT min(abs(datediff(minute,pgmconcurso.fechahora,@fecha)))as minimo FROM pgmconcurso where idestadoPgmconcurso > 10) "
                vsql = vsql & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad "
                vsql = vsql & " order by  p1.fechahora desc"
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fecha", fechahora)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next

                Return listaPC

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function EstadoPgmConcurso(ByVal idPgmconcurso As Long) As Integer
            Dim sql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim estado As Integer = 0
            Try
                sql = "select  idestadopgmconcurso from pgmconcurso where idpgmconcurso=" & idPgmconcurso
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                cm.CommandText = sql
                estado = cm.ExecuteScalar
                Return estado
            Catch ex As Exception
                Throw New Exception("Prog de Concursos - Problema al leer estado del concurso ->" & idPgmconcurso & "<-")
                Return estado
            End Try
        End Function
        '** revertir concurso
        Public Function getPgmConcursosaRevertir(ByVal fechaHora As DateTime) As List(Of PgmConcurso)
            Dim oPc As PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaPC As New List(Of PgmConcurso)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT p1.idconcurso,p1.idpgmconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,coalesce(p1.fechaHoraIniReal,'01/01/1999') as fechaHoraIniReal,coalesce(p1.fechaHoraFinReal,'01/01/1999')fechaHoraFinReal,coalesce(p1.escribano,0) escribano,coalesce(p1.operador,'') operador, p1.localidad " _
                        & " FROM " _
                        & " PgmConcurso p1 " _
                        & " inner join pgmsorteo p2 on p1.idpgmconcurso=p2.idpgmconcurso " _
                        & " where p2.idestadoPgmconcurso >=50   " _
                        & " and convert(datetime,convert(varchar(10),p1.fechahora,103),103)>=(select convert(datetime,convert(varchar(10),dateadd(day," & General.DiasSorteosAnteriores * -1 & " ,max(fechahora)),103),103) as fecha from pgmsorteo where idestadopgmconcurso=50)" _
                        & " group by  p1.idconcurso,p1.idestadoPgmConcurso,p1.fechahora,p1.idPgmSorteoPrincipal,p1.fechaHoraIniReal,p1.fechaHoraFinReal,p1.escribano,p1.operador,p1.idpgmconcurso, p1.localidad order by p1.fechahora desc"

                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fecha", fechaHora)


                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oPc = New PgmConcurso
                    Load(oPc, r, True)
                    listaPC.Add(oPc)
                Next
                Return listaPC

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function RevertirConcurso(ByVal pidPgmConcurso As Integer, Optional ByVal pidPgmsorteo As Integer = 0) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim msgRet As String = ""

            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "usprevertirconcurso"

                cm.Parameters.AddWithValue("@idPgmConcurso", pidPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                
                cm.Parameters.AddWithValue("@idpgmsorteo", pidPgmsorteo)
                cm.Parameters("@idPgmsorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output
                cm.ExecuteNonQuery()

                msgRet = cm.Parameters("@msgRet").Value


                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                End If

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try


        End Function



        Public Function Obtener_url_PDF(ByVal idpgmconcurso As Integer, ByRef sin_archivos As Boolean) As String

            Dim cm As SqlCommand = New SqlCommand
            Dim msgRet As String = ""
            Dim vsql As String = ""
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim url As String = ""
            Try
                sin_archivos = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                ' si los campos URLson null,significa que el FTP al 175 no funcionó
                vsql = " select  coalesce(url_primer_premio_age,'')url_primer_premio_age, coalesce(url_provincias,'')url_provincias  FROM PgmConcurso     WHERE idpgmconcurso = " & idpgmconcurso
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                For Each r As DataRow In dt.Rows
                    If r("url_primer_premio_age") = "" And r("url_provincias") = "" Then
                        sin_archivos = True
                    End If
                    url = r("url_primer_premio_age") & "," & r("url_provincias")
                Next
                
                Return url

            Catch ex As Exception
                Throw New Exception(ex.Message)
                If Not dr.IsClosed Then dr.Close()
                Return Nothing
            End Try

        End Function

        Public Function Guardar_url_PDF(ByVal idpgmconcurso As Integer, ByVal nombre_archivo_age As String, ByVal nombre_archivo_prv As String) As Boolean

            Dim cm As SqlCommand = New SqlCommand
            Dim msgRet As String = ""
            Dim vsql As String = ""
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim url As String = ""
            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " UPDATE PgmConcurso set  url_primer_premio_age='" & nombre_archivo_age.Trim & "', url_provincias='" & nombre_archivo_prv.Trim & "'  WHERE idpgmconcurso = " & idpgmconcurso
                cm.CommandText = vsql
                cm.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try

        End Function

        Public Function InsertarPgmsorteosContingencia(ByVal idpgmsorteo As Integer, ByVal idestado As Integer, ByVal idjuego As Integer, ByVal sorteo As Integer, ByVal fecha_hora As String, ByVal fecha_horapres As String, ByVal fecha_horaprox As String) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim msgRet As String = ""
            Dim vsql As String = ""
            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " insert into rec_pgmsorteo (idPgmSorteo,idEstadoPgmConcurso, idJuego, nroSorteo, fechahora, fechaHoraPrescripcion, fechaHoraProximo) VALUES(" & idpgmsorteo & "," & idestado & "," & idjuego & "," & sorteo & ",'" & fecha_hora & "','" & fecha_horapres & "','" & fecha_horaprox & "')"
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.ExecuteNonQuery()




                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try


        End Function

        Public Function ObtenerDatosEscenariosGanadores1Premio(ByVal pidPgmConcurso As Integer, ByVal pcalcular As Integer) As DataTable
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetEscenarioGanadores"

                cm.Parameters.Clear()

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmsorteo", pidPgmConcurso)
                cm.Parameters("@idPgmsorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@msgRet", pcalcular)
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

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

        Public Function ObtenerDatosProximoSorteo(ByVal pidPgmConcurso As Integer, ByVal pcalcular As Integer) As DataTable
            Dim oPc As New PgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspCalcularPozoProxSorteo"

                cm.Parameters.Clear()

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmsorteo", pidPgmConcurso)
                cm.Parameters("@idPgmsorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@calcular", pcalcular)
                cm.Parameters("@calcular").Direction = ParameterDirection.Input

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

        Public Function versionarParametros(ByVal pIdPgmConcurso As Int64, ByVal pComentario As String, ByVal pUsuario As String) As Int32
            Dim cm As SqlCommand = New SqlCommand
            Dim msgRet As String = ""
            Dim nroVersion As Int32 = 0

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspVersionarParametros"

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmConcurso", pIdPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@comentario", pComentario)
                cm.Parameters("@comentario").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@usuario", pUsuario)
                cm.Parameters("@usuario").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@nroVersion", SqlDbType.Int))
                cm.Parameters("@nroVersion").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output
                cm.ExecuteNonQuery()

                msgRet = cm.Parameters("@msgRet").Value

                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                Else
                    nroVersion = cm.Parameters("@nroVersion").Value
                End If

            Catch ex As Exception
                FileSystemHelper.Log("versionarParametros DAL: IdPgmConcurso ->" & pIdPgmConcurso & "<- pComentario ->" & pComentario & "<- pUsuario ->" & pUsuario & "<- Excepción: " & ex.Message)
            End Try
            Return nroVersion
        End Function

    End Class


End Namespace