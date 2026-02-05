Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    Public Class ExtractoBrinco
        Inherits Extracto

        Private _CuponesGanadoresSueldos As List(Of CuponGanador)
        Private _PremiosPrimerSorteo As List(Of Premio)

        Public Property CuponesGanadoresSueldos() As List(Of CuponGanador)
            Get
                Return _CuponesGanadoresSueldos
            End Get
            Set(ByVal value As List(Of CuponGanador))
                _CuponesGanadoresSueldos = value
            End Set
        End Property

        Public Property PremiosPrimerSorteo() As List(Of Premio)
            Get
                Return _PremiosPrimerSorteo
            End Get
            Set(ByVal value As List(Of Premio))
                _PremiosPrimerSorteo = value
            End Set
        End Property
    End Class
End Namespace