Imports libEntities.Entities
Imports Sorteos.Helpers


Public Class MenuBO

    Public Function GetMenu() As Menu
        Dim o As New Menu
        o.Items = Me.Cargar_Items
        Return o
    End Function

    Private Function Cargar_Items() As ListaOrdenada(Of MenuItem)
        Dim Dal As New MenuDal

        ' Lista de todos los Items de Menú
        Dim ListaTodos As ListaOrdenada(Of MenuItem) = Dal.Obtener_Todos_Items()
        ' Lista que se devuelve
        Dim ListaHijosRetorno As ListaOrdenada(Of MenuItem) = New ListaOrdenada(Of MenuItem)
        ' Array list para ordenar
        Dim ListaHijos As ArrayList = New ArrayList

        ' Recorro los items y por cada uno que sea raiz completo los hijos
        For Each oItem As MenuItem In ListaTodos
            If oItem.MenuPadreID = "" Then
                ListaHijos.Add(oItem)
            End If
        Next

        ' Recorro los items para eliminar los que ya son hijos de algun nodo
        For Each oItem As MenuItem In ListaHijos
            ListaTodos.Remove(oItem)
        Next

        ' Recorro los items y por cada uno que sea raiz completo los hijos
        For Each oItem As MenuItem In ListaHijos
            Completar_Hijos(oItem, ListaTodos)
        Next

        ListaHijos.Sort()
        For Each oItem As MenuItem In ListaHijos
            ListaHijosRetorno.Add(oItem)
        Next

        Return ListaHijosRetorno

    End Function

    Private Sub Completar_Hijos(ByVal pItemPadre As MenuItem, ByVal pListaTodos As ListaOrdenada(Of MenuItem))

        ' Array list para ordenar
        Dim ListaHijos As ArrayList = New ArrayList

        ' Recorro los items y busco los hijos de este nodo...
        For Each oItem As MenuItem In pListaTodos
            If oItem.MenuPadreID = pItemPadre.MenuId Then
                ListaHijos.Add(oItem)
            End If
        Next

        ' Ordeno la lista 
        ListaHijos.Sort()

        ' Recorro los items hijos y completo el padre
        For Each oItem As MenuItem In ListaHijos
            pItemPadre.Items.Add(oItem)
        Next

        ' Recorro los items para eliminar de la lista de pendientes, los que ya son hijos de este nodo 
        For Each oItem As MenuItem In pItemPadre.Items
            pListaTodos.Remove(oItem)
        Next

        ' Recorro los items hijos para que cada uno de ellos busque a sus items hijos
        For Each oItem As MenuItem In pItemPadre.Items
            Completar_Hijos(oItem, pListaTodos)
        Next

    End Sub

End Class
