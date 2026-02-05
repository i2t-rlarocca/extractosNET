Imports Sorteos.Helpers

Namespace Entities
    Public Class PgmSorteo
        Private _idPgmSorteo As Integer
        Private _pgmConcurso As PgmConcurso
        Private _idEstadoPgmConcurso As Integer
        Private _idJuego As Integer
        Private _nroSorteo As Integer
        Private _fechaHora As Date
        Private _fechaHoraPrescripcion As Date
        Private _fechaHoraProximo As Date
        Private _fechaHoraIniReal As Date
        Private _fechaHoraFinReal As Date
        Private _localidad As String
        Private _autoridades As ListaOrdenada(Of Autoridad)
        Private _pozos As List(Of Pozo)
        Private _extraccionesLoteria As ListaOrdenada(Of pgmSorteo_loteria)
        Private _idJuegoLetra As String
        Private _pozoEstimado As Double
        Private _confirmadoParcial As Boolean
        Private _idPgmConcurso As Long
        Private _nombreSorteo As String
        Private _pathLocalJuego As String
        Private _parProxPozoConfirmado As Boolean
        Private _idSor As String
        Private _id_agr_juego As Integer

        Public Property idPgmSorteo() As Integer
            Get
                Return _idPgmSorteo
            End Get
            Set(ByVal value As Integer)
                _idPgmSorteo = value
            End Set
        End Property
        Public Property PgmConcurso() As PgmConcurso
            Get
                Return _PgmConcurso
            End Get
            Set(ByVal value As PgmConcurso)
                _PgmConcurso = value
            End Set
        End Property
        Public Property idEstadoPgmConcurso() As Long
            Get
                Return _idEstadoPgmConcurso
            End Get
            Set(ByVal value As Long)
                _idEstadoPgmConcurso = value
            End Set
        End Property
        Public Property idJuego() As Integer
            Get
                Return _idJuego
            End Get
            Set(ByVal value As Integer)
                _idJuego = value
            End Set
        End Property
        Public Property nroSorteo() As Integer
            Get
                Return _nroSorteo
            End Get
            Set(ByVal value As Integer)
                _nroSorteo = value
            End Set
        End Property
        Public Property fechaHora() As Date
            Get
                Return _fechaHora
            End Get
            Set(ByVal value As Date)
                _fechaHora = value
            End Set
        End Property
        Public Property fechaHoraPrescripcion() As Date
            Get
                Return _fechaHoraPrescripcion
            End Get
            Set(ByVal value As Date)
                _fechaHoraPrescripcion = value
            End Set
        End Property
        Public Property fechaHoraProximo() As Date
            Get
                Return _fechaHoraProximo
            End Get
            Set(ByVal value As Date)
                _fechaHoraProximo = value
            End Set
        End Property
        Public Property fechaHoraIniReal() As Date
            Get
                Return _fechaHoraIniReal
            End Get
            Set(ByVal value As Date)
                _fechaHoraIniReal = value
            End Set
        End Property
        Public Property fechaHoraFinReal() As Date
            Get
                Return _fechaHoraFinReal
            End Get
            Set(ByVal value As Date)
                _fechaHoraFinReal = value
            End Set
        End Property
        Public Property localidad() As String
            Get
                Return _localidad
            End Get
            Set(ByVal value As String)
                _localidad = value
            End Set
        End Property
        Public Property autoridades() As ListaOrdenada(Of Autoridad)
            Get
                Return _autoridades
            End Get
            Set(ByVal value As ListaOrdenada(Of Autoridad))
                _autoridades = value
            End Set
        End Property
        Public Property pozos() As List(Of Pozo)
            Get
                Return _pozos
            End Get
            Set(ByVal value As List(Of Pozo))
                _pozos = value
            End Set
        End Property
        Public Property ExtraccionesLoteria() As ListaOrdenada(Of pgmSorteo_loteria)
            Get
                Return _ExtraccionesLoteria
            End Get
            Set(ByVal value As ListaOrdenada(Of pgmSorteo_loteria))
                _ExtraccionesLoteria = value
            End Set
        End Property
        Public Property idJuegoLetra() As String
            Get
                Return _idJuegoLetra
            End Get
            Set(ByVal value As String)
                _idJuegoLetra = value
            End Set
        End Property
        Public Property PozoEstimado() As Double
            Get
                Return _pozoEstimado
            End Get
            Set(ByVal value As Double)
                _pozoEstimado = value
            End Set
        End Property
        Public Property ConfirmadoParcial() As Boolean
            Get
                Return _ConfirmadoParcial
            End Get
            Set(ByVal value As Boolean)
                _ConfirmadoParcial = value
            End Set
        End Property
        Public Property idPgmConcurso() As Long
            Get
                Return _idPgmConcurso
            End Get
            Set(ByVal value As Long)
                _idPgmConcurso = value
            End Set
        End Property
        Public Property NombreSorteo() As String
            Get
                Return _NombreSorteo
            End Get
            Set(ByVal value As String)
                _NombreSorteo = value
            End Set
        End Property
        Public Property PathLocalJuego() As String
            Get
                Return _PathLocalJuego
            End Get
            Set(ByVal value As String)
                _PathLocalJuego = value
            End Set
        End Property
        Public Property ParProxPozoConfirmado() As Boolean
            Get
                Return _parProxPozoConfirmado
            End Get
            Set(ByVal value As Boolean)
                _parProxPozoConfirmado = value
            End Set
        End Property
        Public Property idSor() As String
            Get
                Return _idSor
            End Get
            Set(ByVal value As String)
                _idSor = value
            End Set
        End Property
        Public Property idAgrJuego() As Integer
            Get
                Return _id_agr_juego
            End Get
            Set(ByVal value As Integer)
                _id_agr_juego = value
            End Set
        End Property
    End Class
End Namespace