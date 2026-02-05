Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Drawing.Printing

Public Class Listado
    Public Function GenerarLstDifCuad(ByVal ds As DataSet, ByVal path_reporte As String, Optional ByVal enviarAImpresora As Boolean = True, Optional ByVal visualizar As String = "") As Boolean
        Dim rpt As New ReportDocument
        Dim ruta As String = path_reporte & "\rptDifCuad.rpt"
        Try
            rpt.Load(ruta)
            rpt.Refresh()
            rpt.SetDataSource(ds)
            rpt.SetParameterValue("visualizar", visualizar)
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Function
        End Try
        If enviarAImpresora Then
            Try
                Dim pd As New PrintDocument
                Dim s_Default_Printer As String = pd.PrinterSettings.PrinterName
                rpt.PrintOptions.PrinterName = s_Default_Printer
                rpt.PrintToPrinter(1, True, 1, 99)
            Catch ex As Exception
                MsgBox("Problemas con la Impresora al intentar imprimir listado. Verifique o consulte al personal técnico. Para imprimir cuando esté solucionado el inconveniente, ingrese desde el menú imprimir.", MsgBoxStyle.Exclamation, "Problema en impresora")
            End Try
        End If
    End Function

    Public Function GenerarLstDifCuadPdf(ByVal ds As DataSet, ByVal path_reporte As String, ByRef path_pdf As String, ByRef msgRet As String, Optional ByVal nombreReporte As String = "", Optional ByVal nombrePdf As String = "", Optional ByVal enviarAImpresora As Boolean = True, Optional ByVal visualizar As String = "") As Boolean
        Dim rpt As New ReportDocument
        Dim ruta As String = ""
        Dim rutapdf As String = ""
        If Left(path_pdf, 1) <> "\" Then
            path_pdf = path_pdf & "\"
        End If
        rutapdf = path_pdf & nombrePdf
        If nombreReporte.Trim.Length > 0 Then
            ruta = path_reporte & "\" & nombreReporte
        Else
            ruta = path_reporte & "\rptDifCuad.rpt"
        End If

        Try
            rpt.Load(ruta)
            rpt.Refresh()
            rpt.SetDataSource(ds)
            rpt.SetParameterValue("visualizar", visualizar)
            ' AGREGAR EXPORTAR A PDF
            Try
                If System.IO.File.Exists(rutapdf) Then
                    System.IO.File.Delete(rutapdf)
                End If
            Catch ex As Exception

            End Try
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, rutapdf)

            '*** lo manda a la impresora
            If enviarAImpresora Then
                Try
                    rpt.PrintToPrinter(1, True, 1, 99)
                Catch ex As Exception
                    MsgBox("GenerarLstDifCuadPdf ->Problemas al imprimir listado.Para imprimir,ingrese desde el menú imprimir.", MsgBoxStyle.Exclamation, "Impresión")
                End Try
            End If
            rpt.Dispose()
            rpt.Close()
            rpt = Nothing

        Catch ex As Exception
            Throw New Exception("GenerarLstDifCuadPdf -> " & ex.Message)
            Return False
        End Try

        Return True

    End Function

    Public Function GenerarListado(ByVal ds As DataSet, ByVal path_reporte As String, Optional ByVal enviarAImpresora As Boolean = True, Optional ByVal visualizar As String = "") As Boolean
        Dim rpt As New ReportDocument
        Dim ruta As String = path_reporte & "\rptExtracciones.rpt"
        Try
            rpt.Load(ruta)
            rpt.Refresh()
            rpt.SetDataSource(ds)
            rpt.SetParameterValue("visualizar", visualizar)
        Catch ex As Exception
            MsgBox(ex.Message)
            Exit Function
        End Try
        If enviarAImpresora Then
            Try
                Dim pd As New PrintDocument
                Dim s_Default_Printer As String = pd.PrinterSettings.PrinterName
                rpt.PrintOptions.PrinterName = s_Default_Printer
                rpt.PrintToPrinter(1, True, 1, 99)
            Catch ex As Exception
                MsgBox("Problemas con la Impresora al intentar imprimir listado. Verifique o consulte al personal técnico. Para imprimir cuando esté solucionado el inconveniente, ingrese desde el menú imprimir.", MsgBoxStyle.Exclamation, "Problema en impresora")
            End Try
        End If
    End Function

    Public Function GenerarListadoPdf(ByVal ds As DataSet, ByVal path_reporte As String, ByRef path_pdf As String, ByRef msgRet As String, Optional ByVal nombreReporte As String = "", Optional ByVal nombrePdf As String = "", Optional ByVal enviarAImpresora As Boolean = True, Optional ByVal visualizar As String = "") As Boolean
        Dim rpt As New ReportDocument
        Dim ruta As String = ""
        Dim rutapdf As String = ""
        If Left(path_pdf, 1) <> "\" Then
            path_pdf = path_pdf & "\"
        End If
        rutapdf = path_pdf & nombrePdf
        If nombreReporte.Trim.Length > 0 Then
            ruta = path_reporte & "\" & nombreReporte
        Else
            ruta = path_reporte & "\rptExtracciones.rpt"
        End If

        Try
            rpt.Load(ruta)
            rpt.Refresh()
            rpt.SetDataSource(ds)
            rpt.SetParameterValue("visualizar", visualizar)
            ' AGREGAR EXPORTAR A PDF
            Try
                If System.IO.File.Exists(rutapdf) Then
                    System.IO.File.Delete(rutapdf)
                End If
            Catch ex As Exception

            End Try
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, rutapdf)

            '*** lo manda a la impresora
            If enviarAImpresora Then
                Try
                    rpt.PrintToPrinter(1, True, 1, 99)
                Catch ex As Exception
                    MsgBox("GenerarListadoPdf ->Problemas al imprimir listado.Para imprimir,ingrese desde el menú imprimir.", MsgBoxStyle.Exclamation, "Impresión")
                End Try
            End If
            rpt.Dispose()
            rpt.Close()
            rpt = Nothing

        Catch ex As Exception
            Throw New Exception("GenerarListadoPdf -> " & ex.Message)
            Return False
        End Try

        Return True

    End Function

    Public Function GenerarListadoParametros(ByVal DsPar As DataSet, ByVal PathReporte As String, ByVal destino As String, ByVal PathDestino As String, ByVal msgret As String) As Boolean
        Return GenerarParametros(DsPar, PathReporte)
    End Function
    Public Function GenerarListadoDifCuad(ByVal DslE As DataSet, ByVal PathReporte As String, ByVal destino As String, ByRef PathDestino As String, ByVal msgret As String, Optional ByVal nombreReporte As String = "", Optional ByVal nombrePdf As String = "", Optional ByVal enviarAImpresora As Boolean = True, Optional ByVal visualizar As String = "") As Boolean
        If destino = 0 Then
            Return GenerarLstDifCuad(DslE, PathReporte, enviarAImpresora, visualizar)
        Else
            Return GenerarLstDifCuadPdf(DslE, PathReporte, PathDestino, msgret, nombreReporte, nombrePdf, enviarAImpresora, visualizar)
        End If
    End Function
    Public Function GenerarListadoExtracciones(ByVal DslE As DataSet, ByVal PathReporte As String, ByVal destino As String, ByRef PathDestino As String, ByVal msgret As String, Optional ByVal nombreReporte As String = "", Optional ByVal nombrePdf As String = "", Optional ByVal enviarAImpresora As Boolean = True, Optional ByVal visualizar As String = "") As Boolean
        If destino = 0 Then
            Return GenerarListado(DslE, PathReporte, enviarAImpresora, visualizar)
        Else
            Return GenerarListadoPdf(DslE, PathReporte, PathDestino, msgret, nombreReporte, nombrePdf, enviarAImpresora, visualizar)
        End If
    End Function
    'Public Function GenerarListadoExtractoOficial(ByVal DsPar As DataSet, ByVal PathReporte As String, ByVal destino As String, ByVal PathDestino As String, ByVal msgret As String) As Boolean
    Public Function GenerarListadoExtractoOficial(ByVal DsPar As DataSet, ByVal PathReporte As String, Optional ByVal visualizar As String = "") As Boolean
        Return (GenerarListadoMail(DsPar, PathReporte, visualizar))
    End Function

    Public Function GenerarParametros(ByVal ds As DataSet, ByVal path_reporte As String, Optional ByVal conGanadores As String = "N", Optional ByVal nCopias As Integer = 1) As Boolean
        Dim rpt As New ReportDocument
        Dim ruta As String = path_reporte & "\rptListadoParametros.rpt"
        Dim dsSub As New DataSet

        Try
            'dsSub.Tables.Add(ds.Tables(1))
            ' Default printer      
            Dim pd As New PrintDocument
            Dim s_Default_Printer As String = pd.PrinterSettings.PrinterName
            rpt.PrintOptions.PrinterName = s_Default_Printer
            rpt.Load(ruta)
            rpt.Refresh()

            rpt.SetDataSource(ds.Tables(0))
            rpt.Subreports.Item("ParametrosDetalleJuegos.rpt").SetDataSource(ds.Tables(1))
            If Sorteos.Helpers.General.Jurisdiccion <> "E" Then
                rpt.Subreports.Item("ParametrosDetalleJuegosConApuGan.rpt").SetDataSource(ds.Tables(1))
                rpt.SetParameterValue("ConGanadores", conGanadores)
            End If
        Catch ex As Exception
            'MsgBox("Problemas al Generar Listado de Parametros.")
            Throw New Exception("GenerarParametros -> " & ex.Message)
            Return False
        End Try
        Try
            rpt.PrintToPrinter(nCopias, True, 1, 99)
            Return True
        Catch ex As Exception
            'MsgBox("Problemas al imprimir Listado de Parámetros. Para reintentar, ingrese desde el menú imprimir.", MsgBoxStyle.Exclamation, MDIContenedor.Text))
            Throw New Exception("GenerarParametros -> " & ex.Message)
            Return False
        End Try

    End Function

    Public Function GenerarListadoMail(ByVal ds As DataSet, ByVal path_reporte As String, Optional ByVal visualizar As String = "") As Boolean
        Dim rpt As New ReportDocument
        Dim ruta As String = path_reporte & "\rptExtraccionesMail.rpt"
        Try
            rpt.Load(ruta)
            rpt.Refresh()
            rpt.SetDataSource(ds)
            rpt.SetParameterValue("visualizar", visualizar)
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
        Try
            rpt.PrintToPrinter(1, True, 1, 99)
        Catch ex As Exception
            MsgBox("GenerarListadoMail ->Problemas al imprimir listado.Para imprimir,ingrese desde el menú imprimir.", MsgBoxStyle.Information, "Impresión")
        End Try


    End Function
    Public Sub GenerarParametrosPozoProximo(ByVal ds As DataSet, ByVal path_reporte As String, Optional ByVal nCopias As Integer = 1)
        Dim rpt As New ReportDocument
        Dim ruta As String = path_reporte & "\rptparametrospozoproximo.rpt"
        Dim dsSub As New DataSet

        Try
            'dsSub.Tables.Add(ds.Tables(1))
            ' Default printer      
            Dim pd As New PrintDocument
            Dim s_Default_Printer As String = pd.PrinterSettings.PrinterName
            rpt.PrintOptions.PrinterName = s_Default_Printer
            rpt.Load(ruta)
            rpt.Refresh()
            rpt.SetDataSource(ds)

        Catch ex As Exception
            MsgBox("Problemas GenerarParametrosPozoProximo:" & ex.Message)
        End Try
        Try
            rpt.PrintToPrinter(nCopias, True, 1, 99)
        Catch ex As Exception
            MsgBox("GenerarParametrosPozoProximo ->Problemas al imprimir listado de pozos estimados.Para imprimir,ingrese desde el menú imprimir.", MsgBoxStyle.Exclamation, "Impresión")
        End Try

    End Sub

    Public Sub GenerarEscenariosGanadores1Premio(ByVal ds As DataSet, ByVal path_reporte As String, Optional ByVal nCopias As Integer = 1)
        Dim rpt As New ReportDocument
        Dim ruta As String = path_reporte & "\rptPlanillaPosiblesGanLocutor.rpt"
        Dim dsSub As New DataSet

        Try
            'dsSub.Tables.Add(ds.Tables(1))
            ' Default printer      
            Dim pd As New PrintDocument
            Dim s_Default_Printer As String = pd.PrinterSettings.PrinterName
            rpt.PrintOptions.PrinterName = s_Default_Printer
            rpt.Load(ruta)
            rpt.Refresh()
            rpt.SetDataSource(ds)

        Catch ex As Exception
            MsgBox("Problemas GenerarEscenariosGanadores1Premio:" & ex.Message)
        End Try
        Try
            rpt.PrintToPrinter(nCopias, True, 1, 99)
        Catch ex As Exception
            MsgBox("GenerarEscenariosGanadores1Premio -> Problemas al imprimir listado de escenarios de ganadores. Para imprimir,ingrese desde el menú imprimir.", MsgBoxStyle.Exclamation, "Impresión")
        End Try

    End Sub

End Class

