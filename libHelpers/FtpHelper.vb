Imports libEntities.Entities
Imports System.Net.FtpWebRequest
Imports System.Net
Imports System.IO
Imports System.Text


Public Class FtpHelper
    ' ------------------------------------------
    ' Directorio

    Public Function existeDirectorio(ByVal oSrvFTP As SrvFTP, ByVal pathFTP As String) As Boolean
        Dim _dirRaiz As String = oSrvFTP.DirRaiz.Trim
        Dim _pathFtp As String = pathFTP.Trim
        Dim strUri As String = ""
        Return True
        Dim peticionFTP As FtpWebRequest

        _dirRaiz.Replace("\", "/")
        If Not _dirRaiz.StartsWith("/") Then ' le agrego la primer barra
            _dirRaiz = "/" & _dirRaiz
        End If
        If _dirRaiz.EndsWith("/") Then ' le saco la ultima barra
            _dirRaiz = _dirRaiz.Substring(0, _dirRaiz.LastIndexOf("/"))
        End If

        _pathFtp.Replace("\", "/")
        If Not _pathFtp.StartsWith("/") Then ' le agrego la primer barra
            _pathFtp = "/" & _pathFtp
        End If
        If _pathFtp.Length > 1 And _pathFtp.EndsWith("/") Then ' le saco la ultima barra
            _pathFtp = _pathFtp.Substring(0, _pathFtp.LastIndexOf("/"))
        End If

        strUri = oSrvFTP.Proto & "://" & oSrvFTP.Servidor & _dirRaiz & _pathFtp
        ' strUri = "FTP://172.18.20.132/SANTA FE"
        ' Creamos una peticion FTP con la dirección del objeto que queremos saber si existe
        peticionFTP = CType(WebRequest.Create(New Uri(strUri)), FtpWebRequest)

        ' Fijamos el usuario y la contraseña de la petición
        peticionFTP.Credentials = New NetworkCredential(oSrvFTP.Usr, oSrvFTP.Pwd)

        ' Para saber si el objeto existe, solicitamos la fecha de creación del mismo
        ''peticionFTP.Method = WebRequestMethods.Ftp.GetDateTimestamp
        peticionFTP.Method = WebRequestMethods.Ftp.ListDirectory
        'peticionFTP.KeepAlive = True
        peticionFTP.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.

        peticionFTP.UsePassive = oSrvFTP.Pasivo
        peticionFTP.EnableSsl = oSrvFTP.Ssl

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

    Public Function creaDirectorio(ByVal oSrvFTP As SrvFTP, ByVal pathFTP As String, ByRef msgErr As String) As Boolean
        Dim _dirRaiz As String = oSrvFTP.DirRaiz.Trim
        Dim _pathFtp As String = pathFTP.Trim
        Dim strUri As String = ""

        Dim peticionFTP As FtpWebRequest

        _dirRaiz.Replace("\", "/")
        If Not _dirRaiz.StartsWith("/") Then ' le agrego la primer barra
            _dirRaiz = "/" & _dirRaiz
        End If
        If _dirRaiz.EndsWith("/") Then ' le saco la ultima barra
            _dirRaiz = _dirRaiz.Substring(0, _dirRaiz.LastIndexOf("/"))
        End If

        _pathFtp.Replace("\", "/")
        If Not _pathFtp.StartsWith("/") Then ' le agrego la primer barra
            _pathFtp = "/" & _pathFtp
        End If
        If _pathFtp.Length > 1 And _pathFtp.EndsWith("/") Then ' le saco la ultima barra
            _pathFtp = _pathFtp.Substring(0, _pathFtp.LastIndexOf("/"))
        End If

        strUri = oSrvFTP.Proto & "://" & oSrvFTP.Servidor & _dirRaiz & _pathFtp
        ' strUri = "FTP://172.18.20.132/SANTA FE"

        ' Creamos una peticion FTP con la dirección del directorio que queremos crear
        peticionFTP = CType(WebRequest.Create(New Uri(strUri)), FtpWebRequest)

        ' Fijamos el usuario y la contraseña de la petición
        peticionFTP.Credentials = New NetworkCredential(oSrvFTP.Usr, oSrvFTP.Pwd)

        ' Seleccionamos el comando que vamos a utilizar: Crear un directorio
        peticionFTP.Method = WebRequestMethods.Ftp.MakeDirectory

        peticionFTP.UsePassive = oSrvFTP.Pasivo
        peticionFTP.EnableSsl = oSrvFTP.Ssl

        Try
            Dim respuesta As FtpWebResponse
            respuesta = CType(peticionFTP.GetResponse(), FtpWebResponse)
            respuesta.Close()
            ' Si todo ha ido bien, se devolverá true
            msgErr = ""
            Return True
        Catch ex As Exception
            ' Si se produce algún fallo, se devolverá el mensaje del error
            msgErr = ex.Message
            Return False
        End Try
    End Function

    ' ------------------------------------------
    ' Archivo

    Public Function existeArchivo(ByVal archivo As String, ByVal oSrvFTP As SrvFTP, ByVal pathFTP As String) As Boolean
        Dim _dirRaiz As String = oSrvFTP.DirRaiz.Trim
        Dim _pathFtp As String = pathFTP.Trim
        Dim _archivo As String = archivo.Trim
        Dim strUri As String = ""
        Return False
        Dim peticionFTP As FtpWebRequest

        _dirRaiz.Replace("\", "/")
        If Not _dirRaiz.StartsWith("/") Then ' le agrego la primer barra
            _dirRaiz = "/" & _dirRaiz
        End If
        If _dirRaiz.EndsWith("/") Then ' le saco la ultima barra
            _dirRaiz = _dirRaiz.Substring(0, _dirRaiz.LastIndexOf("/"))
        End If

        _pathFtp.Replace("\", "/")
        If Not _pathFtp.StartsWith("/") Then ' le agrego la primer barra
            _pathFtp = "/" & _pathFtp
        End If
        If _pathFtp.Length > 1 And _pathFtp.EndsWith("/") Then ' le saco la ultima barra
            _pathFtp = _pathFtp.Substring(0, _pathFtp.LastIndexOf("/"))
        End If

        _archivo.Replace("\", "/")
        If Not _archivo.StartsWith("/") Then ' le agrego la primer barra
            _archivo = "/" & _archivo
        End If
        If _archivo.Length > 1 And _archivo.EndsWith("/") Then ' le saco la ultima barra
            _archivo = _archivo.Substring(0, _archivo.LastIndexOf("/"))
        End If

        strUri = oSrvFTP.Proto & "://" & oSrvFTP.Servidor & _dirRaiz & _pathFtp & _archivo

        'oSrvFTP.Proto & "://" & oSrvFTP.Servidor & IIf(oSrvFTP.DirRaiz = "", "", "/") & oSrvFTP.DirRaiz & IIf(pathFTP = "", "", "/") & pathFTP & IIf(archivo = "", "", "/") & archivo

        ' Creamos una peticion FTP con la dirección del objeto que queremos saber si existe
        peticionFTP = CType(WebRequest.Create(New Uri(strUri)), FtpWebRequest)

        ' Fijamos el usuario y la contraseña de la petición
        peticionFTP.Credentials = New NetworkCredential(oSrvFTP.Usr, oSrvFTP.Pwd)

        ' Para saber si el objeto existe, solicitamos la fecha de creación del mismo
        peticionFTP.Method = WebRequestMethods.Ftp.GetDateTimestamp

        peticionFTP.UsePassive = oSrvFTP.Pasivo
        peticionFTP.EnableSsl = oSrvFTP.Ssl

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

    Public Function delArchivo(ByVal archivo As String, ByVal oSrvFTP As SrvFTP, ByVal pathFTP As String, ByRef msgErr As String) As Boolean
        Dim peticionFTP As FtpWebRequest
        Dim strUri As String = oSrvFTP.Proto & "://" & oSrvFTP.Servidor & IIf(oSrvFTP.DirRaiz = "", "", "/") & oSrvFTP.DirRaiz & IIf(pathFTP = "", "", "/") & pathFTP & IIf(archivo = "", "", "/") & archivo

        ' Creamos una petición FTP con la dirección del archivo a eliminar
        peticionFTP = CType(WebRequest.Create(New Uri(strUri)), FtpWebRequest)

        ' Fijamos el usuario y la contraseña de la petición
        peticionFTP.Credentials = New NetworkCredential(oSrvFTP.Usr, oSrvFTP.Pwd)

        ' Seleccionamos el comando que vamos a utilizar: Eliminar un archivo
        peticionFTP.Method = WebRequestMethods.Ftp.DeleteFile
        peticionFTP.UsePassive = oSrvFTP.Pasivo
        peticionFTP.EnableSsl = oSrvFTP.Ssl

        Try
            Dim respuestaFTP As FtpWebResponse
            respuestaFTP = CType(peticionFTP.GetResponse(), FtpWebResponse)
            respuestaFTP.Close()
            ' Si todo ha ido bien, devolvemos String.Empty
            Return True
        Catch ex As Exception
            ' Si se produce algún fallo, se devolverá el mensaje del error
            msgErr = ex.Message
            Return False
        End Try
    End Function
    Public Function RemoteCertificateValidationCallback(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
        Return True
    End Function
    Public Function putArchivo(ByVal archivo As String, ByVal pathOrigen As String, ByVal oSrvFTP As SrvFTP, ByVal pathFTP As String, ByRef msgErr As String) As Boolean
        Dim _origen As String = (pathOrigen & "\" & archivo).Replace("/", "\").Replace("\/", "\").Replace("\\", "\")
        If (Not _origen.StartsWith("\\")) And _origen.StartsWith("\") Then
            _origen = "\" & _origen
        End If

        Dim infoarchivo As New FileInfo(_origen)
        Dim _dirRaiz As String = oSrvFTP.DirRaiz.Trim
        Dim _pathFtp As String = pathFTP.Trim
        Dim _archivo As String = archivo.Trim
        Dim strUri As String = ""

        ' Si no existe el archivo origen salimos con false
        If Not FileSystemHelper.ExisteArchivo(_origen) Then
            msgErr = "El archivo origen " & _origen & " no existe. Verifique."
            Return False
        End If

        ' Si no existe el directorio, lo creamos
        If Not existeDirectorio(oSrvFTP, pathFTP) Then
            creaDirectorio(oSrvFTP, pathFTP, msgErr)
        End If

        _dirRaiz.Replace("\", "/")
        If Not _dirRaiz.StartsWith("/") Then ' le agrego la primer barra
            _dirRaiz = "/" & _dirRaiz
        End If
        If _dirRaiz.EndsWith("/") Then ' le saco la ultima barra
            _dirRaiz = _dirRaiz.Substring(0, _dirRaiz.LastIndexOf("/"))
        End If

        _pathFtp.Replace("\", "/")
        If Not _pathFtp.StartsWith("/") Then ' le agrego la primer barra
            _pathFtp = "/" & _pathFtp
        End If
        If _pathFtp.Length > 1 And _pathFtp.EndsWith("/") Then ' le saco la ultima barra
            _pathFtp = _pathFtp.Substring(0, _pathFtp.LastIndexOf("/"))
        End If

        _archivo.Replace("\", "/")
        If Not _archivo.StartsWith("/") Then ' le agrego la primer barra
            _archivo = "/" & _archivo
        End If
        If _archivo.Length > 1 And _archivo.EndsWith("/") Then ' le saco la ultima barra
            _archivo = _archivo.Substring(0, _archivo.LastIndexOf("/"))
        End If

        strUri = oSrvFTP.Proto & "://" & oSrvFTP.Servidor & _dirRaiz & _pathFtp & _archivo

        ' ***********************************************
        'Return enviarFTP(_origen, strUri)
        ' ***********************************************

        ' Creamos una peticion FTP con la dirección del archivo que vamos a subir
        Dim peticionFTP As FtpWebRequest
        peticionFTP = CType(FtpWebRequest.Create(New Uri(strUri)), FtpWebRequest)

        ' Fijamos el usuario y la contraseña de la petición
        peticionFTP.Credentials = New NetworkCredential(oSrvFTP.Usr, oSrvFTP.Pwd)

        peticionFTP.UsePassive = oSrvFTP.Pasivo

        ' Seleccionamos el comando que vamos a utilizar: Subir un archivo
        peticionFTP.Method = WebRequestMethods.Ftp.UploadFile

        ' Especificamos el tipo de transferencia de datos
        'peticionFTP.KeepAlive = False
        'peticionFTP.Timeout = (60 * 1000) * 3 '3 mins
        'peticionFTP.UseBinary = False
        peticionFTP.Proxy = Nothing ' Esta asignación es importantisimo con los que trabajen en windows XP ya que por defecto esta propiedad esta para ser asignado a un servidor http lo cual ocacionaria un error si deseamos conectarnos con un FTP, en windows Vista y el Seven no tube este problema.

        peticionFTP.UsePassive = oSrvFTP.Pasivo
        peticionFTP.EnableSsl = oSrvFTP.Ssl

        ' Informamos al servidor sobre el tamaño del archivo que vamos a subir
        peticionFTP.ContentLength = infoarchivo.Length
        System.Net.ServicePointManager.ServerCertificateValidationCallback = New System.Net.Security.RemoteCertificateValidationCallback(AddressOf RemoteCertificateValidationCallback)

        Try
            Dim bFile() As Byte = System.IO.File.ReadAllBytes(_origen)
            Dim clsStream As System.IO.Stream = Nothing
            Try
                clsStream = peticionFTP.GetRequestStream()
            Catch ex1 As Exception
            End Try

            clsStream.Write(bFile, 0, bFile.Length)
            clsStream.Close()
            clsStream.Dispose()

            Return True
        Catch ex As Exception
            Throw New Exception("Problema al enviar archivo de extracto a LotBA por FTP:" & ex.Message)
            Return False
        End Try

    End Function

    Public Function getArchivo(ByVal archivo As String, ByVal pathDestino As String, ByVal oSrvFTP As SrvFTP, ByVal pathFTP As String, ByRef msgErr As String) As Boolean
        Dim infoarchivo As New FileInfo(pathDestino & "/" & archivo)

        Dim strUri As String = oSrvFTP.Proto & "://" & oSrvFTP.Servidor & IIf(oSrvFTP.DirRaiz = "", "", "/") & oSrvFTP.DirRaiz & IIf(pathFTP = "", "", "/") & pathFTP & IIf(archivo = "", "", "/") & archivo
        Dim dirFTP As String = oSrvFTP.Servidor & "/" & pathFTP

        msgErr = ""

        Dim peticionFTP As FtpWebRequest


        ' Creamos una peticion FTP con la dirección del archivo que vamos a subir
        peticionFTP = CType(FtpWebRequest.Create(New Uri(strUri)), FtpWebRequest)


        ' Fijamos el usuario y la contraseña de la petición
        peticionFTP.Credentials = New NetworkCredential(oSrvFTP.Usr, oSrvFTP.Pwd)

        ' Seleccionamos el comando que vamos a utilizar: Descargar un archivo
        peticionFTP.Method = WebRequestMethods.Ftp.DownloadFile

        ' Especificamos el tipo de transferencia de datos
        peticionFTP.UseBinary = True

        ' Informamos al servidor sobre el tamaño del archivo que vamos a Descargar
        peticionFTP.ContentLength = infoarchivo.Length

        peticionFTP.UsePassive = oSrvFTP.Pasivo
        peticionFTP.EnableSsl = oSrvFTP.Ssl

        Try

            ' Obtener el resultado del comando
            Dim reader As New StreamReader(peticionFTP.GetResponse().GetResponseStream())

            ' Leer el stream (el contenido del archivo)
            Dim res As String = reader.ReadToEnd()

            ' Mostrarlo.
            'Console.WriteLine(res)

            ' Guardarlo localmente 
            Dim sw As New StreamWriter(pathDestino & "/" & archivo, False, Encoding.Default)
            sw.Write(res)
            sw.Close()

            ' Cerrar el stream abierto.
            reader.Close()
            '' '''''''''''''''''''''''''''''''''''''
            ' Si todo ha ido bien, se devolverá true
            Return True
        Catch ex As Exception
            ' Si se produce algún fallo, se devolverá el mensaje del error
            msgErr = ex.Message
            Return False
        End Try

    End Function

    ' ------------------------------------------
    ' Lista de Archivos

    Public Function delListaArchivo(ByVal lstFile() As String, ByVal separador As String, ByVal oSrvFTP As SrvFTP, ByRef msgErr As String) As Boolean
        Dim i As Integer
        Dim matValue() As String
        Dim origen As String
        Dim archivo As String
        Dim destino As String

        If lstFile.Count = 0 Then Exit Function

        Try
            For i = 0 To lstFile.GetLength(0) - 1
                matValue = Split(lstFile(i), separador)
                origen = matValue(0)
                destino = matValue(1)
                archivo = matValue(2)
                If existeArchivo(archivo, oSrvFTP, destino) Then
                    If Not delArchivo(archivo, oSrvFTP, destino, msgErr) Then
                        Return False
                    End If
                End If
            Next
            '' '''''''''''''''''''''''''''''''''''''
            ' Si todo ha ido bien, se devolverá true
            Return True
        Catch ex As Exception
            ' Si se produce algún fallo, se devolverá el mensaje del error
            msgErr = ex.Message
            Return False
        End Try
    End Function

    Public Function putListaArchivo(ByVal lstFile() As String, ByVal separador As String, ByVal oSrvFTP As SrvFTP, ByRef msgErr As String) As Boolean
        Dim i As Integer
        Dim matValue() As String
        Dim origen As String
        Dim archivo As String
        Dim destino As String

        If lstFile.Count = 0 Then Return True

        Try
            FileSystemHelper.Log(" FtpHelper.putListaArchivo - INICIO")
            For i = 0 To lstFile.GetLength(0) - 1
                matValue = Split(lstFile(i), separador)
                origen = matValue(0)
                destino = matValue(1)
                archivo = matValue(2)

                If Not putArchivo(archivo, origen, oSrvFTP, destino, msgErr) Then
                    FileSystemHelper.Log(" FtpHelper.putListaArchivo - PUT FALLIDO de:  origen ->" & origen & "<-  destino ->" & destino & "<-   archivo ->" & archivo & "<-")
                    Return False
                End If
            Next
            '' '''''''''''''''''''''''''''''''''''''
            ' Si todo ha ido bien, se devolverá true
            FileSystemHelper.Log(" FtpHelper.putListaArchivo - FIN OK")

            Return True
        Catch ex As Exception
            ' Si se produce algún fallo, se devolverá el mensaje del error
            FileSystemHelper.Log(" FtpHelper.putListaArchivo - FIN ERROR msgRet ->" & msgErr & "<-")
            msgErr = ex.Message
            Return False
        End Try
    End Function

    Public Function getListaArchivo(ByVal lstFile() As String, ByVal separador As String, ByVal oSrvFTP As SrvFTP, ByRef msgErr As String) As Boolean
        Dim i As Integer
        Dim matValue() As String
        Dim origen As String
        Dim archivo As String
        Dim destino As String

        If lstFile.Count = 0 Then Exit Function

        Try
            For i = 0 To lstFile.GetLength(0) - 1
                matValue = Split(lstFile(i), separador)
                origen = matValue(0)
                destino = matValue(1)
                archivo = matValue(2)
                If getArchivo(archivo, destino, oSrvFTP, origen, msgErr) Then
                    Return False
                End If
            Next
            '' '''''''''''''''''''''''''''''''''''''
            ' Si todo ha ido bien, se devolverá true
            Return True
        Catch ex As Exception
            ' Si se produce algún fallo, se devolverá el mensaje del error
            msgErr = ex.Message
            Return False
        End Try
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
End Class