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
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (Application.Text == "通知公告")
            {
                NoticePage noticePage = new NoticePage();
                noticePage.Show();
            }
        }
    }
}
