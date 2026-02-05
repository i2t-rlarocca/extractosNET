Imports Sorteos.Helpers
Namespace Entities
    Public Class cJuegosSorteo
        Private _idjuego As Integer
        Private _JuegoDesc As String
        Private _seleccionada As Integer
        Public Property IdJuego() As Integer
            Get
                Return _idjuego
            End Get
            Set(ByVal value As Integer)
                _idjuego = value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Return _JuegoDesc
            End Get
            Set(ByVal value As String)
                _JuegoDesc = value
            End Set
        End Property
        Public Property Seleccionada() As Integer
            Get
                Return _seleccionada
            End Get
            Set(ByVal value As Integer)
                _seleccionada = value
            End Set
        End Property

    End Class
End Namespace