Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class Juego

        Public Shared Function GetHoraSorteo(ByVal id As String) As Date

            Dim o As ExtractoEntities.Juego = New ExtractoEntities.Juego
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select jue_hora From juegos Where jue_id = @id"

                cm.Parameters.AddWithValue("@id", id)

                Return cm.ExecuteScalar

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetJuego(ByVal id As String) As ExtractoEntities.Juego

            Dim o As ExtractoEntities.Juego = New ExtractoEntities.Juego
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From juego Where idjuego = @id"

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

        Public Shared Function Load(ByRef o As ExtractoEntities.Juego, ByRef dr As DataRow) As Boolean
            Try
                o.Id = Es_Nulo(Of String)(dr("idjuego"), "")
                o.CantidadExtractos = 0 'Es_Nulo(Of Integer)(dr("jue_cant_ext"), 0)
                o.Modalidades = ModalidadJuego.GetModalidades(Es_Nulo(Of String)(dr("idjuego"), ""))
                o.Nombre = Es_Nulo(Of String)(dr("jue_desc"), "")
                Load = True
            Catch ex As Exception
                Load = False
            End Try
        End Function
    End Class
End Namespace