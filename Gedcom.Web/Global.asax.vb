Imports System.Reflection
Imports System.Diagnostics
Imports System.Web.SessionState
Imports log4net

Public Class Global_asax
    Inherits System.Web.HttpApplication

    Private Shared ReadOnly log As ILog = LogManager.GetLogger(GetType(Process))

    Sub Application_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application is started
        log4net.Config.XmlConfigurator.Configure()
        log.Info("GCR iniciado")
    End Sub

    Sub Session_Start(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session is started
    End Sub

    Sub Application_BeginRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires at the beginning of each request
        If Not IsNothing(Context) Then
            If Not Context.Request.RawUrl.ToUpper.Contains("WEBRESOURCE") Then
                log.InfoFormat("Página visitada: {0} desde {1}", Context.Request.RawUrl, Context.Request.UserHostAddress)
            End If
        End If

    End Sub

    Sub Application_AuthenticateRequest(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires upon attempting to authenticate the use
    End Sub

    Sub Application_Error(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when an error occurs
    End Sub

    Sub Session_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the session ends
    End Sub

    Sub Application_End(ByVal sender As Object, ByVal e As EventArgs)
        ' Fires when the application ends
        log.Info("GCR detenido")

        Dim runtime As HttpRuntime = GetType(System.Web.HttpRuntime).InvokeMember("_theRuntime", _
               BindingFlags.NonPublic Or BindingFlags.Static Or BindingFlags.GetField, _
               Nothing, Nothing, Nothing)

        If Not IsNothing(runtime) Then
            Dim shutDownMessage As String = runtime.GetType().InvokeMember("_shutDownMessage",
                        BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField, _
                        Nothing, _
                        runtime, _
                        Nothing)

            Dim shutDownStack As String = runtime.GetType().InvokeMember("_shutDownStack", _
                BindingFlags.NonPublic Or BindingFlags.Instance Or BindingFlags.GetField, _
                Nothing, _
                runtime, _
                Nothing)

            log.InfoFormat("Motivo {0}", shutDownMessage)
            log.InfoFormat("Pila {0}", shutDownStack)

        End If

    End Sub

End Class