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

        Return "INSERT [dbo].[Families] (" & _
            "[Id], " & _
            "[Notes], " & _
            "[NotesSummary], " & _
            "[Husband_Id], " & _
            "[Wife_Id], " & _
            "[Date]" & _
            ") VALUES (" & _
            "N'" & Me.Id & "', " & _
            "N'" & Util.escape(Me.Notes) & "', " & _
            "N'" & Util.escape(Me.NotesSummary) & "', " & _
            "N'" & Me.Husband_Id & "', " & _
            "N'" & Me.Wife_Id & "', " & _
            d1 & _
            ")" & vbCrLf & "GO" & vbCrLf
    End Function

End Class
