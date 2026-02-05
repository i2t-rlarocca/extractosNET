Imports libEntities.Entities
Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports Sorteos.Helpers

Namespace Data
    Public Class CargoDAL

        Public Function GetCargos() As ListaOrdenada(Of Cargo)
            Dim o As Cargo
            Dim lista As New ListaOrdenada(Of Cargo)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM cargo where coalesce(habilitado,0) = 1 order by idcargo"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Cargo
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
        Public Function Load(ByRef o As Cargo, _
                                   ByRef dr As DataRow, _
                                   ByVal recuperarObjComponentes As Boolean) As Boolean

            Try


                o.idCargo = dr("idcargo")
                o.Cargo = dr("cargo")
                o.Orden = dr("orden")
                o.Habilitado = dr("habilitado")
                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace