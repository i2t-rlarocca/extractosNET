Imports Sorteos.Bussiness
Imports System.Data.SqlClient
Imports Sorteos.Helpers
Imports System.IO


Public Class cPublicador
    Private Shared _fechaLog As String = ""
    Public Shared Property fechalog()
        Get
            Return _fechaLog
        End Get
        Set(ByVal value)
            _fechaLog = value
        End Set
    End Property

    Public Function LeeParametrosDisplay(ByRef idpgmconcurso As Long, ByRef novedadesDisplay As Boolean) As Boolean
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim dr As SqlDataReader
        Try
            LeeParametrosDisplay = False
            sql = "select coalesce(pu.idpgmconcurso,0) as idpgmconcurso, coalesce(pu.par_novedadesDisplay,0) as novedadesDisplay from parametrospublicador pu inner join pgmconcurso pc on pc.idpgmconcurso = pu.idpgmconcurso "
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            dr = cm.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                idpgmconcurso = dr("idpgmconcurso")
                novedadesDisplay = dr("novedadesDisplay")
            End If
            Try
                dr.Close()
            Catch ex2 As Exception
            End Try
            LeeParametrosDisplay = True
        Catch ex As Exception
            Try
                If Not dr.IsClosed Then
                    dr.Close()
                End If
            Catch ex2 As Exception
            End Try

            Throw New Exception("Publicador ON-LINE - Problema al leer parámetros.")
        End Try
    End Function

    Public Sub NovedadesDisplayON()
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Try
            sql = "update parametrospublicador set par_novedadesDisplay=1"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro de novedades.")
        End Try
    End Sub

    Public Sub NovedadesDisplayOFF()
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Try
            sql = "update parametrospublicador set par_novedadesDisplay=0"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro de novedades.")
        End Try
    End Sub

    Public Sub Par_PublicandoON()
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Try
            sql = "update parametrospublicador set Par_Publicando=1"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro de estado.")
        End Try
    End Sub
    Public Sub Par_PublicandoOFF()
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Try
            sql = "update parametrospublicador set Par_Publicando=0"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro de estado.")
        End Try
    End Sub

    Public Sub NovedadesWebON()
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Try
            sql = "update parametrospublicador set par_novedadesWeb=1"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro de novedades.")
        End Try
    End Sub
    Public Sub NovedadesWebOFF()
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Try
            sql = "update parametrospublicador set par_novedadesWeb=0"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro de novedades.")
        End Try
    End Sub

    Public Function Lee_Par_publicador() As Integer
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim _valor As Integer = 0
        Try
            sql = "select coalesce(par_publicador,0)as valor from parametrospublicador"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            _valor = cm.ExecuteScalar
            Return _valor
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al leer parámetro identificador del publicador.")
        End Try
    End Function
    Public Function Lee_Par_Terminar() As Boolean
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim _valor As Integer = 0
        Try
            sql = "select coalesce(par_terminar,0)as valor from parametrospublicador"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            _valor = cm.ExecuteScalar
            Return _valor
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al leer parámetro de finalización.")
        End Try
    End Function
    Public Sub IncrementaPar_Publicador()
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim _valor As Integer = 0
        Try
            sql = "update parametrospublicador set par_publicador=par_publicador +1"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro identificador del publicador.")
        End Try
    End Sub

    Public Sub DecrementaPar_Publicador()
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim _valor As Integer = 0
        Try
            sql = "update parametrospublicador set par_publicador=par_publicador - 1, par_terminar = case when par_publicador - 1 <= 0 then 0 else par_terminar end "
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro de estado.")
        End Try
    End Sub

    Public Function ObtenerTiempoTimer() As Integer
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim _valor As Integer = 0
        Try

            sql = "select coalesce(par_timer,0)as valor from parametrospublicador"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            _valor = cm.ExecuteScalar
            Return _valor
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al leer parámetro de lapso temporal.")
        End Try
    End Function

    Public Function LeeParametrosWeb(ByRef idpgmconcurso As Integer, ByRef novedadesWeb As Boolean) As Boolean
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim dr As SqlDataReader
        Try
            LeeParametrosWeb = False
            sql = "select coalesce(idpgmconcurso,0)as idpgmconcurso,coalesce(par_novedadesWeb,0)as novedadesWeb from parametrospublicador"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            dr = cm.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                idpgmconcurso = dr("idpgmconcurso")
                novedadesWeb = dr("novedadesWeb")
            End If
            dr.Close()
            LeeParametrosWeb = True
        Catch ex As Exception
            If Not dr.IsClosed Then
                dr.Close()
            End If
            Throw New Exception("Publicador ON-LINE - Problema al parámetros de publicación web.")
        End Try
    End Function

    Public Shared Sub Log(ByVal texto As String)

        'creamos el nombre del archivo
        Try
            Dim fecha As String = ""
            Dim fechaarchivo As String = ""
            Dim archivo = General.ArchivoLog 'Pathlog
            If Not archivo.EndsWith("\") Then archivo = archivo & "\"
            fecha = Now.ToShortDateString
            fechaarchivo = Mid(fecha, 7, 4) & Mid(fecha, 4, 2) & Mid(fecha, 1, 2)
            'actualiza la fecha de ultimo log del publicador
            If fecha <> fechalog Then
                ActualizafechaLog(fecha)
            End If
            archivo = archivo & fechaarchivo & "_sorteosPublicadorCAS.log"
            Dim path As String = archivo.Trim.Substring(0, archivo.Trim.LastIndexOf("\"))
            FileSystemHelper.CrearPath(path)
            'MsgBox("log publicador: " & archivo)
            Dim f As StreamWriter = New StreamWriter(archivo, True)
            f.WriteLine(texto)
            f.Close()
        Catch ex As Exception
            '            Throw New Exception(" Problema Log:" & ex.Message)
            ' excepción de grabación de log!! No se hace nada!!!
        End Try

    End Sub

    Public Function LeeFechaLog() As String

        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim dr As SqlDataReader
        Dim fecha As String = ""
        Try
            sql = "select coalesce(fechaultimolog,'01/01/1999')as fechalog from parametrospublicador"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            dr = cm.ExecuteReader()
            If dr.HasRows Then
                dr.Read()
                fecha = dr("fechalog")
            End If
            dr.Close()
            Return fecha
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al leer parámetro fecha último log de publicación.")
        End Try
    End Function

    Private Shared Sub ActualizafechaLog(ByVal fecha As String)
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Try
            sql = "update parametrospublicador set fechaultimolog='" & fecha & "'"
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al actualizar parámetro fecha último log de publicación.")
        End Try
    End Sub

    Public Function ObtenerTiempoTimer_premios(Optional ByVal timerPremios2 As Boolean = False) As Integer
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim _valor As Integer = 0
        Try
            If Not timerPremios2 Then
                sql = "select coalesce(timer_premios,0)as valor from parametrospublicador"
            Else
                sql = "select coalesce(timer2_premios,0)as valor from parametrospublicador"
            End If
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            _valor = cm.ExecuteScalar
            Return _valor
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al leer parámetro lapso tiempo verificación archivo premios.")
        End Try
    End Function

    Public Function ObtenerTiempoTimer_pozos(Optional ByVal timerPozos2 As Boolean = False) As Integer
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim _valor As Integer = 0
        Try
            If Not timerPozos2 Then
                sql = "select coalesce(timer_pozos,0)as valor from parametrospublicador"
            Else
                sql = "select coalesce(timer_pozos2,0)as valor from parametrospublicador"
            End If
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            _valor = cm.ExecuteScalar
            Return _valor
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al leer parámetro lapso tiempo verificación archivo sueldos.")
        End Try
    End Function

    Public Function ObtenerTiempoTimer_Extractos(Optional ByVal timerPremios2 As Boolean = False) As Integer
        Dim sql As String
        Dim cm As SqlCommand = New SqlCommand
        Dim _valor As Integer = 0
        Try
            If Not timerPremios2 Then
                sql = "select coalesce(timer_extractos,0)as valor from parametrospublicador"
            Else
                sql = "select coalesce(timer_extractos2,0)as valor from parametrospublicador"
            End If
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            cm.CommandText = sql
            _valor = cm.ExecuteScalar
            Return _valor
        Catch ex As Exception
            Throw New Exception("Publicador ON-LINE - Problema al leer parámetro lapso tiempo verificación archivo extracto.")
        End Try
    End Function
    
   
End Class
