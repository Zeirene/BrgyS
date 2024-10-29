Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports System.IO


Public Class Form7
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
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


        'Dim studentSection As String = TextBox2.Text
        'Dim studentYear As String = TextBox3.Text

        ' Generate a customized file name based on student's name and current date/time
        Dim sanitizedStudentName As String = NAMEOFAPPLICANT
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd") ' Add a timestamp to the file name

        Dim newFileName As String = sanitizedStudentName & "_" & dateTimeStamp & ".docx"
        Dim newFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newFileName)

        ' Copy the template file to a new file with the customized name
        File.Copy(templatePath, newFilePath, True) ' The True flag will overwrite if the file already exists

        ' Replace placeholders in the new document
        ReplaceTextInWordDocument(newFilePath, "{Name}", NAMEOFAPPLICANT)
        ReplaceTextInWordDocument(newFilePath, "{Address}", resaddress)
        ReplaceTextInWordDocument(newFilePath, "{BName}", BNAME)
        ReplaceTextInWordDocument(newFilePath, "{BAddress}", BADDRESS)
        ReplaceTextInWordDocument(newFilePath, "{NatureB}", NATUREB)

        ' Inform the user that the document has been saved
        'MessageBox.Show("Document created and saved successfully as " & newFilePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        MessageBox.Show("Document created and ready to print ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

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

    Private Sub Form7_Load(sender As Object, e As EventArgs) Handles MyBase.Load

        Guna2TextBox7.Text = Form3.Guna2TextBox6.Text + Form3.Guna2TextBox7.Text + Form3.Guna2TextBox8.Text
        Guna2TextBox4.Text = Form3.Guna2TextBox9.Text

    End Sub
End Class