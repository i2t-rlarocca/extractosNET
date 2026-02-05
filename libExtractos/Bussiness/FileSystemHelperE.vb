Imports Microsoft.VisualBasic
Imports System.IO
Imports ICSharpCode.SharpZipLib.Checksums
Imports ICSharpCode.SharpZipLib.Zip
Imports ICSharpCode.SharpZipLib.GZip

Public Class FileSystemHelperE
    Private Shared _path_log As String = ""

    Public Shared Property pathLog() As String
        Get
            If _path_log = "" Then Return "c:"
            Return _path_log
        End Get
        Set(ByVal value As String)
            _path_log = value
        End Set
    End Property

    Public Shared Sub GeneraArchivoTXTDesdeArray(ByVal registros() As String, ByVal path As String, ByVal nombre As String, ByVal msgErr As String)
        'Variables para abrir el archivo en modo de escritura
        Dim strStreamW As Stream
        Dim strStreamWriter As StreamWriter
        Try
            ' Verifico que exista el directorio
            If Not CrearPath(path) Then
                msgErr = "No se pudo crear la ruta para el paquete."
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
            For Each r In registros
                'Escribimos la línea en el achivo de texto
                strStreamWriter.WriteLine(r)
            Next

            strStreamWriter.Close()

            msgErr = ""

        Catch ex As Exception
            strStreamWriter.Close()
            msgErr = ex.Message
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

    'Public Shared Sub ComprimirListaArchivos(ByVal fileNames() As String, _
    '              ByVal zipPath As String, _
    '              ByVal zipFic As String, _
    '              ByRef msgErr As String, _
    '              Optional ByVal crearAuto As Boolean = False)
    '    ' comprimir los ficheros del array en el zip indicado
    '    ' si crearAuto = True, zipfile sera el directorio en el que se guardara
    '    ' y se generara automaticamente el nombre con la fecha y hora actual
    '    Dim objCrc32 As New Crc32()
    '    Dim strmZipOutputStream As ZipOutputStream
    '    msgErr = ""
    '    '
    '    If zipFic = "" Then
    '        zipFic = "."
    '        crearAuto = True
    '    End If
    '    If crearAuto Then
    '        ' si hay que crear el nombre del fichero
    '        ' este sera el path indicado y la fecha actual
    '        zipFic &= "\ZIP" & DateTime.Now.ToString("yyMMddHHmmss") & ".zip"
    '    Else
    '        zipFic = zipPath & "\" & zipFic
    '    End If

    '    If Not CrearPath(zipPath) Then
    '        msgErr = "No se pudo crear la ruta para el paquete."
    '        Exit Sub
    '    End If
    '    strmZipOutputStream = New ZipOutputStream(File.Create(zipFic))
    '    ' Compression Level: 0-9
    '    ' 0: no(Compression)
    '    ' 9: maximum compression
    '    strmZipOutputStream.SetLevel(6)
    '    '
    '    Dim strFile As String
    '    For Each strFile In fileNames
    '        If Not System.IO.File.Exists(strFile) Then
    '            Continue For
    '        End If
    '        Dim strmFile As FileStream = File.OpenRead(strFile)
    '        Dim abyBuffer(Convert.ToInt32(strmFile.Length - 1)) As Byte
    '        '
    '        strmFile.Read(abyBuffer, 0, abyBuffer.Length)
    '        '
    '        '------------------------------------------------------------------
    '        ' para guardar sin el primer path
    '        'Dim sFile As String = strFile
    '        'Dim i As Integer = sFile.IndexOf("\")
    '        'If i > -1 Then
    '        '    sFile = sFile.Substring(i + 1).TrimStart
    '        'End If
    '        '------------------------------------------------------------------
    '        '
    '        '------------------------------------------------------------------
    '        ' para guardar solo el nombre del fichero
    '        ' esto solo se debe hacer si no se procesan directorios
    '        ' que puedan contener nombres repetidos
    '        'Dim sFile As String = Path.GetFileName(strFile)
    '        'Dim theEntry As ZipEntry = New ZipEntry(sFile)
    '        '------------------------------------------------------------------
    '        '
    '        ' se guarda con el path completo
    '        Dim theEntry As ZipEntry = New ZipEntry(strFile)
    '        '
    '        ' guardar la fecha y hora de la ultima modificacion
    '        Dim fi As New FileInfo(strFile)
    '        theEntry.DateTime = fi.LastWriteTime
    '        'theEntry.DateTime = DateTime.Now
    '        '
    '        theEntry.Size = strmFile.Length
    '        strmFile.Close()
    '        objCrc32.Reset()
    '        objCrc32.Update(abyBuffer)
    '        theEntry.Crc = objCrc32.Value
    '        strmZipOutputStream.PutNextEntry(theEntry)
    '        strmZipOutputStream.Write(abyBuffer, 0, abyBuffer.Length)
    '    Next
    '    strmZipOutputStream.Finish()
    '    strmZipOutputStream.Close()

    'End Sub

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
        'Dim archivo = Request.ServerVariables("APPL_PHYSICAL_PATH") & "Logs\pruebas.txt"
        Try
            'Dim archivo = "I:\FerozoWebHosting\portalcas.com.ar\public_html\demoextractos\" & "Logs\pruebas.txt"
            Dim archivo = pathLog & "\Logs\pruebas.txt"

            'Dim archivo = "d:\pruebas.txt"
            '******
            ''conectamos con el FSO
            'Dim confile = CreateObject("scripting.filesystemobject")

            ''creamos el objeto TextStream
            'Dim fich = confile.OpenTextFile(archivo, 8, True)

            ''escribimos los números del 0 al 9
            'fich.writeLine(texto)

            ''cerramos el fichero
            'fich.close()
            '******
            Dim path As String = archivo.Trim.Substring(0, archivo.Trim.LastIndexOf("\"))
            FileSystemHelperE.CrearPath(path)
            Dim f As StreamWriter = New StreamWriter(archivo, True)
            f.WriteLine(texto)
            f.Close()
        Catch ex As Exception

        End Try
    End Sub

    Public Shared Function cnvFechaAccess(ByVal fecha As String) As String

        'Dim aux1 = Split(fecha, "/")
        Dim aux1 = Split(Left(fecha, 10), "/")
        Dim _fecha As Integer = (((Left(aux1(2), 4) * 100) + aux1(1)) * 100) + aux1(0)
        Return _fecha

    End Function


End Class
