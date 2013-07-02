Public Class Util

    Public Shared Function escape(input As String) As String
        If Not IsNothing(input) Then
            If input.IndexOf("'"c) Then
                Return input.Replace("'", "''")
            Else
                Return input
            End If
        Else
            Return ""
        End If
    End Function
End Class
