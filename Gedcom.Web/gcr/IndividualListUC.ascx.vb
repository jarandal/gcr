Imports Gedcom.Model
Public Class IndividualListUC
    Inherits System.Web.UI.UserControl

    Private _Individuals As List(Of Individual)
    Public Property Individuals() As List(Of Individual)
        Get
            Return _Individuals
        End Get
        Set(ByVal value As List(Of Individual))
            _Individuals = value
        End Set
    End Property


    Private Sub Page_PreRender(sender As Object, e As System.EventArgs) Handles Me.PreRender
        If Not IsNothing(Individuals) Then
            gvIndividuals.DataSource = Individuals
            gvIndividuals.DataBind()
        End If
    End Sub

End Class