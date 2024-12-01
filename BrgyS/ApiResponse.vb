Imports Newtonsoft.Json

Public Class ApiResponse
    ' Define the classes to deserialize the JSON response
    Public Class ResidentApiResponse
        <JsonProperty("bms_resident_records")>
        Public Property BmsResidentRecords As List(Of ResidentRecord)
    End Class

    Public Class ResidentRecord
        <JsonProperty("resident_id")>
        Public Property ResidentId As Long

        <JsonProperty("resident_type")>
        Public Property ResidentType As String

        <JsonProperty("resident_account_created")>
        Public Property ResidentAccountCreated As DateTime

        <JsonProperty("year_of_stay")>
        Public Property YearOfStay As Integer

        <JsonProperty("birth_place")>
        Public Property BirthPlace As String

        <JsonProperty("sex")>
        Public Property Sex As String

        <JsonProperty("address")>
        Public Property Address As String

        <JsonProperty("contact_number")>
        Public Property ResidentContactNumber As String

        <JsonProperty("blood_type")>
        Public Property BloodType As String

        <JsonProperty("email")>
        Public Property ResidentEmail As String

        <JsonProperty("resident_registered_date")>
        Public Property ResidentRegisteredDate As DateTime

        <JsonProperty("birthdate")>
        Public Property ResidentBirthdate As Date

        <JsonProperty("last_name")>
        Public Property ResidentLastName As String

        <JsonProperty("civil_status")>
        Public Property CivilStatus As String

        <JsonProperty("first_name")>
        Public Property ResidentFirstName As String

        <JsonProperty("sitio")>
        Public Property Sitio As String

        <JsonProperty("middle_name")>
        Public Property ResidentMiddleName As String

        <JsonProperty("street")>
        Public Property Street As String
    End Class
End Class
