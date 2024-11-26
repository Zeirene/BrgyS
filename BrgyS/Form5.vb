Imports MySql.Data.MySqlClient

Public Class Form5
    Private Sub Form5_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'load data's into table
        loadinfo()
    End Sub


    Public Sub loadinfo()
        Guna2DataGridView1.Rows.Clear()
        openCon()
        Try
            ' SQL query with proper joins and formatting
            Dim query As String = "
            SELECT 
                tl.resident_id, 
                CONCAT(ri.last_name, ', ', ri.given_name, ' ', ri.middle_name) AS ResidentName, 
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
                resident_info ri 
                ON tl.resident_id = ri.resident_id
            INNER JOIN 
                staff_info si 
                ON tl.staff_id = si.staff_id;"

            ' Create a MySqlCommand object
            Dim command As New MySqlCommand(query, con)

            ' Execute the command and obtain a MySqlDataReader
            Dim reader As MySqlDataReader = command.ExecuteReader()

            ' Clear the DataGridView before adding rows (optional, if needed)
            Guna2DataGridView1.Rows.Clear()

            ' Loop through the rows in the MySqlDataReader
            While reader.Read()
                ' Add a new row to the DataGridView
                Guna2DataGridView1.Rows.Add(
                reader("ResidentName"),
                reader("StaffName"),
                reader("Transaction_data"),
                reader("payment"),
                reader("type"),
                reader("status")
            )
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


End Class