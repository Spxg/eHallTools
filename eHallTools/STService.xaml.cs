using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace eHallTools
{
    public partial class STService : Window
    {
        public STService()
        {
            InitializeComponent();
            Function.Items.Clear();
            Function.Items.Add("考试安排查询");
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (Function.Text == "考试安排查询")
            {
                ExamArrangement examArrangement = new ExamArrangement();
                examArrangement.Show();
            }
        }
    }
}
