<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class ConcursoInicioParametros
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
        Me.ReportDocument = New CrystalDecisions.CrystalReports.Engine.ReportDocument
        Me.crvListadoVisor = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crvListadoVisor
        '
        Me.crvListadoVisor.ActiveViewIndex = -1
        Me.crvListadoVisor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crvListadoVisor.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crvListadoVisor.Location = New System.Drawing.Point(0, 0)
        Me.crvListadoVisor.Name = "crvListadoVisor"
        Me.crvListadoVisor.Size = New System.Drawing.Size(695, 432)
        Me.crvListadoVisor.TabIndex = 0
        '
        'ConcursoInicioParametros
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(695, 432)
        Me.Controls.Add(Me.crvListadoVisor)
        Me.Name = "ConcursoInicioParametros"
        Me.Text = "ConcursoInicioParametros"
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents ReportDocument As CrystalDecisions.CrystalReports.Engine.ReportDocument
    Friend WithEvents crvListadoVisor As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
