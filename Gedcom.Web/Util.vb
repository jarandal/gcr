Public Class Util
    Public Shared Function FormatDate(d As Date?) As String
        If Not IsNothing(d) Then
            If d.HasValue Then

                If d.Value.DayOfYear = 1 Then
                    Return d.Value.Year
                Else
                    Return d.Value.ToShortDateString()
                End If


            End If
        End If
        Return ""
    End Function

End Class
