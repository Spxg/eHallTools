using System.Windows;

namespace eHallTools
{
    public partial class ScoreQuery : Window
    {
        public ScoreQuery()
        {
            InitializeComponent();
            ScoreOperation scoreOperation = new ScoreOperation(ScoreGrid);
            scoreOperation.ShowScoreInfo();
        }
    }
}
