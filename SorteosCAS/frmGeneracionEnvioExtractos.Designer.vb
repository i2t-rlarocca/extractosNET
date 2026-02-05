<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneracionEnvioExtractos
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGeneracionEnvioExtractos))
        Me.CDCarpetaDestino = New System.Windows.Forms.FolderBrowserDialog
        Me.Panel1 = New System.Windows.Forms.Panel
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.btnEnviaryGenerar = New System.Windows.Forms.Button
        Me.GpbGenerarArchivo = New System.Windows.Forms.GroupBox
        Me.PtbEsperando1 = New System.Windows.Forms.PictureBox
        Me.LblGenerarArchivo = New System.Windows.Forms.Label
        Me.PtbGenerar = New System.Windows.Forms.PictureBox
        Me.btnbuscarCarpeta = New System.Windows.Forms.Button
        Me.txtCarpeta = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnGenerar = New System.Windows.Forms.Button
        Me.GpbEnvioaMedios = New System.Windows.Forms.GroupBox
        Me.PtbEsperando = New System.Windows.Forms.PictureBox
        Me.btnEnviar = New System.Windows.Forms.Button
        Me.lblenviomail = New System.Windows.Forms.Label
        Me.PtbEnvio = New System.Windows.Forms.PictureBox
        Me.txtDestinatarios = New System.Windows.Forms.TextBox
        Me.Lblcarpeta = New System.Windows.Forms.Label
        Me.LBLTITULO = New System.Windows.Forms.Label
        Me.lblTituloConfirmado = New System.Windows.Forms.Label
        Me.Panel1.SuspendLayout()
        Me.GpbGenerarArchivo.SuspendLayout()
        CType(Me.PtbEsperando1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PtbGenerar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.GpbEnvioaMedios.SuspendLayout()
        CType(Me.PtbEsperando, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PtbEnvio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'Panel1
        '
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.Panel1.Controls.Add(Me.btnCancelar)
        Me.Panel1.Controls.Add(Me.btnEnviaryGenerar)
        Me.Panel1.Controls.Add(Me.GpbGenerarArchivo)
        Me.Panel1.Controls.Add(Me.GpbEnvioaMedios)
        Me.Panel1.Controls.Add(Me.LBLTITULO)
        Me.Panel1.Controls.Add(Me.lblTituloConfirmado)
        Me.Panel1.Location = New System.Drawing.Point(7, 6)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(735, 283)
        Me.Panel1.TabIndex = 0
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.BackgroundImage = CType(resources.GetObject("btnCancelar.BackgroundImage"), System.Drawing.Image)
        Me.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Myriad Web Pro", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(369, 249)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(145, 28)
        Me.btnCancelar.TabIndex = 57
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btnEnviaryGenerar
        '
        Me.btnEnviaryGenerar.BackColor = System.Drawing.SystemColors.Control
        Me.btnEnviaryGenerar.BackgroundImage = CType(resources.GetObject("btnEnviaryGenerar.BackgroundImage"), System.Drawing.Image)
        Me.btnEnviaryGenerar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnviaryGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnviaryGenerar.Font = New System.Drawing.Font("Myriad Web Pro", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnviaryGenerar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnEnviaryGenerar.Location = New System.Drawing.Point(218, 249)
        Me.btnEnviaryGenerar.Name = "btnEnviaryGenerar"
        Me.btnEnviaryGenerar.Size = New System.Drawing.Size(145, 28)
        Me.btnEnviaryGenerar.TabIndex = 56
        Me.btnEnviaryGenerar.Text = "Enviar y Generar"
        Me.btnEnviaryGenerar.UseVisualStyleBackColor = False
        '
        'GpbGenerarArchivo
        '
        Me.GpbGenerarArchivo.Controls.Add(Me.PtbEsperando1)
        Me.GpbGenerarArchivo.Controls.Add(Me.LblGenerarArchivo)
        Me.GpbGenerarArchivo.Controls.Add(Me.PtbGenerar)
        Me.GpbGenerarArchivo.Controls.Add(Me.btnbuscarCarpeta)
        Me.GpbGenerarArchivo.Controls.Add(Me.txtCarpeta)
        Me.GpbGenerarArchivo.Controls.Add(Me.Label2)
        Me.GpbGenerarArchivo.Controls.Add(Me.btnGenerar)
        Me.GpbGenerarArchivo.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpbGenerarArchivo.Location = New System.Drawing.Point(18, 160)
        Me.GpbGenerarArchivo.Name = "GpbGenerarArchivo"
        Me.GpbGenerarArchivo.Size = New System.Drawing.Size(700, 83)
        Me.GpbGenerarArchivo.TabIndex = 55
        Me.GpbGenerarArchivo.TabStop = False
        Me.GpbGenerarArchivo.Text = "Generación de archivos para Boldt"
        '
        'PtbEsperando1
        '
        Me.PtbEsperando1.Image = Global.SorteosCAS.My.Resources.Imagenes.loading2
        Me.PtbEsperando1.Location = New System.Drawing.Point(655, 21)
        Me.PtbEsperando1.Name = "PtbEsperando1"
        Me.PtbEsperando1.Size = New System.Drawing.Size(32, 32)
        Me.PtbEsperando1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbEsperando1.TabIndex = 61
        Me.PtbEsperando1.TabStop = False
        Me.PtbEsperando1.Visible = False
        '
        'LblGenerarArchivo
        '
        Me.LblGenerarArchivo.AutoSize = True
        Me.LblGenerarArchivo.Location = New System.Drawing.Point(145, 53)
        Me.LblGenerarArchivo.Name = "LblGenerarArchivo"
        Me.LblGenerarArchivo.Size = New System.Drawing.Size(142, 15)
        Me.LblGenerarArchivo.TabIndex = 60
        Me.LblGenerarArchivo.Text = "Generación de archivo"
        Me.LblGenerarArchivo.Visible = False
        '
        'PtbGenerar
        '
        Me.PtbGenerar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PtbGenerar.Image = Global.SorteosCAS.My.Resources.Imagenes.DELETE
        Me.PtbGenerar.Location = New System.Drawing.Point(123, 52)
        Me.PtbGenerar.Name = "PtbGenerar"
        Me.PtbGenerar.Size = New System.Drawing.Size(16, 16)
        Me.PtbGenerar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbGenerar.TabIndex = 59
        Me.PtbGenerar.TabStop = False
        Me.PtbGenerar.Visible = False
        '
        'btnbuscarCarpeta
        '
        Me.btnbuscarCarpeta.BackColor = System.Drawing.SystemColors.Control
        Me.btnbuscarCarpeta.BackgroundImage = CType(resources.GetObject("btnbuscarCarpeta.BackgroundImage"), System.Drawing.Image)
        Me.btnbuscarCarpeta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnbuscarCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnbuscarCarpeta.Font = New System.Drawing.Font("Myriad Web Pro", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnbuscarCarpeta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnbuscarCarpeta.Location = New System.Drawing.Point(614, 21)
        Me.btnbuscarCarpeta.Name = "btnbuscarCarpeta"
        Me.btnbuscarCarpeta.Size = New System.Drawing.Size(35, 23)
        Me.btnbuscarCarpeta.TabIndex = 58
        Me.btnbuscarCarpeta.Text = "..."
        Me.btnbuscarCarpeta.UseVisualStyleBackColor = False
        '
        'txtCarpeta
        '
        Me.txtCarpeta.Enabled = False
        Me.txtCarpeta.Font = New System.Drawing.Font("Myriad Web Pro", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarpeta.Location = New System.Drawing.Point(123, 21)
        Me.txtCarpeta.Name = "txtCarpeta"
        Me.txtCarpeta.Size = New System.Drawing.Size(485, 25)
        Me.txtCarpeta.TabIndex = 57
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(7, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(110, 15)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Carpeta Destino :"
        '
        'btnGenerar
        '
        Me.btnGenerar.BackColor = System.Drawing.SystemColors.Control
        Me.btnGenerar.BackgroundImage = CType(resources.GetObject("btnGenerar.BackgroundImage"), System.Drawing.Image)
        Me.btnGenerar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerar.Font = New System.Drawing.Font("Myriad Web Pro", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnGenerar.Location = New System.Drawing.Point(755, 21)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(145, 23)
        Me.btnGenerar.TabIndex = 54
        Me.btnGenerar.Text = "Generar Archivo"
        Me.btnGenerar.UseVisualStyleBackColor = False
        Me.btnGenerar.Visible = False
        '
        'GpbEnvioaMedios
        '
        Me.GpbEnvioaMedios.Controls.Add(Me.PtbEsperando)
        Me.GpbEnvioaMedios.Controls.Add(Me.btnEnviar)
        Me.GpbEnvioaMedios.Controls.Add(Me.lblenviomail)
        Me.GpbEnvioaMedios.Controls.Add(Me.PtbEnvio)
        Me.GpbEnvioaMedios.Controls.Add(Me.txtDestinatarios)
        Me.GpbEnvioaMedios.Controls.Add(Me.Lblcarpeta)
        Me.GpbEnvioaMedios.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpbEnvioaMedios.Location = New System.Drawing.Point(18, 73)
        Me.GpbEnvioaMedios.Name = "GpbEnvioaMedios"
        Me.GpbEnvioaMedios.Size = New System.Drawing.Size(700, 83)
        Me.GpbEnvioaMedios.TabIndex = 54
        Me.GpbEnvioaMedios.TabStop = False
        Me.GpbEnvioaMedios.Text = "Enviar Extracto por Mail"
        '
        'PtbEsperando
        '
        Me.PtbEsperando.Image = Global.SorteosCAS.My.Resources.Imagenes.loading2
        Me.PtbEsperando.Location = New System.Drawing.Point(655, 21)
        Me.PtbEsperando.Name = "PtbEsperando"
        Me.PtbEsperando.Size = New System.Drawing.Size(32, 32)
        Me.PtbEsperando.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbEsperando.TabIndex = 57
        Me.PtbEsperando.TabStop = False
        Me.PtbEsperando.Visible = False
        '
        'btnEnviar
        '
        Me.btnEnviar.BackColor = System.Drawing.SystemColors.Control
        Me.btnEnviar.BackgroundImage = CType(resources.GetObject("btnEnviar.BackgroundImage"), System.Drawing.Image)
        Me.btnEnviar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnEnviar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnEnviar.Font = New System.Drawing.Font("Myriad Web Pro", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEnviar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnEnviar.Location = New System.Drawing.Point(755, 23)
        Me.btnEnviar.Name = "btnEnviar"
        Me.btnEnviar.Size = New System.Drawing.Size(145, 23)
        Me.btnEnviar.TabIndex = 56
        Me.btnEnviar.Text = "Enviar a medios"
        Me.btnEnviar.UseVisualStyleBackColor = False
        Me.btnEnviar.Visible = False
        '
        'lblenviomail
        '
        Me.lblenviomail.AutoSize = True
        Me.lblenviomail.Location = New System.Drawing.Point(145, 61)
        Me.lblenviomail.Name = "lblenviomail"
        Me.lblenviomail.Size = New System.Drawing.Size(89, 15)
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
        Me.txtDestinatarios.Font = New System.Drawing.Font("Myriad Web Pro", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDestinatarios.Location = New System.Drawing.Point(123, 21)
        Me.txtDestinatarios.Name = "txtDestinatarios"
        Me.txtDestinatarios.Size = New System.Drawing.Size(526, 25)
        Me.txtDestinatarios.TabIndex = 53
        '
        'Lblcarpeta
        '
        Me.Lblcarpeta.AutoSize = True
        Me.Lblcarpeta.BackColor = System.Drawing.Color.Transparent
        Me.Lblcarpeta.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblcarpeta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Lblcarpeta.Location = New System.Drawing.Point(26, 27)
        Me.Lblcarpeta.Name = "Lblcarpeta"
        Me.Lblcarpeta.Size = New System.Drawing.Size(91, 15)
        Me.Lblcarpeta.TabIndex = 52
        Me.Lblcarpeta.Text = "Destinatarios:"
        '
        'LBLTITULO
        '
        Me.LBLTITULO.AutoSize = True
        Me.LBLTITULO.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LBLTITULO.Location = New System.Drawing.Point(15, 46)
        Me.LBLTITULO.Name = "LBLTITULO"
        Me.LBLTITULO.Size = New System.Drawing.Size(315, 15)
        Me.LBLTITULO.TabIndex = 42
        Me.LBLTITULO.Text = "A continuación podra realizar las siguientes operaciones"
        '
        'lblTituloConfirmado
        '
        Me.lblTituloConfirmado.AutoSize = True
        Me.lblTituloConfirmado.BackColor = System.Drawing.Color.Transparent
        Me.lblTituloConfirmado.Font = New System.Drawing.Font("Myriad Web Pro", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblTituloConfirmado.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTituloConfirmado.Location = New System.Drawing.Point(15, 16)
        Me.lblTituloConfirmado.Name = "lblTituloConfirmado"
        Me.lblTituloConfirmado.Size = New System.Drawing.Size(284, 15)
        Me.lblTituloConfirmado.TabIndex = 41
        Me.lblTituloConfirmado.Text = "EL SORTEO HA SIDO CONFIRMADO CON ÉXITO"
        '
        'frmGeneracionEnvioExtractos
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(748, 296)
        Me.Controls.Add(Me.Panel1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGeneracionEnvioExtractos"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Envío para medios y generación de archivos para Boldt"
        Me.TopMost = True
        Me.Panel1.ResumeLayout(False)
        Me.Panel1.PerformLayout()
        Me.GpbGenerarArchivo.ResumeLayout(False)
        Me.GpbGenerarArchivo.PerformLayout()
        CType(Me.PtbEsperando1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PtbGenerar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.GpbEnvioaMedios.ResumeLayout(False)
        Me.GpbEnvioaMedios.PerformLayout()
        CType(Me.PtbEsperando, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PtbEnvio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents CDCarpetaDestino As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Friend WithEvents LBLTITULO As System.Windows.Forms.Label
    Friend WithEvents lblTituloConfirmado As System.Windows.Forms.Label
    Friend WithEvents GpbGenerarArchivo As System.Windows.Forms.GroupBox
    Friend WithEvents btnbuscarCarpeta As System.Windows.Forms.Button
    Friend WithEvents txtCarpeta As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents GpbEnvioaMedios As System.Windows.Forms.GroupBox
    Friend WithEvents txtDestinatarios As System.Windows.Forms.TextBox
    Friend WithEvents Lblcarpeta As System.Windows.Forms.Label
    Friend WithEvents lblenviomail As System.Windows.Forms.Label
    Friend WithEvents PtbEnvio As System.Windows.Forms.PictureBox
    Friend WithEvents btnEnviaryGenerar As System.Windows.Forms.Button
    Friend WithEvents LblGenerarArchivo As System.Windows.Forms.Label
    Friend WithEvents PtbGenerar As System.Windows.Forms.PictureBox
    Friend WithEvents btnEnviar As System.Windows.Forms.Button
    Friend WithEvents PtbEsperando1 As System.Windows.Forms.PictureBox
    Friend WithEvents PtbEsperando As System.Windows.Forms.PictureBox
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
End Class
