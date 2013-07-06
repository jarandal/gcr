Imports Gedcom.Model
Public Class IndividualUC
    Inherits System.Web.UI.UserControl

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
    End Sub

    Private _individual As Individual
    Public Property Individual() As Individual
        Get
            Return _individual
        End Get
        Set(ByVal value As Individual)
            _individual = value
        End Set
    End Property

    Public Property IsNameVisible() As Boolean
        Get
            If IsNothing(ViewState("IsNameVisible")) Then Return True
            Return ViewState("IsNameVisible")
        End Get
        Set(ByVal value As Boolean)
            ViewState("IsNameVisible") = value
        End Set
    End Property

    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not IsNothing(Individual) Then
            If IsNameVisible = False Then
                pnlName.Visible = False
            End If
            hlnkNames.Text = Individual.FirstName & " " & Individual.SurName
            hlnkNames.NavigateUrl = "IndividualPage.aspx?Id=" & Individual.Original_Id

            If Individual.BirthDate.HasValue OrElse Not String.IsNullOrWhiteSpace(Individual.BirthPlace) Then
                lblBirth.Text = Util.FormatDate(Individual.BirthDate) & " " & Individual.BirthPlace
            Else
                pnlBirth.Visible = False
            End If
            If Individual.DeathDate.HasValue OrElse Not String.IsNullOrWhiteSpace(Individual.DeathPlace) Then
                lblDeath.Text = Util.FormatDate(Individual.DeathDate) & " " & Individual.DeathPlace
            Else
                pnlDeath.Visible = False
            End If
        End If
    End Sub
End Class