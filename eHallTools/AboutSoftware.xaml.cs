using System;
using System.Windows;
using System.Windows.Input;

namespace eHallTools
{
    public partial class AboutSoftware : Window
    {
        public AboutSoftware()
        {
            InitializeComponent();
        }

        private void Item_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();
            
            try
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "https://github.com/Spxg/eHallTools";
                process.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("打开失败");
            }
        }

        private void Author_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            System.Diagnostics.Process process = new System.Diagnostics.Process();

            try
            {
                process.StartInfo.UseShellExecute = true;
                process.StartInfo.FileName = "https://spxg.me";
                process.Start();
            }
            catch (Exception)
            {
                MessageBox.Show("打开失败");
            }
        }
    }
}
