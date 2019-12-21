using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;

namespace eHallTools
{
    class ExamOperation
    {
        public async void ShowArrangedExam(DataGrid examGrid)
        {
            await MainWindow.operateClient.GetAsync("http://ssfw.tjut.edu.cn/ssfw/j_spring_ids_security_check");

            string data = await MainWindow.operateClient.GetStringAsync("http://ssfw.tjut.edu.cn/ssfw/xsks/kcxx.do");
            data = data.Replace("\r", String.Empty);
            data = data.Replace("\n", String.Empty);
            data = data.Replace("\t", String.Empty);
            data = Regex.Match(data, @"(?<=已安排考试课程).*?(?=</fieldset>)").Value;

            ObservableCollection<ExamInfo> examData = new ObservableCollection<ExamInfo>();

            foreach (var item in Regex.Matches(data, @"(?<=class=""t_con"">).*?(?=</tr>)"))
            {
                int i = 0;
                string[] temp = new string[11];

                foreach (var value in Regex.Matches(item.ToString(), @"(?<="">).*?(?=(</span>|</td>))"))
                {
                    temp[i++] = value.ToString();
                }
                
                examData.Add(new ExamInfo()
                {
                    SerialNumber = int.Parse(temp[0]),
                    SubjectNumber = int.Parse(temp[1]),
                    SubjectName = temp[2],
                    SubjectProperity = temp[3],
                    SubjectTeacher = temp[4],
                    Credit = int.Parse(temp[5]),
                    Time = temp[6],
                    Place = temp[7],
                    Method = temp[8],
                    Way = temp[9],
                    Status = temp[10]
                }) ;
            }
            examGrid.DataContext = examData;
        }
    }

    public class ExamInfo
    {
        public int SerialNumber { get; set; }
        public int SubjectNumber { get; set; }
        public string SubjectName { get; set; }
        public string SubjectProperity { get; set; }
        public string SubjectTeacher { get; set; }
        public int Credit { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        public string Method { get; set; }
        public string Way { get; set; }
        public string Status { get; set; }
    }
}
