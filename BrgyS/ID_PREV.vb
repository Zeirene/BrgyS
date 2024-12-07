Public Class ID_PREV
    ' Declare private variables for properties
    Private _resName As String
    Private _idnum As String
    Private _img As String
    Private _addr As String
    Private _tin As String
    Private _bday As String
    Private _btype As String
    Private _precint As String
    Private _emername As String
    Private _emercont As String
    Private _emeraddr As String


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
    Public Property img As String
        Get
            Return _img
        End Get
        Set(value As String)
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
End Class