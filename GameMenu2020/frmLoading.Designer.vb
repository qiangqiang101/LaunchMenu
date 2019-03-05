<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLoading
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
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
        Me.components = New System.ComponentModel.Container()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLoading))
        Me.lblMemes = New System.Windows.Forms.Label()
        Me.lblLoading = New System.Windows.Forms.Label()
        Me.pbLoading = New System.Windows.Forms.PictureBox()
        Me.tLoading = New System.Windows.Forms.Timer(Me.components)
        CType(Me.pbLoading, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'lblMemes
        '
        Me.lblMemes.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblMemes.Font = New System.Drawing.Font("Segoe UI", 11.25!, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblMemes.ForeColor = System.Drawing.Color.White
        Me.lblMemes.Location = New System.Drawing.Point(12, 174)
        Me.lblMemes.Name = "lblMemes"
        Me.lblMemes.Size = New System.Drawing.Size(260, 71)
        Me.lblMemes.TabIndex = 0
        Me.lblMemes.Text = "TRYING TO SKIP A CUTSCENE"
        Me.lblMemes.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLoading
        '
        Me.lblLoading.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lblLoading.Font = New System.Drawing.Font("Segoe UI", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblLoading.ForeColor = System.Drawing.Color.Gray
        Me.lblLoading.Location = New System.Drawing.Point(12, 222)
        Me.lblLoading.Name = "lblLoading"
        Me.lblLoading.Size = New System.Drawing.Size(260, 23)
        Me.lblLoading.TabIndex = 1
        Me.lblLoading.Text = "LOADING"
        Me.lblLoading.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'pbLoading
        '
        Me.pbLoading.Anchor = System.Windows.Forms.AnchorStyles.None
        Me.pbLoading.Image = Global.LaunchMenu.My.Resources.Resources.LaunchMenuLogo_w_t
        Me.pbLoading.Location = New System.Drawing.Point(2, 45)
        Me.pbLoading.Name = "pbLoading"
        Me.pbLoading.Size = New System.Drawing.Size(280, 132)
        Me.pbLoading.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbLoading.TabIndex = 2
        Me.pbLoading.TabStop = False
        '
        'tLoading
        '
        Me.tLoading.Enabled = True
        Me.tLoading.Interval = 30
        '
        'frmLoading
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(32, Byte), Integer), CType(CType(33, Byte), Integer), CType(CType(35, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(284, 311)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblLoading)
        Me.Controls.Add(Me.lblMemes)
        Me.Controls.Add(Me.pbLoading)
        Me.DoubleBuffered = True
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmLoading"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Loading..."
        CType(Me.pbLoading, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub

    Friend WithEvents lblMemes As Label
    Friend WithEvents lblLoading As Label
    Friend WithEvents pbLoading As PictureBox
    Friend WithEvents tLoading As Timer
End Class
