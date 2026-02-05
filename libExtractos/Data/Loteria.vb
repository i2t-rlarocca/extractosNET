Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class Loteria

        Public Shared Function GetLoteria(ByVal id As String) As ExtractoEntities.Loteria

            Dim o As ExtractoEntities.Loteria = New ExtractoEntities.Loteria
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From loteria Where idloteria = @id"

                cm.Parameters.AddWithValue("@id", id)

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                For Each r As DataRow In dt.Rows
                    Load(o, r)
                Next

                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function Load(ByRef o As ExtractoEntities.Loteria, ByRef dr As DataRow) As Boolean
            Try
                o.Id = Es_Nulo(Of String)(dr("idloteria"), "")
                o.Nombre = Es_Nulo(Of String)(dr("nombre"), "")
                o.Orden = dr("orden_extracto_qnl")
                Load = True
            Catch ex As Exception
                Load = False
            End Try
        End Function


        Public Shared Function GetLoterias() As List(Of ExtractoEntities.Loteria)

            Dim l As List(Of ExtractoEntities.Loteria) = New List(Of ExtractoEntities.Loteria)

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From loteria order by lot_orden "

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Dim o As New ExtractoEntities.Loteria
                    Load(o, r)
                    l.Add(o)
                Next

                Return l


            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Shared Function Get_Loterias() As DataTable

            Dim o As ExtractoEntities.Loteria = New ExtractoEntities.Loteria
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From loteria order by orden_extracto_qnl "

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()
                Return dt

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace