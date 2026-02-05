Namespace Entities
    Public Class EstadoPgmConcurso
        Private _idEstadoPgmConcurso As Integer
        Private _nombre As String
        Private _descripcion As String
        Public Property idEstadoPgmConcurso() As Integer
            Get
                Return _idEstadoPgmConcurso
            End Get
            Set(ByVal value As Integer)
                _idEstadoPgmConcurso = value
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
        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(ByVal value As String)
                _descripcion = value
            End Set
        End Property
    End Class
End Namespace