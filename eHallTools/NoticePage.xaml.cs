using System;
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
            int checkNumber = 0;
            
            if (!int.TryParse(PageNumber.Text, out checkNumber) || checkNumber <= 0)
            {
                MessageBox.Show("请输入正确的页数");
                PageNumber.Text = "1";
            }

            if (!int.TryParse(PageSize.Text, out checkNumber) || checkNumber <= 0)
            {
                MessageBox.Show("请输入正确的数字");
                PageSize.Text = "10";
            }

            JsonOperation notice = new JsonOperation();
            PageObject pageJson = await notice.GetPageInfoAsync(PageNumber.Text, PageSize.Text, SearchParam.Text);

            if (pageJson != null)
            {
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

                TotalSize.Text = pageJson.Qp.TotalSize;
                PageGrid.DataContext = PageData;
            }
            else
            {
                SearchParam.Text = String.Empty;
                PageNumber.Text = "1";
            }
        }

        private void PageGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PageInfo item = (PageInfo)PageGrid.SelectedItem;
            NoticeContent content = new NoticeContent(item.NoticeId, item.Attachment);
            content.Show();
        }

        private void PageSize_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowPage();
            }
        }

        private void NextPage_Click(object sender, RoutedEventArgs e)
        {
            int checkNumber = 0;

            if (!int.TryParse(PageNumber.Text, out checkNumber) || checkNumber <= 0)
            {
                MessageBox.Show("请输入正确的数字");
                PageNumber.Text = "1";
            }
            else
            {
                PageNumber.Text = (checkNumber + 1).ToString();
                ShowPage();
            }
        }

        private void PreviousPage_Click(object sender, RoutedEventArgs e)
        {
            int checkNumber = 0;

            if (!int.TryParse(PageNumber.Text, out checkNumber) || checkNumber < 1)
            {
                MessageBox.Show("请输入正确的数字");
                PageNumber.Text = "1";
            }
            else
            {
                PageNumber.Text = (checkNumber - 1).ToString();
                ShowPage();
            }
        }

        private void PageNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowPage();
            }
        }

        private void Search_Click(object sender, RoutedEventArgs e)
        {
            PageNumber.Text = "1";
            ShowPage();
        }

        private void SearchParam_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ShowPage();
            }
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
