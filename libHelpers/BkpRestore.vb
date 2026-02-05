Imports System.Data.SqlClient
Imports Sorteos.Helpers.General

Public Class BkpRestore

    Public Function backupDB(ByVal cnStr As String, ByVal pathBkp As String, Optional ByRef bd As String = "")
        Dim cn As SqlConnection = New SqlConnection()
        cn.ConnectionString = cnStr
        Dim cm As SqlCommand = New SqlCommand
        Dim vsql As String
        If Not pathBkp.EndsWith("\") Then pathBkp = pathBkp & "\"
        Try
            cm.Connection = cn
            cm.Connection.Open()
            cm.CommandType = CommandType.Text
            bd = cm.Connection.Database
            vsql = "backup database " & cn.Database & " to disk = '" & pathBkp & cn.Database & ".bak' with init, nounload, name = 'Copia de seguridad " & cn.Database & "', skip, stats = 10, format, medianame = 'NomConjMed', mediadescription = 'DescConMed'"
            FileSystemHelper.Log(Now.ToLongDateString & " - restoreDB: Comando RESTORE -> " & vsql & "<-")
            cm.CommandText = vsql
            cm.ExecuteNonQuery()
            Try
                cm.Connection.Close()
            Catch ex2 As Exception
            End Try

            Return True

        Catch ex As Exception
            Try
                cm.Connection.Close()
            Catch ex2 As Exception
            End Try

            Throw New Exception(ex.Message)
            Return False
        End Try

    End Function

    Public Function restoreDB(ByVal bd As String, ByVal cnStr As String, ByVal pathBkp As String, ByVal pathBD As String)
        Dim desde As Integer = -1
        Dim hasta As Integer = -1
        Dim bd_rem As String = ""
        Return True ' No era necesario hacer restores cruzados. Solo se hace backup. 
        ' Aparte habia mucho problema con los permisos a los directorios y fue imposible implementar el cruce
        Dim cn2 As SqlConnection = New SqlConnection()

        desde = cnStr.IndexOf("Initial Catalog=") + 16
        hasta = cnStr.IndexOf(";User ID=") - 1
        bd_rem = cnStr.Substring(desde, cnStr.IndexOf(";User ID=") - desde)
        cn2.ConnectionString = cnStr.Replace(bd_rem, "master")

        Dim cm As SqlCommand = New SqlCommand
        Dim vsql As String

        If Not pathBkp.EndsWith("\") Then pathBkp = pathBkp & "\"
        If Not pathBD.EndsWith("\") Then pathBD = pathBD & "\"
        Try
            cm.Connection = cn2
            cm.Connection.Open()

            cm.CommandType = CommandType.Text

            vsql = "USE MASTER; " & _
                   " RESTORE DATABASE " & bd_rem & " " & _
                   " FROM  DISK = N'" & pathBkp & bd & ".bak' " & _
                   " WITH REPLACE, FILE = 1, " & _
                   " MOVE N'dev_sorteosCas_Data' TO N'" & pathBD & bd_rem & "_Data.mdf',  " & _
                   " MOVE N'dev_sorteosCas_Log' TO N'" & pathBD & bd_rem & "_Log.ldf',  " & _
                   " NOUNLOAD,  STATS = 10; "
            FileSystemHelper.Log(Now.ToLongDateString & " - restoreDB: Comando RESTORE -> " & vsql & "<-")
            cm.CommandText = vsql
            cm.ExecuteNonQuery()
            Try
                cm.Connection.Close()
            Catch ex2 As Exception
            End Try

            Return True

        Catch ex As Exception
            Try
                cm.Connection.Close()
            Catch ex2 As Exception
            End Try

            Throw New Exception(ex.Message)
            Return False
        End Try
        Return True
    End Function

End Class
