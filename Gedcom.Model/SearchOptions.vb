Public Class SearchOptions
    Public Max As Integer = 0
    Public Includes As String = ""

    Public Sub New(Optional Max As Integer = 0, Optional Includes As String = "")
        Me.Max = Max
        Me.Includes = Includes
    End Sub
End Class
