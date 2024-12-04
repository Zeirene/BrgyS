Imports MySql.Data.MySqlClient

Public Class Form6
    Private Sub Form6_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        UpdateBusinesses()
    End Sub

    Public Sub UpdateBusinesses()
        FlowLayoutPanel1.Controls.Clear()

        Try
            con.Open()
            Using cmd As New MySqlCommand("SELECT p_log_id, b_name, loc_type, b_address, b_details FROM permits_log;", con)
                Dim reader As MySqlDataReader = cmd.ExecuteReader()
                While reader.Read()
                    Dim pLogId = reader.GetInt32("p_log_id")
                    Dim businessName = reader.GetString("b_name")
                    Dim ownerName = reader.GetString("loc_type") ' Assuming loc_type represents owner name
                    Dim status = reader.GetString("b_details") ' Assuming b_details represents status

                    AddBusinessPanel(pLogId, businessName, ownerName, status)
                End While
                reader.Close()
            End Using
        Catch ex As Exception
            MsgBox("Error fetching data: " & ex.Message)
        Finally
            con.Close()
        End Try
    End Sub

    Private Sub AddBusinessPanel(ByVal pLogId As Integer, ByVal businessName As String, ByVal ownerName As String, ByVal status As String)
        ' Create a panel for the business
        Dim businessPanel As New Panel With {
            .Size = New Size(755, 80),
            .BackColor = Color.LightGray,
            .Margin = New Padding(10)
        }

        ' Add business name label
        Dim lblBusinessName As New Label With {
            .Text = "Business: " & businessName,
            .Font = New Font("Arial", 10, FontStyle.Bold),
            .Location = New Point(10, 10),
            .AutoSize = True
        }
        businessPanel.Controls.Add(lblBusinessName)

        ' Add owner name label
        Dim lblOwnerName As New Label With {
            .Text = "Owner: " & ownerName,
            .Font = New Font("Arial", 10),
            .Location = New Point(10, 30),
            .AutoSize = True
        }
        businessPanel.Controls.Add(lblOwnerName)

        ' Add status label
        Dim lblStatus As New Label With {
            .Text = "Status: " & status,
            .Font = New Font("Arial", 10),
            .Location = New Point(10, 50),
            .AutoSize = True
        }
        businessPanel.Controls.Add(lblStatus)

        ' Add email button
        Dim btnEmail As New Button With {
            .Text = "Send Email",
            .Size = New Size(100, 30),
            .Location = New Point(640, 25),
            .Tag = businessName ' Use Tag to store business information if needed
        }
        AddHandler btnEmail.Click, Sub() SendEmail(pLogId, businessName)
        businessPanel.Controls.Add(btnEmail)

        ' Add the panel to the FlowLayoutPanel
        FlowLayoutPanel1.Controls.Add(businessPanel)
    End Sub

    Private Sub SendEmail(ByVal pLogId As Integer, ByVal businessName As String)
        ' Placeholder for sending email logic
        MsgBox("Sending email to business: " & businessName & " (ID: " & pLogId & ")")
    End Sub
End Class