Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports Sorteos.Bussiness
Imports libEntities.Entities
Imports Sorteos.Helpers
Imports Sorteos.Extractos


Namespace Data
    Public Class ArchivoBoldtDAL


        Public Function ObtenerCodigoLoteriaExtracto(ByVal idloteria As Long) As String
            Dim _loteria As String = ""
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable
            Try
                ObtenerCodigoLoteriaExtracto = ""
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "SELECT coalesce(idloteria,'') as idloteria FROM loteria where idloteriaBoldt=" & idloteria
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    _loteria = dr("idloteria")
                End If
                Try
                    dr.Close()
                    dr = Nothing
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm = Nothing
                Catch ex1 As Exception
                End Try
                Return _loteria.Trim
            Catch ex As Exception
                Try
                    dr.Close()
                    dr = Nothing
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm = Nothing
                Catch ex1 As Exception
                End Try

                ObtenerCodigoLoteriaExtracto = ""
            End Try
        End Function

        Public Function ObtenerCodigoJuegoBoldt(ByVal idJuego As Long) As String
            Dim _juego As String = ""
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable
            Try
                ObtenerCodigoJuegoBoldt = ""
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "SELECT COALESCE(jue_nombarchivoboldt,'') AS  nombreArchivo FROM JUEGO where idjuego=" & idJuego
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    _juego = dr("nombreArchivo")
                End If
                dr.Close()
                _juego.PadLeft(3, "0")
                Return _juego
            Catch ex As Exception
                ObtenerCodigoJuegoBoldt = ""
            End Try
        End Function

        Public Function ObtenerLoteriasParaArchivoBoldt(ByVal idpgmsorteo As Long) As DataTable
            Dim _juego As String = ""
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "SELECT * FROM archBoldt02_v where idpgmsorteo=" & idpgmsorteo
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                Return dt
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Sub LimpiarDifCuadraturaExtracto(ByRef oSorteo As PgmSorteo)
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "DELETE PgmSorteo_auddif WHERE idpgmsorteo = " & oSorteo.idPgmSorteo

                cm.CommandText = vsql
                cm.ExecuteNonQuery()
                Try
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm = Nothing
                Catch ex1 As Exception
                End Try
            Catch ex As Exception
                Try
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm = Nothing
                Catch ex1 As Exception
                End Try
            End Try

        End Sub

        Public Sub auditarDifCuadraturaExtracto(ByRef oSorteo As PgmSorteo, ByVal aud_detalle As String)
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "INSERT INTO PgmSorteo_auddif (idpgmsorteo, fechahora, detalle) VALUES(" & oSorteo.idPgmSorteo & ", '" & _
                        FormatDateTime(Now(), DateFormat.ShortDate) & " " & Now().Hour & ":" & Now().Minute & ":" & Now().Second & "', '" & aud_detalle & "')"

                cm.CommandText = vsql
                cm.ExecuteNonQuery()
                Try
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm = Nothing
                Catch ex1 As Exception
                End Try
            Catch ex As Exception
                Try
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm = Nothing
                Catch ex1 As Exception
                End Try
            End Try

        End Sub


        Public Function ObtenerNombreArchExtractoInterJ(ByVal idpgmSorteo As Long, ByVal idLoteria As String, ByRef archivo As String, ByRef msgRet As String) As Boolean
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable

            Try
                archivo = ""
                msgRet = ""
                cm.Connection = General.Obtener_Conexion
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetArchExtUnif"
                cm.CommandTimeout = 120

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idpgmSorteo", idpgmSorteo)
                cm.Parameters("@idpgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idLoteria", idLoteria)
                cm.Parameters("@idLoteria").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@archivo", SqlDbType.VarChar, 255))
                cm.Parameters("@archivo").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "OK" Then
                    Throw New Exception(msgRet)
                    Return False
                End If
                archivo = cm.Parameters("@archivo").Value
                Return True

            Catch ex As Exception
                Try
                    dr.Close()
                    dr = Nothing
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm = Nothing
                Catch ex1 As Exception
                End Try
                Return False
            End Try
        End Function

        Public Function ObtenerExtractoInterJ(ByVal idpgmSorteo As Integer, ByVal idLoteria As String, ByRef archivo As String, ByRef msgRet As String) As DataTable
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable

            Try
                archivo = ""
                msgRet = ""
                cm.Connection = General.Obtener_Conexion
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetExtUnif"
                cm.CommandTimeout = 120

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idpgmSorteo", idpgmSorteo)
                cm.Parameters("@idpgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idLoteria", idLoteria)
                cm.Parameters("@idLoteria").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@archivo", SqlDbType.VarChar, 255))
                cm.Parameters("@archivo").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "OK" Then
                    Throw New Exception(msgRet)
                    Return Nothing
                End If
                archivo = cm.Parameters("@archivo").Value
                Return dt

            Catch ex As Exception
                Try
                    dr.Close()
                    dr = Nothing
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm = Nothing
                Catch ex1 As Exception
                End Try
                Return Nothing
            End Try
        End Function

        Public Function InsertaCabeceraExtracto_Auditoria(ByVal Archivo As String, ByVal valor As Boolean, ByVal extracto As cExtractoArchivoBoldt) As Boolean
            Dim _loteria As String = ""
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim _idpgmconcurso As String = ""
            Dim total As Integer = 0
            Try

                InsertaCabeceraExtracto_Auditoria = False
                _idpgmconcurso = extracto.idJuego & extracto.NumeroSorteo.ToString.PadLeft(6, "0")
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "select count(*) as total from extracto_aud_cab where idpgmconcurso=" & _idpgmconcurso
                cm.CommandText = vsql
                total = cm.ExecuteScalar
                If total = 0 Then
                    vsql = "insert into extracto_aud_cab(idpgmconcurso,fechahora,archivo,resultado) values (" & _idpgmconcurso & ",@fechahora,'" & Archivo & "'," & IIf((valor = True), 1, 0) & ")"
                Else
                    vsql = "update extracto_aud_cab set fechahora=@fechahora,archivo='" & Archivo & "',resultado=" & IIf((valor = True), 1, 0) & " where idpgmconcurso=" & _idpgmconcurso
                End If
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@fechahora", Now)
                cm.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception("InsertaCabeceraExtracto_Auditoria:" & ex.Message)
                Return False
            End Try
        End Function
        Public Function InsertaDetalleExtracto_Auditoria(ByVal extracto As cExtractoArchivoBoldt) As Boolean
            Dim _loteria As String = ""
            Dim vsql As String
            Dim cmd As SqlCommand = New SqlCommand
            Dim idpgmconcurso As String = ""
            Dim Transaccion As SqlTransaction = Nothing
            Dim i As Integer = 0
            Dim valor As Long = 0
            Dim Valores As New List(Of String)
            Dim posicion As Integer = 0
            Dim nroFila As Long = 0
            Try
                InsertaDetalleExtracto_Auditoria = False
                idpgmconcurso = extracto.idJuego & extracto.NumeroSorteo.ToString.PadLeft(6, "0")
                cmd.Connection = General.Obtener_Conexion
                cmd.CommandType = CommandType.Text
                'asignacion de los valores
                Valores.Add(extracto.Numero_1.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_2.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_3.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_4.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_5.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_6.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_7.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_8.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_9.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_10.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_11.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_12.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_13.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_14.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_15.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_16.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_17.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_18.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_19.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_20.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_21.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_22.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_23.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_24.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_25.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_26.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_27.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_28.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_29.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_30.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_31.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_32.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_33.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_34.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_35.Valor.ToString.PadLeft(2, "00"))
                Valores.Add(extracto.Numero_36.Valor.ToString.PadLeft(2, "00"))
                '*** controla si ya existe o no
                Dim total As Integer
                vsql = "select count(*) as total from extracto_aud_det where idpgmconcurso=" & idpgmconcurso
                cmd.CommandText = vsql
                total = cmd.ExecuteScalar

                Transaccion = cmd.Connection.BeginTransaction
                Select Case extracto.idJuego


                    Case 4 '** Quini 6
                        nroFila = 1
                        'tradicional
                        posicion = 1
                        For i = 0 To 5
                            If total = 0 Then
                                vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",1,'" & Valores(i) & "'," & posicion & ")"
                            Else
                                vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                            End If
                            cmd.Transaction = Transaccion
                            cmd.CommandText = vsql
                            cmd.ExecuteNonQuery()
                            nroFila = nroFila + 1
                            posicion = posicion + 1
                        Next
                        posicion = 1
                        'la segunda
                        For i = 6 To 11
                            If total = 0 Then
                                vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",2,'" & Valores(i) & "'," & posicion & ")"
                            Else
                                vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                            End If
                            cmd.Transaction = Transaccion
                            cmd.CommandText = vsql
                            cmd.ExecuteNonQuery()
                            posicion = posicion + 1
                            nroFila = nroFila + 1
                        Next
                        posicion = 1
                        'revancha
                        For i = 12 To 17
                            If total = 0 Then
                                vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",3,'" & Valores(i) & "'," & posicion & ")"
                            Else
                                vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                            End If
                            cmd.Transaction = Transaccion
                            cmd.CommandText = vsql
                            cmd.ExecuteNonQuery()
                            posicion = posicion + 1
                            nroFila = nroFila + 1
                        Next
                        posicion = 1
                        'siempre sale
                        For i = 18 To 23
                            If total = 0 Then
                                vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",7,'" & Valores(i) & "'," & posicion & ")"
                            Else
                                vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                            End If
                            cmd.Transaction = Transaccion
                            cmd.CommandText = vsql
                            cmd.ExecuteNonQuery()
                            posicion = posicion + 1
                            nroFila = nroFila + 1
                        Next

                        'adicionales
                        If Valores(25) <> 0 Or Valores(25) <> 0 Or Valores(26) <> 0 Or Valores(27) <> 0 Or Valores(28) <> 0 Or Valores(29) <> 0 Then
                            posicion = 1
                            For i = 24 To 29
                                If total = 0 Then
                                    vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",5,'" & Valores(i) & "'," & posicion & ")"
                                Else
                                    vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                                End If
                                cmd.Transaction = Transaccion
                                cmd.CommandText = vsql
                                cmd.ExecuteNonQuery()
                                posicion = posicion + 1
                                nroFila = nroFila + 1
                            Next
                        End If
                        If Valores(30) <> 0 Or Valores(31) <> 0 Or Valores(32) <> 0 Or Valores(33) <> 0 Or Valores(34) <> 0 Or Valores(35) <> 0 Then
                            posicion = 1
                            For i = 30 To 35
                                If total = 0 Then
                                    vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",5,'" & Valores(i) & "'," & posicion & ")"
                                Else
                                    vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                                End If
                                cmd.Transaction = Transaccion
                                cmd.CommandText = vsql
                                cmd.ExecuteNonQuery()
                                posicion = posicion + 1
                                nroFila = nroFila + 1
                            Next
                        End If
                    Case 13 '** Brinco
                        'tradicional
                        nroFila = 1
                        posicion = 1
                        For i = 0 To 5
                            If total = 0 Then
                                vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",1,'" & Valores(i) & "'," & posicion & ")"
                            Else
                                vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                            End If
                            cmd.Transaction = Transaccion
                            cmd.CommandText = vsql
                            cmd.ExecuteNonQuery()
                            posicion = posicion + 1
                            nroFila = nroFila + 1
                        Next
                        'adicionales
                        If Valores(24) <> 0 Or Valores(25) <> 0 Or Valores(26) <> 0 Or Valores(27) <> 0 Or Valores(28) <> 0 Or Valores(29) <> 0 Then
                            posicion = 1
                            For i = 24 To 29
                                If total = 0 Then
                                    vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",5,'" & Valores(i) & "'," & posicion & ")"
                                Else
                                    vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                                End If
                                cmd.Transaction = Transaccion
                                cmd.CommandText = vsql
                                cmd.ExecuteNonQuery()
                                posicion = posicion + 1
                                nroFila = nroFila + 1
                            Next
                        End If
                        If Valores(30) <> 0 Or Valores(31) <> 0 Or Valores(32) <> 0 Or Valores(33) <> 0 Or Valores(34) <> 0 Or Valores(35) <> 0 Then
                            posicion = 1
                            For i = 30 To 35
                                If total = 0 Then
                                    vsql = "insert into extracto_aud_det (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto) values (" & nroFila & "," & idpgmconcurso & ",5,'" & Valores(i) & "'," & posicion & ")"
                                Else
                                    vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                                End If
                                cmd.Transaction = Transaccion
                                cmd.CommandText = vsql
                                cmd.ExecuteNonQuery()
                                posicion = posicion + 1
                                nroFila = nroFila + 1
                            Next
                        End If
                    Case 30 '*** Poceada Federal
                        nroFila = 1
                        posicion = 1
                        For i = 0 To 20
                            If total = 0 Then
                                vsql = "insert into extracto_aud_det values (idfila,idpgmconcurso,idmodalidad,valorboldt,posicionenextracto)(" & nroFila & "," & idpgmconcurso & ",1,'" & Valores(i) & "'," & posicion & ")"
                            Else
                                vsql = "update extracto_aud_det  set valorboldt='" & Valores(i) & "',posicionenextracto=" & posicion & " where idfila=" & nroFila & " and idpgmconcurso=" & idpgmconcurso
                            End If
                            cmd.Transaction = Transaccion
                            cmd.CommandText = vsql
                            cmd.ExecuteNonQuery()
                            posicion = posicion + 1
                            nroFila = nroFila + 1
                        Next
                End Select

                Transaccion.Commit()
                Try
                    cmd.Connection.Close()
                    cmd.Connection = Nothing
                    cmd = Nothing
                Catch ex1 As Exception
                End Try
                Return True
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Try
                    cmd.Connection.Close()
                    cmd.Connection = Nothing
                    cmd = Nothing
                Catch ex1 As Exception
                End Try
                Throw New Exception("InsertaCabeceraExtracto_Auditoria:" & ex.Message)
                Return False
            End Try
        End Function

        Public Function ActualizaDetalleExtracto_Auditoria(ByVal idpgmconcurso As Long) As Boolean
            Dim _loteria As String = ""
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim Transaccion As SqlTransaction = Nothing
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim idfila As Integer = 0
            Dim valor As String = ""
            Try
                ActualizaDetalleExtracto_Auditoria = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "select idpgmconcurso,idextraccionesdet,valor from extraccionesdet a"
                vsql = vsql & " inner join extraccionescab b on a.idextraccionescab=b.idextraccionescab"
                vsql = vsql & " where b.idpgmconcurso=" & idpgmconcurso
                vsql = vsql & " order by b.idextraccionescab ,posicionenextracto"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                idfila = 1
                Transaccion = cm.Connection.BeginTransaction
                For Each r As DataRow In dt.Rows
                    valor = r("valor").ToString.PadLeft(2, "0")
                    vsql = "update extracto_aud_det set idextraccionesdet=" & r("idextraccionesdet") & ",valor='" & valor & "' where idpgmconcurso=" & r("idpgmconcurso") & " and idfila=" & idfila
                    cm.Transaction = Transaccion
                    cm.CommandText = vsql
                    cm.ExecuteNonQuery()
                    idfila = idfila + 1
                Next
                Transaccion.Commit()
                Return True
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function ControlarDetalleExtracto_Auditoria(ByVal idpgmconcurso As Long) As Boolean
            Dim _loteria As String = ""
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim Transaccion As SqlTransaction = Nothing
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                ControlarDetalleExtracto_Auditoria = True
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "select idpgmconcurso,idfila,coalesce(valor,0)valor ,coalesce(valorboldt,0)valorboldt from extracto_aud_det "
                vsql = vsql & " where idpgmconcurso=" & idpgmconcurso
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                Transaccion = cm.Connection.BeginTransaction
                For Each r As DataRow In dt.Rows
                    If r("valor") <> r("valorboldt") Then
                        ControlarDetalleExtracto_Auditoria = False
                        vsql = "update extracto_aud_det set resultado=0  where idpgmconcurso=" & r("idpgmconcurso") & " and idfila=" & r("idfila")
                        cm.Transaction = Transaccion
                        cm.CommandText = vsql
                        cm.ExecuteNonQuery()
                    Else
                        vsql = "update extracto_aud_det set resultado=1  where idpgmconcurso=" & r("idpgmconcurso") & " and idfila=" & r("idfila")
                        cm.Transaction = Transaccion
                        cm.CommandText = vsql
                        cm.ExecuteNonQuery()
                    End If
                Next
                Transaccion.Commit()
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function Actualiza_ExtraccionesDet_con_ExtractoBoldt(ByVal idpgmconcurso As Long) As Boolean
            Dim _loteria As String = ""
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim Transaccion As SqlTransaction = Nothing
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim idfila As Integer = 0
            Try
                Actualiza_ExtraccionesDet_con_ExtractoBoldt = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "select coalesce(idextraccionesdet,0)idextraccionesdet ,coalesce(valorBoldt,0)valorBoldt from extracto_aud_det "
                vsql = vsql & " where idpgmconcurso=" & idpgmconcurso

                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                Transaccion = cm.Connection.BeginTransaction
                For Each r As DataRow In dt.Rows
                    vsql = "update  extraccionesdet set  valor='" & r("valorBoldt") & "'  where idextraccionesdet=" & r("idextraccionesdet")
                    cm.Transaction = Transaccion
                    cm.CommandText = vsql
                    cm.ExecuteNonQuery()
                Next
                Transaccion.Commit()
                Return True
            Catch ex As Exception
                If Not Transaccion Is Nothing Then
                    Transaccion.Rollback()
                End If
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function Tiene_que_generar_archivoextracto(ByVal idjuego As Long) As Boolean
            Try
                Dim vsql As String
                Dim valor As Boolean = False
                Dim cm As SqlCommand = New SqlCommand
                Dim dr As SqlDataReader
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "select coalesce(generaarchivoboldt,0) as genera from juego where idjuego = " & idjuego
                cm.CommandText = vsql
                dr = cm.ExecuteReader
                If dr.HasRows Then
                    dr.Read()
                    If dr("genera") <> 0 Then
                        valor = True
                    End If
                End If
                dr.Close()
                Return valor
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function existe_auditoria_archivoextracto(ByVal idpgmsorteo As Long) As Boolean
            Dim _loteria As String = ""
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim _idpgmconcurso As String = ""
            Dim total As Integer = 0
            Try

                existe_auditoria_archivoextracto = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = "select count(*) as total from extracto_aud_cab where idpgmconcurso=" & idpgmsorteo
                cm.CommandText = vsql
                total = cm.ExecuteScalar
                If total <> 0 Then
                    existe_auditoria_archivoextracto = True
                End If
            Catch ex As Exception
                Throw New Exception("InsertaCabeceraExtracto_Auditoria:" & ex.Message)
                Return False
            End Try
        End Function
    End Class
End Namespace