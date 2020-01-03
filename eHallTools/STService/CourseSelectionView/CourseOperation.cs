using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using HtmlAgilityPack;

namespace eHallTools
{
    class CourseOperation
    {
        private readonly DataGrid courseGrid;
        public CourseOperation(DataGrid courseGrid)
        {
            this.courseGrid = courseGrid;
        }

        public async void ShowCourseInfo()
        {
            HtmlNodeCollection info = await ParseInfo();
            ObservableCollection<CourseInfo> courseInfo = new ObservableCollection<CourseInfo>();

            foreach (var item in info)
            {
                int i = 0;
                string[] temp = new string[12];

                foreach (var value in item.Elements("td"))
                {
                    string data = value.InnerText;
                    temp[i++] = data;
                }

                courseInfo.Add(new CourseInfo
                {
                    Semester = temp[0],
                    SubjectNumber = int.Parse(temp[1]),
                    SubjectName = temp[2],
                    LearnTime = int.Parse(temp[3]),
                    Credit = double.Parse(temp[4]),
                    Time = temp[5],
                    Place = temp[6],
                    SubjectTeacher = temp[7],
                    Campus = temp[8],
                    Type = temp[9],
                    LearnMethod = temp[10],
                    Operation = temp[11]
                });
            }

            courseGrid.DataContext = courseInfo;
        }

        public async Task<HtmlNodeCollection> ParseInfo()
        {
            string data = await MainWindow.operateClient.GetStringAsync("http://ssfw.tjut.edu.cn/ssfw/xkgl/xkjgcx.do");
            data = data.Replace("\n", string.Empty);
            data = data.Replace("\t", string.Empty);
            data = data.Replace("\r", string.Empty);
            data = data.Replace("<br/>", "\n");

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);
            HtmlNode root = doc.DocumentNode;
            HtmlNodeCollection nodes = root.SelectNodes("//*[@class=\"ks-tabs-panel ks-tabs-panel-selected\"]/div/table/tr[@class=\"t_con\"]");

            return nodes;
        }
    }
}
