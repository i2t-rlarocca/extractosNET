Imports Microsoft.VisualBasic
Imports System.Windows.Forms
Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.GZip
Imports System.Text
Imports System.Security.Cryptography



Public Class FileSystemHelper

    Public Shared Function encriptarArchivo(ByVal pathArchivo As String, ByVal metodo As String, ByRef msgRet As String, Optional ByVal ExeMetodo As String = "", Optional ByVal pwdMetodo As String = "") As Boolean
        Try
            Select Case metodo
                Case "GPG"
                    Dim comando As String = ""
                    Dim proceso As New Process
                    If System.IO.File.Exists(pathArchivo & ".gpg") Then
                        System.IO.File.Delete(pathArchivo & ".gpg")
                    End If
                    proceso.StartInfo.FileName = "encriptar.bat"
                    proceso.StartInfo.Arguments = Chr(34) & ExeMetodo & Chr(34) & " " & Chr(34) & pwdMetodo & Chr(34) & " " & Chr(34) & pathArchivo & Chr(34)
                    proceso.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                    proceso.Start()
                    'comando = "start D:\cas\encriptar.bat " & Chr(34) & General.pathGPG & Chr(34) & " " & Chr(34) & General.claveGPG & Chr(34) & " " & Chr(34) & patharchivo & Chr(34)
                    'Shell("cmd.exe /K " & comando, AppWinStyle.MinimizedNoFocus)
                Case Else
                    msgRet = "Método " & metodo & " no implementado."
                    Return False
            End Select
            Return True
        Catch ex As Exception
            msgRet = ex.Message()
            Return False
        End Try
    End Function

    Public Shared Function desEncriptarArchivo(ByVal pathArchivo As String, ByVal metodo As String, ByRef destino As String, ByRef msgRet As String, Optional ByVal ExeMetodo As String = "", Optional ByVal pwdMetodo As String = "") As Boolean
        Try
            Select Case metodo
                Case "GPG"
                    Dim comando As String = ""
                    Dim proceso As New Process
                    ''If System.IO.File.Exists(pathArchivo & ".gpg") Then
                    ''    System.IO.File.Delete(pathArchivo & ".gpg")
                    ''End If
                    proceso.StartInfo.FileName = "desencriptar_interjur.bat"
                    proceso.StartInfo.Arguments = Chr(34) & ExeMetodo & Chr(34) & " " & Chr(34) & pwdMetodo & Chr(34) & " " & destino & Chr(34) & pathArchivo & Chr(34)
                    proceso.StartInfo.WindowStyle = ProcessWindowStyle.Normal
                    proceso.Start()
                    'comando = "start D:\cas\encriptar.bat " & Chr(34) & General.pathGPG & Chr(34) & " " & Chr(34) & General.claveGPG & Chr(34) & " " & Chr(34) & patharchivo & Chr(34)
                    'Shell("cmd.exe /K " & comando, AppWinStyle.MinimizedNoFocus)
                Case Else
                    msgRet = "Método " & metodo & " no implementado."
                    Return False
            End Select
            Return True
        Catch ex As Exception
            msgRet = ex.Message()
            Return False
        End Try
    End Function

    Public Shared Sub GeneraArchivoTXTDesdeArray(ByVal registros() As String, ByVal path As String, ByVal nombre As String, ByVal msgErr As String)
        'Variables para abrir el archivo en modo de escritura
        Dim strStreamW As Stream
        Dim strStreamWriter As StreamWriter
        Try
            ' Verifico que exista el directorio
            If Not CrearPath(path) Then
                msgErr = "GeneraArchivoTXTDesdeArray: No se pudo crear la ruta para el paquete."
                Exit Sub
            End If

            Dim FilePath As String = path & "\" & nombre

            'Se abre el archivo y si este no existe se crea
            strStreamW = File.OpenWrite(FilePath)
            strStreamWriter = New StreamWriter(strStreamW, _
                                System.Text.Encoding.UTF8)

            'Se traen los datos que necesitamos para el archivo
            'TraerDatosArchivo retorna un dataset pero perfectamente
            'podria ser cualquier otro tipo de objeto
            For i = 0 To UBound(registros) - 1
                'Escribimos la línea en el achivo de texto
                If i = 0 Then
                    strStreamWriter.WriteLine(registros(i).Trim)
                Else
                    strStreamWriter.WriteLine(registros(i))
                End If
            Next

            strStreamWriter.Close()

            msgErr = ""

        Catch ex As Exception
            Try
                strStreamWriter.Close()
            Catch exI As Exception
            End Try

            msgErr = "GeneraArchivoTXTDesdeArray: " & ex.Message
        End Try
    End Sub

    Public Shared Function BorrarPath(ByVal path As String, Optional ByVal borrarContenido As Boolean = True) As Boolean
        Try
            System.IO.Directory.Delete(path, borrarContenido)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function ExisteArchivo(ByVal nomArchivo As String) As Boolean
        Dim existe As Boolean = False
        Try
            If File.Exists(nomArchivo) Then
                existe = True
            End If
        Catch ex As Exception
        End Try
        Return existe
    End Function

    Public Shared Function getDescArchivoDesdeOpenDialog(ByRef openFileD As System.Windows.Forms.OpenFileDialog, ByRef nomArch As String, ByRef pathArch As String, ByRef extArch As String, Optional ByVal filter As String = "*.*", Optional ByVal defaultExt As String = "", Optional ByVal initialDir As String = "c:\") As String
        Dim nombreConPath As String = ""

        ' Inicializo parametros de retorno
        nomArch = ""
        extArch = ""
        pathArch = ""
        nombreConPath = ""

        ' Configuro el dialog para que permita seleccionar un unico archivo y con los filtros recibidos
        openFileD.Multiselect = False
        openFileD.SupportMultiDottedExtensions = True
        openFileD.Filter = filter
        openFileD.DefaultExt = defaultExt
        openFileD.InitialDirectory = initialDir

        ' Obtengo el archivo
        openFileD.ShowDialog()

        ' El usuario eligio algo?
        If openFileD.FileNames.Count > 0 Then
            nombreConPath = openFileD.FileNames(0)
            nomArch = openFileD.SafeFileName
            pathArch = nombreConPath.Trim.Replace("\" & nomArch, "")
            If nomArch.LastIndexOf(".") > 0 Then
                extArch = nomArch.Substring(nomArch.LastIndexOf(".") + 1, nomArch.Length - nomArch.LastIndexOf(".") - 1)
            End If
        End If
        Return nombreConPath
    End Function


    Public Shared Function CrearPath(ByVal path As String) As Boolean
        Try
            If Not Directory.Exists(path) Then
                Directory.CreateDirectory(path)
            End If
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Sub CopiarListaArchivos(ByVal lstFile() As String, ByVal separador As String)
        Dim i As Integer
        Dim matValue() As String
        Dim origen As String
        Dim archivo As String
        Dim destino As String

        Dim archivoinfo As FileInfo

        If lstFile(0) = "" Then Exit Sub

        For i = 0 To lstFile.GetLength(0) - 1
            matValue = Split(lstFile(i), separador)
            origen = matValue(0)
            destino = matValue(1)
            archivo = matValue(2)
            If System.IO.File.Exists(origen & "\" & archivo) Then
                If Not Directory.Exists(destino) Then
                    Directory.CreateDirectory(destino)
                End If
                System.IO.File.Delete(destino & "\" & archivo)
                archivoinfo = New FileInfo(origen & "\" & archivo)
                archivoinfo.CopyTo(destino & "\" & archivo)
            End If
        Next
    End Sub

    Public Shared Sub ComprimirListaArchivos(ByVal fileNames() As String, _
                     ByVal zipPath As String, _
                     ByVal zipFic As String, _
                     ByRef msgErr As String, _
                     Optional ByVal crearAuto As Boolean = False)
        ' comprimir los ficheros del array en el zip indicado
        ' si crearAuto = True, zipfile será el directorio en el que se guardará
        ' y se generará automáticamente el nombre con la fecha y hora actual
        Dim objCrc32 As New Crc32()
        Dim strmZipOutputStream As ZipOutputStream
        msgErr = ""
        '
        If zipFic = "" Then
            zipFic = "."
            crearAuto = True
        End If
        If crearAuto Then
            ' si hay que crear el nombre del fichero
            ' éste será el path indicado y la fecha actual
            zipFic &= "\ZIP" & DateTime.Now.ToString("yyMMddHHmmss") & ".zip"
        Else
            zipFic = zipPath & "\" & zipFic
        End If

        If Not CrearPath(zipPath) Then
            msgErr = "No se pudo crear la ruta para el paquete."
            Exit Sub
        End If
        strmZipOutputStream = New ZipOutputStream(File.Create(zipFic))
        ' Compression Level: 0-9
        ' 0: no(Compression)
        ' 9: maximum compression
        strmZipOutputStream.SetLevel(6)
        '
        Dim strFile As String
        For Each strFile In fileNames
            If Not System.IO.File.Exists(strFile) Then
                Continue For
            End If
            Dim strmFile As FileStream = File.OpenRead(strFile)
            Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
            '
            strmFile.Read(abyBuffer, 0, abyBuffer.Length)
            '
            '------------------------------------------------------------------
            ' para guardar sin el primer path
            'Dim sFile As String = strFile
            'Dim i As Integer = sFile.IndexOf("\")
            'If i > -1 Then
            '    sFile = sFile.Substring(i + 1).TrimStart
            'End If
            '------------------------------------------------------------------
            '
            '------------------------------------------------------------------
            ' para guardar sólo el nombre del fichero
            ' esto sólo se debe hacer si no se procesan directorios
            ' que puedan contener nombres repetidos
            Dim sFile As String = Path.GetFileName(strFile)
            Dim theEntry As ZipEntry = New ZipEntry(sFile)
            '------------------------------------------------------------------
            '
            ' se guarda con el path completo
            'Dim theEntry As ZipEntry = New ZipEntry(strFile)
            '
            ' guardar la fecha y hora de la última modificación
            Dim fi As New FileInfo(strFile)
            theEntry.DateTime = fi.LastWriteTime
            'theEntry.DateTime = DateTime.Now
            '
            theEntry.Size = strmFile.Length
            strmFile.Close()
            objCrc32.Reset()
            objCrc32.Update(abyBuffer)
            theEntry.Crc = objCrc32.Value
            strmZipOutputStream.PutNextEntry(theEntry)
            strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
        Next
        strmZipOutputStream.Finish()
        strmZipOutputStream.Close()

    End Sub

    Public Shared Function convertFecha(ByVal fecha As Date) As String
        Dim meses(12) As String
        meses(1) = "Enero"
        meses(2) = "Febrero"
        meses(3) = "Marzo"
        meses(4) = "Abril"
        meses(5) = "Mayo"
        meses(6) = "Junio"
        meses(7) = "Julio"
        meses(8) = "Agosto"
        meses(9) = "Septiembre"
        meses(10) = "Octubre"
        meses(11) = "Noviembre"
        meses(12) = "Diciembre"

        Return fecha.Day.ToString & " de " & meses(fecha.Month) & " de " & fecha.Year.ToString
    End Function

    Public Shared Sub Log(ByVal texto As String)

        'creamos el nombre del archivo
        Dim archivo = General.ArchivoLog  'ConfigurationManager.AppSettings("pathLogs").ToString & "\pruebas.txt"
        'Dim archivo = "I:\DATOS\DEMOEXTRACTOS\Logs\pruebas.txt"
        If Not archivo.EndsWith("\") Then archivo = archivo & "\"
        archivo = archivo & "sorteosCAS.log"
        Dim path As String = archivo.Trim.Substring(0, archivo.Trim.LastIndexOf("\"))
        FileSystemHelper.CrearPath(path)
        Dim f As StreamWriter = New StreamWriter(archivo, True)
        f.WriteLine(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & texto)
        f.Close()

        'conectamos con el FSO
        'Dim confile = CreateObject("scripting.filesystemobject")

        'creamos el objeto TextStream
        'Dim fich = confile.OpenTextFile(archivo, 8, True)

        'escribimos los números del 0 al 9
        'fich.writeLine(texto)

        'cerramos el fichero
        'fich.close()
    End Sub

    Public Shared Function cnvFechaAccess(ByVal fecha As String) As String

        Dim aux1 = Split(Left(fecha, 10), "/")
        Dim _fecha As Integer = (((aux1(2) * 100) + aux1(1)) * 100) + aux1(0)
        Return _fecha

    End Function

    Public Shared Function BorrarArchivo(ByVal path As String) As Boolean
        Try
            System.IO.File.Delete(path)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Sub Descomprimir(ByVal directorio As String, _
                           Optional ByVal zipFic As String = "", _
                           Optional ByVal eliminar As Boolean = False, _
                           Optional ByVal renombrar As Boolean = False)
        ' descomprimir el contenido de zipFic en el directorio indicado.
        ' si zipFic no tiene la extensión .zip, se entenderá que es un directorio y
        ' se procesará el primer fichero .zip de ese directorio.
        ' si eliminar es True se eliminará ese fichero zip después de descomprimirlo.
        ' si renombrar es True se añadirá al final .descomprimido
        '**en patharchivoFinal guarda la ruta del archivo dezipeado
        If Not zipFic.ToLower.EndsWith(".zip") Then
            zipFic = Directory.GetFiles(zipFic, "*.zip")(0)
        End If
        ' si no se ha indicado el directorio, usar el actual
        If directorio = "" Then directorio = "."
        '
        Dim z As New ZipInputStream(File.OpenRead(zipFic))
        Dim theEntry As ZipEntry
        '
        Do
            theEntry = z.GetNextEntry()
            If Not theEntry Is Nothing Then
                Dim fileName As String = directorio & "\" & Path.GetFileName(theEntry.Name)
                '
                ' dará error si no existe el path
                Dim streamWriter As FileStream
                Try
                    streamWriter = File.Create(fileName)

                Catch ex As DirectoryNotFoundException
                    Directory.CreateDirectory(Path.GetDirectoryName(fileName))
                    streamWriter = File.Create(fileName)

                End Try
                '
                Dim size As Integer
                Dim data(2048) As Byte
                Do
                    size = z.Read(data, 0, data.Length)
                    If (size > 0) Then
                        streamWriter.Write(data, 0, size)
                    Else
                        Exit Do
                    End If
                Loop
                streamWriter.Close()
            Else
                Exit Do
            End If
        Loop
        z.Close()
        '
        ' cuando se hayan extraído los ficheros, renombrarlo
        If renombrar Then
            File.Copy(zipFic, zipFic & ".descomprimido")
        End If
        If eliminar Then
            File.Delete(zipFic)
        End If
    End Sub

    Public Shared Function getHash(ByVal pathYarchivo As String, ByVal metodo As String, ByRef clave As String, ByRef msgErr As String) As Boolean
        ' metodo: MD5, SHA1
        ' encoding: "", UNICODE
        msgErr = ""

        Try
            Select Case metodo.ToUpper
                Case "MD5"
                    clave = generarMD5(pathYarchivo)

                Case "SHA1"
                    clave = generarMD5(pathYarchivo)

                Case Else
                    msgErr = "Método hash: '" & metodo & "' no implementado."
                    Return False
            End Select

            Return True
        Catch ex As Exception
            msgErr = ex.Message
            Return False
        End Try

    End Function

    Public Shared Function generarMD5(ByVal fichero As String) As String
        Dim cadenaMD5 As String = ""
        Dim cadenaFichero As FileStream
        Dim bytesFichero As [Byte]()
        Dim MD5Crypto As New MD5CryptoServiceProvider
        cadenaFichero = File.Open(fichero, FileMode.Open, FileAccess.Read)
        bytesFichero = MD5Crypto.ComputeHash(cadenaFichero)
        cadenaFichero.Close()
        cadenaMD5 = BitConverter.ToString(bytesFichero)
        cadenaMD5 = cadenaMD5.Replace("-", "")
        Return cadenaMD5
    End Function

    Public Shared Function generarSHA1(ByVal fichero As String) As String
        Dim cadenaMD5 As String = ""
        Dim cadenaFichero As FileStream
        Dim bytesFichero As [Byte]()
        Dim SHA1Crypto As New SHA1CryptoServiceProvider
        cadenaFichero = File.Open(fichero, FileMode.Open, FileAccess.Read)
        bytesFichero = SHA1Crypto.ComputeHash(cadenaFichero)
        cadenaFichero.Close()
        cadenaMD5 = BitConverter.ToString(bytesFichero)
        cadenaMD5 = cadenaMD5.Replace("-", "")
        Return cadenaMD5
    End Function

    Public Shared Function generarSHA512(ByVal fichero As String) As String
        Dim cadena512 As String = ""
        Dim pos As Integer = 0
        Dim cadenaFichero As FileStream
        Dim bytesFichero As [Byte]()
        Dim SHA512Crypto As New SHA512CryptoServiceProvider
        cadenaFichero = File.Open(fichero, FileMode.Open, FileAccess.Read)
        bytesFichero = SHA512Crypto.ComputeHash(cadenaFichero)
        cadenaFichero.Close()
        cadena512 = BitConverter.ToString(bytesFichero)
        cadena512 = cadena512.Replace("-", "")
        pos = fichero.LastIndexOf("\")
        If pos = -1 Then
            pos = fichero.LastIndexOf("/")
        End If
        Return cadena512 & " *" & Mid(fichero, pos + 2)
    End Function

    Public Shared Function ControlSha1InterJ(ByVal archivo As String, ByVal archivocontrol As String) As Boolean

        Dim _sha1 As String
        Dim sha1Archivo As String = ""
        Dim f As StreamReader

        ControlSha1InterJ = False

        'generar sha1
        _sha1 = FileSystemHelper.generarSHA1(archivo)

        'controlar con el sha1 del archivo
        f = New StreamReader(archivocontrol)

        While Not f.EndOfStream
            sha1Archivo = f.ReadLine().Trim
            If _sha1.ToLower = sha1Archivo.ToLower Then
                ControlSha1InterJ = True
                f.Dispose()
                Exit Function
            End If
        End While
        f.Dispose()

    End Function

    Public Shared Function ControlSha512InterJ(ByVal archivo As String, ByVal archivocontrol As String) As Boolean

        Dim _sha512 As String
        Dim sha512Archivo As String = ""
        Dim f As StreamReader

        ControlSha512InterJ = False

        'generar sha1
        _sha512 = FileSystemHelper.generarSHA512(archivo)

        'controlar con el sha1 del archivo
        f = New StreamReader(archivocontrol)

        While Not f.EndOfStream
            sha512Archivo = f.ReadLine().Trim
            If _sha512.ToLower = sha512Archivo.ToLower Then
                ControlSha512InterJ = True
                f.Dispose()
                Exit Function
            End If
        End While
        f.Dispose()

    End Function

    '**** 17/10/2012************
    Public Shared Function ControlArchivoMd5(ByVal archivo As String, ByVal archivocontrol As String) As Boolean
        'generar md5
        Dim md5 As String
        Dim md5archivo As String = ""
        Dim f As StreamReader
        Dim linea As String
        Dim desde As Long = 0
        Dim hasta As Long = 0
        Dim longitud As Long = 0
        md5 = FileSystemHelper.generarMD5(archivo)
        'controlar con el md5 del archivo
        'copiar zip a carpeta testigo
        'devolver archivo para continuar con la carga de premio
        f = New StreamReader(archivocontrol)

        While Not f.EndOfStream
            linea = f.ReadLine().Trim
            If InStr(linea, "<md5>") > 0 Then
                'a partir del siguiente digito a "<md5>",la longitud de un campo md5 es de 32 digitos hexadecimales
                md5archivo = Mid(linea, 6, 32)
                If md5.ToLower = md5archivo.ToLower Then
                    ControlArchivoMd5 = True
                Else
                    ControlArchivoMd5 = False
                End If
                f.Dispose()
                Exit Function
            End If
        End While
        f.Dispose()

    End Function
    '** 08/11/2012 depura los archivos de log anteriores a 60 dias de la fecha actual
    Public Shared Sub DepurarArchivoLog()
        Try
            Dim PathLogs As String = General.ArchivoLog
            Dim archivo As String = ""
            Dim archivos() As String
            Dim fechaactual As String
            Dim fechadesde As String = ""
            Dim fechaarchivodesde As Long = 0
            Dim fechaarchivo As Long = 0
            Dim nombrearchivo As String = ""
            Dim hasta As Long = 0
            Dim partefecha As String = ""

            archivos = IO.Directory.GetFiles(PathLogs)
            fechaactual = Now.AddDays(-60).ToShortDateString
            fechaactual = Mid(fechaactual, 7, 4) & Mid(fechaactual, 4, 2) & Mid(fechaactual, 1, 2)
            fechaarchivodesde = CLng(fechaactual)
            For Each archivo In archivos
                nombrearchivo = IO.Path.GetFileName(archivo)
                hasta = InStr(nombrearchivo, "_")
                If hasta <> 0 Then
                    partefecha = Mid(nombrearchivo, 1, hasta - 1)
                    If IsNumeric(partefecha) Then
                        fechaarchivo = CLng(Mid(nombrearchivo, 1, 8))
                        If fechaarchivo < fechaarchivodesde Then
                            FileSystemHelper.BorrarArchivo(archivo)
                        End If
                    End If
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub


    Public Shared Sub DepurarArchivoPDF()
        Try
            Dim path_PDF As String
            Dim s As String = Application.ExecutablePath
            Dim pos As Long
            Dim longitud As Long
            Dim archivo As String = ""
            Dim archivos() As String
            Dim nombrearchivo As String = ""
            Dim hasta As Long = 0
            Dim partefecha As String = ""
            pos = s.LastIndexOf("\")
            '** obtiene la longitud del nombre
            longitud = s.Length - pos
            path_PDF = s.Substring(0, s.Length - longitud + 1) & "extracto_pdf"
            archivos = IO.Directory.GetFiles(path_PDF)
            For Each archivo In archivos
                nombrearchivo = IO.Path.GetFileName(archivo)
                FileSystemHelper.BorrarArchivo(archivo)
            Next
        Catch ex As Exception

        End Try

    End Sub

End Class
