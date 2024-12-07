Imports Guna.UI2.WinForms

Public Class Permits_PREV
    Private _busname, _BADDRESS, _NATUREB As String
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
End Class