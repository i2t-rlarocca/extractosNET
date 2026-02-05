Imports Sorteos.Helpers
Imports libEntities.Entities
Imports Sorteos.Bussiness
Imports System.IO
Imports Sorteos.Extractos
Imports Sorteos.Data

Public Class ConcursoInicio
    Private oPst405 As TabPage = Nothing
    Private oPst1305 As TabPage = Nothing

    Private oPstApu405 As TabPage = Nothing
    Private oPstApu1305 As TabPage = Nothing

    Private oPstPozoReal405 As TabPage = Nothing
    Private oPstPozoReal1305 As TabPage = Nothing

    Private l As Integer = -1
    Private r As Integer = -1
    Dim Archivo_Pozo_encontrado As Boolean = False
    Dim segundos As Integer
    Dim opublicador As New Publicador.cPublicador
    Dim estoyEnLoad As Boolean = False

#Region "PROPIEDADES"
    Private _inicio As Boolean
    Private _oPC As PgmConcurso

    Public Property inicio() As Boolean
        Get
            Return _inicio
        End Get
        Set(ByVal value As Boolean)
            _inicio = value
        End Set
    End Property
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
    Public Sub New()
        Try
            ' Llamada necesaria para el Diseñador de Windows Forms.
            InitializeComponent()
        Catch ex As Exception
        End Try
        inicio = True
    End Sub

    Private Sub ConcursoInicio_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        Try
            '*** detiene el timer
            Timer1.Stop()
            '*** actualiza el conteo de ventanas hijas
            MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ConcursoInicio_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Try
            estoyEnLoad = True
            ' 07/10/2011 - Agrego RL: no se inicializaban los valores de los filtros
            Dim d As DateTime = New Date(Date.Now.Ticks)
            Me.Location = New System.Drawing.Point(0, 0)
            dtpFechaConcurso.Value = d.ToString("dd/MM/yyyy")
            dtpFechaConcurso.MinDate = String.Format("{0:dd/MM/yyyy}", d.AddDays(General.DiasSorteosAnteriores * -1).ToShortDateString)
            dtpFechaConcurso.MaxDate = String.Format("{0:dd/MM/yyyy}", d.AddDays(30).ToShortDateString)
            dtpHoraConcurso.Value = d.ToString("dd/MM/yyyy HH:mm:ss")
            dtpHoraConcurso.MinDate = dtpFechaConcurso.MinDate
            dtpHoraConcurso.MaxDate = dtpFechaConcurso.MaxDate

            ' 07/10/2011 - FIN Agrego RL: no se inicializaban los valores de los filtros
            'EL BOTON INCIAR SE HABILITA CUANDO SE HACE CLICK EN LISTAR 
            setValorListaConcurso()
            If oPC IsNot Nothing Then
                setControlesValoresConcurso(oPC)
                If oPC.estadoPgmConcurso = 10 Then
                    btnParametrosListar.Enabled = True
                End If
            Else
                btnParametrosListar.Enabled = False
            End If
            inicio = False
            '** obtiene el tiempo configurado para el timer de pozos
            segundos = opublicador.ObtenerTiempoTimer_pozos
            estoyEnLoad = False
        Catch ex As Exception
            FileSystemHelper.Log(" ConcursoInicio: Load. - Excepcion PASANTE: " & ex.Message)
        End Try
    End Sub

    Private Sub CboConcurso_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboConcurso.SelectedIndexChanged
        If Not inicio Then
            '*** detiene el timer que busca los archivos de pozo,si es que hay algun activo
            Timer1.Stop()
            Archivo_Pozo_encontrado = False
            setControlesValoresConcurso(CboConcurso.SelectedItem)
            '*** iniciar el timer para el aviso de recepcion de archivos,para los juegos que tienen pozo
            If IniciaTimer(CboConcurso.SelectedItem) Then
                Timer1.Stop()
                Timer1.Interval = segundos * 1000
                Timer1.Start()
            End If
        End If
    End Sub

    Private Sub BtnBuscarConcurso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBuscarConcurso.Click
        '*** detiene el timer que busca los archivos de pozo,si es que a+hay algun activo
        Dim prueba As New PgmSorteoBO



        Timer1.Stop()
        pnlApuJuego13.Visible = False
        pnlApuJuego4.Visible = False
        pnlApuJuego30.Visible = False

        pnlPozoJuego13.Visible = False
        pnlPozoJuego4.Visible = False
        pnlPozoJuego30.Visible = False

        pnlPozoRealJuego13.Visible = False
        pnlPozoRealJuego4.Visible = False
        pnlPozoRealJuego30.Visible = False

        Archivo_Pozo_encontrado = False
        Lblespera.Visible = True
        Lblespera.Refresh()


        setValorListaConcurso()

        If oPC IsNot Nothing Then
            '*** iniciar el timer para el aviso de recepcion de archivos para los juegos que tiene pozos
            If IniciaTimer(oPC) Then
                Timer1.Stop()
                Timer1.Interval = segundos * 1000
                Timer1.Start()
            End If
            If oPC.estadoPgmConcurso = 10 Then
                btnParametrosListar.Enabled = True
            End If
        Else
            btnParametrosListar.Enabled = False
        End If


    End Sub

    Private Sub btnAutoridadJuegoAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        autoridadJuegoAgregar()
    End Sub

    Private Sub btnAutoridadJuegoQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        autoridadJuegoQuitar()
    End Sub

    Private Sub btnPozoObtener4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPozoObtener4.Click
        pozoObtener(4, True)
    End Sub

    Private Sub btnPozoGuardar4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPozoGuardar4.Click
        pozoGuardar(4)
    End Sub

    Private Sub btnPozoObtener13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPozoObtener13.Click
        pozoObtener(13, True)
    End Sub

    Private Sub btnPozoGuardar13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPozoGuardar13.Click
        pozoGuardar(13)
    End Sub

    Private Sub btnPozoObtener30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPozoObtener30.Click
        pozoObtener(30, True)
    End Sub

    Private Sub btnPozoGuardar30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPozoGuardar30.Click
        pozoGuardar(30)
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnParametrosListar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnParametrosListar.Click
        Dim boPgmConcurso As New PgmConcursoBO
        Dim oPgmSorteoBO As New PgmSorteoBO
        Dim opgmsorteo As PgmSorteo
        Dim juegodal As New JuegoDAL
        Dim msg As String = ""
        Dim nroVersion As Int32 = 0

        Me.Cursor = Cursors.WaitCursor

        ' Persisto parametros...
        Try
            FileSystemHelper.Log("ParametrosListar: inicio guardar parametros, concurso:" & oPC.idPgmConcurso & ".")
            If oPC Is Nothing Then Exit Sub
            getValores()
            If (boPgmConcurso.valida(oPC, msg)) Then
                boPgmConcurso.setPgmConcurso(oPC)
            Else
                Me.Cursor = Cursors.Default
                MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            FileSystemHelper.Log("ParametrosListar: FIN OK guardar parametros, concurso:" & oPC.idPgmConcurso & ".")
        Catch ex As Exception
            FileSystemHelper.Log("ParametrosListar: ERROR al guardar parametros, concurso:" & oPC.idPgmConcurso & " - Ex: " & ex.Message)
            MsgBox("Ocurrió un problema al guardar los parámetros del concurso. Cierre y vuelva a abrir la ventana e intente nuevamente. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
            Me.Cursor = Cursors.Default
            Exit Sub
        End Try

        ' Validaciones....
        Try
            '**23/07/2012,si el juego principal no tiene autoridades cargadas,se emite un mensaje
            If oPgmSorteoBO.NoTieneAutoridades(oPC.idPgmSorteoPrincipal) Then
                Me.Cursor = Cursors.Default
                MsgBox("Faltan cargar autoridades.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            If oPgmSorteoBO.NoTieneAEscribano(oPC.idPgmSorteoPrincipal) Then
                Me.Cursor = Cursors.Default
                MsgBox("Falta cargar el Escribano. Revise los parámetros.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            If oPgmSorteoBO.NoTieneAutoridadDelSorteo(oPC.idPgmSorteoPrincipal) Then
                Me.Cursor = Cursors.Default
                MsgBox("Falta cargar Autoridad del Sorteo. Revise los parámetros.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            '**27/11/2012
            For Each opgmsorteo In oPC.PgmSorteos
                If juegodal.esQuiniela(opgmsorteo.idJuego) Then
                    If oPgmSorteoBO.NoTieneOtrasJurisdicciones(opgmsorteo.idPgmSorteo) Then
                        Me.Cursor = Cursors.Default
                        MsgBox("Faltan cargar otras jurisdicciones.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                End If
                If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Then
                    If oPgmSorteoBO.NoTienePozos(opgmsorteo.idPgmSorteo, oPC.concurso.IdConcurso) Then
                        Me.Cursor = Cursors.Default
                        MsgBox("El juego '" & opgmsorteo.NombreSorteo & "' no tienen pozos cargados. Revise lo parámetros.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                End If
            Next
        Catch ex As Exception
            FileSystemHelper.Log("ParametrosListar: ERROR PASANTE al validar parametros, concurso:" & oPC.idPgmConcurso & " - Ex: " & ex.Message)
        End Try

        ' si no se han confirmado los parametros para calculo de pozo estimado prox sorteo abro la ventana...
        For Each opgmsorteo In oPC.PgmSorteos
            If opgmsorteo.idJuego = 4 And (Not opgmsorteo.ParProxPozoConfirmado) Then 'Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 
                Me.Cursor = Cursors.Default
                MsgBox("Antes de listar o iniciar debe confirmar los Parámetros para Estimación de Pozo del Próximo Sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
                'btncriteriosPozos_Click(sender, e)
                'Exit Sub
            End If
        Next
        'se habilita el boton de inciar concurso
        If btnConcursoIniciar.Enabled = False Then
            If oPC.estadoPgmConcurso = 10 Then
                btnConcursoIniciar.Enabled = True
            End If
        End If

        ' Imprimo....
        Try
            nroVersion = boPgmConcurso.versionarParametros(oPC.idPgmConcurso, "PROVISORIO", MDIContenedor.usuarioAutenticado.Usuario)

            FileSystemHelper.Log("ParametrosListar: inicio listar parametros, concurso ->" & oPC.idPgmConcurso & "<- nroVersion ->" & nroVersion & "<-.")
            '08/08/2017 RL: el lst parametros se imprime sólo CON o SIN espacio para ganadores 
            '               dependiendo del concurso

            ''24-5-2016 HG- se agregan todos los concurso que contienen poceada
            'If oPC.concurso.IdConcurso = 16 Or oPC.concurso.IdConcurso = 17 Or oPC.concurso.IdConcurso = 18 Or oPC.concurso.IdConcurso = 19 Or oPC.concurso.IdConcurso = 5 Or oPC.concurso.IdConcurso = 7 Or oPC.concurso.IdConcurso = 9 Or oPC.concurso.IdConcurso = 13 Or oPC.concurso.IdConcurso = 15 Or oPC.concurso.IdConcurso = 20 Then
            '    ImprimirParametros(oPC.idPgmConcurso, "S") ' Si es uno de estos concursos imprimo CON espacio para ganadores
            '    If General.ListarReporteDetalleParametros = "S" Then
            '        'si este parametro = S -> se imprime tambien el listado SIN ganadores
            '        ImprimirParametros(oPC.idPgmConcurso, "N")
            '    End If
            'Else
            '    'se imprime el listado de parametros definito
            '    ImprimirParametros(oPC.idPgmConcurso, "N")
            'End If
            If oPC.concurso.LstParConEspacioGan Then
                If oPC.concurso.IdConcurso = 16 Or oPC.concurso.IdConcurso = 17 Or oPC.concurso.IdConcurso = 18 Or oPC.concurso.IdConcurso = 19 Then
                    ImprimirParametros(oPC.idPgmConcurso, "S", 1) ' 5 copias. No funciona la NCopias en la dll de crystal
                    ImprimirParametros(oPC.idPgmConcurso, "S", 1)
                    ImprimirParametros(oPC.idPgmConcurso, "S", 1)
                    ImprimirParametros(oPC.idPgmConcurso, "S", 1)
                    'ImprimirParametros(oPC.idPgmConcurso, "S", 1)
                Else
                    ImprimirParametros(oPC.idPgmConcurso, "S", 1)
                End If
            End If
            ' si, además, el parametro LstRepDetParam = true imprimo version SIN ganad....
            'If General.ListarReporteDetalleParametros = "S" Then
            If oPC.concurso.LstRepDetParam Then
                If oPC.concurso.IdConcurso = 16 Or oPC.concurso.IdConcurso = 17 Or oPC.concurso.IdConcurso = 18 Or oPC.concurso.IdConcurso = 19 Then
                    ImprimirParametros(oPC.idPgmConcurso, "N", 1) ' 3 copias. No funciona la NCopias en la dll de crystal
                    ImprimirParametros(oPC.idPgmConcurso, "N", 1)
                    'ImprimirParametros(oPC.idPgmConcurso, "N", 1)
                Else
                    ImprimirParametros(oPC.idPgmConcurso, "N", 1)
                End If

            End If

            ' 2018 RL: reemplazo el parametro del ini por un campo en bd...
            'FIN 08/08/2017 RL: el lst parametros se imprime sólo CON o SIN espacio para ganadores 
            '               dependiendo del concurso
            FileSystemHelper.Log("ParametrosListar: FIN OK listar parametros, concurso ->" & oPC.idPgmConcurso & "<- nroVersion ->" & nroVersion & "<-.")
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            FileSystemHelper.Log("ParametrosListar: ERROR en listar parametros, concurso:" & oPC.idPgmConcurso & " - Ex: " & ex.Message)
            MsgBox("Ocurrió un problema al intentar imprimir los parámetros del concurso. Verifique la impresora, cierre y vuelva a abrir la ventana e intente nuevamente. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End Try

        ' Imprimo los ESCENARIOS POZOS PROX SORTEO 
        If oPC.concurso.JuegoPrincipal.Juego.EstimaPozosProxSorteo Then
            Try
                FileSystemHelper.Log("ParametrosListar: inicio listar ESCENARIOS POZOS PROX SORTEO, concurso:" & oPC.idPgmConcurso)
                boPgmConcurso.ImprimirParametrospozoproximo(oPC.idPgmConcurso, 1, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, msg, 1)
                boPgmConcurso.ImprimirParametrospozoproximo(oPC.idPgmConcurso, 1, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, msg, 1)
                boPgmConcurso.ImprimirParametrospozoproximo(oPC.idPgmConcurso, 1, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, msg, 1)
                FileSystemHelper.Log("ParametrosListar: FIN OK listar ESCENARIOS POZOS PROX SORTEO, concurso:" & oPC.idPgmConcurso)
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                FileSystemHelper.Log("ParametrosListar: ERROR al listar ESCENARIOS POZOS PROX SORTEO, concurso:" & oPC.idPgmConcurso & " - Ex: " & ex.Message)
                MsgBox("Ocurrió un problema al listar ESCENARIOS POZOS PROX SORTEO. Cierre y vuelva a abrir la ventana e intente nuevamente. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End Try
        End If

        ' Imprimo los ESCENARIOS GANADORES DEL PRIMER PREMIO
        If oPC.concurso.JuegoPrincipal.Juego.EstimaPozosProxSorteo Then
            Try
                FileSystemHelper.Log("ParametrosListar: inicio listar ESCENARIOS GANADORES DEL PRIMER PREMIO, concurso:" & oPC.idPgmConcurso)
                boPgmConcurso.ImprimirEscenariosGanadores1Premio(oPC.idPgmConcurso, 1, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, msg, 1)
                FileSystemHelper.Log("ParametrosListar: FIN OK listar ESCENARIOS GANADORES DEL PRIMER PREMIO, concurso:" & oPC.idPgmConcurso)
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                FileSystemHelper.Log("ParametrosListar: ERROR al listar ESCENARIOS GANADORES DEL PRIMER PREMIO, concurso:" & oPC.idPgmConcurso & " - Ex: " & ex.Message)
                MsgBox("Ocurrió un problema al listar ESCENARIOS GANADORES DEL PRIMER PREMIO. Cierre y vuelva a abrir la ventana e intente nuevamente. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End Try
        End If
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub btnIniciarConcurso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConcursoIniciar.Click
        Dim _tienepozos As Boolean = True
        Dim oPgmSorteoBO As New PgmSorteoBO
        Dim _tieneautoridades As Boolean = True
        Dim _tieneEscribano As Boolean = True
        Dim _tieneAutoridadDelSorteo As Boolean = True
        Dim oJuegoBO As New JuegoBO
        Dim _tieneOtrasJurisdicciones As Boolean = True
        Dim _jurisdiccionesObligatorias As Boolean = IIf(General.JurisdiccionesObligatorias.Trim.ToUpper = "S", True, False)
        Dim _superaMaximoJurisdiciones As Boolean = False

        '**27/11/2012
        Timer1.Stop()
        For Each oPgmSorteo In oPC.PgmSorteos
            If oPgmSorteo.idJuego = 4 Or oPgmSorteo.idJuego = 13 Or oPgmSorteo.idJuego = 30 Then
                If _tienepozos Then
                    If oPgmSorteoBO.NoTienePozos(oPgmSorteo.idPgmSorteo, oPC.concurso.IdConcurso) Then
                        _tienepozos = False
                    End If
                End If
            End If
            If _tieneautoridades Then
                If oPgmSorteoBO.NoTieneAutoridades(oPgmSorteo.idPgmSorteo) Then
                    _tieneautoridades = False
                End If
            End If
            If _tieneEscribano Then
                If oPgmSorteoBO.NoTieneAEscribano(oPgmSorteo.idPgmSorteo) Then
                    _tieneEscribano = False
                End If
            End If

            If oPgmSorteoBO.NoTieneAutoridadDelSorteo(oPgmSorteo.idPgmSorteo) Then
                _tieneAutoridadDelSorteo = False
            End If

            '**27/11/2012*** si es quiniela controla que tenga cargadas otras jurisdicciones(al menos una loteria)

            If oJuegoBO.esQuiniela(oPgmSorteo.idJuego) Then
                If _tieneOtrasJurisdicciones Then
                    If oPgmSorteoBO.NoTieneOtrasJurisdicciones(oPgmSorteo.idPgmSorteo) Then
                        _tieneOtrasJurisdicciones = False
                    End If
                    '**26/11/13 se copntrola la cantidad maxima de otras jurisdicciones
                    Dim cantidad As Integer
                    cantidad = General.CantidadMaximaJurisdicciones
                    If oPgmSorteoBO.CantidadOtrasJurisdicciones(oPgmSorteo.idPgmSorteo) > cantidad Then
                        _superaMaximoJurisdiciones = True
                    End If
                End If

            End If
        Next

        If Not _tienepozos Then
            MsgBox("Existen juegos del concurso '" & oPC.nombre & "' que no tienen pozos cargados.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End If

        If Not _tieneautoridades Then
            MsgBox("Existen juegos del concurso '" & oPC.nombre & "' que no tienen autoridades cargadas.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End If

        If Not _tieneEscribano Then
            MsgBox("Existen juegos del concurso '" & oPC.nombre & "' que no tienen cargada la autoridad 'Area Notarial'. Revise la lista de autoridades.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End If

        If Not _tieneOtrasJurisdicciones Then
            If _jurisdiccionesObligatorias Then
                MsgBox("Existen juegos del concurso '" & oPC.nombre & "' que no tienen cargadas Jurisdicciones. VERIFIQUE.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            Else
                If (MsgBox("Existen juegos del concurso '" & oPC.nombre & "' que no tienen cargadas Jurisdicciones. ¿CONTINUAR DE TODOS MODOS?", MsgBoxStyle.YesNo, MDIContenedor.Text) = MsgBoxResult.No) Then
                    Exit Sub
                End If
            End If
        End If
        '***26/11/2013
        If _superaMaximoJurisdiciones Then
            MsgBox("No se pueden configurar más de " & General.CantidadMaximaJurisdicciones & " Jurisdicciones.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End If

        ' solo para SF...
        If General.Jurisdiccion = "S" Then
            If Not _tieneAutoridadDelSorteo Then
                MsgBox("Existen juegos del concurso '" & oPC.nombre & "' que no tienen cargada la Autoridad del Sorteo. Revise la lista de autoridades.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
        End If
        ' si no se han confirmado los parametros para calculo de pozo estimado prox sorteo abro la ventana...
        For Each opgmsorteo In oPC.PgmSorteos
            If (opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13) And (Not opgmsorteo.ParProxPozoConfirmado) Then ' Or opgmsorteo.idJuego = 30 
                btncriteriosPozos_Click(sender, e)
            End If
        Next
        concursoIniciar()

        'cierra ventana al finalizar concurso
        Try
            ' Me.Dispose()
            Me.Close()
        Catch ex As Exception
        End Try

    End Sub
#End Region




#Region "FUNCIONES"
    ' ********************************************************************************************
    ' *************************** FUNCIONES ******************************************************
    ' ********************************************************************************************
    ' Localica el/los concursos candidatos según fecha y hora ingresados
    Private Sub setValorListaConcurso()
        Dim listaPC As ListaOrdenada(Of PgmConcurso)
        Dim _fechaHora As DateTime
        Dim boPgmConcurso As New PgmConcursoBO
        Dim bopgmsorteo As New PgmSorteoBO
        Dim hora As String
        Dim hay_sorteos As Boolean = False
        Dim mensajeWS As String = ""
        Dim juegop As New ConcursoJuego

        Try
            FileSystemHelper.Log(" ConcursoInicio: setValorListaConcurso: valida y formatea fecha_hora para filtro de obtención de los pgmConcursos aptos para iniciar.")
            ' MVR y la hora??????????????
            _fechaHora = dtpFechaConcurso.Value
            _fechaHora = _fechaHora.AddHours(-1 * dtpFechaConcurso.Value.Hour).AddMinutes(-1 * dtpFechaConcurso.Value.Minute).AddSeconds(-1 * dtpFechaConcurso.Value.Second)
            If Trim(_fechaHora) = "" Then
                MsgBox("Ingrese una fecha para el sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            If Trim(dtpHoraConcurso.Value) = "" Then
                MsgBox("Ingrese una hora para el sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            ' 07/10/2011 - Agrego RL: se quedaba sin la hora
            _fechaHora = _fechaHora.AddHours(dtpHoraConcurso.Value.Hour).AddMinutes(dtpHoraConcurso.Value.Minute).AddSeconds(dtpHoraConcurso.Value.Second)

        Catch ex As Exception
            FileSystemHelper.Log(" ConcursoInicio: setValorListaConcurso: ERROR en valida y formatea fecha_hora para filtro de obtención de los pgmConcursos aptos para iniciar. - Excepcion: " & ex.Message)
            Cursor = System.Windows.Forms.Cursors.Default
            Lblespera.Visible = False
            Me.BtnBuscarConcurso.Enabled = True
            MsgBox("Ocurrió un problema al obtener los concursos. Verifique si tiene sorteos no iniciados para la fecha y hora de búsqueda. Caso contrario cierre y vuelva a abrir la Aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End Try

        ' Antes de consultar deshabilito el Buscar para que no me sigan haciendo click....
        Me.BtnBuscarConcurso.Enabled = False
        If Not estoyEnLoad Then
            Lblespera.Visible = True
        End If
        '*** el boton de criterios de pozos solo se habilita para quini 6
        Me.btncriteriosPozos.Enabled = False
        Me.btncriteriosPozos.Visible = False
        'el boton de iniciar se habilita  al presioanr el boton de listar.
        btnConcursoIniciar.Enabled = False

        ' consulta y carga la lista desde la BD de sorteo...
        Try
            FileSystemHelper.Log(" ConcursoInicio: setValorListaConcurso: obtiene PgmConcursoNoIniciadoOIniciado con fecha:" & _fechaHora)
            listaPC = boPgmConcurso.getPgmConcursoNoIniciadoOIniciado(_fechaHora)
            FileSystemHelper.Log(" ConcursoInicio: setValorListaConcurso: obtiene PgmConcursoNoIniciadoOIniciado con fecha:" & _fechaHora & ". Concursos encontrados en la BD: " & listaPC.Count)
        Catch ex As Exception
            FileSystemHelper.Log(" ConcursoInicio: setValorListaConcurso: ERROR en obtiene PgmConcursoNoIniciadoOIniciado con fecha:" & _fechaHora & " - Excepcion: " & ex.Message)
            Cursor = System.Windows.Forms.Cursors.Default
            Lblespera.Visible = False
            Me.BtnBuscarConcurso.Enabled = True
            MsgBox("Ocurrió un problema al obtener los concursos. Verifique si tiene sorteos no iniciados para la fecha y hora de búsqueda. Caso contrario cierre y vuelva a abrir la Aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End Try

        ' si no encuentra y no esta en el Load, intenta traer del calendario de suite, si está habilitado...
        Try
            If listaPC.Count = 0 Then
                ' Veo si tengo implementada la conexion con el calendario de suite intento traer de ahi...
                If Not estoyEnLoad Then
                    hora = _fechaHora.ToString("yyyy-MM-dd HH:mm")

                    If General.Obtener_pgmsorteos_ws = "S" Then
                        FileSystemHelper.Log(" ConcursoInicio: inicio WS php: obtenerWSpgmsorteos del suite con fecha-hora: " & hora)

                        If bopgmsorteo.obtenerWSpgmsorteos(hora, mensajeWS) Then ' si encontro sorteos,se vuelve a llamar al metodo de cargarconcurso
                            listaPC = boPgmConcurso.getPgmConcursoNoIniciadoOIniciado(_fechaHora)
                            FileSystemHelper.Log(" ConcursoInicio:concurso encontrados  a traves del WS:" & listaPC.Count)
                            If listaPC.Count = 0 Then
                                Cursor = System.Windows.Forms.Cursors.Default
                                MsgBox("No hay Concursos en condiciones de ser iniciados. Verifique el Programa de Sorteos.", MsgBoxStyle.Information, MDIContenedor.Text)
                            Else
                                hay_sorteos = True
                                DeshabilitarControles(True)
                            End If
                        Else
                            ' si no hay registros muestar el mismomensaje de antes
                            If mensajeWS.ToUpper.Trim = "" Then
                                Cursor = System.Windows.Forms.Cursors.Default
                                FileSystemHelper.Log(" ConcursoInicio: FIN SIN DATOS: WS php: obtenerWSpgmsorteos del suite con fecha-hora: " & hora & ".")
                                MsgBox("No hay Concursos en condiciones de ser iniciados. Verifique el Programa de Sorteos", MsgBoxStyle.Information, MDIContenedor.Text)
                            Else
                                If InStr(UCase(mensajeWS), "ERROR") > 0 Then
                                    Cursor = System.Windows.Forms.Cursors.Default
                                    FileSystemHelper.Log(" ConcursoInicio: FIN CON ERROR: WS php: obtenerWSpgmsorteos del suite con fecha-hora: " & hora & ". Mensaje recibido: " & mensajeWS)
                                    MsgBox("Ocurrió un problema al obtener los concursos del Calendario de Suite. Verifique si tiene sorteos no iniciados para la fecha y hora de búsqueda. Caso contrario cierre y vuelva a abrir la Aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
                                Else
                                    Cursor = System.Windows.Forms.Cursors.Default
                                    MsgBox("No hay Concursos en condiciones de ser iniciados. " & vbCrLf & "No se ha registrado el fin de captación de apuestas para los juegos: " & vbCrLf & mensajeWS, MsgBoxStyle.Information, MDIContenedor.Text)
                                End If
                            End If
                        End If ' fin llamado al ws

                    Else ' sigue como antes de Ws
                        Cursor = System.Windows.Forms.Cursors.Default
                        MsgBox("No hay Concursos en condiciones de ser iniciados. Verifique el Programa de Sorteos.", MsgBoxStyle.Information, MDIContenedor.Text)
                    End If ' fin if parametro ws = S

                End If ' fin if no estoy en load

                Lblespera.Visible = False
                Me.BtnBuscarConcurso.Enabled = True
                Cursor = System.Windows.Forms.Cursors.Default

                'grpGeneral.Visible = False
                'deshabilita controles
                If hay_sorteos = False Then
                    DeshabilitarControles()
                    Exit Sub
                End If

            Else 'habilita controles por las dudas
                DeshabilitarControles(True)

                Lblespera.Visible = False
                Me.BtnBuscarConcurso.Enabled = True

                CboConcurso.DataSource = listaPC
                CboConcurso.ValueMember = "idPgmConcurso"
                CboConcurso.DisplayMember = "nombre"
                CboConcurso.Refresh()
                oPC = listaPC(0)

                'si el juego es quini 6 habilita la pestaña de criterios pozos
                juegop = oPC.concurso.JuegoPrincipal

                If (juegop.Juego.IdJuego = 4 Or juegop.Juego.IdJuego = 13) And juegop.Juego.EstimaPozosProxSorteo Then
                    Me.btncriteriosPozos.Enabled = True
                    Me.btncriteriosPozos.Visible = True
                Else
                    Me.btncriteriosPozos.Enabled = False
                    Me.btncriteriosPozos.Visible = False
                End If

                If oPC.estadoPgmConcurso > 10 Then
                    Me.btnConcursoIniciar.Enabled = False
                End If
            End If ' fin count = 0
            'setControlesValoresConcurso(listaPC(0))
            FileSystemHelper.Log(" ConcursoInicio:buscar concurso OK")
        Catch ex As Exception
            FileSystemHelper.Log(" ConcursoInicio:setValorListaConcurso - Excepcion: " & ex.Message)
            Cursor = System.Windows.Forms.Cursors.Default
            Lblespera.Visible = False
            Me.BtnBuscarConcurso.Enabled = True
            If Not estoyEnLoad Then
                MsgBox("Ocurrió un problema al obtener los concursos. Verifique si tiene sorteos no iniciados para la fecha y hora de búsqueda. Caso contrario cierre y vuelva a abrir la Aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
            End If
        End Try
    End Sub

    Private Sub setControlesValoresConcurso(ByVal oPgmConc As PgmConcurso)
        Try
            Dim oPCBO As New PgmConcursoBO
            ' se presentan los valores de los datos fijos
            oPC = oPgmConc
            dtpFechaConcurso.Value = oPC.fechaHora
            dtpHoraConcurso.Value = oPC.fechaHora
            txtJuegoRector.Text = oPC.concurso.JuegoPrincipal.Juego.Jue_Desc
            txtNroSorteoConcurso.Text = oPCBO.getNroSorteoPpal(oPC)
            'CboConcurso.Text = oPC.concurso.Nombre

            txtModeloExtracciones.Text = oPC.concurso.ModeloExtracciones.Nombre
            txtCantExtraccionesConcurso.Text = oPC.concurso.ModeloExtracciones.cantExtracciones

            'se crean los controles dependientes del concurso
            setControles()
            setValores()

            ' RL: habilito / deshabilito botones según estado
            'se habilita el boton de inciar concurso
            If btnConcursoIniciar.Enabled = False Then
                If oPC.estadoPgmConcurso = 10 Then
                    btnConcursoIniciar.Enabled = True
                    btnConcursoRevertir.Enabled = False
                Else
                    btnConcursoIniciar.Enabled = False
                    If oPC.estadoPgmConcurso = 20 Then
                        btnConcursoRevertir.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            FileSystemHelper.Log(" ConcursoInicio:setControlesValoresConcurso - Excepcion PASANTE: " & ex.Message)
            MsgBox("Ha ocurrido una Excepción al intentar completar los datos en pantalla. Cierre el Sistema y vuelta a intentar. Si el problema persiste contacte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub setControles()
        Dim boSorteo As New JuegoBO
        Dim idJuego As Integer
        Dim idExtr As Integer

        Dim Tab As TabControl
        Dim Pestaña As TabPage
        Dim Etiqueta As Label
        Dim Text As TextBox
        Dim DataPicker As DateTimePicker
        Dim Grilla As DataGridView
        Dim Boton As Button
        Dim Check As CheckBox
        Dim panelPozo As Panel
        Dim panelPozoReal As Panel
        Dim panelApu As Panel
        Dim grpBox As GroupBox
        Dim LetraPestania As Font
        Dim LetraAutoridad As Font

        FileSystemHelper.Log("concursoinicio:setControles INICIO setControles, concurso: " & oPC.idPgmConcurso)

        LetraAutoridad = New Font("Myriad Web Pro", 8, FontStyle.Bold)

        LetraPestania = New Font("Myriad Web Pro", 10, FontStyle.Regular)
        Dim LetraNegrita As Font
        LetraNegrita = New Font("Myriad Web Pro", 11, FontStyle.Bold)
        Dim LetraTitulo As Font
        LetraTitulo = New Font("Myriad Web Pro", 11, FontStyle.Regular)

        Dim LetraGrilla As Font
        LetraGrilla = New Font("Myriad Web Pro", 10, FontStyle.Regular)
        Dim BotonAutoridadVisible As Boolean


        ' crea tabJuegos y lo inserta en el formulario
        Tab = Me.grpJuegos.Controls("tabJuegos")
        If Not IsNothing(Tab) Then
            ''For Each TabControl In Tab.TabPages
            ''    Tab.SelectTab(TabControl)
            ''    idJuego = getIdJuegoActual()

            ''    panelPozo = TabControl.Controls("pnlPozoJuego" & idJuego)
            ''    If Not IsNothing(panelPozo) Then
            ''        panelPozo.Visible = False
            ''        grpJuegos.Controls.Add(panelPozo)
            ''    End If
            ''Next

            ' Resguarda panel de apuestas sino se pierde
            For Each TabControl In Tab.TabPages
                Tab.SelectTab(TabControl)
                idJuego = getIdJuegoActual()

                panelApu = TabControl.Controls("pnlApuJuego" & idJuego)
                If Not IsNothing(panelApu) Then
                    panelApu.Visible = False
                    grpJuegos.Controls.Add(panelApu)
                End If
            Next

            Me.grpJuegos.Controls.Remove(Tab)
        End If

        Tab = New TabControl
        Tab.Name = "tabJuegos"
        Tab.Font = LetraTitulo
        Tab.Location = New System.Drawing.Point(2, 19)
        Tab.Width = grpJuegos.Width - 7 'New System.Drawing.Size(634, 374)
        Tab.Height = grpJuegos.Height - 30
        Tab.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top
        Me.grpJuegos.Controls.Add(Tab)
        If oPC.PgmSorteos.Count > 1 Then
            BotonAutoridadVisible = True
        Else
            BotonAutoridadVisible = False
        End If
        Dim verboton As Boolean = False

        ' agrega las pestañas al tabJuegos
        Dim juego_principal As Integer
        juego_principal = oPC.concurso.JuegoPrincipal.Juego.IdJuego
        grpExtracciones.Height = 441
        grpPozos.Visible = False
        For Each oSorteo In oPC.PgmSorteos
            idJuego = oSorteo.idJuego

            ' pestaña
            Pestaña = New TabPage
            Pestaña.BackColor = Color.White
            Pestaña.Font = LetraPestania
            Pestaña.Name = "pstJuego-" & idJuego
            Pestaña.Text = "" & boSorteo.getJuegoDescripcion(oSorteo.idJuego)
            Pestaña.Height = Tab.Height - 10
            Tab.Controls.Add(Pestaña)

            ' ** renglón 0
            Text = New TextBox
            Text.Name = "txtIdPgmSorteo" & idJuego
            Text.Location = New System.Drawing.Point(0, 0)
            Text.Size = New System.Drawing.Size(33, 20)
            Text.Visible = False
            Pestaña.Controls.Add(Text)

            ' ** renglón 1
            Etiqueta = New Label
            Etiqueta.Name = "lblSorteoJuego" & idJuego
            Etiqueta.Text = "Sorteo:"
            Etiqueta.Location = New System.Drawing.Point(41, 15)
            Etiqueta.Size = New System.Drawing.Size(52, 13)
            'Etiqueta.Size = New System.Drawing.Size(52, 20)
            Etiqueta.ForeColor = Color.FromArgb(90, 90, 90)
            Pestaña.Controls.Add(Etiqueta)

            Text = New TextBox
            Text.Name = "txtSorteoJuego" & idJuego
            Text.Location = New System.Drawing.Point(93, 11)
            Text.Size = New System.Drawing.Size(62, 20)
            Text.Font = LetraPestania
            Text.TextAlign = HorizontalAlignment.Right
            Text.Enabled = False
            Text.ForeColor = Color.FromArgb(239, 239, 239)
            Pestaña.Controls.Add(Text)

            Etiqueta = New Label
            Etiqueta.Name = "lblHoraInicioJuego" & idJuego
            Etiqueta.Text = "Hora inicio:"
            Etiqueta.Location = New System.Drawing.Point(160, 15)
            Etiqueta.Size = New System.Drawing.Size(75, 13)
            'Etiqueta.Size = New System.Drawing.Size(100, 20)
            Etiqueta.ForeColor = Color.FromArgb(90, 90, 90)
            Pestaña.Controls.Add(Etiqueta)

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpHoraInicioJuego" & idJuego
            DataPicker.Location = New System.Drawing.Point(240, 12)
            DataPicker.Size = New System.Drawing.Size(60, 20)
            DataPicker.Format = DateTimePickerFormat.Custom
            DataPicker.CustomFormat = "HH:mm"
            DataPicker.ShowUpDown = True
            Pestaña.Controls.Add(DataPicker)

            Etiqueta = New Label
            Etiqueta.Name = "lblHoraFinJuego" & idJuego
            Etiqueta.Text = "Hora fin:"
            Etiqueta.Location = New System.Drawing.Point(272, 15)
            Etiqueta.Size = New System.Drawing.Size(60, 13)
            'Etiqueta.Size = New System.Drawing.Size(100, 20)
            Etiqueta.ForeColor = Color.FromArgb(90, 90, 90)
            Etiqueta.Visible = False
            Pestaña.Controls.Add(Etiqueta)

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpHoraFinJuego" & idJuego
            DataPicker.Location = New System.Drawing.Point(336, 12)
            DataPicker.Size = New System.Drawing.Size(60, 20)
            DataPicker.Format = DateTimePickerFormat.Custom
            DataPicker.CustomFormat = "HH:mm"
            DataPicker.ShowUpDown = True
            DataPicker.Visible = False
            Pestaña.Controls.Add(DataPicker)

            Etiqueta = New Label
            Etiqueta.Name = "lblLocalidadJuego" & idJuego
            Etiqueta.Text = "Localidad:"
            Etiqueta.Location = New System.Drawing.Point(23, 43)
            Etiqueta.TextAlign = ContentAlignment.MiddleLeft
            Etiqueta.Size = New System.Drawing.Size(69, 13)
            'Etiqueta.Size = New System.Drawing.Size(69, 20)
            Etiqueta.ForeColor = Color.FromArgb(90, 90, 90)
            Pestaña.Controls.Add(Etiqueta)

            Text = New TextBox
            Text.Name = "txtLocalidadJuego" & idJuego
            Text.Location = New System.Drawing.Point(93, 40)
            Text.Size = New System.Drawing.Size(175, 20)
            Pestaña.Controls.Add(Text)

            ' ** renglón 2
            Etiqueta = New Label
            Etiqueta.Name = "lblFechaPrescribeJuego" & idJuego
            Etiqueta.Text = "Prescribe el:"
            Etiqueta.Location = New System.Drawing.Point(11, 69)
            Etiqueta.Size = New System.Drawing.Size(82, 13)
            'Etiqueta.Size = New System.Drawing.Size(82, 20)
            Pestaña.Controls.Add(Etiqueta)

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpFechaPrescribeJuego" & idJuego
            DataPicker.Location = New System.Drawing.Point(93, 67)
            DataPicker.Size = New System.Drawing.Size(270, 20)
            DataPicker.Format = DateTimePickerFormat.Long
            DataPicker.CustomFormat = "dd/MM/yyyy"
            DataPicker.ShowUpDown = True
            Pestaña.Controls.Add(DataPicker)

            Etiqueta = New Label
            Etiqueta.Name = "lblHoraPrescribeJuego" & idJuego
            Etiqueta.Text = "Hora:"
            Etiqueta.Location = New System.Drawing.Point(418, 41)
            Etiqueta.Size = New System.Drawing.Size(41, 13)
            'Etiqueta.Size = New System.Drawing.Size(41, 20)
            Etiqueta.Visible = False

            Pestaña.Controls.Add(Etiqueta)

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpHoraPrescribeJuego" & idJuego
            DataPicker.Location = New System.Drawing.Point(460, 67)
            DataPicker.Size = New System.Drawing.Size(60, 20)
            DataPicker.Format = DateTimePickerFormat.Custom
            DataPicker.CustomFormat = "HH:mm"
            DataPicker.ShowUpDown = True
            Pestaña.Controls.Add(DataPicker)

            ' ** renglón 3
            Etiqueta = New Label
            Etiqueta.Name = "lblFechaProximoJuego" & idJuego
            Etiqueta.Text = "Prox. sorteo:"
            Etiqueta.Location = New System.Drawing.Point(8, 101)
            Etiqueta.Size = New System.Drawing.Size(85, 13)
            'Etiqueta.Size = New System.Drawing.Size(85, 20)
            Pestaña.Controls.Add(Etiqueta)

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpFechaProximoJuego" & idJuego
            DataPicker.Location = New System.Drawing.Point(93, 99)
            DataPicker.Size = New System.Drawing.Size(270, 20)
            DataPicker.Format = DateTimePickerFormat.Long
            DataPicker.CustomFormat = "dd/MM/yyyy"
            DataPicker.ShowUpDown = True
            Pestaña.Controls.Add(DataPicker)

            Etiqueta = New Label
            Etiqueta.Name = "lblHoraProximoJuego" & idJuego
            Etiqueta.Text = "Hora:"
            Etiqueta.Location = New System.Drawing.Point(380, 101)
            Etiqueta.Size = New System.Drawing.Size(41, 13)
            'Etiqueta.Size = New System.Drawing.Size(41, 20)
            Pestaña.Controls.Add(Etiqueta)

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpHoraProximoJuego" & idJuego
            DataPicker.Location = New System.Drawing.Point(422, 99)
            DataPicker.Size = New System.Drawing.Size(60, 20)
            DataPicker.Format = DateTimePickerFormat.Custom
            DataPicker.CustomFormat = "HH:mm"
            DataPicker.ShowUpDown = True
            Pestaña.Controls.Add(DataPicker)


            ' ** grilla
            Dim fuente As Font
            fuente = New Font("Myriad Web Pro", 7, FontStyle.Bold)
            Etiqueta = New Label
            Etiqueta.Name = "lblAutoridadJuego" & idJuego
            Etiqueta.Text = "Autoridades:"
            Etiqueta.Location = New System.Drawing.Point(4, 140)
            Etiqueta.Size = New System.Drawing.Size(90, 13)
            fuente = (New Label).Font
            'Etiqueta.Size = New System.Drawing.Size(90, 20)
            Pestaña.Controls.Add(Etiqueta)

            'Etiqueta = Pestaña.Controls(0)
            'MsgBox(Etiqueta.Font.ToString())



            Grilla = New DataGridView
            Grilla.Name = "dgvAutoridadJuego" & idJuego
            Grilla.Font = LetraGrilla
            Grilla.Location = New System.Drawing.Point(4, 157)
            Grilla.Size = New System.Drawing.Size(240, 143)
            Grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect
            Pestaña.Controls.Add(Grilla)


            Boton = New Button
            Boton.Name = "btnAutoridadJuego_Agregar" & idJuego
            Boton.Text = "&AGREGAR"
            Boton.Location = New System.Drawing.Point(4, 302)
            ' ToolTip1.SetToolTip(Boton, "Agrega autoridad")
            'Boton.Size = New System.Drawing.Size(76, 26)
            Boton.Size = New System.Drawing.Size(80, 25)
            Boton.FlatStyle = FlatStyle.Flat
            Boton.BackColor = System.Drawing.SystemColors.Control
            Boton.BackgroundImageLayout = ImageLayout.Stretch
            Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
            Boton.ForeColor = Color.FromArgb(58, 118, 166)
            Boton.Font = LetraAutoridad
            AddHandler Boton.Click, AddressOf btnAutoridadJuegoAgregar_Click
            AddHandler Boton.MouseDown, AddressOf botones_MouseDown
            AddHandler Boton.MouseHover, AddressOf botones_MouseHover
            AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave
            AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
            Pestaña.Controls.Add(Boton)

            Boton = New Button
            Boton.Name = "btnAutoridadJuego_Quitar" & idJuego
            Boton.Text = "&QUITAR"
            Boton.Location = New System.Drawing.Point(90, 302)
            'ToolTip1.SetToolTip(Boton, "Quita autoridad")
            Boton.BackColor = System.Drawing.SystemColors.Control
            'Boton.Size = New System.Drawing.Size(76, 26)
            Boton.Size = New System.Drawing.Size(80, 25)
            Boton.ForeColor = Color.FromArgb(58, 118, 166)
            Boton.FlatStyle = FlatStyle.Flat
            Boton.BackgroundImageLayout = ImageLayout.Stretch
            Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
            Boton.Font = LetraAutoridad
            AddHandler Boton.Click, AddressOf btnAutoridadJuegoQuitar_Click
            AddHandler Boton.MouseDown, AddressOf botones_MouseDown
            AddHandler Boton.MouseHover, AddressOf botones_MouseHover
            AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave
            AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
            Pestaña.Controls.Add(Boton)
            '** boton aplicar al resto
            Boton = New Button
            Boton.Name = "btnAplicarAlResto" & idJuego
            Boton.Text = "&APLICAR AL RESTO"
            Boton.FlatStyle = FlatStyle.Flat
            Boton.BackColor = System.Drawing.SystemColors.Control
            Boton.BackgroundImageLayout = ImageLayout.Stretch
            Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
            Boton.ForeColor = Color.FromArgb(58, 118, 166)
            Boton.Location = New System.Drawing.Point(178, 282)
            'Boton.Size = New System.Drawing.Size(112, 26)
            Boton.Size = New System.Drawing.Size(115, 25)
            Boton.Font = LetraAutoridad

            AddHandler Boton.Click, AddressOf btnAplicarAutoridadAlResto_Click
            AddHandler Boton.MouseDown, AddressOf botones_MouseDown
            AddHandler Boton.MouseHover, AddressOf botones_MouseHover
            AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave
            AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
            'habilta el aplciar al resto solo para la el juego principal
            'Boton.Visible = IIf((BotonAutoridadVisible And idJuego = juego_principal), True, False)
            '** 19/10/2012 inc 2787 se deshabilita
            Boton.Visible = False
            Pestaña.Controls.Add(Boton)

            '*** 21/11/2012 hg GRILLA DE OTRAS JURISDICCIONES PARA LOS JUEGOS TIPO QUINIELA***********

            If boSorteo.esQuiniela(idJuego) Then
                Etiqueta = New Label
                Etiqueta.Name = "lblOtrasJuridisccionesJuego" & idJuego
                Etiqueta.Text = "Jurisdicciones:"
                Etiqueta.Location = New System.Drawing.Point(248, 140)
                Etiqueta.Size = New System.Drawing.Size(95, 13)
                Pestaña.Controls.Add(Etiqueta)

                Grilla = New DataGridView
                Grilla.Name = "dgvJurisdiccionJuego" & idJuego
                Grilla.Font = LetraGrilla
                Grilla.Location = New System.Drawing.Point(248, 157)
                Grilla.Size = New System.Drawing.Size(215, 143)
                Grilla.SelectionMode = DataGridViewSelectionMode.FullRowSelect
                Pestaña.Controls.Add(Grilla)

                Boton = New Button
                Boton.Name = "btnJurisdiccionJuego_Agregar" & idJuego
                Boton.Text = "&AGREGAR"
                Boton.Location = New System.Drawing.Point(248, 302)
                Boton.Visible = True
                'Boton.Size = New System.Drawing.Size(76, 26)
                Boton.Size = New System.Drawing.Size(80, 25)
                Boton.FlatStyle = FlatStyle.Flat
                Boton.BackColor = System.Drawing.SystemColors.Control
                Boton.BackgroundImageLayout = ImageLayout.Stretch
                Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
                Boton.ForeColor = Color.FromArgb(58, 118, 166)
                Boton.Font = LetraAutoridad
                AddHandler Boton.Click, AddressOf btnJurisdiccionJuegoAgregar_Click
                AddHandler Boton.MouseDown, AddressOf botones_MouseDown
                AddHandler Boton.MouseHover, AddressOf botones_MouseHover
                AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave
                AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
                Pestaña.Controls.Add(Boton)

                Boton = New Button
                Boton.Name = "btnJurisdiccionJuego_Quitar" & idJuego
                Boton.Text = "&QUITAR"
                Boton.Location = New System.Drawing.Point(332, 302)
                Boton.Visible = True
                Boton.BackColor = System.Drawing.SystemColors.Control
                'Boton.Size = New System.Drawing.Size(76, 26)
                Boton.Size = New System.Drawing.Size(80, 25)
                Boton.ForeColor = Color.FromArgb(58, 118, 166)
                Boton.FlatStyle = FlatStyle.Flat
                Boton.BackgroundImageLayout = ImageLayout.Stretch
                Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
                Boton.Font = LetraAutoridad
                AddHandler Boton.Click, AddressOf btnJurisdiccionJuegoQuitar_Click
                AddHandler Boton.MouseDown, AddressOf botones_MouseDown
                AddHandler Boton.MouseHover, AddressOf botones_MouseHover
                AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave
                AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
                Pestaña.Controls.Add(Boton)

            End If
            '*** FIN GRILLA OTRAS JURIDICCIONES
            '*** 26/02/2015 GRILLA DE APUESTAS PARA LOS JUEGOS TIPO POCEADOS ***********
            ' si existe un panel con apuestas para el juego lo inserta en la pestaña.
            ' eso se da cuando el juego tiene pozos
            If oSorteo.pozos.Count <> 0 Then
                If idJuego <> 50 And idJuego <> 51 Then
                    '******** Apuestas
                    panelApu = grpJuegos.Controls("pnlApuJuego" & idJuego)
                    panelApu.Visible = True
                    panelApu.AutoScroll = True
                    panelApu.Location = New System.Drawing.Point(245, 138)
                    fuente = New Font("Myriad Web Pro", 10, FontStyle.Regular)

                    panelApu.Font = fuente
                    Pestaña.Controls.Add(panelApu)
                    Try
                        Etiqueta = panelApu.Controls("lblTitApuJue" & idJuego)
                        Etiqueta.Font = fuente
                        Etiqueta = panelApu.Controls("lblTotApuJue" & idJuego)
                        Etiqueta.Font = fuente
                        panelApu.Controls("txtTotApu" & idJuego).Font = fuente
                    Catch exxx As Exception
                    End Try
                    '********
                    ' remueve las pestañas de sorteos adicionales
                    If idJuego = 4 Then ' quini 6
                        If oPC.concurso.IdConcurso = 18 Then ' tiene sorteo adicional
                            If Not IsNothing(oPstApu405) Then
                                Me.tabApuJuego4.TabPages.Add(oPstApu405)
                                oPstApu405 = Nothing
                            End If
                        Else ' no tiene sorteo adicional
                            If IsNothing(oPst405) Then
                                Try
                                    oPstApu405 = Me.tabApuJuego4.TabPages("pstApu405")
                                    Me.Controls.Remove(tabApuJuego4.TabPages("pstApu405"))
                                    tabApuJuego4.TabPages("pstApu405").Dispose()
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                    If idJuego = 13 Then ' brinco
                        If oPC.concurso.IdConcurso = 19 Then ' tiene sorteo adicional
                            If tabApuJuego13.TabPages.Count = 1 Then
                                If Not IsNothing(oPstApu1305) Then
                                    tabApuJuego13.TabPages.Add(oPstApu1305)
                                    oPstApu1305 = Nothing
                                End If
                            End If
                        Else ' no tiene sorteo adicional
                            If tabApuJuego13.TabPages.Count = 2 Then
                                Try
                                    oPstApu1305 = tabApuJuego13.TabPages("pstApu1305")
                                    Me.Controls.Remove(tabApuJuego13.TabPages("pstApu1305"))
                                    tabApuJuego13.TabPages("pstApu1305").Dispose()
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                End If
            End If
            '*** FIN GRILLA APUESTAS

            '*** GRUPO POZOS
            ' si existe un panel con apuestas para el juego lo inserta en la pestaña.
            ' eso se da cuando el juego tiene pozos
            If oSorteo.pozos.Count <> 0 Then
                grpExtracciones.Height = 170
                grpPozos.Visible = True
                If idJuego <> 50 And idJuego <> 51 Then
                    '******** Pozos
                    panelPozo = grpPozos.Controls("pnlPozoJuego" & idJuego)
                    panelPozo.Visible = True
                    panelPozo.AutoScroll = True
                    panelPozo.Location = New System.Drawing.Point(250, 19)
                    'panelPozo.Font = fuente

                    'Pestaña.Controls.Add(panelApu)
                    '********
                    ' remueve las pestañas de sorteos adicionales
                    If idJuego = 4 Then ' quini 6
                        If oPC.concurso.IdConcurso = 18 Then ' tiene sorteo adicional
                            If Not IsNothing(oPst405) Then
                                Me.tabPozoJuego4.TabPages.Add(oPst405)
                                oPst405 = Nothing
                            End If
                        Else ' no tiene sorteo adicional
                            If IsNothing(oPst405) Then
                                Try
                                    oPst405 = Me.tabPozoJuego4.TabPages("pst405")
                                    Me.Controls.Remove(tabPozoJuego4.TabPages("pst405"))
                                    tabPozoJuego4.TabPages("pst405").Dispose()
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                    If idJuego = 13 Then ' brinco
                        If oPC.concurso.IdConcurso = 19 Then ' tiene sorteo adicional
                            If tabPozoJuego13.TabPages.Count = 1 Then
                                If Not IsNothing(oPst1305) Then
                                    tabPozoJuego13.TabPages.Add(oPst1305)
                                    oPst1305 = Nothing
                                End If
                            End If
                        Else ' no tiene sorteo adicional
                            If tabPozoJuego13.TabPages.Count = 2 Then
                                Try
                                    oPst1305 = tabPozoJuego13.TabPages("pst1305")
                                    Me.Controls.Remove(tabPozoJuego13.TabPages("pst1305"))
                                    tabPozoJuego13.TabPages("pst1305").Dispose()
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                    '********
                    '******** Pozos Reales
                    panelPozoReal = grpPozos.Controls("pnlPozoRealJuego" & idJuego)
                    panelPozoReal.Visible = True
                    panelPozoReal.AutoScroll = True
                    panelPozoReal.Location = New System.Drawing.Point(6, 19)
                    'panelPozoReal.Font = fuente
                    'Pestaña.Controls.Add(panelApu)
                    '********
                    ' remueve las pestañas de sorteos adicionales
                    If idJuego = 4 Then ' quini 6
                        If oPC.concurso.IdConcurso = 18 Then ' tiene sorteo adicional
                            If Not IsNothing(oPstPozoReal405) Then
                                Me.tabPozoRealJuego4.TabPages.Add(oPstPozoReal405)
                                oPstPozoReal405 = Nothing
                            End If
                        Else ' no tiene sorteo adicional
                            If IsNothing(oPstPozoReal405) Then
                                Try
                                    oPstPozoReal405 = Me.tabPozoRealJuego4.TabPages("pstPozoReal405")
                                    Me.Controls.Remove(tabPozoRealJuego4.TabPages("pstPozoReal405"))
                                    tabPozoRealJuego4.TabPages("pstPozoReal405").Dispose()
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                    If idJuego = 13 Then ' brinco
                        If oPC.concurso.IdConcurso = 19 Then ' tiene sorteo adicional
                            If tabPozoRealJuego13.TabPages.Count = 1 Then
                                If Not IsNothing(oPstPozoReal1305) Then
                                    tabPozoRealJuego13.TabPages.Add(oPstPozoReal1305)
                                    oPstPozoReal1305 = Nothing
                                End If
                            End If
                        Else ' no tiene sorteo adicional
                            If tabPozoRealJuego13.TabPages.Count = 2 Then
                                Try
                                    oPstPozoReal1305 = tabPozoRealJuego13.TabPages("pstPozoReal1305")
                                    Me.Controls.Remove(tabPozoRealJuego13.TabPages("pstPozoReal1305"))
                                    tabPozoRealJuego13.TabPages("pstPozoReal1305").Dispose()
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                    '********
                End If
            End If

            ' '' si existe un panel con pozos para el juego lo inserta en la pestaña
            ''If oSorteo.pozos.Count <> 0 Then
            ''    If idJuego <> 50 And idJuego <> 51 Then
            ''        panelPozo = grpJuegos.Controls("pnlPozoJuego" & idJuego)
            ''        panelPozo.Visible = True
            ''        panelPozo.AutoScroll = True
            ''        panelPozo.Location = New System.Drawing.Point(287, 120)
            ''        Pestaña.Controls.Add(panelPozo)
            ''        '********
            ''        ' remueve las pestañas de sorteos adicionales
            ''        If idJuego = 4 Then ' quini 6
            ''            If oPC.concurso.IdConcurso = 18 Then ' tiene sorteo adicional
            ''                If Not IsNothing(oPst405) Then
            ''                    Me.tabPozoJuego4.TabPages.Add(oPst405)
            ''                    oPst405 = Nothing
            ''                End If
            ''            Else ' no tiene sorteo adicional
            ''                If IsNothing(oPst405) Then
            ''                    oPst405 = Me.tabPozoJuego4.TabPages("pst405")
            ''                    Me.Controls.Remove(tabPozoJuego4.TabPages("pst405"))
            ''                    tabPozoJuego4.TabPages("pst405").Dispose()
            ''                End If
            ''            End If
            ''        End If

            ''        If idJuego = 13 Then ' brinco
            ''            If oPC.concurso.IdConcurso = 19 Then ' tiene sorteo adicional
            ''                If tabPozoJuego13.TabPages.Count = 1 Then
            ''                    If Not IsNothing(oPst1305) Then
            ''                        tabPozoJuego13.TabPages.Add(oPst1305)
            ''                        oPst1305 = Nothing
            ''                    End If
            ''                End If
            ''            Else ' no tiene sorteo adicional
            ''                If tabPozoJuego13.TabPages.Count = 2 Then
            ''                    oPst1305 = tabPozoJuego13.TabPages("pst1305")
            ''                    Me.Controls.Remove(tabPozoJuego13.TabPages("pst1305"))
            ''                    tabPozoJuego13.TabPages("pst1305").Dispose()
            ''                End If
            ''            End If
            ''        End If
            ''        '********

            ''    End If
            ''End If
        Next

        '**** Extracciones *** 
        Try
            FileSystemHelper.Log("concursoinicio:setControles  INICIO armado pestañas datos del modelo de extracción, concurso: " & oPC.idPgmConcurso)
            ''If idJuego <> 4 And idJuego <> 13 And idJuego <> 30 Then
            ''    grpExtracciones.Height = 441
            ''    grpPozos.Visible = False
            ''Else
            ''    grpExtracciones.Height = 170
            ''    grpPozos.Visible = True
            ''End If
            Tab = Me.grpExtracciones.Controls("tabExtracciones")
            If Not IsNothing(Tab) Then
                Me.grpExtracciones.Controls.Remove(Tab)
            End If
            'MsgBox(grpExtracciones.Height)

            Tab = New TabControl
            Tab.Name = "tabExtracciones"
            Tab.Font = LetraTitulo
            Tab.Location = New System.Drawing.Point(6, 19)

            Tab.Width = grpExtracciones.Width - 10
            Tab.Height = grpExtracciones.Height - 30
            Tab.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Bottom Or AnchorStyles.Top
            grpExtracciones.Controls.Add(Tab)

            ' agrega las pestañas al tabExtracciones
            For Each oExtr In oPC.Extracciones
                idExtr = oExtr.idExtraccionesCAB

                ' pestaña
                Pestaña = New TabPage
                Pestaña.BackColor = Color.White
                Pestaña.Font = LetraPestania
                Pestaña.Name = "pstExtracciones-" & idExtr
                Pestaña.Width = 198
                Pestaña.Text = IIf(oExtr.ModeloExtraccionesDET.Obligatoria, "(R) ", "(O) ") & _
                                oExtr.Titulo
                'ToolTip1.SetToolTip(Pestaña, Pestaña.Text)
                Tab.Controls.Add(Pestaña)

                '** renglon 1
                ''Etiqueta = New Label
                ''Etiqueta.Name = "lblTope" & idExtr
                ''Etiqueta.Text = "Tope:"
                ''Etiqueta.Font = LetraPestania
                ''Etiqueta.ForeColor = Color.FromArgb(110, 114, 116)
                ''Etiqueta.Location = New System.Drawing.Point(4, 15)
                ''Etiqueta.Size = New System.Drawing.Size(47, 13)
                ''Pestaña.Controls.Add(Etiqueta)

                Text = New TextBox
                Text.Name = "txtTope" & idExtr
                ''Text.Location = New System.Drawing.Point(52, 13)
                Text.Location = New System.Drawing.Point(4, 10)
                Text.Size = New System.Drawing.Size(120, 20)
                Text.Enabled = False
                Pestaña.Controls.Add(Text)

                Etiqueta = New Label
                Etiqueta.Name = "lblCantExtracciones" & idExtr
                Etiqueta.Text = "Cant:"
                Etiqueta.Font = LetraPestania
                Etiqueta.ForeColor = Color.FromArgb(110, 114, 116)
                ''Etiqueta.Location = New System.Drawing.Point(200, 15)
                Etiqueta.Location = New System.Drawing.Point(123, 12)
                Etiqueta.Size = New System.Drawing.Size(40, 13)
                Pestaña.Controls.Add(Etiqueta)

                Text = New TextBox
                Text.Name = "txtCantExtracciones" & idExtr
                Text.TextAlign = HorizontalAlignment.Right
                ''                Text.Location = New System.Drawing.Point(242, 13)
                Text.Location = New System.Drawing.Point(162, 10)
                Text.Size = New System.Drawing.Size(33, 20)
                Text.Enabled = False
                Pestaña.Controls.Add(Text)

                Etiqueta = New Label
                Etiqueta.Name = "lblCifras" & idExtr
                Etiqueta.Text = "Cifras:"
                Etiqueta.Font = LetraPestania
                Etiqueta.ForeColor = Color.FromArgb(110, 114, 116)
                ''Etiqueta.Location = New System.Drawing.Point(324, 15)
                Etiqueta.Location = New System.Drawing.Point(192, 12)
                Etiqueta.Size = New System.Drawing.Size(44, 13)
                Pestaña.Controls.Add(Etiqueta)

                Text = New TextBox
                Text.Name = "txtCifras" & idExtr
                Text.TextAlign = HorizontalAlignment.Right
                ''                Text.Location = New System.Drawing.Point(368, 13)
                Text.Location = New System.Drawing.Point(236, 10)
                Text.Size = New System.Drawing.Size(33, 20)
                Text.Enabled = False
                Pestaña.Controls.Add(Text)

                Etiqueta = New Label
                Etiqueta.Name = "lblValorMin" & idExtr
                Etiqueta.Text = "Mín:"
                Etiqueta.Font = LetraPestania
                Etiqueta.ForeColor = Color.FromArgb(110, 114, 116)
                ''Etiqueta.Location = New System.Drawing.Point(16, 45)
                Etiqueta.Location = New System.Drawing.Point(269, 12)
                Etiqueta.Size = New System.Drawing.Size(37, 13)
                Pestaña.Controls.Add(Etiqueta)

                Text = New TextBox
                Text.Name = "txtValorMin" & idExtr
                Text.TextAlign = HorizontalAlignment.Right
                ''Text.Location = New System.Drawing.Point(106, 43)
                Text.Location = New System.Drawing.Point(306, 10)
                Text.Size = New System.Drawing.Size(30, 20)
                Text.Enabled = False
                Pestaña.Controls.Add(Text)

                Etiqueta = New Label
                Etiqueta.Name = "lblValorMax" & idExtr
                Etiqueta.Text = "Máx:"
                Etiqueta.Font = LetraPestania
                Etiqueta.ForeColor = Color.FromArgb(110, 114, 116)
                ''Etiqueta.Location = New System.Drawing.Point(275, 45)
                Etiqueta.Location = New System.Drawing.Point(336, 12)
                Etiqueta.Size = New System.Drawing.Size(40, 13)
                Pestaña.Controls.Add(Etiqueta)

                Text = New TextBox
                Text.Name = "txtValorMax" & idExtr
                Text.TextAlign = HorizontalAlignment.Right
                Text.Location = New System.Drawing.Point(376, 10)
                Text.Size = New System.Drawing.Size(33, 20)
                Text.Enabled = False
                Pestaña.Controls.Add(Text)

                ' ** renglón 2
                Etiqueta = New Label
                Etiqueta.Name = "lblMetodoIng" & idExtr
                Etiqueta.Text = "Mét. de ingreso:"
                Etiqueta.Font = LetraPestania
                Etiqueta.ForeColor = Color.FromArgb(110, 114, 116)
                Etiqueta.Location = New System.Drawing.Point(2, 40)
                Etiqueta.Size = New System.Drawing.Size(103, 13)
                Pestaña.Controls.Add(Etiqueta)

                Text = New TextBox
                Text.Name = "txtMetodoIng" & idExtr
                Text.Location = New System.Drawing.Point(106, 41)
                Text.Size = New System.Drawing.Size(285, 20)
                Text.Enabled = False
                Pestaña.Controls.Add(Text)

                ' ** renglón 3
                Check = New CheckBox
                Check.Name = "chkSorteaPosicion" & idExtr
                Check.Text = "Sortea posición"
                Check.Location = New System.Drawing.Point(106, 65)
                Check.Size = New System.Drawing.Size(140, 17)
                Check.Font = LetraPestania
                Check.Enabled = False
                Pestaña.Controls.Add(Check)

                Check = New CheckBox
                Check.Name = "chkAdmiteRepetidos" & idExtr
                Check.Text = "Admite repetidos"
                ''Check.Location = New System.Drawing.Point(265, 110)
                Check.Location = New System.Drawing.Point(265, 65)
                Check.Size = New System.Drawing.Size(150, 17)
                Check.Font = LetraPestania
                Check.Enabled = False
                Pestaña.Controls.Add(Check)

                ' ** renglón 4
                Etiqueta = New Label
                Etiqueta.Name = "lblCriterioFin" & idExtr
                Etiqueta.Text = "Criterio de fin:"
                Etiqueta.Font = LetraPestania
                Etiqueta.ForeColor = Color.FromArgb(110, 114, 116)
                Etiqueta.Location = New System.Drawing.Point(13, 85)
                Etiqueta.Size = New System.Drawing.Size(92, 13)
                Pestaña.Controls.Add(Etiqueta)

                Text = New TextBox
                Text.Name = "txtCriterioFin" & idExtr
                Text.Location = New System.Drawing.Point(105, 85)
                Text.Size = New System.Drawing.Size(285, 20)
                Text.TextAlign = HorizontalAlignment.Left
                Text.Enabled = False
                Pestaña.Controls.Add(Text)
            Next
            FileSystemHelper.Log("concursoinicio:setControles  FIN OK armado pestañas datos del modelo de extracción, concurso: " & oPC.idPgmConcurso)
        Catch ex As Exception
            FileSystemHelper.Log("concursoinicio:setControles  FIN ERROR PASANTE en armado pestañas datos del modelo de extracción, parte derecha de la pantalla de Inicio con datos no relevantes., concurso: " & oPC.idPgmConcurso)
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
        '**** FIN GROUP EXTRACCIONES
        FileSystemHelper.Log("concursoinicio:setControles FIN OK setControles, concurso: " & oPC.idPgmConcurso)
    End Sub

    ' presenta en el formulario los valores de la entidad oPC
    Private Sub setValores()
        Dim tab As TabControl
        Dim DataPicker As DateTimePicker
        Dim Boton As Button
        Dim Check As CheckBox
        Dim habilitarControl As Boolean
        Dim panelPozo As Control
        Dim verBoton As Boolean
        Dim _label As Label
        Dim juegoDAl As New JuegoDAL

        FileSystemHelper.Log("concursoinicio:setValores INICIO setValores, concurso: " & oPC.idPgmConcurso)
        habilitarControl = IIf(oPC.estadoPgmConcurso = 30, False, True)

        tab = Me.Controls("grpGeneral").Controls("grpJuegos").Controls("tabJuegos")

        btncriteriosPozos.Visible = False

        ' valores de las pestañas tabJuegos
        Dim juego_principal As Integer
        juego_principal = oPC.concurso.JuegoPrincipal.Juego.IdJuego
        grpExtracciones.Height = 441
        grpPozos.Visible = False
        For Each oSorteo In oPC.PgmSorteos
            'los botons de quitar y agregar autoridad solo se habilitan pra el juego principal
            If oSorteo.idJuego = juego_principal Then
                verBoton = True
            Else
                verBoton = False
            End If
            ' localiza y hace activa la pestaña
            tab.SelectTab("pstJuego-" & oSorteo.idJuego)

            ' ** renglón 0 
            getControlJ("txtIdPgmSorteo").Text = oSorteo.idPgmSorteo

            ' ** renglón 1
            getControlJ("txtSorteoJuego").Text = oSorteo.nroSorteo
            'getControlJ("txtSorteoJuego").Enabled = habilitarControl

            DataPicker = getControlJ("dtpHoraInicioJuego")
            If oSorteo.fechaHoraIniReal = "01/01/1999" Then
                DataPicker.Value = oSorteo.fechaHora
            Else
                DataPicker.Value = oSorteo.fechaHoraIniReal
            End If
            DataPicker.Enabled = habilitarControl

            DataPicker = getControlJ("dtpHoraFinJuego")
            DataPicker.Value = oSorteo.fechaHoraFinReal
            DataPicker.Enabled = habilitarControl

            getControlJ("txtLocalidadJuego").Text = oSorteo.localidad
            getControlJ("txtLocalidadJuego").Enabled = habilitarControl

            ' ** renglón 2
            DataPicker = getControlJ("dtpFechaPrescribeJuego")
            DataPicker.Value = oSorteo.fechaHoraPrescripcion
            DataPicker.Enabled = habilitarControl

            DataPicker = getControlJ("dtpHoraPrescribeJuego")
            DataPicker.Value = oSorteo.fechaHoraPrescripcion
            DataPicker.Enabled = habilitarControl
            DataPicker.Visible = False ' RL: 21-08-2012 - Incid 2737: no mostrar hora prescripcion

            '_label = getControlJ("lblHoraPrescribeJuego")
            '_label.Visible = False

            ' ** renglón 3
            DataPicker = getControlJ("dtpFechaProximoJuego")
            DataPicker.Value = oSorteo.fechaHoraProximo
            DataPicker.Enabled = habilitarControl

            DataPicker = getControlJ("dtpHoraProximoJuego")
            DataPicker.Value = oSorteo.fechaHoraProximo
            DataPicker.Enabled = habilitarControl

            ' ** grilla autoridades
            Dim boAutoridad As New AutoridadBO
            setGrillaAutoridad()

            Boton = getControlJ("btnAutoridadJuego_Agregar")
            If habilitarControl Then
                'por mas que este habilitado el concurso,tiene que controlar qu sea el juego principal
                Boton.Visible = verBoton
            Else

                Boton.Visible = habilitarControl
            End If


            Boton = getControlJ("btnAutoridadJuego_Quitar")
            If habilitarControl Then
                Boton.Visible = verBoton
            Else
                Boton.Visible = habilitarControl
            End If

            ' ** apuestas
            'panelPozo = Me.Controls("grpGeneral").Controls("grpJuegos").Controls("pnlApuJuego" & oSorteo.idJuego)
            panelPozo = getControlJ("pnlApuJuego")
            'panelPozo = tab.SelectedTab.Controls("pnlPozoJuego" & oSorteo.idJuego)
            If Not IsNothing(panelPozo) Then
                setApuestas()
            End If

            If oSorteo.pozos.Count <> 0 Then
                ' ** pozos
                panelPozo = Me.Controls("grpGeneral").Controls("grpPozos").Controls("pnlPozoJuego" & oSorteo.idJuego)
                'panelPozo = tab.SelectedTab.Controls("pnlPozoJuego" & oSorteo.idJuego)
                If Not IsNothing(panelPozo) Then
                    grpExtracciones.Height = 170
                    grpPozos.Visible = True
                    setPozo()
                End If

                ' ** pozos reales
                panelPozo = Me.Controls("grpGeneral").Controls("grpPozos").Controls("pnlPozoRealJuego" & oSorteo.idJuego)
                'panelPozo = tab.SelectedTab.Controls("pnlPozoJuego" & oSorteo.idJuego)
                If Not IsNothing(panelPozo) Then
                    setPozoReal()
                End If
                If (oSorteo.idJuego = 4 Or oSorteo.idJuego = 13) Then
                    btncriteriosPozos.Visible = True
                End If
            End If

            ' ** grilla Jurisdicciones
            If juegoDAl.esQuiniela(oSorteo.idJuego) Then
                setGrillaJurisdicciones()

                Boton = getControlJ("btnJurisdiccionJuego_Agregar")
                Boton.Visible = True

                Boton = getControlJ("btnJurisdiccionJuego_Quitar")
                Boton.Visible = True
            End If
        Next
        tab.SelectTab(0)

        tab = Me.Controls("grpGeneral").Controls("grpExtracciones").Controls("tabExtracciones")
        ''If juego_principal <> 4 And juego_principal <> 13 And juego_principal <> 30 Then
        ''    grpExtracciones.Height = 441
        ''    grpPozos.Visible = False
        ''Else
        ''    grpExtracciones.Height = 170
        ''    grpPozos.Visible = True
        ''End If
        For Each oExtr In oPC.Extracciones
            ' localiza y hace activa la pestaña
            tab.SelectTab("pstExtracciones-" & oExtr.idExtraccionesCAB)

            ' ** renglón 1
            getControlE("txtCantExtracciones").Text = oExtr.ModeloExtraccionesDET.cantExtractos
            Check = getControlE("chkSorteaPosicion")
            Check.Checked = oExtr.ModeloExtraccionesDET.sorteaPosicion

            Check = getControlE("chkAdmiteRepetidos")
            Check.Checked = oExtr.ModeloExtraccionesDET.AdmiteRepetidos

            getControlE("txtTope").Text = oExtr.ModeloExtraccionesDET.tipoTope.descripcion

            ' ** renglón 2
            getControlE("txtCifras").Text = oExtr.ModeloExtraccionesDET.cantCifras
            getControlE("txtValorMin").Text = oExtr.ModeloExtraccionesDET.valorMinimo
            getControlE("txtValorMax").Text = oExtr.ModeloExtraccionesDET.valorMaximo

            ' ** renglón 3
            getControlE("txtCriterioFin").Text = oExtr.ModeloExtraccionesDET.criterioFinExtraccion.Nombre

            ' ** renglón 4
            getControlE("txtMetodoIng").Text = oExtr.ModeloExtraccionesDET.metodoIngreso.Nombre
        Next
        tab.SelectTab(0)
        FileSystemHelper.Log("concursoinicio:setValores FIN OK setValores, concurso: " & oPC.idPgmConcurso)
    End Sub

    ' carga los valores del formulario en la entidad oPC
    Private Sub getValores()
        Dim tab As TabControl

        tab = Me.Controls("grpGeneral").Controls("grpJuegos").Controls("tabJuegos")

        ' actualiza los valores de la entidad
        For Each oSorteo In oPC.PgmSorteos
            ' hace activa la pestaña
            tab.SelectTab("pstJuego-" & oSorteo.idJuego)

            ' ** renglón 1
            'hg aca concatenar la fecha del concurso mas hora de dtp 
            'en fecha ini y fecha fin
            Dim _hora As String
            Dim _HoraControl As String
            'fecha del concurso
            _hora = dtpFechaConcurso.Value.ToShortDateString

            'hora de inicio del juego
            _HoraControl = getControlJ("dtpHoraInicioJuego").Text

            oSorteo.fechaHora = _hora & " " & _HoraControl

            oSorteo.fechaHoraIniReal = _hora & " " & _HoraControl   'getControlJ("dtpHoraInicioJuego").Text

            'hora fin de juego
            _HoraControl = getControlJ("dtpHoraFinJuego").Text
            oSorteo.fechaHoraFinReal = _hora & " " & _HoraControl

            oSorteo.localidad = getControlJ("txtLocalidadJuego").Text

            ' ** renglón 2
            oSorteo.fechaHoraPrescripcion = getControlJ("dtpFechaPrescribeJuego").Text & " " & getControlJ("dtpHoraPrescribeJuego").Text

            ' ** renglón 3
            oSorteo.fechaHoraProximo = getControlJ("dtpFechaProximoJuego").Text & " " & getControlJ("dtpHoraProximoJuego").Text

            ' ** pozos
            ' ?? no se guardan acá?
            'panelPozo = TAB.SelectedTab.Controls("pnlPozoJuego" & oSorteo.idJuego)
            'If Not IsNothing(panelPozo) Then
            'setPozo()
            'End If
        Next
    End Sub

    ' retorna el control solicitado del tabPage juego actual
    Private Function getControlJ(ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Integer

        tab = Me.grpJuegos.Controls("tabJuegos")
        mAux = Split(tab.SelectedTab.Name, "-")
        idJuego = mAux(1)

        nombreCtrl &= idJuego
        Return tab.SelectedTab.Controls(nombreCtrl)
    End Function

    ' retorna el control solicitado del tabPage extracciones actual
    Private Function getControlE(ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Long

        tab = Me.grpExtracciones.Controls("tabExtracciones")

        mAux = Split(tab.SelectedTab.Name, "-")
        idJuego = mAux(1)

        nombreCtrl &= idJuego
        Return tab.SelectedTab.Controls(nombreCtrl)
    End Function

    ' retorna el control solicitado del tabPage pozo (inlcuido en tab Juego) actual
    Private Function getControlTabGP(ByVal pst As String, ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Long

        'tab = Me.grpPozos.Controls("tabJuegos")
        If nombreCtrl.StartsWith("txtTotPozo") Then
            mAux = Split(nombreCtrl, "txtTotPozo")
            If nombreCtrl.ToString().Substring(10, 1) = "4" Then
                idJuego = nombreCtrl.ToString().Substring(10, 1)
            Else
                idJuego = nombreCtrl.ToString().Substring(10, 2)
            End If
            Return Me.grpPozos.Controls("pnlPozoJuego" & idJuego).Controls(nombreCtrl)
        Else
            mAux = Split(nombreCtrl, "txtPozo")
            If nombreCtrl.ToString().Substring(3, 1) = "4" Then
                idJuego = nombreCtrl.ToString().Substring(3, 1)
            Else
                idJuego = nombreCtrl.ToString().Substring(3, 2)
            End If

            tab = Me.grpPozos.Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
            Return tab.TabPages(pst).Controls(nombreCtrl)
        End If

        'tab = grpJuegos.Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)


        'Return tab.TabPages(pst).Controls(nombreCtrl)
    End Function

    ' retorna el control solicitado del tabPage pozo (inlcuido en tab Juego) actual
    Private Function getControlTabGPR(ByVal pst As String, ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Long

        'tab = Me.grpPozos.Controls("tabJuegos")

        If nombreCtrl.StartsWith("txtTotPozoReal") Then
            mAux = Split(nombreCtrl, "txtTotPozoReal")
            If nombreCtrl.ToString().Substring(14, 1) = "4" Then
                idJuego = nombreCtrl.ToString().Substring(14, 1)
            Else
                idJuego = nombreCtrl.ToString().Substring(14, 2)
            End If

            Return Me.grpPozos.Controls("pnlPozoRealJuego" & idJuego).Controls(nombreCtrl)
        Else
            mAux = Split(nombreCtrl, "txtPozoReal")
            If nombreCtrl.ToString().Substring(11, 1) = "4" Then
                idJuego = nombreCtrl.ToString().Substring(11, 1)
            Else
                idJuego = nombreCtrl.ToString().Substring(11, 2)
            End If

            tab = Me.grpPozos.Controls("pnlPozoRealJuego" & idJuego).Controls("tabPozoRealJuego" & idJuego)
            Return tab.TabPages(pst).Controls(nombreCtrl)
        End If


        ''If nombreCtrl.StartsWith("txtTotPozoReal") Then
        ''    mAux = Split(nombreCtrl, "txtTotPozoReal")
        ''    idJuego = nombreCtrl.ToString().Substring(14, 2)
        ''Else
        ''    mAux = Split(nombreCtrl, "txtPozoReal")
        ''    idJuego = nombreCtrl.ToString().Substring(11, 2)
        ''End If

        ' ''tab = grpJuegos.Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
        ''tab = Me.grpPozos.Controls("pnlPozoRealJuego" & idJuego).Controls("tabPozoRealJuego" & idJuego)

        ''Return tab.TabPages(pst).Controls(nombreCtrl)
    End Function

    ' retorna el control solicitado del tabPage pozo (inlcuido en tab Juego) actual
    Private Function getControlJA(ByVal pst As String, ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Long


        tab = Me.grpJuegos.Controls("tabJuegos")
        mAux = Split(tab.SelectedTab.Name, "-")
        idJuego = mAux(1)

        If nombreCtrl.StartsWith("txtTotApu") Then
            Return tab.SelectedTab.Controls("pnlApuJuego" & idJuego).Controls(nombreCtrl)
        Else
            tab = tab.SelectedTab.Controls("pnlApuJuego" & idJuego).Controls("tabApuJuego" & idJuego)
            Return tab.TabPages(pst).Controls(nombreCtrl)
        End If



        ' ''tab = grpJuegos.Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
        ''tab = tab.SelectedTab.Controls("pnlApuJuego" & idJuego).Controls("tabApuJuego" & idJuego)

        ''Return tab.TabPages(pst).Controls(nombreCtrl)
    End Function

    ' retorna el control solicitado del tabPage pozo (inlcuido en tab Juego) actual
    Private Function getControlJP(ByVal pst As String, ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Long

        tab = Me.grpJuegos.Controls("tabJuegos")
        mAux = Split(tab.SelectedTab.Name, "-")
        idJuego = mAux(1)

        'tab = grpJuegos.Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
        tab = tab.SelectedTab.Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)

        Return tab.TabPages(pst).Controls(nombreCtrl)
    End Function

    Private Function getIdJuegoActual() As Int16
        Dim tab As TabControl
        Dim mNombre As Array

        ' localiza el tab de juego seleccionado
        tab = Me.Controls("grpGeneral").Controls("grpJuegos").Controls("tabJuegos")
        mNombre = Split(tab.SelectedTab.Name, "-")

        Return mNombre(1)
    End Function

    Private Function getNroSorteoActual() As Integer
        Return Integer.Parse(getControlJ("txtSorteoJuego").Text)
    End Function

    Private Sub autoridadJuegoAgregar()
        Dim frmAutoridadABM As New frmAutoridadABM
        Dim boAutoridad As New AutoridadBO

        Try
            'dispara el formulario
            frmAutoridadABM.Modo = "CI"
            frmAutoridadABM.idPgmSorteo = getControlJ("txtIdPgmSorteo").Text
            frmAutoridadABM.idJuego = getIdJuegoActual()
            frmAutoridadABM.ShowDialog()

            ' recarga la grilla 
            setGrillaAutoridad()

            frmAutoridadABM = Nothing
            '** 23/07/2012  hg si tiene juegos dependientes,el aplicar al resto se hace automaticamente
            If oPC.PgmSorteos.Count > 1 Then
                AplicarAutoridaAlResto()
            End If

        Catch ex As Exception
            MsgBox("Problema al intentar agregar la autoridad: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub autoridadJuegoQuitar()
        Dim dgv As DataGridView
        Dim boAutoridad = New AutoridadBO
        Dim oAutoridad = New Autoridad
        Dim _idPgmSorteo As Integer

        dgv = getControlJ("dgvAutoridadJuego")
        If Not dgv.CurrentRow Is Nothing Then
            If MsgBox("¿Está seguro que desea quitar la autoridad del sorteo? ", MsgBoxStyle.YesNo, MDIContenedor.Text) = Windows.Forms.DialogResult.Yes Then
                oAutoridad = dgv.CurrentRow.DataBoundItem
                _idPgmSorteo = getControlJ("txtIdPgmSorteo").Text
                boAutoridad.eliminarAutoridad_PgmSorteo(oAutoridad, _idPgmSorteo)

                setGrillaAutoridad()
                '** 23/07/2012  hg si tiene juegos dependientes,el aplicar al resto se hace automaticamente
                If oPC.PgmSorteos.Count > 1 Then
                    AplicarAutoridaAlResto()
                End If
            End If
        End If
    End Sub

    Public Sub setGrillaAutoridad(Optional ByVal pidpgmsorteo As Integer = -1)
        Dim dgv As DataGridView
        Dim boAutoridad = New AutoridadBO
        Dim boConcurso As New PgmConcursoBO
        Dim listaJuegoAutoridades As New ListaOrdenada(Of libEntities.Entities.Autoridad)
        Dim _idpgmSorteo As Long
        dgv = getControlJ("dgvAutoridadJuego")
        dgv.Columns.Clear()

        dgv.EditMode = DataGridViewEditMode.EditProgrammatically
        dgv.AllowUserToOrderColumns = False
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToResizeColumns = False
        dgv.AllowUserToResizeRows = False
        dgv.AutoGenerateColumns = False
        dgv.RowHeadersVisible = False
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        If pidpgmsorteo < 0 Then
            _idpgmSorteo = getControlJ("txtIdPgmSorteo").Text
            listaJuegoAutoridades = boAutoridad.getAutoridad_PgmSorteo(getControlJ("txtIdPgmSorteo").Text)
            dgv.DataSource = listaJuegoAutoridades 'boAutoridad.getAutoridad_PgmSorteo(getControlJ("txtIdPgmSorteo").Text)
        Else
            _idpgmSorteo = pidpgmsorteo
            listaJuegoAutoridades = boAutoridad.getAutoridad_PgmSorteo(pidpgmsorteo)
            dgv.DataSource = listaJuegoAutoridades 'boAutoridad.getAutoridad_PgmSorteo(pidpgmsorteo)
        End If
        boConcurso.setPgmSorteo_Autoridades(oPC, _idpgmSorteo, listaJuegoAutoridades)

        dgv.Columns.Add("0", "Código")
        dgv.Columns(0).Width = 0
        dgv.Columns(0).DataPropertyName = "idAutoridad"
        dgv.Columns(0).Visible = False
        dgv.Columns.Add("1", "Nombre")
        dgv.Columns(1).Width = 140
        dgv.Columns(1).DataPropertyName = "nombre"
        dgv.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgv.Columns.Add("2", "Cargo")
        dgv.Columns(2).Width = 140
        dgv.Columns(2).DataPropertyName = "cargo"
        dgv.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgv.ClearSelection()

    End Sub

    Public Sub setPozo()
        Dim boPozo As New PozoBO
        Dim lista As List(Of Pozo)
        Dim modalidad As String

        lista = boPozo.getPozo(getIdJuegoActual(), getNroSorteoActual())

        For Each oPozo In lista
            modalidad = IIf(Len(CStr(oPozo.idPremio)) = 7, Mid(oPozo.idPremio, 1, 4), Mid(oPozo.idPremio, 1, 3))
            Try
                getControlTabGP("pst" & modalidad, "txt" & oPozo.idPremio).Text = oPozo.importe.ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))

                getControlTabGP("pst" & modalidad, "lbl" & oPozo.idPremio).Text = oPozo.nombrePremio
            Catch ex As Exception

            End Try
        Next
        Dim tot_pozo As Double = 0.0
        Dim tot_pozo_real As Double = 0.0
        Dim tot_apu As Int64 = 0
        modalidad = 0

        If Not boPozo.getPozoTotal(getIdJuegoActual(), getNroSorteoActual(), modalidad, tot_pozo, tot_pozo_real, tot_apu) Then

        Else
            getControlTabGP("pst" & Integer.Parse((modalidad / 1000).ToString.Split(",")(0)), "txtTotPozo" & Integer.Parse((modalidad / 100000).ToString.Split(",")(0))).Text = tot_pozo.ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))

        End If


    End Sub

    Public Sub setPozoReal()
        Dim boPozo As New PozoBO
        Dim lista As List(Of Pozo)
        Dim modalidad As String

        lista = boPozo.getPozo(getIdJuegoActual(), getNroSorteoActual())

        For Each oPozo In lista
            modalidad = IIf(Len(CStr(oPozo.idPremio)) = 7, Mid(oPozo.idPremio, 1, 4), Mid(oPozo.idPremio, 1, 3))
            Try
                getControlTabGPR("pstPozoReal" & modalidad, "txtPozoReal" & oPozo.idPremio).Text = oPozo.importeRec.ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
                getControlTabGPR("pstPozoReal" & modalidad, "lblPozoReal" & oPozo.idPremio).Text = oPozo.nombrePremio
            Catch ex As Exception

            End Try
        Next
        Dim tot_pozo As Double = 0.0
        Dim tot_pozo_real As Double = 0.0
        Dim tot_apu As Int64 = 0
        modalidad = 0

        If Not boPozo.getPozoTotal(getIdJuegoActual(), getNroSorteoActual(), modalidad, tot_pozo, tot_pozo_real, tot_apu) Then

        Else
            getControlTabGPR("pstPozoReal" & Integer.Parse((modalidad / 1000).ToString.Split(",")(0)), "txtTotPozoReal" & Integer.Parse((modalidad / 100000).ToString.Split(",")(0))).Text = tot_pozo_real.ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))

        End If
    End Sub

    Public Sub setApuestas()
        Dim boPozo As New PozoBO
        Dim lista As List(Of Pozo)
        Dim modalidad As String

        lista = boPozo.getPozo(getIdJuegoActual(), getNroSorteoActual())

        For Each oPozo In lista
            modalidad = IIf(Len(CStr(oPozo.idPremio)) = 7, Mid(oPozo.idPremio, 1, 4), Mid(oPozo.idPremio, 1, 3))
            Try
                getControlJA("pstApu" & modalidad, "txtApu" & oPozo.idPremio).Text = oPozo.Apuestas.ToString("N0", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
                getControlJA("pstApu" & modalidad, "lblApu" & oPozo.idPremio).Text = oPozo.nombrePremio
            Catch ex As Exception

            End Try
        Next
        Dim tot_pozo As Double = 0.0
        Dim tot_pozo_real As Double = 0.0
        Dim tot_apu As Int64 = 0
        modalidad = 0

        If Not boPozo.getPozoTotal(getIdJuegoActual(), getNroSorteoActual(), modalidad, tot_pozo, tot_pozo_real, tot_apu) Then

        Else
            getControlJA("pstApu" & Integer.Parse((modalidad / 1000).ToString.Split(",")(0)), "txtTotApu" & Integer.Parse((modalidad / 100000).ToString.Split(",")(0))).Text = tot_apu.ToString("N0", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))

        End If
    End Sub

    Public Sub pozoObtener(ByVal idJuego As Integer, Optional ByVal Obtienearchivo As Boolean = False)
        Try
            Dim idJuegoAct As String
            Dim sorteoAct As String
            Dim idSorteo As String
            Dim idpgmsorteo As Long

            idJuegoAct = Mid("00", 1, 2 - Len(getIdJuegoActual())) & getIdJuegoActual()
            sorteoAct = Mid("000000", 1, 6 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text

            idSorteo = idJuegoAct & sorteoAct
            idpgmsorteo = CLng(idSorteo)

            'setPozoDesdeArchivo(idJuego)
            'HG 22-10-2015
            ' Primero intenta cargar los pozos desde el WS,si el WS no encuntra pozo o falla,lo intenta cargar desde el archivo como siempre
            Dim sorteoBO As New PgmSorteoBO
            Dim cargardesdearchivo As Boolean = True

            If General.Obtener_pgmsorteos_ws = "S" Then
                If sorteoBO.obtenerPozosWS(idpgmsorteo) = True Then
                    cargardesdearchivo = False
                    If Obtienearchivo Then
                        MsgBox("Pozos y apuestas actualizados correctamente.")
                    End If
                End If
            End If
            If cargardesdearchivo Then
                setPozoDesdeArchivoZIP(idJuego, Obtienearchivo)
            End If
        Catch ex As Exception
            MsgBox("Problemas al obtener pozo desde archivo:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Public Sub pozoGuardar(ByVal idJuego As Integer)
        Dim boPozo As New PozoBO
        Dim lista As List(Of Pozo)
        Dim modalidad As String
        Dim oPozoNuevo As Pozo
        Dim separadordec As String
        Dim idjuegoActual As Integer
        Dim _NocargaAdicional As Boolean = False
        Try

            Me.Cursor = Cursors.WaitCursor
            'se  obtiene el separaddor decimal d ela configuracion regional para poder formatear correctamente el pozo
            separadordec = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
            idjuegoActual = getIdJuegoActual()
            lista = boPozo.getPozo(idjuegoActual, getNroSorteoActual())
            Dim _importe As String = "0"

            For Each oPozo In lista
                modalidad = IIf(Len(CStr(oPozo.idPremio)) = 7, Mid(oPozo.idPremio, 1, 4), Mid(oPozo.idPremio, 1, 3))
                'para quini6 y brinco si no tiene adicionales,no se tiene que cargar la modalidad 501
                If idjuegoActual = 4 Then
                    If modalidad = "405" And oPC.concurso.IdConcurso <> 18 Then
                        _NocargaAdicional = True
                    End If
                End If
                If idjuegoActual = 13 Then
                    If modalidad = "1305" And oPC.concurso.IdConcurso <> 19 Then
                        _NocargaAdicional = True
                    End If
                End If
                'para que no de error al querer cargar los text de sorteos adicionales cuando es un sorteo comun de quini o brinco
                If Not _NocargaAdicional Then
                    oPozoNuevo = New Pozo
                    oPozoNuevo.idPgmsorteo = getControlJ("txtIdPgmSorteo").Text
                    oPozoNuevo.idPremio = oPozo.idPremio
                    If Not IsNothing(getControlTabGP("pst" & modalidad, "txt" & oPozo.idPremio)) Then
                        _importe = getControlTabGP("pst" & modalidad, "txt" & oPozo.idPremio).Text
                        ''If separadordec = "," Then
                        ''    _importe = _importe.Replace(".", ",")
                        ''Else
                        ''    _importe = _importe.Replace(",", ".")
                        ''End If
                        _importe = IIf(_importe.Trim.Length = 0, 0, _importe.Trim)
                        Try
                            oPozoNuevo.importe = Double.Parse(_importe)
                        Catch ex As Exception
                            Me.Cursor = Cursors.Default
                            MsgBox("Para el premio -> " & oPozo.nombrePremio & " <- se ha ingresado un valor no numérico. Verifique.", MsgBoxStyle.Information, MDIContenedor.Text)
                            CType(getControlTabGP("pst" & modalidad, "txt" & oPozo.idPremio), TextBox).SelectAll()
                            getControlTabGP("pst" & modalidad, "txt" & oPozo.idPremio).Focus()
                            Exit Sub
                        End Try

                        ' ------------------------------------------------------------------------------------
                        ' pozo real
                        If Not IsNothing(getControlTabGPR("pstPozoReal" & modalidad, "txtPozoReal" & oPozo.idPremio)) Then
                            _importe = getControlTabGPR("pstPozoReal" & modalidad, "txtPozoReal" & oPozo.idPremio).Text
                            ''If separadordec = "," Then
                            ''    _importe = _importe.Replace(".", ",")
                            ''Else
                            ''    _importe = _importe.Replace(",", ".")
                            ''End If
                            _importe = IIf(_importe.Trim.Length = 0, 0, _importe.Trim)
                            Try
                                oPozoNuevo.importeRec = Double.Parse(_importe)
                            Catch ex As Exception
                                Me.Cursor = Cursors.Default
                                MsgBox("Para el premio -> " & oPozo.nombrePremio & " <- se ha ingresado un valor no numérico en e Pozo S/Recaudación. Verifique.", MsgBoxStyle.Information, MDIContenedor.Text)
                                CType(getControlTabGPR("pstPozoReal" & modalidad, "txtPozoReal" & oPozo.idPremio), TextBox).SelectAll()
                                getControlTabGPR("pstPozoReal" & modalidad, "txtPozoReal" & oPozo.idPremio).Focus()
                                Exit Sub
                            End Try
                        End If
                        ' ------------------------------------------------------------------------------------
                        ' ------------------------------------------------------------------------------------
                        ' apuestas
                        If Not IsNothing(getControlJA("pstApu" & modalidad, "txtApu" & oPozo.idPremio)) Then
                            _importe = getControlJA("pstApu" & modalidad, "txtApu" & oPozo.idPremio).Text
                            ''If separadordec = "," Then
                            ''    _importe = _importe.Replace(".", ",")
                            ''Else
                            ''    _importe = _importe.Replace(",", ".")
                            ''End If
                            _importe = IIf(_importe.Trim.Length = 0, 0, _importe.Trim)
                            Try
                                oPozoNuevo.Apuestas = Double.Parse(_importe)
                            Catch ex As Exception
                                Me.Cursor = Cursors.Default
                                MsgBox("Para el premio -> " & oPozo.nombrePremio & " <- se ha ingresado un valor no numérico en e Pozo S/Recaudación. Verifique.", MsgBoxStyle.Information, MDIContenedor.Text)
                                CType(getControlJA("pstApu" & modalidad, "txtApu" & oPozo.idPremio), TextBox).SelectAll()
                                getControlJA("pstApu" & modalidad, "txtApu" & oPozo.idPremio).Focus()
                                Exit Sub
                            End Try
                        End If
                        ' ------------------------------------------------------------------------------------
                        boPozo.setPozo(oPozoNuevo)
                        'getControlTabGP("pst" & modalidad, "txt" & oPozo.idPremio).Text = getControlJP("pst" & modalidad, "txt" & oPozo.idPremio).Text.Replace(".", ",")
                    End If
                    boPozo.setPozo(oPozoNuevo)
                Else
                    _NocargaAdicional = False

                End If
            Next
            Me.Cursor = Cursors.Default
            MsgBox("Los pozos se han guardado correctamente", MsgBoxStyle.Information, MDIContenedor.Text)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
        End Try
    End Sub

    Public Sub setPozoDesdeArchivoZIP(ByVal idJuego As Integer, Optional ByVal Obtenerarchivo As Boolean = False)
        Dim f As StreamReader
        Dim archivo As String
        Dim idJuegoAct As String
        Dim sorteoAct As String

        Dim linea As String
        Dim juego As String
        Dim sorteo As String
        Dim modalidad As String
        Dim codigo As String
        Dim importePozo As String
        Dim cantGanadores As String
        Dim importePremio As String
        Dim vacante As String
        Dim cantApuestas As String
        Dim importePozoReal As String
        Dim cantApuestasInt As Int64
        Dim importePozoRealDbl As Double

        Dim controlTxt As Control
        Dim gralDal As New Sorteos.Data.GeneralDAL
        Dim rta As Integer
        Dim boPremio As New PremioBO
        'Dim lista As List(Of Premio)
        Dim prefijo As String = General.PrefijoPozo
        Dim Sorteodal As New PgmSorteoDAL
        Dim idSorteo As String
        Dim idpgmsorteo As Long
        Dim archivoOrigen As String = ""
        Dim archivoDestino As String = ""
        Dim nombrearchivo As String = ""
        Dim pathDestino As String = ""
        Dim ArchivoControl As String = ""
        Dim parametrosCopiar As String()
        Dim cantidadAciertos As Integer = 0
        Dim RequiereAciertos As Integer = 0
        Dim NombrePremio As String = ""
        'Dim _Habilita_2Premio_Quini6 As Boolean
        Dim _habilita As Boolean
        Dim _archivosIguales As Boolean = False
        Dim pathOrigen As String
        Dim Adic_tipo As Integer
        Dim resto As Long
        Dim sorteoBO As New PgmSorteoBO

        Try

            idJuegoAct = Mid("00", 1, 2 - Len(getIdJuegoActual())) & getIdJuegoActual()
            sorteoAct = Mid("000000", 1, 6 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text
            idSorteo = idJuegoAct & sorteoAct
            idpgmsorteo = CLng(idSorteo)
            '** comentar al implementar zip
            pathOrigen = gralDal.getParametro("INI", "PATH_POZOS")

            If Not pathOrigen.EndsWith("\") Then
                pathOrigen = pathOrigen & "\"
            End If
            'RL
            'archivo = pathOrigen & prefijo & idJuegoAct.PadLeft(2, "0") & sorteoAct & ".dat"

            '***** descomentar al implementar archivo zip
            '*****
            '** se formatea el nombre del archivo
            nombrearchivo = prefijo & idJuegoAct.PadLeft(2, "0") & sorteoAct
            '** obtengo la ruta  donde se guardan los archivos zip
            pathDestino = General.Path_Pozo_Destino
            '** obtengo el archivo zip
            'RL
            archivoOrigen = pathOrigen & nombrearchivo & ".zip"

            '** formo el path del archivo destino ,si se deszipeo con exito
            If Not pathDestino.EndsWith("\") Then
                pathDestino = pathDestino & "\"
            End If

            'controla que el origen y el destino no sean iguales
            If pathOrigen = pathDestino Then
                _archivosIguales = True
            End If
            'RL
            archivoDestino = pathDestino & nombrearchivo & ".dat"
            ArchivoControl = pathDestino & nombrearchivo & ".cxt"




            '** busca en la BD si hay pozos cargados sino los busca en el archivo
            If Not Obtenerarchivo Then
                If Not Sorteodal.NoTienepozos(idpgmsorteo, CInt(idJuegoAct)) Then
                    setPozo()
                    Exit Sub
                Else ' No hay datos en la base -> pruebo desde archivo
                    '****** DESCOMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                    'RL
                    If Not File.Exists(archivoOrigen) Then

                        '*** COMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                        'RL
                        'If Not File.Exists(archivo) Then

                        rta = MsgBox("No pudo localizarse el archivo en la ruta por defecto.  Desea seleccionarlo manualmente.", MsgBoxStyle.YesNo, MDIContenedor.Text)

                        If rta = vbYes Then
                            openFileD.Filter = "Archivos de sorteos|*.zip"
                            openFileD.DefaultExt = "zip"
                            openFileD.ShowDialog()

                            If openFileD.FileNames.Count = 0 Then

                            Else
                                'archivo = openFileD.FileNames(0)
                                '**DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                                'RL
                                'archivoDestino = openFileD.FileNames(0)
                                archivoOrigen = openFileD.FileNames(0)
                                Dim a As Integer = InStrRev(archivoOrigen, "\")
                                nombrearchivo = archivoOrigen.Substring(a)
                                pathOrigen = archivoOrigen.Substring(0, archivoOrigen.Length - nombrearchivo.Length)
                                nombrearchivo = nombrearchivo.Replace(".zip", "")
                            End If
                        Else
                            MsgBox("Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information, MDIContenedor.Text)
                            setPozo()
                            Exit Sub
                        End If
                    End If
                    '**18/10/2012****
                    'DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                    '** copia el zip a la carpeta destino
                    If _archivosIguales Then
                        MsgBox("Los parámetros de origen y destino configurados para pozos son iguales. No se realiza la carga de pozos desde archivo.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                    ReDim parametrosCopiar(0)
                    'parametrosCopiar(0) = gralDal.getParametro("INI", "PATH_POZOS") & ";" & pathDestino & ";" & nombrearchivo & ".zip"
                    parametrosCopiar(0) = pathOrigen & ";" & pathDestino & ";" & nombrearchivo & ".zip"
                    FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")

                    '' ''** descomprime el archivo a la carpeta destino
                    'RL
                    FileSystemHelper.Descomprimir(pathDestino, archivoOrigen)

                    '** control del archivo contra el archivo de control md5
                    If General.CtrMD5Pozos = "S" Then
                        If Not FileSystemHelper.ControlArchivoMd5(archivoDestino, ArchivoControl) Then
                            MsgBox("El archivo " & nombrearchivo & ".dat no coincide con el archivo de control. Los pozos no pueden ser cargados." & vbCrLf & "Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information, MDIContenedor.Text)
                            setPozo()
                            'borra archivos .dat y .cxt
                            FileSystemHelper.BorrarArchivo(archivoDestino)
                            FileSystemHelper.BorrarArchivo(ArchivoControl)
                            Exit Sub
                        End If
                    End If
                    f = New StreamReader(archivoDestino)

                    '*** COMENTAR LA SIGUIENTE LINEA  AL  IMPLEMENTAR ARCHIVO ZIP
                    'RL
                    'f = New StreamReader(archivo)

                    While Not f.EndOfStream
                        linea = f.ReadLine()

                        juego = Mid(linea, 1, 2)
                        sorteo = Mid(linea, 3, 6)
                        modalidad = Mid(linea, 9, 2)
                        codigo = idJuegoAct & Mid(linea, 13, 5)
                        importePozo = Mid(linea, 18, 17) ' 15E2D
                        cantGanadores = Mid(linea, 35, 6)
                        importePremio = Mid(linea, 41, 17) ' 15E2D
                        vacante = Mid(linea, 58, 1)
                        cantApuestas = Mid(linea, 59, 9)
                        importePozoReal = Mid(linea, 68, 17) ' 15E2D

                        '** 18/10/2012 ****
                        boPremio.ObtieneDatosPremio(codigo, cantidadAciertos, RequiereAciertos, NombrePremio, juego, sorteo, Adic_tipo)
                        _habilita = IIf((RequiereAciertos = 0), False, True)

                        ' validaciones
                        If CInt(juego) <> CInt(idJuegoAct) Then
                            MsgBox("El registro no corresponde al juego actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                            Exit Sub
                        End If

                        If CDbl(sorteo) <> CDbl(sorteoAct) Then
                            MsgBox("El registro no corresponde al sorteo actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                            Exit Sub
                        End If

                        '*** los sueldos no viene en el archivo de pozos
                        If codigo = 1301006 Or codigo = 1309001 Then
                            Continue While
                        End If
                        If codigo = 3001004 Then
                            Continue While
                        End If
                        If juego = "04" Then juego = "4"
                        ' Quini, Brinco, y Poceada Fed. es obligatorio el pozo real y cant de apuestas
                        If (idJuego = 4 Or idJuego = 13 Or idJuego = 30) Then
                            Try
                                Math.DivRem(CLng(codigo), 1000, resto)
                                cantApuestasInt = Int64.Parse(cantApuestas.Trim())
                                If cantApuestasInt <= 0 Then
                                    If resto = 1 Then
                                        MsgBox("Cantidad de apuestas no puede ser menor o igual a cero para el " & NombrePremio & ". Avise a Boldt para que genere un nuevo archivo o regístrelo manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                                    End If

                                End If
                            Catch ex As Exception
                                MsgBox("Cantidad de apuestas errónea para el " & NombrePremio & ". Avise a Boldt para que genere un nuevo archivo o regístrelo manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                                Exit Sub
                            End Try
                            Try
                                Math.DivRem(CLng(codigo), 100000, resto)
                                importePozoRealDbl = Double.Parse(importePozoReal / 100)
                                If importePozoRealDbl <= 0 Then
                                    If resto <> 5005 And resto <> 5001 And resto <> 7002 And resto <> 4001 And resto <> 4002 Then
                                        MsgBox("Importe real de Pozo no puede ser menor o igual a cero para el " & NombrePremio & ". Avise a Boldt para que genere un nuevo archivo o regístrelo manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                                    End If
                                End If
                            Catch ex As Exception
                                MsgBox("Importe real de Pozo erróneo para el " & NombrePremio & ". Avise a Boldt para que genere un nuevo archivo o regístrelo manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                                Exit Sub
                            End Try
                        End If
                        ' Asigno valores leidos a controles de pantalla
                        ' ** Importe Pozo
                        'controlTxt = getControlJP("pst" & juego & modalidad, "txt" & codigo)
                        controlTxt = getControlTabGP("pst" & juego & modalidad, "txt" & codigo)
                        If IsNothing(controlTxt) Then
                            'MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Else
                            controlTxt.Text = CDbl(importePozo / 100).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
                            getControlTabGP("pst" & juego & modalidad, "txtTotPozo" & juego).Text = actualizaTotPozo()
                        End If

                        ' ** Importe Pozo Real
                        ' getPanel
                        'controlTxt = getControlJP("pst" & juego & modalidad, "txt" & codigo)
                        controlTxt = getControlTabGPR("pstPozoReal" & juego & modalidad, "txtPozoReal" & codigo)
                        If IsNothing(controlTxt) Then
                            'MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Else
                            controlTxt.Text = importePozoRealDbl.ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
                            getControlTabGPR("pstPozoReal" & juego & modalidad, "txtTotPozoReal" & juego).Text = actualizaTotPozoReal()
                        End If

                        ' ** Cantidad de Apuestas
                        'controlTxt = getControlJP("pst" & juego & modalidad, "txt" & codigo)
                        controlTxt = getControlJA("pstApu" & juego & modalidad, "txtApu" & codigo)
                        If IsNothing(controlTxt) Then
                            'MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Else
                            controlTxt.Text = cantApuestasInt.ToString("N0", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
                            getControlJA("pstApu" & juego & modalidad, "txtTotApu" & juego).Text = actualizaTotApu()
                        End If
                    End While
                    f.Dispose()
                    '** DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                    '** borra los archivos .dat y .cxt,dejando solo los archivos zip en la carpeta de destino
                    'RL
                    FileSystemHelper.BorrarArchivo(archivoDestino)
                    FileSystemHelper.BorrarArchivo(ArchivoControl)
                    MsgBox("Los pozos se han cargado correctamente." & vbCrLf & " Revise que los valores sea correctos y haga click en Guardar Pozos para guardar los pozos cargados desde el archivo.", MsgBoxStyle.Information, MDIContenedor.Text)

                End If
            Else
                'Se trata de cargar desde el archivo sin importar si tiene pozos cargados o no
                If MsgBox("Los pozos se actualizarán con los datos del archivo." & vbCrLf & "¿Desea continuar?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, MDIContenedor.Text) = MsgBoxResult.No Then
                    Exit Sub
                End If
                '****** DESCOMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                'RL
                If Not File.Exists(archivoOrigen) Then

                    '*** COMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                    'RL
                    'If Not File.Exists(archivo) Then

                    rta = MsgBox("No pudo localizarse el archivo en la ruta por defecto.  Desea seleccionarlo manualmente.", MsgBoxStyle.YesNo, MDIContenedor.Text)

                    If rta = vbYes Then
                        openFileD.Filter = "Archivos de sorteos|*.zip"
                        openFileD.DefaultExt = "zip"
                        openFileD.ShowDialog()

                        If openFileD.FileNames.Count = 0 Then
                            '*** iniciar el timer para el aviso de recepcion de archivos
                            Timer1.Interval = 30000
                            Timer1.Start()
                            Exit Sub
                        Else
                            'pathOrigen = openFileD.
                            archivo = openFileD.FileNames(0)
                            '**DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                            'RL
                            archivoOrigen = openFileD.FileNames(0)
                            Dim a As Integer = InStrRev(archivoOrigen, "\")
                            nombrearchivo = archivoOrigen.Substring(a)
                            pathOrigen = archivoOrigen.Substring(0, archivoOrigen.Length - nombrearchivo.Length)
                            nombrearchivo = nombrearchivo.Replace(".zip", "")

                        End If
                    Else
                        MsgBox("Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information, MDIContenedor.Text)
                        setPozo()
                        Exit Sub
                    End If
                End If
                '**18/10/2012****
                'DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                If _archivosIguales Then
                    MsgBox("Los parámetros de origen y destino configurados para pozos son iguales. No se realiza la carga de pozos desde archivo.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End If
                '** copia el zip a la carpeta destino
                ReDim parametrosCopiar(0)
                parametrosCopiar(0) = pathOrigen & ";" & pathDestino & ";" & nombrearchivo & ".zip"
                FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")
                'RL
                '** descomprime el archivo a la carpeta destino
                FileSystemHelper.Descomprimir(pathDestino, archivoOrigen)

                '** control del archivo contra el archivo de control md5
                If General.CtrMD5Pozos = "S" Then
                    If Not FileSystemHelper.ControlArchivoMd5(archivoDestino, ArchivoControl) Then
                        MsgBox("El archivo " & nombrearchivo & ".dat no coincide con el archivo de control. Los pozos no pueden ser cargados." & vbCrLf & "Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information, MDIContenedor.Text)
                        setPozo()
                        'borra archivos .dat y .cxt
                        FileSystemHelper.BorrarArchivo(archivoDestino)
                        FileSystemHelper.BorrarArchivo(ArchivoControl)
                        Exit Sub
                    End If
                End If

                f = New StreamReader(archivoDestino)

                '*** COMENTAR LA SIGUIENTE LINEA  AL  IMPLEMENTAR ARCHIVO ZIP
                'RL
                'f = New StreamReader(archivo)

                While Not f.EndOfStream
                    linea = f.ReadLine()

                    juego = Mid(linea, 1, 2)
                    sorteo = Mid(linea, 3, 6)
                    modalidad = Mid(linea, 9, 2)
                    codigo = idJuegoAct & Mid(linea, 13, 5)
                    importePozo = Mid(linea, 18, 17) ' 15E2D
                    cantGanadores = Mid(linea, 35, 6)
                    importePremio = Mid(linea, 41, 17) ' 15E2D
                    vacante = Mid(linea, 58, 1)
                    cantApuestas = Mid(linea, 59, 9)
                    importePozoReal = Mid(linea, 68, 17) ' 15E2D

                    '** 18/10/2012 ****
                    boPremio.ObtieneDatosPremio(codigo, cantidadAciertos, RequiereAciertos, NombrePremio, juego, sorteo, Adic_tipo)

                    _habilita = IIf((RequiereAciertos = 0), False, True)
                    ' controles
                    If CInt(juego) <> CInt(idJuegoAct) Then
                        MsgBox("El registro no corresponde al juego actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If

                    If CDbl(sorteo) <> CDbl(sorteoAct) Then
                        MsgBox("El registro no corresponde al sorteo actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                    '*** los sueldos no viene en el archivo de pozos
                    If codigo = 1301006 Or codigo = 1309001 Then
                        Continue While
                    End If
                    If codigo = 3001004 Then
                        Continue While
                    End If
                    If juego = "04" Then juego = "4"
                    ' Quini, Brinco, y Poceada Fed. es obligatorio el pozo real y cant de apuestas
                    If (idJuego = 4 Or idJuego = 13 Or idJuego = 30) Then
                        Try
                            Math.DivRem(CLng(codigo), 1000, resto)
                            cantApuestasInt = Int64.Parse(cantApuestas.Trim())
                            If cantApuestasInt <= 0 Then
                                If resto = 1 Then
                                    MsgBox("Cantidad de apuestas no puede ser menor o igual a cero para el " & NombrePremio & ". Avise a Boldt para que genere un nuevo archivo o regístrelo manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                                End If

                            End If
                        Catch ex As Exception
                            MsgBox("Cantidad de apuestas errónea para el " & NombrePremio & ". Avise a Boldt para que genere un nuevo archivo o regístrelo manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                            Exit Sub
                        End Try
                        Try
                            Math.DivRem(CLng(codigo), 100000, resto)
                            importePozoRealDbl = Double.Parse(importePozoReal / 100)
                            If importePozoRealDbl <= 0 Then
                                If resto <> 5005 And resto <> 5001 And resto <> 7002 And resto <> 4001 And resto <> 4002 Then
                                    MsgBox("Importe real de Pozo no puede ser menor o igual a cero para el " & NombrePremio & ". Avise a Boldt para que genere un nuevo archivo o regístrelo manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                                End If
                            End If
                        Catch ex As Exception
                            MsgBox("Importe real de Pozo erróneo para el " & NombrePremio & ". Avise a Boldt para que genere un nuevo archivo o regístrelo manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                            Exit Sub
                        End Try
                    End If
                    ' Asigno valores leidos a controles de pantalla
                    ' ** Importe Pozo
                    'controlTxt = getControlJP("pst" & juego & modalidad, "txt" & codigo)
                    controlTxt = getControlTabGP("pst" & juego & modalidad, "txt" & codigo)
                    If IsNothing(controlTxt) Then
                        'MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Else
                        controlTxt.Text = CDbl(importePozo / 100).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
                        getControlTabGP("pst" & juego & modalidad, "txtTotPozo" & juego).Text = actualizaTotPozo()
                    End If

                    ' ** Importe Pozo Real
                    ' getPanel
                    'controlTxt = getControlJP("pst" & juego & modalidad, "txt" & codigo)
                    controlTxt = getControlTabGPR("pstPozoReal" & juego & modalidad, "txtPozoReal" & codigo)
                    If IsNothing(controlTxt) Then
                        'MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Else
                        controlTxt.Text = importePozoRealDbl.ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
                        getControlTabGPR("pstPozoReal" & juego & modalidad, "txtTotPozoReal" & juego).Text = actualizaTotPozoReal()
                    End If

                    ' ** Cantidad de Apuestas
                    'controlTxt = getControlJP("pst" & juego & modalidad, "txt" & codigo)
                    controlTxt = getControlJA("pstApu" & juego & modalidad, "txtApu" & codigo)
                    If IsNothing(controlTxt) Then
                        'MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Else
                        controlTxt.Text = cantApuestasInt.ToString("N0", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
                        getControlJA("pstApu" & juego & modalidad, "txtTotApu" & juego).Text = actualizaTotApu()
                    End If

                End While
                f.Dispose()
                '** DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                '** borra los archivos .dat y .cxt,dejando solo los archivos zip en la carpeta de destino
                'RL
                FileSystemHelper.BorrarArchivo(archivoDestino)
                FileSystemHelper.BorrarArchivo(ArchivoControl)
                MsgBox("Los pozos se han cargado correctamente." & vbCrLf & " Revise que los valores sea correctos y haga click en Guardar Pozos para guardar los pozos cargados desde el archivo.", MsgBoxStyle.Information, MDIContenedor.Text)

            End If
        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Problemas al cargar pozos: " & ex.Message)
            MsgBox("Problemas al cargar pozos: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try


    End Sub

    Public Sub setPozoDesdeArchivo(ByVal idJuego As Integer)
        Dim f As StreamReader
        Dim archivo As String
        Dim idJuegoAct As String
        Dim sorteoAct As String

        Dim linea As String
        Dim juego As String
        Dim sorteo As String
        Dim modalidad As String
        Dim codigo As String
        Dim importe As String
        Dim controlTxt As Control
        Dim gralDal As New Sorteos.Data.GeneralDAL
        Dim rta As Integer
        Dim prefijo As String = General.PrefijoPozo
        '*** 18/10/2012**
        Dim nombrearchivo As String = ""
        Dim archivoOrigen As String = ""
        Dim archivoDestino As String = ""
        Dim PathOrigen As String = ""
        Dim pathDestino As String = ""
        Dim ArchivoControl As String = ""
        Dim parametrosCopiar As String()

        idJuegoAct = Mid("00", 1, 2 - Len(getIdJuegoActual())) & getIdJuegoActual()
        sorteoAct = Mid("000000", 1, 6 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text


        'nombrearchivo = prefijo & idJuegoAct & sorteoAct


        archivo = gralDal.getParametro("INI", "PATH_POZOS") & "\" & prefijo & idJuegoAct & sorteoAct & ".dat"

        ''archivoOrigen = gralDal.getParametro("INI", "PATH_POZOS") & "\" & prefijo & idJuegoAct & sorteoAct & ".zip"
        ''pathDestino = General.Path_Pozo_Destino
        ''If Not pathDestino.EndsWith("\") Then
        ''    pathDestino = pathDestino & "\"
        ''End If
        ''Archivocontrol = pathDestino & nombrearchivo & ".cxt"

        If Not File.Exists(archivo) Then
            ''If Not File.Exists(archivoOrigen) Then
            rta = MsgBox("No pudo localizarse el archivo en la ruta por defecto.  Desea seleccionarlo manualmente.", MsgBoxStyle.YesNo, MDIContenedor.Text)

            If rta = vbYes Then
                openFileD.Filter = "Archivos de sorteos|*.dat"
                openFileD.DefaultExt = "dat"
                openFileD.ShowDialog()

                If openFileD.FileNames.Count = 0 Then
                    Exit Sub
                Else
                    'archivo = openFileD.FileNames(0)
                    archivoDestino = openFileD.FileNames(0)
                End If
            Else
                Exit Sub
            End If
        End If

        '**18/10/2012****
        'DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
        '** copia el zip a la carpeta destino
        ''ReDim parametrosCopiar(0)
        ''parametrosCopiar(0) = gralDal.getParametro("INI", "PATH_POZOS") & ";" & pathDestino & ";" & nombrearchivo & ".zip"
        ''FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")

        ' ''** descomprime el archivo a la carpeta destino
        ''FileSystemHelper.Descomprimir(pathDestino, archivoOrigen)

        '** control del archivo contra el archivo de control md5
        ''If Not FileSystemHelper.ControlArchivoMd5(archivoDestino, Archivocontrol) Then
        ''    MsgBox("El archivo " & nombrearchivo & ".dat no coincide con el archivo de control.Los premios no pueden ser cargados." & vbCrLf & "Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information)
        ''    setPozo()
        ''    'borra archivos .dat y .cxt
        ''    FileSystemHelper.BorrarArchivo(archivoDestino)
        ''    FileSystemHelper.BorrarArchivo(Archivocontrol)
        ''    Exit Sub
        ''End If

        ''f = New StreamReader(archivoDestino)

        f = New StreamReader(archivo)

        While Not f.EndOfStream
            linea = f.ReadLine()

            juego = Mid(linea, 1, 2)
            sorteo = Mid(linea, 3, 6)
            modalidad = Mid(linea, 9, 2)
            codigo = Mid(linea, 11, 7)
            importe = Mid(linea, 18, 17) ' 15E2D

            ' controles
            If juego <> idJuegoAct Then
                MsgBox("El registro no corresponde al juego actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            If sorteo <> sorteoAct Then
                MsgBox("El registro no corresponde al sorteo actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            controlTxt = getControlJP("pst" & juego & modalidad, "txt" & codigo)
            If IsNothing(controlTxt) Then
                'MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
            Else
                controlTxt.Text = CDbl(importe / 100)
            End If

        End While
        f.Dispose()
        'borra los archivos .dat y.cxt
        FileSystemHelper.BorrarArchivo(archivoDestino)
        FileSystemHelper.BorrarArchivo(ArchivoControl)


    End Sub

    ''Public Sub parametrosListar()
    ''    Dim frmParams As New ConcursoInicioParametros
    ''    Dim bo As New PgmConcursoBO
    ''    Dim msg As String

    ''    ' carga los valores en la entidad
    ''    getValores()

    ''    If (bo.valida(oPC, msg)) Then
    ''        ' listar parametros
    ''        bo.setPgmConcurso(oPC)
    ''        frmParams.oPC = oPC
    ''        frmParams.Show()
    ''    Else
    ''        MsgBox(msg, MsgBoxStyle.Information)
    ''    End If


    ''End Sub

    Public Sub concursoIniciar()
        Dim bo As New PgmConcursoBO
        Dim msg As String
        Dim ItemMenu As ToolStripMenuItem

        ' Variables para crear el path archivos de otras jurisdicciones
        Dim pathArchivoJur As String = ""
        Dim nroVersion As Int32 = 0

        Me.Cursor = Cursors.WaitCursor

        ' carga los valores en la entidad
        getValores()

        If (bo.valida(oPC, msg)) Then
            Me.Cursor = Cursors.WaitCursor
            bo.setPgmConcurso(oPC)
            Try
                Me.Cursor = Cursors.WaitCursor
                FileSystemHelper.Log("concursoinicio:llamar a bo.Iniciar, concurso:" & oPC.idPgmConcurso)
                bo.Iniciar(oPC)
                FileSystemHelper.Log("concursoinicio: funcion  bo.Iniciar OK, concurso:" & oPC.idPgmConcurso)
            Catch ex As Exception
                Me.Cursor = Cursors.Arrow
                If ex.Message.StartsWith("No") Or ex.Message.StartsWith("El") Then
                    MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                Else
                    If Not (ex.Message.StartsWith("Pr")) Then
                        FileSystemHelper.Log("concursoinicio: funcion  bo.Iniciar ERROR, concurso:" & oPC.idPgmConcurso & " - Excepcion: " & ex.Message)
                        MsgBox("Ocurrió un problema al intentar iniciar el concurso. Cierre y vuelva a abrir la ventana e intente nuevamente. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                End If
            End Try

            ' Imprimo PARAMETROS....
            Try
                Me.Cursor = Cursors.WaitCursor
                nroVersion = bo.versionarParametros(oPC.idPgmConcurso, "VERIFICADO", MDIContenedor.usuarioAutenticado.Usuario)
                FileSystemHelper.Log("ConcursoINICIO: listar parametros, concurso ->" & oPC.idPgmConcurso & "<- nroVersion ->" & nroVersion & "<-.")
                '08/08/2017 RL: el lst parametros se imprime sólo CON o SIN espacio para ganadores 
                '               dependiendo del concurso

                ''24-5-2016 HG- se agregan todos los concurso que contienen poceada
                'If oPC.concurso.IdConcurso = 16 Or oPC.concurso.IdConcurso = 17 Or oPC.concurso.IdConcurso = 18 Or oPC.concurso.IdConcurso = 19 Or oPC.concurso.IdConcurso = 5 Or oPC.concurso.IdConcurso = 7 Or oPC.concurso.IdConcurso = 9 Or oPC.concurso.IdConcurso = 13 Or oPC.concurso.IdConcurso = 15 Or oPC.concurso.IdConcurso = 20 Then
                '    ImprimirParametros(oPC.idPgmConcurso, "S") ' Si es uno de estos concursos imprimo CON espacio para ganadores
                '    If General.ListarReporteDetalleParametros = "S" Then
                '        'si este parametro = S -> se imprime tambien el listado SIN ganadores
                '        ImprimirParametros(oPC.idPgmConcurso, "N")
                '    End If
                'Else
                '    'se imprime el listado de parametros definito
                '    ImprimirParametros(oPC.idPgmConcurso, "N")
                'End If

                ''RL: 23/07/2018. Caso 107680. Piden que este no se imprima aquí para quini y brinco
                If Not (oPC.concurso.IdConcurso = 16 Or oPC.concurso.IdConcurso = 17 Or oPC.concurso.IdConcurso = 18 Or oPC.concurso.IdConcurso = 19) Then
                    If oPC.concurso.LstParConEspacioGan Then
                        ImprimirParametros(oPC.idPgmConcurso, "S", 1)
                    End If
                End If

                ' Si el parametro LstRepDetParam = true imprimo version SIN ganad....
                If oPC.concurso.LstRepDetParam Then
                    ImprimirParametros(oPC.idPgmConcurso, "N", 1)
                End If

                ' 2018 RL: reemplazo el parametro del ini por un campo en bd...
                'FIN 08/08/2017 RL: el lst parametros se imprime sólo CON o SIN espacio para ganadores 
                '               dependiendo del concurso
                FileSystemHelper.Log("ConcursoINICIO: listar parametros, FIN OK! concurso ->" & oPC.idPgmConcurso & "<- nroVersion ->" & nroVersion & "<-.")
            Catch ex As Exception
                FileSystemHelper.Log("concursoinicio: ERROR en listar parametros, concurso:" & oPC.idPgmConcurso)
                MsgBox("Ocurrió un problema al intentar imprimir los parámetros del concurso. Verifique la impresora, cierre y vuelva a abrir la ventana e intente nuevamente. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
                Me.Cursor = Cursors.Default
                Exit Sub
            End Try

            ''RL: 23/07/2018. Caso 107680. Piden que este no se imprima aquí para quini y brinco
            ' '' Imprimo los ESCENARIOS POZOS PROX SORTEO 
            ''If oPC.concurso.JuegoPrincipal.Juego.EstimaPozosProxSorteo Then
            ''    Try
            ''        FileSystemHelper.Log("ParametrosListar: inicio listar ESCENARIOS POZOS PROX SORTEO, concurso:" & oPC.idPgmConcurso)
            ''        bo.ImprimirParametrospozoproximo(oPC.idPgmConcurso, 1, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, msg)
            ''        FileSystemHelper.Log("ParametrosListar: FIN OK listar ESCENARIOS POZOS PROX SORTEO, concurso:" & oPC.idPgmConcurso)
            ''    Catch ex As Exception
            ''        Me.Cursor = Cursors.Default
            ''        FileSystemHelper.Log("ParametrosListar: ERROR al listar ESCENARIOS POZOS PROX SORTEO, concurso:" & oPC.idPgmConcurso & " - Ex: " & ex.Message)
            ''        MsgBox("Ocurrió un problema al listar ESCENARIOS POZOS PROX SORTEO. Cierre y vuelva a abrir la ventana e intente nuevamente. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
            ''        Exit Sub
            ''    End Try
            ''End If
            ' '' Imprimo los ESCENARIOS GANADORES DEL PRIMER PREMIO
            ''If oPC.concurso.JuegoPrincipal.Juego.EstimaPozosProxSorteo Then
            ''    Try
            ''        FileSystemHelper.Log("ParametrosListar: inicio listar ESCENARIOS GANADORES DEL PRIMER PREMIO, concurso:" & oPC.idPgmConcurso)
            ''        bo.ImprimirEscenariosGanadores1Premio(oPC.idPgmConcurso, 1, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, msg)
            ''        FileSystemHelper.Log("ParametrosListar: FIN OK listar ESCENARIOS GANADORES DEL PRIMER PREMIO, concurso:" & oPC.idPgmConcurso)
            ''    Catch ex As Exception
            ''        Me.Cursor = Cursors.Default
            ''        FileSystemHelper.Log("ParametrosListar: ERROR al listar ESCENARIOS GANADORES DEL PRIMER PREMIO, concurso:" & oPC.idPgmConcurso & " - Ex: " & ex.Message)
            ''        MsgBox("Ocurrió un problema al listar ESCENARIOS GANADORES DEL PRIMER PREMIO. Cierre y vuelva a abrir la ventana e intente nuevamente. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
            ''        Exit Sub
            ''    End Try
            ''End If
            '' FIN RL: 23/07/2018. Caso 107680. Piden que este no se imprima aquí para quini y brinco
        Try
            'CREA CARPETA PARA ARCHIVO DE OTRAS JURISDICCIONES
                For Each oSorteo In oPC.PgmSorteos
                    ' If oSorteo.idJuego = 2 Or oSorteo.idJuego = 3 Or oSorteo.idJuego = 8 Then ' solo puede haber oro en vesp y noct, excep en matu por eso la considero
                    For Each oLoteria In oSorteo.ExtraccionesLoteria
                        If oLoteria.Loteria.path_extracto.Trim.Length > 0 Then
                            If Not (oLoteria.Loteria.path_extracto.Trim.EndsWith("\") Or oLoteria.Loteria.path_extracto.Trim.EndsWith("/")) Then
                                oLoteria.Loteria.path_extracto = oLoteria.Loteria.path_extracto.Trim & "\"
                            End If
                            If Not (oSorteo.PathLocalJuego.EndsWith("\") Or oSorteo.PathLocalJuego.EndsWith("/")) Then
                                oSorteo.PathLocalJuego = oSorteo.PathLocalJuego.Trim & "\"
                            End If
                            If (oSorteo.PathLocalJuego.StartsWith("\") Or oSorteo.PathLocalJuego.StartsWith("/")) Then
                                oSorteo.PathLocalJuego = oSorteo.PathLocalJuego.Trim.Substring(1, oSorteo.PathLocalJuego.Trim.Length - 1)
                            End If
                            pathArchivoJur = oLoteria.Loteria.path_extracto & oSorteo.PathLocalJuego & oSorteo.nroSorteo.ToString.Trim
                            FileSystemHelper.CrearPath(pathArchivoJur)
                            'Exit For
                        End If
                    Next
                Next
            Catch ex As Exception
                ' Error pasante: si no pude crear un path logueo y sigo de largo....
                FileSystemHelper.Log("concursoinicio: ERROR al crear path para archivos de jurisdicciones. Concurso: " & oPC.idPgmConcurso & ". pathArchivoJur->" & pathArchivoJur & "<-")
            End Try

            Me.Cursor = Cursors.Default
            Try
                FileSystemHelper.Log("concursoinicio: abriendo formulario de carga de extracciones. Concurso: " & oPC.idPgmConcurso)
                'se cierra carga de extracciones si ya estaba abierto
                For Each FORMULARIO As Form In MDIContenedor.MdiChildren
                    If UCase(FORMULARIO.Name) = "CONCURSOEXTRACCIONES" Then
                        FORMULARIO.Close()
                    End If
                Next
                Dim frm As ConcursoExtracciones = MDIContenedor.FormRegistrarExtraccionesInstance
                frm.MdiParent = MDIContenedor
                'frm.WindowState = FormWindowState.Maximized
                frm.OPgmConcurso = oPC
                frm.Show()
                FileSystemHelper.Log("concursoinicio: Inicio de concurso OK, concurso:" & oPC.idPgmConcurso)
            Catch ex As Exception
                FileSystemHelper.Log("concursoinicio: ERROR en abriendo formulario de carga de extracciones. Concurso: " & oPC.idPgmConcurso)
                MsgBox("Ocurrió un problema al intentar abrir el formulario de carga de extracciones. Cierre y vuelva a abrir la aplicación y vaya a la carga de extracciones. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)

                Exit Sub
            End Try
        Else
            Me.Cursor = Cursors.Default
            MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
        End If
        Me.Cursor = Cursors.Default
    End Sub

    Private Sub DeshabilitarControles(Optional ByVal habilita = False)
        If Not habilita Then
            Me.grpExtracciones.Enabled = False
            Me.grpJuegos.Enabled = False
            Me.CboConcurso.Enabled = False
            Me.btnConcursoIniciar.Enabled = False
            Me.btnConcursoRevertir.Enabled = False
            'Me.txtJuegoRector.Enabled = False
            'Me.txtModeloExtracciones.Enabled = False
            'Me.txtNroSorteoConcurso.Enabled = False
        Else
            Me.grpExtracciones.Enabled = True
            Me.grpJuegos.Enabled = True
            Me.CboConcurso.Enabled = True
            Me.btnConcursoIniciar.Enabled = True
            Me.btnConcursoRevertir.Enabled = True
            'Me.txtJuegoRector.Enabled = True
            'Me.txtModeloExtracciones.Enabled = True
            'Me.txtNroSorteoConcurso.Enabled = True
        End If
    End Sub
#End Region



    Private Sub btnConcursoRevertir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConcursoRevertir.Click
        ' ahora invoco a la capa de negocio para que ejecute la logica de la confirmacion de la extraccion
        Dim PgmBO As New PgmConcursoBO
        Dim idExtraccionSig As Integer
        Try
            If MsgBox("¿Desea revertir el concurso '" & oPC.concurso.Descripcion & "'?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo, MDIContenedor.Text) = MsgBoxResult.No Then
                Exit Sub
            End If
            FileSystemHelper.Log(" ConcursoInicio:Inicio reversion concurso:" & oPC.idPgmConcurso)
            Me.Cursor = Cursors.WaitCursor
            idExtraccionSig = PgmBO.RevertirExtracciones(oPC, oPC.Operador, -1, , 1) 'no se envia la cabecera para que se revierta todo el concurso,el valor 1 es para se que borre el campo de importe pozo en la tabla de premio_sorteo
            'ConcursoInicio_Load(sender, e)
            BtnBuscarConcurso_Click(sender, e)
            btnConcursoRevertir.Enabled = False
            Me.Cursor = Cursors.Default
            FileSystemHelper.Log(" ConcursoInicio:reversion concurso OK:" & oPC.idPgmConcurso)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            If ex.Message.IndexOf("no se encuentra en estado requerido") > -1 Then
                MsgBox("No se realizó la reversión puesto que el sorteo no ha sido iniciado aún.", MsgBoxStyle.Information, MDIContenedor.Text)
                FileSystemHelper.Log(" ConcursoInicio:No se realizó la reversión puesto que el sorteo no ha sido iniciado aún.concurso :" & oPC.idPgmConcurso)
            Else
                MsgBox("Error:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
                FileSystemHelper.Log(" ConcursoInicio: Problema reversion concurso :" & oPC.idPgmConcurso & " " & ex.Message)
            End If
        End Try
    End Sub

    Private Sub ImprimirParametros(ByVal pIdpgmconcurso As Int32, Optional ByVal ConGanadores As String = "N", Optional ByVal nCopias As Integer = 1)
        Dim PgmBO As New PgmConcursoBO
        Dim dt As DataTable
        Dim ds As New DataSet

        Dim dt2 As DataTable
        Dim ds2 As New DataSet
        Try

            dt = PgmBO.ObtenerDatosExtraccionesCAB(pIdpgmconcurso)
            dt.TableName = "Parametros"
            'dt.WriteXmlSchema("D:\listadoparametros.xml")
            ds.Tables.Add(dt)

            dt = PgmBO.ObtenerDatosJuegos(pIdpgmconcurso)
            'dt.TableName = "JuegosParametros"
            'dt.WriteXmlSchema("D:\Visual2008\SorteosCAS\DEV\SorteosCAS\bin\listadoJuegosparametros.xml")
            ds.Tables.Add(dt)


            Dim path_reporte As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES"
            Dim reporte As New Listado
            reporte.GenerarParametros(ds, path_reporte, ConGanadores, nCopias)

        Catch ex As Exception
            FileSystemHelper.Log("Problemas al imprimir parametros del concurso:" & pIdpgmconcurso)
        End Try
    End Sub
    

    Private Sub txt1301001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt1301001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt1301002.Enabled And txt1301002.Visible Then txt1301002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt1301002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt1301002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt1301003.Enabled And txt1301003.Visible Then txt1301003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt1301003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt1301003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt1301005.Enabled And txt1301005.Visible Then txt1301005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub



    Private Sub txt1305001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txt1305001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt1305005.Enabled And txt1305005.Visible Then txt1305005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt3001001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt3001002.Enabled And txt3001002.Visible Then txt3001002.Focus()

        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt3001002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt3001003.Enabled And txt3001003.Visible Then txt3001003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt401001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt401002.Enabled And txt401002.Visible Then txt401002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt401002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt401003.Enabled And txt401003.Visible Then txt401003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt401003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt401004.Enabled And txt401004.Visible Then txt401004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt402001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt402002.Enabled And txt402002.Visible Then txt402002.Focus()

        End If
    End Sub

    Private Sub txt402002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt402003.Enabled And txt402003.Visible Then txt402003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt402003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt402004.Enabled And txt402004.Visible Then txt402004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt403001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt403002.Enabled And txt403002.Visible Then txt403002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt404001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If tabPozoJuego4.TabPages.Count = 5 Then
                If txt404002.Enabled And txt404002.Visible Then txt404002.Focus()
            Else
                Me.btnPozoGuardar4.Focus()
            End If
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt405001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt405002.Enabled And txt405002.Visible Then txt405002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt405002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt405005.Enabled And txt405005.Visible Then txt405005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub




    Private Sub txt407001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txt407002.Enabled And txt407002.Visible Then txt407002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub




    Private Sub btnAplicarAutoridadAlResto_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        If MsgBox("¿Desea aplicar la autoridades del juego actual a los demás juegos del concurso?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo, MDIContenedor.Text) = MsgBoxResult.Yes Then
            AplicarAutoridaAlResto()
        End If
    End Sub
    Private Sub AplicarAutoridaAlResto()
        Dim boAutoridad = New AutoridadBO
        Dim idpgmsorteo As Integer
        Dim boPgmConcurso As New PgmConcursoBO
        Dim msg As String
        Try

            idpgmsorteo = getControlJ("txtIdPgmSorteo").Text
            If boAutoridad.AplicarAutoridadAlResto(idpgmsorteo, _oPC) = False Then
                FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " Falló AplicarAutoridaAlResto en Concurso Inicio")
            Else


                If Not oPC Is Nothing Then
                    getValores()
                    If (boPgmConcurso.valida(oPC, msg)) Then
                        boPgmConcurso.setPgmConcurso(oPC)
                    Else
                        MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                    setControlesValoresConcurso(oPC)
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub




    Private Sub txt3001003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If btnPozoGuardar30.Enabled Then btnPozoGuardar30.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub






    Private Sub txt401004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego4.SelectedIndex = 1
            If Me.txt402001.Enabled Then txt402001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt402004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego4.SelectedIndex = 2
            If Me.txt403001.Enabled Then txt403001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt403002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego4.SelectedIndex = 3
            If Me.txt407001.Enabled Then txt407001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txt407002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego4.SelectedIndex = 4
            If Me.txt404001.Enabled Then txt404001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub






    Private Sub txt404002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If tabPozoJuego4.TabPages.Count = 6 Then
                Me.tabPozoJuego4.SelectedIndex = 4
                If Me.txt405001.Enabled Then txt404001.Focus()
            End If
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub





    Private Sub btnParametrosListar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnParametrosListar.EnabledChanged
        If btnParametrosListar.Enabled Then
            btnParametrosListar.BackgroundImageLayout = ImageLayout.Stretch
            btnParametrosListar.BackgroundImage = My.Resources.Imagenes.boton_normal

        Else
            btnParametrosListar.BackgroundImageLayout = ImageLayout.Stretch
            btnParametrosListar.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnParametrosListar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnParametrosListar.MouseDown
        btnParametrosListar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnParametrosListar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnParametrosListar.MouseHover
        btnParametrosListar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnParametrosListar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnParametrosListar.MouseLeave
        btnParametrosListar.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnConcursoIniciar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConcursoIniciar.EnabledChanged
        If btnConcursoIniciar.Enabled Then
            btnConcursoIniciar.BackgroundImageLayout = ImageLayout.Stretch
            btnConcursoIniciar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnConcursoIniciar.BackgroundImageLayout = ImageLayout.Stretch
            btnConcursoIniciar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If

    End Sub

    Private Sub btnConcursoIniciar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnConcursoIniciar.MouseDown
        btnConcursoIniciar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnConcursoIniciar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConcursoIniciar.MouseHover
        btnConcursoIniciar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnConcursoIniciar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConcursoIniciar.MouseLeave
        btnConcursoIniciar.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnConcursoRevertir_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConcursoRevertir.EnabledChanged
        If btnConcursoRevertir.Enabled Then
            btnConcursoRevertir.BackgroundImageLayout = ImageLayout.Stretch
            btnConcursoRevertir.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnConcursoRevertir.BackgroundImageLayout = ImageLayout.Stretch
            btnConcursoRevertir.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnConcursoRevertir_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnConcursoRevertir.MouseDown
        btnConcursoRevertir.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnConcursoRevertir_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConcursoRevertir.MouseHover
        btnConcursoRevertir.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnConcursoRevertir_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConcursoRevertir.MouseLeave
        btnConcursoRevertir.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub BtnBuscarConcurso_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBuscarConcurso.EnabledChanged
        If BtnBuscarConcurso.Enabled Then
            BtnBuscarConcurso.BackgroundImageLayout = ImageLayout.Stretch
            BtnBuscarConcurso.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            BtnBuscarConcurso.BackgroundImageLayout = ImageLayout.Stretch
            BtnBuscarConcurso.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub BtnBuscarConcurso_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnBuscarConcurso.MouseDown
        BtnBuscarConcurso.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub BtnBuscarConcurso_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBuscarConcurso.MouseHover
        BtnBuscarConcurso.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub BtnBuscarConcurso_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnBuscarConcurso.MouseLeave
        BtnBuscarConcurso.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnSalir_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.EnabledChanged
        If btnSalir.Enabled Then
            btnSalir.BackgroundImageLayout = ImageLayout.Stretch
            btnSalir.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnSalir.BackgroundImageLayout = ImageLayout.Stretch
            btnSalir.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnSalir_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnSalir.MouseDown
        btnSalir.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnSalir_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.MouseHover
        btnSalir.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnSalir_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnSalir.MouseLeave
        btnSalir.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnPozoGuardar13_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar13.EnabledChanged
        If btnPozoGuardar13.Enabled Then
            btnPozoGuardar13.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoGuardar13.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPozoGuardar13.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoGuardar13.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnPozoGuardar13_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPozoGuardar13.MouseDown
        btnPozoGuardar13.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPozoGuardar13_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar13.MouseHover
        btnPozoGuardar13.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPozoGuardar13_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar13.MouseLeave
        btnPozoGuardar13.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnPozoGuardar30_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar30.EnabledChanged
        If btnPozoGuardar30.Enabled Then
            btnPozoGuardar30.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoGuardar30.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPozoGuardar30.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoGuardar30.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnPozoGuardar30_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPozoGuardar30.MouseDown
        btnPozoGuardar30.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPozoGuardar30_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar30.MouseHover
        btnPozoGuardar30.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPozoGuardar30_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar30.MouseLeave
        btnPozoGuardar30.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnPozoGuardar4_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar4.EnabledChanged
        If btnPozoGuardar4.Enabled Then
            btnPozoGuardar4.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoGuardar4.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPozoGuardar4.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoGuardar4.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPozoGuardar4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPozoGuardar4.MouseDown
        btnPozoGuardar4.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPozoGuardar4_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar4.MouseHover
        btnPozoGuardar4.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPozoGuardar4_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoGuardar4.MouseLeave
        btnPozoGuardar4.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnPozoObtener13_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener13.EnabledChanged
        If btnPozoObtener13.Enabled Then
            btnPozoObtener13.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoObtener13.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPozoObtener13.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoObtener13.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPozoObtener13_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPozoObtener13.MouseDown
        btnPozoObtener13.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPozoObtener13_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener13.MouseHover
        btnPozoObtener13.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPozoObtener13_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener13.MouseLeave
        btnPozoObtener13.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnPozoObtener30_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener30.EnabledChanged
        If btnPozoObtener30.Enabled Then
            btnPozoObtener30.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoObtener30.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPozoObtener30.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoObtener30.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnPozoObtener30_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPozoObtener30.MouseDown
        btnPozoObtener30.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPozoObtener30_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener30.MouseHover
        btnPozoObtener30.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPozoObtener30_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener30.MouseLeave
        btnPozoObtener30.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnPozoObtener4_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener4.EnabledChanged
        If btnPozoObtener4.Enabled Then
            btnPozoObtener4.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoObtener4.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPozoObtener4.BackgroundImageLayout = ImageLayout.Stretch
            btnPozoObtener4.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPozoObtener4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPozoObtener4.MouseDown
        btnPozoObtener4.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPozoObtener4_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener4.MouseHover
        btnPozoObtener4.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPozoObtener4_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPozoObtener4.MouseLeave
        btnPozoObtener4.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    '**
    Private Sub botones_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs)
        Dim boton As Button
        boton = CType(sender, Button)
        boton.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub botones_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim boton As Button
        boton = CType(sender, Button)
        boton.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub botones_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim boton As Button
        boton = CType(sender, Button)
        boton.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub
    Private Sub botones_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnConcursoIniciar.EnabledChanged
        Dim boton As Button
        boton = CType(sender, Button)
        If boton.Enabled Then
            boton.BackgroundImageLayout = ImageLayout.Stretch
            boton.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            boton.BackgroundImageLayout = ImageLayout.Stretch
            boton.BackgroundImage = My.Resources.Imagenes.boton_off
        End If

    End Sub



    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick

        Try
            Dim ventana As New Publicador.FrmAccionTimer
            Dim BOpgmconcurso As New PgmConcursoBO
            Dim estado As Integer = 0
            Dim path_archivos_Pozos As String = ""
            Dim opgmsorteo As PgmSorteo
            Dim juegoact As String = ""
            Dim nrosorteo As String = ""
            Dim nombre_Archivo As String = ""
            Dim prefijoPozo As String = General.PrefijoPozo
            Dim entro_juego_pozo As Boolean = False
            Dim gralDal As New Sorteos.Data.GeneralDAL
            Dim faltan_cargar_pozos As Boolean = False
            Dim BOpgmsorteo As New PgmSorteoBO
            Dim segundos As Integer

            Timer1.Stop()

            segundos = opublicador.ObtenerTiempoTimer_pozos(True)
            For Each opgmsorteo In oPC.PgmSorteos
                'las loterias no cargan pozos
                If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Then
                    If BOpgmsorteo.NoTienePozos(opgmsorteo.idPgmSorteo, opgmsorteo.idJuego) Then
                        faltan_cargar_pozos = True
                    End If
                End If
            Next
            If Not faltan_cargar_pozos Then
                Exit Sub
            End If
            If Archivo_Pozo_encontrado = False Then
                estado = oPC.estadoPgmConcurso
                If estado = 10 Then
                    path_archivos_Pozos = gralDal.getParametro("INI", "PATH_POZOS")
                    If Not path_archivos_Pozos.EndsWith("\") Then
                        path_archivos_Pozos = path_archivos_Pozos & "\"
                    End If
                    For Each opgmsorteo In oPC.PgmSorteos 'opgmconcurso.PgmSorteos
                        'las loterias no cargan pozos
                        If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Then
                            entro_juego_pozo = True
                            juegoact = opgmsorteo.idJuego.ToString.PadLeft(2, "00")
                            nrosorteo = opgmsorteo.nroSorteo.ToString.PadLeft(6, "000000")
                            nombre_Archivo = path_archivos_Pozos & prefijoPozo & juegoact & nrosorteo & ".zip"
                            If File.Exists(nombre_Archivo) Then
                                'pone la bandera en true
                                Archivo_Pozo_encontrado = True
                                ' y llama al formulario para mostrar el msj
                                ventana.leyenda = "Archivo de pozos detectado." & vbCrLf
                                ventana.ShowDialog()
                                'accion 0:acepto,1 avisar mas tarde
                                'si acepto detengo el timar
                                If ventana.vAccion = 0 Then
                                    ventana.Dispose()
                                    Exit Sub
                                End If
                            End If
                        End If
                    Next
                End If

            Else 'el archivo ya fue encontrado asi que vuelvo a mostrar el formulario
                ventana.leyenda = "Archivo de pozos detectado." & vbCrLf
                ventana.ShowDialog()
                'accion 0:acepto,1 avisar mas tarde
                'si acepto detengo el timar
                If ventana.vAccion = 0 Then
                    ventana.Dispose()
                    Exit Sub
                End If
            End If
            Timer1.Interval = segundos * 1000
            Timer1.Start()

        Catch ex As Exception
            Throw New Exception
        End Try
    End Sub
    Public Sub setGrillaJurisdicciones(Optional ByVal pidpgmsorteo As Integer = -1)
        Dim dgv As DataGridView
        Dim boloteria = New PgmSorteoLoteriaBO
        Dim boConcurso As New PgmConcursoBO
        Dim listaJuegoAutoridades As New ListaOrdenada(Of pgmSorteo_loteria)
        Dim _idpgmSorteo As Long
        dgv = getControlJ("dgvJurisdiccionJuego")
        dgv.Columns.Clear()

        dgv.EditMode = DataGridViewEditMode.EditProgrammatically
        dgv.AllowUserToOrderColumns = False
        dgv.AllowUserToAddRows = False
        dgv.AllowUserToResizeColumns = False
        dgv.AllowUserToResizeRows = False
        dgv.AutoGenerateColumns = False
        dgv.RowHeadersVisible = False
        dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        If pidpgmsorteo < 0 Then
            _idpgmSorteo = getControlJ("txtIdPgmSorteo").Text
            listaJuegoAutoridades = boloteria.getSorteosLoteria(getControlJ("txtIdPgmSorteo").Text)
            dgv.DataSource = listaJuegoAutoridades
        Else
            _idpgmSorteo = pidpgmsorteo
            listaJuegoAutoridades = boloteria.getSorteosLoteria(pidpgmsorteo)
            dgv.DataSource = listaJuegoAutoridades
        End If
        boConcurso.setPgmSorteo_Loterias(oPC, _idpgmSorteo, listaJuegoAutoridades)

        dgv.Columns.Add("0", "Codigo")
        dgv.Columns(0).Width = 0
        dgv.Columns(0).DataPropertyName = "idloteria"
        dgv.Columns(0).Visible = False
        dgv.Columns.Add("1", "Nombre")
        dgv.Columns(1).Width = 140
        dgv.Columns(1).DataPropertyName = "NombreLoteria"
        dgv.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
        dgv.ClearSelection()

    End Sub
    Private Sub btnJurisdiccionJuegoAgregar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        JurisdiccionJuegoAgregar()
    End Sub

    Private Sub btnJurisdiccionJuegoQuitar_Click(ByVal sender As Object, ByVal e As System.EventArgs)
        JurisdiccionJuegoQuitar()
    End Sub
    Private Sub JurisdiccionJuegoAgregar()
        Dim frmJurisdiccionesABM As New FrmOtrasJurisdiccionesABM
        Dim boAutoridad As New AutoridadBO

        Try
            'dispara el formulario

            frmJurisdiccionesABM.idPgmSorteo = getControlJ("txtIdPgmSorteo").Text

            frmJurisdiccionesABM.ShowDialog()

            ' recarga la grilla 
            setGrillaJurisdicciones()

            frmJurisdiccionesABM = Nothing


        Catch ex As Exception
            MsgBox("Problema al intentar agregar la jurisdicción: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Sub JurisdiccionJuegoQuitar()
        Dim dgv As DataGridView
        Dim bopgmsorteo_loteria = New PgmSorteoLoteriaBO
        Dim opgmsorteo_loteria = New pgmSorteo_loteria
        Dim _idPgmSorteo As Integer
        Dim opgmsorteo As New PgmSorteo
        Dim bopgmsorteo As New PgmSorteoBO
        Try


            dgv = getControlJ("dgvJurisdiccionJuego")
            If Not dgv.CurrentRow Is Nothing Then
                opgmsorteo_loteria = dgv.CurrentRow.DataBoundItem
                If MsgBox("¿Está seguro que desea quitar la Jurisdicción '" & opgmsorteo_loteria.Loteria.Nombre & "' del sorteo? ", MsgBoxStyle.YesNo, MDIContenedor.Text) = Windows.Forms.DialogResult.Yes Then

                    _idPgmSorteo = getControlJ("txtIdPgmSorteo").Text
                    bopgmsorteo_loteria.QuitarSorteoLoteriaInicio(opgmsorteo_loteria.Loteria.IdLoteria, _idPgmSorteo)
                    setGrillaJurisdicciones()
                End If
            End If
        Catch ex As Exception
            MsgBox("Problemas al quitar Jurisdicción: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Function IniciaTimer(ByVal opgmConcurso As PgmConcurso) As Boolean
        Dim oPgmSorteoBO As New PgmSorteoBO
        Dim valor As Boolean = False
        Try
            Dim opgmsorteo As PgmSorteo
            For Each opgmsorteo In opgmConcurso.PgmSorteos
                If oPgmSorteoBO.NoTienePozos(opgmsorteo.idPgmSorteo, oPC.concurso.IdConcurso) Then

                    If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Then
                        valor = True
                        Return valor
                        Exit Function
                    End If
                End If
            Next
        Catch ex As Exception
            valor = False
        End Try
        Return valor
    End Function

    Private Sub txtApu3001001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApu1301001.TextChanged
        txtTotApu30.Text = txtApu3001001.Text
    End Sub

    Private Sub txtApu1301001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApu1301001.TextChanged
        txtTotApu13.Text = txtApu1301001.Text
    End Sub


    Private Sub txt1301001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1301001.TextChanged
        txtTotPozo13.Text = actualizaTotPozo()
    End Sub

    Private Sub txt1301002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1301002.TextChanged
        txtTotPozo13.Text = actualizaTotPozo()
    End Sub

    Private Sub txt1301003_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1301003.TextChanged
        txtTotPozo13.Text = actualizaTotPozo()
    End Sub

    Private Sub txt1301004_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1301004.TextChanged
        txtTotPozo13.Text = actualizaTotPozo()
    End Sub

    Private Sub txt1301005_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1301005.TextChanged
        txtTotPozo13.Text = actualizaTotPozo()
    End Sub

    Private Sub txt1305001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1305001.TextChanged
        txtTotPozo13.Text = actualizaTotPozo()
    End Sub

    Private Sub txt1305005_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt1305005.TextChanged
        txtTotPozo13.Text = actualizaTotPozo()
    End Sub

    Private Function actualizaTotApu() As String
        Dim boPozo As New PozoBO
        Dim lista As List(Of Pozo)
        Dim modalidad As String
        Dim tot_pozo As Double = 0.0

        lista = boPozo.getPozo(getIdJuegoActual(), getNroSorteoActual())

        For Each oPozo In lista
            modalidad = IIf(Len(CStr(oPozo.idPremio)) = 7, Mid(oPozo.idPremio, 1, 4), Mid(oPozo.idPremio, 1, 3))
            Try
                If (modalidad Mod 100) = 1 And (Not CType(getControlJA("pstApu" & modalidad, "txtApu" & oPozo.idPremio), TextBox).ReadOnly) Then
                    tot_pozo = tot_pozo + getControlJA("pstApu" & modalidad, "txtApu" & oPozo.idPremio).Text
                End If
            Catch ex As Exception
            End Try
        Next
        Return tot_pozo.ToString("N0", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
    End Function

    Private Function actualizaTotPozo() As String
        Dim boPozo As New PozoBO
        Dim lista As List(Of Pozo)
        Dim modalidad As String
        Dim tot_pozo As Double = 0.0


        lista = boPozo.getPozo(getIdJuegoActual(), getNroSorteoActual())

        For Each oPozo In lista
            modalidad = IIf(Len(CStr(oPozo.idPremio)) = 7, Mid(oPozo.idPremio, 1, 4), Mid(oPozo.idPremio, 1, 3))
            Try
                tot_pozo = tot_pozo + getControlTabGP("pst" & modalidad, "txt" & oPozo.idPremio).Text
            Catch ex As Exception
            End Try
        Next
        Return tot_pozo.ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
    End Function

    Private Function actualizaTotPozoReal() As String
        Dim boPozo As New PozoBO
        Dim lista As List(Of Pozo)
        Dim modalidad As String
        Dim tot_pozo As Double = 0.0


        lista = boPozo.getPozo(getIdJuegoActual(), getNroSorteoActual())

        For Each oPozo In lista
            modalidad = IIf(Len(CStr(oPozo.idPremio)) = 7, Mid(oPozo.idPremio, 1, 4), Mid(oPozo.idPremio, 1, 3))
            Try
                tot_pozo = tot_pozo + getControlTabGPR("pstPozoReal" & modalidad, "txtPozoReal" & oPozo.idPremio).Text
            Catch ex As Exception
            End Try
        Next
        Return tot_pozo.ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
    End Function

    Private Sub txtPozoReal1301001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPozoReal1301001.TextChanged
        txtTotPozoReal13.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal1301002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPozoReal1301002.TextChanged
        txtTotPozoReal13.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal1301003_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPozoReal1301003.TextChanged
        txtTotPozoReal13.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal1301004_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPozoReal1301004.TextChanged
        txtTotPozoReal13.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal1301005_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPozoReal1301005.TextChanged
        txtTotPozoReal13.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal1305001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPozoReal1305001.TextChanged
        txtTotPozoReal13.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal1305005_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPozoReal1305005.TextChanged
        txtTotPozoReal13.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal405001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal405002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal405005_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal404001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal404002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal407001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal407002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal403001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal403002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal402001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal402002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal402003_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal402004_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal401001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal401002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal401003_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal401004_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal4.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txt405001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt405002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt405005_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt404001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt404002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt407001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt407002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt403001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt403002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt402001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt402002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt402003_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt402004_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt401001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt401002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt401003_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt401004_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozo4.Text = actualizaTotPozo()
    End Sub

    Private Sub txt3001001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt3001001.TextChanged
        txtTotPozo30.Text = actualizaTotPozo()
    End Sub

    Private Sub txt3001002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt3001002.TextChanged
        txtTotPozo30.Text = actualizaTotPozo()
    End Sub

    Private Sub txt3001003_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txt3001003.TextChanged
        txtTotPozo30.Text = actualizaTotPozo()
    End Sub

    Private Sub txtPozoReal3001001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal30.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal3001002_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal30.Text = actualizaTotPozoReal()
    End Sub

    Private Sub txtPozoReal3001003_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        txtTotPozoReal30.Text = actualizaTotPozoReal()
    End Sub

    ' apuestas tradicional. aplican a: segunda y adicional 
    Private Sub txtApu401001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApu401001.TextChanged
        txtTotApu4.Text = actualizaTotApu()
        txtApu402001.Text = txtApu401001.Text
        txtApu405001.Text = txtApu401001.Text
    End Sub

    ' apuestas revancha. aplican tambien a: extra
    Private Sub txtApu403001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApu403001.TextChanged
        txtTotApu4.Text = actualizaTotApu()
        txtApu404001.Text = txtApu403001.Text
    End Sub

    ' apuestas s sale. 
    Private Sub txtApu407001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtApu407001.TextChanged
        txtTotApu4.Text = actualizaTotApu()
    End Sub

    Private Sub btncriteriosPozos_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btncriteriosPozos.Click
        Dim oPgmSorteoBO As New PgmSorteoBO
        Dim oSor As New PgmSorteo

        Try
            If oPgmSorteoBO.NoTienePozos(oPC.idPgmSorteoPrincipal, oPC.concurso.IdConcurso) Then
                MsgBox("Los Pozos del sorteo actual aún no han sido registrado. Cargue primero estos datos y vuelva a intentar.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            Select Case Math.Floor((oPC.idPgmSorteoPrincipal / 1000000))
                Case 4
                    frmParametrosPozoEstimadoQ6.v_oSorteo = oPgmSorteoBO.getPgmSorteo(oPC.idPgmSorteoPrincipal)
                    frmParametrosPozoEstimadoQ6.ShowDialog()
                Case 13
                    frmParametrosPozoEstimadoBR.v_oSorteo = oPgmSorteoBO.getPgmSorteo(oPC.idPgmSorteoPrincipal)
                    frmParametrosPozoEstimadoBR.ShowDialog()
                Case Else
                    MsgBox("Este juego no tiene habilitada la Estimación de Próx. Pozo.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
            End Select

            'actualizo propiedad ParPozoConfirmado
            oSor = oPgmSorteoBO.getPgmSorteo(oPC.idPgmSorteoPrincipal)
            For Each opgmsorteo In oPC.PgmSorteos
                If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Then 'Or opgmsorteo.idJuego = 30 
                    opgmsorteo.ParProxPozoConfirmado = oSor.ParProxPozoConfirmado
                End If
            Next
        Catch ex As Exception

        End Try

    End Sub

    
End Class
