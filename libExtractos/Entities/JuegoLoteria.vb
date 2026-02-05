Imports Microsoft.VisualBasic

Namespace ExtractoEntities


    <System.Serializable()> Public Class JuegoLoteria

        Private _Autoridades As List(Of Autoridad)
        Private _HoraSorteo As String
        Private _Juego As Juego
        Private _Loteria As Loteria

        Public Property Autoridades() As List(Of Autoridad)
            Get
                Return _Autoridades
            End Get
            Set(ByVal value As List(Of Autoridad))
                _Autoridades = value
            End Set
        End Property

        Public Property HoraSorteo() As String
            Get
                Return _HoraSorteo
            End Get
            Set(ByVal value As String)
                _HoraSorteo = value
            End Set
        End Property

        Public Property Juego() As Juego
            Get
                Return _Juego
            End Get
            Set(ByVal value As Juego)
                _Juego = value
            End Set
        End Property

        Public Property Loteria() As Loteria
            Get
                Return _Loteria
            End Get
            Set(ByVal value As Loteria)
                _Loteria = value
            End Set
        End Property
    End Class
End Namespace