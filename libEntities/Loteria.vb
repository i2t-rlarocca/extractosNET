Namespace Entities


    Public Class Loteria
        Private _idLoteria As Char
        Private _nombre As String
        Private _prv_id As Char
        Private _orden_extracto_qnl As Integer
        Private _path_extracto As String
        Private _cifras As Integer
        Private _nroSorteoObligatorio As Boolean
        Private _letras_extracto_qnl As Boolean
        Private _cant_letras_extracto As Integer
        Private _long_letras_extracto As Integer
        Private _cifrasIngresadaDesdeForm As Integer
        Private _idLoteriaBoldt As Integer
        Private _idLoteriaDisplay As Integer
        Private _clave As String
        Private _extension_arch_extracto As String
        Private _fmt_arch_extracto As Integer
        Private _metodo_habitual As Integer

        Public Property IdLoteria() As Char
            Get
                Return _idLoteria
            End Get
            Set(ByVal value As Char)
                _idLoteria = value
            End Set
        End Property
        Public Property Nombre() As String
            Get
                Return _nombre
            End Get
            Set(ByVal value As String)
                _nombre = value
            End Set
        End Property
        Public Property Prv_Id() As Char
            Get
                Return _prv_id
            End Get
            Set(ByVal value As Char)
                _prv_id = value
            End Set
        End Property
        Public Property orden_extracto_qnl() As Integer
            Get
                Return _orden_extracto_qnl
            End Get
            Set(ByVal value As Integer)
                _orden_extracto_qnl = value
            End Set
        End Property
        Public Property path_extracto() As String
            Get
                Return _path_extracto
            End Get
            Set(ByVal value As String)
                _path_extracto = value
            End Set
        End Property
        Public Property Cifras() As Integer
            Get
                Return _cifras
            End Get
            Set(ByVal value As Integer)
                _cifras = value
            End Set
        End Property
        Public Property nroSorteoObligatorio() As Boolean
            Get
                Return _nroSorteoObligatorio
            End Get
            Set(ByVal value As Boolean)
                _nroSorteoObligatorio = value
            End Set
        End Property

        Public Property cant_letras_extracto() As Integer
            Get
                Return _cant_letras_extracto
            End Get
            Set(ByVal value As Integer)
                _cant_letras_extracto = value
            End Set
        End Property

        Public Property letras_extracto_qnl() As Boolean
            Get
                Return _letras_extracto_qnl
            End Get
            Set(ByVal value As Boolean)
                _letras_extracto_qnl = value
            End Set
        End Property
        Public Property long_letras_extracto() As Integer
            Get
                Return _long_letras_extracto
            End Get
            Set(ByVal value As Integer)
                _long_letras_extracto = value
            End Set
        End Property
        Public Property CifrasIngresadaDesdeForm() As Integer
            Get
                Return _cifrasIngresadaDesdeForm
            End Get
            Set(ByVal value As Integer)
                _cifrasIngresadaDesdeForm = value
            End Set
        End Property
        Public Property IdLoteriaBoldt() As Integer
            Get
                Return _idLoteriaBoldt
            End Get
            Set(ByVal value As Integer)
                _idLoteriaBoldt = value
            End Set
        End Property
        Public Property IdLoteriaDisplay() As Integer
            Get
                Return _idLoteriaDisplay
            End Get
            Set(ByVal value As Integer)
                _idLoteriaDisplay = value
            End Set
        End Property
        Public Property Clave() As String
            Get
                Return _clave
            End Get
            Set(ByVal value As String)
                _clave = value
            End Set
        End Property
        Public Property Extension_arch_Extracto() As String
            Get
                Return _extension_arch_extracto
            End Get
            Set(ByVal value As String)
                _extension_arch_extracto = value
            End Set
        End Property
        Public Property Fmt_arch_Extracto() As Integer
            Get
                Return _fmt_arch_extracto
            End Get
            Set(ByVal value As Integer)
                _fmt_arch_extracto = value
            End Set
        End Property
        Public Property Metodo_Habitual() As Integer
            Get
                Return _metodo_habitual
            End Get
            Set(ByVal value As Integer)
                _metodo_habitual = value
            End Set
        End Property
    End Class
End Namespace