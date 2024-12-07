Public Class Clearance_PREV

    ' Declare private variables for properties
    Private _resName As String
    Private _bday As String
    Private _stat As String
    Private _addr As String
    Private _stay As String
    Private _purp As String

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
        clearanceQCID.InsertTransactionLog()
    End Sub
End Class
