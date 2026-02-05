<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAnularSorteo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAnularSorteo))
        Me.Grplogin = New System.Windows.Forms.GroupBox
        Me.txtusuario = New System.Windows.Forms.TextBox
        Me.Label6 = New System.Windows.Forms.Label
        Me.lblerror = New System.Windows.Forms.Label
        Me.Btncancelarlogin = New System.Windows.Forms.Button
        Me.btnIngresar = New System.Windows.Forms.Button
        Me.txtpwd = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.grpAnular = New System.Windows.Forms.GroupBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.cboExtractos = New System.Windows.Forms.ComboBox
        Me.Label4 = New System.Windows.Forms.Label
        Me.cboConcurso = New System.Windows.Forms.ComboBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.btnAnular = New System.Windows.Forms.Button
        Me.lblpublicando = New System.Windows.Forms.Label
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.txtmotivo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Grplogin.SuspendLayout()
        Me.grpAnular.SuspendLayout()
        Me.SuspendLayout()
        '
        'Grplogin
        '
        Me.Grplogin.Controls.Add(Me.txtusuario)
        Me.Grplogin.Controls.Add(Me.Label6)
        Me.Grplogin.Controls.Add(Me.lblerror)
        Me.Grplogin.Controls.Add(Me.Btncancelarlogin)
        Me.Grplogin.Controls.Add(Me.btnIngresar)
        Me.Grplogin.Controls.Add(Me.txtpwd)
        Me.Grplogin.Controls.Add(Me.Label1)
        Me.Grplogin.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Grplogin.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Grplogin.Location = New System.Drawing.Point(12, 3)
        Me.Grplogin.Name = "Grplogin"
        Me.Grplogin.Size = New System.Drawing.Size(481, 141)
        Me.Grplogin.TabIndex = 0
        Me.Grplogin.TabStop = False
        Me.Grplogin.Text = "Sólo Usuarios Autorizados"
        '
        'txtusuario
        '
        Me.txtusuario.AcceptsTab = True
        Me.txtusuario.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtusuario.Location = New System.Drawing.Point(116, 36)
        Me.txtusuario.MaxLength = 255
        Me.txtusuario.Name = "txtusuario"
        Me.txtusuario.Size = New System.Drawing.Size(256, 24)
        Me.txtusuario.TabIndex = 1
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label6.Location = New System.Drawing.Point(45, 42)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(67, 18)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Usuario"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblerror
        '
        Me.lblerror.AutoSize = True
        Me.lblerror.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblerror.ForeColor = System.Drawing.Color.Red
        Me.lblerror.Location = New System.Drawing.Point(193, 19)
        Me.lblerror.Name = "lblerror"
        Me.lblerror.Size = New System.Drawing.Size(0, 16)
        Me.lblerror.TabIndex = 36
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
        Me.Btncancelarlogin.Location = New System.Drawing.Point(266, 99)
        Me.Btncancelarlogin.Name = "Btncancelarlogin"
        Me.Btncancelarlogin.Size = New System.Drawing.Size(101, 30)
        Me.Btncancelarlogin.TabIndex = 4
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
        Me.btnIngresar.Location = New System.Drawing.Point(159, 99)
        Me.btnIngresar.Name = "btnIngresar"
        Me.btnIngresar.Size = New System.Drawing.Size(101, 30)
        Me.btnIngresar.TabIndex = 3
        Me.btnIngresar.Text = "Ingresar"
        Me.btnIngresar.UseVisualStyleBackColor = False
        '
        'txtpwd
        '
        Me.txtpwd.AcceptsTab = True
        Me.txtpwd.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtpwd.Location = New System.Drawing.Point(117, 69)
        Me.txtpwd.MaxLength = 255
        Me.txtpwd.Name = "txtpwd"
        Me.txtpwd.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtpwd.Size = New System.Drawing.Size(255, 24)
        Me.txtpwd.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(17, 72)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(95, 18)
        Me.Label1.TabIndex = 32
        Me.Label1.Text = "Contraseña"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'grpAnular
        '
        Me.grpAnular.Controls.Add(Me.Label3)
        Me.grpAnular.Controls.Add(Me.cboExtractos)
        Me.grpAnular.Controls.Add(Me.Label4)
        Me.grpAnular.Controls.Add(Me.cboConcurso)
        Me.grpAnular.Controls.Add(Me.Label5)
        Me.grpAnular.Controls.Add(Me.btnAnular)
        Me.grpAnular.Controls.Add(Me.lblpublicando)
        Me.grpAnular.Controls.Add(Me.btnCancelar)
        Me.grpAnular.Controls.Add(Me.txtmotivo)
        Me.grpAnular.Controls.Add(Me.Label2)
        Me.grpAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grpAnular.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.grpAnular.Location = New System.Drawing.Point(12, 150)
        Me.grpAnular.Name = "grpAnular"
        Me.grpAnular.Size = New System.Drawing.Size(481, 243)
        Me.grpAnular.TabIndex = 1
        Me.grpAnular.TabStop = False
        Me.grpAnular.Text = "Revertir"
        '
        'Label3
        '
        Me.Label3.FlatStyle = System.Windows.Forms.FlatStyle.System
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(6, 152)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(469, 22)
        Me.Label3.TabIndex = 46
        Me.Label3.Text = "(*) Elija ""TODOS"" si necesita modificar el Sorteo propio, o un ""Extracto Particul" & _
            "ar"" si sólo necesita modificar una Jurisdicción o los Premios de un juego partic" & _
            "ular."
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label3.Visible = False
        '
        'cboExtractos
        '
        Me.cboExtractos.BackColor = System.Drawing.Color.White
        Me.cboExtractos.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboExtractos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboExtractos.FormattingEnabled = True
        Me.cboExtractos.ItemHeight = 18
        Me.cboExtractos.Location = New System.Drawing.Point(116, 57)
        Me.cboExtractos.Name = "cboExtractos"
        Me.cboExtractos.Size = New System.Drawing.Size(351, 26)
        Me.cboExtractos.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label4.Location = New System.Drawing.Point(15, 60)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(95, 18)
        Me.Label4.TabIndex = 45
        Me.Label4.Text = "Extracto (*)"
        '
        'cboConcurso
        '
        Me.cboConcurso.BackColor = System.Drawing.Color.White
        Me.cboConcurso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboConcurso.FormattingEnabled = True
        Me.cboConcurso.ItemHeight = 18
        Me.cboConcurso.Location = New System.Drawing.Point(117, 26)
        Me.cboConcurso.Name = "cboConcurso"
        Me.cboConcurso.Size = New System.Drawing.Size(351, 26)
        Me.cboConcurso.TabIndex = 5
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label5.Location = New System.Drawing.Point(51, 29)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(59, 18)
        Me.Label5.TabIndex = 43
        Me.Label5.Text = "Sorteo"
        '
        'btnAnular
        '
        Me.btnAnular.BackColor = System.Drawing.SystemColors.Control
        Me.btnAnular.BackgroundImage = CType(resources.GetObject("btnAnular.BackgroundImage"), System.Drawing.Image)
        Me.btnAnular.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAnular.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnular.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnAnular.Location = New System.Drawing.Point(159, 200)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.Size = New System.Drawing.Size(101, 34)
        Me.btnAnular.TabIndex = 8
        Me.btnAnular.Text = "Revertir"
        Me.btnAnular.UseVisualStyleBackColor = False
        '
        'lblpublicando
        '
        Me.lblpublicando.AutoSize = True
        Me.lblpublicando.BackColor = System.Drawing.Color.Transparent
        Me.lblpublicando.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpublicando.ForeColor = System.Drawing.Color.Red
        Me.lblpublicando.Location = New System.Drawing.Point(186, 179)
        Me.lblpublicando.Name = "lblpublicando"
        Me.lblpublicando.Size = New System.Drawing.Size(108, 18)
        Me.lblpublicando.TabIndex = 39
        Me.lblpublicando.Text = "Revirtiendo..."
        Me.lblpublicando.Visible = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.BackgroundImage = CType(resources.GetObject("btnCancelar.BackgroundImage"), System.Drawing.Image)
        Me.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(266, 200)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(101, 34)
        Me.btnCancelar.TabIndex = 9
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'txtmotivo
        '
        Me.txtmotivo.AcceptsTab = True
        Me.txtmotivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtmotivo.Location = New System.Drawing.Point(116, 89)
        Me.txtmotivo.MaxLength = 255
        Me.txtmotivo.Multiline = True
        Me.txtmotivo.Name = "txtmotivo"
        Me.txtmotivo.Size = New System.Drawing.Size(351, 60)
        Me.txtmotivo.TabIndex = 7
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(51, 89)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(59, 18)
        Me.Label2.TabIndex = 36
        Me.Label2.Text = "Motivo"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FrmAnularSorteo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(505, 400)
        Me.Controls.Add(Me.grpAnular)
        Me.Controls.Add(Me.Grplogin)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAnularSorteo"
        Me.Text = "Revertir Concurso"
        Me.Grplogin.ResumeLayout(False)
        Me.Grplogin.PerformLayout()
        Me.grpAnular.ResumeLayout(False)
        Me.grpAnular.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Grplogin As System.Windows.Forms.GroupBox
    Friend WithEvents lblerror As System.Windows.Forms.Label
    Friend WithEvents Btncancelarlogin As System.Windows.Forms.Button
    Friend WithEvents btnIngresar As System.Windows.Forms.Button
    Friend WithEvents txtpwd As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents grpAnular As System.Windows.Forms.GroupBox
    Friend WithEvents txtmotivo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cboConcurso As System.Windows.Forms.ComboBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents btnAnular As System.Windows.Forms.Button
    Friend WithEvents lblpublicando As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents cboExtractos As System.Windows.Forms.ComboBox
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents Label6 As System.Windows.Forms.Label
    Friend WithEvents txtusuario As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
