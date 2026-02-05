Imports Microsoft.VisualBasic
Imports CrystalDecisions.CrystalReports.Engine
Imports CrystalDecisions.Shared
Imports System.Reflection
Imports System.Data
Imports System.Data.OleDb
Imports System.IO
Imports System.Windows.Forms


Public Class ExtractoReporte

    Dim _origen As String = ""
    Public Property Origen() As String
        Get
            Return _origen
        End Get
        Set(ByVal value As String)
            _origen = value
        End Set
    End Property
    'Public Function GenerarExtracto(ByVal sorteo_id As Integer, ByVal path_PDF As String, ByVal path_temporal As String, ByVal path_reporte As String, ByVal path_imagen As String, ByRef nombre_pdf As String) As Boolean
    Public Function GenerarExtractoLocal(ByVal sorteo_id As Long, ByRef path_destino_PDF As String, Optional ByVal path_informes As String = "INFORMES") As Boolean

        'Dim drSorteo As 
        Dim dtSorteo As New DataTable
        Dim dtFirmas As New DataTable
        Dim ds As New DataSet
        Dim cantidad_columnas As Integer
        Dim arc_nombre As String
        Dim plantilla As String
        Dim pos As Integer
        Dim longitud As Integer
        Dim s As String = Application.ExecutablePath
        Dim idjuego As Integer = 0
        Dim path_reporte As String = ""
        Dim path_imagen As String = ""
        Dim path_temporal As String = ""
        Dim nombre_pdf As String = ""
        Dim path_PDF As String = ""
        Dim codigo As Long = Aleatorio(1, 100) 'numeros aleatorios entre el 1-100
        Dim firmaEscribano As String = ""
        Dim firmaJefeSorteo As String = ""

        '** busca la ultima barra donde empieza el nombre del ejecutable
        pos = s.LastIndexOf("\")
        '** obtiene la longitud del nombre
        longitud = s.Length - pos
        path_PDF = s.Substring(0, s.Length - longitud + 1) & "extracto_pdf"
        path_reporte = s.Substring(0, s.Length - longitud + 1) & path_informes
        path_imagen = Application.ExecutablePath.Substring(0, s.Length - longitud + 1) & path_informes & "\GRAFICA"
        path_temporal = Application.ExecutablePath.Substring(0, s.Length - longitud + 1) & "extracto_pdf\sorteador"
        Try
            If Not System.IO.Directory.Exists(path_PDF) Then
                System.IO.Directory.CreateDirectory(path_PDF)
            End If
            If Not System.IO.Directory.Exists(path_temporal) Then
                System.IO.Directory.CreateDirectory(path_temporal)
            End If
        Catch ex As Exception
        End Try

        Try

            ' OBTIENE DATOS DEL SORTEO

            dtSorteo = ExtractoData.Sorteo.GetSorteoDT(sorteo_id)
            dtSorteo.TableName = "Sorteo"

            Dim sorteo_nro = dtSorteo.Rows(0).Item(2).ToString
            Dim juego_id = dtSorteo.Rows(0).Item(1).ToString 'dtSorteo(0).Item(1).ToString
            Dim est_sorteo = dtSorteo.Rows(0).Item(7).ToString 'dtSorteo(0).Item(7).ToString
            Dim idLoteria = dtSorteo.Rows(0).Item(0).ToString

            If idLoteria = "E" Then
                dtFirmas = ExtractoData.Sorteo.GetFirmasDT(sorteo_id)
                dtFirmas.TableName = "Firmas"
                firmaEscribano = path_imagen & "\" & dtFirmas.Rows(0).Item(0).ToString
                firmaJefeSorteo = path_imagen & "\" & dtFirmas.Rows(0).Item(1).ToString
                dtFirmas = Nothing
            End If

            FileSystemHelperE.Log("sorteo_id -> " & sorteo_id & vbCrLf)
            FileSystemHelperE.Log("sorteo_nro -> " & sorteo_nro & vbCrLf)
            FileSystemHelperE.Log("juego_id -> " & juego_id & vbCrLf)
            FileSystemHelperE.Log("est_sorteo -> " & est_sorteo & vbCrLf)

            ' COMPRUEBA EXISTENCIA DE EXTRACTO
            Dim path_Archivo_pdf As String
            Dim archivoinfo As FileInfo

            If Not Directory.Exists(path_temporal) Then
                Directory.CreateDirectory(path_temporal)
            End If

            nombre_pdf = "ex_" & juego_id & "_" & sorteo_nro & "_" & codigo & ".pdf"
            path_Archivo_pdf = path_PDF & "\" & nombre_pdf
            path_destino_PDF = path_Archivo_pdf

            FileSystemHelperE.Log("nombre_pdf -> " & nombre_pdf & vbCrLf)
            FileSystemHelperE.Log("path_Archivo_pdf -> " & path_Archivo_pdf & vbCrLf)

            If System.IO.File.Exists(path_temporal & "\" & nombre_pdf) Then
                Try
                    System.IO.File.Delete(path_temporal & "\" & nombre_pdf)
                Catch ex As Exception
                End Try
            End If

            If Me.Origen.Trim <> "" Then
                If File.Exists(path_Archivo_pdf) Then
                    FileSystemHelperE.Log("existe archivo pdf para origen:" & Origen)
                    Try
                        System.IO.File.Delete(path_Archivo_pdf)
                    Catch ex As Exception
                    End Try
                End If
            Else
                If File.Exists(path_Archivo_pdf) Then
                    FileSystemHelperE.Log("existe archivo pdf para origen:" & Origen)
                    Try
                        System.IO.File.Delete(path_Archivo_pdf)
                    Catch ex As Exception
                    End Try
                End If
            End If

            ' GENERA REPORTE 
            Dim rpt As New ReportDocument

            '-----------------------------------------------------------------------------------------------
            'Q U I N I E L A S
            '-----------------------------------------------------------------------------------------------
            If juego_id = "P" Or juego_id = "M" _
                  Or juego_id = "V" Or juego_id = "N" Or juego_id = "U" Then
                FileSystemHelperE.Log("Entro al if juego. " & juego_id & vbCrLf)
                ds = ExtractoBussiness.ExtractoQuiniela.GetExtractoDT(sorteo_id)
                cantidad_columnas = ds.Tables.Count
                FileSystemHelperE.Log("ejecuto ok el getextractoDT. cantidad_columnas -> " & cantidad_columnas & vbCrLf)

                'firmaEscribano = path_imagen & "\" & "Escribano_Julio_Cesar_Saurin.png"
                'firmaJefeSorteo = path_imagen & "\" & "Jefe_de_Sorteos_Fabian_Martin.png"

                Select Case juego_id
                    Case "P"
                        plantilla = path_imagen & "/" & "e_Quiniela_"
                        FileSystemHelperE.Log("Entro al CASE. Es Primero. plantilla -> " & plantilla & vbCrLf)
                    Case "M"
                        plantilla = path_imagen & "/" & "e_Quiniela_"
                        FileSystemHelperE.Log("Entro al CASE. matutina. plantilla -> " & plantilla & vbCrLf)

                    Case "V"
                        plantilla = path_imagen & "/" & "e_Quiniela_"
                        FileSystemHelperE.Log("Entro al CASE. vespertina. plantilla -> " & plantilla & vbCrLf)
                    Case "N"
                        plantilla = path_imagen & "/" & "e_Quiniela_"
                        FileSystemHelperE.Log("Entro al CASE. Es noturna. plantilla -> " & plantilla & vbCrLf)
                    Case "U"
                        plantilla = path_imagen & "/" & "e_Quiniela_"
                        FileSystemHelperE.Log("Entro al CASE. Es noturna. plantilla -> " & plantilla & vbCrLf)
                End Select

                arc_nombre = "rptQuiniela"

                ds.Tables.Add(dtSorteo)
                ' ds.WriteXmlSchema("D:\VS2008\Extractos\DEV\INFORMES_IAFAS\ExtractoQnl.xml")
                FileSystemHelperE.Log("Agrego al dataset el dtSorteo." & vbCrLf)
                Try

                    Dim ruta As String = path_reporte & "/" & arc_nombre & ".rpt"
                    FileSystemHelperE.Log("ruta -> " & ruta & vbCrLf)
                    rpt.Load(ruta)
                    FileSystemHelperE.Log("hizo el  rpt.Load(ruta)" & vbCrLf)
                    rpt.Refresh()
                    FileSystemHelperE.Log("hizo el  rpt.Refresh()" & vbCrLf)

                    rpt.ReportDefinition.Areas("DetailArea1").Sections(0).SectionFormat.EnableSuppress = True
                    FileSystemHelperE.Log("hizo el  rpt.ReportDefinition.Areas(""DetailArea1"").Sections(0).SectionFormat.EnableSuppress = True" & vbCrLf)
                    rpt.ReportDefinition.Areas("DetailArea1").Sections(1).SectionFormat.EnableSuppress = True
                    FileSystemHelperE.Log("hizo el  rpt.ReportDefinition.Areas(""DetailArea1"").Sections(1).SectionFormat.EnableSuppress = True" & vbCrLf)
                    rpt.ReportDefinition.Areas("DetailArea1").Sections(2).SectionFormat.EnableSuppress = True
                    FileSystemHelperE.Log("hizo el  rpt.ReportDefinition.Areas(""DetailArea1"").Sections(2).SectionFormat.EnableSuppress = True" & vbCrLf)
                    rpt.ReportDefinition.Areas("DetailArea1").Sections(3).SectionFormat.EnableSuppress = True
                    FileSystemHelperE.Log("hizo el  rpt.ReportDefinition.Areas(""DetailArea1"").Sections(3).SectionFormat.EnableSuppress = True" & vbCrLf)

                    rpt.ReportDefinition.Areas("DetailArea1").Sections(4).SectionFormat.EnableSuppress = True
                    FileSystemHelperE.Log("hizo el  rpt.ReportDefinition.Areas(""DetailArea1"").Sections(4).SectionFormat.EnableSuppress = True" & vbCrLf)


                    rpt.ReportDefinition.Areas("DetailArea1").Sections(5).SectionFormat.EnableSuppress = True
                    FileSystemHelperE.Log("hizo el  rpt.ReportDefinition.Areas(""DetailArea1"").Sections(5).SectionFormat.EnableSuppress = True" & vbCrLf)

                    ' SubReportes
                    Select Case cantidad_columnas
                        Case 1
                            rpt.Subreports.Item(arc_nombre & "1.rpt").SetDataSource(ds)
                            rpt.ReportDefinition.Areas("DetailArea1").Sections(4).SectionFormat.EnableSuppress = False
                            plantilla = plantilla & "3Col.jpg"
                            rpt.SetParameterValue(0, plantilla)
                            rpt.SetParameterValue(1, est_sorteo)
                            If idLoteria = "E" Then
                                rpt.SetParameterValue(2, firmaEscribano)
                                rpt.SetParameterValue(3, firmaJefeSorteo)
                            End If

                        Case 2
                            rpt.Subreports.Item(arc_nombre & "2.rpt").SetDataSource(ds)
                            rpt.ReportDefinition.Areas("DetailArea1").Sections(3).SectionFormat.EnableSuppress = False
                            plantilla = plantilla & "3Col.jpg"
                            rpt.SetParameterValue(0, plantilla)
                            rpt.SetParameterValue(1, est_sorteo)
                            If idLoteria = "E" Then
                                rpt.SetParameterValue(2, firmaEscribano)
                                rpt.SetParameterValue(3, firmaJefeSorteo)
                            End If
                        Case 3
                            FileSystemHelperE.Log("entes de -> rpt.Subreports.Item(arc_nombre & ""3.rpt"").SetDataSource(ds)" & vbCrLf)
                            rpt.Subreports.Item(arc_nombre & "3.rpt").SetDataSource(ds)
                            FileSystemHelperE.Log("asigno el datasource al subreporte de 3 col" & vbCrLf)
                            rpt.ReportDefinition.Areas("DetailArea1").Sections(0).SectionFormat.EnableSuppress = False
                            plantilla = plantilla & "3Col.jpg"
                            FileSystemHelperE.Log("plantilla -> " & plantilla & vbCrLf)
                            rpt.SetParameterValue(0, plantilla)
                            rpt.SetParameterValue(1, est_sorteo)
                            If idLoteria = "E" Then
                                rpt.SetParameterValue(2, firmaEscribano)
                                rpt.SetParameterValue(3, firmaJefeSorteo)
                            End If
                        Case 4
                            FileSystemHelperE.Log("entes de -> rpt.Subreports.Item(arc_nombre & ""4.rpt"").SetDataSource(ds)" & vbCrLf)
                            rpt.Subreports.Item(arc_nombre & "4.rpt").SetDataSource(ds)
                            FileSystemHelperE.Log("asigno el datasource al subreporte de 4 col" & vbCrLf)
                            rpt.ReportDefinition.Areas("DetailArea1").Sections(1).SectionFormat.EnableSuppress = False
                            plantilla = plantilla & "4Col.jpg"
                            FileSystemHelperE.Log("plantilla -> " & plantilla & vbCrLf)
                            rpt.SetParameterValue(0, plantilla)
                            rpt.SetParameterValue(1, est_sorteo)
                            If idLoteria = "E" Then
                                rpt.SetParameterValue(2, firmaEscribano)
                                rpt.SetParameterValue(3, firmaJefeSorteo)
                            End If
                        Case 5
                            FileSystemHelperE.Log("entes de -> rpt.Subreports.Item(arc_nombre & ""5.rpt"").SetDataSource(ds)" & vbCrLf)
                            rpt.Subreports.Item(arc_nombre & "5.rpt").SetDataSource(ds)
                            FileSystemHelperE.Log("asigno el datasource al subreporte de 5 col" & vbCrLf)
                            rpt.ReportDefinition.Areas("DetailArea1").Sections(2).SectionFormat.EnableSuppress = False
                            plantilla = plantilla & "5Col.jpg"
                            FileSystemHelperE.Log("plantilla -> " & plantilla & vbCrLf)
                            rpt.SetParameterValue(0, plantilla)
                            rpt.SetParameterValue(1, est_sorteo)
                            If idLoteria = "E" Then
                                rpt.SetParameterValue(2, firmaEscribano)
                                rpt.SetParameterValue(3, firmaJefeSorteo)
                            End If
                        Case 6
                            FileSystemHelperE.Log("entes de -> rpt.Subreports.Item(arc_nombre & ""6.rpt"").SetDataSource(ds)" & vbCrLf)
                            rpt.Subreports.Item(arc_nombre & "6.rpt").SetDataSource(ds)
                            FileSystemHelperE.Log("asigno el datasource al subreporte de 6 col" & vbCrLf)
                            rpt.ReportDefinition.Areas("DetailArea1").Sections(5).SectionFormat.EnableSuppress = False
                            plantilla = plantilla & "6Col.jpg"
                            FileSystemHelperE.Log("plantilla -> " & plantilla & vbCrLf)
                            rpt.SetParameterValue(0, plantilla)
                            rpt.SetParameterValue(1, est_sorteo)
                            If idLoteria = "E" Then
                                rpt.SetParameterValue(2, firmaEscribano)
                                rpt.SetParameterValue(3, firmaJefeSorteo)
                            End If
                    End Select
                    ' ds.WriteXmlSchema("D:\ExtractoQnl.xml")
                Catch ex As Exception
                    FileSystemHelperE.Log("error en asignacion de datasource y parametros -> " & ex.Message & vbCrLf)
                    'Throw New Exception(ex.Message)
                End Try

            End If

            '-----------------------------------------------------------------------------------------------
            'Q U I N I  6
            '-----------------------------------------------------------------------------------------------
            If juego_id = "Q2" Then
                FileSystemHelperE.Log(Now & " entra a Juego " & juego_id)
                ds = ExtractoBussiness.ExtractoQuini6.GetExtractoDT(sorteo_id)
                ds.Tables.Add(dtSorteo)

                Dim visualizar As String = ""
                visualizar = getExtra(ds.Tables(0))

                Dim valores As String = ""
                valores = ExtractoData.Valor.GetValores(4, sorteo_nro)
                Dim valor() As String
                valor = Split(valores, "-")
                'ds.WriteXmlSchema("D:\VS2008\Extractos\DEV\INFORMES_IAFAS\ExtractoQuini.xml")

                Dim ruta As String = path_reporte & "/rptQuini6.rpt"
                rpt.Load(ruta)
                rpt.Refresh()
                rpt.SetDataSource(ds)

                Dim plantilla1 As String = path_imagen & "/e_Quini6_1.jpg"
                Dim plantilla2 As String = path_imagen & "/e_Quini6_2.jpg"
                Dim plantilla3 As String = path_imagen & "/e_Quini6_5y6_3.jpg"
                Dim plantilla4 As String = path_imagen & "/e_Quini6_5_4.jpg"


                rpt.SetParameterValue(0, plantilla1)
                rpt.SetParameterValue(1, plantilla2)
                rpt.SetParameterValue(2, visualizar)
                rpt.SetParameterValue(3, valor(0))
                rpt.SetParameterValue(4, valor(1))
                rpt.SetParameterValue(5, valor(2))
                rpt.SetParameterValue(6, plantilla3)
                rpt.SetParameterValue(7, plantilla4)
            End If
            '-----------------------------------------------------------------------------------------------
            'BRINCO
            '-----------------------------------------------------------------------------------------------
            If juego_id = "BR" Then
                ds = ExtractoBussiness.ExtractoBrinco.GetExtractoDT(sorteo_id)
                ds.Tables.Add(dtSorteo)

                'ds.WriteXmlSchema("D:\Visual Studio 2008\Extractos\DEV\INFORMES_IAFAS\ExtractoBr.xml")

                Dim ruta As String = path_reporte
                If sorteo_nro < 666 Then
                    ruta = ruta & "/rptBrinco.rpt"
                Else
                    ruta = ruta & "/rptBrinco_dic2012.rpt"
                End If
                rpt.Load(ruta)
                rpt.Refresh()
                rpt.Subreports.Item("rptBrincoExtracto.rpt").SetDataSource(ds)
                rpt.Subreports.Item("rptBrincoSueldos.rpt").SetDataSource(ds)

            End If

            '-----------------------------------------------------------------------------------------------
            'TOMBOLA
            '-----------------------------------------------------------------------------------------------
            If juego_id = "TM" Then
                ds = ExtractoBussiness.ExtractoTombola.GetExtractoDT(sorteo_id)
                ds.Tables.Add(dtSorteo)

                'ds.WriteXmlSchema("D:\VS2008\Extractos\DEV\INFORMES_IAFAS\ExtractoTm.xml")

                Dim ruta As String = path_reporte & "/rptTombola.rpt"
                rpt.Load(ruta)
                rpt.Refresh()
                rpt.SetDataSource(ds)
            End If

            '-----------------------------------------------------------------------------------------------
            'POCEADA FEDERAL
            '-----------------------------------------------------------------------------------------------
            If juego_id = "PF" Then
                ds = ExtractoBussiness.ExtractoPoceada.GetExtractoDT(sorteo_id)
                ds.Tables.Add(dtSorteo)

                ' ds.WriteXmlSchema("D:\ExtractoPF.xml")

                Dim ruta As String = path_reporte & "/rptPoceada.rpt"
                rpt.Load(ruta)
                rpt.Refresh()
                rpt.SetDataSource(ds)
                '**12/12/2013
                If idLoteria = "E" Then
                    rpt.SetParameterValue(0, firmaEscribano)
                    rpt.SetParameterValue(1, firmaJefeSorteo)
                    ' rpt.Refresh()
                End If
            End If

            '-----------------------------------------------------------------------------------------------
            'LOTERÍA CHICA
            '-----------------------------------------------------------------------------------------------
            If juego_id = "LC" Then
                ds = ExtractoBussiness.ExtractoLoteriaChica.GetExtractoDT(sorteo_id)
                ds.Tables.Add(dtSorteo)

                'ds.WriteXmlSchema("D:\Visual Studio 2008\Extractos\DEV\INFORMES_IAFAS\ExtractoLC.xml")

                Dim ruta As String = path_reporte & "/rptLoteriaChica.rpt"
                rpt.Load(ruta)
                rpt.Refresh()
                rpt.SetDataSource(ds)
            End If

            '-----------------------------------------------------------------------------------------------
            'LOTERÍA TRADICIONAL
            '-----------------------------------------------------------------------------------------------
            If juego_id = "LO" Then
                ds = ExtractoBussiness.ExtractoLoteria.GetExtractoDT(sorteo_id)
                ds.Tables.Add(dtSorteo)

                'ds.WriteXmlSchema("D:\Visual Studio 2008\Extractos\DEV\INFORMES_IAFAS\ExtractoLO.xml")

                Dim ruta As String = path_reporte & "/rptLoteria.rpt"
                rpt.Load(ruta)
                rpt.Refresh()
                rpt.SetDataSource(ds)
                '**27/09/2013
                If idLoteria = "E" Then
                    rpt.SetParameterValue(0, firmaEscribano)
                    rpt.SetParameterValue(1, firmaJefeSorteo)
                    '  rpt.Refresh()
                End If
            End If

            ' EXPORTA REPORTE A PDF EN DIRECTORIO DEFINITIVO 

            'If est_sorteo = 0 Then
            ' rpt.ExportToDisk(ExportFormatType.PortableDocFormat, path_temporal & "\" & nombre_pdf)
            ' Else
            rpt.ExportToDisk(ExportFormatType.PortableDocFormat, path_Archivo_pdf)
            rpt.Close()
            'End If

            'If File.Exists(path_Archivo_pdf) Then
            '    archivoinfo = New FileInfo(path_Archivo_pdf)
            '    If System.IO.File.Exists(path_temporal & "\" & nombre_pdf) Then
            '        System.IO.File.Delete(path_temporal & "\" & nombre_pdf)
            '    End If
            '    archivoinfo.CopyTo(path_temporal & "\" & nombre_pdf)
            '    Exit Function
            'End If
        Catch ex As Exception
            FileSystemHelperE.Log("Excepcion generarextracto " & ex.Message)
            Throw New Exception(ex.Message)
        End Try
    End Function
    Public Function getExtra(ByVal tabla As DataTable) As String

        Dim visualizar As String = "000000"
        Dim arr(18) As String
        Dim row As DataRow

        For Each row In tabla.Rows
            arr(0) = row("EXT_NRO1")
            arr(1) = row("EXT_NRO2")
            arr(2) = row("EXT_NRO3")
            arr(3) = row("EXT_NRO4")
            arr(4) = row("EXT_NRO5")
            arr(5) = row("EXT_NRO6")

            Dim cant As Integer = arr.Length

            visualizar += VerificaNro(arr, row("EXT_NRO7"), 7)
            visualizar += VerificaNro(arr, row("EXT_NRO8"), 8)
            visualizar += VerificaNro(arr, row("EXT_NRO9"), 9)
            visualizar += VerificaNro(arr, row("EXT_NRO10"), 10)
            visualizar += VerificaNro(arr, row("EXT_NRO11"), 11)
            visualizar += VerificaNro(arr, row("EXT_NRO12"), 12)
            visualizar += VerificaNro(arr, row("EXT_NRO13"), 13)
            visualizar += VerificaNro(arr, row("EXT_NRO14"), 14)
            visualizar += VerificaNro(arr, row("EXT_NRO15"), 15)
            visualizar += VerificaNro(arr, row("EXT_NRO16"), 16)
            visualizar += VerificaNro(arr, row("EXT_NRO17"), 17)
            visualizar += VerificaNro(arr, row("EXT_NRO18"), 18)

        Next

        Return visualizar
    End Function

    Private Function VerificaNro(ByRef arr() As String, ByVal nro As String, ByVal posi As Integer) As String

        Dim marca As String = "0"
        Dim cant As Integer = arr.Length

        For j = 0 To cant - 1
            If arr(j) = nro Then
                marca = "1"
            End If
        Next

        arr(posi) = nro

        Return marca

    End Function

    Protected Sub Log(ByVal texto As String)

        'creamos el nombre del archivo
        Dim archivo = FileSystemHelperE.pathLog & "\pruebas.txt"

        'If Not Directory.Exists(My.Application.Info.DirectoryPath & "\Logs") Then
        '    Directory.CreateDirectory(My.Application.Info.DirectoryPath & "\Logs")
        'End If
        'conectamos con el FSO
        Dim confile = CreateObject("scripting.filesystemobject")

        'creamos el objeto TextStream
        Dim fich = confile.OpenTextFile(archivo, 8, True)

        'escribimos los números del 0 al 9
        fich.writeLine(texto)

        'cerramos el fichero
        fich.close()
    End Sub
    Private Function Aleatorio(ByVal Minimo As Long, ByVal Maximo As Long) As Long
        Randomize() ' inicializar 
        Aleatorio = CLng((Minimo - Maximo) * Rnd() + Maximo)
    End Function
End Class
