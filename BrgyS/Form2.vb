Imports Avalonia.Controls.Documents
Imports Avalonia.Styling
Imports MySql.Data.MySqlClient

Public Class Form2
    Private _staffID As String
    Public Property staffID() As String
        Get
            Return _staffID
        End Get
        Set(value As String)
            _staffID = value
            Try
                openCon()
                Using cmd As New MySqlCommand("SELECT * FROM staff_info WHERE staff_id = @user", con)
                    cmd.Parameters.AddWithValue("@user", value)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Dim lname = reader("last_name").ToString()
                            Dim gname = reader("given_name").ToString()
                            Dim mname = reader("middle_name").ToString()
                            Guna2HtmlLabel4.Text = lname + "," + gname + " " + mname
                        Else
                            Guna2HtmlLabel4.Text = "?"
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                con.Close()
            End Try
        End Set
    End Property
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set time and name for clerk after login
        switchPanel(Form3)
        Timer1.Interval = 1000     ' Set the interval to 1 second (1000 milliseconds)
        Timer1.Start()

    End Sub
    Private Sub Guna2CircleButton3_Click(sender As Object, e As EventArgs)
        WindowState = FormWindowState.Minimized
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        switchPanel(Form3)
        Guna2Button1.FillColor = Color.FromArgb(30, 71, 125)
        Guna2Button1.ForeColor = Color.White
        Guna2Button1.Font = New Font(Guna2Button1.Font, FontStyle.Bold)
        Guna2Button2.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button2.FillColor = Color.White
        Guna2Button2.ForeColor = Color.Black
        Guna2Button3.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button3.FillColor = Color.White
        Guna2Button3.ForeColor = Color.Black
        Guna2Button5.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button5.FillColor = Color.White
        Guna2Button5.ForeColor = Color.Black
    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        switchPanel(Form5)
        Guna2Button1.FillColor = Color.White
        Guna2Button1.ForeColor = Color.Black
        Guna2Button1.Font = New Font(Guna2Button1.Font, FontStyle.Regular)
        Guna2Button3.FillColor = Color.White
        Guna2Button3.ForeColor = Color.Black
        Guna2Button3.Font = New Font(Guna2Button1.Font, FontStyle.Regular)
        Guna2Button2.Font = New Font(Guna2Button2.Font, FontStyle.Bold)
        Guna2Button2.FillColor = Color.FromArgb(30, 71, 125)
        Guna2Button2.ForeColor = Color.White
        Guna2Button5.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button5.FillColor = Color.White
        Guna2Button5.ForeColor = Color.Black
    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        switchPanel(Form6)
        Guna2Button3.FillColor = Color.FromArgb(30, 71, 125)
        Guna2Button3.ForeColor = Color.White
        Guna2Button3.Font = New Font(Guna2Button1.Font, FontStyle.Bold)
        Guna2Button1.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button1.FillColor = Color.White
        Guna2Button1.ForeColor = Color.Black
        Guna2Button2.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button2.FillColor = Color.White
        Guna2Button2.ForeColor = Color.Black
        Guna2Button5.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button5.FillColor = Color.White
        Guna2Button5.ForeColor = Color.Black
    End Sub
    Private Sub Guna2Button5_Click(sender As Object, e As EventArgs) Handles Guna2Button5.Click
        Dim result As DialogResult = MessageBox.Show("Are you sure you want to log out?", "Confirm Logout", MessageBoxButtons.YesNo, MessageBoxIcon.Question)

        If result = DialogResult.Yes Then
            Dim form1 As New Form1()
            form1.Show() ' Show the login form (Form1) again
            Me.Dispose() ' Close the current form (Form2) to log out
        End If

        Guna2Button5.FillColor = Color.FromArgb(30, 71, 125)
        Guna2Button5.ForeColor = Color.White
        Guna2Button5.Font = New Font(Guna2Button1.Font, FontStyle.Bold)
        Guna2Button1.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button1.FillColor = Color.White
        Guna2Button1.ForeColor = Color.Black
        Guna2Button2.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button2.FillColor = Color.White
        Guna2Button2.ForeColor = Color.Black
        Guna2Button3.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button3.FillColor = Color.White
        Guna2Button3.ForeColor = Color.Black
        Guna2Button4.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button4.FillColor = Color.White
        Guna2Button4.ForeColor = Color.Black
    End Sub
    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Guna2HtmlLabel5.Text = DateTime.Now.ToString("HH:mm:ss")
    End Sub

    'funtions////////////////////////////////////////////////////////////////////

    Sub switchPanel(ByVal panel As Form)
        Guna2Panel1.Controls.Clear()
        panel.TopLevel = False
        Guna2Panel1.Controls.Add(panel)
        panel.Show()
    End Sub

End Class