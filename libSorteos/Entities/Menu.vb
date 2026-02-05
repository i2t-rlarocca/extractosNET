Imports Sorteos.Helpers

Namespace Entities


    Public Class Menu

        Dim _items As ListaOrdenada(Of MenuItem)
        Public Property Items() As ListaOrdenada(Of MenuItem)
            Get
                Return _items
            End Get
            Set(ByVal value As ListaOrdenada(Of MenuItem))
                _items = value
            End Set
        End Property

    End Class
End Namespace