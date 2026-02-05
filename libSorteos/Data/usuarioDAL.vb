Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports Sorteos.Bussiness
Imports libEntities.Entities
Imports Sorteos.Helpers
Imports Sorteos.Extractos


Namespace Data
    Public Class usuarioDAL
        Public Function getUsuario(ByVal idusuario As Long) As cUsuario
            Dim o As cUsuario
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM usuarios where idusuario= " & idusuario
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New cUsuario
                    Load(o, r)
                Next
                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception("getUsuario:" & ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function getUsuario(ByVal Usuario As String, ByVal pwd As String) As cUsuario
            Dim o As cUsuario
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM usuarios where upper(usuario)= '" & Usuario.ToUpper & "' and pwd='" & pwd & "'"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New cUsuario
                    Load(o, r)
                Next
                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception("getUsuario:" & ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function Load(ByRef o As cUsuario, _
                                  ByRef dr As DataRow) As Boolean

            Try
                Dim oC As New usuarioDAL
                o.idUsuario = dr("idusuario")
                o.Usuario = dr("usuario")
                o.PWD = dr("pwd")
                o.NombreUsuario = dr("nombreusuario")
                o.LoginHabilitado = dr("LoginHabilitado")
                o.PozoEstimadoPFHabilitado = dr("PozoEstimadoPFHabilitado")
                o.PozoEstimadoQ6yBRHabilitado = dr("PozoEstimadoQ6yBRHabilitado")
                o.ReversionHabilitada = dr("ReversionHabilitada")
                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function setLogAnulacion(ByVal pidPgmconcurso As Long, ByVal pidpgmsorteo As Long, ByVal idusuario As Long, ByVal usuario As String, ByVal motivo As String) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                setLogAnulacion = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " INSERT INTO logRevertirconcurso (fechahora,idpgmconcurso,idpgmsorteo,idusuario,autorizante,motivo) values (@fecha," & pidPgmconcurso & "," & pidpgmsorteo & "," & idusuario & ",'" & usuario & "','" & motivo & "')"
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@fecha", Now.ToString("dd/MM/yyyy HH:mm:ss"))
                cm.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception("setLogAnulacion:" & ex.Message)
                Return False
            End Try
        End Function

        Public Function getUsuarioID(ByVal usuario As String) As Long
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim vsql As String
            Dim ID As Long = 0
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  coalesce(idusuario,0)as idusuario FROM usuarios where upper(usuario) like '" & usuario.ToUpper & "' "
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    ID = dr("idusuario")
                End If
                dr.Close()
                Return ID
            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception("getUsuarioID:" & ex.Message)
                Return Nothing
            End Try
        End Function
        'hugo 17/06/13
        Public Function ActualizaUltimoAcceso(ByVal idUsuario As Long) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                ActualizaUltimoAcceso = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " UPDATE usuarios SET ultimoacceso=@fecha where idusuario=" & idUsuario
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.Parameters.AddWithValue("@fecha", Now.ToString("dd/MM/yyyy HH:mm:ss"))
                cm.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception("ActualizaUltimoAcceso:" & ex.Message)
                Return False
            End Try
        End Function

        Public Function ActualizaContrasenia(ByVal idUsuario As Long, ByVal pwdN As String) As Boolean
            Dim cm As SqlCommand = New SqlCommand
            Dim vsql As String
            Try
                ActualizaContrasenia = False
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " UPDATE usuarios SET pwd = '" & pwdN & "', ultimoCambioPWD = getdate() where idusuario=" & idUsuario
                cm.CommandText = vsql
                cm.Parameters.Clear()
                cm.ExecuteNonQuery()
                Return True
            Catch ex As Exception
                Throw New Exception("ActualizaContrasenia:" & ex.Message)
                Return False
            End Try
        End Function
    End Class
End Namespace