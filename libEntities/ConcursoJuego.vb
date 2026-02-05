Namespace Entities


    Public Class ConcursoJuego
        Implements IComparable
        
        Private _idconcurso As Integer
        Private _juego As Juego
        Private _cantCifras As Integer
        Private _orden As Integer
        Private _esPrincipal As Boolean


        Public Property idconcurso() As Integer
            Get
                Return _idconcurso
            End Get
            Set(ByVal value As Integer)
                _idconcurso = value
            End Set

        End Property
        Public Property Juego() As Juego
            Get
                Return _juego
            End Get
            Set(ByVal value As Juego)
                _juego = value
            End Set

        End Property
        Public Property cantCifras() As Integer
            Get
                Return _cantCifras
            End Get
            Set(ByVal value As Integer)
                _cantCifras = value
            End Set

        End Property
        Public Property orden() As Integer
            Get
                Return _orden
            End Get
            Set(ByVal value As Integer)
                _orden = value
            End Set
        End Property
        Public Property esPrincipal() As Boolean
            Get
                Return _esPrincipal
            End Get
            Set(ByVal value As Boolean)
                _esPrincipal = value
            End Set

        End Property

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim oItem As MenuItem = obj
            If Me.Orden > oItem.Orden Then
                Return 1
            ElseIf Me.Orden < oItem.Orden Then
                Return -1
            Else
                Return 0
            End If
        End Function
    End Class

End Namespace