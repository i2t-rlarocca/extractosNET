Imports libEntities.Entities
Imports Sorteos.Data
Imports Sorteos.Helpers

Namespace Bussiness


    Public Class PgmSorteoLoteriaBO
        Public Function getSorteosLoteria(ByVal pidPgmSorteo As Integer) As ListaOrdenada(Of pgmSorteo_loteria)
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Dim ls As New ListaOrdenada(Of pgmSorteo_loteria)
                ls = oDal.getSorteosLoteria(pidPgmSorteo)
                Return ls

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function CargarDetalleDesdeArchivo(ByVal pidPgmsorteo As Integer, ByVal poLoteria As Loteria) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Return oDal.CargarDetalleDesdeArchivo(pidPgmsorteo, poLoteria)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try

        End Function
        Public Function InsertarDatosdesdeArchivo(ByVal pvalores As String, ByVal pidPgmSorteo As Integer, ByVal pidLoteria As Char, ByVal pCifras As Integer, Optional ByVal pletras As String = "") As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Return oDal.InsertarDatosdesdeArchivo(pvalores, pidPgmSorteo, pidLoteria, pCifras, pletras)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function InsertarActualizarExtracto_QNL(ByVal pSorteoLoteria As pgmSorteo_loteria, Optional ByVal pModifica As Boolean = False) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Return oDal.InsertarActualizarExtracto_QNL(pSorteoLoteria, pModifica)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function QuitarSorteoLoteria(ByVal oSorteo As PgmSorteo, ByVal pidLoteria As Char, ByVal pidpgmSorteo As Integer) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                oDal.QuitarSorteoLoteria(pidLoteria, pidpgmSorteo)
                If AnularWeb(oSorteo, pidLoteria, False, True) = False Then
                    Throw New Exception("Error al revertir los datos web.")
                    Return False
                    Exit Function
                End If
                Return True


            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function AgregarSorteoLoteria(ByVal pidLoteria As Char, ByVal pidpgmSorteo As Integer, Optional ByVal pNroSorteo As Integer = 0) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Return oDal.AgregarSorteoLoteria(pidLoteria, pidpgmSorteo, pNroSorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function ActualizaFecIniFinLoteria(ByVal pidpgmSorteo As Integer, ByVal pidLoteria As Char, ByVal HoraLoteria As DateTime, ByVal HoraIniExtraccion As DateTime, ByVal HoraFinExtraccion As DateTime) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Return (oDal.ActualizaFecIniFinLoteria(pidpgmSorteo, pidLoteria, HoraLoteria, HoraIniExtraccion, HoraFinExtraccion))
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function Confirmar(ByVal pidPgmSorteo As Integer, ByVal pIdLoteria As Char, ByVal Horaloteria As DateTime, ByVal HoraIniExtraccion As DateTime, ByVal HoraFinExtraccion As DateTime) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Return (oDal.Confirmar(pidPgmSorteo, pIdLoteria, Horaloteria, HoraIniExtraccion, HoraFinExtraccion))
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

        Public Function Revertir(ByRef oSorteo As PgmSorteo, ByVal pIdLoteria As Char, ByVal idModalidad As Integer) As pgmSorteo_loteria
            Try
                Revertir = Nothing
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                oDal.Revertir(oSorteo.idPgmSorteo, pIdLoteria, idModalidad)
                oSorteo.ExtraccionesLoteria.Clear()
                oSorteo.ExtraccionesLoteria = getSorteosLoteria(oSorteo.idPgmSorteo)
                For Each o As pgmSorteo_loteria In oSorteo.ExtraccionesLoteria
                    If o.Loteria.IdLoteria = pIdLoteria Then
                        Revertir = o
                    End If
                Next
                If AnularWeb(oSorteo, pIdLoteria, False, True) = False Then
                    Throw New Exception("Error al revertir los datos web.")
                    Exit Function
                End If
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function AnularWeb(ByVal oSorteo As PgmSorteo, ByVal pIdLoteria As Char, Optional ByVal porMenu As Boolean = False, Optional ByVal onLine As Boolean = False) As Boolean

            '**** controles
            Dim _continuar As Boolean = False
            Dim _ModoPublicacion As Integer = General.ModoPublicacion
            Dim _PublicaWeb As String = General.PublicaWeb.ToUpper
            Dim _PublicarWebON As String = General.PublicarWebON
            Dim _PublicarWebOFF As String = General.PublicarWebOFF
            Dim _PublicarWebONOFF As Boolean = (_PublicarWebON = "S" And onLine) Or (_PublicarWebOFF = "S" And (Not onLine))
            If (_ModoPublicacion = 1 And _PublicaWeb = "N" And _PublicarWebONOFF) Or (_ModoPublicacion = 0 And _PublicarWebONOFF) Then
                _continuar = True
            End If
            If Not _continuar Then
                FileSystemHelper.Log(" La publicación a la Web no está habilitada.Parametros: ModoPublicacion:" & _ModoPublicacion & " ,PublicaWeb:" & _PublicaWeb & " ,PublicarWebON:" & _PublicarWebOFF & " ,PublicarWebOFF:" & _PublicarWebON)
                Return True
                Exit Function
            End If
            '***************

            Dim ws As New WSExtractos.ExtractoServicioClient
            Dim extr As New WSExtractos.Extracto
            extr.Juego = oSorteo.idJuegoLetra.Trim
            extr.Loteria = pIdLoteria
            extr.NumeroSorteo = oSorteo.nroSorteo
            extr.FechaHoraSorteo = oSorteo.fechaHora

            Dim _nroIntento As Integer = 1
            While _nroIntento <= General.CantidadIntentos
                Try
                    If porMenu Then
                        ''Return ws.reAnularExtracto(extr)
                        ''Dim strWSllegada As String = ws.pruebaLlegadaAlIIS()
                        ''Dim strWSpermisos As String = ws.pruebaPermisosLog()
                        Return ws.reAnularExtracto(extr)
                    Else
                        Return ws.eliminarExtracto(extr)
                    End If
                    Exit While
                Catch ex As Exception
                    If InStr(UCase(ex.Message), "BAD GATEWAY") > 0 Or InStr(UCase(ex.Message), "TIMEOUT") > 0 Then
                        FileSystemHelper.Log(Now & " AnularWeb_IntentoNro:" & _nroIntento & " - " & ex.Message)
                        _nroIntento = _nroIntento + 1
                    Else
                        Throw New Exception("Problema al anular en web. Anulación cancelada. " & ex.Message)
                        Return False
                        Exit Function
                    End If
                End Try
            End While

            Return True
        End Function
        Public Function InsertarLetras_QNL(ByVal pSorteoLoteria As pgmSorteo_loteria) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Return oDal.InsertarLetras_QNL(pSorteoLoteria)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        '** 27/11/2012
        Public Function QuitarSorteoLoteriaInicio(ByVal pidLoteria As Char, ByVal pidpgmSorteo As Integer) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                oDal.QuitarSorteoLoteria(pidLoteria, pidpgmSorteo)
                Return True

            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function
        Public Function ActualizaNroSorteoLoteria(ByVal pidLoteria As Char, ByVal pidpgmSorteo As Integer, ByVal pNroSorteo As Integer) As Boolean
            Try
                Dim oDal As New Data.PgmSorteo_LoteriaDAL
                Return oDal.ActualizaNroSorteoLoteria(pidLoteria, pidpgmSorteo, pNroSorteo)
            Catch ex As Exception
                Throw New Exception(ex.Message)
                Return False
            End Try
        End Function

    End Class
End Namespace