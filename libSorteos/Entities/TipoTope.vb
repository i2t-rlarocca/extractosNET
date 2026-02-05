Namespace Entities


    Public Class TipoTope

        Private _idTipoTope As Integer
        Private _nombre As String
        Private _descripcion As String
        Public Property idTipoTope() As Integer
            Get
                Return _idTipoTope
            End Get
            Set(ByVal value As Integer)
                _idTipoTope = value
            End Set
        End Property
        Public Property nombre() As String
            Get
                Return _nombre
            End Get
            Set(ByVal value As String)
                _nombre = value
            End Set
        End Property
        Public Property descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(ByVal value As String)
                _descripcion = value
            End Set
        End Property
    End Class

End Namespace