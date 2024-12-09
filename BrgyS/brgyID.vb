Imports AForge.Video
Imports AForge.Video.DirectShow
Imports BrgyS.ApiResponse

Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports Guna.UI2.WinForms

Imports MySql.Data.MySqlClient

Imports System.Drawing.Imaging
Imports System.Drawing.Printing
Imports System.IO
'Imports Spire.Doc
Imports Documentss = Spire.Doc.Document

Public Class brgyID
    Private _resID As String
    'Private _staffID As String


    Public Property resID() As String
        Get
            Return _resID
        End Get
        Set(value As String)
            _resID = value
            Try
                openCon()
                Using cmd As New MySqlCommand("SELECT * FROM resident_info WHERE resident_id = @user", con)
                    cmd.Parameters.AddWithValue("@user", value)

                    Using reader As MySqlDataReader = cmd.ExecuteReader()
                        If reader.Read() Then
                            Label2.Text = reader("resident_id").ToString()
                        End If
                    End Using
                End Using
            Catch ex As Exception
                MsgBox(ex.Message)
            Finally
                con.Close()
            End Try
        End Set
    End Property


    Dim CAMERA As VideoCaptureDevice
    Dim bmp As Bitmap

    Private Sub brgyID_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Camstart() 'load camera
        Guna2TextBox1.Text = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text 'fullname
        Guna2TextBox3.Text = Form3.Guna2TextBox9.Text + " " + Form3.Guna2ComboBox2.Text + " " + Form3.Guna2ComboBox3.Text 'address

        Label2.Text = Form3.Guna2HtmlLabel15.Text
        Label3.Text = Form2.staffID
        'load other info by query

    End Sub
    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        CAMERA.SignalToStop()
        PictureBox1.Image = Nothing
        Camstart() 'load camera

    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        'capture pic
        'If PictureBox1.Image IsNot Nothing Then
        '    Dim newBitmap As Bitmap = PictureBox1.Image
        'End If
        'If ID_PREV.Guna2PictureBox1.Image IsNot Nothing Then
        '    Dim newBitmap2 As Bitmap = ID_PREV.Guna2PictureBox1.Image
        'End If
        'Savepic()
        'PictureBox1.Image = PictureBox1.Image
        'ID_PREV.img = PictureBox1.Image

        'ID_PREV.img = ID_PREV.Guna2PictureBox1.Image
        'CAMERA.SignalToStop()

        ' Ensure the image is captured and exists
        If PictureBox1.Image IsNot Nothing Then
            ' Transfer image to ID_PREV's Guna2PictureBox1
            ID_PREV.Guna2PictureBox1.Image = CType(PictureBox1.Image.Clone(), Image)
            Savepic()
            PictureBox1.Image = PictureBox1.Image
        Else
            MessageBox.Show("No image captured to transfer.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        'clear
        'PictureBox1.Image = Nothing
        ' Reset the text fields
        Guna2TextBox1.Text = String.Empty
        Guna2TextBox3.Text = String.Empty
        Guna2TextBox4.Text = String.Empty
        Guna2TextBox6.Text = String.Empty
        Guna2TextBox8.Text = String.Empty
        Guna2TextBox7.Text = String.Empty
        Guna2TextBox9.Text = String.Empty

        ' Clear the label and combo box selections
        Label2.Text = String.Empty
        Guna2ComboBox1.SelectedIndex = -1

        ' Clear the picture box image
        PictureBox1.Image = Nothing

        ' Reset the date picker
        Guna2DateTimePicker1.Value = DateTime.Now
    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        'save and print docu
        ' Validate if any required field is empty
        If String.IsNullOrWhiteSpace(Guna2TextBox1.Text) Then
            MessageBox.Show("Please enter the Resident Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2TextBox3.Text) Then
            MessageBox.Show("Please enter the Address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2TextBox4.Text) Then
            MessageBox.Show("Please enter the TIN.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2TextBox6.Text) Then
            MessageBox.Show("Please enter the Precinct.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2TextBox8.Text) Then
            MessageBox.Show("Please enter the Emergency Contact Name.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2TextBox7.Text) Then
            MessageBox.Show("Please enter the Emergency Contact Number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If String.IsNullOrWhiteSpace(Guna2TextBox9.Text) Then
            MessageBox.Show("Please enter the Emergency Contact Address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        If PictureBox1.Image Is Nothing Then
            MessageBox.Show("Please take a picture.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If

        ' After validation passes, assign values to ID_PREV
        ID_PREV.resName = Guna2TextBox1.Text
        ID_PREV.idnum = Label2.Text
        ID_PREV.img = PictureBox1.Image
        ID_PREV.addr = Guna2TextBox3.Text + " Brgy Sta Lucia, QUEZON CITY"
        ID_PREV.tin = Guna2TextBox4.Text
        ID_PREV.bday = Guna2DateTimePicker1.Value.ToString("MMMM, dd yyyy")
        ID_PREV.btype = Guna2ComboBox1.Text
        ID_PREV.precint = Guna2TextBox6.Text
        ID_PREV.emername = Guna2TextBox8.Text
        ID_PREV.emercont = Guna2TextBox7.Text
        ID_PREV.emeraddr = Guna2TextBox9.Text

        ' Show the ID_PREV form
        ID_PREV.Show()

        'generatedocfile()
        'InsertTransactionLog()
    End Sub


    'validations//////////////////////////////////////////////////////////////////////////////////////////////
    Private Sub Guna2TextBox6_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox6.KeyPress
        'validations
        onlyacceptnum(e)
    End Sub
    Private Sub Guna2TextBox7_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox7.KeyPress
        onlyacceptnum(e)
    End Sub
    Private Sub Guna2TextBox4_KeyPress(sender As Object, e As KeyPressEventArgs) Handles Guna2TextBox4.KeyPress
        onlyacceptnum(e)
    End Sub







    'functions and sub/////////////////////////////////////////////////////////////
    Private Sub generatedocfile()
        ' Path to the template document
        Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\BRGYidTEMP.docx"

        ' Check if the template file exists
        If Not File.Exists(templatePath) Then
            MessageBox.Show("Template file not found at: " & templatePath, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Return
        End If

        ' Input values from the textboxes
        Dim NAMEOFAPPLICANT As String = Guna2TextBox1.Text
        Dim BRGYID As String = Label2.Text
        Dim RESADDRESS As String = Guna2TextBox3.Text + " Brgy Sta Lucia, QUEZON CITY"
        Dim PHOTO As String = Label5.Text
        Dim TIN As String = Guna2TextBox4.Text
        Dim BDAY As String = Guna2DateTimePicker1.Value.ToString("MM:dd:yy")
        Dim BTYPE As String = Guna2ComboBox1.Text
        Dim PRECINTNO As String = Guna2TextBox6.Text
        Dim EMERNAME As String = Guna2TextBox8.Text
        Dim EMERNO As String = Guna2TextBox7.Text
        Dim EMERADD As String = Guna2TextBox9.Text

        ' Generate a new .docx file path
        Dim sanitizedStudentName As String = NAMEOFAPPLICANT
        Dim typeofpaper As String = Form3.Guna2ComboBox1.Text
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd")
        Dim newDocxFileName As String = typeofpaper & "_" & sanitizedStudentName & "_" & dateTimeStamp & ".docx"
        Dim newDocxFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newDocxFileName)

        Try
            ' Copy the template file and replace placeholders
            File.Copy(templatePath, newDocxFilePath, True)
            ReplaceTextInWordDocument(newDocxFilePath, "{BDay}", BDAY)


            ReplaceTextInWordDocument(newDocxFilePath, "{IDNumber}", BRGYID)
            ReplaceTextInWordDocument(newDocxFilePath, "{TIN}", TIN)
            ReplaceTextInWordDocument(newDocxFilePath, "{Precinct}", PRECINTNO)
            ReplaceTextInWordDocument(newDocxFilePath, "{numcon}", EMERNO)



            ReplaceTextInWordDocument(newDocxFilePath, "{Name}", NAMEOFAPPLICANT)
            ReplaceTextInWordDocument(newDocxFilePath, "{Address}", RESADDRESS)
            ReplaceTextInWordDocument(newDocxFilePath, "{BloodType}", BTYPE)
            ReplaceTextInWordDocument(newDocxFilePath, "{NameOfEmerContact}", EMERNAME)
            ReplaceTextInWordDocument(newDocxFilePath, "{addcon}", EMERADD)

            InsertImageInWordDocument(newDocxFilePath, "{image}", PHOTO)


            PrintDocxFile(newDocxFilePath, newDocxFileName)

        Catch ex As Exception
            MessageBox.Show("An error occurred on insert: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
    Public Sub PrintDocxFile(docxFilePath As String, filename As String)
        'Create a Document object

        ' Create a Document object
        Try
            ' Load the Word document using Spire.Doc
            Dim doc As New Documentss()
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
    Private Sub InsertImageInWordDocument(filePath As String, imagePlaceholder As String, imagePath As String)
        Using wordDoc As WordprocessingDocument = WordprocessingDocument.Open(filePath, True)
            Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
            Dim picbody As Body = mainPart.Document.Body

            ' Find the paragraph containing the image placeholder
            For Each paragraph As Paragraph In picbody.Descendants(Of Paragraph)()
                For Each run As Run In paragraph.Descendants(Of Run)()
                    For Each textElement As Text In run.Descendants(Of Text)()
                        If textElement.Text.Contains(imagePlaceholder) Then
                            ' Replace the placeholder text with an empty string (removing the placeholder)
                            textElement.Text = textElement.Text.Replace(imagePlaceholder, "")

                            ' Add an image part to the document
                            Dim imagePart As ImagePart = mainPart.AddImagePart(ImagePartType.Jpeg)

                            ' Feed the image data
                            Using stream As FileStream = New FileStream(imagePath, FileMode.Open)
                                imagePart.FeedData(stream)
                            End Using

                            ' Get the unique relationship ID for the image part
                            Dim relationshipId As String = mainPart.GetIdOfPart(imagePart)

                            ' Insert the image at the location of the placeholder
                            Dim element As New Drawing(
                                New DocumentFormat.OpenXml.Drawing.Wordprocessing.Inline(
                                    New DocumentFormat.OpenXml.Drawing.Wordprocessing.Extent() With {.Cx = 2116800L, .Cy = 2116800L}, ' Image size in EMU
                                    New DocumentFormat.OpenXml.Drawing.Wordprocessing.EffectExtent() With {.LeftEdge = 0L, .TopEdge = 0L, .RightEdge = 0L, .BottomEdge = 0L},
                                    New DocumentFormat.OpenXml.Drawing.Wordprocessing.DocProperties() With {.Id = 1UI, .Name = "Image 1"},
                                    New DocumentFormat.OpenXml.Drawing.Wordprocessing.NonVisualGraphicFrameDrawingProperties(
                                        New DocumentFormat.OpenXml.Drawing.GraphicFrameLocks() With {.NoChangeAspect = True}),
                                    New DocumentFormat.OpenXml.Drawing.Graphic(
                                        New DocumentFormat.OpenXml.Drawing.GraphicData(
                                            New DocumentFormat.OpenXml.Drawing.Pictures.Picture(
                                                New DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureProperties(
                                                    New DocumentFormat.OpenXml.Drawing.Pictures.NonVisualDrawingProperties() With {.Id = 0UI, .Name = "Image"},
                                                    New DocumentFormat.OpenXml.Drawing.Pictures.NonVisualPictureDrawingProperties()
                                                ),
                                                New DocumentFormat.OpenXml.Drawing.Pictures.BlipFill(
                                                    New DocumentFormat.OpenXml.Drawing.Blip() With {.Embed = relationshipId, .CompressionState = DocumentFormat.OpenXml.Drawing.BlipCompressionValues.Print},
                                                    New DocumentFormat.OpenXml.Drawing.Stretch(New DocumentFormat.OpenXml.Drawing.FillRectangle())
                                                ),
                                                New DocumentFormat.OpenXml.Drawing.Pictures.ShapeProperties(
                                                    New DocumentFormat.OpenXml.Drawing.Transform2D(
                                                        New DocumentFormat.OpenXml.Drawing.Offset() With {.X = 0L, .Y = 0L},
                                                        New DocumentFormat.OpenXml.Drawing.Extents() With {.Cx = 2116800L, .Cy = 1944000L}
                                                    ),
                                                    New DocumentFormat.OpenXml.Drawing.PresetGeometry(New DocumentFormat.OpenXml.Drawing.AdjustValueList()) With {.Preset = DocumentFormat.OpenXml.Drawing.ShapeTypeValues.Rectangle}
                                                )
                                            )
                                        ) With {.Uri = "http://schemas.openxmlformats.org/drawingml/2006/picture"}
                                    )
                                )
                            )

                            ' Append the drawing to the Run
                            run.AppendChild(New Run(element))
                        End If
                    Next
                Next
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
            Dim newBitmap2 As Bitmap = ID_PREV.Guna2PictureBox1.Image
            newBitmap.Save(filepath, ImageFormat.Png)
            Label5.Text = filepath
        End If
        CAMERA.SignalToStop()
        'MsgBox("picture saved")
    End Sub


    Private Function generatefilename() As String
        Return System.DateTime.Now.ToString("yyyyMMdd") + "_" + Guna2TextBox1.Text
    End Function

    Public Async Sub InsertTransactionLog()

        Dim InResidentId As Long? = Nothing

        ' Check if Label2.Text is empty or if InResidentId is null
        If String.IsNullOrWhiteSpace(Label2.Text) OrElse Not InResidentId.HasValue Then
            ' Insert new resident if InResidentId is null or Label2.Text is empty
            Dim selectedDate As DateTime = Guna2DateTimePicker1.Value

            ' Adjust the DateTime to the first day of the selected month
            Dim firstDayOfMonth As New DateTime(selectedDate.Year, selectedDate.Month, 1)
            ' Prepare the new resident data
            Dim newResident As New ResidentRecord() With {
            .ResidentFirstName = Form3.Guna2TextBox7.Text,
            .ResidentType = "Resident",
            .BirthPlace = "Sauyo",
            .Sex = "Male",
            .Address = Form3.Guna2TextBox9.Text,
            .ResidentBirthdate = firstDayOfMonth.ToString("yyyy-MM-dd"),
            .ResidentContactNumber = Guna2TextBox7.Text,
            .ResidentLastName = Form3.Guna2TextBox8.Text,
            .CivilStatus = "Single",
            .Sitio = Form3.Guna2ComboBox3.SelectedItem?.ToString(),
            .ResidentMiddleName = Form3.Guna2TextBox6.Text,
            .ResidentEmail = Guna2TextBox2.Text,
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
            Try
                InResidentId = Long.Parse(Label2.Text)
            Catch ex As FormatException
                'MessageBox.Show("Invalid Resident ID format.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If

        Try
            ' Create the transaction log object
            Dim log As New TransactionLog() With {
            .LogDate = Date.Today.ToString("yyyy-MM-dd"),
            .LogTime = DateTime.Now.ToString("HH:mm:ss"),
            .Type = Form3.Guna2ComboBox2.Text,
            .Status = "Completed",
            .Payment = Decimal.Parse(Form3.Guna2TextBox16.Text),
            .ResidentId = InResidentId,
            .StaffId = Form2.staffID
        }

            ' Use the same API client for both operations
            Dim client As New ApiClient()

            ' Debugging message before calling the API
            MessageBox.Show("Attempting to insert transaction log...")

            ' Insert transaction log
            Dim success As Boolean = Await client.InsertTransactionLogAsync(log)

            If success Then
                ' Notify the user of success
                MessageBox.Show("Transaction log inserted successfully!")
            Else
                ' Notify the user of failure
                MessageBox.Show("Failed to insert transaction log.")
            End If
        Catch ex As FormatException
            ' Handle invalid number format or other issues
            MessageBox.Show("Invalid input format: " & ex.Message, "Input Error", MessageBoxButtons.OK, MessageBoxIcon.Warning)
        Catch ex As Exception
            ' Handle any other exceptions that occurred and display the error message
            MessageBox.Show("Error: " & ex.Message, "General Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Public Sub onlyacceptnum(e As KeyPressEventArgs)
        If Not Char.IsControl(e.KeyChar) AndAlso Not Char.IsDigit(e.KeyChar) Then
            e.Handled = True
        End If
    End Sub


End Class