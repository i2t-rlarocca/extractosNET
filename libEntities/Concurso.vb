Imports Sorteos.Helpers

Namespace Entities


    Public Class Concurso

        Private _idConcurso As Integer
        Private _nombre As String
        Private _descripcion As String
        Private _juegoPrincipal As ConcursoJuego
        Private _tieneJuegoDependiente As Boolean
        Private _modeloExtracciones As ModeloExtracciones
        Private _juegos As Listaordenada(Of ConcursoJuego)
        Private _lstParConEspacioGan As Boolean
        Private _LstRepDetParam As Boolean

        Public Property IdConcurso() As Integer
            Get
                Return _idConcurso
            End Get
            Set(ByVal value As Integer)
                _idConcurso = value
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
        Public Property JuegoPrincipal() As ConcursoJuego
            Get
                Return _juegoPrincipal
            End Get
            Set(ByVal value As ConcursoJuego)
                _juegoPrincipal = value
            End Set
        End Property
        Public Property TieneJuegoDependiente() As Boolean
            Get
                Return _tieneJuegoDependiente
            End Get
            Set(ByVal value As Boolean)
                _tieneJuegoDependiente = value
            End Set
        End Property
        Public Property ModeloExtracciones() As ModeloExtracciones
            Get
                Return _modeloExtracciones
            End Get
            Set(ByVal value As ModeloExtracciones)
                _modeloExtracciones = value
            End Set
        End Property
        Public Property Juegos() As ListaOrdenada(Of ConcursoJuego)
            Get
                Return _juegos
            End Get
            Set(ByVal value As ListaOrdenada(Of ConcursoJuego))
                _juegos = value
            End Set
        End Property
        Public Property LstParConEspacioGan() As Boolean
            Get
                Return _lstParConEspacioGan
            End Get
            Set(ByVal value As Boolean)
                _lstParConEspacioGan = value
            End Set
        End Property
        Public Property LstRepDetParam() As Boolean
            Get
                Return _LstRepDetParam
            End Get
            Set(ByVal value As Boolean)
                _LstRepDetParam = value
            End Set
        End Property
    End Class
End Namespace
