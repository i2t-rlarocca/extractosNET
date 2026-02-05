Namespace Entities


    Public Class DiaSemana
        Private _idDiaSemana As Integer
        Private _nombre As String
        Private _abrev As Char()
        Public Property idDiaSemana() As Integer
            Get
                Return _idDiaSemana
            End Get
            Set(ByVal value As Integer)
                _idDiaSemana = value
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
        Public Property Abrev() As Char()
            Get
                Return _abrev
            End Get
            Set(ByVal value As Char())
                _abrev = value
            End Set
        End Property
    End Class
End Namespace