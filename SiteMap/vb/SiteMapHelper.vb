Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.IO.Compression
Imports System.Xml.Linq

Public Class SiteMapHelper
    ' Methods
    Private Function CreateSiteMapUrlNode(ByVal data As SiteMapData) As XElement
        Dim node As New XElement(DirectCast((Me._xmlns + "url"), XName), New XElement(DirectCast((Me._xmlns + "loc"), XName), data.Loc))
        If data.Lastmod.HasValue Then
            node.Add(New XElement(DirectCast((Me._xmlns + "lastmod"), XName), data.Lastmod.Value.ToString("yyyy-MM-dd")))
        End If
        If (Not data.Changefreq Is Nothing) Then
            node.Add(New XElement(DirectCast((Me._xmlns + "changefreq"), XName), data.Changefreq))
        End If
        If data.Priority.HasValue Then
            node.Add(New XElement(DirectCast((Me._xmlns + "priority"), XName), data.Priority.ToString))
        End If
        Return node
    End Function

    Public Function GenerateSiteMap(ByVal dataRows As List(Of SiteMapData)) As XDocument
        Dim xmlNodes As IEnumerable(Of XElement) = (From x In dataRows Select Me.CreateSiteMapUrlNode(x))
        Return New XDocument(New XDeclaration("1.0", "utf-8", "yes"), New Object() { New XElement(DirectCast((Me._xmlns + "urlset"), XName), New Object() { New XAttribute(DirectCast((XNamespace.Xmlns + "xsd"), XName), "http://www.w3.org/2001/XMLSchema"), New XAttribute(DirectCast((XNamespace.Xmlns + "xsi"), XName), "http://www.w3.org/2001/XMLSchema-instance"), xmlNodes }) })
    End Function

    Public Function GZipSiteMap(ByVal siteMap As XDocument) As MemoryStream
        Dim CS$1$0000 As MemoryStream
        Using ms As MemoryStream = New MemoryStream
            siteMap.Save(ms)
            ms.Seek(0, SeekOrigin.Begin)
            Using compressed As MemoryStream = New MemoryStream
                Using zip As GZipStream = New GZipStream(compressed, CompressionMode.Compress)
                    ms.CopyTo(zip, CInt(ms.Length))
                End Using
                CS$1$0000 = New MemoryStream(compressed.ToArray)
            End Using
        End Using
        Return CS$1$0000
    End Function


    ' Fields
    Private _xmlns As XNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9"
End Class


