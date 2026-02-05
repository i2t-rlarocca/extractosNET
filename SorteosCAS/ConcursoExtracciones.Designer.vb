<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConcursoExtracciones
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(ConcursoExtracciones))
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.cboIraExtraccion = New System.Windows.Forms.ComboBox
        Me.btnextraccionSiguiente = New System.Windows.Forms.Button
        Me.btnExtraccionAnterior = New System.Windows.Forms.Button
        Me.GroupBoxIngreso = New System.Windows.Forms.GroupBox
        Me.cboMetodoIngreso = New System.Windows.Forms.ComboBox
        Me.lblmetodoingreso = New System.Windows.Forms.Label
        Me.GpbConfirmarExtraccion = New System.Windows.Forms.GroupBox
        Me.btnRevertirExtraccion = New System.Windows.Forms.Button
        Me.btmConfirmar = New System.Windows.Forms.Button
        Me.DTPHoraFinextraccion = New System.Windows.Forms.DateTimePicker
        Me.Label3 = New System.Windows.Forms.Label
        Me.DTPHoraInicioextraccion = New System.Windows.Forms.DateTimePicker
        Me.Label1 = New System.Windows.Forms.Label
        Me.gpbIngresoDatos = New System.Windows.Forms.GroupBox
        Me.btnPorArchivo = New System.Windows.Forms.Button
        Me.btnSonido = New System.Windows.Forms.Button
        Me.LblTeclados = New System.Windows.Forms.Label
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
        Me.txtPosEnExtracto2 = New System.Windows.Forms.TextBox
        Me.txtValor1Extraccion = New System.Windows.Forms.TextBox
        Me.txtPosEnExtracto1 = New System.Windows.Forms.TextBox
        Me.lblValor = New System.Windows.Forms.Label
        Me.lblPosicion = New System.Windows.Forms.Label
        Me.TabExtracciones = New System.Windows.Forms.TabControl
        Me.txtModeloExtracciones = New System.Windows.Forms.TextBox
        Me.lblModelo = New System.Windows.Forms.Label
        Me.CboConcurso = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.DTPFechaConcurso = New System.Windows.Forms.DateTimePicker
        Me.DTPHoraConcurso = New System.Windows.Forms.DateTimePicker
        Me.lblHora = New System.Windows.Forms.Label
        Me.lblFecha = New System.Windows.Forms.Label
        Me.BackgroundWorker1 = New System.ComponentModel.BackgroundWorker
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.BTNSALIR = New System.Windows.Forms.Button
        Me.btnExtra = New System.Windows.Forms.Button
        Me.btnRevertir = New System.Windows.Forms.Button
        Me.btnListarParametros = New System.Windows.Forms.Button
        Me.btnFinalizar = New System.Windows.Forms.Button
        Me.txtCantExtracciones = New System.Windows.Forms.TextBox
        Me.lblCantExtracciones = New System.Windows.Forms.Label
        Me.CAutoridadesBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.GroupBox3.SuspendLayout()
        Me.GroupBoxIngreso.SuspendLayout()
        Me.GpbConfirmarExtraccion.SuspendLayout()
        Me.gpbIngresoDatos.SuspendLayout()
        CType(Me.CAutoridadesBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupBox3
        '
        Me.GroupBox3.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBox3.BackColor = System.Drawing.Color.Transparent
        Me.GroupBox3.Controls.Add(Me.cboIraExtraccion)
        Me.GroupBox3.Controls.Add(Me.btnextraccionSiguiente)
        Me.GroupBox3.Controls.Add(Me.btnExtraccionAnterior)
        Me.GroupBox3.Controls.Add(Me.GroupBoxIngreso)
        Me.GroupBox3.Controls.Add(Me.TabExtracciones)
        Me.GroupBox3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.GroupBox3.Location = New System.Drawing.Point(7, 65)
        Me.GroupBox3.Margin = New System.Windows.Forms.Padding(1)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Padding = New System.Windows.Forms.Padding(1)
        Me.GroupBox3.Size = New System.Drawing.Size(1015, 524)
        Me.GroupBox3.TabIndex = 15
        Me.GroupBox3.TabStop = False
        '
        'cboIraExtraccion
        '
        Me.cboIraExtraccion.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cboIraExtraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboIraExtraccion.FormattingEnabled = True
        Me.cboIraExtraccion.Location = New System.Drawing.Point(275, 493)
        Me.cboIraExtraccion.Margin = New System.Windows.Forms.Padding(4)
        Me.cboIraExtraccion.Name = "cboIraExtraccion"
        Me.cboIraExtraccion.Size = New System.Drawing.Size(238, 24)
        Me.cboIraExtraccion.TabIndex = 77
        Me.cboIraExtraccion.Text = "IR A EXTRACCION"
        '
        'btnextraccionSiguiente
        '
        Me.btnextraccionSiguiente.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnextraccionSiguiente.BackColor = System.Drawing.SystemColors.Control
        Me.btnextraccionSiguiente.BackgroundImage = CType(resources.GetObject("btnextraccionSiguiente.BackgroundImage"), System.Drawing.Image)
        Me.btnextraccionSiguiente.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnextraccionSiguiente.Enabled = False
        Me.btnextraccionSiguiente.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnextraccionSiguiente.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnextraccionSiguiente.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnextraccionSiguiente.Location = New System.Drawing.Point(521, 492)
        Me.btnextraccionSiguiente.Margin = New System.Windows.Forms.Padding(4)
        Me.btnextraccionSiguiente.Name = "btnextraccionSiguiente"
        Me.btnextraccionSiguiente.Size = New System.Drawing.Size(259, 26)
        Me.btnextraccionSiguiente.TabIndex = 76
        Me.btnextraccionSiguiente.Text = "EXTRACCION &SIGUIENTE   >>"
        Me.ToolTip1.SetToolTip(Me.btnextraccionSiguiente, "Visualiza la extracción siguiente")
        Me.btnextraccionSiguiente.UseVisualStyleBackColor = False
        '
        'btnExtraccionAnterior
        '
        Me.btnExtraccionAnterior.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnExtraccionAnterior.BackColor = System.Drawing.SystemColors.Control
        Me.btnExtraccionAnterior.BackgroundImage = CType(resources.GetObject("btnExtraccionAnterior.BackgroundImage"), System.Drawing.Image)
        Me.btnExtraccionAnterior.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExtraccionAnterior.Enabled = False
        Me.btnExtraccionAnterior.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExtraccionAnterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExtraccionAnterior.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnExtraccionAnterior.Location = New System.Drawing.Point(8, 492)
        Me.btnExtraccionAnterior.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExtraccionAnterior.Name = "btnExtraccionAnterior"
        Me.btnExtraccionAnterior.Size = New System.Drawing.Size(259, 26)
        Me.btnExtraccionAnterior.TabIndex = 75
        Me.btnExtraccionAnterior.Text = "<<   EXTRACCION &ANTERIOR"
        Me.btnExtraccionAnterior.UseVisualStyleBackColor = False
        '
        'GroupBoxIngreso
        '
        Me.GroupBoxIngreso.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxIngreso.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.GroupBoxIngreso.Controls.Add(Me.cboMetodoIngreso)
        Me.GroupBoxIngreso.Controls.Add(Me.lblmetodoingreso)
        Me.GroupBoxIngreso.Controls.Add(Me.GpbConfirmarExtraccion)
        Me.GroupBoxIngreso.Controls.Add(Me.gpbIngresoDatos)
        Me.GroupBoxIngreso.Location = New System.Drawing.Point(718, 16)
        Me.GroupBoxIngreso.Margin = New System.Windows.Forms.Padding(4)
        Me.GroupBoxIngreso.Name = "GroupBoxIngreso"
        Me.GroupBoxIngreso.Padding = New System.Windows.Forms.Padding(4)
        Me.GroupBoxIngreso.Size = New System.Drawing.Size(281, 469)
        Me.GroupBoxIngreso.TabIndex = 38
        Me.GroupBoxIngreso.TabStop = False
        '
        'cboMetodoIngreso
        '
        Me.cboMetodoIngreso.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cboMetodoIngreso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboMetodoIngreso.FormattingEnabled = True
        Me.cboMetodoIngreso.Location = New System.Drawing.Point(8, 48)
        Me.cboMetodoIngreso.Margin = New System.Windows.Forms.Padding(4)
        Me.cboMetodoIngreso.Name = "cboMetodoIngreso"
        Me.cboMetodoIngreso.Size = New System.Drawing.Size(267, 26)
        Me.cboMetodoIngreso.TabIndex = 39
        '
        'lblmetodoingreso
        '
        Me.lblmetodoingreso.AutoSize = True
        Me.lblmetodoingreso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblmetodoingreso.Location = New System.Drawing.Point(4, 21)
        Me.lblmetodoingreso.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblmetodoingreso.Name = "lblmetodoingreso"
        Me.lblmetodoingreso.Size = New System.Drawing.Size(154, 18)
        Me.lblmetodoingreso.TabIndex = 38
        Me.lblmetodoingreso.Text = "Método de Ingreso:"
        '
        'GpbConfirmarExtraccion
        '
        Me.GpbConfirmarExtraccion.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.btnRevertirExtraccion)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.btmConfirmar)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.DTPHoraFinextraccion)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.Label3)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.DTPHoraInicioextraccion)
        Me.GpbConfirmarExtraccion.Controls.Add(Me.Label1)
        Me.GpbConfirmarExtraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpbConfirmarExtraccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.GpbConfirmarExtraccion.Location = New System.Drawing.Point(8, 371)
        Me.GpbConfirmarExtraccion.Margin = New System.Windows.Forms.Padding(4)
        Me.GpbConfirmarExtraccion.Name = "GpbConfirmarExtraccion"
        Me.GpbConfirmarExtraccion.Padding = New System.Windows.Forms.Padding(4)
        Me.GpbConfirmarExtraccion.Size = New System.Drawing.Size(268, 72)
        Me.GpbConfirmarExtraccion.TabIndex = 37
        Me.GpbConfirmarExtraccion.TabStop = False
        Me.GpbConfirmarExtraccion.Text = "Confirmar Extracción"
        '
        'btnRevertirExtraccion
        '
        Me.btnRevertirExtraccion.BackColor = System.Drawing.SystemColors.Control
        Me.btnRevertirExtraccion.BackgroundImage = CType(resources.GetObject("btnRevertirExtraccion.BackgroundImage"), System.Drawing.Image)
        Me.btnRevertirExtraccion.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRevertirExtraccion.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRevertirExtraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRevertirExtraccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnRevertirExtraccion.Location = New System.Drawing.Point(14, 25)
        Me.btnRevertirExtraccion.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRevertirExtraccion.Name = "btnRevertirExtraccion"
        Me.btnRevertirExtraccion.Size = New System.Drawing.Size(119, 26)
        Me.btnRevertirExtraccion.TabIndex = 65
        Me.btnRevertirExtraccion.Text = "&REVERTIR"
        Me.ToolTip1.SetToolTip(Me.btnRevertirExtraccion, "Revierte la extracción actual")
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
        Me.btmConfirmar.Location = New System.Drawing.Point(137, 25)
        Me.btmConfirmar.Margin = New System.Windows.Forms.Padding(4)
        Me.btmConfirmar.Name = "btmConfirmar"
        Me.btmConfirmar.Size = New System.Drawing.Size(119, 26)
        Me.btmConfirmar.TabIndex = 64
        Me.btmConfirmar.Text = "C&ONFIRMAR"
        Me.ToolTip1.SetToolTip(Me.btmConfirmar, "Confirma la extracción actual")
        Me.btmConfirmar.UseVisualStyleBackColor = False
        '
        'DTPHoraFinextraccion
        '
        Me.DTPHoraFinextraccion.CustomFormat = "HH:mm:ss"
        Me.DTPHoraFinextraccion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPHoraFinextraccion.Location = New System.Drawing.Point(162, 74)
        Me.DTPHoraFinextraccion.Margin = New System.Windows.Forms.Padding(4)
        Me.DTPHoraFinextraccion.Name = "DTPHoraFinextraccion"
        Me.DTPHoraFinextraccion.ShowUpDown = True
        Me.DTPHoraFinextraccion.Size = New System.Drawing.Size(90, 24)
        Me.DTPHoraFinextraccion.TabIndex = 22
        Me.DTPHoraFinextraccion.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.Location = New System.Drawing.Point(73, 78)
        Me.Label3.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(99, 20)
        Me.Label3.TabIndex = 21
        Me.Label3.Text = "HORA FIN:"
        Me.Label3.Visible = False
        '
        'DTPHoraInicioextraccion
        '
        Me.DTPHoraInicioextraccion.CustomFormat = "HH:mm:ss"
        Me.DTPHoraInicioextraccion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPHoraInicioextraccion.Location = New System.Drawing.Point(162, 42)
        Me.DTPHoraInicioextraccion.Margin = New System.Windows.Forms.Padding(4)
        Me.DTPHoraInicioextraccion.Name = "DTPHoraInicioextraccion"
        Me.DTPHoraInicioextraccion.ShowUpDown = True
        Me.DTPHoraInicioextraccion.Size = New System.Drawing.Size(90, 24)
        Me.DTPHoraInicioextraccion.TabIndex = 20
        Me.DTPHoraInicioextraccion.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(52, 46)
        Me.Label1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(125, 20)
        Me.Label1.TabIndex = 19
        Me.Label1.Text = "HORA INICIO:"
        Me.Label1.Visible = False
        '
        'gpbIngresoDatos
        '
        Me.gpbIngresoDatos.Controls.Add(Me.btnPorArchivo)
        Me.gpbIngresoDatos.Controls.Add(Me.btnSonido)
        Me.gpbIngresoDatos.Controls.Add(Me.LblTeclados)
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
        Me.gpbIngresoDatos.Controls.Add(Me.txtPosEnExtracto2)
        Me.gpbIngresoDatos.Controls.Add(Me.txtValor1Extraccion)
        Me.gpbIngresoDatos.Controls.Add(Me.txtPosEnExtracto1)
        Me.gpbIngresoDatos.Controls.Add(Me.lblValor)
        Me.gpbIngresoDatos.Controls.Add(Me.lblPosicion)
        Me.gpbIngresoDatos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.gpbIngresoDatos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.gpbIngresoDatos.Location = New System.Drawing.Point(8, 105)
        Me.gpbIngresoDatos.Margin = New System.Windows.Forms.Padding(4)
        Me.gpbIngresoDatos.Name = "gpbIngresoDatos"
        Me.gpbIngresoDatos.Padding = New System.Windows.Forms.Padding(4)
        Me.gpbIngresoDatos.Size = New System.Drawing.Size(268, 259)
        Me.gpbIngresoDatos.TabIndex = 36
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
        Me.btnPorArchivo.Location = New System.Drawing.Point(14, 71)
        Me.btnPorArchivo.Margin = New System.Windows.Forms.Padding(4)
        Me.btnPorArchivo.Name = "btnPorArchivo"
        Me.btnPorArchivo.Size = New System.Drawing.Size(239, 26)
        Me.btnPorArchivo.TabIndex = 70
        Me.btnPorArchivo.Text = "&OBTENER EXTRACCIONES"
        Me.ToolTip1.SetToolTip(Me.btnPorArchivo, "Modifica un extracto(F1)")
        Me.btnPorArchivo.UseVisualStyleBackColor = False
        '
        'btnSonido
        '
        Me.btnSonido.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSonido.Location = New System.Drawing.Point(216, 73)
        Me.btnSonido.Margin = New System.Windows.Forms.Padding(4)
        Me.btnSonido.Name = "btnSonido"
        Me.btnSonido.Size = New System.Drawing.Size(30, 26)
        Me.btnSonido.TabIndex = 69
        Me.ToolTip1.SetToolTip(Me.btnSonido, "Habilitar/Deshabilitar Sonido")
        Me.btnSonido.UseVisualStyleBackColor = True
        '
        'LblTeclados
        '
        Me.LblTeclados.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTeclados.Location = New System.Drawing.Point(4, 224)
        Me.LblTeclados.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.LblTeclados.Name = "LblTeclados"
        Me.LblTeclados.Size = New System.Drawing.Size(242, 31)
        Me.LblTeclados.TabIndex = 68
        Me.LblTeclados.Text = "Teclados Deshabilitados"
        Me.LblTeclados.TextAlign = System.Drawing.ContentAlignment.TopCenter
        Me.LblTeclados.Visible = False
        '
        'CboPuertos
        '
        Me.CboPuertos.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.CboPuertos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboPuertos.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboPuertos.FormattingEnabled = True
        Me.CboPuertos.Location = New System.Drawing.Point(14, 71)
        Me.CboPuertos.Margin = New System.Windows.Forms.Padding(4)
        Me.CboPuertos.Name = "CboPuertos"
        Me.CboPuertos.Size = New System.Drawing.Size(118, 28)
        Me.CboPuertos.TabIndex = 67
        Me.CboPuertos.Visible = False
        '
        'BtnConectar
        '
        Me.BtnConectar.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnConectar.Location = New System.Drawing.Point(162, 73)
        Me.BtnConectar.Margin = New System.Windows.Forms.Padding(4)
        Me.BtnConectar.Name = "BtnConectar"
        Me.BtnConectar.Size = New System.Drawing.Size(39, 26)
        Me.BtnConectar.TabIndex = 66
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
        Me.btnCancelar.Location = New System.Drawing.Point(11, 187)
        Me.btnCancelar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(120, 26)
        Me.btnCancelar.TabIndex = 65
        Me.btnCancelar.Text = "&CANCELAR"
        Me.ToolTip1.SetToolTip(Me.btnCancelar, "Cancelar Ingreso(F3)")
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
        Me.btmModificar.Location = New System.Drawing.Point(135, 187)
        Me.btmModificar.Margin = New System.Windows.Forms.Padding(4)
        Me.btmModificar.Name = "btmModificar"
        Me.btmModificar.Size = New System.Drawing.Size(124, 26)
        Me.btmModificar.TabIndex = 64
        Me.btmModificar.Text = "&MODIFICAR"
        Me.ToolTip1.SetToolTip(Me.btmModificar, "Modifica un extracto(F1)")
        Me.btmModificar.UseVisualStyleBackColor = False
        '
        'txtExtractoHasta
        '
        Me.txtExtractoHasta.Enabled = False
        Me.txtExtractoHasta.Location = New System.Drawing.Point(219, 30)
        Me.txtExtractoHasta.Margin = New System.Windows.Forms.Padding(4)
        Me.txtExtractoHasta.Name = "txtExtractoHasta"
        Me.txtExtractoHasta.Size = New System.Drawing.Size(34, 24)
        Me.txtExtractoHasta.TabIndex = 62
        Me.txtExtractoHasta.Text = "20"
        Me.txtExtractoHasta.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblDe
        '
        Me.lblDe.Location = New System.Drawing.Point(171, 35)
        Me.lblDe.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblDe.Name = "lblDe"
        Me.lblDe.Size = New System.Drawing.Size(40, 24)
        Me.lblDe.TabIndex = 61
        Me.lblDe.Text = "DE "
        '
        'txtordenExtracto
        '
        Me.txtordenExtracto.Enabled = False
        Me.txtordenExtracto.Location = New System.Drawing.Point(126, 30)
        Me.txtordenExtracto.Margin = New System.Windows.Forms.Padding(4)
        Me.txtordenExtracto.Name = "txtordenExtracto"
        Me.txtordenExtracto.Size = New System.Drawing.Size(34, 24)
        Me.txtordenExtracto.TabIndex = 60
        Me.txtordenExtracto.Text = "1"
        Me.txtordenExtracto.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblExtracto
        '
        Me.lblExtracto.Location = New System.Drawing.Point(8, 35)
        Me.lblExtracto.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblExtracto.Name = "lblExtracto"
        Me.lblExtracto.Size = New System.Drawing.Size(120, 24)
        Me.lblExtracto.TabIndex = 59
        Me.lblExtracto.Text = "Orden de salida"
        '
        'lblingreso2
        '
        Me.lblingreso2.AutoSize = True
        Me.lblingreso2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblingreso2.Location = New System.Drawing.Point(43, 156)
        Me.lblingreso2.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblingreso2.Name = "lblingreso2"
        Me.lblingreso2.Size = New System.Drawing.Size(93, 18)
        Me.lblingreso2.TabIndex = 58
        Me.lblingreso2.Text = "INGRESO 2:"
        '
        'lblingreso1
        '
        Me.lblingreso1.AutoSize = True
        Me.lblingreso1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblingreso1.Location = New System.Drawing.Point(44, 115)
        Me.lblingreso1.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblingreso1.Name = "lblingreso1"
        Me.lblingreso1.Size = New System.Drawing.Size(93, 18)
        Me.lblingreso1.TabIndex = 57
        Me.lblingreso1.Text = "INGRESO 1:"
        '
        'txtValorExtraccion2
        '
        Me.txtValorExtraccion2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValorExtraccion2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtValorExtraccion2.Location = New System.Drawing.Point(136, 148)
        Me.txtValorExtraccion2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtValorExtraccion2.Name = "txtValorExtraccion2"
        Me.txtValorExtraccion2.Size = New System.Drawing.Size(65, 24)
        Me.txtValorExtraccion2.TabIndex = 56
        Me.txtValorExtraccion2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPosEnExtracto2
        '
        Me.txtPosEnExtracto2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPosEnExtracto2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtPosEnExtracto2.Location = New System.Drawing.Point(220, 148)
        Me.txtPosEnExtracto2.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPosEnExtracto2.MaxLength = 2
        Me.txtPosEnExtracto2.Name = "txtPosEnExtracto2"
        Me.txtPosEnExtracto2.Size = New System.Drawing.Size(36, 24)
        Me.txtPosEnExtracto2.TabIndex = 57
        Me.txtPosEnExtracto2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtValor1Extraccion
        '
        Me.txtValor1Extraccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtValor1Extraccion.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtValor1Extraccion.Location = New System.Drawing.Point(136, 107)
        Me.txtValor1Extraccion.Margin = New System.Windows.Forms.Padding(4)
        Me.txtValor1Extraccion.Name = "txtValor1Extraccion"
        Me.txtValor1Extraccion.Size = New System.Drawing.Size(65, 24)
        Me.txtValor1Extraccion.TabIndex = 53
        Me.txtValor1Extraccion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPosEnExtracto1
        '
        Me.txtPosEnExtracto1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPosEnExtracto1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.txtPosEnExtracto1.Location = New System.Drawing.Point(220, 107)
        Me.txtPosEnExtracto1.Margin = New System.Windows.Forms.Padding(4)
        Me.txtPosEnExtracto1.MaxLength = 2
        Me.txtPosEnExtracto1.Name = "txtPosEnExtracto1"
        Me.txtPosEnExtracto1.Size = New System.Drawing.Size(36, 24)
        Me.txtPosEnExtracto1.TabIndex = 54
        Me.txtPosEnExtracto1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblValor
        '
        Me.lblValor.AutoSize = True
        Me.lblValor.Location = New System.Drawing.Point(132, 73)
        Me.lblValor.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblValor.Name = "lblValor"
        Me.lblValor.Size = New System.Drawing.Size(82, 18)
        Me.lblValor.TabIndex = 52
        Me.lblValor.Text = "NUMERO"
        '
        'lblPosicion
        '
        Me.lblPosicion.AutoSize = True
        Me.lblPosicion.Location = New System.Drawing.Point(216, 73)
        Me.lblPosicion.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblPosicion.Name = "lblPosicion"
        Me.lblPosicion.Size = New System.Drawing.Size(43, 18)
        Me.lblPosicion.TabIndex = 51
        Me.lblPosicion.Text = "POS"
        '
        'TabExtracciones
        '
        Me.TabExtracciones.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabExtracciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabExtracciones.Location = New System.Drawing.Point(8, 20)
        Me.TabExtracciones.Margin = New System.Windows.Forms.Padding(4)
        Me.TabExtracciones.Name = "TabExtracciones"
        Me.TabExtracciones.SelectedIndex = 0
        Me.TabExtracciones.Size = New System.Drawing.Size(705, 465)
        Me.TabExtracciones.TabIndex = 39
        '
        'txtModeloExtracciones
        '
        Me.txtModeloExtracciones.Enabled = False
        Me.txtModeloExtracciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtModeloExtracciones.Location = New System.Drawing.Point(92, 43)
        Me.txtModeloExtracciones.Margin = New System.Windows.Forms.Padding(4)
        Me.txtModeloExtracciones.Name = "txtModeloExtracciones"
        Me.txtModeloExtracciones.Size = New System.Drawing.Size(335, 24)
        Me.txtModeloExtracciones.TabIndex = 12
        Me.txtModeloExtracciones.Text = "3 - Lotería c/ Noc, c/ Ch y/o Poc. Fed. y/o Tóm"
        '
        'lblModelo
        '
        Me.lblModelo.AutoSize = True
        Me.lblModelo.BackColor = System.Drawing.Color.Transparent
        Me.lblModelo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblModelo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblModelo.Location = New System.Drawing.Point(18, 47)
        Me.lblModelo.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblModelo.Name = "lblModelo"
        Me.lblModelo.Size = New System.Drawing.Size(69, 18)
        Me.lblModelo.TabIndex = 11
        Me.lblModelo.Text = "Modelo:"
        '
        'CboConcurso
        '
        Me.CboConcurso.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.CboConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboConcurso.FormattingEnabled = True
        Me.CboConcurso.Location = New System.Drawing.Point(92, 6)
        Me.CboConcurso.Margin = New System.Windows.Forms.Padding(4)
        Me.CboConcurso.Name = "CboConcurso"
        Me.CboConcurso.Size = New System.Drawing.Size(323, 26)
        Me.CboConcurso.TabIndex = 8
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(4, 11)
        Me.Label4.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 18)
        Me.Label4.TabIndex = 7
        Me.Label4.Text = "Concurso:"
        '
        'DTPFechaConcurso
        '
        Me.DTPFechaConcurso.CalendarFont = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPFechaConcurso.Enabled = False
        Me.DTPFechaConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPFechaConcurso.Location = New System.Drawing.Point(507, 6)
        Me.DTPFechaConcurso.Margin = New System.Windows.Forms.Padding(4)
        Me.DTPFechaConcurso.Name = "DTPFechaConcurso"
        Me.DTPFechaConcurso.ShowUpDown = True
        Me.DTPFechaConcurso.Size = New System.Drawing.Size(276, 24)
        Me.DTPFechaConcurso.TabIndex = 15
        '
        'DTPHoraConcurso
        '
        Me.DTPHoraConcurso.CustomFormat = "HH:mm"
        Me.DTPHoraConcurso.Enabled = False
        Me.DTPHoraConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPHoraConcurso.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.DTPHoraConcurso.Location = New System.Drawing.Point(895, 7)
        Me.DTPHoraConcurso.Margin = New System.Windows.Forms.Padding(4)
        Me.DTPHoraConcurso.Name = "DTPHoraConcurso"
        Me.DTPHoraConcurso.ShowUpDown = True
        Me.DTPHoraConcurso.Size = New System.Drawing.Size(59, 24)
        Me.DTPHoraConcurso.TabIndex = 14
        '
        'lblHora
        '
        Me.lblHora.AutoSize = True
        Me.lblHora.BackColor = System.Drawing.Color.Transparent
        Me.lblHora.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblHora.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblHora.Location = New System.Drawing.Point(841, 11)
        Me.lblHora.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblHora.Name = "lblHora"
        Me.lblHora.Size = New System.Drawing.Size(50, 18)
        Me.lblHora.TabIndex = 13
        Me.lblHora.Text = "Hora:"
        '
        'lblFecha
        '
        Me.lblFecha.AutoSize = True
        Me.lblFecha.BackColor = System.Drawing.Color.Transparent
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblFecha.Location = New System.Drawing.Point(447, 11)
        Me.lblFecha.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(59, 18)
        Me.lblFecha.TabIndex = 12
        Me.lblFecha.Text = "Fecha:"
        '
        'BTNSALIR
        '
        Me.BTNSALIR.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.BTNSALIR.BackColor = System.Drawing.SystemColors.Control
        Me.BTNSALIR.BackgroundImage = CType(resources.GetObject("BTNSALIR.BackgroundImage"), System.Drawing.Image)
        Me.BTNSALIR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BTNSALIR.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BTNSALIR.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BTNSALIR.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.BTNSALIR.Location = New System.Drawing.Point(707, 594)
        Me.BTNSALIR.Margin = New System.Windows.Forms.Padding(4)
        Me.BTNSALIR.Name = "BTNSALIR"
        Me.BTNSALIR.Size = New System.Drawing.Size(80, 26)
        Me.BTNSALIR.TabIndex = 83
        Me.BTNSALIR.Text = "SA&LIR"
        Me.ToolTip1.SetToolTip(Me.BTNSALIR, "Salir")
        Me.BTNSALIR.UseVisualStyleBackColor = False
        '
        'btnExtra
        '
        Me.btnExtra.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnExtra.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnExtra.BackgroundImage = CType(resources.GetObject("btnExtra.BackgroundImage"), System.Drawing.Image)
        Me.btnExtra.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnExtra.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnExtra.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnExtra.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnExtra.Location = New System.Drawing.Point(13, 594)
        Me.btnExtra.Margin = New System.Windows.Forms.Padding(4)
        Me.btnExtra.Name = "btnExtra"
        Me.btnExtra.Size = New System.Drawing.Size(223, 26)
        Me.btnExtra.TabIndex = 82
        Me.btnExtra.Text = "PREMIO E&XTRA QUINI 6"
        Me.ToolTip1.SetToolTip(Me.btnExtra, "Muetra los números del premio Extra de Quini6")
        Me.btnExtra.UseVisualStyleBackColor = False
        '
        'btnRevertir
        '
        Me.btnRevertir.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnRevertir.BackColor = System.Drawing.SystemColors.Control
        Me.btnRevertir.BackgroundImage = CType(resources.GetObject("btnRevertir.BackgroundImage"), System.Drawing.Image)
        Me.btnRevertir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnRevertir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnRevertir.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnRevertir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnRevertir.Location = New System.Drawing.Point(244, 594)
        Me.btnRevertir.Margin = New System.Windows.Forms.Padding(4)
        Me.btnRevertir.Name = "btnRevertir"
        Me.btnRevertir.Size = New System.Drawing.Size(197, 26)
        Me.btnRevertir.TabIndex = 81
        Me.btnRevertir.Text = "RE&VERTIR SORTEO"
        Me.ToolTip1.SetToolTip(Me.btnRevertir, "Revierte el concurso actual")
        Me.btnRevertir.UseVisualStyleBackColor = False
        '
        'btnListarParametros
        '
        Me.btnListarParametros.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnListarParametros.AutoSize = True
        Me.btnListarParametros.BackColor = System.Drawing.SystemColors.Control
        Me.btnListarParametros.BackgroundImage = CType(resources.GetObject("btnListarParametros.BackgroundImage"), System.Drawing.Image)
        Me.btnListarParametros.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnListarParametros.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnListarParametros.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnListarParametros.ForeColor = System.Drawing.Color.White
        Me.btnListarParametros.Image = Global.SorteosCAS.My.Resources.Imagenes.ImagenImprimir
        Me.btnListarParametros.Location = New System.Drawing.Point(450, 594)
        Me.btnListarParametros.Margin = New System.Windows.Forms.Padding(4)
        Me.btnListarParametros.Name = "btnListarParametros"
        Me.btnListarParametros.Size = New System.Drawing.Size(32, 26)
        Me.btnListarParametros.TabIndex = 80
        Me.ToolTip1.SetToolTip(Me.btnListarParametros, "Imprimir Listados")
        Me.btnListarParametros.UseVisualStyleBackColor = False
        '
        'btnFinalizar
        '
        Me.btnFinalizar.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.btnFinalizar.BackColor = System.Drawing.SystemColors.Control
        Me.btnFinalizar.BackgroundImage = CType(resources.GetObject("btnFinalizar.BackgroundImage"), System.Drawing.Image)
        Me.btnFinalizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnFinalizar.Enabled = False
        Me.btnFinalizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnFinalizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnFinalizar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnFinalizar.Location = New System.Drawing.Point(490, 594)
        Me.btnFinalizar.Margin = New System.Windows.Forms.Padding(4)
        Me.btnFinalizar.Name = "btnFinalizar"
        Me.btnFinalizar.Size = New System.Drawing.Size(197, 26)
        Me.btnFinalizar.TabIndex = 79
        Me.btnFinalizar.Text = "&FINALIZAR SORTEO"
        Me.ToolTip1.SetToolTip(Me.btnFinalizar, "Finaliza el concurso actual")
        Me.btnFinalizar.UseVisualStyleBackColor = False
        '
        'txtCantExtracciones
        '
        Me.txtCantExtracciones.Enabled = False
        Me.txtCantExtracciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCantExtracciones.Location = New System.Drawing.Point(661, 43)
        Me.txtCantExtracciones.Margin = New System.Windows.Forms.Padding(4)
        Me.txtCantExtracciones.Name = "txtCantExtracciones"
        Me.txtCantExtracciones.Size = New System.Drawing.Size(45, 24)
        Me.txtCantExtracciones.TabIndex = 85
        Me.txtCantExtracciones.Text = "2"
        Me.txtCantExtracciones.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'lblCantExtracciones
        '
        Me.lblCantExtracciones.AutoSize = True
        Me.lblCantExtracciones.BackColor = System.Drawing.Color.Transparent
        Me.lblCantExtracciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblCantExtracciones.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblCantExtracciones.Location = New System.Drawing.Point(548, 47)
        Me.lblCantExtracciones.Margin = New System.Windows.Forms.Padding(4, 0, 4, 0)
        Me.lblCantExtracciones.Name = "lblCantExtracciones"
        Me.lblCantExtracciones.Size = New System.Drawing.Size(111, 18)
        Me.lblCantExtracciones.TabIndex = 84
        Me.lblCantExtracciones.Text = "Extracciones:"
        '
        'CAutoridadesBindingSource
        '
        Me.CAutoridadesBindingSource.DataSource = GetType(Sorteos.Helpers.cAutoridades)
        '
        'ConcursoExtracciones
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(9.0!, 18.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1006, 633)
        Me.Controls.Add(Me.txtCantExtracciones)
        Me.Controls.Add(Me.lblCantExtracciones)
        Me.Controls.Add(Me.BTNSALIR)
        Me.Controls.Add(Me.btnExtra)
        Me.Controls.Add(Me.btnRevertir)
        Me.Controls.Add(Me.btnListarParametros)
        Me.Controls.Add(Me.txtModeloExtracciones)
        Me.Controls.Add(Me.btnFinalizar)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.lblModelo)
        Me.Controls.Add(Me.DTPFechaConcurso)
        Me.Controls.Add(Me.DTPHoraConcurso)
        Me.Controls.Add(Me.lblHora)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.CboConcurso)
        Me.Controls.Add(Me.Label4)
        Me.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Margin = New System.Windows.Forms.Padding(4)
        Me.Name = "ConcursoExtracciones"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Extracciones del Sorteo"
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBoxIngreso.ResumeLayout(False)
        Me.GroupBoxIngreso.PerformLayout()
        Me.GpbConfirmarExtraccion.ResumeLayout(False)
        Me.GpbConfirmarExtraccion.PerformLayout()
        Me.gpbIngresoDatos.ResumeLayout(False)
        Me.gpbIngresoDatos.PerformLayout()
        CType(Me.CAutoridadesBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents CboConcurso As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents DTPFechaConcurso As System.Windows.Forms.DateTimePicker
    Friend WithEvents DTPHoraConcurso As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblHora As System.Windows.Forms.Label
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents txtModeloExtracciones As System.Windows.Forms.TextBox
    Friend WithEvents lblModelo As System.Windows.Forms.Label
    Friend WithEvents CAutoridadesBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents BackgroundWorker1 As System.ComponentModel.BackgroundWorker
    Friend WithEvents GroupBoxIngreso As System.Windows.Forms.GroupBox
    Friend WithEvents GpbConfirmarExtraccion As System.Windows.Forms.GroupBox
    Friend WithEvents btmConfirmar As System.Windows.Forms.Button
    Friend WithEvents DTPHoraFinextraccion As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents DTPHoraInicioextraccion As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents gpbIngresoDatos As System.Windows.Forms.GroupBox
    Friend WithEvents btmModificar As System.Windows.Forms.Button
    Friend WithEvents txtExtractoHasta As System.Windows.Forms.TextBox
    Friend WithEvents lblDe As System.Windows.Forms.Label
    Friend WithEvents txtordenExtracto As System.Windows.Forms.TextBox
    Friend WithEvents lblExtracto As System.Windows.Forms.Label
    Friend WithEvents lblingreso2 As System.Windows.Forms.Label
    Friend WithEvents lblingreso1 As System.Windows.Forms.Label
    Friend WithEvents txtValorExtraccion2 As System.Windows.Forms.TextBox
    Friend WithEvents txtPosEnExtracto2 As System.Windows.Forms.TextBox
    Friend WithEvents txtValor1Extraccion As System.Windows.Forms.TextBox
    Friend WithEvents txtPosEnExtracto1 As System.Windows.Forms.TextBox
    Friend WithEvents lblValor As System.Windows.Forms.Label
    Friend WithEvents lblPosicion As System.Windows.Forms.Label
    Friend WithEvents TabExtracciones As System.Windows.Forms.TabControl
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblmetodoingreso As System.Windows.Forms.Label
    Friend WithEvents cboMetodoIngreso As System.Windows.Forms.ComboBox
    Friend WithEvents btnRevertirExtraccion As System.Windows.Forms.Button
    Friend WithEvents cboIraExtraccion As System.Windows.Forms.ComboBox
    Friend WithEvents btnextraccionSiguiente As System.Windows.Forms.Button
    Friend WithEvents btnExtraccionAnterior As System.Windows.Forms.Button
    Friend WithEvents ToolTip1 As System.Windows.Forms.ToolTip
    Friend WithEvents BTNSALIR As System.Windows.Forms.Button
    Friend WithEvents btnExtra As System.Windows.Forms.Button
    Friend WithEvents btnRevertir As System.Windows.Forms.Button
    Friend WithEvents btnListarParametros As System.Windows.Forms.Button
    Friend WithEvents btnFinalizar As System.Windows.Forms.Button
    Friend WithEvents CboPuertos As System.Windows.Forms.ComboBox
    Friend WithEvents BtnConectar As System.Windows.Forms.Button
    Friend WithEvents LblTeclados As System.Windows.Forms.Label
    Friend WithEvents btnSonido As System.Windows.Forms.Button
    Friend WithEvents txtCantExtracciones As System.Windows.Forms.TextBox
    Friend WithEvents lblCantExtracciones As System.Windows.Forms.Label
    Friend WithEvents btnPorArchivo As System.Windows.Forms.Button

End Class
