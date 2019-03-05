Public Class slLock

    Private Sub btnStart_Click(sender As Object, e As EventArgs) Handles btnStart.Click
        Try
            If txtPwd.Text <> txtConfirm.Text Then
                MsgBox("The Password do not match, please re-enter password in both fields.", MsgBoxStyle.Critical, "Error")
                txtPwd.Clear()
                txtConfirm.Clear()
                txtPwd.Focus()
            ElseIf txtPwd.Text = Nothing Then
                MsgBox("Please enter password.", MsgBoxStyle.Critical, "Error")
                txtPwd.Focus()
            Else
                Dim fb As frmScreenLock = ParentForm
                'Todo
                Dim slU As New slUnlock
                fb.Panel1.Controls.Add(slU)
                slU.Location = Me.Location
                fb.Password = txtPwd.Text
                fb.ControlBox = False
                fb.Panel1.Controls.Remove(Me)
            End If
        Catch ex As Exception
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub tbOpacity_Scroll(sender As Object) Handles tbOpacity.Scroll
        Try
            If tbOpacity.Value > 80 Then
                tbOpacity.Value = 80
            End If
            Dim fb As frmScreenLock = ParentForm
            For Each bg As frmScreenLock In fb.Backgrounds
                bg.Opacity = Val(tbOpacity.Maximum - tbOpacity.Value + 1) / 100
            Next
            fb.Opacity = Val(tbOpacity.Maximum - tbOpacity.Value + 1) / 100
        Catch ex As Exception
            MsgBox($"{ex.Message} {ex.StackTrace}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub btmCancel_Click(sender As Object, e As EventArgs) Handles btmCancel.Click
        ParentForm.Close()
        frmMenu.WindowState = FormWindowState.Maximized
    End Sub
End Class
