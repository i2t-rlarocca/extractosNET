<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmConfiguracion
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
        Me.TabCconfiguracion = New System.Windows.Forms.TabControl
        Me.tabInicio = New System.Windows.Forms.TabPage
        Me.GroupBox4 = New System.Windows.Forms.GroupBox
        Me.btnIniciar = New System.Windows.Forms.Button
        Me.btnDetener = New System.Windows.Forms.Button
        Me.TabCconfiguracion.SuspendLayout()
        Me.tabInicio.SuspendLayout()
        Me.GroupBox4.SuspendLayout()
        Me.SuspendLayout()
        '
        'TabCconfiguracion
        '
        Me.TabCconfiguracion.Controls.Add(Me.tabInicio)
        Me.TabCconfiguracion.Location = New System.Drawing.Point(1, 12)
        Me.TabCconfiguracion.Name = "TabCconfiguracion"
        Me.TabCconfiguracion.SelectedIndex = 0
        Me.TabCconfiguracion.Size = New System.Drawing.Size(463, 184)
        Me.TabCconfiguracion.TabIndex = 0
        '
        'tabInicio
        '
        Me.tabInicio.Controls.Add(Me.GroupBox4)
        Me.tabInicio.Location = New System.Drawing.Point(4, 22)
        Me.tabInicio.Name = "tabInicio"
        Me.tabInicio.Padding = New System.Windows.Forms.Padding(3)
        Me.tabInicio.Size = New System.Drawing.Size(455, 158)
        Me.tabInicio.TabIndex = 0
        Me.tabInicio.Text = "Inicio"
        Me.tabInicio.UseVisualStyleBackColor = True
        '
        'GroupBox4
        '
        Me.GroupBox4.Controls.Add(Me.btnIniciar)
        Me.GroupBox4.Controls.Add(Me.btnDetener)
        Me.GroupBox4.Location = New System.Drawing.Point(7, 27)
        Me.GroupBox4.Name = "GroupBox4"
        Me.GroupBox4.Size = New System.Drawing.Size(424, 76)
        Me.GroupBox4.TabIndex = 9
        Me.GroupBox4.TabStop = False
        Me.GroupBox4.Text = "Programa de impresión"
        '
        'btnIniciar
        '
        Me.btnIniciar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnIniciar.Image = Global.ConfigPublicador.My.Resources.Resources.printer_add
        Me.btnIniciar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnIniciar.Location = New System.Drawing.Point(97, 30)
        Me.btnIniciar.Name = "btnIniciar"
        Me.btnIniciar.Size = New System.Drawing.Size(128, 25)
        Me.btnIniciar.TabIndex = 8
        Me.btnIniciar.Text = "&Iniciar programa"
        Me.btnIniciar.UseVisualStyleBackColor = True
        '
        'btnDetener
        '
        Me.btnDetener.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnDetener.Image = Global.ConfigPublicador.My.Resources.Resources.printer_deny
        Me.btnDetener.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.btnDetener.Location = New System.Drawing.Point(231, 30)
        Me.btnDetener.Name = "btnDetener"
        Me.btnDetener.Size = New System.Drawing.Size(136, 25)
        Me.btnDetener.TabIndex = 7
        Me.btnDetener.Text = "&Detener programa"
        Me.btnDetener.UseVisualStyleBackColor = True
        '
        'frmConfiguracion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(476, 203)
        Me.Controls.Add(Me.TabCconfiguracion)
        Me.Name = "frmConfiguracion"
        Me.Text = "Configuración"
        Me.TabCconfiguracion.ResumeLayout(False)
        Me.tabInicio.ResumeLayout(False)
        Me.GroupBox4.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents TabCconfiguracion As System.Windows.Forms.TabControl
    Friend WithEvents tabInicio As System.Windows.Forms.TabPage
    Friend WithEvents GroupBox4 As System.Windows.Forms.GroupBox
    Friend WithEvents btnIniciar As System.Windows.Forms.Button
    Friend WithEvents btnDetener As System.Windows.Forms.Button

End Class
