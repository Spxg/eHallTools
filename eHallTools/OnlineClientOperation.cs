using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using HtmlAgilityPack;

namespace eHallTools
{
    class OnlineClientOperation
    {
        private readonly DataGrid clientGrid;

        public OnlineClientOperation(DataGrid clientGrid)
        {
            this.clientGrid = clientGrid;
        }

        public async Task<ObservableCollection<OnlineClientInfo>> ShowClient()
        {
            HtmlNodeCollection nodes = await ParseInfoAsync();
            ObservableCollection<OnlineClientInfo> client = new ObservableCollection<OnlineClientInfo>();

            foreach (var item in nodes)
            {
                int i = 0;
                string[] temp = new string[5];

                foreach (var info in item.Elements("td"))
                {
                    if (i == 3)
                    {
                        string data = info.InnerHtml;
                        data = Regex.Match(data, @"(?<=removeOnline\(\').*?(?=\'\))").Value;
                        
                        if (data != string.Empty)
                        {
                            temp[i++] = "false";
                        }
                        else
                        {
                            temp[i++] = "true";
                        }

                        temp[i] = data;
                    }
                    else
                    {
                        temp[i++] = info.InnerText;
                    }
                }

                client.Add(new OnlineClientInfo()
                {
                    UserIp = temp[0],
                    LoginTime = temp[1],
                    ClientType = temp[2],
                    IsCurrentClient = bool.Parse(temp[3]),
                    TokenId = temp[4]
                });
            }

            clientGrid.DataContext = client;
            return client;
        }

        public async Task<HtmlNodeCollection> ParseInfoAsync()
        {
            HtmlNodeCollection nodes = null;

            try
            {
                string data = await MainWindow.operateClient.GetStringAsync("http://authserver.tjut.edu.cn/authserver/userOnline.do");
                data = data.Replace("\t", string.Empty);
                data = data.Replace("\n", string.Empty);
                data = data.Replace("\r", string.Empty);

                HtmlDocument doc = new HtmlDocument();
                doc.LoadHtml(data);
                HtmlNode root = doc.DocumentNode;
                nodes = root.SelectNodes("/html/body/div/div/div/div/div/div/div[2]/div[1]/table/tr");
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("302"))
                {
                    MessageBox.Show("302跳转导致错误");
                }
            }

            return nodes;
        }
    }
}

public class OnlineClientInfo
{
    public string UserIp { get; set; }
    public string LoginTime { get; set; }
    public string ClientType { get; set; }
    public bool IsCurrentClient { get; set; }
    public string TokenId { get; set; }
}
