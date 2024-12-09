Imports Newtonsoft.Json

Public Class ApiResponse




    Public Class Account
        <JsonProperty("brgy_user_id")>
        Public Property BrgyUserId As Long

        <JsonProperty("brgy_account_id")>
        Public Property BrgyAccountId As Long

        <JsonProperty("brgy_email")>
        Public Property BrgyEmail As String

        <JsonProperty("brgy_firstName")>
        Public Property BrgyFirstName As String

        <JsonProperty("brgy_lastName")>
        Public Property BrgyLastName As String

        <JsonProperty("brgy_password_hashed")>
        Public Property BrgyPasswordHashed As String

        <JsonProperty("brgy_account_user_type")>
        Public Property BrgyAccountUserType As String

        <JsonProperty("brgy_account_date_created")>
        Public Property BrgyAccountDateCreated As String

        <JsonProperty("brgy_password")>
        Public Property BrgyPassword As String
    End Class

    Public Class AccountApiResponse
        <JsonProperty("bms_account_staffs")>
        Public Property BmsAccountStaffs As List(Of Account)
    End Class



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
        Public Property ResidentAccountCreated As String

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

        <JsonProperty("birth_date")>
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


    ' Wrapper for the API response
    Public Class TransactionLogResponse
        <JsonProperty("bms_resident_transaction_logs")>
        Public Property BmsResidentTransactionLogs As List(Of TransactionLog)
    End Class

    ' Individual transaction log model
    Public Class TransactionLog
        <JsonProperty("resident_id")>
        Public Property ResidentId As Long

        <JsonProperty("log_time")>
        Public Property LogTime As String

        <JsonProperty("status")>
        Public Property Status As String

        <JsonProperty("payment")>
        Public Property Payment As Decimal

        <JsonProperty("log_id")>
        Public Property LogId As Long

        <JsonProperty("log_date")>
        Public Property LogDate As String

        <JsonProperty("staff_id")>
        Public Property StaffId As String

        <JsonProperty("type")>
        Public Property Type As String
    End Class

    Public Class PermitLogResponse
        <JsonProperty("bms_resident_permit_logs")>
        Public Property BmsResidentPermitLogs As List(Of PermitLog)
    End Class

    Public Class PermitLog
        <JsonProperty("resident_id")>
        Public Property ResidentId As String

        <JsonProperty("b_details")>
        Public Property BDetails As String

        <JsonProperty("loc_type")>
        Public Property LocType As String

        <JsonProperty("b_address")>
        Public Property BAddress As String

        <JsonProperty("b_name")>
        Public Property BName As String

        <JsonProperty("m_rental")>
        Public Property MRental As Decimal

        <JsonProperty("log_id")>
        Public Property LogId As String

        <JsonProperty("stay_duration")>
        Public Property StayDuration As String

        <JsonProperty("p_log_id")>
        Public Property PLogId As Long
    End Class

End Class
