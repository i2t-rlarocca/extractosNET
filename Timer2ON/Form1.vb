Imports Timer2On.General
Imports System.Data
Imports System.Data.SqlClient

Public Class Form1
    Dim _milisegundos As Integer
    Dim _tiempo As Integer
    Dim archivo As String
    Private Sub Form1_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        General.PathIni = Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 13)
        ActivarTimer()
        'End
    End Sub
    Private Sub ActivarTimer()
        Dim cm As SqlCommand = New SqlCommand
        Dim dr As SqlDataReader = Nothing
        Dim vsql As String
        Dim valor As Integer
        Dim idJuego As Integer
        Try
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            vsql = "select estado,id_juego from estado "
            cm.CommandText = vsql
            dr = cm.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                valor = dr("estado")
                idJuego = dr("id_juego")
                dr.Close()
                If valor = 3 Then
                    vsql = "select timer2_on from secuencia_display where destino_id=" & idJuego
                    cm.CommandText = vsql
                    dr = cm.ExecuteReader()
                    If dr.HasRows Then
                        dr.Read()
                        _tiempo = dr("timer2_on") * 1000
                        dr.Close()
                        Timer1.Interval = _tiempo
                        Timer1.Enabled = True
                    Else
                        General.Log("Sub ActivarTimer:No hay registros en la tabla secuencia_display.No se realiza actualización")
                        End
                    End If
                Else
                    General.Log("Sub ActivarTimer:El juego con id(" & idJuego & ") tiene estado:" & valor & " en la tabla estado.No se realiza actualización")
                    End 'finaliza el pgm
                End If

            Else
                General.Log("Sub ActivarTimer:No hay registros en la tabla estado.No se realiza actualización")
                End 'finalizar pgm
            End If
        Catch ex As Exception
            General.Log("Error Sub ActivarTimer:" & ex.Message)
        End Try
    End Sub

    Private Sub Timer1_Tick(ByVal sender As Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        Try
            ActualizaEstado()
            Timer1.Enabled = False
            End

        Catch ex As Exception
            General.Log("Error Sub Timer1_Tick:" & ex.Message)
        End Try
    End Sub
    Private Function ActualizaEstado() As Boolean
        Dim cm As SqlCommand = New SqlCommand
        Dim vsql As String
        Try
            ActualizaEstado = False
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            vsql = "UPDATE estado set estado=0 "
            cm.CommandText = vsql
            cm.ExecuteNonQuery()
            ActualizaEstado = True
        Catch ex As Exception
            General.Log("Error Function ActualizaEstado:" & ex.Message)
            ActualizaEstado = False
        End Try
    End Function


End Class
