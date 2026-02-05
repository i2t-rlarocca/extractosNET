Imports Sorteos.Helpers
Namespace Entities

    Public Class ParProxPozoEstimado

        Private _idPgmSorteo As Integer
        ' *** De tabla Parametros_Pozo ***
        Private _porc_var_apuestas_miercoles_domingos As Decimal
        Private _porc_var_apuestas_domingos_miercoles As Decimal
        ' DESUSO....
        'Private _porc_apuesta_total_revancha As Decimal ' % que se toma para mod REVANCHA, de la cant total de apuestas q se estima se van a vender
        'Private _porc_apuesta_total_ss As Decimal ' % que se toma para mod S SALE, de la cant total de apuestas q se estima se van a vender
        'Private _porc_para_valor_apuesta_tradic As Decimal ' % del precio que se toma para los calculos
        'Private _porc_para_valor_apuesta_revancha As Decimal ' % del precio que se toma para los calculos
        'Private _porc_para_valor_apuesta_ss As Decimal ' % del precio que se toma para los calculos
        ' FIN DESUSO....
        Private _pozo_premio_extra As Decimal ' Importe previsto como pozo para premio extra
        Private _pozo_sorteo_adicional As Decimal ' Importe previsto como pozo para MODALIDAD ADICIONAL, cuando hay
        Private _cant_apuestas_estimadas As Integer ' Cantidad de apuestas estimadas en base a los % de variacion y/o valor ingresado por el usuario
        ' *** De tabla Parametros_Pozo_Modalidad ***
        ' Mod Tradicional (1)
        Private _porc_dist_rec_1 As Decimal ' % de la rec de la modalidad, que se toma para distribuir entre los diferentes premios de la modalidad
        Private _porc_dist_rec_reserva_1 As Decimal ' % de la rec de la modalidad, que se toma para asignar a la reserva
        Private _valor_apuesta_1 As Decimal ' Precio de venta de la modalidad tradicional+segunda
        Private _porc_valor_apuesta_1 As Decimal ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo
        Private _porc_cant_apuesta_1 As Decimal ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad
        Private _minimo_asegurado_1 As Decimal ' Importe mínimo asegurado para esta modalidad en el prox sorteo
        ' Mod La Segunda (2)
        Private _porc_dist_rec_2 As Decimal
        Private _porc_dist_rec_reserva_2 As Decimal
        Private _valor_apuesta_2 As Decimal ' Precio de venta de la modalidad tradicional+segunda
        Private _porc_valor_apuesta_2 As Decimal ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo
        Private _porc_cant_apuesta_2 As Decimal ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad
        Private _minimo_asegurado_2 As Decimal ' Importe mínimo asegurado para esta modalidad en el prox sorteo
        ' Mod Revancha (3)
        Private _porc_dist_rec_3 As Decimal
        Private _porc_dist_rec_reserva_3 As Decimal
        Private _valor_apuesta_3 As Decimal ' Precio de venta "diferencial" de la modalidad revancha
        Private _porc_valor_apuesta_3 As Decimal ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo
        Private _porc_cant_apuesta_3 As Decimal ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad
        Private _minimo_asegurado_3 As Decimal ' Importe mínimo asegurado para esta modalidad en el prox sorteo
        ' Mod S. Sale (7)
        Private _porc_dist_rec_7 As Decimal
        Private _porc_dist_rec_reserva_7 As Decimal
        Private _valor_apuesta_7 As Decimal ' Precio de venta "diferencial" de la modalidad revancha
        Private _porc_valor_apuesta_7 As Decimal ' % del precio de venta a tomar para estimar la recaudación de esta modalidad del prox sorteo
        Private _porc_cant_apuesta_7 As Decimal ' % de la cant total de apuestas estimadas para el prox sorteo, a tomar para esta modalidad
        Private _minimo_asegurado_7 As Decimal ' Importe mínimo asegurado para esta modalidad en el prox sorteo
        ' *** De tabla Parametros_Pozo_Premio ***
        ' Premios Mod Tradicional (1)
        Private _porc_dist_rec_impo_1_1 As Decimal ' % de la rec estimada de la mod 1 a distribuir al 1er premio
        Private _porc_dist_rec_impo_1_2 As Decimal ' % de la rec estimada de la mod 1 a distribuir al 2do premio
        Private _porc_dist_rec_impo_1_3 As Decimal ' % de la rec estimada de la mod 1 a distribuir al 3er premio
        Private _porc_dist_rec_impo_1_4 As Decimal ' SOLO BRINCO: % de la rec estimada de la mod 1 a distribuir al 4to premio
        Private _porc_dist_rec_impo_1_esti As Decimal ' % de la rec estimada de la mod 1 a distribuir al premio estimulo
        ' Premios Mod La Segunda (2)
        Private _porc_dist_rec_impo_2_1 As Decimal ' % de la rec estimada de la mod 2 a distribuir al 1er premio
        Private _porc_dist_rec_impo_2_2 As Decimal ' % de la rec estimada de la mod 2 a distribuir al 2do premio
        Private _porc_dist_rec_impo_2_3 As Decimal ' % de la rec estimada de la mod 2 a distribuir al 3er premio
        Private _porc_dist_rec_impo_2_esti As Decimal ' % de la rec estimada de la mod 2 a distribuir al premio estimulo
        ' Premios Mod Revancha (3)
        Private _porc_dist_rec_impo_3_1 As Decimal ' % de la rec estimada de la mod 3 a distribuir al 1er premio
        Private _porc_dist_rec_impo_3_esti As Decimal ' % de la rec estimada de la mod 3 a distribuir al premio estimulo
        ' Premios Mod S. Sale (7)
        Private _porc_dist_rec_impo_7_1 As Decimal ' % de la rec estimada de la mod 7 a distribuir al 1er premio
        Private _porc_dist_rec_impo_7_esti As Decimal ' % de la rec estimada de la mod 7 a distribuir al premio estimulo


        Public Property IdPgmSorteo() As Integer
            Get
                Return _idPgmSorteo
            End Get
            Set(ByVal value As Integer)
                _idPgmSorteo = value
            End Set
        End Property

        ' *** De tabla Parametros_Pozo ***
        Public Property Porc_var_apuestas_miercoles_domingos() As Decimal
            Get
                Return _porc_var_apuestas_miercoles_domingos
            End Get
            Set(ByVal value As Decimal)
                _porc_var_apuestas_miercoles_domingos = value
            End Set
        End Property
        Public Property Porc_var_apuestas_domingos_miercoles() As Decimal
            Get
                Return _porc_var_apuestas_domingos_miercoles
            End Get
            Set(ByVal value As Decimal)
                _porc_var_apuestas_domingos_miercoles = value
            End Set
        End Property
        Public Property Pozo_premio_extra() As Decimal
            Get
                Return _pozo_premio_extra
            End Get
            Set(ByVal value As Decimal)
                _pozo_premio_extra = value
            End Set
        End Property
        Public Property Pozo_sorteo_adicional() As Decimal
            Get
                Return _pozo_sorteo_adicional
            End Get
            Set(ByVal value As Decimal)
                _pozo_sorteo_adicional = value
            End Set
        End Property
        ' *** De tabla Parametros_Pozo_Modalidad ***
        ' Mod Tradicional (1)
        Public Property Porc_dist_rec_1() As Decimal
            Get
                Return _porc_dist_rec_1
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_1 = value
            End Set
        End Property
        Public Property Porc_dist_rec_reserva_1() As Decimal
            Get
                Return _porc_dist_rec_reserva_1
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_reserva_1 = value
            End Set
        End Property
        Public Property Valor_apuesta_1() As Decimal
            Get
                Return _valor_apuesta_1
            End Get
            Set(ByVal value As Decimal)
                _valor_apuesta_1 = value
            End Set
        End Property
        Public Property Porc_valor_apuesta_1() As Decimal
            Get
                Return _porc_valor_apuesta_1
            End Get
            Set(ByVal value As Decimal)
                _porc_valor_apuesta_1 = value
            End Set
        End Property
        Public Property Porc_cant_apuesta_1() As Decimal
            Get
                Return _porc_cant_apuesta_1
            End Get
            Set(ByVal value As Decimal)
                _porc_cant_apuesta_1 = value
            End Set
        End Property
        Public Property Minimo_asegurado_1() As Decimal
            Get
                Return _minimo_asegurado_1
            End Get
            Set(ByVal value As Decimal)
                _minimo_asegurado_1 = value
            End Set
        End Property
        ' Mod La Segunda (2)
        Public Property Porc_dist_rec_2() As Decimal
            Get
                Return _porc_dist_rec_2
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_2 = value
            End Set
        End Property
        Public Property Porc_dist_rec_reserva_2() As Decimal
            Get
                Return _porc_dist_rec_reserva_2
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_reserva_2 = value
            End Set
        End Property
        Public Property Valor_apuesta_2() As Decimal
            Get
                Return _valor_apuesta_2
            End Get
            Set(ByVal value As Decimal)
                _valor_apuesta_2 = value
            End Set
        End Property
        Public Property Porc_valor_apuesta_2() As Decimal
            Get
                Return _porc_valor_apuesta_2
            End Get
            Set(ByVal value As Decimal)
                _porc_valor_apuesta_2 = value
            End Set
        End Property
        Public Property Porc_cant_apuesta_2() As Decimal
            Get
                Return _porc_cant_apuesta_2
            End Get
            Set(ByVal value As Decimal)
                _porc_cant_apuesta_2 = value
            End Set
        End Property
        Public Property Minimo_asegurado_2() As Decimal
            Get
                Return _minimo_asegurado_2
            End Get
            Set(ByVal value As Decimal)
                _minimo_asegurado_2 = value
            End Set
        End Property
        ' Mod Revancha (3)
        Public Property Porc_dist_rec_3() As Decimal
            Get
                Return _porc_dist_rec_3
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_3 = value
            End Set
        End Property
        Public Property Porc_dist_rec_reserva_3() As Decimal
            Get
                Return _porc_dist_rec_reserva_3
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_reserva_3 = value
            End Set
        End Property
        Public Property Valor_apuesta_3() As Decimal
            Get
                Return _valor_apuesta_3
            End Get
            Set(ByVal value As Decimal)
                _valor_apuesta_3 = value
            End Set
        End Property
        Public Property Porc_valor_apuesta_3() As Decimal
            Get
                Return _porc_valor_apuesta_3
            End Get
            Set(ByVal value As Decimal)
                _porc_valor_apuesta_3 = value
            End Set
        End Property
        Public Property Porc_cant_apuesta_3() As Decimal
            Get
                Return _porc_cant_apuesta_3
            End Get
            Set(ByVal value As Decimal)
                _porc_cant_apuesta_3 = value
            End Set
        End Property
        Public Property Minimo_asegurado_3() As Decimal
            Get
                Return _minimo_asegurado_3
            End Get
            Set(ByVal value As Decimal)
                _minimo_asegurado_3 = value
            End Set
        End Property
        ' Mod S. Sale (7)
        Public Property Porc_dist_rec_7() As Decimal
            Get
                Return _porc_dist_rec_7
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_7 = value
            End Set
        End Property
        Public Property Porc_dist_rec_reserva_7() As Decimal
            Get
                Return _porc_dist_rec_reserva_7
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_reserva_7 = value
            End Set
        End Property
        Public Property Valor_apuesta_7() As Decimal
            Get
                Return _valor_apuesta_7
            End Get
            Set(ByVal value As Decimal)
                _valor_apuesta_7 = value
            End Set
        End Property
        Public Property Porc_valor_apuesta_7() As Decimal
            Get
                Return _porc_valor_apuesta_7
            End Get
            Set(ByVal value As Decimal)
                _porc_valor_apuesta_7 = value
            End Set
        End Property
        Public Property Porc_cant_apuesta_7() As Decimal
            Get
                Return _porc_cant_apuesta_7
            End Get
            Set(ByVal value As Decimal)
                _porc_cant_apuesta_7 = value
            End Set
        End Property
        Public Property Minimo_asegurado_7() As Decimal
            Get
                Return _minimo_asegurado_7
            End Get
            Set(ByVal value As Decimal)
                _minimo_asegurado_7 = value
            End Set
        End Property
        ' *** De tabla Parametros_Pozo_Premio ***
        ' Premios Mod Tradicional (1)
        Public Property Porc_dist_rec_impo_1_1() As Decimal
            Get
                Return _porc_dist_rec_impo_1_1
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_1_1 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_1_2() As Decimal
            Get
                Return _porc_dist_rec_impo_1_2
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_1_2 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_1_3() As Decimal
            Get
                Return _porc_dist_rec_impo_1_3
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_1_3 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_1_4() As Decimal
            Get
                Return _porc_dist_rec_impo_1_4
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_1_4 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_1_esti() As Decimal
            Get
                Return _porc_dist_rec_impo_1_esti
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_1_esti = value
            End Set
        End Property
        ' Premios Mod La Segunda (2)
        Public Property Porc_dist_rec_impo_2_1() As Decimal
            Get
                Return _porc_dist_rec_impo_2_1
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_2_1 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_2_2() As Decimal
            Get
                Return _porc_dist_rec_impo_2_2
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_2_2 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_2_3() As Decimal
            Get
                Return _porc_dist_rec_impo_2_3
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_2_3 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_2_esti() As Decimal
            Get
                Return _porc_dist_rec_impo_2_esti
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_2_esti = value
            End Set
        End Property
        ' Premios Mod Revancha (3)
        Public Property Porc_dist_rec_impo_3_1() As Decimal
            Get
                Return _porc_dist_rec_impo_3_1
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_3_1 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_3_esti() As Decimal
            Get
                Return _porc_dist_rec_impo_3_esti
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_3_esti = value
            End Set
        End Property
        ' Premios Mod S. Sale (7)
        Public Property Porc_dist_rec_impo_7_1() As Decimal
            Get
                Return _porc_dist_rec_impo_7_1
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_7_1 = value
            End Set
        End Property
        Public Property Porc_dist_rec_impo_7_esti() As Decimal
            Get
                Return _porc_dist_rec_impo_7_esti
            End Get
            Set(ByVal value As Decimal)
                _porc_dist_rec_impo_7_esti = value
            End Set
        End Property
        Public Property Cant_apuestas_estimadas() As Integer
            Get
                Return _cant_apuestas_estimadas
            End Get
            Set(ByVal value As Integer)
                _cant_apuestas_estimadas = value
            End Set
        End Property
    End Class
End Namespace