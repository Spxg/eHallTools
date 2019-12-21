using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System;

namespace eHallTools
{
    class FileOperation
    {
        public async void ShowFileAsync(string token, DataGrid fileGrid)
        { 
            JsonOperation notice = new JsonOperation();
            FileList fileList = await notice.GetFileInfoAsync(token);

            ObservableCollection<FileInfo> fileData = new ObservableCollection<FileInfo>();

            foreach (var item in fileList.AList)
            {
                fileData.Add(new FileInfo()
                {
                    FileName = item.FileName,
                    FileSize = item.FileSize,
                    FileToken = item.FileToken
                });
            }

            fileGrid.DataContext = fileData;
        }

        public async void DownloadFileAync(string fileToken, string fileName)
        {
            var directory = Environment.CurrentDirectory + "\\downloads";
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            var path = Path.Combine(directory, fileName);
            using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
            {
                var stream = await MainWindow.operateClient.GetStreamAsync(MainWindow.eHallHttp + "/publicapp/sys/emapcomponent/file/getAttachmentFile/" + fileToken + ".do");
                stream.CopyTo(file);
            }

            MessageBox.Show("下载完成");
        }

        public class FileInfo
        {
            public string FileName { get; set; }
            public string FileSize { get; set; }
            public string FileToken { get; set; }
        }
    }
}
