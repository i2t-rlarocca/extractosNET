Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class ExtractoBrinco

        Public Shared Function GetExtracto(ByVal idLoteria As String, _
                                           ByVal idJuego As String, _
                                           ByVal numeroSorteo As Integer, _
                                           ByVal fecha As String, _
                                           ByVal recuperarSorteo As Boolean) As ExtractoEntities.ExtractoBrinco

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                Dim _Extracto As ExtractoEntities.Extracto
                Dim _ExtractoBrinco As New ExtractoEntities.ExtractoBrinco

                _Extracto = Extracto.GetExtracto(idLoteria, idJuego, numeroSorteo, True)

                _ExtractoBrinco.CantidadCifras = _Extracto.CantidadCifras
                _ExtractoBrinco.HoraSorteoOrigen = _Extracto.HoraSorteoOrigen
                _ExtractoBrinco.Id = _Extracto.Id
                _ExtractoBrinco.Numeros = _Extracto.Numeros
                _ExtractoBrinco.NumeroSorteoOrigen = _Extracto.NumeroSorteoOrigen
                _ExtractoBrinco.Sorteo = _Extracto.Sorteo
                '**09/10/2012*********
                _ExtractoBrinco.Localidad = _Extracto.Localidad
                _ExtractoBrinco.HoraProximo = _Extracto.Sorteo.FechaHoraProximoSorteo.ToString("HH:mm")
                _ExtractoBrinco.FechaHoraProximoSorteo = _Extracto.FechaHoraProximoSorteo

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From ext_brinco " & _
                                 "Where ext_loteria = '" & idLoteria & "' " & _
                                 " And jue_id = '" & idJuego & "' " & _
                                 " And ext_sorteo = " & numeroSorteo

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows

                    '*************************************************************************

                    _ExtractoBrinco.PremiosPrimerSorteo = New List(Of ExtractoEntities.Premio)

                    '*************************************************************************
                    Dim PremioPrimerSorteo1 As New ExtractoEntities.Premio
                    PremioPrimerSorteo1.CuponesGanadores = r("exj_gana1")
                    PremioPrimerSorteo1.Pozo = r("exj_pozo1")
                    PremioPrimerSorteo1.Premio = "1º Premio 6 aciertos"
                    If r("exj_gana1") > 0 Then
                        PremioPrimerSorteo1.PremioPorCupon = r("exj_pozo1") / r("exj_gana1")
                    End If

                    _ExtractoBrinco.PremiosPrimerSorteo.Add(PremioPrimerSorteo1)
                    '*************************************************************************
                    Dim PremioPrimerSorteo2 As New ExtractoEntities.Premio
                    PremioPrimerSorteo2.CuponesGanadores = r("exj_gana2")
                    PremioPrimerSorteo2.Pozo = r("exj_pozo2")
                    PremioPrimerSorteo2.Premio = "2º Premio 5 aciertos"
                    If r("exj_gana2") > 0 Then
                        PremioPrimerSorteo2.PremioPorCupon = r("exj_pozo2") / r("exj_gana2")
                    End If

                    _ExtractoBrinco.PremiosPrimerSorteo.Add(PremioPrimerSorteo2)
                    '*************************************************************************
                    Dim PremioPrimerSorteo3 As New ExtractoEntities.Premio
                    PremioPrimerSorteo3.CuponesGanadores = r("exj_gana3")
                    PremioPrimerSorteo3.Pozo = r("exj_pozo3")
                    PremioPrimerSorteo3.Premio = "3º Premio 4 aciertos"
                    If r("exj_gana3") > 0 Then
                        PremioPrimerSorteo3.PremioPorCupon = r("exj_pozo3") / r("exj_gana3")
                    End If

                    _ExtractoBrinco.PremiosPrimerSorteo.Add(PremioPrimerSorteo3)
                    '*************************************************************************
                    Dim PremioPrimerSorteo4 As New ExtractoEntities.Premio
                    PremioPrimerSorteo4.CuponesGanadores = r("exj_gana4")
                    PremioPrimerSorteo4.Pozo = r("exj_pozo4")
                    PremioPrimerSorteo4.Premio = "4º Premio 3 aciertos"
                    If r("exj_gana4") > 0 Then
                        PremioPrimerSorteo4.PremioPorCupon = r("exj_pozo4") / r("exj_gana4")
                    End If

                    _ExtractoBrinco.PremiosPrimerSorteo.Add(PremioPrimerSorteo4)
                    '*************************************************************************
                    Dim PremioPrimerSorteo5 As New ExtractoEntities.Premio
                    PremioPrimerSorteo5.CuponesGanadores = r("exj_ganaestimu")
                    PremioPrimerSorteo5.Pozo = r("exj_pozoestimu")
                    PremioPrimerSorteo5.Premio = "Premio estimulo agenciero"
                    If r("exj_ganaestimu") > 0 Then
                        PremioPrimerSorteo4.PremioPorCupon = r("exj_pozoestimu") / r("exj_ganaestimu")
                    End If

                    _ExtractoBrinco.PremiosPrimerSorteo.Add(PremioPrimerSorteo5)
                    '**09/10/12***
                    If _ExtractoBrinco.Sorteo.PozoProximoEstimado <= 0 Then
                        _ExtractoBrinco.Sorteo.PozoProximoEstimado = r("exj_pozoesti")
                    End If
                Next

                cm.CommandText = "Select * From det_ext_brinco " & _
                                 "Where ext_loteria = '" & idLoteria & "' " & _
                                 " And jue_id = '" & idJuego & "' " & _
                                 " And ext_sorteo = " & numeroSorteo & _
                                 " Order By exj_orden"

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                _ExtractoBrinco.CuponesGanadoresSueldos = New List(Of ExtractoEntities.CuponGanador)
                For Each r As DataRow In dt.Rows
                    Dim cg As New ExtractoEntities.CuponGanador
                    cg.Agencia = Es_Nulo(Of String)(r("exj_agencia"), "")
                    cg.Cupon = Es_Nulo(Of String)(r("exj_ticket"), "")
                    cg.Localidad = Es_Nulo(Of String)(r("exj_localidad"), "")
                    cg.Provincia = Es_Nulo(Of String)(r("exj_provincia"), "")
                    _ExtractoBrinco.CuponesGanadoresSueldos.Add(cg)
                Next

                Return _ExtractoBrinco

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
            Dim dt_premio As New DataTable
            Dim dt_sueldo As New DataTable
            Dim dt_Extracto As New DataTable
            Dim dt_adicional As New DataTable

            Try
                Dim _ExtractoBrinco As New ExtractoEntities.ExtractoBrinco

                dt_Extracto = Extracto.GetExtractoDT(idLoteria, idJuego, idPgmSorteo)
                dt_Extracto.TableName = "Extracto_Brinco"

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                'If numeroSorteo >= 666 Then
                '    cm.CommandText = "select vs.jue_id, @numeroSorteo AS ext_sorteo, vs.mod_id, vs.mod_valapu from valor_apuesta_sorteo vs  where vs.idjuego = @idjuego and vs.ext_sorteo = @numeroSorteo"
                '    cm.Parameters.Clear()
                '    cm.Parameters.AddWithValue("@idJuego", idJuego)
                '    cm.Parameters.AddWithValue("@numeroSorteo", numeroSorteo)

                '    dr = cm.ExecuteReader()
                '    If Not dr.HasRows Then
                '        dr.Close()
                '        ' inserto los valor_apuesta-sorteo y e. 
                '        cm.CommandText = "Insert Into valor_apuesta_sorteo (jue_id, ext_sorteo, mod_id, mod_valapu) " _
                '                       & "Select jue_id, @numeroSorteo, mod_id, 5 " _
                '                       & "From valor_apuesta Where jue_id = @idJuego and mod_id = 1"
                '        cm.Parameters.Clear()
                '        cm.Parameters.AddWithValue("@idJuego", idJuego)
                '        cm.Parameters.AddWithValue("@numeroSorteo", numeroSorteo)

                '        cm.ExecuteNonQuery()
                '    End If

                'End If

                'cm.CommandText = " Select b.*, IIf(IsNull(vas.mod_valapu) Or vas.mod_valapu= 0,va.mod_valapu,vas.mod_valapu) AS mod_valapu From ((ext_brinco b " & _
                '                 " left join  valor_apuesta_sorteo vas on b.jue_id = vas.jue_id and  b.ext_sorteo = vas.ext_sorteo )" & _
                '                 " left join  valor_apuesta va on b.jue_id = va.jue_id )" & _
                '                 " Where b.ext_loteria = '" & idLoteria & "' " & _
                '                 "  And b.jue_id = '" & idJuego & "' " & _
                '                 "  And b.ext_sorteo = " & numeroSorteo
                'cm.Parameters.Clear()
                'dr = cm.ExecuteReader()
                'dt_premio.Load(dr)
                Dim premios As List(Of ExtractoEntities.Premio)
                dt_premio = Extracto.getPremios_LocalDT(idLoteria, idJuego, idPgmSorteo, premios)
                dt_premio.TableName = "Extracto_Brinco_Premios"

                cm.CommandText = "select 'S' as ext_loteria,'BR' as jue_id,idpgmsorteo % 1000000 as ext_sorteo ,orden as exj_id,cupon as exj_ticket,agencia as exj_agencia,localidad as exj_localidad,provincia as exj_provincia,orden as exj_orden  from premio_sueldo_br  a  Where idpgmsorteo = " & idPgmSorteo

                dr = cm.ExecuteReader()
                dt_sueldo.Load(dr)
                dt_sueldo.TableName = "Extracto_Brinco_Sueldos"
                dr.Close()

                ' localiza los datos del sorteo extra
                'cm.CommandText = " Select * from sorteos_adicionales " _
                '               & " where ext_loteria = '" & idLoteria & "' " _
                '               & " and ext_juego = '" & idJuego & "' " _
                '               & " and ext_sorteo = " & numeroSorteo

                'dr = cm.ExecuteReader()
                'dt_adicional.Load(dr)
                dt_adicional = crea_DT_premioAdicional(idLoteria, idJuego, idPgmSorteo, premios)
                dt_adicional.TableName = "Extracto_Brinco_Adicional"
                'dr.Close()

                '** genera datable de minimos asegurados
                Dim vsql As String
                Dim dtaux As New DataTable
                Dim dt_minimos As New DataTable
                vsql = "select * from minimosasegurados where nrosorteodesde in"
                vsql = vsql & " (select max(nrosorteodesde) "
                vsql = vsql & "  from minimosasegurados where idjuego='BR' and nrosorteodesde<=" & idPgmSorteo & " % 1000000 )"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dtaux.Load(dr)
                dr.Close()
                dt_minimos = crea_DT_MinimosAsegurados(dtaux)
                dt_minimos.TableName = "Extracto_Brinco_MinimosAsegurados"

                ds.Tables.Add(dt_Extracto)
                ds.Tables.Add(dt_premio)
                ds.Tables.Add(dt_sueldo)
                ds.Tables.Add(dt_adicional)
                ds.Tables.Add(dt_minimos)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Private Shared Function crea_DT_MinimosAsegurados(ByVal dtable As DataTable) As DataTable
            Try
                Dim dt As New DataTable
                Dim dr As DataRow = dt.NewRow
                dt.Columns.Add("jue_id")
                dt.Columns.Add("minimoModalidad1", Type.GetType("System.Double"))
                dt.Columns.Add("minimoModalidad2", Type.GetType("System.Double"))
                dt.Columns.Add("minimoModalidad3", Type.GetType("System.Double"))
                dt.Columns.Add("minimoModalidad4", Type.GetType("System.Double"))

                dr("minimoModalidad1") = 0
                dr("minimoModalidad2") = 0
                dr("minimoModalidad3") = 0
                dr("minimoModalidad4") = 0

                For Each r As DataRow In dtable.Rows
                    dr("jue_id") = r("idjuego")
                    Select Case r("idmodalidad")
                        Case 1
                            dr("minimoModalidad1") = r("importe")
                        Case 2
                            dr("minimoModalidad2") = r("importe")
                        Case 3
                            dr("minimoModalidad3") = r("importe")
                        Case 4
                            dr("minimoModalidad4") = r("importe")
                        Case Else
                    End Select
                Next
                dt.Rows.Add(dr)
                dt.AcceptChanges()
                Return dt

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Private Shared Function crea_DT_premioAdicional(ByVal idLoteria As String, ByVal idJuego As Long, ByVal numeroSorteo As Long, ByVal lstpremio As List(Of ExtractoEntities.Premio)) As DataTable
            Try
                Dim cm As SqlCommand = New SqlCommand
                Dim drAux As SqlDataReader
                Dim ds As New DataSet
                Dim sqlAdicional As String

                Dim dt As New DataTable
                Dim dr As DataRow = dt.NewRow

                dt.Columns.Add("ext_loteria")
                dt.Columns.Add("jue_id")
                dt.Columns.Add("ext_sorteo", Type.GetType("System.Int64"))
                dt.Columns.Add("ext_fecha")

                dt.Columns.Add("ext_nro1", Type.GetType("System.String"))
                dt.Columns.Add("ext_nro2", Type.GetType("System.String"))
                dt.Columns.Add("ext_nro3", Type.GetType("System.String"))
                dt.Columns.Add("ext_nro4", Type.GetType("System.String"))
                dt.Columns.Add("ext_nro5", Type.GetType("System.String"))
                dt.Columns.Add("ext_nro6", Type.GetType("System.String"))
                dt.Columns.Add("Ext_pozo1", Type.GetType("System.Double"))
                dt.Columns.Add("Ext_ganadores1", Type.GetType("System.Double"))
                dt.Columns.Add("Ext_aciertos1", Type.GetType("System.Double"))
                dt.Columns.Add("Ext_premiocupon1", Type.GetType("System.Double"))
                dt.Columns.Add("Ext_pozo2", Type.GetType("System.Double"))
                dt.Columns.Add("Ext_ganadores2", Type.GetType("System.Double"))
                dt.Columns.Add("Ext_aciertos2", Type.GetType("System.Double"))
                dt.Columns.Add("Ext_premiocupon2", Type.GetType("System.Double"))

                dt.Columns.Add("ext_pozoEstimulo", Type.GetType("System.Double"))
                dt.Columns.Add("ext_ganadoresEstimulo", Type.GetType("System.Int64"))
                dt.Columns.Add("ext_premioEstimulo", Type.GetType("System.Double"))
                dt.Columns.Add("ext_estado")
                dt.Columns.Add("ext_localidad")

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text


                dr("ext_nro1") = 0
                dr("ext_nro2") = 0
                dr("ext_nro3") = 0
                dr("ext_nro4") = 0
                dr("ext_nro5") = 0
                dr("ext_nro6") = 0

                sqlAdicional = "select e.idloteria as ext_loteria, case when p.idjuego=49 then 'P'  else  case when p.idjuego=8  then 'M'  else case when p.idjuego=3 then 'V'  else case when p.idjuego=2 then 'N' else  case when p.idjuego=1 then 'TM' else  case when p.idjuego=30 then 'PF'else  case when p.idjuego=50 then 'LO'  else case when p.idjuego=51 then 'LC' else   case when p.idjuego=13 then 'BR' else case when p.idjuego=4 then 'Q2' else '' end end end end  end  end  end end end end as ext_jue, p.idpgmsorteo as ext_sorteo, p.fechahora as ext_fecha"
                sqlAdicional = sqlAdicional & " ,coalesce(nro_qn6_bri_adi_1,'0') as ext_nro1,coalesce(nro_qn6_bri_adi_2,'0') as ext_nro2,coalesce(nro_qn6_bri_adi_3,'0') as ext_nro3,coalesce(nro_qn6_bri_adi_4,'0') as ext_nro4,coalesce(nro_qn6_bri_adi_5,'0') as ext_nro5,coalesce(nro_qn6_bri_adi_6,'0') as ext_nro6"
                sqlAdicional = sqlAdicional & " ,p.localidad as ext_localidad,p.fechahora as ext_fecha"
                sqlAdicional = sqlAdicional & " from PgmSorteo p   "
                sqlAdicional = sqlAdicional & " inner join  extracto_qn6  e on p.IdPgmSorteo = e.IdPgmSorteo  "
                sqlAdicional = sqlAdicional & " where p.IdPgmSorteo = " & numeroSorteo & " and p.idJuego = " & idJuego & " and e.idLoteria = '" & idLoteria & "'"
                cm.CommandText = sqlAdicional
                drAux = cm.ExecuteReader
                If drAux.HasRows Then
                    drAux.Read()
                    If drAux("ext_nro1").ToString.Trim <> "" And drAux("ext_nro2").ToString.Trim <> "" And drAux("ext_nro3").ToString.Trim <> "" And drAux("ext_nro4").ToString.Trim <> "" And drAux("ext_nro5").ToString.Trim <> "" And drAux("ext_nro6").ToString.Trim <> "" Then
                        dr("ext_loteria") = idLoteria
                        dr("jue_id") = idJuego
                        dr("ext_sorteo") = numeroSorteo
                        dr("ext_fecha") = drAux("ext_fecha")
                        dr("ext_nro1") = drAux("ext_nro1")
                        dr("ext_nro2") = drAux("ext_nro2")
                        dr("ext_nro3") = drAux("ext_nro3")
                        dr("ext_nro4") = drAux("ext_nro4")
                        dr("ext_nro5") = drAux("ext_nro5")
                        dr("ext_nro6") = drAux("ext_nro6")
                        dr("Ext_pozo1") = lstpremio(6).Pozo
                        dr("Ext_ganadores1") = lstpremio(6).CuponesGanadores
                        dr("Ext_aciertos1") = lstpremio(6).CantAciertos
                        dr("Ext_premiocupon1") = lstpremio(6).PremioPorCupon
                        dr("Ext_pozo2") = 0
                        dr("Ext_ganadores2") = 0
                        dr("Ext_aciertos2") = 0
                        dr("Ext_premiocupon2") = 0

                        dr("ext_pozoEstimulo") = lstpremio(10).Pozo
                        dr("ext_ganadoresEstimulo") = lstpremio(10).CuponesGanadores
                        dr("ext_premioEstimulo") = lstpremio(10).PremioPorCupon
                        dr("ext_estado") = "C"
                        dr("ext_localidad") = drAux("ext_localidad")
                    End If
                End If
                drAux.Close()
                dt.Rows.Add(dr)
                dt.AcceptChanges()
                Return dt
            Catch ex As Exception
                Return Nothing
            End Try


        End Function

    End Class
End Namespace