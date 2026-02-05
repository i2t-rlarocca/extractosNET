<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Inicio
    Inherits frmBase

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
        Me.Button1 = New System.Windows.Forms.Button
        Me.Button2 = New System.Windows.Forms.Button
        Me.btnConfirmar = New System.Windows.Forms.Button
        Me.Button4 = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.TextBox1 = New System.Windows.Forms.TextBox
        Me.btnListados = New System.Windows.Forms.Button
        Me.btnPremios = New System.Windows.Forms.Button
        Me.btnPublicarDisplay = New System.Windows.Forms.Button
        Me.btnPublicarWeb = New System.Windows.Forms.Button
        Me.SuspendLayout()
        '
        'Button1
        '
        Me.Button1.Location = New System.Drawing.Point(92, 32)
        Me.Button1.Name = "Button1"
        Me.Button1.Size = New System.Drawing.Size(232, 28)
        Me.Button1.TabIndex = 0
        Me.Button1.Text = "INICIAR CONCURSO"
        Me.Button1.UseVisualStyleBackColor = True
        '
        'Button2
        '
        Me.Button2.Location = New System.Drawing.Point(92, 80)
        Me.Button2.Name = "Button2"
        Me.Button2.Size = New System.Drawing.Size(232, 28)
        Me.Button2.TabIndex = 1
        Me.Button2.Text = "REGISTRAR EXTRACCIONES"
        Me.Button2.UseVisualStyleBackColor = True
        '
        'btnConfirmar
        '
        Me.btnConfirmar.Location = New System.Drawing.Point(92, 216)
        Me.btnConfirmar.Name = "btnConfirmar"
        Me.btnConfirmar.Size = New System.Drawing.Size(232, 28)
        Me.btnConfirmar.TabIndex = 6
        Me.btnConfirmar.Text = "CONFIRMAR CONCURSO"
        Me.btnConfirmar.UseVisualStyleBackColor = True
        '
        'Button4
        '
        Me.Button4.Location = New System.Drawing.Point(92, 114)
        Me.Button4.Name = "Button4"
        Me.Button4.Size = New System.Drawing.Size(232, 28)
        Me.Button4.TabIndex = 3
        Me.Button4.Text = "OTRAS JURISDICCIONES"
        Me.Button4.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(159, 171)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(0, 13)
        Me.Label1.TabIndex = 4
        '
        'TextBox1
        '
        Me.TextBox1.BackColor = System.Drawing.SystemColors.GradientInactiveCaption
        Me.TextBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.Location = New System.Drawing.Point(134, 379)
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.ReadOnly = True
        Me.TextBox1.Size = New System.Drawing.Size(138, 20)
        Me.TextBox1.TabIndex = 5
        Me.TextBox1.Text = "Versión:    01.00.17"
        Me.TextBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnListados
        '
        Me.btnListados.Location = New System.Drawing.Point(92, 265)
        Me.btnListados.Name = "btnListados"
        Me.btnListados.Size = New System.Drawing.Size(232, 28)
        Me.btnListados.TabIndex = 4
        Me.btnListados.Text = "IMPRIMIR LISTADOS"
        Me.btnListados.UseVisualStyleBackColor = True
        '
        'btnPremios
        '
        Me.btnPremios.Location = New System.Drawing.Point(92, 163)
        Me.btnPremios.Name = "btnPremios"
        Me.btnPremios.Size = New System.Drawing.Size(232, 28)
        Me.btnPremios.TabIndex = 5
        Me.btnPremios.Text = "PREMIOS"
        Me.btnPremios.UseVisualStyleBackColor = True
        '
        'btnPublicarDisplay
        '
        Me.btnPublicarDisplay.Location = New System.Drawing.Point(92, 299)
        Me.btnPublicarDisplay.Name = "btnPublicarDisplay"
        Me.btnPublicarDisplay.Size = New System.Drawing.Size(232, 28)
        Me.btnPublicarDisplay.TabIndex = 7
        Me.btnPublicarDisplay.Text = "(RE) PUBLICAR A DISPLAY"
        Me.btnPublicarDisplay.UseVisualStyleBackColor = True
        '
        'btnPublicarWeb
        '
        Me.btnPublicarWeb.Location = New System.Drawing.Point(92, 333)
        Me.btnPublicarWeb.Name = "btnPublicarWeb"
        Me.btnPublicarWeb.Size = New System.Drawing.Size(232, 28)
        Me.btnPublicarWeb.TabIndex = 8
        Me.btnPublicarWeb.Text = "(RE) PUBLICAR A WEB"
        Me.btnPublicarWeb.UseVisualStyleBackColor = True
        '
        'Inicio
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(417, 412)
        Me.Controls.Add(Me.btnPublicarWeb)
        Me.Controls.Add(Me.btnPublicarDisplay)
        Me.Controls.Add(Me.btnPremios)
        Me.Controls.Add(Me.btnListados)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Button4)
        Me.Controls.Add(Me.btnConfirmar)
        Me.Controls.Add(Me.Button2)
        Me.Controls.Add(Me.Button1)
        Me.Name = "Inicio"
        Me.Text = "Bienvenido a SorteosCAS!"
        Me.TransparencyKey = System.Drawing.Color.FromArgb(CType(CType(192, Byte), Integer), CType(CType(192, Byte), Integer), CType(CType(255, Byte), Integer))
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents Button1 As System.Windows.Forms.Button
    Friend WithEvents Button2 As System.Windows.Forms.Button
    Friend WithEvents btnConfirmar As System.Windows.Forms.Button
    Friend WithEvents Button4 As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents TextBox1 As System.Windows.Forms.TextBox
    Friend WithEvents btnListados As System.Windows.Forms.Button
    Friend WithEvents btnPremios As System.Windows.Forms.Button
    Friend WithEvents btnPublicarDisplay As System.Windows.Forms.Button
    Friend WithEvents btnPublicarWeb As System.Windows.Forms.Button
End Class
