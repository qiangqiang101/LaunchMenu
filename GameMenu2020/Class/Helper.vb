Imports System.Drawing.Drawing2D
Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.CompilerServices
Imports System.Xml

Public Module Helper

    Public CurrentCategory As GameList
    Public xmlPath As String = $"{My.Application.Info.DirectoryPath}\Apps"
    Public settingFile As String = $"{Application.StartupPath}\Data\Settings.xml"
    Public CurrentSettings As MenuSettings

    Public Function SafeImageFromFile(path As String) As Image
        If File.Exists(path) Then
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read)
                Dim img = Image.FromStream(fs)
                Return img
            End Using
        Else
            Using fs As New FileStream($"{Application.StartupPath}\Data\Images\NO_IMAGE.jpg", FileMode.Open, FileAccess.Read)
                Dim img = Image.FromStream(fs)
                Return img
            End Using
        End If
    End Function

    Public Enum RatingEnum
        RatingPending = 0
        EveryOne = 1
        EveryOne10 = 2
        EarlyChildhood = 3
        Teen = 4
        Mature = 5
        AdultsOnly = 6
        Pegi3 = 7
        Pegi7 = 8
        Pegi12 = 9
        Pegi16 = 10
        Pegi18 = 11
    End Enum

    <Extension>
    Public Sub Sort(TabControl As TabControl)
        Dim tablist = TabControl.TabPages.Cast(Of TabPage)().ToList()
        tablist.Sort(New TabPageComparer())
        TabControl.TabPages.Clear()
        Dim index As Integer = 0
        Do Until TabControl.TabCount = tablist.Count
            For Each tab As TabPage In tablist
                If tab.TabIndex = index Then
                    TabControl.TabPages.Add(tab)
                End If
            Next
            index += 1
        Loop
    End Sub

    <Extension>
    Public Function Base64ToImage(Image As String) As Image
        Dim b64 As String = Image.Replace(" ", "+")
        Dim bite() As Byte = Convert.FromBase64String(b64)
        Dim stream As New MemoryStream(bite)
        Return Drawing.Image.FromStream(stream)
    End Function

    <Extension>
    Public Function ImageToBase64(img As Image) As String
        Dim stream As New MemoryStream
        Dim bmp As Bitmap = New Bitmap(img)
        bmp.Save(stream, ImageFormat.Png)
        Return Convert.ToBase64String(stream.ToArray)
    End Function

    <Extension>
    Public Function ToRoundedImage(img As Image, bgColor As Color, Optional radius As Integer = 20) As Image
        radius *= 2

        Dim rImg As Bitmap = New Bitmap(img.Width, img.Height)
        Using g As Graphics = Graphics.FromImage(rImg)
            g.Clear(bgColor)
            g.SmoothingMode = SmoothingMode.AntiAlias
            Dim brush As New TextureBrush(img)
            Dim path As New GraphicsPath()
            path.AddArc(0, 0, radius, radius, 180, 90)
            path.AddArc(0 + rImg.Width - radius, 0, radius, radius, 270, 90)
            path.AddArc(0 + rImg.Width - radius, 0 + rImg.Height - radius, radius, radius, 0, 90)
            path.AddArc(0, 0 + rImg.Height - radius, radius, radius, 90, 90)
            g.FillPath(brush, path)
            Return rImg
        End Using
    End Function

    <Extension>
    Public Function ToSocialIcon(socialLogo As String) As SocialLabel.SocialLogo
        Select Case socialLogo
            Case "Facebook"
                Return SocialLabel.SocialLogo.Facebook
            Case "Google+"
                Return SocialLabel.SocialLogo.GooglePlus
            Case "Twitter"
                Return SocialLabel.SocialLogo.Twitter
            Case "Youtube"
                Return SocialLabel.SocialLogo.Youtube
            Case "VK"
                Return SocialLabel.SocialLogo.VK
            Case "Discord"
                Return SocialLabel.SocialLogo.Discord
            Case "LinkedIn"
                Return SocialLabel.SocialLogo.LinkedIn
            Case "Pinterest"
                Return SocialLabel.SocialLogo.Pinterest
            Case "Instagram"
                Return SocialLabel.SocialLogo.Instagram
            Case "Tumblr"
                Return SocialLabel.SocialLogo.Tumblr
            Case "Flickr"
                Return SocialLabel.SocialLogo.Flickr
            Case "Reddit"
                Return SocialLabel.SocialLogo.Reddit
            Case "Snapchat"
                Return SocialLabel.SocialLogo.Snapchat
            Case "WhatsApp"
                Return SocialLabel.SocialLogo.WhatsApp
            Case "Weibo"
                Return SocialLabel.SocialLogo.Weibo
            Case "Tencent Weibo"
                Return SocialLabel.SocialLogo.TencentWeibo
            Case "QQ"
                Return SocialLabel.SocialLogo.QQ
        End Select
        Return Nothing
    End Function

End Module