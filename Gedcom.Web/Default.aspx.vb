Public Class _Default
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        Response.Redirect(String.Format("gcr/SearchPage.aspx?FirstName={0}&SurName={1}", txtFirstName.Text.Trim, txtSurName.Text.Trim))
    End Sub
End Class