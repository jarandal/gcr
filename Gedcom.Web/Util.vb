Public Class Util
    Public Shared Function FormatDate(d As Date?) As String
        If Not IsNothing(d) Then
            If d.HasValue Then
                Return d.Value.ToShortDateString()
            End If
        End If
        Return ""
    End Function

End Class
