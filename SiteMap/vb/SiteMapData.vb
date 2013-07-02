Imports System
Imports System.Runtime.CompilerServices

<Serializable> _
Public Class SiteMapData
    ' Properties
    Public Property Changefreq As String
        Get
        Set(ByVal value As String)
    End Property
    Public Property Lastmod As Nullable(Of DateTime)
        Get
        Set(ByVal value As Nullable(Of DateTime))
    End Property
    Public Property Loc As String
        Get
        Set(ByVal value As String)
    End Property
    Public Property Priority As Nullable(Of Decimal)
        Get
        Set(ByVal value As Nullable(Of Decimal))
    End Property
End Class


