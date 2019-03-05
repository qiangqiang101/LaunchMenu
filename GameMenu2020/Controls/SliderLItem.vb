Imports System.ComponentModel
Imports System.Drawing.Drawing2D
Imports LaunchMenu

Public Class SliderLItem

    Public Property Caption() As String
        Get
            Return Tint.Title
        End Get
        Set(value As String)
            Tint.Title = value
        End Set
    End Property

    Public Property SubCaption() As String
        Get
            Return Tint.Subtitle
        End Get
        Set(value As String)
            Tint.Subtitle = value
        End Set
    End Property

    Public Property PublishDate() As String
        Get
            Return Tint.PubDate
        End Get
        Set(value As String)
            Tint.PubDate = value
        End Set
    End Property

    Public Property FeedURL As String
    Public Property FeedSubtitle As String
    Public Property FeedDate As String

    Private Sub Controls_Click(sender As Object, e As EventArgs) Handles Tint.Click
        OnClick(EventArgs.Empty)
    End Sub

    Private Sub SliderLItem_BackgroundImageChanged(sender As Object, e As EventArgs) Handles Me.BackgroundImageChanged
        bwEnter.RunWorkerAsync()
    End Sub

    Private Sub bwEnter_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwEnter.DoWork
        CheckForIllegalCrossThreadCalls = False

        Dim i As Integer = 200
        Do Until i = 0
            If Tint.InvokeRequired Then
                Tint.Invoke(Sub()
                                Tint.BackColor = Color.FromArgb(i, Tint.BackColor)
                                Tint.Update()
                            End Sub)
            Else
                Tint.BackColor = Color.FromArgb(i, Tint.BackColor)
                Tint.Update()
            End If
            i -= 10
        Loop
    End Sub
End Class
