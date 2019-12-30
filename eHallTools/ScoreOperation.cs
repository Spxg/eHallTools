using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Controls;

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
            string info = await ParseInfo();
            ObservableCollection<ScoreInfo> scoreInfo = new ObservableCollection<ScoreInfo>();

            foreach (var item in Regex.Matches(info, @"(?<=class=""t_con"">).*?(?=</tr>)"))
            {
                int i = 0;
                string[] temp = new string[13];

                foreach (var value in Regex.Matches(item.ToString(), @"(?<="">).*?(?=</td>)"))
                {
                    string data = value.ToString();

                    if (value.ToString().Contains("strong"))
                    {
                        data = Regex.Match(value.ToString(), @"(?<=<strong>).*?(?=</strong>)").Value;
                    }
                    else if (value.ToString().Contains("font"))
                    {
                        data = Regex.Match(value.ToString(), @"(?<=<font.*?>).*?(?=</font>)").Value;
                    }

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

        public async Task<string> ParseInfo()
        {
            string data = await MainWindow.operateClient.GetStringAsync("http://ssfw.tjut.edu.cn/ssfw/zhcx/cjxx.do");
            data = data.Replace("\r", String.Empty);
            data = data.Replace("\n", String.Empty);
            data = data.Replace("\t", String.Empty);
            data = data.Replace("&nbsp;", String.Empty);
            data = data.Replace(" ", String.Empty);
            data = Regex.Match(data, "<table.*</table>").Value;

            return data;
        }
    }
}
