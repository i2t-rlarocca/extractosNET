Imports Sorteos.Entities
Namespace Entities


    Public Class cExtractoArchivoBoldt
        Private _NumeroSorteo As Integer
        Private _idJuego As Integer
        Private _Loteria As String
        Private _Cifras As Integer
        Private _Localidad As String
        '** confirmadoParcial
        Private _FechaHoraSorteo As DateTime
        Private _FechaHoraCaducidad As DateTime
        Private _FechaHoraProximo As DateTime
        Private _Autoridad_1 As Autoridad
        Private _Autoridad_2 As Autoridad
        Private _Autoridad_3 As Autoridad
        Private _Autoridad_4 As Autoridad
        Private _Autoridad_5 As Autoridad
        Private _Numero_1 As cValorPosicion
        Private _Numero_2 As cValorPosicion
        Private _Numero_3 As cValorPosicion
        Private _Numero_4 As cValorPosicion
        Private _Numero_5 As cValorPosicion
        Private _Numero_6 As cValorPosicion
        Private _Numero_7 As cValorPosicion
        Private _Numero_8 As cValorPosicion
        Private _Numero_9 As cValorPosicion
        Private _Numero_10 As cValorPosicion
        Private _Numero_11 As cValorPosicion
        Private _Numero_12 As cValorPosicion
        Private _Numero_13 As cValorPosicion
        Private _Numero_14 As cValorPosicion
        Private _Numero_15 As cValorPosicion
        Private _Numero_16 As cValorPosicion
        Private _Numero_17 As cValorPosicion
        Private _Numero_18 As cValorPosicion
        Private _Numero_19 As cValorPosicion
        Private _Numero_20 As cValorPosicion
        Private _Numero_21 As cValorPosicion
        Private _Numero_22 As cValorPosicion
        Private _Numero_23 As cValorPosicion
        Private _Numero_24 As cValorPosicion
        Private _Numero_25 As cValorPosicion
        Private _Numero_26 As cValorPosicion
        Private _Numero_27 As cValorPosicion
        Private _Numero_28 As cValorPosicion
        Private _Numero_29 As cValorPosicion
        Private _Numero_30 As cValorPosicion
        Private _Numero_31 As cValorPosicion
        Private _Numero_32 As cValorPosicion
        Private _Numero_33 As cValorPosicion
        Private _Numero_34 As cValorPosicion
        Private _Numero_35 As cValorPosicion
        Private _Numero_36 As cValorPosicion
        Private _Juego As String
        Private _HoraIniLoteria As String
        Private _HoraFinLoteria As String

        Public Property NumeroSorteo() As Integer
            Get
                Return _NumeroSorteo
            End Get
            Set(ByVal value As Integer)
                _NumeroSorteo = value
            End Set
        End Property
        Public Property idJuego() As Integer
            Get
                Return _idJuego
            End Get
            Set(ByVal value As Integer)
                _idJuego = value
            End Set
        End Property
        Public Property Loteria() As String
            Get
                Return _Loteria
            End Get
            Set(ByVal value As String)
                _Loteria = value
            End Set
        End Property

        Public Property Cifras() As Integer
            Get
                Return _Cifras
            End Get
            Set(ByVal value As Integer)
                _Cifras = value
            End Set
        End Property
        Public Property Localidad() As String
            Get
                Return _Localidad
            End Get
            Set(ByVal value As String)
                _Localidad = value
            End Set
        End Property
        Public Property FechaHoraSorteo() As DateTime
            Get
                Return _FechaHoraSorteo
            End Get
            Set(ByVal value As DateTime)
                _FechaHoraSorteo = value
            End Set
        End Property
        Public Property FechaHoraCaducidad() As DateTime
            Get
                Return _FechaHoraCaducidad
            End Get
            Set(ByVal value As DateTime)
                _FechaHoraCaducidad = value
            End Set
        End Property
        Public Property FechaHoraProximo() As DateTime
            Get
                Return _FechaHoraProximo
            End Get
            Set(ByVal value As DateTime)
                _FechaHoraProximo = value
            End Set
        End Property
        Public Property Autoridad_1() As Autoridad
            Get
                Return _Autoridad_1
            End Get
            Set(ByVal value As Autoridad)
                _Autoridad_1 = value
            End Set
        End Property
        Public Property Autoridad_2() As Autoridad
            Get
                Return _Autoridad_2
            End Get
            Set(ByVal value As Autoridad)
                _Autoridad_2 = value
            End Set
        End Property
        Public Property Autoridad_3() As Autoridad
            Get
                Return _Autoridad_3
            End Get
            Set(ByVal value As Autoridad)
                _Autoridad_3 = value
            End Set
        End Property
        Public Property Autoridad_4() As Autoridad
            Get
                Return _Autoridad_4
            End Get
            Set(ByVal value As Autoridad)
                _Autoridad_4 = value
            End Set
        End Property
        Public Property Autoridad_5() As Autoridad
            Get
                Return _Autoridad_5
            End Get
            Set(ByVal value As Autoridad)
                _Autoridad_5 = value
            End Set
        End Property
        Public Property Numero_1() As cValorPosicion
            Get
                Return _Numero_1
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_1 = value
            End Set
        End Property
        Public Property Numero_2() As cValorPosicion
            Get
                Return _Numero_2
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_2 = value
            End Set
        End Property
        Public Property Numero_3() As cValorPosicion
            Get
                Return _Numero_3
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_3 = value
            End Set
        End Property
        Public Property Numero_4() As cValorPosicion
            Get
                Return _Numero_4
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_4 = value
            End Set
        End Property
        Public Property Numero_5() As cValorPosicion
            Get
                Return _Numero_5
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_5 = value
            End Set
        End Property
        Public Property Numero_6() As cValorPosicion
            Get
                Return _Numero_6
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_6 = value
            End Set
        End Property
        Public Property Numero_7() As cValorPosicion
            Get
                Return _Numero_7
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_7 = value
            End Set
        End Property
        Public Property Numero_8() As cValorPosicion
            Get
                Return _Numero_8
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_8 = value
            End Set
        End Property
        Public Property Numero_9() As cValorPosicion
            Get
                Return _Numero_9
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_9 = value
            End Set
        End Property
        Public Property Numero_10() As cValorPosicion
            Get
                Return _Numero_10
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_10 = value
            End Set
        End Property
        Public Property Numero_11() As cValorPosicion
            Get
                Return _Numero_11
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_11 = value
            End Set
        End Property
        Public Property Numero_12() As cValorPosicion
            Get
                Return _Numero_12
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_12 = value
            End Set
        End Property
        Public Property Numero_13() As cValorPosicion
            Get
                Return _Numero_13
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_13 = value
            End Set
        End Property
        Public Property Numero_14() As cValorPosicion
            Get
                Return _Numero_14
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_14 = value
            End Set
        End Property
        Public Property Numero_15() As cValorPosicion
            Get
                Return _Numero_15
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_15 = value
            End Set
        End Property
        Public Property Numero_16() As cValorPosicion
            Get
                Return _Numero_16
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_16 = value
            End Set
        End Property
        Public Property Numero_17() As cValorPosicion
            Get
                Return _Numero_17
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_17 = value
            End Set
        End Property
        Public Property Numero_18() As cValorPosicion
            Get
                Return _Numero_18
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_18 = value
            End Set
        End Property
        Public Property Numero_19() As cValorPosicion
            Get
                Return _Numero_19
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_19 = value
            End Set
        End Property
        Public Property Numero_20() As cValorPosicion
            Get
                Return _Numero_20
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_20 = value
            End Set
        End Property
        Public Property Numero_21() As cValorPosicion
            Get
                Return _Numero_21
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_21 = value
            End Set
        End Property
        Public Property Numero_22() As cValorPosicion
            Get
                Return _Numero_22
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_22 = value
            End Set
        End Property
        Public Property Numero_23() As cValorPosicion
            Get
                Return _Numero_23
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_23 = value
            End Set
        End Property
        Public Property Numero_24() As cValorPosicion
            Get
                Return _Numero_24
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_24 = value
            End Set
        End Property
        Public Property Numero_25() As cValorPosicion
            Get
                Return _Numero_25
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_25 = value
            End Set
        End Property
        Public Property Numero_26() As cValorPosicion
            Get
                Return _Numero_26
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_26 = value
            End Set
        End Property
        Public Property Numero_27() As cValorPosicion
            Get
                Return _Numero_27
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_27 = value
            End Set
        End Property
        Public Property Numero_28() As cValorPosicion
            Get
                Return _Numero_28
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_28 = value
            End Set
        End Property
        Public Property Numero_29() As cValorPosicion
            Get
                Return _Numero_29
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_29 = value
            End Set
        End Property
        Public Property Numero_30() As cValorPosicion
            Get
                Return _Numero_30
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_30 = value
            End Set
        End Property
        Public Property Numero_31() As cValorPosicion
            Get
                Return _Numero_31
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_31 = value
            End Set
        End Property
        Public Property Numero_32() As cValorPosicion
            Get
                Return _Numero_32
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_32 = value
            End Set
        End Property
        Public Property Numero_33() As cValorPosicion
            Get
                Return _Numero_33
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_33 = value
            End Set
        End Property
        Public Property Numero_34() As cValorPosicion
            Get
                Return _Numero_34
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_34 = value
            End Set
        End Property
        Public Property Numero_35() As cValorPosicion
            Get
                Return _Numero_35
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_35 = value
            End Set
        End Property
        Public Property Numero_36() As cValorPosicion
            Get
                Return _Numero_36
            End Get
            Set(ByVal value As cValorPosicion)
                _Numero_36 = value
            End Set
        End Property

        Public Property Juego() As String
            Get
                Return _Juego
            End Get
            Set(ByVal value As String)
                _Juego = value
            End Set
        End Property

        Public Property HoraIniLoteria() As String
            Get
                Return _HoraIniLoteria
            End Get
            Set(ByVal value As String)
                _HoraIniLoteria = value
            End Set
        End Property

        Public Property HoraFinLoteria() As String
            Get
                Return _HoraFinLoteria
            End Get
            Set(ByVal value As String)
                _HoraFinLoteria = value
            End Set
        End Property
    End Class
End Namespace