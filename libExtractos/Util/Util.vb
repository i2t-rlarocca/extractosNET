Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.IO

Public Class Util

    Public Shared Function PeriodoToString(ByVal periodo As Integer) As String
        If periodo = 0 Then
            Return ""
        Else
            Dim perString As String = periodo
            Return Right(perString, 2) & "/" & Left(perString, 4)
        End If

    End Function

    Public Shared Function Date2Mask(ByVal fecha As Date) As String
        If fecha.Year = 1 Then
            Return "__/__/____"
        Else
            Return fecha.ToShortDateString
        End If
    End Function

    Public Shared Function Date2Label(ByVal fecha As Date) As String
        If fecha.Year = 1 Then
            Return ""
        Else
            Return fecha.ToShortDateString
        End If
    End Function

    Public Shared Function DateTime2Label(ByVal fecha As Date) As String
        If fecha.Year = 1 Then
            Return ""
        Else
            Return fecha.ToShortDateString & " - " & fecha.ToShortTimeString
        End If
    End Function

    Public Shared Function DateTime2TimeString(ByVal fecha As Date) As String
        If fecha.Year = 1 Then
            Return ""
        Else
            Return fecha.ToString("HH:mm")
        End If
    End Function

    Public Shared Function Minutos2Label(ByVal minutos As Integer) As String
        If minutos = 0 Then
            Return "&nbsp;"
        Else

            If minutos < 60 Then
                Return minutos & " mi"
            Else
                Return Int(minutos / 60) & ":" & (minutos Mod 60) & " hs"
            End If
        End If
    End Function

    Public Shared Function Mask2Date(ByVal fecha As String) As Date

        If fecha = "" Or fecha = "__/__/____" Then
            Return Nothing
        Else
            Dim _Fecha() = Split(fecha, "/")
            Return New Date(_Fecha(2), _Fecha(1), _Fecha(0))
        End If
    End Function

    Public Shared Function ValidaDesdeHasta(ByVal desde As String, ByVal hasta As String) As Boolean

        Dim _desde() = Split(desde, "/")
        Dim _hasta() = Split(hasta, "/")

        If New Date(Mid(_desde(2), 1, 4), _desde(1), _desde(0)) <= New Date(Mid(_hasta(2), 1, 4), _hasta(1), _hasta(0)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Mask2DateTime(ByVal fecha As String, ByVal hora As String) As Date

        If fecha = "" Or fecha = "__/__/____" Or hora = "" Or hora = "__:__" Then
            Return Nothing
        Else
            Dim _Fecha() = Split(fecha, "/")
            Dim _Hora() = Split(hora, ":")
            Return New Date(_Fecha(2), _Fecha(1), _Fecha(0), _Hora(0), _Hora(1), 0)
        End If
    End Function

    Public Shared Function DateToString(ByVal fecha As String) As String

        If fecha = "" Or fecha = "__/__/____" Then
            Return Nothing
        Else
            Dim f As Date
            f = fecha
            Return Format(f, "yyyy-MM-dd")
        End If
    End Function

    'Convierte el tipo TimeSpan de .NET al tipo Time de MYSQL 
    Public Shared Function TimeSpanToTime(ByRef pTime As TimeSpan) As String
        Try
            If Not pTime = Nothing Then
                Return String.Concat(pTime.Hours, ":", pTime.Minutes, ":", pTime.Seconds)
            End If
            Return New String("00:00:00")
        Catch ex As Exception
            Return New String("00:00:00")
        End Try
    End Function

    'Convierte el tipo Time de MYSQL al Tipo TimeSpan de .NET
    Public Shared Function TimeToTimeSpan(ByVal pTime As String) As TimeSpan
        Try
            If pTime.Length = 8 Then
                If pTime.Contains(":") Then
                    Return New TimeSpan(pTime.Substring(0, 2), pTime.Substring(3, 2), pTime.Substring(6, 2))
                End If
            End If
            'Si el parametro no es correcto, devuelvo timespan vacío
            Return New TimeSpan(0, 0, 0)
        Catch ex As Exception
            Return New TimeSpan(0, 0, 0)
        End Try
    End Function

    Public Shared Function ValidarFechas(ByVal desde As String, ByVal hasta As String) As Boolean

        Dim _desde() = Split(desde, "/")
        Dim _hasta() = Split(hasta, "/")

        If New Date(Mid(_desde(2), 1, 4), _desde(1), _desde(0)) = New Date(Mid(_hasta(2), 1, 4), _hasta(1), _hasta(0)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function ValidarFecha(ByVal fecha As String) As Boolean
        Try
            Dim f As New Date
            f = CDate(fecha)
            Return True
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function ValidarHora(ByVal hora As String) As Boolean
        Try
            If hora = "" Then
                Return True
            Else
                Dim hr = hora.Split(":")
                If hr.Length <> 2 Then
                    Return False
                Else
                    If Not IsNumeric(hr(0)) Then
                        Return False
                    Else
                        If Not IsNumeric(hr(1)) Then
                            Return False
                        Else
                            Dim hh As Integer = hr(0)
                            If hh < 0 Or hh > 23 Then
                                Return False
                            Else
                                Dim mm As Integer = hr(1)
                                If mm < 0 Or mm > 59 Then
                                    Return False
                                Else
                                    Return True
                                End If
                            End If
                        End If
                    End If
                End If
            End If
        Catch ex As Exception
            Return False
        End Try
    End Function

    Public Shared Function ValidarMail(ByVal sMail As String) As Boolean
        If sMail.Length = 0 Then
            Return True
        Else
            Return Regex.IsMatch(sMail, _
                    "^([\w-]+\.)*?[\w-]+@[\w-]+\.([\w-]+\.)*?[\w]+$")
        End If
    End Function

    Public Shared Function FileToBytes(ByVal Path As String) As Byte()
        Dim sPath As String
        sPath = Path
        Dim Ruta As New FileStream(sPath, FileMode.Open, FileAccess.Read)
        Dim Binario(CInt(Ruta.Length)) As Byte
        Ruta.Read(Binario, 0, CInt(Ruta.Length))
        Ruta.Close()
        Return Binario
    End Function

    Public Shared Sub OpenFile(ByVal Bin As Byte(), ByVal nombre As String)
        Dim oFileStream As FileStream
        Dim pathTemporal As String = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) & "\" & nombre

        If File.Exists(pathTemporal) Then File.Delete(pathTemporal)
        oFileStream = New FileStream(pathTemporal, FileMode.CreateNew)
        oFileStream.Write(Bin, 0, Bin.Length)
        oFileStream.Close()
        oFileStream = Nothing

        Dim proceso As New System.Diagnostics.Process
        With proceso
            .StartInfo.FileName = pathTemporal
            .Start()
        End With
    End Sub

    Public Shared Function GetExpressionFecha() As String

        Return "(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d"
    End Function

    Public Shared Function GetExpressionHora() As String

        Return "^(0[0-9]|1\d|2[0-3]):([0-5]\d)$"
    End Function


    Public Shared Function Log(ByVal texto As String, ByVal archivoLog As String)

        'conectamos con el FSO
        Dim confile = CreateObject("scripting.filesystemobject")

        'creamos el objeto TextStream
        Dim fich = confile.OpenTextFile(archivoLog, 8, True, True)

        'escribimos los números del 0 al 9
        fich.writeline(texto)

        'cerramos el fichero
        fich.close()
        Return True
    End Function

    Public Shared Function CrearLog(ByVal archivoLog As String)

        'conectamos con el FSO
        Dim confile = CreateObject("scripting.filesystemobject")

        'creamos el objeto TextStream
        Dim fich = confile.OpenTextFile(archivoLog, 2, True, True)

        'cerramos el fichero
        fich.close()
        Return True
    End Function
End Class