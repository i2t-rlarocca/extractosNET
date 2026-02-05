Imports libEntities.Entities
Imports Sorteos.Bussiness
Imports Sorteos.Helpers
Imports System.IO


Public Class FrmIniPublicador
    Private vPrimerLoad As Boolean
    Private salida As Boolean = False
    Dim opublicar As cPublicador
    Dim Archivo_Premio_encontrado As Boolean = False
    Dim gralDal As New Sorteos.Data.GeneralDAL
    '**** variables de archivo de premios encontrados por juego
    Dim premioPoceada As Boolean = False
    Dim premioQuini As Boolean = False
    Dim premioLoteria As Boolean = False
    Dim premioBrinco As Boolean = False
    Dim premioLoteriaChica As Boolean = False
    'reutilizo esta clase para guardar ,idjuego,marca de archivo encontrado
    Dim _oPgmConcurso As New libEntities.entities.PgmConcurso
    Dim listaPremiosEncotrado As ListaOrdenada(Of cValorPosicion)
    Dim AvisarMasTarde As Boolean = False
    Dim listaExtractoEncontrado As ListaOrdenada(Of cValorPosicion)
    '**** variables de archivo de extractos encontrados por juego
    Dim extractoBrinco As Boolean = False
    Dim extractoQuini As Boolean = False
    Dim extractoPoceada As Boolean = False
    Dim listaSueldosEncontrado As ListaOrdenada(Of cValorPosicion)
    Dim sueldobrinco As Boolean = False

    Private Sub FrmIniPublicador_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Try
            If salida Then Exit Sub
            If vPrimerLoad Then
                listaExtractoEncontrado = Nothing
                Publicacion()
            End If
        Catch ex As Exception
            MsgBox("Problemas en publicación:" & ex.Message)
        End Try
    End Sub

    Private Sub FrmIniPublicador_Disposed(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Disposed
        If NotifyIcon1 IsNot Nothing Then
            NotifyIcon1.Visible = False
            NotifyIcon1.Dispose()
            NotifyIcon1 = Nothing
        End If
    End Sub

    Private Sub FrmIniPublicador_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        If NotifyIcon1 IsNot Nothing Then
            NotifyIcon1.Visible = False
            NotifyIcon1.Dispose()
        End If
    End Sub

    Private Sub FrmIniPublicador_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        vPrimerLoad = True
        salida = False
        Dim ProcesoUnico As Boolean = True
        Dim contador As Integer = 0
        If ProcesoUnico Then
            Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("Publicador")
            For Each p As Process In pProcess

                contador = contador + 1
            Next
            If contador > 1 Then
                FileSystemHelper.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss") & " El proceso ya se encuentra en ejecución.Revise el ícono en la barra de tareas")
                salida = True
                Me.Close()
                Me.Dispose()
                Exit Sub
            End If
        End If

        ' '' Mutex local para sólo permitir una instancia de la aplicación por usuario
        ''Dim _mutex As System.Threading.Mutex

        ' '' Obtengo el nombre del ensamblado donde se encuentra ésta función
        ''Dim NombreAssembly As String = System.Reflection.Assembly.GetExecutingAssembly().GetName().Name

        ' '' Nombre del mutex según Tipo (visibilidad)
        ''Dim mutexName As String = NombreAssembly

        ''Dim newMutexCreated As Boolean
        ''Try
        ''    ' Abro/Creo mutex con nombre único
        ''    _mutex = New System.Threading.Mutex(False, mutexName, newMutexCreated)

        ''    If Not newMutexCreated Then
        ''        ' El mutex ya existía, Libero el mutex 
        ''        _mutex.Close()
        ''        MessageBox.Show("La aplicación ya se encuentra en ejecución. Encuentre el ícono de la aplicación en la barra de tareas.", "SoteosCAS - Publicador", MessageBoxButtons.OK, MessageBoxIcon.Information)
        ''        Me.Close()
        ''        Exit Sub
        ''    End If

        ''    NotifyIcon1.Visible = True
        ''    'NotifyIcon1.Icon = Icon.FromHandle(My.Resources.printer_add.GetHicon)
        ''    NotifyIcon1.Text = "SorteosCAS - Publicador"

        ''Catch ex As Exception

        ''End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Dim _idpgmconcurso As Long
        Dim novedadDisplay As Boolean
        Dim novedadWeb As Boolean
        Dim _terminar As Boolean
        Dim pgmConcursoBo As New PgmConcursoBO
        Dim procesoBo As New ProccessBO
        Dim msj As String = ""
        'Al cumplirse el tiempo del Timer:
        'Parar el Timer.
        Timer1.Stop()
        '** Leer par_ novedadesDisplay, idpgmconcurso.
        opublicar.LeeParametrosDisplay(_idpgmconcurso, novedadDisplay)
        '** Establecer  par_ novedadesDisplay en FALSE.
        opublicar.NovedadesDisplayOFF()

        '** Si  par_ novedadesDisplay (leido) = TRUE:
        If novedadDisplay Then
            'cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & "Hay novedades Display para concurso:" & _idpgmconcurso)
            'Establecer par_ publicando en TRUE.
            opublicar.Par_PublicandoON()
            Try
                'Publicar a displayCAS.
                cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & " Publicar DisplayCAS concurso:" & _idpgmconcurso)
                pgmConcursoBo.publicarDisplay(_idpgmconcurso)
            Catch ex As Exception
                'Si hubo errores en la publicación → Establecer par_novedadesDisplay en TRUE
                cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & " Hubo errores al publicar display: " & ex.Message)
                opublicar.NovedadesDisplayON()
            End Try
            'Establecer par_ publicando en FALSE.
            opublicar.Par_PublicandoOFF()
        End If

        'Leer par_ novedadesWeb, idpgmconcurso.
        opublicar.LeeParametrosWeb(_idpgmconcurso, novedadWeb)
        'Establecer par_ novedadesWeb en FALSE.
        opublicar.NovedadesWebOFF()
        'Si  par_ novedadesWeb (leido) = TRUE:
        If novedadWeb Then
            'cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & " Hay novedades Web concurso:" & _idpgmconcurso)
            'Establecer par_ publicando en TRUE.
            opublicar.Par_PublicandoON()
            Try
                'Publicar a Web.
                cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & " Publica a Web concurso:" & _idpgmconcurso)
                pgmConcursoBo.publicarWEB(_idpgmconcurso)
            Catch ex As Exception
                'Si hubo errores en la publicación → Establecer par_novedadesWeb en TRUE
                cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & " Error al publicar a Web:" & ex.Message)
                cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & " Establece par_novedadesWeb en TRUE")
                opublicar.NovedadesWebON()
            End Try
            'Establecer par_ publicando en FALSE.
            opublicar.Par_PublicandoOFF()
        End If
        'Leer par_ terminar.
        _terminar = opublicar.Lee_Par_Terminar()
        'Si  par_ terminar  = TRUE :
        If _terminar Then
            'Decrementar par_publicador en 1.
            opublicar.DecrementaPar_Publicador()
            'Finalizar el Timer.
            Timer1.Stop()
            'Finalizar ejecución.
            'System.Threading.Thread.Sleep(TimeSpan.FromSeconds(6))
            Me.Close()
            Me.Dispose()
        Else
            'se empieza de nuevo el ciclo
            'cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & "Comienza nuevo ciclo.")
            Dim segundos As Integer
            segundos = opublicar.ObtenerTiempoTimer()
            Timer1.Interval = segundos * 1000
            Timer1.Start()
        End If
    End Sub
    Private Sub Publicacion()
        Try
            Dim ProcesoBo As New ProccessBO
            Dim msj As String = ""
            vPrimerLoad = False
            'cPublicador.Pathlog = Application.StartupPath.ToString
            opublicar = New cPublicador
            Dim par_publicador As Integer

            Dim segundos As Integer
            Dim fechalog As String
            FileSystemHelper.DepurarArchivoLog()
            cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss.ffff") & " Ejecuto la depuracion de log")
            '** lee la fecha del ultimo log de los parametros
            fechalog = opublicar.LeeFechaLog
            cPublicador.fechalog = fechalog

            '** lee la variable Par_publicador
            par_publicador = opublicar.Lee_Par_publicador()

            '**** fin para timer 3

            'Si par_publicador > 0 → Finalizar ejecución.
            ''If par_publicador = 0 Then

            '** Incrementar par_publicador en 1.
            opublicar.IncrementaPar_Publicador()

            segundos = opublicar.ObtenerTiempoTimer_Extractos
            'empiezael timer de extractos
            TimerExtracto.Interval = segundos * 1000
            'TimerExtracto.Start()

            segundos = opublicar.ObtenerTiempoTimer_premios
            'empieza el timer de premios
            Timer3.Interval = segundos * 1000
            Timer3.Start()


            'empiezael timer de sueldos
            TimerSueldos.Interval = segundos * 1000
            TimerSueldos.Start()

            segundos = opublicar.ObtenerTiempoTimer()
            Timer1.Interval = segundos * 1000
            'Iniciar  el Timer.
            Timer1.Start()

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
        End Try

    End Sub

   

    Private Sub Timer3_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer3.Tick
        Try
            Dim BOpgmconcurso As New PgmConcursoBO
            Dim _idpgmconcurso As Long
            Dim novedadDisplay As Boolean
            Dim estado As Integer = 0
            Dim path_archivos_Premios As String = ""
            Dim opgmsorteo As PgmSorteo
            Dim juegoact As String = ""
            Dim nrosorteo As String = ""
            Dim nombre_Archivo As String = ""
            Dim prefijoPremio As String = General.PrefijoPremio
            Dim segundos As Integer
            Dim entro_juego_premio As Boolean = False
            Dim ventana As New FrmAccionTimer
            Dim avisa As Boolean = False
            Dim existe_archivo As Boolean = False
            Dim faltan_cargar_archivos As Boolean = True
            Dim faltan_cargar_premios As Boolean = False
            Dim juegosEncontrados As String = ""
            Dim msj As String = ""
            Dim BOpgmsorteo As New PgmSorteoBO
            Dim juegos As cValorPosicion
            '** loteria y loteria chica desde boldt los premios se generan con prefijo poz
            Dim prefijoPremioLoterias As String = General.PrefijoPozo
            Dim nombre_Archivoloterias As String = ""

            segundos = opublicar.ObtenerTiempoTimer_premios(True) 'obtiene el timer 2 de premios
            Timer3.Stop()

            opublicar.LeeParametrosDisplay(_idpgmconcurso, novedadDisplay)
            If _idpgmconcurso > 0 Then
                estado = BOpgmconcurso.EstadoPgmConcurso(_idpgmconcurso)
                _oPgmConcurso = BOpgmconcurso.getPgmConcurso(_idpgmconcurso)

                'creo la lista con los juegos que tienen premios
                If listaPremiosEncotrado Is Nothing Then
                    listaPremiosEncotrado = New ListaOrdenada(Of cValorPosicion)
                    For Each opgmsorteo In _oPgmConcurso.PgmSorteos
                        If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Or opgmsorteo.idJuego = 50 Or opgmsorteo.idJuego = 51 Then
                            juegos = New cValorPosicion
                            juegos.Valor = opgmsorteo.idJuego
                            juegos.Posicion = -1 ' se usa como marca de archivo encontrado,(- no existe,1 archivo encontrado,2 archivo informardo)
                            listaPremiosEncotrado.Add(juegos)
                        End If
                    Next
                End If
                '*** revisa si estan todos los premios cargados en BD
                For Each opgmsorteo In _oPgmConcurso.PgmSorteos
                    If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Or opgmsorteo.idJuego = 50 Or opgmsorteo.idJuego = 51 Then
                        If BOpgmsorteo.NoTienePremiosCargados(opgmsorteo.idPgmSorteo, opgmsorteo.idJuego) Then
                            faltan_cargar_premios = True
                        Else
                            MarcaArchivoJuegoConPremio_O_confirmado(opgmsorteo.idJuego)
                        End If
                    End If
                Next
                If Not faltan_cargar_archivos Then
                    Exit Sub
                End If

                '*** faltan premios por cargar y el concurso esta finalizado
                If estado = 40 Then
                    cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss") & " timer premios - el estado del  concurso es 40: ")
                    path_archivos_Premios = gralDal.getParametro("INI", "PATH_PREMIOS")
                    If Not path_archivos_Premios.EndsWith("\") Then
                        path_archivos_Premios = path_archivos_Premios & "\"
                    End If
                    '** recorre los sorteos del concurso
                    For Each opgmsorteo In _oPgmConcurso.PgmSorteos
                        '** busca archivos solo de los juegos que tienen premios
                        If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Or opgmsorteo.idJuego = 50 Or opgmsorteo.idJuego = 51 Then
                            estado = BOpgmsorteo.getEstadoPgmsorteo(opgmsorteo.idPgmSorteo)
                            If estado = 40 Then
                                entro_juego_premio = True
                                juegoact = opgmsorteo.idJuego.ToString.PadLeft(2, "00")
                                nrosorteo = opgmsorteo.nroSorteo.ToString.PadLeft(6, "000000")
                                nombre_Archivo = path_archivos_Premios & prefijoPremio & juegoact & nrosorteo & ".zip"
                                '** para loteria y loteria chica los premios se generan con prefijo poz
                                nombre_Archivoloterias = ""
                                If opgmsorteo.idJuego = 50 Or opgmsorteo.idJuego = 51 Then
                                    nombre_Archivoloterias = path_archivos_Premios & prefijoPremioLoterias & juegoact & nrosorteo & ".zip"
                                End If
                                ControlaArchivo(opgmsorteo.idJuego, nombre_Archivo, nombre_Archivoloterias)
                            Else
                                MarcaArchivoJuegoConPremio_O_confirmado(opgmsorteo.idJuego)
                            End If
                        End If
                    Next
                    juegosEncontrados = ArmaLeyendaJuegoEncontrados()
                    If juegosEncontrados.Trim() <> "" Then
                        cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss") & " timer premios - encontro archivo juegos: " & juegosEncontrados)
                        ventana.leyenda = "Existen archivos de premios para los juegos:" & juegosEncontrados
                        ventana.ShowDialog()
                        'accion 0:acepto,1 avisar mas tarde
                        'si acepto detengo el timer si se encontraron todos los archivos
                        If ventana.vAccion = 0 Then
                            If NoExisteArchivosPendientes() Then
                                ventana.Dispose()
                                Exit Sub
                            End If
                            Marcar_Archivo_JuegoInformados()
                        Else
                            Marcar_Archivo_JuegoEncontrados()
                        End If
                    End If
                End If
            End If
            'inicia el timer
            Timer3.Interval = segundos * 1000
            Timer3.Start()
            '********************************************************
        Catch ex As Exception
            'Throw New Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
        End Try
    End Sub
    Private Function NoExisteArchivosPendientes() As Boolean
        Try
            Dim juegopremio As cValorPosicion
            For Each juegopremio In listaPremiosEncotrado
                If juegopremio.Posicion = -1 Then
                    NoExisteArchivosPendientes = False
                    Exit Function
                End If
            Next
            NoExisteArchivosPendientes = True
        Catch ex As Exception
            NoExisteArchivosPendientes = False
            cPublicador.Log("Error publicacion: " & ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Information, "Pulicador on line")
        End Try
    End Function

    Private Sub MarcaArchivoJuegoEncontrado(ByVal idjuego As Long)
        Try
            Dim juegopremio As cValorPosicion

            For Each juegopremio In listaPremiosEncotrado
                If juegopremio.Valor = idjuego Then
                    juegopremio.Posicion = 1
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Information, "Publicador Online")
        End Try
    End Sub
    Private Sub ControlaArchivo(ByVal idJuego As Long, ByVal nombre_archivo As String, Optional ByVal nombre_archivo2 As String = "")
        Try

            Select Case idJuego
                Case 4
                    If Not premioQuini Then
                        If File.Exists(nombre_archivo) Then
                            premioQuini = True
                            MarcaArchivoJuegoEncontrado(idJuego)
                        End If
                    End If
                Case 13
                    If Not premioBrinco Then
                        If File.Exists(nombre_archivo) Then
                            premioBrinco = True
                            MarcaArchivoJuegoEncontrado(idJuego)
                        End If
                    End If
                Case 30
                    If Not premioPoceada Then
                        If File.Exists(nombre_archivo) Then
                            premioPoceada = True
                            MarcaArchivoJuegoEncontrado(idJuego)
                        End If
                    End If
                Case 50
                    'tiene que buscar el prefijo poz o pre en los archivos
                    If Not premioLoteria Then
                        If File.Exists(nombre_archivo) Or File.Exists(nombre_archivo2) Then
                            premioLoteria = True
                            MarcaArchivoJuegoEncontrado(idJuego)
                        End If
                    End If
                Case 51
                    'tiene que buscar el prefijo poz o pre en los archivos
                    If Not premioLoteriaChica Then
                        If File.Exists(nombre_archivo) Or File.Exists(nombre_archivo2) Then
                            premioLoteriaChica = True
                            MarcaArchivoJuegoEncontrado(idJuego)
                        End If
                    End If
            End Select
        Catch ex As Exception
            Throw New Exception("Error existe archivo:" & ex.Message)
        End Try
    End Sub

    Private Sub Marcar_Archivo_JuegoInformados()
        Try
            Dim juegopremio As cValorPosicion

            For Each juegopremio In listaPremiosEncotrado
                If juegopremio.Posicion = 1 Then
                    juegopremio.Posicion = 2

                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Information, "Publicador Online")
        End Try
    End Sub
    Private Sub Marcar_Archivo_JuegoEncontrados()
        Try
            Dim juegopremio As cValorPosicion

            For Each juegopremio In listaPremiosEncotrado
                If juegopremio.Posicion = 2 Then
                    juegopremio.Posicion = 1
                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Information, "Publicador Online")
        End Try
    End Sub
    Private Function ArmaLeyendaJuegoEncontrados() As String
        Try
            Dim juegopremio As cValorPosicion
            Dim leyenda As String = ""
            For Each juegopremio In listaPremiosEncotrado
                If juegopremio.Posicion = 1 Then
                    Select Case juegopremio.Valor
                        Case 4
                            leyenda = leyenda & "Quini 6 , "

                        Case 13
                            leyenda = leyenda & "Brinco , "

                        Case 30
                            leyenda = leyenda & "Poceada federal , "

                        Case 50
                            leyenda = leyenda & "Lotería , "

                        Case 51
                            leyenda = leyenda & "Lotería Chica , "
                        Case Else
                            leyenda = leyenda & "el juego es ->" & juegopremio.Valor & "<-"
                    End Select
                End If
            Next
            If leyenda.Trim <> "" Then
                leyenda = Mid(leyenda.Trim, 1, Len(leyenda.Trim) - 1)
            End If
            Return leyenda.Trim
        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Information, "Publicador Online")
        End Try
    End Function

    Private Sub MarcaArchivoJuegoConPremio_O_confirmado(ByVal idjuego As Long)
        Try
            Dim juegopremio As cValorPosicion

            For Each juegopremio In listaPremiosEncotrado
                If juegopremio.Valor = idjuego Then
                    juegopremio.Posicion = 3 'para que no busque el archivo
                    Select Case idjuego
                        Case 4
                            premioQuini = True
                        Case 13
                            premioBrinco = True
                        Case 30
                            premioPoceada = True
                        Case 50
                            premioLoteria = True
                        Case 51
                            premioLoteriaChica = True
                    End Select
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Information, "Publicador Online")
        End Try
    End Sub

    

    Private Sub TimerExtracto_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerExtracto.Tick
        Try
            Dim BOpgmconcurso As New PgmConcursoBO
            Dim _idpgmconcurso As Long
            Dim novedadDisplay As Boolean
            Dim estado As Integer = 0
            Dim path_archivos_Extractos As String = General.CarpetaOrigenArchivosExtractoBoldt
            Dim opgmsorteo As PgmSorteo
            Dim juegoact As String = ""
            Dim nrosorteo As String = ""
            Dim nombre_Archivo As String = ""
            Dim segundos As Integer
            Dim entro_juego_premio As Boolean = False
            Dim ventana As New FrmAccionTimer
            Dim avisa As Boolean = False
            Dim existe_archivo As Boolean = False
            Dim faltan_cargar_archivos As Boolean = True
            Dim faltan_cargar_premios As Boolean = False
            Dim extractosEncontrados As String = ""
            Dim msj As String = ""
            Dim BOpgmsorteo As New PgmSorteoBO
            Dim Extractos As cValorPosicion
            Dim oArchivoBoldt As New Sorteos.Bussiness.ArchivoBoldtBO

            segundos = opublicar.ObtenerTiempoTimer_Extractos(True) 'obtiene el timer 2 de premios
            TimerExtracto.Stop()

            opublicar.LeeParametrosDisplay(_idpgmconcurso, novedadDisplay)
            If _idpgmconcurso > 0 Then
                estado = BOpgmconcurso.EstadoPgmConcurso(_idpgmconcurso)
                _oPgmConcurso = BOpgmconcurso.getPgmConcurso(_idpgmconcurso)

                'creo la lista con los juegos que tienen premios
                If listaExtractoEncontrado Is Nothing Then
                    listaExtractoEncontrado = New ListaOrdenada(Of cValorPosicion)
                    For Each opgmsorteo In _oPgmConcurso.PgmSorteos
                        If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Then
                            Extractos = New cValorPosicion
                            Extractos.Valor = opgmsorteo.idJuego
                            Extractos.Posicion = -1 ' se usa como marca de archivo encontrado,(- no existe,1 archivo encontrado,2 archivo informardo)
                            listaExtractoEncontrado.Add(Extractos)
                        End If
                    Next
                End If

                '*** faltan premios por cargar y el concurso esta finalizado
                If estado = 40 Then

                    If Not path_archivos_Extractos.EndsWith("\") Then
                        path_archivos_Extractos = path_archivos_Extractos & "\"
                    End If
                    '** recorre los sorteos del concurso
                    For Each opgmsorteo In _oPgmConcurso.PgmSorteos
                        '** busca archivos solo de los juegos que tienen premios
                        If opgmsorteo.idJuego = 4 Or opgmsorteo.idJuego = 13 Or opgmsorteo.idJuego = 30 Then
                            estado = BOpgmsorteo.getEstadoPgmsorteo(opgmsorteo.idPgmSorteo)
                            If estado = 40 Then
                                entro_juego_premio = True
                                juegoact = opgmsorteo.idJuego.ToString.PadLeft(2, "00")
                                nrosorteo = opgmsorteo.nroSorteo.ToString.PadLeft(6, "000000")
                                nombre_Archivo = path_archivos_Extractos & oArchivoBoldt.CreaNombreArchivoExtracto(juegoact, nrosorteo) & ".zip"
                                ControlaArchivoExtracto(opgmsorteo.idJuego, nombre_Archivo)
                            Else
                                MarcaArchivoExtractoconfirmado(opgmsorteo.idJuego)
                            End If
                        End If
                    Next
                    extractosEncontrados = ArmaLeyendaExtractoEncontrados()
                    If extractosEncontrados.Trim() <> "" Then
                        cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss") & " timer extracto - encontro archivo extracto: " & extractosEncontrados)
                        ventana.leyenda = "Existen archivos de EXTRACTOS para los juegos:  " & extractosEncontrados
                        ventana.ShowDialog()
                        'accion 0:acepto,1 avisar mas tarde
                        'si acepto detengo el timer si se encontraron todos los archivos
                        If ventana.vAccion = 0 Then
                            If NoExisteArchivosExtractoPendientes() Then
                                ventana.Dispose()
                                Exit Sub
                            End If
                            Marcar_Archivo_ExtractoInformados()
                        Else
                            Marcar_Archivo_ExtractoEncontrados()
                        End If
                    End If
                End If
            End If

            'inicia el timer
            TimerExtracto.Interval = segundos * 1000
            TimerExtracto.Start()
            '********************************************************
        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("timer_extracto_tick:" & ex.Message)
        End Try

    End Sub
    '***** funciones para archivos extractos ************
    Private Function NoExisteArchivosExtractoPendientes() As Boolean
        Try
            Dim juegoExtracto As cValorPosicion
            For Each juegoExtracto In listaExtractoEncontrado
                If juegoExtracto.Posicion = -1 Then
                    NoExisteArchivosExtractoPendientes = False
                    Exit Function
                End If
            Next
            NoExisteArchivosExtractoPendientes = True
        Catch ex As Exception
            NoExisteArchivosExtractoPendientes = False
            cPublicador.Log("Error publicacion: " & ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Information, "Publicador Online")
        End Try
    End Function

    Private Sub MarcaArchivoExtractoEncontrado(ByVal idjuego As Long)
        Try
            Dim juegoextracto As cValorPosicion

            For Each juegoextracto In listaExtractoEncontrado
                If juegoextracto.Valor = idjuego Then
                    juegoextracto.Posicion = 1
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("MarcaArchivoExtractoEncontrado:" & ex.Message)
        End Try
    End Sub
    Private Sub ControlaArchivoExtracto(ByVal idJuego As Long, ByVal nombre_archivo As String)
        Try

            Select Case idJuego
                Case 4
                    If Not extractoQuini Then
                        If File.Exists(nombre_archivo) Then
                            extractoQuini = True
                            MarcaArchivoExtractoEncontrado(idJuego)
                        End If
                    End If
                Case 13
                    If Not extractoBrinco Then
                        If File.Exists(nombre_archivo) Then
                            extractoBrinco = True
                            MarcaArchivoExtractoEncontrado(idJuego)
                        End If
                    End If
                Case 30
                    If Not extractoPoceada Then
                        If File.Exists(nombre_archivo) Then
                            extractoPoceada = True
                            MarcaArchivoExtractoEncontrado(idJuego)
                        End If
                    End If

            End Select
        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("Problema ControlaArchivoExtracto:" & ex.Message)
        End Try
    End Sub

    Private Sub Marcar_Archivo_ExtractoInformados()
        Try
            Dim juegoExtracto As cValorPosicion

            For Each juegoExtracto In listaExtractoEncontrado

                If juegoExtracto.Posicion = 1 Then
                    juegoExtracto.Posicion = 2

                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("Problema Marcar_Archivo_ExtractoInformados:" & ex.Message)
        End Try
    End Sub
    Private Sub Marcar_Archivo_ExtractoEncontrados()
        Try
            Dim juegoExtracto As cValorPosicion

            For Each juegoExtracto In listaExtractoEncontrado
                If juegoExtracto.Posicion = 2 Then
                    juegoExtracto.Posicion = 1
                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("Marcar_Archivo_ExtractoEncontrados:" & ex.Message)
        End Try
    End Sub
    Private Function ArmaLeyendaExtractoEncontrados() As String
        Try
            Dim juegoExtracto As cValorPosicion
            Dim leyenda As String = ""
            For Each juegoExtracto In listaExtractoEncontrado
                If juegoExtracto.Posicion = 1 Then
                    Select Case juegoExtracto.Valor
                        Case 4
                            leyenda = leyenda & "Quini 6 , "

                        Case 13
                            leyenda = leyenda & "Brinco , "

                        Case 30
                            leyenda = leyenda & "Poceada federal , "
                        Case Else
                            leyenda = leyenda & "el juego es: " & juegoExtracto.Valor
                    End Select
                End If
            Next
            If leyenda.Trim <> "" Then
                leyenda = Mid(leyenda.Trim, 1, Len(leyenda.Trim) - 1)
            End If
            Return leyenda
        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("ArmaLeyendaExtractoEncontrados:" & ex.Message)

        End Try
    End Function
    Private Sub MarcaArchivoExtractoconfirmado(ByVal idjuego As Long)
        Try
            Dim juegoExtracto As cValorPosicion

            For Each juegoExtracto In listaExtractoEncontrado
                If juegoExtracto.Valor = idjuego Then
                    juegoExtracto.Posicion = 3 'para que no busque el archivo
                    Select Case idjuego
                        Case 4
                            extractoQuini = True
                        Case 13
                            extractoBrinco = True
                        Case 30
                            extractoPoceada = True
                    End Select
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("MarcaArchivoExtractoconfirmado:" & ex.Message)
        End Try
    End Sub
    '**** fin funciones para extractos ******************

    Private Sub TimerSueldos_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles TimerSueldos.Tick

        Try
            Dim BOpgmconcurso As New PgmConcursoBO
            Dim _idpgmconcurso As Long = 0
            Dim novedadDisplay As Boolean = False
            Dim estado As Integer = 0
            Dim path_archivos_Premios As String = ""
            Dim opgmsorteo As PgmSorteo
            Dim juegoact As String = ""
            Dim nrosorteo As String = ""
            Dim nombre_Archivo As String = ""
            Dim prefijoPremio As String = General.PrefijoSueldo
            Dim segundos As Integer
            Dim entro_juego_premio As Boolean = False
            Dim ventana As New FrmAccionTimer
            Dim avisa As Boolean = False
            Dim existe_archivo As Boolean = False
            Dim faltan_cargar_archivos As Boolean = True
            Dim faltan_cargar_premios As Boolean = False
            Dim juegosEncontrados As String = ""
            Dim msj As String = ""
            Dim BOpgmsorteo As New PgmSorteoBO
            Dim juegos As cValorPosicion
            Dim Noesbrinco As Boolean = True


            segundos = opublicar.ObtenerTiempoTimer_premios(True) 'obtiene el timer 2 de premios
            TimerSueldos.Stop()

            opublicar.LeeParametrosDisplay(_idpgmconcurso, novedadDisplay)
            'MsgBox(_idpgmconcurso)
            If _idpgmconcurso > 0 Then
                estado = BOpgmconcurso.EstadoPgmConcurso(_idpgmconcurso)
                'MsgBox(estado)
                Try
                    _oPgmConcurso = BOpgmconcurso.getPgmConcurso(_idpgmconcurso)
                Catch ex3 As Exception
                    _oPgmConcurso = Nothing
                End Try
                'MsgBox("pasooo")

                If (_oPgmConcurso IsNot Nothing) Then
                    For Each opgmsorteo In _oPgmConcurso.PgmSorteos
                        If opgmsorteo.idJuego = 13 Then
                            Noesbrinco = False
                        End If
                    Next
                    If Noesbrinco Then 'si no es brinco no tiene que buscar archivo de sueldos
                        'inicia el timer
                        TimerSueldos.Interval = segundos * 1000
                        TimerSueldos.Start()
                        Exit Sub
                    Else
                        If estado <> 40 Then
                            'inicia el timer
                            TimerSueldos.Interval = segundos * 1000
                            TimerSueldos.Start()
                            Exit Sub
                        End If
                    End If

                    'creo la lista con los juegos que tienen premios
                    If listaSueldosEncontrado Is Nothing Then
                        listaSueldosEncontrado = New ListaOrdenada(Of cValorPosicion)
                        For Each opgmsorteo In _oPgmConcurso.PgmSorteos

                            If opgmsorteo.idJuego = 13 Then
                                juegos = New cValorPosicion
                                juegos.Valor = opgmsorteo.idJuego
                                juegos.Posicion = -1 ' se usa como marca de archivo encontrado,(-1 no existe,1 archivo encontrado,2 archivo informardo)
                                listaSueldosEncontrado.Add(juegos)
                            End If
                        Next
                    End If
                    '*** revisa si estan todos los premios cargados en BD
                    For Each opgmsorteo In _oPgmConcurso.PgmSorteos
                        If opgmsorteo.idJuego = 13 Then
                            If BOpgmsorteo.NoTienePremiosSueldosCargados(opgmsorteo.idPgmSorteo, opgmsorteo.idJuego) Then
                                faltan_cargar_premios = True
                            Else
                                MarcaArchivoJuegoConPremioSueldos_O_confirmado(opgmsorteo.idJuego)
                            End If
                        End If
                    Next
                    If Not faltan_cargar_archivos Then
                        Exit Sub
                    End If

                    '*** faltan premios por cargar y el concurso esta finalizado
                    If estado = 40 Then

                        path_archivos_Premios = gralDal.getParametro("INI", "PATH_PREMIOS")
                        If Not path_archivos_Premios.EndsWith("\") Then
                            path_archivos_Premios = path_archivos_Premios & "\"
                        End If
                        '** recorre los sorteos del concurso
                        For Each opgmsorteo In _oPgmConcurso.PgmSorteos
                            '** busca archivos solo de los juegos que tienen premios
                            If opgmsorteo.idJuego = 13 Then
                                estado = BOpgmsorteo.getEstadoPgmsorteo(opgmsorteo.idPgmSorteo)
                                If estado = 40 Then
                                    entro_juego_premio = True
                                    juegoact = opgmsorteo.idJuego.ToString.PadLeft(2, "00")
                                    nrosorteo = opgmsorteo.nroSorteo.ToString.PadLeft(6, "000000")
                                    nombre_Archivo = path_archivos_Premios & prefijoPremio & juegoact & nrosorteo & ".zip"
                                    ControlaArchivoSueldo(opgmsorteo.idJuego, nombre_Archivo)
                                Else
                                    MarcaArchivoJuegoConPremioSueldos_O_confirmado(opgmsorteo.idJuego)
                                End If
                            End If
                        Next
                        juegosEncontrados = ArmaLeyendaSueldoEncontrados()
                        If juegosEncontrados.Trim <> "" Then
                            cPublicador.Log(Now.ToString("dd/MM/yyyy  HH:mm:ss") & " timer sueldo - encontro archivo sueldo: " & juegosEncontrados)
                            ventana.leyenda = "Existen archivos de Premios SUELDOS para Brinco."
                            ventana.ShowDialog()
                            'accion 0:acepto,1 avisar mas tarde
                            'si acepto detengo el timer si se encontraron todos los archivos
                            If ventana.vAccion = 0 Then
                                If NoExisteArchivosSueldosPendientes() Then
                                    ventana.Dispose()
                                    Exit Sub
                                End If
                                Marcar_Archivo_SueldoInformados()
                            Else
                                Marcar_Archivo_SueldoEncontrados()
                            End If
                        End If
                    End If
                End If
            End If

            'inicia el timer
            TimerSueldos.Interval = segundos * 1000
            TimerSueldos.Start()
            '********************************************************
        Catch ex As Exception
            Try
                cPublicador.Log("Error publicacion: " & ex.Message)
            Catch ex2 As Exception
            End Try
            Throw New Exception("TimerSueldos_Tick -> " & ex.Message)
        End Try
    End Sub


    '***** funciones para archivos Sueldos ************
    Private Function NoExisteArchivosSueldosPendientes() As Boolean
        Try
            Dim juegoExtracto As cValorPosicion
            For Each juegoExtracto In listaSueldosEncontrado
                If juegoExtracto.Posicion = -1 Then
                    NoExisteArchivosSueldosPendientes = False
                    Exit Function
                End If
            Next
            NoExisteArchivosSueldosPendientes = True
        Catch ex As Exception
            NoExisteArchivosSueldosPendientes = False
            MsgBox(ex.Message, MsgBoxStyle.Information, "Publicador Online")
        End Try
    End Function

    Private Sub MarcaArchivoSueldoEncontrado(ByVal idjuego As Long)
        Try
            Dim juegoextracto As cValorPosicion

            For Each juegoextracto In listaSueldosEncontrado
                If juegoextracto.Valor = idjuego Then
                    juegoextracto.Posicion = 1
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("MarcaArchivoSueldoEncontrado:" & ex.Message)
        End Try
    End Sub
    Private Sub ControlaArchivoSueldo(ByVal idJuego As Long, ByVal nombre_archivo As String)
        Try
            If idJuego = 13 Then
                If Not sueldobrinco Then
                    If File.Exists(nombre_archivo) Then
                        sueldobrinco = True
                        MarcaArchivoSueldoEncontrado(idJuego)
                    End If
                End If
            End If

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("Problema ControlaArchivoSueldo:" & ex.Message)
        End Try
    End Sub

    Private Sub Marcar_Archivo_SueldoInformados()
        Try
            Dim juegoExtracto As cValorPosicion

            For Each juegoExtracto In listaSueldosEncontrado

                If juegoExtracto.Posicion = 1 Then
                    juegoExtracto.Posicion = 2

                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("Problema Marcar_Archivo_SueldoInformados:" & ex.Message)
        End Try
    End Sub
    Private Sub Marcar_Archivo_SueldoEncontrados()
        Try
            Dim juegoExtracto As cValorPosicion

            For Each juegoExtracto In listaSueldosEncontrado
                If juegoExtracto.Posicion = 2 Then
                    juegoExtracto.Posicion = 1
                End If
            Next

        Catch ex As Exception
            Throw New Exception("Marcar_Archivo_SueldoEncontrados:" & ex.Message)
        End Try
    End Sub
    Private Function ArmaLeyendaSueldoEncontrados() As String
        Try
            Dim juegoExtracto As cValorPosicion
            Dim leyenda As String = ""
            For Each juegoExtracto In listaSueldosEncontrado
                If juegoExtracto.Posicion = 1 Then
                    Select Case juegoExtracto.Valor
                        Case 4
                            leyenda = leyenda & "Quini 6 , "

                        Case 13
                            leyenda = leyenda & "Brinco , "

                        Case 30
                            leyenda = leyenda & "Poceada federal , "

                    End Select
                End If
            Next
            If leyenda.Trim <> "" Then
                leyenda = Mid(leyenda.Trim, 1, Len(leyenda.Trim) - 1)
            End If
            Return leyenda.Trim
        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("ArmaLeyendaSueldoEncontrados:" & ex.Message)
        End Try
    End Function
    Private Sub MarcaArchivoSueldoconfirmado(ByVal idjuego As Long)
        Try
            Dim juegoExtracto As cValorPosicion

            For Each juegoExtracto In listaSueldosEncontrado
                If juegoExtracto.Valor = idjuego Then
                    juegoExtracto.Posicion = 3 'para que no busque el archivo
                    Select Case idjuego
                        
                        Case 13
                            SueldoBrinco = True
                        
                    End Select
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            Throw New Exception("MarcaArchivoSueldoconfirmado:" & ex.Message)
        End Try
    End Sub
    Private Sub MarcaArchivoJuegoConPremioSueldos_O_confirmado(ByVal idjuego As Long)
        Try
            Dim juegopremio As cValorPosicion

            For Each juegopremio In listaSueldosEncontrado
                If juegopremio.Valor = idjuego Then
                    juegopremio.Posicion = 3 'para que no busque el archivo
                    Select Case idjuego
                        
                        Case 13
                            sueldobrinco = True
                        
                    End Select
                    Exit Sub
                End If
            Next

        Catch ex As Exception
            cPublicador.Log("Error publicacion: " & ex.Message)
            MsgBox(ex.Message, MsgBoxStyle.Information, "Publicador Online")
        End Try
    End Sub
    '**** fin funciones para extractos ******************
End Class
