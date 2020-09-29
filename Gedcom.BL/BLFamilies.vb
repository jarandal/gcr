Imports Gedcom.Model
Imports System.Data.Objects

Public Class BLFamilies

    Friend Shared Function GetById(ctx As GcrContext, Id As String, Optional so As SearchOptions = Nothing) As Family
        Dim q As ObjectQuery(Of Family) = (From fam In ctx.Families Where fam.Id = Id)
        q = GcrContext.ApplySearchOptions(q, so)
        Return q.FirstOrDefault()
    End Function

    Public Shared Function GetById(Id As String, Optional so As SearchOptions = Nothing) As Family
        Dim ctx As New Gedcom.Model.GcrContext
        Return GetById(ctx, Id, so)
    End Function

    Friend Shared Function GetByIndividualId(ctx As GcrContext, Id As String, Optional so As SearchOptions = Nothing) As List(Of Family)
        Dim q As ObjectQuery(Of Family) = (From fam In ctx.Families Where fam.Husband_Id = Id Or fam.Wife_Id = Id Order By fam.Original_Id Ascending)
        q = GcrContext.ApplySearchOptions(q, so)
        Return q.ToList
    End Function

    Public Shared Function GetByIndividualId(Id As String, Optional so As SearchOptions = Nothing) As List(Of Family)
        Dim ctx As New Gedcom.Model.GcrContext
        Return GetByIndividualId(ctx, Id, so)
    End Function

    Public Shared Sub LoadParentNames(Families As List(Of Family), Optional so As SearchOptions = Nothing)
        Dim ctx As New Gedcom.Model.GcrContext
        For Each f In Families
            Dim auxF As Family = f
            f.HusbandName = (From ind In ctx.CurrentIndividuals(so) Where ind.Id = auxF.Husband_Id Select (ind.FirstName & " " & ind.SurName)).FirstOrDefault
            f.WifeName = (From ind In ctx.CurrentIndividuals(so) Where ind.Id = auxF.Wife_Id Select (ind.FirstName & " " & ind.SurName)).FirstOrDefault
        Next
    End Sub



End Class
