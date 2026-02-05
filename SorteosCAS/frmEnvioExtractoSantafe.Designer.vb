<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmEnvioExtractoSantafe
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmEnvioExtractoSantafe))
        Me.GpbEnvioaMedios = New System.Windows.Forms.GroupBox
        Me.PtbEsperando = New System.Windows.Forms.PictureBox
        Me.lblenviomail = New System.Windows.Forms.Label
        Me.PtbEnvio = New System.Windows.Forms.PictureBox
        Me.txtDestinatarios = New System.Windows.Forms.TextBox
        Me.Lblcarpeta = New System.Windows.Forms.Label
        Me.btnEnviar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.Label1 = New System.Windows.Forms.Label
        Me.GpbEnvioaMedios.SuspendLayout()
        CType(Me.PtbEsperando, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PtbEnvio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GpbEnvioaMedios
        '
        Me.GpbEnvioaMedios.Controls.Add(Me.PtbEsperando)
        Me.GpbEnvioaMedios.Controls.Add(Me.lblenviomail)
        Me.GpbEnvioaMedios.Controls.Add(Me.PtbEnvio)
        Me.GpbEnvioaMedios.Controls.Add(Me.txtDestinatarios)
        Me.GpbEnvioaMedios.Controls.Add(Me.Lblcarpeta)
        Me.GpbEnvioaMedios.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpbEnvioaMedios.Location = New System.Drawing.Point(12, 33)
        Me.GpbEnvioaMedios.Name = "GpbEnvioaMedios"
        Me.GpbEnvioaMedios.Size = New System.Drawing.Size(695, 83)
        Me.GpbEnvioaMedios.TabIndex = 55
        Me.GpbEnvioaMedios.TabStop = False
        Me.GpbEnvioaMedios.Text = "Enviar Listado de Extracto por Mail"
        '
        'PtbEsperando
        '
        Me.PtbEsperando.Image = Global.SorteosCAS.My.Resources.Imagenes.loading2
        Me.PtbEsperando.Location = New System.Drawing.Point(655, 14)
        Me.PtbEsperando.Name = "PtbEsperando"
        Me.PtbEsperando.Size = New System.Drawing.Size(32, 32)
        Me.PtbEsperando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbEsperando.TabIndex = 56
        Me.PtbEsperando.TabStop = False
        Me.PtbEsperando.Visible = False
        '
        'lblenviomail
        '
        Me.lblenviomail.AutoSize = True
        Me.lblenviomail.Location = New System.Drawing.Point(145, 61)
        Me.lblenviomail.Name = "lblenviomail"
        Me.lblenviomail.Size = New System.Drawing.Size(102, 16)
        Me.lblenviomail.TabIndex = 55
        Me.lblenviomail.Text = "Envío de mail"
        Me.lblenviomail.Visible = False
        '
        'PtbEnvio
        '
        Me.PtbEnvio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PtbEnvio.Image = Global.SorteosCAS.My.Resources.Imagenes.ImagenAceptar
        Me.PtbEnvio.Location = New System.Drawing.Point(123, 61)
        Me.PtbEnvio.Name = "PtbEnvio"
        Me.PtbEnvio.Size = New System.Drawing.Size(16, 16)
        Me.PtbEnvio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbEnvio.TabIndex = 54
        Me.PtbEnvio.TabStop = False
        Me.PtbEnvio.Visible = False
        '
        'txtDestinatarios
        '
        Me.txtDestinatarios.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDestinatarios.Location = New System.Drawing.Point(123, 21)
        Me.txtDestinatarios.Name = "txtDestinatarios"
        Me.txtDestinatarios.Size = New System.Drawing.Size(526, 24)
        Me.txtDestinatarios.TabIndex = 53
        '
        'Lblcarpeta
        '
        Me.Lblcarpeta.AutoSize = True
        Me.Lblcarpeta.BackColor = System.Drawing.Color.Transparent
        Me.Lblcarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblcarpeta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Lblcarpeta.Location = New System.Drawing.Point(26, 27)
        Me.Lblcarpeta.Name = "Lblcarpeta"
        Me.Lblcarpeta.Size = New System.Drawing.Size(104, 16)
        Me.Lblcarpeta.TabIndex = 52
        Me.Lblcarpeta.Text = "Destinatarios:"
        '
        'btnEnviar
        '
        Me.btnEnviar.BackColor = System.Drawing.SystemColors.Control
        Me.btnEnviar.BackgroundImage = CType(resources.GetObject("btnEnviar.BackgroundImage"), System.Drawing.Image)
        Me.btnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnviar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnviar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnEnviar.Location = New System.Drawing.Point(207, 122)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(145, 35)
        Me.btnEnviar.TabIndex = 56
        Me.btnEnviar.Text = "Enviar a medios"
        Me.btnEnviar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.BackgroundImage = CType(resources.GetObject("btnCancelar.BackgroundImage"), System.Drawing.Image)
        Me.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(367, 122)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(145, 35)
        Me.btnCancelar.TabIndex = 57
        Me.btnCancelar.Text = "Salir"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.Location = New System.Drawing.Point(12, 9)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(316, 16)
        Me.Label1.TabIndex = 58
        Me.Label1.Text = "A continuación podrá realizar la siguiente operación"
        '
        'frmEnvioExtractoSantafe
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(718, 158)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnEnviar)
        Me.Controls.Add(Me.GpbEnvioaMedios)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmEnvioExtractoSantafe"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Envío de Listado de Extracto por Mail"
        Me.GpbEnvioaMedios.ResumeLayout(False)
        Me.GpbEnvioaMedios.PerformLayout()
        CType(Me.PtbEsperando, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PtbEnvio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GpbEnvioaMedios As System.Windows.Forms.GroupBox
    Friend WithEvents btnEnviar As System.Windows.Forms.Button
    Friend WithEvents lblenviomail As System.Windows.Forms.Label
    Friend WithEvents PtbEnvio As System.Windows.Forms.PictureBox
    Friend WithEvents txtDestinatarios As System.Windows.Forms.TextBox
    Friend WithEvents Lblcarpeta As System.Windows.Forms.Label
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents PtbEsperando As System.Windows.Forms.PictureBox
End Class
