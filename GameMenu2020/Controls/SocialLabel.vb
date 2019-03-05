Public Class SocialLabel
    Inherits Label

    Dim FontAwesome As Font
    Dim FAFont As New Text.PrivateFontCollection()

    Public Sub New()
        FAFont.AddFontFile($"{Application.StartupPath}\Data\Fonts\Font Awesome 5 Brands-Regular-400.otf")
        FontAwesome = New Font(FAFont.Families(0), 23.0F, FontStyle.Regular, GraphicsUnit.Pixel)
        Font = FontAwesome
        AutoSize = False
        Size = New Size(CInt(FontAwesome.Size + 10), CInt(FontAwesome.Size))
        ForeColor = _nColor
        Cursor = Cursors.Hand
        TextAlign = ContentAlignment.MiddleCenter
    End Sub

    Public Enum SocialLogo
        Facebook
        GooglePlus
        Twitter
        Youtube
        VK
        Discord
        LinkedIn
        Pinterest
        Instagram
        Tumblr
        Flickr
        Reddit
        Snapchat
        WhatsApp
        Weibo
        TencentWeibo
        QQ
    End Enum

    Private _logo As SocialLogo = SocialLogo.Facebook
    Public Property Logo() As SocialLogo
        Get
            Return _logo
        End Get
        Set(value As SocialLogo)
            _logo = value
            Text = GetLogoText(value)
        End Set
    End Property

    Private _hColor As Color = Color.White
    Public Property HoverColor() As Color
        Get
            Return _hColor
        End Get
        Set(value As Color)
            _hColor = value
        End Set
    End Property

    Private _nColor As Color = Color.Silver
    Public Property NormalColor() As Color
        Get
            Return _nColor
        End Get
        Set(value As Color)
            _nColor = value
        End Set
    End Property

    Public Property URL() As String

    Private Function GetLogoText(logo As SocialLogo) As String
        Select Case logo
            Case SocialLogo.Discord
                Return ""
            Case SocialLogo.Facebook
                Return ""
            Case SocialLogo.Flickr
                Return ""
            Case SocialLogo.GooglePlus
                Return ""
            Case SocialLogo.Instagram
                Return ""
            Case SocialLogo.LinkedIn
                Return ""
            Case SocialLogo.Pinterest
                Return ""
            Case SocialLogo.QQ
                Return ""
            Case SocialLogo.Reddit
                Return ""
            Case SocialLogo.Snapchat
                Return ""
            Case SocialLogo.TencentWeibo
                Return ""
            Case SocialLogo.Tumblr
                Return ""
            Case SocialLogo.Twitter
                Return ""
            Case SocialLogo.VK
                Return ""
            Case SocialLogo.Weibo
                Return ""
            Case SocialLogo.WhatsApp
                Return ""
            Case SocialLogo.Youtube
                Return ""
            Case Else
                Return Nothing
        End Select
    End Function

    Protected Overrides Sub OnMouseEnter(e As EventArgs)
        MyBase.OnMouseEnter(e)

        ForeColor = _hColor
    End Sub

    Protected Overrides Sub OnMouseLeave(e As EventArgs)
        MyBase.OnMouseLeave(e)

        ForeColor = _nColor
    End Sub

    Protected Overrides Sub OnClick(e As EventArgs)
        MyBase.OnClick(e)

        Process.Start(URL)
    End Sub

End Class
