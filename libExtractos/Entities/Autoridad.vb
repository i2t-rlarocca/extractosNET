Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    <System.Serializable()> Public Class Autoridad

        Private _Cargo As String
        Private _Id As Integer
        Private _Nombre As String
        Private _Firma As String

        Public Property Cargo() As String
            Get
                Return _Cargo
            End Get
            Set(ByVal value As String)
                _Cargo = value
            End Set
        End Property

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

        Public Property Firma() As String
            Get
                Return _Firma
            End Get
            Set(ByVal value As String)
                _Firma = value
            End Set
        End Property
    End Class
End Namespace