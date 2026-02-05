Namespace Entities


    Public Class ModeloExtraccionesDet
        Inherits TipoExtraccion
        Implements IComparable

        Private _idModeloExtraccionesDet As Integer
        Private _idModeloExtracciones As Integer
        Private _orden As Integer
        Private _obligatoria As Boolean
        Private _titulo As String

        Public Property idModeloExtraccionesDet() As Integer
            Get
                Return _idModeloExtraccionesDet
            End Get
            Set(ByVal value As Integer)
                _idModeloExtraccionesDet = value
            End Set
        End Property
        Public Property IdModeloExtracciones() As Integer
            Get
                Return _idModeloExtracciones
            End Get
            Set(ByVal value As Integer)
                _idModeloExtracciones = value
            End Set
        End Property
        Public Property Orden() As Integer
            Get
                Return _orden
            End Get
            Set(ByVal value As Integer)
                _orden = value
            End Set
        End Property
        Public Property Obligatoria() As Boolean
            Get
                Return _obligatoria
            End Get
            Set(ByVal value As Boolean)
                _obligatoria = value
            End Set
        End Property
        Public Property Titulo() As String
            Get
                Return _titulo
            End Get
            Set(ByVal value As String)
                _titulo = value
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