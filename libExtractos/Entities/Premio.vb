Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    Public Class Premio

        Private _CuponesGanadores As Integer
        Private _Premio As String
        Private _PremioPorCupon As Double
        Private _Pozo As Double
        '** 01/10/2012
        Private _cantAciertos As Integer
        Private _extcanasta As Boolean
        '22/02/13
        Private _valorapuesta As Double
        Private _pozoestimadoJuego As Double
        Private _fechahoraproximo As Date

        Public Property CuponesGanadores() As Integer
            Get
                Return _CuponesGanadores
            End Get
            Set(ByVal value As Integer)
                _CuponesGanadores = value
            End Set
        End Property

        Public Property Pozo() As Double
            Get
                Return _Pozo
            End Get
            Set(ByVal value As Double)
                _Pozo = value
            End Set
        End Property

        Public Property Premio() As String
            Get
                Return _Premio
            End Get
            Set(ByVal value As String)
                _Premio = value
            End Set
        End Property

        Public Property PremioPorCupon() As Double
            Get
                Return _PremioPorCupon
            End Get
            Set(ByVal value As Double)
                _PremioPorCupon = value
            End Set
        End Property
        Public Property CantAciertos() As Integer
            Get
                Return _cantAciertos
            End Get
            Set(ByVal value As Integer)
                _cantAciertos = value
            End Set
        End Property
        Public Property ext_canasta() As Boolean
            Get
                Return _extcanasta
            End Get
            Set(ByVal value As Boolean)
                _extcanasta = value
            End Set
        End Property
        Public Property ValorApuesta() As Double
            Get
                Return _valorapuesta
            End Get
            Set(ByVal value As Double)
                _valorapuesta = value
            End Set
        End Property
        Public Property PozoEstimadoJuego() As Double
            Get
                Return _pozoestimadoJuego
            End Get
            Set(ByVal value As Double)
                _pozoestimadoJuego = value
            End Set
        End Property
        Public Property FechahoraProximo() As Date
            Get
                Return _fechahoraproximo
            End Get
            Set(ByVal value As Date)
                _fechahoraproximo = value
            End Set
        End Property
    End Class
End Namespace