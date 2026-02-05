Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Bussiness
    Public Class UsuarioBO
        Public Function getUsuario(ByVal idusuario As Long) As cUsuario
            Try
                Dim o As New Data.usuarioDAL
                Return o.getUsuario(idusuario)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function getUsuario(ByVal usuario As String, ByVal pwd As String) As cUsuario
            Try
                Dim o As New Data.usuarioDAL
                Return o.getUsuario(usuario, pwd)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function setLogAnulacion(ByVal pidPgmconcurso As Long, ByVal pidpgmsorteo As Long, ByVal idusuario As Long, ByVal usuario As String, ByVal motivo As String) As Boolean
            Try
                Dim o As New Data.usuarioDAL
                Return o.setLogAnulacion(pidPgmconcurso, pidpgmsorteo, idusuario, usuario, motivo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function getUsuarioID(ByVal nombreusuario As String) As Long
            Try
                Dim o As New Data.usuarioDAL
                Return o.getUsuarioID(nombreusuario)
            Catch ex As Exception

            End Try
        End Function
        'hugo 17/06/13
        Public Function ActualizaUltimoAcceso(ByVal idUsuario As Long) As Boolean
            Try
                Dim o As New Data.usuarioDAL
                Return o.ActualizaUltimoAcceso(idUsuario)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function ActualizaContrasenia(ByVal idUsuario As Long, ByVal pwdN As String) As Boolean
            Try
                Dim o As New Data.usuarioDAL
                Return o.ActualizaContrasenia(idUsuario, pwdN)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace