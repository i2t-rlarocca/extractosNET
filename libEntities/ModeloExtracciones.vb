Imports Sorteos.Helpers

Namespace Entities


    Public Class ModeloExtracciones

        Private _idModeloExtracciones As Integer
        Private _nombre As String
        Private _descripcion As String
        Private _cantExtracciones As Integer
        Private _extracciones As ListaOrdenada(Of ModeloExtraccionesDet)

        Public Property ModeloExtraccionesDet() As ListaOrdenada(Of ModeloExtraccionesDet)
            Get
                Return _extracciones
            End Get
            Set(ByVal value As ListaOrdenada(Of ModeloExtraccionesDet))
                _extracciones = value
            End Set
        End Property

        Public Property idModeloExtracciones() As Integer
            Get
                Return _idModeloExtracciones
            End Get
            Set(ByVal value As Integer)
                _idModeloExtracciones = value
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
        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(ByVal value As String)
                _descripcion = value
            End Set
        End Property
        Public Property cantExtracciones() As Integer
            Get
                Return _cantExtracciones
            End Get
            Set(ByVal value As Integer)
                _cantExtracciones = value
            End Set
        End Property

    End Class

End Namespace