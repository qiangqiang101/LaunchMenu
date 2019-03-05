<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class slLock
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btmCancel = New LaunchMenu.MDButton()
        Me.btnStart = New LaunchMenu.MDButton()
        Me.txtPwd = New LaunchMenu.MDTextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtConfirm = New LaunchMenu.MDTextBox()
        Me.tbOpacity = New LaunchMenu.MDTrackBar()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btmCancel
        '
        Me.btmCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btmCancel.BaseColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(84, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.btmCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btmCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btmCancel.Location = New System.Drawing.Point(172, 105)
        Me.btmCancel.Name = "btmCancel"
        Me.btmCancel.Rounded = True
        Me.btmCancel.Size = New System.Drawing.Size(140, 44)
        Me.btmCancel.TabIndex = 5
        Me.btmCancel.Text = "Cancel"
        Me.btmCancel.TextColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnStart.BaseColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.btnStart.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStart.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnStart.Location = New System.Drawing.Point(26, 105)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Rounded = True
        Me.btnStart.Size = New System.Drawing.Size(140, 44)
        Me.btnStart.TabIndex = 4
        Me.btnStart.Text = "Lock"
        Me.btnStart.TextColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        '
        'txtPwd
        '
        Me.txtPwd.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.txtPwd.EnterPressed = False
        Me.txtPwd.LightTheme = False
        Me.txtPwd.Location = New System.Drawing.Point(108, 6)
        Me.txtPwd.MaxLength = 32767
        Me.txtPwd.Multiline = False
        Me.txtPwd.Name = "txtPwd"
        Me.txtPwd.Placeholder = ""
        Me.txtPwd.ReadOnly = False
        Me.txtPwd.RightImage = Nothing
        Me.txtPwd.RightImageSize = New System.Drawing.Size(17, 17)
        Me.txtPwd.Size = New System.Drawing.Size(215, 29)
        Me.txtPwd.TabIndex = 1
        Me.txtPwd.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtPwd.TextColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtPwd.UseSystemPasswordChar = True
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(4, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(98, 15)
        Me.Label3.TabIndex = 14
        Me.Label3.Text = "Transparency (%)"
        '
        'txtConfirm
        '
        Me.txtConfirm.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.txtConfirm.EnterPressed = False
        Me.txtConfirm.LightTheme = False
        Me.txtConfirm.Location = New System.Drawing.Point(108, 41)
        Me.txtConfirm.MaxLength = 32767
        Me.txtConfirm.Multiline = False
        Me.txtConfirm.Name = "txtConfirm"
        Me.txtConfirm.Placeholder = ""
        Me.txtConfirm.ReadOnly = False
        Me.txtConfirm.RightImage = Nothing
        Me.txtConfirm.RightImageSize = New System.Drawing.Size(17, 17)
        Me.txtConfirm.Size = New System.Drawing.Size(215, 29)
        Me.txtConfirm.TabIndex = 2
        Me.txtConfirm.TextAlign = System.Windows.Forms.HorizontalAlignment.Left
        Me.txtConfirm.TextColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer), CType(CType(200, Byte), Integer))
        Me.txtConfirm.UseSystemPasswordChar = True
        '
        'tbOpacity
        '
        Me.tbOpacity.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.tbOpacity.HatchColor = System.Drawing.Color.FromArgb(CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer), CType(CType(50, Byte), Integer))
        Me.tbOpacity.LightTheme = False
        Me.tbOpacity.Location = New System.Drawing.Point(108, 76)
        Me.tbOpacity.Maximum = 100
        Me.tbOpacity.Minimum = 0
        Me.tbOpacity.Name = "tbOpacity"
        Me.tbOpacity.ShowValue = True
        Me.tbOpacity.Size = New System.Drawing.Size(215, 23)
        Me.tbOpacity.Style = LaunchMenu.MDTrackBar._Style.Knob
        Me.tbOpacity.TabIndex = 3
        Me.tbOpacity.Text = "Transparency"
        Me.tbOpacity.TrackColor = System.Drawing.Color.FromArgb(CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer), CType(CType(100, Byte), Integer))
        Me.tbOpacity.Value = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(4, 13)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 15)
        Me.Label1.TabIndex = 10
        Me.Label1.Text = "Password"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(4, 48)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 15)
        Me.Label2.TabIndex = 12
        Me.Label2.Text = "Confirm"
        '
        'slLock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Controls.Add(Me.btmCancel)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.txtPwd)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtConfirm)
        Me.Controls.Add(Me.tbOpacity)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.Label2)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "slLock"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Size = New System.Drawing.Size(328, 155)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btmCancel As MDButton
    Friend WithEvents btnStart As MDButton
    Friend WithEvents txtPwd As MDTextBox
    Friend WithEvents Label3 As Label
    Friend WithEvents txtConfirm As MDTextBox
    Friend WithEvents tbOpacity As MDTrackBar
    Friend WithEvents Label1 As Label
    Friend WithEvents Label2 As Label
End Class
