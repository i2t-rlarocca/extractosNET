Namespace Entities


    Public Class MetodoIngreso

        Private _idMetodoIngreso As Integer
        Private _nombre As String
        Private _descripcion As String
        Private _habilitadoExtracciones As String
        Private _habilitadoJurisdicciones As String


        Public Property IDMetodoIngreso() As Integer
            Get
                Return _idMetodoIngreso
            End Get
            Set(ByVal value As Integer)
                _idMetodoIngreso = value
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

        Public Property Descripcion() As String
            Get
                Return _descripcion
            End Get
            Set(ByVal value As String)
                _descripcion = value
            End Set
        End Property

        Public Property HabilitadoExtracciones() As String
            Get
                Return _habilitadoExtracciones
            End Get
            Set(ByVal value As String)
                _habilitadoExtracciones = value
            End Set
        End Property
        Public Property HabilitadoJurisdicciones() As String
            Get
                Return _habilitadoJurisdicciones
            End Get
            Set(ByVal value As String)
                _habilitadoJurisdicciones = value
            End Set
        End Property
    End Class

End Namespace