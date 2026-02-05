Imports Sorteos.Helpers
Namespace Entities

    Public Class pgmSorteo_loteria
        Dim _idPgmSorteo As Integer
        Dim _loteria As Loteria
        Dim _nroSorteoLoteria As Integer
        Dim _fechaHoraLoteria As DateTime
        Dim _fechaHoraIniReal As DateTime
        Dim _fechaHoraFinReal As DateTime
        Dim _Extractos_Qnl As Extracto_qnl
        Dim _extracto_letras_Qnl As ListaOrdenada(Of extracto_qnl_letras)
        '** 
        Dim _revertidaParcial As Integer
        '** nombre de loteria solo para visualizar la grilla de loterias en la pantlla de inicio,ya que no se puede acceder al objeto loteria.nombre
        Dim _nombreLoteria As String
        '** fecha de confirmacion para saber si fue confirmada
        Dim _fechaConfirmacion As DateTime
        Dim _confirmada As Boolean


        Public Property IdPgmSorteo() As Integer
            Get
                Return _idPgmSorteo
            End Get
            Set(ByVal value As Integer)
                _idPgmSorteo = value
            End Set
        End Property
        Public Property Loteria() As Loteria
            Get
                Return _loteria
            End Get
            Set(ByVal value As Loteria)
                _loteria = value
            End Set
        End Property
        Public Property NroSorteoLoteria() As Integer
            Get
                Return _nroSorteoLoteria
            End Get
            Set(ByVal value As Integer)
                _nroSorteoLoteria = value
            End Set
        End Property
        Public Property FechaHoraLoteria() As DateTime
            Get
                Return _fechaHoraLoteria
            End Get
            Set(ByVal value As DateTime)
                _fechaHoraLoteria = value
            End Set
        End Property
        Public Property FechaHoraIniReal() As DateTime
            Get
                Return _fechaHoraIniReal
            End Get
            Set(ByVal value As DateTime)
                _fechaHoraIniReal = value
            End Set
        End Property
        Public Property FechaHoraFinReal() As DateTime
            Get
                Return _fechaHoraFinReal
            End Get
            Set(ByVal value As DateTime)
                _fechaHoraFinReal = value
            End Set
        End Property
        Public Property Extractos_QNl() As Extracto_qnl
            Get
                Return _Extractos_Qnl
            End Get
            Set(ByVal value As Extracto_qnl)
                _Extractos_Qnl = value
            End Set
        End Property
        Public Property Extracto_Letras_Qnl() As ListaOrdenada(Of extracto_qnl_letras)
            Get
                Return _extracto_letras_Qnl
            End Get
            Set(ByVal value As ListaOrdenada(Of extracto_qnl_letras))
                _extracto_letras_Qnl = value
            End Set
        End Property
        '**
        Public Property RevertidaParcial() As Integer
            Get
                Return _revertidaParcial
            End Get
            Set(ByVal value As Integer)
                _revertidaParcial = value
            End Set
        End Property
        '**27/11/2012
        Public Property NombreLoteria() As String
            Get
                Return _nombreLoteria
            End Get
            Set(ByVal value As String)
                _nombreLoteria = value

            End Set
        End Property
        Public Property Fechaconfirmacion() As DateTime
            Get
                Return _fechaConfirmacion
            End Get
            Set(ByVal value As DateTime)
                _fechaConfirmacion = value
            End Set
        End Property
        Public Property Confirmada() As Boolean
            Get
                Return _confirmada
            End Get
            Set(ByVal value As Boolean)
                _confirmada = value
            End Set
        End Property

        Public Function ObtenerValorPosicion(ByVal pOrden As Integer, ByRef pValor As Integer) As Boolean
            Dim _valor As cPosicionValorLoterias
            Try
                ObtenerValorPosicion = False

                For Each _valor In Me.Extractos_QNl.Valores
                    If _valor.Posicion = pOrden Then
                        If _valor.Valor <> "" Then
                            pValor = _valor.Valor
                            ObtenerValorPosicion = True
                        End If
                        Exit Function
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

    End Class
End Namespace