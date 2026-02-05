Imports Microsoft.VisualBasic
Imports System.Data.SqlClient
Imports System.Configuration

Public Class General

    Private Shared gConn As SqlConnection
    Private Shared gConnRem As SqlConnection

    Private Shared gConnD As SqlConnection

    Private Shared _connStr As String = ""
    Private Shared _connStrRem As String = ""

    Private Shared _pathIni As String = ""
    Private Shared _archivoLog As String = ""
    Private Shared _version As String = ""
    ' parametros publicador
    Private Shared _ModoPublicacion As String = ""
    Private Shared _PublicaDisplay As String = ""
    Private Shared _PublicaWeb As String = ""
    '**** parametros mail
    Private Shared _MailHost As String = ""
    Private Shared _MailPort As String = ""
    Private Shared _MailFrom As String = ""
    Private Shared _MailSep As String = ""
    Private Shared _ListaEnvioExtracto As String = ""

    '** 17/10/2012
    Private Shared _prefijoPozo As String = ""
    Private Shared _prefijoPremio As String = ""
    Private Shared _PathPremiosDestino As String = ""
    Private Shared _PathPozoDestino As String = ""
    '**22/10/2012
    Private Shared _pozoEstimadoPoceada As String = ""
    '**29/10/2012
    Private Shared _CtrMD5Premios As String = ""
    Private Shared _CtrMD5Pozos As String = ""
    Private Shared _CarpetaOrigenArchivosExtractoBoldt As String = ""
    Private Shared _CarpetaDestinoArchivosExtractoBoldt As String = ""
    Private Shared _prefijoSueldo As String = ""
    Private Shared _cantidadIntentos As String = ""

    '**** parametros para Publicacion*********
    Private Shared _PublicarWebON As String = ""
    Private Shared _PublicarWebOFF As String = ""
    Private Shared _PublicaExtractosWSRestON As String = ""
    Private Shared _PublicaExtractosWSRestOFF As String = ""
    Private Shared _Jurisdiccion As String = ""

    '*** Parametros archivos para Boldt
    Private Shared _carpetaArchivosBoldt As String = ""
    Private Shared _letraPciaArchivosboldt As String = ""
    Private Shared _nroPciaArchivosboldt As String = ""
    Private Shared _carpetaExtractoBoltConfirmado As String = ""

    Private Shared _pathInformes As String = ""
    Private Shared _loginHabilitado As String = ""
    Private Shared _extractoAMedios As String = ""

    '***26/11/2013
    Private Shared _cantidadMaximaJurisdicciones As String = ""
    '***27/06/2014 servidor ftp
    Private Shared _servidorFTP As String = ""
    Private Shared _usuarioFTP As String = ""
    Private Shared _pwdFTP As String = ""
    Private Shared _enviarFTP As String = ""
    Private Shared _encriptarGPG As String = ""
    Private Shared _pathGPG As String = ""
    Private Shared _claveGPG As String = ""
    Private Shared _Modo_Operacion As String = ""
    Private Shared _CarpetaOrigenArchivosExtractoComparar As String = ""
    Private Shared _Carpeta_Bkp As String = ""
    Private Shared _Carpeta_Bkp_Rem As String = ""
    Private Shared _Carpeta_Bd_Rem As String = ""
    Private Shared _Obtener_pgmsorteos_ws As String = ""
    Private Shared _Url_extractos As String = ""
    Private Shared _carpetaFTPExtractos As String = ""
    Private Shared _DiasSorteosAnteriores As String = ""
    Private Shared _ListarReporteDetalleParametros As String = ""
    Private Shared _urlRest As String = ""
    Private Shared _prefijoRPTExtracto As String = ""
    Private Shared _Origen As String = ""
    Private Shared _calculaPozosProximo As String = ""

    '26/04/2017
    Private Shared _CarpetaOrigenArchivosExtractoOtrasJuris As String = ""
    Private Shared _CarpetaDestinoArchivosExtractoOtrasJuris As String = ""

    ' Variables para Extracto Interjurisdiccional
    Private Shared _extr_interjur As String = ""
    Private Shared _extr_interjur_CTR_SHA1 As String = ""

    Private Shared _extr_interjur_ftp_envi As String = ""
    Private Shared _extr_interjur_ftp_prot As String = ""
    Private Shared _extr_interjur_ftp_srvr As String = ""
    Private Shared _extr_interjur_ftp_puer As String = ""
    Private Shared _extr_interjur_ftp_pasi As Boolean = True
    Private Shared _extr_interjur_ftp_ssl As Boolean = True

    Private Shared _extr_interjur_ftp_user As String = ""
    Private Shared _extr_interjur_ftp_pass As String = ""
    Private Shared _extr_interjur_ftp_raiz As String = ""
    Private Shared _extr_interjur_ftp_dire As String = ""

    Private Shared _extr_interjur_gpg_encr As String = ""
    Private Shared _extr_interjur_gpg_path As String = ""
    Private Shared _extr_interjur_gpg_clve As String = ""

    ' -----------------------------------------------------------------
    ' Extracto INTERJURISDICCIONAL - Parametros FTP

    Private Shared _jurisdiccionesObligatorias As String = ""

    Public Shared Property JurisdiccionesObligatorias() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                strCnStr = "S"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 26).ToLower = "jurisdiccionesobligatorias" Then
                        strCnStr = linea.Substring(27, linea.Length - 27)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _jurisdiccionesObligatorias = "S"
            Else
                _jurisdiccionesObligatorias = strCnStr.Trim
            End If

            Return _jurisdiccionesObligatorias

        End Get
        Set(ByVal value As String)
            _jurisdiccionesObligatorias = value
        End Set
    End Property

    Public Shared Property Extr_interjur_CTR_SHA1() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ctr_sha1" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_CTR_SHA1 = "C:\"
            Else
                _extr_interjur_CTR_SHA1 = strCnStr.Trim
            End If

            Return _extr_interjur_CTR_SHA1

        End Get
        Set(ByVal value As String)
            _extr_interjur_CTR_SHA1 = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Envi() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_envi" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_envi = "C:\"
            Else
                _extr_interjur_ftp_envi = strCnStr.Trim
            End If

            Return _extr_interjur_ftp_envi

        End Get
        Set(ByVal value As String)
            _extr_interjur_ftp_envi = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Prot() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_prot" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_prot = "C:\"
            Else
                _extr_interjur_ftp_prot = strCnStr.Trim
            End If

            Return _extr_interjur_ftp_prot

        End Get
        Set(ByVal value As String)
            _extr_interjur_ftp_prot = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Srvr() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_srvr" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_srvr = "C:\"
            Else
                _extr_interjur_ftp_srvr = strCnStr.Trim
            End If

            Return _extr_interjur_ftp_srvr

        End Get
        Set(ByVal value As String)
            _extr_interjur_ftp_srvr = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Puer() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_puer" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_puer = "C:\"
            Else
                _extr_interjur_ftp_puer = strCnStr.Trim
            End If

            Return _extr_interjur_ftp_puer

        End Get
        Set(ByVal value As String)
            _extr_interjur_ftp_puer = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_User() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_user" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_user = "C:\"
            Else
                _extr_interjur_ftp_user = strCnStr.Trim
            End If

            Return _extr_interjur_ftp_user

        End Get
        Set(ByVal value As String)
            _extr_interjur_ftp_user = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Pasi() As Boolean
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _extr_interjur_ftp_pasi = True
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_pasi" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_pasi = True
            Else
                _extr_interjur_ftp_pasi = IIf(strCnStr.Trim.ToUpper = "N", False, True)
            End If

            Return _extr_interjur_ftp_pasi

        End Get
        Set(ByVal value As Boolean)
            _extr_interjur_ftp_pasi = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Ssl() As Boolean
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _extr_interjur_ftp_ssl = True
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 21).ToLower = "extr_interjur_ftp_ssl" Then
                        strCnStr = linea.Substring(22, linea.Length - 22)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_ssl = True
            Else
                _extr_interjur_ftp_ssl = IIf(strCnStr.Trim.ToUpper = "N", False, True)
            End If

            Return _extr_interjur_ftp_ssl

        End Get
        Set(ByVal value As Boolean)
            _extr_interjur_ftp_ssl = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Pass() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = ""
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_pass" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_pass = ""
            Else
                _extr_interjur_ftp_pass = strCnStr.Trim
            End If

            Return _extr_interjur_ftp_pass

        End Get
        Set(ByVal value As String)
            _extr_interjur_ftp_pass = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Raiz() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "/"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_raiz" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_raiz = "/"
            Else
                _extr_interjur_ftp_raiz = strCnStr.Trim
            End If

            Return _extr_interjur_ftp_raiz

        End Get
        Set(ByVal value As String)
            _extr_interjur_ftp_raiz = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Ftp_Dire() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "/"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_ftp_dire" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_ftp_dire = "/"
            Else
                _extr_interjur_ftp_dire = strCnStr
            End If

            Return _extr_interjur_ftp_dire.Trim

        End Get
        Set(ByVal value As String)
            _extr_interjur_ftp_dire = value
        End Set
    End Property

    ' -----------------------------------------------------------------
    ' Extracto INTERJURISDICCIONAL - Parametros GPG

    Public Shared Property Extr_Interjur_Gpg_Encr() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_gpg_encr" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_gpg_encr = "C:\"
            Else
                _extr_interjur_gpg_encr = strCnStr.Trim
            End If

            Return _extr_interjur_gpg_encr

        End Get
        Set(ByVal value As String)
            _extr_interjur_gpg_encr = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Gpg_Path() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 22).ToLower = "extr_interjur_gpg_path" Then
                        strCnStr = linea.Substring(23, linea.Length - 23)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_gpg_path = "C:\"
            Else
                _extr_interjur_gpg_path = strCnStr.Trim
            End If

            Return _extr_interjur_gpg_path

        End Get
        Set(ByVal value As String)
            _extr_interjur_gpg_path = value
        End Set
    End Property

    Public Shared Property Extr_Interjur_Gpg_Clve() As String
        Get
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 11).ToLower = "extr_interjur_gpg_clve" Then
                        strCnStr = linea.Substring(22, linea.Length - 22)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur_gpg_clve = "C:\"
            Else
                _extr_interjur_gpg_clve = strCnStr.Trim
            End If

            Return _extr_interjur_gpg_clve

        End Get
        Set(ByVal value As String)
            _extr_interjur_gpg_clve = value
        End Set
    End Property

    ' ----------------------------------------------------------------

    Public Shared Property Carpeta_Bd_Rem() As String
        Get
            'If _Carpeta_Bd_Rem = "" Then
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bd_Rem = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 14).ToLower = "carpeta_bd_rem" Then
                        strCnStr = linea.Substring(15, linea.Length - 15)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _Carpeta_Bd_Rem = "C:\"
            Else
                _Carpeta_Bd_Rem = strCnStr

            End If
            'End If
            Return _Carpeta_Bd_Rem

        End Get
        Set(ByVal value As String)
            _Carpeta_Bd_Rem = value
        End Set
    End Property

    Public Shared Property Carpeta_Bkp() As String
        Get
            'If _Carpeta_Bkp = "" Then
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 11).ToLower = "carpeta_bkp" Then
                        strCnStr = linea.Substring(12, linea.Length - 12)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _Carpeta_Bkp = "C:\"
            Else
                _Carpeta_Bkp = strCnStr

            End If
            'End If
            Return _Carpeta_Bkp

        End Get
        Set(ByVal value As String)
            _Carpeta_Bkp = value
        End Set
    End Property

    Public Shared Property Carpeta_Bkp_Rem() As String
        Get
            'If _Carpeta_Bkp_Rem = "" Then
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp_Rem = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 15).ToLower = "carpeta_bkp_rem" Then
                        strCnStr = linea.Substring(16, linea.Length - 16)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _Carpeta_Bkp_Rem = "C:\"
            Else
                _Carpeta_Bkp_Rem = strCnStr

            End If
            'End If
            Return _Carpeta_Bkp_Rem

        End Get
        Set(ByVal value As String)
            _Carpeta_Bkp_Rem = value
        End Set
    End Property

    Public Shared Property CarpetaOrigenArchivosExtractoComparar() As String
        Get
            'If _CarpetaOrigenArchivosExtractoComparar = "" Then
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _CarpetaOrigenArchivosExtractoComparar = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 37).ToLower = "carpetaorigenarchivosextractocomparar" Then
                        strCnStr = linea.Substring(38, linea.Length - 38)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _CarpetaOrigenArchivosExtractoComparar = "C:\"
            Else
                _CarpetaOrigenArchivosExtractoComparar = strCnStr

            End If
            'End If
            Return _CarpetaOrigenArchivosExtractoComparar

        End Get
        Set(ByVal value As String)
            _CarpetaOrigenArchivosExtractoComparar = value
        End Set
    End Property

    Public Shared Property Modo_Operacion() As String
        Get

            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Modo_Operacion = ""
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 14).ToLower = "modo_operacion" Then
                        strCnStr = linea.Substring(15, linea.Length - 15).ToUpper
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _Modo_Operacion = ""
            Else
                _Modo_Operacion = strCnStr
            End If

            Return _Modo_Operacion
        End Get
        Set(ByVal value As String)
            _Modo_Operacion = value
        End Set
    End Property

    Public Shared Property ExtractoAMedios() As String
        Get
            If _extractoAMedios = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _extractoAMedios = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 15).ToLower = "extractoamedios" Then
                            strCnStr = linea.Substring(16, linea.Length - 16)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _extractoAMedios = "N"
                Else
                    _extractoAMedios = strCnStr
                End If
            End If
            Return _extractoAMedios
        End Get
        Set(ByVal value As String)
            _extractoAMedios = value
        End Set
    End Property

    Public Shared Property LoginHabilitado() As String
        Get
            If _loginHabilitado = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _loginHabilitado = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 15).ToLower = "loginhabilitado" Then
                            strCnStr = linea.Substring(16, linea.Length - 16)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _loginHabilitado = "N"
                Else
                    _loginHabilitado = strCnStr
                End If
            End If
            Return _loginHabilitado
        End Get
        Set(ByVal value As String)
            _loginHabilitado = value
        End Set
    End Property

    Public Shared Property PathInformes() As String
        Get
            If _pathInformes = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _pathInformes = "c:"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 12).ToLower = "pathinformes" Then
                            strCnStr = linea.Substring(13, linea.Length - 13)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _pathInformes = "c:"
                Else
                    _pathInformes = strCnStr
                End If
            End If
            Return _pathInformes
        End Get
        Set(ByVal value As String)
            _pathInformes = value
        End Set
    End Property

    Public Shared Property ArchivoLog() As String
        Get
            If _archivoLog = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _archivoLog = "c:\sorteos.log"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 10).ToLower = "archivolog" Then
                            strCnStr = linea.Substring(11, linea.Length - 11)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _archivoLog = "c:\sorteos.log"
                Else
                    _archivoLog = strCnStr
                End If
            End If
                Return _archivoLog
        End Get
        Set(ByVal value As String)
            _archivoLog = value
        End Set
    End Property

    Public Shared Property PathIni() As String
        Get
            Return _pathIni
        End Get
        Set(ByVal value As String)
            _pathIni = value
        End Set
    End Property

    Public Shared Property ConnStringRem() As String
        Get
            'If _connStrRem = "" Then
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                strCnStr = "Data Source=sqlsrv02;Initial Catalog=dev_sorteoscas;User ID=data;Password=cpc;Connect Timeout=120;"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If
                    If Left(linea, 19).ToLower() = "connectionstringrem" Then
                        strCnStr = linea.Substring(21, linea.Length - 22)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If

            _connStrRem = strCnStr
            'End If
            Return _connStrRem

        End Get
        Set(ByVal value As String)
            _connStrRem = value
        End Set
    End Property

    Public Shared Property ConnString() As String
        Get
            If _connStr = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    strCnStr = "Data Source=sqlsrv02\sqlexpress;Initial Catalog=dev_sorteoscas;User ID=data;Password=cpc;Connect Timeout=120;"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 16) = "ConnectionString" Then
                            strCnStr = linea.Substring(18, linea.Length - 19)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If

                _connStr = strCnStr
            End If
            Return _connStr

        End Get
        Set(ByVal value As String)
            _connStr = value
        End Set
    End Property

    Public Shared Property ConnStringDisplay() As String
        Get
            If _connStr = "" Then
                Return "Data Source=sqlsrv02;Initial Catalog=dev_displaycas;User ID=data;Password=cpc;Connect Timeout=120;"
            Else
                Return _connStr
            End If

        End Get
        Set(ByVal value As String)
            _connStr = value
        End Set
    End Property

    Public Shared Function Iniciar_Conexion_Rem() As String
        Try
            If gConnRem Is Nothing Then
                gConnRem = New SqlConnection
                gConnRem.ConnectionString = ConnStringRem
                gConnRem.Open()

            Else
                If gConnRem.State = Data.ConnectionState.Closed Then
                    gConnRem.ConnectionString = ConnStringRem
                    gConnRem.Open()

                End If
            End If

            Return ""

        Catch ex As SqlException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.ToString
        End Try
    End Function

    Public Shared Function Obtener_Conexion_Rem() As SqlConnection
        Try
            Dim mensaje As String = Iniciar_Conexion_Rem()
            If mensaje = "" Then
                Return gConnRem
            Else
                Throw New Exception(mensaje)
            End If
        Catch ex As Exception
            Throw New Exception("No se puede iniciar la conexión a la base de resguardo: " & ex.Message)
            FileSystemHelper.Log(" No se puede iniciar la conexión a la base de resguardo: " & ex.Message)
        End Try
    End Function

    Public Shared Function Iniciar_Conexion() As String
        Try
            If gConn Is Nothing Then
                gConn = New SqlConnection
                gConn.ConnectionString = ConnString 'ConfigurationManager.ConnectionStrings("cnE").ConnectionString
                gConn.Open()

            Else
                If gConn.State = Data.ConnectionState.Closed Then
                    gConn.ConnectionString = ConnString 'ConfigurationManager.ConnectionStrings("cnE").ConnectionString
                    gConn.Open()

                End If
            End If

            Return ""

        Catch ex As SqlException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.ToString
        End Try
    End Function

    Public Shared Function Obtener_Conexion() As SqlConnection
        Try
            Dim mensaje As String = Iniciar_Conexion()
            If mensaje = "" Then
                Return gConn
            Else
                Throw New Exception(mensaje)
            End If
        Catch ex As Exception
            Throw New Exception("No se puede iniciar la conexión: " & ex.Message)
            FileSystemHelper.Log(" No se puede iniciar la conexión: " & ex.Message)
        End Try
    End Function

    Public Shared Function Cerrar_Conexion() As String
        Try
            If Not gConn Is Nothing Then
                If gConn.State = Data.ConnectionState.Open Then
                    gConn.Close()
                End If
            End If
            gConn = Nothing
            Return ""
        Catch ex As SqlException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.Message
        End Try

    End Function

    Private Shared Function Setear_Charset_Conexion(ByRef conn As SqlConnection, ByVal pCharset As String, ByVal pCollate As String) As String
        Try
            Dim vsql As String = ""
            vsql = "SET NAMES " & pCharset & ""

            If pCollate <> "" Then
                vsql = vsql & " COLLATE '" & pCollate & "'"
            End If

            Dim command As SqlCommand = conn.CreateCommand
            command.Connection = conn
            command.CommandText = vsql
            command.ExecuteNonQuery()

            Return ""

        Catch ex As SqlException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.Message
        End Try

    End Function

    Public Shared Function Clonar_Conexion() As SqlConnection
        Dim conn As SqlConnection = New SqlConnection
        conn.ConnectionString = ConnString 'ConfigurationManager.ConnectionStrings("db").ConnectionString
        conn.Open()
        Return conn
    End Function

    Public Shared Function Iniciar_Conexion_Display() As String
        Try
            If gConnD Is Nothing Then
                gConnD = New SqlConnection
                gConnD.ConnectionString = ConnStringDisplay 'ConfigurationManager.ConnectionStrings("cnD").ConnectionString
                gConnD.Open()
            Else
                If gConnD.State = Data.ConnectionState.Closed Then
                    gConnD.ConnectionString = ConnStringDisplay 'ConfigurationManager.ConnectionStrings("cnD").ConnectionString
                    gConnD.Open()
                End If
            End If

            Return ""

        Catch ex As SqlException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.ToString
        End Try
    End Function

    Public Shared Function Obtener_Conexion_Display() As SqlConnection
        Try
            Dim mensaje As String = Iniciar_Conexion_Display()
            If mensaje = "" Then
                Return gConnD
            Else
                Throw New Exception(mensaje)
            End If
        Catch ex As Exception
            Throw New Exception("No se puede iniciar la conexión con el servidor del Display: " & ex.Message)
        End Try
    End Function

    Public Shared Function Cerrar_Conexion_Display() As String
        Try
            If Not gConnD Is Nothing Then
                If gConnD.State = Data.ConnectionState.Open Then
                    gConnD.Close()
                End If
            End If
            Return ""
        Catch ex As SqlException
            Return "Error: " & ex.Message
        Catch ex2 As Exception
            Return "Error: " & ex2.Message
        End Try

    End Function

    Public Shared Function Es_Nulo(Of T)(ByVal pNulo As Object, ByVal pValorDevuelto As T) As T
        If DBNull.Value.Equals(pNulo) Or pNulo Is Nothing Then
            Return pValorDevuelto
        Else
            'If TypeOf pNulo Is MySql.Data.Types.MySqlDateTime Then
            'If pNulo.year > 0 Then
            'Return CType(pNulo.value, T)
            'Else
            'Return CType(Nothing, T)
            'End If
            'Else
            Return CType(pNulo, T)
            'End If
        End If
    End Function

    Public Enum MetodoIngreso
        digitacionSimple = 1
        digitacionDobleTecladoSimple = 2
        digitacionSimpleTecladoDoble = 3
        lecturaArchivo = 4
        lecturaBolilleros = 5
    End Enum
    Public Enum CriterioFinExtraccion
        CantMaxExtraccionesAlcanzadas = 1
        ExtraccionesDiferentesEnRondaAnt = 2
    End Enum
    ''' <summary>
    ''' Solo permite el Ingreso de letras, espacios y teclas de control,mas numeros de 1a 9 y los caracteres '.-_()'
    ''' </summary>
    Public Shared Sub SoloLetras(ByRef pSender As Object, ByRef pEv As System.Windows.Forms.KeyPressEventArgs)
        If (Char.IsLetter(pEv.KeyChar) Or Char.IsControl(pEv.KeyChar) Or Char.IsSeparator(pEv.KeyChar) Or Char.IsDigit(pEv.KeyChar) Or (InStr(".-_()", pEv.KeyChar) > 0)) Then
            pEv.Handled = False
        Else
            pEv.Handled = True
        End If
    End Sub

    ''' <summary>
    ''' Solo permite el Ingreso de numeros
    ''' </summary>
    Public Shared Sub SoloNumeros(ByRef pSender As Object, ByRef pEv As System.Windows.Forms.KeyPressEventArgs)
        If (Char.IsDigit(pEv.KeyChar) Or Char.IsControl(pEv.KeyChar)) Then
            pEv.Handled = False
        Else
            pEv.Handled = True
        End If
    End Sub
    Public Shared Sub SoloLetrasNacional(ByRef pSender As Object, ByRef pEv As System.Windows.Forms.KeyPressEventArgs)
        If (Char.IsLetter(pEv.KeyChar) Or Char.IsControl(pEv.KeyChar)) Then
            pEv.Handled = False
        Else
            pEv.Handled = True
        End If
    End Sub
    Public Shared Sub SoloNumerosDecimales(ByRef pSender As Object, ByRef pEv As System.Windows.Forms.KeyPressEventArgs)
        If (Char.IsDigit(pEv.KeyChar) Or Char.IsControl(pEv.KeyChar) Or (InStr(",.", pEv.KeyChar) > 0)) Then
            pEv.Handled = False
        Else
            pEv.Handled = True
        End If
    End Sub
    Public Shared Sub SoloNumerosDecimalesConSigno(ByRef pSender As Object, ByRef pEv As System.Windows.Forms.KeyPressEventArgs)
        If (Char.IsDigit(pEv.KeyChar) Or Char.IsControl(pEv.KeyChar) Or (InStr(",.-", pEv.KeyChar) > 0)) Then
            pEv.Handled = False
        Else
            pEv.Handled = True
        End If
    End Sub
    Public Shared Sub SoloNumerosDecimalesSinMiles(ByRef pSender As Object, ByRef pEv As System.Windows.Forms.KeyPressEventArgs)
        If (Char.IsDigit(pEv.KeyChar) Or Char.IsControl(pEv.KeyChar) Or (InStr(",", pEv.KeyChar) > 0)) Then
            pEv.Handled = False
        Else
            pEv.Handled = True
        End If
    End Sub
    Public Shared Function ConvertirNro(ByVal numero As String) As String
        Dim valor As String = ""
        Dim separadordec As String
        Try


            'se  obtiene el separaddor decimal d ela configuracion regional para poder formatear correctamente el numero
            separadordec = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

            If separadordec = "." Then
                valor = Replace(numero, ",", ".")
            Else
                valor = Replace(numero, ".", ",")
            End If
            Return valor
        Catch ex As Exception
            MsgBox("Problema al convertir Numero:" & ex.Message)
            Return valor
        End Try
    End Function
    Public Shared Function ConvertirNroentero(ByVal numero As String) As String
        Dim valor As String = ""
        Dim separadordec As String
        Dim arreglo() As String
        Try


            'se  obtiene el separaddor decimal d ela configuracion regional para poder formatear correctamente el numero
            separadordec = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator

            If separadordec = "." Then
                valor = Replace(numero, ",", ".")
                arreglo = valor.Split(".")
                Select Case arreglo(1).Length
                    Case 1

                    Case 2
                    Case 3
                    Case 4


                End Select
            Else
                valor = Replace(numero, ".", ",")
            End If

            Return valor
        Catch ex As Exception
            MsgBox("Problema al convertir Numero:" & ex.Message)
            Return valor
        End Try
    End Function
    Public Function decimalAS2decimalUsuario(ByVal decimalAS As Double) As String
        Dim numero As String

        Try

            numero = CStr(decimalAS)
            ' esto no: System.Threading.Thread.CurrentThread.CurrentCulture = New System.Globalization.CultureInfo("es-AR")
            ' esto tampoco: numero = FormatNumber(numero, 2, TriState.UseDefault, TriState.UseDefault, TriState.True)
            numero = decimalAS.ToString("#,0.00", New System.Globalization.CultureInfo("es-AR"))

            Return numero.Trim()

        Catch ex As Exception
            Return Nothing
        End Try
    End Function
    Public Shared Property Version() As String
        Get
            Dim pos As Integer

            If _version = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _version = "v 01.00.00"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                       
                        If Left(linea, 13).Trim().ToLower = "numeroversion" Then
                            strCnStr = linea.Substring(14, linea.Length - 14)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _version = "v 01.00.00"
                Else
                    _version = strCnStr
                End If
            End If
            Return _version
        End Get
        Set(ByVal value As String)
            _version = value
        End Set
    End Property

    ' agregado de parametros publicador
    Public Shared Property ModoPublicacion() As String
        Get
            Dim pos As Integer

            If _ModoPublicacion = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _ModoPublicacion = "0"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 15) = "ModoPublicacion" Then
                            strCnStr = linea.Substring(16, linea.Length - 16)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _ModoPublicacion = "0"
                Else
                    _ModoPublicacion = strCnStr
                End If
            End If
            Return _ModoPublicacion
        End Get
        Set(ByVal value As String)
            _ModoPublicacion = value
        End Set
    End Property

    Public Shared Property PublicaDisplay() As String
        Get
            If _PublicaDisplay = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _PublicaDisplay = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 14).ToLower = "publicadisplay" Then
                            strCnStr = linea.Substring(15, linea.Length - 15)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _PublicaDisplay = "N"
                Else
                    _PublicaDisplay = strCnStr
                End If
            End If
            Return _PublicaDisplay
        End Get
        Set(ByVal value As String)
            _PublicaDisplay = value
        End Set
    End Property

    Public Shared Property PublicaWeb() As String
        Get
            If _PublicaWeb = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _PublicaWeb = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 10).ToLower = "publicaweb" Then
                            strCnStr = linea.Substring(11, linea.Length - 11)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _PublicaWeb = "N"
                Else
                    _PublicaWeb = strCnStr
                End If
            End If
            Return _PublicaWeb
        End Get
        Set(ByVal value As String)
            _PublicaWeb = value
        End Set
    End Property
    '*** IAFAS agregado de paramemtros
    Public Shared Sub ActualizaParametrosPublicador(ByVal ModoPublicacion As Integer, ByVal publicaDisplay As String, ByVal PublicaWeb As String, ByVal Termina As Integer, ByVal PublicarWebON As String, ByVal PublicarWebOff As String)
        Dim cm As SqlCommand = New SqlCommand
        Dim vsql As String
        Try
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            vsql = "UPDATE parametrosPublicador set modopublicacion=" & ModoPublicacion & " , publicaDisplay='" & publicaDisplay & "',PublicaWeb='" & PublicaWeb & "' ,par_terminar=" & Termina & ",publicarwebon='" & PublicarWebON & "',publicarweboff='" & PublicarWebOff & "'"
            cm.CommandText = vsql
            cm.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    Public Shared Sub ActualizaPar_Terminar_Publicador()
        Dim cm As SqlCommand = New SqlCommand
        Dim vsql As String
        Try
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text

            vsql = "UPDATE parametrosPublicador set par_terminar=1"
            cm.CommandText = vsql
            cm.ExecuteNonQuery()

        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub
    Public Shared Sub ActualizaVariableConfigurador()
        Dim cm As SqlCommand = New SqlCommand
        Dim vsql As String
        Try
            cm.Connection = General.Obtener_Conexion
            cm.CommandType = CommandType.Text
            vsql = " UPDATE ParametrosPublicador set par_terminar=0,par_publicador=0,par_publicando=0"
            cm.CommandText = vsql
            cm.ExecuteNonQuery()
        Catch ex As Exception
            Throw New Exception(ex.Message)
        End Try
    End Sub

    '****************
    Public Shared Property MailHost() As String
        Get
            If _MailHost = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _MailHost = "localhost"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 8) = "MailHost" Then
                            strCnStr = linea.Substring(9, linea.Length - 9)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _MailHost = "Localhost"
                Else
                    _MailHost = strCnStr
                End If
            End If
            Return _MailHost
        End Get
        Set(ByVal value As String)
            _MailHost = value
        End Set
    End Property


    Public Shared Property MailPort() As String
        Get
            If _MailPort = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _MailPort = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 8) = "MailPort" Then
                            strCnStr = linea.Substring(9, linea.Length - 9)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _MailPort = "0"
                Else
                    _MailPort = strCnStr
                End If
            End If
            Return _MailPort
        End Get
        Set(ByVal value As String)
            _MailPort = value
        End Set
    End Property

    Public Shared Property MailFrom() As String
        Get
            If _MailFrom = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _MailFrom = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 8) = "MailFrom" Then
                            strCnStr = linea.Substring(9, linea.Length - 9)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _MailFrom = ""
                Else
                    _MailFrom = strCnStr
                End If
            End If
            Return _MailFrom
        End Get
        Set(ByVal value As String)
            _MailFrom = value
        End Set
    End Property
    Public Shared Property MailSep() As String
        Get
            If _MailSep = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _MailSep = ";"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 7) = "MailSep" Then
                            strCnStr = linea.Substring(8, linea.Length - 8)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _MailSep = ";"
                Else
                    _MailSep = strCnStr
                End If
            End If
            Return _MailSep
        End Get
        Set(ByVal value As String)
            _MailSep = value
        End Set
    End Property

    Public Shared Property ListaEnvioExtracto() As String
        Get
            If _ListaEnvioExtracto = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _ListaEnvioExtracto = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 18) = "ListaEnvioExtracto" Then
                            strCnStr = linea.Substring(19, linea.Length - 19)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _ListaEnvioExtracto = ""
                Else
                    _ListaEnvioExtracto = strCnStr
                End If
            End If
            Return _ListaEnvioExtracto
        End Get
        Set(ByVal value As String)
            _ListaEnvioExtracto = value
        End Set
    End Property
    Public Shared Property CarpetaArchivosBoldt() As String
        Get
            If _CarpetaArchivosboldt = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _CarpetaArchivosboldt = "C:\"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 20).ToLower = "carpetaarchivosboldt" Then
                            strCnStr = linea.Substring(21, linea.Length - 21)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _CarpetaArchivosboldt = "C:\"
                Else
                    _CarpetaArchivosboldt = strCnStr
                End If
            End If
            Return _CarpetaArchivosboldt
        End Get
        Set(ByVal value As String)
            _CarpetaArchivosboldt = value
        End Set
    End Property

    Public Shared Property LetraPciaArchivosBoldt() As String
        Get
            If _LetraPciaArchivosBoldt = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _LetraPciaArchivosBoldt = "C"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 22).ToLower = "letrapciaarchivosboldt" Then
                            strCnStr = linea.Substring(23, linea.Length - 23)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _LetraPciaArchivosBoldt = "C"
                Else
                    _LetraPciaArchivosBoldt = strCnStr
                End If
            End If
            Return _LetraPciaArchivosBoldt
        End Get
        Set(ByVal value As String)
            _LetraPciaArchivosBoldt = value
        End Set
    End Property

    Public Shared Property NroPciaArchivosBoldt() As String
        Get
            If _nroPciaArchivosboldt = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _nroPciaArchivosboldt = "0"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 20).ToLower = "nropciaarchivosboldt" Then
                            strCnStr = linea.Substring(21, linea.Length - 21)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _nroPciaArchivosboldt = "0"
                Else
                    _nroPciaArchivosboldt = strCnStr
                End If
            End If
            Return _nroPciaArchivosboldt
        End Get
        Set(ByVal value As String)
            _nroPciaArchivosboldt = value
        End Set
    End Property

    '******
    Public Shared Property PrefijoPozo() As String
        Get
            If _prefijoPozo = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _prefijoPozo = "poz"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 12).ToLower = "prefijo_pozo" Then
                            strCnStr = linea.Substring(13, linea.Length - 13)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _prefijoPozo = "poz"
                Else
                    _prefijoPozo = strCnStr
                End If
            End If
            Return _prefijoPozo
        End Get
        Set(ByVal value As String)
            _prefijoPozo = value
        End Set
    End Property

    Public Shared Property PrefijoPremio() As String
        Get
            If _prefijoPremio = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _prefijoPremio = "pre"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 14).ToLower = "prefijo_premio" Then
                            strCnStr = linea.Substring(15, linea.Length - 15)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _prefijoPremio = "pre"
                Else
                    _prefijoPremio = strCnStr
                End If
            End If
            Return _prefijoPremio
        End Get
        Set(ByVal value As String)
            _prefijoPremio = value
        End Set
    End Property

    Public Shared Property Path_Premios_Destino() As String
        Get
            If _PathPremiosDestino = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _PathPremiosDestino = "C:"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 20).ToLower = "path_premios_destino" Then
                            strCnStr = linea.Substring(21, linea.Length - 21)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _PathPremiosDestino = "C:"
                Else
                    _PathPremiosDestino = strCnStr
                End If
            End If
            Return _PathPremiosDestino
        End Get
        Set(ByVal value As String)
            _PathPremiosDestino = value
        End Set
    End Property

    Public Shared Property CtrMD5Premios() As String
        Get
            If _CtrMD5Premios = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _CtrMD5Premios = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 13).ToLower = "ctrmd5premios" Then
                            strCnStr = linea.Substring(14, linea.Length - 14)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _CtrMD5Premios = "N"
                Else
                    _CtrMD5Premios = strCnStr
                End If
            End If
            Return _CtrMD5Premios
        End Get
        Set(ByVal value As String)
            _CtrMD5Premios = value
        End Set
    End Property

    Public Shared Property CtrMD5Pozos() As String
        Get
            If _CtrMD5Pozos = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _CtrMD5Pozos = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 11).ToLower = "ctrmd5pozos" Then
                            strCnStr = linea.Substring(12, linea.Length - 12)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _CtrMD5Pozos = "N"
                Else
                    _CtrMD5Pozos = strCnStr
                End If
            End If
            Return _CtrMD5Pozos
        End Get
        Set(ByVal value As String)
            _CtrMD5Pozos = value
        End Set
    End Property

    Public Shared Property Path_Pozo_Destino() As String
        Get
            If _PathPozoDestino = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _PathPozoDestino = "C:"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 17).ToLower = "path_pozo_destino" Then
                            strCnStr = linea.Substring(18, linea.Length - 18)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _PathPozoDestino = "C:"
                Else
                    _PathPozoDestino = strCnStr
                End If
            End If
            Return _PathPozoDestino
        End Get
        Set(ByVal value As String)
            _PathPozoDestino = value
        End Set
    End Property

    '**22/10/2012
    Public Shared Property Pozo_Estimado_Poceada() As String
        Get
            If _pozoEstimadoPoceada = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _pozoEstimadoPoceada = "0"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 29).ToLower = "pozo_estimado_poceada_federal" Then
                            strCnStr = linea.Substring(30, linea.Length - 30)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _pozoEstimadoPoceada = "0"
                Else
                    _pozoEstimadoPoceada = strCnStr
                End If
            End If
            Return _pozoEstimadoPoceada
        End Get
        Set(ByVal value As String)
            _pozoEstimadoPoceada = value
        End Set
    End Property
    Public Shared Property CarpetaDestinoArchivosExtractoBoldt() As String
        Get
            If _CarpetaDestinoArchivosExtractoBoldt = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _CarpetaDestinoArchivosExtractoBoldt = "C:\"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 35).ToLower = "carpetadestinoarchivosextractoboldt" Then
                            strCnStr = linea.Substring(36, linea.Length - 36)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _CarpetaDestinoArchivosExtractoBoldt = "C:\"
                Else
                    _CarpetaDestinoArchivosExtractoBoldt = strCnStr
                End If
            End If
            Return _CarpetaDestinoArchivosExtractoBoldt
        End Get
        Set(ByVal value As String)
            _CarpetaDestinoArchivosExtractoBoldt = value
        End Set
    End Property

    Public Shared Property CarpetaDestinoArchivosExtractoOtrasJuris() As String
        Get
            If _CarpetaDestinoArchivosExtractoOtrasJuris = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _CarpetaDestinoArchivosExtractoOtrasJuris = "C:\"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 40).ToLower = "carpetadestinoarchivosextractootrasjuris" Then
                            strCnStr = linea.Substring(41, linea.Length - 41)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _CarpetaDestinoArchivosExtractoOtrasJuris = "C:\"
                Else
                    _CarpetaDestinoArchivosExtractoOtrasJuris = strCnStr
                End If
            End If
            Return _CarpetaDestinoArchivosExtractoOtrasJuris
        End Get
        Set(ByVal value As String)
            _CarpetaDestinoArchivosExtractoOtrasJuris = value
        End Set
    End Property

    Public Shared Property CarpetaOrigenArchivosExtractoOtrasJuris() As String
        Get
            If _CarpetaOrigenArchivosExtractoOtrasJuris = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _CarpetaOrigenArchivosExtractoOtrasJuris = "C:\"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 39).ToLower = "carpetaorigenarchivosextractootrasjuris" Then
                            strCnStr = linea.Substring(40, linea.Length - 40)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _CarpetaOrigenArchivosExtractoOtrasJuris = "C:\"
                Else
                    _CarpetaOrigenArchivosExtractoOtrasJuris = strCnStr

                End If
            End If
            Return _CarpetaOrigenArchivosExtractoOtrasJuris

        End Get
        Set(ByVal value As String)
            _CarpetaOrigenArchivosExtractoOtrasJuris = value
        End Set
    End Property


    Public Shared Property CarpetaOrigenArchivosExtractoBoldt() As String
        Get
            If _CarpetaOrigenArchivosExtractoBoldt = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _CarpetaOrigenArchivosExtractoBoldt = "C:\"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 34).ToLower = "carpetaorigenarchivosextractoboldt" Then
                            strCnStr = linea.Substring(35, linea.Length - 35)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _CarpetaOrigenArchivosExtractoBoldt = "C:\"
                Else
                    _CarpetaOrigenArchivosExtractoBoldt = strCnStr

                End If
            End If
            Return _CarpetaOrigenArchivosExtractoBoldt

        End Get
        Set(ByVal value As String)
            _CarpetaOrigenArchivosExtractoBoldt = value
        End Set
    End Property

    Public Shared Property PrefijoSueldo() As String
        Get
            If _prefijoSueldo = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _prefijoSueldo = "sue"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 14).ToLower = "prefijo_sueldo" Then
                            strCnStr = linea.Substring(15, linea.Length - 15)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _prefijoSueldo = "sue"
                Else
                    _prefijoSueldo = strCnStr
                End If
            End If
            Return _prefijoSueldo
        End Get
        Set(ByVal value As String)
            _prefijoSueldo = value
        End Set
    End Property

    Public Shared Property CantidadIntentos() As String
        Get
            If _cantidadIntentos = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _cantidadIntentos = "2"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 26).ToLower = "Nro_Intentos_Acceso_Remoto" Then
                            strCnStr = linea.Substring(27, linea.Length - 27)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _cantidadIntentos = "2"
                Else
                    _cantidadIntentos = strCnStr
                End If
            End If
            Return _cantidadIntentos
        End Get
        Set(ByVal value As String)
            _cantidadIntentos = value
        End Set
    End Property


    Public Shared Property CarpetaExtractoBoltConfirmado() As String
        Get
            If _carpetaExtractoBoltConfirmado = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _carpetaExtractoBoltConfirmado = "C:\"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 29).ToLower = "carpetaextractoboltconfirmado" Then
                            strCnStr = linea.Substring(30, linea.Length - 30)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _carpetaExtractoBoltConfirmado = "C:\"
                Else
                    _carpetaExtractoBoltConfirmado = strCnStr
                End If
            End If
            Return _carpetaExtractoBoltConfirmado
        End Get
        Set(ByVal value As String)
            _carpetaExtractoBoltConfirmado = value
        End Set
    End Property
    '*** parametros publicador*******
    Public Shared Property PublicarWebON() As String
        Get
            If _PublicarWebON = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _PublicarWebON = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 13).ToLower = "publicarwebon" Then
                            strCnStr = linea.Substring(14, linea.Length - 14).ToUpper
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _PublicarWebON = "N"
                Else
                    _PublicarWebON = strCnStr
                End If
            End If
            Return _PublicarWebON
        End Get
        Set(ByVal value As String)
            _PublicarWebON = value
        End Set
    End Property

    Public Shared Property PublicarWebOFF() As String
        Get
            If _PublicarWebOFF = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _PublicarWebOFF = "S"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 14).ToLower = "publicarweboff" Then
                            strCnStr = linea.Substring(15, linea.Length - 15).ToUpper
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _PublicarWebOFF = "S"
                Else
                    _PublicarWebOFF = strCnStr
                End If
            End If
            Return _PublicarWebOFF
        End Get
        Set(ByVal value As String)
            _PublicarWebOFF = value
        End Set
    End Property
    Public Shared Property PublicaExtractosWSRestON() As String
        Get
            If _PublicaExtractosWSRestON = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _PublicaExtractosWSRestON = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una liena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 24).ToLower = "publicaextractoswsreston" Then
                            strCnStr = linea.Substring(25, linea.Length - 25).ToUpper
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _PublicaExtractosWSRestON = "N"
                Else
                    _PublicaExtractosWSRestON = strCnStr
                End If
            End If
            Return _PublicaExtractosWSRestON
        End Get
        Set(ByVal value As String)
            _PublicaExtractosWSRestON = value
        End Set
    End Property

    Public Shared Property PublicaExtractosWSRestOFF() As String
        Get
            If _PublicaExtractosWSRestOFF = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _PublicaExtractosWSRestOFF = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una lÃ­ena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 25).ToLower = "publicaextractoswsrestoff" Then
                            strCnStr = linea.Substring(26, linea.Length - 26).ToUpper
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _PublicaExtractosWSRestOFF = "N"
                Else
                    _PublicaExtractosWSRestOFF = strCnStr
                End If
            End If
            Return _PublicaExtractosWSRestOFF
        End Get
        Set(ByVal value As String)
            _PublicaExtractosWSRestOFF = value
        End Set
    End Property

    Public Shared Property Jurisdiccion() As String
        Get
            If _Jurisdiccion = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _Jurisdiccion = "S"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 12).ToLower = "jurisdiccion" Then
                            strCnStr = linea.Substring(13, linea.Length - 13).ToUpper
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _Jurisdiccion = "S"
                Else
                    _Jurisdiccion = strCnStr
                End If
            End If
            Return _Jurisdiccion
        End Get
        Set(ByVal value As String)
            _Jurisdiccion = value
        End Set
    End Property
    '*** 26/11/2013
    Public Shared Property CantidadMaximaJurisdicciones() As String
        Get
            If _cantidadMaximaJurisdicciones = "" Or _cantidadMaximaJurisdicciones = "0" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _cantidadMaximaJurisdicciones = "0"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 28).ToLower = "cantidadmaximajurisdicciones" Then
                            strCnStr = linea.Substring(29, linea.Length - 29)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _cantidadMaximaJurisdicciones = "0"
                Else
                    _cantidadMaximaJurisdicciones = strCnStr
                End If
            End If
            Return _cantidadMaximaJurisdicciones
        End Get
        Set(ByVal value As String)
            _cantidadMaximaJurisdicciones = value
        End Set
    End Property
    Public Shared Property servidorFTP() As String
        Get
            If _servidorFTP = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _servidorFTP = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 11).ToLower = "servidorftp" Then
                            strCnStr = linea.Substring(12, linea.Length - 12)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _servidorFTP = ""
                Else
                    _servidorFTP = strCnStr
                End If
            End If
            Return _servidorFTP
        End Get
        Set(ByVal value As String)
            _servidorFTP = value
        End Set
    End Property

    Public Shared Property usuarioFTP() As String
        Get
            If _usuarioFTP = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _usuarioFTP = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 10).ToLower = "usuarioftp" Then
                            strCnStr = linea.Substring(11, linea.Length - 11)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _usuarioFTP = ""
                Else
                    _usuarioFTP = strCnStr
                End If
            End If
            Return _usuarioFTP
        End Get
        Set(ByVal value As String)
            _usuarioFTP = value
        End Set
    End Property
    Public Shared Property pwdFTP() As String
        Get
            If _pwdFTP = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _pwdFTP = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 6).ToLower = "pwdftp" Then
                            strCnStr = linea.Substring(7, linea.Length - 7)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _pwdFTP = ""
                Else
                    _pwdFTP = strCnStr
                End If
            End If
            Return _pwdFTP
        End Get
        Set(ByVal value As String)
            _pwdFTP = value
        End Set
    End Property

    '**********FTP y GPG************
    Public Shared Property EnviarFTP() As String
        Get
            If _enviarFTP = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _enviarFTP = "S"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 9).ToLower = "enviarftp" Then
                            strCnStr = linea.Substring(10, linea.Length - 10).ToUpper
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _enviarFTP = "S"
                Else
                    _enviarFTP = strCnStr
                End If
            End If
            Return _enviarFTP
        End Get
        Set(ByVal value As String)
            _enviarFTP = value
        End Set
    End Property
    Public Shared Property EncriptarGPG() As String
        Get
            If _encriptarGPG = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _encriptarGPG = "S"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 12).ToLower = "encriptargpg" Then
                            strCnStr = linea.Substring(13, linea.Length - 13).ToUpper
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _encriptarGPG = "S"
                Else
                    _encriptarGPG = strCnStr
                End If
            End If
            Return _encriptarGPG
        End Get
        Set(ByVal value As String)
            _encriptarGPG = value
        End Set
    End Property

    '***
    Public Shared Property pathGPG() As String
        Get
            If _pathGPG = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _pathGPG = "S"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 7).ToLower = "pathgpg" Then
                            strCnStr = linea.Substring(8, linea.Length - 8).ToUpper
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _pathGPG = ""
                Else
                    _pathGPG = strCnStr
                End If
            End If
            Return _pathGPG
        End Get
        Set(ByVal value As String)
            _pathGPG = value
        End Set
    End Property
    Public Shared Property claveGPG() As String
        Get
            If _claveGPG = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _claveGPG = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 8).ToLower = "clavegpg" Then
                            strCnStr = linea.Substring(9, linea.Length - 9)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _claveGPG = ""
                Else
                    _claveGPG = strCnStr
                End If
            End If
            Return _claveGPG
        End Get
        Set(ByVal value As String)
            _claveGPG = value
        End Set
    End Property


    Public Shared Property Obtener_pgmsorteos_ws() As String
        Get
            If _Obtener_pgmsorteos_ws = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _Obtener_pgmsorteos_ws = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 21).ToLower = "obtener_pgmsorteos_ws" Then
                            strCnStr = linea.Substring(22, linea.Length - 22)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _Obtener_pgmsorteos_ws = "N"
                Else
                    _Obtener_pgmsorteos_ws = strCnStr
                End If
            End If
            Return _Obtener_pgmsorteos_ws
        End Get
        Set(ByVal value As String)
            _Obtener_pgmsorteos_ws = value
        End Set
    End Property

    Public Shared Property Url_extractos() As String
        Get
            If _Url_extractos = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _Url_extractos = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 12).ToLower = "url_extracto" Then
                            strCnStr = linea.Substring(13, linea.Length - 13)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _Url_extractos = ""
                Else
                    _Url_extractos = strCnStr
                End If
            End If
            Return _Url_extractos
        End Get
        Set(ByVal value As String)
            _Url_extractos = value
        End Set
    End Property


    Public Shared Property carpetaFTPExtractos() As String
        Get
            If _carpetaFTPExtractos = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _carpetaFTPExtractos = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If
                        If Left(linea, 19).ToLower = "carpetaftpextractos" Then
                            strCnStr = linea.Substring(20, linea.Length - 20)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _carpetaFTPExtractos = ""
                Else
                    _carpetaFTPExtractos = strCnStr
                End If
            End If
            Return _carpetaFTPExtractos
        End Get
        Set(ByVal value As String)
            _carpetaFTPExtractos = value
        End Set
    End Property
    Public Shared Property DiasSorteosAnteriores() As String
        Get
            Dim pos As Integer

            If _DiasSorteosAnteriores = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _DiasSorteosAnteriores = "0"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 21) = "diassorteosanteriores" Then
                            strCnStr = linea.Substring(22, linea.Length - 22)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _DiasSorteosAnteriores = "0"
                Else
                    _DiasSorteosAnteriores = strCnStr
                End If
            End If
            Return _DiasSorteosAnteriores
        End Get
        Set(ByVal value As String)
            _DiasSorteosAnteriores = value
        End Set
    End Property
    ' listar o no reprote detalle parametros
    Public Shared Property ListarReporteDetalleParametros() As String
        Get
            Dim pos As Integer

            If _ListarReporteDetalleParametros = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _ListarReporteDetalleParametros = "N"
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una líena del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 30) = "listarreportedetalleparametros" Then
                            strCnStr = linea.Substring(31, linea.Length - 31)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                If strCnStr.Trim.Length = 0 Then
                    _ListarReporteDetalleParametros = "N"
                Else
                    _ListarReporteDetalleParametros = strCnStr
                End If
            End If
            Return _ListarReporteDetalleParametros
        End Get
        Set(ByVal value As String)
            _ListarReporteDetalleParametros = value
        End Set
    End Property
   

    
    
    Public Shared Property urlRest() As String

        Get
            If _urlRest = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _urlRest = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una linea del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 7).Trim().ToLower = "urlrest" Then
                            strCnStr = linea.Substring(8, linea.Length - 8)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                _urlRest = strCnStr
            End If
            Return _urlRest
        End Get
        Set(ByVal value As String)
            _urlRest = value
        End Set
    End Property

    Public Shared Property prefijoRPTExtracto() As String

        Get
            If _prefijoRPTExtracto = "" Then
                Dim linea As String = ""
                Dim strCnStr As String = ""
                Dim archivo = PathIni & "sorteos.ini"

                If Not System.IO.File.Exists(archivo) Then
                    _prefijoRPTExtracto = ""
                Else
                    ' Leer el fichero usando la codificación de Windows
                    ' pero pudiendo usar la marca de tipo de fichero (BOM)
                    Dim sr As New System.IO.StreamReader( _
                                archivo, _
                                System.Text.Encoding.Default, _
                                True)

                    ' Leer el contenido mientras no se llegue al final
                    While sr.Peek() <> -1
                        ' Leer una linea del fichero
                        linea = sr.ReadLine()
                        ' Si no está vacía, añadirla al control
                        ' Si está vacía, continuar el bucle
                        If String.IsNullOrEmpty(linea) Then
                            Continue While
                        End If

                        If Left(linea, 18).Trim().ToLower = "prefijorptextracto" Then
                            strCnStr = linea.Substring(19, linea.Length - 19)
                            Exit While
                        End If
                    End While
                    ' Cerrar el fichero
                    sr.Close()

                End If
                _prefijoRPTExtracto = strCnStr
            End If
            Return _prefijoRPTExtracto
        End Get
        Set(ByVal value As String)
            _prefijoRPTExtracto = value
        End Set
    End Property
    Public Shared Property Origen() As String
        Get
            'If _Carpeta_Bd_Rem = "" Then
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Origen = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 6).ToLower = "origen" Then
                        strCnStr = linea.Substring(7, linea.Length - 7)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _Origen = "QNL"
            Else
                _Origen = strCnStr

            End If
            'End If
            Return _Origen

        End Get
        Set(ByVal value As String)
            _Origen = value
        End Set
    End Property
    Public Shared Function ValidarFechas(ByVal desde As String, ByVal hasta As String) As Boolean

        Dim _desde() = Split(desde, "/")
        Dim _hasta() = Split(hasta, "/")

        If New Date(Mid(_desde(2), 1, 4), _desde(1), _desde(0)) = New Date(Mid(_hasta(2), 1, 4), _hasta(1), _hasta(0)) Then
            Return True
        Else
            Return False
        End If
    End Function
    Public Shared Function ValidaDesdeHasta(ByVal desde As String, ByVal hasta As String) As Boolean

        Dim _desde() = Split(desde, "/")
        Dim _hasta() = Split(hasta, "/")

        If New Date(Mid(_desde(2), 1, 4), _desde(1), _desde(0)) <= New Date(Mid(_hasta(2), 1, 4), _hasta(1), _hasta(0)) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function RedondearLong(ByVal valorARedondear As Long, ByVal PosicionRedondeo As Integer) As Long
        Dim retorno As Long = 0
        Dim min As Long = Math.Pow(10, PosicionRedondeo)

        retorno = Convert.ToInt64(Math.Ceiling((Convert.ToDecimal(valorARedondear) / min)) * min)
        Return retorno

    End Function

    Public Shared Property CalculaPozosProximo() As String
        Get
            'If _Carpeta_Bd_Rem = "" Then
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Origen = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 19).ToLower = "calculapozosproximo" Then
                        strCnStr = linea.Substring(20, linea.Length - 20)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _calculaPozosProximo = "N"
            Else
                _calculaPozosProximo = strCnStr
            End If
            'End If
            Return _calculaPozosProximo

        End Get
        Set(ByVal value As String)
            _calculaPozosProximo = value
        End Set
    End Property

    Public Shared Property Extr_Interjur() As String
        Get
            'If _Carpeta_Bkp = "" Then
            Dim linea As String = ""
            Dim strCnStr As String = ""
            Dim archivo = PathIni & "sorteos.ini"

            If Not System.IO.File.Exists(archivo) Then
                _Carpeta_Bkp = "C:\"
            Else
                ' Leer el fichero usando la codificación de Windows
                ' pero pudiendo usar la marca de tipo de fichero (BOM)
                Dim sr As New System.IO.StreamReader( _
                            archivo, _
                            System.Text.Encoding.Default, _
                            True)

                ' Leer el contenido mientras no se llegue al final
                While sr.Peek() <> -1
                    ' Leer una líena del fichero
                    linea = sr.ReadLine()
                    ' Si no está vacía, añadirla al control
                    ' Si está vacía, continuar el bucle
                    If String.IsNullOrEmpty(linea) Then
                        Continue While
                    End If

                    If Left(linea, 13).ToLower = "extr_interjur" Then
                        strCnStr = linea.Substring(14, linea.Length - 14)
                        Exit While
                    End If
                End While
                ' Cerrar el fichero
                sr.Close()

            End If
            If strCnStr.Trim.Length = 0 Then
                _extr_interjur = "C:\"
            Else
                _extr_interjur = strCnStr
            End If

            Return _extr_interjur

        End Get
        Set(ByVal value As String)
            _extr_interjur = value
        End Set
    End Property

End Class

