Namespace Entities


    Public Class ExtraccionesDET
        Implements IComparable


        Private _idExtraccionesDET As Integer
        Private _idExtraccionesCAB As Integer
        Private _orden As Integer
        Private _valor As String
        Private _posicionEnExtracto As Integer
        Private _Ordenamiento As TipoOrdenamiento
        Private _repetido As Integer = 0

        Public Property Ordenamiento() As TipoOrdenamiento
            Get
                Return _Ordenamiento
            End Get
            Set(ByVal value As TipoOrdenamiento)
                _Ordenamiento = value
            End Set
        End Property
        Enum TipoOrdenamiento As Integer
            SinOrden = 0
            OrdenPosicion = 1
            OrdenExtraccion = 2
            OrdenValor = 3
        End Enum


        Private _fechaHora As Date
        Public Property idExtraccionesDET() As Integer
            Get
                Return _idExtraccionesDET
            End Get
            Set(ByVal value As Integer)
                _idExtraccionesDET = value
            End Set
        End Property
        Public Property idExtraccionesCAB() As Integer
            Get
                Return _idExtraccionesCAB
            End Get
            Set(ByVal value As Integer)
                _idExtraccionesCAB = value
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
        Public Property Valor() As String
            Get
                Return _valor
            End Get
            Set(ByVal value As String)
                _valor = value
            End Set
        End Property
        Public Property PosicionEnExtracto() As Integer
            Get
                Return _posicionEnExtracto
            End Get
            Set(ByVal value As Integer)
                _posicionEnExtracto = value
            End Set
        End Property

        Public Property FechaHora() As Date
            Get
                Return _fechaHora
            End Get
            Set(ByVal value As Date)
                _fechaHora = value
            End Set
        End Property


        Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo
            Dim ExtraccionDET As ExtraccionesDET
            ExtraccionDET = obj
            Select Case Ordenamiento
                Case TipoOrdenamiento.OrdenExtraccion
                    If Me.Orden = -1 Then
                        Return 1
                    End If
                    If ExtraccionDET.Orden = -1 Then
                        Return -1
                    End If
                    If Me.Orden < ExtraccionDET.Orden Then
                        Return -1
                    ElseIf Me.Orden = ExtraccionDET.Orden Then
                        Return 0
                    Else
                        Return 1
                    End If
                Case TipoOrdenamiento.OrdenPosicion
                    If Me.PosicionEnExtracto = -1 Then
                        Return 1
                    End If
                    If ExtraccionDET.PosicionEnExtracto = -1 Then
                        Return -1
                    End If
                    If Me.PosicionEnExtracto < ExtraccionDET.PosicionEnExtracto Then
                        Return -1
                    ElseIf Me.PosicionEnExtracto = ExtraccionDET.PosicionEnExtracto Then
                        Return 0
                    Else
                        Return 1
                    End If
                Case TipoOrdenamiento.OrdenValor
                    If Me.Valor = -1 Then
                        Return 1
                    End If
                    If ExtraccionDET.Valor = -1 Then
                        Return -1
                    End If
                    If Me.Valor < ExtraccionDET.Valor Then
                        Return -1
                    ElseIf Me.Valor = ExtraccionDET.Valor Then
                        Return 0
                    Else
                        Return 1
                    End If

            End Select


        End Function
        Public Property Repetido() As Integer
            Get
                Return _repetido
            End Get
            Set(ByVal value As Integer)
                _repetido = value
            End Set
        End Property

    End Class
End Namespace