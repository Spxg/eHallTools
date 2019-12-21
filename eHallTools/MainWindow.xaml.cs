using System;
using System.Windows;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;

namespace eHallTools
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public static HttpClient operateClient;
        public static string authserverHttp;
        public static string eHallHttp;
        public static string configPath;
        public MainWindow()
        {
            InitializeComponent();
            configPath = Environment.CurrentDirectory + "//config";
            
            UpdateServerAddress();
            GetSettings();
        }

        private async void Login_Click(object sender, RoutedEventArgs e)
        {
            Login.IsEnabled = false;
            authserverHttp = AuthserverHttp.SelectedItem.ToString();
            eHallHttp = EhallHttp.SelectedItem.ToString();

            CookieContainer cc = new CookieContainer();

            bool loginStatus = false;
            using (var loginHandler = new HttpClientHandler { AllowAutoRedirect = false, CookieContainer = cc })
            {
                var loginClient = new HttpClient(loginHandler);
                loginClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36");
                loginStatus = await new Account().LoginAsync(StudentId.Text, Password.Password, loginClient);
                loginClient.Dispose();
            }

            Account account = new Account();
            var operateHandler = new HttpClientHandler { CookieContainer = cc };
            operateClient = new HttpClient(operateHandler);
            operateClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) " +
                "AppleWebKit/537.36 (KHTML, like Gecko) Chrome/77.0.3865.120 Safari/537.36");

            if (loginStatus)
            {
                Applications applications = new Applications();
                var info = await account.GetInfoAsync();

                applications.StudentId.Text = info[0].Value;
                applications.StudentName.Text = info[1].Value;
                applications.Sex.Text = info[2].Value;
                applications.School.Text = info[3].Value;

                UpdateSettings();
                Hide();
                applications.ShowDialog();
                Show();
            }

            Login.IsEnabled = true;

            await account.LogoutAsync();
        }

        public void GetSettings()
        {
            JsonOperation jsonOperation = new JsonOperation();
            Setting settings = jsonOperation.GetSettingsInfo();

            University.SelectedIndex = settings.SelectedUniversityIndex.Value;
            RememberPassword.IsChecked = settings.RememberPassword;
            AutoLogin.IsChecked = settings.AutoLogin;
            StudentId.Text = settings.StudentId;
            Password.Password = settings.Password;

            if (settings.AutoLogin)
            {
                ButtonAutomationPeer peer = new ButtonAutomationPeer(Login);
                IInvokeProvider login = peer.GetPattern(PatternInterface.Invoke) as IInvokeProvider;
                login.Invoke();
            }
        }

        public void UpdateSettings()
        {
            JsonOperation jsonOperation = new JsonOperation();
            Setting settings = jsonOperation.GetSettingsInfo();
            
            var directory = Environment.CurrentDirectory + "\\config";
            var path = Path.Combine(directory, "settings.json");

            settings.SelectedUniversityIndex = University.SelectedIndex;
            settings.StudentId = StudentId.Text;
            settings.RememberPassword = RememberPassword.IsChecked.Value;
            settings.AutoLogin = AutoLogin.IsChecked.Value;

            if (RememberPassword.IsChecked.Value)
            {
                settings.Password = Password.Password;
            }
            else
            {
                settings.Password = string.Empty;
                settings.AutoLogin = false;
            }

            jsonOperation.UpdateJson<Setting>(path, settings);
        }


        public void UpdateServerAddress()
        {
            JsonOperation jsonOperation = new JsonOperation();
            ServerList server = jsonOperation.GetServerInfo();

            University.Items.Clear();
            AuthserverHttp.Items.Clear();
            EhallHttp.Items.Clear();

            foreach (var item in server.Info)
            {
                University.Items.Add(item.University);
                AuthserverHttp.Items.Add(item.AuthserverHttp);
                EhallHttp.Items.Add(item.EhallHttp);
            }

            University.SelectedIndex = 0;
            AuthserverHttp.SelectedIndex = 0;
            EhallHttp.SelectedIndex = 0;
        }

        private void Update_Click(object sender, RoutedEventArgs e)
        {
            UpdateServerAddress();
        }

        private void University_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            AuthserverHttp.SelectedIndex = University.SelectedIndex;
            EhallHttp.SelectedIndex = University.SelectedIndex;
        }

        private void AuthserverHttp_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            University.SelectedIndex = AuthserverHttp.SelectedIndex;
            EhallHttp.SelectedIndex = AuthserverHttp.SelectedIndex;
        }

        private void EhallHttp_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            University.SelectedIndex = EhallHttp.SelectedIndex;
            EhallHttp.SelectedIndex = EhallHttp.SelectedIndex;
        }

        private void AutoLogin_Checked(object sender, RoutedEventArgs e)
        {
            RememberPassword.IsChecked = true;
        }

        private void RememberPassword_Click(object sender, RoutedEventArgs e)
        {
            if (!RememberPassword.IsChecked.Value)
            {
                AutoLogin.IsChecked = false;
            }
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            EditServer editServer = new EditServer();
            JsonOperation jsonOperation = new JsonOperation();
            ServerList server = jsonOperation.GetServerInfo();

            foreach (var item in server.Info)
            {
                editServer.UniversityList.Items.Add(item.University);
            }

            editServer.UniversityList.SelectedIndex = 0;

            editServer.ShowDialog();
        }
    }
}
