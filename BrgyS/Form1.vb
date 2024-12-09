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

    Private Async Sub mylogin()
        ' Fetch account records using API
        Dim client As New ApiClient()

        ' Fetch the list of account records
        Dim accountRecords As List(Of ApiResponse.Account) = Await client.GetAccountRecordsAsync()

        ' Check if account records are available
        If accountRecords IsNot Nothing AndAlso accountRecords.Any() Then
            ' Iterate through each account record to match the username and password
            For Each account As ApiResponse.Account In accountRecords
                ' Check if the entered username and password match any account from the API response
                If account.BrgyEmail = Guna2TextBox1.Text AndAlso account.BrgyPassword = Guna2TextBox2.Text Then
                    ' If match found, proceed to Form2
                    Form2.staffID = account.BrgyUserId.ToString() ' Use brgy_user_id for staffID
                    Form2.Show()
                    Me.Hide()
                    Exit Sub ' Exit the loop once a valid login is found
                End If
            Next

            ' If no match is found, show an error message
            MessageBox.Show("Invalid username or password. Please try again.", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        Else
            ' If no account records found, show an error message
            MessageBox.Show("No account records found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub


    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Form2.Close()
        Me.Dispose()
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


    Private Async Sub FetchAccount()

        Dim client As New ApiClient()

        ' Fetch the list of account records
        Dim accountRecords As List(Of ApiResponse.Account) = Await client.GetAccountRecordsAsync()

        ' Display account details for each record in the list using MessageBox
        For Each account As ApiResponse.Account In accountRecords
            MessageBox.Show($"User ID: {account.BrgyUserId}" & Environment.NewLine &
                    $"Account ID: {account.BrgyAccountId}" & Environment.NewLine &
                    $"Email: {account.BrgyEmail}")
            ' You can also display other properties as needed
        Next

    End Sub


    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'FetchResidents()



    End Sub

    Private Sub Guna2HtmlLabel1_Click(sender As Object, e As EventArgs) Handles Guna2HtmlLabel1.Click

    End Sub

    Private Sub Guna2PictureBox1_Click(sender As Object, e As EventArgs) Handles Guna2PictureBox1.Click

    End Sub
End Class
