Imports Sorteos.Helpers
Namespace Entities
    Public Class Extracto_qnl

        Dim _idPgmsorteo As Integer
        Dim _idLoteria As Char
        Dim _loteria As Char
        Dim _cifras As Integer
        Dim _Valores As ListaOrdenada(Of cPosicionValorLoterias)
        Dim _progres As Char
        Dim _coddescr As Char
        Dim _extract_lote_1 As Char
        Dim _extract_lote_2 As Char
        Dim _extract_lote_3 As Char
        Dim _extract_lote_4 As Char
        Dim _extract_lote_5 As Char
        Dim _extract_lote_6 As Char
        Dim _extract_lote_7 As Char
        Dim _extract_lote_8 As Char
        Dim _extract_lote_9 As Char
        Dim _extract_lote_10 As Char
        Dim _extract_lote_11 As Char
        Dim _extract_lote_12 As Char
        Dim _extract_lote_13 As Char
        Dim _extract_lote_14 As Char
        Dim _extract_lote_15 As Char
        Public Property idPgmSorteo() As Integer
            Get
                Return _idPgmsorteo
            End Get
            Set(ByVal value As Integer)
                _idPgmsorteo = value
            End Set
        End Property
        Public Property idLoteria() As Char
            Get
                Return _idLoteria
            End Get
            Set(ByVal value As Char)
                _idLoteria = value
            End Set
        End Property
        Public Property Loteria() As Char
            Get
                Return _loteria
            End Get
            Set(ByVal value As Char)
                _loteria = value
            End Set
        End Property
        Public Property Cifras() As Integer
            Get
                Return _cifras
            End Get
            Set(ByVal value As Integer)
                _cifras = value
            End Set
        End Property
        Public Property Valores() As ListaOrdenada(Of cPosicionValorLoterias)
            Get
                Return _Valores
            End Get
            Set(ByVal value As ListaOrdenada(Of cPosicionValorLoterias))
                _Valores = value
            End Set
        End Property

        Public Property Progres() As Char
            Get
                Return _progres
            End Get
            Set(ByVal value As Char)
                _progres = value
            End Set
        End Property
        Public Property coddescr() As Char
            Get
                Return _coddescr
            End Get
            Set(ByVal value As Char)
                _coddescr = value
            End Set
        End Property
        Public Property extract_lote_1() As Char
            Get
                Return _extract_lote_1
            End Get
            Set(ByVal value As Char)
                _extract_lote_1 = value
            End Set
        End Property
        Public Property extract_lote_2() As Char
            Get
                Return _extract_lote_2
            End Get
            Set(ByVal value As Char)
                _extract_lote_2 = value
            End Set
        End Property
        Public Property extract_lote_3() As Char
            Get
                Return _extract_lote_3
            End Get
            Set(ByVal value As Char)
                _extract_lote_3 = value
            End Set
        End Property
        Public Property extract_lote_4() As Char
            Get
                Return _extract_lote_4
            End Get
            Set(ByVal value As Char)
                _extract_lote_4 = value
            End Set
        End Property
        Public Property extract_lote_5() As Char
            Get
                Return _extract_lote_5
            End Get
            Set(ByVal value As Char)
                _extract_lote_5 = value
            End Set
        End Property
        Public Property extract_lote_6() As Char
            Get
                Return _extract_lote_6
            End Get
            Set(ByVal value As Char)
                _extract_lote_6 = value
            End Set
        End Property
        Public Property extract_lote_7() As Char
            Get
                Return _extract_lote_7
            End Get
            Set(ByVal value As Char)
                _extract_lote_7 = value
            End Set
        End Property
        Public Property extract_lote_8() As Char
            Get
                Return _extract_lote_8
            End Get
            Set(ByVal value As Char)
                _extract_lote_8 = value
            End Set
        End Property
        Public Property extract_lote_9() As Char
            Get
                Return _extract_lote_9
            End Get
            Set(ByVal value As Char)
                _extract_lote_9 = value
            End Set
        End Property
        Public Property extract_lote_10() As Char
            Get
                Return _extract_lote_10
            End Get
            Set(ByVal value As Char)
                _extract_lote_10 = value
            End Set
        End Property
        Public Property extract_lote_11() As Char
            Get
                Return _extract_lote_11
            End Get
            Set(ByVal value As Char)
                _extract_lote_11 = value
            End Set
        End Property
        Public Property extract_lote_12() As Char
            Get
                Return _extract_lote_12
            End Get
            Set(ByVal value As Char)
                _extract_lote_12 = value
            End Set
        End Property
        Public Property extract_lote_13() As Char
            Get
                Return _extract_lote_13
            End Get
            Set(ByVal value As Char)
                _extract_lote_13 = value
            End Set
        End Property
        Public Property extract_lote_14() As Char
            Get
                Return _extract_lote_14
            End Get
            Set(ByVal value As Char)
                _extract_lote_14 = value
            End Set
        End Property
        Public Property extract_lote_15() As Char
            Get
                Return _extract_lote_15
            End Get
            Set(ByVal value As Char)
                _extract_lote_15 = value
            End Set
        End Property
        Public Function ActualizarDetalle(ByVal pVal As String, ByVal pPos As Integer) As ListaOrdenada(Of cPosicionValorLoterias)
            Dim _valores As cPosicionValorLoterias
            Try
                For Each _valores In Me.Valores
                    If _valores.Posicion = pPos Then
                        _valores.Valor = pVal
                        Return Me.Valores
                        Exit Function
                    End If
                Next
                Return Nothing
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Function

        Public Sub New()
            Dim i As Integer
            Dim ls As New ListaOrdenada(Of cPosicionValorLoterias)
            Dim _Valor As cPosicionValorLoterias
            For i = 1 To 20
                _Valor = New cPosicionValorLoterias
                _Valor.Posicion = i
                _Valor.Valor = ""
                ls.Add(_Valor)
            Next
            Me.Valores = ls
        End Sub
    End Class
End Namespace