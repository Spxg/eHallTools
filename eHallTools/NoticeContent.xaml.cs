using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace eHallTools
{
    public partial class NoticeContent : Window
    {
        private readonly string noticeId;
        private readonly string token;
        private string content;
        private string title;

        public NoticeContent(string noticeId, string token)
        {
            InitializeComponent();

            this.noticeId = noticeId;
            this.token = token;

            ShowContentAsync();
        }

        public async void ShowContentAsync()
        {
            FileOperation fileOperation = new FileOperation(FileGrid);
            JsonOperation notice = new JsonOperation();
            NoticeDetail detail = await notice.GetContentInfoAsync(noticeId);

            title = detail.Title;
            content = detail.Content;

            string text = Regex.Replace(content, @"<.*?>|&.*?;", string.Empty);
            text = "        " + text;

            foreach (var m in Regex.Matches(text, @"(?<=(：|。)\s*)\w{1,2}、"))
            {
                text = text.Insert(text.LastIndexOf(m.ToString()), "\n");
            }

            foreach (var m in Regex.Matches(text, @"(?<=(：|。)\s*)\d(、|\.)"))
            {
                text = text.Insert(text.LastIndexOf(m.ToString()), "\n");
            }

            DetailContent.Text = text;

            fileOperation.ShowFileAsync(token);
        }

        public void DownloadNotice()
        {
            string fileName = title + ".html";
            var directory = Path.Combine(Environment.CurrentDirectory + "\\downloads\\", title, "notice");
            var path = Path.Combine(directory, fileName);
            
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            if (!File.Exists(path))
            {
                using StreamWriter sw = new StreamWriter(path, false);
                sw.Write(content);
            }
        }

        private void FileGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FileGrid.SelectedIndex != -1)
            {
                FileOperation.FileInfo item = (FileOperation.FileInfo)FileGrid.SelectedItem;
                FileOperation fileOperation = new FileOperation(FileGrid);
                fileOperation.DownloadFileAync(item.FileToken, item.FileName, title);
            }
        }

        private void DetailContent_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
           
            DownloadNotice();
            string fileName = title + ".html";
            var directory = Path.Combine(Environment.CurrentDirectory + "\\downloads\\", title, "notice");
            var path = Path.Combine(directory, fileName);

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
    }
}
