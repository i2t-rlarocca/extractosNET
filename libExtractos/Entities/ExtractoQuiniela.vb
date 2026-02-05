Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    Public Class ExtractoQuiniela
        Inherits Extracto
        Private _Loteria As Loteria

        Public Property Loteria() As Loteria
            Get
                Return _Loteria
            End Get
            Set(ByVal value As Loteria)
                _Loteria = value
            End Set
        End Property
    End Class
End Namespace