Imports System.Drawing.Imaging
Imports System.IO
Imports System.Management
Imports System.Net
Imports System.Runtime.CompilerServices
Imports System.Security.Principal
Imports TsudaKageyu

Module Helper

    <Extension>
    Public Function ToName(type As CategoryType) As String
        Return [Enum].GetName(GetType(CategoryType), type)
    End Function

    <Extension>
    Public Function ToName(type As CategoryIcon) As String
        Return [Enum].GetName(GetType(CategoryIcon), type)
    End Function

    <Extension>
    Public Sub Clear(tb As XylosTextBox)
        tb.Text = Nothing
    End Sub

    <Extension>
    Public Sub Sort(listview As ListView, column As ColumnHeader)
        listview.ListViewItemSorter = New ListViewItemComparer(column.Index, listview.Sorting)
        listview.Sort()
    End Sub

    Public Function SafeImageFromFile(path As String) As Image
        If File.Exists(path) Then
            Using fs As New FileStream(path, FileMode.Open, FileAccess.Read)
                Dim img = Image.FromStream(fs)
                Return img
            End Using
        Else
            Using fs As New FileStream(Application.StartupPath & "\Data\Images\NO_IMAGE.jpg", FileMode.Open, FileAccess.Read)
                Dim img = Image.FromStream(fs)
                Return img
            End Using
        End If
    End Function

    <Extension>
    Public Function Base64ToImage(Image As String) As Image
        Dim b64 As String = Image.Replace(" ", "+")
        Dim bite() As Byte = Convert.FromBase64String(b64)
        Dim stream As New MemoryStream(bite)
        Return Drawing.Image.FromStream(stream)
    End Function

    <Extension>
    Public Function ImageToBase64(img As Image) As String
        Dim stream As New MemoryStream
        Dim bmp As Bitmap = New Bitmap(img)
        bmp.Save(stream, ImageFormat.Png)
        Return Convert.ToBase64String(stream.ToArray)
    End Function

    Public Function GenerateImage(firstLetter As String, bgcolor As Color, forecolor As Color, size As Size, Optional isSquare As Boolean = False) As Image
        Dim bmp As New Bitmap(size.Width, size.Height)
        Dim g As Graphics = Graphics.FromImage(bmp)
        Using brush As New SolidBrush(bgcolor)
            g.FillRectangle(brush, 0, 0, size.Width, size.Height)
        End Using
        g.TextRenderingHint = Drawing.Text.TextRenderingHint.AntiAlias
        Dim format As New StringFormat()
        format.Alignment = StringAlignment.Center
        If isSquare Then
            g.DrawString(firstLetter, New Font("Segoe UI", 150.0F, FontStyle.Bold), New SolidBrush(forecolor), New RectangleF(0, -20, size.Width, size.Height), format)
        Else
            g.DrawString(firstLetter, New Font("Segoe UI", 400.0F, FontStyle.Bold), New SolidBrush(forecolor), New RectangleF(0, 0, size.Width, size.Height), format)
        End If

        Return DirectCast(bmp, Image)
    End Function

    <Extension>
    Public Function ResizeImage(original As Image) As Image
        Return ResizeBitmap(original, 100, 0)
    End Function

    Private Function ResizeBitmap(bmp As Bitmap, newWidth As Integer, newHeight As Integer) As Bitmap
        Dim ratio As Double = bmp.Width / bmp.Height
        Dim nSize As Size = bmp.Size
        If newWidth > 0 Then
            nSize = New Size(newWidth, CInt(newWidth / ratio))
        ElseIf newHeight > 0 Then
            nSize = New Size(CInt(newHeight * ratio), newHeight)
        End If
        Dim b As New Bitmap(nSize.Width, nSize.Height)
        Using g As Graphics = Graphics.FromImage(b)
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(bmp, 0, 0, nSize.Width, nSize.Height)
        End Using
        Return b
    End Function

    Public Function GetFileIconAsImage(file As String, Optional index As Integer = 0) As Image
        Try
            Dim icon As Icon = Nothing
            Dim splitIcons As Icon() = Nothing


            Dim extractor = New IconExtractor(file)
            icon = extractor.GetIcon(index)
            splitIcons = IconUtil.Split(icon)

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

            Return DirectCast(IconUtil.ToBitmap(ikon), Image)
        Catch ex As Exception
            If index >= 20 Then
                Return Nothing
            Else
                Return GetFileIconAsImage(file, index + 1)
            End If
        End Try
    End Function

    Public Function IsFileOwner(file As String) As Boolean
        Dim fs = IO.File.GetAccessControl(file)
        Dim sid = fs.GetOwner(GetType(SecurityIdentifier))
        Dim ntAcc = sid.Translate(GetType(NTAccount))
        Dim user = WindowsIdentity.GetCurrent()
        If ntAcc.Value = user.Name Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Function getHWID() As String
        Dim Trim As String = MD5CalcString(GetProcessorId() + "--" + GetVolumeSerial() + "--" + GetMotherBoardID())
        Return Trim.Substring(0, Trim.Length - 16)
    End Function

    Private Function MD5CalcString(ByVal strData As String) As String
        Dim objMD5 As New System.Security.Cryptography.MD5CryptoServiceProvider
        Dim arrData() As Byte
        Dim arrHash() As Byte
        arrData = System.Text.Encoding.UTF8.GetBytes(strData)
        arrHash = objMD5.ComputeHash(arrData)
        objMD5 = Nothing
        Return ByteArrayToString(arrHash)
    End Function

    Private Function ByteArrayToString(ByVal arrInput() As Byte) As String
        Dim strOutput As New System.Text.StringBuilder(arrInput.Length)
        For i As Integer = 0 To arrInput.Length - 1
            strOutput.Append(arrInput(i).ToString("X2"))
        Next
        Return strOutput.ToString().ToLower
    End Function

    Private Function GetProcessorId() As String
        Dim strProcessorId As String = String.Empty
        Dim query As New SelectQuery("Win32_processor")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            strProcessorId = info("processorId").ToString()
        Next
        Return strProcessorId
    End Function

    Private Function GetVolumeSerial(Optional ByVal strDriveLetter As String = "C") As String
        Dim disk As ManagementObject = New ManagementObject(String.Format("win32_logicaldisk.deviceid=""{0}:""", strDriveLetter))
        disk.Get()
        Return disk("VolumeSerialNumber").ToString()
    End Function

    Private Function GetMotherBoardID() As String
        Dim strMotherBoardID As String = String.Empty
        Dim query As New SelectQuery("Win32_BaseBoard")
        Dim search As New ManagementObjectSearcher(query)
        Dim info As ManagementObject
        For Each info In search.Get()
            strMotherBoardID = info("SerialNumber").ToString()
        Next
        Return strMotherBoardID
    End Function

    Public Function CheckForInternetConnection() As Boolean
        Try
            Using client = New WebClient()
                Using stream = client.OpenRead("http://zettabytetek.com/")
                    Return True
                End Using
            End Using
        Catch
            Return False
        End Try
    End Function

    <Extension>
    Public Function CheckActivation(userHWID As String) As Boolean
        Dim result As Boolean = False
        Try
            Dim canLogin As Boolean = False
            Dim wClient As New WebClient()
            Dim strSource As String = wClient.DownloadString("http://zettabytetek.com/software/ELS/" + "index.php?a=canuse&hwid=" + userHWID + "&product=" + "LaunchMenu")
            If strSource.Contains("TRUE") Then
                strSource = wClient.DownloadString("http://zettabytetek.com/software/ELS/" + "index.php?a=checkdate&hwid=" + userHWID)
                If strSource.Contains("LaunchMenu") Then
                    ' Check the date
                    Dim start As Integer = strSource.IndexOf("LaunchMenu") + "LaunchMenu".Length
                    Dim [end] As Integer = strSource.IndexOf(":" + "LaunchMenu")
                    Dim [date] As DateTime = Convert.ToDateTime(strSource.Substring(start, [end] - start))

                    strSource = wClient.DownloadString("http://weltzeit4u.com/Datum/index.php")
                    ' Get the current date
                    Dim start2 As Integer = strSource.IndexOf("<span id='gross_fett_blau'>") + 27
                    Dim end2 As Integer = strSource.IndexOf("</span> (arabische")
                    Dim dateToday As String = strSource.Substring(start2, end2 - start2) + " 00:00:00"
                    If [date] < Convert.ToDateTime(dateToday) Then
                        result = True
                    Else
                        canLogin = True
                    End If
                    If canLogin Then
                        result = True
                    End If
                End If
            Else
                result = False
            End If
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Critical, "Error")
            Logger.Log(ex.Message & ex.StackTrace)
        End Try
        Return result
    End Function

    Public Function Now2() As Date
        Dim wClient As New WebClient()
        Dim strSource = wClient.DownloadString("http://weltzeit4u.com/Datum/index.php")
        ' Get the current date
        Dim start2 As Integer = strSource.IndexOf("<span id='gross_fett_blau'>") + 27
        Dim end2 As Integer = strSource.IndexOf("</span> (arabische")
        Dim dateToday As String = strSource.Substring(start2, end2 - start2) + " 00:00:00"
        Return Convert.ToDateTime(dateToday)
    End Function

End Module
