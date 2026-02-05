Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    <System.Serializable()> Public Class Juego

        Private _Id As String
        Private _CantidadExtractos As Integer
        Private _Nombre As String
        Private _Modalidades As List(Of ModalidadJuego)

        Public Property Id() As String
            Get
                Return _Id
            End Get
            Set(ByVal value As String)
                _Id = value
            End Set
        End Property

        Public Property CantidadExtractos() As Integer
            Get
                Return _CantidadExtractos
            End Get
            Set(ByVal value As Integer)
                _CantidadExtractos = value
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

        Public Property Modalidades() As List(Of ModalidadJuego)
            Get
                Return _Modalidades
            End Get
            Set(ByVal value As List(Of ModalidadJuego))
                _Modalidades = value
            End Set
        End Property
    End Class
End Namespace