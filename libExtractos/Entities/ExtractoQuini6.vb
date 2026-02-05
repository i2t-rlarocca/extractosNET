Imports Microsoft.VisualBasic

Namespace ExtractoEntities

    Public Class ExtractoQuini6
        Inherits Extracto

        Private _NumerosLaSegundaDelQuini As List(Of Numero)
        Private _NumerosPrimerSorteo As List(Of Numero)
        Private _NumerosRevancha As List(Of Numero)
        Private _NumerosSiempreSale As List(Of Numero)

        Private _PremioExtra As Premio
        Private _PremiosLaSegundaDelQuini As List(Of Premio)
        Private _PremiosPrimerSorteo As List(Of Premio)
        Private _PremiosRevancha As List(Of Premio)
        Private _PremiosSiempreSale As List(Of Premio)

        Public Property NumerosLaSegundaDelQuini() As List(Of Numero)
            Get
                Return _NumerosLaSegundaDelQuini
            End Get
            Set(ByVal value As List(Of Numero))
                _NumerosLaSegundaDelQuini = value
            End Set
        End Property

        Public Property NumerosPrimerSorteo() As List(Of Numero)
            Get
                Return _NumerosPrimerSorteo
            End Get
            Set(ByVal value As List(Of Numero))
                _NumerosPrimerSorteo = value
            End Set
        End Property

        Public Property NumerosRevancha() As List(Of Numero)
            Get
                Return _NumerosRevancha
            End Get
            Set(ByVal value As List(Of Numero))
                _NumerosRevancha = value
            End Set
        End Property

        Public Property NumerosSiempreSale() As List(Of Numero)
            Get
                Return _NumerosSiempreSale
            End Get
            Set(ByVal value As List(Of Numero))
                _NumerosSiempreSale = value
            End Set
        End Property

        Public Property PremioExtra() As Premio
            Get
                Return _PremioExtra
            End Get
            Set(ByVal value As Premio)
                _PremioExtra = value
            End Set
        End Property

        Public Property PremiosLaSegundaDelQuini() As List(Of Premio)
            Get
                Return _PremiosLaSegundaDelQuini
            End Get
            Set(ByVal value As List(Of Premio))
                _PremiosLaSegundaDelQuini = value
            End Set
        End Property

        Public Property PremiosPrimerSorteo() As List(Of Premio)
            Get
                Return _PremiosPrimerSorteo
            End Get
            Set(ByVal value As List(Of Premio))
                _PremiosPrimerSorteo = value
            End Set
        End Property

        Public Property PremiosRevancha() As List(Of Premio)
            Get
                Return _PremiosRevancha
            End Get
            Set(ByVal value As List(Of Premio))
                _PremiosRevancha = value
            End Set
        End Property

        Public Property PremiosSiempreSale() As List(Of Premio)
            Get
                Return _PremiosSiempreSale
            End Get
            Set(ByVal value As List(Of Premio))
                _PremiosSiempreSale = value
            End Set
        End Property
    End Class
End Namespace