Imports System.Data
Imports System.Data.SqlClient
Imports libEntities.Entities
Imports Sorteos.Helpers
Imports Sorteos.Helpers.General
Namespace Data



    Public Class ExtraccionesLoteriaDAL

        Public Function getExtraccionesLoteria(ByVal pidPgmSorteo As Integer, ByVal pidLoteria As Char) As Extracto_qnl

            Dim o As New Extracto_qnl
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT * " _
                       & " FROM " _
                       & " extracto_qnl where idpgmsorteo=" & pidPgmSorteo & " and idloteria='" & pidLoteria & "'"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New Extracto_qnl
                    Load(o, r, True)
                Next

                Return o

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function Load(ByRef o As Extracto_qnl, _
                                    ByRef dr As DataRow, _
                                    ByVal recuperarObjComponentes As Boolean) As Boolean

            Try
                Dim _valor As cPosicionValorLoterias
                Dim _lsValores As New ListaOrdenada(Of cPosicionValorLoterias)
                Dim _existeValor As String
                Dim i As Integer

                o.idLoteria = dr("idLoteria")
                o.idPgmSorteo = dr("idPgmSorteo")
                o.Cifras = dr("Cifras")
                o.coddescr = Es_Nulo(Of String)(dr("coddescr"), "")
                o.extract_lote_1 = Es_Nulo(Of Char)(dr("extrac_lote_1"), "")
                o.extract_lote_2 = Es_Nulo(Of Char)(dr("extrac_lote_2"), "")
                o.extract_lote_3 = Es_Nulo(Of Char)(dr("extrac_lote_3"), "")
                o.extract_lote_4 = Es_Nulo(Of Char)(dr("extrac_lote_4"), "")
                o.extract_lote_5 = Es_Nulo(Of Char)(dr("extrac_lote_5"), "")
                o.extract_lote_6 = Es_Nulo(Of Char)(dr("extrac_lote_6"), "")
                o.extract_lote_7 = Es_Nulo(Of Char)(dr("extrac_lote_7"), "")
                o.extract_lote_8 = Es_Nulo(Of Char)(dr("extrac_lote_8"), "")
                o.extract_lote_9 = Es_Nulo(Of Char)(dr("extrac_lote_9"), "")
                o.extract_lote_10 = Es_Nulo(Of Char)(dr("extrac_lote_10"), "")
                o.extract_lote_11 = Es_Nulo(Of Char)(dr("extrac_lote_11"), "")
                o.extract_lote_12 = Es_Nulo(Of Char)(dr("extrac_lote_12"), "")
                o.extract_lote_13 = Es_Nulo(Of Char)(dr("extrac_lote_13"), "")
                o.extract_lote_14 = Es_Nulo(Of Char)(dr("extrac_lote_14"), "")
                o.extract_lote_15 = Es_Nulo(Of Char)(dr("extrac_lote_15"), "")
                o.Loteria = dr("Loteria")
                o.Progres = Es_Nulo(Of Char)(("Progres"), "")

                For i = 1 To 20
                    _existeValor = Es_Nulo(Of String)(dr("Nro" & i & "T"), "")
                    _valor = New cPosicionValorLoterias
                    _valor.Posicion = i
                    If _existeValor.Trim <> "" Then
                        _valor.Valor = dr("Nro" & i & "T")
                    Else
                        _valor.Valor = ""
                    End If
                    _lsValores.Add(_valor)
                Next
                o.Valores = _lsValores
                Load = True
            Catch ex As Exception
                Load = False
                Throw New Exception(ex.Message)
            End Try
        End Function
        Public Function getExtructurasLetras(ByVal pIdpgmSorteo As Integer, ByVal pidLoteria As Loteria) As ListaOrdenada(Of extracto_qnl_letras)
            Dim ls As New ListaOrdenada(Of extracto_qnl_letras)
            Dim oExtracto_Qnl_letras As extracto_qnl_letras
            Dim oExtracto_Qnl_letrasAux As extracto_qnl_letras
            Dim lsLetras As New ListaOrdenada(Of extracto_qnl_letras)
            Dim lsLetrasCompleta As New ListaOrdenada(Of extracto_qnl_letras)
            Try
                If pidLoteria.letras_extracto_qnl = False Then
                    Return Nothing
                    Exit Function
                End If
                ls = getExtracciones_QNL_Letras(pIdpgmSorteo, pidLoteria.IdLoteria)
                'creo la una lista con la estrutctura vacia
                For i As Integer = 1 To pidLoteria.cant_letras_extracto
                    oExtracto_Qnl_letras = New extracto_qnl_letras
                    oExtracto_Qnl_letras.Orden = i
                    oExtracto_Qnl_letras.letra = ""
                    lsLetras.Add(oExtracto_Qnl_letras)
                Next

                For Each oExtracto_Qnl_letrasAux In lsLetras
                    oExtracto_Qnl_letrasAux.idLoteria = pidLoteria.IdLoteria
                    oExtracto_Qnl_letrasAux.idPgmSorteo = pIdpgmSorteo
                    oExtracto_Qnl_letrasAux.letra = ""
                    For Each oExtracto_Qnl_letras In ls
                        If oExtracto_Qnl_letrasAux.Orden = oExtracto_Qnl_letras.Orden Then
                            oExtracto_Qnl_letrasAux = oExtracto_Qnl_letras
                        End If
                    Next
                    lsLetrasCompleta.Add(oExtracto_Qnl_letrasAux)
                Next
                Return lsLetrasCompleta

            Catch ex As Exception

                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function


        Public Function getExtracciones_QNL_Letras(ByVal pidPgmSorteo As Integer, ByVal pidloteria As Char) As ListaOrdenada(Of extracto_qnl_letras)
            Dim ls As New ListaOrdenada(Of extracto_qnl_letras)
            Dim o As New extracto_qnl_letras
            Dim cm As SqlCommand = New SqlCommand
            Dim dr As SqlDataReader
            Dim dt As New DataTable
            Dim vsql As String
            Try
                cm.Connection = General.Obtener_Conexion
                cm.CommandType = CommandType.Text

                vsql = "SELECT * " _
                       & " FROM " _
                       & " extracto_qnl_letras where idpgmsorteo=" & pidPgmSorteo & " and idloteria='" & pidloteria & "'"
                cm.CommandText = vsql
                dr = cm.ExecuteReader()
                dt.Load(dr)
                dr.Close()

                For Each r As DataRow In dt.Rows
                    o = New extracto_qnl_letras
                    LoadExtraciones_Qnl_Letras(o, r, True)
                    ls.Add(o)
                Next

                Return ls

            Catch ex As Exception
                If Not dr.IsClosed Then dr.Close()
                Throw New Exception(ex.Message)
                Return Nothing
            End Try
        End Function
        Public Function LoadExtraciones_Qnl_Letras(ByRef o As extracto_qnl_letras, _
                                    ByRef dr As DataRow, _
                                    ByVal recuperarObjComponentes As Boolean) As Boolean
            Try
                o.idLoteria = dr("idLoteria")
                o.idPgmSorteo = dr("idPgmSorteo")
                o.Orden = dr("orden")
                o.letra = Es_Nulo(Of String)(dr("letra"), "")
            Catch ex As Exception
                dr = Nothing
                Throw New Exception(ex.Message)
                Return Nothing
            End Try

        End Function
    End Class
End Namespace