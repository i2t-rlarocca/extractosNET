Imports System.Drawing.Drawing2D
Imports System.Security.Permissions

Public Class frmBase
    Inherits System.Windows.Forms.Form

#Region "Variables"

    Private leftEdge, topEdge, bottomEdge, rightEdge, _
           titleBar, titleBarBorder, closeButton, _
           bottomleftEdge, bottomRightEdge, _
           maxButton, minButton, iconButton As GraphicsPath

    Private formActive As Boolean = True

    Private ButtonTip As ToolTip

    Private SystemMenu As WindowMenu


    Dim primerLoad As Boolean = True
    Public Enum Estado_Boton As Byte
        INVISIBLE = 0
        HABILITADO = 1
        INHABILITADO = 2
    End Enum

    ''' <summary>
    ''' Evento que se produce al presionar el boton cerrar
    ''' </summary>
    ''' <remarks></remarks>
    Public Event Cerrar_Clicked As EventHandler

    Private _puedeMaximizar As Boolean = False
    Private _puedeMinimizar As Boolean = False
    Private _puedeCerrar As Boolean = True

    Private _MaximizarOver As Boolean = False
    Private _MinimizarOver As Boolean = False
    Private _CerrarOver As Boolean = False

#End Region

#Region "Propiedades"

    Private _tamanioBorde As Single = 1
    <System.ComponentModel.Category("Aspecto"), _
      System.ComponentModel.Description("Tamaño del borde del formulario, excepto en la barra de título"), _
        System.ComponentModel.DefaultValue(1)> _
    Public Property TamanioBorde() As Single
        Get
            Return _tamanioBorde
        End Get
        Set(ByVal value As Single)
            _tamanioBorde = value
        End Set
    End Property

    Private _BordeBarraTitulo As Single = 1
    <System.ComponentModel.Category("Aspecto"), _
      System.ComponentModel.Description("Tamaño del borde de la barra de título"), _
        System.ComponentModel.DefaultValue(1)> _
    Public Property BordeBarraTitulo() As Single
        Get
            Return _BordeBarraTitulo
        End Get
        Set(ByVal value As Single)
            _BordeBarraTitulo = value
        End Set
    End Property

    Private _anguloEsquina As Single = 0
    <System.ComponentModel.Category("Aspecto"), _
      System.ComponentModel.Description("Angulo de las esquinas superiores del fomulario"), _
        System.ComponentModel.DefaultValue(0)> _
    Public Property AnguloEsquina() As Single
        Get
            Return _anguloEsquina
        End Get
        Set(ByVal value As Single)
            _anguloEsquina = value
        End Set
    End Property

    Private _colorBarraTitulo As Brush = Brushes.Blue
    <System.ComponentModel.Category("Aspecto"), _
      System.ComponentModel.Description("Color de la barra de Título")> _
    Public Property ColorBarraTitulo() As Brush
        Get
            Return _colorBarraTitulo
        End Get
        Set(ByVal value As Brush)
            _colorBarraTitulo = value
        End Set
    End Property

    Private _colorBordeBarraTitulo As Brush = Brushes.Black
    <System.ComponentModel.Category("Aspecto"), _
      System.ComponentModel.Description("Color del Borde de la barra de título")> _
    Public Property ColorBordeBarraTitulo() As Brush
        Get
            Return _colorBordeBarraTitulo
        End Get
        Set(ByVal value As Brush)
            _colorBordeBarraTitulo = value
        End Set
    End Property

    Private _colorBordes As Brush = Brushes.Black
    <System.ComponentModel.Category("Aspecto"), _
     System.ComponentModel.Description("Color de los bordes del form")> _
    Public Property ColorBordes() As Brush
        Get
            Return _colorBordes
        End Get
        Set(ByVal value As Brush)
            _colorBordes = value
        End Set
    End Property

    Private _colorBarraTituloInactiva As Brush = Brushes.Gray
    <System.ComponentModel.Category("Aspecto"), _
     System.ComponentModel.Description("Color de la barra de título cuando está inactiva")> _
    Public Property ColorBarraTituloInactiva() As Brush
        Get
            Return _colorBarraTituloInactiva
        End Get
        Set(ByVal value As Brush)
            _colorBarraTituloInactiva = value
        End Set
    End Property

    Private _FuenteBarraTitulo As Font = New Font("Arial", 8, FontStyle.Bold)
    <System.ComponentModel.Category("Aspecto"), _
     System.ComponentModel.Description("Fuente del texto en la barra de título")> _
    Public Property FuenteBarraTitulo() As Font
        Get
            Return _FuenteBarraTitulo
        End Get
        Set(ByVal value As Font)
            _FuenteBarraTitulo = value
        End Set
    End Property

    Private _ColorFuenteBarraTitulo As Color = Color.White
    <System.ComponentModel.Category("Aspecto"), _
     System.ComponentModel.Description("Color de la fuente del texto en la barra de título")> _
    Public Property ColorFuenteBarraTitulo() As Color
        Get
            Return _ColorFuenteBarraTitulo
        End Get
        Set(ByVal value As Color)
            _ColorFuenteBarraTitulo = value
        End Set
    End Property

    Private _Maximizar As Estado_Boton = Estado_Boton.INVISIBLE
    <System.ComponentModel.Category("Aspecto"), _
     System.ComponentModel.Description("Indica el estado del botón Maximizar"), _
     System.ComponentModel.DefaultValue(Estado_Boton.INVISIBLE)> _
    Public Property BotonMaximizar() As Estado_Boton
        Get
            Return _Maximizar
        End Get
        Set(ByVal value As Estado_Boton)
            If value = Estado_Boton.HABILITADO Then
                _puedeMaximizar = True
            Else
                _puedeMaximizar = False
            End If
            _Maximizar = value
        End Set
    End Property

    Private _Minimizar As Estado_Boton = Estado_Boton.INVISIBLE
    <System.ComponentModel.Category("Aspecto"), _
    System.ComponentModel.Description("Indica el estado del botón Minimizar"), _
    System.ComponentModel.DefaultValue(Estado_Boton.INVISIBLE)> _
    Public Property BotonMinimizar() As Estado_Boton
        Get
            Return _Minimizar
        End Get
        Set(ByVal value As Estado_Boton)
            If value = Estado_Boton.HABILITADO Then
                _puedeMinimizar = True
            Else
                _puedeMinimizar = False
            End If
            _Minimizar = value
        End Set
    End Property

    Private _Cerrar As Estado_Boton = Estado_Boton.HABILITADO
    <System.ComponentModel.Category("Aspecto"), _
    System.ComponentModel.Description("Indica el estado del botón Cerrar"), _
    System.ComponentModel.DefaultValue(Estado_Boton.HABILITADO)> _
    Public Property BotonCerrar() As Estado_Boton
        Get
            Return _Cerrar
        End Get
        Set(ByVal value As Estado_Boton)
            If value = Estado_Boton.HABILITADO Then
                _puedeCerrar = True
            Else
                _puedeCerrar = False
            End If
            _Cerrar = value
        End Set
    End Property

    Private _puedeRedimensionar As Boolean = False
    <System.ComponentModel.Category("Aspecto"), System.ComponentModel.Description("Indica si el usuario puede redimensionar el formulario")> _
    Public Property PermiteRedimensionar() As Boolean
        Get
            Return _puedeRedimensionar
        End Get
        Set(ByVal value As Boolean)
            _puedeRedimensionar = value
        End Set
    End Property

    <System.ComponentModel.Category("Aspecto"), System.ComponentModel.Description("Indica si muestra o nó el icono en la barra de titulo de la ventana"), _
        System.ComponentModel.DefaultValue(False)> _
    Protected Overloads Property ShowIcon() As Boolean
        Get
            Return MyBase.ShowIcon
        End Get
        Set(ByVal value As Boolean)
            MyBase.ShowIcon = value
        End Set
    End Property

    Public ReadOnly Property PuedeMinimizar() As Boolean
        Get
            Return _puedeMinimizar
        End Get
    End Property

    Public ReadOnly Property PuedeMaximizar() As Boolean
        Get
            Return _puedeMaximizar
        End Get
    End Property

    Public ReadOnly Property PuedeCerrar() As Boolean
        Get
            Return _puedeCerrar
        End Get
    End Property

    Public ReadOnly Property PuedeRedimensionar() As Boolean
        Get
            Return _puedeRedimensionar
        End Get
    End Property

#End Region

#Region "Constructor"

    Public Sub New()
        ' Creo las figuras
        Me.leftEdge = New GraphicsPath()
        Me.topEdge = New GraphicsPath()
        Me.rightEdge = New GraphicsPath()
        Me.bottomEdge = New GraphicsPath()
        Me.titleBar = New GraphicsPath()
        Me.closeButton = New GraphicsPath()
        Me.maxButton = New GraphicsPath()
        Me.minButton = New GraphicsPath()
        Me.iconButton = New GraphicsPath()
        Me.titleBarBorder = New GraphicsPath()
        Me.bottomleftEdge = New GraphicsPath()
        Me.bottomRightEdge = New GraphicsPath()

        ' Seteo el estilo doblebuffer
        Me.SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        Me.SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
        Me.SetStyle(ControlStyles.UserPaint, True)

        Me.FormBorderStyle = Windows.Forms.FormBorderStyle.None
        Me.ControlBox = False

        ' Agrega el menu del sistema
        Me.SystemMenu = New WindowMenu(Me)
        AddHandler Me.SystemMenu.SystemEvent, AddressOf SystemMenu_SystemEvent

        'tool tip para los botones
        ButtonTip = New ToolTip()

        ' Aparece en el centro de la pantalla
        Me.StartPosition = FormStartPosition.CenterScreen

        Me.KeyPreview = True

        Me.BackColor = Color.FromArgb(224, 224, 224)
        Me.ShowIcon = False
        Me.ShowInTaskbar = False


        Me.Opacity = 0

    End Sub

#End Region

#Region "Métodos"

    Protected Overrides ReadOnly Property CreateParams() As System.Windows.Forms.CreateParams
        Get
            ' Para que se pueda minimizar...
            Const WS_MINIMIZEBOX As Int32 = &H20000
            ' Para que no se pueda cerrar...
            Const CS_NOCLOSE As Int32 = &H200
            Dim cParams As System.Windows.Forms.CreateParams = MyBase.CreateParams
            cParams.Style = cParams.Style Or WS_MINIMIZEBOX
            cParams.ClassStyle = cParams.ClassStyle Or CS_NOCLOSE

            Return cParams

        End Get
    End Property

    Private Function IIf(Of T)(ByVal expresion As Boolean, _
                            ByVal truePart As T, _
                            ByVal falsePart As T) As T
        If expresion Then
            Return truePart
        Else
            Return falsePart
        End If
    End Function

    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Try
            ' Cuando se vence el tiempo de espera hago visible el formulario
            ' y detengo el timer
            sender.stop()
            Me.Opacity = 1
        Catch ex As Exception

        End Try
    End Sub

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        If primerLoad = True Then
            primerLoad = False
            ' Creo los dibujos
            BuildPaths()

            ' Establezco el ancho adecuado del form
            Me.Padding = New Padding(TamanioBorde, titleBarBorder.GetBounds().Height, TamanioBorde, TamanioBorde)

            ' Area de trabajo cuando aparece maximizado
            Me.MaximizedBounds = Screen.GetWorkingArea(Me)

            Dim Reloj As Timer = New Timer
            AddHandler Reloj.Tick, AddressOf Timer1_Tick
            Reloj.Interval = 1
            Reloj.Start()
        End If
    End Sub

    Private Sub Form1_Resize(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Resize
        If (Me.Created) Then
            BuildPaths()
            _MinimizarOver = False
            _MaximizarOver = False
            _CerrarOver = False
            Me.Invalidate()
            Me.ButtonTip.SetToolTip(Me, "")
        End If
    End Sub

    Private Sub Form1_Paint(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PaintEventArgs) Handles MyBase.Paint
        Dim eg As ExtendedGraphics = New ExtendedGraphics
        Dim rc As Rectangle
        Dim bm As Bitmap
        Try
            Dim lgb As LinearGradientBrush = New LinearGradientBrush(titleBarBorder.GetBounds(), Color.FromArgb(135, 148, 156), Color.FromArgb(196, 199, 200), LinearGradientMode.Horizontal)
            'Dim lgb As LinearGradientBrush = New LinearGradientBrush(titleBarBorder.GetBounds(), Color.DodgerBlue, Color.FromArgb(100, 127, 176, 223), LinearGradientMode.Horizontal)

            Me.ColorBarraTitulo = lgb
            e.Graphics.FillPath(ColorBordes, leftEdge)
            e.Graphics.FillPath(ColorBordes, rightEdge)
            e.Graphics.FillPath(ColorBordes, bottomEdge)
            e.Graphics.FillPath(ColorBordeBarraTitulo, titleBarBorder)
            e.Graphics.FillPath(ColorBarraTitulo, titleBar)
            e.Graphics.FillPath(ColorBordes, bottomleftEdge)
            e.Graphics.FillPath(ColorBordes, bottomRightEdge)

            If (Not Me.formActive) Then
                e.Graphics.FillRectangle(ColorBarraTituloInactiva, Rectangle.Round(titleBar.GetBounds()))
            End If

            'Icono del form
            If Me.ShowIcon Then
                rc = Rectangle.Round(iconButton.GetBounds())
                bm = Me.Icon.ToBitmap
                bm.MakeTransparent(Color.Magenta)
                e.Graphics.DrawImage(bm, rc)
            End If

            Minimizar(e)

            ' Título del Form
            Dim textRect As RectangleF = titleBar.GetBounds()
            textRect.X += IIf(Of Long)(Me.ShowIcon, iconButton.GetBounds().Width + 2, 2)
            textRect.Width = textRect.Width - (minButton.GetBounds.Width * 3) - (iconButton.GetBounds.Width) - 15
            TextRenderer.DrawText(e.Graphics, Me.Text, FuenteBarraTitulo, Rectangle.Round(textRect), ColorFuenteBarraTitulo, TextFormatFlags.EndEllipsis Or TextFormatFlags.VerticalCenter)
        Catch ex As Exception

        End Try

    End Sub

    Private Sub Minimizar(ByVal e As System.Windows.Forms.PaintEventArgs)
        Dim rc As Rectangle
        Dim bm As Bitmap

        ' Minimizar
        rc = Rectangle.Round(minButton.GetBounds())
        bm = IIf(_MinimizarOver = False, My.Resources.Imagenes.BtnMinimize, My.Resources.Imagenes.BtnMinimizeOver)
        bm.MakeTransparent(Color.Magenta)
        e.Graphics.DrawImage(bm, rc)

        ' Maximizar
        rc = Rectangle.Round(maxButton.GetBounds())
        If Me.WindowState = FormWindowState.Normal Then
            bm = IIf(_MaximizarOver = False, My.Resources.Imagenes.BtnMaximize, My.Resources.Imagenes.BtnMaximizeOver)
        Else
            'Todo:Cambiar por botón restaurar
            bm = IIf(_MaximizarOver = False, My.Resources.Imagenes.BtnMaximize, My.Resources.Imagenes.BtnMaximizeOver)
        End If
        bm.MakeTransparent(Color.Magenta)
        e.Graphics.DrawImage(bm, rc)

        ' Cerrar
        rc = Rectangle.Round(closeButton.GetBounds())
        bm = IIf(_CerrarOver = False, My.Resources.Imagenes.BtnClose, My.Resources.Imagenes.BtnCloseOver)
        bm.MakeTransparent(Color.Magenta)
        e.Graphics.DrawImage(bm, rc)

    End Sub

    Protected Overrides Sub OnTextChanged(ByVal e As System.EventArgs)
        MyBase.OnTextChanged(e)
        Me.Invalidate()
    End Sub

    Private Sub BuildPaths()
        Dim e As ExtendedGraphics = New ExtendedGraphics()

        ' Con los estilos visuales del XP no se debe alterar el tamanio de la barra de titulo
        ' por eso se hardcodean los valores
        ' Dim buttonSize As Int32 = SystemFonts.CaptionFont.Height
        ' Dim edgeSize As Int32 = SystemInformation.CaptionHeight + 2

        Dim edgeSize As Int32 = 22
        Dim buttonSize As Int32 = 14
        Dim buttonPadding As Int32 = 2

        ' Left Sizing Edge
        leftEdge.Reset()
        leftEdge.AddRectangle(New RectangleF(0, edgeSize, TamanioBorde, Me.Height - (edgeSize * 2)))

        ' Top Sizing Edge
        ' topEdge.Reset()
        ' topEdge.AddRectangle(New Rectangle(edgeSize, 0, Me.Width - (edgeSize * 2), 5))

        'Right Sizing Edge
        rightEdge.Reset()
        rightEdge.AddRectangle(New RectangleF(Me.Width - TamanioBorde, edgeSize, TamanioBorde, Me.Height - (edgeSize * 2)))

        'Bottom Sizing Edge
        bottomEdge.Reset()
        bottomEdge.AddRectangle(New RectangleF(edgeSize, Me.Height - TamanioBorde, Me.Width - (edgeSize * 2), TamanioBorde))

        ' Top-Left Sizing Edge
        'topLeftEdge.Reset()
        ' topLeftEdge.AddRectangle(New Rectangle(0, 0, edgeSize, edgeSize))
        ' topLeftEdge.AddRectangle(New Rectangle(5, 5, edgeSize - 5, edgeSize - 5))

        ''Top-Right Sizing Edge
        'topRightEdge.Reset()
        'topRightEdge.AddRectangle(New Rectangle(Me.Width - edgeSize, 0, edgeSize, edgeSize))
        'topRightEdge.AddRectangle(New Rectangle(Me.Width - edgeSize, 5, edgeSize - 5, edgeSize - 5))

        ''Bottom-Left Sizing Edge
        bottomleftEdge.Reset()
        bottomleftEdge.AddRectangle(New RectangleF(0, Me.Height - edgeSize, edgeSize, edgeSize))
        bottomleftEdge.AddRectangle(New RectangleF(TamanioBorde, Me.Height - edgeSize, edgeSize - TamanioBorde, edgeSize - TamanioBorde))

        ''Bottom-Right Sizing Edge
        bottomRightEdge.Reset()
        bottomRightEdge.AddRectangle(New RectangleF(Me.Width - edgeSize, Me.Height - edgeSize, edgeSize, edgeSize))
        bottomRightEdge.AddRectangle(New RectangleF(Me.Width - edgeSize, Me.Height - edgeSize, edgeSize - TamanioBorde, edgeSize - TamanioBorde))

        Dim posYBotones As Long = (edgeSize / 2) - (buttonSize / 2)

        ' Close Button
        Dim ubicacion As Integer = 1
        closeButton.Reset()
        If _Cerrar <> Estado_Boton.INVISIBLE Then
            closeButton.AddRectangle(New RectangleF(Me.Width - BordeBarraTitulo - ((buttonSize + buttonPadding) * ubicacion), posYBotones, buttonSize, buttonSize))
            titleBar.AddPath(closeButton, False)
            ubicacion = ubicacion + 1
        End If
        maxButton.Reset()
        If _Maximizar <> Estado_Boton.INVISIBLE Then
            ' Maximize Button
            maxButton.AddRectangle(New RectangleF(Me.Width - BordeBarraTitulo - ((buttonSize + buttonPadding) * ubicacion), posYBotones, buttonSize, buttonSize))
            titleBar.AddPath(maxButton, False)
            ubicacion = ubicacion + 1
        End If

        'Minimize Button
        minButton.Reset()
        If _Minimizar <> Estado_Boton.INVISIBLE Then
            minButton.AddRectangle(New RectangleF(Me.Width - BordeBarraTitulo - ((buttonSize + buttonPadding) * ubicacion), posYBotones, buttonSize, buttonSize))
            titleBar.AddPath(minButton, False)
        End If

        ' Window Menu Button (Icon)
        iconButton.Reset()
        iconButton.AddRectangle(New RectangleF(BordeBarraTitulo + 2, posYBotones, buttonSize, buttonSize))
        titleBar.AddPath(iconButton, False)

        titleBarBorder = e.DrawRoundRectangle(0, 0, Me.Width, edgeSize, AnguloEsquina)
        titleBar = e.DrawRoundRectangle(BordeBarraTitulo, BordeBarraTitulo, _
                                        Me.Width - (BordeBarraTitulo * 2), edgeSize - (BordeBarraTitulo * 2), AnguloEsquina)

    End Sub

    <PermissionSet(SecurityAction.Demand, Name:="FullTrust")> _
    Protected Overrides Sub WndProc(ByRef m As System.Windows.Forms.Message)

        'No dejo maximizar si el form no permite
        If m.Msg = User32.NCMouseMessage.WM_NCLBUTTONDBLCLK And Not Me.PuedeMaximizar Then
            m.Msg = User32.WM_NULL
        End If

        ' Si esta maximizado no permite mover...
        If (Me.WindowState = FormWindowState.Maximized) Then
            If (m.Msg = User32.WM_SYSCOMMAND AndAlso _
                m.WParam.ToInt32() = User32.SysCommand.SC_MOVE OrElse _
                m.Msg = User32.NCMouseMessage.WM_NCLBUTTONDOWN AndAlso _
                m.WParam.ToInt32() = User32.NCHitTestResult.HTCAPTION) Then
                m.Msg = User32.WM_NULL
            End If
        End If

        MyBase.WndProc(m)

        Select Case (m.Msg)
            Case User32.WM_GETSYSMENU
                Me.SystemMenu.Show(Me, Me.PointToClient(New Point(m.LParam.ToInt32())))
            Case User32.WM_NCACTIVATE
                Me.formActive = m.WParam.ToInt32() <> 0
                Me.Invalidate()
            Case User32.WM_NCHITTEST
                m.Result = OnNonClientHitTest(m.LParam)
            Case User32.NCMouseMessage.WM_NCLBUTTONUP
                OnNonClientLButtonUp(m.LParam)
            Case User32.NCMouseMessage.WM_NCRBUTTONUP
                OnNonClientRButtonUp(m.LParam)
            Case User32.NCMouseMessage.WM_NCMOUSEMOVE
                OnNonClientMouseMove(m.LParam)
        End Select

    End Sub

    Private Sub OnNonClientLButtonUp(ByVal lParam As IntPtr)
        Dim code As User32.SysCommand = User32.SysCommand.SC_DEFAULT
        Dim point As Point = Me.PointToClient(New Point(lParam.ToInt32()))

        If (Me.closeButton.IsVisible(point)) AndAlso _puedeCerrar Then
            ' Sale de la rutina porque invoca al evento cerrar }
            ' para sobreescribirlo en los forms heredados

            'code = User32.SysCommand.SC_CLOSE
            Invocar_Cerrar()
            Exit Sub
        ElseIf (Me.maxButton.IsVisible(point)) AndAlso _puedeMaximizar Then
            code = IIf(Me.WindowState = FormWindowState.Normal, User32.SysCommand.SC_MAXIMIZE, User32.SysCommand.SC_RESTORE)
        ElseIf (Me.minButton.IsVisible(point)) AndAlso _puedeMinimizar Then
            code = User32.SysCommand.SC_MINIMIZE
        End If

        SendNCWinMessage(User32.WM_SYSCOMMAND, New IntPtr(code), IntPtr.Zero)

    End Sub

    Private Sub OnNonClientRButtonUp(ByVal lParam As IntPtr)
        If (Me.titleBar.IsVisible(Me.PointToClient(New Point(lParam.ToInt32())))) Then
            SendNCWinMessage(User32.WM_GETSYSMENU, IntPtr.Zero, lParam)
        End If
    End Sub

    Private Sub OnNonClientMouseMove(ByVal lParam As IntPtr)
        Dim point As Point = Me.PointToClient(New Point(lParam.ToInt32()))
        Dim tooltip As String

        If (Me.closeButton.IsVisible(point)) Then
            tooltip = "Cerrar"
        ElseIf (Me.maxButton.IsVisible(point)) Then
            tooltip = IIf(Me.WindowState = FormWindowState.Normal, "Maximizar", "Restaurar")
        ElseIf (Me.minButton.IsVisible(point)) Then
            tooltip = "Minimizar"
        ElseIf (Me.iconButton.IsVisible(point)) Then
            tooltip = Me.Text
        Else
            tooltip = String.Empty
        End If

        If (ButtonTip.GetToolTip(Me) <> tooltip) Then
            ButtonTip.SetToolTip(Me, tooltip)
        End If

    End Sub

    Private Function OnNonClientHitTest(ByVal lParam As IntPtr) As IntPtr
        Dim result As User32.NCHitTestResult = User32.NCHitTestResult.HTCLIENT
        Dim point As Point = Me.PointToClient(New Point(lParam.ToInt32()))

        _MinimizarOver = False
        _MaximizarOver = False
        _CerrarOver = False

        If (Me.titleBar.IsVisible(point)) Then
            result = User32.NCHitTestResult.HTCAPTION
        End If

        If (Me.WindowState = FormWindowState.Normal) Then
            If Me._puedeRedimensionar Then
                'If (Me.topLeftEdge.IsVisible(point)) Then
                ' result = User32.NCHitTestResult.HTTOPLEFT
                If (Me.topEdge.IsVisible(point)) Then
                    result = User32.NCHitTestResult.HTTOP
                    'ElseIf (Me.topRightEdge.IsVisible(point)) Then
                    '    result = User32.NCHitTestResult.HTTOPRIGHT
                ElseIf (Me.leftEdge.IsVisible(point)) Then
                    result = User32.NCHitTestResult.HTLEFT
                ElseIf (Me.rightEdge.IsVisible(point)) Then
                    result = User32.NCHitTestResult.HTRIGHT
                ElseIf (Me.bottomleftEdge.IsVisible(point)) Then
                    result = User32.NCHitTestResult.HTBOTTOMLEFT
                ElseIf (Me.bottomEdge.IsVisible(point)) Then
                    result = User32.NCHitTestResult.HTBOTTOM
                ElseIf (Me.bottomRightEdge.IsVisible(point)) Then
                    result = User32.NCHitTestResult.HTBOTTOMRIGHT
                End If
            End If
        End If

        If (Me.closeButton.IsVisible(point)) Then
            result = User32.NCHitTestResult.HTBORDER
            _CerrarOver = True
        ElseIf (Me.maxButton.IsVisible(point)) Then
            result = User32.NCHitTestResult.HTBORDER
            _MaximizarOver = True
        ElseIf (Me.minButton.IsVisible(point)) Then
            result = User32.NCHitTestResult.HTBORDER
            _MinimizarOver = True
        ElseIf (Me.iconButton.IsVisible(point)) Then
            result = User32.NCHitTestResult.HTBORDER
        End If

        Invalidate(Rectangle.Round(titleBar.GetBounds()))

        Return New IntPtr(result)

    End Function

    Private Sub SendNCWinMessage(ByVal msg As Int32, ByVal wParam As IntPtr, ByVal lParam As IntPtr)
        Dim message As Message = message.Create(Me.Handle, msg, wParam, lParam)
        Me.WndProc(message)
    End Sub

    Protected Sub SystemMenu_SystemEvent(ByVal sender As Object, ByVal ev As WindowMenuEventArgs)
        SendNCWinMessage(User32.WM_SYSCOMMAND, New IntPtr(ev.SystemCommand), IntPtr.Zero)
    End Sub

    Private Sub InitializeComponent()
        Me.SuspendLayout()
        '
        'frmFormProduccion
        '
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.ClientSize = New System.Drawing.Size(311, 287)
        Me.DoubleBuffered = True
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.KeyPreview = True
        Me.Name = "frmFormProduccion"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.ResumeLayout(False)

    End Sub

    Public Sub Invocar_Cerrar()
        If _puedeCerrar Then
            RaiseEvent Cerrar_Clicked(Me, New EventArgs())
        End If
    End Sub

#End Region

End Class

Friend NotInheritable Class User32

    Public Const WM_NULL As Int32 = &H0
    Public Const WM_NCHITTEST As Int32 = &H84
    Public Const WM_NCACTIVATE As Int32 = &H86
    Public Const WM_SYSCOMMAND As Int32 = &H112
    Public Const WM_ENTERMENULOOP As Int32 = &H211
    Public Const WM_EXITMENULOOP As Int32 = &H212
    '********** Undocumented message **********
    Public Const WM_GETSYSMENU As Int32 = &H313
    '*****************************************

    Private Sub New()
        MyBase.New()
        'Non-Instantiable class
    End Sub

    Public Enum SysCommand
        SC_SIZE = &HF000
        SC_MOVE = &HF010
        SC_MINIMIZE = &HF020
        SC_MAXIMIZE = &HF030
        SC_NEXTWINDOW = &HF040
        SC_PREVWINDOW = &HF050
        SC_CLOSE = &HF060
        SC_VSCROLL = &HF070
        SC_HSCROLL = &HF080
        SC_MOUSEMENU = &HF090
        SC_KEYMENU = &HF100
        SC_ARRANGE = &HF110
        SC_RESTORE = &HF120
        SC_TASKLIST = &HF130
        SC_SCREENSAVE = &HF140
        SC_HOTKEY = &HF150
        SC_DEFAULT = &HF160
        SC_MONITORPOWER = &HF170
        SC_CONTEXTHELP = &HF180
        SC_SEPARATOR = &HF00F
    End Enum

    Public Enum NCHitTestResult
        HTERROR = (-2)
        HTTRANSPARENT
        HTNOWHERE
        HTCLIENT
        HTCAPTION
        HTSYSMENU
        HTGROWBOX
        HTMENU
        HTHSCROLL
        HTVSCROLL
        HTMINBUTTON
        HTMAXBUTTON
        HTLEFT
        HTRIGHT
        HTTOP
        HTTOPLEFT
        HTTOPRIGHT
        HTBOTTOM
        HTBOTTOMLEFT
        HTBOTTOMRIGHT
        HTBORDER
        HTOBJECT
        HTCLOSE
        HTHELP
    End Enum

    Public Enum NCMouseMessage
        WM_NCMOUSEMOVE = &HA0
        WM_NCLBUTTONDOWN = &HA1
        WM_NCLBUTTONUP = &HA2
        WM_NCLBUTTONDBLCLK = &HA3
        WM_NCRBUTTONDOWN = &HA4
        WM_NCRBUTTONUP = &HA5
        WM_NCRBUTTONDBLCLK = &HA6
        WM_NCMBUTTONDOWN = &HA7
        WM_NCMBUTTONUP = &HA8
        WM_NCMBUTTONDBLCLK = &HA9
        WM_NCXBUTTONDOWN = &HAB
        WM_NCXBUTTONUP = &HAC
        WM_NCXBUTTONDBLCLK = &HAD
    End Enum

    Public Shared Function MakeLParam(ByVal LoWord As Int32, ByVal HiWord As Int32) As IntPtr
        Return New IntPtr((HiWord << 16) Or (LoWord And &HFFFF))
    End Function

End Class

Friend NotInheritable Class WindowMenu
    Inherits ContextMenu

    Private Owner As frmBase
    Private menuRestore, menuMove, menuSize, menuMin, menuMax, menuSep, menuClose As MenuItem

    Public Event SystemEvent As WindowMenuEventHandler

    Public Sub New(ByVal owner As Form)
        MyBase.New()
        Me.Owner = owner

        menuRestore = CreateMenuItem("Restaurar")
        menuMove = CreateMenuItem("Mover")
        menuSize = CreateMenuItem("Tamaño")
        menuMin = CreateMenuItem("Minimizar")
        menuMax = CreateMenuItem("Maximizar")
        menuSep = CreateMenuItem("-")
        menuClose = CreateMenuItem("Cerrar", Shortcut.AltF4)

        Me.MenuItems.AddRange(New MenuItem() {menuRestore, menuMove, menuSize, menuMin, menuMax, menuSep, menuClose})

        menuClose.DefaultItem = True
    End Sub

    Protected Overrides Sub OnPopup(ByVal e As EventArgs)

        Select Case Owner.WindowState
            Case FormWindowState.Normal
                menuRestore.Enabled = False
                menuMax.Enabled = True And Owner.PuedeMaximizar
                menuMin.Enabled = True And Owner.PuedeMinimizar
                menuMove.Enabled = True
                menuSize.Enabled = True And Owner.PuedeRedimensionar
                menuClose.Enabled = True And Owner.PuedeCerrar
            Case FormWindowState.Minimized
                menuRestore.Enabled = True
                menuMax.Enabled = True And Owner.PuedeMaximizar
                menuMin.Enabled = False And Owner.PuedeMinimizar
                menuMove.Enabled = False
                menuSize.Enabled = False And Owner.PuedeRedimensionar
                menuClose.Enabled = True And Owner.PuedeCerrar
            Case FormWindowState.Maximized
                menuRestore.Enabled = True
                menuMax.Enabled = False And Owner.PuedeMaximizar
                menuMin.Enabled = True And Owner.PuedeMinimizar
                menuMove.Enabled = False
                menuSize.Enabled = False And Owner.PuedeRedimensionar
                menuClose.Enabled = True And Owner.PuedeCerrar
        End Select

        MyBase.OnPopup(e)

    End Sub

    Private Sub OnWindowMenuClick(ByVal sender As Object, ByVal e As EventArgs)

        Select Case Me.MenuItems.IndexOf(DirectCast(sender, MenuItem))
            Case 0
                SendSysCommand(User32.SysCommand.SC_RESTORE)
            Case 1
                SendSysCommand(User32.SysCommand.SC_MOVE)
            Case 2
                SendSysCommand(User32.SysCommand.SC_SIZE)
            Case 3
                SendSysCommand(User32.SysCommand.SC_MINIMIZE)
            Case 4
                SendSysCommand(User32.SysCommand.SC_MAXIMIZE)
            Case 6
                ' Invoca al evento cerrar.. 
                Me.Owner.Invocar_Cerrar()
                Exit Sub
                'SendSysCommand(User32.SysCommand.SC_CLOSE)
        End Select
    End Sub

    Private Function CreateMenuItem(ByVal text As String) As MenuItem
        Return CreateMenuItem(text, Shortcut.None)
    End Function

    Private Function CreateMenuItem(ByVal text As String, ByVal shortcut As Shortcut) As MenuItem
        Dim item As MenuItem = New MenuItem(text, AddressOf OnWindowMenuClick, shortcut)
        item.OwnerDraw = True
        AddHandler item.MeasureItem, AddressOf item_MeasureItem
        AddHandler item.DrawItem, AddressOf item_DrawItem
        Return item
    End Function

    Private Sub item_MeasureItem(ByVal sender As Object, ByVal e As MeasureItemEventArgs)
        Dim item As MenuItem = Me.MenuItems(e.Index)
        Dim itemText As String = item.Text
        itemText += "/tAlt+F4"
        Dim itemSize As Size = TextRenderer.MeasureText(itemText, SystemFonts.MenuFont)
        e.ItemHeight = IIf(e.Index = 5, 8, itemSize.Height + 7)
        e.ItemWidth = itemSize.Width + itemSize.Height + 23
    End Sub

    Private Sub item_DrawItem(ByVal sender As Object, ByVal e As DrawItemEventArgs)

        Dim item As MenuItem = Me.MenuItems(e.Index)

        If item.Enabled Then
            e.DrawBackground()
        Else
            e.Graphics.FillRectangle(SystemBrushes.Menu, e.Bounds)
        End If

        If e.Index = 5 Then
            e.Graphics.DrawLine(SystemPens.GrayText, e.Bounds.Left + 2, e.Bounds.Top + 3, e.Bounds.Right - 2, e.Bounds.Top + 3)
        Else
            Dim format As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.NoPadding

            Dim textColor As Color = IIf(item.Enabled, SystemColors.MenuText, SystemColors.GrayText)

            Using marlettFont As New Font("Marlett", 10)
                Dim GlyphRect As Rectangle = e.Bounds
                GlyphRect.Width = GlyphRect.Height

                If item Is menuRestore Then
                    TextRenderer.DrawText(e.Graphics, "2", marlettFont, GlyphRect, textColor, format)
                ElseIf item Is menuMin Then
                    TextRenderer.DrawText(e.Graphics, "0", marlettFont, GlyphRect, textColor, format)
                ElseIf item Is menuMax Then
                    TextRenderer.DrawText(e.Graphics, "1", marlettFont, GlyphRect, textColor, format)
                ElseIf item Is menuClose Then
                    TextRenderer.DrawText(e.Graphics, "r", marlettFont, GlyphRect, textColor, format)
                End If
            End Using

            format = format And (TextFormatFlags.Left Or Not TextFormatFlags.HorizontalCenter)

            Dim textRect As Rectangle = New Rectangle(e.Bounds.Left + e.Bounds.Height + 3, e.Bounds.Top, e.Bounds.Width - e.Bounds.Height - 3, e.Bounds.Height)

            TextRenderer.DrawText(e.Graphics, item.Text, SystemFonts.MenuFont, textRect, textColor, format)

            If (item Is menuClose) Then
                Dim shortcut As String = "Alt+F4"
                Dim shortcutSize As Size = TextRenderer.MeasureText(shortcut, SystemFonts.MenuFont)
                textRect.X = textRect.Right - shortcutSize.Width - 13
                TextRenderer.DrawText(e.Graphics, shortcut, SystemFonts.MenuFont, textRect, textColor, format)
            End If

        End If

    End Sub

    Private Function IIf(Of T)(ByVal expresion As Boolean, _
                            ByVal truePart As T, _
                            ByVal falsePart As T) As T
        If expresion Then
            Return truePart
        Else
            Return falsePart
        End If
    End Function

    Private Sub SendSysCommand(ByVal command As User32.SysCommand)
        Dim ev As WindowMenuEventArgs = New WindowMenuEventArgs(DirectCast(command, Int32))
        RaiseEvent SystemEvent(Me, ev)
    End Sub

End Class

Public Class WindowMenuEventArgs
    Inherits EventArgs

    Private sysCommand As Int32

    Public ReadOnly Property SystemCommand() As Int32
        Get
            Return Me.sysCommand
        End Get
    End Property

    Public Sub New(ByVal command As Int32)
        MyBase.new()
        Me.sysCommand = command
    End Sub

End Class

Public Delegate Sub WindowMenuEventHandler(ByVal sender As Object, ByVal ev As WindowMenuEventArgs)

' A simple extension to the Graphics class for extended 
' graphic routines, such, 
' as for creating rounded rectangles. 
' Because, Graphics class is an abstract class, 
' that is why it can not be inherited. Although,
' I have provided a simple constructor 
' that builds the ExtendedGraphics object around a 
' previously created Graphics object. 
' Please contact: aaronreginald@yahoo.com for the most 
' recent implementations of
' this class. 
Public Class ExtendedGraphics


#Region "Fills a Rounded Rectangle with integers."
    Public Function FillRoundRectangle(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal radius As Integer) As GraphicsPath

        Dim fx As Single = Convert.ToSingle(x)
        Dim fy As Single = Convert.ToSingle(y)
        Dim fwidth As Single = Convert.ToSingle(width)
        Dim fheight As Single = Convert.ToSingle(height)
        Dim fradius As Single = Convert.ToSingle(radius)

        Return Me.FillRoundRectangle(fx, fy, fwidth, fheight, fradius)
    End Function
#End Region


#Region "Fills a Rounded Rectangle with continuous numbers."
    Public Function FillRoundRectangle(ByVal x As Single, ByVal y As Single, ByVal width As Single, ByVal height As Single, ByVal radius As Single) As GraphicsPath
        Dim rectangle As New RectangleF(x, y, width, height)
        Dim path As GraphicsPath = Me.GetRoundedRect(rectangle, radius)
        Return path
    End Function
#End Region


#Region "Draws a Rounded Rectangle border with integers."
    Public Function DrawRoundRectangle(ByVal x As Integer, ByVal y As Integer, ByVal width As Integer, ByVal height As Integer, ByVal radius As Integer) As GraphicsPath
        Dim fx As Single = Convert.ToSingle(x)
        Dim fy As Single = Convert.ToSingle(y)
        Dim fwidth As Single = Convert.ToSingle(width)
        Dim fheight As Single = Convert.ToSingle(height)
        Dim fradius As Single = Convert.ToSingle(radius)
        Return DrawRoundRectangle(fx, fy, fwidth, fheight, fradius)
    End Function
#End Region


#Region "Draws a Rounded Rectangle border with continuous numbers."
    Public Function DrawRoundRectangle(ByVal x As Single, ByVal y As Single, ByVal width As Single, ByVal height As Single, ByVal radius As Single) As GraphicsPath
        Dim rectangle As New RectangleF(x, y, width, height)
        Dim path As GraphicsPath = Me.GetRoundedRect(rectangle, radius)
        Return path
    End Function
#End Region


#Region "Get the desired Rounded Rectangle path."
    Private Function GetRoundedRect(ByVal baseRect As RectangleF, ByVal radius As Single) As GraphicsPath
        ' if corner radius is less than or equal to zero, 

        ' return the original rectangle 

        If radius <= 0.0F Then
            Dim mPath As New GraphicsPath()
            mPath.AddRectangle(baseRect)
            mPath.CloseFigure()
            Return mPath
        End If

        ' if the corner radius is greater than or equal to 

        ' half the width, or height (whichever is shorter) 

        ' then return a capsule instead of a lozenge 

        If radius >= (Math.Min(baseRect.Width, baseRect.Height)) / 2.0R Then
            Return GetCapsule(baseRect)
        End If

        ' create the arc for the rectangle sides and declare 

        ' a graphics path object for the drawing 

        Dim diameter As Single = radius * 2.0F
        Dim sizeF As New SizeF(diameter, diameter)
        Dim arc As New RectangleF(baseRect.Location, sizeF)
        Dim path As GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()

        ' top left arc 

        path.AddArc(arc, 180, 90)

        ' top right arc 

        arc.X = baseRect.Right - diameter
        path.AddArc(arc, 270, 90)

        ' bottom right arc 

        arc.Y = baseRect.Bottom - diameter
        path.AddArc(arc, 0, 90)

        ' bottom left arc

        arc.X = baseRect.Left
        path.AddArc(arc, 90, 90)

        path.CloseFigure()
        Return path

    End Function

#End Region

    Private Function GetCapsule(ByVal baseRect As RectangleF) As GraphicsPath
        Dim diameter As Single
        Dim arc As RectangleF
        Dim path As GraphicsPath = New System.Drawing.Drawing2D.GraphicsPath()
        Try
            If baseRect.Width > baseRect.Height Then
                ' return horizontal capsule 

                diameter = baseRect.Height
                Dim sizeF As New SizeF(diameter, diameter)
                arc = New RectangleF(baseRect.Location, sizeF)
                path.AddArc(arc, 90, 180)
                arc.X = baseRect.Right - diameter
                path.AddArc(arc, 270, 180)
            ElseIf baseRect.Width < baseRect.Height Then
                ' return vertical capsule 

                diameter = baseRect.Width
                Dim sizeF As New SizeF(diameter, diameter)
                arc = New RectangleF(baseRect.Location, sizeF)
                path.AddArc(arc, 180, 180)
                arc.Y = baseRect.Bottom - diameter
                path.AddArc(arc, 0, 180)
            Else
                ' return circle 

                path.AddEllipse(baseRect)
            End If
        Catch ex As Exception
            path.AddEllipse(baseRect)
        Finally
            path.CloseFigure()
        End Try
        Return path
    End Function

End Class