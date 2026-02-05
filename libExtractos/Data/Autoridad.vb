Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports Sorteos.Helpers
Imports Sorteos.Helpers.General
Imports System.Data.SqlClient


Namespace ExtractoData

    Public Class Autoridad

        Public Shared Function GetAutoridades(ByVal idJuego As String, ByVal idLoteria As String, Optional ByVal paraboldt As Boolean = False, Optional ByVal pidpgmsorteo As String = "") As List(Of ExtractoEntities.Autoridad)

            Dim l As List(Of ExtractoEntities.Autoridad) = New List(Of ExtractoEntities.Autoridad)
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim _where As String = ""
            '04/04/2017 hg SE AGREGA EL FILTRODEL PGMSORTEO PARA OBTENER LAS AUTORIDADES DEL SORTEO
            If pidpgmsorteo.Trim <> "" Then
                _where = " AND b.idpgmsorteo=" & pidpgmsorteo.Trim
            End If
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                If idLoteria = "S" Then
                    cm.CommandText = "Select * From autoridadextracto Where idjuego = @idJuego And loteria = 'S'"

                    cm.Parameters.AddWithValue("@idJuego", idJuego)
                    cm.Parameters.AddWithValue("@idLoteria", idLoteria)
                Else
                    If idLoteria = "E" Then
                        cm.CommandText = "Select	Autoridad1_Cargo = (" & _
                                        "select top 1 a.cargo " & _
                                        "				from autoridad a  " & _
                                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _where & _
                                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                        "                        where(ps.idjuego = " & idJuego & ") " & _
                                        "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
                                        "		Autoridad1_Nombre = ( " & _
                                        "				select top 1 a.nombre " & _
                                        "				from autoridad a  " & _
                                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _where & _
                                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                        "				where ps.idjuego = " & idJuego & "  " & _
                                        "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
                                        "		Autoridad1_Firma = ( " & _
                                        "				select top 1 a.firma " & _
                                        "				from autoridad a  " & _
                                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _where & _
                                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                        "				where ps.idjuego = " & idJuego & "  " & _
                                        "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
                                        "		Autoridad2_Cargo = ( " & _
                                        "				select top 1 a.cargo " & _
                                        "				from autoridad a  " & _
                                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _where & _
                                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                        "				where ps.idjuego = " & idJuego & "  " & _
                                        "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
                                        "		Autoridad2_Nombre = ( " & _
                                        "				select top 1 a.nombre " & _
                                        "				from autoridad a  " & _
                                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _where & _
                                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                        "				where ps.idjuego = " & idJuego & "  " & _
                                        "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
                                        "		Autoridad2_Firma = ( " & _
                                        "				select top 1 a.firma " & _
                                        "				from autoridad a  " & _
                                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _where & _
                                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                        "				where ps.idjuego = " & idJuego & "  " & _
                                        "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
                                        "        Autoridad3_Cargo = '', " & _
                                        "        Autoridad3_Nombre = '', " & _
                                        "        Autoridad3_Firma = 'sin_firma.PNG', " & _
                                        "        Autoridad4_Cargo = '', " & _
                                        "        Autoridad4_Nombre = '', " & _
                                        "        Autoridad4_Firma = 'sin_firma.PNG', " & _
                                        "        Autoridad5_Cargo = '', " & _
                                        "        Autoridad5_Nombre = '', " & _
                                        "        Autoridad5_Firma = 'sin_firma.PNG' "

                    Else
                        Throw New Exception("Error al obtener Autoridades: Lotería no soportada.")
                        Return Nothing
                    End If
                End If

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    If Not paraboldt Then 'para boldt se obtienen a partir de la 2 autoridad
                        Dim a1 As New ExtractoEntities.Autoridad
                        a1.Cargo = Es_Nulo(Of String)(r("Autoridad1_Cargo"), "")
                        a1.Nombre = Es_Nulo(Of String)(r("Autoridad1_Nombre"), "")
                        a1.Firma = Es_Nulo(Of String)(r("Autoridad1_Firma"), "")
                        l.Add(a1)
                    End If

                    Dim a2 As New ExtractoEntities.Autoridad
                    a2.Cargo = Es_Nulo(Of String)(r("Autoridad2_Cargo"), "")
                    a2.Nombre = Es_Nulo(Of String)(r("Autoridad2_Nombre"), "")
                    a2.Firma = Es_Nulo(Of String)(r("Autoridad2_Firma"), "")
                    l.Add(a2)

                    Dim a3 As New ExtractoEntities.Autoridad
                    a3.Cargo = Es_Nulo(Of String)(r("Autoridad3_Cargo"), "")
                    a3.Nombre = Es_Nulo(Of String)(r("Autoridad3_Nombre"), "")
                    a3.Firma = Es_Nulo(Of String)(r("Autoridad3_Firma"), "")
                    l.Add(a3)

                    Dim a4 As New ExtractoEntities.Autoridad
                    a4.Cargo = Es_Nulo(Of String)(r("Autoridad4_Cargo"), "")
                    a4.Nombre = Es_Nulo(Of String)(r("Autoridad4_Nombre"), "")
                    a4.Firma = Es_Nulo(Of String)(r("Autoridad4_Firma"), "")
                    l.Add(a4)

                    Dim a5 As New ExtractoEntities.Autoridad
                    a5.Cargo = Es_Nulo(Of String)(r("Autoridad5_Cargo"), "")
                    a5.Nombre = Es_Nulo(Of String)(r("Autoridad5_Nombre"), "")
                    a5.Firma = Es_Nulo(Of String)(r("Autoridad5_Firma"), "")
                    l.Add(a5)
                Next

                Return l

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetAutoridadesDT(ByVal idJuego As String, _
                                             ByVal idLoteria As String) As DataTable


            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From autoridadextracto Where jue_id = @idJuego And ext_loteria = @idLoteria"

                cm.Parameters.AddWithValue("@idJuego", idJuego)
                cm.Parameters.AddWithValue("@idLoteria", idLoteria)
                dr = cm.ExecuteReader()
                dt.Load(dr)

                Return dt
            Catch ex As Exception
                dt = Nothing
                Throw New Exception(ex.Message)
            End Try


        End Function
    End Class
End Namespace