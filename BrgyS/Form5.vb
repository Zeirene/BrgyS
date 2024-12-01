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

        ' Now, fetch the transaction data from the database
        openCon()
        Try
            ' SQL query to fetch the transaction data (ensure resident_id is returned as VARCHAR)
            Dim query As String = "
                SELECT 
                    tl.resident_id, 
                    si.staff_id, 
                    CONCAT(si.last_name, ', ', si.given_name, ' ', si.middle_name) AS StaffName, 
                    CONCAT(tl.log_date, ' ', tl.log_time) AS Transaction_data,
                    tl.payment,
                    CASE 
                        WHEN tl.type = 'A' THEN 'Barangay ID'
                        WHEN tl.type = 'B' THEN 'Barangay Clearance'
                        WHEN tl.type = 'Z' THEN 'Certification'
                        WHEN tl.type = 'D' THEN 'Business Permit'
                        WHEN tl.type = 'E' THEN 'Residency for QCID'
                        WHEN tl.type = 'F' THEN 'Burial/Medical & Financial/Educational/Philhealth'
                        WHEN tl.type = 'I' THEN 'ID (TEST)'
                        WHEN tl.type = 'P' THEN 'PERMIT (TEST)'
                        WHEN tl.type = 'C' THEN 'CLEARANCE (TEST)'
                        ELSE 'Unknown'
                    END AS Type,
                    CASE 
                        WHEN tl.status = 'A' THEN 'Accepted'
                        WHEN tl.status = 'R' THEN 'Rejected'
                        WHEN tl.status = 'P' THEN 'Pending'
                        WHEN tl.status = 'C' THEN 'Completed'
                        ELSE 'Unknown'
                    END AS Status
                FROM 
                    transaction_log tl
                INNER JOIN 
                    staff_info si 
                    ON tl.staff_id = si.staff_id;"

            ' Create a MySqlCommand object
            Dim command As New MySqlCommand(query, con)

            ' Execute the command and obtain a MySqlDataReader
            Dim reader As MySqlDataReader = command.ExecuteReader()

            ' Loop through the rows in the MySqlDataReader
            While reader.Read()
                ' Fetch the resident_id from the database (as VARCHAR)
                Dim residentId As String = reader("resident_id").ToString()

                ' Try to convert resident_id (VARCHAR) to Long (or handle as String)
                Dim residentIdAsLong As Long = 0
                If Long.TryParse(residentId, residentIdAsLong) Then
                    ' Fetch the resident name from the API response using the resident_id
                    Dim residentName As String = If(residentDictionary.ContainsKey(residentIdAsLong), residentDictionary(residentIdAsLong), "Unknown Resident")

                    ' Add a new row to the DataGridView
                    Guna2DataGridView1.Rows.Add(
                        residentName,
                        reader("StaffName"),
                        reader("Transaction_data"),
                        reader("payment"),
                        reader("type"),
                        reader("status")
                    )
                Else
                    ' Handle invalid resident_id format (if it can't be parsed to Long)
                    Guna2DataGridView1.Rows.Add(
                        "Invalid Resident ID",
                        reader("StaffName"),
                        reader("Transaction_data"),
                        reader("payment"),
                        reader("type"),
                        reader("status")
                    )
                End If
            End While

            ' Close the MySqlDataReader
            reader.Close()

        Catch ex As Exception
            ' Show an error message if an exception occurs
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Ensure the connection is closed
            con.Close()
        End Try
    End Sub

    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

    End Sub
End Class
