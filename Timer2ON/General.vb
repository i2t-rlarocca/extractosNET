Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Configuration
Public Class General
    Private Shared gConn As SqlConnection
    Private Shared gConnD As SqlConnection
    Private Shared _connStr As String = ""
    Private Shared _pathIni As String = ""
    Public Shared Property ConnString() As String
        Get
            If _connStr = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "\sorteosCAS.ini"
                If Not System.IO.File.Exists(archivo) Then
                    _connStr = "Data Source=sqlsrv02;Initial Catalog=dev_displaycas;User ID=data;Password=cpc;Connect Timeout=120;"
                Else
                    ' Leer el fichero usando la codificacin de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader(archivo, System.Text.Encoding.Default, True)
                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una lena del fichero
                        linea = sr.ReadLine()

                        ' Si no est vaca, aadirla al control
                        ' Si est vaca, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 16) = "ConnectionString" Then
                            strCnStr = linea.Substring(18, linea.Length - 19)

                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                _connStr = strCnStr

            End If
            Return _connStr
        End Get
        Set(ByVal value As String)
            _connStr = value

        End Set
    End Property

    Public Shared Function Iniciar_Conexion() As String
        Try
            If gConn Is Nothing Then
                gConn = New SqlConnection
                gConn.ConnectionString = ConnString

                'ConfigurationManager.ConnectionStrings("cnE").ConnectionString
                gConn.Open()

            Else
                If gConn.State = Data.ConnectionState.Closed Then
                    gConn.ConnectionString = ConnString

                    'ConfigurationManager.ConnectionStrings("cnE").ConnectionString
                    gConn.Open()

                End If
            End If
            Return ""
        Catch ex As SqlException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.ToString
        End Try
    End Function

    Public Shared Function Obtener_Conexion() As SqlConnection
        Try
            Dim mensaje As String = Iniciar_Conexion()
            If mensaje = "" Then
                Return gConn
            Else
                Throw New Exception(mensaje)
            End If
        Catch ex As Exception
            Throw New Exception("No se puede iniciar la conexin: " & ex.Message)
        End Try
    End Function
    Public Shared Property PathIni() As String
        Get
            Return _pathIni
        End Get
        Set(ByVal value As String)
            _pathIni = value
        End Set
    End Property
    Public Shared Sub Log(ByVal texto As String)
        'creamos el nombre del archivo
        Dim archivo = PathIni & "\Timer2ON.log"
        'Dim archivo = "I:\DATOS\DEMOEXTRACTOS\Logs\pruebas.txt"
        'conectamos con el FSO
        Dim confile = CreateObject("scripting.filesystemobject")
        'creamos el objeto TextStream
        Dim fich = confile.OpenTextFile(archivo, 8, True)
        'escribimos los nmeros del 0 al 9
        fich.writeLine(texto)

        'cerramos el fichero
        fich.close()

    End Sub
End Class
