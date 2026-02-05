Imports Sorteos.Helpers
Namespace Entities


    Public Class PgmConcurso
        Implements IComparable

        Private _idPgmConcurso As Integer
        Private _nombre As String
        Private _estadoPgmConcurso As Integer
        Private _fechaHora As Date
        Private _concurso As Concurso
        Private _idPgmSorteoPrincipal As Integer
        Private _fechaHoraIniReal As Date
        Private _fechaHoraFinReal As Date
        Private _escribano As Integer
        Private _operador As String
        Private _localidad As String
        Private _pgmSorteos As ListaOrdenada(Of PgmSorteo)
        Private _Extracciones As ListaOrdenada(Of ExtraccionesCAB)
        Private _MetodosIngreso As ListaOrdenada(Of MetodoIngreso)
        Private _MetodosIngresoJurisdicciones As ListaOrdenada(Of MetodoIngreso)

        Public Property PgmSorteos() As ListaOrdenada(Of PgmSorteo)
            Get
                Return _pgmSorteos
            End Get
            Set(ByVal value As ListaOrdenada(Of PgmSorteo))
                _pgmSorteos = value
            End Set
        End Property

        Public Property idPgmConcurso() As Integer
            Get
                Return _idPgmConcurso
            End Get
            Set(ByVal value As Integer)
                _idPgmConcurso = value
            End Set
        End Property
        Public Property nombre() As String
            Get
                Return _nombre
            End Get
            Set(ByVal value As String)
                _nombre = value
            End Set
        End Property
        Public Property estadoPgmConcurso() As Integer
            Get
                Return _estadoPgmConcurso
            End Get
            Set(ByVal value As Integer)
                _estadoPgmConcurso = value
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
        Public Property concurso() As Concurso
            Get
                Return _concurso
            End Get
            Set(ByVal value As Concurso)
                _concurso = value
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
        Public Property idPgmSorteoPrincipal() As Integer
            Get
                Return _idPgmSorteoPrincipal
            End Get
            Set(ByVal value As Integer)
                _idPgmSorteoPrincipal = value
            End Set
        End Property
        Public Property Escribano() As Integer
            Get
                Return _escribano
            End Get
            Set(ByVal value As Integer)
                _escribano = value
            End Set
        End Property
        Public Property Operador() As String
            Get
                Return _operador
            End Get
            Set(ByVal value As String)
                _operador = value
            End Set
        End Property
        Public Property Extracciones() As ListaOrdenada(Of ExtraccionesCAB)
            Get
                Return _Extracciones
            End Get
            Set(ByVal value As ListaOrdenada(Of ExtraccionesCAB))
                _Extracciones = value
            End Set
        End Property
        Public Property MetodosIngreso() As ListaOrdenada(Of MetodoIngreso)
            Get
                Return _MetodosIngreso
            End Get
            Set(ByVal value As ListaOrdenada(Of MetodoIngreso))
                _MetodosIngreso = value
            End Set
        End Property
        Public Property MetodosIngresoJurisdicciones() As ListaOrdenada(Of MetodoIngreso)
            Get
                Return _MetodosIngresoJurisdicciones
            End Get
            Set(ByVal value As ListaOrdenada(Of MetodoIngreso))
                _MetodosIngresoJurisdicciones = value
            End Set
        End Property

        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim oPgmConcurso As PgmConcurso = obj
            If Me.concurso.Nombre > oPgmConcurso.concurso.Nombre Then
                Return 1
            ElseIf Me.concurso.Nombre < oPgmConcurso.concurso.Nombre Then
                Return -1
            Else
                Return 0
            End If
        End Function
    End Class
End Namespace