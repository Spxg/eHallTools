using System.Windows;

namespace eHallTools
{
    /// <summary>
    /// Interaction logic for STService.xaml
    /// </summary>
    public partial class STService : Window
    {
        public STService()
        {
            ExamOperation examOperation = new ExamOperation();
            InitializeComponent();
            examOperation.ShowArrangedExam(ArrangedExamGrid);
        }
    }
}
