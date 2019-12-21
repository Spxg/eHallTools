using System.Collections.Generic;
using System.Net.Http;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace eHallTools
{
    class Account
    {
        public async Task<bool> LoginAsync(string studentId, string password, HttpClient loginClient)
        {
            var initResponse = await loginClient.GetStringAsync(MainWindow.authserverHttp + "/authserver/login");
            var loginPostData = new Dictionary<string, string>
            {
                { "username", studentId },
                { "password", password },
                { "lt", Regex.Match(initResponse, @"(?<=name=""lt"".*?"").*(?="")").Value },
                { "dllt", "userNamePasswordLogin" },
                { "execution", Regex.Match(initResponse, @"(?<=name=""execution"".*?"").*(?="")").Value },
                { "_eventId", "submit" },
                { "rmShown", "1" }
            };

            var loginPost = new FormUrlEncodedContent(loginPostData);
            var loginResponse = await loginClient.PostAsync(MainWindow.authserverHttp + "/authserver/login", loginPost);

            var loginError = Regex.Match(await loginResponse.Content.ReadAsStringAsync(), @"loginMsg").Success;

            if (loginError)
            {
                MessageBox.Show("登录失败");
                return false;
            }
            else
            {
                return true;
            }
        }

        public async Task LogoutAsync()
        {
            await MainWindow.operateClient.GetAsync(MainWindow.authserverHttp + "/authserver/logout");
        }

        public async Task<MatchCollection> GetInfoAsync()
        {
            var data = await MainWindow.operateClient.GetStringAsync(MainWindow.eHallHttp + "/publicapp/sys/bulletin/index.do");
            var temp = Regex.Match(data, @"(?<=\[).*(?=\])").Value;
            var info = Regex.Matches(temp, @"(?<="")\w+(?="")");
            return info;
        }
    }
}
