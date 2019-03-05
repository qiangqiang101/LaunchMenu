Public Class SliderSItem

    Public Property Caption() As String
        Get
            Return Title.Text
        End Get
        Set(value As String)
            Title.Text = value
        End Set
    End Property

    Public Property Opacity() As Integer
        Get
            Return Tint.BackColor.A
        End Get
        Set(value As Integer)
            Tint.BackColor = Color.FromArgb(value, Tint.BackColor)
        End Set
    End Property

    Private _Selected As Boolean
    Public Property Selected As Boolean
        Get
            Return _Selected
        End Get
        Set(value As Boolean)
            If _Selected <> value Then
                _Selected = value

                OnSelectedChanged(EventArgs.Empty)
            End If
        End Set
    End Property

    Public Event SelectedChanged As EventHandler

    Protected Overridable Sub OnSelectedChanged(ByVal e As EventArgs)
        RaiseEvent SelectedChanged(Me, e)
    End Sub

    Public Property FeedURL As String
    Public Property FeedSubtitle As String
    Public Property FeedDate As String

    Private Sub pFeedTitle_MouseEnter(sender As Object, e As EventArgs) Handles Title.MouseEnter
        If Not Selected Then bwEnter.RunWorkerAsync()
    End Sub

    Private Sub pFeedTitle_MouseLeave(sender As Object, e As EventArgs) Handles Title.MouseLeave
        If Not Selected Then bwLeave.RunWorkerAsync()
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

    Private Sub bwLeave_DoWork(sender As Object, e As System.ComponentModel.DoWorkEventArgs) Handles bwLeave.DoWork
        CheckForIllegalCrossThreadCalls = False

        Dim i As Integer = 0
        Do Until i = 200
            If Tint.InvokeRequired Then
                Tint.Invoke(Sub()
                                Tint.BackColor = Color.FromArgb(i, Tint.BackColor)
                                Tint.Update()
                            End Sub)
            Else
                Tint.BackColor = Color.FromArgb(i, Tint.BackColor)
                Tint.Update()
            End If
            i += 10
        Loop
    End Sub

    Private Sub SliderSItem_SelectedChanged(sender As Object, e As EventArgs) Handles Me.SelectedChanged
        If _Selected Then
            Tint.BackColor = Color.FromArgb(0, Tint.BackColor)
        Else
            bwLeave.RunWorkerAsync()
        End If
    End Sub

    Private Sub Title_Click(sender As Object, e As EventArgs) Handles Title.Click
        OnClick(EventArgs.Empty)
    End Sub
End Class
