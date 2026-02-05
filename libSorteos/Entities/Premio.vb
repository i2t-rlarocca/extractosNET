Namespace Entities
    Public Class Premio

        Private _idPgmSorteo As Int32
        Private _idPremio As Int32
        Private _importePozo As Double
        Private _cantGanadores As Int32
        Private _importePremio As Double
        Private _vacante As Int32
        Private _secuencia As Int32
        Private _nombrepremio As String

        Private _AciertosDefecto As String
        Private _cargaPozo As Boolean
        Private _RequiereAciertos As Boolean
        Private _habilitado As Boolean
        Private _destino_web_pozo As String
        Private _destino_web_aciertos As String
        Private _destino_web_ganadores As String
        Private _destino_web_premio As String


        Public Property idPgmsorteo() As Int32
            Get
                Return _idPgmSorteo
            End Get
            Set(ByVal value As Int32)
                _idPgmSorteo = value
            End Set
        End Property
        Public Property idPremio() As Int32
            Get
                Return _idPremio
            End Get
            Set(ByVal value As Int32)
                _idPremio = value
            End Set
        End Property
        Public Property importePozo() As Double
            Get
                Return _importePozo
            End Get
            Set(ByVal value As Double)
                _importePozo = value
            End Set
        End Property
        Public Property cantGanadores() As Int32
            Get
                Return _cantGanadores
            End Get
            Set(ByVal value As Int32)
                _cantGanadores = value
            End Set
        End Property
        Public Property importePremio() As Double
            Get
                Return _importePremio
            End Get
            Set(ByVal value As Double)
                _importePremio = value
            End Set
        End Property
        Public Property vacante() As Int32
            Get
                Return _vacante
            End Get
            Set(ByVal value As Int32)
                _vacante = value
            End Set
        End Property
        Public Property secuencia() As Int32
            Get
                Return _secuencia
            End Get
            Set(ByVal value As Int32)
                _secuencia = value
            End Set
        End Property
        Public Property NombrePremio() As String
            Get
                Return _nombrepremio
            End Get
            Set(ByVal value As String)
                _nombrepremio = value
            End Set
        End Property

        Public Property Habilitado() As Boolean
            Get
                Return _habilitado
            End Get
            Set(ByVal value As Boolean)
                _habilitado = value
            End Set
        End Property
        Public Property RequiereAciertos() As Boolean
            Get
                Return _RequiereAciertos
            End Get
            Set(ByVal value As Boolean)
                _RequiereAciertos = value
            End Set
        End Property

        Public Property AciertosPorDef() As String
            Get
                Return _AciertosDefecto
            End Get
            Set(ByVal value As String)
                _AciertosDefecto = value
            End Set
        End Property
        Public Property CargaPozo() As Boolean
            Get
                Return _cargaPozo
            End Get
            Set(ByVal value As Boolean)
                _cargaPozo = value
            End Set
        End Property

        Public Property Destino_Web_Pozo() As String
            Get
                Return _destino_web_pozo
            End Get
            Set(ByVal value As String)
                _destino_web_pozo = value
            End Set
        End Property
        Public Property Destino_Web_Aciertos() As String
            Get
                Return _destino_web_aciertos
            End Get
            Set(ByVal value As String)
                _destino_web_aciertos = value
            End Set
        End Property
        Public Property Destino_Web_Premio() As String
            Get
                Return _destino_web_premio
            End Get
            Set(ByVal value As String)
                _destino_web_premio = value
            End Set
        End Property
        Public Property Destino_Web_Ganadores() As String
            Get
                Return _destino_web_ganadores
            End Get
            Set(ByVal value As String)
                _destino_web_ganadores = value
            End Set
        End Property

    End Class
End Namespace

