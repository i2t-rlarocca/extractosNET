Imports Sorteos.Helpers

Namespace Entities

    Public Class MenuItem

        Implements IComparable


        Public Enum Tipo_Menu As Long
            Raiz = 0
            SubMenu = 1
            OpcionMenu = 2
        End Enum

        Dim _items As ListaOrdenada(Of MenuItem) = New ListaOrdenada(Of MenuItem)
        Public Property Items() As ListaOrdenada(Of MenuItem)
            Get
                Return _items
            End Get
            Set(ByVal value As ListaOrdenada(Of MenuItem))
                _items = value
            End Set
        End Property

        Private _MenuID As String
        Public Property MenuId() As String
            Get
                Return _MenuID
            End Get
            Set(ByVal value As String)
                _MenuID = value
            End Set
        End Property

        Private _MenuPadreID As String
        Public Property MenuPadreID() As String
            Get
                Return _MenuPadreID
            End Get
            Set(ByVal value As String)
                _MenuPadreID = value
            End Set
        End Property

        Private _FuncionID As Long
        Public Property FuncionID() As Long
            Get
                Return _FuncionID
            End Get
            Set(ByVal value As Long)
                _FuncionID = value
            End Set
        End Property

        Private _descripcion As String
        Public Property Descripion() As String
            Get
                Return _descripcion
            End Get
            Set(ByVal value As String)
                _descripcion = value
            End Set
        End Property

        Private _observacion As String
        Public Property Observacion() As String
            Get
                Return _observacion
            End Get
            Set(ByVal value As String)
                _observacion = value
            End Set
        End Property

        Private _tipoMenu As Tipo_Menu
        Public Property TipoMenu() As Tipo_Menu
            Get
                Return _tipoMenu
            End Get
            Set(ByVal value As Tipo_Menu)
                _tipoMenu = value
            End Set
        End Property

        Private _moduloid As Long
        Public Property ModuloID() As Long
            Get
                Return _moduloid
            End Get
            Set(ByVal value As Long)
                _moduloid = value
            End Set
        End Property

        Private _orden As Long
        Public Property Orden() As Long
            Get
                Return _orden
            End Get
            Set(ByVal value As Long)
                _orden = value
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