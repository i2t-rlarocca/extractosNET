<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmImprimirListados
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmImprimirListados))
        Me.GroupBoxGral = New System.Windows.Forms.GroupBox
        Me.btnCerrar = New System.Windows.Forms.Button
        Me.BtnImprimir = New System.Windows.Forms.Button
        Me.GroupBoxSorteos = New System.Windows.Forms.GroupBox
        Me.CheckedListBoxSorteos = New System.Windows.Forms.CheckedListBox
        Me.GroupBoxJuegos = New System.Windows.Forms.GroupBox
        Me.CheckedListBoxJuegos = New System.Windows.Forms.CheckedListBox
        Me.GroupBox1 = New System.Windows.Forms.GroupBox
        Me.Chkpozoproximo = New System.Windows.Forms.CheckBox
        Me.chkDifCuad = New System.Windows.Forms.CheckBox
        Me.chkParametrosGan = New System.Windows.Forms.CheckBox
        Me.ChkExtractos = New System.Windows.Forms.CheckBox
        Me.ChkExtracciones = New System.Windows.Forms.CheckBox
        Me.chkParametros = New System.Windows.Forms.CheckBox
        Me.chkEscGan = New System.Windows.Forms.CheckBox
        Me.GroupBoxGral.SuspendLayout()
        Me.GroupBoxSorteos.SuspendLayout()
        Me.GroupBoxJuegos.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'GroupBoxGral
        '
        Me.GroupBoxGral.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
                    Or System.Windows.Forms.AnchorStyles.Left) _
                    Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.GroupBoxGral.Controls.Add(Me.btnCerrar)
        Me.GroupBoxGral.Controls.Add(Me.BtnImprimir)
        Me.GroupBoxGral.Controls.Add(Me.GroupBoxSorteos)
        Me.GroupBoxGral.Controls.Add(Me.GroupBoxJuegos)
        Me.GroupBoxGral.Controls.Add(Me.GroupBox1)
        Me.GroupBoxGral.Location = New System.Drawing.Point(3, 12)
        Me.GroupBoxGral.Name = "GroupBoxGral"
        Me.GroupBoxGral.Size = New System.Drawing.Size(778, 563)
        Me.GroupBoxGral.TabIndex = 0
        Me.GroupBoxGral.TabStop = False
        '
        'btnCerrar
        '
        Me.btnCerrar.BackColor = System.Drawing.SystemColors.Control
        Me.btnCerrar.BackgroundImage = CType(resources.GetObject("btnCerrar.BackgroundImage"), System.Drawing.Image)
        Me.btnCerrar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.btnCerrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.btnCerrar.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCerrar.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.btnCerrar.Location = New System.Drawing.Point(417, 524)
        Me.btnCerrar.Name = "btnCerrar"
        Me.btnCerrar.Size = New System.Drawing.Size(124, 26)
        Me.btnCerrar.TabIndex = 9
        Me.btnCerrar.Text = "&CERRAR"
        Me.btnCerrar.UseVisualStyleBackColor = False
        '
        'BtnImprimir
        '
        Me.BtnImprimir.BackColor = System.Drawing.SystemColors.Control
        Me.BtnImprimir.BackgroundImage = CType(resources.GetObject("BtnImprimir.BackgroundImage"), System.Drawing.Image)
        Me.BtnImprimir.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.BtnImprimir.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.BtnImprimir.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.BtnImprimir.ForeColor = System.Drawing.Color.FromArgb(CType(CType(52, Byte), Integer), CType(CType(118, Byte), Integer), CType(CType(166, Byte), Integer))
        Me.BtnImprimir.Location = New System.Drawing.Point(270, 524)
        Me.BtnImprimir.Name = "BtnImprimir"
        Me.BtnImprimir.Size = New System.Drawing.Size(124, 26)
        Me.BtnImprimir.TabIndex = 8
        Me.BtnImprimir.Text = "&IMPRIMIR"
        Me.BtnImprimir.UseVisualStyleBackColor = False
        '
        'GroupBoxSorteos
        '
        Me.GroupBoxSorteos.Controls.Add(Me.CheckedListBoxSorteos)
        Me.GroupBoxSorteos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxSorteos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.GroupBoxSorteos.Location = New System.Drawing.Point(411, 148)
        Me.GroupBoxSorteos.Name = "GroupBoxSorteos"
        Me.GroupBoxSorteos.Size = New System.Drawing.Size(329, 357)
        Me.GroupBoxSorteos.TabIndex = 6
        Me.GroupBoxSorteos.TabStop = False
        Me.GroupBoxSorteos.Text = "SORTEOS"
        '
        'CheckedListBoxSorteos
        '
        Me.CheckedListBoxSorteos.CheckOnClick = True
        Me.CheckedListBoxSorteos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckedListBoxSorteos.FormattingEnabled = True
        Me.CheckedListBoxSorteos.Location = New System.Drawing.Point(6, 19)
        Me.CheckedListBoxSorteos.Name = "CheckedListBoxSorteos"
        Me.CheckedListBoxSorteos.Size = New System.Drawing.Size(317, 310)
        Me.CheckedListBoxSorteos.TabIndex = 7
        Me.CheckedListBoxSorteos.UseCompatibleTextRendering = True
        '
        'GroupBoxJuegos
        '
        Me.GroupBoxJuegos.Controls.Add(Me.CheckedListBoxJuegos)
        Me.GroupBoxJuegos.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBoxJuegos.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.GroupBoxJuegos.Location = New System.Drawing.Point(45, 148)
        Me.GroupBoxJuegos.Name = "GroupBoxJuegos"
        Me.GroupBoxJuegos.Size = New System.Drawing.Size(329, 357)
        Me.GroupBoxJuegos.TabIndex = 4
        Me.GroupBoxJuegos.TabStop = False
        Me.GroupBoxJuegos.Text = "JUEGOS"
        '
        'CheckedListBoxJuegos
        '
        Me.CheckedListBoxJuegos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CheckedListBoxJuegos.FormattingEnabled = True
        Me.CheckedListBoxJuegos.Location = New System.Drawing.Point(6, 19)
        Me.CheckedListBoxJuegos.Name = "CheckedListBoxJuegos"
        Me.CheckedListBoxJuegos.Size = New System.Drawing.Size(317, 310)
        Me.CheckedListBoxJuegos.TabIndex = 5
        Me.CheckedListBoxJuegos.UseCompatibleTextRendering = True
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.chkEscGan)
        Me.GroupBox1.Controls.Add(Me.Chkpozoproximo)
        Me.GroupBox1.Controls.Add(Me.chkDifCuad)
        Me.GroupBox1.Controls.Add(Me.chkParametrosGan)
        Me.GroupBox1.Controls.Add(Me.ChkExtractos)
        Me.GroupBox1.Controls.Add(Me.ChkExtracciones)
        Me.GroupBox1.Controls.Add(Me.chkParametros)
        Me.GroupBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 11.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.GroupBox1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(110, Byte), Integer), CType(CType(114, Byte), Integer), CType(CType(116, Byte), Integer))
        Me.GroupBox1.Location = New System.Drawing.Point(15, 19)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(763, 123)
        Me.GroupBox1.TabIndex = 0
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "LISTADOS"
        '
        'Chkpozoproximo
        '
        Me.Chkpozoproximo.AutoSize = True
        Me.Chkpozoproximo.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Chkpozoproximo.Location = New System.Drawing.Point(6, 83)
        Me.Chkpozoproximo.Name = "Chkpozoproximo"
        Me.Chkpozoproximo.Size = New System.Drawing.Size(343, 20)
        Me.Chkpozoproximo.TabIndex = 6
        Me.Chkpozoproximo.Text = "LISTADO DE POZO ESTIMADO PROXIMO SORTEO"
        Me.Chkpozoproximo.UseVisualStyleBackColor = True
        '
        'chkDifCuad
        '
        Me.chkDifCuad.AutoSize = True
        Me.chkDifCuad.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDifCuad.Location = New System.Drawing.Point(440, 57)
        Me.chkDifCuad.Name = "chkDifCuad"
        Me.chkDifCuad.Size = New System.Drawing.Size(318, 20)
        Me.chkDifCuad.TabIndex = 5
        Me.chkDifCuad.Text = "LISTADO DE DIFERENCIAS EN CUADRATURA"
        Me.chkDifCuad.UseVisualStyleBackColor = True
        '
        'chkParametrosGan
        '
        Me.chkParametrosGan.AutoSize = True
        Me.chkParametrosGan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkParametrosGan.Location = New System.Drawing.Point(6, 57)
        Me.chkParametrosGan.Name = "chkParametrosGan"
        Me.chkParametrosGan.Size = New System.Drawing.Size(393, 20)
        Me.chkParametrosGan.TabIndex = 4
        Me.chkParametrosGan.Text = "LISTADO DE PARAMETROS PARA ANOTAR GANADORES"
        Me.chkParametrosGan.UseVisualStyleBackColor = True
        Me.chkParametrosGan.Visible = False
        '
        'ChkExtractos
        '
        Me.ChkExtractos.AutoSize = True
        Me.ChkExtractos.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkExtractos.Location = New System.Drawing.Point(440, 31)
        Me.ChkExtractos.Name = "ChkExtractos"
        Me.ChkExtractos.Size = New System.Drawing.Size(306, 20)
        Me.ChkExtractos.TabIndex = 3
        Me.ChkExtractos.Text = "LISTADO DE EXTRACTO PARA LOS MEDIOS"
        Me.ChkExtractos.UseVisualStyleBackColor = True
        '
        'ChkExtracciones
        '
        Me.ChkExtracciones.AutoSize = True
        Me.ChkExtracciones.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ChkExtracciones.Location = New System.Drawing.Point(216, 31)
        Me.ChkExtracciones.Name = "ChkExtracciones"
        Me.ChkExtracciones.Size = New System.Drawing.Size(213, 20)
        Me.ChkExtracciones.TabIndex = 2
        Me.ChkExtracciones.Text = "LISTADO DE EXTRACCIONES"
        Me.ChkExtracciones.UseVisualStyleBackColor = True
        '
        'chkParametros
        '
        Me.chkParametros.AutoSize = True
        Me.chkParametros.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkParametros.Location = New System.Drawing.Point(6, 31)
        Me.chkParametros.Name = "chkParametros"
        Me.chkParametros.Size = New System.Drawing.Size(204, 20)
        Me.chkParametros.TabIndex = 1
        Me.chkParametros.Text = "LISTADO DE PARAMETROS"
        Me.chkParametros.UseVisualStyleBackColor = True
        '
        'chkEscGan
        '
        Me.chkEscGan.AutoSize = True
        Me.chkEscGan.Font = New System.Drawing.Font("Microsoft Sans Serif", 9.75!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkEscGan.Location = New System.Drawing.Point(440, 83)
        Me.chkEscGan.Name = "chkEscGan"
        Me.chkEscGan.Size = New System.Drawing.Size(285, 20)
        Me.chkEscGan.TabIndex = 7
        Me.chkEscGan.Text = "LISTADO DE ESCENARIOS GANADORES"
        Me.chkEscGan.UseVisualStyleBackColor = True
        '
        'FrmImprimirListados
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer), CType(CType(239, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(784, 587)
        Me.Controls.Add(Me.GroupBoxGral)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Name = "FrmImprimirListados"
        Me.Text = "Impresión de Listados"
        Me.TopMost = True
        Me.GroupBoxGral.ResumeLayout(False)
        Me.GroupBoxSorteos.ResumeLayout(False)
        Me.GroupBoxJuegos.ResumeLayout(False)
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents GroupBoxGral As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxSorteos As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBoxJuegos As System.Windows.Forms.GroupBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents BtnImprimir As System.Windows.Forms.Button
    Friend WithEvents ChkExtractos As System.Windows.Forms.CheckBox
    Friend WithEvents ChkExtracciones As System.Windows.Forms.CheckBox
    Friend WithEvents chkParametros As System.Windows.Forms.CheckBox
    Friend WithEvents CheckedListBoxJuegos As System.Windows.Forms.CheckedListBox
    Friend WithEvents CheckedListBoxSorteos As System.Windows.Forms.CheckedListBox
    Friend WithEvents btnCerrar As System.Windows.Forms.Button
    Friend WithEvents chkParametrosGan As System.Windows.Forms.CheckBox
    Friend WithEvents chkDifCuad As System.Windows.Forms.CheckBox
    Friend WithEvents Chkpozoproximo As System.Windows.Forms.CheckBox
    Friend WithEvents chkEscGan As System.Windows.Forms.CheckBox
End Class
