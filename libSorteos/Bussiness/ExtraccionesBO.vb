Imports libEntities.Entities
Imports Sorteos.Data
Imports Sorteos.Helpers

Namespace Bussiness

    Public Class ExtraccionesBO

        Public Function Confirmar(ByRef o As ExtraccionesCAB, ByRef horaFinConcurso As DateTime) As Long

            If o Is Nothing Then
                Throw New Exception("Confirmar Extracción -> se ha pasado como parámetro una extraccion vacía.")
                Exit Function
            End If

            Try
                Dim proxEx As New ExtraccionesCAB
                Dim oDal As New Data.ExtraccionesDAL

                Return oDal.Confirmar(o, horaFinConcurso)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function Cargar(ByVal idPGMConcurso As Long, ByVal oModeloExtraccionDET As ModeloExtraccionesDet) As ExtraccionesCAB

            Try
                Dim o As New ExtraccionesCAB
                Dim oDal As New Data.ExtraccionesDAL
                o = oDal.Cargar(idPGMConcurso, oModeloExtraccionDET)
                Return o
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function InsertarActualizarExtraccionesDET(ByVal oextraccionesCAB As ExtraccionesCAB, ByVal pExtraccionesDET As ExtraccionesDET, Optional ByVal pModifica As Boolean = False) As Boolean
            Dim oDal As New Data.ExtraccionesDAL
            If oDal.InsertarActualizarExtraccionesDET(oextraccionesCAB, pExtraccionesDET, pModifica) Then
                InsertarActualizarExtraccionesDET = True
            Else
                InsertarActualizarExtraccionesDET = False
            End If
        End Function
        Public Sub ActualizarMetodoIngreso(ByVal pIdCabecera As Integer, ByVal pIdMetodoIngreso As Integer)
            Dim oDal As New Data.ExtraccionesDAL
            Try
                oDal.ActualizarMetodoIngreso(pIdCabecera, pIdMetodoIngreso)
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try

        End Sub

        Public Function ObtenerExtraccionesDET(ByVal pIdCabecera As Integer) As ListaOrdenada(Of ExtraccionesDET)
            Dim oDal As New Data.ExtraccionesDAL
            Dim ls As New ListaOrdenada(Of ExtraccionesDET)
            Try
                ls = oDal.getExtraccionesDET(pIdCabecera)
                If ls Is Nothing Then
                    Return Nothing
                Else
                    Return ls
                End If
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function


        Public Function getExtraccionDT(ByVal idPgmConcurso) As DataTable
            Dim oDal As New Data.ExtraccionesDAL
            Dim dt As DataTable

            Try
                dt = oDal.getExtraccionesDT(idPgmConcurso)
                Return dt

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function
        Public Function getExtraccionesQuini6(ByVal pidPgmConcurso As Integer) As ListaOrdenada(Of String)
            Dim oDal As New Data.ExtraccionesDAL
            Dim ls As ListaOrdenada(Of String)

            Try
                ls = oDal.getExtraccionesQuini6(pidPgmConcurso)
                Return ls
            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function BorrarExtraccionesDEt(ByVal ocabecera As ExtraccionesCAB, ByVal porden As Integer) As Boolean
            Dim oDal As New Data.ExtraccionesDAL
            Try
                Return (oDal.BorrarExtraccionesDEt(ocabecera, porden))

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function
        Public Function OrdenaPosicionExtractoExtraccionesDET(ByVal idExtraccionCAB As Long) As Boolean
            Dim oDal As New Data.ExtraccionesDAL
            Try
                Return oDal.OrdenaPosicionExtractoExtraccionesDET(idExtraccionCAB)

            Catch ex As Exception
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function
       
        Public Function GenerarExtracto(ByVal pCabecera As ExtraccionesCAB) As Boolean
            Dim oDal As New Data.ExtraccionesDAL
            If oDal.GenerarExtracto(pCabecera) Then
                GenerarExtracto = True
            Else
                GenerarExtracto = False
            End If
        End Function

        Public Function GenerarExtractoCompleto(ByVal idPgmConcurso As Long) As Boolean
            Dim oDal As New Data.ExtraccionesDAL
            If oDal.GenerarExtractoCompleto(idPgmConcurso) Then
                GenerarExtractoCompleto = True
            Else
                GenerarExtractoCompleto = False
            End If
        End Function

        Public Function getExtraccionesDET(ByVal idExtraccionCAB As Long) As ListaOrdenada(Of ExtraccionesDET)
            Dim oDal As New Data.ExtraccionesDAL
            Return oDal.getExtraccionesDET(idExtraccionCAB)
        End Function
        Public Function ObtenerFechasBolillas(ByVal pIdcabecera As Long, ByVal UltimaBolilla As Integer, ByRef FechaHoraPrimerBolilla As DateTime, ByRef fechaHoraUltimaBolilla As DateTime) As Boolean
            Dim oDal As New Data.ExtraccionesDAL
            If oDal.ObtenerFechasBolillas(pIdcabecera, UltimaBolilla, FechaHoraPrimerBolilla, fechaHoraUltimaBolilla) Then
                ObtenerFechasBolillas = True
            Else
                ObtenerFechasBolillas = False
            End If
        End Function

        '**05/11/2012
        Public Function GenerarExtractoJuegosDependientes(ByVal pIdpgmconcurso As Long, ByVal pidJuego As Long) As Boolean
            Dim oDal As New Data.ExtraccionesDAL
            If oDal.GenerarExtractoJuegosDependientes(pIdpgmconcurso, pidJuego) Then
                GenerarExtractoJuegosDependientes = True
            Else
                GenerarExtractoJuegosDependientes = False
            End If
        End Function

    End Class
End Namespace