using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml.Linq;

    [Serializable]
    public class SiteMapData
    {
        public string Loc { get; set; }
        public DateTime? Lastmod { get; set; }
        public string Changefreq { get; set; }
        public decimal? Priority { get; set; }
    }

    public class SiteMapHelper
    {
        XNamespace _xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        public XDocument GenerateSiteMap(List<SiteMapData> dataRows)
        {
            var xmlNodes =
               (from x in dataRows
                select CreateSiteMapUrlNode(x));

            XDocument siteMap = new XDocument(
               new XDeclaration("1.0", "utf-8", "yes"),
               new XElement(_xmlns + "urlset",
                  new XAttribute(XNamespace.Xmlns + "xsd", "http://www.w3.org/2001/XMLSchema"),
                  new XAttribute(XNamespace.Xmlns + "xsi", "http://www.w3.org/2001/XMLSchema-instance"), xmlNodes));

            return siteMap;
        }

        public MemoryStream GZipSiteMap(XDocument siteMap)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                siteMap.Save(ms);
                ms.Seek(0, SeekOrigin.Begin);
                using (MemoryStream compressed = new MemoryStream())
                {
                    // The zip stream has to be closed to write out all the bytes. The underlying stream
                    // is closed as well. To get the bytes of the the closed stream ToArray must be called.
                    using (GZipStream zip = new GZipStream(compressed, System.IO.Compression.CompressionMode.Compress))
                    {
                        ms.CopyTo(zip, (int)ms.Length);
                    }

                    return new MemoryStream(compressed.ToArray());
                }
            }
        }

        private XElement CreateSiteMapUrlNode(SiteMapData data)
        {
            XElement node = new XElement(_xmlns + "url", new XElement(_xmlns + "loc", data.Loc));

            if (data.Lastmod.HasValue)
            {
                node.Add(new XElement(_xmlns + "lastmod", data.Lastmod.Value.ToString("yyyy-MM-dd")));
            }

            if (data.Changefreq != null)
            {
                node.Add(new XElement(_xmlns + "changefreq", data.Changefreq));
            }

            if (data.Priority.HasValue)
            {
                node.Add(new XElement(_xmlns + "priority", data.Priority.ToString()));
            }

            return node;
        }
    }
