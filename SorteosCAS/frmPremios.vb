Imports sorteos.helpers
Imports libEntities.Entities
Imports sorteos.bussiness
Imports System.IO
Imports sorteos.data
Imports sorteos.helpers.General
Imports System.Security.Cryptography
Imports Sorteos.Extractos



Public Class frmPremios
#Region "PROPIEDADES"
    Private _inicio As Boolean
    Private _oPC As PgmConcurso
    Private oPst405 As TabPage = Nothing
    Private oPst1305 As TabPage = Nothing
    Private cabezaLoteria As Long = -1
    'variable para saber si tiene que guardar los premiosen el load
    Dim guardaPremios As Boolean = False
    Dim _cargando As Boolean = True
    Dim _HuboCambiosPoceada As Boolean = False
    Dim _HuboCambiosQuini As Boolean = False
    Dim _HuboCambiosBrinco As Boolean = False
    Dim _HuboCambiosLoteria As Boolean = False
    Dim _HuboCambiosLoteriaChica As Boolean = False

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
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        inicio = True
    End Sub

    Private Sub BtnBuscarConcurso_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnBuscarConcurso.Click
        FileSystemHelper.Log("frmPremios:Buscar concurso en frm premios")
        setValorListaConcurso()
    End Sub

    Private Sub frmPremios_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If _HuboCambiosPoceada Or _HuboCambiosBrinco Or _HuboCambiosQuini Or _HuboCambiosLoteria Or _HuboCambiosLoteriaChica Then
            If MsgBox("Se han detectados cambios. ¿Desea guardar los cambios?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, MDIContenedor.Text) = MsgBoxResult.Yes Then
                Try
                    'se guardan los premios del juego correspondiente
                    If _HuboCambiosPoceada Then
                        premioGuardar(30, True)
                    End If
                    If _HuboCambiosLoteria Then
                        premioGuardar(50, True)
                    End If
                    If _HuboCambiosLoteriaChica Then
                        premioGuardar(51, True)
                    End If
                    If _HuboCambiosBrinco Then
                        premioGuardar(13, True)

                    End If
                    If _HuboCambiosQuini Then
                        premioGuardar(4, True)
                    End If

                Catch ex As Exception
                    MsgBox("Hubo Problemas al guardar los premios :" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
                End Try
            End If
        End If
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub

    Private Sub frmPremios_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim d As DateTime = New Date(Date.Now.Ticks)
        Me.Location = New System.Drawing.Point(0, 0)
        dtpFechaConcurso.Value = d.ToString("dd/MM/yyyy")
        dtpFechaConcurso.MinDate = String.Format("{0:dd/MM/yyyy}", d.AddDays(General.DiasSorteosAnteriores * -1).ToShortDateString)
        dtpFechaConcurso.MaxDate = String.Format("{0:dd/MM/yyyy}", d.AddDays(30).ToShortDateString)
        dtpHoraConcurso.Value = d.ToString("dd/MM/yyyy HH:mm:ss")
        dtpHoraConcurso.MinDate = dtpFechaConcurso.MinDate
        dtpHoraConcurso.MaxDate = dtpFechaConcurso.MaxDate

        'setValorListaConcurso()
        'If oPC IsNot Nothing Then
        'setControlesValoresPremios(oPC)
        'End If

        inicio = False
    End Sub

    Private Sub CboConcurso_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboConcurso.SelectedIndexChanged
        If Not inicio Then
            oPC = CboConcurso.SelectedItem
            setControlesValoresPremios(CboConcurso.SelectedItem)
        End If
    End Sub

    Private Sub btnPremioObtener4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioObtener4.Click
        premioObtener(4, True)
    End Sub

    Private Sub btnPremioGuardar4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioGuardar4.Click
        premioGuardar(4)
    End Sub

    Private Sub btnPremioObtener13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioObtener13.Click
        premioObtener(13, True)
    End Sub

    Private Sub btnPremioGuardar13_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioGuardar13.Click
        premioGuardar(13)
    End Sub

    Private Sub btnPremioObtener30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioObtener30.Click
        premioObtener(30, True)
    End Sub

    Private Sub btnPremioGuardar30_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioGuardar30.Click
        premioGuardar(30)
    End Sub

    Private Sub btnPremioObtener50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioObtener50.Click
        premioObtener(50, True)
    End Sub

    Private Sub btnPremioGuardar50_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioGuardar50.Click
        premioGuardar(50)
    End Sub

    Private Sub btnPremioObtener51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioObtener51.Click
        premioObtener(51)
    End Sub

    Private Sub btnPremioGuardar51_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremioGuardar51.Click
        premioGuardar(51)
    End Sub
#End Region

#Region "FUNCIONES"
    ' ********************************************************************************************
    ' *************************** FUNCIONES ******************************************************
    ' ********************************************************************************************
    ' Localica el/los concursos candidatos según fecha y hora ingresados
    Private Sub setValorListaConcurso()
        Dim listaPC As ListaOrdenada(Of PgmConcurso)
        'Dim listaPC As List(Of PgmConcurso)
        Dim fechaHora As DateTime
        Dim boPgmConcurso As New PgmConcursoBO
        Dim agregaItem As Boolean

        Try
            fechaHora = dtpFechaConcurso.Value
            fechaHora = fechaHora.AddHours(-1 * dtpFechaConcurso.Value.Hour).AddMinutes(-1 * dtpFechaConcurso.Value.Minute) '.AddSeconds(-1 * dtpFechaConcurso.Value.Second)
            If Trim(fechaHora) = "" Then
                MsgBox("Ingrese una fecha para el sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            If Trim(dtpHoraConcurso.Value) = "" Then
                MsgBox("Ingrese una hora para el sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            fechaHora = fechaHora.AddHours(dtpHoraConcurso.Value.Hour).AddMinutes(dtpHoraConcurso.Value.Minute) '.AddSeconds(dtpHoraConcurso.Value.Second)
            ' consulta y carga la lista y carga
            'listaPC = boPgmConcurso.getPgmConcurso(fechaHora)

            listaPC = boPgmConcurso.getPgmConcursoFinalizadoConPremios(fechaHora)

            CboConcurso.Items.Clear()
            CboConcurso.ValueMember = "idPgmConcurso"
            CboConcurso.DisplayMember = "nombre"

            For Each oPC2 In listaPC
                For Each oSorteo In oPC2.PgmSorteos
                    '**21/03/2012  la loteria(50) y loteria chica(51) no cargan pozos pero si cargan premios
                    If oSorteo.pozos.Count <> 0 Or oSorteo.idJuego = 50 Or oSorteo.idJuego = 51 Then
                        agregaItem = True
                    End If
                Next

                If agregaItem Then CboConcurso.Items.Add(oPC2)
            Next

            If CboConcurso.Items.Count = 0 Then
                MsgBox("No hay concursos en condiciones para cargar premios.", MsgBoxStyle.Information, MDIContenedor.Text)
                dtpFechaConcurso.Focus()
                Exit Sub
            Else
                CboConcurso.SelectedItem = CboConcurso.Items(0)
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub setControlesValoresPremios(ByVal oPgmConc As PgmConcurso)
        Dim oPCBO As New PgmConcursoBO

        'se crean los controles 
        setControles()
        setValores()
    End Sub

    Private Sub setControles()
        Dim boSorteo As New JuegoBO
        Dim idJuego As Integer

        Dim Tab As TabControl
        Dim Pestaña As TabPage
        Dim Etiqueta As Label
        Dim Text As TextBox
        Dim DataPicker As DateTimePicker
        Dim panelPozo As Control
        Dim letra As Font
        ' crea tabJuegos y lo inserta en el formulario
        letra = New Font("Myriad Web Pro", 10, FontStyle.Regular)
        Tab = Me.grpJuegos.Controls("tabJuegos")
        If Not IsNothing(Tab) Then
            For Each TabControl In Tab.TabPages
                Tab.SelectTab(TabControl)
                idJuego = getIdJuegoActual()

                panelPozo = TabControl.Controls("pnlPozoJuego" & idJuego)
                If Not IsNothing(panelPozo) Then
                    panelPozo.Visible = False
                    grpJuegos.Controls.Add(panelPozo)
                End If
            Next

            Me.grpJuegos.Controls.Remove(Tab)
        End If

        Tab = New TabControl
        Tab.Name = "tabJuegos"
        Tab.Font = letra
        Tab.Location = New System.Drawing.Point(6, 19)

        Tab.Size = New System.Drawing.Size(625, 410)
        Me.grpJuegos.Controls.Add(Tab)

        ' agrega las pestañas al tabJuegos
        For Each oSorteo In oPC.PgmSorteos
            idJuego = oSorteo.idJuego

            'la loteria(50) y loteria chica(51) no cargan pozos pero si cargan premios
            If oSorteo.pozos.Count <> 0 Or idJuego = 50 Or idJuego = 51 Then
                ' pestaña
                Pestaña = New TabPage
                Pestaña.BackColor = Color.FromArgb(239, 239, 239)
                Pestaña.Name = "pstJuego-" & idJuego
                'Pestaña.Height = 230
                Pestaña.Text = "" & boSorteo.getJuegoDescripcion(idJuego)
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
                Etiqueta.Location = New System.Drawing.Point(45, 15)
                Etiqueta.Size = New System.Drawing.Size(55, 15)
                Pestaña.Controls.Add(Etiqueta)

                Text = New TextBox
                Text.Name = "txtSorteoJuego" & idJuego
                Text.Location = New System.Drawing.Point(100, 11)
                Text.Size = New System.Drawing.Size(62, 20)
                Text.Enabled = False
                Pestaña.Controls.Add(Text)

                Etiqueta = New Label
                Etiqueta.Name = "lblFechaPrescribeJuego" & idJuego
                Etiqueta.Text = "Prescribe el:"
                Etiqueta.Location = New System.Drawing.Point(16, 41)
                Etiqueta.Size = New System.Drawing.Size(84, 15)
                Pestaña.Controls.Add(Etiqueta)

                DataPicker = New DateTimePicker
                DataPicker.Name = "dtpFechaPrescribeJuego" & idJuego
                DataPicker.Location = New System.Drawing.Point(100, 38)
                DataPicker.Size = New System.Drawing.Size(248, 20)
                DataPicker.Format = DateTimePickerFormat.Long
                DataPicker.CustomFormat = "dd/MM/yyyy"
                DataPicker.ShowUpDown = True
                DataPicker.Enabled = False
                Pestaña.Controls.Add(DataPicker)

                Etiqueta = New Label
                Etiqueta.Name = "lblHoraPrescribeJuego" & idJuego
                Etiqueta.Text = "Hora:"
                Etiqueta.Location = New System.Drawing.Point(354, 41)
                Etiqueta.Size = New System.Drawing.Size(44, 15)
                Pestaña.Controls.Add(Etiqueta)

                DataPicker = New DateTimePicker
                DataPicker.Name = "dtpHoraPrescribeJuego" & idJuego
                DataPicker.Location = New System.Drawing.Point(400, 37)
                DataPicker.Size = New System.Drawing.Size(55, 20)
                DataPicker.Format = DateTimePickerFormat.Custom
                DataPicker.CustomFormat = "HH:mm"
                DataPicker.ShowUpDown = True
                DataPicker.Enabled = False
                Pestaña.Controls.Add(DataPicker)

                ' si existe un panel con pozos para el juego lo inserta en la pestaña
                panelPozo = grpJuegos.Controls("pnlPozoJuego" & idJuego)
                panelPozo.Visible = True
                panelPozo.Location = New System.Drawing.Point(8, 65)
                Pestaña.Controls.Add(panelPozo)


                ' remueve las pestañas de sorteos adicionales
                If idJuego = 4 Then ' quini 6
                    If oPC.concurso.IdConcurso = 18 Then
                        'If Mid(CboConcurso.Text, 1, 2) = 18 Then ' tiene sorteo adicional
                        If Not IsNothing(oPst405) Then
                            tabPozoJuego4.TabPages.Add(oPst405)
                            oPst405 = Nothing
                        End If
                    Else ' no tiene sorteo adicional
                        If IsNothing(oPst405) Then
                            oPst405 = tabPozoJuego4.TabPages("pst405")
                            If tabPozoJuego4.TabPages.Count = 6 Then
                                Me.Controls.Remove(tabPozoJuego4.TabPages("pst405"))
                                tabPozoJuego4.TabPages("pst405").Dispose()
                            End If
                        End If
                    End If
                End If

                If idJuego = 13 Then ' brinco
                    If oPC.concurso.IdConcurso = 19 Then
                        'If Mid(CboConcurso.Text, 1, 2) = 19 Then ' tiene sorteo adicional
                        If Not IsNothing(oPst1305) Then
                            tabPozoJuego13.TabPages.Add(oPst1305)

                            oPst1305 = Nothing
                        End If
                        '25-04-19HG si el sorteo es mayor a 1000,nomuestra la pestaña de sueldos
                        If oSorteo.nroSorteo >= 1000 Then


                            If tabPozoJuego13.TabPages.Count = 3 Then
                                Me.Controls.Remove(tabPozoJuego13.TabPages("pst1306"))
                                tabPozoJuego13.TabPages("pst1306").Dispose()
                            End If
                        End If
                        '25-04-19
                    Else ' no tiene sorteo adicional
                        oPst1305 = tabPozoJuego13.TabPages("pst1305")
                        If tabPozoJuego13.TabPages.Count = 3 Then
                            Me.Controls.Remove(tabPozoJuego13.TabPages("pst1305"))
                            tabPozoJuego13.TabPages("pst1305").Dispose()
                        End If
                    End If
                End If
                '***25/09/2013 HG se quita la pestañana de extraccion y progresion para el juego loteria IAFAS
                Try
                    If idJuego = 50 And General.Jurisdiccion = "E" Then
                        ''pestaña extracciones
                        oPst1305 = tabPozoJuego50.TabPages("pst5006")
                        Me.Controls.Remove(tabPozoJuego50.TabPages("pst5006"))
                        tabPozoJuego50.TabPages("pst5006").Dispose()
                        'pestaña progresion
                        oPst1305 = tabPozoJuego50.TabPages("pst5007")
                        Me.Controls.Remove(tabPozoJuego50.TabPages("pst5007"))
                        tabPozoJuego50.TabPages("pst5007").Dispose()
                    End If
                Catch ex As Exception
                End Try
                '***fin 25/09/2013 se quita la pestañana de extraccion y progresion para el juego loteria IAFAS


            End If
        Next
    End Sub

    ' presenta en el formulario los valores de la entidad oPC
    Private Sub setValores()
        Dim tab As TabControl
        Dim DataPicker As DateTimePicker
        Dim habilitarControl As Boolean
        Dim panelPozo As Control
        Dim sBO As New PgmSorteoBO

        tab = Me.grpJuegos.Controls("tabJuegos")

        ' valores de las pestañas tabJuegos
        For Each oSorteo In oPC.PgmSorteos
            If oSorteo.pozos.Count <> 0 Or oSorteo.idJuego = 50 Or oSorteo.idJuego = 51 Then
                If oSorteo.idJuego = 50 Then
                    'cabezaLoteria = Sorteo
                End If
                ' localiza y hace activa la pestaña
                tab.SelectTab("pstJuego-" & oSorteo.idJuego)

                ' ** renglón 0 
                getControlJ("txtIdPgmSorteo").Text = oSorteo.idPgmSorteo

                ' ** renglón 1
                getControlJ("txtSorteoJuego").Text = oSorteo.nroSorteo
                getControlJ("txtSorteoJuego").Enabled = habilitarControl


                ' ** renglón 2
                DataPicker = getControlJ("dtpFechaPrescribeJuego")
                DataPicker.Value = oSorteo.fechaHoraPrescripcion

                DataPicker = getControlJ("dtpHoraPrescribeJuego")
                DataPicker.Value = oSorteo.fechaHoraPrescripcion

                ' ** pozos
                panelPozo = tab.SelectedTab.Controls("pnlPozoJuego" & oSorteo.idJuego)
                If Not IsNothing(panelPozo) Then
                    premioObtener(oSorteo.idJuego)
                    ' Obtengo datos actualizados del sorteo y lo asigno a la pesatania y concurso
                    oSorteo = sBO.getPgmSorteo(oSorteo.idPgmSorteo)
                    For Each o In oPC.PgmSorteos
                        If o.idPgmSorteo = oSorteo.idPgmSorteo Then
                            o = oSorteo
                        End If
                    Next
                    ''For i As Integer = 0 To oPC.PgmSorteos.Count - 1
                    ''    If oPC.PgmSorteos(i).idPgmSorteo = oSorteo.idPgmSorteo Then
                    ''        oPC.PgmSorteos.RemoveAt(i)
                    ''        oPC.PgmSorteos.Add(oSorteo)
                    ''    End If
                    ''Next
                    'si el juego esta confirmado,no se muestran los botones de obtener y guardar premios
                    If oSorteo.idEstadoPgmConcurso = 50 Then
                        panelPozo.Controls("btnPremioObtener" & oSorteo.idJuego).Enabled = False
                        panelPozo.Controls("btnPremioGuardar" & oSorteo.idJuego).Enabled = False
                    End If
                    '26/09/2013 HG si es loteria de iafas no muestra el 2do campo de los 2 y 3 premios de aproximacion
                    Try
                        If oSorteo.idJuego = 50 And General.Jurisdiccion = "E" Then
                            getControlJP("pst5004", "txtPrem5004002").Text = 0 'asigna valor para que no de error de validacion
                            getControlJP("pst5004", "txtPrem5004002").Visible = False
                            getControlJP("pst5004", "lblPrem5004002").Visible = False
                            getControlJP("pst5005", "txtPrem5005002").Text = 0 'asigna valor para que no de error de validacion
                            getControlJP("pst5005", "txtPrem5005002").Visible = False
                            getControlJP("pst5005", "lblPrem5005002").Visible = False
                        End If
                    Catch ex As Exception

                    End Try
                End If
            End If
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

    ' retorna el control solicitado del tabPage juego actual
    Private Function getControlJ(ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Int16
        Try
            tab = Me.grpJuegos.Controls("tabJuegos")
            mAux = Split(tab.SelectedTab.Name, "-")
            idJuego = mAux(1)

            nombreCtrl &= idJuego
            Return tab.SelectedTab.Controls(nombreCtrl)
        Catch ex As Exception

        End Try
    End Function

    ' retorna el control solicitado del tabPage pozo (inlcuido en tab Juego) actual
    Private Function getControlJP(ByVal pst As String, ByVal nombreCtrl As String) As Control
        Dim tab As TabControl
        Dim mAux As Array
        Dim idJuego As Integer
        Try

            tab = Me.grpJuegos.Controls("tabJuegos")
            mAux = Split(tab.SelectedTab.Name, "-")
            idJuego = mAux(1)

            tab = tab.SelectedTab.Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
            If (IsNothing(tab.TabPages(pst))) Then
                Return Nothing
            Else
                Return tab.TabPages(pst).Controls(nombreCtrl)
            End If
        Catch ex As Exception

        End Try

    End Function

    Public Sub setPozo()
        Dim boPremio As New PremioBO
        Dim lista As List(Of Premio)
        Dim modalidad As String
        Dim _juego As Integer
        Dim _Habilita As Boolean
        Dim _Aciertos As String = ""
        Dim _Habilita_2Premio_Quini6 As Boolean
        Dim premioporapuesta As Double = -1
        'Dim cantGanadores As Long = 0

        _juego = Mid("00", 1, 2 - Len(getIdJuegoActual())) & getIdJuegoActual()
        lista = boPremio.getPremio(getIdJuegoActual(), getNroSorteoActual())
        'Nota:Los campos correspondiente al estímulo de PF se ponen invisible,no se quitan por si mas adelante deciden mostrarse.
        For Each oPremio In lista
            modalidad = IIf(Len(CStr(oPremio.idPremio)) = 7, Mid(oPremio.idPremio, 1, 4), Mid(oPremio.idPremio, 1, 3))
            Try
                If oPremio.idPremio = 1301006 Then 'presenta los sueldos
                    Dim boSueldo As New SueldoBO
                    Dim lPremio As New List(Of Sueldo)
                    Dim oSueldo As Sueldo
                    Dim i As Integer

                    lPremio = boSueldo.getSueldo(13, oPremio.idPgmsorteo)
                    i = 0
                    For Each oSueldo In lPremio
                        i = i + 1
                        getControlJP("pst1306", "txtAge130600" & i).Text = oSueldo.agencia
                        getControlJP("pst1306", "txtLoc130600" & i).Text = oSueldo.localidad
                        getControlJP("pst1306", "txtPcia130600" & i).Text = oSueldo.provincia
                        getControlJP("pst1306", "txtTick130600" & i).Text = oSueldo.cupon
                    Next

                Else
                    '** si el campo aciertos por defecto era nulo,viene un 99
                    If oPremio.AciertosPorDef = 99 Then
                        _Aciertos = ""
                    Else
                        _Aciertos = oPremio.AciertosPorDef

                    End If
                    If oPremio.RequiereAciertos = False Then
                        _Habilita = False
                    Else
                        _Habilita = True
                    End If

                    If _juego = 50 Or _juego = 51 Then
                        getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text = oPremio.importePremio
                        If oPremio.idPremio = 5007001 And General.Jurisdiccion = "S" Then 'la progresion se guarda como cantganadores
                            getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Text = oPremio.cantGanadores
                        End If
                        getControlJP("pst" & modalidad, "lblPrem" & oPremio.idPremio).Text = oPremio.NombrePremio
                    Else

                        'para quini 6 y  brinco comun
                        If _juego = 4 Or _juego = 13 Then

                            If _juego = 4 Then
                                If oPremio.idPremio = 405002 Then
                                    If oPremio.cantGanadores = 0 And oPremio.importePremio = 0 Then
                                        _Habilita_2Premio_Quini6 = False
                                    Else
                                        _Habilita_2Premio_Quini6 = True
                                    End If
                                    getControlJP("pst" & modalidad, "txtAciertos" & oPremio.idPremio).Enabled = _Habilita_2Premio_Quini6
                                    getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Enabled = _Habilita_2Premio_Quini6
                                    getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Enabled = _Habilita_2Premio_Quini6
                                    getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Enabled = _Habilita_2Premio_Quini6
                                End If
                            End If
                            getControlJP("pst" & modalidad, "txtAciertos" & oPremio.idPremio).Text = _Aciertos
                            getControlJP("pst" & modalidad, "txtAciertos" & oPremio.idPremio).Enabled = _Habilita

                        End If
                        'si no hubo ganadores se muestra cero en lugar del importe del premio
                        premioporapuesta = oPremio.importePremio

                        If _juego = 13 Or _juego = 30 Then
                            If oPremio.cantGanadores = 0 Then
                                premioporapuesta = 0
                            End If
                        End If

                        'If oPremio.idPremio = 5007001 Then ' progresion -> lo calculo automaticamente si esta vacio

                        'Else
                        '    cantGanadores = oPremio.cantGanadores
                        'End If
                        getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text = oPremio.importePozo
                        getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Text = oPremio.cantGanadores
                        getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text = premioporapuesta
                        getControlJP("pst" & modalidad, "lblPrem" & oPremio.idPremio).Text = oPremio.NombrePremio.ToUpper

                        'End If
                    End If
                End If

            Catch ex As Exception

            End Try
        Next
    End Sub

    Public Sub premioObtener(ByVal idJuego As Int16, Optional ByVal ForzarArchivo As Boolean = False)
        Dim cargardesdearchivo As Boolean = True
        Dim arbo As New ArchivoBoldtBO
        Dim sorteoAct
        guardaPremios = False
        ' 1. Busco el extracto, si corresponde
        If idJuego = 4 Or idJuego = 13 Then
            If General.Obtener_pgmsorteos_ws = "S" Then
                sorteoAct = Mid("00000", 1, 5 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text
                arbo.Generar_archivosExtracto_y_Control_por_WS(idJuego * 1000000 + sorteoAct)
            End If
            setExtractoDesdeArchivoZIP(idJuego)
        End If

        If General.Obtener_pgmsorteos_ws = "S" Then
            If setPremioDesdeWS(idJuego, ForzarArchivo) Then
                cargardesdearchivo = False
                setPozo()

            End If
        End If
        If cargardesdearchivo Then
            ' 2. Cargo Premios
            FileSystemHelper.Log(" frmPremios:ejecuta setPremioDesdeArchivoZIP, para el juego:" & idJuego)
            setPremioDesdeArchivoZIP(idJuego, ForzarArchivo)
            ' 3. Cargo Premios Sueldo, si corresponde
            If idJuego = 13 Then 'premios sueldos Brinco
                FileSystemHelper.Log(" frmPremios:ejecuta setPremioSueldosDesdeArchivoZIP, para el juego:" & idJuego)
                setPremioSueldosDesdeArchivoZIP(ForzarArchivo)
            End If
        End If

        ' 4. Actualizo Progresion, si corresponde
        If idJuego = 50 And General.Jurisdiccion = "S" Then
            Dim oSorteoBO As New PgmSorteoBO
            Dim _vprogresion As Integer = 0
            Dim _nrosorteo As Integer = 0
            Dim _pgmsorteo As Long = 0
            _nrosorteo = Mid("00000", 1, 5 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text
            _pgmsorteo = idJuego * 1000000 + _nrosorteo
            _vprogresion = oSorteoBO.RecuperaProgresionLoteria(_pgmsorteo)
            txtGan5007001.Text = _vprogresion.ToString.Trim


        End If
        ' 5. Guardo Premios, si corresponde
        If guardaPremios Then
            FileSystemHelper.Log(" frmPremios: ejecuta premioGuardar, para el juego:" & idJuego)
            premioGuardar(idJuego, True)
        End If
    End Sub

    Public Sub setPremioDesdeArchivoZIP(ByVal idJuego As Int16, Optional ByVal Obtenerarchivo As Boolean = False)
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
        Dim controlTxt As Control
        Dim gralDal As New sorteos.data.GeneralDAL
        Dim rta As Integer
        Dim boPremio As New PremioBO
        'Dim lista As List(Of Premio)
        Dim prefijo As String = General.PrefijoPremio
        Dim Sorteodal As New PgmSorteoDAL
        Dim idSorteo As String
        Dim idPgmSorteo As Long
        Dim ArchivoOrigen As String = ""
        Dim ArchivoDestino As String = ""
        Dim PathDestino As String = ""
        Dim NombreArchivo As String = ""
        Dim Archivocontrol As String = ""
        Dim parametrosCopiar As String()
        Dim cantidadAciertos As Integer = 0
        Dim cantidadAciertosNuevo As Integer = 0

        Dim RequiereAciertos As Integer = 0
        Dim NombrePremio As String = ""
        Dim _Habilita_2Premio_Quini6 As Boolean
        Dim _habilita As Boolean
        Dim PremioPorApuesta As Double
        Dim _archivosIguales As Boolean = False
        Dim pathOrigen As String
        '** loteria y loteria chica Boldt generar los premios como poz
        Dim prefijoLoterias As String = General.PrefijoPozo
        Dim NombreArchivoLoterias As String = ""
        Dim ArchivoOrigenLoteria As String = ""
        Dim usuarioBO As New UsuarioBO
        Dim idUsuario As Long = 0
        Dim vinoPozoEstimado As Boolean = False
        Dim tomarPozoSugerido As Boolean = False
        Dim hay6Ac As Boolean
        Dim adic_tipo As Integer
        Dim codigo_nuevo As Long
        Dim codigo_habilitar As Long
        Dim cantidadAciertos_nuevo As Integer
        Dim requiereAciertos_nuevo As Boolean
        Dim estimulo As Boolean = True
        Dim codigo_estimulo_habilitar As Long

        Dim nombre_prv As String = ""
        Dim nombre_age As String = ""
        Dim odal As New PgmConcursoDAL
        Dim existe As Boolean = False

        Dim prv_encontrado As Boolean = False
        Dim age_encontrado As Boolean = False

        Dim oSorteoBO As New PgmSorteoBO

        Try
            _cargando = True
            idJuegoAct = Mid("00", 1, 2 - Len(getIdJuegoActual())) & getIdJuegoActual()
            sorteoAct = Mid("000000", 1, 6 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text

            'idJuegoAct = getIdJuegoActual()
            'sorteoAct = getControlJ("txtSorteoJuego").Text
            idSorteo = idJuegoAct & sorteoAct

            idpgmsorteo = CLng(idSorteo)
            '** comentar al implementar zip
            pathOrigen = gralDal.getParametro("INI", "PATH_PREMIOS")

            If Not pathOrigen.EndsWith("\") Then
                pathOrigen = pathOrigen & "\"
            End If
            'RL
            'archivo = pathOrigen & prefijo & idJuegoAct.PadLeft(2, "0") & sorteoAct & ".dat"

            '***** descomentar al implementar archivo zip
            '*****
            '** se formatea el nombre del archivo
            NombreArchivo = prefijo & idJuegoAct.PadLeft(2, "0") & sorteoAct

            nombre_prv = "PRV" & getIdJuegoActual().ToString().PadLeft(2, "0") & getControlJ("txtSorteoJuego").Text.PadLeft(5, "0") & ".pdf"
            nombre_age = "AGE" & getIdJuegoActual().ToString().PadLeft(2, "0") & getControlJ("txtSorteoJuego").Text.PadLeft(5, "0") & ".pdf"

            '** obtengo la ruta  donde se guardan los archivos zip
            PathDestino = General.Path_Premios_Destino
            '** obtengo el archivo zip
            'RL
            ArchivoOrigen = pathOrigen & NombreArchivo & ".zip"

            '**** para loteria y loteria chica viene los premios como poz ***
            '****************************************************************
            If idJuego = 50 Or idJuego = 51 Then
                existe = False
                Try
                    existe = File.Exists(ArchivoOrigen)
                Catch ex As Exception
                    FileSystemHelper.Log(" setpremiosdesdearchivozip:FAllo al verificar si existe el archivo para Loteria: " & ArchivoOrigen & " ex:" & ex.Message)
                End Try

                If Not existe Then
                    NombreArchivoLoterias = prefijoLoterias & idJuegoAct.PadLeft(2, "0") & sorteoAct
                    ArchivoOrigenLoteria = pathOrigen & NombreArchivoLoterias & ".zip"
                    'si existe el archivo con prefijo poz 
                    If File.Exists(ArchivoOrigenLoteria) Then
                        ArchivoOrigen = ArchivoOrigenLoteria
                        NombreArchivo = NombreArchivoLoterias
                    End If

                End If
            End If

            '** formo el path del archivo destino ,si se deszipeo con exito
            If Not PathDestino.EndsWith("\") Then
                PathDestino = PathDestino & "\"
            End If

            'controla que el origen y el destino no sean iguales
            If pathOrigen = PathDestino Then
                _archivosIguales = True
            End If
            'RL
            ArchivoDestino = PathDestino & NombreArchivo & ".dat"
            Archivocontrol = PathDestino & NombreArchivo & ".cxt"


            '** busca en la BD si hay premios cargados sino los busca en el archivo
            If Not Obtenerarchivo Then
                If Not Sorteodal.NoTienePremiosCargados(idpgmsorteo, CInt(idJuegoAct)) Then
                    setPozo()
                    _cargando = False
                    Exit Sub
                Else
                    '*** 27/09/2013
                    If General.Jurisdiccion = "E" Then
                        'setea los pozo cuando es poceada pero no sigue con la carga desde archivo porque no esta habilitada
                        If idJuego = 30 Then
                            setPozo()
                        End If
                        Exit Sub
                    End If
                    '****** DESCOMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                    'RL

                    existe = False
                    Try
                        existe = File.Exists(ArchivoOrigen)
                    Catch ex As Exception
                        FileSystemHelper.Log(" setpremiosdesdearchivozip:FAllo al verificar si existe el archivo para: " & ArchivoOrigen & " ex:" & ex.Message)
                    End Try

                    If Not existe Then

                        '*** COMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                        'RL
                        'If Not File.Exists(archivo) Then

                        rta = MsgBox("No pudo localizarse el archivo en la ruta por defecto.  Desea seleccionarlo manualmente.", MsgBoxStyle.YesNo, MDIContenedor.Text)

                        If rta = vbYes Then
                            OpenFileD.Filter = "Archivos de sorteos|*.zip"
                            OpenFileD.DefaultExt = "zip"
                            OpenFileD.ShowDialog()

                            If OpenFileD.FileNames.Count = 0 Then
                                _cargando = False
                                Exit Sub
                            Else
                                'pathOrigen = openFileD.
                                archivo = OpenFileD.FileNames(0)
                                '**DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                                'RL
                                ArchivoOrigen = OpenFileD.FileNames(0)
                                Dim a As Integer = InStrRev(ArchivoOrigen, "\")
                                NombreArchivo = ArchivoOrigen.Substring(a)
                                pathOrigen = ArchivoOrigen.Substring(0, ArchivoOrigen.Length - NombreArchivo.Length)
                                NombreArchivo = NombreArchivo.Replace(".zip", "")
                                ArchivoDestino = PathDestino & NombreArchivo & ".dat"
                                Archivocontrol = PathDestino & NombreArchivo & ".cxt"


                            End If
                        Else
                            MsgBox("Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information, MDIContenedor.Text)
                            setPozo()
                            _cargando = False
                            Exit Sub
                        End If
                    End If
                    '**18/10/2012****
                    'DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                    '** copia el zip a la carpeta destino
                    If _archivosIguales Then
                        MsgBox("Los parámetros de origen y destino configurados para premios son iguales. No se realiza la carga de premios desde archivo.", MsgBoxStyle.Information, MDIContenedor.Text)
                        _cargando = False
                        Exit Sub
                    End If
                    ReDim parametrosCopiar(0)
                    parametrosCopiar(0) = pathOrigen & ";" & PathDestino & ";" & NombreArchivo & ".zip"
                    FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")

                    '' ''** descomprime el archivo a la carpeta destino
                    'RL
                    FileSystemHelper.Descomprimir(PathDestino, ArchivoOrigen)

                    '** control del archivo contra el archivo de control md5
                    If General.CtrMD5Premios = "S" Then
                        If Not FileSystemHelper.ControlArchivoMd5(ArchivoDestino, Archivocontrol) Then
                            MsgBox("El archivo " & NombreArchivo & ".dat no coincide con el archivo de control.Los premios no pueden ser cargados." & vbCrLf & "Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information, MDIContenedor.Text)
                            setPozo()
                            'borra archivos .dat y .cxt
                            FileSystemHelper.BorrarArchivo(ArchivoDestino)
                            FileSystemHelper.BorrarArchivo(Archivocontrol)
                            _cargando = False
                            Exit Sub
                        End If
                    End If
                    f = New StreamReader(ArchivoDestino)

                    '*** COMENTAR LA SIGUIENTE LINEA  AL  IMPLEMENTAR ARCHIVO ZIP
                    'RL
                    'f = New StreamReader(archivo)
                    linea = ""
                    hay6Ac = False
                    While Not f.EndOfStream
                        linea = f.ReadLine()

                        juego = Mid(linea, 1, 2)
                        sorteo = Mid(linea, 3, 6)
                        modalidad = Mid(linea, 9, 2)
                        If modalidad = 5 Then
                            Dim aca As String = ""
                        End If
                        codigo = idJuegoAct & Mid(linea, 13, 5)
                        importePozo = Mid(linea, 18, 17) ' 15E2D
                        cantGanadores = Mid(linea, 35, 6)
                        importePremio = Mid(linea, 41, 17) ' 15E2D
                        vacante = Mid(linea, 58, 1)

                        '** 18/10/2012 ****
                        boPremio.ObtieneDatosPremio(codigo, cantidadAciertos, RequiereAciertos, NombrePremio, juego, sorteo, adic_tipo)
                        _habilita = IIf((RequiereAciertos = 0), False, True)
                        cantidadAciertosNuevo = cantidadAciertos

                        ' controles
                        If CInt(juego) <> CInt(idJuegoAct) Then
                            MsgBox("El registro no corresponde al juego actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                            _cargando = False
                            Exit Sub
                        End If

                        If CDbl(sorteo) <> CDbl(sorteoAct) Then
                            MsgBox("El registro no corresponde al sorteo actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                            _cargando = False
                            Exit Sub
                        End If

                        If modalidad = "09" Then
                            Try
                                idUsuario = usuarioBO.getUsuarioID("Sistema")
                                boPremio.GuardarPozoEstimadoJuego(juego, IIf(CDbl(importePozo / 100) > 0, CDbl(importePozo / 100), CDbl(importePremio / 100)), idUsuario)
                                vinoPozoEstimado = True
                            Catch ex As Exception
                                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & MDIContenedor.usuarioAutenticado.Usuario & " Setpremiodesdearchivozip: error al guardar pozo estimado:" & ex.Message)
                                MsgBox("Problemas al leer el Pozo Estimado para Próximo Sorteo. Verifique el archivo de premios con Operadores de Boldt, cierre y abra nuevamente la aplicación y vuelva a Otener Premios.", MsgBoxStyle.Information, MDIContenedor.Text)
                            End Try
                            Continue While
                        End If
                        '*** los sueldos no viene en el archivo de premios
                        If codigo = 1301006 Then
                            Continue While
                        End If
                        '*** en el siempre sale vienen diferentes codigos segun la cant de aciertos pero van a parar
                        '*** siempre al mismo grupo de txts en la pantalla
                        If codigo = 407003 Or codigo = 407004 Or codigo = 407005 Then
                            codigo = 407001
                        End If
                        ' Q6 y BR adicional: 
                        '   Adic_Tipo = 1 -> Si hay 6 aciertos se da un segundo premio con 5,4 o 3 aciertos
                        '   Adic_Tipo = 2 -> Funciona como un Siempre Sale -> cualquiera sea la cant. aci. se deben mostrar como Primer Premio
                        If codigo = 405001 Or codigo = 1305001 Then
                            hay6Ac = True
                            If adic_tipo = 2 Then
                                _Habilita_2Premio_Quini6 = False
                            Else
                                _Habilita_2Premio_Quini6 = True
                            End If
                        End If
                        If codigo = 407002 Then
                            Dim mivar As String = ""
                        End If
                        If (codigo = 405002 Or codigo = 405003 Or codigo = 405004) Or (codigo = 1305002 Or codigo = 1305003 Or codigo = 1305004) Then
                            codigo_nuevo = codigo
                            If (codigo = 405002 Or codigo = 405003 Or codigo = 405004) Then
                                codigo_habilitar = 405002
                            End If
                            If (codigo = 1305002 Or codigo = 1305003 Or codigo = 1305004) Then
                                codigo_habilitar = 135002
                            End If
                            If (codigo = 405003 Or codigo = 405004) Or (codigo = 1305003 Or codigo = 1305004) Then
                                If (codigo = 405003 Or codigo = 405004) Then
                                    codigo_estimulo_habilitar = 405005
                                End If
                                If (codigo = 1305003 Or codigo = 1305004) Then
                                    codigo_estimulo_habilitar = 1305005
                                End If
                                estimulo = False
                            Else
                                estimulo = True
                            End If
                            If adic_tipo = 2 Then
                                _Habilita_2Premio_Quini6 = False
                                If juego = 4 Then
                                    codigo_nuevo = 405001
                                Else
                                    codigo_nuevo = 1305001
                                End If
                            Else
                                If hay6Ac Then
                                    _Habilita_2Premio_Quini6 = True
                                    If juego = 4 Then
                                        codigo_nuevo = 405002
                                    Else
                                        codigo_nuevo = 1305002
                                    End If
                                Else
                                    _Habilita_2Premio_Quini6 = False
                                    If juego = 4 Then
                                        codigo_nuevo = 405001
                                    Else
                                        codigo_nuevo = 1305001
                                    End If
                                End If
                            End If
                            If juego = 4 Then
                                'getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo_nuevo).Text = cantidadAciertos
                                getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo_habilitar).Enabled = _Habilita_2Premio_Quini6
                                getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo_habilitar).Enabled = _Habilita_2Premio_Quini6
                                getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo_habilitar).Enabled = _Habilita_2Premio_Quini6
                                getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo_habilitar).Enabled = _Habilita_2Premio_Quini6
                            End If
                            ''If estimulo = False Then
                            ''    getControlJP("pst" & idJuegoAct & "05", "txtAciertos" & codigo_estimulo_habilitar).Enabled = estimulo
                            ''    getControlJP("pst" & idJuegoAct & "05", "txtPozo" & codigo_estimulo_habilitar).Enabled = estimulo
                            ''    getControlJP("pst" & idJuegoAct & "05", "txtGan" & codigo_estimulo_habilitar).Enabled = estimulo
                            ''    getControlJP("pst" & idJuegoAct & "05", "txtPrem" & codigo_estimulo_habilitar).Enabled = estimulo
                            ''End If
                            codigo = codigo_nuevo
                            ' como cambie el codigo, vuelvo a obtener los datos
                            boPremio.ObtieneDatosPremio(codigo, cantidadAciertos_nuevo, requiereAciertos_nuevo, NombrePremio, juego, sorteo, adic_tipo)
                        End If
                        ' FIN Q6 y BR adicional
                        If idJuegoAct = 50 Or idJuegoAct = 51 Then
                            getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Text = CDbl(importePremio / 100)
                            If codigo = 5007001 And General.Jurisdiccion = "S" Then 'la progresion se guarda como cantganadores
                                getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Text = CLng(cantGanadores)
                            End If
                            If idJuegoAct = 50 Then
                                getControlJP("pst" & idJuegoAct & modalidad, "lblPrem" & codigo).Text = NombrePremio
                            End If
                        Else

                            controlTxt = getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo)
                            'la modalidad 09 por ser el pozo estimado no existen  controles
                            If IsNothing(controlTxt) And modalidad <> "09" Then
                                MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                            Else ' la modalidad '09' es el importe de pozo estimado no corresponde al circuito de premio y solo e para brinco y quini6
                                If modalidad = "09" Then
                                    ' RL: 12-01-2015. lo comenté porque lo llevé arriba, hasta poder REFACTORIZAR este bendito menjunje!!
                                    ''Try
                                    ''    idUsuario = usuarioBO.getUsuarioID("Sistema")
                                    ''    boPremio.GuardarPozoEstimadoJuego(juego, IIf(CDbl(importePozo / 100) > 0, CDbl(importePozo / 100), CDbl(importePremio / 100)), idUsuario)
                                    ''    vinoPozoEstimado = True
                                    ''Catch ex As Exception
                                    ''    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & MDIContenedor.usuarioAutenticado.Usuario & " Setpremiodesdearchivozip: error al guardar pozo estimado:" & ex.Message)
                                    ''End Try
                                Else 'sigue como siempre
                                    'para quini 6 y  brinco comun
                                    If idJuegoAct = 4 Or idJuegoAct = 13 Then
                                        ''If idJuegoAct = 4 Then
                                        ''    If (codigo = 405002 Or codigo = 405003 Or codigo = 405004) Or (codigo = 1305002 Or codigo = 1305003 Or codigo = 1305004) Then
                                        ''        If CLng(cantGanadores) = 0 And CDbl(importePremio / 100) = 0 Then
                                        ''            _Habilita_2Premio_Quini6 = False
                                        ''        Else
                                        ''            _Habilita_2Premio_Quini6 = True
                                        ''        End If
                                        ''        getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Enabled = _Habilita_2Premio_Quini6
                                        ''        getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo).Enabled = _Habilita_2Premio_Quini6
                                        ''        getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Enabled = _Habilita_2Premio_Quini6
                                        ''        getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Enabled = _Habilita_2Premio_Quini6
                                        ''    End If
                                        ''End If
                                        getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Text = IIf((cantidadAciertosNuevo = 0), "", cantidadAciertosNuevo)
                                        getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Enabled = _habilita
                                    End If
                                    'si no hubo ganadores se muestra cero en lugar del importe del premio
                                    PremioPorApuesta = CDbl(importePremio / 100)

                                    If idJuegoAct = 13 Or idJuegoAct = 30 Then
                                        If CLng(cantGanadores) = 0 Then
                                            PremioPorApuesta = 0
                                        End If
                                    End If

                                    getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo).Text = CDbl(importePozo / 100)
                                    getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Text = CLng(cantGanadores)
                                    getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Text = PremioPorApuesta
                                    Try
                                        If idJuegoAct <> 30 Then
                                            getControlJP("pst" & idJuegoAct & modalidad, "lblPrem" & codigo).Text = NombrePremio
                                        End If
                                    Catch ex As Exception

                                    End Try
                                End If
                            End If
                        End If
                    End While
                    Try
                        f.Dispose()
                    Catch ex As Exception
                    End Try

                    If hay6Ac And _Habilita_2Premio_Quini6 = False And idJuego = 4 Then
                        getControlJP("pst" & idJuego & "05", "txtAciertos" & "405002").Enabled = _Habilita_2Premio_Quini6
                        getControlJP("pst" & idJuego & "05", "txtPozo" & "405002").Enabled = _Habilita_2Premio_Quini6
                        getControlJP("pst" & idJuego & "05", "txtGan" & "405002").Enabled = _Habilita_2Premio_Quini6
                        getControlJP("pst" & idJuego & "05", "txtPrem" & "405002").Enabled = _Habilita_2Premio_Quini6
                    End If
                    If linea.Trim.Length > 0 And (idJuego = 30 Or idJuego = 4 Or idJuego = 13) And Not vinoPozoEstimado Then
                        tomarPozoSugerido = True
                    End If

                    'HG 22-10-2015
                    '*** ENVIO DE FTP Y COPIA LOCAL DE ARCHIVOS PDF
                    'antes de borrar el archivo,colocar los archivos de prv y age en carpeta local
                    ' el archivo de provincia es obligatorio para quini 6
                    If General.Obtener_pgmsorteos_ws = "S" Then
                        
                        If Not File.Exists(PathDestino & nombre_prv) Then
                            If idJuego = 4 Then
                                MsgBox("No se encontró el archivo de distribución por provincias que es de caracter Obligatorio", MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If
                        Else
                            prv_encontrado = True
                        End If

                        'el archivo de agentes,es obligatorio si salio el primer premio
                        If Not File.Exists(PathDestino & nombre_age) Then
                            If Sorteodal.SalioPrimer_premio(idpgmsorteo, idJuego) Then
                                MsgBox("No se encontró el archivo de Agencia Vendedora Primer premio y es de caracter Obligatorio", MsgBoxStyle.Exclamation)
                                Exit Sub
                            End If
                        Else
                            age_encontrado = True
                        End If

                        Dim carpeta As String = ""
                        Dim datosFTP As Boolean = False
                        'si estan configurados los datos de ftp,servidor,usaurio y clave los envia
                        'NO tiene en cuenta la variable de enviarFTP que  se utiliza en iAFAS para enviar los extractos
                        If General.servidorFTP.Trim <> "" And General.usuarioFTP.Trim <> "" And General.pwdFTP.Trim <> "" Then
                            datosFTP = True
                        End If
                        If prv_encontrado And age_encontrado Then
                            ReDim parametrosCopiar(1)
                            parametrosCopiar(0) = pathOrigen & ";" & General.Path_Premios_Destino & ";" & nombre_prv
                            parametrosCopiar(1) = pathOrigen & ";" & General.Path_Premios_Destino & ";" & nombre_age
                            FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")
                            If datosFTP Then
                                Try
                                    If GeneralBO.CrearDirectorioFTP(General.carpetaFTPExtractos, carpeta) Then
                                        GeneralBO.enviarFTP(PathDestino & nombre_prv, carpeta & nombre_prv)
                                        GeneralBO.enviarFTP(PathDestino & nombre_age, carpeta & nombre_age)

                                        If General.Url_extractos.EndsWith("/") Then
                                            nombre_prv = General.Url_extractos & nombre_prv
                                            nombre_age = General.Url_extractos & nombre_age
                                        Else
                                            nombre_prv = General.Url_extractos & "/" & nombre_prv
                                            nombre_age = General.Url_extractos & "/" & nombre_age
                                        End If
                                        odal.Guardar_url_PDF(idSorteo, nombre_age, nombre_prv)
                                    End If

                                Catch ex As Exception
                                    MsgBox("No se pudieron enviar los Archivos al Servidor FTP", MsgBoxStyle.Information)
                                End Try

                            End If
                        Else
                            If prv_encontrado Then
                                ReDim parametrosCopiar(0)
                                parametrosCopiar(0) = pathOrigen & ";" & General.Path_Premios_Destino & ";" & nombre_prv
                                FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")
                                If datosFTP Then
                                    Try
                                        'If GeneralBO.CrearDirectorioFTP("Extracto_pdf", carpeta) Then
                                        If GeneralBO.CrearDirectorioFTP(General.carpetaFTPExtractos, carpeta) Then
                                            GeneralBO.enviarFTP(PathDestino & nombre_prv, carpeta & nombre_prv)
                                            If General.Url_extractos.EndsWith("/") Then
                                                nombre_prv = General.Url_extractos & nombre_prv
                                            Else
                                                nombre_prv = General.Url_extractos & "/" & nombre_prv
                                            End If
                                            nombre_age = ""
                                            odal.Guardar_url_PDF(idSorteo, nombre_age, nombre_prv)
                                        End If

                                    Catch ex As Exception
                                        MsgBox("No se pudieron enviar los Archivos al Servidor FTP", MsgBoxStyle.Information)
                                    End Try

                                End If
                            Else
                                If age_encontrado Then
                                    ReDim parametrosCopiar(0)
                                    parametrosCopiar(0) = pathOrigen & ";" & General.Path_Premios_Destino & ";" & nombre_age
                                    FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")
                                    If datosFTP Then
                                        Try
                                            If GeneralBO.CrearDirectorioFTP(General.carpetaFTPExtractos, carpeta) Then
                                                GeneralBO.enviarFTP(PathDestino & nombre_age, carpeta & nombre_age)
                                                If General.Url_extractos.EndsWith("/") Then
                                                    nombre_age = General.Url_extractos & nombre_age
                                                Else
                                                    nombre_age = General.Url_extractos & "/" & nombre_age
                                                End If
                                                nombre_prv = ""
                                                odal.Guardar_url_PDF(idSorteo, nombre_age, nombre_prv)
                                            End If

                                        Catch ex As Exception
                                            MsgBox("No se pudieron enviar los Archivos al Servidor FTP", MsgBoxStyle.Information)
                                        End Try
                                    End If
                                End If
                            End If
                        End If
                    End If
                    '*** FIN FTP Y COPIA LOCAL PDF

                    '** DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                    '** borra los archivos .dat y .cxt,dejando solo los archivos zip en la carpeta de destino
                    'RL
                    _cargando = False
                    FileSystemHelper.BorrarArchivo(ArchivoDestino)
                    FileSystemHelper.BorrarArchivo(Archivocontrol)
                    End If
            Else ' ELSE OBTENERARCHIVO
                    '******Se trata de cargar desde el archivo sin importar si tiene premios cargados o no
                If MsgBox("Los premios se actualizarán con los datos del archivo." & vbCrLf & "¿Desea continuar?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, MDIContenedor.Text) = MsgBoxResult.No Then
                    _cargando = False
                    Exit Sub
                End If
                existe = False
                Try
                    existe = File.Exists(ArchivoOrigen)
                Catch ex As Exception
                    FileSystemHelper.Log(" setpremiosdesdearchivozip:FAllo al verificar si existe el archivo para: " & ArchivoOrigen & " ex:" & ex.Message)
                End Try

                If Not existe Then
                    rta = MsgBox("No pudo localizarse el archivo en la ruta por defecto.  Desea seleccionarlo manualmente.", MsgBoxStyle.YesNo, MDIContenedor.Text)
                    If rta = vbYes Then
                        OpenFileD.Filter = "Archivos de sorteos|*.zip"
                        OpenFileD.DefaultExt = "zip"
                        OpenFileD.ShowDialog()
                        If OpenFileD.FileNames.Count = 0 Then
                            _cargando = False
                            Exit Sub
                        Else
                            archivo = OpenFileD.FileNames(0)
                            ArchivoOrigen = OpenFileD.FileNames(0)
                            Dim a As Integer = InStrRev(ArchivoOrigen, "\")
                            NombreArchivo = ArchivoOrigen.Substring(a)
                            pathOrigen = ArchivoOrigen.Substring(0, ArchivoOrigen.Length - NombreArchivo.Length)
                            NombreArchivo = NombreArchivo.Replace(".zip", "")
                            ArchivoDestino = PathDestino & NombreArchivo & ".dat"
                            Archivocontrol = PathDestino & NombreArchivo & ".cxt"
                        End If
                    Else
                        MsgBox("Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information, MDIContenedor.Text)
                        setPozo()
                        _cargando = False
                        Exit Sub
                    End If
                End If
                If _archivosIguales Then
                    MsgBox("Los parámetros de origen y destino configurados para premios son iguales. No se realiza la carga de premios desde archivo.", MsgBoxStyle.Information, MDIContenedor.Text)
                    _cargando = False
                    Exit Sub
                End If
                '** copia el zip a la carpeta destino
                ReDim parametrosCopiar(0)
                parametrosCopiar(0) = pathOrigen & ";" & PathDestino & ";" & NombreArchivo & ".zip"
                FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")
                FileSystemHelper.Descomprimir(PathDestino, ArchivoOrigen)

                '** control del archivo contra el archivo de control md5
                If General.CtrMD5Premios = "S" Then
                    If Not FileSystemHelper.ControlArchivoMd5(ArchivoDestino, Archivocontrol) Then
                        MsgBox("El archivo " & NombreArchivo & ".dat no coincide con el archivo de control.Los premios no pueden ser cargados." & vbCrLf & "Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information, MDIContenedor.Text)
                        setPozo()
                        'borra archivos .dat y .cxt
                        FileSystemHelper.BorrarArchivo(ArchivoDestino)
                        FileSystemHelper.BorrarArchivo(Archivocontrol)
                        _cargando = False
                        Exit Sub
                    End If
                End If
                f = New StreamReader(ArchivoDestino)

                linea = ""
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

                    boPremio.ObtieneDatosPremio(codigo, cantidadAciertos, RequiereAciertos, NombrePremio, juego, sorteo, adic_tipo)
                    _habilita = IIf((RequiereAciertos = 0), False, True)
                    cantidadAciertosNuevo = cantidadAciertos

                    ' controles
                    If CInt(juego) <> CInt(idJuegoAct) Then
                        MsgBox("El registro no corresponde al juego actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                        _cargando = False
                        Exit Sub
                    End If
                    If CDbl(sorteo) <> CDbl(sorteoAct) Then
                        MsgBox("El registro no corresponde al sorteo actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                        _cargando = False
                        Exit Sub
                    End If

                    If modalidad = "09" Then
                        Try
                            idUsuario = usuarioBO.getUsuarioID("Sistema")
                            boPremio.GuardarPozoEstimadoJuego(juego, IIf(CDbl(importePozo / 100) > 0, CDbl(importePozo / 100), CDbl(importePremio / 100)), idUsuario)
                            vinoPozoEstimado = True
                        Catch ex As Exception
                            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & MDIContenedor.usuarioAutenticado.Usuario & " Setpremiodesdearchivozip: error al guardar pozo estimado:" & ex.Message)
                            MsgBox("Problemas al leer el Pozo Estimado para Próximo Sorteo. Verifique el archivo de premios con Operadores de Boldt, cierre y abra nuevamente la aplicación y vuelva a Otener Premios.", MsgBoxStyle.Information, MDIContenedor.Text)
                        End Try
                        Continue While
                    End If
                    '*** los sueldos no viene en el archivo de premios
                    If codigo = 1301006 Then
                        Continue While
                    End If
                    '*** en el siempre sale vienen diferentes codigos segun la cant de aciertos pero van a parar
                    '*** siempre al mismo grupo de txts en la pantalla
                    If codigo = 407003 Or codigo = 407004 Or codigo = 407005 Then
                        codigo = 407001
                    End If
                    ' Q6 y BR adicional: 
                    '   Adic_Tipo = 1 -> Si hay 6 aciertos se da un segundo premio con 5,4 o 3 aciertos
                    '   Adic_Tipo = 2 -> Funciona como un Siempre Sale -> cualquiera sea la cant. aci. se deben mostrar como Primer Premio
                    If codigo = 405001 Or codigo = 1305001 Then
                        hay6Ac = True
                        If adic_tipo = 2 Then
                            _Habilita_2Premio_Quini6 = False
                        Else
                            _Habilita_2Premio_Quini6 = True
                        End If
                    End If
                    If (codigo = 405002 Or codigo = 405003 Or codigo = 405004) Or (codigo = 1305002 Or codigo = 1305003 Or codigo = 1305004) Then
                        codigo_nuevo = codigo
                        If (codigo = 405002 Or codigo = 405003 Or codigo = 405004) Then
                            codigo_habilitar = 405002
                        End If
                        If (codigo = 1305002 Or codigo = 1305003 Or codigo = 1305004) Then
                            codigo_habilitar = 135002
                        End If
                        If (codigo = 405003 Or codigo = 405004) Or (codigo = 1305003 Or codigo = 1305004) Then
                            If (codigo = 405003 Or codigo = 405004) Then
                                codigo_estimulo_habilitar = 405005
                            End If
                            If (codigo = 1305003 Or codigo = 1305004) Then
                                codigo_estimulo_habilitar = 1305005
                            End If
                            estimulo = False
                        Else
                            estimulo = True
                        End If
                        If adic_tipo = 2 Then
                            _Habilita_2Premio_Quini6 = False
                            If juego = 4 Then
                                codigo_nuevo = 405001
                            Else
                                codigo_nuevo = 1305001
                            End If
                        Else
                            If hay6Ac Then
                                _Habilita_2Premio_Quini6 = True
                                If juego = 4 Then
                                    codigo_nuevo = 405002
                                Else
                                    codigo_nuevo = 1305002
                                End If
                            Else
                                _Habilita_2Premio_Quini6 = False
                                If juego = 4 Then
                                    codigo_nuevo = 405001
                                Else
                                    codigo_nuevo = 1305001
                                End If
                            End If
                        End If
                        'getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo_nuevo).Text = cantidadAciertos
                        If juego = 4 Then
                            getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo_habilitar).Enabled = _Habilita_2Premio_Quini6
                            getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo_habilitar).Enabled = _Habilita_2Premio_Quini6
                            getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo_habilitar).Enabled = _Habilita_2Premio_Quini6
                            getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo_habilitar).Enabled = _Habilita_2Premio_Quini6
                        End If

                        ''If estimulo = False Then
                        ''    getControlJP("pst" & idJuegoAct & "05", "txtAciertos" & codigo_estimulo_habilitar).Enabled = estimulo
                        ''    getControlJP("pst" & idJuegoAct & "05", "txtPozo" & codigo_estimulo_habilitar).Enabled = estimulo
                        ''    getControlJP("pst" & idJuegoAct & "05", "txtGan" & codigo_estimulo_habilitar).Enabled = estimulo
                        ''    getControlJP("pst" & idJuegoAct & "05", "txtPrem" & codigo_estimulo_habilitar).Enabled = estimulo
                        ''End If

                        codigo = codigo_nuevo
                        ' como cambie el codigo, vuelvo a obtener los datos
                        boPremio.ObtieneDatosPremio(codigo, cantidadAciertos, RequiereAciertos, NombrePremio, juego, sorteo, adic_tipo)
                    End If
                    ' FIN Q6 y BR adicional
                    If idJuegoAct = 50 Or idJuegoAct = 51 Then
                        getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Text = CDbl(importePremio / 100)
                        If codigo = 5007001 Then 'la progresion se guarda como cantganadores
                            getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Text = CLng(cantGanadores)
                        End If
                        If idJuegoAct = 50 Then
                            getControlJP("pst" & idJuegoAct & modalidad, "lblPrem" & codigo).Text = NombrePremio
                        End If
                    Else
                        controlTxt = getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo)
                        If IsNothing(controlTxt) And modalidad <> "09" Then
                            MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Else
                            '******06/02/2013 hg  ***************************
                            'se guarda el pozo estimado que ahora tb viene en premio como modalidad 09 para quini6 y brinco
                            If modalidad = "09" Then
                                ' RL: 12-01-2015. lo comenté porque lo llevé arriba, hasta poder REFACTORIZAR este bendito menjunje!!
                                ''Try
                                ''    idUsuario = usuarioBO.getUsuarioID("Sistema")
                                ''    boPremio.GuardarPozoEstimadoJuego(juego, IIf(CDbl(importePozo / 100) > 0, CDbl(importePozo / 100), CDbl(importePremio / 100)), idUsuario)
                                ''    vinoPozoEstimado = True
                                ''Catch ex As Exception
                                ''    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & MDIContenedor.usuarioAutenticado.Usuario & " Setpremiodesdearchivozip: error al guardar pozo estimado:" & ex.Message)
                                ''End Try
                            Else 'sigue como siempre
                                'para quini 6 y  brinco comun
                                If idJuegoAct = 4 Or idJuegoAct = 13 Then
                                    ''If idJuegoAct = 4 Then
                                    ''    If codigo = 405002 Then
                                    ''        If CLng(cantGanadores) = 0 And CDbl(importePremio / 100) = 0 Then
                                    ''            _Habilita_2Premio_Quini6 = False
                                    ''        Else
                                    ''            _Habilita_2Premio_Quini6 = True
                                    ''        End If
                                    ''        getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    ''        getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    ''        getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    ''        getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    ''    End If
                                    ''End If
                                    getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Text = IIf((cantidadAciertosNuevo = 0), "", cantidadAciertosNuevo)
                                    getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Enabled = _habilita
                                End If
                                'si no hubo ganadores se muestra cero en lugar del importe del premio
                                PremioPorApuesta = CDbl(importePremio / 100)

                                If idJuegoAct = 13 Or idJuegoAct = 30 Then
                                    If CLng(cantGanadores) = 0 Then
                                        PremioPorApuesta = 0
                                    End If
                                End If

                                getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo).Text = CDbl(importePozo / 100)
                                getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Text = CLng(cantGanadores)
                                getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Text = PremioPorApuesta
                                Try
                                    If idJuegoAct <> 30 Then
                                        getControlJP("pst" & idJuegoAct & modalidad, "lblPrem" & codigo).Text = NombrePremio
                                    End If
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If
                End While
                Try
                    f.Dispose()
                Catch ex As Exception
                End Try

                If hay6Ac And _Habilita_2Premio_Quini6 = False And idJuego = 4 Then
                    getControlJP("pst" & idJuego & "05", "txtAciertos" & "405002").Text = ""
                    getControlJP("pst" & idJuego & "05", "txtAciertos" & "405002").Enabled = _Habilita_2Premio_Quini6
                    getControlJP("pst" & idJuego & "05", "txtPozo" & "405002").Enabled = _Habilita_2Premio_Quini6
                    getControlJP("pst" & idJuego & "05", "txtGan" & "405002").Enabled = _Habilita_2Premio_Quini6
                    getControlJP("pst" & idJuego & "05", "txtPrem" & "405002").Enabled = _Habilita_2Premio_Quini6
                End If
                If linea.Trim.Length > 0 And (idJuego = 30 Or idJuego = 4 Or idJuego = 13) And Not vinoPozoEstimado Then
                    tomarPozoSugerido = True
                End If


                ' RL. 01/02/2018 - Validacion de los pdf de distrib x pcia y primer premio 
                '                  SIN DEPENDER DE CONEXION A CALENDARIO DE SORTEOS SUITE
                If (idJuego = 4 OrElse idJuego = 13 OrElse idJuego = 30) AndAlso (oSorteoBO.exigirPdfPrimerPremio(idPgmSorteo)) Then
                    If (Not File.Exists(PathDestino & nombre_age)) Then
                        MsgBox("No se encontró el archivo requerido con el detalle de Agencias Vendedoras de los Primeros Premios. Consulte a Soporte.", MsgBoxStyle.Exclamation, MDIContenedor.Text)
                        Exit Sub
                    End If
                End If
                If (idJuego = 4 OrElse idJuego = 13 OrElse idJuego = 30) AndAlso (oSorteoBO.exigirPdfDistribPcias(idPgmSorteo)) Then
                    If (Not File.Exists(PathDestino & nombre_prv)) Then
                        MsgBox("No se encontró el archivo requerido con el detalle de Agencias Vendedoras de los Primeros Premios. Consulte a Soporte.", MsgBoxStyle.Exclamation, MDIContenedor.Text)
                        Exit Sub
                    End If
                End If
                ' FIN RL. 01/02/2018 - Validacion de los pdf de distrib x pcia y primer premio 
                '                  SIN DEPENDER DE CONEXION A CALENDARIO DE SORTEOS SUITE


                'HG 22-10-2015
                '*** ENVIO DE FTP Y COPIA LOCAL DE ARCHIVOS PDF
                'antes de borrar el archivo,colocar los archivos de prv y age en carpeta local
                ' el archivo de provincia es obligatorio para quini 6
                If General.Obtener_pgmsorteos_ws = "S" Then
                    If Not File.Exists(PathDestino & nombre_prv) Then
                        If idJuego = 4 Then
                            MsgBox("No se encontró el archivo requerido con la  Distribución de Ganadores por Provincia. Consulte a Soporte.", MsgBoxStyle.Exclamation, MDIContenedor.Text)
                            Exit Sub
                        End If
                    Else
                        prv_encontrado = True
                    End If
                    'el archivo de agentes,es obligatorio si salio el primer premio
                    If (Not File.Exists(PathDestino & nombre_age)) Then
                        If Sorteodal.SalioPrimer_premio(idpgmsorteo, idJuego) Then
                            MsgBox("No se encontró el archivo de Agencia Vendedora Primer premio y es de caracter Obligatorio", MsgBoxStyle.Exclamation, MDIContenedor.Text)
                            Exit Sub
                        End If
                    Else
                        age_encontrado = True
                    End If

                    Dim carpeta As String = ""
                    Dim datosFTP As Boolean = False
                    'si estan configurados los datos de ftp,servidor,usaurio y clave los envia
                    'NO tiene en cuenta la variable de enviarFTP que  se utiliza en iAFAS para enviar los extractos
                    If General.servidorFTP.Trim <> "" And General.usuarioFTP.Trim <> "" And General.pwdFTP.Trim <> "" Then
                        datosFTP = True
                    End If
                    If prv_encontrado And age_encontrado Then
                        ReDim parametrosCopiar(1)
                        parametrosCopiar(0) = pathOrigen & ";" & General.Path_Premios_Destino & ";" & nombre_prv
                        parametrosCopiar(1) = pathOrigen & ";" & General.Path_Premios_Destino & ";" & nombre_age
                        FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")
                        If datosFTP Then
                            Try
                                If GeneralBO.CrearDirectorioFTP(General.carpetaFTPExtractos, carpeta) Then
                                    GeneralBO.enviarFTP(PathDestino & nombre_prv, carpeta & nombre_prv)
                                    GeneralBO.enviarFTP(PathDestino & nombre_age, carpeta & nombre_age)

                                    If General.Url_extractos.EndsWith("/") Then
                                        nombre_prv = General.Url_extractos & nombre_prv
                                        nombre_age = General.Url_extractos & nombre_age
                                    Else
                                        nombre_prv = General.Url_extractos & "/" & nombre_prv
                                        nombre_age = General.Url_extractos & "/" & nombre_age
                                    End If
                                    odal.Guardar_url_PDF(idSorteo, nombre_age, nombre_prv)
                                End If
                            Catch ex As Exception
                                MsgBox("No se pudieron enviar los Archivos al Servidor FTP", MsgBoxStyle.Information)
                            End Try

                        End If
                    Else
                        If prv_encontrado Then
                            ReDim parametrosCopiar(0)
                            parametrosCopiar(0) = pathOrigen & ";" & General.Path_Premios_Destino & ";" & nombre_prv
                            FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")
                            If datosFTP Then
                                Try
                                    'If GeneralBO.CrearDirectorioFTP("Extracto_pdf", carpeta) Then
                                    If GeneralBO.CrearDirectorioFTP(General.carpetaFTPExtractos, carpeta) Then
                                        GeneralBO.enviarFTP(PathDestino & nombre_prv, carpeta & nombre_prv)
                                        If General.Url_extractos.EndsWith("/") Then
                                            nombre_prv = General.Url_extractos & nombre_prv
                                        Else
                                            nombre_prv = General.Url_extractos & "/" & nombre_prv
                                        End If
                                        nombre_age = ""
                                        odal.Guardar_url_PDF(idSorteo, nombre_age, nombre_prv)
                                    End If
                                Catch ex As Exception
                                    MsgBox("No se pudieron enviar los Archivos al Servidor FTP", MsgBoxStyle.Information)
                                End Try

                            End If
                        Else
                            If age_encontrado Then
                                ReDim parametrosCopiar(0)
                                parametrosCopiar(0) = pathOrigen & ";" & General.Path_Premios_Destino & ";" & nombre_age
                                FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")
                                If datosFTP Then
                                    Try
                                        'If GeneralBO.CrearDirectorioFTP("Extracto_pdf", carpeta) Then
                                        If GeneralBO.CrearDirectorioFTP(General.carpetaFTPExtractos, carpeta) Then
                                            GeneralBO.enviarFTP(PathDestino & nombre_age, carpeta & nombre_age)

                                            If General.Url_extractos.EndsWith("/") Then
                                                nombre_age = General.Url_extractos & nombre_age
                                            Else
                                                nombre_age = General.Url_extractos & "/" & nombre_age
                                            End If
                                            nombre_prv = ""
                                            odal.Guardar_url_PDF(idSorteo, nombre_age, nombre_prv)
                                        End If
                                    Catch ex As Exception
                                        MsgBox("No se pudieron enviar los Archivos al Servidor FTP", MsgBoxStyle.Information)
                                    End Try
                                End If
                            End If
                        End If
                    End If
                End If
                '*** FIN FTP Y COPIA LOCAL PDF


                '** DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                '** borra los archivos .dat y .cxt,dejando solo los archivos zip en la carpeta de destino
                'RL
                _cargando = False
                FileSystemHelper.BorrarArchivo(ArchivoDestino)
                FileSystemHelper.BorrarArchivo(Archivocontrol)
            End If

            If tomarPozoSugerido Then
                MsgBox("No se ha encontrado en el archivo el importe del POZO ESTIMADO PARA PROXIMO SORTEO. Se utilizará el último valor disponible.", MsgBoxStyle.Information, MDIContenedor.Text)
                Try
                    idUsuario = usuarioBO.getUsuarioID("Sistema")
                    Dim pozoBO As New PozoBO
                    Dim fe As DateTime
                    importePozo = pozoBO.getPozoSugerido(idJuego, fe)
                    If importePozo <= 0 Then
                        MsgBox("No hay importe de Pozo Estimado para el Próximo Sorteo. Deberá registrarlo manualmente a través del menú Interfaces.")
                    Else
                        boPremio.GuardarPozoEstimadoJuego(idJuego, CDbl(importePozo), idUsuario)
                    End If
                Catch ex As Exception
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & MDIContenedor.usuarioAutenticado.Usuario & " Setpremiodesdearchivozip: error al guardar pozo estimado:" & ex.Message)
                End Try
            End If

            'si llego hasta aca es porque los premios se cargaron desde archivo
            guardaPremios = True
        Catch ex As Exception
            Try
                f.Dispose()
            Catch ex2 As Exception
            End Try
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Problemas al cargar premios: " & ex.Message)
            MsgBox("Problemas al cargar premios: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Public Sub setPremioDesdeArchivo(ByVal idJuego As Int16, Optional ByVal Obtenerarchivo As Boolean = False)
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
        Dim controlTxt As Control
        Dim gralDal As New sorteos.data.GeneralDAL
        Dim rta As Integer
        Dim boPremio As New PremioBO
        'Dim lista As List(Of Premio)
        Dim prefijo As String = General.PrefijoPremio
        Dim Sorteodal As New PgmSorteoDAL
        Dim idSorteo As String
        Dim idpgmsorteo As Long
        Dim ArchivoOrigen As String = ""
        Dim ArchivoDestino As String = ""
        Dim PathDestino As String = ""
        Dim NombreArchivo As String = ""
        Dim Archivocontrol As String = ""
        Dim parametrosCopiar As String()
        Dim cantidadAciertos As Integer = 0
        Dim RequiereAciertos As Integer = 0
        Dim NombrePremio As String = ""
        Dim _Habilita_2Premio_Quini6 As Boolean
        Dim _habilita As Boolean
        Dim PremioPorApuesta As Double
        Dim _archivosIguales As Boolean = False
        Dim pathOrigen As String
        Dim Adic_Tipo As Integer

        Try



            idJuegoAct = Mid("00", 1, 2 - Len(getIdJuegoActual())) & getIdJuegoActual()
            sorteoAct = Mid("000000", 1, 6 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text
            idSorteo = idJuegoAct & sorteoAct
            idpgmsorteo = CLng(idSorteo)
            '** comentar al implementar zip

            pathOrigen = gralDal.getParametro("INI", "PATH_PREMIOS")

            If Not pathOrigen.EndsWith("\") Then
                pathOrigen = pathOrigen & "\"
            End If
            archivo = pathOrigen & prefijo & idJuegoAct.PadLeft(2, "0") & sorteoAct & ".dat"

            '***** descomentar al implementar archivo zip
            '*****
            ' ''** se formatea el nombre del archivo
            NombreArchivo = prefijo & idJuegoAct.PadLeft(2, "0") & sorteoAct
            ''** obtengo la ruta  donde se guardan los archivos zip
            PathDestino = General.Path_Premios_Destino
            ' ''** obtengo el archivo zip
            ''ArchivoOrigen = gralDal.getParametro("INI", "PATH_PREMIOS") & "\" & NombreArchivo & ".zip"

            ' ''** formo el path del archivo destino ,si se deszipeo con exito
            If Not PathDestino.EndsWith("\") Then
                PathDestino = PathDestino & "\"
            End If

            'controla que el origen y el destino no sean iguales
            If pathOrigen = PathDestino Then
                _archivosIguales = True
            End If
            ''ArchivoDestino = PathDestino & NombreArchivo & ".dat"
            ''Archivocontrol = PathDestino & NombreArchivo & ".cxt"


            '** busca en la BD si hay premios cargados sino los busca en el archivo
            If Not Obtenerarchivo Then
                If Not Sorteodal.NoTienePremiosCargados(idpgmsorteo, CInt(idJuegoAct)) Then
                    setPozo()
                    Exit Sub
                Else
                    '****** DESCOMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                    'If Not File.Exists(ArchivoOrigen) Then

                    '*** COMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                    If Not File.Exists(archivo) Then

                        rta = MsgBox("No pudo localizarse el archivo en la ruta por defecto.  Desea seleccionarlo manualmente.", MsgBoxStyle.YesNo, MDIContenedor.Text)

                        If rta = vbYes Then
                            OpenFileD.Filter = "Archivos de sorteos|*.dat"
                            OpenFileD.DefaultExt = "dat"
                            OpenFileD.ShowDialog()

                            If OpenFileD.FileNames.Count = 0 Then
                                Exit Sub
                            Else
                                archivo = OpenFileD.FileNames(0)
                                '**DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                                'ArchivoDestino = OpenFileD.FileNames(0)
                            End If
                        Else
                            MsgBox("Se intentarán localizar valores guardados para los premios.", MsgBoxStyle.Information, MDIContenedor.Text)
                            setPozo()
                            Exit Sub
                        End If
                    End If
                    '**18/10/2012****
                    'DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                    '** copia el zip a la carpeta destino
                    If _archivosIguales Then
                        MsgBox("Los parámetros de origen y destino configurados para premios son iguales. No se realiza la carga de premios desde archivo.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                    ReDim parametrosCopiar(0)
                    parametrosCopiar(0) = gralDal.getParametro("INI", "PATH_PREMIOS") & ";" & PathDestino & ";" & NombreArchivo & ".dat"
                    FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")

                    '' ''** descomprime el archivo a la carpeta destino
                    ' ''FileSystemHelper.Descomprimir(PathDestino, ArchivoOrigen)

                    '' ''** control del archivo contra el archivo de control md5
                    '' ' If Not FileSystemHelper.ControlArchivoMd5(ArchivoDestino, Archivocontrol) Then
                    ' ''    MsgBox("El archivo " & NombreArchivo & ".dat no coincide con el archivo de control.Los premios no pueden ser cargados." & vbCrLf & "Se intentarán localizar valores guardados para los pozos.", MsgBoxStyle.Information)
                    ' ''    setPozo()
                    ' ''    'borra archivos .dat y .cxt
                    ' ''    FileSystemHelper.BorrarArchivo(ArchivoDestino)
                    ' ''    FileSystemHelper.BorrarArchivo(Archivocontrol)
                    ' ''    Exit Sub
                    ' ''End If

                    ' ''f = New StreamReader(ArchivoDestino)

                    '*** COMENTAR LA SIGUIENTE LINEA  AL  IMPLEMENTAR ARCHIVO ZIP
                    f = New StreamReader(archivo)

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

                        '** 18/10/2012 ****
                        boPremio.ObtieneDatosPremio(codigo, cantidadAciertos, RequiereAciertos, NombrePremio, juego, sorteo, Adic_Tipo)


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
                        '*** los sueldos no viene en el archivo de premios
                        If codigo = 1301006 Then
                            Continue While
                        End If

                        If idJuegoAct = 50 Or idJuegoAct = 51 Then
                            getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Text = CDbl(importePremio / 100)
                            If codigo = 5007001 Then 'la progresion se guarda como cantganadores
                                getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Text = CLng(cantGanadores)
                            End If
                            If idJuegoAct = 50 Then
                                getControlJP("pst" & idJuegoAct & modalidad, "lblPrem" & codigo).Text = NombrePremio
                            End If
                        Else

                            controlTxt = getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo)
                            If IsNothing(controlTxt) Then
                                MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                            Else
                                'para quini 6 y  brinco comun
                                If idJuegoAct = 4 Or idJuegoAct = 13 Then

                                    ''If idJuegoAct = 4 Then
                                    ''    If codigo = 405002 Then
                                    ''        If CLng(cantGanadores) = 0 And CDbl(importePremio / 100) = 0 Then
                                    ''            _Habilita_2Premio_Quini6 = False
                                    ''        Else
                                    ''            _Habilita_2Premio_Quini6 = True
                                    ''        End If
                                    ''        getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    ''        getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    ''        getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    ''        getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    ''    End If
                                    ''End If
                                    getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Text = IIf((cantidadAciertos = 0), "", cantidadAciertos)
                                    getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Enabled = _habilita

                                End If
                                'si no hubo ganadores se muestra cero en lugar del importe del premio
                                PremioPorApuesta = CDbl(importePremio / 100)

                                If idJuegoAct = 13 Or idJuegoAct = 30 Then
                                    If CLng(cantGanadores) = 0 Then
                                        PremioPorApuesta = 0
                                    End If
                                End If

                                getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo).Text = CDbl(importePozo / 100)
                                getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Text = CLng(cantGanadores)
                                getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Text = PremioPorApuesta
                                Try
                                    If idJuegoAct <> 30 Then
                                        getControlJP("pst" & idJuegoAct & modalidad, "lblPrem" & codigo).Text = NombrePremio
                                    End If
                                Catch ex As Exception

                                End Try
                                '*******************************************
                                '*******************************************

                            End If
                        End If


                    End While
                    f.Dispose()
                    '** DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                    '** borra los archivos .dat y .cxt,dejando solo los archivos zip en la carpeta de destino
                    ' ''FileSystemHelper.BorrarArchivo(ArchivoDestino)
                    ' ''FileSystemHelper.BorrarArchivo(Archivocontrol)

                End If
            Else
                'Se trata de cargar desde el archivo sin importar si tiene premios cargados o no
                If MsgBox("Los premios se actualizarán con los datos del archivo." & vbCrLf & "¿Desea continuar?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, MDIContenedor.Text) = MsgBoxResult.No Then
                    Exit Sub
                End If
                '****** DESCOMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                'If Not File.Exists(ArchivoOrigen) Then

                '*** COMENTAR LA SIGUIENTE LINEA AL IMPLEMENTAR ARCHIVO ZIP
                If Not File.Exists(archivo) Then

                    rta = MsgBox("No pudo localizarse el archivo en la ruta por defecto.  Desea seleccionarlo manualmente.", MsgBoxStyle.YesNo, MDIContenedor.Text)

                    If rta = vbYes Then
                        OpenFileD.ShowDialog()

                        If OpenFileD.FileNames.Count = 0 Then
                            Exit Sub
                        Else
                            archivo = OpenFileD.FileNames(0)
                            '**DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                            'ArchivoDestino = OpenFileD.FileNames(0)
                        End If
                    Else
                        MsgBox("Se intentarán localizar valores guardados para los premios.", MsgBoxStyle.Information, MDIContenedor.Text)
                        setPozo()
                        Exit Sub
                    End If
                End If
                '**18/10/2012****
                'DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                If _archivosIguales Then
                    MsgBox("Los parámetros de origen y destino configurados para premios son iguales. No se realiza la carga de premios desde archivo.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End If
                '** copia el zip a la carpeta destino
                ReDim parametrosCopiar(0)
                parametrosCopiar(0) = gralDal.getParametro("INI", "PATH_PREMIOS") & ";" & PathDestino & ";" & NombreArchivo & ".dat"
                FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")

                '*** COMENTAR LA SIGUIENTE LINEA  AL  IMPLEMENTAR ARCHIVO ZIP
                f = New StreamReader(archivo)

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

                    '** 18/10/2012 ****
                    boPremio.ObtieneDatosPremio(codigo, cantidadAciertos, RequiereAciertos, NombrePremio, juego, sorteo, Adic_Tipo)

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
                    '*** los sueldos no viene en el archivo de premios
                    If codigo = 1301006 Then
                        Continue While
                    End If

                    If idJuegoAct = 50 Or idJuegoAct = 51 Then
                        getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Text = CDbl(importePremio / 100)
                        If codigo = 5007001 Then 'la progresion se guarda como cantganadores
                            getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Text = CLng(cantGanadores)
                        End If
                        If idJuegoAct = 50 Then
                            getControlJP("pst" & idJuegoAct & modalidad, "lblPrem" & codigo).Text = NombrePremio
                        End If
                    Else

                        controlTxt = getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo)
                        If IsNothing(controlTxt) Then
                            MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Else
                            'para quini 6 y  brinco comun
                            If idJuegoAct = 4 Or idJuegoAct = 13 Then

                                If idJuegoAct = 4 Then
                                    If codigo = 405002 Then
                                        If CLng(cantGanadores) = 0 And CDbl(importePremio / 100) = 0 Then
                                            _Habilita_2Premio_Quini6 = False
                                        Else
                                            _Habilita_2Premio_Quini6 = True
                                        End If
                                        getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Enabled = _Habilita_2Premio_Quini6
                                        getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo).Enabled = _Habilita_2Premio_Quini6
                                        getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Enabled = _Habilita_2Premio_Quini6
                                        getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Enabled = _Habilita_2Premio_Quini6
                                    End If
                                End If
                                getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Text = IIf((cantidadAciertos = 0), "", cantidadAciertos)
                                getControlJP("pst" & idJuegoAct & modalidad, "txtAciertos" & codigo).Enabled = _habilita

                            End If
                            'si no hubo ganadores se muestra cero en lugar del importe del premio
                            PremioPorApuesta = CDbl(importePremio / 100)

                            If idJuegoAct = 13 Or idJuegoAct = 30 Then
                                If CLng(cantGanadores) = 0 Then
                                    PremioPorApuesta = 0
                                End If
                            End If

                            getControlJP("pst" & idJuegoAct & modalidad, "txtPozo" & codigo).Text = CDbl(importePozo / 100)
                            getControlJP("pst" & idJuegoAct & modalidad, "txtGan" & codigo).Text = CLng(cantGanadores)
                            getControlJP("pst" & idJuegoAct & modalidad, "txtPrem" & codigo).Text = PremioPorApuesta
                            Try
                                If idJuegoAct <> 30 Then
                                    getControlJP("pst" & idJuegoAct & modalidad, "lblPrem" & codigo).Text = NombrePremio
                                End If
                            Catch ex As Exception

                            End Try
                            '*******************************************
                            '*******************************************

                        End If
                    End If


                End While
                f.Dispose()
                '** DESCOMENTAR AL IMPLEMENTAR ARCHIVO ZIP
                '** borra los archivos .dat y .cxt,dejando solo los archivos zip en la carpeta de destino
                ' ''FileSystemHelper.BorrarArchivo(ArchivoDestino)
                ' ''FileSystemHelper.BorrarArchivo(Archivocontrol)

            End If
        Catch ex As Exception
            'MsgBox("Problemas al cargar premios: " & ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub

    Public Sub premioGuardar(ByVal idJuego As Int16, Optional ByVal NoMostrarMsj As Boolean = False)
        Dim lista As List(Of Premio)
        Dim oPremio As Premio
        Dim modalidad As String
        Dim msgRet As String = ""
        Dim msg As String
        Dim boPremio As New PremioBO
        Dim oPremioNuevo As Premio
        Dim rta As Integer
        Dim i As Integer
        Dim tab As TabControl
        Dim oPgmSorteo As New PgmSorteo
        Dim opgmsorteoAux As New PgmSorteo
        Dim opgmSorteoBO As New PgmSorteoBO
        Dim separadordec As String
        Dim _ValorPremio As Double
        Dim GENERAL As New GeneralDAL
        Dim _JuegoActual As Integer
        Dim _cantAciertos As String
        Dim _errorPublicar As Boolean = False
        Dim premiosSueldosVacios As Integer = 0
        Dim _PublicarWebON = Sorteos.Helpers.General.PublicarWebON
        Dim _PublicaExtractosWSRestON = Sorteos.Helpers.General.PublicaExtractosWSRestON
        Dim _PublicaExtractosWSRestOFF = Sorteos.Helpers.General.PublicaExtractosWSRestOFF
        Dim cantidadAciertos As Integer
        Dim RequiereAciertos As Boolean
        Dim NombrePremio As String
        Dim Juego As Integer
        Dim Sorteo As Integer
        Dim Adic_Tipo As Integer

        ' ''Try
        FileSystemHelper.Log(" frmPremios:inicia premioguardar,juego:" & idJuego)
        Me.Cursor = Cursors.WaitCursor
        ' Cierra formularios que pueden traer problemas si permanecen abiertos
        If MDIContenedor.formFinalizar IsNot Nothing Then
            Try
                MDIContenedor.formFinalizar.Close()
                MDIContenedor.formFinalizar.Dispose()
            Catch ex As Exception

            End Try
        End If

        'se  obtiene el separaddor decimal d ela configuracion regional para poder formatear correctamente el pozo
        separadordec = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator
        '** Obtengo el pgmsorteo del juego
        For Each opgmsorteoAux In oPC.PgmSorteos
            If opgmsorteoAux.idJuego = idJuego Then
                oPgmSorteo = opgmsorteoAux
                Exit For
            End If
        Next

        lista = boPremio.getPremio(getIdJuegoActual(), getNroSorteoActual())
        _JuegoActual = getIdJuegoActual()
        i = 0
        For Each oPremio In lista
            modalidad = IIf(Len(CStr(oPremio.idPremio)) = 7, Mid(oPremio.idPremio, 1, 4), Mid(oPremio.idPremio, 1, 3))
            'cuando sea adicional,se borra los que haya en la tabla premio sorteo y se vuelve a cargar con los datos cargados desde el ABM
            If oPremio.idPremio = 405001 Then
                If Not IsNothing(getControlJP("pst" & modalidad, "txtaciertos" & oPremio.idPremio)) Then
                    'si el pimer premio es menor a 6 aciertos solo se ingresa un premio asi que se tiene que borrar los datos del 2 premio si es que existen
                    If getControlJP("pst" & modalidad, "txtaciertos" & oPremio.idPremio).Text < 6 Then
                        LimpiaText2PremioQuini()
                    End If
                End If
                boPremio.BorraPremioAdicional(oPremio)
                Exit For
            End If
        Next
        For Each oPremio In lista
            ' valida los tipos de datos
            i = i + 1

            modalidad = IIf(Len(CStr(oPremio.idPremio)) = 7, Mid(oPremio.idPremio, 1, 4), Mid(oPremio.idPremio, 1, 3))

            ' Para sorteos adicionales controla segun el tipo de sorteo adicional
            ' Si tipo_adic = 1 (primer premio y segundo premio) -> Si codigo es 2,3 o 4 debo controlar como siempre
            Juego = oPremio.idPgmsorteo / 1000000
            Sorteo = oPremio.idPgmsorteo Mod 1000000
            boPremio.ObtieneDatosPremio(oPremio.idPremio, cantidadAciertos, RequiereAciertos, NombrePremio, Juego, Sorteo, Adic_Tipo)


            '** cantidad aciertos,solo controla si esta habilitado
            If Not IsNothing(getControlJP("pst" & modalidad, "txtaciertos" & oPremio.idPremio)) Then
                If getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Enabled Then
                    If getControlJP("pst" & modalidad, "txtaciertos" & oPremio.idPremio).Enabled Then
                        If Not IsNumeric(getControlJP("pst" & modalidad, "txtaciertos" & oPremio.idPremio).Text) Then
                            msg = "Para el premio '" & boPremio.getDescripcionPremio(oPremio.idPremio) & "' se esperaba un número en el campo 'Cantidad Aciertos'." & vbCr
                            Me.Cursor = Cursors.Default
                            Me.Refresh()
                            MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                            tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                            tab.SelectTab("pst" & modalidad)
                            getControlJP("pst" & modalidad, "txtaciertos" & oPremio.idPremio).Focus()
                            Exit Sub
                        End If
                    End If
                    ''End If

                    ''If Not IsNothing(getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio)) Then
                    If Not IsNumeric(getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text) Then
                        msg = "Para el premio '" & boPremio.getDescripcionPremio(oPremio.idPremio) & "' se esperaba un número en el campo 'Pozo'." & vbCr
                        Me.Cursor = Cursors.Default
                        Me.Refresh()
                        MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                        tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                        tab.SelectTab("pst" & modalidad)
                        getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Focus()
                        Exit Sub
                    End If
                    ''End If

                    ''If Not IsNothing(getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio)) Then
                    If getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Enabled Then
                        If Not IsNumeric(getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Text) Then
                            msg = "Para el premio '" & boPremio.getDescripcionPremio(oPremio.idPremio) & "' se esperaba un número en el campo 'Cantidad ganadores'." & vbCr
                            Me.Cursor = Cursors.Default
                            Me.Refresh()
                            MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                            tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                            tab.SelectTab("pst" & modalidad)
                            getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Focus()
                            Exit Sub
                        End If
                    End If


                    If Not IsNothing(getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio)) Then
                        If getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Enabled Then

                            ' valida los tipos de datos
                            If separadordec = "." Then
                                If Not IsNumeric(getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text.Replace(",", ".")) Then
                                    msg = "Para el premio '" & boPremio.getDescripcionPremio(oPremio.idPremio) & "' se esperaba un número en el campo 'Premio por apuesta'." & vbCr
                                    Me.Cursor = Cursors.Default
                                    Me.Refresh()
                                    MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                                    tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                                    tab.SelectTab("pst" & modalidad)
                                    getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Focus()
                                    Exit Sub
                                End If
                            Else
                                If Not IsNumeric(getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text.Replace(".", ",")) Then
                                    msg = "Para el premio '" & boPremio.getDescripcionPremio(oPremio.idPremio) & "' se esperaba un número en el campo 'Premio por apuesta'." & vbCr
                                    Me.Cursor = Cursors.Default
                                    Me.Refresh()
                                    MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                                    tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                                    tab.SelectTab("pst" & modalidad)
                                    getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Focus()
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If

                End If
            End If
            If Sorteos.Helpers.General.Jurisdiccion = "S" Then
                If oPremio.idPremio = 5007001 And Juego = 50 Then
                    If Not IsNumeric(getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Text) Then
                        msg = "La progresión debe ser numérica." & vbCr
                        Me.Cursor = Cursors.Default
                        Me.Refresh()
                        MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                        tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                        tab.SelectTab("pst" & modalidad)
                        getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Focus()
                        Exit Sub
                    End If
                    If (getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Text = 0) Then
                        Dim oSorteoBO As New PgmSorteoBO
                        Dim _vprogresion As Integer = 0
                        Dim _nrosorteo As Integer = getNroSorteoActual()
                        If _nrosorteo = 0 Then
                            _nrosorteo = Mid("00000", 1, 5 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text
                        End If
                        _vprogresion = oSorteoBO.getProgresionLoteria(Juego, _nrosorteo)

                        If _vprogresion = 0 Then
                            msg = "La progresión calculada es cero.Por favor , realice los siguientes pasos." & vbCr & "1 - Revierta la extracción , conservando los numeros" & vbCr & "2 - Confirme nuevamente la extracción" & vbCr & "3 - Dirígase a la pantalla de extracto y verifique que la progresión sea distinta de cero" & vbCr & "4- Si no se corrige,contáctese con soporte técnico."
                            Me.Cursor = Cursors.Default
                            Me.Refresh()
                            MsgBox(msg, MsgBoxStyle.Information, MDIContenedor.Text)
                            tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                            tab.SelectTab("pst" & modalidad)
                            getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Focus()
                            Exit Sub
                        Else
                            txtGan5007001.Text = _vprogresion
                        End If

                    End If
                End If
            End If
        Next

        i = 0
        For Each oPremio In lista
            ' si los tipos de datos son correctos valida según las reglas de negocio
            i = i + 1
            msg = ""

            modalidad = IIf(Len(CStr(oPremio.idPremio)) = 7, Mid(oPremio.idPremio, 1, 4), Mid(oPremio.idPremio, 1, 3))

            If Not IsNothing(getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio)) Then
                If Not ((modalidad = 405 Or modalidad = 1305) And (getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Enabled = False)) Then
                    oPremioNuevo = New Premio
                    oPremioNuevo.importePozo = IIf(Not IsNothing(getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio)), getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text, 0)
                    oPremioNuevo.cantGanadores = IIf(Not IsNothing(getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio)), getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Text, 0)
                    'oPremioNuevo.a = IIf(Not IsNothing(getControlJP("pst" & modalidad, "txtAciertos" & oPremio.idPremio)), getControlJP("pst" & modalidad, "txtAciertos" & oPremio.idPremio).Text, 0)
                    If separadordec = "," Then
                        _ValorPremio = IIf(Not IsNothing(getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio)), getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text.Replace(".", ","), 0)
                    Else
                        _ValorPremio = IIf(Not IsNothing(getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio)), getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text.Replace(",", "."), 0)
                    End If
                    oPremioNuevo.importePremio = _ValorPremio
                    oPremioNuevo.idPremio = oPremio.idPremio
                    If boPremio.validaPremio(oPremioNuevo, msgRet) Then
                        If msgRet <> "" Then
                            Me.Cursor = Cursors.Default
                            Me.Refresh()
                            rta = MsgBox(msgRet & vbCr & vbCr & "¿Continuar guardando?", MsgBoxStyle.YesNo, MDIContenedor.Text)
                            If rta = vbNo Then
                                tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                                tab.SelectTab("pst" & modalidad)
                                getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Focus()
                                Exit Sub
                            End If
                        End If
                    End If
                    ''Else
                    ''    Me.Cursor = Cursors.Default
                    ''    Me.Refresh()
                    ''    MsgBox(msgRet, MsgBoxStyle.Information, MDIContenedor.Text)
                    ''    tab = grpJuegos.Controls("tabJuegos").Controls("pstJuego-" & idJuego).Controls("pnlPozoJuego" & idJuego).Controls("tabPozoJuego" & idJuego)
                    ''    tab.SelectTab("pst" & modalidad)
                    ''    getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Focus()
                    ''    Exit Sub
                End If
            End If
        Next

        For Each oPremio In lista
            modalidad = IIf(Len(CStr(oPremio.idPremio)) = 7, Mid(oPremio.idPremio, 1, 4), Mid(oPremio.idPremio, 1, 3))
            If modalidad = 405 Then
                Dim xxx As String
                xxx = ""
            End If
            oPremioNuevo = New Premio
            oPremioNuevo.idPgmsorteo = getControlJ("txtIdPgmSorteo").Text
            oPremioNuevo.idPremio = oPremio.idPremio
            If Not IsNothing(getControlJP("pst" & modalidad, "txtaciertos" & oPremio.idPremio)) Then
                _cantAciertos = getControlJP("pst" & modalidad, "txtaciertos" & oPremio.idPremio).Text
                If _cantAciertos.Trim = "" Then
                    oPremioNuevo.AciertosPorDef = DBNull.Value.ToString
                Else
                    oPremioNuevo.AciertosPorDef = _cantAciertos
                End If
            Else
                oPremioNuevo.AciertosPorDef = 0
            End If
            If Not IsNothing(getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio)) Then
                If separadordec = "," Then
                    _ValorPremio = IIf(getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text.Replace(".", ",").Trim() = "", 0, getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text.Replace(".", ","))
                    oPremioNuevo.importePozo = IIf(getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text.Replace(".", ",").Trim() = "", 0, getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text.Replace(".", ","))
                Else
                    oPremioNuevo.importePozo = IIf(getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text.Replace(",", ".").Trim() = "", 0, getControlJP("pst" & modalidad, "txtPozo" & oPremio.idPremio).Text.Replace(",", "."))
                End If
            Else
                oPremioNuevo.importePozo = 0
            End If
            If Not IsNothing(getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio)) Then
                oPremioNuevo.cantGanadores = IIf(getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Text.Trim() = "", 0, getControlJP("pst" & modalidad, "txtGan" & oPremio.idPremio).Text)
            Else
                oPremioNuevo.cantGanadores = 0
            End If
            If Not IsNothing(getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio)) Then
                If separadordec = "," Then
                    oPremioNuevo.importePremio = IIf(getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text.Replace(".", ",").Trim() = "", 0, getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text.Replace(".", ","))
                Else
                    oPremioNuevo.importePremio = IIf(getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text.Replace(",", ".").Trim() = "", 0, getControlJP("pst" & modalidad, "txtPrem" & oPremio.idPremio).Text.Replace(",", "."))
                End If
            Else
                oPremioNuevo.importePremio = 0
            End If
            '** 21-03-2012 configuro el campo vacante para los juegos 13,30,en el importe se coloca el importe del pozo,para que se muestre correctamente
            'cuando realiza el envio al display donde si es vacante tiene que mostrar el pozo del premio
            If _JuegoActual = 4 Or _JuegoActual = 13 Or _JuegoActual = 30 Then
                'seteo el campo de vacante
                If oPremioNuevo.cantGanadores = 0 Then
                    oPremioNuevo.vacante = 1
                    '** para la poceada y brinco ,el extracto y el display cuando es vacante se muestra el pozo
                    '** para el quini el extracto cuando es vacante el premio es cero y el display muestra el pozo.
                    If _JuegoActual = 13 Or _JuegoActual = 30 Then
                        oPremioNuevo.importePremio = oPremioNuevo.importePozo
                    End If
                End If
            End If
            '***fin
            If modalidad = 405 Then
                Dim xxx As String
                xxx = ""
            End If
            boPremio.setPremio(oPremioNuevo)

            ' sueldos del brinco 
            If oPremio.idPremio = 1301006 Then
                Sorteo = oPremio.idPgmsorteo Mod 1000000

                If Sorteo < 1000 Then
                    Dim boSueldo As New SueldoBO
                    Dim oSueldo As Sueldo

                    ' elimina los sueldos anteriores
                    boSueldo.eliminarSueldo(oPgmSorteo.idPgmSorteo)

                    Dim cant_sueldos As Integer = 10
                    If GENERAL.getParametro("INI", "CANTIDAD_SUELDO_BR") <> "" Then
                        cant_sueldos = GENERAL.getParametro("INI", "CANTIDAD_SUELDO_BR")
                    End If
                    For i = 1 To cant_sueldos
                        oSueldo = New Sueldo
                        oSueldo.idPgmsorteo = oPremio.idPgmsorteo
                        oSueldo.idPremio = oPremio.idPremio
                        oSueldo.orden = i
                        'el campo de ticket va al campo cupon
                        'al campo apuesta va el left ()30 del valor ingresado en el campo ticket
                        oSueldo.cupon = getControlJP("pst1306", "txtTick" & idJuego & "0600" & i).Text

                        oSueldo.apuesta = ExtraerSoloNros(oSueldo.cupon)

                        oSueldo.agencia = getControlJP("pst1306", "txtAge" & idJuego & "0600" & i).Text
                        oSueldo.localidad = getControlJP("pst1306", "txtLoc" & idJuego & "0600" & i).Text
                        oSueldo.importePremio = GENERAL.getParametro("INI", "IMPORTE_SUELDO_BR")
                        oSueldo.provincia = getControlJP("pst1306", "txtPcia" & idJuego & "0600" & i).Text
                        oSueldo.razonSocial = ""
                        'oSueldo.apuesta = getControlJP("pst1306", "txtTick" & idJuego & "0600" & i).Text

                        '** si los datos de agencia,localidad,provincia y apuesta estan vacíos el dato no se inserta
                        If oSueldo.agencia.Trim <> "" And oSueldo.provincia.Trim <> "" And oSueldo.localidad.Trim <> "" And oSueldo.apuesta.Trim <> "" Then
                            boSueldo.setSueldo(oSueldo)
                        Else
                            premiosSueldosVacios = premiosSueldosVacios + 1
                        End If
                    Next
                    'controla que hay al meos un premio sueldo
                    If premiosSueldosVacios = cant_sueldos Then
                        Me.Cursor = Cursors.Default
                        Me.Refresh()
                        MsgBox("Se deben registrar los premios Sueldo.", MsgBoxStyle.Exclamation, MDIContenedor.Text)
                        Exit Sub
                    End If
                End If 'termina sorteo <1000

            End If ' termina sueldos
        Next

        For Each o In oPC.PgmSorteos
            If o.idJuego = idJuego Then
                oPgmSorteo = opgmSorteoBO.getPgmSorteo(o.idPgmSorteo)
                o = oPgmSorteo
            End If
        Next
        '** control de cambios en premios
        Select Case oPgmSorteo.idJuego
            Case 4
                _HuboCambiosQuini = False
            Case 13
                _HuboCambiosBrinco = False
            Case 30
                _HuboCambiosPoceada = False
            Case 50
                _HuboCambiosLoteria = False
            Case 51
                _HuboCambiosLoteriaChica = False
        End Select

        'AGREGADO FSCOTTA
        If _PublicaExtractosWSRestON = "S" Or _PublicaExtractosWSRestOFF = "S" Then
            Try

                opgmSorteoBO.publicarWEBRest(oPgmSorteo)

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                Me.Refresh()
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " frmPremios.PremioGuardar -> Publicación a web -> " & ex.Message)
                _errorPublicar = True
            End Try
        End If

        '----------------------------------

        FileSystemHelper.Log(" frmPremios:premio guardados ok ,juego:" & idJuego)
        FileSystemHelper.Log(" frmPremios:function premioguardar,intenta publicar a web ,sorteo:" & oPgmSorteo.idPgmSorteo)

        Try
            If _PublicarWebON = "S" Then ' corresponde on line
                '** publico a la web
                opgmSorteoBO.publicarWEB(oPgmSorteo)
                FileSystemHelper.Log(" frmPremios:function premioguardar,publicar a web OK,sorteo:" & oPgmSorteo.idPgmSorteo)
            Else
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Guardar Premios: La publicación on line a la Web no está habilitada. PublicarWebON: " & _PublicarWebON & ".")
            End If
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Me.Refresh()
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " frmPremios.PremioGuardar -> Publicación a web -> " & ex.Message)
            _errorPublicar = True
        End Try
        Me.Cursor = Cursors.Default
        Me.Refresh()
        If idJuego = 30 Or idJuego = 4 Or idJuego = 13 Then
            'MsgBox("No se ha encontrado en el archivo el importe del POZO ESTIMADO PARA PROXIMO SORTEO. Se utilizarÃ¡ el Ãºltimo valor disponible.", MsgBoxStyle.Information, MDIContenedor.Text)
            Try
                Dim idUsuario As Long
                Dim importePozo As Double

                Dim usuarioBo As New UsuarioBO
                Dim oSorteo As New PgmSorteo
                Dim oSorteoBO As New PgmSorteoBO
                Dim pozoBO As New PozoBO

                idUsuario = usuarioBo.getUsuarioID("Sistema")
                For Each o In oPC.PgmSorteos
                    If o.idJuego = idJuego Then
                        oSorteo = oSorteoBO.getPgmSorteo(o.idPgmSorteo)
                        o = oSorteo
                    End If
                Next
                If oSorteo.PozoEstimado <= 0 Then
                    Dim fe As DateTime
                    importePozo = pozoBO.getPozoSugerido(idJuego, fe)
                    FileSystemHelper.Log(" frmPremios:function premioguardar,ejecuto getPozoSugerido,juego:" & idJuego & " pozo obtenido:" & importePozo)
                    If importePozo <= 0 Then
                        MsgBox("No hay importe de Pozo Estimado para el Próximo Sorteo. Deberá registrarlo manualmente a través del menú Interfaces.")
                    Else
                        boPremio.GuardarPozoEstimadoJuego(idJuego, CDbl(importePozo), idUsuario)
                        FileSystemHelper.Log("frmPremios: function premioguardar,guardo pozo estimado OK")
                    End If
                End If
            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & MDIContenedor.usuarioAutenticado.Usuario & " Setpremiodesdearchivozip: error al guardar pozo estimado:" & ex.Message)
            End Try
        End If
        '**30/10/2012 para que no muestre el mensaje cuando se carga los premios desde el load con archivo
        If NoMostrarMsj = False Then

            If _errorPublicar Then
                MsgBox("Se ha realizado la carga de premios correctamente pero se presentó la siguiente situación." & vbCrLf & "- Problemas en la publicación a la Web.Para actualizar la Web , ingrese desde el menú Interfaces.", MsgBoxStyle.Exclamation, MDIContenedor.Text)
            Else
                MsgBox("Se ha realizado la carga de premios correctamente.", MsgBoxStyle.Information, MDIContenedor.Text)
            End If
        End If
        FileSystemHelper.Log("frmPremios: function premioguardar termino OK")
        ' ''Catch ex As Exception

        ' ''    Me.Cursor = Cursors.Default
        ' ''    Me.Refresh()
        ' ''    MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        ' ''    FileSystemHelper.Log(" frmPremios:Problemas function premioguardar :" & ex.Message)

        ' ''End Try
    End Sub

    Private Function getNroSorteoActual() As Integer
        Return Integer.Parse(getControlJ("txtSorteoJuego").Text)
    End Function

#End Region

    Private Sub txtPrem1301001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem1301001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtGan1301002.Enabled And txtGan1301002.Visible Then txtGan1301002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem1301002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem1301002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtGan1301003.Enabled And txtGan1301003.Visible Then txtGan1301003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Function ExtraerSoloNros(ByVal pCadena As String) As String
        Dim _CadenaDevuelta As String
        Dim _CadenaOri As String
        Dim _caracter As String

        Try
            _CadenaDevuelta = ""
            _CadenaOri = pCadena.ToCharArray
            For Each _caracter In _CadenaOri
                If Char.IsDigit(_caracter) = True Then
                    _CadenaDevuelta &= _caracter
                End If
            Next
            'devuelve 30 nros y completa con ceros si es necesario
            '_CadenaDevuelta = _CadenaDevuelta.PadLeft(30, "0")
            '_CadenaDevuelta = Mid(_CadenaDevuelta, 1, 30)
            Return _CadenaDevuelta
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub txtGan1301001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan1301001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtPrem1301001.Enabled And txtPrem1301001.Visible Then txtPrem1301001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan1301002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan1301002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtPrem1301002.Enabled And txtPrem1301002.Visible Then txtPrem1301002.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan1301003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan1301003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtPrem1301003.Enabled And txtPrem1301003.Visible Then txtPrem1301003.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan1301004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan1301004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtPrem1301004.Enabled And txtPrem1301004.Visible Then txtPrem1301004.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan1301005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan1301005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtPrem1301005.Enabled And txtPrem1301005.Visible Then txtPrem1301005.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPrem1301003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem1301003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtGan1301004.Enabled And txtGan1301004.Visible Then txtGan1301004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem1301004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem1301004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtGan1301005.Enabled And txtGan1301005.Visible Then txtGan1301005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem1301005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem1301005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.tabPozoJuego13.TabPages.Count = 3 Then
                tabPozoJuego13.SelectedIndex = 1
                If txtaciertos1305001.Enabled And txtaciertos1305001.Visible Then txtaciertos1305001.Focus()
            Else
                tabPozoJuego13.SelectedIndex = 2
                If txtPcia1306001.Enabled Then txtPcia1306001.Focus()
            End If
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtGan1305001_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtGan1305001.GotFocus
        txtGan1305001.SelectAll()
    End Sub

    Private Sub txtGan1305001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan1305001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtPrem1305001.Enabled Then txtPrem1305001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan1305005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan1305005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtPrem1305005.Enabled And txtPrem1305005.Visible Then txtPrem1305005.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPrem1305001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem1305001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtGan1305005.Enabled And txtGan1305005.Visible Then txtGan1305005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem1305004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtGan1305005.Enabled And txtGan1305005.Visible Then txtGan1305005.Focus()
        End If
    End Sub

    Private Sub txtPrem1305005_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrem1305005.GotFocus
        txtPrem1305005.SelectAll()
    End Sub

    Private Sub txtPrem1305005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem1305005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.tabPozoJuego13.TabPages.Count = 3 Then
                Me.tabPozoJuego13.SelectedIndex = 2
                If txtPcia1306001.Enabled And txtPcia1306001.Visible Then txtPcia1306001.Focus()
            End If
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtLoc1306001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306001.Enabled And txtAge1306001.Visible Then txtAge1306001.Focus()
        End If
    End Sub

    Private Sub txtPcia1306001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306001.Enabled And txtLoc1306001.Visible Then txtLoc1306001.Focus()
        End If
    End Sub

    Private Sub txtPcia13060010_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia13060010.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtLoc13060010.Enabled And txtLoc13060010.Visible Then txtLoc13060010.Focus()
        End If
    End Sub

    Private Sub txtPcia1306002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306002.Enabled And txtLoc1306002.Visible Then txtLoc1306002.Focus()
        End If
    End Sub

    Private Sub txtPcia1306003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306003.Enabled And txtLoc1306003.Visible Then txtLoc1306003.Focus()
        End If
    End Sub

    Private Sub txtPcia1306004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306004.Enabled And txtLoc1306004.Visible Then txtLoc1306004.Focus()
        End If
    End Sub

    Private Sub txtPcia1306005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306005.Enabled And txtLoc1306005.Visible Then txtLoc1306005.Focus()
        End If
    End Sub

    Private Sub txtPcia1306006_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306006.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306006.Enabled And txtLoc1306006.Visible Then txtLoc1306006.Focus()
        End If
    End Sub

    Private Sub txtPcia1306007_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306007.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306007.Enabled And txtLoc1306007.Visible Then txtLoc1306007.Focus()
        End If
    End Sub

    Private Sub txtPcia1306008_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306008.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306008.Enabled And txtLoc1306008.Visible Then txtLoc1306008.Focus()
        End If
    End Sub

    Private Sub txtPcia1306009_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPcia1306009.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtLoc1306009.Enabled And txtLoc1306009.Visible Then txtLoc1306009.Focus()
        End If
    End Sub

    Private Sub txtLoc13060010_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc13060010.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge13060010.Enabled And txtAge13060010.Visible Then txtAge13060010.Focus()
        End If
    End Sub

    Private Sub txtLoc1306002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306002.Enabled And txtAge1306002.Visible Then txtAge1306002.Focus()
        End If
    End Sub

    Private Sub txtLoc1306003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306003.Enabled And txtAge1306003.Visible Then txtAge1306003.Focus()
        End If
    End Sub

    Private Sub txtLoc1306004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306004.Enabled And txtAge1306004.Visible Then txtAge1306004.Focus()
        End If
    End Sub

    Private Sub txtLoc1306005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306005.Enabled And txtAge1306005.Visible Then txtAge1306005.Focus()
        End If
    End Sub

    Private Sub txtLoc1306006_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306006.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306006.Enabled And txtAge1306006.Visible Then txtAge1306006.Focus()
        End If
    End Sub

    Private Sub txtLoc1306007_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306007.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306007.Enabled And txtAge1306007.Visible Then txtAge1306007.Focus()
        End If
    End Sub

    Private Sub txtLoc1306008_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306008.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306008.Enabled And txtAge1306008.Visible Then txtAge1306008.Focus()
        End If
    End Sub

    Private Sub txtLoc1306009_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtLoc1306009.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtAge1306009.Enabled And txtAge1306009.Visible Then txtAge1306009.Focus()
        End If
    End Sub

    Private Sub txtAge1306001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306001.Enabled And txtTick1306001.Visible Then txtTick1306001.Focus()
        End If
    End Sub

    Private Sub txtAge13060010_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge13060010.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick13060010.Enabled And txtTick13060010.Visible Then txtTick13060010.Focus()
        End If
    End Sub

    Private Sub txtAge1306002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306002.Enabled And txtTick1306002.Visible Then txtTick1306002.Focus()
        End If
    End Sub

    Private Sub txtAge1306003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306003.Enabled And txtTick1306003.Visible Then txtTick1306003.Focus()
        End If
    End Sub

    Private Sub txtAge1306004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306004.Enabled And txtTick1306004.Visible Then txtTick1306004.Focus()
        End If
    End Sub

    Private Sub txtAge1306005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306005.Enabled And txtTick1306005.Visible Then txtTick1306005.Focus()
        End If
    End Sub

    Private Sub txtAge1306006_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306006.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306006.Enabled And txtTick1306006.Visible Then txtTick1306006.Focus()
        End If
    End Sub

    Private Sub txtAge1306007_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306007.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306007.Enabled And txtTick1306007.Visible Then txtTick1306007.Focus()
        End If
    End Sub

    Private Sub txtAge1306008_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306008.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306008.Enabled And txtTick1306008.Visible Then txtTick1306008.Focus()
        End If
    End Sub

    Private Sub txtAge1306009_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAge1306009.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtTick1306009.Enabled And txtTick1306009.Visible Then txtTick1306009.Focus()
        End If
    End Sub

    Private Sub txtTick1306001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia1306002.Enabled And txtPcia1306002.Visible Then txtPcia1306002.Focus()
        End If
    End Sub

    Private Sub txtTick13060010_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick13060010.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.btnPremioGuardar13.Focus()
        End If
    End Sub

    Private Sub txtTick1306002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia1306003.Enabled And txtPcia1306003.Visible Then txtPcia1306003.Focus()
        End If
    End Sub

    Private Sub txtTick1306003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia1306004.Enabled And txtPcia1306004.Visible Then txtPcia1306004.Focus()
        End If
    End Sub

    Private Sub txtTick1306004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia1306005.Enabled And txtPcia1306005.Visible Then txtPcia1306005.Focus()
        End If
    End Sub

    Private Sub txtTick1306005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia1306006.Enabled And txtPcia1306006.Visible Then txtPcia1306006.Focus()
        End If
    End Sub

    Private Sub txtTick1306006_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306006.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia1306007.Enabled And txtPcia1306007.Visible Then txtPcia1306007.Focus()
        End If
    End Sub

    Private Sub txtTick1306007_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306007.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia1306008.Enabled And txtPcia1306008.Visible Then txtPcia1306008.Focus()
        End If
    End Sub

    Private Sub txtTick1306008_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306008.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia1306009.Enabled And txtPcia1306009.Visible Then txtPcia1306009.Focus()
        End If
    End Sub

    Private Sub txtTick1306009_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTick1306009.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPcia13060010.Enabled And txtPcia13060010.Visible Then txtPcia13060010.Focus()
        End If
    End Sub

    Private Sub txtGan3001001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan3001001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem3001001.Enabled And txtPrem3001001.Visible Then txtPrem3001001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan3001002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan3001002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem3001002.Enabled And txtPrem3001002.Visible Then txtPrem3001002.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan3001003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan3001003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem3001003.Enabled And txtPrem3001003.Visible Then txtPrem3001003.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan3001004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan3001004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem3001004.Enabled And txtPrem3001004.Visible Then txtPrem3001004.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPrem3001001_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPrem3001001.GotFocus
        txtPrem3001001.SelectAll()
    End Sub

    Private Sub txtPrem3001001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem3001001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan3001002.Enabled And txtGan3001002.Visible Then txtGan3001002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem3001002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem3001002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan3001003.Enabled And txtGan3001003.Visible Then txtGan3001003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem3001003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem3001003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.btnPremioGuardar30.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5101001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5101001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5101002.Enabled And txtPrem5101002.Visible Then txtPrem5101002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5101002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5101002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5101003.Enabled And txtPrem5101003.Visible Then txtPrem5101003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5101003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5101003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5101004.Enabled And txtPrem5101004.Visible Then txtPrem5101004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5101004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5101004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5101005.Enabled And txtPrem5101005.Visible Then txtPrem5101005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5101005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5101005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            tabPozoJuego51.SelectedIndex = 1
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5102001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5102001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5102002.Enabled And txtPrem5102002.Visible Then txtPrem5102002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5102002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5102002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5102003.Enabled And txtPrem5102003.Visible Then txtPrem5102003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5102003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5102003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5102004.Enabled And txtPrem5102004.Visible Then txtPrem5102004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5102004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5102004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5102005.Enabled And txtPrem5102005.Visible Then txtPrem5102005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5102005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5102005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            tabPozoJuego51.SelectedIndex = 2
            If Me.txtPrem5103001.Enabled And txtPrem5103001.Visible Then txtPrem5103001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5103001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5103001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5103002.Enabled And txtPrem5103002.Visible Then txtPrem5103002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5103002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5103002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5103003.Enabled And txtPrem5103003.Visible Then txtPrem5103003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5103003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5103003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5103004.Enabled And txtPrem5103004.Visible Then txtPrem5103004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5103004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5103004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5103005.Enabled And txtPrem5103005.Visible Then txtPrem5103005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5103005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5103005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego51.SelectedIndex = 3
            If Me.txtPrem5104001.Enabled And txtPrem5104001.Visible Then txtPrem5104001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5104001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5104001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If btnPremioGuardar51.Enabled Then btnPremioGuardar51.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001002.Enabled And txtPrem5001002.Visible Then txtPrem5001002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001003.Enabled And txtPrem5001003.Visible Then txtPrem5001003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001004.Enabled And txtPrem5001004.Visible Then txtPrem5001004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001005.Enabled And txtPrem5001005.Visible Then txtPrem5001005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001006.Enabled And txtPrem5001006.Visible Then txtPrem5001006.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001006_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001006.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001007.Enabled And txtPrem5001007.Visible Then txtPrem5001007.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001007_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001007.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001008.Enabled And txtPrem5001008.Visible Then txtPrem5001008.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001008_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001008.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001009.Enabled And txtPrem5001009.Visible Then txtPrem5001009.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001009_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001009.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001010.Enabled And txtPrem5001010.Visible Then txtPrem5001010.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001010_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001010.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001011.Enabled And txtPrem5001011.Visible Then txtPrem5001011.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001011_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001011.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001012.Enabled And txtPrem5001012.Visible Then txtPrem5001012.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001012_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001012.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001013.Enabled And txtPrem5001013.Visible Then txtPrem5001013.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001013_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001013.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001014.Enabled And txtPrem5001014.Visible Then txtPrem5001014.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001014_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001014.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001015.Enabled And txtPrem5001015.Visible Then txtPrem5001015.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001015_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001015.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001016.Enabled And txtPrem5001016.Visible Then txtPrem5001016.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001016_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001016.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001017.Enabled And txtPrem5001017.Visible Then txtPrem5001017.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001017_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001017.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001018.Enabled And txtPrem5001018.Visible Then txtPrem5001018.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001018_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001018.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001019.Enabled And txtPrem5001019.Visible Then txtPrem5001019.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001019_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001019.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5001020.Enabled And txtPrem5001020.Visible Then txtPrem5001020.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5001020_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5001020.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            tabPozoJuego50.SelectedIndex = 1
            If Me.txtPrem5002001.Enabled And txtPrem5002001.Visible Then txtPrem5002001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5002001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5002001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5002002.Enabled And txtPrem5002002.Visible Then txtPrem5002002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5002002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5002002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5002003.Enabled And txtPrem5002003.Visible Then txtPrem5002003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5002003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5002003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            tabPozoJuego50.SelectedIndex = 2
            If Me.txtPrem5003001.Enabled And txtPrem5003001.Visible Then txtPrem5003001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5003001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5003001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5003002.Enabled And txtPrem5003002.Visible Then txtPrem5003002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5003002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5003002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5003003.Enabled And txtPrem5003003.Visible Then txtPrem5003003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5003003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5003003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5003004.Enabled And txtPrem5003004.Visible Then txtPrem5003004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5003004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5003004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego50.SelectedIndex = 3
            If Me.txtPrem5004001.Enabled And txtPrem5004001.Visible Then txtPrem5004001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5004001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5004001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5004002.Enabled And txtPrem5004002.Visible Then txtPrem5004002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5004002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5004002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego50.SelectedIndex = 4
            If Me.txtPrem5005001.Enabled And txtPrem5005001.Visible Then txtPrem5005001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5005001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5005001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem5005002.Enabled And txtPrem5005002.Visible Then txtPrem5005002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5005002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5005002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego50.SelectedIndex = 5
            If Me.txtPrem5006001.Enabled And txtPrem5006001.Visible Then txtPrem5006001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5006001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5006001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.tabPozoJuego50.SelectedIndex = 6
            If Me.txtPrem5007001.Enabled And txtPrem5007001.Visible Then txtPrem5007001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem5007001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem5007001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan5007001.Enabled And txtGan5007001.Visible Then txtGan5007001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtGan401001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan401001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem401001.Enabled And txtPrem401001.Visible Then txtPrem401001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan401002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan401002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem401002.Enabled And txtPrem401002.Visible Then txtPrem401002.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan401003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan401003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem401003.Enabled And txtPrem401003.Visible Then txtPrem401003.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan401004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan401004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem401004.Enabled And txtPrem401004.Visible Then txtPrem401004.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPrem401001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem401001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan401002.Enabled And txtGan401002.Visible Then txtGan401002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem401002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem401002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan401003.Enabled And txtGan401003.Visible Then txtGan401003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem401003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem401003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan401004.Enabled And txtGan401004.Visible Then txtGan401004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem401004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem401004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            tabPozoJuego4.SelectedIndex = 1
            If Me.txtGan402001.Enabled And txtGan402001.Visible Then txtGan402001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtGan402001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan402001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem402001.Enabled And txtPrem402001.Visible Then txtPrem402001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan402002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan402002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem402002.Enabled And txtPrem402002.Visible Then txtPrem402002.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan402003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan402003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem402003.Enabled And txtPrem402003.Visible Then txtPrem402003.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan402004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan402004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem402004.Enabled And txtPrem402004.Visible Then txtPrem402004.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPrem402001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem402001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan402002.Enabled And txtGan402002.Visible Then txtGan402002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem402002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem402002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan402003.Enabled And txtGan402003.Visible Then txtGan402003.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem402003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem402003.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan402004.Enabled And txtGan402004.Visible Then txtGan402004.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem402004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem402004.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            tabPozoJuego4.SelectedIndex = 2
            If Me.txtGan403001.Enabled And txtGan403001.Visible Then txtGan403001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtGan403001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan403001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem403001.Enabled And txtPrem403001.Visible Then txtPrem403001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan403002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan403002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem403002.Enabled And txtPrem403002.Visible Then txtPrem403002.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPrem403001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem403001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan403002.Enabled And txtGan403002.Visible Then txtGan403002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem403002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem403002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            tabPozoJuego4.SelectedIndex = 3
            If Me.TxtAciertos407001.Enabled And TxtAciertos407001.Visible Then TxtAciertos407001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem404001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem404001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If tabPozoJuego4.TabPages.Count = 6 Then
                tabPozoJuego4.SelectedIndex = 5
                If Me.txtaciertos405001.Enabled And txtaciertos405001.Visible Then txtaciertos405001.Focus()
            Else
                Me.btnPremioGuardar4.Focus()
            End If
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtGan407001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan407001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem407001.Enabled And txtPrem407001.Visible Then txtPrem407001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan407002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan407002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem407002.Enabled And txtPrem407002.Visible Then txtPrem407002.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPrem407001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem407001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan407002.Enabled And txtGan407002.Visible Then txtGan407002.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem407002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem407002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            tabPozoJuego4.SelectedIndex = 4
            If Me.txtGan404001.Enabled And txtGan404001.Visible Then txtGan404001.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtGan404001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan404001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem404001.Enabled And txtPrem404001.Visible Then txtPrem404001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan405001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan405001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem405001.Enabled And txtPrem405001.Visible Then txtPrem405001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan405002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan405002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem405002.Enabled And txtPrem405002.Visible Then txtPrem405002.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtGan405005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan405005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtPrem405005.Enabled And txtPrem405005.Visible Then txtPrem405005.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPrem405001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem405001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If txtaciertos405002.Enabled Then
                txtaciertos405002.Focus()
            Else
                If txtGan405005.Enabled Then txtGan405005.Focus()
            End If
        Else
            General.SoloNumerosDecimales(sender, e)

        End If
    End Sub

    Private Sub txtPrem405002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem405002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan405005.Enabled And txtGan405005.Visible Then txtGan405005.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtPrem405003_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            'If Me.txtGan405004.Enabled And txtGan405004.Visible Then txtGan405004.Focus()
        End If
    End Sub

    Private Sub txtPrem405004_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            If Me.txtGan405005.Enabled And txtGan405005.Visible Then txtGan405005.Focus()
        End If
    End Sub

    Private Sub txtPrem405005_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrem405005.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.btnPremioGuardar4.Focus()
        Else
            General.SoloNumerosDecimales(sender, e)
        End If
    End Sub

    Private Sub txtGan5007001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtGan5007001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            Me.btnPremioGuardar50.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtaciertos405001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtaciertos405001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            If txtaciertos405001.Text.Trim = "" Then
                MsgBox("Ingrese un valor numérico", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            If txtaciertos405001.Text = 6 Then
                Habilita2PremioQuini(True)
            Else
                Habilita2PremioQuini(False)
            End If
            If txtGan405001.Enabled Then txtGan405001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub Habilita2PremioQuini(ByVal Habilita As Boolean)
        If Habilita Then
            Me.txtaciertos405002.Enabled = True
            Me.txtGan405002.Enabled = True
            Me.txtPrem405002.Enabled = True
        Else

            Me.txtaciertos405002.Enabled = False
            Me.txtGan405002.Enabled = False
            Me.txtPrem405002.Enabled = False
        End If
    End Sub

    Private Sub TxtAciertos407001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtAciertos407001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            If txtGan407001.Text.Trim = "" Then
                MsgBox("Ingrese un valor numérico.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            If txtGan407001.Enabled Then txtGan407001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtaciertos405002_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtaciertos405002.GotFocus
        txtaciertos405002.SelectAll()
    End Sub

    Private Sub txtaciertos405002_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtaciertos405002.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            If txtaciertos405002.Text.Trim = "" Then
                MsgBox("Ingrese un valor numérico", MsgBoxStyle.Information, MDIContenedor.Text)
                If txtaciertos405002.Enabled Then txtaciertos405002.Focus()
                Exit Sub
            End If
            If txtGan405002.Enabled Then txtGan405002.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub LimpiaText2PremioQuini()
        Me.txtaciertos405002.Text = 0
        Me.txtGan405002.Text = 0
        Me.txtPrem405002.Text = 0
    End Sub

    Private Sub txtaciertos1305001_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtaciertos1305001.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then

            If txtGan1305001.Enabled Then txtGan1305001.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
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
        If BtnBuscarConcurso.Enabled Then
            BtnBuscarConcurso.BackgroundImageLayout = ImageLayout.Stretch
            BtnBuscarConcurso.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            BtnBuscarConcurso.BackgroundImageLayout = ImageLayout.Stretch
            BtnBuscarConcurso.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar13_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar13.EnabledChanged
        If btnPremioGuardar13.Enabled Then
            btnPremioGuardar13.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar13.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar13.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar13.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar13_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioGuardar13.MouseDown
        btnPremioGuardar13.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioGuardar13_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar13.MouseHover
        btnPremioGuardar13.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioGuardar13_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar13.MouseLeave
        If btnPremioGuardar13.Enabled Then
            btnPremioGuardar13.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar13.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar13.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar13.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnPremioGuardar30_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar30.EnabledChanged
        If btnPremioGuardar30.Enabled Then
            btnPremioGuardar30.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar30.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar30.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar30.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar30_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioGuardar30.MouseDown
        btnPremioGuardar30.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioGuardar30_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar30.MouseHover
        btnPremioGuardar30.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioGuardar30_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar30.MouseLeave
        If btnPremioGuardar30.Enabled Then
            btnPremioGuardar30.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar30.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar30.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar30.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar4_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar4.EnabledChanged
        If btnPremioGuardar4.Enabled Then
            btnPremioGuardar4.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar4.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar4.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar4.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioGuardar4.MouseDown
        btnPremioGuardar4.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioGuardar4_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar4.MouseHover
        btnPremioGuardar4.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioGuardar4_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar4.MouseLeave
        If btnPremioGuardar4.Enabled Then
            btnPremioGuardar4.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar4.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar4.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar4.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar50_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar50.EnabledChanged
        If btnPremioGuardar50.Enabled Then
            btnPremioGuardar50.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar50.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar50.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar50.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar50_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioGuardar50.MouseDown
        btnPremioGuardar50.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioGuardar50_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar50.MouseHover
        btnPremioGuardar50.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioGuardar50_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar50.MouseLeave
        If btnPremioGuardar50.Enabled Then
            btnPremioGuardar50.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar50.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar50.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar50.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar51_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar51.EnabledChanged
        If btnPremioGuardar51.Enabled Then
            btnPremioGuardar51.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar51.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar51.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar51.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioGuardar51_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioGuardar51.MouseDown
        btnPremioGuardar51.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioGuardar51_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar51.MouseHover
        btnPremioGuardar51.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioGuardar51_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioGuardar51.MouseLeave
        If btnPremioGuardar51.Enabled Then
            btnPremioGuardar51.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar51.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioGuardar51.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioGuardar51.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener13_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener13.EnabledChanged
        If btnPremioObtener13.Enabled Then
            btnPremioObtener13.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener13.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener13.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener13.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener13_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioObtener13.MouseDown
        btnPremioObtener13.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioObtener13_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener13.MouseHover
        btnPremioObtener13.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioObtener13_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener13.MouseLeave
        If btnPremioObtener13.Enabled Then
            btnPremioObtener13.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener13.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener13.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener13.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener30_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener30.EnabledChanged
        If btnPremioObtener30.Enabled Then
            btnPremioObtener30.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener30.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener30.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener30.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener30_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioObtener30.MouseDown
        btnPremioObtener30.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioObtener30_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener30.MouseHover
        btnPremioObtener30.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioObtener30_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener30.MouseLeave
        If btnPremioObtener30.Enabled Then
            btnPremioObtener30.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener30.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener30.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener30.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener4_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener4.EnabledChanged
        If btnPremioObtener4.Enabled Then
            btnPremioObtener4.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener4.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener4.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener4.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener4_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioObtener4.MouseDown
        btnPremioObtener4.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioObtener4_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener4.MouseHover
        btnPremioObtener4.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioObtener4_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener4.MouseLeave
        If btnPremioObtener4.Enabled Then
            btnPremioObtener4.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener4.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener4.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener4.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener50_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener50.EnabledChanged
        If btnPremioObtener50.Enabled Then
            btnPremioObtener50.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener50.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener50.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener50.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener50_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioObtener50.MouseDown
        btnPremioObtener50.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioObtener50_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener50.MouseHover
        btnPremioObtener50.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioObtener50_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener50.MouseLeave
        If btnPremioObtener50.Enabled Then
            btnPremioObtener50.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener50.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener50.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener50.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener51_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener51.EnabledChanged
        If btnPremioObtener51.Enabled Then
            btnPremioObtener51.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener51.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener51.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener51.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPremioObtener51_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPremioObtener51.MouseDown
        btnPremioObtener51.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPremioObtener51_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener51.MouseHover
        btnPremioObtener51.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPremioObtener51_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPremioObtener51.MouseLeave
        If btnPremioObtener51.Enabled Then
            btnPremioObtener51.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener51.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPremioObtener51.BackgroundImageLayout = ImageLayout.Stretch
            btnPremioObtener51.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub CalculaValorApuesta(ByVal txtbox As TextBox)
        Try
            Dim _Nombre As String
            Dim _Codigo As String
            Dim _Apostadores As Integer
            Dim modalidad As String
            Dim idJuegoAct As Integer
            Dim _Pozo As Double
            Dim _Premio_por_Apuesta As Double = 0


            idJuegoAct = getIdJuegoActual()
            _Nombre = txtbox.Name
            _Codigo = Mid(_Nombre, 7, Len(_Nombre)).ToString '6 ocupa la cadena txtgan que es el campo que se utiliza
            _Apostadores = txtbox.Text
            modalidad = IIf(Len(CStr(_Codigo)) = 7, Mid(_Codigo, 1, 4), Mid(_Codigo, 1, 3))
            'obtengo el control del pozo
            If Not IsNothing(getControlJP("pst" & modalidad, "txtPozo" & _Codigo)) Then
                If getControlJP("pst" & modalidad, "txtPozo" & _Codigo).Text.Trim <> "" Then
                    _Pozo = getControlJP("pst" & modalidad, "txtPozo" & _Codigo).Text
                    If _Apostadores = 0 Then
                        _Premio_por_Apuesta = 0
                    Else
                        _Premio_por_Apuesta = System.Math.Round((_Pozo / _Apostadores), 2)
                    End If

                    'obtengo el control de la apuesta
                    If Not IsNothing(getControlJP("pst" & modalidad, "txtPrem" & _Codigo)) Then
                        getControlJP("pst" & modalidad, "txtPrem" & _Codigo).Text = _Premio_por_Apuesta
                    End If
                End If
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub





    Private Sub Validate_Textos(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGan3001001.Validated, txtGan3001002.Validated, txtGan3001003.Validated, txtGan3001004.Validated, txtGan1301001.Validated, txtGan1301002.Validated, txtGan1301003.Validated, txtGan1301004.Validated, txtGan1301005.Validated, txtGan1305001.Validated, txtGan1305005.Validated, txtGan401001.Validated, txtGan401002.Validated, txtGan401003.Validated, txtGan401004.Validated, txtGan402001.Validated, txtGan402002.Validated, txtGan402003.Validated, txtGan402004.Validated, txtGan403001.Validated, txtGan403002.Validated, txtGan407001.Validated, txtGan407002.Validated, txtGan404001.Validated, txtGan405001.Validated, txtGan405002.Validated, txtGan405005.Validated, txtGan5007001.Validated
        CalculaValorApuesta(sender)
    End Sub

    Private Sub CambiosPremios(ByVal txtbox As TextBox)
        Dim idJuegoAct As Integer
        Try
            idJuegoAct = getIdJuegoActual()
            Select Case idJuegoAct
                Case 4
                    _HuboCambiosQuini = True
                Case 13
                    _HuboCambiosBrinco = True
                Case 30
                    _HuboCambiosPoceada = True
                Case 50
                    _HuboCambiosLoteria = True
                Case 51
                    _HuboCambiosLoteriaChica = True
            End Select

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    '**06/11/2012
    'se controla los ganadores (txtganxxx) y los premios(txtpremxxx)
    Private Sub TextChanged_Premios(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtGan3001001.TextChanged, txtGan3001002.TextChanged, txtGan3001003.TextChanged, txtPrem3001001.TextChanged, txtPrem3001002.TextChanged, txtPrem3001003.TextChanged, txtGan1301001.TextChanged, txtGan1301002.TextChanged, txtGan1301003.TextChanged, txtGan1301004.TextChanged, txtGan1301005.TextChanged, txtGan1305001.TextChanged, txtGan1305005.TextChanged, txtGan401001.TextChanged, txtGan401002.TextChanged, txtGan401003.TextChanged, txtGan401004.TextChanged, txtGan402001.TextChanged, txtGan402002.TextChanged, txtGan402003.TextChanged, txtGan402004.TextChanged, txtGan403001.TextChanged, txtGan403002.TextChanged, txtGan407001.TextChanged, txtGan407002.TextChanged, txtGan404001.TextChanged, txtGan405001.TextChanged, txtGan405002.TextChanged, txtGan405005.TextChanged, txtGan5007001.TextChanged, txtPrem401001.TextChanged, txtPrem401002.TextChanged, txtPrem401003.TextChanged, txtPrem401004.TextChanged, txtPrem402001.TextChanged, txtPrem402002.TextChanged, txtPrem402003.TextChanged, txtPrem402004.TextChanged, txtPrem403001.TextChanged, txtPrem403002.TextChanged, txtPrem407001.TextChanged, txtPrem407002.TextChanged, txtPrem404001.TextChanged, txtPrem405001.TextChanged, txtPrem405002.TextChanged, txtPrem405005.TextChanged, txtPrem1301001.TextChanged, txtPrem1301002.TextChanged, txtPrem1301003.TextChanged, txtPrem1301004.TextChanged, txtPrem1301005.TextChanged, txtPrem1305001.TextChanged, txtPrem1305005.TextChanged, txtPrem5001001.TextChanged, txtPrem5001002.TextChanged, txtPrem5001003.TextChanged, txtPrem5001004.TextChanged, txtPrem5001005.TextChanged, txtPrem5001006.TextChanged, txtPrem5001007.TextChanged, txtPrem5001008.TextChanged, txtPrem5001009.TextChanged, txtPrem5001010.TextChanged, txtPrem5001011.TextChanged, txtPrem5001012.TextChanged, txtPrem5001013.TextChanged, txtPrem5001014.TextChanged, txtPrem5001015.TextChanged, txtPrem5001016.TextChanged, txtPrem5001017.TextChanged, txtPrem5001018.TextChanged, txtPrem5001019.TextChanged, txtPrem5001020.TextChanged, txtPrem5002001.TextChanged, txtPrem5002002.TextChanged, txtPrem5002003.TextChanged, txtPrem5003001.TextChanged, txtPrem5003002.TextChanged, txtPrem5003003.TextChanged, txtPrem5003004.TextChanged, txtPrem5004001.TextChanged, txtPrem5004002.TextChanged, txtPrem5005001.TextChanged, txtPrem5005002.TextChanged, txtPrem5006001.TextChanged, txtPrem5007001.TextChanged, txtPrem5101001.TextChanged, txtPrem5101002.TextChanged, txtPrem5101003.TextChanged, txtPrem5101004.TextChanged, txtPrem5101005.TextChanged, txtPrem5102001.TextChanged, txtPrem5102002.TextChanged, txtPrem5102003.TextChanged, txtPrem5102004.TextChanged, txtPrem5102005.TextChanged, txtPrem5103001.TextChanged, txtPrem5103002.TextChanged, txtPrem5103003.TextChanged, txtPrem5103004.TextChanged, txtPrem5103005.TextChanged, txtPrem5104001.TextChanged, txtLoc1306001.TextChanged, txtLoc1306002.TextChanged, txtLoc1306003.TextChanged, txtLoc1306004.TextChanged, txtLoc1306005.TextChanged, txtLoc1306006.TextChanged, txtLoc1306007.TextChanged, txtLoc1306008.TextChanged, txtLoc1306009.TextChanged, txtLoc13060010.TextChanged, txtPcia1306001.TextChanged, txtPcia1306002.TextChanged, txtPcia1306003.TextChanged, txtPcia1306004.TextChanged, txtPcia1306005.TextChanged, txtPcia1306006.TextChanged, txtPcia1306007.TextChanged, txtPcia1306008.TextChanged, txtPcia1306009.TextChanged, txtPcia13060010.TextChanged, txtAge1306001.TextChanged, txtAge1306002.TextChanged, txtAge1306003.TextChanged, txtAge1306004.TextChanged, txtAge1306005.TextChanged, txtAge1306006.TextChanged, txtAge1306007.TextChanged, txtAge1306008.TextChanged, txtAge1306009.TextChanged, txtAge13060010.TextChanged, txtTick1306001.TextChanged, txtTick1306002.TextChanged, txtTick1306003.TextChanged, txtTick1306004.TextChanged, txtTick1306005.TextChanged, txtTick1306006.TextChanged, txtTick1306007.TextChanged, txtTick1306008.TextChanged, txtTick1306009.TextChanged, txtTick13060010.TextChanged
        If Not _cargando Then
            CambiosPremios(sender)
        End If
    End Sub

    '**** carga de extractos a partir de archivo
    Public Sub setExtractoDesdeArchivoZIP(ByVal idjuego As Long)
        Dim oArchivoBoldt As New Sorteos.Bussiness.ArchivoBoldtBO
        Dim _BOExtraccion As New Sorteos.Bussiness.ExtraccionesBO

        Dim boPremio As New PremioBO
        Dim NombrePremio As String = ""
        Dim sorteoAct As String = ""
        '** loteria y loteria chica Boldt generar los premios como poz
        Dim prefijoLoterias As String = General.PrefijoPozo
        Dim NombreArchivoLoterias As String = ""
        Dim ArchivoOrigenExtracto As String = ""
        Dim extractoBoldt As New cExtractoArchivoBoldt
        Dim idpgmconcurso As String = ""
        'Dim _valor As Boolean = True
        Dim pathreportes As String = ""
        Try
            pathreportes = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes
            sorteoAct = Mid("00000", 1, 5 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text
            oArchivoBoldt.ControlarExtractoQuiniBrinco(idjuego, sorteoAct, pathreportes)

            '**se genera el extracto,este sub,genera la posición en extracto,con lo cual se tiene que actualizar esta posicion en el detalle de memoria(_extraccionesDet) para el valor ingresado 
            Try
                If Not _BOExtraccion.GenerarExtractoCompleto(idjuego*1000000+sorteoAct) Then
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & "Fallo GenerarExtractoCompleto en sub setExtractoDesdeArchivoZIP")
                End If
            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & "Fallo GenerarExtractoCompleto en sub setExtractoDesdeArchivoZIP:" & ex.Message)
            End Try

            ''extractoBoldt = oArchivoBoldt.GenerarExtractodesdeArchivo(idjuego, sorteoAct, ArchivoOrigenExtracto)
            ''If Not extractoBoldt Is Nothing Then
            ''    '** inserta el detalle del extracto en las tablas de auditorias
            ''    oArchivoBoldt.InsertaDetalleExtracto_Auditoria(extractoBoldt)
            ''    idpgmconcurso = idjuego & sorteoAct.PadLeft(6, "0")
            ''    '** actualiza la tabla de detalle con los datos de la tabla de extraccionesdet para el concurso seleccionado
            ''    oArchivoBoldt.ActualizaDetalleExtracto_Auditoria(idpgmconcurso)
            ''    '*** controla que los valores del extarcto y de la tabla campo por campo
            ''    If Not oArchivoBoldt.ControlarDetalleExtracto_Auditoria(idpgmconcurso) Then
            ''        _valor = False
            ''    End If
            ''    '*** inserta los datos d ela cabecera en la tabla de auditoria
            ''    oArchivoBoldt.InsertaCabeceraExtracto_Auditoria(ArchivoOrigenExtracto, _valor, extractoBoldt)
            ''    '** si hay diferncias de valores,se muestra un msj al usuario y se actualzian con los datos del extracto de Boldt
            ''    If Not _valor Then
            ''        If MsgBox("Existen diferencias entre los valores cargados en el sistema y los valores generados por Boldt.Se actualizarán los valores con los datos enviados en el Extracto de Boldt.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
            ''            Exit Sub
            ''        End If
            ''        ' se actualiza la tabla de extraccionesdet con los valores del extracto de Boldt y se imprime un nuevo listado
            ''        oArchivoBoldt.Actualiza_ExtraccionesDet_con_ExtractoBoldt(idpgmconcurso)
            ''        'se imprime un listado con los nuevos valores
            ''        ListarExtracciones(idpgmconcurso)
            ''    End If

            ''End If
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " Problemas al cargar extracto: " & ex.Message)
            MsgBox("Problemas al cargar extracto: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub
    Private Sub ListarExtracciones(ByVal _idpgmconcurso As Long)
        Dim PgmBO As New PgmConcursoBO
        Dim dt As DataTable
        Dim ds As New DataSet

        dt = PgmBO.ObtenerDatosListado(_idpgmconcurso)
        ds.Tables.Add(dt)
        'ds.WriteXmlSchema("D:\Listado1.xml")
        Dim path_reporte As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes  '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES"
        Dim reporte As New Listado
        reporte.GenerarListado(ds, path_reporte)
    End Sub
    '*** premios sueldos
    Public Sub setPremioSueldosDesdeArchivoZIP(Optional ByVal ForzarArchivo As Boolean = False)
        Dim idJuegoAct As String
        Dim sorteoAct As String

        Dim idSorteo As String
        Dim idpgmsorteo As Long
        Dim cant_sueldos As Integer = 0
        Dim DesdeArchivo As Boolean = False

        Dim boPremio As New PremioBO
        Dim gralDal As New Sorteos.Data.GeneralDAL
        Dim Sorteodal As New PgmSorteoDAL
        Dim prefijo As String = General.PrefijoSueldo

        Try
            idJuegoAct = Mid("00", 1, 2 - Len(getIdJuegoActual())) & getIdJuegoActual()
            sorteoAct = Mid("000000", 1, 6 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text
            idSorteo = idJuegoAct & sorteoAct
            idpgmsorteo = CLng(idSorteo)
            cant_sueldos = gralDal.getParametro("INI", "CANTIDAD_SUELDO_BR")
            'HG25-4-19 solo entra si elsorteo esmenor a mill
            If sorteoAct >= 1000 Then
                guardaPremios = True
                Exit Sub
            End If

            ' Determina si toma los datos del archivo. Caso contrario solo intentara leer de la base
            DesdeArchivo = False
            If Not Sorteodal.NoTienePremiosSueldosCargados(idpgmsorteo, CInt(idJuegoAct)) Then ' Existen datos en la BD
                If ForzarArchivo Then
                    If MsgBox("Se reemplazarán los PREMIOS SUELDOS existentes por los datos del archivo." & vbCrLf & "¿Desea continuar?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, MDIContenedor.Text) = MsgBoxResult.Yes Then
                        DesdeArchivo = True
                    End If
                End If
            Else ' No existen datos en la BD
                DesdeArchivo = True
            End If

            If DesdeArchivo Then
                CargarSueldosDesdeZip(cant_sueldos, idJuegoAct, sorteoAct, ForzarArchivo)
            Else
                CargarSueldosDesdeBD(cant_sueldos)
            End If
            guardaPremios = True

        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Problemas al cargar premios: " & ex.Message)
            MsgBox("Problemas al cargar premios: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Sub CargarSueldosDesdeBD(ByVal CantidadSueldos As Integer)
        setPozo()
    End Sub

    Private Sub CargarSueldosDesdeZip(ByVal CantidadSueldos As Integer, ByVal idJuegoAct As String, ByVal sorteoAct As String, ByVal ForzarArchivo As Boolean)
        Dim ZipOrigenPredeterminado As String = ""
        Dim ZipOrigen As String = ZipOrigenPredeterminado

        Dim PathDestino As String = ""
        Dim NombreArchivo As String = ""
        Dim ArchivoDatosDestino As String = ""
        Dim ArchivoControlDestino As String = ""

        Dim linea As String
        Dim sorteo As String
        Dim codigo As String
        Dim controlTxt As Control
        Dim gralDal As New Sorteos.Data.GeneralDAL
        Dim boPremio As New PremioBO
        Dim prefijo As String = General.PrefijoSueldo
        Dim Sorteodal As New PgmSorteoDAL
        Dim parametrosCopiar As String()
        Dim _archivosIguales As Boolean = False
        Dim pathOrigen As String
        Dim tiporegistro As String = ""
        Dim orden As Integer
        Dim agencia As String = ""
        Dim ticket As String = ""
        Dim provincia As String = ""
        Dim localidad As String = ""
        Dim razonsoc As String = ""
        Dim agente As String = ""
        Dim subagente As String = ""
        Dim vendedor As String = ""

        ' Obtiene ruta predeterminada 
        pathOrigen = gralDal.getParametro("INI", "PATH_PREMIOS")
        If Not pathOrigen.EndsWith("\") Then
            pathOrigen = pathOrigen & "\"
        End If
        '** obtengo el archivo zip
        ZipOrigen = pathOrigen & prefijo & idJuegoAct.PadLeft(2, "0") & sorteoAct & ".zip"
        ZipOrigenPredeterminado = ZipOrigen

        '** obtengo la ruta  donde se guardan los archivos zip
        PathDestino = General.Path_Premios_Destino
        '** formo el path del archivo destino ,si se deszipeo con exito
        If Not PathDestino.EndsWith("\") Then
            PathDestino = PathDestino & "\"
        End If

        ' Si no esta el archivo en la ruta predeterminada y el usuario hizo click en boton Obtener, pregunto si quiere elegir el mismo el archivo
        If Not File.Exists(ZipOrigenPredeterminado) And ForzarArchivo Then
            If MsgBox("No pudo localizarse el archivo de PREMIOS SUELDO en la ruta predeterminada. ¿Desea seleccionarlo manualmente?.", MsgBoxStyle.YesNo, MDIContenedor.Text) = vbYes Then
                OpenFileD.Filter = "Archivos de Premios Sueldo|*.zip"
                OpenFileD.DefaultExt = "zip,rar"
                OpenFileD.ShowDialog()

                If OpenFileD.FileNames.Count = 0 Then
                    MsgBox("No se seleccionó ningún archivo. Los premios no se actualizarán.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                Else
                    ZipOrigen = OpenFileD.FileNames(0)
                End If
                Try
                    OpenFileD.Dispose()
                Catch ex As Exception
                End Try
            End If
        End If
        If File.Exists(ZipOrigen) Then
            NombreArchivo = ZipOrigen.Substring(InStrRev(ZipOrigen, "\"))
            pathOrigen = ZipOrigen.Substring(0, ZipOrigen.Length - NombreArchivo.Length)
            NombreArchivo = NombreArchivo.Replace(".zip", "")
            ArchivoDatosDestino = PathDestino & NombreArchivo & ".dat"
            ArchivoControlDestino = PathDestino & NombreArchivo & ".cxt"

            'controla que el origen y el destino no sean iguales
            If pathOrigen = PathDestino Then
                MsgBox("Las carpetas origen y destino configuradas para PREMIOS SUELDO son iguales. No se realiza la carga de premios desde archivo. Consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            ReDim parametrosCopiar(0)
            parametrosCopiar(0) = pathOrigen & ";" & PathDestino & ";" & NombreArchivo & ".zip"
            FileSystemHelper.CopiarListaArchivos(parametrosCopiar, ";")

            '' ''** descomprime el archivo a la carpeta destino
            'RL
            FileSystemHelper.Descomprimir(PathDestino, PathDestino & "\" & NombreArchivo & ".zip")

            '** control del archivo contra el archivo de control md5
            If General.CtrMD5Premios = "S" Then
                If Not FileSystemHelper.ControlArchivoMd5(ArchivoDatosDestino, ArchivoControlDestino) Then
                    MsgBox("El archivo " & NombreArchivo & ".dat no coincide con el archivo de control.Los premios sueldos no pueden ser cargados." & vbCrLf & "Se tomarán los últimos valores guardados, si existen.", MsgBoxStyle.Information, MDIContenedor.Text)
                    CargarSueldosDesdeBD(CantidadSueldos)
                    'borra archivos .dat y .cxt
                    FileSystemHelper.BorrarArchivo(ArchivoDatosDestino)
                    FileSystemHelper.BorrarArchivo(ArchivoControlDestino)
                    Exit Sub
                End If
            End If
            Dim f As StreamReader = New StreamReader(ArchivoDatosDestino)
            Dim i As Integer = 1
            Dim ticketList() As String

            While ((Not f.EndOfStream) And (i <= CantidadSueldos))
                linea = f.ReadLine()
                tiporegistro = Mid(linea, 1, 2)
                If CInt(idJuegoAct) <> 13 Then
                    MsgBox("El registro no corresponde al juego actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                    _cargando = False
                    Exit Sub
                End If
                If tiporegistro = "01" Then
                    sorteo = Mid(linea, 6, 6)
                    If CDbl(sorteo) <> CDbl(sorteoAct) Then
                        MsgBox("El registro no corresponde al sorteo actual.", MsgBoxStyle.Information, MDIContenedor.Text)
                        _cargando = False
                        Exit Sub
                    End If
                End If

                If tiporegistro = "02" Then
                    orden = Mid(linea, 3, 2)
                    agente = Mid(linea, 5, 7)
                    subagente = Mid(linea, 12, 3)
                    vendedor = Mid(linea, 15, 3)
                    agencia = agente.PadLeft(7, "0") & "/" & subagente.PadLeft(3, "0") & "/" & vendedor.PadLeft(3, "0")
                    razonsoc = Mid(linea, 18, 50)
                    ticket = Mid(linea, 68, 30)
                    localidad = Mid(linea, 98, 50)
                    provincia = Mid(linea, 148, 50)

                    If InStr(ticket.Trim, ":") > 0 Then
                        ticketList = ticket.Split(":")
                        ticket = ticketList(0).Substring(0, ticketList(0).Length - 3)
                    End If

                    'los controles estan generadoos de la forma txtagen1306001...10 para los sueldos
                    If orden >= 10 Then
                        codigo = "1306" & orden.ToString.PadLeft(4, "0")
                    Else
                        codigo = "1306" & orden.ToString.PadLeft(3, "0")
                    End If

                    controlTxt = getControlJP("pst1306", "txtage" & codigo)

                    If IsNothing(controlTxt) Then
                        MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                    End If
                    controlTxt = getControlJP("pst1306", "txtloc" & codigo)

                    If IsNothing(controlTxt) Then
                        MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                    End If
                    controlTxt = getControlJP("pst1306", "txttick" & codigo)

                    If IsNothing(controlTxt) Then
                        MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                    End If
                    controlTxt = getControlJP("pst1306", "txtpcia" & codigo)

                    If IsNothing(controlTxt) Then
                        MsgBox("El código de premio no es correcto.", MsgBoxStyle.Information, MDIContenedor.Text)
                    End If
                    getControlJP("pst1306", "txtAge" & codigo).Text = agencia.Trim
                    getControlJP("pst1306", "txtLoc" & codigo).Text = localidad.Trim
                    getControlJP("pst1306", "txtPcia" & codigo).Text = provincia.Trim
                    getControlJP("pst1306", "txtTick" & codigo).Text = ticket.Trim
                    i = i + 1
                End If

            End While
            If (Not f.EndOfStream) Then
                MsgBox("Atención: en el archivo hay más de " & CantidadSueldos & "premios sueldos. Si es correcto modifique la configuración del Sistema para que permita la cantidad correcta de premios, cierre el Sistema y vuelva a intentar.", MsgBoxStyle.Information, MDIContenedor.Text)
            End If
            f.Dispose()
            FileSystemHelper.BorrarArchivo(ArchivoDatosDestino)
            FileSystemHelper.BorrarArchivo(ArchivoControlDestino)

        End If

    End Sub
    '*** premios desde ws
    Public Function setPremioDesdeWS(ByVal idJuego As Integer, Optional ByVal forzararchivo As Boolean = False) As Boolean

        Dim idJuegoAct As String
        Dim sorteoAct As String
        Dim boPremio As New PremioBO
        Dim Sorteodal As New PgmSorteoDAL
        Dim idSorteo As String
        Dim idpgmsorteo As Long
        Dim mensaje As String = ""
        Try
            _cargando = True
            idJuegoAct = Mid("00", 1, 2 - Len(getIdJuegoActual())) & getIdJuegoActual()
            sorteoAct = Mid("000000", 1, 6 - Len(getControlJ("txtSorteoJuego").Text)) & getControlJ("txtSorteoJuego").Text

            idSorteo = idJuegoAct & sorteoAct
            '*** pruebas borrar
            Dim boldBO As New ArchivoBoldtBO
            boldBO.Generar_archivosExtracto_y_Control_por_WS(idSorteo)

            '**** fin prueba


            idpgmsorteo = CLng(idSorteo)

            'siempre trata de pasar los archivos
            ' obtiene los PDF de agencias ,provincias para quini , brinco y PF
            If idJuegoAct = 4 Or idJuegoAct = 13 Or idJuegoAct = 30 Then
                Try
                    If Not boPremio.getPDF(idJuegoAct, sorteoAct, mensaje) Then
                        MsgBox("No se pudieron obtener los archivo PDF:" & vbCrLf & mensaje, MsgBoxStyle.Information)
                    End If
                Catch ex As Exception
                    Return False
                    Exit Function
                End Try

            End If

            If forzararchivo = False Then
                If Not Sorteodal.NoTienePremiosCargados(idpgmsorteo, CInt(idJuegoAct)) Then
                    setPozo()
                    Return True
                    Exit Function
                Else
                    '*** hg 03/09/2015 llama al ws
                    Return boPremio.getPremioWS(idpgmsorteo, CInt(idJuegoAct))

                End If
            Else
                Return boPremio.getPremioWS(idpgmsorteo, CInt(idJuegoAct))
            End If

        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Problemas al cargar premios desde WS: " & ex.Message)
            Return False
        End Try

    End Function
   
    Private Sub txtPozo1301001_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtPozo1301001.TextChanged

    End Sub
End Class