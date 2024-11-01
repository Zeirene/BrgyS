Imports AForge.Video
Imports AForge.Video.DirectShow
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms
Imports System.Drawing.Imaging
Imports System.IO


Public Class brgyID

    Dim CAMERA As VideoCaptureDevice
    Dim bmp As Bitmap
    Private Sub brgyID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Camstart() 'load camera
        Guna2TextBox1.Text = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text
        Guna2TextBox3.Text = Form3.Guna2TextBox9.Text
        'load other info by query

    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        CAMERA.SignalToStop()
        PictureBox1.Image = Nothing
        Camstart() 'load camera

    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click

        If PictureBox1.Image IsNot Nothing Then
            Dim newBitmap As Bitmap = PictureBox1.Image
        End If
        CAMERA.SignalToStop()
        PictureBox1.Image = PictureBox1.Image

    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        'clear
        PictureBox1.Image = Nothing

    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        'save and print docu
        generatedocfile()

    End Sub




    'functions and sub
    Private Sub generatedocfile()
        ' Path to the template document
        Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\clearancetemplate.docx"

        ' Input values from the textboxes
        Dim NAMEOFAPPLICANT As String = Guna2TextBox1.Text
        Dim BRGYID As String = "???"
        Dim RESADDRESS As String = Guna2TextBox3.Text
        Dim PHOTO As String = ""
        Dim TIN As String = Guna2TextBox4.Text
        Dim BDAY As String = Guna2TextBox2.Text
        Dim BTYPE As String = Guna2TextBox5.Text
        Dim PRECINTNO As String = Guna2TextBox6.Text
        Dim EMERNAME As String = Guna2TextBox8.Text
        Dim EMERNO As String = Guna2TextBox7.Text
        Dim EMERADD As String = Guna2TextBox9.Text




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
        'ReplaceTextInWordDocument(newFilePath, "{Name}", NAMEOFAPPLICANT)
        'ReplaceTextInWordDocument(newFilePath, "{Address}", resaddress)
        'ReplaceTextInWordDocument(newFilePath, "{BName}", BNAME)
        'ReplaceTextInWordDocument(newFilePath, "{BAddress}", BADDRESS)
        'ReplaceTextInWordDocument(newFilePath, "{NatureB}", NATUREB)

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
    'camera//////////////////////////////////////////////////////////////////////////////////////
    Private Sub Camstart()
        Dim cameras As VideoCaptureDeviceForm = New VideoCaptureDeviceForm
        If cameras.ShowDialog = DialogResult.OK Then
            CAMERA = cameras.VideoDevice
            AddHandler CAMERA.NewFrame, New NewFrameEventHandler(AddressOf Captured)
            CAMERA.Start()
        ElseIf cameras.ShowDialog = DialogResult.Cancel Then
            Me.Close()
        End If
    End Sub
    Private Sub Captured(sender As Object, eventsArgs As NewFrameEventArgs)
        bmp = DirectCast(eventsArgs.Frame.Clone(), Bitmap)
        PictureBox1.Image = DirectCast(eventsArgs.Frame.Clone(), Bitmap)
    End Sub
    Private Sub Savepic()
        Dim filename, filepath As String
        filename = generatefilename()
        filepath = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\Generated Pics\" + filename + ".jpg"

        If PictureBox1.Image IsNot Nothing Then
            Dim newBitmap As Bitmap = PictureBox1.Image
            newBitmap.Save(filepath, ImageFormat.Png)
            'Form8.Label10.Text = filepath
        End If

        MsgBox("picture saved")
        CAMERA.SignalToStop()
    End Sub
    Private Function generatefilename() As String
        Return System.DateTime.Now.ToString("yyyyMMdd") + "_" + Guna2TextBox1.Text
    End Function


End Class