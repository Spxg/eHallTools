using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using HtmlAgilityPack;

namespace eHallTools
{
    class ExamOperation
    {
        private readonly DataGrid arrangedExamGrid;
        private readonly DataGrid arrangingExamGrid;
        private readonly DataGrid notArrangeExamGrid;

        public ExamOperation(DataGrid arrangedExamGrid, DataGrid arrangingExamGrid, DataGrid notArrangeExamGrid)
        {
            this.arrangedExamGrid = arrangedExamGrid;
            this.arrangingExamGrid = arrangingExamGrid;
            this.notArrangeExamGrid = notArrangeExamGrid;
        }

        public async void ShowExamInfo()
        {
            (HtmlNodeCollection arrangedExamInfo, HtmlNodeCollection arrangingExamInfo, HtmlNodeCollection notArrangeExamInfo) = await ParseInfo();
            ObservableCollection<ArrangedExamInfo> arrangedExamData = new ObservableCollection<ArrangedExamInfo>();
            ObservableCollection<ArrangingExamInfo> arrangingExamData = new ObservableCollection<ArrangingExamInfo>();
            ObservableCollection<NotArrangeExamInfo> notArrangeExamData = new ObservableCollection<NotArrangeExamInfo>();

            foreach (var item in arrangedExamInfo)
            {
                int i = 0;
                string[] temp = new string[11];

                foreach (var value in item.Elements("td"))
                {
                    temp[i++] = value.InnerText;
                }

                arrangedExamData.Add(new ArrangedExamInfo()
                {
                    SerialNumber = int.Parse(temp[0]),
                    SubjectNumber = int.Parse(temp[1]),
                    SubjectName = temp[2],
                    SubjectProperity = temp[3],
                    SubjectTeacher = temp[4],
                    Credit = temp[5],
                    Time = temp[6],
                    Place = temp[7],
                    Method = temp[8],
                    Way = temp[9],
                    Status = temp[10]
                });
            }
            
            foreach (var item in arrangingExamInfo)
            {
                int i = 0;
                string[] temp = new string[7];

                foreach (var value in item.Elements("td"))
                {
                    temp[i++] = value.InnerText;
                }
                
                arrangingExamData.Add(new ArrangingExamInfo()
                {
                    SerialNumber = int.Parse(temp[0]),
                    SubjectNumber = int.Parse(temp[1]),
                    SubjectName = temp[2],
                    SubjectProperity = temp[3],
                    SubjectTeacher = temp[4],
                    Credit = double.Parse(temp[5]),
                    TimeAndPlace = temp[6]
                });
            }
            
            foreach (var item in notArrangeExamInfo)
            {
                int i = 0;
                string[] temp = new string[5];

                foreach (var value in item.Elements("td"))
                {
                    temp[i++] = value.InnerText;
                }

                notArrangeExamData.Add(new NotArrangeExamInfo()
                {
                    SerialNumber = int.Parse(temp[0]),
                    SubjectNumber = int.Parse(temp[1]),
                    SubjectName = temp[2],
                    Credit = double.Parse(temp[3]),
                    TimeAndPlace = temp[4]
                });
            }

            arrangedExamGrid.DataContext = arrangedExamData;
            arrangingExamGrid.DataContext = arrangingExamData;
            notArrangeExamGrid.DataContext = notArrangeExamData;
        }

        public async Task<(HtmlNodeCollection, HtmlNodeCollection, HtmlNodeCollection)> ParseInfo()
        {
            string data = await MainWindow.operateClient.GetStringAsync("http://ssfw.tjut.edu.cn/ssfw/xsks/kcxx.do");
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);

            HtmlNode root = doc.DocumentNode;
            HtmlNodeCollection arrangedExamInfo = root.SelectNodes("/html/body/div[1]/div/fieldset[1]/table/tr[@class=\"t_con\"]");
            HtmlNodeCollection arrangingExamInfo = root.SelectNodes("/html/body/div[1]/div/fieldset[2]/table/tr[@class=\"t_con\"]");
            HtmlNodeCollection notArrangeExamInfo = root.SelectNodes("/html/body/div[1]/div/fieldset[3]/table/tr[@class=\"t_con\"]");

            return (arrangedExamInfo, arrangingExamInfo, notArrangeExamInfo);
        }
    }
}
