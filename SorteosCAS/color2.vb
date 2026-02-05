Imports System.IO
Imports System.Drawing


Public Class color2
    Inherits Windows.Forms.ProfessionalColorTable
    Public Overrides ReadOnly Property MenuItemSelectedGradientBegin() As System.Drawing.Color

        Get
            'MsgBox(MenuItemSelectedGradientBegin.ToArgb)

            Return Color.Transparent

        End Get

    End Property
    Public Overrides ReadOnly Property MenuItemSelectedGradientEnd() As System.Drawing.Color

        Get
            Return Color.Transparent

        End Get

    End Property

    Public Overrides ReadOnly Property MenuItemBorder() As System.Drawing.Color

        Get


            Return Color.Transparent

        End Get

    End Property
End Class
