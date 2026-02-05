Imports libEntities.Entities
Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General

Imports Sorteos.Helpers

Namespace Data

    Public Class LoteriaDAL
        Public Function getLoterias() As ListaOrdenada(Of Loteria)
            Dim ls As New ListaOrdenada(Of Loteria)
            Dim o As New Loteria
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT * " _
                       & " FROM " _
                       & " loteria where habilitada=1 order by orden_extracto_qnl "
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Loteria
                    Load(o, r, True)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getLoteria(ByVal pidLoteria As Char) As Loteria
            Dim o As New Loteria
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT * " _
                       & " FROM " _
                       & " loteria where idloteria='" & pidLoteria & "'"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Loteria
                    Load(o, r, True)
                Next

                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function Load(ByRef o As Loteria, _
                                    ByRef dr As DataRow, _
                                    ByVal recuperarObjComponentes As Boolean) As Boolean

            Try
                o.IdLoteria = dr("idLoteria")
                o.Cifras = Es_Nulo(Of Integer)(dr("Cifras"), 0)
                o.Nombre = dr("nombre")
                o.nroSorteoObligatorio = dr("nroSorteoObligatorio")
                o.orden_extracto_qnl = dr("orden_extracto_qnl")
                o.path_extracto = Es_Nulo(Of String)(dr("path_extracto"), "")
                o.Prv_Id = Es_Nulo(Of Char)(dr("Prv_Id"), "")
                o.letras_extracto_qnl = Es_Nulo(Of Boolean)(dr("letras_extracto_qnl"), False)
                o.long_letras_extracto = Es_Nulo(Of Integer)(dr("long_letras_extracto_qnl"), 0)
                o.cant_letras_extracto = Es_Nulo(Of Integer)(dr("cant_letras_extracto_qnl"), 0)
                'al principio son iguales
                o.CifrasIngresadaDesdeForm = Es_Nulo(Of Integer)(dr("Cifras"), 0)
                o.IdLoteriaBoldt = Es_Nulo(Of Integer)(dr("idLoteriaBoldt"), 0)
                o.IdLoteriaDisplay = Es_Nulo(Of Integer)(dr("idLoteriaDisplay"), 0)
                o.Clave = Es_Nulo(Of String)(dr("clave"), "")
                o.Extension_arch_Extracto = Es_Nulo(Of String)(dr("extension_arch_extracto"), "")
                o.Fmt_arch_Extracto = Es_Nulo(Of Integer)(dr("fmt_arch_extracto"), 0)
                o.Metodo_Habitual = Es_Nulo(Of Integer)(dr("metodo_habitual"), 1)

                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace