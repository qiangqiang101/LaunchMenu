Imports System.Collections
Imports System.IO
Imports System.Net
Imports System.Text
Imports System.Threading

Public Class Form1
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim thread As New Thread(Sub()
                                     For Each line In TextBox1.Text.Split(vbNewLine)
                                         Dim startswith As String = "/assets/images/"
                                         Dim endswith As String = "_thumb.jpg"
                                         If line.Contains(startswith) AndAlso line.Contains(endswith) Then
                                             Dim l0 = line '.ToLower
                                             Dim l1 = l0.Substring(l0.IndexOf(startswith))
                                             Dim l2 = InStr(l1, endswith)
                                             Dim l3 = l1.Substring(0, l2 - 1)
                                             Dim l4 = "https://www.joblo.com" & l3 & endswith
                                             Dim l5 = l4.Replace("_thumb", "")
                                             ListBox1.Items.Add(l5)
                                         End If
                                     Next
                                     ProgressBar1.Maximum = ListBox1.Items.Count
                                     For Each item As String In ListBox1.Items
                                         Try
                                             Dim uri As Uri = New Uri(item)
                                             Dim filename As String = Path.GetFileName(uri.LocalPath)
                                             filename = filename.Replace("-", " ").Replace("thumb", "").Replace("_", " ").Replace(".jpg", "")
                                             filename = Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(filename)
                                             Dim wc As New WebClient
                                             wc.DownloadFile(item, "C:\Users\Bartholomew\Desktop\moviecover\" & filename & ".jpg")
                                             wc.Dispose()
                                             ProgressBar1.Value += 1
                                         Catch ex As Exception
                                             MsgBox(ex.Message)
                                         End Try
                                     Next
                                 End Sub)
        thread.Start()

    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        CheckForIllegalCrossThreadCalls = False
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Dim line As String = Nothing
        For Each item In TagsInputTextbox1.Items
            line &= item.Text & " "
        Next
        MsgBox(line)
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        MsgBox(TagsInputTextbox1.Text)
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        'Using client As New WebClient
        '    Dim reqparam As New Specialized.NameValueCollection
        '    reqparam.Add("user-key", "43a3a51e791e9f712af56d02fda04a9a")
        '    reqparam.Add("Accept", "application/json")
        '    Dim responsebytes = client.UploadValues("https://api-v3.igdb.com/screenshots", "POST", reqparam)
        '    Dim responsebody = (New Text.UTF8Encoding).GetString(responsebytes)
        'End Using

        Dim request As WebRequest = WebRequest.Create("https://api-v3.igdb.com/screenshots")
        request.Credentials = CredentialCache.DefaultCredentials
        request.Headers.Add("user-key", "43a3a51e791e9f712af56d02fda04a9a")
        request.Headers.Add(HttpRequestHeader.Accept, "application/json")
        CType(request, HttpWebRequest).UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/60.0.3112.113 Safari/537.36"
        request.Method = "POST"
        Dim postData As String = "fields alpha_channel,animated,game,height,image_id,url,width;"
        Dim byteArray As Byte() = Encoding.UTF8.GetBytes(postdata)
        request.ContentLength = byteArray.Length
        request.ContentType = "application/x-www-form-urlencoded"
        Dim dataStream As Stream = request.GetRequestStream()
        dataStream.Write(byteArray, 0, byteArray.Length)
        dataStream.Close()
        Dim response As WebResponse = request.GetResponse()
        Dim statusDesc As String = CType(response, HttpWebResponse).StatusDescription
        MsgBox(statusDesc)
        dataStream = response.GetResponseStream
        Dim reader As New StreamReader(dataStream)
        Dim responseFromServer As String = reader.ReadToEnd
        MsgBox(responseFromServer)
        reader.Close()
        dataStream.Close()
        response.Close()
    End Sub
End Class