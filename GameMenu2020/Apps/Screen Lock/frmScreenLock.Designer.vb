<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmScreenLock
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmScreenLock))
        Me.tTime = New System.Windows.Forms.Timer(Me.components)
        Me.tBackgroundImage = New System.Windows.Forms.Timer(Me.components)
        Me.tWeather = New System.Windows.Forms.Timer(Me.components)
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.SlLock1 = New LaunchMenu.slLock()
        Me.Panel1.SuspendLayout()
        Me.SuspendLayout()
        '
        'tTime
        '
        Me.tTime.Enabled = True
        Me.tTime.Interval = 60
        '
        'tBackgroundImage
        '
        Me.tBackgroundImage.Enabled = True
        Me.tBackgroundImage.Interval = 300000
        '
        'tWeather
        '
        Me.tWeather.Enabled = True
        Me.tWeather.Interval = 1800000
        '
        'Panel1
        '
        Me.Panel1.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Panel1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Panel1.Controls.Add(Me.SlLock1)
        Me.Panel1.Location = New System.Drawing.Point(1001, 561)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(331, 156)
        Me.Panel1.TabIndex = 7
        Me.Panel1.Visible = False
        '
        'SlLock1
        '
        Me.SlLock1.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.SlLock1.Dock = System.Windows.Forms.DockStyle.Fill
        Me.SlLock1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.SlLock1.ForeColor = System.Drawing.Color.White
        Me.SlLock1.Location = New System.Drawing.Point(0, 0)
        Me.SlLock1.Name = "SlLock1"
        Me.SlLock1.Padding = New System.Windows.Forms.Padding(3)
        Me.SlLock1.Size = New System.Drawing.Size(331, 156)
        Me.SlLock1.TabIndex = 0
        '
        'frmScreenLock
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.ClientSize = New System.Drawing.Size(1344, 729)
        Me.Controls.Add(Me.Panel1)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmScreenLock"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Screen Lock"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.Panel1.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents tTime As Timer
    Friend WithEvents tBackgroundImage As Timer
    Friend WithEvents tWeather As Timer
    Friend WithEvents Panel1 As Panel
    Friend WithEvents SlLock1 As slLock
End Class
