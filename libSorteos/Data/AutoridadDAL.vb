Imports libEntities.Entities
Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers

Namespace Data

    Public Class AutoridadDAL

        Public Function getAutoridad() As ListaOrdenada(Of Autoridad)
            Dim ls As New ListaOrdenada(Of Autoridad)
            Dim o As New Autoridad
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select j.jue_desc, a.idAutoridad, a.nombre, a.cargo, a.orden, 0 idPgmSorteo" _
                      & " from autoridad a " _
                      & " inner join juego j on a.idJuego = j.idJuego " _
                      & " order by jue_desc "
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Autoridad
                    Load(o, r)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                ls = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function

        Public Function getAutoridad(ByVal nombre As String, ByVal idJuego As Integer) As ListaOrdenada(Of Autoridad)
            Dim ls As New ListaOrdenada(Of Autoridad)
            Dim o As New Autoridad
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim sWhere As String

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                sWHere = "nombre like '%" & nombre & "%' "
                sWhere &= IIf(idJuego <> 0, " and j.idJuego = " & idJuego, "")

                vsql = " select j.idJuego, j.jue_desc, a.idAutoridad, a.nombre, a.cargo, a.orden, 0 idPgmSorteo " _
                     & " from autoridad a " _
                     & " inner join juego j on a.idJuego = j.idJuego " _
                     & " where " & sWHere & " order by jue_desc "
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Autoridad
                    Load(o, r)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                ls = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getAutoridad(ByVal idJuego As Integer) As ListaOrdenada(Of Autoridad)
            Dim ls As New ListaOrdenada(Of Autoridad)
            Dim o As New Autoridad
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select j.idJuego, j.jue_desc, a.idAutoridad, a.nombre, a.cargo, a.orden, 0 idPgmSorteo " _
                     & " from autoridad a " _
                     & " inner join juego j on a.idJuego = j.idJuego " _
                     & " where j.idJuego = @idJuego order by jue_desc "

                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@idJuego", idJuego)
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Autoridad
                    Load(o, r)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                ls = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getAutoridadDT(ByVal idPgmConcurso As Integer) As DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT a.idJuego, a.nombre autNombre, a.cargo autCargo " _
                     & " FROM autoridad a  " _
                     & " inner join pgmSorteo_Autoridad sa on a.idAutoridad = sa.idAutoridad   " _
                     & " inner join pgmSorteo s on sa.idPgmSorteo = s.idPgmSorteo   " _
                     & " where s.idPgmConcurso= @idPgmConcurso "
                cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)
                cm.CommandText = vsql

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

        
        Public Function Load(ByRef o As Autoridad, ByRef dr As DataRow) As Boolean
            Try
                o.idAutoridad = dr("idAutoridad")
                o.idJuego = dr("idJuego")
                o.juegoDesc = dr("jue_desc")
                o.idPgmSorteo = dr("idPgmSorteo")
                o.Orden = dr("orden")
                o.Nombre = dr("Nombre")
                o.cargo = dr("cargo")
                o.Firma = dr("firma")

                Return True
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Function setAutoridad(ByVal oAutoridad As Autoridad) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim sql As String
            Dim dr As SqlDataReader
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                If oAutoridad.idAutoridad = 0 Then
                    sql = "insert into autoridad (idJuego, cargo, nombre, orden) values (@idJuego, @cargo, @nombre, @orden)"
                    cm.CommandText = sql
                    cm.Parameters.Clear()
                    cm.Parameters.AddWithValue("@idJuego", oAutoridad.idJuego)
                    cm.Parameters.AddWithValue("@cargo", oAutoridad.cargo)
                    cm.Parameters.AddWithValue("@nombre", oAutoridad.Nombre)
                    cm.Parameters.AddWithValue("@orden", oAutoridad.Orden)
                    cm.ExecuteNonQuery()
                    'obtengo el ultimo ID de autoridad de la tabla
                    sql = " select @@identity as valor"
                    cm.CommandText = sql
                    dr = cm.ExecuteReader()
                    dr.Read()
                    oAutoridad.idAutoridad = dr("valor")
                    dr.Close()
                    dr = Nothing
                Else
                    sql = " update autoridad set idJuego = @idJuego, cargo = @cargo, nombre = @nombre, orden = @orden where idAutoridad =  @idAutoridad "
                    cm.CommandText = sql
                    cm.Parameters.Clear()
                    cm.Parameters.AddWithValue("@idJuego", oAutoridad.idJuego)
                    cm.Parameters.AddWithValue("@cargo", oAutoridad.cargo)
                    cm.Parameters.AddWithValue("@nombre", oAutoridad.Nombre)
                    cm.Parameters.AddWithValue("@orden", oAutoridad.Orden)
                    cm.Parameters.AddWithValue("@idAutoridad", oAutoridad.idAutoridad)
                    cm.ExecuteNonQuery()
                End If

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function eliminarAutoridad(ByVal oAutoridad As Autoridad) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim sql As String
            Dim total As Integer

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                sql = " select count(*) from PgmSorteo_Autoridad where idAutoridad =  @idAutoridad "
                cm.CommandText = sql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idAutoridad", oAutoridad.idAutoridad)
                total = cm.ExecuteScalar()
                If total > 0 Then
                    MsgBox("La autoridad participa en al menos un sorteo." & vbCr & vbCr & "No es posible eliminarla.")
                    Return False
                End If


                sql = " delete from Autoridad where idAutoridad =  @idAutoridad "
                cm.CommandText = sql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idAutoridad", oAutoridad.idAutoridad)
                cm.ExecuteNonQuery()

                Return True

            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Function setAutoridad_PgmSorteo(ByVal oAutoridad As Autoridad) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim sql As String
            Dim total As Int16
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' determina si está la autoridad insertada y determina la acción
                sql = " select count(*) from PgmSorteo_Autoridad where idPgmSorteo = @idPgmSorteo and idAutoridad = @idAutoridad "
                cm.Parameters.Clear()
                cm.CommandText = sql
                cm.Parameters.AddWithValue("@idPgmSorteo", oAutoridad.idPgmSorteo)
                cm.Parameters.AddWithValue("@idAutoridad", oAutoridad.idAutoridad)
                total = cm.ExecuteScalar()

                If total = 0 Then
                    sql = "insert into PgmSorteo_Autoridad (idPgmSorteo, idAutoridad, orden) values (@idPgmSorteo, @idAutoridad, @orden)"
                    cm.CommandText = sql
                    cm.Parameters.Clear()
                    cm.Parameters.AddWithValue("@idPgmSorteo", oAutoridad.idPgmSorteo)
                    cm.Parameters.AddWithValue("@idAutoridad", oAutoridad.idAutoridad)
                    cm.Parameters.AddWithValue("@orden", oAutoridad.Orden)
                    cm.ExecuteNonQuery()
                Else
                    sql = " update PgmSorteo_Autoridad set orden = @orden where idPgmSorteo = @idPgmSorteo and idAutoridad = @idAutoridad "
                    cm.CommandText = sql
                    cm.Parameters.Clear()
                    cm.Parameters.AddWithValue("@idPgmSorteo", oAutoridad.idPgmSorteo)
                    cm.Parameters.AddWithValue("@idAutoridad", oAutoridad.idAutoridad)
                    cm.Parameters.AddWithValue("@orden", oAutoridad.Orden)
                    cm.ExecuteNonQuery()
                End If

                Return True

            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function eliminarAutoridad_PgmSorteo(ByVal oAutoridad As Autoridad, ByVal idPgmSorteo As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim sql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                sql = " delete from PgmSorteo_Autoridad where idAutoridad =  @idAutoridad and idPgmSorteo = @idPgmSorteo"
                cm.CommandText = sql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idAutoridad", oAutoridad.idAutoridad)
                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.ExecuteNonQuery()

                Return True

            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getAutoridad_PgmSorteo(ByVal idPgmSorteo As Integer) As ListaOrdenada(Of Autoridad)
            Dim ls As New ListaOrdenada(Of Autoridad)
            Dim o As New Autoridad
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT j.idJuego, j.jue_desc, a.idJuego, a.idAutoridad, a.cargo, a.nombre, sa.idPgmSorteo, sa.orden, a.firma FROM PgmSorteo_autoridad sa " _
                     & " INNER JOIN Autoridad a on sa.idAutoridad = a.idAutoridad " _
                     & " INNER JOIN juego j on a.idJuego = j.idJuego " _
                     & " WHERE idPgmSorteo = @idPgmSorteo order by a.orden"
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Autoridad
                    Load(o, r)

                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                ls = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function AplicarAutoridadAlResto(ByVal idPgmSorteo As Integer, ByVal OPC As PgmConcurso) As Boolean
            Dim ls As New ListaOrdenada(Of Autoridad)
            Dim o As New Autoridad
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable
            Dim pgmsorteo As PgmSorteo
            Dim oautoridad As Autoridad
            Dim _IdautoridadEnJuego As Integer
            Try
                AplicarAutoridadAlResto = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                ls = Me.getAutoridad_PgmSorteo(idPgmSorteo)
                'recorro todos los sorteos del concurso
                For Each pgmsorteo In OPC.PgmSorteos
                    If pgmsorteo.idPgmSorteo <> idPgmSorteo Then
                        'quito las autoridades para un sorteo en particular
                        eliminarAutoridad_PgmSorteo(pgmsorteo.idPgmSorteo)
                        For Each oautoridad In ls
                            'existe una autoridad con el mismo cargo,nombre y orden en el juego
                            'solo inserta en pgmsorteo_autoridad
                            If ExisteAutoridaEnJuego(oautoridad, pgmsorteo.idJuego, _IdautoridadEnJuego) Then
                                oautoridad.idJuego = pgmsorteo.idJuego
                                oautoridad.idAutoridad = _IdautoridadEnJuego
                                oautoridad.idPgmSorteo = pgmsorteo.idPgmSorteo
                                Me.setAutoridad_PgmSorteo(oautoridad)
                            Else
                                oautoridad.idJuego = pgmsorteo.idJuego
                                oautoridad.idAutoridad = 0
                                'se tiene que agregar la nueva autoridad
                                Me.setAutoridad(oautoridad)
                                'se inserta la autoridad al pgmsorteo
                                oautoridad.idPgmSorteo = pgmsorteo.idPgmSorteo
                                Me.setAutoridad_PgmSorteo(oautoridad)
                            End If
                        Next
                    End If
                Next
                Return True
            Catch ex As Exception
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                ls = Nothing
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function eliminarAutoridad_PgmSorteo(ByVal idPgmSorteo As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim sql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                sql = " delete from PgmSorteo_Autoridad where  idPgmSorteo = @idPgmSorteo"
                cm.CommandText = sql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.ExecuteNonQuery()

                Return True

            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function ExisteAutoridaEnJuego(ByVal oAutoridad As Autoridad, ByVal pIdJuego As Integer, ByRef pIdAutoridadenJuego As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim sql As String
            Dim dr As SqlDataReader = Nothing
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                ExisteAutoridaEnJuego = False
                sql = "select idautoridad from autoridad where cargo='" & oAutoridad.cargo & "' and nombre='" & oAutoridad.Nombre & "' and orden=" & oAutoridad.Orden & " and idjuego=" & pIdJuego
                cm.CommandText = sql
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    pIdAutoridadenJuego = dr("idautoridad")
                    ExisteAutoridaEnJuego = True
                Else
                    ExisteAutoridaEnJuego = False
                End If
                dr.Close()
            Catch ex As Exception
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

    End Class
End Namespace