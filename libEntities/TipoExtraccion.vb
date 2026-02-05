Namespace Entities


    Public Class TipoExtraccion

        Private _idTipoExtraccion As Integer
        Private _nombre As String
        Private _descripcion As String
        Private _tipoTope As TipoTope
        Private _cantExtractos As Integer
        Private _cantExtractosPorColumna As Integer
        Private _cantCifras As Integer
        Private _metodoIngreso As MetodoIngreso
        Private _tipoValorExtraido As TipoValorExtraido
        Private _valorMinimo As Integer
        Private _valorMaximo As Integer
        Private _sorteaPosicion As Boolean
        Private _criterioFinExtraccion As CriterioFinExtraccion
        Private _ordenExtraccion As OrdenExtraccion
        Private _ordenEnExtracto As OrdenEnExtracto
        Private _AdmiteRepetidos As Boolean
        Public Property idTipoExtraccion() As Integer
            Get
                Return _idTipoExtraccion
            End Get
            Set(ByVal value As Integer)
                _idTipoExtraccion = value
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
        Public Property descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(ByVal value As String)
                _descripcion = value
            End Set
        End Property
        Public Property tipoTope() As TipoTope
            Get
                Return _tipoTope
            End Get
            Set(ByVal value As TipoTope)
                _tipoTope = value
            End Set
        End Property
        Public Property cantExtractos() As Integer
            Get
                Return _cantExtractos
            End Get
            Set(ByVal value As Integer)
                _cantExtractos = value
            End Set
        End Property
        Public Property cantExtractosPorColumna() As Integer
            Get
                Return _cantExtractosPorColumna
            End Get
            Set(ByVal value As Integer)
                _cantExtractosPorColumna = value
            End Set
        End Property
        Public Property cantCifras() As Integer
            Get
                Return _cantCifras
            End Get
            Set(ByVal value As Integer)
                _cantCifras = value
            End Set
        End Property
        Public Property metodoIngreso() As MetodoIngreso
            Get
                Return _metodoIngreso
            End Get
            Set(ByVal value As MetodoIngreso)
                _metodoIngreso = value
            End Set
        End Property
        Public Property tipoValorExtraido() As TipoValorExtraido
            Get
                Return _tipoValorExtraido
            End Get
            Set(ByVal value As TipoValorExtraido)
                _tipoValorExtraido = value
            End Set
        End Property
        Public Property valorMinimo() As Integer
            Get
                Return _valorMinimo
            End Get
            Set(ByVal value As Integer)
                _valorMinimo = value
            End Set
        End Property
        Public Property valorMaximo() As Integer
            Get
                Return _valorMaximo
            End Get
            Set(ByVal value As Integer)
                _valorMaximo = value
            End Set
        End Property
        Public Property sorteaPosicion() As Boolean
            Get
                Return _sorteaPosicion
            End Get
            Set(ByVal value As Boolean)
                _sorteaPosicion = value
            End Set
        End Property
        Public Property criterioFinExtraccion() As CriterioFinExtraccion
            Get
                Return _criterioFinExtraccion
            End Get
            Set(ByVal value As CriterioFinExtraccion)
                _criterioFinExtraccion = value
            End Set
        End Property
        Public Property ordenExtraccion() As OrdenExtraccion
            Get
                Return _ordenExtraccion
            End Get
            Set(ByVal value As OrdenExtraccion)
                _ordenExtraccion = value
            End Set
        End Property
        Public Property ordenEnExtracto() As OrdenEnExtracto
            Get
                Return _ordenEnExtracto
            End Get
            Set(ByVal value As OrdenEnExtracto)
                _ordenEnExtracto = value
            End Set
        End Property
        Public Property AdmiteRepetidos() As Boolean
            Get
                Return _AdmiteRepetidos
            End Get
            Set(ByVal value As Boolean)
                _AdmiteRepetidos = value
            End Set
        End Property
    End Class

End Namespace
