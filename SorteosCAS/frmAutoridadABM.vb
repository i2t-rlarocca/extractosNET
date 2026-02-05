Imports sorteos.helpers
Imports libEntities.Entities
Imports sorteos.bussiness

Public Class frmAutoridadABM

#Region "VARIABLES"

#End Region

#Region "PROPIEDADES"
    Private _Modo As String  ' CI: concurso inicio
    Private _idJuego As Integer
    Private _idPgmSorteo As Integer
    Private _oAutoridad As Autoridad

    Public Property Modo() As String
        Get
            Return _Modo
        End Get
        Set(ByVal value As String)
            _Modo = value
        End Set
    End Property

    Public Property idJuego() As Integer
        Get
            Return _idJuego
        End Get
        Set(ByVal value As Integer)
            _idJuego = value
        End Set
    End Property

    Public Property idPgmSorteo() As Integer
        Get
            Return _idPgmSorteo
        End Get
        Set(ByVal value As Integer)
            _idPgmSorteo = value
        End Set
    End Property

    Public Property oAutoridad() As Autoridad
        Get
            Return _oAutoridad
        End Get
        Set(ByVal value As Autoridad)
            _oAutoridad = value
        End Set
    End Property
#End Region


#Region "FUNCIONES"
    Private Sub Inicializar_Controles()
        Dim boJuego As New JuegoBO
        Dim boCargo As New CargoBO

        cmbJuego.DataSource = boJuego.getJuego()
        cmbJuego.ValueMember = "idJuego"
        cmbJuego.DisplayMember = "jue_desc"
        cmbJuego.Refresh()

        CmbCargo.DataSource = boCargo.GetCargos
        CmbCargo.ValueMember = "idcargo"
        CmbCargo.DisplayMember = "cargo"
        CmbCargo.Refresh()
        setValores(Nothing)
        If Modo = "CI" Then
            cmbJuego.SelectedValue = idJuego
            cmbJuego.Enabled = False
            txtOrden.Text = 1
            Nuevo()
        End If

        Buscar()
    End Sub

    Private Sub Buscar()
        Dim boAutoridad As New AutoridadBO

        Try
            ' se realiza la búsqueda
            Dim lLista As ListaOrdenada(Of Autoridad) = boAutoridad.getAutoridad(Me.txtNombreApellido.Text, idJuego)

            dgvAutoridades.DataSource = lLista
            ' se configuran las columnas de la grilla

            With dgvAutoridades
                .Columns.Clear()
                .ScrollBars = ScrollBars.Both
                .EditMode = DataGridViewEditMode.EditProgrammatically
                .AllowUserToResizeColumns = False
                .AllowUserToResizeRows = False
                .AutoGenerateColumns = False

                .Columns.Add("0", "Código")
                .Columns(0).Width = 0
                .Columns(0).DataPropertyName = "idAutoridad"
                .Columns(0).Visible = False

                .Columns.Add("1", "Juego")
                .Columns(1).Width = 110
                .Columns(1).DataPropertyName = "juegoDesc"
                '.Columns(1).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                .Columns.Add("2", "Nombre")
                .Columns(2).Width = 160
                .Columns(2).DataPropertyName = "nombre"
                '.Columns(2).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                .Columns.Add("3", "Cargo")
                .Columns(3).Width = 190
                .Columns(3).DataPropertyName = "cargo"
                '.Columns(3).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill

                .Columns.Add("4", "Orden")
                .Columns(4).Width = 40
                .Columns(4).DataPropertyName = "orden"
                .Columns(4).AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            End With

        Catch ex As Exception
            MsgBox("Error al consultar las autoridad: " & ex.Message)
        End Try
    End Sub

    Sub setValores(ByVal oAut As Autoridad)
        If oAut Is Nothing Then
            Try
                Dim cargo As New Cargo
                cargo = CmbCargo.SelectedItem
                txtOrden.Text = cargo.Orden
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        Else
            oAutoridad = oAut

            cmbJuego.SelectedValue = oAutoridad.idJuego
            txtNombre.Text = oAutoridad.Nombre
            'txtCargo.Text = oAutoridad.cargo
            Dim i As Integer
            i = ObtenerIdCargo(oAutoridad.cargo)
            If i = 0 Then
                CmbCargo.SelectedValue = 1
            Else
                CmbCargo.SelectedValue = i
            End If

            txtOrden.Text = oAutoridad.Orden
        End If
    End Sub

    Private Sub Cerrar()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub Nuevo()
        oAutoridad = New Autoridad
        oAutoridad.idAutoridad = 0
        oAutoridad.idPgmSorteo = idPgmSorteo

        Limpiar()
    End Sub

    Private Sub Guardar()
        Dim boAutoridad As New AutoridadBO
        Dim _cargo As Cargo
        Dim msg As String

        Try
            oAutoridad.idJuego = cmbJuego.SelectedValue
            oAutoridad.Nombre = txtNombre.Text
            _cargo = CmbCargo.SelectedItem
            oAutoridad.cargo = UCase(_cargo.Cargo)  'UCase(txtCargo.Text)
            oAutoridad.Orden = IIf(Trim(txtOrden.Text) = "", 0, txtOrden.Text)

            If boAutoridad.valida(oAutoridad, msg) Then
                If boAutoridad.setAutoridad(oAutoridad) Then
                    Nuevo()
                    Buscar()
                End If
            Else
                MsgBox(msg)
            End If

        Catch ex As Exception
            MsgBox("Error al guardar los datos de la autoridad: " & ex.Message)
        End Try
    End Sub

    Private Sub Eliminar()
        Dim boAutoridad As New AutoridadBO
        Dim oAutoridad As New Autoridad
        Dim msg As String

        Try
            If Not dgvAutoridades.CurrentRow Is Nothing Then
                oAutoridad = dgvAutoridades.CurrentRow.DataBoundItem()
                If MsgBox("¿Está seguro que desea eliminar la autoridad?", MsgBoxStyle.YesNo) = Windows.Forms.DialogResult.Yes Then
                    boAutoridad.eliminarAutoridad(oAutoridad)
                    Nuevo()
                    Buscar()
                End If
            End If


            If boAutoridad.valida(_oAutoridad, msg) Then
                If (boAutoridad.setAutoridad(_oAutoridad)) Then
                    Me.dgvAutoridades.DataSource = boAutoridad.getAutoridad()
                End If
            End If

        Catch ex As Exception
            MsgBox("Error al eliminar los datos de la autoridad: " & ex.Message)
        End Try

    End Sub

    Private Sub Limpiar()
        cmbJuego.SelectedValue = idJuego
        txtNombre.Text = ""
        CmbCargo.SelectedValue = 1
        'txtCargo.Text = ""
        'txtOrden.Text = ""
        txtNombre.Focus()
    End Sub

    Private Sub Seleccionar()
        Dim boAutoridad As New AutoridadBO
        Dim lst As New ListaOrdenada(Of Autoridad)
        Dim oAutoridad As New Autoridad
        Try
            If Not dgvAutoridades.CurrentRow Is Nothing Then
                ' valida que no exista una autoridad con ese mismo orden para el juego
                lst = boAutoridad.getAutoridad_PgmSorteo(idPgmSorteo)
                For Each aut In lst

                    If aut.Orden = Me.txtOrden.Text Then
                        MsgBox("Ya existe una autoridad para este cargo en el sorteo actual. Quite primero la anterior o realice una nueva selección.")
                        Exit Sub
                    End If
                Next

                oAutoridad = dgvAutoridades.CurrentRow.DataBoundItem()
                oAutoridad.idPgmSorteo = idPgmSorteo
                oAutoridad.Orden = txtOrden.Text

                boAutoridad.setAutoridad_PgmSorteo(oAutoridad)

                Me.Cerrar()
            End If

        Catch e As Exception
            MsgBox("Error al intentar seleccionar la autoridad: " & e.Message)
        End Try
    End Sub
#End Region


#Region "EVENTOS"
    Private Sub btnBuscar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBuscar.Click
        Buscar()
    End Sub

    Private Sub dgvAutoridades_CellClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAutoridades.CellClick
        setValores(dgvAutoridades.CurrentRow.DataBoundItem)
    End Sub

    Private Sub dgvAutoridades_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvAutoridades.CellContentDoubleClick
        Seleccionar()
    End Sub

    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        Seleccionar()
    End Sub

    Private Sub btnNuevo_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNuevo.Click
        Nuevo()
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEliminar.Click
        Eliminar()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnGuardar.Click
        Guardar()
    End Sub

    Private Sub frmAutoridadABM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Inicializar_Controles()
    End Sub
#End Region

   
    Private Sub txtNombreApellido_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNombreApellido.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            btnBuscar_Click(sender, e)
        Else
            General.SoloLetras(sender, e)
        End If
    End Sub

    Private Sub txtOrden_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtOrden.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            btnGuardar.Focus()
        Else
            General.SoloNumeros(sender, e)
        End If
    End Sub

    Private Sub txtNombre_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtNombre.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            CmbCargo.Focus()
        Else
            General.SoloLetras(sender, e)
        End If
    End Sub

    
   
    Private Sub txtCargo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            txtOrden.Focus()
        Else
            General.SoloLetras(sender, e)
        End If
    End Sub

    Private Sub CmbCargo_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles CmbCargo.SelectedIndexChanged
        Try
            Dim cargo As New Cargo
            cargo = CmbCargo.SelectedItem
            Me.txtOrden.Text = cargo.Orden
            If Me.btnGuardar.Enabled Then Me.btnGuardar.Focus()
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub
    Private Function ObtenerIdCargo(ByVal cargo As String) As Integer
        Try
            Dim ocargo As Cargo
            Dim _indice As Integer = 0
            If CmbCargo.Items.Count > 0 Then
                For Each ocargo In CmbCargo.Items
                    If UCase(ocargo.Cargo) = UCase(cargo) Then
                        _indice = ocargo.idCargo
                        Return _indice
                        Exit For
                    End If
                Next
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Function
End Class

