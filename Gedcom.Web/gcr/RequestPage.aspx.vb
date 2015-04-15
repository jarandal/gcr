Imports log4net
Imports System.IO

Public Class RequestPage
    Inherits System.Web.UI.Page

    Private Shared ReadOnly log As ILog = LogManager.GetLogger(GetType(Process))

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        OriginalId = ""

        trCaptcha.Visible = False

        If String.IsNullOrEmpty(OriginalId) Then
            trIndividual.Visible = False
        End If

    End Sub

    Public Property OriginalId() As String
        Get
            Return ViewState("OriginalId")
        End Get
        Set(ByVal value As String)
            ViewState("OriginalId") = value
        End Set
    End Property

    Protected Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click

        Dim captchaIsValid As Boolean = False

        If trCaptcha.Visible Then
            Try
                CaptchaControl1.ValidateCaptcha(txtCaptcha.Text)
                If (CaptchaControl1.UserValidated) Then
                    captchaIsValid = True
                End If
            Catch ex As Exception
                log.ErrorFormat("Error ignorado al validar el captcha {0} {1}", ex.Message, ex.ToString)
            End Try
        Else
            captchaIsValid = True
        End If

        If captchaIsValid Then

            Dim fu As FileUpload = Nothing
            Dim filenames As String = ""
            Dim files As New List(Of String)

            Dim funame As String = "FileUpload"
            Dim i As Integer
            For i = 1 To 100
                fu = FileUploadContainer.FindControl(funame & i)
                If Not IsNothing(fu) Then
                    Dim uploadPath As String = Context.Server.MapPath("~/Upload").TrimEnd("\"c) & "\Attachments\"
                    If Not Directory.Exists(uploadPath) Then Directory.CreateDirectory(uploadPath)
                    Dim tempfile As String = uploadPath & fu.FileName
                    If Not String.IsNullOrEmpty(fu.FileName) Then
                        fu.SaveAs(tempfile)
                        files.Add(tempfile)
                        filenames = filenames + " " & tempfile
                    End If
                End If
            Next


            Me.pnlOK.Visible = False
            Me.pnlError.Visible = False

            Dim x As Model.Request = Nothing

            Try
                x = New Model.Request
                x.IP = Request.UserHostAddress
                x.Name = Me.txtName.Text
                x.Reason = Me.ddlReason.SelectedItem.Text
                x.Text = Me.txtText.Text
                x.Date = Now
                x.Email = Me.txtEmail.Text
                x.Phone = Me.txtPhoneNumber.Text
                x.Original_Id = "I1"
                x.Attachments = filenames
                x.AttachmentsQty = files.Count

                BL.BLRequest.Add(x)

                xRequest = x
                xFiles = files

                Dim t As New System.Threading.Thread(AddressOf sendmail)
                t.Start()

                Me.pnlOK.Visible = True
                trCaptcha.Visible = False

            Catch ex As Exception
                log.ErrorFormat("Error al enviar requerimiento {0} {1}", ex.Message, ex.ToString)
                pnlError.Visible = True
            End Try

            If pnlOK.Visible = True Then
                Me.btnEnviar.Visible = False
            End If
        End If

    End Sub

    Dim xRequest As Model.Request = Nothing
    Dim xFiles As List(Of String)

    Public Sub sendmail()
        Try
            BL.BLRequest.SendEmail(xRequest, xFiles)
        Catch ex As Exception
            log.ErrorFormat("Error al enviar el requerimiento via mail {0} {1}", ex.Message, ex.ToString)
        End Try
    End Sub

End Class