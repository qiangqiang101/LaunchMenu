<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class AppItemW
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
        Me.bwEnter = New System.ComponentModel.BackgroundWorker()
        Me.bwLeave = New System.ComponentModel.BackgroundWorker()
        Me.wiPanel = New LaunchMenu.WideItemPanel()
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
        'wiPanel
        '
        Me.wiPanel.buttonColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.wiPanel.buttonText = "Play"
        Me.wiPanel.ColorBehind = System.Drawing.Color.FromArgb(CType(CType(114, Byte), Integer), CType(CType(137, Byte), Integer), CType(CType(218, Byte), Integer))
        Me.wiPanel.ColorLeft = System.Drawing.Color.Transparent
        Me.wiPanel.ColorRight = System.Drawing.Color.FromArgb(CType(CType(150, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.wiPanel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.wiPanel.Dock = System.Windows.Forms.DockStyle.Fill
        Me.wiPanel.Font = New System.Drawing.Font("Segoe UI Semibold", 10.0!, System.Drawing.FontStyle.Bold)
        Me.wiPanel.ForeColor = System.Drawing.Color.White
        Me.wiPanel.Image = Global.LaunchMenu.My.Resources.Resources.NO_IMAGE
        Me.wiPanel.ImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.wiPanel.Location = New System.Drawing.Point(0, 0)
        Me.wiPanel.Name = "wiPanel"
        Me.wiPanel.Size = New System.Drawing.Size(235, 80)
        Me.wiPanel.TabIndex = 0
        Me.wiPanel.Title = "Need for Speed 2015"
        '
        'AppItemW
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.Controls.Add(Me.wiPanel)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "AppItemW"
        Me.Size = New System.Drawing.Size(235, 80)
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents wiPanel As WideItemPanel
    Friend WithEvents bwEnter As System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLeave As System.ComponentModel.BackgroundWorker
End Class
