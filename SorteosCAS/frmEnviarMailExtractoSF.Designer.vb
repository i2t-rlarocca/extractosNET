<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEnviarMailExtractoSF
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEnviarMailExtractoSF))
        Me.txtNroSorteo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbJuego = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnEnviar = New System.Windows.Forms.Button
        Me.lblgenerando = New System.Windows.Forms.Label
        Me.txtDestinatarios = New System.Windows.Forms.TextBox
        Me.Lblcarpeta = New System.Windows.Forms.Label
        Me.CDCarpetaDestino = New System.Windows.Forms.FolderBrowserDialog
        Me.lbldesc = New System.Windows.Forms.Label
        Me.txtdesc = New System.Windows.Forms.TextBox
        Me.SuspendLayout()
        '
        'txtNroSorteo
        '
        Me.txtNroSorteo.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNroSorteo.Location = New System.Drawing.Point(572, 6)
        Me.txtNroSorteo.MaxLength = 8
        Me.txtNroSorteo.Name = "txtNroSorteo"
        Me.txtNroSorteo.Size = New System.Drawing.Size(100, 24)
        Me.txtNroSorteo.TabIndex = 15
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(445, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(122, 18)
        Me.Label2.TabIndex = 13
        Me.Label2.Text = "Número sorteo"
        '
        'cmbJuego
        '
        Me.cmbJuego.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.cmbJuego.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbJuego.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cmbJuego.FormattingEnabled = True
        Me.cmbJuego.Location = New System.Drawing.Point(68, 6)
        Me.cmbJuego.Name = "cmbJuego"
        Me.cmbJuego.Size = New System.Drawing.Size(262, 26)
        Me.cmbJuego.TabIndex = 16
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(54, 18)
        Me.Label1.TabIndex = 14
        Me.Label1.Text = "Juego"
        '
        'btnEnviar
        '
        Me.btnEnviar.BackColor = System.Drawing.SystemColors.Control
        Me.btnEnviar.BackgroundImage = CType(resources.GetObject("btnEnviar.BackgroundImage"), System.Drawing.Image)
        Me.btnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnviar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnviar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnEnviar.Location = New System.Drawing.Point(598, 46)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(84, 26)
        Me.btnEnviar.TabIndex = 12
        Me.btnEnviar.Text = "Enviar"
        Me.btnEnviar.UseVisualStyleBackColor = False
        '
        'lblgenerando
        '
        Me.lblgenerando.AutoSize = True
        Me.lblgenerando.BackColor = System.Drawing.Color.Transparent
        Me.lblgenerando.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblgenerando.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblgenerando.Location = New System.Drawing.Point(485, 52)
        Me.lblgenerando.Name = "lblgenerando"
        Me.lblgenerando.Size = New System.Drawing.Size(106, 18)
        Me.lblgenerando.TabIndex = 17
        Me.lblgenerando.Text = "Generando..."
        Me.lblgenerando.Visible = False
        '
        'txtDestinatarios
        '
        Me.txtDestinatarios.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDestinatarios.Location = New System.Drawing.Point(111, 46)
        Me.txtDestinatarios.Name = "txtDestinatarios"
        Me.txtDestinatarios.Size = New System.Drawing.Size(368, 24)
        Me.txtDestinatarios.TabIndex = 19
        '
        'Lblcarpeta
        '
        Me.Lblcarpeta.AutoSize = True
        Me.Lblcarpeta.BackColor = System.Drawing.Color.Transparent
        Me.Lblcarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblcarpeta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Lblcarpeta.Location = New System.Drawing.Point(8, 48)
        Me.Lblcarpeta.Name = "Lblcarpeta"
        Me.Lblcarpeta.Size = New System.Drawing.Size(113, 18)
        Me.Lblcarpeta.TabIndex = 18
        Me.Lblcarpeta.Text = "Destinatarios:"
        '
        'lbldesc
        '
        Me.lbldesc.AutoSize = True
        Me.lbldesc.BackColor = System.Drawing.Color.Transparent
        Me.lbldesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbldesc.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lbldesc.Location = New System.Drawing.Point(12, 89)
        Me.lbldesc.Name = "lbldesc"
        Me.lbldesc.Size = New System.Drawing.Size(98, 18)
        Me.lbldesc.TabIndex = 20
        Me.lbldesc.Text = "Descripción"
        '
        'txtdesc
        '
        Me.txtdesc.Location = New System.Drawing.Point(111, 78)
        Me.txtdesc.Multiline = True
        Me.txtdesc.Name = "txtdesc"
        Me.txtdesc.Size = New System.Drawing.Size(561, 33)
        Me.txtdesc.TabIndex = 21
        '
        'FrmEnviarMailExtractoSF
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.fondo1sinbarras4
        Me.ClientSize = New System.Drawing.Size(686, 79)
        Me.Controls.Add(Me.txtdesc)
        Me.Controls.Add(Me.lbldesc)
        Me.Controls.Add(Me.txtDestinatarios)
        Me.Controls.Add(Me.Lblcarpeta)
        Me.Controls.Add(Me.lblgenerando)
        Me.Controls.Add(Me.txtNroSorteo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbJuego)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnEnviar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmEnviarMailExtractoSF"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Enviar Listado de Extracto por mail"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNroSorteo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbJuego As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnEnviar As System.Windows.Forms.Button
    Friend WithEvents lblgenerando As System.Windows.Forms.Label
    Friend WithEvents txtDestinatarios As System.Windows.Forms.TextBox
    Friend WithEvents Lblcarpeta As System.Windows.Forms.Label
    Friend WithEvents CDCarpetaDestino As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lbldesc As System.Windows.Forms.Label
    Friend WithEvents txtdesc As System.Windows.Forms.TextBox
End Class
