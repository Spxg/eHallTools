namespace eHallTools
{
    public class ArrangedExamInfo
    {
        public int SerialNumber { get; set; }
        public int SubjectNumber { get; set; }
        public string SubjectName { get; set; }
        public string SubjectProperity { get; set; }
        public string SubjectTeacher { get; set; }
        public double Credit { get; set; }
        public string Time { get; set; }
        public string Place { get; set; }
        public string Method { get; set; }
        public string Way { get; set; }
        public string Status { get; set; }
    }

    public class ArrangingExamInfo
    {
        public int SerialNumber { get; set; }
        public int SubjectNumber { get; set; }
        public string SubjectName { get; set; }
        public string SubjectProperity { get; set; }
        public string SubjectTeacher { get; set; }
        public double Credit { get; set; }
        public string TimeAndPlace { get; set; }
    }

    public class NotArrangeExamInfo
    {
        public int SerialNumber { get; set; }
        public int SubjectNumber { get; set; }
        public string SubjectName { get; set; }
        public double Credit { get; set; }
        public string TimeAndPlace { get; set; }
    }
}
