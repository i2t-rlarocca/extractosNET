<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMensajeComparacionExtractos
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
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.txtmensaje = New System.Windows.Forms.TextBox
        Me.txtcopias = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnContinuar = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.Label2)
        Me.GroupBox1.Controls.Add(Me.txtmensaje)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(12, 27)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(987, 250)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtmensaje
        '
        Me.txtmensaje.Location = New System.Drawing.Point(6, 59)
        Me.txtmensaje.Multiline = True
        Me.txtmensaje.Name = "txtmensaje"
        Me.txtmensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtmensaje.Size = New System.Drawing.Size(975, 176)
        Me.txtmensaje.TabIndex = 0
        '
        'txtcopias
        '
        Me.txtcopias.Location = New System.Drawing.Point(276, 293)
        Me.txtcopias.MaxLength = 1
        Me.txtcopias.Name = "txtcopias"
        Me.txtcopias.Size = New System.Drawing.Size(77, 20)
        Me.txtcopias.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(38, 295)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(232, 18)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Cantidad de copias a imprimir"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(256, 18)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(513, 18)
        Me.Label2.TabIndex = 1
        Me.Label2.Text = "Listado de diferencias encontradas en la comparación de extractos"
        '
        'btnContinuar
        '
        Me.btnContinuar.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnContinuar.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinuar.ForeColor = System.Drawing.Color.Transparent
        Me.btnContinuar.Location = New System.Drawing.Point(374, 289)
        Me.btnContinuar.Name = "btnContinuar"
        Me.btnContinuar.Size = New System.Drawing.Size(87, 27)
        Me.btnContinuar.TabIndex = 3
        Me.btnContinuar.Text = "&Continuar"
        Me.btnContinuar.UseVisualStyleBackColor = False
        '
        'FrmMensajeComparacionExtractos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1004, 330)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnContinuar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtcopias)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmMensajeComparacionExtractos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Resultados de comparación de extractos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtmensaje As System.Windows.Forms.TextBox
    Friend WithEvents txtcopias As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnContinuar As System.Windows.Forms.Button
End Class
