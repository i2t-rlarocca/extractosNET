Imports Microsoft.VisualBasic
Imports System.Data

Namespace ExtractoBussiness

    Public Class ExtractoLoteria
        Public Shared Function GetExtracto(ByVal idSorteo As Integer) As ExtractoEntities.ExtractoLoteria

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoLoteria As New ExtractoEntities.ExtractoLoteria

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                _ExtractoLoteria = ExtractoData.ExtractoLoteria.GetExtracto(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, _Sorteo.Numero, _Sorteo.FechaHoraSorteo, True)

                Return _ExtractoLoteria

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetExtractoDT(ByVal idSorteo As Long) As DataSet
            Dim ds As New DataSet
            'Dim dt As DataTable

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoLoteria As New ExtractoEntities.ExtractoLoteria

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                ds = ExtractoData.ExtractoLoteria.GetExtractoDT(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, idSorteo, _Sorteo.FechaHoraSorteo, True)

                'ds.Tables.Add(dt)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace

