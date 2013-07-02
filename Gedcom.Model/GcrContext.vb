﻿Imports System.Data.Objects
Imports System.Data.EntityClient
Imports System.Data.SqlClient
Imports System.Text

Public Class GcrContext
    Inherits ModelContainer

    Public Sub New()
        MyBase.New()
    End Sub

    Public Sub New(ByVal connectionString As String)
        MyBase.New(connectionString)
    End Sub

    Public Sub New(ByVal connection As EntityConnection)
        MyBase.New(connection)
    End Sub

    Public Shared Function Include(Of obj)(query As ObjectQuery(Of obj), includes As String) As ObjectQuery(Of obj)
        Dim q As ObjectQuery(Of obj) = query
        If includes.Length > 0 Then
            Dim inc() As String = includes.Split(","c)
            For i = 0 To inc.Length - 1
                If inc(i).Length > 0 Then
                    q = q.Include(inc(i).Trim)
                End If
            Next
        End If
        Return q
    End Function

    Public Shared Function ApplySearchOptions(Of obj)(query As ObjectQuery(Of obj), so As SearchOptions) As ObjectQuery(Of obj)
        Dim q As ObjectQuery(Of obj) = query
        If Not IsNothing(so) Then
            If Not String.IsNullOrWhiteSpace(so.Includes) Then q = GcrContext.Include(q, so.Includes)
            If so.Max > 0 Then q = q.Take(so.Max)
        End If
        Return q
    End Function

    Private Shared Function getScript(scriptresource As String) As String
        Dim s As System.IO.Stream = Nothing
        Dim sr As System.IO.StreamReader = Nothing
        Dim sql As String = ""
        Try
            s = System.Reflection.Assembly.GetExecutingAssembly.GetManifestResourceStream(scriptresource)
            sr = New System.IO.StreamReader(s)
            sql = sr.ReadToEnd
        Finally
            If Not IsNothing(sr) Then sr.Close()
            If Not IsNothing(s) Then s.Close()
        End Try
        Return sql
    End Function

    Public Shared Function getDatabaseScript()
        Return getScript("Gedcom.Model.Model.sql")
    End Function

    Public Shared Function getDropScript()
        Return getScript("Gedcom.Model.Model.Drop.sql")
    End Function

    Public Shared Function getIndexScript()
        Return getScript("Gedcom.Model.Model.Index.sql")
    End Function

    Public Sub RebuildDatabase()
        Me.ExecuteStoreCommand(getDatabaseScript)
    End Sub

    Public Sub RebuildIndexes()
        Me.ExecuteStoreCommand(getIndexScript)
    End Sub

    Public Shared Function getSqlExpressFilename() As String
        Dim tempdir As String = System.IO.Path.GetTempPath().TrimEnd("\"c)
        Dim filename As String = tempdir + "\gcr.mdf"
        Return filename
    End Function

    Public Shared Function getSqlExpressConnectionString() As String
        Return "Data Source=.\sqlexpress;AttachDbFilename=" & getSqlExpressFilename() & ";Initial Catalog=gcr;Integrated Security=True;User Instance=True;"
    End Function

    Public Shared Function getServerConnectionString() As String
        Return System.Configuration.ConfigurationManager.ConnectionStrings("ServerConnection").ConnectionString
    End Function

    Public Shared Function CreateSqlExpressDatabase() As String

        Dim filename As String = getSqlExpressFilename()

        Dim databaseName As String = "gcr"
        Dim connection As SqlConnection = Nothing
        Dim command As SqlCommand = Nothing

        Try
            connection = New SqlConnection("Data Source=.\sqlexpress;Initial Catalog=master;Integrated Security=true;User Instance=True;User Instance=True;")
            connection.Open()

            command = connection.CreateCommand()

            command.CommandText = "if exists(select * from sys.databases where name = '" & databaseName & "') ALTER DATABASE " & databaseName & " SET OFFLINE WITH ROLLBACK IMMEDIATE"
            command.ExecuteNonQuery()

            command.CommandText = "if exists(select * from sys.databases where name = '" & databaseName & "') DROP DATABASE " + databaseName
            command.ExecuteNonQuery()

            command.CommandText = "if exists(select * from sys.databases where name = '" & databaseName & "') EXEC sp_detach_db '" + databaseName + "', 'true'"
            command.ExecuteNonQuery()

            If System.IO.File.Exists(filename) Then
                System.IO.File.Delete(filename)
            End If

            Dim logfilename As String = filename.Replace(".mdf", "_log.ldf")
            If System.IO.File.Exists(logfilename) Then
                System.IO.File.Delete(logfilename)
            End If

            command.CommandText =
                   "CREATE DATABASE " + databaseName + _
                   " ON PRIMARY (NAME=" + databaseName + _
                   ", FILENAME='" + filename + "')"

            command.ExecuteNonQuery()

        Finally
            command.Dispose()
            connection.Close()
        End Try
        Dim c As String = "metadata=res://*/Model.csdl|res://*/Model.ssdl|res://*/Model.msl;provider=System.Data.SqlClient;provider connection string=""" & getSqlExpressConnectionString() & """"
        Return c
    End Function

    'Public Shared Sub BackupSqlExpressDatabase(filename As String)

    '    Dim databaseName As String = "gcr"
    '    Dim connection As SqlConnection = Nothing
    '    Dim command As SqlCommand = Nothing

    '    Try
    '        If System.IO.File.Exists(filename) Then
    '            System.IO.File.Delete(filename)
    '        End If

    '        connection = New SqlConnection("Data Source=.\sqlexpress;Initial Catalog=master;Integrated Security=true;User Instance=True;")
    '        connection.Open()

    '        command = connection.CreateCommand()

    '        command.CommandText = "backup database " & databaseName & " to disk='" & filename & "'"
    '        command.ExecuteNonQuery()

    '    Finally
    '        command.Dispose()
    '        connection.Close()
    '    End Try
    'End Sub


    Public Shared Sub RestoreServerDatabase(filename As String, processingFile As String)

        Dim databaseName As String = "gcr_db"
        Dim connection As SqlConnection = Nothing
        Dim command As SqlCommand = Nothing


        Try
            If System.IO.File.Exists(filename) = True Then

                Dim progress As Integer = 0

                connection = New SqlConnection(getServerConnectionString)
                connection.Open()

                command = connection.CreateCommand()

                command.CommandText = getDatabaseScript()
                command.ExecuteNonQuery()

                command.CommandText = "USE [" + databaseName + "]"
                command.ExecuteNonQuery()


                Dim objReader As System.IO.StreamReader = Nothing
                Try
                    objReader = New System.IO.StreamReader(filename)
                    Dim commandText As String = ""
                    Do While objReader.Peek() <> -1
                        Try
                            Dim aux As String = objReader.ReadLine()
                            If aux.Trim = "GO" Then
                                command.CommandText = commandText
                                command.ExecuteNonQuery()
                                commandText = ""
                            Else
                                commandText = commandText & vbCrLf & aux
                            End If

                            If processingFile.Length > 0 Then
                                Dim new_progress As Integer = CInt(objReader.BaseStream.Position / objReader.BaseStream.Length * 100)
                                If progress <> new_progress And progress < 100 Then
                                    '\\ nunca graba el 100
                                    progress = new_progress
                                    System.IO.File.WriteAllText(processingFile, progress)
                                End If
                            End If

                        Catch ex As Exception
                            Debug.Write(ex.Message & " " & commandText)
                            commandText = ""
                        End Try
                    Loop
                Finally
                    If Not IsNothing(objReader) Then objReader.Close()
                End Try

                command.CommandText = getIndexScript()
                command.ExecuteNonQuery()

                command.CommandText = "if exists(select * from sys.databases where name = '" & databaseName & "') DBCC SHRINKDATABASE ('" & databaseName & "' , 0);"
                command.ExecuteNonQuery()

            End If

        Finally
            If Not IsNothing(command) Then command.Dispose()
            If Not IsNothing(connection) Then connection.Close()
            If System.IO.File.Exists(filename) Then System.IO.File.Delete(filename)
        End Try


    End Sub

    Public Overrides Function SaveChanges(options As System.Data.Objects.SaveOptions) As Integer
        'Dim retry As Boolean = False
        'Try
        Return MyBase.SaveChanges(options)

        'Catch ex As Exception
        '    If TypeOf ex Is System.Data.UpdateException Then
        '        If Not IsNothing(ex.InnerException) Then
        '            If TypeOf (ex.InnerException) Is System.Data.SqlClient.SqlException Then
        '                If ex.Message.ToLower.Contains("deadlock") Then
        '                    retry = True
        '                End If
        '            End If
        '        End If
        '    End If

        '    If retry Then
        '        System.Threading.Thread.Sleep(500)
        '        Return MyBase.SaveChanges(options)
        '    Else
        '        Throw
        '    End If

        'End Try

    End Function

End Class
