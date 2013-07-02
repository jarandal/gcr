Imports System
Imports System.Collections.Generic
Imports System.IO
Imports System.IO.Compression
Imports System.Xml.Linq
Imports System.Xml
Imports System.Text

'http://www.nathanfox.net/blog/72/NET-code-to-help-with-XML-sitemap-generation
Namespace SiteMap

    Public Class SiteMapHelper
        ' Methods
        Private Shared Function CreateSiteMapUrlNode(ByVal data As SiteMapData) As XElement
            Dim node As New XElement(DirectCast((_xmlns + "url"), XName), New XElement(DirectCast((_xmlns + "loc"), XName), data.Loc))
            If data.LastMod.HasValue Then
                node.Add(New XElement(DirectCast((_xmlns + "lastmod"), XName), data.LastMod.Value.ToString("yyyy-MM-dd")))
            End If
            If (Not data.Changefreq Is Nothing) Then
                node.Add(New XElement(DirectCast((_xmlns + "changefreq"), XName), data.Changefreq))
            End If
            If data.Priority.HasValue Then
                node.Add(New XElement(DirectCast((_xmlns + "priority"), XName), data.Priority.Value.ToString(System.Globalization.CultureInfo.InvariantCulture)))
            End If
            Return node
        End Function

        Private Shared Function CreateSiteMapIndexNode(ByVal data As SiteMapIndex) As XElement
            Dim node As New XElement(DirectCast((_xmlns + "sitemap"), XName), New XElement(DirectCast((_xmlns + "loc"), XName), data.Loc))
            If data.LastMod.HasValue Then
                node.Add(New XElement(DirectCast((_xmlns + "lastmod"), XName), data.LastMod.Value.ToString("yyyy-MM-dd")))
            End If
            Return node
        End Function


        Public Shared Function GenerateSiteMap(ByVal dataRows As List(Of SiteMapData)) As XDocument
            Dim xmlNodes As IEnumerable(Of XElement) = (From x In dataRows Select CreateSiteMapUrlNode(x))
            Return New XDocument(New XDeclaration("1.0", "utf-8", "yes"), New Object() {New XElement(DirectCast((_xmlns + "urlset"), XName), New Object() {New XAttribute(DirectCast((XNamespace.Xmlns + "xsd"), XName), "http://www.w3.org/2001/XMLSchema"), New XAttribute(DirectCast((XNamespace.Xmlns + "xsi"), XName), "http://www.w3.org/2001/XMLSchema-instance"), xmlNodes})})
        End Function

        Public Shared Function GZipSiteMap(ByVal siteMap As XDocument) As MemoryStream
            Dim aux As MemoryStream
            Using ms As MemoryStream = New MemoryStream

                Dim settings As XmlWriterSettings = New XmlWriterSettings()
                settings.Encoding = Encoding.UTF8
                settings.ConformanceLevel = ConformanceLevel.Document
                settings.Indent = True

                Using xw As XmlWriter = XmlTextWriter.Create(ms, settings)
                    siteMap.Save(xw)
                    xw.Flush()
                End Using

                ms.Seek(0, SeekOrigin.Begin)
                Using compressed As MemoryStream = New MemoryStream
                    Using zip As GZipStream = New GZipStream(compressed, CompressionMode.Compress)
                        ms.CopyTo(zip, CInt(ms.Length))
                    End Using
                    aux = New MemoryStream(compressed.ToArray)
                End Using
            End Using
            Return aux
        End Function

        Public Shared Function GenerateSiteIndex(ByVal dataRows As List(Of SiteMapIndex)) As XDocument
            Dim xmlNodes As IEnumerable(Of XElement) = (From x In dataRows Select CreateSiteMapIndexNode(x))
            Return New XDocument(New XDeclaration("1.0", "utf-8", "yes"), New Object() {New XElement(DirectCast((_xmlns + "sitemapindex"), XName), New Object() {New XAttribute(DirectCast((XNamespace.Xmlns + "xsd"), XName), "http://www.w3.org/2001/XMLSchema"), New XAttribute(DirectCast((XNamespace.Xmlns + "xsi"), XName), "http://www.w3.org/2001/XMLSchema-instance"), xmlNodes})})
        End Function

        ' Fields
        Private Shared _xmlns As XNamespace = "http://www.sitemaps.org/schemas/sitemap/0.9"
    End Class
End Namespace

