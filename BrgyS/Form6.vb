Imports MySql.Data.MySqlClient

Public Class Form6
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load

    End Sub

    Public Sub updateMessage()
        FlowLayoutPanel1.Controls.Clear()

        Try
            con.Open()
            Using cmd As New MySqlCommand("SELECT iid, title, idate, itime, content, istatus, uid, grid FROM inbox WHERE uid = @id;", con)
                'cmd.Parameters.AddWithValue("@id", orgID)
                Dim reader As MySqlDataReader = cmd.ExecuteReader
                While reader.Read
                    Dim iid = reader.GetInt32(0)
                    Dim title = reader.GetString(1)
                    Dim idate = reader.GetDateTime(2)
                    Dim itime = reader.GetTimeSpan(3)
                    Dim content = reader.GetString(4)
                    Dim istatus = reader.GetString(5)
                    Dim uid = reader.GetInt32(6)
                    Dim grid = reader.GetInt32(7)

                    NewMessage(iid, title, idate, itime, content, istatus, uid, grid)
                End While
                cmd.Parameters.Clear()
                reader.Close()
            End Using
        Catch ex As Exception
            MsgBox("From updating the message " & ex.Message)
        Finally
            con.Close()
        End Try

    End Sub
    Private Sub NewMessage(ByVal iid As Integer, ByVal title As String, ByVal idate As DateTime, ByVal itime As TimeSpan, ByVal content As String, ByVal istatus As String, ByVal uid As Integer, ByVal grid As Integer)

        Dim idateString As String = idate.ToString("yyyy-MM-dd")

        Dim messagePanel As New Panel With {
        .Size = New Size(755, 60),
        .BackColor = Color.FromArgb(44, 193, 146),
        .Enabled = True,
        .Visible = True
    }

        If istatus = "read" Then
            messagePanel.BackColor = Color.White
        End If

        FlowLayoutPanel1.Controls.Add(messagePanel)

        Dim mesTitle As New Label With {
        .Size = New Size(118, 21),
        .Location = New Point(29, 15),
        .Font = New Font("Bahnschrift SemiBold", 12.0F, FontStyle.Bold),
        .ForeColor = Color.White,
        .Text = title,
        .AutoSize = True,
        .Margin = New Padding(10, 10, 10, 10)
    }

        If istatus = "read" Then
            mesTitle.ForeColor = Color.Black
        End If

        Dim mesDate As New Label With {
        .Size = New Size(118, 21),
        .Location = New Point(650, 15),
        .Font = New Font("Bahnschrift SemiBold", 12.0F, FontStyle.Bold),
        .ForeColor = Color.White,
        .Text = idate.ToString("yyyy-MM-dd"),
        .AutoSize = False,
        .Margin = New Padding(10, 10, 10, 10)
    }

        If istatus = "read" Then
            mesDate.ForeColor = Color.Black
        End If

        messagePanel.Controls.Add(mesTitle)
        messagePanel.Controls.Add(mesDate)

        'AddHandler messagePanel.Click, Sub()
        '                                   updateInboxStatus(iid)
        '                                   Dim formInfo As New messageDetails(iid, title, idate, itime, content, istatus, uid, grid)
        '                                   formInfo.Show()
        '                               End Sub

    End Sub
    Private Sub updateInboxStatus(ByVal iid As Integer)
        openCon()
        Dim updateQuery As String = "UPDATE inbox SET istatus = 'read' WHERE iid = @iid"
        Using cmd As New MySqlCommand(updateQuery, con)
            cmd.Parameters.AddWithValue("@iid", iid)
            cmd.ExecuteNonQuery()
        End Using
        con.Close()
    End Sub
End Class