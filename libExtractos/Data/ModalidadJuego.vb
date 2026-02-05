Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class ModalidadJuego

        Public Shared Function GetModalidades(ByVal idJuego As String) As List(Of ExtractoEntities.ModalidadJuego)

            Dim l As List(Of ExtractoEntities.ModalidadJuego) = New List(Of ExtractoEntities.ModalidadJuego)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From valor_apuesta Where idjuego = " & idJuego

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Dim o As New ExtractoEntities.ModalidadJuego
                    Load(o, r)
                    l.Add(o)
                Next

                Return l

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetValorApuesta(ByVal idPgmSorteo As Long) As Double

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                'cm.CommandText = "Select vap_valapu From valor_apuesta_sorteo Where idpgmsorteo =  " & idPgmSorteo
                cm.CommandText = "select coalesce(vap.vap_valapu, coalesce(va.vap_valapu,0)) as valorapuesta  from valor_apuesta va left join valor_apuesta_sorteo vap on va.idvalorapuesta = vap.idvalorapuesta where idjuego=" & Math.Round((idPgmSorteo / 1000000), 0) & " and coalesce(vap.idpgmsorteo," & idPgmSorteo & ") = " & idPgmSorteo

                Return cm.ExecuteScalar

            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function Load(ByRef o As ExtractoEntities.ModalidadJuego, ByRef dr As DataRow) As Boolean
            Try
                o.Id = Es_Nulo(Of Integer)(dr("idmodalidad"), 0)
                o.Nombre = Es_Nulo(Of String)(dr("vap_desc"), "")
                o.ValorApuesta = Es_Nulo(Of Double)(dr("vap_valapu"), 0)

                Load = True
            Catch ex As Exception
                Load = False
            End Try
        End Function
    End Class
End Namespace