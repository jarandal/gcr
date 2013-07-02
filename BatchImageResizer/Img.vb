Imports System
Imports System.ComponentModel
Imports System.ComponentModel.Design
Imports System.Drawing
Imports System.Drawing.Drawing2D
Imports System.Drawing.Design
Imports System.Windows.Forms
Imports System.Diagnostics
Imports System.Drawing.Imaging


Public Class Img

    Public Shared Function resizeImage(ByVal imgPhoto As Image, ByVal Width As Integer, ByVal Height As Integer, ByVal format As PixelFormat, ByVal xdpi As Single, ByVal ydpi As Single) As Image
        Dim sourceWidth As Integer = imgPhoto.Width
        Dim sourceHeight As Integer = imgPhoto.Height

        Dim bmPhoto As Bitmap = New Bitmap(Width, Height, format)
        bmPhoto.SetResolution(xdpi, ydpi)

        Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        grPhoto.Clear(Color.White)

        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic

        grPhoto.DrawImage(imgPhoto, _
           New Rectangle(0, 0, Width, Height), _
           New Rectangle(0, 0, sourceWidth, sourceHeight), _
           GraphicsUnit.Pixel)

        grPhoto.Dispose()

        Return bmPhoto
    End Function

    Public Shared Function resizeImage(ByVal imgPhoto As Image, ByVal Width As Integer, ByVal Height As Integer, ByVal xdpi As Single, ByVal ydpi As Single) As Image
        Dim sourceWidth As Integer = imgPhoto.Width
        Dim sourceHeight As Integer = imgPhoto.Height

        Dim bmPhoto As Bitmap = New Bitmap(Width, Height)
        bmPhoto.SetResolution(xdpi, ydpi)

        Dim grPhoto As Graphics = Graphics.FromImage(bmPhoto)
        grPhoto.Clear(Color.White)

        grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic

        grPhoto.DrawImage(imgPhoto, _
           New Rectangle(0, 0, Width, Height), _
           New Rectangle(0, 0, sourceWidth, sourceHeight), _
           GraphicsUnit.Pixel)

        grPhoto.Dispose()

        Return bmPhoto
    End Function

    Public Shared Function resizeImage(ByVal imgPhoto As Image, ByVal Width As Integer, ByVal Height As Integer, ByVal format As PixelFormat) As Image
        Return resizeImage(imgPhoto, Width, Height, format, imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)
    End Function

    Public Shared Function resizeImage(ByVal imgPhoto As Image, ByVal Width As Integer, ByVal Height As Integer) As Image
        Return resizeImage(imgPhoto, Width, Height, PixelFormat.Format24bppRgb, imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)
    End Function

    Public Shared Function Crop(ByVal imgPhoto As Image, ByVal srcRect As Rectangle) As Image

        Dim grPhoto As Graphics = Nothing
        Dim bmp As Bitmap = Nothing
        Try
            bmp = New Bitmap(srcRect.Width, srcRect.Height, PixelFormat.Format24bppRgb)
            bmp.SetResolution(imgPhoto.HorizontalResolution, imgPhoto.VerticalResolution)

            grphoto = Graphics.FromImage(bmp)
            grPhoto.Clear(Color.White)
            grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic

            Dim destRect As Rectangle = New Rectangle(0, 0, srcRect.Width, srcRect.Height)

            grPhoto.DrawImage(imgPhoto, _
                destRect, _
                srcRect, _
                GraphicsUnit.Pixel)

            grPhoto.Dispose()

            Return bmp
        Catch ex As Exception
            If Not IsNothing(grPhoto) Then grPhoto.Dispose()
            If Not IsNothing(bmp) Then bmp.Dispose()
            Return Nothing
        End Try

    End Function


    Public Shared Function proportionalResize(ByVal src As Bitmap, ByVal MaxWidth As Integer, ByVal MaxHeight As Integer) As Bitmap

        Dim w As Integer = src.Width
        Dim h As Integer = src.Height


        ' Longest and shortest dimension
        Dim longestDimension As Integer = IIf(w > h, w, h)
        Dim shortestDimension As Integer = IIf(w < h, w, h)
        ' propotionality
        Dim factor As Decimal = longestDimension / shortestDimension

        ' default width is greater than height
        Dim newWidth As Decimal = MaxWidth
        Dim newHeight As Decimal = MaxWidth / factor

        ' if height greater than width recalculate
        If (w < h) Then
            newWidth = MaxHeight / factor
            newHeight = MaxHeight
        End If
    
        ' Create new Bitmap at new dimensions
        Dim result As Bitmap = New Bitmap(CInt(newWidth), CInt(newHeight))
        Dim g As Graphics = Graphics.FromImage(CType(result, Image))
        g.DrawImage(src, 0, 0, CInt(newWidth), CInt(newHeight))

        Return result

    End Function



End Class
