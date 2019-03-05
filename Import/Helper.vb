Imports System.Drawing.Imaging
Imports System.IO
Imports System.Runtime.CompilerServices
Imports TsudaKageyu

Module Helper

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

End Module
