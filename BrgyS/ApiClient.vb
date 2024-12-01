Imports System.Net.Http
Imports System.Net.Http.Headers
Imports Newtonsoft.Json


Public Class ApiClient
    Private ReadOnly apiUrl As String = "https://yjme796l3k.execute-api.ap-southeast-2.amazonaws.com/dev/api/v1/brgy/residents/personal_records"

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
End Class
