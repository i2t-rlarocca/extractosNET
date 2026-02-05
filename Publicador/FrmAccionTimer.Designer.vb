<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAccionTimer
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAccionTimer))
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtleyenda = New System.Windows.Forms.TextBox
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnAvisarMastarde = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtleyenda)
        Me.GroupBox1.Controls.Add(Me.btnAceptar)
        Me.GroupBox1.Controls.Add(Me.btnAvisarMastarde)
        Me.GroupBox1.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(4, 2)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(465, 100)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Confirmación de recepción"
        '
        'txtleyenda
        '
        Me.txtleyenda.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.txtleyenda.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.txtleyenda.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtleyenda.ForeColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.txtleyenda.Location = New System.Drawing.Point(9, 22)
        Me.txtleyenda.Multiline = True
        Me.txtleyenda.Name = "txtleyenda"
        Me.txtleyenda.Size = New System.Drawing.Size(450, 38)
        Me.txtleyenda.TabIndex = 29
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.SystemColors.Control
        Me.btnAceptar.BackgroundImage = CType(resources.GetObject("btnAceptar.BackgroundImage"), System.Drawing.Image)
        Me.btnAceptar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAceptar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAceptar.Font = New System.Drawing.Font("Microsoft Sans Serif", 6.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnAceptar.Location = New System.Drawing.Point(111, 66)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(107, 26)
        Me.btnAceptar.TabIndex = 28
        Me.btnAceptar.Text = "&ACEPTAR"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'btnAvisarMastarde
        '
        Me.btnAvisarMastarde.BackColor = System.Drawing.SystemColors.Control
        Me.btnAvisarMastarde.BackgroundImage = CType(resources.GetObject("btnAvisarMastarde.BackgroundImage"), System.Drawing.Image)
        Me.btnAvisarMastarde.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAvisarMastarde.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAvisarMastarde.Font = New System.Drawing.Font("Myriad Web Pro", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAvisarMastarde.ForeColor = System.Drawing.Color.FromArgb(CType(CType(58, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnAvisarMastarde.Location = New System.Drawing.Point(224, 66)
        Me.btnAvisarMastarde.Name = "btnAvisarMastarde"
        Me.btnAvisarMastarde.Size = New System.Drawing.Size(130, 26)
        Me.btnAvisarMastarde.TabIndex = 27
        Me.btnAvisarMastarde.Text = "A&VISAR MAS TARDE"
        Me.btnAvisarMastarde.UseVisualStyleBackColor = False
        '
        'FrmAccionTimer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(473, 106)
        Me.ControlBox = False
        Me.Controls.Add(Me.GroupBox1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmAccionTimer"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Recepción de archivos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnAvisarMastarde As System.Windows.Forms.Button
    Friend WithEvents txtleyenda As System.Windows.Forms.TextBox
End Class
