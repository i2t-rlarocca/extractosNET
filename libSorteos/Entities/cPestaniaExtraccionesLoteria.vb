Namespace Entities


   

    Public Class cPestaniaExtraccionesLoteria
        Dim _IdPestania As Char
        Dim _NroExtracciones As Integer
        Dim _NroConcurso As Integer
        Dim _fechaini As Date
        Dim _fechafin As Date
        Dim _preservarFecha As Integer
        Dim _nroSorteoJur As Long
        Dim _letra1 As String
        Dim _letra2 As String
        Dim _letra3 As String
        Dim _letra4 As String

        Public Property IdPestania() As Char
            Get
                Return _IdPestania
            End Get
            Set(ByVal value As Char)
                _IdPestania = value
            End Set
        End Property
        Public Property NroExtracciones() As Integer
            Get
                Return _NroExtracciones
            End Get
            Set(ByVal value As Integer)
                _NroExtracciones = value
            End Set
        End Property
        Public Property NroConcurso() As Integer
            Get
                Return _NroConcurso
            End Get
            Set(ByVal value As Integer)
                _NroConcurso = value
            End Set
        End Property
        Public Property FechaInicio() As Date
            Get
                Return _fechaini
            End Get
            Set(ByVal value As Date)
                _fechaini = value
            End Set
        End Property
        Public Property FechaFin() As Date
            Get
                Return _fechafin
            End Get
            Set(ByVal value As Date)
                _fechafin = value
            End Set
        End Property
        Public Property PreservarFecha() As Integer
            Get
                Return _preservarFecha
            End Get
            Set(ByVal value As Integer)
                _preservarFecha = value
            End Set
        End Property
        Public Property NroSorteoJur() As Long
            Get
                Return _nroSorteoJur
            End Get
            Set(ByVal value As Long)
                _nroSorteoJur = value
            End Set
        End Property
        Public Property Letra1() As String
            Get
                Return _letra1
            End Get
            Set(ByVal value As String)
                _letra1 = value
            End Set
        End Property
        Public Property Letra2() As String
            Get
                Return _letra2
            End Get
            Set(ByVal value As String)
                _letra2 = value
            End Set
        End Property
        Public Property Letra3() As String
            Get
                Return _letra3
            End Get
            Set(ByVal value As String)
                _letra3 = value
            End Set
        End Property
        Public Property Letra4() As String
            Get
                Return _letra4
            End Get
            Set(ByVal value As String)
                _letra4 = value
            End Set
        End Property
    End Class
End Namespace