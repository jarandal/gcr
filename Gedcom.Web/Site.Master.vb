Public Class Site
    Inherits System.Web.UI.MasterPage

    Private Sub Page_Load(sender As Object, e As System.EventArgs) Handles Me.Load
        If My.Settings.Debug = True Then
            divTitle.Visible = False
        End If
    End Sub
End Class