using System.Windows;

namespace eHallTools
{
    /// <summary>
    /// Interaction logic for Applications.xaml
    /// </summary>
    public partial class Applications : Window
    {
        public Applications()
        {
            InitializeComponent();
            Application.Items.Clear();
            Application.Items.Add("通知公告");
            Application.Items.Add("师生服务");
        }

        private async void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Text == "通知公告")
            {
                await MainWindow.operateClient.GetAsync(MainWindow.eHallHttp + "/publicapp/sys/bulletin/configSet/noraml/getRouteConfig.do");
                
                NoticePage noticePage = new NoticePage();
                noticePage.Show();
            }

            if (Application.Text == "师生服务")
            {
                await MainWindow.operateClient.GetAsync("http://ssfw.tjut.edu.cn/ssfw/j_spring_ids_security_check");

                STService service = new STService();
                service.Show();
            }
        }
    }
}
