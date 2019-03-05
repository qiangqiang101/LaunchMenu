<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmActivate
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(frmActivate))
        Me.rbActivate = New System.Windows.Forms.RadioButton()
        Me.rbPurchase = New System.Windows.Forms.RadioButton()
        Me.BtnCancel = New System.Windows.Forms.Button()
        Me.btnNext = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.rbTry = New System.Windows.Forms.RadioButton()
        Me.SuspendLayout()
        '
        'rbActivate
        '
        Me.rbActivate.AutoSize = True
        Me.rbActivate.Location = New System.Drawing.Point(63, 169)
        Me.rbActivate.Name = "rbActivate"
        Me.rbActivate.Size = New System.Drawing.Size(264, 19)
        Me.rbActivate.TabIndex = 38
        Me.rbActivate.Text = "I have a product key and I'm ready to activate"
        Me.rbActivate.UseVisualStyleBackColor = True
        '
        'rbPurchase
        '
        Me.rbPurchase.AutoSize = True
        Me.rbPurchase.Checked = True
        Me.rbPurchase.Location = New System.Drawing.Point(63, 144)
        Me.rbPurchase.Name = "rbPurchase"
        Me.rbPurchase.Size = New System.Drawing.Size(139, 19)
        Me.rbPurchase.TabIndex = 37
        Me.rbPurchase.TabStop = True
        Me.rbPurchase.Text = "I'm ready to purchase"
        Me.rbPurchase.UseVisualStyleBackColor = True
        '
        'BtnCancel
        '
        Me.BtnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.BtnCancel.Location = New System.Drawing.Point(380, 392)
        Me.BtnCancel.Name = "BtnCancel"
        Me.BtnCancel.Size = New System.Drawing.Size(92, 27)
        Me.BtnCancel.TabIndex = 36
        Me.BtnCancel.Text = "Cancel"
        Me.BtnCancel.UseVisualStyleBackColor = True
        '
        'btnNext
        '
        Me.btnNext.Location = New System.Drawing.Point(282, 392)
        Me.btnNext.Name = "btnNext"
        Me.btnNext.Size = New System.Drawing.Size(92, 27)
        Me.btnNext.TabIndex = 35
        Me.btnNext.Text = "Next"
        Me.btnNext.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.Location = New System.Drawing.Point(16, 57)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(438, 46)
        Me.Label2.TabIndex = 34
        Me.Label2.Text = "Thanks for using this Software, If you like this Software, Please consider buying" &
    " it."
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Font = New System.Drawing.Font("Segoe UI", 11.0!)
        Me.Label1.ForeColor = System.Drawing.Color.SteelBlue
        Me.Label1.Location = New System.Drawing.Point(16, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(189, 20)
        Me.Label1.TabIndex = 33
        Me.Label1.Text = "Launch Manager Activation"
        '
        'rbTry
        '
        Me.rbTry.AutoSize = True
        Me.rbTry.Location = New System.Drawing.Point(63, 194)
        Me.rbTry.Name = "rbTry"
        Me.rbTry.Size = New System.Drawing.Size(223, 19)
        Me.rbTry.TabIndex = 39
        Me.rbTry.Text = "I want to continue evaluating for now"
        Me.rbTry.UseVisualStyleBackColor = True
        '
        'frmActivate
        '
        Me.AcceptButton = Me.btnNext
        Me.AutoScaleDimensions = New System.Drawing.SizeF(7.0!, 15.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.CancelButton = Me.BtnCancel
        Me.ClientSize = New System.Drawing.Size(484, 431)
        Me.Controls.Add(Me.rbTry)
        Me.Controls.Add(Me.rbActivate)
        Me.Controls.Add(Me.rbPurchase)
        Me.Controls.Add(Me.BtnCancel)
        Me.Controls.Add(Me.btnNext)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Font = New System.Drawing.Font("Segoe UI", 9.0!)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.Icon = CType(resources.GetObject("$this.Icon"), System.Drawing.Icon)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmActivate"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Activate Launch Manager"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents rbActivate As RadioButton
    Friend WithEvents rbPurchase As RadioButton
    Friend WithEvents BtnCancel As Button
    Friend WithEvents btnNext As Button
    Friend WithEvents Label2 As Label
    Friend WithEvents Label1 As Label
    Friend WithEvents rbTry As RadioButton
End Class
