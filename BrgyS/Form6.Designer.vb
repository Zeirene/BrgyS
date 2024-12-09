<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class Form6
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
        Dim DataGridViewCellStyle1 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle2 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Dim DataGridViewCellStyle3 As DataGridViewCellStyle = New DataGridViewCellStyle()
        Guna2DataGridView1 = New Guna.UI2.WinForms.Guna2DataGridView()
        Names = New DataGridViewTextBoxColumn()
        Column4 = New DataGridViewTextBoxColumn()
        Column2 = New DataGridViewTextBoxColumn()
        Column3 = New DataGridViewTextBoxColumn()
        Column5 = New DataGridViewTextBoxColumn()
        Column1 = New DataGridViewTextBoxColumn()
        CType(Guna2DataGridView1, ComponentModel.ISupportInitialize).BeginInit()
        SuspendLayout()
        ' 
        ' Guna2DataGridView1
        ' 
        Guna2DataGridView1.AllowUserToAddRows = False
        Guna2DataGridView1.AllowUserToDeleteRows = False
        DataGridViewCellStyle1.BackColor = Color.FromArgb(CByte(44), CByte(48), CByte(52))
        Guna2DataGridView1.AlternatingRowsDefaultCellStyle = DataGridViewCellStyle1
        Guna2DataGridView1.BorderStyle = BorderStyle.FixedSingle
        DataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle2.BackColor = Color.FromArgb(CByte(15), CByte(16), CByte(18))
        DataGridViewCellStyle2.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle2.ForeColor = Color.White
        DataGridViewCellStyle2.SelectionBackColor = SystemColors.Highlight
        DataGridViewCellStyle2.SelectionForeColor = SystemColors.HighlightText
        DataGridViewCellStyle2.WrapMode = DataGridViewTriState.True
        Guna2DataGridView1.ColumnHeadersDefaultCellStyle = DataGridViewCellStyle2
        Guna2DataGridView1.ColumnHeadersHeight = 17
        Guna2DataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Guna2DataGridView1.Columns.AddRange(New DataGridViewColumn() {Names, Column4, Column2, Column3, Column5, Column1})
        DataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleLeft
        DataGridViewCellStyle3.BackColor = Color.FromArgb(CByte(33), CByte(37), CByte(41))
        DataGridViewCellStyle3.Font = New Font("Segoe UI", 9F)
        DataGridViewCellStyle3.ForeColor = Color.White
        DataGridViewCellStyle3.SelectionBackColor = Color.FromArgb(CByte(114), CByte(117), CByte(119))
        DataGridViewCellStyle3.SelectionForeColor = Color.White
        DataGridViewCellStyle3.WrapMode = DataGridViewTriState.False
        Guna2DataGridView1.DefaultCellStyle = DataGridViewCellStyle3
        Guna2DataGridView1.GridColor = Color.FromArgb(CByte(50), CByte(56), CByte(62))
        Guna2DataGridView1.Location = New Point(3, 12)
        Guna2DataGridView1.Name = "Guna2DataGridView1"
        Guna2DataGridView1.ReadOnly = True
        Guna2DataGridView1.RowHeadersVisible = False
        Guna2DataGridView1.Size = New Size(836, 583)
        Guna2DataGridView1.TabIndex = 12
        Guna2DataGridView1.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Dark
        Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(CByte(44), CByte(48), CByte(52))
        Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.Font = Nothing
        Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty
        Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty
        Guna2DataGridView1.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty
        Guna2DataGridView1.ThemeStyle.BackColor = Color.White
        Guna2DataGridView1.ThemeStyle.GridColor = Color.FromArgb(CByte(50), CByte(56), CByte(62))
        Guna2DataGridView1.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(CByte(15), CByte(16), CByte(18))
        Guna2DataGridView1.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None
        Guna2DataGridView1.ThemeStyle.HeaderStyle.Font = New Font("Segoe UI", 9F)
        Guna2DataGridView1.ThemeStyle.HeaderStyle.ForeColor = Color.White
        Guna2DataGridView1.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing
        Guna2DataGridView1.ThemeStyle.HeaderStyle.Height = 17
        Guna2DataGridView1.ThemeStyle.ReadOnly = True
        Guna2DataGridView1.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(CByte(33), CByte(37), CByte(41))
        Guna2DataGridView1.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal
        Guna2DataGridView1.ThemeStyle.RowsStyle.Font = New Font("Segoe UI", 9F)
        Guna2DataGridView1.ThemeStyle.RowsStyle.ForeColor = Color.White
        Guna2DataGridView1.ThemeStyle.RowsStyle.Height = 25
        Guna2DataGridView1.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(CByte(114), CByte(117), CByte(119))
        Guna2DataGridView1.ThemeStyle.RowsStyle.SelectionForeColor = Color.White
        ' 
        ' Names
        ' 
        Names.HeaderText = "Resident ID"
        Names.Name = "Names"
        Names.ReadOnly = True
        ' 
        ' Column4
        ' 
        Column4.HeaderText = "Resident Name"
        Column4.Name = "Column4"
        Column4.ReadOnly = True
        ' 
        ' Column2
        ' 
        Column2.HeaderText = "Permit Log ID"
        Column2.Name = "Column2"
        Column2.ReadOnly = True
        ' 
        ' Column3
        ' 
        Column3.HeaderText = "Business Name"
        Column3.Name = "Column3"
        Column3.ReadOnly = True
        ' 
        ' Column5
        ' 
        Column5.HeaderText = "Business Address"
        Column5.Name = "Column5"
        Column5.ReadOnly = True
        ' 
        ' Column1
        ' 
        Column1.HeaderText = "Status"
        Column1.Name = "Column1"
        Column1.ReadOnly = True
        ' 
        ' Form6
        ' 
        AutoScaleDimensions = New SizeF(7F, 15F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = Color.White
        ClientSize = New Size(842, 598)
        ControlBox = False
        Controls.Add(Guna2DataGridView1)
        FormBorderStyle = FormBorderStyle.None
        MaximizeBox = False
        MdiChildrenMinimizedAnchorBottom = False
        MinimizeBox = False
        Name = "Form6"
        StartPosition = FormStartPosition.CenterScreen
        Text = "Form6"
        CType(Guna2DataGridView1, ComponentModel.ISupportInitialize).EndInit()
        ResumeLayout(False)
    End Sub

    Friend WithEvents Guna2DataGridView1 As Guna.UI2.WinForms.Guna2DataGridView
    Friend WithEvents Names As DataGridViewTextBoxColumn
    Friend WithEvents Column4 As DataGridViewTextBoxColumn
    Friend WithEvents Column2 As DataGridViewTextBoxColumn
    Friend WithEvents Column3 As DataGridViewTextBoxColumn
    Friend WithEvents Column5 As DataGridViewTextBoxColumn
    Friend WithEvents Column1 As DataGridViewTextBoxColumn
End Class
