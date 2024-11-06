Imports AForge.Video
Imports AForge.Video.DirectShow
Imports DocumentFormat.OpenXml.Packaging
Imports DocumentFormat.OpenXml.Wordprocessing
Imports MySql.Data.MySqlClient
Imports System.Drawing.Imaging
Imports Syncfusion.DocIO.DLS
Imports Syncfusion.DocToPDFConverter
Imports Syncfusion.Pdf
Imports System.IO

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
        Guna2TextBox3.Text = Form3.Guna2TextBox9.Text 'address

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
        If PictureBox1.Image IsNot Nothing Then
            Dim newBitmap As Bitmap = PictureBox1.Image
        End If
        CAMERA.SignalToStop()
        PictureBox1.Image = PictureBox1.Image
        Savepic()

    End Sub
    Private Sub Guna2Button2_Click(sender As Object, e As EventArgs) Handles Guna2Button2.Click
        'clear
        PictureBox1.Image = Nothing
    End Sub
    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        'save and print docu
        generatedocfile()
        InsertTransactionLog()
    End Sub

    'functions and sub
    Private Sub generatedocfile()
        ' Path to the template document
        Dim templatePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\BRGYidTEMP.docx"

        ' Input values from the textboxes
        Dim NAMEOFAPPLICANT As String = Guna2TextBox1.Text
        Dim BRGYID As String = resID
        Dim RESADDRESS As String = Guna2TextBox3.Text
        Dim PHOTO As String = Label1.Text
        Dim TIN As String = Guna2TextBox4.Text
        Dim BDAY As String = Guna2DateTimePicker1.Value.ToString("MM/dd/yyyy")
        Dim BTYPE As String = Guna2ComboBox1.Text
        Dim PRECINTNO As String = Guna2TextBox6.Text
        Dim EMERNAME As String = Guna2TextBox8.Text
        Dim EMERNO As String = Guna2TextBox7.Text
        Dim EMERADD As String = Guna2TextBox9.Text

        ' Generate a new .docx file path
        Dim sanitizedStudentName As String = NAMEOFAPPLICANT
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd")
        Dim newDocxFileName As String = sanitizedStudentName & "_" & dateTimeStamp & ".docx"
        Dim newDocxFilePath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", newDocxFileName)

        ' Copy the template file and replace placeholders
        File.Copy(templatePath, newDocxFilePath, True)
        ReplaceTextInWordDocument(newDocxFilePath, "{Name}", NAMEOFAPPLICANT)
        ReplaceTextInWordDocument(newDocxFilePath, "{IDNumber}", BRGYID)
        ReplaceTextInWordDocument(newDocxFilePath, "{Address}", RESADDRESS)
        InsertImageInWordDocument(newDocxFilePath, "{Photo}", PHOTO)
        ReplaceTextInWordDocument(newDocxFilePath, "{TIN}", TIN)
        ReplaceTextInWordDocument(newDocxFilePath, "{BDay}", BDAY)
        ReplaceTextInWordDocument(newDocxFilePath, "{BloodType}", BTYPE)
        ReplaceTextInWordDocument(newDocxFilePath, "{Precinct}", PRECINTNO)
        ReplaceTextInWordDocument(newDocxFilePath, "{NameOfEmerContact}", EMERNAME)
        ReplaceTextInWordDocument(newDocxFilePath, "{NumberOfEmerContact}", EMERNO)
        ReplaceTextInWordDocument(newDocxFilePath, "{AddressOfEmerContact}", EMERADD)

        '' Convert to PDF using Syncfusion
        'Dim pdfDocPath As String = Path.Combine("C:\Users\John Roi\source\repos\BrgyS\BrgyS\docu\generated docu\", sanitizedStudentName & "_" & dateTimeStamp & ".pdf")
        'ConvertDocxToPdf(newDocxFilePath, pdfDocPath)

        '' Print the PDF
        'PrintPdf(pdfDocPath)


        MessageBox.Show("Document created, saved as PDF, and sent to printer.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)
    End Sub
    'Private Sub ConvertDocxToPdf(docxFilePath As String, pdfFilePath As String)
    '    ' Open a FileStream for the .docx file
    '    Using fileStream As New FileStream(docxFilePath, FileMode.Open, FileAccess.Read)
    '        ' Load the Word document from the stream with the correct FormatType
    '        Using wordDoc As New WordDocument(fileStream, Syncfusion.DocIO.FormatType.Docx)
    '            ' Initialize DocToPDFConverter
    '            Dim converter As New Syncfusion.DocToPDFConverter.DocToPDFConverter()

    '            ' Convert the Word document to PDF
    '            Dim pdfDoc As Syncfusion.Pdf.PdfDocument = converter.ConvertToPDF(wordDoc)

    '            ' Save the PDF document to the specified file path
    '            pdfDoc.Save(pdfFilePath)

    '            ' Close the PDF document
    '            pdfDoc.Close(True)
    '        End Using
    '    End Using
    'End Sub

    '' Print the PDF document
    'Private Sub PrintPdf(pdfFilePath As String)
    '    Using pdfDoc As New Syncfusion.Pdf.Parsing.PdfLoadedDocument(pdfFilePath)
    '        ' Create a printer instance for the PDF
    '        Dim pdfPrinter As New Syncfusion.Pdf.Parsing.PdfDocumentPrinter(pdfDoc)
    '        ' Print the PDF document
    '        pdfPrinter.Print()
    '    End Using
    'End Sub

    Private Sub InsertImageInWordDocument(filePath As String, imagePlaceholder As String, imagePath As String)
        Using wordDoc As WordprocessingDocument = WordprocessingDocument.Open(filePath, True)
            Dim mainPart As MainDocumentPart = wordDoc.MainDocumentPart
            Dim documentBody As Body = mainPart.Document.Body

            ' Find the paragraph containing the image placeholder
            For Each paragraph As Paragraph In documentBody.Descendants(Of Paragraph)()
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
                                    New DocumentFormat.OpenXml.Drawing.Wordprocessing.Extent() With {.Cx = 990000L, .Cy = 792000L}, ' Image size in EMU
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
                                                        New DocumentFormat.OpenXml.Drawing.Extents() With {.Cx = 990000L, .Cy = 792000L}
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
    Private Sub ConvertDocxToPdfAndPrint(docxFilePath As String, pdfFilePath As String)
        Dim wordApp As New Microsoft.Office.Interop.Word.Application()
        Dim wordDoc As Microsoft.Office.Interop.Word.Document = Nothing

        Try
            ' Open the DOCX file
            wordDoc = wordApp.Documents.Open(docxFilePath)

            ' Export as PDF
            wordDoc.ExportAsFixedFormat(pdfFilePath, Microsoft.Office.Interop.Word.WdExportFormat.wdExportFormatPDF)

            ' Print the PDF
            wordDoc.PrintOut()

        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Finally
            ' Close Word application and document
            If wordDoc IsNot Nothing Then wordDoc.Close(False)
            wordApp.Quit(False)
        End Try
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
            Label1.Text = filepath
        End If

        MsgBox("picture saved")
        CAMERA.SignalToStop()
    End Sub
    Private Function generatefilename() As String
        Return System.DateTime.Now.ToString("yyyyMMdd") + "_" + Guna2TextBox1.Text
    End Function

    Public Sub InsertTransactionLog()
        Dim logDate As Date = Date.Today
        Dim logTime As Date = DateTime.Now.ToString("HH:mm:ss")
        Dim logType As String = Form3.Guna2ComboBox2.Text
        Dim logStatus As String = "Completed"
        Dim payment As Decimal = Decimal.Parse(Form3.Guna2TextBox16.Text)
        Dim residentId As String = Label2.Text
        Dim staffId As String = Form2.staffID ' staffId is treated as a String

        Try
            ' Open the connection to the database
            con.Open()

            'Prepare the SQL query to insert the transaction log data
            Dim insertQuery As String = "INSERT INTO transaction_log (log_date, log_time, type, status, payment, resident_id, staff_id) 
                                     VALUES (@log_date, @log_time, @type, @status, @payment, @resident_id, @staff_id)"

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


End Class