Imports BrgyS.ApiResponse
Imports MySql.Data.MySqlClient

Public Class Form5
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data's into table
        loadinfo()
    End Sub

    Public Async Sub loadinfo()
        Guna2DataGridView1.Rows.Clear()

        ' Create an instance of ApiClient
        Dim apiClient As New ApiClient()

        ' Fetch the resident information from the API
        Dim residents As List(Of ApiResponse.ResidentRecord) = Await apiClient.GetResidentRecordsAsync()

        ' Convert the list of ResidentRecord to a dictionary (mapping resident_id to resident_name)
        Dim residentDictionary As New Dictionary(Of Long, String)() ' Use Long as key type here
        For Each resident In residents
            ' Ensure the ResidentId is properly treated as Long
            Dim residentId As Long = Convert.ToInt64(resident.ResidentId)

            ' Check if the residentId is within the valid range of Long
            If residentId >= Long.MinValue And residentId <= Long.MaxValue Then
                residentDictionary.Add(residentId, $"{resident.ResidentFirstName} {resident.ResidentLastName}")
            Else
                ' Handle invalid ResidentId case if needed
                MessageBox.Show("Invalid ResidentId", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Next

        ' Now, fetch the transaction data from the API
        Try
            ' Fetch transaction logs from the API
            Dim transactionLogs As List(Of TransactionLog) = Await apiClient.GetTransactionLogsAsync()

            ' Fetch account details (this is used to map staff_id to their full name)
            Dim accountRecords As List(Of ApiResponse.Account) = Await apiClient.GetAccountRecordsAsync()

            ' Loop through the transaction logs and display them in the DataGridView
            For Each log As TransactionLog In transactionLogs
                ' Fetch the resident name from the dictionary using the resident_id
                Dim residentName As String = If(residentDictionary.ContainsKey(log.ResidentId), residentDictionary(log.ResidentId), "Unknown Resident")

                ' Convert staff_id (string) to long for comparison
                Dim staffIdLong As Long = 0
                If Long.TryParse(log.StaffId, staffIdLong) Then
                    ' Find the staff account details using staff_id
                    Dim staffAccount As ApiResponse.Account = accountRecords.FirstOrDefault(Function(acc) acc.BrgyUserId = staffIdLong)

                    ' Get the staff name, if the account is found
                    Dim staffName As String = If(staffAccount IsNot Nothing, $"{staffAccount.BrgyFirstName} {staffAccount.BrgyLastName}", "Unknown Staff")

                    ' Add a new row to the DataGridView with the transaction log details and staff name
                    Guna2DataGridView1.Rows.Add(
                    residentName,
                    staffName,  ' Display staff name instead of ID
                    $"{log.LogDate} {log.LogTime}",
                    log.Payment,
                    log.Type,
                    log.Status
                )
                Else
                    ' Handle the case where staff_id can't be converted to long (invalid staff_id format)
                    Guna2DataGridView1.Rows.Add(
                    residentName,
                    "Invalid Staff ID",  ' Display an error message if staff_id is invalid
                    $"{log.LogDate} {log.LogTime}",
                    log.Payment,
                    log.Type,
                    log.Status
                )
                End If
            Next

        Catch ex As Exception
            ' Show an error message if an exception occurs
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub


    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

    End Sub
End Class
