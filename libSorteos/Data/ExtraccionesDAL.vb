Imports System.Data
Imports System.Data.SqlClient
Imports libEntities.Entities
Imports Sorteos.Helpers
Imports Sorteos.Helpers.General


Namespace Data

    Public Class ExtraccionesDAL

        Public Function getExtraccionesCabs(ByVal idPgmConcurso As Long, Optional ByRef modEx As ModeloExtracciones = Nothing) As ListaOrdenada(Of ExtraccionesCAB)

            Dim ls As New ListaOrdenada(Of ExtraccionesCAB)

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim oCabecera As ExtraccionesCAB

            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT  * FROM extraccionesCAB where idpgmConcurso= " & idPgmConcurso
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    oCabecera = New ExtraccionesCAB
                    If modEx IsNot Nothing Then
                        Load(oCabecera, r, False)
                        For Each om As ModeloExtraccionesDet In modEx.ModeloExtraccionesDet
                            If oCabecera.idModeloExtraccionesDET = om.idModeloExtraccionesDet Then
                                oCabecera.ModeloExtraccionesDET = om
                                Exit For
                            End If
                        Next
                        oCabecera.ExtraccionesDET = getEstructuraExtraccionesDET(oCabecera.idExtraccionesCAB, oCabecera.ModeloExtraccionesDET)
                    Else
                        Load(oCabecera, r, True)
                    End If
                    ls.Add(oCabecera)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function


        Public Function Cargar(ByVal idPGMConcurso As Long, ByVal oModeloExtraccionDET As ModeloExtraccionesDet) As ExtraccionesCAB

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim oCabecera As ExtraccionesCAB
            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM extraccionesCAB where idpgmConcurso= " & idPGMConcurso & " and idModeloExtraccionesDET=" & oModeloExtraccionDET.idModeloExtraccionesDet
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()


                oCabecera = New ExtraccionesCAB
                oCabecera.ModeloExtraccionesDET = oModeloExtraccionDET
                For Each r As DataRow In dt.Rows
                    Load(oCabecera, r, True)
                Next
                oCabecera.ExtraccionesDET = getEstructuraExtraccionesDET(oCabecera.idExtraccionesCAB, oModeloExtraccionDET)
                Return oCabecera

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Throw New Exception(ex.Message)
            End Try

        End Function



        Public Function Load(ByRef o As ExtraccionesCAB, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try
                Dim omI As New GeneralDAL
                Dim oExtraccionesDet As New ListaOrdenada(Of ExtraccionesDET)
                Dim oAux As New ModeloExtraccionesDAL

                o.idExtraccionesCAB = dr("idExtraccionesCAB")
                o.idModeloExtraccionesDET = dr("idModeloExtraccionesDet")
                o.FechaHoraFinReal = Es_Nulo(Of Date)(dr("FechaHoraFinReal"), "01/01/1999")
                o.FechaHoraIniReal = Es_Nulo(Of Date)(dr("FechaHoraIniReal"), "01/01/1999")
                o.idPgmConcurso = dr("idPgmConcurso")
                o.orden = dr("orden")
                o.Titulo = dr("Titulo")
                o.MetodoIngreso = omI.getMetodoIngreso(dr("idmetodoingreso"))
                o.Ejecutada = dr("estadoEjecucion")
                If recuperarObjComponentes Then
                    o.ModeloExtraccionesDET = (New ModeloExtraccionesDAL).getModeloExtraccionDET(dr("idModeloExtraccionesDET"))
                    o.ExtraccionesDET = getEstructuraExtraccionesDET(o.idExtraccionesCAB, o.ModeloExtraccionesDET)
                End If

                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getExtraccionesDET(ByVal idExtraccionCAB As Long) As ListaOrdenada(Of ExtraccionesDET)
            Dim ls As New ListaOrdenada(Of ExtraccionesDET)

            Dim o As New ExtraccionesDET

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text


                vsql = " SELECT * FROM extraccionesDET where idextraccionesCAB=" & idExtraccionCAB
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New ExtraccionesDET
                    LoadExtraccionesDET(o, r, True)
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
        Public Function LoadExtraccionesDET(ByRef o As ExtraccionesDET, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try
                o.FechaHora = dr("fechahora")
                o.idExtraccionesCAB = dr("idExtraccionesCAB")
                o.idExtraccionesDET = dr("idExtraccionesDET")
                o.Orden = dr("Orden")
                o.PosicionEnExtracto = dr("PosicionEnExtracto")

                o.Valor = dr("Valor")
                'para pintar grilla textox  cuando es repetido en pestaña opcional
                o.Repetido = 0

                LoadExtraccionesDET = True
            Catch ex As Exception
                LoadExtraccionesDET = False
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Function getEstructuraExtraccionesDET(ByVal idExtraccionCAB As Long, ByRef oModeloExtraccionDET As ModeloExtraccionesDet) As ListaOrdenada(Of ExtraccionesDET)
            Dim lsEstructura As New ListaOrdenada(Of ExtraccionesDET)
            Dim lsdetalle As New ListaOrdenada(Of ExtraccionesDET)
            Dim o As New ExtraccionesDET
            Dim oeAux As New ExtraccionesDET
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable

            Dim i As Integer
            Dim tope As Integer

            Try
                lsdetalle = getExtraccionesDET(idExtraccionCAB)
                If oModeloExtraccionDET.tipoTope.idTipoTope = 1 Then ' Fijo
                    tope = oModeloExtraccionDET.cantExtractos
                Else
                    tope = lsdetalle.Count
                End If
                For i = 1 To tope
                    o = New ExtraccionesDET
                    o.PosicionEnExtracto = i
                    o.FechaHora = "01/01/1999"
                    o.Repetido = 0
                    lsEstructura.Add(o)
                Next

                For Each oeAux In lsEstructura
                    oeAux.Valor = -1
                    oeAux.Orden = -1
                    For Each o In lsdetalle
                        If o.PosicionEnExtracto = oeAux.PosicionEnExtracto Then
                            oeAux.FechaHora = o.FechaHora
                            oeAux.idExtraccionesCAB = o.idExtraccionesCAB
                            oeAux.idExtraccionesDET = o.idExtraccionesDET
                            oeAux.Orden = o.Orden
                            oeAux.PosicionEnExtracto = o.PosicionEnExtracto
                            oeAux.Valor = o.Valor
                            Exit For
                        End If
                    Next
                Next
                lsdetalle = Nothing
                Return lsEstructura
            Catch ex As Exception
                dr = Nothing
                lsEstructura = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try


        End Function
       

        Public Function ExisteNroCAB(ByVal idExtraccionCAB As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim resultado As Boolean
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            Try

                vsql = " SELECT * FROM extraccionesCAB where idextraccionesCAB=" & idExtraccionCAB
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                If dr.HasRows = True Then
                    resultado = True
                Else
                    resultado = False
                End If
                dr.Close()
                Return resultado
            Catch ex As Exception
                ExisteNroCAB = False
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function ExisteNroDET(ByVal idExtraccionDET As Integer, ByVal idExtraccionesCAB As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim resultado As Boolean
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            Try
                vsql = " SELECT * FROM extraccionesDET where idextraccionesDET=" & idExtraccionDET & " and idextraccionesCAB=" & idExtraccionesCAB
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows = True Then
                    resultado = True
                Else
                    resultado = False
                End If
                dr.Close()
                Return resultado
            Catch ex As Exception
                ExisteNroDET = False
                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function InsertarActualizarExtraccionesDET(ByVal pCabecera As ExtraccionesCAB, ByVal pExtraccionesDET As ExtraccionesDET, Optional ByVal pModifica As Boolean = False) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim resultado As Boolean
            Dim Transaccion As SqlTransaction = Nothing
            Dim _fecha As DateTime
            Dim msgRet As String = ""

            _fecha = pExtraccionesDET.FechaHora
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                resultado = False
                Transaccion = cm.Connection.BeginTransaction
                If pModifica = False Then
                    vsql = " insert into extraccionesDET(idExtraccionesCAB,orden,valor,posicionenextracto,fechahora)  values (" & pCabecera.idExtraccionesCAB & "," & pExtraccionesDET.Orden & ",'" & pExtraccionesDET.Valor & "'," & pExtraccionesDET.PosicionEnExtracto & " ,@fecha)"
                    cm.Transaction = Transaccion
                    cm.CommandText = vsql
                    cm.Parameters.AddWithValue("@fecha", _fecha)
                    dr = cm.ExecuteReader()
                    dr.Close()
                    dr = Nothing
                    'obtengo el ultimo ID ExtraccionesDET de la tabla
                    vsql = " select @@identity as valor"
                    cm.CommandText = vsql
                    cm.Transaction = Transaccion
                    dr = cm.ExecuteReader()
                    dr.Read()
                    pExtraccionesDET.idExtraccionesDET = dr("valor")
                    dr.Close()
                    dr = Nothing
                    '** se comenta esta parte por las modificaciones realizadas en control de fecha de la incidencia 2481
                    ' '' Si es la primer bolita actualizo la fechahoraINIReal
                    ''If pExtraccionesDET.Orden = 1 And pCabecera.orden = 1 Then

                    ''    vsql = " select fechahora from pgmconcurso where idpgmconcurso = @idpgmconcurso "
                    ''    cm.CommandText = vsql
                    ''    cm.Parameters.Clear()
                    ''    cm.Parameters.AddWithValue("@idpgmconcurso", pCabecera.idPgmConcurso)
                    ''    dr = cm.ExecuteReader()
                    ''    dr.Read()
                    ''    _fecha = dr("fechahora")
                    ''    dr.Close()
                    ''    dr = Nothing

                    ''    'se actualiza pgmsorteo y pgmconcurso con la fechahora del pgmconcurso
                    ''    vsql = "update pgmsorteo set fechahorainireal = @fecha1 where idpgmconcurso = @idpgmconcurso "
                    ''    cm.Transaction = Transaccion
                    ''    cm.CommandText = vsql
                    ''    cm.Parameters.Clear()
                    ''    cm.Parameters.AddWithValue("@fecha1", _fecha)
                    ''    cm.Parameters.AddWithValue("@idpgmconcurso", pCabecera.idPgmConcurso)
                    ''    cm.ExecuteNonQuery()

                    ''    vsql = "update pgmconcurso set fechahorainireal = @fecha2 where idpgmconcurso = @idpgmconcurso1 "
                    ''    cm.Transaction = Transaccion
                    ''    cm.CommandText = vsql
                    ''    cm.Parameters.Clear()
                    ''    cm.Parameters.AddWithValue("@fecha2", _fecha)
                    ''    cm.Parameters.AddWithValue("@idpgmconcurso1", pCabecera.idPgmConcurso)
                    ''    cm.ExecuteNonQuery()
                    ''End If
                Else
                    vsql = " update extraccionesDET  set valor='" & pExtraccionesDET.Valor & "',posicionenExtracto=" & pExtraccionesDET.PosicionEnExtracto & ",orden=" & pExtraccionesDET.Orden & "   where idextraccionesDET=" & pExtraccionesDET.idExtraccionesDET & " and idextraccionesCAB=" & pCabecera.idExtraccionesCAB
                    cm.CommandText = vsql
                    'cm.Parameters.AddWithValue("@fecha", _fecha)
                    cm.Transaction = Transaccion
                    dr = cm.ExecuteReader()
                    dr.Close()
                    dr = Nothing
                End If
                Transaccion.Commit()

                resultado = True

            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
                resultado = False
            End Try
            'HG se pasa a la funcion Generar aparte
            ' ''Try
            ' ''    '**** carga tablas historizas
            ' ''    cm.Parameters.Clear()
            ' ''    cm.CommandType = CommandType.StoredProcedure
            ' ''    cm.CommandText = "uspGenerarExtracto"
            ' ''    cm.CommandTimeout = 10

            ' ''    cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
            ' ''    cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

            ' ''    cm.Parameters.AddWithValue("@idPgmConcurso", pCabecera.idPgmConcurso)
            ' ''    cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

            ' ''    cm.Parameters.AddWithValue("@idCabeceraActual", pCabecera.idExtraccionesCAB)
            ' ''    cm.Parameters("@idCabeceraActual").Direction = ParameterDirection.Input


            ' ''    cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
            ' ''    cm.Parameters("@msgRet").Direction = ParameterDirection.Output

            ' ''    cm.ExecuteNonQuery()

            ' ''    msgRet = cm.Parameters("@msgRet").Value
            ' ''    'If msgRet <> "" Then
            ' ''    '    Throw New Exception(msgRet)
            ' ''    'End If
            ' ''    '****
            ' ''Catch ex As Exception

            ' ''End Try
            Return resultado
        End Function

        ' esta reemplazaria a confirmarCabecera
        Public Function Confirmar(ByRef o As ExtraccionesCAB, ByRef horaFinConcurso As DateTime) As Long
            Dim cm As SqlCommand = New SqlCommand
            Dim idExtraccionesCabSig As Integer = -1
            Dim msgRet As String = ""

            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspConfirmarExtraccion"

                '@idPgmConcurso int, -- Entrada oblig
                '@idExtraccionesCab int, -- Entrada oblig
                '@HoraIniReal datetime, -- Entrada opcional.  valor por defecto getdate() 
                '@HoraFinReal datetime, -- Entrada opcional.  valor por defecto getdate() 
                '@idExtraccionesCabSig int, -- Salida. valor por defecto null = no hay siguiente
                '@HoraFinConcurso datetime, -- Salida. valor por defecto null
                '@msgRet varchar(1024) -- Salida
                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmConcurso", o.idPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input
                cm.Parameters.AddWithValue("@idExtraccionesCab", o.idExtraccionesCAB)
                cm.Parameters("@idExtraccionesCab").Direction = ParameterDirection.Input

                '@HoraIniReal
                cm.Parameters.Add(New SqlParameter("@HoraIniReal", SqlDbType.DateTime))
                cm.Parameters("@HoraIniReal").Direction = ParameterDirection.Input
                cm.Parameters("@HoraIniReal").Value = o.FechaHoraIniReal

                '@HoraFinReal
                cm.Parameters.Add(New SqlParameter("@HoraFinReal", SqlDbType.DateTime))
                cm.Parameters("@HoraFinReal").Direction = ParameterDirection.Input
                cm.Parameters("@HoraFinReal").Value = o.FechaHoraFinReal '.ToString("dd/MM/yyyy HH:mm:ss")

                cm.Parameters.Add(New SqlParameter("@idExtraccionesCabSig", SqlDbType.Int))
                cm.Parameters("@idExtraccionesCabSig").Direction = ParameterDirection.Output

                cm.Parameters.AddWithValue("@HoraFinConcurso", horaFinConcurso)
                cm.Parameters("@HoraFinConcurso").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.ExecuteNonQuery()

                idExtraccionesCabSig = IIf(cm.Parameters("@idExtraccionesCabSig").SqlValue.isnull, -1, cm.Parameters("@idExtraccionesCabSig").Value)
                horaFinConcurso = IIf(cm.Parameters("@HoraFinConcurso").SqlValue.isnull, Nothing, cm.Parameters("@HoraFinConcurso").Value)
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

        ' esta ya no se usaria
        Public Sub ConfirmarCabecera(ByVal pidCabecera As Integer, ByVal FechaInicio As Date, ByVal fechaFin As Date, ByVal pEjecutada As Integer)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim Transaccion As SqlTransaction = Nothing
            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                Transaccion = cm.Connection.BeginTransaction
                vsql = " update extraccionesCAB set fechaHoraIniReal=@fechaini,fechaHoraFinReal=@fechafin,ejecutada=" & pEjecutada & " where idExtraccionesCab= " & pidCabecera
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fechaini", FechaInicio)
                cm.Parameters.AddWithValue("@fechafin", fechaFin)
                dr = cm.ExecuteReader()
                dr.Close()
                dr = Nothing
                Transaccion.Commit()
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Sub
        Public Function ActualizarMetodoIngreso(ByVal pIdCabecera As Integer, ByVal pIdMetodoIngreso As Integer)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim Transaccion As SqlTransaction = Nothing
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            Try
                Transaccion = cm.Connection.BeginTransaction
                vsql = " UPDATE extraccionesCAB SET idmetodoingreso=" & pIdMetodoIngreso & " WHERE idextraccionesCAB=" & pIdCabecera
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dr.Close()
                dr = Nothing
                Transaccion.Commit()
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getExtraccionesDT(ByVal idPgmConcurso As Long) As DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT e.nombre nombreExtraccion, ec.esObligatoria, " _
                     & " e.cantExtractos cantExtracciones, e.sorteaPosicion, tt.nombre tipoTope,  " _
                     & " e.cantCifras, e.valorMinimo, e.valorMaximo, cf.nombre criterioFinExtraccion, mi.nombre metodoIngreso  " _
                     & " FROM extraccionesCAB ec " _
                     & " inner join modeloExtraccionesDet med on ec.idModeloExtraccionesDET = med.idModeloExtraccionesDET " _
                     & " inner join tipoExtraccion e on med.idTipoExtraccion = e.idTipoExtraccion " _
                     & " inner join metodoIngreso mi on e.idMetodoIngreso = mi.idMetodoIngreso " _
                     & " inner join tipoTope tt on e.idTipoTope = tt.idTipoTope " _
                     & " inner join criterioFinExtraccion cf on e.idCriterioFinExtraccion = cf.idCriterioFinExtraccion " _
                     & " where ec.idpgmConcurso = " & idPgmConcurso
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
        Public Function getExtraccionesQuini6(ByVal pidPgmConcurso As Integer) As ListaOrdenada(Of String)
            Dim ls As New ListaOrdenada(Of String)

            Dim o As New ExtraccionesDET

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text


                vsql = "SELECT P1.VALOR,P2.ORDEN"
                vsql = vsql & " FROM EXTRACCIONESDET  P1"
                vsql = vsql & " LEFT JOIN EXTRACCIONESCAB P2 ON P2.IDEXTRACCIONESCAB=P1.IDEXTRACCIONESCAB"
                vsql = vsql & " LEFT JOIN PGMCONCURSO P3 ON P3.IDPGMCONCURSO=P2.IDPGMCONCURSO"
                vsql = vsql & " WHERE P3.IDPGMCONCURSO=" & pidPgmConcurso & " AND P2.ORDEN IN(1,2,3)"
                vsql = vsql & " ORDER BY P2.ORDEN,P1.VALOR"

                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                While (dr.Read)
                    ls.Add(CStr(dr.GetValue(0)))
                End While
                dr.Close()
                dr = Nothing
                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                ls = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try


        End Function
        Public Function BorrarExtraccionesDEt(ByVal ocabecera As ExtraccionesCAB, ByVal porden As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Dim Transaccion As SqlTransaction = Nothing
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                Transaccion = cm.Connection.BeginTransaction
                vsql = " delete from extraccionesDET  where idextraccionesCAB=" & ocabecera.idExtraccionesCAB & " and orden >" & porden
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                cm.ExecuteNonQuery()
               
                Transaccion.Commit()
                BorrarExtraccionesDEt = True
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
                BorrarExtraccionesDEt = False
            End Try
        End Function

        Public Function OrdenaPosicionExtractoExtraccionesDET(ByVal idExtraccionCAB As Long) As Boolean
            Dim ls As New ListaOrdenada(Of ExtraccionesDET)


            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim i As Integer
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text


                vsql = " SELECT * FROM extraccionesDET where idextraccionesCAB=" & idExtraccionCAB & " order by valor"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                i = 1
                For Each r As DataRow In dt.Rows
                    vsql = " UPDATE  extraccionesDET set posicionEnExtracto=" & i & " where idextraccionesdet=" & r("idextraccionesdet")
                    cm.CommandText = vsql
                    cm.ExecuteNonQuery()
                    i = i + 1
                Next

                OrdenaPosicionExtractoExtraccionesDET = True

            Catch ex As Exception
                OrdenaPosicionExtractoExtraccionesDET = False
                If Not dr.IsClosed Then dr.Close()
                ls = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try


        End Function
        Public Function ObtenerPosicionExtracto(ByVal idExtraccionesCAB As Integer, ByVal pOrden As Integer) As Integer
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim i As Integer
            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                i = 0

                vsql = " SELECT coalesce(posicionEnextracto,0)as posicionEnExtracto FROM extraccionesDET where idextraccionesCAB=" & idExtraccionesCAB & " and orden=" & pOrden
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    i = dr("posicionEnExtracto")
                End If

                dr.Close()
                dr = Nothing
                Return i
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function GenerarExtracto(ByVal pCabecera As ExtraccionesCAB) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim Transaccion As SqlTransaction = Nothing
            Dim msgRet As String = ""

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                '**** carga tablas historizas
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGenerarExtracto"
                cm.CommandTimeout = 10

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmConcurso", pCabecera.idPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idCabeceraActual", pCabecera.idExtraccionesCAB)
                cm.Parameters("@idCabeceraActual").Direction = ParameterDirection.Input


                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.ExecuteNonQuery()

                msgRet = cm.Parameters("@msgRet").Value
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                GenerarExtracto = False
            End Try
        End Function

        Public Function GenerarExtractoCompleto(ByVal idPgmConcurso As Long) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim Transaccion As SqlTransaction = Nothing
            Dim msgRet As String = ""

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                '**** carga tablas historizas
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGenerarExtracto"
                cm.CommandTimeout = 10

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idCabeceraActual", System.DBNull.Value)
                cm.Parameters("@idCabeceraActual").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.ExecuteNonQuery()

                msgRet = cm.Parameters("@msgRet").Value
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                GenerarExtractoCompleto = False
            End Try
        End Function

        Public Function ObtenerFechasBolillas(ByVal pIdcabecera As Long, ByVal UltimaBolilla As Integer, ByRef FechaHoraPrimerBolilla As DateTime, ByRef fechaHoraUltimaBolilla As DateTime) As Boolean

            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Dim dr As SqlDataReader
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select coalesce(fechahora,'01/01/1999')as fechahora from extraccionesdet where orden=1 and idextraccionesCAB=" & pIdcabecera
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    FechaHoraPrimerBolilla = dr("fechahora")
                End If
                dr.Close()
                dr = Nothing
                If UltimaBolilla <> 0 Then 'tope fijo
                    vsql = " select coalesce(fechahora,'01/01/1999')as fechahora from extraccionesdet where  idextraccionesCAB=" & pIdcabecera & " and orden=" & UltimaBolilla
                Else 'tope variable,obtengo la fecha de la ultima extracción
                    vsql = " select top 1 coalesce(fechahora,'01/01/1999')as fechahora from extraccionesdet where  idextraccionesCAB=" & pIdcabecera & " order by orden desc"
                End If
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    fechaHoraUltimaBolilla = dr("fechahora")
                End If
                dr.Close()
                dr = Nothing
                ObtenerFechasBolillas = True
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                ObtenerFechasBolillas = False
            End Try

        End Function

        '***05/11/2012**
        Public Function GenerarExtractoJuegosDependientes(ByVal pIdpgmconcurso As Long, ByVal pidJuego As Long) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim Transaccion As SqlTransaction = Nothing
            Dim msgRet As String = ""

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                '**** carga tablas historizas
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGeneraExtractoJuegosDependientes"
                cm.CommandTimeout = 30

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmConcurso", pIdpgmconcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idJuego", pidJuego)
                cm.Parameters("@idJuego").Direction = ParameterDirection.Input


                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.ExecuteNonQuery()

                msgRet = cm.Parameters("@msgRet").Value
                If msgRet.Trim <> "" Then
                    Return False
                    Exit Function
                End If
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                GenerarExtractoJuegosDependientes = False
            End Try
        End Function

    End Class

End Namespace