Imports System.IO
Imports ICSharpCode.SharpZipLib.Core
Imports ICSharpCode.SharpZipLib.Zip


Public Class Zip

    Public Shared Sub ExtractZipFile(archiveFilenameIn As String, outFolder As String, Optional password As String = "")
        Dim zf As ZipFile = Nothing
        Try
            Dim fs As FileStream = File.OpenRead(archiveFilenameIn)
            zf = New ZipFile(fs)
            If Not [String].IsNullOrEmpty(password) Then    ' AES encrypted entries are handled automatically
                zf.Password = password
            End If
            For Each zipEntry As ZipEntry In zf
                Try
                    If Not zipEntry.IsFile Then     ' Ignore directories
                        Continue For
                    End If
                    Dim entryFileName As [String] = zipEntry.Name
                    ' to remove the folder from the entry:- entryFileName = Path.GetFileName(entryFileName);
                    ' Optionally match entrynames against a selection list here to skip as desired.
                    ' The unpacked length is available in the zipEntry.Size property.

                    Dim buffer As Byte() = New Byte(4095) {}    ' 4K is optimum
                    Dim zipStream As Stream = zf.GetInputStream(zipEntry)

                    ' Manipulate the output filename here as desired.
                    Dim fullZipToPath As [String] = Path.Combine(outFolder, entryFileName)
                    Dim directoryName As String = Path.GetDirectoryName(fullZipToPath)
                    If directoryName.Length > 0 Then
                        If Not Directory.Exists(directoryName) Then
                            Directory.CreateDirectory(directoryName)
                        End If
                    End If

                    Dim uncompress As Boolean = True
                    If File.Exists(fullZipToPath) Then
                        Dim fi As New FileInfo(fullZipToPath)
                        If fi.Length = zipEntry.Size Then
                            uncompress = False
                        End If
                    End If

                    ' Unzip file in buffered chunks. This is just as fast as unpacking to a buffer the full size
                    ' of the file, but does not waste memory.
                    ' The "Using" will close the stream even if an exception occurs.
                    If uncompress Then
                        Using streamWriter As FileStream = File.Create(fullZipToPath)
                            StreamUtils.Copy(zipStream, streamWriter, buffer)
                        End Using
                    End If
                Catch ex As Exception
                    '\\ ignora cualquier error al descomprimir los archivos en forma individual
                End Try
            Next
        Finally
            If zf IsNot Nothing Then
                zf.IsStreamOwner = True     ' Makes close also shut the underlying stream
                ' Ensure we release resources
                zf.Close()
            End If
        End Try
    End Sub
End Class
