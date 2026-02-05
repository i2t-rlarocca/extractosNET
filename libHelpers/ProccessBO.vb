Imports System.Windows.Forms
Imports Sorteos.Helpers

Public Class ProccessBO
    Public Function IniciarProceso(ByVal Path As String, ByVal NombreProceso As String, ByVal NombreExe As String, ByVal ProcesoUnico As Boolean, ByRef Msj As String) As Boolean
        Dim lRuta As String
        Dim FileName As String

        Try
            IniciarProceso = False
            If Path.Trim = "" Then
                Msj = "Falta ingresar el Path que apunta al proceso."
                Exit Function
            Else
                lRuta = Path
                If Not lRuta.EndsWith("\") Then lRuta = lRuta & "\"
            End If
            '** controla el nombre del proceso
            If NombreProceso.Trim = "" Then
                Msj = "Falta ingresar el nombre del proceso."
                Exit Function
            End If

            If NombreExe.Trim = "" Then
                Msj = "Falta ingresar el nombre del ejecutable"
                Exit Function
            End If

            FileName = lRuta & NombreExe
            '** que exista el ejecutable
            If Not IO.File.Exists(FileName) Then
                Msj = "No existe el ejecutable " & NombreExe & " en el Path: " & lRuta
                Exit Function
            End If
            'Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName("ImpresorOrfiec")

            If ProcesoUnico Then
                Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName(NombreProceso)
                For Each p As Process In pProcess
                    Msj = "El proceso ya se encuentra en ejecución.Revise el ícono en la barra de tareas"
                    IniciarProceso = True
                    Exit Function
                Next
            End If

            Dim prc As New Process()
            prc.StartInfo.UseShellExecute = True
            prc.StartInfo.FileName = FileName
            prc.Start()
            prc = Nothing
            IniciarProceso = True
            Exit Function

        Catch ex As Exception
            Throw New Exception("Error al iniciar el Proceso: " & ex.Message & "No se pudo iniciar la aplicación")
        End Try
    End Function

    Public Function DetenerProceso(ByVal NombreProceso As String, ByRef Msj As String) As Boolean
        Try

            DetenerProceso = False
            Dim pProcess() As Process = System.Diagnostics.Process.GetProcessesByName(NombreProceso)
            For Each p As Process In pProcess
                p.CloseMainWindow()
                p.Kill()
            Next
            'actualiza als variabales  par_terminar,par_publicador y par_publicando a cero(false)
            General.ActualizaVariableConfigurador()
            Msj = "Se ha cerrado el proceso:" & NombreProceso
            DetenerProceso = True
            'Dim lstr As String = "Se ha cerrado el programa de impresión"
            'lstr = lstr & Environment.NewLine & Environment.NewLine
            'lstr = lstr & "Importante: Para quitar el ícono del área de notificación posicione el mouse sobre el mismo."

        Catch ex As Exception
            ' Throw New Exception("No se pudo detener el proceso:" & ex.Message)
        End Try
    End Function

End Class
