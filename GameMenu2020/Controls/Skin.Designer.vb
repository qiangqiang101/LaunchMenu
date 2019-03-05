<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Skin
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
        Me.lblMaximize = New System.Windows.Forms.Label()
        Me.lblMinimize = New System.Windows.Forms.Label()
        Me.lblClose = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'lblMaximize
        '
        Me.lblMaximize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMaximize.BackColor = System.Drawing.Color.Transparent
        Me.lblMaximize.Font = New System.Drawing.Font("Marlett", 10.0!)
        Me.lblMaximize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.lblMaximize.Location = New System.Drawing.Point(39, 6)
        Me.lblMaximize.Name = "lblMaximize"
        Me.lblMaximize.Size = New System.Drawing.Size(25, 25)
        Me.lblMaximize.TabIndex = 4
        Me.lblMaximize.Text = "1"
        Me.lblMaximize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblMinimize
        '
        Me.lblMinimize.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMinimize.BackColor = System.Drawing.Color.Transparent
        Me.lblMinimize.Font = New System.Drawing.Font("Marlett", 10.0!)
        Me.lblMinimize.ForeColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.lblMinimize.Location = New System.Drawing.Point(12, 6)
        Me.lblMinimize.Name = "lblMinimize"
        Me.lblMinimize.Size = New System.Drawing.Size(25, 25)
        Me.lblMinimize.TabIndex = 3
        Me.lblMinimize.Text = "0"
        Me.lblMinimize.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblClose
        '
        Me.lblClose.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblClose.BackColor = System.Drawing.Color.Transparent
        Me.lblClose.Font = New System.Drawing.Font("Marlett", 10.0!)
        Me.lblClose.ForeColor = System.Drawing.Color.FromArgb(CType(CType(165, Byte), Integer), CType(CType(166, Byte), Integer), CType(CType(167, Byte), Integer))
        Me.lblClose.Location = New System.Drawing.Point(66, 6)
        Me.lblClose.Name = "lblClose"
        Me.lblClose.Size = New System.Drawing.Size(25, 25)
        Me.lblClose.TabIndex = 2
        Me.lblClose.Text = "r"
        Me.lblClose.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Skin
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(34, Byte), Integer), CType(CType(37, Byte), Integer))
        Me.Controls.Add(Me.lblMaximize)
        Me.Controls.Add(Me.lblMinimize)
        Me.Controls.Add(Me.lblClose)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.MaximumSize = New System.Drawing.Size(100, 35)
        Me.MinimumSize = New System.Drawing.Size(100, 35)
        Me.Name = "Skin"
        Me.Padding = New System.Windows.Forms.Padding(6)
        Me.Size = New System.Drawing.Size(100, 35)
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents lblClose As Label
    Friend WithEvents lblMinimize As Label
    Friend WithEvents lblMaximize As Label
End Class
