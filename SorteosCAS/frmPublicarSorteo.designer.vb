<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmPublicarSorteo
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmPublicarSorteo))
        Me.btnActualizar = New System.Windows.Forms.Button
        Me.txtNroSorteo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.lblpublicando = New System.Windows.Forms.Label
        Me.btnAnular = New System.Windows.Forms.Button
        Me.cmbJuego = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.chkForzar = New System.Windows.Forms.CheckBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'btnActualizar
        '
        Me.btnActualizar.BackColor = System.Drawing.SystemColors.Control
        Me.btnActualizar.BackgroundImage = CType(resources.GetObject("btnActualizar.BackgroundImage"), System.Drawing.Image)
        Me.btnActualizar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnActualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnActualizar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnActualizar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnActualizar.Location = New System.Drawing.Point(480, 73)
        Me.btnActualizar.Name = "btnActualizar"
        Me.btnActualizar.Size = New System.Drawing.Size(89, 32)
        Me.btnActualizar.TabIndex = 0
        Me.btnActualizar.Text = "Publicar"
        Me.btnActualizar.UseVisualStyleBackColor = False
        '
        'txtNroSorteo
        '
        Me.txtNroSorteo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroSorteo.Location = New System.Drawing.Point(469, 7)
        Me.txtNroSorteo.Name = "txtNroSorteo"
        Me.txtNroSorteo.Size = New System.Drawing.Size(100, 24)
        Me.txtNroSorteo.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(342, 10)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 18)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Número sorteo"
        '
        'lblpublicando
        '
        Me.lblpublicando.AutoSize = True
        Me.lblpublicando.BackColor = System.Drawing.Color.Transparent
        Me.lblpublicando.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblpublicando.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblpublicando.Location = New System.Drawing.Point(8, 80)
        Me.lblpublicando.Name = "lblpublicando"
        Me.lblpublicando.Size = New System.Drawing.Size(106, 18)
        Me.lblpublicando.TabIndex = 8
        Me.lblpublicando.Text = "Publicando..."
        Me.lblpublicando.Visible = False
        '
        'btnAnular
        '
        Me.btnAnular.BackColor = System.Drawing.SystemColors.Control
        Me.btnAnular.BackgroundImage = CType(resources.GetObject("btnAnular.BackgroundImage"), System.Drawing.Image)
        Me.btnAnular.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnAnular.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnAnular.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnAnular.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnAnular.Location = New System.Drawing.Point(386, 73)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.Size = New System.Drawing.Size(88, 32)
        Me.btnAnular.TabIndex = 9
        Me.btnAnular.Text = "Anular"
        Me.btnAnular.UseVisualStyleBackColor = False
        '
        'cmbJuego
        '
        Me.cmbJuego.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cmbJuego.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJuego.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbJuego.FormattingEnabled = True
        Me.cmbJuego.Location = New System.Drawing.Point(64, 7)
        Me.cmbJuego.Name = "cmbJuego"
        Me.cmbJuego.Size = New System.Drawing.Size(262, 26)
        Me.cmbJuego.TabIndex = 11
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(8, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 18)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Juego"
        '
        'chkForzar
        '
        Me.chkForzar.AutoSize = True
        Me.chkForzar.Location = New System.Drawing.Point(358, 50)
        Me.chkForzar.Name = "chkForzar"
        Me.chkForzar.Size = New System.Drawing.Size(15, 14)
        Me.chkForzar.TabIndex = 12
        Me.chkForzar.UseVisualStyleBackColor = True
        Me.chkForzar.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(383, 46)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(123, 18)
        Me.Label3.TabIndex = 13
        Me.Label3.Text = "Forzar Off-Line"
        Me.Label3.Visible = False
        '
        'frmPublicarSorteo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.BackgroundImage = CType(resources.GetObject("$this.BackgroundImage"), System.Drawing.Image)
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ClientSize = New System.Drawing.Size(579, 108)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.chkForzar)
        Me.Controls.Add(Me.txtNroSorteo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbJuego)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.lblpublicando)
        Me.Controls.Add(Me.btnActualizar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "frmPublicarSorteo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "(Re) Publicación a "
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnActualizar As System.Windows.Forms.Button
    Friend WithEvents txtNroSorteo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents lblpublicando As System.Windows.Forms.Label
    Friend WithEvents btnAnular As System.Windows.Forms.Button
    Friend WithEvents cmbJuego As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents chkForzar As System.Windows.Forms.CheckBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
End Class
