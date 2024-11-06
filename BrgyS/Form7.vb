Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.IO
Imports Microsoft.Office.Interop.Word



Public Class Form7
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        generatedocfile()
    End Sub

    Private Sub generatedocfile()
        ' Path to the template document
        Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\clearancetemplate.docx"

        ' Input values from the textboxes
        Dim NAMEOFAPPLICANT As String = Guna2TextBox7.Text
        Dim resaddress As String = Guna2TextBox4.Text
        Dim BNAME As String = Guna2TextBox2.Text
        Dim BADDRESS As String = Guna2TextBox5.Text
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

        ' Convert the .docx to a .pdf file
        Dim newPdfFileName As String = sanitizedStudentName & "_" & dateTimeStamp & ".pdf"
        Dim newPdfFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newPdfFileName)
        ConvertDocxToPdf(newDocxFilePath, newPdfFilePath)

        MessageBox.Show("Document created and saved as PDF: " & newPdfFilePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub

    Private Sub ReplaceTextInWordDocument(filePath As String, placeholder As String, replacementText As String)
        Using wordDoc As WordprocessingDocument = WordprocessingDocument.Open(filePath, True)
            Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
            Dim documentBody As Body = mainPart.Document.Body

            For Each textElement As Text In documentBody.Descendants(Of Text)()
                If textElement.Text.Contains(placeholder) Then
                    textElement.Text = textElement.Text.Replace(placeholder, replacementText)
                End If
            Next
            mainPart.Document.Save()
        End Using
    End Sub

    'Private Sub ConvertDocxToPdf(docxFilePath As String, pdfFilePath As String)
    '    Dim wordApp As New Microsoft.Office.Interop.Word.Application()
    '    Dim wordDoc As Microsoft.Office.Interop.Word.Document = Nothing
    '    Try
    '        wordDoc = wordApp.Documents.Open(docxFilePath)
    '        wordDoc.ExportAsFixedFormat(pdfFilePath, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)
    '    Catch ex As Exception
    '        MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
    '    Finally
    '        If wordDoc IsNot Nothing Then wordDoc.Close(False)
    '        wordApp.Quit(False)
    '    End Try
    'End Sub
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

    Private Sub PrintDocument(filePath As String)

    End Sub

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Guna2TextBox7.Text = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text
        Guna2TextBox4.Text = Form3.Guna2TextBox9.Text

    End Sub


End Class