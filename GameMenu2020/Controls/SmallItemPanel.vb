Imports System.Drawing.Drawing2D

Public Class SmallItemPanel
    Inherits Panel

    Private clr1 As Color = Color.Transparent
    Private clr2 As Color = Color.Black
    Private clr3 As Color = Color.FromArgb(114, 137, 218)
    Private btnclr As Color = Color.FromArgb(67, 181, 129)

    Public Property ColorUp() As Color
        Get
            Return clr1
        End Get
        Set(value As Color)
            clr1 = value
        End Set
    End Property

    Public Property ColorDown() As Color
        Get
            Return clr2
        End Get
        Set(value As Color)
            clr2 = value
        End Set
    End Property

    Public Property ColorBehind() As Color
        Get
            Return clr3
        End Get
        Set(value As Color)
            clr3 = value
        End Set
    End Property

    Public Property buttonColor() As Color
        Get
            Return btnclr
        End Get
        Set(value As Color)
            btnclr = value
        End Set
    End Property

    Private _title As String = "Title"
    Public Property Title() As String
        Get
            Return _title
        End Get
        Set(value As String)
            _title = value
        End Set
    End Property

    Private _buttonText As String = "Start"
    Public Property buttonText() As String
        Get
            Return _buttonText
        End Get
        Set(value As String)
            _buttonText = value
        End Set
    End Property

    Private _image As Image = New Bitmap(Width, Height)
    Public Property Image() As Image
        Get
            Return _image
        End Get
        Set(value As Image)
            _image = value
        End Set
    End Property

    Private _imgLayout As ImageLayout = ImageLayout.Zoom
    Public Property ImageLayout() As ImageLayout
        Get
            Return _imgLayout
        End Get
        Set(value As ImageLayout)
            _imgLayout = value
        End Set
    End Property

    Public Sub New()
        DoubleBuffered = True
    End Sub

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim control As Color = SystemColors.Control
        Dim window As Color = SystemColors.Window

        Dim formGraphics As Graphics = e.Graphics
        Dim gBrush As New LinearGradientBrush(New Point(0, 0), New Point(0, Height), clr1, clr2)
        formGraphics.InterpolationMode = InterpolationMode.NearestNeighbor
        formGraphics.PixelOffsetMode = PixelOffsetMode.Half
        formGraphics.SmoothingMode = SmoothingMode.AntiAlias

        DrawRoundedRectangle(formGraphics, ClientRectangle, 20, New Pen(BackColor))
        FillRoundedRectangle(formGraphics, ClientRectangle, 20, New SolidBrush(clr3))
        Dim tbrush As New TextureBrush(_image)
        Select Case _imgLayout
            Case ImageLayout.Tile
                tbrush.WrapMode = WrapMode.Tile
                formGraphics.FillRectangle(tbrush, New RectangleF(10, 10, Width - 20, Width - 20))
            Case ImageLayout.Stretch
                formGraphics.DrawImage(_image, New RectangleF(10, 10, Width - 20, Width - 20))
            Case ImageLayout.Center
                tbrush.WrapMode = WrapMode.Clamp
                Dim displayArea As New Rectangle(10, 10, Width - 20, Width - 20)
                Dim xDisplayCenterRelative As New Point(displayArea.Width / 2, displayArea.Height / 2)
                Dim xImageCenterRelative As New Point(_image.Width / 2, _image.Height / 2)
                Dim xOffsetRelative As New Point(xDisplayCenterRelative.X - xImageCenterRelative.X, xDisplayCenterRelative.Y - xImageCenterRelative.Y)
                Dim xAbsolutePixel As Point = xOffsetRelative + New Size(displayArea.Location)
                tbrush.TranslateTransform(xAbsolutePixel.X, xAbsolutePixel.Y)
                formGraphics.FillRectangle(tbrush, New RectangleF(10, 10, Width - 20, Height - 20))
            Case ImageLayout.None
                formGraphics.DrawImage(_image, New RectangleF(10, 10, _image.Width, _image.Height))
            Case ImageLayout.Zoom
                Dim aspectRatio As Double
                Dim newHeight, newWidth As Integer
                Dim maxWidth As Integer = Width - 20
                Dim maxHeight As Integer = Width - 20

                If _image.Width > maxWidth Or _image.Height > maxHeight Then
                    If _image.Width >= _image.Height Then ' image is wider than tall
                        newWidth = maxWidth
                        aspectRatio = _image.Width / maxWidth
                        newHeight = CInt(_image.Height / aspectRatio)
                    Else ' image is taller than wide
                        newHeight = maxHeight
                        aspectRatio = _image.Height / maxHeight
                        newWidth = CInt(_image.Width / aspectRatio)
                    End If
                Else
                    If _image.Width > _image.Height Then
                        newWidth = maxWidth
                        aspectRatio = _image.Width / maxWidth
                        newHeight = CInt(_image.Height / aspectRatio)
                    Else
                        newHeight = maxHeight
                        aspectRatio = _image.Height / maxHeight
                        newWidth = CInt(_image.Width / aspectRatio)
                    End If
                End If

                Dim newX As Integer = (Width - newWidth) / 2
                Dim newY As Integer = (Height - newHeight) / 2

                formGraphics.DrawImage(_image, New RectangleF(newX, newY, newWidth, newHeight))
        End Select

        FillRoundedRectangle(formGraphics, ClientRectangle, 20, gBrush)

        Dim format As StringFormat = StringFormat.GenericDefault
        format.Alignment = StringAlignment.Center
        format.LineAlignment = StringAlignment.Near
        format.Trimming = StringTrimming.EllipsisWord

        formGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias

        If _mouseEnter Then
            DrawRoundedRectangle(formGraphics, New Rectangle(10, Height - 55, Width - 20, 40), 20, New Pen(BackColor))
            FillRoundedRectangle(formGraphics, New Rectangle(10, Height - 55, Width - 20, 40), 20, New SolidBrush(btnclr))
            'formGraphics.DrawString(_buttonText, Font, New SolidBrush(ForeColor), New RectangleF(10, Height - 50, Width - 20, 40), format)
            Using f As Font = New Font(Font.Name, Font.Size, Font.Style, GraphicsUnit.Point)
                Dim rect As New Rectangle(5, Height - 65, Width - 10, 60)
                Dim flags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak
                TextRenderer.DrawText(formGraphics, _buttonText, f, rect, ForeColor, flags)
                formGraphics.DrawRectangle(Pens.Transparent, rect)
            End Using
        Else
            'formGraphics.DrawString(_title, Font, New SolidBrush(ForeColor), New RectangleF(10, Height - 50, Width - 20, 40), format)
            Using f As Font = New Font(Font.Name, Font.Size, Font.Style, GraphicsUnit.Point)
                Dim rect As New Rectangle(5, Height - 65, Width - 10, 60)
                Dim flags As TextFormatFlags = TextFormatFlags.HorizontalCenter Or TextFormatFlags.VerticalCenter Or TextFormatFlags.WordBreak
                TextRenderer.DrawText(formGraphics, _title, f, rect, ForeColor, flags)
                formGraphics.DrawRectangle(Pens.Transparent, rect)
            End Using
        End If
    End Sub

    Private _mouseEnter As Boolean = False

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)

        _mouseEnter = True
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)

        _mouseEnter = False
    End Sub

    Protected Shadows Sub Update()
        Refresh()
    End Sub

    Private Sub DrawRoundedRectangle(ByVal g As Graphics, ByVal r As Rectangle, ByVal d As Integer, ByVal p As Pen)
        g.DrawArc(p, r.X, r.Y, d, d, 180, 90)
        g.DrawLine(p, CInt(r.X + d / 2), r.Y, CInt(r.X + r.Width - d / 2), r.Y)
        g.DrawArc(p, r.X + r.Width - d, r.Y, d, d, 270, 90)
        g.DrawLine(p, r.X, CInt(r.Y + d / 2), r.X, CInt(r.Y + r.Height - d / 2))
        g.DrawLine(p, CInt(r.X + r.Width), CInt(r.Y + d / 2), CInt(r.X + r.Width), CInt(r.Y + r.Height - d / 2))
        g.DrawLine(p, CInt(r.X + d / 2), CInt(r.Y + r.Height), CInt(r.X + r.Width - d / 2), CInt(r.Y + r.Height))
        g.DrawArc(p, r.X, r.Y + r.Height - d, d, d, 90, 90)
        g.DrawArc(p, r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90)
    End Sub

    Private Sub FillRoundedRectangle(ByVal g As Graphics, ByVal r As Rectangle, ByVal d As Integer, ByVal b As Brush)
        Dim mode As SmoothingMode = g.SmoothingMode
        g.SmoothingMode = SmoothingMode.HighSpeed
        g.FillPie(b, r.X, r.Y, d, d, 180, 90)
        g.FillPie(b, r.X + r.Width - d, r.Y, d, d, 270, 90)
        g.FillPie(b, r.X, r.Y + r.Height - d, d, d, 90, 90)
        g.FillPie(b, r.X + r.Width - d, r.Y + r.Height - d, d, d, 0, 90)
        g.FillRectangle(b, CInt(r.X + d / 2), r.Y, r.Width - d, CInt(d / 2))
        g.FillRectangle(b, r.X, CInt(r.Y + d / 2), r.Width, CInt(r.Height - d))
        g.FillRectangle(b, CInt(r.X + d / 2), CInt(r.Y + r.Height - d / 2), CInt(r.Width - d), CInt(d / 2))
        g.SmoothingMode = mode
    End Sub
End Class
