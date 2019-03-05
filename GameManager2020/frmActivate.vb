Imports System.IO

Public Class frmActivate

    Private trialFile As String = $"{Application.StartupPath}\Data\Trial.xml"
    Private CurrentTrial As Trial

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        Select Case True
            Case rbPurchase.Checked
                Process.Start("http://zettabytetek.com/product-category/software")
                Close()
            Case rbActivate.Checked
                frmRegister.Show()
                Close()
            Case rbTry.Checked
                If Not File.Exists(trialFile) Then
                    Dim temp As New Trial(trialFile)
                    temp.TrialFileName = trialFile
                    temp.StartDate = Encrypt(Now2.ToShortDateString)
                    temp.Save()
                    File.SetAttributes(trialFile, FileAttributes.Hidden)
                End If

                frmMain.skipCheck = True
                frmMain.Show()
                Close()
        End Select
    End Sub

    Private Sub BtnCancel_Click(sender As Object, e As EventArgs) Handles BtnCancel.Click
        Close()
    End Sub

    Private Sub frmActivate_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        If File.Exists(trialFile) Then
            Dim temp As New Trial(trialFile)
            CurrentTrial = temp.Instance

            Dim span = Date.Parse(Decrypt(CurrentTrial.StartDate)) - Now2()
            If span.TotalDays > -30 Then
                rbTry.Enabled = True
            Else
                rbTry.Enabled = False
            End If
        End If
    End Sub

    Private Function Encrypt(str As String) As String
        Return Convert.ToBase64String(System.Text.Encoding.Unicode.GetBytes(str))
    End Function

    Private Function Decrypt(str As String) As String
        Return System.Text.Encoding.Unicode.GetString(Convert.FromBase64String(str))
    End Function

    Public Function DetectClockManipulation(thresholdTime As Date) As Boolean
        Dim adjustedThresholdTime As Date = New Date(thresholdTime.Year, thresholdTime.Month, thresholdTime.Day, 23, 59, 59)
        Dim eventLog As EventLog = New EventLog("system")
        For Each entry As EventLogEntry In eventLog.Entries
            If (entry.TimeWritten > adjustedThresholdTime) Then
                Return True
            End If
        Next
        Return False
    End Function
End Class