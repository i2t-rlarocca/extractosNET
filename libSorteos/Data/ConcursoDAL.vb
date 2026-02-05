Imports libEntities.Entities
Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers
Imports Sorteos.Helpers.General

Namespace Data

    Public Class ConcursoDAL

        Public Function getConcurso(ByVal idConcurso As Integer) As Concurso
            Dim oC As New Concurso
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM concurso where idconcurso= " & idConcurso
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Load(oC, r)
                Next

                Return oC

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function

        Public Function Load(ByRef o As Concurso, _
                           ByRef dr As DataRow, _
                           Optional ByVal recuperarObjComponentes As Boolean = True) As Boolean

            Try
                Dim oMEDal As New ModeloExtraccionesDAL
                Dim oJue As ConcursoJuego
                o.IdConcurso = dr("idConcurso")
                o.Nombre = dr("nombre")
                o.Descripcion = dr("descripcion")
                o.ModeloExtracciones = oMEDal.getModeloExtracciones(dr("idmodeloextracciones"))
                o.TieneJuegoDependiente = dr("tieneJuegoDependiente")
                o.Juegos = getConcursoJuegos(dr("idConcurso"))
                o.LstParConEspacioGan = Es_Nulo(Of Boolean)(dr("lstParConEspacioGan"), 0)
                o.LstRepDetParam = Es_Nulo(Of Boolean)(dr("lstRepDetParam"), 0)
                For Each oJue In o.Juegos
                    If oJue.esPrincipal = True Then
                        o.JuegoPrincipal = oJue
                        Exit For
                    End If
                Next
                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Function getConcursoJuegos(ByVal idConcurso As Integer) As ListaOrdenada(Of ConcursoJuego)
            Dim ls As New ListaOrdenada(Of ConcursoJuego)
            Dim o As New ConcursoJuego

            Dim oJ As New Juego

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text


                vsql = " SELECT * FROM concursojuego where idconcurso=" & idConcurso & " order by orden"
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New ConcursoJuego
                    LoadConcursoJuegos(o, r)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                ls = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function

        Public Function LoadConcursoJuegos(ByRef o As ConcursoJuego, _
                                   ByRef dr As DataRow) As Boolean

            Try
                Dim oJ As New JuegoDAL

                o.cantCifras = dr("cantcifras")
                o.orden = dr("orden")
                o.Juego = oJ.getJuego(dr("idJuego"))
                o.idconcurso = dr("idconcurso")
                o.esPrincipal = dr("esPrincipal")

                LoadConcursoJuegos = True
            Catch ex As Exception
                LoadConcursoJuegos = False
                Throw New Exception(ex.Message)
            End Try
        End Function


    End Class


End Namespace