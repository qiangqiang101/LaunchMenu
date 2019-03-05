<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class AppItemL
    Inherits System.Windows.Forms.UserControl

    'UserControl overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
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
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.bwEnter = New System.ComponentModel.BackgroundWorker()
        Me.bwLeave = New System.ComponentModel.BackgroundWorker()
        Me.liPanel = New LaunchMenu.LargeItemPanel()
        Me.cmsMenu = New LaunchMenu.MDContextMenuStrip()
        Me.tmsiOpen = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmsiOpenFileLocation = New System.Windows.Forms.ToolStripMenuItem()
        Me.tmsiAdministrator = New System.Windows.Forms.ToolStripMenuItem()
        Me.ToolStripSeparator1 = New System.Windows.Forms.ToolStripSeparator()
        Me.tmsiWebsite = New System.Windows.Forms.ToolStripMenuItem()
        Me.cmsMenu.SuspendLayout()
        Me.SuspendLayout()
        '
        'bwEnter
        '
        Me.bwEnter.WorkerSupportsCancellation = True
        '
        'bwLeave
        '
        Me.bwLeave.WorkerSupportsCancellation = True
        '
        'liPanel
        '
        Me.liPanel.buttonColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.liPanel.buttonText = "Play"
        Me.liPanel.ColorBehind = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.liPanel.ColorDown = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.liPanel.ColorUp = System.Drawing.Color.Transparent
        Me.liPanel.ContextMenuStrip = Me.cmsMenu
        Me.liPanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.liPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.liPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.liPanel.ForeColor = System.Drawing.Color.White
        Me.liPanel.Image = Global.LaunchMenu.My.Resources.Resources.NO_IMAGE
        Me.liPanel.ImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.liPanel.Location = New System.Drawing.Point(0, 0)
        Me.liPanel.Name = "liPanel"
        Me.liPanel.Size = New System.Drawing.Size(163, 231)
        Me.liPanel.TabIndex = 0
        Me.liPanel.Title = "Need for Speed Payback for Speed Payback"
        '
        'cmsMenu
        '
        Me.cmsMenu.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.cmsMenu.ForeColor = System.Drawing.Color.White
        Me.cmsMenu.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tmsiOpen, Me.tmsiOpenFileLocation, Me.tmsiAdministrator, Me.ToolStripSeparator1, Me.tmsiWebsite})
        Me.cmsMenu.LightTheme = False
        Me.cmsMenu.Name = "cmsMenu"
        Me.cmsMenu.ShowImageMargin = False
        Me.cmsMenu.Size = New System.Drawing.Size(159, 98)
        '
        'tmsiOpen
        '
        Me.tmsiOpen.Font = New System.Drawing.Font("Segoe UI", 9.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.tmsiOpen.Name = "tmsiOpen"
        Me.tmsiOpen.Size = New System.Drawing.Size(158, 22)
        Me.tmsiOpen.Text = "Open"
        '
        'tmsiOpenFileLocation
        '
        Me.tmsiOpenFileLocation.Name = "tmsiOpenFileLocation"
        Me.tmsiOpenFileLocation.Size = New System.Drawing.Size(158, 22)
        Me.tmsiOpenFileLocation.Text = "Open file location"
        '
        'tmsiAdministrator
        '
        Me.tmsiAdministrator.Name = "tmsiAdministrator"
        Me.tmsiAdministrator.Size = New System.Drawing.Size(158, 22)
        Me.tmsiAdministrator.Text = "Run as administrator"
        '
        'ToolStripSeparator1
        '
        Me.ToolStripSeparator1.Name = "ToolStripSeparator1"
        Me.ToolStripSeparator1.Size = New System.Drawing.Size(155, 6)
        '
        'tmsiWebsite
        '
        Me.tmsiWebsite.Name = "tmsiWebsite"
        Me.tmsiWebsite.Size = New System.Drawing.Size(158, 22)
        Me.tmsiWebsite.Text = "Visit website"
        '
        'AppItemL
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.liPanel)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "AppItemL"
        Me.Size = New System.Drawing.Size(163, 231)
        Me.cmsMenu.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents bwEnter As System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLeave As System.ComponentModel.BackgroundWorker
    Friend WithEvents liPanel As LargeItemPanel
    Friend WithEvents cmsMenu As MDContextMenuStrip
    Friend WithEvents tmsiOpen As ToolStripMenuItem
    Friend WithEvents tmsiOpenFileLocation As ToolStripMenuItem
    Friend WithEvents tmsiAdministrator As ToolStripMenuItem
    Friend WithEvents ToolStripSeparator1 As ToolStripSeparator
    Friend WithEvents tmsiWebsite As ToolStripMenuItem
End Class
