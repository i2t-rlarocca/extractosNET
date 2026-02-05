Imports Microsoft.VisualBasic
Imports System.Data

Namespace ExtractoBussiness

    Public Class ExtractoPoceada
        Public Shared Function GetExtracto(ByVal idSorteo As Integer) As ExtractoEntities.ExtractoPoceada

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoPoceada As New ExtractoEntities.ExtractoPoceada

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                _ExtractoPoceada = ExtractoData.ExtractoPoceada.GetExtracto(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, _Sorteo.Numero, _Sorteo.FechaHoraSorteo, True)

                Return _ExtractoPoceada

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetExtractoDT(ByVal idSorteo As Long) As DataSet
            Dim ds As New DataSet

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoPoceada As New ExtractoEntities.ExtractoPoceada

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                ds = ExtractoData.ExtractoPoceada.GetExtractoDT(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, idSorteo, _Sorteo.FechaHoraSorteo, True)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

    End Class
End Namespace

