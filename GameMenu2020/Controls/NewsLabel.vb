Public Class NewsLabel
    Inherits Control

    Private _pubDate As String = Now.ToShortDateString
    Public Property PubDate() As String
        Get
            Return _pubDate
        End Get
        Set(value As String)
            Try
                Dim _date As DateTime = DateTime.Parse(value)
                _pubDate = $"{_date.ToShortDateString} {_date.ToShortTimeString}"
            Catch ex As Exception
                _pubDate = value
            End Try
        End Set
    End Property

    Private _text As String = Name
    Public Shadows Property Text() As String
        Get
            Return _text
        End Get
        Set(value As String)
            _text = value
        End Set
    End Property

    Private _dateColor As Color = Color.Gray
    Public Property DateColor() As Color
        Get
            Return _dateColor
        End Get
        Set(value As Color)
            _dateColor = value
        End Set
    End Property

    Private _URL As String
    Public Property URL() As String
        Get
            Return _URL
        End Get
        Set(value As String)
            _URL = value
        End Set
    End Property

    Private _image As Image
    Public Property Image() As Image
        Get
            Return _image
        End Get
        Set(value As Image)
            _image = value
        End Set
    End Property

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim dateWidth As Integer = TextRenderer.MeasureText(e.Graphics, _pubDate, Font).Width
        Dim textWidth As Integer = TextRenderer.MeasureText(e.Graphics, _text, Font).Width

        e.Graphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        TextRenderer.DrawText(e.Graphics, _pubDate, Font, New Point(0, 2), _dateColor)
        TextRenderer.DrawText(e.Graphics, _text, Font, New Point(dateWidth + 2, 2), ForeColor)
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)

        Process.Start(_URL)
    End Sub

    Public Sub New()
        SetStyle(ControlStyles.SupportsTransparentBackColor, True)
    End Sub
End Class
