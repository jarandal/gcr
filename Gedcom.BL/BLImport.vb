Imports System.Threading
Imports System.Threading.Tasks
Imports Gedcom.Model
Imports log4net

Public Class BLImport

    Private Shared ReadOnly log As ILog = LogManager.GetLogger(GetType(Process))

    Public Sub createxml(gedcomfile As String, xmlfile As String)

        Dim x As New GedcomParser.GedcomRecordReader

        x.ReadGedcom(gedcomfile)
        Dim gcd As Gedcom.GedcomDatabase = x.Database
        Dim i As Integer

        Dim xml As New System.Xml.XmlDocument
        Dim root = xml.CreateNode("element", "genealogy", "")
        xml.AppendChild(root)
        Dim xi = xml.CreateNode("element", "individuals", "")

        i = 0
        For Each ind In gcd.Individuals
            ind.GenerateXML(xi)
            i = i + 1
            If i = 100 Then Exit For
        Next
        root.AppendChild(xi)

        i = 0
        Dim xf = xml.CreateNode("element", "families", "")
        For Each fam In gcd.Families
            fam.GenerateXML(xf)
            i = i + 1
            If i = 100 Then Exit For
        Next

        root.AppendChild(xf)

        xml.Save(xmlfile)


    End Sub

    Public auxFamilyIndex As New Dictionary(Of String, String)

    Public Sub ClearAux()
        auxFamilyIndex.Clear()
        individualsProcesed = 0
        familiesProcesed = 0
        familiesTotal = 0
        individualsTotal = 0
    End Sub

    ''Public Sub import(gedcomfile As String, Optional connectionstring As String = "")

    ''    Dim ctx As Gedcom.Model.GcrContext

    ''    ctx = getCtx(connectionstring)
    ''    ctx.RebuildDatabase()
    ''    ClearAux()

    ''    x = New GedcomParser.GedcomRecordReader

    ''    x.ReadGedcom(gedcomfile)
    ''    Dim gcd As Gedcom.GedcomDatabase = x.Database
    ''    Dim i As Long

    ''    Dim total As Long = gcd.Individuals.LongCount

    ''    i = 0
    ''    For Each ind In gcd.Individuals
    ''        insertIndividual(ctx, ind)
    ''        i = i + 1
    ''        If (i Mod 100) = 0 Then
    ''            ctx.SaveChanges()
    ''            Progress = CInt(i / total * 100)
    ''            RaiseEvent ImportPercentageDone(Me, System.EventArgs.Empty)
    ''            ctx.Dispose()
    ''            ctx = getCtx(connectionstring)
    ''        End If
    ''    Next

    ''    ctx.SaveChanges()
    ''    ctx.Dispose()

    ''End Sub

    Class Range
        Public start_index As Long
        Public end_index As Long
    End Class

    Public Sub importParallel(gedcomfile As String, Paralellism As Integer, Optional connectionstring As String = "")

        Dim sw As New Stopwatch

        Dim gcd As Gedcom.GedcomDatabase = LeerArchivoGedcom(gedcomfile)
        generarBaseDeDatos(connectionstring)

        log.Info("Inicia la importación en paralelo")

        sw.Start()

        individualsProcesed = 0
        familiesProcesed = 0
        ClearAux()

        individualsTotal = gcd.Individuals.Count
        familiesTotal = gcd.Families.Count
        Dim strInfo As String = ""

        '\\ individuos y familias en paralelo
        strInfo = "Individuos y familias en paralelo"
        Parallel.For(0, 2, _
                     Sub(index)
                         Select Case index
                             Case 0
                                 importFamiliesRange(gcd, 0, gcd.Families.Count - 1, connectionstring)
                             Case 1
                                 importIndividualRange(gcd, 0, gcd.Individuals.Count - 1, connectionstring)
                         End Select
                     End Sub)


        sw.Stop()
        log.InfoFormat("Modo='{0}' Paralelismo={1} Milisegundos={2}", strInfo, Paralellism, sw.ElapsedMilliseconds)

        Dim ctx As Gedcom.Model.GcrContext = getCtx(connectionstring)
        ctx.RebuildIndexes()
        ctx.SaveChanges()
        ctx.Dispose()

        log.Info("Finaliza la importación en paralelo")
    End Sub

    Public Function LeerArchivoGedcom(gedcomfile As String) As Gedcom.GedcomDatabase
        Dim sw As New Stopwatch
        sw.Start()

        log.Info("Inicia la lectura del archivo gedcom")
        x = New GedcomParser.GedcomRecordReader
        x.ReadGedcom(gedcomfile)
        log.InfoFormat("Fin de la lectura del archivo gedcom, Milisegundos={0}", sw.ElapsedMilliseconds)
        Dim gcd As Gedcom.GedcomDatabase = x.Database

        Return gcd
    End Function

    Public Sub generarBaseDeDatos(connectionstring As String)
        Dim sw As New Stopwatch
        sw.Start()

        log.Info("Inicia la creacion de la base de datos")
        Dim ctx As Gedcom.Model.GcrContext
        ctx = getCtx(connectionstring)
        ctx.RebuildDatabase()
        log.InfoFormat("Fin de la creacion de la base de datos, Milisegundos={0}", sw.ElapsedMilliseconds)
        ctx.SaveChanges()
        ctx.Dispose()

    End Sub

    Public Sub exportToFile(gedcomfile As String, filename As String)

        Dim gcd As Gedcom.GedcomDatabase = LeerArchivoGedcom(gedcomfile)

        Dim sw As New Stopwatch
        sw.Start()

        log.Info("Inicia la generacion del archivo sql")

        individualsProcesed = 0
        familiesProcesed = 0

        individualsTotal = gcd.Individuals.Count
        familiesTotal = gcd.Families.Count

        'Dim path As String = (New System.IO.FileInfo(filename)).DirectoryName

        Dim files As New List(Of String)

        Dim indsql As String = filename & ".ind"
        Dim famsql As String = filename & ".fam"

        files.Add(indsql)
        files.Add(famsql)

        Dim writer1 As New System.IO.StreamWriter(famsql, False, System.Text.Encoding.Unicode)
        Dim writer2 As New System.IO.StreamWriter(indsql, False, System.Text.Encoding.Unicode)

        '\\ individuos y familias en paralelo
        Parallel.For(0, 2, _
                     Sub(index)
                         Select Case index
                             Case 0
                                 writer1.WriteLine("-- FAMILIES --")
                                 writer1.WriteLine("GO")
                                 importFamiliesRange(gcd, 0, gcd.Families.Count - 1, "", writer1)
                             Case 1
                                 writer1.WriteLine("-- INDIVIDUALS --")
                                 writer1.WriteLine("GO")
                                 importIndividualRange(gcd, 0, gcd.Individuals.Count - 1, "", writer2)
                         End Select
                     End Sub)
        writer1.Close()
        writer2.Close()

        mergefiles(filename, files)

        If System.IO.File.Exists(indsql) Then System.IO.File.Delete(indsql)
        If System.IO.File.Exists(famsql) Then System.IO.File.Delete(famsql)

        sw.Stop()
        log.Info("Finaliza la generacion del archivo sql")
    End Sub

    Private Sub mergefiles(strDestinationfile As String, files As List(Of String))
        Dim myBuffer(4096) As Byte
        Dim fsdest As System.IO.FileStream = Nothing
        Dim fsSecondFile As System.IO.FileStream = Nothing

        Try
            fsdest = New System.IO.FileStream(strDestinationfile, System.IO.FileMode.Create)

            For Each strFile In files
                Try
                    fsSecondFile = New System.IO.FileStream(strFile, IO.FileMode.Open, IO.FileAccess.Read, IO.FileShare.Read)
                    Dim read As Integer = 0
                    read = fsSecondFile.Read(myBuffer, 0, myBuffer.Length)
                    Do While read > 0
                        fsdest.Write(myBuffer, 0, read)
                        read = fsSecondFile.Read(myBuffer, 0, myBuffer.Length)
                    Loop
                Finally
                    If Not IsNothing(fsSecondFile) Then fsSecondFile.Close()
                End Try
            Next
        Finally
            If Not IsNothing(fsdest) Then fsdest.Close()
        End Try

    End Sub



    Public individualsProcesed As Long = 0
    Public familiesProcesed As Long = 0
    Public individualsTotal As Long = 0
    Public familiesTotal As Long = 0

    Public Sub importIndividualRange(gcd As Gedcom.GedcomDatabase, start_index As Long, end_index As Long, Optional connectionstring As String = "", Optional writer As System.IO.StreamWriter = Nothing)

        Dim ctx As Gedcom.Model.GcrContext = Nothing
        If IsNothing(writer) Then ctx = getCtx(connectionstring)

        Dim i As Long
        Dim Progress As Integer = 0

        For i = start_index To end_index
            Dim ind As Gedcom.GedcomIndividualRecord = gcd.Individuals(i)
            Dim ind1 = insertIndividual(ctx, ind)
            If Not IsNothing(writer) Then writer.WriteLine(ind1.getInsertSql)
            individualsProcesed = individualsProcesed + 1
            If (i Mod 100) = 0 Then
                If Not IsNothing(ctx) Then ctx.SaveChanges()
                'Progress = 50 + CInt(individualsProcesed / individualsTotal * 100)
                Progress = CInt((individualsProcesed + familiesProcesed) / (individualsTotal + familiesTotal) * 100)
                RaiseEvent ImportPercentageDone(Me, New ImportPercentageDoneEventArgs(Progress))
                If Not IsNothing(ctx) Then ctx.Dispose()
                If Not IsNothing(ctx) Then ctx = getCtx(connectionstring)
            End If
        Next

        Progress = CInt((individualsProcesed + familiesProcesed) / (individualsTotal + familiesTotal) * 100)
        RaiseEvent ImportPercentageDone(Me, New ImportPercentageDoneEventArgs(Progress))
        If Not IsNothing(ctx) Then ctx.SaveChanges()
        If Not IsNothing(ctx) Then ctx.Dispose()

    End Sub

    Public Sub importFamiliesRange(gcd As Gedcom.GedcomDatabase, start_index As Long, end_index As Long, Optional connectionstring As String = "", Optional writer As System.IO.StreamWriter = Nothing)

        Dim ctx As Gedcom.Model.GcrContext = Nothing
        If IsNothing(writer) Then ctx = getCtx(connectionstring)

        Dim i As Long
        Dim Progress As Integer = 0

        For i = start_index To end_index
            Dim fam As Gedcom.GedcomFamilyRecord = gcd.Families(i)
            Dim fam1 = insertFamily(ctx, fam)
            If Not IsNothing(writer) Then writer.WriteLine(fam1.getInsertSql)
            familiesProcesed = familiesProcesed + 1
            If (i Mod 100) = 0 Then
                If Not IsNothing(ctx) Then ctx.SaveChanges()
                Progress = CInt((individualsProcesed + familiesProcesed) / (individualsTotal + familiesTotal) * 100)
                RaiseEvent ImportPercentageDone(Me, New ImportPercentageDoneEventArgs(Progress))
                If Not IsNothing(ctx) Then ctx.Dispose()
                If Not IsNothing(ctx) Then ctx = getCtx(connectionstring)
            End If
        Next

        Progress = CInt((individualsProcesed + familiesProcesed) / (individualsTotal + familiesTotal) * 100)
        RaiseEvent ImportPercentageDone(Me, New ImportPercentageDoneEventArgs(Progress))
        If Not IsNothing(ctx) Then ctx.SaveChanges()
        If Not IsNothing(ctx) Then ctx.Dispose()

    End Sub


    Private Function getCtx(Optional connectionstring As String = "") As Gedcom.Model.GcrContext
        Dim ctx As Gedcom.Model.GcrContext
        If Not String.IsNullOrWhiteSpace(connectionstring) Then
            ctx = New Gedcom.Model.GcrContext(connectionstring)
        Else
            ctx = New Gedcom.Model.GcrContext()
        End If
        Return ctx
    End Function

    Public Function insertIndividual(ctx As GcrContext, individual As Gedcom.GedcomIndividualRecord) As Individual

        Dim ind As New Individual

        ind.Id = individual.XRefID
        ind.Original_Id = individual.OriginalRefID

        Dim preferedName = (From n1 In individual.Names Where n1.PreferedName = True Select n1).FirstOrDefault
        If Not IsNothing(preferedName) Then
            ind.FirstName = preferedName.Given.Trim
            If preferedName.SurnamePrefix.Trim.Length > 0 Then
                ind.SurName = preferedName.SurnamePrefix.Trim & " " & preferedName.Surname.Trim
            Else
                ind.SurName = preferedName.Surname.Trim
            End If
        End If
        ind.Sex = individual.SexChar
        ind.Notes = getNotes(individual.Database, individual.Notes)
        ind.NotesSummary = ind.Notes

        If Not IsNothing(individual.Death) Then
            If Not IsNothing(individual.Death.Date) Then ind.DeathDate = individual.Death.Date.DateTime1
            If Not IsNothing(individual.Death.Place) Then ind.DeathPlace = individual.Death.Place.Name
        End If

        If Not IsNothing(individual.Birth) Then
            If Not IsNothing(individual.Birth.Date) Then ind.BirthDate = individual.Birth.Date.DateTime1
            If Not IsNothing(individual.Birth.Place) Then ind.BirthPlace = individual.Birth.Place.Name
        End If

        ind.Dead = individual.Dead

        If ind.NotesSummary.Length > 2000 Then ind.NotesSummary = ind.NotesSummary.Substring(0, 2000)

        Dim childIn = individual.ChildIn()
        If childIn.Count > 0 Then
            If Not String.IsNullOrWhiteSpace(childIn(0).Family) Then
                Dim fx = (From x In individual.Database.Families Where x.XRefID = childIn(0).Family Select x).FirstOrDefault()
                If Not IsNothing(fx) Then
                    ind.Family_Id = fx.XRefID
                End If
            End If
        End If


        For Each MMindex In individual.Multimedia
            Dim auxMMIndex As String = MMindex
            Dim media = individual.Database.Media.Where(Function(x) x.XRefID = auxMMIndex).FirstOrDefault
            If Not IsNothing(media) Then
                Dim med As New Media
                med.Title = media.Title
                med.Notes = getNotes(individual.Database, media.Notes)
                med.Filename = media.Files(0).Filename
                med.Individual = ind
                ind.Media.Add(med)
            End If

        Next

        If Not IsNothing(individual.Events) Then
            For Each evt In individual.Events
                Dim auxevt As New [Event]
                If Not IsNothing(evt.Date) Then auxevt.Date = evt.Date.DateTime1
                If Not IsNothing(evt.Place) Then auxevt.Place = evt.Place.Name
                auxevt.Type = evt.EventType
                auxevt.Individual = ind
                ind.Events.Add(auxevt)
            Next
        End If

        If Not IsNothing(ctx) Then ctx.Individuals.AddObject(ind)

        Return ind

    End Function

    Public Function insertFamily(ctx As GcrContext, family As Gedcom.GedcomFamilyRecord) As Family

        Dim fam As Family = Nothing

        If auxFamilyIndex.ContainsKey(family.XRefID) Then
            Return Nothing
        Else
            fam = New Family
            fam.Id = family.XRefID
            fam.Husband_Id = family.Husband
            fam.Wife_Id = family.Wife
            fam.Notes = getNotes(family.Database, family.Notes)
            fam.NotesSummary = fam.Notes
            If Not IsNothing(family.Marriage) Then
                If Not IsNothing(family.Marriage.Date) Then fam.Date = family.Marriage.Date.DateTime1
            End If
            If fam.NotesSummary.Length > 2000 Then fam.NotesSummary = fam.NotesSummary.Substring(0, 2000)
            If Not IsNothing(ctx) Then ctx.Families.AddObject(fam)
            auxFamilyIndex.Add(fam.Id, fam.Id)
        End If

        Return fam
    End Function

    Public Function getNotes(db As Gedcom.GedcomDatabase, Notes As GedcomRecordList(Of String)) As String
        Dim aux As String = ""
        For Each n1 In Notes
            Dim noteId As String = n1
            Dim note1 = db.Notes.FirstOrDefault(Function(x) x.XRefID = noteId)
            If Not IsNothing(note1) Then aux += note1.Text & vbCrLf
        Next
        Return aux
    End Function

    WithEvents x As GedcomParser.GedcomRecordReader

    Public Function getMediaList(gedcomfile As String) As List(Of String)
        Dim auxList As New List(Of String)
        x = New GedcomParser.GedcomRecordReader
        x.ReadGedcom(gedcomfile)
        Dim gcd As Gedcom.GedcomDatabase = x.Database
        For Each m In gcd.Media
            For Each f In m.Files
                auxList.Add(f.Filename)
            Next
        Next
        Return auxList
    End Function

    Public Event ReadGedComPercentageDone(sender As Object, e As System.EventArgs)

    Private Sub x_PercentageDone(sender As Object, e As System.EventArgs) Handles x.PercentageDone
        Progress = x.Progress
        RaiseEvent ReadGedComPercentageDone(Me, System.EventArgs.Empty)
    End Sub

    Private _Progress As Integer
    Public Property Progress() As Integer
        Get
            Return _Progress
        End Get
        Set(ByVal value As Integer)
            _Progress = value
        End Set
    End Property

    Public Sub rebuildDatabase()
        Dim ctx As New Gedcom.Model.GcrContext
        ctx.RebuildDatabase()
    End Sub

    Public Event ImportPercentageDone(sender As Object, e As ImportPercentageDoneEventArgs)

    Public Class ImportPercentageDoneEventArgs
        Inherits EventArgs
        Public ProgressPrecentage As Integer = 0
        Sub New()
        End Sub
        Sub New(ProgressPrecentage As Integer)
            Me.ProgressPrecentage = ProgressPrecentage
        End Sub
    End Class

    Public Function CreateSqlExpressDatabase() As String
        Return Gedcom.Model.GcrContext.CreateSqlExpressDatabase()
    End Function

    'Public Sub BackupSqlExpressDatabase(filename As String)
    '    Gedcom.Model.GcrContext.BackupSqlExpressDatabase(filename)
    'End Sub

    'Public Sub RestoreServerDatabase(filename As String)
    '    Gedcom.Model.GcrContext.RestoreServerDatabase(filename)
    'End Sub

End Class
