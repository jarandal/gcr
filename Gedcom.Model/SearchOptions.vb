Public Class SearchOptions
    Public Max As Integer = 0
    Public Includes As String = ""
    Public IsAdmin As Boolean = False

    Public Sub New(Optional Max As Integer = 0, Optional Includes As String = "", Optional IsAdmin As Boolean = False)
        Me.Max = Max
        Me.Includes = Includes
        Me.IsAdmin = IsAdmin
    End Sub
End Class
