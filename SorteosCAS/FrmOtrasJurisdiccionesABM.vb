Imports sorteos.helpers
Imports libEntities.Entities
Imports sorteos.bussiness
Public Class FrmOtrasJurisdiccionesABM
#Region "PROPIEDADES"
    Private _idPgmSorteo As Integer

    Public Property idPgmSorteo() As Integer
        Get
            Return _idPgmSorteo
        End Get
        Set(ByVal value As Integer)
            _idPgmSorteo = value
        End Set
    End Property
#End Region

    Private Sub Seleccionar()
        Dim bopgmsorteoloteria As New PgmSorteoLoteriaBO
        Dim lst As New ListaOrdenada(Of pgmSorteo_loteria)
        Dim Loteria As New Loteria
        Dim pgmsorteo As pgmSorteo_loteria
        Try
            If Not dgvJurisdiccion.CurrentRow Is Nothing Then
                Loteria = dgvJurisdiccion.CurrentRow.DataBoundItem()
                ' valida que no exista una autoridad con ese mismo orden para el juego
                lst = bopgmsorteoloteria.getSorteosLoteria(idPgmSorteo)
                For Each pgmsorteo In lst
                    If pgmsorteo.Loteria.IdLoteria = Loteria.IdLoteria Then
                        MsgBox("Ya existe la Jurisdicción en el sorteo actual. Quite primero la anterior o realice una nueva selección.", MsgBoxStyle.Exclamation)
                        Exit Sub
                    End If
                Next


                'opgmsorteoLoteria.idPgmSorteo = idPgmSorteo
                bopgmsorteoloteria.AgregarSorteoLoteria(Loteria.IdLoteria, idPgmSorteo)

                Me.Cerrar()
            End If

        Catch e As Exception
            MsgBox("Error al intentar seleccionar la Jurisdicción: " & e.Message)
        End Try
    End Sub
    Private Sub CargarGrilla()
        Dim loteriaBO As New LoteriaBO

        Try
            ' se realiza la búsqueda
            Dim encontrado As Integer = -1
            Dim lLista As ListaOrdenada(Of Loteria) = loteriaBO.getLoterias
            For i = 0 To lLista.Count - 1
                If lLista(i).IdLoteria = General.Jurisdiccion Then
                    encontrado = i
                End If
            Next
            If encontrado > -1 Then lLista.RemoveAt(encontrado)

            dgvJurisdiccion.DataSource = lLista
            ' se configuran las columnas de la grilla

            With dgvJurisdiccion
                .Columns.Clear()
                .ScrollBars = ScrollBars.Both
                .EditMode = DataGridViewEditMode.EditProgrammatically
                .AllowUserToResizeColumns = False
                .AllowUserToResizeRows = False
                .AutoGenerateColumns = False

                .Columns.Add("0", "Código")
                .Columns(0).Width = 0
                .Columns(0).DataPropertyName = "idloteria"
                .Columns(0).Visible = False

                .Columns.Add("1", "")
                .Columns(1).Width = 356
                .Columns(1).DataPropertyName = "nombre"

            End With

        Catch ex As Exception
            MsgBox("Error al consultar las loterías: " & ex.Message)
        End Try
    End Sub



    Private Sub FrmOtrasJurisdiccionesABM_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        CargarGrilla()
    End Sub


    Private Sub btnSeleccionar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSeleccionar.Click
        Seleccionar()
    End Sub
    Private Sub Cerrar()
        Me.Close()
        Me.Dispose()
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancelar.Click
        Cerrar()
    End Sub



    Private Sub dgvJurisdiccion_CellDoubleClick(ByVal sender As Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles dgvJurisdiccion.CellDoubleClick
        Seleccionar()
    End Sub
End Class
