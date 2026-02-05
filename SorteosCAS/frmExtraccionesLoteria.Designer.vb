<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmExtraccionesLoteria
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmExtraccionesLoteria))
        Me.DTPFechaConcurso = New System.Windows.Forms.DateTimePicker
        Me.DTPHoraConcurso = New System.Windows.Forms.DateTimePicker
        Me.lblHora = New System.Windows.Forms.Label
        Me.lblFecha = New System.Windows.Forms.Label
        Me.CboConcurso = New System.Windows.Forms.ComboBox
        Me.lblconcurso = New System.Windows.Forms.Label
        Me.GroupBoxExtracciones = New System.Windows.Forms.GroupBox
        Me.btnSalir = New System.Windows.Forms.Button
        Me.GroupBoxIngreso = New System.Windows.Forms.GroupBox
        Me.btnRevertirExtraccion = New System.Windows.Forms.Button
        Me.btmConfirmar = New System.Windows.Forms.Button
        Me.cboMetodoIngreso = New System.Windows.Forms.ComboBox
        Me.lblmetodoingreso = New System.Windows.Forms.Label
        Me.GpbConfirmarExtraccion = New System.Windows.Forms.GroupBox
        Me.lblLetras = New System.Windows.Forms.Label
        Me.TxtLetra4 = New System.Windows.Forms.TextBox
        Me.TxtLetra3 = New System.Windows.Forms.TextBox
        Me.TxtLetra2 = New System.Windows.Forms.TextBox
        Me.TxtLetra1 = New System.Windows.Forms.TextBox
        Me.DTPHoraFinextraccion = New System.Windows.Forms.DateTimePicker
        Me.lblHorafin = New System.Windows.Forms.Label
        Me.DTPHoraInicioextraccion = New System.Windows.Forms.DateTimePicker
        Me.lblhorainicio = New System.Windows.Forms.Label
        Me.gpbIngresoDatos = New System.Windows.Forms.GroupBox
        Me.btnPorArchivo = New System.Windows.Forms.Button
        Me.lblteclados = New System.Windows.Forms.Label
        Me.btnSonido = New System.Windows.Forms.Button
        Me.CboPuertos = New System.Windows.Forms.ComboBox
        Me.BtnConectar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btmModificar = New System.Windows.Forms.Button
        Me.txtExtractoHasta = New System.Windows.Forms.TextBox
        Me.lblDe = New System.Windows.Forms.Label
        Me.txtordenExtracto = New System.Windows.Forms.TextBox
        Me.lblExtracto = New System.Windows.Forms.Label
        Me.lblingreso2 = New System.Windows.Forms.Label
        Me.lblingreso1 = New System.Windows.Forms.Label
        Me.txtValorExtraccion2 = New System.Windows.Forms.TextBox
        Me.txtValor1Extraccion = New System.Windows.Forms.TextBox
        Me.lblValor = New System.Windows.Forms.Label
        Me.TabExtracciones = New System.Windows.Forms.TabControl
        Me.BtnQuitar = New System.Windows.Forms.Button
        Me.btnAgregar = New System.Windows.Forms.Button
        Me.cboJurisdiccion = New System.Windows.Forms.ComboBox
        Me.lbljurisdicciones = New System.Windows.Forms.Label
        Me.txtNroSorteo = New System.Windows.Forms.TextBox
        Me.lblSorteo = New System.Windows.Forms.Label
        Me.txtJuegoPrincipal = New System.Windows.Forms.TextBox
        Me.lbljuego = New System.Windows.Forms.Label
        Me.txtNroSorteoJurisdiccion = New System.Windows.Forms.TextBox
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BtnActualizarNro = New System.Windows.Forms.Button
        Me.lblCifras = New System.Windows.Forms.Label
        Me.cboCantCifras = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.OpenFileD = New System.Windows.Forms.OpenFileDialog
        Me.GroupBoxExtracciones.SuspendLayout()
        Me.GroupBoxIngreso.SuspendLayout()
        Me.GpbConfirmarExtraccion.SuspendLayout()
        Me.gpbIngresoDatos.SuspendLayout()
        Me.SuspendLayout()
        '
        'DTPFechaConcurso
        '
        Me.DTPFechaConcurso.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.DTPFechaConcurso.CalendarMonthBackground = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.DTPFechaConcurso.Enabled = False
        Me.DTPFechaConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPFechaConcurso.Location = New System.Drawing.Point(495, 7)
        Me.DTPFechaConcurso.Name = "DTPFechaConcurso"
        Me.DTPFechaConcurso.ShowUpDown = True
        Me.DTPFechaConcurso.Size = New System.Drawing.Size(272, 24)
        Me.DTPFechaConcurso.TabIndex = 3
        '
        'DTPHoraConcurso
        '
        Me.DTPHoraConcurso.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.DTPHoraConcurso.CustomFormat = "HH:mm"
        Me.DTPHoraConcurso.Enabled = False
        Me.DTPHoraConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPHoraConcurso.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPHoraConcurso.Location = New System.Drawing.Point(904, 9)
        Me.DTPHoraConcurso.Name = "DTPHoraConcurso"
        Me.DTPHoraConcurso.ShowUpDown = True
        Me.DTPHoraConcurso.Size = New System.Drawing.Size(61, 24)
        Me.DTPHoraConcurso.TabIndex = 20
        '
        'lblHora
        '
        Me.lblHora.AutoSize = True
        Me.lblHora.BackColor = System.Drawing.Color.Transparent
        Me.lblHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHora.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblHora.Location = New System.Drawing.Point(849, 11)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(50, 18)
        Me.lblHora.TabIndex = 19
        Me.lblHora.Text = "Hora:"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.BackColor = System.Drawing.Color.Transparent
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblFecha.Location = New System.Drawing.Point(432, 11)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(59, 18)
        Me.lblFecha.TabIndex = 2
        Me.lblFecha.Text = "Fecha:"
        '
        'CboConcurso
        '
        Me.CboConcurso.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.CboConcurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboConcurso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.CboConcurso.FormattingEnabled = True
        Me.CboConcurso.Location = New System.Drawing.Point(92, 7)
        Me.CboConcurso.Name = "CboConcurso"
        Me.CboConcurso.Size = New System.Drawing.Size(322, 26)
        Me.CboConcurso.TabIndex = 1
        '
        'lblconcurso
        '
        Me.lblconcurso.AutoSize = True
        Me.lblconcurso.BackColor = System.Drawing.Color.Transparent
        Me.lblconcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblconcurso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblconcurso.Location = New System.Drawing.Point(7, 11)
        Me.lblconcurso.Name = "lblconcurso"
        Me.lblconcurso.Size = New System.Drawing.Size(87, 18)
        Me.lblconcurso.TabIndex = 0
        Me.lblconcurso.Text = "Concurso:"
        '
        'GroupBoxExtracciones
        '
        Me.GroupBoxExtracciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxExtracciones.Controls.Add(Me.btnSalir)
        Me.GroupBoxExtracciones.Controls.Add(Me.GroupBoxIngreso)
        Me.GroupBoxExtracciones.Controls.Add(Me.TabExtracciones)
        Me.GroupBoxExtracciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxExtracciones.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.GroupBoxExtracciones.Location = New System.Drawing.Point(10, 118)
        Me.GroupBoxExtracciones.Name = "GroupBoxExtracciones"
        Me.GroupBoxExtracciones.Size = New System.Drawing.Size(993, 510)
        Me.GroupBoxExtracciones.TabIndex = 13
        Me.GroupBoxExtracciones.TabStop = False
        Me.GroupBoxExtracciones.Text = "Jurisdicciones"
        '
        'btnSalir
        '
        Me.btnSalir.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnSalir.BackColor = System.Drawing.SystemColors.Control
        Me.btnSalir.BackgroundImage = CType(resources.GetObject("btnSalir.BackgroundImage"), System.Drawing.Image)
        Me.btnSalir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnSalir.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSalir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnSalir.Location = New System.Drawing.Point(457, 478)
        Me.btnSalir.Name = "btnSalir"
        Me.btnSalir.Size = New System.Drawing.Size(79, 26)
        Me.btnSalir.TabIndex = 2
        Me.btnSalir.Text = "&SALIR"
        Me.ToolTip1.SetToolTip(Me.btnSalir, "Salir")
        Me.btnSalir.UseVisualStyleBackColor = False
        '
        'GroupBoxIngreso
        '
        Me.GroupBoxIngreso.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxIngreso.Controls.Add(Me.btnRevertirExtraccion)
        Me.GroupBoxIngreso.Controls.Add(Me.btmConfirmar)
        Me.GroupBoxIngreso.Controls.Add(Me.cboMetodoIngreso)
        Me.GroupBoxIngreso.Controls.Add(Me.lblmetodoingreso)
        Me.GroupBoxIngreso.Controls.Add(Me.GpbConfirmarExtraccion)
        Me.GroupBoxIngreso.Controls.Add(Me.gpbIngresoDatos)
        Me.GroupBoxIngreso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxIngreso.Location = New System.Drawing.Point(745, 16)
        Me.GroupBoxIngreso.Name = "GroupBoxIngreso"
        Me.GroupBoxIngreso.Size = New System.Drawing.Size(248, 409)
        Me.GroupBoxIngreso.TabIndex = 1
        Me.GroupBoxIngreso.TabStop = False
        '
        'btnRevertirExtraccion
        '
        Me.btnRevertirExtraccion.BackColor = System.Drawing.SystemColors.Control
        Me.btnRevertirExtraccion.BackgroundImage = CType(resources.GetObject("btnRevertirExtraccion.BackgroundImage"), System.Drawing.Image)
        Me.btnRevertirExtraccion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRevertirExtraccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRevertirExtraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRevertirExtraccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnRevertirExtraccion.Location = New System.Drawing.Point(6, 377)
        Me.btnRevertirExtraccion.Name = "btnRevertirExtraccion"
        Me.btnRevertirExtraccion.Size = New System.Drawing.Size(104, 26)
        Me.btnRevertirExtraccion.TabIndex = 11
        Me.btnRevertirExtraccion.Text = "&REVERTIR"
        Me.ToolTip1.SetToolTip(Me.btnRevertirExtraccion, "Revertir Extracción")
        Me.btnRevertirExtraccion.UseVisualStyleBackColor = False
        '
        'btmConfirmar
        '
        Me.btmConfirmar.BackColor = System.Drawing.SystemColors.Control
        Me.btmConfirmar.BackgroundImage = CType(resources.GetObject("btmConfirmar.BackgroundImage"), System.Drawing.Image)
        Me.btmConfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btmConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btmConfirmar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btmConfirmar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btmConfirmar.Location = New System.Drawing.Point(115, 377)
        Me.btmConfirmar.Name = "btmConfirmar"
        Me.btmConfirmar.Size = New System.Drawing.Size(121, 26)
        Me.btmConfirmar.TabIndex = 12
        Me.btmConfirmar.Text = "C&ONFIRMAR"
        Me.ToolTip1.SetToolTip(Me.btmConfirmar, "Confirmar Extracción")
        Me.btmConfirmar.UseVisualStyleBackColor = False
        '
        'cboMetodoIngreso
        '
        Me.cboMetodoIngreso.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cboMetodoIngreso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMetodoIngreso.FormattingEnabled = True
        Me.cboMetodoIngreso.Location = New System.Drawing.Point(16, 37)
        Me.cboMetodoIngreso.Name = "cboMetodoIngreso"
        Me.cboMetodoIngreso.Size = New System.Drawing.Size(205, 26)
        Me.cboMetodoIngreso.TabIndex = 1
        '
        'lblmetodoingreso
        '
        Me.lblmetodoingreso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmetodoingreso.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblmetodoingreso.Location = New System.Drawing.Point(11, 16)
        Me.lblmetodoingreso.Name = "lblmetodoingreso"
        Me.lblmetodoingreso.Size = New System.Drawing.Size(176, 18)
        Me.lblmetodoingreso.TabIndex = 0
        Me.lblmetodoingreso.Text = "Método de Ingreso:"
        '
        'GpbConfirmarExtraccion
        '
        Me.GpbConfirmarExtraccion.Controls.Add(Me.lblLetras)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.TxtLetra4)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.TxtLetra3)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.TxtLetra2)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.TxtLetra1)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.DTPHoraFinextraccion)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.lblHorafin)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.DTPHoraInicioextraccion)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.lblhorainicio)
        Me.GpbConfirmarExtraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpbConfirmarExtraccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.GpbConfirmarExtraccion.Location = New System.Drawing.Point(8, 254)
        Me.GpbConfirmarExtraccion.Name = "GpbConfirmarExtraccion"
        Me.GpbConfirmarExtraccion.Size = New System.Drawing.Size(217, 116)
        Me.GpbConfirmarExtraccion.TabIndex = 0
        Me.GpbConfirmarExtraccion.TabStop = False
        Me.GpbConfirmarExtraccion.Text = "Confirmar Extracción"
        '
        'lblLetras
        '
        Me.lblLetras.AutoSize = True
        Me.lblLetras.Enabled = False
        Me.lblLetras.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLetras.Location = New System.Drawing.Point(11, 25)
        Me.lblLetras.Name = "lblLetras"
        Me.lblLetras.Size = New System.Drawing.Size(83, 20)
        Me.lblLetras.TabIndex = 0
        Me.lblLetras.Text = "LETRAS:"
        '
        'TxtLetra4
        '
        Me.TxtLetra4.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtLetra4.Enabled = False
        Me.TxtLetra4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLetra4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.TxtLetra4.Location = New System.Drawing.Point(174, 23)
        Me.TxtLetra4.MaxLength = 1
        Me.TxtLetra4.Name = "TxtLetra4"
        Me.TxtLetra4.Size = New System.Drawing.Size(25, 24)
        Me.TxtLetra4.TabIndex = 4
        Me.TxtLetra4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtLetra3
        '
        Me.TxtLetra3.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtLetra3.Enabled = False
        Me.TxtLetra3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLetra3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.TxtLetra3.Location = New System.Drawing.Point(147, 23)
        Me.TxtLetra3.MaxLength = 1
        Me.TxtLetra3.Name = "TxtLetra3"
        Me.TxtLetra3.Size = New System.Drawing.Size(25, 24)
        Me.TxtLetra3.TabIndex = 3
        Me.TxtLetra3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtLetra2
        '
        Me.TxtLetra2.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtLetra2.Enabled = False
        Me.TxtLetra2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLetra2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.TxtLetra2.Location = New System.Drawing.Point(120, 23)
        Me.TxtLetra2.MaxLength = 1
        Me.TxtLetra2.Name = "TxtLetra2"
        Me.TxtLetra2.Size = New System.Drawing.Size(25, 24)
        Me.TxtLetra2.TabIndex = 2
        Me.TxtLetra2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'TxtLetra1
        '
        Me.TxtLetra1.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper
        Me.TxtLetra1.Enabled = False
        Me.TxtLetra1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtLetra1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.TxtLetra1.Location = New System.Drawing.Point(93, 23)
        Me.TxtLetra1.MaxLength = 1
        Me.TxtLetra1.Name = "TxtLetra1"
        Me.TxtLetra1.Size = New System.Drawing.Size(25, 24)
        Me.TxtLetra1.TabIndex = 1
        Me.TxtLetra1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'DTPHoraFinextraccion
        '
        Me.DTPHoraFinextraccion.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DTPHoraFinextraccion.CustomFormat = "HH:mm:00"
        Me.DTPHoraFinextraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPHoraFinextraccion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPHoraFinextraccion.Location = New System.Drawing.Point(135, 83)
        Me.DTPHoraFinextraccion.Name = "DTPHoraFinextraccion"
        Me.DTPHoraFinextraccion.ShowUpDown = True
        Me.DTPHoraFinextraccion.Size = New System.Drawing.Size(64, 24)
        Me.DTPHoraFinextraccion.TabIndex = 8
        '
        'lblHorafin
        '
        Me.lblHorafin.AutoSize = True
        Me.lblHorafin.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHorafin.Location = New System.Drawing.Point(28, 85)
        Me.lblHorafin.Name = "lblHorafin"
        Me.lblHorafin.Size = New System.Drawing.Size(99, 20)
        Me.lblHorafin.TabIndex = 7
        Me.lblHorafin.Text = "HORA FIN:"
        '
        'DTPHoraInicioextraccion
        '
        Me.DTPHoraInicioextraccion.CalendarForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.DTPHoraInicioextraccion.CustomFormat = "HH:mm:00"
        Me.DTPHoraInicioextraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPHoraInicioextraccion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPHoraInicioextraccion.Location = New System.Drawing.Point(135, 53)
        Me.DTPHoraInicioextraccion.Name = "DTPHoraInicioextraccion"
        Me.DTPHoraInicioextraccion.ShowUpDown = True
        Me.DTPHoraInicioextraccion.Size = New System.Drawing.Size(64, 24)
        Me.DTPHoraInicioextraccion.TabIndex = 6
        '
        'lblhorainicio
        '
        Me.lblhorainicio.AutoSize = True
        Me.lblhorainicio.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblhorainicio.Location = New System.Drawing.Point(6, 56)
        Me.lblhorainicio.Name = "lblhorainicio"
        Me.lblhorainicio.Size = New System.Drawing.Size(125, 20)
        Me.lblhorainicio.TabIndex = 5
        Me.lblhorainicio.Text = "HORA INICIO:"
        '
        'gpbIngresoDatos
        '
        Me.gpbIngresoDatos.Controls.Add(Me.btnPorArchivo)
        Me.gpbIngresoDatos.Controls.Add(Me.lblteclados)
        Me.gpbIngresoDatos.Controls.Add(Me.btnSonido)
        Me.gpbIngresoDatos.Controls.Add(Me.CboPuertos)
        Me.gpbIngresoDatos.Controls.Add(Me.BtnConectar)
        Me.gpbIngresoDatos.Controls.Add(Me.btnCancelar)
        Me.gpbIngresoDatos.Controls.Add(Me.btmModificar)
        Me.gpbIngresoDatos.Controls.Add(Me.txtExtractoHasta)
        Me.gpbIngresoDatos.Controls.Add(Me.lblDe)
        Me.gpbIngresoDatos.Controls.Add(Me.txtordenExtracto)
        Me.gpbIngresoDatos.Controls.Add(Me.lblExtracto)
        Me.gpbIngresoDatos.Controls.Add(Me.lblingreso2)
        Me.gpbIngresoDatos.Controls.Add(Me.lblingreso1)
        Me.gpbIngresoDatos.Controls.Add(Me.txtValorExtraccion2)
        Me.gpbIngresoDatos.Controls.Add(Me.txtValor1Extraccion)
        Me.gpbIngresoDatos.Controls.Add(Me.lblValor)
        Me.gpbIngresoDatos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpbIngresoDatos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.gpbIngresoDatos.Location = New System.Drawing.Point(8, 69)
        Me.gpbIngresoDatos.Name = "gpbIngresoDatos"
        Me.gpbIngresoDatos.Size = New System.Drawing.Size(234, 180)
        Me.gpbIngresoDatos.TabIndex = 3
        Me.gpbIngresoDatos.TabStop = False
        Me.gpbIngresoDatos.Text = "Ingreso de Datos"
        '
        'btnPorArchivo
        '
        Me.btnPorArchivo.BackColor = System.Drawing.SystemColors.Control
        Me.btnPorArchivo.BackgroundImage = CType(resources.GetObject("btnPorArchivo.BackgroundImage"), System.Drawing.Image)
        Me.btnPorArchivo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnPorArchivo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnPorArchivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnPorArchivo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnPorArchivo.Location = New System.Drawing.Point(-2, 49)
        Me.btnPorArchivo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPorArchivo.Name = "btnPorArchivo"
        Me.btnPorArchivo.Size = New System.Drawing.Size(239, 26)
        Me.btnPorArchivo.TabIndex = 71
        Me.btnPorArchivo.Text = "&OBTENER EXTRACCIONES"
        Me.ToolTip1.SetToolTip(Me.btnPorArchivo, "Modifica un extracto(F1)")
        Me.btnPorArchivo.UseVisualStyleBackColor = False
        '
        'lblteclados
        '
        Me.lblteclados.AutoSize = True
        Me.lblteclados.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblteclados.Location = New System.Drawing.Point(5, 162)
        Me.lblteclados.Name = "lblteclados"
        Me.lblteclados.Size = New System.Drawing.Size(142, 15)
        Me.lblteclados.TabIndex = 13
        Me.lblteclados.Text = "Teclados Deshabilitados"
        Me.lblteclados.Visible = False
        '
        'btnSonido
        '
        Me.btnSonido.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSonido.Location = New System.Drawing.Point(164, 53)
        Me.btnSonido.Name = "btnSonido"
        Me.btnSonido.Size = New System.Drawing.Size(29, 25)
        Me.btnSonido.TabIndex = 7
        Me.ToolTip1.SetToolTip(Me.btnSonido, "Habilitar/Deshabilitar Sonido")
        Me.btnSonido.UseVisualStyleBackColor = True
        '
        'CboPuertos
        '
        Me.CboPuertos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboPuertos.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboPuertos.FormattingEnabled = True
        Me.CboPuertos.Location = New System.Drawing.Point(11, 54)
        Me.CboPuertos.Name = "CboPuertos"
        Me.CboPuertos.Size = New System.Drawing.Size(89, 28)
        Me.CboPuertos.TabIndex = 4
        Me.CboPuertos.Visible = False
        '
        'BtnConectar
        '
        Me.BtnConectar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConectar.Location = New System.Drawing.Point(120, 53)
        Me.BtnConectar.Name = "BtnConectar"
        Me.BtnConectar.Size = New System.Drawing.Size(29, 25)
        Me.BtnConectar.TabIndex = 6
        Me.ToolTip1.SetToolTip(Me.BtnConectar, "Conectar a dispositivo")
        Me.BtnConectar.UseVisualStyleBackColor = True
        Me.BtnConectar.Visible = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.BackgroundImage = CType(resources.GetObject("btnCancelar.BackgroundImage"), System.Drawing.Image)
        Me.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelar.Enabled = False
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(5, 136)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(108, 26)
        Me.btnCancelar.TabIndex = 3
        Me.btnCancelar.Text = "&CANCELAR"
        Me.ToolTip1.SetToolTip(Me.btnCancelar, "Cancelar(F3)")
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btmModificar
        '
        Me.btmModificar.BackColor = System.Drawing.SystemColors.Control
        Me.btmModificar.BackgroundImage = CType(resources.GetObject("btmModificar.BackgroundImage"), System.Drawing.Image)
        Me.btmModificar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btmModificar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btmModificar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btmModificar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btmModificar.Location = New System.Drawing.Point(118, 136)
        Me.btmModificar.Name = "btmModificar"
        Me.btmModificar.Size = New System.Drawing.Size(110, 26)
        Me.btmModificar.TabIndex = 4
        Me.btmModificar.Text = "&MODIFICAR "
        Me.ToolTip1.SetToolTip(Me.btmModificar, "Modificar Extracción(F1)")
        Me.btmModificar.UseVisualStyleBackColor = False
        '
        'txtExtractoHasta
        '
        Me.txtExtractoHasta.Enabled = False
        Me.txtExtractoHasta.Location = New System.Drawing.Point(189, 23)
        Me.txtExtractoHasta.Name = "txtExtractoHasta"
        Me.txtExtractoHasta.Size = New System.Drawing.Size(33, 24)
        Me.txtExtractoHasta.TabIndex = 3
        Me.txtExtractoHasta.Text = "20"
        Me.txtExtractoHasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDe
        '
        Me.lblDe.Location = New System.Drawing.Point(152, 27)
        Me.lblDe.Name = "lblDe"
        Me.lblDe.Size = New System.Drawing.Size(33, 18)
        Me.lblDe.TabIndex = 2
        Me.lblDe.Text = "DE "
        '
        'txtordenExtracto
        '
        Me.txtordenExtracto.Enabled = False
        Me.txtordenExtracto.Location = New System.Drawing.Point(118, 23)
        Me.txtordenExtracto.Name = "txtordenExtracto"
        Me.txtordenExtracto.Size = New System.Drawing.Size(33, 24)
        Me.txtordenExtracto.TabIndex = 1
        Me.txtordenExtracto.Text = "1"
        Me.txtordenExtracto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblExtracto
        '
        Me.lblExtracto.Location = New System.Drawing.Point(2, 27)
        Me.lblExtracto.Name = "lblExtracto"
        Me.lblExtracto.Size = New System.Drawing.Size(124, 18)
        Me.lblExtracto.TabIndex = 0
        Me.lblExtracto.Text = "Orden de salida"
        '
        'lblingreso2
        '
        Me.lblingreso2.AutoSize = True
        Me.lblingreso2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblingreso2.Location = New System.Drawing.Point(4, 113)
        Me.lblingreso2.Name = "lblingreso2"
        Me.lblingreso2.Size = New System.Drawing.Size(93, 18)
        Me.lblingreso2.TabIndex = 9
        Me.lblingreso2.Text = "INGRESO 2:"
        '
        'lblingreso1
        '
        Me.lblingreso1.AutoSize = True
        Me.lblingreso1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblingreso1.Location = New System.Drawing.Point(5, 83)
        Me.lblingreso1.Name = "lblingreso1"
        Me.lblingreso1.Size = New System.Drawing.Size(93, 18)
        Me.lblingreso1.TabIndex = 57
        Me.lblingreso1.Text = "INGRESO 1:"
        '
        'txtValorExtraccion2
        '
        Me.txtValorExtraccion2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorExtraccion2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtValorExtraccion2.Location = New System.Drawing.Point(104, 106)
        Me.txtValorExtraccion2.Name = "txtValorExtraccion2"
        Me.txtValorExtraccion2.Size = New System.Drawing.Size(73, 24)
        Me.txtValorExtraccion2.TabIndex = 2
        Me.txtValorExtraccion2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValor1Extraccion
        '
        Me.txtValor1Extraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValor1Extraccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtValor1Extraccion.Location = New System.Drawing.Point(104, 76)
        Me.txtValor1Extraccion.Name = "txtValor1Extraccion"
        Me.txtValor1Extraccion.Size = New System.Drawing.Size(73, 24)
        Me.txtValor1Extraccion.TabIndex = 1
        Me.txtValor1Extraccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblValor
        '
        Me.lblValor.AutoSize = True
        Me.lblValor.Location = New System.Drawing.Point(101, 50)
        Me.lblValor.Name = "lblValor"
        Me.lblValor.Size = New System.Drawing.Size(82, 18)
        Me.lblValor.TabIndex = 5
        Me.lblValor.Text = "NUMERO"
        '
        'TabExtracciones
        '
        Me.TabExtracciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabExtracciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabExtracciones.Location = New System.Drawing.Point(6, 16)
        Me.TabExtracciones.Name = "TabExtracciones"
        Me.TabExtracciones.SelectedIndex = 0
        Me.TabExtracciones.Size = New System.Drawing.Size(733, 455)
        Me.TabExtracciones.TabIndex = 0
        '
        'BtnQuitar
        '
        Me.BtnQuitar.BackColor = System.Drawing.SystemColors.Control
        Me.BtnQuitar.BackgroundImage = CType(resources.GetObject("BtnQuitar.BackgroundImage"), System.Drawing.Image)
        Me.BtnQuitar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnQuitar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnQuitar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnQuitar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.BtnQuitar.Location = New System.Drawing.Point(699, 75)
        Me.BtnQuitar.Name = "BtnQuitar"
        Me.BtnQuitar.Size = New System.Drawing.Size(91, 26)
        Me.BtnQuitar.TabIndex = 8
        Me.BtnQuitar.Text = "&QUITAR"
        Me.ToolTip1.SetToolTip(Me.BtnQuitar, "Quitar Jurisdicción")
        Me.BtnQuitar.UseVisualStyleBackColor = False
        Me.BtnQuitar.Visible = False
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.SystemColors.Control
        Me.btnAgregar.BackgroundImage = CType(resources.GetObject("btnAgregar.BackgroundImage"), System.Drawing.Image)
        Me.btnAgregar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAgregar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAgregar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAgregar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnAgregar.Location = New System.Drawing.Point(796, 75)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.Size = New System.Drawing.Size(103, 26)
        Me.btnAgregar.TabIndex = 7
        Me.btnAgregar.Text = "&AGREGAR"
        Me.ToolTip1.SetToolTip(Me.btnAgregar, "Agregar Jurisdicción")
        Me.btnAgregar.UseVisualStyleBackColor = False
        Me.btnAgregar.Visible = False
        '
        'cboJurisdiccion
        '
        Me.cboJurisdiccion.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cboJurisdiccion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple
        Me.cboJurisdiccion.Enabled = False
        Me.cboJurisdiccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboJurisdiccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.cboJurisdiccion.FormattingEnabled = True
        Me.cboJurisdiccion.Location = New System.Drawing.Point(117, 44)
        Me.cboJurisdiccion.Name = "cboJurisdiccion"
        Me.cboJurisdiccion.Size = New System.Drawing.Size(412, 25)
        Me.cboJurisdiccion.TabIndex = 5
        '
        'lbljurisdicciones
        '
        Me.lbljurisdicciones.AutoSize = True
        Me.lbljurisdicciones.BackColor = System.Drawing.Color.Transparent
        Me.lbljurisdicciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbljurisdicciones.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lbljurisdicciones.Location = New System.Drawing.Point(7, 52)
        Me.lbljurisdicciones.Name = "lbljurisdicciones"
        Me.lbljurisdicciones.Size = New System.Drawing.Size(104, 18)
        Me.lbljurisdicciones.TabIndex = 4
        Me.lbljurisdicciones.Text = "Jurisdicción:"
        '
        'txtNroSorteo
        '
        Me.txtNroSorteo.Enabled = False
        Me.txtNroSorteo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroSorteo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtNroSorteo.Location = New System.Drawing.Point(455, 77)
        Me.txtNroSorteo.Name = "txtNroSorteo"
        Me.txtNroSorteo.Size = New System.Drawing.Size(62, 24)
        Me.txtNroSorteo.TabIndex = 12
        Me.txtNroSorteo.Text = "8365"
        Me.txtNroSorteo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblSorteo
        '
        Me.lblSorteo.AutoSize = True
        Me.lblSorteo.BackColor = System.Drawing.Color.Transparent
        Me.lblSorteo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblSorteo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblSorteo.Location = New System.Drawing.Point(391, 80)
        Me.lblSorteo.Name = "lblSorteo"
        Me.lblSorteo.Size = New System.Drawing.Size(64, 18)
        Me.lblSorteo.TabIndex = 11
        Me.lblSorteo.Text = "Sorteo:"
        '
        'txtJuegoPrincipal
        '
        Me.txtJuegoPrincipal.Enabled = False
        Me.txtJuegoPrincipal.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJuegoPrincipal.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtJuegoPrincipal.Location = New System.Drawing.Point(116, 77)
        Me.txtJuegoPrincipal.Name = "txtJuegoPrincipal"
        Me.txtJuegoPrincipal.Size = New System.Drawing.Size(265, 24)
        Me.txtJuegoPrincipal.TabIndex = 10
        Me.txtJuegoPrincipal.Text = "QUINIELA NOCTURNA"
        '
        'lbljuego
        '
        Me.lbljuego.AutoSize = True
        Me.lbljuego.BackColor = System.Drawing.Color.Transparent
        Me.lbljuego.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbljuego.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lbljuego.Location = New System.Drawing.Point(52, 80)
        Me.lbljuego.Name = "lbljuego"
        Me.lbljuego.Size = New System.Drawing.Size(59, 18)
        Me.lbljuego.TabIndex = 9
        Me.lbljuego.Text = "Juego:"
        '
        'txtNroSorteoJurisdiccion
        '
        Me.txtNroSorteoJurisdiccion.Enabled = False
        Me.txtNroSorteoJurisdiccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroSorteoJurisdiccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtNroSorteoJurisdiccion.Location = New System.Drawing.Point(605, 45)
        Me.txtNroSorteoJurisdiccion.MaxLength = 5
        Me.txtNroSorteoJurisdiccion.Name = "txtNroSorteoJurisdiccion"
        Me.txtNroSorteoJurisdiccion.Size = New System.Drawing.Size(75, 24)
        Me.txtNroSorteoJurisdiccion.TabIndex = 6
        Me.txtNroSorteoJurisdiccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'BtnActualizarNro
        '
        Me.BtnActualizarNro.BackColor = System.Drawing.SystemColors.Control
        Me.BtnActualizarNro.BackgroundImage = CType(resources.GetObject("BtnActualizarNro.BackgroundImage"), System.Drawing.Image)
        Me.BtnActualizarNro.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnActualizarNro.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnActualizarNro.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnActualizarNro.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.BtnActualizarNro.Location = New System.Drawing.Point(699, 45)
        Me.BtnActualizarNro.Name = "BtnActualizarNro"
        Me.BtnActualizarNro.Size = New System.Drawing.Size(118, 26)
        Me.BtnActualizarNro.TabIndex = 24
        Me.BtnActualizarNro.Text = "&ACTUALIZAR"
        Me.ToolTip1.SetToolTip(Me.BtnActualizarNro, "Agregar Jurisdicción")
        Me.BtnActualizarNro.UseVisualStyleBackColor = False
        '
        'lblCifras
        '
        Me.lblCifras.AutoSize = True
        Me.lblCifras.BackColor = System.Drawing.Color.Transparent
        Me.lblCifras.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCifras.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblCifras.Location = New System.Drawing.Point(541, 81)
        Me.lblCifras.Name = "lblCifras"
        Me.lblCifras.Size = New System.Drawing.Size(58, 18)
        Me.lblCifras.TabIndex = 21
        Me.lblCifras.Text = "Cifras:"
        '
        'cboCantCifras
        '
        Me.cboCantCifras.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCantCifras.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCantCifras.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.cboCantCifras.FormattingEnabled = True
        Me.cboCantCifras.Location = New System.Drawing.Point(605, 80)
        Me.cboCantCifras.Name = "cboCantCifras"
        Me.cboCantCifras.Size = New System.Drawing.Size(75, 21)
        Me.cboCantCifras.TabIndex = 23
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(535, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(64, 18)
        Me.Label1.TabIndex = 25
        Me.Label1.Text = "Sorteo:"
        '
        'frmExtraccionesLoteria
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.fondoExtraccionesLoterias
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1006, 633)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.BtnActualizarNro)
        Me.Controls.Add(Me.cboCantCifras)
        Me.Controls.Add(Me.lblCifras)
        Me.Controls.Add(Me.txtNroSorteoJurisdiccion)
        Me.Controls.Add(Me.BtnQuitar)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.cboJurisdiccion)
        Me.Controls.Add(Me.lbljurisdicciones)
        Me.Controls.Add(Me.txtNroSorteo)
        Me.Controls.Add(Me.lblSorteo)
        Me.Controls.Add(Me.txtJuegoPrincipal)
        Me.Controls.Add(Me.lbljuego)
        Me.Controls.Add(Me.GroupBoxExtracciones)
        Me.Controls.Add(Me.DTPFechaConcurso)
        Me.Controls.Add(Me.DTPHoraConcurso)
        Me.Controls.Add(Me.lblHora)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.CboConcurso)
        Me.Controls.Add(Me.lblconcurso)
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Name = "frmExtraccionesLoteria"
        Me.Text = "Extracciones QNL de otras jurisdicciones"
        Me.GroupBoxExtracciones.ResumeLayout(False)
        Me.GroupBoxIngreso.ResumeLayout(False)
        Me.GpbConfirmarExtraccion.ResumeLayout(False)
        Me.GpbConfirmarExtraccion.PerformLayout()
        Me.gpbIngresoDatos.ResumeLayout(False)
        Me.gpbIngresoDatos.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents DTPFechaConcurso As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPHoraConcurso As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHora As System.Windows.Forms.Label
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents CboConcurso As System.Windows.Forms.ComboBox
    Friend WithEvents lblconcurso As System.Windows.Forms.Label
    Friend WithEvents GroupBoxExtracciones As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxIngreso As System.Windows.Forms.GroupBox
    Friend WithEvents cboMetodoIngreso As System.Windows.Forms.ComboBox
    Friend WithEvents lblmetodoingreso As System.Windows.Forms.Label
    Friend WithEvents GpbConfirmarExtraccion As System.Windows.Forms.GroupBox
    Friend WithEvents DTPHoraFinextraccion As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHorafin As System.Windows.Forms.Label
    Friend WithEvents DTPHoraInicioextraccion As System.Windows.Forms.DateTimePicker
    Friend WithEvents gpbIngresoDatos As System.Windows.Forms.GroupBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btmModificar As System.Windows.Forms.Button
    Friend WithEvents txtExtractoHasta As System.Windows.Forms.TextBox
    Friend WithEvents lblDe As System.Windows.Forms.Label
    Friend WithEvents txtordenExtracto As System.Windows.Forms.TextBox
    Friend WithEvents lblExtracto As System.Windows.Forms.Label
    Friend WithEvents lblingreso2 As System.Windows.Forms.Label
    Friend WithEvents lblingreso1 As System.Windows.Forms.Label
    Friend WithEvents txtValorExtraccion2 As System.Windows.Forms.TextBox
    Friend WithEvents txtValor1Extraccion As System.Windows.Forms.TextBox
    Friend WithEvents lblValor As System.Windows.Forms.Label
    Friend WithEvents TabExtracciones As System.Windows.Forms.TabControl
    Friend WithEvents BtnQuitar As System.Windows.Forms.Button
    Friend WithEvents btnAgregar As System.Windows.Forms.Button
    Friend WithEvents cboJurisdiccion As System.Windows.Forms.ComboBox
    Friend WithEvents lbljurisdicciones As System.Windows.Forms.Label
    Friend WithEvents txtNroSorteo As System.Windows.Forms.TextBox
    Friend WithEvents lblSorteo As System.Windows.Forms.Label
    Friend WithEvents txtJuegoPrincipal As System.Windows.Forms.TextBox
    Friend WithEvents lbljuego As System.Windows.Forms.Label
    Friend WithEvents txtNroSorteoJurisdiccion As System.Windows.Forms.TextBox
    Friend WithEvents lblLetras As System.Windows.Forms.Label
    Friend WithEvents TxtLetra4 As System.Windows.Forms.TextBox
    Friend WithEvents TxtLetra3 As System.Windows.Forms.TextBox
    Friend WithEvents TxtLetra2 As System.Windows.Forms.TextBox
    Friend WithEvents TxtLetra1 As System.Windows.Forms.TextBox
    Friend WithEvents lblhorainicio As System.Windows.Forms.Label
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents btnSalir As System.Windows.Forms.Button
    Friend WithEvents CboPuertos As System.Windows.Forms.ComboBox
    Friend WithEvents BtnConectar As System.Windows.Forms.Button
    Friend WithEvents btnSonido As System.Windows.Forms.Button
    Friend WithEvents lblteclados As System.Windows.Forms.Label
    Friend WithEvents lblCifras As System.Windows.Forms.Label
    Friend WithEvents cboCantCifras As System.Windows.Forms.ComboBox
    Friend WithEvents btnRevertirExtraccion As System.Windows.Forms.Button
    Friend WithEvents btmConfirmar As System.Windows.Forms.Button
    Friend WithEvents BtnActualizarNro As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnPorArchivo As System.Windows.Forms.Button
    Friend WithEvents OpenFileD As System.Windows.Forms.OpenFileDialog
End Class
