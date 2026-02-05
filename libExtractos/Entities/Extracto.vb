Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    Public Class Extracto

        Private _CantidadCifras As Integer
        Private _HoraSorteoOrigen As String
        Private _Id As Integer
        Private _Sorteo As Sorteo
        Private _Numeros As List(Of Numero)
        Private _NumeroSorteoOrigen As Integer
        Private _FechaHoraCaducidadExtracto As DateTime
        Private _FechaHoraProximoSorteo As DateTime
        Private _HoraProximo As String
        Private _Escribano As String
        Private _Localidad As String
        Private _ConfirmadoParcial As Integer

        Public Property CantidadCifras() As Integer
            Get
                Return _CantidadCifras
            End Get
            Set(ByVal value As Integer)
                _CantidadCifras = value
            End Set
        End Property

        Public Property HoraSorteoOrigen() As String
            Get
                Return _HoraSorteoOrigen
            End Get
            Set(ByVal value As String)
                _HoraSorteoOrigen = value
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

        Public Property Sorteo() As Sorteo
            Get
                Return _Sorteo
            End Get
            Set(ByVal value As Sorteo)
                _Sorteo = value
            End Set
        End Property

        Public Property Numeros() As List(Of Numero)
            Get
                Return _Numeros
            End Get
            Set(ByVal value As List(Of Numero))
                _Numeros = value
            End Set
        End Property

        Public Property NumeroSorteoOrigen() As Integer
            Get
                Return _NumeroSorteoOrigen
            End Get
            Set(ByVal value As Integer)
                _NumeroSorteoOrigen = value
            End Set
        End Property
        Public Property FechaHoraCaducidadExtracto() As DateTime
            Get
                Return _FechaHoraCaducidadExtracto
            End Get
            Set(ByVal value As DateTime)
                _FechaHoraCaducidadExtracto = value
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

        Public Property Escribano() As String
            Get
                Return _Escribano
            End Get
            Set(ByVal value As String)
                _Escribano = value
            End Set
        End Property

        Public Property Localidad() As String
            Get
                Return _Localidad
            End Get
            Set(ByVal value As String)
                _Localidad = value
            End Set
        End Property

        Public Property HoraProximo() As String
            Get
                Return _HoraProximo
            End Get
            Set(ByVal value As String)
                _HoraProximo = value
            End Set
        End Property
        Public Property ConfirmadoParcial() As Integer
            Get
                Return _ConfirmadoParcial
            End Get
            Set(ByVal value As Integer)
                _ConfirmadoParcial = value
            End Set
        End Property
    End Class
End Namespace