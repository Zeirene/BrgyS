Imports System.Runtime.ConstrainedExecution
Public Class Form1
    ' Variable to track whether the password is shown or hidden
    Private isPasswordVisible As Boolean = False
    Public Sub New()
        InitializeComponent()
        Me.KeyPreview = True ' This allows the form to capture key presses
    End Sub

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
    End Sub

    Private Sub Guna2CircleButton1_Click_1(sender As Object, e As EventArgs)
        End
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        Guna2TextBox1.Clear()
        Guna2TextBox2.Clear()
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged

    End Sub

    Private Sub Guna2TextBox2_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox2.TextChanged

    End Sub
    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        ' Check if the ENTER key is pressed
        If e.KeyCode = Keys.Enter Then
            Guna2Button1.PerformClick() ' Trigger the button click event
        End If
    End Sub

    ' Logic for logging in when Guna2Button1 is clicked
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If Guna2TextBox1.Text = "user" AndAlso Guna2TextBox2.Text = "passw" Then
            ' Open Form2 if the credentials are correct
            Dim form2 As New Form2()
            form2.Show()
            Me.Hide() ' Hide Form1
        Else
            ' Show error message if the credentials are incorrect
            MessageBox.Show("Incorrect username or password!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    ' Logic to toggle password visibility when Guna2CircleButton2 is clicked


    Private Sub Guna2CircleButton3_Click(sender As Object, e As EventArgs)
        WindowState = FormWindowState.Minimized
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
End Class
