Namespace Entities


    Public Class Juego
        Private _idJuego As Integer
        Private _Gjg_Id As Integer
        Private _Jue_Desc As String
        Private _Jue_HorLun As String
        Private _Jue_HorMar As String
        Private _Jue_HorMie As String
        Private _Jue_HorJue As String
        Private _Jue_HorVie As String
        Private _Jue_HorSab As String
        Private _Jue_HorDom As String
        Private _Jue_DesNro As Integer
        Private _Jue_HastNro As Integer
        Private _Jue_Habi As Char
        Private _Id_Agr_Juego As Integer
        Private _EnviarAMedios As String
        Private _EnviarAMediosConfParcial As String
        Private _Jue_PathLocal As String
        Private _EstimaPozosProxSorteo As Boolean
        Private _RedondeoPozoProxSorteo As Long

        Public Property IdJuego() As Integer
            Get
                Return _idJuego
            End Get
            Set(ByVal value As Integer)
                _idJuego = value
            End Set
        End Property
        Public Property Gjg_Id() As Integer
            Get
                Return _Gjg_Id
            End Get
            Set(ByVal value As Integer)
                _Gjg_Id = value
            End Set
        End Property
        Public Property Jue_Desc() As String
            Get
                Return _Jue_Desc
            End Get
            Set(ByVal value As String)
                _Jue_Desc = value
            End Set
        End Property
        Public Property Jue_HorLun() As String
            Get
                Return _Jue_HorLun
            End Get
            Set(ByVal value As String)
                _Jue_HorLun = value
            End Set
        End Property
        Public Property Jue_HorMar() As String
            Get
                Return _Jue_HorMar
            End Get
            Set(ByVal value As String)
                _Jue_HorMar = value
            End Set
        End Property

        Public Property Jue_HorMie() As String
            Get
                Return _Jue_HorMie
            End Get
            Set(ByVal value As String)
                _Jue_HorMie = value
            End Set
        End Property

        Public Property Jue_HorJue() As String
            Get
                Return _Jue_HorJue
            End Get
            Set(ByVal value As String)
                _Jue_HorJue = value
            End Set
        End Property

        Public Property Jue_HorVie() As String
            Get
                Return _Jue_HorVie
            End Get
            Set(ByVal value As String)
                _Jue_HorVie = value
            End Set
        End Property

        Public Property Jue_HorSab() As String
            Get
                Return _Jue_HorSab
            End Get
            Set(ByVal value As String)
                _Jue_HorSab = value
            End Set
        End Property
        Public Property Jue_HorDom() As String
            Get
                Return _Jue_HorDom
            End Get
            Set(ByVal value As String)
                _Jue_HorDom = value
            End Set
        End Property
        Public Property Jue_DesNro() As Integer
            Get
                Return _Jue_DesNro
            End Get
            Set(ByVal value As Integer)
                _Jue_DesNro = value
            End Set
        End Property
        Public Property Jue_HastNro() As Integer
            Get
                Return _Jue_HastNro
            End Get
            Set(ByVal value As Integer)
                _Jue_HastNro = value
            End Set
        End Property
        Public Property Jue_Habi() As Char
            Get
                Return _Jue_Habi
            End Get
            Set(ByVal value As Char)
                _Jue_Habi = value
            End Set
        End Property
        Public Property Id_Agr_Juego() As Integer
            Get
                Return _Id_Agr_Juego
            End Get
            Set(ByVal value As Integer)
                _Id_Agr_Juego = value
            End Set
        End Property
        Public Property EnviarAMedios() As String
            Get
                Return _EnviarAMedios
            End Get
            Set(ByVal value As String)
                _EnviarAMedios = value
            End Set
        End Property
        Public Property EnviarAMediosConfParcial() As String
            Get
                Return _EnviarAMediosConfParcial
            End Get
            Set(ByVal value As String)
                _EnviarAMediosConfParcial = value
            End Set
        End Property
        Public Property Jue_PathLocal() As String
            Get
                Return _Jue_PathLocal
            End Get
            Set(ByVal value As String)
                _Jue_PathLocal = value
            End Set
        End Property
        Public Property EstimaPozosProxSorteo() As Boolean
            Get
                Return _EstimaPozosProxSorteo
            End Get
            Set(ByVal value As Boolean)
                _EstimaPozosProxSorteo = value
            End Set
        End Property
        Public Property RedondeoPozoProxSorteo() As Long
            Get
                Return _RedondeoPozoProxSorteo
            End Get
            Set(ByVal value As Long)
                _RedondeoPozoProxSorteo = value
            End Set
        End Property

    End Class
End Namespace