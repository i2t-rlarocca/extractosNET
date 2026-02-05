Namespace Entities
    Public Class PgmSorteo_Autoridad
        Private _idPgmsorteo As PgmSorteo_Autoridad
        Private _autoridad As Autoridad
        Public Property idPgmsorteo() As PgmSorteo_Autoridad
            Get
                Return _idPgmsorteo
            End Get
            Set(ByVal value As PgmSorteo_Autoridad)
                _idPgmsorteo = value
            End Set
        End Property
        Public Property Autoridad() As Autoridad
            Get
                Return _autoridad
            End Get
            Set(ByVal value As Autoridad)
                _autoridad = value
            End Set
        End Property
    End Class
End Namespace