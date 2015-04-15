Public Class Admin
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            Update()
        End If
    End Sub

    Protected Sub btnLogin_Click(sender As Object, e As EventArgs) Handles btnLogin.Click
        If Gedcom.BL.BLIndividuals.validateLogin(txtUser.Text, txtPassword.Text) Then
            Util.IsAdmin = True
        Else
            lblLoginError.Visible = True
        End If
        Update()
    End Sub

    Protected Sub btnLogout_Click(sender As Object, e As EventArgs) Handles btnLogout.Click
        Util.IsAdmin = False
        Me.Session.Abandon()
        Update()
    End Sub

    Sub Update()
        pnlLogin.Visible = Not Util.IsAdmin
        pnlLogout.Visible = Util.IsAdmin
    End Sub
End Class