Imports Sorteos.Helpers
Imports libEntities.Entities
Imports Sorteos.Bussiness
Imports Sorteos.Extractos
Imports Sorteos.Data
Imports System.Text.RegularExpressions
Public Class FrmcontingenciaWS
    

    Private Sub FrmcontingenciaWS_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim boJuego As New JuegoBO
        Dim lstJuegos As ListaOrdenada(Of Juego)
        Dim lstJuegos2 As ListaOrdenada(Of Juego)
        Dim lstJuegos3 As ListaOrdenada(Of Juego)
        Dim lstJuegos4 As ListaOrdenada(Of Juego)
        Dim idjuegos As String = "2,3,8,49,50,30,1,4,13"

        lstJuegos = boJuego.getJuegos(idjuegos)
        lstJuegos2 = boJuego.getJuegos(idjuegos)
        lstJuegos3 = boJuego.getJuegos(idjuegos)
        lstJuegos4 = boJuego.getJuegos(idjuegos)


        cmbJuego.DataSource = lstJuegos
        cmbJuego.ValueMember = "IdJuego"
        cmbJuego.DisplayMember = "Jue_Desc"
        cmbJuego.Refresh()
        cmbJuego.SelectedIndex = 0


        cmbJuego2.DataSource = lstJuegos2
        cmbJuego2.ValueMember = "IdJuego"
        cmbJuego2.DisplayMember = "Jue_Desc"
        cmbJuego2.Refresh()
        cmbJuego2.SelectedIndex = 0


        cmbJuego3.DataSource = lstJuegos3
        cmbJuego3.ValueMember = "IdJuego"
        cmbJuego3.DisplayMember = "Jue_Desc"
        cmbJuego3.Refresh()
        cmbJuego3.SelectedIndex = 0

        cmbJuego4.DataSource = lstJuegos4
        cmbJuego4.ValueMember = "IdJuego"
        cmbJuego4.DisplayMember = "Jue_Desc"
        cmbJuego4.Refresh()
        cmbJuego4.SelectedIndex = 0

        

    End Sub

    
    Private Sub btnconfirmar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnconfirmar.Click
        Try
            Dim sorteobo As New PgmSorteoBO
            Dim odal As New PgmSorteoDAL
            Dim r As New Regex("([0-1][0-9]|2[0-3]):[0-5][0-9]")

            

            Dim _fechahora As DateTime
            Dim _fechahorapres As DateTime
            Dim _fechahoraprox As DateTime
            Dim v_pgmsorteo As Integer = 0
            Dim v_pgmsorteo2 As Integer = 0
            Dim v_pgmsorteo3 As Integer = 0
            Dim v_pgmsorteo4 As Integer = 0
            Dim fecha_hora As String = ""
            Dim fecha_horapres As String = ""
            Dim fecha_horaprox As String = ""

            Dim v_juego As Integer = 0
            Dim v_juego2 As Integer = 0
            Dim v_juego3 As Integer = 0
            Dim v_juego4 As Integer = 0

            MsgBox("¿Esta acción creará el sorteo de forma manual,desea continuar?", MsgBoxStyle.Question + MsgBoxStyle.YesNo + MsgBoxStyle.DefaultButton2, "Crear sorteo")
            If Me.txtsorteo.Text.Trim = "" And Me.txtfecha.Text.Trim = "" Then
                MsgBox("Debe ingresar al menos un sorteo.", MsgBoxStyle.Information)
                Exit Sub
            End If

            If Me.txtsorteo.Text.Trim <> "" And Me.txtfecha.Text.Trim <> "" Then
                If IsDate(txtfecha.Text) = False Then
                    MsgBox("Ingrese una fecha válida", MsgBoxStyle.Information)
                    txtfecha.Focus()
                    Exit Sub
                End If
                If IsDate(txtfechapres.Text) = False Then
                    MsgBox("Ingrese una fecha de prescripción válida", MsgBoxStyle.Information)
                    txtfechapres.Focus()
                    Exit Sub
                End If
                If IsDate(txtfechaprox.Text) = False Then
                    MsgBox("Ingrese una fecha de próximo sorteo válida", MsgBoxStyle.Information)
                    txtfechaprox.Focus()
                    Exit Sub
                End If
                If Not (r.Match(Me.txthora.Text)).Success Then
                    MsgBox("Ingrese una hora válida", MsgBoxStyle.Information)
                    txthora.Focus()
                    Exit Sub
                End If

                _fechahora = txtfecha.Text & " " & Me.txthora.Text
                _fechahorapres = txtfechapres.Text
                _fechahoraprox = txtfechaprox.Text
                fecha_hora = _fechahora.ToString("yyyy-dd-MM HH:mm")
                fecha_horapres = _fechahorapres.ToString("yyyy-dd-MM HH:mm")
                fecha_horaprox = _fechahoraprox.ToString("yyyy-dd-MM HH:mm")
                v_pgmsorteo = (Me.cmbJuego.SelectedValue) * 1000000 + Me.txtsorteo.Text
                v_juego = cmbJuego.SelectedValue

                If v_juego2 <> 0 And v_juego = v_juego2 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                If v_juego3 <> 0 And v_juego = v_juego3 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                If v_juego4 <> 0 And v_juego = v_juego4 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                sorteobo.InsertarPgmsorteosContingencia(v_pgmsorteo, 10, v_juego, txtsorteo.Text, fecha_hora, fecha_horapres, fecha_horaprox)
            End If


            If Me.txtsorteo2.Text.Trim <> "" And Me.txtfecha2.Text.Trim <> "" Then
                If IsDate(txtfecha2.Text) = False Then
                    MsgBox("Ingrese una fecha válida", MsgBoxStyle.Information)
                    txtfecha2.Focus()
                    Exit Sub
                End If
                If IsDate(txtfechapres2.Text) = False Then
                    MsgBox("Ingrese una fecha de prescripción válida", MsgBoxStyle.Information)
                    txtfechapres2.Focus()
                    Exit Sub
                End If
                If IsDate(txtfechaprox2.Text) = False Then
                    MsgBox("Ingrese una fecha de próximo sorteo válida", MsgBoxStyle.Information)
                    txtfechaprox2.Focus()
                    Exit Sub
                End If
                If Not (r.Match(Me.txthora.Text)).Success Then
                    MsgBox("Ingrese una hora válida", MsgBoxStyle.Information)
                    txthora.Focus()
                    Exit Sub
                End If
                v_juego2 = cmbJuego2.SelectedValue

                If v_juego <> 0 And v_juego2 = v_juego Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                If v_juego3 <> 0 And v_juego2 = v_juego3 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                If v_juego4 <> 0 And v_juego2 = v_juego4 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If
                v_pgmsorteo2 = (Me.cmbJuego2.SelectedValue) * 1000000 + Me.txtsorteo2.Text
                _fechahora = txtfecha2.Text & " " & Me.txthora.Text
                _fechahorapres = txtfechapres2.Text
                _fechahoraprox = txtfechaprox2.Text
                fecha_hora = _fechahora.ToString("yyyy-dd-MM HH:mm")
                fecha_horapres = _fechahorapres.ToString("yyyy-dd-MM HH:mm")
                fecha_horaprox = _fechahoraprox.ToString("yyyy-dd-MM HH:mm")
                sorteobo.InsertarPgmsorteosContingencia(v_pgmsorteo2, 10, v_juego2, txtsorteo.Text, fecha_hora, fecha_horapres, fecha_horaprox)
            End If

            If Me.txtsorteo3.Text.Trim <> "" And Me.txtfecha3.Text.Trim <> "" Then
                If IsDate(txtfecha3.Text) = False Then
                    MsgBox("Ingrese una fecha válida", MsgBoxStyle.Information)
                    txtfecha3.Focus()
                    Exit Sub
                End If
                If IsDate(txtfechapres3.Text) = False Then
                    MsgBox("Ingrese una fecha de prescripción válida", MsgBoxStyle.Information)
                    txtfechapres3.Focus()
                    Exit Sub
                End If
                If IsDate(txtfechaprox3.Text) = False Then
                    MsgBox("Ingrese una fecha de próximo sorteo válida", MsgBoxStyle.Information)
                    txtfechaprox3.Focus()
                    Exit Sub
                End If
                If Not (r.Match(Me.txthora3.Text)).Success Then
                    MsgBox("Ingrese una hora válida", MsgBoxStyle.Information)
                    txthora.Focus()
                    Exit Sub
                End If
                v_juego3 = cmbJuego3.SelectedValue

                If v_juego <> 0 And v_juego3 = v_juego Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                If v_juego2 <> 0 And v_juego3 = v_juego2 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                If v_juego4 <> 0 And v_juego3 = v_juego4 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If
                v_pgmsorteo3 = (Me.cmbJuego3.SelectedValue) * 1000000 + Me.txtsorteo3.Text
                _fechahora = txtfecha2.Text & " " & Me.txthora.Text
                _fechahorapres = txtfechapres2.Text
                _fechahoraprox = txtfechaprox2.Text
                fecha_hora = _fechahora.ToString("yyyy-dd-MM HH:mm")
                fecha_horapres = _fechahorapres.ToString("yyyy-dd-MM HH:mm")
                fecha_horaprox = _fechahoraprox.ToString("yyyy-dd-MM HH:mm")
                sorteobo.InsertarPgmsorteosContingencia(v_pgmsorteo3, 10, v_juego3, txtsorteo.Text, fecha_hora, fecha_horapres, fecha_horaprox)
            End If

            If Me.txtsorteo4.Text.Trim <> "" And Me.txtfecha4.Text.Trim <> "" Then
                If IsDate(txtfecha4.Text) = False Then
                    MsgBox("Ingrese una fecha válida", MsgBoxStyle.Information)
                    txtfecha4.Focus()
                    Exit Sub
                End If
                If IsDate(txtfechapres4.Text) = False Then
                    MsgBox("Ingrese una fecha de prescripción válida", MsgBoxStyle.Information)
                    txtfechapres4.Focus()
                    Exit Sub
                End If
                If IsDate(txtfechaprox4.Text) = False Then
                    MsgBox("Ingrese una fecha de próximo sorteo válida", MsgBoxStyle.Information)
                    txtfechaprox4.Focus()
                    Exit Sub
                End If
                If Not (r.Match(Me.txthora4.Text)).Success Then
                    MsgBox("Ingrese una hora válida", MsgBoxStyle.Information)
                    txthora.Focus()
                    Exit Sub
                End If
                v_juego4 = cmbJuego4.SelectedValue

                If v_juego <> 0 And v_juego4 = v_juego Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                If v_juego2 <> 0 And v_juego4 = v_juego2 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If


                If v_juego3 <> 0 And v_juego4 = v_juego3 Then
                    MsgBox("El juego se encuentra repetido por favor revise los juegos ingresados", MsgBoxStyle.Information)
                    txtsorteo.Focus()
                    Exit Sub
                End If
                v_pgmsorteo4 = (Me.cmbJuego4.SelectedValue) * 1000000 + Me.txtsorteo4.Text
                _fechahora = txtfecha2.Text & " " & Me.txthora.Text
                _fechahorapres = txtfechapres2.Text
                _fechahoraprox = txtfechaprox2.Text
                fecha_hora = _fechahora.ToString("yyyy-dd-MM HH:mm")
                fecha_horapres = _fechahorapres.ToString("yyyy-dd-MM HH:mm")
                fecha_horaprox = _fechahoraprox.ToString("yyyy-dd-MM HH:mm")
                sorteobo.InsertarPgmsorteosContingencia(v_pgmsorteo4, 10, v_juego4, txtsorteo.Text, fecha_hora, fecha_horapres, fecha_horaprox)
            End If


            If odal.CrearProgramaSorteo() = True Then
                MsgBox("El sorteo ha sido creado correctamente", MsgBoxStyle.Information)
            Else
                MsgBox("El sorteo no se ha podido crear,por favor revise los juegos ingresados y vuelva a intertarlo.", MsgBoxStyle.Information)
            End If
            Me.Close()



        Catch ex As Exception
            MsgBox("Problemas al crear el sorteo.", MsgBoxStyle.Critical)

        End Try
    End Sub

    Private Sub txtsorteo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsorteo.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfecha.Focus()

        Else
            General.SoloNumeros(sender, e)

        End If
    End Sub
    Private Sub txtsorteo2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsorteo.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfecha2.Focus()

        Else
            General.SoloNumeros(sender, e)

        End If
    End Sub

   


    Private Sub txtsorteo3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsorteo2.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfecha3.Focus()

        Else
            General.SoloNumeros(sender, e)

        End If
    End Sub

    Private Sub txtfecha_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfecha.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txthora.Focus()
        End If


    End Sub
    Private Sub txtfecha2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfecha.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txthora2.Focus()
        End If


    End Sub
    Private Sub txtfecha3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfecha.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txthora3.Focus()
        End If


    End Sub

    Private Sub txtfecha4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfecha.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txthora4.Focus()
        End If


    End Sub

    Private Sub txthora4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfechapres.Focus()

        End If
    End Sub
    Private Sub txthora2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txthora.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfechapres2.Focus()

        End If
    End Sub
    Private Sub txthora3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txthora.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfechapres3.Focus()

        End If
    End Sub
    Private Sub txthora_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txthora.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfechapres.Focus()

        End If
    End Sub

    Private Sub txtfechapres_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfechapres.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfechaprox.Focus()
        End If
    End Sub
    Private Sub txtfechapres2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfechapres.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfechaprox2.Focus()
        End If
    End Sub
    Private Sub txtfechapres3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfechapres.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfechaprox3.Focus()
        End If
    End Sub

    Private Sub txtfechapres4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtfechapres.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfechaprox4.Focus()
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Me.Close()

    End Sub

    Private Sub txtsorteo4_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtsorteo4.KeyPress
        If e.KeyChar = Convert.ToChar(Keys.Return) Then
            Me.txtfecha4.Focus()

        Else
            General.SoloNumeros(sender, e)

        End If
    End Sub

   
    Public Function esHoraValida() As Boolean
        Dim r As New Regex("([0-1][0-9]|2[0-3]):[0-5][0-9]")
        Dim sw As Boolean = True
        If Not (r.Match(Me.txtHora.Text)).Success Then
            sw = False
            'Si el dato de entrada no es hora, mostrar un mensaje al usuario ("Debe ingresar la hora con formato válido [08:30].")
        End If
        Return sw
    End Function

  
    Private Sub txtfechapres_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtfechapres.TextChanged

    End Sub

    Private Sub txthora_MaskInputRejected(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MaskInputRejectedEventArgs) Handles txthora.MaskInputRejected

    End Sub

    Private Sub txtsorteo_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsorteo.TextChanged

    End Sub

    Private Sub txtsorteo2_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles txtsorteo2.TextChanged

    End Sub
End Class