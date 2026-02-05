<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class MDIContenedor
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(MDIContenedor))
        Me.PictureBox1 = New System.Windows.Forms.PictureBox
        Me.MenuStrip1 = New System.Windows.Forms.MenuStrip
        Me.MIIniciarConcurso = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem1 = New System.Windows.Forms.ToolStripMenuItem
        Me.MIREGISTRAREXTRACCIONES = New System.Windows.Forms.ToolStripMenuItem
        Me.SEPToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MIJurisdicciones = New System.Windows.Forms.ToolStripMenuItem
        Me.SEP2ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MIPREMIOS = New System.Windows.Forms.ToolStripMenuItem
        Me.SEP3ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MIFinalizarConcurso = New System.Windows.Forms.ToolStripMenuItem
        Me.SEP4ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MIPUBLICAR = New System.Windows.Forms.ToolStripMenuItem
        Me.Mi_ExtractoBoldt = New System.Windows.Forms.ToolStripMenuItem
        Me.MI_MailExtracto = New System.Windows.Forms.ToolStripMenuItem
        Me.PUBLICARADISPLAY = New System.Windows.Forms.ToolStripMenuItem
        Me.PUBLICARAWEB = New System.Windows.Forms.ToolStripMenuItem
        Me.MI_Iniciar = New System.Windows.Forms.ToolStripMenuItem
        Me.Mi_PozoSugerido = New System.Windows.Forms.ToolStripMenuItem
        Me.MI_ANULARSORTEO = New System.Windows.Forms.ToolStripMenuItem
        Me.MI_CAMBIARPWD = New System.Windows.Forms.ToolStripMenuItem
        Me.MI_CONTINGENCIA = New System.Windows.Forms.ToolStripMenuItem
        Me.SEP5ToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.MIImprimirListado = New System.Windows.Forms.ToolStripMenuItem
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.MenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'PictureBox1
        '
        Me.PictureBox1.BackColor = System.Drawing.Color.Transparent
        Me.PictureBox1.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.LogoLoteria
        Me.PictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PictureBox1.Location = New System.Drawing.Point(768, 623)
        Me.PictureBox1.Name = "PictureBox1"
        Me.PictureBox1.Size = New System.Drawing.Size(232, 63)
        Me.PictureBox1.TabIndex = 10
        Me.PictureBox1.TabStop = False
        Me.PictureBox1.Visible = False
        '
        'MenuStrip1
        '
        Me.MenuStrip1.BackColor = System.Drawing.Color.FromArgb(CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer), CType(CType(224, Byte), Integer))
        Me.MenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.MIIniciarConcurso, Me.ToolStripMenuItem1, Me.MIREGISTRAREXTRACCIONES, Me.SEPToolStripMenuItem, Me.MIJurisdicciones, Me.SEP2ToolStripMenuItem, Me.MIPREMIOS, Me.SEP3ToolStripMenuItem, Me.MIFinalizarConcurso, Me.SEP4ToolStripMenuItem, Me.MIPUBLICAR, Me.SEP5ToolStripMenuItem, Me.MIImprimirListado})
        Me.MenuStrip1.Location = New System.Drawing.Point(0, 0)
        Me.MenuStrip1.Name = "MenuStrip1"
        Me.MenuStrip1.Size = New System.Drawing.Size(1018, 37)
        Me.MenuStrip1.TabIndex = 13
        Me.MenuStrip1.Text = "MenuStrip1"
        '
        'MIIniciarConcurso
        '
        Me.MIIniciarConcurso.BackColor = System.Drawing.Color.Transparent
        Me.MIIniciarConcurso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MIIniciarConcurso.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MIIniciarConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MIIniciarConcurso.ForeColor = System.Drawing.Color.White
        Me.MIIniciarConcurso.Image = Global.SorteosCAS.My.Resources.Imagenes.inicio_normal
        Me.MIIniciarConcurso.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MIIniciarConcurso.Name = "MIIniciarConcurso"
        Me.MIIniciarConcurso.Size = New System.Drawing.Size(87, 33)
        Me.MIIniciarConcurso.Tag = "concursoinicio"
        Me.MIIniciarConcurso.Text = "Iniciar"
        Me.MIIniciarConcurso.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'ToolStripMenuItem1
        '
        Me.ToolStripMenuItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.ToolStripMenuItem1.Enabled = False
        Me.ToolStripMenuItem1.Image = Global.SorteosCAS.My.Resources.Imagenes.BarraTitulo
        Me.ToolStripMenuItem1.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.ToolStripMenuItem1.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.ToolStripMenuItem1.Name = "ToolStripMenuItem1"
        Me.ToolStripMenuItem1.Padding = New System.Windows.Forms.Padding(0)
        Me.ToolStripMenuItem1.Size = New System.Drawing.Size(8, 33)
        Me.ToolStripMenuItem1.Text = "ToolStripMenuItem1"
        '
        'MIREGISTRAREXTRACCIONES
        '
        Me.MIREGISTRAREXTRACCIONES.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MIREGISTRAREXTRACCIONES.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MIREGISTRAREXTRACCIONES.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MIREGISTRAREXTRACCIONES.ForeColor = System.Drawing.Color.White
        Me.MIREGISTRAREXTRACCIONES.Image = CType(resources.GetObject("MIREGISTRAREXTRACCIONES.Image"), System.Drawing.Image)
        Me.MIREGISTRAREXTRACCIONES.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MIREGISTRAREXTRACCIONES.Name = "MIREGISTRAREXTRACCIONES"
        Me.MIREGISTRAREXTRACCIONES.Padding = New System.Windows.Forms.Padding(0)
        Me.MIREGISTRAREXTRACCIONES.Size = New System.Drawing.Size(93, 33)
        Me.MIREGISTRAREXTRACCIONES.Tag = "ConcursoExtracciones"
        Me.MIREGISTRAREXTRACCIONES.Text = "SORTEAR"
        '
        'SEPToolStripMenuItem
        '
        Me.SEPToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.SEPToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SEPToolStripMenuItem.Enabled = False
        Me.SEPToolStripMenuItem.Image = Global.SorteosCAS.My.Resources.Imagenes.BarraTitulo
        Me.SEPToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SEPToolStripMenuItem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SEPToolStripMenuItem.Name = "SEPToolStripMenuItem"
        Me.SEPToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.SEPToolStripMenuItem.Size = New System.Drawing.Size(8, 33)
        Me.SEPToolStripMenuItem.Text = "SEP1"
        '
        'MIJurisdicciones
        '
        Me.MIJurisdicciones.BackColor = System.Drawing.Color.Transparent
        Me.MIJurisdicciones.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MIJurisdicciones.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MIJurisdicciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MIJurisdicciones.ForeColor = System.Drawing.Color.White
        Me.MIJurisdicciones.Image = CType(resources.GetObject("MIJurisdicciones.Image"), System.Drawing.Image)
        Me.MIJurisdicciones.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MIJurisdicciones.Name = "MIJurisdicciones"
        Me.MIJurisdicciones.Padding = New System.Windows.Forms.Padding(0)
        Me.MIJurisdicciones.Size = New System.Drawing.Size(140, 33)
        Me.MIJurisdicciones.Tag = "frmExtraccionesJurisdicciones2"
        Me.MIJurisdicciones.Text = "JURISDICCIONES"
        Me.MIJurisdicciones.TextImageRelation = System.Windows.Forms.TextImageRelation.Overlay
        '
        'SEP2ToolStripMenuItem
        '
        Me.SEP2ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SEP2ToolStripMenuItem.Enabled = False
        Me.SEP2ToolStripMenuItem.Image = Global.SorteosCAS.My.Resources.Imagenes.BarraTitulo
        Me.SEP2ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SEP2ToolStripMenuItem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SEP2ToolStripMenuItem.Name = "SEP2ToolStripMenuItem"
        Me.SEP2ToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.SEP2ToolStripMenuItem.Size = New System.Drawing.Size(8, 33)
        Me.SEP2ToolStripMenuItem.Text = "SEP2"
        '
        'MIPREMIOS
        '
        Me.MIPREMIOS.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MIPREMIOS.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MIPREMIOS.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MIPREMIOS.ForeColor = System.Drawing.Color.White
        Me.MIPREMIOS.Image = CType(resources.GetObject("MIPREMIOS.Image"), System.Drawing.Image)
        Me.MIPREMIOS.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MIPREMIOS.Name = "MIPREMIOS"
        Me.MIPREMIOS.Size = New System.Drawing.Size(101, 33)
        Me.MIPREMIOS.Text = "PREMIOS"
        '
        'SEP3ToolStripMenuItem
        '
        Me.SEP3ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SEP3ToolStripMenuItem.Enabled = False
        Me.SEP3ToolStripMenuItem.Image = Global.SorteosCAS.My.Resources.Imagenes.BarraTitulo
        Me.SEP3ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SEP3ToolStripMenuItem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SEP3ToolStripMenuItem.Name = "SEP3ToolStripMenuItem"
        Me.SEP3ToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.SEP3ToolStripMenuItem.Size = New System.Drawing.Size(8, 33)
        Me.SEP3ToolStripMenuItem.Text = "SEP3"
        '
        'MIFinalizarConcurso
        '
        Me.MIFinalizarConcurso.BackColor = System.Drawing.Color.Transparent
        Me.MIFinalizarConcurso.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MIFinalizarConcurso.CheckOnClick = True
        Me.MIFinalizarConcurso.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MIFinalizarConcurso.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MIFinalizarConcurso.ForeColor = System.Drawing.Color.White
        Me.MIFinalizarConcurso.Image = Global.SorteosCAS.My.Resources.Imagenes.fin_normal
        Me.MIFinalizarConcurso.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MIFinalizarConcurso.Name = "MIFinalizarConcurso"
        Me.MIFinalizarConcurso.Size = New System.Drawing.Size(81, 33)
        Me.MIFinalizarConcurso.Text = "Extractos"
        Me.MIFinalizarConcurso.TextImageRelation = System.Windows.Forms.TextImageRelation.ImageAboveText
        '
        'SEP4ToolStripMenuItem
        '
        Me.SEP4ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SEP4ToolStripMenuItem.Enabled = False
        Me.SEP4ToolStripMenuItem.Image = Global.SorteosCAS.My.Resources.Imagenes.BarraTitulo
        Me.SEP4ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SEP4ToolStripMenuItem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SEP4ToolStripMenuItem.Name = "SEP4ToolStripMenuItem"
        Me.SEP4ToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.SEP4ToolStripMenuItem.Size = New System.Drawing.Size(8, 33)
        Me.SEP4ToolStripMenuItem.Text = "SEP4"
        '
        'MIPUBLICAR
        '
        Me.MIPUBLICAR.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MIPUBLICAR.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MIPUBLICAR.DropDownItems.AddRange(New System.Windows.Forms.ToolStripItem() {Me.Mi_ExtractoBoldt, Me.MI_MailExtracto, Me.PUBLICARADISPLAY, Me.PUBLICARAWEB, Me.MI_Iniciar, Me.Mi_PozoSugerido, Me.MI_ANULARSORTEO, Me.MI_CAMBIARPWD, Me.MI_CONTINGENCIA})
        Me.MIPUBLICAR.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MIPUBLICAR.ForeColor = System.Drawing.Color.White
        Me.MIPUBLICAR.Image = Global.SorteosCAS.My.Resources.Imagenes.Interfaces_normal
        Me.MIPUBLICAR.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MIPUBLICAR.Name = "MIPUBLICAR"
        Me.MIPUBLICAR.Size = New System.Drawing.Size(81, 33)
        Me.MIPUBLICAR.Text = "herra"
        '
        'Mi_ExtractoBoldt
        '
        Me.Mi_ExtractoBoldt.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.boton_press
        Me.Mi_ExtractoBoldt.ForeColor = System.Drawing.Color.White
        Me.Mi_ExtractoBoldt.Name = "Mi_ExtractoBoldt"
        Me.Mi_ExtractoBoldt.Size = New System.Drawing.Size(337, 22)
        Me.Mi_ExtractoBoldt.Text = "EXTRACTO PARA BOLDT"
        '
        'MI_MailExtracto
        '
        Me.MI_MailExtracto.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.boton_normal
        Me.MI_MailExtracto.ForeColor = System.Drawing.Color.White
        Me.MI_MailExtracto.Name = "MI_MailExtracto"
        Me.MI_MailExtracto.Size = New System.Drawing.Size(337, 22)
        Me.MI_MailExtracto.Text = "EXTRACTO POR MAIL"
        '
        'PUBLICARADISPLAY
        '
        Me.PUBLICARADISPLAY.BackgroundImage = CType(resources.GetObject("PUBLICARADISPLAY.BackgroundImage"), System.Drawing.Image)
        Me.PUBLICARADISPLAY.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PUBLICARADISPLAY.Checked = True
        Me.PUBLICARADISPLAY.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PUBLICARADISPLAY.ForeColor = System.Drawing.Color.White
        Me.PUBLICARADISPLAY.Name = "PUBLICARADISPLAY"
        Me.PUBLICARADISPLAY.Size = New System.Drawing.Size(337, 22)
        Me.PUBLICARADISPLAY.Text = "(RE)PUBLICAR A DISPLAY"
        '
        'PUBLICARAWEB
        '
        Me.PUBLICARAWEB.BackgroundImage = CType(resources.GetObject("PUBLICARAWEB.BackgroundImage"), System.Drawing.Image)
        Me.PUBLICARAWEB.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.PUBLICARAWEB.Checked = True
        Me.PUBLICARAWEB.CheckState = System.Windows.Forms.CheckState.Checked
        Me.PUBLICARAWEB.ForeColor = System.Drawing.Color.White
        Me.PUBLICARAWEB.Name = "PUBLICARAWEB"
        Me.PUBLICARAWEB.Size = New System.Drawing.Size(337, 22)
        Me.PUBLICARAWEB.Text = "(RE)PUBLICAR A WEB"
        '
        'MI_Iniciar
        '
        Me.MI_Iniciar.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.boton_normal
        Me.MI_Iniciar.ForeColor = System.Drawing.Color.White
        Me.MI_Iniciar.Name = "MI_Iniciar"
        Me.MI_Iniciar.Size = New System.Drawing.Size(337, 22)
        Me.MI_Iniciar.Text = "PUBLICADOR"
        '
        'Mi_PozoSugerido
        '
        Me.Mi_PozoSugerido.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.boton_normal
        Me.Mi_PozoSugerido.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Mi_PozoSugerido.ForeColor = System.Drawing.Color.White
        Me.Mi_PozoSugerido.Name = "Mi_PozoSugerido"
        Me.Mi_PozoSugerido.Size = New System.Drawing.Size(337, 22)
        Me.Mi_PozoSugerido.Text = "INGRESO POZO SUGERIDO"
        '
        'MI_ANULARSORTEO
        '
        Me.MI_ANULARSORTEO.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.boton_normal
        Me.MI_ANULARSORTEO.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MI_ANULARSORTEO.ForeColor = System.Drawing.Color.White
        Me.MI_ANULARSORTEO.Name = "MI_ANULARSORTEO"
        Me.MI_ANULARSORTEO.Size = New System.Drawing.Size(337, 22)
        Me.MI_ANULARSORTEO.Text = "REVERTIR CONCURSO"
        '
        'MI_CAMBIARPWD
        '
        Me.MI_CAMBIARPWD.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.boton_normal
        Me.MI_CAMBIARPWD.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MI_CAMBIARPWD.ForeColor = System.Drawing.Color.White
        Me.MI_CAMBIARPWD.Name = "MI_CAMBIARPWD"
        Me.MI_CAMBIARPWD.Size = New System.Drawing.Size(337, 22)
        Me.MI_CAMBIARPWD.Text = "CAMBIAR CONTRASEÑA"
        '
        'MI_CONTINGENCIA
        '
        Me.MI_CONTINGENCIA.BackgroundImage = Global.SorteosCAS.My.Resources.Imagenes.boton_normal
        Me.MI_CONTINGENCIA.ForeColor = System.Drawing.Color.White
        Me.MI_CONTINGENCIA.Name = "MI_CONTINGENCIA"
        Me.MI_CONTINGENCIA.Size = New System.Drawing.Size(337, 22)
        Me.MI_CONTINGENCIA.Text = "CONTINGENCIA CARGA DE SORTEO"
        '
        'SEP5ToolStripMenuItem
        '
        Me.SEP5ToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.SEP5ToolStripMenuItem.Enabled = False
        Me.SEP5ToolStripMenuItem.Image = Global.SorteosCAS.My.Resources.Imagenes.BarraTitulo
        Me.SEP5ToolStripMenuItem.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.SEP5ToolStripMenuItem.Margin = New System.Windows.Forms.Padding(2, 0, 2, 0)
        Me.SEP5ToolStripMenuItem.Name = "SEP5ToolStripMenuItem"
        Me.SEP5ToolStripMenuItem.Padding = New System.Windows.Forms.Padding(0)
        Me.SEP5ToolStripMenuItem.Size = New System.Drawing.Size(8, 33)
        Me.SEP5ToolStripMenuItem.Text = "SEP5"
        '
        'MIImprimirListado
        '
        Me.MIImprimirListado.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.MIImprimirListado.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.MIImprimirListado.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.MIImprimirListado.ForeColor = System.Drawing.Color.White
        Me.MIImprimirListado.Image = CType(resources.GetObject("MIImprimirListado.Image"), System.Drawing.Image)
        Me.MIImprimirListado.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None
        Me.MIImprimirListado.Name = "MIImprimirListado"
        Me.MIImprimirListado.Size = New System.Drawing.Size(81, 33)
        Me.MIImprimirListado.Text = "IMPRIMIR "
        '
        'MDIContenedor
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(96, Byte), Integer), CType(CType(106, Byte), Integer), CType(CType(111, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1018, 703)
        Me.Controls.Add(Me.MenuStrip1)
        Me.Controls.Add(Me.PictureBox1)
        Me.ForeColor = System.Drawing.Color.Transparent
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.IsMdiContainer = True
        Me.MainMenuStrip = Me.MenuStrip1
        Me.MaximizeBox = False
        Me.Name = "MDIContenedor"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Sistema de Sorteos"
        Me.TransparencyKey = System.Drawing.Color.Transparent
        CType(Me.PictureBox1, System.ComponentModel.ISupportInitialize).EndInit()
        Me.MenuStrip1.ResumeLayout(False)
        Me.MenuStrip1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents PictureBox1 As System.Windows.Forms.PictureBox
    Friend WithEvents MenuStrip1 As System.Windows.Forms.MenuStrip
    Friend WithEvents MIFinalizarConcurso As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MIIniciarConcurso As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MIJurisdicciones As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem1 As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MIREGISTRAREXTRACCIONES As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MIPREMIOS As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MIImprimirListado As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MIPUBLICAR As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SEPToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SEP2ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SEP3ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SEP4ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents SEP5ToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PUBLICARADISPLAY As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents PUBLICARAWEB As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MI_Iniciar As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Mi_ExtractoBoldt As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MI_MailExtracto As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents Mi_PozoSugerido As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MI_ANULARSORTEO As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MI_CAMBIARPWD As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents MI_CONTINGENCIA As System.Windows.Forms.ToolStripMenuItem

End Class
