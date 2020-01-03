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
            Function.Items.Add("选课结果查看");
        }

        private void Apply_Click(object sender, RoutedEventArgs e)
        {
            if (Function.SelectedIndex == 0)
            {
                ExamArrangement examArrangement = new ExamArrangement();
                examArrangement.Show();
            }

            if (Function.SelectedIndex == 1)
            {
                ScoreQuery scoreQuery = new ScoreQuery();
                scoreQuery.Show();
            }

            if (Function.SelectedIndex == 2)
            {
                CourseSelectionView view = new CourseSelectionView();
                view.Show();
            }
        }
    }
}
