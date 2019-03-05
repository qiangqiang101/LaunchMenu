Imports System.Xml.Serialization
Imports System.IO

Public Structure MenuSettings

    Public ReadOnly Property Instance As MenuSettings
        Get
            Return ReadFromFile()
        End Get
    End Property

    <XmlIgnore>
    Public Property SettingsFileName() As String

    Public CompanyName As String
    Public Language As String
    Public ActivityFeeds As String
    Public FeedsURL As String
    Public MediaNode As String
    Public GameGernes As List(Of Gerne)
    Public GamePublishers As List(Of Publisher)
    Public GameDevelopers As List(Of Developer)
    Public AppDevelopers As List(Of Developer)
    Public AppTypes As List(Of AppType)
    Public MovieGernes As List(Of Gerne)
    Public MovieProductions As List(Of Production)
    Public MovieDistributors As List(Of Distributor)
    Public MovieStars As List(Of Actor)
    Public Social As List(Of SocialIcon)
    Public ScreenLock As Boolean
    Public Mouse As Boolean
    Public Notepad As Boolean
    Public Calculator As Boolean
    Public IExplorer As Boolean
    Public Magnifier As Boolean
    Public MathInput As Boolean
    Public SnippingTool As Boolean
    Public Wordpad As Boolean
    Public Controller As Boolean
    Public MSEdge As Boolean
    Public Activity As Boolean
    Public NewItems As Boolean
    Public GameItemSize As Size
    Public AppItemSize As Size
    Public MovieItemSize As Size

    Public Sub New(fn As String)
        SettingsFileName = fn
    End Sub

    'Public Sub New(fn As String, cn As String, l As String, af As String, fu As String, mn As String, gg As List(Of Gerne), gp As List(Of Publisher), gd As List(Of Developer),
    '               ad As List(Of Developer), apt As List(Of AppType), mg As List(Of Gerne), mp As List(Of Production), md As List(Of Distributor), ms As List(Of Actor))
    '    SettingsFileName = fn
    '    CompanyName = fn
    '    Language = l
    '    ActivityFeeds = af
    '    FeedsURL = fu
    '    MediaNode = mn
    '    GameGernes = gg
    '    GamePublishers = gp
    '    GameDevelopers = gd
    '    AppDevelopers = ad
    '    AppTypes = apt
    '    MovieGernes = mg
    '    MovieProductions = mp
    '    MovieDistributors = md
    '    MovieStars = ms
    'End Sub

    Public Sub Save()
        Dim ser = New XmlSerializer(GetType(MenuSettings))
        Dim writer As TextWriter = New StreamWriter(SettingsFileName)
        ser.Serialize(writer, Me)
        writer.Close()
    End Sub

    Public Function ReadFromFile() As MenuSettings
        If Not File.Exists(SettingsFileName) Then
            Return New MenuSettings(SettingsFileName) ', CompanyName, Language, ActivityFeeds, FeedsURL, MediaNode, GameGernes, GamePublishers, GameDevelopers, AppDevelopers, AppTypes,
            'MovieGernes, MovieProductions, MovieDistributors, MovieStars)
        End If

        Try
            Dim ser = New XmlSerializer(GetType(MenuSettings))
            Dim reader As TextReader = New StreamReader(SettingsFileName)
            Dim instance = CType(ser.Deserialize(reader), MenuSettings)
            reader.Close()
            Return instance
        Catch ex As Exception
            Return New MenuSettings(SettingsFileName) ', CompanyName, Language, ActivityFeeds, FeedsURL, MediaNode, GameGernes, GamePublishers, GameDevelopers, AppDevelopers, AppTypes,
            'MovieGernes, MovieProductions, MovieDistributors, MovieStars)
        End Try
    End Function

End Structure

Public Structure Gerne
    Public Name As String

    Public Sub New(n As String)
        Name = n
    End Sub
End Structure

Public Structure Publisher
    Public Name As String

    Public Sub New(n As String)
        Name = n
    End Sub
End Structure

Public Structure Developer
    Public Name As String

    Public Sub New(n As String)
        Name = n
    End Sub
End Structure

Public Structure AppType
    Public Name As String

    Public Sub New(n As String)
        Name = n
    End Sub
End Structure

Public Structure Production
    Public Name As String

    Public Sub New(n As String)
        Name = n
    End Sub
End Structure

Public Structure Distributor
    Public Name As String

    Public Sub New(n As String)
        Name = n
    End Sub
End Structure

Public Structure Actor
    Public Name As String

    Public Sub New(n As String)
        Name = n
    End Sub
End Structure

Public Structure SocialIcon
    Public Logo As String
    Public URL As String

    Public Sub New(sl As String, u As String)
        Logo = sl
        URL = u
    End Sub
End Structure