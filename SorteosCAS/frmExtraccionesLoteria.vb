Imports sorteos.helpers
Imports sorteos.entities
Imports sorteos.bussiness
'*** digitador***
Imports System.IO.Ports
Imports System.Threading
Imports System.IO


Public Class frmExtraccionesLoteria
    Dim TagPages As Collection
    Dim opgmConcursoBO As PgmConcursoBO
    Dim oPgmConcursos As New List(Of PgmConcurso)
    Dim oPgmConcurso As PgmConcurso
    Dim PgmSorteoBO As PgmSorteoBO
    Dim opgmSorteo As PgmSorteo
    Dim ListaExtraccionesSorteadas As ListaOrdenada(Of cPosicionValorLoterias)
    Dim ListaPestaniaExtracciones As List(Of cPestaniaExtraccionesLoteria)

    Private FechaHoravacia As DateTime = New DateTime(1999, 1, 1) ' para determinar si se cargo una fecha-hora
    Private HoraInicioExtraccionActual As New DateTime
    Private HoraFinExtraccionActual As New DateTime

    Dim extraccionesCargadas As Integer
    Public Carga As Boolean = True
    Dim lsLoterias As ListaOrdenada(Of Loteria)
    Private _estoyEnLoad As Boolean = False
    '*** digitador***
    Dim ValorenPuertoSerie As String = ""
    Public actualiza As MethodInvoker
    Public WithEvents SerialPort As New SerialPort
    Private ReceiveBuffer As String = ""
    Private m_FormDefInstance As ConcursoExtracciones
    Private m_InitializingDefInstance As Boolean
    Private SerialPortClosing As Boolean
    Dim PuertoSerieActual As String
    Dim IngresoDigitador As Boolean = False
    Dim generalBO As New GeneralBO
    Dim ParametroSonido As String
    Dim Sonidohabilitado As Boolean = True
    Dim VariableSonidoDevuelto As Integer = -1
    Dim IdconcursoActual As Integer
    Dim _TecladoHabilitado As Boolean = False
    Dim LetraNormal As Font
    Dim LetraNegrita As Font
    Dim Letra10Normal As Font
    '***
    Dim fechaIniAntesRevertir As Date = FechaHoravacia
    Dim fechaFinAntesRevertir As Date = FechaHoravacia
    Private idLoteriaASeleccionar As Char = ""

    Private IndiceResolucion As Integer = ObtenerIndiceResolucion()

    Dim Modificando As Boolean = False

    Private ultMetodoElegido As Integer = 0

    Private Sub ActualizarExtraccionesEnPanel(ByVal idpgmConcurso As Long, ByRef pn As Panel, ByRef oPgmsorteoLoteria As pgmSorteo_loteria)
        Dim _indice As Integer = Me.IndiceResolucion
        Dim pTop As Integer
        Dim pleftOrden As Integer
        Dim pLeftPos As Integer
        Dim pLeftVal As Integer
        Dim fuente As Font = New Font("Myriad Web Pro", 10, FontStyle.Regular)
        Dim letra As Font = New Font("Myriad Web Pro", 10, FontStyle.Bold)
        Dim LetraNegrita As Font = Me.LetraNegrita

        pTop = 60
        pleftOrden = 35 + _indice
        pLeftPos = 61 + _indice
        pLeftVal = 125 + _indice

        Dim formato As String = CrearFormatoCifras(oPgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm)

        '** genero los textBox de cada extraccion, recorriendo la estructura del detalle de las extracciones
        Dim i As Integer = 1
        'inicializo la variable global de la cantidad de extracciones que se van ingresando
        extraccionesCargadas = 0
        For Each _valorposicion In oPgmsorteoLoteria.Extractos_QNl.Valores
            pn.Controls.Add(CrearEtiqueta("lblorden" & oPgmsorteoLoteria.Loteria.IdLoteria & "-" & i, i, pleftOrden, pTop, 25, fuente, , True))
            pn.Controls.Add(CrearEtiqueta("txtPosModeloid" & oPgmsorteoLoteria.Loteria.IdLoteria & "-" & i, IIf(_valorposicion.Valor = "", "", _valorposicion.Posicion), pLeftPos, pTop, 49, LetraNegrita, True, , True, 1))
            pn.Controls.Add(CrearEtiqueta("txtValorModeloid" & oPgmsorteoLoteria.Loteria.IdLoteria & "-" & i, _valorposicion.Valor.Trim, pLeftVal, pTop, 82, LetraNegrita, True, True, True, 2))
            pTop = pTop + 28
            If _valorposicion.Valor <> "" Then
                extraccionesCargadas = extraccionesCargadas + 1
            End If
            If i = 10 Then
                pleftOrden = 290 + _indice
                pLeftVal = 384 + _indice
                pLeftPos = 320 + _indice
                pTop = 60

                pn.Controls.Add(CrearEtiqueta("lblvalormodelo" & oPgmSorteoLoteria.Loteria.IdLoteria, "NUMERO", 395 + _indice, 35, 68, fuente))
                pn.Controls.Add(CrearEtiqueta("lblposModelo" & oPgmSorteoLoteria.Loteria.IdLoteria, "POS", 320 + _indice, 35, 62, fuente, True))
            End If
            '*** La fecha de inicio de extracción,se setea con la primera extraccion
            i = i + 1
        Next

        ActualizarPestaniaExtracciones(oPgmConcurso.idPgmConcurso, oPgmsorteoLoteria, extraccionesCargadas)


    End Sub

    Public Sub ActualizarPanelExtraccionesJurisdiccion(ByVal ops As PgmSorteo, ByVal idLoteria As Char)
        Dim PgmsorteoLoteria As pgmSorteo_loteria
        Dim _fechainicio As DateTime = FechaHoravacia
        Dim _fechafin As DateTime = FechaHoravacia

        Try
            If Not SeleccionarPestania(idLoteria) Then
                MsgBox("No se localizó Panel para la jurisdicción '" & idLoteria & "'", MsgBoxStyle.Critical, MDIContenedor.Text)
                Exit Sub
            End If
            For Each PgmsorteoLoteria In ops.ExtraccionesLoteria
                If PgmsorteoLoteria.Loteria.IdLoteria = idLoteria Then
                    Dim tp As TabPage
                    Dim tp_index As Integer
                    Dim pn As Panel
                    Dim pn_index As Integer
                    For h As Integer = 0 To TabExtracciones.TabPages.Count - 1
                        If TabExtracciones.TabPages(h).Name = "TabExtraccionesModelo" & idLoteria Then
                            tp = TabExtracciones.TabPages(h)
                            tp_index = h
                            For k As Integer = 0 To tp.Controls.Count - 1
                                If tp.Controls(k).Name = "grdExtraccionesModelo" & idLoteria Then
                                    pn = tp.Controls(k)
                                    pn_index = k
                                    ActualizarExtraccionesEnPanel(ops.idPgmConcurso, pn, PgmsorteoLoteria) ',,_indice,pTop,pleftOrden,pLeftPos,pLeftVal,fuente,letraNegrita
                                    Exit For
                                End If
                            Next
                            '*actualiza fecha del objeto de memoria si corresponde 
                            If ObtenerFechaPestania(PgmsorteoLoteria, ops.idPgmConcurso, _fechainicio, _fechafin) Then
                                If _fechainicio.Date = FechaHoravacia.Date Then
                                    _fechainicio = New DateTime(PgmsorteoLoteria.FechaHoraLoteria.Year, PgmsorteoLoteria.FechaHoraLoteria.Month, PgmsorteoLoteria.FechaHoraLoteria.Day, _fechainicio.Hour, _fechainicio.Minute, _fechainicio.Second)
                                End If
                                If _fechafin.Date = FechaHoravacia.Date Then
                                    _fechafin = New DateTime(PgmsorteoLoteria.FechaHoraLoteria.Year, PgmsorteoLoteria.FechaHoraLoteria.Month, PgmsorteoLoteria.FechaHoraLoteria.Day, _fechafin.Hour, _fechafin.Minute, _fechafin.Second)
                                End If
                                PgmsorteoLoteria.FechaHoraIniReal = _fechainicio
                                PgmsorteoLoteria.FechaHoraFinReal = _fechafin
                                DTPHoraInicioextraccion.Value = _fechainicio
                                DTPHoraFinextraccion.Value = _fechafin
                            End If
                            Exit For
                        End If
                    Next
                    Exit For
                End If
            Next

            HabilitarOperaciones()
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:ActualizarPanelExtraccionesJurisdiccion - Excepcion: " & ex.Message)
            MsgBox("Ha ocurrido un problema al actualizar los datos de la jurisdicción. Cierre y vuelva a abrir la aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Public Sub CrearControles(ByVal ops As PgmSorteo)
        Dim iContador As Integer
        Dim Pestaña As TabPage
        Dim PanelExtracciones As Panel
        Dim Combo As New ComboBox
        Dim Nombre As String
        Dim i As Integer
        Dim Letra As Font
        Dim Fuente As Font
        Dim pLeftPos As Integer
        Dim pLeftVal As Integer
        Dim pleftOrden As Integer
        Dim pTop As Integer
        Dim PgmsorteoLoteria As pgmSorteo_loteria
        Dim _valorposicion As cPosicionValorLoterias
        Dim _indice As Integer
        '**** fecha en memoria
        Dim _fechainicio As Date = Nothing
        Dim _fechafin As Date = Nothing

        Try
            Me.TabExtracciones.Controls.Clear()
            Fuente = New Font("Myriad Web Pro", 10, FontStyle.Regular)
            Letra = New Font("Myriad Web Pro", 10, FontStyle.Bold)
            TagPages = New Collection
            iContador = 0
            _indice = ObtenerIndiceResolucion()
            For Each PgmsorteoLoteria In ops.ExtraccionesLoteria
                pleftOrden = 35 + _indice
                pLeftPos = 61 + _indice
                pLeftVal = 125 + _indice
                pTop = 60
                Pestaña = New TabPage
                PanelExtracciones = New Panel
                PanelExtracciones.Name = "grdExtraccionesModelo" & PgmsorteoLoteria.Loteria.IdLoteria
                PanelExtracciones.BackColor = Color.White
                PanelExtracciones.Left = 5
                PanelExtracciones.Top = 6
                PanelExtracciones.Width = Pestaña.Width - 1
                ' PanelExtracciones.Height = TabExtracciones.Height - 5
                PanelExtracciones.AutoScroll = True
                PanelExtracciones.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
                Pestaña.Controls.Add(PanelExtracciones)

                '** crea la el ingreso de las dupla valor-Pos de acuerdo al tipo de extracción
                PanelExtracciones.Controls.Add(CrearEtiqueta("lblordenextraccionJuego" & PgmsorteoLoteria.Loteria.IdLoteria, "Las extracciones se muestran en :", 10, 10, 220, Fuente))
                PanelExtracciones.Controls.Add(CrearText("txtOrdenextraccionJuego" & PgmsorteoLoteria.Loteria.IdLoteria, "Orden de Extracción", 230, 8, 164, 2, Fuente, True, False, False))

                Dim detalleExtracciones As New Extracto_qnl
                PanelExtracciones.Controls.Add(CrearEtiqueta("lblvalormodelo" & PgmsorteoLoteria.Loteria.IdLoteria, "NUMERO", 129 + _indice, 35, 68, Fuente))
                PanelExtracciones.Controls.Add(CrearEtiqueta("lblposModelo" & PgmsorteoLoteria.Loteria.IdLoteria, "POS", 58 + _indice, 35, 62, Fuente, True))
                Dim formato As String
                formato = CrearFormatoCifras(PgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm)
                '** genero los textBox recorriendo la estructura del detalle de las extracciones

                i = 1
                'inicializo la variable global de la cantidad de extracciones que se van ingresando
                extraccionesCargadas = 0
                For Each _valorposicion In PgmsorteoLoteria.Extractos_QNl.Valores
                    PanelExtracciones.Controls.Add(CrearEtiqueta("lblorden" & PgmsorteoLoteria.Loteria.IdLoteria & "-" & i, i, pleftOrden, pTop, 25, Fuente, , True))
                    'PanelExtracciones.Controls.Add(CrearText("txtPosModeloid" & PgmsorteoLoteria.Loteria.IdLoteria & "-" & i, IIf(_valorposicion.Valor = "", "", _valorposicion.Posicion), pLeftPos, pTop, 49, 1, LetraNegrita, True, True, True, "110, 114, 116"))
                    'PanelExtracciones.Controls.Add(CrearText("txtValorModeloid" & PgmsorteoLoteria.Loteria.IdLoteria & "-" & i, _valorposicion.Valor.Trim, pLeftVal, pTop, 82, 1, LetraNegrita, True, True, True, "110, 114, 116"))
                    PanelExtracciones.Controls.Add(CrearEtiqueta("txtPosModeloid" & PgmsorteoLoteria.Loteria.IdLoteria & "-" & i, IIf(_valorposicion.Valor = "", "", _valorposicion.Posicion), pLeftPos, pTop, 49, LetraNegrita, True, , True, 1))
                    PanelExtracciones.Controls.Add(CrearEtiqueta("txtValorModeloid" & PgmsorteoLoteria.Loteria.IdLoteria & "-" & i, _valorposicion.Valor.Trim, pLeftVal, pTop, 82, LetraNegrita, True, True, True, 2))
                    pTop = pTop + 28
                    If _valorposicion.Valor <> "" Then
                        extraccionesCargadas = extraccionesCargadas + 1
                    End If
                    If i = 10 Then
                        pleftOrden = 290 + _indice
                        pLeftVal = 384 + _indice
                        pLeftPos = 320 + _indice
                        pTop = 60

                        PanelExtracciones.Controls.Add(CrearEtiqueta("lblvalormodelo" & PgmsorteoLoteria.Loteria.IdLoteria, "NUMERO", 395 + _indice, 35, 68, Fuente))
                        PanelExtracciones.Controls.Add(CrearEtiqueta("lblposModelo" & PgmsorteoLoteria.Loteria.IdLoteria, "POS", 320 + _indice, 35, 62, Fuente, True))
                    End If
                    '*** La fecha de inicio de extracción,se setea con la primera extraccion
                    i = i + 1
                Next

                ActualizarPestaniaExtracciones(oPgmConcurso.idPgmConcurso, PgmsorteoLoteria, extraccionesCargadas)

                '*** fin creacion de duplas

                Pestaña.BackColor = Color.White
                Pestaña.Name = "TabExtraccionesModelo" & PgmsorteoLoteria.Loteria.IdLoteria

                Nombre = ""
                Nombre = PgmsorteoLoteria.Loteria.Nombre
                Pestaña.Text = Nombre
                Pestaña.Tag = PgmsorteoLoteria
                iContador = iContador + 1
                TabExtracciones.TabPages.Add(Pestaña)
                '*actualiza fecha del objeto de meoria si corresponde 
                _fechainicio = Nothing
                _fechafin = Nothing
                If ObtenerFechaPestania(PgmsorteoLoteria.Loteria.IdLoteria, oPgmConcurso.idPgmConcurso, _fechainicio, _fechafin) Then
                    If _fechainicio.Date = New Date(1, 1, 1) Or _fechainicio.Date = FechaHoravacia Then
                        _fechainicio = New DateTime(PgmsorteoLoteria.FechaHoraLoteria.Year, PgmsorteoLoteria.FechaHoraLoteria.Month, PgmsorteoLoteria.FechaHoraLoteria.Day, _fechainicio.Hour, _fechainicio.Minute, _fechainicio.Second)
                    End If
                    If _fechafin.Date = New Date(1, 1, 1) Or _fechafin.Date = FechaHoravacia Then
                        _fechafin = New DateTime(PgmsorteoLoteria.FechaHoraLoteria.Year, PgmsorteoLoteria.FechaHoraLoteria.Month, PgmsorteoLoteria.FechaHoraLoteria.Day, _fechafin.Hour, _fechafin.Minute, _fechafin.Second)
                    End If
                    PgmsorteoLoteria.FechaHoraIniReal = _fechainicio
                    PgmsorteoLoteria.FechaHoraFinReal = _fechafin
                End If

            Next

            HabilitarControles()
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:CrearControles - Excepcion: " & ex.Message)
            ' MsgBox("Problema CrearControles:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Sub HabilitarControles(Optional ByVal setearMetodo As Boolean = True)
        Dim pextraccionesCargadas As Integer
        Dim oPgmsorteoLoteria As pgmSorteo_loteria
        Dim Fecha As String
        Dim _cifras As Integer
        Dim _confirmada As Boolean
        Dim _idconcurso As Integer
        Dim _cifrasBD As Integer
        Dim _tieneNroObligatorio As Boolean = False
        Try
            If TabExtracciones.TabPages.Count = 0 Then
                extraccionesCargadas = 0
                Me.btmConfirmar.Enabled = False
                Me.btmModificar.Enabled = False
                Exit Sub
            End If
            If Me.idLoteriaASeleccionar <> "" Then
                SeleccionarPestania(Me.idLoteriaASeleccionar)
                Me.idLoteriaASeleccionar = ""
            End If
            _confirmada = False
            oPgmsorteoLoteria = TabExtracciones.SelectedTab.Tag
            _idconcurso = CboConcurso.SelectedValue
            If _estoyEnLoad Then
                cboJurisdiccion.SelectedValue = oPgmsorteoLoteria.Loteria.IdLoteria
            End If

            'si es montevideo muestro el combo de cifras
            If oPgmsorteoLoteria.Loteria.IdLoteria = "O" Then
                lblCifras.Visible = True
                cboCantCifras.Enabled = True
                cboCantCifras.Visible = True

                If generalBO.LoteriaComenzada(oPgmsorteoLoteria.IdPgmSorteo, oPgmsorteoLoteria.Loteria.IdLoteria, _cifrasBD) Then
                    oPgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm = _cifrasBD
                    SeteaCantCfirasMonetevideo(_cifrasBD)
                    'cboCantCifras.SelectedItem = _cifrasBD
                    cboCantCifras.Enabled = False
                    If _cifrasBD = 3 Then
                        If setearMetodo Then
                            SeteaMetodoSegunValue(4) ' 4 = lectura de archivo
                        End If
                    End If
                Else
                    SeteaCantCfirasMonetevideo(oPgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm)
                    'cboCantCifras.SelectedItem = oPgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm
                    cboCantCifras.Enabled = True
                    If oPgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm = 3 Then
                        If setearMetodo Then
                            SeteaMetodoSegunValue(4)
                        End If
                    End If
                End If
            Else
                If setearMetodo Then
                    SeteaMetodoSegunValue(1)
                End If

                lblCifras.Visible = False
                cboCantCifras.Visible = False
                cboCantCifras.Enabled = False
                End If
                'TtxtCifras.Text = oPgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm

                Fecha = General.Es_Nulo(Of Date)(oPgmsorteoLoteria.Fechaconfirmacion.ToString("dd/MM/yyyy"), "01/01/1999")

            If oPgmsorteoLoteria.Confirmada Then
                _confirmada = True

                If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = False
                txtValorExtraccion2.Enabled = False
                btmModificar.Enabled = False
                btmConfirmar.Enabled = False
                btnPorArchivo.Enabled = False
                DTPHoraFinextraccion.Enabled = False
                DTPHoraFinextraccion.Value = oPgmsorteoLoteria.FechaHoraFinReal
                DTPHoraInicioextraccion.Enabled = False
                DTPHoraInicioextraccion.Value = oPgmsorteoLoteria.FechaHoraIniReal
                BtnActualizarNro.Enabled = False

            Else
                If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = True
                txtValorExtraccion2.Enabled = True
                btmModificar.Enabled = True
                DTPHoraFinextraccion.Enabled = True
                DTPHoraInicioextraccion.Enabled = True
                '*****
                If oPgmsorteoLoteria.RevertidaParcial = 1 Then
                    DTPHoraFinextraccion.Value = oPgmsorteoLoteria.FechaHoraFinReal
                    DTPHoraInicioextraccion.Value = oPgmsorteoLoteria.FechaHoraIniReal
                Else
                    'no esta confirmada ni tampoco fue revertida
                    DTPHoraFinextraccion.Value = oPgmsorteoLoteria.FechaHoraFinReal
                    DTPHoraInicioextraccion.Value = oPgmsorteoLoteria.FechaHoraIniReal

                End If
                If cboMetodoIngreso.SelectedValue = 4 Then
                    btnPorArchivo.Enabled = True
                End If
            End If
            pextraccionesCargadas = ObtenerPestaniaExtracciones(oPgmsorteoLoteria.Loteria.IdLoteria, _idconcurso)

            txtExtractoHasta.Text = 20

            txtValor1Extraccion.MaxLength = oPgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm
            txtValorExtraccion2.MaxLength = oPgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm

            If pextraccionesCargadas < 20 Then
                If pextraccionesCargadas = 0 Then
                    btnRevertirExtraccion.Enabled = False
                    btmModificar.Enabled = False
                Else
                    btmModificar.Enabled = True
                End If
                pextraccionesCargadas = pextraccionesCargadas + 1
                txtordenExtracto.Text = pextraccionesCargadas
                txtValorExtraccion2.Enabled = True
                If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = True

                btmConfirmar.Enabled = False
            ElseIf pextraccionesCargadas = 20 Then
                txtValorExtraccion2.Enabled = False
                If Me.txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = False
                btmModificar.Enabled = False
                txtordenExtracto.Text = pextraccionesCargadas
                If Not _confirmada Then
                    btmModificar.Enabled = True
                    Me.btmConfirmar.Enabled = True
                End If
            End If
            txtordenExtracto.Enabled = False
            If oPgmsorteoLoteria.Loteria.IdLoteria = "N" Then
                ActualizaLetras(oPgmsorteoLoteria)
                If Not _confirmada Then
                    Habilitaletras()
                Else
                    Habilitaletras(True)
                End If
            Else
                limpiaLetras()
                Habilitaletras(True)
            End If

            If NroSorteoObligatorio(lsLoterias, oPgmsorteoLoteria.Loteria.IdLoteria, _cifras) Then
                txtNroSorteoJurisdiccion.MaxLength = _cifras
                txtNroSorteoJurisdiccion.Enabled = True

                If Not _confirmada Then BtnActualizarNro.Enabled = True
                _tieneNroObligatorio = True
            Else
                txtNroSorteoJurisdiccion.Enabled = False
                BtnActualizarNro.Enabled = False
                _tieneNroObligatorio = False
            End If
            Me.txtNroSorteoJurisdiccion.Text = IIf(oPgmsorteoLoteria.NroSorteoLoteria = 0, "", oPgmsorteoLoteria.NroSorteoLoteria)
            cboJurisdiccion.SelectedValue = oPgmsorteoLoteria.Loteria.IdLoteria


            If CboPuertos.Visible Then
                Me.txtValorExtraccion2.Enabled = False
            End If
            If Me.txtNroSorteoJurisdiccion.Enabled And txtNroSorteoJurisdiccion.Text.Trim = "" Then
                txtNroSorteoJurisdiccion.Focus()
            Else
                If Me.txtValor1Extraccion.Visible = False Then
                    If txtValorExtraccion2.Enabled Then txtValorExtraccion2.Focus()
                Else
                    If txtValor1Extraccion.Enabled Then txtValor1Extraccion.Focus()
                End If
            End If
            If btmConfirmar.Enabled And TxtLetra1.Enabled Then
                TxtLetra1.Focus()
            Else
                If btmConfirmar.Enabled Then
                    DTPHoraInicioextraccion.Focus()
                End If
            End If

            'para quitar una pestaña que ya fue confirmada  se tiene que revertir primero
            '** 27/11/2012 si fue confirmada deshabilito el nro de sorteo
            If _confirmada Then
                BtnQuitar.Enabled = False
                txtNroSorteoJurisdiccion.Enabled = False
            Else
                If _tieneNroObligatorio Then txtNroSorteoJurisdiccion.Enabled = True
                BtnQuitar.Enabled = True
            End If
            HabilitarOperaciones()
            If Me.txtNroSorteoJurisdiccion.Enabled And txtNroSorteoJurisdiccion.Text.Trim = "" Then
                txtNroSorteoJurisdiccion.Focus()
            Else
                If Me.txtValor1Extraccion.Visible = False Then
                    If txtValorExtraccion2.Enabled Then txtValorExtraccion2.Focus()
                Else
                    If txtValor1Extraccion.Enabled Then txtValor1Extraccion.Focus()
                End If
            End If
            If btmConfirmar.Enabled And TxtLetra1.Enabled Then
                TxtLetra1.Focus()
            Else
                If btmConfirmar.Enabled Then
                    DTPHoraInicioextraccion.Focus()
                End If
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:HabilitarControles - Excepcion: " & ex.Message)
            ' MsgBox("Ha ocurrido un pro" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub
    Private Function CrearEtiqueta(ByVal pNombre As String, ByVal pTexto As String, ByVal pLef As Integer, ByVal pTop As Integer, ByVal pAncho As Integer, Optional ByVal Fuente As Font = Nothing, Optional ByVal pVisible As Boolean = True, Optional ByVal pDerecha As Integer = -1, Optional ByVal cargaImagen As Boolean = False, Optional ByVal TipoImagen As Integer = -1) As Label
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
            If pDerecha > -1 Then
                Etiqueta.TextAlign = ContentAlignment.MiddleRight
            End If
            If cargaImagen Then
                Select Case TipoImagen
                    Case 1 'posicion
                        Etiqueta.Image = My.Resources.Imagenes.campovalores
                    Case 2 'valor
                        Etiqueta.Image = My.Resources.Imagenes.campovaloresValor
                    Case 3 'hora
                        Etiqueta.Image = My.Resources.Imagenes.campovaloresHora
                End Select

                Etiqueta.TextAlign = ContentAlignment.MiddleCenter
            End If
            Return Etiqueta
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:CrearEtiqueta - Excepcion: " & ex.Message)
            Return Nothing
        End Try
    End Function

    Private Function CrearText(ByVal pNombre As String, ByVal pTexto As String, ByVal pLef As Integer, ByVal pTop As Integer, ByVal pAncho As Integer, ByVal pAlineacion As Integer, Optional ByVal Fuente As Font = Nothing, Optional ByVal pVisible As Boolean = True, Optional ByVal pEnabled As Boolean = False, Optional ByVal pSoloLectura As Boolean = True, Optional ByVal RGB As String = "") As TextBox
        Dim texto As New TextBox
        Dim Rojo As Integer
        Dim Verde As Integer
        Dim Azul As Integer
        Dim Colores() As String
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
            If RGB.Trim <> "" Then
                Colores = RGB.Split(",")
                Rojo = Colores(0)
                Verde = Colores(1)
                Azul = Colores(2)
                texto.ForeColor = Color.FromArgb(Rojo, Verde, Azul)
            End If
            Return texto
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:CrearText - Excepcion: " & ex.Message)
            'MsgBox("Problema CrearText:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Function

    Private Sub frmExtraccionesLoteria_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated

        If Me.txtNroSorteoJurisdiccion.Enabled And txtNroSorteoJurisdiccion.Visible And txtNroSorteoJurisdiccion.Text.Trim.Length = 0 Then
            txtNroSorteoJurisdiccion.Focus()
        Else
            If txtValor1Extraccion.Visible And txtValor1Extraccion.Enabled Then
                txtValor1Extraccion.Focus()
            Else
                If txtValorExtraccion2.Visible And txtValorExtraccion2.Enabled Then
                    txtValorExtraccion2.Focus()
                Else
                    If TxtLetra1.Enabled Then
                        Me.TxtLetra1.Focus()
                    Else
                        If DTPHoraFinextraccion.Enabled Then
                            Me.DTPHoraFinextraccion.Focus()
                        Else
                            Me.btnSalir.Focus()
                        End If
                    End If
                End If
            End If
        End If
    End Sub


    Private Sub frmExtraccionesLoteria_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        With SerialPort
            If .IsOpen Then
                SerialPortClosing = True
                .DiscardInBuffer()
                .Close()
            End If
        End With
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
        MDIContenedor.MIJurisdicciones.Image = My.Resources.Imagenes.jur_normal
    End Sub

    Private Sub frmExtraccionesLoteria_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode.ToString() = "F1") Then
            If btmModificar.Enabled Then
                btmModificar_Click(btmModificar, Nothing)
            End If
        End If
        If (e.KeyCode.ToString() = "F3") Then
            btnCancelar_Click(btnCancelar, Nothing)
        End If
    End Sub

    Private Sub frmExtraccionesLoteria_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.Location = New System.Drawing.Point(0, 0)
        Dim oloteria As New LoteriaBO
        Dim _general As New GeneralBO
        Try
            Me.Location = New System.Drawing.Point(0, 0)
            LetraNormal = New Font("Myriad Web Pro", 11, FontStyle.Regular)
            LetraNegrita = New Font("Myriad Web Pro", 11, FontStyle.Bold)

            Letra10Normal = New Font("Myriad Web Pro", 10, FontStyle.Regular)

            lsLoterias = New ListaOrdenada(Of Loteria)
            opgmConcursoBO = New PgmConcursoBO
            _estoyEnLoad = True
            oPgmConcursos = opgmConcursoBO.getPgmConcursoQuiniela
            MDIContenedor.CerrarHijo = False
            If oPgmConcursos.Count = 0 Then
                MsgBox("No se encontraron concursos en condiciones de ser visualizados.", MsgBoxStyle.Information, MDIContenedor.Text)
                MDIContenedor.CerrarHijo = True
                Exit Sub
            Else
                DeshabilitaControles(True)
            End If
            oPgmConcurso = oPgmConcursos(0)
            opgmSorteo = opgmConcursoBO.ObtenerPgmSorteoQuiniela(oPgmConcurso)
            IdconcursoActual = oPgmConcurso.idPgmConcurso

            'ActualizarControlesPgmConcurso()

            Me.CboConcurso.DisplayMember = "nombre"
            Me.CboConcurso.ValueMember = "idpgmconcurso"
            Me.CboConcurso.DataSource = oPgmConcursos
            '** Combo jurisdicciones
            lsLoterias = oloteria.getLoterias
            Me.cboJurisdiccion.DataSource = lsLoterias
            cboJurisdiccion.DisplayMember = "nombre"
            cboJurisdiccion.ValueMember = "idloteria"
            '** combo de cifras montevideo
            cboCantCifras.Items.Clear()
            cboCantCifras.Items.Add("3")
            cboCantCifras.Items.Add("4")


            '** Combo metodo de Ingreso
            cboMetodoIngreso.DisplayMember = "nombre"
            cboMetodoIngreso.ValueMember = "idmetodoingreso"
            cboMetodoIngreso.DataSource = oPgmConcurso.MetodosIngresoJurisdicciones
            cboMetodoIngreso.DropDownStyle = ComboBoxStyle.DropDownList


            ''If generalBO.ObtenerCarpetaArchivosExtractoOtrasJuris(opgmSorteo.idJuego) <> "C:\" Then
            ''    cboMetodoIngreso.SelectedValue = 4
            ''Else
            ''    cboMetodoIngreso.SelectedValue = 1
            ''End If



            PuertoSerieActual = generalBO.ObtenerPuerto
            CboPuertos.Items.Clear()
            CboPuertos.Items.Add(PuertoSerieActual)
            SeteaComboPuerto(PuertoSerieActual)
            SerialPort = New SerialPort
            With SerialPort
                .DtrEnable = True
                .RtsEnable = True
                .Handshake = Handshake.None
                .ReadBufferSize = 4096
                .RtsEnable = True
                .DtrEnable = True
            End With
            ConfigurarPuertoSerie()
            BtnConectar.Image = My.Resources.Imagenes.conectar.ToBitmap
            ParametroSonido = _general.ObtenerSonido
            If ParametroSonido.Equals("S") Then
                Sonidohabilitado = True
                btnSonido.Image = My.Resources.Imagenes.SinSonido.ToBitmap
                ToolTip1.SetToolTip(btnSonido, "Deshabilitar sonido")
            Else
                Sonidohabilitado = False
                btnSonido.Image = My.Resources.Imagenes.conSonido.ToBitmap
                ToolTip1.SetToolTip(btnSonido, "Habilitar sonido")
            End If

            Carga = False
            cboJurisdiccion_SelectedIndexChanged(sender, e)
            '**
            Dim _psorteoLoteria As pgmSorteo_loteria
            Dim fecha As String
            If TabExtracciones.TabPages.Count > 0 Then
                _psorteoLoteria = TabExtracciones.SelectedTab.Tag
                fecha = Sorteos.Helpers.General.Es_Nulo(Of Date)(_psorteoLoteria.FechaHoraFinReal, "01/01/1999")
                If fecha <> "01/01/1999" Then
                    BtnQuitar.Enabled = False
                End If

            End If

        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:frmExtraccionesLoteria_Load - Excepcion: " & ex.Message)
            MsgBox("Ha ocurrido un problema al cargar la pantalla. Cierre y vuelva a abrir la aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)

        Finally
            _estoyEnLoad = False
        End Try
    End Sub
    Private Sub CompletarValoresExtracciones(ByVal pSorteoLoteria As pgmSorteo_loteria, ByVal pExtracto_QNL_valor As cPosicionValorLoterias, ByVal pModifica As Boolean)

        Dim nombreTextoValor As String
        Dim nombreTextoPos As String
        Dim nombreTextoHora As String
        Dim nombreGrilla As String
        Dim nombreTab As String
        Dim nombreLbl As String
        Dim i As Integer
        Dim NombreText As String
        Dim Cajatexto As TextBox
        Dim Formato As String
        Dim etiqueta As Label
        Dim nombreL As String

        Dim Valores As New cPosicionValorLoterias
        Try
            If pSorteoLoteria.Extractos_QNl.Valores Is Nothing Then
                Exit Sub
            End If
            nombreGrilla = "grdExtraccionesModelo" & pSorteoLoteria.Loteria.IdLoteria
            nombreTab = "TabExtraccionesModelo" & pSorteoLoteria.Loteria.IdLoteria
            nombreTextoValor = "txtValorModeloid" & pSorteoLoteria.Loteria.IdLoteria
            nombreTextoPos = "txtPosModeloid" & pSorteoLoteria.Loteria.IdLoteria
            nombreTextoHora = "txtHoraModeloid" & pSorteoLoteria.Loteria.IdLoteria
            nombreLbl = "lblorden" & pSorteoLoteria.Loteria.IdLoteria
            i = 1


            Formato = ""
            'Formato = CrearFormatoCifras(oCabecera.ModeloExtraccionesDET.cantCifras)
            For Each Valores In pSorteoLoteria.Extractos_QNl.Valores
                If pModifica = True Then 'si es una modificacion pinto todos
                    nombreL = nombreLbl & "-" & i
                    etiqueta = New Label
                    etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(nombreL)
                    etiqueta.Text = i
                    etiqueta.Visible = True


                    NombreText = nombreTextoValor & "-" & i
                    etiqueta = New Label
                    etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                    etiqueta.Text = IIf(Valores.Valor = "", "", Valores.Valor.Trim)
                    etiqueta.TextAlign = ContentAlignment.MiddleCenter
                    etiqueta.Image = My.Resources.Imagenes.campovaloresValor
                    etiqueta.Visible = True

                    NombreText = nombreTextoPos & "-" & i
                    etiqueta = New Label
                    etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                    etiqueta.Text = IIf(Valores.Valor = "", "", Valores.Posicion)
                    etiqueta.TextAlign = ContentAlignment.MiddleCenter
                    etiqueta.Image = My.Resources.Imagenes.campovalores
                    etiqueta.Visible = True

                Else

                    'es un ingreso solo pinto el registro correspondiente
                    If Valores.Posicion = pExtracto_QNL_valor.Posicion Then
                        nombreL = nombreLbl & "-" & i
                        etiqueta = New Label
                        etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(nombreL)
                        etiqueta.Text = i
                        etiqueta.Visible = True


                        NombreText = nombreTextoValor & "-" & i
                        etiqueta = New Label
                        etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                        etiqueta.TextAlign = ContentAlignment.MiddleCenter
                        etiqueta.Image = My.Resources.Imagenes.campovaloresValor
                        etiqueta.Text = IIf(Valores.Valor = "", "", Valores.Valor)


                        NombreText = nombreTextoPos & "-" & i
                        etiqueta = New Label
                        etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                        etiqueta.Text = IIf(Valores.Valor = "", "", Valores.Posicion)
                        etiqueta.Image = My.Resources.Imagenes.campovalores
                        etiqueta.TextAlign = ContentAlignment.MiddleCenter
                        Exit For
                    End If
                End If
                i = i + 1
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:CompletarValoresExtracciones - Excepcion: " & ex.Message)
            'MsgBox("Problema CompletarValoresExtracciones:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub txtValorExtraccion2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValorExtraccion2.GotFocus
        txtValorExtraccion2.SelectAll()
    End Sub

    Private Sub txtValorExtraccion2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValorExtraccion2.KeyPress
        Try
            Dim _modifica As Boolean
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                If btnRevertirExtraccion.Enabled = False Then
                    btnRevertirExtraccion.Enabled = True
                    btmModificar.Enabled = True
                End If
                e.Handled = True
                _modifica = False
                If txtordenExtracto.Enabled Then 'si el orden del extracto esta habilitado es una modificacion
                    _modifica = True
                Else 'un ingreso en la BD es del orden 1 ,los demas son actualizaciones por el formato de la tabla
                    If txtordenExtracto.Text > 1 Then
                        _modifica = True
                    End If
                End If
                If txtValor1Extraccion.Visible Then
                    If txtValor1Extraccion.Text = "" Then
                        MsgBox("El valor no puede ser vacío.", MsgBoxStyle.Information, MDIContenedor.Text)
                        txtValor1Extraccion.Focus()
                        Exit Sub
                    End If
                    If txtValor1Extraccion.Text <> txtValorExtraccion2.Text Then
                        MsgBox("El Valor 1 debe ser igual al Valor 2.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                End If
                If TabExtracciones.TabPages.Count = 0 Then
                    MsgBox("No existen jurisdicciones ingresadas.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End If
                GuardarDatosDB(_modifica)
                If Modificando Then
                    Modificando = False
                End If
                btnCancelar.Enabled = False
                Dim idPestania As Char = cboJurisdiccion.SelectedValue
                If ObtenerPestaniaExtracciones(idPestania, oPgmConcurso.idPgmConcurso) = 20 Then
                    txtValorExtraccion2.Enabled = False
                    If DTPHoraInicioextraccion.Value <> FechaHoravacia And DTPHoraFinextraccion.Value <> FechaHoravacia Then
                        btmConfirmar.Enabled = True
                    End If
                Else
                    If Me.txtValor1Extraccion.Visible Then
                        Me.txtValor1Extraccion.Focus()
                        Exit Sub
                    Else
                        If txtValorExtraccion2.Enabled Then
                            Exit Sub
                        End If
                    End If
                End If

            Else
                General.SoloNumeros(sender, e)
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:txtValorExtraccion2_KeyPress - Excepcion: " & ex.Message)
            'MsgBox("Problema txtValorExtraccion2_KeyPress:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Public Sub GuardarDatosDB(ByVal pModifica As Boolean)
        Dim _BOPgmSorteoLoteria As New pgmSorteo_loteria
        Dim _SorteoLoteriaBO As New PgmSorteoLoteriaBO
        Dim _sorteoLoteria As New pgmSorteo_loteria
        Dim _extraccion_QNL As New Extracto_qnl
        Dim msj As String
        Dim letras(4) As String
        Dim OrdenRepetido As Integer
        Dim _posicion As Integer
        Dim _valor As String
        'Dim _valorFormateado As Integer
        Dim ColumnaMax As Integer = 0
        Dim valor As String
        'NoModificarMetodoIngreso = True
        Dim fechaingreso As Date
        Dim opgmsorteo As New PgmSorteoBO
        Try
            msj = ""
            _sorteoLoteria = TabExtracciones.SelectedTab.Tag
            '** completo las variables que se envia a la funcion de actualizacion
            _posicion = txtordenExtracto.Text

            _valor = txtValorExtraccion2.Text


            If Me.txtValor1Extraccion.Visible Then
                If Me.txtValor1Extraccion.Text.Trim = "" Then
                    MsgBox("El valor no puede ser vacío", MsgBoxStyle.Information, MDIContenedor.Text)
                    txtValor1Extraccion.Focus()
                    Exit Sub
                Else
                    valor = Replace(txtValor1Extraccion.Text, "+", "z")
                    valor = Replace(valor, "-", "z")
                    If Not IsNumeric(valor) Then
                        MsgBox("Ingrese un valor numérico.", MsgBoxStyle.Information, MDIContenedor.Text)
                        txtValor1Extraccion.Focus()
                        Exit Sub
                    End If
                End If
            End If

            If Me.txtValorExtraccion2.Text.Trim = "" Then
                MsgBox("El valor no puede ser vacío", MsgBoxStyle.Information, MDIContenedor.Text)
                If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                Exit Sub
            Else
                valor = Replace(txtValorExtraccion2.Text, "+", "z")
                valor = Replace(valor, "-", "z")
                If Not IsNumeric(valor) Then
                    MsgBox("Ingrese un valor numérico.", MsgBoxStyle.Information, MDIContenedor.Text)
                    If txtValorExtraccion2.Enabled Then
                        Me.txtValorExtraccion2.Focus()
                    Else
                        txtValorExtraccion2.Text = ""
                    End If

                    Exit Sub
                End If
            End If


            If txtordenExtracto.Enabled = False Then 'si no es una modificación no se puede repetir la posición del extracto
                If Not ControlarPosicion(_sorteoLoteria, _posicion, msj) Then
                    MsgBox(msj, MsgBoxStyle.Information, MDIContenedor.Text)
                    If CboPuertos.Visible Then ' si es digitador limpio los textBox ya que se encuentra deshablitado
                        Me.txtValorExtraccion2.Text = ""
                    End If
                    Exit Sub
                End If

            End If

            'controla la cantidad de cifras ingresadas,cuadno es el cero no se controla la cantidad de cifras
            'el 0 se toma como nro completo
            If txtValorExtraccion2.Text <> 0 Then
                If Len(txtValorExtraccion2.Text) <> _sorteoLoteria.Loteria.CifrasIngresadaDesdeForm Then
                    MsgBox("El valor ingresado deber contener (" & _sorteoLoteria.Loteria.CifrasIngresadaDesdeForm & ") cifras", MsgBoxStyle.Information, MDIContenedor.Text)
                    If CboPuertos.Visible Then ' si es digitador limpio los textBox ya que se encuentra deshablitado
                        Me.txtValorExtraccion2.Text = ""
                    End If
                    Exit Sub
                End If
            Else
                Dim formato As String
                formato = CrearFormatoCifras(_sorteoLoteria.Loteria.CifrasIngresadaDesdeForm)
                _valor = Format(txtValorExtraccion2.Text, formato)
            End If
            '** si estan visible  los dos text de valores y posiciones estos deben ser iguales
            If txtValor1Extraccion.Visible Then
                If txtValor1Extraccion.Text <> txtValorExtraccion2.Text Then
                    MsgBox("El Valor 1 debe ser igual al Valor 2.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End If
            End If


            If Me.txtordenExtracto.Enabled = False Then
                ActualizarPestaniaExtracciones(oPgmConcurso.idPgmConcurso, _sorteoLoteria, _posicion)
            End If
            fechaingreso = Now
            If _posicion = 1 Then
                DTPHoraInicioextraccion.Value = fechaingreso
                If opgmsorteo.getEstadoPgmsorteo(_sorteoLoteria.IdPgmSorteo) = 50 Then
                    MsgBox("El sorteo al que desea agregar la jurisdicción ya ha sido confirmado.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Me.Dispose()
                    Me.Close()
                End If
            End If
            '** 19/10/2012 si es uan modficacion nos e modifica la fecha de fin

            If Not txtordenExtracto.Enabled And cboMetodoIngreso.SelectedValue <> 4 Then
                DTPHoraFinextraccion.Value = fechaingreso
            End If

            _sorteoLoteria.Extractos_QNl.Valores = _sorteoLoteria.Extractos_QNl.ActualizarDetalle(_valor, _posicion) 'la posicion siempre se envia,si no esta visible el campo posicion,la posicion es igual al orden de extraccion

            If Not _SorteoLoteriaBO.InsertarActualizarExtracto_QNL(_sorteoLoteria, pModifica) Then
                MsgBox("Problema al insertar datos en el detalle de la extraccion", MsgBoxStyle.Information, MDIContenedor.Text)
            End If

            '** con el ingreso de la primera extraccion se cambia el estado a en sorteo
            HabilitarControles(False) ' rl pruebo
            Dim _ValorPosicion As New cPosicionValorLoterias
            _ValorPosicion.Posicion = _posicion
            _ValorPosicion.Valor = _valor
            CompletarValoresExtracciones(_sorteoLoteria, _ValorPosicion, pModifica)
            'si fue una modificación con modo de digitador,activo el teclado que se deshabilta en la modificación
            If pModifica And CboPuertos.Visible Then
                HabilitarTeclado()
            End If
            LimpiarControles()

        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:GuardarDatosDB - Excepcion: " & ex.Message)
            MsgBox("Ha ocurrido un problema al guardar los datos. Cierre y vuelva a abrir la aplicación. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Function ControlarPosicion(ByVal oSorteoLoteria As pgmSorteo_loteria, ByVal pPosicion As Integer, ByRef Msj As String) As Boolean
        Dim _valores As cPosicionValorLoterias
        Try
            ControlarPosicion = True
            Msj = ""
            'si la cantidad es 0 quiere decir que el top es variable por lo cual no se controla el limite

            If pPosicion > 20 Then
                Msj = "En '" & oSorteoLoteria.Loteria.Nombre & "' solo se permite un máximo de (20) posiciones"
                ControlarPosicion = False
                Exit Function
            End If
            For Each _valores In oSorteoLoteria.Extractos_QNl.Valores
                If _valores.Posicion = pPosicion And _valores.Valor <> "" Then
                    Msj = "La posición ingresada ya existe en los valores ingresado."
                    ControlarPosicion = False
                    Exit Function
                End If
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:ControlarPosicion - Excepcion: " & ex.Message)
            'MsgBox("Problema ControlarPosicion:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    Function ControlarPosicionModificada(ByVal oSorteoLoteria As pgmSorteo_loteria, ByVal pPosicion As Integer, ByRef pOrden As Integer) As Boolean
        Dim _valores As cPosicionValorLoterias
        Try
            ControlarPosicionModificada = True
            For Each _valores In oSorteoLoteria.Extractos_QNl.Valores
                If _valores.Posicion = pPosicion Then
                    pOrden = _valores.Posicion
                    ControlarPosicionModificada = False
                    Exit Function
                End If
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:ControlarPosicionModificada - Excepcion: " & ex.Message)
            'MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub ActualizarPestaniaExtracciones(ByVal idPgmConcurso As Long, ByVal oPgmSorteoLoteria As pgmSorteo_loteria, ByVal Nroextracciones As Integer)
        Dim Pestania As cPestaniaExtraccionesLoteria
        Dim encontrado As Boolean
        Try
            If ListaPestaniaExtracciones Is Nothing Then
                Pestania = New cPestaniaExtraccionesLoteria
                ListaPestaniaExtracciones = New List(Of cPestaniaExtraccionesLoteria)
                Pestania.IdPestania = oPgmSorteoLoteria.Loteria.IdLoteria
                Pestania.NroExtracciones = Nroextracciones
                Pestania.NroConcurso = idPgmConcurso
                Pestania.PreservarFecha = 0
                Pestania.FechaInicio = oPgmSorteoLoteria.FechaHoraIniReal
                Pestania.FechaFin = oPgmSorteoLoteria.FechaHoraFinReal

                ListaPestaniaExtracciones.Add(Pestania)
            Else
                encontrado = False
                For Each Pestania In ListaPestaniaExtracciones
                    If Pestania.IdPestania = oPgmSorteoLoteria.Loteria.IdLoteria And Pestania.NroConcurso = idPgmConcurso Then
                        Pestania.NroExtracciones = Nroextracciones
                        encontrado = True
                        Exit Sub
                    End If
                Next
                If encontrado = False Then
                    Pestania = New cPestaniaExtraccionesLoteria
                    Pestania.IdPestania = oPgmSorteoLoteria.Loteria.IdLoteria
                    Pestania.NroExtracciones = Nroextracciones
                    Pestania.NroConcurso = idPgmConcurso
                    Pestania.PreservarFecha = 0
                    Pestania.FechaInicio = oPgmSorteoLoteria.FechaHoraIniReal
                    Pestania.FechaFin = oPgmSorteoLoteria.FechaHoraFinReal

                    ListaPestaniaExtracciones.Add(Pestania)
                End If
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:ActualizarPestaniaExtracciones - Excepcion: " & ex.Message)
            MsgBox("Ha ocurrido un problema al actualizar los datos en pantalla. Cierre y vuelva a abrir la aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub ActualizarPestaniaExtracciones(ByVal idPgmConcurso As Integer, ByVal idLoteria As Char, ByVal Nroextracciones As Integer)
        Dim Pestania As cPestaniaExtraccionesLoteria
        Dim encontrado As Boolean
        Try
            If ListaPestaniaExtracciones Is Nothing Then
                Pestania = New cPestaniaExtraccionesLoteria
                ListaPestaniaExtracciones = New List(Of cPestaniaExtraccionesLoteria)
                Pestania.IdPestania = idLoteria
                Pestania.NroExtracciones = Nroextracciones
                Pestania.NroConcurso = idPgmConcurso
                Pestania.PreservarFecha = 0
                Pestania.FechaInicio = Nothing
                Pestania.FechaFin = Nothing

                ListaPestaniaExtracciones.Add(Pestania)
            Else
                encontrado = False
                For Each Pestania In ListaPestaniaExtracciones
                    If Pestania.IdPestania = idLoteria And Pestania.NroConcurso = idPgmConcurso Then
                        Pestania.NroExtracciones = Nroextracciones
                        encontrado = True
                        Exit Sub
                    End If
                Next
                If encontrado = False Then
                    Pestania = New cPestaniaExtraccionesLoteria
                    Pestania.IdPestania = idLoteria
                    Pestania.NroExtracciones = Nroextracciones
                    Pestania.NroConcurso = idPgmConcurso
                    Pestania.PreservarFecha = 0
                    Pestania.FechaInicio = Nothing
                    Pestania.FechaFin = Nothing

                    ListaPestaniaExtracciones.Add(Pestania)
                End If
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:ActualizarPestaniaExtracciones - Excepcion: " & ex.Message)
            MsgBox("Ha ocurrido un problema al actualizar los datos en pantalla. Cierre y vuelva a abrir la aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Function ObtenerPestaniaExtracciones(ByVal idLoteria As Char, Optional ByVal pIdPgmConcurso As Integer = -1) As Integer
        Dim Pestania As cPestaniaExtraccionesLoteria
        Try
            If ListaPestaniaExtracciones Is Nothing Then
                ObtenerPestaniaExtracciones = 1
                Exit Function
            Else
                For Each Pestania In ListaPestaniaExtracciones
                    If Pestania.IdPestania = idLoteria And Pestania.NroConcurso = pIdPgmConcurso Then
                        ObtenerPestaniaExtracciones = Pestania.NroExtracciones
                        Exit Function
                    End If
                Next
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:ObtenerPestaniaExtracciones - Excepcion: " & ex.Message)
            'MsgBox("Problema ObtenerPestaniaExtracciones:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
            ObtenerPestaniaExtracciones = 1
        End Try
    End Function
    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            Dim oloteria As pgmSorteo_loteria
            Dim _idconcurso As Integer
            Dim _nroextracciones As Integer = 0
            _idconcurso = CboConcurso.SelectedValue

            '**19/10/2012
            oloteria = TabExtracciones.SelectedTab.Tag
            _nroextracciones = ObtenerPestaniaExtracciones(oloteria.Loteria.IdLoteria, _idconcurso)
            If _nroextracciones <> txtExtractoHasta.Text Then
                _nroextracciones = _nroextracciones + 1
            End If
            Me.txtordenExtracto.Text = _nroextracciones
            'si esta en modo de digitador doble,en la modificación habilito el teclado para que se siga con el ingreso
            Me.txtValor1Extraccion.Enabled = True
            Me.txtValorExtraccion2.Enabled = True
            If CboPuertos.Visible Then
                HabilitarTeclado()
                Me.txtValorExtraccion2.Enabled = False
            End If
            LimpiarControles()
            btnCancelar.Enabled = False
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:btnCancelar_Click - Excepcion: " & ex.Message)
            ' MsgBox("Problema btnCancelar_Click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub txtValor1Extraccion_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValor1Extraccion.GotFocus
        txtValor1Extraccion.SelectAll()
    End Sub



    Private Sub txtValor1Extraccion_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValor1Extraccion.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                If btnRevertirExtraccion.Enabled = False Then
                    btnRevertirExtraccion.Enabled = True
                    btmModificar.Enabled = True
                End If
                e.Handled = True
                SendKeys.Send("{TAB}")
            Else
                General.SoloNumeros(sender, e)
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:txtValor1Extraccion_KeyPress - Excepcion: " & ex.Message)
            'MsgBox("txtValor1Extraccion_KeyPress:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Function CrearFormatoCifras(ByVal CantCifras As Integer) As String
        Dim formato As String
        Dim i As Integer
        formato = ""
        For i = 1 To CantCifras
            formato = formato & "0"
        Next
        CrearFormatoCifras = formato
    End Function

    Private Sub TabExtracciones_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles TabExtracciones.SelectedIndexChanged
        HabilitarControles()

        HabilitaControlMetodIngreso(cboMetodoIngreso.SelectedValue)
    End Sub

    Private Sub cboMetodoIngreso_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMetodoIngreso.SelectedIndexChanged
        Try
            DesconectaPuerto()

            HabilitaControlMetodIngreso(cboMetodoIngreso.SelectedValue)
            LimpiarControles()
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:cboMetodoIngreso_SelectedIndexChanged - Excepcion: " & ex.Message)
            'MsgBox("Problema cboMetodoIngreso:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub HabilitaControlMetodIngreso(ByVal ValorCombo As Integer)
        Try
            Dim _confirmada As Boolean = False
            Dim _cantidad_numeros_cargados As Integer = 0
            Dim nro As String
            Dim oPgmsorteoLoteria As pgmSorteo_loteria

            If TabExtracciones.TabPages.Count > 0 Then
                oPgmsorteoLoteria = TabExtracciones.SelectedTab.Tag
                _confirmada = oPgmsorteoLoteria.Confirmada
                _cantidad_numeros_cargados = ObtenerPestaniaExtracciones(oPgmsorteoLoteria.Loteria.IdLoteria, oPgmConcurso.idPgmConcurso)
            End If

            Me.txtValor1Extraccion.Text = ""
            Me.txtValorExtraccion2.Text = ""

            'nro = CrearFormatoCifras(oCabecera.ModeloExtraccionesDET.cantCifras)
            'nro = 4

            ' DesconectaPuerto()
            Select Case ValorCombo
                Case 1, 3
                    Me.btnPorArchivo.Visible = False

                    lblingreso1.Visible = False
                    IngresoDigitador = False
                    txtValor1Extraccion.Visible = False
                    'acomodo los lbl del ingreso 2
                    lblingreso2.Text = "  INGRESO:"
                    lblValor.Top = 82
                    If ValorCombo = 3 Then
                        lblValor.Top = 85
                        BtnConectar.Visible = True
                        CboPuertos.Visible = True
                        btnSonido.Visible = True
                        lblteclados.Visible = True
                        IngresoDigitador = True
                        SeteaComboPuerto(PuertoSerieActual)

                        txtValorExtraccion2.Enabled = False
                        If _confirmada Or _cantidad_numeros_cargados = 20 Then
                            BtnConectar.Enabled = False
                            CboPuertos.Enabled = False
                            btnSonido.Enabled = False
                        Else
                            BtnConectar.Enabled = True
                            CboPuertos.Enabled = True
                            btnSonido.Enabled = True
                        End If
                    Else
                        BtnConectar.Visible = False
                        CboPuertos.Visible = False
                        btnSonido.Visible = False
                        lblteclados.Visible = False
                        If _confirmada = False And _cantidad_numeros_cargados < 20 Then
                            txtValorExtraccion2.Enabled = True
                        Else
                            txtValorExtraccion2.Enabled = False
                        End If
                    End If

                Case 2 'ingreso doble
                    Me.btnPorArchivo.Visible = False

                    BtnConectar.Visible = False
                    CboPuertos.Visible = False
                    btnSonido.Visible = False
                    lblteclados.Visible = False
                    lblingreso1.Visible = True
                    txtValor1Extraccion.Visible = True
                    If txtordenExtracto.Text = txtExtractoHasta.Text Then
                        txtValor1Extraccion.Enabled = False
                    End If
                    lblingreso2.Text = "INGRESO 2:"
                    lblValor.Left = 99
                    lblValor.Top = 56
                    If (Not _confirmada) And _cantidad_numeros_cargados < 20 Then
                        txtValor1Extraccion.Enabled = True
                        txtValorExtraccion2.Enabled = True
                    Else
                        txtValor1Extraccion.Enabled = False
                        txtValorExtraccion2.Enabled = False
                    End If

                Case 4
                    If TabExtracciones.TabPages.Count > 0 Then
                        If oPgmsorteoLoteria.Loteria.path_extracto.Trim.Length = 0 Then
                            MsgBox("Esta jurisdicción no tiene habilitado este método de ingreso. Intente con otro.")
                            cboMetodoIngreso.SelectedIndex = 0
                            Exit Sub
                        Else
                            If oPgmsorteoLoteria.Loteria.IdLoteria = "O" And cboCantCifras.SelectedItem = "4" Then
                                MsgBox("Este método de ingreso no está habilitado para 4 CIFRAS. Intente con otro.")
                                cboMetodoIngreso.SelectedIndex = 0
                                Exit Sub
                            End If
                        End If
                    End If
                    If _confirmada Then
                        Me.btnPorArchivo.Enabled = False
                    End If

                    Me.btnPorArchivo.Visible = True
                    Me.lblingreso1.Visible = False
                    Me.txtValor1Extraccion.Visible = False

                    DesconectaPuerto()

                    Me.BtnConectar.Visible = False
                    Me.CboPuertos.Visible = False
                    Me.btnSonido.Visible = False
                    lblteclados.Visible = False
                    txtValorExtraccion2.Enabled = False

                    If btnPorArchivo.Visible = True Then
                        If _confirmada Then
                            btnPorArchivo.Enabled = False
                        Else
                            btnPorArchivo.Enabled = True
                        End If
                    End If

                    If txtValorExtraccion2.Text.Trim = "" Then
                        txtValor1Extraccion.Text = ""
                        txtValorExtraccion2.Text = nro
                    End If

                    If _confirmada = False Then
                        txtValorExtraccion2.Enabled = False
                        If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                    Else
                        txtValorExtraccion2.Enabled = False
                        If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                    End If

                    txtValorExtraccion2.SelectAll()
                    If txtValorExtraccion2.Enabled Then txtValorExtraccion2.Focus()

            End Select

        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:HabilitaControlMetodIngreso - Excepcion: " & ex.Message)
            MsgBox("Ha ocurrido un problema al establecer el método de ingreso de nros. Cierre y vuelva a abrir la aplicación e intente nuevamente. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub cboJurisdiccion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboJurisdiccion.SelectedIndexChanged
        If Not Carga Then
            HabilitarControles()
        End If

        ''HabilitarOperaciones()
        'Dim idPestania As Char
        ''Dim _cifras As Integer
        ''Dim nroSorteo As Integer
        ''Dim oLoterias As New pgmSorteo_loteria
        ''Dim _cifrasaux As Integer

        ''Try
        ''    If Carga Then Exit Sub
        ''idPestania = cboJurisdiccion.SelectedValue
        ''    If BuscaPestaniaHabilitada(idPestania, oPgmConcurso.idPgmConcurso) Then
        ''        btnAgregar.Enabled = False
        ''        ' txtNroSorteoJurisdiccion.Enabled = False
        ''        If Me.TabExtracciones.TabPages.Count > 0 Then 'antes de habilitar la pestana.ver si esta confirmada
        ''            '** 27/11/2012 - si esta confirmada deshabilito el ingreso del nro de sorteo
        ''            oLoterias = TabExtracciones.SelectedTab.Tag
        ''            txtNroSorteoJurisdiccion.Text = IIf(oLoterias.NroSorteoLoteria = 0, "", oLoterias.NroSorteoLoteria)
        ''            If oLoterias.FechaHoraFinReal = "01/01/1999" Or oLoterias.RevertidaParcial = 1 Then
        ''                ''BtnQuitar.Enabled = True
        ''                txtNroSorteoJurisdiccion.Enabled = True
        ''            Else
        ''                txtNroSorteoJurisdiccion.Enabled = False
        ''            End If
        ''            If idPestania <> "O" Then
        ''                cboCantCifras.Enabled = False
        ''            Else
        ''                If generalBO.LoteriaComenzada(oLoterias.IdPgmSorteo, oLoterias.Loteria.IdLoteria, _cifrasaux) Then
        ''                    cboCantCifras.Enabled = False
        ''                Else
        ''                    cboCantCifras.Enabled = True
        ''                End If
        ''            End If
        ''        End If
        ''    Else '27/11/2012 esta parate no se tendrias que ejecutar mas
        ''        txtNroSorteoJurisdiccion.Text = ""
        ''        btnAgregar.Enabled = True
        ''        BtnQuitar.Enabled = False
        ''        If idPestania <> "O" Then
        ''            cboCantCifras.Enabled = False
        ''        Else
        ''            cboCantCifras.Enabled = True
        ''        End If
        ''    End If

        ''    If NroSorteoObligatorio(lsLoterias, idPestania, _cifras, nroSorteo) Then
        ''        txtNroSorteoJurisdiccion.MaxLength = _cifras
        ''        'txtNroSorteoJurisdiccion.Enabled = btnAgregar.Enabled
        ''        If txtNroSorteoJurisdiccion.Enabled Then
        ''            txtNroSorteoJurisdiccion.Focus()
        ''            '** 27/11/2012
        ''            ''ElseIf btnAgregar.Enabled Then
        ''            ''    btnAgregar.Focus()
        ''            ''ElseIf BtnQuitar.Enabled Then
        ''            ''    BtnQuitar.Focus()
        ''        End If
        ''    Else
        ''        txtNroSorteoJurisdiccion.Enabled = False
        ''    End If
        ''Catch ex As Exception
        ''MsgBox("Problema al visualizar detalle de la Jurisdicción:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        ''End Try
    End Sub

    Private Sub HabilitarOperaciones()

        Dim idPestania As Char
        Dim _cifras As Integer ' Cantidad de cifras a cargar 
        Dim nroSorteo As Integer ' Nro de sorteo propio de cada jurisdiccion
        Dim oLoteria As New pgmSorteo_loteria
        Dim _cifrasaux As Integer

        Try
            idPestania = cboJurisdiccion.SelectedValue
            TabExtracciones.SelectedIndex = SeleccionarPestania(idPestania, opgmSorteo.idPgmSorteo, oLoteria)
            txtNroSorteoJurisdiccion.Text = IIf(oLoteria.NroSorteoLoteria = 0, "", oLoteria.NroSorteoLoteria)

            ' Primero deshabilito todo
            btnAgregar.Enabled = _
            btnAgregar.Visible = _
            BtnQuitar.Enabled = _
            BtnQuitar.Visible = _
            txtNroSorteoJurisdiccion.Enabled = _
            BtnActualizarNro.Enabled = _
            cboCantCifras.Enabled = _
            cboMetodoIngreso.Enabled = _
            txtordenExtracto.Enabled = _
            btnCancelar.Enabled = _
            btmModificar.Enabled = _
            TxtLetra1.Enabled = _
            TxtLetra2.Enabled = _
            TxtLetra3.Enabled = _
            TxtLetra4.Enabled = _
            DTPHoraInicioextraccion.Enabled = _
            DTPHoraFinextraccion.Enabled = _
            btnRevertirExtraccion.Enabled = _
            btmConfirmar.Enabled = False

            ' Si el Concurso no esta confirmado veo que habilito segun el estado del concurso y la jurisdiccion actual
            If opgmSorteo.idEstadoPgmConcurso < 50 And opgmSorteo.idEstadoPgmConcurso > 10 Then
                If oLoteria.Confirmada Then
                    btnRevertirExtraccion.Enabled = True
                Else
                    If NroSorteoObligatorio(lsLoterias, idPestania, _cifras, nroSorteo) Then
                        txtNroSorteoJurisdiccion.Enabled = True
                        BtnActualizarNro.Enabled = True
                    End If
                    If idPestania = "O" And Not (generalBO.LoteriaComenzada(oLoteria.IdPgmSorteo, idPestania, _cifrasaux)) Then
                        cboCantCifras.Enabled = True
                    End If
                    cboMetodoIngreso.Enabled = True

                    HabilitaControlMetodIngreso(cboMetodoIngreso.SelectedValue)
                    If idPestania = "N" Then
                        TxtLetra1.Enabled = _
                        TxtLetra2.Enabled = _
                        TxtLetra3.Enabled = _
                        TxtLetra4.Enabled = True
                    End If

                    DTPHoraInicioextraccion.Enabled = True
                    DTPHoraFinextraccion.Enabled = True
                    If ObtenerPestaniaExtracciones(idPestania, oPgmConcurso.idPgmConcurso) = 20 And DTPHoraInicioextraccion.Value <> FechaHoravacia And DTPHoraFinextraccion.Value <> FechaHoravacia Then
                        btmConfirmar.Enabled = True
                    End If
                End If
            End If

        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:HabilitarOperaciones - Excepcion: " & ex.Message)
            MsgBox("Ocurrió un problema al preparar la pantalla de carga. Cierre y vuelva a abrir la aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Function BuscaPestaniaHabilitada(ByVal pidloteria As Char, ByVal idpgmConcurso As Integer) As Boolean
        Dim pestania As New cPestaniaExtraccionesLoteria
        Dim i As Integer
        Try
            BuscaPestaniaHabilitada = False
            If ListaPestaniaExtracciones Is Nothing Then Exit Function
            i = 0
            For Each pestania In ListaPestaniaExtracciones
                If pestania.IdPestania = pidloteria And pestania.NroConcurso = idpgmConcurso Then
                    If SeleccionarPestania(pidloteria) Then
                        BuscaPestaniaHabilitada = True
                        Exit Function
                    End If

                End If
                i = i + 1
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:BuscaPestaniaHabilitada - Excepcion: " & ex.Message)
            'MsgBox("Problema BuscaPestaniaHabilitada:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Function QuitarTabPage(ByVal idLoteria As Char, ByVal idPgmSorteo As Long) As Integer
        Try
            Dim oloteria As pgmSorteo_loteria
            Dim tabp As TabPage
            Dim i As Integer

            i = 0
            QuitarTabPage = -1
            For Each tabp In TabExtracciones.TabPages
                oloteria = tabp.Tag

                If oloteria.Loteria.IdLoteria = idLoteria And oloteria.IdPgmSorteo = idPgmSorteo Then
                    TabExtracciones.TabPages.Remove(tabp)
                    QuitarTabPage = i
                    Exit Function
                End If
                i = i + 1
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:QuitarTabPage - Excepcion: " & ex.Message)
            ' MsgBox("Problema al quitar pestaña: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
            QuitarTabPage = -1
        End Try
    End Function

    Private Function SeleccionarPestania(ByVal idLoteria As Char, ByVal idPgmSorteo As Long, ByRef oLoteria As pgmSorteo_loteria) As Integer
        Try
            Dim tabp As TabPage
            Dim i As Integer

            i = 0
            SeleccionarPestania = Nothing
            For Each tabp In TabExtracciones.TabPages
                oLoteria = tabp.Tag

                If oLoteria.Loteria.IdLoteria = idLoteria And oLoteria.IdPgmSorteo = idPgmSorteo Then
                    TabExtracciones.SelectedIndex = i
                    SeleccionarPestania = i
                    Exit Function
                End If
                i = i + 1
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:SeleccionarPestania - Excepcion: " & ex.Message)
            'MsgBox("Problema SeleccionarPestania:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
            SeleccionarPestania = -1
        End Try
    End Function

    Private Function SeleccionarPestania(ByVal index As Char) As Boolean
        Try
            Dim tabp As TabPage
            Dim i As Integer
            Dim oLoteria As pgmSorteo_loteria
            i = 0
            SeleccionarPestania = False
            For Each tabp In TabExtracciones.TabPages
                oLoteria = tabp.Tag

                If oLoteria.Loteria.IdLoteria = index Then
                    TabExtracciones.SelectedIndex = i
                    SeleccionarPestania = True
                    Exit Function
                End If
                i = i + 1
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:SeleccionarPestania - Excepcion: " & ex.Message)
            'MsgBox("Problema SeleccionarPestania:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    Function QuitarPestaniaEliminada(ByVal pidloteria As Char)
        Dim pestania As New cPestaniaExtraccionesLoteria
        Try
            If ListaPestaniaExtracciones Is Nothing Then Exit Function
            For Each pestania In ListaPestaniaExtracciones

                If pestania.IdPestania = pidloteria Then
                    ListaPestaniaExtracciones.Remove(pestania)
                    Exit Function
                End If
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:QuitarPestaniaEliminada - Excepcion: " & ex.Message)
            'MsgBox("QuitarPestaniaEliminada:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub BtnQuitar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnQuitar.Click
        Dim SorteoLoteriaBO As New PgmSorteoLoteriaBO
        Dim pgmsorteoLoteriaBO As New PgmSorteoLoteriaBO
        Dim idLoteria As Char
        Dim _idpgmsorteo As Integer
        Dim _idConcurso As Integer
        Try
            _idConcurso = CboConcurso.SelectedValue
            _idpgmsorteo = opgmSorteo.idPgmSorteo
            If MsgBox("Al quitar '" & cboJurisdiccion.Text & "' se eliminarán los datos del extracto actual ya registrado" & vbCrLf & "¿Desea quitarla de todas maneras?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo, MDIContenedor.Text) = MsgBoxResult.Yes Then
                idLoteria = cboJurisdiccion.SelectedValue
                If SorteoLoteriaBO.QuitarSorteoLoteria(opgmSorteo, idLoteria, _idpgmsorteo) = False Then
                    MsgBox("Hubo un Problema al eliminar la jurisdicción '" & cboJurisdiccion.Text & "'. ", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                Else
                    QuitarPestaniaEliminada(idLoteria)
                    TabExtracciones.Controls.Clear()
                    frmExtraccionesLoteria_Load(sender, e)
                    If IdconcursoActual <> _idConcurso Then
                        IdconcursoActual = _idConcurso
                        CboConcurso.SelectedValue = _idConcurso
                    End If
                End If
            Else
                Exit Sub
            End If
            '** deconecta el puerto
            DesconectaPuerto()
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:BtnQuitar_Click - Excepcion: " & ex.Message)
            MsgBox("Ocurrió un problema al quitar la jurisdicción. Cierre y vuelva a abrir la aplicación e intente nuevamente. Si el problema susbsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Public Sub ColocarFoco()
        Try
            If txtordenExtracto.Enabled Then txtordenExtracto.Enabled = False
            If txtNroSorteoJurisdiccion.Enabled And txtNroSorteoJurisdiccion.Text.Trim = "" Then
                txtNroSorteoJurisdiccion.Focus()
            Else

                If txtValor1Extraccion.Visible Then
                    Me.txtValor1Extraccion.Focus()
                Else
                    If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                End If
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:LimpiarControles - Excepcion: " & ex.Message)
            'MsgBox("Problema LimpiarControles:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Public Sub LimpiarControles()
        Try
            Me.txtValor1Extraccion.Text = ""
            Me.txtValorExtraccion2.Text = ""
            If txtordenExtracto.Enabled Then txtordenExtracto.Enabled = False
            If txtNroSorteoJurisdiccion.Enabled And txtNroSorteoJurisdiccion.Text.Trim = "" Then
                txtNroSorteoJurisdiccion.Focus()
            Else

                If txtValor1Extraccion.Enabled And txtValor1Extraccion.Visible Then
                    Me.txtValor1Extraccion.Focus()
                Else
                    If txtValorExtraccion2.Enabled And txtValorExtraccion2.Visible Then
                        Me.txtValorExtraccion2.Focus()
                    Else
                        If TxtLetra1.Enabled And TxtLetra1.Visible Then
                            Me.TxtLetra1.Focus()
                        Else
                            If DTPHoraFinextraccion.Enabled And DTPHoraFinextraccion.Visible Then
                                Me.DTPHoraFinextraccion.Focus()
                            Else
                                Try
                                    Me.btnSalir.Focus()
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    End If

                End If
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:LimpiarControles - Excepcion: " & ex.Message)
            ' MsgBox("Problema LimpiarControles:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAgregar.Click
        Dim _idpgmsorteo As Integer
        Dim oLoteriaSorteoBo As New PgmSorteoLoteriaBO
        Dim _idloteria As Char
        Dim _cifras As Integer
        Dim _idPgmConcurso As Integer

        Try
            _idloteria = cboJurisdiccion.SelectedValue
            _idpgmsorteo = opgmSorteo.idPgmSorteo
            _idPgmConcurso = CboConcurso.SelectedValue
            If NroSorteoObligatorio(lsLoterias, _idloteria, _cifras) Then
                If txtNroSorteoJurisdiccion.Text = "" Then
                    MsgBox("El ingreso de Nro. de Sorteo es obligatorio para la lotería seleccionada.", MsgBoxStyle.Information, MDIContenedor.Text)
                    txtNroSorteoJurisdiccion.Enabled = True
                    txtNroSorteoJurisdiccion.Focus()
                    Exit Sub
                End If
            End If
            If Not oLoteriaSorteoBo.AgregarSorteoLoteria(_idloteria, _idpgmsorteo, IIf(txtNroSorteoJurisdiccion.Text = "", 0, txtNroSorteoJurisdiccion.Text)) Then
                MsgBox("Hubo un Problema al agregar lotería.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            '** deconecta el puerto
            DesconectaPuerto()

            TabExtracciones.Controls.Clear()
            frmExtraccionesLoteria_Load(sender, e)
            If IdconcursoActual <> _idPgmConcurso Then
                IdconcursoActual = _idPgmConcurso
                CboConcurso.SelectedValue = _idPgmConcurso
            End If
            cboJurisdiccion.SelectedValue = _idloteria
            txtNroSorteoJurisdiccion.Enabled = False
            If txtValor1Extraccion.Visible Then
                txtValor1Extraccion.Focus()
            Else
                txtValorExtraccion2.Focus()
            End If

        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:ConfbtnAgregar_Click - Excepcion: " & ex.Message)
            MsgBox("Ocurrió un problema al agregar la jurisdicción. Cierre y vuelva a abrir la aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub Habilitaletras(Optional ByVal Deshabilita As Boolean = False)
        If Deshabilita = False Then
            Me.lblLetras.Enabled = True
            Me.TxtLetra1.Enabled = True
            Me.TxtLetra2.Enabled = True
            Me.TxtLetra3.Enabled = True
            Me.TxtLetra4.Enabled = True

        Else
            Me.lblLetras.Enabled = False
            Me.TxtLetra1.Enabled = False
            Me.TxtLetra2.Enabled = False
            Me.TxtLetra3.Enabled = False
            Me.TxtLetra4.Enabled = False

        End If
    End Sub
    Private Function NroSorteoObligatorio(ByVal lsLoterias As ListaOrdenada(Of Loteria), ByVal pIdLoteria As Char, ByRef Cifras As Integer, Optional ByRef Nrosorteo As String = "") As Boolean
        Dim oloteria As New Loteria
        Dim oSorteoLoteria As pgmSorteo_loteria
        For Each oloteria In lsLoterias
            If oloteria.IdLoteria = pIdLoteria Then
                If oloteria.nroSorteoObligatorio Then
                    NroSorteoObligatorio = True
                    '*** se cambia a 5 digitos el nro de sorteo
                    'Cifras = oloteria.CifrasIngresadaDesdeForm
                    Cifras = 5
                    If Nrosorteo <> "" Then
                        If Not opgmSorteo Is Nothing Then
                            For Each oSorteoLoteria In opgmSorteo.ExtraccionesLoteria
                                If oSorteoLoteria.Loteria.IdLoteria = pIdLoteria Then
                                    Nrosorteo = oSorteoLoteria.NroSorteoLoteria
                                    Exit For
                                End If
                            Next
                        End If
                    End If

                    Exit Function
                Else
                    Cifras = 0
                    NroSorteoObligatorio = False
                    Exit Function
                End If
            End If
        Next
    End Function

    Private Sub btmModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmModificar.Click
        Try
            'If btnAgregar.Enabled Then
            '    MsgBox("La lotería que desea modificar no fue agregada a la grilla.", MsgBoxStyle.Information, MDIContenedor.Text)
            '    Exit Sub
            'End If
            HabilitarControlesModificacion()
        Catch ex As Exception
            MsgBox("Problema btmModificar_Click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub HabilitarControlesModificacion()
        Try
            txtordenExtracto.Enabled = True
            btnCancelar.Enabled = True
            'Me.txtValor1Extraccion.Enabled = False
            Me.txtValorExtraccion2.Enabled = False
            'si esta en modo de digitador doble,en la modificación deshabilito el teclado
            If CboPuertos.Visible Then
                DesHabilitarTeclado()
            End If
            txtordenExtracto.Focus()
        Catch ex As Exception
            MsgBox("Problema HabilitarControlesModificacion:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub txtordenExtracto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtordenExtracto.KeyPress
        Dim _Valor As Integer
        Dim _opgmsorteoLoteria As New pgmSorteo_loteria
        Dim FormatoValor As String
        Dim _idConcurso As Integer

        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                _idConcurso = CboConcurso.SelectedValue
                e.Handled = True
                If TabExtracciones.TabPages.Count > 0 Then
                    _opgmsorteoLoteria = TabExtracciones.SelectedTab.Tag
                End If
                If _opgmsorteoLoteria.ObtenerValorPosicion(Me.txtordenExtracto.Text, _Valor) Then
                    Me.txtValor1Extraccion.Enabled = True
                    Me.txtValorExtraccion2.Enabled = True
                    FormatoValor = CrearFormatoCifras(_opgmsorteoLoteria.Loteria.CifrasIngresadaDesdeForm)
                    Me.txtValor1Extraccion.Text = Format(_Valor, FormatoValor)
                    Me.txtValorExtraccion2.Text = Format(_Valor, FormatoValor)
                    Modificando = True
                    If txtValor1Extraccion.Visible Then
                        txtValor1Extraccion.Focus()
                    Else
                        If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                    End If

                Else
                    MsgBox("El orden que intenta modificar aún no ha sido registrado. Verifique y vuelva a ingresar el orden, o cancele.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Me.txtordenExtracto.Text = ObtenerPestaniaExtracciones(_opgmsorteoLoteria.Loteria.IdLoteria, _idConcurso) + 1
                    LimpiarControles()
                End If
            Else
                General.SoloNumeros(sender, e)
            End If
        Catch ex As Exception
            MsgBox("Problema txtordenExtracto_KeyPress:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    Private Sub btmConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmConfirmar.Click
        Dim pgmSorteoLoteriaBO As New PgmSorteoLoteriaBO
        Dim oSorteoBO As New PgmSorteoBO
        Dim opgmSorteoLoteria As pgmSorteo_loteria = Nothing
        Dim _txtletra As TextBox
        Dim txt As String
        Dim fecha As String
        Dim confirmadoOK As Boolean = False

        Dim _idpgmsorteo As Integer
        Dim _idloteria As Char
        Dim _cifras As Integer
        Dim _idPgmConcurso As Integer
        Dim _errorPublicar As Boolean = False
        Dim _PublicarWebON = General.PublicarWebON
        Dim _PublicaExtractosWSRestOFF = General.PublicaExtractosWSRestOFF
        Dim _PublicaExtractosWSRestON = General.PublicaExtractosWSRestON

        Try

            If Modificando Then
                If MsgBox("Quedan modificaciones pendientes de ser Guardadas. " & vbCrLf & " ¿Desea guardarlos ahora?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, MDIContenedor.Text) = MsgBoxResult.Yes Then
                    GuardarDatosDB(True)
                End If
                Modificando = False
            End If
            ' RL: 14/12/2015 - Modifico validacion de fechahora ini y fin real porque fallaba si se dejaba en cero la hora...
            If DTPHoraInicioextraccion.Value.Hour = FechaHoravacia.Hour Then
                MsgBox("Falta completar la hora de inicio.", MsgBoxStyle.Information, MDIContenedor.Text)
                DTPHoraInicioextraccion.Focus()
                Exit Sub
            End If
            If DTPHoraFinextraccion.Value.Hour = FechaHoravacia.Hour Then
                MsgBox("Falta completar la hora de fin.", MsgBoxStyle.Information, MDIContenedor.Text)
                DTPHoraInicioextraccion.Focus()
                Exit Sub
            End If
            If DTPHoraInicioextraccion.Value.Date = FechaHoravacia Then
                DTPHoraInicioextraccion.Value = New DateTime(opgmSorteo.fechaHora.Year, opgmSorteo.fechaHora.Month, opgmSorteo.fechaHora.Day, DTPHoraInicioextraccion.Value.Hour, DTPHoraInicioextraccion.Value.Minute, DTPHoraInicioextraccion.Value.Second)
            End If
            If DTPHoraFinextraccion.Value.Date = FechaHoravacia Then
                DTPHoraFinextraccion.Value = New DateTime(opgmSorteo.fechaHora.Year, opgmSorteo.fechaHora.Month, opgmSorteo.fechaHora.Day, DTPHoraFinextraccion.Value.Hour, DTPHoraFinextraccion.Value.Minute, DTPHoraFinextraccion.Value.Second)
            End If
            If DTPHoraInicioextraccion.Value > DTPHoraFinextraccion.Value Then
                MsgBox("La hora de inicio debe ser menor o igual que la hora de fin ", MsgBoxStyle.Information, MDIContenedor.Text)
                DTPHoraInicioextraccion.Focus()
                Exit Sub
            End If
            ' RL: 14/12/2015 - FIN Modifico validacion de fechahora ini y fin real porque fallaba si se dejaba en cero la hora...

            '**27/11/2012 se pasa aca el control de l ingreso de nro se sorteo
            _idloteria = cboJurisdiccion.SelectedValue
            Me.idLoteriaASeleccionar = _idloteria
            _idpgmsorteo = opgmSorteo.idPgmSorteo
            _idPgmConcurso = CboConcurso.SelectedValue
            If oSorteoBO.getEstadoPgmsorteo(_idpgmsorteo) = 50 Then
                MsgBox("El sorteo al que desea agregar la jurisdicción ya ha sido confirmado.", MsgBoxStyle.Information, MDIContenedor.Text)
                Try
                    Me.Close()
                    Me.Dispose()
                Catch ex As Exception
                End Try
            End If
            If NroSorteoObligatorio(lsLoterias, _idloteria, _cifras) Then
                If txtNroSorteoJurisdiccion.Text = "" Then
                    MsgBox("El ingreso de Nro. de Sorteo es obligatorio para la lotería seleccionada.", MsgBoxStyle.Information, MDIContenedor.Text)
                    txtNroSorteoJurisdiccion.Enabled = True
                    txtNroSorteoJurisdiccion.Focus()
                    Exit Sub
                Else
                    'actualiza el nro de sorteo  de todas maneras,por las dudas si ingresaron el nro de sorteo pero no hicieron click en el boton actualizar
                    If Not pgmSorteoLoteriaBO.ActualizaNroSorteoLoteria(_idloteria, _idpgmsorteo, IIf(txtNroSorteoJurisdiccion.Text = "", 0, txtNroSorteoJurisdiccion.Text)) Then
                        MsgBox("Hubo un Problema al actualizar el número de sorteo de la lotería seleccionada.", MsgBoxStyle.Information, MDIContenedor.Text)
                        Exit Sub
                    End If
                    '***actualiza el objeto en memoria
                    'actualiza el objeto sorteo para reflejar la hora de confirmacion
                    opgmSorteo = oSorteoBO.getPgmSorteo(opgmSorteo.idPgmSorteo)
                    For Each PgmsorteoLoteria In opgmSorteo.ExtraccionesLoteria
                        If PgmsorteoLoteria.Loteria.IdLoteria = _idloteria And PgmsorteoLoteria.IdPgmSorteo = _idpgmsorteo Then
                            PgmsorteoLoteria.NroSorteoLoteria = txtNroSorteoJurisdiccion.Text
                            'TabExtracciones.SelectedTab.Tag = PgmsorteoLoteria
                            Exit For
                        End If
                    Next
                End If
            End If
            '******
            Me.Cursor = Cursors.WaitCursor
            For Each opgmSorteoLoteria In opgmSorteo.ExtraccionesLoteria
                If opgmSorteoLoteria.Loteria.IdLoteria = cboJurisdiccion.SelectedValue Then

                    fecha = oPgmConcurso.fechaHora.ToShortDateString

                    fecha = fecha & " " & DTPHoraInicioextraccion.Value.ToLongTimeString
                    opgmSorteoLoteria.FechaHoraLoteria = CDate(fecha)
                    opgmSorteoLoteria.FechaHoraIniReal = CDate(fecha)

                    fecha = oPgmConcurso.fechaHora.ToShortDateString
                    fecha = fecha & " " & DTPHoraFinextraccion.Value.ToLongTimeString
                    opgmSorteoLoteria.FechaHoraFinReal = CDate(fecha)
                    Exit For
                End If
            Next
            If Not opgmSorteoLoteria Is Nothing Then
                If opgmSorteoLoteria.Loteria.IdLoteria = "N" Then
                    'si se cargan letras,se tiene que cargar todas
                    If TxtLetra1.Text.Trim <> "" Or TxtLetra2.Text.Trim <> "" Or TxtLetra3.Text.Trim <> "" Or TxtLetra4.Text.Trim <> "" Then
                        For i As Integer = 1 To opgmSorteoLoteria.Loteria.cant_letras_extracto
                            _txtletra = New TextBox
                            txt = "txtletra" & i
                            _txtletra = Me.Controls("GroupBoxExtracciones").Controls("GroupBoxIngreso").Controls("GpbConfirmarExtraccion").Controls(txt)
                            If _txtletra.Text.Trim = "" Then
                                Me.Cursor = Cursors.Default
                                MsgBox("Debe completar todas las letras para poder confirmar la extracción.", MsgBoxStyle.Information, MDIContenedor.Text)
                                If _txtletra.Enabled Then _txtletra.Focus()
                                Exit Sub
                            End If
                        Next
                        If InsertarLetrasNacional(opgmSorteoLoteria) = False Then
                            Me.Cursor = Cursors.Default
                            MsgBox("Problema al ingresar las letras del extracto", MsgBoxStyle.Information, MDIContenedor.Text)
                            Exit Sub
                        End If
                    End If
                End If
            End If

            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:llama a confirmar Otra Lotería ->  " & cboJurisdiccion.SelectedValue)
            If pgmSorteoLoteriaBO.Confirmar(opgmSorteo.idPgmSorteo, cboJurisdiccion.SelectedValue, opgmSorteoLoteria.FechaHoraLoteria, opgmSorteoLoteria.FechaHoraIniReal, opgmSorteoLoteria.FechaHoraFinReal) Then
                confirmadoOK = True
                opgmSorteoLoteria.Fechaconfirmacion = Now
            End If
            'actualiza el objeto sorteo para reflejar la hora de confirmacion
            opgmSorteo = oSorteoBO.getPgmSorteo(opgmSorteo.idPgmSorteo)
            For Each PgmsorteoLoteria In opgmSorteo.ExtraccionesLoteria
                If PgmsorteoLoteria.Loteria.IdLoteria = _idloteria And PgmsorteoLoteria.IdPgmSorteo = _idpgmsorteo Then
                    opgmSorteoLoteria = PgmsorteoLoteria
                    opgmSorteoLoteria.RevertidaParcial = 0
                    opgmSorteoLoteria.Confirmada = True
                    TabExtracciones.SelectedTab.Tag = PgmsorteoLoteria
                    Exit For
                End If
            Next

            '** deconecta el puerto
            DesconectaPuerto()
            'AGREGADO POR FSCOTTA
            If _PublicaExtractosWSRestON = "S" Or _PublicaExtractosWSRestOFF = "S" Then
                Try
                    oSorteoBO.publicarWEBRest(opgmSorteo)
                    'FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Confirmar Jurisdiccion: La publicación on line a la Web no está habilitada. PublicaExtractosWSRestON: " & _PublicaExtractosWSRestON & ".")
                Catch ex As Exception
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:Confirmar Otra Lotería -> Publicacion a web -> " & ex.Message)
                    _errorPublicar = True
                End Try
            End If
            '----------------------------------------

            Try
                If _PublicarWebON = "S" Then ' corresponde on line
                    oSorteoBO.publicarWEB(opgmSorteo)
                Else
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:Confirmar Jurisdiccion: La publicación on line a la Web no está habilitada. PublicarWebON: " & _PublicarWebON & ".")
                End If

            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:Confirmar Otra Lotería -> Publicacion a web -> " & ex.Message)
                _errorPublicar = True
            End Try



            Me.Cursor = Cursors.Default
            If confirmadoOK Then
                If _errorPublicar Then
                    MsgBox("Se ha realizado la confirmación correctamente pero se presentó la siguiente situación." & vbCrLf & "- Problemas en la publicación a la Web.Para actualizar la Web , ingrese desde el menú Interfaces ", MsgBoxStyle.Exclamation, MDIContenedor.Text)
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:confirmar Otra Lotería ->  " & cboJurisdiccion.SelectedValue & " OK pero tuvo problemas al publicar")
                Else
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:confirmar Otra Lotería ->  " & cboJurisdiccion.SelectedValue & " OK ")
                    MsgBox("Se ha realizado la confirmación correctamente.", MsgBoxStyle.Information, MDIContenedor.Text)

                End If
                'LimpiarFechaPestania(opgmSorteoLoteria.Loteria.IdLoteria, oPgmConcurso.idPgmConcurso)
            End If
            limpiaLetras()
            HabilitarControles()

            'btnRevertirExtraccion.Enabled = True
            'cboJurisdiccion.Focus()

            'HabilitarOperaciones()
        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:Confirmar Otra Lotería -> " & ex.Message)
            MsgBox("Problema: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub btnRevertirExtraccion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevertirExtraccion.Click
        Dim pgmsorteoLoteriaBO As New PgmSorteoLoteriaBO
        Dim oSorteoBO As New PgmSorteoBO
        Dim _idloteria As Char
        Dim _idPgmConcurso As Integer
        Dim ventana As New frmConfirmacionRevertir
        Dim _modalidad As Integer = 0
        Dim _pgmsorteoLoteria As New pgmSorteo_loteria
        
        Try
            _pgmsorteoLoteria = TabExtracciones.SelectedTab.Tag

            'If btnAgregar.Enabled Then
            '    MsgBox("La lotería no se encuentra agregada a la grilla. No se puede revertir", MsgBoxStyle.Information, MDIContenedor.Text)
            '    Exit Sub
            'End If
            If ControlarLoteriaVacia(_pgmsorteoLoteria) Then
                'MsgBox("La lotería no contiene valores. Solo se habilitará la modificación del nro. de sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                If _pgmsorteoLoteria.Loteria.nroSorteoObligatorio = True Then
                    txtNroSorteoJurisdiccion.Enabled = True
                    BtnActualizarNro.Enabled = True
                End If
                'Exit Sub
                _modalidad = 2
            Else
                ventana._NombreExtraccion = cboJurisdiccion.Text
                ventana.ShowDialog()
                If ventana._Cancelado = True Then
                    Exit Sub
                End If
                _modalidad = ventana._Modalidad
            End If

            'si no se revierte completamente(2),tiene que conservar las horas de inicio y fin del concurso
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:Revertir Otra Lotería -> " & cboJurisdiccion.Text & " Modalidad:" & _modalidad)
            If _modalidad <> 0 Then
                fechaFinAntesRevertir = DTPHoraFinextraccion.Value
                fechaIniAntesRevertir = DTPHoraInicioextraccion.Value
            End If
            _idPgmConcurso = CboConcurso.SelectedValue
            _idloteria = cboJurisdiccion.SelectedValue
            Me.idLoteriaASeleccionar = _idloteria

            _pgmsorteoLoteria = pgmsorteoLoteriaBO.Revertir(opgmSorteo, _idloteria, _modalidad)
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:Reversion OK Otra Lotería -> " & cboJurisdiccion.Text & " Modalidad:" & _modalidad)

            For i As Integer = 0 To oPgmConcurso.PgmSorteos.Count - 1
                If oPgmConcurso.PgmSorteos(i).idPgmSorteo = opgmSorteo.idPgmSorteo Then
                    oPgmConcurso.PgmSorteos(i) = opgmSorteo
                End If
            Next
            For i As Integer = 0 To oPgmConcursos.Count - 1
                If oPgmConcursos(i).idPgmConcurso = oPgmConcurso.idPgmConcurso Then
                    oPgmConcursos(i) = oPgmConcurso
                End If
            Next
            'TabExtracciones.Controls.Clear()
            If _modalidad = 2 Then
                LimpiarFechaPestania(_pgmsorteoLoteria.Loteria.IdLoteria, oPgmConcurso.idPgmConcurso)
            Else
                _pgmsorteoLoteria.RevertidaParcial = 1
                _pgmsorteoLoteria.Confirmada = False
            End If

            'frmExtraccionesLoteria_Load(sender, e)
            'If IdconcursoActual <> _idPgmConcurso Then
            '   IdconcursoActual = _idPgmConcurso
            '   CboConcurso.SelectedValue = _idPgmConcurso
            'End If
            'cboJurisdiccion.SelectedValue = _idloteria

            '** deconecta el puerto
            DesconectaPuerto()

            ' Cierra formularios que pueden traer problemas si permanecen abiertos
            If MDIContenedor.formInicio IsNot Nothing Then
                Try
                    MDIContenedor.formInicio.Close()
                    MDIContenedor.formInicio.Dispose()
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
            CboConcurso.SelectedValue = _idPgmConcurso
            Dim tpg As New System.Windows.Forms.TabPage

            'tpg = TabExtracciones.SelectedTab
            CboConcurso_SelectedIndexChanged(sender, e)
            SeleccionarPestania(_idloteria)
            'TabExtracciones.SelectedTab = tpg

            ' ''actualiza el objeto sorteo para reflejar la hora de confirmacion
            ''opgmSorteo = oSorteoBO.getPgmSorteo(opgmSorteo.idPgmSorteo)
            ''HabilitarControles()

        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " OtrasJurisdicciones:Revertir Otra Lotería -> " & ex.Message)
            MsgBox("Problema al Revertir Jurisdicción: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub ActualizarControlesPgmConcurso()
        Try

            Me.txtJuegoPrincipal.Text = oPgmConcurso.concurso.JuegoPrincipal.Juego.Jue_Desc
            Me.txtNroSorteo.Text = oPgmConcurso.PgmSorteos(0).nroSorteo
            Me.DTPFechaConcurso.Value = oPgmConcurso.fechaHora
            Me.DTPHoraConcurso.Value = oPgmConcurso.fechaHora
            opgmSorteo = opgmConcursoBO.ObtenerPgmSorteoQuiniela(oPgmConcurso)
            IdconcursoActual = oPgmConcurso.idPgmConcurso
            CrearControles(opgmSorteo)
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " ActualizarControlesPgmConcurso Otra Lotería -> " & ex.Message)
            MsgBox("Problema ActualizarControlesPgmConcurso: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Sub CboConcurso_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboConcurso.SelectedIndexChanged
        Dim i As Integer
        Try
            i = CboConcurso.SelectedValue
            ''If Not _estoyEnLoad Then
            '** limpia las letras al cambiar el concurso
            limpiaLetras()
            oPgmConcurso = getPgmConcursoElegido(i)
            If oPgmConcurso IsNot Nothing Then
                ActualizarControlesPgmConcurso()
            End If
            ''End If
            If Me.txtValor1Extraccion.Enabled Then
                Me.txtValor1Extraccion.Focus()
            End If
            'If Me.idLoteriaASeleccionar <> "" Then
            '    SeleccionarPestania(Me.idLoteriaASeleccionar)
            '    Me.idLoteriaASeleccionar = ""
            'End If
            'cboJurisdiccion_SelectedIndexChanged(sender, e)
            LimpiarControles()
        Catch ex As Exception
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " CboConcurso_SelectedIndexChanged Otra Lotería -> " & ex.Message)
            MsgBox("Problema: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Function getPgmConcursoElegido(ByRef idPgmC As Long) As PgmConcurso
        Try

            For Each p As PgmConcurso In oPgmConcursos
                If p.idPgmConcurso = idPgmC Then
                    Return p
                    Exit Function
                End If
            Next
            Return Nothing
        Catch ex As Exception
            getPgmConcursoElegido = Nothing
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " getPgmConcursoElegido Otra Lotería -> " & ex.Message)
            MsgBox("Problema: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub txtNroSorteoJurisdiccion_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtNroSorteoJurisdiccion.GotFocus
        txtNroSorteoJurisdiccion.SelectAll()
    End Sub

    Private Sub txtNroSorteoJurisdiccion_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNroSorteoJurisdiccion.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                If btnAgregar.Enabled Then
                    btnAgregar.Focus()
                End If
                If BtnQuitar.Enabled Then
                    BtnQuitar.Focus()
                End If
                BtnActualizarNro_Click(sender, e)
            Else
                General.SoloNumeros(sender, e)
            End If
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " txtNroSorteoJurisdiccion_KeyPress Otra Lotería -> " & ex.Message)
            MsgBox("Problema: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Sub TxtLetra1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtLetra1.KeyPress
        Dim osorteloteria As New pgmSorteo_loteria
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            If TabExtracciones.TabPages.Count = 0 Then
                MsgBox("No existen jurisdicciones ingresadas.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            osorteloteria = TabExtracciones.SelectedTab.Tag
            If Not osorteloteria.Extracto_Letras_Qnl Is Nothing Then
                osorteloteria.Extracto_Letras_Qnl(0).letra = TxtLetra1.Text.Trim
                osorteloteria.Extracto_Letras_Qnl(0).Orden = 1
                InsertarLetrasNacional(osorteloteria)
            End If
            SendKeys.Send("{TAB}")
        Else
            General.SoloLetrasNacional(sender, e)
        End If
    End Sub


    Private Sub TxtLetra2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtLetra2.KeyPress
        Dim osorteloteria As New pgmSorteo_loteria
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            If TabExtracciones.TabPages.Count = 0 Then
                MsgBox("No existen jurisdicciones ingresadas.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            osorteloteria = TabExtracciones.SelectedTab.Tag
            If Not osorteloteria.Extracto_Letras_Qnl Is Nothing Then
                osorteloteria.Extracto_Letras_Qnl(1).letra = TxtLetra1.Text.Trim
                osorteloteria.Extracto_Letras_Qnl(1).Orden = 2
                InsertarLetrasNacional(osorteloteria)
            End If
            SendKeys.Send("{TAB}")
        Else
            General.SoloLetrasNacional(sender, e)
        End If
    End Sub

    Private Sub TxtLetra3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtLetra3.KeyPress
        Dim osorteloteria As New pgmSorteo_loteria
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            If TabExtracciones.TabPages.Count = 0 Then
                MsgBox("No existen jurisdicciones ingresadas.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            osorteloteria = TabExtracciones.SelectedTab.Tag
            If Not osorteloteria.Extracto_Letras_Qnl Is Nothing Then
                osorteloteria.Extracto_Letras_Qnl(2).letra = TxtLetra1.Text.Trim
                osorteloteria.Extracto_Letras_Qnl(2).Orden = 3
                InsertarLetrasNacional(osorteloteria)
            End If
            If TxtLetra4.Enabled Then TxtLetra4.Focus()
        Else
            General.SoloLetrasNacional(sender, e)
        End If
    End Sub

    Private Sub TxtLetra4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles TxtLetra4.KeyPress
        Dim osorteloteria As New pgmSorteo_loteria
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            If TabExtracciones.TabPages.Count = 0 Then
                MsgBox("No existen jurisdicciones ingresadas.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            osorteloteria = TabExtracciones.SelectedTab.Tag
            If Not osorteloteria.Extracto_Letras_Qnl Is Nothing Then
                osorteloteria.Extracto_Letras_Qnl(3).letra = TxtLetra4.Text.Trim
                osorteloteria.Extracto_Letras_Qnl(3).Orden = 4
                InsertarLetrasNacional(osorteloteria)
            End If
            If btmConfirmar.Enabled Then
                If DTPHoraInicioextraccion.Enabled Then DTPHoraInicioextraccion.Focus()
                ' btmConfirmar.Focus()
            Else
                SendKeys.Send("{TAB}")
            End If
        Else
            General.SoloLetrasNacional(sender, e)
        End If
    End Sub
    Private Sub DeshabilitaControles(Optional ByVal Habilita As Boolean = False)
        If Not Habilita Then
            Me.GroupBoxIngreso.Enabled = False
            Me.btnAgregar.Enabled = False
            Me.BtnQuitar.Enabled = False
        Else
            Me.GroupBoxIngreso.Enabled = True
            Me.btnAgregar.Enabled = True
            Me.BtnQuitar.Enabled = True
        End If
    End Sub
    Private Function ObtenerIndiceResolucion() As Integer
        Dim ancho As Integer
        Dim indice As Integer
        Try
            indice = 0
            ancho = Screen.PrimaryScreen.Bounds.Width
            If ancho < 800 Then
                ancho = 800
            End If
            If ancho >= 800 And ancho < 1024 Then
                ancho = 800
            ElseIf ancho >= 1024 And ancho < 1280 Then
                ancho = 1024
            ElseIf ancho > 1280 Then
                ancho = 1280
            End If
            Select Case ancho
                Case 1024
                    indice = 95

                Case 1280
                    indice = 185

            End Select
            Return indice
        Catch ex As Exception
            Return 1
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " ObtenerIndiceResolucion Otra Lotería -> " & ex.Message)
            MsgBox("Problema: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()

    End Sub


    Public Sub New()
        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        actualiza = AddressOf AgregarNroDesdeDigitador
    End Sub
    Private Sub AgregarNroDesdeDigitador()
        Try
            If Me.txtValorExtraccion2.Text = "" Then
                Me.txtValorExtraccion2.Text = ValorenPuertoSerie  'pNumero
                Me.txtValorExtraccion2_KeyPress(Nothing, New System.Windows.Forms.KeyPressEventArgs(Convert.ToChar(Keys.Return)))
            End If

        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " AgregarNroDesdeDigitador Otra Lotería -> " & ex.Message)
            MsgBox("Problema:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Public Sub SeteaComboPuerto(ByVal pPuerto As String)
        Dim i As Integer
        For i = 0 To CboPuertos.Items.Count - 1
            If CboPuertos.Items(i) = pPuerto Then
                CboPuertos.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Private Sub CboPuertos_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CboPuertos.SelectedIndexChanged
        Dim i As Integer
        If _estoyEnLoad = False Then
            i = CboPuertos.SelectedIndex
            PuertoSerieActual = CboPuertos.Items(i)
        End If
    End Sub

    Private Sub BtnConectar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConectar.Click
        Try
            If CboPuertos.Text = "" Then
                MsgBox("Seleccione un puerto.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            ConectarDesconectarPuertoSerie()
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " BtnConectar_Click Otra Lotería -> " & ex.Message)
            'MsgBox("Problema:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub HabilitarTeclado()
        Dim m_msj As String
        Try
            With SerialPort
                If .IsOpen = True Then
                    m_msj = ""
                    m_msj = ">A" & vbCr
                    .Write(m_msj)
                    Thread.Sleep(100)
                    _TecladoHabilitado = True
                End If
            End With
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " HabilitarTeclado Otra Lotería -> " & ex.Message)
            'MsgBox("Problema:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub HabilitarSonido()
        Dim m_msj As String
        'VariableSonidoDevuelto=0 esta desactivado el sonido
        'VariableSonidoDevuelto=1 esta activado el sonido
        Try
            With SerialPort
                If .IsOpen = True Then
                    If VariableSonidoDevuelto = -1 Then 'es la primera vez que se pulsa
                        m_msj = ""
                        m_msj = ">B" & vbCr
                        .Write(m_msj)
                    Else
                        If Sonidohabilitado Then 'hay que habilitar sonido
                            If VariableSonidoDevuelto <> 1 Then 'y el teclado tiene desactivado el sonido lo activo
                                m_msj = ""
                                m_msj = ">B" & vbCr
                                .Write(m_msj)
                            End If
                        Else 'hay que deshabilitar sonido
                            If VariableSonidoDevuelto <> 0 Then 'y el teclado tiene activo el sonido lo  desactivo                                m_msj = ""
                                m_msj = ">B" & vbCr
                                .Write(m_msj)
                            End If
                        End If
                    End If
                End If
            End With
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " HabilitarSonido Otra Lotería -> " & ex.Message)
            'MsgBox("Problema:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub DesHabilitarTeclado()
        Dim m_msj As String
        Try
            With SerialPort
                If .IsOpen = True Then
                    m_msj = ""
                    m_msj = ">D" & vbCr
                    .Write(m_msj)
                    Thread.Sleep(100)
                    _TecladoHabilitado = False
                End If
            End With
        Catch ex As Exception
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " DesHabilitarTeclado Otra Lotería -> " & ex.Message)
            'MsgBox("Problema:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub SerialPort_DataReceived(ByVal sender As Object, ByVal e As System.IO.Ports.SerialDataReceivedEventArgs) Handles SerialPort.DataReceived
        Dim nro As String = ""
        Try

            If SerialPortClosing = False Then
                If SerialPort.BytesToRead > 0 Then ReceiveBuffer = SerialPort.ReadLine
                If ReceiveBuffer <> "" Then
                    If InStr(ReceiveBuffer, "OK") > 0 Then 'se ingreso un nro correcto
                        DesHabilitarTeclado()
                        nro = ReceiveBuffer.Replace("OK", "")
                        nro = nro.Trim()
                        ValorenPuertoSerie = nro
                        Me.Invoke(actualiza)
                        HabilitarTeclado()
                    End If
                End If
                If btmConfirmar.Enabled Then
                    'Me.lblteclados.Visible = True
                    'lblteclados.Text = "Teclados Deshabilitados"
                    'BtnConectar.Image = My.Resources.Imagenes.conectar.ToBitmap
                    'ToolTip1.SetToolTip(BtnConectar, "Conectar dispositivo")
                    'LabelDesconectarPuerto()
                    DesHabilitarTeclado()
                    SerialPortClosing = True
                End If
            End If
        Catch ex As Exception
            'SerialPort.DiscardInBuffer()
            SerialPort.Close()
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " SerialPort_DataReceived Otra Lotería -> " & ex.Message)
            'MsgBox("Problema:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub ConfigurarPuertoSerie()
        Dim OldPort As String
        Dim PortOpen As Boolean
        Try
            With SerialPort
                OldPort = .PortName
                PortOpen = .IsOpen
                If PortOpen = True Then .Close()
                .BaudRate = 9600
                .Parity = Parity.None
                .DataBits = 8
                .StopBits = StopBits.One
                .PortName = PuertoSerieActual 'CboPuertos.SelectedItem
                .Handshake = IO.Ports.Handshake.None
                If ExistePuerto(PuertoSerieActual) Then
                    .Open()
                    SerialPortClosing = True
                    DesHabilitarTeclado()
                    .Close()
                End If
            End With
        Catch ex As Exception
            FileSystemHelper.Log(" OtrasJurisdicciones:ConfigurarPuertoSerie - Excepcion: " & ex.Message)
            'MsgBox("Problema:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub ConectarDesconectarPuertoSerie()
        Dim ex As Exception
        With SerialPort

            If .IsOpen = False Then
                Try
                    .PortName = PuertoSerieActual
                    .Open()
                    HabilitarTeclado()
                Catch ex
                    FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " Problema al abrir el puerto Otra Lotería -> " & ex.Message)
                    MsgBox("Problema al abrir el puerto:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End Try
            Else
                Try
                    SerialPortClosing = True
                    DesHabilitarTeclado()
                    .Close()
                Catch ex
                    FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " Problema al cerrar el puerto Otra Lotería -> " & ex.Message)
                    MsgBox("Problema al cerrar el puerto:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
                End Try
            End If
            If .IsOpen = True Then
                .ReceivedBytesThreshold = 1
                SerialPortClosing = False
                LabelDesconectarPuerto(_TecladoHabilitado)
            Else
                LabelDesconectarPuerto(_TecladoHabilitado)
                SerialPortClosing = True
            End If
        End With
    End Sub

    Private Sub btnSonido_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSonido.Click
        If Sonidohabilitado Then 'si esta activado lo desactivo y viceversa
            Sonidohabilitado = False
            btnSonido.Image = My.Resources.Imagenes.conSonido.ToBitmap
            ToolTip1.SetToolTip(btnSonido, "Habilitar sonido")
        Else
            Sonidohabilitado = True
            btnSonido.Image = My.Resources.Imagenes.SinSonido.ToBitmap
            ToolTip1.SetToolTip(btnSonido, "Deshabilitar sonido")
        End If
        HabilitarSonido()
    End Sub

    Private Sub limpiaLetras()
        Try
            Me.TxtLetra1.Text = ""
            Me.TxtLetra2.Text = ""
            Me.TxtLetra3.Text = ""
            Me.TxtLetra4.Text = ""
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:limpiaLetras - Excepción: " & ex.Message)
            'MsgBox("Problema:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    Public Sub SeteaCantCfirasMonetevideo(ByVal pcifras As Integer)
        Dim i As Integer
        For i = 0 To cboCantCifras.Items.Count - 1
            If cboCantCifras.Items(i) = pcifras Then
                cboCantCifras.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub

    Public Sub SeteaMetodoSegunValue(ByVal value As Integer)
        Dim i As Integer
        For i = 0 To cboMetodoIngreso.Items.Count - 1
            If CType(cboMetodoIngreso.Items(i), MetodoIngreso).IDMetodoIngreso = value Then
                cboMetodoIngreso.SelectedIndex = i
                Exit For
            End If
        Next
    End Sub
    Private Sub cboCantCifras_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboCantCifras.SelectedIndexChanged
        Try
            If _estoyEnLoad = True Then Exit Sub
            Dim i As Integer
            Dim oloteria As pgmSorteo_loteria
            oloteria = TabExtracciones.SelectedTab.Tag
            i = cboCantCifras.SelectedItem
            oloteria.Loteria.CifrasIngresadaDesdeForm = cboCantCifras.SelectedItem
            txtValor1Extraccion.MaxLength = i
            txtValorExtraccion2.MaxLength = i

            If txtValor1Extraccion.Visible Then
                txtValor1Extraccion.Focus()
            Else
                If txtValorExtraccion2.Enabled Then
                    txtValorExtraccion2.Focus()
                End If
            End If
            If oloteria.Loteria.IdLoteria = "O" And i = 3 Then
                SeteaMetodoSegunValue(4)
            Else
                SeteaMetodoSegunValue(1)
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:cboCantCifras_SelectedIndexChanged - Excepción: " & ex.Message)
            MsgBox("Ha ocurrido un problema al establecer la cantidad de cifras. Cierre y vuelva a abrir la aplicación. Si el problema subsiste consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Function InsertarLetrasNacional(ByVal _sorteoLoteria As pgmSorteo_loteria) As Boolean
        Dim _SorteoLoteriaBO As New PgmSorteoLoteriaBO
        Dim _txtletra As TextBox
        Dim ls As New ListaOrdenada(Of extracto_qnl_letras)
        Dim oExtracto_Qnl_letras As extracto_qnl_letras
        Dim txt As String
        Try
            InsertarLetrasNacional = False
            For i As Integer = 1 To _sorteoLoteria.Loteria.cant_letras_extracto
                oExtracto_Qnl_letras = New extracto_qnl_letras
                _txtletra = New TextBox
                txt = "txtletra" & i
                _txtletra = Me.Controls("GroupBoxExtracciones").Controls("GroupBoxIngreso").Controls("GpbConfirmarExtraccion").Controls(txt)
                oExtracto_Qnl_letras.Orden = i
                oExtracto_Qnl_letras.letra = _txtletra.Text.Trim
                oExtracto_Qnl_letras.idLoteria = _sorteoLoteria.Loteria.IdLoteria
                oExtracto_Qnl_letras.idPgmSorteo = _sorteoLoteria.idPgmSorteo
                ls.Add(oExtracto_Qnl_letras)
            Next
            _sorteoLoteria.Extracto_Letras_Qnl = ls
            If _SorteoLoteriaBO.InsertarLetras_QNL(_sorteoLoteria) = False Then
                MsgBox("Problema al ingresar las letras del extracto.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Function
            End If
            InsertarLetrasNacional = True
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:InsertarLetrasNacional - Excepción: " & ex.Message)
            MsgBox("Ha ocurrido un problema al guardar las letras. No obstante puede continuar la carga.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub ActualizaLetras(ByVal _sorteoLoteria As pgmSorteo_loteria)
        Dim oExtracto_Qnl_letras As extracto_qnl_letras
        Dim _txtletra As TextBox
        Dim txt As String
        Dim i As Integer
        i = 1
        Try
            For Each oExtracto_Qnl_letras In _sorteoLoteria.Extracto_Letras_Qnl
                _txtletra = New TextBox
                txt = "txtletra" & i
                _txtletra = Me.Controls("GroupBoxExtracciones").Controls("GroupBoxIngreso").Controls("GpbConfirmarExtraccion").Controls(txt)
                _txtletra.Text = oExtracto_Qnl_letras.letra
                i = i + 1
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:ActualizaLetras - Excepción: " & ex.Message)
            MsgBox("Ha ocurrido un problema al actualizar las letras. No obstante puede continuar la carga.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub DesconectaPuerto()
        Try
            With SerialPort
                If .IsOpen Then
                    DesHabilitarTeclado()
                    LabelDesconectarPuerto(_TecladoHabilitado)
                    SerialPortClosing = True
                    .Close()
                Else
                    If ExistePuerto(PuertoSerieActual) Then
                        .Open()
                        DesHabilitarTeclado()
                        LabelDesconectarPuerto(_TecladoHabilitado)
                        SerialPortClosing = True
                        .Close()
                    End If
                End If
            End With
        Catch ex As Exception
            'no hace nada
        End Try

    End Sub

    Private Function LabelDesconectarPuerto(Optional ByVal conecta As Boolean = False)
        Try
            If Not conecta Then
                lblteclados.Text = "Teclados Deshabilitados"
                BtnConectar.Image = My.Resources.Imagenes.conectar.ToBitmap
                ToolTip1.SetToolTip(BtnConectar, "Conectar dispositivo")
            Else
                lblteclados.Text = "Teclados Habilitados"
                BtnConectar.Image = My.Resources.Imagenes.desconectar.ToBitmap
                ToolTip1.SetToolTip(BtnConectar, "Desconectar dispositivo")
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:LabelDesconectarPuerto - Excepción: " & ex.Message)
            'MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Function

    Public Function ExistePuerto(ByVal pPuerto As String) As Boolean
        Dim puertos() As String
        Dim i As Integer
        Try

            'obtengo los los nombres puertos de la PC
            puertos = System.IO.Ports.SerialPort.GetPortNames()
            ExistePuerto = False
            For i = 0 To puertos.Count - 1
                If puertos(i) = pPuerto Then
                    ExistePuerto = True
                    Exit For
                End If
            Next
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:ExistePuerto - Excepción: " & ex.Message)
            'MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub DTPHoraInicioextraccion_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DTPHoraInicioextraccion.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Dim idPestania As Char = cboJurisdiccion.SelectedValue
            If ObtenerPestaniaExtracciones(idPestania, oPgmConcurso.idPgmConcurso) = 20 And DTPHoraInicioextraccion.Value <> FechaHoravacia And DTPHoraFinextraccion.Value <> FechaHoravacia Then
                btmConfirmar.Enabled = True
            End If
            If DTPHoraFinextraccion.Enabled Then
                DTPHoraFinextraccion.Focus()
            End If
        End If
    End Sub

    Private Sub DTPHoraFinextraccion_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles DTPHoraFinextraccion.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Dim idPestania As Char = cboJurisdiccion.SelectedValue
            If ObtenerPestaniaExtracciones(idPestania, oPgmConcurso.idPgmConcurso) = 20 And DTPHoraInicioextraccion.Value <> FechaHoravacia And DTPHoraFinextraccion.Value <> FechaHoravacia Then
                btmConfirmar.Enabled = True
            End If
            If btmConfirmar.Enabled Then
                btmConfirmar.Focus()
            End If
        End If
    End Sub

    Private Sub btnAgregar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.EnabledChanged
        If btnAgregar.Enabled Then
            btnAgregar.BackgroundImageLayout = ImageLayout.Stretch
            btnAgregar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnAgregar.BackgroundImageLayout = ImageLayout.Stretch
            btnAgregar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnAgregar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnAgregar.MouseDown
        btnAgregar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnAgregar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.MouseHover
        btnAgregar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnAgregar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAgregar.MouseLeave
        If btnAgregar.Enabled Then
            btnAgregar.BackgroundImage = My.Resources.Imagenes.boton_normal
        End If
    End Sub

    Private Sub btmConfirmar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmConfirmar.EnabledChanged
        If btmConfirmar.Enabled Then
            btmConfirmar.BackgroundImageLayout = ImageLayout.Stretch
            btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else

            btmConfirmar.BackgroundImageLayout = ImageLayout.Stretch
            btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btmConfirmar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btmConfirmar.MouseDown
        btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btmConfirmar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmConfirmar.MouseHover
        btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btmConfirmar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmConfirmar.MouseLeave
        If btmConfirmar.Enabled Then
            btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_normal
        End If
    End Sub

    Private Sub btmModificar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmModificar.EnabledChanged
        If btmModificar.Enabled Then
            btmModificar.BackgroundImageLayout = ImageLayout.Stretch
            btmModificar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btmModificar.BackgroundImageLayout = ImageLayout.Stretch
            btmModificar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btmModificar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btmModificar.MouseDown
        btmModificar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btmModificar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmModificar.MouseHover
        btmModificar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btmModificar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmModificar.MouseLeave
        If btmModificar.Enabled Then
            btmModificar.BackgroundImage = My.Resources.Imagenes.boton_normal
        End If
    End Sub

    Private Sub btnCancelar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.EnabledChanged
        If btnCancelar.Enabled Then
            btnCancelar.BackgroundImageLayout = ImageLayout.Stretch
            btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else

            btnCancelar.BackgroundImageLayout = ImageLayout.Stretch
            btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnCancelar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCancelar.MouseDown
        btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnCancelar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.MouseHover
        btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnCancelar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.MouseLeave
        If btnCancelar.Enabled Then
            btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub BtnQuitar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnQuitar.EnabledChanged
        If BtnQuitar.Enabled Then
            BtnQuitar.BackgroundImageLayout = ImageLayout.Stretch
            BtnQuitar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            BtnQuitar.BackgroundImageLayout = ImageLayout.Stretch
            BtnQuitar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub BtnQuitar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnQuitar.MouseDown
        BtnQuitar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub BtnQuitar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnQuitar.MouseHover
        BtnQuitar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub BtnQuitar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnQuitar.MouseLeave
        If BtnQuitar.Enabled Then
            BtnQuitar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            BtnQuitar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnRevertirExtraccion_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertirExtraccion.EnabledChanged
        If btnRevertirExtraccion.Enabled Then
            btnRevertirExtraccion.BackgroundImageLayout = ImageLayout.Stretch
            btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnRevertirExtraccion.BackgroundImageLayout = ImageLayout.Stretch
            btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnRevertirExtraccion_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnRevertirExtraccion.MouseDown
        btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnRevertirExtraccion_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertirExtraccion.MouseHover
        btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnRevertirExtraccion_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertirExtraccion.MouseLeave
        If btnRevertirExtraccion.Enabled Then
            btnRevertirExtraccion.BackgroundImageLayout = ImageLayout.Stretch
            btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnRevertirExtraccion.BackgroundImageLayout = ImageLayout.Stretch
            btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
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
        If btnSalir.Enabled Then
            btnSalir.BackgroundImageLayout = ImageLayout.Stretch
            btnSalir.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnSalir.BackgroundImageLayout = ImageLayout.Stretch
            btnSalir.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub


    Private Sub BtnActualizarNro_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnActualizarNro.Click
        Dim _idpgmsorteo As Integer
        Dim oLoteriaSorteoBo As New PgmSorteoLoteriaBO
        Dim _idloteria As Char
        Dim _idPgmConcurso As Integer
        Dim PgmsorteoLoteria As pgmSorteo_loteria

        Try
            _idloteria = cboJurisdiccion.SelectedValue
            _idpgmsorteo = opgmSorteo.idPgmSorteo
            _idPgmConcurso = CboConcurso.SelectedValue
            If txtNroSorteoJurisdiccion.Text.Trim = "" Then
                MsgBox("Ingrese un número de sorteo.", MsgBoxStyle.Information, MDIContenedor.Text)
                txtNroSorteoJurisdiccion.Focus()
                Exit Sub
            End If
            '***actualiza la BD
            If Not oLoteriaSorteoBo.ActualizaNroSorteoLoteria(_idloteria, _idpgmsorteo, IIf(txtNroSorteoJurisdiccion.Text = "", 0, txtNroSorteoJurisdiccion.Text)) Then
                MsgBox("Hubo un Problema al actualizar el número de sorteo de la lotería seleccionada.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If

            '***actualiza el objeto en memoria
            For Each PgmsorteoLoteria In opgmSorteo.ExtraccionesLoteria
                If PgmsorteoLoteria.Loteria.IdLoteria = _idloteria And PgmsorteoLoteria.idPgmSorteo = _idpgmsorteo Then
                    PgmsorteoLoteria.NroSorteoLoteria = txtNroSorteoJurisdiccion.Text
                    TabExtracciones.SelectedTab.Tag = PgmsorteoLoteria
                    Exit For
                End If
            Next
            MsgBox("El número de sorteo se actualizó correctamente.", MsgBoxStyle.Information, MDIContenedor.Text)
            If txtValor1Extraccion.Visible Then
                If txtValor1Extraccion.Enabled Then txtValor1Extraccion.Focus()
            Else
                If txtValorExtraccion2.Enabled Then
                    txtValorExtraccion2.Focus()
                Else
                    If TxtLetra1.Enabled Then
                        TxtLetra1.Focus()
                    Else
                        If DTPHoraInicioextraccion.Enabled Then
                            DTPHoraInicioextraccion.Focus()
                        End If
                    End If
                End If
            End If

        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:BtnActualizarNro_Click - Excepción: " & ex.Message)
            MsgBox("Ha ocurrido un problema al actualizar el nro de sorteo particular de la jurisdicción. No obstante puede continuar la carga.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Function ControlarLoteriaVacia(ByVal oSorteoLoteria As pgmSorteo_loteria) As Boolean
        Dim _valores As cPosicionValorLoterias
        Try
            For Each _valores In oSorteoLoteria.Extractos_QNl.Valores
                If _valores.Valor <> "" Then
                    ControlarLoteriaVacia = False
                    Exit Function
                End If
            Next
            ControlarLoteriaVacia = True
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:ControlarLoteriaVacia - Excepción: " & ex.Message)
            ' MsgBox("Problema ControlarLoteriaVacia:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    '****************
    Private Sub ActualizarFechaPestaniaExtracciones(ByVal idPgmConcurso As Integer, ByVal idLoteria As Char, ByVal fechaInicio As Date, ByVal fechaFin As Date)
        Dim Pestania As cPestaniaExtraccionesLoteria
        Dim encontrado As Boolean
        Try
            If ListaPestaniaExtracciones Is Nothing Then
                Exit Sub
            Else
                encontrado = False
                For Each Pestania In ListaPestaniaExtracciones
                    If Pestania.IdPestania = idLoteria And Pestania.NroConcurso = idPgmConcurso Then
                        Pestania.PreservarFecha = 1
                        Pestania.FechaInicio = fechaInicio
                        Pestania.FechaFin = fechaFin
                        encontrado = True
                        Exit Sub
                    End If
                Next

            End If
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:ActualizarFechaPestaniaExtracciones - Excepción: " & ex.Message)
            ' MsgBox("Problema ActualizarFechaPestaniaExtracciones:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Function ObtenerFechaPestania(ByVal oPgmSorteoLoteria As pgmSorteo_loteria, ByVal idpgmConcurso As Integer, ByRef FechaInicio As Date, ByRef FechaFin As Date) As Boolean
        Dim pestania As New cPestaniaExtraccionesLoteria
        Try
            FechaInicio = oPgmSorteoLoteria.FechaHoraIniReal
            FechaFin = oPgmSorteoLoteria.FechaHoraFinReal
            ObtenerFechaPestania = False
            If ListaPestaniaExtracciones Is Nothing Then Exit Function
            For Each pestania In ListaPestaniaExtracciones
                If pestania.IdPestania = oPgmSorteoLoteria.Loteria.IdLoteria And pestania.NroConcurso = idpgmConcurso Then
                    If pestania.PreservarFecha = 1 Then
                        FechaInicio = pestania.FechaInicio
                        FechaFin = pestania.FechaFin
                        ObtenerFechaPestania = True
                    End If
                End If

            Next
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:ObtenerFechaPestania - Excepción: " & ex.Message)
            ' MsgBox("Problema BuscaPestaniaHabilitada:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Function ObtenerFechaPestania(ByVal pidloteria As Char, ByVal idpgmConcurso As Integer, ByRef FechaInicio As Date, ByRef FechaFin As Date) As Boolean
        Dim pestania As New cPestaniaExtraccionesLoteria
        Try
            ObtenerFechaPestania = False
            If ListaPestaniaExtracciones Is Nothing Then Exit Function
            For Each pestania In ListaPestaniaExtracciones
                If pestania.IdPestania = pidloteria And pestania.NroConcurso = idpgmConcurso Then
                    If pestania.PreservarFecha = 1 Then
                        FechaInicio = pestania.FechaInicio
                        FechaFin = pestania.FechaFin
                        ObtenerFechaPestania = True
                    End If
                End If

            Next
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:ObtenerFechaPestania - Excepción: " & ex.Message)
            ' MsgBox("Problema BuscaPestaniaHabilitada:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Sub LimpiarFechaPestania(ByVal pidloteria As Char, ByVal idpgmConcurso As Long)
        Dim pestania As New cPestaniaExtraccionesLoteria
        Try
            If ListaPestaniaExtracciones Is Nothing Then Exit Sub
            For Each pestania In ListaPestaniaExtracciones
                If pestania.IdPestania = pidloteria And pestania.NroConcurso = idpgmConcurso Then
                    pestania.FechaInicio = FechaHoravacia
                    pestania.FechaFin = FechaHoravacia
                End If
            Next

        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:LimpiarFechaPestania - Excepción: " & ex.Message)
            ' MsgBox("Problema LimpiarFechaPestania:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub DTPHoraFinextraccion_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles DTPHoraFinextraccion.LostFocus
        Dim oPgmsorteoLoteria As New pgmSorteo_loteria
        Dim fecha As Date
        Dim fecha1 As Date
        Try
            If TabExtracciones.TabPages.Count > 0 Then
                oPgmsorteoLoteria = TabExtracciones.SelectedTab.Tag
                ObtenerFechaPestania(oPgmsorteoLoteria.Loteria.IdLoteria, oPgmConcurso.idPgmConcurso, fecha, fecha1)
                'setea al objeto de MEMORIA la fecha y hora sin necesidad de que confirmen
                oPgmsorteoLoteria.FechaHoraFinReal = DTPHoraFinextraccion.Value
                oPgmsorteoLoteria.FechaHoraIniReal = DTPHoraInicioextraccion.Value
                ActualizarFechaPestaniaExtracciones(oPgmConcurso.idPgmConcurso, oPgmsorteoLoteria.Loteria.IdLoteria, oPgmsorteoLoteria.FechaHoraIniReal, oPgmsorteoLoteria.FechaHoraFinReal)
                ObtenerFechaPestania(oPgmsorteoLoteria.Loteria.IdLoteria, oPgmConcurso.idPgmConcurso, fecha, fecha1)
            End If

        Catch ex As Exception

        End Try
    End Sub

    Private Function getDescArchivoJur(ByRef oSorteo As PgmSorteo, ByRef oLoteria As Loteria, ByRef nomArchExtrJur As String, ByRef pathArchExtrJur As String, ByRef extArchExtrJur As String, ByRef claveArchExtrJur As String) As String
        Dim nombreConPath As String = ""
        Dim prefijo As String = ""
        'Inicializo...
        nomArchExtrJur = ""
        extArchExtrJur = ""
        pathArchExtrJur = "c:\"
        nombreConPath = pathArchExtrJur
        claveArchExtrJur = ""

        Try
            ' Armo nombre. Depende de la jurisdiccion
            Select Case oLoteria.IdLoteria
                Case "O"
                    prefijo = "QUI"
                    Dim horarioSorteoM As String = "150000"
                    Dim horarioSorteoV As String = "150000"
                    Dim horarioSorteoN As String = "210000"

                    nomArchExtrJur = prefijo & oSorteo.fechaHora.ToString("yyyyMMdd") & _
                             IIf(oSorteo.idJuego = 2, horarioSorteoN, IIf(oSorteo.idJuego = 3, horarioSorteoV, IIf(oSorteo.idJuego = 8, horarioSorteoM, "")))
                Case Else
                    nomArchExtrJur = ""
            End Select

            ' Armo el path para leer/guardar el archivo. Es independiente de la jurisdiccion
            If Not (oLoteria.path_extracto.Trim.EndsWith("\") Or oLoteria.path_extracto.Trim.EndsWith("/")) Then
                oLoteria.path_extracto = oLoteria.path_extracto.Trim & "\"
            End If
            If Not (oSorteo.PathLocalJuego.EndsWith("\") Or oSorteo.PathLocalJuego.EndsWith("/")) Then
                oSorteo.PathLocalJuego = oSorteo.PathLocalJuego.Trim & "\"
            End If
            If (oSorteo.PathLocalJuego.StartsWith("\") Or oSorteo.PathLocalJuego.StartsWith("/")) Then
                oSorteo.PathLocalJuego = oSorteo.PathLocalJuego.Trim.Substring(1, oSorteo.PathLocalJuego.Trim.Length - 1)
            End If
            pathArchExtrJur = oLoteria.path_extracto & oSorteo.PathLocalJuego & oSorteo.nroSorteo.ToString.Trim
            ' Por las dudas intento crearlo. Si ya existe no pasa nada...
            FileSystemHelper.CrearPath(pathArchExtrJur)

            ' Armo el resto de los descriptores a devolver
            extArchExtrJur = oLoteria.Extension_arch_Extracto.Trim.Replace(".", "")
            claveArchExtrJur = oLoteria.Clave
            nombreConPath = pathArchExtrJur & IIf(Not pathArchExtrJur.EndsWith("\"), "\", "") & nomArchExtrJur & _
                            IIf(claveArchExtrJur = "", ".zip", "." & extArchExtrJur & ".gpg")
        Catch ex As Exception
            FileSystemHelper.Log(" ExtraccionesJurisdicciones: Excepción en getDescArchivoJur: " & ex.Message)
            nomArchExtrJur = ""
            extArchExtrJur = ""
            pathArchExtrJur = ""
            nombreConPath = ""
            claveArchExtrJur = ""
        End Try

        Return nombreConPath
    End Function


    ' ''Private Sub btnPorArchivo2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

    ' ''    ' Objetos instanciados
    ' ''    Dim opcBO As New PgmConcursoBO
    ' ''    Dim arBO As New ArchivoBoldtBO
    ' ''    Dim extractoBoldt As New cExtractoArchivoBoldt
    ' ''    Dim oCabecera As New pgmSorteo_loteria

    ' ''    ' Otras variables varias
    ' ''    Dim _valor As Boolean = True
    ' ''    Dim titulo As String = ""
    ' ''    Dim bandera As Integer
    ' ''    Dim archivoCopiado As String = ""
    ' ''    Dim rta As String
    ' ''    Dim _cifrasBD As Integer


    ' ''    ' Datos del sorteo
    ' ''    Dim idpgmsorteo As Long = opgmSorteo.idPgmSorteo
    ' ''    Dim idJuego As Integer = opgmSorteo.idJuego
    ' ''    Dim nroSorteoActual As Long = opgmSorteo.nroSorteo
    ' ''    ' Control de flujo
    ' ''    Dim salirFin As Boolean = False
    ' ''    Dim esValido As Boolean = False
    ' ''    Dim archivoEncontrado As Boolean = False
    ' ''    ' Descriptores del archivo
    ' ''    Dim nombreConPath As String = ""
    ' ''    Dim nomArchExtrJur As String = ""
    ' ''    Dim pathArchExtrJur As String = ""
    ' ''    Dim extArchExtrJur As String = ""
    ' ''    Dim claveArchExtrJur As String = ""
    ' ''    ' Parametros para busqueda manual del archivo
    ' ''    Dim filter As String = "*.*"
    ' ''    Dim defaultExt As String = ""
    ' ''    Dim initialDir As String = "c:\"
    ' ''    Dim nomArchOrigen As String = ""
    ' ''    Dim extArchOrigen As String = ""
    ' ''    Dim pathOrigen As String = ""
    ' ''    Dim nombreConPathOrigen As String = ""

    ' ''    FileSystemHelper.Log(" ExtraccionesJurisdicciones::btnPorArchivo - Inicio btnPorArchivo_click")

    ' ''    oCabecera = TabExtracciones.SelectedTab.Tag
    ' ''    titulo = oCabecera.NombreLoteria

    ' ''    ' Si ya hay datos cargados pregunto si sobreescribirlos
    ' ''    If generalBO.LoteriaComenzada(opgmSorteo.idPgmSorteo, oCabecera.Loteria.IdLoteria, _cifrasBD) Then
    ' ''        'If oCabecera.Extractos_QNl.Valores.Count > 0 Then
    ' ''        rta = MsgBox("Existen nros ya ingresados. Desea tomar los datos del archivo de todos modos?.", MsgBoxStyle.YesNo, MDIContenedor.Text)
    ' ''        If rta = vbNo Then
    ' ''            Exit Sub
    ' ''        End If
    ' ''    End If
    ' ''    ' Verifico que la jurisdiccion tiene habilitada la carga por archivo
    ' ''    If oCabecera.Loteria.path_extracto.Trim.Length = 0 Then
    ' ''        MsgBox("Esta Jurisdicción no tiene habilitada la carga por archivo.", MsgBoxStyle.Exclamation)
    ' ''        Exit Sub
    ' ''    End If

    ' ''    Try
    ' ''        salirFin = False
    ' ''        esValido = False

    ' ''        While ((Not salirFin) And (Not esValido))
    ' ''            archivoEncontrado = False
    ' ''            While ((Not salirFin) And (Not archivoEncontrado))
    ' ''                ' Verifico si existe el archivo
    ' ''                nombreConPath = getDescArchivoJur(opgmSorteo, oCabecera.Loteria, nomArchExtrJur, pathArchExtrJur, extArchExtrJur, claveArchExtrJur)
    ' ''                If (FileSystemHelper.ExisteArchivo(nombreConPath)) Then
    ' ''                    archivoEncontrado = True
    ' ''                Else
    ' ''                    ' Aviso que el arch no esta en la ruta por defecto y si quiere buscarlo manualmente
    ' ''                    rta = MsgBox("No se ha encontrado el archivo correspondiente a la Jurisdicción. Desea buscarlo Ud mismo?", MsgBoxStyle.YesNo, MDIContenedor.Text)
    ' ''                    If rta = vbYes Then
    ' ''                        initialDir = pathArchExtrJur
    ' ''                        If oCabecera.Loteria.Clave = "" Then
    ' ''                            filter = "Archivos (*.zip)|*.zip"
    ' ''                            defaultExt = "zip"
    ' ''                        Else
    ' ''                            filter = "Archivos (*.gpg)|*.gpg"
    ' ''                            defaultExt = "gpg"
    ' ''                        End If
    ' ''                        nombreConPathOrigen = FileSystemHelper.getDescArchivoDesdeOpenDialog(OpenFileD, nomArchOrigen, pathOrigen, extArchOrigen, filter, defaultExt, initialDir)
    ' ''                        If (nomArchOrigen.Trim.Length = 0) Then ' el usuario no eligio archivo, cancelo la busqueda manual
    ' ''                            salirFin = True
    ' ''                        Else
    ' ''                            ' eligio un archivo, lo copio con el nombre esperado en el dir de trabajo

    ' ''                        End If
    ' ''                    End If
    ' ''                End If
    ' ''            End While
    ' ''            If (Not salirFin) Then ' significa que el while anterior salio por archivoEncontrado = true

    ' ''            End If
    ' ''        End While
    ' ''        If (esValido) Then

    ' ''        End If






    ' ''        FileSystemHelper.Log(" ExtraccionesJurisdicciones: generar de extractos a partir de archivos para:" & opgmSorteo.idPgmSorteo)
    ' ''        extractoBoldt = arBO.GenerarExtractodesdeArchivo_O(opgmSorteo.idJuego, opgmSorteo.nroSorteo, oPgmConcurso, bandera, pathArchivoExtracto)
    ' ''        FileSystemHelper.Log(" ExtraccionesJurisdicciones: generacion de extractos a partir de archivos para:" & idpgmsorteo & IIf(bandera = 1, " - No se encontro archivo gpg.", " - Se encontro archivo gpg y se desencripto."))
    ' ''        If Not extractoBoldt Is Nothing Then
    ' ''            setNrosSegunJuego(oCabecera, idJuego, extractoBoldt, titulo, sender)
    ' ''        ElseIf bandera = 1 Then
    ' ''            If noEncontroArchivo(oCabecera, pathArchivoExtracto, archivoCopiado) Then
    ' ''                btnPorArchivo_Click(sender, e)
    ' ''            End If
    ' ''        End If

    ' ''    Catch ex As Exception
    ' ''        FileSystemHelper.Log(" ExtraccionesJurisdicciones: Problemas Obtener extracciones click, pgmsorteo:" & idpgmsorteo & " " & ex.Message)
    ' ''        MsgBox("Ha ocurrido un problema al procesar el archivo de Extracciones. Cierre y vuelva a abrir la aplicación e intente nuevamente. Si el problema subsiste solicite un nuevo archivo, registre manualmente o consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
    ' ''        Exit Sub
    ' ''    End Try

    ' ''        Case Else
    ' ''        FileSystemHelper.Log(" ExtraccionesJurisdicciones: La lectura de extracciones desde un archivo no esta implementada para esta jurisdicción, pgmsorteo:" & idpgmsorteo & " Jurisdicción: " & oCabecera.Loteria.IdLoteria & ".")
    ' ''        MsgBox("La lectura de extracciones desde un archivo no esta implementada para esta jurisdicción.", MsgBoxStyle.Information, MDIContenedor.Text)
    ' ''    End Select
    ' ''End Sub

    Private Sub btnPorArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPorArchivo.Click

        Dim oCabecera As New pgmSorteo_loteria
        Dim titulo As String = ""
        Dim pathArchivoExtracto As String = ""

        oCabecera = TabExtracciones.SelectedTab.Tag
        titulo = oCabecera.NombreLoteria

        Dim opcBO As New PgmConcursoBO
        Dim arBO As New ArchivoBoldtBO
        Dim extractoBoldt As New cExtractoArchivoBoldt
        Dim ArchivoOrigenExtracto As String = ""
        Dim idpgmsorteo As Long = opgmSorteo.idPgmSorteo
        Dim _valor As Boolean = True
        Dim idJuego As Integer = opgmSorteo.idJuego
        Dim nroSorteoActual As Long = opgmSorteo.nroSorteo
        Dim bandera As Integer
        Dim archivoCopiado As String = ""
        Dim rta As String
        Dim _cifrasBD As Integer

        If generalBO.LoteriaComenzada(opgmSorteo.idPgmSorteo, oCabecera.Loteria.IdLoteria, _cifrasBD) Then
            'If oCabecera.Extractos_QNl.Valores.Count > 0 Then
            rta = MsgBox("Existen nros ya ingresados. Desea tomar los datos del archivo de todos modos?.", MsgBoxStyle.YesNo, MDIContenedor.Text)
            If rta = vbNo Then
                Exit Sub
            End If
        End If
        Select Case oCabecera.Loteria.IdLoteria
            Case "O"
                Try
                    FileSystemHelper.Log(" ExtraccionesJurisdicciones: inicio Obtener extracciones click")
                    'el archivo deberia haberse creado enel load pero si no se creo tratade volver a crearlo
                    If oCabecera.Loteria.path_extracto.Trim.Length = 0 Then
                        MsgBox("Esta Jurisdicción no tiene habilitada la carga por archivo.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                    FileSystemHelper.Log(" ExtraccionesJurisdicciones: generar de extractos a partir de archivos para:" & opgmSorteo.idPgmSorteo)
                    extractoBoldt = arBO.GenerarExtractodesdeArchivo_O(opgmSorteo.idJuego, opgmSorteo.nroSorteo, oPgmConcurso, bandera, pathArchivoExtracto)
                    FileSystemHelper.Log(" ExtraccionesJurisdicciones: generacion de extractos a partir de archivos para:" & idpgmsorteo & IIf(bandera = 1, " - No se encontro archivo gpg.", " - Se encontro archivo gpg y se desencripto."))
                    If Not extractoBoldt Is Nothing Then
                        setNrosSegunJuego(oCabecera, idJuego, extractoBoldt, titulo, sender)
                    ElseIf bandera = 1 Then
                        If noEncontroArchivo(oCabecera, pathArchivoExtracto, archivoCopiado) Then
                            btnPorArchivo_Click(sender, e)
                        End If
                    End If

                Catch ex As Exception
                    FileSystemHelper.Log(" ExtraccionesJurisdicciones: Problemas Obtener extracciones click, pgmsorteo:" & idpgmsorteo & " " & ex.Message)
                    MsgBox("Ha ocurrido un problema al procesar el archivo de Extracciones. Cierre y vuelva a abrir la aplicación e intente nuevamente. Si el problema subsiste solicite un nuevo archivo, registre manualmente o consulte a Soporte.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End Try

            Case Else
                FileSystemHelper.Log(" ExtraccionesJurisdicciones: La lectura de extracciones desde un archivo no esta implementada para esta jurisdicción, pgmsorteo:" & idpgmsorteo & " Jurisdicción: " & oCabecera.Loteria.IdLoteria & ".")
                MsgBox("La lectura de extracciones desde un archivo no esta implementada para esta jurisdicción.", MsgBoxStyle.Information, MDIContenedor.Text)
        End Select
    End Sub


    Public Function noEncontroArchivo(ByVal oCabecera As pgmSorteo_loteria, ByVal pathDestino As String, ByRef archivoCopiado As String) As Boolean

        Dim ArchivoOrigen As String = ""
        Dim lstFile As String()
        Dim pathOrigen As String
        Dim NombreArchivo As String = ""
        Dim ArchivoDestino As String = ""
        Dim Archivocontrol As String = ""
        Dim rta As Integer
        archivoCopiado = ""
        Try
            FileSystemHelper.Log("Concursoextracciones:noEncontroArchivo inicio Obtener archivo manual.")
            If pathDestino.Trim.Length <= 0 Then Return False

            rta = MsgBox("No se ha encontrado el archivo correspondiente a la Jurisdicción. Desea buscarlo Ud mismo?", MsgBoxStyle.YesNo, MDIContenedor.Text)

            If rta = vbYes Then

                If oCabecera.Loteria.Clave = "" Then
                    OpenFileD.Filter = "Archivos (*.zip)|*.zip"
                    OpenFileD.DefaultExt = "zip"
                Else
                    OpenFileD.Filter = "Archivos (*.gpg)|*.gpg"
                    OpenFileD.DefaultExt = "gpg"
                End If

                OpenFileD.ShowDialog()

                If OpenFileD.FileNames.Count = 0 Then
                    Return False
                Else
                    Dim juego As String = ""

                    ArchivoOrigen = OpenFileD.FileNames(0)

                    NombreArchivo = OpenFileD.SafeFileName

                    pathOrigen = ArchivoOrigen.Trim.Replace("\" & NombreArchivo, "")


                    ''pathDestino = generalBO.ObtenerCarpetaArchivosExtractoOtrasJuris(oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego, oCabecera.Loteria.IdLoteria)

                    ''If oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 2 Then
                    ''    juego = "Nocturna"
                    ''ElseIf oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 3 Then
                    ''    juego = "Verpertina"
                    ''ElseIf oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 8 Then
                    ''    juego = "Matutina"
                    ''End If

                    ''PathDestino += "\" + juego + "\" + txtNroSorteo.Text + "\O\"

                    ' Por las dudas si el path no existe lo crea, sino no hace nada...
                    FileSystemHelper.CrearPath(pathDestino)

                    ''If (Not System.IO.Directory.Exists(PathDestino)) Then
                    ''    System.IO.Directory.CreateDirectory(PathDestino)
                    ''End If

                    ReDim lstFile(0)

                    lstFile(0) = pathOrigen & ";" & pathDestino & ";" & NombreArchivo
                    FileSystemHelper.CopiarListaArchivos(lstFile, ";")
                    'File.Copy(pathOrigen, pathDestino + "\" + NombreArchivo, True)
                    archivoCopiado = pathDestino & NombreArchivo
                    FileSystemHelper.Log("Concursoextracciones:noEncontroArchivo FIN OK Obtener archivo manual.")
                    Return True
                End If
            Else
                Return False
            End If
        Catch ex As Exception
            FileSystemHelper.Log("Concursoextracciones:noEncontroArchivo FIN ERROR Obtener archivo manual - Excepción: " & ex.Message)
            Return False
        End Try

    End Function

    Public Function noEncontroArchivo_ant(ByVal oCabecera As pgmSorteo_loteria)

        Dim ArchivoOrigen As String = ""
        Dim pathDestino As String = ""
        Dim pathOrigen As String
        Dim NombreArchivo As String = ""
        Dim ArchivoDestino As String = ""
        Dim Archivocontrol As String = ""
        Dim rta As Integer
        If pathDestino.Trim.Length <= 0 Then Return False

        rta = MsgBox("No se ha encontrado el archivo correspondiente a la Jurisdicción. Desea buscarlo Ud mismo?", MsgBoxStyle.YesNo, MDIContenedor.Text)

        If rta = vbYes Then

            If oCabecera.Loteria.Clave = "" Then
                OpenFileD.Filter = "Archivos (*.zip)|*.zip"
                OpenFileD.DefaultExt = "zip"
            Else
                OpenFileD.Filter = "Archivos (*.gpg)|*.gpg"
                OpenFileD.DefaultExt = "gpg"
            End If

            OpenFileD.ShowDialog()

            If OpenFileD.FileNames.Count = 0 Then
                Return False
            Else
                Dim juego As String = ""

                ArchivoOrigen = OpenFileD.FileNames(0)

                NombreArchivo = OpenFileD.SafeFileName

                pathOrigen = ArchivoOrigen

                'PathDestino = generalBO.ObtenerCarpetaArchivosExtractoOtrasJuris(oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego, oCabecera.Loteria.IdLoteria)

                If oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 2 Then
                    juego = "Nocturna"
                ElseIf oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 3 Then
                    juego = "Verpertina"
                ElseIf oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 8 Then
                    juego = "Matutina"
                End If

                pathDestino += "\" + juego + "\" + txtNroSorteo.Text + "\O\"

                If (Not System.IO.Directory.Exists(pathDestino)) Then
                    System.IO.Directory.CreateDirectory(pathDestino)
                End If
                File.Copy(pathOrigen, pathDestino + "\" + NombreArchivo, True)
                Return True
            End If
        Else
            Return False
        End If
    End Function

    Private Sub btnPorArchivo_Click_ant(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Dim oCabecera As New pgmSorteo_loteria
        Dim titulo As String = ""

        oCabecera = TabExtracciones.SelectedTab.Tag
        titulo = oCabecera.NombreLoteria

        Dim opcBO As New PgmConcursoBO
        Dim arBO As New ArchivoBoldtBO
        Dim extractoBoldt As New cExtractoArchivoBoldt
        Dim ArchivoOrigenExtracto As String = ""
        Dim idpgmsorteo As String = ""
        Dim _valor As Boolean = True
        Dim idJuego As Integer
        Dim nroSorteoActual As Long
        Dim bandera As Integer


        Select Case oCabecera.Loteria.IdLoteria
            Case "O"
                Try
                    FileSystemHelper.Log("Concursoextracciones: inicio Obtener extracciones click")
                    If opcBO.getJuegoSorteoRector(oPgmConcurso, idJuego, nroSorteoActual) Then
                        'el archivo deberia haberse creado enel load pero si no se creo tratade volver a crearlo
                        If generalBO.ObtenerCarpetaArchivosExtractoOtrasJuris(nroSorteoActual, oCabecera.Loteria.IdLoteria) = "" Then
                            MsgBox("La carga por archivo no esta habilitada porque no posee configurado un Path ", MsgBoxStyle.Exclamation)
                            Exit Sub
                        End If
                        FileSystemHelper.Log(" Concursoextracciones:generar de extractos a partir de archivos para:" & idpgmsorteo)
                        extractoBoldt = arBO.GenerarExtractodesdeArchivo_O(idJuego, nroSorteoActual, oPgmConcurso, bandera, ArchivoOrigenExtracto)
                        FileSystemHelper.Log("Concursoextracciones: generacion de extractos a partir de archivos para:" & idpgmsorteo & " OK")
                        If Not extractoBoldt Is Nothing Then
                            setNrosSegunJuego(oCabecera, idJuego, extractoBoldt, titulo, sender)
                        ElseIf bandera = 1 Then
                            If noEncontroArchivo_ant(oCabecera) Then
                                btnPorArchivo_Click(sender, e)
                            End If
                        End If
                    End If
                Catch ex As Exception
                    FileSystemHelper.Log(" Concursoextracciones:btnPorArchivo_Click, pgmsorteo:" & idpgmsorteo & " - Excepción: " & ex.Message)
                    MsgBox("El Sistema detectó un problema con el archivo de Extracciones. Se interrumpe la carga. Solicite un nuevo archivo o registre manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End Try

            Case Else

                MsgBox("La carga por archivo solo no esta implementada", MsgBoxStyle.Information, MDIContenedor.Text)

        End Select
    End Sub

    Private Sub setNrosSegunJuego(ByVal oCabecera As pgmSorteo_loteria, ByVal idJuego As Integer, ByRef extractoBoldt As cExtractoArchivoBoldt, ByVal titulo As String, ByVal sender As System.Object)
        Try
            oCabecera = TabExtracciones.SelectedTab.Tag
            If oCabecera.Extractos_QNl.Valores.Count > 0 Then
                txtordenExtracto.Enabled = True
            Else
                txtordenExtracto.Enabled = False
            End If
            txtordenExtracto.Text = "1"
            txtValorExtraccion2.Text = extractoBoldt.Numero_1.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "2"
            txtValorExtraccion2.Text = extractoBoldt.Numero_2.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "3"
            txtValorExtraccion2.Text = extractoBoldt.Numero_3.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "4"
            txtValorExtraccion2.Text = extractoBoldt.Numero_4.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "5"
            txtValorExtraccion2.Text = extractoBoldt.Numero_5.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "6"
            txtValorExtraccion2.Text = extractoBoldt.Numero_6.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "7"
            txtValorExtraccion2.Text = extractoBoldt.Numero_7.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "8"
            txtValorExtraccion2.Text = extractoBoldt.Numero_8.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "9"
            txtValorExtraccion2.Text = extractoBoldt.Numero_9.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "10"
            txtValorExtraccion2.Text = extractoBoldt.Numero_10.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "11"
            txtValorExtraccion2.Text = extractoBoldt.Numero_11.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "12"
            txtValorExtraccion2.Text = extractoBoldt.Numero_12.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "13"
            txtValorExtraccion2.Text = extractoBoldt.Numero_13.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "14"
            txtValorExtraccion2.Text = extractoBoldt.Numero_14.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "15"
            txtValorExtraccion2.Text = extractoBoldt.Numero_15.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "16"
            txtValorExtraccion2.Text = extractoBoldt.Numero_16.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "17"
            txtValorExtraccion2.Text = extractoBoldt.Numero_17.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "18"
            txtValorExtraccion2.Text = extractoBoldt.Numero_18.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "19"
            txtValorExtraccion2.Text = extractoBoldt.Numero_19.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            txtordenExtracto.Enabled = True
            txtordenExtracto.Text = "20"
            txtValorExtraccion2.Text = extractoBoldt.Numero_20.ValorSTR
            txtValorExtraccion2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))

            DTPHoraInicioextraccion.Value = extractoBoldt.FechaHoraSorteo
            DTPHoraFinextraccion.Value = extractoBoldt.FechaHoraCaducidad
            If txtNroSorteoJurisdiccion.Visible And extractoBoldt.NumeroSorteo <> 0 Then
                txtNroSorteoJurisdiccion.Text = extractoBoldt.NumeroSorteo
            End If

            ' '' Todo ok -> habilito modificación y confirmación
            ''btmModificar.Enabled = True
            ''btmConfirmar.Enabled = True
        Catch ex As Exception
            FileSystemHelper.Log("Concursoextracciones:setNrosSegunJuego inicio Obtener extracciones click")
        End Try

    End Sub



    Private Sub txtNroSorteoJurisdiccion_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtNroSorteoJurisdiccion.TextChanged

    End Sub
End Class