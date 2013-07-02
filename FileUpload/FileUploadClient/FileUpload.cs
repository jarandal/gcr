using System;
using System.Net;
using System.IO;
using System.Threading;
using System.ComponentModel;
using System.Text;


namespace DC.FileUpload
{
    public enum FileUploadStatus
    {
        Pending,
        Uploading,
        Complete,
        Error,
        Canceled,
        Removed,
        Resizing
    }

    public class FileUpload 
    {
        public event ProgressChangedEvent UploadProgressChanged;
        //public event EventHandler StatusChanged;

        public long ChunkSize = 4194304;

        public Uri UploadUrl { get; set; }
        private FileInfo file;
        public FileInfo File
        {
            get { return file; }
            set
            {
                file = value;
                Stream temp = file.OpenRead();
                FileLength = temp.Length;
                temp.Close();
            }
        }
        public string Name { get { return File.Name; } }
        private long fileLength;
        public long FileLength
        {
            get { return fileLength; }
            set
            {
                fileLength = value;

            }
        }

        private MemoryStream resizeStream = null;
        public int ImageSize { get; set; }
        
        private long bytesUploaded;
        public long BytesUploaded
        {
            get { return bytesUploaded; }
            set
            {
                bytesUploaded = value;

            }
        }

        private int uploadPercent;
        public int UploadPercent
        {
            get { return uploadPercent; }
            set
            {
                uploadPercent = value;

            }
        }

        private FileUploadStatus status;
        public FileUploadStatus Status 
        {
            get { return status; }
            set
            {
                status = value;
                
            }
        }
               
        private bool cancel;
        private bool remove;

        private bool displayThumbnail;
        public bool DisplayThumbnail
        {
            get { return displayThumbnail; }
            set
            {
                displayThumbnail = value;

            }
        }

        public FileUpload()
        {            
            Status = FileUploadStatus.Pending;
        }

        public FileUpload(Uri uploadUrl)
            : this()
        {
            UploadUrl = uploadUrl;
        }

        public FileUpload(Uri uploadUrl, FileInfo fileToUpload)
            : this(uploadUrl)
        {
            File = fileToUpload;
        }

        public void Upload()
        {
            if (File == null || UploadUrl == null)
                return;
            Status = FileUploadStatus.Uploading;
            cancel = false;

            CheckFileOnServer();           
        }

        private void CheckFileOnServer()
        {
            UriBuilder ub = new UriBuilder(UploadUrl);
            ub.Query = string.Format("{1}filename={0}&GetBytes=true", File.Name, string.IsNullOrEmpty(ub.Query) ? "" : ub.Query.Remove(0, 1) + "&");
            WebClient client = new WebClient();
            //client.DownloadStringCompleted += new DownloadStringCompletedEventHandler(client_DownloadStringCompleted);
            client.DownloadString(ub.Uri);
            UploadFileEx();
        }

        ////void client_DownloadStringCompleted(object sender, DownloadStringCompletedEventArgs e)
        ////{
        ////    //long lengthtemp = 0;
        ////    //if (!string.IsNullOrEmpty(e.Result))
        ////    //{
        ////    //    lengthtemp = long.Parse(e.Result);
        ////    //}

        ////    //if (lengthtemp > 0)
        ////    //{
        ////    //    MessageBoxResult result;
        ////    //    if (lengthtemp == FileLength)
        ////    //    {
        ////    //        result = MessageBox.Show("File already exists, overwrite?", "Overwrite?", MessageBoxButton.OKCancel);
        ////    //        if (result == MessageBoxResult.OK)
        ////    //            lengthtemp = 0;
        ////    //        else
        ////    //        {

        ////    //            UploadProgressChangedEventArgs args = new UploadProgressChangedEventArgs(100, FileLength - BytesUploaded, BytesUploaded, FileLength, file.Name);
        ////    //            this.Dispatcher.BeginInvoke(delegate()
        ////    //            {
        ////    //                UploadProgressChanged(this, args);
        ////    //            });
        ////    //            BytesUploaded = FileLength;
        ////    //            Status = FileUploadStatus.Complete;
        ////    //            return;
        ////    //        }
        ////    //    }
        ////    //    else
        ////    //    {
        ////    //        result = MessageBox.Show("File already exists, continue upload?", "Continue?", MessageBoxButton.OKCancel);
        ////    //        if (result == MessageBoxResult.Cancel)
        ////    //            lengthtemp = 0;
        ////    //    }
        ////    //}
            
        ////    UploadFileEx();
        ////}

        public void CancelUpload()
        {
            cancel = true;
        }

        public void RemoveUpload()
        {
            cancel = true;
            remove = true;
            if(Status != FileUploadStatus.Uploading)
                Status = FileUploadStatus.Removed;
        }

        public void UploadFileEx()
        {
            Status = FileUploadStatus.Uploading;
            long temp = FileLength - BytesUploaded;

            UriBuilder ub = new UriBuilder(UploadUrl);
            bool complete = temp <= ChunkSize;            
            ub.Query = string.Format("{3}filename={0}&StartByte={1}&Complete={2}", File.Name, BytesUploaded, complete, string.IsNullOrEmpty(ub.Query) ? "" : ub.Query.Remove(0,1) + "&");

            HttpWebRequest webrequest = (HttpWebRequest)WebRequest.Create(ub.Uri);
            webrequest.Method = "POST";
            webrequest.BeginGetRequestStream(new AsyncCallback(WriteCallback), webrequest);
        }

        private void WriteCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webrequest = (HttpWebRequest)asynchronousResult.AsyncState;
            // End the operation.
            Stream requestStream = webrequest.EndGetRequestStream(asynchronousResult);

            byte[] buffer = new Byte[4096];
            int bytesRead = 0;
            int tempTotal = 0;

            Stream fileStream = resizeStream != null ? (Stream)resizeStream : File.OpenRead();

            //using (FileStream fileStream = File.OpenRead())
            //{
                fileStream.Position = BytesUploaded;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0 && tempTotal + bytesRead < ChunkSize && !cancel)
                {
                    requestStream.Write(buffer, 0, bytesRead);
                    requestStream.Flush();
                    BytesUploaded += bytesRead;
                    tempTotal += bytesRead;
                    if (UploadProgressChanged != null)
                    {
                        int percent = (int)(((double)BytesUploaded / (double)FileLength) * 100);
                        UploadProgressChangedEventArgs args = new UploadProgressChangedEventArgs(percent, bytesRead, BytesUploaded, FileLength, file.Name);
                        if (UploadProgressChanged != null) UploadProgressChanged(this, args);
                        
                    }
                }
            //}

            // only close the stream if it came from the file, don't close resizestream so we don't have to resize it over again.
                if (resizeStream == null)
                    fileStream.Close();
            requestStream.Close();
            webrequest.BeginGetResponse(new AsyncCallback(ReadCallback), webrequest);

        }
        private void ReadCallback(IAsyncResult asynchronousResult)
        {
            HttpWebRequest webrequest = (HttpWebRequest)asynchronousResult.AsyncState;
            HttpWebResponse response = (HttpWebResponse)webrequest.EndGetResponse(asynchronousResult);
            StreamReader reader = new StreamReader(response.GetResponseStream());

            string responsestring = reader.ReadToEnd();
            reader.Close();

            if (cancel)
            {
                if (resizeStream != null)
                    resizeStream.Close();
                if (remove)
                    Status = FileUploadStatus.Removed;
                else
                    Status = FileUploadStatus.Canceled;
            }
            else if (BytesUploaded < FileLength)
                UploadFileEx();
            else
            {
                if (resizeStream != null)
                    resizeStream.Close();

                Status = FileUploadStatus.Complete;
            }

        }

    }
}
