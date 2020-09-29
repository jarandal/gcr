Partial Public Class Family


    Private _WifeName As String
    Public Property WifeName() As String
        Get
            Return _WifeName
        End Get
        Set(ByVal value As String)
            _WifeName = value
        End Set
    End Property

    Private _HusbandName As String
    Public Property HusbandName() As String
        Get
            Return _HusbandName
        End Get
        Set(ByVal value As String)
            _HusbandName = value
        End Set
    End Property

    Public Function getInsertSql() As String

        Dim d1 As String = "NULL"
        If (Me.Date.HasValue) Then d1 = "'" & Me.Date.Value.ToString("s") & "'"

        Return "INSERT [dbo].[Families_temp] (" &
            "[Id], " &
            "[Original_Id], " &
            "[Notes], " &
            "[NotesSummary], " &
            "[Husband_Id], " &
            "[Wife_Id], " &
            "[Date]" &
            ") VALUES (" &
            "N'" & Me.Id & "', " &
            "N'" & Me.Original_Id & "', " &
            "N'" & Util.escape(Me.Notes) & "', " &
            "N'" & Util.escape(Me.NotesSummary) & "', " &
            "N'" & Me.Husband_Id & "', " &
            "N'" & Me.Wife_Id & "', " &
            d1 &
            ")" & vbCrLf & "GO" & vbCrLf
    End Function

End Class
