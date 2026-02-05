Namespace Entities

    Public Class SrvFTP
        Private _servidor As String
        Private _puerto As Int32
        Private _pasivo As Boolean
        Private _proto As String
        Private _usr As String
        Private _pwd As String
        Private _dirRaiz As String
        Private _ssl As Boolean

        Public Property Servidor() As String
            Get
                Return _servidor
            End Get
            Set(ByVal value As String)
                _servidor = value
            End Set
        End Property

        Public Property Puerto() As Int32
            Get
                Return _puerto
            End Get
            Set(ByVal value As Int32)
                _puerto = value
            End Set
        End Property

        Public Property Pasivo() As Boolean
            Get
                Return _pasivo
            End Get
            Set(ByVal value As Boolean)
                _pasivo = value
            End Set
        End Property

        Public Property Proto() As String
            Get
                Return _proto
            End Get
            Set(ByVal value As String)
                _proto = value
            End Set
        End Property

        Public Property Usr() As String
            Get
                Return _usr
            End Get
            Set(ByVal value As String)
                _usr = value
            End Set
        End Property

        Public Property Pwd() As String
            Get
                Return _pwd
            End Get
            Set(ByVal value As String)
                _pwd = value
            End Set
        End Property

        Public Property DirRaiz() As String
            Get
                Return _dirRaiz
            End Get
            Set(ByVal value As String)
                _dirRaiz = value
            End Set
        End Property

        Public Property Ssl() As Boolean
            Get
                Return _ssl
            End Get
            Set(ByVal value As Boolean)
                _ssl = value
            End Set
        End Property

    End Class
End Namespace
