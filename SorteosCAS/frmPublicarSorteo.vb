Imports sorteos.helpers
Imports libEntities.Entities
Imports sorteos.bussiness
'Imports System.Data.SqlClient
Imports System.IO

Public Class frmPublicarSorteo
    Private _destinoPublicacion As String = ""
    Public Property DestinoPublicacion() As String
        Get
            Return _destinoPublicacion
        End Get
        Set(ByVal value As String)
            _destinoPublicacion = value
            If _destinoPublicacion.ToUpper = "DISPLAY" Then
                Me.Text = "(Re) Publicación a Display"
            End If
            If _destinoPublicacion.ToUpper = "WEB" Then
                Me.Text = "(Re) Publicación a Web"
            End If
        End Set
    End Property

    Private Sub frmPublicarSorteo_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing
        MDIContenedor.m_ChildFormNumber = MDIContenedor.m_ChildFormNumber - 1
    End Sub
    Private Sub frmActualizaWeb_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Dim boJuego As New JuegoBO
        Dim lstJuegos As ListaOrdenada(Of Juego)
        Me.Location = New System.Drawing.Point(0, 0)
        If _destinoPublicacion = "" Then
            MsgBox("No se ha indicado el destino de publicación. No re realiza el proceso. Verifique.")
            Try
                Me.Close()
                Me.Dispose()
            Catch ex As Exception
            End Try
            Exit Sub
        End If
        If _destinoPublicacion <> "WEB" Then
            btnAnular.Enabled = False
            btnAnular.Visible = False
            chkForzar.Visible = True
            Label3.Visible = True
        Else
            chkForzar.Visible = False
            Label3.Visible = False
        End If
        lstJuegos = boJuego.getJuego()
        cmbJuego.DataSource = lstJuegos
        cmbJuego.ValueMember = "IdJuego"
        cmbJuego.DisplayMember = "Jue_Desc"
        cmbJuego.Refresh()
    End Sub

    Private Sub btnActualizar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnActualizar.Click
        Dim oSorteo As New PgmSorteo
        Dim boSorteo As New PgmSorteoBO
        Dim oJuegoBO As New JuegoBO
        Dim sorteoLotBO As New PgmSorteoLoteriaBO
        Dim _errorAnular As Boolean = False
        Dim _errorPublicar As Boolean = False
        '*** IAFAS**********
        Dim _publicarweboff As String = General.PublicarWebOFF
        Dim _publicarwebon As String = General.PublicarWebON
        Dim _PublicaExtractosWSRestOFF As String = General.PublicaExtractosWSRestOFF
        Dim _PublicaExtractosWSRestON As String = General.PublicaExtractosWSRestON

        If Me.DestinoPublicacion.ToUpper = "WEB" Then
            If _publicarweboff = "N" And _publicarwebon = "N" Then
                MsgBox("La publicación a Web no está habilitada en la aplicación", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If
        ' ***** valida las entradas **************
        If cmbJuego.SelectedValue = 0 Or txtNroSorteo.Text = "" Then
            MsgBox("Indique número de sorteo y juego a actualizar.")
            Exit Sub
        End If

        oSorteo.idPgmSorteo = boSorteo.getPgmSorteoId(cmbJuego.SelectedValue, txtNroSorteo.Text)
        If oSorteo.idPgmSorteo = 0 Then
            MsgBox("No existe el número de sorteo para el juego indicado.")
            Exit Sub
        End If
        ' ***************************************
        If DestinoPublicacion.ToUpper = "WEB" Then
            lblpublicando.Text = "Anulando..."
            lblpublicando.Visible = True
            Me.Refresh()
            Me.Cursor = Cursors.WaitCursor
            btnAnular.Enabled = False
            btnActualizar.Enabled = False

            'AGREGADO POR FSCOTTA

            'ME PARECE QUE TENDRIAMOS QUE LLAMAR A LA ANULACION REST SI CORRESPONDE, 
            'PERO ME ESTA CONFUNFIENDO UN POCO LA LOGICA DE ESTE BLOQUE DONDE VERIFICA SI ANULAR OTRAS JURISDICCIONES 

            'If _PublicaExtractosWSRestOFF <> "N" And _PublicaExtractosWSRestON <> "N" Then
            '    Try
            '        oSorteo = boSorteo.getPgmSorteo(oSorteo.idPgmSorteo)

            '        ' Anulo otras jurisdicciones, si existen
            '        Dim oexlot As pgmSorteo_loteria
            '        For Each oexlot In oSorteo.ExtraccionesLoteria
            '            sorteoLotBO.AnularWeb(oSorteo, oexlot.Loteria.IdLoteria, True)
            '        Next

            '        ' Por ultimo anulo SAnta FE
            '        '** quita la marca de confirmado parcial si corresponde a una loteria
            '        If oSorteo.idJuego = 2 Or oSorteo.idJuego = 3 Or oSorteo.idJuego = 8 Or oSorteo.idJuego = 49 Then
            '            boSorteo.AnularQuinielaSF(oSorteo, oSorteo.fechaHoraPrescripcion, oSorteo.fechaHoraProximo, oSorteo.fechaHoraIniReal, oSorteo.fechaHora, oSorteo.fechaHoraFinReal)
            '        End If
            '        sorteoLotBO.AnularWeb(oSorteo, General.Jurisdiccion, True)

            '    Catch ex As Exception
            '        _errorAnular = True
            '        FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " " & Me.Text & " -> " & ex.Message)
            '        lblpublicando.Text = "Finalizado...Hubo problemas en la anulación."
            '        Me.Cursor = Cursors.Default
            '        'btnAnular.Enabled = True
            '        btnActualizar.Enabled = True
            '        Exit Sub
            '    End Try
            'End If
            '**************************

            Try
                oSorteo = boSorteo.getPgmSorteo(oSorteo.idPgmSorteo)

                ' Anulo otras jurisdicciones, si existen
                Dim oexlot As pgmSorteo_loteria
                For Each oexlot In oSorteo.ExtraccionesLoteria
                    sorteoLotBO.AnularWeb(oSorteo, oexlot.Loteria.IdLoteria, True)
                Next

                ' Por ultimo anulo SAnta FE
                '** quita la marca de confirmado parcial si corresponde a una loteria
                If ojuegoBO.esquiniela(oSorteo.idJuego) Then
                    boSorteo.AnularQuinielaSF(oSorteo, oSorteo.fechaHoraPrescripcion, oSorteo.fechaHoraProximo, oSorteo.fechaHoraIniReal, oSorteo.fechaHora, oSorteo.fechaHoraFinReal)
                End If
                sorteoLotBO.AnularWeb(oSorteo, General.Jurisdiccion, True)

            Catch ex As Exception
                _errorAnular = True
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " " & Me.Text & " -> " & ex.Message)
                lblpublicando.Text = "Finalizado...Hubo problemas en la anulación."
                Me.Cursor = Cursors.Default
                'btnAnular.Enabled = True
                btnActualizar.Enabled = True
                ''Exit Sub
            End Try
        End If

        ' ***************************************
        lblpublicando.Text = "Publicando..."
        lblpublicando.Visible = True
        Me.Refresh()
        Me.Cursor = Cursors.WaitCursor
        If btnAnular.Visible Then btnAnular.Enabled = False
        btnActualizar.Enabled = False

        'AGREGADO POR FSCOTTA

        If _PublicaExtractosWSRestOFF <> "N" And _PublicaExtractosWSRestON <> "N" Then
            Try
                oSorteo = boSorteo.getPgmSorteo(oSorteo.idPgmSorteo)
                If DestinoPublicacion.ToUpper = "DISPLAY" Then
                    boSorteo.publicarWEBRest(oSorteo, chkForzar.Checked)
                Else
                    boSorteo.publicarWEBRest(oSorteo, True)
                End If
            Catch ex As Exception
                _errorPublicar = True
                FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " " & Me.Text & " -> " & ex.Message)
            Finally
                If _errorPublicar Then
                    lblpublicando.Text = "Finalizado...Hubo problemas en la publicación"
                Else
                    lblpublicando.Text = "Finalizado..."
                End If

                Me.Cursor = Cursors.Default
                If DestinoPublicacion.ToUpper <> "DISPLAY" Then
                    btnAnular.Enabled = True
                End If
                btnActualizar.Enabled = True
            End Try
        End If


        Try
            oSorteo = boSorteo.getPgmSorteo(oSorteo.idPgmSorteo)
            If DestinoPublicacion.ToUpper = "DISPLAY" Then
                boSorteo.publicarDisplay(oSorteo, chkForzar.Checked)
            Else
                boSorteo.publicarWEB(oSorteo, True)
            End If
        Catch ex As Exception
            _errorPublicar = True
            FileSystemHelper.Log(MDIContenedor.usuarioAutenticado.Usuario & " " & Me.Text & " -> " & ex.Message)
        Finally
            If _errorPublicar Then
                lblpublicando.Text = "Finalizado...Hubo problemas en la publicación"
            Else
                lblpublicando.Text = "Finalizado..."
            End If

            Me.Cursor = Cursors.Default
            If DestinoPublicacion.ToUpper <> "DISPLAY" Then
                btnAnular.Enabled = True
            End If
            btnActualizar.Enabled = True
        End Try
    End Sub

    Private Sub btnAnular_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnular.Click
        Dim oSorteo As New PgmSorteo
        Dim boSorteo As New PgmSorteoBO
        Dim sorteoLotBO As New PgmSorteoLoteriaBO
        Dim oJuegoBO As New JuegoBO
        Dim _errorAnular As Boolean = False
        Dim _PublicarWebOFF As String = General.PublicarWebOFF
        If Me.DestinoPublicacion.ToUpper = "WEB" Then
            If _PublicarWebOFF = "N" Then
                MsgBox("La anulación a Web no está habilitada en la aplicación", MsgBoxStyle.Information)
                Exit Sub
            End If
        End If
        ' ***** valida las entradas **************
        If cmbJuego.SelectedValue = 0 Or txtNroSorteo.Text = "" Then
            MsgBox("Indique número de sorteo y juego a actualizar.")
            Exit Sub
        End If

        oSorteo.idPgmSorteo = boSorteo.getPgmSorteoId(cmbJuego.SelectedValue, txtNroSorteo.Text)
        If oSorteo.idPgmSorteo = 0 Then
            MsgBox("No existe el número de sorteo para el juego indicado.")
            Exit Sub
        End If
        ' ***************************************
        lblpublicando.Text = "Anulando..."
        lblpublicando.Visible = True
        Me.Refresh()
        Me.Cursor = Cursors.WaitCursor
        btnAnular.Enabled = False
        btnActualizar.Enabled = False

        Try
            oSorteo = boSorteo.getPgmSorteo(oSorteo.idPgmSorteo)
            If DestinoPublicacion.ToUpper = "WEB" Then
                ' Anulo otras jurisdicciones, si existen
                Dim oexlot As pgmSorteo_loteria
                For Each oexlot In oSorteo.ExtraccionesLoteria
                    sorteoLotBO.AnularWeb(oSorteo, oexlot.Loteria.IdLoteria, True)
                Next
            End If
            ' Por ultimo anulo SAnta FE
            '** quita la marca de confirmado parcial si corresponde a una loteria
            If ojuegobo.esquiniela(oSorteo.idJuego) Then
                boSorteo.AnularQuinielaSF(oSorteo, oSorteo.fechaHoraPrescripcion, oSorteo.fechaHoraProximo, oSorteo.fechaHoraIniReal, oSorteo.fechaHora, oSorteo.fechaHoraFinReal)
            End If
            sorteoLotBO.AnularWeb(oSorteo, General.Jurisdiccion, True)
        Catch ex As Exception
            _errorAnular = True
            FileSystemHelper.Log(mdicontenedor.usuarioAutenticado.Usuario & " " & Me.Text & " -> " & ex.Message)
        Finally
            Me.Cursor = Cursors.Default
            If _errorAnular Then
                lblpublicando.Text = "Finalizado...Hubo problemas en la anulación"
            Else
                lblpublicando.Text = "Finalizado..."
            End If

            btnAnular.Enabled = True
            btnActualizar.Enabled = True
        End Try

    End Sub

    Private Sub btnAnular_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnular.EnabledChanged
        If btnAnular.Enabled Then
            btnAnular.BackgroundImageLayout = ImageLayout.Stretch
            btnAnular.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnAnular.BackgroundImageLayout = ImageLayout.Stretch
            btnAnular.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnAnular_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnAnular.MouseDown
        btnAnular.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnAnular_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnular.MouseHover
        btnAnular.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnAnular_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnAnular.MouseLeave
        If btnAnular.Enabled Then
            btnAnular.BackgroundImageLayout = ImageLayout.Stretch
            btnAnular.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnAnular.BackgroundImageLayout = ImageLayout.Stretch
            btnAnular.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnActualizar_EnabledChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizar.EnabledChanged
        If btnActualizar.Enabled Then
            btnActualizar.BackgroundImageLayout = ImageLayout.Stretch
            btnActualizar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnActualizar.BackgroundImageLayout = ImageLayout.Stretch
            btnActualizar.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub

    Private Sub btnActualizar_MouseDown(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles btnActualizar.MouseDown
        btnActualizar.BackgroundImage = My.Resources.Imagenes.boton_press
    End Sub

    Private Sub btnActualizar_MouseHover(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizar.MouseHover
        btnActualizar.BackgroundImage = My.Resources.Imagenes.boton_over
    End Sub

    Private Sub btnActualizar_MouseLeave(ByVal sender As Object, ByVal e As System.EventArgs) Handles btnActualizar.MouseLeave
        If btnActualizar.Enabled Then
            btnActualizar.BackgroundImageLayout = ImageLayout.Stretch
            btnActualizar.BackgroundImage = My.Resources.Imagenes.boton_normal
        Else
            btnActualizar.BackgroundImageLayout = ImageLayout.Stretch
            btnActualizar.BackgroundImage = My.Resources.Imagenes.boton_off

        End If
    End Sub
End Class