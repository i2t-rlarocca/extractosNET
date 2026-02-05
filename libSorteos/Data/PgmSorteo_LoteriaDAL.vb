Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports libEntities.Entities
Imports Sorteos.Helpers
Imports Sorteos.Helpers.General
Namespace Data
    Public Class PgmSorteo_LoteriaDAL
        Private FechaHoravacia As DateTime = New DateTime(1999, 1, 1) ' para determinar si se cargo una fecha-hora

        Public Function getSorteosLoteria(ByVal pidPgmSorteo As Integer) As ListaOrdenada(Of pgmSorteo_loteria)
            Dim ls As New ListaOrdenada(Of pgmSorteo_loteria)
            Dim o As New pgmSorteo_loteria
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                '**27/11/2012 se agrega el nombre de la loteria solo para la visualizacion en la grilla de inicio
                vsql = "SELECT pl.* , l.nombre as nombreloteria, case when fechahoraconf is null then 'N' else 'S' end as Confirmada"
                vsql = vsql & " FROM "
                vsql = vsql & " PgmSorteo_Loteria pl"
                vsql = vsql & " INNER JOIN loteria l on pl.idloteria=l.idloteria"
                vsql = vsql & " WHERE idpgmsorteo = " & pidPgmSorteo
                vsql = vsql & " ORDER BY l.orden_extracto_qnl "
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                For Each r As DataRow In dt.Rows
                    o = New pgmSorteo_loteria
                    Load(o, r, True)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function Load(ByRef o As pgmSorteo_loteria, _
                                    ByRef dr As DataRow, _
                                    ByVal recuperarObjComponentes As Boolean) As Boolean

            Try
                Dim oE As New ExtraccionesLoteriaDAL
                Dim oL As New LoteriaDAL
                Dim ExtractoCargado As String
                o.Loteria = oL.getLoteria(dr("idLoteria"))
                o.IdPgmSorteo = dr("idPgmSorteo")
                o.FechaHoraLoteria = dr("FechaHoraLoteria")
                o.FechaHoraFinReal = Es_Nulo(Of Date)(dr("fechaHoraFinReal"), fechahoravacia)
                o.FechaHoraIniReal = Es_Nulo(Of Date)(dr("fechaHoraIniReal"), fechahoravacia)
                o.NroSorteoLoteria = dr("nrosorteoloteria")
                'hay que controlar si se tiene que carga desde archivo o no
                'si se carga de archivo,guarda el detalle en la bd por lo cual la carga de detalla no tiene diferencias

                o.Extractos_QNl = oE.getExtraccionesLoteria(dr("idpgmsorteo"), dr("idLoteria"))
                o.Extracto_Letras_Qnl = oE.getExtructurasLetras(dr("idPgmSorteo"), o.Loteria)

                If o.Extractos_QNl.idLoteria.Equals(Nothing) Then
                    If CargarDetalleDesdeArchivo(dr("idPgmSorteo"), o.Loteria) Then
                        'vuelvo a cargar
                        o.Extractos_QNl = oE.getExtraccionesLoteria(dr("idpgmsorteo"), dr("idLoteria"))
                        o.Extracto_Letras_Qnl = oE.getExtructurasLetras(dr("idPgmSorteo"), o.Loteria)
                    End If
                End If
                '*****
                o.RevertidaParcial = Es_Nulo(Of Integer)(dr("revertidaparcial"), 0)
                '**27/11/2012
                o.NombreLoteria = Es_Nulo(Of String)(dr("nombreLoteria"), "")
                '**08-03-13
                o.Fechaconfirmacion = Es_Nulo(Of Date)(dr("fechaHoraconf"), FechaHoravacia)
                o.Confirmada = IIf((Es_Nulo(Of String)(dr("confirmada"), "N")) = "N", False, True)

                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function CargarDetalleDesdeArchivo(ByVal pidPgmsorteo As Integer, ByVal poLoteria As Loteria) As Boolean
            'si existe un path de arhivo,se intenta carga desde archivo
            Dim NombreArchivo As String
            Try
                NombreArchivo = ""
                CargarDetalleDesdeArchivo = False
                If Len(poLoteria.path_extracto.Trim) > 0 Then
                    If Right(poLoteria.path_extracto.Trim, 1) <> "\" Then
                        NombreArchivo = poLoteria.path_extracto.Trim & "\"
                    Else
                        NombreArchivo = poLoteria.path_extracto.Trim
                    End If

                    NombreArchivo = NombreArchivo & "fquiniela-24930.txt"
                    CargarDetalleDesdeArchivo = CargaDetalleDesdeArchivo(NombreArchivo, pidPgmsorteo, poLoteria)

                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function CargaDetalleDesdeArchivo(ByVal path As String, ByVal pidpgmSorteo As Integer, ByVal oLoteria As Loteria) As Boolean
            Dim linea As String
            Dim valores As String
            Dim letras As String
            Dim letrasAux As String
            Try
                If System.IO.File.Exists(path) = False Then
                    CargaDetalleDesdeArchivo = False
                    Exit Function
                End If

                If path.Trim = "" Then
                    CargaDetalleDesdeArchivo = False
                    Exit Function
                End If

                Dim Archivo As IO.StreamReader = New IO.StreamReader(path)
                Dim ilinea As Integer
                Dim i As Integer
                ilinea = 1
                valores = ""
                letras = ""
                Select Case oLoteria.IdLoteria
                    Case "N" 'loteria nacional
                        If Not Archivo.EndOfStream Then
                            linea = Archivo.ReadToEnd
                            For i = 1 To 20
                                ilinea = linea.IndexOf(Right("00" & i, 2) & " ->")
                                valores = valores & Mid(linea, ilinea + 6, oLoteria.Cifras) & ","
                            Next
                            valores = Left(valores, Len(valores) - 1)
                            ilinea = linea.IndexOf("Letras: ")
                            letrasAux = Mid(linea, ilinea + 9, oLoteria.cant_letras_extracto)
                            For i = 1 To Len(letrasAux)
                                letras = letras & "'" & Mid(letrasAux, i, 1) & "',"
                            Next
                            letras = Left(letras, Len(letras) - 1)
                            'inserto en la base de datos
                            InsertarDatosdesdeArchivo(valores, pidpgmSorteo, oLoteria.IdLoteria, oLoteria.Cifras, letras)

                        End If
                        Archivo.Close()
                        CargaDetalleDesdeArchivo = True
                End Select

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try




        End Function
        Public Function InsertarDatosdesdeArchivo(ByVal pvalores As String, ByVal pidPgmSorteo As Integer, ByVal pidLoteria As Char, ByVal pCifras As Integer, Optional ByVal pletras As String = "") As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Dim resultado As Boolean
            Dim Letras() As String
            Dim InsertLetras As String
            Dim Transaccion As SqlTransaction = Nothing

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                resultado = False
                Transaccion = cm.Connection.BeginTransaction
                'borro los datos si existe y vuelvo a ingresar
                vsql = " delete from extracto_qnl where idpgmsorteo=" & pidPgmSorteo & " and idloteria='" & pidLoteria & "'"
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                cm.ExecuteNonQuery()

                vsql = " insert into extracto_qnl (idpgmsorteo,idloteria,loteria,cifras,Nro1T,Nro2T,Nro3T,Nro4T,Nro5T,Nro6T,Nro7T,Nro8T,Nro9T,Nro10T,Nro11T,Nro12T,Nro13T,Nro14T,Nro15T,Nro16T,Nro17T,Nro18T,Nro19T,Nro20T )values (" & pidPgmSorteo & ",'" & pidLoteria & "','" & pidLoteria & "'," & pCifras & ","
                vsql = vsql & pvalores & ")"

                cm.CommandText = vsql
                cm.ExecuteNonQuery()
                If Len(pletras) > 0 Then
                    vsql = " delete from extracto_qnl_letras where idpgmsorteo=" & pidPgmSorteo & " and idloteria='" & pidLoteria & "'"
                    cm.CommandText = vsql
                    cm.ExecuteNonQuery()

                    Letras = Split(pletras, ",")
                    For i = 0 To UBound(Letras)
                        InsertLetras = ""
                        InsertLetras = InsertLetras & "( " & pidPgmSorteo & ",'" & pidLoteria & "'," & i + 1 & "," & Letras(i) & ");"
                        InsertLetras = Left(InsertLetras, Len(InsertLetras) - 1)
                        vsql = " insert into extracto_qnl_letras (idpgmsorteo,idloteria,orden,letra) values "
                        vsql = vsql & InsertLetras
                        cm.CommandText = vsql
                        cm.ExecuteNonQuery()

                    Next


                End If

                Transaccion.Commit()
                resultado = True
                Return resultado
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function ExtraerDatosArchivo(ByVal pLinea As String, ByVal pInicio As Integer, ByVal pcantidad As Integer, Optional ByVal SonLetras As Boolean = False) As String
            Try
                Dim datos As String
                Dim i As Integer
                Dim letras As String
                datos = Mid(pLinea, pInicio, pcantidad)
                If SonLetras Then
                    letras = ""
                    For i = 1 To Len(datos)
                        letras = letras & Mid(datos, i, 1) & ","
                    Next
                    letras = Left(letras, Len(letras) - 1)
                    datos = letras
                End If
                Return datos
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function InsertarActualizarExtracto_QNL(ByVal pSorteoLoteria As pgmSorteo_loteria, Optional ByVal pModifica As Boolean = False) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dr2 As SqlDataReader
            Dim vsql As String
            Dim vsqlInsertUpdate As String
            Dim resultado As Boolean
            Dim Transaccion As SqlTransaction = Nothing
            Dim _valor As cPosicionValorLoterias

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                resultado = False
                Transaccion = cm.Connection.BeginTransaction
                vsqlInsertUpdate = ""
                If pModifica = False Or pModifica = True Then
                    vsql = "delete extracto_qnl where idpgmsorteo = " & pSorteoLoteria.IdPgmSorteo & " and idloteria = '" & pSorteoLoteria.Loteria.IdLoteria & "'"
                    cm.Transaction = Transaccion
                    cm.CommandText = vsql
                    dr2 = cm.ExecuteReader()
                    dr2.Close()
                    dr2 = Nothing
                    'se tiene que ingresar la los datos de cabecera y desps recorrer la coleccion de valores para actualizar los campos de valores
                    vsql = " insert into extracto_qnl(idpgmsorteo,idloteria,loteria,cifras,Nro1T,Nro2T,Nro3T,Nro4T,Nro5T,Nro6T,Nro7T,Nro8T,Nro9T,Nro10T,Nro11T,Nro12T,Nro13T,Nro14T,Nro15T,Nro16T,Nro17T,Nro18T,Nro19T,Nro20T) values (" & pSorteoLoteria.IdPgmSorteo & ",'" & pSorteoLoteria.Loteria.IdLoteria & "','" & pSorteoLoteria.Loteria.IdLoteriaDisplay & "'," & pSorteoLoteria.Loteria.CifrasIngresadaDesdeForm & ", "
                    '** Se actualizan los datos segun la coleccion de valores

                    For Each _valor In pSorteoLoteria.Extractos_QNl.Valores
                        vsqlInsertUpdate = vsqlInsertUpdate & "'" & _valor.Valor & "',"
                    Next
                    vsqlInsertUpdate = Left(vsqlInsertUpdate, Len(vsqlInsertUpdate) - 1)
                    vsqlInsertUpdate = vsqlInsertUpdate & ")"
                    vsql = vsql & vsqlInsertUpdate
                    cm.Transaction = Transaccion
                    cm.CommandText = vsql
                    dr = cm.ExecuteReader()
                    dr.Close()
                    dr = Nothing

                Else
                    'Se actualizan los datos segun la coleccion de valores
                    vsql = " UPDATE extracto_qnl SET "
                    For Each _valor In pSorteoLoteria.Extractos_QNl.Valores
                        vsqlInsertUpdate = vsqlInsertUpdate & "  Nro" & _valor.Posicion & "T='" & _valor.Valor & "',"
                    Next
                    vsqlInsertUpdate = Left(vsqlInsertUpdate, Len(vsqlInsertUpdate) - 1)
                    vsql = vsql & vsqlInsertUpdate
                    vsql = vsql & " where idpgmsorteo=" & pSorteoLoteria.IdPgmSorteo & " and idloteria='" & pSorteoLoteria.Loteria.IdLoteria & "'"

                    cm.CommandText = vsql
                    cm.Transaction = Transaccion
                    dr = cm.ExecuteReader()
                    dr.Close()
                    dr = Nothing
                End If
                Transaccion.Commit()
                resultado = True
                Return resultado
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                dr = Nothing
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function QuitarSorteoLoteria(ByVal pidLoteria As Char, ByVal pidpgmSorteo As Integer) As Boolean
            Dim dt As New DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String = ""
            Dim resultado As Boolean
            Dim Transaccion As SqlTransaction = Nothing

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                resultado = False
                Transaccion = cm.Connection.BeginTransaction
                'borro los datos del extracto
                vsql = " delete from extracto_qnl where idpgmsorteo=" & pidpgmSorteo & " and idloteria='" & pidLoteria & "'"
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                cm.ExecuteNonQuery()

                'borro los datos del extracto de letras
                vsql = " delete from extracto_qnl_letras where idpgmsorteo=" & pidpgmSorteo & " and idloteria='" & pidLoteria & "'"
                cm.CommandText = vsql
                cm.ExecuteNonQuery()

                'borro los datos del sorteo loteria
                vsql = " delete from pgmsorteo_loteria where idpgmsorteo=" & pidpgmSorteo & " and idloteria='" & pidLoteria & "'"
                cm.CommandText = vsql
                cm.ExecuteNonQuery()
                Transaccion.Commit()
                resultado = True
                Return resultado

            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function AgregarSorteoLoteria(ByVal pidLoteria As Char, ByVal pidpgmSorteo As Integer, Optional ByVal pNroSorteo As Integer = 0) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String = ""
            Dim resultado As Boolean
            Dim Transaccion As SqlTransaction = Nothing

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                resultado = False
                Transaccion = cm.Connection.BeginTransaction
                'inserto los datos del sorteo loteria
                vsql = " INSERT INTO pgmsorteo_loteria (idpgmsorteo,idloteria,nrosorteoloteria,fechahoraloteria) values(" & pidpgmSorteo & ",'" & pidLoteria & "'," & pNroSorteo & ",@fecha)"
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fecha", Now)
                cm.ExecuteNonQuery()
                Transaccion.Commit()
                resultado = True
                Return resultado

            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function Confirmar(ByVal pidPgmSorteo As Integer, ByVal pIdLoteria As Char, ByVal HoraLoteria As DateTime, ByVal HoraIniExtraccion As DateTime, ByVal HoraFinExtraccion As DateTime, Optional ByVal opc As String = "CON") As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim idExtraccionesCabSig As Integer = -1
            Dim msgRet As String = ""

            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspConfirmarExtraccionLoterias"

                ' cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                ' cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmSorteo", pidPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idloteria", pIdLoteria)
                cm.Parameters("@idloteria").Direction = ParameterDirection.Input

                '@HoraLoteria
                cm.Parameters.Add(New SqlParameter("@Horaloteria", SqlDbType.DateTime))
                cm.Parameters("@Horaloteria").Direction = ParameterDirection.Input
                cm.Parameters("@Horaloteria").Value = HoraLoteria

                '@HoraIniReal
                cm.Parameters.Add(New SqlParameter("@HoraIniReal", SqlDbType.DateTime))
                cm.Parameters("@HoraIniReal").Direction = ParameterDirection.Input
                cm.Parameters("@HoraIniReal").Value = HoraIniExtraccion

                '@HoraFinReal
                cm.Parameters.Add(New SqlParameter("@HoraFinReal", SqlDbType.DateTime))
                cm.Parameters("@HoraFinReal").Direction = ParameterDirection.Input
                cm.Parameters("@HoraFinReal").Value = HoraFinExtraccion

                '@opc ("ACT"->SOLO ACTUALIZA LAS FECHAS, "CON"->CONFIRMA
                cm.Parameters.Add(New SqlParameter("@opc", SqlDbType.VarChar))
                cm.Parameters("@opc").Direction = ParameterDirection.Input
                cm.Parameters("@opc").Value = opc

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.ExecuteNonQuery()

                ' idExtraccionesCabSig = IIf(cm.Parameters("@idExtraccionesCabSig").SqlValue.isnull, -1, cm.Parameters("@idExtraccionesCabSig").Value)
                ' horaFinConcurso = IIf(cm.Parameters("@HoraFinConcurso").SqlValue.isnull, Nothing, cm.Parameters("@HoraFinConcurso").Value)
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
        Public Function Revertir(ByVal pidPgmSorteo As Integer, ByVal pIdLoteria As Char, ByVal pidModalidad As Integer) As Boolean

            Dim cm As SqlCommand = New SqlCommand


            Dim idExtraccionesCabSig As Integer = -1
            Dim msgRet As String = ""

            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspRevertirExtraccionLoterias"

                cm.Parameters.AddWithValue("@idPgmSorteo", pidPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idloteria", pIdLoteria)
                cm.Parameters("@idloteria").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@BorraDetalle", pidModalidad)
                cm.Parameters("@BorraDetalle").Direction = ParameterDirection.Input

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

        Public Function InsertarLetras_QNL(ByVal pSorteoLoteria As pgmSorteo_loteria, Optional ByVal pModifica As Boolean = False) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim vsql As String
            Dim vsqlInsertUpdate As String
            Dim resultado As Boolean
            Dim Transaccion As SqlTransaction = Nothing
            Dim _valor As extracto_qnl_letras

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                resultado = False
                Transaccion = cm.Connection.BeginTransaction
                vsqlInsertUpdate = ""
                vsql = ""
                'borro los datos del extracto de letras
                vsql = " delete from extracto_qnl_letras where idpgmsorteo=" & pSorteoLoteria.IdPgmSorteo & " and idloteria='" & pSorteoLoteria.Loteria.IdLoteria & "'"
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                cm.ExecuteNonQuery()
                vsql = ""

                '** Se actualizan los datos segun la coleccion de valores
                'vsqlInsertUpdate = "INSERT INTO extracto_qnl_letras values("
                For Each _valor In pSorteoLoteria.Extracto_Letras_Qnl
                    vsqlInsertUpdate = "INSERT INTO extracto_qnl_letras values("
                    vsqlInsertUpdate = vsqlInsertUpdate & _valor.idPgmSorteo & ",'" & _valor.idLoteria & "'," & _valor.Orden & ",'" & _valor.letra & "')"
                    cm.CommandText = vsqlInsertUpdate
                    dr = cm.ExecuteReader()
                    dr.Close()
                    dr = Nothing
                Next
                'vsqlInsertUpdate = Left(vsqlInsertUpdate, Len(vsqlInsertUpdate) - 1)
                'vsqlInsertUpdate = vsqlInsertUpdate & ")"
                'vsql = vsqlInsertUpdate
                ' cm.Transaction = Transaccion
                'cm.CommandText = vsql
                'dr = cm.ExecuteReader()
                'dr.Close()
                'dr = Nothing
                Transaccion.Commit()
                resultado = True
                Return resultado
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                    dr = Nothing
                End If
                Return resultado
                Throw New Exception(ex.Message)
            End Try
        End Function
        '**27/11/2012
        Public Function ActualizaNroSorteoLoteria(ByVal pidLoteria As Char, ByVal pidpgmSorteo As Integer, ByVal pNroSorteo As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String = ""
            Dim resultado As Boolean
            Dim Transaccion As SqlTransaction = Nothing

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                resultado = False
                Transaccion = cm.Connection.BeginTransaction
                'inserto los datos del sorteo loteria
                vsql = " UPDATE pgmsorteo_loteria set nrosorteoloteria=" & pNroSorteo & " where idpgmsorteo=" & pidpgmSorteo & " and idloteria='" & pidLoteria & "'"
                cm.Transaction = Transaccion
                cm.CommandText = vsql
                cm.ExecuteNonQuery()
                Transaccion.Commit()
                resultado = True
                Return resultado

            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function ActualizaFecIniFinLoteria(ByVal pidpgmSorteo As Integer, ByVal pidLoteria As Char, ByVal HoraLoteria As DateTime, ByVal HoraIniExtraccion As DateTime, ByVal HoraFinExtraccion As DateTime) As Boolean
            Me.Confirmar(pidpgmSorteo, pidLoteria, HoraLoteria, HoraIniExtraccion, HoraFinExtraccion, "ACT")
        End Function

    End Class
End Namespace