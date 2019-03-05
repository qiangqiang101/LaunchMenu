Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports System.IO
Imports System.Threading
Imports LaunchMenu.WeatherUndergroundData

Public Class frmScreenLock

    Dim Time As String = TimeOfDay.ToString("hh:mm:ss tt")
    Dim [Date] As String = Format(Now, "dddd, MMMM dd, yyyy")
    Dim WeatherIcon As String = "W"
    Dim Temperature As String = "30°C"
    Dim GeoLocation As String = "Kuala Lumpur"
    Dim Weather As String = "Rain"
    Dim Wind As String = "30°C/20°C"
    Dim CurrentWeather As New CurrentConditions("ee9681a8ed7bd77d")
    Dim WeatherFont As New Text.PrivateFontCollection()
    Dim WeatherIconFont As Font
    Dim WeatherThread As Thread
    Public IsPrimary As Boolean = True
    Public Backgrounds As New List(Of Form)
    Public Password As String

    Private Sub frmBackground_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        Try
            Dim formGraphics As Graphics = e.Graphics
            formGraphics.InterpolationMode = InterpolationMode.NearestNeighbor
            formGraphics.PixelOffsetMode = PixelOffsetMode.Half
            formGraphics.SmoothingMode = SmoothingMode.AntiAlias

            Dim colLeft As Color = Color.Transparent
            Dim colRight As Color = Color.FromArgb(200, Color.Black)

            Dim gBrush As New LinearGradientBrush(New Point(0, 0), New Point(Width, 0), colLeft, colRight)
            formGraphics.FillRectangle(gBrush, ClientRectangle)

            Dim leftFormat As StringFormat = StringFormat.GenericDefault
            leftFormat.Alignment = StringAlignment.Near
            leftFormat.Trimming = StringTrimming.EllipsisWord

            formGraphics.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
            'Date
            formGraphics.DrawString([Date], New Font("Helvetica", 40.0F, FontStyle.Regular), Brushes.White, New RectangleF(20, Height - 100, Width - 40, 80), leftFormat)
            'Time
            formGraphics.DrawString(Time, New Font("Helvetica", 80.0F, FontStyle.Regular), Brushes.White, New RectangleF(20, Height - 210, Width - 40, 160), leftFormat)
            'Temperature
            formGraphics.DrawString(Temperature, New Font("Helvetica", 80.0F, FontStyle.Regular), Brushes.White, New RectangleF((Width / 2) + 160, 50, Width / 2 + 160, 160), leftFormat)
            'Location
            formGraphics.DrawString(GeoLocation, New Font("Helvetica", 30.0F, FontStyle.Regular), Brushes.White, New RectangleF(Width / 2, 170, Width / 2 + 160, 60), leftFormat)
            'Weather
            formGraphics.DrawString(Weather, New Font("Helvetica", 30.0F, FontStyle.Regular), Brushes.White, New RectangleF(Width / 2, 220, Width / 2 + 160, 60), leftFormat)
            'Wind
            formGraphics.DrawString(Wind, New Font("Helvetica", 15.0F, FontStyle.Regular), Brushes.White, New RectangleF(Width / 2, 270, Width / 2 + 160, 30), leftFormat)
            'Weather Icon
            formGraphics.DrawString(WeatherIcon, WeatherIconFont, Brushes.White, New RectangleF(Width / 2, 40, Width / 2, 160), leftFormat)
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Sub GetWeather()
        Temperature = $"{CurrentWeather.Temperature.Temperature_Celsius}°C"
        GeoLocation = CurrentWeather.DisplayLocation.FullLocation
        Weather = CurrentWeather.CurrentWeatherConditions
        Wind = CurrentWeather.Wind.WindDirectionAndSpeed_KPH_String

        Select Case CurrentWeather.WeatherIcon.IconText
            Case "mostlycloudy", "mostlysunny", "partlycloudy", "partlysunny"
                WeatherIcon = ""
            Case "tstorms", "chancetstorms"
                WeatherIcon = ""
            Case "chancerain", "rain"
                WeatherIcon = ""
            Case "chancesleet", "sleet"
                WeatherIcon = ""
            Case "chancesnow", "snow", "chanceflurries", "flurries"
                WeatherIcon = ""
            Case "clear", "sunny"
                WeatherIcon = ""
            Case "cloudy"
                WeatherIcon = ""
            Case "fog", "hazy"
                WeatherIcon = ""
            Case "nt_chanceflurries", "nt_chancesnow", "nt_flurries", "nt_snow"
                WeatherIcon = ""
            Case "nt_chancerain", "nt_rain"
                WeatherIcon = ""
            Case "nt_chancesleet", "nt_sleet"
                WeatherIcon = ""
            Case "nt_chancetstorms", "nt_tstorms"
                WeatherIcon = ""
            Case "nt_sunny", "nt_clear"
                WeatherIcon = ""
            Case "nt_cloudy"
                WeatherIcon = ""
            Case "nt_fog", "nt_hazy"
                WeatherIcon = ""
            Case "nt_mostlycloudy", "nt_mostlysunny", "nt_partlycloudy", "nt_partlysunny", "nt_partlycloudy"
                WeatherIcon = ""
            Case Else
                WeatherIcon = ""
        End Select
    End Sub

    Private Sub frmBackground_Load(sender As Object, e As EventArgs) Handles Me.Load
        Try
            Hide()
            CheckForIllegalCrossThreadCalls = False

            WeatherFont.AddFontFile($"{Application.StartupPath}\Data\Fonts\weathericons-regular-webfont.ttf")
            WeatherIconFont = New Font(WeatherFont.Families(0), 80)

            If IsPrimary Then
                For i As Integer = 0 To Screen.AllScreens.Count - 1
                    Dim bg(i) As frmScreenLock, s(i) As Screen
                    If Not i = Screen.AllScreens.Count - 1 Then
                        bg(i) = New frmScreenLock With {.IsPrimary = False}
                        s(i) = Screen.AllScreens(i)
                        bg(i).Tag = i
                        bg(i).Location = s(i).Bounds.Location + New Point(100, 100)
                        bg(i).WindowState = FormWindowState.Maximized
                        bg(i).BackgroundImage = Image.FromStream(New FileStream(GetRandomImageFilePath($"{Application.StartupPath}\Data\Images\Screen Lock"), FileMode.Open, FileAccess.Read))
                        bg(i).Show()
                        Backgrounds.Add(bg(i))
                    End If
                Next
            End If

            Dim Scr As Screen = Screen.FromControl(Me)
            If Scr.Primary Then
                Panel1.Show()
                Refresh()
            End If

            BackgroundImage = Image.FromStream(New FileStream(GetRandomImageFilePath($"{Application.StartupPath}\Data\Images\Screen Lock"), FileMode.Open, FileAccess.Read))

            WeatherThread = New Thread(AddressOf GetWeather)
            WeatherThread.Start()
            Show()
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
        End Try
    End Sub

    Private Function GetRandomImageFilePath(folder As String) As String
        Dim files() As String = Directory.GetFiles(folder, "*.jpg")
        Dim random As Random = New Random()
        Dim result As String = files(random.Next(0, files.Length - 1))
        Return result
    End Function

    Private Sub tTime_Tick(sender As Object, e As EventArgs) Handles tTime.Tick
        Time = TimeOfDay.ToString("hh:mm:ss tt")
        [Date] = Format(Now, "dddd, MMMM dd, yyyy")
        Refresh()
    End Sub

    Private Sub tBackgroundImage_Tick(sender As Object, e As EventArgs) Handles tBackgroundImage.Tick
        BackgroundImage = Image.FromStream(New FileStream(GetRandomImageFilePath($"{Application.StartupPath}\Data\Images\Screen Lock"), FileMode.Open, FileAccess.Read))
    End Sub

    Private Sub tWeather_Tick(sender As Object, e As EventArgs) Handles tWeather.Tick
        If Not WeatherThread.ThreadState = ThreadState.Running Then
            WeatherThread = New Thread(AddressOf GetWeather)
            WeatherThread.Start()
        End If
    End Sub

    Private Sub frmBackground_SizeChanged(sender As Object, e As EventArgs) Handles Me.SizeChanged
        Refresh()
    End Sub

    Private Sub frmBackground_Click(sender As Object, e As EventArgs) Handles Me.Click
        Dim Scr As Screen = Screen.FromControl(Me)
        If Panel1.Visible Then
            If Scr.Primary Then Panel1.Hide()
        Else
            If Scr.Primary Then
                Panel1.Show()
            End If
        End If
    End Sub

    Private Sub frmBackground_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        If IsPrimary Then
            For Each bg In Backgrounds
                bg.Close()
            Next
        End If
        frmMenu.WindowState = FormWindowState.Maximized
    End Sub
End Class