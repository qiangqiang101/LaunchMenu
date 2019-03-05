<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class frmLaunch
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmLaunch))
        Me.pbImage = New System.Windows.Forms.PictureBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.lblGameName = New System.Windows.Forms.Label()
        Me.lblPublisher = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.lblGerne = New System.Windows.Forms.Label()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.lblDescription = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.pbRating = New System.Windows.Forms.PictureBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.lblDeveloper = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.lblStarring = New System.Windows.Forms.Label()
        Me.lblDirector = New System.Windows.Forms.Label()
        Me.btnCancel = New LaunchMenu.MDButton()
        Me.btnWebsite = New LaunchMenu.MDButton()
        Me.btnStart = New LaunchMenu.MDButton()
        Me.Skin1 = New LaunchMenu.Skin()
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.pbRating, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'pbImage
        '
        Me.pbImage.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.pbImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pbImage.Image = Global.LaunchMenu.My.Resources.Resources.NO_IMAGE
        Me.pbImage.Location = New System.Drawing.Point(12, 37)
        Me.pbImage.Name = "pbImage"
        Me.pbImage.Size = New System.Drawing.Size(190, 250)
        Me.pbImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbImage.TabIndex = 1
        Me.pbImage.TabStop = False
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label1.ForeColor = System.Drawing.Color.White
        Me.Label1.Location = New System.Drawing.Point(208, 37)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(48, 19)
        Me.Label1.TabIndex = 2
        Me.Label1.Text = "Name:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGameName
        '
        Me.lblGameName.AutoSize = True
        Me.lblGameName.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.lblGameName.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblGameName.ForeColor = System.Drawing.Color.White
        Me.lblGameName.Location = New System.Drawing.Point(295, 37)
        Me.lblGameName.Name = "lblGameName"
        Me.lblGameName.Size = New System.Drawing.Size(127, 19)
        Me.lblGameName.TabIndex = 3
        Me.lblGameName.Text = "Trand Aheft Guto X"
        Me.lblGameName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblPublisher
        '
        Me.lblPublisher.AutoSize = True
        Me.lblPublisher.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.lblPublisher.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblPublisher.ForeColor = System.Drawing.Color.White
        Me.lblPublisher.Location = New System.Drawing.Point(295, 75)
        Me.lblPublisher.Name = "lblPublisher"
        Me.lblPublisher.Size = New System.Drawing.Size(107, 19)
        Me.lblPublisher.TabIndex = 5
        Me.lblPublisher.Text = "Gockstar Rames"
        Me.lblPublisher.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label3
        '
        Me.Label3.AutoSize = True
        Me.Label3.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Label3.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label3.ForeColor = System.Drawing.Color.White
        Me.Label3.Location = New System.Drawing.Point(208, 75)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(68, 19)
        Me.Label3.TabIndex = 4
        Me.Label3.Text = "Publisher:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblGerne
        '
        Me.lblGerne.AutoSize = True
        Me.lblGerne.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.lblGerne.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblGerne.ForeColor = System.Drawing.Color.White
        Me.lblGerne.Location = New System.Drawing.Point(295, 94)
        Me.lblGerne.Name = "lblGerne"
        Me.lblGerne.Size = New System.Drawing.Size(116, 19)
        Me.lblGerne.TabIndex = 7
        Me.lblGerne.Text = "Action-adventure"
        Me.lblGerne.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Label4.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label4.ForeColor = System.Drawing.Color.White
        Me.Label4.Location = New System.Drawing.Point(208, 94)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(49, 19)
        Me.Label4.TabIndex = 6
        Me.Label4.Text = "Gerne:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDescription
        '
        Me.lblDescription.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.lblDescription.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblDescription.ForeColor = System.Drawing.Color.White
        Me.lblDescription.Location = New System.Drawing.Point(295, 113)
        Me.lblDescription.Name = "lblDescription"
        Me.lblDescription.Size = New System.Drawing.Size(432, 99)
        Me.lblDescription.TabIndex = 9
        Me.lblDescription.Text = "Description of the game"
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Label5.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label5.ForeColor = System.Drawing.Color.White
        Me.Label5.Location = New System.Drawing.Point(208, 113)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(81, 19)
        Me.Label5.TabIndex = 8
        Me.Label5.Text = "Description:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'pbRating
        '
        Me.pbRating.BackColor = System.Drawing.Color.Black
        Me.pbRating.BackgroundImage = Global.LaunchMenu.My.Resources.Resources.ESRB_Mature
        Me.pbRating.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom
        Me.pbRating.Location = New System.Drawing.Point(299, 215)
        Me.pbRating.Name = "pbRating"
        Me.pbRating.Size = New System.Drawing.Size(59, 72)
        Me.pbRating.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.pbRating.TabIndex = 15
        Me.pbRating.TabStop = False
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Label2.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label2.ForeColor = System.Drawing.Color.White
        Me.Label2.Location = New System.Drawing.Point(208, 215)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(51, 19)
        Me.Label2.TabIndex = 16
        Me.Label2.Text = "Rating:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'lblDeveloper
        '
        Me.lblDeveloper.AutoSize = True
        Me.lblDeveloper.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.lblDeveloper.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblDeveloper.ForeColor = System.Drawing.Color.White
        Me.lblDeveloper.Location = New System.Drawing.Point(295, 56)
        Me.lblDeveloper.Name = "lblDeveloper"
        Me.lblDeveloper.Size = New System.Drawing.Size(101, 19)
        Me.lblDeveloper.TabIndex = 22
        Me.lblDeveloper.Text = "Nockstar Rorth"
        Me.lblDeveloper.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Label7.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label7.ForeColor = System.Drawing.Color.White
        Me.Label7.Location = New System.Drawing.Point(208, 56)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(74, 19)
        Me.Label7.TabIndex = 21
        Me.Label7.Text = "Developer:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Label6.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.Label6.ForeColor = System.Drawing.Color.White
        Me.Label6.Location = New System.Drawing.Point(208, 234)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(60, 19)
        Me.Label6.TabIndex = 23
        Me.Label6.Text = "Starring:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.Label6.Visible = False
        '
        'lblStarring
        '
        Me.lblStarring.AutoSize = True
        Me.lblStarring.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.lblStarring.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblStarring.ForeColor = System.Drawing.Color.White
        Me.lblStarring.Location = New System.Drawing.Point(295, 234)
        Me.lblStarring.Name = "lblStarring"
        Me.lblStarring.Size = New System.Drawing.Size(311, 19)
        Me.lblStarring.TabIndex = 24
        Me.lblStarring.Text = "Jwayne Dohnson, Srnold, Achzenegger, Lruce Bee"
        Me.lblStarring.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblStarring.Visible = False
        '
        'lblDirector
        '
        Me.lblDirector.AutoSize = True
        Me.lblDirector.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.lblDirector.Font = New System.Drawing.Font("Segoe UI", 10.0!)
        Me.lblDirector.ForeColor = System.Drawing.Color.White
        Me.lblDirector.Location = New System.Drawing.Point(295, 215)
        Me.lblDirector.Name = "lblDirector"
        Me.lblDirector.Size = New System.Drawing.Size(110, 19)
        Me.lblDirector.TabIndex = 25
        Me.lblDirector.Text = "Zobert Remeckis"
        Me.lblDirector.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        Me.lblDirector.Visible = False
        '
        'btnCancel
        '
        Me.btnCancel.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnCancel.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.btnCancel.BaseColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(84, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnCancel.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnCancel.Location = New System.Drawing.Point(445, 293)
        Me.btnCancel.Name = "btnCancel"
        Me.btnCancel.Rounded = True
        Me.btnCancel.Size = New System.Drawing.Size(140, 44)
        Me.btnCancel.TabIndex = 20
        Me.btnCancel.Text = "Cancel"
        Me.btnCancel.TextColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        '
        'btnWebsite
        '
        Me.btnWebsite.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnWebsite.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.btnWebsite.BaseColor = System.Drawing.Color.FromArgb(CType(CType(79, Byte), Integer), CType(CType(84, Byte), Integer), CType(CType(92, Byte), Integer))
        Me.btnWebsite.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnWebsite.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnWebsite.Location = New System.Drawing.Point(299, 293)
        Me.btnWebsite.Name = "btnWebsite"
        Me.btnWebsite.Rounded = True
        Me.btnWebsite.Size = New System.Drawing.Size(140, 44)
        Me.btnWebsite.TabIndex = 18
        Me.btnWebsite.Text = "Visit Website"
        Me.btnWebsite.TextColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        '
        'btnStart
        '
        Me.btnStart.Anchor = System.Windows.Forms.AnchorStyles.Bottom
        Me.btnStart.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.btnStart.BaseColor = System.Drawing.Color.FromArgb(CType(CType(67, Byte), Integer), CType(CType(181, Byte), Integer), CType(CType(129, Byte), Integer))
        Me.btnStart.Cursor = System.Windows.Forms.Cursors.Hand
        Me.btnStart.Font = New System.Drawing.Font("Segoe UI", 12.0!)
        Me.btnStart.Location = New System.Drawing.Point(153, 293)
        Me.btnStart.Name = "btnStart"
        Me.btnStart.Rounded = True
        Me.btnStart.Size = New System.Drawing.Size(140, 44)
        Me.btnStart.TabIndex = 17
        Me.btnStart.Text = "Start"
        Me.btnStart.TextColor = System.Drawing.Color.FromArgb(CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer), CType(CType(243, Byte), Integer))
        '
        'Skin1
        '
        Me.Skin1.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.Skin1.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.Skin1.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Skin1.ForeColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.Skin1.Location = New System.Drawing.Point(655, 0)
        Me.Skin1.MaximumSize = New System.Drawing.Size(100, 35)
        Me.Skin1.MinimumSize = New System.Drawing.Size(100, 35)
        Me.Skin1.Name = "Skin1"
        Me.Skin1.Padding = New System.Windows.Forms.Padding(6)
        Me.Skin1.Size = New System.Drawing.Size(100, 35)
        Me.Skin1.TabIndex = 0
        '
        'frmLaunch
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(54, Byte), Integer), CType(CType(57, Byte), Integer), CType(CType(63, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(739, 349)
        Me.Controls.Add(Me.lblDirector)
        Me.Controls.Add(Me.lblStarring)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.lblDeveloper)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.btnCancel)
        Me.Controls.Add(Me.btnWebsite)
        Me.Controls.Add(Me.btnStart)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.pbRating)
        Me.Controls.Add(Me.lblDescription)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.lblGerne)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.lblPublisher)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.lblGameName)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.pbImage)
        Me.Controls.Add(Me.Skin1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.ForeColor = System.Drawing.Color.FromArgb(CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer), CType(CType(246, Byte), Integer))
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmLaunch"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Game Launcher"
        CType(Me.pbImage, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.pbRating, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents Skin1 As Skin
    Friend WithEvents pbImage As PictureBox
    Friend WithEvents Label1 As Label
    Friend WithEvents lblGameName As Label
    Friend WithEvents lblPublisher As Label
    Friend WithEvents Label3 As Label
    Friend WithEvents lblGerne As Label
    Friend WithEvents Label4 As Label
    Friend WithEvents lblDescription As Label
    Friend WithEvents Label5 As Label
    Friend WithEvents pbRating As PictureBox
    Friend WithEvents Label2 As Label
    Friend WithEvents btnStart As MDButton
    Friend WithEvents btnWebsite As MDButton
    Friend WithEvents btnCancel As MDButton
    Friend WithEvents lblDeveloper As Label
    Friend WithEvents Label7 As Label
    Friend WithEvents Label6 As Label
    Friend WithEvents lblStarring As Label
    Friend WithEvents lblDirector As Label
End Class
