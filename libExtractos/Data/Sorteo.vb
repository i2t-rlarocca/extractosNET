Imports Microsoft.VisualBasic
Imports System.Data
Imports System.Data.OleDb
Imports Sorteos.Helpers
Imports System.Data.SqlClient

Namespace ExtractoData

    Public Class Sorteo
        Public Shared pathLog As String = FileSystemHelperE.pathLog

      
        Public Shared Function GetSorteoDT(ByVal idpgmsorteo As Long) As DataTable

            Dim o As New ExtractoEntities.Sorteo
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim sent As String
            Dim tabla As String = ""
            Dim idjuego As Integer = 0

            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                idjuego = (idpgmsorteo / 1000000)

                Select Case idjuego
                    Case 1, 30
                        ' 1	Tómbola
                        ' 30	Poceada Federal
                        tabla = " extracto_tom "
                    Case 2, 3, 8, 49, 50, 51, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 50 Lotería Tradic.
                        ' 51 Lotería Chica
                        ' 26 El Ultimo
                        tabla = " extracto_qnl "
                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        tabla = " extracto_qn6 "
                End Select

                sent = " select e.idloteria as Ext_loteria, " & _
                       "     case s.idjuego " & _
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
                       "     end as jue_id, " & _
                       "     s.nrosorteo as ext_sorteo, " & _
                       "     s.fechahora as ext_fecha, " & _
                       "     s.fechahoraproximo as ext_ProxSorteo, " & _
                       "     '' as ext_sigsorteo, " & _
                       "     case when idestadopgmconcurso=50 then 1 else 0 end as ext_estado, " & _
                       "     1 as id, " & _
                       "     fechahoraprescripcion as ext_caducidad, " & _
                       "     a.cargo as ext_Autor1_cargo, " & _
                       "     a.nombre as ext_Autor1_nombre, " & _
                       "     coalesce(ae.autoridad2_cargo,'') as ext_Autor2_cargo, " & _
                       "     coalesce(ae.autoridad2_nombre,'') as ext_Autor2_nombre, " & _
                       "     coalesce(ae.autoridad3_cargo,'') as ext_Autor3_cargo, " & _
                       "     coalesce(ae.autoridad3_nombre,'') as ext_Autor3_nombre, " & _
                       "     coalesce(ae.autoridad4_cargo,'') as ext_Autor4_cargo, " & _
                       "     coalesce(ae.autoridad4_nombre,'') as ext_Autor4_nombre, " & _
                       "     coalesce(ae.autoridad5_cargo,'') as ext_Autor5_cargo, " & _
                       "     coalesce(ae.autoridad5_nombre,'') as ext_Autor5_nombre, " & _
                       "     s.localidad as ext_localidad, " & _
                       "     coalesce(PozoEstimadoProximo,0) as ext_pozoProxEstimado, " & _
                       "     s.idjuego as idjuego   " & _
                       "     from pgmsorteo s  " & _
                       "      inner join " & tabla & " e on e.idpgmsorteo=s.idpgmsorteo " & _
                       "      inner join pgmsorteo_autoridad pa on pa.idpgmsorteo=s.idpgmsorteo and pa.orden = 1  " & _
                       "      inner join autoridad a on pa.idautoridad=a.idautoridad " & _
                       "      inner join autoridadextracto ae on s.idjuego=ae.idjuego " & _
                       "      where e.idloteria= ae.loteria and s.idpgmsorteo=" & idpgmsorteo

                cm.CommandText = sent
                FileSystemHelperE.Log("Consulta getsorteoDT:" & sent)
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                Return dt

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function GetFirmasDT(ByVal idpgmsorteo As Long) As DataTable

            Dim o As New ExtractoEntities.Sorteo
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim sent As String
            Dim tabla As String = ""
            Dim idjuego As Integer = 0

            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                idjuego = (idpgmsorteo / 1000000)

                Select Case idjuego
                    Case 1, 30
                        ' 1	Tómbola
                        ' 30	Poceada Federal
                        tabla = " extracto_tom "
                    Case 2, 3, 8, 49, 50, 51, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 50 Lotería Tradic.
                        ' 51 Lotería Chica
                        ' 26 El Ultimo
                        tabla = " extracto_qnl "


                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        tabla = " extracto_qn6 "
                End Select

                sent = " select auER.Autoridad1_firma, auER.Autoridad2_firma"
                sent = sent & " from pgmsorteo s"
                sent = sent & " inner join " & tabla & " e on e.idpgmsorteo=s.idpgmsorteo"

                sent = sent & " inner join pgmsorteo_autoridad pa on pa.idpgmsorteo=s.idpgmsorteo and pa.orden =1"
                sent = sent & " inner join autoridad a on pa.idautoridad=a.idautoridad"
                sent = sent & " inner join autoridadextracto ae on s.idjuego=ae.idjuego"
                sent = sent & " inner join (Select	" & idpgmsorteo & " as idpgmsorteo, Autoridad1_Cargo = (" & _
                                "select top 1 a.cargo " & _
                                "				from autoridad a  " & _
                                "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
                                "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                "                        where(ps.idpgmsorteo = " & idpgmsorteo & ") " & _
                                "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
                                "		Autoridad1_Nombre = ( " & _
                                "				select top 1 a.nombre " & _
                                "				from autoridad a  " & _
                                "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
                                "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
                                "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
                                "		Autoridad1_Firma = ( " & _
                                "				select top 1 a.firma " & _
                                "				from autoridad a  " & _
                                "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
                                "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
                                "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
                                "		Autoridad2_Cargo = ( " & _
                                "				select top 1 a.cargo " & _
                                "				from autoridad a  " & _
                                "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
                                "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
                                "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
                                "		Autoridad2_Nombre = ( " & _
                                "				select top 1 a.nombre " & _
                                "				from autoridad a  " & _
                                "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
                                "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
                                "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
                                "		Autoridad2_Firma = ( " & _
                                "				select top 1 a.firma " & _
                                "				from autoridad a  " & _
                                "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
                                "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
                                "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
                                "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
                                "        Autoridad3_Cargo = '', " & _
                                "        Autoridad3_Nombre = '', " & _
                                "        Autoridad3_Firma = 'sin_firma.PNG', " & _
                                "        Autoridad4_Cargo = '', " & _
                                "        Autoridad4_Nombre = '', " & _
                                "        Autoridad4_Firma = 'sin_firma.PNG', " & _
                                "        Autoridad5_Cargo = '', " & _
                                "        Autoridad5_Nombre = '', " & _
                                "        Autoridad5_Firma = 'sin_firma.PNG' ) auER on auER.idpgmsorteo = s.idpgmsorteo "

                sent = sent & " where e.idloteria= ae.loteria and s.idpgmsorteo=" & idpgmsorteo

                cm.CommandText = sent
                FileSystemHelperE.Log("Consulta getsorteoDT:" & sent)
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                Return dt

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try
        End Function

        ''Public Shared Function GetSorteo(ByVal idpgmsorteo As Long) As ExtractoEntities.Sorteo

        ''    Dim o As New ExtractoEntities.Sorteo
        ''    Dim cm As SqlCommand = New SqlCommand
        ''    Dim dr As SqlDataReader
        ''    Dim dt As New DataTable
        ''    Dim sent As String = ""
        ''    Dim tabla As String = ""
        ''    Dim idjuego As Integer = 0
        ''    Try
        ''        cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
        ''        cm.CommandType = Data.CommandType.Text

        ''        'cm.CommandText = "Select * From pgm_sorteos " & _
        ''        '                 "Where idpgmsorteo = '" & numeroSorteo & "'" & _
        ''        '                 " And idjuego = " & idJuego
        ''        idjuego = idpgmsorteo / 1000000
        ''        Select Case idjuego
        ''            Case 1, 30
        ''                ' 1	Tómbola
        ''                ' 30	Poceada Federal
        ''                tabla = " extracto_tom "
        ''            Case 2, 3, 8, 49, 50, 51
        ''                ' 2	Qnl. Nocturna
        ''                ' 3	Qnl. Vespertina
        ''                ' 8	Qnl. Matutina
        ''                ' 49 El Primero
        ''                ' 50 Lotería Tradic.
        ''                ' 51 Lotería Chica
        ''                tabla = " extracto_qnl "


        ''            Case 4, 13
        ''                ' 4	Quini 6 
        ''                '13	Brinco
        ''                tabla = " extracto_qn6 "
        ''        End Select


        ''        sent = " select e.idloteria as Ext_loteria,"
        ''        sent = sent & " case when s.idjuego=49 then 'P' "
        ''        sent = sent & " else "
        ''        sent = sent & " case when s.idjuego=8  then 'M' "
        ''        sent = sent & " else case when s.idjuego=3 then 'V' "
        ''        sent = sent & " else case when s.idjuego=2 then 'N' else "
        ''        sent = sent & " case when s.idjuego=1 then 'TM' else "
        ''        sent = sent & " case when s.idjuego=30 then 'PF'else "
        ''        sent = sent & " case when s.idjuego=50 then 'LO' "
        ''        sent = sent & " else case when s.idjuego=51 then 'LC' else  "
        ''        sent = sent & " case when s.idjuego=13 then 'BR' else"
        ''        sent = sent & " case when s.idjuego=4 then 'Q2' else ''"
        ''        sent = sent & " end end end end  end  end  end end end end as jue_id"
        ''        sent = sent & " ,s.nrosorteo as ext_sorteo,s.fechahora as ext_fecha,s.fechahoraproximo as ext_ProxSorteo,'' as ext_sigsorteo ,case when idestadopgmconcurso=50 then 1 else 0 end as ext_estado,1 as id,fechahoraprescripcion as ext_caducidad,a.cargo as ext_Autor1_cargo,a.nombre as ext_Autor1_nombre"
        ''        sent = sent & ",case when ae.idloteria ='E' then auER.autoridad2_cargo else coalesce(ae.autoridad2_cargo,'') end as ext_Autor2_cargo,case when ae.idloteria ='E' then auER.autoridad2_nombre else coalesce(ae.autoridad2_nombre,'') end as ext_Autor2_nombre"
        ''        sent = sent & ",case when ae.idloteria ='E' then auER.autoridad3_cargo else coalesce(ae.autoridad3_cargo,'') end as ext_Autor3_cargo,case when ae.idloteria ='E' then auER.autoridad3_nombre else coalesce(ae.autoridad3_nombre,'') end as ext_Autor3_nombre"
        ''        sent = sent & ",case when ae.idloteria ='E' then auER.autoridad4_cargo else coalesce(ae.autoridad4_cargo,'') end as ext_Autor4_cargo,case when ae.idloteria ='E' then auER.autoridad4_nombre else coalesce(ae.autoridad4_nombre,'') end as ext_Autor4_nombre"
        ''        sent = sent & ",case when ae.idloteria ='E' then auER.autoridad5_cargo else coalesce(ae.autoridad5_cargo,'') end as ext_Autor5_cargo,case when ae.idloteria ='E' then auER.autoridad5_nombre else coalesce(ae.autoridad5_nombre,'') end as ext_Autor5_nombre"
        ''        sent = sent & ",s.localidad as ext_localidad,coalesce(PozoEstimadoProximo,0) as ext_pozoProxEstimado,s.idjuego as idjuego "
        ''        sent = sent & " from pgmsorteo s"
        ''        sent = sent & " inner join " & tabla & " e on e.idpgmsorteo=s.idpgmsorteo"

        ''        sent = sent & " inner join pgmsorteo_autoridad pa on pa.idpgmsorteo=s.idpgmsorteo and pa.orden =1"
        ''        sent = sent & " inner join autoridad a on pa.idautoridad=a.idautoridad"
        ''        sent = sent & " inner join autoridadextracto ae on s.idjuego=ae.idjuego"
        ''        sent = sent & " inner join (Select	" & idpgmsorteo & ", Autoridad1_Cargo = (" & _
        ''                        "select top 1 a.cargo " & _
        ''                        "				from autoridad a  " & _
        ''                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
        ''                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
        ''                        "                        where(ps.idpgmsorteo = " & idpgmsorteo & ") " & _
        ''                        "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
        ''                        "		Autoridad1_Nombre = ( " & _
        ''                        "				select top 1 a.nombre " & _
        ''                        "				from autoridad a  " & _
        ''                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
        ''                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
        ''                        "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
        ''                        "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
        ''                        "		Autoridad1_Firma = ( " & _
        ''                        "				select top 1 a.firma " & _
        ''                        "				from autoridad a  " & _
        ''                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
        ''                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
        ''                        "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
        ''                        "				and upper(ltrim(rtrim(a.cargo))) = 'AREA NOTARIAL'), " & _
        ''                        "		Autoridad2_Cargo = ( " & _
        ''                        "				select top 1 a.cargo " & _
        ''                        "				from autoridad a  " & _
        ''                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
        ''                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
        ''                        "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
        ''                        "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
        ''                        "		Autoridad2_Nombre = ( " & _
        ''                        "				select top 1 a.nombre " & _
        ''                        "				from autoridad a  " & _
        ''                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
        ''                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
        ''                        "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
        ''                        "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
        ''                        "		Autoridad2_Firma = ( " & _
        ''                        "				select top 1 a.firma " & _
        ''                        "				from autoridad a  " & _
        ''                        "				inner join PgmSorteo_Autoridad b on a.idautoridad = b.idautoridad " & _
        ''                        "				inner join pgmsorteo ps on b.idpgmsorteo = ps.IDpgmsorteo " & _
        ''                        "				where ps.idpgmsorteo = " & idpgmsorteo & "  " & _
        ''                        "				and upper(ltrim(rtrim(a.cargo))) = 'JEFE DE SORTEO'), " & _
        ''                        "        Autoridad3_Cargo = '', " & _
        ''                        "        Autoridad3_Nombre = '', " & _
        ''                        "        Autoridad3_Firma = 'sin_firma.PNG', " & _
        ''                        "        Autoridad4_Cargo = '', " & _
        ''                        "        Autoridad4_Nombre = '', " & _
        ''                        "        Autoridad4_Firma = 'sin_firma.PNG', " & _
        ''                        "        Autoridad5_Cargo = '', " & _
        ''                        "        Autoridad5_Nombre = '', " & _
        ''                        "        Autoridad5_Firma = 'sin_firma.PNG' ) auER on auER.idpgmsorteo = s.idpgmsorteo "

        ''        sent = sent & " where e.idloteria= ae.idloteria and s.idpgmsorteo=" & idpgmsorteo

        ''        cm.CommandText = sent
        ''        dr = cm.ExecuteReader()
        ''        dt.Load(dr)
        ''        dr.Close()

        ''        For Each r As DataRow In dt.Rows
        ''            Load(o, r)
        ''        Next

        ''        Return o

        ''    Catch ex As Exception
        ''        FileSystemHelperE.Log("Excepcion getsorteo:" & ex.Message)
        ''        If Not dr Is Nothing Then
        ''            If Not dr.IsClosed Then dr.Close()
        ''        End If
        ''        Throw New Exception(ex.Message)
        ''    End Try
        ''End Function

        Public Shared Function GetSorteo(ByVal idPgmSorteo As Long) As ExtractoEntities.Sorteo

            Dim o As New ExtractoEntities.Sorteo
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim sent As String = ""
            Dim tabla As String = ""
            Dim idjuego As Integer = 0
            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text

                'cm.CommandText = "Select * From pgm_sorteos " & _
                '                 "Where idpgmsorteo = '" & numeroSorteo & "'" & _
                '                 " And idjuego = " & idJuego
                If idpgmsorteo > 1000000 Then
                    idjuego = idpgmsorteo / 1000000
                End If
                Select Case idjuego
                    Case 1, 30
                        ' 1	Tómbola
                        ' 30	Poceada Federal
                        tabla = " extracto_tom "
                    Case 2, 3, 8, 49, 50, 51, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 50 Lotería Tradic.
                        ' 51 Lotería Chica
                        ' 26 El Ultimo
                        tabla = " extracto_qnl "
                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        tabla = " extracto_qn6 "
                End Select

                ''sent = " select e.idloteria as Ext_loteria,"
                ''sent = sent & " case when s.idjuego=49 then 'P' "
                ''sent = sent & " else "
                ''sent = sent & " case when s.idjuego=8  then 'M' "
                ''sent = sent & " else case when s.idjuego=3 then 'V' "
                ''sent = sent & " else case when s.idjuego=2 then 'N' else "
                ''sent = sent & " case when s.idjuego=1 then 'TM' else "
                ''sent = sent & " case when s.idjuego=30 then 'PF'else "
                ''sent = sent & " case when s.idjuego=50 then 'LO' "
                ''sent = sent & " else case when s.idjuego=51 then 'LC' else  "
                ''sent = sent & " case when s.idjuego=13 then 'BR' else"
                ''sent = sent & " case when s.idjuego=4 then 'Q2' else ''"
                ''sent = sent & " end end end end  end  end  end end end end as jue_id"
                ''sent = sent & " ,s.nrosorteo as ext_sorteo,s.fechahora as ext_fecha,s.fechahoraproximo as ext_ProxSorteo,'' as ext_sigsorteo ,case when idestadopgmconcurso=50 then 1 else 0 end as ext_estado,1 as id,fechahoraprescripcion as ext_caducidad,a.cargo as ext_Autor1_cargo,a.nombre as ext_Autor1_nombre"
                ''sent = sent & ",coalesce(ae.autoridad2_cargo,'')as ext_Autor2_cargo,coalesce(ae.autoridad2_nombre,'')as ext_Autor2_nombre"
                ''sent = sent & ",coalesce(ae.autoridad3_cargo,'')as ext_Autor3_cargo,coalesce(ae.autoridad3_nombre,'')as ext_Autor3_nombre"
                ''sent = sent & ",coalesce(ae.autoridad4_cargo,'')as ext_Autor4_cargo,coalesce(ae.autoridad4_nombre,'')as ext_Autor4_nombre"
                ''sent = sent & ",coalesce(ae.autoridad5_cargo,'')as ext_Autor5_cargo,coalesce(ae.autoridad5_nombre,'')as ext_Autor5_nombre"
                ''sent = sent & ",s.localidad as ext_localidad,coalesce(PozoEstimadoProximo,0) as ext_pozoProxEstimado,s.idjuego as idjuego "
                ''sent = sent & " from pgmsorteo s"
                ''sent = sent & " inner join " & tabla & " e on e.idpgmsorteo=s.idpgmsorteo"

                ''sent = sent & " inner join pgmsorteo_autoridad pa on pa.idpgmsorteo=s.idpgmsorteo and pa.orden =1"
                ''sent = sent & " inner join autoridad a on pa.idautoridad=a.idautoridad"
                ''sent = sent & " inner join autoridadextracto ae on s.idjuego=ae.idjuego"
                ''sent = sent & " where e.idloteria= ae.loteria and s.idpgmsorteo=" & idPgmSorteo

                sent = " select e.idloteria as Ext_loteria, " & _
       "     case s.idjuego " & _
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
       "     end as jue_id, " & _
       "     s.nrosorteo as ext_sorteo, " & _
       "     s.fechahora as ext_fecha, " & _
       "     s.fechahoraproximo as ext_ProxSorteo, " & _
       "     '' as ext_sigsorteo, " & _
       "     case when idestadopgmconcurso=50 then 1 else 0 end as ext_estado, " & _
       "     1 as id, " & _
       "     fechahoraprescripcion as ext_caducidad, " & _
       "     a.cargo as ext_Autor1_cargo, " & _
       "     a.nombre as ext_Autor1_nombre, " & _
       "     coalesce(ae.autoridad2_cargo,'') as ext_Autor2_cargo, " & _
       "     coalesce(ae.autoridad2_nombre,'') as ext_Autor2_nombre, " & _
       "     coalesce(ae.autoridad3_cargo,'') as ext_Autor3_cargo, " & _
       "     coalesce(ae.autoridad3_nombre,'') as ext_Autor3_nombre, " & _
       "     coalesce(ae.autoridad4_cargo,'') as ext_Autor4_cargo, " & _
       "     coalesce(ae.autoridad4_nombre,'') as ext_Autor4_nombre, " & _
       "     coalesce(ae.autoridad5_cargo,'') as ext_Autor5_cargo, " & _
       "     coalesce(ae.autoridad5_nombre,'') as ext_Autor5_nombre, " & _
       "     s.localidad as ext_localidad, " & _
       "     coalesce(PozoEstimadoProximo,0) as ext_pozoProxEstimado, " & _
       "     s.idjuego as idjuego   " & _
       "     from pgmsorteo s  " & _
       "      inner join " & tabla & " e on e.idpgmsorteo=s.idpgmsorteo " & _
       "      inner join pgmsorteo_autoridad pa on pa.idpgmsorteo=s.idpgmsorteo and pa.orden = 1  " & _
       "      inner join autoridad a on pa.idautoridad=a.idautoridad " & _
       "      inner join autoridadextracto ae on s.idjuego=ae.idjuego " & _
       "      where e.idloteria= ae.loteria and s.idpgmsorteo=" & idPgmSorteo
                cm.CommandText = sent
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Load(o, r)
                Next

                Return o

            Catch ex As Exception
                FileSystemHelperE.Log("Excepcion getsorteo:" & ex.Message)
                If Not dr Is Nothing Then
                    If Not dr.IsClosed Then dr.Close()
                End If
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Shared Function Load(ByRef o As ExtractoEntities.Sorteo, ByRef dr As DataRow) As Boolean
            Try
                Dim idJuego As String
                Dim idPgmSorteo As Long
                Dim hora, fechaHora As Date

                o.Estado = Es_Nulo(Of Integer)(dr("ext_estado"), 0)
                o.FechaHoraCaducidadSorteo = Es_Nulo(Of Date)(dr("ext_caducidad"), Nothing)
                o.FechaHoraProximoSorteo = Es_Nulo(Of Date)(dr("ext_proxsorteo"), Nothing)
                o.FechaHoraSorteo = Es_Nulo(Of Date)(dr("ext_fecha"), Nothing)
                o.Id = Es_Nulo(Of Integer)(dr("id"), 0)
                '*****************************************************************************
                o.Numero = Es_Nulo(Of Integer)(dr("ext_sorteo"), 0)
                o.PozoProximoEstimado = Es_Nulo(Of Double)(dr("ext_pozoproxestimado"), 0)
                '*****************************************************************************
                idJuego = Es_Nulo(Of String)(dr("jue_id"), "")
                idPgmSorteo = Es_Nulo(Of Integer)(dr("idjuego"), 0) * 1000000 + o.Numero

                'If o.FechaHoraSorteo.TimeOfDay.ToString = "00:00:00" Then
                '    hora = Juego.GetHoraSorteo(idJuego)
                '    fechaHora = o.FechaHoraSorteo.AddMinutes((hora.Hour * 60) + hora.Minute)
                '    o.FechaHoraSorteo = fechaHora
                'End If

                '*****************************************************************************
                Dim la As New List(Of ExtractoEntities.Autoridad)

                Dim _ListaAutoridades As List(Of ExtractoEntities.Autoridad)

                _ListaAutoridades = ExtractoData.Autoridad.GetAutoridades(dr("idjuego"), dr("ext_loteria"))

                Dim extr As ExtractoEntities.Extracto
                extr = ExtractoData.Extracto.GetExtracto(dr("ext_loteria"), dr("idjuego"), dr("ext_sorteo"), False)

                'Hora Sorteo
                If o.FechaHoraSorteo.TimeOfDay.ToString = "00:00:00" Then
                    'No esta cargada la hora de sorteo en el pgm_sorteos
                    If extr.HoraSorteoOrigen = Nothing Then
                        'No esta cargada la hora de sorteo en Extracto
                        hora = Juego.GetHoraSorteo(idJuego)
                        fechaHora = o.FechaHoraSorteo.AddMinutes((hora.Hour * 60) + hora.Minute)
                        o.FechaHoraSorteo = fechaHora
                    Else
                        'Se recupera la hora de sorteo en Extracto
                        hora = extr.HoraSorteoOrigen
                        fechaHora = o.FechaHoraSorteo.AddMinutes((hora.Hour * 60) + hora.Minute)
                        o.FechaHoraSorteo = fechaHora
                    End If
                End If

                'Fecha Proximo
                If o.FechaHoraProximoSorteo.Year = 1 Then
                    'No esta cargada la fecha de proximo sorteo en pgm_sorteo
                    If extr.FechaHoraProximoSorteo.Year <> 1 Then
                        o.FechaHoraProximoSorteo = Es_Nulo(Of Date)(extr.FechaHoraProximoSorteo, Nothing)
                    Else
                        o.FechaHoraProximoSorteo = Es_Nulo(Of Date)(FormatDateTime(Date.Now, DateFormat.ShortDate), Nothing)
                    End If
                End If


                'Hora Proximo
                If o.FechaHoraProximoSorteo.TimeOfDay.ToString = "00:00:00" And o.FechaHoraProximoSorteo.Year <> 1 Then
                    If extr.HoraProximo = Nothing Then
                        hora = FormatDateTime(o.FechaHoraSorteo, DateFormat.LongTime)
                        fechaHora = o.FechaHoraProximoSorteo.AddMinutes((hora.Hour * 60) + hora.Minute)
                        o.FechaHoraProximoSorteo = fechaHora
                    Else
                        hora = extr.HoraProximo
                        fechaHora = o.FechaHoraProximoSorteo.AddMinutes((hora.Hour * 60) + hora.Minute)
                        o.FechaHoraProximoSorteo = fechaHora
                    End If

                End If

                'Escribano
                Dim a1 As New ExtractoEntities.Autoridad
                a1.Cargo = Es_Nulo(Of String)(dr("ext_autor1_cargo"), "")
                a1.Nombre = Es_Nulo(Of String)(dr("ext_autor1_nombre"), "")
                'a1.Firma = _ListaAutoridades(0).Firma
                If dr("ext_estado") = 0 Then
                    a1.Cargo = _ListaAutoridades(0).Cargo
                    If extr.Escribano = Nothing Then
                        a1.Nombre = _ListaAutoridades(0).Nombre
                    Else
                        a1.Nombre = extr.Escribano
                    End If
                End If

                la.Add(a1)

                Dim a2 As New ExtractoEntities.Autoridad
                a2.Cargo = Es_Nulo(Of String)(dr("ext_autor2_cargo"), "")
                a2.Nombre = Es_Nulo(Of String)(dr("ext_autor2_nombre"), "")
                'a2.Firma = _ListaAutoridades(1).Firma
                If dr("ext_estado") = 0 Then
                    a2.Cargo = _ListaAutoridades(1).Cargo
                    a2.Nombre = _ListaAutoridades(1).Nombre
                End If
                la.Add(a2)

                Dim a3 As New ExtractoEntities.Autoridad
                a3.Cargo = Es_Nulo(Of String)(dr("ext_autor3_cargo"), "")
                a3.Nombre = Es_Nulo(Of String)(dr("ext_autor3_nombre"), "")
                'a3.Firma = _ListaAutoridades(2).Firma
                If dr("ext_estado") = 0 Then
                    a3.Cargo = _ListaAutoridades(2).Cargo
                    a3.Nombre = _ListaAutoridades(2).Nombre
                End If
                la.Add(a3)

                Dim a4 As New ExtractoEntities.Autoridad
                a4.Cargo = Es_Nulo(Of String)(dr("ext_autor4_cargo"), "")
                a4.Nombre = Es_Nulo(Of String)(dr("ext_autor4_nombre"), "")
                'a4.Firma = _ListaAutoridades(3).Firma
                If dr("ext_estado") = 0 Then
                    a4.Cargo = _ListaAutoridades(3).Cargo
                    a4.Nombre = _ListaAutoridades(3).Nombre
                End If
                la.Add(a4)

                Dim a5 As New ExtractoEntities.Autoridad
                a5.Cargo = Es_Nulo(Of String)(dr("ext_autor5_cargo"), "")
                a5.Nombre = Es_Nulo(Of String)(dr("ext_autor5_nombre"), "")
                'a5.Firma = _ListaAutoridades(4).Firma
                If dr("ext_estado") = 0 Then
                    a5.Cargo = _ListaAutoridades(4).Cargo
                    a5.Nombre = _ListaAutoridades(4).Nombre
                End If
                la.Add(a5)

                Dim jl As New ExtractoEntities.JuegoLoteria
                jl.Autoridades = la
                jl.HoraSorteo = o.FechaHoraSorteo.TimeOfDay.ToString
                jl.Juego = Juego.GetJuego(dr("idjuego"))
                For Each m As ExtractoEntities.ModalidadJuego In jl.Juego.Modalidades
                    m.ValorApuesta = ModalidadJuego.GetValorApuesta(idPgmSorteo)
                Next
                jl.Loteria = Loteria.GetLoteria(Es_Nulo(Of String)(dr("ext_loteria"), ""))
                o.JuegoLoteria = jl
                '*****************************************************************************
                '*****************************************************************************
                Dim l As New ExtractoEntities.Localidad
                l.Nombre = Es_Nulo(Of String)(dr("ext_localidad"), "")
                o.Localidad = l
                o.LotSorteo = GetLotSorteo(dr("idjuego"), o.Numero)

                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception("Data.Sorteo.Load - " & ex.Message)
            End Try
        End Function


        Public Shared Function GetLotSorteo(ByVal juego As Integer, ByVal numero As String) As List(Of String)

            Dim l As List(Of String) = New List(Of String)

            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim Stabla As String = ""
            Dim idPgmSorteo As Long = juego * 1000000 + numero

            Try
                cm.Connection = Sorteos.Helpers.General.Obtener_Conexion
                cm.CommandType = Data.CommandType.Text
                FileSystemHelperE.Log("entra a getlotsorteo")
                Dim snt As String
                Select Case juego
                    Case 1, 30
                        ' 1	Tómbola
                        ' 30	Poceada Federal
                        Stabla = " extracto_tom "

                    Case 2, 3, 8, 49, 50, 51, 26
                        ' 2	Qnl. Nocturna
                        ' 3	Qnl. Vespertina
                        ' 8	Qnl. Matutina
                        ' 49 El Primero
                        ' 50 Lotería Tradic.
                        ' 51 Lotería Chica
                        ' 26 El Ultimo
                        Stabla = " extracto_qnl "

                    Case 4, 13
                        ' 4	Quini 6 
                        '13	Brinco
                        Stabla = " extracto_qn6 "
                End Select



                snt = "Select * From loteria "
                snt = snt & " inner join " & Stabla & " E on loteria.idLOTERIA = e.IDloteria "
                snt = snt & " Where  idpgmsorteo = @idPgmSorteo "
                snt = snt & " order by orden_extracto_qnl "

                'cm.Parameters.AddWithValue("@id_juego", juego)
                cm.Parameters.AddWithValue("@idPgmSorteo", idPgmSorteo)

                cm.CommandText = snt

                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    Dim a As String = r("IdLoteria")
                    l.Add(a)
                Next

                Return l

            Catch ex As Exception
                FileSystemHelperE.Log("getlotsorteo:" & ex.Message)
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
            End Try
        End Function

        Protected Shared Sub Log(ByVal texto As String)

            'creamos el nombre del archivo
            'Dim archivo = ConfigurationManager.AppSettings("pathLogs").ToString & "\pruebas.txt"
            Dim archivo = FileSystemHelperE.pathLog & "\pruebas.txt"

            'conectamos con el FSO
            Dim confile = CreateObject("scripting.filesystemobject")

            'creamos el objeto TextStream
            Dim fich = confile.OpenTextFile(archivo, 8, True)

            'escribimos los números del 0 al 9
            fich.writeLine(texto)

            'cerramos el fichero
            fich.close()
        End Sub
        

    End Class
End Namespace