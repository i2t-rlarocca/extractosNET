Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    <System.Serializable()> Public Class Loteria

        Private _Id As String
        Private _Nombre As String
        Private _Orden As String

        Public Property Id() As String
            Get
                Return _Id
            End Get
            Set(ByVal value As String)
                _Id = value
            End Set
        End Property

        Public Property Nombre() As String
            Get
                Return _Nombre
            End Get
            Set(ByVal value As String)
                _Nombre = value
            End Set
        End Property

        Public Property Orden() As String
            Get
                Return _Orden
            End Get
            Set(ByVal value As String)
                _Orden = value
            End Set
        End Property

    End Class
End Namespace