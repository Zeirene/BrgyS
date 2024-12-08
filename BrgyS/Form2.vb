Imports Avalonia.Controls.Documents
Imports Avalonia.Styling
Imports BrgyS.ApiResponse
Imports MySql.Data.MySqlClient

Public Class Form2
    Private _staffID As String


    Public Property staffID() As String
        Get
            Return _staffID
        End Get
        Set(value As String)
            _staffID = value
            ' Call the async method to fetch account details
            FetchAccountDetailsAsync(value)
        End Set
    End Property

    ' Asynchronous method to fetch account details
    Private Async Sub FetchAccountDetailsAsync(staffID As String)
        Try
            ' Assuming ApiClient is a class that can fetch account details from the API
            Dim client As New ApiClient()

            ' Fetch account records from the API
            Dim accountRecords As List(Of ApiResponse.Account) = Await client.GetAccountRecordsAsync()

            ' Look for the user matching the staffID
            Dim user = accountRecords.FirstOrDefault(Function(acc) acc.BrgyUserId.ToString() = staffID)

            If user IsNot Nothing Then
                ' Display the full name (first name + last name) in Guna2HtmlLabel4
                Guna2HtmlLabel4.Text = user.BrgyLastName + ", " + user.BrgyFirstName
            Else
                ' If no matching user is found, set the label to "?"
                Guna2HtmlLabel4.Text = "?"
            End If
        Catch ex As Exception
            MsgBox(ex.Message)
        End Try
    End Sub


    Private Async Sub FetchTransactionLogsAsync()
        Dim client As New ApiClient()
        Try
            ' Call the GetTransactionLogsAsync function
            Dim transactionLogs As List(Of TransactionLog) = Await client.GetTransactionLogsAsync()

            ' Process the transaction logs (for example, display them in a ListBox or DataGridView)
            For Each log As TransactionLog In transactionLogs
                ' Display each transaction log in a MessageBox
                MessageBox.Show($"Log ID: {log.LogId}{Environment.NewLine}" &
                            $"Date: {log.LogDate}{Environment.NewLine}" &
                            $"Status: {log.Status}{Environment.NewLine}" &
                            $"Payment: {log.Payment}",
                            "Transaction Log",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information)
            Next
        Catch ex As Exception
            ' Handle any exceptions that occur during the API call
            MsgBox("An error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
        End Try
    End Sub


    Public Async Sub InsertPermitLog()
        Try
            Dim apiClient As New ApiClient()
            Dim newLog As New PermitLog With {
            .ResidentId = "987654321",
            .BDetails = "New Building Details",
            .LocType = "apartment",
            .BAddress = "456 New Address, City, Country",
            .BName = "New Apartments",
            .MRental = 2000,
            .LogId = "999999999",
            .StayDuration = "24 months",
            .PLogId = 1234567890123
        }

            Dim result As Boolean = Await apiClient.InsertPermitLogAsync(newLog)

            If result Then
                MessageBox.Show("Permit log inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to insert permit log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show("Error inserting permit log: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'Set time and name for clerk after login
        switchPanel(Form3)
        Timer1.Interval = 1000     ' Set the interval to 1 second (1000 milliseconds)
        Timer1.Start()
        'InsertPermitLog()
        ' FetchTransactionLogsAsync()

    End Sub
    Private Sub Form2_FormClosed(sender As Object, e As FormClosedEventArgs) Handles Me.FormClosed
        ' Close all open forms
        Application.Exit()
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

        Guna2Button5.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button5.FillColor = Color.White
        Guna2Button5.ForeColor = Color.Black
    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        switchPanel(Form5)
        Guna2Button1.FillColor = Color.White
        Guna2Button1.ForeColor = Color.Black
        Guna2Button1.Font = New Font(Guna2Button1.Font, FontStyle.Regular)

        Guna2Button2.Font = New Font(Guna2Button2.Font, FontStyle.Bold)
        Guna2Button2.FillColor = Color.FromArgb(30, 71, 125)
        Guna2Button2.ForeColor = Color.White
        Guna2Button5.Font = New Font(Guna2Button2.Font, FontStyle.Regular)
        Guna2Button5.FillColor = Color.White
        Guna2Button5.ForeColor = Color.Black
    End Sub
    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        switchPanel(Form6)

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