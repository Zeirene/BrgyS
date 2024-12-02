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




Public Class Form7
    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2TextBox7.Text = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text 'name
        Guna2TextBox4.Text = Form3.Guna2TextBox9.Text + " " + Form3.Guna2ComboBox2.Text + " " + Form3.Guna2ComboBox3.Text 'address

        Label2.Text = Form3.Guna2HtmlLabel15.Text
        Label3.Text = Form2.staffID
    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        generatedocfile()
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

    Public Sub InsertTransactionLog()
        Dim logDate As Date = Date.Today
        Dim logTime As Date = DateTime.Now.ToString("HH:mm:ss")
        Dim logType As String = Form3.Guna2ComboBox2.Text
        Dim logStatus As String = "Completed"
        Dim payment As Decimal = Decimal.Parse(Form3.Guna2TextBox16.Text)
        Dim residentId As String = Label2.Text
        Dim staffId As String = Form2.staffID ' staffId is treated as a String


        ' Input values from the textboxes
        Dim NAMEOFAPPLICANT As String = Guna2TextBox7.Text
        Dim resaddress As String = Guna2TextBox4.Text + " Brgy Sta Lucia, QUEZON CITY"
        Dim BNAME As String = Guna2TextBox2.Text
        Dim BADDRESS As String = Guna2TextBox5.Text + " Brgy Sta Lucia, QUEZON CITY"
        Dim NATUREB As String = Guna2TextBox9.Text

        'Try
        '    ' Open the connection to the database
        '    con.Open()

        '    'Prepare the SQL query to insert the transaction log data
        '    Dim insertQuery As String = "INSERT INTO transaction_log (log_date, log_time, type, status, payment, resident_id, staff_id) 
        '                             VALUES (@log_date, @log_time, @type, @status, @payment, @resident_id, @staff_id)"

        '    'Dim insertQuery As String = "INSERT INTO transaction_log (log_date, log_time, type, status, payment) 
        '    '                         VALUES (@log_date, @log_time, @type, @status, @payment)"

        '    ' Create a MySQL command object with the query and connection
        '    Using cmd As New MySqlCommand(insertQuery, con)
        '        ' Add parameters to the command
        '        cmd.Parameters.AddWithValue("@log_date", logDate)
        '        cmd.Parameters.AddWithValue("@log_time", logTime)
        '        cmd.Parameters.AddWithValue("@type", logType)
        '        cmd.Parameters.AddWithValue("@status", logStatus)
        '        cmd.Parameters.AddWithValue("@payment", payment)
        '        cmd.Parameters.AddWithValue("@resident_id", residentId)
        '        cmd.Parameters.AddWithValue("@staff_id", staffId)

        '        ' Execute the command to insert data into the transaction_log table
        '        cmd.ExecuteNonQuery()

        '        ' Optional: Show a success message after insertion
        '        MessageBox.Show("Transaction log inserted successfully!")
        '    End Using

        'Catch ex As Exception
        '    ' Handle any errors that may have occurred
        '    MessageBox.Show("Error: " & ex.Message)
        'Finally
        '    ' Ensure the connection is closed
        '    If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
        '        con.Close()
        '    End If
        'End Try

        Try
            ' Insert into transaction_log table
            Dim insertTransactionLog As String = "INSERT INTO transaction_log (log_date, log_time, type, status, payment, resident_id, staff_id) VALUES (@log_date, @log_time, @type, @status, @payment, @resident_id, @staff_id); SELECT LAST_INSERT_ID();"
            Dim logId As Integer
            Using cmd As New MySqlCommand(insertTransactionLog, con)
                cmd.Parameters.AddWithValue("@log_date", logDate)
                cmd.Parameters.AddWithValue("@log_time", logTime)
                cmd.Parameters.AddWithValue("@type", logType)
                cmd.Parameters.AddWithValue("@status", logStatus)
                cmd.Parameters.AddWithValue("@payment", payment)
                cmd.Parameters.AddWithValue("@resident_id", residentId)
                cmd.Parameters.AddWithValue("@staff_id", staffId)

                ' Execute the query and retrieve the last inserted log_id
                logId = Convert.ToInt32(cmd.ExecuteScalar())
            End Using

            ' Insert into permit_log table
            Dim insertPermitLog As String = "INSERT INTO permit_log (log_id, loc_type, b_name, b_address, stay_duration, m_rental, b_details) VALUES (@log_id, @loc_type, @b_name, @b_address, @stay_duration, @m_rental, @b_details);"

            Using cmd As New MySqlCommand(insertPermitLog, con)
                cmd.Parameters.AddWithValue("@log_id", logId)
                cmd.Parameters.AddWithValue("@loc_type", Guna2ComboBox2.Text) ' Replace with actual location type
                cmd.Parameters.AddWithValue("@b_name", BNAME) ' Replace with actual building name
                cmd.Parameters.AddWithValue("@b_address", BADDRESS) ' Replace with actual address
                cmd.Parameters.AddWithValue("@stay_duration", Guna2TextBox6.Text) ' Replace with actual duration in months
                cmd.Parameters.AddWithValue("@m_rental", Guna2TextBox8.Text) ' Replace with actual rental amount
                cmd.Parameters.AddWithValue("@b_details", NATUREB) ' Replace with actual details

                cmd.ExecuteNonQuery()
            End Using

            ' Commit transaction
            'Transaction.Commit()
            Console.WriteLine("Transaction completed successfully.")
        Catch ex As Exception
            ' Rollback transaction in case of error
            'Transaction.Rollback()
            Console.WriteLine("Error: " & ex.Message)
        End Try
    End Sub
    Private Sub generatedocfile()
        ' Path to the template document
        Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\clearancetemplate.docx"

        ' Input values from the textboxes
        Dim NAMEOFAPPLICANT As String = Guna2TextBox7.Text
        Dim resaddress As String = Guna2TextBox4.Text + " Brgy Sta Lucia, QUEZON CITY"
        Dim BNAME As String = Guna2TextBox2.Text
        Dim BADDRESS As String = Guna2TextBox5.Text + " Brgy Sta Lucia, QUEZON CITY"
        Dim NATUREB As String = Guna2TextBox9.Text

        ' Generate a new file path for the modified .docx file
        Dim sanitizedStudentName As String = NAMEOFAPPLICANT
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd")
        Dim newDocxFileName As String = sanitizedStudentName & "_" & dateTimeStamp & ".docx"
        Dim newDocxFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newDocxFileName)

        ' Copy the template file to a new .docx file
        File.Copy(templatePath, newDocxFilePath, True)

        ' Replace placeholders in the new .docx document
        ReplaceTextInWordDocument(newDocxFilePath, "{Name}", NAMEOFAPPLICANT)
        ReplaceTextInWordDocument(newDocxFilePath, "{Address}", resaddress)
        ReplaceTextInWordDocument(newDocxFilePath, "{BName}", BNAME)
        ReplaceTextInWordDocument(newDocxFilePath, "{BAddress}", BADDRESS)
        ReplaceTextInWordDocument(newDocxFilePath, "{NatureB}", NATUREB)

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

    Private Sub ReplaceTextInWordDocument(filePath As String, placeholder As String, replacementText As String)
        Using wordDoc As WordprocessingDocument = WordprocessingDocument.Open(filePath, True)
            Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
            Dim documentBody As DocumentFormat.OpenXml.Wordprocessing.Body = mainPart.Document.Body

            For Each textElement As Text In documentBody.Descendants(Of Text)()
                If textElement.Text.Contains(placeholder) Then
                    textElement.Text = textElement.Text.Replace(placeholder, replacementText)
                End If
            Next
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