Imports System
Imports System.Runtime.CompilerServices

'http://www.nathanfox.net/blog/72/NET-code-to-help-with-XML-sitemap-generation
Namespace SiteMap
    <Serializable()> _
    Public Class SiteMapData
        ' Properties

        Public Sub New()
        End Sub

        Public Sub New(Loc As String, priority As Decimal)
            Me.Loc = Loc
            Me.Priority = priority
            Me.Changefreq = "Monthly"
        End Sub

        Private _changefreq As String
        Public Property Changefreq() As String
            Get
                Return _changefreq
            End Get
            Set(ByVal value As String)
                _changefreq = value
            End Set
        End Property

        Private _lastmod As Nullable(Of DateTime)
        Public Property LastMod() As Nullable(Of DateTime)
            Get
                Return _lastmod
            End Get
            Set(ByVal value As Nullable(Of DateTime))
                _lastmod = value
            End Set
        End Property

        Private _Loc As String
        Public Property Loc() As String
            Get
                Return _Loc
            End Get
            Set(ByVal value As String)
                _Loc = value
            End Set
        End Property

        Private _priority As Nullable(Of Decimal)
        Public Property Priority() As Nullable(Of Decimal)
            Get
                Return _priority
            End Get
            Set(ByVal value As Nullable(Of Decimal))
                _priority = value
            End Set
        End Property

    End Class

End Namespace

