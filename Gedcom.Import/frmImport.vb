Imports Ionic.Zip
Imports System.ComponentModel
Imports System.IO.Compression
Imports System.IO
Imports DC.FileUpload
Imports System.Net
Imports System.Text
Imports System.Drawing.Imaging
Imports Microsoft.SqlServer.Management.Smo
Imports System.Data.SqlClient
Imports log4net

Public Class frmImport

    Private Shared ReadOnly log As ILog = LogManager.GetLogger(GetType(Process))

    Private Sub frmImport_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        log4net.Config.XmlConfigurator.Configure()
        DateTimePicker1.Value = Now.Subtract(New TimeSpan(7, 0, 0, 0, 0))
    End Sub

    Public Const TST_SALTA_CARGA As Boolean = True
    Public Const TST_IMG_DIR As String = "C:\Users\Javier\AppData\Local\Temp\BIR_150613085530"
    Public Const TST_FILENAME As String = "D:\Prog\PAF4\Barrera1.ged"
    Public Const TST_ZIPFILENAME = "C:\Users\Javier\AppData\Local\Temp\BIR_260613050916.ZIP"
    Public Const TST_UPLOADEDFILENAME = "BIR_260613050916.ZIP"

    Private Sub btnFindFile_Click(sender As System.Object, e As System.EventArgs) Handles btnFindFile.Click
        OpenFileDialog1.CheckFileExists = True
        OpenFileDialog1.Multiselect = False
        OpenFileDialog1.Filter = "Archivos Gedcom (*.ged)|*.ged"
        OpenFileDialog1.ShowDialog()
    End Sub

    Private Sub OpenFileDialog1_FileOk(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles OpenFileDialog1.FileOk
        Me.txtFilename.Text = OpenFileDialog1.FileName

    End Sub

    Private _Filename As String
    Public Property Filename() As String
        Get
            Return _Filename
        End Get
        Set(ByVal value As String)
            _Filename = value
        End Set
    End Property

    Private _Phase As String
    Public Property Phase() As String
        Get
            Return _Phase
        End Get
        Set(ByVal value As String)
            _Phase = value
        End Set
    End Property

    Private Function ValidateFile() As Boolean
        Filename = Me.txtFilename.Text
        If Not String.IsNullOrWhiteSpace(Filename) Then
            If System.IO.File.Exists(Filename) Then
                Return True
            Else
                MsgBox("El archivo seleccionado no existe", MsgBoxStyle.Exclamation, "Atención")
            End If
        Else
            MsgBox("Debe seleccionar un archivo", MsgBoxStyle.Information, "Atención")
        End If
        Filename = ""
        Return False
    End Function

    Public Sub import()

        If BackgroundWorker1.IsBusy <> True Then
            If ValidateFile() Then
                BackgroundWorker1.RunWorkerAsync()
            End If
        End If

    End Sub

    Private Sub cancelAsyncButton_Click(ByVal sender As System.Object, _
    ByVal e As System.EventArgs)
        If BackgroundWorker1.WorkerSupportsCancellation = True Then
            ' Cancel the asynchronous operation.
            BackgroundWorker1.CancelAsync()
        End If
    End Sub

    ' This event handler is where the time-consuming work is done. 
    Private Sub backgroundWorker1_DoWork(ByVal sender As System.Object, _
    ByVal e As DoWorkEventArgs) Handles BackgroundWorker1.DoWork
        Dim worker As BackgroundWorker = CType(sender, BackgroundWorker)

        If (worker.CancellationPending = True) Then
            e.Cancel = True
        Else
            ' Perform a time consuming operation and report progress.

            'Process_Upload(TST_ZIPFILENAME)
            'Process_Import(TST_UPLOADEDFILENAME)

            'Process_RestoreDB(backupfilename)
            

            'Dim mediafiles = Process_ConvertMedia()
            'Process_Zip(Filename, mediafiles)
            'Process_LocalDB(Filename)

            'Dim ZipFile As String = Process_Zip(Filename, mediafiles)
            'Dim ShortFileName As String = Process_Upload(ZipFile)
            'Process_Import(ShortFileName)

            'backupDatabase()

            'restoreServerDatabase("D:\Prog\PAF4\gcr.bak")

            '\\ PRUEBA
            ''Dim zipfile As String = TST_ZIPFILENAME
            ''Dim ShortFileName As String = Process_Upload(zipfile) '"BIR_200613054331.ZIP"
            ''Process_Import(ShortFileName)

            ''Process_ExportSql(Filename)


            ''XXXX
            ''Dim fi As New FileInfo(Filename)
            ''Dim rootdir As String = fi.DirectoryName

            ''Dim mediaFiles = Process_ConvertMedia()
            ''Process_LocalDB(Filename)
            ''Dim backupfilename As String = Process_BackupDB()
            ''Dim zipfile As String = Process_Zip(backupfilename, rootdir, mediaFiles)

            ''Dim ShortFileName As String = Process_Upload(zipfile)
            ''Process_Import(ShortFileName)


            '' FINAL
            Dim fi As New FileInfo(Filename)
            Dim rootdir As String = fi.DirectoryName
            Dim mediaFiles = Process_ConvertMedia()
            Dim backupfilename As String = Process_ExportSql(Filename)
            Dim zipfile As String = Process_Zip(backupfilename, rootdir, mediaFiles)
            File.Delete(backupfilename)
            Dim ShortFileName As String = Process_Upload(zipfile)
            File.Delete(zipfile)
            Process_Import(ShortFileName)


            ''Dim ShortFileName As String = Process_Upload(TST_ZIPFILENAME)
            ''Process_Import(ShortFileName)

        End If

    End Sub

    ' This event handler updates the progress. 
    Private Sub backgroundWorker1_ProgressChanged(ByVal sender As System.Object, _
    ByVal e As ProgressChangedEventArgs) Handles BackgroundWorker1.ProgressChanged

        lblInfo.Text = Phase & " " & (e.ProgressPercentage.ToString() + "%")
        ProgressBar1.Visible = True
        ProgressBar1.Value = e.ProgressPercentage

        If Phase.StartsWith("MSGBOX") Then
            Phase = Phase.Substring(7)
            MsgBox(Phase, MsgBoxStyle.Information)
        End If

    End Sub

    ' This event handler deals with the results of the background operation. 
    Private Sub backgroundWorker1_RunWorkerCompleted(ByVal sender As System.Object, _
    ByVal e As RunWorkerCompletedEventArgs) Handles BackgroundWorker1.RunWorkerCompleted
        If e.Cancelled = True Then
            lblInfo.Text = "Cancelado!"
        ElseIf e.Error IsNot Nothing Then
            lblInfo.Text = "Error: " & e.Error.Message
            log.ErrorFormat("Error al importar {0}", e.Error.Message)
            log.ErrorFormat("Detalle del error {0}", e.Error.ToString)
        Else
            lblInfo.Text = "Listo!"
        End If
        ProgressBar1.Visible = False
    End Sub

    Public Class ProcessEventArgs
        Inherits EventArgs
        Public Progress As Integer
        Public Phase As String
        Sub New()
        End Sub
        Sub New(Phase As String, Percentage As Integer)
            Me.Phase = Phase
            Me.Progress = Percentage
        End Sub
    End Class

    Public Event ProcessProgress(source As Object, evt As ProcessEventArgs)

    Public Function Process_ConvertMedia() As List(Of String)


        Dim fi As New FileInfo(Filename)

        Dim imp As New Gedcom.BL.BLImport


        Dim mediafiles As New List(Of String)
        mediafiles.AddRange(GetAllFiles(fi.DirectoryName, "*.jpg"))
        mediafiles.AddRange(GetAllFiles(fi.DirectoryName, "*.gif"))
        mediafiles.AddRange(GetAllFiles(fi.DirectoryName, "*.bmp"))

        Return ConvertMedia(mediafiles)

    End Function

    Public Function ConvertMedia(media As List(Of String)) As List(Of String)

        Dim convertedMedia As New List(Of String)

        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Procesando archivos multimedia", 0))

        Dim destdir As String = ""
        Dim basedir As String = New DirectoryInfo(Filename).FullName
        Dim basedirname As String = New DirectoryInfo(Filename).Name
        Dim rootdir As String = basedir.Substring(0, basedir.Length - basedirname.Length)

        'Dim tempdir As String = System.IO.Path.GetTempPath().TrimEnd("\"c) & "\BIR_" & Now.ToString("ddMMyyhhmmss")
        'System.IO.Directory.CreateDirectory(tempdir)

        Dim Imagenes As New List(Of FileInfo)
        For Each m In media
            Dim auxfilename As String = m
            If File.Exists(auxfilename) Then
                Dim fi As FileInfo = New FileInfo(auxfilename)
                If fi.LastWriteTime > DateTimePicker1.Value Then
                    If Not fi.Name.Contains("BIR_") Then
                        Imagenes.Add(fi)
                    End If
                End If
            End If
        Next

        Dim i As Integer = 0
        For Each aux In Imagenes

            Dim relativedir As String = aux.DirectoryName.Substring(rootdir.Length)
            Dim filename As String = aux.Name


            Dim name As String = filename
            Dim pos As Integer = filename.LastIndexOf("."c)
            If pos >= 0 Then
                name = "BIR_" & filename.Substring(0, pos)
            End If

            destdir = rootdir.TrimEnd("\"c) & "\" & relativedir

            'Dim copydir As String = tempdir & "\" & relativedir
            'System.IO.Directory.CreateDirectory(copydir)

            Dim fn As String = ""
            fn = resizeBitmap(aux.FullName, name, destdir, 120, 120)
            If Not String.IsNullOrWhiteSpace(fn) Then convertedMedia.Add(fn)
            'If Not String.IsNullOrWhiteSpace(fn) Then
            '    Dim fi1 As New FileInfo(fn)
            '    File.Copy(fn, copydir & "\" & fi1.Name, True)
            'End If
            fn = resizeBitmap(aux.FullName, name, destdir, 800, 600)
            If Not String.IsNullOrWhiteSpace(fn) Then convertedMedia.Add(fn)
            'If Not String.IsNullOrWhiteSpace(fn) Then
            '    Dim fi1 As New FileInfo(fn)
            '    File.Copy(fn, copydir & "\" & fi1.Name, True)
            'End If

            i = i + 1
            Dim percentage As Integer = CInt(i / Imagenes.Count * 100)
            RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Procesando archivos multimedia", percentage))

        Next

        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Procesando archivos multimedia", 100))

        Return convertedMedia

    End Function

    Public Function resizeBitmap(filename As String, name As String, destdir As String, w As Integer, h As Integer) As String
        Dim newfilename As String = destdir & "\" & name & "_" & w.ToString("0000") & "_" & h.ToString("0000") & ".jpg"
        If Not File.Exists(newfilename) Then
            Dim bmp As Bitmap = Nothing
            Dim bmp2 As Bitmap = Nothing
            Try
                Dim jgpEncoder As ImageCodecInfo = GetEncoder(ImageFormat.Jpeg)
                bmp = System.Drawing.Bitmap.FromFile(filename)
                bmp2 = Img.proportionalResize(bmp, w, h)
                bmp2.Save(newfilename, jgpEncoder, GetEncoderParams)
            Catch ex As Exception
                '\\ Ignoramos cualquier error en la conversion del archivo
                newfilename = ""
            Finally
                If Not IsNothing(bmp) Then bmp.Dispose()
                If Not IsNothing(bmp2) Then bmp2.Dispose()
            End Try
        End If
        Return newfilename
    End Function

    Private Function GetEncoder(ByVal format As ImageFormat) As ImageCodecInfo

        Dim codecs As ImageCodecInfo() = ImageCodecInfo.GetImageDecoders()

        Dim codec As ImageCodecInfo
        For Each codec In codecs
            If codec.FormatID = format.Guid Then
                Return codec
            End If
        Next codec
        Return Nothing

    End Function

    Private Function GetEncoderParams() As EncoderParameters
        ' Create an Encoder object based on the GUID 
        ' for the Quality parameter category. 
        Dim myEncoder As System.Drawing.Imaging.Encoder = System.Drawing.Imaging.Encoder.Quality

        ' Create an EncoderParameters object. 
        ' An EncoderParameters object has an array of EncoderParameter 
        ' objects. In this case, there is only one 
        ' EncoderParameter object in the array. 
        Dim myEncoderParameters As New EncoderParameters(1)

        Dim myEncoderParameter As New EncoderParameter(myEncoder, 50&)
        myEncoderParameters.Param(0) = myEncoderParameter
        Return myEncoderParameters
    End Function

    Private Sub frmImport_ProcessProgress(source As Object, evt As ProcessEventArgs) Handles Me.ProcessProgress
        Phase = evt.Phase
        BackgroundWorker1.ReportProgress(evt.Progress)
    End Sub

    Private Sub BLImport_GedComReaderProcessProgress(source As Object, evt As EventArgs)
        Dim b As Gedcom.BL.BLImport = source
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Leyendo archivo Gedcom", b.Progress))
    End Sub

    Private Sub BLImport_ImportProcessProgress(source As Object, evt As Gedcom.BL.BLImport.ImportPercentageDoneEventArgs)
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Importando registros", evt.ProgressPrecentage))
    End Sub

    Public Function GetAllFiles(strPath As String, pattern As String, Optional recursive As Boolean = True) As List(Of String)

        Dim filelist As List(Of String) = New List(Of String)
        Dim objRoot As New DirectoryInfo(strPath)

        If objRoot.Exists Then

            Dim so As SearchOption = IIf(recursive, SearchOption.AllDirectories, SearchOption.TopDirectoryOnly)
            For Each objFile In objRoot.GetFiles(pattern, so)
                filelist.Add(objFile.FullName)
            Next

        End If
        Return filelist

    End Function

    Public Function Process_Zip(file As String, rootdir As String, medialist As List(Of String)) As String

        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Comprimiendo archivos", 0))

        Dim sqlfi As New FileInfo(file)
        Dim pos As Integer = sqlfi.Name.LastIndexOf("."c)
        Dim zipName As String = System.IO.Path.GetTempPath().TrimEnd("\"c) & "\" & sqlfi.Name.Substring(0, pos) & ".zip"


        Using zip1 As ZipFile = New ZipFile
            zip1.AddFile(file, "\")

            For Each f In medialist
                Dim fi As New FileInfo(f)
                Dim di As String = fi.DirectoryName.Substring(rootdir.Length)
                Try
                    zip1.AddFile(f, di)
                Catch ex As ArgumentException
                    '\\ por si hay algun archivo repetido
                End Try
            Next

            AddHandler zip1.SaveProgress, _
                   New EventHandler(Of SaveProgressEventArgs)(AddressOf Me.zip1_SaveProgress)
            zip1.Save(zipName)
        End Using

        Return zipName

    End Function

    Const LFL As String = "http://localhost/gcr/FileUpload.ashx"
    Const RFL As String = "http://genealogiachilenaenred.cl/FileUpload.ashx"
    Public Function Process_Upload(uploadfile As String) As String
        If File.Exists(uploadfile) Then

            RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Cargando archivo al servidor", 0))

            Dim f As New FileUpload
            AddHandler f.UploadProgressChanged, AddressOf fileUpload_UploadProgressChanged1


            f.File = New FileInfo(uploadfile)
            f.ChunkSize = "2500000"

            If My.Settings.WebSite.ToUpper.Contains("LOCALHOST") Then
                f.UploadUrl = New System.UriBuilder(LFL).Uri
            Else
                f.UploadUrl = New System.UriBuilder(RFL).Uri
            End If
            f.Upload()

            Debug.WriteLine("WebUpload Uri ='" & f.UploadUrl.ToString & "'")

            While (f.Status = FileUploadStatus.Uploading)
                System.Threading.Thread.Sleep(100)
            End While

            RemoveHandler f.UploadProgressChanged, AddressOf fileUpload_UploadProgressChanged1
        Else
            RaiseEvent ProcessProgress(Me, New ProcessEventArgs("MSGBOX El archivo no existe", 0))
        End If

            Dim fi As New FileInfo(uploadfile)
            Return fi.Name

    End Function

    Private Sub zip1_SaveProgress(ByVal sender As Object, ByVal e As SaveProgressEventArgs)
        Select Case e.EventType
            Case ZipProgressEventType.Saving_AfterWriteEntry
                Me.StepArchiveProgress(e)
        End Select
    End Sub

    Private Sub StepArchiveProgress(ByVal e As SaveProgressEventArgs)
        Dim progress As Integer = (e.EntriesSaved / e.EntriesTotal * 100)
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Comprimiendo archivos", progress))
    End Sub

    Private Sub fileUpload_UploadProgressChanged1(sender As Object, args As DC.FileUpload.UploadProgressChangedEventArgs)
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Cargando archivo al servidor", args.ProgressPercentage))
    End Sub

    Dim Process_Import_Running As Boolean = False

    Public Sub Process_Import(uploadedfilename As String)

        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Procesando archivo en el servidor", 0))

        Dim ub As UriBuilder = New UriBuilder(My.Settings.WebSite.TrimEnd("/c") & "/Process.ashx")
        ub.Query = String.Format("filename={0}", uploadedfilename)

        Dim wc As New WebClient
        AddHandler wc.DownloadStringCompleted, AddressOf Process_Import_Completed
        wc.DownloadStringAsync(ub.Uri)
        Process_Import_Running = True

        While Process_Import_Running

            System.Threading.Thread.Sleep(2000)
            Application.DoEvents()

            Try
                Dim ub2 As UriBuilder = New UriBuilder(My.Settings.WebSite.TrimEnd("/c") & "/Process.ashx")
                ub2.Query = "GetProgress=True"
                Dim wc2 As New WebClient
                Dim aux As String = wc2.DownloadString(ub2.Uri)
                If IsNumeric(aux) Then
                    RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Procesando archivo en el servidor", CInt(aux)))
                    If CInt(aux) = 100 Then
                        Exit While
                    End If
                Else
                    RaiseEvent ProcessProgress(Me, New ProcessEventArgs("MSGBOX Error al recuperar el progreso '" & aux & "'", 0))
                End If
                wc2.Dispose()
            Catch ex As Exception
                log.ErrorFormat("Error ignorado al consultar el progreso {0} - {1}", ex.Message, ex.ToString)
            End Try
        End While

        wc.Dispose()

    End Sub

    Private Sub Process_Import_Completed(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        '  If the string request went as planned and wasn't cancelled:
        If e.Cancelled = True Then
            RaiseEvent ProcessProgress(Me, New ProcessEventArgs("MSGBOX El proceso se ha cancelado", 0))
        End If
        If Not IsNothing(e.Error) Then
            RaiseEvent ProcessProgress(Me, New ProcessEventArgs("MSGBOX El proceso ha terminado con error. '" & e.Error.Message & "'", 0))
        End If
        Process_Import_Running = False

        If Not String.IsNullOrWhiteSpace(e.Result) Then
            RaiseEvent ProcessProgress(Me, New ProcessEventArgs("MSGBOX " & e.Result, 100))
        End If

    End Sub

    Private Sub Process_Import_Progress(ByVal sender As Object, ByVal e As DownloadStringCompletedEventArgs)
        '  If the string request went as planned and wasn't cancelled:
        If e.Cancelled = False AndAlso e.Error Is Nothing Then

        End If
    End Sub



    Public Sub Process_LocalDB(gedcomfile As String)
        Dim blimport As Gedcom.BL.BLImport
        blimport = New Gedcom.BL.BLImport
        Dim connectionstring As String = blimport.CreateSqlExpressDatabase()
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Leyendo archivo de individuos", 0))
        AddHandler blimport.ReadGedComPercentageDone, AddressOf BLImport_GedComReaderProcessProgress
        AddHandler blimport.ImportPercentageDone, AddressOf BLImport_ImportPercentageDone
        blimport.importParallel(gedcomfile, 5, connectionstring)
        RemoveHandler blimport.ReadGedComPercentageDone, AddressOf BLImport_GedComReaderProcessProgress
        RemoveHandler blimport.ImportPercentageDone, AddressOf BLImport_ImportPercentageDone
    End Sub

    Public Function Process_ExportSql(gedcomfile As String) As String
        Dim blimport As Gedcom.BL.BLImport
        blimport = New Gedcom.BL.BLImport
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Leyendo archivo de individuos", 0))
        AddHandler blimport.ReadGedComPercentageDone, AddressOf BLImport_GedComReaderProcessProgress
        AddHandler blimport.ImportPercentageDone, AddressOf BLImport_ImportPercentageDone
        Dim sqlFileName As String = System.IO.Path.GetTempPath().TrimEnd("\"c) & "\BIR_" & Now.ToString("ddMMyyhhmmss") & ".sql"
        blimport.exportToFile(gedcomfile, sqlFileName)
        RemoveHandler blimport.ReadGedComPercentageDone, AddressOf BLImport_GedComReaderProcessProgress
        RemoveHandler blimport.ImportPercentageDone, AddressOf BLImport_ImportPercentageDone
        Return sqlFileName
    End Function



    Private Sub BLImport_ImportPercentageDone(sender As Object, e As Gedcom.BL.BLImport.ImportPercentageDoneEventArgs)
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Importando individuos a la base de datos", e.ProgressPrecentage))
    End Sub

    Private Sub BLImport_ReadGedComPercentageDone(sender As Object, e As System.EventArgs)
        Dim b As Gedcom.BL.BLImport = sender
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Leyendo archivo de individuos", b.Progress))
    End Sub

    Public Function Process_BackupDB() As String

        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Generando script de base de datos", 1))

        Dim tempdir As String = System.IO.Path.GetTempPath().TrimEnd("\"c) & "\BIR_" & Now.ToString("ddMMyyhhmmss")
        System.IO.Directory.CreateDirectory(tempdir)
        Dim filename As String = tempdir & "\gcr.sql"


        Dim connection As SqlConnection = Nothing
        connection = New SqlConnection(Gedcom.Model.GcrContext.getSqlExpressConnectionString())

        Dim sb As StringBuilder = New StringBuilder()

        Dim srv As Server = New Server(New Microsoft.SqlServer.Management.Common.ServerConnection(connection))

        Dim dbs As Database = srv.Databases("gcr")
        dbs.Initialize()

        Dim options As ScriptingOptions = New ScriptingOptions()

        options.ScriptData = True

        'options.ScriptDrops = True

        options.FileName = filename

        options.EnforceScriptingOptions = True

        options.ScriptSchema = True

        options.IncludeHeaders = True

        options.AppendToFile = True

        options.Indexes = True

        options.WithDependencies = False

        Dim tables() As String = New String() {"Individuals", "Families", "Events", "Media"}

        For Each tbl In tables
            Dim script = dbs.Tables(tbl).EnumScript(options)
        Next

        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Generando script de base de datos", 100))

        Return filename

    End Function

    'Public Function backupDatabase() As String
    '    RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Respaldando base de datos", 1))
    '    Dim tempdir As String = System.IO.Path.GetTempPath().TrimEnd("\"c) & "\BIR_" & Now.ToString("ddMMyyhhmmss")
    '    System.IO.Directory.CreateDirectory(tempdir)
    '    Dim aux As String = tempdir & "\gcr.bak"
    '    Dim imp As New Gedcom.BL.BLImport
    '    imp.BackupSqlExpressDatabase(aux)
    '    RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Respaldando base de datos", 100))
    '    Return aux
    'End Function

    'Public Sub restoreServerDatabase(backupfilename As String)
    '    RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Restaurando base de datos", 1))
    '    Dim imp As New Gedcom.BL.BLImport
    '    imp.RestoreServerDatabase(backupfilename)
    '    RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Restaurando base de datos", 100))
    'End Sub

    Public Sub Process_RestoreDB(sqlfilename As String)
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Reconstruyendo base de datos", 1))
        Gedcom.Model.GcrContext.RestoreServerDatabase(sqlfilename, "")
        RaiseEvent ProcessProgress(Me, New ProcessEventArgs("Reconstruyendo base de datos", 100))
    End Sub


    Private Sub btnImportar_Click(sender As System.Object, e As System.EventArgs) Handles btnImportar.Click
        If System.IO.File.Exists(Me.txtFilename.Text) Then
            import()
        Else
            MsgBox("El archivo seleccionado no existe.")
        End If
    End Sub

    
    

End Class
