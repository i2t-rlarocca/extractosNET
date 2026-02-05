Imports sorteos.helpers
Imports libEntities.Entities
Imports sorteos.bussiness
Imports Sorteos.Extractos
Imports System.Net


Public Class FrmConcursoFinalizar

    Private estaEnLoad As Boolean = False
    Private _oPC As PgmConcurso
    Private _inicio As Boolean
    Dim TagPages As Collection
    Dim HoraInicioExtraccionActual As New Date
    Dim HoraFinExtraccionActual As New Date
    Dim _modificado As Boolean = False
    Dim _cargando As Boolean = True
    Dim JuegosModificadosAnt As New ListaOrdenada(Of Integer)
    '22/10/2012 para mostar el pozo estimao o sugerido
    Dim pozoEstimado As String = ""
    Dim ImportePozoEstimado As Double = 0
    Dim PozoBo As New PozoBO
    Dim boPremio As New PremioBO
    Dim vNotienePremios As Boolean = False
    Dim JuegosExtractocargados As New ListaOrdenada(Of cValorPosicion)
    Dim JuegosModificados As New ListaOrdenada(Of cValorPosicion)
    Dim vienedepestania As Boolean = False

    Dim TabJuegosFuente As Font = New Font("Myriad Web Pro", 11, FontStyle.Regular)
    Dim TabJuegosLetra As Font = New Font("Myriad Web Pro", 10, FontStyle.Regular)
    Dim TabJuegosLetraNegrita As Font = New Font("Myriad Web Pro", 11, FontStyle.Bold)
    'Dim TabJuegosLetra8 As Font = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
    'Dim TabJuegosLetra8N As Font = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)

    Dim pestaniaActiva As Integer = 0

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

    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        btnAnteriores.BackColor = System.Drawing.SystemColors.Control
        btnAnteriores.ForeColor = Color.FromArgb(52, 118, 166)
        btnAnteriores.FlatStyle = FlatStyle.Flat
        btnAnteriores.BackgroundImageLayout = ImageLayout.Stretch
        btnAnteriores.BackgroundImage = My.Resources.Imagenes.boton_normal

        AddHandler btnAnteriores.MouseDown, AddressOf botones_MouseDown
        AddHandler btnAnteriores.MouseHover, AddressOf botones_MouseHover
        AddHandler btnAnteriores.EnabledChanged, AddressOf botones_EnabledChanged
        AddHandler btnAnteriores.MouseLeave, AddressOf botones_MouseLeave
    End Sub

    Private Sub FrmConcursoFinalizar_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub

    Private Sub FrmConcursoFinalizar_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        inicio = True
        estaEnLoad = True
        PTBreloj.Visible = True
        _cargando = True
        Try

            Dim d As DateTime = New Date(Date.Now.Ticks)
            Me.Location = New System.Drawing.Point(0, 0)
            DTPFechaConcurso.Value = d.ToString("dd/MM/yyyy")
            DTPFechaConcurso.MinDate = String.Format("{0:dd/MM/yyyy}", d.AddDays(General.DiasSorteosAnteriores * -1).ToShortDateString)
            DTPFechaConcurso.MaxDate = String.Format("{0:dd/MM/yyyy}", d.AddDays(30).ToShortDateString)
            dtpHoraConcurso.Value = d.ToString("dd/MM/yyyy HH:mm:ss")
            dtpHoraConcurso.MinDate = DTPFechaConcurso.MinDate
            dtpHoraConcurso.MaxDate = DTPFechaConcurso.MaxDate
            Application.DoEvents()
            setValorListaConcurso(True)
            'If oPC IsNot Nothing Then
            '    setControlesValoresConcurso(oPC)
            'End If
            _cargando = False
        Catch ex As Exception
            MsgBox("Problema en load" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
        PTBreloj.Visible = False
        inicio = False
        estaEnLoad = False

    End Sub

    Private Sub setValorListaConcurso(Optional ByVal soloUltimo As Boolean = False)
        Dim listaPC As ListaOrdenada(Of PgmConcurso)
        Dim _fechaHora As DateTime
        Dim boPgmConcurso As New PgmConcursoBO
        Try
            'JuegosModificados = New ListaOrdenada(Of Integer)
            _fechaHora = DTPFechaConcurso.Value
            _fechaHora = _fechaHora.AddHours(-1 * DTPFechaConcurso.Value.Hour).AddMinutes(-1 * DTPFechaConcurso.Value.Minute).AddSeconds(-1 * DTPFechaConcurso.Value.Second)
            If Trim(_fechaHora) = "" Then
                MsgBox("Ingrese una fecha para el sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            If Trim(dtpHoraConcurso.Value) = "" Then
                MsgBox("Ingrese una hora para el sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            Me.Cursor = Cursors.WaitCursor
            MDIContenedor.Cursor = Cursors.WaitCursor
            Me.Refresh()
            _fechaHora = _fechaHora.AddHours(dtpHoraConcurso.Value.Hour).AddMinutes(dtpHoraConcurso.Value.Minute).AddSeconds(dtpHoraConcurso.Value.Second)
            ' consulta y carga la lista
            listaPC = boPgmConcurso.getPgmConcursoFinalizado(_fechaHora, soloUltimo)
            MDIContenedor.CerrarHijo = False
            If listaPC.Count = 0 Then
                MsgBox("No hay Concursos programados en condiciones de ser Confirmados.", MsgBoxStyle.Information, MDIContenedor.Text)
                PTBreloj.Visible = False
                Me.Cursor = Cursors.Default
                MDIContenedor.Cursor = Cursors.Default
                Me.Refresh()
                MDIContenedor.CerrarHijo = True
                Exit Sub
            Else 'habilita controles por las dudas
                'DeshabilitarControles(True)
            End If
            CboConcurso.ValueMember = "idPgmConcurso"
            CboConcurso.DisplayMember = "nombre"
            CboConcurso.DataSource = listaPC
            CboConcurso.Refresh()
            oPC = listaPC(0)
            'setControlesValoresConcurso(listaPC(0))
            Me.Cursor = Cursors.Default
            MDIContenedor.Cursor = Cursors.Default
            Me.Refresh()
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MDIContenedor.Cursor = Cursors.Default
            Me.Refresh()
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub setControlesValoresConcurso(ByVal oPgmConc As PgmConcurso)
        Dim oPCBO As New PgmConcursoBO
        Dim oPgmSorteoBO As New PgmSorteoBO
        Dim oPgmSorteo As PgmSorteo
        Dim _tienePremios As Boolean = True
        Dim _tieneJurisdiccionesCargadas As Boolean = True
        Dim _regenera As Boolean = False
        Dim _juegoextracto As cValorPosicion
        Dim usuario As String = ""
        ' se presentan los valores de los datos fijos
        oPC = oPgmConc

        'controla jurisdicciones y premio
        For Each oPgmSorteo In oPC.PgmSorteos
            '*** inilizaza la lista de juegos extractos
            _juegoextracto = New cValorPosicion

            '*** carga el juego y valor -1(no se cargo extracto) 
            _juegoextracto.Posicion = oPgmSorteo.idJuego
            _juegoextracto.Valor = -1
            JuegosExtractocargados.Add(_juegoextracto)

            '********* 
            If oPgmSorteo.idJuego = 30 Or oPgmSorteo.idJuego = 4 Or oPgmSorteo.idJuego = 13 Then
                _regenera = True
            End If
        Next
        '' 'Comento a ver que pasa
        ''If _regenera Then 'actualiza el objeto de memoria para refresacar los cambios en pozos
        ''    oPC = oPCBO.getPgmConcurso(oPC.idPgmConcurso)
        ''End If
        vNotienePremios = False
        If Not _tienePremios Then
            ' MsgBox("El concurso '" & oPC.nombre & "' no tiene premios cargados.", MsgBoxStyle.Information)
            '**18/12/2012 si no tiene premios se deshabilita el boton confirmar
            vNotienePremios = True
        End If

        DTPFechaConcurso.Value = oPC.fechaHora
        dtpHoraConcurso.Value = oPC.fechaHora
        'se crean los controles dependientes del concurso
        Me.Cursor = Cursors.WaitCursor
        MDIContenedor.Cursor = Cursors.WaitCursor
        setControles()
        setValores()
        PTBreloj.Visible = False
        grpJuegos.Visible = True
        Me.Cursor = Cursors.Default
        MDIContenedor.Cursor = Cursors.Default
    End Sub

    ' presenta en el formulario los valores de la entidad oPC
    Private Sub setValores()
        Dim tab As TabControl
        Dim DataPicker As DateTimePicker
        Dim Boton As Button
        Dim habilitarControl As Boolean
        Dim text As TextBox


        habilitarControl = True 'IIf(oPC.estadoPgmConcurso = 40, False, True)

        tab = Me.Controls("grpJuegos").Controls("tabJuegos")

        ' valores de las pestañas tabJuegos
        For Each oSorteo In oPC.PgmSorteos

            ' localiza y hace activa la pestaña
            tab.SelectTab("pstJuego-" & oSorteo.idJuego)

            DataPicker = getControlJ("grpActual", "dtpHoraInicioJuego")
            DataPicker.Value = oSorteo.fechaHoraIniReal
            DataPicker.Enabled = habilitarControl

            DataPicker = getControlJ("grpActual", "dtpHoraFinJuego")
            DataPicker.Value = oSorteo.fechaHoraFinReal
            DataPicker.Enabled = habilitarControl

            DataPicker = getControlJ("grpActual", "dtpFechaPrescripcion")
            DataPicker.Value = oSorteo.fechaHoraPrescripcion
            DataPicker.Enabled = habilitarControl

            DataPicker = getControlJ("grpActual", "dtpHoraPrescripcion")
            DataPicker.Value = oSorteo.fechaHoraPrescripcion
            DataPicker.Enabled = habilitarControl
            DataPicker.Visible = False ' RL: 21-08-2012 - Incid 2737: no mostrar hora prescripcion

            DataPicker = getControlJ("grpProximo", "dtpFechaProximo")
            DataPicker.Value = oSorteo.fechaHoraProximo
            DataPicker.Enabled = habilitarControl

            DataPicker = getControlJ("grpProximo", "dtpHoraProximo")
            DataPicker.Value = oSorteo.fechaHoraProximo
            DataPicker.Enabled = habilitarControl
            text = getControlJ("grpProximo", "txtPozo")
            '*** 22/10/2012 para la poceada federal se muestra el pozo estimado o sugerido y no se puede modificar
            If oSorteo.idJuego = 30 Then
                text.Text = IIf((ImportePozoEstimado = 0), "", ImportePozoEstimado)
                text.Enabled = False
            Else

                text.Text = oSorteo.PozoEstimado
            End If

            text.MaxLength = 13

        Next
        tab.SelectTab(0)

    End Sub

    Private Sub crearPestaniasJuegos()
        Dim boJuego As New JuegoBO
        Dim BoSorteo As New PgmSorteoBO

        Dim Pestania As TabPage

        ' agrega las pestañas al tabJuegos
        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " INICIO crearPestaniasJuegos")

        Me.TabJuegos.Controls.Clear()
        For Each oSorteo In oPC.PgmSorteos
            Pestania = crearPestaniaJuego(oSorteo)
            If Pestania IsNot Nothing Then
                TabJuegos.Controls.Add(Pestania)
            End If
        Next

        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " FIN crearPestaniasJuegos")
    End Sub

    Private Function crearPestaniaJuego(ByRef oSorteo As PgmSorteo) As TabPage
        Dim Fuente As Font = TabJuegosFuente
        Dim Letra As Font = TabJuegosLetra
        Dim LetraNegrita As Font = TabJuegosLetraNegrita
        'Dim Letra8 As Font= TabJuegosLetra8
        'Dim Letra8N As Font= TabJuegosLetra8N

        Dim boJuego As New JuegoBO
        Dim sorteoBO As New PgmSorteoBO

        Dim TabJ As TabControl
        Dim PestaniaJ As New TabPage

        Dim GrpActual As GroupBox
        Dim GrpProximo As GroupBox
        Dim GrpExtracto As GroupBox

        Dim Etiqueta As Label
        Dim DataPicker As DateTimePicker
        Dim wb As WebBrowser
        Dim Boton As Button
        Dim panelmsj As Panel
        Dim txtPozoEstimado As TextBox
        Dim lblPozoEstimado As Label

        Dim _extractolocal As Boolean = False
        Dim pozovisible As Boolean = False

        Dim msjvarios As String = ""
        Dim _sinPremios As Boolean = False
        Dim _publicarwebOFF As String = General.PublicarWebOFF
        Dim _publicarwebON As String = General.PublicarWebON
        Dim url As String = ""

        Dim pestaniaActiva As Integer = 0
        Dim _DeshabilitaBotonConfirmarExtracto As Boolean = False

        ' agrega las Pestanias al tabJuegos
        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " INICIO crearPestaniaJuego")

        TabJ = Me.grpJuegos.Controls("tabJuegos")

        'ObtenerIndiceResolucion(_indice, _left, _AlturaGrilla)

        ' Pestania
        PestaniaJ = New TabPage
        PestaniaJ.BackColor = Color.Transparent
        PestaniaJ.Name = "pstJuego-" & oSorteo.idJuego
        PestaniaJ.Text = "" & boJuego.getJuegoDescripcion(oSorteo.idJuego)
        PestaniaJ.Font = Fuente
        PestaniaJ.BackColor = Color.FromArgb(239, 239, 239)
        'PestaniaJ.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
        'PestaniaJ.Height = Me.TabJuegos.Height - 3
        'PestaniaJ.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
        PestaniaJ.Tag = oSorteo

        ' ****  Cabecera   ****
        ' Seccion Sorteo Actual
        GrpActual = New GroupBox
        GrpActual.Name = "grpActual-" & oSorteo.idJuego
        GrpActual.Text = "Sorteo Actual"
        GrpActual.Location = New System.Drawing.Point(2, 2)
        GrpActual.Size = New System.Drawing.Size(460, 78)
        GrpActual.Font = Letra
        GrpActual.ForeColor = Color.FromArgb(90, 90, 90)
        GrpActual.Visible = True
        GrpActual.Controls.Add(CrearEtiqueta("lblHoraInicioReal" & oSorteo.idJuego, "Hora Inicio Real:", 4, 20, 104, Letra))

        DataPicker = New DateTimePicker
        DataPicker.Name = "dtpHoraInicioJuego-" & oSorteo.idJuego
        DataPicker.Location = New System.Drawing.Point(110, 17)
        DataPicker.Size = New System.Drawing.Size(55, 24)
        DataPicker.Format = DateTimePickerFormat.Custom
        DataPicker.CustomFormat = "HH:mm"
        DataPicker.ShowUpDown = True
        AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
        GrpActual.Controls.Add(DataPicker)
        GrpActual.Controls.Add(CrearEtiqueta("lblHoraFinReal" & oSorteo.idJuego, "Hora Fin Real:", 306, 20, 89, Letra))

        DataPicker = New DateTimePicker
        DataPicker.Name = "dtpHoraFinJuego-" & oSorteo.idJuego
        DataPicker.Location = New System.Drawing.Point(395, 17)
        DataPicker.Size = New System.Drawing.Size(55, 24)
        DataPicker.Format = DateTimePickerFormat.Custom
        DataPicker.CustomFormat = "HH:mm"
        DataPicker.ShowUpDown = True
        DataPicker.Visible = True
        AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
        GrpActual.Controls.Add(DataPicker)
        GrpActual.Controls.Add(CrearEtiqueta("lblfechaPrescripcion" & oSorteo.idJuego, "F.Prescripción:", 11, 51, 97, Letra))

        DataPicker = New DateTimePicker
        DataPicker.Name = "dtpFechaPrescripcion-" & oSorteo.idJuego
        DataPicker.Location = New System.Drawing.Point(110, 47)
        DataPicker.Size = New System.Drawing.Size(233, 20)
        DataPicker.Format = DateTimePickerFormat.Long
        DataPicker.CustomFormat = "dd/MM/yyyy"
        DataPicker.ShowUpDown = True
        DataPicker.Visible = True
        AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
        GrpActual.Controls.Add(DataPicker)
        '' GrpActual.Controls.Add(CrearEtiqueta("lblhoraPrescripcion" & idJuego, "Hora:", 354, 51, 40, Letra))

        DataPicker = New DateTimePicker
        DataPicker.Name = "dtpHoraPrescripcion-" & oSorteo.idJuego
        DataPicker.Location = New System.Drawing.Point(395, 47)
        DataPicker.Size = New System.Drawing.Size(55, 20)
        DataPicker.Format = DateTimePickerFormat.Custom
        DataPicker.CustomFormat = "HH:mm"
        DataPicker.ShowUpDown = True
        DataPicker.Visible = False
        AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
        GrpActual.Controls.Add(DataPicker)

        PestaniaJ.Controls.Add(GrpActual)

        ' Seccion Sorteo Proximo
        GrpProximo = New GroupBox
        GrpProximo.Name = "grpProximo-" & oSorteo.idJuego
        GrpProximo.Text = "Próximo Sorteo"
        GrpProximo.Location = New System.Drawing.Point(470, 2)
        GrpProximo.Size = New System.Drawing.Size(300, 78)
        GrpProximo.Anchor = AnchorStyles.Left Or AnchorStyles.Top
        GrpProximo.Font = Letra
        GrpProximo.ForeColor = Color.FromArgb(90, 90, 90)
        GrpProximo.Visible = True
        GrpProximo.Controls.Add(CrearEtiqueta("lblfechaProximo" & oSorteo.idJuego, "Fecha:", 6, 20, 47, Letra))

        DataPicker = New DateTimePicker
        DataPicker.Name = "dtpFechaProximo-" & oSorteo.idJuego
        DataPicker.Location = New System.Drawing.Point(56, 17)
        DataPicker.Size = New System.Drawing.Size(233, 20)
        DataPicker.Format = DateTimePickerFormat.Long
        DataPicker.CustomFormat = "dd/MM/yyyy"
        DataPicker.ShowUpDown = True
        DataPicker.Visible = True
        AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
        GrpProximo.Controls.Add(DataPicker)
        GrpProximo.Controls.Add(CrearEtiqueta("lblHoraProximo" & oSorteo.idJuego, "Hora:", 12, 51, 43, Letra))

        DataPicker = New DateTimePicker
        DataPicker.Name = "dtpHoraProximo-" & oSorteo.idJuego
        DataPicker.Location = New System.Drawing.Point(56, 47)
        DataPicker.Size = New System.Drawing.Size(63, 20)
        DataPicker.Format = DateTimePickerFormat.Custom
        DataPicker.CustomFormat = "HH:mm"
        DataPicker.ShowUpDown = True
        DataPicker.Visible = True
        AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
        GrpProximo.Controls.Add(DataPicker)

        If oSorteo.pozos.Count <> 0 Then
            If Not (oSorteo.idJuego = 50 Or oSorteo.idJuego = 51) Then
                pozovisible = True
            End If
        End If

        lblPozoEstimado = New Label
        lblPozoEstimado = CrearEtiqueta("lblPozo-" & oSorteo.idJuego, "Pozo:", 142, 51, 42, Fuente, pozovisible)

        GrpProximo.Controls.Add(lblPozoEstimado)

        txtPozoEstimado = New TextBox
        txtPozoEstimado = CrearText("txtPozo-" & oSorteo.idJuego, "", 186, 47, 110, 1, Fuente, pozovisible, pozovisible, False)

        If oSorteo.pozos.Count = 0 Then
            txtPozoEstimado.Visible = False
            txtPozoEstimado.Visible = False
        End If
        AddHandler txtPozoEstimado.TextChanged, AddressOf texto_TextChanged

        GrpProximo.Controls.Add(txtPozoEstimado)

        PestaniaJ.Controls.Add(GrpProximo)
        ' ****  FIN Cabecera   ****

        ' ****  Seccion Izquierda  ****
        ' Panel Extracto. Primero agrego los controles al panel y despues el panel a la pestania
        GrpExtracto = New GroupBox
        GrpExtracto.Name = "grpExtracto-" & oSorteo.idJuego
        'GrpExtracto.Text = "Extracto Oficial"
        GrpExtracto.Font = Letra
        GrpExtracto.ForeColor = Color.FromArgb(90, 90, 90)
        GrpExtracto.Location = New System.Drawing.Point(2, 85)
        GrpExtracto.Size = New System.Drawing.Size(768, 430)

        ' Panel para visualizar mensaje cuando no se pudo obtener el extracto de ninguna manera
        panelmsj = New Panel
        panelmsj.Name = "panelmsj-" & oSorteo.idJuego
        panelmsj.Location = New System.Drawing.Point(3, 20)
        panelmsj.Width = GrpExtracto.Width - 10
        panelmsj.Height = GrpExtracto.Height - 25
        panelmsj.Visible = False

        ' Etiqueta para mostrar que no hay extracto, en el panel 
        Etiqueta = New Label
        Etiqueta.Name = "lblSinExtracto-" & oSorteo.idJuego
        Etiqueta.Text = "No se pudo obtener el extracto correspondiente a este juego."
        Etiqueta.Width = GrpExtracto.Width - 40
        Etiqueta.ForeColor = Color.Red
        Etiqueta.Font = Fuente

        panelmsj.Controls.Add(Etiqueta)
        GrpExtracto.Controls.Add(panelmsj)

        wb = New WebBrowser
        wb.Name = "wbExtracto-" & oSorteo.idJuego
        wb.Location = New System.Drawing.Point(3, 10)
        wb.Width = GrpExtracto.Width - 10
        wb.Height = GrpExtracto.Height - 15
        wb.Visible = False

        GrpExtracto.Controls.Add(wb)

        PestaniaJ.Controls.Add(GrpExtracto)
        ' ****  FIN Seccion Izquierda  ****

        ' ****  Seccion Derecha  ****
        ' Boton Actualizar Extracto
        Boton = New Button
        Boton.Name = "btnGuardarCambios-" & oSorteo.idJuego
        Boton.Text = "&ACTUALIZAR EXTRACTO "
        Boton.Location = New System.Drawing.Point(777, 53)
        Boton.Size = New System.Drawing.Size(190, 26)
        Boton.Font = LetraNegrita
        Boton.BackColor = System.Drawing.SystemColors.Control
        Boton.ForeColor = Color.FromArgb(52, 118, 166)
        Boton.FlatStyle = FlatStyle.Flat
        Boton.BackgroundImageLayout = ImageLayout.Stretch
        Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
        Boton.Enabled = True
        AddHandler Boton.Click, AddressOf btnGuardarCambios_Click
        AddHandler Boton.MouseDown, AddressOf botones_MouseDown
        AddHandler Boton.MouseHover, AddressOf botones_MouseHover
        AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
        AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave

        PestaniaJ.Controls.Add(Boton)

        ' Etiqueta para el mensaje que indica que el extracto esta generado localmente
        Etiqueta = New Label
        Etiqueta.Name = "lbllocal-" & oSorteo.idJuego
        If _publicarwebOFF = "S" Or _publicarwebON = "S" Then 'si ambos parametros son N, no se muetra leyenda de generado local
            Etiqueta.Text = "Debido a problemas en la publicación se ha generado el Extracto de manera local. " & vbCrLf & "Cuando se restablezca el servicio de Internet deberá re-publicar y confirmar."
        Else
            Etiqueta.Text = "Extracto Local."
        End If
        Etiqueta.Font = LetraNegrita
        Etiqueta.ForeColor = Color.Red
        Etiqueta.Location = New System.Drawing.Point(777, 130)
        Etiqueta.Size = New System.Drawing.Size(190, 160)
        Etiqueta.Visible = False

        PestaniaJ.Controls.Add(Etiqueta)

        ' Etiqueta para mensajes de datos faltantes (premios o jurisdicciones)
        Etiqueta = New Label
        Etiqueta.Name = "lblMensajes-" & oSorteo.idJuego
        Etiqueta.Text = ""
        Etiqueta.Font = LetraNegrita
        Etiqueta.ForeColor = Color.Red
        Etiqueta.Location = New System.Drawing.Point(777, 300)
        Etiqueta.Size = New System.Drawing.Size(190, 160)
        Etiqueta.Visible = False

        PestaniaJ.Controls.Add(Etiqueta)

        ' Botones Confirmar JURISDICCION y Anular JURISDICCION
        Boton = New Button
        Boton.Name = "btnConfirmarLoteria-" & oSorteo.idJuego
        If General.Jurisdiccion = "E" Then
            Boton.Text = "CONFIRMAR E. RIOS"
        Else
            Boton.Text = "CONFIRMAR SANTA FE"
        End If

        Boton.Location = New System.Drawing.Point(777, 460)
        Boton.Size = New System.Drawing.Size(190, 26)
        'Boton.Visible = IIf(oSorteo.ConfirmadoParcial Or (Not EsQuiniela(oSorteo.idJuego)), False, True)
        ' Modif RL 28/12/2021: boton confirmar parcial habilitado en todos los modos de operacion
        ''If General.Modo_Operacion.ToUpper() <> "PC-B" Then
        ''    Boton.Visible = True
        ''    Boton.Enabled = True
        ''Else
        ''    Boton.Visible = False
        ''    Boton.Enabled = False
        ''End If
        Boton.Visible = True
        Boton.Enabled = True
        ' FIN Modif RL 28/12/2021: boton confirmar parcial habilitado en todos los modos de operacion
        Boton.Font = LetraNegrita
        Boton.BackColor = System.Drawing.SystemColors.Control
        Boton.ForeColor = Color.FromArgb(52, 118, 166)
        Boton.FlatStyle = FlatStyle.Flat
        Boton.BackgroundImageLayout = ImageLayout.Stretch
        Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
        AddHandler Boton.Click, AddressOf btnConfirmarExtractoSantafe_Click
        AddHandler Boton.MouseDown, AddressOf botones_MouseDown
        AddHandler Boton.MouseHover, AddressOf botones_MouseHover
        AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
        AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave

        PestaniaJ.Controls.Add(Boton)

        Boton = New Button
        Boton.Name = "btnAnularLoteria-" & oSorteo.idJuego
        If General.Jurisdiccion = "E" Then
            Boton.Text = "ANULAR E. RIOS"
        Else
            Boton.Text = "ANULAR SANTA FE"
        End If

        Boton.Location = New System.Drawing.Point(777, 460)
        Boton.Size = New System.Drawing.Size(190, 26)
        Boton.Font = LetraNegrita
        Boton.BackColor = System.Drawing.SystemColors.Control
        Boton.ForeColor = Color.FromArgb(52, 118, 166)
        Boton.FlatStyle = FlatStyle.Flat
        Boton.BackgroundImageLayout = ImageLayout.Stretch
        Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
        'Boton.Visible = IIf(oSorteo.ConfirmadoParcial = True, True, False)
        If General.Modo_Operacion.ToUpper() <> "PC-B" Then
            Boton.Visible = True
            Boton.Enabled = True
        Else
            Boton.Visible = False
            Boton.Enabled = False
        End If
        AddHandler Boton.Click, AddressOf btnAnularExtractoSantafe_Click
        AddHandler Boton.MouseDown, AddressOf botones_MouseDown
        AddHandler Boton.MouseHover, AddressOf botones_MouseHover
        AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
        AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave

        PestaniaJ.Controls.Add(Boton)
        '*** fin confirmar/anular solo Jurisdiccion local

        Boton = New Button
        Boton.Name = "btnConfirmarExtracto-" & oSorteo.idJuego
        Boton.Text = "&CONFIRMAR EXTRACTO"
        Boton.Font = LetraNegrita
        Boton.BackColor = System.Drawing.SystemColors.Control
        Boton.ForeColor = Color.FromArgb(52, 118, 166)
        Boton.FlatStyle = FlatStyle.Flat
        Boton.Location = New System.Drawing.Point(777, 493)
        Boton.Size = New System.Drawing.Size(190, 26)
        Boton.BackgroundImageLayout = ImageLayout.Stretch
        Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
        Boton.Enabled = True
        AddHandler Boton.Click, AddressOf btnConfirmarExtracto_Click
        AddHandler Boton.MouseDown, AddressOf botones_MouseDown
        AddHandler Boton.MouseHover, AddressOf botones_MouseHover
        AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
        AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave

        PestaniaJ.Controls.Add(Boton)

        
        ' ****  FIN Seccion Derecha  ****

        Return PestaniaJ

    End Function

    Private Sub setControles()
        Dim boJuego As New JuegoBO
        Dim idJuego As Integer

        Dim Tab As TabControl
        Dim Pestaña As TabPage
        Dim GrpActual As GroupBox
        Dim GrpProximo As GroupBox
        Dim GrpExtracto As GroupBox
        Dim Etiqueta As Label
        Dim Text As TextBox
        Dim DataPicker As DateTimePicker
        Dim wb As WebBrowser
        Dim Boton As Button
        Dim panelmsj As Panel
        Dim Letra As Font
        Dim LetraNegrita As Font
        'Dim Letra8 As Font
        'Dim Letra8N As Font
        Dim Fuente As Font
        Dim _indice As Integer
        Dim _left As Integer
        Dim _AlturaGrilla As Integer
        Dim _Esloteria As Boolean = False
        Dim _extractolocal As Boolean = False
        Dim sorteoBO As New PgmSorteoBO
        Dim msjvarios As String = ""
        Dim _sinPremios As Boolean = False
        Dim _publicarwebOFF As String = General.PublicarWebOFF
        Dim _publicarwebON As String = General.PublicarWebON

        Me.Cursor = Cursors.WaitCursor
        MDIContenedor.Cursor = Cursors.WaitCursor
        ObtenerIndiceResolucion(_indice, _left, _AlturaGrilla)
        Fuente = New Font("Myriad Web Pro", 11, FontStyle.Regular)
        Letra = New Font("Myriad Web Pro", 10, FontStyle.Regular)
        LetraNegrita = New Font("Myriad Web Pro", 11, FontStyle.Bold)
        'Letra8 = New Font("Microsoft Sans Serif", 9, FontStyle.Bold)
        'Letra8n = New Font("Microsoft Sans Serif", 9, FontStyle.Regular)

        TabJuegos.Controls.Clear()
        ' crea tabJuegos y lo inserta en el formulario
        Tab = Me.grpJuegos.Controls("tabJuegos")
        If Not IsNothing(Tab) Then
            For Each TabControl In Tab.TabPages
                Tab.SelectTab(TabControl)
                idJuego = getIdJuegoActual()
            Next
            Me.grpJuegos.Controls.Remove(Tab)
        End If

        Me.grpJuegos.Controls.Add(Tab)
        Dim pestaniaActiva As Integer = 0
        Dim _DeshabilitaBotonConfirmarExtracto As Boolean = False
        Dim BoSorteo As New PgmSorteoBO
        Dim oJuegoBO As New JuegoBO
        btnAnteriores.Font = LetraNegrita
        ' agrega las pestañas al tabJuegos
        FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " INICIO setControles")
        For Each oSorteo In oPC.PgmSorteos
            idJuego = oSorteo.idJuego
            _Esloteria = ojuegobo.EsQuiniela(idJuego)
            ' pestaña
            Pestaña = New TabPage
            Pestaña.BackColor = Color.Transparent
            Pestaña.Name = "pstJuego-" & idJuego
            Pestaña.Text = "" & boJuego.getJuegoDescripcion(oSorteo.idJuego)
            Pestaña.Tag = oSorteo
            Pestaña.Font = Fuente
            ' Pestaña.Padding = New System.Windows.Forms.Padding(1, 1, 1, 1)
            'Pestaña.Height = Me.TabJuegos.Height - 3
            'Pestaña.Margin = New System.Windows.Forms.Padding(0, 0, 0, 0)
            Pestaña.BackColor = Color.FromArgb(239, 239, 239)

            TabJuegos.Controls.Add(Pestaña)

            TabJuegos.SelectedIndex = pestaniaActiva
            pestaniaActiva = pestaniaActiva + 1

            GrpActual = New GroupBox
            GrpActual.Name = "grpActual"
            GrpActual.Text = "Sorteo Actual"
            GrpActual.Location = New System.Drawing.Point(2, 2)
            GrpActual.Size = New System.Drawing.Size(460, 78)
            GrpActual.Font = Letra
            GrpActual.ForeColor = Color.FromArgb(90, 90, 90)
            Pestaña.Controls.Add(GrpActual)

            GrpActual.Controls.Add(CrearEtiqueta("lblHoraInicioReal" & idJuego, "Hora Inicio Real:", 4, 20, 104, Letra))

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpHoraInicioJuego" & idJuego
            DataPicker.Location = New System.Drawing.Point(110, 17)
            DataPicker.Size = New System.Drawing.Size(55, 24)
            DataPicker.Format = DateTimePickerFormat.Custom
            DataPicker.CustomFormat = "HH:mm"
            DataPicker.ShowUpDown = True
            AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
            GrpActual.Controls.Add(DataPicker)

            GrpActual.Controls.Add(CrearEtiqueta("lblHoraFinReal" & idJuego, "Hora Fin Real:", 306, 20, 89, Letra))
            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpHoraFinJuego" & idJuego
            DataPicker.Location = New System.Drawing.Point(395, 17)
            DataPicker.Size = New System.Drawing.Size(55, 24)
            DataPicker.Format = DateTimePickerFormat.Custom
            DataPicker.CustomFormat = "HH:mm"
            DataPicker.ShowUpDown = True
            AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
            GrpActual.Controls.Add(DataPicker)

            GrpActual.Controls.Add(CrearEtiqueta("lblfechaPrescripcion" & idJuego, "F.Prescripción:", 11, 51, 97, Letra))

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpFechaPrescripcion" & idJuego
            DataPicker.Location = New System.Drawing.Point(110, 47)
            DataPicker.Size = New System.Drawing.Size(233, 20)
            DataPicker.Format = DateTimePickerFormat.Long
            DataPicker.CustomFormat = "dd/MM/yyyy"
            DataPicker.ShowUpDown = True
            AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
            GrpActual.Controls.Add(DataPicker)

            '' GrpActual.Controls.Add(CrearEtiqueta("lblhoraPrescripcion" & idJuego, "Hora:", 354, 51, 40, Letra))

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpHoraPrescripcion" & idJuego
            DataPicker.Location = New System.Drawing.Point(395, 47)
            DataPicker.Size = New System.Drawing.Size(55, 20)
            DataPicker.Format = DateTimePickerFormat.Custom
            DataPicker.CustomFormat = "HH:mm"
            DataPicker.ShowUpDown = True
            AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
            GrpActual.Controls.Add(DataPicker)

            '-- grilla proximo
            GrpProximo = New GroupBox
            GrpProximo.Name = "grpProximo"
            GrpProximo.Text = "Próximo Sorteo"
            GrpProximo.Location = New System.Drawing.Point(470, 2)
            GrpProximo.Size = New System.Drawing.Size(300, 78)
            GrpProximo.Anchor = AnchorStyles.Left Or AnchorStyles.Top
            GrpProximo.Font = Letra
            GrpProximo.ForeColor = Color.FromArgb(90, 90, 90)
            Pestaña.Controls.Add(GrpProximo)

            GrpProximo.Controls.Add(CrearEtiqueta("lblfechaProximo" & idJuego, "Fecha:", 6, 20, 47, Letra))

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpFechaProximo" & idJuego
            DataPicker.Location = New System.Drawing.Point(56, 17)
            DataPicker.Size = New System.Drawing.Size(233, 20)
            DataPicker.Format = DateTimePickerFormat.Long
            DataPicker.CustomFormat = "dd/MM/yyyy"
            DataPicker.ShowUpDown = True
            AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
            GrpProximo.Controls.Add(DataPicker)

            GrpProximo.Controls.Add(CrearEtiqueta("lblHoraProximo" & idJuego, "Hora:", 12, 51, 43, Letra))

            DataPicker = New DateTimePicker
            DataPicker.Name = "dtpHoraProximo" & idJuego
            DataPicker.Location = New System.Drawing.Point(56, 47)
            DataPicker.Size = New System.Drawing.Size(63, 20)
            DataPicker.Format = DateTimePickerFormat.Custom
            DataPicker.CustomFormat = "HH:mm"
            DataPicker.ShowUpDown = True
            AddHandler DataPicker.ValueChanged, AddressOf dtpicker_ValueChanged
            GrpProximo.Controls.Add(DataPicker)
            Dim pozovisible As Boolean
            If oSorteo.pozos.Count = 0 Then
                pozovisible = False
            Else
                If oSorteo.idJuego = 50 Or oSorteo.idJuego = 51 Then
                    pozovisible = False
                Else
                    pozovisible = True
                End If
            End If

            GrpProximo.Controls.Add(CrearEtiqueta("lblPozo" & idJuego, "Pozo:", 142, 51, 42, Fuente, pozovisible))
            GrpProximo.Controls.Add(CrearText("txtPozo" & idJuego, "", 186, 47, 110, 1, Fuente, pozovisible, pozovisible, False))
            If oSorteo.pozos.Count = 0 Then
                getControlJ("grpProximo", "lblPozo").Visible = False
                getControlJ("grpProximo", "txtPozo").Visible = False

            End If
            AddHandler getControlJ("grpProximo", "txtPozo").TextChanged, AddressOf texto_TextChanged

            Boton = New Button
            Boton.Name = "btnGuardarCambios" & idJuego
            Boton.Text = "&ACTUALIZAR EXTRACTO "
            Boton.Location = New System.Drawing.Point(777, 53)
            Boton.Size = New System.Drawing.Size(190, 26)
            Boton.Font = LetraNegrita
            Boton.BackColor = System.Drawing.SystemColors.Control
            Boton.ForeColor = Color.FromArgb(52, 118, 166)
            Boton.FlatStyle = FlatStyle.Flat
            Boton.BackgroundImageLayout = ImageLayout.Stretch
            Boton.BackgroundImage = My.Resources.Imagenes.boton_normal

            AddHandler Boton.Click, AddressOf btnGuardarCambios_Click
            AddHandler Boton.MouseDown, AddressOf botones_MouseDown
            AddHandler Boton.MouseHover, AddressOf botones_MouseHover
            AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
            AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave

            If oSorteo.idEstadoPgmConcurso = 50 Then
                Boton.Enabled = False
            Else
                Boton.Enabled = True
            End If
            Pestaña.Controls.Add(Boton)




            GrpExtracto = New GroupBox
            GrpExtracto.Name = "grpExtracto" & idJuego
            'GrpExtracto.Text = "Extracto Oficial"
            GrpExtracto.Font = Letra
            GrpExtracto.ForeColor = Color.FromArgb(90, 90, 90)
            GrpExtracto.Location = New System.Drawing.Point(2, 85)
            If _Esloteria Then
                GrpExtracto.Size = New System.Drawing.Size(768, 430)
            Else
                GrpExtracto.Size = New System.Drawing.Size(768, 430)
            End If
            Pestaña.Controls.Add(GrpExtracto)


            wb = New WebBrowser
            wb.Name = "wbExtracto" & idJuego
            wb.Location = New System.Drawing.Point(3, 20)
            wb.Width = GrpExtracto.Width - 10
            wb.Height = GrpExtracto.Height - 25
            'panel para visualizar mensaje
            panelmsj = New Panel
            panelmsj.Name = "panelmsj" & idJuego
            panelmsj.Location = New System.Drawing.Point(3, 20)
            panelmsj.Width = GrpExtracto.Width - 10
            If _Esloteria Then
                panelmsj.Height = GrpExtracto.Height - 25
            Else
                panelmsj.Height = GrpExtracto.Height - 25
            End If
            panelmsj.Visible = False
            Etiqueta = New Label
            Etiqueta.Text = "No se encontró el archivo de extracto correspondiente a este juego."
            Etiqueta.Width = GrpExtracto.Width - 40
            Etiqueta.ForeColor = Color.Red
            Etiqueta.Font = Fuente
            panelmsj.Controls.Add(Etiqueta)
            GrpExtracto.Controls.Add(panelmsj)

            '*** verifica si tiene que generar el extracto o no 
            wb.Visible = False
            panelmsj.Visible = True
            'If pestaniaActiva > 0 And pestaniaActiva = 1 Then
            '    If Not TieneExtractoCargado(oSorteo.idJuego) Then

            Dim sBO As New PgmSorteoBO
            Dim url As String = ""
            Try
                If pestaniaActiva > 0 And pestaniaActiva = 1 Then
                    FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " setControles:Va a generar el extracto remoto para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    If Not TieneExtractoCargado(oSorteo.idJuego) Then
                        If General.Modo_Operacion <> "PC-B" Then
                            If General.PublicaExtractosWSRestOFF = "S" Then
                                url = sBO.getUrlExtractoOficialRest(oSorteo)
                                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto REST: ->" & url & "<- para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                            Else
                                url = sBO.getUrlExtractoOficial(oSorteo)
                                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto NET: ->" & url & "<- para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                            End If

                            wb.Url = New System.Uri(url)
                        Else
                            url = ""
                        End If

                        MarcaExtractoCargado(oSorteo.idJuego)
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto OK para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    End If
                End If

                Me.Cursor = Cursors.WaitCursor
                wb.Refresh()
                wb.Visible = True
                panelmsj.Visible = False
                Me.Cursor = Cursors.Default
                'MarcaExtractoCargado(oSorteo.idJuego)
            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Error extracto remoto:" & ex.Message & " para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                Try
                    'si el error es porque no tiene premios cargados,no se genera el extracto para tener coherencia con el WS
                    If Juegoconpremios(oSorteo) And vNotienePremios = True Then
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:No tiene premios cargados:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                        wb.Visible = False
                        panelmsj.Visible = True
                    Else
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Va a generar el extracto LOCAL para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                        '** extracto local
                        Dim ds As New ExtractoReporte
                        ds.GenerarExtractoLocal(oSorteo.idPgmSorteo, url, General.PathInformes)
                        Me.Cursor = Cursors.WaitCursor
                        wb.Url = New System.Uri(url)
                        wb.Refresh()
                        wb.Visible = True
                        Me.Cursor = Cursors.Default
                        panelmsj.Visible = False
                        '*** muestra msj de fallo de conexion

                        Etiqueta = New Label
                        Etiqueta.Name = "lbllocal" & idJuego
                        If _publicarwebOFF = "S" Or _publicarwebON = "S" Then 'si ambos parametros son N, no se muetra leyenda de generado local  y se habilita el boton confirmar
                            Etiqueta.Text = "Debido a problemas en la publicación se ha generado el Extracto de manera local" & vbCrLf & "Cuando se restablezca el servicio de Internet deberá re-publicar y confirmar."
                        Else
                            Etiqueta.Text = "Extracto Local."
                        End If

                        Etiqueta.Font = LetraNegrita
                        Etiqueta.ForeColor = Color.Red
                        Etiqueta.Location = New System.Drawing.Point(777, 130)
                        Etiqueta.Size = New System.Drawing.Size(190, 160)
                        Etiqueta.Visible = True
                        Pestaña.Controls.Add(Etiqueta)

                        _extractolocal = True
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto LOCAL OK para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    End If
                Catch ex1 As Exception
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Problema al generar el extracto LOCAL:" & ex.Message & " para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    wb.Visible = False
                    panelmsj.Visible = True
                End Try

            End Try
            ' End If 'tiene extracto cargado
            'End If 'es pestania activa 1
            GrpExtracto.Controls.Add(wb)
            '*** confirmar/anular solo SF
            If _Esloteria Then

                Boton = New Button
                Boton.Name = "btnConfirmarLoteria" & idJuego
                If General.Jurisdiccion = "E" Then
                    Boton.Text = "CONFIRMAR E. RIOS"
                Else
                    Boton.Text = "CONFIRMAR SANTA FE"
                End If

                Boton.Location = New System.Drawing.Point(777, 460)
                Boton.Size = New System.Drawing.Size(190, 26)
                Boton.Visible = IIf(oSorteo.ConfirmadoParcial = True, False, True)
                Boton.Font = LetraNegrita
                Boton.BackColor = System.Drawing.SystemColors.Control
                Boton.ForeColor = Color.FromArgb(52, 118, 166)
                Boton.FlatStyle = FlatStyle.Flat
                Boton.BackgroundImageLayout = ImageLayout.Stretch
                Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
                AddHandler Boton.Click, AddressOf btnConfirmarExtractoSantafe_Click
                AddHandler Boton.MouseDown, AddressOf botones_MouseDown
                AddHandler Boton.MouseHover, AddressOf botones_MouseHover
                AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
                AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave

                If oSorteo.idEstadoPgmConcurso = 50 Or _extractolocal Then
                    Boton.Enabled = False
                Else
                    Boton.Enabled = True
                End If
                If General.Modo_Operacion.ToUpper() = "PC-B" Then
                    Boton.Visible = False
                    Boton.Enabled = False
                End If

                Pestaña.Controls.Add(Boton)

                Boton = New Button
                Boton.Name = "btnAnularLoteria" & idJuego
                If General.Jurisdiccion = "E" Then
                    Boton.Text = "ANULAR E. RIOS"
                Else
                    Boton.Text = "ANULAR SANTA FE"
                End If

                Boton.Location = New System.Drawing.Point(777, 460)
                Boton.Size = New System.Drawing.Size(190, 26)
                Boton.Font = LetraNegrita
                Boton.BackColor = System.Drawing.SystemColors.Control
                Boton.ForeColor = Color.FromArgb(52, 118, 166)
                Boton.FlatStyle = FlatStyle.Flat
                Boton.BackgroundImageLayout = ImageLayout.Stretch
                Boton.BackgroundImage = My.Resources.Imagenes.boton_normal
                Boton.Visible = IIf(oSorteo.ConfirmadoParcial = True, True, False)
                AddHandler Boton.Click, AddressOf btnAnularExtractoSantafe_Click
                AddHandler Boton.MouseDown, AddressOf botones_MouseDown
                AddHandler Boton.MouseHover, AddressOf botones_MouseHover
                AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
                AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave

                If oSorteo.idEstadoPgmConcurso = 50 Then
                    Boton.Enabled = False
                Else
                    If _extractolocal Then
                        If _publicarwebOFF = "N" And _publicarwebON = "N" Then
                            Boton.Enabled = True
                        Else
                            Boton.Enabled = False
                        End If
                    Else
                        Boton.Enabled = True
                    End If

                End If
                If General.Modo_Operacion.ToUpper() = "PC-B" Then
                    Boton.Visible = False
                    Boton.Enabled = False
                End If

                Pestaña.Controls.Add(Boton)
            End If

            '*** fin confirmar/anular solo Santa Fe
            '*** controla el extracto de quini 6 y brinco si corresponde
            Dim controlQuiniBrinco As Boolean = False
            If ControlarExtractoQuiniBrinco(oSorteo) Then
                controlQuiniBrinco = True
            End If

            Boton = New Button
            Boton.Name = "btnConfirmarExtracto" & idJuego
            Boton.Text = "&CONFIRMAR EXTRACTO"
            Boton.Font = LetraNegrita
            Boton.BackColor = System.Drawing.SystemColors.Control
            Boton.ForeColor = Color.FromArgb(52, 118, 166)
            Boton.FlatStyle = FlatStyle.Flat
            Boton.Location = New System.Drawing.Point(777, 493)
            Boton.Size = New System.Drawing.Size(190, 26)
            Boton.BackgroundImageLayout = ImageLayout.Stretch
            Boton.BackgroundImage = My.Resources.Imagenes.boton_normal

            AddHandler Boton.Click, AddressOf btnConfirmarExtracto_Click
            AddHandler Boton.MouseDown, AddressOf botones_MouseDown
            AddHandler Boton.MouseHover, AddressOf botones_MouseHover
            AddHandler Boton.EnabledChanged, AddressOf botones_EnabledChanged
            AddHandler Boton.MouseLeave, AddressOf botones_MouseLeave
            msjvarios = ""

            If _extractolocal Then
                'si el extracto se genero local,no se habilita el boton confirmar
                Boton.Enabled = False
                '*** para mostrar otros msj
                If oJuegoBO.esQuiniela(oSorteo.idJuego) Then
                    _DeshabilitaBotonConfirmarExtracto = BoSorteo.OtrasJurisdicciones_SinConfirmar(oSorteo.idPgmSorteo)
                    If _DeshabilitaBotonConfirmarExtracto Then
                        msjvarios = msjvarios & "Faltan cargar jurisdicciones." & vbCrLf
                    End If
                End If
                If Juegoconpremios(oSorteo) Then
                    If sorteoBO.NoTienePremiosCargados(oSorteo.idPgmSorteo, oSorteo.idJuego) Then
                        msjvarios = msjvarios & "Faltan cargar premios." & vbCrLf
                    End If
                End If
                If controlQuiniBrinco = False Then
                    msjvarios = msjvarios & "Faltan control de extracto." & vbCrLf
                End If
                'si esta todo bien cargado,no existen msj de error y los parametros _publicarwebOFF=N y _publicarwebON=N, se habilita el boton confirmar
                If msjvarios.Trim = "" Then
                    If _publicarwebOFF = "N" And _publicarwebON = "N" Then
                        Boton.Enabled = True
                    End If
                End If

            Else 'sigue como siempre
                '**05/11/2012 si no tiene otras jurisdicciones cargadas no se habilta el boton confirmar cuando el juego es quiniela
                If oSorteo.idEstadoPgmConcurso = 50 Then
                    Boton.Enabled = False
                Else
                    Boton.Enabled = True
                End If
                '**27/11/2012 para habilitar el boton confirmar tiene que tener jurisdicciones cargadas y confirmadas.
                If oJuegoBO.esQuiniela(oSorteo.idJuego) Then
                    _DeshabilitaBotonConfirmarExtracto = BoSorteo.OtrasJurisdicciones_SinConfirmar(oSorteo.idPgmSorteo)
                    If _DeshabilitaBotonConfirmarExtracto Then
                        msjvarios = msjvarios & "Faltan cargar jurisdicciones." & vbCrLf
                    End If
                    If oSorteo.idEstadoPgmConcurso = 50 Or (_DeshabilitaBotonConfirmarExtracto) Then
                        Boton.Enabled = False
                    Else
                        Boton.Enabled = True
                    End If
                End If
                '** 18/12/2012 si no tiene premios cargados tb se deshabilita el boton confirmar
                '** para los juegos que tienen premios
                If Juegoconpremios(oSorteo) Then
                    If sorteoBO.NoTienePremiosCargados(oSorteo.idPgmSorteo, oSorteo.idJuego) Then
                        _sinPremios = True
                        msjvarios = msjvarios & "Faltan cargar premios." & vbCrLf
                    End If
                    If controlQuiniBrinco = False Then
                        msjvarios = msjvarios & "Faltan control de extracto." & vbCrLf
                    End If
                    If oSorteo.idEstadoPgmConcurso = 50 Or _sinPremios Or controlQuiniBrinco = False Then
                        Boton.Enabled = False
                    Else
                        Boton.Enabled = True
                    End If
                End If
            End If

            Pestaña.Controls.Add(Boton)

            '*** muestra msj si faltan algo por cargar
            Etiqueta = New Label
            Etiqueta.Name = "lblMensajes" & idJuego
            Etiqueta.Text = msjvarios
            Etiqueta.Font = LetraNegrita
            Etiqueta.ForeColor = Color.Red
            Etiqueta.Location = New System.Drawing.Point(777, 300)
            Etiqueta.Size = New System.Drawing.Size(190, 160)
            Etiqueta.Visible = True
            Pestaña.Controls.Add(Etiqueta)

        Next
        TabJuegos.SelectedIndex = 1
        FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " FIN setControles")
        Me.Cursor = Cursors.Default
        MDIContenedor.Cursor = Cursors.Default
    End Sub

    Private Sub ActualizarCabeceraPestaniaJuego(ByRef tab As TabControl)
        Dim DataPicker As DateTimePicker
        Dim habilitarControl As Boolean
        Dim text As TextBox

        Dim oSorteo As PgmSorteo = tab.SelectedTab.Tag

        habilitarControl = True

        DataPicker = getControlJ("grpActual-", "dtpHoraInicioJuego-")
        DataPicker.Value = oSorteo.fechaHoraIniReal
        DataPicker.Enabled = habilitarControl

        DataPicker = getControlJ("grpActual-", "dtpHoraFinJuego-")
        DataPicker.Value = oSorteo.fechaHoraFinReal
        DataPicker.Enabled = habilitarControl

        DataPicker = getControlJ("grpActual-", "dtpFechaPrescripcion-")
        DataPicker.Value = oSorteo.fechaHoraPrescripcion
        DataPicker.Enabled = habilitarControl

        DataPicker = getControlJ("grpActual-", "dtpHoraPrescripcion-")
        DataPicker.Value = oSorteo.fechaHoraPrescripcion
        DataPicker.Enabled = habilitarControl
        DataPicker.Visible = False ' RL: 21-08-2012 - Incid 2737: no mostrar hora prescripcion

        DataPicker = getControlJ("grpProximo-", "dtpFechaProximo-")
        DataPicker.Value = oSorteo.fechaHoraProximo
        DataPicker.Enabled = habilitarControl

        DataPicker = getControlJ("grpProximo-", "dtpHoraProximo-")
        DataPicker.Value = oSorteo.fechaHoraProximo
        DataPicker.Enabled = habilitarControl
        text = getControlJ("grpProximo-", "txtPozo-")
        ' 30/07/2013. RL. el pozo estimado ya se actualizo al finalizar extracciones, leer archivo de premios o por el abm de pozo sugerido
        text.Text = oSorteo.PozoEstimado
        text.MaxLength = 13

    End Sub

    ' presenta en el formulario los valores de la entidad oPC
    Private Sub setValoresCabeceras()
        Dim tab As TabControl

        tab = Me.Controls("grpJuegos").Controls("tabJuegos")

        ' valores de las pestañas tabJuegos
        For Each oSorteo In oPC.PgmSorteos

            ' localiza y hace activa la pestaña
            tab.SelectTab("pstJuego-" & oSorteo.idJuego)

            ActualizarCabeceraPestaniaJuego(tab)

        Next
        tab.SelectTab(0)

    End Sub

    Private Function getIdJuegoActual() As Int16
        Dim tab As TabControl
        Dim mNombre As Array

        ' localiza el tab de juego seleccionado
        tab = Me.Controls("grpJuegos").Controls("tabJuegos")
        mNombre = Split(tab.SelectedTab.Name, "-")

        Return mNombre(1)
    End Function

    Private Function getControlJ(ByVal grupo As String, ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Int16

        tab = Me.grpJuegos.Controls("tabJuegos")
        mAux = Split(tab.SelectedTab.Name, "-")
        idJuego = mAux(1)

        nombreCtrl &= idJuego
        If InStr(grupo, "-") > 0 Then
            grupo &= idJuego
        End If
        Return tab.SelectedTab.Controls(grupo).Controls(nombreCtrl)
    End Function
    Private Function CrearEtiqueta(ByVal pNombre As String, ByVal pTexto As String, ByVal pLef As Integer, ByVal pTop As Integer, ByVal pAncho As Integer, Optional ByVal Fuente As Font = Nothing, Optional ByVal pVisible As Boolean = True) As Label
        Dim Etiqueta As New Label
        Try
            Etiqueta.Name = pNombre
            Etiqueta.Text = pTexto
            Etiqueta.Top = pTop
            Etiqueta.Left = pLef
            Etiqueta.Width = pAncho
            If Not Fuente Is Nothing Then
                Etiqueta.Font = Fuente
            End If
            Etiqueta.Visible = pVisible
            Return Etiqueta
        Catch ex As Exception
            Return Nothing
            MsgBox("CrearEtiqueta:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Function CrearText(ByVal pNombre As String, ByVal pTexto As String, ByVal pLef As Integer, ByVal pTop As Integer, ByVal pAncho As Integer, ByVal pAlineacion As Integer, Optional ByVal Fuente As Font = Nothing, Optional ByVal pVisible As Boolean = True, Optional ByVal pEnabled As Boolean = False, Optional ByVal pSoloLectura As Boolean = True, Optional ByVal ancla As Integer = -1) As TextBox
        Dim texto As New TextBox
        Try
            texto.Name = pNombre
            texto.Text = pTexto
            texto.Top = pTop
            texto.Left = pLef
            texto.Width = pAncho
            If Not Fuente Is Nothing Then
                texto.Font = Fuente
            End If
            texto.Visible = pVisible
            Select Case pAlineacion
                Case 1 'derecha
                    texto.TextAlign = HorizontalAlignment.Right
                Case 2 'izquierda
                    texto.TextAlign = HorizontalAlignment.Left
                Case 3 'centro
                    texto.TextAlign = HorizontalAlignment.Center
            End Select
            texto.Enabled = pEnabled
            texto.ReadOnly = pSoloLectura
            If pSoloLectura Then
                texto.BackColor = Color.White
            End If
            Select Case ancla
                Case 1
                    texto.Anchor = AnchorStyles.Left Or AnchorStyles.Top
                Case 2
                    'texto.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom Or AnchorStyles.Top
            End Select

            Return texto
        Catch ex As Exception
            MsgBox("CrearText:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
            Return Nothing
        End Try

    End Function

    Private Sub btnGuardarCambios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim oSorteo As New PgmSorteo
        Dim sBO As New PgmSorteoBO

        Dim DataPickerF As DateTimePicker
        Dim DataPickerH As DateTimePicker
        Dim Text As TextBox

        Dim fechaHoraCaducidadSorteo As DateTime
        Dim fechaHoraProximoSorteo As DateTime
        Dim fechaHoraInicio As DateTime
        Dim fechaHoraSorteo As DateTime
        Dim fechaHoraFin As DateTime
        Dim pozoProximoEstimado As Double
        Dim webB As WebBrowser
        Dim NombreWebB As String
        Dim tab As TabControl
        Dim nombreGrp As String
        Dim nombrepestania As String
        Dim pozo As String
        Dim separadordec As String
        Dim LetraNegrita As Font
        Dim sinPremios As Boolean = False
        Dim msjvarios As String = ""
        Dim etiqueta As Label
        Dim Boton As Button
        Dim btn As String = ""
        Dim _publicarweboff As String = General.PublicarWebOFF
        Dim _publicarwebon As String = General.PublicarWebON
        Dim _PublicaExtractosWSRestON As String = General.PublicaExtractosWSRestON
        Dim _PublicaExtractosWSRestOFF As String = General.PublicaExtractosWSRestOFF

        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()

        'se  obtiene el separaddor decimal d ela configuracion regional para poder formatear correctamente el pozo
        separadordec = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

        tab = Me.Controls("grpJuegos").Controls("tabJuegos")
        oSorteo = tab.SelectedTab.Tag
        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar: INICIO btnGuardarCambios_click:sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)

        DataPickerF = getControlJ("grpActual-", "dtpFechaPrescripcion-")
        DataPickerH = getControlJ("grpActual-", "dtpHoraPrescripcion-")
        'If DataPickerH Is Nothing Then DataPickerH = New DateTime(DataPickerF.Value.Ticks)
        fechaHoraCaducidadSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerF = getControlJ("grpProximo-", "dtpFechaProximo-")
        DataPickerH = getControlJ("grpProximo-", "dtpHoraProximo-")
        fechaHoraProximoSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerH = getControlJ("grpActual-", "dtpHoraInicioJuego-")
        fechaHoraInicio = FormatDateTime(oSorteo.fechaHoraIniReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
        fechaHoraSorteo = FormatDateTime(oSorteo.fechaHora, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerH = getControlJ("grpActual-", "dtpHoraFinJuego-")
        fechaHoraFin = FormatDateTime(oSorteo.fechaHoraFinReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        Text = getControlJ("grpProximo-", "txtPozo-")
        If Text.Text = "" Then
            If Text.Enabled Then 'si esta habilitado lo debe ingresar
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar: validacion:Ingrese un monto para el pozo. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                Me.Cursor = Cursors.Default
                Me.Refresh()
                MsgBox("Ingrese un monto para el pozo.", MsgBoxStyle.Information, MDIContenedor.Text)
                'vienedepestania = False
                Exit Sub
            Else
                ' 30/07/2013. RL. el pozo estimado ya se actualizo
                Text.Text = oSorteo.PozoEstimado
            End If
        Else
            If Text.Enabled Then
                If Len(Text.Text) > 13 Then
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:Validacion:Longitud de Pozo demasiado larga. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Me.Cursor = Cursors.Default
                    Me.Refresh()
                    MsgBox("Longitud de Pozo demasiado larga.", MsgBoxStyle.Information, MDIContenedor.Text)
                    'vienedepestania = False
                    Exit Sub
                End If
                If separadordec = "." Then
                    pozo = Replace(Text.Text, ",", ".")
                Else
                    pozo = Replace(Text.Text, ".", ",")
                End If

                If Not IsNumeric(pozo) Then
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:validacion:El pozo debe ser un valor numÃ©rico. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Me.Cursor = Cursors.Default
                    Me.Refresh()
                    MsgBox("El pozo debe ser un valor numÃ©rico", MsgBoxStyle.Information, MDIContenedor.Text)
                    'vienedepestania = False
                    Exit Sub
                End If
                If pozo < 0 Then
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:validacion:El pozo debe ser un valor positivo. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Me.Cursor = Cursors.Default
                    Me.Refresh()
                    MsgBox("El pozo debe ser un valor positivo", MsgBoxStyle.Information, MDIContenedor.Text)
                    'vienedepestania = False
                    Exit Sub
                End If
                If pozo = 0 Then
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:validacion: El pozo debe ser un valor mayor a cero. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Me.Cursor = Cursors.Default
                    Me.Refresh()
                    MsgBox("El pozo debe ser un valor mayor a cero", MsgBoxStyle.Information, MDIContenedor.Text)
                    'vienedepestania = False
                    Exit Sub
                End If
                pozoProximoEstimado = pozo
            Else
                pozoProximoEstimado = Text.Text
            End If
        End If

        ' Comienza la actualizacion

        oSorteo.fechaHoraPrescripcion = fechaHoraCaducidadSorteo
        oSorteo.fechaHoraProximo = fechaHoraProximoSorteo

        oSorteo.fechaHoraIniReal = fechaHoraInicio
        oSorteo.fechaHoraFinReal = fechaHoraFin

        ' Actualiza datos en bd sorteador
        If sBO.ActualizarDatosSorteo(oSorteo, pozoProximoEstimado) = False Then
            Me.Cursor = Cursors.Default
            Me.Refresh()
            MsgBox("Error al actualizar sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:Problemas al ejecutars BO.ActualizarDatosSorteo ,Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
            'vienedepestania = False
            Exit Sub
        End If

        ' leo la lista de sorteos actualizados del pgmConcurso
        Dim lstSorteos As ListaOrdenada(Of PgmSorteo)
        lstSorteos = sBO.getPgmSorteos(oSorteo.idPgmConcurso)

        ' Actualiza datos en bd web
        'AGREGADO FSCOTTA
        If _PublicaExtractosWSRestON = "S" Or _PublicaExtractosWSRestOFF = "S" Then
            Try
                For Each os As PgmSorteo In lstSorteos
                    sBO.publicarWEBRest(os)
                    'sBO.ActualizarSorteoWeb(os, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, pozoProximoEstimado)
                Next
            Catch ex As Exception
                '** no se muestra ningun msj cuando es por fallo de internet
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar: publicarWEBRest btnGuardarCambios_click:Problemas alActualizarSorteoWeb:" & ex.Message & " sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                ' MsgBox("btnGuardarCambios_Click:" & ex.Message, MsgBoxStyle.Information)
            End Try
        End If
        '-----------------------------------

        Try
            If _publicarweboff = "S" Or _publicarwebon = "S" Then
                For Each os As PgmSorteo In lstSorteos
                    fechaHoraCaducidadSorteo = os.fechaHoraPrescripcion
                    fechaHoraProximoSorteo = os.fechaHoraProximo
                    fechaHoraSorteo = os.fechaHoraIniReal
                    fechaHoraInicio = os.fechaHoraIniReal
                    fechaHoraFin = os.fechaHoraFinReal
                    pozoProximoEstimado = os.PozoEstimado

                    sBO.ActualizarSorteoWeb(os, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, pozoProximoEstimado)
                Next
            Else
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & "ConcursoFinalizar: Actualizar Extracto: La publicación a la Web no está habilitada. PublicarWebON: " & _publicarwebon & ", PublicarWebOFF: " & _publicarweboff & ".")
            End If
        Catch ex As Exception
            '** no se muestra ningun msj cuando es por fallo de internet
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar: btnGuardarCambios_click:Problemas alActualizarSorteoWeb:" & ex.Message & " sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
            ' MsgBox("btnGuardarCambios_Click:" & ex.Message, MsgBoxStyle.Information)
        End Try

        ' Obtengo datos actualizados del sorteo y lo asigno a la pesatania y concurso
        oSorteo = sBO.getPgmSorteo(oSorteo.idPgmSorteo)

        ' Si modifiqué un dato del juego Rector marco como "sucios" todos los juegos...
        If oSorteo.idPgmSorteo = oSorteo.idPgmConcurso Then ' es el juego rector...
            For i As Integer = 0 To oPC.PgmSorteos.Count - 1
                For Each os As PgmSorteo In lstSorteos
                    If oPC.PgmSorteos(i).idPgmSorteo = os.idPgmSorteo Then
                        oPC.PgmSorteos.RemoveAt(i)
                        oPC.PgmSorteos.Add(oSorteo)
                    End If
                Next
            Next
            For Each os As PgmSorteo In lstSorteos
                MarcaExtractoNoCargado(os.idJuego)
            Next
            For i As Integer = 0 To tab.TabCount - 1
                For Each os As PgmSorteo In lstSorteos
                    If CType(tab.TabPages(i).Tag, PgmSorteo).idPgmSorteo = os.idPgmSorteo Then
                        tab.TabPages(i).Tag = os
                    End If
                Next
            Next
        Else ' Sino marco como "sucio" solo el juego en cuestion...
            For i As Integer = 0 To oPC.PgmSorteos.Count - 1
                If oPC.PgmSorteos(i).idPgmSorteo = oSorteo.idPgmSorteo Then
                    oPC.PgmSorteos.RemoveAt(i)
                    oPC.PgmSorteos.Add(oSorteo)
                End If
            Next
            MarcaExtractoNoCargado(oSorteo.idJuego)
        End If

        tab.SelectedTab.Tag = oSorteo
        TabJuegos_Click(sender, e)

        Me.Cursor = Cursors.Default
        Me.Refresh()
        'vienedepestania = False
    End Sub

    Private Sub btnGuardarCambios_Click_ant(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim oSorteo As New PgmSorteo
        Dim sBO As New PgmSorteoBO
        Dim oJuegoBO As New JuegoBO

        Dim fechaHoraCaducidadSorteo As DateTime
        Dim fechaHoraProximoSorteo As DateTime
        Dim fechaHoraInicio As DateTime
        Dim fechaHoraSorteo As DateTime
        Dim fechaHoraFin As DateTime
        Dim pozoProximoEstimado As Double
        Dim webB As WebBrowser
        Dim NombreWebB As String
        Dim tab As TabControl
        Dim nombreGrp As String
        Dim nombrepestania As String
        Dim pozo As String
        Dim separadordec As String
        Dim LetraNegrita As Font
        Dim sinPremios As Boolean = False
        Dim msjvarios As String = ""
        Dim etiqueta As Label
        Dim Boton As Button
        Dim btn As String = ""
        Dim _publicarweboff As String = General.PublicarWebOFF
        Dim _publicarwebon As String = General.PublicarWebON

        Me.Cursor = Cursors.WaitCursor
        MDIContenedor.Cursor = Cursors.WaitCursor

        LetraNegrita = New Font("Myriad Web Pro", 11, FontStyle.Bold)
        'se  obtiene el separaddor decimal d ela configuracion regional para poder formatear correctamente el pozo
        separadordec = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

        tab = Me.Controls("grpJuegos").Controls("tabJuegos")

        oSorteo = tab.SelectedTab.Tag
        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " INICIO btnGuardarCambios_click:sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
        If vienedepestania Then
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click: Click en PESTANIA")
        Else
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click: Click en BOTON actualizarextracto")
        End If
        'sale si se hace click en la pestania y el extracto ya fue generado
        If vienedepestania Then
            If TieneExtractoCargado(oSorteo.idJuego) Then
                vienedepestania = False
                Me.Cursor = Cursors.Default
                MDIContenedor.Cursor = Cursors.Default
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " FIN btnGuardarCambios_click:salio porque tiene extracto cargado.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                Exit Sub
            End If
        End If

        NombreWebB = "wbExtracto" & oSorteo.idJuego
        nombreGrp = "grpExtracto" & oSorteo.idJuego
        nombrepestania = "pstJuego-" & oSorteo.idJuego
        webB = New WebBrowser
        webB = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Controls(NombreWebB)

        Dim DataPickerF As DateTimePicker
        Dim DataPickerH As DateTimePicker
        Dim Text As TextBox
        Me.Cursor = Cursors.WaitCursor
        MDIContenedor.Cursor = Cursors.WaitCursor
        DataPickerF = getControlJ("grpActual", "dtpFechaPrescripcion")
        DataPickerH = getControlJ("grpActual", "dtpHoraPrescripcion")
        fechaHoraCaducidadSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerF = getControlJ("grpProximo", "dtpFechaProximo")
        DataPickerH = getControlJ("grpProximo", "dtpHoraProximo")
        fechaHoraProximoSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerH = getControlJ("grpActual", "dtpHoraInicioJuego")
        fechaHoraInicio = FormatDateTime(oSorteo.fechaHoraIniReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
        fechaHoraSorteo = FormatDateTime(oSorteo.fechaHora, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerH = getControlJ("grpActual", "dtpHoraFinJuego")
        fechaHoraFin = FormatDateTime(oSorteo.fechaHoraFinReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        Text = getControlJ("grpProximo", "txtPozo")
        If Text.Text = "" Then
            If Text.Enabled Then 'si esta habilitado lo debe ingresar
                MsgBox("Ingrese un monto para el pozo.", MsgBoxStyle.Information, MDIContenedor.Text)
                Me.Cursor = Cursors.Default
                MDIContenedor.Cursor = Cursors.Default
                vienedepestania = False
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " validacion:Ingrese un monto para el pozo. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                Exit Sub
            Else
                ' 30/07/2013. RL. el pozo estimado ya se actualizo
                Text.Text = oSorteo.PozoEstimado
                ''If oSorteo.idJuego = 30 Then
                ''    '*************
                ''    pozoEstimado = sorteos.helpers.General.Pozo_Estimado_Poceada
                ''    ' si tiene ganadores el 1er premio ,se utiliza el pozo asegurado que se configura en INI
                ''    If boPremio.Tiene_ganadores_premio(oSorteo.idPgmSorteo, 3001001) Then
                ''        If Not IsNumeric(pozoEstimado) Then
                ''            pozoProximoEstimado = 0
                ''        Else
                ''            pozoProximoEstimado = CDbl(pozoEstimado)
                ''        End If

                ''    Else 'si el 1er premio quedo vacante se utiliza el pozo acumulado que se obtiene desde la BD
                ''        pozoProximoEstimado = PozoBo.getPozoSugerido(oSorteo.idJuego)
                ''    End If
                ''    Text.Text = pozoProximoEstimado
                ''    '******************
                ''Else

                ''    pozoProximoEstimado = 0
                ''End If
            End If
        Else
            If Text.Enabled Then
                If Len(Text.Text) > 13 Then
                    MsgBox("Longitud de Pozo demasiado larga.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Me.Cursor = Cursors.Default
                    MDIContenedor.Cursor = Cursors.Default
                    vienedepestania = False
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Validacion:Longitud de Pozo demasiado larga. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Exit Sub
                End If
                If separadordec = "." Then
                    pozo = Replace(Text.Text, ",", ".")
                Else
                    pozo = Replace(Text.Text, ".", ",")
                End If

                If Not IsNumeric(pozo) Then
                    MsgBox("El pozo debe ser un valor numérico", MsgBoxStyle.Information)
                    Me.Cursor = Cursors.Default
                    MDIContenedor.Cursor = Cursors.Default
                    vienedepestania = False
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " validacion:El pozo debe ser un valor numérico. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Exit Sub
                End If
                If pozo < 0 Then
                    MsgBox("El pozo debe ser un valor positivo", MsgBoxStyle.Information, MDIContenedor.Text)
                    Me.Cursor = Cursors.Default
                    MDIContenedor.Cursor = Cursors.Default
                    vienedepestania = False
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " validacion:El pozo debe ser un valor positivo. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Exit Sub
                End If
                If pozo = 0 Then
                    MsgBox("El pozo debe ser un valor mayor a cero", MsgBoxStyle.Information, MDIContenedor.Text)
                    Me.Cursor = Cursors.Default
                    MDIContenedor.Cursor = Cursors.Default
                    vienedepestania = False
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " validacion: El pozo debe ser un valor mayor a cero. FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Exit Sub
                End If
                pozoProximoEstimado = pozo
            Else
                pozoProximoEstimado = Text.Text
            End If
        End If

        Try
            oSorteo.fechaHoraPrescripcion = fechaHoraCaducidadSorteo
            oSorteo.fechaHoraProximo = fechaHoraProximoSorteo

            oSorteo.fechaHoraIniReal = fechaHoraInicio
            oSorteo.fechaHoraFinReal = fechaHoraFin
            ' Actualiza datos en bd sorteador
            If sBO.ActualizarDatosSorteo(oSorteo, pozoProximoEstimado) = False Then
                Me.Cursor = Cursors.Default
                MDIContenedor.Cursor = Cursors.Default
                MsgBox("Error al actualizar sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                vienedepestania = False
                Exit Sub
            End If
            ' Actualiza datos en bd web
            Try
                If _publicarweboff = "S" Or _publicarwebon = "S" Then
                    sBO.ActualizarSorteoWeb(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, pozoProximoEstimado)
                Else
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Actualizar Extracto: La publicación a la Web no está habilitada. PublicarWebON: " & _publicarwebon & ", PublicarWebOFF: " & _publicarweboff & ".")
                End If
            Catch ex As Exception
                '** no se muestra ningun msj cuando es por fallo de internet
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:Problemas alActualizarSorteoWeb:" & ex.Message & " sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                ' MsgBox("btnGuardarCambios_Click:" & ex.Message, MsgBoxStyle.Information)
            End Try

            '**** control de premios y otras jurisidcciones **************************
            If Juegoconpremios(oSorteo) Then
                sinPremios = sBO.NoTienePremiosCargados(oSorteo.idPgmSorteo, oSorteo.idJuego)
                If sinPremios Then
                    msjvarios = msjvarios & "Faltan cargar premios" & vbCrLf
                    '***deshabilita boton confirmar
                    Try
                        Boton = getControl("btnConfirmarExtracto")
                        Boton.Enabled = False
                    Catch ex As Exception
                    End Try
                Else
                    If oSorteo.idEstadoPgmConcurso = 40 Then
                        '***habilita boton confirmar
                        Try
                            Boton = getControl("btnConfirmarExtracto")
                            Boton.Enabled = True
                        Catch ex As Exception
                        End Try
                    End If
                End If
            End If
            If oJuegoBO.esQuiniela(oSorteo.idJuego) Then
                If sBO.OtrasJurisdicciones_SinConfirmar(oSorteo.idPgmSorteo) Then
                    msjvarios = msjvarios & "Faltan cargar otras jurisdicciones"
                    '***deshabilita boton confirmar
                    Try
                        Boton = getControl("btnConfirmarExtracto")
                        Boton.Enabled = False
                    Catch ex As Exception
                    End Try
                Else
                    '** habilita el boton confirmar
                    If oSorteo.idEstadoPgmConcurso = 40 Then
                        Try
                            Boton = getControl("btnConfirmarExtracto")
                            Boton.Enabled = True
                        Catch ex As Exception
                        End Try
                    End If
                End If
            End If

            Try 'si ya esta creado
                etiqueta = getControl("lblMensajes")
                etiqueta.Text = msjvarios
            Catch ex As Exception 'sino lo crea
                etiqueta = New Label
                etiqueta.Name = "lblMensajes" & oSorteo.idJuego
                etiqueta.Text = msjvarios
                'etiqueta.Font = LetraNegrita
                etiqueta.ForeColor = Color.Red
                etiqueta.Location = New System.Drawing.Point(777, 300)
                etiqueta.Size = New System.Drawing.Size(190, 160)
                etiqueta.Visible = True
                Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Controls.Add(etiqueta)
            End Try

            '********************fin control premios y otras jurisdicciones**********
            oSorteo = sBO.getPgmSorteo(oSorteo.idPgmSorteo)
            Borrardelista(oSorteo.idJuego)

            Dim url As String = ""
            Dim panelmsj As Panel
            Try '*** controla que el panel este creado
                panelmsj = getControlJ("GrpExtracto" & oSorteo.idJuego, "panelmsj")
            Catch ex2 As Exception
                '*** crea el panel para visualizar mensaje
                panelmsj = New Panel
                panelmsj.Name = "panelmsj" & oSorteo.idJuego
                panelmsj.Location = New System.Drawing.Point(3, 20)
                panelmsj.Width = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Width - 10
                panelmsj.Height = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Height - 25
                panelmsj.Visible = False
                etiqueta = New Label
                'etiqueta.Text = "No se encontró el archivo de Extracto correspondiente a este juego."
                etiqueta.Text = ""
                etiqueta.Width = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Width - 40
                etiqueta.ForeColor = Color.Red
                panelmsj.Controls.Add(etiqueta)
                Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Controls.Add(panelmsj)
            End Try

            sinPremios = sBO.NoTienePremiosCargados(oSorteo.idPgmSorteo, oSorteo.idJuego)
            If Juegoconpremios(oSorteo) And sinPremios Then
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:No tiene premios cargados.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                'If sinPremios Then
                webB.Visible = False
                Try '*** controla que el panel este creado
                    panelmsj = getControlJ("GrpExtracto" & oSorteo.idJuego, "panelmsj")
                    panelmsj.Visible = True
                Catch ex2 As Exception
                End Try
            End If
            Try
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:Va a obtener Extracto remoto para  sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                If General.Modo_Operacion <> "PC-B" Then
                    If General.PublicaExtractosWSRestOFF = "S" Then
                        url = sBO.getUrlExtractoOficialRest(oSorteo)
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto REST: ->" & url & "<- para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Else
                        url = sBO.getUrlExtractoOficial(oSorteo)
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto NET: ->" & url & "<- para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    End If

                    Me.Cursor = Cursors.WaitCursor
                    webB.Refresh()
                    webB.Url = New System.Uri(url)
                    webB.Refresh()
                    webB.Visible = True
                    Me.Cursor = Cursors.Default
                Else
                    url = ""
                End If
                panelmsj = getControlJ("GrpExtracto" & oSorteo.idJuego, "panelmsj")
                panelmsj.Visible = False
                Try '** se fija si esta creado el control
                    etiqueta = getControl("lbllocal")
                    etiqueta.Text = ""
                    etiqueta.Visible = False
                Catch ex1 As Exception
                End Try
                MarcaExtractoCargado(oSorteo.idJuego)
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:Extracto Remoto OK para  sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:Problema al Obtener Extracto Remoto:" & ex.Message & " sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                '*** si el error es porque no tiene premios cargados,no se genera el extracto para tener coherencia con el WS
                sinPremios = sBO.NoTienePremiosCargados(oSorteo.idPgmSorteo, oSorteo.idJuego)
                If Juegoconpremios(oSorteo) And sinPremios Then
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:No tiene premios cargados.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    'If sinPremios Then
                    webB.Visible = False
                    Try '*** controla que el panel este creado
                        panelmsj = getControlJ("GrpExtracto" & oSorteo.idJuego, "panelmsj")
                        panelmsj.Visible = True
                    Catch ex2 As Exception
                        '*** crea el panel para visualizar mensaje
                        panelmsj = New Panel
                        panelmsj.Name = "panelmsj" & oSorteo.idJuego
                        panelmsj.Location = New System.Drawing.Point(3, 20)
                        panelmsj.Width = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Width - 10
                        panelmsj.Height = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Height - 25
                        panelmsj.Visible = False
                        etiqueta = New Label
                        etiqueta.Text = "No se encontró el archivo de Extracto correspondiente a este juego."
                        etiqueta.Width = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Width - 40
                        etiqueta.ForeColor = Color.Red
                        panelmsj.Controls.Add(etiqueta)
                        panelmsj.Visible = True
                        Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Controls.Add(panelmsj)
                    End Try
                    'End If

                Else     '** generacion de extracto local
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:Va a generar el extracto Local sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Dim etiquetalocal As String = ""
                    Dim ds As New ExtractoReporte

                    etiquetalocal = "lbllocal" & oSorteo.idJuego
                    ds.GenerarExtractoLocal(oSorteo.idPgmSorteo, url, General.PathInformes)
                    webB.Url = New System.Uri(url)
                    webB.Refresh()
                    webB.Visible = True
                    '*** muestra msj de fallo de conexion

                    Try '** se fija si esta creado el control
                        etiqueta = getControl("lbllocal")
                        If _publicarweboff = "S" Or _publicarwebon = "S" Then
                            etiqueta.Text = "Debido a problemas en la publicación se ha generado el Extracto de manera local" & vbCrLf & "Cuando se restablezca el servicio de Internet deberá re-publicar y confirmar."
                        Else
                            etiqueta.Text = "Extracto Local."
                        End If
                    Catch ex1 As Exception '** sino trata de crearlo
                        '*** muestra msj de fallo de conexion
                        etiqueta = New Label
                        etiqueta.Name = "lbllocal" & oSorteo.idJuego
                        If _publicarweboff = "S" Or _publicarwebon = "S" Then
                            etiqueta.Text = "Debido a problemas en la publicación se ha generado el Extracto de manera local" & vbCrLf & "Cuando se restablezca el servicio de Internet deberá re-publicar y confirmar."
                        Else
                            etiqueta.Text = "Extracto Local."
                        End If
                        etiqueta.Font = LetraNegrita
                        etiqueta.ForeColor = Color.Red
                        etiqueta.Location = New System.Drawing.Point(777, 150)
                        etiqueta.Size = New System.Drawing.Size(180, 160)
                        etiqueta.Visible = True
                        Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls.Add(etiqueta)
                    End Try

                    '** deshabilita el boton confirmar
                    Try
                        Dim botonConfirmar As Button
                        botonConfirmar = getControl("btnConfirmarExtracto")
                        botonConfirmar.Enabled = False
                        If msjvarios.Trim = "" Then
                            If _publicarweboff = "N" And _publicarwebon = "N" Then
                                botonConfirmar.Enabled = True
                            Else
                                botonConfirmar.Enabled = False
                            End If
                        Else
                            If General.Modo_Operacion = "PC-B" Then
                                botonConfirmar.Enabled = True
                            End If
                        End If
                    Catch exbt As Exception
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click: No pudo crear control btnConfirmarExtracto")
                    End Try
                    'si es quiniela deshabilita el boton confirmar santafe
                    If oJuegoBO.esQuiniela(oSorteo.idJuego) Then
                        Try
                            Dim botonConfirmarStafe As Button
                            botonConfirmarStafe = getControl("btnConfirmarLoteria")
                            If msjvarios.Trim = "" Then
                                If _publicarweboff = "N" And _publicarwebon = "N" Then
                                    botonConfirmarStafe.Enabled = True
                                Else
                                    botonConfirmarStafe.Enabled = False
                                End If
                            End If

                        Catch exbs As Exception
                            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:No pudo crear control btnConfirmarLoteria")
                        End Try
                    End If
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " btnGuardarCambios_click:Extracto Local OK. Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                End If
                vienedepestania = False
                Me.Cursor = Cursors.Default
                MDIContenedor.Cursor = Cursors.Default
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                Exit Sub
            End Try
            Me.Cursor = Cursors.Default
            MDIContenedor.Cursor = Cursors.Default
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " FIN btnGuardarCambios_click.Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MDIContenedor.Cursor = Cursors.Default
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " FIN btnGuardarCambios_click. Problemas:" & ex.Message & "Sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
            MsgBox("btnGuardarCambios_Click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
        vienedepestania = False
    End Sub

    Private Function PozoEstimadoOK(ByRef oSorteo As PgmSorteo, ByRef text As TextBox) As Double
        PozoEstimadoOK = -1.0

        'Text = getControlJ("grpProximo-", "txtPozo-")
        If oSorteo.idJuego = 30 Or oSorteo.idJuego = 4 Or oSorteo.idJuego = 13 Then
            If oSorteo.PozoEstimado > 0 Then
                PozoEstimadoOK = oSorteo.PozoEstimado
                If text.Enabled Then
                    If Double.TryParse(text.Text.Trim, PozoEstimadoOK) Then
                        If PozoEstimadoOK <= 0 Then
                            PozoEstimadoOK = -1.0
                        End If
                    Else
                        PozoEstimadoOK = -1.0
                    End If
                End If

            End If
        Else
            PozoEstimadoOK = oSorteo.PozoEstimado
        End If
    End Function

    Private Sub btnConfirmarExtracto_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim sBO As New PgmSorteoBO
        Dim cBO As New PgmConcursoBO
        Dim jBO As New JuegoBO
        Dim oSorteo As New PgmSorteo
        Dim oJuego As New Juego

        Dim DataPickerF As DateTimePicker
        Dim DataPickerH As DateTimePicker
        Dim Text As TextBox

        Dim fechaHoraCaducidadSorteo As DateTime
        Dim fechaHoraProximoSorteo As DateTime
        Dim fechaHoraInicio As DateTime
        Dim fechaHoraSorteo As DateTime
        Dim fechaHoraFin As DateTime
        Dim pozoProximoEstimado As Double

        Dim nombreGrp As String
        Dim nombrepestania As String
        Dim boton As Button
        Dim nombreBoton As String
        Dim tab As TabControl
        'variables para pantalla  generar y enviar
        Dim _nroSorteo As Long = 0
        Dim _nombreSorteo As String = 0
        Dim _sorteo As New PgmSorteo
        Dim _idPgmconcurso As Long = 0
        Dim _huboErrores As Boolean = False
        Dim msjErrores As String = ""
        Dim msjExtracto As String = ""
        Dim msjExtractoWeb As String = ""
        Dim msjDisplay As String = ""
        Dim msjBkpRst As String = ""
        Dim oBkp As New Sorteos.Helpers.BkpRestore
        Dim _publicarweboff As String = General.PublicarWebOFF
        Dim _publicarwebon As String = General.PublicarWebON
        Dim _PublicaExtractosWSRestOFF As String = General.PublicaExtractosWSRestOFF
        Dim _PublicaExtractosWSRestON As String = General.PublicaExtractosWSRestON
        Dim destino As String = "0"
        Dim path As String = ""
        Dim msj As String = ""
        Dim cnStr As String = ""
        Dim pathBkp As String = ""
        Dim cnStrRem As String = ""
        Dim pathBkpRem As String = ""
        Dim pathBdRem As String = ""
        Dim bd As String = ""
        Dim _Modo_Operacion As String = General.Modo_Operacion.ToUpper()
        Dim msgRet As String = ""

        If _Modo_Operacion = "PC-B" Then
            If Not (MsgBox("Esta acción dará por finalizado el sorteo y generará el ARCHIVO DE CONTROL de Extracto correspondiente para cuadratura. " & vbCrLf & "Haga click en ACEPTAR sólo si está seguro. Caso contrario haga click en CANCELAR.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ATENCION: Se requiere confirmación") = MsgBoxResult.Ok) Then
                Exit Sub
            End If
        Else
            If Not (MsgBox("Esta acción dará por finalizado el sorteo y confirmado el Extracto correspondiente. Esta operación es IRREVERSIBLE. " & vbCrLf & "¿Está seguro que desea establecer como DEFINITIVOS los datos del Extracto?", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ATENCION: Se requiere confirmación") = MsgBoxResult.Ok) Then
                Exit Sub
            End If
        End If
        '** Control de cambios 
        If juegoEstaModificado(oSorteo.idJuego) Then
            MsgBox("Se han detectado cambios en los datos del sorteo. Guarde los datos antes de confirmarlo.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End If

        Cursor = Cursors.WaitCursor
        Me.Refresh()
        Application.DoEvents()

        tab = Me.Controls("grpJuegos").Controls("tabJuegos")
        oSorteo = tab.SelectedTab.Tag
        FileSystemHelper.Log(" ConcursoFinalizar:Inicio confirmar extracto , sorteo:" & oSorteo.idPgmSorteo)
        Cursor = Cursors.WaitCursor
        Me.Refresh()
        ' Validacion del Pozo Estimado Proximo Sorteo
        Text = getControlJ("grpProximo-", "txtPozo-")

        If sBO.NoTienePozos(oSorteo.idPgmSorteo, oPC.concurso.IdConcurso) Then
            pozoProximoEstimado = PozoEstimadoOK(oSorteo, Text)
            If pozoProximoEstimado < 0 Then
                Me.Cursor = Cursors.Default
                Me.Refresh()
                MsgBox("El importe del Pozo Estimado no ha sido registrado o es incorrecto. Verifique.", MsgBoxStyle.Information, MDIContenedor.Text)
                If Text.Enabled Then Text.Focus()
                Exit Sub
            End If
        Else
            pozoProximoEstimado = 0
        End If
        '29/12/16 HG controla la progresion para loteria,si es cero,vuelvo a calcular,si sigue en cero,mostramos mensaje
        If oSorteo.idJuego = 50 And General.Jurisdiccion = "S" Then
            Dim _progresion As Integer = 0
            _progresion = sBO.RecuperaProgresionLoteria(oSorteo.idPgmSorteo)
            If _progresion = 0 Then
                _progresion = sBO.getProgresionLoteria(oSorteo.idJuego, oSorteo.nroSorteo)
            End If
            If _progresion = 0 Then
                Me.Cursor = Cursors.Default
                Me.Refresh()
                MsgBox("Confirmación Cancelada. La progresión calculada es CERO.Por favor , realice los siguientes pasos." & vbCr & "1 - Revierta la extracción , conservando los numeros" & vbCr & "2 - Confirme nuevamente la extracción" & vbCr & "3 - Dirígase a la pantalla de extracto y verifique que la progresión sea distinta de cero" & vbCr & "4- Si no se corrige,contáctese con soporte técnico.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
        End If

        nombreGrp = "grpExtracto-" & oSorteo.idJuego
        nombrepestania = "pstJuego-" & oSorteo.idJuego
        nombreBoton = "btnGuardarCambios-" & oSorteo.idJuego

        ''**** para actualziar el webbrowser
        ''Dim NombreWebB As String
        ''Dim webB As WebBrowser
        ''NombreWebB = "wbExtracto-" & oSorteo.idJuego
        ''webB = New WebBrowser
        ''webB = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Controls(NombreWebB)

        DataPickerF = getControlJ("grpActual-", "dtpFechaPrescripcion-")
        DataPickerH = getControlJ("grpActual-", "dtpHoraPrescripcion-")
        fechaHoraCaducidadSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerF = getControlJ("grpProximo-", "dtpFechaProximo-")
        DataPickerH = getControlJ("grpProximo-", "dtpHoraProximo-")
        fechaHoraProximoSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerH = getControlJ("grpActual-", "dtpHoraInicioJuego-")
        fechaHoraInicio = FormatDateTime(oSorteo.fechaHoraIniReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
        fechaHoraSorteo = FormatDateTime(oSorteo.fechaHora, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        DataPickerH = getControlJ("grpActual-", "dtpHoraFinJuego-")
        fechaHoraFin = FormatDateTime(oSorteo.fechaHoraFinReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

        Cursor = Cursors.WaitCursor
        Me.Refresh()
        If sBO.ControlCuadratura(oSorteo, msgRet, False) Then
            ' primero actualizo estado del PgmSorteo en bd local
            Try
                FileSystemHelper.Log(" ConcursoFinalizar:llama a sBO.ActualizarEstadoSorteo")
                sBO.ActualizarEstadoSorteo(oSorteo.idPgmSorteo, 50)
            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Error btnConfirmarExtracto_click:" & ex.Message)
                Me.Cursor = Cursors.Default
                Me.Refresh()
                MsgBox("Confirmar extracto oficial -> " & ex.Message, MsgBoxStyle.Critical, MDIContenedor.Text)
                Exit Sub
            End Try
            '**24/10/2012
            ' segundo actualizo estado del PgmConcurso en bd local

            Cursor = Cursors.WaitCursor
            Me.Refresh()
            Try
                FileSystemHelper.Log(" ConcursoFinalizar:llama a cBO.ActualizarEstadoSorteo")
                cBO.ActualizarEstado(CboConcurso.SelectedValue) 'oSorteo.PgmConcurso.idPgmConcurso)
            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Error btnConfirmarExtracto_click:" & ex.Message)
                Me.Cursor = Cursors.Default
                Me.Refresh()
                MsgBox("Confirmar extracto oficial -> " & ex.Message, MsgBoxStyle.Critical, MDIContenedor.Text)
                Exit Sub
            End Try
            If _Modo_Operacion <> "PC-B" Then
                ' tercero autorizo extracto oficial en la web
                Cursor = Cursors.WaitCursor
                Me.Refresh()
                'AGREGADO FSCOTTA
                If _PublicaExtractosWSRestON = "S" Or _PublicaExtractosWSRestOFF = "S" Then
                    Try
                        sBO.publicarWEBRest(oSorteo)
                        'sBO.AutorizarExtractoOficial(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, pozoProximoEstimado, 1)
                    Catch ex As Exception
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & "ConcursoFinalizar: Error publicarWEBRest btnConfirmarExtracto_click:" & ex.Message)
                        'Me.Cursor = Cursors.Default
                        _huboErrores = True
                        msjExtractoWeb = " - Problemas al autorizar el extracto en la Web."
                    End Try
                End If
                '-------------------------------
                Try
                    FileSystemHelper.Log(" ConcursoFinalizar:llama a sBO.AutorizarExtractoOficial")
                    sBO.AutorizarExtractoOficial(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin, pozoProximoEstimado, 1)
                Catch ex As Exception
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:Error btnConfirmarExtracto_click:" & ex.Message)
                    'Me.Cursor = Cursors.Default
                    _huboErrores = True
                    msjExtractoWeb = " - Problemas al autorizar el extracto en la Web."
                End Try

                ' cuarto publico a display (off line se hace siempre)
                Cursor = Cursors.WaitCursor
                Me.Refresh()
                Try
                    FileSystemHelper.Log(" llama a cBO.publicarDisplay")
                    cBO.publicarDisplay(CboConcurso.SelectedValue)
                Catch ex As Exception
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Error btnConfirmarExtracto_click:" & ex.Message)
                    _huboErrores = True
                    msjDisplay = " - Problemas al publicar a Display. Para actualizar el Display, ingrese desde el menú Interfaces."
                End Try
            End If

            ' si estoy en modo doble carga hago el bkp - rst
            If (_Modo_Operacion = "PC-A" Or _Modo_Operacion = "PC-B") And General.ConnStringRem.Trim <> "" Then
                oBkp = New Sorteos.Helpers.BkpRestore()
                cnStr = General.ConnString
                pathBkp = General.Carpeta_Bkp
                cnStrRem = General.ConnStringRem
                pathBkpRem = General.Carpeta_Bkp_Rem
                pathBdRem = General.Carpeta_Bd_Rem

                Try
                    If oBkp.backupDB(cnStr, pathBkp, bd) Then
                        Try
                            If Not oBkp.restoreDB(bd, cnStrRem, pathBkpRem, pathBdRem) Then
                                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:Error btnConfirmarExtracto_click: - Problemas al realizar la restauración de copia de seguridad en el sistema de doble carga " & _Modo_Operacion & ".")
                                _huboErrores = True
                                msjBkpRst = " - Problemas al realizar la restauración de copia de seguridad en el sistema de doble carga " & _Modo_Operacion & ". Puede continuar con normalidad pero avise via mail a Soporte."
                            End If
                        Catch exRST As Exception
                            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:Error btnConfirmarExtracto_click: - Problemas al realizar la restauración de copia de seguridad en el sistema de doble carga " & _Modo_Operacion & ".")
                            _huboErrores = True
                            msjBkpRst = " - Problemas al realizar la restauración de copia de seguridad en el sistema de doble carga " & _Modo_Operacion & ". Puede continuar con normalidad pero avise via mail a Soporte."
                        End Try
                    Else
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar:Error btnConfirmarExtracto_click: - Problemas al realizar la copia de seguridad para el sistema de doble carga " & _Modo_Operacion & ".")
                        _huboErrores = True
                        msjBkpRst = " - Problemas al realizar la copia de seguridad para el sistema de doble carga " & _Modo_Operacion & ". Puede continuar con normalidad pero avise via mail a Soporte."
                    End If
                Catch exBKP As Exception
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar: Error btnConfirmarExtracto_click: - Problemas al realizar la copia de seguridad para el sistema de doble carga " & _Modo_Operacion & ".")
                    _huboErrores = True
                    msjBkpRst = " - Problemas al realizar la copia de seguridad para el sistema de doble carga " & _Modo_Operacion & ". Puede continuar con normalidad pero avise via mail a Soporte."
                End Try
            End If

            If _huboErrores Then
                msjErrores = "La confirmación ha sido realizada con éxito pero se presentaron las siguientes situaciones:" & vbCrLf
                If msjExtractoWeb.Trim <> "" Then
                    msjErrores = msjErrores & msjExtractoWeb & vbCrLf
                End If
                If msjExtracto.Trim <> "" Then
                    msjErrores = msjErrores & msjExtracto & vbCrLf
                End If
                If msjDisplay.Trim <> "" Then
                    msjErrores = msjErrores & msjDisplay & vbCrLf
                End If
                If msjBkpRst.Trim <> "" Then
                    msjErrores = msjErrores & msjBkpRst & vbCrLf
                End If
                Me.Cursor = Cursors.Default
                Me.Refresh()

                MsgBox(msjErrores, MsgBoxStyle.Exclamation, MDIContenedor.Text)
            End If

            ' Obtengo datos actualizados del sorteo y lo asigno a la pesatania y concurso
            oSorteo = sBO.getPgmSorteo(oSorteo.idPgmSorteo)
            For i As Integer = 0 To oPC.PgmSorteos.Count - 1
                If oPC.PgmSorteos(i).idPgmSorteo = oSorteo.idPgmSorteo Then
                    oPC.PgmSorteos.RemoveAt(i)
                    oPC.PgmSorteos.Add(oSorteo)
                End If
            Next
            MarcaExtractoNoCargado(oSorteo.idJuego)

            tab.SelectedTab.Tag = oSorteo
            TabJuegos_Click(sender, e)

            Me.Cursor = Cursors.Default
            Me.Refresh()
            FileSystemHelper.Log(" ConcursoFinalizar: confirmacion Extracto Ok,sorteo:" & oSorteo.idPgmSorteo)


            ' Traigo la configuracion del juego
            oJuego = jBO.getJuego(oSorteo.idJuego)

            ' Generacion de archivo Extracto para Boldt
            If _Modo_Operacion <> "PC-B" And (oSorteo.idJuego = 4 Or oSorteo.idJuego = 13 Or oSorteo.idJuego = 30 Or oSorteo.idJuego = 50 Or oSorteo.idJuego = 51) Then
                frmGeneracionExtractoBoldt.vSoloJurLocal = False
                frmGeneracionExtractoBoldt.vPath = General.CarpetaExtractoBoltConfirmado
            End If
            FileSystemHelper.Log(" ConcursoFinalizar:llama a generacion de extracto para boldt,sorteo:" & oSorteo.idPgmSorteo)
            frmGeneracionExtractoBoldt.voSorteo = New PgmSorteo
            frmGeneracionExtractoBoldt.voSorteo = oSorteo
            frmGeneracionExtractoBoldt.vNroSorteo = oSorteo.nroSorteo
            frmGeneracionExtractoBoldt.vNombreSorteo = tab.SelectedTab.Text
            frmGeneracionExtractoBoldt.vSoloJurLocal = False
            frmGeneracionExtractoBoldt.StartPosition = FormStartPosition.CenterParent
            frmGeneracionExtractoBoldt.ShowDialog()


            ' Envio de Extracto a Medios
            ' ''If General.Jurisdiccion = "E" Or oSorteo.idJuego = 4 Or oSorteo.idJuego = 13 Then
            ''If oJuego.EnviarAMedios.Trim.ToUpper <> "NO" Then
            ''    frmEnvioExtractoSantafe.vEnviarExtractoOficial = oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E")
            ''    frmEnvioExtractoSantafe.vEnviarEncriptado = oJuego.EnviarAMedios.Trim.ToUpper.EndsWith("E")
            ''    frmEnvioExtractoSantafe.vidPgmConcurso = oSorteo.idPgmConcurso
            ''    frmEnvioExtractoSantafe.vidPgmSorteo = oSorteo.idPgmSorteo
            ''    frmEnvioExtractoSantafe.ShowDialog()
            ''End If
            If _Modo_Operacion <> "PC-B" Then
                If (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("N") And oSorteo.idEstadoPgmConcurso = 50)) _
                    Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("E") And oSorteo.idEstadoPgmConcurso = 50)) _
                    Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("L") And oSorteo.idEstadoPgmConcurso = 50)) _
                    Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("L") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("N") And oSorteo.idEstadoPgmConcurso >= 40)) _
                    Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("L") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("E") And oSorteo.idEstadoPgmConcurso >= 40)) _
                     Then
                    Try
                        FileSystemHelper.Log(" ConcursoFinalizar: llama al envio de mail para medios,sorteo:" & oSorteo.idPgmSorteo)
                        FrmEnviarMailExtracto.vidPgmConcurso = oSorteo.idPgmConcurso
                        FrmEnviarMailExtracto.vidPgmSorteo = oSorteo.idPgmSorteo
                        FrmEnviarMailExtracto.vosorteo = oSorteo
                        FrmEnviarMailExtracto.StartPosition = FormStartPosition.CenterParent

                        FrmEnviarMailExtracto.ShowDialog()
                    Catch ex As Exception
                        MsgBox(ex.Message)
                    End Try

                End If
            End If
        Else
            Me.Cursor = Cursors.Default
            Me.Refresh()
            If _Modo_Operacion = "PC-A" Then
                If msgRet.Trim() = "" Then
                    'llama a un formulario desde donde imprimir
                    FrmMensajeComparacionExtractos._idpgmconcurso = oSorteo.idPgmConcurso
                    FrmMensajeComparacionExtractos._idpgmsorteo = oSorteo.idPgmSorteo
                    FrmMensajeComparacionExtractos.ShowDialog()
                    Exit Sub
                    '
                    'MsgBox("No hay correspondencia de datos entre ambas cargas. Se imprimirá un listado de diferencias.", MsgBoxStyle.Information, MDIContenedor.Text)
                    ' imprimir reporte diferencias
                    'If Not sBO.GenerarListadoDifCuad(oSorteo.idPgmConcurso, oSorteo.idPgmSorteo, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, destino, path, msj) Then
                    'MsgBox(msj, MsgBoxStyle.Information)
                    'Exit Sub
                    'End If
                Else
                    MsgBox(msgRet, MsgBoxStyle.Information, MDIContenedor.Text)
                End If
            Else
                MsgBox("No hay correspondencia de datos entre los existentes en Display y Extracto.", MsgBoxStyle.Information, MDIContenedor.Text)
            End If
        End If
    End Sub

    Private Function SeleccionarPestania(ByVal index As Integer) As Boolean
        Try
            Dim tabp As TabPage
            Dim i As Integer
            Dim oSorteo As PgmSorteo
            i = 0
            SeleccionarPestania = False
            For Each tabp In TabJuegos.TabPages
                oSorteo = tabp.Tag

                If oSorteo.idJuego = index Then
                    TabJuegos.SelectedIndex = i
                    TabJuegos.SelectedTab = tabp
                    SeleccionarPestania = True
                    Exit Function
                End If
                i = i + 1
            Next
        Catch ex As Exception
            MsgBox("Problema SeleccionarPestania:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub CboConcurso_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboConcurso.SelectedIndexChanged
        Dim oPgmConBO As New PgmConcursoBO
        Dim _juegoextracto As cValorPosicion

        PTBreloj.Visible = True

        Me.Cursor = Cursors.WaitCursor
        Me.Refresh()

        ' Traigo datos actualizados del pgmConcurso
        Me.oPC = oPgmConBO.getPgmConcurso(CType(CboConcurso.SelectedItem, PgmConcurso).idPgmConcurso)

        'If Not inicio Then
        JuegosModificados = Nothing
        _cargando = True
        JuegosExtractocargados = Nothing
        JuegosExtractocargados = New ListaOrdenada(Of cValorPosicion)

        For Each oPgmSorteo In oPC.PgmSorteos
            '*** inilizaza la lista de juegos extractos
            _juegoextracto = New cValorPosicion

            '*** carga el juego y valor -1(no se cargo extracto) 
            _juegoextracto.Posicion = oPgmSorteo.idJuego
            _juegoextracto.Valor = -1
            JuegosExtractocargados.Add(_juegoextracto)
        Next

        DTPFechaConcurso.Value = oPC.fechaHora
        dtpHoraConcurso.Value = oPC.fechaHora

        ' Creo todo el contenido interno del TabJuegos
        crearPestaniasJuegos()
        grpJuegos.Visible = True
        TabJuegos.Visible = True
        SeleccionarPestania(oPC.idPgmSorteoPrincipal / 1000000) 'Juego Rector

        TabJuegos_Click(sender, e)

        'setControlesValoresConcurso(CboConcurso.SelectedItem)
        grpJuegos.Visible = True
        TabJuegos.Visible = True
        'TabJuegos.SelectedTab.Controls("grpActual-" & (oPC.idPgmSorteoPrincipal / 1000000)).Visible = True
        _cargando = False

        PTBreloj.Visible = False
        Me.Cursor = Cursors.Default
        Me.Refresh()
        'End If
    End Sub

    Private Sub ObtenerIndiceResolucion(ByRef pIndice As Integer, ByRef Pleft As Integer, ByRef pAlturaGrilla As Integer)
        Dim ancho As Integer
        Try
            pIndice = 0
            Pleft = 0
            pAlturaGrilla = 0
            ancho = Screen.PrimaryScreen.Bounds.Width
            If ancho < 800 Then
                ancho = 800
            End If
            If ancho >= 800 And ancho < 1024 Then
                ancho = 800
            ElseIf ancho >= 1024 And ancho < 1280 Then
                ancho = 1024
            ElseIf ancho > 1280 Then
                ancho = 1024
            End If
            Select Case ancho
                Case 1024
                    pIndice = 120
                    pAlturaGrilla = 150
                    Pleft = 50
                Case 1280
                    pIndice = 120
                    Pleft = 50
                    pAlturaGrilla = 150
            End Select

        Catch ex As Exception
            Pleft = 0
            pIndice = 0
            MsgBox("Error ObtenerIndiceResolucion:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub btnConfirmarExtractoSantafe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sBO As New PgmSorteoBO
        Dim cBO As New PgmConcursoBO
        Dim oSorteo As New PgmSorteo
        Dim fechaHoraCaducidadSorteo As DateTime
        Dim fechaHoraProximoSorteo As DateTime
        Dim fechaHoraInicio As DateTime
        Dim fechaHoraSorteo As DateTime
        Dim fechaHoraFin As DateTime
        Dim tab As TabControl
        Dim DataPickerF As DateTimePicker
        Dim DataPickerH As DateTimePicker
        'variable  pantalla enviar
        Dim _idpgmconcurso As Long = 0
        Dim jBO As New JuegoBO
        Dim oJuego As New Juego
        Dim _Modo_Operacion As String = General.Modo_Operacion.ToUpper()
        Dim msgRet As String = ""

        If _Modo_Operacion = "PC-B" Then
            If Not (MsgBox("Esta acción generará el ARCHIVO de CONTROL de Extracto del SORTEO PROPIO DE " & IIf(General.Jurisdiccion = "S", "QLA", "TLA") & ". Luego podrá anularlos en caso de ser necesario. " & vbCrLf & "¿Continuar?", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ATENCION: Se requiere confirmación") = MsgBoxResult.Ok) Then
                Exit Sub
            End If
        Else
            If Not (MsgBox("Esta acción confirmará únicamente el Extracto del SORTEO PROPIO DE " & IIf(General.Jurisdiccion = "S", "QLA", "TLA") & ". Luego podrá anularlos en caso de ser necesario. " & vbCrLf & "¿Continuar?", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ATENCION: Se requiere confirmación") = MsgBoxResult.Ok) Then
                Exit Sub
            End If
        End If
       

        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Refresh()

            tab = Me.Controls("grpJuegos").Controls("tabJuegos")
            oSorteo = tab.SelectedTab.Tag

            FileSystemHelper.Log(" Inicio confirmar SORTEO PROPIO, sorteo:" & oSorteo.idPgmSorteo)
            If sBO.ControlCuadratura(oSorteo, msgRet, True) Then
                '** Control de cambios 
                If juegoEstaModificado(oSorteo.idJuego) Then
                    Me.Cursor = Cursors.Default
                    Me.Refresh()
                    MsgBox("Se han detectado cambios en los datos del sorteo. Guarde los datos antes de confirmarlo.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End If

                DataPickerF = getControlJ("grpActual-", "dtpFechaPrescripcion-")
                DataPickerH = getControlJ("grpActual-", "dtpHoraPrescripcion-")
                fechaHoraCaducidadSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

                DataPickerF = getControlJ("grpProximo-", "dtpFechaProximo-")
                DataPickerH = getControlJ("grpProximo-", "dtpHoraProximo-")
                fechaHoraProximoSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

                DataPickerH = getControlJ("grpActual-", "dtpHoraInicioJuego-")
                fechaHoraInicio = FormatDateTime(oSorteo.fechaHoraIniReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
                fechaHoraSorteo = FormatDateTime(oSorteo.fechaHora, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

                DataPickerH = getControlJ("grpActual-", "dtpHoraFinJuego-")
                fechaHoraFin = FormatDateTime(oSorteo.fechaHoraFinReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

                sBO.ConfirmarQuinielaSF(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin)

                ' Obtengo datos actualizados del sorteo y lo asigno a la pesatania y concurso
                oSorteo = sBO.getPgmSorteo(oSorteo.idPgmSorteo)
                For i As Integer = 0 To oPC.PgmSorteos.Count - 1
                    If oPC.PgmSorteos(i).idPgmSorteo = oSorteo.idPgmSorteo Then
                        oPC.PgmSorteos.RemoveAt(i)
                        oPC.PgmSorteos.Add(oSorteo)
                    End If
                Next
                MarcaExtractoNoCargado(oSorteo.idJuego)

                tab.SelectedTab.Tag = oSorteo
                TabJuegos_Click(sender, e)

                Me.Cursor = Cursors.Default
                Me.Refresh()

                MsgBox("La confirmación del SORTEO PROPIO DE QLA se ha realizado correctamente.", MsgBoxStyle.Information, MDIContenedor.Text)

                ' Envio Extracto a medios
                ''frmEnvioExtractoSantafe.vEnviarExtractoOficial = (General.ExtractoAMedios = "S")
                ''frmEnvioExtractoSantafe.vidPgmConcurso = oPC.idPgmConcurso
                ''frmEnvioExtractoSantafe.vidPgmSorteo = oPC.idPgmSorteoPrincipal ' para verificar si el rector ya esta confirmado
                ''frmEnvioExtractoSantafe.ShowDialog()

                ' Traigo la configuracion del juego
                oJuego = jBO.getJuego(oSorteo.idJuego)
                If _Modo_Operacion <> "PC-B" Then
                    If (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("N") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("L") And oSorteo.idEstadoPgmConcurso >= 40)) _
                        Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("E") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("L") And oSorteo.idEstadoPgmConcurso >= 40)) _
                        Or (oJuego.EnviarAMedios.Trim.ToUpper.StartsWith("L") And (oJuego.EnviarAMediosConfParcial.Trim.ToUpper.StartsWith("L") And oSorteo.idEstadoPgmConcurso >= 40)) _
                         Then
                        FrmEnviarMailExtracto.vidPgmConcurso = oPC.idPgmConcurso
                        '''FrmEnviarMailExtracto.vidPgmSorteo = oPC.idPgmSorteoPrincipal ' para verificar si el rector ya esta confirmado
                        FrmEnviarMailExtracto.vidPgmSorteo = oSorteo.idPgmSorteo ' el sorteo de QNL!!!!
                        FrmEnviarMailExtracto.StartPosition = FormStartPosition.CenterParent
                        FrmEnviarMailExtracto.ShowDialog()
                    End If
                Else
                    frmGeneracionExtractoBoldt.voSorteo = New PgmSorteo
                    frmGeneracionExtractoBoldt.voSorteo = oSorteo
                    frmGeneracionExtractoBoldt.vNroSorteo = oSorteo.nroSorteo
                    frmGeneracionExtractoBoldt.vNombreSorteo = tab.SelectedTab.Text
                    frmGeneracionExtractoBoldt.vSoloJurLocal = True
                    frmGeneracionExtractoBoldt.StartPosition = FormStartPosition.CenterParent
                    frmGeneracionExtractoBoldt.ShowDialog()
                End If
            Else
                Me.Cursor = Cursors.Default
                Me.Refresh()
                If _Modo_Operacion = "PC-A" Then
                    If msgRet.Trim() = "" Then
                        'llama a un formulario desde donde imprimir
                        FrmMensajeComparacionExtractos._idpgmconcurso = oSorteo.idPgmConcurso
                        FrmMensajeComparacionExtractos._idpgmsorteo = oSorteo.idPgmSorteo
                        FrmMensajeComparacionExtractos.ShowDialog()
                        Exit Sub
                        '
                        'MsgBox("No hay correspondencia de datos entre ambas cargas. Se imprimirá un listado de diferencias.", MsgBoxStyle.Information, MDIContenedor.Text)
                        ' imprimir reporte diferencias
                        'If Not sBO.GenerarListadoDifCuad(oSorteo.idPgmConcurso, oSorteo.idPgmSorteo, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, destino, path, msj) Then
                        'MsgBox(msj, MsgBoxStyle.Information)
                        'Exit Sub
                        'End If
                    Else
                        MsgBox(msgRet, MsgBoxStyle.Information, MDIContenedor.Text)
                    End If
                End If
            End If
            FileSystemHelper.Log(" ConcursoFinalizar:Fin confirmar SORTEO PROPIO, sorteo:" & oSorteo.idPgmSorteo)
        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar: btnConfirmarExtractoSantafe_click:" & ex.Message)

            Me.Cursor = Cursors.Default
            Me.Refresh()

            MsgBox("Excepción en Confirmar SORTEO PROPIO. " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
            FileSystemHelper.Log(" ConcursoFinalizar:Problema confirmar SORTEO PROPIO, sorteo:" & oSorteo.idPgmSorteo & " " & ex.Message)
        End Try

    End Sub

    Private Sub btnAnularExtractoSantafe_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim sBO As New PgmSorteoBO
        Dim cBO As New PgmConcursoBO
        Dim oSorteo As New PgmSorteo
        Dim fechaHoraCaducidadSorteo As DateTime
        Dim fechaHoraProximoSorteo As DateTime
        Dim fechaHoraInicio As DateTime
        Dim fechaHoraSorteo As DateTime
        Dim fechaHoraFin As DateTime
        Dim tab As TabControl
        Dim DataPickerF As DateTimePicker
        Dim DataPickerH As DateTimePicker

        If Not (MsgBox("Esta acción anulará la confirmación del SORTEO PROPIO DE QLA. Luego podrá volver a confirmarlos. " & vbCrLf & "¿Está seguro que desea anular?", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel, "ATENCION: Se requiere confirmación") = MsgBoxResult.Ok) Then
            Exit Sub
        End If

        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Refresh()

            tab = Me.Controls("grpJuegos").Controls("tabJuegos")
            oSorteo = tab.SelectedTab.Tag

            DataPickerF = getControlJ("grpActual-", "dtpFechaPrescripcion-")
            DataPickerH = getControlJ("grpActual-", "dtpHoraPrescripcion-")
            fechaHoraCaducidadSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

            DataPickerF = getControlJ("grpProximo-", "dtpFechaProximo-")
            DataPickerH = getControlJ("grpProximo-", "dtpHoraProximo-")
            fechaHoraProximoSorteo = FormatDateTime(DataPickerF.Value, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

            DataPickerH = getControlJ("grpActual-", "dtpHoraInicioJuego-")
            fechaHoraInicio = FormatDateTime(oSorteo.fechaHoraIniReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)
            fechaHoraSorteo = FormatDateTime(oSorteo.fechaHora, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

            DataPickerH = getControlJ("grpActual-", "dtpHoraFinJuego-")
            fechaHoraFin = FormatDateTime(oSorteo.fechaHoraFinReal, DateFormat.ShortDate) + " " + FormatDateTime(DataPickerH.Value, DateFormat.LongTime)

            sBO.AnularQuinielaSF(oSorteo, fechaHoraCaducidadSorteo, fechaHoraProximoSorteo, fechaHoraInicio, fechaHoraSorteo, fechaHoraFin)

            ' Obtengo datos actualizados del sorteo y lo asigno a la pesatania y concurso
            oSorteo = sBO.getPgmSorteo(oSorteo.idPgmSorteo)
            For i As Integer = 0 To oPC.PgmSorteos.Count - 1
                If oPC.PgmSorteos(i).idPgmSorteo = oSorteo.idPgmSorteo Then
                    oPC.PgmSorteos.RemoveAt(i)
                    oPC.PgmSorteos.Add(oSorteo)
                End If
            Next
            MarcaExtractoNoCargado(oSorteo.idJuego)

            tab.SelectedTab.Tag = oSorteo
            TabJuegos_Click(sender, e)

            Me.Cursor = Cursors.Default
            Me.Refresh()

            MsgBox("La anulación de la confirmación del SORTEO PROPIO DE QLA. se ha realizado correctamente.", MsgBoxStyle.Information, MDIContenedor.Text)

        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " ConcursoFinalizar: btnAnularExtractoSantafe_click:" & ex.Message)

            Me.Cursor = Cursors.Default
            Me.Refresh()

            MsgBox("Problema al Anular el SORTEO PROPIO. Cierre e intente nuevamente. Si la falla persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Sub dtpicker_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim osorteo As PgmSorteo
        If Not _cargando Then
            osorteo = TabJuegos.SelectedTab.Tag
            _modificado = True
            If noexisteEnlista(osorteo.idJuego) Then
                MarcaJuegoModificado(osorteo.idJuego)
            End If
        End If
    End Sub

    Private Sub texto_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Dim osorteo As PgmSorteo
        If Not _cargando Then
            osorteo = TabJuegos.SelectedTab.Tag
            _modificado = True
            If noexisteEnlista(osorteo.idJuego) Then
                MarcaJuegoModificado(osorteo.idJuego)
            End If
        End If
    End Sub

    Private Function juegoEstaModificado(ByVal idjuego As Integer) As Boolean
        Dim juego As Integer
        Try
            juegoEstaModificado = False
            If Not JuegosModificados Is Nothing Then
                If JuegosModificados.Count > 0 Then
                    For juego = 0 To JuegosModificados.Count - 1
                        If JuegosModificados(juego).Posicion = idjuego And JuegosModificados(juego).Valor = 1 Then
                            juegoEstaModificado = True
                        End If
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Function noexisteEnlista(ByVal idjuego As Integer) As Boolean
        Dim juego As Integer
        Try
            noexisteEnlista = True
            If Not JuegosModificadosAnt Is Nothing Then
                If JuegosModificadosAnt.Count > 0 Then
                    For Each juego In JuegosModificadosAnt
                        If juego = idjuego Then
                            Return False
                            Exit Function
                        End If
                    Next
                End If
            Else
                JuegosModificadosAnt = New ListaOrdenada(Of Integer)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub Borrardelista(ByVal idjuego As Integer)
        Dim juego As Integer
        Dim i As Integer
        Try
            i = 0
            If Not JuegosModificadosAnt Is Nothing Then
                If JuegosModificadosAnt.Count > 0 Then
                    For Each juego In JuegosModificadosAnt
                        If juego = idjuego Then
                            JuegosModificadosAnt(i) = -1
                            JuegosModificadosAnt.Remove(i)
                            Exit Sub
                        End If
                        i = i + 1
                    Next
                End If
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

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
    Private Sub botones_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs)
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
    Private Function Juegoconpremios(ByVal osorteo As PgmSorteo) As Boolean
        Try
            Juegoconpremios = False
            If osorteo.idJuego = 4 Or osorteo.idJuego = 13 Or osorteo.idJuego = 30 Or osorteo.idJuego = 50 Or osorteo.idJuego = 51 Then
                Juegoconpremios = True
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function


    '****
    Private Function getControl(ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Int16

        tab = Me.grpJuegos.Controls("tabJuegos")
        mAux = Split(tab.SelectedTab.Name, "-")
        idJuego = mAux(1)

        nombreCtrl &= idJuego
        Return tab.SelectedTab.Controls(nombreCtrl)
    End Function

    Private Function TieneExtractoCargado(ByVal idjuego As Integer) As Boolean
        Dim juego As cValorPosicion
        Try
            TieneExtractoCargado = False
            If Not JuegosExtractocargados Is Nothing Then
                If JuegosExtractocargados.Count > 0 Then
                    For Each juego In JuegosExtractocargados
                        If juego.Posicion = idjuego Then
                            If juego.Valor <> -1 Then
                                Return True
                                Exit Function
                            End If
                        End If
                    Next
                End If
            Else
                JuegosExtractocargados = New ListaOrdenada(Of cValorPosicion)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub MarcaJuegoModificado(ByVal idjuego As Integer)
        Dim juego As cValorPosicion
        Try

            If Not JuegosModificados Is Nothing Then
                If JuegosModificados.Count > 0 Then
                    For Each juego In JuegosModificados
                        If juego.Posicion = idjuego Then
                            juego.Valor = 1
                            Exit Sub
                        End If
                    Next
                End If
            Else
                JuegosModificados = New ListaOrdenada(Of cValorPosicion)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub MarcaJuegoNoModificado(ByVal idjuego As Integer)
        Dim juego As cValorPosicion
        Try

            If Not JuegosModificados Is Nothing Then
                If JuegosModificados.Count > 0 Then
                    For Each juego In JuegosModificados
                        If juego.Posicion = idjuego Then
                            juego.Valor = -1
                            Exit Sub
                        End If
                    Next
                End If
            Else
                JuegosModificados = New ListaOrdenada(Of cValorPosicion)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    Private Sub MarcaExtractoCargado(ByVal idjuego As Integer)
        Dim juego As cValorPosicion
        Try

            If Not JuegosExtractocargados Is Nothing Then
                If JuegosExtractocargados.Count > 0 Then
                    For Each juego In JuegosExtractocargados
                        If juego.Posicion = idjuego Then
                            juego.Valor = 1
                            Exit Sub
                        End If
                    Next
                End If
            Else
                JuegosExtractocargados = New ListaOrdenada(Of cValorPosicion)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub MarcaExtractoNoCargado(ByVal idjuego As Integer)
        Dim juego As cValorPosicion
        Try

            If Not JuegosExtractocargados Is Nothing Then
                If JuegosExtractocargados.Count > 0 Then
                    For Each juego In JuegosExtractocargados
                        If juego.Posicion = idjuego Then
                            juego.Valor = -1
                            Exit Sub
                        End If
                    Next
                End If
            Else
                JuegosExtractocargados = New ListaOrdenada(Of cValorPosicion)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub TabJuegos_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabJuegos.Click
        Dim oSorteo As New PgmSorteo
        Dim tab As TabControl

        Try
            Me.Cursor = Cursors.WaitCursor
            Me.Refresh()

            vienedepestania = True

            tab = Me.Controls("grpJuegos").Controls("tabJuegos")
            oSorteo = tab.SelectedTab.Tag

            'sale si se hace click en la pestania y el extracto ya fue generado
            If Not TieneExtractoCargado(oSorteo.idJuego) Then
                Me.TabJuegos.Enabled = False
                ActualizarPestaniaJuego(tab)
                MarcaExtractoCargado(oSorteo.idJuego)
                MarcaJuegoNoModificado(oSorteo.idJuego)
                Me.TabJuegos.Enabled = True
            End If

            vienedepestania = False

            Me.Cursor = Cursors.Default
            Me.Refresh()

        Catch ex As Exception
            Me.TabJuegos.Enabled = False
            Me.Cursor = Cursors.Default
            Me.Refresh()
            MsgBox("Tabjuegos_click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub GenerarExtracto()
        Try
            Dim sBO As New PgmSorteoBO
            Dim oJuegoBO As New JuegoBO
            Dim tab As TabControl
            Dim osorteo As PgmSorteo
            Dim nombreGrp As String = ""
            Dim nombrepestania As String = ""
            Dim msjvarios As String = ""
            Dim etiqueta As New Label
            '** obtiene la pestania actual
            tab = Me.Controls("grpJuegos").Controls("tabJuegos")
            osorteo = tab.SelectedTab.Tag

            If Not TieneExtractoCargado(osorteo.idJuego) Then
                nombreGrp = "grpExtracto" & osorteo.idJuego
                nombrepestania = "pstJuego-" & osorteo.idJuego

                '**** control de premios y otras jurisidcciones **************************
                If Juegoconpremios(osorteo) Then
                    If sBO.NoTienePremiosCargados(osorteo.idPgmSorteo, osorteo.idJuego) Then
                        msjvarios = msjvarios & "Faltan cargar premios" & vbCrLf
                    End If
                End If
                If ojuegobo.EsQuiniela(osorteo.idJuego) Then
                    If sBO.OtrasJurisdicciones_SinConfirmar(osorteo.idPgmSorteo) Then
                        msjvarios = msjvarios & "Faltan cargar otras jurisdicciones"
                    End If
                End If

                Try 'si ya esta creado
                    etiqueta = getControlJ("GrpExtracto" & osorteo.idJuego, "lblMensajes")
                    etiqueta.Text = msjvarios
                Catch ex As Exception
                    etiqueta = New Label
                    etiqueta.Name = "lblMensajes" & osorteo.idJuego
                    etiqueta.Text = msjvarios
                    'etiqueta.Font = LetraNegrita
                    etiqueta.ForeColor = Color.Red
                    etiqueta.Location = New System.Drawing.Point(777, 300)
                    etiqueta.Size = New System.Drawing.Size(190, 160)
                    etiqueta.Visible = True
                    Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Controls.Add(etiqueta)
                End Try

                '********************fin control premios y otras jurisdicciones**********

                '**** para actualizar el webbrowser primero hayq ue quitarlo y volverlo a crear
                Dim NombreWebB As String
                Dim wb As WebBrowser
                Dim panelmsj As Panel
                'Dim etiqueta As Label
                NombreWebB = "wbExtracto" & osorteo.idJuego
                '*** quita el control webbrowser de la pestania()
                wb = getControlJ("GrpExtracto" & osorteo.idJuego, NombreWebB)
                Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Controls.Remove(wb)

                '** lo vuelve a crear
                wb = New WebBrowser
                wb.Name = "wbExtracto" & osorteo.idJuego
                wb.Location = New System.Drawing.Point(3, 20)
                wb.Width = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Width - 10
                wb.Height = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Height - 25
                '**obtiene el panel con el msj de error y lo deshabilita
                panelmsj = getControlJ("GrpExtracto" & osorteo.idJuego, "panelmsj")
                panelmsj.Visible = False
                '*** actualiza extracto al confirmar
                Dim url As String = ""
                Try
                    If General.Modo_Operacion <> "PC-B" Then
                        If General.PublicaExtractosWSRestOFF = "S" Then
                            url = sBO.getUrlExtractoOficialRest(osorteo)
                            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto REST: ->" & url & "<- para sorteo:" & osorteo.nroSorteo & " juego:" & osorteo.idJuego)
                        Else
                            url = sBO.getUrlExtractoOficial(osorteo)
                            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto NET: ->" & url & "<- para sorteo:" & osorteo.nroSorteo & " juego:" & osorteo.idJuego)
                        End If

                        Me.Cursor = Cursors.WaitCursor
                        wb.Refresh()
                        wb.Url = New System.Uri(url)
                        wb.Refresh()
                        Me.Cursor = Cursors.Default
                    Else
                        url = ""
                    End If
                    '** lo marca para que no vuelva a generar el PDF
                    MarcaExtractoCargado(osorteo.idJuego)
                Catch ex As Exception
                    'habilita msj de error
                    panelmsj.Visible = True
                End Try
                'agrega el webbrowser actualizado a la  pestania
                Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrepestania).Controls(nombreGrp).Controls.Add(wb)
            End If
        Catch ex As Exception
            MsgBox("Problemas al generar Extracto", MsgBoxStyle.Critical, MDIContenedor.Text)
        End Try
    End Sub
    Private Function ControlarExtractoQuiniBrinco(ByVal osorteo As PgmSorteo) As Boolean
        Try
            Dim oArchivoBoldt As New ArchivoBoldtBO
            Dim pathReportes As String = ""
            ControlarExtractoQuiniBrinco = False
            If osorteo.idJuego <> 4 And osorteo.idJuego <> 13 Then 'solo se controla para quini6 y brinco
                ControlarExtractoQuiniBrinco = True
                Exit Function
            End If
            'si no existe en la tbal de auiditoria,es porque no se realizo el control de extracto
            If Not oArchivoBoldt.existe_auditoria_archivoextracto(osorteo.idPgmSorteo) Then
                pathReportes = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes
                MsgBox("Se procederá a realizar el control con extracto de Boldt.Puede demorar unos minutos.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly, "Control de extracto")
                Me.Cursor = Cursors.WaitCursor
                MDIContenedor.Cursor = Cursors.WaitCursor
                Try
                    If Not oArchivoBoldt.ControlarExtractoQuiniBrinco(osorteo.idJuego, osorteo.nroSorteo, pathReportes) Then
                        Exit Function
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
                    Me.Cursor = Cursors.Default
                    MDIContenedor.Cursor = Cursors.Default
                    Exit Function
                End Try
            End If
            ControlarExtractoQuiniBrinco = True
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MDIContenedor.Cursor = Cursors.Default
            MsgBox("Problemas al controlar extracto:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Function

    Private Function getUrlExtracto(ByRef oSorteo As PgmSorteo, ByRef esExtractoLocal As Boolean) As String
        Dim url As String = ""
        Dim sBO As New PgmSorteoBO

        esExtractoLocal = True
        If General.Modo_Operacion <> "PC-B" Then
            If General.PublicarWebOFF = "S" Or General.PublicarWebON = "S" Then
                Try
                    If General.PublicaExtractosWSRestOFF = "S" Then
                        url = sBO.getUrlExtractoOficialRest(oSorteo)
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto REST: ->" & url & "<- para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    Else
                        url = sBO.getUrlExtractoOficial(oSorteo)
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto NET: ->" & url & "<- para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                    End If
                    esExtractoLocal = False
                Catch ex As Exception ' si falla url continuara valiendo "" y se intentara a continuacion el local
                End Try
            Else
                If General.PublicaExtractosWSRestOFF = "S" Then
                    Try
                        url = sBO.getUrlExtractoOficialRest(oSorteo)
                        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " setControles:Extracto remoto REST: ->" & url & "<- para sorteo:" & oSorteo.nroSorteo & " juego:" & oSorteo.idJuego)
                        esExtractoLocal = False
                    Catch ex As Exception
                    End Try
                End If
            End If
            If url = "" Then
                Try
                    Dim ds As New ExtractoReporte
                    esExtractoLocal = True

                    ds.GenerarExtractoLocal(oSorteo.idPgmSorteo, url, General.PathInformes)

                Catch ex As Exception
                End Try
            End If
        End If
        Return url
    End Function

    Private Sub ActualizarPestaniaJuego(ByRef tab As TabControl)
        Dim sBO As New PgmSorteoBO
        Dim oSorteo As New PgmSorteo
        Dim oJuegoBO As New JuegoBO
        Dim url As String = ""
        Dim esExtractoLocal As Boolean = True

        Dim nombrePestania As String
        Dim nombreGrp As String
        Dim nombreWebB As String
        Dim nombrePanelMsj As String
        Dim nombreLblSinExtracto As String
        Dim nombreLbllocal As String
        Dim nombreLblMensajes As String
        Dim nombreBtnConfirmar As String
        Dim nombreBtnConfirmarStafe As String
        Dim nombreBtnAnularStafe As String
        Dim nombreBtnGuardarCambios As String
        Dim nombreGrpActual As String
        Dim nombreGrpProximo As String

        Dim webB As WebBrowser = New WebBrowser
        Dim panelMsj As Panel
        Dim LblSinExtracto As Label
        Dim lbllocal As Label
        Dim lblMensajes As Label
        Dim botonConfirmar As Button
        Dim botonConfirmarStafe As Button
        Dim botonAnularStafe As Button
        Dim botonGuardarCambios As Button
        Dim grpActual As GroupBox
        Dim grpProximo As GroupBox
        Dim dtpHoraInicioJuego As DateTimePicker
        Dim dtpHoraFinJuego As DateTimePicker
        Dim txtPozo As TextBox

        ' Primero actualizo los datos de cabecera
        ActualizarCabeceraPestaniaJuego(tab)

        'tab = Me.Controls("grpJuegos").Controls("tabJuegos")
        oSorteo = tab.SelectedTab.Tag

        nombrePestania = "pstJuego-" & oSorteo.idJuego
        nombreGrp = "grpExtracto-" & oSorteo.idJuego
        nombreWebB = "wbExtracto-" & oSorteo.idJuego
        nombrePanelMsj = "panelmsj-" & oSorteo.idJuego
        nombreLblSinExtracto = "lblSinExtracto-" & oSorteo.idJuego
        nombreLbllocal = "lbllocal-" & oSorteo.idJuego
        nombreLblMensajes = "lblMensajes-" & oSorteo.idJuego
        nombreBtnGuardarCambios = "btnGuardarCambios-" & oSorteo.idJuego
        nombreBtnConfirmar = "btnConfirmarExtracto-" & oSorteo.idJuego
        nombreBtnConfirmarStafe = "btnConfirmarLoteria-" & oSorteo.idJuego
        nombreBtnAnularStafe = "btnAnularLoteria-" & oSorteo.idJuego
        nombreGrpActual = "grpActual-" & oSorteo.idJuego
        nombreGrpProximo = "grpProximo-" & oSorteo.idJuego

        dtpHoraInicioJuego = getControlJ("grpActual-", "dtpHoraInicioJuego-")
        dtpHoraFinJuego = getControlJ("grpActual-", "dtpHoraFinJuego-")
        txtPozo = getControlJ("grpProximo-", "txtPozo-")

        webB = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreGrp).Controls(nombreWebB)
        panelMsj = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreGrp).Controls(nombrePanelMsj)
        LblSinExtracto = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreGrp).Controls(nombrePanelMsj).Controls(nombreLblSinExtracto)
        lbllocal = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreLbllocal)
        lblMensajes = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreLblMensajes)
        botonGuardarCambios = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreBtnGuardarCambios)
        botonConfirmar = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreBtnConfirmar)
        botonConfirmarStafe = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreBtnConfirmarStafe)
        botonAnularStafe = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreBtnAnularStafe)
        grpActual = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreGrpActual)
        grpProximo = Me.Controls("grpJuegos").Controls("tabJuegos").Controls(nombrePestania).Controls(nombreGrpProximo)

        ' Visibilidad
        txtPozo.Visible = False
        panelMsj.Visible = False
        LblSinExtracto.Visible = False
        webB.Visible = False
        botonConfirmarStafe.Visible = False
        botonAnularStafe.Visible = False
        lbllocal.Visible = False
        lblMensajes.Visible = False

        ' Acceso
        dtpHoraInicioJuego.Enabled = False
        dtpHoraFinJuego.Enabled = False
        txtPozo.Enabled = False
        botonGuardarCambios.Enabled = False
        botonConfirmar.Enabled = False
        botonConfirmarStafe.Enabled = False
        botonAnularStafe.Enabled = False
        grpActual.Enabled = False
        grpProximo.Enabled = False

        ' Pozo Estimado Proximo Sorteo
        If oSorteo.pozos.Count <> 0 Then
            If Not (oSorteo.idJuego = 50 Or oSorteo.idJuego = 51) Then
                txtPozo.Visible = True
                ''If oSorteo.idEstadoPgmConcurso = 40 Then  ' Asterisqueado porque el pozo no se modifica en esta pantalla
                ''    txtPozo.Enabled = True
                ''End If
            End If
        End If

        ' Boton guardar cambios
        If oSorteo.idEstadoPgmConcurso = 40 Then
            grpActual.Visible = True
            If oSorteo.idPgmSorteo = oPC.idPgmSorteoPrincipal Then ' si es juego rector habilito modificacion de hora real
                dtpHoraInicioJuego.Enabled = True
                dtpHoraFinJuego.Enabled = True
            End If
            grpActual.Enabled = True
            grpProximo.Enabled = True
            botonGuardarCambios.Enabled = True
        End If

        ' El resto depende de si se puede obtener el extracto
        If Juegoconpremios(oSorteo) And _
            sBO.NoTienePremiosCargados(oSorteo.idPgmSorteo, oSorteo.idJuego) Then
            LblSinExtracto.Text = "El Extracto Oficial no se visualiza porque faltan cargar Premios."
            LblSinExtracto.Visible = True
            panelMsj.Visible = True
        Else ' es un juego sin premios o es con premios y ya los tiene cargados
            url = getUrlExtracto(oSorteo, esExtractoLocal)
            If General.Modo_Operacion = "PC-B" Then
                url = ""
            End If
            If url <> "" Then
                Me.Cursor = Cursors.WaitCursor
                webB.Url = New System.Uri(url)
                webB.Refresh()
                webB.Visible = True
                Me.Cursor = Cursors.Default
                If oSorteo.idEstadoPgmConcurso = 40 Then
                    If esExtractoLocal Then
                        If General.PublicarWebOFF = "N" And General.PublicarWebON = "N" And General.PublicaExtractosWSRestOFF = "N" And General.PublicaExtractosWSRestON = "N" Then
                            botonConfirmar.Enabled = True
                        End If
                        ' Boton Confirmar / Anular Jurisdiccion
                        If ojuegobo.EsQuiniela(oSorteo.idJuego) And _
                            General.PublicarWebOFF = "N" And General.PublicarWebON = "N" And General.PublicaExtractosWSRestOFF = "N" And General.PublicaExtractosWSRestON = "N" And (General.Modo_Operacion.ToUpper() <> "PC-B") Then
                            If (Not oSorteo.ConfirmadoParcial) Then
                                botonConfirmarStafe.Visible = True
                                botonConfirmarStafe.Enabled = True
                            Else
                                botonAnularStafe.Visible = True
                                botonAnularStafe.Enabled = True
                            End If
                        End If
                        lbllocal.Visible = True
                    Else ' no es local
                        If General.PublicarWebOFF = "S" Or General.PublicarWebON = "S" Or General.PublicaExtractosWSRestOFF = "S" Or General.PublicaExtractosWSRestON = "S" Then
                            If oJuegoBO.esQuiniela(oSorteo.idJuego) Then
                                If sBO.OtrasJurisdicciones_SinConfirmar(oSorteo.idPgmSorteo) Then
                                    lblMensajes.Text = "Faltan cargar otras jurisdicciones"
                                    lblMensajes.Visible = True
                                Else
                                    botonConfirmar.Enabled = True
                                End If
                            Else
                                botonConfirmar.Enabled = True
                            End If
                        End If
                        ' Boton Confirmar / Anular Jurisdiccion
                        If oJuegoBO.esQuiniela(oSorteo.idJuego) And _
                            (General.PublicarWebOFF = "S" Or General.PublicarWebON = "S" Or General.PublicaExtractosWSRestOFF = "S" Or General.PublicaExtractosWSRestON = "S") And (General.Modo_Operacion.ToUpper() <> "PC-B") Then
                            If (Not oSorteo.ConfirmadoParcial) Then
                                botonConfirmarStafe.Visible = True
                                botonConfirmarStafe.Enabled = True
                            Else
                                botonAnularStafe.Visible = True
                                botonAnularStafe.Enabled = True
                            End If
                        End If
                    End If
                End If ' est = 40
            Else ' url = ""
                'LblSinExtracto.Text = "Problemas al generar el Extracto Oficial. Revise los datos del sorteo, re-publique a web y vuelva a intentar."
                If General.Modo_Operacion = "PC-B" Then
                    If oJuegoBO.esQuiniela(oSorteo.idJuego)  Then
                        If (Not oSorteo.ConfirmadoParcial) Then
                            botonConfirmarStafe.Visible = True
                            botonConfirmarStafe.Enabled = True
                        Else
                            botonAnularStafe.Visible = True
                            botonAnularStafe.Enabled = True
                        End If
                    End If
                    botonConfirmar.Enabled = True
                    LblSinExtracto.Text = "Presione el botón CONFIRMAR para generar el archivo de control."
                Else
                    LblSinExtracto.Text = "Problemas al generar el Extracto Oficial. Revise los datos del sorteo, re-publique a web y vuelva a intentar."
                End If
                LblSinExtracto.Visible = True
                panelMsj.Visible = True
            End If
        End If
        'botonConfirmarStafe
        ''If General.Modo_Operacion.ToUpper() <> "PC-B" Then
        ''    botonConfirmarStafe.Visible = True
        ''    botonConfirmarStafe.Enabled = True
        ''Else
        ''    botonConfirmarStafe.Visible = False
        ''    botonConfirmarStafe.Enabled = False
        ''End If
        grpActual.Visible = True
        grpProximo.Visible = True

    End Sub
    ' hugo 14/10/2015
    Private Function esQuinioBrincooPoceada(ByVal osorteo As PgmSorteo) As Boolean
        Try
            esQuinioBrincooPoceada = False
            If osorteo.idJuego = 4 Or osorteo.idJuego = 13 Or osorteo.idJuego = 30 Then
                esQuinioBrincooPoceada = True
            End If

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Function

    Private Sub btnAnteriores_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAnteriores.Click
        setValorListaConcurso(False)
    End Sub
End Class