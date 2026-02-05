Imports Sorteos.Helpers
Namespace Entities
    Public Class cJuegoSorteo
        Private _idjuego As Integer
        Private _NroSorteo As Integer
        Private _idpgmsorteo As Integer
        Private _seleccionada As Integer
        Private _DisplayText As String
        Private _idpgmConcurso As Integer
        Public Property IdJuego() As Integer
            Get
                Return _idjuego
            End Get
            Set(ByVal value As Integer)
                _idjuego = value
            End Set
        End Property
        Public Property NroSorteo() As Integer
            Get
                Return _NroSorteo
            End Get
            Set(ByVal value As Integer)
                _NroSorteo = value
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
        Public Property idPgmSorteo() As Integer
            Get
                Return _idpgmsorteo
            End Get
            Set(ByVal value As Integer)
                _idpgmsorteo = value
            End Set
        End Property
        Public Property DisplayText() As String
            Get
                Return _DisplayText
            End Get
            Set(ByVal value As String)
                _DisplayText = value
            End Set
        End Property
        Public Property IdPgmConcurso() As Integer
            Get
                Return _idpgmConcurso
            End Get
            Set(ByVal value As Integer)
                _idpgmConcurso = value
            End Set
        End Property

    End Class
End Namespace