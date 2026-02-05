Imports Sorteos.Helpers
Namespace Entities


    Public Class ExtraccionesCAB
        Private _idExtraccionesCAB As Integer
        Private _PgmConcurso As Integer
        Private _orden As Integer
        Private _titulo As String
        Private _MetodoIngreso As MetodoIngreso
        Private _fechaHoraIniReal As DateTime
        Private _fechaHoraFinReal As DateTime
        Private _idModeloExtraccionDET As Integer
        Private _ModeloExtraccionDET As ModeloExtraccionesDet
        Private _ExtraccionesDET As ListaOrdenada(Of ExtraccionesDET)
        Private _Ordenamiento As ExtraccionesDET.TipoOrdenamiento
        Private _ejecutada As Integer


        Public Property Ordenamiento() As ExtraccionesDET.TipoOrdenamiento
            Get
                Return _Ordenamiento
            End Get
            Set(ByVal value As ExtraccionesDET.TipoOrdenamiento)
                _Ordenamiento = value
                OrdenarDetalle()


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


        Public Property idPgmConcurso() As Integer
            Get
                Return _PgmConcurso
            End Get
            Set(ByVal value As Integer)
                _PgmConcurso = value
            End Set
        End Property

        Public Property orden() As Integer
            Get
                Return _orden
            End Get
            Set(ByVal value As Integer)
                _orden = value
            End Set
        End Property

        Public Property Titulo() As String
            Get
                Return _titulo
            End Get
            Set(ByVal value As String)
                _titulo = value
            End Set
        End Property
        Public Property MetodoIngreso() As MetodoIngreso
            Get
                Return _MetodoIngreso
            End Get
            Set(ByVal value As MetodoIngreso)
                _MetodoIngreso = value
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
        Public Property ExtraccionesDET() As ListaOrdenada(Of ExtraccionesDET)
            Get
                Return _ExtraccionesDET
            End Get
            Set(ByVal value As ListaOrdenada(Of ExtraccionesDET))
                _ExtraccionesDET = value
            End Set
        End Property
        Public Property idModeloExtraccionesDET() As Integer
            Get
                Return _idModeloExtraccionDET
            End Get
            Set(ByVal value As Integer)
                _idModeloExtraccionDET = value
            End Set
        End Property
        Public Property ModeloExtraccionesDET() As ModeloExtraccionesDet
            Get
                Return _ModeloExtraccionDET
            End Get
            Set(ByVal value As ModeloExtraccionesDet)
                _ModeloExtraccionDET = value
            End Set
        End Property
        Public Property Ejecutada() As Integer
            Get
                Return _ejecutada
            End Get
            Set(ByVal value As Integer)
                _ejecutada = value
            End Set
        End Property
        

        Public Function ActualizarDetalle(ByVal pOrden As Integer, ByVal pVal As String, ByVal pHora As Date, ByVal pPos As Integer, Optional ByVal pmodifica As Boolean = False) As ExtraccionesDET
            Dim oExtraccioensDET As New ExtraccionesDET
            Dim oExtraccionesAux As New ExtraccionesDET
            Try
                If pmodifica Then 'en la modificacion se debe controlar el orden 
                    For Each oExtraccioensDET In Me.ExtraccionesDET
                        If oExtraccioensDET.Orden = pOrden Then Exit For
                    Next
                    For Each oExtraccionesAux In Me.ExtraccionesDET
                        If oExtraccionesAux.PosicionEnExtracto = pPos Then Exit For
                    Next
                    'l alista se crea con todas las posiciones por lo que se deben actualizar los valores de la misma
                    oExtraccioensDET.Orden = -1
                    oExtraccioensDET.Valor = -1
                    oExtraccionesAux.Orden = pOrden
                    oExtraccionesAux.idExtraccionesDET = oExtraccioensDET.idExtraccionesDET
                    oExtraccionesAux.FechaHora = oExtraccioensDET.FechaHora
                    oExtraccionesAux.Valor = pVal
                    Return oExtraccionesAux
                    Exit Function

                Else 'es un ingreso 
                    For Each oExtraccioensDET In Me.ExtraccionesDET
                        If oExtraccioensDET.PosicionEnExtracto = pPos Then
                            oExtraccioensDET.Orden = pOrden
                            oExtraccioensDET.Valor = pVal
                            oExtraccioensDET.FechaHora = pHora
                            Return oExtraccioensDET
                            Exit Function
                        End If
                    Next
                End If
                Return Nothing
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Function

        Public Sub OrdenarDetalle()
            Dim ListaDetalles As New ArrayList
            Dim ExtraccionDEt As ExtraccionesDET

            For Each ExtraccionDEt In Me.ExtraccionesDET
                ExtraccionDEt.Ordenamiento = Me.Ordenamiento
                ListaDetalles.Add(ExtraccionDEt)
            Next
            ListaDetalles.Sort()
            Me.ExtraccionesDET.Clear()

            For Each ExtraccionDEt In ListaDetalles
                ExtraccionesDET.Add(ExtraccionDEt)
            Next

        End Sub
        'SI EL ORDEN NO SE ENCUENTRA DEVUELVE FALSO
        Public Function ObtenerValorPosicion(ByVal pOrden As Integer, ByRef pPosicion As Integer, ByRef pValor As Integer) As Boolean
            Dim _extraccionDET As ExtraccionesDET
            Try
                ObtenerValorPosicion = False
                For Each _extraccionDET In Me.ExtraccionesDET
                    If _extraccionDET.Orden = pOrden Then
                        pPosicion = _extraccionDET.PosicionEnExtracto
                        pValor = _extraccionDET.Valor
                        ObtenerValorPosicion = True
                        Exit Function
                    End If
                Next
            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try

        End Function

        Public Function ConfirmarExtraccion(ByVal pFechainicio As Date, ByVal pFechaFin As Date, ByVal pEjecutada As Integer)

            Try
                Me.FechaHoraIniReal = pFechainicio
                Me.FechaHoraIniReal = pFechaFin
                Me.Ejecutada = pEjecutada

            Catch ex As Exception
                Throw New Exception(ex.Message)
            End Try
        End Function
        '**********
        Public Function ActualizarEstructuraExtraccionDET(ByVal ExtraccionesCAB As ExtraccionesCAB)
            Dim o As New ExtraccionesDET
            Dim oeAux As New ExtraccionesDET
           
            Dim dt As New DataTable
            Dim i As Integer
            Try
                If ExtraccionesCAB.ModeloExtraccionesDET.cantExtractos <> 0 Then
                    Return Nothing
                End If
                i = ExtraccionesCAB.ExtraccionesDET.Count
                o = New ExtraccionesDET
                o.PosicionEnExtracto = i + 1
                o.FechaHora = Now
                ExtraccionesCAB.ExtraccionesDET.Add(o)
            Catch ex As Exception
                Throw New Exception(ex.Message)

            End Try


        End Function

    End Class
End Namespace