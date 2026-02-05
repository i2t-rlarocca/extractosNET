Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.SqlClient

Public Class SeguridadDAL

    Public Function getFuncionesRol(ByVal rol As Integer, ByRef msgErr As String) As ArrayList
        Dim lage As New ArrayList

        Dim connStr As String = "definir conn string " 'ConfigurationManager.ConnectionStrings("cn").ConnectionString
        Dim cn As New SqlConnection(connStr)
        Dim cmd As New SqlCommand
        cmd.Connection = cn
        Try
            cmd.CommandTimeout = 1800 'ConfigurationManager.AppSettings("CommandTimeout")
        Catch ex As Exception
            cmd.CommandTimeout = 1800
        End Try
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "uspGetFuncionesRol"
        cn.Open()

        cmd.Parameters.Clear()

        '@retorno
        cmd.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
        cmd.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

        '@id_rol
        cmd.Parameters.Add(New SqlParameter("@id_rol", SqlDbType.VarChar, 20))
        cmd.Parameters("@id_rol").Direction = ParameterDirection.Input
        cmd.Parameters("@id_rol").Value = rol

        '@msg_error
        cmd.Parameters.Add(New SqlParameter("@msg_error", SqlDbType.VarChar, 1024))
        cmd.Parameters("@msg_error").Direction = ParameterDirection.Output
        Try
            Dim dre As SqlDataReader
            dre = cmd.ExecuteReader()
            While dre.Read
                lage.Add(dre(0))
            End While
            dre = Nothing
            msgErr = cmd.Parameters("@msg_error").Value.ToString.Trim
        Catch ex As Exception
            msgErr = "Problema al recuperar funciones del rol " & rol.ToString & ". " & ex.Message
        Finally
            Try
                cn.Close()
            Catch ex As Exception
            End Try
        End Try
        Return lage

    End Function
    Public Function getAgenciasDelUsuario(ByVal idUsuario As String, ByRef msgErr As String) As ArrayList
        Dim lage As New ArrayList

        Dim connStr As String = "definir cnnn string" 'ConfigurationManager.ConnectionStrings("cn").ConnectionString
        Dim cn As New SqlConnection(connStr)
        Dim cmd As New SqlCommand
        cmd.Connection = cn
        Try
            cmd.CommandTimeout = 1800 'ConfigurationManager.AppSettings("CommandTimeout")
        Catch ex As Exception
            cmd.CommandTimeout = 1800
        End Try
        cmd.CommandType = CommandType.StoredProcedure
        cmd.CommandText = "uspGetAgenciasDelUsuario"
        cn.Open()

        cmd.Parameters.Clear()

        '@retorno
        cmd.Parameters.Add(New SqlParameter("@retorno", SqlDbType.Int))
        cmd.Parameters("@retorno").Direction = ParameterDirection.ReturnValue

        '@id_usuario
        cmd.Parameters.Add(New SqlParameter("@id_usuario", SqlDbType.VarChar, 20))
        cmd.Parameters("@id_usuario").Direction = ParameterDirection.Input
        cmd.Parameters("@id_usuario").Value = idUsuario

        '@msg_error
        cmd.Parameters.Add(New SqlParameter("@msg_error", SqlDbType.VarChar, 1024))
        cmd.Parameters("@msg_error").Direction = ParameterDirection.Output
        Try
            Dim dre As SqlDataReader
            dre = cmd.ExecuteReader()
            While dre.Read
                lage.Add(dre(0))
            End While
            dre = Nothing
            msgErr = cmd.Parameters("@msg_error").Value.ToString.Trim
        Catch ex As Exception
            msgErr = "Problema al recuperar agencias del usuario: " & ex.Message
        Finally
            Try
                cn.Close()
            Catch ex As Exception
            End Try
        End Try
        Return lage
    End Function

End Class
