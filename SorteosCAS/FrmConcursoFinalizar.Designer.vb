<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmConcursoFinalizar
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmConcursoFinalizar))
        Me.Button1 = New System.Windows.Forms.Button
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.CboConcurso = New System.Windows.Forms.ComboBox
        Me.DTPFechaConcurso = New System.Windows.Forms.DateTimePicker
        Me.lblFecha = New System.Windows.Forms.Label
        Me.grpJuegos = New System.Windows.Forms.GroupBox
        Me.TabJuegos = New System.Windows.Forms.TabControl
        Me.dtpHoraConcurso = New System.Windows.Forms.DateTimePicker
        Me.BtnBuscarConcurso = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.PTBreloj = New System.Windows.Forms.PictureBox
        Me.btnAnteriores = New System.Windows.Forms.Button
        Me.grpJuegos.SuspendLayout()
        CType(Me.PTBreloj, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(117, 186)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(75, 23)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "Button1"
        Me.Button1.UseVisualStyleBackColor = True
        Me.Button1.Visible = False
        '
        'TextBox1
        '
        Me.TextBox1.Location = New System.Drawing.Point(12, 141)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.Size = New System.Drawing.Size(100, 20)
        Me.TextBox1.TabIndex = 1
        Me.TextBox1.Visible = False
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(3, 11)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(87, 18)
        Me.Label4.TabIndex = 8
        Me.Label4.Text = "Concurso:"
        '
        'CboConcurso
        '
        Me.CboConcurso.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.CboConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CboConcurso.FormattingEnabled = True
        Me.CboConcurso.Location = New System.Drawing.Point(91, 6)
        Me.CboConcurso.Name = "CboConcurso"
        Me.CboConcurso.Size = New System.Drawing.Size(375, 28)
        Me.CboConcurso.TabIndex = 9
        '
        'DTPFechaConcurso
        '
        Me.DTPFechaConcurso.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.DTPFechaConcurso.Enabled = False
        Me.DTPFechaConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.DTPFechaConcurso.Location = New System.Drawing.Point(590, 7)
        Me.DTPFechaConcurso.Name = "DTPFechaConcurso"
        Me.DTPFechaConcurso.ShowUpDown = True
        Me.DTPFechaConcurso.Size = New System.Drawing.Size(273, 24)
        Me.DTPFechaConcurso.TabIndex = 17
        '
        'lblFecha
        '
        Me.lblFecha.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblFecha.AutoSize = True
        Me.lblFecha.BackColor = System.Drawing.Color.Transparent
        Me.lblFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblFecha.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblFecha.Location = New System.Drawing.Point(529, 11)
        Me.lblFecha.Name = "lblFecha"
        Me.lblFecha.Size = New System.Drawing.Size(59, 18)
        Me.lblFecha.TabIndex = 16
        Me.lblFecha.Text = "Fecha:"
        '
        'grpJuegos
        '
        Me.grpJuegos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.grpJuegos.BackColor = System.Drawing.Color.Transparent
        Me.grpJuegos.Controls.Add(Me.TabJuegos)
        Me.grpJuegos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpJuegos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.grpJuegos.Location = New System.Drawing.Point(6, 45)
        Me.grpJuegos.Margin = New System.Windows.Forms.Padding(0)
        Me.grpJuegos.Name = "grpJuegos"
        Me.grpJuegos.Padding = New System.Windows.Forms.Padding(1, 0, 1, 1)
        Me.grpJuegos.Size = New System.Drawing.Size(992, 580)
        Me.grpJuegos.TabIndex = 18
        Me.grpJuegos.TabStop = False
        '
        'TabJuegos
        '
        Me.TabJuegos.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.TabJuegos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TabJuegos.Location = New System.Drawing.Point(2, 19)
        Me.TabJuegos.Margin = New System.Windows.Forms.Padding(1)
        Me.TabJuegos.Multiline = True
        Me.TabJuegos.Name = "TabJuegos"
        Me.TabJuegos.Padding = New System.Drawing.Point(0, 0)
        Me.TabJuegos.SelectedIndex = 0
        Me.TabJuegos.Size = New System.Drawing.Size(984, 557)
        Me.TabJuegos.TabIndex = 39
        '
        'dtpHoraConcurso
        '
        Me.dtpHoraConcurso.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.dtpHoraConcurso.Cursor = System.Windows.Forms.Cursors.Default
        Me.dtpHoraConcurso.CustomFormat = "HH:mm"
        Me.dtpHoraConcurso.Enabled = False
        Me.dtpHoraConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.dtpHoraConcurso.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.dtpHoraConcurso.Location = New System.Drawing.Point(929, 7)
        Me.dtpHoraConcurso.Name = "dtpHoraConcurso"
        Me.dtpHoraConcurso.ShowUpDown = True
        Me.dtpHoraConcurso.Size = New System.Drawing.Size(62, 24)
        Me.dtpHoraConcurso.TabIndex = 20
        '
        'BtnBuscarConcurso
        '
        Me.BtnBuscarConcurso.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.BtnBuscarConcurso.Image = CType(resources.GetObject("BtnBuscarConcurso.Image"), System.Drawing.Image)
        Me.BtnBuscarConcurso.Location = New System.Drawing.Point(1014, 7)
        Me.BtnBuscarConcurso.Name = "BtnBuscarConcurso"
        Me.BtnBuscarConcurso.Size = New System.Drawing.Size(22, 23)
        Me.BtnBuscarConcurso.TabIndex = 19
        Me.BtnBuscarConcurso.UseVisualStyleBackColor = True
        Me.BtnBuscarConcurso.Visible = False
        '
        'Label1
        '
        Me.Label1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(877, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(50, 18)
        Me.Label1.TabIndex = 21
        Me.Label1.Text = "Hora:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'PTBreloj
        '
        Me.PTBreloj.BackColor = System.Drawing.Color.Transparent
        Me.PTBreloj.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PTBreloj.Image = CType(resources.GetObject("PTBreloj.Image"), System.Drawing.Image)
        Me.PTBreloj.Location = New System.Drawing.Point(472, 6)
        Me.PTBreloj.Name = "PTBreloj"
        Me.PTBreloj.Size = New System.Drawing.Size(38, 28)
        Me.PTBreloj.TabIndex = 22
        Me.PTBreloj.TabStop = False
        '
        'btnAnteriores
        '
        Me.btnAnteriores.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnteriores.ForeColor = System.Drawing.SystemColors.WindowText
        Me.btnAnteriores.Location = New System.Drawing.Point(468, 6)
        Me.btnAnteriores.Name = "btnAnteriores"
        Me.btnAnteriores.Size = New System.Drawing.Size(58, 28)
        Me.btnAnteriores.TabIndex = 23
        Me.btnAnteriores.Tag = ""
        Me.btnAnteriores.Text = "ANT."
        Me.btnAnteriores.UseVisualStyleBackColor = True
        '
        'FrmConcursoFinalizar
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(1006, 633)
        Me.Controls.Add(Me.btnAnteriores)
        Me.Controls.Add(Me.PTBreloj)
        Me.Controls.Add(Me.grpJuegos)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpHoraConcurso)
        Me.Controls.Add(Me.BtnBuscarConcurso)
        Me.Controls.Add(Me.DTPFechaConcurso)
        Me.Controls.Add(Me.lblFecha)
        Me.Controls.Add(Me.CboConcurso)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Button1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmConcursoFinalizar"
        Me.ShowInTaskbar = False
        Me.Text = "Confirmar Sorteo"
        Me.grpJuegos.ResumeLayout(False)
        CType(Me.PTBreloj, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents CboConcurso As System.Windows.Forms.ComboBox
    Friend WithEvents DTPFechaConcurso As System.Windows.Forms.DateTimePicker
    Friend WithEvents lblFecha As System.Windows.Forms.Label
    Friend WithEvents grpJuegos As System.Windows.Forms.GroupBox
    Friend WithEvents TabJuegos As System.Windows.Forms.TabControl
    Friend WithEvents dtpHoraConcurso As System.Windows.Forms.DateTimePicker
    Friend WithEvents BtnBuscarConcurso As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PTBreloj As System.Windows.Forms.PictureBox
    Friend WithEvents btnAnteriores As System.Windows.Forms.Button
End Class
