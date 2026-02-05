Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    Public Class CuponGanador

        Private _Agencia As String
        Private _Cupon As String
        Private _Localidad As String
        Private _Provincia As String

        Public Property Agencia() As String
            Get
                Return _Agencia
            End Get
            Set(ByVal value As String)
                _Agencia = value
            End Set
        End Property

        Public Property Cupon() As String
            Get
                Return _Cupon
            End Get
            Set(ByVal value As String)
                _Cupon = value
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

        Public Property Provincia() As String
            Get
                Return _Provincia
            End Get
            Set(ByVal value As String)
                _Provincia = value
            End Set
        End Property
    End Class
End Namespace