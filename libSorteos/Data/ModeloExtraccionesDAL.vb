Imports libEntities.Entities
Imports System.Data
Imports System.Data.SqlClient
Imports Sorteos.Helpers.General
Imports Sorteos.Helpers

Namespace Data

    Public Class ModeloExtraccionesDAL

        Public Function getModeloExtracciones(ByVal idModeloExtracciones As Integer) As ModeloExtracciones
            Dim oME As New ModeloExtracciones

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = " SELECT  * FROM ModeloExtracciones where idModeloExtracciones= " & idModeloExtracciones
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Load(oME, r)
                Next

                Return oME

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function

        Public Function Load(ByRef o As ModeloExtracciones, _
                                   ByRef dr As DataRow, _
                                   Optional ByVal recuperarObjComponentes As Boolean = True) As Boolean
            Try
                Dim oMEDet As New ListaOrdenada(Of ModeloExtraccionesDet)
                oMEDet = getModeloExtraccionesDET(dr("idmodeloextracciones"))

                o.idModeloExtracciones = dr("idModeloExtracciones")
                o.ModeloExtraccionesDet = oMEDet
                o.cantExtracciones = dr("cantExtracciones")
                o.Descripcion = dr("Descripcion")
                o.Nombre = dr("nombre")
                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getModeloExtraccionesDET(ByVal idModeloExtracciones As Integer) As ListaOrdenada(Of ModeloExtraccionesDet)
            Dim ls As New ListaOrdenada(Of ModeloExtraccionesDet)

            Dim o As New ModeloExtraccionesDet

            Dim oJ As New Juego

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text


                vsql = " SELECT * FROM modeloextracciones_v where idmodeloextracciones=" & idModeloExtracciones
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New ModeloExtraccionesDet
                    LoadModeloExtraccionesDet(o, r)
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

        Public Function LoadModeloExtraccionesDet(ByRef o As ModeloExtraccionesDet, _
                           ByRef dr As DataRow, _
                           Optional ByVal recuperarObjComponentes As Boolean = True) As Boolean
            Try
                Dim Ogeneral As New GeneralDAL

                o.idTipoExtraccion = dr("idTipoExtraccion")
                o.idModeloExtraccionesDet = dr("idModeloExtraccionesDet")
                o.Orden = dr("orden")
                '.... Recordar completar campos tanto de tipoExtraccion como ModeloExtraccionesDet por la herencia. Ver INHERITS en la entity ModeloExtraccionesDet
                o.Obligatoria = dr("esObligatoria")
                o.Titulo = dr("Titulo")
                '-- propiedades tipoextraccion
                o.Nombre = dr("tipoextraccion_nombre")
                o.descripcion = dr("descripcion")
                o.tipoTope = Ogeneral.getTipoTope(dr("idtipotope"))
                o.cantExtractos = Es_Nulo(Of Integer)(dr("cantextractos"), 0)
                o.cantExtractosPorColumna = dr("cantExtractosPorColumna")
                o.cantCifras = dr("cantCifras")
                o.metodoIngreso = Ogeneral.getMetodoIngreso(dr("idmetodoingreso"))
                o.tipoValorExtraido = Ogeneral.getTipoValorExtraido(dr("idtipovalorextraido"))
                o.sorteaPosicion = Es_Nulo(Of Boolean)(dr("sorteaPosicion"), False)
                o.valorMinimo = dr("valorMinimo")
                o.valorMaximo = dr("valorMaximo")
                o.criterioFinExtraccion = Ogeneral.getCriterioFinExtraccion(dr("idcriterioFinExtraccion"))
                o.ordenExtraccion = Ogeneral.getOrdeExtraccion(dr("idordenextraccion"))
                o.ordenEnExtracto = Ogeneral.getOrdenEnExtracto(dr("idordenEnExtracto"))
                o.AdmiteRepetidos = Es_Nulo(Of Boolean)(dr("admiterepetidos"), False)
                LoadModeloExtraccionesDet = True
            Catch ex As Exception
                LoadModeloExtraccionesDet = False
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function getModeloExtraccionDET(ByVal idModeloExtraccionDET As Integer) As ModeloExtraccionesDet


            Dim o As New ModeloExtraccionesDet



            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text


                vsql = " SELECT * FROM modeloextracciones_v where idmodeloextraccionesDET=" & idModeloExtraccionDET
                cm.CommandText = vsql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    LoadModeloExtraccionesDet(o, r)
                Next

                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()

                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function

    End Class

End Namespace