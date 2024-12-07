Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
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
        Clearance_PREV.resName = Guna2TextBox1.Text
        Clearance_PREV.stat = Guna2ComboBox1.Text
        Clearance_PREV.addr = Guna2TextBox4.Text + " Brgy Sta Lucia, QUEZON CITY"
        Clearance_PREV.stay = Guna2TextBox2.Text
        Clearance_PREV.purp = Guna2ComboBox3.Text


        'Dim anotherForm As New Clearance_PREV()
        Clearance_PREV.Show()

        'generatedocfile()
        'InsertTransactionLog()
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
    Public Sub InsertTransactionLog()
        Dim logDate As Date = Date.Today
        Dim logTime As Date = DateTime.Now.ToString("HH: mm:ss")
        Dim logType As String = Form3.Guna2ComboBox2.Text
        Dim logStatus As String = "Completed"
        Dim payment As Decimal = Decimal.Parse(Form3.Guna2TextBox16.Text)
        Dim residentId As String = Label2.Text
        Dim staffId As String = Form2.staffID ' staffId is treated as a String

        Try
            ' Open the connection to the database
            con.Open()

            'Prepare the SQL query to insert the transaction log data
            Dim insertQuery As String = "INSERT INTO transaction_log (log_date, log_time, type, status, payment, residentId, staff_id) 
                                     VALUES (@log_date, @log_time, @type, @status, @payment, @residentId, @staff_id)"

            'Dim insertQuery As String = "INSERT INTO transaction_log (log_date, log_time, type, status, payment) 
            '                         VALUES (@log_date, @log_time, @type, @status, @payment)"

            ' Create a MySQL command object with the query and connection
            Using cmd As New MySqlCommand(insertQuery, con)
                ' Add parameters to the command
                cmd.Parameters.AddWithValue("@log_date", logDate)
                cmd.Parameters.AddWithValue("@log_time", logTime)
                cmd.Parameters.AddWithValue("@type", logType)
                cmd.Parameters.AddWithValue("@status", logStatus)
                cmd.Parameters.AddWithValue("@payment", payment)
                cmd.Parameters.AddWithValue("@resident_id", residentId)
                cmd.Parameters.AddWithValue("@staff_id", staffId)

                ' Execute the command to insert data into the transaction_log table
                cmd.ExecuteNonQuery()

                ' Optional: Show a success message after insertion
                MessageBox.Show("Transaction log inserted successfully!")
            End Using

        Catch ex As Exception
            ' Handle any errors that may have occurred
            MessageBox.Show("Error: " & ex.Message)
        Finally
            ' Ensure the connection is closed
            If con IsNot Nothing AndAlso con.State = ConnectionState.Open Then
                con.Close()
            End If
        End Try
    End Sub



    Public Sub onlyacceptnum(e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub
End Class