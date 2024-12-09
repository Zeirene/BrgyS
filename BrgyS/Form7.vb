Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.IO
Imports Microsoft.Office.Interop.Word
Imports MySql.Data.MySqlClient
Imports System.Reflection.Emit
Imports Spire.Doc

Imports Document = Spire.Doc.Document
Imports System.Drawing.Printing
Imports DocumentFormat.OpenXml.Drawing.Charts
Imports System.Transactions
Imports BrgyS.ApiResponse
Imports Guna.UI2.WinForms




Public Class Form7
    Private _busname As String
    Public BNAME, BADDRESS, NATUREB As String




    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2TextBox7.Text = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text 'name
        Guna2TextBox4.Text = Form3.Guna2TextBox9.Text + " " + Form3.Guna2ComboBox2.Text

        Label2.Text = Form3.Guna2HtmlLabel15.Text
        Label3.Text = Form2.staffID


        Dim NAMEOFAPPLICANT As String = Guna2TextBox7.Text
        Dim resaddress As String = Guna2TextBox4.Text + " Brgy Sta Lucia, QUEZON CITY"
        BNAME = Guna2TextBox2.Text
        BADDRESS = Guna2TextBox5.Text + " Brgy Sta Lucia, QUEZON CITY"
        NATUREB = Guna2TextBox9.Text



    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        'Dim anotherForm As New Permits_PREV()
        'anotherForm.Show()

        Permits_PREV.busname = Guna2TextBox2.Text
        Permits_PREV.BADDRESS = Guna2TextBox5.Text + " Brgy Sta Lucia, QUEZON CITY"
        Permits_PREV.NATUREB = Guna2TextBox9.Text

        Permits_PREV.Show()

        'generatedocfile()
        InsertTransactionLog()
        'sendtoadmin2()

    End Sub

    'validations//////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Guna2TextBox3_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox3.KeyPress
        onlyacceptnum(e)
    End Sub



    'functions and sub/////////////////////////////////////////////////////////////

    'Public Sub InsertPermitLog()
    '    Try
    '        ' Open the connection to the database
    '        con.Open()

    '        'Prepare the SQL query to insert the transaction log data
    '        Dim insertQuery As String = "INSERT INTO transaction_log (log_date, log_time, type, status, payment, resident_id, staff_id) 
    '                                 VALUES (@log_date, @log_time, @type, @status, @payment, @resident_id, @staff_id)"

    '        'Dim insertQuery As String = "INSERT INTO transaction_log (log_date, log_time, type, status, payment) 
    '        '                         VALUES (@log_date, @log_time, @type, @status, @payment)"

    '        ' Create a MySQL command object with the query and connection
    '        Using cmd As New MySqlCommand(insertQuery, con)
    '            ' Add parameters to the command
    '            cmd.Parameters.AddWithValue("@log_date", logDate)
    '            cmd.Parameters.AddWithValue("@log_time", logTime)
    '            cmd.Parameters.AddWithValue("@type", logType)
    '            cmd.Parameters.AddWithValue("@status", logStatus)
    '            cmd.Parameters.AddWithValue("@payment", payment)
    '            cmd.Parameters.AddWithValue("@resident_id", residentId)
    '            cmd.Parameters.AddWithValue("@staff_id", staffId)

    '            ' Execute the command to insert data into the transaction_log table
    '            cmd.ExecuteNonQuery()

    '            ' Optional: Show a success message after insertion
    '            MessageBox.Show("Transaction log inserted successfully!")
    '        End Using

    '    Catch ex As Exception
    '        ' Handle any errors that may have occurred
    '        MessageBox.Show("Error: " & ex.Message)
    '    Finally
    '        ' Ensure the connection is closed
    '        If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
    '            con.Close()
    '        End If
    '    End Try
    'End Sub

    '  .ResidentFirstName = Form3.Guna2TextBox7.Text,
    '.ResidentType = "Resident",
    '.YearOfStay = Integer.Parse(Guna2TextBox6.Text),
    '.BirthPlace = "Sauyo",
    '.Sex = "Male",
    '.Address = Form3.Guna2TextBox9.Text,
    '.ResidentContactNumber = Guna2TextBox3.Text,
    '.BloodType = "Unidentified",
    '.ResidentEmail = Guna2TextBox10.Text,
    '.ResidentRegisteredDate = formattedDate,
    '.ResidentBirthdate = formattedDate,
    '.ResidentLastName = Form3.Guna2TextBox8.Text,
    '.CivilStatus = "Single",
    '.Sitio = Form3.Guna2ComboBox3.SelectedItem?.ToString(),
    '.ResidentMiddleName = Form3.Guna2TextBox6.Text,
    '.Street = Form3.Guna2ComboBox4.SelectedItem?.ToString()'
    Public Async Sub InsertTransactionLog()
        Dim InResidentId As Long?

        ' If Label2.Text is empty, create a new resident
        If String.IsNullOrWhiteSpace(Label2.Text) Then
            ' Insert new resident
            Dim residentAccountCreated As New Date(2025, 1, 10, 8, 48, 30) ' Example date with time
            Dim formattedDate As String = residentAccountCreated.ToString("yyyy-MM-ddTHH:mm:sszzz")
            '            .ResidentEmail = Guna2TextBox10.Text,
            Dim newResident As New ResidentRecord() With {
            .ResidentFirstName = Form3.Guna2TextBox7.Text,
            .ResidentType = "Resident",
            .YearOfStay = Integer.Parse(Guna2TextBox6.Text),
            .BirthPlace = "Sauyo",
            .Sex = "Male",
            .Address = Form3.Guna2TextBox9.Text,
            .ResidentContactNumber = Guna2TextBox3.Text,
            .BloodType = "Unidentified",
            .ResidentRegisteredDate = formattedDate,
            .ResidentBirthdate = formattedDate,
            .ResidentLastName = Form3.Guna2TextBox8.Text,
            .CivilStatus = "Single",
            .Sitio = Form3.Guna2ComboBox3.SelectedItem?.ToString(),
            .ResidentMiddleName = Form3.Guna2TextBox6.Text,
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
            If Long.TryParse(Label2.Text, parsedValue) Then
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

                ' Prepare the permit log data
                Dim permitLog As New PermitLog With {
                .ResidentId = transactionLog.ResidentId.ToString(),
                .BDetails = Guna2TextBox9.Text, ' Nature of Business
                .LocType = Guna2TextBox1.Text,
                .BAddress = Guna2TextBox5.Text & " Brgy Sta Lucia, QUEZON CITY",
                .BName = Guna2TextBox2.Text,
                .MRental = Decimal.Parse(Guna2TextBox8.Text),
                .LogId = logId.Value.ToString(),
                .StayDuration = Guna2TextBox6.Text
            }

                ' Insert the permit log
                Dim permitResult As Boolean = Await client.InsertPermitLogAsync(permitLog)

                If permitResult Then

                    MessageBox.Show("Permit log inserted successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Else
                    MessageBox.Show("Failed to insert permit log.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End If
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





    Private Sub generatedocfile()
        ' Path to the template document
        'Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\permits.docx"
        Dim templatePath As String = "C:\Users\Administrator\source\repos\pseudoclans\BrgyS\BrgyS\docu\permits.docx"

        ' Input values from the textboxes
        Dim NAMEOFAPPLICANT As String = Guna2TextBox7.Text
        Dim resaddress As String = Guna2TextBox4.Text + " Brgy Sta Lucia, Quezon City"
        Dim BNAME As String = Guna2TextBox2.Text
        Dim BADDRESS As String = Guna2TextBox5.Text + " Brgy Sta Lucia, Quezon City"
        Dim NATUREB As String = Guna2TextBox9.Text

        ' Generate a new file path for the modified .docx file
        Dim sanitizedStudentName As String = NAMEOFAPPLICANT
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyy MM dd, HH mm")
        Dim typeofpaper As String = Form3.Guna2ComboBox1.Text
        Dim newDocxFileName As String = dateTimeStamp & " " & sanitizedStudentName & "_" & typeofpaper & ".docx"

        'Dim newDocxFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newDocxFileName)
        Dim newDocxFilePath As String = Path.Combine("C:\Users\Administrator\source\repos\pseudoclans\BrgyS\BrgyS\docu\generated docu\", newDocxFileName)

        ' Copy the template file to a new .docx file
        File.Copy(templatePath, newDocxFilePath, True)

        ' Replace placeholders in the new .docx document
        ReplaceTextInWordDocument(newDocxFilePath, "{Name}", NAMEOFAPPLICANT)
        ReplaceTextInWordDocument(newDocxFilePath, "{Address}", resaddress)
        ReplaceTextInWordDocument(newDocxFilePath, "{BName}", BNAME)
        ReplaceTextInWordDocument(newDocxFilePath, "{BAddress}", BADDRESS)
        ReplaceTextInWordDocument(newDocxFilePath, "{NatureB}", NATUREB)

        MessageBox.Show("Document created And ready to print ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)


        '' Convert the .docx to a .pdf file
        'Dim newPdfFileName As String = sanitizedStudentName & "_" & dateTimeStamp & ".pdf"
        'Dim newPdfFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newPdfFileName)
        'ConvertDocxToPdf(newDocxFilePath, newPdfFilePath)

        'PrintDocxFile(newDocxFilePath, newDocxFileName)

        'MessageBox.Show("Document created and saved as PDF: " & newPdfFilePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Public Sub PrintDocxFile(docxFilePath As String, filename As String)
        'Create a Document object

        ' Create a Document object
        Try
            ' Load the Word document using Spire.Doc
            Dim doc As New Document()
            doc.LoadFromFile(docxFilePath)

            ' Generate a uniformed file name with timestamp
            'Dim folderPath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\printeddocs\" ' Specify your folder path
            'If Not Directory.Exists(folderPath) Then
            '    Directory.CreateDirectory(folderPath) ' Create folder if it doesn't exist
            'End If

            '' Create a uniformed name for the file
            'Dim newFilePath As String = Path.Combine(folderPath, filename)

            '' Save the document with the uniformed file name
            'doc.SaveToFile(newFilePath)

            '' Optional: Show message confirming the file has been saved
            'MessageBox.Show("Document saved as: " & filename, "Save Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

            ' Get the PrintDocument object from Spire.Doc document
            Dim printDoc As PrintDocument = doc.PrintDocument

            ' Optional: Specify the printer name
            ' printDoc.PrinterSettings.PrinterName = "Your Printer Name"

            ' Optional: Specify the range of pages to print
            ' printDoc.PrinterSettings.FromPage = 1
            ' printDoc.PrinterSettings.ToPage = 1

            ' Optional: Specify the number of copies to print
            ' printDoc.PrinterSettings.Copies = 1

            ' Print the document
            printDoc.Print()

            ' Show success message for printing
            MessageBox.Show("Document sent to printer successfully.", "Print Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

        Catch ex As Exception
            ' Handle any errors
            MessageBox.Show("Error printing or saving document: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    'Private Sub ReplaceTextInWordDocument(filePath As String, placeholder As String, replacementText As String)
    '    Using wordDoc As WordprocessingDocument = WordprocessingDocument.Open(filePath, True)
    '        Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
    '        Dim documentBody As DocumentFormat.OpenXml.Wordprocessing.Body = mainPart.Document.Body

    '        For Each textElement As Text In documentBody.Descendants(Of Text)()
    '            If textElement.Text.Contains(placeholder) Then
    '                textElement.Text = textElement.Text.Replace(placeholder, replacementText)
    '            End If
    '        Next
    '        mainPart.Document.Save()
    '    End Using
    'End Sub

    Private Sub ReplaceTextInWordDocument(filePath As String, placeholder As String, replacementText As String)
        ' Open the existing Word document as read/write
        Using wordDoc As WordprocessingDocument = WordprocessingDocument.Open(filePath, True)
            ' Get the main document part
            Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
            Dim documentBody As DocumentFormat.OpenXml.Wordprocessing.Body = mainPart.Document.Body

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
    Private Sub ConvertDocxToPdf(docxFilePath As String, pdfFilePath As String)
        Dim wordApp As New Application()
        Dim wordDoc As Microsoft.Office.Interop.Word.Document = Nothing
        Try
            ' Validate the DOCX file path
            If Not File.Exists(docxFilePath) Then
                Throw New FileNotFoundException("The DOCX file was not found.", docxFilePath)
            End If

            ' Open the Word document
            wordDoc = wordApp.Documents.Open(docxFilePath, ReadOnly:=True, Visible:=False)

            ' Ensure the PDF directory exists
            Dim pdfDir As String = Path.GetDirectoryName(pdfFilePath)
            If Not Directory.Exists(pdfDir) Then
                Directory.CreateDirectory(pdfDir)
            End If

            ' Export to PDF format
            wordDoc.ExportAsFixedFormat(pdfFilePath, WdExportFormat.wdExportFormatPDF)

            ' Confirm the file has been saved
            If Not File.Exists(pdfFilePath) Then
                Throw New Exception("The PDF file was not created successfully.")
            End If

        Catch ex As Exception
            MessageBox.Show("Error during PDF conversion: " & ex.Message, "Conversion Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close the document without saving any changes
            If wordDoc IsNot Nothing Then wordDoc.Close(SaveChanges:=False)
            ' Quit the Word application
            wordApp.Quit(SaveChanges:=False)
        End Try
    End Sub


    Public Sub onlyacceptnum(e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub

End Class