Namespace Entities
    Public Class Pozo
        Private _idPgmSorteo As Int32
        Private _idPremio As Int32
        Private _importe As Double
        Private _nombrePremio As String
        Private _apuestas As Int64
        Private _importeRec As Double

        Public Property idPgmsorteo() As Int32
            Get
                Return _idPgmSorteo
            End Get
            Set(ByVal value As Int32)
                _idPgmSorteo = value
            End Set
        End Property
        Public Property idPremio() As Int32
            Get
                Return _idPremio
            End Get
            Set(ByVal value As Int32)
                _idPremio = value
            End Set
        End Property
        Public Property importe() As Double
            Get
                Return _importe
            End Get
            Set(ByVal value As Double)
                _importe = value
            End Set
        End Property

        Public Property nombrePremio() As String
            Get
                Return _nombrePremio
            End Get
            Set(ByVal value As String)
                _nombrePremio = value
            End Set
        End Property

        Public Property importeRec() As Double
            Get
                Return _importeRec
            End Get
            Set(ByVal value As Double)
                _importeRec = value
            End Set
        End Property

        Public Property Apuestas() As Int64
            Get
                Return _Apuestas
            End Get
            Set(ByVal value As Int64)
                _Apuestas = value
            End Set
        End Property
    End Class
End Namespace
