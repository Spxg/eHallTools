using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace eHallTools
{
    /// <summary>
    /// Interaction logic for NoticeContent.xaml
    /// </summary>
    public partial class NoticeContent : Window
    {
        readonly string noticeId;
        readonly string token;

        public NoticeContent(string noticeId, string token)
        {
            InitializeComponent();

            this.noticeId = noticeId;
            this.token = token;

            ShowContentAsync();
        }

        public async void ShowContentAsync()
        {
            FileOperation fileOperation = new FileOperation();
            JsonOperation notice = new JsonOperation();
            NoticeDetail content = await notice.GetContentInfoAsync(noticeId);
            
            string text = Regex.Replace(content.Content, @"<.*?>|&.*?;", string.Empty);
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

            fileOperation.ShowFileAsync(token, FileGrid);
        }

        private void FileGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (FileGrid.SelectedIndex != -1)
            {
                FileOperation.FileInfo item = (FileOperation.FileInfo)FileGrid.SelectedItem;
                FileOperation fileOperation = new FileOperation();
                fileOperation.DownloadFileAync(item.FileToken, item.FileName);
            }
        }
    }
}
