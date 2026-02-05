'Imports System.IO

Imports sorteos.helpers
Imports sorteos.entities
Imports sorteos.bussiness
Imports sorteos.extractos
Imports Sorteos.Data

Public Class FrmEnviarMailExtractoSF

    Private Sub btnEnviar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnviar.Click
        Dim jBO As New JuegoBO
        Dim oJuego As New Juego

        Dim oSorteo As New PgmSorteo
        Dim boSorteo As New PgmSorteoBO
        Dim oArchivoBold As New ArchivoBoldtBO
        Dim path As String = ""

        ' ***** valida las entradas **************
        If cmbJuego.SelectedValue = 0 Or txtNroSorteo.Text = "" Then
            MsgBox("Indique número de sorteo y juego a generar.")
            txtNroSorteo.Focus()
            Exit Sub
        End If
        If Not IsNumeric(Me.txtNroSorteo.Text) Then
            MsgBox("El número de sorteo debe ser un número.")
            txtNroSorteo.Focus()
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
            Exit Sub
        End If
        ' ***************************************
        path = txtDestinatarios.Text

        lblgenerando.Text = "Generando..."
        lblgenerando.Visible = True
        Me.Refresh()
        Me.Cursor = Cursors.WaitCursor
        btnEnviar.Enabled = False

        Try
            oSorteo = boSorteo.getPgmSorteo(oSorteo.idPgmSorteo)

            If (oSorteo.idJuego = 2 Or oSorteo.idJuego = 3 Or oSorteo.idJuego = 8 Or oSorteo.idJuego = 49) Then
                If (oSorteo.idEstadoPgmConcurso < 40 And General.Jurisdiccion = "E") Then
                    MsgBox("El sorteo no ha sido finalizado.")
                    txtNroSorteo.Focus()
                    Exit Sub
                Else
                    If General.Jurisdiccion <> "E" Then
                        If Not ((oSorteo.idEstadoPgmConcurso = 40 And oSorteo.ConfirmadoParcial = True) Or (oSorteo.idEstadoPgmConcurso > 40)) Then
                            MsgBox("El sorteo no ha sido confirmado.")
                            txtNroSorteo.Focus()
                            Exit Sub
                        End If
                    End If
                End If
            Else
                If oSorteo.idEstadoPgmConcurso <> 50 Then
                    MsgBox("El sorteo no ha sido confirmado")
                    txtNroSorteo.Focus()
                    Exit Sub
                End If
            End If

            Dim _pdf As String = ""
            Dim falloExtractoLocal As Boolean = False

            ' Traigo la configuracion del juego
            oJuego = jBO.getJuego(oSorteo.idJuego)
            'If oSorteo.idEstadoPgmConcurso = 50 And General.ExtractoAMedios = "S" Then
            If oSorteo.idEstadoPgmConcurso = 50 And oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") Then
                '** extracto local
                Try
                    Dim ds As New ExtractoReporte
                    ds.GenerarExtractoLocal(oSorteo.idPgmSorteo, _pdf, General.PathInformes)
                Catch ex As Exception
                    falloExtractoLocal = True
                    MsgBox("Problemas al generar el Extracto. No se realiza el envío.")
                    Exit Sub
                End Try
            End If
            If oSorteo.idEstadoPgmConcurso < 50 Or falloExtractoLocal Or (Not (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E"))) Then
                ' Obtengo el pdf -> GenerarPdfExtractoSF
                ' Si ok -> Enviar mail.
                If Not GenerarPdfExtractoSF(oSorteo.idPgmConcurso, _pdf) Then
                    MsgBox("Problemas al generar el reporte")
                    Exit Sub
                End If
            End If

            'envio por mail el pdf con el reporte
            lblgenerando.Text = "Enviando..."
            EnviarMail(_pdf)
            'System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15))
            MsgBox("Extracto enviado satisfactoriamente a los destinatarios indicados.", MsgBoxStyle.Information)

        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & ex.Message)
            MsgBox("Problema al enviar extracto: " & ex.Message, MsgBoxStyle.Critical)
        Finally
            lblgenerando.Text = "Finalizado..."
            Me.Cursor = Cursors.Default
            btnEnviar.Enabled = True
            Me.Close()
            Me.Dispose()
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

    Private Sub FrmEnviarMailExtractoSF_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub

    Private Sub FrmEnviarMailExtractoSF_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim boJuego As New JuegoBO
        Dim lstJuegos As ListaOrdenada(Of Juego)
        Dim idjuegos As String = "2,3,8,49"
        Dim destinatarios As String = General.ListaEnvioExtracto
        Me.Location = New System.Drawing.Point(0, 0)


        'lstJuegos = boJuego.getJuegos(idjuegos)
        lstJuegos = boJuego.getJuegosAMedios()
        cmbJuego.DataSource = lstJuegos
        cmbJuego.ValueMember = "IdJuego"
        cmbJuego.DisplayMember = "Jue_Desc"
        cmbJuego.Refresh()
        Me.txtDestinatarios.Text = destinatarios
    End Sub


    Private Function EnviarMail(ByVal pdf As String) As Boolean
        Try

            GeneralBO.EnviarMail(pdf, txtDestinatarios.Text.Trim, txtdesc.Text.Trim)
        Catch ex As Exception
            Throw New Exception(ex.Message)
            Return False
        End Try

        'Dim MailHost As String = General.MailHost
        'Dim Mailport As String = General.MailPort
        'Dim MailFrom As String = General.MailFrom
        'Dim MailSep As String = General.MailSep
        'Dim ListaEnvioExtracto As String = txtDestinatarios.Text
        'Dim boMail As New MailBO
        'Dim Cuerpo As String = ""
        'Dim Titulo As String = ""

        'If General.Jurisdiccion = "E" Then
        '    Cuerpo = "Se Adjuntan datos del Extracto de Entre Ríos. " & vbCrLf & vbCrLf & vbCrLf & "IAFAS - Instituto de Ayuda Financiera a la Acción Social" & vbCrLf & "                             Lotería de Entre Ríos"
        '    Titulo = "Extracto de Entre Ríos"
        'Else
        '    Cuerpo = "Se Adjuntan datos del Extracto de Santa Fe. " & vbCrLf & vbCrLf & vbCrLf & "CAS - Caja de Asistencia Social" & vbCrLf & "         Lotería de Santa Fe"
        '    Titulo = "Extracto de Santa Fe"
        'End If

        'Dim _adjunto As New List(Of String)
        'Try
        '    _adjunto.Add(pdf)

        '    boMail.envioMail(MailHost, MailFrom, ListaEnvioExtracto, Titulo, Cuerpo, False, _adjunto, MailSep)
        '    boMail = Nothing
        '    'System.Threading.Thread.Sleep(TimeSpan.FromSeconds(15))
        'Catch ex As Exception
        '    Throw New Exception(ex.Message)
        '    Return False
        'End Try
        Return True

    End Function

End Class