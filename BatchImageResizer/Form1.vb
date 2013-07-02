Imports System.IO
Imports System.Text.RegularExpressions

Public Class Form1

    Private Sub btnExplore_Click(sender As System.Object, e As System.EventArgs) Handles btnExplore.Click
        FolderBrowserDialog1.ShowNewFolderButton = False
        FolderBrowserDialog1.ShowDialog()
        txtPath.Text = FolderBrowserDialog1.SelectedPath
        doRefresh()
    End Sub

    Dim Imagenes As New List(Of FileInfo)

    Public Sub obtenerImagenes(path As String, Optional level As Integer = 0)

        If level = 0 Then
            Imagenes.Clear()
        Else

            Dim di As New DirectoryInfo(path)
            If (di.Attributes And FileAttributes.Hidden) = FileAttributes.Hidden Then
                Exit Sub
            End If

        End If

        Dim regex As New Regex(".*?_\d+_\d+\.jpg")

        Dim arrI = Directory.GetFiles(path)
        For Each aux In arrI
            Dim fi As New FileInfo(aux)
            Select Case fi.Extension.ToLower.TrimStart("."c)
                Case "jpg", "gif", "bpm", "png"
                    If Not regex.IsMatch(fi.Name) Then
                        Imagenes.Add(fi)
                    End If
            End Select
        Next

        Dim arrP = Directory.GetDirectories(path)
        For Each aux In arrP
            obtenerImagenes(aux, level + 1)
        Next


    End Sub

    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click
        doRefresh()
    End Sub

    Public Sub doRefresh()
        If Directory.Exists(txtPath.Text) Then
            obtenerImagenes(txtPath.Text)
        End If
        lblInfo.Text = String.Format("Se encontraron {0} imágenes.", Imagenes.Count)
    End Sub

    Private Sub btnProcess_Click(sender As System.Object, e As System.EventArgs) Handles btnProcess.Click
        Process()
    End Sub

    Public Sub Process()

        Dim basedir As String = New DirectoryInfo(txtPath.Text).FullName
        Dim basedirname As String = New DirectoryInfo(txtPath.Text).Name
        Dim rootdir As String = basedir.Substring(0, basedir.Length - basedirname.Length)

        Dim tempdir As String = System.IO.Path.GetTempPath().TrimEnd("\"c) & "\BIR_" & Now.ToString("ddMMyyhhmmss")

        System.IO.Directory.CreateDirectory(tempdir)

        ProgressBar1.Value = 0
        ProgressBar1.Minimum = 0
        ProgressBar1.Maximum = Imagenes.Count

        For Each aux In Imagenes

            Dim relativedir As String = aux.DirectoryName.Substring(rootdir.Length)

            Dim filename As String = aux.Name

            Dim name As String = filename
            Dim pos As Integer = filename.LastIndexOf("."c)
            If pos >= 0 Then
                name = "BIR_" & filename.Substring(0, pos)
            End If

            Dim destdir As String = tempdir.TrimEnd("\"c) & "\" & relativedir
            System.IO.Directory.CreateDirectory(destdir)
            resizeBitmap(aux.FullName, name, destdir, 240, 240)
            resizeBitmap(aux.FullName, name, destdir, 800, 600)

            ProgressBar1.Increment(1)
            Application.DoEvents()

        Next

        ProgressBar1.Value = 0
        lblInfo.Text = "Listo"

    End Sub

    Public Sub resizeBitmap(filename As String, name As String, destdir As String, w As Integer, h As Integer)
        Dim newfilename As String = destdir & "\" & name & "_" & w.ToString("0000") & "_" & h.ToString("0000") & ".jpg"
        If Not File.Exists(newfilename) Then
            Dim bmp As Bitmap = System.Drawing.Bitmap.FromFile(filename)
            Dim bmp2 As Bitmap = Img.proportionalResize(bmp, w, h)
            bmp.Dispose()
            bmp2.Save(newfilename, System.Drawing.Imaging.ImageFormat.Jpeg)
            bmp2.Dispose()
        End If
    End Sub

End Class
