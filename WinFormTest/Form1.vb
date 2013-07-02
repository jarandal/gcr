Public Class Form1

    Private Sub Form1_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Button1_Click(sender As System.Object, e As System.EventArgs) Handles Button1.Click
        Dim imp As New Gedcom.BL.BLImport
        imp.createxml("D:\Prog\Gedcom\test.ged", "D:\Prog\Gedcom.net\temp\xmltest2.xml")
    End Sub


    Private Sub btnImportarGedcom_Click(sender As System.Object, e As System.EventArgs) Handles btnImportarGedcom.Click
        'Dim imp As New Gedcom.BL.BLImport
        'imp.import("D:\Prog\Gedcom\barrera1.ged")
        'imp.import("D:\Prog\Gedcom\test.ged")

    End Sub
End Class
