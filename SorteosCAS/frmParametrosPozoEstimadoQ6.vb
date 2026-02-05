Imports System.Globalization

Imports Sorteos.Helpers
Imports libEntities.Entities
Imports Sorteos.Bussiness
Imports Sorteos.Extractos

Public Class frmParametrosPozoEstimadoQ6
    Public v_oSorteo As New PgmSorteo
    Private oParProxPozoEstimado As New ParProxPozoEstimado



    Private Sub frmParametrosPozoEstimadoQ6_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Try
            Dim oParProxPozoEstimadoBO As New ParProxPozoEstimadoBO

            oParProxPozoEstimado = oParProxPozoEstimadoBO.getParProxPozoEstimado(v_oSorteo.idPgmSorteo)

            If oParProxPozoEstimado Is Nothing Then
                FileSystemHelper.Log("frmParametrosPozoEstimadoQ6.Load: el getParProxPozoEstimado retornó Nothing!! Se cierra la pantalla de par prox sorteo.")
                Throw New Exception("Problemas al obtener los Parámetros para cálculo del Próx. Pozo Estimado. Cierre la aplicación y vuelva a intentar. Si el problema persiste consulte a Soporte.")
                Try
                    Me.Close()
                Catch ex2 As Exception
                    FileSystemHelper.Log("frmParametrosPozoEstimadoQ6.Load: Excepción al querer cerrar la pantalla en el load porque el get parametros devolvió Nothing!!" & ex2.Message)
                End Try
                Exit Sub
            Else
                oParProxPozoEstimado.IdPgmSorteo = v_oSorteo.idPgmSorteo
            End If

            actualizarValoresEnPantalla() ' propiedades del obj ==> valores de controles de pantalla



            ''Dim bopozo As New PozoBO
            ''If bopozo.getParametrosPozoEstimadoProximoSorteo(idpgmsorteo, apu_miercoles, apu_domingo, porc_apu_total_revancha, porc_apu_total_ss, porc_valor_apu_tradicional, porc_valor_apu_revancha, porc_valor_apu_ss, pozo_extra, pozo_adicional, minimo_tradicional, minimo_segunda, minimo_revancha, valapu_tradicional, valapu_revancha, valapu_ss, porc_1premiotradicional, porc_2premiotradicional, porc_3premiotradicional, porc_estimulotradicional, porc_1premiorevancha, porc_estimulorevancha, porc_1premioSS, porc_estimuloSS) Then
            ''    lblvigencia.Text = "Valores vigentes desde Sorteo Nro: " & idpgmsorteo - 4000000
            ''    txtPorc_var_apuestas_miercoles_domingos.Text = Double.Parse(apu_miercoles).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtPorc_var_apuestas_domingos_miercoles.Text = Double.Parse(apu_domingo).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtPorc_cant_apuesta_3.Text = Double.Parse(porc_apu_total_revancha).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtPorc_cant_apuesta_7.Text = Double.Parse(porc_apu_total_ss).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txt_importe_apu_tradic.Text = Double.Parse(valapu_tradicional).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtapu_tradicional.Text = Double.Parse(porc_valor_apu_tradicional).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txt_importe_apu_revancha.Text = Double.Parse(valapu_revancha).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txt_apu_revancha.Text = Double.Parse(porc_valor_apu_revancha).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txt_importe_apu_ss.Text = Double.Parse(valapu_ss).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txt_apu_ss.Text = Double.Parse(porc_valor_apu_ss).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))

            ''    txtpremioTradic1er.Text = Double.Parse(porc_1premiotradicional).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtpremioTradic2do.Text = Double.Parse(porc_2premiotradicional).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtpremioTradic3ro.Text = Double.Parse(porc_3premiotradicional).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtpremioTradicestimulo.Text = Double.Parse(porc_estimulotradicional).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))

            ''    txtpremiorevancha1er.Text = Double.Parse(porc_1premiorevancha).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtpremiorevanchaestimulo.Text = Double.Parse(porc_estimulorevancha).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))

            ''    txtpremioss1er.Text = Double.Parse(porc_1premioSS).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtpremioSSestimulo.Text = Double.Parse(porc_estimuloSS).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))

            ''    txtpozo_extra.Text = Double.Parse(pozo_extra).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtpozo_sorteoadicional.Text = Double.Parse(pozo_adicional).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtminimo_tradicional.Text = Double.Parse(minimo_tradicional).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtminimo_revancha.Text = Double.Parse(minimo_revancha).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''    txtminimo_segunda.Text = Double.Parse(minimo_segunda).ToString("N", System.Globalization.CultureInfo.CreateSpecificCulture("es-AR"))
            ''End If

        Catch ex As Exception
            FileSystemHelper.Log("frmParametrosPozoEstimadoQ6.Load: Excepción ->" & ex.Message & "<-")
            MsgBox("Ocurrió una Excepción al abrir la ventana de Parámetros de Próx. Pozo. Cierre la aplicación y vuelva a intentar. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Critical, MDIContenedor.Text)
            Try
                Me.Close()
            Catch ex2 As Exception
                FileSystemHelper.Log("frmParametrosPozoEstimadoQ6.Load: Excepción al querer cerrar la pantalla en el load ->" & ex2.Message & "<-")
            End Try
        End Try
    End Sub

    Private Sub btnSalir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub btnguardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnguardar.Click
        Try
            Dim oSorteoBO As New PgmSorteoBO
            Dim oPCBO As New PgmConcursoBO
            Dim oParProxPozoEstimadoBO As New ParProxPozoEstimadoBO
            Dim dt As New DataTable

            'If MsgBox("Esta información actualizará los parametros para el calculo del pozo del próximo sorteo,desea continuar?", MsgBoxStyle.Question + MsgBoxStyle.DefaultButton2 + MsgBoxStyle.YesNo) = MsgBoxResult.No Then Exit Sub

            If Not validarValoresEnPantalla() Then
                Exit Sub
            End If

            asignarValoresDePantallaAlObj() ' valores de controles de pantalla ==> propiedades del obj

            If Not oParProxPozoEstimadoBO.setParProxPozoEstimado(oParProxPozoEstimado) Then
                MsgBox("Se produjo un problema al guardar los Parámetros para cálculo del Próx. Pozo Estimado. Cierre la aplicación y vuelva a intentar. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Critical, MDIContenedor.Text)
            End If

            If Not oSorteoBO.setParProxPozoConfirmado(v_oSorteo, True) Then
                MsgBox("Se produjo un problema al confirmar los Parámetros para cálculo del Próx. Pozo Estimado. Cierre la aplicación y vuelva a intentar. Si el problema persiste consulte a Soporte.", MsgBoxStyle.Critical, MDIContenedor.Text)
            End If

            dt = oPCBO.ObtenerDatosProximosorteo(Me.v_oSorteo.idPgmConcurso, 0)
            If dt IsNot Nothing Then
                For Each r As DataRow In dt.Rows
                    If CStr(r("de_escenario")).StartsWith("1") Then
                        lblEsce1.Text = r("de_escenario")
                        txtEsce1.Text = r("pozo_millones")
                    End If
                    If CStr(r("de_escenario")).StartsWith("2") Then
                        lblEsce2.Text = r("de_escenario")
                        txtEsce2.Text = r("pozo_millones")
                    End If
                    If CStr(r("de_escenario")).StartsWith("3") Then
                        lblEsce3.Text = r("de_escenario")
                        txtEsce3.Text = r("pozo_millones")
                    End If
                    If CStr(r("de_escenario")).StartsWith("4") Then
                        lblEsce4.Text = r("de_escenario")
                        txtEsce4.Text = r("pozo_millones")
                    End If
                    If CStr(r("de_escenario")).StartsWith("5") Then
                        lblEsce5.Text = r("de_escenario")
                        txtEsce5.Text = r("pozo_millones")
                    End If
                    If CStr(r("de_escenario")).StartsWith("6") Then
                        lblEsce6.Text = r("de_escenario")
                        txtEsce6.Text = r("pozo_millones")
                    End If
                    If CStr(r("de_escenario")).StartsWith("7") Then
                        lblEsce7.Text = r("de_escenario")
                        txtEsce7.Text = r("pozo_millones")
                    End If
                    If CStr(r("de_escenario")).StartsWith("8") Then
                        lblEsce8.Text = r("de_escenario")
                        txtEsce8.Text = r("pozo_millones")
                    End If
                Next
                dt = Nothing
            End If

            'Me.Close()
        Catch ex As Exception
            FileSystemHelper.Log("frmParametrosPozoEstimadoQ6.btnguardar_Click - Excepción ->" & ex.Message & "<-")
            MsgBox("Se produjo una EXCEPCION al confirmar los Parámetros para cálculo del Próx. Pozo Estimado. Cierre la aplicación y vuelva a intentar. Si el problema persiste consulte a Soporte.")
        End Try
    End Sub

    Private Sub txtCant_apuestas_estimadas_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_var_apuestas_miercoles_domingos.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_var_apuestas_miercoles_domingos_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_var_apuestas_domingos_miercoles.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_var_apuestas_domingos_miercoles_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_var_apuestas_domingos_miercoles.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_cant_apuesta_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_cant_apuesta_7.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_cant_apuesta_7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtValor_apuesta_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtValor_apuesta_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValor_apuesta_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtValor_apuesta_3.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtValor_apuesta_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValor_apuesta_3.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtValor_apuesta_7.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtValor_apuesta_7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtValor_apuesta_7.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_valor_apuesta_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_valor_apuesta_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_valor_apuesta_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_valor_apuesta_3.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_valor_apuesta_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_valor_apuesta_3.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_valor_apuesta_7.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_valor_apuesta_7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_valor_apuesta_7.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtMinimo_asegurado_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtMinimo_asegurado_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMinimo_asegurado_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtMinimo_asegurado_3.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtMinimo_asegurado_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMinimo_asegurado_3.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPozo_premio_extra.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPozo_premio_extra_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPozo_premio_extra.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPozo_sorteo_adicional.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPozo_sorteo_adicional_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPozo_sorteo_adicional.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_2.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_2.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_3.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_3.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_7.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_7.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_1_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_1_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_1_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_1_2.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_1_2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_1_2.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_1_3.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_1_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_1_3.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_1_esti.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_1_esti_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_1_esti.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_2_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_2_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_2_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_2_2.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_2_2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_2_2.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_2_3.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_2_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_2_3.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_2_esti.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_2_esti_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_2_esti.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_3_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_3_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_3_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_3_esti.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_3_esti_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_3_esti.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_7_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_7_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_7_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_impo_7_esti.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_impo_7_esti_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_impo_7_esti.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_reserva_1.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_reserva_1_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_reserva_1.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_reserva_2.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_reserva_2_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_reserva_2.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_reserva_3.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_reserva_3_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_reserva_3.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                txtPorc_dist_rec_reserva_7.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub
    Private Sub txtPorc_dist_rec_reserva_7_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPorc_dist_rec_reserva_7.KeyPress
        Try
            If e.KeyChar = Convert.ToChar(Keys.Return) Then
                e.Handled = True
                btnguardar.Focus()
            Else
                General.SoloNumerosDecimalesSinMiles(sender, e)
            End If
        Catch ex As Exception
            'MsgBox(Err.Description, MsgBoxStyle.Critical)
        End Try
    End Sub

    ' ----------------------------------------------------------------------------------
    ' Métodos de Asignación

    Private Sub actualizarValoresEnPantalla()
        ' Cuadro Estimación de Apuestas del Próximo Sorteo
        txtPorc_var_apuestas_miercoles_domingos.Text = oParProxPozoEstimado.Porc_var_apuestas_miercoles_domingos.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_var_apuestas_domingos_miercoles.Text = oParProxPozoEstimado.Porc_var_apuestas_domingos_miercoles.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_cant_apuesta_3.Text = oParProxPozoEstimado.Porc_cant_apuesta_3.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_cant_apuesta_7.Text = oParProxPozoEstimado.Porc_cant_apuesta_7.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtCant_apuestas_estimadas.Text = oParProxPozoEstimado.Cant_apuestas_estimadas.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        actualizarApuestas()

        ' Cuadro Valor de la apuesta y Mínimos Asegurados
        txtValor_apuesta_1.Text = oParProxPozoEstimado.Valor_apuesta_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtValor_apuesta_3.Text = oParProxPozoEstimado.Valor_apuesta_3.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtValor_apuesta_7.Text = oParProxPozoEstimado.Valor_apuesta_7.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_valor_apuesta_1.Text = oParProxPozoEstimado.Porc_valor_apuesta_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_valor_apuesta_3.Text = oParProxPozoEstimado.Porc_valor_apuesta_3.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_valor_apuesta_7.Text = oParProxPozoEstimado.Porc_valor_apuesta_7.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtMinimo_asegurado_1.Text = oParProxPozoEstimado.Minimo_asegurado_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtMinimo_asegurado_3.Text = oParProxPozoEstimado.Minimo_asegurado_3.ToString(CultureInfo.CreateSpecificCulture("es-AR"))

        ' Cuadro Otros Pozos
        Dim pe As Long
        Dim pa As Long
        getLong(oParProxPozoEstimado.Pozo_premio_extra.ToString(CultureInfo.CreateSpecificCulture("es-AR")), pe)
        getLong(oParProxPozoEstimado.Pozo_sorteo_adicional.ToString(CultureInfo.CreateSpecificCulture("es-AR")), pa)
        txtPozo_premio_extra.Text = pe.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPozo_sorteo_adicional.Text = pa.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        'txtPozo_premio_extra.Text = oParProxPozoEstimado.Pozo_premio_extra.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        'txtPozo_sorteo_adicional.Text = oParProxPozoEstimado.Pozo_sorteo_adicional.ToString(CultureInfo.CreateSpecificCulture("es-AR"))

        ' Cuadro Distribución de la Recaudación
        txtPorc_dist_rec_1.Text = oParProxPozoEstimado.Porc_dist_rec_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_2.Text = oParProxPozoEstimado.Porc_dist_rec_2.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_3.Text = oParProxPozoEstimado.Porc_dist_rec_3.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_7.Text = oParProxPozoEstimado.Porc_dist_rec_7.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_1_1.Text = oParProxPozoEstimado.Porc_dist_rec_impo_1_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_1_2.Text = oParProxPozoEstimado.Porc_dist_rec_impo_1_2.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_1_3.Text = oParProxPozoEstimado.Porc_dist_rec_impo_1_3.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_1_esti.Text = oParProxPozoEstimado.Porc_dist_rec_impo_1_esti.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_2_1.Text = oParProxPozoEstimado.Porc_dist_rec_impo_2_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_2_2.Text = oParProxPozoEstimado.Porc_dist_rec_impo_2_2.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_2_3.Text = oParProxPozoEstimado.Porc_dist_rec_impo_2_3.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_2_esti.Text = oParProxPozoEstimado.Porc_dist_rec_impo_2_esti.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_3_1.Text = oParProxPozoEstimado.Porc_dist_rec_impo_3_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_3_esti.Text = oParProxPozoEstimado.Porc_dist_rec_impo_3_esti.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_7_1.Text = oParProxPozoEstimado.Porc_dist_rec_impo_7_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_impo_7_esti.Text = oParProxPozoEstimado.Porc_dist_rec_impo_7_esti.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_reserva_1.Text = oParProxPozoEstimado.Porc_dist_rec_reserva_1.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_reserva_2.Text = oParProxPozoEstimado.Porc_dist_rec_reserva_2.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_reserva_3.Text = oParProxPozoEstimado.Porc_dist_rec_reserva_3.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        txtPorc_dist_rec_reserva_7.Text = oParProxPozoEstimado.Porc_dist_rec_reserva_7.ToString(CultureInfo.CreateSpecificCulture("es-AR"))

    End Sub

    Private Sub asignarValoresDePantallaAlObj()
        ' Tabla parametros_pozo
        getDecimal(txtPorc_var_apuestas_miercoles_domingos.Text, oParProxPozoEstimado.Porc_var_apuestas_miercoles_domingos)

        getDecimal(txtPorc_var_apuestas_domingos_miercoles.Text, oParProxPozoEstimado.Porc_var_apuestas_domingos_miercoles)
        getDecimal(txtPozo_premio_extra.Text, oParProxPozoEstimado.Pozo_premio_extra)
        getDecimal(txtPozo_sorteo_adicional.Text, oParProxPozoEstimado.Pozo_sorteo_adicional)
        getLong(txtCant_apuestas_estimadas.Text, oParProxPozoEstimado.Cant_apuestas_estimadas)

        ' Tabla parametros_pozo_modalidad
        getDecimal(txtPorc_dist_rec_1.Text, oParProxPozoEstimado.Porc_dist_rec_1)
        getDecimal(txtPorc_dist_rec_reserva_1.Text, oParProxPozoEstimado.Porc_dist_rec_reserva_1)
        getDecimal(txtValor_apuesta_1.Text, oParProxPozoEstimado.Valor_apuesta_1)
        getDecimal(txtPorc_valor_apuesta_1.Text, oParProxPozoEstimado.Porc_valor_apuesta_1)
        getDecimal(oParProxPozoEstimado.Porc_cant_apuesta_1, "100,00")
        getDecimal(txtMinimo_asegurado_1.Text, oParProxPozoEstimado.Minimo_asegurado_1)

        getDecimal(txtPorc_dist_rec_2.Text, oParProxPozoEstimado.Porc_dist_rec_2)
        getDecimal(txtPorc_dist_rec_reserva_2.Text, oParProxPozoEstimado.Porc_dist_rec_reserva_2)
        getDecimal(txtValor_apuesta_1.Text, oParProxPozoEstimado.Valor_apuesta_2)
        getDecimal(txtPorc_valor_apuesta_1.Text, oParProxPozoEstimado.Porc_valor_apuesta_2)
        getDecimal(oParProxPozoEstimado.Porc_cant_apuesta_2, "100,00")
        getDecimal(txtMinimo_asegurado_1.Text, oParProxPozoEstimado.Minimo_asegurado_2)

        getDecimal(txtPorc_dist_rec_3.Text, oParProxPozoEstimado.Porc_dist_rec_3)
        getDecimal(txtPorc_dist_rec_reserva_3.Text, oParProxPozoEstimado.Porc_dist_rec_reserva_3)
        getDecimal(txtValor_apuesta_3.Text, oParProxPozoEstimado.Valor_apuesta_3)
        getDecimal(txtPorc_valor_apuesta_3.Text, oParProxPozoEstimado.Porc_valor_apuesta_3)
        getDecimal(txtPorc_cant_apuesta_3.Text, oParProxPozoEstimado.Porc_cant_apuesta_3)
        getDecimal(txtMinimo_asegurado_3.Text, oParProxPozoEstimado.Minimo_asegurado_3)

        getDecimal(txtPorc_dist_rec_7.Text, oParProxPozoEstimado.Porc_dist_rec_7)
        getDecimal(txtPorc_dist_rec_reserva_7.Text, oParProxPozoEstimado.Porc_dist_rec_reserva_7)
        getDecimal(txtValor_apuesta_7.Text, oParProxPozoEstimado.Valor_apuesta_7)
        getDecimal(txtPorc_valor_apuesta_7.Text, oParProxPozoEstimado.Porc_valor_apuesta_7)
        getDecimal(txtPorc_cant_apuesta_7.Text, oParProxPozoEstimado.Porc_cant_apuesta_7)
        oParProxPozoEstimado.Minimo_asegurado_7 = 0

        ' Tabla parametros_pozo_premio
        getDecimal(txtPorc_dist_rec_impo_1_1.Text, oParProxPozoEstimado.Porc_dist_rec_impo_1_1)
        getDecimal(txtPorc_dist_rec_impo_1_2.Text, oParProxPozoEstimado.Porc_dist_rec_impo_1_2)
        getDecimal(txtPorc_dist_rec_impo_1_3.Text, oParProxPozoEstimado.Porc_dist_rec_impo_1_3)
        getDecimal(txtPorc_dist_rec_impo_1_esti.Text, oParProxPozoEstimado.Porc_dist_rec_impo_1_esti)
        getDecimal(txtPorc_dist_rec_impo_2_1.Text, oParProxPozoEstimado.Porc_dist_rec_impo_2_1)
        getDecimal(txtPorc_dist_rec_impo_2_2.Text, oParProxPozoEstimado.Porc_dist_rec_impo_2_2)
        getDecimal(txtPorc_dist_rec_impo_2_3.Text, oParProxPozoEstimado.Porc_dist_rec_impo_2_3)
        getDecimal(txtPorc_dist_rec_impo_2_esti.Text, oParProxPozoEstimado.Porc_dist_rec_impo_2_esti)
        getDecimal(txtPorc_dist_rec_impo_3_1.Text, oParProxPozoEstimado.Porc_dist_rec_impo_3_1)
        getDecimal(txtPorc_dist_rec_impo_3_esti.Text, oParProxPozoEstimado.Porc_dist_rec_impo_3_esti)
        getDecimal(txtPorc_dist_rec_impo_7_1.Text, oParProxPozoEstimado.Porc_dist_rec_impo_7_1)
        getDecimal(txtPorc_dist_rec_impo_7_esti.Text, oParProxPozoEstimado.Porc_dist_rec_impo_7_esti)
    End Sub

    ' ----------------------------------------------------------------------------------
    ' Funciones de Conversión

    Private Function getDecimal(ByVal valorSTR As String, ByRef valorDEC As Decimal) As Boolean
        Dim style As NumberStyles
        Dim culture As CultureInfo
        Dim res As Boolean = False
        Dim a As String() = valorSTR.Split(".")
        valorDEC = 0.0
        If a.Count = 2 Then
            valorSTR = valorSTR.Replace(",", "")
            valorSTR = valorSTR.Replace(".", ",")
        End If
        'valorSTR = valorSTR.Replace(".", ",")
        style = NumberStyles.Number Or NumberStyles.AllowDecimalPoint 'Or NumberStyles.AllowThousands
        culture = CultureInfo.CreateSpecificCulture("es-AR")
        res = Decimal.TryParse(valorSTR, style, culture, valorDEC)
        Return res
    End Function

    Private Function getLong(ByVal valorSTR As String, ByRef valorLNG As Long) As Boolean
        Dim style As NumberStyles
        Dim culture As CultureInfo
        Dim res As Boolean = False
        valorLNG = 0
        style = NumberStyles.Number Or NumberStyles.AllowThousands
        culture = CultureInfo.CreateSpecificCulture("es-AR")
        res = Long.TryParse(valorSTR, style, culture, valorLNG)
        Return res
    End Function

    ' ----------------------------------------------------------------------------------
    ' Funciones de Validación

    Private Function esNumero(ByVal lbl As String, ByVal txt As TextBox) As Boolean
        Dim res As Boolean = True
        Dim valorDEC As Decimal
        If (txt.Text.Trim() = "") OrElse (Not getDecimal(txt.Text, valorDEC)) Then
            res = False
            MsgBox("Dato inválido: " & lbl & " debe ser un número.", MsgBoxStyle.Critical, MDIContenedor.Text)
            txt.Focus()
        End If
        Return res
    End Function

    Private Function porcValido(ByVal lbl As String, ByVal txt As TextBox, Optional ByVal conCero As Boolean = False, Optional ByVal conNegativos As Boolean = False) As Boolean
        Dim res As Boolean = True
        Dim val As Decimal = 0.0
        res = getDecimal(txt.Text, val)
        If Not conNegativos Then
            If res AndAlso (val < 0 Or val > 100) Then
                res = False
                MsgBox("Dato inválido: " & lbl & " debe ser un número positivo menor o igual que 100 (cien).", MsgBoxStyle.Critical, MDIContenedor.Text)
                txt.Focus()
            ElseIf res AndAlso (val = 0 And (Not conCero)) Then
                res = False
                MsgBox("Dato inválido: " & lbl & " debe ser un número positivo mayor que 0 (cero) y menor o igual que 100 (cien).", MsgBoxStyle.Critical, MDIContenedor.Text)
                txt.Focus()
            End If
        Else
            If res And (val < -100 Or val > 100) Then
                res = False
                MsgBox("Dato inválido: " & lbl & " debe ser un número entre -100 y 100.", MsgBoxStyle.Critical, MDIContenedor.Text)
                txt.Focus()
            ElseIf res AndAlso (val = 0 And (Not conCero)) Then
                res = False
                MsgBox("Dato inválido: " & lbl & " debe ser un número positivo mayor que 0 (cero) y menor o igual que 100 (cien).", MsgBoxStyle.Critical, MDIContenedor.Text)
                txt.Focus()
            End If
        End If
        Return res
    End Function

    Private Function CantValida(ByVal lbl As String, ByVal txt As TextBox) As Boolean
        Dim res As Boolean = True
        Dim val As Long = 0
        res = getLong(txt.Text, val)
        If res AndAlso (val <= 0 Or val > 2000000000) Then
            res = False
            MsgBox("Dato inválido: " & lbl & " debe ser un número ENTERO mayor que 0 (cero).", MsgBoxStyle.Critical, MDIContenedor.Text)
            txt.Focus()
        End If
        Return res
    End Function

    Private Function valorApuValida(ByVal lbl As String, ByVal txt As TextBox) As Boolean
        Dim res As Boolean = True
        Dim val As Decimal = 0.0
        res = getDecimal(txt.Text, val)
        If res AndAlso (val <= 0 Or val > 10000) Then
            res = False
            MsgBox("Dato inválido: " & lbl & " debe ser un número mayor que 0 (cero) y menor que 10.000 (diez mil).", MsgBoxStyle.Critical, MDIContenedor.Text)
            txt.Focus()
        End If
        Return res
    End Function

    Private Function importeValido(ByVal lbl As String, ByVal txt As TextBox, Optional ByVal conCero As Boolean = False) As Boolean
        Dim res As Boolean = True
        Dim val As Decimal = 0.0
        res = getDecimal(txt.Text, val)
        If res AndAlso (val < 0 Or val > 2000000000) Then
            res = False
            MsgBox("Dato inválido: " & lbl & " debe ser un número positivo.", MsgBoxStyle.Critical, MDIContenedor.Text)
            txt.Focus()
        ElseIf res AndAlso (val = 0 And (Not conCero)) Then
            res = False
            MsgBox("Dato inválido: " & lbl & " debe ser un número positivo mayor que 0 (cero).", MsgBoxStyle.Critical, MDIContenedor.Text)
            txt.Focus()
        End If
        Return res
    End Function

    Private Function validarEsNumero() As Boolean
        ' Cuadro Estimación de Apuestas del Próximo Sorteo
        'txtPorc_var_apuestas_miercoles_domingos.Text
        If Not esNumero(Lblmiercoles.Text, txtPorc_var_apuestas_miercoles_domingos) Then
            Return False
        End If
        'txtPorc_var_apuestas_domingos_miercoles.Text
        If Not esNumero(lbldomingo.Text, txtPorc_var_apuestas_domingos_miercoles) Then
            Return False
        End If
        'txtCant_apuestas_estimadas.Text
        If Not esNumero(lblApuestas.Text, txtCant_apuestas_estimadas) Then
            Return False
        End If
        'txtPorc_cant_apuesta_3.Text
        If Not esNumero(Lblrevancha.Text, txtPorc_cant_apuesta_3) Then
            Return False
        End If
        'txtPorc_cant_apuesta_7.Text
        If Not esNumero(lblporc_apu_ss.Text, txtPorc_cant_apuesta_7) Then
            Return False
        End If

        ' Cuadro Valor de la apuesta y Mínimos Asegurados
        'txtValor_apuesta_1.Text
        If Not esNumero(lbl_apu_tradicional.Text, txtValor_apuesta_1) Then
            Return False
        End If
        'txtValor_apuesta_3.Text
        If Not esNumero(Lbl_apu_revancha.Text, txtValor_apuesta_3) Then
            Return False
        End If
        'txtValor_apuesta_7.Text
        If Not esNumero(Lbl_apu_ss.Text, txtValor_apuesta_7) Then
            Return False
        End If
        'txtPorc_valor_apuesta_1.Text
        If Not esNumero(lblPropParaCalc.Text & " de " & lbl_apu_tradicional.Text, txtPorc_valor_apuesta_1) Then
            Return False
        End If
        'txtPorc_valor_apuesta_3.Text
        If Not esNumero(lblPropParaCalc.Text & " de " & Lbl_apu_revancha.Text, txtPorc_valor_apuesta_3) Then
            Return False
        End If
        'txtPorc_valor_apuesta_7.Text
        If Not esNumero(lblPropParaCalc.Text & " de " & Lbl_apu_ss.Text, txtPorc_valor_apuesta_7) Then
            Return False
        End If
        'txtMinimo_asegurado_1.Text
        If Not esNumero(lblMinAseg.Text & " de " & lbl_apu_tradicional.Text, txtMinimo_asegurado_1) Then
            Return False
        End If
        'txtMinimo_asegurado_3.Text
        If Not esNumero(lblMinAseg.Text & " de " & Lbl_apu_revancha.Text, txtMinimo_asegurado_3) Then
            Return False
        End If

        ' Cuadro Otros Pozos
        'txtPozo_premio_extra.Text
        If Not esNumero(Lbl_pozoextra.Text, txtPozo_premio_extra) Then
            Return False
        End If
        'txtPozo_sorteo_adicional.Text
        If Not esNumero(lblPozoAdic.Text, txtPozo_sorteo_adicional) Then
            Return False
        End If

        ' Cuadro Distribución de la Recaudación
        'txtPorc_dist_rec_1.Text
        If Not esNumero(lblDistPorMod.Text & " de " & lblMod1.Text, txtPorc_dist_rec_1) Then
            Return False
        End If
        'txtPorc_dist_rec_2.Text
        If Not esNumero(lblDistPorMod.Text & " de " & lblMod2.Text, txtPorc_dist_rec_2) Then
            Return False
        End If
        'txtPorc_dist_rec_3.Text
        If Not esNumero(lblDistPorMod.Text & " de " & lblMod3.Text, txtPorc_dist_rec_3) Then
            Return False
        End If
        'txtPorc_dist_rec_7.Text
        If Not esNumero(lblDistPorMod.Text & " de " & lblMod7.Text, txtPorc_dist_rec_7) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_1_1.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblPrimero.Text, txtPorc_dist_rec_impo_1_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_1_2.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblSegundo.Text, txtPorc_dist_rec_impo_1_2) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_1_3.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblTercero.Text, txtPorc_dist_rec_impo_1_3) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_1_esti.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblEstimulo.Text, txtPorc_dist_rec_impo_1_esti) Then
            Return False
        End If
        'txtPorc_dist_rec_reserva_1.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblReserva.Text, txtPorc_dist_rec_reserva_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_2_1.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblPrimero.Text, txtPorc_dist_rec_impo_2_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_2_2.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblSegundo.Text, txtPorc_dist_rec_impo_2_2) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_2_3.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblTercero.Text, txtPorc_dist_rec_impo_2_3) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_2_esti.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblEstimulo.Text, txtPorc_dist_rec_impo_2_esti) Then
            Return False
        End If
        'txtPorc_dist_rec_reserva_2.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblReserva.Text, txtPorc_dist_rec_reserva_2) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_3_1.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod3.Text & " " & lblPrimero.Text, txtPorc_dist_rec_impo_3_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_3_esti.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod3.Text & " " & lblEstimulo.Text, txtPorc_dist_rec_impo_3_esti) Then
            Return False
        End If
        'txtPorc_dist_rec_reserva_3.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod3.Text & " " & lblReserva.Text, txtPorc_dist_rec_reserva_3) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_7_1.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod7.Text & " " & lblPrimero.Text, txtPorc_dist_rec_impo_7_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_7_esti.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod7.Text & " " & lblEstimulo.Text, txtPorc_dist_rec_impo_7_esti) Then
            Return False
        End If
        'txtPorc_dist_rec_reserva_7.Text
        If Not esNumero(lblDistPorPre.Text & " de " & lblMod7.Text & " " & lblReserva.Text, txtPorc_dist_rec_reserva_7) Then
            Return False
        End If
        Return True
    End Function

    Private Function validarImportes() As Boolean
        'txtPozo_premio_extra.Text
        If Not importeValido(Lbl_pozoextra.Text, txtPozo_premio_extra) Then
            Return False
        End If
        'txtPozo_sorteo_adicional.Text
        If Not importeValido(lblPozoAdic.Text, txtPozo_sorteo_adicional, True) Then
            Return False
        End If
        'txtMinimo_asegurado_1.Text
        If Not importeValido(lblMinAseg.Text & " de " & lbl_apu_tradicional.Text, txtMinimo_asegurado_1) Then
            Return False
        End If
        'txtMinimo_asegurado_3.Text
        If Not importeValido(lblMinAseg.Text & " de " & Lbl_apu_revancha.Text, txtMinimo_asegurado_3) Then
            Return False
        End If
        Return True
    End Function

    Private Function validarCant() As Boolean
        'txtCant_apuestas_estimadas.Text
        If Not CantValida(lblApuestas.Text, txtCant_apuestas_estimadas) Then
            Return False
        End If
        Return True
    End Function

    Private Function validarValApu() As Boolean
        'txtValor_apuesta_1.Text
        If Not valorApuValida(lbl_apu_tradicional.Text, txtValor_apuesta_1) Then
            Return False
        End If
        'txtValor_apuesta_3.Text
        If Not valorApuValida(Lbl_apu_revancha.Text, txtValor_apuesta_3) Then
            Return False
        End If
        'txtValor_apuesta_7.Text
        If Not valorApuValida(Lbl_apu_ss.Text, txtValor_apuesta_7) Then
            Return False
        End If
        Return True
    End Function

    Private Function validarPorcentajes() As Boolean
        ' Cuadro Estimación de Apuestas del Próximo Sorteo
        'txtPorc_var_apuestas_miercoles_domingos.Text
        If Not porcValido(Lblmiercoles.Text, txtPorc_var_apuestas_miercoles_domingos, , True) Then
            Return False
        End If
        'txtPorc_var_apuestas_domingos_miercoles.Text
        If Not porcValido(lbldomingo.Text, txtPorc_var_apuestas_domingos_miercoles, , True) Then
            Return False
        End If
        'txtPorc_cant_apuesta_3.Text
        If Not porcValido(Lblrevancha.Text, txtPorc_cant_apuesta_3) Then
            Return False
        End If
        'txtPorc_cant_apuesta_7.Text
        If Not porcValido(lblporc_apu_ss.Text, txtPorc_cant_apuesta_7) Then
            Return False
        End If

        ' Cuadro Valor de la apuesta y Mínimos Asegurados
        'txtPorc_valor_apuesta_1.Text
        If Not porcValido(lblPropParaCalc.Text & " de " & lbl_apu_tradicional.Text, txtPorc_valor_apuesta_1) Then
            Return False
        End If
        'txtPorc_valor_apuesta_3.Text
        If Not porcValido(lblPropParaCalc.Text & " de " & Lbl_apu_revancha.Text, txtPorc_valor_apuesta_3) Then
            Return False
        End If
        'txtPorc_valor_apuesta_7.Text
        If Not porcValido(lblPropParaCalc.Text & " de " & Lbl_apu_ss.Text, txtPorc_valor_apuesta_7) Then
            Return False
        End If

        ' Cuadro Distribución de la Recaudación
        'txtPorc_dist_rec_1.Text
        If Not porcValido(lblDistPorMod.Text & " de " & lblMod1.Text, txtPorc_dist_rec_1) Then
            Return False
        End If
        'txtPorc_dist_rec_2.Text
        If Not porcValido(lblDistPorMod.Text & " de " & lblMod2.Text, txtPorc_dist_rec_2) Then
            Return False
        End If
        'txtPorc_dist_rec_3.Text
        If Not porcValido(lblDistPorMod.Text & " de " & lblMod3.Text, txtPorc_dist_rec_3) Then
            Return False
        End If
        'txtPorc_dist_rec_7.Text
        If Not porcValido(lblDistPorMod.Text & " de " & lblMod7.Text, txtPorc_dist_rec_7) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_1_1.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblPrimero.Text, txtPorc_dist_rec_impo_1_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_1_2.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblSegundo.Text, txtPorc_dist_rec_impo_1_2) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_1_3.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblTercero.Text, txtPorc_dist_rec_impo_1_3) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_1_esti.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblEstimulo.Text, txtPorc_dist_rec_impo_1_esti) Then
            Return False
        End If
        'txtPorc_dist_rec_reserva_1.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod1.Text & " " & lblReserva.Text, txtPorc_dist_rec_reserva_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_2_1.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblPrimero.Text, txtPorc_dist_rec_impo_2_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_2_2.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblSegundo.Text, txtPorc_dist_rec_impo_2_2) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_2_3.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblTercero.Text, txtPorc_dist_rec_impo_2_3) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_2_esti.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblEstimulo.Text, txtPorc_dist_rec_impo_2_esti) Then
            Return False
        End If
        'txtPorc_dist_rec_reserva_2.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod2.Text & " " & lblReserva.Text, txtPorc_dist_rec_reserva_2) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_3_1.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod3.Text & " " & lblPrimero.Text, txtPorc_dist_rec_impo_3_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_3_esti.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod3.Text & " " & lblEstimulo.Text, txtPorc_dist_rec_impo_3_esti) Then
            Return False
        End If
        'txtPorc_dist_rec_reserva_3.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod3.Text & " " & lblReserva.Text, txtPorc_dist_rec_reserva_3) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_7_1.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod7.Text & " " & lblPrimero.Text, txtPorc_dist_rec_impo_7_1) Then
            Return False
        End If
        'txtPorc_dist_rec_impo_7_esti.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod7.Text & " " & lblEstimulo.Text, txtPorc_dist_rec_impo_7_esti) Then
            Return False
        End If
        'txtPorc_dist_rec_reserva_7.Text
        If Not porcValido(lblDistPorPre.Text & " de " & lblMod7.Text & " " & lblReserva.Text, txtPorc_dist_rec_reserva_7) Then
            Return False
        End If
        Return True
    End Function

    Private Function validarValoresEnPantalla() As Boolean
        If Not validarEsNumero() Then
            Return False
        End If
        If Not validarPorcentajes() Then
            Return False
        End If
        If Not validarCant() Then
            Return False
        End If
        If Not validarValApu() Then
            Return False
        End If
        If Not validarImportes() Then
            Return False
        End If
        Return True
    End Function

    Private Sub actualizarApuestas()
        Dim porc As Decimal = 0.0
        Dim porc_3 As Decimal = 0.0
        Dim porc_7 As Decimal = 0.0

        Dim apu_est As Long = 0

        If txtCant_apuestas_estimadas.Text.Trim = "0" Or txtCant_apuestas_estimadas.Text.Trim = "" Then
            If v_oSorteo.fechaHora.ToString("ddd").ToUpper() = "WED" OrElse v_oSorteo.fechaHora.ToString("ddd").ToUpper() = "MIE" Then
                porc = oParProxPozoEstimado.Porc_var_apuestas_miercoles_domingos
            Else
                porc = oParProxPozoEstimado.Porc_var_apuestas_domingos_miercoles
            End If
            For Each po As Pozo In v_oSorteo.pozos
                If po.idPremio = 401001 OrElse po.idPremio = 1301001 Then
                    apu_est = apu_est + po.Apuestas
                End If
            Next
            apu_est = General.RedondearLong(apu_est + (apu_est * porc / 100), 3)
        Else
            getLong(txtCant_apuestas_estimadas.Text, apu_est)
            ' apu_est = oParProxPozoEstimado.Cant_apuestas_estimadas
        End If
        getDecimal(txtPorc_cant_apuesta_3.Text, porc_3)
        getDecimal(txtPorc_cant_apuesta_7.Text, porc_7)
        txtCant_apuestas_estimadas.Text = apu_est.ToString(CultureInfo.CreateSpecificCulture("es-AR"))
        lblCant_apuestas_estimadas_3.Text = General.RedondearLong(((apu_est * porc_3) / 100.0), 0).ToString(CultureInfo.CreateSpecificCulture("es-AR")) & " u   [C22]"
        lblCant_apuestas_estimadas_7.Text = General.RedondearLong(((apu_est * porc_7) / 100.0), 0).ToString(CultureInfo.CreateSpecificCulture("es-AR")) & " u   [C33]"

    End Sub

    Private Sub txtCant_apuestas_estimadas_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        actualizarApuestas()
    End Sub

    Private Sub txtPorc_cant_apuesta_3_LostFocus(ByVal sender As Object, ByVal e As System.EventArgs)
        actualizarApuestas()
    End Sub

    Private Sub txtPorc_cant_apuesta_3_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        actualizarApuestas()
    End Sub

    Private Sub txtCant_apuestas_estimadas_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        actualizarApuestas()
    End Sub

    Private Sub txtPorc_cant_apuesta_7_TextChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
        actualizarApuestas()
    End Sub

End Class