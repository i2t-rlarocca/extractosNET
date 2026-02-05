Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    <System.Serializable()> Public Class ModalidadJuego

        Private _Id As Integer
        Private _Nombre As String
        Private _ValorApuesta As Double

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

        Public Property ValorApuesta() As Double
            Get
                Return _ValorApuesta
            End Get
            Set(ByVal value As Double)
                _ValorApuesta = value
            End Set
        End Property
    End Class
End Namespace