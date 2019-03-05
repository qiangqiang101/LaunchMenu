<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class SliderSItem
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
        Me.Tint = New System.Windows.Forms.Panel()
        Me.Title = New LaunchMenu.MDLabel()
        Me.bwEnter = New System.ComponentModel.BackgroundWorker()
        Me.bwLeave = New System.ComponentModel.BackgroundWorker()
        Me.Tint.SuspendLayout()
        Me.SuspendLayout()
        '
        'Tint
        '
        Me.Tint.BackColor = System.Drawing.Color.FromArgb(CType(CType(200, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer), CType(CType(0, Byte), Integer))
        Me.Tint.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch
        Me.Tint.Controls.Add(Me.Title)
        Me.Tint.Cursor = System.Windows.Forms.Cursors.Hand
        Me.Tint.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Tint.Location = New System.Drawing.Point(0, 0)
        Me.Tint.Name = "Tint"
        Me.Tint.Size = New System.Drawing.Size(296, 90)
        Me.Tint.TabIndex = 9
        '
        'Title
        '
        Me.Title.BackColor = System.Drawing.Color.Transparent
        Me.Title.Dock = System.Windows.Forms.DockStyle.Fill
        Me.Title.Font = New System.Drawing.Font("Helvetica", 12.0!, System.Drawing.FontStyle.Bold)
        Me.Title.ForeColor = System.Drawing.Color.White
        Me.Title.Location = New System.Drawing.Point(0, 0)
        Me.Title.Name = "Title"
        Me.Title.Size = New System.Drawing.Size(296, 90)
        Me.Title.TabIndex = 7
        Me.Title.Text = "That Car Speeding"
        Me.Title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'bwEnter
        '
        '
        'bwLeave
        '
        '
        'SliderSItem
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.Transparent
        Me.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center
        Me.Controls.Add(Me.Tint)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Name = "SliderSItem"
        Me.Size = New System.Drawing.Size(296, 90)
        Me.Tint.ResumeLayout(False)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Tint As Panel
    Friend WithEvents Title As MDLabel
    Friend WithEvents bwEnter As System.ComponentModel.BackgroundWorker
    Friend WithEvents bwLeave As System.ComponentModel.BackgroundWorker
End Class
