Imports Gedcom.BL
Imports Gedcom.Model
Public Class SearchPage
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        If Not Page.IsPostBack Then
            txtFirstName.Text = Request.QueryString("FirstName")
            txtSurName.Text = Request.QueryString("SurName")

            If Not String.IsNullOrWhiteSpace(txtFirstName.Text) OrElse Not String.IsNullOrWhiteSpace(txtSurName.Text) Then
                Search()
            End If

        End If
    End Sub

    Protected Sub btnSearch_Click(sender As Object, e As EventArgs) Handles btnSearch.Click
        search()
    End Sub

    Private _MaxReg As Integer = 25
    Public Property MaxReg() As Integer
        Get
            Return _MaxReg
        End Get
        Set(ByVal value As Integer)
            _MaxReg = value
        End Set
    End Property


    Public Sub Search()
        Dim r = BLIndividuals.SearchByName(Me.txtFirstName.Text, Me.txtSurName.Text, New SearchOptions(Max:=MaxReg, IsAdmin:=Util.IsAdmin))
        If r.Count > 0 Then
            ResultList.Individuals = r
            pnlResults.Visible = True
            pnlNotFound.Visible = False
            trSearchTips.Visible = False
            If r.Count = MaxReg Then
                pnlMaxReg.Visible = True
            End If
        Else
            trSearchTips.Visible = True
            pnlResults.Visible = False
            pnlNotFound.Visible = True
        End If
    End Sub
End Class