using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace eHallTools
{
    /// <summary>
    /// Interaction logic for NoticePage.xaml
    /// </summary>
    public partial class NoticePage : Window
    {
        public NoticePage()
        {
            InitializeComponent();
            ShowPage();
        }

        public async void ShowPage()
        {
            JsonOperation notice = new JsonOperation();
            PageObject pageJson = await notice.GetPageInfoAsync(PageNumber.Text, PageSize.Text);
            ObservableCollection<PageInfo> PageData = new ObservableCollection<PageInfo>();

            foreach (var item in pageJson.Qp.AList)
            {
                PageData.Add(new PageInfo()
                {
                    Title = item.Title,
                    ReadCount = int.Parse(item.Read_Count),
                    PublishPeople = item.Publish_User_Name,
                    PublishDept = item.Publish_User_Dept_Name,
                    PublishTime = item.Create_time,
                    LastUpdateTime = item.Update_Time,
                    NoticeId = item.Wid,
                    Attachment = item.Attachment
                });
            }
            
            PageGrid.DataContext = PageData;

        }

        private void PageGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PageInfo item = (PageInfo)PageGrid.SelectedItem;
            NoticeContent content = new NoticeContent(item.NoticeId, item.Attachment);
            content.Show();
        }

        private void PageSize_KeyDown(object sender, KeyEventArgs e)
        {
            ShowPage();
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            PageNumber.Text = (int.Parse(PageNumber.Text) + 1).ToString();
            ShowPage();
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            int number = int.Parse(PageNumber.Text);
            
            if (number > 1)
            {
                PageNumber.Text = (number - 1).ToString();
                ShowPage();
            }
        }

        private void PageNumber_KeyDown(object sender, KeyEventArgs e)
        {
            ShowPage();
        }

        public class PageInfo
        {
            public string Title { get; set; }
            public int ReadCount { get; set; }
            public string PublishPeople { get; set; }
            public string PublishDept { get; set; }
            public string PublishTime { get; set; }
            public string LastUpdateTime { get; set; }
            public string NoticeId { get; set; }
            public string Attachment { get; set; }
        }
    }
}
