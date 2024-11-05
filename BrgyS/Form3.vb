Imports System.IO
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient

Public Class Form3

    Private Sub Guna2TextBox1_TextChanged(sender As Object, e As EventArgs) Handles Guna2TextBox1.TextChanged
        LoadResidentInformation(Guna2TextBox1.Text)
    End Sub

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
                    '
                    If reader.Read() Then
                        Guna2TextBox6.Text = reader("last_name").ToString()
                        Guna2TextBox7.Text = reader("given_name").ToString()
                        Guna2TextBox8.Text = reader("middle_name").ToString()
                        Guna2TextBox9.Text = reader("address").ToString()
                        Guna2HtmlLabel15.Text = reader("resident_id").ToString()
                    Else
                        Guna2TextBox6.Clear()
                        Guna2TextBox7.Clear()
                        Guna2TextBox8.Clear()
                        Guna2TextBox9.Clear()
                        Guna2HtmlLabel15.Text = ""
                    End If
                End Using
            End Using

        Catch ex As Exception
            ' Handle any errors that may have occurred
            MessageBox.Show("An error occurred: " & ex.Message)
        Finally
            con.Close()
        End Try

    End Sub


    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        If Guna2ComboBox2.SelectedItem IsNot Nothing Then
            Select Case Guna2ComboBox2.SelectedItem.ToString()
                Case "ID"
                    'save resident id to call out in another form
                    'brgyID.resID = Table.Rows(0)("resident_id").ToString

                    ' Show the ID form
                    Me.Hide() ' Optional: hides the current form instead of closing
                    brgyID.Show()


                Case "CLEARANCE"
                    ' Show the Clearance form
                    Me.Hide() ' Optional: hides the current form instead of closing
                    clearanceQCID.Show()

                Case "PERMITS"
                    ' Show the Permits form
                    Me.Hide() ' Optional: hides the current form instead of closing
                    Form7.Show()

                Case Else
                    MessageBox.Show("Please select a valid option.")
            End Select
        Else
            MessageBox.Show("Please select an option from the dropdown.")
        End If
    End Sub


    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        'clear
        Guna2TextBox1.Text = ""
        Guna2TextBox6.Clear()
        Guna2TextBox7.Clear()
        Guna2TextBox8.Clear()
        Guna2TextBox9.Clear()
        Guna2TextBox16.Clear()
    End Sub

    'function

    Private Sub generatedocfile()
        ' Path to the template document
        Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\DOCUMENTS\TEMPLATES\"

        ' Input values from the textboxes
        Dim lastname As String = Guna2TextBox6.Text
        Dim givenname As String = Guna2TextBox7.Text
        Dim middlename As String = Guna2TextBox8.Text

        'Dim studentSection As String = TextBox2.Text
        'Dim studentYear As String = TextBox3.Text

        ' Generate a customized file name based on student's name and current date/time
        Dim sanitizedStudentName As String = lastname + "," + givenname + " " + middlename
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd") ' Add a timestamp to the file name

        Dim newFileName As String = sanitizedStudentName & "_" & dateTimeStamp & ".docx"
        Dim newFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\DOCUMENTS\GENERATED DOCU\", newFileName)

        ' Copy the template file to a new file with the customized name
        File.Copy(templatePath, newFilePath, True) ' The True flag will overwrite if the file already exists

        ' Replace placeholders in the new document
        'ReplaceTextInWordDocument(newFilePath, "{Name}", studentName)
        'ReplaceTextInWordDocument(newFilePath, "{Section}", studentSection)
        'ReplaceTextInWordDocument(newFilePath, "{Year}", studentYear)

        ' Inform the user that the document has been saved
        MessageBox.Show("Document created and saved successfully as " & newFilePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ReplaceTextInWordDocument(filePath As String, placeholder As String, replacementText As String)
        ' Open the existing Word document as read/write
        Using wordDoc As WordprocessingDocument = WordprocessingDocument.Open(filePath, True)
            ' Get the main document part
            Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
            Dim documentBody As Body = mainPart.Document.Body

            ' Loop through all text elements in the document
            For Each textElement As Text In documentBody.Descendants(Of Text)()
                ' Check if the text contains the placeholder
                If textElement.Text.Contains(placeholder) Then
                    ' Replace the placeholder with the actual value
                    textElement.Text = textElement.Text.Replace(placeholder, replacementText)
                End If
            Next

            ' Save changes to the document
            mainPart.Document.Save()
        End Using
    End Sub

End Class