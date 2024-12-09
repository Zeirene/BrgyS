Imports System.Drawing.Printing

Public Class ID_PREV
    ' Declare private variables for properties
    Private _resName As String
    Private _idnum As String
    Private _img As Bitmap
    Private _addr As String
    Private _tin As String
    Private _bday As String
    Private _btype As String
    Private _precint As String
    Private _emername As String
    Private _emercont As String
    Private _emeraddr As String
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
    Public Property idnum As String
        Get
            Return _idnum
        End Get
        Set(value As String)
            _idnum = value
        End Set
    End Property

    ' Public property for stat
    Public Property img As Bitmap
        Get
            Return _img
        End Get
        Set(value As Bitmap)
            _img = value
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
    Public Property tin As String
        Get
            Return _tin
        End Get
        Set(value As String)
            _tin = value
        End Set
    End Property

    ' Public property for purp
    Public Property bday As String
        Get
            Return _bday
        End Get
        Set(value As String)
            _bday = value
        End Set
    End Property
    Public Property btype As String
        Get
            Return _btype
        End Get
        Set(value As String)
            _btype = value
        End Set
    End Property
    Public Property precint As String
        Get
            Return _precint
        End Get
        Set(value As String)
            _precint = value
        End Set
    End Property
    Public Property emername As String
        Get
            Return _emername
        End Get
        Set(value As String)
            _emername = value
        End Set
    End Property
    Public Property emercont As String
        Get
            Return _emercont
        End Get
        Set(value As String)
            _emercont = value
        End Set
    End Property
    Public Property emeraddr As String
        Get
            Return _emeraddr
        End Get
        Set(value As String)
            _emeraddr = value
        End Set
    End Property


    Private Sub ID_PREV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        insertdata()
    End Sub

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        CaptureForm()
        brgyID.InsertTransactionLog()
        Dim newform As New Form3()
        Form2.switchPanel(newform)

        Me.Close()

    End Sub
    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Me.Close()
    End Sub

    Public Sub insertdata()
        Label1.Text = resName
        Label2.Text = idnum
        'Guna2PictureBox1.Image = img.ToString
        Label3.Text = addr
        Label4.Text = tin
        Label5.Text = bday
        Label6.Text = btype
        Label7.Text = precint
        Label8.Text = emername
        Label9.Text = emercont
        Label10.Text = emeraddr

    End Sub
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