Imports libEntities.Entities
Imports Sorteos.Helpers
Imports System.Net
Imports System.IO
Imports Sorteos.Extractos

Namespace Bussiness


    Public Class PgmConcursoBO

        Public Function publicarDisplay(ByVal idPgmConcurso As Integer) As Boolean
            Dim dioTimeout As Boolean = False

            Dim oPgmConcurso As New PgmConcurso
            oPgmConcurso = getPgmConcurso(idPgmConcurso)
            Dim boSorteo As New PgmSorteoBO
            For Each os As PgmSorteo In oPgmConcurso.PgmSorteos
                'AGREGADOR POR FSCOTTA
                If General.PublicaExtractosWSRestOFF = "S" Or General.PublicaExtractosWSRestON = "S" Then
                    Dim respuesta As String
                    Try
                        respuesta = boSorteo.publicarExtRest(os)
                        FileSystemHelper.Log("publicarDisplay - publicarExtRest -> " & respuesta)
                    Catch ex As Exception
                        FileSystemHelper.Log("publicarDisplay - publicarExtRest(error) -> " & ex.Message)
                    End Try
                End If


                Try
                    'If Not dioTimeout Then
                    boSorteo.publicarDisplay(os, False, dioTimeout)
                    'End If
                Catch ex As Exception
                    Try
                        FileSystemHelper.Log("pgmConcursoBO.publicarDisplay -> " & ex.Message)
                    Catch ex1 As Exception
                    End Try
                End Try
            Next
            Return True

        End Function

        Public Function publicarWEB(ByVal idPgmConcurso As Integer) As Boolean
            Dim oPgmConcurso As New PgmConcurso
            oPgmConcurso = getPgmConcurso(idPgmConcurso)
            Dim boSorteo As New PgmSorteoBO
            For Each os As PgmSorteo In oPgmConcurso.PgmSorteos
                'AGREGADO POR FSCOTTA
                If General.PublicaExtractosWSRestON = "S" Or General.PublicaExtractosWSRestOFF = "S" Then
                    Try
                        boSorteo.publicarExtRest(os)
                    Catch ex As Exception
                        FileSystemHelper.Log("pgmConcursoBO.publicarExtRest -> " & ex.Message)
                    End Try
                Else
                    '---------------
                    Try
                        boSorteo.publicarWEB(os)
                    Catch ex As Exception
                        FileSystemHelper.Log("pgmConcursoBO.publicarWeb -> " & ex.Message)
                    End Try
                End If
            Next
            Return True
        End Function

        Public Function publicarWEB(ByVal oConcurso As PgmConcurso) As Boolean
            Dim boSorteo As New PgmSorteoBO
            For Each os As PgmSorteo In oConcurso.PgmSorteos
                'AGREGADO POR FSCOTTA
                If General.PublicaExtractosWSRestON = "S" Or General.PublicaExtractosWSRestOFF = "S" Then
                    Try
                        boSorteo.publicarWEBRest(os)
                    Catch ex As Exception
                        FileSystemHelper.Log("pgmConcursoBO.publicarExtRest -> " & ex.Message)
                    End Try
                End If
                '---------------
                If General.PublicarWebON = "S" Or General.PublicarWebOFF = "S" Then
                    Try
                        boSorteo.publicarWEB(os)
                    Catch ex As Exception
                        FileSystemHelper.Log("pgmConcursoBO.publicarWeb anterior -> " & ex.Message)
                    End Try
                End If
            Next
            Return True
        End Function


        Public Function getNroSorteoPpal(ByVal oPgmC As PgmConcurso) As Integer

            For Each ps As PgmSorteo In oPgmC.PgmSorteos
                If ps.idPgmSorteo = oPgmC.idPgmSorteoPrincipal Then
                    Return ps.nroSorteo
                End If
            Next
        End Function

        Public Function getPgmConcurso(ByVal FechaHora As DateTime) As List(Of PgmConcurso)
            Try
                Dim listaPC As List(Of PgmConcurso)
                Dim oDal As New Data.PgmConcursoDAL
                listaPC = oDal.getPgmConcurso(FechaHora)
                Return listaPC

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcurso(ByVal idPgmConcurso As Long) As PgmConcurso
            Try
                Dim o As New PgmConcurso
                Dim oDal As New Data.PgmConcursoDAL
                o = oDal.getPgmConcurso(idPgmConcurso)
                Return o

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcursoDT(ByVal idPgmConcurso As Integer) As DataTable
            Try
                Dim dt As DataTable
                Dim oDal As New Data.PgmConcursoDAL
                dt = oDal.getPgmConcursoDT(idPgmConcurso)

                Return dt

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcursoNoIniciadoOIniciado(ByVal fechahora As DateTime) As ListaOrdenada(Of PgmConcurso)
            Try
                Dim ls As New ListaOrdenada(Of PgmConcurso)
                Dim oDal As New Data.PgmConcursoDAL
                ls = oDal.getPgmConcursoNoIniciadoOIniciado(fechahora)

                Return ls

            Catch ex As Exception
                FileSystemHelper.Log(" PgmConcursoBO:getPgmConcursoNoIniciadoOIniciado - Excepcion: " & ex.Message)
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcursoInicializadooFinalizado() As List(Of PgmConcurso)
            Try
                Dim ls As List(Of PgmConcurso)
                Dim oDal As New Data.PgmConcursoDAL
                ls = oDal.getPgmConcursoIniciadooFinalizado()
                Return ls

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcursoInicializadooFinalizado(ByVal Fecha As DateTime) As List(Of PgmConcurso)
            Try
                Dim ls As New List(Of PgmConcurso)
                Dim oDal As New Data.PgmConcursoDAL
                ls = oDal.getPgmConcursoIniciadooFinalizado(Fecha)
                Return ls

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function getPgmConcursoFinalizado() As List(Of PgmConcurso)
            Try
                Dim ls As List(Of PgmConcurso)
                Dim oDal As New Data.PgmConcursoDAL
                ls = oDal.getPgmConcursoFinalizado()
                Return ls

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getPgmConcursoFinalizado(ByVal fechahora As DateTime, Optional ByVal soloUltimo As Boolean = False) As ListaOrdenada(Of PgmConcurso)
            Try
                Dim ls As New ListaOrdenada(Of PgmConcurso)
                Dim oDal As New Data.PgmConcursoDAL
                ls = oDal.getPgmConcursoFinalizado(fechahora, soloUltimo)
                Return ls

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function valida(ByVal oPC As PgmConcurso, ByRef msgRet As String) As Boolean
            Dim boSorteo As New JuegoBO

            msgRet = ""

            For Each oSorteo In oPC.PgmSorteos
                If Trim(oPC.localidad) = "" Then msgRet &= boSorteo.getJuegoDescripcion(oSorteo.idJuego) & ": falta indicar la localidad"

                If oSorteo.fechaHoraPrescripcion < oPC.fechaHora Then msgRet &= boSorteo.getJuegoDescripcion(oSorteo.idJuego) & ": la fecha de prescripción debe ser posterior a la del sorteo actual" & vbCr
                If oSorteo.fechaHoraProximo < oPC.fechaHora Then msgRet &= boSorteo.getJuegoDescripcion(oSorteo.idJuego) & ": la fecha de próximo sorteo debe ser posterior a la del sorteo actual" & vbCr

                If msgRet = "" Then
                    Return True
                Else
                    Return False
                End If
            Next
        End Function

        Public Function setPgmConcurso(ByVal oPC As PgmConcurso) As Boolean
            Dim oDal As New Data.PgmSorteoDAL
            Dim _idJuegoPrincipal As Integer
            Try
                _idJuegoPrincipal = oPC.concurso.JuegoPrincipal.Juego.IdJuego
                For Each oSorteo In oPC.PgmSorteos
                    oDal.setPgmSorteo(oSorteo, _idJuegoPrincipal)
                Next

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function Iniciar(ByVal oPC As PgmConcurso) As Boolean
            Dim oDal As New Data.PgmConcursoDAL
            Try
                Return oDal.Iniciar(oPC)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function RevertirExtracciones(ByVal pPgmConcurso As PgmConcurso, ByVal pUsuario As String, ByVal pIdExtraccionesCAB As Integer, Optional ByVal modalidad As Integer = -1, Optional ByVal pBorraPozo As Integer = 0) As Integer
            Dim oPgmCDal As New Data.PgmConcursoDAL
            Dim oPgmSDal As New Data.PgmSorteoDAL
            Dim oJuegoBO As New Bussiness.JuegoBO

            Try
                For Each _opgmsorteo In pPgmConcurso.PgmSorteos
                    If oJuegoBO.esQuiniela(_opgmsorteo.idJuego) Then
                        oPgmSDal.AnularQuinielaSF(_opgmsorteo.idPgmSorteo)
                    End If
                Next

                Return oPgmCDal.RevertirExtracciones(pPgmConcurso.idPgmConcurso, pUsuario, pIdExtraccionesCAB, modalidad, pBorraPozo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function
        Public Function Finalizar(ByVal pidPgmConcurso As Integer) As Boolean
            Dim oDal As New Data.PgmConcursoDAL
            Try
                If oDal.Finalizar(pidPgmConcurso) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function ActualizarEstadoConcurso(ByVal pIdPgmConcurso As Integer, ByVal pEstado As Integer) As Boolean
            Dim oDal As New Data.PgmConcursoDAL
            Try
                If oDal.ActualizarEstadoConcurso(pIdPgmConcurso, pEstado) Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function getPgmConcursoQuiniela() As List(Of PgmConcurso)
            Try
                Dim ls As New List(Of PgmConcurso)
                Dim oDal As New Data.PgmConcursoDAL
                ls = oDal.getPgmConcursoQuiniela
                Return ls

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function ObtenerPgmSorteoQuiniela(ByVal opgmConcurso As PgmConcurso) As PgmSorteo
            Dim oDal As New Data.PgmConcursoDAL
            Dim opgmSorteo As PgmSorteo
            Try
                opgmSorteo = Nothing
                opgmSorteo = oDal.ObtenerPgmSorteoQuiniela(opgmConcurso)
                Return opgmSorteo
            Catch ex As Exception
                opgmSorteo = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function ObtenerDatosListado(ByVal idConcurso As Integer) As DataTable
            Dim oDal As New Data.PgmConcursoDAL
            Dim dt As DataTable
            Try
                dt = Nothing
                dt = oDal.ObtenerDatosListado(idConcurso)
                Return dt
            Catch ex As Exception
                dt = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function ActualizarEstado(ByVal idpgmConcurso As Integer) As Boolean

            Try
                Dim dal As New Data.PgmConcursoDAL
                dal.setPgmConcursoEstado(idpgmConcurso)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try

            Return True
        End Function
        Public Function ObtenerDatosExtraccionesCAB(ByVal pidPgmConcurso As Integer) As DataTable
            Dim oDal As New Data.PgmConcursoDAL
            Dim dt As DataTable
            Try
                dt = Nothing
                dt = oDal.ObtenerDatosExtraccionesCAB(pidPgmConcurso)
                Return dt
            Catch ex As Exception
                dt = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function ObtenerDatosJuegos(ByVal pidPgmConcurso As Integer) As DataTable
            Dim oDal As New Data.PgmConcursoDAL
            Dim dt As DataTable
            Try
                dt = Nothing
                dt = oDal.ObtenerDatosJuegos(pidPgmConcurso)
                Return dt
            Catch ex As Exception
                dt = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getPgmConcursoFinalizadoConPremios(ByVal fechahora As DateTime) As ListaOrdenada(Of PgmConcurso)
            Try
                Dim ls As New ListaOrdenada(Of PgmConcurso)
                Dim oDal As New Data.PgmConcursoDAL
                ls = oDal.getPgmConcursoFinalizadoConPremios(fechahora)
                Return ls

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function Concurso_con_PremiosConfirmados(ByVal _opgmconcurso As PgmConcurso, ByRef lst As String) As Boolean
            Try
                Dim _resultado As Boolean = False
                Dim _opgmsorteo As PgmSorteo
                lst = ""
                For Each _opgmsorteo In _opgmconcurso.PgmSorteos
                    If _opgmsorteo.idEstadoPgmConcurso = 50 Then
                        lst = lst & _opgmsorteo.idPgmSorteo & ","
                        _resultado = True
                    End If
                Next
                If lst.Trim.Length > 0 Then
                    lst = Left(lst, lst.Length - 1)
                End If
                Return _resultado

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getJuegoSorteoRector(ByVal _opgmconcurso As PgmConcurso, ByRef juego As Integer, ByRef sorteo As Long) As Boolean
            Try
                Dim _resultado As Boolean = False
                Dim _opgmsorteo As PgmSorteo
                juego = -1
                sorteo = -1
                For Each _opgmsorteo In _opgmconcurso.PgmSorteos
                    If _opgmsorteo.idPgmSorteo = _opgmsorteo.idPgmConcurso Then 'juego rector
                        juego = _opgmsorteo.idJuego
                        sorteo = _opgmsorteo.nroSorteo
                        Return True
                    End If
                Next

                Return IIf(juego = -1 Or sorteo = -1, False, True)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function setPgmSorteo_Autoridades(ByRef _opgmconcurso As PgmConcurso, ByVal pIdpgmSorteo As Long, ByVal lstAutoridades As ListaOrdenada(Of Autoridad)) As Boolean
            Try
                Dim _resultado As Boolean = False
                Dim _opgmsorteo As PgmSorteo
                For Each _opgmsorteo In _opgmconcurso.PgmSorteos
                    If _opgmsorteo.idPgmSorteo = pIdpgmSorteo Then
                        _opgmsorteo.autoridades.Clear()
                        _opgmsorteo.autoridades = lstAutoridades
                        Exit For
                    End If
                Next
                Return _resultado

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function EstadoPgmConcurso(ByVal idPgmconcurso As Long) As Integer
            Try
                Dim oDal As New Data.PgmConcursoDAL
                Return oDal.EstadoPgmConcurso(idPgmconcurso)

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function setPgmSorteo_Loterias(ByRef _opgmconcurso As PgmConcurso, ByVal pIdpgmSorteo As Long, ByVal lstloterias As ListaOrdenada(Of pgmSorteo_loteria)) As Boolean
            Try
                Dim _resultado As Boolean = False
                Dim _opgmsorteo As PgmSorteo
                For Each _opgmsorteo In _opgmconcurso.PgmSorteos
                    If _opgmsorteo.idPgmSorteo = pIdpgmSorteo Then
                        _opgmsorteo.ExtraccionesLoteria.Clear()
                        _opgmsorteo.ExtraccionesLoteria = lstloterias
                        Exit For
                    End If
                Next
                Return _resultado

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        '** publicacion otras loterias
        Public Function publicarDisplayOtrasJurisdicciones(ByVal idPgmConcurso As Integer, ByVal idloteria As Char) As Boolean
            Dim oDal As New Data.PgmSorteoDAL
            If oDal.publicarDisplayOtrasJurisdicciones(idPgmConcurso, idloteria) Then
                Return True
            Else
                Return False
            End If

        End Function

        Public Function getPgmConcursosaRevertir(ByVal fechaHora As DateTime) As List(Of PgmConcurso)
            Try
                Dim oDal As New Data.PgmConcursoDAL
                Return oDal.getPgmConcursosaRevertir(fechaHora)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function

        Public Function RevertirConcurso(ByVal pidPgmConcurso As Integer, Optional ByVal pidpgmsorteo As Integer = 0) As Boolean
            Try
                Dim oDal As New Data.PgmConcursoDAL
                Return oDal.RevertirConcurso(pidPgmConcurso, pidpgmsorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function
        Public Function ExisteArchivoRemoto(ByVal url As String) As Boolean
            Try


                Dim Peticion As System.Net.WebRequest
                Dim Respuesta As System.Net.HttpWebResponse
                Try
                    Peticion = System.Net.WebRequest.Create(url)
                    Respuesta = Peticion.GetResponse()
                    Return True
                Catch ex As System.Net.WebException
                    Return False
                End Try

            Catch ex As Exception

            End Try






        End Function
        Public Function DescargarPDF(ByVal idpgmconcurso As Integer) As Boolean
            ' descarga el archivo remoto en un directorio local
            Try

                Dim oDal As New Data.PgmConcursoDAL
                Dim lista As String = ""
                Dim archivos() As String
                Dim urlarchivo As String = ""
                Dim nombrearchivo As String = ""
                Dim _WebClient As New System.Net.WebClient()
                Dim destino As String
                Dim sin_archivos As Boolean
                Try
                    lista = oDal.Obtener_url_PDF(idpgmconcurso, sin_archivos)
                Catch ex As Exception
                    Return False
                End Try

                If sin_archivos Then
                    Exit Function
                End If
                archivos = Split(lista, ",")
                For Each urlarchivo In archivos
                    Try
                        If urlarchivo.Trim <> "" Then
                            'Dim _WebClient As New System.Net.WebClient()
                            If Me.ExisteArchivoRemoto(urlarchivo) Then

                                nombrearchivo = System.IO.Path.GetFileName(urlarchivo)
                                destino = General.Path_Premios_Destino & "\" & nombrearchivo
                                ' The DownloadFile() method downloads the Web resource and saves it into the current file-system folder.
                                urlarchivo = urlarchivo & "?t=" & DateAndTime.Now.ToString

                                ' _WebClient.DownloadFile(urlarchivo, General.Path_Premios_Destino & "\" & nombrearchivo)
                                'My.Computer.Network.DownloadFile(urlarchivo, destino, False, 3600000)
                                'GeneralBO.DescargarArchivoFTP(nombrearchivo, destino)
                                GeneralBO.DownloadFile(nombrearchivo, destino)

                            Else
                                Return False
                                Exit Function
                            End If
                        End If
                    Catch Exp As Exception
                        Return False
                    End Try
                Next
                Return True
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function listar_pdf(ByVal juego As Integer, ByVal sorteo As Integer, ByRef archivo_age As String, ByRef archivo_prv As String, ByVal salidaPDF As Integer) As Boolean
            'listar pdf y devolver el path local a los archivos
            'si listar es 1,muestra los pdf  con el visor predeterminado del sistema(tiene que existir uno)
            'si listar es 0,no muestra nada
            Try
                Dim oDal As New Data.PgmConcursoDAL
                Dim nombre_age As String = ""
                Dim nombre_prv As String = ""
                Dim loPSI As New ProcessStartInfo
                Dim loProceso As New Process
                Dim idpgmconcurso As Integer
                Dim lista As String = ""
                Dim archivos() As String
                Dim juegostr As String = juego
                Dim sorteostr As String = sorteo
                Dim ruta As String = ""
                Dim sin_archivos As Boolean
                Dim problemas As Boolean = False
                idpgmconcurso = juego * 1000000 + sorteo


                lista = oDal.Obtener_url_PDF(idpgmconcurso, sin_archivos)
                If sin_archivos Then
                    Exit Function
                End If

                archivos = Split(lista, ",")

                If Trim(archivos(0) <> "") Then ' si el campo tiene datos significa que se realizo el FTP del archivo
                    nombre_age = "AGE" & Mid("00", 1, 2 - Len(juegostr)) & juego & Mid("000000", 1, 5 - Len(sorteostr)) & sorteo & ".pdf"
                    nombre_age = "AGE" & juego.ToString().Trim().PadLeft(2, "0") & sorteo.ToString().Trim().PadLeft(5, "0") & ".pdf"
                    ruta = General.Path_Premios_Destino & "\" & nombre_age
                    FileSystemHelper.Log("Ruta archivo primer premio ->" & ruta & "<-")
                    If IO.File.Exists(ruta) Then
                        archivo_age = ruta 'General.Path_Premios_Destino & "\" & nombre_age
                        If salidaPDF = 1 Then
                            loPSI.FileName = ruta 'General.Path_Premios_Destino & "\" & nombre_age
                            loProceso = Process.Start(loPSI)
                        End If

                    Else 'si no existe trato de volver a obtener
                        'DescargarPDF(idpgmconcurso)
                        archivo_age = ""
                    End If

                    If Not IO.File.Exists(General.Path_Premios_Destino & "\" & nombre_age) Then
                        archivo_age = ""
                        If salidaPDF = 1 Then
                            MsgBox("No se encontró archivo de Agencia Vendedora del Primer Premio en el equipo local:" & General.Path_Premios_Destino & "\" & nombre_age & vbCrLf & "Pruebe descargarlo desde la siguiente dirección:" & archivos(0))
                            problemas = True
                        End If
                    End If
                End If
                If Trim(archivos(1) <> "") Then
                    nombre_prv = "PRV" & Mid("00", 1, 2 - Len(juegostr)) & juego & Mid("000000", 1, 5 - Len(sorteostr)) & sorteo & ".pdf"
                    nombre_prv = "PRV" & juego.ToString().Trim().PadLeft(2, "0") & sorteo.ToString().Trim().PadLeft(5, "0") & ".pdf"
                    ruta = General.Path_Premios_Destino & "\" & nombre_prv
                    FileSystemHelper.Log("Ruta archivo provincias ->" & ruta & "<-")
                    If IO.File.Exists(ruta) Then
                        archivo_prv = ruta 'General.Path_Premios_Destino & "\" & nombre_prv
                        If salidaPDF = 1 Then
                            loPSI.FileName = ruta 'General.Path_Premios_Destino & "\" & nombre_prv
                            loProceso = Process.Start(loPSI)
                        End If
                    Else 'si no existe trato de vovler a obtener
                        'DescargarPDF(idpgmconcurso)
                        nombre_prv = "PRV" & Mid("000000", 1, 5 - Len(sorteostr)) & sorteo & ".pdf"
                        nombre_prv = "PRV" & juego.ToString().Trim().PadLeft(2, "0") & sorteo.ToString().Trim().PadLeft(5, "0") & ".pdf"
                        ruta = General.Path_Premios_Destino & "\" & nombre_prv
                        FileSystemHelper.Log("Ruta archivo provincias ->" & ruta & "<-")
                        If IO.File.Exists(ruta) Then
                            archivo_prv = ruta 'General.Path_Premios_Destino & "\" & nombre_prv
                            If salidaPDF = 1 Then
                                loPSI.FileName = ruta 'General.Path_Premios_Destino & "\" & nombre_prv
                                loProceso = Process.Start(loPSI)
                            End If
                        Else
                            archivo_prv = ""
                        End If
                    End If
                    If Not IO.File.Exists(ruta) Then
                        nombre_prv = ""
                        If salidaPDF = 1 Then
                            MsgBox("No se encontró archivo de Distribución por Provincia en el equipo local:" & ruta & vbCrLf & "Pruebe descargarlo desde la siguiente dirección:" & archivos(1))
                            problemas = True
                        End If
                    End If
                End If

                If Not problemas Then
                    Return True
                Else
                    Return False
                End If
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function Obtener_url_PDF(ByVal idpgmconcurso As Integer, ByRef sin_archivos As Boolean) As String
            Dim odal As New Data.PgmConcursoDAL
            Try
                Return odal.Obtener_url_PDF(idpgmconcurso, sin_archivos)
            Catch ex As Exception
                Return False
            End Try
        End Function
        Public Function ObtenerDatosEscenariosGanadores1Premio(ByVal pidPgmConcurso As Integer, ByVal pcalcular As Integer) As DataTable
            Dim oDal As New Data.PgmConcursoDAL
            Dim dt As DataTable
            Try
                dt = Nothing
                dt = oDal.ObtenerDatosEscenariosGanadores1Premio(pidPgmConcurso, pcalcular)
                Return dt
            Catch ex As Exception
                dt = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function ObtenerDatosProximosorteo(ByVal pidPgmConcurso As Integer, ByVal pcalcular As Integer) As DataTable
            Dim oDal As New Data.PgmConcursoDAL
            Dim dt As DataTable
            Try
                dt = Nothing
                dt = oDal.ObtenerDatosProximoSorteo(pidPgmConcurso, pcalcular)
                Return dt
            Catch ex As Exception
                dt = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function ImprimirEscenariosGanadores1Premio(ByVal pIdpgmconcurso As Int32, ByVal plistar As Integer, ByVal path_reporte As String, ByRef mensaje As String, Optional ByVal nCopias As Integer = 1) As Boolean
            Dim PgmBO As New PgmConcursoBO
            Dim dt As DataTable
            Dim ds As New DataSet

            Try

                dt = PgmBO.ObtenerDatosEscenariosGanadores1Premio(pIdpgmconcurso, plistar)
                dt.TableName = "ParametrosPozoEstimado"
                'dt.WriteXmlSchema("D:\rptPlanillaPosiblesGanLocutor.xml")
                ds.Tables.Add(dt)

                'Dim path_reporte As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES"
                Dim reporte As New Listado
                reporte.GenerarEscenariosGanadores1Premio(ds, path_reporte, nCopias)
                Return True

            Catch ex As Exception
                mensaje = ex.Message
                Return False
            End Try
        End Function

        Public Function ImprimirParametrospozoproximo(ByVal pIdpgmconcurso As Int32, ByVal plistar As Integer, ByVal path_reporte As String, ByRef mensaje As String, Optional ByVal nCopias As Integer = 1) As Boolean
            Dim PgmBO As New PgmConcursoBO
            Dim dt As DataTable
            Dim ds As New DataSet

            Try
                dt = PgmBO.ObtenerDatosProximosorteo(pIdpgmconcurso, plistar)
                dt.TableName = "ParametrosPozoEstimado"
                'dt.WriteXmlSchema("D:\parametrospozoproximo.xml")
                ds.Tables.Add(dt)

                'Dim path_reporte As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES"
                Dim reporte As New Listado
                reporte.GenerarParametrosPozoProximo(ds, path_reporte, nCopias)
                Return True

            Catch ex As Exception
                mensaje = ex.Message
                Return False
            End Try
        End Function

        Public Function versionarParametros(ByVal pIdPgmConcurso As Int64, ByVal pComentario As String, ByVal pUsuario As String) As Int32
            Dim nroVersion As Int32 = 0
            Try
                Dim oDal As New Data.PgmConcursoDAL
                nroVersion = oDal.versionarParametros(pIdPgmConcurso, pComentario, pUsuario)
                FileSystemHelper.Log("versionarParametros: IdPgmConcurso ->" & pIdPgmConcurso & "<- pComentario ->" & pComentario & "<- pUsuario ->" & pUsuario & "<- OK")
            Catch ex As Exception
                FileSystemHelper.Log("versionarParametros: IdPgmConcurso ->" & pIdPgmConcurso & "<- pComentario ->" & pComentario & "<- pUsuario ->" & pUsuario & "<- Excepción: " & ex.Message)
            End Try
            Return nroVersion
        End Function
    End Class
End Namespace