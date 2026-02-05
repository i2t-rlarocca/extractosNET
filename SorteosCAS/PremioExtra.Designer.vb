<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class PremioExtra
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
        Me.GroupExtraQuini6 = New System.Windows.Forms.GroupBox
        Me.imagen3 = New System.Windows.Forms.PictureBox
        Me.imagen2 = New System.Windows.Forms.PictureBox
        Me.imagen1 = New System.Windows.Forms.PictureBox
        Me.Label18 = New System.Windows.Forms.Label
        Me.Label12 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.lblTitulo = New System.Windows.Forms.Label
        Me.GroupExtraQuini6.SuspendLayout()
        CType(Me.imagen3, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imagen2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.imagen1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GroupExtraQuini6
        '
        Me.GroupExtraQuini6.Controls.Add(Me.imagen3)
        Me.GroupExtraQuini6.Controls.Add(Me.imagen2)
        Me.GroupExtraQuini6.Controls.Add(Me.imagen1)
        Me.GroupExtraQuini6.Controls.Add(Me.Label18)
        Me.GroupExtraQuini6.Controls.Add(Me.Label12)
        Me.GroupExtraQuini6.Controls.Add(Me.Label1)
        Me.GroupExtraQuini6.Location = New System.Drawing.Point(10, 129)
        Me.GroupExtraQuini6.Name = "GroupExtraQuini6"
        Me.GroupExtraQuini6.Size = New System.Drawing.Size(1000, 565)
        Me.GroupExtraQuini6.TabIndex = 0
        Me.GroupExtraQuini6.TabStop = False
        '
        'imagen3
        '
        Me.imagen3.Image = Global.SorteosCAS.My.Resources.Imagenes.BolitasQuini4
        Me.imagen3.Location = New System.Drawing.Point(7, 368)
        Me.imagen3.Name = "imagen3"
        Me.imagen3.Size = New System.Drawing.Size(988, 180)
        Me.imagen3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imagen3.TabIndex = 15
        Me.imagen3.TabStop = False
        '
        'imagen2
        '
        Me.imagen2.Image = Global.SorteosCAS.My.Resources.Imagenes.BolitasQuini4
        Me.imagen2.Location = New System.Drawing.Point(7, 193)
        Me.imagen2.Name = "imagen2"
        Me.imagen2.Size = New System.Drawing.Size(988, 180)
        Me.imagen2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imagen2.TabIndex = 14
        Me.imagen2.TabStop = False
        '
        'imagen1
        '
        Me.imagen1.BackColor = System.Drawing.Color.Transparent
        Me.imagen1.Image = Global.SorteosCAS.My.Resources.Imagenes.BolitasQuini4
        Me.imagen1.Location = New System.Drawing.Point(7, 19)
        Me.imagen1.Name = "imagen1"
        Me.imagen1.Size = New System.Drawing.Size(988, 180)
        Me.imagen1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.imagen1.TabIndex = 13
        Me.imagen1.TabStop = False
        '
        'Label18
        '
        Me.Label18.AutoSize = True
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.Location = New System.Drawing.Point(140, 385)
        Me.Label18.Name = "Label18"
        Me.Label18.Size = New System.Drawing.Size(153, 108)
        Me.Label18.TabIndex = 12
        Me.Label18.Text = "06"
        Me.Label18.Visible = False
        '
        'Label12
        '
        Me.Label12.AutoSize = True
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.Location = New System.Drawing.Point(140, 218)
        Me.Label12.Name = "Label12"
        Me.Label12.Size = New System.Drawing.Size(153, 108)
        Me.Label12.TabIndex = 6
        Me.Label12.Text = "06"
        Me.Label12.Visible = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 72.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(131, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(153, 108)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "06"
        Me.Label1.Visible = False
        '
        'lblTitulo
        '
        Me.lblTitulo.AutoSize = True
        Me.lblTitulo.BackColor = System.Drawing.Color.White
        Me.lblTitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 32.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTitulo.Location = New System.Drawing.Point(7, 65)
        Me.lblTitulo.Name = "lblTitulo"
        Me.lblTitulo.Size = New System.Drawing.Size(880, 51)
        Me.lblTitulo.TabIndex = 1
        Me.lblTitulo.Text = "QUINI 6 - SORTEO @nro PREMIO EXTRA"
        '
        'PremioExtra
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.White
        Me.ClientSize = New System.Drawing.Size(1022, 718)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblTitulo)
        Me.Controls.Add(Me.GroupExtraQuini6)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "PremioExtra"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.GroupExtraQuini6.ResumeLayout(False)
        Me.GroupExtraQuini6.PerformLayout()
        CType(Me.imagen3, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imagen2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.imagen1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GroupExtraQuini6 As System.Windows.Forms.GroupBox
    Friend WithEvents lblTitulo As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label18 As System.Windows.Forms.Label
    Friend WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents imagen1 As System.Windows.Forms.PictureBox
    Friend WithEvents imagen2 As System.Windows.Forms.PictureBox
    Friend WithEvents imagen3 As System.Windows.Forms.PictureBox
End Class
