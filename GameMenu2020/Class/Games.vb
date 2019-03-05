Imports System.Drawing.Imaging
Imports System.IO
Imports System.Xml.Serialization

Public Structure GameList

    Public ReadOnly Property Instance As GameList
        Get
            Return ReadFromFile()
        End Get
    End Property

    <XmlIgnore>
    Public Property FileName() As String

    Public Category As String
    Public Index As Integer
    Public Type As CategoryType
    Public Games As List(Of Game)
    Public HasIcon As Boolean
    Public Icon As CategoryIcon

    <XmlIgnore>
    Public ReadOnly Property TotalGames() As Integer
        Get
            Return Games.Count
        End Get
    End Property

    Public Sub New(_filename As String)
        FileName = _filename
    End Sub

    Public Sub New(_filename As String, c As String, i As Integer, t As CategoryType, g As List(Of Game), hi As Boolean, ic As Integer)
        FileName = _filename
        Category = c
        Index = i
        Type = t
        Games = g
        HasIcon = hi
        Icon = ic
    End Sub

    Public Sub Save()
        Dim ser = New XmlSerializer(GetType(GameList))
        Dim writer As TextWriter = New StreamWriter(FileName)
        ser.Serialize(writer, Me)
        writer.Close()
    End Sub

    Public Function ReadFromFile() As GameList
        If Not File.Exists(FileName) Then
            Return New GameList(FileName, Category, Index, Type, Games, HasIcon, Icon)
        End If

        Try
            Dim ser = New XmlSerializer(GetType(GameList))
            Dim reader As TextReader = New StreamReader(FileName)
            Dim instance = CType(ser.Deserialize(reader), GameList)
            reader.Close()
            Return instance
        Catch ex As Exception
            Return New GameList(FileName, Category, Index, Type, Games, HasIcon, Icon)
        End Try
    End Function

End Structure

Public Structure Game

    Public Name As String
    Public Path As String
    Public StartIn As String
    Public InstallDate As Date
    Public Website As String
    Public Description As String
    Public Gerne As String
    Public Publisher As String
    Public Developer As String
    Public Rating As String
    Public Image As String

    Public Function GetImage() As Image
        Dim b64 As String = Image.Replace(" ", "+")
        Dim bite() As Byte = Convert.FromBase64String(b64)
        Dim stream As New MemoryStream(bite)
        Return Drawing.Image.FromStream(stream)
    End Function

    Public Sub SetImage(img As Image)
        Dim stream As New MemoryStream
        Dim bmp As Bitmap = New Bitmap(img)
        bmp.Save(stream, ImageFormat.Png)
        Image = Convert.ToBase64String(stream.ToArray)
    End Sub

    Public Sub New(n As String, p As String, si As String, id As Date, w As String, d As String, g As String, pub As String, dev As String, r As String, i As String)
        Name = n
        Path = p
        StartIn = si
        InstallDate = id
        Website = w
        Description = d
        Gerne = g
        Publisher = pub
        Developer = dev
        Rating = r
        Image = i
    End Sub
End Structure

Public Enum CategoryType
    Game
    Application
    Movie
    Seperator
End Enum

Public Enum CategoryIcon
    _3DGlasses
    _3dsMax
    _007
    AdamSandler
    AfterEffects
    Bridge
    Dreamweaver
    Fireworks
    Flash
    Framemaker
    Illustrator
    InDesign
    Lightroom
    Photoshop
    AMD
    Football
    Animation
    Apple
    ArrynHouse
    Astronaut
    Audacity
    Audience
    Autocad
    BartSimpson
    Baseball
    Batman
    BattleNet
    BBC
    Behance
    Bing
    BlackBlood
    Twitter1
    Twitter2
    Bluray
    BodyArmor
    Bowling
    BowlingPins
    BowlingSpare
    BronzeMedal
    Brutus
    CaptainAmerica
    CD
    Chrome
    Comedy
    ComedyDramaMask
    CommandLine
    CommunicationMobile
    CompactDisc
    Cricket
    DayOfTheTentacle
    DeadSkull
    Delicious
    Dell
    DeviantArt
    Dice
    Digg
    DVD
    Discord
    Disney
    DocumentaryFilm
    Dolphin
    Doraemon
    Dota2
    Dropbox
    DTS
    DuckDuckGo
    Ebay
    Emu4Ios
    EquestrianStatue
    Evernote
    Facebook
    FacebookMessenger
    FidgetSpinner
    Fight
    FilmNoir
    Firefox
    Instagram1
    FoodAsResource
    FrankensteinsMonster
    Garena
    Git
    Github
    Gitlab
    Gmail
    GoogleCloud
    GoogleDocs
    GoogleDrive
    GoogleEarth
    Google
    GoogleForms
    GoogleFormsNew
    GoogleGroups
    GoogleImages
    GoogleKeep
    GoogleMaps
    GoogleNews
    GooglePhotos
    GooglePlayMusic
    GooglePlus
    GoogleSheets
    GoogleSites
    GoogleSlides
    GoogleWallet
    GreyjoyHouse
    Hammerstein
    Hawkeys
    HawkeysSymbol
    HBO
    HBOGo
    HD720p
    HD1080p
    HDTV
    Hedgehog
    HighRes
    HomerSimpson
    HouseStark
    Hulk
    Hulu
    Icq
    Imdb
    iMovie
    IndianaJones
    Instagram2
    IE
    IronMan
    JasonVoorhees
    JavelinThrow
    JetpackJoyride
    Joker
    JokerSuicideSquad
    Kicking
    Kik
    KylieJenner
    LacrosseStick
    Launchpad
    LOL
    Lego
    Linkedin
    Linux
    LisaSimpson
    LovingHearts
    MeggieSimpson
    MagicalScroll
    MagicLamp
    MargeSimpson
    MartellHouse
    Maxthon
    MSAccess
    MSEdge
    MSExcel
    MSExchange
    MSGroove
    MSOffice
    MSOneNote
    MSOutlook
    MSPaint
    MSPowerpoint
    MSPublisher
    MSSharepoint
    MSVisio
    MSWindows
    MSWord
    MilesMorales
    Minecraft
    MinecraftMainCharacter
    Minion
    Minion2
    MovieClapperTool
    Movie
    MovieFilmStrip
    MoviePopcorn1
    MoviePopcorn2
    MovieProjector
    MovieRoll
    MoviesFolder
    MovieTicket
    MuscleBuilding
    MusicNote
    Myspace1
    Myspace2
    Naruto
    NeedForSpeed
    Nintendo
    NintendoWii
    NintendoWithCard
    ObsStudio
    OldGoogle
    OneDrive
    Opera
    Origin
    OverwatchLeague
    Paypal
    pCloud
    Pennywise
    PersonKickingBall
    Pinwheel
    PiratesOfTheCaribbean
    PixarLamp
    Pokemon
    PoleVault
    PopcornMaker
    QQ
    QueenOfClubs
    Racket
    Reddit
    Renren
    RetroTV
    RickSanchez
    RockstarGames
    RottenTomatoes
    RubicsCube
    Ruby
    Safari
    SafariWeb
    Scoreboard
    Shortcuts
    ShotPut
    Showtime
    Shuttercock
    SilverMedal
    Skittle
    Skype
    Snapchat
    SoccerYellowCard
    SoftetherVPN
    SonicTheHedgehog
    Soundcloud
    SportNet
    Spotify
    Squash
    SquashRacquet
    StarWars
    Steam
    Steampunk
    Subtitles
    Superman
    SuperMario
    Sway
    Swift
    SwordAndShield
    Symantec
    Targaryen
    Teamspeak
    Teamviewer
    TencentWeibo
    TennisBall
    TheDragonTeam
    TheFlashSign
    TrackAndField
    Triforce
    TrophyCup
    TullyHouse
    Tumblr
    Twilight
    Twitch
    TwoCirclesInASquare
    TwoTickets
    TyrellHouse
    Ubuntu
    UCBrowser
    Unity
    UnrealEngine
    Uplay
    Vbucks
    Vegas
    Viber
    VideoGameController
    VideoProjector
    Vimeo
    Vine
    VirtualBox
    VisualGameBoy
    VK
    VLC
    WebShield
    Wechat
    Weibo
    Western
    WhatsApp
    Wii
    Wikipedia
    Winamp
    Windows8
    WinRAR
    WoodyWoodpecker
    Wordpress
    WorldCup
    WWW
    Wrestling
    Xbox
    XboxMenu
    Xmen
    Youtube
    YoutubePlay
End Enum
