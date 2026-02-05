Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Bussiness

    Public Class AutoridadBO

        Public Function getAutoridad() As ListaOrdenada(Of Autoridad)
            Try
                Dim listaAut As ListaOrdenada(Of Autoridad)
                Dim oDal As New Data.AutoridadDAL

                listaAut = oDal.getAutoridad()
                Return listaAut

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getAutoridad(ByVal idJuego As Integer) As ListaOrdenada(Of Autoridad)
            Try
                Dim listaAut As ListaOrdenada(Of Autoridad)
                Dim oDal As New Data.AutoridadDAL

                listaAut = oDal.getAutoridad(idJuego)
                Return listaAut

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getAutoridad(ByVal apeNom As String, ByVal idJuego As Integer) As ListaOrdenada(Of Autoridad)
            Try
                Dim listaAut As ListaOrdenada(Of Autoridad)
                Dim oDal As New Data.AutoridadDAL

                listaAut = oDal.getAutoridad(apeNom, idJuego)
                Return listaAut

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getAutoridadDT(ByVal idPgmSorteo As String) As DataTable
            Try
                Dim oDal As New Data.AutoridadDAL

                Return oDal.getAutoridadDT(idPgmSorteo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function valida(ByVal oAutoridad As Autoridad, ByRef msgRet As String) As Boolean
            Dim lst As New ListaOrdenada(Of Autoridad)

            msgRet = ""
            If oAutoridad.idJuego = 0 Then msgRet &= "Falta indicar el juego." & vbCrLf
            If Trim(oAutoridad.Nombre) = "" Then msgRet &= "Falta indicar el nombre." & vbCrLf
            If Trim(oAutoridad.cargo) = "" Then msgRet &= "Falta indicar el cargo." & vbCrLf
            If oAutoridad.Orden = 0 Then msgRet &= "Falta indicar el orden." & vbCrLf
            If oAutoridad.Orden < 1 Or oAutoridad.Orden > 5 Then msgRet &= "Sólo se permiten nros. de orden entre 1 y 5." & vbCrLf

            '' valida que no exista una autoridad con ese mismo orden para el juego
            'lst = Me.getAutoridad(oAutoridad.idJuego)
            'For Each aut In lst
            '    If aut.Orden = oAutoridad.Orden Then
            '        msgRet &= "Ya existe una autoridad para ese orden."
            '    End If
            'Next

            If msgRet = "" Then
                Return True
            Else
                Return False
            End If
        End Function

        Public Function setAutoridad(ByVal oAutoridad As Autoridad) As Boolean
            Try
                Dim oDal As New Data.AutoridadDAL

                Return oDal.setAutoridad(oAutoridad)
                
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function eliminarAutoridad(ByVal oAutoridad As Autoridad) As Boolean
            Try
                Dim oDal As New Data.AutoridadDAL

                Return oDal.eliminarAutoridad(oAutoridad)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function setAutoridad_PgmSorteo(ByVal oAutoridad As Autoridad) As Boolean
            Try
                Dim oDal As New Data.AutoridadDAL

                Return oDal.setAutoridad_PgmSorteo(oAutoridad)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function eliminarAutoridad_PgmSorteo(ByVal oAutoridad As Autoridad, ByVal idPgmSorteo As Integer) As Boolean
            Try
                Dim oDal As New Data.AutoridadDAL

                Return oDal.eliminarAutoridad_PgmSorteo(oAutoridad, idPgmSorteo)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function getAutoridad_PgmSorteo(ByVal idPgmSorteo As Integer) As ListaOrdenada(Of Autoridad)
            Try
                Dim listaAut As ListaOrdenada(Of Autoridad)
                Dim oDal As New Data.AutoridadDAL

                listaAut = oDal.getAutoridad_PgmSorteo(idPgmSorteo)
                Return listaAut

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function AplicarAutoridadAlResto(ByVal idPgmSorteo As Integer, ByVal OPC As PgmConcurso) As Boolean
            Try
                Dim oDal As New Data.AutoridadDAL
                Return oDal.AplicarAutoridadAlResto(idPgmSorteo, OPC)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

    End Class
End Namespace