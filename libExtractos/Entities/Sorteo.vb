Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    Public Enum EstadoSorteo
        Pendiente = 0
        Autorizado = 1
        Todos = 9
    End Enum

    <System.Serializable()> Public Class Sorteo

        Private _Estado As EstadoSorteo
        Private _FechaHoraCaducidadSorteo As DateTime
        Private _FechaHoraProximoSorteo As DateTime
        Private _FechaHoraSorteo As DateTime
        Private _Id As Integer
        Private _JuegoLoteria As JuegoLoteria
        Private _Localidad As Localidad
        Private _Numero As Integer
        Private _PozoProximoEstimado As Double
        Private _LotSorteo As List(Of String)
        Private _confirmadoParcial As Boolean

        Public Property Estado() As EstadoSorteo
            Get
                Return _Estado
            End Get
            Set(ByVal value As EstadoSorteo)
                _Estado = value
            End Set
        End Property

        Public Property FechaHoraCaducidadSorteo() As DateTime
            Get
                Return _FechaHoraCaducidadSorteo
            End Get
            Set(ByVal value As DateTime)
                _FechaHoraCaducidadSorteo = value
            End Set
        End Property

        Public Property FechaHoraProximoSorteo() As DateTime
            Get
                Return _FechaHoraProximoSorteo
            End Get
            Set(ByVal value As DateTime)
                _FechaHoraProximoSorteo = value
            End Set
        End Property

        Public Property FechaHoraSorteo() As DateTime
            Get
                Return _FechaHoraSorteo
            End Get
            Set(ByVal value As DateTime)
                _FechaHoraSorteo = value
            End Set
        End Property

        Public Property Id() As Integer
            Get
                Return _Id
            End Get
            Set(ByVal value As Integer)
                _Id = value
            End Set
        End Property

        Public Property JuegoLoteria() As JuegoLoteria
            Get
                Return _JuegoLoteria
            End Get
            Set(ByVal value As JuegoLoteria)
                _JuegoLoteria = value
            End Set
        End Property

        Public Property Localidad() As Localidad
            Get
                Return _Localidad
            End Get
            Set(ByVal value As Localidad)
                _Localidad = value
            End Set
        End Property

        Public Property Numero() As Integer
            Get
                Return _Numero
            End Get
            Set(ByVal value As Integer)
                _Numero = value
            End Set
        End Property
        Public Property PozoProximoEstimado() As Double
            Get
                Return _PozoProximoEstimado
            End Get
            Set(ByVal value As Double)
                _PozoProximoEstimado = value
            End Set
        End Property

        Public Property LotSorteo() As List(Of String)
            Get
                Return _LotSorteo
            End Get
            Set(ByVal value As List(Of String))
                _LotSorteo = value
            End Set
        End Property

      

   


    End Class
End Namespace