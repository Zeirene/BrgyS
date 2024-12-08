Imports System.Net.Http
Imports System.Net.Http.Headers
Imports BrgyS.ApiResponse
Imports System.Text
Imports Newtonsoft.Json
Imports Newtonsoft.Json.Linq


Public Class ApiClient
    Private ReadOnly apiUrl As String = "https://yjme796l3k.execute-api.ap-southeast-2.amazonaws.com/dev/api/v1/brgy/residents/personal_records"
    Private ReadOnly apiUrl2 As String = "https://yjme796l3k.execute-api.ap-southeast-2.amazonaws.com/dev/api/v1/brgy/residents/transaction_logs"
    Private ReadOnly apiUrl3 As String = "https://yjme796l3k.execute-api.ap-southeast-2.amazonaws.com/dev/api/v1/brgy/account_portal_existing"
    Private ReadOnly apiUrl4 As String = "https://yjme796l3k.execute-api.ap-southeast-2.amazonaws.com/dev/api/v1/brgy/residents/permit_logs"


    Public Async Function GetAccountRecordsAsync() As Task(Of List(Of ApiResponse.Account))
        Using client As New HttpClient()
            ' Set headers (if required)
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Try
                ' Send GET request
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl3)

                If response.IsSuccessStatusCode Then
                    ' Read the response content
                    Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()

                    ' Log the raw response for debugging purposes
                    'MessageBox.Show("Raw API Response: " & jsonResponse)

                    ' Parse the JSON response into a .NET object
                    Dim apiResponse As ApiResponse.AccountApiResponse = JsonConvert.DeserializeObject(Of ApiResponse.AccountApiResponse)(jsonResponse)

                    ' Return the list of accounts from the response
                    Return apiResponse.BmsAccountStaffs
                Else
                    ' Handle non-successful response
                    Throw New Exception($"API Request failed with status code: {response.StatusCode}")
                End If
            Catch ex As Exception
                ' Handle exceptions
                Throw New Exception("An error occurred while fetching account records: " & ex.Message)
            End Try
        End Using
    End Function

    ' Function to fetch resident records
    Public Async Function GetResidentRecordsAsync() As Task(Of List(Of ApiResponse.ResidentRecord))
        Using client As New HttpClient()
            ' Set headers (if required)
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Try
                ' Send GET request
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl)

                If response.IsSuccessStatusCode Then
                    ' Read the response content
                    Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()

                    ' Parse the JSON response into a .NET object
                    Dim apiResponse As ApiResponse.ResidentApiResponse = JsonConvert.DeserializeObject(Of ApiResponse.ResidentApiResponse)(jsonResponse)
                    Return apiResponse.BmsResidentRecords
                Else
                    ' Handle non-successful response
                    Throw New Exception($"API Request failed with status code: {response.StatusCode}")
                End If
            Catch ex As Exception
                ' Handle exceptions
                Throw New Exception("An error occurred while fetching resident records: " & ex.Message)
            End Try
        End Using
    End Function


    Public Async Function GetTransactionLogsAsync() As Task(Of List(Of TransactionLog))
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            client.Timeout = TimeSpan.FromSeconds(30) ' Set timeout for the request

            Try
                ' Send GET request
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl2)

                If response.IsSuccessStatusCode Then
                    ' Read the response content
                    Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()

                    ' Log the response for debugging
                    Console.WriteLine(jsonResponse)

                    ' Parse the JSON response into the response model
                    Dim transactionResponse As TransactionLogResponse = JsonConvert.DeserializeObject(Of TransactionLogResponse)(jsonResponse)

                    ' Return the list of transaction logs
                    Return transactionResponse.BmsResidentTransactionLogs
                Else
                    ' Handle non-successful response
                    Throw New Exception($"API Request failed with status code: {response.StatusCode}")
                End If
            Catch ex As Exception
                ' Log and rethrow the exception for debugging
                Console.WriteLine("Error occurred while fetching transaction logs: " & ex.Message)
                Throw
            End Try
        End Using
    End Function


    Public Async Function InsertTransactionLogAsync(log As TransactionLog) As Task(Of Long?)
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Try
                ' Serialize the log object to JSON
                Dim jsonContent As String = JsonConvert.SerializeObject(log)
                Dim httpContent As New StringContent(jsonContent, Encoding.UTF8, "application/json")

                ' Send POST request
                Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl2, httpContent)

                If response.IsSuccessStatusCode Then
                    ' Read the response content
                    Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()

                    ' Parse the response to extract log_id
                    Dim responseData As JObject = JObject.Parse(jsonResponse)
                    Dim logId As Long = responseData("newly_data")("log_id").ToObject(Of Long)()

                    ' Return the log_id
                    Return logId
                Else
                    ' Log failure and return Nothing
                    MessageBox.Show("API Request failed: " & response.StatusCode.ToString())
                    Return Nothing
                End If
            Catch ex As Exception
                ' Handle exceptions
                MessageBox.Show("Error: " & ex.Message)
                Return Nothing
            End Try
        End Using
    End Function


    Public Async Function GetPermitLogsAsync() As Task(Of List(Of PermitLog))
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))
            client.Timeout = TimeSpan.FromSeconds(30) ' Set timeout for the request

            Try
                ' Send GET request
                Dim response As HttpResponseMessage = Await client.GetAsync(apiUrl4)

                If response.IsSuccessStatusCode Then
                    ' Read the response content
                    Dim jsonResponse As String = Await response.Content.ReadAsStringAsync()

                    ' Log the response for debugging
                    Console.WriteLine(jsonResponse)

                    ' Parse the JSON response into the response model
                    Dim permitResponse As PermitLogResponse = JsonConvert.DeserializeObject(Of PermitLogResponse)(jsonResponse)

                    ' Return the list of permit logs
                    Return permitResponse.BmsResidentPermitLogs
                Else
                    ' Handle non-successful response
                    Throw New Exception($"API Request failed with status code: {response.StatusCode}")
                End If
            Catch ex As Exception
                ' Log and rethrow the exception for debugging
                Console.WriteLine("Error occurred while fetching permit logs: " & ex.Message)
                Throw
            End Try
        End Using
    End Function

    Public Async Function InsertPermitLogAsync(log As PermitLog) As Task(Of Boolean)
        Using client As New HttpClient()
            client.DefaultRequestHeaders.Accept.Add(New MediaTypeWithQualityHeaderValue("application/json"))

            Try
                ' Serialize the log into JSON
                Dim jsonContent As String = JsonConvert.SerializeObject(log)

                ' Prepare the HTTP content
                Dim httpContent As New StringContent(jsonContent, Encoding.UTF8, "application/json")

                ' Send POST request
                Dim response As HttpResponseMessage = Await client.PostAsync(apiUrl4, httpContent)

                ' Check if the response is successful
                If response.IsSuccessStatusCode Then
                    ' Log success for debugging
                    Console.WriteLine("Permit log inserted successfully.")
                    Return True
                Else
                    ' Handle non-successful response
                    Dim responseBody As String = Await response.Content.ReadAsStringAsync()
                    Console.WriteLine("API Request failed: " & responseBody)
                    Return False
                End If
            Catch ex As Exception
                ' Handle exceptions
                Console.WriteLine("An error occurred while inserting permit log: " & ex.Message)
                Return False
            End Try
        End Using
    End Function



End Class
