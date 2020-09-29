Imports System.IO
Imports Gedcom.Model
Imports System.Data.Objects
Imports System.Net.Mail
Imports System.Net.Mime
Imports System.Net
Imports log4net
Imports System.Text

Public Class BLRequest

    Private Shared ReadOnly log As ILog = LogManager.GetLogger(GetType(Process))

    Friend Shared Sub Add(ctx As GcrContext, request As Model.Request)
        ctx.Request.AddObject(request)
    End Sub

    Public Shared Function Add(request As Model.Request) As Long
        Dim ctx As New Gedcom.Model.GcrContext
        Add(ctx, request)
        ctx.SaveChanges()
        ctx.Detach(request)
        Return request.Id
    End Function

    Public Shared Sub SendEmail(request As Model.Request, files As List(Of String))

        ' Specify the file to be attached and sent.
        ' This example assumes that a file named Data.xls exists in the
        ' current working directory.
        ' Create a message and set up the recipients.
        Dim ind As Individual = Nothing
        If Not String.IsNullOrEmpty(request.Original_Id) Then
            ind = BLIndividuals.GetByOriginalId(request.Original_Id)
        End If

        Dim sb As New StringBuilder
        sb.AppendLine("Tipo de requerimiento : " & request.Reason)
        sb.AppendLine("Nombre del Solicitante : " & request.Name)
        sb.AppendLine("Teléfono del Solicitante : " & request.Phone)
        sb.AppendLine("Email del Solicitante : " & request.Email)
        sb.AppendLine("Fecha de Solicitud : " & request.Date)
        sb.AppendLine("Indice en PAF4 :" & request.Original_Id)
        If Not IsNothing(ind) Then
            sb.AppendLine("Nombre del individuo : " & ind.FullName)
        End If
        sb.AppendLine("Dirección IP del Solicitante: " & request.IP)
        sb.AppendLine()
        sb.AppendLine("Texto de la solicitud:")
        sb.AppendLine("---------------------------------------------")
        sb.AppendLine(request.Text)
        sb.AppendLine("---------------------------------------------")
        sb.AppendLine()
        If Not String.IsNullOrEmpty(request.Attachments) Then
            sb.AppendLine("Archivos Adjuntos: " & request.Attachments)
        End If

        Dim message As New MailMessage(request.Email, My.Settings.EmailTo, request.Reason & " - " & request.Name, sb.ToString)

        Dim attachements As New List(Of Attachment)

        If Not IsNothing(files) Then
            For Each File In files
                ' Create  the file attachment for this e-mail message.
                Dim data As New Attachment(File, MediaTypeNames.Application.Octet)
                ' Add time stamp information for the file.
                Dim disposition As ContentDisposition
                disposition = data.ContentDisposition
                disposition.CreationDate = IO.File.GetCreationTime(File)
                disposition.ModificationDate = IO.File.GetLastWriteTime(File)
                disposition.ReadDate = IO.File.GetLastAccessTime(File)
                ' Add the file attachment to this e-mail message.
                message.Attachments.Add(data)
                attachements.Add(data)
            Next
        End If

        'Send the message.
        Dim client As New SmtpClient(My.Settings.SmtpServer, My.Settings.SmtpPort)
        ' Add credentials if the SMTP server requires them.
        client.Credentials = New NetworkCredential(My.Settings.SmtpUser, My.Settings.SmtpPassword)

        Try
            client.Timeout = 600000 '600 segundos
            log.ErrorFormat("Email sent from {0}", request.Email)
            client.Send(message)
        Catch ex As Exception
            log.ErrorFormat("Error in SendMail: {0} - {1}", ex.Message, ex.ToString())
            '\\ en caso de error intenta enviar sin adjuntos
            If Not IsNothing(files) Then
                log.ErrorFormat("Retry without atachments")
                request.Attachments = "No fue posible adjuntar los archivos : " & request.Attachments
                SendEmail(request, Nothing)
            End If
        Finally
            For Each aux In attachements
                aux.Dispose()
            Next
        End Try

    End Sub


End Class
