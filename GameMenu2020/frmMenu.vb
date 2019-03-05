Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Threading
Imports LaunchMenu.WinApi

Public Class frmMenu

#Region "Window Behavior"

#Region "Fields"
    Private dwmMargins As Dwm.MARGINS
    Private _marginOk As Boolean
    Private _aeroEnabled As Boolean = False
#End Region
#Region "Ctor"
    Public Sub New()
        SetStyle(ControlStyles.ResizeRedraw, True)

        InitializeComponent()

        DoubleBuffered = True

    End Sub
#End Region
#Region "Props"
    Public ReadOnly Property AeroEnabled() As Boolean
        Get
            Return _aeroEnabled
        End Get
    End Property
#End Region
#Region "Methods"
    Public Shared Function LoWord(ByVal dwValue As Integer) As Integer
        Return dwValue And &HFFFF
    End Function
    ''' <summary>
    ''' Equivalent to the HiWord C Macro
    ''' </summary>
    ''' <param name="dwValue"></param>
    ''' <returns></returns>
    Public Shared Function HiWord(ByVal dwValue As Integer) As Integer
        Return (dwValue >> 16) And &HFFFF
    End Function
#End Region

    Private Sub Form1_Activated(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Activated
        Dwm.DwmExtendFrameIntoClientArea(Me.Handle, dwmMargins)
    End Sub

    Protected Overloads Overrides Sub WndProc(ByRef m As Message)
        Dim WM_NCCALCSIZE As Integer = &H83
        Dim WM_NCHITTEST As Integer = &H84
        Dim result As IntPtr

        Dim dwmHandled As Integer = Dwm.DwmDefWindowProc(m.HWnd, m.Msg, m.WParam, m.LParam, result)

        If dwmHandled = 1 Then
            m.Result = result
            Exit Sub
        End If

        If m.Msg = WM_NCCALCSIZE AndAlso CInt(m.WParam) = 1 Then
            Dim nccsp As NCCALCSIZE_PARAMS =
              DirectCast(Marshal.PtrToStructure(m.LParam,
              GetType(NCCALCSIZE_PARAMS)), NCCALCSIZE_PARAMS)

            ' Adjust (shrink) the client rectangle to accommodate the border:
            nccsp.rect0.Top += 0
            nccsp.rect0.Bottom += 0
            nccsp.rect0.Left += 0
            nccsp.rect0.Right += 0

            If Not _marginOk Then
                'Set what client area would be for passing to DwmExtendIntoClientArea. Also remember that at least one of these values NEEDS TO BE > 1, else it won't work.
                dwmMargins.cyTopHeight = 0
                dwmMargins.cxLeftWidth = 0
                dwmMargins.cyBottomHeight = 3
                dwmMargins.cxRightWidth = 0
                _marginOk = True
            End If

            Marshal.StructureToPtr(nccsp, m.LParam, False)

            m.Result = IntPtr.Zero
        ElseIf m.Msg = WM_NCHITTEST AndAlso CInt(m.Result) = 0 Then
            m.Result = HitTestNCA(m.HWnd, m.WParam, m.LParam)
        Else
            MyBase.WndProc(m)
        End If
    End Sub

    Private Function HitTestNCA(ByVal hwnd As IntPtr, ByVal wparam _
                                      As IntPtr, ByVal lparam As IntPtr) As IntPtr
        Dim HTNOWHERE As Integer = 0
        Dim HTCLIENT As Integer = 1
        Dim HTCAPTION As Integer = 2
        Dim HTGROWBOX As Integer = 4
        Dim HTSIZE As Integer = HTGROWBOX
        Dim HTMINBUTTON As Integer = 8
        Dim HTMAXBUTTON As Integer = 9
        Dim HTLEFT As Integer = 10
        Dim HTRIGHT As Integer = 11
        Dim HTTOP As Integer = 12
        Dim HTTOPLEFT As Integer = 13
        Dim HTTOPRIGHT As Integer = 14
        Dim HTBOTTOM As Integer = 15
        Dim HTBOTTOMLEFT As Integer = 16
        Dim HTBOTTOMRIGHT As Integer = 17
        Dim HTREDUCE As Integer = HTMINBUTTON
        Dim HTZOOM As Integer = HTMAXBUTTON
        Dim HTSIZEFIRST As Integer = HTLEFT
        Dim HTSIZELAST As Integer = HTBOTTOMRIGHT

        Dim p As New Point(LoWord(CInt(lparam)), HiWord(CInt(lparam)))

        Dim topleft As Rectangle = RectangleToScreen(New Rectangle(0, 0,
                                   dwmMargins.cxLeftWidth, dwmMargins.cxLeftWidth))

        If topleft.Contains(p) Then
            Return New IntPtr(HTTOPLEFT)
        End If

        Dim topright As Rectangle =
          RectangleToScreen(New Rectangle(Width - dwmMargins.cxRightWidth, 0,
          dwmMargins.cxRightWidth, dwmMargins.cxRightWidth))

        If topright.Contains(p) Then
            Return New IntPtr(HTTOPRIGHT)
        End If

        Dim botleft As Rectangle =
           RectangleToScreen(New Rectangle(0, Height - dwmMargins.cyBottomHeight,
           dwmMargins.cxLeftWidth, dwmMargins.cyBottomHeight))

        If botleft.Contains(p) Then
            Return New IntPtr(HTBOTTOMLEFT)
        End If

        Dim botright As Rectangle =
            RectangleToScreen(New Rectangle(Width - dwmMargins.cxRightWidth,
            Height - dwmMargins.cyBottomHeight,
            dwmMargins.cxRightWidth, dwmMargins.cyBottomHeight))

        If botright.Contains(p) Then
            Return New IntPtr(HTBOTTOMRIGHT)
        End If

        Dim top As Rectangle =
            RectangleToScreen(New Rectangle(0, 0, Width, dwmMargins.cxLeftWidth))

        If top.Contains(p) Then
            Return New IntPtr(HTTOP)
        End If

        Dim cap As Rectangle =
            RectangleToScreen(New Rectangle(0, dwmMargins.cxLeftWidth,
            Width, dwmMargins.cyTopHeight - dwmMargins.cxLeftWidth))

        If cap.Contains(p) Then
            Return New IntPtr(HTCAPTION)
        End If

        Dim left As Rectangle =
            RectangleToScreen(New Rectangle(0, 0, dwmMargins.cxLeftWidth, Height))

        If left.Contains(p) Then
            Return New IntPtr(HTLEFT)
        End If

        Dim right As Rectangle =
            RectangleToScreen(New Rectangle(Width - dwmMargins.cxRightWidth,
            0, dwmMargins.cxRightWidth, Height))

        If right.Contains(p) Then
            Return New IntPtr(HTRIGHT)
        End If

        Dim bottom As Rectangle =
            RectangleToScreen(New Rectangle(0, Height - dwmMargins.cyBottomHeight,
            Width, dwmMargins.cyBottomHeight))

        If bottom.Contains(p) Then
            Return New IntPtr(HTBOTTOM)
        End If

        Return New IntPtr(HTCLIENT)
    End Function

    Private Const BorderWidth As Integer = 6

    Private _resizeDir As ResizeDirection = ResizeDirection.None

    Private Sub Form1_MouseDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseDown
        If e.Button = MouseButtons.Left Then
            If Me.Width - BorderWidth > e.Location.X AndAlso e.Location.X > BorderWidth AndAlso e.Location.Y > BorderWidth Then
                MoveControl(Me.Handle)
            Else
                Select Case FormBorderStyle
                    Case FormBorderStyle.Sizable, FormBorderStyle.SizableToolWindow
                        If WindowState <> FormWindowState.Maximized Then
                            ResizeForm(resizeDir)
                        End If
                End Select
            End If
        End If
    End Sub

    Public Enum ResizeDirection
        None = 0
        Left = 1
        TopLeft = 2
        Top = 4
        TopRight = 8
        Right = 16
        BottomRight = 32
        Bottom = 64
        BottomLeft = 128
    End Enum

    Private Property resizeDir() As ResizeDirection
        Get
            Return _resizeDir
        End Get
        Set(ByVal value As ResizeDirection)
            _resizeDir = value

            'Change cursor
            Select Case value
                Case ResizeDirection.Left
                    Me.Cursor = Cursors.SizeWE

                Case ResizeDirection.Right
                    Me.Cursor = Cursors.SizeWE

                Case ResizeDirection.Top
                    Me.Cursor = Cursors.SizeNS

                Case ResizeDirection.Bottom
                    Me.Cursor = Cursors.SizeNS

                Case ResizeDirection.BottomLeft
                    Me.Cursor = Cursors.SizeNESW

                Case ResizeDirection.TopRight
                    Me.Cursor = Cursors.SizeNESW

                Case ResizeDirection.BottomRight
                    Me.Cursor = Cursors.SizeNWSE

                Case ResizeDirection.TopLeft
                    Me.Cursor = Cursors.SizeNWSE

                Case Else
                    Me.Cursor = Cursors.Default
            End Select
        End Set
    End Property

    Private Sub Form1_MouseMove(ByVal sender As System.Object, ByVal e As System.Windows.Forms.MouseEventArgs) Handles MyBase.MouseMove
        'Calculate which direction to resize based on mouse position

        If e.Location.X < BorderWidth And e.Location.Y < BorderWidth Then
            resizeDir = ResizeDirection.TopLeft

        ElseIf e.Location.X < BorderWidth And e.Location.Y > Me.Height - BorderWidth Then
            resizeDir = ResizeDirection.BottomLeft

        ElseIf e.Location.X > Me.Width - BorderWidth And e.Location.Y > Me.Height - BorderWidth Then
            resizeDir = ResizeDirection.BottomRight

        ElseIf e.Location.X > Me.Width - BorderWidth And e.Location.Y < BorderWidth Then
            resizeDir = ResizeDirection.TopRight

        ElseIf e.Location.X < BorderWidth Then
            resizeDir = ResizeDirection.Left

        ElseIf e.Location.X > Me.Width - BorderWidth Then
            resizeDir = ResizeDirection.Right

        ElseIf e.Location.Y < BorderWidth Then
            resizeDir = ResizeDirection.Top

        ElseIf e.Location.Y > Me.Height - BorderWidth Then
            resizeDir = ResizeDirection.Bottom

        Else
            resizeDir = ResizeDirection.None
        End If
    End Sub

    Private Sub MoveControl(ByVal hWnd As IntPtr)
        ReleaseCapture()
        SendMessage(hWnd, WM_NCLBUTTONDOWN, HTCAPTION, 0)
    End Sub

    Private Sub ResizeForm(ByVal direction As ResizeDirection)
        Dim dir As Integer = -1
        Select Case direction
            Case ResizeDirection.Left
                dir = HTLEFT
            Case ResizeDirection.TopLeft
                dir = HTTOPLEFT
            Case ResizeDirection.Top
                dir = HTTOP
            Case ResizeDirection.TopRight
                dir = HTTOPRIGHT
            Case ResizeDirection.Right
                dir = HTRIGHT
            Case ResizeDirection.BottomRight
                dir = HTBOTTOMRIGHT
            Case ResizeDirection.Bottom
                dir = HTBOTTOM
            Case ResizeDirection.BottomLeft
                dir = HTBOTTOMLEFT
        End Select

        If dir <> -1 Then
            ReleaseCapture()
            SendMessage(Me.Handle, WM_NCLBUTTONDOWN, dir, 0)
        End If

    End Sub

    <DllImport("user32.dll")>
    Public Shared Function ReleaseCapture() As Boolean
    End Function

    <DllImport("user32.dll")>
    Public Shared Function SendMessage(ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByVal lParam As Integer) As Integer
    End Function

    Private Const WM_NCLBUTTONDOWN As Integer = &HA1
    Private Const HTBORDER As Integer = 18
    Private Const HTBOTTOM As Integer = 15
    Private Const HTBOTTOMLEFT As Integer = 16
    Private Const HTBOTTOMRIGHT As Integer = 17
    Private Const HTCAPTION As Integer = 2
    Private Const HTLEFT As Integer = 10
    Private Const HTRIGHT As Integer = 11
    Private Const HTTOP As Integer = 12
    Private Const HTTOPLEFT As Integer = 13
    Private Const HTTOPRIGHT As Integer = 14
#End Region

#Region "Draw Form"

    Protected Overrides Sub OnPaint(e As PaintEventArgs)
        MyBase.OnPaint(e)

        Dim formGraphics As Graphics = e.Graphics

        If ShowIcon Then
            formGraphics.DrawIcon(Icon, New Rectangle(New Point(9, 9), New Size(20, 20)))
            Using f As Font = New Font(Font.Name, 10.0F, Font.Style, GraphicsUnit.Point)
                Dim rect As New Rectangle(30, 9, Width - 10, 20)
                Dim flags As TextFormatFlags = TextFormatFlags.Left
                TextRenderer.DrawText(formGraphics, Text, f, rect, ForeColor, flags)
                formGraphics.DrawRectangle(Pens.Transparent, rect)
            End Using
        Else
            Using f As Font = New Font(Font.Name, 10.0F, Font.Style, GraphicsUnit.Point)
                Dim rect As New Rectangle(9, 9, Width - 10, 20)
                Dim flags As TextFormatFlags = TextFormatFlags.Left
                TextRenderer.DrawText(formGraphics, Text, f, rect, ForeColor, flags)
                formGraphics.DrawRectangle(Pens.Transparent, rect)
            End Using
        End If
    End Sub

#End Region

    Private FeedURL As String = "https://www.imnotmental.com"
    Private FeedItems As ArrayList
    Private TabPageDic As New Dictionary(Of TabPage, GameList)
    Private NewGames As New GameList($"{Application.StartupPath}\Cache\newgamecache.xml")
    Private NewMovie As New GameList($"{Application.StartupPath}\Cache\newmoviecache.xml")

#Region "Translate"

    Private welcome_to As String = "Welcome to "
    Private screen_lock As String = "Screen Lock"
    Private mouse_properties As String = "Mouse Properties"
    Private notepad As String = "Notepad"
    Private calculator As String = "Calculator"
    Private internet_explorer As String = "Internet Explorer"
    Private magnifier As String = "Magnifier"
    Private math_input_panel As String = "Math Input Panel"
    Private snipping_tool As String = "Snipping Tool"
    Private wordpad As String = "Wordpad"
    Private controller_setting As String = "Controller Setting"
    Private microsoft_edge As String = "Microsoft Edge"

#End Region

    Private Sub frmMenu_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Hide()

            Dim ms As New MenuSettings(settingFile)
            CurrentSettings = ms.Instance
            frmLoading.Translate()

            CheckForIllegalCrossThreadCalls = False

            Translate()

            tpWelcome.TabIndex = 0
            tpRekon.TabIndex = 1
            tpNewGame.TabIndex = 2
            tpNewMovie.TabIndex = 3

            Text = $"{welcome_to}{CurrentSettings.CompanyName}"

            If CurrentSettings.Activity Then
                Dim feed As New Thread(AddressOf InvokeReadFeed)
                feed.Start()
            Else
                tcGames.TabPages.Remove(tpWelcome)
            End If

            Dim pop As New Thread(AddressOf InvokePopulate)
            pop.Start()

            If CurrentSettings.NewItems Then
                NewGames = NewGames.Instance
                NewMovie = NewMovie.Instance

                TabPageDic.Add(tpNewGame, NewGames)
                TabPageDic.Add(tpNewMovie, NewMovie)
            Else
                tcGames.TabPages.Remove(tpRekon)
                tcGames.TabPages.Remove(tpNewGame)
                tcGames.TabPages.Remove(tpNewMovie)
            End If

            flpSearchResult.Width = 0
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub tLoading_Tick(sender As Object, e As EventArgs) Handles tLoading.Tick
        Try
            Show()
            If tcGames.SelectedTab.Tag <> Nothing Then tcGames.SelectTab(1)
            If Not Opacity = 1 Then Opacity += 0.02 Else tLoading.Stop() : frmLoading.Hide() : tAnimation.Start()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub tAnimation_Tick(sender As Object, e As EventArgs) Handles tAnimation.Tick
        Try
            If txtSearch.IsFocused Then
                Do Until txtSearch.Width = 250
                    txtSearch.Width += 1
                    flpSocial.Location = New Point(flpSocial.Location.X - 1, flpSocial.Location.Y)
                    txtSearch.Location = New Point(txtSearch.Location.X - 1, txtSearch.Location.Y)
                    txtSearch.Update()
                Loop
            Else
                If txtSearch.Text = txtSearch.Placeholder Then
                    Do Until txtSearch.Width = 150
                        txtSearch.Width -= 1
                        flpSocial.Location = New Point(flpSocial.Location.X + 1, flpSocial.Location.Y)
                        txtSearch.Location = New Point(txtSearch.Location.X + 1, txtSearch.Location.Y)
                        txtSearch.Update()
                    Loop
                    If Not tcGames.SelectedTab.Text = tpWelcome.Text Then
                        flpSearchResult.AutoScroll = False
                        Do Until flpSearchResult.Width = 0
                            flpSearchResult.Width -= 10
                            flpSearchResult.Update()
                        Loop
                    Else
                        flpSearchResult.Width = 0
                    End If
                End If
            End If
            If txtSearch.EnterPressed AndAlso Not txtSearch.Text = Nothing AndAlso Not tcGames.SelectedTab.Text = tpWelcome.Text Then
                flpSearchResult.Controls.Clear()
                Dim openedTab As TabPage = tcGames.SelectedTab
                Dim gl As GameList = TabPageDic.Item(openedTab)
                For Each game In gl.Games
                    Select Case gl.Type
                        Case CategoryType.Application
                            If game.Name.ToLower.Contains(txtSearch.Text.ToLower) Then
                                flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                            Else
                                If game.Developer.ToLower.Contains(txtSearch.Text.ToLower) Then
                                    flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                Else
                                    If game.Gerne.ToLower.Contains(txtSearch.Text.ToLower) Then
                                        flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                    Else
                                        If game.Description.ToLower.Contains(txtSearch.Text.ToLower) Then
                                            flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                        End If
                                    End If
                                End If
                            End If
                        Case CategoryType.Game
                            If game.Name.ToLower.Contains(txtSearch.Text.ToLower) Then
                                flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                            Else
                                If game.Developer.ToLower.Contains(txtSearch.Text.ToLower) Then
                                    flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                Else
                                    If game.Publisher.ToLower.Contains(txtSearch.Text.ToLower) Then
                                        flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                    Else
                                        If game.Description.ToLower.Contains(txtSearch.Text.ToLower) Then
                                            flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                        Else
                                            If game.Gerne.ToLower.Contains(txtSearch.Text.ToLower) Then
                                                flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                        Case CategoryType.Movie
                            If game.Name.ToLower.Contains(txtSearch.Text.ToLower) Then
                                flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                            Else
                                If game.Developer.ToLower.Contains(txtSearch.Text.ToLower) Then
                                    flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                Else
                                    If game.Publisher.ToLower.Contains(txtSearch.Text.ToLower) Then
                                        flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                    Else
                                        If game.Description.ToLower.Contains(txtSearch.Text.ToLower) Then
                                            flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                        Else
                                            If game.Gerne.ToLower.Contains(txtSearch.Text.ToLower) Then
                                                flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                            Else
                                                If game.StartIn.ToLower.Contains(txtSearch.Text.ToLower) Then
                                                    flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                                Else
                                                    If game.Rating.ToLower.Contains(txtSearch.Text.ToLower) Then
                                                        flpSearchResult.Controls.Add(New AppItemW(game.Name, game.Path, game.StartIn, game.Website, game.Publisher, game.Gerne, game.Description, game.Rating, game.Image, game.Developer, gl.Type))
                                                    End If
                                                End If
                                            End If
                                        End If
                                    End If
                                End If
                            End If
                    End Select
                Next
                Do Until flpSearchResult.Width = 260
                    flpSearchResult.Width += 20
                    flpSearchResult.Refresh()
                Loop
                flpSearchResult.AutoScroll = True
                flpSearchResult.Refresh()
                txtSearch.EnterPressed = False
            Else
                txtSearch.EnterPressed = False
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub InvokePopulate()
        Try
            If InvokeRequired Then
                BeginInvoke(DirectCast(Sub()
                                           Populate()
                                       End Sub, MethodInvoker))
            Else
                Populate()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub Populate()
        Try
            If Directory.Exists(xmlPath) Then
                For Each cat In Directory.GetFiles(xmlPath, "*.xml")
                    Dim t As New GameList(cat)
                    t.ReadFromFile()
                    Dim gl As GameList = t.Instance

                    Dim flp As New SmoothAutoScrollFlowLayoutPanel With {.Dock = DockStyle.Fill, .AutoScroll = True, .ParentForm = Me}
                    Dim tp As New TabPage(gl.Category)
                    With tp
                        If gl.HasIcon Then .ImageIndex = gl.Icon
                        If gl.Type = CategoryType.Seperator Then .Tag = "S"
                        .TabIndex = gl.Index + 4 'Activity, Recommended, New Games, New Movies
                        .Padding = New Padding(3, 3, 3, 3)
                        .Controls.Add(flp)
                        TabPageDic.Add(tp, gl)
                    End With

                    tcGames.TabPages.Add(tp)
                    For Each g As Game In gl.Games
                        Select Case gl.Type
                            Case CategoryType.Game
                                If CurrentSettings.NewItems Then
                                    Dim span = DateTime.Parse(g.InstallDate) - Now
                                    If span.TotalDays <= 14 Then
                                        flpNewGames.Controls.Add(New AppItemL(g.Name, g.Path, g.StartIn, g.Website, g.Publisher, g.Gerne, g.Description, g.Rating, g.Image, g.Developer) With {.Size = CurrentSettings.GameItemSize})
                                        NewGames.Games.Add(g)
                                    End If
                                End If
                                flp.Controls.Add(New AppItemL(g.Name, g.Path, g.StartIn, g.Website, g.Publisher, g.Gerne, g.Description, g.Rating, g.Image, g.Developer) With {.Size = CurrentSettings.GameItemSize})
                            Case CategoryType.Movie
                                If CurrentSettings.NewItems Then
                                    Dim span = DateTime.Parse(g.InstallDate) - Now
                                    If span.TotalDays <= 14 Then
                                        flpNewMovies.Controls.Add(New AppItemL(g.Name, g.Path, g.StartIn, g.Website, g.Publisher, g.Gerne, g.Description, g.Rating, g.Image, g.Developer, gl.Type) With {.Size = CurrentSettings.MovieItemSize})
                                        NewMovie.Games.Add(g)
                                    End If
                                End If
                                flp.Controls.Add(New AppItemL(g.Name, g.Path, g.StartIn, g.Website, g.Publisher, g.Gerne, g.Description, g.Rating, g.Image, g.Developer, gl.Type) With {.Size = CurrentSettings.MovieItemSize})
                            Case CategoryType.Application
                                flp.Controls.Add(New AppItemS(g.Name, g.Path, g.StartIn, g.Website, g.Publisher, g.Gerne, g.Description, g.Rating, g.Image, g.Developer) With {.Size = CurrentSettings.AppItemSize})
                        End Select
                    Next
                Next

                tcGames.Sort()

                If CurrentSettings.Activity Then
                    If CurrentSettings.ScreenLock Then flpQuickLaunch.Controls.Add(New AppItemS(screen_lock, "launchmenu:screenlock", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.Settings_Screen_Lock.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.Mouse Then flpQuickLaunch.Controls.Add(New AppItemS(mouse_properties, "launchmenu:mouse", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.mouse.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.Notepad Then flpQuickLaunch.Controls.Add(New AppItemS(notepad, "launchmenu:notepad", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.Notepad.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.Calculator Then flpQuickLaunch.Controls.Add(New AppItemS(calculator, "launchmenu:calc", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.calculator.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.IExplorer Then flpQuickLaunch.Controls.Add(New AppItemS(internet_explorer, "launchmenu:ie", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.Internet_Explorer.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.Magnifier Then flpQuickLaunch.Controls.Add(New AppItemS(magnifier, "launchmenu:magnify", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.magnify.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.MathInput Then flpQuickLaunch.Controls.Add(New AppItemS(math_input_panel, "launchmenu:mip", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.mip.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.SnippingTool Then flpQuickLaunch.Controls.Add(New AppItemS(snipping_tool, "launchmenu:snippingtool", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.snip.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.Wordpad Then flpQuickLaunch.Controls.Add(New AppItemS(wordpad, "launchmenu:wordpad", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.wordpad.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.Controller Then flpQuickLaunch.Controls.Add(New AppItemS(controller_setting, "launchmenu:joy", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.joy.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                    If CurrentSettings.MSEdge Then flpQuickLaunch.Controls.Add(New AppItemS(microsoft_edge, "launchmenu:edge", Nothing, Nothing, Nothing, Nothing, Nothing, Nothing, My.Resources.Microsoft_Edge.ImageToBase64, Nothing) With {.Size = CurrentSettings.AppItemSize})
                End If

                For Each s As SocialIcon In CurrentSettings.Social
                    flpSocial.Controls.Add(New SocialLabel() With {.URL = s.URL, .Logo = s.Logo.ToSocialIcon})
                Next
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

#Region "News Feed"

    Private Sub InvokeReadFeed()
        Try
            If InvokeRequired Then
                BeginInvoke(DirectCast(Sub()
                                           ReadFeed()
                                       End Sub, MethodInvoker))
            Else
                ReadFeed()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub ReadFeed()
        Try
            Dim channel As New RSSChannel(CurrentSettings.FeedsURL)

            Dim threadPool As New List(Of Thread)
            Dim fit As New Thread(Sub() FeedItems = channel.GetChannelItems(CurrentSettings.MediaNode))
            fit.Start()
            threadPool.Add(fit)

            For Each t In threadPool
                t.Join()
            Next

            Dim item1 As RSSItem = FeedItems(0)
            SliderSItem1.Caption = item1.Title
            SliderSItem1.FeedSubtitle = item1.Description
            SliderSItem1.FeedURL = item1.Link
            SliderSItem1.FeedDate = item1.PublishDate
            SliderSItem1.BackgroundImage = item1.Image

            SliderLItem1.Caption = item1.Title
            SliderLItem1.SubCaption = item1.Description
            SliderLItem1.PublishDate = item1.PublishDate
            SliderLItem1.FeedURL = item1.Link
            SliderLItem1.BackgroundImage = item1.Image

            Dim item2 As RSSItem = FeedItems(1)
            SliderSItem2.Caption = item2.Title
            SliderSItem2.FeedSubtitle = item2.Description
            SliderSItem2.FeedURL = item2.Link
            SliderSItem2.FeedDate = item2.PublishDate
            SliderSItem2.BackgroundImage = item2.Image

            Dim item3 As RSSItem = FeedItems(2)
            SliderSItem3.Caption = item3.Title
            SliderSItem3.FeedSubtitle = item3.Description
            SliderSItem3.FeedURL = item3.Link
            SliderSItem3.FeedDate = item3.PublishDate
            SliderSItem3.BackgroundImage = item3.Image

            Dim item4 As RSSItem = FeedItems(3)
            SliderSItem4.Caption = item4.Title
            SliderSItem4.FeedSubtitle = item4.Description
            SliderSItem4.FeedURL = item4.Link
            SliderSItem4.FeedDate = item4.PublishDate
            SliderSItem4.BackgroundImage = item4.Image

            For i As Integer = 4 To FeedItems.Count - 1
                Dim rss As RSSItem = FeedItems(i)
                Dim newsItem As New NewsLabel() With {.Text = rss.Title, .URL = rss.Link, .PubDate = rss.PublishDate, .ForeColor = Color.White, .Cursor = Cursors.Hand, .Size = New Size(flpNewsLeft.Width - 50, 20), .AutoSize = False, .Font = New Font("Segoe UI", 10S, FontStyle.Regular), .Image = rss.Image, .BackColor = Color.Transparent}
                flpNewsLeft.Controls.Add(newsItem)
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub SliderSItem1_Click(sender As Object, e As EventArgs) Handles SliderSItem1.Click
        Try
            SliderSItem1.Selected = True
            SliderSItem2.Selected = False
            SliderSItem3.Selected = False
            SliderSItem4.Selected = False

            SliderLItem1.BackgroundImage = SliderSItem1.BackgroundImage
            SliderLItem1.Caption = SliderSItem1.Caption
            SliderLItem1.SubCaption = SliderSItem1.FeedSubtitle
            SliderLItem1.PublishDate = SliderSItem1.FeedDate
            SliderLItem1.FeedURL = SliderSItem1.FeedURL
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub SliderSItem2_Click(sender As Object, e As EventArgs) Handles SliderSItem2.Click
        Try
            SliderSItem1.Selected = False
            SliderSItem2.Selected = True
            SliderSItem3.Selected = False
            SliderSItem4.Selected = False

            SliderLItem1.BackgroundImage = SliderSItem2.BackgroundImage
            SliderLItem1.Caption = SliderSItem2.Caption
            SliderLItem1.SubCaption = SliderSItem2.FeedSubtitle
            SliderLItem1.PublishDate = SliderSItem2.FeedDate
            SliderLItem1.FeedURL = SliderSItem2.FeedURL
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub SliderSItem3_Click(sender As Object, e As EventArgs) Handles SliderSItem3.Click
        Try
            SliderSItem1.Selected = False
            SliderSItem2.Selected = False
            SliderSItem3.Selected = True
            SliderSItem4.Selected = False

            SliderLItem1.BackgroundImage = SliderSItem3.BackgroundImage
            SliderLItem1.Caption = SliderSItem3.Caption
            SliderLItem1.SubCaption = SliderSItem3.FeedSubtitle
            SliderLItem1.PublishDate = SliderSItem3.FeedDate
            SliderLItem1.FeedURL = SliderSItem3.FeedURL
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub SliderSItem4_Click(sender As Object, e As EventArgs) Handles SliderSItem4.Click
        Try
            SliderSItem1.Selected = False
            SliderSItem2.Selected = False
            SliderSItem3.Selected = False
            SliderSItem4.Selected = True

            SliderLItem1.BackgroundImage = SliderSItem4.BackgroundImage
            SliderLItem1.Caption = SliderSItem4.Caption
            SliderLItem1.SubCaption = SliderSItem4.FeedSubtitle
            SliderLItem1.PublishDate = SliderSItem4.FeedDate
            SliderLItem1.FeedURL = SliderSItem4.FeedURL
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub SliderLItem1_Click(sender As Object, e As EventArgs) Handles SliderLItem1.Click
        Try
            Process.Start(SliderLItem1.FeedURL)
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub tcGames_SelectedIndexChanged(sender As Object, e As EventArgs) Handles tcGames.SelectedIndexChanged
        txtSearch.Text = txtSearch.Placeholder
    End Sub

#End Region

    Private Sub Translate()
        Dim iniFile As String = $"{Application.StartupPath}\Data\Language\{CurrentSettings.Language}.ini"

        '= ReadIniValue(iniFile, "MENU", "")
        welcome_to = ReadIniValue(iniFile, "MENU", "Welcome_To")
        screen_lock = ReadIniValue(iniFile, "MENU", "Screen_Lock")
        mouse_properties = ReadIniValue(iniFile, "MENU", "Mouse_Properties")
        notepad = ReadIniValue(iniFile, "MENU", "Notepad")
        calculator = ReadIniValue(iniFile, "MENU", "Calculator")
        internet_explorer = ReadIniValue(iniFile, "MENU", "Internet_Explorer")
        magnifier = ReadIniValue(iniFile, "MENU", "Magnifier")
        math_input_panel = ReadIniValue(iniFile, "MENU", "Math_Input_Panel")
        snipping_tool = ReadIniValue(iniFile, "MENU", "Snipping_Tool")
        wordpad = ReadIniValue(iniFile, "MENU", "Wordpad")
        controller_setting = ReadIniValue(iniFile, "MENU", "Controller_Setting")
        microsoft_edge = ReadIniValue(iniFile, "MENU", "Microsoft_Edge")
        txtSearch.Text = ReadIniValue(iniFile, "MENU", "Search")
        txtSearch.Placeholder = ReadIniValue(iniFile, "MENU", "Search")
        tpNewGame.Text = ReadIniValue(iniFile, "MENU", "New_Games")
        tpNewMovie.Text = ReadIniValue(iniFile, "MENU", "New_Movies")
        tpRekon.Text = ReadIniValue(iniFile, "MENU", "Recommended")
        tpWelcome.Text = ReadIniValue(iniFile, "MENU", "Activity")
        Label1.Text = ReadIniValue(iniFile, "MENU", "More_News")
        Label2.Text = ReadIniValue(iniFile, "MENU", "Quick_Launcher")
    End Sub

End Class
