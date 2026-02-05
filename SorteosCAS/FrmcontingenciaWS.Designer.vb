<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmcontingenciaWS
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
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.Label5 = New System.Windows.Forms.Label
        Me.txtsorteo = New System.Windows.Forms.TextBox
        Me.txtfechapres = New System.Windows.Forms.TextBox
        Me.txtfecha = New System.Windows.Forms.TextBox
        Me.txtfechaprox = New System.Windows.Forms.TextBox
        Me.btnconfirmar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.Label6 = New System.Windows.Forms.Label
        Me.cmbJuego = New System.Windows.Forms.ComboBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txthora = New System.Windows.Forms.MaskedTextBox
        Me.GroupBox2 = New System.Windows.Forms.GroupBox
        Me.txthora2 = New System.Windows.Forms.MaskedTextBox
        Me.cmbJuego2 = New System.Windows.Forms.ComboBox
        Me.txtsorteo2 = New System.Windows.Forms.TextBox
        Me.txtfechaprox2 = New System.Windows.Forms.TextBox
        Me.txtfechapres2 = New System.Windows.Forms.TextBox
        Me.txtfecha2 = New System.Windows.Forms.TextBox
        Me.GroupBox3 = New System.Windows.Forms.GroupBox
        Me.txthora3 = New System.Windows.Forms.MaskedTextBox
        Me.cmbJuego3 = New System.Windows.Forms.ComboBox
        Me.txtfechaprox3 = New System.Windows.Forms.TextBox
        Me.txtfecha3 = New System.Windows.Forms.TextBox
        Me.txtfechapres3 = New System.Windows.Forms.TextBox
        Me.txtsorteo3 = New System.Windows.Forms.TextBox
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.txthora4 = New System.Windows.Forms.MaskedTextBox
        Me.cmbJuego4 = New System.Windows.Forms.ComboBox
        Me.txtfechaprox4 = New System.Windows.Forms.TextBox
        Me.txtfecha4 = New System.Windows.Forms.TextBox
        Me.txtfechapres4 = New System.Windows.Forms.TextBox
        Me.txtsorteo4 = New System.Windows.Forms.TextBox
        Me.GroupBox1.SuspendLayout()
        Me.GroupBox2.SuspendLayout()
        Me.GroupBox3.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(29, 24)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Juego"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(196, 23)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(49, 15)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "Sorteo"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(296, 24)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(46, 15)
        Me.Label3.TabIndex = 3
        Me.Label3.Text = "Fecha"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(432, 24)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(130, 15)
        Me.Label4.TabIndex = 4
        Me.Label4.Text = "Fecha Prescripción"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(568, 24)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(129, 15)
        Me.Label5.TabIndex = 5
        Me.Label5.Text = "Fecha Próx. Sorteo"
        '
        'txtsorteo
        '
        Me.txtsorteo.Location = New System.Drawing.Point(187, 19)
        Me.txtsorteo.MaxLength = 7
        Me.txtsorteo.Name = "txtsorteo"
        Me.txtsorteo.Size = New System.Drawing.Size(76, 20)
        Me.txtsorteo.TabIndex = 1
        '
        'txtfechapres
        '
        Me.txtfechapres.Location = New System.Drawing.Point(423, 19)
        Me.txtfechapres.MaxLength = 10
        Me.txtfechapres.Name = "txtfechapres"
        Me.txtfechapres.Size = New System.Drawing.Size(100, 20)
        Me.txtfechapres.TabIndex = 4
        '
        'txtfecha
        '
        Me.txtfecha.Location = New System.Drawing.Point(287, 19)
        Me.txtfecha.MaxLength = 10
        Me.txtfecha.Name = "txtfecha"
        Me.txtfecha.Size = New System.Drawing.Size(68, 20)
        Me.txtfecha.TabIndex = 2
        '
        'txtfechaprox
        '
        Me.txtfechaprox.Location = New System.Drawing.Point(559, 19)
        Me.txtfechaprox.MaxLength = 10
        Me.txtfechaprox.Name = "txtfechaprox"
        Me.txtfechaprox.Size = New System.Drawing.Size(100, 20)
        Me.txtfechaprox.TabIndex = 5
        '
        'btnconfirmar
        '
        Me.btnconfirmar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnconfirmar.BackColor = System.Drawing.SystemColors.Control
        Me.btnconfirmar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnconfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnconfirmar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnconfirmar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnconfirmar.Location = New System.Drawing.Point(232, 340)
        Me.btnconfirmar.Name = "btnconfirmar"
        Me.btnconfirmar.Size = New System.Drawing.Size(135, 25)
        Me.btnconfirmar.TabIndex = 24
        Me.btnconfirmar.Text = "&CONFIRMAR"
        Me.btnconfirmar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancelar.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(373, 340)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(126, 25)
        Me.btnCancelar.TabIndex = 23
        Me.btnCancelar.Text = "&CANCELAR"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer), CType(CType(90, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(370, 23)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(38, 15)
        Me.Label6.TabIndex = 25
        Me.Label6.Text = "Hora"
        '
        'cmbJuego
        '
        Me.cmbJuego.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cmbJuego.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJuego.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbJuego.FormattingEnabled = True
        Me.cmbJuego.Location = New System.Drawing.Point(11, 17)
        Me.cmbJuego.Name = "cmbJuego"
        Me.cmbJuego.Size = New System.Drawing.Size(159, 26)
        Me.cmbJuego.TabIndex = 29
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txthora)
        Me.GroupBox1.Controls.Add(Me.txtfechaprox)
        Me.GroupBox1.Controls.Add(Me.txtsorteo)
        Me.GroupBox1.Controls.Add(Me.txtfechapres)
        Me.GroupBox1.Controls.Add(Me.txtfecha)
        Me.GroupBox1.Controls.Add(Me.cmbJuego)
        Me.GroupBox1.Location = New System.Drawing.Point(12, 41)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(685, 57)
        Me.GroupBox1.TabIndex = 34
        Me.GroupBox1.TabStop = False
        '
        'txthora
        '
        Me.txthora.Location = New System.Drawing.Point(361, 19)
        Me.txthora.Mask = "00:00"
        Me.txthora.Name = "txthora"
        Me.txthora.Size = New System.Drawing.Size(42, 20)
        Me.txthora.TabIndex = 3
        Me.txthora.ValidatingType = GetType(Date)
        '
        'GroupBox2
        '
        Me.GroupBox2.Controls.Add(Me.txthora2)
        Me.GroupBox2.Controls.Add(Me.cmbJuego2)
        Me.GroupBox2.Controls.Add(Me.txtsorteo2)
        Me.GroupBox2.Controls.Add(Me.txtfechaprox2)
        Me.GroupBox2.Controls.Add(Me.txtfechapres2)
        Me.GroupBox2.Controls.Add(Me.txtfecha2)
        Me.GroupBox2.Location = New System.Drawing.Point(12, 109)
        Me.GroupBox2.Name = "GroupBox2"
        Me.GroupBox2.Size = New System.Drawing.Size(685, 57)
        Me.GroupBox2.TabIndex = 35
        Me.GroupBox2.TabStop = False
        '
        'txthora2
        '
        Me.txthora2.Location = New System.Drawing.Point(361, 19)
        Me.txthora2.Mask = "00:00"
        Me.txthora2.Name = "txthora2"
        Me.txthora2.Size = New System.Drawing.Size(42, 20)
        Me.txthora2.TabIndex = 8
        Me.txthora2.ValidatingType = GetType(Date)
        '
        'cmbJuego2
        '
        Me.cmbJuego2.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cmbJuego2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJuego2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbJuego2.FormattingEnabled = True
        Me.cmbJuego2.Location = New System.Drawing.Point(11, 19)
        Me.cmbJuego2.Name = "cmbJuego2"
        Me.cmbJuego2.Size = New System.Drawing.Size(159, 26)
        Me.cmbJuego2.TabIndex = 41
        '
        'txtsorteo2
        '
        Me.txtsorteo2.Location = New System.Drawing.Point(187, 19)
        Me.txtsorteo2.MaxLength = 7
        Me.txtsorteo2.Name = "txtsorteo2"
        Me.txtsorteo2.Size = New System.Drawing.Size(76, 20)
        Me.txtsorteo2.TabIndex = 6
        '
        'txtfechaprox2
        '
        Me.txtfechaprox2.Location = New System.Drawing.Point(559, 19)
        Me.txtfechaprox2.MaxLength = 10
        Me.txtfechaprox2.Name = "txtfechaprox2"
        Me.txtfechaprox2.Size = New System.Drawing.Size(100, 20)
        Me.txtfechaprox2.TabIndex = 10
        '
        'txtfechapres2
        '
        Me.txtfechapres2.Location = New System.Drawing.Point(423, 19)
        Me.txtfechapres2.MaxLength = 10
        Me.txtfechapres2.Name = "txtfechapres2"
        Me.txtfechapres2.Size = New System.Drawing.Size(100, 20)
        Me.txtfechapres2.TabIndex = 9
        '
        'txtfecha2
        '
        Me.txtfecha2.Location = New System.Drawing.Point(287, 19)
        Me.txtfecha2.MaxLength = 10
        Me.txtfecha2.Name = "txtfecha2"
        Me.txtfecha2.Size = New System.Drawing.Size(68, 20)
        Me.txtfecha2.TabIndex = 7
        '
        'GroupBox3
        '
        Me.GroupBox3.Controls.Add(Me.txthora3)
        Me.GroupBox3.Controls.Add(Me.cmbJuego3)
        Me.GroupBox3.Controls.Add(Me.txtfechaprox3)
        Me.GroupBox3.Controls.Add(Me.txtfecha3)
        Me.GroupBox3.Controls.Add(Me.txtfechapres3)
        Me.GroupBox3.Controls.Add(Me.txtsorteo3)
        Me.GroupBox3.Location = New System.Drawing.Point(12, 184)
        Me.GroupBox3.Name = "GroupBox3"
        Me.GroupBox3.Size = New System.Drawing.Size(685, 57)
        Me.GroupBox3.TabIndex = 36
        Me.GroupBox3.TabStop = False
        '
        'txthora3
        '
        Me.txthora3.Location = New System.Drawing.Point(361, 17)
        Me.txthora3.Mask = "00:00"
        Me.txthora3.Name = "txthora3"
        Me.txthora3.Size = New System.Drawing.Size(42, 20)
        Me.txthora3.TabIndex = 13
        Me.txthora3.ValidatingType = GetType(Date)
        '
        'cmbJuego3
        '
        Me.cmbJuego3.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cmbJuego3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJuego3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbJuego3.FormattingEnabled = True
        Me.cmbJuego3.Location = New System.Drawing.Point(11, 17)
        Me.cmbJuego3.Name = "cmbJuego3"
        Me.cmbJuego3.Size = New System.Drawing.Size(159, 26)
        Me.cmbJuego3.TabIndex = 36
        '
        'txtfechaprox3
        '
        Me.txtfechaprox3.Location = New System.Drawing.Point(559, 19)
        Me.txtfechaprox3.MaxLength = 10
        Me.txtfechaprox3.Name = "txtfechaprox3"
        Me.txtfechaprox3.Size = New System.Drawing.Size(100, 20)
        Me.txtfechaprox3.TabIndex = 15
        '
        'txtfecha3
        '
        Me.txtfecha3.Location = New System.Drawing.Point(287, 17)
        Me.txtfecha3.MaxLength = 10
        Me.txtfecha3.Name = "txtfecha3"
        Me.txtfecha3.Size = New System.Drawing.Size(68, 20)
        Me.txtfecha3.TabIndex = 12
        '
        'txtfechapres3
        '
        Me.txtfechapres3.Location = New System.Drawing.Point(423, 17)
        Me.txtfechapres3.MaxLength = 10
        Me.txtfechapres3.Name = "txtfechapres3"
        Me.txtfechapres3.Size = New System.Drawing.Size(100, 20)
        Me.txtfechapres3.TabIndex = 14
        '
        'txtsorteo3
        '
        Me.txtsorteo3.Location = New System.Drawing.Point(187, 17)
        Me.txtsorteo3.MaxLength = 7
        Me.txtsorteo3.Name = "txtsorteo3"
        Me.txtsorteo3.Size = New System.Drawing.Size(76, 20)
        Me.txtsorteo3.TabIndex = 11
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.txthora4)
        Me.GroupBox4.Controls.Add(Me.cmbJuego4)
        Me.GroupBox4.Controls.Add(Me.txtfechaprox4)
        Me.GroupBox4.Controls.Add(Me.txtfecha4)
        Me.GroupBox4.Controls.Add(Me.txtfechapres4)
        Me.GroupBox4.Controls.Add(Me.txtsorteo4)
        Me.GroupBox4.Location = New System.Drawing.Point(12, 247)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(685, 57)
        Me.GroupBox4.TabIndex = 37
        Me.GroupBox4.TabStop = False
        '
        'txthora4
        '
        Me.txthora4.Location = New System.Drawing.Point(361, 17)
        Me.txthora4.Mask = "00:00"
        Me.txthora4.Name = "txthora4"
        Me.txthora4.Size = New System.Drawing.Size(42, 20)
        Me.txthora4.TabIndex = 18
        Me.txthora4.ValidatingType = GetType(Date)
        '
        'cmbJuego4
        '
        Me.cmbJuego4.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cmbJuego4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJuego4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbJuego4.FormattingEnabled = True
        Me.cmbJuego4.Location = New System.Drawing.Point(11, 17)
        Me.cmbJuego4.Name = "cmbJuego4"
        Me.cmbJuego4.Size = New System.Drawing.Size(159, 26)
        Me.cmbJuego4.TabIndex = 36
        '
        'txtfechaprox4
        '
        Me.txtfechaprox4.Location = New System.Drawing.Point(559, 19)
        Me.txtfechaprox4.MaxLength = 10
        Me.txtfechaprox4.Name = "txtfechaprox4"
        Me.txtfechaprox4.Size = New System.Drawing.Size(100, 20)
        Me.txtfechaprox4.TabIndex = 20
        '
        'txtfecha4
        '
        Me.txtfecha4.Location = New System.Drawing.Point(287, 17)
        Me.txtfecha4.MaxLength = 10
        Me.txtfecha4.Name = "txtfecha4"
        Me.txtfecha4.Size = New System.Drawing.Size(68, 20)
        Me.txtfecha4.TabIndex = 17
        '
        'txtfechapres4
        '
        Me.txtfechapres4.Location = New System.Drawing.Point(423, 17)
        Me.txtfechapres4.MaxLength = 10
        Me.txtfechapres4.Name = "txtfechapres4"
        Me.txtfechapres4.Size = New System.Drawing.Size(100, 20)
        Me.txtfechapres4.TabIndex = 19
        '
        'txtsorteo4
        '
        Me.txtsorteo4.Location = New System.Drawing.Point(187, 17)
        Me.txtsorteo4.MaxLength = 7
        Me.txtsorteo4.Name = "txtsorteo4"
        Me.txtsorteo4.Size = New System.Drawing.Size(76, 20)
        Me.txtsorteo4.TabIndex = 16
        '
        'FrmcontingenciaWS
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(730, 377)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox4)
        Me.Controls.Add(Me.GroupBox3)
        Me.Controls.Add(Me.GroupBox2)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnconfirmar)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Name = "FrmcontingenciaWS"
        Me.Text = "Contingencia Carga de sorteo "
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.GroupBox2.ResumeLayout(False)
        Me.GroupBox2.PerformLayout()
        Me.GroupBox3.ResumeLayout(False)
        Me.GroupBox3.PerformLayout()
        Me.GroupBox4.ResumeLayout(False)
        Me.GroupBox4.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents txtsorteo As System.Windows.Forms.TextBox
    Friend WithEvents txtfechapres As System.Windows.Forms.TextBox
    Friend WithEvents txtfecha As System.Windows.Forms.TextBox
    Friend WithEvents txtfechaprox As System.Windows.Forms.TextBox
    Friend WithEvents btnconfirmar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents cmbJuego As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox2 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbJuego2 As System.Windows.Forms.ComboBox
    Friend WithEvents txtsorteo2 As System.Windows.Forms.TextBox
    Friend WithEvents txtfechaprox2 As System.Windows.Forms.TextBox
    Friend WithEvents txtfechapres2 As System.Windows.Forms.TextBox
    Friend WithEvents txtfecha2 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox3 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbJuego3 As System.Windows.Forms.ComboBox
    Friend WithEvents txtfechaprox3 As System.Windows.Forms.TextBox
    Friend WithEvents txtfecha3 As System.Windows.Forms.TextBox
    Friend WithEvents txtfechapres3 As System.Windows.Forms.TextBox
    Friend WithEvents txtsorteo3 As System.Windows.Forms.TextBox
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents cmbJuego4 As System.Windows.Forms.ComboBox
    Friend WithEvents txtfechaprox4 As System.Windows.Forms.TextBox
    Friend WithEvents txtfecha4 As System.Windows.Forms.TextBox
    Friend WithEvents txtfechapres4 As System.Windows.Forms.TextBox
    Friend WithEvents txtsorteo4 As System.Windows.Forms.TextBox
    Friend WithEvents txthora As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txthora2 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txthora3 As System.Windows.Forms.MaskedTextBox
    Friend WithEvents txthora4 As System.Windows.Forms.MaskedTextBox
End Class
