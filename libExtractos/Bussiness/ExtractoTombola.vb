Imports Microsoft.VisualBasic
Imports System.Data

Namespace ExtractoBussiness

    Public Class ExtractoTombola
        Public Shared Function GetExtracto(ByVal idSorteo As Integer) As ExtractoEntities.ExtractoTombola

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoTombola As New ExtractoEntities.ExtractoTombola

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                _ExtractoTombola = ExtractoData.ExtractoTombola.GetExtracto(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, _Sorteo.Numero, _Sorteo.FechaHoraSorteo, True)

                Return _ExtractoTombola

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetExtractoDT(ByVal idSorteo As Long) As DataSet
            Dim ds As New DataSet
            
            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoTombola As New ExtractoEntities.ExtractoTombola

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                ds = ExtractoData.ExtractoTombola.GetExtractoDT(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, idSorteo, _Sorteo.FechaHoraSorteo, True)

                'ds.Tables.Add(dt)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace

