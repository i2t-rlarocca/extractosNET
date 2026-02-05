Namespace Entities
    Public Class Sueldo

        Private _idPgmSorteo As Int32
        Private _idPremio As Int32
        Private _orden As Int32
        Private _cupon As String
        Private _agencia As String
        Private _localidad As String
        Private _provincia As String
        Private _importePremio As Double
        Private _razonSocial As String
        Private _apuesta As String
        
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
        Public Property orden() As Int32
            Get
                Return _orden
            End Get
            Set(ByVal value As Int32)
                _orden = value
            End Set
        End Property
        Public Property cupon() As String
            Get
                Return _cupon
            End Get
            Set(ByVal value As String)
                _cupon = value
            End Set
        End Property
        Public Property agencia() As String
            Get
                Return _agencia
            End Get
            Set(ByVal value As String)
                _agencia = value
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
        Public Property provincia() As String
            Get
                Return _provincia
            End Get
            Set(ByVal value As String)
                _provincia = value
            End Set
        End Property
        Public Property importePremio() As Double
            Get
                Return _importePremio
            End Get
            Set(ByVal value As Double)
                _importePremio = value
            End Set
        End Property
        Public Property razonSocial() As String
            Get
                Return _razonSocial
            End Get
            Set(ByVal value As String)
                _razonSocial = value
            End Set
        End Property
        Public Property apuesta() As String
            Get
                Return _apuesta
            End Get
            Set(ByVal value As String)
                _apuesta = value
            End Set
        End Property
    End Class
End Namespace


