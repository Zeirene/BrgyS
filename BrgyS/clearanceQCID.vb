Public Class clearanceQCID
    Private Sub clearanceQCID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2TextBox1.Text = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text
        Guna2TextBox3.Text = Form3.Guna2TextBox9.Text
    End Sub
End Class