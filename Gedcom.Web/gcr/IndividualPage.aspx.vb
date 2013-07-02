Imports Gedcom.BL
Imports Gedcom.Model

Public Class IndividualPage
    Inherits System.Web.UI.Page

    Public Property XRefID() As String
        Get
            Return ViewState("XRefID")
        End Get
        Set(ByVal value As String)
            ViewState("XRefID") = value
        End Set
    End Property


    Public Property Original_ID() As String
        Get
            Return ViewState("Original_ID")
        End Get
        Set(ByVal value As String)
            ViewState("Original_ID") = value
        End Set
    End Property

    Public Property FullName() As String
        Get
            Return ViewState("FullName")
        End Get
        Set(ByVal value As String)
            ViewState("FullName") = value
        End Set
    End Property


    Dim individual As Individual = Nothing
    Dim family As Family = Nothing
    Dim families As List(Of Family) = Nothing

    Public Class MediaImg

        Private _thumbnail As String
        Public Property thumbnail() As String
            Get
                Return _thumbnail
            End Get
            Set(ByVal value As String)
                _thumbnail = value
            End Set
        End Property

        Private _image As String
        Public Property image() As String
            Get
                Return _image
            End Get
            Set(ByVal value As String)
                _image = value
            End Set
        End Property

        Private _note As String
        Public Property note() As String
            Get
                Return _note
            End Get
            Set(ByVal value As String)
                _note = value
            End Set
        End Property


    End Class

    Public Property MediaCount() As Integer
        Get
            Return ViewState("MediaCount")
        End Get
        Set(ByVal value As Integer)
            ViewState("MediaCount") = value
        End Set
    End Property

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ClientScript.GetPostBackEventReference(Me, String.Empty)

        If Not Page.IsPostBack Then
            If Not String.IsNullOrWhiteSpace(Request.QueryString("ID")) Then
                Original_ID = Request.QueryString("ID")
            Else
                Original_ID = ""
            End If

            If Not String.IsNullOrWhiteSpace(Original_ID) Then
                individual = BLIndividuals.GetByOriginalId(Original_ID, New SearchOptions(Includes:="Family,Media"))
                If Not IsNothing(individual) Then
                    XRefID = individual.Id

                    IndividualUC.Individual = individual

                    FullName = individual.FullName

                    Dim mlist As New System.ComponentModel.BindingList(Of MediaImg)

                    MediaCount = individual.Media.Count

                    If MediaCount > 0 Then

                        For Each im In individual.Media

                            Dim filename As String = im.Filename
                            filename = filename.Substring(0, filename.Length - 4)

                            Dim m As New MediaImg

                            filename = filename.Replace("\", "/")
                            Dim pos As Integer = filename.LastIndexOf("/")
                            If pos < 0 Then
                                m.thumbnail = "BIR_" & filename & "_0120_0120" & ".jpg"
                                m.image = "BIR_" & filename & "_0800_0600" & ".jpg"
                            Else
                                m.thumbnail = filename.Substring(0, pos) & "/BIR_" & filename.Substring(pos + 1) & "_0120_0120" & ".jpg"
                                m.image = filename.Substring(0, pos) & "/BIR_" & filename.Substring(pos + 1) & "_0800_0600" & ".jpg"
                            End If

                            m.note = im.Notes

                            mlist.Add(m)

                        Next
                    Else
                        gallery.Visible = False
                    End If


                    Repeater1.DataSource = mlist
                    Repeater1.DataBind()

                    If Not IsNothing(individual.Family) Then
                        family = individual.Family
                        FatherUC.Individual = BLIndividuals.GetById(family.Husband_Id)
                        MotherUC.Individual = BLIndividuals.GetById(family.Wife_Id)
                        If Not IsNothing(FatherUC.Individual) OrElse Not IsNothing(MotherUC.Individual) Then
                            pnlParents.Visible = True
                        End If
                    End If

                    families = BLFamilies.GetByIndividualId(XRefID, New SearchOptions(Includes:="Childrens"))
                    BLFamilies.LoadParentNames(families)

                    If Not IsNothing(families) AndAlso families.Count > 0 Then

                        pnlFamilies.Visible = True
                        pnlNoFamilies.Visible = False
                        ddlFamilies.DataSource = families

                        If families(0).Husband_Id = XRefID Then
                            '\\ Es el esposo
                            ddlFamilies.DataTextField = "WifeName"
                        Else
                            '\\ es la esposa
                            ddlFamilies.DataTextField = "HusbandName"
                        End If

                        ddlFamilies.DataValueField = "Id"
                        ddlFamilies.DataBind()

                        ddlFamilies.SelectedIndex = 0

                        If families.Count = 1 Then
                            ddlFamilies.Visible = False
                        Else
                            ddlFamilies.Visible = True
                        End If

                        RefreshChildrens()

                    Else
                        pnlFamilies.Visible = False
                        pnlNoFamilies.Visible = True
                    End If

                    If individual.Notes.Replace(vbCrLf, "").Trim.Length = 0 Then
                        lnkNotes.Visible = False
                    Else
                        Me.txtNotes.Text = individual.Notes
                    End If

                End If
            End If
        End If

    End Sub

    Public Sub RefreshChildrens()

        If Not String.IsNullOrWhiteSpace(ddlFamilies.SelectedValue) Then
            If IsNothing(families) Then families = BLFamilies.GetByIndividualId(XRefID, New SearchOptions(Includes:="Childrens"))
            Dim family As Family = families.FirstOrDefault(Function(x) x.Id = ddlFamilies.SelectedValue)
            If Not IsNothing(family) Then

                If family.Husband_Id = XRefID Then
                    '\\ Es el esposo
                    SpouseUC.Individual = BLIndividuals.GetById(family.Wife_Id)
                Else
                    '\\ es la esposa
                    SpouseUC.Individual = BLIndividuals.GetById(family.Husband_Id)
                End If


                If Not IsNothing(family.Childrens) Then
                    If family.Childrens.Count > 0 Then
                        pnlChildrens.Visible = True
                        pnlNoChildrens.Visible = False
                        ChildrenList.Individuals = family.Childrens.OrderBy(Function(x) x.BirthDate).ToList
                    Else
                        pnlChildrens.Visible = False
                        pnlNoChildrens.Visible = True
                    End If
                End If

                If String.IsNullOrWhiteSpace(family.Notes) Then
                    lnkFamilyNotes.Visible = False
                Else
                    lnkFamilyNotes.Visible = True
                    Me.txtFamilyNotes.Text = family.Notes
                End If


            End If
        End If

    End Sub

    Protected Sub ddlFamilies_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ddlFamilies.SelectedIndexChanged
        RefreshChildrens()
    End Sub
End Class