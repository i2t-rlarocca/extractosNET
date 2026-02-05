Imports Sorteos.Helpers
Namespace Entities


    Public Class cUsuario
        Private _idusuario As Long
        Private _usuario As String
        Private _pwd As String
        Private _nombreUsuario As String
        Private _ultimoAcceso As DateTime
        Private _loginHabilitado As Boolean
        Private _pozoEstimadoPFHabilitado As Boolean
        Private _pozoEstimadoQ6yBRHabilitado As Boolean
        Private _reversionHabilitada As Boolean

        Public Property IdUsuario() As Long
            Get
                Return _idusuario
            End Get
            Set(ByVal value As Long)
                _idusuario = value
            End Set
        End Property
        Public Property Usuario() As String
            Get
                Return _Usuario
            End Get
            Set(ByVal value As String)
                _Usuario = value
            End Set
        End Property

        Public Property PWD() As String
            Get
                Return _pwd
            End Get
            Set(ByVal value As String)
                _pwd = value
            End Set
        End Property
        Public Property NombreUsuario() As String
            Get
                Return _nombreUsuario
            End Get
            Set(ByVal value As String)
                _nombreUsuario = value
            End Set
        End Property
        Public Property UltimoAcceso() As DateTime
            Get
                Return _ultimoAcceso
            End Get
            Set(ByVal value As DateTime)
                _ultimoAcceso = value
            End Set
        End Property
        Public Property LoginHabilitado() As Boolean
            Get
                Return _loginHabilitado
            End Get
            Set(ByVal value As Boolean)
                _loginHabilitado = value
            End Set
        End Property
        Public Property PozoEstimadoPFHabilitado() As Boolean
            Get
                Return _pozoEstimadoPFHabilitado
            End Get
            Set(ByVal value As Boolean)
                _pozoEstimadoPFHabilitado = value
            End Set
        End Property
        Public Property PozoEstimadoQ6yBRHabilitado() As Boolean
            Get
                Return _pozoEstimadoQ6yBRHabilitado
            End Get
            Set(ByVal value As Boolean)
                _pozoEstimadoQ6yBRHabilitado = value
            End Set
        End Property
        Public Property ReversionHabilitada() As Boolean
            Get
                Return _ReversionHabilitada
            End Get
            Set(ByVal value As Boolean)
                _ReversionHabilitada = value
            End Set
        End Property
    End Class
End Namespace
