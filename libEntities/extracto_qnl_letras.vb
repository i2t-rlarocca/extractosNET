Namespace Entities

    Public Class extracto_qnl_letras
        Dim _idPgmSorteo As Integer
        Dim _idloteria As Char
        Dim _orden As Integer
        Dim _letra As String

        Public Property idPgmSorteo() As Integer
            Get
                Return _idPgmSorteo
            End Get
            Set(ByVal value As Integer)
                _idPgmSorteo = value
            End Set
        End Property
        Public Property idLoteria() As Char
            Get
                Return _idloteria
            End Get
            Set(ByVal value As Char)
                _idloteria = value
            End Set
        End Property
        Public Property Orden() As Integer
            Get
                Return _orden
            End Get
            Set(ByVal value As Integer)
                _orden = value
            End Set
        End Property
        Public Property letra() As String
            Get
                Return _letra
            End Get
            Set(ByVal value As String)
                _letra = value
            End Set
        End Property
    End Class
End Namespace
