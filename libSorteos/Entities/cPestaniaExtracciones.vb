Namespace Entities


    Public Class cPestaniaExtracciones
        Dim _IdPestania As Integer
        Dim _NroExtracciones As Integer
        Public Property IdPestania() As Integer
            Get
                Return _IdPestania
            End Get
            Set(ByVal value As Integer)
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
    End Class
End Namespace