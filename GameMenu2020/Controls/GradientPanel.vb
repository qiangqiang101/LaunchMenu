Imports System.Drawing.Drawing2D

Public Class GradientPanel
    Inherits Panel

    Protected Overrides Sub OnPaint(ByVal e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim control As Color = SystemColors.Control
        Dim window As Color = SystemColors.Window

        Dim formGraphics As Graphics = e.Graphics
        formGraphics.InterpolationMode = InterpolationMode.NearestNeighbor
        formGraphics.PixelOffsetMode = PixelOffsetMode.Half
        formGraphics.SmoothingMode = SmoothingMode.AntiAlias

        If leftright Then
            Dim gBrush As New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(Width, 0), clr1, clr2)
            formGraphics.FillRectangle(gBrush, ClientRectangle)
        Else
            Dim gBrush As New Drawing2D.LinearGradientBrush(New Point(0, 0), New Point(0, Height), clr1, clr2)
            formGraphics.FillRectangle(gBrush, ClientRectangle)
        End If

        Dim format As StringFormat = StringFormat.GenericDefault
        format.Alignment = StringAlignment.Near
        format.Trimming = StringTrimming.EllipsisWord

        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        e.Graphics.DrawString(_title, New Font("Helvetica", 17S, FontStyle.Bold), Brushes.White, New RectangleF(20, Height - 100, Width - 40, 30), format)
        e.Graphics.DrawString(_subtitle, New Font("Helvetica", 14S, FontStyle.Regular), New SolidBrush(Color.FromArgb(170, 172, 175)), New RectangleF(20, Height - 70, Width - 40, 25), format)
        e.Graphics.DrawString(_pubdate, New Font("Helvetica", 10S, FontStyle.Bold), New SolidBrush(Color.FromArgb(114, 118, 125)), New RectangleF(20, Height - 45, Width - 40, 15), format)
    End Sub

    Dim clr1 As Color = Color.Transparent
    Dim clr2 As Color = Color.Black

    Property Color1() As Color
        Get
            Return clr1
        End Get
        Set(ByVal value As Color)
            clr1 = value
        End Set
    End Property

    Property Color2() As Color
        Get
            Return clr2
        End Get
        Set(ByVal value As Color)
            clr2 = value
        End Set
    End Property

    Dim leftright As Boolean
    Property LeftToRight() As Boolean
        Get
            Return leftright
        End Get
        Set(value As Boolean)
            leftright = value
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

    Private _subtitle As String = "Subtitle"
    Public Property Subtitle() As String
        Get
            Return _subtitle
        End Get
        Set(value As String)
            _subtitle = value
        End Set
    End Property

    Private _pubdate As String = Now.ToShortDateString
    Public Property PubDate() As String
        Get
            Return _pubdate
        End Get
        Set(value As String)
            Try
                Dim _date As DateTime = DateTime.Parse(value)
                _pubdate = $"{_date.ToLongDateString} {_date.ToLongTimeString}"
            Catch ex As Exception
                _pubdate = value
            End Try
        End Set
    End Property

    Public Sub New()
        DoubleBuffered = True
    End Sub

    Private _radius As Integer = 20
    Public Property Radius() As Integer
        Get
            Return _radius
        End Get
        Set(value As Integer)
            _radius = value
        End Set
    End Property

    Private _backcolor As Color = BackColor
    Public Property RoundedBackColor() As Color
        Get
            Return _backcolor
        End Get
        Set(value As Color)
            _backcolor = value
        End Set
    End Property

    Private Sub SliderLItem_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Dim rect As Rectangle = ClientRectangle
        rect.X = rect.X + 1
        rect.Y = rect.Y + 1
        rect.Width -= 2
        rect.Height -= 2
        Using bb As GraphicsPath = GetPath(rect, _radius)
            Using br As New SolidBrush(RoundedBackColor)
                e.Graphics.FillPath(br, bb)
            End Using
        End Using
    End Sub

    Protected Function GetPath(ByVal rc As Rectangle, ByVal r As Int32) As GraphicsPath
        Dim x As Int32 = rc.X, y As Int32 = rc.Y, w As Int32 = rc.Width, h As Int32 = rc.Height
        r = r << 1
        Dim path As GraphicsPath = New GraphicsPath()
        If r > 0 Then
            If (r > h) Then r = h
            If (r > w) Then r = w
            path.AddArc(x, y, r, r, 180, 90)
            path.AddArc(x + w - r, y, r, r, 270, 90)
            path.AddArc(x + w - r, y + h - r, r, r, 0, 90)
            path.AddArc(x, y + h - r, r, r, 90, 90)
            path.CloseFigure()
        Else
            path.AddRectangle(rc)
        End If
        Return path
    End Function
End Class
