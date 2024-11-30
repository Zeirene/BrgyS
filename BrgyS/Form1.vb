Imports System.Runtime.ConstrainedExecution
Imports MySql.Data.MySqlClient
Public Class Form1
    ' Variable to track whether the password is shown or hidden
    Private isPasswordVisible As Boolean = False

    ' Logic for logging in when Guna2Button1 is clicked
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        mylogin()

        'If Guna2TextBox1.Text = "admin" AndAlso Guna2TextBox2.Text = "123" Then
        '    ' Open Form2 if the credentials are correct
        '    Dim form2 As New Form2()
        '    form2.Show()
        '    Me.Hide() ' Hide Form1
        'Else
        '    ' Show error message if the credentials are incorrect
        '    MessageBox.Show("Incorrect username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        'End If
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        If isPasswordVisible Then
            ' If the password is visible, hide it by setting PasswordChar back to '*'
            Guna2TextBox2.PasswordChar = "*"
            isPasswordVisible = False
        Else
            ' If the password is hidden, show it by setting PasswordChar to an empty string
            Guna2TextBox2.PasswordChar = ""
            isPasswordVisible = True
        End If
    End Sub

    Private Sub mylogin()
        Try
            openCon()

            Using command As New MySqlCommand("SELECT * FROM staff_info WHERE user = @username  AND pass = @password", con)
                command.Parameters.Add("@username", MySqlDbType.VarChar).Value = Guna2TextBox1.Text
                command.Parameters.Add("@password", MySqlDbType.VarChar).Value = Guna2TextBox2.Text

                Dim adapter As New MySqlDataAdapter(command)
                Dim table As New DataTable
                adapter.Fill(table)

                If Guna2TextBox1.Text = "" Or
                   Guna2TextBox2.Text = "" Then
                    MsgBox("Please Fill All Fields!")
                ElseIf table.Rows.Count = 0 Then
                    MsgBox("Invalid username or password. Please try again.", MsgBoxStyle.Exclamation, "Login Error")
                Else
                    ' MsgBox("Login successful!", MsgBoxStyle.Information, "Success")
                    Me.Hide()
                    con.Close()
                    Form2.staffID = table.Rows(0)("staff_id").ToString
                    'brgyID.staffID = table.Rows(0)("staff_id").ToString

                    Form2.Show()

                End If
            End Using
        Catch ex As Exception
            MsgBox("An error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Me.Dispose()
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged

    End Sub

    Public Async Sub FetchResidents()
        Dim client As New ApiClient()

        Try
            Dim residents As List(Of ApiResponse.ResidentRecord) = Await client.GetResidentRecordsAsync()

            ' Testing if it's working 
            For Each resident In residents
                Guna2TextBox1.AppendText($"Name: {resident.ResidentFirstName} {resident.ResidentLastName}, Email: {resident.ResidentEmail}{Environment.NewLine}")

            Next
        Catch ex As Exception
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        FetchResidents()
    End Sub

    Private Sub Guna2HtmlLabel1_Click(sender As Object, e As EventArgs) Handles Guna2HtmlLabel1.Click

    End Sub
End Class
