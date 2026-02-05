Imports Sorteos.Bussiness
Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Bussiness

    Public Class PremioBO

        Public Function getPremio(ByVal idJuego As Int32, ByVal nroSorteo As Integer) As List(Of Premio)
            Try
                Dim listaPremio As List(Of Premio)
                Dim oDal As New Data.PremioDAL

                listaPremio = oDal.getPremio(idJuego, nroSorteo)
                Return listaPremio

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function setPremio(ByVal oPremio As Premio) As Boolean
            Try
                Dim oDal As New Data.PremioDAL

                Return oDal.setPremio(oPremio)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function validaPremio(ByVal oPremio As Premio, ByRef msgRet As String) As Boolean
            Dim descPremio As String

            Try
                msgRet = ""

                descPremio = getDescripcionPremio(oPremio.idPremio)
                ' que el monto del premio por cupon no exceda al pozo
                If oPremio.importePremio > oPremio.importePozo And oPremio.importePozo <> 0 Then
                    msgRet = "Para el premio '" & descPremio & "' el monto del premio por cupón no puede exceder al monto del pozo."
                    Return False
                End If
                If oPremio.cantGanadores = 0 And oPremio.importePremio > 0 Then
                    msgRet = "El premio '" & descPremio & "'  esta vacante,no se pueden ingresar premios."
                    Return False
                End If

                If oPremio.cantGanadores > 0 And oPremio.importePremio <= 0 Then
                    msgRet = "Para el premio '" & descPremio & "' se debe cargar el monto del premio."
                    Return False
                End If


                ' que el monto del premio por cupón multilicado por la cantidad de ganadores se aproxime al pozo al máximo con tolerancia de $1 hacia abajo y hacia arriba
                'controlo la cuadratura solo si el importe de pozo es mayor a cero(se carga pozo)
                '**21/03/2012 ***
                ''If oPremio.importePozo > 0 Then
                ''    If oPremio.importePremio <> 0 And oPremio.cantGanadores <> 0 Then
                ''        If ((oPremio.importePremio * oPremio.cantGanadores) < oPremio.importePozo - 1) Or ((oPremio.importePremio * oPremio.cantGanadores) > oPremio.importePozo + 1) Then
                ''            msgRet = "Para el premio '" & descPremio & "' el monto consignado por cupón y/o la cantidad de ganadores podrían ser incorrectos."
                ''        End If
                ''    End If
                ''End If

                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Function getDescripcionPremio(ByVal idPremio As Integer) As String
            Try
                Dim oDal As New Data.PremioDAL

                Return oDal.getDescripcionPremio(idPremio)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return ""
            End Try
        End Function
        Public Function BorraPremioAdicional(ByVal opremio As Premio) As Boolean
            Try
                Dim oDal As New Data.PremioDAL
                Return oDal.BorraPremioAdicional(opremio)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Sub ObtieneDatosPremio(ByVal idPremio As Integer, ByRef CantAciertos As Integer, ByRef RequierAciertos As Integer, ByRef NombrePremio As String, ByVal Juego As Integer, ByVal Sorteo As Long, Optional ByRef Adic_Tipo As Integer = 1)
            Try
                Dim oDal As New Data.PremioDAL
                oDal.ObtieneDatosPremio(idPremio, CantAciertos, RequierAciertos, NombrePremio, Juego, Sorteo, Adic_Tipo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub
        '**22/10/2012***
        Public Function Tiene_ganadores_premio(ByVal idpgmsorteo As Long, ByVal idPremio As Integer) As Boolean
            Try
                Dim oDal As New Data.PremioDAL
                Return oDal.Tiene_Ganadores_sorteo(idpgmsorteo, idPremio)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function GuardarPozoEstimadoJuego(ByVal idjuego As Integer, ByVal PozoEstimado As Double, ByVal idusuario As Long) As ListaOrdenada(Of cJuegoSorteo)
            Dim oPozoBO As New PozoBO
            Dim oPgmSorteoBo As New PgmSorteoBO
            Dim oPgmSorteo As New PgmSorteo
            Dim oJuegoSorteosActualizarEnWeb As New ListaOrdenada(Of cJuegoSorteo)

            Try
                If PozoEstimado <= 0 Then
                    Throw New Exception(" GuardarPozoEstimadoJuego: se recibió un valor de Importe menor o igual a cero. No se guardará el valor de pozo estimado.")
                    Return Nothing
                End If
                ' primero actualizo en la BD del sorteador
                oJuegoSorteosActualizarEnWeb = oPozoBO.setPozoSugerido(PozoEstimado, idusuario, idjuego, Now)

                ' despues veo si tengo que actualizar la web
                If oJuegoSorteosActualizarEnWeb IsNot Nothing Then
                    ' Debo actualizar los pozos en web, de los sorteos devueltos en la lista
                    Try
                        For Each o As cJuegoSorteo In oJuegoSorteosActualizarEnWeb
                            oPgmSorteo = oPgmSorteoBo.getPgmSorteo(o.idPgmSorteo)
                            oPgmSorteoBo.ActualizarSorteoWeb(oPgmSorteo, oPgmSorteo.fechaHoraPrescripcion, oPgmSorteo.fechaHoraProximo, oPgmSorteo.fechaHoraIniReal, oPgmSorteo.fechaHora, oPgmSorteo.fechaHoraFinReal, oPgmSorteo.PozoEstimado)
                        Next
                    Catch ex As Exception
                    End Try
                End If
            Catch ex As Exception
                Throw New Exception("GuardarPozoEstimadoJuego:" & ex.Message)
            End Try
            Return oJuegoSorteosActualizarEnWeb
            ''Try
            ''    Dim oDal As New Data.PremioDAL
            ''    Return oDal.GuardarPozoEstimadoJuego(idjuego, PozoEstimado, idusuario)
            ''Catch ex As Exception
            ''    Throw New Exception("GuardarPozoEstimadoJuego:" & ex.Message)
            ''End Try
        End Function
        'obtenr premios desde WS
        Public Function getPremioWS(ByVal idpgmsorteo, ByVal idjuego) As Boolean
            Dim svc As New WSCAS.Pgmsorteos
            Dim odal As New Data.PremioDAL
            Dim boPremio As New PremioBO
            Dim usuarioBO As New UsuarioBO

            Dim msj As String = ""
            Dim pozoestimado As String = ""
            Try
                'Dim listaPremio As List(Of Premio)
                'Dim oDal As New Data.PremioDAL
                'listaPremio = oDal.getPremio(idJuego, nroSorteo)
                msj = svc.getpremios(idpgmsorteo, General.Origen)
                ' obtiene el PDF

                If msj.ToUpper = "OK" Then
                    If idjuego = 4 Or idjuego = 13 Or idjuego = 30 Then
                        pozoestimado = svc.getpozoestimado(idpgmsorteo)
                        Try
                            boPremio.GuardarPozoEstimadoJuego(idjuego, pozoestimado, usuarioBO.getUsuarioID("Sistema"))
                        Catch ex As Exception
                            FileSystemHelper.Log(" GetpremioWS: error al guardar pozo estimado:" & ex.Message)
                            MsgBox("Problemas al leer el Pozo Estimado para Próximo Sorteo. Verifique el archivo de premios con Operadores de Boldt, cierre y abra nuevamente la aplicación y vuelva a Obtener Premios.", MsgBoxStyle.Information)
                        End Try
                    End If
                    Return odal.InsertarPremiosdesdeWS(idpgmsorteo)
                Else
                    FileSystemHelper.Log("Problemas al obtener premios desde WS:" & msj)
                    Return False

                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
                FileSystemHelper.Log("Problemas al obtener premios desde WS:" & ex.Message)
                Return False
            End Try
        End Function

        Public Function getPozoEstimadoWS(ByVal idpgmsorteo) As Boolean
            Dim svc As New WSCAS.Pgmsorteos
            Dim odal As New Data.PremioDAL


            Dim msj As String = ""
            Try
                'Dim listaPremio As List(Of Premio)
                'Dim oDal As New Data.PremioDAL
                'listaPremio = oDal.getPremio(idJuego, nroSorteo)
                msj = svc.getpremios(idpgmsorteo, General.Origen)
                ' obtiene el PDF

                If msj.ToUpper <> "ERROR" Then

                    Return odal.InsertarPremiosdesdeWS(idpgmsorteo)
                Else
                    FileSystemHelper.Log("Problemas al obtener premios desde WS:" & msj)
                    Return False

                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
                FileSystemHelper.Log("Problemas al obtener premios desde WS:" & ex.Message)
                Return False
            End Try
        End Function
        'obtenr premios desde WS
        Public Function getPDF(ByVal juego As Integer, ByVal sorteo As Integer, ByRef msjret As String) As Boolean
            Dim svc As New WSCAS.Pgmsorteos



            Dim msj As String = ""
            Try
                'Dim listaPremio As List(Of Premio)
                'Dim oDal As New Data.PremioDAL
                'listaPremio = oDal.getPremio(idJuego, nroSorteo)
                msj = svc.getPDF(juego, sorteo, General.Origen)

                If msj.ToUpper = "OK" Then
                    Return True
                Else
                    FileSystemHelper.Log("Problemas al obtener PDF remotos:" & msj)
                    msjret = msj
                    Return False

                End If


                'Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

    End Class
End Namespace
