Imports System.IO
Imports DC.FileUpload
Public Class Form2

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim f As New FileUpload
        f.File = New FileInfo("C:\Users\Javier\AppData\Local\Temp\BIR_150613093757.zip")
        f.ChunkSize = "2500000"
        f.UploadUrl = New System.Uri("http://localhost:4807/FileUpload.ashx")
        f.Upload()
    End Sub
End Class