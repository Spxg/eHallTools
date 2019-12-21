using System;
using System.IO;
using System.Windows;

namespace eHallTools
{
    /// <summary>
    /// Interaction logic for EditServer.xaml
    /// </summary>
    public partial class EditServer : Window
    {
        public EditServer()
        {
            InitializeComponent();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var path = Path.Combine(MainWindow.configPath, "server.json");
            JsonOperation jsonOperation = new JsonOperation();
            ServerList server = jsonOperation.GetServerInfo();

            bool removeStatus = server.Info.Remove(server.Info[UniversityList.SelectedIndex]);
            jsonOperation.UpdateJson<ServerList>(path, server);

            if (removeStatus)
            {
                MessageBox.Show("删除成功");
            }
        }

        private void AddNewServer_Click(object sender, RoutedEventArgs e)
        {
            AddServer addServer = new AddServer();
            addServer.ShowDialog();
        }
    }
}
