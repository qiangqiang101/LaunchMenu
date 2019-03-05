<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class slUnlock
    Inherits System.Windows.Forms.UserControl

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.btnStart = New LaunchMenu.MDButton()
        Me.txtPwd = New LaunchMenu.MDTextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.tTaskMngr = New System.Windows.Forms.Timer(Me.components)
        Me.SuspendLayout()
        '
        'btnStart
        '
        Me.btnStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.btnStart.BaseColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.btnStart.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStart.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnStart.Location = New System.Drawing.Point(95, 77)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Rounded = True
        Me.btnStart.Size = New System.Drawing.Size(140, 44)
        Me.btnStart.TabIndex = 2
        Me.btnStart.Text = "Unlock"
        Me.btnStart.TextColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        '
        'txtPwd
        '
        Me.txtPwd.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.txtPwd.EnterPressed = False
        Me.txtPwd.LightTheme = False
        Me.txtPwd.Location = New System.Drawing.Point(89, 32)
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
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(22, 39)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(57, 15)
        Me.Label1.TabIndex = 13
        Me.Label1.Text = "Password"
        '
        'tTaskMngr
        '
        Me.tTaskMngr.Enabled = True
        Me.tTaskMngr.Interval = 1
        '
        'slUnlock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.txtPwd)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ForeColor = System.Drawing.Color.White
        Me.Name = "slUnlock"
        Me.Padding = New System.Windows.Forms.Padding(3)
        Me.Size = New System.Drawing.Size(328, 155)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents btnStart As MDButton
    Friend WithEvents txtPwd As MDTextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents tTaskMngr As Timer
End Class
