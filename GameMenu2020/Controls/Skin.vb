Public Class Skin

    Dim drag As Boolean
    Dim mousex As Integer
    Dim mousey As Integer

    Public Sub New()
        InitializeComponent()
    End Sub

    Private Sub Skin_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Anchor = (AnchorStyles.Top Or AnchorStyles.Right)
        Location = New Point(ParentForm.Width - Width, 0)
    End Sub

    Private Sub ControlButton_MouseEnter(sender As Object, e As EventArgs) Handles lblMinimize.MouseEnter, lblMaximize.MouseEnter, lblClose.MouseEnter
        sender.ForeColor = Color.White
        If sender Is lblClose Then
            sender.BackColor = Color.FromArgb(240, 71, 71)
        Else
            sender.BackColor = Color.FromArgb(43, 45, 48)
        End If
    End Sub

    Private Sub ControlButton_MouseLeave(sender As Object, e As EventArgs) Handles lblMinimize.MouseLeave, lblMaximize.MouseLeave, lblClose.MouseLeave
        sender.ForeColor = Color.FromArgb(165, 166, 167)
        sender.BackColor = sender.Parent.BackColor
    End Sub

    Private Sub lblClose_Click(sender As Object, e As EventArgs) Handles lblClose.Click
        ParentForm.Close()
    End Sub

    Private Sub lblMinimize_Click(sender As Object, e As EventArgs) Handles lblMinimize.Click
        ParentForm.WindowState = FormWindowState.Minimized
    End Sub

    Private Sub lblMaximize_Click(sender As Object, e As EventArgs) Handles lblMaximize.Click
        Select Case ParentForm.WindowState
            Case FormWindowState.Normal
                ParentForm.WindowState = FormWindowState.Maximized
            Case FormWindowState.Maximized
                ParentForm.WindowState = FormWindowState.Normal
        End Select
    End Sub

    Private Sub Skin_Paint(sender As Object, e As PaintEventArgs) Handles Me.Paint
        lblMaximize.Visible = ParentForm.MaximizeBox
        lblMinimize.Visible = ParentForm.MinimizeBox
        lblClose.Visible = ParentForm.ControlBox
        Select Case ParentForm.WindowState
            Case FormWindowState.Normal
                lblMaximize.Text = "1"
            Case FormWindowState.Maximized
                lblMaximize.Text = "2"
        End Select
    End Sub

    Private Sub Skin_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        drag = True
        mousex = Cursor.Position.X - Me.ParentForm.Left
        mousey = Cursor.Position.Y - Me.ParentForm.Top
    End Sub

    Private Sub Skin_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If drag Then
            Me.ParentForm.Top = Cursor.Position.Y - mousey
            Me.ParentForm.Left = Cursor.Position.X - mousex
        End If
    End Sub

    Private Sub Skin_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        drag = False
    End Sub
End Class
