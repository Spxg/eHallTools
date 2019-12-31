using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Controls;
using HtmlAgilityPack;

namespace eHallTools
{
    class ScoreOperation
    {
        private readonly DataGrid scoreGrid;

        public ScoreOperation(DataGrid scoreGrid)
        {
            this.scoreGrid = scoreGrid;
        }

        public async void ShowScoreInfo()
        {
            HtmlNodeCollection info = await ParseInfo();
            ObservableCollection<ScoreInfo> scoreInfo = new ObservableCollection<ScoreInfo>();

            foreach (var item in info)
            {
                int i = 0;
                string[] temp = new string[13];

                foreach (var value in item.Elements("td"))
                {
                    string data = value.InnerText;
                    data = data.Replace(" ", string.Empty);
                    temp[i++] = data;
                }

                scoreInfo.Add(new ScoreInfo()
                {
                    SerialNumber = int.Parse(temp[0]),
                    LearnType = temp[1],
                    Semester = temp[2],
                    SubjectNumber = int.Parse(temp[3]),
                    SubjectName = temp[4],
                    SubjectType = temp[5],
                    SubjectProperity = temp[6],
                    PublicSubjectType = temp[7],
                    Credit = temp[8],
                    Score = temp[9],
                    LearnMethod = temp[10],
                    RepeatOperation = temp[11],
                    Remark = temp[12]
                });
            }

            scoreGrid.DataContext = scoreInfo;
        }

        public async Task<HtmlNodeCollection> ParseInfo()
        {
            string data = await MainWindow.operateClient.GetStringAsync("http://ssfw.tjut.edu.cn/ssfw/zhcx/cjxx.do");
            data = data.Replace("&nbsp;", string.Empty);
            data = data.Replace("\n", string.Empty);
            data = data.Replace("\t", string.Empty);
            data = data.Replace("\r", string.Empty);

            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(data);
            HtmlNode root = doc.DocumentNode;
            HtmlNodeCollection nodes = root.SelectNodes("//*[@title=\"原始成绩\"]/table/tr[@class=\"t_con\"]");

            return nodes;
        }
    }
}
