Public Class AppItemW

    Private watch As String = "Watch"
    Private start As String = "Start"

    Public Property ApplicationName As String
        Get
            Return wiPanel.Title
        End Get
        Set(value As String)
            wiPanel.Title = value
        End Set
    End Property
    Public Property Path As String
    Public Property StartIn As String
    Public Property Website As String
    Public Property Publisher As String
    Public Property Developer As String
    Public Property Gerne As String
    Public Property Description As String
    Public Property Rating As String
    Public Property Opacity() As Integer
        Get
            Return wiPanel.ColorBehind.A
        End Get
        Set(value As Integer)
            wiPanel.ColorBehind = Color.FromArgb(value, wiPanel.ColorBehind)
        End Set
    End Property
    Public Property Opacity2() As Integer
        Get
            Return wiPanel.ColorRight.A
        End Get
        Set(value As Integer)
            wiPanel.ColorRight = Color.FromArgb(value, wiPanel.ColorRight)
        End Set
    End Property
    Public Property Type() As CategoryType

    Public Sub New(n As String, p As String, si As String, w As String, pub As String, g As String, d As String, r As String, i As String, dev As String, Optional t As CategoryType = CategoryType.Game)
        InitializeComponent()
        Translate()
        wiPanel.ColorBehind = Color.FromArgb(0, wiPanel.ColorBehind)
        ApplicationName = n
        Path = p
        StartIn = si
        Website = w
        Publisher = pub
        Developer = dev
        Gerne = g
        Description = d
        Rating = r
        wiPanel.Image = i.Base64ToImage
        Type = t
        If t = CategoryType.Movie Then
            wiPanel.buttonText = watch
        ElseIf t = CategoryType.Application Then
            wiPanel.buttonText = start
        End If
    End Sub

    Private Sub AppItemL_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, wiPanel.MouseEnter
        If Not bwEnter.IsBusy Then bwEnter.RunWorkerAsync()
    End Sub

    Private Sub AppItemL_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, wiPanel.MouseLeave
        If Not bwLeave.IsBusy Then bwLeave.RunWorkerAsync()
    End Sub

    Private Sub AppItemL_Click(sender As Object, e As EventArgs) Handles Me.Click, wiPanel.Click
        Run()
    End Sub

    Private Sub Run()
        Dim fl As New frmLaunch
        fl.Show()
        With fl
            If Type = CategoryType.Game Then
                .Text = ApplicationName
                .lblGameName.Text = ApplicationName
                .lblPublisher.Text = Publisher
                .lblDeveloper.Text = Developer
                .lblGerne.Text = Gerne
                .lblDescription.Text = Description
                .pbImage.Image = wiPanel.Image

                Select Case Rating
                    Case "Rating Pending"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Image\Rating\ESRB_Rating_Pending.png")
                    Case "Everyone"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\ESRB_Everyone.png")
                    Case "Everyone 10 +"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\ESRB_Everyone_10+.png")
                    Case "Early Childhood"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\ESRB_Early_Childhood.png")
                    Case "Teen"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\ESRB_Teen.png")
                    Case "Mature"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\ESRB_Mature.png")
                    Case "Adults Only"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\ESRB_Adults_Only.png")
                    Case "PEGI 3"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\PEGI_3.png")
                    Case "PEGI 7"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\PEGI_7.png")
                    Case "PEGI 12"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\PEGI_12.png")
                    Case "PEGI 16"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\PEGI_16.png")
                    Case "PEGI 18"
                        .pbRating.Image = SafeImageFromFile($"{Application.StartupPath}\Data\Images\Rating\PEGI_18.png")
                End Select
                .Path = Path
                .StartIn = StartIn
                .Website = Website
            ElseIf Type = CategoryType.Movie Then
                .Text = ApplicationName
                .lblGameName.Text = ApplicationName
                .Label3.Text = "Production:"
                .lblPublisher.Text = Publisher
                .Label7.Text = "Distributor:"
                .lblDeveloper.Text = Developer
                .lblGerne.Text = Gerne
                .lblDescription.Text = Description
                .Label2.Text = "Director:"
                .lblDirector.Text = Rating
                .lblDirector.Show()
                .Label6.Show()
                .lblStarring.Text = StartIn
                .lblStarring.Show()
                .pbRating.Hide()
                .pbImage.Image = wiPanel.Image
                .Path = Path
                .Website = Website
            ElseIf Type = CategoryType.Application Then
                .Text = ApplicationName
                .lblGameName.Text = ApplicationName
                .lblDeveloper.Text = Developer
                .lblGerne.Text = Gerne
                .lblDescription.Text = Description
                .pbImage.Image = wiPanel.Image
                .Path = Path
                .StartIn = StartIn
                .Website = Website

                .Label3.Hide()
                .Label2.Hide()
                .lblPublisher.Hide()
                .pbRating.Hide()
                .Label4.Text = "Type:"
            End If
        End With
    End Sub

    Private Sub bwEnter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwEnter.DoWork
        CheckForIllegalCrossThreadCalls = False

        Dim i As Integer = 0
        Do Until i = 255
            If InvokeRequired Then
                Invoke(Sub()
                           Opacity = i
                           wiPanel.Refresh()
                       End Sub)
            Else
                Opacity = i
                wiPanel.Refresh()
            End If
            i += 17
        Loop
    End Sub

    Private Sub bwLeave_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwLeave.DoWork
        CheckForIllegalCrossThreadCalls = False

        Dim i As Integer = 255
        Do Until i = 0
            i -= 17
            If InvokeRequired Then
                Invoke(Sub()
                           Opacity = i
                           wiPanel.Refresh()
                       End Sub)
            Else
                Opacity = i
                wiPanel.Refresh()
            End If
        Loop
    End Sub

    Private Sub Translate()
        Dim iniFile As String = $"{Application.StartupPath}\Data\Language\{CurrentSettings.Language}.ini"

        '= ReadIniValue(iniFile, "ITEM", "")
        wiPanel.buttonText = ReadIniValue(iniFile, "ITEM", "Play")
        watch = ReadIniValue(iniFile, "ITEM", "Watch")
        start = ReadIniValue(iniFile, "ITEM", "Start")
    End Sub

End Class
