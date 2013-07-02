Public Class Media

    Public Function getInsertSql() As String

        Dim i1 As String = "NULL"
        If (Me.SortOrder.HasValue) Then i1 = CInt(Me.SortOrder.Value)

        Return "INSERT [dbo].[Media] (" & _
            "[Filename], " & _
            "[Title], " & _
            "[Notes], " & _
            "[SortOrder], " & _
            "[Individual_Id]" & _
            ") VALUES (" & _
            "N'" & Util.escape(Me.Filename) & "', " & _
            "N'" & Util.escape(Me.Title) & "', " & _
            "N'" & Util.escape(Me.Notes) & "', " & _
            i1 & ", " & _
            "N'" & Me.Individual_Id & "'" & _
            ")" & vbCrLf & "GO" & vbCrLf

    End Function

End Class
