Imports IWshRuntimeLibrary

Public Class Shortcut

    Public Property FileName As String

    Public ReadOnly Property Name As String
        Get
            Return IO.Path.GetFileNameWithoutExtension(GetFullName())
        End Get
    End Property

    Public ReadOnly Property Description As String
        Get
            Return GetDescription()
        End Get
    End Property

    Public ReadOnly Property FileLocation As String
        Get
            Return IO.Path.GetDirectoryName(FileName)
        End Get
    End Property

    Public ReadOnly Property Target As String
        Get
            Return GetTargetPath()
        End Get
    End Property

    Public ReadOnly Property WorkingDirectory As String
        Get
            Return GetWorkingDirectory()
        End Get
    End Property

    Public ReadOnly Property Hotkey As String
        Get
            Return GetHotkey()
        End Get
    End Property

    Public ReadOnly Property Arguments As String
        Get
            Return GetArguments()
        End Get
    End Property

    Public ReadOnly Property IconLocation As String
        Get
            Return GetIconLocation()
        End Get
    End Property

    Public Sub New(lnkFile As String)
        FileName = lnkFile
    End Sub

    Private Function GetWorkingDirectory() As String
        Dim wShell As New WshShell()
        Dim shortcut As WshShortcut = CType(wShell.CreateShortcut(FileName), WshShortcut)
        Return shortcut.WorkingDirectory
    End Function

    Private Function GetDescription() As String
        Dim wShell As New WshShell()
        Dim shortcut As WshShortcut = CType(wShell.CreateShortcut(FileName), WshShortcut)
        Return shortcut.Description
    End Function

    Private Function GetFullName() As String
        Dim wShell As New WshShell()
        Dim shortcut As WshShortcut = CType(wShell.CreateShortcut(FileName), WshShortcut)
        Return shortcut.FullName
    End Function

    Private Function GetHotkey() As String
        Dim wShell As New WshShell()
        Dim shortcut As WshShortcut = CType(wShell.CreateShortcut(FileName), WshShortcut)
        Return shortcut.Hotkey
    End Function

    Private Function GetIconLocation() As String
        Dim wShell As New WshShell()
        Dim shortcut As WshShortcut = CType(wShell.CreateShortcut(FileName), WshShortcut)
        Return shortcut.IconLocation
    End Function

    Private Function GetTargetPath() As String
        Dim wShell As New WshShell()
        Dim shortcut As WshShortcut = CType(wShell.CreateShortcut(FileName), WshShortcut)
        Return shortcut.TargetPath
    End Function

    Private Function GetArguments() As String
        Dim wShell As New WshShell()
        Dim shortcut As WshShortcut = CType(wShell.CreateShortcut(FileName), WshShortcut)
        Return shortcut.Arguments
    End Function

End Class