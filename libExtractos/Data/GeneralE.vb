Imports Microsoft.VisualBasic
Imports System.Data.OleDb
Imports System.Configuration

Public Module GeneralE

    Private gConn As OleDbConnection
    Private gConnD As OleDbConnection
    Private _connStr As String = ""
    Private _pathIni As String = ""

    Public Property PathIni() As String
        Get
            Return _pathIni
        End Get
        Set(ByVal value As String)
            _pathIni = value
        End Set
    End Property

    Public Property ConnString() As String
        Get
            If _connStr.Trim = "" Then
                _connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=\\Mstr-02\datos\extractos\quiniela_web.mdb;Persist Security Info=True"
            End If
            Return _connStr
        End Get
        Set(ByVal value As String)
            _connStr = value
        End Set
    End Property

    Public Function Iniciar_Conexion() As String
        Try
            If gConn Is Nothing Then
                gConn = New OleDbConnection
                gConn.ConnectionString = ConnString 'ConfigurationManager.ConnectionStrings("cnE").ConnectionString
                gConn.Open()
                'Setear_Charset_Conexion(gConn, "utf8", "")
            Else
                If gConn.State = Data.ConnectionState.Closed Then
                    gConn.ConnectionString = ConnString 'ConfigurationManager.ConnectionStrings("cnE").ConnectionString
                    gConn.Open()
                    'Setear_Charset_Conexion(gConn, "utf8", "")
                End If
            End If

            Return ""

        Catch ex As OleDbException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.ToString
        End Try
    End Function

    Public Function Obtener_Conexion() As OleDbConnection
        Try
            Dim mensaje As String = Iniciar_Conexion()
            If mensaje = "" Then
                Return gConn
            Else
                Throw New Exception(mensaje)
            End If
        Catch ex As Exception
            Throw New Exception("No se puede iniciar la conexión: " & ex.Message)
        End Try
    End Function

    Public Function Cerrar_Conexion() As String
        Try
            If Not gConn Is Nothing Then
                If gConn.State = Data.ConnectionState.Open Then
                    gConn.Close()
                End If
            End If
            Return ""
        Catch ex As OleDbException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.Message
        End Try

    End Function

    Private Function Setear_Charset_Conexion(ByRef conn As OleDbConnection, ByVal pCharset As String, ByVal pCollate As String) As String
        Try
            Dim vsql As String = ""
            vsql = "SET NAMES " & pCharset & ""

            If pCollate <> "" Then
                vsql = vsql & " COLLATE '" & pCollate & "'"
            End If

            Dim command As OleDbCommand = conn.CreateCommand
            command.Connection = conn
            command.CommandText = vsql
            command.ExecuteNonQuery()

            Return ""

        Catch ex As OleDbException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.Message
        End Try

    End Function


    Public Function Iniciar_Conexion_Display() As String
        Try
            If gConnD Is Nothing Then
                gConnD = New OleDbConnection
                gConnD.ConnectionString = "definir conexion" 'ConfigurationManager.ConnectionStrings("cnD").ConnectionString
                gConnD.Open()
            Else
                If gConnD.State = Data.ConnectionState.Closed Then
                    gConnD.ConnectionString = "definir conexion" 'ConfigurationManager.ConnectionStrings("cnD").ConnectionString
                    gConnD.Open()
                End If
            End If

            Return ""

        Catch ex As OleDbException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.ToString
        End Try
    End Function

    Public Function Obtener_Conexion_Display() As OleDbConnection
        Try
            Dim mensaje As String = Iniciar_Conexion_Display()
            If mensaje = "" Then
                Return gConnD
            Else
                Throw New Exception(mensaje)
            End If
        Catch ex As Exception
            Throw New Exception("No se puede iniciar la conexión con el servidor del Display: " & ex.Message)
        End Try
    End Function

    Public Function Cerrar_Conexion_Display() As String
        Try
            If Not gConnD Is Nothing Then
                If gConnD.State = Data.ConnectionState.Open Then
                    gConnD.Close()
                End If
            End If
            Return ""
        Catch ex As OleDbException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.Message
        End Try

    End Function

    Public Function Clonar_Conexion() As OleDbConnection
        Dim conn As OleDbConnection = New OleDbConnection
        conn.ConnectionString = ConnString 'ConfigurationManager.ConnectionStrings("db").ConnectionString
        conn.Open()
        Return conn
    End Function


    Public Function Es_Nulo(Of T)(ByVal pNulo As Object, ByVal pValorDevuelto As T) As T
        If DBNull.Value.Equals(pNulo) Or pNulo Is Nothing Then
            Return pValorDevuelto
        Else
            'If TypeOf pNulo Is MySql.Data.Types.MySqlDateTime Then
            'If pNulo.year > 0 Then
            'Return CType(pNulo.value, T)
            'Else
            'Return CType(Nothing, T)
            'End If
            'Else
            Return CType(pNulo, T)
            'End If
        End If
    End Function



End Module