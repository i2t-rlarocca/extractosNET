Namespace Entities
    Public Class Juego_Loteria
        Dim _idJuego As Integer
        Dim _idDiaSemana As Integer
        Dim _idLoteria As Integer
        Public Property idJuego() As Integer
            Get
                Return _idJuego
            End Get
            Set(ByVal value As Integer)
                _idJuego = value
            End Set
        End Property
        Public Property idDiaSemana() As Integer
            Get
                Return _idDiaSemana
            End Get
            Set(ByVal value As Integer)
                _idDiaSemana = value
            End Set
        End Property
        Public Property idLoteria() As Integer
            Get
                Return _idLoteria
            End Get
            Set(ByVal value As Integer)
                _idLoteria = value
            End Set
        End Property
    End Class
End Namespace