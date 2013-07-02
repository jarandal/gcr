Imports System
Imports System.Runtime.CompilerServices

'http://www.nathanfox.net/blog/72/NET-code-to-help-with-XML-sitemap-generation
Namespace SiteMap
    <Serializable()> _
    Public Class SiteMapIndex
        ' Properties

        Public Sub New()
        End Sub

        Public Sub New(Loc As String, lastmod As Nullable(Of Date))
            Me.Loc = Loc
            Me.LastMod = lastmod
        End Sub

        
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

    End Class

End Namespace

