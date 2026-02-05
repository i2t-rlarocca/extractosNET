Imports Microsoft.VisualBasic
Imports System.Data


Namespace ExtractoBussiness

    Public Class ExtractoQuiniela

        Private _Extracto_Lot1 As ExtractoEntities.ExtractoQuiniela
        Private _Extracto_Lot2 As ExtractoEntities.ExtractoQuiniela
        Private _Extracto_Lot3 As ExtractoEntities.ExtractoQuiniela
        Private _Extracto_Lot4 As ExtractoEntities.ExtractoQuiniela
        Private _Extracto_Lot5 As ExtractoEntities.ExtractoQuiniela
        Private _Sorteo As ExtractoEntities.Sorteo

        Public Property Extracto_Lot1() As ExtractoEntities.ExtractoQuiniela
            Get
                Return _Extracto_Lot1
            End Get
            Set(ByVal value As ExtractoEntities.ExtractoQuiniela)
                _Extracto_Lot1 = value
            End Set
        End Property

        Public Property Extracto_Lot2() As ExtractoEntities.ExtractoQuiniela
            Get
                Return _Extracto_Lot2
            End Get
            Set(ByVal value As ExtractoEntities.ExtractoQuiniela)
                _Extracto_Lot2 = value
            End Set
        End Property

        Public Property Extracto_Lot3() As ExtractoEntities.ExtractoQuiniela
            Get
                Return _Extracto_Lot3
            End Get
            Set(ByVal value As ExtractoEntities.ExtractoQuiniela)
                _Extracto_Lot3 = value
            End Set
        End Property

        Public Property Extracto_Lot4() As ExtractoEntities.ExtractoQuiniela
            Get
                Return _Extracto_Lot4
            End Get
            Set(ByVal value As ExtractoEntities.ExtractoQuiniela)
                _Extracto_Lot4 = value
            End Set
        End Property
        Public Property Extracto_Lot5() As ExtractoEntities.ExtractoQuiniela
            Get
                Return _Extracto_Lot5
            End Get
            Set(ByVal value As ExtractoEntities.ExtractoQuiniela)
                _Extracto_Lot5 = value
            End Set
        End Property

        Public Property Sorteo() As ExtractoEntities.Sorteo
            Get
                Return _Sorteo
            End Get
            Set(ByVal value As ExtractoEntities.Sorteo)
                _Sorteo = value
            End Set
        End Property

        Public Shared Function GetExtracto(ByVal idSorteo As Integer, ByVal idjuego As Integer) As ExtractoQuiniela

            Try
                Dim _ExtractoQuiniela As New ExtractoQuiniela
                Dim _loterias As List(Of ExtractoEntities.Loteria)
                Dim _extracto_aux As ExtractoEntities.ExtractoQuiniela
                Dim x As Integer


                'Incorpora datos del sorteo...
                _ExtractoQuiniela.Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)
                _loterias = ExtractoData.Loteria.GetLoterias

                Dim listaLot As List(Of String)
                listaLot = _ExtractoQuiniela.Sorteo.LotSorteo

                For Each lot As String In listaLot
                    _extracto_aux = ExtractoData.ExtractoQuiniela.GetExtracto(lot, _ExtractoQuiniela.Sorteo.JuegoLoteria.Juego.Id, _ExtractoQuiniela.Sorteo.Numero, _ExtractoQuiniela.Sorteo.FechaHoraSorteo, True)
                    If _extracto_aux.Numeros IsNot Nothing Then
                        x += 1
                        Select Case x
                            Case 1
                                _ExtractoQuiniela._Extracto_Lot1 = _extracto_aux
                            Case 2
                                _ExtractoQuiniela._Extracto_Lot2 = _extracto_aux
                            Case 3
                                _ExtractoQuiniela._Extracto_Lot3 = _extracto_aux
                            Case 4
                                _ExtractoQuiniela._Extracto_Lot4 = _extracto_aux
                            Case 5
                                _ExtractoQuiniela._Extracto_Lot5 = _extracto_aux
                        End Select
                    End If

                Next

                Return _ExtractoQuiniela

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
      
        Public Shared Function GetExtractoDT(ByVal idSorteo As Long) As DataSet
            Dim ds As New DataSet
            FileSystemHelperE.Log("idsorteo -> " & idSorteo & vbCrLf)
            Try
                Dim _ExtractoQuiniela As New ExtractoQuiniela
                'prueba
                'idSorteo = 2008411
                'Incorpora datos del sorteo...
                _ExtractoQuiniela.Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)
                Dim dt As New DataTable
                dt = ExtractoData.Loteria.Get_Loterias()
                Dim dtable As DataTable
                Dim i As Integer = 1

                For Each r As DataRow In dt.Rows
                    dtable = ExtractoData.ExtractoQuiniela.GetExtractoDT(r("idloteria"), _ExtractoQuiniela.Sorteo.JuegoLoteria.Juego.Id, idSorteo)
                    If dtable.Rows.Count > 0 Then
                        dtable.TableName = "Extracto_Loteria_" & i
                        i += 1
                        ds.Tables.Add(dtable)
                        dtable = Nothing
                    End If
                Next

                Return ds

            Catch ex As Exception

                Throw New Exception(ex.Message)
            End Try
        End Function
    

    End Class
End Namespace