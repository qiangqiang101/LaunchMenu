Public Class SmoothAutoScrollFlowLayoutPanel
    Inherits FlowLayoutPanel

    Public Sub New()
        DoubleBuffered = True

        SetStyle(ControlStyles.UserPaint, True)
        SetStyle(ControlStyles.AllPaintingInWmPaint, True)
        SetStyle(ControlStyles.OptimizedDoubleBuffer, True)
    End Sub

    Protected Overrides Sub OnScroll(se As ScrollEventArgs)
        MyBase.OnScroll(se)
    End Sub

    Protected Overrides ReadOnly Property CreateParams As CreateParams
        Get
            Dim cp As CreateParams = MyBase.CreateParams
            cp.ExStyle = cp.ExStyle Or &H2000000
            Return cp
        End Get
    End Property

    Public Property ParentForm() As Form

    Protected Overrides Sub OnResize(eventargs As EventArgs)
        MyBase.OnResize(eventargs)

        If Not ParentForm Is Nothing Then
            If ParentForm.WindowState = FormWindowState.Minimized Then AutoScroll = False
            If ParentForm.WindowState = FormWindowState.Normal Then AutoScroll = True
            If ParentForm.WindowState = FormWindowState.Maximized Then AutoScroll = True
        End If
    End Sub

End Class
