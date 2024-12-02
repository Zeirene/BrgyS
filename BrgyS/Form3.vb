Imports System.IO
Imports System.Net.Http
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json

Public Class Form3
    Private TypeToIds As New Dictionary(Of String, List(Of String)) From {
        {"ID", New List(Of String) From {"BRGY ID"}},
        {"CLEARANCE", New List(Of String) From {"NBI", "TIN", "PHILHEALTH", "POLICE CLEARANCE", "POSTAL", "LTAP (license to operate And possess firearms)"}},
        {"PERMITS", New List(Of String) From {"BUILDING PERMIT", "BUSINESS PERMIT"}}
    }
    Private WithEvents debounceTimer As New Timer() ' Timer to debounce the TextChanged event
    Private debounceDelay As Integer = 300 ' Delay in milliseconds

    Private Async Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Load all resident data when the form loads
        Guna2ComboBox2.Items.AddRange(TypeToIds.Keys.ToArray())
        debounceTimer.Interval = debounceDelay

    End Sub




    Private Sub Guna2TextBox16_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox16.KeyPress
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Ignore the key if it is not a number or backspace
        End If
    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        'validations



        If Guna2ComboBox2.SelectedItem IsNot Nothing Then
            Select Case Guna2ComboBox2.SelectedItem.ToString()
                Case "ID"

                    'Try
                    '    openCon()

                    '    Using command As New MySqlCommand("SELECT * FROM resident_info WHERE resident_id = @resident_id", con)
                    '        command.Parameters.Add("@resident_id", MySqlDbType.VarChar).Value = Guna2TextBox1.Text ' search

                    '        Dim adapter As New MySqlDataAdapter(command)
                    '        Dim table As New DataTable
                    '        adapter.Fill(table)


                    '        If table.Rows.Count = 0 Then
                    '            MsgBox("Invalid username or password. Please try again.", MsgBoxStyle.Exclamation, "Login Error")
                    '        Else
                    '            con.Close()

                    '            brgyID.resID = table.Rows(0)("resident_id").ToString
                    '            Dim anotherForm As New brgyID()
                    '            Form2.switchPanel(anotherForm)

                    '        End If
                    '    End Using
                    'Catch ex As Exception
                    '    MsgBox("may An error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
                    'Finally
                    '    con.Close()
                    'End Try

                    'brgyID.resID = Table.Rows(0)("resident_id").ToString
                    Dim anotherForm As New brgyID()
                    Form2.switchPanel(anotherForm)



                Case "CLEARANCE"
                    ' Show the Clearance form
                    Dim anotherForm As New clearanceQCID()
                    Form2.switchPanel(anotherForm)

                Case "PERMITS"
                    ' Show the Permits form
                    Dim anotherForm As New Form7()
                    Form2.switchPanel(anotherForm)

                Case Else
                    MessageBox.Show("Please select a valid option.")
            End Select
        Else
            MessageBox.Show("Please select an option from the dropdown.")
        End If
    End Sub


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        ' Clear input fields
        Guna2TextBox1.Clear()
        Guna2TextBox6.Clear()
        Guna2TextBox7.Clear()
        Guna2TextBox8.Clear()
        Guna2TextBox9.Clear()
        Guna2HtmlLabel15.Text = ""


    End Sub


    'function
    Private Async Sub LoadResidentInformation(searchTerm As String)
        Guna2DataGridView1.Rows.Clear()

        ' Check if the search term is empty or whitespace
        If String.IsNullOrWhiteSpace(searchTerm) Then
            Guna2DataGridView1.Rows.Clear()

            ' Clear fields if no record is found
            ClearFields()
            Await loadform()
            Return
        End If

        Try
            ' Create an instance of ApiClient
            Dim apiClient As New ApiClient()

            ' Fetch resident records from the API
            Dim residentRecords As List(Of ApiResponse.ResidentRecord) = Await apiClient.GetResidentRecordsAsync()

            ' Filter the records based on the search term (case-insensitive search)
            Dim filteredRecords = residentRecords.Where(Function(record) record.ResidentId.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList()

            ' Clear the DataGridView
            Guna2DataGridView1.Rows.Clear()

            If filteredRecords.Any() Then
                ' Display the filtered record(s)
                For Each record In filteredRecords
                    Dim fullname As String = $"{record.ResidentLastName}, {record.ResidentFirstName} {record.ResidentMiddleName}"
                    Dim fulladdress As String = $"{record.Address} {record.Sitio} {record.Street}"
                    Guna2DataGridView1.Rows.Add(record.ResidentId, fullname, fulladdress)
                Next

                ' Populate the first record's details in the TextBoxes and Labels
                Dim firstRecord = filteredRecords.First()
                Guna2TextBox6.Text = firstRecord.ResidentLastName
                Guna2TextBox7.Text = firstRecord.ResidentFirstName
                Guna2TextBox8.Text = firstRecord.ResidentMiddleName
                Guna2TextBox9.Text = firstRecord.Address
                Guna2ComboBox3.Text = firstRecord.Sitio
                Guna2ComboBox4.Text = firstRecord.Street
                Guna2HtmlLabel15.Text = firstRecord.ResidentId.ToString()
            Else

                ' Clear the DataGridView
                Guna2DataGridView1.Rows.Clear()

                ' Clear fields if no record is found
                ClearFields()

            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while fetching data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub LoadResidentInformation2(searchTerm As String)
        Guna2DataGridView1.Rows.Clear()

        ' Check if the search term is empty or whitespace
        If String.IsNullOrWhiteSpace(searchTerm) Then
            Guna2DataGridView1.Rows.Clear()

            ' Clear fields if no record is found
            ClearFields()
            Await loadform()
            Return
        End If

        Try
            ' Create an instance of ApiClient
            Dim apiClient As New ApiClient()

            ' Fetch resident records from the API
            Dim residentRecords As List(Of ApiResponse.ResidentRecord) = Await apiClient.GetResidentRecordsAsync()

            ' Filter the records based on the search term (case-insensitive search)
            Dim filteredRecords = residentRecords.Where(Function(record) record.ResidentId.ToString().Contains(searchTerm, StringComparison.OrdinalIgnoreCase)).ToList()

            ' Clear the DataGridView
            Guna2DataGridView1.Rows.Clear()

            If filteredRecords.Any() Then
                ' Display the filtered record(s)
                For Each record In filteredRecords
                    Dim fullname As String = $"{record.ResidentLastName}, {record.ResidentFirstName} {record.ResidentMiddleName}"
                    Dim fulladdress As String = $"{record.Address} {record.Sitio} {record.Street}"
                    Guna2DataGridView1.Rows.Add(record.ResidentId, fullname, fulladdress)
                Next

                ' Populate the first record's details in the TextBoxes and Labels
                Dim firstRecord = filteredRecords.First()
                Guna2TextBox6.Text = firstRecord.ResidentLastName
                Guna2TextBox7.Text = firstRecord.ResidentFirstName
                Guna2TextBox8.Text = firstRecord.ResidentMiddleName
                Guna2TextBox9.Text = firstRecord.Address
                Guna2ComboBox3.Text = firstRecord.Sitio
                Guna2ComboBox4.Text = firstRecord.Street
                Guna2HtmlLabel15.Text = firstRecord.ResidentId.ToString()
            Else

                ' Clear the DataGridView
                Guna2DataGridView1.Rows.Clear()

                ' Clear fields if no record is found
                ClearFields()

            End If
        Catch ex As Exception
            MessageBox.Show("An error occurred while fetching data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub ClearFields()
        ' Clear all TextBoxes and Labels
        Guna2TextBox1.Text = ""
        Guna2ComboBox2.Text = ""
        Guna2TextBox6.Text = ""
        Guna2TextBox7.Text = ""
        Guna2TextBox8.Text = ""
        Guna2TextBox9.Text = ""
        Guna2HtmlLabel15.Text = ""

    End Sub

    Private Sub Guna2TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox1.KeyPress

        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True ' Ignore the key if it is not a number or backspace

        End If
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        ' Stop the previous timer if there was one
        debounceTimer.Stop()

        ' Start a new timer when the text is changed
        debounceTimer.Start()
    End Sub


    Private Sub debounceTimer_Tick(sender As Object, e As EventArgs) Handles debounceTimer.Tick
        ' Stop the timer to prevent it from firing again
        debounceTimer.Stop()

        ' Now load the resident information
        LoadResidentInformation(Guna2TextBox1.Text)

    End Sub



    'Private Function GetStringValue(reader As MySqlDataReader, v As String) As String
    '    Throw New NotImplementedException()
    'End Function

    Private Async Function loadform() As Task
        Try
            ' Clear the DataGridView
            Guna2DataGridView1.Rows.Clear()

            ' Create an instance of ApiClient
            Dim apiClient As New ApiClient()

            ' Fetch resident records using the ApiClient
            Dim residentRecords As List(Of ApiResponse.ResidentRecord) = Await apiClient.GetResidentRecordsAsync()

            ' Loop through the resident records and populate the DataGridView
            For Each record In residentRecords
                Dim fullname As String = $"{record.ResidentLastName}, {record.ResidentFirstName} {record.ResidentMiddleName}"
                Dim fulladdress As String = $"{record.Address} {record.Sitio} {record.Street}"
                Guna2DataGridView1.Rows.Add(record.ResidentId, fullname, fulladdress)
            Next
        Catch ex As Exception
            MessageBox.Show("Error loading data: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function


    Private Sub Guna2DataGridView1_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles Guna2DataGridView1.CellContentClick

        Guna2TextBox1.Text = Guna2DataGridView1.Rows(e.RowIndex).Cells(0).Value
    End Sub


    'Case "Sitio 2"
    '            Guna2ComboBox3.Items.Add("Juan Luna")
    '            Guna2ComboBox3.Items.Add("J.P. Rizal")
    '            Guna2ComboBox3.Items.Add("M. H Del Pilar")
    '            Guna2ComboBox3.Items.Add("Jacinto")
    '            Guna2ComboBox3.Items.Add("Diego Silang")
    '            Guna2ComboBox3.Items.Add("Malvar")
    '            Guna2ComboBox3.Items.Add("Panganiban")
    '            Guna2ComboBox3.Items.Add("A. Dela Cruz")
    '            Guna2ComboBox3.Items.Add("F. Balagtas")

    '        Case "Sitio 3"
    '            Guna2ComboBox3.Items.Add("J.P. Rizal")
    '            Guna2ComboBox3.Items.Add("Visayas Avenue")
    '            Guna2ComboBox3.Items.Add("Francisco Park")
    '            Guna2ComboBox3.Items.Add("Bukaneg")
    '            Guna2ComboBox3.Items.Add("Balagtas")
    '            Guna2ComboBox3.Items.Add("Aguinaldo")
    '            Guna2ComboBox3.Items.Add("Panday Pira")
    '            Guna2ComboBox3.Items.Add("Lakandula")
    '            Guna2ComboBox3.Items.Add("M. Aquino")
    '            Guna2ComboBox3.Items.Add("Lopez Jaena")

    '        Case "Sitio 4"
    '            Guna2ComboBox3.Items.Add("Jose Abad Santos")
    '            Guna2ComboBox3.Items.Add("Mabini")
    '            Guna2ComboBox3.Items.Add("Humabon")
    '            Guna2ComboBox3.Items.Add("Gomez")
    '            Guna2ComboBox3.Items.Add("Burgos")
    '            Guna2ComboBox3.Items.Add("Zamora")
    '            Guna2ComboBox3.Items.Add("Bonifacio")
    '            Guna2ComboBox3.Items.Add("J. Basa")
    '            Guna2ComboBox3.Items.Add("Naning Ponce")

    '        Case "Sitio 5"
    '            Guna2ComboBox3.Items.Add("J.P. Rizal")
    '            Guna2ComboBox3.Items.Add("Jose Abad Santos")
    '            Guna2ComboBox3.Items.Add("Mabini")
    '            Guna2ComboBox3.Items.Add("Bonifacio")
    '            Guna2ComboBox3.Items.Add("T. Alonzo")
    '            Guna2ComboBox3.Items.Add("Paterno")

    '        Case "Sitio 6"
    '            Guna2ComboBox3.Items.Add("Jose Abad Santos")
    '            Guna2ComboBox3.Items.Add("T. Alonzo")
    '            Guna2ComboBox3.Items.Add("Paterno")
    '            Guna2ComboBox3.Items.Add("Veronica")
    '            Guna2ComboBox3.Items.Add("Agoncillo")
    '            Guna2ComboBox3.Items.Add("Natividad")
    '            Guna2ComboBox3.Items.Add("Rajah Soliman")

    '        Case "Sitio 7"
    '            Guna2ComboBox3.Items.Add("F. Calderon")
    '            Guna2ComboBox3.Items.Add("J. Palma")
    '            Guna2ComboBox3.Items.Add("Lapu-Lapu")
    '            Guna2ComboBox3.Items.Add("Gumamela")
    '            Guna2ComboBox3.Items.Add("Dahlia")
    '            Guna2ComboBox3.Items.Add("Rosas")
    '            Guna2ComboBox3.Items.Add("Camia")
    '            Guna2ComboBox3.Items.Add("Rosal")
    '            Guna2ComboBox3.Items.Add("Sampaguita")
    '            Guna2ComboBox3.Items.Add("Tarhaville Ave.")

    '        Case Else
    '            ' Optional: If an unrecognized item is selected, you could show a message or keep Guna2ComboBox3 empty
    '            Guna2ComboBox3.Items.Add("No streets available")
    '    End Select
    'End Sub

    Private Sub Guna2ComboBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox3.SelectedIndexChanged
        Guna2ComboBox4.Items.Clear()

        ' Check which sitio is selected in Guna2ComboBox3 and add corresponding streets to Guna2ComboBox3
        Select Case Guna2ComboBox3.SelectedItem.ToString
            Case "Sitio 1"
                Guna2ComboBox4.Items.Add("J.P Rizal")
                Guna2ComboBox4.Items.Add("Paguio")
                Guna2ComboBox4.Items.Add("Endraca Compound")
                Guna2ComboBox4.Items.Add("Cursilista")
                Guna2ComboBox4.Items.Add("Pamana")
                Guna2ComboBox4.Items.Add("Castro Compound")
                Guna2ComboBox4.Items.Add("Sta. Marcela")
                Guna2ComboBox4.Items.Add("Galvez Compound")
                Guna2ComboBox4.Items.Add("Rivera Compound")
                Guna2ComboBox4.Items.Add("Amity Ville")
                Guna2ComboBox4.Items.Add("Plain Ville")

            Case "Sitio 2"
                Guna2ComboBox4.Items.Add("Juan Luna")
                Guna2ComboBox4.Items.Add("J.P. Rizal")
                Guna2ComboBox4.Items.Add("M. H Del Pilar")
                Guna2ComboBox4.Items.Add("Jacinto")
                Guna2ComboBox4.Items.Add("Diego Silang")
                Guna2ComboBox4.Items.Add("Malvar")
                Guna2ComboBox4.Items.Add("Panganiban")
                Guna2ComboBox4.Items.Add("A. Dela Cruz")
                Guna2ComboBox4.Items.Add("F. Balagtas")

            Case "Sitio 3"
                Guna2ComboBox4.Items.Add("J.P. Rizal")
                Guna2ComboBox4.Items.Add("Visayas Avenue")
                Guna2ComboBox4.Items.Add("Francisco Park")
                Guna2ComboBox4.Items.Add("Bukaneg")
                Guna2ComboBox4.Items.Add("Balagtas")
                Guna2ComboBox4.Items.Add("Aguinaldo")
                Guna2ComboBox4.Items.Add("Panday Pira")
                Guna2ComboBox4.Items.Add("Lakandula")
                Guna2ComboBox4.Items.Add("M. Aquino")
                Guna2ComboBox4.Items.Add("Lopez Jaena")

            Case "Sitio 4"
                Guna2ComboBox4.Items.Add("Jose Abad Santos")
                Guna2ComboBox4.Items.Add("Mabini")
                Guna2ComboBox4.Items.Add("Humabon")
                Guna2ComboBox4.Items.Add("Gomez")
                Guna2ComboBox4.Items.Add("Burgos")
                Guna2ComboBox4.Items.Add("Zamora")
                Guna2ComboBox4.Items.Add("Bonifacio")
                Guna2ComboBox4.Items.Add("J. Basa")
                Guna2ComboBox4.Items.Add("Naning Ponce")

            Case "Sitio 5"
                Guna2ComboBox4.Items.Add("J.P. Rizal")
                Guna2ComboBox4.Items.Add("Jose Abad Santos")
                Guna2ComboBox4.Items.Add("Mabini")
                Guna2ComboBox4.Items.Add("Bonifacio")
                Guna2ComboBox4.Items.Add("T. Alonzo")
                Guna2ComboBox4.Items.Add("Paterno")

            Case "Sitio 6"
                Guna2ComboBox4.Items.Add("Jose Abad Santos")
                Guna2ComboBox4.Items.Add("T. Alonzo")
                Guna2ComboBox4.Items.Add("Paterno")
                Guna2ComboBox4.Items.Add("Veronica")
                Guna2ComboBox4.Items.Add("Agoncillo")
                Guna2ComboBox4.Items.Add("Natividad")
                Guna2ComboBox4.Items.Add("Rajah Soliman")

            Case "Sitio 7"
                Guna2ComboBox4.Items.Add("F. Calderon")
                Guna2ComboBox4.Items.Add("J. Palma")
                Guna2ComboBox4.Items.Add("Lapu-Lapu")
                Guna2ComboBox4.Items.Add("Gumamela")
                Guna2ComboBox4.Items.Add("Dahlia")
                Guna2ComboBox4.Items.Add("Rosas")
                Guna2ComboBox4.Items.Add("Camia")
                Guna2ComboBox4.Items.Add("Rosal")
                Guna2ComboBox4.Items.Add("Sampaguita")
                Guna2ComboBox4.Items.Add("Tarhaville Ave.")

            Case Else
                ' Optional: If an unrecognized item is selected, you could show a message or keep Guna2ComboBox3 empty
                Guna2ComboBox4.Items.Add("No streets available")
        End Select
    End Sub

    Private Sub Guna2ComboBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles Guna2ComboBox2.SelectedIndexChanged
        ' Clear Guna2ComboBox1 before adding new items
        Guna2ComboBox1.Items.Clear()

        ' Get the selected type from Guna2ComboBox2
        If Guna2ComboBox2.SelectedItem IsNot Nothing Then
            Dim selectedType As String = Guna2ComboBox2.SelectedItem.ToString()

            ' Populate Guna2ComboBox1 with IDs related to the selected type
            If TypeToIds.ContainsKey(selectedType) Then
                Guna2ComboBox1.Items.AddRange(TypeToIds(selectedType).ToArray())
            End If
        End If
    End Sub
End Class