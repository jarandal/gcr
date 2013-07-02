Partial Public Class [Event]

    Public Function getInsertSql() As String

        Dim d1 As String = "NULL"
        If (Me.Date.HasValue) Then d1 = "'" & Me.Date.Value.ToString("s") & "'"

        Return "INSERT [dbo].[Events] (" & _
            "[Type], " & _
            "[Date], " & _
            "[Notes], " & _
            "[Individual_Id], " & _
            "[Place]" & _
            ") VALUES (" & _
            "N'" & Me.Type & "', " & _
            d1 & ", " & _
            "N'" & Util.escape(Me.Notes) & "', " & _
            "N'" & Me.Individual_Id & "', " & _
            "N'" & Util.escape(Me.Place) & "'" & _
            ")" & vbCrLf & "GO" & vbCrLf

    End Function

End Class
