Imports Microsoft.VisualBasic
Imports System.Data

Namespace ExtractoBussiness

    Public Class ExtractoBrinco

        Public Shared Function GetExtracto(ByVal idSorteo As Integer) As ExtractoEntities.ExtractoBrinco

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoBrinco As New ExtractoEntities.ExtractoBrinco

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                _ExtractoBrinco = ExtractoData.ExtractoBrinco.GetExtracto(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, _Sorteo.Numero, _Sorteo.FechaHoraSorteo, True)

                Return _ExtractoBrinco

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Shared Function GetExtractoDT(ByVal idSorteo As Integer) As DataSet
            Dim ds As New DataSet

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoBrinco As New ExtractoEntities.ExtractoBrinco

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                ds = ExtractoData.ExtractoBrinco.GetExtractoDT(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, idSorteo, _Sorteo.FechaHoraSorteo, True)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

    End Class
End Namespace