Namespace Entities


    Public Class cValorPosicion
        Dim _Valor As Integer
        Dim _ValorSTR As String
        Dim _Posicion As Integer
        Public Property Valor() As Integer
            Get
                Return _Valor
            End Get
            Set(ByVal value As Integer)
                _Valor = value
            End Set
        End Property
        Public Property ValorSTR() As String
            Get
                Return _ValorSTR
            End Get
            Set(ByVal value As String)
                _ValorSTR = value
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