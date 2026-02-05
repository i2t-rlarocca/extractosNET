<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCambioPWD
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCambioPWD))
        Me.GrbLogin = New System.Windows.Forms.GroupBox
        Me.txtPwdN2 = New System.Windows.Forms.TextBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.txtPwdN = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.lblerror = New System.Windows.Forms.Label
        Me.BtnCancelar = New System.Windows.Forms.Button
        Me.btnIngresar = New System.Windows.Forms.Button
        Me.txtpwd = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.txtUsuario = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.GrbLogin.SuspendLayout()
        Me.SuspendLayout()
        '
        'GrbLogin
        '
        Me.GrbLogin.Controls.Add(Me.txtPwdN2)
        Me.GrbLogin.Controls.Add(Me.Label4)
        Me.GrbLogin.Controls.Add(Me.txtPwdN)
        Me.GrbLogin.Controls.Add(Me.Label3)
        Me.GrbLogin.Controls.Add(Me.lblerror)
        Me.GrbLogin.Controls.Add(Me.BtnCancelar)
        Me.GrbLogin.Controls.Add(Me.btnIngresar)
        Me.GrbLogin.Controls.Add(Me.txtpwd)
        Me.GrbLogin.Controls.Add(Me.Label1)
        Me.GrbLogin.Controls.Add(Me.txtUsuario)
        Me.GrbLogin.Controls.Add(Me.Label2)
        Me.GrbLogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GrbLogin.Location = New System.Drawing.Point(0, 3)
        Me.GrbLogin.Name = "GrbLogin"
        Me.GrbLogin.Size = New System.Drawing.Size(481, 220)
        Me.GrbLogin.TabIndex = 26
        Me.GrbLogin.TabStop = False
        '
        'txtPwdN2
        '
        Me.txtPwdN2.AcceptsTab = True
        Me.txtPwdN2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPwdN2.Location = New System.Drawing.Point(188, 148)
        Me.txtPwdN2.MaxLength = 255
        Me.txtPwdN2.Name = "txtPwdN2"
        Me.txtPwdN2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwdN2.Size = New System.Drawing.Size(226, 24)
        Me.txtPwdN2.TabIndex = 4
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(34, 148)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(141, 18)
        Me.Label4.TabIndex = 34
        Me.Label4.Text = "Reingrese Nueva:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtPwdN
        '
        Me.txtPwdN.AcceptsTab = True
        Me.txtPwdN.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPwdN.Location = New System.Drawing.Point(188, 118)
        Me.txtPwdN.MaxLength = 255
        Me.txtPwdN.Name = "txtPwdN"
        Me.txtPwdN.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPwdN.Size = New System.Drawing.Size(226, 24)
        Me.txtPwdN.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(23, 118)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(152, 18)
        Me.Label3.TabIndex = 32
        Me.Label3.Text = "Contraseña Nueva:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblerror
        '
        Me.lblerror.AutoSize = True
        Me.lblerror.ForeColor = System.Drawing.Color.Red
        Me.lblerror.Location = New System.Drawing.Point(6, 18)
        Me.lblerror.Name = "lblerror"
        Me.lblerror.Size = New System.Drawing.Size(120, 16)
        Me.lblerror.TabIndex = 31
        Me.lblerror.Text = "                            "
        Me.lblerror.Visible = False
        '
        'BtnCancelar
        '
        Me.BtnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.BtnCancelar.BackgroundImage = CType(resources.GetObject("BtnCancelar.BackgroundImage"), System.Drawing.Image)
        Me.BtnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.BtnCancelar.Location = New System.Drawing.Point(229, 178)
        Me.BtnCancelar.Name = "BtnCancelar"
        Me.BtnCancelar.Size = New System.Drawing.Size(101, 36)
        Me.BtnCancelar.TabIndex = 6
        Me.BtnCancelar.Text = "Cancelar"
        Me.BtnCancelar.UseVisualStyleBackColor = False
        '
        'btnIngresar
        '
        Me.btnIngresar.BackColor = System.Drawing.SystemColors.Control
        Me.btnIngresar.BackgroundImage = CType(resources.GetObject("btnIngresar.BackgroundImage"), System.Drawing.Image)
        Me.btnIngresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnIngresar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnIngresar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIngresar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnIngresar.Location = New System.Drawing.Point(122, 178)
        Me.btnIngresar.Name = "btnIngresar"
        Me.btnIngresar.Size = New System.Drawing.Size(101, 36)
        Me.btnIngresar.TabIndex = 5
        Me.btnIngresar.Text = "Aplicar"
        Me.btnIngresar.UseVisualStyleBackColor = False
        '
        'txtpwd
        '
        Me.txtpwd.AcceptsTab = True
        Me.txtpwd.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpwd.Location = New System.Drawing.Point(188, 75)
        Me.txtpwd.MaxLength = 255
        Me.txtpwd.Name = "txtpwd"
        Me.txtpwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpwd.Size = New System.Drawing.Size(226, 24)
        Me.txtpwd.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(24, 75)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(151, 18)
        Me.Label1.TabIndex = 27
        Me.Label1.Text = "Contraseña Actual:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUsuario
        '
        Me.txtUsuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuario.Location = New System.Drawing.Point(188, 44)
        Me.txtUsuario.MaxLength = 255
        Me.txtUsuario.Name = "txtUsuario"
        Me.txtUsuario.Size = New System.Drawing.Size(226, 24)
        Me.txtUsuario.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(103, 44)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(72, 18)
        Me.Label2.TabIndex = 25
        Me.Label2.Text = "Usuario:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FrmCambioPWD
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(486, 224)
        Me.Controls.Add(Me.GrbLogin)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCambioPWD"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cambio de Contraseña"
        Me.GrbLogin.ResumeLayout(False)
        Me.GrbLogin.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GrbLogin As System.Windows.Forms.GroupBox
    Friend WithEvents lblerror As System.Windows.Forms.Label
    Friend WithEvents BtnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnIngresar As System.Windows.Forms.Button
    Friend WithEvents txtpwd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtUsuario As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents txtPwdN2 As System.Windows.Forms.TextBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents txtPwdN As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
