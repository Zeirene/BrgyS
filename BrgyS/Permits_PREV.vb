Imports System.Drawing.Printing
Imports Guna.UI2.WinForms

Public Class Permits_PREV
    Private _busname, _BADDRESS, _NATUREB As String
    Private _bitmapToPrint As Bitmap

    Public Property busname As String
        Get
            Return _busname
        End Get
        Set(value As String)
            _busname = value
            Try
                Label3.Text = value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Set
    End Property
    Public Property BADDRESS As String
        Get
            Return _BADDRESS
        End Get
        Set(value As String)
            _BADDRESS = value
            Try
                Label4.Text = value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Set
    End Property
    Public Property NATUREB As String
        Get
            Return _NATUREB
        End Get
        Set(value As String)
            _NATUREB = value
            Try
                Label5.Text = value
            Catch ex As Exception
                MsgBox(ex.Message)
            End Try
        End Set
    End Property
    Private Sub Permits_PREV_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        inserttolblb()
    End Sub

    Public Sub inserttolblb()
        Dim resname, addr As String
        resname = Form3.Guna2TextBox6.Text + "," + Form3.Guna2TextBox7.Text + " " + Form3.Guna2TextBox8.Text 'name
        addr = Form3.Guna2TextBox9.Text + " " + Form3.Guna2ComboBox2.Text + " " + Form3.Guna2ComboBox3.Text + " Brgy Sta Lucia, QUEZON CITY" 'address


        Label1.Text = resname
        Label2.Text = addr
        'Label3.Text = busname
        'Label4.Text = Form7.BADDRESS
        'Label5.Text = Form7.NATUREB



    End Sub

    Private Sub Guna2Button1_Click(sender As Object, e As EventArgs)


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
        Dim newFileName As String = $"{saveFolder}{_busname}_{typeofpaper}_{dateTimeStamp}.png"

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

    Private Sub Guna2Button3_Click(sender As Object, e As EventArgs) Handles Guna2Button3.Click
        CaptureForm()
        Form7.InsertTransactionLog()
        Dim newform As New Form3
        Form2.switchPanel(newform)
        Me.Close()
    End Sub

    Private Sub Guna2Button4_Click(sender As Object, e As EventArgs) Handles Guna2Button4.Click
        Me.Close()
    End Sub
End Class