Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Bussiness

    Public Class PozoBO

        Public Function getPozoTotal(ByVal idJuego As Int32, ByVal nroSorteo As Integer, ByRef idModalidad As Integer, ByRef tot_pozo As Double, ByRef tot_pozo_real As Double, ByRef tot_apu As Int64) As Boolean
            Try

                Dim oDal As New Data.PozoDAL
                Return oDal.getPozoTotal(idJuego, nroSorteo, idmodalidad, tot_pozo, tot_pozo_real, tot_apu)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function getPozo(ByVal idJuego As Int32, ByVal nroSorteo As Integer) As List(Of Pozo)
            Try
                Dim listaPozo As List(Of Pozo)
                Dim oDal As New Data.PozoDAL

                listaPozo = oDal.getPozo(idJuego, nroSorteo)
                Return listaPozo

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPozoReal(ByVal idJuego As Int32, ByVal nroSorteo As Integer) As List(Of Pozo)
            Try
                Dim listaPozo As List(Of Pozo)
                Dim oDal As New Data.PozoDAL

                listaPozo = oDal.getPozo(idJuego, nroSorteo)
                Return listaPozo

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPozoDT(ByVal idPgmConcurso As Integer) As DataTable
            Try
                Dim oDal As New Data.PozoDAL

                Return oDal.getPozoDT(idPgmConcurso)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function valida(ByVal oPozo As Pozo, ByRef msgRet As String) As Boolean
            msgRet = ""
            '           If oPozo.idJuego = 0 Then msgRet &= "Falta indicar el juego"
            '          If Trim(oAutoridad.Nombre) = "" Then msgRet &= "Falta indicar el nombre"
            '         If Trim(oAutoridad.cargo) = "" Then msgRet &= "Falta indicar el cargo"
            '        If oAutoridad.Orden = 0 Then msgRet &= "Falta indicar el orden"
            '
            If msgRet = "" Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function setPozo(ByVal oPozo As Pozo) As Boolean
            Try
                Dim oDal As New Data.PozoDAL

                Return oDal.setPozo(oPozo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function BorraPozo(ByVal idPgmSorteo As Integer) As Boolean
            Try
                Dim oDal As New Data.PozoDAL

                Return oDal.BorraPozo(idPgmSorteo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function setPozoSugerido(ByVal ImportePozo As Double, ByVal idUsuario As Long, ByVal idjuego As Integer, ByVal fechaModificacion As DateTime) As ListaOrdenada(Of cJuegoSorteo)
            Try
                Dim odal As New Data.PozoDAL
                Return odal.setPozoSugerido(ImportePozo, idUsuario, idjuego, fechaModificacion)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function setPozoSugerido(ByVal ImportePozo As Double, ByVal idUsuario As Long, ByVal idjuego As Long, ByVal fechaModificacion As Date) As Boolean
            Try
                Dim odal As New Data.PozoDAL
                Return odal.setPozoSugerido(ImportePozo, idUsuario, idjuego, fechaModificacion)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        '**22/10/2012
        Public Function getPozoSugerido(ByVal idjuego As Long, Optional ByRef fechamodificacion As DateTime = Nothing) As Double
            Try
                Dim odal As New Data.PozoDAL
                Return odal.getPozoSugerido(idjuego, fechamodificacion)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function getUsuarioPozoSugerido(ByVal idjuego As Long) As String
            Try
                Dim odal As New Data.PozoDAL
                Return odal.getUsuarioPozoSugerido(idjuego)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function setParametrosPozoEstimadoProximoSorteo(ByVal idpgmsorteo As Integer, ByVal apu_miercoles As String, ByVal apu_domingo As String, ByVal porc_apu_total_revancha As String, ByVal porc_apu_total_ss As String, ByVal porc_valor_apu_tradicional As String, ByVal porc_valor_apu_revancha As String, ByVal porc_valor_apu_ss As String, ByVal pozo_extra As String, ByVal pozo_adicional As String, ByVal minimoasegurado_tradicional As String, ByVal minimoasegurado_segunda As String, ByVal minimoasegurado_revancha As String, ByVal valorapuesta_tradicional As String, ByVal valorapuesta_revancha As String, ByVal valorapuesta_ss As String, ByVal porc_1premiotradicional As String, ByVal porc_2premiotradicional As String, ByVal porc_3premiotradicional As String, ByVal porc_estimulotradicional As String, ByVal porc_1premiorevancha As String, ByVal porc_estimulorevancha As String, ByVal porc_1premioSS As String, ByVal porc_estimuloSS As String, ByRef msj As String) As Boolean
            Try
                Dim odal As New Data.PozoDAL
                Return odal.setParametrosPozoEstimadoProximoSorteo(idpgmsorteo, apu_miercoles.Replace(",", "").Replace(".", ""), apu_domingo.Replace(",", "").Replace(".", ""), porc_apu_total_revancha.Replace(",", "").Replace(".", ""), porc_apu_total_ss.Replace(",", "").Replace(".", ""), porc_valor_apu_tradicional.Replace(",", "").Replace(".", ""), porc_valor_apu_revancha.Replace(",", "").Replace(".", ""), porc_valor_apu_ss.Replace(",", "").Replace(".", ""), CDec(pozo_extra), CDec(pozo_adicional), CDec(minimoasegurado_tradicional), CDec(minimoasegurado_segunda), CDec(minimoasegurado_revancha), valorapuesta_tradicional.Replace(",", "."), valorapuesta_revancha.Replace(",", "."), valorapuesta_ss.Replace(",", "."), porc_1premiotradicional.Replace(",", "").Replace(".", ""), porc_2premiotradicional.Replace(",", "").Replace(".", ""), porc_3premiotradicional.Replace(",", "").Replace(".", ""), porc_estimulotradicional.Replace(",", "").Replace(".", ""), porc_1premiorevancha.Replace(",", "").Replace(".", ""), porc_estimulorevancha.Replace(",", "").Replace(".", ""), porc_1premioSS.Replace(",", "").Replace(".", ""), porc_estimuloSS.Replace(",", "").Replace(".", ""), msj)
            Catch ex As Exception
                Throw New Exception(msj & " " & ex.Message)
                Return False
            End Try
        End Function
        Public Function getParametrosPozoEstimadoProximoSorteo(ByRef idpgmsorteo As Integer, ByRef apu_miercoles As String, ByRef apu_domingo As String, ByRef porc_apu_total_revancha As String, ByRef porc_apu_total_ss As String, ByRef porc_valor_apu_tradicional As String, ByRef porc_valor_apu_revancha As String, ByRef porc_valor_apu_ss As String, ByRef pozo_extra As String, ByRef pozo_adicional As String, ByRef minimoasegurado_tradicional As String, ByRef minimoasegurado_segunda As String, ByRef minimoasegurado_revancha As String, ByRef valorapuesta_tradicional As String, ByRef valorapuesta_revancha As String, ByRef valorapuesta_ss As String, ByRef porc_1premiotradicional As String, ByRef porc_2premiotradicional As String, ByRef porc_3premiotradicional As String, ByRef porc_estimulotradicional As String, ByRef porc_1premiorevancha As String, ByRef porc_estimulorevancha As String, ByRef porc_1premioSS As String, ByRef porc_estimuloSS As String) As Boolean
            Try
                Dim odal As New Data.PozoDAL
                Return odal.getParametrosPozoEstimadoProximoSorteo(idpgmsorteo, apu_miercoles, apu_domingo, porc_apu_total_revancha, porc_apu_total_ss, porc_valor_apu_tradicional, porc_valor_apu_revancha, porc_valor_apu_ss, pozo_extra, pozo_adicional, minimoasegurado_tradicional, minimoasegurado_segunda, minimoasegurado_revancha, valorapuesta_tradicional, valorapuesta_revancha, valorapuesta_ss, porc_1premiotradicional, porc_2premiotradicional, porc_3premiotradicional, porc_estimulotradicional, porc_1premiorevancha, porc_estimulorevancha, porc_1premioSS, porc_estimuloSS)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
    End Class
End Namespace