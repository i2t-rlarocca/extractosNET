Imports System.ComponentModel
Imports System.Reflection


Public Class ListaOrdenada(Of T)
    Inherits BindingList(Of T)

    Private sortDirectionValue As ListSortDirection
    Private sortPropertyValue As PropertyDescriptor
    Private isSortedValue As Boolean
    'private ArrayList sortedList;
    Private unsortedItems As ArrayList

    Public Sub New()
    End Sub

    Public Sub New(ByVal collection As IEnumerable(Of T))
        For Each t As T In collection
            Me.Add(t)
        Next
    End Sub

    Public Overloads Overrides Sub EndNew(ByVal itemIndex As Integer)
        If sortPropertyValue IsNot Nothing AndAlso itemIndex = Me.Count - 1 Then
            ApplySortCore(Me.sortPropertyValue, Me.sortDirectionValue)
        End If
        MyBase.EndNew(itemIndex)
    End Sub

    Protected Overloads Overrides ReadOnly Property SupportsSortingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overloads Overrides ReadOnly Property SupportsSearchingCore() As Boolean
        Get
            Return True
        End Get
    End Property

    Protected Overloads Overrides ReadOnly Property SortPropertyCore() As PropertyDescriptor
        Get
            Return sortPropertyValue
        End Get
    End Property

    Protected Overloads Overrides ReadOnly Property SortDirectionCore() As ListSortDirection
        Get
            Return sortDirectionValue
        End Get
    End Property

    Protected Overloads Overrides ReadOnly Property IsSortedCore() As Boolean
        Get
            Return isSortedValue
        End Get
    End Property

    Public Sub RemoveSort()
        RemoveSortCore()
    End Sub

    Protected Overloads Overrides Sub ApplySortCore(ByVal prop As PropertyDescriptor, ByVal direction As ListSortDirection)
        Dim sortedList As New ArrayList()
        ' Check to see if the property type we are sorting by implements
        ' the IComparable interface.
        Dim interfaceType As Type = prop.PropertyType.GetInterface("IComparable")

        If interfaceType IsNot Nothing Then
            ' If so, set the SortPropertyValue and SortDirectionValue.
            sortPropertyValue = prop
            sortDirectionValue = direction
            isSortedValue = True

            unsortedItems = New ArrayList(Me.Count)

            ' Loop through each item, adding it the the sortedItems ArrayList.
            For Each item As [Object] In Me.Items
                sortedList.Add(prop.GetValue(item))
                unsortedItems.Add(item)
            Next

            ' Call Sort on the ArrayList.
            sortedList.Sort()
            Dim temp As T

            ' Check the sort direction and then copy the sorted items
            ' back into the list.
            If direction = ListSortDirection.Descending Then
                sortedList.Reverse()
            End If

            For i As Integer = 0 To Me.Count - 1
                Dim position As Integer = Find(prop.Name, sortedList(i), i)
                If position <> i AndAlso position > i Then
                    temp = Me(i)
                    Me(i) = Me(position)
                    Me(position) = temp
                End If
            Next
            ' Raise the ListChanged event so bound controls refresh their
            ' values.
            OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        Else
            ' If the property type does not implement IComparable, let the user
            ' know.
            Throw New NotSupportedException(("Cannot sort by " & prop.Name & ". This") + prop.PropertyType.ToString() & " does not implement IComparable")
        End If
    End Sub

    Protected Overloads Overrides Sub RemoveSortCore()
        Dim position As Integer
        Dim temp As Object
        ' Ensure the list has been sorted.
        If unsortedItems IsNot Nothing Then
            ' Loop through the unsorted items and reorder the
            ' list per the unsorted list.
            Dim i As Integer = 0
            While i < unsortedItems.Count
                position = Me.Find(sortPropertyValue.Name, unsortedItems(i).[GetType]().GetProperty(sortPropertyValue.Name).GetValue(unsortedItems(i), Nothing), i)
                If position >= 0 AndAlso position <> i Then
                    temp = Me(i)
                    Me(i) = Me(position)
                    Me(position) = DirectCast(temp, T)
                    i += 1
                ElseIf position = i Then
                    i += 1
                Else
                    ' If an item in the unsorted list no longer exists, delete it.
                    unsortedItems.RemoveAt(i)
                End If
            End While
            isSortedValue = False
            OnListChanged(New ListChangedEventArgs(ListChangedType.Reset, -1))
        End If
    End Sub

    Protected Overloads Overrides Function FindCore(ByVal prop As PropertyDescriptor, ByVal key As Object) As Integer
        ' Get the property info for the specified property.
        Dim propInfo As PropertyInfo = GetType(T).GetProperty(prop.Name)
        Dim item As T

        If key IsNot Nothing Then
            ' Loop through the the items to see if the key
            ' value matches the property value.
            For i As Integer = 0 To Count - 1
                item = DirectCast(Items(i), T)
                If propInfo.GetValue(item, Nothing).Equals(key) Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function

    Private Overloads Function FindCore(ByVal prop As PropertyDescriptor, ByVal key As Object, ByVal index As Integer) As Integer
        ' Get the property info for the specified property.
        Dim propInfo As PropertyInfo = GetType(T).GetProperty(prop.Name)
        Dim item As T

        If key IsNot Nothing Then
            ' Loop through the the items to see if the key
            ' value matches the property value.
            For i As Integer = index To Count - 1
                item = DirectCast(Items(i), T)
                If propInfo.GetValue(item, Nothing).Equals(key) Then
                    Return i
                End If
            Next
        End If
        Return -1
    End Function

    Public Function Find(ByVal [property] As String, ByVal key As Object) As Integer
        ' Check the properties for a property with the specified name.
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim prop As PropertyDescriptor = properties.Find([property], True)

        ' If there is not a match, return -1 otherwise pass search to
        ' FindCore method.
        If prop Is Nothing Then
            Return -1
        Else
            Return FindCore(prop, key)
        End If
    End Function

    Public Function Find(ByVal [property] As String, ByVal key As Object, ByVal fromIndex As Integer) As Integer
        ' Check the properties for a property with the specified name.
        Dim properties As PropertyDescriptorCollection = TypeDescriptor.GetProperties(GetType(T))
        Dim prop As PropertyDescriptor = properties.Find([property], True)

        ' If there is not a match, return -1 otherwise pass search to
        ' FindCore method.
        If prop Is Nothing Then
            Return -1
        Else
            Return FindCore(prop, key, fromIndex)
        End If
    End Function


End Class

