Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports Sorteos.Bussiness
Imports libEntities.Entities
Imports Sorteos.Helpers
Imports Sorteos.Extractos


Namespace Data

    Public Class PgmSorteoDAL

        Public Function publicarDisplay(ByVal idPgmConcurso As Integer, Optional ByVal idPgmSorteo As Integer = -1, Optional ByVal forzarOffline As Boolean = False) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim msgRet As String = ""
            Dim strForzarOffline As String = ""

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            If forzarOffline Then
                strForzarOffline = "S"
            Else
                strForzarOffline = "N"
            End If
            Try
                '**** carga tablas historizas
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspActualizarDisplay"
                cm.CommandTimeout = 120

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                If idPgmSorteo > 1 Then
                    cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)
                    cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input
                End If

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.Parameters.AddWithValue("@forzarOffline", strForzarOffline)
                cm.Parameters("@forzarOffline").Direction = ParameterDirection.Input

                cm.ExecuteNonQuery()

                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return False
                End If

            Catch ex As Exception
                Throw New Exception(" publicarDisplay: " & ex.Message)
                Return False
            End Try

            Return True
        End Function

        Public Function getExtraccciones(ByVal idJuego As Integer, ByVal idPgmSorteo As Integer, ByVal idLoteria As String, ByRef oExtracto As WSExtractos.Extracto) As Boolean
            Dim sSQL As String
            Dim sTabla As String = ""

            Dim cn As New SqlConnection(General.ConnString)
            Dim miCm As New SqlCommand
            Dim dr1 As SqlDataReader
            Dim draux As SqlDataReader
            miCm.Connection = cn
            Dim dt As New DataTable
            Dim r As DataRow
            Dim fecha As String
            Try

                miCm.Connection = General.Obtener_Conexion
                miCm.CommandType = CommandType.Text
                miCm.CommandTimeout = 60

                '** para otras jurisdicciones se tiene que controlar solo si estan confirmadas
                'If (idJuego = 2 Or idJuego = 3 Or idJuego = 8 Or idJuego = 49) And idLoteria <> General.Jurisdiccion Then
                '    sSQL = " select coalesce(fechahoraconf,'') as fecha from pgmsorteo_loteria where idloteria='" & idLoteria & "' and idpgmsorteo=" & idPgmSorteo
                '    miCm.CommandText = sSQL
                '    draux = miCm.ExecuteReader
                '    If draux.HasRows Then
                '        draux.Read()
                '        fecha = draux("fecha").ToString.Trim
                '        If InStr(fecha, "1900") <> 0 Then
                '            draux.Close()
                '            getExtraccciones = True
                '            Exit Function
                '        End If
                '    End If
                '    draux.Close()
                'End If

                ' ************ definición de la tabla de extractos ******************
                Select Case idJuego
                    Case 1, 30
                        ' 1	Tómbola
                        ' 30	Poceada Federal
                        sTabla = " extracto_tom "

                    Case 2, 3, 8, 49, 50, 51, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 50 Lotería Tradic.
                        ' 51 Lotería Chica
                        sTabla = " extracto_qnl "

                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        sTabla = " extracto_qn6 "
                End Select


                ' ************ obtención de datos ******************
                sSQL = " select e.* from PgmSorteo p " & _
                      " inner join " & sTabla & " e on p.IdPgmSorteo = e.IdPgmSorteo " & _
                      " where p.IdPgmSorteo = @IdPgmSorteo and p.idJuego = @idJuego and e.idLoteria = @idLoteria "
                miCm.CommandText = sSQL
                miCm.Parameters.Clear()
                miCm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                miCm.Parameters.AddWithValue("@idJuego", idJuego)
                miCm.Parameters.AddWithValue("@idLoteria", idLoteria)

                dr1 = miCm.ExecuteReader()
                dt.Load(dr1)
                dr1.Close()
                'dt.Load(miCm.ExecuteReader)
                miCm.Parameters.Clear()
                miCm.Connection.Close()
                miCm.Connection = Nothing
                miCm.Dispose()
                If cn.State <> ConnectionState.Closed Then cn.Close()
                cn = Nothing
                SqlConnection.ClearAllPools()
                'dr1.Close()
                r = dt.Rows(0)

                oExtracto.Loteria = r("idLoteria")

                Select Case idJuego
                    Case 1, 30 ' 1	Tómbola
                        ' 30	Poceada Federal
                        oExtracto.Cifras = 2
                        If idJuego = 1 Then
                            oExtracto.Juego = "TM"
                        Else
                            oExtracto.Juego = "PF"
                        End If

                        oExtracto.Numero_1.Valor = IIf(r("nro_tom_poc_1").ToString.Trim.Length = 0, "0", r("nro_tom_poc_1"))
                        oExtracto.Numero_2.Valor = IIf(r("nro_tom_poc_2").ToString.Trim.Length = 0, "0", r("nro_tom_poc_2"))
                        oExtracto.Numero_3.Valor = IIf(r("nro_tom_poc_3").ToString.Trim.Length = 0, "0", r("nro_tom_poc_3"))
                        oExtracto.Numero_4.Valor = IIf(r("nro_tom_poc_4").ToString.Trim.Length = 0, "0", r("nro_tom_poc_4"))
                        oExtracto.Numero_5.Valor = IIf(r("nro_tom_poc_5").ToString.Trim.Length = 0, "0", r("nro_tom_poc_5"))
                        oExtracto.Numero_6.Valor = IIf(r("nro_tom_poc_6").ToString.Trim.Length = 0, "0", r("nro_tom_poc_6"))
                        oExtracto.Numero_7.Valor = IIf(r("nro_tom_poc_7").ToString.Trim.Length = 0, "0", r("nro_tom_poc_7"))
                        oExtracto.Numero_8.Valor = IIf(r("nro_tom_poc_8").ToString.Trim.Length = 0, "0", r("nro_tom_poc_8"))
                        oExtracto.Numero_9.Valor = IIf(r("nro_tom_poc_9").ToString.Trim.Length = 0, "0", r("nro_tom_poc_9"))
                        oExtracto.Numero_10.Valor = IIf(r("nro_tom_poc_10").ToString.Trim.Length = 0, "0", r("nro_tom_poc_10"))
                        oExtracto.Numero_11.Valor = IIf(r("nro_tom_poc_11").ToString.Trim.Length = 0, "0", r("nro_tom_poc_11"))
                        oExtracto.Numero_12.Valor = IIf(r("nro_tom_poc_12").ToString.Trim.Length = 0, "0", r("nro_tom_poc_12"))
                        oExtracto.Numero_13.Valor = IIf(r("nro_tom_poc_13").ToString.Trim.Length = 0, "0", r("nro_tom_poc_13"))
                        oExtracto.Numero_14.Valor = IIf(r("nro_tom_poc_14").ToString.Trim.Length = 0, "0", r("nro_tom_poc_14"))
                        oExtracto.Numero_15.Valor = IIf(r("nro_tom_poc_15").ToString.Trim.Length = 0, "0", r("nro_tom_poc_15"))
                        oExtracto.Numero_16.Valor = IIf(r("nro_tom_poc_16").ToString.Trim.Length = 0, "0", r("nro_tom_poc_16"))
                        oExtracto.Numero_17.Valor = IIf(r("nro_tom_poc_17").ToString.Trim.Length = 0, "0", r("nro_tom_poc_17"))
                        oExtracto.Numero_18.Valor = IIf(r("nro_tom_poc_18").ToString.Trim.Length = 0, "0", r("nro_tom_poc_18"))
                        oExtracto.Numero_19.Valor = IIf(r("nro_tom_poc_19").ToString.Trim.Length = 0, "0", r("nro_tom_poc_19"))
                        oExtracto.Numero_20.Valor = IIf(r("nro_tom_poc_20").ToString.Trim.Length = 0, "0", r("nro_tom_poc_20"))
                        oExtracto.Numero_21.Valor = 0
                        oExtracto.Numero_22.Valor = 0
                        oExtracto.Numero_23.Valor = 0
                        oExtracto.Numero_24.Valor = 0
                        oExtracto.Numero_25.Valor = 0
                        oExtracto.Numero_26.Valor = 0
                        oExtracto.Numero_27.Valor = 0
                        oExtracto.Numero_28.Valor = 0
                        oExtracto.Numero_29.Valor = 0
                        oExtracto.Numero_30.Valor = 0
                        oExtracto.Numero_31.Valor = 0
                        oExtracto.Numero_32.Valor = 0
                        oExtracto.Numero_33.Valor = 0
                        oExtracto.Numero_34.Valor = 0
                        oExtracto.Numero_35.Valor = 0
                        oExtracto.Numero_36.Valor = 0

                        oExtracto.Letra_1.Valor = ""
                        oExtracto.Letra_2.Valor = ""
                        oExtracto.Letra_3.Valor = ""
                        oExtracto.Letra_4.Valor = ""

                    Case 2, 3, 8, 49, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 26 El Ultimo
                        oExtracto.Cifras = r("cifras")
                        Select Case idJuego
                            Case 2
                                oExtracto.Juego = "N"
                            Case 3
                                oExtracto.Juego = "V"
                            Case 8
                                oExtracto.Juego = "M"
                            Case 49
                                oExtracto.Juego = "P"
                            Case 26
                                oExtracto.Juego = "U"
                            Case Else
                                oExtracto.Juego = "X"
                        End Select

                        oExtracto.Numero_1.Valor = IIf(r("Nro1T").ToString.Trim.Length = 0, "0", r("Nro1T"))
                        oExtracto.Numero_2.Valor = IIf(r("Nro2T").ToString.Trim.Length = 0, "0", r("Nro2T"))
                        oExtracto.Numero_3.Valor = IIf(r("Nro3T").ToString.Trim.Length = 0, "0", r("Nro3T"))
                        oExtracto.Numero_4.Valor = IIf(r("Nro4T").ToString.Trim.Length = 0, "0", r("Nro4T"))
                        oExtracto.Numero_5.Valor = IIf(r("Nro5T").ToString.Trim.Length = 0, "0", r("Nro5T"))
                        oExtracto.Numero_6.Valor = IIf(r("Nro6T").ToString.Trim.Length = 0, "0", r("Nro6T"))
                        oExtracto.Numero_7.Valor = IIf(r("Nro7T").ToString.Trim.Length = 0, "0", r("Nro7T"))
                        oExtracto.Numero_8.Valor = IIf(r("Nro8T").ToString.Trim.Length = 0, "0", r("Nro8T"))
                        oExtracto.Numero_9.Valor = IIf(r("Nro9T").ToString.Trim.Length = 0, "0", r("Nro9T"))
                        oExtracto.Numero_10.Valor = IIf(r("Nro10T").ToString.Trim.Length = 0, "0", r("Nro10T"))
                        oExtracto.Numero_11.Valor = IIf(r("Nro11T").ToString.Trim.Length = 0, "0", r("Nro11T"))
                        oExtracto.Numero_12.Valor = IIf(r("Nro12T").ToString.Trim.Length = 0, "0", r("Nro12T"))
                        oExtracto.Numero_13.Valor = IIf(r("Nro13T").ToString.Trim.Length = 0, "0", r("Nro13T"))
                        oExtracto.Numero_14.Valor = IIf(r("Nro14T").ToString.Trim.Length = 0, "0", r("Nro14T"))
                        oExtracto.Numero_15.Valor = IIf(r("Nro15T").ToString.Trim.Length = 0, "0", r("Nro15T"))
                        oExtracto.Numero_16.Valor = IIf(r("Nro16T").ToString.Trim.Length = 0, "0", r("Nro16T"))
                        oExtracto.Numero_17.Valor = IIf(r("Nro17T").ToString.Trim.Length = 0, "0", r("Nro17T"))
                        oExtracto.Numero_18.Valor = IIf(r("Nro18T").ToString.Trim.Length = 0, "0", r("Nro18T"))
                        oExtracto.Numero_19.Valor = IIf(r("Nro19T").ToString.Trim.Length = 0, "0", r("Nro19T"))
                        oExtracto.Numero_20.Valor = IIf(r("Nro20T").ToString.Trim.Length = 0, "0", r("Nro20T"))
                        oExtracto.Numero_21.Valor = 0
                        oExtracto.Numero_22.Valor = 0
                        oExtracto.Numero_23.Valor = 0
                        oExtracto.Numero_24.Valor = 0
                        oExtracto.Numero_25.Valor = 0
                        oExtracto.Numero_26.Valor = 0
                        oExtracto.Numero_27.Valor = 0
                        oExtracto.Numero_28.Valor = 0
                        oExtracto.Numero_29.Valor = 0
                        oExtracto.Numero_30.Valor = 0
                        oExtracto.Numero_31.Valor = 0
                        oExtracto.Numero_32.Valor = 0
                        oExtracto.Numero_33.Valor = 0
                        oExtracto.Numero_34.Valor = 0
                        oExtracto.Numero_35.Valor = 0
                        oExtracto.Numero_36.Valor = 0

                        oExtracto.Letra_1.Valor = ""
                        oExtracto.Letra_2.Valor = ""
                        oExtracto.Letra_3.Valor = ""
                        oExtracto.Letra_4.Valor = ""

                    Case 50
                        ' 50	Lotería Tradic.
                        oExtracto.Cifras = r("cifras")
                        oExtracto.Juego = "LO"

                        oExtracto.Numero_1.Valor = IIf(r("Nro1T").ToString.Trim.Length = 0, "0", r("Nro1T"))
                        oExtracto.Numero_2.Valor = IIf(r("Nro2T").ToString.Trim.Length = 0, "0", r("Nro2T"))
                        oExtracto.Numero_3.Valor = IIf(r("Nro3T").ToString.Trim.Length = 0, "0", r("Nro3T"))
                        oExtracto.Numero_4.Valor = IIf(r("Nro4T").ToString.Trim.Length = 0, "0", r("Nro4T"))
                        oExtracto.Numero_5.Valor = IIf(r("Nro5T").ToString.Trim.Length = 0, "0", r("Nro5T"))
                        oExtracto.Numero_6.Valor = IIf(r("Nro6T").ToString.Trim.Length = 0, "0", r("Nro6T"))
                        oExtracto.Numero_7.Valor = IIf(r("Nro7T").ToString.Trim.Length = 0, "0", r("Nro7T"))
                        oExtracto.Numero_8.Valor = IIf(r("Nro8T").ToString.Trim.Length = 0, "0", r("Nro8T"))
                        oExtracto.Numero_9.Valor = IIf(r("Nro9T").ToString.Trim.Length = 0, "0", r("Nro9T"))
                        oExtracto.Numero_10.Valor = IIf(r("Nro10T").ToString.Trim.Length = 0, "0", r("Nro10T"))
                        oExtracto.Numero_11.Valor = IIf(r("Nro11T").ToString.Trim.Length = 0, "0", r("Nro11T"))
                        oExtracto.Numero_12.Valor = IIf(r("Nro12T").ToString.Trim.Length = 0, "0", r("Nro12T"))
                        oExtracto.Numero_13.Valor = IIf(r("Nro13T").ToString.Trim.Length = 0, "0", r("Nro13T"))
                        oExtracto.Numero_14.Valor = IIf(r("Nro14T").ToString.Trim.Length = 0, "0", r("Nro14T"))
                        oExtracto.Numero_15.Valor = IIf(r("Nro15T").ToString.Trim.Length = 0, "0", r("Nro15T"))
                        oExtracto.Numero_16.Valor = IIf(r("Nro16T").ToString.Trim.Length = 0, "0", r("Nro16T"))
                        oExtracto.Numero_17.Valor = IIf(r("Nro17T").ToString.Trim.Length = 0, "0", r("Nro17T"))
                        oExtracto.Numero_18.Valor = IIf(r("Nro18T").ToString.Trim.Length = 0, "0", r("Nro18T"))
                        oExtracto.Numero_19.Valor = IIf(r("Nro19T").ToString.Trim.Length = 0, "0", r("Nro19T"))
                        oExtracto.Numero_20.Valor = IIf(r("Nro20T").ToString.Trim.Length = 0, "0", r("Nro20T"))
                        oExtracto.Numero_21.Valor = IIf(r("progres").ToString.Trim.Length = 0, "0", r("progres"))
                        oExtracto.Numero_22.Valor = IIf(r("extrac_lote_1").ToString.Trim.Length = 0, "00000", r("extrac_lote_1").ToString.PadLeft(5, "0")) 'formatea a 5 cifras con ceros a la izquierda
                        oExtracto.Numero_23.Valor = IIf(r("extrac_lote_2").ToString.Trim.Length = 0, "00000", r("extrac_lote_2").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_24.Valor = IIf(r("extrac_lote_3").ToString.Trim.Length = 0, "00000", r("extrac_lote_3").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_25.Valor = IIf(r("extrac_lote_4").ToString.Trim.Length = 0, "00000", r("extrac_lote_4").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_26.Valor = IIf(r("extrac_lote_5").ToString.Trim.Length = 0, "00000", r("extrac_lote_5").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_27.Valor = IIf(r("extrac_lote_6").ToString.Trim.Length = 0, "00000", r("extrac_lote_6").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_28.Valor = IIf(r("extrac_lote_7").ToString.Trim.Length = 0, "00000", r("extrac_lote_7").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_29.Valor = IIf(r("extrac_lote_8").ToString.Trim.Length = 0, "00000", r("extrac_lote_8").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_30.Valor = IIf(r("extrac_lote_9").ToString.Trim.Length = 0, "00000", r("extrac_lote_9").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_31.Valor = IIf(r("extrac_lote_10").ToString.Trim.Length = 0, "00000", r("extrac_lote_10").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_32.Valor = IIf(r("extrac_lote_11").ToString.Trim.Length = 0, "00000", r("extrac_lote_11").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_33.Valor = IIf(r("extrac_lote_12").ToString.Trim.Length = 0, "00000", r("extrac_lote_12").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_34.Valor = IIf(r("extrac_lote_13").ToString.Trim.Length = 0, "00000", r("extrac_lote_13").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_35.Valor = IIf(r("extrac_lote_14").ToString.Trim.Length = 0, "00000", r("extrac_lote_14").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_36.Valor = IIf(r("extrac_lote_15").ToString.Trim.Length = 0, "00000", r("extrac_lote_15").ToString.PadLeft(5, "0"))

                        oExtracto.Letra_1.Valor = ""
                        oExtracto.Letra_2.Valor = ""
                        oExtracto.Letra_3.Valor = ""
                        oExtracto.Letra_4.Valor = ""


                    Case 51
                        ' 51	Lotería Chica
                        oExtracto.Cifras = r("cifras")
                        oExtracto.Juego = "LC"

                        oExtracto.Numero_1.Valor = IIf(r("Nro1T").ToString.Trim.Length = 0, "0", Mid(r("Nro1T"), 1))
                        oExtracto.Numero_2.Valor = IIf(r("Nro2T").ToString.Trim.Length = 0, "0", Mid(r("Nro2T"), 1))
                        oExtracto.Numero_3.Valor = IIf(r("Nro3T").ToString.Trim.Length = 0, "0", Mid(r("Nro3T"), 1))
                        oExtracto.Numero_4.Valor = IIf(r("Nro4T").ToString.Trim.Length = 0, "0", Mid(r("Nro4T"), 1))
                        oExtracto.Numero_5.Valor = IIf(r("Nro5T").ToString.Trim.Length = 0, "0", Mid(r("Nro5T"), 1))
                        oExtracto.Numero_6.Valor = 0
                        oExtracto.Numero_7.Valor = 0
                        oExtracto.Numero_8.Valor = 0
                        oExtracto.Numero_9.Valor = 0
                        oExtracto.Numero_10.Valor = 0
                        oExtracto.Numero_11.Valor = 0
                        oExtracto.Numero_12.Valor = 0
                        oExtracto.Numero_13.Valor = 0
                        oExtracto.Numero_14.Valor = 0
                        oExtracto.Numero_15.Valor = 0
                        oExtracto.Numero_16.Valor = 0
                        oExtracto.Numero_17.Valor = 0
                        oExtracto.Numero_18.Valor = 0
                        oExtracto.Numero_19.Valor = 0
                        oExtracto.Numero_20.Valor = 0
                        oExtracto.Numero_21.Valor = 0
                        oExtracto.Numero_21.Valor = 0
                        oExtracto.Numero_22.Valor = 0
                        oExtracto.Numero_23.Valor = 0
                        oExtracto.Numero_24.Valor = 0
                        oExtracto.Numero_25.Valor = 0
                        oExtracto.Numero_26.Valor = 0
                        oExtracto.Numero_27.Valor = 0
                        oExtracto.Numero_28.Valor = 0
                        oExtracto.Numero_29.Valor = 0
                        oExtracto.Numero_30.Valor = 0
                        oExtracto.Numero_31.Valor = 0
                        oExtracto.Numero_32.Valor = 0
                        oExtracto.Numero_33.Valor = 0
                        oExtracto.Numero_34.Valor = 0
                        oExtracto.Numero_35.Valor = 0
                        oExtracto.Numero_36.Valor = 0

                        oExtracto.Letra_1.Valor = ""
                        oExtracto.Letra_2.Valor = ""
                        oExtracto.Letra_3.Valor = ""
                        oExtracto.Letra_4.Valor = ""


                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        oExtracto.Cifras = 0
                        Select Case idJuego
                            Case 4
                                oExtracto.Juego = "Q2"
                            Case 13
                                oExtracto.Juego = "BR"
                        End Select

                        oExtracto.Numero_1.Valor = r("nro_qn6_bri_1")
                        oExtracto.Numero_2.Valor = r("nro_qn6_bri_2")
                        oExtracto.Numero_3.Valor = r("nro_qn6_bri_3")
                        oExtracto.Numero_4.Valor = r("nro_qn6_bri_4")
                        oExtracto.Numero_5.Valor = r("nro_qn6_bri_5")
                        oExtracto.Numero_6.Valor = r("nro_qn6_bri_6")
                        oExtracto.Numero_7.Valor = r("nro_qn6_bri_7")
                        oExtracto.Numero_8.Valor = r("nro_qn6_bri_8")
                        oExtracto.Numero_9.Valor = r("nro_qn6_bri_9")
                        oExtracto.Numero_10.Valor = r("nro_qn6_bri_10")
                        oExtracto.Numero_11.Valor = r("nro_qn6_bri_11")
                        oExtracto.Numero_12.Valor = r("nro_qn6_bri_12")
                        oExtracto.Numero_13.Valor = r("nro_qn6_bri_13")
                        oExtracto.Numero_14.Valor = r("nro_qn6_bri_14")
                        oExtracto.Numero_15.Valor = r("nro_qn6_bri_15")
                        oExtracto.Numero_16.Valor = r("nro_qn6_bri_16")
                        oExtracto.Numero_17.Valor = r("nro_qn6_bri_17")
                        oExtracto.Numero_18.Valor = r("nro_qn6_bri_18")
                        oExtracto.Numero_19.Valor = r("nro_qn6_bri_19")
                        oExtracto.Numero_20.Valor = r("nro_qn6_bri_20")
                        oExtracto.Numero_21.Valor = r("nro_qn6_bri_21")
                        oExtracto.Numero_22.Valor = r("nro_qn6_bri_22")
                        oExtracto.Numero_23.Valor = r("nro_qn6_bri_23")
                        oExtracto.Numero_24.Valor = r("nro_qn6_bri_24")
                        oExtracto.Numero_25.Valor = IIf(r("nro_qn6_bri_adi_1").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_1"))
                        oExtracto.Numero_26.Valor = IIf(r("nro_qn6_bri_adi_2").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_2"))
                        oExtracto.Numero_27.Valor = IIf(r("nro_qn6_bri_adi_3").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_3"))
                        oExtracto.Numero_28.Valor = IIf(r("nro_qn6_bri_adi_4").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_4"))
                        oExtracto.Numero_29.Valor = IIf(r("nro_qn6_bri_adi_5").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_5"))
                        oExtracto.Numero_30.Valor = IIf(r("nro_qn6_bri_adi_6").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_6"))
                        oExtracto.Numero_31.Valor = IIf(r("nro_qn6_bri_adi_7").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_7"))
                        oExtracto.Numero_32.Valor = IIf(r("nro_qn6_bri_adi_8").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_8"))
                        oExtracto.Numero_33.Valor = IIf(r("nro_qn6_bri_adi_9").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_9"))
                        oExtracto.Numero_34.Valor = IIf(r("nro_qn6_bri_adi_10").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_10"))
                        oExtracto.Numero_35.Valor = IIf(r("nro_qn6_bri_adi_11").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_11"))
                        oExtracto.Numero_36.Valor = IIf(r("nro_qn6_bri_adi_12").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_12"))

                        oExtracto.Letra_1.Valor = ""
                        oExtracto.Letra_2.Valor = ""
                        oExtracto.Letra_3.Valor = ""
                        oExtracto.Letra_4.Valor = ""
                End Select


                miCm = Nothing
                r = Nothing
                'dr1 = Nothing
                dt = Nothing

                Return True

            Catch ex As Exception
                r = Nothing
                'dr1 = Nothing
                dt = Nothing
                miCm = Nothing
                Return False
            End Try
        End Function

        Function getPremios(ByVal idJuego As Integer, ByVal idPgmSorteo As String, ByVal oExtracto As WSExtractos.Extracto) As Boolean
            Dim sSQL As String
            Dim sTabla As String = ""

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim r As DataRow

            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' ************ obtención de datos ******************
                sSQL = " select p.idPremio,  " _
                     & " coalesce(idPgmSorteo, 0) idPgmSorteo, coalesce(importe_pozo, 0) importe_pozo, " _
                     & " coalesce(cant_ganadores, 0) cant_ganadores, coalesce(importe_premio, 0) importe_premio, " _
                     & " coalesce(vacante, 0) vacante, coalesce(secuencia, 0) secuencia ,COALESCE(cant_aciertos,0) as cant_aciertos" _
                     & " from premio p " _
                     & " left join premio_sorteo ps on p.idPremio = ps.idPremio and ps.idPgmSorteo = @idPgmSorteo " _
                     & " where p.idJuego = @idJuego  and habilitado=1" _
                     & " order by p.idjuego,p.idmodalidad,p.orden "
                cm.CommandText = sSQL
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.Parameters.AddWithValue("@idJuego", idJuego)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                ' ************ carga de las listas ******************
                Dim lstPremio As New List(Of WSExtractos.Premio)
                Dim oPremio As WSExtractos.Premio
                Dim totPremio As Integer
                Dim i As Integer = 0
                For Each r In dt.Rows
                    oPremio = New WSExtractos.Premio
                    oPremio.CuponesGanadores = r("cant_ganadores")
                    oPremio.Pozo = r("importe_pozo")
                    oPremio.PremioPorCupon = r("importe_premio")
                    oPremio.Aciertos = r("cant_aciertos")
                    lstPremio.Add(oPremio)
                    If r("idPremio") = 405002 Or r("idPremio") = 405003 Or r("idPremio") = 405004 Then
                        Dim xxx As String
                        xxx = ""
                    End If
                    If r("idPremio") = 1305002 Or r("idPremio") = 1305003 Or r("idPremio") = 1305004 Or r("idPremio") = 1305005 Then
                        Dim xxx As String
                        xxx = ""
                    End If
                    i = i + 1
                Next
                dt.Clear()
                dt = Nothing


                totPremio = lstPremio.Count

               
                Select Case idJuego
                    Case 1 ' 1	Tómbola
                        ' SIN PREMIOS

                    Case 30 ' 30	Poceada Federal
                        ' SIN Premios
                        If totPremio <= 0 Then
                            oExtracto.Premio_1 = New WSExtractos.Premio
                            oExtracto.Premio_1.Aciertos = 8
                            oExtracto.Premio_1.CuponesGanadores = 0
                            oExtracto.Premio_1.Pozo = 5
                            oExtracto.Premio_1.PremioPorCupon = 0


                            oExtracto.Premio_2 = New WSExtractos.Premio
                            oExtracto.Premio_2.Aciertos = 7
                            oExtracto.Premio_2.CuponesGanadores = 0
                            oExtracto.Premio_2.Pozo = 5
                            oExtracto.Premio_2.PremioPorCupon = 0

                            oExtracto.Premio_3 = New WSExtractos.Premio
                            oExtracto.Premio_3.Aciertos = 6
                            oExtracto.Premio_3.CuponesGanadores = 0
                            oExtracto.Premio_3.Pozo = 5
                            oExtracto.Premio_3.PremioPorCupon = 0
                        End If
                        ' CON PREMIOS
                        If totPremio > 0 Then
                            oExtracto.Premio_1 = New WSExtractos.Premio
                            oExtracto.Premio_1 = lstPremio(0)
                        End If
                        If totPremio > 1 Then
                            oExtracto.Premio_2 = New WSExtractos.Premio
                            oExtracto.Premio_2 = lstPremio(1)
                        End If
                        If totPremio > 2 Then
                            oExtracto.Premio_3 = New WSExtractos.Premio
                            oExtracto.Premio_3 = lstPremio(2)
                        End If
                        If totPremio > 3 Then
                            oExtracto.Premio_4 = New WSExtractos.Premio
                            oExtracto.Premio_4 = lstPremio(3)
                        End If
                        If totPremio > 4 Then
                            oExtracto.Premio_5 = New WSExtractos.Premio
                            oExtracto.Premio_5 = lstPremio(4)
                        End If
                        If totPremio > 5 Then
                            oExtracto.Premio_6 = New WSExtractos.Premio
                            oExtracto.Premio_6 = lstPremio(5)
                        End If
                        If totPremio > 6 Then
                            oExtracto.Premio_7 = New WSExtractos.Premio
                            oExtracto.Premio_7 = lstPremio(6)
                        End If
                        If totPremio > 7 Then
                            oExtracto.Premio_8 = New WSExtractos.Premio
                            oExtracto.Premio_8 = lstPremio(7)
                        End If
                        If totPremio > 8 Then
                            oExtracto.Premio_9 = New WSExtractos.Premio
                            oExtracto.Premio_9 = lstPremio(8)
                        End If
                        If totPremio > 9 Then
                            oExtracto.Premio_10 = New WSExtractos.Premio
                            oExtracto.Premio_10 = lstPremio(9)
                        End If
                        If totPremio > 10 Then
                            oExtracto.Premio_11 = New WSExtractos.Premio
                            oExtracto.Premio_11 = lstPremio(10)
                        End If
                        If totPremio > 11 Then
                            oExtracto.Premio_12 = New WSExtractos.Premio
                            oExtracto.Premio_12 = lstPremio(11)
                        End If
                        If totPremio > 12 Then
                            oExtracto.Premio_13 = New WSExtractos.Premio
                            oExtracto.Premio_13 = lstPremio(12)
                        End If
                        If totPremio > 13 Then
                            oExtracto.Premio_14 = New WSExtractos.Premio
                            oExtracto.Premio_14 = lstPremio(13)
                        End If
                        If totPremio > 14 Then
                            oExtracto.Premio_15 = New WSExtractos.Premio
                            oExtracto.Premio_15 = lstPremio(14)
                        End If
                        If totPremio > 15 Then
                            oExtracto.Premio_16 = New WSExtractos.Premio
                            oExtracto.Premio_16 = lstPremio(15)
                        End If
                        If totPremio > 16 Then
                            oExtracto.Premio_17 = New WSExtractos.Premio
                            oExtracto.Premio_17 = lstPremio(16)
                        End If
                        If totPremio > 17 Then
                            oExtracto.Premio_18 = New WSExtractos.Premio
                            oExtracto.Premio_18 = lstPremio(17)
                        End If
                        If totPremio > 18 Then
                            oExtracto.Premio_19 = New WSExtractos.Premio
                            oExtracto.Premio_19 = lstPremio(18)
                        End If
                        If totPremio > 19 Then
                            oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.Premio_20 = lstPremio(19)
                        End If

                    Case 2, 3, 8, 49
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' SIN PREMIOS

                    Case 50
                        ' 50	Lotería Tradic.
                        ' CON PREMIOS
                        If totPremio > 0 Then
                            oExtracto.Premio_1 = New WSExtractos.Premio
                            oExtracto.Premio_1 = lstPremio(0)
                        End If
                        If totPremio > 1 Then
                            oExtracto.Premio_2 = New WSExtractos.Premio
                            oExtracto.Premio_2 = lstPremio(1)
                        End If
                        If totPremio > 2 Then
                            oExtracto.Premio_3 = New WSExtractos.Premio
                            oExtracto.Premio_3 = lstPremio(2)
                        End If
                        If totPremio > 3 Then
                            oExtracto.Premio_4 = New WSExtractos.Premio
                            oExtracto.Premio_4 = lstPremio(3)
                        End If
                        If totPremio > 4 Then
                            oExtracto.Premio_5 = New WSExtractos.Premio
                            oExtracto.Premio_5 = lstPremio(4)
                        End If
                        If totPremio > 5 Then
                            oExtracto.Premio_6 = New WSExtractos.Premio
                            oExtracto.Premio_6 = lstPremio(5)
                        End If
                        If totPremio > 6 Then
                            oExtracto.Premio_7 = New WSExtractos.Premio
                            oExtracto.Premio_7 = lstPremio(6)
                        End If
                        If totPremio > 7 Then
                            oExtracto.Premio_8 = New WSExtractos.Premio
                            oExtracto.Premio_8 = lstPremio(7)
                        End If
                        If totPremio > 8 Then
                            oExtracto.Premio_9 = New WSExtractos.Premio
                            oExtracto.Premio_9 = lstPremio(8)
                        End If
                        If totPremio > 9 Then
                            oExtracto.Premio_10 = New WSExtractos.Premio
                            oExtracto.Premio_10 = lstPremio(9)
                        End If
                        If totPremio > 10 Then
                            oExtracto.Premio_11 = New WSExtractos.Premio
                            oExtracto.Premio_11 = lstPremio(10)
                        End If
                        If totPremio > 11 Then
                            oExtracto.Premio_12 = New WSExtractos.Premio
                            oExtracto.Premio_12 = lstPremio(11)
                        End If
                        If totPremio > 12 Then
                            oExtracto.Premio_13 = New WSExtractos.Premio
                            oExtracto.Premio_13 = lstPremio(12)
                        End If
                        If totPremio > 13 Then
                            oExtracto.Premio_14 = New WSExtractos.Premio
                            oExtracto.Premio_14 = lstPremio(13)
                        End If
                        If totPremio > 14 Then
                            oExtracto.Premio_15 = New WSExtractos.Premio
                            oExtracto.Premio_15 = lstPremio(14)
                        End If
                        If totPremio > 15 Then
                            oExtracto.Premio_16 = New WSExtractos.Premio
                            oExtracto.Premio_16 = lstPremio(15)
                        End If
                        If totPremio > 16 Then
                            oExtracto.Premio_17 = New WSExtractos.Premio
                            oExtracto.Premio_17 = lstPremio(16)
                        End If
                        If totPremio > 17 Then
                            oExtracto.Premio_18 = New WSExtractos.Premio
                            oExtracto.Premio_18 = lstPremio(17)
                        End If
                        If totPremio > 18 Then
                            oExtracto.Premio_19 = New WSExtractos.Premio
                            oExtracto.Premio_19 = lstPremio(18)
                        End If
                        If totPremio > 19 Then
                            oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.Premio_20 = lstPremio(19)
                        End If
                        '** se completan los datos de premios progresiones,aporximaciones,etc
                        'terminaciones
                        If totPremio > 20 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_imp3 = lstPremio(20).PremioPorCupon
                        End If
                        If totPremio > 21 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_imp2 = lstPremio(21).PremioPorCupon
                        End If
                        If totPremio > 22 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_imp1 = lstPremio(22).PremioPorCupon
                        End If
                        '** aproximaciones
                        '1 premio
                        If totPremio > 23 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_1aprox1 = lstPremio(23).PremioPorCupon
                        End If
                        If totPremio > 24 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_1aprox2 = lstPremio(24).PremioPorCupon
                        End If
                        If totPremio > 25 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_1aprox3 = lstPremio(25).PremioPorCupon
                        End If
                        If totPremio > 26 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_1aprox4 = lstPremio(26).PremioPorCupon
                        End If

                        '2 premio
                        If totPremio > 27 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_2aprox1 = lstPremio(27).PremioPorCupon
                        End If
                        If totPremio > 28 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_2aprox2 = lstPremio(28).PremioPorCupon
                        End If


                        '3 premio
                        If totPremio > 29 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_3aprox1 = lstPremio(29).PremioPorCupon
                        End If
                        If totPremio > 30 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_3aprox2 = lstPremio(30).PremioPorCupon
                        End If

                        'extraccion
                        If totPremio > 31 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_exj_extra = lstPremio(31).PremioPorCupon
                        End If
                        'progresion
                        If totPremio > 32 Then
                            'oExtracto.Premio_20 = New WSExtractos.Premio
                            oExtracto.lo_Progresion = lstPremio(32).PremioPorCupon
                        End If

                    Case 51
                        ' 51	Lotería Chica
                        ' CON PREMIOS
                        If totPremio > 0 Then
                            oExtracto.lo_exj_imp1 = lstPremio(0).PremioPorCupon
                        End If
                        If totPremio > 1 Then
                            oExtracto.lo_exj_imp2 = lstPremio(1).PremioPorCupon
                        End If
                        If totPremio > 2 Then
                            oExtracto.lo_exj_imp3 = lstPremio(2).PremioPorCupon
                        End If
                        If totPremio > 3 Then
                            oExtracto.lo_exj_imp4 = lstPremio(3).PremioPorCupon
                        End If
                        If totPremio > 4 Then
                            oExtracto.lo_exj_imp5 = lstPremio(4).PremioPorCupon
                        End If
                        If totPremio > 5 Then
                            oExtracto.lo_exj_1aprox3 = lstPremio(5).PremioPorCupon
                        End If
                        If totPremio > 6 Then
                            oExtracto.lo_exj_2aprox3 = lstPremio(6).PremioPorCupon
                        End If
                        If totPremio > 7 Then
                            oExtracto.lo_exj_3aprox3 = lstPremio(7).PremioPorCupon
                        End If
                        If totPremio > 8 Then
                            oExtracto.lo_exj_4aprox3 = lstPremio(8).PremioPorCupon
                        End If
                        If totPremio > 9 Then
                            oExtracto.lo_exj_5aprox3 = lstPremio(9).PremioPorCupon
                        End If
                        If totPremio > 10 Then
                            oExtracto.lo_exj_1aprox2 = lstPremio(10).PremioPorCupon
                        End If
                        If totPremio > 11 Then
                            oExtracto.lo_exj_2aprox2 = lstPremio(11).PremioPorCupon
                        End If
                        If totPremio > 12 Then
                            oExtracto.lo_exj_3aprox2 = lstPremio(12).PremioPorCupon
                        End If
                        If totPremio > 13 Then
                            oExtracto.lo_exj_4aprox2 = lstPremio(13).PremioPorCupon
                        End If
                        If totPremio > 14 Then
                            oExtracto.lo_exj_5aprox2 = lstPremio(14).PremioPorCupon
                        End If
                        If totPremio > 15 Then
                            oExtracto.lo_exj_1aprox1 = lstPremio(15).PremioPorCupon
                        End If

                    Case 4
                        ' 4	Quini 6 
                        ' CON PREMIOS
                        'tradicional 1 premio
                        If totPremio > 0 Then
                            oExtracto.Premio_1 = New WSExtractos.Premio
                            oExtracto.Premio_1 = lstPremio(0)
                        End If
                        'tradicional 2 premio
                        If totPremio > 1 Then
                            oExtracto.Premio_2 = New WSExtractos.Premio
                            oExtracto.Premio_2 = lstPremio(1)
                        End If
                        'tradicional 3 premio
                        If totPremio > 2 Then
                            oExtracto.Premio_3 = New WSExtractos.Premio
                            oExtracto.Premio_3 = lstPremio(2)
                        End If
                        'tradicional estímulo
                        If totPremio > 3 Then
                            oExtracto.Premio_4 = New WSExtractos.Premio
                            oExtracto.Premio_4 = lstPremio(3)
                        End If
                        'segunda 1 premio
                        If totPremio > 4 Then
                            oExtracto.Premio_5 = New WSExtractos.Premio
                            oExtracto.Premio_5 = lstPremio(4)
                        End If
                        'segunda 2 premio
                        If totPremio > 5 Then
                            oExtracto.Premio_6 = New WSExtractos.Premio
                            oExtracto.Premio_6 = lstPremio(5)
                        End If
                        'segunda 3 premio
                        If totPremio > 6 Then
                            oExtracto.Premio_7 = New WSExtractos.Premio
                            oExtracto.Premio_7 = lstPremio(6)
                        End If
                        'segunda estímulo
                        If totPremio > 7 Then
                            oExtracto.Premio_8 = New WSExtractos.Premio
                            oExtracto.Premio_8 = lstPremio(7)
                        End If
                        'revancha 1 premio
                        If totPremio > 8 Then
                            oExtracto.Premio_9 = New WSExtractos.Premio
                            oExtracto.Premio_9 = lstPremio(8)
                        End If
                        'Revancha estímulo
                        If totPremio > 9 Then
                            oExtracto.Premio_10 = New WSExtractos.Premio
                            oExtracto.Premio_10 = lstPremio(9)
                        End If
                        'premio extra
                        If totPremio > 10 Then
                            oExtracto.Premio_11 = New WSExtractos.Premio
                            oExtracto.Premio_11 = lstPremio(10)
                        End If
                        'adicional 1 premio
                        If totPremio > 11 Then
                            oExtracto.Premio_12 = New WSExtractos.Premio
                            oExtracto.Premio_12 = lstPremio(11)
                        End If
                        'adicional 2 premio
                        If totPremio > 12 Then
                            oExtracto.Premio_13 = New WSExtractos.Premio
                            oExtracto.Premio_13 = lstPremio(12)
                        End If
                        'adicional 3 premio
                        If totPremio > 13 Then
                            If lstPremio(13).PremioPorCupon > 0 Then
                                oExtracto.Premio_13 = lstPremio(13)
                            End If
                        End If
                        'adicional 4 premio
                        If totPremio > 14 Then
                            If lstPremio(14).PremioPorCupon > 0 Then
                                oExtracto.Premio_13 = lstPremio(14)
                            End If
                        End If
                        'adicional estímulo
                        If totPremio > 15 Then
                            oExtracto.Premio_14 = New WSExtractos.Premio
                            oExtracto.Premio_14 = lstPremio(15)
                        End If
                        'siempre sale 1 premio
                        If totPremio > 16 Then
                            oExtracto.Premio_15 = New WSExtractos.Premio
                            oExtracto.Premio_15 = lstPremio(16)
                        End If
                        'siempre sale estímulo
                        If totPremio > 17 Then
                            oExtracto.Premio_16 = New WSExtractos.Premio
                            oExtracto.Premio_16 = lstPremio(17)
                        End If


                    Case 13
                        '13	Brinco
                        ' CON PREMIOS
                        ' CON SUELDOS
                        'brinco común 6 aciertos
                        If totPremio > 0 Then
                            oExtracto.Premio_1 = New WSExtractos.Premio
                            oExtracto.Premio_1 = lstPremio(0)
                        End If
                        'brinco común 5 aciertos
                        If totPremio > 1 Then
                            oExtracto.Premio_2 = New WSExtractos.Premio
                            oExtracto.Premio_2 = lstPremio(1)
                        End If
                        'brinco común 4 aciertos
                        If totPremio > 2 Then
                            oExtracto.Premio_3 = New WSExtractos.Premio
                            oExtracto.Premio_3 = lstPremio(2)
                        End If
                        'brinco común 3 aciertos
                        If totPremio > 3 Then
                            oExtracto.Premio_4 = New WSExtractos.Premio
                            oExtracto.Premio_4 = lstPremio(3)
                        End If
                        'brinco común estímulo
                        If totPremio > 4 Then
                            oExtracto.Premio_5 = New WSExtractos.Premio
                            oExtracto.Premio_5 = lstPremio(4)
                        End If
                        'brinco común sueldos
                        If totPremio > 5 Then
                            oExtracto.Premio_6 = New WSExtractos.Premio
                            oExtracto.Premio_6 = lstPremio(5)
                        End If
                        'brinco adicional 1 premio
                        If totPremio > 6 Then
                            oExtracto.Premio_7 = New WSExtractos.Premio
                            oExtracto.Premio_7 = lstPremio(6)
                        End If
                        'brinco adicional 2 premio
                        If totPremio > 7 Then
                            If lstPremio(7).PremioPorCupon > 0 Then
                                oExtracto.Premio_7 = New WSExtractos.Premio
                                oExtracto.Premio_7 = lstPremio(7)
                            End If
                        End If
                        'brinco adicional 3 premio
                        If totPremio > 8 Then
                            If lstPremio(8).PremioPorCupon > 0 Then
                                oExtracto.Premio_7 = New WSExtractos.Premio
                                oExtracto.Premio_7 = lstPremio(8)
                            End If
                        End If
                        'brinco adicional 4 premio
                        If totPremio > 9 Then
                            If lstPremio(9).PremioPorCupon > 0 Then
                                oExtracto.Premio_7 = New WSExtractos.Premio
                                oExtracto.Premio_7 = lstPremio(9)
                            End If
                        End If
                        'brinco adicional estimulo
                        If totPremio > 10 Then
                            oExtracto.Premio_8 = New WSExtractos.Premio
                            oExtracto.Premio_8 = lstPremio(10)
                        End If

                        sSQL = " select orden, agencia, localidad, provincia, LTRIM(RTRIM(SUBSTRING(COALESCE(cupon,' '), 1, CASE WHEN CHARINDEX(':',COALESCE(cupon,' ')) > 3 THEN CHARINDEX(':',COALESCE(cupon,' ')) -3 ELSE LEN(COALESCE(cupon,' ')) END))) AS CUPON, secuencia from premio_sueldo_br " _
                             & " where idPgmSorteo = @idPgmSorteo order by orden "
                        cm.CommandText = sSQL
                        cm.Parameters.Clear()
                        cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)

                        dr = cm.ExecuteReader()
                        dt = New DataTable
                        dt.Load(dr)
                        dr.Close()

                        Dim lstSueldo As New List(Of WSExtractos.Sueldo)
                        Dim oSueldo As WSExtractos.Sueldo

                        For Each r In dt.Rows
                            oSueldo = New WSExtractos.Sueldo
                            oSueldo = New WSExtractos.Sueldo
                            oSueldo.Agencia = r("agencia")
                            oSueldo.Id = r("orden")
                            oSueldo.Localidad = r("localidad")
                            oSueldo.Orden = r("orden")
                            oSueldo.Provincia = r("provincia")
                            oSueldo.Ticket = r("cupon")

                            lstSueldo.Add(oSueldo)
                        Next

                        Dim totSueldos As Integer
                        totSueldos = lstSueldo.Count
                        oExtracto.bri_HaySueldo = IIf(totSueldos > 0, "S", "N")
                        oExtracto.bri_CantSueldo = totSueldos
                        oExtracto.bri_PozoEstimado = oExtracto.PozoProxEstimado
                        If totSueldos > 0 Then
                            oExtracto.bri_Sueldo_1 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_1 = lstSueldo(0)
                        End If
                        If totSueldos > 1 Then
                            oExtracto.bri_Sueldo_2 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_2 = lstSueldo(1)
                        End If
                        If totSueldos > 2 Then
                            oExtracto.bri_Sueldo_3 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_3 = lstSueldo(2)
                        End If
                        If totSueldos > 3 Then
                            oExtracto.bri_Sueldo_4 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_4 = lstSueldo(3)
                        End If
                        If totSueldos > 4 Then
                            oExtracto.bri_Sueldo_5 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_5 = lstSueldo(4)
                        End If
                        If totSueldos > 5 Then
                            oExtracto.bri_Sueldo_6 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_6 = lstSueldo(5)
                        End If
                        If totSueldos > 6 Then
                            oExtracto.bri_Sueldo_7 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_7 = lstSueldo(6)
                        End If
                        If totSueldos > 7 Then
                            oExtracto.bri_Sueldo_8 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_8 = lstSueldo(7)
                        End If
                        If totSueldos > 8 Then
                            oExtracto.bri_Sueldo_9 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_9 = lstSueldo(8)
                        End If
                        If totSueldos > 9 Then
                            oExtracto.bri_Sueldo_10 = New WSExtractos.Sueldo
                            oExtracto.bri_Sueldo_10 = lstSueldo(9)
                        End If
                End Select

                Return True

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Return False
            End Try
        End Function

        Public Function getPgmSorteos(ByVal idPgmConcurso As Integer) As ListaOrdenada(Of PgmSorteo)

            Dim ls As New ListaOrdenada(Of PgmSorteo)
            Dim o As New PgmSorteo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT * "
                vsql = vsql & " FROM pgmsorteo  "
                vsql = vsql & " where  idPgmConcurso = @idPgmConcurso"

                vsql = " SELECT s.*, j.jue_letras, j.id_agr_juego " _
                     & " FROM pgmsorteo s " _
                     & " inner join pgmconcurso pc on pc.idpgmconcurso = s.idpgmconcurso " _
                     & " inner join concursoJuego cj on s.idJuego = cj.idJuego and cj.idConcurso = pc.idconcurso " _
                     & " inner join juego j on j.idJuego = cj.idJuego " _
                     & " where pc.idPgmConcurso = @idPgmConcurso " _
                     & " order by orden "

                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Try
                        If Helpers.General.Modo_Operacion = "PC-A" And r("idEstadoPgmConcurso") <= 20 Then
                            setParametrosSorteoPCA(r("idPgmSorteo"))
                        End If
                    Catch ex1 As Exception
                    End Try

                    o = New PgmSorteo
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

        Public Function getPgmSorteos(ByVal idPgmConcurso As Integer, ByVal idConcurso As Integer, Optional ByVal Estado As Integer = -1) As ListaOrdenada(Of PgmSorteo)

            Dim ls As New ListaOrdenada(Of PgmSorteo)
            Dim o As New PgmSorteo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT s.*, j.jue_letras, j.id_agr_juego " _
                     & " FROM pgmsorteo s " _
                     & " inner join pgmconcurso pc on pc.idpgmconcurso = s.idpgmconcurso " _
                     & " inner join concursoJuego cj on s.idJuego = cj.idJuego and cj.idConcurso = pc.idconcurso " _
                     & " inner join juego j on j.idJuego = cj.idJuego " _
                     & " where pc.idPgmConcurso = @idPgmConcurso " & IIf(Estado > -1, " and s.idestadopgmconcurso = " & Estado, "") _
                     & " order by orden "
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Try
                        If Helpers.General.Modo_Operacion = "PC-A" And r("idEstadoPgmConcurso") <= 20 Then
                            setParametrosSorteoPCA(r("idPgmSorteo"))
                        End If
                    Catch ex1 As Exception
                    End Try

                    'o.idPgmSorteo = r("idPgmSorteo")
                    o = New PgmSorteo
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

        Public Function getUltimoConfirmado(Optional ByVal idJuego As Integer = -1) As PgmSorteo
            Dim o As New PgmSorteo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT top 1 s.*, coalesce(j.jue_letras,'') as jue_letras, j.id_agr_juego " _
                     & " FROM pgmsorteo s " _
                     & " inner join pgmconcurso pc on pc.idpgmconcurso = s.idpgmconcurso " _
                     & " inner join concursoJuego cj on s.idJuego = cj.idJuego and cj.idConcurso = pc.idconcurso " _
                     & " inner join juego j on j.idJuego = cj.idJuego " _
                     & " where s.idJuego = case when " & idJuego & " <> -1 then " & idJuego & " else s.idJuego end and s.idEstadoPgmConcurso = 50 order by pc.fechahora desc "
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New PgmSorteo
                    Load(o, r)
                Next
                If dt.Rows.Count = 0 Then
                    Return Nothing
                End If
                Return o

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getUltimoEnCurso() As PgmSorteo
            Dim o As New PgmSorteo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT top 1 s.*, coalesce(j.jue_letras,'') as jue_letras, j.id_agr_juego " _
                     & " FROM pgmsorteo s " _
                     & " inner join pgmconcurso pc on pc.idpgmconcurso = s.idpgmconcurso " _
                     & " inner join concursoJuego cj on s.idJuego = cj.idJuego and cj.idConcurso = pc.idconcurso " _
                     & " inner join juego j on j.idJuego = cj.idJuego " _
                     & " where s.idEstadoPgmConcurso in (20,30,40) order by pc.fechahora desc, cj.esprincipal desc "
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New PgmSorteo
                    Load(o, r)
                Next
                If dt.Rows.Count = 0 Then
                    Return Nothing
                End If
                Return o

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getIdSor(ByRef oPgmSorteo As PgmSorteo) As String
            Dim id_sor As String = ""
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT id_sor " _
                     & " FROM pgmsorteo s " _
                     & " where s.idPgmsorteo = " & oPgmSorteo.idPgmSorteo
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    id_sor = r("id_sor")
                Next

                Return id_sor

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function


        Public Function getPgmSorteo(ByVal idPgmSorteo As Long) As PgmSorteo
            Dim o As New PgmSorteo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT s.*, coalesce(j.jue_letras,'') as jue_letras, j.id_agr_juego " _
                     & " FROM pgmsorteo s " _
                     & " inner join pgmconcurso pc on pc.idpgmconcurso = s.idpgmconcurso " _
                     & " inner join concursoJuego cj on s.idJuego = cj.idJuego and cj.idConcurso = pc.idconcurso " _
                     & " inner join juego j on j.idJuego = cj.idJuego " _
                     & " where s.idPgmSOrteo= @idPgmSorteo order by orden "
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Try
                        If Helpers.General.Modo_Operacion = "PC-A" And r("idEstadoPgmConcurso") <= 20 Then
                            setParametrosSorteoPCA(r("idPgmSorteo"))
                        End If
                    Catch ex1 As Exception
                    End Try

                    o = New PgmSorteo
                    Load(o, r)
                Next

                Return o

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmSorteoId(ByVal idJuego As Integer, ByVal nroSorteo As Integer) As Integer
            'Dim o As New PgmSorteo
            Dim idsorteo As Integer
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT idpgmsorteo, id_sor FROM PgmSorteo where idJuego = @idJuego and nroSorteo = @nroSorteo "
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idJuego", idJuego)
                cm.Parameters.AddWithValue("@nroSorteo", nroSorteo)
                idsorteo = cm.ExecuteScalar
                ''dr = cm.ExecuteReader()
                ''dt.Load(dr)
                ''dr.Close()

                ''For Each r As DataRow In dt.Rows
                ''    o = New PgmSorteo
                ''    o.idPgmSorteo = r("idPgmSorteo")
                ''Next

                ''dr = Nothing
                ''dt = Nothing
                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                General.Cerrar_Conexion()
                'Return o.idPgmSorteo
                Return idsorteo

            Catch ex As Exception
                dr = Nothing
                dt = Nothing
                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                Throw New Exception(ex.Message)
                Return 0
            End Try
        End Function

        Public Function getPgmSorteoIdSor(ByVal idJuego As Integer, ByVal nroSorteo As Integer) As String
            'Dim o As New PgmSorteo
            Dim id_sor As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  id_sor FROM PgmSorteo where idJuego = @idJuego and nroSorteo = @nroSorteo "
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@idJuego", idJuego)
                cm.Parameters.AddWithValue("@nroSorteo", nroSorteo)
                id_sor = cm.ExecuteScalar
  
                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                General.Cerrar_Conexion()
                'Return o.idPgmSorteo
                Return id_sor

            Catch ex As Exception
                Try
                    dr = Nothing
                    dt = Nothing
                    cm.Connection.Close()
                    cm.Connection = Nothing
                    cm.Dispose()
                    cm = Nothing
                Catch ex2 As Exception
                End Try
                Throw New Exception(ex.Message)
                Return ""
            End Try
        End Function

        Public Function getPgmSorteo(ByVal idJuego As Integer, ByVal nroSorteo As Double) As PgmSorteo
            Dim o As New PgmSorteo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                'vsql = " SELECT ps.*, j.id_agr_juego FROM PgmSorteo ps inner join juego j on j.idjuego = ps.idjuego where idJuego = @idJuego and nroSorteo = @nroSorteo "
                vsql = " SELECT s.*, coalesce(j.jue_letras,'') as jue_letras, j.id_agr_juego " _
                     & " FROM pgmsorteo s " _
                    & " inner join pgmconcurso pc on pc.idpgmconcurso = s.idpgmconcurso " _
                    & " inner join concursoJuego cj on s.idJuego = cj.idJuego and cj.idConcurso = pc.idconcurso " _
                    & " inner join juego j on j.idJuego = cj.idJuego " _
                    & " where s.idJuego = @idJuego and s.nroSorteo = @nroSorteo  order by orden "
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@idJuego", idJuego)
                cm.Parameters.AddWithValue("@nroSorteo", nroSorteo)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New PgmSorteo
                    o.idPgmSorteo = r("idPgmSorteo")
                Next

                Return o

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmSorteosDT(ByVal idPgmConcurso As Integer) As DataTable
            Dim o As New PgmSorteo

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT j.idJuego, j.jue_desc, s.nroSorteo, s.fechaHora, s.fechaHoraPrescripcion, fechaHoraProximo " _
                     & " FROM pgmSorteo s " _
                     & " inner join juego j on s.idJuego = j.idJuego " _
                     & " where idPgmConcurso = @idPgmConcurso "
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                Return dt

            Catch ex As Exception
                If Not dr.isclosed Then dr.close()
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function


        Public Function setPgmSorteo(ByVal oPS As PgmSorteo, ByVal idJuegoPrincipal As Integer) As Boolean
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " update PgmSorteo " _
                     & " set fechaHora = @fechaHora, fechaHoraPrescripcion = @fechaHoraPrescripcion, fechaHoraProximo = @fechaHoraProximo, localidad = @localidad,fechahorainireal = @fechahorainireal " _
                     & " where idPgmSorteo = @idPgmSorteo "

                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@fechaHora", oPS.fechaHora)
                cm.Parameters.AddWithValue("@fechaHoraPrescripcion", oPS.fechaHoraPrescripcion)
                cm.Parameters.AddWithValue("@fechaHoraProximo", oPS.fechaHoraProximo)
                cm.Parameters.AddWithValue("@localidad", oPS.localidad)
                cm.Parameters.AddWithValue("@idPgmSorteo", oPS.idPgmSorteo)
                cm.Parameters.AddWithValue("@fechaHoraIniReal", oPS.fechaHoraIniReal)
                cm.ExecuteNonQuery()
                '** si es el juego rector,tb se actualiza la hora inicio del concurso
                If oPS.idJuego = idJuegoPrincipal Then
                    vsql = " update Pgmconcurso " _
                         & " set fechahorainireal = @fechaHoraIniReal " _
                         & " where idPgmconcurso = @idPgmconcurso "
                    cm.CommandText = vsql
                    cm.Parameters.Clear()
                    cm.Parameters.AddWithValue("@fechaHoraIniReal", oPS.fechaHoraIniReal)
                    cm.Parameters.AddWithValue("@idPgmconcurso", oPS.idPgmSorteo)
                    cm.ExecuteNonQuery()
                End If

                If Helpers.General.Modo_Operacion = "PC-A" And oPS.idEstadoPgmConcurso <= 20 Then
                    setParametrosSorteoPCA(oPS.idPgmSorteo)
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Shared Function Load(ByRef o As PgmSorteo, ByRef dr As DataRow) As Boolean
            Dim boAutoridad As New AutoridadBO
            Dim boPozo As New PozoBO
            Dim oe As New PgmSorteo_LoteriaDAL
            Dim oConcurso As New PgmConcursoDAL
            Dim juegobo As New JuegoBO
            Try
                o.idPgmSorteo = dr("idPgmSorteo")
                o.fechaHora = dr("fechahora")
                o.fechaHoraFinReal = Es_Nulo(Of Date)(dr("fechaHoraFinReal"), "01/01/1999")
                o.fechaHoraIniReal = Es_Nulo(Of Date)(dr("fechaHoraIniReal"), "01/01/1999")
                o.fechaHoraPrescripcion = Es_Nulo(Of Date)(dr("fechaHoraPrescripcion"), "01/01/1999")
                o.fechaHoraProximo = Es_Nulo(Of Date)(dr("fechaHoraProximo"), "01/01/1999")
                o.idEstadoPgmConcurso = dr("idEstadoPgmConcurso")
                o.idJuego = dr("idJuego")
                o.nroSorteo = dr("nroSorteo")
                o.idJuegoLetra = dr("jue_letras")
                o.localidad = Es_Nulo(Of String)(dr("localidad"), "")
                o.autoridades = boAutoridad.getAutoridad_PgmSorteo(dr("idPgmSorteo"))
                o.pozos = boPozo.getPozo(dr("idJuego"), dr("nroSorteo"))
                o.PozoEstimado = Es_Nulo(Of Double)(dr("pozoestimadoproximo"), 0)
                o.ExtraccionesLoteria = oe.getSorteosLoteria(dr("idPgmSorteo"))
                o.ConfirmadoParcial = Es_Nulo(Of Boolean)(dr("ConfirmadoParcial"), 0)
                o.idPgmConcurso = Es_Nulo(Of Long)(dr("idpgmconcurso"), 0)
                'o.NombreSorteo = o.nroSorteo & " - " & juegobo.getJuegoDescripcion(o.idJuego)
                o.NombreSorteo = juegobo.getJuegoDescripcion(o.idJuego) & "  -  " & o.nroSorteo
                o.PathLocalJuego = juegobo.getJuego(o.idJuego).Jue_PathLocal.Trim
                o.ParProxPozoConfirmado = Es_Nulo(Of Boolean)(dr("ParProxPozoConfirmado"), 0)
                o.idSor = Es_Nulo(Of String)(dr("id_sor"), "")
                o.idAgrJuego = dr("id_agr_juego")

                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function GetJuegoSorteos(ByVal cantDias As Integer) As ListaOrdenada(Of cJuegoSorteo)
            Dim ls As New ListaOrdenada(Of cJuegoSorteo)
            Dim o As New cJuegoSorteo
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select ps.idjuego,ps.nrosorteo,j.jue_desc as juego,ps.idpgmsorteo,ps.fechahora,ps.idpgmconcurso from "
                vsql = vsql & " ("
                vsql = vsql & "select idjuego,max(fechahora) as fechahora from pgmsorteo where idestadopgmconcurso>10"
                vsql = vsql & " group by idjuego"
                vsql = vsql & ")FechaJuego"
                vsql = vsql & " inner join pgmsorteo ps on ps.idjuego=fechajuego.idjuego"
                vsql = vsql & " left join juego j on ps.idjuego=j.idjuego"
                vsql = vsql & " where(ps.idestadopgmconcurso > 10)"
                vsql = vsql & " and ps.fechahora>=datediff(d,fechajuego.fechahora-@cantDias,fechajuego.fechahora)"
                vsql = vsql & " group by ps.idjuego,ps.nrosorteo,j.jue_desc,ps.idpgmsorteo,ps.fechahora,ps.idpgmconcurso"
                vsql = vsql & " order by ps.fechahora desc"
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@cantDias", cantDias)

                dr = cm.ExecuteReader()

                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New cJuegoSorteo
                    LoadJuegoSorteos(o, r)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.isclosed Then dr.close()
                GetJuegoSorteos = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function GetJuegosSorteo(ByVal cantDias As Integer) As ListaOrdenada(Of cJuegosSorteo)
            Dim ls As New ListaOrdenada(Of cJuegosSorteo)
            Dim o As New cJuegosSorteo
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select distinct idjuego as idjuego, juego from("

                vsql = vsql & " select ps.idjuego,ps.nrosorteo,j.jue_desc as juego from "
                vsql = vsql & " ("
                vsql = vsql & " select idjuego,max(fechahora) as fechahora from pgmsorteo where idestadopgmconcurso>10"
                vsql = vsql & " group by idjuego"
                vsql = vsql & " )FechaJuego"
                vsql = vsql & " inner join pgmsorteo ps on ps.idjuego=fechajuego.idjuego"
                vsql = vsql & " left join juego j on ps.idjuego=j.idjuego"
                vsql = vsql & " where(ps.idestadopgmconcurso > 10)"
                vsql = vsql & " and ps.fechahora>=datediff(d,fechajuego.fechahora-@cantDias,fechajuego.fechahora)"
                vsql = vsql & " group by ps.idjuego,ps.nrosorteo,j.jue_desc"
                vsql = vsql & " )as a"
                cm.CommandText = vsql
                cm.Parameters.AddWithValue("@cantDias", cantDias)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New cJuegosSorteo
                    LoadJuegossorteo(o, r)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.isclosed Then dr.close()
                GetJuegosSorteo = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function LoadJuegossorteo(ByRef o As cJuegosSorteo, ByRef dr As DataRow) As Boolean
            Try
                o.IdJuego = dr("idjuego")
                o.Nombre = dr("juego")
            Catch ex As Exception
                LoadJuegossorteo = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function LoadJuegoSorteos(ByRef o As cJuegoSorteo, ByRef dr As DataRow) As Boolean
            Try
                o.IdJuego = dr("idjuego")
                o.NroSorteo = dr("nrosorteo")
                o.idPgmSorteo = dr("idpgmsorteo")
                o.DisplayText = dr("nrosorteo") & " - " & Format(dr("fechahora"), "dd/MM/yyyy")
                o.IdPgmConcurso = dr("idpgmconcurso")
            Catch ex As Exception
                LoadJuegoSorteos = False
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function GenerarListadoExtracciones(ByVal idPgmConcurso As Integer, ByVal idpgmsorteo As Integer, ByVal destino As String, ByVal PathDestino As String, ByVal msgret As String) As Boolean
            Dim dt As DataTable
            Dim ds As New DataSet
            Dim pgmC As New PgmConcursoDAL
            dt = pgmC.ObtenerDatosListado(idPgmConcurso)
            ds.Tables.Add(dt)
            'ds.WriteXmlSchema("D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES\Listado1.xml")
            Dim path_reporte As String = PathDestino ' Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & "INFORMES" '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES"
            Dim reporte As New Listado
            reporte.GenerarListado(ds, path_reporte)
        End Function

        Public Function setPgmSorteoEstado(ByVal idPgmSorteo As Integer, ByVal estado As Integer) As Boolean
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspActualizaEstadoSorteo"

                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idEstadopgmSorteo", estado)
                cm.Parameters("@idEstadopgmSorteo").Direction = ParameterDirection.Input

                cm.ExecuteNonQuery()

            Catch ex As Exception
                FileSystemHelper.Log("setPgmSorteoEstado:" & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function setParametrosSorteoPCA(ByVal idPgmSorteo As Integer) As Boolean
            Dim cm As SqlCommand = New SqlCommand

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspObtenerParametrosPCA"

                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.ExecuteNonQuery()

            Catch ex As Exception
                FileSystemHelper.Log("setParametrosSorteoPCA:" & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function ActualizarDatosSorteo(ByVal sorteo As PgmSorteo, ByVal pozo As Double) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String

            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " UPDATE pgmSorteo " _
                       & " set fechaHoraIniReal = @fechaHoraIniReal " _
                       & " , fechaHoraFinReal = @fechaHoraIniReal " _
                       & " , fechaHoraProximo = @fechaHoraProximo " _
                       & " , fechaHoraPrescripcion = @fechaHoraPrescripcion " _
                       & " , PozoEstimadoProximo = @PozoEstimadoProximo " _
                       & " WHERE idPgmSorteo = @IdPgmSorteo"

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@fechaHoraIniReal", sorteo.fechaHoraIniReal)
                cm.Parameters.AddWithValue("@fechaHoraFinReal", sorteo.fechaHoraFinReal)
                cm.Parameters.AddWithValue("@fechaHoraProximo", sorteo.fechaHoraProximo)
                cm.Parameters.AddWithValue("@fechaHoraPrescripcion", sorteo.fechaHoraPrescripcion)
                cm.Parameters.AddWithValue("@PozoEstimadoProximo", pozo) 'IIf(sorteo.pozos.Count > 0, sorteo.pozos(0).importe, 0))
                cm.Parameters.AddWithValue("@IdPgmSorteo", sorteo.idPgmSorteo)

                cm.ExecuteNonQuery()

                vsql = " UPDATE pgmSorteo " _
                       & " set fechaHoraIniReal = @fechaHoraIniReal " _
                       & " , fechaHoraFinReal = @fechaHoraIniReal " _
                       & " WHERE idPgmConcurso = @IdPgmConcurso"

                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@fechaHoraIniReal", sorteo.fechaHoraIniReal)
                cm.Parameters.AddWithValue("@fechaHoraFinReal", sorteo.fechaHoraFinReal)
                cm.Parameters.AddWithValue("@IdPgmConcurso", sorteo.idPgmConcurso)

                cm.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                Throw New Exception(" ActualizarEstadoSorteo: " & ex.Message)
                Return False
                ''MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function
        Public Function NoTienePremiosCargados(ByVal pidPgmSorteo As Integer, ByVal pidJuego As Integer) As Boolean
            Dim vSQL As String
            Dim sTabla As String = ""

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Dim nroSorteo As Long

            Try
                nroSorteo = pidPgmSorteo Mod 1000000
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vSQL = "select * from premio p"
                vSQL = vSQL & " inner join pgmsorteo pgm on p.idjuego=pgm.idjuego"
                vSQL = vSQL & " where p.habilitado = 1 and pgm.idpgmsorteo=" & pidPgmSorteo & " and p.idjuego=" & pidJuego
                cm.CommandText = vSQL
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Close()
                    If pidJuego = 13 Then 'si es brinco tb tiene que tener los premios sueldos

                        vSQL = " select * "
                        vSQL = vSQL & " from  premio_sorteo p "
                        vSQL = vSQL & "  where p.idPgmsorteo = " & pidPgmSorteo
                        cm.CommandText = vSQL
                        dr = cm.ExecuteReader()
                        If dr.HasRows Then
                            dr.Close()

                            If nroSorteo < 1000 Then 'a partir del sorteo 1000,no se envian mas precios sueldos
                                vSQL = "select * from premio_sueldo_br"
                                vSQL = vSQL & " where idPgmsorteo =" & pidPgmSorteo
                                cm.CommandText = vSQL
                                dr = cm.ExecuteReader()
                                If Not dr.HasRows Then
                                    resultado = True
                                End If
                                dr.Close()
                            End If
                        Else
                            resultado = True
                        End If
                    Else
                        vSQL = " select * "
                        vSQL = vSQL & " from  premio_sorteo p "
                        vSQL = vSQL & "  where p.idPgmsorteo = " & pidPgmSorteo
                        cm.CommandText = vSQL
                        dr = cm.ExecuteReader()
                        If Not dr.HasRows Then
                            resultado = True
                        End If
                        dr.Close()
                    End If
                Else
                    dr.Close()
                End If
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Return True
            End Try
        End Function
        Public Function SinJurisdiccionesCargadas(ByVal pIdPgmsorteo As Integer) As Boolean
            Dim cantidad As Integer
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select * from pgmsorteo p2 where "
                vsql = vsql & " idjuego in(2,3,8,49) and "
                vsql = vsql & " p2.idpgmsorteo =" & pIdPgmsorteo
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then 'si es una quiniela,tiene que tener cargado minimo 2 loterias
                    dr.Close()
                    vsql = " select count(*) as cantidad  from extracto_qnl where "
                    vsql = vsql & " idpgmsorteo =" & pIdPgmsorteo
                    cm.CommandText = vsql
                    cantidad = cm.ExecuteScalar()
                    dr.Close()
                    If cantidad < 2 Then
                        resultado = True
                    End If
                Else
                    dr.Close()
                End If
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Return True
            End Try

        End Function
        Public Sub ConfirmarQuinielaSF(ByVal pidPgmsorteo As Integer)
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " UPDATE pgmSorteo " _
                       & " set confirmadoparcial=1" _
                       & " WHERE idPgmSorteo = " & pidPgmsorteo

                cm.CommandText = vsql
                cm.ExecuteNonQuery()
            Catch ex As Exception
                Throw (New Exception(" ConfirmarQuinielaSF: " & ex.Message))
            End Try
        End Sub

        Public Sub AnularQuinielaSF(ByVal pidPgmsorteo As Integer)
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " UPDATE pgmSorteo " _
                       & " set confirmadoparcial = 0" _
                       & " WHERE idPgmSorteo = " & pidPgmsorteo

                cm.CommandText = vsql
                cm.ExecuteNonQuery()
            Catch ex As Exception
                Throw New Exception(" AnularQuinielaSF: " & ex.Message)
            End Try
        End Sub
        Public Function NoTieneAutoridades(ByVal pIdPgmsorteo As Integer) As Boolean
            Dim cantidad As Integer
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select * from pgmsorteo_autoridad "
                vsql = vsql & " where idpgmsorteo =" & pIdPgmsorteo
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If Not dr.HasRows Then
                    resultado = True
                End If
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                Return True
            End Try

        End Function

        Public Function NoTienepozos(ByVal pIdPgmsorteo As Integer, ByVal pidConcurso As Integer) As Boolean
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                'busca pozos cargados para quini,brinco y poceada
                vsql = "  select pre.idjuego, coalesce(ad.tipo,0) as adic_tipo, pre.idmodalidad, pre.idpremio,coalesce(pgm.importe_pozo,0) as importepozo "
                vsql = vsql & " from premio pre"
                vsql = vsql & " left join (select * from pgmsorteo_pozos where idpgmsorteo = " & pIdPgmsorteo & ") pgm on pre.idpremio=pgm.idpremio "
                vsql = vsql & " left join (select * from pgmsorteo_adic where idpgmsorteo = " & pIdPgmsorteo & ") ad on ad.idpgmsorteo = pgm.idpgmsorteo "
                vsql = vsql & " where(pre.habilitado = 1)"
                vsql = vsql & " and pre.carga_pozo=1 and pre.idjuego = " & pIdPgmsorteo & " / 1000000"
                vsql = vsql & " order by pre.idpremio "
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                For Each r In dt.Rows
                    If CDbl(r("Importepozo")) = 0 Then
                        'If (r("idpremio") = "1305001" And pidConcurso = 19) Or (r("idpremio") = "1305005" And pidConcurso = 17) Or (r("idpremio") = "405001" And pidConcurso = 16) Or (r("idpremio") = "405002" And pidConcurso = 16) Or (r("idpremio") = "405005" And pidConcurso = 16) Then
                        If (r("idjuego") = 4 And r("idmodalidad") = 5 And pidConcurso = 16) Or (r("idjuego") = 13 And r("idmodalidad") = 5 And pidConcurso = 17) Or (r("idjuego") = 13 And r("idpremio") >= 1305002 And r("idpremio") <= 1305005 And pidConcurso = 19 And r("adic_tipo") = 2) Or (r("idjuego") = 4 And r("idpremio") >= 405002 And r("idpremio") <= 405005 And pidConcurso = 18 And r("adic_tipo") = 2) Then
                            resultado = False
                        Else
                            resultado = True
                            Exit For
                        End If
                    End If
                Next
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                Return True
            End Try

        End Function

        ''Public Function ExigePremios(ByVal pIdPgmsorteo As Integer, ByVal pidConcurso As Integer) As Boolean
        ''    Dim vsql As String
        ''    Dim cm As SqlCommand = New SqlCommand
        ''    Dim dr As SqlDataReader = Nothing
        ''    Dim dt As New DataTable
        ''    Dim r As DataRow
        ''    Dim resultado As Boolean
        ''    Try
        ''        resultado = False
        ''        cm.Connection = General.Obtener_Conexion
        ''        cm.CommandType = CommandType.Text
        ''        'busca pozos cargados para quini,brinco y poceada
        ''        vsql = "  select pre.idpremio,coalesce(pgm.importe_pozo,0) as importepozo "
        ''        vsql = vsql & " from premio pre"
        ''        vsql = vsql & " left join (select * from pgmsorteo_pozos where idpgmsorteo = " & pIdPgmsorteo & ") pgm on pre.idpremio=pgm.idpremio "
        ''        vsql = vsql & " where(pre.habilitado = 1)"
        ''        vsql = vsql & " and pre.carga_pozo=1 and pre.idjuego = " & pIdPgmsorteo & " / 1000000"
        ''        vsql = vsql & " order by pre.idpremio "
        ''        cm.CommandText = vsql
        ''        dr = cm.ExecuteReader()
        ''        dt.Load(dr)
        ''        dr.Close()
        ''        For Each r In dt.Rows
        ''            If CDbl(r("Importepozo")) = 0 Then
        ''                If (r("idpremio") = "1305001" And pidConcurso = 17) Or (r("idpremio") = "1305005" And pidConcurso = 17) Or (r("idpremio") = "405001" And pidConcurso = 16) Or (r("idpremio") = "405002" And pidConcurso = 16) Or (r("idpremio") = "405005" And pidConcurso = 16) Then
        ''                    resultado = False
        ''                Else
        ''                    resultado = True
        ''                    Exit For
        ''                End If
        ''            End If
        ''        Next
        ''        If Not dr.IsClosed Then dr.Close()
        ''        Return resultado
        ''    Catch ex As Exception
        ''        If Not dr Is Nothing Then
        ''            If Not dr.IsClosed Then dr.Close()
        ''        End If
        ''        Return True
        ''    End Try

        ''End Function

        Public Function NoTieneAutoridadEscribano(ByVal pIdPgmsorteo As Integer) As Boolean
            Dim cantidad As Integer
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select * from pgmsorteo_autoridad "
                vsql = vsql & " where orden=1 and idpgmsorteo =" & pIdPgmsorteo
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If Not dr.HasRows Then
                    resultado = True
                End If
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                Return True
            End Try

        End Function

        Public Function NoTieneAutoridadDelSorteo(ByVal pIdPgmsorteo As Integer) As Boolean
            Dim cantidad As Integer
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select * from "
                vsql = vsql & " (select caj.obligatorio from pgmsorteo ps "
                vsql = vsql & " inner join cargo_juego caj on caj.idjuego = ps.idjuego "
                vsql = vsql & " where ps.idpgmsorteo = " & pIdPgmsorteo
                vsql = vsql & " and caj.idcargo = 3) o left join  " ' -- 3 = Autoridad del sorteo
                vsql = vsql & " (select caj.obligatorio from pgmsorteo_autoridad pa "
                vsql = vsql & " inner join pgmsorteo ps on pa.idpgmsorteo = ps.idpgmsorteo "
                vsql = vsql & " inner join cargo_juego caj on caj.idjuego = ps.idjuego and caj.idcargo = pa.orden "
                vsql = vsql & " where pa.idpgmsorteo = " & pIdPgmsorteo
                vsql = vsql & " and pa.orden=3 ) c on 1=1 "
                vsql = vsql & " where (o.obligatorio = 1 and c.obligatorio is not null) " ' -- si es obligatorio solo tiene que devolver registro si la parte right existe
                vsql = vsql & " or (o.obligatorio =0) " ' -- si no es obligatorio siempre devolvera registro....
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If Not dr.HasRows Then
                    resultado = True
                End If
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                Return True
            End Try

        End Function

        Public Function getPgmConcursoId(ByVal idpgmsorteo As Long) As Long
            'Dim o As New PgmSorteo
            Dim idconcurso As Integer
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT distinct idpgmconcurso FROM PgmSorteo where idpgmsorteo= " & idpgmsorteo
                cm.CommandText = vsql
                idconcurso = cm.ExecuteScalar
                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                General.Cerrar_Conexion()
                Return idconcurso

            Catch ex As Exception
                dr = Nothing
                dt = Nothing
                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                Throw New Exception(ex.Message)
                Return 0
            End Try
        End Function
        '**06/11/2012
        Public Sub ActualizaProgresionLoteria(ByVal Progresion As Long, ByVal pidPgmsorteo As Long)
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " UPDATE extracto_qnl "
                vsql = vsql & " set progres=" & Progresion
                vsql = vsql & "  WHERE idPgmSorteo = " & pidPgmsorteo

                cm.CommandText = vsql
                cm.ExecuteNonQuery()
            Catch ex As Exception
                Throw New Exception(" ActualizaProgresionLoteria: " & ex.Message)
            End Try
        End Sub

        Public Function getEstadoPgmSorteo(ByVal idpgmsorteo As Long) As Integer
            'Dim o As New PgmSorteo
            Dim idestado As Integer
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT coalesce(idestadopgmconcurso,0) as estado FROM PgmSorteo where idpgmsorteo = " & idpgmsorteo
                cm.CommandText = vsql
                cm.Parameters.Clear()
                idestado = cm.ExecuteScalar

                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                General.Cerrar_Conexion()
                'Return o.idPgmSorteo
                Return idestado

            Catch ex As Exception
                dr = Nothing
                dt = Nothing
                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                Throw New Exception(ex.Message)
                Return 0
            End Try
        End Function
        '**27/11/2012
        Public Function NoTieneOtrasJurisdicciones(ByVal pIdPgmsorteo As Integer) As Boolean
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader = Nothing
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select * from PgmSorteo_Loteria "
                vsql = vsql & " where  idpgmsorteo =" & pIdPgmsorteo
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If Not dr.HasRows Then
                    resultado = True
                End If
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                Return True
            End Try

        End Function
        Public Function OtrasJurisdicciones_SinConfirmar(ByVal pIdPgmsorteo As Integer) As Boolean
            Dim cantidad As Integer
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select * from pgmsorteo p2 where "
                vsql = vsql & " idjuego in(2,3,8,49) and "
                vsql = vsql & " p2.idpgmsorteo =" & pIdPgmsorteo
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then 'si es una quiniela,tiene que tener cargado minimo 2 loterias
                    dr.Close()
                    vsql = " select count(*) as cantidad from PgmSorteo_Loteria "
                    vsql = vsql & " where  idpgmsorteo =" & pIdPgmsorteo & " and (fechahoraconf is null or revertidaparcial=1)"
                    cm.CommandText = vsql
                    cantidad = cm.ExecuteScalar()
                    dr.Close()
                    If cantidad <> 0 Then
                        resultado = True
                    End If
                Else
                    dr.Close()
                End If
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Return True
            End Try
            Try
                resultado = False
                ' las jurisdicciones confirmadas tiene fechafinreal <>null y revertidaparcial=0
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select * from PgmSorteo_Loteria "
                vsql = vsql & " where  idpgmsorteo =" & pIdPgmsorteo & " and (coalesce(fechahorafinreal,-1)=-1 or revertidaparcial=1)"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If Not dr.HasRows Then
                    resultado = True
                End If
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                Return True
            End Try

        End Function

        '**10/01/2013 obtiene os minimos asegurados(Q6 y BR)
        Function getMinimosAsegurados(ByVal idJuego As String, ByVal oExtracto As WSExtractos.Extracto) As Boolean
            Dim sSQL As String
            Dim sTabla As String = ""

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim r As DataRow

            Try

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                ' ************ obtencion de datos ******************
                sSQL = "select p.*, v.*, coalesce(v.vap_valapu,0) as vap_valapu_actual "
                sSQL = sSQL & " from MinimosAsegurados p "
                sSQL = sSQL & " left join (select j.jue_letras as idjuego, case when j.idjuego = 4 then va.idvalorapuesta else va.idmodalidad end as idmodalidad, coalesce(vap.vap_valapu,va.vap_valapu,0) as vap_valapu"
                sSQL = sSQL & " from valor_apuesta va "
                sSQL = sSQL & " inner join juego j on va.idjuego = j.idjuego"
                sSQL = sSQL & " left join valor_apuesta_sorteo vap on va.idvalorapuesta = vap.idvalorapuesta and vap.idpgmsorteo = (select idjuego from juego where jue_letras = '" & idJuego & "') * 1000000 + " & oExtracto.NumeroSorteo
                sSQL = sSQL & " where va.idjuego = (select idjuego from juego where jue_letras = '" & idJuego & "')) v on v.idjuego = p.idjuego and v.idmodalidad = p.idmodalidad"
                sSQL = sSQL & " where p.idJuego = '" & idJuego & "'"
                sSQL = sSQL & " order by p.idjuego,p.idmodalidad"

                ''sSQL = sSQL & " from MinimosAsegurados p "
                ''sSQL = sSQL & " where p.idJuego = '" & idJuego & "'"
                ''sSQL = sSQL & " order by p.idjuego,p.idmodalidad"
                cm.CommandText = sSQL
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                ' ************ carga de las listas ******************
                Dim lstMinimos As New List(Of WSExtractos.MinimoAsegurado)
                Dim oMinimo As WSExtractos.MinimoAsegurado
                If dt.Rows.Count <> 0 Then
                    oExtracto.MinimosAsegurados = New WSExtractos.MinimoAsegurado(dt.Rows.Count - 1) {}
                End If
                Dim i As Integer = 0
                For Each r In dt.Rows
                    oMinimo = New WSExtractos.MinimoAsegurado
                    oMinimo.idJuego = r("idjuego")
                    oMinimo.idModalidad = r("idmodalidad")
                    oMinimo.NroSorteodesde = r("nrosorteodesde")
                    oMinimo.Importe = r("importe")
                    oMinimo.FechaHoraCarga = r("fechahoracarga")
                    oMinimo.Usuariocargar = r("usuariocarga")
                    oMinimo.ValorApuesta = r("vap_valapu_actual")

                    lstMinimos.Add(oMinimo)
                    oExtracto.MinimosAsegurados(i) = oMinimo
                    i = i + 1
                Next

                dt.Clear()
                dt = Nothing
                Return True

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Return False
            End Try
        End Function

        '** publicacion de otras loterias al display
        Public Function publicarDisplayOtrasJurisdicciones(ByVal idPgmConcurso As Integer, ByVal idloteria As Char, Optional ByVal idPgmSorteo As Integer = -1) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim msgRet As String = ""

            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            Try
                '**** carga tablas historizas
                cm.Parameters.Clear()
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspActualizarDisplayOtrasJurisdicciones"
                cm.CommandTimeout = 30

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)
                cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idloteria", idloteria)
                cm.Parameters("@idloteria").Direction = ParameterDirection.Input

                If idPgmSorteo > 1 Then
                    cm.Parameters.AddWithValue("@idPgmConcurso", idPgmConcurso)
                    cm.Parameters("@idPgmConcurso").Direction = ParameterDirection.Input
                End If

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.ExecuteNonQuery()

                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return False
                End If

            Catch ex As Exception
                Throw New Exception(" publicarDisplayOtrasJurisdicciones: " & ex.Message)
                Return False
            End Try

            Return True
        End Function


        '** obtiene las letras de las otras jurisdicciones para publicar al display
        Public Function getLetraSorteoOtrasJurisdicciones(ByVal idPgmConcurso) As List(Of Char)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Dim listaLetras As New List(Of Char)

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select idloteria from pgmsorteo_loteria a"
                vsql = vsql & " inner join pgmsorteo b on a.idpgmsorteo=b.idpgmsorteo"
                vsql = vsql & " where(b.idpgmconcurso = " & idPgmConcurso & " And b.idestadopgmconcurso = 50)"
                vsql = vsql & " order by a.idloteria"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                For Each r As DataRow In dt.Rows
                    listaLetras.Add(r("idloteria"))
                Next
                Return listaLetras

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        '** premio sueldos
        Public Function NoTienePremiosSueldosCargados(ByVal pidPgmSorteo As Integer, ByVal pidJuego As Integer) As Boolean
            Dim vSQL As String
            Dim sTabla As String = ""

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                If pidJuego <> 13 Then
                    Return resultado
                    Exit Function
                End If
                vSQL = "select * from premio_sueldo_br"
                vSQL = vSQL & " where idPgmsorteo =" & pidPgmSorteo
                cm.CommandText = vSQL
                dr = cm.ExecuteReader()
                If Not dr.HasRows Then
                    resultado = True
                End If
                dr.Close()
                If Not dr.IsClosed Then dr.Close()
                Return resultado
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Return True
            End Try
        End Function

        Public Function getExtracccionesLocal(ByVal idJuego As Integer, ByVal idPgmSorteo As Integer, ByVal idLoteria As String, ByRef oExtracto As cExtractoArchivoBoldt) As Boolean
            Dim sSQL As String
            Dim sTabla As String = ""
            Dim sSelect As String = ""
            Dim sFrom As String = ""

            Dim cn As New SqlConnection(General.ConnString)
            Dim miCm As New SqlCommand
            Dim dr1 As SqlDataReader
            Dim draux As SqlDataReader
            miCm.Connection = cn
            Dim dt As New DataTable
            Dim r As DataRow
            Dim fecha As String

            Dim oJuegoDAL As New JuegoDAL

            Try

                miCm.Connection = General.Obtener_Conexion
                miCm.CommandType = CommandType.Text
                miCm.CommandTimeout = 60

                '** para otras jurisdicciones se tiene que controlar solo si estan confirmadas
                If oJuegoDAL.esQuiniela(idJuego) And idLoteria <> General.Jurisdiccion Then
                    sSQL = " select coalesce(fechahoraconf,'') as fecha, datepart(hour,fechahorainireal) * 100 + datepart(minute,fechahorainireal) as horaini,datepart(hour,fechahorafinreal) * 100 + datepart(minute,fechahorafinreal) as horafin from pgmsorteo_loteria where idloteria='" & idLoteria & "' and idpgmsorteo=" & idPgmSorteo
                    miCm.CommandText = sSQL
                    draux = miCm.ExecuteReader
                    If draux.HasRows Then
                        draux.Read()
                        fecha = draux("fecha").ToString.Trim
                        If InStr(fecha, "1900") <> 0 Then
                            draux.Close()
                            getExtracccionesLocal = True
                            Exit Function
                        End If
                    End If
                    draux.Close()
                End If

                ' ************ definición de la tabla de extractos ******************
                Select Case idJuego
                    Case 1, 30
                        ' 1	Tómbola
                        ' 30	Poceada Federal
                        sTabla = " extracto_tom "

                        ''Case 2, 3, 8, 49, 50, 51
                        ''    ' 2	Qnl. Nocturna
                        ''    ' 3	Qnl. Vespertina
                        ''    ' 8	Qnl. Matutina
                        ''    ' 49 El Primero


                        ''    sSelect = " coalesce(datepart(hour,fechahorainireal) * 100 + datepart(minute,fechahorainireal),'') as horaini, coalesce(datepart(hour,fechahorafinreal) * 100 + datepart(minute,pl.fechahorafinreal),'') as horafin, "
                        ''    sFrom = " inner join pgmsorteo_loteria pl on pl.idloteria='" & idLoteria & "' and pl.idpgmsorteo= p.IdPgmSorteo "
                    Case 2, 3, 8, 49, 50, 51, 26
                        ' 50 Lotería Tradic.
                        ' 51 Lotería Chica
                        sTabla = " extracto_qnl "
                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        sTabla = " extracto_qn6 "
                End Select
                ' ************ obtención de datos ******************
                sSQL = " select " & sSelect & " e.* from PgmSorteo p " & _
                      " inner join " & sTabla & " e on p.IdPgmSorteo = e.IdPgmSorteo " & _
                        " where p.IdPgmSorteo = @IdPgmSorteo and p.idJuego = @idJuego and e.idLoteria = @idLoteria "
                ''sSQL = " select " & sSelect & " e.* from PgmSorteo p " & _
                ''      " inner join " & sTabla & " e on p.IdPgmSorteo = e.IdPgmSorteo " & _
                ''      sFrom & _
                ''      " where p.IdPgmSorteo = @IdPgmSorteo and p.idJuego = @idJuego and e.idLoteria = @idLoteria "
                miCm.CommandText = sSQL
                miCm.Parameters.Clear()
                miCm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                miCm.Parameters.AddWithValue("@idJuego", idJuego)
                miCm.Parameters.AddWithValue("@idLoteria", idLoteria)

                dr1 = miCm.ExecuteReader()
                dt.Load(dr1)
                dr1.Close()
                'dt.Load(miCm.ExecuteReader)
                miCm.Parameters.Clear()
                miCm.Connection.Close()
                miCm.Connection = Nothing
                miCm.Dispose()
                If cn.State <> ConnectionState.Closed Then cn.Close()
                cn = Nothing
                SqlConnection.ClearAllPools()
                'dr1.Close()
                r = dt.Rows(0)

                oExtracto.Loteria = r("idLoteria")

                Select Case idJuego
                    Case 1, 30 ' 1	Tómbola
                        ' 30	Poceada Federal
                        oExtracto.Cifras = 2
                        If idJuego = 1 Then
                            oExtracto.Juego = "TM"
                        Else
                            oExtracto.Juego = "PF"
                        End If

                        oExtracto.Numero_1.Valor = IIf(r("nro_tom_poc_1").ToString.Trim.Length = 0, "0", r("nro_tom_poc_1"))
                        oExtracto.Numero_2.Valor = IIf(r("nro_tom_poc_2").ToString.Trim.Length = 0, "0", r("nro_tom_poc_2"))
                        oExtracto.Numero_3.Valor = IIf(r("nro_tom_poc_3").ToString.Trim.Length = 0, "0", r("nro_tom_poc_3"))
                        oExtracto.Numero_4.Valor = IIf(r("nro_tom_poc_4").ToString.Trim.Length = 0, "0", r("nro_tom_poc_4"))
                        oExtracto.Numero_5.Valor = IIf(r("nro_tom_poc_5").ToString.Trim.Length = 0, "0", r("nro_tom_poc_5"))
                        oExtracto.Numero_6.Valor = IIf(r("nro_tom_poc_6").ToString.Trim.Length = 0, "0", r("nro_tom_poc_6"))
                        oExtracto.Numero_7.Valor = IIf(r("nro_tom_poc_7").ToString.Trim.Length = 0, "0", r("nro_tom_poc_7"))
                        oExtracto.Numero_8.Valor = IIf(r("nro_tom_poc_8").ToString.Trim.Length = 0, "0", r("nro_tom_poc_8"))
                        oExtracto.Numero_9.Valor = IIf(r("nro_tom_poc_9").ToString.Trim.Length = 0, "0", r("nro_tom_poc_9"))
                        oExtracto.Numero_10.Valor = IIf(r("nro_tom_poc_10").ToString.Trim.Length = 0, "0", r("nro_tom_poc_10"))
                        oExtracto.Numero_11.Valor = IIf(r("nro_tom_poc_11").ToString.Trim.Length = 0, "0", r("nro_tom_poc_11"))
                        oExtracto.Numero_12.Valor = IIf(r("nro_tom_poc_12").ToString.Trim.Length = 0, "0", r("nro_tom_poc_12"))
                        oExtracto.Numero_13.Valor = IIf(r("nro_tom_poc_13").ToString.Trim.Length = 0, "0", r("nro_tom_poc_13"))
                        oExtracto.Numero_14.Valor = IIf(r("nro_tom_poc_14").ToString.Trim.Length = 0, "0", r("nro_tom_poc_14"))
                        oExtracto.Numero_15.Valor = IIf(r("nro_tom_poc_15").ToString.Trim.Length = 0, "0", r("nro_tom_poc_15"))
                        oExtracto.Numero_16.Valor = IIf(r("nro_tom_poc_16").ToString.Trim.Length = 0, "0", r("nro_tom_poc_16"))
                        oExtracto.Numero_17.Valor = IIf(r("nro_tom_poc_17").ToString.Trim.Length = 0, "0", r("nro_tom_poc_17"))
                        oExtracto.Numero_18.Valor = IIf(r("nro_tom_poc_18").ToString.Trim.Length = 0, "0", r("nro_tom_poc_18"))
                        oExtracto.Numero_19.Valor = IIf(r("nro_tom_poc_19").ToString.Trim.Length = 0, "0", r("nro_tom_poc_19"))
                        oExtracto.Numero_20.Valor = IIf(r("nro_tom_poc_20").ToString.Trim.Length = 0, "0", r("nro_tom_poc_20"))
                        oExtracto.Numero_21.Valor = 0
                        oExtracto.Numero_22.Valor = 0
                        oExtracto.Numero_23.Valor = 0
                        oExtracto.Numero_24.Valor = 0
                        oExtracto.Numero_25.Valor = 0
                        oExtracto.Numero_26.Valor = 0
                        oExtracto.Numero_27.Valor = 0
                        oExtracto.Numero_28.Valor = 0
                        oExtracto.Numero_29.Valor = 0
                        oExtracto.Numero_30.Valor = 0
                        oExtracto.Numero_31.Valor = 0
                        oExtracto.Numero_32.Valor = 0
                        oExtracto.Numero_33.Valor = 0
                        oExtracto.Numero_34.Valor = 0
                        oExtracto.Numero_35.Valor = 0
                        oExtracto.Numero_36.Valor = 0

                        

                    Case 2, 3, 8, 49, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 26 El Ultimo
                        oExtracto.Cifras = r("cifras")
                        Select Case idJuego
                            Case 2
                                oExtracto.Juego = "N"
                            Case 3
                                oExtracto.Juego = "V"
                            Case 8
                                oExtracto.Juego = "M"
                            Case 49
                                oExtracto.Juego = "P"
                            Case 26
                                oExtracto.Juego = "U"
                        End Select


                        oExtracto.Numero_1.Valor = IIf(r("Nro1T").ToString.Trim.Length = 0, "0", r("Nro1T"))
                        oExtracto.Numero_2.Valor = IIf(r("Nro2T").ToString.Trim.Length = 0, "0", r("Nro2T"))
                        oExtracto.Numero_3.Valor = IIf(r("Nro3T").ToString.Trim.Length = 0, "0", r("Nro3T"))
                        oExtracto.Numero_4.Valor = IIf(r("Nro4T").ToString.Trim.Length = 0, "0", r("Nro4T"))
                        oExtracto.Numero_5.Valor = IIf(r("Nro5T").ToString.Trim.Length = 0, "0", r("Nro5T"))
                        oExtracto.Numero_6.Valor = IIf(r("Nro6T").ToString.Trim.Length = 0, "0", r("Nro6T"))
                        oExtracto.Numero_7.Valor = IIf(r("Nro7T").ToString.Trim.Length = 0, "0", r("Nro7T"))
                        oExtracto.Numero_8.Valor = IIf(r("Nro8T").ToString.Trim.Length = 0, "0", r("Nro8T"))
                        oExtracto.Numero_9.Valor = IIf(r("Nro9T").ToString.Trim.Length = 0, "0", r("Nro9T"))
                        oExtracto.Numero_10.Valor = IIf(r("Nro10T").ToString.Trim.Length = 0, "0", r("Nro10T"))
                        oExtracto.Numero_11.Valor = IIf(r("Nro11T").ToString.Trim.Length = 0, "0", r("Nro11T"))
                        oExtracto.Numero_12.Valor = IIf(r("Nro12T").ToString.Trim.Length = 0, "0", r("Nro12T"))
                        oExtracto.Numero_13.Valor = IIf(r("Nro13T").ToString.Trim.Length = 0, "0", r("Nro13T"))
                        oExtracto.Numero_14.Valor = IIf(r("Nro14T").ToString.Trim.Length = 0, "0", r("Nro14T"))
                        oExtracto.Numero_15.Valor = IIf(r("Nro15T").ToString.Trim.Length = 0, "0", r("Nro15T"))
                        oExtracto.Numero_16.Valor = IIf(r("Nro16T").ToString.Trim.Length = 0, "0", r("Nro16T"))
                        oExtracto.Numero_17.Valor = IIf(r("Nro17T").ToString.Trim.Length = 0, "0", r("Nro17T"))
                        oExtracto.Numero_18.Valor = IIf(r("Nro18T").ToString.Trim.Length = 0, "0", r("Nro18T"))
                        oExtracto.Numero_19.Valor = IIf(r("Nro19T").ToString.Trim.Length = 0, "0", r("Nro19T"))
                        oExtracto.Numero_20.Valor = IIf(r("Nro20T").ToString.Trim.Length = 0, "0", r("Nro20T"))
                        oExtracto.Numero_21.Valor = 0
                        oExtracto.Numero_22.Valor = 0
                        oExtracto.Numero_23.Valor = 0
                        oExtracto.Numero_24.Valor = 0
                        oExtracto.Numero_25.Valor = 0
                        oExtracto.Numero_26.Valor = 0
                        oExtracto.Numero_27.Valor = 0
                        oExtracto.Numero_28.Valor = 0
                        oExtracto.Numero_29.Valor = 0
                        oExtracto.Numero_30.Valor = 0
                        oExtracto.Numero_31.Valor = 0
                        oExtracto.Numero_32.Valor = 0
                        oExtracto.Numero_33.Valor = 0
                        oExtracto.Numero_34.Valor = 0
                        oExtracto.Numero_35.Valor = 0
                        oExtracto.Numero_36.Valor = 0

                        
                    Case 50
                        ' 50	Lotería Tradic.
                        oExtracto.Cifras = r("cifras")
                        oExtracto.Juego = "LO"

                        oExtracto.Numero_1.Valor = IIf(r("Nro1T").ToString.Trim.Length = 0, "0", r("Nro1T"))
                        oExtracto.Numero_2.Valor = IIf(r("Nro2T").ToString.Trim.Length = 0, "0", r("Nro2T"))
                        oExtracto.Numero_3.Valor = IIf(r("Nro3T").ToString.Trim.Length = 0, "0", r("Nro3T"))
                        oExtracto.Numero_4.Valor = IIf(r("Nro4T").ToString.Trim.Length = 0, "0", r("Nro4T"))
                        oExtracto.Numero_5.Valor = IIf(r("Nro5T").ToString.Trim.Length = 0, "0", r("Nro5T"))
                        oExtracto.Numero_6.Valor = IIf(r("Nro6T").ToString.Trim.Length = 0, "0", r("Nro6T"))
                        oExtracto.Numero_7.Valor = IIf(r("Nro7T").ToString.Trim.Length = 0, "0", r("Nro7T"))
                        oExtracto.Numero_8.Valor = IIf(r("Nro8T").ToString.Trim.Length = 0, "0", r("Nro8T"))
                        oExtracto.Numero_9.Valor = IIf(r("Nro9T").ToString.Trim.Length = 0, "0", r("Nro9T"))
                        oExtracto.Numero_10.Valor = IIf(r("Nro10T").ToString.Trim.Length = 0, "0", r("Nro10T"))
                        oExtracto.Numero_11.Valor = IIf(r("Nro11T").ToString.Trim.Length = 0, "0", r("Nro11T"))
                        oExtracto.Numero_12.Valor = IIf(r("Nro12T").ToString.Trim.Length = 0, "0", r("Nro12T"))
                        oExtracto.Numero_13.Valor = IIf(r("Nro13T").ToString.Trim.Length = 0, "0", r("Nro13T"))
                        oExtracto.Numero_14.Valor = IIf(r("Nro14T").ToString.Trim.Length = 0, "0", r("Nro14T"))
                        oExtracto.Numero_15.Valor = IIf(r("Nro15T").ToString.Trim.Length = 0, "0", r("Nro15T"))
                        oExtracto.Numero_16.Valor = IIf(r("Nro16T").ToString.Trim.Length = 0, "0", r("Nro16T"))
                        oExtracto.Numero_17.Valor = IIf(r("Nro17T").ToString.Trim.Length = 0, "0", r("Nro17T"))
                        oExtracto.Numero_18.Valor = IIf(r("Nro18T").ToString.Trim.Length = 0, "0", r("Nro18T"))
                        oExtracto.Numero_19.Valor = IIf(r("Nro19T").ToString.Trim.Length = 0, "0", r("Nro19T"))
                        oExtracto.Numero_20.Valor = IIf(r("Nro20T").ToString.Trim.Length = 0, "0", r("Nro20T"))
                        oExtracto.Numero_21.Valor = IIf(r("progres").ToString.Trim.Length = 0, "0", r("progres"))
                        oExtracto.Numero_22.Valor = IIf(r("extrac_lote_1").ToString.Trim.Length = 0, "00000", r("extrac_lote_1").ToString.PadLeft(5, "0")) 'formatea a 5 cifras con ceros a la izquierda
                        oExtracto.Numero_23.Valor = IIf(r("extrac_lote_2").ToString.Trim.Length = 0, "00000", r("extrac_lote_2").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_24.Valor = IIf(r("extrac_lote_3").ToString.Trim.Length = 0, "00000", r("extrac_lote_3").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_25.Valor = IIf(r("extrac_lote_4").ToString.Trim.Length = 0, "00000", r("extrac_lote_4").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_26.Valor = IIf(r("extrac_lote_5").ToString.Trim.Length = 0, "00000", r("extrac_lote_5").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_27.Valor = IIf(r("extrac_lote_6").ToString.Trim.Length = 0, "00000", r("extrac_lote_6").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_28.Valor = IIf(r("extrac_lote_7").ToString.Trim.Length = 0, "00000", r("extrac_lote_7").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_29.Valor = IIf(r("extrac_lote_8").ToString.Trim.Length = 0, "00000", r("extrac_lote_8").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_30.Valor = IIf(r("extrac_lote_9").ToString.Trim.Length = 0, "00000", r("extrac_lote_9").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_31.Valor = IIf(r("extrac_lote_10").ToString.Trim.Length = 0, "00000", r("extrac_lote_10").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_32.Valor = IIf(r("extrac_lote_11").ToString.Trim.Length = 0, "00000", r("extrac_lote_11").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_33.Valor = IIf(r("extrac_lote_12").ToString.Trim.Length = 0, "00000", r("extrac_lote_12").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_34.Valor = IIf(r("extrac_lote_13").ToString.Trim.Length = 0, "00000", r("extrac_lote_13").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_35.Valor = IIf(r("extrac_lote_14").ToString.Trim.Length = 0, "00000", r("extrac_lote_14").ToString.PadLeft(5, "0"))
                        oExtracto.Numero_36.Valor = IIf(r("extrac_lote_15").ToString.Trim.Length = 0, "00000", r("extrac_lote_15").ToString.PadLeft(5, "0"))

                        


                    Case 51
                        ' 51	Lotería Chica
                        oExtracto.Cifras = r("cifras")
                        oExtracto.Juego = "LC"

                        oExtracto.Numero_1.Valor = IIf(r("Nro1T").ToString.Trim.Length = 0, "0", Mid(r("Nro1T"), 1))
                        oExtracto.Numero_2.Valor = IIf(r("Nro2T").ToString.Trim.Length = 0, "0", Mid(r("Nro2T"), 1))
                        oExtracto.Numero_3.Valor = IIf(r("Nro3T").ToString.Trim.Length = 0, "0", Mid(r("Nro3T"), 1))
                        oExtracto.Numero_4.Valor = IIf(r("Nro4T").ToString.Trim.Length = 0, "0", Mid(r("Nro4T"), 1))
                        oExtracto.Numero_5.Valor = IIf(r("Nro5T").ToString.Trim.Length = 0, "0", Mid(r("Nro5T"), 1))
                        oExtracto.Numero_6.Valor = 0
                        oExtracto.Numero_7.Valor = 0
                        oExtracto.Numero_8.Valor = 0
                        oExtracto.Numero_9.Valor = 0
                        oExtracto.Numero_10.Valor = 0
                        oExtracto.Numero_11.Valor = 0
                        oExtracto.Numero_12.Valor = 0
                        oExtracto.Numero_13.Valor = 0
                        oExtracto.Numero_14.Valor = 0
                        oExtracto.Numero_15.Valor = 0
                        oExtracto.Numero_16.Valor = 0
                        oExtracto.Numero_17.Valor = 0
                        oExtracto.Numero_18.Valor = 0
                        oExtracto.Numero_19.Valor = 0
                        oExtracto.Numero_20.Valor = 0
                        oExtracto.Numero_21.Valor = 0
                        oExtracto.Numero_21.Valor = 0
                        oExtracto.Numero_22.Valor = 0
                        oExtracto.Numero_23.Valor = 0
                        oExtracto.Numero_24.Valor = 0
                        oExtracto.Numero_25.Valor = 0
                        oExtracto.Numero_26.Valor = 0
                        oExtracto.Numero_27.Valor = 0
                        oExtracto.Numero_28.Valor = 0
                        oExtracto.Numero_29.Valor = 0
                        oExtracto.Numero_30.Valor = 0
                        oExtracto.Numero_31.Valor = 0
                        oExtracto.Numero_32.Valor = 0
                        oExtracto.Numero_33.Valor = 0
                        oExtracto.Numero_34.Valor = 0
                        oExtracto.Numero_35.Valor = 0
                        oExtracto.Numero_36.Valor = 0

                        
                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        oExtracto.Cifras = 0
                        Select Case idJuego
                            Case 4
                                oExtracto.Juego = "Q2"
                            Case 13
                                oExtracto.Juego = "BR"
                        End Select

                        oExtracto.Numero_1.Valor = IIf(r("nro_qn6_bri_1").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_1"))
                        oExtracto.Numero_2.Valor = IIf(r("nro_qn6_bri_2").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_2"))
                        oExtracto.Numero_3.Valor = IIf(r("nro_qn6_bri_3").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_3"))
                        oExtracto.Numero_4.Valor = IIf(r("nro_qn6_bri_4").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_4"))
                        oExtracto.Numero_5.Valor = IIf(r("nro_qn6_bri_5").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_5"))
                        oExtracto.Numero_6.Valor = IIf(r("nro_qn6_bri_6").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_6"))
                        oExtracto.Numero_7.Valor = IIf(r("nro_qn6_bri_7").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_7"))
                        oExtracto.Numero_8.Valor = IIf(r("nro_qn6_bri_8").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_8"))
                        oExtracto.Numero_9.Valor = IIf(r("nro_qn6_bri_9").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_9"))
                        oExtracto.Numero_10.Valor = IIf(r("nro_qn6_bri_10").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_10"))
                        oExtracto.Numero_11.Valor = IIf(r("nro_qn6_bri_11").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_11"))
                        oExtracto.Numero_12.Valor = IIf(r("nro_qn6_bri_12").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_12"))
                        oExtracto.Numero_13.Valor = IIf(r("nro_qn6_bri_13").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_13"))
                        oExtracto.Numero_14.Valor = IIf(r("nro_qn6_bri_14").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_14"))
                        oExtracto.Numero_15.Valor = IIf(r("nro_qn6_bri_15").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_15"))
                        oExtracto.Numero_16.Valor = IIf(r("nro_qn6_bri_16").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_16"))
                        oExtracto.Numero_17.Valor = IIf(r("nro_qn6_bri_17").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_17"))
                        oExtracto.Numero_18.Valor = IIf(r("nro_qn6_bri_18").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_18"))
                        oExtracto.Numero_19.Valor = IIf(r("nro_qn6_bri_19").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_19"))
                        oExtracto.Numero_20.Valor = IIf(r("nro_qn6_bri_20").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_20"))
                        oExtracto.Numero_21.Valor = IIf(r("nro_qn6_bri_21").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_21"))
                        oExtracto.Numero_22.Valor = IIf(r("nro_qn6_bri_22").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_22"))
                        oExtracto.Numero_23.Valor = IIf(r("nro_qn6_bri_23").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_23"))
                        oExtracto.Numero_24.Valor = IIf(r("nro_qn6_bri_24").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_24"))
                        oExtracto.Numero_25.Valor = IIf(r("nro_qn6_bri_adi_1").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_1"))
                        oExtracto.Numero_26.Valor = IIf(r("nro_qn6_bri_adi_2").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_2"))
                        oExtracto.Numero_27.Valor = IIf(r("nro_qn6_bri_adi_3").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_3"))
                        oExtracto.Numero_28.Valor = IIf(r("nro_qn6_bri_adi_4").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_4"))
                        oExtracto.Numero_29.Valor = IIf(r("nro_qn6_bri_adi_5").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_5"))
                        oExtracto.Numero_30.Valor = IIf(r("nro_qn6_bri_adi_6").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_6"))
                        oExtracto.Numero_31.Valor = IIf(r("nro_qn6_bri_adi_7").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_7"))
                        oExtracto.Numero_32.Valor = IIf(r("nro_qn6_bri_adi_8").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_8"))
                        oExtracto.Numero_33.Valor = IIf(r("nro_qn6_bri_adi_9").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_9"))
                        oExtracto.Numero_34.Valor = IIf(r("nro_qn6_bri_adi_10").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_10"))
                        oExtracto.Numero_35.Valor = IIf(r("nro_qn6_bri_adi_11").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_11"))
                        oExtracto.Numero_36.Valor = IIf(r("nro_qn6_bri_adi_12").ToString.Trim.Length = 0, "0", r("nro_qn6_bri_adi_12"))

                        
                End Select


                miCm = Nothing
                r = Nothing
                dt = Nothing

                Return True

            Catch ex As Exception
                r = Nothing
                dt = Nothing
                miCm = Nothing
                Return False
            End Try
        End Function

        Public Function CantidadJurisdicciones(ByVal pIdPgmsorteo As Integer) As Integer
            Dim cantidad As Integer = 0
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim r As DataRow
            Dim resultado As Boolean
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                vsql = " select * from pgmsorteo p2 where "
                vsql = vsql & " idjuego in(2,3,8,49) and "
                vsql = vsql & " p2.idpgmsorteo =" & pIdPgmsorteo
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                If dr.HasRows Then 'si es una quiniela,tiene que tener cargado minimo 2 loterias
                    dr.Close()
                    vsql = " select count(*) as cantidad from PgmSorteo_Loteria "
                    vsql = vsql & " where  idpgmsorteo =" & pIdPgmsorteo
                    cm.CommandText = vsql
                    cantidad = cm.ExecuteScalar()
                    dr.Close()

                Else
                    dr.Close()
                End If
                If Not dr.IsClosed Then dr.Close()
                Return cantidad
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Return cantidad
            End Try
        End Function

        Public Function CrearProgramaSorteo() As Boolean
            Dim cm As SqlCommand = New SqlCommand

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspCrearPgmConcursos"
                cm.ExecuteNonQuery()
                Return True

            Catch ex As Exception
                FileSystemHelper.Log("crearprogramasorteo:" & ex.Message)
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


        Public Function SalioPrimer_premio(ByVal pIdPgmsorteo As Integer, ByVal idJuego As Integer) As Boolean

            Dim Cantidad As Integer = 0
            Dim vsql As String
            Dim cm As SqlCommand = New SqlCommand
            Dim resultado As Boolean
            Dim _where As String = ""
            Try
                resultado = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text


                If idJuego = 4 Or idJuego = 13 Then
                    _where = " AND cant_aciertos=6"
                Else
                    _where = " AND idpremio=3001001"
                End If

                vsql = " select count(*) as cantidad from premio_sorteo  "
                vsql = vsql & " where vacante=0 and  idpgmsorteo =" & pIdPgmsorteo & _where
                cm.CommandText = vsql
                Cantidad = cm.ExecuteScalar()
                If Cantidad > 0 Then
                    resultado = True
                End If

                SalioPrimer_premio = resultado
            Catch ex As Exception

                Return resultado
            End Try
        End Function

        Public Function getExtRest(ByVal IdPgmsorteo As Integer, ByVal idLoteria As String, Optional ByVal forzarOffline As Boolean = False) As DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim sExt As String = ""
            Dim msgRet As String = ""
            Dim strForzarOffline As String = ""

            If forzarOffline Then
                strForzarOffline = "S"
            Else
                strForzarOffline = "N"
            End If

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetExtRest"

                cm.Parameters.AddWithValue("@idpgmSorteo", IdPgmsorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@idloteria", idLoteria)
                cm.Parameters("@idLoteria").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.Parameters.AddWithValue("@forzarOffline", strForzarOffline)
                cm.Parameters("@forzarOffline").Direction = ParameterDirection.Input

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return Nothing
                End If
                Return dt

            Catch ex As Exception
                FileSystemHelper.Log("getExtRest:" & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPremiosRest(ByVal IdPgmsorteo As Integer) As DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim msgRet As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetPremiosRest"

                cm.Parameters.AddWithValue("@idPgmSorteo", IdPgmsorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output


                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return Nothing
                End If

                Return dt

            Catch ex As Exception
                FileSystemHelper.Log("getPremiosRest:" & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getSueldosRest(ByVal IdPgmsorteo As Integer) As DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim msgRet As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetSueldosRest"

                cm.Parameters.AddWithValue("@idPgmSorteo", IdPgmsorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return Nothing
                End If

                Return dt

            Catch ex As Exception
                FileSystemHelper.Log("getSueldosRest:" & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getMinAsegRest(ByVal IdPgmsorteo As Integer) As DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim msgRet As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetMinAsegRest"

                cm.Parameters.AddWithValue("@idPgmSorteo", IdPgmsorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return Nothing
                End If

                Return dt

            Catch ex As Exception
                FileSystemHelper.Log("getMinAsegRest:" & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getValApuRest(ByVal IdPgmsorteo As Integer) As DataTable
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim sExt As String = ""
            Dim msgRet As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetValApuRest"

                cm.Parameters.AddWithValue("@idPgmSorteo", IdPgmsorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return Nothing
                End If

                Return dt

            Catch ex As Exception
                FileSystemHelper.Log("getMinAsegRest:" & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        '**06/11/2012
        Public Function RecuperaProgresionLoteria(ByVal pidPgmsorteo As Long) As Integer
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Dim Progresion As Integer
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " select coalesce(progres,0) progres from  extracto_qnl "
                vsql = vsql & "  WHERE idPgmSorteo = " & pidPgmsorteo
                cm.CommandText = vsql
                Progresion = cm.ExecuteScalar
                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                General.Cerrar_Conexion()
                'Return o.idPgmSorteo
                Return Progresion
            Catch ex As Exception
                cm.Connection.Close()
                cm.Connection = Nothing
                cm.Dispose()
                cm = Nothing
                Return 0
                Throw New Exception(" recuperaProgresionLoteria: " & ex.Message)
            End Try
        End Function

        Public Function exigirPdfPrimerPremio(ByVal idPgmSorteo As Integer) As Boolean
            Dim msgRet As String = ""
            Dim e As String = ""

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspExigirPdfPrimerPremio"

                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@exigir", SqlDbType.Char, 1))
                cm.Parameters("@exigir").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 255))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return False
                End If
                e = cm.Parameters("@exigir").Value

            Catch ex As Exception
                FileSystemHelper.Log("PgmSorteoDal.exigirPdfPrimerPremio: " & ex.Message)
                Throw New Exception(ex.Message)
                Return False
            End Try

            Return (e = "S")
        End Function

        Public Function enviarPdfPrimerPremio(ByVal idPgmSorteo As Integer) As Boolean
            Dim msgRet As String = ""
            Dim e As String = ""

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspEnviarPdfPrimerPremio"

                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@enviar", SqlDbType.Char, 1))
                cm.Parameters("@enviar").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 255))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return False
                End If
                e = cm.Parameters("@enviar").Value

            Catch ex As Exception
                FileSystemHelper.Log("PgmSorteoDal.exigirPdfPrimerPremio: " & ex.Message)
                Throw New Exception(ex.Message)
                Return False
            End Try

            Return (e = "S")
        End Function

        Public Function exigirPdfDistribPcias(ByVal idPgmSorteo As Integer) As Boolean
            Dim msgRet As String = ""
            Dim e As String = ""

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspExigirPdfDistribPcias"

                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@exigir", SqlDbType.Char, 1))
                cm.Parameters("@exigir").Direction = ParameterDirection.Output

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 255))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                msgRet = cm.Parameters("@msgRet").Value
                If msgRet <> "" Then
                    Throw New Exception(msgRet)
                    Return False
                End If
                e = cm.Parameters("@exigir").Value

            Catch ex As Exception
                FileSystemHelper.Log("PgmSorteoDal.exigirPdfDistribPcias: " & ex.Message)
                Throw New Exception(ex.Message)
                Return False
            End Try

            Return (e = "S")
        End Function

        Public Function setParProxPozoConfirmado(ByRef oSorteo As PgmSorteo, ByVal estado As Boolean) As Boolean
            Dim res As Boolean = True
            Dim cm As SqlCommand = New SqlCommand

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspConfirmarParProxPozo"

                cm.Parameters.AddWithValue("@idPgmSorteo", oSorteo.idPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@estado", estado)
                cm.Parameters("@estado").Direction = ParameterDirection.Input

                cm.ExecuteNonQuery()

            Catch ex As Exception
                FileSystemHelper.Log("setParProxPozoConfirmado - Excepción: " & ex.Message)
                Throw New Exception("setParProxPozoConfirmado - Excepción: " & ex.Message)
                res = False
            End Try
            Return res
        End Function
    End Class

End Namespace