Namespace Entities
    Public Class Sorteo_Loteria
        Private _pgmSorteo As PgmSorteo
        Private _Loteria As Loteria
        Private _nroSorteoLoteria As Long
        Private _fechaHoraLoteria As Date
        Public Property pgmSorteo() As PgmSorteo
            Get
                Return _pgmSorteo
            End Get
            Set(ByVal value As PgmSorteo)
                _pgmSorteo = value
            End Set
        End Property
        Public Property Loteria() As Loteria
            Get
                Return _Loteria
            End Get
            Set(ByVal value As Loteria)
                _Loteria = value
            End Set
        End Property
        Public Property nroSorteoLoteria() As Long
            Get
                Return _nroSorteoLoteria
            End Get
            Set(ByVal value As Long)
                _nroSorteoLoteria = value
            End Set
        End Property
        Public Property fechaHoraLoteria() As Date
            Get
                Return _fechaHoraLoteria
            End Get
            Set(ByVal value As Date)
                _fechaHoraLoteria = value
            End Set
        End Property
    End Class
End Namespace