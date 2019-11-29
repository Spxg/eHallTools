using System;
using System.IO;
using System.Windows;

namespace eHallTools
{
    /// <summary>
    /// Interaction logic for AddServer.xaml
    /// </summary>
    public partial class AddServer : Window
    {
        public AddServer()
        {
            InitializeComponent();
        }

        public void AddServerInfo()
        {
            var path = Path.Combine(Environment.CurrentDirectory + "\\config", "server.json");

            Server newServer = new Server()
            {
                University = NewUniversity.Text,
                AuthserverHttp = NewAuthServer.Text,
                EhallHttp = NewEhallServer.Text
            };

            JsonOperation jsonOperation = new JsonOperation();
            ServerList server = jsonOperation.GetServerInfo();
            server.Info.Add(newServer);

            jsonOperation.UpdateJson<ServerList>(path, server);

            MessageBox.Show("添加成功");
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddServerInfo();
        }
    }
}

