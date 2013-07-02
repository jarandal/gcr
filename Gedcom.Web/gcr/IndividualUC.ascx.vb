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


    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not IsNothing(Individual) Then
            hlnkNames.Text = Individual.FirstName & " " & Individual.SurName
            hlnkNames.NavigateUrl = "IndividualPage.aspx?Id=" & Individual.Original_Id
            lblBirth.Text = Util.FormatDate(Individual.BirthDate) & " " & Individual.BirthPlace
            lblDeath.Text = Util.FormatDate(Individual.DeathDate) & " " & Individual.DeathPlace
        End If
    End Sub
End Class