Partial Public Class Individual

    Public ReadOnly Property FullName() As String
        Get
            Return FirstName & " " & SurName
        End Get
    End Property

    Public Function getInsertSql() As String

        Dim sb As New System.Text.StringBuilder

        Dim d1 As String = "NULL"
        If (Me.BirthDate.HasValue) Then d1 = "'" & Me.BirthDate.Value.ToString("s") & "'"

        Dim d2 As String = "NULL"
        If (Me.DeathDate.HasValue) Then d2 = "'" & Me.DeathDate.Value.ToString("s") & "'"

        Dim i1 As String = "NULL"
        If (Me.Dead.HasValue) Then i1 = IIf(Me.Dead.Value, "1", "0")

        sb.Append("INSERT [dbo].[Individuals] (" & _
        "[Id], " & _
        "[FirstName], " & _
        "[SurName], " & _
        "[Sex], " & _
        "[Notes], " & _
        "[NotesSummary], " & _
        "[Family_Id], " & _
        "[BirthDate], " & _
        "[BirthPlace], " & _
        "[DeathDate], " & _
        "[DeathPlace], " & _
        "[Dead], " & _
        "[Original_Id]" & _
        ") VALUES (" & _
        "N'" & Me.Id & "', " & _
        "N'" & Util.escape(Me.FirstName) & "', " & _
        "N'" & Util.escape(Me.SurName) & "', " & _
        "N'" & Me.Sex & "', " & _
        "N'" & Util.escape(Me.Notes) & "', " & _
        "N'" & Util.escape(Me.NotesSummary) & "', " & _
        IIf(String.IsNullOrWhiteSpace(Me.Family_Id), "NULL", "N'" & Me.Family_Id & "'") & ", " & _
        d1 & ", " & _
        "N'" & Util.escape(BirthPlace) & "', " & _
        d2 & ", " & _
        "N'" & Util.escape(DeathPlace) & "', " & _
        i1 & ", " & _
        "N'" & Me.Original_Id & "'" & _
        ")" & vbCrLf & "GO" & vbCrLf)

        For Each M In Media
            sb.Append(M.getInsertSql)
        Next

        For Each e In Events
            sb.Append(e.getInsertSql)
        Next

        Return sb.ToString

    End Function


End Class
