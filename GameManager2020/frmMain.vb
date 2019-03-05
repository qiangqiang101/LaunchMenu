Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Security.Principal
Imports System.Text
Imports System.Threading
Imports TsudaKageyu

Public Class frmMain

    <DllImport("uxtheme.dll", ExactSpelling:=True, CharSet:=CharSet.Unicode)>
    Private Shared Function SetWindowTheme(ByVal hwnd As IntPtr, ByVal pszSubAppName As String, ByVal pszSubIdList As String) As Integer
    End Function

    Private xmlPath As String = $"{Application.StartupPath}\Apps"
    Private settingFile As String = $"{Application.StartupPath}\Data\Settings.xml"
    Private gameImageZip As String = $"{Application.StartupPath}\Data\Games.zip"
    Private movieImageZip As String = $"{Application.StartupPath}\Data\Movies.zip"
    Private appImageZip As String = $"{Application.StartupPath}\Data\Apps.zip"
    Private CurrentSettings As MenuSettings
    Private userHWID As String
    Public skipCheck As Boolean = False
    Private registered As Boolean = False
    Private gameInitial As String = My.Computer.FileSystem.SpecialDirectories.Desktop
    Private appInitial As String = My.Computer.FileSystem.SpecialDirectories.Desktop
    Private movieInitial As String = My.Computer.FileSystem.SpecialDirectories.Desktop
    Public splashPercentage As Integer = 0

    Private Sub frmMain_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Hide()
        CheckForIllegalCrossThreadCalls = False

        Try
            userHWID = $"{getHWID.ToUpper} - {SystemInformation.ComputerName}"

            If skipCheck Then
                Load_frmMain()
            Else
                If CheckForInternetConnection() Then
                    If userHWID.CheckActivation() Then
                        registered = True
                        Load_frmMain()
                    Else
                        frmActivate.Show()
                        Close()
                    End If
                Else
                    Close()
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Load_frmMain()
        SetWindowTheme(lvCategory.Handle, "Explorer", Nothing)
        SetWindowTheme(lvApp.Handle, "Explorer", Nothing)
        SetWindowTheme(lvGame.Handle, "Explorer", Nothing)
        SetWindowTheme(lvMovie.Handle, "Explorer", Nothing)
        SetWindowTheme(lvSocial.Handle, "Explorer", Nothing)

        Dim ms As New MenuSettings(settingFile)
        ms.ReadFromFile()
        CurrentSettings = ms.Instance

        splashPercentage = 5

        Dim ct As New Thread(AddressOf PopulateCategory) : ct.Start()
        splashPercentage = 10
        Dim gt As New Thread(AddressOf PopulateGame) : gt.Start()
        splashPercentage = 20
        Dim at As New Thread(AddressOf PopulateApp) : at.Start()
        splashPercentage = 30
        Dim mt As New Thread(Sub() PopulateMovie(True)) : mt.Start()
        splashPercentage = 40
        Dim lt As New Thread(AddressOf LoadLanguageFileList) : lt.Start()
        splashPercentage = 50
        Dim st As New Thread(AddressOf LoadSettings) : st.Start()
        splashPercentage = 60
        Dim sgt As New Thread(AddressOf LoadSettingsGame) : sgt.Start()
        splashPercentage = 70
        Dim sat As New Thread(AddressOf LoadSettingsApp) : sat.Start()
        splashPercentage = 80
        Dim smt As New Thread(AddressOf LoadSettingsMovie) : smt.Start()
        splashPercentage = 90
        cmbCatIcon.SelectedIndex = 0

        AboutLoad()
        splashPercentage = 100

        Thread.Sleep(5000)
        tcTab.SelectTab(1)
        Show()

    End Sub

#Region "Category"

    Private cSI As ListViewItem

    Private Sub PopulateCategory()
        Try
            lvCategory.Items.Clear()

            If Directory.Exists(xmlPath) Then
                For Each cat In Directory.GetFiles(xmlPath, "*.xml")
                    Dim t As New GameList(cat)
                    t.ReadFromFile()
                    Dim gl As GameList = t.Instance

                    Dim item As New ListViewItem(gl.Category)
                    With item
                        .SubItems.Add($"{gl.TotalGames} {gl.Type.ToName}(s)")
                        .SubItems.Add(gl.Index)
                        .SubItems.Add(gl.Type.ToName)
                        .SubItems.Add($"{If(gl.HasIcon, "✔", "✘")}")
                        .SubItems.Add(gl.Icon.ToName)
                        .Tag = cat
                    End With
                    lvCategory.Items.Add(item)
                Next

                lvCategory.Sort(chSort)
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiCatEdit_Click(sender As Object, e As EventArgs) Handles tsmiCatEdit.Click
        Try
            If Not lvCategory.SelectedItems.Count = 0 Then
                cSI = lvCategory.SelectedItems.Item(0)
                txtCatName.Text = cSI.Text
                txtCatIndex.Text = cSI.SubItems(2).Text
                cmbCatType.SelectedItem = cSI.SubItems(3).Text
                cmbCatIcon.SelectedItem = cSI.SubItems(5).Text
                cbCatIcon.Checked = If(cSI.SubItems(4).Text = "✔", True, False)
                btnCatAdd.Enabled = False
                btnCatEdit.Enabled = True
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lvCategory_DoubleClick(sender As Object, e As EventArgs) Handles lvCategory.DoubleClick
        If Not lvCategory.SelectedItems.Count = 0 Then
            tsmiCatEdit.PerformClick()
        End If
    End Sub

    Private Sub tsmiCatRefresh_Click(sender As Object, e As EventArgs) Handles tsmiCatRefresh.Click
        Dim ct As New Thread(AddressOf PopulateCategory) : ct.Start()
    End Sub

    Private Sub tsmiCatDelete_Click(sender As Object, e As EventArgs) Handles tsmiCatDelete.Click
        Try
            If Not lvCategory.SelectedItems.Count = 0 Then
                cSI = lvCategory.SelectedItems.Item(0)
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {cSI.Text}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    If File.Exists(cSI.Tag) Then File.Delete(cSI.Tag)
                    lvCategory.Items.Remove(cSI)
                End If
                cSI = Nothing
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnCatAdd_Click(sender As Object, e As EventArgs) Handles btnCatAdd.Click
        If txtCatName.Text = Nothing Then
            MsgBox("Please Enter Category Name.", MsgBoxStyle.Exclamation, "Error")
        ElseIf txtCatIndex.Text = Nothing Then
            MsgBox("Please Enter Index.", MsgBoxStyle.Exclamation, "Error")
        Else
            Try
                Dim item As New ListViewItem(txtCatName.Text)
                With item
                    .SubItems.Add($"{0} {cmbCatType.SelectedItem}(s)")
                    .SubItems.Add(txtCatIndex.Text)
                    .SubItems.Add(cmbCatType.SelectedItem)
                    .SubItems.Add($"{If(cbCatIcon.Checked, "✔", "✘")}")
                    .SubItems.Add(cmbCatIcon.SelectedItem)
                    .Tag = $"{xmlPath}\{txtCatName.Text}.xml"
                End With
                lvCategory.Items.Add(item)

                txtCatName.Clear()
                txtCatIndex.Clear()
                cmbCatIcon.SelectedIndex = 0
                cmbCatType.SelectedIndex = 0
                cbCatIcon.Checked = False
            Catch ex As Exception
                Logger.Log($"{ex.Message} {ex.StackTrace}")
                MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub btnCatEdit_Click(sender As Object, e As EventArgs) Handles btnCatEdit.Click
        If txtCatName.Text = Nothing Then
            MsgBox("Please Enter Category Name.", MsgBoxStyle.Exclamation, "Error")
        ElseIf txtCatIndex.Text = Nothing Then
            MsgBox("Please Enter Index.", MsgBoxStyle.Exclamation, "Error")
        Else
            Try
                Dim t As New GameList(cSI.Tag)
                t.ReadFromFile()
                Dim gl As GameList = t.Instance

                cSI.Text = txtCatName.Text
                cSI.SubItems(1).Text = $"{If(gl.TotalGames = Nothing, 0, gl.TotalGames)} {cmbCatType.SelectedItem}(s)"
                cSI.SubItems(2).Text = txtCatIndex.Text
                cSI.SubItems(3).Text = cmbCatType.SelectedItem
                cSI.SubItems(4).Text = $"{If(cbCatIcon.Checked, "✔", "✘")}"
                cSI.SubItems(5).Text = cmbCatIcon.SelectedItem
                cSI = Nothing

                txtCatName.Clear()
                txtCatIndex.Clear()
                cmbCatIcon.SelectedIndex = 0
                cmbCatType.SelectedIndex = 0
                cbCatIcon.Checked = False
                btnCatEdit.Enabled = False
                btnCatAdd.Enabled = True
            Catch ex As Exception
                Logger.Log($"{ex.Message} {ex.StackTrace}")
                MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub btnCatCancel_Click(sender As Object, e As EventArgs) Handles btnCatCancel.Click
        txtCatName.Clear()
        txtCatIndex.Clear()
        cmbCatIcon.SelectedIndex = 0
        cmbCatType.SelectedIndex = 0
        cbCatIcon.Checked = False
        btnCatAdd.Enabled = True
        btnCatEdit.Enabled = False
    End Sub

    Private Sub btnCatSave_Click(sender As Object, e As EventArgs) Handles btnCatSave.Click
        For Each item As ListViewItem In lvCategory.Items
            Dim temp As New GameList(item.Tag)
            temp.ReadFromFile()
            Dim gl = temp.Instance
            gl.Category = item.Text
            gl.HasIcon = If(item.SubItems(4).Text = "✔", True, False)
            gl.Icon = CInt(DirectCast([Enum].Parse(GetType(CategoryIcon), item.SubItems(5).Text), CategoryIcon))
            gl.Index = item.SubItems(2).Text
            gl.Type = CInt(DirectCast([Enum].Parse(GetType(CategoryType), item.SubItems(3).Text), CategoryType))
            gl.FileName = item.Tag
            gl.Save()
        Next
        MsgBox("Category Save Completed.", MsgBoxStyle.Information, "LaunchManager")
    End Sub

    Private Sub cmbCatIcon_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbCatIcon.SelectedIndexChanged
        pbIcon.Image = iconList.Images(cmbCatIcon.SelectedIndex)
    End Sub

    Private Sub txtCatIndex_KeyPress(sender As Object, e As KeyPressEventArgs) Handles txtCatIndex.KeyPress
        If Not Char.IsNumber(e.KeyChar) AndAlso Not Char.IsControl(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

    Private Sub cmbCatIcon_DrawItem(sender As Object, e As DrawItemEventArgs) Handles cmbCatIcon.DrawItem
        If e.Index <> -1 Then
            e.Graphics.DrawImage(iconList.Images(e.Index), e.Bounds.Left, e.Bounds.Top)
        End If
    End Sub

    Private Sub cmbCatIcon_MeasureItem(sender As Object, e As MeasureItemEventArgs) Handles cmbCatIcon.MeasureItem
        e.ItemHeight = iconList.ImageSize.Height
        e.ItemWidth = iconList.ImageSize.Width
    End Sub

#End Region

#Region "Settings"
#Region "General Settings"

    Private sSI As ListViewItem

    Private Sub LoadLanguageFileList()
        For Each file As String In Directory.GetFiles($"{Application.StartupPath}\Data\Language", "*.ini")
            cmbSetLang.Items.Add(Path.GetFileNameWithoutExtension(file))
        Next
    End Sub

    Private Sub LoadSettings()
        Try
            txtSetName.Text = CurrentSettings.CompanyName
            cmbSetLang.SelectedItem = CurrentSettings.Language
            cmbSetRSS.SelectedItem = CurrentSettings.ActivityFeeds
            txtSetFeedURL.Text = CurrentSettings.FeedsURL
            txtSetMediaNode.Text = CurrentSettings.MediaNode

            numGameW.Value = CurrentSettings.GameItemSize.Width
            numGameH.Value = CurrentSettings.GameItemSize.Height
            numAppW.Value = CurrentSettings.AppItemSize.Width
            numAppH.Value = CurrentSettings.AppItemSize.Height
            numMovieW.Value = CurrentSettings.MovieItemSize.Width
            numMovieH.Value = CurrentSettings.MovieItemSize.Height
            cbActivity.Checked = CurrentSettings.Activity
            cbNewItem.Checked = CurrentSettings.NewItems

            For Each si As SocialIcon In CurrentSettings.Social
                Dim item As New ListViewItem(si.Logo)
                With item
                    .SubItems.Add(si.URL)
                    .Tag = si
                End With
                lvSocial.Items.Add(item)
            Next

            cbQLSLock.Checked = CurrentSettings.ScreenLock
            cbQLMouse.Checked = CurrentSettings.Mouse
            cbQLNote.Checked = CurrentSettings.Notepad
            cbQLCalc.Checked = CurrentSettings.Calculator
            cbQLIE.Checked = CurrentSettings.IExplorer
            cbQLMag.Checked = CurrentSettings.Magnifier
            cbQLMath.Checked = CurrentSettings.MathInput
            cbQLSnip.Checked = CurrentSettings.SnippingTool
            cbQLWord.Checked = CurrentSettings.Wordpad
            cbQLJoy.Checked = CurrentSettings.Controller
            cbQLEdge.Checked = CurrentSettings.MSEdge
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub cmbSetRSS_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbSetRSS.SelectedIndexChanged
        Select Case CType(cmbSetRSS.SelectedItem, String)
            Case "IGN"
                txtSetFeedURL.Text = "http://feeds.ign.com/ign/e3-videos"
                txtSetFeedURL.Enabled = False
                txtSetMediaNode.Enabled = False
                txtSetMediaNode.Text = "media:thumbnail/@url"
            Case "Gamespot"
                txtSetFeedURL.Text = "https://www.gamespot.com/feeds/mashup/"
                txtSetFeedURL.Enabled = False
                txtSetMediaNode.Enabled = False
                txtSetMediaNode.Text = "media:content/@url"
            Case "PC Gamer"
                txtSetFeedURL.Text = "https://www.pcgamer.com/rss/"
                txtSetFeedURL.Enabled = False
                txtSetMediaNode.Enabled = False
                txtSetMediaNode.Text = "enclosure/@url"
            Case "Digital Trends"
                txtSetFeedURL.Text = "https://www.digitaltrends.com/gaming/feed/"
                txtSetFeedURL.Enabled = False
                txtSetMediaNode.Enabled = False
                txtSetMediaNode.Text = "enclosure/@url"
            Case "Custom"
                txtSetFeedURL.Text = ""
                txtSetMediaNode.Text = ""
                txtSetMediaNode.Enabled = True
                txtSetFeedURL.Enabled = True
        End Select
    End Sub

    Private Sub btnSetSave_Click(sender As Object, e As EventArgs) Handles btnSetSave.Click
        Try
            CurrentSettings.CompanyName = txtSetName.Text
            CurrentSettings.Language = cmbSetLang.SelectedItem
            CurrentSettings.ActivityFeeds = cmbSetRSS.SelectedItem
            CurrentSettings.FeedsURL = txtSetFeedURL.Text
            CurrentSettings.MediaNode = txtSetMediaNode.Text

            CurrentSettings.GameItemSize = New Size(numGameW.Value, numGameH.Value)
            CurrentSettings.AppItemSize = New Size(numAppW.Value, numAppH.Value)
            CurrentSettings.MovieItemSize = New Size(numMovieW.Value, numMovieH.Value)
            CurrentSettings.Activity = cbActivity.Checked
            CurrentSettings.NewItems = cbNewItem.Checked

            Dim Social As New List(Of SocialIcon)
            For Each s As ListViewItem In lvSocial.Items
                Social.Add(s.Tag)
            Next
            CurrentSettings.Social = Social

            CurrentSettings.ScreenLock = cbQLSLock.Checked
            CurrentSettings.Mouse = cbQLMouse.Checked
            CurrentSettings.Notepad = cbQLNote.Checked
            CurrentSettings.Calculator = cbQLCalc.Checked
            CurrentSettings.IExplorer = cbQLIE.Checked
            CurrentSettings.Magnifier = cbQLMag.Checked
            CurrentSettings.MathInput = cbQLMath.Checked
            CurrentSettings.SnippingTool = cbQLSnip.Checked
            CurrentSettings.Wordpad = cbQLWord.Checked
            CurrentSettings.Controller = cbQLJoy.Checked
            CurrentSettings.MSEdge = cbQLEdge.Checked

            CurrentSettings.SettingsFileName = settingFile
            CurrentSettings.Save()
            MsgBox("Settings Save Completed.", MsgBoxStyle.Information, "LaunchManager")
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnSetSocial_Click(sender As Object, e As EventArgs) Handles btnSetSocial.Click
        Try
            If Not txtSetSocial.Text = Nothing Then
                If btnSetSocial.Text = "Add" Then
                    Dim item As New ListViewItem(CType(cmbSetSocial.SelectedItem, String))
                    With item
                        .SubItems.Add(txtSetSocial.Text)
                        .Tag = New SocialIcon(cmbSetSocial.SelectedItem, txtSetSocial.Text)
                    End With
                    lvSocial.Items.Add(item)
                ElseIf btnSetSocial.Text = "Edit" Then
                    sSI.Text = cmbSetSocial.SelectedItem
                    sSI.SubItems(1).Text = txtSetSocial.Text
                    sSI.Tag = New SocialIcon(cmbSetSocial.SelectedItem, txtSetSocial.Text)
                    btnSetSocial.Text = "Add" : btnSetSocial.Image = My.Resources.add
                End If
            End If
            txtSetSocial.Clear()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lvSocial_DoubleClick(sender As Object, e As EventArgs) Handles lvSocial.DoubleClick
        tsmiSetSocialEdit.PerformClick()
    End Sub

    Private Sub tsmiSetSocialEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetSocialEdit.Click
        Try
            If Not lvSocial.SelectedItems.Count = 0 Then
                sSI = lvSocial.SelectedItems.Item(0)
                txtSetSocial.Text = sSI.SubItems(1).Text
                cmbSetSocial.Text = sSI.Text
                btnSetSocial.Text = "Edit" : btnSetSocial.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetSocialDel_Click(sender As Object, e As EventArgs) Handles tsmiSetSocialDel.Click
        Try
            If Not lvSocial.SelectedItems.Count = 0 Then
                sSI = lvSocial.SelectedItems.Item(0)
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {sSI.Text}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lvSocial.Items.Remove(sSI)
                End If
                sSI = Nothing
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetSocialRef_Click(sender As Object, e As EventArgs) Handles tsmiSetSocialRef.Click
        lvSocial.Items.Clear()

        For Each si As SocialIcon In CurrentSettings.Social
            Dim item As New ListViewItem(si.Logo)
            With item
                .SubItems.Add(si.URL)
                .Tag = si
            End With
            lvSocial.Items.Add(item)
        Next
    End Sub

#End Region
#Region "Game Settings"

    Private Sub LoadSettingsGame()
        Try
            For Each g In CurrentSettings.GameGernes
                lbSetGerne.Items.Add(g.Name)
            Next
            For Each p In CurrentSettings.GamePublishers
                lbSetPub.Items.Add(p.Name)
            Next
            For Each d In CurrentSettings.GameDevelopers
                lbSetDev.Items.Add(d.Name)
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetGerneEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetGerneEdit.Click
        Try
            If Not lbSetGerne.SelectedItem = Nothing Then
                txtSetGerne.Text = lbSetGerne.SelectedItem
                btnSetGerne.Text = "Edit" : btnSetGerne.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetGerneDel_Click(sender As Object, e As EventArgs) Handles tsmiSetGerneDel.Click
        Try
            If Not lbSetGerne.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetGerne.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetGerne.Items.Remove(lbSetGerne.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetGerneRef_Click(sender As Object, e As EventArgs) Handles tsmiSetGerneRef.Click
        Try
            lbSetGerne.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each g In CurrentSettings.GameGernes
                                        lbSetGerne.Items.Add(g.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetGerne_DoubleClick(sender As Object, e As EventArgs) Handles lbSetGerne.DoubleClick
        tsmiSetGerneEdit.PerformClick()
    End Sub

    Private Sub btnSetGerne_Click(sender As Object, e As EventArgs) Handles btnSetGerne.Click
        Try
            If Not txtSetGerne.Text = Nothing Then
                If btnSetGerne.Text = "Add" Then
                    If txtSetGerne.Text.Contains(vbNewLine) Then
                        lbSetGerne.Items.AddRange(txtSetGerne.Text.Split(vbNewLine))
                    Else
                        lbSetGerne.Items.Add(txtSetGerne.Text)
                    End If
                ElseIf btnSetGerne.Text = "Edit" Then
                    If lbSetGerne.SelectedIndex > -1 Then lbSetGerne.Items(lbSetGerne.SelectedIndex) = txtSetGerne.Text
                    btnSetGerne.Text = "Add" : btnSetGerne.Image = My.Resources.add
                End If
            End If
            txtSetGerne.Clear()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetPubEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetPubEdit.Click
        Try
            If Not lbSetPub.SelectedItem = Nothing Then
                txtSetPub.Text = lbSetPub.SelectedItem
                btnSetPub.Text = "Edit" : btnSetPub.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetPubDel_Click(sender As Object, e As EventArgs) Handles tsmiSetPubDel.Click
        Try
            If Not lbSetPub.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetPub.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetPub.Items.Remove(lbSetPub.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetPubRef_Click(sender As Object, e As EventArgs) Handles tsmiSetPubRef.Click
        Try
            lbSetPub.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each p In CurrentSettings.GamePublishers
                                        lbSetPub.Items.Add(p.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetPub_DoubleClick(sender As Object, e As EventArgs) Handles lbSetPub.DoubleClick
        tsmiSetPubEdit.PerformClick()
    End Sub

    Private Sub btnSetPub_Click(sender As Object, e As EventArgs) Handles btnSetPub.Click
        Try
            If Not txtSetPub.Text = Nothing Then
                If btnSetPub.Text = "Add" Then
                    If txtSetPub.Text.Contains(vbNewLine) Then
                        lbSetPub.Items.AddRange(txtSetPub.Text.Split(vbNewLine))
                    Else
                        lbSetPub.Items.Add(txtSetPub.Text)
                    End If
                ElseIf btnSetPub.Text = "Edit" Then
                    If lbSetPub.SelectedIndex > -1 Then lbSetPub.Items(lbSetPub.SelectedIndex) = txtSetPub.Text
                    btnSetPub.Text = "Add" : btnSetPub.Image = My.Resources.add
                End If
                txtSetPub.Clear()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnSetGameSave_Click(sender As Object, e As EventArgs) Handles btnSetGameSave.Click
        Try
            Dim Gerne As New List(Of Gerne)
            For Each g As String In lbSetGerne.Items
                Gerne.Add(New Gerne(g.Trim))
            Next
            CurrentSettings.GameGernes = Gerne
            Dim Publisher As New List(Of Publisher)
            For Each p As String In lbSetPub.Items
                Publisher.Add(New Publisher(p.Trim))
            Next
            CurrentSettings.GamePublishers = Publisher
            Dim Developer As New List(Of Developer)
            For Each d As String In lbSetDev.Items
                Developer.Add(New Developer(d.Trim))
            Next
            CurrentSettings.GameDevelopers = Developer
            CurrentSettings.SettingsFileName = settingFile
            CurrentSettings.Save()

            MsgBox("Game Settings Save Completed, Game Manager needs to be restart to take effect.", MsgBoxStyle.Information, "LaunchManager")
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetDevEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetDevEdit.Click
        Try
            If Not lbSetDev.SelectedItem = Nothing Then
                txtSetDev.Text = lbSetDev.SelectedItem
                btnSetDev.Text = "Edit" : btnSetDev.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetDevDel_Click(sender As Object, e As EventArgs) Handles tsmiSetDevDel.Click
        Try
            If Not lbSetDev.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetDev.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetDev.Items.Remove(lbSetDev.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetDevRef_Click(sender As Object, e As EventArgs) Handles tsmiSetDevRef.Click
        Try
            lbSetDev.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each g In CurrentSettings.GameDevelopers
                                        lbSetDev.Items.Add(g.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetDev_DoubleClick(sender As Object, e As EventArgs) Handles lbSetDev.DoubleClick
        tsmiSetDevEdit.PerformClick()
    End Sub

    Private Sub btnSetDev_Click(sender As Object, e As EventArgs) Handles btnSetDev.Click
        Try
            If Not txtSetDev.Text = Nothing Then
                If btnSetDev.Text = "Add" Then
                    If txtSetDev.Text.Contains(vbNewLine) Then
                        lbSetDev.Items.AddRange(txtSetDev.Text.Split(vbNewLine))
                    Else
                        lbSetDev.Items.Add(txtSetDev.Text)
                    End If
                ElseIf btnSetDev.Text = "Edit" Then
                    If lbSetDev.SelectedIndex > -1 Then lbSetDev.Items(lbSetDev.SelectedIndex) = txtSetDev.Text
                    btnSetDev.Text = "Add" : btnSetDev.Image = My.Resources.add
                End If
            End If
            txtSetDev.Clear()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region
#Region "App Settings"

    Private Sub LoadSettingsApp()
        Try
            For Each d In CurrentSettings.AppDevelopers
                lbSetAppDev.Items.Add(d.Name)
            Next
            For Each t In CurrentSettings.AppTypes
                lbSetAppType.Items.Add(t.Name)
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetAppDevEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetAppDevEdit.Click
        Try
            If Not lbSetAppDev.SelectedItem = Nothing Then
                txtSetAppDev.Text = lbSetAppDev.SelectedItem
                btnSetAppDev.Text = "Edit" : btnSetAppDev.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetAppDevDel_Click(sender As Object, e As EventArgs) Handles tsmiSetAppDevDel.Click
        Try
            If Not lbSetAppDev.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetAppDev.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetAppDev.Items.Remove(lbSetAppDev.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetAppDevRef_Click(sender As Object, e As EventArgs) Handles tsmiSetAppDevRef.Click
        Try
            lbSetAppDev.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each g In CurrentSettings.AppDevelopers
                                        lbSetAppDev.Items.Add(g.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetAppDev_DoubleClick(sender As Object, e As EventArgs) Handles lbSetAppDev.DoubleClick
        tsmiSetAppDevEdit.PerformClick()
    End Sub

    Private Sub btnSetAppDev_Click(sender As Object, e As EventArgs) Handles btnSetAppDev.Click
        Try
            If Not txtSetAppDev.Text = Nothing Then
                If btnSetAppDev.Text = "Add" Then
                    If txtSetAppDev.Text.Contains(vbNewLine) Then
                        lbSetAppDev.Items.AddRange(txtSetAppDev.Text.Split(vbNewLine))
                    Else
                        lbSetAppDev.Items.Add(txtSetAppDev.Text)
                    End If
                ElseIf btnSetAppDev.Text = "Edit" Then
                    If lbSetAppDev.SelectedIndex > -1 Then lbSetAppDev.Items(lbSetAppDev.SelectedIndex) = txtSetAppDev.Text
                    btnSetAppDev.Text = "Add" : btnSetAppDev.Image = My.Resources.add
                End If
            End If
            txtSetAppDev.Clear()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetAppTypeEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetAppTypeEdit.Click
        Try
            If Not lbSetAppType.SelectedItem = Nothing Then
                txtSetAppType.Text = lbSetAppType.SelectedItem
                btnSetAppType.Text = "Edit" : btnSetAppType.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetAppTypeDel_Click(sender As Object, e As EventArgs) Handles tsmiSetAppTypeDel.Click
        Try
            If Not lbSetAppType.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetAppType.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetAppType.Items.Remove(lbSetAppType.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetAppTypeRef_Click(sender As Object, e As EventArgs) Handles tsmiSetAppTypeRef.Click
        Try
            lbSetAppType.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each g In CurrentSettings.AppTypes
                                        lbSetAppType.Items.Add(g.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetAppType_DoubleClick(sender As Object, e As EventArgs) Handles lbSetAppType.DoubleClick
        tsmiSetAppTypeEdit.PerformClick()
    End Sub

    Private Sub btnSetAppType_Click(sender As Object, e As EventArgs) Handles btnSetAppType.Click
        Try
            If Not txtSetAppType.Text = Nothing Then
                If btnSetAppType.Text = "Add" Then
                    If txtSetAppType.Text.Contains(vbNewLine) Then
                        lbSetAppType.Items.AddRange(txtSetAppType.Text.Split(vbNewLine))
                    Else
                        lbSetAppType.Items.Add(txtSetAppType.Text)
                    End If
                ElseIf btnSetAppType.Text = "Edit" Then
                    If lbSetAppType.SelectedIndex > -1 Then lbSetAppType.Items(lbSetAppType.SelectedIndex) = txtSetAppType.Text
                    btnSetAppType.Text = "Add" : btnSetAppType.Image = My.Resources.add
                End If
            End If
            txtSetAppType.Clear()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnSetAppSave_Click(sender As Object, e As EventArgs) Handles btnSetAppSave.Click
        Try
            Dim Developer As New List(Of Developer)
            For Each d As String In lbSetAppDev.Items
                Developer.Add(New Developer(d.Trim))
            Next
            CurrentSettings.AppDevelopers = Developer
            Dim AppType As New List(Of AppType)
            For Each t As String In lbSetAppType.Items
                AppType.Add(New AppType(t.Trim))
            Next
            CurrentSettings.AppTypes = AppType
            CurrentSettings.SettingsFileName = settingFile
            CurrentSettings.Save()
            MsgBox("Application Settings Save Completed, Game Manager needs to be restart to take effect.", MsgBoxStyle.Information, "LaunchManager")
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region
#Region "Movie Settings"

    Private Sub LoadSettingsMovie()
        Try
            For Each g In CurrentSettings.MovieGernes
                lbSetMovieGerne.Items.Add(g.Name)
            Next
            For Each p In CurrentSettings.MovieProductions
                lbSetMoviePro.Items.Add(p.Name)
            Next
            For Each d In CurrentSettings.MovieDistributors
                lbSetMovieDis.Items.Add(d.Name)
            Next
            For Each s In CurrentSettings.MovieStars
                lbSetMovieStar.Items.Add(s.Name)
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieGerneEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieGerneEdit.Click
        Try
            If Not lbSetMovieGerne.SelectedItem = Nothing Then
                txtSetMovieGerne.Text = lbSetMovieGerne.SelectedItem
                btnSetMovieGerne.Text = "Edit" : btnSetMovieGerne.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieGerneDel_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieGerneDel.Click
        Try
            If Not lbSetMovieGerne.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetMovieGerne.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetMovieGerne.Items.Remove(lbSetMovieGerne.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieGerneRef_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieGerneRef.Click
        Try
            lbSetMovieGerne.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each g In CurrentSettings.MovieGernes
                                        lbSetMovieGerne.Items.Add(g.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetMovieGerne_DoubleClick(sender As Object, e As EventArgs) Handles lbSetMovieGerne.DoubleClick
        tsmiSetMovieGerneEdit.PerformClick()
    End Sub

    Private Sub btnSetMovieGerne_Click(sender As Object, e As EventArgs) Handles btnSetMovieGerne.Click
        Try
            If Not txtSetMovieGerne.Text = Nothing Then
                If btnSetMovieGerne.Text = "Add" Then
                    If txtSetMovieGerne.Text.Contains(vbNewLine) Then
                        lbSetMovieGerne.Items.AddRange(txtSetMovieGerne.Text.Split(vbNewLine))
                    Else
                        lbSetMovieGerne.Items.Add(txtSetMovieGerne.Text)
                    End If
                ElseIf btnSetMovieGerne.Text = "Edit" Then
                    If lbSetMovieGerne.SelectedIndex > -1 Then lbSetMovieGerne.Items(lbSetMovieGerne.SelectedIndex) = txtSetMovieGerne.Text
                    btnSetMovieGerne.Text = "Add" : btnSetMovieGerne.Image = My.Resources.add
                End If
            End If
            txtSetMovieGerne.Clear()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieProEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieProEdit.Click
        Try
            If Not lbSetMoviePro.SelectedItem = Nothing Then
                txtSetMoviePro.Text = lbSetMoviePro.SelectedItem
                btnSetMoviePro.Text = "Edit" : btnSetMoviePro.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieProDel_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieProDel.Click
        Try
            If Not lbSetMoviePro.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetMoviePro.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetMoviePro.Items.Remove(lbSetMoviePro.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieProRef_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieProRef.Click
        Try
            lbSetMoviePro.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each p In CurrentSettings.MovieProductions
                                        lbSetMoviePro.Items.Add(p.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetMoviePro_DoubleClick(sender As Object, e As EventArgs) Handles lbSetMoviePro.DoubleClick
        tsmiSetMovieProEdit.PerformClick()
    End Sub

    Private Sub btnSetMoviePro_Click(sender As Object, e As EventArgs) Handles btnSetMoviePro.Click
        Try
            If Not txtSetMoviePro.Text = Nothing Then
                If btnSetMoviePro.Text = "Add" Then
                    If txtSetMoviePro.Text.Contains(vbNewLine) Then
                        lbSetMoviePro.Items.AddRange(txtSetMoviePro.Text.Split(vbNewLine))
                    Else
                        lbSetMoviePro.Items.Add(txtSetMoviePro.Text)
                    End If
                ElseIf btnSetMoviePro.Text = "Edit" Then
                    If lbSetMoviePro.SelectedIndex > -1 Then lbSetMoviePro.Items(lbSetMoviePro.SelectedIndex) = txtSetMoviePro.Text
                    btnSetMoviePro.Text = "Add" : btnSetMoviePro.Image = My.Resources.add
                End If
                txtSetMoviePro.Clear()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnSetMovieSave_Click(sender As Object, e As EventArgs) Handles btnSetMovieSave.Click
        Try
            Dim Gerne As New List(Of Gerne)
            For Each g As String In lbSetMovieGerne.Items
                Gerne.Add(New Gerne(g.Trim))
            Next
            CurrentSettings.MovieGernes = Gerne
            Dim Production As New List(Of Production)
            For Each p As String In lbSetMoviePro.Items
                Production.Add(New Production(p.Trim))
            Next
            CurrentSettings.MovieProductions = Production
            Dim Distributor As New List(Of Distributor)
            For Each d As String In lbSetMovieDis.Items
                Distributor.Add(New Distributor(d.Trim))
            Next
            CurrentSettings.MovieDistributors = Distributor
            Dim Star As New List(Of Actor)
            For Each s As String In lbSetMovieStar.Items
                s = s.Replace(",", "")
                Star.Add(New Actor(s.Trim))
            Next
            CurrentSettings.MovieStars = Star
            CurrentSettings.SettingsFileName = settingFile
            CurrentSettings.Save()
            MsgBox("Movie Settings Save Completed, Game Manager needs to be restart to take effect.", MsgBoxStyle.Information, "Movie Manager 2020")
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieDisEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieDisEdit.Click
        Try
            If Not lbSetMovieDis.SelectedItem = Nothing Then
                txtSetMovieDis.Text = lbSetMovieDis.SelectedItem
                btnSetMovieDis.Text = "Edit" : btnSetMovieDis.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieDisDel_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieDisDel.Click
        Try
            If Not lbSetMovieDis.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetMovieDis.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetMovieDis.Items.Remove(lbSetMovieDis.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieDisRef_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieDisRef.Click
        Try
            lbSetMovieDis.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each g In CurrentSettings.MovieDistributors
                                        lbSetMovieDis.Items.Add(g.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetMovieDis_DoubleClick(sender As Object, e As EventArgs) Handles lbSetMovieDis.DoubleClick
        tsmiSetMovieDisEdit.PerformClick()
    End Sub

    Private Sub btnSetMovieDis_Click(sender As Object, e As EventArgs) Handles btnSetMovieDis.Click
        Try
            If Not txtSetMovieDis.Text = Nothing Then
                If btnSetMovieDis.Text = "Add" Then
                    If txtSetMovieDis.Text.Contains(vbNewLine) Then
                        lbSetMovieDis.Items.AddRange(txtSetMovieDis.Text.Split(vbNewLine))
                    Else
                        lbSetMovieDis.Items.Add(txtSetMovieDis.Text)
                    End If
                ElseIf btnSetMovieDis.Text = "Edit" Then
                    If lbSetMovieDis.SelectedIndex > -1 Then lbSetMovieDis.Items(lbSetMovieDis.SelectedIndex) = txtSetMovieDis.Text
                    btnSetMovieDis.Text = "Add" : btnSetMovieDis.Image = My.Resources.add
                End If
            End If
            txtSetMovieDis.Clear()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieStarEdit_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieStarEdit.Click
        Try
            If Not lbSetMovieStar.SelectedItem = Nothing Then
                txtSetMovieStar.Text = lbSetMovieStar.SelectedItem
                btnSetMovieStar.Text = "Edit" : btnSetMovieStar.Image = My.Resources.pencil
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieStarDel_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieStarDel.Click
        Try
            If Not lbSetMovieStar.SelectedItem = Nothing Then
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {lbSetMovieStar.SelectedItem}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lbSetMovieStar.Items.Remove(lbSetMovieStar.SelectedItem)
                End If
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiSetMovieStarRef_Click(sender As Object, e As EventArgs) Handles tsmiSetMovieStarRef.Click
        Try
            lbSetMovieStar.Items.Clear()
            Dim t As New Thread(Sub()
                                    For Each g In CurrentSettings.MovieStars
                                        lbSetMovieStar.Items.Add(g.Name)
                                    Next
                                End Sub) : t.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lbSetMovieStar_DoubleClick(sender As Object, e As EventArgs) Handles lbSetMovieStar.DoubleClick
        tsmiSetMovieStarEdit.PerformClick()
    End Sub

    Private Sub btnSetMovieStar_Click(sender As Object, e As EventArgs) Handles btnSetMovieStar.Click
        Try
            If Not txtSetMovieStar.Text = Nothing Then
                If btnSetMovieStar.Text = "Add" Then
                    If txtSetMovieStar.Text.Contains(vbNewLine) Then
                        lbSetMovieStar.Items.AddRange(txtSetMovieStar.Text.Split(vbNewLine))
                    Else
                        lbSetMovieStar.Items.Add(txtSetMovieStar.Text)
                    End If
                ElseIf btnSetMovieStar.Text = "Edit" Then
                    If lbSetMovieStar.SelectedIndex > -1 Then lbSetMovieStar.Items(lbSetMovieStar.SelectedIndex) = txtSetMovieStar.Text
                    btnSetMovieStar.Text = "Add" : btnSetMovieStar.Image = My.Resources.add
                End If
            End If
            txtSetMovieStar.Clear()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

#End Region
#End Region

#Region "Game"

    Private _GameList As New List(Of GameList)
    Private _GameImageBase64 As String = Nothing
    Private _GameInstallDate As String = Nothing
    Private gSI As ListViewItem
    Private gameClipboard As ListViewItem

    Private Sub PopulateGame()
        Try
            _GameList.Clear()
            cmbGameCategory.Items.Clear()
            cmbGameGerne.Items.Clear()
            cmbGamePub.Items.Clear()
            cmbGameDev.Items.Clear()

            If Directory.Exists(xmlPath) Then
                For Each cat In Directory.GetFiles(xmlPath, "*.xml")
                    Dim t As New GameList(cat)
                    t.ReadFromFile()
                    Dim gl As GameList = t.Instance
                    gl.FileName = cat
                    _GameList.Add(gl)
                Next
            End If

            For Each gli As GameList In _GameList
                If gli.Type = CategoryType.Game Then cmbGameCategory.Items.Add(gli.Category)
            Next

            For Each g In CurrentSettings.GameGernes
                cmbGameGerne.Items.Add(g.Name)
                cmbGameGerne.AutoCompleteCustomSource.Add(g.Name)
            Next

            For Each p In CurrentSettings.GamePublishers
                cmbGamePub.Items.Add(p.Name)
                cmbGamePub.AutoCompleteCustomSource.Add(p.Name)
            Next

            For Each d In CurrentSettings.GameDevelopers
                cmbGameDev.Items.Add(d.Name)
                cmbGameDev.AutoCompleteCustomSource.Add(d.Name)
            Next

            If cmbGameCategory.Items.Count > 0 Then cmbGameCategory.SelectedIndex = 0
            If cmbGameRating.Items.Count > 0 Then cmbGameRating.SelectedIndex = 0
            cmbGameDev.Text = Nothing
            cmbGameGerne.Text = Nothing
            cmbGamePub.Text = Nothing

            pbGamePreview2.Hide()
            lblGameBtmDesc.Hide()
            lblGameBtmWDir.Hide()
            lblGameBtmWeb.Hide()
            Label26.Hide()
            Label27.Hide()
            Label29.Hide()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnGameCatRef_Click(sender As Object, e As EventArgs) Handles btnGameCatRef.Click
        Dim gt As New Thread(AddressOf PopulateGame) : gt.Start()
    End Sub

    Private Sub cmbGameCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbGameCategory.SelectedIndexChanged
        RefreshGames()
    End Sub

    Private Sub RefreshGames()
        Try
            Dim gl As GameList = _GameList.Find(Function(x) x.Category = cmbGameCategory.SelectedItem)

            lvGame.Items.Clear()

            For Each game As Game In gl.Games
                Dim item As New ListViewItem(game.Name)
                With item
                    .SubItems.Add(game.Path)
                    .SubItems.Add(game.InstallDate)
                    .SubItems.Add(game.Gerne)
                    .SubItems.Add(game.Developer)
                    .SubItems.Add(game.Publisher)
                    .SubItems.Add(game.Rating)
                    .Tag = game
                End With
                lvGame.Items.Add(item)
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnGameBrowse_Click(sender As Object, e As EventArgs) Handles btnGameBrowse.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Game..."
            ofd.RestoreDirectory = True
            ofd.InitialDirectory = gameInitial
            ofd.Filter = "All Supported Type|*.exe;*.bat;*.lnk;*.url|EXE File (.exe)|*.exe|Batch File (.bat)|*.bat|Shortcut File (.lnk)|*.lnk|URL File (.url)|*.url|Any File|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                txtGamePath.Text = ofd.FileName
                gameInitial = Path.GetDirectoryName(ofd.FileName)
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnGameStartinBrowse_Click(sender As Object, e As EventArgs) Handles btnGameStartinBrowse.Click
        Try
            Dim fsd = New FolderSelectDialog()
            fsd.Title = "Select Game Directory..."
            fsd.InitialDirectory = txtGamePath.Text

            If fsd.ShowDialog(IntPtr.Zero) Then
                txtGameStartin.Text = fsd.FileName
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnGameAdd_Click(sender As Object, e As EventArgs) Handles btnGameAdd.Click
        Try
            If txtGameName.Text = Nothing Then
                MsgBox("Please enter game name.", MsgBoxStyle.Critical, "Error")
            ElseIf txtGamePath.Text = Nothing Then
                MsgBox("Please enter game path.", MsgBoxStyle.Critical, "Error")
            ElseIf Not File.Exists(txtGamePath.Text) Then
                MsgBox("Selected file is not exist.", MsgBoxStyle.Critical, "Error")
            Else
                'If txtGameStartin.Text = Nothing Then txtGameStartin.Text = Path.GetDirectoryName(txtGamePath.Text)
                If txtGameWeb.Text = Nothing Then
                    Dim q As String = txtGameName.Text.Replace("+", "%2B").Replace(" ", "+").ToLower
                    txtGameWeb.Text = "https://www.google.com/search?q=" & q
                End If
                If _GameImageBase64 = Nothing Then
                    Dim imageGen As Image = GenerateImage(txtGameName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                    _GameImageBase64 = pbGamePreview.Image.ImageToBase64
                End If
                _GameInstallDate = Now.ToShortDateString

                Dim item As New ListViewItem(txtGameName.Text)
                With item
                    .SubItems.Add(txtGamePath.Text)
                    .SubItems.Add(_GameInstallDate)
                    .SubItems.Add(cmbGameGerne.Text)
                    .SubItems.Add(cmbGameDev.Text)
                    .SubItems.Add(cmbGamePub.Text)
                    .SubItems.Add(cmbGameRating.SelectedItem)
                    .Tag = New Game(txtGameName.Text, txtGamePath.Text, txtGameStartin.Text, _GameInstallDate, txtGameWeb.Text, txtGameDescription.Text,
                                    cmbGameGerne.Text, cmbGamePub.Text, cmbGameDev.Text, cmbGameRating.SelectedItem, _GameImageBase64)
                End With
                lvGame.Items.Add(item)

                txtGameName.Clear()
                txtGamePath.Clear()
                txtGameStartin.Clear()
                txtGameWeb.Clear()
                txtGameDescription.Clear()
                _GameImageBase64 = Nothing
                cmbGameRating.SelectedIndex = 0
                cmbGameDev.Text = Nothing
                cmbGameGerne.Text = Nothing
                cmbGamePub.Text = Nothing
                pbGamePreview.Image = My.Resources.NO_IMAGE
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnGameCancel_Click(sender As Object, e As EventArgs) Handles btnGameCancel.Click
        txtGameName.Clear()
        txtGamePath.Clear()
        txtGameStartin.Clear()
        txtGameWeb.Clear()
        txtGameDescription.Clear()
        _GameImageBase64 = Nothing
        cmbGameGerne.Text = Nothing
        cmbGamePub.Text = Nothing
        If cmbGameRating.Items.Count > 0 Then cmbGameRating.SelectedIndex = 0
        cmbGameDev.Text = Nothing
        pbGamePreview.Image = My.Resources.NO_IMAGE
        btnGameAdd.Enabled = True
        btnGameEdit.Enabled = False
    End Sub

    Private Sub sbtnGameBrowseImg_Click(sender As Object, e As EventArgs) Handles sbtnGameBrowseImg.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Cover Art..."
            ofd.InitialDirectory = gameImageZip
            ofd.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png|All files (*.*)|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                Dim img As Image = SafeImageFromFile(ofd.FileName)
                pbGamePreview.Image = img
                _GameImageBase64 = img.ImageToBase64()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiGameBrowseIcon_Click(sender As Object, e As EventArgs) Handles tsmiGameBrowseIcon.Click
        Try
            Dim ipd = New IconPickerDialog
            ipd.FileName = txtGamePath.Text
            If ipd.ShowDialog(Me) <> DialogResult.Cancel Then
                Dim fileName = ipd.FileName
                Dim index = ipd.IconIndex
                Dim icon As Icon = Nothing
                Dim splitIcons As Icon() = Nothing

                Try
                    If Path.GetExtension(ipd.FileName).ToLower() = ".ico" Then
                        icon = New Icon(ipd.FileName)
                    Else
                        Dim extractor = New IconExtractor(fileName)
                        icon = extractor.GetIcon(index)
                    End If

                    splitIcons = IconUtil.Split(icon)
                Catch ex As Exception
                    Logger.Log($"{ex.Message} {ex.StackTrace}")
                    MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
                    Return
                End Try

                Dim iSize As Integer = 0
                Dim iSizeS As New Size(0, 0)
                Dim ikon As Icon = Nothing

                For Each i In splitIcons
                    Dim sizeI As Integer = i.Size.Width + i.Size.Height
                    If sizeI > iSize Then
                        iSizeS = i.Size
                        ikon = i
                    End If
                Next

                Dim img As Image = DirectCast(IconUtil.ToBitmap(ikon), Image)
                pbGamePreview.Image = img
                _GameImageBase64 = img.ImageToBase64()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiGameGenImg_Click(sender As Object, e As EventArgs) Handles tsmiGameGenImg.Click
        Try
            Dim imageGen As Image = GenerateImage(txtGameName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
            pbGamePreview.Image = imageGen
            _GameImageBase64 = pbGamePreview.Image.ImageToBase64
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnGameEdit_Click(sender As Object, e As EventArgs) Handles btnGameEdit.Click
        Try
            If txtGameName.Text = Nothing Then
                MsgBox("Please enter game name.", MsgBoxStyle.Critical, "Error")
            ElseIf txtGamePath.Text = Nothing Then
                MsgBox("Please enter game path.", MsgBoxStyle.Critical, "Error")
            ElseIf Not File.Exists(txtGamePath.Text) Then
                MsgBox("Selected file is not exist.", MsgBoxStyle.Critical, "Error")
            Else
                'If txtGameStartin.Text = Nothing Then txtGameStartin.Text = Path.GetDirectoryName(txtGamePath.Text)
                If txtGameWeb.Text = Nothing Then
                    Dim q As String = txtGameName.Text.Replace("+", "%2B").Replace(" ", "+").ToLower
                    txtGameWeb.Text = "https://www.google.com/search?q=" & q
                End If
                If _GameImageBase64 = Nothing Then
                    Dim imageGen As Image = GenerateImage(txtGameName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                    _GameImageBase64 = pbGamePreview.Image.ImageToBase64
                End If
                _GameInstallDate = Now.ToShortDateString

                gSI.Text = txtGameName.Text
                gSI.SubItems(1).Text = txtGamePath.Text
                gSI.SubItems(2).Text = _GameInstallDate
                gSI.SubItems(3).Text = cmbGameGerne.Text
                gSI.SubItems(4).Text = cmbGameDev.Text
                gSI.SubItems(5).Text = cmbGamePub.Text
                gSI.SubItems(6).Text = cmbGameRating.SelectedItem
                gSI.Tag = New Game(txtGameName.Text, txtGamePath.Text, txtGameStartin.Text, _GameInstallDate, txtGameWeb.Text, txtGameDescription.Text,
                                    cmbGameGerne.Text, cmbGamePub.Text, cmbGameDev.Text, cmbGameRating.SelectedItem, _GameImageBase64)

                txtGameName.Clear()
                txtGamePath.Clear()
                txtGameStartin.Clear()
                txtGameWeb.Clear()
                txtGameDescription.Clear()
                _GameImageBase64 = Nothing
                If cmbGameRating.Items.Count > 0 Then cmbGameRating.SelectedIndex = 0
                cmbGameDev.Text = Nothing
                cmbGameGerne.Text = Nothing
                cmbGamePub.Text = Nothing
                pbGamePreview.Image = My.Resources.NO_IMAGE
                btnGameEdit.Enabled = False
                btnGameAdd.Enabled = True
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiGameEdit_Click(sender As Object, e As EventArgs) Handles tsmiGameEdit.Click
        Try
            If Not lvGame.SelectedItems.Count = 0 Then
                gSI = lvGame.SelectedItems.Item(0)

                Dim game As Game = DirectCast(gSI.Tag, Game)
                txtGameName.Text = game.Name
                txtGamePath.Text = game.Path
                txtGameStartin.Text = game.StartIn
                txtGameWeb.Text = game.Website
                txtGameDescription.Text = game.Description
                cmbGameDev.Text = game.Developer
                cmbGameGerne.Text = game.Gerne
                cmbGamePub.Text = game.Publisher
                cmbGameRating.SelectedItem = game.Rating
                _GameImageBase64 = game.Image
                _GameInstallDate = game.InstallDate
                pbGamePreview.Image = _GameImageBase64.Base64ToImage

                btnGameAdd.Enabled = False
                btnGameEdit.Enabled = True
                btnGameSave.Enabled = True
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lvGame_DoubleClick(sender As Object, e As EventArgs) Handles lvGame.DoubleClick
        tsmiGameEdit.PerformClick()
    End Sub

    Private Sub tsmiGameDel_Click(sender As Object, e As EventArgs) Handles tsmiGameDel.Click
        Try
            If Not lvGame.SelectedItems.Count = 0 Then
                gSI = lvGame.SelectedItems.Item(0)
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {gSI.Text}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lvGame.Items.Remove(gSI)
                End If
                gSI = Nothing
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiGameRef_Click(sender As Object, e As EventArgs) Handles tsmiGameRef.Click
        RefreshGames()
    End Sub

    Private Sub btnGameSave_Click(sender As Object, e As EventArgs) Handles btnGameSave.Click
        Try
            Dim tempCat As Integer = cmbGameCategory.SelectedIndex
            Dim temp As GameList = _GameList.Find(Function(x) x.Category = cmbGameCategory.SelectedItem)

            Dim gl = temp.Instance
            gl.Games.Clear()

            For Each item As ListViewItem In lvGame.Items
                gl.Games.Add(DirectCast(item.Tag, Game))
            Next

            gl.FileName = temp.FileName
            gl.Save()
            MsgBox("Game Save Completed.", MsgBoxStyle.Information, "LaunchManager")

            Dim gt As New Thread(AddressOf PopulateGame) : gt.Start()

            cmbGameCategory.SelectedIndex = tempCat
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lblGameDragDrop_DragEnter(sender As Object, e As DragEventArgs) Handles lblGameDragDrop.DragEnter
        Try
            Dim formats As String() = e.Data.GetFormats
            e.Effect = DragDropEffects.All

            For Each file As String In e.Data.GetData(DataFormats.FileDrop)
                If Path.GetExtension(file) = ".lnk" Then
                    If IsFileOwner(file) Then
                        Dim lnk As New Shortcut(file)
                        Dim q As String = lnk.Name.Replace("+", "%2B").Replace(" ", "+").ToLower
                        Dim fweb As String = "https://www.google.com/search?q=" & q

                        Dim item As New ListViewItem(lnk.Name)
                        With item
                            .SubItems.Add(lnk.Target)
                            .SubItems.Add(Now.ToShortDateString)
                            .SubItems.Add("")
                            .SubItems.Add("")
                            .SubItems.Add("")
                            .SubItems.Add(cmbGameRating.Items.Item(0))
                            Dim image As Image = Nothing
                            If lnk.IconLocation.Length >= 3 Then
                                Try
                                    Dim iconLocation As String = lnk.IconLocation.Substring(0, lnk.IconLocation.IndexOf(","))
                                    image = Bitmap.FromHicon(New Icon(iconLocation, New Size(256, 256)).Handle)
                                Catch ex As Exception
                                    image = GetFileIconAsImage(lnk.Target)
                                    If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                    If image Is Nothing Then image = GenerateImage(lnk.Name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                                End Try
                            Else
                                image = GetFileIconAsImage(lnk.Target)
                                If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                If image Is Nothing Then image = GenerateImage(lnk.Name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                            End If
                            .Tag = New Game(lnk.Name, lnk.Target, lnk.WorkingDirectory, Now.ToShortDateString, fweb, lnk.Description,
                                            cmbGameGerne.Items.Item(0), cmbGamePub.Items.Item(0), cmbGameDev.Items.Item(0), cmbGameRating.Items.Item(0), image.ImageToBase64)
                        End With
                        lvGame.Items.Add(item)
                    Else
                        Dim name As String = IO.Path.GetFileNameWithoutExtension(file)
                        Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                        Dim fweb As String = "https://www.google.com/search?q=" & q

                        Dim item As New ListViewItem(name)
                        With item
                            .SubItems.Add(file)
                            .SubItems.Add(Now.ToShortDateString)
                            .SubItems.Add("")
                            .SubItems.Add("")
                            .SubItems.Add("")
                            .SubItems.Add(cmbGameRating.Items.Item(0))
                            Dim image As Image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                            .Tag = New Game(name, file, Nothing, Now.ToShortDateString, fweb, Nothing,
                                            cmbGameGerne.Items.Item(0), cmbGamePub.Items.Item(0), cmbGameDev.Items.Item(0), cmbGameRating.Items.Item(0), image.ImageToBase64)
                        End With
                        lvGame.Items.Add(item)
                    End If
                ElseIf Path.GetExtension(file) = ".url" Then
                    Dim name As String = Path.GetFileNameWithoutExtension(file)
                    Dim target As String = ReadIniValue(file, "InternetShortcut", "URL")
                    Dim iconLocation As String = ReadIniValue(file, "InternetShortcut", "IconFile")
                    Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                    Dim fweb As String = "https://www.google.com/search?q=" & q

                    Dim item As New ListViewItem(name)
                    With item
                        .SubItems.Add(file)
                        .SubItems.Add(Now.ToShortDateString)
                        .SubItems.Add("")
                        .SubItems.Add("")
                        .SubItems.Add("")
                        .SubItems.Add(cmbGameRating.Items.Item(0))
                        Dim image As Image = Nothing
                        If Not iconLocation = Nothing Then
                            Try
                                image = Bitmap.FromHicon(New Icon(iconLocation, New Size(256, 256)).Handle)
                            Catch ex As Exception
                                image = GetFileIconAsImage(iconLocation)
                                If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                If image Is Nothing Then image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                            End Try
                        Else
                            image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                        End If
                        .Tag = New Game(name, target, Nothing, Now.ToShortDateString, fweb, Nothing,
                                        cmbGameGerne.Items.Item(0), cmbGamePub.Items.Item(0), cmbGameDev.Items.Item(0), cmbGameRating.Items.Item(0), image.ImageToBase64)
                    End With
                    lvGame.Items.Add(item)
                End If
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lvGame_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvGame.SelectedIndexChanged
        If Not lvGame.SelectedItems.Count = 0 Then
            Dim selecteditem As ListViewItem = lvGame.SelectedItems.Item(0)
            Dim game As Game = CType(selecteditem.Tag, Game)
            pbGamePreview2.Image = game.Image.Base64ToImage
            lblGameBtmDesc.Text = game.Description
            lblGameBtmWDir.Text = game.StartIn
            lblGameBtmWeb.Text = game.Website
            pbGamePreview2.Show()
            lblGameBtmDesc.Show()
            lblGameBtmWDir.Show()
            lblGameBtmWeb.Show()
            Label26.Show()
            Label27.Show()
            Label29.Show()
        Else
            pbGamePreview2.Hide()
            lblGameBtmDesc.Hide()
            lblGameBtmWDir.Hide()
            lblGameBtmWeb.Hide()
            Label26.Hide()
            Label27.Hide()
            Label29.Hide()
        End If
    End Sub

    Private Sub btnGameBulkAdd_Click(sender As Object, e As EventArgs) Handles btnGameBulkAdd.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Games..."
            ofd.RestoreDirectory = True
            ofd.InitialDirectory = gameInitial
            ofd.Filter = "Shortcut Files (.lnk, .url)|*.lnk;*.url|All Files|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = True

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                For Each file As String In ofd.FileNames
                    If Path.GetExtension(file) = ".lnk" Then
                        If IsFileOwner(file) Then
                            gameInitial = Path.GetDirectoryName(file)
                            Dim lnk As New Shortcut(file)
                            Dim q As String = lnk.Name.Replace("+", "%2B").Replace(" ", "+").ToLower
                            Dim fweb As String = "https://www.google.com/search?q=" & q

                            Dim item As New ListViewItem(lnk.Name)
                            With item
                                .SubItems.Add(lnk.Target)
                                .SubItems.Add(Now.ToShortDateString)
                                .SubItems.Add("")
                                .SubItems.Add("")
                                .SubItems.Add("")
                                .SubItems.Add(cmbGameRating.Items.Item(0))
                                Dim image As Image = Nothing
                                If lnk.IconLocation.Length >= 3 Then
                                    Try
                                        Dim iconLocation As String = lnk.IconLocation.Substring(0, lnk.IconLocation.IndexOf(","))
                                        image = Bitmap.FromHicon(New Icon(iconLocation, New Size(256, 256)).Handle)
                                    Catch ex As Exception
                                        image = GetFileIconAsImage(lnk.Target)
                                        If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                        If image Is Nothing Then image = GenerateImage(lnk.Name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                                    End Try
                                Else
                                    image = GetFileIconAsImage(lnk.Target)
                                    If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                    If image Is Nothing Then image = GenerateImage(lnk.Name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                                End If
                                .Tag = New Game(lnk.Name, lnk.Target, lnk.WorkingDirectory, Now.ToShortDateString, fweb, lnk.Description,
                                                cmbGameGerne.Items.Item(0), cmbGamePub.Items.Item(0), cmbGameDev.Items.Item(0), cmbGameRating.Items.Item(0), image.ImageToBase64)
                            End With
                            lvGame.Items.Add(item)
                        Else
                            Dim name As String = IO.Path.GetFileNameWithoutExtension(file)
                            Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                            Dim fweb As String = "https://www.google.com/search?q=" & q

                            Dim item As New ListViewItem(name)
                            With item
                                .SubItems.Add(file)
                                .SubItems.Add(Now.ToShortDateString)
                                .SubItems.Add("")
                                .SubItems.Add("")
                                .SubItems.Add("")
                                .SubItems.Add(cmbGameRating.Items.Item(0))
                                Dim image As Image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                                .Tag = New Game(name, file, Nothing, Now.ToShortDateString, fweb, Nothing,
                                                cmbGameGerne.Items.Item(0), cmbGamePub.Items.Item(0), cmbGameDev.Items.Item(0), cmbGameRating.Items.Item(0), image.ImageToBase64)
                            End With
                            lvGame.Items.Add(item)
                        End If
                    ElseIf Path.GetExtension(file) = ".url" Then
                        Dim name As String = Path.GetFileNameWithoutExtension(file)
                        Dim target As String = ReadIniValue(file, "InternetShortcut", "URL")
                        Dim iconLocation As String = ReadIniValue(file, "InternetShortcut", "IconFile")
                        Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                        Dim fweb As String = "https://www.google.com/search?q=" & q

                        Dim item As New ListViewItem(name)
                        With item
                            .SubItems.Add(file)
                            .SubItems.Add(Now.ToShortDateString)
                            .SubItems.Add("")
                            .SubItems.Add("")
                            .SubItems.Add("")
                            .SubItems.Add(cmbGameRating.Items.Item(0))
                            Dim image As Image = Nothing
                            If Not iconLocation = Nothing Then
                                Try
                                    image = Bitmap.FromHicon(New Icon(iconLocation, New Size(256, 256)).Handle)
                                Catch ex As Exception
                                    image = GetFileIconAsImage(iconLocation)
                                    If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                    If image Is Nothing Then image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                                End Try
                            Else
                                image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                            End If
                            .Tag = New Game(name, target, Nothing, Now.ToShortDateString, fweb, Nothing,
                                            cmbGameGerne.Items.Item(0), cmbGamePub.Items.Item(0), cmbGameDev.Items.Item(0), cmbGameRating.Items.Item(0), image.ImageToBase64)
                        End With
                        lvGame.Items.Add(item)
                    End If
                Next
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiGameCopy_Click(sender As Object, e As EventArgs) Handles tsmiGameCopy.Click
        If lvGame.SelectedItems.Count = 0 Then Exit Sub
        tsmiGamePaste.Enabled = True
        gameClipboard = lvGame.SelectedItems.Item(0)
    End Sub

    Private Sub tsmiGamePaste_Click(sender As Object, e As EventArgs) Handles tsmiGamePaste.Click
        lvGame.Items.Add(gameClipboard.Clone)
    End Sub

#End Region

#Region "Application"

    Private _AppList As New List(Of GameList)
    Private _AppImageBase64 As String = Nothing
    Private _AppInstallDate As String = Nothing
    Private aSI As ListViewItem
    Private appClipboard As ListViewItem

    Private Sub PopulateApp()
        Try
            _AppList.Clear()
            cmbAppCategory.Items.Clear()
            cmbAppType.Items.Clear()
            cmbAppDev.Items.Clear()

            If Directory.Exists(xmlPath) Then
                For Each cat In Directory.GetFiles(xmlPath, "*.xml")
                    Dim t As New GameList(cat)
                    t.ReadFromFile()
                    Dim gl As GameList = t.Instance
                    gl.FileName = cat
                    _AppList.Add(gl)
                Next
            End If

            For Each ali As GameList In _AppList
                If ali.Type = CategoryType.Application Then cmbAppCategory.Items.Add(ali.Category)
            Next

            For Each t In CurrentSettings.AppTypes
                cmbAppType.Items.Add(t.Name)
                cmbAppType.AutoCompleteCustomSource.Add(t.Name)
            Next

            For Each d In CurrentSettings.AppDevelopers
                cmbAppDev.Items.Add(d.Name)
                cmbAppDev.AutoCompleteCustomSource.Add(d.Name)
            Next

            If cmbAppCategory.Items.Count > 0 Then cmbAppCategory.SelectedIndex = 0
            cmbAppDev.Text = Nothing
            cmbAppType.Text = Nothing

            pbAppPreview2.Hide()
            lblAppBtmDesc.Hide()
            lblAppBtmWDir.Hide()
            lblAppBtmWeb.Hide()
            Label31.Hide()
            Label32.Hide()
            Label24.Hide()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnAppCatRef_Click(sender As Object, e As EventArgs) Handles btnAppCatRef.Click
        Dim at As New Thread(AddressOf PopulateApp) : at.Start()
    End Sub

    Private Sub cmbAppCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbAppCategory.SelectedIndexChanged
        RefreshApps()
    End Sub

    Private Sub RefreshApps()
        Try
            Dim al As GameList = _AppList.Find(Function(x) x.Category = cmbAppCategory.SelectedItem)

            lvApp.Items.Clear()

            For Each App As Game In al.Games
                Dim item As New ListViewItem(App.Name)
                With item
                    .SubItems.Add(App.Path)
                    .SubItems.Add(App.InstallDate)
                    .SubItems.Add(App.Gerne)
                    .SubItems.Add(App.Developer)
                    .Tag = App
                End With
                lvApp.Items.Add(item)
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnAppBrowse_Click(sender As Object, e As EventArgs) Handles btnAppBrowse.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Application..."
            ofd.RestoreDirectory = True
            ofd.InitialDirectory = appInitial
            ofd.Filter = "All Supported Type|*.exe;*.bat;*.lnk;*.url|EXE File (.exe)|*.exe|Batch File (.bat)|*.bat|Shortcut File (.lnk)|*.lnk|URL File (.url)|*.url|Any File|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                txtAppPath.Text = ofd.FileName
                appInitial = Path.GetDirectoryName(ofd.FileName)
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnAppStartinBrowse_Click(sender As Object, e As EventArgs) Handles btnAppStartinBrowse.Click
        Try
            Dim fsd = New FolderSelectDialog()
            fsd.Title = "Select Application Directory..."
            fsd.InitialDirectory = txtAppPath.Text

            If fsd.ShowDialog(IntPtr.Zero) Then
                txtAppStartin.Text = fsd.FileName
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnAppAdd_Click(sender As Object, e As EventArgs) Handles btnAppAdd.Click
        Try
            If txtAppName.Text = Nothing Then
                MsgBox("Please enter Application name.", MsgBoxStyle.Critical, "Error")
            ElseIf txtAppPath.Text = Nothing Then
                MsgBox("Please enter Application path.", MsgBoxStyle.Critical, "Error")
            ElseIf Not File.Exists(txtAppPath.Text) Then
                MsgBox("Selected file is not exist.", MsgBoxStyle.Critical, "Error")
            Else
                If txtAppWeb.Text = Nothing Then
                    Dim q As String = txtAppName.Text.Replace("+", "%2B").Replace(" ", "+").ToLower
                    txtAppWeb.Text = "https://www.google.com/search?q=" & q
                End If
                If _AppImageBase64 = Nothing Then
                    Dim imageGen As Image = GenerateImage(txtAppName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                    _AppImageBase64 = pbAppPreview.Image.ImageToBase64
                End If
                _AppInstallDate = Now.ToShortDateString

                Dim item As New ListViewItem(txtAppName.Text)
                With item
                    .SubItems.Add(txtAppPath.Text)
                    .SubItems.Add(_AppInstallDate)
                    .SubItems.Add(cmbAppType.Text)
                    .SubItems.Add(cmbAppDev.Text)
                    .Tag = New Game(txtAppName.Text, txtAppPath.Text, txtAppStartin.Text, _AppInstallDate, txtAppWeb.Text, txtAppDesc.Text,
                                    cmbAppType.Text, Nothing, cmbAppDev.Text, Nothing, _AppImageBase64)
                End With
                lvApp.Items.Add(item)

                txtAppName.Clear()
                txtAppPath.Clear()
                txtAppStartin.Clear()
                txtAppWeb.Clear()
                txtAppDesc.Clear()
                _AppImageBase64 = Nothing
                cmbAppDev.Text = Nothing
                cmbAppType.Text = Nothing
                pbAppPreview.Image = My.Resources.NO_IMAGE
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnAppCancel_Click(sender As Object, e As EventArgs) Handles btnAppCancel.Click
        txtAppName.Clear()
        txtAppPath.Clear()
        txtAppStartin.Clear()
        txtAppWeb.Clear()
        txtAppDesc.Clear()
        _AppImageBase64 = Nothing
        cmbAppType.Text = Nothing
        cmbAppDev.Text = Nothing
        pbAppPreview.Image = My.Resources.NO_IMAGE
        btnAppAdd.Enabled = True
        btnAppEdit.Enabled = False
    End Sub

    Private Sub sbtnAppBrowseImg_Click(sender As Object, e As EventArgs) Handles sbtnAppBrowseImg.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Image..."
            ofd.InitialDirectory = appImageZip
            ofd.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png|All files (*.*)|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                Dim img As Image = SafeImageFromFile(ofd.FileName)
                pbAppPreview.Image = img
                _AppImageBase64 = img.ImageToBase64()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiAppBrowseIcon_Click(sender As Object, e As EventArgs) Handles tsmiAppBrowseIcon.Click
        Try
            Dim ipd = New IconPickerDialog
            ipd.FileName = txtAppPath.Text
            If ipd.ShowDialog(Me) <> DialogResult.Cancel Then
                Dim fileName = ipd.FileName
                Dim index = ipd.IconIndex
                Dim icon As Icon = Nothing
                Dim splitIcons As Icon() = Nothing

                Try
                    If Path.GetExtension(ipd.FileName).ToLower() = ".ico" Then
                        icon = New Icon(ipd.FileName)
                    Else
                        Dim extractor = New IconExtractor(fileName)
                        icon = extractor.GetIcon(index)
                    End If

                    splitIcons = IconUtil.Split(icon)
                Catch ex As Exception
                    Logger.Log($"{ex.Message} {ex.StackTrace}")
                    MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
                    Return
                End Try

                Dim iSize As Integer = 0
                Dim iSizeS As New Size(0, 0)
                Dim ikon As Icon = Nothing

                For Each i In splitIcons
                    Dim sizeI As Integer = i.Size.Width + i.Size.Height
                    If sizeI > iSize Then
                        iSizeS = i.Size
                        ikon = i
                    End If
                Next

                Dim img As Image = DirectCast(IconUtil.ToBitmap(ikon), Image)
                pbAppPreview.Image = img
                _AppImageBase64 = img.ImageToBase64()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiAppGenImg_Click(sender As Object, e As EventArgs) Handles tsmiAppGenImg.Click
        Try
            Dim imageGen As Image = GenerateImage(txtAppName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
            pbAppPreview.Image = imageGen
            _AppImageBase64 = pbAppPreview.Image.ImageToBase64
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnAppEdit_Click(sender As Object, e As EventArgs) Handles btnAppEdit.Click
        Try
            If txtAppName.Text = Nothing Then
                MsgBox("Please enter Application name.", MsgBoxStyle.Critical, "Error")
            ElseIf txtAppPath.Text = Nothing Then
                MsgBox("Please enter Application path.", MsgBoxStyle.Critical, "Error")
            ElseIf Not File.Exists(txtAppPath.Text) Then
                MsgBox("Selected file is not exist.", MsgBoxStyle.Critical, "Error")
            Else
                If txtAppWeb.Text = Nothing Then
                    Dim q As String = txtAppName.Text.Replace("+", "%2B").Replace(" ", "+").ToLower
                    txtAppWeb.Text = "https://www.google.com/search?q=" & q
                End If
                If _AppImageBase64 = Nothing Then
                    Dim imageGen As Image = GenerateImage(txtAppName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                    _AppImageBase64 = pbAppPreview.Image.ImageToBase64
                End If
                _AppInstallDate = Now.ToShortDateString

                aSI.Text = txtAppName.Text
                aSI.SubItems(1).Text = txtAppPath.Text
                aSI.SubItems(2).Text = _AppInstallDate
                aSI.SubItems(3).Text = cmbAppType.Text
                aSI.SubItems(4).Text = cmbAppDev.Text
                aSI.Tag = New Game(txtAppName.Text, txtAppPath.Text, txtAppStartin.Text, _AppInstallDate, txtAppWeb.Text, txtAppDesc.Text,
                                    cmbAppType.Text, Nothing, cmbAppDev.Text, Nothing, _AppImageBase64)

                txtAppName.Clear()
                txtAppPath.Clear()
                txtAppStartin.Clear()
                txtAppWeb.Clear()
                txtAppDesc.Clear()
                _AppImageBase64 = Nothing
                cmbAppType.Text = Nothing
                cmbAppDev.Text = Nothing
                pbAppPreview.Image = My.Resources.NO_IMAGE
                btnAppEdit.Enabled = False
                btnAppAdd.Enabled = True
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiAppEdit_Click(sender As Object, e As EventArgs) Handles tsmiAppEdit.Click
        Try
            If Not lvApp.SelectedItems.Count = 0 Then
                aSI = lvApp.SelectedItems.Item(0)

                Dim App As Game = DirectCast(aSI.Tag, Game)
                txtAppName.Text = App.Name
                txtAppPath.Text = App.Path
                txtAppStartin.Text = App.StartIn
                txtAppWeb.Text = App.Website
                txtAppDesc.Text = App.Description
                cmbAppDev.Text = App.Developer
                cmbAppType.Text = App.Gerne
                _AppImageBase64 = App.Image
                _AppInstallDate = App.InstallDate
                pbAppPreview.Image = _AppImageBase64.Base64ToImage

                btnAppAdd.Enabled = False
                btnAppEdit.Enabled = True
                btnAppSave.Enabled = True
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lvApp_DoubleClick(sender As Object, e As EventArgs) Handles lvApp.DoubleClick
        tsmiAppEdit.PerformClick()
    End Sub

    Private Sub tsmiAppDel_Click(sender As Object, e As EventArgs) Handles tsmiAppDel.Click
        Try
            If Not lvApp.SelectedItems.Count = 0 Then
                aSI = lvApp.SelectedItems.Item(0)
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {aSI.Text}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lvApp.Items.Remove(aSI)
                End If
                aSI = Nothing
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiAppRef_Click(sender As Object, e As EventArgs) Handles tsmiAppRef.Click
        RefreshApps()
    End Sub

    Private Sub btnAppSave_Click(sender As Object, e As EventArgs) Handles btnAppSave.Click
        Try
            Dim tempCat As Integer = cmbGameCategory.SelectedIndex
            Dim temp As GameList = _AppList.Find(Function(x) x.Category = cmbAppCategory.SelectedItem)

            Dim al = temp.Instance
            al.Games.Clear()

            For Each item As ListViewItem In lvApp.Items
                al.Games.Add(DirectCast(item.Tag, Game))
            Next

            al.FileName = temp.FileName
            al.Save()
            MsgBox("Application Save Completed.", MsgBoxStyle.Information, "LaunchManager")

            Dim at As New Thread(AddressOf PopulateApp) : at.Start()

            cmbGameCategory.SelectedIndex = tempCat
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lblAppDragDrop_DragEnter(sender As Object, e As DragEventArgs) Handles lblAppDragDrop.DragEnter
        Try
            Dim formats As String() = e.Data.GetFormats
            e.Effect = DragDropEffects.All

            For Each file As String In e.Data.GetData(DataFormats.FileDrop)
                If Path.GetExtension(file) = ".lnk" Then
                    If IsFileOwner(file) Then
                        Dim lnk As New Shortcut(file)
                        Dim q As String = lnk.Name.Replace("+", "%2B").Replace(" ", "+").ToLower
                        Dim fweb As String = "https://www.google.com/search?q=" & q

                        Dim item As New ListViewItem(lnk.Name)
                        With item
                            .SubItems.Add(lnk.Target)
                            .SubItems.Add(Now.ToShortDateString)
                            .SubItems.Add("")
                            .SubItems.Add("")
                            Dim image As Image = Nothing
                            If lnk.IconLocation.Length >= 3 Then
                                Try
                                    Dim iconLocation As String = lnk.IconLocation.Substring(0, lnk.IconLocation.IndexOf(","))
                                    image = Bitmap.FromHicon(New Icon(iconLocation, New Size(256, 256)).Handle)
                                Catch ex As Exception
                                    image = GetFileIconAsImage(lnk.Target)
                                    If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                    If image Is Nothing Then image = GenerateImage(lnk.Name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                                End Try
                            Else
                                image = GetFileIconAsImage(lnk.Target)
                                If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                If image Is Nothing Then image = GenerateImage(lnk.Name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                            End If
                            .Tag = New Game(lnk.Name, lnk.Target, lnk.WorkingDirectory, Now.ToShortDateString, fweb, lnk.Description,
                                            cmbAppType.Items.Item(0), Nothing, cmbAppDev.Items.Item(0), Nothing, image.ImageToBase64)
                        End With
                        lvApp.Items.Add(item)
                    Else
                        Dim name As String = IO.Path.GetFileNameWithoutExtension(file)
                        Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                        Dim fweb As String = "https://www.google.com/search?q=" & q

                        Dim item As New ListViewItem(name)
                        With item
                            .SubItems.Add(file)
                            .SubItems.Add(Now.ToShortDateString)
                            .SubItems.Add("")
                            .SubItems.Add("")
                            Dim image As Image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                            .Tag = New Game(name, file, Nothing, Now.ToShortDateString, fweb, Nothing,
                                            cmbAppType.Items.Item(0), Nothing, cmbAppDev.Items.Item(0), Nothing, image.ImageToBase64)
                        End With
                        lvApp.Items.Add(item)
                    End If
                ElseIf Path.GetExtension(file) = ".url" Then
                    Dim name As String = Path.GetFileNameWithoutExtension(file)
                    Dim target As String = ReadIniValue(file, "InternetShortcut", "URL")
                    Dim iconLocation As String = ReadIniValue(file, "InternetShortcut", "IconFile")
                    Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                    Dim fweb As String = "https://www.google.com/search?q=" & q

                    Dim item As New ListViewItem(name)
                    With item
                        .SubItems.Add(file)
                        .SubItems.Add(Now.ToShortDateString)
                        .SubItems.Add("")
                        .SubItems.Add("")
                        Dim image As Image = Nothing
                        If Not iconLocation = Nothing Then
                            Try
                                image = Bitmap.FromHicon(New Icon(iconLocation, New Size(256, 256)).Handle)
                            Catch ex As Exception
                                image = GetFileIconAsImage(iconLocation)
                                If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                If image Is Nothing Then image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                            End Try
                        Else
                            image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                        End If
                        .Tag = New Game(name, target, Nothing, Now.ToShortDateString, fweb, Nothing,
                                        cmbAppType.Items.Item(0), Nothing, cmbAppDev.Items.Item(0), Nothing, image.ImageToBase64)
                    End With
                    lvApp.Items.Add(item)
                End If
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lvApp_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvApp.SelectedIndexChanged
        If Not lvApp.SelectedItems.Count = 0 Then
            Dim selecteditem As ListViewItem = lvApp.SelectedItems.Item(0)
            Dim App As Game = CType(selecteditem.Tag, Game)
            pbAppPreview2.Image = App.Image.Base64ToImage
            lblAppBtmDesc.Text = App.Description
            lblAppBtmWDir.Text = App.StartIn
            lblAppBtmWeb.Text = App.Website
            pbAppPreview2.Show()
            lblAppBtmDesc.Show()
            lblAppBtmWDir.Show()
            lblAppBtmWeb.Show()
            Label31.Show()
            Label32.Show()
            Label24.Show()
        Else
            pbAppPreview2.Hide()
            lblAppBtmDesc.Hide()
            lblAppBtmWDir.Hide()
            lblAppBtmWeb.Hide()
            Label31.Hide()
            Label32.Hide()
            Label24.Hide()
        End If
    End Sub

    Private Sub btnAppBulkAdd_Click(sender As Object, e As EventArgs) Handles btnAppBulkAdd.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Apps..."
            ofd.RestoreDirectory = True
            ofd.InitialDirectory = appInitial
            ofd.Filter = "Shortcut Files (.lnk, .url)|*.lnk;*.url|All Files|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = True

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                For Each file As String In ofd.FileNames
                    appInitial = Path.GetDirectoryName(file)
                    If Path.GetExtension(file) = ".lnk" Then
                        If IsFileOwner(file) Then
                            Dim lnk As New Shortcut(file)
                            Dim q As String = lnk.Name.Replace("+", "%2B").Replace(" ", "+").ToLower
                            Dim fweb As String = "https://www.google.com/search?q=" & q

                            Dim item As New ListViewItem(lnk.Name)
                            With item
                                .SubItems.Add(lnk.Target)
                                .SubItems.Add(Now.ToShortDateString)
                                .SubItems.Add("")
                                .SubItems.Add("")
                                Dim image As Image = Nothing
                                If lnk.IconLocation.Length >= 3 Then
                                    Try
                                        Dim iconLocation As String = lnk.IconLocation.Substring(0, lnk.IconLocation.IndexOf(","))
                                        image = Bitmap.FromHicon(New Icon(iconLocation, New Size(256, 256)).Handle)
                                    Catch ex As Exception
                                        image = GetFileIconAsImage(lnk.Target)
                                        If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                        If image Is Nothing Then image = GenerateImage(lnk.Name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                                    End Try
                                Else
                                    image = GetFileIconAsImage(lnk.Target)
                                    If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                    If image Is Nothing Then image = GenerateImage(lnk.Name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                                End If
                                .Tag = New Game(lnk.Name, lnk.Target, lnk.WorkingDirectory, Now.ToShortDateString, fweb, lnk.Description,
                                            cmbAppType.Items.Item(0), Nothing, cmbAppDev.Items.Item(0), Nothing, image.ImageToBase64)
                            End With
                            lvApp.Items.Add(item)
                        Else
                            Dim name As String = IO.Path.GetFileNameWithoutExtension(file)
                            Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                            Dim fweb As String = "https://www.google.com/search?q=" & q

                            Dim item As New ListViewItem(name)
                            With item
                                .SubItems.Add(file)
                                .SubItems.Add(Now.ToShortDateString)
                                .SubItems.Add("")
                                .SubItems.Add("")
                                Dim image As Image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                                .Tag = New Game(name, file, Nothing, Now.ToShortDateString, fweb, Nothing,
                                            cmbAppType.Items.Item(0), Nothing, cmbAppDev.Items.Item(0), Nothing, image.ImageToBase64)
                            End With
                            lvApp.Items.Add(item)
                        End If
                    ElseIf Path.GetExtension(file) = ".url" Then
                        Dim name As String = Path.GetFileNameWithoutExtension(file)
                        Dim target As String = ReadIniValue(file, "InternetShortcut", "URL")
                        Dim iconLocation As String = ReadIniValue(file, "InternetShortcut", "IconFile")
                        Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                        Dim fweb As String = "https://www.google.com/search?q=" & q

                        Dim item As New ListViewItem(name)
                        With item
                            .SubItems.Add(file)
                            .SubItems.Add(Now.ToShortDateString)
                            .SubItems.Add("")
                            .SubItems.Add("")
                            Dim image As Image = Nothing
                            If Not iconLocation = Nothing Then
                                Try
                                    image = Bitmap.FromHicon(New Icon(iconLocation, New Size(256, 256)).Handle)
                                Catch ex As Exception
                                    image = GetFileIconAsImage(iconLocation)
                                    If image Is Nothing Then image = Icon.ExtractAssociatedIcon(file).ToBitmap
                                    If image Is Nothing Then image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                                End Try
                            Else
                                image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(256, 256), True)
                            End If
                            .Tag = New Game(name, target, Nothing, Now.ToShortDateString, fweb, Nothing,
                                        cmbAppType.Items.Item(0), Nothing, cmbAppDev.Items.Item(0), Nothing, image.ImageToBase64)
                        End With
                        lvApp.Items.Add(item)
                    End If
                Next
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiAppCopy_Click(sender As Object, e As EventArgs) Handles tsmiAppCopy.Click
        If lvApp.SelectedItems.Count = 0 Then Exit Sub
        tsmiAppPaste.Enabled = True
        appClipboard = lvApp.SelectedItems.Item(0)
    End Sub

    Private Sub tsmiAppPaste_Click(sender As Object, e As EventArgs) Handles tsmiAppPaste.Click
        lvApp.Items.Add(appClipboard.Clone)
    End Sub

#End Region

#Region "Movie"

    Private _MovieList As New List(Of GameList)
    Private _MovieImageBase64 As String = Nothing
    Private _MovieInstallDate As String = Nothing
    Private mSI As ListViewItem
    Private movieClipboard As ListViewItem

    Private Sub PopulateMovie(Optional reloadStarring As Boolean = False)
        Try
            _MovieList.Clear()
            cmbMovieCategory.Items.Clear()
            cmbMovieGerne.Items.Clear()
            cmbMoviePro.Items.Clear()
            cmbMovieDis.Items.Clear()

            If Directory.Exists(xmlPath) Then
                For Each cat In Directory.GetFiles(xmlPath, "*.xml")
                    Dim t As New GameList(cat)
                    t.ReadFromFile()
                    Dim ml As GameList = t.Instance
                    ml.FileName = cat
                    _MovieList.Add(ml)
                Next
            End If

            For Each mli As GameList In _MovieList
                If mli.Type = CategoryType.Movie Then cmbMovieCategory.Items.Add(mli.Category)
            Next

            For Each g In CurrentSettings.MovieGernes
                cmbMovieGerne.Items.Add(g.Name)
                cmbMovieGerne.AutoCompleteCustomSource.Add(g.Name)
            Next

            For Each p In CurrentSettings.MovieProductions
                cmbMoviePro.Items.Add(p.Name)
                cmbMoviePro.AutoCompleteCustomSource.Add(p.Name)
            Next

            For Each d In CurrentSettings.MovieDistributors
                cmbMovieDis.Items.Add(d.Name)
                cmbMovieDis.AutoCompleteCustomSource.Add(d.Name)
            Next

            If reloadStarring Then
                For Each s In CurrentSettings.MovieStars
                    txtpMovieStar.AutoCompleteCustomSource.Add(s.Name)
                Next
            End If

            If cmbMovieCategory.Items.Count > 0 Then cmbMovieCategory.SelectedIndex = 0
            cmbMovieDis.Text = Nothing
            cmbMovieGerne.Text = Nothing
            cmbMoviePro.Text = Nothing

            pbMoviePreview2.Hide()
            lblMovieBtmDesc.Hide()
            lblMovieBtmStar.Hide()
            lblMovieBtmWeb.Hide()
            Label46.Hide()
            Label47.Hide()
            Label10.Hide()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnMovieCatRef_Click(sender As Object, e As EventArgs) Handles btnMovieCatRef.Click
        Dim gt As New Thread(AddressOf PopulateMovie) : gt.Start()
        tsmiMoviePaste.Enabled = False
    End Sub

    Private Sub cmbMovieCategory_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbMovieCategory.SelectedIndexChanged
        RefreshMovies()
    End Sub

    Private Sub RefreshMovies()
        Try
            Dim ml As GameList = _MovieList.Find(Function(x) x.Category = cmbMovieCategory.SelectedItem)

            lvMovie.Items.Clear()

            For Each movie As Game In ml.Games
                Dim item As New ListViewItem(movie.Name)
                With item
                    .SubItems.Add(movie.Path)
                    .SubItems.Add(movie.InstallDate)
                    .SubItems.Add(movie.Gerne)
                    .SubItems.Add(movie.Developer)
                    .SubItems.Add(movie.Publisher)
                    .SubItems.Add(movie.Rating)
                    .Tag = movie
                End With
                lvMovie.Items.Add(item)
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnMovieBrowse_Click(sender As Object, e As EventArgs) Handles btnMovieBrowse.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Movie..."
            ofd.RestoreDirectory = True
            ofd.InitialDirectory = movieInitial
            ofd.Filter = "All Supported Video Type|*.mkv;*.avi;*.mov;*.qt;*.flv;*.f4v;*.f4p;*.f4a;*.f4b;*.wmv;*.rm;*.rmvb;*.mp4;*.m4p;*.m4v;*.mpg;*.mpeg;*.m2v|Matroska|*.mkv|AVI|*.avi|QuickTime|*.mov;*.qt|Flash Video|*.flv;*.f4v;*.f4p;*.f4a;*.f4b|Windows Media Video|*.wmv|RealMedia|*.rm;*.rmvb|MPEG-4|*.mp4;*.m4p;*.m4v|MPEG-2|*.mpg;*.mpeg;*.m2v|All|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                txtMoviePath.Text = ofd.FileName
                movieInitial = Path.GetDirectoryName(ofd.FileName)
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnMovieAdd_Click(sender As Object, e As EventArgs) Handles btnMovieAdd.Click
        Try
            If txtMovieName.Text = Nothing Then
                MsgBox("Please enter movie name.", MsgBoxStyle.Critical, "Error")
            ElseIf txtMoviePath.Text = Nothing Then
                MsgBox("Please enter movie path.", MsgBoxStyle.Critical, "Error")
            ElseIf Not File.Exists(txtMoviePath.Text) Then
                MsgBox("Selected file is not exist.", MsgBoxStyle.Critical, "Error")
            Else
                If txtMovieWeb.Text = Nothing Then
                    Dim q As String = txtMovieName.Text.Replace("+", "%2B").Replace(" ", "+").ToLower
                    txtMovieWeb.Text = "https://www.google.com/search?q=" & q
                End If
                If _MovieImageBase64 = Nothing Then
                    Dim imageGen As Image = GenerateImage(txtMovieName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                    _MovieImageBase64 = pbMoviePreview.Image.ImageToBase64
                End If
                _MovieInstallDate = Now.ToShortDateString

                Dim item As New ListViewItem(txtMovieName.Text)
                With item
                    .SubItems.Add(txtMoviePath.Text)
                    .SubItems.Add(_MovieInstallDate)
                    .SubItems.Add(cmbMovieGerne.Text)
                    .SubItems.Add(cmbMovieDis.Text)
                    .SubItems.Add(cmbMoviePro.Text)
                    .SubItems.Add(txtMovieDirect.Text)
                    .Tag = New Game(txtMovieName.Text, txtMoviePath.Text, txtpMovieStar.ToString, _MovieInstallDate, txtMovieWeb.Text, txtMovieDescription.Text,
                                    cmbMovieGerne.Text, cmbMoviePro.Text, cmbMovieDis.Text, txtMovieDirect.Text, _MovieImageBase64)
                End With
                lvMovie.Items.Add(item)

                txtMovieName.Clear()
                txtMoviePath.Clear()
                txtpMovieStar.Clear()
                txtpMovieStar.ClearText()
                txtMovieWeb.Clear()
                txtMovieDescription.Clear()
                txtMovieDirect.Clear()
                _MovieImageBase64 = Nothing
                cmbMovieDis.Text = Nothing
                cmbMovieGerne.Text = Nothing
                cmbMoviePro.Text = Nothing
                pbMoviePreview.Image = My.Resources.NO_IMAGE
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnMovieCancel_Click(sender As Object, e As EventArgs) Handles btnMovieCancel.Click
        txtMovieName.Clear()
        txtMoviePath.Clear()
        txtpMovieStar.Clear()
        txtpMovieStar.ClearText()
        txtMovieWeb.Clear()
        txtMovieDescription.Clear()
        txtMovieDirect.Clear()
        _MovieImageBase64 = Nothing
        cmbMovieDis.Text = Nothing
        cmbMovieGerne.Text = Nothing
        cmbMoviePro.Text = Nothing
        pbMoviePreview.Image = My.Resources.NO_IMAGE
        btnMovieAdd.Enabled = True
        btnMovieEdit.Enabled = False
    End Sub

    Private Sub sbtnMovieBrowseImg_Click(sender As Object, e As EventArgs) Handles sbtnMovieBrowseImg.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Cover Art..."
            ofd.InitialDirectory = movieImageZip
            ofd.Filter = "Image Files(*.bmp;*.jpg;*.jpeg;*.gif;*.png)|*.bmp;*.jpg;*.jpeg;*.gif;*.png|All files (*.*)|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                Dim img As Image = SafeImageFromFile(ofd.FileName)
                pbMoviePreview.Image = img
                _MovieImageBase64 = img.ImageToBase64()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiMovieGenImg_Click(sender As Object, e As EventArgs) Handles tsmiMovieGenImg.Click
        Try
            Dim imageGen As Image = GenerateImage(txtMovieName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
            pbMoviePreview.Image = imageGen
            _MovieImageBase64 = pbMoviePreview.Image.ImageToBase64
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnMovieEdit_Click(sender As Object, e As EventArgs) Handles btnMovieEdit.Click
        Try
            If txtMovieName.Text = Nothing Then
                MsgBox("Please enter movie name.", MsgBoxStyle.Critical, "Error")
            ElseIf txtMoviePath.Text = Nothing Then
                MsgBox("Please enter movie path.", MsgBoxStyle.Critical, "Error")
            ElseIf Not File.Exists(txtMoviePath.Text) Then
                MsgBox("Selected file is not exist.", MsgBoxStyle.Critical, "Error")
            Else
                If txtMovieWeb.Text = Nothing Then
                    Dim q As String = txtMovieName.Text.Replace("+", "%2B").Replace(" ", "+").ToLower
                    txtMovieWeb.Text = "https://www.google.com/search?q=" & q
                End If
                If _MovieImageBase64 = Nothing Then
                    Dim imageGen As Image = GenerateImage(txtMovieName.Text.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                    _MovieImageBase64 = pbMoviePreview.Image.ImageToBase64
                End If
                _MovieInstallDate = Now.ToShortDateString

                mSI.Text = txtMovieName.Text
                mSI.SubItems(1).Text = txtMoviePath.Text
                mSI.SubItems(2).Text = _MovieInstallDate
                mSI.SubItems(3).Text = cmbMovieGerne.Text
                mSI.SubItems(4).Text = cmbMovieDis.Text
                mSI.SubItems(5).Text = cmbMoviePro.Text
                mSI.SubItems(6).Text = txtMovieDirect.Text
                mSI.Tag = New Game(txtMovieName.Text, txtMoviePath.Text, txtpMovieStar.ToString, _MovieInstallDate, txtMovieWeb.Text, txtMovieDescription.Text,
                                    cmbMovieGerne.Text, cmbMoviePro.Text, cmbMovieDis.Text, txtMovieDirect.Text, _MovieImageBase64)

                txtMovieName.Clear()
                txtMoviePath.Clear()
                txtpMovieStar.Clear()
                txtpMovieStar.ClearText()
                txtMovieWeb.Clear()
                txtMovieDescription.Clear()
                txtMovieDirect.Clear()
                _MovieImageBase64 = Nothing
                cmbMovieDis.Text = Nothing
                cmbMovieGerne.Text = Nothing
                cmbMoviePro.Text = Nothing
                pbMoviePreview.Image = My.Resources.NO_IMAGE
                btnMovieEdit.Enabled = False
                btnMovieAdd.Enabled = True
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiMovieEdit_Click(sender As Object, e As EventArgs) Handles tsmiMovieEdit.Click
        Try
            If Not lvMovie.SelectedItems.Count = 0 Then
                mSI = lvMovie.SelectedItems.Item(0)

                Dim movie As Game = DirectCast(mSI.Tag, Game)
                txtMovieName.Text = movie.Name
                txtMoviePath.Text = movie.Path
                txtpMovieStar.AddRange(movie.StartIn.Split(", "))
                txtMovieWeb.Text = movie.Website
                txtMovieDescription.Text = movie.Description
                cmbMovieDis.Text = movie.Developer
                cmbMovieGerne.Text = movie.Gerne
                cmbMoviePro.Text = movie.Publisher
                txtMovieDirect.Text = movie.Rating
                _MovieImageBase64 = movie.Image
                _MovieInstallDate = movie.InstallDate
                pbMoviePreview.Image = _MovieImageBase64.Base64ToImage

                btnMovieAdd.Enabled = False
                btnMovieEdit.Enabled = True
                btnMovieSave.Enabled = True
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lvMovie_DoubleClick(sender As Object, e As EventArgs) Handles lvMovie.DoubleClick
        tsmiMovieEdit.PerformClick()
    End Sub

    Private Sub tsmiMovieDel_Click(sender As Object, e As EventArgs) Handles tsmiMovieDel.Click
        Try
            If Not lvMovie.SelectedItems.Count = 0 Then
                mSI = lvMovie.SelectedItems.Item(0)
                Dim result As DialogResult = MessageBox.Show($"Are you sure you want to Delete {mSI.Text}?", "Confirm Delete?", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                If result = DialogResult.Yes Then
                    lvMovie.Items.Remove(mSI)
                End If
                mSI = Nothing
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiMovieRef_Click(sender As Object, e As EventArgs) Handles tsmiMovieRef.Click
        RefreshMovies()
    End Sub

    Private Sub btnMovieSave_Click(sender As Object, e As EventArgs) Handles btnMovieSave.Click
        Try
            Dim tempCat As Integer = cmbMovieCategory.SelectedIndex
            Dim temp As GameList = _MovieList.Find(Function(x) x.Category = cmbMovieCategory.SelectedItem)

            Dim ml = temp.Instance
            ml.Games.Clear()

            For Each item As ListViewItem In lvMovie.Items
                ml.Games.Add(DirectCast(item.Tag, Game))
            Next

            ml.FileName = temp.FileName
            ml.Save()
            MsgBox("Movie Save Completed.", MsgBoxStyle.Information, "LaunchManager")

            Dim gt As New Thread(AddressOf PopulateMovie) : gt.Start()
            tsmiMoviePaste.Enabled = False

            cmbMovieCategory.SelectedIndex = tempCat
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lblMovieDragDrop_DragEnter(sender As Object, e As DragEventArgs) Handles lblMovieDragDrop.DragEnter
        Try
            Dim formats As String() = e.Data.GetFormats
            e.Effect = DragDropEffects.All

            For Each file As String In e.Data.GetData(DataFormats.FileDrop)
                Dim name As String = IO.Path.GetFileNameWithoutExtension(file)
                Dim path As String = file
                Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                Dim web As String = "https://www.google.com/search?q=" & q

                Dim item As New ListViewItem(name)
                With item
                    .SubItems.Add(path)
                    .SubItems.Add(Now.ToShortDateString)
                    .SubItems.Add("")
                    .SubItems.Add("")
                    .SubItems.Add("")
                    .SubItems.Add("")
                    Dim image As Image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                    .Tag = New Game(name, path, Nothing, Now.ToShortDateString, web, Nothing, cmbMovieGerne.Items.Item(0), cmbMoviePro.Items.Item(0), cmbMovieDis.Items.Item(0),
                                    Nothing, image.ImageToBase64)
                End With
                lvMovie.Items.Add(item)
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub lvMovie_SelectedIndexChanged(sender As Object, e As EventArgs) Handles lvMovie.SelectedIndexChanged
        If Not lvMovie.SelectedItems.Count = 0 Then
            Dim selecteditem As ListViewItem = lvMovie.SelectedItems.Item(0)
            Dim movie As Game = CType(selecteditem.Tag, Game)
            pbMoviePreview2.Image = movie.Image.Base64ToImage
            lblMovieBtmDesc.Text = movie.Description
            lblMovieBtmStar.Text = movie.StartIn
            lblMovieBtmWeb.Text = movie.Website
            pbMoviePreview2.Show()
            lblMovieBtmDesc.Show()
            lblMovieBtmStar.Show()
            lblMovieBtmWeb.Show()
            Label46.Show()
            Label47.Show()
            Label10.Show()
        Else
            pbMoviePreview2.Hide()
            lblMovieBtmDesc.Hide()
            lblMovieBtmStar.Hide()
            lblMovieBtmWeb.Hide()
            Label46.Hide()
            Label47.Hide()
            Label10.Hide()
        End If
    End Sub

    Private Sub btnMovieBulkAdd_Click(sender As Object, e As EventArgs) Handles btnMovieBulkBrowse.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Movies..."
            ofd.RestoreDirectory = True
            ofd.InitialDirectory = movieInitial
            ofd.Filter = "Matroska|*.mkv|AVI|*.avi|QuickTime|*.mov;*.qt|Flash Video|*.flv;*.f4v;*.f4p;*.f4a;*.f4b|Windows Media Video|*.wmv|RealMedia|*.rm;*.rmvb|MPEG-4|*.mp4;*.m4p;*.m4v|MPEG-2|*.mpg;*.mpeg;*.m2v|All|*.*"
            ofd.FilterIndex = 1
            ofd.Multiselect = True

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                For Each file As String In ofd.FileNames
                    movieInitial = IO.Path.GetDirectoryName(file)
                    Dim name As String = IO.Path.GetFileNameWithoutExtension(file)
                    Dim path As String = file
                    Dim q As String = name.Replace("+", "%2B").Replace(" ", "+").ToLower
                    Dim web As String = "https://www.google.com/search?q=" & q

                    Dim item As New ListViewItem(name)
                    With item
                        .SubItems.Add(path)
                        .SubItems.Add(Now.ToShortDateString)
                        .SubItems.Add("")
                        .SubItems.Add("")
                        .SubItems.Add("")
                        .SubItems.Add("")
                        Dim image As Image = GenerateImage(name.Substring(0, 1), Color.FromArgb(114, 137, 218), Color.White, New Size(284, 439))
                        .Tag = New Game(name, path, Nothing, Now.ToShortDateString, web, Nothing, cmbMovieGerne.Items.Item(0), cmbMoviePro.Items.Item(0), cmbMovieDis.Items.Item(0),
                                        Nothing, image.ImageToBase64)
                    End With
                    lvMovie.Items.Add(item)
                Next
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tsmiMovieCopy_Click(sender As Object, e As EventArgs) Handles tsmiMovieCopy.Click
        If lvMovie.SelectedItems.Count = 0 Then Exit Sub
        tsmiMoviePaste.Enabled = True
        movieClipboard = lvMovie.SelectedItems.Item(0)
    End Sub

    Private Sub tsmiMoviePaste_Click(sender As Object, e As EventArgs) Handles tsmiMoviePaste.Click
        lvMovie.Items.Add(movieClipboard.Clone)
    End Sub

#End Region

#Region "About"

    Private Sub AboutLoad()
        lblCopyright.Text = My.Application.Info.Copyright
        If registered Then btnAbtReg.Hide()
    End Sub

    Private Sub llblAbtProductWeb_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llblAbtProductWeb.LinkClicked
        Process.Start("http://zettabytetek.com/")
    End Sub

    Private Sub llblAbtPurchase_LinkClicked(sender As Object, e As LinkLabelLinkClickedEventArgs) Handles llblAbtPurchase.LinkClicked
        Process.Start("http://zettabytetek.com/shop")
    End Sub

    Private Sub btnAbtReg_Click(sender As Object, e As EventArgs) Handles btnAbtReg.Click
        frmRegister.Show()
        Close()
    End Sub

#End Region
End Class
