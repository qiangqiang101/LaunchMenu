Public Class AppItemS

    Private type As String = "Type:"

    Public Property ApplicationName As String
        Get
            Return siPanel.Title
        End Get
        Set(value As String)
            siPanel.Title = value
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
            Return siPanel.ColorBehind.A
        End Get
        Set(value As Integer)
            siPanel.ColorBehind = Color.FromArgb(value, siPanel.ColorBehind)
        End Set
    End Property
    Public Property Opacity2() As Integer
        Get
            Return siPanel.ColorDown.A
        End Get
        Set(value As Integer)
            siPanel.ColorDown = Color.FromArgb(value, siPanel.ColorDown)
        End Set
    End Property

    Public Sub New(n As String, p As String, si As String, w As String, pub As String, g As String, d As String, r As String, i As String, dev As String)
        InitializeComponent()
        Translate()
        siPanel.ColorBehind = Color.FromArgb(0, siPanel.ColorBehind)
        ApplicationName = n
        Path = p
        StartIn = si
        Website = w
        Publisher = pub
        Developer = dev
        Gerne = g
        Description = d
        Rating = r
        siPanel.Image = i.Base64ToImage

        If Path.Contains("launchmenu:") Then
            siPanel.ContextMenuStrip = New ContextMenuStrip
        End If
    End Sub

    Private Sub AppItemS_MouseEnter(sender As Object, e As EventArgs) Handles Me.MouseEnter, siPanel.MouseEnter
        If Not bwEnter.IsBusy Then bwEnter.RunWorkerAsync()
    End Sub

    Private Sub AppItemS_MouseLeave(sender As Object, e As EventArgs) Handles Me.MouseLeave, siPanel.MouseLeave
        If Not bwLeave.IsBusy Then bwLeave.RunWorkerAsync()
    End Sub

    Private Sub AppItemS_DoubleClick(sender As Object, e As EventArgs) Handles Me.Click, siPanel.Click
        Run(False)
    End Sub

    Private Sub Run(admin As Boolean)
        If Path.Contains("launchmenu:") Then
            Select Case Path
                Case "launchmenu:screenlock"
                    frmScreenLock.Show()
                    frmMenu.WindowState = FormWindowState.Minimized
                Case "launchmenu:mouse"
                    Process.Start("main.cpl")
                Case "launchmenu:ie"
                    Process.Start("iexplore")
                Case "launchmenu:edge"
                    Process.Start("microsoft-edge:")
                Case "launchmenu:calc"
                    Process.Start("calc")
                Case "launchmenu:notepad"
                    Process.Start("notepad")
                Case "launchmenu:joy"
                    Process.Start("joy.cpl")
                Case "launchmenu:magnify"
                    Process.Start("magnify")
                Case "launchmenu:mip"
                    Process.Start("C:\Program Files\Common Files\Microsoft Shared\Ink\mip.exe")
                Case "launchmenu:snippingtool"
                    Process.Start("C:\Windows\sysnative\SnippingTool.exe")
                Case "launchmenu:wordpad"
                    Process.Start("write")
            End Select
        Else
            Dim fl As New frmLaunch
            fl.Show()
            With fl
                .Text = ApplicationName
                .lblGameName.Text = ApplicationName
                .lblDeveloper.Text = Developer
                .lblGerne.Text = Gerne
                .lblDescription.Text = Description
                .pbImage.Image = siPanel.Image
                .Path = Path
                .StartIn = StartIn
                .Website = Website
                .RunAsAdmin = admin

                .Label3.Hide()
                .Label2.Hide()
                .lblPublisher.Hide()
                .pbRating.Hide()
                .Label4.Text = type
            End With
        End If
    End Sub

    Private Sub bwEnter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwEnter.DoWork
        CheckForIllegalCrossThreadCalls = False

        Dim i As Integer = 0
        Do Until i = 255
            i += 17
            If InvokeRequired Then
                Invoke(Sub()
                           Opacity = i
                           siPanel.Refresh()
                       End Sub)
            Else
                Opacity = i
                siPanel.Refresh()
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
                           siPanel.Refresh()
                       End Sub)
            Else
                Opacity = i
                siPanel.Refresh()
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
        siPanel.buttonText = ReadIniValue(iniFile, "ITEM", "Start")
        type = ReadIniValue(iniFile, "LAUNCHER", "Type")
    End Sub

End Class
