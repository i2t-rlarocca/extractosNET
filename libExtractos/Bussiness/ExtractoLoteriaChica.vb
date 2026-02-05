Imports Microsoft.VisualBasic
Imports System.Data

Namespace ExtractoBussiness

    Public Class ExtractoLoteriaChica
        Public Shared Function GetExtracto(ByVal idSorteo As Integer) As ExtractoEntities.ExtractoLoteriaChica

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoLoteriaChica As New ExtractoEntities.ExtractoLoteriaChica

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                _ExtractoLoteriaChica = ExtractoData.ExtractoLoteriaChica.GetExtracto(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, _Sorteo.Numero, _Sorteo.FechaHoraSorteo, True)

                Return _ExtractoLoteriaChica

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetExtractoDT(ByVal idSorteo As Long) As DataSet
            Dim ds As New DataSet
            Dim dt As DataTable

            Try
                Dim _Sorteo As ExtractoEntities.Sorteo
                Dim _ExtractoLoteriaChica As New ExtractoEntities.ExtractoLoteriaChica

                _Sorteo = ExtractoData.Sorteo.GetSorteo(idSorteo)

                ds = ExtractoData.ExtractoLoteriaChica.GetExtractoDT(_Sorteo.JuegoLoteria.Loteria.Id, _Sorteo.JuegoLoteria.Juego.Id, idSorteo, _Sorteo.FechaHoraSorteo, True)

                'ds.Tables.Add(dt)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace

