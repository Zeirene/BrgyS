Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms
Imports MySql.Data.MySqlClient
Imports System.IO
Public Class clearanceQCID
    Private Sub clearanceQCID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Guna2TextBox1.Text = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text
        Guna2TextBox3.Text = Form3.Guna2TextBox9.Text
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        generatedocfile()

    End Sub

    Private Sub generatedocfile()
        ' Path to the template document
        Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\BRGYidTEMP.docx"

        ' Input values from the textboxes
        Dim NAMEOFRESIDENT As String = Guna2TextBox1.Text
        Dim BDAY As String = Guna2TextBox2.Text
        Dim CIVITSTAT As String = Guna2TextBox3.Text

        Dim RESADDRESS As String = Guna2TextBox4.Text
        Dim YEARSOFSTAY As String = Guna2TextBox5.Text
        Dim PURPOSE As String = Guna2TextBox6.Text

        'Dim studentSection As String = TextBox2.Text
        'Dim studentYear As String = TextBox3.Text

        ' Generate a customized file name based on student's name and current date/time
        Dim sanitizedStudentName As String = NAMEOFRESIDENT
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd") ' Add a timestamp to the file name

        Dim newFileName As String = sanitizedStudentName & "_" & dateTimeStamp & ".docx"
        Dim newFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newFileName)

        ' Copy the template file to a new file with the customized name
        File.Copy(templatePath, newFilePath, True) ' The True flag will overwrite if the file already exists

        ' Replace placeholders in the new document
        ReplaceTextInWordDocument(newFilePath, "{Name}", NAMEOFRESIDENT)
        ReplaceTextInWordDocument(newFilePath, "{BDay}", BDAY)
        ReplaceTextInWordDocument(newFilePath, "{BDay}", BDAY)

        ReplaceTextInWordDocument(newFilePath, "{Address}", RESADDRESS)
        ReplaceTextInWordDocument(newFilePath, "{BDay}", BDAY)
        ReplaceTextInWordDocument(newFilePath, "{BDay}", BDAY)



        ' Inform the user that the document has been saved
        'MessageBox.Show("Document created and saved successfully as " & newFilePath, "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
        MessageBox.Show("Document created and ready to print ", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub


    'function
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