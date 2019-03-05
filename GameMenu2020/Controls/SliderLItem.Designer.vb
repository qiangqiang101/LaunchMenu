<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class SliderLItem
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
        Me.Tint = New LaunchMenu.GradientPanel()
        Me.SuspendLayout()
        '
        'bwEnter
        '
        '
        'Tint
        '
        Me.Tint.BackColor = System.Drawing.Color.Transparent
        Me.Tint.Color1 = System.Drawing.Color.Transparent
        Me.Tint.Color2 = System.Drawing.Color.Black
        Me.Tint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Tint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tint.LeftToRight = False
        Me.Tint.Location = New System.Drawing.Point(0, 0)
        Me.Tint.Name = "Tint"
        Me.Tint.Padding = New System.Windows.Forms.Padding(9)
        Me.Tint.PubDate = "Wednesday, 20 February, 2019 12:00:00 AM"
        Me.Tint.Radius = 20
        Me.Tint.RoundedBackColor = System.Drawing.Color.Transparent
        Me.Tint.Size = New System.Drawing.Size(714, 378)
        Me.Tint.Subtitle = "Subtitle"
        Me.Tint.TabIndex = 3
        Me.Tint.Title = "Title"
        '
        'SliderLItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Controls.Add(Me.Tint)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "SliderLItem"
        Me.Size = New System.Drawing.Size(714, 378)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tint As GradientPanel
    Friend WithEvents bwEnter As System.ComponentModel.BackgroundWorker
End Class
