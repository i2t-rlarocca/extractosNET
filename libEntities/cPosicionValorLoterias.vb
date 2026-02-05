Namespace Entities
    Public Class cPosicionValorLoterias
        Dim _Valor As String
        Dim _Posicion As Integer
        Public Property Valor() As String
            Get
                Return _Valor
            End Get
            Set(ByVal value As String)
                _Valor = value
            End Set
        End Property

        Public Property Posicion() As Integer
            Get
                Return _Posicion
            End Get
            Set(ByVal value As Integer)
                _Posicion = value
            End Set
        End Property
    End Class
End Namespace