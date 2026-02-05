<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmEnviarMailExtracto
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEnviarMailExtracto))
        Me.txtNroSorteo = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.cmbJuego = New System.Windows.Forms.ComboBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.btnEnviar = New System.Windows.Forms.Button
        Me.txtDestinatarios = New System.Windows.Forms.TextBox
        Me.Lblcarpeta = New System.Windows.Forms.Label
        Me.CDCarpetaDestino = New System.Windows.Forms.FolderBrowserDialog
        Me.lbldesc = New System.Windows.Forms.Label
        Me.txtdesc = New System.Windows.Forms.TextBox
        Me.PtbEsperando = New System.Windows.Forms.PictureBox
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.lblenviomail = New System.Windows.Forms.Label
        Me.PtbEnvio = New System.Windows.Forms.PictureBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnlistaranexos = New System.Windows.Forms.Button
        Me.Chkenviaranexos = New System.Windows.Forms.CheckBox
        CType(Me.PtbEsperando, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PtbEnvio, System.ComponentModel.ISupportInitialize).BeginInit()
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
        Me.btnEnviar.Location = New System.Drawing.Point(210, 175)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(145, 35)
        Me.btnEnviar.TabIndex = 12
        Me.btnEnviar.Text = "Enviar"
        Me.btnEnviar.UseVisualStyleBackColor = False
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
        Me.lbldesc.Location = New System.Drawing.Point(12, 78)
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
        'PtbEsperando
        '
        Me.PtbEsperando.Image = Global.SorteosCAS.My.Resources.Imagenes.loading2
        Me.PtbEsperando.Location = New System.Drawing.Point(485, 40)
        Me.PtbEsperando.Name = "PtbEsperando"
        Me.PtbEsperando.Size = New System.Drawing.Size(32, 32)
        Me.PtbEsperando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbEsperando.TabIndex = 57
        Me.PtbEsperando.TabStop = False
        Me.PtbEsperando.Visible = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.BackgroundImage = CType(resources.GetObject("btnCancelar.BackgroundImage"), System.Drawing.Image)
        Me.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(372, 175)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(145, 35)
        Me.btnCancelar.TabIndex = 58
        Me.btnCancelar.Text = "Salir"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'lblenviomail
        '
        Me.lblenviomail.AutoSize = True
        Me.lblenviomail.BackColor = System.Drawing.Color.Transparent
        Me.lblenviomail.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!)
        Me.lblenviomail.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.lblenviomail.Location = New System.Drawing.Point(25, 130)
        Me.lblenviomail.Name = "lblenviomail"
        Me.lblenviomail.Size = New System.Drawing.Size(96, 18)
        Me.lblenviomail.TabIndex = 60
        Me.lblenviomail.Text = "Envío de mail"
        Me.lblenviomail.Visible = False
        '
        'PtbEnvio
        '
        Me.PtbEnvio.BackColor = System.Drawing.Color.Transparent
        Me.PtbEnvio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PtbEnvio.Image = Global.SorteosCAS.My.Resources.Imagenes.ImagenAceptar
        Me.PtbEnvio.Location = New System.Drawing.Point(12, 130)
        Me.PtbEnvio.Name = "PtbEnvio"
        Me.PtbEnvio.Size = New System.Drawing.Size(16, 16)
        Me.PtbEnvio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbEnvio.TabIndex = 59
        Me.PtbEnvio.TabStop = False
        Me.PtbEnvio.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(12, 96)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(84, 18)
        Me.Label3.TabIndex = 61
        Me.Label3.Text = "(opcional)"
        '
        'btnlistaranexos
        '
        Me.btnlistaranexos.BackColor = System.Drawing.SystemColors.Control
        Me.btnlistaranexos.BackgroundImage = CType(resources.GetObject("btnlistaranexos.BackgroundImage"), System.Drawing.Image)
        Me.btnlistaranexos.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnlistaranexos.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnlistaranexos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnlistaranexos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnlistaranexos.Location = New System.Drawing.Point(45, 175)
        Me.btnlistaranexos.Name = "btnlistaranexos"
        Me.btnlistaranexos.Size = New System.Drawing.Size(145, 35)
        Me.btnlistaranexos.TabIndex = 62
        Me.btnlistaranexos.Text = "Listar Anexos"
        Me.btnlistaranexos.UseVisualStyleBackColor = False
        Me.btnlistaranexos.Visible = False
        '
        'Chkenviaranexos
        '
        Me.Chkenviaranexos.AutoSize = True
        Me.Chkenviaranexos.BackColor = System.Drawing.Color.Transparent
        Me.Chkenviaranexos.Checked = True
        Me.Chkenviaranexos.CheckState = System.Windows.Forms.CheckState.Checked
        Me.Chkenviaranexos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chkenviaranexos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Chkenviaranexos.Location = New System.Drawing.Point(535, 183)
        Me.Chkenviaranexos.Name = "Chkenviaranexos"
        Me.Chkenviaranexos.Size = New System.Drawing.Size(126, 20)
        Me.Chkenviaranexos.TabIndex = 63
        Me.Chkenviaranexos.Text = "Enviar Anexos"
        Me.Chkenviaranexos.UseVisualStyleBackColor = False
        Me.Chkenviaranexos.Visible = False
        '
        'FrmEnviarMailExtracto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.fondoLisoConBarra
        Me.ClientSize = New System.Drawing.Size(720, 215)
        Me.Controls.Add(Me.Chkenviaranexos)
        Me.Controls.Add(Me.btnlistaranexos)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblenviomail)
        Me.Controls.Add(Me.PtbEnvio)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.PtbEsperando)
        Me.Controls.Add(Me.txtdesc)
        Me.Controls.Add(Me.lbldesc)
        Me.Controls.Add(Me.txtDestinatarios)
        Me.Controls.Add(Me.Lblcarpeta)
        Me.Controls.Add(Me.txtNroSorteo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cmbJuego)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnEnviar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmEnviarMailExtracto"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent
        Me.Text = "Enviar Datos de Extracto por mail"
        CType(Me.PtbEsperando, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PtbEnvio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents txtNroSorteo As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents cmbJuego As System.Windows.Forms.ComboBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnEnviar As System.Windows.Forms.Button
    Friend WithEvents txtDestinatarios As System.Windows.Forms.TextBox
    Friend WithEvents Lblcarpeta As System.Windows.Forms.Label
    Friend WithEvents CDCarpetaDestino As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents lbldesc As System.Windows.Forms.Label
    Friend WithEvents txtdesc As System.Windows.Forms.TextBox
    Friend WithEvents PtbEsperando As System.Windows.Forms.PictureBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents lblenviomail As System.Windows.Forms.Label
    Friend WithEvents PtbEnvio As System.Windows.Forms.PictureBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents btnlistaranexos As System.Windows.Forms.Button
    Friend WithEvents Chkenviaranexos As System.Windows.Forms.CheckBox
End Class
