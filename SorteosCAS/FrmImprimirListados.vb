Imports sorteos.helpers
Imports libEntities.Entities
Imports sorteos.bussiness

Imports sorteos.extractos
Public Class FrmImprimirListados
    Dim lsJuegos As New ListaOrdenada(Of cJuegosSorteo)
    Dim lsjuegoSorteos As New ListaOrdenada(Of cJuegoSorteo)
    Dim lsjuegoSorteosParametros As New ListaOrdenada(Of cJuegoSorteo)
    Dim lsSorteosActuales As ListaOrdenada(Of cJuegoSorteo)
    Dim SorteosBO As New PgmSorteoBO
    Dim PgmConcursoBO As New PgmConcursoBO
    Dim _CantidadDias As Integer
    Dim CargaPanelSorteo As Boolean

    Private Sub CargarSorteos(ByVal cantDias As Integer)
        Try
            lsjuegoSorteos = SorteosBO.GetJuegoSorteos(cantDias)
        Catch ex As Exception
            MsgBox("Error CargarSorteos:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub CargarJuegos(ByVal cantDias As Integer)
        Try
            lsJuegos = SorteosBO.GetJuegosSorteo(cantDias)
        Catch ex As Exception
            MsgBox("Error CargarJuegos:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub CrearPanelJuegos()
        Dim _juego As New cJuegosSorteo
       
        Try
            If lsJuegos Is Nothing Then Exit Sub
            Me.CheckedListBoxJuegos.DataSource = lsJuegos
            CheckedListBoxJuegos.DisplayMember = "nombre"
            CheckedListBoxJuegos.ValueMember = "IdJuego"
        Catch ex As Exception
            MsgBox("Error CrearPanelJuegos:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub FrmImprimirListados_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
        MDIContenedor.formImprimir = Nothing
    End Sub

    Private Sub FrmImprimirListados_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Try
            Me.Location = New System.Drawing.Point(0, 0)

            If General.Modo_Operacion.ToUpper() = "PC-A" Or General.Modo_Operacion.ToUpper() = "PC-B" Then
                chkDifCuad.Enabled = True
            Else
                chkDifCuad.Enabled = False
            End If

            If General.Jurisdiccion = "S" Then
                chkParametrosGan.Visible = True
            End If
            _CantidadDias = 30
            CargarJuegos(_CantidadDias)
            MDIContenedor.CerrarHijo = False
            If lsJuegos.Count = 0 Then
                MsgBox("No se encontraron juegos en condiciones de ser visualizados.", MsgBoxStyle.Information)
                Me.GroupBoxJuegos.Enabled = False
                Me.GroupBoxSorteos.Enabled = False
                Me.BtnImprimir.Enabled = False
                MDIContenedor.CerrarHijo = True
                Exit Sub
            Else
                Me.GroupBoxJuegos.Enabled = True
                Me.GroupBoxSorteos.Enabled = True
                Me.BtnImprimir.Enabled = True

            End If

            CrearPanelJuegos()
            CargarSorteos(_CantidadDias)
            If General.Modo_Operacion.ToUpper() = "PC-A" Or General.Modo_Operacion.ToUpper() = "PC-B" Then
                chkDifCuad.Enabled = True
            Else
                chkDifCuad.Enabled = False
            End If
            AddHandler CheckedListBoxJuegos.SelectedIndexChanged, AddressOf CheckedListBoxjuegos_SelectedIndexChanged
        Catch ex As Exception
            MsgBox("Error FrmImprimirListados_Load:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub CrearPanelSorteos(ByVal lsSorteos As ListaOrdenada(Of cJuegoSorteo))
        Try
            Dim juego As cJuegoSorteo
            If lsSorteos Is Nothing Then Exit Sub

            CheckedListBoxSorteos.DisplayMember = "displaytext"
            CheckedListBoxSorteos.ValueMember = "IdJuego"
            CheckedListBoxSorteos.Items.Clear()
            For Each juego In lsSorteos
                If juego.Seleccionada = 1 Then
                    Me.CheckedListBoxSorteos.Items.Add(juego, True)
                Else
                    Me.CheckedListBoxSorteos.Items.Add(juego, False)
                End If
            Next
        Catch ex As Exception
            MsgBox("Error CrearPanelSorteos:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Function ObtenerSorteosJuego(ByVal pidJuego As Integer) As ListaOrdenada(Of cJuegoSorteo)
        Dim ls As New ListaOrdenada(Of cJuegoSorteo)
        Try
            For Each _sorteo In lsjuegoSorteos
                If _sorteo.IdJuego = pidJuego Then
                    ls.Add(_sorteo)
                End If
            Next
            Return ls
        Catch ex As Exception
            Return Nothing
            MsgBox("Error ObtenerSorteosJuego:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Function
    Private Function ObtenerSorteosSeleccionados(ByVal pidJuego As Integer) As ListaOrdenada(Of cJuegoSorteo)
        Dim ls As New ListaOrdenada(Of cJuegoSorteo)
        Try
            For Each _sorteo In lsjuegoSorteos
                If _sorteo.IdJuego = pidJuego And _sorteo.Seleccionada = 1 Then
                    ls.Add(_sorteo)
                End If
            Next
            Return ls
        Catch ex As Exception
            Return Nothing
            MsgBox("Error ObtenerSorteosSeleccionados:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Function

    Private Sub CheckedListBoxjuegos_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxJuegos.ItemCheck
        Dim juego As cJuegosSorteo
        Dim _index As Integer
        Try
            juego = CheckedListBoxJuegos.SelectedItem
            _index = CheckedListBoxJuegos.SelectedIndex

            CargaPanelSorteo = False
            If CheckedListBoxJuegos.GetItemChecked(_index) = True Then
                LimpiarSorteos(lsSorteosActuales, juego.IdJuego)
                CrearPanelSorteos(lsSorteosActuales)
                ActualizarJuego(lsJuegos, juego.IdJuego, False)
            Else 'si estaba sin chequear,quiero decir que se chequea
                ActualizarJuego(lsJuegos, juego.IdJuego, True)
            End If
        Catch ex As Exception
            MsgBox("Error CheckedListBoxjuegos_ItemCheck:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub ActualizarJuego(ByVal ls As ListaOrdenada(Of cJuegosSorteo), ByVal pidJuego As Integer, ByVal seleccion As Boolean)
        Dim juego As cJuegosSorteo
        Try
            For Each juego In ls
                If juego.IdJuego = pidJuego Then
                    If seleccion Then
                        juego.Seleccionada = 1
                    Else
                        juego.Seleccionada = 0
                    End If
                    Exit Sub
                End If
            Next
        Catch ex As Exception
            MsgBox("Error ActualizarJuego:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub SeleccionarSorteo(ByVal ls As ListaOrdenada(Of cJuegoSorteo), ByVal nroSorteo As Integer)
        Dim juego As cJuegoSorteo
        Try
            For Each juego In ls
                If juego.NroSorteo = nroSorteo Then
                    juego.Seleccionada = 1
                End If
            Next
        Catch ex As Exception
            MsgBox("Error SeleccionarSorteo:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Sub QuitarSorteo(ByVal ls As ListaOrdenada(Of cJuegoSorteo), ByVal nroSorteo As Integer)
        Dim juego As cJuegoSorteo
        Try
            For Each juego In ls
                If juego.NroSorteo = nroSorteo Then
                    juego.Seleccionada = 0
                End If
            Next
        Catch ex As Exception
            MsgBox("Error QuitarSorteo:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub LimpiarSorteos(ByVal ls As ListaOrdenada(Of cJuegoSorteo), ByVal pidJuego As Integer)
        Dim juego As cJuegoSorteo
        Try
            For Each juego In ls
                If juego.IdJuego = pidJuego Then
                    juego.Seleccionada = 0
                End If
            Next
        Catch ex As Exception
            MsgBox("Error LimpiarSorteos:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub CheckedListBoxSorteos_ItemCheck(ByVal sender As Object, ByVal e As System.Windows.Forms.ItemCheckEventArgs) Handles CheckedListBoxSorteos.ItemCheck
        Dim juego As cJuegoSorteo
        Dim _index As Integer
        Dim _indexjuego As Integer
        Try

            juego = CheckedListBoxSorteos.SelectedItem
            _indexjuego = CheckedListBoxJuegos.SelectedIndex
            If Not CheckedListBoxJuegos.GetItemChecked(_indexjuego) Then
                CheckedListBoxJuegos.SetItemChecked(_indexjuego, True)
            End If
            If CargaPanelSorteo = False Then
                _index = CheckedListBoxSorteos.SelectedIndex
                If CheckedListBoxSorteos.GetItemChecked(_index) Then
                    QuitarSorteo(lsSorteosActuales, juego.NroSorteo)
                    If CheckedListBoxSorteos.CheckedItems.Count = 1 Then
                        CheckedListBoxJuegos.SetItemChecked(_indexjuego, False)
                    End If
                Else
                    SeleccionarSorteo(lsSorteosActuales, juego.NroSorteo)
                End If
            End If
        Catch ex As Exception
            MsgBox("Error CheckedListBoxSorteos_ItemCheck:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub CheckedListBoxjuegos_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles CheckedListBoxJuegos.SelectedIndexChanged
        Dim cambio As Boolean = True
        Try
            If CheckedListBoxJuegos.Tag IsNot Nothing Then
                If CheckedListBoxJuegos.Tag.Equals(CheckedListBoxJuegos.SelectedItem) And (CheckedListBoxSorteos.Items.Count > 0) Then
                    cambio = False
                End If
            End If
            CheckedListBoxJuegos.Tag = CheckedListBoxJuegos.SelectedItem
            If cambio Then
                Dim juego As cJuegosSorteo
                Dim _index As Integer
                juego = CheckedListBoxJuegos.SelectedItem
                _index = CheckedListBoxJuegos.SelectedIndex
                lsSorteosActuales = ObtenerSorteosJuego(juego.IdJuego)
                CargaPanelSorteo = True
                CrearPanelSorteos(lsSorteosActuales)
                CargaPanelSorteo = False
            End If
        Catch ex As Exception
            MsgBox("Error CheckedListBoxjuegos_SelectedIndexChanged:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub

    Private Sub BtnImprimir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles BtnImprimir.Click
        Dim msj As String = ""
        Dim destino As String = "0"
        Dim path As String = ""
        Dim sorteo As cJuegoSorteo
        Dim pgmSorteoBo As New PgmSorteoBO
        Dim pgmConcursoBo As New PgmConcursoBO
        Me.Cursor = Cursors.WaitCursor

        Try
            If Me.chkParametrosGan.Checked = False And Me.chkParametros.Checked = False And Me.ChkExtracciones.Checked = False And ChkExtractos.Checked = False And Me.chkDifCuad.Checked = False And Me.Chkpozoproximo.Checked = False And Me.chkEscGan.Checked = False Then
                Me.Cursor = Cursors.Default
                MsgBox("Debe seleccionar un tipo de listado.", MsgBoxStyle.Information)
                Exit Sub
            End If
            If CheckedListBoxJuegos.CheckedItems.Count = 0 Then
                Me.Cursor = Cursors.Default
                MsgBox("No existen juegos seleccionados.", MsgBoxStyle.Information)
                Exit Sub
            End If
            If JuegosSinSorteos(msj) Then
                Me.Cursor = Cursors.Default
                If MsgBox("Los siguientes juegos no poseen sorteos seleccionados." & vbCrLf & msj & "¿Desea continuar de todas maneras?", MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo + MsgBoxStyle.Question) = MsgBoxResult.No Then
                    Exit Sub
                End If
            End If
            '** impresion de parametros**
            'se quitan los concurso repetidos y luego se imprimen
            Dim oconcurso As cJuegoSorteo
            If Me.chkParametros.Checked = True Then
                lsjuegoSorteosParametros = Nothing
                QuitarConcursoRepetidos(lsjuegoSorteos)
                For Each oconcurso In lsjuegoSorteosParametros
                    If oconcurso.Seleccionada = 1 Then
                        ImprimirParametros(oconcurso.IdPgmConcurso, "N")
                    End If
                Next
                
            End If
            If Me.chkParametrosGan.Checked = True Then
                lsjuegoSorteosParametros = Nothing
                QuitarConcursoRepetidos(lsjuegoSorteos)
                For Each oconcurso In lsjuegoSorteosParametros
                    If oconcurso.Seleccionada = 1 Then
                        ImprimirParametros(oconcurso.IdPgmConcurso, "S")
                    End If
                Next

            End If
            For Each sorteo In lsjuegoSorteos
                If sorteo.Seleccionada = 1 Then
                    If Me.ChkExtracciones.Checked = True Then
                        If Not pgmSorteoBo.GenerarListadoExtracciones(sorteo.IdPgmConcurso, sorteo.idPgmSorteo, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, destino, path, msj) Then
                            Me.Cursor = Cursors.Default
                            MsgBox(msj, MsgBoxStyle.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If
                    If Me.ChkExtractos.Checked = True Then
                        If Not pgmSorteoBo.GenerarListadoExtractoOficial(sorteo.IdPgmConcurso, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes) Then
                            MsgBox(msj, MsgBoxStyle.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If
                    If Me.chkDifCuad.Checked = True Then
                        If Not pgmSorteoBo.GenerarListadoDifCuad(sorteo.IdPgmConcurso, sorteo.idPgmSorteo, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, destino, path, 1, msj) Then
                            MsgBox(msj, MsgBoxStyle.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If
                    If Me.Chkpozoproximo.Checked = True Then
                        If sorteo.IdJuego <> 4 And sorteo.IdJuego <> 13 Then
                            MsgBox("Reporte solo disponible para Quini 6 y Brinco.", MsgBoxStyle.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                        If Not pgmConcursoBo.ImprimirParametrospozoproximo(sorteo.IdPgmConcurso, 0, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, msj) Then
                            MsgBox(msj, MsgBoxStyle.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If
                    If Me.chkEscGan.Checked Then
                        If sorteo.IdJuego <> 4 And sorteo.IdJuego <> 13 Then
                            MsgBox("Reporte solo disponible para Quini 6 y Brinco.", MsgBoxStyle.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                        If Not pgmConcursoBo.ImprimirEscenariosGanadores1Premio(sorteo.IdPgmConcurso, 0, Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes, msj) Then
                            MsgBox(msj, MsgBoxStyle.Information)
                            Me.Cursor = Cursors.Default
                            Exit Sub
                        End If
                    End If
                End If
            Next
            Me.Cursor = Cursors.Default

        Catch ex As Exception
            Me.Cursor = Cursors.Default
            MsgBox("Error BtnImprimir_Click:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Sub
    Private Function JuegosSinSorteos(ByRef msj As String) As Boolean
        Dim str As String = ""
        Dim juego As cJuegosSorteo
        Dim lstSorteos As ListaOrdenada(Of cJuegoSorteo)
        Try
            JuegosSinSorteos = False
            For Each juego In lsJuegos
                If juego.Seleccionada = 1 Then
                    lstSorteos = ObtenerSorteosSeleccionados(juego.IdJuego)
                    If lstSorteos.Count = 0 Then
                        str = str & "-" & juego.Nombre & vbCrLf
                        JuegosSinSorteos = True
                    End If
                End If
            Next
            msj = str
        Catch ex As Exception
            JuegosSinSorteos = False
            MsgBox("Error JuegosSinSorteos:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Function

    Private Sub btnCerrar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCerrar.Click
        Me.Close()
    End Sub
    Private Sub QuitarConcursoRepetidos(ByVal lstConcursos As ListaOrdenada(Of cJuegoSorteo))
        Dim concursos As cJuegoSorteo
        Dim concursoAux As cJuegoSorteo
        Dim encontrado As Boolean = False
        For Each concursos In lstConcursos
            If lsjuegoSorteosParametros Is Nothing Then
                If concursos.Seleccionada = 1 Then
                    lsjuegoSorteosParametros = New ListaOrdenada(Of cJuegoSorteo)
                    lsjuegoSorteosParametros.Add(concursos)
                End If
            Else
                For Each concursoAux In lsjuegoSorteosParametros
                    If concursoAux.IdPgmConcurso = concursos.IdPgmConcurso Then
                        encontrado = True
                        Exit For
                    End If
                Next
                If Not encontrado And concursos.Seleccionada = 1 Then
                    lsjuegoSorteosParametros.Add(concursos)
                Else
                    encontrado = False
                End If
            End If
        Next

    End Sub
    Private Sub ImprimirParametros(ByVal pIdpgmconcurso As Long, Optional ByVal ConGanadores As String = "N")
        Dim PgmBO As New PgmConcursoBO
        Dim dt As DataTable
        Dim ds As New DataSet
        dt = PgmBO.ObtenerDatosExtraccionesCAB(pIdpgmconcurso)
        ds.Tables.Add(dt)

        dt = PgmBO.ObtenerDatosJuegos(pIdpgmconcurso)
        ds.Tables.Add(dt)

        Dim path_reporte As String = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & General.PathInformes '  "D:\VS2008\SorteosCas.Root\SorteosCAS\dev\libExtractos\INFORMES"
        Dim reporte As New Listado
        reporte.GenerarParametros(ds, path_reporte, ConGanadores)
    End Sub

    Private Sub BtnImprimir_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprimir.EnabledChanged
        If BtnImprimir.Enabled Then
            BtnImprimir.BackgroundImageLayout = ImageLayout.Stretch
            BtnImprimir.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            BtnImprimir.BackgroundImageLayout = ImageLayout.Stretch
            BtnImprimir.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub BtnImprimir_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles BtnImprimir.MouseDown
        BtnImprimir.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub BtnImprimir_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprimir.MouseHover
        BtnImprimir.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub BtnImprimir_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles BtnImprimir.MouseLeave
        If BtnImprimir.Enabled Then
            BtnImprimir.BackgroundImageLayout = ImageLayout.Stretch
            BtnImprimir.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            BtnImprimir.BackgroundImageLayout = ImageLayout.Stretch
            BtnImprimir.BackgroundImage = My.Resources.Imagenes.boton_off
        End If
    End Sub

    Private Sub btnCerrar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.EnabledChanged
        If btnCerrar.Enabled Then
            btnCerrar.BackgroundImageLayout = ImageLayout.Stretch
            btnCerrar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnCerrar.BackgroundImageLayout = ImageLayout.Stretch
            btnCerrar.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnCerrar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnCerrar.MouseDown
        btnCerrar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnCerrar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.MouseHover
        btnCerrar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnCerrar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnCerrar.MouseLeave
        If btnCerrar.Enabled Then
            btnCerrar.BackgroundImageLayout = ImageLayout.Stretch
            btnCerrar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnCerrar.BackgroundImageLayout = ImageLayout.Stretch
            btnCerrar.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub


End Class