Namespace Bussiness
    Public Class MailBO
        Public Function validaMail(ByVal p_email As String) As Boolean
            Dim rexp As System.Text.RegularExpressions.Regex = New System.Text.RegularExpressions.Regex("^(([^<;>;()[\]\\.,;:\s@\""]+" & _
            "(\.[^<;>;()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@" & _
            "((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}" & _
            "\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+" & _
            "[a-zA-Z]{2,}))$")

            Return (rexp.IsMatch(p_email))
        End Function

        Public Function envioMail(ByVal hostMail As String, ByVal fromMail As String, ByVal toMail As String, ByVal subjectMail As String, ByVal bodyMail As String, Optional ByVal isBodyHtml As Boolean = True, Optional ByVal attachMail As List(Of String) = Nothing, Optional ByVal sepToMail As String = ";", Optional ByVal hostPort As Integer = 0, Optional ByVal remite As String = "") As Boolean
            Try
                'lblMail.Visible = True
                Dim correo As New System.Net.Mail.MailMessage

                ' ** Configuro el From
                correo.From = New System.Net.Mail.MailAddress(fromMail, remite)

                ' ** Configuro el To
                Dim direcciones As String
                direcciones = toMail
                Dim vDirecciones() As String = direcciones.Split(sepToMail)
                For i As Integer = 0 To vDirecciones.Length - 1
                    If (vDirecciones(i).Trim() <> "") Then
                        If validaMail(vDirecciones(i)) Then
                            correo.To.Add(vDirecciones(i))
                        Else
                            Return False
                        End If
                    End If
                Next
                ' ** Configuro el Subject
                correo.Subject = subjectMail

                ' ** Configuro el Body
                correo.IsBodyHtml = isBodyHtml
                correo.Priority = System.Net.Mail.MailPriority.Normal
                correo.Body = bodyMail

                ' ** Configuro el Attachment
                Dim arcAttach As System.Net.Mail.Attachment
                If attachMail IsNot Nothing Then
                    For j As Integer = 1 To attachMail.Count
                        arcAttach = New System.Net.Mail.Attachment(attachMail(j - 1).ToString)
                        correo.Attachments.Add(arcAttach)
                    Next
                End If

                ' ** Configuro el SMTP
                Dim smtp As New System.Net.Mail.SmtpClient
                smtp.Host = hostMail '"192.168.127.200"
                If hostPort > 0 Then
                    smtp.Port = hostPort
                End If

                ' ** Envio el mail
                smtp.Timeout = 300000
                'MsgBox(smtp.Timeout)

                smtp.Send(correo)
                correo.Dispose()
                correo = Nothing
                smtp = Nothing

                Return True

            Catch ex As Exception
                Helpers.FileSystemHelper.Log("MailBO.enviarMail - Excepcion: " & ex.Message)
                Return False
            End Try

        End Function
    End Class
End Namespace