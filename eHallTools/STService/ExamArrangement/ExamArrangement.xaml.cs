using System.Windows;

namespace eHallTools
{
    public partial class ExamArrangement : Window
    {
        public ExamArrangement()
        {
            InitializeComponent();
            ExamOperation examOperation = new ExamOperation(ArrangedExamGrid, ArrangingExamGrid, NotArrangeExamGrid);
            examOperation.ShowExamInfo();
        }
    }
}
