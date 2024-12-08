Imports System.Drawing.Printing
Public Class Clearance_PREV

    ' Declare private variables for properties
    Private _resName As String
    Private _bday As String
    Private _stat As String
    Private _addr As String
    Private _stay As String
    Private _purp As String
    Private _bitmapToPrint As Bitmap

    ' Public property for resName
    Public Property resName As String
        Get
            Return _resName
        End Get
        Set(value As String)
            _resName = value
        End Set
    End Property

    ' Public property for bday
    Public Property bday As String
        Get
            Return _bday
        End Get
        Set(value As String)
            _bday = value
        End Set
    End Property

    ' Public property for stat
    Public Property stat As String
        Get
            Return _stat
        End Get
        Set(value As String)
            _stat = value
        End Set
    End Property

    ' Public property for addr
    Public Property addr As String
        Get
            Return _addr
        End Get
        Set(value As String)
            _addr = value
        End Set
    End Property

    ' Public property for stay
    Public Property stay As String
        Get
            Return _stay
        End Get
        Set(value As String)
            _stay = value
        End Set
    End Property

    ' Public property for purp
    Public Property purp As String
        Get
            Return _purp
        End Get
        Set(value As String)
            _purp = value
        End Set
    End Property

    ' Event handler for form load
    Private Sub Clearance_PREV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        insertintoRTB()
    End Sub

    ' Method to insert values into RichTextBox controls
    Public Sub insertintoRTB()
        Try
            ' Prepare the text
            Dim resident As String = _resName
            Dim bdayFormatted As String = clearanceQCID.Guna2DateTimePicker1.Value.ToString("MMMM d, yyyy")
            Dim text As String = $"This is to certify that {resident}, born on {bdayFormatted}, of {_stat} status, is a bona fide resident of {_addr}, Barangay Sta. Lucia, Quezon City, and has been residing in this barangay for {_stay} years."
            Dim text2 As String = $"This certification is issued upon the request of the above-named resident for the purpose of {_purp}."
            Dim text3 As String = $"Issued this {DateTime.Now:dd} of {DateTime.Now:MMMM, yyyy} at Barangay Sta. Lucia, Quezon City, Philippines."

            ' Insert the text into RichTextBox controls
            RichTextBox1.Text = text
            RichTextBox2.Text = text2
            RichTextBox3.Text = text3
        Catch ex As Exception
            ' Display an error message
            MsgBox($"An error occurred: {ex.Message}", MsgBoxStyle.Critical, "Error")
        End Try
    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs) Handles Guna2Button1.Click
        CaptureForm()
        'clearanceQCID.InsertTransactionLog()
    End Sub

    'Private Sub CaptureForm()
    '    ' Define the rectangle for the part of the form to capture
    '    Dim captureArea As New Rectangle(1, 1, 522, 700)

    '    ' Create a bitmap with the size of the capture area
    '    Dim bmp As New Bitmap(captureArea.Width, captureArea.Height)

    '    ' Create a graphics object to draw onto the bitmap
    '    Using g As Graphics = Graphics.FromImage(bmp)
    '        ' Capture the specified part of the form
    '        g.CopyFromScreen(Me.PointToScreen(captureArea.Location), Point.Empty, captureArea.Size)
    '    End Using

    '    ' Generate file name with details
    '    Dim typeofpaper As String = Form3.Guna2ComboBox1.Text
    '    Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd")
    '    Dim newDocxFileName As String = _resName & "_" & typeofpaper & "_" & dateTimeStamp

    '    ' Save the bitmap to a file (e.g., PNG)
    '    Dim savePath As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\Generated Pics\captured form\" & newDocxFileName & ".png"
    '    bmp.Save(savePath, Imaging.ImageFormat.Png)

    '    ' Release resources
    '    bmp.Dispose()

    '    MessageBox.Show("Specified part of the form captured and saved!")
    'End Sub
    Private Sub CaptureForm()
        ' Define the rectangle for the part of the form to capture
        Dim captureArea As New Rectangle(1, 1, 522, 700)

        ' Create a bitmap with the size of the capture area
        Dim originalBitmap As New Bitmap(captureArea.Width, captureArea.Height)

        ' Create a graphics object to draw onto the bitmap
        Using g As Graphics = Graphics.FromImage(originalBitmap)
            ' Capture the specified part of the form
            g.CopyFromScreen(Me.PointToScreen(captureArea.Location), Point.Empty, captureArea.Size)
        End Using

        ' Resize the bitmap to fit the desired paper size (8.5 x 11 inches at 96 DPI)
        Dim paperWidth As Integer = CInt(21.59 * 96 / 2.54) ' Convert cm to pixels (96 DPI)
        Dim paperHeight As Integer = CInt(27.94 * 96 / 2.54) ' Convert cm to pixels (96 DPI)
        _bitmapToPrint = New Bitmap(paperWidth, paperHeight)

        Using g As Graphics = Graphics.FromImage(_bitmapToPrint)
            ' Draw the original bitmap scaled to fit the paper
            g.InterpolationMode = Drawing2D.InterpolationMode.HighQualityBicubic
            g.DrawImage(originalBitmap, New Rectangle(0, 0, paperWidth, paperHeight))
        End Using

        ' Dispose of the original bitmap as it's no longer needed
        originalBitmap.Dispose()

        ' Automatically save the resized image to the specified folder
        Dim saveFolder As String = "C:\Users\John Roi\source\repos\BrgyS\BrgyS\Generated Pics\captured form\"
        Dim typeofpaper As String = Form3.Guna2ComboBox1.Text
        Dim dateTimeStamp As String = DateTime.Now.ToString("yyyyMMdd")
        Dim newFileName As String = $"{saveFolder}{_resName}_{typeofpaper}_{dateTimeStamp}.png"

        ' Ensure the folder exists
        If Not IO.Directory.Exists(saveFolder) Then
            IO.Directory.CreateDirectory(saveFolder)
        End If

        ' Save the resized bitmap
        _bitmapToPrint.Save(newFileName, Imaging.ImageFormat.Png)
        MessageBox.Show($"Captured image saved at: {newFileName}")

        ' Show Print Dialog
        Dim printDialog As New PrintDialog()
        Dim printDoc As New PrintDocument()

        ' Set custom paper size
        Dim paperSize As New PaperSize("CustomPaper", paperWidth, paperHeight)
        printDoc.DefaultPageSettings.PaperSize = paperSize

        ' Assign the PrintPage event handler to print the bitmap
        AddHandler printDoc.PrintPage, AddressOf PrintPageHandler
        printDialog.Document = printDoc

        ' Display the dialog and print if the user clicks OK
        If printDialog.ShowDialog() = DialogResult.OK Then
            printDoc.Print()
        End If

        ' Dispose of the resized bitmap after saving and printing
        _bitmapToPrint.Dispose()
    End Sub

    Private Sub PrintPageHandler(sender As Object, e As PrintPageEventArgs)
        ' Draw the resized bitmap onto the print document
        If _bitmapToPrint IsNot Nothing Then
            ' Get the dimensions of the page
            Dim pageBounds As Rectangle = e.PageBounds

            ' Center the image on the page
            Dim marginX As Integer = (pageBounds.Width - _bitmapToPrint.Width) \ 2
            Dim marginY As Integer = (pageBounds.Height - _bitmapToPrint.Height) \ 2

            e.Graphics.DrawImage(_bitmapToPrint, marginX, marginY)
        End If
    End Sub


End Class
