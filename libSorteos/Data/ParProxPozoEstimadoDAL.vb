Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Data

    Public Class ParProxPozoEstimadoDAL
        Public Function getParProxPozoEstimado(ByVal idPgmSorteo As Integer) As ParProxPozoEstimado
            Dim oPar As New ParProxPozoEstimado
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim msgRet As String = ""
            Dim ret As Integer = 0

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspGetParProxPozoEstimado"

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.ExecuteNonQuery()
                msgRet = cm.Parameters("@msgRet").Value
                ret = cm.Parameters("@retorno").Value

                If (ret <> 0) OrElse (msgRet <> "") Then
                    Throw New Exception("getParProxPozoEstimado - Excepción: " & msgRet)
                    oPar = Nothing
                Else
                    dr = cm.ExecuteReader()
                    If dr.HasRows Then
                        dt.Load(dr)
                        dr.Close()
                        For Each r As DataRow In dt.Rows ' trae siempre uno pero es una forma general de obtener el datarow...
                            Load(oPar, r)
                        Next
                    Else
                        oPar = Nothing
                    End If
                End If
            Catch ex As Exception
                Throw New Exception("getParProxPozoEstimado - Excepción: " & ex.Message)
                oPar = Nothing
            End Try
            Return oPar
        End Function
        Public Function setParProxPozoEstimado(ByVal oPar As ParProxPozoEstimado) As Boolean
            Dim res As Boolean

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim msgRet As String = ""
            Dim ret As Integer = 0

            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.StoredProcedure
                cm.CommandText = "uspSetParProxPozoEstimado"

                cm.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
                cm.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

                cm.Parameters.AddWithValue("@idPgmSorteo", oPar.IdPgmSorteo)
                cm.Parameters("@idPgmSorteo").Direction = ParameterDirection.Input

                ' parametros a actualizar...
                cm.Parameters.AddWithValue("@Porc_var_apuestas_miercoles_domingos", oPar.Porc_var_apuestas_miercoles_domingos)
                cm.Parameters("@Porc_var_apuestas_miercoles_domingos").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@Porc_var_apuestas_domingos_miercoles", oPar.Porc_var_apuestas_domingos_miercoles)
                cm.Parameters("@Porc_var_apuestas_domingos_miercoles").Direction = ParameterDirection.Input

                cm.Parameters.AddWithValue("@Pozo_premio_extra", oPar.Pozo_premio_extra)
                cm.Parameters("@Pozo_premio_extra").Direction = ParameterDirection.Input ' Importe previsto como pozo para premio extra

                cm.Parameters.AddWithValue("@Pozo_sorteo_adicional", oPar.Pozo_sorteo_adicional)
                cm.Parameters("@Pozo_sorteo_adicional").Direction = ParameterDirection.Input ' Importe previsto como pozo para MODALIDAD ADICIONAL, cuando hay
                '
                cm.Parameters.AddWithValue("@Cant_apuestas_estimadas", oPar.Cant_apuestas_estimadas)
                cm.Parameters("@Cant_apuestas_estimadas").Direction = ParameterDirection.Input ' cant apuestas
                '
                cm.Parameters.AddWithValue("@Porc_dist_rec_1", oPar.Porc_dist_rec_1)
                cm.Parameters("@Porc_dist_rec_1").Direction = ParameterDirection.Input ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad

                cm.Parameters.AddWithValue("@Porc_dist_rec_reserva_1", oPar.Porc_dist_rec_reserva_1)
                cm.Parameters("@Porc_dist_rec_reserva_1").Direction = ParameterDirection.Input ' % de la rec de la modalidad, que se toma para asignar a la reserva

                cm.Parameters.AddWithValue("@Valor_apuesta_1", oPar.Valor_apuesta_1)
                cm.Parameters("@Valor_apuesta_1").Direction = ParameterDirection.Input ' Precio de venta de la modalidad tradicional+segunda

                cm.Parameters.AddWithValue("@Porc_valor_apuesta_1", oPar.Porc_valor_apuesta_1)
                cm.Parameters("@Porc_valor_apuesta_1").Direction = ParameterDirection.Input ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo

                cm.Parameters.AddWithValue("@Porc_cant_apuesta_1", oPar.Porc_cant_apuesta_1)
                cm.Parameters("@Porc_cant_apuesta_1").Direction = ParameterDirection.Input ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad

                cm.Parameters.AddWithValue("@Minimo_asegurado_1", oPar.Minimo_asegurado_1)
                cm.Parameters("@Minimo_asegurado_1").Direction = ParameterDirection.Input ' Importe mínimo asegurado para esta modalidad en el prox sorteo
                '
                cm.Parameters.AddWithValue("@Porc_dist_rec_2", oPar.Porc_dist_rec_2)
                cm.Parameters("@Porc_dist_rec_2").Direction = ParameterDirection.Input ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad

                cm.Parameters.AddWithValue("@Porc_dist_rec_reserva_2", oPar.Porc_dist_rec_reserva_2)
                cm.Parameters("@Porc_dist_rec_reserva_2").Direction = ParameterDirection.Input ' % de la rec de la modalidad, que se toma para asignar a la reserva

                cm.Parameters.AddWithValue("@Valor_apuesta_2", oPar.Valor_apuesta_2)
                cm.Parameters("@Valor_apuesta_2").Direction = ParameterDirection.Input ' Precio de venta de la modalidad tradicional+segunda

                cm.Parameters.AddWithValue("@Porc_valor_apuesta_2", oPar.Porc_valor_apuesta_2)
                cm.Parameters("@Porc_valor_apuesta_2").Direction = ParameterDirection.Input ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo

                cm.Parameters.AddWithValue("@Porc_cant_apuesta_2", oPar.Porc_cant_apuesta_2)
                cm.Parameters("@Porc_cant_apuesta_2").Direction = ParameterDirection.Input ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad

                cm.Parameters.AddWithValue("@Minimo_asegurado_2", oPar.Minimo_asegurado_2)
                cm.Parameters("@Minimo_asegurado_2").Direction = ParameterDirection.Input ' Importe mínimo asegurado para esta modalidad en el prox sorteo
                '
                cm.Parameters.AddWithValue("@Porc_dist_rec_3", oPar.Porc_dist_rec_3)
                cm.Parameters("@Porc_dist_rec_3").Direction = ParameterDirection.Input ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad

                cm.Parameters.AddWithValue("@Porc_dist_rec_reserva_3", oPar.Porc_dist_rec_reserva_3)
                cm.Parameters("@Porc_dist_rec_reserva_3").Direction = ParameterDirection.Input ' % de la rec de la modalidad, que se toma para asignar a la reserva

                cm.Parameters.AddWithValue("@Valor_apuesta_3", oPar.Valor_apuesta_3)
                cm.Parameters("@Valor_apuesta_3").Direction = ParameterDirection.Input ' Precio de venta de la modalidad tradicional+segunda

                cm.Parameters.AddWithValue("@Porc_valor_apuesta_3", oPar.Porc_valor_apuesta_3)
                cm.Parameters("@Porc_valor_apuesta_3").Direction = ParameterDirection.Input ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo

                cm.Parameters.AddWithValue("@Porc_cant_apuesta_3", oPar.Porc_cant_apuesta_3)
                cm.Parameters("@Porc_cant_apuesta_3").Direction = ParameterDirection.Input ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad

                cm.Parameters.AddWithValue("@Minimo_asegurado_3", oPar.Minimo_asegurado_3)
                cm.Parameters("@Minimo_asegurado_3").Direction = ParameterDirection.Input ' Importe mínimo asegurado para esta modalidad en el prox sorteo
                '
                cm.Parameters.AddWithValue("@Porc_dist_rec_7", oPar.Porc_dist_rec_7)
                cm.Parameters("@Porc_dist_rec_7").Direction = ParameterDirection.Input ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad

                cm.Parameters.AddWithValue("@Porc_dist_rec_reserva_7", oPar.Porc_dist_rec_reserva_7)
                cm.Parameters("@Porc_dist_rec_reserva_7").Direction = ParameterDirection.Input ' % de la rec de la modalidad, que se toma para asignar a la reserva

                cm.Parameters.AddWithValue("@Valor_apuesta_7", oPar.Valor_apuesta_7)
                cm.Parameters("@Valor_apuesta_7").Direction = ParameterDirection.Input ' Precio de venta de la modalidad tradicional+segunda

                cm.Parameters.AddWithValue("@Porc_valor_apuesta_7", oPar.Porc_valor_apuesta_7)
                cm.Parameters("@Porc_valor_apuesta_7").Direction = ParameterDirection.Input ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo

                cm.Parameters.AddWithValue("@Porc_cant_apuesta_7", oPar.Porc_cant_apuesta_7)
                cm.Parameters("@Porc_cant_apuesta_7").Direction = ParameterDirection.Input ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad

                cm.Parameters.AddWithValue("@Minimo_asegurado_7", oPar.Minimo_asegurado_7)
                cm.Parameters("@Minimo_asegurado_7").Direction = ParameterDirection.Input ' Importe mínimo asegurado para esta modalidad en el prox sorteo
                '
                '
                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_1_1", oPar.Porc_dist_rec_impo_1_1)
                cm.Parameters("@Porc_dist_rec_impo_1_1").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al 1er premio

                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_1_2", oPar.Porc_dist_rec_impo_1_2)
                cm.Parameters("@Porc_dist_rec_impo_1_2").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al 2do premio

                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_1_3", oPar.Porc_dist_rec_impo_1_3)
                cm.Parameters("@Porc_dist_rec_impo_1_3").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al 3er premio

                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_1_esti", oPar.Porc_dist_rec_impo_1_esti)
                cm.Parameters("@Porc_dist_rec_impo_1_esti").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al premio estimulo
                '
                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_2_1", oPar.Porc_dist_rec_impo_2_1)
                cm.Parameters("@Porc_dist_rec_impo_2_1").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al 1er premio

                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_2_2", oPar.Porc_dist_rec_impo_2_2)
                cm.Parameters("@Porc_dist_rec_impo_2_2").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al 2do premio

                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_2_3", oPar.Porc_dist_rec_impo_2_3)
                cm.Parameters("@Porc_dist_rec_impo_2_3").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al 3er premio

                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_2_esti", oPar.Porc_dist_rec_impo_2_esti)
                cm.Parameters("@Porc_dist_rec_impo_2_esti").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al premio estimulo
                '
                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_3_1", oPar.Porc_dist_rec_impo_3_1)
                cm.Parameters("@Porc_dist_rec_impo_3_1").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al 1er premio

                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_3_esti", oPar.Porc_dist_rec_impo_3_esti)
                cm.Parameters("@Porc_dist_rec_impo_3_esti").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al premio estimulo
                '
                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_7_1", oPar.Porc_dist_rec_impo_7_1)
                cm.Parameters("@Porc_dist_rec_impo_7_1").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al 1er premio

                cm.Parameters.AddWithValue("@Porc_dist_rec_impo_7_esti", oPar.Porc_dist_rec_impo_7_esti)
                cm.Parameters("@Porc_dist_rec_impo_7_esti").Direction = ParameterDirection.Input ' % de la rec estimada de la mod 1 a distribuir al premio estimulo
                'FIN parametros a actualizar

                cm.Parameters.Add(New SqlParameter("@msgRet", SqlDbType.VarChar, 1024))
                cm.Parameters("@msgRet").Direction = ParameterDirection.Output

                cm.ExecuteNonQuery()
                msgRet = cm.Parameters("@msgRet").Value
                ret = cm.Parameters("@retorno").Value

                If (ret <> 0) OrElse (msgRet <> "") Then
                    Throw New Exception("setParProxPozoEstimado - Excepción: " & msgRet)
                    res = False
                Else
                    res = True
                End If
            Catch ex As Exception
                res = False
            End Try
            Return res
        End Function
        Private Function Load(ByRef o As ParProxPozoEstimado, ByRef dr As DataRow) As Boolean
            Dim res As Boolean
            Try
                o.IdPgmSorteo = Es_Nulo(Of Integer)(dr("idPgmSorteo"), 0)

                o.Porc_var_apuestas_miercoles_domingos = Es_Nulo(Of Decimal)(dr("Porc_var_apuestas_miercoles_domingos"), 0)
                o.Porc_var_apuestas_domingos_miercoles = Es_Nulo(Of Decimal)(dr("Porc_var_apuestas_domingos_miercoles"), 0)

                o.Pozo_premio_extra = Es_Nulo(Of Decimal)(dr("Pozo_premio_extra"), 0) ' Importe previsto como pozo para premio extra
                o.Pozo_sorteo_adicional = Es_Nulo(Of Decimal)(dr("Pozo_sorteo_adicional"), 0) ' Importe previsto como pozo para MODALIDAD ADICIONAL, cuando hay
                o.Cant_apuestas_estimadas = Es_Nulo(Of Integer)(dr("Cant_apuestas_estimadas"), 0) ' Cantidad de apuestas estimadas en base a los % de variacion y/o valor ingresado por el usuario

                o.Porc_dist_rec_1 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_1"), 0) ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad
                o.Porc_dist_rec_reserva_1 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_reserva_1"), 0) ' % de la rec de la modalidad, que se toma para asignar a la reserva
                o.Valor_apuesta_1 = Es_Nulo(Of Decimal)(dr("Valor_apuesta_1"), 0) ' Precio de venta de la modalidad tradicional+segunda
                o.Porc_valor_apuesta_1 = Es_Nulo(Of Decimal)(dr("Porc_valor_apuesta_1"), 0) ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo
                o.Porc_cant_apuesta_1 = Es_Nulo(Of Decimal)(dr("Porc_cant_apuesta_1"), 0) ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad
                o.Minimo_asegurado_1 = Es_Nulo(Of Decimal)(dr("Minimo_asegurado_1"), 0) ' Importe mínimo asegurado para esta modalidad en el prox sorteo

                o.Porc_dist_rec_2 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_2"), 0) ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad
                o.Porc_dist_rec_reserva_2 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_reserva_2"), 0) ' % de la rec de la modalidad, que se toma para asignar a la reserva
                o.Valor_apuesta_2 = Es_Nulo(Of Decimal)(dr("Valor_apuesta_2"), 0) ' Precio de venta de la modalidad tradicional+segunda
                o.Porc_valor_apuesta_2 = Es_Nulo(Of Decimal)(dr("Porc_valor_apuesta_2"), 0) ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo
                o.Porc_cant_apuesta_2 = Es_Nulo(Of Decimal)(dr("Porc_cant_apuesta_2"), 0) ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad
                o.Minimo_asegurado_2 = Es_Nulo(Of Decimal)(dr("Minimo_asegurado_2"), 0) ' Importe mínimo asegurado para esta modalidad en el prox sorteo

                o.Porc_dist_rec_3 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_3"), 0) ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad
                o.Porc_dist_rec_reserva_3 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_reserva_3"), 0) ' % de la rec de la modalidad, que se toma para asignar a la reserva
                o.Valor_apuesta_3 = Es_Nulo(Of Decimal)(dr("Valor_apuesta_3"), 0) ' Precio de venta de la modalidad tradicional+segunda
                o.Porc_valor_apuesta_3 = Es_Nulo(Of Decimal)(dr("Porc_valor_apuesta_3"), 0) ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo
                o.Porc_cant_apuesta_3 = Es_Nulo(Of Decimal)(dr("Porc_cant_apuesta_3"), 0) ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad
                o.Minimo_asegurado_3 = Es_Nulo(Of Decimal)(dr("Minimo_asegurado_3"), 0) ' Importe mínimo asegurado para esta modalidad en el prox sorteo

                o.Porc_dist_rec_7 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_7"), 0) ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad
                o.Porc_dist_rec_reserva_7 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_reserva_7"), 0) ' % de la rec de la modalidad, que se toma para asignar a la reserva
                o.Valor_apuesta_7 = Es_Nulo(Of Decimal)(dr("Valor_apuesta_7"), 0) ' Precio de venta de la modalidad tradicional+segunda
                o.Porc_valor_apuesta_7 = Es_Nulo(Of Decimal)(dr("Porc_valor_apuesta_7"), 0) ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo
                o.Porc_cant_apuesta_7 = Es_Nulo(Of Decimal)(dr("Porc_cant_apuesta_7"), 0) ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad
                o.Minimo_asegurado_7 = Es_Nulo(Of Decimal)(dr("Minimo_asegurado_7"), 0) ' Importe mínimo asegurado para esta modalidad en el prox sorteo

                o.Porc_dist_rec_impo_1_1 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_1_1"), 0) ' % de la rec estimada de la mod 1 a distribuir al 1er premio
                o.Porc_dist_rec_impo_1_2 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_1_2"), 0) ' % de la rec estimada de la mod 1 a distribuir al 2do premio
                o.Porc_dist_rec_impo_1_3 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_1_3"), 0) ' % de la rec estimada de la mod 1 a distribuir al 3er premio
                o.Porc_dist_rec_impo_1_4 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_1_4"), 0) ' SOLO BRINCO: % de la rec estimada de la mod 1 a distribuir al 4to premio
                o.Porc_dist_rec_impo_1_esti = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_1_esti"), 0) ' % de la rec estimada de la mod 1 a distribuir al premio estimulo

                o.Porc_dist_rec_impo_2_1 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_2_1"), 0) ' % de la rec estimada de la mod 1 a distribuir al 1er premio
                o.Porc_dist_rec_impo_2_2 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_2_2"), 0) ' % de la rec estimada de la mod 1 a distribuir al 2do premio
                o.Porc_dist_rec_impo_2_3 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_2_3"), 0) ' % de la rec estimada de la mod 1 a distribuir al 3er premio
                o.Porc_dist_rec_impo_2_esti = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_2_esti"), 0) ' % de la rec estimada de la mod 1 a distribuir al premio estimulo

                o.Porc_dist_rec_impo_3_1 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_3_1"), 0) ' % de la rec estimada de la mod 3 a distribuir al 1er premio
                o.Porc_dist_rec_impo_3_esti = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_3_esti"), 0) ' % de la rec estimada de la mod 3 a distribuir al premio estimulo

                o.Porc_dist_rec_impo_7_1 = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_7_1"), 0) ' % de la rec estimada de la mod 7 a distribuir al 1er premio
                o.Porc_dist_rec_impo_7_esti = Es_Nulo(Of Decimal)(dr("Porc_dist_rec_impo_7_esti"), 0) ' % de la rec estimada de la mod 7 a distribuir al premio estimulo

                res = True
            Catch ex As Exception
                res = False
                Throw New Exception(ex.Message)
            End Try
            Return res
        End Function

    End Class
End Namespace
