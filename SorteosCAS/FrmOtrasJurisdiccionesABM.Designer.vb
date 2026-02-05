<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmOtrasJurisdiccionesABM
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
        Me.dgvJurisdiccion = New System.Windows.Forms.DataGridView
        Me.btnSeleccionar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.txtOrden = New System.Windows.Forms.TextBox
        CType(Me.dgvJurisdiccion, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvJurisdiccion
        '
        Me.dgvJurisdiccion.AllowUserToAddRows = False
        Me.dgvJurisdiccion.AllowUserToDeleteRows = False
        Me.dgvJurisdiccion.AllowUserToResizeRows = False
        Me.dgvJurisdiccion.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvJurisdiccion.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically
        Me.dgvJurisdiccion.Location = New System.Drawing.Point(4, 11)
        Me.dgvJurisdiccion.MultiSelect = False
        Me.dgvJurisdiccion.Name = "dgvJurisdiccion"
        Me.dgvJurisdiccion.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None
        Me.dgvJurisdiccion.RowHeadersVisible = False
        Me.dgvJurisdiccion.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.dgvJurisdiccion.Size = New System.Drawing.Size(359, 201)
        Me.dgvJurisdiccion.StandardTab = True
        Me.dgvJurisdiccion.TabIndex = 7
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnSeleccionar.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnSeleccionar.ForeColor = System.Drawing.Color.Transparent
        Me.btnSeleccionar.Location = New System.Drawing.Point(383, 21)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(97, 23)
        Me.btnSeleccionar.TabIndex = 8
        Me.btnSeleccionar.Text = "Seleccionar"
        Me.btnSeleccionar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.Color.LightSkyBlue
        Me.btnCancelar.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.Transparent
        Me.btnCancelar.Location = New System.Drawing.Point(383, 50)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(97, 23)
        Me.btnCancelar.TabIndex = 10
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'txtOrden
        '
        Me.txtOrden.Enabled = False
        Me.txtOrden.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtOrden.Location = New System.Drawing.Point(383, 79)
        Me.txtOrden.Name = "txtOrden"
        Me.txtOrden.Size = New System.Drawing.Size(51, 22)
        Me.txtOrden.TabIndex = 11
        Me.txtOrden.Visible = False
        '
        'FrmOtrasJurisdiccionesABM
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(492, 215)
        Me.ControlBox = False
        Me.Controls.Add(Me.txtOrden)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.dgvJurisdiccion)
        Me.Name = "FrmOtrasJurisdiccionesABM"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Jurisdicciones"
        CType(Me.dgvJurisdiccion, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvJurisdiccion As System.Windows.Forms.DataGridView
    Friend WithEvents btnSeleccionar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents txtOrden As System.Windows.Forms.TextBox
End Class
