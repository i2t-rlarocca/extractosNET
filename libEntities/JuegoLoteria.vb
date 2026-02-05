Namespace Entities


    Public Class JuegoLoteria
        Private _Juego As Juego
        Private _diaSemana As DiaSemana
        Private _idloteria As Loteria
        Public Property Juego() As Juego
            Get
                Return _Juego
            End Get
            Set(ByVal value As Juego)
                _Juego = value
            End Set
        End Property
        Public Property diaSemana() As DiaSemana
            Get
                Return _diaSemana
            End Get
            Set(ByVal value As DiaSemana)
                _diaSemana = value
            End Set
        End Property
        Public Property idloteria() As Loteria
            Get
                Return _idloteria
            End Get
            Set(ByVal value As Loteria)
                _idloteria = value
            End Set
        End Property
    End Class
End Namespace