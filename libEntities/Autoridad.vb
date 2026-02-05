Imports Sorteos.Helpers
Namespace Entities
    Public Class Autoridad
        Private _idAutoridad As Integer
        Private _idJuego As Integer
        Private _juegoDesc As String
        Private _idPgmSorteo As Integer
        Private _cargo As String
        Private _nombre As String
        Private _orden As Integer
        Private _firma As String

        Public Property idAutoridad() As Integer
            Get
                Return _idAutoridad
            End Get
            Set(ByVal value As Integer)
                _idAutoridad = value
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
        Public Property juegoDesc() As String
            Get
                Return _juegoDesc
            End Get
            Set(ByVal value As String)
                _juegoDesc = value
            End Set
        End Property
        Public Property idPgmSorteo() As Integer
            Get
                Return _idPgmSorteo
            End Get
            Set(ByVal value As Integer)
                _idPgmSorteo = value
            End Set
        End Property
        Public Property cargo() As String
            Get
                Return _cargo
            End Get
            Set(ByVal value As String)
                _cargo = value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Return _nombre
            End Get
            Set(ByVal value As String)
                _nombre = value
            End Set
        End Property
        Public Property Orden() As Integer
            Get
                Return _orden
            End Get
            Set(ByVal value As Integer)
                _orden = value
            End Set
        End Property

        Public Property Firma() As String
            Get
                Return _firma
            End Get
            Set(ByVal value As String)
                _firma = value
            End Set
        End Property
    End Class
End Namespace