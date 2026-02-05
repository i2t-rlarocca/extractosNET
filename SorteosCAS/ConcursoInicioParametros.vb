Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared

Imports sorteos.helpers
Imports libEntities.Entities
Imports Sorteos.Bussiness

Public Class ConcursoInicioParametros
#Region "PROPIEDADES"
    Private _oPC As PgmConcurso

    Public Property oPC() As PgmConcurso
        Get
            Return _oPC
        End Get
        Set(ByVal value As PgmConcurso)
            _oPC = value
        End Set
    End Property
#End Region

#Region "EVENTOS"
    ' ********************************************************************************************
    ' *************************** EVENTOS ********************************************************
    ' ********************************************************************************************
    Private Sub ConcursoInicioParametros_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        listarParametros()
    End Sub
#End Region

#Region "FUNCIONES"
    ' ********************************************************************************************
    ' *************************** FUNCIONES ******************************************************
    ' ********************************************************************************************
    Private Sub listarParametros()
        Dim boPgmConcurso As New PgmConcursoBO
        Dim boExtraccion As New ExtraccionesBO
        Dim boPgmSorteo As New PgmSorteoBO
        Dim boAutoridad As New AutoridadBO
        Dim boPozo As New PozoBO

        Dim oPgmConcurso As New PgmConcurso

        Dim dtCabecera As DataTable
        Dim dtExtraccion As DataTable
        Dim dtSorteos As DataTable
        Dim dtAutoridad As DataTable
        Dim dtPozo As DataTable

        Dim dsCab As New DataSet
        Dim dsExt As New DataSet
        Dim dsSort As New DataSet
        Dim dsAut As New DataSet
        Dim dsPozo As New DataSet

        Dim rpt As New ReportDocument
        Dim ruta As String = "D:\Visual Studio 2008\SorteosCAS\DEV\libExtractos\" & General.PathInformes & "\concursoParametros.rpt"


        dtCabecera = boPgmConcurso.getPgmConcursoDT(oPC.idPgmConcurso)
        dtCabecera.TableName = "Cabecera"
        dsCab.Tables.Add(dtCabecera)


        dtExtraccion = boExtraccion.getExtraccionDT(oPC.idPgmConcurso)
        dtExtraccion.TableName = "Extraccion"
        dsExt.Tables.Add(dtExtraccion)

        dtSorteos = boPgmSorteo.getPgmSorteosDT(oPC.idPgmConcurso)
        dtSorteos.TableName = "Sorteos"
        dsCab.Tables.Add(dtSorteos)

        dtAutoridad = boAutoridad.getAutoridadDT(oPC.idPgmConcurso)
        dtAutoridad.TableName = "Autoridad"
        dsAut.Tables.Add(dtAutoridad)

        dtPozo = boPozo.getPozoDT(oPC.idPgmConcurso)
        dtPozo.TableName = "Pozo"
        dsPozo.Tables.Add(dtPozo)

        'dsCab.WriteXml("D:\Visual Studio 2008\SorteosCAS\DEV\libExtractos\" & General.PathInformes & "\ParametrosCabecera.xml")

        'dsExt.WriteXml("D:\Visual Studio 2008\SorteosCAS\DEV\libExtractos\INFORMES\ParametrosExtracciones.xml")
        'dsSort.WriteXml("D:\Visual Studio 2008\SorteosCAS\DEV\libExtractos\INFORMES\ParametrosSorteos.xml")
        'dsAut.WriteXml("D:\Visual Studio 2008\SorteosCAS\DEV\libExtractos\INFORMES\ParametrosAutoridades.xml")

        'dsPozo.WriteXml("D:\Visual Studio 2008\SorteosCAS\DEV\libExtractos\" & General.PathInformes & "\ParametrosPozo.xml")

        rpt.Load(ruta)
        rpt.Refresh()

        ' estructura del reporte (concursoParametros.rpt)
        ' * datos de la cabecera
        ' * subreporte 0
        '       - plan de extracciones (concursoParametrosExtracciones.rpt)
        ' * subreporte 1
        '       - juegos participantes (concursoParametrosJuegos.rpt)
        '           .datos del juego
        '           . subreporte 0 
        '               autoridades (concursoParametrosJuegosAutoridades.rpt)
        '           . subreporte 1
        '               modalidades y pozos  (concursoParametrosJuegosModalidades.rpt)

        rpt.SetDataSource(dsCab)
        rpt.Subreports(0).SetDataSource(dsExt)
        rpt.Subreports(1).SetDataSource(dsAut)
        'rpt.Subreports(1).Subreports(0).SetDataSource(dsAut)

        

        crvListadoVisor.ReportSource = rpt
        
    End Sub
#End Region

End Class