Imports libEntities.Entities
Imports Sorteos.Data
Imports Sorteos.Helpers
Imports Sorteos.Extractos
Imports Sorteos.WSExtractos
Imports System.IO
Imports System.Security.Cryptography
Imports System.Xml
Imports System.Text


Namespace Bussiness

    Public Class PgmSorteoBO

        Public Function setParametrosSorteoPCA(ByVal idPgmSorteo As Integer) As Boolean
            Dim oDal As New Data.PgmSorteoDAL

            Try
                Return oDal.setParametrosSorteoPCA(idPgmSorteo)

            Catch ex As Exception

                Return Nothing
                MsgBox("No se pudieron obtener los parámetros de PC-A. Intente nuevamente o cárguelos manualmente.", MsgBoxStyle.Information)
            End Try
        End Function

        Public Function getProgresionLoteria(ByVal idJuego As Integer, ByVal nroSorteo As Long) As Integer

            Dim idPgmSorteo As Long = idJuego * 1000000 + nroSorteo
            Dim progresion As Long
            Dim oDal As New PgmSorteoDAL
            Dim extr As New WSExtractos.Extracto
            extr.Autoridad_1 = New WSExtractos.Autoridad
            extr.Autoridad_2 = New WSExtractos.Autoridad
            extr.Autoridad_3 = New WSExtractos.Autoridad
            extr.Autoridad_4 = New WSExtractos.Autoridad
            extr.Autoridad_5 = New WSExtractos.Autoridad

            extr.Numero_1 = New WSExtractos.Numero
            extr.Numero_2 = New WSExtractos.Numero
            extr.Numero_3 = New WSExtractos.Numero
            extr.Numero_4 = New WSExtractos.Numero
            extr.Numero_5 = New WSExtractos.Numero
            extr.Numero_6 = New WSExtractos.Numero
            extr.Numero_7 = New WSExtractos.Numero
            extr.Numero_8 = New WSExtractos.Numero
            extr.Numero_9 = New WSExtractos.Numero
            extr.Numero_10 = New WSExtractos.Numero
            extr.Numero_11 = New WSExtractos.Numero
            extr.Numero_12 = New WSExtractos.Numero
            extr.Numero_13 = New WSExtractos.Numero
            extr.Numero_14 = New WSExtractos.Numero
            extr.Numero_15 = New WSExtractos.Numero
            extr.Numero_16 = New WSExtractos.Numero
            extr.Numero_17 = New WSExtractos.Numero
            extr.Numero_18 = New WSExtractos.Numero
            extr.Numero_19 = New WSExtractos.Numero
            extr.Numero_20 = New WSExtractos.Numero
            extr.Numero_21 = New WSExtractos.Numero
            extr.Numero_22 = New WSExtractos.Numero
            extr.Numero_23 = New WSExtractos.Numero
            extr.Numero_24 = New WSExtractos.Numero
            extr.Numero_25 = New WSExtractos.Numero
            extr.Numero_26 = New WSExtractos.Numero
            extr.Numero_27 = New WSExtractos.Numero
            extr.Numero_28 = New WSExtractos.Numero
            extr.Numero_29 = New WSExtractos.Numero
            extr.Numero_30 = New WSExtractos.Numero
            extr.Numero_31 = New WSExtractos.Numero
            extr.Numero_32 = New WSExtractos.Numero
            extr.Numero_33 = New WSExtractos.Numero
            extr.Numero_34 = New WSExtractos.Numero
            extr.Numero_35 = New WSExtractos.Numero
            extr.Numero_36 = New WSExtractos.Numero

            extr.Letra_1 = New WSExtractos.Letra
            extr.Letra_2 = New WSExtractos.Letra
            extr.Letra_3 = New WSExtractos.Letra
            extr.Letra_4 = New WSExtractos.Letra
            extr.NumeroSorteo = nroSorteo
            extr.exporto = "N"
            '** 26/09/2013 hg si es entre Rios no se calcula progresion
            If General.Jurisdiccion = "E" Then
                Return 0
                Exit Function
            End If
            '** fin 26/09/2013

            ' extracciones
            If Not oDal.getExtraccciones(idJuego, idPgmSorteo, General.Jurisdiccion, extr) Then
                FileSystemHelper.Log("Error Progresion calculada:No se localizaron las extracciones.")
                Throw New Exception("getProgresionLoteria:No se localizaron las extracciones.")
                Return False
            End If
            Try
                progresion = calculaProgresionLoteria(Long.Parse(extr.Numero_1.Valor))
                FileSystemHelper.Log("Progresion calculada:" & progresion & " para el Nrto ingresado:" & Long.Parse(extr.Numero_1.Valor))
            Catch ex As Exception
                FileSystemHelper.Log("Error al calcular Progresion:" & ex.Message & " para el Nrto ingresado:" & Long.Parse(extr.Numero_1.Valor))
                Throw New Exception("getProgresionLoteria:Problema al calcular la progresion:" & ex.Message)

            End Try

            Return progresion
        End Function

        Public Function calculaProgresionLoteria(ByVal nro As Long) As Integer
            Dim progresion As Integer = -1
            Try
                If nro < 10 Then
                    progresion = nro
                Else
                    progresion = calculaProgresionLoteria(sumaCifras(nro))
                End If

                Return progresion

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return progresion
            End Try
        End Function

        Private Function sumaCifras(ByRef Nro As Long) As Long
            If Nro < 10 Then
                Return Nro
            Else
                Return (Nro Mod 10) + sumaCifras(Math.Floor(Nro / 10))
            End If
        End Function


        Public Function publicarDisplay(ByVal oSorteo As PgmSorteo, Optional ByVal forzarOffline As Boolean = False, Optional ByRef dioTimeout As Boolean = False) As Boolean
            Dim oDal As New PgmSorteoDAL
            Dim oJuegoBO As New JuegoBO
            Dim Nrointento As Integer = 1
            If General.PublicaDisplay = "N" Then
                Return True
            End If
            While Nrointento <= General.CantidadIntentos
                Try
                    oDal.publicarDisplay(oSorteo.idPgmConcurso, , forzarOffline)
                    '*** publicacion de otras jurisdicciones **********************
                    If oJuegoBO.esQuiniela(oSorteo.idJuego) Then
                        Dim letras As New List(Of Char)
                        Dim idloteria As Char
                        letras = getLetraPgmSorteoOtrasJurisdicciones(oSorteo.idPgmConcurso)
                        For Each idloteria In letras
                            FileSystemHelper.Log(". Publica Jurisdiccion->" & idloteria & "<- sorteo->" & oSorteo.idPgmSorteo & "<-")
                            publicarDisplayOtrasJurisdicciones(oSorteo, idloteria)
                        Next
                    End If
                    Return True
                    '**********fin de publicacion de otras jurisdicciones *********
                Catch ex As Exception
                    If InStr(UCase(ex.Message), "CADUCADO") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                        Try
                            FileSystemHelper.Log(" publicarDisplay_IntentoNro:" & Nrointento & " - " & ex.Message)
                        Catch ex1 As Exception
                        End Try
                        Nrointento = Nrointento + 1
                        dioTimeout = True
                    Else
                        Throw New Exception("Problema al publicarDISPLAY." & ex.Message)
                        Return False
                        Exit Function
                    End If
                End Try
            End While


        End Function

        Public Function publicarWEBRest(ByVal oSorteo As PgmSorteo, Optional ByVal forzarOFFLine As Boolean = False) As Boolean
            Dim oJuegoBO As New JuegoBO
            Dim sExt As String
            Dim sExtOtrJur As String
            Dim sPremios As String
            Dim sSueldos As String
            Dim sMinAseg As String
            Dim sValApu As String

            Try
                ' publico extracto propio
                sExt = publicarExtRest(oSorteo, forzarOFFLine)
                FileSystemHelper.Log("  publicarExtRest juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & "  -> " & sExt)
            Catch ex As Exception
                FileSystemHelper.Log("  publicarExtRest(catch) juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & "  -> " & ex.Message)
            End Try

            If ojuegobo.esquiniela(oSorteo.idJuego) Then
                Try
                    ' publico extracto otras jurisdicciones
                    sExtOtrJur = publicarExtOtrJurRest(oSorteo, forzarOFFLine)
                    FileSystemHelper.Log("  publicarExtOtrJurRest  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & sExtOtrJur)
                Catch ex As Exception
                    FileSystemHelper.Log("  publicarExtOtrJurRest(catch)  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & ex.Message)
                End Try
            End If

            If oSorteo.idJuego = 4 Or oSorteo.idJuego = 13 Or oSorteo.idJuego = 30 Or oSorteo.idJuego = 50 Or oSorteo.idJuego = 51 Then
                Try
                    ' publico premios
                    sPremios = publicarPremiosRest(oSorteo)
                    FileSystemHelper.Log("  publicarPremiosRest juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & sPremios)
                Catch ex As Exception
                    FileSystemHelper.Log("  publicarPremiosRest(catch)  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & ex.Message)
                End Try
            End If

            If oSorteo.idJuego = 13 Then
                Try
                    If oSorteo.nroSorteo < 1000 Then
                        ' publico premios sueldo
                        sSueldos = publicarSueldosRest(oSorteo)
                        FileSystemHelper.Log("  publicarSueldosRest  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & sSueldos)
                    End If
                Catch ex As Exception
                    FileSystemHelper.Log("  publicarSueldosRest(catch)  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & ex.Message)
                End Try
            End If
            Try
                ' publico minimos asegurados
                sMinAseg = publicarMinAseg(oSorteo)
                FileSystemHelper.Log("  publicarMinAseg  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & sMinAseg)
            Catch ex As Exception
                FileSystemHelper.Log("  publicarMinAseg(catch)  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & ex.Message)
            End Try

            Try
                ' publico valor apuesta
                sValApu = publicarValApuRest(oSorteo)
                FileSystemHelper.Log("  publicarValApuRest  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & sValApu)
            Catch ex As Exception
                FileSystemHelper.Log("  publicarValApuRest(catch)  juego: " & oSorteo.idJuego & " - sorteo: " & oSorteo.nroSorteo & " -> " & ex.Message)
            End Try


        End Function

        Public Function publicarExtRest(ByVal oSorteo As PgmSorteo, Optional ByVal forzarOFFLine As Boolean = False, Optional ByVal idLoteria As String = "") As String

            Dim sExt As DataTable
            Dim oDalSorteo As New PgmSorteoDAL
            If idLoteria = "" Then
                idLoteria = General.Jurisdiccion
            End If

            Dim url As String = ""
            Dim respuesta As String = ""
            Try
                sExt = oDalSorteo.getExtRest(oSorteo.idPgmSorteo, idLoteria, forzarOFFLine)

                url = General.urlRest + "/restCas/extracto/setExtractoCabecera"

                Dim uri As New Uri(url)
                Dim data As String = sExt.Rows(0).Item(0)
                Dim res As New restBO
                If data = "" Then
                    FileSystemHelper.Log("publicarExtRest - No se publica porque el stored devolvio vacio. opgmSorteo -> " & oSorteo.idPgmSorteo & " - estado: " & oSorteo.idEstadoPgmConcurso & ".")
                    Return ""
                End If
                FileSystemHelper.Log("publicarExtRest - cabecera: url: -->" & url & "<--.")
                FileSystemHelper.Log("publicarExtRest - cabecera: -->" & data & "<--.")
                respuesta = res.GetPOSTResponse(uri, data, "application/x-www-form-urlencoded; charset=UTF-8", "POST")

                Select Case oSorteo.idJuego
                    Case 4, 13
                        'QUINI 6/BRINCO 
                        url = General.urlRest + "/restQuini6/extractoQ6/setExtractoQ6"
                    Case 2, 3, 8, 49, 50
                        'QUINIELA NOCTURNO/QUINIELA VESPERTINA/QUINIELA MATITUNA/EL PRIMERO/LOTERIA TRADICIONAL
                        url = General.urlRest + "/restQuinielas/extractoQnl/setExtractoQNL"
                    Case 30, 1
                        'POCEADA FEDERAL/TOMBOLA
                        url = General.urlRest + "/restTombola/extractoTom/setExtractoTOM"
                End Select

                uri = New Uri(url)
                data = sExt.Rows(0).Item(1)
                FileSystemHelper.Log("publicarExtRest - extracto: --" & data & "--.")
                respuesta += " - " + res.GetPOSTResponse(uri, data, "application/x-www-form-urlencoded; charset=UTF-8", "POST")
                Return respuesta
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function anularExtRest(ByVal oSorteo As PgmSorteo, Optional ByVal forzarOFFLine As Boolean = False) As String

            Dim sExt As DataTable
            Dim oDalSorteo As New PgmSorteoDAL
            Dim idLoteria = General.Jurisdiccion
            Dim url As String = ""
            Dim respuesta As String
            Try
                sExt = oDalSorteo.getExtRest(oSorteo.idPgmSorteo, idLoteria, forzarOFFLine)

                url = General.urlRest + "/restCas/extracto/setExtractoCabecera"

                If url Is Nothing Then
                    Throw New Exception("Url vacia")
                    Return False
                End If

                Dim uri As New Uri(url)
                Dim data As String = sExt.Rows(0).Item(0)

                Dim ind As Integer = data.IndexOf("estado")
                Dim estfind As String = Mid(data, ind, 10)

                'data = data.Replace(estfind, """estado"":9")

                Dim res As New restBO
                respuesta = res.GetPOSTResponse(uri, data, "application/x-www-form-urlencoded; charset=UTF-8", "POST")
                Return respuesta

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function publicarValApuRest(ByVal oSorteo As PgmSorteo, Optional ByVal forzarOFFLine As Boolean = False) As String
            Dim oDalSorteo As New PgmSorteoDAL
            Dim retorno As DataTable
            Dim respuesta As String = ""
            Try
                retorno = oDalSorteo.getValApuRest(oSorteo.idPgmSorteo)
                Dim url As String

                url = General.urlRest + "/restCas/extracto/setValorApuesta"

                If url Is Nothing Then
                    Throw New Exception("Url vacia")
                    Return False
                End If

                Dim uri As New Uri(url)

                For Each r In retorno.Rows()
                    Dim data As String = r("resultado")
                    Dim res As New restBO
                    respuesta += " | " + res.GetPOSTResponse(uri, data, "application/x-www-form-urlencoded; charset=UTF-8", "POST")
                Next
                If respuesta = "" Then
                    Return "No hubo registros"
                Else
                    Return respuesta
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try


        End Function

        Public Function publicarPremiosRest(ByVal oSorteo As PgmSorteo, Optional ByVal forzarOFFLine As Boolean = False) As String
            Dim oDalSorteo As New PgmSorteoDAL
            Dim retorno As DataTable
            Dim respuesta As String = ""
            If Not oDalSorteo.NoTienePremiosCargados(oSorteo.idPgmSorteo, oSorteo.idJuego) Then
                Try
                    retorno = oDalSorteo.getPremiosRest(oSorteo.idPgmSorteo)

                    For Each r In retorno.Rows()
                        Dim uri As New Uri(General.urlRest + "/restCas/extracto/setPremioSorteo")
                        Dim data As String = r("resultado")
                        Dim res As New restBO
                        respuesta += " | " + res.GetPOSTResponse(uri, data, "application/x-www-form-urlencoded; charset=UTF-8", "POST")
                    Next
                    If respuesta = "" Then
                        Return "No hubo registros"
                    Else
                        Return respuesta
                    End If
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                End Try
            Else
                'logueo que no hay premios
                Return "No se registraron los premios para el sorteo indicado.  Publicación web cancelada. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso
            End If

        End Function

        Public Function publicarExtOtrJurRest(ByVal oSorteo As PgmSorteo, Optional ByVal forzarOFFLine As Boolean = False) As String
            Dim respuesta As String = ""
            Try
                For Each juris In oSorteo.ExtraccionesLoteria
                    If juris.Confirmada Then
                        If juris.Loteria.IdLoteria <> General.Jurisdiccion And Trim(juris.Loteria.IdLoteria) <> "" Then
                            respuesta += " | " + publicarExtRest(oSorteo, forzarOFFLine, juris.Loteria.IdLoteria)
                        End If
                    End If
                Next
                If respuesta = "" Then
                    Return "No hubo registros"
                Else
                    Return respuesta
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function publicarSueldosRest(ByVal oSorteo As PgmSorteo) As String
            Dim oDalSorteo As New PgmSorteoDAL
            Dim retorno As DataTable
            Dim respuesta As String = ""
            Try
                retorno = oDalSorteo.getSueldosRest(oSorteo.idPgmSorteo)

                For Each r In retorno.Rows()
                    Dim uri As New Uri(General.urlRest + "/restCas/extracto/setPremioBRSueldo")
                    Dim data As String = r("resultado")
                    Dim res As New restBO
                    respuesta += " | " + res.GetPOSTResponse(uri, data, "application/x-www-form-urlencoded; charset=UTF-8", "POST")
                Next
                If respuesta = "" Then
                    Return "No hubo registros"
                Else
                    Return respuesta
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function publicarMinAseg(ByVal oSorteo As PgmSorteo) As String
            Dim oDalSorteo As New PgmSorteoDAL
            Dim retorno As DataTable
            Dim respuesta As String = ""
            Try
                retorno = oDalSorteo.getMinAsegRest(oSorteo.idPgmSorteo)

                For Each r In retorno.Rows()
                    Dim uri As New Uri(General.urlRest + "/restCas/extracto/setMinimosAsegurados")
                    Dim data As String = r("resultado")
                    Dim res As New restBO
                    respuesta += " | " + res.GetPOSTResponse(uri, data, "application/x-www-form-urlencoded; charset=UTF-8", "POST")
                Next
                If respuesta = "" Then
                    Return "No hubo registros"
                Else
                    Return respuesta
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function publicarWEB(ByVal oSorteo As PgmSorteo, Optional ByVal porMenu As Boolean = False) As Boolean
            Dim ws As New ExtractoServicioClient

            Dim extr As WSExtractos.Extracto
            Try
                extr = New WSExtractos.Extracto
            Catch ex As Exception
                FileSystemHelper.Log(" publicarWEB. El error está al instanciar el WSExtractos. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
            End Try


            Dim boSorteo As New PgmSorteoBO
            Dim oDal As New PgmSorteoDAL

            Dim idPgmSorteo As Integer

            '**** controles
            Dim _continuar As Boolean = False
            Dim _ModoPublicacion As Integer = General.ModoPublicacion
            Dim _PublicaWeb As String = General.PublicaWeb.ToUpper
            Dim _PublicarWebON As String = General.PublicarWebON
            Dim _PublicarWebOFF As String = General.PublicarWebOFF
            If (_ModoPublicacion = 1 And _PublicaWeb = "N" And (_PublicarWebON = "S" Or _PublicarWebOFF = "S")) Or (_ModoPublicacion = 0 And (_PublicarWebON = "S" Or _PublicarWebOFF = "S")) Then
                _continuar = True
            End If
            If Not _continuar Then
                FileSystemHelper.Log(" La publicación a la Web no está habilitada.Parametros: ModoPublicacion:" & _ModoPublicacion & " ,PublicaWeb:" & _PublicaWeb & " ,PublicarWebON:" & _PublicarWebOFF & " ,PublicarWebOFF:" & _PublicarWebON)
                Return True
                Exit Function
            End If
            '***************

            idPgmSorteo = oSorteo.idPgmSorteo  'boSorteo.getPgmSorteoId(cmbJuego.SelectedValue, txtNroSorteo.Text)

            If idPgmSorteo = 0 Then
                FileSystemHelper.Log(" No existe el número de sorteo para el juego indicado.  Publicación web cancelada. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                Throw New Exception("No existe el número de sorteo para el juego indicado.  Publicación web cancelada.")
                Return False
                Exit Function
            End If

            If oDal.NoTienePremiosCargados(oSorteo.idPgmSorteo, oSorteo.idJuego) Then
                FileSystemHelper.Log(" No se registraron los premios para el sorteo indicado.  Publicación web cancelada. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                Return True
                Exit Function
            End If
            'If oDal.SinJurisdiccionesCargadas(idPgmSorteo) Then
            '    Return True
            '    Exit Function
            'End If

            extr.Autoridad_1 = New WSExtractos.Autoridad
            extr.Autoridad_2 = New WSExtractos.Autoridad
            extr.Autoridad_3 = New WSExtractos.Autoridad
            extr.Autoridad_4 = New WSExtractos.Autoridad
            extr.Autoridad_5 = New WSExtractos.Autoridad

            extr.Numero_1 = New WSExtractos.Numero
            extr.Numero_2 = New WSExtractos.Numero
            extr.Numero_3 = New WSExtractos.Numero
            extr.Numero_4 = New WSExtractos.Numero
            extr.Numero_5 = New WSExtractos.Numero
            extr.Numero_6 = New WSExtractos.Numero
            extr.Numero_7 = New WSExtractos.Numero
            extr.Numero_8 = New WSExtractos.Numero
            extr.Numero_9 = New WSExtractos.Numero
            extr.Numero_10 = New WSExtractos.Numero
            extr.Numero_11 = New WSExtractos.Numero
            extr.Numero_12 = New WSExtractos.Numero
            extr.Numero_13 = New WSExtractos.Numero
            extr.Numero_14 = New WSExtractos.Numero
            extr.Numero_15 = New WSExtractos.Numero
            extr.Numero_16 = New WSExtractos.Numero
            extr.Numero_17 = New WSExtractos.Numero
            extr.Numero_18 = New WSExtractos.Numero
            extr.Numero_19 = New WSExtractos.Numero
            extr.Numero_20 = New WSExtractos.Numero
            extr.Numero_21 = New WSExtractos.Numero
            extr.Numero_22 = New WSExtractos.Numero
            extr.Numero_23 = New WSExtractos.Numero
            extr.Numero_24 = New WSExtractos.Numero
            extr.Numero_25 = New WSExtractos.Numero
            extr.Numero_26 = New WSExtractos.Numero
            extr.Numero_27 = New WSExtractos.Numero
            extr.Numero_28 = New WSExtractos.Numero
            extr.Numero_29 = New WSExtractos.Numero
            extr.Numero_30 = New WSExtractos.Numero
            extr.Numero_31 = New WSExtractos.Numero
            extr.Numero_32 = New WSExtractos.Numero
            extr.Numero_33 = New WSExtractos.Numero
            extr.Numero_34 = New WSExtractos.Numero
            extr.Numero_35 = New WSExtractos.Numero
            extr.Numero_36 = New WSExtractos.Numero

            extr.Letra_1 = New WSExtractos.Letra
            extr.Letra_2 = New WSExtractos.Letra
            extr.Letra_3 = New WSExtractos.Letra
            extr.Letra_4 = New WSExtractos.Letra

            extr.FechaHoraCaducidad = oSorteo.fechaHoraPrescripcion
            extr.FechaHoraProximo = oSorteo.fechaHoraProximo

            If oSorteo.idEstadoPgmConcurso >= 30 Then
                extr.FechaHoraSorteo = oSorteo.fechaHoraIniReal
            Else
                extr.FechaHoraSorteo = oSorteo.fechaHora
            End If

            extr.Localidad = oSorteo.localidad
            extr.NumeroSorteo = oSorteo.nroSorteo
            extr.exporto = "N"
            extr.ConfirmadoParcial = oSorteo.ConfirmadoParcial
            extr.PozoProxEstimado = oSorteo.PozoEstimado


            ' autoridades
            Dim oAutoridad As libEntities.Entities.Autoridad
            If oSorteo.autoridades.Count > 0 Then
                For Each oAutoridad In oSorteo.autoridades
                    If oAutoridad.Orden = 1 Then
                        extr.Autoridad_1.Nombre = oAutoridad.Nombre.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").ToUpper
                        extr.Autoridad_1.Cargo = oAutoridad.cargo.ToUpper
                        extr.Autoridad_1.Firma = "sin_firma.png"
                        If General.Jurisdiccion = "E" Then
                            extr.Autoridad_1.Firma = oAutoridad.Firma
                        End If
                    End If
                    If General.Jurisdiccion = "E" Then
                        If oAutoridad.Orden = 3 Then
                            extr.Autoridad_2.Nombre = oAutoridad.Nombre.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").ToUpper
                            extr.Autoridad_2.Cargo = oAutoridad.cargo.ToUpper
                            extr.Autoridad_2.Firma = oAutoridad.Firma
                        End If
                    End If
                Next
                'extr.Autoridad_1.Nombre = oSorteo.autoridades(0).Nombre
                'extr.Autoridad_1.Cargo = oSorteo.autoridades(0).cargo
            Else
                extr.Autoridad_1.Nombre = ""
                extr.Autoridad_1.Cargo = ""
                extr.Autoridad_1.Firma = "sin_firma.png"
                If General.Jurisdiccion = "E" Then
                    extr.Autoridad_2.Nombre = ""
                    extr.Autoridad_2.Cargo = ""
                    extr.Autoridad_2.Firma = "sin_firma.png"
                End If
            End If

            ' el resto de las autoridades no se actualiza
            If General.Jurisdiccion = "S" Then
                extr.Autoridad_2.Nombre = ""
                extr.Autoridad_2.Cargo = ""
                extr.Autoridad_2.Firma = "sin_firma.png"
            End If

            extr.Autoridad_3.Nombre = ""
            extr.Autoridad_3.Cargo = ""
            extr.Autoridad_4.Nombre = ""
            extr.Autoridad_4.Cargo = ""
            extr.Autoridad_5.Nombre = ""
            extr.Autoridad_5.Cargo = ""

            '**22-3-2012 se establece el estado del sorteo
            If oSorteo.idEstadoPgmConcurso = 50 Then
                extr.estadoSorteo = 1
            Else
                extr.estadoSorteo = 0
            End If

            ' extracciones
            ' Primero vuelco Santa Fe
            If Not oDal.getExtraccciones(oSorteo.idJuego, oSorteo.idPgmSorteo, General.Jurisdiccion, extr) Then
                FileSystemHelper.Log(" No se localizaron los extractos. Publicación web cancelada. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                Throw New Exception("No se localizaron los extractos. Publicación web cancelada.")
                Return False
                Exit Function
            End If

            ' premios
            If Not oDal.getPremios(oSorteo.idJuego, oSorteo.idPgmSorteo, extr) Then
                FileSystemHelper.Log(" No se localizaron los premios. Publicación web cancelada. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                Throw New Exception("No se localizaron los premios. Publicación web cancelada.")
                Exit Function
            End If
            '14/01/2013
            '**minimos asegurados
            Dim juego As String = ""
            Select Case oSorteo.idJuego
                Case 4
                    juego = "Q2"
                Case 13
                    juego = "BR"
            End Select

            If Not oDal.getMinimosAsegurados(juego, extr) Then
                FileSystemHelper.Log(" No se localizaron los minimos asegurados para Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
            End If

            FileSystemHelper.Log(" Valores antes de llamar a setExtracto-> Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)

            Dim _nroIntento As Integer = 1
            While _nroIntento <= General.CantidadIntentos
                Try
                    If porMenu Then
                        ws.rePublicarExtracto(extr)
                        FileSystemHelper.Log(" salio setextracto por menu -> Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                    Else
                        ws.setExtracto(extr)
                        FileSystemHelper.Log(" salio de setextracto -> Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                    End If
                    Exit While
                Catch ex As Exception
                    FileSystemHelper.Log(Now & " publicarWeb - error en setExtracto _IntentoNro: " & _nroIntento & " - " & ex.Message)
                    If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                        FileSystemHelper.Log(Now & " publicarWeb_IntentoNro:" & _nroIntento & " - " & ex.Message)
                        _nroIntento = _nroIntento + 1
                    Else
                        FileSystemHelper.Log(" Problema al transferir datos para extracto. Publicación cancelada. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                        Throw New Exception("Problema al transferir datos para extracto. Publicación cancelada. " & ex.Message)
                        Return False
                        Exit Function
                    End If
                End Try
            End While
            ' genera el archivo
            Try
                setArchivo(extr)
            Catch ex As Exception
                FileSystemHelper.Log("Problema al generar testigo de web. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                'Throw New Exception("Problema al generar testigo de web. " & ex.Message)
                'Return False
                'Exit Function
            End Try

            ' Segundo vuelco otras jurisdicciones, si existen
            FileSystemHelper.Log(" antes de publicar otras loterias")
            Dim oexlot As pgmSorteo_loteria
            For Each oexlot In oSorteo.ExtraccionesLoteria
                FileSystemHelper.Log(" - setExtracto de Otras Jurisdicciones. Fecha confirmacion:" & oexlot.Fechaconfirmacion & " loteria:" & oexlot.Loteria.IdLoteria)
                If oexlot.Fechaconfirmacion.ToString("dd/MM/yyyy") <> "01/01/1999" Then
                    If oSorteo.idEstadoPgmConcurso >= 30 Then
                        extr.FechaHoraSorteo = oexlot.FechaHoraIniReal
                    Else
                        extr.FechaHoraSorteo = oexlot.FechaHoraLoteria
                    End If
                    If Not oDal.getExtraccciones(oSorteo.idJuego, oSorteo.idPgmSorteo, oexlot.Loteria.IdLoteria, extr) Then
                        FileSystemHelper.Log(" No se localizaron las extracciones. Publicación cancelada. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                        Throw New Exception("No se localizaron las extracciones. Publicación cancelada.")
                        Return False
                        Exit Function
                    End If

                    _nroIntento = 1
                    While _nroIntento <= General.CantidadIntentos
                        Try
                            If porMenu Then
                                ws.rePublicarExtracto(extr)
                            Else
                                ws.setExtracto(extr)
                            End If
                            Exit While
                        Catch ex As Exception
                            If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                                FileSystemHelper.Log(Now & " Otras jurisdicciones - publicarWeb_IntentoNro:" & _nroIntento & " - " & ex.Message)
                                _nroIntento = _nroIntento + 1
                            Else
                                FileSystemHelper.Log(" Problema al publicar a web  Otras jurisdicciones. Publicación cancelada. Loteria: " & oexlot.Loteria.IdLoteria & " Juego: " & oSorteo.idJuego & " Sorteo: " & oSorteo.nroSorteo & " Estado Concurso: " & oSorteo.idEstadoPgmConcurso)
                                Throw New Exception("Problema al publicar a web Otras jurisdicciones. Publicación cancelada. Loteria: " & oexlot.Loteria.IdLoteria & ex.Message)
                                Return False
                                Exit Function
                            End If
                        End Try
                    End While

                    ' genera el archivo
                    Try
                        setArchivo(extr)
                    Catch ex As Exception
                        FileSystemHelper.Log(" Problema al generar testigo de web Otras jurisdicciones. Juego:" & oSorteo.idJuego & " Sorteo:" & oSorteo.nroSorteo & " Estado Concurso:" & oSorteo.idEstadoPgmConcurso)
                        'Throw New Exception("Problema al generar testigo de web Otras jurisdicciones. " & ex.Message)
                        'Return False
                        'Exit Function
                    End Try
                End If
            Next

            Return True
        End Function


        Private Function setArchivo(ByVal oExtr As WSExtractos.Extracto) As Boolean
            Dim ret As Boolean = True

            Dim f As StreamWriter
            Dim archivo As String

            Dim lineaCAB As String = ""
            Dim lineaEXT As String = ""
            Dim lineaDAT As String = ""
            Dim lineaTOT As String = ""

            ' CAB   ext_loteria ext_juego   ext_sorteo  dia_fecha   mes_fecha   anio_fecha
            ' CAB   S   TM  1234    30  12  2011
            lineaCAB = "CAB" & vbTab _
                        & oExtr.Loteria & vbTab _
                        & oExtr.Juego & vbTab _
                        & oExtr.NumeroSorteo & vbTab _
                        & DatePart(DateInterval.Day, oExtr.FechaHoraSorteo) & vbTab _
                        & DatePart(DateInterval.Month, oExtr.FechaHoraSorteo) & vbTab _
                        & DatePart(DateInterval.Year, oExtr.FechaHoraSorteo) & vbCr

            lineaEXT = "EXT" & vbTab _
                        & oExtr.Loteria & vbTab _
                        & oExtr.Juego & vbTab _
                        & oExtr.NumeroSorteo & vbTab _
                        & DatePart(DateInterval.Day, oExtr.FechaHoraSorteo) & vbTab & DatePart(DateInterval.Month, oExtr.FechaHoraSorteo) & vbTab & DatePart(DateInterval.Year, oExtr.FechaHoraSorteo) & vbTab _
                        & DatePart(DateInterval.Day, oExtr.FechaHoraCaducidad) & vbTab & DatePart(DateInterval.Month, oExtr.FechaHoraCaducidad) & vbTab & DatePart(DateInterval.Year, oExtr.FechaHoraCaducidad) & vbTab _
                        & oExtr.Numero_1.Valor & vbTab & oExtr.Numero_2.Valor & vbTab & oExtr.Numero_3.Valor & vbTab & oExtr.Numero_4.Valor & vbTab & oExtr.Numero_5.Valor & vbTab _
                        & oExtr.Numero_6.Valor & vbTab & oExtr.Numero_7.Valor & vbTab & oExtr.Numero_8.Valor & vbTab & oExtr.Numero_9.Valor & vbTab & oExtr.Numero_10.Valor & vbTab _
                        & oExtr.Numero_11.Valor & vbTab & oExtr.Numero_12.Valor & vbTab & oExtr.Numero_13.Valor & vbTab & oExtr.Numero_14.Valor & vbTab & oExtr.Numero_15.Valor & vbTab _
                        & oExtr.Numero_16.Valor & vbTab & oExtr.Numero_17.Valor & vbTab & oExtr.Numero_18.Valor & vbTab & oExtr.Numero_19.Valor & vbTab & oExtr.Numero_20.Valor & vbTab _
                        & oExtr.Numero_21.Valor & vbTab & oExtr.Numero_22.Valor & vbTab & oExtr.Numero_23.Valor & vbTab & oExtr.Numero_24.Valor & vbTab & oExtr.Numero_25.Valor & vbTab _
                        & oExtr.Numero_26.Valor & vbTab & oExtr.Numero_27.Valor & vbTab & oExtr.Numero_28.Valor & vbTab & oExtr.Numero_29.Valor & vbTab & oExtr.Numero_30.Valor & vbTab _
                        & oExtr.Numero_31.Valor & vbTab & oExtr.Numero_32.Valor & vbTab & oExtr.Numero_33.Valor & vbTab & oExtr.Numero_34.Valor & vbTab & oExtr.Numero_35.Valor & vbTab _
                        & oExtr.Numero_36.Valor & vbTab _
                        & oExtr.Letra_1.Valor & vbTab & oExtr.Letra_2.Valor & vbTab & oExtr.Letra_3.Valor & vbTab & oExtr.Letra_4.Valor & vbTab _
                        & oExtr.exporto & vbTab _
                        & oExtr.Cifras & vbTab _
                        & DatePart(DateInterval.Hour, oExtr.FechaHoraSorteo) & vbTab & DatePart(DateInterval.Minute, oExtr.FechaHoraSorteo) & vbTab & DatePart(DateInterval.Second, oExtr.FechaHoraSorteo) & vbTab & "x.x." & vbTab _
                        & oExtr.Autoridad_1.Nombre & vbTab _
                        & oExtr.Localidad & vbTab _
                        & DatePart(DateInterval.Day, oExtr.FechaHoraProximo) & vbTab & DatePart(DateInterval.Month, oExtr.FechaHoraProximo) & vbTab & DatePart(DateInterval.Year, oExtr.FechaHoraProximo) & vbTab _
                        & DatePart(DateInterval.Hour, oExtr.FechaHoraProximo) & vbTab & DatePart(DateInterval.Minute, oExtr.FechaHoraProximo) & vbTab & DatePart(DateInterval.Second, oExtr.FechaHoraProximo) & vbTab & "x.x." & vbCr

            lineaTOT = "TOT" & vbTab _
                        & (CStr(CLng(IIf(oExtr.Numero_1.Valor.Trim = "", 0, oExtr.Numero_1.Valor)) + CLng(IIf(oExtr.Numero_2.Valor.Trim = "", 0, oExtr.Numero_2.Valor)) + CLng(IIf(oExtr.Numero_3.Valor.Trim = "", 0, oExtr.Numero_3.Valor)) + CLng(IIf(oExtr.Numero_4.Valor.Trim = "", 0, oExtr.Numero_4.Valor)) + CLng(IIf(oExtr.Numero_5.Valor.Trim = "", 0, oExtr.Numero_5.Valor)) + CLng(IIf(oExtr.Numero_6.Valor.Trim = "", 0, oExtr.Numero_6.Valor)) + CLng(IIf(oExtr.Numero_7.Valor.Trim = "", 0, oExtr.Numero_7.Valor)) + CLng(IIf(oExtr.Numero_8.Valor.Trim = "", 0, oExtr.Numero_8.Valor)) + CLng(IIf(oExtr.Numero_9.Valor.Trim = "", 0, oExtr.Numero_9.Valor)) + CLng(IIf(oExtr.Numero_10.Valor.Trim = "", 0, oExtr.Numero_10.Valor)) + _
                                CLng(IIf(oExtr.Numero_11.Valor.Trim = "", 0, oExtr.Numero_11.Valor)) + CLng(IIf(oExtr.Numero_12.Valor.Trim = "", 0, oExtr.Numero_12.Valor)) + CLng(IIf(oExtr.Numero_13.Valor.Trim = "", 0, oExtr.Numero_13.Valor)) + CLng(IIf(oExtr.Numero_14.Valor.Trim = "", 0, oExtr.Numero_14.Valor)) + CLng(IIf(oExtr.Numero_15.Valor.Trim = "", 0, oExtr.Numero_15.Valor)) + _
                                CLng(IIf(oExtr.Numero_16.Valor.Trim = "", 0, oExtr.Numero_16.Valor)) + CLng(IIf(oExtr.Numero_17.Valor.Trim = "", 0, oExtr.Numero_17.Valor)) + CLng(IIf(oExtr.Numero_18.Valor.Trim = "", 0, oExtr.Numero_18.Valor)) + CLng(IIf(oExtr.Numero_19.Valor.Trim = "", 0, oExtr.Numero_19.Valor)) + CLng(IIf(oExtr.Numero_20.Valor.Trim = "", 0, oExtr.Numero_20.Valor)) + _
                                CLng(IIf(oExtr.Numero_21.Valor.Trim = "", 0, oExtr.Numero_21.Valor)) + CLng(IIf(oExtr.Numero_22.Valor.Trim = "", 0, oExtr.Numero_22.Valor)) + CLng(IIf(oExtr.Numero_23.Valor.Trim = "", 0, oExtr.Numero_23.Valor)) + CLng(IIf(oExtr.Numero_24.Valor.Trim = "", 0, oExtr.Numero_24.Valor)) + CLng(IIf(oExtr.Numero_25.Valor.Trim = "", 0, oExtr.Numero_25.Valor)) + _
                                CLng(IIf(oExtr.Numero_26.Valor.Trim = "", 0, oExtr.Numero_26.Valor)) + CLng(IIf(oExtr.Numero_27.Valor.Trim = "", 0, oExtr.Numero_27.Valor)) + CLng(IIf(oExtr.Numero_28.Valor.Trim = "", 0, oExtr.Numero_28.Valor)) + CLng(IIf(oExtr.Numero_29.Valor.Trim = "", 0, oExtr.Numero_29.Valor)) + CLng(IIf(oExtr.Numero_30.Valor.Trim = "", 0, oExtr.Numero_30.Valor)) + _
                                CLng(IIf(oExtr.Numero_31.Valor.Trim = "", 0, oExtr.Numero_31.Valor)) + CLng(IIf(oExtr.Numero_32.Valor.Trim = "", 0, oExtr.Numero_32.Valor)) + CLng(IIf(oExtr.Numero_33.Valor.Trim = "", 0, oExtr.Numero_33.Valor)) + CLng(IIf(oExtr.Numero_34.Valor.Trim = "", 0, oExtr.Numero_34.Valor)) + CLng(IIf(oExtr.Numero_35.Valor.Trim = "", 0, oExtr.Numero_35.Valor)) + _
                                CLng(IIf(oExtr.Numero_36.Valor.Trim = "", 0, oExtr.Numero_36.Valor))))

            ' carga los premios
            Select Case oExtr.Juego
                Case "TM" ' 1	Tómbola
                    lineaDAT = "DAT"

                Case "N", "V", "M", "P", "U"
                    ' 2	Qnl. Nocturna
                    ' 3	Qnl. Vespertina
                    ' 8	Qnl. Matutina
                    ' 49 El Primero
                    ' 26 El Ultimo
                    lineaDAT = "DAT"

                Case "LO"
                    ' 50	Lotería Tradic.
                    lineaDAT = "DAT"

                Case "LC"
                    ' 51	Lotería Chica
                    lineaDAT = "DAT"

                Case "Q2", "BR"
                    ' 4	Quini 6 
                    '13	Brinco
                    lineaDAT = "DAT"
            End Select

            Try

                Dim gralDal As New GeneralDAL
                archivo = gralDal.getParametro("WEB", "PATH_TESTIGOS") & "\datos_" & oExtr.Loteria & "_" & oExtr.Juego & "_" & oExtr.NumeroSorteo & ".dat"
                FileSystemHelper.CrearPath(gralDal.getParametro("WEB", "PATH_TESTIGOS"))
                f = New StreamWriter(archivo, False)
                f.WriteLine(lineaCAB)
                f.WriteLine(lineaEXT)
                If lineaDAT <> "DAT" Then f.WriteLine(lineaDAT)
                f.WriteLine(lineaTOT)
                f.Close()
            Catch ex As Exception
                Try
                    f.Close()
                Catch ex2 As Exception
                End Try
                f = Nothing
                Throw New Exception(ex.Message)
            End Try
            Return ret
        End Function

        Public Function revertirWeb(ByVal oSorteo As PgmSorteo) As Boolean

            '**** controles
            Dim _continuar As Boolean = False
            Dim _ModoPublicacion As Integer = General.ModoPublicacion
            Dim _PublicaWeb As String = General.PublicaWeb.ToUpper
            Dim _PublicarWebON As String = General.PublicarWebON
            Dim _PublicarWebOFF As String = General.PublicarWebOFF
            If (_ModoPublicacion = 1 And _PublicaWeb = "N" And (_PublicarWebON = "S" Or _PublicarWebOFF = "S")) Or (_ModoPublicacion = 0 And (_PublicarWebON = "S" Or _PublicarWebOFF = "S")) Then
                _continuar = True
            End If
            If Not _continuar Then
                FileSystemHelper.Log(" La reversión Web no está habilitada. Parámetros: ModoPublicacion:" & _ModoPublicacion & " ,PublicaWeb:" & _PublicaWeb & " ,PublicarWebON:" & _PublicarWebON & " ,PublicarWebOFF:" & _PublicarWebOFF)
                Return True
                Exit Function
            End If
            '***************

            Dim ws As New ExtractoServicioClient
            Dim extr As New WSExtractos.Extracto

            Dim boSorteo As New PgmSorteoBO
            Dim oDal As New PgmSorteoDAL

            extr.Loteria = General.Jurisdiccion
            extr.Juego = oSorteo.idJuego
            extr.NumeroSorteo = oSorteo.nroSorteo

            Dim _nroIntento As Integer = 1
            While _nroIntento <= General.CantidadIntentos
                Try
                    ws.reAnularExtracto(extr)
                    'ws.eliminarExtracto(extr)
                    Exit While
                Catch ex As Exception
                    If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                        FileSystemHelper.Log(Now & " revertirWeb_IntentoNro:" & _nroIntento & " - " & ex.Message)
                        _nroIntento = _nroIntento + 1
                    Else
                        Throw New Exception("Problema al revertir web." & ex.Message)
                        Return False
                        Exit Function
                    End If
                End Try
            End While

            Return True
        End Function

        Public Function getPgmSorteos(ByVal idPgmConcurso As Integer) As ListaOrdenada(Of PgmSorteo)

            Dim oDal As New Data.PgmSorteoDAL
            Dim ls As New ListaOrdenada(Of PgmSorteo)
            Try
                ls = oDal.getPgmSorteos(idPgmConcurso)
                If ls Is Nothing Then
                    Return Nothing
                Else
                    Return ls
                End If
            Catch ex As Exception
                getPgmSorteos = Nothing
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function getPgmSorteo(ByVal idPgmSorteo As Long) As PgmSorteo

            Dim oDal As New Data.PgmSorteoDAL

            Try
                Return oDal.getPgmSorteo(idPgmSorteo)

            Catch ex As Exception

                Return Nothing
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function getUltimoConfirmado(Optional ByVal idJuego As Integer = -1) As PgmSorteo

            Dim oDal As New Data.PgmSorteoDAL

            Try
                Return oDal.getUltimoConfirmado(idJuego)

            Catch ex As Exception
                Throw New Exception("PgmSorteoBO->getUltimoConfirmado: " & ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getUltimoEnCurso() As PgmSorteo

            Dim oDal As New Data.PgmSorteoDAL

            Try

                Return oDal.getUltimoEnCurso()

            Catch ex As Exception
                Throw New Exception("PgmSorteoBO->getUltimoEnCurso: " & ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmSorteoId(ByVal idJuego As Integer, ByVal nroSorteo As Integer) As Integer

            Dim oDal As New Data.PgmSorteoDAL

            Try
                Return oDal.getPgmSorteoId(idJuego, nroSorteo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return 0

            End Try
        End Function
        Public Function getPgmSorteoIdSor(ByVal idJuego As Integer, ByVal nroSorteo As Integer) As String

            Dim oDal As New Data.PgmSorteoDAL

            Try
                Return oDal.getPgmSorteoIdSor(idJuego, nroSorteo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return 0

            End Try
        End Function

        Public Function getPgmSorteosDT(ByVal idPgmConcurso As Integer) As DataTable
            Dim oDal As New Data.PgmSorteoDAL

            Try
                Return oDal.getPgmSorteosDT(idPgmConcurso)

            Catch ex As Exception
                getPgmSorteosDT = Nothing
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function getPgmSorteoLoteria(ByVal idPgmSorteo As Integer) As ListaOrdenada(Of pgmSorteo_loteria)

            Dim oDal As New Data.PgmSorteo_LoteriaDAL
            Dim ls As New ListaOrdenada(Of pgmSorteo_loteria)
            Try
                ls = oDal.getSorteosLoteria(idPgmSorteo)
                If ls Is Nothing Then
                    Return Nothing
                Else
                    Return ls
                End If
            Catch ex As Exception
                getPgmSorteoLoteria = Nothing
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function GetJuegoSorteos(ByVal cantDias As Integer) As ListaOrdenada(Of cJuegoSorteo)
            Dim oDal As New Data.PgmSorteoDAL
            Dim ls As New ListaOrdenada(Of cJuegoSorteo)
            Try
                ls = oDal.GetJuegoSorteos(cantDias)
                If ls Is Nothing Then
                    Return Nothing
                Else
                    Return ls
                End If
            Catch ex As Exception
                GetJuegoSorteos = Nothing
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function GetJuegosSorteo(ByVal cantDias As Integer) As ListaOrdenada(Of cJuegosSorteo)
            Dim oDal As New Data.PgmSorteoDAL
            Dim ls As New ListaOrdenada(Of cJuegosSorteo)
            Try
                ls = oDal.GetJuegosSorteo(cantDias)
                If ls Is Nothing Then
                    Return Nothing
                Else
                    Return ls
                End If
            Catch ex As Exception
                GetJuegosSorteo = Nothing
                MsgBox(ex.Message, MsgBoxStyle.Information)
            End Try
        End Function

        Public Function GenerarListadoParametros(ByVal idPgmConcurso As Integer, ByVal idpgmsorteo As Integer, ByVal destino As String, ByVal PathDestino As String, ByVal msgret As String) As Boolean

        End Function

        Public Function GenerarListadoDifCuad(ByVal idPgmConcurso As Integer, ByVal idpgmsorteo As Integer, ByVal path_reporte As String, ByVal destino As String, ByRef PathDestino As String, ByVal cantidadCopias As Integer, ByRef msgRet As String) As Boolean
            Dim RETORNO As Boolean = False
            Try

                Dim dal As New PgmConcursoDAL
                Dim dt As DataTable
                Dim ds As New DataSet
                Dim oc As New PgmConcurso
                Dim dtExtra As DataTable
                Dim er As New ExtractoReporte
                Dim visualizar As String = "000000000000000000"
                Dim i As Integer

                ' Obtener los datos para el reporte
                dt = dal.ObtenerDatosListadoDifCuad(idPgmConcurso, idpgmsorteo, visualizar)
                dt.TableName = "Table1"
                ' concatena en un msj


                'dt.WriteXmlSchema("D:\Visual2008\SorteoCAS\DEV\SorteosCAS\bin\Debug\INFORMES\Listado1.xml")
                ds.Tables.Add(dt)
                oc = dal.getPgmConcurso(idPgmConcurso)
                For Each opgmsorteo In oc.PgmSorteos
                    If opgmsorteo.idPgmSorteo = oc.idPgmSorteoPrincipal Then
                        dtExtra = ExtractoData.Extracto.GetExtractoDT(General.Jurisdiccion, opgmsorteo.idJuego, opgmsorteo.idPgmSorteo)
                        dtExtra.TableName = "Table2"
                        If opgmsorteo.idJuego = 4 Then
                            er.getExtra(dtExtra)
                        End If
                        Exit For
                    End If
                Next
                ds.Tables.Add(dtExtra)

                ' Generar el reporte
                Dim reporte As New Listado
                For i = 1 To cantidadCopias
                    reporte.GenerarListadoDifCuad(ds, path_reporte, destino, PathDestino, msgRet, , , , visualizar)
                    'reporte.GenerarListadoExtracciones(ds, path_reporte, destino, PathDestino, msgRet, visualizar)
                Next
                RETORNO = True
            Catch ex As Exception
                msgRet = ex.Message
                RETORNO = False
            End Try
            Return RETORNO
        End Function

        Public Function GenerarListadoExtracciones(ByVal idPgmConcurso As Integer, ByVal idpgmsorteo As Integer, ByVal path_reporte As String, ByVal destino As String, ByRef PathDestino As String, ByRef msgRet As String) As Boolean
            Dim RETORNO As Boolean = False
            Try

                Dim dal As New PgmConcursoDAL
                Dim dt As DataTable
                Dim ds As New DataSet
                Dim oc As New PgmConcurso
                Dim dtExtra As DataTable
                Dim er As New ExtractoReporte
                Dim visualizar As String = "000000000000000000"

                ' Obtener los datos para el reporte
                dt = dal.ObtenerDatosListado(idPgmConcurso)
                dt.TableName = "Table1"
                'dt.WriteXmlSchema("D:\Visual2008\SorteoCAS\DEV\SorteosCAS\bin\Debug\INFORMES\Listado1.xml")
                ds.Tables.Add(dt)
                oc = dal.getPgmConcurso(idPgmConcurso)
                For Each opgmsorteo In oc.PgmSorteos
                    If opgmsorteo.idPgmSorteo = oc.idPgmSorteoPrincipal Then
                        dtExtra = ExtractoData.Extracto.GetExtractoDT(General.Jurisdiccion, opgmsorteo.idJuego, opgmsorteo.idPgmSorteo)
                        dtExtra.TableName = "Table2"
                        If opgmsorteo.idJuego = 4 Then
                            visualizar = er.getExtra(dtExtra)
                        End If
                        Exit For
                    End If
                Next
                ds.Tables.Add(dtExtra)

                ' Generar el reporte
                Dim reporte As New Listado
                'reporte.GenerarListadoExtracciones(ds, path_reporte, destino, PathDestino, msgRet)
                reporte.GenerarListadoExtracciones(ds, path_reporte, destino, PathDestino, msgRet, visualizar)
                RETORNO = True
            Catch ex As Exception
                msgRet = ex.Message
                RETORNO = False
            End Try
            Return RETORNO
        End Function
        'Public Function GenerarListadoExtractoOficial(ByVal idPgmConcurso As Integer, ByVal idpgmsorteo As Integer, ByVal destino As String, ByVal PathDestino As String, ByVal msgret As String) As Boolean
        Public Function GenerarListadoExtractoOficial(ByVal idPgmConcurso As Integer, ByVal PathReporte As String) As Boolean
            Dim RETORNO As Boolean = False
            Try

                Dim dal As New PgmConcursoDAL
                Dim dt As DataTable
                Dim ds As New DataSet
                Dim oc As New PgmConcurso
                Dim dtExtra As DataTable
                Dim er As New ExtractoReporte
                Dim visualizar As String = "000000000000000000"

                ' Obtener los datos para el reporte
                dt = dal.ObtenerDatosListado(idPgmConcurso)
                dt.TableName = "Table1"
                ds.Tables.Add(dt)
                oc = dal.getPgmConcurso(idPgmConcurso)
                For Each opgmsorteo In oc.PgmSorteos
                    If opgmsorteo.idPgmSorteo = oc.idPgmSorteoPrincipal Then
                        dtExtra = ExtractoData.Extracto.GetExtractoDT(General.Jurisdiccion, opgmsorteo.idJuego, opgmsorteo.idPgmSorteo)
                        dtExtra.TableName = "Table2"
                        If opgmsorteo.idJuego = 4 Then
                            visualizar = er.getExtra(dtExtra)
                        End If
                        Exit For
                    End If
                Next
                ds.Tables.Add(dtExtra)

                ' Generar el reporte
                Dim reporte As New Listado
                reporte.GenerarListadoMail(ds, PathReporte, visualizar)
                RETORNO = True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                RETORNO = False
            End Try
            Return RETORNO
        End Function

        Public Function ControlCuadratura(Optional ByVal voSorteo As PgmSorteo = Nothing, Optional ByRef msgRet As String = "", Optional ByRef SoloJurLocal As Boolean = False) As Boolean

            If General.Modo_Operacion.ToUpper = "PC-A" Then
                Dim oArchivoBold As New ArchivoBoldtBO
                Dim Path As String = General.CarpetaOrigenArchivosExtractoComparar

                Try
                    oArchivoBold.GenerarArchivoExtracto(voSorteo, Path, SoloJurLocal, False)

                    If Not oArchivoBold.CompararArchivoExtracto(voSorteo, Path, msgRet, SoloJurLocal) Then
                        Return False
                    End If
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                    Return False
                End Try
                Return True
            Else
                Return True
            End If

        End Function
        Public Function ActualizarEstadoSorteo(ByVal idpgmSorteo As Integer, ByVal estado As Integer) As Boolean
            Try

                Dim dal As New PgmSorteoDAL
                dal.setPgmSorteoEstado(idpgmSorteo, estado)
                Return True
            Catch ex As Exception
                Throw New Exception(" ActualizarEstadoSorteo: " & ex.Message)
                Return False
            End Try

        End Function

        Public Function getIdSor(ByRef oPgmSorteo As PgmSorteo) As String
            Dim oDal As New Data.PgmSorteoDAL

            Return oDal.getIdSor(oPgmSorteo)

        End Function


        Public Function getUrlExtractoOficial(ByVal oSorteo As PgmSorteo) As String
            Dim dc As New ExtractoServicioClient
            Dim sorteoDto As New pgmSorteoDto
            Dim url As String = ""

            sorteoDto.juego = oSorteo.idJuegoLetra
            sorteoDto.loteria = General.Jurisdiccion
            sorteoDto.numero = oSorteo.nroSorteo
            sorteoDto.estado = oSorteo.idEstadoPgmConcurso
            sorteoDto.fechaHoraSorteo = oSorteo.fechaHora
            sorteoDto.fechaHoraProximoSorteo = oSorteo.fechaHoraProximo
            sorteoDto.fechaHoraCaducidadSorteo = oSorteo.fechaHoraPrescripcion
            If oSorteo.idJuego = 4 Or oSorteo.idJuego = 13 Or oSorteo.idJuego = 30 Then
                If oSorteo.pozos.Count > 0 Then
                    sorteoDto.pozoProximoEstimado = oSorteo.pozos(0).importe
                Else
                    sorteoDto.pozoProximoEstimado = 0
                End If
            End If

            Dim _nroIntento As Integer = 1
            While _nroIntento <= General.CantidadIntentos
                Try
                    url = dc.ObtenerPathExtracto(sorteoDto)
                    Exit While
                Catch ex As Exception
                    If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                        FileSystemHelper.Log(" getUrlExtractoOficial_IntentoNro:" & _nroIntento & " - " & ex.Message)
                        _nroIntento = _nroIntento + 1
                    Else
                        Throw New Exception(" getUrlExtractoOficial:" & ex.Message)
                        Return Nothing
                    End If
                End Try
            End While

            Return url

        End Function

        Public Function getUrlExtractoOficialRest(ByVal oSorteo As PgmSorteo) As String
            Dim url As String = ""
            Dim psBO As New PgmSorteoBO
            url = General.prefijoRPTExtracto & "&param_ID_sor=" & psBO.getIdSor(oSorteo)
            Return url

        End Function

        Public Function AutorizarExtractoOficial(ByVal oSorteo As PgmSorteo, ByVal fechaHoraCaducidadSorteo As DateTime, ByVal fechaHoraProximoSorteo As DateTime, ByVal fechaHoraInicio As DateTime, ByVal fechaHoraSorteo As DateTime, ByVal fechaHoraFin As DateTime, ByVal pozoProximoEstimado As Double, Optional ByVal estado As Integer = 0) As Boolean
            '**** controles
            Dim _continuar As Boolean = False
            Dim _ModoPublicacion As Integer = General.ModoPublicacion
            Dim _PublicaWeb As String = General.PublicaWeb.ToUpper
            Dim _PublicarWebON As String = General.PublicarWebON
            Dim _PublicarWebOFF As String = General.PublicarWebOFF
            Dim _PublicarWebONOFF As Boolean = (_PublicarWebON = "S") Or (_PublicarWebOFF = "S")
            If (_ModoPublicacion = 1 And _PublicaWeb = "N" And _PublicarWebONOFF) Or (_ModoPublicacion = 0 And _PublicarWebONOFF) Then
                _continuar = True
            End If
            If Not _continuar Then ' Si no esta habilitada la publicacion a web lo unico que hago es anular a nivel local
                FileSystemHelper.Log(" Autorizar Extracto Oficial en web: La publicación a la Web no está habilitada.Parametros: ModoPublicacion:" & _ModoPublicacion & " ,PublicaWeb:" & _PublicaWeb & " ,PublicarWebON:" & _PublicarWebOFF & " ,PublicarWebOFF:" & _PublicarWebON)
                Return True
                Exit Function
            End If
            '***************

            Dim dc As New WSExtractos.ExtractoServicioClient
            Dim sorteoDto As WSExtractos.pgmSorteoDto
            Try

                sorteoDto = CargarSorteoDTO(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, pozoProximoEstimado, estado)

                Dim _nroIntento As Integer = 1
                While _nroIntento <= General.CantidadIntentos
                    Try
                        dc.ActualizarEstadoSorteo(sorteoDto)
                        Return True
                        Exit While
                    Catch ex As Exception
                        If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                            FileSystemHelper.Log(" AutorizarExtractoOficial_IntentoNro:" & _nroIntento & " - " & ex.Message)
                            _nroIntento = _nroIntento + 1
                        Else
                            Throw New Exception(ex.Message)
                            FileSystemHelper.Log(" AutorizarExtractoOficial:" & ex.Message)
                            Return False
                        End If
                    End Try
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
                FileSystemHelper.Log(" AutorizarExtractoOficial:" & ex.Message)
                Return False
            End Try
        End Function

        Public Function ActualizarSorteoWeb(ByVal oSorteo As PgmSorteo, ByVal fechaHoraCaducidadSorteo As DateTime, ByVal fechaHoraProximoSorteo As DateTime, ByVal fechaHoraInicio As DateTime, ByVal fechaHoraSorteo As DateTime, ByVal fechaHoraFin As DateTime, ByVal pozoProximoEstimado As Double) As Boolean
            Dim dc As New WSExtractos.ExtractoServicioClient
            Dim sorteoDto As WSExtractos.pgmSorteoDto
            Dim _nroIntento As Integer = 1
            Try
                sorteoDto = CargarSorteoDTO(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, pozoProximoEstimado)
                While _nroIntento <= General.CantidadIntentos
                    Try
                        dc.ActualizarParametrosSorteo(sorteoDto)
                        Return True
                    Catch ex As Exception
                        If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                            FileSystemHelper.Log(" ActualizarSorteoWeb_IntentoNro:" & _nroIntento & " - " & ex.Message)
                            _nroIntento = _nroIntento + 1
                        Else
                            Throw New Exception(ex.Message)
                            FileSystemHelper.Log("ActualizarSorteoWeb:" & ex.Message)
                            Return False
                        End If
                    End Try
                End While
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try

        End Function

        Public Function CargarSorteoDTO(ByVal oSorteo As PgmSorteo, ByVal fechaHoraCaducidadSorteo As DateTime, ByVal fechaHoraProximoSorteo As DateTime, ByVal fechaHoraInicio As DateTime, ByVal fechaHoraSorteo As DateTime, ByVal fechaHoraFin As DateTime, ByVal pozoProximoEstimado As Double, Optional ByVal estado As Integer = 0) As WSExtractos.pgmSorteoDto
            Dim sorteoDto As New WSExtractos.pgmSorteoDto

            sorteoDto.juego = oSorteo.idJuego
            sorteoDto.loteria = General.Jurisdiccion
            sorteoDto.numero = oSorteo.nroSorteo
            sorteoDto.fechaHoraSorteo = oSorteo.fechaHora
            sorteoDto.estado = estado
            sorteoDto.fechaHoraCaducidadSorteo = fechaHoraCaducidadSorteo
            sorteoDto.fechaHoraProximoSorteo = fechaHoraProximoSorteo 'FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
            sorteoDto.fechaHoraInicio = fechaHoraInicio 'FormatDateTime(oSorteo.fechaHoraIniReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
            sorteoDto.fechaHoraSorteo = fechaHoraSorteo 'FormatDateTime(oSorteo.fechaHora, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
            sorteoDto.fechaHoraFin = fechaHoraFin 'FormatDateTime(oSorteo.fechaHoraFinReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
            sorteoDto.pozoProximoEstimado = pozoProximoEstimado


            Return sorteoDto

        End Function

        Public Function ActualizarDatosSorteo(ByVal sorteo As PgmSorteo, ByVal pozo As Double) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.ActualizarDatosSorteo(sorteo, pozo)
            Catch ex As Exception
                Throw New Exception(" ActualizarDatosSorteo: " & ex.Message)
                Return False
            End Try

        End Function

        Public Function NoTienePremiosCargados(ByVal pidPgmsorteo As Integer, ByVal pidJuego As Integer) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.NoTienePremiosCargados(pidPgmsorteo, pidJuego)
            Catch ex As Exception
                Throw New Exception(" NoTienePremios: " & ex.Message)

            End Try
        End Function

        Public Function SinJurisdiccionesCargadas(ByVal pIdPgmsorteo As Integer) As Boolean
            Try


                Dim dal As New PgmSorteoDAL
                Return dal.SinJurisdiccionesCargadas(pIdPgmsorteo)

            Catch ex As Exception
                Throw New Exception(" SinJurisdiccionesCargadas: " & ex.Message)

            End Try
        End Function

        '** confirmar solo QLA LOCAL
        Public Function ConfirmarQuinielaSF(ByVal oSorteo As PgmSorteo, ByVal fechaHoraCaducidadSorteo As Date, ByVal fechaHoraProximoSorteo As Date, ByVal fechaHoraInicio As Date, ByVal fechaHoraSorteo As Date, ByVal fechaHoraFin As Date) As Boolean
            Dim dal As New PgmSorteoDAL
            ' Solo ejecuto la parte que se conecta a web vieja si esta habilitada la publicacion vieja. 
            Dim _PublicarWebON As String = General.PublicarWebON
            Dim _PublicarWebOFF As String = General.PublicarWebOFF
            Dim _PublicarWebONOFF As Boolean = (_PublicarWebON = "S") Or (_PublicarWebOFF = "S")

            If _PublicarWebONOFF Then
                ' Esta habilitada la publicacion a web vieja. Por lo tanto intento anular SF y si ok, anulo a nivel local, como antes.
                Dim dc As New WSExtractos.ExtractoServicioClient
                Dim sorteoDto As WSExtractos.pgmSorteoDto
                Dim _nroIntento As Integer = 1
                Try
                    sorteoDto = CargarSorteoDTO(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, 0)
                    sorteoDto.ConfirmadoParcial = 1
                    While _nroIntento <= General.CantidadIntentos
                        Try
                            dc.ConfirmarOAnularQuinielaSF(sorteoDto)
                            Exit While
                        Catch ex As Exception
                            If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                                FileSystemHelper.Log(" ConfirmarQuinielaSF_IntentoNro:" & _nroIntento & " - " & ex.Message)
                                _nroIntento = _nroIntento + 1
                            Else
                                Throw New Exception(ex.Message)
                                FileSystemHelper.Log(" ConfirmarQuinielaSF:" & ex.Message)
                                Return False
                            End If
                        End Try
                    End While
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                    Return False
                End Try
            End If
            '-------------------------------
            ' Actualiza campo confirmadaParcial
            Try
                FileSystemHelper.Log("Confirmación de Qla LOCAL - Actualiza campo confirmadaParcial al sorteo: " & oSorteo.idPgmSorteo)
                dal.ConfirmarQuinielaSF(oSorteo.idPgmSorteo)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
            '-------------------------------
            ' Genera archivo para intercambio con otras jurisdicciones
        End Function

        Public Function AnularQuinielaSF(ByVal oSorteo As PgmSorteo, ByVal fechaHoraCaducidadSorteo As Date, ByVal fechaHoraProximoSorteo As Date, ByVal fechaHoraInicio As Date, ByVal fechaHoraSorteo As Date, ByVal fechaHoraFin As Date) As Boolean
            Dim dal As New PgmSorteoDAL
            ' Solo ejecuto la parte que se conecta a web vieja si esta habilitada la publicacion vieja. 
            Dim _PublicarWebON As String = General.PublicarWebON
            Dim _PublicarWebOFF As String = General.PublicarWebOFF
            Dim _PublicarWebONOFF As Boolean = (_PublicarWebON = "S") Or (_PublicarWebOFF = "S")

            If _PublicarWebONOFF Then
                ' Esta habilitada la publicacion a web vieja. Por lo tanto intento anular SF y si ok, anulo a nivel local, como antes.
                Dim dc As New WSExtractos.ExtractoServicioClient
                Dim sorteoDto As WSExtractos.pgmSorteoDto
                Dim _nroIntento As Integer = 1
                Try
                    sorteoDto = CargarSorteoDTO(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, 0)
                    sorteoDto.ConfirmadoParcial = 0
                    While _nroIntento <= General.CantidadIntentos
                        Try
                            dc.ConfirmarOAnularQuinielaSF(sorteoDto)
                            Exit While
                        Catch ex As Exception
                            If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                                FileSystemHelper.Log(" AnularQuinielaSF_IntentoNro:" & _nroIntento & " - " & ex.Message)
                                _nroIntento = _nroIntento + 1
                            Else
                                Throw New Exception(ex.Message)
                                FileSystemHelper.Log(" AnularQuinielaSF:" & ex.Message)
                                Return False
                            End If
                        End Try
                    End While
                Catch ex As Exception
                    Throw New Exception(ex.Message)
                    Return False
                End Try
            End If
            '-------------------------------
            ' Actualiza campo confirmadaParcial
            Try
                FileSystemHelper.Log("Anulación de confirmada Parcial: actualiza campo confirmadaParcia a 0.")
                dal.AnularQuinielaSF(oSorteo.idPgmSorteo)
                Return True
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
            '-------------------------------
        End Function

        Public Function NoTieneAutoridades(ByVal pidPgmsorteo As Integer) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.NoTieneAutoridades(pidPgmsorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function NoTieneAutoridadDelSorteo(ByVal pidPgmsorteo As Integer) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.NoTieneAutoridadDelSorteo(pidPgmsorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function
        Public Function NoTienePozos(ByVal pidPgmsorteo As Integer, ByRef pIdConcurso As Integer) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.NoTienepozos(pidPgmsorteo, pIdConcurso)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function NoTieneAEscribano(ByVal pidPgmsorteo As Integer) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.NoTieneAutoridadEscribano(pidPgmsorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getPgmConcursoId(ByVal idpgmSorteo As Long) As Long

            Dim oDal As New Data.PgmSorteoDAL

            Try
                Return oDal.getPgmConcursoId(idpgmSorteo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return 0

            End Try
        End Function
        '**06/11/2012
        Public Sub ActualizaProgresionLoteria(ByVal Progresion As Long, ByVal pidPgmsorteo As Long)
            Dim oDal As New Data.PgmSorteoDAL
            Try
                oDal.ActualizaProgresionLoteria(Progresion, pidPgmsorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub
        '**22/11/2012
        Public Function getEstadoPgmsorteo(ByVal pidPgmsorteo As Long) As Integer
            Dim oDal As New Data.PgmSorteoDAL
            Try
                Return oDal.getEstadoPgmSorteo(pidPgmsorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        '**27/11/2012
        Public Function NoTieneOtrasJurisdicciones(ByVal pidPgmsorteo As Integer) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.NoTieneOtrasJurisdicciones(pidPgmsorteo)
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function OtrasJurisdicciones_SinConfirmar(ByVal pidPgmsorteo As Integer) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.OtrasJurisdicciones_SinConfirmar(pidPgmsorteo)
            Catch ex As Exception
                Return False
                Throw New Exception(ex.Message)
            End Try
        End Function
        '*** volcado de otras loterias
        Public Function publicarDisplayOtrasJurisdicciones(ByVal oSorteo As PgmSorteo, ByVal idloteria As Char) As Boolean
            Dim oDal As New PgmSorteoDAL
            Dim Nrointento As Integer = 1
            While Nrointento <= General.CantidadIntentos
                Try
                    oDal.publicarDisplayOtrasJurisdicciones(oSorteo.idPgmConcurso, idloteria)
                    Return True
                Catch ex As Exception
                    If InStr(UCase(ex.Message), "CADUCADO") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                        FileSystemHelper.Log(" publicarDisplayOtrasJurisdicciones_IntentoNro:" & Nrointento & " - " & ex.Message)
                        Nrointento = Nrointento + 1
                    Else
                        Throw New Exception("Problema al publicarDISPLAYOtrasJurisdicciones." & ex.Message)
                        Return False
                        Exit Function
                    End If
                End Try
            End While

        End Function
        Public Function getLetraPgmSorteoOtrasJurisdicciones(ByVal opgmsorteo As PgmSorteo) As List(Of Char)
            Dim oDal As New PgmSorteoDAL
            Return oDal.getLetraSorteoOtrasJurisdicciones(opgmsorteo.idPgmConcurso)
        End Function
        Public Function NoTienePremiosSueldosCargados(ByVal pidPgmsorteo As Integer, ByVal pidJuego As Integer) As Boolean
            Try
                Dim dal As New PgmSorteoDAL
                Return dal.NoTienePremiosSueldosCargados(pidPgmsorteo, pidJuego)
            Catch ex As Exception
                Throw New Exception(" NoTienePremiosSueldosCargados: " & ex.Message)

            End Try
        End Function
        Function getLetraPgmSorteoOtrasJurisdicciones(ByVal idPgmConcurso) As List(Of Char)
            Dim oDal As New Data.PgmSorteoDAL
            Return oDal.getLetraSorteoOtrasJurisdicciones(idPgmConcurso)
        End Function
        '***26/11/2013
        Public Function CantidadOtrasJurisdicciones(ByVal IdPgmsorteo As Integer) As Integer
            Dim oDal As New Data.PgmSorteoDAL
            Return oDal.CantidadJurisdicciones(IdPgmsorteo)
        End Function

        ' ws 
        Public Function obtenerWSpgmsorteos(ByVal fechahora As String, ByRef msj As String) As Boolean
            Try
                ' Dim svc As New Services.SimpleService
                Dim svc As New WSCAS.Pgmsorteos

                Dim odal As New Data.PgmSorteoDAL


                Dim s As String = svc.getpgmsorteos(fechahora, General.Origen)

                If s.ToUpper = "OK" Then
                    FileSystemHelper.Log("*** funcion getpgmsorteos de WS termino OK,va a llamar al SP CrearProgramaSorteo")
                    Return odal.CrearProgramaSorteo()
                    FileSystemHelper.Log("*** ejecucion SP CrearProgramaSorteo OK")
                Else
                    msj = s.ToUpper
                    FileSystemHelper.Log("*** funcion getpgmsorteos de WS termino con problemas:" & msj)
                    Return False

                End If
            Catch ex As Exception
                FileSystemHelper.Log(" Problema al obtener sorteos desde WS: " & ex.Message)
                Throw New Exception(" Problema al obtener sorteos desde WS: " & ex.Message)
            End Try
        End Function

        Public Function setSorteadoPgmconcurso(ByVal idpgmsorteo As Long) As Boolean
            Try

           
                Dim svc As New WSCAS.Pgmsorteos

                Dim s As String = svc.setsorteado(idpgmsorteo)

                If s.ToUpper = "OK" Then
                    Return True
                Else

                    Return False

                End If
            Catch ex As Exception
                Throw New Exception(" Problemas al setear concurso como sorteaod en WS: " & ex.Message)
            End Try
        End Function
        Public Function obtenerPozosWS(ByVal idpgmsorteo As Integer) As Boolean
            Try
                Dim svc As New WSCAS.Pgmsorteos
                Dim s As Object = svc.getpozos(idpgmsorteo, General.Origen)

                If s.ToUpper = "OK" Then
                    Return True
                Else
                    FileSystemHelper.Log("obtenerPozosWS:Problemas al cargar pozos desde WS : " & s)
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(" Problemas al obtener pozos desde WS: " & ex.Message)
                FileSystemHelper.Log("obtenerPozosWS:Problemas al cargar pozos desde WS : " & ex.Message)
            End Try
        End Function
        Public Function MostrarListadoDiferencias(ByVal idPgmConcurso As Integer, ByVal idpgmsorteo As Integer) As String
            Dim dal As New PgmConcursoDAL
            Dim dt As DataTable
            Dim visualizar As String = "000000000000000000"
            Dim msj As String = ""
            Try
                dt = dal.ObtenerDatosListadoDifCuad(idPgmConcurso, idpgmsorteo, visualizar)
                For Each r As DataRow In dt.Rows
                    msj = msj & r("aud_detalle") & vbCrLf

                Next
                MostrarListadoDiferencias = msj

            Catch ex As Exception
                Throw New Exception(" MostrarListadoDiferencias: " & ex.Message)

            End Try

        End Function

        Public Function InsertarPgmsorteosContingencia(ByVal idpgmsorteo As Integer, ByVal idestado As Integer, ByVal idjuego As Integer, ByVal sorteo As Integer, ByVal fecha_hora As String, ByVal fecha_horapres As String, ByVal fecha_horaprox As String) As Boolean
            Try
                Dim odal As New PgmSorteoDAL

                Return odal.InsertarPgmsorteosContingencia(idpgmsorteo, idestado, idjuego, sorteo, fecha_hora, fecha_horapres, fecha_horaprox)

            Catch ex As Exception
                Throw New Exception(" insertarpgmsorteoscontingencia: " & ex.Message)
            End Try

        End Function
        '**29/12/2016
        Public Function RecuperaProgresionLoteria(ByVal pidPgmsorteo As Long) As Integer
            Dim oDal As New Data.PgmSorteoDAL
            Try
                Return oDal.RecuperaProgresionLoteria(pidPgmsorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function exigirPdfPrimerPremio(ByVal idPgmSorteo As Integer) As Boolean
            Dim oDal As New Data.PgmSorteoDAL
            Dim exigir As Boolean = False
            Try
                exigir = oDal.exigirPdfPrimerPremio(idPgmSorteo)
            Catch ex As Exception
                FileSystemHelper.Log("PgmSorteoBo.exigirPdfPrimerPremio: Excepción ->" & ex.Message & "<-")
                Throw New Exception("Problema al verificar PDF Ganadores del Primer Premio.")
            End Try
            Return exigir
        End Function

        Public Function enviarPdfPrimerPremio(ByVal idPgmSorteo As Integer) As Boolean
            Dim oDal As New Data.PgmSorteoDAL
            Dim enviar As Boolean = False
            Try
                enviar = oDal.enviarPdfPrimerPremio(idPgmSorteo)
            Catch ex As Exception
                FileSystemHelper.Log("PgmSorteoBo.enviarPdfPrimerPremio: Excepción ->" & ex.Message & "<-")
                Throw New Exception("Problema al verificar si se debe enviar PDF Ganadores del Primer Premio.")
            End Try
            Return enviar
        End Function

        Public Function exigirPdfDistribPcias(ByVal idPgmSorteo As Integer) As Boolean
            Dim oDal As New Data.PgmSorteoDAL
            Dim exigir As Boolean = False
            Try
                exigir = oDal.exigirPdfDistribPcias(idPgmSorteo)
            Catch ex As Exception
                FileSystemHelper.Log("PgmSorteoBo.exigirPdfDistribPcias: Excepción ->" & ex.Message & "<-")
                Throw New Exception("Problema al verificar PDF Distribución de Premios por Provincia.")
            End Try
            Return exigir
        End Function
        Public Function setParProxPozoConfirmado(ByRef oSorteo As PgmSorteo, ByVal estado As Boolean) As Boolean
            Dim oDal As New Data.PgmSorteoDAL
            Dim res As Boolean
            Try
                res = oDal.setParProxPozoConfirmado(oSorteo, estado)
            Catch ex As Exception
                res = False
                FileSystemHelper.Log("PgmSorteoBo.setParProxPozoConfirmado: Excepción ->" & ex.Message & "<-")
                Throw New Exception("Problema al confirmar los parámetros para cálculo de pozo estimado.")
            End Try
            Return res
        End Function
    End Class
End Namespace
