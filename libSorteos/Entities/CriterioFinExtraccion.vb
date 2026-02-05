Namespace Entities


    Public Class CriterioFinExtraccion

        Private _idCriterioFinExtraccion As Integer
        Private _nombre As String
        Private _descripcion As String
        Public Property idCriterioFinExtraccion() As Integer
            Get
                Return _idCriterioFinExtraccion
            End Get
            Set(ByVal value As Integer)
                _idCriterioFinExtraccion = value
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
