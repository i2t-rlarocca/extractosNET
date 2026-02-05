<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfirmacionRevertir
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
        Me.btnAceptar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.RadioRevertirNumeros = New System.Windows.Forms.RadioButton
        Me.RadioRevertirEstado = New System.Windows.Forms.RadioButton
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnAceptar
        '
        Me.btnAceptar.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnAceptar.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAceptar.ForeColor = System.Drawing.Color.Transparent
        Me.btnAceptar.Location = New System.Drawing.Point(87, 89)
        Me.btnAceptar.Name = "btnAceptar"
        Me.btnAceptar.Size = New System.Drawing.Size(87, 27)
        Me.btnAceptar.TabIndex = 0
        Me.btnAceptar.Text = "&Aceptar"
        Me.btnAceptar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnCancelar.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.Transparent
        Me.btnCancelar.Location = New System.Drawing.Point(223, 90)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(87, 27)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.Controls.Add(Me.RadioRevertirNumeros)
        Me.Panel1.Controls.Add(Me.RadioRevertirEstado)
        Me.Panel1.Location = New System.Drawing.Point(10, 9)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(383, 74)
        Me.Panel1.TabIndex = 2
        '
        'RadioRevertirNumeros
        '
        Me.RadioRevertirNumeros.AutoSize = True
        Me.RadioRevertirNumeros.Location = New System.Drawing.Point(14, 44)
        Me.RadioRevertirNumeros.Name = "RadioRevertirNumeros"
        Me.RadioRevertirNumeros.Size = New System.Drawing.Size(160, 19)
        Me.RadioRevertirNumeros.TabIndex = 1
        Me.RadioRevertirNumeros.TabStop = True
        Me.RadioRevertirNumeros.Text = "Revertir completamente"
        Me.RadioRevertirNumeros.UseVisualStyleBackColor = True
        '
        'RadioRevertirEstado
        '
        Me.RadioRevertirEstado.AutoSize = True
        Me.RadioRevertirEstado.Location = New System.Drawing.Point(14, 16)
        Me.RadioRevertirEstado.Name = "RadioRevertirEstado"
        Me.RadioRevertirEstado.Size = New System.Drawing.Size(349, 19)
        Me.RadioRevertirEstado.TabIndex = 0
        Me.RadioRevertirEstado.TabStop = True
        Me.RadioRevertirEstado.Text = "Volver atrás el estado conservando los números ingresados"
        Me.RadioRevertirEstado.UseVisualStyleBackColor = True
        '
        'frmConfirmacionRevertir
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(398, 120)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnAceptar)
        Me.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmConfirmacionRevertir"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Confirmación de reversión"
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents btnAceptar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents RadioRevertirNumeros As System.Windows.Forms.RadioButton
    Friend WithEvents RadioRevertirEstado As System.Windows.Forms.RadioButton
End Class
