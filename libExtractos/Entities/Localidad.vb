Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    <System.Serializable()> Public Class Localidad

        Private _Id As Integer
        Private _Nombre As String

        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Set(ByVal value As Integer)
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
    End Class
End Namespace