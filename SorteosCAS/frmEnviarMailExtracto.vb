'Imports System.IO

Imports sorteos.helpers
Imports libEntities.Entities
Imports sorteos.bussiness
Imports sorteos.extractos
Imports Sorteos.Data

Public Class FrmEnviarMailExtracto
    Public vidPgmConcurso As Long = 0
    Public vidPgmSorteo As Long = 0
    Public vosorteo As New PgmSorteo
    Public venviar_archivo As Boolean = True
    Public PDF_OK As Boolean = True

    Private Sub FrmEnviarMailExtracto_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim boJuego As New JuegoBO
        Dim lstJuegos As ListaOrdenada(Of Juego)
        Dim idjuegos As String = "2,3,8,49,26"
        Dim destinatarios As String = General.ListaEnvioExtracto
        Me.Location = New System.Drawing.Point(0, 0)

        If Me.vidPgmSorteo = 0 Then
            'lstJuegos = boJuego.getJuegos(idjuegos)
            lstJuegos = boJuego.getJuegosAMedios()
        Else
            lstJuegos = New ListaOrdenada(Of Juego)
            lstJuegos.Add(boJuego.getJuego(Me.vidPgmSorteo / 1000000))
        End If
        cmbJuego.DataSource = lstJuegos
        cmbJuego.ValueMember = "IdJuego"
        cmbJuego.DisplayMember = "Jue_Desc"
        cmbJuego.Refresh()
        If Me.vidPgmSorteo <> 0 Then
            cmbJuego.SelectedIndex = 0
            cmbJuego.Enabled = False
            txtNroSorteo.Text = Me.vidPgmSorteo Mod 1000000
            txtNroSorteo.Enabled = False

            If habilitar_listar(cmbJuego.SelectedValue, vidPgmSorteo, vidPgmConcurso) Then

                If btnEnviar.Enabled = True Then
                    btnEnviar.Enabled = False
                End If
                If btnlistaranexos.Visible = False Then
                    btnlistaranexos.Visible = True
                End If
                If Me.Chkenviaranexos.Visible = False Then
                    Me.Chkenviaranexos.Visible = True
                End If
            End If
        End If
        Me.txtDestinatarios.Text = destinatarios
    End Sub

    Private Sub btnEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Dim jBO As New JuegoBO
        Dim oJuego As New Juego

        Dim oSorteo As New PgmSorteo
        Dim boSorteo As New PgmSorteoBO
        Dim oArchivoBold As New ArchivoBoldtBO
        Dim path As String = ""
        Dim _pdf As String = ""
        Dim _path As String = ""
        Dim falloExtractoLocal As Boolean = False
        Dim queEnviar As String = ""

        PtbEnvio.Visible = False
        lblenviomail.Visible = False

        ' ***** valida las entradas **************
        If cmbJuego.SelectedValue = 0 Or txtNroSorteo.Text = "" Then
            MsgBox("Indique número de sorteo y juego a generar.")
            txtNroSorteo.Focus()
            Exit Sub
        End If
        If Not IsNumeric(Me.txtNroSorteo.Text) Then
            MsgBox("El número de sorteo debe ser un número.")
            If txtNroSorteo.Enabled Then
                txtNroSorteo.Focus()
            End If
            Exit Sub
        End If

        If Me.txtDestinatarios.Text.Trim = "" Then
            MsgBox("La lista de destinatarios debe contener al menos una dirección de mail.")
            txtDestinatarios.Focus()
            Exit Sub
        End If

        oSorteo.idPgmSorteo = boSorteo.getPgmSorteoId(cmbJuego.SelectedValue, txtNroSorteo.Text)
        If oSorteo.idPgmSorteo = 0 Then
            MsgBox("No existe el número de sorteo para el juego indicado.")
            If txtNroSorteo.Enabled Then
                txtNroSorteo.Focus()
            End If
            Exit Sub
        End If
        oSorteo.idSor = boSorteo.getPgmSorteoIdSor(cmbJuego.SelectedValue, txtNroSorteo.Text)
        ' ***************************************
        path = txtDestinatarios.Text

        PtbEsperando.Visible = True
        btnEnviar.Enabled = False
        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()

        Try
            oSorteo = boSorteo.getPgmSorteo(oSorteo.idPgmSorteo)
            Me.vidPgmSorteo = oSorteo.idPgmSorteo
            Me.vidPgmConcurso = oSorteo.idPgmConcurso
            ' Traigo la configuracion del juego
            oJuego = jBO.getJuego(oSorteo.idJuego)
            'EnviarAMedios: N=NO; LP=Listado Prov; EP=Extracto; EL=Link al extracto
            If (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("N") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("L") And oSorteo.idEstadoPgmConcurso >= 40)) _
                Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("L") And oSorteo.idEstadoPgmConcurso = 40)) _
                Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("L") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("N") And oSorteo.idEstadoPgmConcurso >= 40)) _
                Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("L") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("E") And oSorteo.idEstadoPgmConcurso >= 40)) _
                Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("L") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("L") And oSorteo.idEstadoPgmConcurso >= 40)) _
                 Then
                queEnviar = "Listado"
            Else
                If (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("N") And oSorteo.idEstadoPgmConcurso = 50)) _
                    Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("E") And oSorteo.idEstadoPgmConcurso = 50)) _
                    Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("L") And oSorteo.idEstadoPgmConcurso = 50)) _
                Then
                    If oJuego.EnviarAMedios.Trim.ToUpper.EndsWith("L") Then
                        queEnviar = "Link"
                    Else
                        queEnviar = "Extracto"
                    End If
                Else
                    queEnviar = "Nada"
                End If
            End If

            Select Case queEnviar
                Case "Nada"
                    PtbEnvio.Visible = True
                    lblenviomail.Visible = True
                    PtbEnvio.Image = My.Resources.Imagenes.DELETE
                    lblenviomail.Text = "El Juego no tiene configurado el envío o no se encuentra en el estado requerido. No se realiza el envío."
                    PtbEsperando.Visible = False
                    If txtNroSorteo.Enabled Then
                        txtNroSorteo.Focus()
                    End If
                    Exit Sub
                Case "Listado"
                    ' Obtengo el pdf -> GenerarPdfExtractoSF
                    ' Si ok -> Enviar mail.
                    _path = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14)
                    If Not GeneralBO.GenerarPdfExtractoSF(vidPgmConcurso, _pdf, _path, IIf(General.Jurisdiccion = "S", False, True)) Then
                        'MsgBox("Problemas al generar el reporte")
                        PtbEnvio.Visible = True
                        lblenviomail.Visible = True
                        PtbEnvio.Image = My.Resources.Imagenes.DELETE
                        lblenviomail.Text = "Problemas al generar el Listado de Extracciones. No se realiza el envío."
                        PtbEsperando.Visible = False
                        Exit Sub
                    End If
                Case "Extracto"
                    '** extracto local
                    Try
                        Dim ds As New ExtractoReporte
                        ds.GenerarExtractoLocal(oSorteo.idPgmSorteo, _pdf, General.PathInformes)
                        ''If General.Jurisdiccion = "E" Then
                        ''    Dim ds As New ExtractoReporte
                        ''    ds.GenerarExtractoLocal(oSorteo.idPgmSorteo, _pdf, General.PathInformes)
                        ''Else
                        ''    _pdf = ""
                        ''End If
                    Catch ex As Exception
                        falloExtractoLocal = True
                        'MsgBox("Problemas al generar el Extracto. No se realiza el envío.")
                        PtbEnvio.Visible = True
                        lblenviomail.Visible = True
                        PtbEnvio.Image = My.Resources.Imagenes.DELETE
                        lblenviomail.Text = "Problemas al generar el Extracto. No se realiza el envío."
                        PtbEsperando.Visible = False
                        Exit Sub
                    End Try
                Case "Link"
                    _pdf = ""
                Case Else
                    Me.Cursor = Cursors.Default
                    PtbEnvio.Visible = True
                    lblenviomail.Visible = True
                    PtbEnvio.Image = My.Resources.Imagenes.ImagenAceptar
                    lblenviomail.Text = "Adjunto no especificado"
                    PtbEsperando.Visible = False
                    btnEnviar.Enabled = True
                    Exit Sub
            End Select

            venviar_archivo = False

            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Me.Text & " Va a llamar a EnviarMail - pdf: " & _pdf & " - txtdesc.Text: " & txtdesc.Text.Trim)
            GeneralBO.EnviarMail(_pdf, txtDestinatarios.Text.Trim, txtdesc.Text.Trim, oSorteo, Chkenviaranexos.Checked)

            PtbEnvio.Visible = True
            lblenviomail.Visible = True
            PtbEnvio.Image = My.Resources.Imagenes.ImagenAceptar
            lblenviomail.Text = "Envío de Mail Ok"
            btnEnviar.Enabled = False
            PtbEsperando.Visible = False

        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & ex.Message)
            PtbEsperando.Visible = False
            PtbEnvio.Visible = True
            lblenviomail.Visible = True
            PtbEnvio.Image = My.Resources.Imagenes.DELETE
            lblenviomail.Text = "Envío de Mail Falló"
        Finally
            Me.Cursor = Cursors.Default
            btnEnviar.Enabled = True
        End Try

    End Sub

    Private Function GenerarPdfExtractoSF(ByVal idPgmConcurso As Long, ByRef pdf As String) As Boolean
        Dim msgRet As String = ""
        Dim PgmBO As New PgmConcursoBO
        Dim dt As DataTable
        Dim ds As New DataSet
        Dim dal As New PgmConcursoDAL
        Dim oc As New PgmConcurso
        Dim dtExtra As DataTable
        Dim er As New ExtractoReporte
        Dim visualizar As String = "000000000000000000"
        Try
            dt = PgmBO.ObtenerDatosListado(idPgmConcurso)
            ds.Tables.Add(dt)

            oc = dal.getPgmConcurso(idPgmConcurso)
            For Each opgmsorteo In oc.PgmSorteos
                If opgmsorteo.idPgmSorteo = oc.idPgmSorteoPrincipal Then
                    dtExtra = ExtractoData.Extracto.GetExtractoDT(General.Jurisdiccion, opgmsorteo.idJuego, opgmsorteo.idPgmSorteo)
                    dtExtra.TableName = "Table2"
                    If opgmsorteo.idJuego = 4 Then
                        visualizar = er.getExtra(dtExtra)
                    End If
                    Exit For
                End If
            Next
            ds.Tables.Add(dtExtra)

        Catch ex As Exception
            Throw New Exception("Problema al recuperar datos para el extracto SF. " & ex.Message)
            Return False
        End Try

        Try
            'ds.WriteXmlSchema("D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES\Listado1.xml")
            Dim path_reporte As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes  '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES"
            Dim path_destino As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & "PDF_INFORMES" '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\PDF_INFORMES"
            Try
                If Not System.IO.Directory.Exists(path_destino) Then
                    System.IO.Directory.CreateDirectory(path_destino)
                End If
            Catch ex As Exception

            End Try
            Dim nombrePDF As String = ""
            If General.Jurisdiccion = "E" Then
                nombrePDF = "extracto_entre_rios_" & idPgmConcurso & ".pdf"
            Else
                nombrePDF = "extracto_santa_fe_" & idPgmConcurso & ".pdf"
            End If


            Dim reporte As New Listado
            If Not reporte.GenerarListadoExtracciones(ds, path_reporte, 1, path_destino, msgRet, "rptExtraccionesMail.rpt", nombrePDF, , visualizar) Then
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & "Problema al generar el Listado de Extracto para envío por Correo Electrónico. " & msgRet)
                Throw New Exception("Problema al generar el Listado de Extracto para envío por Correo Electrónico. " & msgRet)
                Return False
            End If
            reporte = Nothing
            pdf = path_destino & nombrePDF  ' armar nombre
            Return True
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & "Problema al generar el Listado de Extracto para envío por Correo Electrónico. " & ex.Message)
            Throw New Exception("Problema al generar el Listado de Extracto para envío por Correo Electrónico. " & ex.Message)
            Return False
        End Try

        Return True
    End Function

    Private Sub FrmEnviarMailExtracto_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
        Catch ex As Exception

        End Try

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            Me.Dispose()
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Function habilitar_listar(ByVal idjuego As Integer, ByVal pidpgmsorteo As Integer, ByVal pidpgmconcurso As Integer) As Boolean
        Try
            Dim odal As New PgmSorteoDAL
            Dim osorteo As New PgmSorteoBO
            Dim concursoBO As New PgmConcursoBO
            Dim archivos_pdf As String = ""
            Dim lista_pdf() As String

            Dim fallo_ftp As Boolean = False
            If General.Obtener_pgmsorteos_ws = "N" Then
                Return False
                Exit Function
            End If

            'solo se habilita para quini,brinco y PF si coprresponde
            If idjuego <> 4 And idjuego <> 13 And idjuego <> 30 Then
                Return False
                Exit Function
            Else
                'quinie6 siempre tiene que tener el PDF de distribucion por jurisdiccion
                If idjuego = 4 Then
                    Return True
                    Exit Function
                Else
                    If odal.SalioPrimer_premio(pidpgmsorteo, idjuego) = True Then
                        'archivos_pdf = concursoBO.Obtener_url_PDF(pidpgmconcurso, fallo_ftp)
                        'If archivos_pdf.Trim.Length > 0 Then
                        '    If Not fallo_ftp Then
                        '        lista_pdf = Split(archivos_pdf, ",")
                        '        If lista_pdf(0).Trim = "" And lista_pdf(1).Trim = "" Then
                        '            MsgBox("DeberÃ­a existir al menos el archivo correspondiente al primer premio ,pero no se ha encontrado." & vbCrLf & "Revise que se encuentre en el archivo de Premios.")

                        '        End If
                        '    End If
                        'Else
                        '    Return False
                        'End If
                        Return True
                    Else
                        Return False
                    End If
                End If
            End If
            Return True

        Catch ex As Exception
            FileSystemHelper.Log("Problemas en habilitar_listar:" & ex.Message)
            'MsgBox("Problemas en habilitar_listar:" & ex.Message, MsgBoxStyle.Critical)
        End Try
    End Function

    Private Sub btnlistaranexos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnlistaranexos.Click

        Dim concursobo As New PgmConcursoBO
        Dim pidPgmConcurso As Integer
        Dim juego As Integer
        Dim bosorteo As New PgmSorteoBO
        Dim archivo_age As String = ""
        Dim archivo_prv As String = ""
        Try

        
            If Me.txtNroSorteo.Text.Trim = "" Then
                MsgBox("Ingrese Nro de sorteo", MsgBoxStyle.Information)
                Exit Sub
            End If

            Me.btnlistaranexos.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            Me.Refresh()

            If vidPgmConcurso <> 0 Then
                pidPgmConcurso = vidPgmConcurso
                juego = vosorteo.idJuego
            Else
                pidPgmConcurso = bosorteo.getPgmSorteoId(cmbJuego.SelectedValue, txtNroSorteo.Text)
                juego = cmbJuego.SelectedValue
            End If


            'primero intenta descargar los archivos del 175 a la carpeta local de premios
            Dim Nrointento As Integer = 1
            While Nrointento <= General.CantidadIntentos
                Try
                    FileSystemHelper.Log("comienza listado de anexos")
                    FileSystemHelper.Log("** inicio funcion  DescargarPDF**")
                    If concursobo.DescargarPDF(pidPgmConcurso) Then
                        FileSystemHelper.Log("** inicio funcion  listarPDF**")
                        concursobo.listar_pdf(juego, txtNroSorteo.Text, archivo_age, archivo_prv, 1)
                        If Me.btnEnviar.Enabled = False Then
                            Me.btnEnviar.Enabled = True
                        End If
                        Exit While
                    Else
                        If Nrointento = General.CantidadIntentos Then
                            Me.btnlistaranexos.Enabled = True
                            Me.Cursor = Cursors.Default
                            Me.Refresh()
                            FileSystemHelper.Log("Problemas al listar anexos:se llego al limite de intentos")
                            MsgBox("Problemas al obtener Anexos.Aplique procedimiento manual para informar:" & vbCrLf & " - Agencia Primer Premio." & vbCrLf & " - Distribución por Jurisdicción.")

                            btnEnviar.Enabled = True

                        End If
                        Nrointento = Nrointento + 1
                    End If

                Catch ex As Exception
                    Me.btnlistaranexos.Enabled = True
                    Me.Cursor = Cursors.Default
                    Me.Refresh()
                    FileSystemHelper.Log("Problemas al listar anexos:" & ex.Message)
                    MsgBox("Problemas al obtener Anexos.Aplique procedimiento manual para informar:" & vbCrLf & " - Agencia Primer Premio." & vbCrLf & " - Distribución por Jurisdicción.")
                    btnEnviar.Enabled = True
                    PDF_OK = False

                    Exit Sub
                End Try
            End While
            Me.btnlistaranexos.Enabled = True
            
            Me.Cursor = Cursors.Default
            Me.Refresh()
        Catch ex As Exception
            Me.btnlistaranexos.Enabled = True
            Me.Cursor = Cursors.Default
            Me.Refresh()
            MsgBox("Problemas al obtener Anexos.Aplique procedimiento manual para informar:" & vbCrLf & " - Agencia Primer Premio." & vbCrLf & " - Distribución por Jurisdicción.")
            btnEnviar.Enabled = True
            PDF_OK = False
        End Try
    End Sub

    Private Sub txtNroSorteo_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNroSorteo.LostFocus
        Try
            Dim bosorteo As PgmSorteoBO
            Dim idpgmsorteo As Integer
            If txtNroSorteo.Text.Trim <> "" Then
                bosorteo = New PgmSorteoBO
                idpgmsorteo = bosorteo.getPgmSorteoId(cmbJuego.SelectedValue, txtNroSorteo.Text)
                If habilitar_listar(cmbJuego.SelectedValue, idpgmsorteo, bosorteo.getPgmConcursoId(idpgmsorteo)) Then
                    If btnEnviar.Enabled = True Then
                        btnEnviar.Enabled = False
                    End If
                    If btnlistaranexos.Visible = False Then
                        btnlistaranexos.Visible = True
                    End If
                    If Me.Chkenviaranexos.Visible = False Then
                        Me.Chkenviaranexos.Visible = True
                    End If
                End If
            End If

        Catch ex As Exception

        End Try
    End Sub
End Class