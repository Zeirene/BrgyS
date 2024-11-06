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
        LoadResidentInformation(Guna2TextBox1.Text)
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If Guna2ComboBox2.SelectedItem IsNot Nothing Then
            Select Case Guna2ComboBox2.SelectedItem.ToString()
                Case "ID"

                    Try
                        openCon()

                        Using command As New MySqlCommand("SELECT * FROM resident_info WHERE resident_id = @resident_id", con)
                            command.Parameters.Add("@resident_id", MySqlDbType.VarChar).Value = Guna2TextBox1.Text ' search

                            Dim adapter As New MySqlDataAdapter(command)
                            Dim table As New DataTable
                            adapter.Fill(table)


                            If table.Rows.Count = 0 Then
                                MsgBox("Invalid username or password. Please try again.", MsgBoxStyle.Exclamation, "Login Error")
                            Else
                                con.Close()
                                
                                brgyID.resID = table.Rows(0)("resident_id").ToString
                                Dim anotherForm As New brgyID()
                                Form2.switchPanel(anotherForm)

                            End If
                        End Using
                    Catch ex As Exception
                        MsgBox("An error occurred: " & ex.Message, MsgBoxStyle.Critical, "Error")
                    Finally
                        con.Close()
                    End Try



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
        Guna2TextBox6.Clear()
        Guna2TextBox7.Clear()
        Guna2TextBox8.Clear()
        Guna2TextBox9.Clear()
        Guna2TextBox16.Clear()
        Guna2HtmlLabel15.Text = ""

    End Sub

    'function
    Private Sub LoadResidentInformation(searchTerm As String)

        Dim query As String = "SELECT * FROM resident_info WHERE resident_id LIKE @searchTerm"

        If String.IsNullOrWhiteSpace(searchTerm) Then
            Guna2TextBox6.Clear()
            Guna2TextBox7.Clear()
            Guna2TextBox8.Clear()
            Guna2TextBox9.Clear()
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
                        Guna2TextBox6.Text = reader("last_name").ToString()
                        Guna2TextBox7.Text = reader("given_name").ToString()
                        Guna2TextBox8.Text = reader("middle_name").ToString()
                        Guna2TextBox9.Text = reader("address").ToString()
                        Guna2HtmlLabel15.Text = reader("resident_id").ToString()

                        'insert to table
                        While reader.Read()
                            ' Add a new row to the DataGridView
                            Guna2DataGridView1.Rows.Add(reader("resident_id"), reader("last_Name"), reader("given_Name"), reader("middle_Name"), reader("address"))
                        End While
                    Else
                        Guna2TextBox6.Clear()
                        Guna2TextBox7.Clear()
                        Guna2TextBox8.Clear()
                        Guna2TextBox9.Clear()
                        Guna2HtmlLabel15.Text = ""
                        'loadform()
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
                ' Add a new row to the DataGridView
                Guna2DataGridView1.Rows.Add(reader("resident_id"), reader("last_Name"), reader("given_Name"), reader("middle_Name"), reader("address"))
            End While

            ' Close the SqlDataReader
            reader.Close()
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            con.Close()
        End Try
    End Sub

End Class