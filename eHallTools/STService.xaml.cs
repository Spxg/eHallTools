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
            Function.Items.Add("成绩查询");
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (Function.Text == "考试安排查询")
            {
                ExamArrangement examArrangement = new ExamArrangement();
                examArrangement.Show();
            }

            if (Function.Text == "成绩查询")
            {
                ScoreQuery scoreQuery = new ScoreQuery();
                scoreQuery.Show();
            }
        }
    }
}
