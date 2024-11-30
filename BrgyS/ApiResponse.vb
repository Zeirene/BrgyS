Imports Newtonsoft.Json

Public Class ApiResponse
    ' Define the classes to deserialize the JSON response
    Public Class ResidentApiResponse
        <JsonProperty("bms_resident_records")>
        Public Property bms_resident_records As List(Of ResidentRecord)
    End Class

    Public Class ResidentRecord
        <JsonProperty("resident_id")>
        Public Property ResidentId As Long
        <JsonProperty("resident_email")>
        Public Property ResidentEmail As String
        <JsonProperty("resident_first_name")>
        Public Property ResidentFirstName As String
        <JsonProperty("resident_contact_number")>
        Public Property ResidentContactNumber As String
        <JsonProperty("resident_last_name")>
        Public Property ResidentLastName As String
        <JsonProperty("resident_account_created")>
        Public Property ResidentAccountCreated As DateTime
        <JsonProperty("resident_middle_name")>
        Public Property ResidentMiddleName As String
        <JsonProperty("resident_home_address")>
        Public Property ResidentHomeAddress As String
        <JsonProperty("resident_birthdate")>
        Public Property ResidentBirthdate As Date
        <JsonProperty("resident_registered_date")>
        Public Property ResidentRegisteredDate As DateTime
    End Class
End Class
