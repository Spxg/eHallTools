using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;
using System.Collections.Generic;
using System.Net.Http;

namespace eHallTools
{
    public partial class CurrentLogin : Window
    {
        private ObservableCollection<OnlineClientInfo> client;

        public CurrentLogin()
        {
            InitializeComponent();
            InitializeClientShow();
        }

        public async void InitializeClientShow()
        {
            OnlineClientOperation operation = new OnlineClientOperation(ClientGrid);
            client = await operation.ShowClient();
        }

        private async void ClientGrid_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            int index = ClientGrid.SelectedIndex;

            if (!client[index].IsCurrentClient)
            {
                string tokenId = client[index].TokenId;
                var removePostData = new Dictionary<string, string>()
                {
                    { "tokenId" , tokenId}
                };

                var removePost = new FormUrlEncodedContent(removePostData);
                await MainWindow.operateClient.PostAsync(MainWindow.authserverHttp + "/authserver/removeOnlineUser.do", removePost);

                client.Remove(client[index]);
            }
        }

        private async void Remove_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in client)
            {
                if (!item.IsCurrentClient)
                {
                    string tokenId = item.TokenId;
                    var removePostData = new Dictionary<string, string>()
                    {
                        { "tokenId" , tokenId}
                    };

                    var removePost = new FormUrlEncodedContent(removePostData);
                    await MainWindow.operateClient.PostAsync(MainWindow.authserverHttp + "/authserver/removeOnlineUser.do", removePost);
                }
            }

            OnlineClientOperation operation = new OnlineClientOperation(ClientGrid);
            client = await operation.ShowClient();
        }
    }
}
