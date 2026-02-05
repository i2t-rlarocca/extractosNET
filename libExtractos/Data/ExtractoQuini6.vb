Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class ExtractoQuini6

        Public Shared Function GetExtracto(ByVal idLoteria As String, _
                                           ByVal idJuego As String, _
                                           ByVal numeroSorteo As Integer, _
                                           ByVal fecha As String, _
                                           ByVal recuperarSorteo As Boolean) As ExtractoEntities.ExtractoQuini6

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Try
                Dim _Extracto As ExtractoEntities.Extracto
                Dim _ExtractoQuini6 As New ExtractoEntities.ExtractoQuini6

                _Extracto = Extracto.GetExtracto(idLoteria, idJuego, numeroSorteo, True)

                _ExtractoQuini6.CantidadCifras = _Extracto.CantidadCifras
                _ExtractoQuini6.HoraSorteoOrigen = _Extracto.HoraSorteoOrigen
                _ExtractoQuini6.Id = _Extracto.Id
                _ExtractoQuini6.Numeros = _Extracto.Numeros
                _ExtractoQuini6.NumeroSorteoOrigen = _Extracto.NumeroSorteoOrigen
                _ExtractoQuini6.Sorteo = _Extracto.Sorteo

                _ExtractoQuini6.NumerosPrimerSorteo = New List(Of ExtractoEntities.Numero)
                _ExtractoQuini6.NumerosLaSegundaDelQuini = New List(Of ExtractoEntities.Numero)
                _ExtractoQuini6.NumerosRevancha = New List(Of ExtractoEntities.Numero)
                _ExtractoQuini6.NumerosSiempreSale = New List(Of ExtractoEntities.Numero)

                For Each n As ExtractoEntities.Numero In _ExtractoQuini6.Numeros
                    If n.Posicion <= 6 Then
                        _ExtractoQuini6.NumerosPrimerSorteo.Add(n)
                    End If
                    If n.Posicion > 6 And n.Posicion <= 12 Then
                        _ExtractoQuini6.NumerosLaSegundaDelQuini.Add(n)
                    End If
                    If n.Posicion > 12 And n.Posicion <= 18 Then
                        _ExtractoQuini6.NumerosRevancha.Add(n)
                    End If
                    If n.Posicion > 18 And n.Posicion <= 24 Then
                        _ExtractoQuini6.NumerosSiempreSale.Add(n)
                    End If
                Next

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                cm.CommandText = "Select * From ext_quini6 " & _
                                 "Where ext_loteria = '" & idLoteria & "' " & _
                                 " And jue_id = '" & idJuego & "' " & _
                                 " And ext_sorteo = " & numeroSorteo

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows

                    '*************************************************************************

                    _ExtractoQuini6.PremiosPrimerSorteo = New List(Of ExtractoEntities.Premio)

                    '*************************************************************************
                    Dim PremioPrimerSorteo1 As New ExtractoEntities.Premio
                    PremioPrimerSorteo1.CuponesGanadores = r("exj_gana1")
                    PremioPrimerSorteo1.Pozo = r("exj_pozo1")
                    PremioPrimerSorteo1.Premio = "1º Premio"
                    PremioPrimerSorteo1.PremioPorCupon = r("exj_pcupon1")

                    _ExtractoQuini6.PremiosPrimerSorteo.Add(PremioPrimerSorteo1)
                    '*************************************************************************
                    Dim PremioPrimerSorteo2 As New ExtractoEntities.Premio
                    PremioPrimerSorteo2.CuponesGanadores = r("exj_gana2")
                    PremioPrimerSorteo2.Pozo = r("exj_pozo2")
                    PremioPrimerSorteo2.Premio = "2º Premio"
                    PremioPrimerSorteo2.PremioPorCupon = r("exj_pcupon2")

                    _ExtractoQuini6.PremiosPrimerSorteo.Add(PremioPrimerSorteo2)
                    '*************************************************************************
                    Dim PremioPrimerSorteo3 As New ExtractoEntities.Premio
                    PremioPrimerSorteo3.CuponesGanadores = r("exj_gana3")
                    PremioPrimerSorteo3.Pozo = r("exj_pozo3")
                    PremioPrimerSorteo3.Premio = "3º Premio"
                    PremioPrimerSorteo3.PremioPorCupon = r("exj_pcupon3")

                    _ExtractoQuini6.PremiosPrimerSorteo.Add(PremioPrimerSorteo3)
                    '*************************************************************************
                    Dim PremioPrimerSorteo4 As New ExtractoEntities.Premio
                    PremioPrimerSorteo4.CuponesGanadores = r("exj_gesti")
                    PremioPrimerSorteo4.Pozo = r("exj_pesti")
                    PremioPrimerSorteo4.Premio = "ESTIMULO"
                    PremioPrimerSorteo4.PremioPorCupon = r("exj_pcpesti")

                    _ExtractoQuini6.PremiosPrimerSorteo.Add(PremioPrimerSorteo4)

                    '*************************************************************************

                    _ExtractoQuini6.PremiosLaSegundaDelQuini = New List(Of ExtractoEntities.Premio)

                    '*************************************************************************
                    Dim PremioLaSegundaDelQuini1 As New ExtractoEntities.Premio
                    PremioLaSegundaDelQuini1.CuponesGanadores = r("exj_gana2v1")
                    PremioLaSegundaDelQuini1.Pozo = r("exj_pozo2v1")
                    PremioLaSegundaDelQuini1.Premio = "1º Premio"
                    PremioLaSegundaDelQuini1.PremioPorCupon = r("exj_pcupon2v1")

                    _ExtractoQuini6.PremiosLaSegundaDelQuini.Add(PremioLaSegundaDelQuini1)
                    '*************************************************************************
                    Dim PremioLaSegundaDelQuini2 As New ExtractoEntities.Premio
                    PremioLaSegundaDelQuini2.CuponesGanadores = r("exj_gana2v2")
                    PremioLaSegundaDelQuini2.Pozo = r("exj_pozo2v2")
                    PremioLaSegundaDelQuini2.Premio = "2º Premio"
                    PremioLaSegundaDelQuini2.PremioPorCupon = r("exj_pcupon2v2")

                    _ExtractoQuini6.PremiosLaSegundaDelQuini.Add(PremioLaSegundaDelQuini2)
                    '*************************************************************************
                    Dim PremioLaSegundaDelQuini3 As New ExtractoEntities.Premio
                    PremioLaSegundaDelQuini3.CuponesGanadores = r("exj_gana2v3")
                    PremioLaSegundaDelQuini3.Pozo = r("exj_pozo2v3")
                    PremioLaSegundaDelQuini3.Premio = "3º Premio"
                    PremioLaSegundaDelQuini3.PremioPorCupon = r("exj_pcupon2v3")

                    _ExtractoQuini6.PremiosLaSegundaDelQuini.Add(PremioLaSegundaDelQuini3)
                    '*************************************************************************
                    Dim PremioLaSegundaDelQuini4 As New ExtractoEntities.Premio
                    PremioLaSegundaDelQuini4.CuponesGanadores = r("exj_gesti2v")
                    PremioLaSegundaDelQuini4.Pozo = r("exj_pesti2v")
                    PremioLaSegundaDelQuini4.Premio = "ESTIMULO"
                    PremioLaSegundaDelQuini4.PremioPorCupon = r("exj_pcpesti2v")

                    _ExtractoQuini6.PremiosLaSegundaDelQuini.Add(PremioLaSegundaDelQuini4)

                    '*************************************************************************

                    _ExtractoQuini6.PremiosRevancha = New List(Of ExtractoEntities.Premio)

                    '*************************************************************************
                    Dim PremioRevancha1 As New ExtractoEntities.Premio
                    PremioRevancha1.CuponesGanadores = r("exj_ganar")
                    PremioRevancha1.Pozo = r("exj_pozor")
                    PremioRevancha1.Premio = "1º Premio"
                    PremioRevancha1.PremioPorCupon = r("exj_pcuponr")

                    _ExtractoQuini6.PremiosRevancha.Add(PremioRevancha1)
                    '*************************************************************************
                    Dim PremioRevancha2 As New ExtractoEntities.Premio
                    PremioRevancha2.CuponesGanadores = r("exj_gestir")
                    PremioRevancha2.Pozo = r("exj_pestir")
                    PremioRevancha2.Premio = "ESTIMULO"
                    PremioRevancha2.PremioPorCupon = r("exj_pcpestir")

                    _ExtractoQuini6.PremiosRevancha.Add(PremioRevancha2)

                    '*************************************************************************

                    _ExtractoQuini6.PremiosSiempreSale = New List(Of ExtractoEntities.Premio)

                    '*************************************************************************
                    Dim PremioSiempreSale1 As New ExtractoEntities.Premio
                    PremioSiempreSale1.CuponesGanadores = r("exj_ganass")
                    '**02/10/2012
                    PremioSiempreSale1.CantAciertos = r("Exj_aciSS")
                    PremioSiempreSale1.Pozo = r("exj_pozoss")
                    PremioSiempreSale1.Premio = "1º Premio"
                    PremioSiempreSale1.PremioPorCupon = r("exj_pcuponss")

                    _ExtractoQuini6.PremiosSiempreSale.Add(PremioSiempreSale1)
                    '*************************************************************************
                    Dim PremioSiempreSale2 As New ExtractoEntities.Premio
                    PremioSiempreSale2.CuponesGanadores = r("exj_gestiss")
                    PremioSiempreSale2.Pozo = r("exj_pestiss")
                    PremioSiempreSale2.Premio = "ESTIMULO"
                    PremioSiempreSale2.PremioPorCupon = r("exj_pcpestiss")

                    _ExtractoQuini6.PremiosSiempreSale.Add(PremioSiempreSale2)

                    '*************************************************************************

                    _ExtractoQuini6.PremioExtra = New ExtractoEntities.Premio
                    _ExtractoQuini6.PremioExtra.CuponesGanadores = r("exj_canas_gana")
                    _ExtractoQuini6.PremioExtra.Pozo = r("exj_canas_pozo")
                    _ExtractoQuini6.PremioExtra.ext_canasta = r("exj_canasta")
                    _ExtractoQuini6.PremioExtra.Premio = "PREMIO EXTRA"
                    _ExtractoQuini6.PremioExtra.PremioPorCupon = r("exj_canas_premio")
                    '** 11/10/2012********
                    If _ExtractoQuini6.Sorteo.PozoProximoEstimado <= 0 Then
                        _ExtractoQuini6.Sorteo.PozoProximoEstimado = r("exj_pozo")
                    End If
                Next

                Return _ExtractoQuini6

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
            Dim dt_adicional As New DataTable
            Dim dt_Extracto As New DataTable
            Dim lstpremios As New List(Of ExtractoEntities.Premio)

            Try
                Dim _ExtractoQuini6 As New ExtractoEntities.ExtractoQuini6

                dt_Extracto = Extracto.GetExtractoDT(idLoteria, idJuego, idPgmSorteo)
                dt_Extracto.TableName = "Extracto_Quini"


                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                dt_premio = Extracto.getPremios_LocalDT(idLoteria, idJuego, idPgmSorteo, lstpremios)
                dt_premio.TableName = "Extracto_Quini_Premios"

                dt_adicional = crea_DT_premioAdicional(idLoteria, idJuego, idPgmSorteo, lstpremios)
                dt_adicional.TableName = "Extracto_Quini_Adicional"
                'dr.Close()

                '** genera datable de minimos asegurados
                Dim vsql As String
                Dim dtaux As New DataTable
                Dim dt_minimos As New DataTable
                vsql = "select * from minimosasegurados where nrosorteodesde in"
                vsql = vsql & " (select max(nrosorteodesde) "
                vsql = vsql & "  from minimosasegurados where idjuego='Q2' and nrosorteodesde<= " & idPgmSorteo & " % 1000000 )"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dtaux.Load(dr)
                dr.Close()
                dt_minimos = crea_DT_MinimosAsegurados(dtaux)
                dt_minimos.TableName = "Extracto_Quini_MinimosAsegurados"
                ds.Tables.Add(dt_Extracto)
                ds.Tables.Add(dt_premio)
                ds.Tables.Add(dt_adicional)
                ds.Tables.Add(dt_minimos)

                Return ds

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetExtractoQ6ExtraDT(ByVal idLoteria As String, _
                                      ByVal idJuego As String, _
                                      ByVal idPgmSorteo As Long, _
                                      ByVal fecha As String, _
                                      ByVal recuperarSorteo As Boolean) As DataTable

            Dim dt_Extracto As New DataTable
            Try
                Dim _ExtractoQuini6 As New ExtractoEntities.ExtractoQuini6

                dt_Extracto = Extracto.GetExtractoDT(idLoteria, idJuego, idPgmSorteo)
                dt_Extracto.TableName = "Extracto_Quini"

                Return dt_Extracto

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
                    dr("jue_id") = r("idJuego")
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


        Private Shared Function crea_DT_premioAdicional(ByVal idLoteria As String, ByVal idJuego As Long, ByVal idPgmSorteo As Long, ByVal lstpremio As List(Of ExtractoEntities.Premio)) As DataTable
            Try
                Dim cm As SqlCommand = New SqlCommand
                Dim drAux As SqlDataReader
                Dim ds As New DataSet
                Dim sqlAdicional As String

                Dim dt As New DataTable
                Dim dr As DataRow = dt.NewRow

                Dim numeroSorteo As Long = idPgmSorteo Mod 1000000

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

                sqlAdicional = "select e.idloteria as ext_loteria, case when p.idjuego=49 then 'P'  else  case when p.idjuego=8  then 'M'  else case when p.idjuego=3 then 'V'  else case when p.idjuego=2 then 'N' else  case when p.idjuego=1 then 'TM' else  case when p.idjuego=30 then 'PF'else  case when p.idjuego=50 then 'LO'  else case when p.idjuego=51 then 'LC' else   case when p.idjuego=13 then 'BR' else case when p.idjuego=4 then 'Q2' else '' end end end end  end  end  end end end end as ext_jue, p.nrosorteo as ext_sorteo, p.fechahora as ext_fecha"
                sqlAdicional = sqlAdicional & " ,coalesce(nro_qn6_bri_adi_1,'0') as ext_nro1,coalesce(nro_qn6_bri_adi_2,'0') as ext_nro2,coalesce(nro_qn6_bri_adi_3,'0') as ext_nro3,coalesce(nro_qn6_bri_adi_4,'0') as ext_nro4,coalesce(nro_qn6_bri_adi_5,'0') as ext_nro5,coalesce(nro_qn6_bri_adi_6,'0') as ext_nro6"
                sqlAdicional = sqlAdicional & " ,p.localidad as ext_localidad,p.fechahora as ext_fecha"
                sqlAdicional = sqlAdicional & " from PgmSorteo p   "
                sqlAdicional = sqlAdicional & " inner join  extracto_qn6  e on p.IdPgmSorteo = e.IdPgmSorteo  "
                sqlAdicional = sqlAdicional & " where p.IdPgmSorteo = " & idPgmSorteo & " and p.idJuego = " & idJuego & " and e.idLoteria = '" & idLoteria & "'"
                cm.CommandText = sqlAdicional
                drAux = cm.ExecuteReader
                If drAux.HasRows Then
                    drAux.Read()
                    If drAux("ext_nro1").ToString.Trim <> "" And drAux("ext_nro2").ToString.Trim <> "" And drAux("ext_nro3").ToString.Trim <> "" And drAux("ext_nro4").ToString.Trim <> "" And drAux("ext_nro5").ToString.Trim <> "" And drAux("ext_nro6").ToString.Trim <> "" Then

                        dr("ext_loteria") = idLoteria
                        dr("jue_id") = idJuego
                        dr("ext_sorteo") = numeroSorteo
                        dr("ext_fecha") = drAux("ext_fecha")
                        dr("ext_nro1") = drAux("ext_nro1") 'IIf(drAux("ext_nro1").ToString.Trim = "", 0, drAux("ext_nro1"))
                        dr("ext_nro2") = drAux("ext_nro2") 'IIf(drAux("ext_nro2").ToString.Trim = "", 0, drAux("ext_nro2"))
                        dr("ext_nro3") = drAux("ext_nro3") 'IIf(drAux("ext_nro3").ToString.Trim = "", 0, drAux("ext_nro3"))
                        dr("ext_nro4") = drAux("ext_nro4") 'IIf(drAux("ext_nro4").ToString.Trim = "", 0, drAux("ext_nro4"))
                        dr("ext_nro5") = drAux("ext_nro5") 'IIf(drAux("ext_nro5").ToString.Trim = "", 0, drAux("ext_nro5"))
                        dr("ext_nro6") = drAux("ext_nro6") 'IIf(drAux("ext_nro6").ToString.Trim = "", 0, drAux("ext_nro6"))
                        dr("Ext_pozo1") = lstpremio(11).Pozo
                        dr("Ext_ganadores1") = lstpremio(11).CuponesGanadores
                        dr("Ext_aciertos1") = lstpremio(11).CantAciertos
                        dr("Ext_premiocupon1") = lstpremio(11).PremioPorCupon
                        dr("Ext_pozo2") = lstpremio(12).Pozo
                        dr("Ext_ganadores2") = lstpremio(12).CuponesGanadores
                        dr("Ext_aciertos2") = lstpremio(12).CantAciertos
                        dr("Ext_premiocupon2") = lstpremio(12).PremioPorCupon

                        dr("ext_pozoEstimulo") = lstpremio(15).Pozo
                        dr("ext_ganadoresEstimulo") = lstpremio(15).CuponesGanadores
                        dr("ext_premioEstimulo") = lstpremio(15).PremioPorCupon
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
