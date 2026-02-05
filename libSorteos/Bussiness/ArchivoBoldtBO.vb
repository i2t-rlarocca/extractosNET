Imports libEntities.Entities
Imports Sorteos.Data
Imports Sorteos.Helpers
Imports Sorteos.Extractos
Imports Sorteos.WSExtractos
Imports System.IO
Imports System.IO.Compression
Imports System.Security.Cryptography
Imports System.Xml
Imports System.Text
Imports System.Threading

Namespace Bussiness

    Public Enum fmt_extr_interJ
        fmtPropio = 1
        fmtMontev = 2
        fmtInterJ = 3
    End Enum

    Public Class ArchivoBoldtBO

        Public Sub CrearArchivoBoldt(ByVal oextr As cExtractoArchivoBoldt, ByVal _Archivo As String, ByVal path As String, ByVal JuegoLetra As String, Optional ByVal pidpgmsorteo As String = "")
            Dim ret As Boolean = True
            Dim f As StreamWriter
            Dim archivo As String
            Dim lineaReg1 As String = ""
            Dim _FechaSorteo As String = ""
            Dim _HoraSorteo As String = ""
            Dim _FechaPrescripcion As String = ""
            Dim _HoraPrescripcion As String = ""
            Dim _FechaProxSorteo As String = ""
            Dim _HoraProxSorteo As String = ""
            Dim _filler As String = "                    "
            Dim checksumcalculator = New CRC16CCITT(CRC16CCITT.InitialCRCValue.Zeroes)
            Dim crc As String = ""

            Dim RestoAutoridades As String
            RestoAutoridades = ObtenerAutoridadesArchivoBoldt(oextr.Juego, pidpgmsorteo)


            '** formatea las fecha y horas del sorteo

            FormateaFechaHora(oextr.FechaHoraSorteo, _FechaSorteo, _HoraSorteo)
            FormateaFechaHora(oextr.FechaHoraCaducidad, _FechaPrescripcion, _HoraPrescripcion)
            FormateaFechaHora(oextr.FechaHoraProximo, _FechaProxSorteo, _HoraProxSorteo)
            lineaReg1 = "01" & oextr.Juego & oextr.NumeroSorteo.ToString.PadLeft(5, "0") & _FechaSorteo & _HoraSorteo & _FechaPrescripcion & _FechaProxSorteo & _HoraProxSorteo & oextr.Localidad.PadRight(40, " ").ToUpper & oextr.Autoridad_1.cargo.PadRight(30, " ") & oextr.Autoridad_1.Nombre.PadRight(30, " ") & RestoAutoridades & _filler
            Dim value1() As Byte = System.Text.Encoding.ASCII.GetBytes(lineaReg1)
            crc = Format(checksumcalculator.ComputeCheckSum(value1), "X4")
            lineaReg1 = lineaReg1 & crc
            Try
                Dim gralDal As New GeneralDAL
                If path.Trim = "" Then
                    path = "C:"
                Else
                    If Not path.EndsWith("\") Then path = path & "\"
                End If

                FileSystemHelper.CrearPath(path)

                archivo = path & _Archivo & ".ext"

                f = New StreamWriter(archivo, False, UTF8Encoding.ASCII)
                f.WriteLine(lineaReg1)
                f.Close()
            Catch ex As Exception
                Try
                    f.Close()
                Catch ex2 As Exception
                End Try
                f = Nothing
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Public Sub AgregaLoteriaArchivoBoldt(ByVal oextr As cExtractoArchivoBoldt, ByVal Dr As DataRow, ByVal NombreArchivo As String, ByRef registros As Long)
            Dim ret As Boolean = True

            Dim f As StreamWriter
            Dim lineaReg2 As String = ""
            Dim _FechaSorteo As String = ""
            Dim _HoraSorteo As String = ""
            Dim _FechaPrescripcion As String = ""
            Dim _HoraPrescripcion As String = ""
            Dim _FechaProxSorteo As String = ""
            Dim _HoraProxSorteo As String = ""
            Dim checksumcalculator = New CRC16CCITT(CRC16CCITT.InitialCRCValue.Zeroes)
            Dim Renglon As String = ""
            Try
                Dim crc As String

                lineaReg2 = Dr("reg02")
                Dim value1() As Byte = System.Text.Encoding.ASCII.GetBytes(lineaReg2)
                crc = Format(checksumcalculator.ComputeCheckSum(value1), "X4")
                lineaReg2 = lineaReg2 & crc
                f = New StreamWriter(NombreArchivo, True, UTF8Encoding.UTF8)
                f.WriteLine(lineaReg2)
                registros += 1

                '**** registros tipo 3*********
                Select Case oextr.Juego
                    Case "V", "N", "P", "M", "U"
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_1.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_2
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_2.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_3
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_3.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_4.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_5.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_6.Valor, "06")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_7.Valor, "07")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_8.Valor, "08")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_9.Valor, "09")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_10.Valor, "10")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_11.Valor, "11")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_12.Valor, "12")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_13.Valor, "13")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_14.Valor, "14")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_15.Valor, "15")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_16.Valor, "16")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_17.Valor, "17")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_18.Valor, "18")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_19.Valor, "19")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_20.Valor, "20")
                        f.WriteLine(Renglon)
                        registros += 1
                        'se comenta lo que sigue que pertenece a loteria
                    Case "LO"
                        Dim Terminacion3Cifras As String = ""
                        Dim Terminacion2Cifras As String = ""
                        Dim Terminacion1Cifras As String = ""

                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_1.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_2
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_2.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_3
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_3.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_4
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_4.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_5
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_5.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_6
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_6.Valor, "06")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_7
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_7.Valor, "07")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_8
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_8.Valor, "08")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_9
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_9.Valor, "09")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_10
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_10.Valor, "10")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_11
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_11.Valor, "11")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_12
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_12.Valor, "12")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_13
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_13.Valor, "13")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_14
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_14.Valor, "14")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_15
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_15.Valor, "15")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_16
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_16.Valor, "16")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_17
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_17.Valor, "17")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_18
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_18.Valor, "18")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_19
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_19.Valor, "19")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_20
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_20.Valor, "20")
                        f.WriteLine(Renglon)
                        registros += 1
                        '**** terminaciones 1 premio ********
                        Terminaciones(oextr.Numero_1.Valor, Terminacion3Cifras, Terminacion2Cifras, Terminacion1Cifras)
                        '3 cifras
                        Renglon = ArmaRenglonReg03("03", "02", "3", Terminacion3Cifras, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        '2 cifras
                        Renglon = ArmaRenglonReg03("03", "02", "2", Terminacion2Cifras, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                        '1 cifras
                        Renglon = ArmaRenglonReg03("03", "02", "1", Terminacion1Cifras, "03")
                        f.WriteLine(Renglon)
                        registros += 1
                        '************ Aproximaciones de 1 premio **********
                        Dim _Numero As String = ""
                        '** anteriores 
                        _Numero = AproximacionAnterior(oextr.Numero_1.Valor, 1)
                        Renglon = ArmaRenglonReg03("03", "03", oextr.Cifras, _Numero, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        _Numero = AproximacionAnterior(oextr.Numero_1.Valor, 2)
                        Renglon = ArmaRenglonReg03("03", "03", oextr.Cifras, _Numero, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                        _Numero = AproximacionAnterior(oextr.Numero_1.Valor, 3)
                        Renglon = ArmaRenglonReg03("03", "03", oextr.Cifras, _Numero, "03")
                        f.WriteLine(Renglon)
                        registros += 1

                        _Numero = AproximacionAnterior(oextr.Numero_1.Valor, 4)
                        Renglon = ArmaRenglonReg03("03", "03", oextr.Cifras, _Numero, "04")
                        f.WriteLine(Renglon)
                        registros += 1
                        '** posteriores

                        _Numero = AproximacionPosterior(oextr.Numero_1.Valor, 1)
                        Renglon = ArmaRenglonReg03("03", "04", oextr.Cifras, _Numero, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        _Numero = AproximacionPosterior(oextr.Numero_1.Valor, 2)
                        Renglon = ArmaRenglonReg03("03", "04", oextr.Cifras, _Numero, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                        _Numero = AproximacionPosterior(oextr.Numero_1.Valor, 3)
                        Renglon = ArmaRenglonReg03("03", "04", oextr.Cifras, _Numero, "03")
                        f.WriteLine(Renglon)
                        registros += 1

                        _Numero = AproximacionPosterior(oextr.Numero_1.Valor, 4)
                        Renglon = ArmaRenglonReg03("03", "04", oextr.Cifras, _Numero, "04")
                        f.WriteLine(Renglon)
                        registros += 1

                        '****** Aproximaciones 2 Premio *****
                        '**Anteriores
                        _Numero = AproximacionAnterior(oextr.Numero_2.Valor, 1)
                        Renglon = ArmaRenglonReg03("03", "05", oextr.Cifras, _Numero, "01")
                        f.WriteLine(Renglon)
                        registros += 1
                        '** 26/09/2013 HG IAFAS no tiene 2dos premios para las 2 y 3 aproximaciones
                        If General.Jurisdiccion = "S" Then
                            _Numero = AproximacionAnterior(oextr.Numero_2.Valor, 2)
                            Renglon = ArmaRenglonReg03("03", "05", oextr.Cifras, _Numero, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If
                        '**Posteriores 

                        _Numero = AproximacionPosterior(oextr.Numero_2.Valor, 1)
                        Renglon = ArmaRenglonReg03("03", "06", oextr.Cifras, _Numero, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        If General.Jurisdiccion = "S" Then

                            _Numero = AproximacionPosterior(oextr.Numero_2.Valor, 2)
                            Renglon = ArmaRenglonReg03("03", "06", oextr.Cifras, _Numero, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If

                        '**** Aproximaciones 3 Premio ******
                        '**Anteriores
                        _Numero = AproximacionAnterior(oextr.Numero_3.Valor, 1)
                        Renglon = ArmaRenglonReg03("03", "07", oextr.Cifras, _Numero, "01")
                        f.WriteLine(Renglon)
                        registros += 1
                        If General.Jurisdiccion = "S" Then
                            _Numero = AproximacionAnterior(oextr.Numero_3.Valor, 2)
                            Renglon = ArmaRenglonReg03("03", "07", oextr.Cifras, _Numero, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If
                        '**Posteriores 
                        _Numero = AproximacionPosterior(oextr.Numero_3.Valor, 1)
                        Renglon = ArmaRenglonReg03("03", "08", oextr.Cifras, _Numero, "01")
                        f.WriteLine(Renglon)
                        registros += 1
                        If General.Jurisdiccion = "S" Then
                            _Numero = AproximacionPosterior(oextr.Numero_3.Valor, 2)
                            Renglon = ArmaRenglonReg03("03", "08", oextr.Cifras, _Numero, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If
                        '**26/092013 HG el archivo de IAFAS no tiene progresion ni extracciones
                        If General.Jurisdiccion = "S" Then
                            '****** Progresion******* 
                            _Numero = Right(oextr.Numero_21.Valor.ToString, 1)
                            Renglon = ArmaRenglonReg03("03", "09", "1", _Numero, "01")
                            f.WriteLine(Renglon)
                            registros += 1
                            '******** Extracciones*********
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_22.Valor, "01")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_23.Valor, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_24.Valor, "03")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_25.Valor, "04")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_26.Valor, "05")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_27.Valor, "06")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_28.Valor, "07")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_29.Valor, "08")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_30.Valor, "09")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "10", "3", oextr.Numero_31.Valor, "10")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If
                    Case "LC"
                        Dim Terminacion3Cifras As String = ""
                        Dim Terminacion2Cifras As String = ""
                        Dim Terminacion1Cifras As String = ""

                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_1.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_2
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_2.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_3
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_3.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_4
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_4.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_5
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_5.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1

                        '**** terminaciones 1 premio ********
                        Terminaciones(oextr.Numero_1.Valor, Terminacion3Cifras, Terminacion2Cifras, Terminacion1Cifras)
                        '3 cifras
                        Renglon = ArmaRenglonReg03("03", "02", "3", Terminacion3Cifras, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        '2 cifras
                        Renglon = ArmaRenglonReg03("03", "02", "2", Terminacion2Cifras, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                        '1 cifras
                        Renglon = ArmaRenglonReg03("03", "02", "1", Terminacion1Cifras, "03")
                        f.WriteLine(Renglon)
                        registros += 1


                        '******Terminacion 2 premio ******
                        Terminaciones(oextr.Numero_2.Valor, Terminacion3Cifras, Terminacion2Cifras, Terminacion1Cifras)
                        '3 cifras
                        Renglon = ArmaRenglonReg03("03", "11", "3", Terminacion3Cifras, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        '2 cifras
                        Renglon = ArmaRenglonReg03("03", "11", "2", Terminacion2Cifras, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                        '******Terminacion 3 premio ******
                        Terminaciones(oextr.Numero_3.Valor, Terminacion3Cifras, Terminacion2Cifras, Terminacion1Cifras)
                        '3 cifras
                        Renglon = ArmaRenglonReg03("03", "12", "3", Terminacion3Cifras, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        '2 cifras
                        Renglon = ArmaRenglonReg03("03", "12", "2", Terminacion2Cifras, "02")
                        f.WriteLine(Renglon)
                        registros += 1


                        '******Terminacion 4 premio ******
                        Terminaciones(oextr.Numero_4.Valor, Terminacion3Cifras, Terminacion2Cifras, Terminacion1Cifras)
                        '3 cifras
                        Renglon = ArmaRenglonReg03("03", "13", "3", Terminacion3Cifras, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        '2 cifras
                        Renglon = ArmaRenglonReg03("03", "13", "2", Terminacion2Cifras, "02")
                        f.WriteLine(Renglon)
                        registros += 1


                        '******Terminacion 5 premio ******
                        Terminaciones(oextr.Numero_5.Valor, Terminacion3Cifras, Terminacion2Cifras, Terminacion1Cifras)
                        '3 cifras
                        Renglon = ArmaRenglonReg03("03", "14", "3", Terminacion3Cifras, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        '2 cifras
                        Renglon = ArmaRenglonReg03("03", "14", "2", Terminacion2Cifras, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                    Case "TM", "PF"
                        ' renglon de numero_1
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_1.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_2
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_2.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_3
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_3.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_4
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_4.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_5
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_5.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_6
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_6.Valor, "06")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_7
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_7.Valor, "07")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_8
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_8.Valor, "08")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_9
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_9.Valor, "09")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_10
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_10.Valor, "10")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_11
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_11.Valor, "11")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_12
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_12.Valor, "12")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_13
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_13.Valor, "13")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_14
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_14.Valor, "14")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_15
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_15.Valor, "15")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_16
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_16.Valor, "16")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_17
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_17.Valor, "17")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_18
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_18.Valor, "18")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_19
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_19.Valor, "19")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_20
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_20.Valor, "20")
                        f.WriteLine(Renglon)
                        registros += 1

                    Case "Q2"
                        ' sorteo tradicional
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_1.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_2.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_3.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_4.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_5.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_6.Valor, "06")
                        f.WriteLine(Renglon)
                        registros += 1

                        '*** la segunda
                        Renglon = ArmaRenglonReg03("03", "15", oextr.Cifras, oextr.Numero_7.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_8
                        Renglon = ArmaRenglonReg03("03", "15", oextr.Cifras, oextr.Numero_8.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_9
                        Renglon = ArmaRenglonReg03("03", "15", oextr.Cifras, oextr.Numero_9.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1

                        ' renglon de numero_10
                        Renglon = ArmaRenglonReg03("03", "15", oextr.Cifras, oextr.Numero_10.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_11
                        Renglon = ArmaRenglonReg03("03", "15", oextr.Cifras, oextr.Numero_11.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1
                        ' renglon de numero_12
                        Renglon = ArmaRenglonReg03("03", "15", oextr.Cifras, oextr.Numero_12.Valor, "06")
                        f.WriteLine(Renglon)
                        registros += 1

                        '*** la revancha
                        Renglon = ArmaRenglonReg03("03", "16", oextr.Cifras, oextr.Numero_13.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1
                        Renglon = ArmaRenglonReg03("03", "16", oextr.Cifras, oextr.Numero_14.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1
                        Renglon = ArmaRenglonReg03("03", "16", oextr.Cifras, oextr.Numero_15.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1
                        Renglon = ArmaRenglonReg03("03", "16", oextr.Cifras, oextr.Numero_16.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1
                        Renglon = ArmaRenglonReg03("03", "16", oextr.Cifras, oextr.Numero_17.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1
                        Renglon = ArmaRenglonReg03("03", "16", oextr.Cifras, oextr.Numero_18.Valor, "06")
                        f.WriteLine(Renglon)
                        registros += 1

                        '*** Siempre Sale
                        Renglon = ArmaRenglonReg03("03", "17", oextr.Cifras, oextr.Numero_19.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "17", oextr.Cifras, oextr.Numero_20.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1
                        Renglon = ArmaRenglonReg03("03", "17", oextr.Cifras, oextr.Numero_21.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "17", oextr.Cifras, oextr.Numero_22.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "17", oextr.Cifras, oextr.Numero_23.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "17", oextr.Cifras, oextr.Numero_24.Valor, "06")
                        f.WriteLine(Renglon)
                        registros += 1

                        '*** Adicional
                        If oextr.Numero_25.Valor <> 0 Or oextr.Numero_26.Valor <> 0 Or oextr.Numero_27.Valor <> 0 Or oextr.Numero_28.Valor <> 0 Or oextr.Numero_29.Valor <> 0 Or oextr.Numero_30.Valor <> 0 Then
                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_25.Valor, "01")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_26.Valor, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_27.Valor, "03")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_28.Valor, "04")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_29.Valor, "05")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_30.Valor, "06")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If
                        '*** Adicional
                        If oextr.Numero_31.Valor <> 0 Or oextr.Numero_32.Valor <> 0 Or oextr.Numero_33.Valor <> 0 Or oextr.Numero_34.Valor <> 0 Or oextr.Numero_35.Valor <> 0 Or oextr.Numero_36.Valor <> 0 Then
                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_31.Valor, "01")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_32.Valor, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_33.Valor, "03")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_34.Valor, "04")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_35.Valor, "05")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_36.Valor, "06")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If


                    Case "BR"
                        ' sorteo tradicional
                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_1.Valor, "01")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_2.Valor, "02")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_3.Valor, "03")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_4.Valor, "04")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "01", oextr.Cifras, oextr.Numero_5.Valor, "05")
                        f.WriteLine(Renglon)
                        registros += 1

                        Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_6.Valor, "06")
                        f.WriteLine(Renglon)
                        registros += 1


                        '*** Adicional
                        If oextr.Numero_25.Valor <> 0 Or oextr.Numero_26.Valor <> 0 Or oextr.Numero_27.Valor <> 0 Or oextr.Numero_28.Valor <> 0 Or oextr.Numero_29.Valor <> 0 Or oextr.Numero_30.Valor <> 0 Then
                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_25.Valor, "01")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_26.Valor, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_27.Valor, "03")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_28.Valor, "04")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_29.Valor, "05")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_30.Valor, "06")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If
                        '*** Adicional
                        If oextr.Numero_31.Valor <> 0 Or oextr.Numero_32.Valor <> 0 Or oextr.Numero_33.Valor <> 0 Or oextr.Numero_34.Valor <> 0 Or oextr.Numero_35.Valor <> 0 Or oextr.Numero_36.Valor <> 0 Then
                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_31.Valor, "01")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_32.Valor, "02")
                            f.WriteLine(Renglon)
                            registros += 1
                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_33.Valor, "03")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_34.Valor, "04")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_35.Valor, "05")
                            f.WriteLine(Renglon)
                            registros += 1

                            Renglon = ArmaRenglonReg03("03", "18", oextr.Cifras, oextr.Numero_36.Valor, "06")
                            f.WriteLine(Renglon)
                            registros += 1
                        End If
                End Select
                f.Close()
            Catch ex As Exception
                Try
                    f.Close()
                Catch ex2 As Exception
                End Try
                f = Nothing
                Throw New Exception("AgregaLoteriaArchivoBoldt:" & ex.Message)
            End Try
        End Sub

        Public Sub FormateaFechaHora(ByVal fechahora As Date, ByRef Fecha As String, ByRef Hora As String)
            Try
                Dim _fechaAux As String
                Dim _horaAux As String

                _fechaAux = fechahora.ToShortDateString
                Fecha = Mid(_fechaAux, 7, 4) & Mid(_fechaAux, 4, 2) & Mid(_fechaAux, 1, 2)
                '** 26/09/2012 se formatea con 24 hs
                _horaAux = fechahora.ToString("HH:mm:ss")
                Hora = Mid(_horaAux, 1, 2) & Mid(_horaAux, 4, 2)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Function ArmaRenglonReg03(ByVal tipoReg As String, ByVal TipoPremio As String, ByVal Cant_Cifras As String, ByVal Valor As String, ByVal Ubicacion As String) As String
            Try
                Dim _Registro As String
                Dim _filler As String = "                    "
                Dim _crc As String
                Dim checksumcalculator = New CRC16CCITT(CRC16CCITT.InitialCRCValue.Zeroes)
                _Registro = tipoReg.PadLeft(2, "0") & TipoPremio.Trim.PadLeft(2, "0") & Cant_Cifras & Valor.Trim.PadLeft(9, "0") & Ubicacion.Trim.PadLeft(2, "0")
                _Registro = _Registro & _filler

                Dim value1() As Byte = System.Text.Encoding.ASCII.GetBytes(_Registro)
                _crc = Format(checksumcalculator.ComputeCheckSum(value1), "X4")
                _Registro = _Registro & _crc
                Return _Registro
            Catch ex As Exception
                Throw New Exception("ArmaRenglonReg03: " & ex.Message)
            End Try
        End Function

        Private Function ObtenerMD5(ByVal fichero As String) As String
            Dim cadenaMD5 As String = ""
            Dim cadenaFichero As FileStream
            Dim bytesFichero As [Byte]()
            Dim MD5Crypto As New MD5CryptoServiceProvider
            cadenaFichero = File.Open(fichero, FileMode.Open, FileAccess.Read)
            bytesFichero = MD5Crypto.ComputeHash(cadenaFichero)
            cadenaFichero.Close()
            cadenaMD5 = BitConverter.ToString(bytesFichero)
            cadenaMD5 = cadenaMD5.Replace("-", "")
            Return cadenaMD5
        End Function

        Private Function GeneraArchivodeControl(ByVal _archivo As String, ByVal _NombreArchivo As String, ByVal CodigoJuego As String, ByVal Provincia As String, ByVal NroSorteo As Long, ByVal CantReg As Long, ByVal md5 As String) As Boolean
            Try
                Dim doc As XmlTextWriter
                doc = New XmlTextWriter(_archivo, UTF8Encoding.ASCII)
                doc.Formatting = Formatting.Indented
                doc.WriteStartDocument()
                doc.WriteStartElement("Datos")
                doc.WriteStartElement("Registro")
                doc.WriteStartElement("tipo_archivo")
                doc.WriteValue("Extracto")
                doc.WriteEndElement()

                doc.WriteStartElement("version")
                doc.WriteValue("201208")
                doc.WriteEndElement()
                doc.WriteStartElement("nombre_archivo")
                doc.WriteValue(_NombreArchivo & ".ext")
                doc.WriteEndElement()
                doc.WriteStartElement("provincia")
                doc.WriteValue(General.NroPciaArchivosBoldt)
                doc.WriteEndElement()
                doc.WriteStartElement("juego")
                doc.WriteValue(CodigoJuego)
                doc.WriteEndElement()
                doc.WriteStartElement("sorteo")
                doc.WriteValue(NroSorteo)
                doc.WriteEndElement()
                doc.WriteStartElement("cantidad_registros")
                doc.WriteValue(CantReg)
                doc.WriteEndElement()
                doc.WriteStartElement("md5")
                doc.WriteValue(md5)
                doc.WriteEndElement()

                doc.WriteEndElement()
                doc.WriteEndElement()

                doc.WriteEndDocument()
                doc.Close()

            Catch ex As Exception
                Throw New Exception("GeneraArchivodeControl: " & ex.Message)
            End Try


        End Function

        Private Function Obtener_Archivos(ByVal pDirectorio As String, Optional ByVal _filtro As String = "") As String()
            Dim archivos As String()

            Try
                If _filtro.Trim <> "" Then
                    archivos = Directory.GetFiles(pDirectorio, _filtro)
                Else
                    archivos = Directory.GetFiles(pDirectorio)
                End If
                Return archivos
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Private Sub Borrar_Archivos(ByVal pArchivos As String())
            Dim _archivo As String = ""
            Try

                'recorre las lista de archivos de la carpeta y los borra
                For Each _archivo In pArchivos
                    FileSystemHelper.BorrarArchivo(_archivo)
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Sub

        Private Function AproximacionAnterior(ByVal Nroextracto As Long, ByVal PosAproximacion As Integer) As String
            Try
                Dim Resultado As String = ""
                Select Case PosAproximacion
                    Case 1 ' 1 aproximacion
                        Select Case Nroextracto
                            Case 0
                                Resultado = 59999
                            Case Else
                                Resultado = Nroextracto - 1
                        End Select

                    Case 2 ' 2 aproximacion
                        Select Case Nroextracto
                            Case 0
                                Resultado = 59998
                            Case 1
                                Resultado = 59999
                            Case Else
                                Resultado = Nroextracto - 2
                        End Select

                    Case 3 ' 3 aproximacion
                        Select Case Nroextracto
                            Case 0
                                Resultado = 59997
                            Case 1
                                Resultado = 59998
                            Case 2
                                Resultado = 59999
                            Case Else
                                Resultado = Nroextracto - 3
                        End Select

                    Case 4 ' 4 aproximacion
                        Select Case Nroextracto
                            Case 0
                                Resultado = 59996
                            Case 1
                                Resultado = 59997
                            Case 2
                                Resultado = 59998
                            Case 3
                                Resultado = 59999
                            Case Else
                                Resultado = Nroextracto - 4
                        End Select
                End Select
                Resultado.PadLeft(9, "0")
                Return Resultado

            Catch ex As Exception
                Throw New Exception("AproximacionAnterior:" & ex.Message)
            End Try

        End Function

        Private Function AproximacionPosterior(ByVal Nroextracto As Long, ByVal PosAproximacion As Integer) As String
            Try
                Dim Resultado As String = ""
                Select Case PosAproximacion
                    Case 1 ' 1 aproximacion
                        Select Case Nroextracto
                            Case 59999
                                Resultado = 0
                            Case Else
                                Resultado = Nroextracto + 1
                        End Select

                    Case 2 ' 2 aproximacion
                        Select Case Nroextracto
                            Case 59998
                                Resultado = 0
                            Case 59999
                                Resultado = 1
                            Case Else
                                Resultado = Nroextracto + 2
                        End Select

                    Case 3 ' 3 aproximacion
                        Select Case Nroextracto
                            Case 59997
                                Resultado = 0
                            Case 59998
                                Resultado = 1
                            Case 59999
                                Resultado = 2
                            Case Else
                                Resultado = Nroextracto + 3
                        End Select

                    Case 4 ' 4 aproximacion
                        Select Case Nroextracto
                            Case 59996
                                Resultado = 0
                            Case 59997
                                Resultado = 1
                            Case 59998
                                Resultado = 2
                            Case 59999
                                Resultado = 3
                            Case Else
                                Resultado = Nroextracto + 4
                        End Select
                End Select
                Resultado.PadLeft(9, "0")
                Return Resultado

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Private Function Terminaciones(ByVal Nroextracto As Long, ByRef _3cifras As String, ByRef _2cifras As String, ByRef _1cifras As String)
            Try
                Dim Nro As String
                Nro = Nroextracto.ToString
                If Len(Nro) >= 3 Then
                    _3cifras = Right(Nro, 3)
                    _2cifras = Right(Nro, 2)
                    _1cifras = Right(Nro, 1)
                End If

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function GenerarExtractodesdeArchivo(ByVal idjuego As Integer, ByVal nrosorteo As Long, Optional ByRef PathArchivoExtracto As String = "", Optional ByRef PathOri As String = "", Optional ByRef PathDest As String = "", Optional ByRef bandera As Integer = 0, Optional ByRef archivoCopiado As String = "") As cExtractoArchivoBoldt
            Dim _archivo As String = ""
            Dim extr As New cExtractoArchivoBoldt
            Dim boSorteo As New PgmSorteoBO
            Dim oDalBoldt As New ArchivoBoldtDAL
            Dim oDal As New PgmSorteoDAL
            Dim _fechaSorteo As String = ""
            Dim _HoraSorteo As String = ""
            Dim f As StreamReader
            Dim pathDestinoArchivoExtracto As String = General.CarpetaDestinoArchivosExtractoBoldt
            Dim pathOrigenArchivoExtracto As String = General.CarpetaOrigenArchivosExtractoBoldt
            Dim nombreArchivo As String = ""
            Dim ArchivoDestino As String = ""
            Dim Archivocontrol As String = ""
            Dim linea As String = ""
            Dim tiporegistro As String = ""
            Dim _posicion As Integer = 0
            Dim _valor As Long = 0
            Dim _valorSTR As String = ""
            Dim _tipoPremio As Integer = 0
            Dim prefijo As String = ""
            Dim _nrosorteo As String = ""
            Dim archivoOrigen As String = ""
            Try
                If PathOri.Trim.Length > 0 Then
                    pathOrigenArchivoExtracto = PathOri
                End If
                If PathDest.Trim.Length > 0 Then
                    pathDestinoArchivoExtracto = PathDest
                End If
                If Not pathOrigenArchivoExtracto.EndsWith("\") Then
                    pathOrigenArchivoExtracto = pathOrigenArchivoExtracto & "\"
                End If
                If Not pathDestinoArchivoExtracto.EndsWith("\") Then
                    pathDestinoArchivoExtracto = pathDestinoArchivoExtracto & "\"
                End If
                PathOri = pathOrigenArchivoExtracto
                PathDest = pathDestinoArchivoExtracto
                ''crea el nombre del archivo
                nombreArchivo = CreaNombreArchivoExtracto(idjuego, nrosorteo)

                If archivoCopiado.Trim.Length > 0 Then
                    archivoOrigen = archivoCopiado

                    nombreArchivo = archivoCopiado.Substring(archivoCopiado.LastIndexOf("\") + 1, archivoCopiado.Length - archivoCopiado.LastIndexOf("\") - 5)
                    archivoCopiado = ""
                Else
                    archivoOrigen = pathOrigenArchivoExtracto & nombreArchivo & ".zip"
                End If


                If Not File.Exists(archivoOrigen) Then
                    'MsgBox("No se encontró el archivo origen:" & archivoOrigen, MsgBoxStyle.Information)
                    bandera = 1
                    Return Nothing
                End If

                PathArchivoExtracto = archivoOrigen

                '** crea la carpeta si no existe
                FileSystemHelper.CrearPath(pathDestinoArchivoExtracto)
                '** descomprime los archivos
                FileSystemHelper.Descomprimir(pathDestinoArchivoExtracto, archivoOrigen)


                ArchivoDestino = pathDestinoArchivoExtracto & nombreArchivo & ".ext"
                Archivocontrol = pathDestinoArchivoExtracto & nombreArchivo & ".cxt"

                '** control del archivo contra el archivo de control md5
                If General.CtrMD5Premios = "S" Then
                    If Not FileSystemHelper.ControlArchivoMd5(ArchivoDestino, Archivocontrol) Then
                        MsgBox("El archivo " & nombreArchivo & ".ext no coincide con el archivo de control.No se puede generar el extracto.", MsgBoxStyle.Exclamation)
                        'borra archivos .ext y .cxt
                        FileSystemHelper.BorrarArchivo(ArchivoDestino)
                        FileSystemHelper.BorrarArchivo(Archivocontrol)
                        Return Nothing
                    End If
                End If

                extr.Autoridad_1 = New libEntities.Entities.Autoridad
                extr.Autoridad_2 = New libEntities.Entities.Autoridad
                extr.Autoridad_3 = New libEntities.Entities.Autoridad
                extr.Autoridad_4 = New libEntities.Entities.Autoridad
                extr.Autoridad_5 = New libEntities.Entities.Autoridad

                extr.Numero_1 = New cValorPosicion
                extr.Numero_2 = New cValorPosicion
                extr.Numero_3 = New cValorPosicion
                extr.Numero_4 = New cValorPosicion
                extr.Numero_5 = New cValorPosicion
                extr.Numero_6 = New cValorPosicion
                extr.Numero_7 = New cValorPosicion
                extr.Numero_8 = New cValorPosicion
                extr.Numero_9 = New cValorPosicion
                extr.Numero_10 = New cValorPosicion
                extr.Numero_11 = New cValorPosicion
                extr.Numero_12 = New cValorPosicion
                extr.Numero_13 = New cValorPosicion
                extr.Numero_14 = New cValorPosicion
                extr.Numero_15 = New cValorPosicion
                extr.Numero_16 = New cValorPosicion
                extr.Numero_17 = New cValorPosicion
                extr.Numero_18 = New cValorPosicion
                extr.Numero_19 = New cValorPosicion
                extr.Numero_20 = New cValorPosicion
                extr.Numero_21 = New cValorPosicion
                extr.Numero_22 = New cValorPosicion
                extr.Numero_23 = New cValorPosicion
                extr.Numero_24 = New cValorPosicion
                extr.Numero_25 = New cValorPosicion
                extr.Numero_26 = New cValorPosicion
                extr.Numero_27 = New cValorPosicion
                extr.Numero_28 = New cValorPosicion
                extr.Numero_29 = New cValorPosicion
                extr.Numero_30 = New cValorPosicion
                extr.Numero_31 = New cValorPosicion
                extr.Numero_32 = New cValorPosicion
                extr.Numero_33 = New cValorPosicion
                extr.Numero_34 = New cValorPosicion
                extr.Numero_35 = New cValorPosicion
                extr.Numero_36 = New cValorPosicion

                'comienza a leer el archivo 
                f = New StreamReader(ArchivoDestino)

                While Not f.EndOfStream
                    linea = f.ReadLine()
                    tiporegistro = linea.Substring(0, 2)
                    'tipo de registro
                    '01-cabecera
                    '02-datos de loteria
                    '03-detalle de los numeros
                    Select Case tiporegistro
                        Case "01"
                            extr.idJuego = Mid(linea, 3, 3)
                            extr.NumeroSorteo = Mid(linea, 6, 5)
                            extr.FechaHoraSorteo = FormateaFechaHoraaExtracto(Mid(linea, 11, 8), Mid(linea, 19, 4))
                            extr.FechaHoraProximo = FormateaFechaHoraaExtracto(Mid(linea, 11, 8), Mid(linea, 39, 4))
                            extr.FechaHoraCaducidad = FormateaFechaHoraaExtracto(Mid(linea, 11, 8), Mid(linea, 19, 4))
                            extr.Localidad = Mid(linea, 43, 40)
                            extr.Autoridad_1.cargo = Mid(linea, 83, 30)
                            extr.Autoridad_1.Nombre = Mid(linea, 113, 30)
                            extr.Autoridad_2.cargo = Mid(linea, 143, 30)
                            extr.Autoridad_2.Nombre = Mid(linea, 173, 30)
                            extr.Autoridad_3.cargo = Mid(linea, 203, 30)
                            extr.Autoridad_3.Nombre = Mid(linea, 233, 30)
                            extr.Autoridad_4.cargo = Mid(linea, 263, 30)
                            extr.Autoridad_4.Nombre = Mid(linea, 293, 30)
                            extr.Autoridad_5.cargo = Mid(linea, 323, 30)
                            extr.Autoridad_5.Nombre = Mid(linea, 353, 30)

                            Select Case extr.idJuego
                                Case 4, 13
                                    extr.Cifras = 2
                                Case 30
                                    extr.Cifras = 2
                                Case 3, 8, 49
                                    extr.Cifras = 4
                                Case 2, 50
                                    extr.Cifras = 5
                                Case Else
                                    extr.Cifras = 0
                            End Select

                        Case "02"
                            extr.Loteria = oDalBoldt.ObtenerCodigoLoteriaExtracto(Mid(linea, 5, 2))
                            extr.HoraIniLoteria = Mid(linea, 15, 4)
                            extr.HoraFinLoteria = Mid(linea, 19, 4)
                        Case "03"
                            _valor = Mid(linea, 6, 9)
                            extr.Cifras = Mid(linea, 5, 1)
                            _valorSTR = Right(Trim(Mid(linea, 6, 9)), extr.Cifras)
                            _posicion = Mid(linea, 15, 2)
                            Select Case extr.idJuego
                                Case 4, 13
                                    _tipoPremio = Mid(linea, 3, 2)
                                    Select Case _tipoPremio
                                        Case 1 'tradicional
                                            Select Case _posicion
                                                Case 1
                                                    extr.Numero_1.Posicion = 1
                                                    extr.Numero_1.Valor = _valor
                                                    extr.Numero_1.ValorSTR = _valorSTR
                                                Case 2
                                                    extr.Numero_2.Posicion = 2
                                                    extr.Numero_2.Valor = _valor
                                                    extr.Numero_2.ValorSTR = _valorSTR
                                                Case 3
                                                    extr.Numero_3.Posicion = 3
                                                    extr.Numero_3.Valor = _valor
                                                    extr.Numero_3.ValorSTR = _valorSTR
                                                Case 4
                                                    extr.Numero_4.Posicion = 4
                                                    extr.Numero_4.Valor = _valor
                                                    extr.Numero_4.ValorSTR = _valorSTR
                                                Case 5
                                                    extr.Numero_5.Posicion = 5
                                                    extr.Numero_5.Valor = _valor
                                                    extr.Numero_5.ValorSTR = _valorSTR
                                                Case 6
                                                    extr.Numero_6.Posicion = 6
                                                    extr.Numero_6.Valor = _valor
                                                    extr.Numero_6.ValorSTR = _valorSTR

                                            End Select
                                        Case 15 'la segunda
                                            Select Case _posicion
                                                Case 1
                                                    extr.Numero_7.Posicion = 7
                                                    extr.Numero_7.Valor = _valor
                                                    extr.Numero_7.ValorSTR = _valorSTR
                                                Case 2
                                                    extr.Numero_8.Posicion = 8
                                                    extr.Numero_8.Valor = _valor
                                                    extr.Numero_8.ValorSTR = _valorSTR
                                                Case 3
                                                    extr.Numero_9.Posicion = 9
                                                    extr.Numero_9.Valor = _valor
                                                    extr.Numero_9.ValorSTR = _valorSTR
                                                Case 4
                                                    extr.Numero_10.Posicion = 10
                                                    extr.Numero_10.Valor = _valor
                                                    extr.Numero_10.ValorSTR = _valorSTR
                                                Case 5
                                                    extr.Numero_11.Posicion = 11
                                                    extr.Numero_11.Valor = _valor
                                                    extr.Numero_11.ValorSTR = _valorSTR
                                                Case 6
                                                    extr.Numero_12.Posicion = 12
                                                    extr.Numero_12.Valor = _valor
                                                    extr.Numero_12.ValorSTR = _valorSTR
                                            End Select
                                        Case 16 'revancha
                                            Select Case _posicion
                                                Case 1
                                                    extr.Numero_13.Posicion = 13
                                                    extr.Numero_13.Valor = _valor
                                                    extr.Numero_13.ValorSTR = _valorSTR
                                                Case 2
                                                    extr.Numero_14.Posicion = 14
                                                    extr.Numero_14.Valor = _valor
                                                    extr.Numero_14.ValorSTR = _valorSTR
                                                Case 3
                                                    extr.Numero_15.Posicion = 15
                                                    extr.Numero_15.Valor = _valor
                                                    extr.Numero_15.ValorSTR = _valorSTR
                                                Case 4
                                                    extr.Numero_16.Posicion = 16
                                                    extr.Numero_16.Valor = _valor
                                                    extr.Numero_16.ValorSTR = _valorSTR
                                                Case 5
                                                    extr.Numero_17.Posicion = 17
                                                    extr.Numero_17.Valor = _valor
                                                    extr.Numero_17.ValorSTR = _valorSTR
                                                Case 6
                                                    extr.Numero_18.Posicion = 18
                                                    extr.Numero_18.Valor = _valor
                                                    extr.Numero_18.ValorSTR = _valorSTR

                                            End Select
                                        Case 17 ' siempre sale
                                            Select Case _posicion
                                                Case 1
                                                    extr.Numero_19.Posicion = 19
                                                    extr.Numero_19.Valor = _valor
                                                    extr.Numero_19.ValorSTR = _valorSTR
                                                Case 2
                                                    extr.Numero_20.Posicion = 20
                                                    extr.Numero_20.Valor = _valor
                                                    extr.Numero_20.ValorSTR = _valorSTR
                                                Case 3
                                                    extr.Numero_21.Posicion = 21
                                                    extr.Numero_21.Valor = _valor
                                                    extr.Numero_21.ValorSTR = _valorSTR
                                                Case 4
                                                    extr.Numero_22.Posicion = 22
                                                    extr.Numero_22.Valor = _valor
                                                    extr.Numero_22.ValorSTR = _valorSTR
                                                Case 5
                                                    extr.Numero_23.Posicion = 23
                                                    extr.Numero_23.Valor = _valor
                                                    extr.Numero_23.ValorSTR = _valorSTR
                                                Case 6
                                                    extr.Numero_24.Posicion = 24
                                                    extr.Numero_24.Valor = _valor
                                                    extr.Numero_24.ValorSTR = _valorSTR
                                            End Select
                                        Case 18 'adicional brinco,quini puede ser dos adicionales
                                            Select Case _posicion
                                                Case 1
                                                    If extr.Numero_25.Posicion = 0 Then 'es el primer adicional
                                                        extr.Numero_25.Posicion = 25
                                                        extr.Numero_25.Valor = _valor
                                                        extr.Numero_25.ValorSTR = _valorSTR
                                                    Else
                                                        extr.Numero_31.Posicion = 31
                                                        extr.Numero_31.Valor = _valor
                                                        extr.Numero_31.ValorSTR = _valorSTR
                                                    End If

                                                Case 2
                                                    If extr.Numero_26.Posicion = 0 Then 'es el primer adicional
                                                        extr.Numero_26.Posicion = 26
                                                        extr.Numero_26.Valor = _valor
                                                        extr.Numero_26.ValorSTR = _valorSTR
                                                    Else
                                                        extr.Numero_32.Posicion = 32
                                                        extr.Numero_32.Valor = _valor
                                                        extr.Numero_32.ValorSTR = _valorSTR
                                                    End If
                                                Case 3
                                                    If extr.Numero_27.Posicion = 0 Then 'es el primer adicional
                                                        extr.Numero_27.Posicion = 27
                                                        extr.Numero_27.Valor = _valor
                                                        extr.Numero_27.ValorSTR = _valorSTR
                                                    Else
                                                        extr.Numero_33.Posicion = 33
                                                        extr.Numero_33.Valor = _valor
                                                        extr.Numero_33.ValorSTR = _valorSTR
                                                    End If
                                                Case 4
                                                    If extr.Numero_28.Posicion = 0 Then 'es el primer adicional
                                                        extr.Numero_28.Posicion = 28
                                                        extr.Numero_28.Valor = _valor
                                                        extr.Numero_28.ValorSTR = _valorSTR
                                                    Else
                                                        extr.Numero_34.Posicion = 34
                                                        extr.Numero_34.Valor = _valor
                                                        extr.Numero_34.ValorSTR = _valorSTR
                                                    End If
                                                Case 5
                                                    If extr.Numero_29.Posicion = 0 Then 'es el primer adicional
                                                        extr.Numero_29.Posicion = 29
                                                        extr.Numero_29.Valor = _valor
                                                        extr.Numero_29.ValorSTR = _valorSTR
                                                    Else
                                                        extr.Numero_35.Posicion = 35
                                                        extr.Numero_35.Valor = _valor
                                                        extr.Numero_35.ValorSTR = _valorSTR
                                                    End If
                                                Case 6
                                                    If extr.Numero_30.Posicion = 0 Then 'es el primer adicional
                                                        extr.Numero_30.Posicion = 30
                                                        extr.Numero_30.Valor = _valor
                                                        extr.Numero_30.ValorSTR = _valorSTR
                                                    Else
                                                        extr.Numero_36.Posicion = 36
                                                        extr.Numero_36.Valor = _valor
                                                        extr.Numero_36.ValorSTR = _valorSTR
                                                    End If
                                            End Select
                                    End Select

                                Case 2, 3, 8, 49, 30, 50 'son todas extracciones principales por lo que no se controla el tipod e premio
                                    Select Case _posicion
                                        Case 1
                                            extr.Numero_1.Posicion = 1
                                            extr.Numero_1.Valor = _valor
                                            extr.Numero_1.ValorSTR = _valorSTR
                                        Case 2
                                            extr.Numero_2.Posicion = 2
                                            extr.Numero_2.Valor = _valor
                                            extr.Numero_2.ValorSTR = _valorSTR
                                        Case 3
                                            extr.Numero_3.Posicion = 3
                                            extr.Numero_3.Valor = _valor
                                            extr.Numero_3.ValorSTR = _valorSTR
                                        Case 4
                                            extr.Numero_4.Posicion = 4
                                            extr.Numero_4.Valor = _valor
                                            extr.Numero_4.ValorSTR = _valorSTR
                                        Case 5
                                            extr.Numero_5.Posicion = 5
                                            extr.Numero_5.Valor = _valor
                                            extr.Numero_5.ValorSTR = _valorSTR
                                        Case 6
                                            extr.Numero_6.Posicion = 6
                                            extr.Numero_6.Valor = _valor
                                            extr.Numero_6.ValorSTR = _valorSTR
                                        Case 7
                                            extr.Numero_7.Posicion = 7
                                            extr.Numero_7.Valor = _valor
                                            extr.Numero_7.ValorSTR = _valorSTR
                                        Case 8
                                            extr.Numero_8.Posicion = 8
                                            extr.Numero_8.Valor = _valor
                                            extr.Numero_8.ValorSTR = _valorSTR
                                        Case 9
                                            extr.Numero_9.Posicion = 9
                                            extr.Numero_9.Valor = _valor
                                            extr.Numero_9.ValorSTR = _valorSTR
                                        Case 10
                                            extr.Numero_10.Posicion = 10
                                            extr.Numero_10.Valor = _valor
                                            extr.Numero_10.ValorSTR = _valorSTR
                                        Case 11
                                            extr.Numero_11.Posicion = 11
                                            extr.Numero_11.Valor = _valor
                                            extr.Numero_11.ValorSTR = _valorSTR
                                        Case 12
                                            extr.Numero_12.Posicion = 12
                                            extr.Numero_12.Valor = _valor
                                            extr.Numero_12.ValorSTR = _valorSTR
                                        Case 13
                                            extr.Numero_13.Posicion = 13
                                            extr.Numero_13.Valor = _valor
                                            extr.Numero_13.ValorSTR = _valorSTR
                                        Case 14
                                            extr.Numero_14.Posicion = 14
                                            extr.Numero_14.Valor = _valor
                                            extr.Numero_14.ValorSTR = _valorSTR
                                        Case 15
                                            extr.Numero_15.Posicion = 15
                                            extr.Numero_15.Valor = _valor
                                            extr.Numero_15.ValorSTR = _valorSTR
                                        Case 16
                                            extr.Numero_16.Posicion = 16
                                            extr.Numero_16.Valor = _valor
                                            extr.Numero_16.ValorSTR = _valorSTR
                                        Case 17
                                            extr.Numero_17.Posicion = 17
                                            extr.Numero_17.Valor = _valor
                                            extr.Numero_17.ValorSTR = _valorSTR
                                        Case 18
                                            extr.Numero_18.Posicion = 18
                                            extr.Numero_18.Valor = _valor
                                            extr.Numero_18.ValorSTR = _valorSTR
                                        Case 19
                                            extr.Numero_19.Posicion = 19
                                            extr.Numero_19.Valor = _valor
                                            extr.Numero_19.ValorSTR = _valorSTR
                                        Case 20
                                            extr.Numero_20.Posicion = 20
                                            extr.Numero_20.Valor = _valor
                                            extr.Numero_20.ValorSTR = _valorSTR
                                    End Select
                            End Select
                    End Select

                End While
                f.Dispose()
                Return extr
            Catch ex As Exception
                Throw New Exception(" GenerarArchivoExtracto:" & ex.Message)
            End Try
        End Function

        Public Function GenerarExtractodesdeArchivoInterJ(ByVal idJuego As Integer, ByVal idpgmsorteo As Long, ByRef oLoteria As Loteria, Optional ByRef PathArchivoExtracto As String = "", Optional ByRef PathOri As String = "", Optional ByRef PathDest As String = "", Optional ByRef bandera As Integer = 0, Optional ByRef archivoCopiado As String = "") As cExtractoArchivoBoldt
            Dim _archivo As String = ""
            Dim extr As New cExtractoArchivoBoldt
            Dim boSorteo As New PgmSorteoBO
            Dim oDalBoldt As New ArchivoBoldtDAL
            Dim oDal As New PgmSorteoDAL
            Dim f As StreamReader
            Dim pathDestinoArchivoExtracto As String = General.CarpetaDestinoArchivosExtractoBoldt
            Dim pathOrigenArchivoExtracto As String = General.CarpetaOrigenArchivosExtractoBoldt
            Dim nombreArchivo As String = ""
            Dim ArchivoDestino As String = ""
            Dim Archivocontrol As String = ""
            Dim linea As String = ""
            Dim tiporegistro As String = ""
            Dim _posicion As Integer = 0
            Dim _valor As Long = 0
            Dim _valorSTR As String = ""
            Dim _tipoPremio As Integer = 0
            Dim prefijo As String = ""
            Dim _nrosorteo As String = ""
            Dim archivoOrigen As String = ""

            Dim _fechaSorteo As String = ""
            Dim _horaSorteo As String = ""
            Dim _fechaProxSorteo As String = ""
            Dim _horaProxSorteo As String = ""
            Dim _fechaPresc As String = ""
            Dim _horaPresc As String = ""
            Dim arrFec() As String
            Dim msgret As String = ""
            Dim pos_ini As Integer = 0
            Dim ls As New ListaOrdenada(Of extracto_qnl_letras)
            Dim letras As String = ""
            Dim arrLetras(3) As String
            Dim oLetras = New extracto_qnl_letras
            Dim _fechaVacia As Date = Date.Now

            Try
                If PathOri.Trim.Length > 0 Then
                    pathOrigenArchivoExtracto = PathOri
                End If
                If PathDest.Trim.Length > 0 Then
                    pathDestinoArchivoExtracto = PathDest
                End If
                If Not pathOrigenArchivoExtracto.EndsWith("\") Then
                    pathOrigenArchivoExtracto = pathOrigenArchivoExtracto & "\"
                End If
                If Not pathDestinoArchivoExtracto.EndsWith("\") Then
                    pathDestinoArchivoExtracto = pathDestinoArchivoExtracto & "\"
                End If
                PathOri = pathOrigenArchivoExtracto
                PathDest = pathDestinoArchivoExtracto
                ''crea el nombre del archivo
                ''nombreArchivo = CreaNombreArchivoExtracto(idjuego, nrosorteo)
                If Not ObtenerNombreArchExtractoInterJ(idpgmsorteo, oLoteria.IdLoteria, nombreArchivo, msgRet) Then
                    FileSystemHelper.Log("GenerarExtractodesdeArchivoInterJ: no se puede obtener el nombre del archivo de extracto interjurisdiccional para:" & idpgmsorteo & ". Msj: " & msgret)
                    Throw New Exception("No se puede obtener el nombre del archivo de extracto interjurisdiccional para:" & idpgmsorteo & ". Msj: " & msgret)
                    Return Nothing
                End If

                If archivoCopiado.Trim.Length > 0 Then
                    archivoOrigen = archivoCopiado

                    nombreArchivo = archivoCopiado.Substring(archivoCopiado.LastIndexOf("\") + 1, (archivoCopiado.Length - archivoCopiado.LastIndexOf("\")) - 1)
                    archivoCopiado = ""
                Else
                    archivoOrigen = pathOrigenArchivoExtracto & nombreArchivo.Replace(".xml", ".zip")
                End If


                If Not File.Exists(archivoOrigen) Then
                    'MsgBox("No se encontró el archivo origen:" & archivoOrigen, MsgBoxStyle.Information)
                    bandera = 1
                    Return Nothing
                End If

                PathArchivoExtracto = archivoOrigen

                '** crea la carpeta si no existe
                FileSystemHelper.CrearPath(pathDestinoArchivoExtracto)
                pos_ini = archivoOrigen.LastIndexOf(".")

                If Mid(archivoOrigen, pos_ini + 1, 4).ToLower = ".zip" Then
                    '** descomprime los archivos
                    FileSystemHelper.Descomprimir(pathDestinoArchivoExtracto, archivoOrigen)
                    nombreArchivo = nombreArchivo.Replace(".zip", ".xml")
                End If

                ArchivoDestino = pathDestinoArchivoExtracto & nombreArchivo
                Archivocontrol = pathDestinoArchivoExtracto & nombreArchivo.Replace(".xml", ".sha")

                '** control del archivo contra el archivo de control sha1
                If General.Extr_interjur_CTR_SHA1 = "S" Then
                    If Not FileSystemHelper.ControlSha512InterJ(ArchivoDestino, Archivocontrol) Then
                        MsgBox("El archivo " & nombreArchivo & " no se corresponde con el archivo de control SHA 512. No se puede generar el extracto.", MsgBoxStyle.Exclamation)
                        'borra archivos .xml y .sha
                        FileSystemHelper.BorrarArchivo(ArchivoDestino)
                        FileSystemHelper.BorrarArchivo(Archivocontrol)
                        Return Nothing
                    End If
                End If

                extr.Autoridad_1 = New libEntities.Entities.Autoridad
                extr.Autoridad_2 = New libEntities.Entities.Autoridad
                extr.Autoridad_3 = New libEntities.Entities.Autoridad
                extr.Autoridad_4 = New libEntities.Entities.Autoridad
                extr.Autoridad_5 = New libEntities.Entities.Autoridad

                extr.Numero_1 = New cValorPosicion
                extr.Numero_2 = New cValorPosicion
                extr.Numero_3 = New cValorPosicion
                extr.Numero_4 = New cValorPosicion
                extr.Numero_5 = New cValorPosicion
                extr.Numero_6 = New cValorPosicion
                extr.Numero_7 = New cValorPosicion
                extr.Numero_8 = New cValorPosicion
                extr.Numero_9 = New cValorPosicion
                extr.Numero_10 = New cValorPosicion
                extr.Numero_11 = New cValorPosicion
                extr.Numero_12 = New cValorPosicion
                extr.Numero_13 = New cValorPosicion
                extr.Numero_14 = New cValorPosicion
                extr.Numero_15 = New cValorPosicion
                extr.Numero_16 = New cValorPosicion
                extr.Numero_17 = New cValorPosicion
                extr.Numero_18 = New cValorPosicion
                extr.Numero_19 = New cValorPosicion
                extr.Numero_20 = New cValorPosicion
                extr.Numero_21 = New cValorPosicion
                extr.Numero_22 = New cValorPosicion
                extr.Numero_23 = New cValorPosicion
                extr.Numero_24 = New cValorPosicion
                extr.Numero_25 = New cValorPosicion
                extr.Numero_26 = New cValorPosicion
                extr.Numero_27 = New cValorPosicion
                extr.Numero_28 = New cValorPosicion
                extr.Numero_29 = New cValorPosicion
                extr.Numero_30 = New cValorPosicion
                extr.Numero_31 = New cValorPosicion
                extr.Numero_32 = New cValorPosicion
                extr.Numero_33 = New cValorPosicion
                extr.Numero_34 = New cValorPosicion
                extr.Numero_35 = New cValorPosicion
                extr.Numero_36 = New cValorPosicion

                extr.idJuego = idJuego

                'comienza a leer el archivo 
                f = New StreamReader(ArchivoDestino)

                While Not f.EndOfStream
                    linea = f.ReadLine()

                    ' letras de nacional
                    If Left(linea.Trim(), 8).ToLower() = "<letras>" Then ' solo viene en Nacional
                        letras = Mid(linea.Trim(), 9, InStr(linea.Trim().ToLower(), "</letras>") - 9)
                        arrLetras = Split(letras, " ")

                        oLetras = New extracto_qnl_letras
                        oLetras.Orden = 1
                        oLetras.letra = arrLetras(0)
                        oLetras.idLoteria = oLoteria.IdLoteria
                        oLetras.idPgmSorteo = idpgmsorteo
                        ls.Add(oLetras)

                        oLetras = New extracto_qnl_letras
                        oLetras.Orden = 2
                        oLetras.letra = arrLetras(1)
                        oLetras.idLoteria = oLoteria.IdLoteria
                        oLetras.idPgmSorteo = idpgmsorteo
                        ls.Add(oLetras)

                        oLetras = New extracto_qnl_letras
                        oLetras.Orden = 3
                        oLetras.letra = arrLetras(2)
                        oLetras.idLoteria = oLoteria.IdLoteria
                        oLetras.idPgmSorteo = idpgmsorteo
                        ls.Add(oLetras)

                        oLetras = New extracto_qnl_letras
                        oLetras.Orden = 4
                        oLetras.letra = arrLetras(3)
                        oLetras.idLoteria = oLoteria.IdLoteria
                        oLetras.idPgmSorteo = idpgmsorteo
                        ls.Add(oLetras)

                        extr.Extracto_Letras_Qnl = New ListaOrdenada(Of extracto_qnl_letras)
                        For Each e In ls
                            extr.Extracto_Letras_Qnl.Add(e)
                        Next
                        Dim x As Integer = 0
                    End If
                    ' NumeroSorteo
                    If Left(linea.Trim(), 8).ToLower() = "<sorteo>" Then
                        extr.NumeroSorteo = Mid(linea.Trim(), 9, InStr(linea.Trim().ToLower(), "</sorteo>") - 9)
                    End If
                    ' FechaSorteo
                    If Left(linea.Trim(), 13).ToLower() = "<fechasorteo>" Then
                        _fechaSorteo = Mid(linea.Trim(), 14, InStr(linea.Trim().ToLower(), "</fechasorteo>") - 14)
                        arrFec = Split(_fechaSorteo, "-")
                        If UBound(arrFec) < 2 Then
                            _fechaSorteo = _fechaVacia
                        Else
                            _fechaSorteo = arrFec(2) & arrFec(1) & arrFec(0)
                        End If
                    End If
                    ' HoraSorteo
                    If Left(linea.Trim(), 12).ToLower() = "<horasorteo>" Then
                        _horaSorteo = Mid(linea.Trim(), 13, InStr(linea.Trim().ToLower(), "</horasorteo>") - 13).Replace(":", "")
                        ' Hora ini y fin loteria
                        extr.HoraIniLoteria = _horaSorteo.Replace(":", "")
                        extr.HoraFinLoteria = _horaSorteo.Replace(":", "")
                    End If
                    ' FechaHoraSorteo
                    If _fechaSorteo <> "" And _horaSorteo <> "" Then
                        extr.FechaHoraSorteo = FormateaFechaHoraaExtracto(_fechaSorteo, _horaSorteo)
                    End If
                    ' FechaPresc

                    If Left(linea.Trim(), 19).ToLower() = "<fechaprescripcion>" Then
                        _fechaPresc = Mid(linea.Trim(), 20, InStr(linea.Trim().ToLower(), "</fechaprescripcion>") - 20)
                        arrFec = Split(_fechaPresc, "-")
                        If UBound(arrFec) < 2 Then
                            _fechaPresc = _fechaVacia.ToString("yyyyMMdd")
                        Else
                            _fechaPresc = arrFec(2) & arrFec(1) & arrFec(0)
                        End If
                        _horaPresc = "0000"
                        extr.FechaHoraCaducidad = FormateaFechaHoraaExtracto(_fechaPresc, _horaPresc)
                    End If
                    ' FechaProxSorteo
                    If Left(linea.Trim(), 20).ToLower() = "<fechaproximosorteo>" Then
                        _fechaProxSorteo = Mid(linea.Trim(), 21, InStr(linea.Trim().ToLower(), "</fechaproximosorteo>") - 21)
                        arrFec = Split(_fechaProxSorteo, "-")
                        If UBound(arrFec) < 2 Then
                            _fechaProxSorteo = _fechaVacia.ToString("yyyyMMdd")
                        Else
                            _fechaProxSorteo = arrFec(2) & arrFec(1) & arrFec(0)
                        End If
                    End If
                    ' HoraProxSorteo
                    If Left(linea.Trim(), 19).ToLower() = "<horaproximosorteo>" Then
                        _horaProxSorteo = Mid(linea.Trim(), 20, InStr(linea.Trim().ToLower(), "</horaproximosorteo>") - 20).Replace(":", "").Trim
                        If Len(_horaProxSorteo) <> 4 Then
                            _horaProxSorteo = "0000"
                        End If
                    End If
                    ' FechaHoraProxSorteo
                    If _fechaProxSorteo <> "" And _horaProxSorteo <> "" Then
                        extr.FechaHoraProximo = FormateaFechaHoraaExtracto(_fechaProxSorteo, _horaProxSorteo)
                    End If
                    ' Localidad
                    extr.Localidad = ""
                    ' Autoridad 1
                    If Left(linea.Trim(), 15).ToLower() = "<autoridadtipo>" Then
                        Dim _aut = Mid(linea.Trim(), 16, InStr(linea.Trim().ToLower(), "</autoridadtipo>") - 16)
                        If _aut.Trim().ToLower() = "escribano" Or _aut.Trim().ToLower() = "area notarial" Then
                            extr.Autoridad_1.cargo = _aut
                        Else
                            If extr.Autoridad_2.cargo = "" Then
                                extr.Autoridad_2.cargo = _aut
                            End If
                        End If
                    End If
                    If Left(linea.Trim(), 17).ToLower() = "<autoridadnombre>" Then
                        Dim _aut = Mid(linea.Trim(), 18, InStr(linea.Trim().ToLower(), "</autoridadnombre>") - 18)
                        ' si cargue cargo 1 y no cargue nombre 1 => el nombre es el del cargo1
                        ' esto lo puedo hacer asi porque siempre despues de autoridadTipo va a venir autoridadNombre de ESE tipo
                        If extr.Autoridad_1.cargo <> "" And extr.Autoridad_1.Nombre = "" Then
                            extr.Autoridad_1.Nombre = _aut
                        Else
                            If extr.Autoridad_1.cargo <> "" Then ' significa que autoridad1_nombre <> "" => autoridad1 ya esta completa, debo completar autoridad2
                                extr.Autoridad_2.Nombre = _aut
                            End If
                        End If
                    End If
                    ' Las otras autoridades vacias
                    extr.Autoridad_3.cargo = ""
                    extr.Autoridad_3.Nombre = ""
                    extr.Autoridad_4.cargo = ""
                    extr.Autoridad_4.Nombre = ""
                    extr.Autoridad_5.cargo = ""
                    extr.Autoridad_5.Nombre = ""
                    ' Loteria
                    extr.Loteria = oLoteria.IdLoteria
                    ' Extracciones
                    If Left(linea.Trim(), 2).ToLower() = "<n" Then
                        _posicion = Right(Left(linea.Trim(), 4), 2)
                        _valorSTR = Mid(linea.Trim(), 6, InStr(linea.Trim().ToLower(), "</n") - 6).Trim()
                        _valor = _valorSTR
                        Select Case _posicion
                            Case 1
                                extr.Cifras = Len(_valorSTR)
                                extr.Numero_1.Posicion = _posicion
                                extr.Numero_1.Valor = _valor
                                extr.Numero_1.ValorSTR = _valorSTR
                            Case 2
                                extr.Numero_2.Posicion = _posicion
                                extr.Numero_2.Valor = _valor
                                extr.Numero_2.ValorSTR = _valorSTR
                            Case 3
                                extr.Numero_3.Posicion = _posicion
                                extr.Numero_3.Valor = _valor
                                extr.Numero_3.ValorSTR = _valorSTR
                            Case 4
                                extr.Numero_4.Posicion = _posicion
                                extr.Numero_4.Valor = _valor
                                extr.Numero_4.ValorSTR = _valorSTR
                            Case 5
                                extr.Numero_5.Posicion = _posicion
                                extr.Numero_5.Valor = _valor
                                extr.Numero_5.ValorSTR = _valorSTR
                            Case 6
                                extr.Numero_6.Posicion = _posicion
                                extr.Numero_6.Valor = _valor
                                extr.Numero_6.ValorSTR = _valorSTR
                            Case 7
                                extr.Numero_7.Posicion = _posicion
                                extr.Numero_7.Valor = _valor
                                extr.Numero_7.ValorSTR = _valorSTR
                            Case 8
                                extr.Numero_8.Posicion = _posicion
                                extr.Numero_8.Valor = _valor
                                extr.Numero_8.ValorSTR = _valorSTR
                            Case 9
                                extr.Numero_9.Posicion = _posicion
                                extr.Numero_9.Valor = _valor
                                extr.Numero_9.ValorSTR = _valorSTR
                            Case 10
                                extr.Numero_10.Posicion = _posicion
                                extr.Numero_10.Valor = _valor
                                extr.Numero_10.ValorSTR = _valorSTR
                            Case 11
                                extr.Numero_11.Posicion = _posicion
                                extr.Numero_11.Valor = _valor
                                extr.Numero_11.ValorSTR = _valorSTR
                            Case 12
                                extr.Numero_12.Posicion = _posicion
                                extr.Numero_12.Valor = _valor
                                extr.Numero_12.ValorSTR = _valorSTR
                            Case 13
                                extr.Numero_13.Posicion = _posicion
                                extr.Numero_13.Valor = _valor
                                extr.Numero_13.ValorSTR = _valorSTR
                            Case 14
                                extr.Numero_14.Posicion = _posicion
                                extr.Numero_14.Valor = _valor
                                extr.Numero_14.ValorSTR = _valorSTR
                            Case 15
                                extr.Numero_15.Posicion = _posicion
                                extr.Numero_15.Valor = _valor
                                extr.Numero_15.ValorSTR = _valorSTR
                            Case 16
                                extr.Numero_16.Posicion = _posicion
                                extr.Numero_16.Valor = _valor
                                extr.Numero_16.ValorSTR = _valorSTR
                            Case 17
                                extr.Numero_17.Posicion = _posicion
                                extr.Numero_17.Valor = _valor
                                extr.Numero_17.ValorSTR = _valorSTR
                            Case 18
                                extr.Numero_18.Posicion = _posicion
                                extr.Numero_18.Valor = _valor
                                extr.Numero_18.ValorSTR = _valorSTR
                            Case 19
                                extr.Numero_19.Posicion = _posicion
                                extr.Numero_19.Valor = _valor
                                extr.Numero_19.ValorSTR = _valorSTR
                            Case 20
                                extr.Numero_20.Posicion = _posicion
                                extr.Numero_20.Valor = _valor
                                extr.Numero_20.ValorSTR = _valorSTR
                        End Select
                    End If
                    ' -----------------------------------------------------------------------------
                End While
                f.Dispose()
                Return extr
            Catch ex As Exception
                Throw New Exception(" GenerarArchivoExtracto:" & ex.Message)
            End Try
        End Function

        Public Function FormateaFechaHoraaExtracto(ByVal Fecha As String, ByVal Hora As String) As DateTime
            Try
                Dim _fechaAux As String
                Dim _horaAux As String
                _fechaAux = Mid(Fecha, 7, 2) & "/" & Mid(Fecha, 5, 2) & "/" & Mid(Fecha, 1, 4)

                '** 26/09/2012 se formatea con 24 hs
                _horaAux = Mid(Hora, 1, 2) & ":" & Mid(Hora, 3, 2)
                Return CDate(_fechaAux & " " & _horaAux)
            Catch ex As Exception
                Throw New Exception("FormateaFechaHoraaExtracto:" & ex.Message)
            End Try
        End Function

        Public Function InsertaDetalleExtracto_Auditoria(ByVal cextracto As cExtractoArchivoBoldt) As Boolean
            Dim _archivo As String = ""
            Dim _idpgmconcurso As String = ""
            Dim archivoOrigen As String = ""
            Dim oarchivoBoldt As New ArchivoBoldtDAL
            Dim valor As Boolean = True
            Try

                '*** inserta los datos del extracto de boldt en las tablas de auditoria
                Return oarchivoBoldt.InsertaDetalleExtracto_Auditoria(cextracto)

            Catch ex As Exception
                Throw New Exception(" InsertaDetalleExtracto_Auditoria:" & ex.Message)
            End Try

        End Function

        Public Function ActualizaDetalleExtracto_Auditoria(ByVal idpgmconcurso As Long) As Boolean
            Dim _archivo As String = ""
            Dim archivoOrigen As String = ""
            Dim oarchivoBoldt As New ArchivoBoldtDAL
            Dim valor As Boolean = True
            Try
                '*** actualiza la tabla detalle auditoria con los datos de la tabla de extraccionesDet par despeus comparar
                Return oarchivoBoldt.ActualizaDetalleExtracto_Auditoria(idpgmconcurso)

            Catch ex As Exception
                Throw New Exception(" ActualizaDetalleExtracto_Auditoria:" & ex.Message)
            End Try
        End Function

        Public Function ControlarDetalleExtracto_Auditoria(ByVal idpgmconcurso As Long) As Boolean
            Dim _archivo As String = ""
            Dim archivoOrigen As String = ""
            Dim oarchivoBoldt As New ArchivoBoldtDAL
            Dim valor As Boolean = True
            Try
                '**** comparar los valores campo a campo par ver si hay diferencias 
                Return oarchivoBoldt.ControlarDetalleExtracto_Auditoria(idpgmconcurso)
            Catch ex As Exception
                Throw New Exception(" ControlarDetalleExtracto_Auditoria:" & ex.Message)
            End Try

        End Function

        Public Function InsertaCabeceraExtracto_Auditoria(ByVal Archivo As String, ByVal valor As Boolean, ByVal extracto As cExtractoArchivoBoldt) As Boolean
            Dim oarchivoBoldt As New ArchivoBoldtDAL
            Try
                Return oarchivoBoldt.InsertaCabeceraExtracto_Auditoria(Archivo, valor, extracto)

            Catch ex As Exception
                Throw New Exception("InsertaCabeceraExtracto_Auditoria:" & ex.Message)
            End Try
        End Function

        Public Function CreaNombreArchivoExtracto(ByVal idjuego As Long, ByVal nrosorteo As Long) As String
            Dim nombre As String = ""
            Dim _nrosorteo As String = ""
            Dim prefijo As String = ""
            Try
                Dim oDalBoldt As New ArchivoBoldtDAL
                'crea el nombre del archivo
                _nrosorteo = nrosorteo.ToString.PadLeft(5, "0")
                ''Select Case idjuego
                ''    Case 4
                ''        prefijo = "Q6"
                ''    Case 13
                ''        prefijo = "BR"
                ''    Case 30
                ''        prefijo = "PM"
                ''    Case 49
                ''        prefijo = "QA"
                ''    Case 2
                ''        prefijo = "QN"
                ''    Case 3
                ''        prefijo = "QV"
                ''    Case 8
                ''        prefijo = "QM"
                ''    Case 39
                ''        prefijo = "QE"
                ''    Case 50
                ''        prefijo = "BL"
                ''    Case 51
                ''        prefijo = "LC"
                ''    Case 26
                ''        prefijo = "QU"
                ''End Select

                nombre = oDalBoldt.ObtenerCodigoJuegoBoldt(idjuego) & General.LetraPciaArchivosBoldt & _nrosorteo

                Return nombre
            Catch ex As Exception
                Throw New Exception("CreaNombreArchivoExtracto" & ex.Message)
            End Try
        End Function

        Public Function Actualiza_ExtraccionesDet_con_ExtractoBoldt(ByVal idpgmconcurso As Long) As Boolean
            Dim oarchivoBoldt As New ArchivoBoldtDAL
            Try
                Return oarchivoBoldt.Actualiza_ExtraccionesDet_con_ExtractoBoldt(idpgmconcurso)
            Catch ex As Exception
                Throw New Exception("Actualiza_ExtraccionesDet_con_ExtractoBoldt:" & ex.Message)
            End Try
        End Function

        Public Function Tiene_que_generar_archivoextracto(ByVal idjuego As Long) As Boolean

            Dim oarchivoBoldt As New ArchivoBoldtDAL
            Try

                '*** inserta los datos del extracto de boldt en las tablas de auditoria
                Return oarchivoBoldt.Tiene_que_generar_archivoextracto(idjuego)

            Catch ex As Exception
                Throw New Exception(" Tiene_que_generar_archivoextracto:" & ex.Message)
            End Try

        End Function

        Private Sub limpiarVariablesLectura(ByRef NumeroSorteo_fa As String, ByRef FechaHoraSorteo_fa As String, ByRef FechaHoraProximo_fa As String, ByRef FechaHoraCaducidad_fa As String, ByRef _
                            Localidad_fa As String, ByRef Autoridad_1_cargo_fa As String, ByRef Autoridad_1_Nombre_fa As String, ByRef Autoridad_2_cargo_fa As String, ByRef Autoridad_2_Nombre_fa As String, ByRef _
                            Autoridad_3_cargo_fa As String, ByRef Autoridad_3_Nombre_fa As String, ByRef Autoridad_4_cargo_fa As String, ByRef Autoridad_4_Nombre_fa As String, ByRef Autoridad_5_cargo_fa As String, ByRef Autoridad_5_Nombre_fa As String, ByRef _
                            Cifras_fa As String, ByRef Loteria_fa As String, ByRef FechaHora_fa As String, ByRef Modalidad_fa As String, ByRef Cifras_fa3 As String, ByRef ValorSTR_fa As String, ByRef Posicion_fa As String, _
                            ByRef idJuego_fb As String, ByRef NumeroSorteo_fb As String, ByRef FechaHoraSorteo_fb As String, ByRef FechaHoraProximo_fb As String, ByRef FechaHoraCaducidad_fb As String, ByRef _
                            Localidad_fb As String, ByRef Autoridad_1_cargo_fb As String, ByRef Autoridad_1_Nombre_fb As String, ByRef Autoridad_2_cargo_fb As String, ByRef Autoridad_2_Nombre_fb As String, ByRef _
                            Autoridad_3_cargo_fb As String, ByRef Autoridad_3_Nombre_fb As String, ByRef Autoridad_4_cargo_fb As String, ByRef Autoridad_4_Nombre_fb As String, ByRef Autoridad_5_cargo_fb As String, ByRef Autoridad_5_Nombre_fb As String, ByRef _
                            Cifras_fb As String, ByRef Loteria_fb As String, ByRef FechaHora_fb As String, ByRef Modalidad_fb As String, ByRef Cifras_fb3 As String, ByRef ValorSTR_fb As String, ByRef Posicion_fb As String)

        End Sub

        Private Sub leerTipo01(ByRef linea As String, ByRef idJuego As String, ByRef NumeroSorteo As String, ByRef FechaHoraSorteo As String, ByRef FechaHoraProximo As String, ByRef FechaHoraCaducidad As String, _
                            ByRef Localidad As String, ByRef Autoridad_1_cargo As String, ByRef Autoridad_1_Nombre As String, ByRef Autoridad_2_cargo As String, ByRef Autoridad_2_Nombre As String, _
                            ByRef Autoridad_3_cargo As String, ByRef Autoridad_3_Nombre As String, ByRef Autoridad_4_cargo As String, ByRef Autoridad_4_Nombre As String, ByRef Autoridad_5_cargo As String, ByRef Autoridad_5_Nombre As String, _
                            ByRef Cifras As String)
            idJuego = Mid(linea, 3, 3).Trim()
            NumeroSorteo = Mid(linea, 6, 5).Trim()
            FechaHoraSorteo = Mid(linea, 11, 8) & Mid(linea, 19, 4).Trim()
            FechaHoraProximo = Mid(linea, 11, 8) & Mid(linea, 19, 4).Trim()
            FechaHoraCaducidad = Mid(linea, 11, 8) & Mid(linea, 19, 4).Trim()
            Localidad = Mid(linea, 43, 40).Trim()
            Autoridad_1_cargo = Mid(linea, 83, 30).Trim()
            Autoridad_1_Nombre = Mid(linea, 113, 30).Trim()
            Autoridad_2_cargo = Mid(linea, 143, 30).Trim()
            Autoridad_2_Nombre = Mid(linea, 173, 30).Trim()
            Autoridad_3_cargo = Mid(linea, 203, 30).Trim()
            Autoridad_3_Nombre = Mid(linea, 233, 30).Trim()
            Autoridad_4_cargo = Mid(linea, 263, 30).Trim()
            Autoridad_4_Nombre = Mid(linea, 293, 30).Trim()
            Autoridad_5_cargo = Mid(linea, 323, 30).Trim()
            Autoridad_5_Nombre = Mid(linea, 353, 30).Trim()
            Select Case idJuego
                Case 4, 13
                    Cifras = 2
                Case 30
                    Cifras = 2
                Case 3, 8, 49
                    Cifras = 4
                Case 50
                    Cifras = 5
                Case Else
                    Cifras = 0
            End Select
        End Sub

        Private Sub leerTipo02(ByRef linea As String, ByRef Loteria As String, ByRef FechaHora As String)
            Dim oDalBoldt As New ArchivoBoldtDAL
            Loteria = oDalBoldt.ObtenerCodigoLoteriaExtracto(Mid(linea, 5, 2))
            FechaHora = Mid(linea, 7, 12)
        End Sub

        Private Sub leerTipo03(ByRef linea As String, ByRef Modalidad As String, ByRef Cifras As String, ByRef ValorSTR As String, ByRef Posicion As String)
            Dim _tipoPremio As String = ""

            _tipoPremio = Mid(linea, 3, 2)

            Modalidad = _tipoPremio ' por ahora no hago nada
            Cifras = Mid(linea, 5, 1)
            ValorSTR = Right(Trim(Mid(linea, 6, 9)), Cifras)
            Posicion = Mid(linea, 15, 2)
        End Sub

        Public Sub LimpiarDifCuadraturaExtracto(ByRef oSorteo As PgmSorteo)
            Dim oDalBoldt As New ArchivoBoldtDAL
            oDalBoldt.LimpiarDifCuadraturaExtracto(oSorteo)
        End Sub

        Public Sub auditarDifCuadraturaExtracto(ByRef oSorteo As PgmSorteo, ByVal aud_detalle As String)
            Dim oDalBoldt As New ArchivoBoldtDAL
            oDalBoldt.auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
        End Sub

        Public Function CompararArchivoExtracto(ByVal oSorteo As PgmSorteo, ByVal Path As String, ByRef msgRet As String, Optional ByVal SoloJurLocal As Boolean = False) As Boolean
            Dim nombreArchivo As String = ""
            Dim aud_detalle As String = ""
            msgRet = ""
            '--------------------------------------------------------
            ' Archivo PC-A
            Dim fa As StreamReader
            Dim archivoPC_A As String = ""
            Dim linea_fa As String = ""
            Dim tiporegistro_fa As String = ""

            ' Registro tipo 01
            Dim idJuego_fa As String = ""
            Dim NumeroSorteo_fa As String = ""
            Dim FechaHoraSorteo_fa As String = ""
            Dim FechaHoraProximo_fa As String = ""
            Dim FechaHoraCaducidad_fa As String = ""
            Dim Localidad_fa As String = ""
            Dim Autoridad_1_cargo_fa As String = ""
            Dim Autoridad_1_Nombre_fa As String = ""
            Dim Autoridad_2_cargo_fa As String = ""
            Dim Autoridad_2_Nombre_fa As String = ""
            Dim Autoridad_3_cargo_fa As String = ""
            Dim Autoridad_3_Nombre_fa As String = ""
            Dim Autoridad_4_cargo_fa As String = ""
            Dim Autoridad_4_Nombre_fa As String = ""
            Dim Autoridad_5_cargo_fa As String = ""
            Dim Autoridad_5_Nombre_fa As String = ""
            Dim Cifras_fa As String = ""

            ' Registro tipo 02
            Dim Loteria_fa As String = ""
            Dim FechaHora_fa As String = ""

            'Registro tipo 03
            Dim Modalidad_fa As String = ""
            Dim ValorSTR_fa As String = ""
            Dim Posicion_fa As String = ""
            Dim Cifras_fa3 As String = ""
            '--------------------------------------------------------

            ' Archivo PC-B
            Dim fb As StreamReader ' archivo pc-b
            Dim archivoPC_B As String = ""
            Dim linea_fb As String = ""
            Dim tiporegistro_fb As String = ""

            ' Registro tipo 01
            Dim idJuego_fb As String = ""
            Dim NumeroSorteo_fb As String = ""
            Dim FechaHoraSorteo_fb As String = ""
            Dim FechaHoraProximo_fb As String = ""
            Dim FechaHoraCaducidad_fb As String = ""
            Dim Localidad_fb As String = ""
            Dim Autoridad_1_cargo_fb As String = ""
            Dim Autoridad_1_Nombre_fb As String = ""
            Dim Autoridad_2_cargo_fb As String = ""
            Dim Autoridad_2_Nombre_fb As String = ""
            Dim Autoridad_3_cargo_fb As String = ""
            Dim Autoridad_3_Nombre_fb As String = ""
            Dim Autoridad_4_cargo_fb As String = ""
            Dim Autoridad_4_Nombre_fb As String = ""
            Dim Autoridad_5_cargo_fb As String = ""
            Dim Autoridad_5_Nombre_fb As String = ""
            Dim Cifras_fb As String = ""

            ' Registro tipo 02
            Dim Loteria_fb As String = ""
            Dim FechaHora_fb As String = ""

            'Registro tipo 03
            Dim Modalidad_fb As String = ""
            Dim ValorSTR_fb As String = ""
            Dim Posicion_fb As String = ""
            Dim Cifras_fb3 As String = ""
            '--------------------------------------------------------
            Try
                Dim pathDestinoArchivoExtracto As String = General.CarpetaDestinoArchivosExtractoBoldt
                Dim pathOrigenArchivoExtracto As String = General.CarpetaOrigenArchivosExtractoBoldt

                Dim ArchivoDestino As String = ""
                Dim Archivocontrol As String = ""

                Dim tiporegistro As String = ""
                Dim _posicion As Integer = 0
                Dim _valor As Long = 0
                Dim _valorSTR As String = ""
                Dim _tipoPremio As Integer = 0
                Dim prefijo As String = ""
                Dim _nrosorteo As String = ""
                Dim _fechaSorteo As String = ""
                Dim _HoraSorteo As String = ""

                If Not Path.EndsWith("\") Then
                    Path = Path & "\"
                End If

                'agregar barra si no la tiene
                FormateaFechaHora(oSorteo.fechaHoraIniReal, _fechaSorteo, _HoraSorteo)

                ''If Not Path.EndsWith("\") Then
                ''    Path = Path & "\" & _fechaSorteo
                ''Else
                ''    Path = Path & _fechaSorteo
                ''End If
                If Not Path.EndsWith("\") Then Path = Path & "\"

                ''crea el nombre del archivo
                Dim nombreArchivoA As String = ""
                nombreArchivo = CreaNombreArchivoExtracto(oSorteo.idJuego, oSorteo.nroSorteo)
                If SoloJurLocal And General.Modo_Operacion.ToUpper <> "PC-B" Then
                    nombreArchivoA = nombreArchivo & "P" ' P = sólo sorteo PROPIO!!
                Else
                    nombreArchivoA = nombreArchivo
                End If
                archivoPC_A = Path & nombreArchivoA & ".ext"
                If Not File.Exists(archivoPC_A) Then
                    msgRet = "No se encontró el archivo de PC-A:" & archivoPC_A
                    Return False
                End If

                archivoPC_B = Path & nombreArchivo & "c.ext"
                If Not File.Exists(archivoPC_B) Then
                    msgRet = "No se encontró el archivo de PC-B:" & archivoPC_B & ". Primero debe CONFIRMAR SORTEO en PC-B. Luego vuelva a intentar."
                    Return False
                End If

                ' limpia auditoria del sorteo
                LimpiarDifCuadraturaExtracto(oSorteo)
                'comienza a leer los archivos
                fa = New StreamReader(archivoPC_A)
                fb = New StreamReader(archivoPC_B)

                While (Not fa.EndOfStream) And (Not fb.EndOfStream) ' tengo registros en ambos archivos
                    limpiarVariablesLectura(NumeroSorteo_fa, FechaHoraSorteo_fa, FechaHoraProximo_fa, FechaHoraCaducidad_fa, _
                            Localidad_fa, Autoridad_1_cargo_fa, Autoridad_1_Nombre_fa, Autoridad_2_cargo_fa, Autoridad_2_Nombre_fa, _
                            Autoridad_3_cargo_fa, Autoridad_3_Nombre_fa, Autoridad_4_cargo_fa, Autoridad_4_Nombre_fa, Autoridad_5_cargo_fa, Autoridad_5_Nombre_fa, _
                            Cifras_fa, Loteria_fa, FechaHora_fa, Modalidad_fa, Cifras_fa3, ValorSTR_fa, Posicion_fa, _
                             idJuego_fb, NumeroSorteo_fb, FechaHoraSorteo_fb, FechaHoraProximo_fb, FechaHoraCaducidad_fb, _
                            Localidad_fb, Autoridad_1_cargo_fb, Autoridad_1_Nombre_fb, Autoridad_2_cargo_fb, Autoridad_2_Nombre_fb, _
                            Autoridad_3_cargo_fb, Autoridad_3_Nombre_fb, Autoridad_4_cargo_fb, Autoridad_4_Nombre_fb, Autoridad_5_cargo_fb, Autoridad_5_Nombre_fb, _
                            Cifras_fb, Loteria_fb, FechaHora_fb, Modalidad_fb, Cifras_fb3, ValorSTR_fb, Posicion_fb)

                    linea_fa = fa.ReadLine()
                    linea_fb = fb.ReadLine()
                    tiporegistro_fa = linea_fa.Substring(0, 2)
                    tiporegistro_fb = linea_fa.Substring(0, 2)
                    'tipo de registro
                    '01-cabecera
                    '02-datos de loteria
                    '03-detalle de los numeros
                    Select Case tiporegistro_fa
                        Case "01"
                            leerTipo01(linea_fa, idJuego_fa, NumeroSorteo_fa, FechaHoraSorteo_fa, FechaHoraProximo_fa, FechaHoraCaducidad_fa, _
                            Localidad_fa, Autoridad_1_cargo_fa, Autoridad_1_Nombre_fa, Autoridad_2_cargo_fa, Autoridad_2_Nombre_fa, _
                            Autoridad_3_cargo_fa, Autoridad_3_Nombre_fa, Autoridad_4_cargo_fa, Autoridad_4_Nombre_fa, Autoridad_5_cargo_fa, Autoridad_5_Nombre_fa, _
                            Cifras_fa)
                        Case "02"
                            leerTipo02(linea_fa, Loteria_fa, FechaHora_fa)
                            While SoloJurLocal And Loteria_fa <> General.Jurisdiccion And (tiporegistro_fa = "02" Or tiporegistro_fa = "03") And (Not fa.EndOfStream)
                                linea_fa = fa.ReadLine()
                                tiporegistro_fa = linea_fa.Substring(0, 2)
                                If tiporegistro_fa = "02" Then
                                    leerTipo02(linea_fa, Loteria_fa, FechaHora_fa)
                                Else
                                    leerTipo03(linea_fa, Modalidad_fa, Cifras_fa3, ValorSTR_fa, Posicion_fa)
                                End If
                            End While

                        Case "03"
                            leerTipo03(linea_fa, Modalidad_fa, Cifras_fa3, ValorSTR_fa, Posicion_fa)
                        Case Else
                            aud_detalle = "Formato de registro erroneo en extracto PC-A. Cierre la ventana de Extractos e intente nuevamente."
                            auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            Return False
                    End Select
                    Select Case tiporegistro_fb
                        Case "01"
                            leerTipo01(linea_fb, idJuego_fb, NumeroSorteo_fb, FechaHoraSorteo_fb, FechaHoraProximo_fb, FechaHoraCaducidad_fb, _
                            Localidad_fb, Autoridad_1_cargo_fb, Autoridad_1_Nombre_fb, Autoridad_2_cargo_fb, Autoridad_2_Nombre_fb, _
                            Autoridad_3_cargo_fb, Autoridad_3_Nombre_fb, Autoridad_4_cargo_fb, Autoridad_4_Nombre_fb, Autoridad_5_cargo_fb, Autoridad_5_Nombre_fb, _
                            Cifras_fb)
                        Case "02"
                            leerTipo02(linea_fb, Loteria_fb, FechaHora_fb)
                            While SoloJurLocal And Loteria_fb <> General.Jurisdiccion And (tiporegistro_fb = "02" Or tiporegistro_fb = "03") And (Not fb.EndOfStream)
                                linea_fb = fb.ReadLine()
                                tiporegistro_fb = linea_fb.Substring(0, 2)
                                If tiporegistro_fb = "02" Then
                                    leerTipo02(linea_fb, Loteria_fb, FechaHora_fb)
                                Else
                                    leerTipo03(linea_fb, Modalidad_fb, Cifras_fb3, ValorSTR_fb, Posicion_fb)
                                End If
                            End While
                        Case "03"
                            leerTipo03(linea_fb, Modalidad_fb, Cifras_fb3, ValorSTR_fb, Posicion_fb)
                        Case Else
                            aud_detalle = "Formato de registro erroneo en extracto PC-B. Cierre la ventana de Extractos e intente nuevamente."
                            auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            msgRet = aud_detalle
                            Return False
                    End Select

                    ' Comienzan las comparaciones
                    Select Case tiporegistro_fa
                        Case "01"
                            If tiporegistro_fb <> "01" Then
                                aud_detalle = "Se esperaba registro cabecera (01) en archivo de PC-B. Verificar Planilla de Parámetros."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                                msgRet = aud_detalle
                                Return False
                            End If
                            If idJuego_fa <> idJuego_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Juego en PC-A: " & idJuego_fa & ".   Juego en PC-B: " & idJuego_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If NumeroSorteo_fa <> NumeroSorteo_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Sorteo en PC-A: " & NumeroSorteo_fa & ".   Sorteo en PC-B: " & NumeroSorteo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If FechaHoraSorteo_fa <> FechaHoraSorteo_fb Then
                                aud_detalle = "DATOS DEL SORTEO. FechaHoraSorteo en PC-A: " & FechaHoraSorteo_fa & ".   FechaHoraSorteo en PC-B: " & FechaHoraSorteo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If FechaHoraProximo_fa <> FechaHoraProximo_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Sorteo en PC-A: " & NumeroSorteo_fa & ".   Sorteo en PC-B: " & NumeroSorteo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If FechaHoraCaducidad_fa <> FechaHoraCaducidad_fb Then
                                aud_detalle = "DATOS DEL SORTEO. FechaHoraProximo en PC-A: " & FechaHoraProximo_fa & ".   FechaHoraProximo en PC-B: " & FechaHoraProximo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Localidad_fa <> Localidad_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Localidad en PC-A: " & Localidad_fa & ".   Localidad en PC-B: " & Localidad_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_1_cargo_fa <> Autoridad_1_cargo_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_1_cargo en PC-A: " & Autoridad_1_cargo_fa & ".   Autoridad_1_cargo en PC-B: " & Autoridad_1_cargo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_1_Nombre_fa <> Autoridad_1_Nombre_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_1_Nombre en PC-A: " & Autoridad_1_Nombre_fa & ".   Autoridad_1_Nombre en PC-B: " & Autoridad_1_Nombre_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_2_cargo_fa <> Autoridad_2_cargo_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_2_cargo en PC-A: " & Autoridad_2_cargo_fa & ".   Autoridad_2_cargo en PC-B: " & Autoridad_2_cargo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_2_Nombre_fa <> Autoridad_2_Nombre_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_2_Nombre en PC-A: " & Autoridad_2_Nombre_fa & ".   Autoridad_2_Nombre en PC-B: " & Autoridad_2_Nombre_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_3_cargo_fa <> Autoridad_3_cargo_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_3_cargo en PC-A: " & Autoridad_3_cargo_fa & ".   Autoridad_3_cargo en PC-B: " & Autoridad_3_cargo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_3_Nombre_fa <> Autoridad_3_Nombre_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_3_Nombre en PC-A: " & Autoridad_3_Nombre_fa & ".   Autoridad_3_Nombre en PC-B: " & Autoridad_3_Nombre_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_4_cargo_fa <> Autoridad_4_cargo_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_4_cargo en PC-A: " & Autoridad_4_cargo_fa & ".   Autoridad_4_cargo en PC-B: " & Autoridad_4_cargo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_4_Nombre_fa <> Autoridad_4_Nombre_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_4_Nombre en PC-A: " & Autoridad_4_Nombre_fa & ".   Autoridad_4_Nombre en PC-B: " & Autoridad_4_Nombre_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_5_cargo_fa <> Autoridad_5_cargo_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_5_cargo en PC-A: " & Autoridad_5_cargo_fa & ".   Autoridad_5_cargo en PC-B: " & Autoridad_5_cargo_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Autoridad_5_Nombre_fa <> Autoridad_5_Nombre_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Autoridad_5_Nombre en PC-A: " & Autoridad_5_Nombre_fa & ".   Autoridad_5_Nombre en PC-B: " & Autoridad_5_Nombre_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If Cifras_fa <> Cifras_fb Then
                                aud_detalle = "DATOS DEL SORTEO. Cifras en PC-A: " & Cifras_fa & ".   Cifras en PC-B: " & Cifras_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If

                        Case "02"
                            If tiporegistro_fb <> "02" Then
                                aud_detalle = "Se esperaba registro cabecera (02) en archivo de PC-B. Verificar Planilla de Parámetros."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                                msgRet = aud_detalle
                                Return False
                            End If
                            If Loteria_fa <> Loteria_fb Then
                                aud_detalle = "DISTINTA JURISDICCION. Loteria en PC-A: " & Loteria_fa & ".   Loteria en PC-B: " & Loteria_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If
                            If FechaHora_fa <> FechaHora_fb Then
                                aud_detalle = "DISTINTA HORA EN JURISDICCION. FechaHora en PC-A: " & FechaHora_fa & ".   FechaHora en PC-B: " & FechaHora_fb & "."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                            End If

                        Case "03"
                            If tiporegistro_fb <> "03" Then
                                aud_detalle = "Se esperaba registro cabecera (03) en archivo de PC-B. Verificar Planilla de Parámetros."
                                auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                                msgRet = aud_detalle
                                Return False
                            End If
                            If Loteria_fa = Loteria_fb Then
                                If Modalidad_fa <> Modalidad_fb Then
                                    aud_detalle = "EXTRACCIONES. Lot: " & Loteria_fa & ". Modalidad en PC-A: " & Modalidad_fa & ".   Modalidad en PC-B: " & Modalidad_fb & "."
                                    auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                                End If
                                If Cifras_fa3 <> Cifras_fb3 Or Posicion_fa <> Posicion_fb Or ValorSTR_fa <> ValorSTR_fb Then
                                    aud_detalle = "EXTRACCIONES. Lot: " & Loteria_fa & IIf(Cifras_fa3 <> Cifras_fb3, ". Cant. Cifras en PC-A: " & Cifras_fa3, "") & ". Posición en PC-A: " & Posicion_fa & ". Valor en PC-A: " & ValorSTR_fa & IIf(Cifras_fa3 <> Cifras_fb3, ". Cant. Cifras en PC-B: " & Cifras_fb3, "") & ".  Posicion en PC-B: " & Posicion_fb & ". Valor en PC-B: " & ValorSTR_fb & "."
                                    auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                                End If

                            End If

                    End Select

                End While

                If (Not fa.EndOfStream) Or (Not fb.EndOfStream) Then
                    aud_detalle = "Diferencias generales de composición del Extracto. Posible causa: en un instalación se registraron más Jurisdicciones que en la otra. Verifique Listados de parámetros y de Extracciones."
                    auditarDifCuadraturaExtracto(oSorteo, aud_detalle)
                End If

                fa.Dispose()
                fb.Dispose()

                Return (aud_detalle = "")

            Catch ex As Exception
                Throw New Exception("Problema al comparar archivos de Extracto. " & ex.Message)
                Return False
            End Try

        End Function

        Public Function GenerarArchivoExtracto(ByVal oSorteo As PgmSorteo, ByRef Path As String, Optional ByVal SoloJurLocal As Boolean = False, Optional ByVal Enviar As Boolean = True, Optional ByRef nombrePaq As String = "") As String
            Dim _archivo As String = ""
            Dim extr As New cExtractoArchivoBoldt
            Dim boSorteo As New PgmSorteoBO
            Dim oDalBoldt As New ArchivoBoldtDAL
            Dim oDal As New PgmSorteoDAL
            Dim idPgmSorteo As Integer
            Dim _fechaSorteo As String = ""
            Dim _HoraSorteo As String = ""
            Try
                GenerarArchivoExtracto = ""
                idPgmSorteo = oSorteo.idPgmSorteo
                If idPgmSorteo = 0 Then
                    Throw New Exception("No existe el número de sorteo para el juego indicado.")
                    Return ""
                    Exit Function
                End If

                extr.Autoridad_1 = New libEntities.Entities.Autoridad
                extr.Autoridad_2 = New libEntities.Entities.Autoridad
                extr.Autoridad_3 = New libEntities.Entities.Autoridad
                extr.Autoridad_4 = New libEntities.Entities.Autoridad
                extr.Autoridad_5 = New libEntities.Entities.Autoridad

                extr.Numero_1 = New cValorPosicion
                extr.Numero_2 = New cValorPosicion
                extr.Numero_3 = New cValorPosicion
                extr.Numero_4 = New cValorPosicion
                extr.Numero_5 = New cValorPosicion
                extr.Numero_6 = New cValorPosicion
                extr.Numero_7 = New cValorPosicion
                extr.Numero_8 = New cValorPosicion
                extr.Numero_9 = New cValorPosicion
                extr.Numero_10 = New cValorPosicion
                extr.Numero_11 = New cValorPosicion
                extr.Numero_12 = New cValorPosicion
                extr.Numero_13 = New cValorPosicion
                extr.Numero_14 = New cValorPosicion
                extr.Numero_15 = New cValorPosicion
                extr.Numero_16 = New cValorPosicion
                extr.Numero_17 = New cValorPosicion
                extr.Numero_18 = New cValorPosicion
                extr.Numero_19 = New cValorPosicion
                extr.Numero_20 = New cValorPosicion
                extr.Numero_21 = New cValorPosicion
                extr.Numero_22 = New cValorPosicion
                extr.Numero_23 = New cValorPosicion
                extr.Numero_24 = New cValorPosicion
                extr.Numero_25 = New cValorPosicion
                extr.Numero_26 = New cValorPosicion
                extr.Numero_27 = New cValorPosicion
                extr.Numero_28 = New cValorPosicion
                extr.Numero_29 = New cValorPosicion
                extr.Numero_30 = New cValorPosicion
                extr.Numero_31 = New cValorPosicion
                extr.Numero_32 = New cValorPosicion
                extr.Numero_33 = New cValorPosicion
                extr.Numero_34 = New cValorPosicion
                extr.Numero_35 = New cValorPosicion
                extr.Numero_36 = New cValorPosicion


                extr.FechaHoraCaducidad = oSorteo.fechaHoraPrescripcion
                extr.FechaHoraProximo = oSorteo.fechaHoraProximo

                ' siempre que se genere,el sorteo tiene que estar finalizado
                extr.FechaHoraSorteo = oSorteo.fechaHoraIniReal

                extr.Localidad = oSorteo.localidad.ToUpper
                extr.NumeroSorteo = oSorteo.nroSorteo


                ' autoridades
                Dim oAutoridad As libEntities.Entities.Autoridad
                If oSorteo.autoridades.Count > 0 Then
                    For Each oAutoridad In oSorteo.autoridades
                        If oAutoridad.Orden = 1 Then
                            extr.Autoridad_1.Nombre = oAutoridad.Nombre.Replace("á", "a").Replace("é", "e").Replace("í", "i").Replace("ó", "o").Replace("ú", "u").ToUpper
                            extr.Autoridad_1.cargo = oAutoridad.cargo.ToUpper
                        End If
                    Next
                Else
                    extr.Autoridad_1.Nombre = ""
                    extr.Autoridad_1.cargo = ""
                End If


                '**22-3-2012 se establece el estado del sorteo
                If oSorteo.idEstadoPgmConcurso = 50 Then
                    '      extr.estadoSorteo = 1
                Else
                    '     extr.estadoSorteo = 0
                End If
                'agregar barra si no la tiene
                FormateaFechaHora(oSorteo.fechaHoraIniReal, _fechaSorteo, _HoraSorteo)

                If Not Path.EndsWith("\") Then
                    Path = Path & "\" & _fechaSorteo
                Else
                    Path = Path & _fechaSorteo
                End If
                If Not Path.EndsWith("\") Then Path = Path & "\"

                _archivo = oDalBoldt.ObtenerCodigoJuegoBoldt(oSorteo.idJuego) & General.LetraPciaArchivosBoldt & oSorteo.nroSorteo.ToString.PadLeft(5, "0")
                If SoloJurLocal And General.Modo_Operacion.ToUpper <> "PC-B" Then
                    _archivo = _archivo & "P" ' P = sólo sorteo PROPIO!!
                End If

                If General.Modo_Operacion.ToUpper = "PC-B" Then
                    _archivo = _archivo & "c"
                End If
                '** se sobreescribe el codigo de juego por el que se necesita en el archivo
                '*** borro los archivos sin existen *****
                Dim ArchivoExt As String
                Dim ArchivoCXT As String
                Dim ArchivoZip As String
                Dim archivoGPG As String

                ArchivoExt = Path & _archivo & ".ext"
                FileSystemHelper.BorrarArchivo(ArchivoExt)
                ArchivoCXT = Path & _archivo & ".cxt"
                FileSystemHelper.BorrarArchivo(ArchivoCXT)
                ArchivoZip = Path & _archivo & ".zip"
                FileSystemHelper.BorrarArchivo(ArchivoZip)
                archivoGPG = ArchivoZip & ".gpg"
                FileSystemHelper.BorrarArchivo(archivoGPG)
                nombrePaq = _archivo & ".zip"

                extr.Juego = oSorteo.idJuego.ToString.PadLeft(3, "0")
                CrearArchivoBoldt(extr, _archivo, Path, oSorteo.idJuegoLetra, idPgmSorteo)

                Dim dtLoterias As DataTable
                Dim drLoteria As DataRow
                Dim _pathArchivo As String = ""
                Dim _pathArchivoExt As String = ""
                Dim registros As Long = 1

                If Not Path.EndsWith("\") Then Path = Path & "\"

                _pathArchivo = Path & _archivo & ".ext"
                _pathArchivoExt = _pathArchivo
                dtLoterias = oDalBoldt.ObtenerLoteriasParaArchivoBoldt(oSorteo.idPgmSorteo)
                For Each drLoteria In dtLoterias.Rows
                    If (SoloJurLocal And drLoteria("idloteria") = General.Jurisdiccion) Or SoloJurLocal = False Then
                        If oDal.getExtracccionesLocal(oSorteo.idJuego, oSorteo.idPgmSorteo, drLoteria("idloteria"), extr) Then
                            AgregaLoteriaArchivoBoldt(extr, drLoteria, _pathArchivo, registros)
                        End If
                    End If
                Next
                '**** se crea archivo de control***********
                Dim md5 As String
                md5 = ObtenerMD5(_pathArchivo)
                _pathArchivo = Path & _archivo & ".cxt"
                GeneraArchivodeControl(_pathArchivo, _archivo, oSorteo.idJuego, oSorteo.localidad.ToUpper, oSorteo.nroSorteo, registros, md5)
                '******* genera el zip **************
                Dim _listaArchivos As String()
                ReDim _listaArchivos(2)
                Dim msj As String = ""
                Dim _archivoZip As String = _archivo & ".zip"
                '_listaArchivos = Obtener_Archivos(Path)
                _listaArchivos(0) = _pathArchivoExt
                _listaArchivos(1) = _pathArchivo
                Dim archZip As String = _archivoZip
                FileSystemHelper.ComprimirListaArchivos(_listaArchivos, Path, archZip, msj, False)
                '27/06/2014 evniar el archivo por ftp
                If Enviar Then
                    If General.EncriptarGPG.Trim = "S" Then
                        GeneralBO.encriptararchivoGPG(ArchivoZip)
                    End If
                    If General.EnviarFTP.Trim = "S" Then
                        Dim carpeta As String = ""
                        If GeneralBO.CrearDirectorioFTP(_fechaSorteo, carpeta) Then
                            GeneralBO.enviarFTP(ArchivoZip & ".gpg", carpeta & archZip & ".gpg")
                        End If

                    End If
                End If

                '*** borra los archivos xml
                '08/01/2013 hg no se borran los archivos de ctrl y extracto
                ' Borrar_Archivos(_listaArchivos)
            Catch ex As Exception
                Throw New Exception(" GenerarArchivoExtracto:" & ex.Message)
            End Try

        End Function

        Public Function ObtenerNombreArchExtractoInterJ(ByVal idpgmSorteo As Long, ByVal idLoteria As String, ByRef archivo As String, ByRef msgRet As String) As Boolean
            Dim oArchDAL As New ArchivoBoldtDAL
            Try
                Return oArchDAL.ObtenerNombreArchExtractoInterJ(idpgmSorteo, idLoteria, archivo, msgRet)
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function GenerarArchivoExtractoInterJ(ByVal oSorteo As PgmSorteo, ByRef Path As String, Optional ByRef nombrePaq As String = "", Optional ByRef paraEmail As Boolean = False) As String
            Dim oArchDAL As New ArchivoBoldtDAL
            Dim oSorteoBO As New PgmSorteoBO
            Dim oSorteoDal As New PgmSorteoDAL

            Dim dt As New DataTable

            Dim idPgmSorteo As Integer
            Dim _archivo As String = ""
            Dim _archivo_sha1 As String = ""
            Dim _fechaSorteo As String = ""
            Dim _HoraSorteo As String = ""
            Dim msgRet As String = ""

            Dim ArchivoExt As String
            Dim ArchivoCXT As String
            Dim ArchivoZip As String
            Dim archivoGPG As String
            Dim strClave As String

            Dim _registros As String()
            Dim i As Integer = 0

            Try
                FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ - INICIO - Path ->" & Path & "<-")
                GenerarArchivoExtractoInterJ = ""
                If General.Modo_Operacion.ToUpper = "PC-B" Then
                    Return ""
                End If
                idPgmSorteo = oSorteo.idPgmSorteo
                If idPgmSorteo = 0 Then
                    msgRet = "No existe el número de sorteo para el juego indicado."
                    Throw New Exception(msgRet)
                    Return msgRet
                End If

                ' Obtiene Ruta Local para el archivo
                FormateaFechaHora(oSorteo.fechaHoraIniReal, _fechaSorteo, _HoraSorteo)

                If Not Path.EndsWith("\") Then
                    Path = Path & "\" & _fechaSorteo
                Else
                    Path = Path & _fechaSorteo
                End If
                If Not Path.EndsWith("\") Then Path = Path & "\"
                FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ -        - Despues de reacomodar el Path ->" & Path & "<-")

                If (Not Path.StartsWith("\\")) And Path.StartsWith("\") Then
                    Path = "\" & Path
                End If
                FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ -        - Despues de emparchar la doble barra Path ->" & Path & "<-")

                ' Obtiene archivo de datos
                dt = oArchDAL.ObtenerExtractoInterJ(idPgmSorteo, General.Jurisdiccion, _archivo, msgRet)
                If dt Is Nothing Then
                    If msgRet.Trim = "" Then msgRet = "Problema al ejecutar oArchDAL.ObtenerExtractoInterJ."
                    FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ NO HAY DATOS -> " & msgRet)
                    Throw New Exception(msgRet)
                    Return ""
                End If


                ArchivoExt = Path & _archivo

                ArchivoCXT = (Path & _archivo).Replace(".xml", ".sha")

                ArchivoZip = (Path & _archivo).Replace(".xml", ".zip")

                archivoGPG = ArchivoZip & ".gpg"

                nombrePaq = _archivo
                nombrePaq = nombrePaq.Replace(".xml", ".zip").Replace(".sha", ".zip")

                ' Si vengo del envio de mail y ya existe el archivo no lo vuelvo a generar.
                ' Caso contrario sí lo re-genero
                If paraEmail And FileSystemHelper.ExisteArchivo(ArchivoExt) Then
                    Return ""
                End If
                ''If paraEmail Then ' Ya deberìan estar generados...
                ''    Return ""
                ''End If

                ' Borro los archivos si existen *****
                FileSystemHelper.BorrarArchivo(ArchivoExt)
                FileSystemHelper.BorrarArchivo(ArchivoCXT)
                FileSystemHelper.BorrarArchivo(ArchivoZip)
                FileSystemHelper.BorrarArchivo(archivoGPG)

                '*** Vuelco los datos a archivos físicos
                ' 1. paso del dt a array
                ReDim _registros(dt.Rows.Count)
                i = 0
                For Each r As DataRow In dt.Rows
                    If i = 0 Then
                        _registros(i) = r("registro").ToString.Trim
                    Else
                        _registros(i) = r("registro")
                    End If
                    i = i + 1

                Next
                dt.Clear()
                dt = Nothing

                ' 2. genero el archivo fisico
                FileSystemHelper.GeneraArchivoTXTDesdeArray(_registros, Path, _archivo, msgRet)

                ' 3. obtengo clave hash 
                strClave = FileSystemHelper.generarSHA512(ArchivoExt)

                ' 4. genero archivo fisico con la clave hash
                ReDim _registros(1)
                _registros(0) = strClave
                _archivo_sha1 = _archivo.Replace(".xml", ".sha")
                FileSystemHelper.GeneraArchivoTXTDesdeArray(_registros, Path, _archivo_sha1, msgRet)

                ' 5. si existe parametro encripto con gpg
                If General.Extr_Interjur_Gpg_Encr.Trim = "S" Then
                    FileSystemHelper.encriptarArchivo(ArchivoExt, "GPG", msgRet, General.Extr_Interjur_Gpg_Path, General.Extr_Interjur_Gpg_Clve)
                    If msgRet <> "" Then
                        FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ -  FIN ERROR   - msgRet ->" & msgRet & "<-")
                        Throw New Exception(" GenerarArchivoExtractoInterJ: " & msgRet)
                        Return msgRet
                    End If
                End If

                ' 6. si existe parametro hago el FTP
                Dim jbo As New JuegoBO
                If General.Extr_Interjur_Ftp_Envi = "S" And jbo.getJuego(oSorteo.idJuego).FtpExtractoUnif Then
                    Dim oSrvFtp As New SrvFTP
                    Dim ftpHlp As New FtpHelper

                    Dim carpeta As String = General.Extr_Interjur_Ftp_Dire

                    Dim lstArch As String()

                    oSrvFtp.Proto = General.Extr_Interjur_Ftp_Prot
                    oSrvFtp.Servidor = General.Extr_Interjur_Ftp_Srvr
                    oSrvFtp.Puerto = General.Extr_Interjur_Ftp_Puer
                    oSrvFtp.Pasivo = General.Extr_Interjur_Ftp_Pasi
                    oSrvFtp.Usr = General.Extr_Interjur_Ftp_User
                    oSrvFtp.Pwd = General.Extr_Interjur_Ftp_Pass
                    oSrvFtp.DirRaiz = General.Extr_Interjur_Ftp_Raiz

                    If Not ftpHlp.existeDirectorio(oSrvFtp, carpeta) Then
                        If Not ftpHlp.creaDirectorio(oSrvFtp, carpeta, msgRet) Then
                            If msgRet.Trim = "" Then msgRet = "Problema en creaDirectorio"
                            FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ -  FIN ERROR   - msgRet ->" & msgRet & "<-")
                            Throw New Exception(" GenerarArchivoExtractoInterJ: " & msgRet)
                            Return msgRet
                        End If
                    End If

                    ReDim lstArch(1)
                    lstArch(0) = Path & "|" & carpeta & "|" & _archivo
                    lstArch(1) = Path & "|" & carpeta & "|" & _archivo_sha1

                    Try
                        ftpHlp.delListaArchivo(lstArch, "|", oSrvFtp, msgRet)
                    Catch exD As Exception
                    Finally
                        msgRet = ""
                    End Try

                    If Not ftpHlp.putListaArchivo(lstArch, "|", oSrvFtp, msgRet) Then
                        If msgRet = "" Then msgRet = "Problema en putListaArchivo"
                        FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ -  FIN ERROR   - msgRet ->" & msgRet & "<-")
                        Throw New Exception(" GenerarArchivoExtractoInterJ: " & msgRet)
                        Return msgRet
                    End If

                End If

                FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ -  FIN OK")
                Return msgRet
            Catch ex As Exception
                msgRet = ex.Message
                FileSystemHelper.Log(" ArchivoBoldtBO.GenerarArchivoExtractoInterJ -  FIN ERROR   - msgRet ->" & msgRet & "<-")
                Throw New Exception(" GenerarArchivoExtractoInterJ: " & ex.Message)
                Return msgRet
            End Try

        End Function

        Public Function ObtenerAutoridadesArchivoBoldt(ByVal idjuego As String, Optional ByVal pidparametro As String = "") As String
            Dim Autoridades As New ExtractoEntities.Autoridad
            Dim lstAutoridades As New List(Of ExtractoEntities.Autoridad)
            Dim resultado As String = ""
            Try
                lstAutoridades = ExtractoData.Autoridad.GetAutoridades(idjuego, General.Jurisdiccion, True, pidparametro)
                For Each oAutoridad In lstAutoridades
                    resultado = resultado & oAutoridad.Cargo.PadRight(30, " ") & oAutoridad.Nombre.PadRight(30, " ")
                Next
                Return resultado
            Catch ex As Exception
                FileSystemHelper.Log("Excepcion ObtenerAutoridadesArchivoBoldt->" & ex.Message)
                Throw New Exception(ex.Message)
            End Try
        End Function

        Public Function ControlarExtractoQuiniBrinco(ByVal idjuego As Long, ByVal nroSorteoactual As String, ByVal pathReportes As String) As Boolean
            Dim extractoBoldt As New cExtractoArchivoBoldt
            Dim ArchivoOrigenExtracto As String = ""
            Dim idpgmconcurso As String = ""
            Dim _valor As Boolean = True
            Try
                ControlarExtractoQuiniBrinco = False
                extractoBoldt = GenerarExtractodesdeArchivo(idjuego, nroSorteoactual, ArchivoOrigenExtracto)
                If Not extractoBoldt Is Nothing Then
                    '** inserta el detalle del extracto en las tablas de auditorias
                    InsertaDetalleExtracto_Auditoria(extractoBoldt)
                    idpgmconcurso = idjuego & nroSorteoactual.PadLeft(6, "0")
                    '** actualiza la tabla de detalle con los datos de la tabla de extraccionesdet para el concurso seleccionado
                    ActualizaDetalleExtracto_Auditoria(idpgmconcurso)
                    '*** controla que los valores del extarcto y de la tabla campo por campo
                    If Not ControlarDetalleExtracto_Auditoria(idpgmconcurso) Then
                        _valor = False
                    End If
                    '*** inserta los datos d ela cabecera en la tabla de auditoria
                    InsertaCabeceraExtracto_Auditoria(ArchivoOrigenExtracto, _valor, extractoBoldt)
                    '** si hay diferncias de valores,se muestra un msj al usuario y se actualzian con los datos del extracto de Boldt
                    If Not _valor Then
                        If MsgBox("Existen diferencias entre los valores cargados en el sistema y los valores generados por Boldt.Se actualizarán los valores con los datos enviados en el Extracto de Boldt.", MsgBoxStyle.Exclamation + MsgBoxStyle.OkCancel) = MsgBoxResult.Cancel Then
                            Exit Function
                        End If
                        ' se actualiza la tabla de extraccionesdet con los valores del extracto de Boldt y se imprime un nuevo listado
                        Actualiza_ExtraccionesDet_con_ExtractoBoldt(idpgmconcurso)
                        'se imprime un listado con los nuevos valores
                        ListarExtracciones(idpgmconcurso, pathReportes)
                    End If
                    ControlarExtractoQuiniBrinco = True
                End If
            Catch ex As Exception
                Throw New Exception("ControlarExtractoQuniBrinco:" & ex.Message)
            End Try
        End Function

        Private Sub ListarExtracciones(ByVal _idpgmconcurso As Long, ByVal pathReportes As String)
            Dim PgmBO As New PgmConcursoBO
            Dim dt As DataTable
            Dim ds As New DataSet

            dt = PgmBO.ObtenerDatosListado(_idpgmconcurso)
            ds.Tables.Add(dt)
            Dim path_reporte As String = pathReportes 'Application.ExecutablePath.Substring(0, Application.ExecutablePath.Length - 14) & "INFORMES" 
            Dim reporte As New Listado
            reporte.GenerarListado(ds, path_reporte)
        End Sub

        Public Function existe_auditoria_archivoextracto(ByVal idpgmsorteo As Long) As Boolean

            Dim oarchivoBoldt As New ArchivoBoldtDAL
            Try

                '*** inserta los datos del extracto de boldt en las tablas de auditoria
                Return oarchivoBoldt.existe_auditoria_archivoextracto(idpgmsorteo)

            Catch ex As Exception
                Throw New Exception(" existe_auditoria_archivoextracto:" & ex.Message)
            End Try

        End Function

        Public Function Generar_archivosExtracto_y_Control_por_WS(ByVal idpgmsorteo As Integer) As Boolean
            Dim svc As New WSCAS.Pgmsorteos

            Dim f As StreamWriter
            Dim nombre_archivo As String
            Dim msj As String = ""
            Dim archivo() As String
            Dim path As String = General.CarpetaOrigenArchivosExtractoBoldt
            Dim path_control As String = ""
            Dim path_extracto As String = ""
            'Dim listaPremio As List(Of Premio)
            'Dim oDal As New Data.PremioDAL
            'listaPremio = oDal.getPremio(idJuego, nroSorteo)
            Try


                Try
                    msj = svc.getlineascontrol(idpgmsorteo)
                Catch ex As Exception
                    FileSystemHelper.Log("Error al Obtner lineas de archivo control WS:" & ex.Message)
                    Return False
                    Exit Function
                End Try
                'si la respuesta es vacia no tiene registro o dio un error hay quesalir
                If msj.Trim = "" Or InStr(UCase(msj), "ERROR") > 0 Then
                    If msj.Trim <> "" Then 'logueo el error
                        FileSystemHelper.Log("Error al obtener linea de archivo control por WS:" & msj)
                    End If
                    Return False
                    Exit Function
                End If

                archivo = Split(msj, ";")
                If Not path.EndsWith("\") Then path = path & "\"
                nombre_archivo = archivo(0)
                path_control = path & nombre_archivo & ".cxt"
                FileSystemHelper.BorrarArchivo(path)
                Try
                    GeneraArchivodeControl(path_control, archivo(0), archivo(1), archivo(2), archivo(3), archivo(4), archivo(5))
                Catch ex As Exception
                    FileSystemHelper.Log("Error al GeneraArchivodeControl WS:" & ex.Message)
                    Return False
                End Try

                Try
                    msj = svc.getlineasextracto(idpgmsorteo)
                Catch ex As Exception
                    Return False
                End Try
                If msj.Trim = "" Or InStr(UCase(msj), "ERROR") > 0 Then
                    If msj.Trim <> "" Then 'logueo el error
                        FileSystemHelper.Log("Error al obtener linea de archivo control por WS:" & msj)
                    End If
                    Return False
                    Exit Function
                End If
                Try
                    path_extracto = path & nombre_archivo & ".ext"
                    FileSystemHelper.BorrarArchivo(path_extracto)
                    f = New StreamWriter(path_extracto, False, UTF8Encoding.ASCII)
                    archivo = Split(msj, ";")
                    For Each linea As String In archivo
                        f.WriteLine(linea.Trim)

                    Next
                    f.Close()
                Catch ex As Exception
                    Return False
                End Try

                '******* genera el zip **************
                Dim _listaArchivos As String()
                ReDim _listaArchivos(2)
                Dim _archivoZip As String = nombre_archivo & ".zip"
                '_listaArchivos = Obtener_Archivos(Path)
                _listaArchivos(0) = path_extracto
                _listaArchivos(1) = path_control
                Dim archZip As String = _archivoZip
                FileSystemHelper.ComprimirListaArchivos(_listaArchivos, path, archZip, msj, False)
                Return True
            Catch ex As Exception
                FileSystemHelper.Log("Error generar_archivosextracto_control_ws:" & ex.Message)
                Return False
            End Try

        End Function

        Public Function GenerarExtractodesdeArchivo_O(ByVal idjuego As Integer, ByVal nrosorteo As Long, ByVal oPgmConcurso As PgmConcurso, ByRef bandera As Integer, Optional ByRef PathArchivoExtracto As String = "") As cExtractoArchivoBoldt

            Dim extr As New cExtractoArchivoBoldt
            Dim f As StreamReader
            Dim pathArchivoExtractoDB As String
            Dim nombreArchivo As String = ""
            Dim ArchivoDestino As String = ""
            Dim Archivocontrol As String = ""
            Dim linea As String = ""
            Dim tiporegistro As String = ""
            Dim _posicion As Integer = 0
            Dim _valor As Long = 0
            Dim _valorSTR As String = ""
            Dim _tipoPremio As Integer = 0
            Dim prefijo As String = ""
            Dim _nrosorteo As String = ""
            Dim archivoOrigen As String = ""
            Dim extension As String = ""
            Dim clave As String = ""
            Dim rta As Integer
            Dim letrasSorteo As String = ""
            Dim horarioSorteoM As String = "150000"
            Dim horarioSorteoN As String = "210000"
            'Dim juego As String = ""

            Try
                'CREA CARPETA por defecto PARA ARCHIVO DE OTRAS JURISDICCIONES, si no existe...
                Dim encontrado As Boolean
                pathArchivoExtractoDB = ""
                encontrado = False
                For Each oSorteo In oPgmConcurso.PgmSorteos
                    If oSorteo.idJuego = 2 Or oSorteo.idJuego = 3 Or oSorteo.idJuego = 8 Then ' solo puede haber oro en vesp y noct, excep en matu por eso la considero
                        For Each oLoteria In oSorteo.ExtraccionesLoteria
                            If oLoteria.Loteria.IdLoteria = "O" And oLoteria.Loteria.path_extracto.Trim.Length > 0 Then
                                If Not (oLoteria.Loteria.path_extracto.Trim.EndsWith("\") Or oLoteria.Loteria.path_extracto.Trim.EndsWith("/")) Then
                                    oLoteria.Loteria.path_extracto = oLoteria.Loteria.path_extracto.Trim & "\"
                                End If
                                If Not (oSorteo.PathLocalJuego.EndsWith("\") Or oSorteo.PathLocalJuego.EndsWith("/")) Then
                                    oSorteo.PathLocalJuego = oSorteo.PathLocalJuego.Trim & "\"
                                End If
                                If (oSorteo.PathLocalJuego.StartsWith("\") Or oSorteo.PathLocalJuego.StartsWith("/")) Then
                                    oSorteo.PathLocalJuego = oSorteo.PathLocalJuego.Trim.Substring(1, oSorteo.PathLocalJuego.Trim.Length - 1)
                                End If
                                pathArchivoExtractoDB = oLoteria.Loteria.path_extracto & oSorteo.PathLocalJuego & oSorteo.nroSorteo.ToString.Trim
                                FileSystemHelper.CrearPath(pathArchivoExtractoDB)
                                idjuego = oSorteo.idJuego
                                extension = oLoteria.Loteria.Extension_arch_Extracto.Trim.Replace(".", "")
                                clave = oLoteria.Loteria.Clave
                                encontrado = True
                                Exit For
                            End If
                        Next
                    End If
                    If encontrado Then Exit For
                Next
                If Not encontrado Then
                    ' Ninguna jurisdiccion tiene lectura por archivo, salgo!
                    Return Nothing
                End If

                ''pathArchivoExtractoDB = GeneralBO.ObtenerCarpetaArchivosExtractoOtrasJuris(oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego, "O")
                If Not pathArchivoExtractoDB.EndsWith("\") Then
                    pathArchivoExtractoDB += "\"
                End If
                ''Select Case Mid(oPgmConcurso.idPgmSorteoPrincipal, 1, 2)
                ''    Case 20
                ''        letrasSorteo = "QUI"
                ''End Select
                letrasSorteo = "QUI"

                If idjuego = 2 Then
                    nombreArchivo = letrasSorteo + oPgmConcurso.fechaHora.Year.ToString + oPgmConcurso.fechaHora.Month.ToString("D2") + oPgmConcurso.fechaHora.Day.ToString("D2") + horarioSorteoN
                    'juego = "Nocturna"
                Else
                    If idjuego = 3 Then
                        nombreArchivo = letrasSorteo + oPgmConcurso.fechaHora.Year.ToString + oPgmConcurso.fechaHora.Month.ToString("D2") + oPgmConcurso.fechaHora.Day.ToString("D2") + horarioSorteoM
                        'juego = "Verpertina"
                    Else
                        If idjuego = 8 Then
                            nombreArchivo = letrasSorteo + oPgmConcurso.fechaHora.Year.ToString + oPgmConcurso.fechaHora.Month.ToString("D2") + oPgmConcurso.fechaHora.Day.ToString("D2") + horarioSorteoM
                            'juego = "Matutina"
                        End If
                    End If
                End If

                'pathArchivoExtractoDB += juego + "\" + nrosorteo.ToString + "\O\"
                PathArchivoExtracto = pathArchivoExtractoDB
                If clave = "" Then
                    archivoOrigen = pathArchivoExtractoDB & nombreArchivo & ".zip"
                Else
                    archivoOrigen = pathArchivoExtractoDB & nombreArchivo & "." & extension & ".gpg"
                End If


                If Not File.Exists(archivoOrigen) Then
                    bandera = 1
                    Return Nothing
                End If

                'desencriptar el archivo 
                If clave = "" Then
                    FileSystemHelper.Descomprimir(pathArchivoExtractoDB, archivoOrigen)
                Else
                    GeneralBO.desencriptararchivoGPG(archivoOrigen, pathArchivoExtractoDB + nombreArchivo + ".ext", clave)
                    Thread.Sleep(5000)
                End If

                archivoOrigen = pathArchivoExtractoDB & nombreArchivo + ".ext"

                If Not File.Exists(archivoOrigen) Then
                    'MsgBox("No se encontró el archivo de extracto.", MsgBoxStyle.Exclamation)
                    bandera = 1
                    Return Nothing
                End If

                ArchivoDestino = pathArchivoExtractoDB & nombreArchivo & ".ext"

                extr.Autoridad_1 = New libEntities.Entities.Autoridad
                extr.Autoridad_2 = New libEntities.Entities.Autoridad
                extr.Autoridad_3 = New libEntities.Entities.Autoridad
                extr.Autoridad_4 = New libEntities.Entities.Autoridad
                extr.Autoridad_5 = New libEntities.Entities.Autoridad

                extr.Numero_1 = New cValorPosicion
                extr.Numero_2 = New cValorPosicion
                extr.Numero_3 = New cValorPosicion
                extr.Numero_4 = New cValorPosicion
                extr.Numero_5 = New cValorPosicion
                extr.Numero_6 = New cValorPosicion
                extr.Numero_7 = New cValorPosicion
                extr.Numero_8 = New cValorPosicion
                extr.Numero_9 = New cValorPosicion
                extr.Numero_10 = New cValorPosicion
                extr.Numero_11 = New cValorPosicion
                extr.Numero_12 = New cValorPosicion
                extr.Numero_13 = New cValorPosicion
                extr.Numero_14 = New cValorPosicion
                extr.Numero_15 = New cValorPosicion
                extr.Numero_16 = New cValorPosicion
                extr.Numero_17 = New cValorPosicion
                extr.Numero_18 = New cValorPosicion
                extr.Numero_19 = New cValorPosicion
                extr.Numero_20 = New cValorPosicion

                'comienza a leer el archivo 
                f = New StreamReader(ArchivoDestino)

                While Not f.EndOfStream
                    linea = f.ReadLine()

                    'VALIDAR QUE CON LA LETRA SI CORRECPONDE EL JUEGO SINO DAR UN MENSAJE. 
                    ' falta el cmapo de horario V verpertino, N nocturno me parece que es el dropdown 

                    If Mid(linea, 18, 1) = "N" Then
                        If oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 3 Or oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 8 Then
                            rta = MsgBox("Se esta sorteando Vespertina pero el archivo corresponde a Nocturna. Procesar igual?", MsgBoxStyle.YesNo)
                            If rta = vbNo Then
                                Return Nothing
                            End If
                        End If
                    ElseIf Mid(linea, 18, 1) = "V" Then
                        If oPgmConcurso.concurso.JuegoPrincipal.Juego.IdJuego = 2 Then
                            rta = MsgBox("Se esta sorteando Nocturna pero el archivo corresponde a Vespertina. Procesar igual?", MsgBoxStyle.YesNo)
                            If rta = vbNo Then
                                Return Nothing
                            End If
                        End If
                    End If

                    extr.Juego = idjuego
                    extr.NumeroSorteo = 0
                    extr.FechaHoraSorteo = FormateaFechaHoraaExtracto(Mid(linea, 1, 8), Mid(linea, 9, 6))
                    extr.FechaHoraCaducidad = FormateaFechaHoraaExtracto(Mid(linea, 19, 8), Mid(linea, 27, 6))
                    extr.HoraIniLoteria = Mid(linea, 9, 4)
                    extr.HoraFinLoteria = Mid(linea, 27, 4)

                    If Not General.ValidarFechas(extr.FechaHoraSorteo, oPgmConcurso.fechaHoraIniReal) Then
                        f.Dispose()
                        MsgBox("Validacion archivo: La fecha inicio del sorteo no coicide con la fecha del archivo", MsgBoxStyle.Exclamation)
                        Return Nothing
                    End If

                    'If Not Util.ValidarFechas(extr.FechaHoraCaducidad, oPgmConcurso.fechaHoraFinReal) Then
                    '    f.Dispose()
                    '    MsgBox("Validacion archivo: La fecha fin del sorteo no coicide con la fecha del archivo", MsgBoxStyle.Exclamation)
                    '    Return Nothing
                    'End If

                    If Not General.ValidaDesdeHasta(extr.FechaHoraSorteo, extr.FechaHoraCaducidad) Then
                        f.Dispose()
                        MsgBox("Validacion archivo: La fecha de incio es mayor a la fecha de finalización", MsgBoxStyle.Exclamation)
                        Return Nothing
                    End If

                    extr.Juego = Mid(linea, 15, 3)
                    extr.Cifras = 3

                    'Numeros sorteo
                    _valor = Mid(linea, 33, 3)
                    _valorSTR = Right(Trim(Mid(linea, 33, 3)), extr.Cifras)
                    extr.Numero_1.Posicion = 1
                    extr.Numero_1.Valor = _valor
                    extr.Numero_1.ValorSTR = _valorSTR

                    _valor = Mid(linea, 36, 3)
                    _valorSTR = Right(Trim(Mid(linea, 36, 3)), extr.Cifras)
                    extr.Numero_2.Posicion = 2
                    extr.Numero_2.Valor = _valor
                    extr.Numero_2.ValorSTR = _valorSTR

                    _valor = Mid(linea, 39, 3)
                    _valorSTR = Right(Trim(Mid(linea, 39, 3)), extr.Cifras)
                    extr.Numero_3.Posicion = 3
                    extr.Numero_3.Valor = _valor
                    extr.Numero_3.ValorSTR = _valorSTR

                    _valor = Mid(linea, 42, 3)
                    _valorSTR = Right(Trim(Mid(linea, 42, 3)), extr.Cifras)
                    extr.Numero_4.Posicion = 4
                    extr.Numero_4.Valor = _valor
                    extr.Numero_4.ValorSTR = _valorSTR

                    _valor = Mid(linea, 45, 3)
                    _valorSTR = Right(Trim(Mid(linea, 45, 3)), extr.Cifras)
                    extr.Numero_5.Posicion = 5
                    extr.Numero_5.Valor = _valor
                    extr.Numero_5.ValorSTR = _valorSTR

                    _valor = Mid(linea, 48, 3)
                    _valorSTR = Right(Trim(Mid(linea, 48, 3)), extr.Cifras)
                    extr.Numero_6.Posicion = 6
                    extr.Numero_6.Valor = _valor
                    extr.Numero_6.ValorSTR = _valorSTR

                    _valor = Mid(linea, 51, 3)
                    _valorSTR = Right(Trim(Mid(linea, 51, 3)), extr.Cifras)
                    extr.Numero_7.Posicion = 7
                    extr.Numero_7.Valor = _valor
                    extr.Numero_7.ValorSTR = _valorSTR

                    _valor = Mid(linea, 54, 3)
                    _valorSTR = Right(Trim(Mid(linea, 54, 3)), extr.Cifras)
                    extr.Numero_8.Posicion = 8
                    extr.Numero_8.Valor = _valor
                    extr.Numero_8.ValorSTR = _valorSTR

                    _valor = Mid(linea, 57, 3)
                    _valorSTR = Right(Trim(Mid(linea, 57, 3)), extr.Cifras)
                    extr.Numero_9.Posicion = 9
                    extr.Numero_9.Valor = _valor
                    extr.Numero_9.ValorSTR = _valorSTR

                    _valor = Mid(linea, 60, 3)
                    _valorSTR = Right(Trim(Mid(linea, 60, 3)), extr.Cifras)
                    extr.Numero_10.Posicion = 10
                    extr.Numero_10.Valor = _valor
                    extr.Numero_10.ValorSTR = _valorSTR

                    _valor = Mid(linea, 63, 3)
                    _valorSTR = Right(Trim(Mid(linea, 63, 3)), extr.Cifras)
                    extr.Numero_11.Posicion = 11
                    extr.Numero_11.Valor = _valor
                    extr.Numero_11.ValorSTR = _valorSTR

                    _valor = Mid(linea, 66, 3)
                    _valorSTR = Right(Trim(Mid(linea, 66, 3)), extr.Cifras)
                    extr.Numero_12.Posicion = 12
                    extr.Numero_12.Valor = _valor
                    extr.Numero_12.ValorSTR = _valorSTR

                    _valor = Mid(linea, 69, 3)
                    _valorSTR = Right(Trim(Mid(linea, 69, 3)), extr.Cifras)
                    extr.Numero_13.Posicion = 13
                    extr.Numero_13.Valor = _valor
                    extr.Numero_13.ValorSTR = _valorSTR

                    _valor = Mid(linea, 72, 3)
                    _valorSTR = Right(Trim(Mid(linea, 72, 3)), extr.Cifras)
                    extr.Numero_14.Posicion = 14
                    extr.Numero_14.Valor = _valor
                    extr.Numero_14.ValorSTR = _valorSTR

                    _valor = Mid(linea, 75, 3)
                    _valorSTR = Right(Trim(Mid(linea, 75, 3)), extr.Cifras)
                    extr.Numero_15.Posicion = 15
                    extr.Numero_15.Valor = _valor
                    extr.Numero_15.ValorSTR = _valorSTR

                    _valor = Mid(linea, 78, 3)
                    _valorSTR = Right(Trim(Mid(linea, 78, 3)), extr.Cifras)
                    extr.Numero_16.Posicion = 16
                    extr.Numero_16.Valor = _valor
                    extr.Numero_16.ValorSTR = _valorSTR

                    _valor = Mid(linea, 81, 3)
                    _valorSTR = Right(Trim(Mid(linea, 81, 3)), extr.Cifras)
                    extr.Numero_17.Posicion = 17
                    extr.Numero_17.Valor = _valor
                    extr.Numero_17.ValorSTR = _valorSTR

                    _valor = Mid(linea, 84, 3)
                    _valorSTR = Right(Trim(Mid(linea, 84, 3)), extr.Cifras)
                    extr.Numero_18.Posicion = 18
                    extr.Numero_18.Valor = _valor
                    extr.Numero_18.ValorSTR = _valorSTR

                    _valor = Mid(linea, 87, 3)
                    _valorSTR = Right(Trim(Mid(linea, 87, 3)), extr.Cifras)
                    extr.Numero_19.Posicion = 19
                    extr.Numero_19.Valor = _valor
                    extr.Numero_19.ValorSTR = _valorSTR

                    _valor = Mid(linea, 90, 3)
                    _valorSTR = Right(Trim(Mid(linea, 90, 3)), extr.Cifras)
                    extr.Numero_20.Posicion = 20
                    extr.Numero_20.Valor = _valor
                    extr.Numero_20.ValorSTR = _valorSTR

                End While
                f.Dispose()
                Return extr
            Catch ex As Exception
                Throw New Exception(" GenerarArchivoExtractoOtraJuris:" & ex.Message)
            End Try

        End Function

    End Class

End Namespace