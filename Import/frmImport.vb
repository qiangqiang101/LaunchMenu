Imports System.Data.OleDb
Imports System.IO

Public Class frmImport

    Dim xConn As sqlConn
    Private xmlPath As String = $"{Application.StartupPath}\Apps"
    Private _gameList As New List(Of GameList)

    Private Sub btnMdb_Click(sender As Object, e As EventArgs) Handles btnMdb.Click
        Try
            Dim ofd = New OpenFileDialog
            ofd.Title = "Select Game..."
            ofd.RestoreDirectory = True
            ofd.Filter = "Mdb File (.mdb)|*.mdb"
            ofd.FilterIndex = 1
            ofd.Multiselect = False

            If ofd.ShowDialog() <> DialogResult.Cancel Then
                cmbOld.Items.Clear()
                cmbNew.Items.Clear()
                lvOld.Items.Clear()
                lvNew.Items.Clear()
                txtMdb.Text = ofd.FileName

                'Read in lvOld
                ReadDatabase()
            End If
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub ReadDatabase()
        Try
            Dim oldItems() As String = {"Lan Game", "Online Game", "Mini Game", "Office App", "Internet App", "Tool App", "Menu App"}
            cmbOld.Items.AddRange(oldItems)

            _gameList.Clear()
            If Directory.Exists(xmlPath) Then
                For Each cat In Directory.GetFiles(xmlPath, "*.xml")
                    Dim t As New GameList(cat)
                    Dim gl As GameList = t.Instance

                    If Not gl.Type = CategoryType.Seperator Then
                        gl.FileName = cat
                        _gameList.Add(gl)
                        cmbNew.Items.Add(gl.Category)
                    End If
                Next
            End If

            If cmbOld.Items.Count > 0 Then cmbOld.SelectedIndex = 0
            If cmbNew.Items.Count > 0 Then cmbNew.SelectedIndex = 0
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub cmbOld_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbOld.SelectedIndexChanged
        Dim TableName As String = Nothing
        Select Case cmbOld.SelectedItem
            Case "Lan Game"
                TableName = "TblLanGame"
            Case "Online Game"
                TableName = "TblOnlineGame"
            Case "Mini Game"
                TableName = "TblMiniGame"
            Case "Office App"
                TableName = "TblOffice"
            Case "Internet App"
                TableName = "TblInternet"
            Case "Tool App"
                TableName = "TblTool"
            Case "Menu App"
                TableName = "TblMenu"
        End Select

        If Not TableName = Nothing Then
            lvOld.Items.Clear()

            Try
                xConn = New sqlConn
                xConn.connectMe($"SELECT * FROM {TableName};", txtMdb.Text)

                xConn.OLEComm.Connection = xConn.OLEConn

                Dim d As OleDbDataReader = xConn.OLEComm.ExecuteReader()
                Do While d.Read
                    lvOld.Items.Add(New ListViewItem(d("Game_Text").ToString))
                Loop

                xConn.OLEConn.Close()
            Catch ex As Exception
                Logger.Log($"{ex.Message} {ex.StackTrace}")
                MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
            End Try
        End If
    End Sub

    Private Sub cmbNew_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cmbNew.SelectedIndexChanged
        Try
            Dim gl As GameList = _gameList.Find(Function(x) x.Category = cmbNew.SelectedItem)

            lvNew.Items.Clear()

            For Each game As Game In gl.Games
                lvNew.Items.Add(New ListViewItem(game.Name) With {.Tag = game})
            Next
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btnRight_Click(sender As Object, e As EventArgs) Handles btnRight.Click
        If lvOld.SelectedItems.Count = 0 Then Exit Sub

        Dim TableName As String = Nothing
        Select Case cmbOld.SelectedItem
            Case "Lan Game"
                TableName = "TblLanGame"
            Case "Online Game"
                TableName = "TblOnlineGame"
            Case "Mini Game"
                TableName = "TblMiniGame"
            Case "Office App"
                TableName = "TblOffice"
            Case "Internet App"
                TableName = "TblInternet"
            Case "Tool App"
                TableName = "TblTool"
            Case "Menu App"
                TableName = "TblMenu"
            Case Else
                Exit Sub
        End Select

        For Each item As ListViewItem In lvOld.SelectedItems
            Try
                xConn = New sqlConn()
                xConn.connectMe($"SELECT * FROM {TableName} WHERE Game_Text ='{item.Text}';", txtMdb.Text)

                Dim path, startin, idate, web, desc, image, gerne, pub, dev, rate As String

                xConn.OLEComm.Connection = xConn.OLEConn

                Dim d As OleDbDataReader = xConn.OLEComm.ExecuteReader()
                Do While d.Read
                    path = d("Game_Path").ToString
                    startin = IO.Path.GetDirectoryName(path)
                    idate = Date.Parse(d("Game_Install_Time").ToString).ToShortDateString
                    web = d("Game_Reg").ToString
                    desc = item.Text
                    Dim picStr As String = $"{IO.Path.GetDirectoryName(txtMdb.Text)}\Pictures\{item.Text}.jpg"
                    If File.Exists(picStr) Then
                        image = SafeImageFromFile(picStr).ImageToBase64
                    Else
                        image = d("Game_Icon").ToString.GetFileIconAsImage().ImageToBase64
                    End If
                    gerne = Nothing
                    pub = Nothing
                    dev = Nothing
                    rate = "Rating Pending"
                Loop

                xConn.OLEConn.Close()

                Dim game As New Game(item.Text, path, startin, idate, web, desc, gerne, pub, dev, rate, image)
                lvNew.Items.Add(New ListViewItem(item.Text) With {.Tag = Game})
            Catch ex As Exception
                Logger.Log($"{ex.Message} {ex.StackTrace}")
                MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
            End Try
        Next
    End Sub

    Private Sub btnRightall_Click(sender As Object, e As EventArgs) Handles btnRightall.Click
        Dim TableName As String = Nothing
        Select Case cmbOld.SelectedItem
            Case "Lan Game"
                TableName = "TblLanGame"
            Case "Online Game"
                TableName = "TblOnlineGame"
            Case "Mini Game"
                TableName = "TblMiniGame"
            Case "Office App"
                TableName = "TblOffice"
            Case "Internet App"
                TableName = "TblInternet"
            Case "Tool App"
                TableName = "TblTool"
            Case "Menu App"
                TableName = "TblMenu"
            Case Else
                Exit Sub
        End Select

        For Each item As ListViewItem In lvOld.Items
            Try
                xConn = New sqlConn()
                xConn.connectMe($"SELECT * FROM {TableName} WHERE Game_Text ='{item.Text}';", txtMdb.Text)

                Dim path, startin, idate, web, desc, image, gerne, pub, dev, rate As String

                xConn.OLEComm.Connection = xConn.OLEConn

                Dim d As OleDbDataReader = xConn.OLEComm.ExecuteReader()
                Do While d.Read
                    path = d("Game_Path").ToString
                    startin = IO.Path.GetDirectoryName(path)
                    idate = Date.Parse(d("Game_Install_Time").ToString).ToShortDateString
                    web = d("Game_Reg").ToString
                    desc = item.Text
                    Dim picStr As String = $"{IO.Path.GetDirectoryName(txtMdb.Text)}\Pictures\{item.Text}.jpg"
                    If File.Exists(picStr) Then
                        image = SafeImageFromFile(picStr).ImageToBase64
                    Else
                        image = d("Game_Icon").ToString.GetFileIconAsImage().ImageToBase64
                    End If
                    gerne = Nothing
                    pub = Nothing
                    dev = Nothing
                    rate = "Rating Pending"
                Loop

                xConn.OLEConn.Close()

                Dim game As New Game(item.Text, path, startin, idate, web, desc, gerne, pub, dev, rate, image)
                lvNew.Items.Add(New ListViewItem(item.Text) With {.Tag = game})
            Catch ex As Exception
                Logger.Log($"{ex.Message} {ex.StackTrace}")
                MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
            End Try
        Next
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Try
            Dim temp As GameList = _gameList.Find(Function(x) x.Category = cmbNew.SelectedItem)

            Dim gl = temp.Instance
            gl.Games.Clear()

            For Each item As ListViewItem In lvNew.Items
                gl.Games.Add(CType(item.Tag, Game))
            Next

            gl.FileName = temp.FileName
            gl.Save()
            MsgBox($"{cmbNew.SelectedItem} Save Completed.", MsgBoxStyle.Information, "LaunchManager")
        Catch ex As Exception
            Logger.Log($"{ex.Message} {ex.StackTrace}")
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub
End Class
