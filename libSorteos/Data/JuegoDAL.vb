Imports libEntities.Entities
Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports Sorteos.Helpers

Namespace Data

    Public Class JuegoDAL
        Public Function getJuego() As ListaOrdenada(Of Juego)
            Dim o As Juego
            Dim lista As New ListaOrdenada(Of Juego)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM juego where coalesce(jue_habi,'N') = 'S' order by jue_desc"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Juego
                    Load(o, r, True)
                    lista.Add(o)
                Next

                Return lista

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getJuego(ByVal idJuego As Integer) As Juego
            Dim o As Juego
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM juego where idjuego= " & idJuego
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Juego
                    Load(o, r, True)
                Next

                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function Load(ByRef o As Juego, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try
                Dim oC As New JuegoDAL
                o.IdJuego = dr("idjuego")
                o.Gjg_Id = dr("gjg_id")
                o.Jue_Desc = dr("Jue_Desc")
                o.Jue_HorLun = Es_Nulo(Of String)(dr("Jue_HorLun"), "00:00")
                o.Jue_HorMar = Es_Nulo(Of String)(dr("Jue_HorMar"), "00:00")
                o.Jue_HorMie = Es_Nulo(Of String)(dr("Jue_HorMie"), "00:00")
                o.Jue_HorJue = Es_Nulo(Of String)(dr("Jue_HorJue"), "00:00")
                o.Jue_HorVie = Es_Nulo(Of String)(dr("Jue_HorVie"), "00:00")
                o.Jue_HorSab = Es_Nulo(Of String)(dr("Jue_HorSab"), "00:00")
                o.Jue_HorDom = Es_Nulo(Of String)(dr("Jue_HorDom"), "00:00")
                o.Jue_DesNro = Es_Nulo(Of Integer)(dr("Jue_DesNro"), "0")
                o.Jue_HastNro = Es_Nulo(Of Integer)(dr("Jue_HasNro"), "0")
                o.Jue_Habi = Es_Nulo(Of String)(dr("Jue_Habi"), "")
                o.Id_Agr_Juego = Es_Nulo(Of Integer)(dr("Id_Agr_Juego"), "0")
                o.EnviarAMedios = Es_Nulo(Of String)(dr("EnviarAMedios"), "NO")
                o.EnviarAMediosConfParcial = Es_Nulo(Of String)(dr("EnviarAMediosConfParcial"), "NO")
                o.Jue_PathLocal = Es_Nulo(Of String)(dr("Jue_PathLocal"), "")
                o.EstimaPozosProxSorteo = Es_Nulo(Of Boolean)(dr("EstimaPozosProxSorteo"), False)
                o.RedondeoPozoProxSorteo = Es_Nulo(Of Long)(dr("RedondeoPozoProxSorteo"), "0")
                o.GeneraExtractoUnif = Es_Nulo(Of Boolean)(dr("GeneraExtractoUnif"), False)
                o.FtpExtractoUnif = Es_Nulo(Of Boolean)(dr("FtpExtractoUnif"), False)
                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function getJuegos(ByVal idJuegos As String) As ListaOrdenada(Of Juego)
            Dim o As Juego
            Dim lista As New ListaOrdenada(Of Juego)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM juego where idjuego in (" & idJuegos & ") order by jue_desc"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Juego
                    Load(o, r, True)
                    lista.Add(o)
                Next

                Return lista

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getJuegosAMedios() As ListaOrdenada(Of Juego)
            Dim o As Juego
            Dim lista As New ListaOrdenada(Of Juego)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM juego where coalesce(jue_habi,'N') = 'S' and (coalesce(enviarAMedios,'NO') <> 'NO' or coalesce(enviarAMediosConfParcial,'NO') <> 'NO') order by jue_desc"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Juego
                    Load(o, r, True)
                    lista.Add(o)
                Next

                Return lista

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        '**27/11/2012
        Public Function esQuiniela(ByVal idJuego As Long) As Boolean

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                esQuiniela = False

                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "select case when exists (SELECT  * FROM juego where id_agr_juego = 1 and coalesce(jue_habi,'N') = 'S' and idjuego = " & idJuego & ") then 'S' else 'N' end as esQuiniela"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)

                For Each r As DataRow In dt.Rows
                    If r("esQuiniela") = "S" Then
                        esQuiniela = True
                        Exit For
                    End If
                Next
                If Not dr.IsClosed Then dr.Close()
                Return esQuiniela

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return esQuiniela
            End Try

        End Function

    End Class
End Namespace