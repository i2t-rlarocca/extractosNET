Imports libEntities.Entities
Imports System.Data
Imports System.Data.SqlClient

Imports Sorteos.Helpers

Namespace Data


    Public Class GeneralDAL
        Public Function getParametro(ByVal proc As String, ByVal cod As String) As String
            Dim cm As SqlCommand = New SqlCommand

            Dim vsql As String

            Dim valor As String = ""

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  par_valor FROM parametros where upper(ltrim(rtrim(par_proc))) = upper(ltrim(rtrim('" & proc & "'))) " & _
                       " and upper(ltrim(rtrim(par_cod))) = upper(ltrim(rtrim('" & cod & "'))) "

                cm.CommandText = vsql

                valor = cm.ExecuteScalar

                If cm.Connection.State <> ConnectionState.Closed Then cm.Connection.Close()
                cm.Dispose()
                cm = Nothing

                Return valor

            Catch ex As Exception

                valor = ""
                Throw New Exception(ex.Message)
                Return valor
            End Try
        End Function

        Public Function getOrdeExtraccion(ByVal idOrdenExtraccion As Integer) As OrdenExtraccion
            Dim o As New OrdenExtraccion
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM ordenextraccion where idOrdenExtraccion= " & idOrdenExtraccion
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    LoadOrdenExtraccion(o, r, True)
                Next

                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoadOrdenExtraccion(ByRef o As OrdenExtraccion, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try
                o.IdOrdenExtraccion = dr("IdOrdenExtraccion")
                o.Nombre = dr("Nombre")
                o.Descripcion = dr("Descripcion")

                LoadOrdenExtraccion = True
            Catch ex As Exception
                LoadOrdenExtraccion = False
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Function getOrdenEnExtracto(ByVal idOrdenEnExtracto As Integer) As OrdenEnExtracto
            Dim o As New OrdenEnExtracto
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM OrdenEnExtracto where idOrdenEnExtracto= " & idOrdenEnExtracto
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    LoadOrdenEnExtracto(o, r, True)
                Next

                Return o

            Catch ex As Exception
                dr = Nothing

                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoadOrdenEnExtracto(ByRef o As OrdenEnExtracto, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try

                o.idOrdenEnExtracto = dr("idOrdenEnExtracto")
                o.Nombre = dr("Nombre")
                o.Descripcion = dr("Descripcion")

                LoadOrdenEnExtracto = True
            Catch ex As Exception
                LoadOrdenEnExtracto = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getMetodoIngreso(ByVal idMetodoIngreso As Integer) As MetodoIngreso
            Dim o As New MetodoIngreso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM MetodoIngreso where idMetodoIngreso= " & idMetodoIngreso
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    LoadMetodoIngreso(o, r, True)
                Next

                Return o

            Catch ex As Exception
                dr = Nothing

                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoadMetodoIngreso(ByRef o As MetodoIngreso, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try

                o.IDMetodoIngreso = dr("IDMetodoIngreso")
                o.Nombre = dr("Nombre")
                o.Descripcion = dr("Descripcion")

                LoadMetodoIngreso = True
            Catch ex As Exception
                LoadMetodoIngreso = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getMetodosIngreso() As ListaOrdenada(Of MetodoIngreso)
            Dim ls As New ListaOrdenada(Of MetodoIngreso)
            Dim o As MetodoIngreso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                ' *** ATENCION *** HAY QUE SACAR EL WHERE CUANDO SE IMPLEMENTE EL DOBLE DIGITADOR!!!
                vsql = " SELECT  * FROM MetodoIngreso where habilitadoExtracciones = 'S' order by idmetodoIngreso"
                '& " where idmetodOIngreso < 4 " _
                '& " order by idmetodoIngreso "
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New MetodoIngreso
                    LoadMetodoIngreso(o, r, True)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function getMetodosIngresoJurisdicciones() As ListaOrdenada(Of MetodoIngreso)
            Dim ls As New ListaOrdenada(Of MetodoIngreso)
            Dim o As MetodoIngreso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                ' *** ATENCION *** HAY QUE SACAR EL WHERE CUANDO SE IMPLEMENTE EL DOBLE DIGITADOR!!!
                vsql = " SELECT  * FROM MetodoIngreso where habilitadoJurisdicciones = 'S' order by idmetodoIngreso"
                '& " where idmetodOIngreso < 4 " _
                '& " order by idmetodoIngreso "
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New MetodoIngreso
                    LoadMetodoIngreso(o, r, True)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoadMetodosIngreso(ByRef o As MetodoIngreso, _
                                         ByRef dr As DataRow, _
                                         ByVal recuperarObjComponentes As Boolean) As Boolean

            Try

                o.IDMetodoIngreso = dr("IDMetodoIngreso")
                o.Nombre = dr("Nombre")
                o.Descripcion = dr("Descripcion")

                LoadMetodosIngreso = True
            Catch ex As Exception
                LoadMetodosIngreso = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getTipoTope(ByVal idTipoTope As Integer) As TipoTope
            Dim o As New TipoTope
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM TipoTope where idTipoTope= " & idTipoTope
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    LoadTipoTope(o, r, True)
                Next

                Return o

            Catch ex As Exception
                dr = Nothing

                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoadTipoTope(ByRef o As TipoTope, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try

                o.idTipoTope = dr("idTipoTope")
                o.nombre = dr("Nombre")
                o.descripcion = dr("Descripcion")

                LoadTipoTope = True
            Catch ex As Exception
                LoadTipoTope = False
                Throw New Exception(ex.Message)
            End Try
        End Function



        Public Function getCriterioFinExtraccion(ByVal idCriterioFin As Integer) As CriterioFinExtraccion
            Dim o As New CriterioFinExtraccion
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM CriterioFinExtraccion where idCriterioFinExtraccion= " & idCriterioFin
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    LoadCriterioFinExtraccion(o, r, True)
                Next

                Return o

            Catch ex As Exception
                dr = Nothing

                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoadCriterioFinExtraccion(ByRef o As CriterioFinExtraccion, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try

                o.idCriterioFinExtraccion = dr("idCriterioFinExtraccion")
                o.Nombre = dr("Nombre")
                o.Descripcion = dr("Descripcion")

                LoadCriterioFinExtraccion = True
            Catch ex As Exception
                LoadCriterioFinExtraccion = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getEstadoPgmConcurso(ByVal idEstadoPgmConcurso As Long) As EstadoPgmConcurso
            Dim o As New EstadoPgmConcurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM EstadoPgmConcurso where idEstadoPgmConcurso= " & idEstadoPgmConcurso
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    LoadEstadoPgmConcurso(o, r, True)
                Next

                Return o

            Catch ex As Exception
                dr = Nothing

                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoadEstadoPgmConcurso(ByRef o As EstadoPgmConcurso, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try

                o.idEstadoPgmConcurso = dr("idEstadoPgmConcurso")
                o.Nombre = dr("Nombre")
                o.Descripcion = dr("Descripcion")

                LoadEstadoPgmConcurso = True
            Catch ex As Exception
                LoadEstadoPgmConcurso = False
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Function getTipoValorExtraido(ByVal idTipoValorExtraido As Long) As TipoValorExtraido
            Dim o As New TipoValorExtraido
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM TipoValorExtraido where idTipoValorExtraido= " & idTipoValorExtraido
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    LoadTipoValorExtraido(o, r, True)
                Next

                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoadTipoValorExtraido(ByRef o As TipoValorExtraido, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try

                o.IdTipoValorExtraido = dr("IdTipoValorExtraido")
                o.Nombre = dr("Nombre")
                o.Descripcion = dr("Descripcion")

                LoadTipoValorExtraido = True
            Catch ex As Exception
                LoadTipoValorExtraido = False
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Function ObtenerPuerto() As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim puerto As String = ""
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT coalesce(Par_Valor,'') as Par_Valor   FROM parametros where par_cod='PUERTO_COM'"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    puerto = dr("Par_Valor")
                End If
                dr.Close()
                Return puerto
            Catch ex As Exception
                dr = Nothing
                Return puerto
                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function ObtenerSonido() As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim sonido As String = ""
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT coalesce(Par_Valor,'') as Par_Valor   FROM parametros where par_cod='CON_SONIDO'"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    sonido = dr("Par_Valor")
                End If
                dr.Close()
                Return sonido
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Return sonido
                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function LoteriaComenzada(ByVal pIdpgmsorteo As Integer, ByVal pIdloteria As Char, ByRef pCifras As Integer) As Boolean
            Dim Nro As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Try
                LoteriaComenzada = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                Nro = ""
                vsql = " SELECT  LEN(COALESCE(LTRIM(RTRIM(NRO1T)),'')) +LEN(COALESCE(LTRIM(RTRIM(NRO2T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO3T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO4T)),'')) +LEN(COALESCE(LTRIM(RTRIM(NRO5T)),''))"
                vsql = vsql & " +LEN(COALESCE(LTRIM(RTRIM(NRO6T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO7T)),'')) +LEN(COALESCE(LTRIM(RTRIM(NRO8T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO9T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO10T)),''))"
                vsql = vsql & " +LEN(COALESCE(LTRIM(RTRIM(NRO11T)),'')) +LEN(COALESCE(LTRIM(RTRIM(NRO12T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO13T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO14T)),'')) +LEN(COALESCE(LTRIM(RTRIM(NRO15T)),''))"
                vsql = vsql & " +LEN(COALESCE(LTRIM(RTRIM(NRO16T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO17T)),'')) +LEN(COALESCE(LTRIM(RTRIM(NRO18T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO19T)),''))+LEN(COALESCE(LTRIM(RTRIM(NRO20T)),''))AS NRO ,cifras"
                vsql = vsql & "  FROM EXTRACTO_QNL  "
                vsql = vsql & "  WHERE idpgmsorteo=" & pIdpgmsorteo & "and idloteria='" & pIdloteria & "'"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    Nro = dr("nro")
                    pCifras = dr("cifras")
                    If Nro.Trim <> "" Then

                        LoteriaComenzada = True
                    End If
                End If
                dr.Close()

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function ObtenerClaveJur(ByVal idLoteria As String) As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim puerto As String = ""
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT COALESCE(clave,'') as clave FROM loteria WHERE idLoteria = '" & idLoteria & "'"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    puerto = dr("clave")
                End If
                dr.Close()
                Return puerto
            Catch ex As Exception
                dr = Nothing
                Return puerto
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function ObtenerCarpetaArchivosExtractoOtrasJuris(ByVal idJuego As Integer, ByVal idLoteria As String) As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim path As String = ""
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT COALESCE(path_extracto,'') as path_extracto FROM loteria WHERE idLoteria = '" & idLoteria & "'"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    path = General.Es_Nulo(Of String)(dr("path_extracto"), "")
                End If
                dr.Close()

                ''If path = "" Then
                ''    path = "C:\"
                ''End If

                Return path
            Catch ex As Exception
                dr = Nothing
                Return path
                Throw New Exception(ex.Message)
            End Try

        End Function
    End Class
End Namespace