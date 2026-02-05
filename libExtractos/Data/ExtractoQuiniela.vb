Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb

Namespace ExtractoData

    Public Class ExtractoQuiniela

        Public Shared Function GetExtracto(ByVal idLoteria As String, _
                                           ByVal idJuego As String, _
                                           ByVal numeroSorteo As Integer, _
                                           ByVal fecha As String, _
                                           ByVal recuperarSorteo As Boolean) As ExtractoEntities.ExtractoQuiniela

            Try
                Dim _Extracto As ExtractoEntities.Extracto
                Dim _ExtractoQuinila As New ExtractoEntities.ExtractoQuiniela

                _Extracto = Extracto.GetExtracto(idLoteria, idJuego, numeroSorteo, recuperarSorteo)

                _ExtractoQuinila.CantidadCifras = _Extracto.CantidadCifras
                _ExtractoQuinila.HoraSorteoOrigen = _Extracto.HoraSorteoOrigen
                _ExtractoQuinila.Id = _Extracto.Id
                _ExtractoQuinila.Numeros = _Extracto.Numeros
                _ExtractoQuinila.NumeroSorteoOrigen = _Extracto.NumeroSorteoOrigen
                _ExtractoQuinila.Sorteo = _Extracto.Sorteo
                _ExtractoQuinila.Loteria = Loteria.GetLoteria(idLoteria)
                _ExtractoQuinila.FechaHoraCaducidadExtracto = _Extracto.FechaHoraCaducidadExtracto

                Return _ExtractoQuinila

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetExtractoDT(ByVal idLoteria As String, _
                                           ByVal idJuego As String, _
                                           ByVal idPgmSorteo As Long) As DataTable

            Try

                Dim dt As DataTable

                dt = Extracto.GetExtractoDT(idLoteria, idJuego, idPgmSorteo)
                Return dt

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

    End Class
End Namespace