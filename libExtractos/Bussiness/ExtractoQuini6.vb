Imports Microsoft.VisualBasic
Imports System.Data

Namespace ExtractoBussiness

    Public Class ExtractoQuini6

        Public Shared Function GetExtracto(ByVal idSorteo As Integer) As ExtractoEntities.ExtractoQuini6
            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoQuini6 As New ExtractoEntities.ExtractoQuini6

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                _ExtractoQuini6 = ExtractoData.ExtractoQuini6.GetExtracto(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, _Sorteo.Numero, _Sorteo.FechaHoraSorteo, True)

                Return _ExtractoQuini6

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Shared Function GetExtractoDT(ByVal idSorteo As Long) As DataSet
            Dim ds As New DataSet
            Dim dt As DataTable

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoQuini6 As New ExtractoEntities.ExtractoQuini6

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                ds = ExtractoData.ExtractoQuini6.GetExtractoDT(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, idSorteo, _Sorteo.FechaHoraSorteo, True)

                'ds.Tables.Add(dt)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetExtractoQ6ExtraDT(ByVal idSorteo As Long) As DataTable

            Dim dt As DataTable

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoQuini6 As New ExtractoEntities.ExtractoQuini6

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                dt = ExtractoData.ExtractoQuini6.GetExtractoQ6ExtraDT(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, idSorteo, _Sorteo.FechaHoraSorteo, True)


                Return dt

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

    End Class
End Namespace
