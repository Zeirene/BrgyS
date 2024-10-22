Imports System.IO
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms

Public Class Form3
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Label1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click

        'generate docu
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

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        'add new resident
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        'log out
    End Sub

    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        'clear
        Guna2TextBox1.Clear()
        'Guna2TextBox2.Clear()
        'Guna2TextBox3.Clear()
        'Guna2TextBox4.Clear()
        'Guna2TextBox5.Clear()
        Guna2TextBox6.Clear()
        Guna2TextBox7.Clear()
        Guna2TextBox8.Clear()
        Guna2TextBox9.Clear()
        'Guna2TextBox10.Clear()
        'Guna2TextBox11.Clear()
        'Guna2TextBox12.Clear()
        'Guna2TextBox13.Clear()
        Guna2TextBox16.Clear()
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