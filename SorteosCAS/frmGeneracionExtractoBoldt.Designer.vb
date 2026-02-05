<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmGeneracionExtractoBoldt
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmGeneracionExtractoBoldt))
        Me.GpbGenerarArchivo = New System.Windows.Forms.GroupBox
        Me.chkPropio = New System.Windows.Forms.CheckBox
        Me.PtbEsperando1 = New System.Windows.Forms.PictureBox
        Me.LblGenerarArchivo = New System.Windows.Forms.Label
        Me.PtbGenerar = New System.Windows.Forms.PictureBox
        Me.btnBuscarCarpeta = New System.Windows.Forms.Button
        Me.txtCarpeta = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Lblmsj = New System.Windows.Forms.Label
        Me.lbltitulo = New System.Windows.Forms.Label
        Me.CDCarpetaDestino = New System.Windows.Forms.FolderBrowserDialog
        Me.grbInterJ = New System.Windows.Forms.GroupBox
        Me.chkInterJ = New System.Windows.Forms.CheckBox
        Me.PtbEsperando2 = New System.Windows.Forms.PictureBox
        Me.LblGenerarArchivoInterJ = New System.Windows.Forms.Label
        Me.PtbGenerarInterJ = New System.Windows.Forms.PictureBox
        Me.btnBuscarCarpetaInterJ = New System.Windows.Forms.Button
        Me.txtCarpetaInterJ = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.btnGenerar = New System.Windows.Forms.Button
        Me.btnCancelar = New System.Windows.Forms.Button
        Me.GpbGenerarArchivo.SuspendLayout()
        CType(Me.PtbEsperando1, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PtbGenerar, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.grbInterJ.SuspendLayout()
        CType(Me.PtbEsperando2, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.PtbGenerarInterJ, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'GpbGenerarArchivo
        '
        Me.GpbGenerarArchivo.BackColor = System.Drawing.Color.Transparent
        Me.GpbGenerarArchivo.Controls.Add(Me.chkPropio)
        Me.GpbGenerarArchivo.Controls.Add(Me.PtbEsperando1)
        Me.GpbGenerarArchivo.Controls.Add(Me.LblGenerarArchivo)
        Me.GpbGenerarArchivo.Controls.Add(Me.PtbGenerar)
        Me.GpbGenerarArchivo.Controls.Add(Me.btnBuscarCarpeta)
        Me.GpbGenerarArchivo.Controls.Add(Me.txtCarpeta)
        Me.GpbGenerarArchivo.Controls.Add(Me.Label2)
        Me.GpbGenerarArchivo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GpbGenerarArchivo.Location = New System.Drawing.Point(14, 79)
        Me.GpbGenerarArchivo.Name = "GpbGenerarArchivo"
        Me.GpbGenerarArchivo.Size = New System.Drawing.Size(696, 83)
        Me.GpbGenerarArchivo.TabIndex = 56
        Me.GpbGenerarArchivo.TabStop = False
        '
        'chkPropio
        '
        Me.chkPropio.AutoSize = True
        Me.chkPropio.Checked = True
        Me.chkPropio.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkPropio.Location = New System.Drawing.Point(10, 0)
        Me.chkPropio.Name = "chkPropio"
        Me.chkPropio.Size = New System.Drawing.Size(424, 20)
        Me.chkPropio.TabIndex = 62
        Me.chkPropio.Text = "Generar Archivo con Formato para Cálculo de Ganadores"
        Me.chkPropio.UseVisualStyleBackColor = True
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
        Me.LblGenerarArchivo.Location = New System.Drawing.Point(222, 48)
        Me.LblGenerarArchivo.Name = "LblGenerarArchivo"
        Me.LblGenerarArchivo.Size = New System.Drawing.Size(165, 16)
        Me.LblGenerarArchivo.TabIndex = 60
        Me.LblGenerarArchivo.Text = "Generación de archivo"
        Me.LblGenerarArchivo.Visible = False
        '
        'PtbGenerar
        '
        Me.PtbGenerar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PtbGenerar.Image = Global.SorteosCAS.My.Resources.Imagenes.DELETE
        Me.PtbGenerar.Location = New System.Drawing.Point(200, 47)
        Me.PtbGenerar.Name = "PtbGenerar"
        Me.PtbGenerar.Size = New System.Drawing.Size(16, 16)
        Me.PtbGenerar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbGenerar.TabIndex = 59
        Me.PtbGenerar.TabStop = False
        Me.PtbGenerar.Visible = False
        '
        'btnBuscarCarpeta
        '
        Me.btnBuscarCarpeta.BackColor = System.Drawing.SystemColors.Control
        Me.btnBuscarCarpeta.BackgroundImage = CType(resources.GetObject("btnBuscarCarpeta.BackgroundImage"), System.Drawing.Image)
        Me.btnBuscarCarpeta.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBuscarCarpeta.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuscarCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCarpeta.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnBuscarCarpeta.Location = New System.Drawing.Point(614, 21)
        Me.btnBuscarCarpeta.Name = "btnBuscarCarpeta"
        Me.btnBuscarCarpeta.Size = New System.Drawing.Size(35, 23)
        Me.btnBuscarCarpeta.TabIndex = 58
        Me.btnBuscarCarpeta.Text = "..."
        Me.btnBuscarCarpeta.UseVisualStyleBackColor = False
        '
        'txtCarpeta
        '
        Me.txtCarpeta.Enabled = False
        Me.txtCarpeta.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarpeta.Location = New System.Drawing.Point(141, 21)
        Me.txtCarpeta.Name = "txtCarpeta"
        Me.txtCarpeta.Size = New System.Drawing.Size(467, 24)
        Me.txtCarpeta.TabIndex = 57
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label2.Location = New System.Drawing.Point(7, 25)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(128, 16)
        Me.Label2.TabIndex = 56
        Me.Label2.Text = "Carpeta Destino :"
        '
        'Lblmsj
        '
        Me.Lblmsj.AutoSize = True
        Me.Lblmsj.BackColor = System.Drawing.Color.Transparent
        Me.Lblmsj.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Lblmsj.Location = New System.Drawing.Point(15, 46)
        Me.Lblmsj.Name = "Lblmsj"
        Me.Lblmsj.Size = New System.Drawing.Size(316, 16)
        Me.Lblmsj.TabIndex = 58
        Me.Lblmsj.Text = "A continuación podrá realizar la siguiente operación"
        '
        'lbltitulo
        '
        Me.lbltitulo.AutoSize = True
        Me.lbltitulo.BackColor = System.Drawing.Color.Transparent
        Me.lbltitulo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lbltitulo.Location = New System.Drawing.Point(18, 13)
        Me.lbltitulo.Name = "lbltitulo"
        Me.lbltitulo.Size = New System.Drawing.Size(147, 16)
        Me.lbltitulo.TabIndex = 59
        Me.lbltitulo.Text = "EL SORTEO NUMERO"
        '
        'grbInterJ
        '
        Me.grbInterJ.BackColor = System.Drawing.Color.Transparent
        Me.grbInterJ.Controls.Add(Me.chkInterJ)
        Me.grbInterJ.Controls.Add(Me.PtbEsperando2)
        Me.grbInterJ.Controls.Add(Me.LblGenerarArchivoInterJ)
        Me.grbInterJ.Controls.Add(Me.PtbGenerarInterJ)
        Me.grbInterJ.Controls.Add(Me.btnBuscarCarpetaInterJ)
        Me.grbInterJ.Controls.Add(Me.txtCarpetaInterJ)
        Me.grbInterJ.Controls.Add(Me.Label3)
        Me.grbInterJ.Enabled = False
        Me.grbInterJ.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.grbInterJ.Location = New System.Drawing.Point(14, 222)
        Me.grbInterJ.Name = "grbInterJ"
        Me.grbInterJ.Size = New System.Drawing.Size(696, 37)
        Me.grbInterJ.TabIndex = 60
        Me.grbInterJ.TabStop = False
        Me.grbInterJ.Visible = False
        '
        'chkInterJ
        '
        Me.chkInterJ.AutoSize = True
        Me.chkInterJ.Location = New System.Drawing.Point(10, 15)
        Me.chkInterJ.Name = "chkInterJ"
        Me.chkInterJ.Size = New System.Drawing.Size(355, 20)
        Me.chkInterJ.TabIndex = 63
        Me.chkInterJ.Text = "Generar Archivo con Formato Interjurisdiccional"
        Me.chkInterJ.UseVisualStyleBackColor = True
        Me.chkInterJ.Visible = False
        '
        'PtbEsperando2
        '
        Me.PtbEsperando2.Image = Global.SorteosCAS.My.Resources.Imagenes.loading2
        Me.PtbEsperando2.Location = New System.Drawing.Point(655, 41)
        Me.PtbEsperando2.Name = "PtbEsperando2"
        Me.PtbEsperando2.Size = New System.Drawing.Size(32, 32)
        Me.PtbEsperando2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbEsperando2.TabIndex = 61
        Me.PtbEsperando2.TabStop = False
        Me.PtbEsperando2.Visible = False
        '
        'LblGenerarArchivoInterJ
        '
        Me.LblGenerarArchivoInterJ.AutoSize = True
        Me.LblGenerarArchivoInterJ.Location = New System.Drawing.Point(222, 68)
        Me.LblGenerarArchivoInterJ.Name = "LblGenerarArchivoInterJ"
        Me.LblGenerarArchivoInterJ.Size = New System.Drawing.Size(165, 16)
        Me.LblGenerarArchivoInterJ.TabIndex = 60
        Me.LblGenerarArchivoInterJ.Text = "Generación de archivo"
        Me.LblGenerarArchivoInterJ.Visible = False
        '
        'PtbGenerarInterJ
        '
        Me.PtbGenerarInterJ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PtbGenerarInterJ.Image = Global.SorteosCAS.My.Resources.Imagenes.DELETE
        Me.PtbGenerarInterJ.Location = New System.Drawing.Point(200, 67)
        Me.PtbGenerarInterJ.Name = "PtbGenerarInterJ"
        Me.PtbGenerarInterJ.Size = New System.Drawing.Size(16, 16)
        Me.PtbGenerarInterJ.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize
        Me.PtbGenerarInterJ.TabIndex = 59
        Me.PtbGenerarInterJ.TabStop = False
        Me.PtbGenerarInterJ.Visible = False
        '
        'btnBuscarCarpetaInterJ
        '
        Me.btnBuscarCarpetaInterJ.BackColor = System.Drawing.SystemColors.Control
        Me.btnBuscarCarpetaInterJ.BackgroundImage = CType(resources.GetObject("btnBuscarCarpetaInterJ.BackgroundImage"), System.Drawing.Image)
        Me.btnBuscarCarpetaInterJ.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnBuscarCarpetaInterJ.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnBuscarCarpetaInterJ.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnBuscarCarpetaInterJ.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnBuscarCarpetaInterJ.Location = New System.Drawing.Point(614, 41)
        Me.btnBuscarCarpetaInterJ.Name = "btnBuscarCarpetaInterJ"
        Me.btnBuscarCarpetaInterJ.Size = New System.Drawing.Size(35, 23)
        Me.btnBuscarCarpetaInterJ.TabIndex = 58
        Me.btnBuscarCarpetaInterJ.Text = "..."
        Me.btnBuscarCarpetaInterJ.UseVisualStyleBackColor = False
        Me.btnBuscarCarpetaInterJ.Visible = False
        '
        'txtCarpetaInterJ
        '
        Me.txtCarpetaInterJ.Enabled = False
        Me.txtCarpetaInterJ.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCarpetaInterJ.Location = New System.Drawing.Point(141, 41)
        Me.txtCarpetaInterJ.Name = "txtCarpetaInterJ"
        Me.txtCarpetaInterJ.Size = New System.Drawing.Size(467, 24)
        Me.txtCarpetaInterJ.TabIndex = 57
        Me.txtCarpetaInterJ.Visible = False
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.Label3.Location = New System.Drawing.Point(7, 45)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(128, 16)
        Me.Label3.TabIndex = 56
        Me.Label3.Text = "Carpeta Destino :"
        Me.Label3.Visible = False
        '
        'btnGenerar
        '
        Me.btnGenerar.BackColor = System.Drawing.SystemColors.Control
        Me.btnGenerar.BackgroundImage = CType(resources.GetObject("btnGenerar.BackgroundImage"), System.Drawing.Image)
        Me.btnGenerar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnGenerar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnGenerar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGenerar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnGenerar.Location = New System.Drawing.Point(199, 164)
        Me.btnGenerar.Name = "btnGenerar"
        Me.btnGenerar.Size = New System.Drawing.Size(145, 34)
        Me.btnGenerar.TabIndex = 61
        Me.btnGenerar.Text = "Generar Archivo"
        Me.btnGenerar.UseVisualStyleBackColor = False
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCancelar.BackgroundImage = CType(resources.GetObject("btnCancelar.BackgroundImage"), System.Drawing.Image)
        Me.btnCancelar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCancelar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCancelar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnCancelar.Location = New System.Drawing.Point(378, 164)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(145, 34)
        Me.btnCancelar.TabIndex = 62
        Me.btnCancelar.Text = "Salir"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'frmGeneracionExtractoBoldt
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.fondoLiso
        Me.ClientSize = New System.Drawing.Size(722, 210)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGenerar)
        Me.Controls.Add(Me.grbInterJ)
        Me.Controls.Add(Me.lbltitulo)
        Me.Controls.Add(Me.Lblmsj)
        Me.Controls.Add(Me.GpbGenerarArchivo)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmGeneracionExtractoBoldt"
        Me.Text = "Generar archivo extracto"
        Me.GpbGenerarArchivo.ResumeLayout(False)
        Me.GpbGenerarArchivo.PerformLayout()
        CType(Me.PtbEsperando1, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PtbGenerar, System.ComponentModel.ISupportInitialize).EndInit()
        Me.grbInterJ.ResumeLayout(False)
        Me.grbInterJ.PerformLayout()
        CType(Me.PtbEsperando2, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.PtbGenerarInterJ, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GpbGenerarArchivo As System.Windows.Forms.GroupBox
    Friend WithEvents LblGenerarArchivo As System.Windows.Forms.Label
    Friend WithEvents PtbGenerar As System.Windows.Forms.PictureBox
    Friend WithEvents btnBuscarCarpeta As System.Windows.Forms.Button
    Friend WithEvents txtCarpeta As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Lblmsj As System.Windows.Forms.Label
    Friend WithEvents PtbEsperando1 As System.Windows.Forms.PictureBox
    Friend WithEvents lbltitulo As System.Windows.Forms.Label
    Friend WithEvents CDCarpetaDestino As System.Windows.Forms.FolderBrowserDialog
    Friend WithEvents grbInterJ As System.Windows.Forms.GroupBox
    Friend WithEvents PtbEsperando2 As System.Windows.Forms.PictureBox
    Friend WithEvents LblGenerarArchivoInterJ As System.Windows.Forms.Label
    Friend WithEvents PtbGenerarInterJ As System.Windows.Forms.PictureBox
    Friend WithEvents btnBuscarCarpetaInterJ As System.Windows.Forms.Button
    Friend WithEvents txtCarpetaInterJ As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents chkPropio As System.Windows.Forms.CheckBox
    Friend WithEvents chkInterJ As System.Windows.Forms.CheckBox
    Friend WithEvents btnGenerar As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
End Class
