<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmImport
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmImport))
        Me.txtMdb = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnMdb = New System.Windows.Forms.Button()
        Me.tlp = New System.Windows.Forms.TableLayoutPanel()
        Me.pButton = New System.Windows.Forms.Panel()
        Me.btnSave = New System.Windows.Forms.Button()
        Me.btnRightall = New System.Windows.Forms.Button()
        Me.btnRight = New System.Windows.Forms.Button()
        Me.pOld = New System.Windows.Forms.Panel()
        Me.cmbOld = New System.Windows.Forms.ComboBox()
        Me.lvOld = New System.Windows.Forms.ListView()
        Me.chOldName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.pNew = New System.Windows.Forms.Panel()
        Me.lvNew = New System.Windows.Forms.ListView()
        Me.chNewName = CType(New System.Windows.Forms.ColumnHeader(), System.Windows.Forms.ColumnHeader)
        Me.cmbNew = New System.Windows.Forms.ComboBox()
        Me.tlp.SuspendLayout()
        Me.pButton.SuspendLayout()
        Me.pOld.SuspendLayout()
        Me.pNew.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtMdb
        '
        Me.txtMdb.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtMdb.Location = New System.Drawing.Point(80, 12)
        Me.txtMdb.Name = "txtMdb"
        Me.txtMdb.Size = New System.Drawing.Size(496, 23)
        Me.txtMdb.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 15)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "DB File:"
        '
        'btnMdb
        '
        Me.btnMdb.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnMdb.Location = New System.Drawing.Point(582, 12)
        Me.btnMdb.Name = "btnMdb"
        Me.btnMdb.Size = New System.Drawing.Size(44, 23)
        Me.btnMdb.TabIndex = 2
        Me.btnMdb.Text = "..."
        Me.btnMdb.UseVisualStyleBackColor = True
        '
        'tlp
        '
        Me.tlp.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.tlp.ColumnCount = 3
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0!))
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 6.0!))
        Me.tlp.ColumnStyles.Add(New System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 47.0!))
        Me.tlp.Controls.Add(Me.pButton, 1, 0)
        Me.tlp.Controls.Add(Me.pOld, 0, 0)
        Me.tlp.Controls.Add(Me.pNew, 2, 0)
        Me.tlp.Location = New System.Drawing.Point(12, 41)
        Me.tlp.Name = "tlp"
        Me.tlp.RowCount = 1
        Me.tlp.RowStyles.Add(New System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100.0!))
        Me.tlp.Size = New System.Drawing.Size(614, 439)
        Me.tlp.TabIndex = 3
        '
        'pButton
        '
        Me.pButton.Controls.Add(Me.btnSave)
        Me.pButton.Controls.Add(Me.btnRightall)
        Me.pButton.Controls.Add(Me.btnRight)
        Me.pButton.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pButton.Location = New System.Drawing.Point(291, 3)
        Me.pButton.Name = "pButton"
        Me.pButton.Size = New System.Drawing.Size(30, 433)
        Me.pButton.TabIndex = 2
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType(((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Image = Global.Import.My.Resources.Resources.disk
        Me.btnSave.Location = New System.Drawing.Point(3, 407)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(24, 23)
        Me.btnSave.TabIndex = 2
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'btnRightall
        '
        Me.btnRightall.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRightall.Image = Global.Import.My.Resources.Resources.bullet_arrow_right_2
        Me.btnRightall.Location = New System.Drawing.Point(3, 32)
        Me.btnRightall.Name = "btnRightall"
        Me.btnRightall.Size = New System.Drawing.Size(24, 23)
        Me.btnRightall.TabIndex = 1
        Me.btnRightall.UseVisualStyleBackColor = True
        '
        'btnRight
        '
        Me.btnRight.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnRight.Image = Global.Import.My.Resources.Resources.bullet_arrow_right
        Me.btnRight.Location = New System.Drawing.Point(3, 3)
        Me.btnRight.Name = "btnRight"
        Me.btnRight.Size = New System.Drawing.Size(24, 23)
        Me.btnRight.TabIndex = 0
        Me.btnRight.UseVisualStyleBackColor = True
        '
        'pOld
        '
        Me.pOld.Controls.Add(Me.cmbOld)
        Me.pOld.Controls.Add(Me.lvOld)
        Me.pOld.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pOld.Location = New System.Drawing.Point(3, 3)
        Me.pOld.Name = "pOld"
        Me.pOld.Size = New System.Drawing.Size(282, 433)
        Me.pOld.TabIndex = 0
        '
        'cmbOld
        '
        Me.cmbOld.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbOld.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbOld.FormattingEnabled = True
        Me.cmbOld.Location = New System.Drawing.Point(3, 3)
        Me.cmbOld.Name = "cmbOld"
        Me.cmbOld.Size = New System.Drawing.Size(276, 23)
        Me.cmbOld.TabIndex = 1
        '
        'lvOld
        '
        Me.lvOld.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvOld.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chOldName})
        Me.lvOld.GridLines = True
        Me.lvOld.Location = New System.Drawing.Point(3, 32)
        Me.lvOld.Name = "lvOld"
        Me.lvOld.Size = New System.Drawing.Size(276, 398)
        Me.lvOld.TabIndex = 0
        Me.lvOld.UseCompatibleStateImageBehavior = False
        Me.lvOld.View = System.Windows.Forms.View.List
        '
        'chOldName
        '
        Me.chOldName.Text = "Name"
        Me.chOldName.Width = 300
        '
        'pNew
        '
        Me.pNew.Controls.Add(Me.lvNew)
        Me.pNew.Controls.Add(Me.cmbNew)
        Me.pNew.Dock = System.Windows.Forms.DockStyle.Fill
        Me.pNew.Location = New System.Drawing.Point(327, 3)
        Me.pNew.Name = "pNew"
        Me.pNew.Size = New System.Drawing.Size(284, 433)
        Me.pNew.TabIndex = 1
        '
        'lvNew
        '
        Me.lvNew.Anchor = CType((((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Bottom) _
            Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.lvNew.Columns.AddRange(New System.Windows.Forms.ColumnHeader() {Me.chNewName})
        Me.lvNew.GridLines = True
        Me.lvNew.Location = New System.Drawing.Point(3, 32)
        Me.lvNew.Name = "lvNew"
        Me.lvNew.Size = New System.Drawing.Size(278, 398)
        Me.lvNew.TabIndex = 3
        Me.lvNew.UseCompatibleStateImageBehavior = False
        Me.lvNew.View = System.Windows.Forms.View.List
        '
        'chNewName
        '
        Me.chNewName.Text = "Name"
        Me.chNewName.Width = 300
        '
        'cmbNew
        '
        Me.cmbNew.Anchor = CType(((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Left) _
            Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.cmbNew.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cmbNew.FormattingEnabled = True
        Me.cmbNew.Location = New System.Drawing.Point(3, 3)
        Me.cmbNew.Name = "cmbNew"
        Me.cmbNew.Size = New System.Drawing.Size(278, 23)
        Me.cmbNew.TabIndex = 2
        '
        'frmImport
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(638, 492)
        Me.Controls.Add(Me.tlp)
        Me.Controls.Add(Me.btnMdb)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMdb)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.Name = "frmImport"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Import"
        Me.tlp.ResumeLayout(False)
        Me.pButton.ResumeLayout(False)
        Me.pOld.ResumeLayout(False)
        Me.pNew.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtMdb As TextBox
    Friend WithEvents Label1 As Label
    Friend WithEvents btnMdb As Button
    Friend WithEvents tlp As TableLayoutPanel
    Friend WithEvents pButton As Panel
    Friend WithEvents pOld As Panel
    Friend WithEvents cmbOld As ComboBox
    Friend WithEvents lvOld As ListView
    Friend WithEvents chOldName As ColumnHeader
    Friend WithEvents pNew As Panel
    Friend WithEvents lvNew As ListView
    Friend WithEvents chNewName As ColumnHeader
    Friend WithEvents cmbNew As ComboBox
    Friend WithEvents btnRight As Button
    Friend WithEvents btnSave As Button
    Friend WithEvents btnRightall As Button
End Class
