<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPozoSugerido
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPozoSugerido))
        Me.GrbLogin = New System.Windows.Forms.GroupBox
        Me.lblerror = New System.Windows.Forms.Label
        Me.Btncancelarlogin = New System.Windows.Forms.Button
        Me.btnIngresar = New System.Windows.Forms.Button
        Me.txtpwd = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtUsuario = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtfecha = New System.Windows.Forms.TextBox
        Me.TxtAnterior = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.Btncancelarpozo = New System.Windows.Forms.Button
        Me.txtPozosugerido = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnGuardarPozo = New System.Windows.Forms.Button
        Me.GrbLogin.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrbLogin
        '
        Me.GrbLogin.Controls.Add(Me.lblerror)
        Me.GrbLogin.Controls.Add(Me.Btncancelarlogin)
        Me.GrbLogin.Controls.Add(Me.btnIngresar)
        Me.GrbLogin.Controls.Add(Me.txtpwd)
        Me.GrbLogin.Controls.Add(Me.Label1)
        Me.GrbLogin.Controls.Add(Me.txtUsuario)
        Me.GrbLogin.Controls.Add(Me.Label2)
        Me.GrbLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrbLogin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.GrbLogin.Location = New System.Drawing.Point(12, 12)
        Me.GrbLogin.Name = "GrbLogin"
        Me.GrbLogin.Size = New System.Drawing.Size(369, 136)
        Me.GrbLogin.TabIndex = 25
        Me.GrbLogin.TabStop = False
        Me.GrbLogin.Text = "Sólo Usuarios Autorizados"
        '
        'lblerror
        '
        Me.lblerror.AutoSize = True
        Me.lblerror.ForeColor = System.Drawing.Color.Red
        Me.lblerror.Location = New System.Drawing.Point(117, 20)
        Me.lblerror.Name = "lblerror"
        Me.lblerror.Size = New System.Drawing.Size(250, 16)
        Me.lblerror.TabIndex = 31
        Me.lblerror.Text = "Usuario y/o contraseña incorrectos"
        Me.lblerror.Visible = False
        '
        'Btncancelarlogin
        '
        Me.Btncancelarlogin.BackColor = System.Drawing.SystemColors.Control
        Me.Btncancelarlogin.BackgroundImage = CType(resources.GetObject("Btncancelarlogin.BackgroundImage"), System.Drawing.Image)
        Me.Btncancelarlogin.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Btncancelarlogin.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btncancelarlogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btncancelarlogin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.Btncancelarlogin.Location = New System.Drawing.Point(227, 100)
        Me.Btncancelarlogin.Name = "Btncancelarlogin"
        Me.Btncancelarlogin.Size = New System.Drawing.Size(101, 30)
        Me.Btncancelarlogin.TabIndex = 30
        Me.Btncancelarlogin.Text = "Cancelar"
        Me.Btncancelarlogin.UseVisualStyleBackColor = False
        '
        'btnIngresar
        '
        Me.btnIngresar.BackColor = System.Drawing.SystemColors.Control
        Me.btnIngresar.BackgroundImage = CType(resources.GetObject("btnIngresar.BackgroundImage"), System.Drawing.Image)
        Me.btnIngresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIngresar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIngresar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnIngresar.Location = New System.Drawing.Point(120, 100)
        Me.btnIngresar.Name = "btnIngresar"
        Me.btnIngresar.Size = New System.Drawing.Size(101, 30)
        Me.btnIngresar.TabIndex = 29
        Me.btnIngresar.Text = "Ingresar"
        Me.btnIngresar.UseVisualStyleBackColor = False
        '
        'txtpwd
        '
        Me.txtpwd.AcceptsTab = True
        Me.txtpwd.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpwd.Location = New System.Drawing.Point(120, 69)
        Me.txtpwd.MaxLength = 255
        Me.txtpwd.Name = "txtpwd"
        Me.txtpwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpwd.Size = New System.Drawing.Size(226, 24)
        Me.txtpwd.TabIndex = 28
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(22, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 18)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Contraseña"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUsuario
        '
        Me.txtUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuario.Location = New System.Drawing.Point(120, 38)
        Me.txtUsuario.MaxLength = 255
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(226, 24)
        Me.txtUsuario.TabIndex = 26
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(48, 41)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(67, 18)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Usuario"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtfecha)
        Me.GroupBox1.Controls.Add(Me.TxtAnterior)
        Me.GroupBox1.Controls.Add(Me.Label4)
        Me.GroupBox1.Controls.Add(Me.Btncancelarpozo)
        Me.GroupBox1.Controls.Add(Me.txtPozosugerido)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.btnGuardarPozo)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 154)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(369, 136)
        Me.GroupBox1.TabIndex = 26
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Pozo Sugerido"
        '
        'txtfecha
        '
        Me.txtfecha.Enabled = False
        Me.txtfecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtfecha.Location = New System.Drawing.Point(261, 25)
        Me.txtfecha.MaxLength = 15
        Me.txtfecha.Name = "txtfecha"
        Me.txtfecha.Size = New System.Drawing.Size(85, 24)
        Me.txtfecha.TabIndex = 30
        Me.txtfecha.Text = "12/12/2012"
        '
        'TxtAnterior
        '
        Me.TxtAnterior.Enabled = False
        Me.TxtAnterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TxtAnterior.Location = New System.Drawing.Point(120, 25)
        Me.TxtAnterior.MaxLength = 15
        Me.TxtAnterior.Name = "TxtAnterior"
        Me.TxtAnterior.Size = New System.Drawing.Size(135, 24)
        Me.TxtAnterior.TabIndex = 29
        Me.TxtAnterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(42, 30)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(72, 18)
        Me.Label4.TabIndex = 28
        Me.Label4.Text = "Anterior:"
        '
        'Btncancelarpozo
        '
        Me.Btncancelarpozo.BackColor = System.Drawing.SystemColors.Control
        Me.Btncancelarpozo.BackgroundImage = CType(resources.GetObject("Btncancelarpozo.BackgroundImage"), System.Drawing.Image)
        Me.Btncancelarpozo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Btncancelarpozo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.Btncancelarpozo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Btncancelarpozo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.Btncancelarpozo.Location = New System.Drawing.Point(227, 91)
        Me.Btncancelarpozo.Name = "Btncancelarpozo"
        Me.Btncancelarpozo.Size = New System.Drawing.Size(101, 30)
        Me.Btncancelarpozo.TabIndex = 27
        Me.Btncancelarpozo.Text = "Cancelar"
        Me.Btncancelarpozo.UseVisualStyleBackColor = False
        '
        'txtPozosugerido
        '
        Me.txtPozosugerido.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPozosugerido.Location = New System.Drawing.Point(120, 56)
        Me.txtPozosugerido.MaxLength = 15
        Me.txtPozosugerido.Name = "txtPozosugerido"
        Me.txtPozosugerido.Size = New System.Drawing.Size(135, 24)
        Me.txtPozosugerido.TabIndex = 26
        Me.txtPozosugerido.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(42, 59)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(70, 18)
        Me.Label3.TabIndex = 25
        Me.Label3.Text = "Importe:"
        '
        'btnGuardarPozo
        '
        Me.btnGuardarPozo.BackColor = System.Drawing.SystemColors.Control
        Me.btnGuardarPozo.BackgroundImage = CType(resources.GetObject("btnGuardarPozo.BackgroundImage"), System.Drawing.Image)
        Me.btnGuardarPozo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGuardarPozo.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGuardarPozo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardarPozo.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnGuardarPozo.Location = New System.Drawing.Point(120, 91)
        Me.btnGuardarPozo.Name = "btnGuardarPozo"
        Me.btnGuardarPozo.Size = New System.Drawing.Size(101, 30)
        Me.btnGuardarPozo.TabIndex = 24
        Me.btnGuardarPozo.Text = "Guardar"
        Me.btnGuardarPozo.UseVisualStyleBackColor = False
        '
        'frmPozoSugerido
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(391, 298)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.GrbLogin)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmPozoSugerido"
        Me.Text = "Ingreso de pozo próximo sorteo"
        Me.GrbLogin.ResumeLayout(False)
        Me.GrbLogin.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GrbLogin As System.Windows.Forms.GroupBox
    Friend WithEvents Btncancelarlogin As System.Windows.Forms.Button
    Friend WithEvents btnIngresar As System.Windows.Forms.Button
    Friend WithEvents txtpwd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents Btncancelarpozo As System.Windows.Forms.Button
    Friend WithEvents txtPozosugerido As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnGuardarPozo As System.Windows.Forms.Button
    Friend WithEvents lblerror As System.Windows.Forms.Label
    Friend WithEvents txtfecha As System.Windows.Forms.TextBox
    Friend WithEvents TxtAnterior As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
End Class
