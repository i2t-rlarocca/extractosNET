
Public Class cAutoridades
    Private Shared _listaAutoridades As New List(Of cAutoridades)

    Private _cargo As String
    Private _nombre As String

    Public Property Cargo() As String
        Get
            Return _cargo
        End Get
        Set(ByVal value As String)
            _cargo = value
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

    Public ReadOnly Property ListaAutoridades() As List(Of cAutoridades)
        Get
            Return _listaAutoridades
        End Get
    End Property

    Public Sub New(ByVal c As String, ByVal n As String)
        Me._cargo = c
        Me._nombre = n

    End Sub

    Public Shared Function getListaAutoridades() As List(Of cAutoridades)

        Dim a As cAutoridades

        a = New cAutoridades("AREA NOTARIAL", "Ana María Cía")
        _listaAutoridades.Add(a)

        a = New cAutoridades("JEFE DPTO. LOTERIA", "JUAN PEREZ")
        _listaAutoridades.Add(a)

        a = New cAutoridades("AREA COMERCIAL", "DOÑA CATALINA")
        _listaAutoridades.Add(a)

        Return _listaAutoridades
    End Function

End Class
