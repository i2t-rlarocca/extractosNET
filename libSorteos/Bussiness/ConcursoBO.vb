Imports libEntities.Entities

Namespace Bussiness

    Public Class ConcursoBO
        Public Function getConcurso(ByVal idConcurso As Integer) As Concurso
            Try
                Dim o As New Concurso
                Dim oDal As New Data.ConcursoDAL
                o = oDal.getConcurso(idConcurso)
                Return o
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
    End Class
End Namespace