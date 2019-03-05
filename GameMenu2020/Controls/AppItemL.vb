Public Class AppItemL

    Private watch As String = "Watch"
    Private production As String = "Production:"
    Private distributor As String = "Distributor:"
    Private director As String = "Director:"

    Public Property ApplicationName As String
        Get
            Return liPanel.Title
        End Get
        Set(value As String)
            liPanel.Title = value
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
            Return liPanel.ColorBehind.A
        End Get
        Set(value As Integer)
            liPanel.ColorBehind = Color.FromArgb(value, liPanel.ColorBehind)
        End Set
    End Property
    Public Property Opacity2() As Integer
        Get
            Return liPanel.ColorDown.A
        End Get
        Set(value As Integer)
            liPanel.ColorDown = Color.FromArgb(value, liPanel.ColorDown)
        End Set
    End Property
    Public Property Type() As CategoryType

    Public Sub New(n As String, p As String, si As String, w As String, pub As String, g As String, d As String, r As String, i As String, dev As String, Optional t As CategoryType = CategoryType.Game)
        InitializeComponent()
        Translate()
        liPanel.ColorBehind = Color.FromArgb(0, liPanel.ColorBehind)
        ApplicationName = n
        Path = p
        StartIn = si
        Website = w
        Publisher = pub
        Developer = dev
        Gerne = g
        Description = d
        Rating = r
        liPanel.Image = i.Base64ToImage
        Type = t
        If t = CategoryType.Movie Then
            liPanel.buttonText = watch
            tmsiAdministrator.Enabled = False
        End If
    End Sub

    Private Sub AppItemL_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, liPanel.MouseEnter
        If Not bwEnter.IsBusy Then bwEnter.RunWorkerAsync()
    End Sub

    Private Sub AppItemL_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, liPanel.MouseLeave
        If Not bwLeave.IsBusy Then bwLeave.RunWorkerAsync()
    End Sub

    Private Sub AppItemL_Click(sender As Object, e As EventArgs) Handles Me.Click, liPanel.Click
        Run(False)
    End Sub

    Private Sub Run(admin As Boolean)
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
                .pbImage.Image = liPanel.Image

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
                .RunAsAdmin = admin
            ElseIf Type = CategoryType.Movie Then
                .Text = ApplicationName
                .lblGameName.Text = ApplicationName
                .Label3.Text = production
                .lblPublisher.Text = Publisher
                .Label7.Text = distributor
                .lblDeveloper.Text = Developer
                .lblGerne.Text = Gerne
                .lblDescription.Text = Description
                .Label2.Text = director
                .lblDirector.Text = Rating
                .lblDirector.Show()
                .Label6.Show()
                .lblStarring.Text = StartIn
                .lblStarring.Show()
                .pbRating.Hide()
                .pbImage.Image = liPanel.Image
                .Path = Path
                .Website = Website
                .RunAsAdmin = False
            End If
        End With
    End Sub

    Private Sub bwEnter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwEnter.DoWork
        CheckForIllegalCrossThreadCalls = False

        Dim i As Integer = 0
        Do Until i = 255
            i += 17
            If InvokeRequired Then
                Invoke(Sub()
                           Opacity = i
                           liPanel.Refresh()
                       End Sub)
            Else
                Opacity = i
                liPanel.Refresh()
            End If
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
                           liPanel.Refresh()
                       End Sub)
            Else
                Opacity = i
                liPanel.Refresh()
            End If
        Loop
    End Sub

    Private Sub tmsiOpen_Click(sender As Object, e As EventArgs) Handles tmsiOpen.Click
        Run(False)
    End Sub

    Private Sub tmsiOpenFileLocation_Click(sender As Object, e As EventArgs) Handles tmsiOpenFileLocation.Click
        Process.Start(IO.Path.GetDirectoryName(Path))
    End Sub

    Private Sub tmsiAdministrator_Click(sender As Object, e As EventArgs) Handles tmsiAdministrator.Click
        Run(True)
    End Sub

    Private Sub tmsiWebsite_Click(sender As Object, e As EventArgs) Handles tmsiWebsite.Click
        Process.Start(Website)
    End Sub

    Private Sub Translate()
        Dim iniFile As String = $"{Application.StartupPath}\Data\Language\{CurrentSettings.Language}.ini"

        '= ReadIniValue(iniFile, "ITEM", "")
        tmsiAdministrator.Text = ReadIniValue(iniFile, "ITEM", "Run_as_administrator")
        tmsiOpen.Text = ReadIniValue(iniFile, "ITEM", "Open")
        tmsiOpenFileLocation.Text = ReadIniValue(iniFile, "ITEM", "Open_file_location")
        tmsiWebsite.Text = ReadIniValue(iniFile, "ITEM", "Visit_Website")
        liPanel.buttonText = ReadIniValue(iniFile, "ITEM", "Play")
        watch = ReadIniValue(iniFile, "ITEM", "Watch")
        production = ReadIniValue(iniFile, "LAUNCHER", "Production")
        distributor = ReadIniValue(iniFile, "LAUNCHER", "Distributor")
        director = ReadIniValue(iniFile, "LAUNCHER", "Director")
    End Sub
End Class
