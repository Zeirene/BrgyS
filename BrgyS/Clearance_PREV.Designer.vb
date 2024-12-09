<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Clearance_PREV
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
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(Clearance_PREV))
        Dim CustomizableEdges1 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges2 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges3 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        Dim CustomizableEdges4 As Guna.UI2.WinForms.Suite.CustomizableEdges = New Guna.UI2.WinForms.Suite.CustomizableEdges()
        PictureBox1 = New PictureBox()
        RichTextBox1 = New RichTextBox()
        RichTextBox2 = New RichTextBox()
        RichTextBox3 = New RichTextBox()
        Guna2Button4 = New Guna.UI2.WinForms.Guna2Button()
        Guna2Button3 = New Guna.UI2.WinForms.Guna2Button()
        CType(PictureBox1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' PictureBox1
        ' 
        PictureBox1.Image = CType(resources.GetObject("PictureBox1.Image"), Image)
        PictureBox1.Location = New Point(1, 1)
        PictureBox1.Name = "PictureBox1"
        PictureBox1.Size = New Size(522, 735)
        PictureBox1.SizeMode = PictureBoxSizeMode.StretchImage
        PictureBox1.TabIndex = 4
        PictureBox1.TabStop = False
        ' 
        ' RichTextBox1
        ' 
        RichTextBox1.BackColor = Color.White
        RichTextBox1.BorderStyle = BorderStyle.None
        RichTextBox1.Font = New Font("Arial", 8F)
        RichTextBox1.Location = New Point(55, 214)
        RichTextBox1.Name = "RichTextBox1"
        RichTextBox1.ReadOnly = True
        RichTextBox1.Size = New Size(416, 126)
        RichTextBox1.TabIndex = 8
        RichTextBox1.Text = "This is to certify that {name}, born on  {BDAY}, of  {stat} status, is a bona fide resident of {add}, Barangay Sta. Lucia, Quezon City, and has been residing in this barangay for  {stay} years."
        ' 
        ' RichTextBox2
        ' 
        RichTextBox2.BackColor = Color.White
        RichTextBox2.BorderStyle = BorderStyle.None
        RichTextBox2.Font = New Font("Arial", 8F)
        RichTextBox2.Location = New Point(55, 416)
        RichTextBox2.Name = "RichTextBox2"
        RichTextBox2.ReadOnly = True
        RichTextBox2.Size = New Size(416, 41)
        RichTextBox2.TabIndex = 9
        RichTextBox2.Text = "This certification is issued upon the request of the above-named resident for the purpose of {purp}."
        ' 
        ' RichTextBox3
        ' 
        RichTextBox3.BackColor = Color.White
        RichTextBox3.BorderStyle = BorderStyle.None
        RichTextBox3.Font = New Font("Arial", 8F)
        RichTextBox3.Location = New Point(55, 479)
        RichTextBox3.Name = "RichTextBox3"
        RichTextBox3.ReadOnly = True
        RichTextBox3.Size = New Size(416, 41)
        RichTextBox3.TabIndex = 10
        RichTextBox3.Text = "Issued this {day} of {monthyear} at Barangay Sta. Lucia, Quezon City, Philippines."
        ' 
        ' Guna2Button4
        ' 
        Guna2Button4.Animated = True
        Guna2Button4.BorderColor = Color.Firebrick
        Guna2Button4.BorderRadius = 18
        Guna2Button4.BorderThickness = 2
        Guna2Button4.CustomBorderColor = Color.Firebrick
        Guna2Button4.CustomizableEdges = CustomizableEdges1
        Guna2Button4.DisabledState.BorderColor = Color.DarkGray
        Guna2Button4.DisabledState.CustomBorderColor = Color.DarkGray
        Guna2Button4.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        Guna2Button4.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        Guna2Button4.FillColor = Color.Firebrick
        Guna2Button4.Font = New Font("Bahnschrift", 9.75F, FontStyle.Bold)
        Guna2Button4.ForeColor = Color.White
        Guna2Button4.Location = New Point(554, 370)
        Guna2Button4.Name = "Guna2Button4"
        Guna2Button4.PressedColor = Color.Firebrick
        Guna2Button4.ShadowDecoration.CustomizableEdges = CustomizableEdges2
        Guna2Button4.Size = New Size(180, 44)
        Guna2Button4.TabIndex = 18
        Guna2Button4.Text = "CANCEL"
        ' 
        ' Guna2Button3
        ' 
        Guna2Button3.Animated = True
        Guna2Button3.BorderRadius = 18
        Guna2Button3.BorderThickness = 2
        Guna2Button3.CustomizableEdges = CustomizableEdges3
        Guna2Button3.DisabledState.BorderColor = Color.DarkGray
        Guna2Button3.DisabledState.CustomBorderColor = Color.DarkGray
        Guna2Button3.DisabledState.FillColor = Color.FromArgb(CByte(169), CByte(169), CByte(169))
        Guna2Button3.DisabledState.ForeColor = Color.FromArgb(CByte(141), CByte(141), CByte(141))
        Guna2Button3.FillColor = Color.Black
        Guna2Button3.Font = New Font("Bahnschrift", 9.75F, FontStyle.Bold)
        Guna2Button3.ForeColor = Color.White
        Guna2Button3.Location = New Point(554, 320)
        Guna2Button3.Name = "Guna2Button3"
        Guna2Button3.ShadowDecoration.CustomizableEdges = CustomizableEdges4
        Guna2Button3.Size = New Size(180, 44)
        Guna2Button3.TabIndex = 17
        Guna2Button3.Text = "PRINT"
        ' 
        ' Clearance_PREV
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(765, 738)
        Controls.Add(Guna2Button4)
        Controls.Add(Guna2Button3)
        Controls.Add(RichTextBox3)
        Controls.Add(RichTextBox2)
        Controls.Add(RichTextBox1)
        Controls.Add(PictureBox1)
        FormBorderStyle = FormBorderStyle.None
        Name = "Clearance_PREV"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Clearance_PREV"
        CType(PictureBox1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub
    Friend WithEvents PictureBox1 As PictureBox
    Friend WithEvents RichTextBox1 As RichTextBox
    Friend WithEvents RichTextBox2 As RichTextBox
    Friend WithEvents RichTextBox3 As RichTextBox
    Friend WithEvents Guna2Button4 As Guna.UI2.WinForms.Guna2Button
    Friend WithEvents Guna2Button3 As Guna.UI2.WinForms.Guna2Button
End Class
