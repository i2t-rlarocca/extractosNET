Imports Sorteos.Helpers
Imports libEntities.Entities
Imports Sorteos.Bussiness
Public Class PremioExtra
    Dim lsValores As ListaOrdenada(Of String)
    Dim ExtraccionesBO As New ExtraccionesBO
    Dim lsNorepetidos As ListaOrdenada(Of String)
    Public idPgmConcurso As Integer
    Public NroSorteo As Integer

    Private Sub PremioExtra_KeyDown(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyEventArgs) Handles Me.KeyDown
        If UCase(e.KeyCode.ToString()) = "ESCAPE" Then
            Me.Close()
        End If
    End Sub

    Private Sub PremioExtra_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Dim Titulo As String = ""
        Try
            Titulo = "QUINI 6 - SORTEO NRO " & NroSorteo & " - PREMIO EXTRA"
            lblTitulo.Text = Titulo
            lsValores = ExtraccionesBO.getExtraccionesQuini6(idPgmConcurso)
            CrearControles(lsValores)
        Catch ex As Exception
            MsgBox("Error PremioExtra_Load:" & ex.Message, MsgBoxStyle.Information)
        End Try

    End Sub

    Private Sub PremioExtra_MouseDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles Me.MouseDoubleClick
        Me.Close()
    End Sub
    Private Function CrearControles(ByVal ls As ListaOrdenada(Of String))
        Dim _valor As String
        Dim contador As Integer
        Dim Fuente As Font
        Dim FuenteRepetido As Font
        Dim pLeft As Integer
        Dim pTop As Integer

        Try
            pLeft = 34
            pTop = 39
            Fuente = New Font("Microsoft Sans Serif", 64, FontStyle.Bold)
            FuenteRepetido = New Font("Microsoft Sans Serif", 64, FontStyle.Bold Or FontStyle.Strikeout)
            contador = 1
            For Each _valor In ls
                If contador <= 6 Then
                    If ActualizaListaNoRepetido(_valor) Then
                        imagen1.Controls.Add(CrearEtiqueta("lblNro" & contador, _valor, pLeft, pTop, 153, 108, Fuente))
                    Else
                        imagen1.Controls.Add(CrearEtiqueta("lblNro" & contador, _valor, pLeft, pTop, 153, 108, Fuente))
                    End If
                End If
                If contador = 7 Then
                    pLeft = 34
                    pTop = 41
                ElseIf contador = 13 Then
                    pLeft = 34
                    pTop = 42
                End If
                If contador > 6 And contador <= 12 Then
                    If ActualizaListaNoRepetido(_valor) Then
                        imagen2.Controls.Add(CrearEtiqueta("lblNro" & contador, _valor, pLeft, pTop, 153, 108, Fuente))
                    Else
                        imagen2.Controls.Add(CrearEtiquetaRepetida("lblNro" & contador, _valor, pLeft, pTop, 153, 108, FuenteRepetido))
                    End If
                End If
                If contador > 12 Then
                    If ActualizaListaNoRepetido(_valor) Then
                        imagen3.Controls.Add(CrearEtiqueta("lblNro" & contador, _valor, pLeft, pTop, 153, 108, Fuente))
                    Else
                        imagen3.Controls.Add(CrearEtiquetaRepetida("lblNro" & contador, _valor, pLeft, pTop, 153, 108, FuenteRepetido))
                    End If
                End If
                pLeft = pLeft + 156
                contador = contador + 1
            Next

        Catch ex As Exception
            MsgBox("Error crearControles:" & ex.Message, MsgBoxStyle.Information)
        End Try

    End Function
    Private Function ActualizaListaNoRepetido(ByVal pValor As String) As Boolean
        Try
            ActualizaListaNoRepetido = True
            If lsNorepetidos Is Nothing Then
                lsNorepetidos = New ListaOrdenada(Of String)
                lsNorepetidos.Add(pValor)
                Exit Function
            Else
                If Not EstaRepetido(pValor) Then
                    lsNorepetidos.Add(pValor)
                    Exit Function
                Else
                    ActualizaListaNoRepetido = False
                End If
            End If
        Catch ex As Exception
            MsgBox("Error ActualizaListaNoRepetido:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Function
    Private Function EstaRepetido(ByVal _valor As String) As Boolean
        Dim pvalor As String
        Try
            EstaRepetido = False
            If lsNorepetidos Is Nothing Then
                Exit Function
            End If
            For Each pvalor In lsNorepetidos
                If _valor = pvalor Then
                    EstaRepetido = True
                    Exit Function
                End If
            Next
        Catch ex As Exception
            MsgBox("Error EstaRepetido:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Function
    Private Function CrearEtiqueta(ByVal pNombre As String, ByVal pTexto As String, ByVal pLef As Integer, ByVal pTop As Integer, ByVal pAncho As Integer, ByVal pAlto As Integer, Optional ByVal Fuente As Font = Nothing, Optional ByVal pVisible As Boolean = True) As Label
        Dim Etiqueta As New Label
        Try
            Etiqueta.Name = pNombre
            Etiqueta.Text = pTexto
            Etiqueta.Top = pTop
            Etiqueta.Left = pLef
            Etiqueta.Width = pAncho
            Etiqueta.Height = pAlto
            If Not Fuente Is Nothing Then
                Etiqueta.Font = Fuente
            End If
            Etiqueta.Visible = pVisible
            Etiqueta.BackColor = Color.Transparent
            Etiqueta.SendToBack()
            Return Etiqueta
        Catch ex As Exception
            MsgBox("Error CrearEtiqueta" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Function
    Private Function CrearEtiquetaRepetida(ByVal pNombre As String, ByVal pTexto As String, ByVal pLef As Integer, ByVal pTop As Integer, ByVal pAncho As Integer, ByVal pAlto As Integer, Optional ByVal Fuente As Font = Nothing, Optional ByVal pVisible As Boolean = True) As Label
        Dim Etiqueta As New Label
        Try
            Etiqueta.Name = pNombre
            Etiqueta.ForeColor = Color.Red
            Etiqueta.Text = pTexto
            Etiqueta.Top = pTop
            Etiqueta.Left = pLef
            Etiqueta.Width = pAncho
            Etiqueta.Height = pAlto
            If Not Fuente Is Nothing Then
                Etiqueta.Font = Fuente
            End If
            Etiqueta.Visible = pVisible
            Etiqueta.BackColor = Color.Transparent
            Return Etiqueta
        Catch ex As Exception
            MsgBox("Error CrearEtiquetaRepetida:" & ex.Message, MsgBoxStyle.Information)
        End Try
    End Function




   
   
 
    
    
End Class