Imports System.IO
Imports System.Web
Imports System.Web.Services
Imports Ionic.Zip

Imports log4net

Public Class Process
    Implements System.Web.IHttpHandler

    Private Shared ReadOnly log As ILog = LogManager.GetLogger(GetType(Process))

    Sub ProcessRequest(ByVal context As HttpContext) Implements IHttpHandler.ProcessRequest

        Try

            Dim getProgress As Boolean = False
            If Not String.IsNullOrEmpty(context.Request.QueryString("GetProgress")) Then
                getProgress = Boolean.Parse(context.Request.QueryString("GetProgress"))
            End If

            Dim processSQL As Boolean = True
            If Not String.IsNullOrEmpty(context.Request.QueryString("processSQL")) Then
                processSQL = Boolean.Parse(context.Request.QueryString("processSQL"))
            End If

            Dim uploadPath As String = context.Server.MapPath("~/Upload").TrimEnd("\"c)
            Dim processingFile As String = uploadPath & "\Processing.txt"

            If Not getProgress Then

                Dim filename As String = context.Request.QueryString("filename")
                Dim gcrPath As String = context.Server.MapPath("~/gcr")

                If File.Exists(processingFile) Then
                    Dim fi As New FileInfo(processingFile)
                    Dim ts As TimeSpan = Now.Subtract(fi.LastWriteTime)
                    If ts.TotalMinutes > 10 Then
                        File.Delete(processingFile)
                    End If
                End If

                If Not File.Exists(uploadPath & "\Processing.txt") Then
                    context.Response.ContentType = "text/plain"
                    Dim zipfilename As String = uploadPath & "\" & filename
                    If File.Exists(zipfilename) Then
                        System.IO.File.WriteAllText(processingFile, "0")
                        Try
                            Dim files = Zip.ExtractZipFile(zipfilename, gcrPath)
                            Dim sqlfiles = files.Where(Function(x) x.Contains(".sql_")).ToList()

                            If processSQL Then
                                Dim b As New Gedcom.BL.BLImport
                                Gedcom.Model.GcrContext.RestoreServerDatabaseParalel(sqlfiles, processingFile)
                            End If

                            Dim sitemapindexfile As String = Gedcom.BL.BLIndividuals.GenerateSiteMapIndex(uploadPath, My.Settings.WebSite)

                            '\\ graba el 100% en el archivo de progreso
                            System.IO.File.WriteAllText(processingFile, "100")

                        Finally
                            System.IO.File.Delete(processingFile)
                            If File.Exists(zipfilename) Then File.Delete(zipfilename)
                        End Try
                    Else
                        Throw New ApplicationException("El archivo NO existe!")
                    End If
                Else
                    Throw New ApplicationException("Ya hay un proceso en ejecución, espere por favor.")
                End If
            Else
                Dim progress As Integer = 0
                If File.Exists(processingFile) Then
                    progress = CInt(File.ReadAllText(processingFile))
                Else
                    progress = 100
                End If
                context.Response.Write(progress)
            End If
        Catch ex2 As ApplicationException
            context.Response.Write(ex2.Message)
        Catch ex As Exception
            log.ErrorFormat("{0}", ex.ToString)
            context.Response.Write("ERROR: " & ex.Message)
        End Try

    End Sub

    ReadOnly Property IsReusable() As Boolean Implements IHttpHandler.IsReusable
        Get
            Return False
        End Get
    End Property

    Public Function FindGed(path) As String
        Dim files() As String = Directory.GetFiles(path, "*.ged")
        If files.Count > 0 Then
            Return files(0)
        Else
            Throw New Exception("No se encontró el archivo gedcom")
        End If
    End Function

    Public Function FindSql(zipfilename As String, path As String) As String

        Dim pos As Integer = zipfilename.LastIndexOf("."c)

        Dim fi As New FileInfo(path.TrimEnd("\"c) & "\" & zipfilename.Substring(0, pos) & ".sql")

        If fi.Exists Then
            Return fi.FullName
        Else
            Throw New Exception("No se encontró el archivo sql")
        End If
    End Function
End Class