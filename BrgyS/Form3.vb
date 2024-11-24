Imports System.IO
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        loadform()
    End Sub

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        ' Convert the text to uppercase
        Dim cursorPosition As Integer = Guna2TextBox1.SelectionStart
        Guna2TextBox1.Text = Guna2TextBox1.Text.ToUpper()
        Guna2TextBox1.SelectionStart = cursorPosition


        LoadResidentInformation(Guna2TextBox1.Text)
    End Sub

    Private Sub Guna2TextBox1_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox1.KeyPress
        LoadResidentInformation(Guna2TextBox1.Text)
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
        'clear
        loadform()
        Guna2TextBox1.Text = ""
        Guna2TextBox2.Text = ""
        Guna2TextBox3.Text = ""
        Guna2TextBox6.Text = ""
        Guna2TextBox7.Text = ""
        Guna2TextBox8.Text = ""
        Guna2TextBox9.Text = ""
        Guna2HtmlLabel15.Text = ""


    End Sub

    'function
    Private Sub LoadResidentInformation(searchTerm As String)

        Dim query As String = "SELECT * FROM resident_info WHERE resident_id LIKE @searchTerm"

        If String.IsNullOrWhiteSpace(searchTerm) Then
            Guna2TextBox1.Text = ""
            Guna2TextBox2.Text = ""
            Guna2TextBox3.Text = ""
            Guna2TextBox6.Text = ""
            Guna2TextBox7.Text = ""
            Guna2TextBox8.Text = ""
            Guna2TextBox9.Text = ""
            Guna2HtmlLabel15.Text = ""
            loadform()
            Return
        End If

        Try
            openCon()

            Using command As New MySqlCommand(query, con)

                command.Parameters.AddWithValue("@searchTerm", "%" & searchTerm & "%")

                Using reader As MySqlDataReader = command.ExecuteReader()
                    Guna2DataGridView1.Rows.Clear()

                    '
                    If reader.Read() Then
                        ' Set the values in TextBoxes and Labels
                        Guna2TextBox6.Text = reader("last_name").ToString()
                        Guna2TextBox7.Text = reader("given_name").ToString()
                        Guna2TextBox8.Text = reader("middle_name").ToString()
                        Guna2TextBox9.Text = reader("address").ToString()
                        Guna2TextBox2.Text = reader("sitio").ToString()
                        Guna2TextBox3.Text = reader("street").ToString()
                        Guna2HtmlLabel15.Text = reader("resident_id").ToString()

                        ' Generate full name and full address
                        Dim lname As String = reader("last_name").ToString()
                        Dim gname As String = reader("given_name").ToString()
                        Dim mname As String = reader("middle_name").ToString()
                        Dim fullname As String = lname & ", " & gname & " " & mname

                        Dim address As String = reader("address").ToString()
                        Dim sitio As String = reader("sitio").ToString()
                        Dim street As String = reader("street").ToString()
                        Dim fulladdress As String = address & " " & sitio & " " & street

                        ' Insert the row into the DataGridView
                        Guna2DataGridView1.Rows.Add(reader("resident_id"), fullname, fulladdress)

                    Else
                        ' Clear fields if no record is found
                        Guna2TextBox1.Text = ""
                        Guna2TextBox2.Text = ""
                        Guna2TextBox3.Text = ""
                        Guna2TextBox6.Text = ""
                        Guna2TextBox7.Text = ""
                        Guna2TextBox8.Text = ""
                        Guna2TextBox9.Text = ""
                        Guna2HtmlLabel15.Text = ""
                        loadform()

                    End If

                End Using
            End Using

        Catch ex As Exception
            ' Handle any errors that may have occurred
            'MessageBox.Show("An error occurred: " & ex.Message)
        Finally
            con.Close()
        End Try

    End Sub

    'Private Function GetStringValue(reader As MySqlDataReader, v As String) As String
    '    Throw New NotImplementedException()
    'End Function

    Private Sub loadform()
        Guna2DataGridView1.Rows.Clear()

        openCon()

        Try
            Dim query As String = "SELECT * FROM resident_info"

            ' Create a MySqlCommand
            Dim command As New MySqlCommand(query, con)

            ' Execute the command and obtain a reader
            Dim reader As MySqlDataReader = command.ExecuteReader()

            ' Loop through the rows in the SqlDataReader
            While reader.Read()
                Dim lname As String = reader("last_Name").ToString()
                Dim gname As String = reader("given_Name").ToString()
                Dim mname As String = reader("middle_Name").ToString()
                Dim fullname As String = lname & ", " & gname & " " & mname

                Dim address As String = reader("address").ToString()
                Dim sitio As String = reader("sitio").ToString()
                Dim street As String = reader("street").ToString()
                Dim fulladdress As String = address & " " & sitio & " " & street

                ' Add a new row to the DataGridView
                Guna2DataGridView1.Rows.Add(reader("resident_id"), fullname, fulladdress)
            End While

            ' Close the SqlDataReader
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error table: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try

    End Sub



    'Public Sub sitiodata()

    '    Guna2ComboBox4.Items.Clear()

    '    ' Check which sitio is selected in Guna2ComboBox3 and add corresponding streets to Guna2ComboBox4
    '    Select Case Guna2ComboBox3.SelectedItem.ToString
    '        Case "Sitio 1"
    '            Guna2ComboBox4.Items.Add("J.P Rizal")
    '            Guna2ComboBox4.Items.Add("Paguio")
    '            Guna2ComboBox4.Items.Add("Endraca Compound")
    '            Guna2ComboBox4.Items.Add("Cursilista")
    '            Guna2ComboBox4.Items.Add("Pamana")
    '            Guna2ComboBox4.Items.Add("Castro Compound")
    '            Guna2ComboBox4.Items.Add("Sta. Marcela")
    '            Guna2ComboBox4.Items.Add("Galvez Compound")
    '            Guna2ComboBox4.Items.Add("Rivera Compound")
    '            Guna2ComboBox4.Items.Add("Amity Ville")
    '            Guna2ComboBox4.Items.Add("Plain Ville")

    '        Case "Sitio 2"
    '            Guna2ComboBox4.Items.Add("Juan Luna")
    '            Guna2ComboBox4.Items.Add("J.P. Rizal")
    '            Guna2ComboBox4.Items.Add("M. H Del Pilar")
    '            Guna2ComboBox4.Items.Add("Jacinto")
    '            Guna2ComboBox4.Items.Add("Diego Silang")
    '            Guna2ComboBox4.Items.Add("Malvar")
    '            Guna2ComboBox4.Items.Add("Panganiban")
    '            Guna2ComboBox4.Items.Add("A. Dela Cruz")
    '            Guna2ComboBox4.Items.Add("F. Balagtas")

    '        Case "Sitio 3"
    '            Guna2ComboBox4.Items.Add("J.P. Rizal")
    '            Guna2ComboBox4.Items.Add("Visayas Avenue")
    '            Guna2ComboBox4.Items.Add("Francisco Park")
    '            Guna2ComboBox4.Items.Add("Bukaneg")
    '            Guna2ComboBox4.Items.Add("Balagtas")
    '            Guna2ComboBox4.Items.Add("Aguinaldo")
    '            Guna2ComboBox4.Items.Add("Panday Pira")
    '            Guna2ComboBox4.Items.Add("Lakandula")
    '            Guna2ComboBox4.Items.Add("M. Aquino")
    '            Guna2ComboBox4.Items.Add("Lopez Jaena")

    '        Case "Sitio 4"
    '            Guna2ComboBox4.Items.Add("Jose Abad Santos")
    '            Guna2ComboBox4.Items.Add("Mabini")
    '            Guna2ComboBox4.Items.Add("Humabon")
    '            Guna2ComboBox4.Items.Add("Gomez")
    '            Guna2ComboBox4.Items.Add("Burgos")
    '            Guna2ComboBox4.Items.Add("Zamora")
    '            Guna2ComboBox4.Items.Add("Bonifacio")
    '            Guna2ComboBox4.Items.Add("J. Basa")
    '            Guna2ComboBox4.Items.Add("Naning Ponce")

    '        Case "Sitio 5"
    '            Guna2ComboBox4.Items.Add("J.P. Rizal")
    '            Guna2ComboBox4.Items.Add("Jose Abad Santos")
    '            Guna2ComboBox4.Items.Add("Mabini")
    '            Guna2ComboBox4.Items.Add("Bonifacio")
    '            Guna2ComboBox4.Items.Add("T. Alonzo")
    '            Guna2ComboBox4.Items.Add("Paterno")

    '        Case "Sitio 6"
    '            Guna2ComboBox4.Items.Add("Jose Abad Santos")
    '            Guna2ComboBox4.Items.Add("T. Alonzo")
    '            Guna2ComboBox4.Items.Add("Paterno")
    '            Guna2ComboBox4.Items.Add("Veronica")
    '            Guna2ComboBox4.Items.Add("Agoncillo")
    '            Guna2ComboBox4.Items.Add("Natividad")
    '            Guna2ComboBox4.Items.Add("Rajah Soliman")

    '        Case "Sitio 7"
    '            Guna2ComboBox4.Items.Add("F. Calderon")
    '            Guna2ComboBox4.Items.Add("J. Palma")
    '            Guna2ComboBox4.Items.Add("Lapu-Lapu")
    '            Guna2ComboBox4.Items.Add("Gumamela")
    '            Guna2ComboBox4.Items.Add("Dahlia")
    '            Guna2ComboBox4.Items.Add("Rosas")
    '            Guna2ComboBox4.Items.Add("Camia")
    '            Guna2ComboBox4.Items.Add("Rosal")
    '            Guna2ComboBox4.Items.Add("Sampaguita")
    '            Guna2ComboBox4.Items.Add("Tarhaville Ave.")

    '        Case Else
    '            ' Optional: If an unrecognized item is selected, you could show a message or keep Guna2ComboBox4 empty
    '            Guna2ComboBox4.Items.Add("No streets available")
    '    End Select
    'End Sub
End Class