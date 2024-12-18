﻿Imports BrgyS.ApiResponse
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports Newtonsoft.Json
Imports System.IO
Imports System.Reflection.Emit
Public Class clearanceQCID




    Private Sub clearanceQCID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2TextBox1.Text = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text
        Guna2TextBox4.Text = Form3.Guna2TextBox9.Text + " " + Form3.Guna2ComboBox3.Text + " " + Form3.Guna2ComboBox4.Text 'address
        Guna2ComboBox3.Text = Form3.Guna2ComboBox1.Text



        Label2.Text = Form3.Guna2HtmlLabel15.Text
        Label3.Text = Form2.staffID
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        ' Validate if any required field is empty
        If String.IsNullOrWhiteSpace(Guna2TextBox1.Text) Then
            MessageBox.Show("Please enter the Resident Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2ComboBox1.Text) Then
            MessageBox.Show("Please select the Status.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2TextBox4.Text) Then
            MessageBox.Show("Please enter the Address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2TextBox2.Text) Then
            MessageBox.Show("Please enter the Length of Stay.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2ComboBox3.Text) Then
            MessageBox.Show("Please select the Purpose.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' If validation passes, assign values to Clearance_PREV
        Clearance_PREV.resName = Guna2TextBox1.Text
        Clearance_PREV.stat = Guna2ComboBox1.Text
        Clearance_PREV.addr = Guna2TextBox4.Text + " Brgy Sta Lucia, QUEZON CITY"
        Clearance_PREV.stay = Guna2TextBox2.Text
        Clearance_PREV.purp = Guna2ComboBox3.Text

        ' Show the Clearance_PREV form
        Clearance_PREV.Show()

        ' Optional: Call additional methods if needed
        ' generatedocfile()
        ' InsertTransactionLog()

    End Sub








    'functions and sub/////////////////////////////////////////////////////////////
    Private Sub generatedocfile()
        ' Path to the template document
        Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\clearancetemplate.docx"

        ' Input values from the textboxes
        Dim NAMEOFRESIDENT As String = Guna2TextBox1.Text
        Dim BDAY As String = Guna2DateTimePicker1.Value.ToString("MMM,d yyyy")
        Dim CIVITSTAT As String = Guna2ComboBox1.Text


        Dim RESADDRESS As String = Guna2TextBox4.Text + " Brgy Sta Lucia, QUEZON CITY"
        Dim YEARSOFSTAY As String = Guna2TextBox2.Text
        Dim PURPOSE As String = Guna2ComboBox3.Text
        Dim day As String = DateTime.Now.ToString("dd")
        Dim monthyear As String = DateTime.Now.ToString("MM,yyyy")

        'Dim studentSection As String = TextBox2.Text
        'Dim studentYear As String = TextBox3.Text

        ' Generate a customized file name based on student's name and current date/time
        Dim sanitizedStudentName As String = NAMEOFRESIDENT
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd hh mm") ' Add a timestamp to the file name
        Dim typeofpaper As String = Form3.Guna2ComboBox1.Text

        Dim newFileName As String = dateTimeStamp & " " & sanitizedStudentName & "_" & typeofpaper & ".docx"
        Dim newFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newFileName)

        ' Copy the template file to a new file with the customized name
        File.Copy(templatePath, newFilePath, True) ' The True flag will overwrite if the file already exists

        Try
            ' Replace placeholders in the new document
            ReplaceTextInWordDocument(newFilePath, "{name}", NAMEOFRESIDENT)
            ReplaceTextInWordDocument(newFilePath, "{BDAY}", BDAY)
            ReplaceTextInWordDocument(newFilePath, "{stat}", CIVITSTAT)

            ReplaceTextInWordDocument(newFilePath, "{add}", RESADDRESS)
            ReplaceTextInWordDocument(newFilePath, "{stay}", YEARSOFSTAY)
            ReplaceTextInWordDocument(newFilePath, "{purp}", PURPOSE)
            ReplaceTextInWordDocument2(newFilePath, "{day}", day)
            ReplaceTextInWordDocument2(newFilePath, "{monthyear}", monthyear)

            ' Inform the user that the document has been saved
            'MessageBox.Show("Document created And saved successfully as " & newFilePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            MessageBox.Show("Document created And ready to print ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            MessageBox.Show("An error occurred on insert: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Private Sub Guna2TextBox2_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox2.KeyPress
        onlyacceptnum(e)
    End Sub


    'function
    Private Sub ReplaceTextInWordDocument2(filePath As String, placeholder As String, replacementText As String)
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


    Private Sub ReplaceTextInWordDocument(filePath As String, placeholder As String, replacementText As String)
        ' Open the existing Word document as read/write
        Using wordDoc As WordprocessingDocument = WordprocessingDocument.Open(filePath, True)
            ' Get the main document part
            Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
            Dim documentBody As Body = mainPart.Document.Body

            ' Combine all text elements into a single string for easy replacement
            Dim fullText As String = String.Join("", documentBody.Descendants(Of Text)().Select(Function(t) t.Text))

            ' Replace the placeholder in the combined string
            If fullText.Contains(placeholder) Then
                fullText = fullText.Replace(placeholder, replacementText)

                ' Split the combined string back into text elements
                'Dim textElements = documentBody.Descendants(Of Text)().ToArray()
                'Dim index As Integer = 0
                'For Each textElement As Text In textElements
                '    If index < fullText.Length Then
                '        textElement.Text = fullText.Substring(index, textElement.Text.Length)
                '        index += textElement.Text.Length
                '    Else
                '        textElement.Text = ""
                '    End If
                'Next
                Dim textElements = documentBody.Descendants(Of Text)().ToArray()
                Dim index As Integer = 0
                For Each textElement As Text In textElements
                    If index < fullText.Length Then
                        ' Ensure the length doesn't exceed the remaining text
                        Dim lengthToCopy As Integer = Math.Min(textElement.Text.Length, fullText.Length - index)
                        textElement.Text = fullText.Substring(index, lengthToCopy)
                        index += lengthToCopy
                    Else
                        textElement.Text = ""
                    End If
                Next
            End If

            ' Save changes to the document
            mainPart.Document.Save()
        End Using

    End Sub
    Public Async Sub InsertTransactionLog()

        Dim InResidentId As Long?
        ' If Label2.Text is empty, create a new resident
        If String.IsNullOrWhiteSpace(Label2.Text) Then
            ' Insert new resident
            Dim residentAccountCreated As New Date(2025, 1, 10, 8, 48, 30) ' Example date with time
            Dim formattedDate As String = residentAccountCreated.ToString("yyyy-MM-ddTHH:mm:sszzz")

            Dim newResident As New ResidentRecord() With {
            .ResidentFirstName = Form3.Guna2TextBox7.Text,
            .ResidentType = "Resident",
            .BirthPlace = "Sauyo",
            .Sex = "Male",
            .Address = Form3.Guna2TextBox9.Text,
            .ResidentBirthdate = formattedDate,
            .ResidentLastName = Form3.Guna2TextBox8.Text,
            .CivilStatus = Guna2ComboBox1.SelectedItem?.ToString(),
            .Sitio = Form3.Guna2ComboBox3.SelectedItem?.ToString(),
            .ResidentMiddleName = Form3.Guna2TextBox6.Text,
            .ResidentEmail = Guna2TextBox3.Text,
            .Street = Form3.Guna2ComboBox4.SelectedItem?.ToString()
        }

            ' Call API to insert new resident
            Dim client As New ApiClient()
            InResidentId = Await client.InsertResidentAsync(newResident)

            If InResidentId.HasValue Then
                MessageBox.Show($"Resident created successfully with ID: {InResidentId.Value}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Else
                MessageBox.Show("Failed to create resident.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub ' Stop further processing if resident creation fails
            End If
        Else
            ' If Label2.Text has a value, use it as the ResidentId
            ' If Label2.Text has a value, use it as the ResidentId
            Dim parsedValue As Long
            If Long.TryParse(Form3.Guna2TextBox1.Text, parsedValue) Then
                ' Successfully parsed ResidentId
                InResidentId = parsedValue
            Else

                Exit Sub ' Stop further processing if ResidentId is invalid
            End If
        End If

        Try
            ' Create the transaction log object
            Dim transactionLog As New TransactionLog() With {
            .LogDate = Date.Today.ToString("yyyy-MM-dd"),
            .LogTime = DateTime.Now.ToString("HH:mm:ss"),
            .Type = Form3.Guna2ComboBox2.Text,
            .Status = "Pending",
            .Payment = Decimal.Parse(Form3.Guna2TextBox16.Text),
            .ResidentId = InResidentId, ' This will now have a valid ID
            .StaffId = Form2.staffID
        }

            ' Insert the transaction log and get the log_id
            Dim client As New ApiClient()
            Dim logId As Long? = Await client.InsertTransactionLogAsync(transactionLog)

            If logId.HasValue Then
                ' Notify the user of success
                MessageBox.Show($"Transaction log inserted successfully with Log ID: {logId.Value}", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

                '    ' Prepare the permit log data
                '    Dim permitLog As New PermitLog With {
                '    .ResidentId = transactionLog.ResidentId.ToString(),
                '    .BDetails = Guna2TextBox9.Text, ' Nature of Business
                '    .LocType = Guna2TextBox1.Text,
                '    .BAddress = Guna2TextBox5.Text & " Brgy Sta Lucia, QUEZON CITY",
                '    .BName = Guna2TextBox2.Text,
                '    .MRental = Decimal.Parse(Guna2TextBox8.Text),
                '    .LogId = logId.Value.ToString(),
                '    .StayDuration = Guna2TextBox6.Text
                '}

                '    ' Insert the permit log
                '    Dim permitResult As Boolean = Await client.InsertPermitLogAsync(permitLog)

                '    If permitResult Then

                '        MessageBox.Show("Permit log inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                '    Else
                '        MessageBox.Show("Failed to insert permit log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '    End If
            Else
                MessageBox.Show("Failed to retrieve Log ID for transaction log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As FormatException
            MessageBox.Show($"Invalid input format: {ex.Message}", "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            ' Handle general exceptions
            MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub







    Public Sub onlyacceptnum(e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class