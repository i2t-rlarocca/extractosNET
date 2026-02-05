Imports Sorteos.Helpers

Public Class Inicio
    Inherits frmBase

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        ConcursoInicio.Show()
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        ConcursoExtracciones.Show()
    End Sub

    Private Sub Inicio_Cerrar_Clicked(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Cerrar_Clicked
        Me.Close()
    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        frmExtraccionesJurisdicciones.Show()
    End Sub

    Private Sub btnListados_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnListados.Click
        FrmImprimirListados.Show()

    End Sub

    Private Sub btnPremios_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPremios.Click
        frmPremios.Show()
    End Sub

    Private Sub btnConfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnConfirmar.Click
        FrmConcursoFinalizar.Show()
    End Sub

    Private Sub Inicio_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        General.PathIni = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14)
    End Sub

    Private Sub btnPublicarWeb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPublicarWeb.Click
        frmPublicarSorteo.DestinoPublicacion = "WEB"
        frmPublicarSorteo.ShowDialog()
    End Sub

    Private Sub btnPublicarDisplay_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPublicarDisplay.Click
        frmPublicarSorteo.DestinoPublicacion = "DISPLAY"
        frmPublicarSorteo.ShowDialog()
    End Sub
End Class