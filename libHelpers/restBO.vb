Imports System.Runtime.Serialization
Imports System.Net
Imports System.IO
Imports System.Web.UI
Imports System.Text
Imports System.Web.Script.Serialization


Public Class restBO

    Public Function GetPOSTResponse(ByVal uri As System.Uri, ByVal jsonDataByte As String, ByVal contentType As String, ByVal method As String) As String

        Dim webClient As New WebClient()
        Dim reqString As String
        Dim url As String = uri.ToString
        Try
            webClient.Headers("content-type") = "application/x-www-form-urlencoded; charset=utf-8"
            reqString = webClient.UploadString(url, "json=" + jsonDataByte)
            Return reqString
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try

    End Function

End Class
