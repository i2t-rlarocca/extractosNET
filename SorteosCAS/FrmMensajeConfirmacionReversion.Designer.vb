<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMensajeConfirmacionReversion
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
        Me.txtUltimoNroSorteoConfirmado = New System.Windows.Forms.TextBox
        Me.txtUltimoJuegoConfirmado = New System.Windows.Forms.TextBox
        Me.lblanterior = New System.Windows.Forms.Label
        Me.Label3 = New System.Windows.Forms.Label
        Me.txtNroSorteoARevertir = New System.Windows.Forms.TextBox
        Me.txtJuegoARevertir = New System.Windows.Forms.TextBox
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.txtmensaje = New System.Windows.Forms.TextBox
        Me.btnContinuar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.txtUltimoNroSorteoConfirmado)
        Me.GroupBox1.Controls.Add(Me.txtUltimoJuegoConfirmado)
        Me.GroupBox1.Controls.Add(Me.lblanterior)
        Me.GroupBox1.Controls.Add(Me.Label3)
        Me.GroupBox1.Controls.Add(Me.txtNroSorteoARevertir)
        Me.GroupBox1.Controls.Add(Me.txtJuegoARevertir)
        Me.GroupBox1.Controls.Add(Me.lblTitulo)
        Me.GroupBox1.Controls.Add(Me.txtmensaje)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.Location = New System.Drawing.Point(4, 12)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(925, 202)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        '
        'txtUltimoNroSorteoConfirmado
        '
        Me.txtUltimoNroSorteoConfirmado.Font = New System.Drawing.Font("Arial Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUltimoNroSorteoConfirmado.Location = New System.Drawing.Point(486, 115)
        Me.txtUltimoNroSorteoConfirmado.Name = "txtUltimoNroSorteoConfirmado"
        Me.txtUltimoNroSorteoConfirmado.ReadOnly = True
        Me.txtUltimoNroSorteoConfirmado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUltimoNroSorteoConfirmado.Size = New System.Drawing.Size(318, 26)
        Me.txtUltimoNroSorteoConfirmado.TabIndex = 7
        Me.txtUltimoNroSorteoConfirmado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtUltimoNroSorteoConfirmado.Visible = False
        '
        'txtUltimoJuegoConfirmado
        '
        Me.txtUltimoJuegoConfirmado.Font = New System.Drawing.Font("Arial Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUltimoJuegoConfirmado.Location = New System.Drawing.Point(486, 87)
        Me.txtUltimoJuegoConfirmado.Name = "txtUltimoJuegoConfirmado"
        Me.txtUltimoJuegoConfirmado.ReadOnly = True
        Me.txtUltimoJuegoConfirmado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtUltimoJuegoConfirmado.Size = New System.Drawing.Size(318, 26)
        Me.txtUltimoJuegoConfirmado.TabIndex = 6
        Me.txtUltimoJuegoConfirmado.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblanterior
        '
        Me.lblanterior.AutoSize = True
        Me.lblanterior.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblanterior.ForeColor = System.Drawing.Color.Black
        Me.lblanterior.Location = New System.Drawing.Point(553, 65)
        Me.lblanterior.Name = "lblanterior"
        Me.lblanterior.Size = New System.Drawing.Size(187, 18)
        Me.lblanterior.TabIndex = 5
        Me.lblanterior.Text = "ULTIMO CONFIRMADO"
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold)
        Me.Label3.ForeColor = System.Drawing.Color.Black
        Me.Label3.Location = New System.Drawing.Point(169, 63)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(199, 18)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "EXTRACTO A REVERTIR"
        '
        'txtNroSorteoARevertir
        '
        Me.txtNroSorteoARevertir.Font = New System.Drawing.Font("Arial Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroSorteoARevertir.ForeColor = System.Drawing.Color.Red
        Me.txtNroSorteoARevertir.Location = New System.Drawing.Point(116, 114)
        Me.txtNroSorteoARevertir.Name = "txtNroSorteoARevertir"
        Me.txtNroSorteoARevertir.ReadOnly = True
        Me.txtNroSorteoARevertir.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtNroSorteoARevertir.Size = New System.Drawing.Size(318, 26)
        Me.txtNroSorteoARevertir.TabIndex = 3
        Me.txtNroSorteoARevertir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        Me.txtNroSorteoARevertir.Visible = False
        '
        'txtJuegoARevertir
        '
        Me.txtJuegoARevertir.Font = New System.Drawing.Font("Arial Black", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtJuegoARevertir.ForeColor = System.Drawing.Color.Red
        Me.txtJuegoARevertir.Location = New System.Drawing.Point(116, 86)
        Me.txtJuegoARevertir.Name = "txtJuegoARevertir"
        Me.txtJuegoARevertir.ReadOnly = True
        Me.txtJuegoARevertir.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtJuegoARevertir.Size = New System.Drawing.Size(318, 26)
        Me.txtJuegoARevertir.TabIndex = 2
        Me.txtJuegoARevertir.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.Font = New System.Drawing.Font("Arial Black", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.ForeColor = System.Drawing.Color.Red
        Me.lblTitulo.Location = New System.Drawing.Point(112, 18)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(665, 22)
        Me.lblTitulo.TabIndex = 1
        Me.lblTitulo.Text = "El extracto a revertir corresponde a un sorteo ANTERIOR al último confirmado."
        Me.lblTitulo.TextAlign = System.Drawing.ContentAlignment.TopCenter
        '
        'txtmensaje
        '
        Me.txtmensaje.Location = New System.Drawing.Point(4, 158)
        Me.txtmensaje.Name = "txtmensaje"
        Me.txtmensaje.ReadOnly = True
        Me.txtmensaje.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtmensaje.Size = New System.Drawing.Size(913, 22)
        Me.txtmensaje.TabIndex = 0
        Me.txtmensaje.Text = "Si no es lo que desea realizar haga click en CANCELAR. De lo contrario haga click" & _
            " en CONTINUAR para efectivizar la Reversión."
        Me.txtmensaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'btnContinuar
        '
        Me.btnContinuar.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnContinuar.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnContinuar.ForeColor = System.Drawing.Color.Transparent
        Me.btnContinuar.Location = New System.Drawing.Point(328, 220)
        Me.btnContinuar.Name = "btnContinuar"
        Me.btnContinuar.Size = New System.Drawing.Size(125, 27)
        Me.btnContinuar.TabIndex = 2
        Me.btnContinuar.Text = "C&ONTINUAR"
        Me.btnContinuar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnCancelar.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.Transparent
        Me.btnCancelar.Location = New System.Drawing.Point(477, 220)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(125, 27)
        Me.btnCancelar.TabIndex = 1
        Me.btnCancelar.Text = "&CANCELAR"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'FrmMensajeConfirmacionReversion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(931, 260)
        Me.ControlBox = False
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnContinuar)
        Me.Controls.Add(Me.GroupBox1)
        Me.Name = "FrmMensajeConfirmacionReversion"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Reversión de Extracto - Alerta"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents txtmensaje As System.Windows.Forms.TextBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents btnContinuar As System.Windows.Forms.Button
    Friend WithEvents txtNroSorteoARevertir As System.Windows.Forms.TextBox
    Friend WithEvents txtJuegoARevertir As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents lblanterior As System.Windows.Forms.Label
    Friend WithEvents txtUltimoNroSorteoConfirmado As System.Windows.Forms.TextBox
    Friend WithEvents txtUltimoJuegoConfirmado As System.Windows.Forms.TextBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
End Class
