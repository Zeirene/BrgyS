Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports BrgyS.ApiResponse


Public Class Form6
    Private WithEvents debounceTimer As New Timer() ' Timer to debounce the TextChanged event
    Private debounceDelay As Integer = 300 ' Delay in milliseconds

    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'UpdateBusinesses()
        LoadData()
    End Sub

    Private Async Sub LoadData()
        Dim apiClient As New ApiClient()

        ' Fetch data from APIs
        Dim residents As List(Of ApiResponse.ResidentRecord) = Await apiClient.GetResidentRecordsAsync()
        Dim transactionLogs As List(Of ApiResponse.TransactionLog) = Await apiClient.GetTransactionLogsAsync()
        Dim permits As List(Of ApiResponse.PermitLog) = Await apiClient.GetPermitLogsAsync()

        ' Map permits to resident IDs
        Dim permitDictionary As New Dictionary(Of Long, ApiResponse.PermitLog)
        For Each permit In permits
            Dim residentId As Long
            If Long.TryParse(permit.ResidentId, residentId) Then
                permitDictionary(residentId) = permit
            End If
        Next

        ' Map resident ID to name
        Dim residentDictionary As New Dictionary(Of Long, String)
        For Each resident In residents
            residentDictionary(resident.ResidentId) = $"{resident.ResidentFirstName} {resident.ResidentLastName}"
        Next

        ' Clear existing rows
        Guna2DataGridView1.Rows.Clear()

        ' Populate DataGridView
        For Each log In transactionLogs
            Dim residentName As String = If(residentDictionary.ContainsKey(log.ResidentId), residentDictionary(log.ResidentId), "Unknown Resident")
            Dim permitLog As ApiResponse.PermitLog = If(permitDictionary.ContainsKey(log.ResidentId), permitDictionary(log.ResidentId), Nothing)

            ' Get details from permit log
            Dim residentid As String = If(permitLog IsNot Nothing, permitLog.ResidentId.ToString(), "N/A")
            Dim permitsLogId As String = If(permitLog IsNot Nothing, permitLog.PLogId.ToString(), "N/A")
            Dim businessName As String = If(permitLog IsNot Nothing, permitLog.BName, "N/A")
            Dim businessAddress As String = If(permitLog IsNot Nothing, permitLog.BAddress, "N/A")
            Dim status As String = log.Status

            ' Add row to DataGridView
            Guna2DataGridView1.Rows.Add(residentid, residentName, permitsLogId, businessName, businessAddress, status)
        Next
    End Sub

    'Private Async Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs)
    '    Try
    '        ' Ensure the clicked row and column are valid
    '        If e.RowIndex < 0 OrElse e.ColumnIndex < 0 Then Exit Sub

    '        ' Retrieve the ResidentId from the DataGridView
    '        Dim residentId As String = Guna2DataGridView1.Rows(e.RowIndex).Cells("ResidentIdColumn").Value.ToString()

    '        ' Ensure ResidentId is valid
    '        If String.IsNullOrWhiteSpace(residentId) OrElse Not Long.TryParse(residentId, Nothing) Then
    '            MessageBox.Show("Invalid Resident ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End If

    '        ' Fetch the resident's details asynchronously
    '        Dim residentEmail As String = Await GetResidentEmailAsync(Long.Parse(residentId))
    '        If String.IsNullOrWhiteSpace(residentEmail) Then
    '            MessageBox.Show("Resident email not found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '            Exit Sub
    '        End If

    '        ' Retrieve the resident name
    '        Dim residentName = If(residentDictionary.ContainsKey(Long.Parse(residentId)), residentDictionary(Long.Parse(residentId)), "Unknown Resident")

    '        ' Retrieve the approval/rejection status
    '        Dim status As String = Guna2DataGridView1.Rows(e.RowIndex).Cells("StatusColumn").Value.ToString()

    '        ' Compose the email body
    '        Dim emailSubject = $"Your Application Status: {status}"
    '        Dim emailBody = $"Dear {residentName}," & vbCrLf &
    '                              vbCrLf &
    '                              $"We are writing to inform you that your application has been {status}." & vbCrLf &
    '                              "For further details, please contact us at our office." & vbCrLf &
    '                              vbCrLf &
    '                              "Thank you," & vbCrLf &
    '                              "Your Organization"

    '        ' Open the default email client with the email pre-filled
    '        Dim mailtoUri = $"mailto:{residentEmail}?subject={Uri.EscapeDataString(emailSubject)}&body={Uri.EscapeDataString(emailBody)}"
    '        Process.Start(mailtoUri)

    '    Catch ex As Exception
    '        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    End Try
    'End Sub

    Private Sub Guna2DataGridView1_CellContentClick_1(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

    End Sub

    'Private Async Function GetResidentEmailAsync(residentId As Long) As Task(Of String)
    '    Try
    '        ' Fetch the resident's email using the API
    '        Dim apiClient As New ApiClient()
    '        Dim residentDetails As ApiResponse.ResidentRecord = Await apiClient.GetResidentRecordsAsync(residentId)
    '        Return residentDetails?.ResidentEmail
    '    Catch ex As Exception
    '        MessageBox.Show($"Failed to fetch resident email: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '        Return Nothing
    '    End Try
    'End Function
End Class
