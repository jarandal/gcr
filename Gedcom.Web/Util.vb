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

    Public Shared Property IsAdmin() As Boolean
        Get
            If Not IsNothing(System.Web.HttpContext.Current) Then
                If Not IsNothing(System.Web.HttpContext.Current.Session) Then
                    If Not IsNothing(System.Web.HttpContext.Current.Session("ADMIN")) Then
                        Return CBool(System.Web.HttpContext.Current.Session("ADMIN"))
                    End If
                End If
            End If
            Return False
        End Get
        Set(ByVal value As Boolean)
            If Not IsNothing(System.Web.HttpContext.Current) Then
                If Not IsNothing(System.Web.HttpContext.Current.Session) Then
                    System.Web.HttpContext.Current.Session("ADMIN") = value
                End If
            End If
        End Set
    End Property


End Class
