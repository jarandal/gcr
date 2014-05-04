Imports Gedcom.BL
Imports Gedcom.Model

Public Class NotesPage
    Inherits System.Web.UI.Page

    Public Property Original_ID() As String
        Get
            Return ViewState("ID")
        End Get
        Set(ByVal value As String)
            ViewState("ID") = value
        End Set
    End Property

    Public Property Kind() As String
        Get
            Return ViewState("KIND")
        End Get
        Set(ByVal value As String)
            ViewState("KIND") = value
        End Set
    End Property

    Dim individual As Individual = Nothing
    Dim family As Family = Nothing


    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        If Not String.IsNullOrWhiteSpace(Request.QueryString("ID")) Then
            Original_ID = Request.QueryString("ID")
        End If

        If Not String.IsNullOrWhiteSpace(Request.QueryString("KIND")) Then
            Kind = Request.QueryString("KIND")
        End If

        Select Case Kind
            Case "FAM"
                family = BLFamilies.GetById(Original_ID)
                Me.txtNotes.Text = family.Notes
            Case "IND"
                individual = BLIndividuals.GetByOriginalId(Original_ID, New SearchOptions(IsAdmin:=Util.IsAdmin))
                Me.txtNotes.Text = individual.Notes
        End Select
    End Sub

End Class