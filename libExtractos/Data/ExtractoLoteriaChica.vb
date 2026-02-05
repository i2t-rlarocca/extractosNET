Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class ExtractoLoteriaChica
        Public Shared Function GetExtracto(ByVal idLoteria As String, _
                                    ByVal idJuego As String, _
                                    ByVal numeroSorteo As Integer, _
                                    ByVal fecha As String, _
                                    ByVal recuperarSorteo As Boolean) As ExtractoEntities.ExtractoLoteriaChica

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                Dim _Extracto As ExtractoEntities.Extracto
                Dim _ExtractoLoteriaChica As New ExtractoEntities.ExtractoLoteriaChica

                _Extracto = Extracto.GetExtracto(idLoteria, idJuego, numeroSorteo, True)

                _ExtractoLoteriaChica.CantidadCifras = _Extracto.CantidadCifras
                _ExtractoLoteriaChica.HoraSorteoOrigen = _Extracto.HoraSorteoOrigen
                _ExtractoLoteriaChica.Id = _Extracto.Id
                _ExtractoLoteriaChica.Numeros = _Extracto.Numeros
                _ExtractoLoteriaChica.NumeroSorteoOrigen = _Extracto.NumeroSorteoOrigen
                _ExtractoLoteriaChica.Sorteo = _Extracto.Sorteo

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From ext_lotchica " & _
                                 "Where ext_loteria = '" & idLoteria & "' " & _
                                 " And jue_id = '" & idJuego & "' " & _
                                 " And ext_sorteo = " & numeroSorteo

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                'For Each r As DataRow In dt.Rows

                '    '*************************************************************************

                '    _ExtractoLoteriaChica.PremiosPrimerSorteo = New List(Of ExtractoEntities.Premio)

                '    '*************************************************************************
                '    Dim PremioPrimerSorteo1 As New ExtractoEntities.Premio
                '    PremioPrimerSorteo1.CuponesGanadores = r("exj_gana1")
                '    PremioPrimerSorteo1.Pozo = r("exj_pozo1")
                '    PremioPrimerSorteo1.Premio = "1º Premio 6 aciertos"
                '    If r("exj_gana1") > 0 Then
                '        PremioPrimerSorteo1.PremioPorCupon = r("exj_pozo1") / r("exj_gana1")
                '    End If

                '    _ExtractoLoteriaChica.PremiosPrimerSorteo.Add(PremioPrimerSorteo1)
                '    '*************************************************************************
                '    Dim PremioPrimerSorteo2 As New ExtractoEntities.Premio
                '    PremioPrimerSorteo2.CuponesGanadores = r("exj_gana2")
                '    PremioPrimerSorteo2.Pozo = r("exj_pozo2")
                '    PremioPrimerSorteo2.Premio = "2º Premio 5 aciertos"
                '    If r("exj_gana2") > 0 Then
                '        PremioPrimerSorteo2.PremioPorCupon = r("exj_pozo2") / r("exj_gana2")
                '    End If

                '    _ExtractoLoteriaChica.PremiosPrimerSorteo.Add(PremioPrimerSorteo2)
                '    '*************************************************************************
                '    Dim PremioPrimerSorteo3 As New ExtractoEntities.Premio
                '    PremioPrimerSorteo3.CuponesGanadores = r("exj_gana3")
                '    PremioPrimerSorteo3.Pozo = r("exj_pozo3")
                '    PremioPrimerSorteo3.Premio = "3º Premio 4 aciertos"
                '    If r("exj_gana3") > 0 Then
                '        PremioPrimerSorteo3.PremioPorCupon = r("exj_pozo3") / r("exj_gana3")
                '    End If

                '    _ExtractoLoteriaChica.PremiosPrimerSorteo.Add(PremioPrimerSorteo3)
                '    '*************************************************************************
                '    Dim PremioPrimerSorteo4 As New ExtractoEntities.Premio
                '    PremioPrimerSorteo4.CuponesGanadores = r("exj_gana4")
                '    PremioPrimerSorteo4.Pozo = r("exj_pozo4")
                '    PremioPrimerSorteo4.Premio = "4º Premio 3 aciertos"
                '    If r("exj_gana4") > 0 Then
                '        PremioPrimerSorteo4.PremioPorCupon = r("exj_pozo4") / r("exj_gana4")
                '    End If

                '    _ExtractoLoteriaChica.PremiosPrimerSorteo.Add(PremioPrimerSorteo4)
                '    '*************************************************************************
                '    Dim PremioPrimerSorteo5 As New ExtractoEntities.Premio
                '    PremioPrimerSorteo5.CuponesGanadores = r("exj_ganaestimu")
                '    PremioPrimerSorteo5.Pozo = r("exj_pozoestimu")
                '    PremioPrimerSorteo5.Premio = "Premio estimulo agenciero"
                '    If r("exj_ganaestimu") > 0 Then
                '        PremioPrimerSorteo4.PremioPorCupon = r("exj_pozoestimu") / r("exj_ganaestimu")
                '    End If

                '    _ExtractoLoteriaChica.PremiosPrimerSorteo.Add(PremioPrimerSorteo5)
                'Next

                'cm.CommandText = "Select * From det_ext_brinco " & _
                '                 "Where ext_loteria = '" & idLoteria & "' " & _
                '                 " And jue_id = '" & idJuego & "' " & _
                '                 " And ext_sorteo = " & numeroSorteo & _
                '                 " Order By exj_orden"

                'dr = cm.ExecuteReader()
                'dt.Load(dr)
                'dr.Close()

                '_ExtractoLoteriaChica.CuponesGanadoresSueldos = New List(Of ExtractoEntities.CuponGanador)
                'For Each r As DataRow In dt.Rows
                '    Dim cg As New ExtractoEntities.CuponGanador
                '    cg.Agencia = Es_Nulo(Of String)(r("exj_agencia"), "")
                '    cg.Cupon = Es_Nulo(Of String)(r("exj_ticket"), "")
                '    cg.Localidad = Es_Nulo(Of String)(r("exj_localidad"), "")
                '    cg.Provincia = Es_Nulo(Of String)(r("exj_provincia"), "")
                '    _ExtractoLoteriaChica.CuponesGanadoresSueldos.Add(cg)
                'Next

                Return _ExtractoLoteriaChica

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function


        Public Shared Function GetExtractoDT(ByVal idLoteria As String, _
                                           ByVal idJuego As String, _
                                           ByVal idPgmSorteo As Long, _
                                           ByVal fecha As String, _
                                           ByVal recuperarSorteo As Boolean) As DataSet

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader

            Dim ds As New DataSet
            Dim dt_terminacion As New DataTable
            Dim dt_Extracto As New DataTable

            Try
                Dim _ExtractoLoteriaChica As New ExtractoEntities.ExtractoLoteriaChica

                dt_Extracto = Extracto.GetExtractoDT(idLoteria, idJuego, idPgmSorteo)
                dt_Extracto.TableName = "Extracto_LoteriaChica"

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                'cm.CommandText = "Select * From ext_lotchica " & _
                '                 "Where ext_loteria = '" & idLoteria & "' " & _
                '                 " And jue_id = '" & idJuego & "' " & _
                '                 " And ext_sorteo = " & numeroSorteo

                'dr = cm.ExecuteReader()
                'dt_terminacion.Load(dr)
                dt_terminacion = Extracto.getPremios_LocalDT(idLoteria, idJuego, idPgmSorteo)
                dt_terminacion.TableName = "Extracto_Terminaciones"

                ds.Tables.Add(dt_Extracto)
                ds.Tables.Add(dt_terminacion)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
    End Class
End Namespace

