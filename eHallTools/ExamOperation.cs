using System;
using System.Windows.Controls;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

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
            (string arrangedExamInfo, string arrangingExamInfo, string notArrangeExamInfo) = await ParseInfo();
            ObservableCollection<ArrangedExamInfo> arrangedExamData = new ObservableCollection<ArrangedExamInfo>();
            ObservableCollection<ArrangingExamInfo> arrangingExamData = new ObservableCollection<ArrangingExamInfo>();
            ObservableCollection<NotArrangeExamInfo> notArrangeExamData = new ObservableCollection<NotArrangeExamInfo>();

            foreach (var item in Regex.Matches(arrangedExamInfo, @"(?<=class=""t_con"">).*?(?=</tr>)"))
            {
                int i = 0;
                string[] temp = new string[11];

                foreach (var value in Regex.Matches(item.ToString(), @"(?<="">).*?(?=(</span>|</td>))"))
                {
                    temp[i++] = value.ToString();
                }

                arrangedExamData.Add(new ArrangedExamInfo()
                {
                    SerialNumber = int.Parse(temp[0]),
                    SubjectNumber = int.Parse(temp[1]),
                    SubjectName = temp[2],
                    SubjectProperity = temp[3],
                    SubjectTeacher = temp[4],
                    Credit = double.Parse(temp[5]),
                    Time = temp[6],
                    Place = temp[7],
                    Method = temp[8],
                    Way = temp[9],
                    Status = temp[10]
                });
            }
            
            foreach (var item in Regex.Matches(arrangingExamInfo, @"(?<=class=""t_con"">).*?(?=</tr>)"))
            {
                int i = 0;
                string[] temp = new string[7];

                foreach (var value in Regex.Matches(item.ToString(), @"(?<="">).*?(?=(</span>|</td>))"))
                {
                    temp[i++] = value.ToString();
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
            
            foreach (var item in Regex.Matches(notArrangeExamInfo, @"(?<=class=""t_con"">).*?(?=</tr>)"))
            {
                int i = 0;
                string[] temp = new string[5];

                foreach (var value in Regex.Matches(item.ToString(), @"(?<="">).*?(?=(</span>|</td>))"))
                {
                    temp[i++] = value.ToString();
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

        public async Task<(string, string, string)> ParseInfo()
        {
            string data = await MainWindow.operateClient.GetStringAsync("http://ssfw.tjut.edu.cn/ssfw/xsks/kcxx.do");
            data = data.Replace("\r", String.Empty);
            data = data.Replace("\n", String.Empty);
            data = data.Replace("\t", String.Empty);
            var arrangedExamInfo = Regex.Match(data, @"(?<=已安排考试课程).*?(?=</fieldset>)").Value;
            var arrangingExamInfo = Regex.Match(data, @"(?<=安排中的考试课程).*?(?=</fieldset>)").Value;
            var notArrangeExamInfo = Regex.Match(data, @"(?<=未编排考试的课程).*?(?=</fieldset>)").Value;

            return (arrangedExamInfo, arrangingExamInfo, notArrangeExamInfo);
        }
    }
}
