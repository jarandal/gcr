Imports System.IO
Imports Gedcom.Model
Imports System.Data.Objects

Public Class BLIndividuals

    Friend Shared Function SearchByName(ctx As GcrContext, firstName As String, surName As String, Optional so As SearchOptions = Nothing) As List(Of Individual)
        Dim q As ObjectQuery(Of Individual) = (From ind In ctx.Individuals)
        If Not String.IsNullOrWhiteSpace(firstName) Then q = q.Where(Function(x) x.FirstName.Contains(firstName))
        If Not String.IsNullOrWhiteSpace(surName) Then q = q.Where(Function(x) x.SurName.Contains(surName))
        q = GcrContext.ApplySearchOptions(q, so)
        Return q.ToList()
    End Function

    Public Shared Function SearchByName(firstName As String, surName As String, Optional so As SearchOptions = Nothing) As List(Of Individual)
        Dim ctx As New Gedcom.Model.GcrContext
        Return SearchByName(ctx, firstName, surName, so)
    End Function

    Friend Shared Function GetById(ctx As GcrContext, Id As String, Optional so As SearchOptions = Nothing) As Individual
        Dim q As ObjectQuery(Of Individual) = (From ind In ctx.Individuals Where ind.Id = Id)
        q = GcrContext.ApplySearchOptions(q, so)
        Return q.FirstOrDefault()
    End Function

    Friend Shared Function GetByOriginalId(ctx As GcrContext, Original_Id As String, Optional so As SearchOptions = Nothing) As Individual
        Dim q As ObjectQuery(Of Individual) = (From ind In ctx.Individuals Where ind.Original_Id = Original_Id)
        q = GcrContext.ApplySearchOptions(q, so)
        Return q.FirstOrDefault()
    End Function

    Public Shared Function GetById(Id As String, Optional so As SearchOptions = Nothing) As Individual
        Dim ctx As New Gedcom.Model.GcrContext
        Return GetById(ctx, Id, so)
    End Function

    Public Shared Function GetByOriginalId(Original_Id As String, Optional so As SearchOptions = Nothing) As Individual
        Dim ctx As New Gedcom.Model.GcrContext
        Return GetByOriginalId(ctx, Original_Id, so)
    End Function

    Public Shared Function GenerateSiteMapIndex(uploadpath As String, websiteuri As String) As String

        Dim ctx As New Gedcom.Model.GcrContext
        Dim q As ObjectQuery(Of String) = (From ind In ctx.Individuals Select ind.Original_Id)

        Dim RANGE As Integer = 10000
        Dim count As Long = q.Count

        Dim sitemaps As New ArrayList

        Dim list As New List(Of SiteMap.SiteMapData)
        list.Add(New SiteMap.SiteMapData(websiteuri.TrimEnd("/"c), 1D))
        list.Add(New SiteMap.SiteMapData(websiteuri.TrimEnd("/"c) & "/default.aspx", 0.9D))
        list.Add(New SiteMap.SiteMapData(websiteuri.TrimEnd("/"c) & "/gcr/SearchPage.aspx", 0.8D))

        Dim lastsaved As Long = 0

        For i = 0 To count Step RANGE

            Dim start_range As Long = i
            Dim auxStrArray = q.OrderBy(Function(x) x).Skip(start_range).Take(RANGE - 1).ToArray
            Dim auxstr As String = ""

            For Each auxstr In auxStrArray
                Dim x As New SiteMap.SiteMapData
                x.Loc = websiteuri.TrimEnd("/"c) & "/gcr/IndividualPage.aspx?ID=" & auxstr
                x.Changefreq = "monthly"
                list.Add(x)
            Next

            sitemaps.Add(list)
            list = New List(Of SiteMap.SiteMapData)
        Next

        Dim sitemapindexlist As New List(Of SiteMap.SiteMapIndex)

        Dim filename As String = ""
        Dim fullfilename As String = ""
        Dim url As String = ""

        Dim j As Integer = 0
        For Each auxl In sitemaps
            j = j + 1

            filename = "sitemap_" & j.ToString("000") & ".xml.gz"
            fullfilename = uploadpath.TrimEnd("\"c) & "\" & filename
            url = websiteuri.TrimEnd("/"c) & "/Upload/" & filename

            compressAndSaveSitemap(fullfilename, auxl)
            sitemapindexlist.Add(New SiteMap.SiteMapIndex(url, Now))

        Next

        filename = "sitemapindex.xml.gz"
        fullfilename = uploadpath.TrimEnd("\"c) & "\" & filename
        compressAndSaveSitemapIndex(fullfilename, sitemapindexlist)

        Return fullfilename

    End Function

    Shared Sub compressAndSaveSitemap(filename As String, list As List(Of SiteMap.SiteMapData))
        Dim smx As XDocument = SiteMap.SiteMapHelper.GenerateSiteMap(list)
        Dim compressedBytes As System.IO.MemoryStream = SiteMap.SiteMapHelper.GZipSiteMap(smx)
        File.WriteAllBytes(filename, compressedBytes.ToArray)
    End Sub

    Shared Sub compressAndSaveSitemapIndex(filename As String, list As List(Of SiteMap.SiteMapIndex))
        Dim smx As XDocument = SiteMap.SiteMapHelper.GenerateSiteIndex(list)
        Dim compressedBytes As System.IO.MemoryStream = SiteMap.SiteMapHelper.GZipSiteMap(smx)
        File.WriteAllBytes(filename, compressedBytes.ToArray)
    End Sub


End Class
