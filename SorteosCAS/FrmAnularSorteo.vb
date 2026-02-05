Imports sorteos.bussiness
Imports libEntities.Entities
Imports sorteos.data
Imports System.IO
Imports System.Security.Cryptography
Imports sorteos.helpers

Public Class FrmAnularSorteo
    Dim usuarioBO As New UsuarioBO
    Dim ousuario As New cUsuario
    Dim vOpc As New PgmConcurso
    Private _inicio As Boolean
    Private height_original As Integer = Me.Height
    Private height_login As Integer = 170

    Public Property inicio() As Boolean
        Get
            Return _inicio
        End Get
        Set(ByVal value As Boolean)
            _inicio = value
        End Set
    End Property

    Private Sub btnIngresar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIngresar.Click
        Dim pwd As String
        Try
            If Me.txtusuario.Text.Trim = "" Then
                MsgBox("Ingrese Usuario", MsgBoxStyle.Information, MDIContenedor.Text)
                txtusuario.Focus()
                Exit Sub
            End If
            If Me.txtpwd.Text.Trim = "" Then
                MsgBox("Ingrese contraseña", MsgBoxStyle.Information, MDIContenedor.Text)
                txtpwd.Focus()
                Exit Sub
            End If

            pwd = ObtenerMD5(Me.txtpwd.Text.ToLower)
            ousuario = usuarioBO.getUsuario(Me.txtusuario.Text, pwd)
            If Not ousuario Is Nothing Then
                If ousuario.ReversionHabilitada Then
                    Me.Grplogin.Enabled = False
                    Me.Height = height_original
                Else
                    lblerror.Text = "Usuario no habilitado."
                    lblerror.Visible = True
                    If txtusuario.Enabled Then txtusuario.Focus()
                End If
            Else
                lblerror.Text = "Usuario y/o contraseña incorrectos"
                lblerror.Visible = True
                If txtusuario.Enabled Then txtusuario.Focus()
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, MDIContenedor.Text)
        End Try
    End Sub

    Private Function ObtenerMD5(ByVal fichero As String) As String
        Dim md5 As New MD5CryptoServiceProvider
        Dim byteHash() As Byte, ret As String = Nothing
        Try
            byteHash = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(fichero))
            md5.Clear()
            For Each bs As Byte In byteHash
                ret &= bs.ToString("x2")
            Next
            Return ret
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Sub FrmAnularSorteo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub

    Private Sub btnAnular_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Try
            Dim usuarioBO As New UsuarioBO
            Dim nrosorteo As Long = 0
            Dim idusuario As Long = 0
            Dim pgmsorteoBo As New PgmSorteoBO
            Dim pgmconcursoBO As New PgmConcursoBO
            Dim usuario As String = ""
            Dim motivo As String = ""
            Dim opgmsorteo As New PgmSorteo
            Dim opgmsorteoAux As New PgmSorteo
            Dim opgmconcurso As New PgmConcurso
            Dim osorteo As PgmSorteo
            Dim errorWeb As Boolean = False
            If Me.txtusuario.Text.Trim = "" Then
                MsgBox("Ingrese usuario", MsgBoxStyle.Information, MDIContenedor.Text)
                txtusuario.Focus()
                Exit Sub
            End If


            usuario = txtusuario.Text.Trim
            usuario = usuario.Replace("'", "")
            motivo = txtmotivo.Text.Trim
            motivo = motivo.Replace("'", "")
            If usuario.Trim = "" Then
                MsgBox("No ha ingresado un usuario válido.", MsgBoxStyle.Information, MDIContenedor.Text)
                txtusuario.Focus()
                Exit Sub
            End If
            If cboConcurso.SelectedItem.Nombre.Trim.ToUpper = "ELIJA UN SORTEO" Then
                MsgBox("No ha elegido qué SORTEO va a revertir.", MsgBoxStyle.Information, MDIContenedor.Text)
                cboExtractos.Focus()
                Exit Sub
            End If
            If cboExtractos.SelectedItem.NombreSorteo.Trim.ToUpper = "ELIJA UN EXTRACTO" Then
                MsgBox("No ha elegido qué extracto va a revertir.", MsgBoxStyle.Information, MDIContenedor.Text)
                cboExtractos.Focus()
                Exit Sub
            End If

            If General.Jurisdiccion = "S" Then 'solo para santa fe se pide mas detalle
                If motivo.Trim = "" Then
                    MsgBox("No ha ingresado un motivo de reversión válido.", MsgBoxStyle.Information, MDIContenedor.Text)
                    txtmotivo.Focus()
                    Exit Sub
                Else
                    If motivo.Length < 10 Then
                        MsgBox("El Motivo ingresado es demasiado abreviado. Detalle más", MsgBoxStyle.Information, MDIContenedor.Text)
                        txtmotivo.Focus()
                        Exit Sub
                    End If
                End If
            End If

            idusuario = ousuario.IdUsuario
            opgmsorteo = cboExtractos.SelectedItem
            opgmconcurso = cboConcurso.SelectedItem
            opgmsorteoAux = pgmsorteoBo.getUltimoEnCurso()
            If Not (opgmsorteoAux Is Nothing) Then
                If opgmsorteoAux.idPgmConcurso <> opgmsorteo.idPgmConcurso And opgmsorteo.fechaHora > opgmsorteoAux.fechaHora Then
                    FrmMensajeConfirmacionReversion._titulo = "Existe un sorteo anterior NO CONFIRMADO."
                    FrmMensajeConfirmacionReversion.lblanterior.Text = "ANTERIOR NO CONFIRMADO"
                    FrmMensajeConfirmacionReversion._nombreJuegoARevertir = opgmsorteo.NombreSorteo
                    FrmMensajeConfirmacionReversion._nroSorteoARevertir = opgmsorteo.nroSorteo
                    FrmMensajeConfirmacionReversion._nombreJuegoUltimo = opgmsorteoAux.NombreSorteo
                    FrmMensajeConfirmacionReversion._nroSorteoUltimo = opgmsorteoAux.nroSorteo
                    FrmMensajeConfirmacionReversion.ShowDialog()
                    If FrmMensajeConfirmacionReversion._accion.ToUpper.Trim <> "CONTINUAR" Then
                        txtmotivo.Focus()
                        Exit Sub
                    Else
                        Me.txtmotivo.Text = Me.txtmotivo.Text.Trim & vbCrLf & FrmMensajeConfirmacionReversion._titulo & " Juego " & opgmsorteoAux.NombreSorteo & " sorteo " & opgmsorteoAux.nroSorteo
                        motivo = Me.txtmotivo.Text
                    End If
                End If
            End If

            'opgmsorteoAux = pgmsorteoBo.getUltimoConfirmado(opgmsorteo.idJuego)
            opgmsorteoAux = pgmsorteoBo.getUltimoConfirmado()
            If Not (opgmsorteoAux Is Nothing) Then
                'If opgmsorteoAux.idJuego <> opgmsorteo.idJuego Or opgmsorteoAux.nroSorteo <> opgmsorteo.nroSorteo Then
                If opgmsorteoAux.idPgmConcurso <> opgmsorteo.idPgmConcurso And opgmsorteo.fechaHora < opgmsorteoAux.fechaHora Then
                    FrmMensajeConfirmacionReversion._titulo = "El extracto a revertir es ANTERIOR al último confirmado."
                    FrmMensajeConfirmacionReversion.lblanterior.Text = "ULTIMO CONFIRMADO"
                    FrmMensajeConfirmacionReversion._nombreJuegoARevertir = opgmsorteo.NombreSorteo
                    FrmMensajeConfirmacionReversion._nroSorteoARevertir = opgmsorteo.nroSorteo
                    FrmMensajeConfirmacionReversion._nombreJuegoUltimo = opgmsorteoAux.NombreSorteo
                    FrmMensajeConfirmacionReversion._nroSorteoUltimo = opgmsorteoAux.nroSorteo
                    FrmMensajeConfirmacionReversion.ShowDialog()
                    If FrmMensajeConfirmacionReversion._accion.ToUpper.Trim <> "CONTINUAR" Then
                        txtmotivo.Focus()
                        Exit Sub
                    Else
                        Me.txtmotivo.Text = Me.txtmotivo.Text.Trim & vbCrLf & FrmMensajeConfirmacionReversion._titulo & " Juego " & opgmsorteoAux.NombreSorteo & " sorteo " & opgmsorteoAux.nroSorteo
                        motivo = Me.txtmotivo.Text
                    End If

                End If
            End If

            Me.Cursor = Cursors.WaitCursor
            Me.lblpublicando.Text = "Revirtiendo..."
            Me.lblpublicando.Visible = True
            Me.Refresh()
            'idusuario = usuarioBO.getUsuarioID("usuarioanular")


            If MsgBox("Ud está a punto de revertir '" & IIf(opgmsorteo.NombreSorteo.Trim.ToUpper = "TODOS", "TODOS los extractos del sorteo   ", " el extracto de   " & opgmsorteo.NombreSorteo.Trim.ToUpper) & "'. Esta acción ocultará en la WEB el/los extracto/s revertidos y permitirá su modificación." & vbCrLf & "¿Desea continuar?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.OkCancel, MDIContenedor.Text) = MsgBoxResult.Cancel Then
                Me.Cursor = Cursors.Default
                Me.lblpublicando.Text = ""
                Exit Sub
            End If

            '** si la opcion es todos,el idpgmcsorteo esta en -1 sino tiene el idpgmsorteo correspondiente
            If opgmsorteo.idPgmSorteo = -1 Then 'revierte todos los sorteos del concurso
                pgmconcursoBO.RevertirConcurso(opgmsorteo.idPgmConcurso)
            Else 'revierte un sorteo determinado
                pgmconcursoBO.RevertirConcurso(opgmsorteo.idPgmConcurso, opgmsorteo.idPgmSorteo)
            End If

            'AGREGADO POR FSCOTTA
            Dim _PublicaExtractosWSRestOFF As String = General.PublicaExtractosWSRestOFF
            Dim _PublicaExtractosWSRestON As String = General.PublicaExtractosWSRestON
            If _PublicaExtractosWSRestOFF <> "N" Or _PublicaExtractosWSRestON <> "N" Then
                Try
                    If opgmsorteo.idPgmSorteo <> -1 Then 'revierte un solo sorteo
                        AnularSorteoWebRest(opgmsorteo)
                    Else 'tiene que revertir todos los sorteos del concurso
                        For Each osorteo In opgmconcurso.PgmSorteos
                            AnularSorteoWebRest(osorteo)
                        Next
                    End If
                    '*** fin de reversion *******
                Catch ex As Exception
                    errorWeb = True
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy HH:mm:ss") & " btnanular_click.Problemas al Anular web rest:" & ex.Message & " pgmsorteo:" & opgmsorteo.idPgmSorteo)
                End Try
            End If

            '*** reversion web **********
            Dim _PublicarWebOFF As String = General.PublicarWebOFF
            Dim _PublicarWebON As String = General.PublicarWebON
            If _PublicarWebOFF <> "N" Or _PublicarWebON <> "N" Then
                Try
                    If opgmsorteo.idPgmSorteo <> -1 Then 'revierte un solo sorteo
                        AnularSorteoWeb(opgmsorteo)
                    Else 'tiene que revertir todos los sorteos del concurso
                        For Each osorteo In opgmconcurso.PgmSorteos
                            AnularSorteoWeb(osorteo)
                        Next
                    End If
                    '*** fin de reversion *******
                Catch ex As Exception
                    errorWeb = True
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy HH:mm:ss") & " btnanular_click.Problemas al Anular web:" & ex.Message & " pgmsorteo:" & opgmsorteo.idPgmSorteo)
                End Try
            End If
            '** agrego el registro de log
            Try
                usuarioBO.setLogAnulacion(opgmsorteo.idPgmConcurso, opgmsorteo.idPgmSorteo, idusuario, usuario.Trim, motivo)
            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy HH:mm:ss") & " btnanular_click.Problemas al generar log:" & ex.Message & " Usuario:" & usuario & " Motivo:" & motivo)
            End Try
            Me.lblpublicando.Text = ""
            Me.lblpublicando.Visible = False
            Me.Cursor = Cursors.Default
            If errorWeb Then
                MsgBox("Se ha realizado la reversión del concurso con éxito pero se presentaron los siguientes problemas." & vbCrLf & "-Problemas al revertir la publicación Web.Para revertir la publicación Web,ingrese desde el menú Interfaces ", MsgBoxStyle.Information, MDIContenedor.Text)
            Else
                If MDIContenedor.formInicio IsNot Nothing Then
                    Try
                        MDIContenedor.formInicio.Close()
                        MDIContenedor.formInicio.Dispose()
                    Catch ex As Exception

                    End Try
                End If
                If MDIContenedor.formRegistrarExtracciones IsNot Nothing Then
                    Try
                        MDIContenedor.formRegistrarExtracciones.Close()
                        MDIContenedor.formRegistrarExtracciones.Dispose()
                    Catch ex As Exception

                    End Try
                End If
                If MDIContenedor.formPremios IsNot Nothing Then
                    Try
                        MDIContenedor.formPremios.Close()
                        MDIContenedor.formPremios.Dispose()
                    Catch ex As Exception

                    End Try
                End If
                If MDIContenedor.formPublicar IsNot Nothing Then
                    Try
                        MDIContenedor.formPublicar.Close()
                        MDIContenedor.formPublicar.Dispose()
                    Catch ex As Exception

                    End Try
                End If
                If MDIContenedor.formOtrasJuridiscciones2 IsNot Nothing Then
                    Try
                        MDIContenedor.formOtrasJuridiscciones2.Close()
                        MDIContenedor.formOtrasJuridiscciones2.Dispose()
                    Catch ex As Exception

                    End Try
                End If
                If MDIContenedor.formFinalizar IsNot Nothing Then
                    Try
                        MDIContenedor.formFinalizar.Close()
                        MDIContenedor.formFinalizar.Dispose()
                    Catch ex As Exception

                    End Try
                End If
                MsgBox("Reversión realizada con éxito.", MsgBoxStyle.Information, MDIContenedor.Text)
            End If

            Me.Close()

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.lblpublicando.Text = ""
            Me.lblpublicando.Visible = False
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy HH:mm:ss") & " Problemas al revertir sorteo:" & ex.Message)
            MsgBox("Problemas al revertir concurso:" & ex.Message, MsgBoxStyle.Critical, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub FrmAnularSorteo_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Me.Location = New System.Drawing.Point(0, 0)
        Me.height_original = Me.Height
        Me.Height = height_login
        ObtenerConcursos()
        inicio = False

    End Sub
    Private Sub ObtenerConcursos()
        Try
            Dim oconcurso As New PgmConcursoBO
            Dim oPC As New PgmConcurso
            Dim lstConcurso As New List(Of PgmConcurso)
            Dim lstConcurso2 As New List(Of PgmConcurso)
            lstConcurso2 = oconcurso.getPgmConcursosaRevertir(Now)
            If lstConcurso2.Count = 0 Then
                MsgBox("No se encuentran concurso para ser Revertidos", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            '** crea un nuevo elemento para "Todos"
            oPC = New PgmConcurso
            oPC.idPgmConcurso = -1
            oPC.nombre = "ELIJA UN SORTEO"
            lstConcurso.Add(oPC)
            For Each op As PgmConcurso In lstConcurso2
                lstConcurso.Add(op)
            Next

            Me.cboConcurso.DataSource = lstConcurso
            cboConcurso.ValueMember = "idPgmConcurso"
            cboConcurso.DisplayMember = "nombre"
            cboConcurso.Refresh()
            cboConcurso.SelectedIndex = 0 'lstConcurso.Count - 1
            'vOpc = lstConcurso(0)
            'ObtenerSorteos(vOpc)
            If inicio Then txtusuario.Focus()
        Catch ex As Exception
            MsgBox("Problemas al  obtener concursos:" & ex.Message, MsgBoxStyle.Critical, MDIContenedor.Text)

        End Try
    End Sub

    Private Sub ObtenerSorteos(ByVal oconcurso As PgmConcurso)
        Try
            Dim osorteo As PgmSorteo
            Dim odal As New PgmSorteoDAL
            Dim lstsorteos As New ListaOrdenada(Of PgmSorteo)
            Dim jueboBO As New JuegoBO
            Dim juegodesc As String = ""

            lstsorteos = odal.getPgmSorteos(oconcurso.idPgmConcurso, oconcurso.idPgmConcurso, 50)

            If lstsorteos.Count > 0 Then
                '** crea un nuevo elemento para "Todos"
                osorteo = New PgmSorteo
                osorteo.idPgmConcurso = oconcurso.idPgmConcurso
                osorteo.idPgmSorteo = -1
                osorteo.NombreSorteo = "TODOS"
                lstsorteos.Add(osorteo)

                '** crea un nuevo elemento para "Elija un extracto"
                osorteo = New PgmSorteo
                osorteo.idPgmConcurso = oconcurso.idPgmConcurso
                osorteo.idPgmSorteo = -2
                osorteo.NombreSorteo = "ELIJA UN EXTRACTO"
                lstsorteos.Add(osorteo)

                cboExtractos.DataSource = lstsorteos
                cboExtractos.ValueMember = "idpgmsorteo"
                cboExtractos.DisplayMember = "nombresorteo"
                cboExtractos.Refresh()
                cboExtractos.SelectedIndex = lstsorteos.Count - 1
            End If
            If inicio Then txtusuario.Focus()

        Catch ex As Exception
            MsgBox("Problemas al  obtener sorteos :" & ex.Message, MsgBoxStyle.Critical, MDIContenedor.Text)

        End Try
    End Sub


    Private Sub cboConcurso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cboConcurso.SelectedIndexChanged
        
        Try
            Dim oconcurso As New PgmConcurso
            If Not inicio Then
                oconcurso = cboConcurso.SelectedItem
                If oconcurso.idPgmConcurso <> -1 Then
                    ObtenerSorteos(oconcurso)
                End If

            End If
            If inicio Then txtusuario.Focus()
        Catch ex As Exception

        End Try
    End Sub

    Public Sub New()

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        inicio = True
    End Sub

    Private Sub Btncancelarlogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Btncancelarlogin.Click
        Me.Close()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()
    End Sub

    Private Sub txtpwd_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtpwd.GotFocus
        txtpwd.SelectAll()
    End Sub

    Private Sub txtpwd_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtpwd.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            btnIngresar_Click(sender, e)
        End If
    End Sub

    Private Sub txtusuario_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtmotivo.Enabled Then txtmotivo.Focus()
        End If
    End Sub

    Private Sub txtmotivo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtmotivo.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If btnAnular.Enabled Then btnAnular.Focus()
        End If
    End Sub
    Private Function AnularSorteoWeb(ByVal opgmSorteo As PgmSorteo) As Boolean
        Try
            Dim sorteoLotBO As New PgmSorteoLoteriaBO
            Dim PgmSorteoBO As New PgmSorteoBO
            Dim oJuegoBO As New JuegoBO
            ' Anulo otras jurisdicciones, si existen
            'Dim oexlot As pgmSorteo_loteria
            'For Each oexlot In opgmSorteo.ExtraccionesLoteria
            'sorteoLotBO.AnularWeb(opgmSorteo, oexlot.Loteria.IdLoteria, True)
            'Next
            ' Por ultimo anulo Santa FE
            ' Primero anula autorizacion de extracto oficial en la web
            PgmSorteoBO.AutorizarExtractoOficial(opgmSorteo, opgmSorteo.fechaHoraPrescripcion, opgmSorteo.fechaHoraProximo, opgmSorteo.fechaHoraIniReal, opgmSorteo.fechaHora, opgmSorteo.fechaHoraFinReal, opgmSorteo.PozoEstimado, 0)

            '** Luego quita la marca de confirmado parcial si corresponde a una quiniela
            If oJuegoBO.esQuiniela(opgmSorteo.idJuego) Then
                PgmSorteoBO.AnularQuinielaSF(opgmSorteo, opgmSorteo.fechaHoraPrescripcion, opgmSorteo.fechaHoraProximo, opgmSorteo.fechaHoraIniReal, opgmSorteo.fechaHora, opgmSorteo.fechaHoraFinReal)
            End If

            Return True
        Catch ex As Exception
            AnularSorteoWeb = False
            Throw New Exception("AnularSorteowerb: " & ex.Message)
        End Try
    End Function

    Private Function AnularSorteoWebRest(ByVal opgmSorteo As PgmSorteo, Optional ByVal forzarOFFLine As Boolean = False) As Boolean
        Try
            Dim sorteoLotBO As New PgmSorteoLoteriaBO
            Dim PgmSorteoBO As New PgmSorteoBO

            PgmSorteoBO.anularExtRest(opgmSorteo, forzarOFFLine)

            Return True
        Catch ex As Exception
            AnularSorteoWebRest = False
            Throw New Exception("AnularSorteowerbRest: " & ex.Message)
        End Try
    End Function

    Private Sub txtpwd_keypress(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtpwd.KeyPress
        lblerror.Text = ""
    End Sub

    Private Sub txtusuario_keypress(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtusuario.KeyPress
        lblerror.Text = ""
    End Sub

End Class