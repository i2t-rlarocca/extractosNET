'Imports WindowsApplication2.cAutoridades
Imports Sorteos.Helpers
Imports libEntities.Entities
Imports Sorteos.Bussiness
Imports Sorteos.Extractos

Imports System.IO.Ports
Imports System.Threading
Imports Sorteos.Extractos.ExtractoBussiness
Imports System.IO

Public Class ConcursoExtracciones

    Public OPgmConcurso As PgmConcurso
    Public OPgmConcursos As List(Of PgmConcurso)
    Dim oExtracciones As ExtraccionesCAB
    Dim ExtraccionesBO As ExtraccionesBO
    Dim ExtraccionesCargadas As Integer
    Dim ExtraccionesCargadasAnterior As Integer
    Dim ListaExtraccionesSorteadas As ListaOrdenada(Of cValorPosicion)

    Dim NoModificarMetodoIngreso As Boolean
    Dim ListaPestaniaExtracciones As List(Of cPestaniaExtracciones)
    Dim TagPages As Collection
    Dim HoraInicioExtraccionActual As New Date
    Dim HoraFinExtraccionActual As New Date
    Dim ValorenPuertoSerie As String = ""
    Public actualiza As MethodInvoker
    Dim ValorAntesDeModificacion As Integer = -1
    Dim PosicionAntesDeModificacion As Integer = -1
    Private _estoyEnLoad As Boolean = False
    '***************
    Public WithEvents SerialPort As New SerialPort
    Private ReceiveBuffer As String = ""
    Private m_FormDefInstance As ConcursoExtracciones
    Private m_InitializingDefInstance As Boolean
    Private SerialPortClosing As Boolean
    Dim PuertoSerieActual As String
    Dim IngresoDigitador As Boolean = False
    Dim ParametroSonido As String
    Dim Sonidohabilitado As Boolean = True
    Dim VariableSonidoDevuelto As Integer = -1
    '****** extracciones repetidas
    Dim ListaExtraccionesSorteadasModificable As ListaOrdenada(Of cValorPosicion)
    Dim ReiniciaControles As Boolean 'saber si se tiene que volver a crear controles cuadno se hizo modificaciones en extracciones repetidas
    Dim CompletoExtraccionesRepetidas As Boolean = False 'para saber si se completo la la cantidad de extracciones repetidas
    Dim _ExtraccionRepetida As Boolean = False
    Dim _CantidadextraccionesRepetidas As Integer
    Dim _CantidadextraccionesComletadas As Integer
    Dim _extraccionesRepetidas As List(Of String)
    '** Fuentes**
    Dim LetraNegrita As Font
    Dim LetraNormal As Font
    Dim Letra10Normal As Font
    Dim Modificando As Boolean = False


    Public Sub New()

        ' Llamada necesaria para el Diseñador de Windows Forms.
        InitializeComponent()

        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        actualiza = AddressOf AgregarNroDesdeDigitador

    End Sub
    Public Function CrearControles(ByVal oPC As PgmConcurso)
        Dim PanelExtracciones As New Panel
        Dim iContador As Integer
        Dim Pestaña As TabPage
        Dim oConcurso As New ConcursoJuego
        Dim Combo As New ComboBox
        Dim Check As CheckBox
        'Dim oS As PgmSorteo
        Dim HabilitaPosicion As Boolean
        Dim ExtraccionesDETCargadas As New ListaOrdenada(Of ExtraccionesDET)
        Dim Nombre As String
        Dim i As Integer
        Dim Letra10Regular As Font
        Dim Letra8 As Font
        Dim Letra8N As Font
        Dim Fuente10N As Font
        Dim oCabecera As ExtraccionesCAB
        Dim pLeftPos As Integer
        Dim pLeftVal As Integer
        Dim pLeftHora As Integer
        Dim pLeftOrden As Integer
        Dim pTop As Integer
        Dim _indice As Integer
        _indice = ObtenerIndiceResolucion()

        Try
            TabExtracciones.Controls.Clear()
            Fuente10N = New Font("Myriad Web Pro", 10, FontStyle.Bold)
            Letra10Regular = New Font("Myriad Web Pro", 10, FontStyle.Regular)
            Letra8 = New Font("Myriad Web Pro", 9, FontStyle.Bold)
            Letra8N = New Font("Myriad Web Pro", 9, FontStyle.Regular)
            '** creamos los TABS de Extracciones
            TagPages = New Collection
            iContador = 0
            For Each oCabecera In oPC.Extracciones
                pLeftPos = 28
                pLeftVal = 91
                pLeftHora = 185
                pLeftOrden = 0
                pTop = 58

                ExtraccionesCargadas = 0
                Pestaña = New TabPage
                '** Etiquetas**
                Pestaña.Controls.Add(CrearEtiqueta("lblextraccionJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "EXTRACCION", 4, 5, 95, Letra10Regular))
                Pestaña.Controls.Add(CrearEtiqueta("lbldeJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "DE", 138, 5, 29, Letra10Regular))
                Pestaña.Controls.Add(CrearEtiqueta("lblCantCifrasJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "Cant.Extracciones: ", 4, 31, 125, Letra10Regular))
                Pestaña.Controls.Add(CrearEtiqueta("topeJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "Tope:", 232, 31, 46, Letra10Regular))
                Pestaña.Controls.Add(CrearEtiqueta("CifrasxExtraccionJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "Cant. Cifras: ", 378, 31, 92, Letra10Regular))
                Pestaña.Controls.Add(CrearEtiqueta("lblMinJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "Valor Mín:", 527, 31, 74, Letra10Regular))
                Pestaña.Controls.Add(CrearEtiqueta("lblvalormaxJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "Valor Máx:", 525, 61, 78, Letra10Regular))
                Pestaña.Controls.Add(CrearEtiqueta("lblcriteriofinJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "Criterio de Fin:", 4, 61, 104, Letra10Regular))

                '*** textbox
                Pestaña.Controls.Add(CrearText("txtExtDJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oCabecera.orden, 105, 3, 27, 1, Letra10Regular, True, False, False))
                Pestaña.Controls.Add(CrearText("txtExtHJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oPC.concurso.ModeloExtracciones.cantExtracciones, 170, 3, 27, 1, Letra10Regular, True, False, False))
                Pestaña.Controls.Add(CrearEtiqueta("txtModeloJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oCabecera.ModeloExtraccionesDET.Nombre, 208, 3, 270, Letra10Regular, True, False, False))
                Pestaña.Controls.Add(CrearText("txtCantExtraccionesJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, IIf((oCabecera.ModeloExtraccionesDET.tipoTope.idTipoTope = 1), oCabecera.ModeloExtraccionesDET.cantExtractos, "-"), 120, 31, 27, 1, Letra10Regular, True, False, False))
                Pestaña.Controls.Add(CrearText("txtTopeJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oCabecera.ModeloExtraccionesDET.tipoTope.nombre, 278, 31, 62, 2, , True, False, False))
                Pestaña.Controls.Add(CrearText("txtCifrasExtraccionJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oCabecera.ModeloExtraccionesDET.cantCifras, 470, 31, 21, 1, Letra10Regular, True, False, False))
                Pestaña.Controls.Add(CrearText("txtValorMinJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oCabecera.ModeloExtraccionesDET.valorMinimo, 605, 31, 44, 1, Letra10Regular, True, False, False))
                Pestaña.Controls.Add(CrearText("txtValorMaxJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oCabecera.ModeloExtraccionesDET.valorMaximo, 605, 61, 46, 1, Letra10Regular, True, False, False))
                Pestaña.Controls.Add(CrearText("txtcriterioFinExtraccionJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oCabecera.ModeloExtraccionesDET.criterioFinExtraccion.Nombre, 108, 61, 285, 2, Letra10Regular, True, False, False))

                Check = New CheckBox
                Check.Name = "SorteaPosicionJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet
                Check.Text = "Sortea Posición"
                Check.Checked = oCabecera.ModeloExtraccionesDET.sorteaPosicion
                Check.Font = Letra10Regular
                Check.Left = 540
                Check.Top = 3
                Check.Width = 100
                Check.AutoSize = True
                Check.Enabled = False
                Pestaña.Controls.Add(Check)

                PanelExtracciones = New Panel
                PanelExtracciones.Name = "grdExtraccionesModelo" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet
                PanelExtracciones.BackColor = Color.White
                PanelExtracciones.Left = 5
                PanelExtracciones.Top = 88
                PanelExtracciones.Width = Pestaña.Width
                PanelExtracciones.Height = 40
                PanelExtracciones.AutoScroll = True

                PanelExtracciones.Anchor = AnchorStyles.Left Or AnchorStyles.Right Or AnchorStyles.Top Or AnchorStyles.Bottom
                Pestaña.Controls.Add(PanelExtracciones)

                'Pestaña.AutoScroll = True
                '** crea la el ingreso de las dupla valor-Pos de acuerdo al tipo de extracción
                PanelExtracciones.Controls.Add(CrearEtiqueta("lblordenextraccionJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "Las extracciones se muestran en:", 0, 6, 216, Letra10Regular))
                PanelExtracciones.Controls.Add(CrearText("txtOrdenextraccionJuego" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, oCabecera.ModeloExtraccionesDET.ordenEnExtracto.Nombre, 215, 6, 135, 2, Letra10Regular, True, False, False))
                If oCabecera.ModeloExtraccionesDET.Obligatoria = False Then
                    '** muestro cuantos repetidos y hubo y cuantos ya fueron completados
                    PanelExtracciones.Controls.Add(CrearEtiqueta("lblextraccionesRepetidas" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "Posiciones repetidas completadas:", 360, 6, 225, Letra10Regular))
                    PanelExtracciones.Controls.Add(CrearText("txtextraccionesRepetidasDe" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "0", 585, 8, 25, 1, Letra10Regular, True, False, False))
                    PanelExtracciones.Controls.Add(CrearEtiqueta("lblDERepetidas" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "de", 615, 6, 25, Letra10Regular, , 1))
                    PanelExtracciones.Controls.Add(CrearText("txtextraccionesRepetidasHasta" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet, "0", 640, 8, 25, 1, Letra10Regular, True, False, False))
                End If



                HabilitaPosicion = IIf(oCabecera.ModeloExtraccionesDET.sorteaPosicion = True, True, False)
                If HabilitaPosicion = False Then
                    pLeftOrden = 70
                Else
                    pLeftOrden = 0
                End If
                Dim oextraccionesDET As New ExtraccionesDET

                PanelExtracciones.Controls.Add(CrearEtiqueta("lblposModelo" & oCabecera.idModeloExtraccionesDET, "POS", 25 + _indice, 34, 62, Fuente10N, HabilitaPosicion))
                PanelExtracciones.Controls.Add(CrearEtiqueta("lblvalormodelo" & oCabecera.idModeloExtraccionesDET, "NUMERO", 96 + _indice, 34, 72, Fuente10N))
                PanelExtracciones.Controls.Add(CrearEtiqueta("lblHoraModelo" & oCabecera.idModeloExtraccionesDET, "HORA", 185 + _indice, 34, 62, Fuente10N))

                i = 1
                Dim _ancla As Integer
                _ancla = 1
                'inicializo la variable global de la cantidad de extracciones que se van ingresando
                ExtraccionesCargadas = 0
                Select Case oCabecera.ModeloExtraccionesDET.ordenEnExtracto.idOrdenEnExtracto
                    Case 1 'orden extraccion
                        oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenExtraccion
                    Case 2 'porden posicion extracto
                        oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenPosicion
                    Case 3 'orden numerico
                        oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenValor
                End Select
                Dim formato As String
                formato = CrearFormatoCifras(oCabecera.ModeloExtraccionesDET.cantCifras)
                '** genero los textBox recorriendo la estructura del detalle de las extracciones
                For Each oextraccionesDET In oCabecera.ExtraccionesDET
                    PanelExtracciones.Controls.Add(CrearEtiqueta("lblorden" & oCabecera.idModeloExtraccionesDET & "-" & i, i, pLeftOrden, pTop, 25, Letra10Regular, , 1))
                    PanelExtracciones.Controls.Add(CrearEtiqueta("txtPosModeloid" & oCabecera.idModeloExtraccionesDET & "-" & i, IIf(oextraccionesDET.Orden < 0, "", oextraccionesDET.PosicionEnExtracto), pLeftPos, pTop, 49, LetraNegrita, HabilitaPosicion, , True, 1))
                    If oextraccionesDET.Valor <> -1 Then
                        ExtraccionesCargadas = ExtraccionesCargadas + 1
                    End If

                    PanelExtracciones.Controls.Add(CrearEtiqueta("txtValorModeloid" & oCabecera.idModeloExtraccionesDET & "-" & i, IIf(oextraccionesDET.Orden < 0, "", oextraccionesDET.Valor), pLeftVal, pTop, 82, LetraNegrita, True, , True, 2))

                    PanelExtracciones.Controls.Add(CrearEtiqueta("txtHoraModeloid" & oCabecera.idModeloExtraccionesDET & "-" & i, IIf(oextraccionesDET.FechaHora = "01/01/1999", "", oextraccionesDET.FechaHora.ToString("HH:mm:ss")), pLeftHora, pTop, 80, LetraNegrita, True, , True, 3))

                    pTop = pTop + 28

                    '*** La fecha de inicio de extracción,se setea con la primera extraccion
                    If oCabecera.Ejecutada = 1 Or oCabecera.Ejecutada = 2 Then
                        If i = 1 Then
                            DTPHoraInicioextraccion.Value = IIf(oextraccionesDET.FechaHora = "01/01/1999", Now, oextraccionesDET.FechaHora)
                            HoraInicioExtraccionActual = IIf(oextraccionesDET.FechaHora = "01/01/1999", Now, oextraccionesDET.FechaHora)
                        End If
                    End If

                    '*** La fecha de Fin de extracción,se setea con la ultima extraccion
                    'If i = oCabecera.ModeloExtraccionesDET.cantExtractos Then
                    DTPHoraFinextraccion.Value = IIf(oextraccionesDET.FechaHora = "01/01/1999", Now, oextraccionesDET.FechaHora)
                    HoraFinExtraccionActual = IIf(oextraccionesDET.FechaHora = "01/01/1999", Now, oextraccionesDET.FechaHora)
                    ' End If

                    If i = oCabecera.ModeloExtraccionesDET.cantExtractosPorColumna Then
                        If CompletaColumna(oCabecera) Then
                            pLeftPos = 297
                            pLeftVal = 360
                            pLeftHora = 454
                            If HabilitaPosicion = False Then
                                pLeftOrden = 330
                            Else
                                pLeftOrden = 270
                            End If

                            pTop = 58
                            _ancla = 2

                            PanelExtracciones.Controls.Add(CrearEtiqueta("lblposModelo" & oCabecera.idModeloExtraccionesDET, "POS", 294, 34, 62, Fuente10N, HabilitaPosicion))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("lblvalormodelo" & oCabecera.idModeloExtraccionesDET, "NUMERO", 364, 37, 72, Fuente10N))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("lblHoraModelo" & oCabecera.idModeloExtraccionesDET, "HORA", 451, 34, 62, Fuente10N))
                        End If
                    End If
                    i = i + 1
                Next

                ActualizarPestaniaExtracciones(oCabecera.idExtraccionesCAB, ExtraccionesCargadas)

                '*** fin creacion de duplas
                '** controlar cuantos textos se crearon
                If oCabecera.ModeloExtraccionesDET.tipoTope.idTipoTope = 2 Then 'tope variable
                    Dim Contador As Integer
                    Dim Contador2 As Integer
                    Dim j As Integer
                    Dim mostrarPrimero As Boolean
                    Contador = oCabecera.ModeloExtraccionesDET.cantExtractosPorColumna
                    Contador2 = oCabecera.ModeloExtraccionesDET.cantExtractosPorColumna * 2
                    If i < Contador Then
                        If i = 1 Then 'solo muestra el primero cuadno no existen datos cargados
                            mostrarPrimero = True
                        End If

                        For j = i To Contador
                            PanelExtracciones.Controls.Add(CrearEtiqueta("lblorden" & oCabecera.idModeloExtraccionesDET & "-" & j, j, pLeftOrden, pTop, 25, Letra10Regular, mostrarPrimero, 1))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtPosModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftPos, pTop, 49, LetraNegrita, HabilitaPosicion, True, True, 1))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtValorModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftVal, pTop, 82, LetraNegrita, mostrarPrimero, True, True, 2))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtHoraModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftHora, pTop, 80, LetraNegrita, mostrarPrimero, True, True, 3))
                            pTop = pTop + 28
                            mostrarPrimero = False
                        Next
                        '** creo otra columna
                        pLeftPos = 297
                        pLeftVal = 360
                        pLeftHora = 454

                        pLeftOrden = 270

                        pTop = 58
                        For j = Contador + 1 To Contador2
                            PanelExtracciones.Controls.Add(CrearEtiqueta("lblorden" & oCabecera.idModeloExtraccionesDET & "-" & j, j, pLeftOrden, pTop, 25, Letra10Regular, mostrarPrimero, 1))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtPosModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftPos, pTop, 49, LetraNegrita, HabilitaPosicion, True, True, 1))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtValorModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftVal, pTop, 82, LetraNegrita, mostrarPrimero, True, True, 2))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtHoraModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftHora, pTop, 80, LetraNegrita, mostrarPrimero, True, True, 3))
                            pTop = pTop + 28
                        Next
                    End If
                    If i = Contador Then
                        pLeftPos = 297
                        pLeftVal = 360
                        pLeftHora = 454
                        pLeftOrden = 270

                        pTop = 58
                    End If
                    If i > Contador And i < Contador2 Then
                        mostrarPrimero = True
                        For j = i To Contador2
                            PanelExtracciones.Controls.Add(CrearEtiqueta("lblorden" & oCabecera.idModeloExtraccionesDET & "-" & j, j, pLeftOrden, pTop, 25, Letra10Regular, mostrarPrimero, 1))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtPosModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftPos, pTop, 49, , HabilitaPosicion, True, True, 1))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtValorModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftVal, pTop, 82, , mostrarPrimero, True, True, 2))
                            PanelExtracciones.Controls.Add(CrearEtiqueta("txtHoraModeloid" & oCabecera.idModeloExtraccionesDET & "-" & j, "", pLeftHora, pTop, 80, , mostrarPrimero, True, True, 3))
                            pTop = pTop + 28
                            mostrarPrimero = False
                        Next
                    End If
                End If



                Pestaña.BackColor = Color.White
                Pestaña.Name = "TabExtraccionesModelo" & oCabecera.ModeloExtraccionesDET.idModeloExtraccionesDet

                Nombre = ""
                'oS = oPC.PgmSorteos(iContador)
                Nombre = oCabecera.ModeloExtraccionesDET.Titulo
                Nombre = oCabecera.Titulo

                Pestaña.Text = IIf(oCabecera.ModeloExtraccionesDET.Obligatoria = True, "(R) ", "(O) ") & Nombre
                Pestaña.Tag = oCabecera
                TagPages.Add(Pestaña)
                iContador = iContador + 1
            Next

            Dim Tabs As New TabPage
            Dim Ejecutada As Boolean
            Dim indice As Integer
            Dim pExtraccionesCAB As New ExtraccionesCAB
            Dim pExtraccionesCAB_Ant As New ExtraccionesCAB
            Dim titulo As String = ""
            indice = 0
            Ejecutada = False
            cboIraExtraccion.Items.Clear()
            For Each Tabs In TagPages
                pExtraccionesCAB = Tabs.Tag
                '**obtengo el titulo d ela primer pestaña
                If indice = 0 Then
                    titulo = pExtraccionesCAB.Titulo
                End If
                '** muestra las cabeceras ejecutadas y en ejecucion
                If pExtraccionesCAB.Ejecutada <> 0 Then
                    '** VERIFICAR SI SE HABILITA EL BOTON EXTRA DE QUINI6,SOLO PARA PESTAÑAS EJECUTADAS
                    If pExtraccionesCAB.Ejecutada = 1 Then
                        pExtraccionesCAB_Ant = pExtraccionesCAB
                        If InStr(UCase(pExtraccionesCAB.Titulo), "REVANCHA") > 0 Then
                            BtnExtra.Visible = True
                            BtnExtra.Enabled = True
                        End If
                        If pExtraccionesCAB.ModeloExtraccionesDET.Obligatoria = False Then
                            CrearListaExtraccionesSorteadas(oPC.Extracciones, pExtraccionesCAB)
                            CompletarListaExtraccionesSorteadasDesdeLoad(pExtraccionesCAB)
                        End If
                    Else
                        If pExtraccionesCAB.ModeloExtraccionesDET.Obligatoria = False Then
                            CrearListaExtraccionesSorteadas(oPC.Extracciones, pExtraccionesCAB)
                            CompletarListaExtraccionesSorteadasDesdeLoad(pExtraccionesCAB)
                        End If
                    End If
                    Ejecutada = True
                    TabExtracciones.TabPages.Add(Tabs)

                    Me.cboIraExtraccion.Items.Add(pExtraccionesCAB.Titulo)
                End If
                indice = indice + 1
            Next
            If Ejecutada Then
                SeleccionarPestania(indice - 1)
            Else
                TabExtracciones.TabPages.Add(TagPages(1))
                cboIraExtraccion.Items.Clear()
                cboIraExtraccion.Items.Add(titulo)
            End If
            HabilitarControles()
        Catch ex As Exception
            MsgBox("Error al crear controles:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Function

    Private Sub ConcursoExtracciones_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If txtValor1Extraccion.Visible And txtValor1Extraccion.Enabled Then
            txtValor1Extraccion.Focus()
        Else
            If txtValorExtraccion2.Visible And txtValorExtraccion2.Enabled Then
                txtValorExtraccion2.Focus()
            End If
        End If
    End Sub

    Private Sub ConcursoExtracciones_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        With SerialPort
            If .IsOpen Then
                SerialPortClosing = True
                .DiscardInBuffer()
                DesconectaPuerto()
                .Close()
            End If
        End With
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub

    Private Sub ConcursoExtracciones_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If (e.KeyCode.ToString() = "F1") Then
            If btmModificar.Enabled Then
                btmModificar_Click(btmModificar, Nothing)
            End If
        End If
        If (e.KeyCode.ToString() = "F3") Then
            btnCancelar_Click(btnCancelar, Nothing)
        End If
    End Sub

    Private Sub ConcursoExtracciones_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim general As New GeneralBO
        Dim _Fechahora As Date

        _estoyEnLoad = True
        Try
            Me.Location = New System.Drawing.Point(0, 0)
            LetraNormal = New Font("Myriad Web Pro", 11, FontStyle.Regular)
            LetraNegrita = New Font("Myriad Web Pro", 11, FontStyle.Bold)

            Letra10Normal = New Font("Myriad Web Pro", 10, FontStyle.Regular)

            SerialPortClosing = False
            NoModificarMetodoIngreso = False
            '** si entra por menu
            ExtraccionesCargadas = 0
            If OPgmConcurso Is Nothing Then
                _Fechahora = Now
                Dim oPgmConcursoBO As New PgmConcursoBO
                OPgmConcursos = oPgmConcursoBO.getPgmConcursoInicializadooFinalizado()
                'no existe ningun concurso
                MDIContenedor.CerrarHijo = False
                If OPgmConcursos.Count = 0 Then
                    MsgBox("No existen concursos en condiciones de ser visualizados.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Me.GroupBoxIngreso.Enabled = False
                    MDIContenedor.CerrarHijo = True
                    Exit Sub
                End If
                OPgmConcurso = OPgmConcursos(0)
            Else
                OPgmConcursos = New List(Of PgmConcurso)
                OPgmConcursos.Add(OPgmConcurso)
            End If
            CboConcurso.ValueMember = "idpgmconcurso"
            CboConcurso.DisplayMember = "nombre"
            CboConcurso.DataSource = OPgmConcursos
            CboConcurso.Refresh()
            CboConcurso.SelectedValue = OPgmConcurso.idPgmConcurso
            ActualizarPanelConcurso()
            If Me.txtValor1Extraccion.Visible Then
                Me.txtValor1Extraccion.Focus()
            End If
            '**********
            PuertoSerieActual = general.ObtenerPuerto
            CboPuertos.Items.Add(PuertoSerieActual)
            SeteaComboPuerto(PuertoSerieActual)

            SerialPort = New SerialPort
            With SerialPort
                .DtrEnable = True
                .RtsEnable = True
                '.Handshake = Handshake.RequestToSend
                .Handshake = Handshake.None
                .ReadBufferSize = 4096
                .RtsEnable = True
                .DtrEnable = True
            End With
            DefInstance = Me
            ConfigurarPuertoSerie()
            BtnConectar.Image = My.Resources.Imagenes.conectar.ToBitmap
            ParametroSonido = general.ObtenerSonido
            If ParametroSonido.Equals("S") Then
                Sonidohabilitado = True
                btnSonido.Image = My.Resources.Imagenes.SinSonido.ToBitmap
                ToolTip1.SetToolTip(btnSonido, "Deshabilitar sonido")
            Else
                Sonidohabilitado = False
                btnSonido.Image = My.Resources.Imagenes.conSonido.ToBitmap
                ToolTip1.SetToolTip(btnSonido, "Habilitar sonido")
            End If
            DesconectaPuerto()



        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        Finally
            _estoyEnLoad = False
        End Try

    End Sub

    Private Sub ActualizarPanelConcurso()
        HoraFinExtraccionActual = Now
        HoraInicioExtraccionActual = Now
        cboMetodoIngreso.DisplayMember = "nombre"
        cboMetodoIngreso.ValueMember = "idmetodoingreso"
        cboMetodoIngreso.DataSource = OPgmConcurso.MetodosIngreso
        cboMetodoIngreso.DropDownStyle = ComboBoxStyle.DropDownList
        LimpiarControles()

        DTPFechaConcurso.Format = DateTimePickerFormat.Long
        DTPFechaConcurso.Value = OPgmConcurso.fechaHora
        'DTPHoraConcurso.Format = DateTimePickerFormat.Time
        DTPHoraConcurso.Value = OPgmConcurso.fechaHora

        DTPFechaConcurso.Value = OPgmConcurso.fechaHora.ToString("dd/MM/yyyy")
        DTPHoraConcurso.Value = OPgmConcurso.fechaHora.ToString("dd/MM/yyyy HH:mm:ss")
        If OPgmConcurso.estadoPgmConcurso > 20 Then
            Me.btnRevertir.Enabled = True
        Else
            Me.btnRevertir.Enabled = False
        End If
        'CboConcurso.Text = OPgmConcurso.concurso.Nombre
        txtModeloExtracciones.Text = OPgmConcurso.concurso.ModeloExtracciones.Nombre
        txtCantExtracciones.Text = OPgmConcurso.concurso.ModeloExtracciones.cantExtracciones
        CrearControles(OPgmConcurso)
        ActualizaExtraccionesRepetidas(OPgmConcurso)
        '** si el estado es finalizado se muestra pero no se puede editar 
        If OPgmConcurso.estadoPgmConcurso = 40 Then
            HabilitarControlesConcurso()
        Else
            If OPgmConcurso.estadoPgmConcurso = 20 Or OPgmConcurso.estadoPgmConcurso = 30 Then
                HabilitarControlesConcurso(True)
                habilitaBotonFinalizar()
            End If
        End If

    End Sub

    'Private Sub CompletarValoresExtracciones(ByVal oCabecera As ExtraccionesCAB, ByVal pExtraccionesDET As ExtraccionesDET, ByVal pModifica As Boolean)

    '    Dim nombreTextoValor As String
    '    Dim nombreTextoPos As String
    '    Dim nombreTextoHora As String
    '    Dim nombreGrilla As String
    '    Dim nombreTab As String
    '    Dim i As Integer
    '    Dim NombreText As String
    '    Dim Cajatexto As TextBox
    '    Dim Formato As String
    '    Dim habilitaPosicion As Boolean
    '    Dim nombrelbl As String
    '    Dim etiqueta As Label

    '    Dim nombreL As String
    '    Try
    '        nombreGrilla = "grdExtraccionesModelo" & oCabecera.idModeloExtraccionesDET
    '        nombreTab = "TabExtraccionesModelo" & oCabecera.idModeloExtraccionesDET
    '        nombreTextoValor = "txtValorModeloid" & oCabecera.idModeloExtraccionesDET
    '        nombreTextoPos = "txtPosModeloid" & oCabecera.idModeloExtraccionesDET
    '        nombreTextoHora = "txtHoraModeloid" & oCabecera.idModeloExtraccionesDET
    '        nombreLbl = "lblorden" & oCabecera.idModeloExtraccionesDET
    '        If oCabecera.ModeloExtraccionesDET.sorteaPosicion Then
    '            habilitaPosicion = True
    '        Else
    '            habilitaPosicion = False
    '        End If
    '        i = 1

    '        Select Case oCabecera.ModeloExtraccionesDET.ordenEnExtracto.idOrdenEnExtracto
    '            Case 1 'orden extraccion
    '                oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenExtraccion
    '            Case 2 'porden posicion extracto
    '                oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenPosicion
    '            Case 3 'orden numerico
    '                oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenValor
    '        End Select
    '        Formato = ""
    '        Formato = CrearFormatoCifras(oCabecera.ModeloExtraccionesDET.cantCifras)
    '        For Each oextraccionesDET In oCabecera.ExtraccionesDET
    '            If pModifica = True Then 'si es una modificacion pinto todos

    '                nombreL = nombrelbl & "-" & i
    '                etiqueta = New Label
    '                etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(nombreL)
    '                etiqueta.Text = i
    '                etiqueta.Visible = True

    '                NombreText = nombreTextoValor & "-" & i
    '                Cajatexto = New TextBox
    '                Cajatexto = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
    '                Cajatexto.Text = IIf(oextraccionesDET.Valor < 0, "", Format(oextraccionesDET.Valor))
    '                Cajatexto.Visible = True

    '                NombreText = nombreTextoPos & "-" & i
    '                Cajatexto = New TextBox
    '                Cajatexto = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
    '                Cajatexto.Text = IIf(oextraccionesDET.Valor < 0, "", oextraccionesDET.PosicionEnExtracto)
    '                Cajatexto.Visible = habilitaPosicion

    '                NombreText = nombreTextoHora & "-" & i
    '                Cajatexto = New TextBox
    '                Cajatexto = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
    '                Cajatexto.Text = IIf(oextraccionesDET.Valor < 0, "", oextraccionesDET.FechaHora.ToString("HH:mm:ss"))
    '                Cajatexto.Visible = True
    '            Else

    '                'es un ingreso solo pinto el registro correspondiente
    '                If oextraccionesDET.idExtraccionesDET = pExtraccionesDET.idExtraccionesDET Then

    '                    nombreL = nombrelbl & "-" & i
    '                    etiqueta = New Label
    '                    etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(nombreL)
    '                    etiqueta.Text = i
    '                    etiqueta.Visible = True

    '                    NombreText = nombreTextoValor & "-" & i
    '                    Cajatexto = New TextBox
    '                    Cajatexto = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
    '                    Cajatexto.Text = IIf(oextraccionesDET.Valor < 0, "", oextraccionesDET.Valor)
    '                    If Cajatexto.Visible = False Then Cajatexto.Visible = True

    '                    NombreText = nombreTextoPos & "-" & i
    '                    Cajatexto = New TextBox
    '                    Cajatexto = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
    '                    Cajatexto.Text = IIf(oextraccionesDET.PosicionEnExtracto < 0, "", oextraccionesDET.PosicionEnExtracto)
    '                    'If Cajatexto.Visible = False Then Cajatexto.Visible = True
    '                    Cajatexto.Visible = habilitaPosicion

    '                    NombreText = nombreTextoHora & "-" & i
    '                    Cajatexto = New TextBox
    '                    Cajatexto = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
    '                    Cajatexto.Text = IIf(oextraccionesDET.FechaHora.ToShortTimeString = "", "", oextraccionesDET.FechaHora.ToString("HH:mm:ss"))
    '                    Cajatexto.Visible = True    
    '                    Exit For
    '                End If
    '            End If
    '            i = i + 1
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.Message, MsgBoxStyle.Information)
    '    End Try
    'End Sub

    Private Function persistir(ByVal oCabecera As ExtraccionesCAB, ByVal pExtraccionesDET As ExtraccionesDET, Optional ByVal pModifica As Boolean = False) As Boolean
        Dim BOExtraccion As New ExtraccionesBO
        Try
            If BOExtraccion.InsertarActualizarExtraccionesDET(oCabecera, pExtraccionesDET, pModifica) = False Then
                persistir = False
            Else
                persistir = True
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub txtValorExtraccion2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValorExtraccion2.GotFocus
        txtValorExtraccion2.SelectAll()
    End Sub

    Private Sub txtValor2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValorExtraccion2.KeyPress
        Dim PgmBO As New PgmConcursoBO
        Dim _modifica As Boolean
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            _modifica = False
            If txtOrdenExtracto.Enabled Then 'si el orden del extarcto esta habilitado es una modificacion
                _modifica = True
            End If

            If txtValorExtraccion2.Text.Trim = "" Then
                MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos.", MsgBoxStyle.Critical, MDIContenedor.Text)
                If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                Exit Sub
            End If
            If txtValor1Extraccion.Visible Then
                If txtValor1Extraccion.Text <> txtValorExtraccion2.Text Or txtValor1Extraccion.Text = "" Or txtValorExtraccion2.Text = "" Then
                    MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos.", MsgBoxStyle.Critical, MDIContenedor.Text)
                    txtValor1Extraccion.Focus()
                    Exit Sub
                End If
            End If

            If txtPosEnExtracto2.Visible = False Then
                GuardarDatosDB(_modifica)
                If Modificando Then
                    Modificando = False
                End If
                If ReiniciaControles Then
                    TabExtracciones.Controls.Clear()
                    OPgmConcurso = PgmBO.getPgmConcurso(OPgmConcurso.idPgmConcurso)
                    CrearControles(OPgmConcurso)
                    ActualizaExtraccionesRepetidas(OPgmConcurso)
                    ReiniciaControles = False
                End If
                If btmConfirmar.Enabled Then
                    btmConfirmar.Focus()
                End If
            End If
            If Me.txtPosEnExtracto2.Visible Then
                If txtPosEnExtracto2.Enabled Then txtPosEnExtracto2.Focus()
                Exit Sub
            End If

            If Me.txtValor1Extraccion.Visible Then
                Me.txtValor1Extraccion.Focus()
                Exit Sub
            End If
            If Me.txtPosEnExtracto1.Visible Then
                txtPosEnExtracto1.Focus()
                Exit Sub
            Else
                Me.txtValor1Extraccion.Focus()
                Exit Sub
            End If
        Else
            General.SoloNumeros(sender, e)
        End If

    End Sub
    Public Sub GuardarDatosDB(ByVal pModifica As Boolean)
        Dim _BOExtraccion As New ExtraccionesBO
        Dim _BOpgmConcurso As New PgmConcursoBO
        Dim _oCabecera As ExtraccionesCAB
        Dim _extraccionDET As ExtraccionesDET
        Dim msj As String
        Dim OrdenRepetido As Integer
        Dim _orden As Integer
        Dim _posicion As Integer
        Dim _valor As String
        Dim _valorFormateado As Integer
        Dim ColumnaMax As Integer = 0
        Dim valor As String
        NoModificarMetodoIngreso = True
        Try
            msj = ""
            If cboMetodoIngreso.Enabled = False Then cboMetodoIngreso.Enabled = True
            _oCabecera = TabExtracciones.SelectedTab.Tag
            '** completo las variables que se envia a la funcion de actualizacion
            _orden = txtordenExtracto.Text
            '** 22-03-2012 
            If Not pModifica Then
                _posicion = IIf(txtPosEnExtracto2.Visible = False, _orden, txtPosEnExtracto2.Text)
            Else 'al modificar no importa si esta visible o no el campo posicion,ya que en el keypress del txtorden se le asingo el valor que se obtiene de la BD al txtPosEnExtracto2
                _posicion = txtPosEnExtracto2.Text
            End If
            '_posicion = IIf(txtPosEnExtracto2.Visible = False, _orden, txtPosEnExtracto2.Text)
            _valor = txtValorExtraccion2.Text

            'control para digitador doble
            If Me.CboPuertos.Visible Then
                If txtPosEnExtracto2.Visible Then
                    If txtPosEnExtracto2.Text.Trim <> "" Then
                        'desde el digitador se pueden ingresar + y -,la la funcion isnumeric toma como numero la expresion + y - por eso se reemplaza por un caracter
                        valor = Replace(txtPosEnExtracto2.Text, "+", "z")
                        valor = Replace(valor, "-", "z")
                        If Not IsNumeric(valor) Then
                            MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos." & vbCrLf & "Ingrese un valor numérico.", MsgBoxStyle.Critical, MDIContenedor.Text)
                            txtPosEnExtracto2.Text = ""
                            Exit Sub
                        End If
                    End If
                End If

                If txtValorExtraccion2.Text.Trim <> "" Then
                    valor = Replace(txtValorExtraccion2.Text, "+", "z")
                    valor = Replace(valor, "-", "z")
                    If Not IsNumeric(valor) Then
                        MsgBox("INGRESO INVALIDO.Vuelva a ingresar los datos." & vbCrLf & "Ingrese un valor numérico", MsgBoxStyle.Critical, MDIContenedor.Text)
                        txtValorExtraccion2.Text = ""
                        Exit Sub
                    End If
                End If
            End If
            'controles de posicion cero y vacia
            If Me.txtPosEnExtracto1.Visible Then
                If Me.txtPosEnExtracto1.Text = "" Then
                    MsgBox("INGRESO INVALIDO.Vuelva a ingresar los datos." & vbCrLf & "La posición no puede ser vacía.", MsgBoxStyle.Critical, MDIContenedor.Text)
                    If txtValor1Extraccion.Enabled And txtValor1Extraccion.Visible Then txtValor1Extraccion.Focus()
                    Exit Sub
                Else
                    If Me.txtPosEnExtracto1.Text = 0 Then
                        MsgBox("INGRESO INVALIDO.Vuelva a ingresar los datos." & vbCrLf & "La posición debe ser mayor a cero.", MsgBoxStyle.Critical, MDIContenedor.Text)
                        If txtValor1Extraccion.Enabled And txtValor1Extraccion.Visible Then txtValor1Extraccion.Focus()
                        Exit Sub
                    End If
                End If
            End If
            'controles de posicion cero y vacia
            If txtPosEnExtracto2.Visible Then
                If Me.txtPosEnExtracto2.Text = "" Then
                    MsgBox("INGRESO INVALIDO.Vuelva a ingresar los datos." & vbCrLf & "La posición no puede ser vacía.", MsgBoxStyle.Critical, MDIContenedor.Text)
                    If CboPuertos.Visible Then ' si es digitador limpio los textBox ya que se encuentra deshablitado
                        Me.txtPosEnExtracto2.Text = ""
                        Me.txtValorExtraccion2.Text = ""
                    End If
                    If txtValorExtraccion2.Enabled Then txtValorExtraccion2.Focus()
                    Exit Sub
                Else
                    If Me.txtPosEnExtracto2.Text = 0 Then
                        MsgBox("INGRESO INVALIDO.Vuelva a ingresar los datos." & vbCrLf & "La posición debe ser mayor a cero.", MsgBoxStyle.Critical, MDIContenedor.Text)
                        If CboPuertos.Visible Then ' si es digitador limpio los textBox ya que se encuentra deshablitado
                            Me.txtPosEnExtracto2.Text = ""
                            Me.txtValorExtraccion2.Text = ""
                        End If
                        If txtValorExtraccion2.Enabled Then txtValorExtraccion2.Focus()
                        Exit Sub
                    End If
                End If
            End If

            If Me.txtValor1Extraccion.Visible Then
                If Me.txtValor1Extraccion.Text = "" Then
                    MsgBox("INGRESO INVALIDO.Vuelva a ingresar los datos." & vbCrLf & "El valor no puede ser vacío", MsgBoxStyle.Critical, MDIContenedor.Text)
                    txtValor1Extraccion.Focus()
                    Exit Sub
                End If
            End If

            If Me.txtValorExtraccion2.Text = "" Then
                MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos." & vbCrLf & "El valor no puede ser vacío.", MsgBoxStyle.Critical, MDIContenedor.Text)
                If CboPuertos.Visible Then ' si es digitador limpio los textBox ya que se encuentra deshablitado
                    Me.txtPosEnExtracto2.Text = ""
                    Me.txtValorExtraccion2.Text = ""
                End If
                If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                Exit Sub
            End If


            If pModifica = False Then 'si no es una modificación no se puede repetir la posición del extracto
                If txtPosEnExtracto2.Enabled Or (txtPosEnExtracto2.Enabled = False And CboPuertos.Visible) Then
                    If Not ControlarPosicion(_oCabecera, _posicion, msj) Then
                        MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos." & vbCrLf & msj, MsgBoxStyle.Critical, MDIContenedor.Text)
                        If CboPuertos.Visible Then ' si es digitador limpio los textBox ya que se encuentra deshablitado
                            Me.txtPosEnExtracto2.Text = ""
                            Me.txtValorExtraccion2.Text = ""
                        End If
                        If txtValor1Extraccion.Visible Then
                            txtValor1Extraccion.Focus()
                        Else
                            txtValorExtraccion2.Focus()
                        End If
                        Exit Sub
                    End If
                End If
            Else
                If txtPosEnExtracto2.Enabled Then
                    If _oCabecera.ModeloExtraccionesDET.Obligatoria = True Then
                        '** cambios 20/03/2012
                        If _posicion > _oCabecera.ModeloExtraccionesDET.cantExtractos Then
                            MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos." & vbCrLf & "En '" & _oCabecera.Titulo & "' sólo se permite un máximo de (" & _oCabecera.ModeloExtraccionesDET.cantExtractos & ") posiciones.", MsgBoxStyle.Critical, MDIContenedor.Text)
                            If txtValor1Extraccion.Visible Then
                                txtValor1Extraccion.Focus()
                            Else
                                If CboPuertos.Visible Then
                                    Me.txtPosEnExtracto2.Text = ""
                                    Me.txtValorExtraccion2.Text = ""
                                    Exit Sub
                                End If
                                If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                            End If
                            Exit Sub
                        End If
                        If _posicion <> PosicionAntesDeModificacion And PosicionAntesDeModificacion <> -1 Then 'si las posiciones son distintas
                            If Not ControlarPosicionModificada(_oCabecera, _posicion, OrdenRepetido) Then
                                If MsgBox("El extracto con posición '" & _posicion & "' ya está sorteado con orden '" & OrdenRepetido & "'." & vbCrLf & "Para cambiar esta ubicación debe modificar el extracto de orden '" & OrdenRepetido & "'" & vbCrLf & "¿Desea modificar esta ubicación? ", MsgBoxStyle.YesNo + MsgBoxStyle.Question, MDIContenedor.Text) = MsgBoxResult.Yes Then
                                    '** guardo el orden donde se encuentra la posicion para la modificación
                                    _orden = OrdenRepetido
                                Else
                                    btnCancelar_Click(btnCancelar, Nothing)
                                    Exit Sub
                                End If
                            End If
                        End If
                    End If
                End If
            End If
            PosicionAntesDeModificacion = -1
            'controla la cantidad de cifras ingresadas,cuadno es el cero no se controla la cantidad de cifras
            'el 0 se toma como nro completo
            If txtValorExtraccion2.Text <> 0 Then
                If Len(txtValorExtraccion2.Text) <> _oCabecera.ModeloExtraccionesDET.cantCifras Then
                    MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos." & vbCrLf & "El valor ingresado deber contener (" & _oCabecera.ModeloExtraccionesDET.cantCifras & ") cifras", MsgBoxStyle.Critical, MDIContenedor.Text)
                    If CboPuertos.Visible Then
                        Me.txtPosEnExtracto2.Text = ""
                        Me.txtValorExtraccion2.Text = ""
                        Exit Sub
                    End If
                    If txtValor1Extraccion.Visible Then
                        txtValor1Extraccion.Focus()
                    Else

                        If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                    End If
                    Exit Sub
                End If
            Else
                Dim formato As String
                formato = CrearFormatoCifras(_oCabecera.ModeloExtraccionesDET.cantCifras)
                _valor = Format(txtValorExtraccion2.Text, formato)
            End If
            '** si estan visible  los dos text de valores y posiciones estos deben ser iguales
            If txtValor1Extraccion.Visible And txtValor1Extraccion.Enabled Then
                If txtValor1Extraccion.Text <> txtValorExtraccion2.Text Then
                    MsgBox("El Valor de ingreso 1 debe ser igual al Valor de ingreso 2.", MsgBoxStyle.Critical, MDIContenedor.Text)
                    txtValor1Extraccion.Focus()
                    Exit Sub
                End If
            End If
            If txtPosEnExtracto1.Visible Then
                If Me.txtPosEnExtracto1.Text <> txtPosEnExtracto2.Text Then
                    MsgBox("La Posición de ingreso 1 debe ser igual a la posición de ingreso 2.", MsgBoxStyle.Critical, MDIContenedor.Text)
                    txtPosEnExtracto1.Focus()
                    Exit Sub
                End If
            End If

            '** Para el ingreso de tope variable
            '** se generan dos columnas de controles segun el campo cantExtractosPorColumna de la extracción
            '** se debe controlar para que no de error al querer asignar valores a un control que no existe
            If _oCabecera.ModeloExtraccionesDET.tipoTope.idTipoTope = 2 Then
                ColumnaMax = _oCabecera.ModeloExtraccionesDET.cantExtractosPorColumna * 2
                If _orden > ColumnaMax Then
                    MsgBox("La versión actual solo soporta '" & ColumnaMax & "' ingresos cuando el tope de la extracción es variable.", MsgBoxStyle.Critical, MDIContenedor.Text)
                    If CboPuertos.Visible Then
                        Me.txtPosEnExtracto2.Text = ""
                        Me.txtValorExtraccion2.Text = ""
                    End If
                    Exit Sub
                End If
            End If

            If Not ControlaValoresIngreso(_oCabecera, _valor, msj) Then
                MsgBox("INGRESO INVALIDO.Vuelva a ingresar los datos" & vbCrLf & msj, MsgBoxStyle.Critical, MDIContenedor.Text)

                If CboPuertos.Visible Then
                    Me.txtPosEnExtracto2.Text = ""
                    Me.txtValorExtraccion2.Text = ""
                Else
                    If txtValor1Extraccion.Visible Then
                        txtValor1Extraccion.SelectAll()
                        txtValor1Extraccion.Focus()
                    Else
                        txtValorExtraccion2.SelectAll()
                        txtValorExtraccion2.Focus()
                    End If
                End If
                Exit Sub
            End If

            If Not ControlesRepetidos(_valor, _oCabecera, msj) Then
                MsgBox("INGRESO INVALIDO.Vuelva a ingresar los datos" & vbCrLf & msj, MsgBoxStyle.Critical, MDIContenedor.Text)
                If CboPuertos.Visible Then
                    Me.txtPosEnExtracto2.Text = ""
                    Me.txtValorExtraccion2.Text = ""
                End If
                If txtValor1Extraccion.Visible Then
                    txtValor1Extraccion.SelectAll()
                    txtValor1Extraccion.Focus()
                Else
                    txtValorExtraccion2.SelectAll()
                    txtValorExtraccion2.Focus()
                End If
                Exit Sub
            End If
            'si no es un ingreso no se actualiza las extracciones de la pestana
            If pModifica = False Then
                ActualizarPestaniaExtracciones(_oCabecera.idExtraccionesCAB, _orden)
                _oCabecera.ActualizarEstructuraExtraccionDET(_oCabecera)
            End If
            Dim fechaingreso As Date
            fechaingreso = Now
            _extraccionDET = _oCabecera.ActualizarDetalle(_orden, _valor, fechaingreso, _posicion, pModifica) 'la posicion siempre se envia,si no esta visible el campo posicion,la posicion es igual al orden de extraccion
            If Not _BOExtraccion.InsertarActualizarExtraccionesDET(_oCabecera, _extraccionDET, pModifica) Then
                MsgBox("Error al insertar datos en el detalle de la extraccion", MDIContenedor.Text)
            End If
            '** con el ingreso de la primera extraccion se cambia el estado a en sorteo y se habilita la reversion
            If _orden = 1 And Not pModifica Then
                btnRevertir.Enabled = True
                HoraInicioExtraccionActual = fechaingreso
                ' Me.DTPHoraInicioextraccion.Value = HoraInicioExtraccionActual
                If _BOpgmConcurso.ActualizarEstadoConcurso(_oCabecera.idPgmConcurso, 30) = False Then
                    MsgBox("Error al actualizar el estado del concurso", MsgBoxStyle.Information, MDIContenedor.Text)
                End If
            End If

            'CompletarValoresExtracciones(_oCabecera, _extraccionDET, pModifica)

            _valorFormateado = _extraccionDET.Valor.Substring(Len(_extraccionDET.Valor) - _oCabecera.ModeloExtraccionesDET.cantCifras, _oCabecera.ModeloExtraccionesDET.cantCifras)

            If _oCabecera.ModeloExtraccionesDET.Obligatoria = False Then
                If pModifica Then
                    CompletarListaExtraccionesRepetidas(_oCabecera, pModifica, ValorAntesDeModificacion, _valorFormateado, _extraccionDET)
                Else
                    CompletarListaExtraccionesRepetidas(_oCabecera, pModifica, _valorFormateado, , _extraccionDET)
                End If

            End If

            '**se genera el extracto,este sub,genera la posición en extracto,con lo cual se tiene que actualizar esta posicion en el detalle de memoria(_extraccionesDet) para el valor ingresado 
            Try

                If Not _BOExtraccion.GenerarExtracto(_oCabecera) Then
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & "Fallo GenerarExtracto en sub GuardarDatosDB")
                End If
            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & "Fallo GenerarExtracto en sub GuardarDatosDB:" & ex.Message)
            End Try

            CompletarValoresExtracciones(_oCabecera, _extraccionDET, pModifica)

            If ControlarCriterioFin(_oCabecera) Then
                btmConfirmar.Enabled = True
                HoraFinExtraccionActual = fechaingreso
                Me.DTPHoraFinextraccion.Value = fechaingreso
                Me.txtValor1Extraccion.Enabled = False
                Me.txtValorExtraccion2.Enabled = False
                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                Me.txtPosEnExtracto1.Enabled = False
                Me.txtPosEnExtracto2.Enabled = False
            Else
                Me.txtValor1Extraccion.Enabled = True
                Me.txtValorExtraccion2.Enabled = True
                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                Me.txtPosEnExtracto1.Enabled = True
                Me.txtPosEnExtracto2.Enabled = True
            End If
            HabilitarControles(True)
            'si fue una modificación con modo de digitador,activo el teclado que se deshabilta en la modificación
            If pModifica And CboPuertos.Visible Then
                HabilitarTeclado()

            End If
            If Not pModifica Then
                DTPHoraFinextraccion.Value = fechaingreso
            End If
            LimpiarControles()
            NoModificarMetodoIngreso = False

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub txtPosEnExtracto2_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPosEnExtracto2.GotFocus
        txtPosEnExtracto2.SelectAll()
    End Sub

    Private Sub txtPos2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPosEnExtracto2.KeyPress
        Dim _Modifica As Boolean
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                If txtPosEnExtracto2.Visible And txtPosEnExtracto2.Enabled Then
                    If txtPosEnExtracto2.Text.Trim = "" Then
                        MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos.", MsgBoxStyle.Critical, MDIContenedor.Text)
                        If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                        Exit Sub
                    End If
                End If
                If txtPosEnExtracto1.Visible And txtPosEnExtracto1.Enabled Then
                    If txtPosEnExtracto1.Text <> txtPosEnExtracto2.Text Or txtPosEnExtracto1.Text.Trim = "" Or txtPosEnExtracto2.Text.Trim = "" Then
                        MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos.", MsgBoxStyle.Critical, MDIContenedor.Text)
                        txtValor1Extraccion.Focus()
                        Exit Sub
                    End If
                End If
                If Me.txtordenExtracto.Enabled = True Then
                    _Modifica = True
                Else
                    _Modifica = False
                End If
                GuardarDatosDB(_Modifica)
                If Modificando Then
                    Modificando = False
                End If
                If btmConfirmar.Enabled Then
                    btmConfirmar.Focus()
                End If
            Else
                General.SoloNumeros(sender, e)
            End If


        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Public Sub LimpiarControles()
        Try
            Me.txtValor1Extraccion.Text = ""
            Me.txtValorExtraccion2.Text = ""
            Me.txtPosEnExtracto1.Text = ""
            Me.txtPosEnExtracto2.Text = ""
            If txtordenExtracto.Enabled Then txtordenExtracto.Enabled = False
            If txtValor1Extraccion.Enabled Then
                txtValor1Extraccion.Focus()
            Else
                If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub



    Private Sub btmModificar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmModificar.Click
        Try
            FileSystemHelper.Log(" Concursoextracciones:click en modificar numeros, extraccion:")
            HabilitarControlesModificacion()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub HabilitarControles(Optional ByVal Carga As Boolean = False)
        Dim pextraccionesCargadas As Integer
        Dim ocabecera As ExtraccionesCAB
        Dim Fecha As String
        Dim CumpleCriterioFin As Boolean
        Try
            ocabecera = TabExtracciones.SelectedTab.Tag
            If Carga = True Then
                HabilitaControlMetodIngreso(ocabecera, -1)
            Else
                HabilitaControlMetodIngreso(ocabecera)
            End If
            If ocabecera.ModeloExtraccionesDET.sorteaPosicion = True Then
                Me.txtPosEnExtracto1.Enabled = True
                Me.txtPosEnExtracto2.Enabled = True
                Me.txtPosEnExtracto2.Visible = True
                Me.lblPosicion.Visible = True
            Else
                Me.txtPosEnExtracto1.Enabled = False
                Me.txtPosEnExtracto2.Enabled = False
                Me.txtPosEnExtracto1.Visible = False
                Me.txtPosEnExtracto2.Visible = False
                Me.lblPosicion.Visible = False
            End If

            Fecha = General.Es_Nulo(Of Date)(ocabecera.FechaHoraFinReal, "01/01/1999")
            If Fecha <> "01/01/1999" Then
                LimpiarControles()
                '' Reemplazo esto por Me.gpbIngresoDatos.Enabled = False 
                If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = False
                If txtPosEnExtracto1.Visible Then txtPosEnExtracto1.Enabled = False
                txtPosEnExtracto2.Enabled = False
                txtValorExtraccion2.Enabled = False
                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                btmModificar.Enabled = False
                btmConfirmar.Enabled = False
                ''Me.gpbIngresoDatos.Enabled = False

                DTPHoraFinextraccion.Enabled = False
                DTPHoraFinextraccion.Value = ocabecera.FechaHoraFinReal
                DTPHoraInicioextraccion.Enabled = False
                DTPHoraInicioextraccion.Value = ocabecera.FechaHoraIniReal

            Else
                If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = True
                If txtPosEnExtracto1.Visible Then txtPosEnExtracto1.Enabled = True
                txtPosEnExtracto2.Enabled = True
                txtValorExtraccion2.Enabled = True
                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                btmModificar.Enabled = True
                If cboMetodoIngreso.SelectedValue = General.MetodoIngreso.lecturaArchivo Then
                    btnPorArchivo.Visible = True
                Else
                    btnPorArchivo.Visible = False
                End If
                If ControlarCriterioFin(ocabecera) Then
                    If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = False
                    If txtPosEnExtracto1.Visible Then txtPosEnExtracto1.Enabled = False
                    txtPosEnExtracto2.Enabled = False
                    txtValorExtraccion2.Enabled = False
                    If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                    btmConfirmar.Enabled = True
                    CumpleCriterioFin = True
                Else
                    btmConfirmar.Enabled = False
                    CumpleCriterioFin = False
                End If
                DTPHoraFinextraccion.Enabled = True
                DTPHoraInicioextraccion.Enabled = True
                'DTPHoraInicioextraccion.Value = HoraInicioExtraccionActual
                DTPHoraFinextraccion.Value = HoraFinExtraccionActual


            End If
            pextraccionesCargadas = ObtenerPestaniaExtracciones(ocabecera.idExtraccionesCAB)

            If ocabecera.ModeloExtraccionesDET.tipoTope.idTipoTope = 1 Then
                txtExtractoHasta.Text = ocabecera.ModeloExtraccionesDET.cantExtractos
            Else
                txtExtractoHasta.Text = "-"
            End If

            txtValor1Extraccion.MaxLength = ocabecera.ModeloExtraccionesDET.cantCifras
            txtValorExtraccion2.MaxLength = ocabecera.ModeloExtraccionesDET.cantCifras
            If ocabecera.ModeloExtraccionesDET.cantExtractos <> 0 Then
                If pextraccionesCargadas < ocabecera.ModeloExtraccionesDET.cantExtractos Then
                    pextraccionesCargadas = pextraccionesCargadas + 1
                    txtordenExtracto.Text = pextraccionesCargadas
                    txtValorExtraccion2.Enabled = True
                    If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                    txtPosEnExtracto2.Enabled = True
                    If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = True
                    If txtPosEnExtracto1.Visible Then txtPosEnExtracto1.Enabled = True
                    btmModificar.Enabled = True
                ElseIf pextraccionesCargadas = ocabecera.ModeloExtraccionesDET.cantExtractos Then
                    txtValorExtraccion2.Enabled = False
                    If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                    txtPosEnExtracto2.Enabled = False
                    If Me.txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = False
                    If Me.txtPosEnExtracto1.Visible Then txtPosEnExtracto1.Enabled = False
                    txtordenExtracto.Text = pextraccionesCargadas
                    If Fecha = "01/01/1999" Then
                        btmModificar.Enabled = True

                    Else
                        btmModificar.Enabled = False
                    End If
                End If
            Else
                If Fecha = "01/01/1999" And CumpleCriterioFin = False Then 'si no esta confirmada habilito el ingreso
                    txtValorExtraccion2.Enabled = True
                    If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                    txtPosEnExtracto2.Enabled = True
                    If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = True
                    If txtPosEnExtracto1.Visible Then txtPosEnExtracto1.Enabled = True
                    pextraccionesCargadas = pextraccionesCargadas + 1
                Else
                    If Fecha = "01/01/1999" And CumpleCriterioFin Then
                        txtValorExtraccion2.Enabled = False
                        If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                        txtPosEnExtracto2.Enabled = False
                        If txtValor1Extraccion.Visible Then txtValor1Extraccion.Enabled = False
                        If txtPosEnExtracto1.Visible Then txtPosEnExtracto1.Enabled = False
                    End If
                End If
                txtordenExtracto.Text = pextraccionesCargadas
                txtExtractoHasta.Text = "-"

            End If
            txtordenExtracto.Enabled = False
            If IngresoDigitador Then
                Me.txtPosEnExtracto2.Enabled = False
                Me.txtValorExtraccion2.Enabled = False
                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
            Else
                If btmConfirmar.Enabled = False Then
                    If Fecha = "01/01/1999" Then
                        Me.txtPosEnExtracto2.Enabled = True
                        Me.txtValorExtraccion2.Enabled = True
                        'If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                    End If
                Else
                    'si el boton confirmar concurso esta habilitado lo desahilito porque existen pestañas que se faltan confirmar
                    If btnFinalizar.Enabled Then btnFinalizar.Enabled = False
                End If
            End If
            'si no esta confirmada se muestra tantos ceros como cantidad de cifras tenga configurado como ayuda visual
            Dim nro As String
            If Fecha = "01/01/1999" Then
                nro = CrearFormatoCifras(ocabecera.ModeloExtraccionesDET.cantCifras)
            Else
                nro = ""
            End If
            If ocabecera.MetodoIngreso.IDMetodoIngreso <> 2 Then
                If nro.Trim <> "" And nro.Trim <> "00" Then
                    txtValorExtraccion2.Text = nro
                    txtValorExtraccion2.SelectAll()
                End If
                If txtValorExtraccion2.Enabled Then txtValorExtraccion2.Focus()
            Else
                If nro.Trim <> "" And nro.Trim <> "00" Then
                    txtValor1Extraccion.Text = nro
                    txtValor1Extraccion.SelectAll()
                End If
                If txtValor1Extraccion.Enabled Then txtValor1Extraccion.Focus()
            End If
            HabilitaBotonExtra()
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Function ControlarCriterioFin(ByVal oExtraccionesCAB As ExtraccionesCAB) As Boolean
        Try
            Select Case oExtraccionesCAB.ModeloExtraccionesDET.criterioFinExtraccion.idCriterioFinExtraccion
                Case Sorteos.Helpers.General.CriterioFinExtraccion.CantMaxExtraccionesAlcanzadas
                    If ObtenerPestaniaExtracciones(oExtraccionesCAB.idExtraccionesCAB) = oExtraccionesCAB.ModeloExtraccionesDET.cantExtractos Then
                        Return True
                    Else
                        Return False
                    End If
                Case Sorteos.Helpers.General.CriterioFinExtraccion.ExtraccionesDiferentesEnRondaAnt
                    If ListaExtraccionesSorteadasCompletaModificada() Then
                        Return True
                    Else
                        Return False
                    End If
            End Select
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub HabilitarPestania(ByVal proxEx As Long)
        Dim _extraccionesCAB As ExtraccionesCAB
        Dim _extraccionesCABori As ExtraccionesCAB
        Dim _index As Integer
        Dim tabs As TabPage
        Dim repetido As Boolean
        Try
            _extraccionesCAB = TabExtracciones.SelectedTab.Tag

            _extraccionesCABori = TabExtracciones.SelectedTab.Tag
            repetido = False
            _index = 0

            For Each tabs In TagPages
                _extraccionesCAB = tabs.Tag
                If _extraccionesCAB.idExtraccionesCAB = proxEx Then
                    '** se incializa el contador de extracciones
                    ExtraccionesCargadas = 0
                    txtordenExtracto.Text = ExtraccionesCargadas
                    '** de deshabilita el boton de confirmación
                    btmConfirmar.Enabled = False
                    btmModificar.Enabled = True
                    If Not _extraccionesCAB.ModeloExtraccionesDET.Obligatoria Then
                        CrearListaExtraccionesSorteadas(OPgmConcurso.Extracciones, _extraccionesCAB)

                    End If
                    'habilito navegacion
                    SerialPortClosing = False
                    Me.btnExtraccionAnterior.Enabled = True
                    Me.btnextraccionSiguiente.Enabled = True
                    TabExtracciones.TabPages.Add(tabs)
                    If Not _extraccionesCAB.ModeloExtraccionesDET.Obligatoria Then
                        ActualizaExtraccionesRepetidas(OPgmConcurso)
                    End If
                    TabExtracciones.SelectedIndex = _index

                    Exit For
                End If
                _index = _index + 1
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    Private Sub txtordenExtracto_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtordenExtracto.KeyPress
        Dim _posicion As Integer
        Dim _Valor As Integer
        Dim _oCabecera As ExtraccionesCAB
        Dim FormatoValor As String

        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                _oCabecera = TabExtracciones.SelectedTab.Tag
                If _oCabecera.ObtenerValorPosicion(Me.txtordenExtracto.Text, _posicion, _Valor) Then
                    'habilita campos de ingreso
                    Me.txtValor1Extraccion.Enabled = True
                    Me.txtPosEnExtracto1.Enabled = True
                    Me.txtValorExtraccion2.Enabled = True
                    'If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                    Me.txtPosEnExtracto2.Enabled = True

                    FormatoValor = CrearFormatoCifras(_oCabecera.ModeloExtraccionesDET.cantCifras)
                    ValorAntesDeModificacion = Format(_Valor, FormatoValor)
                    PosicionAntesDeModificacion = _posicion
                    Me.txtValor1Extraccion.Text = Format(_Valor, FormatoValor)
                    Me.txtPosEnExtracto1.Text = _posicion
                    Me.txtValorExtraccion2.Text = Format(_Valor, FormatoValor)
                    Me.txtPosEnExtracto2.Text = _posicion
                    Modificando = True
                    If txtValor1Extraccion.Visible And txtValor1Extraccion.Enabled Then
                        txtValor1Extraccion.Focus()
                    Else
                        If txtValorExtraccion2.Enabled Then Me.txtValorExtraccion2.Focus()
                    End If

                Else
                    MsgBox("El orden ingresado no existe", MsgBoxStyle.Information, MDIContenedor.Text)
                    Me.txtordenExtracto.Text = ObtenerPestaniaExtracciones(_oCabecera.idExtraccionesCAB) + 1
                    LimpiarControles()
                    Me.txtordenExtracto.Enabled = True
                    txtordenExtracto.Focus()
                End If
            Else
                General.SoloNumeros(sender, e)
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Function BuscarRepetidos(ByVal oCabecera As ExtraccionesCAB, ByVal pValor As Integer, ByVal porden As Integer) As Boolean
        Dim _extraccionesDET As ExtraccionesDET
        Try
            BuscarRepetidos = False
            For Each _extraccionesDET In oCabecera.ExtraccionesDET
                If _extraccionesDET.Valor = pValor And _extraccionesDET.Orden <> porden Then
                    BuscarRepetidos = True
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MsgBox("Error buscarrepetidos:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    Function ControlarPosicion(ByVal oCabecera As ExtraccionesCAB, ByVal pPosicion As Integer, ByRef Msj As String) As Boolean
        Dim _extraccionesDET As ExtraccionesDET
        Try
            ControlarPosicion = True
            Msj = ""
            'si la cantidad es 0 quiere decir que el top es variable por lo cual no se controla el limite
            If oCabecera.ModeloExtraccionesDET.cantExtractos = 0 Then
                Exit Function
            End If
            If pPosicion > oCabecera.ModeloExtraccionesDET.cantExtractos Then
                Msj = "En '" & oCabecera.Titulo & "' sólo se permite un máximo de (" & oCabecera.ModeloExtraccionesDET.cantExtractos & ") posiciones."
                ControlarPosicion = False
                Exit Function
            End If
            For Each _extraccionesDET In oCabecera.ExtraccionesDET
                If _extraccionesDET.PosicionEnExtracto = pPosicion And _extraccionesDET.Valor <> -1 Then
                    Msj = "La posición ingresada ya existe en los valores ingresados."
                    ControlarPosicion = False
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MsgBox("Error ControlarPosicion:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    Function ControlarPosicionModificada(ByVal oCabecera As ExtraccionesCAB, ByVal pPosicion As Integer, ByRef pOrden As Integer) As Boolean
        Dim _extraccionesDET As ExtraccionesDET
        Try
            ControlarPosicionModificada = True
            For Each _extraccionesDET In oCabecera.ExtraccionesDET
                If _extraccionesDET.PosicionEnExtracto = pPosicion And _extraccionesDET.Valor <> -1 Then
                    pOrden = _extraccionesDET.Orden
                    ControlarPosicionModificada = False
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MsgBox("Error ControlarPosicionModificada:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub txtPosEnExtracto1_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtPosEnExtracto1.GotFocus
        txtPosEnExtracto1.SelectAll()
    End Sub

    Private Sub txtValor1Extraccion_GotFocus(ByVal sender As Object, ByVal e As System.EventArgs) Handles txtValor1Extraccion.GotFocus
        txtValor1Extraccion.SelectAll()
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
    Private Function ControlarFechasSorteo(ByVal FechaInicio As Date, ByVal FechaFin As Date) As Boolean
        If FechaInicio >= FechaFin Then
            ControlarFechasSorteo = False
        Else
            ControlarFechasSorteo = True
        End If
    End Function

    Private Sub btmConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btmConfirmar.Click
        Dim oCabecera As New ExtraccionesCAB
        Dim fechaHora As String
        Dim msj As String = ""

        oCabecera = TabExtracciones.SelectedTab.Tag

        'If Not ControlarFechasSorteo(DTPHoraInicioextraccion.Value, DTPHoraFinextraccion.Value) Then
        '    MsgBox("La hora de Inicio debe ser mayor a la hora de Fin", MsgBoxStyle.Information)
        '    Exit Sub
        'End If
        ''controla que las hora de los DTP con la hora de las bolillas ingresadas
        'If Not ControlaFechas(oCabecera, msj) Then
        '    MsgBox(msj, MsgBoxStyle.Information)
        '    Exit Sub
        'End If
        If Modificando Then
            If MsgBox("Quedan modificaciones pendientes de ser Guardadas. " & vbCrLf & " ¿Desea guardarlos ahora?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, MDIContenedor.Text) = MsgBoxResult.Yes Then
                GuardarDatosDB(True)
            End If
            Modificando = False
        End If
        If Not (MsgBox("Al confirmar la extracción, esta ya no podrá ser modificada." & vbCrLf & " ¿Desea confirmar de todos modos?", MsgBoxStyle.Question + MsgBoxStyle.YesNo, MDIContenedor.Text) = MsgBoxResult.Yes) Then
            Exit Sub
        End If
        '**01/10/20/2012  'si esta en modo de digitador doble,desconecto el puerto
        If CboPuertos.Visible Then
            DesconectaPuerto()
        End If

        'se cambia la forma de actualizar la fecha
        'la fecha de Inicio de la EXTRACCION se act5ualiza con la 1 bolilla
        'y la fecha de Fin con la FECHA ACTUAL al momento de confirmar
        oCabecera.FechaHoraIniReal = oCabecera.ExtraccionesDET(0).FechaHora

        oCabecera.FechaHoraFinReal = Now
        oCabecera.Ejecutada = 1

        ' ahora invoco a la capa de negocio para que ejecute la logica de la confirmacion de la extraccion
        Dim exBO As New ExtraccionesBO
        Dim proxEx As Long
        Dim horaFinConcurso As New DateTime

        Try
            ''btmConfirmar.Enabled = False
            ''btmModificar.Enabled = False
            gpbIngresoDatos.Enabled = False
            Me.Cursor = Cursors.WaitCursor
            FileSystemHelper.Log(" Concursoextracciones:confirma extraccion:" & oCabecera.Titulo)

            proxEx = exBO.Confirmar(oCabecera, horaFinConcurso)
            FileSystemHelper.Log("Concursoextracciones: confirmacion extraccion:" & oCabecera.Titulo & " OK")
            '**Averiguar si se tiene que habilitar el  boton extra de quini6
            If InStr(UCase(oCabecera.Titulo), "REVANCHA") > 0 Then
                btnExtra.Visible = True
                btnExtra.Enabled = True
            End If
            If proxEx = -1 Then
                ' Se terminaron las extracciones. Fin del sorteo.
                '** 05/11/2012 ***** 
                'se generan los extractos de juegos dependientes para que ya se vean en estado provisorio sin necesida de confirmar con concurso
                btmConfirmar.Enabled = False
                Try
                    FileSystemHelper.Log("Concursoextracciones: Intenta generar extractos de juegos dependientes:concurso:" & OPgmConcurso.idPgmConcurso & " j Principal:" & OPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego)
                    exBO.GenerarExtractoJuegosDependientes(OPgmConcurso.idPgmConcurso, OPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego)
                    FileSystemHelper.Log("Concursoextracciones: genero extractos de juegos dependientes:concurso:" & OPgmConcurso.idPgmConcurso & " j Principal:" & OPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego)
                Catch ex As Exception
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & "concursoextracciones: Problema GenerarExtractoJuegosDependientes en btnconfirmar:" & ex.Message)
                End Try

                btnFinalizar.Enabled = True
                btnFinalizar.Focus()
            Else
                HabilitarPestania(proxEx)
            End If
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox("Error btmConfirmar_Click:" & ex.Message, MDIContenedor.Text)
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & " concursoextracciones:Problema btnconfirmar:" & ex.Message)
        End Try

    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Try
            Modificando = False
            Dim ocabecera As ExtraccionesCAB
            ocabecera = TabExtracciones.SelectedTab.Tag
            If btmConfirmar.Enabled Then
                Me.txtordenExtracto.Text = ObtenerPestaniaExtracciones(ocabecera.idExtraccionesCAB)
            Else
                Me.txtordenExtracto.Text = ObtenerPestaniaExtracciones(ocabecera.idExtraccionesCAB) + 1
            End If
            'habilita campos de ingreso 
            Me.txtValor1Extraccion.Enabled = True
            Me.txtPosEnExtracto1.Enabled = True
            Me.txtValorExtraccion2.Enabled = True
            If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
            Me.txtPosEnExtracto2.Enabled = True
            If txtValorExtraccion2.Visible And txtValorExtraccion2.Enabled Then
                txtValorExtraccion2.Focus()
            Else
                If txtPosEnExtracto2.Enabled And txtPosEnExtracto2.Visible Then
                    txtPosEnExtracto2.Focus()
                End If
            End If
            '
            Me.cboMetodoIngreso.Enabled = True
            LimpiarControles()
            'si esta en modo de digitador doble,en la modificación habilito el teclado para que se siga con el ingreso
            If CboPuertos.Visible Then
                HabilitarTeclado()
                Me.txtPosEnExtracto2.Enabled = False
                Me.txtValorExtraccion2.Enabled = False
                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
            End If

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    Private Sub CrearListaExtraccionesSorteadash(ByVal oextraccionCAB As ExtraccionesCAB, ByVal CantidadCifras As Integer)
        Dim _extraccionesDET As ExtraccionesDET
        Dim cValorPosicion As New cValorPosicion
        Dim cVP As New cValorPosicion
        Dim _valorFormateado As Integer
        Dim _Encontrado As Boolean
        Try
            ListaExtraccionesSorteadas = Nothing
            For Each _extraccionesDET In oextraccionCAB.ExtraccionesDET
                If ListaExtraccionesSorteadas Is Nothing Then
                    ListaExtraccionesSorteadas = New ListaOrdenada(Of cValorPosicion)
                    cValorPosicion = New cValorPosicion
                    _valorFormateado = _extraccionesDET.Valor.Substring(Len(_extraccionesDET.Valor) - CantidadCifras, CantidadCifras)
                    cValorPosicion.Valor = _valorFormateado
                    cValorPosicion.Posicion = _extraccionesDET.PosicionEnExtracto
                    ListaExtraccionesSorteadas.Add(cValorPosicion)
                Else

                    _valorFormateado = _extraccionesDET.Valor.Substring(Len(_extraccionesDET.Valor) - CantidadCifras, CantidadCifras)
                    cValorPosicion = New cValorPosicion
                    For Each cVP In ListaExtraccionesSorteadas
                        If cVP.Valor = _valorFormateado Then
                            cValorPosicion.Valor = -1
                            _Encontrado = True
                            Exit For
                        End If
                    Next

                    '** si no esta en la lista los agrego
                    If Not _Encontrado Then
                        cValorPosicion.Valor = _valorFormateado
                    Else
                        _Encontrado = False
                    End If
                    cValorPosicion.Posicion = _extraccionesDET.PosicionEnExtracto
                    ListaExtraccionesSorteadas.Add(cValorPosicion)
                End If

            Next

        Catch ex As Exception
            MsgBox("Error CrearListaExtraccionesSorteadas:" & ex.Message, MDIContenedor.Text)
        End Try
    End Sub

    'Private Sub CompletarListaExtraccionesSorteadas(ByVal pvalor As String, Optional ByVal valoringresado As String = "")
    '    Dim cValorPosicion As New cValorPosicion
    '    Dim cVP As New cValorPosicion
    '    Try
    '        If ListaExtraccionesSorteadas Is Nothing Then
    '            Exit Sub
    '        End If

    '        For Each cValorPosicion In ListaExtraccionesSorteadas
    '            'si ya existe en la lista salgo
    '            For Each cVP In ListaExtraccionesSorteadas
    '                If cVP.Valor = pvalor Then
    '                    If valoringresado.Trim <> "" Then
    '                        cVP.Valor = valoringresado
    '                    End If
    '                    Exit Sub
    '                End If
    '            Next
    '            If cValorPosicion.Valor = -1 Then
    '                cValorPosicion.Valor = pvalor
    '                Exit For
    '            End If
    '        Next
    '    Catch ex As Exception
    '        MsgBox(ex.Message)
    '    End Try
    'End Sub
    Private Sub CompletarListaExtraccionesSorteadasDesdeLoad(ByVal pextraccionesCAB As ExtraccionesCAB)
        Dim extraccionesDET As New ExtraccionesDET
        Try
            If ListaExtraccionesSorteadas Is Nothing Then
                Exit Sub
            End If
            For Each extraccionesDET In pextraccionesCAB.ExtraccionesDET
                CompletarListaExtraccionesSorteadas(extraccionesDET.Valor, , , pextraccionesCAB)
            Next

        Catch ex As Exception
            MsgBox("CompletarListaExtraccionesSorteadasDesdeLoad:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Function ListaExtraccionesSorteadasCompleta() As Boolean
        Dim cValorPosicion As New cValorPosicion
        Try
            ListaExtraccionesSorteadasCompleta = True
            If ListaExtraccionesSorteadas Is Nothing Then
                ListaExtraccionesSorteadasCompleta = False
                Exit Function
            End If
            For Each cValorPosicion In ListaExtraccionesSorteadas
                If cValorPosicion.Valor = -1 Then
                    ListaExtraccionesSorteadasCompleta = False
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MsgBox("ListaExtraccionesSorteadasCompleta:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    Private Function ListaExtraccionesSorteadasCompletaModificada() As Boolean
        Dim cValorPosicion As New cValorPosicion
        Try
            ListaExtraccionesSorteadasCompletaModificada = True
            If ListaExtraccionesSorteadas Is Nothing Then
                ListaExtraccionesSorteadasCompletaModificada = False
                Exit Function
            End If
            For Each cValorPosicion In ListaExtraccionesSorteadasModificable
                If cValorPosicion.Valor = -1 Then
                    ListaExtraccionesSorteadasCompletaModificada = False
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MsgBox("ListaExtraccionesSorteadasCompletaModificada:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub txtValor1Extraccion_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValor1Extraccion.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtPosEnExtracto1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPosEnExtracto1.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            e.Handled = True
            SendKeys.Send("{TAB}")
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub TabExtracciones_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles TabExtracciones.SelectedIndexChanged
        Dim Index As Integer
        Try
            If btmConfirmar.Enabled Then
                HoraInicioExtraccionActual = DTPHoraInicioextraccion.Value
                HoraFinExtraccionActual = DTPHoraFinextraccion.Value
            End If
            If TabExtracciones.Controls.Count = 0 Then Exit Sub
            Index = TabExtracciones.TabPages.IndexOf(TabExtracciones.SelectedTab) + 1

            If Index = 1 Then 'es la primera
                If TabExtracciones.TabPages.Count = 1 Then 'hay una sola pestaña,no se habilita la navegación
                    Me.btnExtraccionAnterior.Enabled = False
                    Me.btnextraccionSiguiente.Enabled = False
                ElseIf TabExtracciones.TabPages.Count > 1 Then 'hay varias pero es la primera
                    Me.btnExtraccionAnterior.Enabled = False
                    Me.btnextraccionSiguiente.Enabled = True
                End If
            Else 'no es la primera
                If Index = TabExtracciones.TabPages.Count Then 'es la ultima
                    Me.btnExtraccionAnterior.Enabled = True
                    Me.btnextraccionSiguiente.Enabled = False
                Else 'es una pestaña del medio
                    Me.btnExtraccionAnterior.Enabled = True
                    Me.btnextraccionSiguiente.Enabled = True
                End If
            End If
            HabilitarControles()
        Catch ex As Exception
            MsgBox("TabExtracciones_SelectedIndexChanged:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub HabilitarControlesModificacion()
        Try
            txtordenExtracto.Enabled = True
            btnCancelar.Enabled = True
            If Me.cboMetodoIngreso.SelectedValue = General.MetodoIngreso.lecturaArchivo Then
                btnPorArchivo.Enabled = False
            End If
            Me.txtValor1Extraccion.Enabled = False
            Me.txtPosEnExtracto1.Enabled = False
            Me.txtValorExtraccion2.Enabled = False
            'If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
            Me.txtPosEnExtracto2.Enabled = False
            'deshabilito el metodo de ingreso para que  no haya errores de visualizacion
            Me.cboMetodoIngreso.Enabled = False
            'si esta en modo de digitador doble,en la modificación deshabilito el teclado
            If CboPuertos.Visible Then
                DesHabilitarTeclado()
            End If
            txtordenExtracto.Focus()
        Catch ex As Exception
            MsgBox("HabilitarControlesModificacion:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BTNSALIR.Click
        Me.Close()
    End Sub

    Private Sub cboMetodoIngreso_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboMetodoIngreso.SelectedIndexChanged
        Dim ocabecera As New ExtraccionesCAB
        Dim Indice As Integer
        Dim boextracciones As New ExtraccionesBO
        Try
            If TabExtracciones.TabPages.Count <> 0 Then
                ocabecera = TabExtracciones.SelectedTab.Tag
                Indice = cboMetodoIngreso.SelectedValue

                If ocabecera.MetodoIngreso.IDMetodoIngreso <> Indice And ocabecera.Ejecutada <> 1 Then
                    ActualizaMetodoIngresoCabecera(Indice)
                    'boextracciones.ActualizarMetodoIngreso(ocabecera.idExtraccionesCAB, Indice)
                End If
                HabilitaControlMetodIngreso(ocabecera, Indice)
            End If
        Catch ex As Exception
            MsgBox("cboMetodoIngreso_SelectedIndexChanged:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub HabilitaControlMetodIngreso(ByVal oCabecera As ExtraccionesCAB, Optional ByVal ValorCombo As Integer = -1)
        Try
            Dim arbo As New ArchivoBoldtBO
            Dim bopgmsorteo As New PgmSorteoBO
            Dim pgmsorteo As New PgmSorteo
            Dim path_archivo As String = General.CarpetaOrigenArchivosExtractoBoldt
            Dim nro As String = ""
            Dim fecha
            Dim _confirmada As Boolean = False
            fecha = General.Es_Nulo(Of Date)(oCabecera.FechaHoraFinReal, "01/01/1999")
            If fecha <> "01/01/1999" Then
                _confirmada = True
            Else

                _confirmada = False
            End If
            If btmConfirmar.Enabled Then
                _confirmada = True
            End If
            nro = CrearFormatoCifras(oCabecera.ModeloExtraccionesDET.cantCifras)
            If ValorCombo = -1 Then 'no se envia el valor del combo
                If oCabecera.MetodoIngreso.IDMetodoIngreso <> General.MetodoIngreso.lecturaArchivo Then
                    Me.btnPorArchivo.Visible = False
                Else
                    If General.Obtener_pgmsorteos_ws = "S" Then
                        pgmsorteo = bopgmsorteo.getPgmSorteo(oCabecera.idPgmConcurso)
                        If Not path_archivo.EndsWith("\") Then
                            path_archivo = path_archivo & "\"
                        End If
                        path_archivo = path_archivo & arbo.CreaNombreArchivoExtracto(pgmsorteo.idJuego, pgmsorteo.nroSorteo) & ".zip"
                        ' si no existe el archivo,trat de crearlo desde el ws
                        If Not File.Exists(path_archivo) Then
                            arbo.Generar_archivosExtracto_y_Control_por_WS(oCabecera.idPgmConcurso)
                        End If


                    End If
                    Me.btnPorArchivo.Visible = True

                End If
                If oCabecera.MetodoIngreso.IDMetodoIngreso <> General.MetodoIngreso.digitacionDobleTecladoSimple Then
                    Me.lblingreso1.Visible = False
                    Me.txtPosEnExtracto1.Visible = False
                    Me.txtValor1Extraccion.Visible = False
                    'acomodo los text del ingreso 2
                    Me.lblingreso2.Text = "  INGRESO:"
                    IngresoDigitador = False
                    If oCabecera.MetodoIngreso.IDMetodoIngreso = General.MetodoIngreso.digitacionSimpleTecladoDoble Then
                        Me.BtnConectar.Visible = True
                        Me.CboPuertos.Visible = True
                        Me.btnSonido.Visible = True
                        IngresoDigitador = True
                        LblTeclados.Visible = True
                        Me.lblValor.Top = 115
                        Me.lblPosicion.Top = 115
                        txtPosEnExtracto2.Text = ""
                        Me.txtPosEnExtracto2.Enabled = False
                        Me.txtValorExtraccion2.Enabled = False
                        If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                        SeteaComboPuerto(PuertoSerieActual)
                        'CboPuertos.SelectedValue = "COM20"
                        If _confirmada Then
                            BtnConectar.Enabled = False
                            CboPuertos.Enabled = False
                            btnSonido.Enabled = False
                        Else
                            BtnConectar.Enabled = True
                            CboPuertos.Enabled = True
                            btnSonido.Enabled = True
                        End If
                        If BtnConectar.Enabled Then BtnConectar.Focus()

                    Else
                        '**05/10/2012
                        If CboPuertos.Visible Then
                            DesconectaPuerto()
                        End If
                        Me.lblValor.Top = 130
                        Me.lblPosicion.Top = 130
                        Me.BtnConectar.Visible = False
                        Me.CboPuertos.Visible = False
                        Me.btnSonido.Visible = False
                        LblTeclados.Visible = False
                        Me.txtPosEnExtracto2.Enabled = True
                        Me.txtValorExtraccion2.Enabled = True
                        If btnPorArchivo.Visible = True Then
                            If _confirmada Then
                                btnPorArchivo.Enabled = False
                            Else
                                btnPorArchivo.Enabled = True
                            End If

                        End If

                        Me.txtPosEnExtracto2.Enabled = True
                        If Me.GroupBoxIngreso.Enabled Then
                            If txtValorExtraccion2.Text.Trim = "" And nro.Trim <> "00" Then
                                txtValorExtraccion2.Text = nro
                            End If
                        End If
                        If _confirmada = False Then
                            txtValorExtraccion2.Enabled = True
                            If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                        Else
                            txtValorExtraccion2.Enabled = False
                            If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                        End If
                        txtValorExtraccion2.SelectAll()
                        If txtValorExtraccion2.Enabled Then txtValorExtraccion2.Focus()
                    End If

                Else 'ingreso doble
                    '**05/10/2012
                    If CboPuertos.Visible Then
                        DesconectaPuerto()
                    End If
                    Me.BtnConectar.Visible = False
                    Me.CboPuertos.Visible = False
                    Me.btnSonido.Visible = False
                    LblTeclados.Visible = False
                    Me.txtPosEnExtracto2.Enabled = True
                    Me.txtValorExtraccion2.Enabled = True
                    If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                    Me.lblingreso1.Visible = True
                    Me.txtPosEnExtracto1.Visible = True
                    Me.txtValor1Extraccion.Visible = True
                    Me.lblingreso2.Text = "INGRESO 2:"
                    Me.lblValor.Top = 73
                    Me.lblPosicion.Top = 73
                    If Me.GroupBoxIngreso.Enabled Then
                        If txtValor1Extraccion.Text.Trim = "" Then
                            txtValorExtraccion2.Text = ""
                            If nro.Trim <> "00" Then
                                txtValor1Extraccion.Text = nro
                            End If
                        End If
                    End If
                    If _confirmada = False Then
                        txtValor1Extraccion.Enabled = True
                        txtValorExtraccion2.Enabled = True
                        If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                    Else
                        txtValor1Extraccion.Enabled = False
                        txtValorExtraccion2.Enabled = False
                        If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                    End If
                    txtValor1Extraccion.SelectAll()
                    If txtValor1Extraccion.Visible Then txtValor1Extraccion.Focus()
                    End If
                    cboMetodoIngreso.SelectedValue = oCabecera.MetodoIngreso.IDMetodoIngreso
                    If oCabecera.MetodoIngreso.IDMetodoIngreso = General.MetodoIngreso.lecturaBolilleros Then
                        Me.gpbIngresoDatos.Enabled = False
                    Else
                        Me.gpbIngresoDatos.Enabled = True
                    End If
            Else '** la configuracion se cambia desde tiempo de ejecucion
                    IngresoDigitador = False
                    Select Case ValorCombo
                        Case 1, 3, 4
                            If oCabecera.MetodoIngreso.IDMetodoIngreso <> General.MetodoIngreso.lecturaArchivo Then
                                Me.btnPorArchivo.Visible = False
                            Else
                                If _confirmada Then
                                    Me.btnPorArchivo.Enabled = False
                                End If
                                Me.btnPorArchivo.Visible = True
                            End If
                            Me.lblingreso1.Visible = False
                            Me.txtPosEnExtracto1.Visible = False
                            Me.txtValor1Extraccion.Visible = False
                            'acomodo los lbl del ingreso 2
                            Me.lblingreso2.Text = "  INGRESO:"

                            If ValorCombo = 3 Then
                                Me.lblValor.Top = 115
                                Me.lblPosicion.Top = 115
                                Me.BtnConectar.Visible = True
                                Me.CboPuertos.Visible = True
                                Me.btnSonido.Visible = True
                                LblTeclados.Visible = True
                                IngresoDigitador = True
                                SeteaComboPuerto(PuertoSerieActual)
                                'CboPuertos.SelectedValue = "COM20"
                                Me.txtPosEnExtracto2.Enabled = False
                                Me.txtValorExtraccion2.Enabled = False
                                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                                txtValorExtraccion2.Text = ""
                                If _confirmada Then
                                    BtnConectar.Enabled = False
                                    CboPuertos.Enabled = False
                                    btnSonido.Enabled = False
                                Else
                                    BtnConectar.Enabled = True
                                    CboPuertos.Enabled = True
                                    btnSonido.Enabled = True
                                End If
                                If BtnConectar.Enabled Then BtnConectar.Focus()
                        Else
                            Me.lblValor.Top = 130

                            Me.lblPosicion.Top = 130
                            DesconectaPuerto()
                            Me.BtnConectar.Visible = False
                            Me.CboPuertos.Visible = False
                            Me.btnSonido.Visible = False
                            LblTeclados.Visible = False
                            Me.txtPosEnExtracto2.Enabled = True
                            txtValorExtraccion2.Enabled = True
                            If btnPorArchivo.Visible = True Then
                                If _confirmada Then
                                    btnPorArchivo.Enabled = False
                                Else
                                    btnPorArchivo.Enabled = True
                                End If
                            End If

                            If txtValorExtraccion2.Text.Trim = "" Then
                                txtValor1Extraccion.Text = ""
                                If nro.Trim <> "00" Then
                                    txtValorExtraccion2.Text = nro
                                End If
                            End If
                            If _confirmada = False Then
                                txtValorExtraccion2.Enabled = True
                                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                            Else
                                txtValorExtraccion2.Enabled = False
                                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                            End If
                            txtValorExtraccion2.SelectAll()
                            If txtValorExtraccion2.Enabled Then txtValorExtraccion2.Focus()

                            End If
                        Case 2 'ingreso doble
                            DesconectaPuerto()
                            Me.BtnConectar.Visible = False
                            Me.CboPuertos.Visible = False
                            Me.btnSonido.Visible = False
                            LblTeclados.Visible = False
                            Me.lblingreso1.Visible = True
                            If oCabecera.ModeloExtraccionesDET.sorteaPosicion Then
                                Me.txtPosEnExtracto1.Visible = True
                            End If
                            Me.txtValor1Extraccion.Visible = True
                            Me.lblingreso2.Text = "INGRESO 2:"
                            Me.lblValor.Left = 132
                            Me.lblValor.Top = 73
                            Me.lblPosicion.Left = 216
                            Me.lblPosicion.Top = 73
                            Me.txtPosEnExtracto2.Enabled = True
                            Me.txtValorExtraccion2.Enabled = True
                            If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                            If txtValor1Extraccion.Text.Trim = "" Then
                                txtValorExtraccion2.Text = ""
                                txtValor1Extraccion.Text = nro
                            End If
                            If _confirmada = False Then
                                txtValor1Extraccion.Enabled = True
                                txtValorExtraccion2.Enabled = True
                                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = True
                            Else
                                txtValor1Extraccion.Enabled = False
                                txtValorExtraccion2.Enabled = False
                                If btnPorArchivo.Visible = True Then btnPorArchivo.Enabled = False
                            End If

                            txtValor1Extraccion.SelectAll()
                            If txtValor1Extraccion.Visible Then txtValor1Extraccion.Focus()
                        Case Else
                            Me.gpbIngresoDatos.Enabled = False
                    End Select
            End If
        Catch ex As Exception
            MsgBox("HabilitaControlMetodIngreso:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub ActualizaMetodoIngresoCabecera(ByVal pIdMetodoIngreso As Integer)
        Dim ocabecera As ExtraccionesCAB
        Try
            ocabecera = TabExtracciones.SelectedTab.Tag
            ocabecera.MetodoIngreso.IDMetodoIngreso = pIdMetodoIngreso
        Catch ex As Exception
            MsgBox("ActualizaMetodoIngresoCabecera:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Sub btnFinalizar_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizar.Click
        ' ahora invoco a la capa de negocio para que ejecute la logica de la confirmacion de la extraccion
        Dim PgmBO As New PgmConcursoBO
        Dim ocabecera As ExtraccionesCAB
        Dim _confirmadoOK As Boolean = False
        '**30/10/2012 varaiable para saber si se generaron bien los archivos para boldt
        Dim _GeneracionOK As Boolean = False
        Dim oPgmSorteoBo As New PgmSorteoBO
        Dim opgmsorteo As New PgmSorteo
        Dim _errorPublicar As Boolean = False
        Dim _Huboerror As Boolean = False
        Dim _msjGeneracionArchivo As String = ""
        Dim _msjProgresion As String = ""
        Dim _msjPublicacionWeb As String = ""
        Dim msjErrores As String = ""
        Dim _PublicarWebON = General.PublicarWebON
        Dim _PublicaExtractosWSRestON = General.PublicaExtractosWSRestON
        Dim _PublicaExtractosWSRestOFF = General.PublicaExtractosWSRestOFF
        Dim _progresion As Integer = 0
        Try
            DesconectaPuerto()
            ocabecera = TabExtracciones.SelectedTab.Tag
            btnFinalizar.Enabled = False
            Me.Cursor = Cursors.WaitCursor

            FileSystemHelper.Log("concursoextracciones:llama a Finalizar sorteo , sorteo:" & ocabecera.Titulo)
            If PgmBO.Finalizar(ocabecera.idPgmConcurso) Then
                _confirmadoOK = True
                FileSystemHelper.Log("concursoextracciones:Finalizar sorteo OK, sorteo:" & ocabecera.Titulo)
                '*** 06/11/2012 se calcula el campo progresion para la loteria y actualiza la base para que se muestre en el display
                Try
                    For Each opgmsorteo In OPgmConcurso.PgmSorteos
                        'actualiza el pgmdesorteo de suite
                        If General.Obtener_pgmsorteos_ws = "S" Then
                            Try
                                oPgmSorteoBo.setSorteadoPgmconcurso(opgmsorteo.idPgmSorteo)
                            Catch exWS As Exception
                            End Try
                        End If
                        '*** 26/09/2013 HG se calcula la progresion solo para santafe
                        If opgmsorteo.idJuego = 50 And General.Jurisdiccion = "S" Then
                            ActualizaProgresionLoteria(opgmsorteo, , _progresion)
                            
                            Exit For
                        End If
                    Next
                Catch ex As Exception
                    _Huboerror = True
                    _msjProgresion = "- Problemas al actualizar la progresión de lotería."
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & "concursoextracciones: Problema ActualizaProgresionLoteria en btnfinalizar_clik:" & ex.Message)
                End Try
               
                '*** 30/10/2012 se generan los archivos de extracto para boldt para los juegos
                '4,13,30,50,51

                Dim archivoBO As New ArchivoBoldtBO
                Dim PathArchivo As String = General.CarpetaArchivosBoldt
                Try

                    For Each opgmsorteo In OPgmConcurso.PgmSorteos
                        If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Or opgmsorteo.idJuego = 50 Or opgmsorteo.idJuego = 51 Or (opgmsorteo.idJuego = 1 And _
                                                                                                                                                                  (OPgmConcurso.concurso.IdConcurso = 6 Or _
                                                                                                                                                                   OPgmConcurso.concurso.IdConcurso = 7 Or _
                                                                                                                                                                   OPgmConcurso.concurso.IdConcurso = 10 Or _
                                                                                                                                                                   OPgmConcurso.concurso.IdConcurso = 11 Or _
                                                                                                                                                                   OPgmConcurso.concurso.IdConcurso = 14 Or _
                                                                                                                                                                   OPgmConcurso.concurso.IdConcurso = 15 _
                                                                                                                                                                   ) _
                                                                                                                                                                   ) Then
                            If archivoBO.Tiene_que_generar_archivoextracto(opgmsorteo.idJuego) Then
                                archivoBO.GenerarArchivoExtracto(opgmsorteo, PathArchivo)
                                Try
                                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & " concursoextracciones: GenerarArchivoExtracto OK :Sorteo:" & opgmsorteo.idPgmSorteo & " path:" & PathArchivo)
                                Catch ex As Exception
                                End Try
                            End If
                        End If
                    Next
                    _GeneracionOK = True
                Catch ex As Exception
                    _Huboerror = True
                    _msjGeneracionArchivo = "- No se pudieron generar los archivos de extractos para Boldt."
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & " concursoextracciones:Problema GenerarArchivoExtracto en btnfinalizar_clik:" & ex.Message)
                End Try
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Try
            Catch ex1 As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & "concursoextracciones: btnFinalizar.Click -> Finalizar concurso -> " & ex.Message)
            End Try
        End Try


        ' PUBLICO A LA WEB

        'AGREGADO POR FSCOTTA
        If _PublicaExtractosWSRestON = "S" Or _PublicaExtractosWSRestOFF = "S" Then
            Try

                PgmBO.publicarWEB(OPgmConcurso)

                'FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Finalizar Extracciones: La publicación on line a la Web no está habilitada. _PublicaExtractosWSRestON: " & _PublicaExtractosWSRestON & ".")

            Catch ex As Exception
                Me.Cursor = Cursors.Default
                Try
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & "concursoextracciones: btnFinalizar.Click -> Publicación a web -> " & ex.Message)
                Catch ex1 As Exception
                End Try
                _errorPublicar = True
                _msjPublicacionWeb = "- Problemas en la publicación a la Web.Para actualizar la Web , ingrese desde el menú Interfaces."
            End Try
        End If
        '-----------------------------------
        Try
            If _PublicarWebON = "S" Then ' corresponde on line
                PgmBO.publicarWEB(OPgmConcurso)
                'Actualizo el pozo estimado prox sorteo si corresponde
                For Each opgmsorteo In OPgmConcurso.PgmSorteos
                    If opgmsorteo.idJuego = 30 Then
                        opgmsorteo = oPgmSorteoBo.getPgmSorteo(opgmsorteo.idPgmSorteo)
                        oPgmSorteoBo.ActualizarSorteoWeb(opgmsorteo, opgmsorteo.fechaHoraPrescripcion, opgmsorteo.fechaHoraProximo, opgmsorteo.fechaHoraIniReal, opgmsorteo.fechaHora, opgmsorteo.fechaHoraFinReal, opgmsorteo.PozoEstimado)
                        Exit For
                    End If
                Next
            Else
                Try
                    FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & "concursoextracciones: Finalizar Extracciones: La publicación on line a la Web no está habilitada. PublicarWebON: " & _PublicarWebON & ".")
                Catch ex As Exception
                End Try
            End If

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            Try
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & " concursoextracciones:btnFinalizar.Click -> Publicación a web -> " & ex.Message)
            Catch ex1 As Exception
            End Try
            _errorPublicar = True
            _msjPublicacionWeb = "- Problemas en la publicación a la Web.Para actualizar la Web , ingrese desde el menú Interfaces."
        End Try


        Me.Cursor = Cursors.Default
        If _confirmadoOK Then
            '** si existe error informo al usuario
            If _Huboerror Then
                msjErrores = "El concurso se ha finalizado correctamente pero se presentaron las siguientes situaciones:" & vbCrLf
                If _msjGeneracionArchivo.Trim <> "" Then
                    msjErrores = msjErrores & _msjGeneracionArchivo & vbCrLf
                End If
                If _msjProgresion.Trim <> "" Then
                    msjErrores = msjErrores & _msjProgresion & vbCrLf
                End If
                If _msjPublicacionWeb.Trim <> "" Then
                    msjErrores = msjErrores & _msjPublicacionWeb & vbCrLf
                End If
                '** emite el listado de extracciones
                ListarParametros()
                MsgBox(msjErrores, MsgBoxStyle.Exclamation, MDIContenedor.Text)
                'MsgBox("El concurso se ha finalizado correctamente pero hubo problemas al generar archivos para Boldt." & vbCrLf & "Para generar los archivos para Boldt, ingrese al menú Interfaces.", MsgBoxStyle.Information)
            Else
                '** emite el listado de extracciones
                ListarParametros()
                MsgBox("El concurso se ha finalizado correctamente.", MsgBoxStyle.Information, MDIContenedor.Text)
            End If

        Else
            Try
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Now.ToString("dd/MM/yyyy  HH:mm:ss") & " concursoextracciones: btnFinalizar.Click -> Confirmado OK es falso")
            Catch ex As Exception
            End Try
        End If
        ' '' HABILITO PARA SEGUIR
        ''Try
        ''    HabilitarControlesConcurso()
        ''Catch ex As Exception
        ''    Me.Cursor = Cursors.Default
        ''    MsgBox("btnFinalizar_Click:" & ex.Message, MsgBoxStyle.Information)
        ''End Try
        'cierra ventana al finalizar concurso
        Try
            ' Me.Dispose()
            Me.Close()
        Catch ex As Exception
        End Try
    End Sub

    Private Sub ActualizarPestaniaExtracciones(ByVal IdExtraccionesCAB As Integer, ByVal Nroextracciones As Integer)
        Dim Pestania As cPestaniaExtracciones
        Dim encontrado As Boolean
        Try
            If ListaPestaniaExtracciones Is Nothing Then
                Pestania = New cPestaniaExtracciones
                ListaPestaniaExtracciones = New List(Of cPestaniaExtracciones)
                Pestania.IdPestania = IdExtraccionesCAB
                Pestania.NroExtracciones = Nroextracciones
                ListaPestaniaExtracciones.Add(Pestania)
            Else
                encontrado = False
                For Each Pestania In ListaPestaniaExtracciones
                    If Pestania.IdPestania = IdExtraccionesCAB Then
                        Pestania.NroExtracciones = Nroextracciones
                        encontrado = True
                        Exit Sub
                    End If
                Next
                If encontrado = False Then
                    Pestania = New cPestaniaExtracciones
                    Pestania.IdPestania = IdExtraccionesCAB
                    Pestania.NroExtracciones = Nroextracciones
                    ListaPestaniaExtracciones.Add(Pestania)
                End If
            End If
        Catch ex As Exception
            MsgBox("ActualizarPestaniaExtracciones:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Function ObtenerPestaniaExtracciones(ByVal IdExtraccionesCAB As Integer) As Integer
        Dim Pestania As cPestaniaExtracciones
        Try
            If ListaPestaniaExtracciones Is Nothing Then
                ObtenerPestaniaExtracciones = 1
                Exit Function
            Else
                For Each Pestania In ListaPestaniaExtracciones
                    If Pestania.IdPestania = IdExtraccionesCAB Then
                        ObtenerPestaniaExtracciones = Pestania.NroExtracciones
                        Exit Function
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ObtenerPestaniaExtracciones:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub btnRevertirExtraccion_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevertirExtraccion.Click
        ' ahora invoco a la capa de negocio para que ejecute la logica de la confirmacion de la extraccion
        Dim PgmBO As New PgmConcursoBO
        Dim ocabecera As ExtraccionesCAB
        Dim idExtraccionSig As Integer
        Dim ventana As New frmConfirmacionRevertir
        Dim _modalidad As Integer = -1
        Dim index As Integer
        Try
            ocabecera = TabExtracciones.SelectedTab.Tag
            index = TabExtracciones.TabPages.IndexOf(TabExtracciones.SelectedTab) + 1
            'If MsgBox("¿Desea revertir la extracción '" & ocabecera.ModeloExtraccionesDET.Nombre & "'?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then
            '    Exit Sub
            'End If
            ventana._NombreExtraccion = ocabecera.ModeloExtraccionesDET.Nombre
            ventana.ShowDialog()
            If ventana._Cancelado = True Then
                Exit Sub
            End If
            FileSystemHelper.Log("concursoextracciones:ingreso a revertir extraccion:" & ocabecera.ModeloExtraccionesDET.Nombre)
            _modalidad = ventana._Modalidad
            DesconectaPuerto()
            idExtraccionSig = PgmBO.RevertirExtracciones(OPgmConcurso, OPgmConcurso.Operador, ocabecera.idExtraccionesCAB, _modalidad)
            FileSystemHelper.Log("concursoextracciones:reversión extraccion:" & ocabecera.ModeloExtraccionesDET.Nombre & " OK")
            LimpiarControles()
            If CboPuertos.Visible Then
                DesHabilitarTeclado()
            End If
            TabExtracciones.Controls.Clear()
            OPgmConcurso = PgmBO.getPgmConcurso(OPgmConcurso.idPgmConcurso)
            CrearControles(OPgmConcurso)
            If ocabecera.ModeloExtraccionesDET.Obligatoria = False Then
                ActualizaExtraccionesRepetidas(OPgmConcurso)
            End If
            'vuelve a la pestaña que se revirtio
            TabExtracciones.TabIndex = index

        Catch ex As Exception
            MsgBox("btnRevertirExtraccion_Click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
            FileSystemHelper.Log("concursoextracciones: problemas reversión extraccion:" & ex.Message)
        End Try
    End Sub

    Private Sub HabilitarControlesConcurso(Optional ByVal Habilitar As Boolean = False)
        Try

            If Not Habilitar Then
                Me.GpbConfirmarExtraccion.Enabled = False
                Me.gpbIngresoDatos.Enabled = False
                Me.cboMetodoIngreso.Enabled = False
                Me.btnFinalizar.Enabled = False
            Else
                Me.GpbConfirmarExtraccion.Enabled = True
                Me.gpbIngresoDatos.Enabled = True
                Me.cboMetodoIngreso.Enabled = True
                'Me.btnFinalizar.Enabled = True
            End If
        Catch ex As Exception
            MsgBox("HabilitarControlesConcurso:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
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
            Return Nothing
            MsgBox("CrearEtiqueta:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Function CrearText(ByVal pNombre As String, ByVal pTexto As String, ByVal pLef As Integer, ByVal pTop As Integer, ByVal pAncho As Integer, ByVal pAlineacion As Integer, Optional ByVal Fuente As Font = Nothing, Optional ByVal pVisible As Boolean = True, Optional ByVal pEnabled As Boolean = False, Optional ByVal pSoloLectura As Boolean = True, Optional ByVal ancla As Integer = -1, Optional ByVal RGB As String = "") As TextBox
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
            Select Case ancla
                Case 1
                    texto.Anchor = AnchorStyles.Left Or AnchorStyles.Top
                Case 2
                    'texto.Anchor = AnchorStyles.Right Or AnchorStyles.Bottom Or AnchorStyles.Top
            End Select
            texto.BorderStyle = BorderStyle.Fixed3D
            If RGB.Trim <> "" Then
                Colores = RGB.Split(",")
                Rojo = Colores(0)
                Verde = Colores(1)
                Azul = Colores(2)
                texto.ForeColor = Color.FromArgb(Rojo, Verde, Azul)
            End If
            texto.BorderStyle = BorderStyle.None
            Return texto
        Catch ex As Exception
            Return Nothing
            MsgBox("CrearText:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Function
    Private Function ExisteControl(ByVal pControl As Control, ByVal pNOmbreControl As String) As Boolean
        Dim ControlNuevo As Control
        Try
            ExisteControl = False
            If pControl.Controls.Count > 0 Then
                For Each ControlNuevo In pControl.Controls
                    If ControlNuevo.Name = pNOmbreControl Then
                        ExisteControl = True
                        Exit Function
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("ExisteControl:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub btnRevertir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRevertir.Click
        ' ahora invoco a la capa de negocio para que ejecute la logica de la confirmacion de la extraccion
        Dim PgmBO As New PgmConcursoBO
        Dim sorteoBO As New PgmSorteoBO
        Dim ocabecera As ExtraccionesCAB
        Dim idExtraccionSig As Integer
        Dim oSorteo As New PgmSorteo
        Dim ventana As New frmConfirmacionRevertir
        Dim _modalidad As Integer = -1
        Dim _listaJuegos As String = ""
        Dim arbo As New ArchivoBoldtBO
        Try
            ocabecera = TabExtracciones.SelectedTab.Tag

            'If MsgBox("¿Desea revertir el Sorteo '" & OPgmConcurso.concurso.Descripcion & "'?", MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then
            If MsgBox("¿Desea revertir el sorteo '" & OPgmConcurso.nombre & "'?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, MDIContenedor.Text) = MsgBoxResult.No Then
                Exit Sub
            End If
            FileSystemHelper.Log("Concursoextracciones:Ingreso a reversion de sorteo :" & ocabecera.Titulo)
            If PgmBO.Concurso_con_PremiosConfirmados(OPgmConcurso, _listaJuegos) = True Then
                ' Inc 2892 -> Si hago reversion de un extracto en noct c/poc c/tom o loteria, el cambiar las bolillas puede hacer que cambien todos los extractos. Por eso obligo a que reviertan total.
                MsgBox("El sorteo que intenta revertir ya contiene extractos confirmados. Para modificarlo primero debe Revertir TODOS los Extractos. Luego vuelva a intentar.", MsgBoxStyle.Information, MDIContenedor.Text)
                FileSystemHelper.Log("Concursoextracciones:El sorteo que intenta revertir ya contiene extractos confirmados. Para modificarlo primero debe Revertir TODOS los Extractos, a travÃ©s del MenÃº Interfaces->Revertir Concurso - Opcion Extracto: TODOS. Luego vuelva a intentar.")
                Exit Sub
                ' Inc 2892 -> lo que sigue ya no se ejecuta:
                If MsgBox("En el sorteo '" & OPgmConcurso.nombre & "' existen juegos que ya fueron confirmados. Se anularán estas confirmaciones. ¿Desea continuar?", MsgBoxStyle.YesNo + MsgBoxStyle.Question, MDIContenedor.Text) = MsgBoxResult.No Then
                    FileSystemHelper.Log("Concursoextracciones:En el sorteo '" & OPgmConcurso.nombre & "' existen juegos que ya fueron confirmados. Se anularán estas confirmaciones.NO continuo.")
                    Exit Sub
                End If
                'se anulan en la web los sorteos ya conmfirmados para el concurso a revertir
                If AnularSorteos(_listaJuegos) = False Then
                    MsgBox("Hubo errores al anular sorteos confirmados.", MsgBoxStyle.Information, MDIContenedor.Text)
                    FileSystemHelper.Log("Concursoextracciones:Hubo errores al anular sorteos confirmados.")
                    Exit Sub
                End If
                ' Fin Inc 2892 -> lo que sigue ya no se ejecuta:
            End If
            '20-11-15 HG - borra el archivo de extracto si existe para que el ws lo vuelva tomar
            If General.Obtener_pgmsorteos_ws = "S" Then
                Dim path_archivo As String = General.CarpetaOrigenArchivosExtractoBoldt
                Dim _opgmsorteo As New PgmSorteo
                For Each _opgmsorteo In OPgmConcurso.PgmSorteos
                    If _opgmsorteo.idJuego = 4 Or _opgmsorteo.idJuego = 13 Then
                        If Not path_archivo.EndsWith("\") Then
                            path_archivo = path_archivo & "\"
                        End If
                        path_archivo = path_archivo & arbo.CreaNombreArchivoExtracto(_opgmsorteo.idJuego, _opgmsorteo.nroSorteo) & ".zip"
                        ' borra el archivo
                        If File.Exists(path_archivo) Then
                            FileSystemHelper.BorrarArchivo(path_archivo)
                        End If
                    End If
                Next
                
            End If

            'ventana._NombreExtraccion = ocabecera.ModeloExtraccionesDET.Nombre
            ventana._NombreExtraccion = OPgmConcurso.nombre
            ventana.ShowDialog()
            If ventana._Cancelado = True Then
                Exit Sub
            End If
            _modalidad = ventana._Modalidad

            DesconectaPuerto()
            Me.Cursor = Cursors.WaitCursor
            Try
                idExtraccionSig = PgmBO.RevertirExtracciones(OPgmConcurso, OPgmConcurso.Operador, -1, _modalidad) 'no se envia la cabecera para que se revierta todo el concurso
                FileSystemHelper.Log("Concursoextracciones:Reversion sorteo OK,concurso:" & ocabecera.Titulo)

            Catch ex As Exception
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Concursoextracciones: btnRevertir.Click -> Llamado a RevertirExtracciones -> " & ex.Message)
            End Try
            ' REVIERTE A LA WEB
            Try
                FileSystemHelper.Log("Concursoextracciones:llama a Reversion WEB ,sorteo:" & ocabecera.Titulo)
                sorteoBO.revertirWeb(oSorteo)
                FileSystemHelper.Log("Concursoextracciones:Reversion WEB OK,sorteo:" & ocabecera.Titulo)
            Catch ex As Exception
                Me.Cursor = Cursors.Default
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " Concursoextracciones: btnFinalizar.Click -> Publicación a web -> " & ex.Message)
            End Try

            TabExtracciones.Controls.Clear()
            OPgmConcurso = PgmBO.getPgmConcurso(OPgmConcurso.idPgmConcurso)
            CrearControles(OPgmConcurso)
            btnRevertir.Enabled = False
            HabilitarControlesConcurso(True)
            btnFinalizar.Enabled = False
            Me.Cursor = Cursors.Default
        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox("btnRevertir_Click: " & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub cboIraExtraccion_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cboIraExtraccion.SelectedIndexChanged
        Dim indice As Integer
        Try
            indice = cboIraExtraccion.Items.IndexOf(cboIraExtraccion.SelectedItem)
            SeleccionarPestania(indice)
        Catch ex As Exception
            MsgBox("cboIraExtraccion_SelectedIndexChanged:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub SeleccionarPestania(ByVal index As Integer)
        Try
            TabExtracciones.SelectedIndex = index
        Catch ex As Exception
            MsgBox("SeleccionarPestania:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    Private Sub btnextraccionSiguiente_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnextraccionSiguiente.Click
        Dim indice As Integer
        Try
            indice = TabExtracciones.SelectedIndex
            indice = indice + 1
            TabExtracciones.SelectedIndex = indice
        Catch ex As Exception
            MsgBox("btnextraccionSiguiente_Click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub

    Private Sub btnExtraccionAnterior_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtraccionAnterior.Click
        Dim indice As Integer
        Try
            indice = TabExtracciones.SelectedIndex
            indice = indice - 1
            TabExtracciones.SelectedIndex = indice
        Catch ex As Exception
            MsgBox("btnExtraccionAnterior_Click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Sub

    Private Sub btnListarParametros_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListarParametros.Click
        ListarParametros()

    End Sub

    Private Function getPgmConcursoElegido(ByRef idPgmC As Long) As PgmConcurso
        Try

            For Each p As PgmConcurso In OPgmConcursos
                If p.idPgmConcurso = idPgmC Then
                    Return p
                    Exit Function
                End If
            Next
            Return Nothing
        Catch ex As Exception
            MsgBox("getPgmConcursoElegido:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub CboConcurso_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CboConcurso.SelectedIndexChanged
        Dim i As Integer
        Try


            i = CboConcurso.SelectedValue
            If Not _estoyEnLoad Then
                OPgmConcurso = getPgmConcursoElegido(i)

                If OPgmConcurso IsNot Nothing Then
                    ActualizarPanelConcurso()
                End If
            End If
            If Me.txtValor1Extraccion.Visible Then
                Me.txtValor1Extraccion.Focus()
            End If
        Catch ex As Exception
            MsgBox("CboConcurso_SelectedIndexChanged:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Function ControlesRepetidos(ByVal _valor As Integer, ByVal oCabecera As ExtraccionesCAB, ByRef msj As String) As Boolean
        Dim oextractoDet As ExtraccionesDET
        Try
            ControlesRepetidos = True
            If oCabecera.ModeloExtraccionesDET.AdmiteRepetidos = False Then
                For Each oextractoDet In oCabecera.ExtraccionesDET
                    If oextractoDet.Valor = _valor Then
                        msj = "El número ingresado ya se fue sorteado."
                        ControlesRepetidos = False
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox("Error :" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Function

    Private Sub BtnExtra_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnExtra.Click
        Try
            PremioExtra.idPgmConcurso = OPgmConcurso.idPgmConcurso
            PremioExtra.NroSorteo = OPgmConcurso.PgmSorteos(0).nroSorteo
            PremioExtra.Show()

        Catch ex As Exception
            MsgBox("Error BtnExtra_Click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Function CompletaColumna(ByVal _ocabecera As ExtraccionesCAB) As Boolean
        Try
            CompletaColumna = True
            If _ocabecera.ModeloExtraccionesDET.tipoTope.idTipoTope = 1 Then
                'si la cantidad de extracto por columna es igual a la cantidad de extracto se mostrar solo una columna
                If _ocabecera.ModeloExtraccionesDET.cantExtractosPorColumna = _ocabecera.ModeloExtraccionesDET.cantExtractos Then
                    CompletaColumna = False
                    Exit Function
                End If
            Else
                Exit Function
            End If
        Catch ex As Exception
            MsgBox("Error CompletaColumna" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Function ControlaValoresIngreso(ByVal _ocabecera As ExtraccionesCAB, ByVal _valor As Integer, ByRef msj As String) As Boolean
        Try
            ControlaValoresIngreso = True
            If _ocabecera.ModeloExtraccionesDET.tipoValorExtraido.IdTipoValorExtraido = 1 Then
                If _valor < _ocabecera.ModeloExtraccionesDET.valorMinimo Or _valor > _ocabecera.ModeloExtraccionesDET.valorMaximo Then
                    msj = "El valor ingresado debe estar comprendido entre " & _ocabecera.ModeloExtraccionesDET.valorMinimo & " y " & _ocabecera.ModeloExtraccionesDET.valorMaximo
                    ControlaValoresIngreso = False
                End If
            End If
        Catch ex As Exception
            MsgBox("Problema ControlaValoresIngreso:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub HabilitaBotonExtra()
        Dim tab As TabPage
        Dim _cabecera As New ExtraccionesCAB

        Try
            Me.btnExtra.Enabled = False
            Me.btnExtra.Visible = False
            For Each tab In TabExtracciones.TabPages
                _cabecera = tab.Tag
                If InStr(UCase(_cabecera.Titulo), "REVANCHA") <> 0 And _cabecera.Ejecutada = 1 Then
                    Me.btnExtra.Enabled = True
                    Me.btnExtra.Visible = True
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            MsgBox("Problema HabilitaBotonExtra:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
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
                    indice = 0

                Case 1280
                    indice = 0

            End Select
            Return indice
        Catch ex As Exception
            Return 1
            MsgBox("Error ObtenerIndiceResolucion:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    '**** digitador doble************
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
                    If InStr(ReceiveBuffer, "B=") > 0 Then 'se envio un comnado para sonido
                        nro = Mid(ReceiveBuffer, 4, 1)
                        VariableSonidoDevuelto = nro
                    End If
                End If
                If btmConfirmar.Enabled Then
                    DesHabilitarTeclado()
                    SerialPortClosing = True
                End If
            End If
        Catch ex As Exception
            '**06/11/2012, si es teclado doble muestra el msj sino no muestra nada
            If cboMetodoIngreso.SelectedValue = 3 Then
                MsgBox("Problema SerialPort_DataReceived:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
            End If
        End Try
    End Sub


    Private Sub BtnConectar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnConectar.Click

        Try
            If CboPuertos.Text = "" Then
                MsgBox("Seleccione un puerto.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
            ConectarDesconectarPuertoSerie()
        Catch ex As Exception
            MsgBox("Problema BtnConectar_Click:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub HabilitarTeclado()
        Dim m_msj As String
        Try
            SerialPortClosing = False
            With SerialPort
                If .IsOpen = True Then
                    m_msj = ""
                    m_msj = ">A" & vbCr
                    .Write(m_msj)
                    Thread.Sleep(600)
                End If
            End With
        Catch ex As Exception
            MsgBox("Problema HabilitarTeclado:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
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
                    'Thread.Sleep(600)
                End If
                'HabilitarTeclado()
            End With
        Catch ex As Exception
            MsgBox("Problema HabilitarSonido:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
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
                    Thread.Sleep(600)
                End If
            End With
        Catch ex As Exception
            MsgBox("Problemas DesHabilitarTeclado:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Private Sub CrearListaExtraccionesSorteadas(ByVal oExtraccionesCab As ListaOrdenada(Of ExtraccionesCAB), ByVal _ExtraccionCABActual As ExtraccionesCAB)
        Dim _extraccionesDET As ExtraccionesDET
        Dim cValorPosicion As New cValorPosicion
        Dim cVP As New cValorPosicion
        Dim _valorFormateado As Integer
        Dim _Encontrado As Boolean
        Dim _CantValores As Integer
        Dim _ExtraccionesCAB As ExtraccionesCAB
        Dim i As Integer

        Try
            ListaExtraccionesSorteadas = Nothing

            For Each _ExtraccionesCAB In oExtraccionesCab
                If _ExtraccionesCAB.orden = 1 Then
                    _CantValores = _ExtraccionesCAB.ModeloExtraccionesDET.cantExtractos
                    Exit For
                End If
            Next
            ListaExtraccionesSorteadas = New ListaOrdenada(Of cValorPosicion)

            _CantidadextraccionesRepetidas = _CantValores
            'crea la lista con -1
            For i = 1 To _CantValores
                cValorPosicion = New cValorPosicion
                cValorPosicion.Valor = -1
                cValorPosicion.Posicion = -1
                ListaExtraccionesSorteadas.Add(cValorPosicion)

            Next

            For Each _ExtraccionesCAB In oExtraccionesCab
                If _ExtraccionesCAB.orden < _ExtraccionCABActual.orden Then
                    For Each _extraccionesDET In _ExtraccionesCAB.ExtraccionesDET
                        _valorFormateado = _extraccionesDET.Valor.Substring(Len(_extraccionesDET.Valor) - _ExtraccionCABActual.ModeloExtraccionesDET.cantCifras, _ExtraccionCABActual.ModeloExtraccionesDET.cantCifras)
                        cValorPosicion = New cValorPosicion
                        For Each cVP In ListaExtraccionesSorteadas
                            If cVP.Valor = _valorFormateado Then
                                _Encontrado = True

                                Exit For
                            End If
                        Next
                        '** si no esta en la lista los agrego en el primer -1 que encuentre
                        If Not _Encontrado Then
                            i = 0
                            _CantidadextraccionesRepetidas = _CantidadextraccionesRepetidas - 1
                            For Each cVP In ListaExtraccionesSorteadas
                                If cVP.Valor = -1 Then
                                    ListaExtraccionesSorteadas(i).Valor = _valorFormateado
                                    ListaExtraccionesSorteadas(i).Posicion = _extraccionesDET.PosicionEnExtracto
                                    Exit For
                                End If
                                i = i + 1
                            Next
                        Else
                            _Encontrado = False
                        End If
                    Next
                End If
            Next
            'para que siempre cree la lista auxiliar
            CopiarListaOriginal()



        Catch ex As Exception
            MsgBox("Error CrearListaExtraccionesSorteadas:" & ex.Message, MDIContenedor.Text)
        End Try
    End Sub

    Public Property DefInstance() As ConcursoExtracciones
        Get
            If m_FormDefInstance Is Nothing OrElse _
                        m_FormDefInstance.IsDisposed Then
                m_InitializingDefInstance = True
                m_FormDefInstance = New ConcursoExtracciones
                m_InitializingDefInstance = False
            End If
            DefInstance = m_FormDefInstance
        End Get
        Set(ByVal Value As ConcursoExtracciones)
            m_FormDefInstance = Value
        End Set
    End Property
    'agrega el nro que se ingreso desde el digitador en el text correspondiente de acuerdo a la configuracion seteada
    ' Private Sub AgregarNroDesdeDigitador(ByVal pNumero As Integer)
    Private Sub AgregarNroDesdeDigitador()
        Try
            If Me.txtPosEnExtracto2.Visible Then
                If Me.txtValorExtraccion2.Text = "" Then
                    Me.txtValorExtraccion2.Text = ValorenPuertoSerie  'pNumero
                    Me.txtValor2_KeyPress(Nothing, New System.Windows.Forms.KeyPressEventArgs(Convert.ToChar(Keys.Return)))
                Else
                    If ValorenPuertoSerie.Length > 2 Then
                        MsgBox("INGRESO INVALIDO. Vuelva a ingresar los datos." & vbCrLf & "La posición solo acepta dos dígitos.", MsgBoxStyle.Critical, MDIContenedor.Text)
                        If CboPuertos.Visible Then
                            Me.txtValorExtraccion2.Text = ""
                            Me.txtPosEnExtracto2.Text = ""
                        End If
                        Exit Sub
                    End If
                    txtPosEnExtracto2.Text = ValorenPuertoSerie 'pNumero
                    Me.txtPos2_KeyPress(Nothing, New System.Windows.Forms.KeyPressEventArgs(Convert.ToChar(Keys.Return)))
                End If
            Else
                Me.txtValorExtraccion2.Text = ValorenPuertoSerie  'pNumero
                Me.txtPos2_KeyPress(Nothing, New System.Windows.Forms.KeyPressEventArgs(Convert.ToChar(Keys.Return)))
            End If
        Catch ex As Exception
            MsgBox("Problema AgregarNroDesdeDigitador:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
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
            End With
        Catch ex As Exception
            MsgBox("Problema ConfigurarPuertoSerie:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
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
                    MsgBox("Error al abrir el puerto:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End Try
            Else
                Try
                    SerialPortClosing = True
                    DesHabilitarTeclado()
                    .Close()
                Catch ex
                    MsgBox("Error al cerrar el puerto:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
                End Try
            End If
            If .IsOpen = True Then
                .ReceivedBytesThreshold = 1
                SerialPortClosing = False
                Me.LblTeclados.Visible = True
                LblTeclados.Text = "Teclados Habilitados"
                BtnConectar.Image = My.Resources.Imagenes.desconectar.ToBitmap
                ToolTip1.SetToolTip(BtnConectar, "Desconectar dispositivo")
            Else
                Me.LblTeclados.Visible = True
                LblTeclados.Text = "Teclados Deshabilitados"
                SerialPortClosing = True
                BtnConectar.Image = My.Resources.Imagenes.conectar.ToBitmap
                ToolTip1.SetToolTip(BtnConectar, "Conectar dispositivo")
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
    Private Sub habilitaBotonFinalizar()
        Dim oExtraccionesCab As ExtraccionesCAB
        Dim oTabPages As TabPage
        For Each oTabPages In TabExtracciones.TabPages
            oExtraccionesCab = oTabPages.Tag
            If oExtraccionesCab.Ejecutada = 0 Or oExtraccionesCab.Ejecutada = 2 Then
                btnFinalizar.Enabled = False
                Exit Sub
            End If
        Next
        btnFinalizar.Enabled = True
    End Sub

    Private Sub DesconectaPuerto()
        Try
            With SerialPort
                If .IsOpen Then
                    LblTeclados.Text = "Teclados Deshabilitados"
                    BtnConectar.Image = My.Resources.Imagenes.conectar.ToBitmap
                    ToolTip1.SetToolTip(BtnConectar, "Conectar dispositivo")
                    DesHabilitarTeclado()
                    SerialPortClosing = True
                    .Close()
                Else
                    .Open()
                    LblTeclados.Text = "Teclados Deshabilitados"
                    BtnConectar.Image = My.Resources.Imagenes.conectar.ToBitmap
                    ToolTip1.SetToolTip(BtnConectar, "Conectar dispositivo")
                    DesHabilitarTeclado()
                    SerialPortClosing = True
                    .Close()

                End If
            End With
        Catch ex As Exception
            'no hace nada
        End Try

    End Sub
    Public Function AnularSorteos(ByVal lst As String) As Boolean
        Dim oSorteo As New PgmSorteo
        Dim boSorteo As New PgmSorteoBO
        Dim sorteoLotBO As New PgmSorteoLoteriaBO
        Dim _resultado As Boolean = False
        Dim _Sorteos() As String
        Dim _i As Integer
        Try
            _Sorteos = lst.Split(",")
            For _i = 0 To UBound(_Sorteos)

                oSorteo = boSorteo.getPgmSorteo(_Sorteos(_i))
                If oSorteo Is Nothing Then
                    Exit Function
                End If
                ' Anulo otras jurisdicciones, si existen
                Dim oexlot As pgmSorteo_loteria
                For Each oexlot In oSorteo.ExtraccionesLoteria
                    sorteoLotBO.AnularWeb(oSorteo, oexlot.Loteria.IdLoteria, False, True)
                Next
                ' Por ultimo anulo SAnta FE
                sorteoLotBO.AnularWeb(oSorteo, General.Jurisdiccion, False, True)
                _resultado = True
                Return _resultado
            Next
        Catch ex As Exception
            Return False
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & Me.Text & " -> " & ex.Message)
        Finally

        End Try
    End Function
    Public Function CopiarListaOriginal() As Boolean
        Dim cValorPosicion As New cValorPosicion
        Dim cVP As New cValorPosicion
        Try
            ListaExtraccionesSorteadasModificable = Nothing
            ListaExtraccionesSorteadasModificable = New ListaOrdenada(Of cValorPosicion)
            If ListaExtraccionesSorteadas Is Nothing Then
                ListaExtraccionesSorteadasModificable = Nothing
                Exit Function
            End If

            For Each cValorPosicion In ListaExtraccionesSorteadas
                cVP = New cValorPosicion
                cVP.Posicion = cValorPosicion.Posicion
                cVP.Valor = cValorPosicion.Valor
                ListaExtraccionesSorteadasModificable.Add(cVP)
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MDIContenedor.Text)
        End Try
    End Function

    Private Sub CompletarListaExtraccionesRepetidas(ByVal oCabecera As ExtraccionesCAB, ByVal pmodifica As Boolean, ByVal _valor As String, Optional ByVal _valorIngresado As String = "", Optional ByRef oextraccionesDET As ExtraccionesDET = Nothing)
        Dim extraccionesDET As New ExtraccionesDET
        Dim nuevoDetalleDet As New ListaOrdenada(Of ExtraccionesDET)
        Dim _valordev As Integer
        Dim _orden As Integer
        Dim boExtracciones As New ExtraccionesBO
        Dim _extracciones As ListaOrdenada(Of ExtraccionesDET)
        Try
            If ListaExtraccionesSorteadas Is Nothing Then
                Exit Sub
            End If
            _valordev = 0
            '** vuelve a crear una lista con los huecos originales
            CopiarListaOriginal()
            For Each extraccionesDET In oCabecera.ExtraccionesDET
                CompletarListaExtraccionesSorteadas(extraccionesDET.Valor, , _valordev, , extraccionesDET)
                If pmodifica Then
                    'por cada nuevo valor agregado comprueba que no se haya completado la lista
                    If ListaExtraccionesSorteadasCompletaModificada() Then
                        '** bandera para redibujar los controles
                        ReiniciaControles = True
                        _orden = ObtenerOrden(oCabecera, _valordev)
                        '** se borra todo lo que se ingreso(utilizando el campo orden) despues del valor que completo las extracciones
                        ExtraccionesBO = New ExtraccionesBO
                        If Not ExtraccionesBO.BorrarExtraccionesDEt(oCabecera, _orden) Then
                            MsgBox("No se pudo borrar el detalle de extracciones", MDIContenedor.Text)
                        End If
                        Exit For
                    End If
                End If
            Next
            'Else
            'CompletarListaExtraccionesSorteadas(_valor, , _valordev, , oextraccionesDET)
            'End If


        Catch ex As Exception
            MsgBox("CompletarListaExtraccionesSorteadasDesdeLoad:" & ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub


    Private Sub CompletarListaExtraccionesSorteadas(ByVal pvalor As String, Optional ByVal valoringresado As String = "", Optional ByRef valor As Integer = 0, Optional ByRef oCabecera As ExtraccionesCAB = Nothing, Optional ByVal oextraccionesDET As ExtraccionesDET = Nothing)
        Dim cValorPosicion As New cValorPosicion
        Dim cVP As New cValorPosicion
        Try
            If ListaExtraccionesSorteadas Is Nothing Then
                Exit Sub
            End If

            For Each cValorPosicion In ListaExtraccionesSorteadasModificable
                'si ya existe en la lista salgo
                For Each cVP In ListaExtraccionesSorteadasModificable
                    If cVP.Valor = pvalor Then
                        If valoringresado.Trim <> "" Then
                            cVP.Valor = valoringresado
                        End If
                        valor = cVP.Valor
                        '** actualiza el estado para saber si tiene que pintar de rojo o no
                        If Not oextraccionesDET Is Nothing Then
                            oextraccionesDET.Repetido = 1
                        End If
                        Exit Sub
                    End If
                Next
                If cValorPosicion.Valor = -1 Then
                    cValorPosicion.Valor = pvalor
                    valor = pvalor
                    If Not oextraccionesDET Is Nothing Then
                        oextraccionesDET.Repetido = 0
                    End If
                    Exit For
                End If
            Next


        Catch ex As Exception
            MsgBox(ex.Message, MDIContenedor.Text)
        End Try
    End Sub
    Private Function ObtenerOrden(ByVal ocabecera As ExtraccionesCAB, ByVal _valor As Integer) As Integer
        Dim _extraccionesDET As ExtraccionesDET
        Dim orden As Integer = 0
        Try
            For Each _extraccionesDET In ocabecera.ExtraccionesDET
                If _extraccionesDET.Valor = _valor Then
                    orden = _extraccionesDET.Orden
                    Return orden
                    Exit Function
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    Private Function ExisteEnListaExtracciones(ByVal _valor As Integer) As Boolean
        Dim _valorLista As cValorPosicion
        Try
            If ListaExtraccionesSorteadas Is Nothing Then Exit Function
            ExisteEnListaExtracciones = False
            For Each _valorLista In ListaExtraccionesSorteadas
                If _valorLista.Valor = _valor Then
                    ExisteEnListaExtracciones = True
                    Exit Function
                End If
            Next
        Catch ex As Exception
            ExisteEnListaExtracciones = False
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function

    Private Sub CompletarValoresExtracciones(ByVal oCabecera As ExtraccionesCAB, ByVal pExtraccionesDET As ExtraccionesDET, ByVal pModifica As Boolean)

        Dim nombreTextoValor As String
        Dim nombreTextoPos As String
        Dim nombreTextoHora As String
        Dim nombreGrilla As String
        Dim nombreTab As String
        Dim i As Integer
        Dim NombreText As String
        Dim Cajatexto As TextBox
        Dim Formato As String
        Dim habilitaPosicion As Boolean
        Dim nombrelbl As String
        Dim etiqueta As Label
        Dim _Norepetidos As Integer
        Dim nombreL As String

        Dim _cabecera As ExtraccionesCAB

        Dim nombrecaja As String

        Try
            nombreGrilla = "grdExtraccionesModelo" & oCabecera.idModeloExtraccionesDET
            nombreTab = "TabExtraccionesModelo" & oCabecera.idModeloExtraccionesDET
            nombreTextoValor = "txtValorModeloid" & oCabecera.idModeloExtraccionesDET
            nombreTextoPos = "txtPosModeloid" & oCabecera.idModeloExtraccionesDET
            nombreTextoHora = "txtHoraModeloid" & oCabecera.idModeloExtraccionesDET
            nombrelbl = "lblorden" & oCabecera.idModeloExtraccionesDET
            If oCabecera.ModeloExtraccionesDET.sorteaPosicion Then
                habilitaPosicion = True
            Else
                habilitaPosicion = False
            End If
            i = 1

            Select Case oCabecera.ModeloExtraccionesDET.ordenEnExtracto.idOrdenEnExtracto
                Case 1 'orden extraccion
                    oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenExtraccion
                Case 2 'porden posicion extracto
                    oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenPosicion
                Case 3 'orden numerico
                    oCabecera.Ordenamiento = ExtraccionesDET.TipoOrdenamiento.OrdenValor
            End Select

            i = 1
            _Norepetidos = 0
            Formato = ""
            Formato = CrearFormatoCifras(oCabecera.ModeloExtraccionesDET.cantCifras)
            For Each oextraccionesDET In oCabecera.ExtraccionesDET
                If pModifica = True Then 'si es una modificacion pinto todos
                    NombreText = nombreTextoValor & "-" & i
                    etiqueta = New Label
                    etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                    etiqueta.Text = IIf(oextraccionesDET.Valor < 0, "", Format(oextraccionesDET.Valor))
                    etiqueta.TextAlign = ContentAlignment.MiddleCenter
                    etiqueta.Visible = True
                    etiqueta.Font = LetraNegrita
                    '** pinta de rojo los repetidos
                    If oextraccionesDET.Repetido = 1 Then
                        etiqueta.Image = My.Resources.Imagenes.campovaloresValorRepetido
                    Else
                        etiqueta.Image = My.Resources.Imagenes.campovaloresValor
                        _Norepetidos = _Norepetidos + 1
                    End If

                    NombreText = nombreTextoPos & "-" & i
                    etiqueta = New Label
                    etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                    etiqueta.Text = IIf(oextraccionesDET.Valor < 0, "", oextraccionesDET.PosicionEnExtracto)
                    etiqueta.Visible = habilitaPosicion
                    etiqueta.TextAlign = ContentAlignment.MiddleCenter
                    etiqueta.Font = LetraNegrita
                    If oextraccionesDET.Repetido = 1 Then
                        etiqueta.Image = My.Resources.Imagenes.campovaloresRepetido
                    Else
                        etiqueta.Image = My.Resources.Imagenes.campovalores
                    End If

                    NombreText = nombreTextoHora & "-" & i
                    etiqueta = New Label
                    etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                    etiqueta.Text = IIf(oextraccionesDET.Valor < 0, "", oextraccionesDET.FechaHora.ToString("HH:mm:ss"))
                    etiqueta.Visible = True
                    etiqueta.TextAlign = ContentAlignment.MiddleCenter
                    etiqueta.Font = LetraNegrita
                    If oextraccionesDET.Repetido = 1 Then
                        etiqueta.Image = My.Resources.Imagenes.campovaloresHoraRepetido
                    Else
                        etiqueta.Image = My.Resources.Imagenes.campovaloresHora
                    End If
                Else
                    If oextraccionesDET.Repetido = 0 Then
                        '** va calculando los valores que se van completando la cantidad de valores no repetidos
                        _Norepetidos = _Norepetidos + 1
                    End If
                    'es un ingreso, solo pinto el registro correspondiente
                    If oextraccionesDET.idExtraccionesDET = pExtraccionesDET.idExtraccionesDET Then

                        nombreL = nombrelbl & "-" & i
                        etiqueta = New Label
                        etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(nombreL)
                        etiqueta.Text = i
                        etiqueta.Visible = True

                        NombreText = nombreTextoValor & "-" & i
                        etiqueta = New Label
                        etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                        etiqueta.Text = IIf(oextraccionesDET.Valor < 0, "", oextraccionesDET.Valor)
                        etiqueta.TextAlign = ContentAlignment.MiddleCenter
                        etiqueta.Font = LetraNegrita
                        '** pinta de rojo los repetidos
                        If oextraccionesDET.Repetido = 1 Then
                            etiqueta.Image = My.Resources.Imagenes.campovaloresValorRepetido
                        Else
                            etiqueta.Image = My.Resources.Imagenes.campovaloresValor
                        End If
                        If etiqueta.Visible = False Then etiqueta.Visible = True

                        NombreText = nombreTextoPos & "-" & i
                        'Cajatexto = New TextBox
                        etiqueta = New Label

                        etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                        etiqueta.Text = IIf(oextraccionesDET.PosicionEnExtracto < 0, "", oextraccionesDET.PosicionEnExtracto)
                        etiqueta.TextAlign = ContentAlignment.MiddleCenter
                        etiqueta.Visible = habilitaPosicion
                        etiqueta.Font = LetraNegrita
                        If oextraccionesDET.Repetido = 1 Then
                            etiqueta.Image = My.Resources.Imagenes.campovaloresRepetido
                        Else
                            etiqueta.Image = My.Resources.Imagenes.campovalores
                        End If

                        NombreText = nombreTextoHora & "-" & i
                        etiqueta = New Label
                        etiqueta = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(NombreText)
                        etiqueta.Text = IIf(oextraccionesDET.FechaHora.ToShortTimeString = "", "", oextraccionesDET.FechaHora.ToString("HH:mm:ss"))
                        etiqueta.TextAlign = ContentAlignment.MiddleCenter
                        etiqueta.Font = LetraNegrita
                        etiqueta.Visible = True
                        If oextraccionesDET.Repetido = 1 Then
                            etiqueta.Image = My.Resources.Imagenes.campovaloresHoraRepetido
                        Else
                            etiqueta.Image = My.Resources.Imagenes.campovaloresHora
                        End If
                        Exit For
                    End If
                End If
                i = i + 1
            Next
            '** solo para extracciones opcionales,actualiza el label de los que fueron comletados
            If oCabecera.ModeloExtraccionesDET.Obligatoria = False Then
                nombreGrilla = "grdExtraccionesModelo" & oCabecera.idModeloExtraccionesDET
                nombreTab = "TabExtraccionesModelo" & oCabecera.idModeloExtraccionesDET
                nombrecaja = "txtextraccionesRepetidasde" & oCabecera.idModeloExtraccionesDET
                Cajatexto = New TextBox
                Cajatexto = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(nombrecaja)
                Cajatexto.Text = _Norepetidos

            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Sub
    Public Function ActualizaExtraccionesRepetidas(ByVal opc As PgmConcurso)
        Dim _cabecera As ExtraccionesCAB
        Dim Cajatexto As TextBox
        Dim nombrecaja, nombreGrilla, nombreTab As String
        'actualiza el lbl de total de valores repetidos a completar
        Try
            For Each _cabecera In opc.Extracciones
                If _cabecera.ModeloExtraccionesDET.Obligatoria = False Then
                    nombreGrilla = "grdExtraccionesModelo" & _cabecera.idModeloExtraccionesDET
                    nombreTab = "TabExtraccionesModelo" & _cabecera.idModeloExtraccionesDET
                    nombrecaja = "txtextraccionesRepetidasHasta" & _cabecera.idModeloExtraccionesDET
                    Cajatexto = New TextBox
                    Cajatexto = Me.TabExtracciones.Controls(nombreTab).Controls(nombreGrilla).Controls(nombrecaja)
                    Cajatexto.Text = _CantidadextraccionesRepetidas
                    '** actualiza el campo de repetido en el detalle para saber si tiene que pintar o no
                    ActualizaDetalleDesdeLoad(_cabecera)
                    '**se llama a redibujar todos los text para que se pinten o no de acuerdo al campo repetido del detalle
                    CompletarValoresExtracciones(_cabecera, _cabecera.ExtraccionesDET(0), True)
                End If
            Next

        Catch ex As Exception

        End Try
    End Function
    Public Function ExisteEnListaExtraccionesRepetidas(ByVal _pvalor As String) As Boolean
        Dim i As String
        Try
            ExisteEnListaExtraccionesRepetidas = False
            If _extraccionesRepetidas Is Nothing Then Exit Function
            For Each i In _extraccionesRepetidas
                If i = _pvalor Then
                    ExisteEnListaExtraccionesRepetidas = True
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    'Public Sub ActualizaExtraccionesDet(ByVal pvalor As String, Optional ByVal oCabecera As ExtraccionesCAB = Nothing, Optional ByRef oExtraccionesDET As ExtraccionesDET = Nothing)

    '    If Not oExtraccionesDET Is Nothing Then
    '        oExtraccionesDET.Repetido = 1
    '        Exit Sub
    '    End If
    '    For Each _extraccionesDET In oCabecera.ExtraccionesDET
    '        If _extraccionesDET.Valor = pvalor Then
    '            _extraccionesDET.Repetido = 1
    '            Exit Sub
    '        End If
    '    Next
    'End Sub

    Public Function ControlaFechas(ByVal oCabecera As ExtraccionesCAB, ByRef msj As String) As Boolean
        Dim BoExtracciones As New ExtraccionesBO
        Dim fechaPrimerBolilla As DateTime
        Dim fechaUltimaBolilla As DateTime
        Dim UltimaBolilla As Integer
        Dim fecDP As Date
        Dim fec As Date


        Try
            UltimaBolilla = oCabecera.ModeloExtraccionesDET.cantExtractos
            If BoExtracciones.ObtenerFechasBolillas(oCabecera.idExtraccionesCAB, UltimaBolilla, fechaPrimerBolilla, fechaUltimaBolilla) Then

                fecDP = ConvertirFecha(DTPHoraInicioextraccion.Value)
                fec = ConvertirFecha(fechaPrimerBolilla)
                If fecDP > fec Then
                    msj = "La hora de inicio debe ser menor o igual a la hora de la primera extracción."
                    ControlaFechas = False
                    Exit Function
                End If


                fecDP = ConvertirFecha(DTPHoraFinextraccion.Value)
                fec = ConvertirFecha(fechaUltimaBolilla)

                If fecDP < fec Then
                    msj = "La hora de fin debe ser mayor o igual a la hora de la última extracción."
                    ControlaFechas = False
                    Exit Function

                End If
            End If
            ControlaFechas = True
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    Public Function ActualizaDetalleDesdeLoad(ByVal oCabecera As ExtraccionesCAB)
        Dim _extraccionesDET As ExtraccionesDET
        Try
            For Each _extraccionesDET In oCabecera.ExtraccionesDET
                '** busca en la lista con los repetidos para actualizar el campo para pintar o no
                If ExisteEnListaExtracciones(_extraccionesDET.Valor) Then
                    _extraccionesDET.Repetido = 1
                End If
            Next
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try
    End Function
    Public Function ConvertirFecha(ByVal Fecha As Date) As Date
        Dim fechaNueva As Date
        Try
            fechaNueva = New Date(Fecha.Year, Fecha.Month, Fecha.Day, Fecha.Hour, Fecha.Minute, Fecha.Second)
            Return fechaNueva
        Catch ex As Exception
            Return Nothing
            MsgBox(ex.Message, MsgBoxStyle.Information, MDIContenedor.Text)
        End Try

    End Function

    Public Sub boton_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs)
        Dim snder = CType(sender, Button)
        If snder.Enabled = True Then
            snder.BackgroundImageLayout = ImageLayout.Stretch
            snder.BackgroundImage = My.Resources.Imagenes.botonseleccionado
        Else
            snder.BackgroundImageLayout = ImageLayout.Stretch
            snder.BackgroundImage = My.Resources.Imagenes.boton_off
        End If

    End Sub

    Private Sub btnCancelar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.EnabledChanged
        If btnCancelar.Enabled = True Then
            btnCancelar.BackgroundImageLayout = ImageLayout.Stretch
            btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnCancelar.BackgroundImageLayout = ImageLayout.Stretch
            btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btmConfirmar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmConfirmar.EnabledChanged
        If btmConfirmar.Enabled = True Then
            btmConfirmar.BackgroundImageLayout = ImageLayout.Stretch
            btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btmConfirmar.BackgroundImageLayout = ImageLayout.Stretch
            btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnPorArchivo_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPorArchivo.EnabledChanged
        If btnPorArchivo.Enabled = True Then
            btnPorArchivo.BackgroundImageLayout = ImageLayout.Stretch
            btnPorArchivo.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnPorArchivo.BackgroundImageLayout = ImageLayout.Stretch
            btnPorArchivo.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btmModificar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmModificar.EnabledChanged
        If btmModificar.Enabled = True Then
            btmModificar.BackgroundImageLayout = ImageLayout.Stretch
            btmModificar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btmModificar.BackgroundImageLayout = ImageLayout.Stretch
            btmModificar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnExtraccionAnterior_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtraccionAnterior.EnabledChanged
        If btnExtraccionAnterior.Enabled = True Then
            btnExtraccionAnterior.BackgroundImageLayout = ImageLayout.Stretch
            btnExtraccionAnterior.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnExtraccionAnterior.BackgroundImageLayout = ImageLayout.Stretch
            btnExtraccionAnterior.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnextraccionSiguiente_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnextraccionSiguiente.EnabledChanged
        If btnextraccionSiguiente.Enabled = True Then
            btnextraccionSiguiente.BackgroundImageLayout = ImageLayout.Stretch
            btnextraccionSiguiente.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnextraccionSiguiente.BackgroundImageLayout = ImageLayout.Stretch
            btnextraccionSiguiente.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnFinalizar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizar.EnabledChanged
        If btnFinalizar.Enabled = True Then
            btnFinalizar.BackgroundImageLayout = ImageLayout.Stretch
            btnFinalizar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnFinalizar.BackgroundImageLayout = ImageLayout.Stretch
            btnFinalizar.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnRevertir_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertir.EnabledChanged
        If btnRevertir.Enabled = True Then
            btnRevertir.BackgroundImageLayout = ImageLayout.Stretch
            btnRevertir.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnRevertir.BackgroundImageLayout = ImageLayout.Stretch
            btnRevertir.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnRevertirExtraccion_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertirExtraccion.EnabledChanged
        If btnRevertirExtraccion.Enabled = True Then
            btnRevertirExtraccion.BackgroundImageLayout = ImageLayout.Stretch
            btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnRevertirExtraccion.BackgroundImageLayout = ImageLayout.Stretch
            btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub BTNSALIR_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTNSALIR.EnabledChanged
        If BTNSALIR.Enabled = True Then
            BTNSALIR.BackgroundImageLayout = ImageLayout.Stretch
            BTNSALIR.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            BTNSALIR.BackgroundImageLayout = ImageLayout.Stretch
            BTNSALIR.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnExtraccionAnterior_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnExtraccionAnterior.MouseDown
        btnExtraccionAnterior.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnExtraccionAnterior_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtraccionAnterior.MouseHover
        btnExtraccionAnterior.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnExtraccionAnterior_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtraccionAnterior.MouseLeave
        If btnExtraccionAnterior.Enabled Then
            btnExtraccionAnterior.BackgroundImage = My.Resources.Imagenes.boton_normal
        End If
    End Sub

    Private Sub btnextraccionSiguiente_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnextraccionSiguiente.MouseDown
        btnextraccionSiguiente.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnextraccionSiguiente_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnextraccionSiguiente.MouseHover
        btnextraccionSiguiente.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnextraccionSiguiente_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnextraccionSiguiente.MouseLeave
        If btnextraccionSiguiente.Enabled Then
            btnextraccionSiguiente.BackgroundImage = My.Resources.Imagenes.boton_normal
        End If
    End Sub

    Private Sub btnRevertir_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnRevertir.MouseDown
        btnRevertir.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnRevertir_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertir.MouseHover
        btnRevertir.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnRevertir_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertir.MouseLeave
        btnRevertir.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnPorArchivo_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnPorArchivo.MouseDown
        btnPorArchivo.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnPorArchivo_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPorArchivo.MouseHover
        btnPorArchivo.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnPorArchivo_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnPorArchivo.MouseLeave
        btnPorArchivo.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btmConfirmar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btmConfirmar.MouseDown
        btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btmConfirmar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmConfirmar.MouseHover
        btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btmConfirmar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmConfirmar.MouseLeave
        btmConfirmar.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub BTNSALIR_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BTNSALIR.MouseDown
        BTNSALIR.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub BTNSALIR_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTNSALIR.MouseHover
        BTNSALIR.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub BTNSALIR_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BTNSALIR.MouseLeave
        BTNSALIR.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnRevertirExtraccion_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnRevertirExtraccion.MouseDown
        btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnRevertirExtraccion_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertirExtraccion.MouseHover
        btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnRevertirExtraccion_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnRevertirExtraccion.MouseLeave
        btnRevertirExtraccion.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnListarParametros_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarParametros.EnabledChanged
        If btnListarParametros.Enabled Then
            btnListarParametros.BackgroundImageLayout = ImageLayout.Stretch
            btnListarParametros.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnListarParametros.BackgroundImageLayout = ImageLayout.Stretch
            btnListarParametros.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnListarParametros_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnListarParametros.MouseDown
        btnListarParametros.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnListarParametros_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarParametros.MouseHover
        btnListarParametros.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnListarParametros_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnListarParametros.MouseLeave
        btnListarParametros.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnFinalizar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnFinalizar.MouseDown
        btnFinalizar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnFinalizar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizar.MouseHover
        btnFinalizar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnFinalizar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnFinalizar.MouseLeave
        btnFinalizar.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnExtra_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtra.EnabledChanged
        If btnExtra.Enabled Then
            btnExtra.BackgroundImageLayout = ImageLayout.Stretch
            btnExtra.BackgroundImage = My.Resources.Imagenes.boton_press

        Else
            btnExtra.BackgroundImageLayout = ImageLayout.Stretch
            btnExtra.BackgroundImage = My.Resources.Imagenes.boton_press
        End If
    End Sub

    Private Sub btnExtra_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnExtra.MouseDown
        btnExtra.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnExtra_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtra.MouseHover
        btnExtra.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnExtra_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnExtra.MouseLeave
        btnExtra.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btnCancelar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCancelar.MouseDown
        btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnCancelar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.MouseHover
        btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnCancelar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCancelar.MouseLeave
        btnCancelar.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub

    Private Sub btmModificar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btmModificar.MouseDown
        btmModificar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btmModificar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmModificar.MouseHover
        btmModificar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btmModificar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btmModificar.MouseLeave
        btmModificar.BackgroundImage = My.Resources.Imagenes.boton_normal
    End Sub
    Private Sub ListarParametros()
        Dim PgmBO As New PgmConcursoBO
        Dim extBO As ExtractoQuini6

        Dim dt As DataTable
        Dim ds As New DataSet
        Dim dtExtra As DataTable = Nothing
        Dim er As New ExtractoReporte
        Dim visualizar As String = "000000000000000000"

        Try
            dt = PgmBO.ObtenerDatosListado(OPgmConcurso.idPgmConcurso)
            ds.Tables.Add(dt)
            For Each opgmsorteo In OPgmConcurso.PgmSorteos
                '*** 26/09/2013 HG se calcula la progresion solo para santafe
                If opgmsorteo.idPgmSorteo = OPgmConcurso.idPgmSorteoPrincipal Then
                    dtExtra = ExtractoData.Extracto.GetExtractoDT(General.Jurisdiccion, opgmsorteo.idJuego, opgmsorteo.idPgmSorteo)
                    If opgmsorteo.idJuego = 4 Then
                        visualizar = er.getExtra(dtExtra)
                    End If
                    Exit For
                End If
            Next
            ds.Tables.Add(dtExtra)
            'ds.WriteXmlSchema("D:\Listado1.xml")
            Dim path_reporte As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes  '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES"
            Dim reporte As New Listado
            reporte.GenerarListado(ds, path_reporte, True, visualizar)
        Catch ex As Exception

        End Try
    End Sub
    '**06/11/2012
    Private Sub ActualizaProgresionLoteria(ByVal opgmsorteo As PgmSorteo, Optional ByVal publicaDisplay As Boolean = True, Optional ByRef vprogresion As Integer = 0)
        Dim PgmsorteoBO As New PgmSorteoBO
        Dim pgmconcursoBo As New PgmConcursoBO
        Dim progresion As Integer
        Try
            'calcula progresion
            progresion = PgmsorteoBO.getProgresionLoteria(opgmsorteo.idJuego, opgmsorteo.nroSorteo)
            'HG 28/12 si laprogeresion es cero,vuelvo a calcular,si sigue dando cero,muestro mensaje
            If progresion = 0 Then
                progresion = PgmsorteoBO.getProgresionLoteria(opgmsorteo.idJuego, opgmsorteo.nroSorteo)
                If progresion = 0 Then
                    vprogresion = progresion
                    Exit Sub
                End If
            End If
            'actualiza el campo progresion en la tabla extracto_qnl
            PgmsorteoBO.ActualizaProgresionLoteria(progresion, opgmsorteo.idPgmSorteo)
            '**** controles
            Dim _continuar As Boolean = False
            Dim _PublicaDisplay As String = General.PublicaDisplay.ToUpper
            If (_PublicaDisplay = "S") Then
                _continuar = True
            End If
            If Not _continuar Then
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " La publicación a Display no está habilitada.Parametros: PublicaDisplay:" & _PublicaDisplay & ".")
                Exit Sub
            End If
            '***************
            If publicaDisplay Then
                pgmconcursoBo.publicarDisplay(opgmsorteo.idPgmConcurso)
            End If
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try



    End Sub

    Private Sub btnPorArchivo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPorArchivo.Click
        ' *** ACA LOGICA DE LECTURA DEL ARCHIVO SIMILAR A PREMIOS, Y ACTUALIZACION DE LA CARGA
        ' ***     POR ULTIMO ACTUALIZACION DEL PANEL
        'MsgBox("Por Archivo")
        '' ESto es una prueba para la navegabilidad de la UI
        ''Dim misNros As String = "01|05|12|14|25|26"
        ''Dim misNrosA() As String = misNros.Split("|")
        ''Dim miSTR As String = Keys.Return
        ''For i = 0 To misNrosA.Count - 1
        ''    MsgBox(misNrosA(i))
        ''    'txtPosEnExtracto2.Text = i + 1
        ''    txtordenExtracto.Text = i + 1
        ''    'txtordenExtracto_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
        ''    txtValorExtraccion2.Text = misNrosA(i) '& Keys.Return
        ''    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
        ''    'SendKeys.Send(Keys.Return)
        ''Next

        Dim oCabecera As New ExtraccionesCAB
        Dim titulo As String = ""

        oCabecera = TabExtracciones.SelectedTab.Tag
        Dim existen As Boolean = False
        If oCabecera.ExtraccionesDET.Count > 0 Then
            For Each d In oCabecera.ExtraccionesDET
                If d.Valor <> "-1" Then
                    existen = True
                    Exit For
                End If
            Next
            If existen Then
                MsgBox("Existen extracciones ingresadas. Primero Revierta con opción 'Completa' y vuelva a intentar.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
            End If
        End If
        titulo = oCabecera.Titulo

        'MsgBox(titulo)
        ' Aca empieza lo real
        Dim opcBO As New PgmConcursoBO
        Dim arBO As New ArchivoBoldtBO
        Dim extractoBoldt As New cExtractoArchivoBoldt
        Dim ArchivoOrigenExtracto As String = ""
        Dim idpgmsorteo As String = ""
        Dim _valor As Boolean = True
        Dim idJuego As Integer
        Dim nroSorteoActual As Long

        Try
            FileSystemHelper.Log("Concursoextracciones: inicio Obtener extracciones click")
            If opcBO.getJuegoSorteoRector(OPgmConcurso, idJuego, nroSorteoActual) Then
                'el archivo deberia haberse creado enel load pero si no se creo tratade volver a crearlo
                If General.Obtener_pgmsorteos_ws = "S" Then
                    Dim path_archivo As String = ""
                    path_archivo = General.CarpetaOrigenArchivosExtractoBoldt
                    If Not path_archivo.EndsWith("\") Then
                        path_archivo = path_archivo & "\"
                    End If
                    path_archivo = path_archivo & arBO.CreaNombreArchivoExtracto(idJuego, nroSorteoActual) & ".zip"
                    ' si no existe el archivo,trat de crearlo desde el ws
                    If Not File.Exists(path_archivo) Then
                        idpgmsorteo = idJuego * 1000000 + nroSorteoActual
                        FileSystemHelper.Log(" Concursoextracciones:generar Archivo de extractos por WS:" & idpgmsorteo)
                        arBO.Generar_archivosExtracto_y_Control_por_WS(idpgmsorteo)
                        FileSystemHelper.Log(" Concursoextracciones:eneraracion Archivo de extractos:" & idpgmsorteo & " por WS OK")
                    End If
                End If
                FileSystemHelper.Log(" Concursoextracciones:generar de extractos a partir de archivos para:" & idpgmsorteo)
                extractoBoldt = arBO.GenerarExtractodesdeArchivo(idJuego, nroSorteoActual, ArchivoOrigenExtracto)
                FileSystemHelper.Log("Concursoextracciones: generacion de extractos a partir de archivos para:" & idpgmsorteo & " OK")
                If Not extractoBoldt Is Nothing Then
                    setNrosSegunJuego(idJuego, extractoBoldt, titulo, sender)
                Else
                    MsgBox("No se encontraron datos.Intente mas tarde.")
                End If
            End If
        Catch ex As Exception
            FileSystemHelper.Log(" Concursoextracciones:Problemas Obtener extracciones click, pgmsorteo:" & idpgmsorteo & " " & ex.Message)
            MsgBox("El Sistema detectó un problema con el archivo de Extracciones. Se interrumpe la carga. Solicite un nuevo archivo o registre manualmente.", MsgBoxStyle.Information, MDIContenedor.Text)
            Exit Sub
        End Try
    End Sub

    Private Sub setNrosSegunJuego(ByVal idJuego As Integer, ByRef extractoBoldt As cExtractoArchivoBoldt, ByVal titulo As String, ByVal sender As System.Object)
        Dim v() As String
        v = titulo.Split("-")
        Select Case idJuego
            Case 4
                If v(2).Trim.ToUpper = "TRADICIONAL" Then
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_1.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_2.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_3.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_4.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_5.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_6.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                ElseIf v(2).Trim.ToUpper = "LA SEGUNDA" Then
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_7.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_8.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_9.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_10.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_11.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_12.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                ElseIf v(2).Trim.ToUpper = "REVANCHA" Then
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_13.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_14.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_15.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_16.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_17.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_18.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                ElseIf v(2).Trim.ToUpper = "SIEMPRE SALE" Then
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_19.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_20.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_21.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_22.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_23.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_24.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                ElseIf v(2).Trim.ToUpper = "SORTEO ADICIONAL 1" Then
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_25.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_26.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_27.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_28.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_29.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_30.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                ElseIf v(2).Trim.ToUpper = "SORTEO ADICIONAL 2" Then
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_31.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_32.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_33.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_34.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_35.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_36.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                Else
                    MsgBox("Modalidad de Sorteo no encontrada en el archivo. Solicite reenvío o realice carga manual.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End If
            Case 13
                If v(2).Trim.ToUpper = "" Then
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_1.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_2.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_3.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_4.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_5.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_6.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                ElseIf v(2).Trim.ToUpper <> "" Then ' SE USA EL DISITNTO A VACIO PARA NO HARCODEAR E NOMBRE
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_25.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_26.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_27.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_28.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_29.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_30.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                ElseIf v(2).Trim.ToUpper = "SORTEO ADICIONAL 2" Then
                    txtordenExtracto.Text = "1"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_31.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "2"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_32.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "3"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_33.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "4"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_34.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "5"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_35.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                    txtordenExtracto.Text = "6"
                    txtValorExtraccion2.Text = extractoBoldt.Numero_36.ValorSTR
                    txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                Else
                    MsgBox("Modalidad de Sorteo no encontrada en el archivo. Solicite reenvío o realice carga manual.", MsgBoxStyle.Information, MDIContenedor.Text)
                    Exit Sub
                End If

            Case 2, 3, 8, 49, 30, 50
                txtordenExtracto.Text = "1"
                txtValorExtraccion2.Text = extractoBoldt.Numero_1.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "2"
                txtValorExtraccion2.Text = extractoBoldt.Numero_2.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "3"
                txtValorExtraccion2.Text = extractoBoldt.Numero_3.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "4"
                txtValorExtraccion2.Text = extractoBoldt.Numero_4.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "5"
                txtValorExtraccion2.Text = extractoBoldt.Numero_5.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "6"
                txtValorExtraccion2.Text = extractoBoldt.Numero_6.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "7"
                txtValorExtraccion2.Text = extractoBoldt.Numero_7.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "8"
                txtValorExtraccion2.Text = extractoBoldt.Numero_8.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "9"
                txtValorExtraccion2.Text = extractoBoldt.Numero_9.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "10"
                txtValorExtraccion2.Text = extractoBoldt.Numero_10.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "11"
                txtValorExtraccion2.Text = extractoBoldt.Numero_11.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "12"
                txtValorExtraccion2.Text = extractoBoldt.Numero_12.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "13"
                txtValorExtraccion2.Text = extractoBoldt.Numero_13.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "14"
                txtValorExtraccion2.Text = extractoBoldt.Numero_14.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "15"
                txtValorExtraccion2.Text = extractoBoldt.Numero_15.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "16"
                txtValorExtraccion2.Text = extractoBoldt.Numero_16.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "17"
                txtValorExtraccion2.Text = extractoBoldt.Numero_17.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "18"
                txtValorExtraccion2.Text = extractoBoldt.Numero_18.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "19"
                txtValorExtraccion2.Text = extractoBoldt.Numero_19.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
                txtordenExtracto.Text = "20"
                txtValorExtraccion2.Text = extractoBoldt.Numero_20.ValorSTR
                txtValor2_KeyPress(sender, (New System.Windows.Forms.KeyPressEventArgs(vbCrLf)))
            Case Else
                MsgBox("Modalidad no implementada para este juego.", MsgBoxStyle.Information, MDIContenedor.Text)
                Exit Sub
        End Select
    End Sub

End Class
