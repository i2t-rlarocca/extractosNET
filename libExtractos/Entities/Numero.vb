Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    Public Class Numero

        Private _Posicion As Integer
        Private _Valor As String

        Public Property Posicion() As Integer
            Get
                Return _Posicion
            End Get
            Set(ByVal value As Integer)
                _Posicion = value
            End Set
        End Property

        Public Property Valor() As String
            Get
                Return _Valor
            End Get
            Set(ByVal value As String)
                _Valor = value
            End Set
        End Property
    End Class
End Namespace