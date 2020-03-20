using System.Windows;

namespace eHallTools
{
    public partial class Applications : Window
    {
        public Applications()
        {
            InitializeComponent();
            Application.Items.Clear();
            Application.Items.Add("通知公告");
            Application.Items.Add("师生服务");
            Application.Items.Add("当前登录");
        }

        private async void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (Application.SelectedIndex == 0)
            {
                await MainWindow.operateClient.GetAsync(MainWindow.eHallHttp + "/publicapp/sys/bulletin/configSet/noraml/getRouteConfig.do");

                NoticePage noticePage = new NoticePage();
                noticePage.Show();
            }

            if (Application.SelectedIndex == 1)
            {
                var check = await MainWindow.operateClient.GetAsync("http://ssfw.tjut.edu.cn/ssfw/cas_index.jsp");

                if (check.IsSuccessStatusCode)
                {
                    STService service = new STService();
                    service.Show();
                }
                else
                {
                    MessageBox.Show("获取权限失败，请尝试退出重新登录");
                }
            }

            if (Application.SelectedIndex == 2)
            {
                CurrentLogin currentLogin = new CurrentLogin();
                currentLogin.Show();
            }
        }
    }
}
