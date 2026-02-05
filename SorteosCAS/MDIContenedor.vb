Imports System.Windows.Forms
Imports Sorteos.Helpers
Imports libEntities.Entities
Imports Sorteos.Bussiness
Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Linq
Imports System.Text

Public Class MDIContenedor
    Inherits Form

    Public m_ChildFormNumber As Integer
    Private MenuItemActual As ToolStripMenuItem

    '** variables para que se abra una sola instancia de cada formulario
    Public formInicio As ConcursoInicio = Nothing
    Public formFinalizar As FrmConcursoFinalizar = Nothing
    Public formRegistrarExtracciones As ConcursoExtracciones = Nothing
    Public formPremios As frmPremios = Nothing
    Public formPublicar As frmPublicarSorteo = Nothing
    'Public formOtrasJuridiscciones As frmExtraccionesJurisdicciones = Nothing
    Public formOtrasJuridiscciones2 As frmExtraccionesJurisdicciones2 = Nothing
    Public formImprimir As FrmImprimirListados = Nothing
    Public CerrarHijo As Boolean = False
    '**************
    Private formArchivoBoldt As FrmGenerarArchivoExtracto = Nothing
    Private formEnvioMailExtractoSF As FrmEnviarMailExtracto = Nothing
    '**19/10/2012
    Private formPozoSugerido As frmPozoSugerido = Nothing
    '**19/10/2012
    Private formAnularsorteo As FrmAnularSorteo = Nothing
    Private formCambioPWD As FrmCambioPWD = Nothing

    Private formContingencia As FrmcontingenciaWS = Nothing
    '** Usuario autenticado
    Private _usuarioAutenticado As cUsuario

    Public Property usuarioAutenticado() As cUsuario
        Get
            Return _usuarioAutenticado
        End Get
        Set(ByVal value As cUsuario)
            If value Is Nothing Then
                _usuarioAutenticado = Nothing
            Else
                If _usuarioAutenticado Is Nothing Then
                    _usuarioAutenticado = New cUsuario
                End If
                _usuarioAutenticado.IdUsuario = value.IdUsuario
                _usuarioAutenticado.Usuario = value.Usuario
                _usuarioAutenticado.PWD = value.PWD
                _usuarioAutenticado.NombreUsuario = value.NombreUsuario
                _usuarioAutenticado.UltimoAcceso = value.UltimoAcceso
                _usuarioAutenticado.LoginHabilitado = value.LoginHabilitado
                _usuarioAutenticado.PozoEstimadoPFHabilitado = value.PozoEstimadoPFHabilitado
                _usuarioAutenticado.PozoEstimadoQ6yBRHabilitado = value.PozoEstimadoQ6yBRHabilitado
                _usuarioAutenticado.ReversionHabilitada = value.ReversionHabilitada
            End If
        End Set
    End Property

    Private Sub formIniciar_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formInicio = Nothing
    End Sub
    Private Sub formFinalizar_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formFinalizar = Nothing
    End Sub
    Public Sub formRegistrarextracciones_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formRegistrarExtracciones = Nothing
    End Sub
    Private Sub formPublicar_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formPublicar = Nothing
    End Sub
    Private Sub formOtrasJuridiscciones2_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formOtrasJuridiscciones2 = Nothing
    End Sub
    Private Sub formImprimir_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formImprimir = Nothing
    End Sub
    Private Sub formPremios_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formPremios = Nothing
    End Sub
    Private Sub formArchivoBoldt_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formArchivoBoldt = Nothing
    End Sub
    Private Sub formEnvioMailExtractoSF_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formEnvioMailExtractoSF = Nothing
    End Sub
    '**19/10/2012****
    Private Sub formPozoSugerido_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formPozoSugerido = Nothing
    End Sub
    Private Sub formAnularsorteo_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formAnularsorteo = Nothing
    End Sub
    Private Sub formCambioPWD_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formCambioPWD = Nothing
    End Sub
    Private Sub formContingencia_Disposed(ByVal sender As Object, ByVal e As EventArgs)
        formContingencia = Nothing
    End Sub


    Private ReadOnly Property FormIniciarInstance() As ConcursoInicio
        Get
            If formInicio Is Nothing Then
                m_ChildFormNumber += 1
                formInicio = New ConcursoInicio()
                AddHandler formInicio.Disposed, New EventHandler(AddressOf formIniciar_Disposed)
                AddHandler formInicio.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
            End If

            Return formInicio
        End Get
    End Property

    Private ReadOnly Property FormFinalizarInstance() As FrmConcursoFinalizar
        Get
            If formFinalizar Is Nothing Then
                m_ChildFormNumber += 1
                formFinalizar = New FrmConcursoFinalizar()
                AddHandler formFinalizar.Disposed, New EventHandler(AddressOf formFinalizar_Disposed)
                AddHandler formFinalizar.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
            End If

            Return formFinalizar
        End Get
    End Property
    Public ReadOnly Property FormRegistrarExtraccionesInstance() As ConcursoExtracciones
        Get
            If formRegistrarExtracciones Is Nothing Then
                m_ChildFormNumber += 1
                formRegistrarExtracciones = New ConcursoExtracciones()
                AddHandler formRegistrarExtracciones.Disposed, New EventHandler(AddressOf formRegistrarextracciones_Disposed)
                AddHandler formRegistrarExtracciones.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
            End If
            Return formRegistrarExtracciones
        End Get
    End Property

    Private ReadOnly Property FormPremiosInstance() As frmPremios
        Get
            If formPremios Is Nothing Then
                m_ChildFormNumber += 1
                formPremios = New frmPremios()
                AddHandler formPremios.Disposed, New EventHandler(AddressOf formPremios_Disposed)
                AddHandler formPremios.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
            End If

            Return formPremios
        End Get
    End Property
    Private ReadOnly Property FormOtrasJurisdicciones2Instance() As frmExtraccionesJurisdicciones2
        Get
            If formOtrasJuridiscciones2 Is Nothing Then
                m_ChildFormNumber += 1
                formOtrasJuridiscciones2 = New frmExtraccionesJurisdicciones2()
                AddHandler formOtrasJuridiscciones2.Disposed, New EventHandler(AddressOf formOtrasJuridiscciones2_Disposed)
                AddHandler formOtrasJuridiscciones2.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
            End If

            Return formOtrasJuridiscciones2
        End Get
    End Property
    Private ReadOnly Property FormPublicarDisplayInstance() As frmPublicarSorteo
        Get
            If formPublicar Is Nothing Then
                m_ChildFormNumber += 1
                formPublicar = New frmPublicarSorteo()
                AddHandler formPublicar.Disposed, New EventHandler(AddressOf formPublicar_Disposed)
                AddHandler formPublicar.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
                AddHandler formPublicar.FormClosing, New FormClosingEventHandler(AddressOf CierraHijoPublicarDisplay_FormClosing)
            End If

            Return formPublicar
        End Get
    End Property

    Private ReadOnly Property FormPublicarWebInstance() As frmPublicarSorteo
        Get
            If formPublicar Is Nothing Then
                m_ChildFormNumber += 1
                formPublicar = New frmPublicarSorteo()
                AddHandler formPublicar.Disposed, New EventHandler(AddressOf formPublicar_Disposed)
                AddHandler formPublicar.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
                AddHandler formPublicar.FormClosing, New FormClosingEventHandler(AddressOf CierraHijoPublicarWeb_FormClosing)
            End If

            Return formPublicar
        End Get
    End Property
    Private ReadOnly Property FormImprimirListadoInstance() As FrmImprimirListados
        Get
            If formImprimir Is Nothing Then
                m_ChildFormNumber += 1
                formImprimir = New FrmImprimirListados
                AddHandler FrmImprimirListados.Disposed, New EventHandler(AddressOf formImprimir_Disposed)
                AddHandler FrmImprimirListados.Resize, New EventHandler(AddressOf MinimizarHijo_resize)

            End If

            Return formImprimir
        End Get
    End Property

    '****


    Private Sub ShowNewForm(ByVal sender As Object, ByVal e As EventArgs)
        ' Cree una nueva instancia del formulario secundario.
        Dim ChildForm As New System.Windows.Forms.Form
        ' Conviértalo en un elemento secundario de este formulario MDI antes de mostrarlo.
        ChildForm.MdiParent = Me

        ChildForm.Text = "Ventana " & m_ChildFormNumber

        ChildForm.Show()
    End Sub

    Private Sub CloseAllToolStripMenuItem_Click(ByVal sender As Object, ByVal e As EventArgs)
        ' Cierre todos los formularios secundarios del principal.
        For Each ChildForm As Form In Me.MdiChildren
            ChildForm.Close()
        Next
    End Sub

    Private Sub MDIContenedor_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        ' Verifica si existen sorteos pendientes de confirmacion
        Try
            Dim BOpgm As New PgmSorteoBO
            Dim pgmsorteo As New PgmSorteo
            pgmsorteo = BOpgm.getUltimoEnCurso
            If pgmsorteo IsNot Nothing Then
                FileSystemHelper.Log("Sorteos pendientes de confirmación. Verifique o avise a la autoridad coordinadora.Usuario:" & usuarioAutenticado.NombreUsuario & "Juego:" & pgmsorteo.idJuego & " Sorteo:" & pgmsorteo.nroSorteo)
                If MsgBox("Existen sorteos pendientes de confirmación (" & pgmsorteo.NombreSorteo & "). Verifique o avise a la autoridad coordinadora. ¿Desea Salir de todos modos?", MsgBoxStyle.YesNo, "ATENCION: SORTEOS SIN CONFIRMAR") = MsgBoxResult.No Then
                    e.Cancel = True
                    Exit Sub
                End If
            End If
        Catch ex As Exception
        End Try

        ' Detengo el Publicador on-line
        Try
            Dim msj As String = ""
            Dim prc As New ProccessBO
            General.ActualizaPar_Terminar_Publicador()
            prc.DetenerProceso("Publicador.exe", msj)
            prc.DetenerProceso("Publicador", msj)
        Catch ex As Exception
        End Try

        ' Depuro archivos temporales de carpeta extractos_pdf
        Try
            FileSystemHelper.DepurarArchivoPDF()
        Catch ex As Exception
        End Try

        ' Si login esta en memoria lo quito
        Try
            Frmlogin.Close()
            Frmlogin.Dispose()
        Catch ex As Exception
        End Try

    End Sub

    Private Sub MDIContenedor_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Me.usuarioAutenticado Is Nothing Then
            MsgBox("Ingreso inválido: usuario no autenticado.", MsgBoxStyle.Exclamation, Me.Text)
            End
        End If
        Dim miToolStripProf As ToolStripProfessionalRenderer = New ToolStripProfessionalRenderer(New Colores)
        Dim _ModoPublicacion As Integer = 0
        Dim _PublicaDisplay As String = ""
        Dim _PublicaWeb As String = ""
        Dim _PublicaWebOFF As String = ""
        Dim _PublicaWebON As String = ""
        Dim ProcesoBO As New ProccessBO
        Dim lRuta As String = Application.StartupPath
        Dim msj As String = ""
        Try
            If General.Jurisdiccion = "S" Then
                Me.PictureBox1.Visible = True
                'Me.Text = "Sistema de Sorteos"
            Else
                Me.Icon = Nothing
                Me.Refresh()
                'Me.Text = "Sistema de Sorteos"
            End If
            Me.MenuStrip1.Renderer = miToolStripProf
            PUBLICARADISPLAY.CheckState = CheckState.Unchecked
            PUBLICARAWEB.CheckState = CheckState.Unchecked
            Me.Text = "   Sistema de Sorteos v" & General.Version
            'lee del archivo .ini las variables del publicador
            If Not lRuta.EndsWith("\") Then lRuta = lRuta & "\"
            General.PathIni = lRuta
            _ModoPublicacion = CInt(General.ModoPublicacion)
            _PublicaDisplay = General.PublicaDisplay
            _PublicaWeb = General.PublicaWeb
            _PublicaWebON = General.PublicarWebON
            _PublicaWebOFF = General.PublicarWebOFF
            '** actualiza la tabla con los valores del archivo .ini
            General.ActualizaParametrosPublicador(_ModoPublicacion, _PublicaDisplay, _PublicaWeb, 0, _PublicaWebON, _PublicaWebOFF)
            '** si el modo de publicacion es 1,activo el publicador
            If _ModoPublicacion = 1 Then
                'terminar=false
                If Not ProcesoBO.IniciarProceso(lRuta, "Publicador", "Publicador.exe", True, msj) Then
                    MsgBox(msj, MsgBoxStyle.Information, Me.Text)
                End If
            End If

        Catch ex As Exception
            MsgBox("Excepción al cargar sistema. " & ex.Message, Me.Text)
        End Try

    End Sub
    Private Sub MDIContenedor_MdiChildActivate(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.MdiChildActivate
        Try
            Dim snder = CType(sender, MDIContenedor)
            Dim ItemMenu As ToolStripMenuItem
            If m_ChildFormNumber > 0 Then

                'pone normal si existe una opcion seleccionada anteriormente
                Select Case MenuItemActual.Name
                    Case "MIIniciarConcurso"
                        MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                    Case "MIJurisdicciones"
                        MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                    Case "MIPREMIOS"
                        MIPREMIOS.Image = My.Resources.Imagenes.premios_normal
                    Case "MIREGISTRAREXTRACCIONES"
                        MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal
                    Case "MIImprimirListado"
                        MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                    Case "IPUBLICAR"
                        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                    Case "MIFinalizarConcurso"
                        MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
                End Select


                '** pone seleccionada la opcion elegida
                If snder.ActiveMdiChild IsNot Nothing Then
                    Select Case UCase(snder.ActiveMdiChild.Name)
                        Case "CONCURSOINICIO"
                            MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_off
                            MenuItemActual = MIIniciarConcurso
                        Case "fRMOTRASJURISDICCIONES"
                            MIJurisdicciones.Image = My.Resources.Imagenes.jur_off
                            MenuItemActual = MIJurisdicciones
                        Case "FRMEXTRACCIONESJURISDICCIONES"
                            MIJurisdicciones.Image = My.Resources.Imagenes.jur_off
                            MenuItemActual = MIJurisdicciones
                        Case "FRMPREMIOS"
                            MIPREMIOS.Image = My.Resources.Imagenes.premios_off
                            MenuItemActual = MIPREMIOS
                        Case "CONCURSOEXTRACCIONES"
                            MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_off
                            MenuItemActual = MIREGISTRAREXTRACCIONES
                        Case "FRMIMPRIMIRLISTADOS"
                            MIImprimirListado.Image = My.Resources.Imagenes.imp_off
                            MenuItemActual = MIImprimirListado
                        Case "FRMPUBLICARSORTEO"
                            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
                            MenuItemActual = MIPUBLICAR
                        Case "FRMCONCURSOFINALIZAR"
                            MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_off
                            MenuItemActual = MIFinalizarConcurso
                            '********
                        Case "FRMGENERARARCHIVOEXTRACTO"
                            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
                            MenuItemActual = MIPUBLICAR
                        Case "FRMENVIARMAILEXTRACTO"
                            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
                            MenuItemActual = MIPUBLICAR
                            '**22/10/2012
                        Case "FRMPOZOSUGERIDO"
                            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
                            MenuItemActual = MIPUBLICAR
                            '**22/10/2012
                        Case "FRMANULARSORTEO"
                            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
                            MenuItemActual = MIPUBLICAR
                        Case "FRMCAMBIOPWD"
                            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
                            MenuItemActual = MIPUBLICAR
                        Case "FRMCONTIGENCIAWS"
                            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
                            MenuItemActual = MIPUBLICAR

                    End Select
                End If
            Else
                ' recorre todos las opciones de menu para ponerlas como normal
                For Each ItemMenu In Me.MenuStrip1.Items
                    If ItemMenu.Enabled Then
                        If ItemMenu.Text = "&cerrar" Or ItemMenu.Text = "&restaurar" Or ItemMenu.Text = "mi&nimizar" Then
                            'no pinta nada
                        Else
                            Select Case UCase(ItemMenu.Name)
                                Case "MIINICIARCONCURSO"
                                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                                Case "MIJURISDICCIONES"
                                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                                Case "MIPREMIOS"
                                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal
                                Case "MIREGISTRAREXTRACCIONES"
                                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal
                                Case "MIIMPRIMIRLISTADO"
                                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                                Case "MIPUBLICAR"
                                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                                Case "MIFINALIZARCONCURSO"
                                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
                            End Select
                        End If
                    End If
                Next
                'limpia la variable que tenia la ultima opcion seleccionada
                MenuItemActual = Nothing
                Me.PictureBox1.BringToFront()
            End If
        Catch ex As Exception

        End Try
    End Sub

    Private Sub INICIO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIIniciarConcurso.Click

        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_off
        MenuItemActual = MIIniciarConcurso
        Dim frm As ConcursoInicio = Me.FormIniciarInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        frm.Show()
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante

        frm.BringToFront()



    End Sub
    Private Sub MIIniciarConcurso_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MIIniciarConcurso.MouseDown
        MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_press
    End Sub

    Private Sub MIIniciarConcurso_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIIniciarConcurso.MouseHover
        MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_over
    End Sub

    Private Sub MIIniciarConcurso_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIIniciarConcurso.MouseLeave
        If MenuItemActual Is Nothing OrElse MenuItemActual.Name <> "MIIniciarConcurso" Then
            MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
        Else
            MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_off
        End If
    End Sub

    Private Sub REGISTRAREXTRACCIONES_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MIREGISTRAREXTRACCIONES.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_off
        MenuItemActual = MIREGISTRAREXTRACCIONES

        Dim frm As ConcursoExtracciones = Me.FormRegistrarExtraccionesInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then

            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()
    End Sub
    Private Sub MIREGISTRAREXTRACCIONES_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MIREGISTRAREXTRACCIONES.MouseDown
        MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_press
    End Sub

    Private Sub MIREGISTRAREXTRACCIONES_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIREGISTRAREXTRACCIONES.MouseHover
        MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_over
    End Sub

    Private Sub MIREGISTRAREXTRACCIONES_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIREGISTRAREXTRACCIONES.MouseLeave

        If MenuItemActual Is Nothing OrElse MenuItemActual.Name <> "MIREGISTRAREXTRACCIONES" Then
            MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal
        Else
            MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_off
        End If
    End Sub

    Private Sub MIJurisdicciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MIJurisdicciones.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        'MIPREMIOS.Image = My.Resources.Imagenes.jur_off
        MenuItemActual = MIJurisdicciones

        Dim frm As frmExtraccionesJurisdicciones2 = Me.FormOtrasJurisdicciones2Instance
        frm.MdiParent = Me
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then

            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()
        frm.ganarFoco()
    End Sub
    Private Sub MIJurisdicciones_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MIJurisdicciones.MouseDown
        MIJurisdicciones.Image = My.Resources.Imagenes.jur_press
    End Sub

    Private Sub MIJurisdicciones_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIJurisdicciones.MouseHover
        MIJurisdicciones.Image = My.Resources.Imagenes.jur_over
    End Sub

    Friend Sub MIJurisdicciones_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIJurisdicciones.MouseLeave
        If MenuItemActual Is Nothing OrElse MenuItemActual.Name <> "MIJurisdicciones" Then
            MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
        Else
            MIJurisdicciones.Image = My.Resources.Imagenes.jur_off
        End If
    End Sub

    Private Sub MIPREMIOS_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MIPREMIOS.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPREMIOS.Image = My.Resources.Imagenes.premios_off
        MenuItemActual = MIPREMIOS

        Dim frm As frmPremios = Me.FormPremiosInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        frm.Show()
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()
    End Sub
    Private Sub MIPREMIOS_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MIPREMIOS.MouseDown
        MIPREMIOS.Image = My.Resources.Imagenes.premios_press
    End Sub

    Private Sub MIPREMIOS_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIPREMIOS.MouseHover
        MIPREMIOS.Image = My.Resources.Imagenes.premios_over
    End Sub

    Private Sub MIPREMIOS_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIPREMIOS.MouseLeave
        If MenuItemActual Is Nothing OrElse MenuItemActual.Name <> "MIPREMIOS" Then
            MIPREMIOS.Image = My.Resources.Imagenes.premios_normal
        Else
            MIPREMIOS.Image = My.Resources.Imagenes.premios_off
        End If
    End Sub

    Private Sub MIFinalizarConcurso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MIFinalizarConcurso.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal

            End Select
        End If
        MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_off
        MenuItemActual = MIFinalizarConcurso

        Dim frm As FrmConcursoFinalizar = Me.FormFinalizarInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then
            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()

    End Sub
    Private Sub MIFinalizarConcurso_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MIFinalizarConcurso.MouseDown
        MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_press
    End Sub

    Private Sub MIFinalizarConcurso_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIFinalizarConcurso.MouseHover
        MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_over
    End Sub

    Private Sub MIFinalizarConcurso_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIFinalizarConcurso.MouseLeave


        If MenuItemActual Is Nothing OrElse MenuItemActual.Name <> "MIFinalizarConcurso" Then
            MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
        Else
            MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_off
        End If
    End Sub
    Private Sub MIPUBLICAR_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MIPUBLICAR.MouseDown
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_press

    End Sub
    Private Sub MIPUBLICAR_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIPUBLICAR.MouseHover
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_over
    End Sub

    Private Sub MIPUBLICAR_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIPUBLICAR.MouseLeave
        If MenuItemActual Is Nothing OrElse MenuItemActual.Name <> "MIPUBLICAR" Then
            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
        Else
            MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        End If
    End Sub

    Private Sub PUBLICARADISPLAY_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PUBLICARADISPLAY.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        MenuItemActual = MIPUBLICAR

        Dim frm As frmPublicarSorteo = Me.FormPublicarDisplayInstance
        frm.MdiParent = Me
        PUBLICARADISPLAY.CheckState = CheckState.Checked
        frm.DestinoPublicacion = "DISPLAY"
        frm.WindowState = FormWindowState.Normal
        frm.Show()
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()
    End Sub

    Private Sub PUBLICARAWEB_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles PUBLICARAWEB.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        MenuItemActual = MIPUBLICAR
        PUBLICARAWEB.CheckState = CheckState.Checked
        PUBLICARAWEB.Image = My.Resources.Imagenes.boton_off
        Dim frm As frmPublicarSorteo = Me.FormPublicarWebInstance
        frm.MdiParent = Me
        frm.DestinoPublicacion = "WEB"
        frm.WindowState = FormWindowState.Normal
        frm.Show()
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()
    End Sub


    Private Sub MIImprimirListado_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MIImprimirListado.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIImprimirListado.Image = My.Resources.Imagenes.imp_off
        MenuItemActual = MIImprimirListado
        Me.PictureBox1.SendToBack()
        Dim frm As FrmImprimirListados = Me.FormImprimirListadoInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then
            frm.Close()
            Exit Sub
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()


    End Sub

    Private Sub MIImprimirListado_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MIImprimirListado.MouseDown
        MIImprimirListado.Image = My.Resources.Imagenes.imp_press
    End Sub


    Private Sub MIImprimirListado_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIImprimirListado.MouseHover
        MIImprimirListado.Image = My.Resources.Imagenes.imp_over
    End Sub


    Private Sub MIImprimirListado_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles MIImprimirListado.MouseLeave
        If MenuItemActual Is Nothing OrElse MenuItemActual.Name <> "MIImprimirListado" Then
            MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
        Else
            MIImprimirListado.Image = My.Resources.Imagenes.imp_off
        End If
    End Sub
    Private Sub MinimizarHijo_resize(ByVal sender As Object, ByVal e As EventArgs)
        Dim todosMinimizados As Boolean = True

        For i As Integer = 0 To Me.MdiChildren.Length - 1

            If Me.MdiChildren(i).WindowState <> FormWindowState.Minimized Then
                todosMinimizados = False
                Exit For
            End If
        Next
        If todosMinimizados Then
            Me.PictureBox1.BringToFront()
        Else
            Me.PictureBox1.SendToBack()
        End If

    End Sub
    Private Sub CierraHijoPublicarDisplay_FormClosing(ByVal sender As Object, ByVal e As EventArgs)
        Me.PUBLICARADISPLAY.Checked = False
    End Sub
    Private Sub CierraHijoPublicarWeb_FormClosing(ByVal sender As Object, ByVal e As EventArgs)
        Me.PUBLICARAWEB.Checked = False
    End Sub


    Private Sub MI_Iniciar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MI_Iniciar.Click
        Try
            Dim procesoBO As New ProccessBO
            Dim msj As String = ""
            Dim lRuta As String = Application.StartupPath
            If Not procesoBO.IniciarProceso(lRuta, "ConfigPublicador", "ConfigPublicador.exe", True, msj) Then
                MsgBox(msj, MsgBoxStyle.Information, Me.Text)
            End If

        Catch ex As Exception

        End Try

    End Sub

    '******************
    Private ReadOnly Property FormArchivoBoldtInstance() As FrmGenerarArchivoExtracto
        Get
            If formArchivoBoldt Is Nothing Then
                m_ChildFormNumber += 1
                formArchivoBoldt = New FrmGenerarArchivoExtracto()
                AddHandler formArchivoBoldt.Disposed, New EventHandler(AddressOf formArchivoBoldt_Disposed)
                AddHandler formArchivoBoldt.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
                'AddHandler formPublicar.FormClosing, New FormClosingEventHandler(AddressOf CierraHijoPublicarWeb_FormClosing)
            End If

            Return formArchivoBoldt
        End Get
    End Property
    Private ReadOnly Property formEnvioMailExtractoSFInstance() As FrmEnviarMailExtracto
        Get
            If formEnvioMailExtractoSF IsNot Nothing Then
                formEnvioMailExtractoSF.Close()
                Try
                    formEnvioMailExtractoSF.Dispose()
                Catch ex As Exception
                End Try
                formEnvioMailExtractoSF = Nothing
            End If
            If formEnvioMailExtractoSF Is Nothing Then
                m_ChildFormNumber += 1
                formEnvioMailExtractoSF = New FrmEnviarMailExtracto
                AddHandler formEnvioMailExtractoSF.Disposed, New EventHandler(AddressOf formEnvioMailExtractoSF_Disposed)
                AddHandler formEnvioMailExtractoSF.Resize, New EventHandler(AddressOf MinimizarHijo_resize)
                'AddHandler formPublicar.FormClosing, New FormClosingEventHandler(AddressOf CierraHijoPublicarWeb_FormClosing)
            End If

            Return formEnvioMailExtractoSF
        End Get
    End Property
    '***19/10/2012********
    Private ReadOnly Property formPozoSugeridoInstance() As frmPozoSugerido
        Get
            If formPozoSugerido IsNot Nothing Then
                formPozoSugerido.Close()
                Try
                    formPozoSugerido.Dispose()
                Catch ex As Exception
                End Try
                formPozoSugerido = Nothing
            End If
            If formPozoSugerido Is Nothing Then
                m_ChildFormNumber += 1
                formPozoSugerido = New frmPozoSugerido
                AddHandler formPozoSugerido.Disposed, New EventHandler(AddressOf formPozoSugerido_Disposed)
                AddHandler formPozoSugerido.Resize, New EventHandler(AddressOf MinimizarHijo_resize)

            End If

            Return formPozoSugerido
        End Get
    End Property
    '***11/03/2013********
    Private ReadOnly Property formAnularsorteoInstance() As FrmAnularSorteo
        Get
            If formAnularsorteo IsNot Nothing Then
                formAnularsorteo.Close()
                Try
                    formAnularsorteo.Dispose()
                Catch ex As Exception
                End Try
                formAnularsorteo = Nothing
            End If
            If formAnularsorteo Is Nothing Then
                m_ChildFormNumber += 1
                formAnularsorteo = New FrmAnularSorteo
                AddHandler formAnularsorteo.Disposed, New EventHandler(AddressOf formAnularsorteo_Disposed)
                AddHandler formAnularsorteo.Resize, New EventHandler(AddressOf MinimizarHijo_resize)

            End If

            Return formAnularsorteo
        End Get
    End Property

    Private ReadOnly Property formCambioPWDInstance() As FrmCambioPWD
        Get
            If formCambioPWD IsNot Nothing Then
                formCambioPWD.Close()
                Try
                    formCambioPWD.Dispose()
                Catch ex As Exception
                End Try
                formCambioPWD = Nothing
            End If
            If formCambioPWD Is Nothing Then
                m_ChildFormNumber += 1
                formCambioPWD = New FrmCambioPWD
                AddHandler formCambioPWD.Disposed, New EventHandler(AddressOf formCambioPWD_Disposed)
                AddHandler formCambioPWD.Resize, New EventHandler(AddressOf MinimizarHijo_resize)

            End If

            Return formCambioPWD
        End Get
    End Property

    Private ReadOnly Property formContingenciaInstance() As FrmcontingenciaWS
        Get
            If formContingencia IsNot Nothing Then
                formContingencia.Close()
                Try
                    formContingencia.Dispose()
                Catch ex As Exception
                End Try
                formContingencia = Nothing
            End If
            If formContingencia Is Nothing Then
                m_ChildFormNumber += 1
                formContingencia = New FrmcontingenciaWS
                AddHandler formContingencia.Disposed, New EventHandler(AddressOf formContingencia_Disposed)
                AddHandler formContingencia.Resize, New EventHandler(AddressOf MinimizarHijo_resize)

            End If

            Return formContingencia
        End Get
    End Property

    Private Sub Mi_ExtractoBoldt_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles Mi_ExtractoBoldt.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        MenuItemActual = MIPUBLICAR

        Dim frm As FrmGenerarArchivoExtracto = Me.FormArchivoBoldtInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then
            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()
    End Sub

    Private Sub MI_MailExtracto_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MI_MailExtracto.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        MenuItemActual = MIPUBLICAR

        Dim frm As FrmEnviarMailExtracto = Me.formEnvioMailExtractoSFInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then
            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()
    End Sub
    '**19/10/2012
    Private Sub Mi_PozoSugerido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Mi_PozoSugerido.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        MenuItemActual = MIPUBLICAR

        Dim frm As frmPozoSugerido = Me.formPozoSugeridoInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then
            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()

    End Sub


    Private Sub MI_CAMBIARPWD_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MI_CAMBIARPWD.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        MenuItemActual = MIPUBLICAR

        Dim frm As FrmCambioPWD = Me.formCambioPWDInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then
            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()

    End Sub

    Private Sub MI_ANULARSORTEO_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MI_ANULARSORTEO.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        MenuItemActual = MIPUBLICAR

        Dim frm As FrmAnularSorteo = Me.formAnularsorteoInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then
            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()

    End Sub

    Private Sub MI_CONTINGENCIA_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles MI_CONTINGENCIA.Click
        If MenuItemActual IsNot Nothing Then
            Select Case MenuItemActual.Name
                Case "MIIniciarConcurso"
                    MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
                Case "MIJurisdicciones"
                    MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
                Case "MIPREMIOS"
                    MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

                Case "MIREGISTRAREXTRACCIONES"
                    MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

                Case "MIImprimirListado"
                    MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
                Case "IPUBLICAR"
                    MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
                Case "MIFinalizarConcurso"
                    MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
            End Select
        End If
        MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_off
        MenuItemActual = MIPUBLICAR

        Dim frm As FrmcontingenciaWS = Me.formContingenciaInstance
        frm.MdiParent = Me
        frm.WindowState = FormWindowState.Normal
        CerrarHijo = False
        frm.Show()
        If CerrarHijo Then
            frm.Close()
        End If
        ' si el Formulario estaba abirto seguramente este en segundo plano
        ' con esta linea hacemos que pase adelante
        frm.BringToFront()

    End Sub

    Private Sub MIPUBLICAR_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MIPUBLICAR.Click
        Dim _publicarwebOFF As String = General.PublicarWebOFF
        Dim _publicarwebON As String = General.PublicarWebON
        If _publicarwebOFF = "N" And _publicarwebON = "N" Then
            PUBLICARAWEB.Visible = False
        End If
    End Sub

    ''Private Sub MIJurisdicciones_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
    ''    If MenuItemActual IsNot Nothing Then
    ''        Select Case MenuItemActual.Name
    ''            Case "MIIniciarConcurso"
    ''                MIIniciarConcurso.Image = My.Resources.Imagenes.inicio_normal
    ''            Case "MIJurisdicciones"
    ''                MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
    ''            Case "MIPREMIOS"
    ''                MIPREMIOS.Image = My.Resources.Imagenes.premios_normal

    ''            Case "MIREGISTRAREXTRACCIONES"
    ''                MIREGISTRAREXTRACCIONES.Image = My.Resources.Imagenes.sortear_normal

    ''            Case "MIImprimirListado"
    ''                MIImprimirListado.Image = My.Resources.Imagenes.imp_normal
    ''            Case "IPUBLICAR"
    ''                MIPUBLICAR.Image = My.Resources.Imagenes.Interfaces_normal
    ''            Case "MIFinalizarConcurso"
    ''                MIFinalizarConcurso.Image = My.Resources.Imagenes.fin_normal
    ''        End Select
    ''    End If
    ''    MIJurisdicciones.Image = My.Resources.Imagenes.jur_off
    ''    MenuItemActual = MIJurisdicciones
    ''    Dim frm As frmExtraccionesJurisdicciones = Me.FormOtrasJurisdicciones2Instance
    ''    frm.MdiParent = Me
    ''    CerrarHijo = False
    ''    frm.Show()
    ''    If CerrarHijo Then

    ''        frm.Close()
    ''    End If
    ''    ' si el Formulario estaba abirto seguramente este en segundo plano
    ''    ' con esta linea hacemos que pase adelante
    ''    frm.BringToFront()

    ''End Sub


End Class
