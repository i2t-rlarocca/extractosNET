Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class Extracto

        Public Shared Function GetExtracto(ByVal idLoteria As String, _
                                           ByVal idJuego As String, _
                                           ByVal numeroSorteo As Integer, _
                                                                                      ByVal recuperarSorteo As Boolean) As ExtractoEntities.Extracto

            Dim o As New ExtractoEntities.Extracto
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim stabla As String = ""
            Dim ssql As String = ""
            Dim pcampos As String = ""
            Dim idPgmSorteo As Long = idJuego * 1000000 + numeroSorteo

            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text
                FileSystemHelperE.Log("entra a getextracto")

                ' Dim _fecha As String = FileSystemHelperE.cnvFechaAccess(fecha)

                'cm.CommandText = "Select * From extracto " & _
                '                 "Where ext_loteria = '" & idLoteria & "' " & _
                '                 " And ext_juego = '" & idJuego & "' " & _
                '                 " And ext_sorteo = " & numeroSorteo '& _
                Select Case idJuego
                    Case 1, 30
                        ' 1	Tómbola
                        ' 30	Poceada Federal
                        stabla = " extracto_tom "
                        '***** campos ****
                        pcampos = " ,coalesce(nro_tom_poc_1,'0') as ext_nro1,coalesce(nro_tom_poc_2,'0') as ext_nro2,coalesce(nro_tom_poc_3,'0') as ext_nro3,coalesce(nro_tom_poc_4,'0') as ext_nro4,coalesce(nro_tom_poc_5,'0') as ext_nro5,coalesce(nro_tom_poc_6,'0') as ext_nro6,coalesce(nro_tom_poc_7,'0') as ext_nro7,coalesce(nro_tom_poc_8,'0') as ext_nro8,coalesce(nro_tom_poc_9,'0') as ext_nro9,coalesce(nro_tom_poc_10,'0') as ext_nro10"
                        pcampos = pcampos & ",coalesce(nro_tom_poc_11,'0') as ext_nro11,coalesce(nro_tom_poc_12,'0') as ext_nro12,coalesce(nro_tom_poc_13,'0') as ext_nro13,coalesce(nro_tom_poc_14,'0') as ext_nro14,coalesce(nro_tom_poc_15,'0') as ext_nro15,coalesce(nro_tom_poc_16,'0') as ext_nro16,coalesce(nro_tom_poc_17,'0') as ext_nro17,coalesce(nro_tom_poc_18,'0') as ext_nro18,coalesce(nro_tom_poc_19,'0') as ext_nro19,coalesce(nro_tom_poc_20,'0') as ext_nro20"
                        pcampos = pcampos & ",'0' as ext_nro21,'0' as ext_nro22,'0' as ext_nro23,'0' as ext_nro24,'0' as ext_nro25,'0' as ext_nro26,'0' as ext_nro27,'0' as ext_nro28,'0' as ext_nro29,'0' as ext_nro30"
                        pcampos = pcampos & ",'0' as ext_nro31,'0' as ext_nro32,'0' as ext_nro33,'0' as ext_nro34,'0' as ext_nro35,'0' as ext_nro36"
                        pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                    Case 2, 3, 8, 49, 50, 51, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 50 Lotería Tradic.
                        ' 51 Lotería Chica
                        ' 26 El Ultimo
                        stabla = " extracto_qnl "
                        If idJuego = 50 Then
                            pcampos = ",coalesce(Nro1T,'0') as ext_nro1,coalesce(Nro2T,'0') as ext_nro2,coalesce(Nro3T,'0') as ext_nro3,coalesce(Nro4T,'0') as ext_nro4,coalesce(Nro5T,'0') as ext_nro5,coalesce(Nro6T,'0') as ext_nro6,coalesce(Nro7T,'0') as ext_nro7,coalesce(Nro8T,'0') as ext_nro8,coalesce(Nro9T,'0') as ext_nro9,coalesce(Nro10T,'0') as ext_nro10"
                            pcampos = pcampos & ",coalesce(Nro11T,'0') as ext_nro11,coalesce(Nro12T,'0') as ext_nro12,coalesce(Nro13T,'0') as ext_nro13,coalesce(Nro14T,'0') as ext_nro14,coalesce(Nro15T,'0') as ext_nro15,coalesce(Nro16T,'0') as ext_nro16,coalesce(Nro17T,'0') as ext_nro17,coalesce(Nro18T,'0') as ext_nro18,coalesce(Nro19T,'0') as ext_nro19,coalesce(Nro20T,'0') as ext_nro20"
                            pcampos = pcampos & ",coalesce(progres,'0') as ext_nro21,coalesce(extrac_lote_1,'0') as ext_nro22,coalesce(extrac_lote_2,'0') as ext_nro23,coalesce(extrac_lote_3,'0') as ext_nro24,coalesce(extrac_lote_4,'0') as ext_nro25,coalesce(extrac_lote_5,'0') as ext_nro26,coalesce(extrac_lote_6,'0') as ext_nro27,coalesce(extrac_lote_7,'0') as ext_nro28,coalesce(extrac_lote_8,'0') as ext_nro29,coalesce(extrac_lote_9,'0') as ext_nro30"
                            pcampos = pcampos & ",coalesce(extrac_lote_10,'0') as ext_nro31,coalesce(extrac_lote_11,'0') as ext_nro32,coalesce(extrac_lote_12,'0') as ext_nro33,coalesce(extrac_lote_13,'0') as ext_nro34,coalesce(extrac_lote_14,'0') as ext_nro35,coalesce(extrac_lote_15,'0') as ext_nro36"
                            pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                        Else

                            If idJuego = 51 Then
                                pcampos = ",coalesce(Nro1T,'0') as ext_nro1,coalesce(Nro2T,'0') as ext_nro2,coalesce(Nro3T,'0') as ext_nro3,coalesce(Nro4T,'0') as ext_nro4,coalesce(Nro5T,'0') as ext_nro5,coalesce(Nro6T,'0') as ext_nro6,coalesce(Nro7T,'0') as ext_nro7,coalesce(Nro8T,'0') as ext_nro8,coalesce(Nro9T,'0') as ext_nro9,coalesce(Nro10T,'0') as ext_nro10"
                                pcampos = pcampos & ",coalesce(Nro11T,'0') as ext_nro11,coalesce(Nro12T,'0') as ext_nro12,coalesce(Nro13T,'0') as ext_nro13,coalesce(Nro14T,'0') as ext_nro14,coalesce(Nro15T,'0') as ext_nro15,coalesce(Nro16T,'0') as ext_nro16,coalesce(Nro17T,'0') as ext_nro17,coalesce(Nro18T,'0') as ext_nro18,coalesce(Nro19T,'0') as ext_nro19,coalesce(Nro20T,'0') as ext_nro20"
                                pcampos = pcampos & ",'0' as ext_nro21,'0' as ext_nro22,'0' as ext_nro23,'0' as ext_nro24,'0' as ext_nro25,'0' as ext_nro26,'0' as ext_nro27,'0' as ext_nro28,'0' as ext_nro29,'0' as ext_nro30"
                                pcampos = pcampos & ",'0' as ext_nro31,'0' as ext_nro32,'0' as ext_nro33,'0' as ext_nro34,'0' as ext_nro35,'0' as ext_nro36"
                                pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                            Else

                                pcampos = ",coalesce(Nro1T,'0') as ext_nro1,coalesce(Nro2T,'0') as ext_nro2,coalesce(Nro3T,'0') as ext_nro3,coalesce(Nro4T,'0') as ext_nro4,coalesce(Nro5T,'0') as ext_nro5,coalesce(Nro6T,'0') as ext_nro6,coalesce(Nro7T,'0') as ext_nro7,coalesce(Nro8T,'0') as ext_nro8,coalesce(Nro9T,'0') as ext_nro9,coalesce(Nro10T,'0') as ext_nro10"
                                pcampos = pcampos & ",coalesce(Nro11T,'0') as ext_nro11,coalesce(Nro12T,'0') as ext_nro12,coalesce(Nro13T,'0') as ext_nro13,coalesce(Nro14T,'0') as ext_nro14,coalesce(Nro15T,'0') as ext_nro15,coalesce(Nro16T,'0') as ext_nro16,coalesce(Nro17T,'0') as ext_nro17,coalesce(Nro18T,'0') as ext_nro18,coalesce(Nro19T,'0') as ext_nro19,coalesce(Nro20T,'0') as ext_nro20"
                                pcampos = pcampos & ",'0' as ext_nro21,'0' as ext_nro22,'0' as ext_nro23,'0' as ext_nro24,'0' as ext_nro25,'0' as ext_nro26,'0' as ext_nro27,'0' as ext_nro28,'0' as ext_nro29,'0' as ext_nro30"
                                pcampos = pcampos & ",'0' as ext_nro31,'0' as ext_nro32,'0' as ext_nro33,'0' as ext_nro34,'0' as ext_nro35,'0' as ext_nro36"
                                pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                            End If

                        End If

                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        stabla = " extracto_qn6 "
                        pcampos = ",coalesce(nro_qn6_bri_1,'0') as ext_nro1,coalesce(nro_qn6_bri_2,'0') as ext_nro2,coalesce(nro_qn6_bri_1,'0') as ext_nro3,coalesce(nro_qn6_bri_4,'0') as ext_nro4,coalesce(nro_qn6_bri_5,'0') as ext_nro5,coalesce(nro_qn6_bri_6,'0') as ext_nro6,coalesce(nro_qn6_bri_7,'0') as ext_nro7,coalesce(nro_qn6_bri_8,'0') as ext_nro8,coalesce(nro_qn6_bri_9,'0') as ext_nro9,coalesce(nro_qn6_bri_10,'0') as ext_nro10"
                        pcampos = pcampos & ",coalesce(nro_qn6_bri_11,'0')  as ext_nro11,coalesce(nro_qn6_bri_12,'0') as ext_nro12,coalesce(nro_qn6_bri_13,'0') as ext_nro13,coalesce(nro_qn6_bri_14,'0') as ext_nro14,coalesce(nro_qn6_bri_15,'0') as ext_nro15,coalesce(nro_qn6_bri_16,'0') as ext_nro16,coalesce(nro_qn6_bri_17,'0') as ext_nro17,coalesce(nro_qn6_bri_18,'0') as ext_nro18,coalesce(nro_qn6_bri_19,'0') as ext_nro19,coalesce(nro_qn6_bri_20,'0') as ext_nro20"
                        pcampos = pcampos & ",coalesce(nro_qn6_bri_21,'0')  as ext_nro21,coalesce(nro_qn6_bri_22,'0') as ext_nro22,coalesce(nro_qn6_bri_23,'0') as ext_nro23,coalesce(nro_qn6_bri_24,'0') as ext_nro24,coalesce(nro_qn6_bri_adi_1,'0') as ext_nro25,coalesce(nro_qn6_bri_adi_2,'0') as ext_nro26,coalesce(nro_qn6_bri_adi_3,'0') as ext_nro27,coalesce(nro_qn6_bri_adi_4,'0') as ext_nro28,coalesce(nro_qn6_bri_adi_5,'0') as ext_nro29,coalesce(nro_qn6_bri_adi_6,'0') as ext_nro30"
                        pcampos = pcampos & ",coalesce(nro_qn6_bri_adi_7,'0')  as ext_nro31,coalesce(nro_qn6_bri_adi_8,'0') as ext_nro32,coalesce(nro_qn6_bri_adi_9,'0') as ext_nro33,coalesce(nro_qn6_bri_adi_10,'0') as ext_nro34,coalesce(nro_qn6_bri_adi_11,'0') as ext_nro35,coalesce(nro_qn6_bri_adi_12,'0') as ext_nro36"
                        pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                End Select


                ' ************ obtención de datos ******************
                ssql = "select e.idloteria as ext_loteria," & _
                        "     case p.idjuego " & _
                        "        when 49 then 'P' " & _
                        "        when 8  then 'M' " & _
                        "        when 3 then 'V' " & _
                        "        when 2 then 'N' " & _
                        "        when 26 then 'U' " & _
                        "        when 1 then 'TM'  " & _
                        "        when 30 then 'PF' " & _
                        "        when 50 then 'LO' " & _
                        "        when 51 then 'LC' " & _
                        "        when 13 then 'BR' " & _
                        "        when 4 then 'Q2' " & _
                        "     end as  ext_jue"
                ssql = ssql & ", p.nrosorteo as ext_sorteo"
                ssql = ssql & ", p.fechahora as ext_fecha,fechahoraprescripcion as ext_fechacaducidad"
                '** campos segun tablas
                ssql = ssql & pcampos
                ssql = ssql & " ,l.cifras as ext_ccifras,convert(varchar(10),p.fechahora,108) as ext_hora,fechahoraproximo as ext_fechaproximo, convert(varchar(10),p.fechahoraproximo,108) as  ext_horaproximo"
                ssql = ssql & "  ,a.nombre as ext_escribano ,p.localidad as ext_localidad,fechahoraproximo as ext_fechaproximo,convert(varchar(10),p.fechahora,108) as ext_horaproximo ,l.idloteria as lod_id,l.nombre as lot_desc,coalesce(l.orden_extracto_qnl,0)as lot_orden"

                ssql = ssql & "  from PgmSorteo p "
                ssql = ssql & "  inner join " & stabla & " e on p.IdPgmSorteo = e.IdPgmSorteo "
                ssql = ssql & " inner join loteria l on l.idloteria=e.idloteria"
                ssql = ssql & "  inner join pgmsorteo_autoridad pa on pa.idpgmsorteo=p.idpgmsorteo and orden=1"
                ssql = ssql & "  inner join autoridad a on pa.idautoridad=a.idautoridad"
                ssql = ssql & "  where p.IdPgmSorteo = " & idPgmSorteo & " and p.idJuego = " & idJuego & " and e.idLoteria = '" & idLoteria & "'"

                cm.CommandText = ssql

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Load(o, r, recuperarSorteo)
                Next

                Return o

            Catch ex As Exception
                FileSystemHelperE.Log("Excepcion getextracto" & ex.Message)
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function Load(ByRef o As ExtractoEntities.Extracto, _
                                    ByRef dr As DataRow, _
                                    ByVal recuperarSorteo As Boolean) As Boolean

            Try
                o.CantidadCifras = Es_Nulo(Of Integer)(dr("ext_ccifras"), 0)
                o.HoraSorteoOrigen = Es_Nulo(Of String)(dr("ext_hora"), Nothing)
                o.Id = 0
                o.FechaHoraCaducidadExtracto = Es_Nulo(Of Date)(dr("ext_fechacaducidad"), Nothing)
                o.FechaHoraProximoSorteo = Es_Nulo(Of Date)(dr("ext_fechaproximo"), Nothing)
                o.HoraProximo = Es_Nulo(Of String)(dr("ext_horaproximo"), Nothing)
                o.Escribano = Es_Nulo(Of String)(dr("ext_escribano"), Nothing)
                o.Localidad = Es_Nulo(Of String)(dr("ext_localidad"), Nothing)

                'Recuperacion de numeros **********************************************************************
                Dim ln As New List(Of ExtractoEntities.Numero)
                'el maximo de numeros es 36 dependiendo el juego
                For i = 1 To 36
                    Dim _Numero As New ExtractoEntities.Numero
                    _Numero.Posicion = i
                    _Numero.Valor = Es_Nulo(Of String)(dr("ext_nro" & i), 0)
                    ln.Add(_Numero)
                Next

                o.Numeros = ln
                'Recuperacion de numeros **********************************************************************
                o.NumeroSorteoOrigen = 0 'FALTA
                If recuperarSorteo Then
                    o.Sorteo = Sorteo.GetSorteo(Es_Nulo(Of Integer)(dr("ext_sorteo"), 0))
                End If
                Load = True
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetExtractoDT(ByVal idLoteria As String, ByVal idJuego As String, ByVal idPgmSorteo As Long) As DataTable

            Dim o As New ExtractoEntities.Extracto
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim stabla As String = ""
            Dim ssql As String = ""
            Dim pcampos As String = ""
            'Dim idPgmSorteo As Long = idJuego * 1000000 + numeroSorteo
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                'cm.CommandText = "Select * From extracto " & _
                '                 "Inner join loteria on loteria.lot_id  = extracto.ext_loteria " & _
                '                 "Where ext_loteria = '" & idLoteria & "' " & _
                '                 " And ext_juego = '" & idJuego & "' " & _
                '                 " And ext_sorteo = " & numeroSorteo '& _

                Select Case idJuego
                    Case 1, 30
                        ' 1	Tómbola
                        ' 30	Poceada Federal
                        stabla = " extracto_tom "
                        '***** campos ****
                        pcampos = " ,coalesce(nro_tom_poc_1,'0') as ext_nro1,coalesce(nro_tom_poc_2,'0') as ext_nro2,coalesce(nro_tom_poc_3,'0') as ext_nro3,coalesce(nro_tom_poc_4,'0') as ext_nro4,coalesce(nro_tom_poc_5,'0') as ext_nro5,coalesce(nro_tom_poc_6,'0') as ext_nro6,coalesce(nro_tom_poc_7,'0') as ext_nro7,coalesce(nro_tom_poc_8,'0') as ext_nro8,coalesce(nro_tom_poc_9,'0') as ext_nro9,coalesce(nro_tom_poc_10,'0') as ext_nro10"
                        pcampos = pcampos & ",coalesce(nro_tom_poc_11,'0') as ext_nro11,coalesce(nro_tom_poc_12,'0') as ext_nro12,coalesce(nro_tom_poc_13,'0') as ext_nro13,coalesce(nro_tom_poc_14,'0') as ext_nro14,coalesce(nro_tom_poc_15,'0') as ext_nro15,coalesce(nro_tom_poc_16,'0') as ext_nro16,coalesce(nro_tom_poc_17,'0') as ext_nro17,coalesce(nro_tom_poc_18,'0') as ext_nro18,coalesce(nro_tom_poc_19,'0') as ext_nro19,coalesce(nro_tom_poc_20,'0') as ext_nro20"
                        pcampos = pcampos & ",'0' as ext_nro21,'0' as ext_nro22,'0' as ext_nro23,'0' as ext_nro24,'0' as ext_nro25,'0' as ext_nro26,'0' as ext_nro27,'0' as ext_nro28,'0' as ext_nro29,'0' as ext_nro30"
                        pcampos = pcampos & ",'0' as ext_nro31,'0' as ext_nro32,'0' as ext_nro33,'0' as ext_nro34,'0' as ext_nro35,'0' as ext_nro36"
                        pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                    Case 2, 3, 8, 49, 50, 51, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 50 Lotería Tradic.
                        ' 51 Lotería Chica
                        ' 26 El Ultimo
                        stabla = " extracto_qnl "
                        If idJuego = 50 Then
                            pcampos = ",coalesce(Nro1T,'0') as ext_nro1,coalesce(Nro2T,'0') as ext_nro2,coalesce(Nro3T,'0') as ext_nro3,coalesce(Nro4T,'0') as ext_nro4,coalesce(Nro5T,'0') as ext_nro5,coalesce(Nro6T,'0') as ext_nro6,coalesce(Nro7T,'0') as ext_nro7,coalesce(Nro8T,'0') as ext_nro8,coalesce(Nro9T,'0') as ext_nro9,coalesce(Nro10T,'0') as ext_nro10"
                            pcampos = pcampos & ",coalesce(Nro11T,'0') as ext_nro11,coalesce(Nro12T,'0') as ext_nro12,coalesce(Nro13T,'0') as ext_nro13,coalesce(Nro14T,'0') as ext_nro14,coalesce(Nro15T,'0') as ext_nro15,coalesce(Nro16T,'0') as ext_nro16,coalesce(Nro17T,'0') as ext_nro17,coalesce(Nro18T,'0') as ext_nro18,coalesce(Nro19T,'0') as ext_nro19,coalesce(Nro20T,'0') as ext_nro20"
                            pcampos = pcampos & ",coalesce(progres,'0') as ext_nro21,coalesce(extrac_lote_1,'0') as ext_nro22,coalesce(extrac_lote_2,'0') as ext_nro23,coalesce(extrac_lote_3,'0') as ext_nro24,coalesce(extrac_lote_4,'0') as ext_nro25,coalesce(extrac_lote_5,'0') as ext_nro26,coalesce(extrac_lote_6,'0') as ext_nro27,coalesce(extrac_lote_7,'0') as ext_nro28,coalesce(extrac_lote_8,'0') as ext_nro29,coalesce(extrac_lote_9,'0') as ext_nro30"
                            pcampos = pcampos & ",coalesce(extrac_lote_10,'0') as ext_nro31,coalesce(extrac_lote_11,'0') as ext_nro32,coalesce(extrac_lote_12,'0') as ext_nro33,coalesce(extrac_lote_13,'0') as ext_nro34,coalesce(extrac_lote_14,'0') as ext_nro35,coalesce(extrac_lote_15,'0') as ext_nro36"
                            pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                        Else

                            If idJuego = 51 Then
                                pcampos = ",coalesce(Nro1T,'0') as ext_nro1,coalesce(Nro2T,'0') as ext_nro2,coalesce(Nro3T,'0') as ext_nro3,coalesce(Nro4T,'0') as ext_nro4,coalesce(Nro5T,'0') as ext_nro5,coalesce(Nro6T,'0') as ext_nro6,coalesce(Nro7T,'0') as ext_nro7,coalesce(Nro8T,'0') as ext_nro8,coalesce(Nro9T,'0') as ext_nro9,coalesce(Nro10T,'0') as ext_nro10"
                                pcampos = pcampos & ",coalesce(Nro11T,'0') as ext_nro11,coalesce(Nro12T,'0') as ext_nro12,coalesce(Nro13T,'0') as ext_nro13,coalesce(Nro14T,'0') as ext_nro14,coalesce(Nro15T,'0') as ext_nro15,coalesce(Nro16T,'0') as ext_nro16,coalesce(Nro17T,'0') as ext_nro17,coalesce(Nro18T,'0') as ext_nro18,coalesce(Nro19T,'0') as ext_nro19,coalesce(Nro20T,'0') as ext_nro20"
                                pcampos = pcampos & ",'0' as ext_nro21,'0' as ext_nro22,'0' as ext_nro23,'0' as ext_nro24,'0' as ext_nro25,'0' as ext_nro26,'0' as ext_nro27,'0' as ext_nro28,'0' as ext_nro29,'0' as ext_nro30"
                                pcampos = pcampos & ",'0' as ext_nro31,'0' as ext_nro32,'0' as ext_nro33,'0' as ext_nro34,'0' as ext_nro35,'0' as ext_nro36"
                                pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                            Else

                                pcampos = ",coalesce(Nro1T,'0') as ext_nro1,coalesce(Nro2T,'0') as ext_nro2,coalesce(Nro3T,'0') as ext_nro3,coalesce(Nro4T,'0') as ext_nro4,coalesce(Nro5T,'0') as ext_nro5,coalesce(Nro6T,'0') as ext_nro6,coalesce(Nro7T,'0') as ext_nro7,coalesce(Nro8T,'0') as ext_nro8,coalesce(Nro9T,'0') as ext_nro9,coalesce(Nro10T,'0') as ext_nro10"
                                pcampos = pcampos & ",coalesce(Nro11T,'0') as ext_nro11,coalesce(Nro12T,'0') as ext_nro12,coalesce(Nro13T,'0') as ext_nro13,coalesce(Nro14T,'0') as ext_nro14,coalesce(Nro15T,'0') as ext_nro15,coalesce(Nro16T,'0') as ext_nro16,coalesce(Nro17T,'0') as ext_nro17,coalesce(Nro18T,'0') as ext_nro18,coalesce(Nro19T,'0') as ext_nro19,coalesce(Nro20T,'0') as ext_nro20"
                                pcampos = pcampos & ",'0' as ext_nro21,'0' as ext_nro22,'0' as ext_nro23,'0' as ext_nro24,'0' as ext_nro25,'0' as ext_nro26,'0' as ext_nro27,'0' as ext_nro28,'0' as ext_nro29,'0' as ext_nro30"
                                pcampos = pcampos & ",'0' as ext_nro31,'0' as ext_nro32,'0' as ext_nro33,'0' as ext_nro34,'0' as ext_nro35,'0' as ext_nro36"
                                pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                            End If

                        End If

                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        stabla = " extracto_qn6 "
                        'numeroSorteo = idJuego * 1000000 + numeroSorteo
                        pcampos = ",coalesce(nro_qn6_bri_1,'0') as ext_nro1,coalesce(nro_qn6_bri_2,'0') as ext_nro2,coalesce(nro_qn6_bri_3,'0') as ext_nro3,coalesce(nro_qn6_bri_4,'0') as ext_nro4,coalesce(nro_qn6_bri_5,'0') as ext_nro5,coalesce(nro_qn6_bri_6,'0') as ext_nro6,coalesce(nro_qn6_bri_7,'0') as ext_nro7,coalesce(nro_qn6_bri_8,'0') as ext_nro8,coalesce(nro_qn6_bri_9,'0') as ext_nro9,coalesce(nro_qn6_bri_10,'0') as ext_nro10"
                        pcampos = pcampos & ",coalesce(nro_qn6_bri_11,'0')  as ext_nro11,coalesce(nro_qn6_bri_12,'0') as ext_nro12,coalesce(nro_qn6_bri_13,'0') as ext_nro13,coalesce(nro_qn6_bri_14,'0') as ext_nro14,coalesce(nro_qn6_bri_15,'0') as ext_nro15,coalesce(nro_qn6_bri_16,'0') as ext_nro16,coalesce(nro_qn6_bri_17,'0') as ext_nro17,coalesce(nro_qn6_bri_18,'0') as ext_nro18,coalesce(nro_qn6_bri_19,'0') as ext_nro19,coalesce(nro_qn6_bri_20,'0') as ext_nro20"
                        pcampos = pcampos & ",coalesce(nro_qn6_bri_21,'0')  as ext_nro21,coalesce(nro_qn6_bri_22,'0') as ext_nro22,coalesce(nro_qn6_bri_23,'0') as ext_nro23,coalesce(nro_qn6_bri_24,'0') as ext_nro24,coalesce(nro_qn6_bri_adi_1,'0') as ext_nro25,coalesce(nro_qn6_bri_adi_2,'0') as ext_nro26,coalesce(nro_qn6_bri_adi_3,'0') as ext_nro27,coalesce(nro_qn6_bri_adi_4,'0') as ext_nro28,coalesce(nro_qn6_bri_adi_5,'0') as ext_nro29,coalesce(nro_qn6_bri_adi_6,'0') as ext_nro30"
                        pcampos = pcampos & ",coalesce(nro_qn6_bri_adi_7,'0')  as ext_nro31,coalesce(nro_qn6_bri_adi_8,'0') as ext_nro32,coalesce(nro_qn6_bri_adi_9,'0') as ext_nro33,coalesce(nro_qn6_bri_adi_10,'0') as ext_nro34,coalesce(nro_qn6_bri_adi_11,'0') as ext_nro35,coalesce(nro_qn6_bri_adi_12,'0') as ext_nro36"
                        pcampos = pcampos & ",'' as ext_let1,'' as ext_let2,'' as ext_let3,'' as ext_let4,'' as ext_exporto,'' as ext_marca_trasp "
                End Select


                ' ************ obtención de datos ******************
                ssql = "select e.idloteria as ext_loteria," & _
                        "     case p.idjuego " & _
                        "        when 49 then 'P' " & _
                        "        when 8  then 'M' " & _
                        "        when 3 then 'V' " & _
                        "        when 2 then 'N' " & _
                        "        when 26 then 'U' " & _
                        "        when 1 then 'TM'  " & _
                        "        when 30 then 'PF' " & _
                        "        when 50 then 'LO' " & _
                        "        when 51 then 'LC' " & _
                        "        when 13 then 'BR' " & _
                        "        when 4 then 'Q2' " & _
                        "     end as  ext_jue"
                ssql = ssql & ", p.nrosorteo as ext_sorteo"
                ssql = ssql & ", p.fechahora as ext_fecha,fechahoraprescripcion as ext_fechacaducidad"
                '** campos segun tablas
                ssql = ssql & pcampos
                ssql = ssql & " ,l.cifras as ext_ccifras,coalesce(pl.fechahorainireal,p.fechahora) as ext_hora"
                ssql = ssql & "  ,a.nombre as ext_escribano ,p.localidad as ext_localidad,fechahoraproximo as ext_fechaproximo,p.fechahoraproximo as ext_horaproximo ,l.idloteria as lod_id,l.nombre as lot_desc,coalesce(l.orden_extracto_qnl,0)as lot_orden"

                ssql = ssql & "  from PgmSorteo p "
                ssql = ssql & "  inner join " & stabla & " e on p.IdPgmSorteo = e.IdPgmSorteo "
                ssql = ssql & " inner join loteria l on l.idloteria=e.idloteria"
                ssql = ssql & "  inner join pgmsorteo_autoridad pa on pa.idpgmsorteo=p.idpgmsorteo and orden=1"
                ssql = ssql & "  inner join autoridad a on pa.idautoridad=a.idautoridad"
                ssql = ssql & "  left join pgmsorteo_loteria pl  on e.IdPgmSorteo = pl.IdPgmSorteo and pl.idloteria=e.idloteria "
                ssql = ssql & "  where p.IdPgmSorteo = " & idPgmSorteo & " and p.idJuego = " & idJuego & " and e.idLoteria = '" & idLoteria & "'"

                cm.CommandText = ssql

                FileSystemHelperE.Log("GetExtractoDT->" & cm.CommandText)
                dr = cm.ExecuteReader()
                dt.Load(dr)
                'dr.Close()

                Return dt

            Catch ex As Exception
                dt = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Function
        '*** premios
        Public Shared Function getPremios_LocalDT(ByVal idLoteria As String, ByVal idJuego As Integer, ByVal idPgmSorteo As Long, Optional ByRef lstPremioadicional As List(Of ExtractoEntities.Premio) = Nothing) As DataTable
            Dim sSQL As String
            Dim sTabla As String = ""

            Dim cm As SqlCommand = New SqlCommand
            Dim draux As SqlDataReader
            Dim dtaux As New DataTable
            Dim r As DataRow

            Dim valorapuesta As Double = 0
            Dim pozoestimado As Double = 0
            Dim numeroSorteo As Long = idPgmSorteo Mod 1000000

            Try

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = CommandType.Text
                If idJuego = 30 Or idJuego = 50 Or idJuego = 51 Or idJuego = 13 Or idJuego = 4 Then
                    'sSQL = "select coalesce(vap_valapu,0) as valorapuesta  from valor_apuesta va left join valor_apuesta_sorteo vap on va.idvalorapuesta = vap.idvalorapuesta where idjuego=" & idJuego & " and coalesce(vap.idpgmsorteo," & idPgmSorteo & ") = " & idPgmSorteo
                    sSQL = "select coalesce(vap.vap_valapu, coalesce(va.vap_valapu,0)) as valorapuesta  from valor_apuesta va left join valor_apuesta_sorteo vap on va.idvalorapuesta = vap.idvalorapuesta where idjuego=" & Math.Round((idPgmSorteo / 1000000), 0) & " and coalesce(vap.idpgmsorteo," & idPgmSorteo & ") = " & idPgmSorteo
                    cm.CommandText = sSQL
                    draux = cm.ExecuteReader()
                    If draux.HasRows Then
                        draux.Read()
                        valorapuesta = draux("valorapuesta")
                    End If
                    draux.Close()
                End If

                sSQL = "select coalesce(p.pozoEstimadoProximo, coalesce(importe,0)) as pozo  from pgmsorteo p left join juego_pozosugerido po on p.idjuego = po.idjuego where p.idPgmSorteo = " & idPgmSorteo
                cm.CommandText = sSQL
                draux = cm.ExecuteReader()
                If draux.HasRows Then
                    draux.Read()
                    pozoestimado = draux("pozo")
                End If
                draux.Close()

                ' ************ obtención de datos ******************
                sSQL = " select p.idPremio,  "
                sSQL = sSQL & " coalesce(ps.idPgmSorteo, 0) idPgmSorteo, coalesce(importe_pozo, 0) importe_pozo, "
                sSQL = sSQL & " coalesce(cant_ganadores, 0) cant_ganadores, coalesce(importe_premio, 0) importe_premio, "
                sSQL = sSQL & " coalesce(vacante, 0) vacante, coalesce(secuencia, 0) secuencia ,COALESCE(cant_aciertos,0) as cant_aciertos"
                'sSQL = sSQL & " ,CASE WHEN COALESCE(vas.VAP_valapu,0)=0 THEN va.VAP_valapu ELSE vas.VAP_valapu END AS mod_valapu"
                'sSQL = sSQL & " , coalesce(b.importe,0)as pozoestimado"
                'sSQL = sSQL & " , coalesce(c.fechahoraproximo,'01/01/1999')as fechahoraproximo"

                sSQL = sSQL & " from premio p "
                sSQL = sSQL & " left join premio_sorteo ps on p.idPremio = ps.idPremio and ps.idPgmSorteo = " & idPgmSorteo
                '*** VALOR APUESTA
                'sSQL = sSQL & " left join  valor_apuesta_sorteo vas on  ps.idpgmsorteo = vas.idpgmsorteo "
                'sSQL = sSQL & "  left join  valor_apuesta va on p.idjuego = va.idjuego "
                '*** pozo estimado
                'sSQL = sSQL & "  left join juego_pozosugerido b on p.idjuego=b.idjuego"
                ' sSQL = sSQL & "   left join pgmsorteo c on ps.idpgmsorteo=c.idpgmsorteo"
                sSQL = sSQL & " where p.idJuego = " & idJuego & "  and habilitado=1"
                sSQL = sSQL & " order by p.idjuego,p.idmodalidad,p.orden "
                cm.CommandText = sSQL

                draux = cm.ExecuteReader()
                dtaux.Load(draux)
                draux.Close()

                ' ************ carga de las listas ******************
                Dim lstPremio As New List(Of ExtractoEntities.Premio)
                Dim oPremio As ExtractoEntities.Premio
                Dim totPremio As Integer

                For Each r In dtaux.Rows
                    oPremio = New ExtractoEntities.Premio
                    oPremio.CuponesGanadores = r("cant_ganadores")
                    oPremio.Pozo = r("importe_pozo")
                    oPremio.PremioPorCupon = r("importe_premio")
                    oPremio.CantAciertos = r("cant_aciertos")
                    'oPremio.ValorApuesta = r("mod_valapu")
                    'oPremio.PozoEstimadoJuego = r("pozoestimado")
                    'oPremio.FechahoraProximo = r("fechahoraproximo")
                    lstPremio.Add(oPremio)
                Next
                dtaux.Clear()
                dtaux = Nothing

                totPremio = lstPremio.Count
                'crea un datbale con los premios  segun del juego
                Dim dt As New DataTable
                Dim dr As DataRow = dt.NewRow
                '****crea un datable de premios segun juego
                '** campos iguales para todos los juegos
                dt.Columns.Add("ext_loteria")
                dt.Columns.Add("jue_id")
                dt.Columns.Add("ext_sorteo", Type.GetType("System.Int64"))
                dr("ext_loteria") = idLoteria
                dr("jue_id") = idJuego
                dr("ext_sorteo") = numeroSorteo ' idPgmSorteo

                Select Case idJuego
                    Case 30 ' 30	Poceada Federal
                        ' CON PREMIOS
                        dt.Columns.Add("exj_ganador1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_ganador2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_ganador3", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_ganadorestimulo", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_apuestas1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_apuestas2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_apuestas3", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_apuestasEstimulo", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_premio1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_premio2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_premio3", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_premioEstimulo", Type.GetType("System.Double"))

                        dt.Columns.Add("exj_pozoestimado", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_valorapuesta", Type.GetType("System.Double"))
                        dt.Columns.Add("mod_valapu", Type.GetType("System.Double"))

                        dr("exj_premioEstimulo") = 0
                        dr("exj_ganadorestimulo") = 0
                        dr("exj_valorapuesta") = 0
                        If totPremio > 0 Then
                            dr("mod_valapu") = valorapuesta
                            dr("exj_pozoestimado") = pozoestimado
                            dr("exj_ganador1") = lstPremio(0).CuponesGanadores
                            dr("exj_apuestas1") = lstPremio(0).Pozo
                            dr("exj_premio1") = lstPremio(0).PremioPorCupon
                        End If
                        If totPremio > 1 Then
                            dr("exj_ganador2") = lstPremio(1).CuponesGanadores
                            dr("exj_apuestas2") = lstPremio(1).Pozo
                            dr("exj_premio2") = lstPremio(1).PremioPorCupon
                        End If
                        If totPremio > 2 Then
                            dr("exj_ganador3") = lstPremio(2).CuponesGanadores
                            dr("exj_apuestas3") = lstPremio(2).Pozo
                            dr("exj_premio3") = lstPremio(2).PremioPorCupon
                        End If

                    Case 50
                        ' 50	Lotería Tradic.
                        ' CON PREMIOS

                        dt.Columns.Add("exj_importe1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe3", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe4", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe5", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe6", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe7", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe8", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe9", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe10", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe11", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe12", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe13", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe14", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe15", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe16", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe17", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe18", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe19", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe20", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_progre", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_extra", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp3", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_1aprox1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_1aprox2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_1aprox3", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_1aprox4", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_2aprox1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_2aprox2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_3aprox1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_3aprox2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_fecprox")
                        dt.Columns.Add("exj_sorprox", Type.GetType("System.Int64"))

                        '*** completa los valores

                        dr("exj_importe1") = lstPremio(0).PremioPorCupon
                        dr("exj_importe2") = lstPremio(1).PremioPorCupon
                        dr("exj_importe3") = lstPremio(2).PremioPorCupon
                        dr("exj_importe4") = lstPremio(3).PremioPorCupon
                        dr("exj_importe5") = lstPremio(4).PremioPorCupon
                        dr("exj_importe6") = lstPremio(5).PremioPorCupon
                        dr("exj_importe7") = lstPremio(6).PremioPorCupon
                        dr("exj_importe8") = lstPremio(7).PremioPorCupon
                        dr("exj_importe9") = lstPremio(8).PremioPorCupon
                        dr("exj_importe10") = lstPremio(9).PremioPorCupon
                        dr("exj_importe11") = lstPremio(10).PremioPorCupon
                        dr("exj_importe12") = lstPremio(11).PremioPorCupon
                        dr("exj_importe13") = lstPremio(12).PremioPorCupon
                        dr("exj_importe14") = lstPremio(13).PremioPorCupon
                        dr("exj_importe15") = lstPremio(14).PremioPorCupon
                        dr("exj_importe16") = lstPremio(15).PremioPorCupon
                        dr("exj_importe17") = lstPremio(16).PremioPorCupon
                        dr("exj_importe18") = lstPremio(17).PremioPorCupon
                        dr("exj_importe19") = lstPremio(18).PremioPorCupon
                        dr("exj_importe20") = lstPremio(19).PremioPorCupon

                        dr("exj_imp3") = lstPremio(20).PremioPorCupon
                        dr("exj_imp2") = lstPremio(21).PremioPorCupon
                        dr("exj_imp1") = lstPremio(22).PremioPorCupon
                        dr("exj_1aprox1") = lstPremio(23).PremioPorCupon
                        dr("exj_1aprox2") = lstPremio(24).PremioPorCupon
                        dr("exj_1aprox3") = lstPremio(25).PremioPorCupon
                        dr("exj_1aprox4") = lstPremio(26).PremioPorCupon
                        dr("exj_2aprox1") = lstPremio(27).PremioPorCupon
                        dr("exj_2aprox2") = lstPremio(28).PremioPorCupon
                        dr("exj_3aprox1") = lstPremio(29).PremioPorCupon
                        dr("exj_3aprox2") = lstPremio(30).PremioPorCupon
                        dr("exj_extra") = lstPremio(31).PremioPorCupon
                        dr("exj_progre") = lstPremio(32).PremioPorCupon
                        dr("exj_fecprox") = Now 'no se usa
                        dr("exj_sorprox") = numeroSorteo + 1

                    Case 51
                        ' 51	Lotería Chica
                        ' CON PREMIOS

                        dt.Columns.Add("exj_importe1", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe2", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe3", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe4", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_importe5", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp13", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp23", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp33", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp43", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp53", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp12", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp22", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp32", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp42", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp52", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_imp11", Type.GetType("System.Double"))
                        dt.Columns.Add("exj_fecprox")
                        dt.Columns.Add("exj_sorprox", Type.GetType("System.Double"))

                        If totPremio > 0 Then
                            dr("exj_fecprox") = Now 'no se usa
                            dr("exj_importe1") = lstPremio(0).PremioPorCupon
                            dr("exj_sorprox") = numeroSorteo + 1
                        End If
                        If totPremio > 1 Then
                            dr("exj_importe2") = lstPremio(1).PremioPorCupon
                        End If
                        If totPremio > 2 Then
                            dr("exj_importe3") = lstPremio(2).PremioPorCupon
                        End If
                        If totPremio > 3 Then
                            dr("exj_importe4") = lstPremio(3).PremioPorCupon
                        End If
                        If totPremio > 4 Then
                            dr("exj_importe5") = lstPremio(4).PremioPorCupon
                        End If
                        If totPremio > 5 Then
                            dr("exj_imp13") = lstPremio(5).PremioPorCupon
                        End If
                        If totPremio > 6 Then
                            dr("exj_imp23") = lstPremio(6).PremioPorCupon
                        End If
                        If totPremio > 7 Then
                            dr("exj_imp33") = lstPremio(7).PremioPorCupon
                        End If
                        If totPremio > 8 Then
                            dr("exj_imp43") = lstPremio(8).PremioPorCupon
                        End If
                        If totPremio > 9 Then
                            dr("exj_imp53") = lstPremio(9).PremioPorCupon
                        End If
                        If totPremio > 10 Then
                            dr("exj_imp12") = lstPremio(10).PremioPorCupon
                        End If
                        If totPremio > 11 Then
                            dr("exj_imp22") = lstPremio(11).PremioPorCupon
                        End If
                        If totPremio > 12 Then
                            dr("exj_imp32") = lstPremio(12).PremioPorCupon
                        End If
                        If totPremio > 13 Then
                            dr("exj_imp42") = lstPremio(13).PremioPorCupon
                        End If
                        If totPremio > 14 Then
                            dr("exj_imp52") = lstPremio(14).PremioPorCupon
                        End If
                        If totPremio > 15 Then
                            dr("exj_imp11") = lstPremio(15).PremioPorCupon
                        End If

                    Case 4
                        ' 4	Quini 6 
                        ' CON PREMIOS
                        dt.Columns.Add("Exj_pozo1", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana1", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pcupon1", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_pozo2", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana2", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pcupon2", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_pozo3", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana3", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pcupon3", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_Pesti", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_Gesti", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_PCpEsti", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_pozo2v1", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana2v1", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pcupon2v1", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_pozo2v2", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana2v2", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pcupon2v2", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_pozo2v3", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana2v3", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pcupon2v3", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_Pesti2v", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_Gesti2v", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_PCpEsti2v", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_pozoR", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_ganaR", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pcuponR", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_PestiR", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_GestiR", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_PCpEstiR", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_pozoSS", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_ganaSS", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_aciSS", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pcuponSS", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_PestiSS", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_GestiSS", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_PCpEstiSS", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_Pozo", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_ProxSorteo")
                        dt.Columns.Add("Exj_Mensaje")
                        dt.Columns.Add("Exj_Canasta", Type.GetType("System.Boolean"))
                        dt.Columns.Add("Exj_Canas_Pozo", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_Canas_Gana", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_Canas_Premio", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_SorProx", Type.GetType("System.Double"))

                        '***** carga los datos
                        lstPremioadicional = lstPremio
                        dr("Exj_pozo1") = lstPremio(0).Pozo
                        dr("Exj_gana1") = lstPremio(0).CuponesGanadores
                        dr("Exj_pcupon1") = lstPremio(0).PremioPorCupon

                        dr("Exj_pozo2") = lstPremio(1).Pozo
                        dr("Exj_gana2") = lstPremio(1).CuponesGanadores
                        dr("Exj_pcupon2") = lstPremio(1).PremioPorCupon

                        dr("Exj_pozo3") = lstPremio(2).Pozo
                        dr("Exj_gana3") = lstPremio(2).CuponesGanadores
                        dr("Exj_pcupon3") = lstPremio(2).PremioPorCupon

                        dr("Exj_Pesti") = lstPremio(3).Pozo
                        dr("Exj_Gesti") = lstPremio(3).CuponesGanadores
                        dr("Exj_PCpEsti") = lstPremio(3).PremioPorCupon

                        dr("Exj_pozo2v1") = lstPremio(4).Pozo
                        dr("Exj_gana2v1") = lstPremio(4).CuponesGanadores
                        dr("Exj_pcupon2v1") = lstPremio(4).PremioPorCupon

                        dr("Exj_pozo2v2") = lstPremio(5).Pozo
                        dr("Exj_gana2v2") = lstPremio(5).CuponesGanadores
                        dr("Exj_pcupon2v2") = lstPremio(5).PremioPorCupon

                        dr("Exj_pozo2v3") = lstPremio(6).Pozo
                        dr("Exj_gana2v3") = lstPremio(6).CuponesGanadores
                        dr("Exj_pcupon2v3") = lstPremio(6).PremioPorCupon

                        dr("Exj_Pesti2v") = lstPremio(7).Pozo
                        dr("Exj_Gesti2v") = lstPremio(7).CuponesGanadores
                        dr("Exj_PCpEsti2v") = lstPremio(7).PremioPorCupon

                        dr("Exj_pozoR") = lstPremio(8).Pozo
                        dr("Exj_ganaR") = lstPremio(8).CuponesGanadores
                        dr("Exj_pcuponR") = lstPremio(8).PremioPorCupon

                        dr("Exj_PestiR") = lstPremio(9).Pozo
                        dr("Exj_GestiR") = lstPremio(9).CuponesGanadores
                        dr("Exj_PCpEstiR") = lstPremio(9).PremioPorCupon


                        dr("Exj_pozoSS") = lstPremio(16).Pozo
                        dr("Exj_ganaSS") = lstPremio(16).CuponesGanadores
                        dr("Exj_aciSS") = lstPremio(16).CantAciertos
                        dr("Exj_pcuponSS") = lstPremio(16).PremioPorCupon

                        dr("Exj_PestiSS") = lstPremio(17).Pozo
                        dr("Exj_GestiSS") = lstPremio(17).CuponesGanadores
                        dr("Exj_PCpEstiSS") = lstPremio(17).PremioPorCupon

                        dr("Exj_Pozo") = pozoestimado 'lstPremio(0).PozoEstimadoJuego
                        dr("Exj_ProxSorteo") = Now 'no se usa
                        dr("Exj_Mensaje") = ""

                        dr("Exj_Canasta") = True
                        dr("Exj_Canas_Pozo") = lstPremio(10).Pozo
                        dr("Exj_Canas_Gana") = lstPremio(10).CuponesGanadores
                        dr("Exj_Canas_Premio") = lstPremio(10).PremioPorCupon

                        dr("Exj_SorProx") = numeroSorteo + 1

                    Case 13
                        dt.Columns.Add("Exj_sorprox", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pozo", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_pozo1", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana1", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_aci1", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pozo2", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana2", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_aci2", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_Pozoesti", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_ProxSorteo")
                        dt.Columns.Add("Exj_pozo3", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana3", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pozo4", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_gana4", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_pozoestimu", Type.GetType("System.Double"))
                        dt.Columns.Add("Exj_ganaestimu", Type.GetType("System.Int64"))
                        dt.Columns.Add("Exj_haySueldos")
                        dt.Columns.Add("Exj_sueldoCanti", Type.GetType("System.Int64"))
                        dt.Columns.Add("mod_valapu", Type.GetType("System.Double"))

                        '*** carga datos
                        If totPremio > 0 Then
                            lstPremioadicional = lstPremio
                            dr("Exj_sorprox") = numeroSorteo + 1
                            dr("Exj_pozo") = lstPremio(0).Pozo
                            dr("mod_valapu") = valorapuesta
                            dr("Exj_Pozoesti") = pozoestimado
                            dr("Exj_ProxSorteo") = Now 'no se usa

                            dr("Exj_pozo1") = lstPremio(0).PremioPorCupon
                            dr("Exj_gana1") = lstPremio(0).CuponesGanadores
                            dr("Exj_aci1") = lstPremio(0).CantAciertos
                        End If
                        If totPremio > 1 Then
                            dr("Exj_pozo2") = lstPremio(1).PremioPorCupon
                            dr("Exj_gana2") = lstPremio(1).CuponesGanadores
                            dr("Exj_aci2") = lstPremio(1).CantAciertos
                        End If
                        If totPremio > 2 Then
                            dr("Exj_pozo3") = lstPremio(2).PremioPorCupon
                            dr("Exj_gana3") = lstPremio(2).CuponesGanadores
                        End If
                        If totPremio > 3 Then
                            dr("Exj_pozo4") = lstPremio(3).PremioPorCupon
                            dr("Exj_gana4") = lstPremio(3).CuponesGanadores
                        End If
                        If totPremio > 4 Then
                            dr("Exj_pozoestimu") = lstPremio(4).PremioPorCupon
                            dr("Exj_ganaestimu") = lstPremio(4).CuponesGanadores
                        End If
                        'no se usan en reporte
                        Dim cantSueldos As Integer = getCantSueldos()
                        dr("Exj_haySueldos") = IIf(cantSueldos > 0, True, False)
                        dr("Exj_sueldoCanti") = cantSueldos

                End Select
                dt.Rows.Add(dr)
                dt.AcceptChanges()
                Return dt

            Catch ex As Exception
                Return Nothing
            End Try
        End Function

        Private Shared Function getCantSueldos() As Integer
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim cantSueldos As Integer = 0

            Try

                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text
                cm.CommandText = "select par_valor from parametros where par_proc = 'INI' and par_cod = 'CANTIDAD_SUELDO_BR'"
                dr = cm.ExecuteReader()
                If dr.HasRows Then
                    dr.Read()
                    cantSueldos = dr("par_valor")
                End If
                dr.Close()
                Return cantSueldos

            Catch ex As Exception
                FileSystemHelperE.Log("Excepcion getCantSueldos" & ex.Message)
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try

        End Function
    End Class
End Namespace