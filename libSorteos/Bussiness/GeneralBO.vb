Imports libEntities.Entities
Imports Sorteos.Helpers
Imports Sorteos.Data
Imports System.Net.Mime.MediaTypeNames
Imports Sorteos.Extractos
Imports System.Net
Imports System.IO
Imports System.Text

Namespace Bussiness

    Public Class GeneralBO

        Public Function ObtenerPuerto() As String
            Dim general As New GeneralDAL
            Try
                Return general.ObtenerPuerto
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function
        Public Function ObtenerSonido() As String
            Dim general As New GeneralDAL
            Try
                Return general.ObtenerSonido
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function
        Public Function LoteriaComenzada(ByVal pIdpgmsorteo As Integer, ByVal pIdloteria As Char, ByRef pcifras As Integer) As Boolean
            Dim general As New GeneralDAL
            Try
                Return general.LoteriaComenzada(pIdpgmsorteo, pIdloteria, pcifras)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Shared Function EnviarMail(ByVal pdf As String, ByVal Destinatarios As String, Optional ByVal pDescripcion As String = "", Optional ByVal osorteo As PgmSorteo = Nothing, Optional ByVal adjunta_pdf As Boolean = False) As Boolean
            Dim MailHost As String = General.MailHost
            Dim Mailport As String = General.MailPort
            Dim MailFrom As String = General.MailFrom
            Dim MailSep As String = General.MailSep
            Dim ListaEnvioExtracto As String = Destinatarios
            Dim boMail As New MailBO
            Dim Cuerpo As String = ""
            Dim Titulo As String = ""
            Dim Remite As String = ""

            Dim path_archivo_age As String = ""
            Dim path_archivo_prv As String = ""

            Dim BO As New PgmConcursoBO
            Dim oSorteoBO As New PgmSorteoBO

            Dim oArchivoBold As New ArchivoBoldtBO
            Dim pathExtractoPropio As String = ""
            Dim nombrePaq As String = ""
            Dim extracto_por_link As Boolean = False
            Dim urlExtracto As String = ""

            Dim pathExtractoInterJ As String = ""
            Dim nombrePaqInterJ As String = ""

            Dim _adjunto As New List(Of String)
            FileSystemHelper.Log("GeneralBO.EnviarMail - INI.")
            Try
                If pdf.Trim.Length > 0 Then
                    _adjunto.Add(pdf)
                End If
                ' 1. adjunto el archivo de extracto propio
                pathExtractoPropio = General.CarpetaArchivosBoldt & "\Propio"
                FileSystemHelper.Log("GeneralBO.EnviarMail -        1. adjunto el archivo de extracto propio - pathExtractoPropio ->" & pathExtractoPropio & "<-")
                oArchivoBold.GenerarArchivoExtracto(osorteo, pathExtractoPropio, True, IIf(General.EnviarFTP = "S", True, False), nombrePaq)
                _adjunto.Add(pathExtractoPropio & nombrePaq)

                ' 2. adjunto el archivo de extracto InterJ
                Dim jbo As New JuegoBO

                If General.Extr_Interjur = "S" And jbo.getJuego(osorteo.idJuego).GeneraExtractoUnif Then
                    pathExtractoInterJ = General.CarpetaExtractoBoltConfirmado
                    FileSystemHelper.Log("GeneralBO.EnviarMail -        2. adjunto el archivo de extracto InterJ - Corresponde Generar - pathExtractoInterJ ->" & pathExtractoInterJ & "<-")
                    FileSystemHelper.Log("GeneralBO.EnviarMail - Va a llamar a GenerarArchivoExtractoInterJ - pathExtractoInterJ ->" & pathExtractoInterJ & "<-")

                    oArchivoBold.GenerarArchivoExtractoInterJ(osorteo, pathExtractoInterJ, nombrePaqInterJ, True)

                    Try
                        If FileSystemHelper.ExisteArchivo(pathExtractoInterJ & nombrePaqInterJ.Replace(".zip", ".xml")) Then
                            _adjunto.Add(pathExtractoInterJ & nombrePaqInterJ.Replace(".zip", ".xml"))
                            _adjunto.Add(pathExtractoInterJ & nombrePaqInterJ.Replace(".zip", ".sha"))
                        Else
                            FileSystemHelper.Log("GeneralBO.EnviarMail - No existe el archivo para adjuntar extracto INTER-JUR, pero el mail se envía de todas maneras - archivo ->" & pathExtractoInterJ & nombrePaqInterJ.Replace(".zip", ".xml") & "<-")
                        End If
                    Catch exJ As Exception
                        FileSystemHelper.Log("GeneralBO.EnviarMail - Fallo al adjuntar extracto INTER-JUR, pero el mail se envía de todas maneras - pathExtractoInterJ: " & pathExtractoInterJ & " - nombrePaqInterJ: " & nombrePaqInterJ)
                    End Try
                Else
                    FileSystemHelper.Log("GeneralBO.EnviarMail -        2. adjunto el archivo de extracto InterJ - NO Corresponde Generar.")
                End If

                ' 3. adjunto los archivos de Poceado: ganadores por agencia, ganadores por provincia
                If adjunta_pdf Then
                    If (osorteo.idJuego = 4 Or osorteo.idJuego = 13 Or osorteo.idJuego = 30) AndAlso (oSorteoBO.enviarPdfPrimerPremio(osorteo.idPgmSorteo)) Then
                        FileSystemHelper.Log("GeneralBO.EnviarMail -        3. adjunto los archivos de Poceado: ganadores por agencia, ganadores por provincia - Corresponde adjuntar.")
                        BO.listar_pdf(osorteo.idJuego, osorteo.nroSorteo, path_archivo_age, path_archivo_prv, 0)
                        If Trim(path_archivo_age) <> "" Then
                            _adjunto.Add(path_archivo_age)
                        End If
                        If (oSorteoBO.exigirPdfDistribPcias(osorteo.idPgmSorteo)) AndAlso Trim(path_archivo_prv) <> "" Then
                            _adjunto.Add(path_archivo_prv)
                        End If
                    Else
                        FileSystemHelper.Log("GeneralBO.EnviarMail -        3. adjunto los archivos de Poceado: ganadores por agencia, ganadores por provincia - NO Corresponde adjuntar.")
                    End If
                Else
                    FileSystemHelper.Log("GeneralBO.EnviarMail -        3. adjunto los archivos de Poceado: ganadores por agencia, ganadores por provincia - NO Corresponde adjuntar.")
                End If

                If General.Jurisdiccion = "E" Then
                    If pDescripcion.Trim <> "" Then
                        Cuerpo = pDescripcion & vbCrLf & vbCrLf
                    End If
                    Cuerpo = Cuerpo & "Se Adjuntan datos del Extracto de Entre Ríos. " & vbCrLf & vbCrLf & vbCrLf & "IAFAS - Instituto de Ayuda Financiera a la Acción Social" & vbCrLf & "                             Lotería de Entre Ríos"
                    Titulo = "Extracto de Entre Ríos"
                    Remite = "IAFAS - Sala de Sorteos"
                Else
                    Cuerpo = "<div style='background:#0060aa;width:100%;float:left;padding:5px;margin-bottom: 10px;'><img src='www.loteriasantafe.gov.ar/logo.png' alt='logo' style='float: left; '/></div>"
                    If pDescripcion.Trim <> "" Then
                        Cuerpo = pDescripcion & vbCrLf & vbCrLf
                    End If
                    Cuerpo = Cuerpo & "Se Adjuntan datos del Extracto de Santa Fe en formato digital. "
                    If pdf.Trim.Length = 0 Then
                        urlExtracto = General.prefijoRPTExtracto & "&param_ID_sor=" & osorteo.idSor
                        Cuerpo = Cuerpo & "Si desea visualizar o descargar el EXTRACTO OFICIAL puede hacerlo con un <a href='" & urlExtracto & "'>Click AQUI.</a> "
                    End If
                    Cuerpo = Cuerpo & vbCrLf & vbCrLf & vbCrLf & "<BR><BR><BR>CAS - Caja de Asistencia Social" & vbCrLf & "<BR>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<a href='http://www.loteriasantafe.gov.ar'>Lotería de Santa Fe</a>"
                    Titulo = "Extracto de Santa Fe"
                    Remite = "Lotería de Santa Fe"
                End If
                If Mailport = "" Then
                    Mailport = "0"
                End If
                boMail.envioMail(MailHost, MailFrom, ListaEnvioExtracto, Titulo, Cuerpo, True, _adjunto, MailSep, Mailport, Remite)
                boMail = Nothing
            Catch ex As Exception
                FileSystemHelper.Log("GeneralBO.EnviarMail - Excepcion: " & ex.Message)
                Throw New Exception("GeneralBO.EnviarMail - " & ex.Message)
                Return False
            End Try
            Return True

        End Function
        Public Shared Function GenerarPdfExtractoSF(ByVal idPgmConcurso As Long, ByRef pdf As String, ByVal path As String, Optional ByVal enviarAImpresora As Boolean = True) As Boolean
            Dim msgRet As String = ""
            Dim PgmBO As New PgmConcursoBO
            Dim dt As DataTable
            Dim ds As New DataSet
            Dim dal As New PgmConcursoDAL
            Dim oc As New PgmConcurso
            Dim dtExtra As DataTable
            Dim er As New ExtractoReporte
            Dim visualizar As String = "000000000000000000"
            Try
                dt = PgmBO.ObtenerDatosListado(idPgmConcurso)
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

            Catch ex As Exception
                Throw New Exception("Problema al recuperar datos para el listado de extracto. " & ex.Message)
                Return False
            End Try

            Try
                'ds.WriteXmlSchema("D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES\Listado1.xml")
                Dim path_reporte As String = path & General.PathInformes  '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES" Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14)
                Dim path_destino As String = path & "PDF_INFORMES" '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\PDF_INFORMES"
                Dim nombrePDF As String = "extracto_" & IIf(General.Jurisdiccion = "E", "entre_rios", "santa_fe") & "_" & idPgmConcurso & ".pdf"
                Try
                    If Not System.IO.Directory.Exists(path_destino) Then
                        System.IO.Directory.CreateDirectory(path_destino)
                    End If
                Catch ex As Exception
                End Try

                Dim reporte As New Listado
                If Not reporte.GenerarListadoExtracciones(ds, path_reporte, 1, path_destino, msgRet, "rptExtraccionesMail.rpt", nombrePDF, enviarAImpresora, visualizar) Then
                    Throw New Exception("Problema al generar el listado de extracto. " & msgRet)
                    Return False
                End If
                reporte = Nothing
                pdf = path_destino & nombrePDF  ' armar nombre
                Return True
            Catch ex As Exception
                Throw New Exception("Problema al generar el listado de extracto. " & ex.Message)
                Return False
            End Try

            Return True
        End Function
        Public Shared Function enviarFTP(ByVal OrigenArchivo As String, ByVal NombreArchivo As String) As Boolean
            Dim clsRequest As System.Net.FtpWebRequest
            Dim servidorFTP As String
            Dim usuarioFTP As String
            Dim pwdFTP As String


            servidorFTP = General.servidorFTP
            If Not servidorFTP.EndsWith("/") Then servidorFTP = servidorFTP & "/"
            usuarioFTP = General.usuarioFTP
            pwdFTP = General.pwdFTP
            Dim _nroIntento As Integer = 1
            Dim _Encontrado As Boolean = False
            While _nroIntento <= General.CantidadIntentos
                If System.IO.File.Exists(OrigenArchivo) Then
                    _Encontrado = True
                    Exit While
                End If
                System.Threading.Thread.Sleep(1000)

            End While
            If _Encontrado = False Then
                Throw New Exception("Problema al enviar archivo de extracto por FTP:" & "No existe el archivo" & NombreArchivo)
                Return False
            End If
            'si existe el archivo, lo elimina 
            If existeArchivo(NombreArchivo) Then
                eliminaArchivoFTP(NombreArchivo)
            End If

            enviarFTP = False
            clsRequest = CType(System.Net.FtpWebRequest.Create(NombreArchivo), System.Net.FtpWebRequest)
            clsRequest.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.
            clsRequest.Credentials = New System.Net.NetworkCredential(usuarioFTP, pwdFTP) ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.UploadFile
            Try
                Dim bFile() As Byte = System.IO.File.ReadAllBytes(OrigenArchivo)
                Dim clsStream As System.IO.Stream = clsRequest.GetRequestStream()
                clsStream.Write(bFile, 0, bFile.Length)
                clsStream.Close()
                clsStream.Dispose()

                enviarFTP = True
            Catch ex As Exception
                Throw New Exception("Problema al enviar archivo de extracto por FTP:" & ex.Message)
                Return False
            End Try
        End Function
        Public Shared Function encriptararchivoGPG(ByVal patharchivo As String) As Boolean
            Dim comando As String = ""
            Dim proceso As New Process
            If System.IO.File.Exists(patharchivo & ".gpg") Then
                System.IO.File.Delete(patharchivo & ".gpg")
            End If
            proceso.StartInfo.FileName = "encriptar.bat"
            proceso.StartInfo.Arguments = Chr(34) & General.pathGPG & Chr(34) & " " & Chr(34) & General.claveGPG & Chr(34) & " " & Chr(34) & patharchivo & Chr(34)
            proceso.StartInfo.WindowStyle = ProcessWindowStyle.Normal
            proceso.Start()
            'comando = "start D:\cas\encriptar.bat " & Chr(34) & General.pathGPG & Chr(34) & " " & Chr(34) & General.claveGPG & Chr(34) & " " & Chr(34) & patharchivo & Chr(34)
            'Shell("cmd.exe /K " & comando, AppWinStyle.MinimizedNoFocus)
        End Function

        Public Shared Function desencriptararchivoGPG(ByVal patharchivo As String, ByVal output As String, ByVal clave As String) As Boolean
            Try
                Dim comando As String = ""
                Dim proceso As New Process
                proceso.StartInfo.FileName = "desencripta_oro.bat"
                proceso.StartInfo.Arguments = Chr(34) & General.pathGPG & Chr(34) & " " & Chr(34) & clave & Chr(34) & " " & Chr(34) & output.Trim & Chr(34) & " " & Chr(34) & patharchivo.Trim & Chr(34)
                proceso.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                FileSystemHelper.Log(" GeneralBO:desencriptararchivoGPG - Argumentos: " & proceso.StartInfo.Arguments)
                proceso.Start()
            Catch ex As Exception
                FileSystemHelper.Log(" GeneralBO:desencriptararchivoGPG - Fallo al desencriptar - Excepción: " & ex.Message)
                Throw New Exception(" Fallo al desencriptar - Excepción: " & ex.Message)
            End Try
        End Function

        Public Shared Function CrearDirectorioFTP(ByVal dir As String, Optional ByRef pcarpeta As String = "") As Boolean
            Dim clsRequest As System.Net.FtpWebRequest
            Dim servidorFTP As String
            Dim usuarioFTP As String
            Dim pwdFTP As String
            Dim cadena As String = ""
            If General.Jurisdiccion = "E" Then
                cadena = "iafas_"
            End If

            servidorFTP = General.servidorFTP
            If Not servidorFTP.EndsWith("/") Then
                servidorFTP = servidorFTP & "/" & Trim(cadena) & dir
            Else
                servidorFTP = servidorFTP & Trim(cadena) & dir
            End If
            'si la carpeta ya existe salgo de la funcion
            If existeDirectorio(servidorFTP) Then
                CrearDirectorioFTP = True
                pcarpeta = servidorFTP & "/"
                Exit Function

            End If

            usuarioFTP = General.usuarioFTP
            pwdFTP = General.pwdFTP

            CrearDirectorioFTP = False
            clsRequest = CType(System.Net.FtpWebRequest.Create(servidorFTP), System.Net.FtpWebRequest)
            clsRequest.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.
            clsRequest.Credentials = New System.Net.NetworkCredential(usuarioFTP, pwdFTP) ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.MakeDirectory
            Try
                Dim respuesta As System.Net.FtpWebResponse
                respuesta = CType(clsRequest.GetResponse(), System.Net.FtpWebResponse)
                respuesta.Close()
                pcarpeta = servidorFTP & "/"
                CrearDirectorioFTP = True
            Catch ex As Exception
                Throw New Exception("Problema al crear directorio por FTP:" & ex.Message)
                pcarpeta = ""
                Return False
            End Try
        End Function


        Public Shared Function existeDirectorio(ByVal dir As String) As Boolean
            Dim peticionFTP As System.Net.FtpWebRequest
            Dim usuarioFTP As String
            Dim pwdFTP As String

            usuarioFTP = General.usuarioFTP
            pwdFTP = General.pwdFTP
            ' Creamos una peticion FTP con la dirección del objeto que queremos saber si existe
            peticionFTP = CType(System.Net.WebRequest.Create(New Uri(dir)), System.Net.FtpWebRequest)
            peticionFTP.Proxy = Nothing

            ' Fijamos el usuario y la contraseña de la petición
            peticionFTP.Credentials = New NetworkCredential(usuarioFTP, pwdFTP)

            ' Para saber si el objeto existe, solicitamos la fecha de creación del mismo
            peticionFTP.Method = WebRequestMethods.Ftp.PrintWorkingDirectory

            peticionFTP.UsePassive = False

            Try
                ' Si el objeto existe, se devolverá True
                Dim respuestaFTP As FtpWebResponse
                respuestaFTP = CType(peticionFTP.GetResponse(), FtpWebResponse)
                Return True
            Catch ex As Exception
                ' Si el objeto no existe, se producirá un error y al entrar por el Catch
                ' se devolverá falso
                Return False
            End Try
        End Function
        Public Shared Function existeArchivo(ByVal dir As String) As Boolean
            Dim peticionFTP As System.Net.FtpWebRequest
            Dim usuarioFTP As String
            Dim pwdFTP As String

            usuarioFTP = General.usuarioFTP
            pwdFTP = General.pwdFTP
            ' Creamos una peticion FTP con la dirección del objeto que queremos saber si existe
            peticionFTP = CType(System.Net.WebRequest.Create(New Uri(dir)), System.Net.FtpWebRequest)
            peticionFTP.Proxy = Nothing

            ' Fijamos el usuario y la contraseña de la petición
            peticionFTP.Credentials = New NetworkCredential(usuarioFTP, pwdFTP)

            ' Para saber si el objeto existe, solicitamos la fecha de creación del mismo
            peticionFTP.Method = WebRequestMethods.Ftp.GetDateTimestamp

            peticionFTP.UsePassive = False

            Try
                ' Si el objeto existe, se devolverá True
                Dim respuestaFTP As FtpWebResponse
                respuestaFTP = CType(peticionFTP.GetResponse(), FtpWebResponse)
                FileSystemHelper.Log(" Existe archivo:" & dir)
                Return True
            Catch ex As Exception
                ' Si el objeto no existe, se producirá un error y al entrar por el Catch
                ' se devolverá falso
                FileSystemHelper.Log("NO Existe archivo:" & dir)
                Return False
            End Try
        End Function

        Public Shared Function eliminaArchivoFTP(ByVal dir As String) As Boolean
            Dim clsRequest As System.Net.FtpWebRequest

            Dim usuarioFTP As String
            Dim pwdFTP As String

            usuarioFTP = General.usuarioFTP
            pwdFTP = General.pwdFTP

            eliminaArchivoFTP = False
            clsRequest = CType(System.Net.FtpWebRequest.Create(dir), System.Net.FtpWebRequest)
            clsRequest.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.
            clsRequest.Credentials = New System.Net.NetworkCredential(usuarioFTP, pwdFTP) ' Usuario y password de acceso al server FTP, si no tubiese, dejar entre comillas, osea ""
            clsRequest.Method = System.Net.WebRequestMethods.Ftp.DeleteFile
            Try
                Dim respuesta As System.Net.FtpWebResponse
                respuesta = CType(clsRequest.GetResponse(), System.Net.FtpWebResponse)
                respuesta.Close()
                eliminaArchivoFTP = True
            Catch ex As Exception

                Throw New Exception("Problema al eliminar archivo por FTP:" & dir & " - " & ex.Message)

                Return False
            End Try
        End Function

        Public Shared Function DescargarArchivoFTP(ByVal nombreArchivo As String, ByVal destinoArchivo As String) As Boolean
            Dim peticionFTP As System.Net.FtpWebRequest
            Dim usuarioFTP As String
            Dim pwdFTP As String
            Dim carpeta As String = ""
            Dim oupstrem As New FileStream(destinoArchivo, FileMode.CreateNew)

            Dim bufferSize As Integer = 2048
            Dim readCount As Integer
            Dim buffer() As Byte = New Byte(bufferSize) {}


            usuarioFTP = General.usuarioFTP
            pwdFTP = General.pwdFTP

            ' Para saber si el objeto existe, solicitamos la fecha de creación del mismo

            If CrearDirectorioFTP("Extracto_pdf", carpeta) Then
                nombreArchivo = carpeta & nombreArchivo
            End If
            ' Creamos una peticion FTP con la dirección del objeto que queremos saber si existe
            peticionFTP = CType(System.Net.WebRequest.Create(New Uri(nombreArchivo)), System.Net.FtpWebRequest)
            peticionFTP.Proxy = Nothing

            ' Fijamos el usuario y la contraseña de la petición
            peticionFTP.Credentials = New NetworkCredential(usuarioFTP, pwdFTP)
            peticionFTP.Method = WebRequestMethods.Ftp.DownloadFile
            peticionFTP.UseBinary = True
            peticionFTP.Timeout = 360000


            'peticionFTP.UsePassive = False
            '' Obtener el resultado del comando
            Dim escritor As Stream
            escritor = peticionFTP.GetRequestStream()

            readCount = escritor.Read(buffer, 0, bufferSize)
            '' Leer el stream (el contenido del archivo)
            '  Dim res As String = reader.ReadToEnd()

            'readCount > 0

            '       outputStream.Write(buffer, 0, readCount);
            '       readCount = ftpStream.Read(buffer, 0, bufferSize);





            ''******
            'Dim dirFtp As FtpWebRequest = CType(FtpWebRequest.Create(ficFTP), FtpWebRequest)

            '' Los datos del usuario (credenciales)
            'Dim cr As New NetworkCredential(user, pass)
            'dirFtp.Credentials = cr

            '' El comando a ejecutar usando la enumeración de WebRequestMethods.Ftp
            'dirFtp.Method = WebRequestMethods.Ftp.DownloadFile

            '' Obtener el resultado del comando
            'Dim reader As New StreamReader(dirFtp.GetResponse().GetResponseStream())

            '' Leer el stream (el contenido del archivo)
            'Dim res As String = reader.ReadToEnd()

            '' Mostrarlo.
            ''Console.WriteLine(res)

            '' Guardarlo localmente con la extensión .txt
            'Dim ficLocal As String = Path.Combine(dirLocal, Path.GetFileName(ficFTP) & ".txt")
            'Dim sw As New StreamWriter(ficLocal, False, Encoding.Default)
            'sw.Write(res)
            'sw.Close()

            '' Cerrar el stream abierto.
            'reader.Close()
            '*******

        End Function


        Public Shared Function DownloadFile(ByVal nombreArchivo As String, ByVal destinoArchivo As String) As Boolean
            Dim reqFTP As FtpWebRequest

            Dim fileName As String = nombreArchivo
            Dim descFilePath As String = destinoArchivo
            Dim carpeta As String = ""
            Dim UrlString As String = ""
            Dim usuarioFTP As String
            Dim pwdFTP As String

            usuarioFTP = General.usuarioFTP
            pwdFTP = General.pwdFTP
            If CrearDirectorioFTP(General.carpetaFTPExtractos, carpeta) Then
                UrlString = carpeta & nombreArchivo
            End If
            Try
                FileSystemHelper.Log(" Funcion downloadfile:quiere descargar el archivo: " & UrlString)
                reqFTP = DirectCast(FtpWebRequest.Create(UrlString), FtpWebRequest)
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile
                reqFTP.UseBinary = True

                reqFTP.Credentials = New NetworkCredential(usuarioFTP, pwdFTP)

                reqFTP.Proxy = Nothing

                FileSystemHelper.Log(" Funcion downloadfile:logueado OK con usuario : " & usuarioFTP)
                Using outputStream As New FileStream(descFilePath, FileMode.OpenOrCreate)
                    FileSystemHelper.Log(" Funcion downloadfile:crea archivo ")
                    Using response As FtpWebResponse = DirectCast(reqFTP.GetResponse(), FtpWebResponse)
                        FileSystemHelper.Log(" Funcion downloadfile:realiza ftpwebresponse")
                        Using ftpStream As Stream = response.GetResponseStream()
                            Dim bufferSize As Integer = 2048
                            Dim readCount As Integer
                            Dim buffer As Byte() = New Byte(bufferSize - 1) {}
                            FileSystemHelper.Log(" Funcion downloadfile:empieza a leer archivo")
                            readCount = ftpStream.Read(buffer, 0, bufferSize)
                            While readCount > 0
                                outputStream.Write(buffer, 0, readCount)
                                readCount = ftpStream.Read(buffer, 0, bufferSize)
                            End While
                            FileSystemHelper.Log(" Funcion downloadfile:Termino de leer archivo")
                        End Using

                    End Using
                End Using
                Return True

            Catch ex As Exception
                FileSystemHelper.Log("Problema al ejecutar funcion downloadfile " & ex.Message)
                Throw New Exception("Failed to download", ex.InnerException)
            End Try
        End Function

        Public Shared Function ObtenerClaveJur(ByVal idLoteria As String) As String
            Dim general As New GeneralDAL
            Try
                Return general.ObtenerClaveJur(idLoteria)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return ""
            End Try

        End Function

        Public Shared Function ObtenerCarpetaArchivosExtractoOtrasJuris(ByVal idJuego As Integer, ByVal idLoteria As String) As String
            Dim general As New GeneralDAL
            Try
                Return general.ObtenerCarpetaArchivosExtractoOtrasJuris(idJuego, idLoteria)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function

    End Class
End Namespace