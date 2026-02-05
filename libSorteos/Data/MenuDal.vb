Imports System.Data.Sql
Imports System.Data.SqlClient
Imports Sorteos.Helpers
Imports Sorteos.Helpers.General
Imports libEntities.Entities

Public Class MenuDal

    ''' <summary>
    ''' Obtiene desde la base de datos la lista de todos los items de este menú
    ''' </summary>
    Public Function Obtener_Todos_Items() As ListaOrdenada(Of MenuItem)

        Dim lListaRetorno As ListaOrdenada(Of MenuItem) = New ListaOrdenada(Of MenuItem)
        Dim cmd As SqlCommand = New SqlCommand
        Dim drConsulta As SqlDataReader
        Try
            cmd.Connection = Obtener_Conexion()
            cmd.CommandText = "estructura_menu_items_s"
            cmd.CommandType = CommandType.StoredProcedure
            ' Ejecuto la consulta            
            drConsulta = cmd.ExecuteReader()
            'Recorro el resultado
            While drConsulta.Read()
                Dim oEntidad As New MenuItem
                LoadItem(oEntidad, drConsulta)
                lListaRetorno.Add(oEntidad)
            End While

            drConsulta.Close()

            Return lListaRetorno
        Catch ex As Exception
            drConsulta = Nothing
            Throw New Exception("Error en la carga de los items de menú: " & ex.Message)
        End Try
    End Function

    ''' <summary>
    ''' Carga la entidad con los datos que le proporciona el DataReader <paramref name="pdrArticuloEstructura "> pdrArticuloEstructura </paramref> que tiene como parametro.
    ''' </summary>
    ''' <param name="pdrArticuloEstructura"> <see cref="MySqlDataReader">DataReader</see> que tiene todos los campos necesarios para completar las propiedades de la entidad </param>
    ''' <returns>Verdadero si todo termino bien. Lanza una excepcion generica en caso de error.</returns>
    ''' <remarks>
    ''' <para>El DataReader <paramref name="pdrArticuloEstructura ">pdrArticuloEstructura</paramref> debe estar abierto y se le debe haber hecho un Read() antes de invocar a esta funcion.</para>
    ''' <para>Programadores: Bosquiazzo, Dario;</para>
    ''' </remarks>
    Public Function LoadItem(ByRef o As libEntities.Entities.MenuItem, ByRef pDr As SqlDataReader) As Boolean
        Try
            o.MenuId = Es_Nulo(Of String)(pDr("menuId"), "")
            o.MenuPadreID = Es_Nulo(Of String)(pDr("menuPadreId"), "")
            o.ModuloID = Es_Nulo(Of Long)(pDr("moduloID"), 0)
            o.Descripion = Es_Nulo(Of String)(pDr("Descripcion"), "")
            o.Observacion = Es_Nulo(Of String)(pDr("observacion"), "")
            o.Orden = Es_Nulo(Of Long)(pDr("orden"), 0)
            o.TipoMenu = Es_Nulo(Of Long)(pDr("TipoMenu"), 0)
            o.FuncionID = Es_Nulo(Of Long)(pDr("FuncionID"), 0)
            LoadItem = True
        Catch ex As Exception
            LoadItem = False
            Throw (ex)
        End Try
    End Function

    End Class
