Imports libEntities.Entities
Imports Sorteos.Helpers

Namespace Bussiness
    Public Class JuegoBO
        Public Function getJuego() As ListaOrdenada(Of Juego)
            Try
                Dim listaAut As ListaOrdenada(Of Juego)
                Dim oDal As New Data.JuegoDAL

                listaAut = oDal.getJuego()
                Return listaAut

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getJuego(ByVal idJuego As Integer) As Juego
            Try
                Dim oDal As New Data.JuegoDAL
                Return oDal.getJuego(idJuego)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getJuegoDescripcion(ByVal idJuego As Integer) As String
            Try
                Dim oDal As New Data.JuegoDAL
                Dim oJuego As Juego

                oJuego = oDal.getJuego(idJuego)

                Return oJuego.Jue_Desc

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function getJuegos(ByVal idJuegos As String) As ListaOrdenada(Of Juego)
            Try
                Dim listaAut As ListaOrdenada(Of Juego)
                Dim oDal As New Data.JuegoDAL

                listaAut = oDal.getJuegos(idJuegos)
                Return listaAut

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function getJuegosAMedios() As ListaOrdenada(Of Juego)
            Try
                Dim listaAut As ListaOrdenada(Of Juego)
                Dim oDal As New Data.JuegoDAL

                listaAut = oDal.getJuegosAMedios()
                Return listaAut

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function

        Public Function esQuiniela(ByVal idJuego As Integer) As Boolean
            Try
                Dim oDal As New Data.JuegoDAL
                Return oDal.esQuiniela(idJuego)

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

    End Class
End Namespace