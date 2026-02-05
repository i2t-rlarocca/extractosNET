Namespace Entities

    Public Class Cargo
        Private _idCargo As Integer
        Private _cargo As String
        Private _orden As Integer
        Private _habilitado As Integer
        Public Property idCargo() As Integer
            Get
                Return _idCargo
            End Get
            Set(ByVal value As Integer)
                _idCargo = value
            End Set

        End Property
        Public Property Cargo() As String
            Get
                Return _cargo
            End Get
            Set(ByVal value As String)
                _cargo = value
            End Set
        End Property
        Public Property Orden() As Integer
            Get
                Return _orden
            End Get
            Set(ByVal value As Integer)
                _orden = value
            End Set
        End Property
        Public Property Habilitado() As Integer
            Get
                Return _habilitado
            End Get
            Set(ByVal value As Integer)
                _habilitado = value
            End Set
        End Property

    End Class
End Namespace