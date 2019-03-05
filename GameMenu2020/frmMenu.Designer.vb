<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmMenu
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmMenu))
        Me.pJustForVisual = New System.Windows.Forms.Panel()
        Me.flpSocial = New System.Windows.Forms.FlowLayoutPanel()
        Me.pDock = New System.Windows.Forms.Panel()
        Me.tcGames = New LaunchMenu.XylosTabControl()
        Me.tpWelcome = New System.Windows.Forms.TabPage()
        Me.flpQuickLaunch = New System.Windows.Forms.FlowLayoutPanel()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.flpNewsLeft = New System.Windows.Forms.FlowLayoutPanel()
        Me.SliderLItem1 = New LaunchMenu.SliderLItem()
        Me.SliderSItem4 = New LaunchMenu.SliderSItem()
        Me.SliderSItem3 = New LaunchMenu.SliderSItem()
        Me.SliderSItem2 = New LaunchMenu.SliderSItem()
        Me.SliderSItem1 = New LaunchMenu.SliderSItem()
        Me.tpRekon = New System.Windows.Forms.TabPage()
        Me.tpNewGame = New System.Windows.Forms.TabPage()
        Me.flpNewGames = New LaunchMenu.SmoothAutoScrollFlowLayoutPanel()
        Me.tpNewMovie = New System.Windows.Forms.TabPage()
        Me.flpNewMovies = New LaunchMenu.SmoothAutoScrollFlowLayoutPanel()
        Me.iconList = New System.Windows.Forms.ImageList(Me.components)
        Me.flpSearchResult = New LaunchMenu.SmoothAutoScrollFlowLayoutPanel()
        Me.txtSearch = New LaunchMenu.MDTextBox()
        Me.tLoading = New System.Windows.Forms.Timer(Me.components)
        Me.tAnimation = New System.Windows.Forms.Timer(Me.components)
        Me.Skin1 = New LaunchMenu.Skin()
        Me.pJustForVisual.SuspendLayout()
        Me.pDock.SuspendLayout()
        Me.tcGames.SuspendLayout()
        Me.tpWelcome.SuspendLayout()
        Me.tpNewGame.SuspendLayout()
        Me.tpNewMovie.SuspendLayout()
        Me.SuspendLayout()
        '
        'pJustForVisual
        '
        Me.pJustForVisual.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pJustForVisual.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.pJustForVisual.Controls.Add(Me.flpSocial)
        Me.pJustForVisual.Controls.Add(Me.pDock)
        Me.pJustForVisual.Controls.Add(Me.txtSearch)
        Me.pJustForVisual.Location = New System.Drawing.Point(12, 37)
        Me.pJustForVisual.Name = "pJustForVisual"
        Me.pJustForVisual.Padding = New System.Windows.Forms.Padding(3)
        Me.pJustForVisual.Size = New System.Drawing.Size(1485, 766)
        Me.pJustForVisual.TabIndex = 30
        '
        'flpSocial
        '
        Me.flpSocial.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.flpSocial.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft
        Me.flpSocial.Location = New System.Drawing.Point(184, 6)
        Me.flpSocial.Name = "flpSocial"
        Me.flpSocial.Size = New System.Drawing.Size(1139, 29)
        Me.flpSocial.TabIndex = 32
        '
        'pDock
        '
        Me.pDock.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.pDock.Controls.Add(Me.tcGames)
        Me.pDock.Controls.Add(Me.flpSearchResult)
        Me.pDock.Location = New System.Drawing.Point(0, 41)
        Me.pDock.Name = "pDock"
        Me.pDock.Size = New System.Drawing.Size(1485, 725)
        Me.pDock.TabIndex = 31
        '
        'tcGames
        '
        Me.tcGames.Alignment = System.Windows.Forms.TabAlignment.Left
        Me.tcGames.Controls.Add(Me.tpWelcome)
        Me.tcGames.Controls.Add(Me.tpRekon)
        Me.tcGames.Controls.Add(Me.tpNewGame)
        Me.tcGames.Controls.Add(Me.tpNewMovie)
        Me.tcGames.Dock = System.Windows.Forms.DockStyle.Fill
        Me.tcGames.FirstHeaderBorder = True
        Me.tcGames.ImageList = Me.iconList
        Me.tcGames.ItemSize = New System.Drawing.Size(40, 180)
        Me.tcGames.Location = New System.Drawing.Point(0, 0)
        Me.tcGames.Multiline = True
        Me.tcGames.Name = "tcGames"
        Me.tcGames.SelectedIndex = 0
        Me.tcGames.Size = New System.Drawing.Size(1235, 725)
        Me.tcGames.SizeMode = System.Windows.Forms.TabSizeMode.Fixed
        Me.tcGames.TabIndex = 30
        '
        'tpWelcome
        '
        Me.tpWelcome.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.tpWelcome.Controls.Add(Me.flpQuickLaunch)
        Me.tpWelcome.Controls.Add(Me.Label2)
        Me.tpWelcome.Controls.Add(Me.Label1)
        Me.tpWelcome.Controls.Add(Me.flpNewsLeft)
        Me.tpWelcome.Controls.Add(Me.SliderLItem1)
        Me.tpWelcome.Controls.Add(Me.SliderSItem4)
        Me.tpWelcome.Controls.Add(Me.SliderSItem3)
        Me.tpWelcome.Controls.Add(Me.SliderSItem2)
        Me.tpWelcome.Controls.Add(Me.SliderSItem1)
        Me.tpWelcome.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tpWelcome.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.tpWelcome.ImageIndex = 266
        Me.tpWelcome.Location = New System.Drawing.Point(184, 4)
        Me.tpWelcome.Name = "tpWelcome"
        Me.tpWelcome.Padding = New System.Windows.Forms.Padding(3)
        Me.tpWelcome.Size = New System.Drawing.Size(1047, 717)
        Me.tpWelcome.TabIndex = 3
        Me.tpWelcome.Text = "Activity"
        '
        'flpQuickLaunch
        '
        Me.flpQuickLaunch.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.flpQuickLaunch.BackColor = System.Drawing.Color.Transparent
        Me.flpQuickLaunch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.flpQuickLaunch.Location = New System.Drawing.Point(11, 551)
        Me.flpQuickLaunch.Name = "flpQuickLaunch"
        Me.flpQuickLaunch.Size = New System.Drawing.Size(1024, 160)
        Me.flpQuickLaunch.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.Label2.AutoSize = True
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(11, 520)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(145, 28)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Quick Launcher"
        '
        'Label1
        '
        Me.Label1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 15.0!)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(11, 387)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(111, 28)
        Me.Label1.TabIndex = 8
        Me.Label1.Text = "More News"
        '
        'flpNewsLeft
        '
        Me.flpNewsLeft.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom), System.Windows.Forms.AnchorStyles)
        Me.flpNewsLeft.AutoScroll = True
        Me.flpNewsLeft.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.flpNewsLeft.Location = New System.Drawing.Point(11, 418)
        Me.flpNewsLeft.Name = "flpNewsLeft"
        Me.flpNewsLeft.Size = New System.Drawing.Size(1024, 99)
        Me.flpNewsLeft.TabIndex = 6
        '
        'SliderLItem1
        '
        Me.SliderLItem1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SliderLItem1.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.SliderLItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SliderLItem1.Caption = "Title"
        Me.SliderLItem1.FeedDate = Nothing
        Me.SliderLItem1.FeedSubtitle = ""
        Me.SliderLItem1.FeedURL = Nothing
        Me.SliderLItem1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SliderLItem1.Location = New System.Drawing.Point(11, 6)
        Me.SliderLItem1.Name = "SliderLItem1"
        Me.SliderLItem1.PublishDate = "today"
        Me.SliderLItem1.Size = New System.Drawing.Size(722, 378)
        Me.SliderLItem1.SubCaption = "Subtitle"
        Me.SliderLItem1.TabIndex = 5
        '
        'SliderSItem4
        '
        Me.SliderSItem4.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SliderSItem4.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.SliderSItem4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SliderSItem4.Caption = ""
        Me.SliderSItem4.FeedDate = Nothing
        Me.SliderSItem4.FeedSubtitle = Nothing
        Me.SliderSItem4.FeedURL = Nothing
        Me.SliderSItem4.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SliderSItem4.Location = New System.Drawing.Point(739, 294)
        Me.SliderSItem4.Name = "SliderSItem4"
        Me.SliderSItem4.Opacity = 200
        Me.SliderSItem4.Selected = False
        Me.SliderSItem4.Size = New System.Drawing.Size(296, 90)
        Me.SliderSItem4.TabIndex = 4
        '
        'SliderSItem3
        '
        Me.SliderSItem3.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SliderSItem3.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.SliderSItem3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SliderSItem3.Caption = ""
        Me.SliderSItem3.FeedDate = Nothing
        Me.SliderSItem3.FeedSubtitle = Nothing
        Me.SliderSItem3.FeedURL = Nothing
        Me.SliderSItem3.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SliderSItem3.Location = New System.Drawing.Point(739, 198)
        Me.SliderSItem3.Name = "SliderSItem3"
        Me.SliderSItem3.Opacity = 200
        Me.SliderSItem3.Selected = False
        Me.SliderSItem3.Size = New System.Drawing.Size(296, 90)
        Me.SliderSItem3.TabIndex = 3
        '
        'SliderSItem2
        '
        Me.SliderSItem2.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SliderSItem2.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.SliderSItem2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SliderSItem2.Caption = ""
        Me.SliderSItem2.FeedDate = Nothing
        Me.SliderSItem2.FeedSubtitle = Nothing
        Me.SliderSItem2.FeedURL = Nothing
        Me.SliderSItem2.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SliderSItem2.Location = New System.Drawing.Point(739, 102)
        Me.SliderSItem2.Name = "SliderSItem2"
        Me.SliderSItem2.Opacity = 200
        Me.SliderSItem2.Selected = False
        Me.SliderSItem2.Size = New System.Drawing.Size(296, 90)
        Me.SliderSItem2.TabIndex = 2
        '
        'SliderSItem1
        '
        Me.SliderSItem1.Anchor = System.Windows.Forms.AnchorStyles.Top
        Me.SliderSItem1.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.SliderSItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.SliderSItem1.Caption = ""
        Me.SliderSItem1.FeedDate = Nothing
        Me.SliderSItem1.FeedSubtitle = Nothing
        Me.SliderSItem1.FeedURL = Nothing
        Me.SliderSItem1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SliderSItem1.Location = New System.Drawing.Point(739, 6)
        Me.SliderSItem1.Name = "SliderSItem1"
        Me.SliderSItem1.Opacity = 0
        Me.SliderSItem1.Selected = True
        Me.SliderSItem1.Size = New System.Drawing.Size(296, 90)
        Me.SliderSItem1.TabIndex = 1
        '
        'tpRekon
        '
        Me.tpRekon.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.tpRekon.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tpRekon.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.tpRekon.Location = New System.Drawing.Point(184, 4)
        Me.tpRekon.Name = "tpRekon"
        Me.tpRekon.Size = New System.Drawing.Size(1047, 717)
        Me.tpRekon.TabIndex = 2
        Me.tpRekon.Tag = "a"
        Me.tpRekon.Text = "Recommended"
        '
        'tpNewGame
        '
        Me.tpNewGame.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.tpNewGame.Controls.Add(Me.flpNewGames)
        Me.tpNewGame.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tpNewGame.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.tpNewGame.ImageIndex = 135
        Me.tpNewGame.Location = New System.Drawing.Point(184, 4)
        Me.tpNewGame.Name = "tpNewGame"
        Me.tpNewGame.Padding = New System.Windows.Forms.Padding(3)
        Me.tpNewGame.Size = New System.Drawing.Size(1047, 717)
        Me.tpNewGame.TabIndex = 0
        Me.tpNewGame.Text = "New Games"
        '
        'flpNewGames
        '
        Me.flpNewGames.AutoScroll = True
        Me.flpNewGames.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpNewGames.Location = New System.Drawing.Point(3, 3)
        Me.flpNewGames.Name = "flpNewGames"
        Me.flpNewGames.ParentForm = Me
        Me.flpNewGames.Size = New System.Drawing.Size(1041, 711)
        Me.flpNewGames.TabIndex = 1
        '
        'tpNewMovie
        '
        Me.tpNewMovie.BackColor = System.Drawing.Color.FromArgb(CType(CType(47, Byte), Integer), CType(CType(49, Byte), Integer), CType(CType(54, Byte), Integer))
        Me.tpNewMovie.Controls.Add(Me.flpNewMovies)
        Me.tpNewMovie.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.tpNewMovie.ForeColor = System.Drawing.Color.FromArgb(CType(CType(124, Byte), Integer), CType(CType(133, Byte), Integer), CType(CType(142, Byte), Integer))
        Me.tpNewMovie.ImageIndex = 121
        Me.tpNewMovie.Location = New System.Drawing.Point(184, 4)
        Me.tpNewMovie.Name = "tpNewMovie"
        Me.tpNewMovie.Padding = New System.Windows.Forms.Padding(3)
        Me.tpNewMovie.Size = New System.Drawing.Size(1047, 717)
        Me.tpNewMovie.TabIndex = 1
        Me.tpNewMovie.Text = "New Movies"
        '
        'flpNewMovies
        '
        Me.flpNewMovies.AutoScroll = True
        Me.flpNewMovies.Dock = System.Windows.Forms.DockStyle.Fill
        Me.flpNewMovies.Location = New System.Drawing.Point(3, 3)
        Me.flpNewMovies.Name = "flpNewMovies"
        Me.flpNewMovies.ParentForm = Me
        Me.flpNewMovies.Size = New System.Drawing.Size(1041, 711)
        Me.flpNewMovies.TabIndex = 0
        '
        'iconList
        '
        Me.iconList.ImageStream = CType(resources.GetObject("iconList.ImageStream"), System.Windows.Forms.ImageListStreamer)
        Me.iconList.TransparentColor = System.Drawing.Color.Transparent
        Me.iconList.Images.SetKeyName(0, "3d-glasses-filled.png")
        Me.iconList.Images.SetKeyName(1, "3ds-max-filled.png")
        Me.iconList.Images.SetKeyName(2, "007-filled.png")
        Me.iconList.Images.SetKeyName(3, "adam-sandler-filled.png")
        Me.iconList.Images.SetKeyName(4, "adobe-after-effects-filled.png")
        Me.iconList.Images.SetKeyName(5, "adobe-bridge-filled.png")
        Me.iconList.Images.SetKeyName(6, "adobe-dreamweaver-filled.png")
        Me.iconList.Images.SetKeyName(7, "adobe-fireworks-filled.png")
        Me.iconList.Images.SetKeyName(8, "adobe-flash-professional-filled.png")
        Me.iconList.Images.SetKeyName(9, "adobe-framemaker-filled.png")
        Me.iconList.Images.SetKeyName(10, "adobe-illustrator-filled.png")
        Me.iconList.Images.SetKeyName(11, "adobe-indesign-filled.png")
        Me.iconList.Images.SetKeyName(12, "adobe-lightroom-filled.png")
        Me.iconList.Images.SetKeyName(13, "adobe-photoshop-filled.png")
        Me.iconList.Images.SetKeyName(14, "amd-filled.png")
        Me.iconList.Images.SetKeyName(15, "american-football-player-filled.png")
        Me.iconList.Images.SetKeyName(16, "animation-filled.png")
        Me.iconList.Images.SetKeyName(17, "apple-logo-filled.png")
        Me.iconList.Images.SetKeyName(18, "arryn-house-filled.png")
        Me.iconList.Images.SetKeyName(19, "astronaut-filled.png")
        Me.iconList.Images.SetKeyName(20, "audacity-filled.png")
        Me.iconList.Images.SetKeyName(21, "audience-filled.png")
        Me.iconList.Images.SetKeyName(22, "autocad-filled.png")
        Me.iconList.Images.SetKeyName(23, "bart-simpson-filled.png")
        Me.iconList.Images.SetKeyName(24, "baseball-filled.png")
        Me.iconList.Images.SetKeyName(25, "batman-filled.png")
        Me.iconList.Images.SetKeyName(26, "battle.net-filled.png")
        Me.iconList.Images.SetKeyName(27, "bbc-filled.png")
        Me.iconList.Images.SetKeyName(28, "behance-filled.png")
        Me.iconList.Images.SetKeyName(29, "bing-filled.png")
        Me.iconList.Images.SetKeyName(30, "black-blood-filled.png")
        Me.iconList.Images.SetKeyName(31, "black-twitter-logo-filled.png")
        Me.iconList.Images.SetKeyName(32, "black-twitter-logo-filled-2.png")
        Me.iconList.Images.SetKeyName(33, "blu-ray-filled.png")
        Me.iconList.Images.SetKeyName(34, "body-armor-filled.png")
        Me.iconList.Images.SetKeyName(35, "bowling-filled.png")
        Me.iconList.Images.SetKeyName(36, "bowling-pins-filled.png")
        Me.iconList.Images.SetKeyName(37, "bowling-spare-filled.png")
        Me.iconList.Images.SetKeyName(38, "bronze-medal-filled.png")
        Me.iconList.Images.SetKeyName(39, "brutus-filled.png")
        Me.iconList.Images.SetKeyName(40, "captain-america-filled.png")
        Me.iconList.Images.SetKeyName(41, "cd-filled.png")
        Me.iconList.Images.SetKeyName(42, "chrome-filled.png")
        Me.iconList.Images.SetKeyName(43, "comedy-2-filled.png")
        Me.iconList.Images.SetKeyName(44, "comedy-drama-mask-filled.png")
        Me.iconList.Images.SetKeyName(45, "command-line-filled.png")
        Me.iconList.Images.SetKeyName(46, "communication-mobile-application-filled.png")
        Me.iconList.Images.SetKeyName(47, "compact-disc-filled.png")
        Me.iconList.Images.SetKeyName(48, "cricket-filled.png")
        Me.iconList.Images.SetKeyName(49, "day-of-the-tentacle-filled.png")
        Me.iconList.Images.SetKeyName(50, "dead-skull-filled.png")
        Me.iconList.Images.SetKeyName(51, "delicious-filled.png")
        Me.iconList.Images.SetKeyName(52, "dell-logo-filled.png")
        Me.iconList.Images.SetKeyName(53, "deviantart-filled.png")
        Me.iconList.Images.SetKeyName(54, "dice-filled.png")
        Me.iconList.Images.SetKeyName(55, "digg-filled.png")
        Me.iconList.Images.SetKeyName(56, "digital-versatile-disc-filled.png")
        Me.iconList.Images.SetKeyName(57, "discord-filled.png")
        Me.iconList.Images.SetKeyName(58, "disney-movies-filled.png")
        Me.iconList.Images.SetKeyName(59, "documentary-film-filled.png")
        Me.iconList.Images.SetKeyName(60, "dolphin-emulator-filled.png")
        Me.iconList.Images.SetKeyName(61, "doraemon-filled.png")
        Me.iconList.Images.SetKeyName(62, "dota-2-filled.png")
        Me.iconList.Images.SetKeyName(63, "dropbox-filled.png")
        Me.iconList.Images.SetKeyName(64, "dts-filled.png")
        Me.iconList.Images.SetKeyName(65, "duckduckgo-filled.png")
        Me.iconList.Images.SetKeyName(66, "ebay-logo-filled.png")
        Me.iconList.Images.SetKeyName(67, "emu4ios-filled.png")
        Me.iconList.Images.SetKeyName(68, "equestrian-statue-filled.png")
        Me.iconList.Images.SetKeyName(69, "evernote-filled.png")
        Me.iconList.Images.SetKeyName(70, "facebook-logo-filled.png")
        Me.iconList.Images.SetKeyName(71, "facebook-messenger-filled.png")
        Me.iconList.Images.SetKeyName(72, "fidget-spinner-filled.png")
        Me.iconList.Images.SetKeyName(73, "fight-filled.png")
        Me.iconList.Images.SetKeyName(74, "film-noir-filled.png")
        Me.iconList.Images.SetKeyName(75, "firefox-filled.png")
        Me.iconList.Images.SetKeyName(76, "first-logo-of-instagram-filled.png")
        Me.iconList.Images.SetKeyName(77, "food-as-resources-filled.png")
        Me.iconList.Images.SetKeyName(78, "frankensteins-monster-filled.png")
        Me.iconList.Images.SetKeyName(79, "garena-filled.png")
        Me.iconList.Images.SetKeyName(80, "git-filled.png")
        Me.iconList.Images.SetKeyName(81, "github-filled.png")
        Me.iconList.Images.SetKeyName(82, "gitlab-filled.png")
        Me.iconList.Images.SetKeyName(83, "gmail-filled.png")
        Me.iconList.Images.SetKeyName(84, "google-cloud-platform-filled.png")
        Me.iconList.Images.SetKeyName(85, "google-docs-filled.png")
        Me.iconList.Images.SetKeyName(86, "google-drive-filled.png")
        Me.iconList.Images.SetKeyName(87, "google-earth-filled.png")
        Me.iconList.Images.SetKeyName(88, "google-filled.png")
        Me.iconList.Images.SetKeyName(89, "google-forms-filled.png")
        Me.iconList.Images.SetKeyName(90, "google-forms-new-logo-filled.png")
        Me.iconList.Images.SetKeyName(91, "google-groups-filled.png")
        Me.iconList.Images.SetKeyName(92, "google-images-filled.png")
        Me.iconList.Images.SetKeyName(93, "google-keep-filled.png")
        Me.iconList.Images.SetKeyName(94, "google-maps-filled.png")
        Me.iconList.Images.SetKeyName(95, "google-news-filled.png")
        Me.iconList.Images.SetKeyName(96, "google-photos-filled.png")
        Me.iconList.Images.SetKeyName(97, "google-play-music-filled.png")
        Me.iconList.Images.SetKeyName(98, "google-plus-filled.png")
        Me.iconList.Images.SetKeyName(99, "google-sheets-filled.png")
        Me.iconList.Images.SetKeyName(100, "google-sites-filled.png")
        Me.iconList.Images.SetKeyName(101, "google-slides-filled.png")
        Me.iconList.Images.SetKeyName(102, "google-wallet-filled.png")
        Me.iconList.Images.SetKeyName(103, "greyjoy-house-filled.png")
        Me.iconList.Images.SetKeyName(104, "hammerstein.png")
        Me.iconList.Images.SetKeyName(105, "hawkeye-filled.png")
        Me.iconList.Images.SetKeyName(106, "hawkeye-symbol-filled.png")
        Me.iconList.Images.SetKeyName(107, "hbo-filled.png")
        Me.iconList.Images.SetKeyName(108, "hbo-go-filled.png")
        Me.iconList.Images.SetKeyName(109, "hd-720p-filled.png")
        Me.iconList.Images.SetKeyName(110, "hd-1080p-filled.png")
        Me.iconList.Images.SetKeyName(111, "hdtv-filled.png")
        Me.iconList.Images.SetKeyName(112, "hedgehog-filled.png")
        Me.iconList.Images.SetKeyName(113, "high-resolution-filled.png")
        Me.iconList.Images.SetKeyName(114, "homer-simpson-filled.png")
        Me.iconList.Images.SetKeyName(115, "house-stark-filled.png")
        Me.iconList.Images.SetKeyName(116, "hulk-filled.png")
        Me.iconList.Images.SetKeyName(117, "hulu-filled.png")
        Me.iconList.Images.SetKeyName(118, "icq-filled.png")
        Me.iconList.Images.SetKeyName(119, "imdb-filled.png")
        Me.iconList.Images.SetKeyName(120, "imovie-filled.png")
        Me.iconList.Images.SetKeyName(121, "indiana-jones-filled.png")
        Me.iconList.Images.SetKeyName(122, "instagram-logo-filled.png")
        Me.iconList.Images.SetKeyName(123, "internet-explorer-browser-filled.png")
        Me.iconList.Images.SetKeyName(124, "iron-man-filled.png")
        Me.iconList.Images.SetKeyName(125, "jason-voorhees-filled.png")
        Me.iconList.Images.SetKeyName(126, "javelin-throw-filled.png")
        Me.iconList.Images.SetKeyName(127, "jetpack-joyride-filled.png")
        Me.iconList.Images.SetKeyName(128, "joker-filled.png")
        Me.iconList.Images.SetKeyName(129, "joker-suicide-squad-filled.png")
        Me.iconList.Images.SetKeyName(130, "kicking-filled.png")
        Me.iconList.Images.SetKeyName(131, "kik-messenger-logo-filled.png")
        Me.iconList.Images.SetKeyName(132, "kylie-jenner-filled.png")
        Me.iconList.Images.SetKeyName(133, "lacrosse-stick-filled.png")
        Me.iconList.Images.SetKeyName(134, "launchpad-filled.png")
        Me.iconList.Images.SetKeyName(135, "league-of-legends-filled.png")
        Me.iconList.Images.SetKeyName(136, "lego-filled.png")
        Me.iconList.Images.SetKeyName(137, "linkedin-filled.png")
        Me.iconList.Images.SetKeyName(138, "linux-filled.png")
        Me.iconList.Images.SetKeyName(139, "lisa-simpson-filled.png")
        Me.iconList.Images.SetKeyName(140, "loving-hearts-filled.png")
        Me.iconList.Images.SetKeyName(141, "maggie-simpson-filled.png")
        Me.iconList.Images.SetKeyName(142, "magical-scroll-filled.png")
        Me.iconList.Images.SetKeyName(143, "magic-lamp-filled.png")
        Me.iconList.Images.SetKeyName(144, "marge-simpson-filled.png")
        Me.iconList.Images.SetKeyName(145, "martell-house-filled.png")
        Me.iconList.Images.SetKeyName(146, "maxthon-filled.png")
        Me.iconList.Images.SetKeyName(147, "microsoft-access-filled.png")
        Me.iconList.Images.SetKeyName(148, "microsoft-edge-filled.png")
        Me.iconList.Images.SetKeyName(149, "microsoft-excel-file-filled.png")
        Me.iconList.Images.SetKeyName(150, "microsoft-exchange-filled.png")
        Me.iconList.Images.SetKeyName(151, "microsoft-groove-filled.png")
        Me.iconList.Images.SetKeyName(152, "microsoft-office-project-filled.png")
        Me.iconList.Images.SetKeyName(153, "microsoft-onenote-filled.png")
        Me.iconList.Images.SetKeyName(154, "microsoft-outlook-filled.png")
        Me.iconList.Images.SetKeyName(155, "microsoft-paint-filled.png")
        Me.iconList.Images.SetKeyName(156, "microsoft-powerpoint-filled.png")
        Me.iconList.Images.SetKeyName(157, "microsoft-publisher-filled.png")
        Me.iconList.Images.SetKeyName(158, "microsoft-sharepoint-filled.png")
        Me.iconList.Images.SetKeyName(159, "microsoft-visio-filled.png")
        Me.iconList.Images.SetKeyName(160, "microsoft-windows-filled.png")
        Me.iconList.Images.SetKeyName(161, "microsoft-word-filled.png")
        Me.iconList.Images.SetKeyName(162, "miles-morales.png")
        Me.iconList.Images.SetKeyName(163, "minecraft-logo-filled.png")
        Me.iconList.Images.SetKeyName(164, "minecraft-main-character-filled.png")
        Me.iconList.Images.SetKeyName(165, "minion-2-filled.png")
        Me.iconList.Images.SetKeyName(166, "minion-filled.png")
        Me.iconList.Images.SetKeyName(167, "movie-clapper-tool-filled.png")
        Me.iconList.Images.SetKeyName(168, "movie-filled.png")
        Me.iconList.Images.SetKeyName(169, "movie-film-strip-filled.png")
        Me.iconList.Images.SetKeyName(170, "movie-popcorn-filled.png")
        Me.iconList.Images.SetKeyName(171, "movie-popcorn-filled-2.png")
        Me.iconList.Images.SetKeyName(172, "movie-projector-filled.png")
        Me.iconList.Images.SetKeyName(173, "movie-roll-filled.png")
        Me.iconList.Images.SetKeyName(174, "movies-folder-filled.png")
        Me.iconList.Images.SetKeyName(175, "movie-ticket-filled.png")
        Me.iconList.Images.SetKeyName(176, "muscle-building-filled.png")
        Me.iconList.Images.SetKeyName(177, "music-note-outline-filled.png")
        Me.iconList.Images.SetKeyName(178, "myspace-app-filled.png")
        Me.iconList.Images.SetKeyName(179, "myspace-filled.png")
        Me.iconList.Images.SetKeyName(180, "naruto-filled.png")
        Me.iconList.Images.SetKeyName(181, "need-for-speed-filled.png")
        Me.iconList.Images.SetKeyName(182, "nintendo-filled.png")
        Me.iconList.Images.SetKeyName(183, "nintendo-wii-u-filled.png")
        Me.iconList.Images.SetKeyName(184, "nintendo-with-card-filled.png")
        Me.iconList.Images.SetKeyName(185, "obs-studio-filled.png")
        Me.iconList.Images.SetKeyName(186, "old-google-logo-filled.png")
        Me.iconList.Images.SetKeyName(187, "onedrive-logo-filled.png")
        Me.iconList.Images.SetKeyName(188, "opera-browser-filled.png")
        Me.iconList.Images.SetKeyName(189, "origin-filled.png")
        Me.iconList.Images.SetKeyName(190, "overwatch-league-filled.png")
        Me.iconList.Images.SetKeyName(191, "paypal-logo-filled.png")
        Me.iconList.Images.SetKeyName(192, "pcloud-filled.png")
        Me.iconList.Images.SetKeyName(193, "pennywise-filled.png")
        Me.iconList.Images.SetKeyName(194, "person-kicking-ball-filled.png")
        Me.iconList.Images.SetKeyName(195, "pinwheel-filled.png")
        Me.iconList.Images.SetKeyName(196, "pirates-of-the-caribbean-filled.png")
        Me.iconList.Images.SetKeyName(197, "pixar-lamp-2-filled.png")
        Me.iconList.Images.SetKeyName(198, "pokemon-filled.png")
        Me.iconList.Images.SetKeyName(199, "pole-vault-filled.png")
        Me.iconList.Images.SetKeyName(200, "popcorn-maker-filled.png")
        Me.iconList.Images.SetKeyName(201, "qq-filled.png")
        Me.iconList.Images.SetKeyName(202, "queen-of-clubs-filled.png")
        Me.iconList.Images.SetKeyName(203, "racket-filled.png")
        Me.iconList.Images.SetKeyName(204, "reddit-filled.png")
        Me.iconList.Images.SetKeyName(205, "renren-filled.png")
        Me.iconList.Images.SetKeyName(206, "retro-tv-filled.png")
        Me.iconList.Images.SetKeyName(207, "rick-sanchez-filled.png")
        Me.iconList.Images.SetKeyName(208, "rockstar-games-filled.png")
        Me.iconList.Images.SetKeyName(209, "rotten-tomatoes-filled.png")
        Me.iconList.Images.SetKeyName(210, "rubik's-cube-filled.png")
        Me.iconList.Images.SetKeyName(211, "ruby-programming-language-filled.png")
        Me.iconList.Images.SetKeyName(212, "safari-filled.png")
        Me.iconList.Images.SetKeyName(213, "safari-web-browser-filled.png")
        Me.iconList.Images.SetKeyName(214, "scoreboard-filled.png")
        Me.iconList.Images.SetKeyName(215, "shortcuts-filled.png")
        Me.iconList.Images.SetKeyName(216, "shot-put-filled.png")
        Me.iconList.Images.SetKeyName(217, "showtime-filled.png")
        Me.iconList.Images.SetKeyName(218, "shuttercock-filled.png")
        Me.iconList.Images.SetKeyName(219, "silver-medal-filled.png")
        Me.iconList.Images.SetKeyName(220, "skittle-filled.png")
        Me.iconList.Images.SetKeyName(221, "skype-logo-filled.png")
        Me.iconList.Images.SetKeyName(222, "snapchat-filled.png")
        Me.iconList.Images.SetKeyName(223, "soccer-yellow-card-filled.png")
        Me.iconList.Images.SetKeyName(224, "softether-vpn-filled.png")
        Me.iconList.Images.SetKeyName(225, "sonic-the-hedgehog-filled.png")
        Me.iconList.Images.SetKeyName(226, "soundcloud-logo-filled.png")
        Me.iconList.Images.SetKeyName(227, "sport-net-filled.png")
        Me.iconList.Images.SetKeyName(228, "spotify-filled.png")
        Me.iconList.Images.SetKeyName(229, "squash-filled.png")
        Me.iconList.Images.SetKeyName(230, "squash-racquet-filled.png")
        Me.iconList.Images.SetKeyName(231, "star-wars-filled.png")
        Me.iconList.Images.SetKeyName(232, "steam-logo-filled.png")
        Me.iconList.Images.SetKeyName(233, "steampunk-filled.png")
        Me.iconList.Images.SetKeyName(234, "subtitles-filled.png")
        Me.iconList.Images.SetKeyName(235, "superman-filled.png")
        Me.iconList.Images.SetKeyName(236, "super-mario-filled.png")
        Me.iconList.Images.SetKeyName(237, "sway-filled.png")
        Me.iconList.Images.SetKeyName(238, "swift-filled.png")
        Me.iconList.Images.SetKeyName(239, "sword-and-shield-filled.png")
        Me.iconList.Images.SetKeyName(240, "symantec-filled.png")
        Me.iconList.Images.SetKeyName(241, "targaryen-house-filled.png")
        Me.iconList.Images.SetKeyName(242, "teamspeak-new-logo-filled.png")
        Me.iconList.Images.SetKeyName(243, "teamviewer-filled.png")
        Me.iconList.Images.SetKeyName(244, "tencent-weibo-filled.png")
        Me.iconList.Images.SetKeyName(245, "tennis-ball-filled.png")
        Me.iconList.Images.SetKeyName(246, "the-dragon-team-filled.png")
        Me.iconList.Images.SetKeyName(247, "the-flash-sign-filled.png")
        Me.iconList.Images.SetKeyName(248, "track-and-field-filled.png")
        Me.iconList.Images.SetKeyName(249, "triforce-filled.png")
        Me.iconList.Images.SetKeyName(250, "trophy-cup-filled.png")
        Me.iconList.Images.SetKeyName(251, "tully-house-filled.png")
        Me.iconList.Images.SetKeyName(252, "tumblr-filled.png")
        Me.iconList.Images.SetKeyName(253, "twilight-filled.png")
        Me.iconList.Images.SetKeyName(254, "twitch-filled.png")
        Me.iconList.Images.SetKeyName(255, "two-circles-in-a-square-filled.png")
        Me.iconList.Images.SetKeyName(256, "two-tickets-filled.png")
        Me.iconList.Images.SetKeyName(257, "tyrell-house-filled.png")
        Me.iconList.Images.SetKeyName(258, "ubuntu-logo-filled.png")
        Me.iconList.Images.SetKeyName(259, "uc-browser-filled.png")
        Me.iconList.Images.SetKeyName(260, "unity-filled.png")
        Me.iconList.Images.SetKeyName(261, "unreal-engine-filled.png")
        Me.iconList.Images.SetKeyName(262, "uplay-filled.png")
        Me.iconList.Images.SetKeyName(263, "v-bucks-filled.png")
        Me.iconList.Images.SetKeyName(264, "vegas-filled.png")
        Me.iconList.Images.SetKeyName(265, "viber-filled.png")
        Me.iconList.Images.SetKeyName(266, "video-game-controller-outline-filled.png")
        Me.iconList.Images.SetKeyName(267, "video-projector-filled.png")
        Me.iconList.Images.SetKeyName(268, "vimeo-filled.png")
        Me.iconList.Images.SetKeyName(269, "vine-filled.png")
        Me.iconList.Images.SetKeyName(270, "virtualbox-filled.png")
        Me.iconList.Images.SetKeyName(271, "visual-game-boy-filled.png")
        Me.iconList.Images.SetKeyName(272, "vk.com-filled.png")
        Me.iconList.Images.SetKeyName(273, "vlc-logo-filled.png")
        Me.iconList.Images.SetKeyName(274, "web-shield-filled.png")
        Me.iconList.Images.SetKeyName(275, "wechat-logo-filled.png")
        Me.iconList.Images.SetKeyName(276, "weibo-filled.png")
        Me.iconList.Images.SetKeyName(277, "western-filled.png")
        Me.iconList.Images.SetKeyName(278, "whatsapp-filled.png")
        Me.iconList.Images.SetKeyName(279, "wii-filled.png")
        Me.iconList.Images.SetKeyName(280, "wikipedia-filled.png")
        Me.iconList.Images.SetKeyName(281, "winamp-filled.png")
        Me.iconList.Images.SetKeyName(282, "windows-8-logo-filled.png")
        Me.iconList.Images.SetKeyName(283, "winrar-filled.png")
        Me.iconList.Images.SetKeyName(284, "woody-woodpecker-filled.png")
        Me.iconList.Images.SetKeyName(285, "wordpress-filled.png")
        Me.iconList.Images.SetKeyName(286, "world-cup-filled.png")
        Me.iconList.Images.SetKeyName(287, "world-wide-web-filled.png")
        Me.iconList.Images.SetKeyName(288, "wrestling-filled.png")
        Me.iconList.Images.SetKeyName(289, "xbox-filled.png")
        Me.iconList.Images.SetKeyName(290, "xbox-menu-filled.png")
        Me.iconList.Images.SetKeyName(291, "x-men-filled.png")
        Me.iconList.Images.SetKeyName(292, "youtube-filled.png")
        Me.iconList.Images.SetKeyName(293, "youtube-play-button-logo-filled.png")
        '
        'flpSearchResult
        '
        Me.flpSearchResult.AutoScroll = True
        Me.flpSearchResult.BackColor = System.Drawing.Color.FromArgb(CType(CType(35, Byte), Integer), CType(CType(39, Byte), Integer), CType(CType(42, Byte), Integer))
        Me.flpSearchResult.Dock = System.Windows.Forms.DockStyle.Right
        Me.flpSearchResult.Location = New System.Drawing.Point(1235, 0)
        Me.flpSearchResult.Name = "flpSearchResult"
        Me.flpSearchResult.ParentForm = Me
        Me.flpSearchResult.Size = New System.Drawing.Size(250, 725)
        Me.flpSearchResult.TabIndex = 31
        '
        'txtSearch
        '
        Me.txtSearch.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtSearch.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.txtSearch.EnterPressed = False
        Me.txtSearch.LightTheme = False
        Me.txtSearch.Location = New System.Drawing.Point(1329, 6)
        Me.txtSearch.MaxLength = 32767
        Me.txtSearch.Multiline = False
        Me.txtSearch.Name = "txtSearch"
        Me.txtSearch.Placeholder = "Search"
        Me.txtSearch.ReadOnly = False
        Me.txtSearch.RightImage = Global.LaunchMenu.My.Resources.Resources.search_500
        Me.txtSearch.RightImageSize = New System.Drawing.Size(17, 17)
        Me.txtSearch.Size = New System.Drawing.Size(150, 29)
        Me.txtSearch.TabIndex = 29
        Me.txtSearch.Text = "Search"
        Me.txtSearch.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtSearch.TextColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtSearch.UseSystemPasswordChar = False
        '
        'tLoading
        '
        Me.tLoading.Enabled = True
        Me.tLoading.Interval = 1
        '
        'tAnimation
        '
        Me.tAnimation.Interval = 30
        '
        'Skin1
        '
        Me.Skin1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Skin1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Skin1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Skin1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Skin1.Location = New System.Drawing.Point(1425, 0)
        Me.Skin1.MaximumSize = New System.Drawing.Size(100, 35)
        Me.Skin1.MinimumSize = New System.Drawing.Size(100, 35)
        Me.Skin1.Name = "Skin1"
        Me.Skin1.Padding = New System.Windows.Forms.Padding(6)
        Me.Skin1.Size = New System.Drawing.Size(100, 35)
        Me.Skin1.TabIndex = 0
        '
        'frmMenu
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(1509, 815)
        Me.Controls.Add(Me.pJustForVisual)
        Me.Controls.Add(Me.Skin1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MinimumSize = New System.Drawing.Size(1276, 854)
        Me.Name = "frmMenu"
        Me.Opacity = 0R
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Launch Menu"
        Me.TransparencyKey = System.Drawing.Color.Fuchsia
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.pJustForVisual.ResumeLayout(False)
        Me.pDock.ResumeLayout(False)
        Me.tcGames.ResumeLayout(False)
        Me.tpWelcome.ResumeLayout(False)
        Me.tpWelcome.PerformLayout()
        Me.tpNewGame.ResumeLayout(False)
        Me.tpNewMovie.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents Skin1 As Skin
    Friend WithEvents txtSearch As MDTextBox
    Friend WithEvents pJustForVisual As Panel
    Friend WithEvents tcGames As XylosTabControl
    Friend WithEvents tpNewGame As TabPage
    Friend WithEvents tpNewMovie As TabPage
    Friend WithEvents tpRekon As TabPage
    Friend WithEvents tpWelcome As TabPage
    Friend WithEvents SliderSItem4 As SliderSItem
    Friend WithEvents SliderSItem3 As SliderSItem
    Friend WithEvents SliderSItem2 As SliderSItem
    Friend WithEvents SliderSItem1 As SliderSItem
    Friend WithEvents SliderLItem1 As SliderLItem
    Friend WithEvents flpNewsLeft As FlowLayoutPanel
    Friend WithEvents Label1 As Label
    Friend WithEvents tLoading As Timer
    Friend WithEvents flpQuickLaunch As FlowLayoutPanel
    Friend WithEvents Label2 As Label
    Friend WithEvents iconList As ImageList
    Friend WithEvents tAnimation As Timer
    Friend WithEvents pDock As Panel
    Friend WithEvents flpSocial As FlowLayoutPanel
    Friend WithEvents flpNewMovies As SmoothAutoScrollFlowLayoutPanel
    Friend WithEvents flpNewGames As SmoothAutoScrollFlowLayoutPanel
    Friend WithEvents flpSearchResult As SmoothAutoScrollFlowLayoutPanel
End Class
