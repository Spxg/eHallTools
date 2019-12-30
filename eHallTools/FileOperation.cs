using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Controls;
using System.Windows;
using System;

namespace eHallTools
{
    class FileOperation
    {
        private readonly DataGrid fileGrid;
        public FileOperation(DataGrid fileGrid)
        {
            this.fileGrid = fileGrid;
        }

        public async void ShowFileAsync(string token)
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

        public async void DownloadFileAync(string fileToken, string fileName, string title)
        {
            MessageBoxResult result;
            var directory = Environment.CurrentDirectory + "\\downloads\\" + title + "\\file";
            var path = Path.Combine(directory, fileName);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(path))
            {
                using (FileStream file = new FileStream(path, FileMode.Create, FileAccess.Write))
                {
                    var stream = await MainWindow.operateClient.GetStreamAsync(MainWindow.eHallHttp + "/publicapp/sys/emapcomponent/file/getAttachmentFile/" + fileToken + ".do");
                    stream.CopyTo(file);
                }
                
                result = MessageBox.Show("下载完成，是否打开", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            }
            else
            {
                result = MessageBox.Show("文件已存在，是否打开", "提示", MessageBoxButton.YesNo, MessageBoxImage.Information);
            }

            if (result == MessageBoxResult.Yes)
            {
                OpenFile(path);
            }
        }

        public void OpenFile(string path)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            try
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = path;
                process.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public class FileInfo
        {
            public string FileName { get; set; }
            public string FileSize { get; set; }
            public string FileToken { get; set; }
        }
    }
}
