using System.Windows;

namespace eHallTools
{
    public partial class CourseSelectionView : Window
    {
        public CourseSelectionView()
        {
            InitializeComponent();
            CourseOperation courseOperation = new CourseOperation(CourseGrid);
            courseOperation.ShowCourseInfo();
        }
    }
}
