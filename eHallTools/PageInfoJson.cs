using System.Collections.Generic;

namespace eHallTools
{
    public class NoticeInfo
    {
        public string Title { get; set; }
        public string Create_time { get; set; }
        public string Publish_User_Dept_Name { get; set; }
        public string Publish_User_Name { get; set; }
        public string Read_Count { get; set; }
        public string Wid { get; set; }
        public string Update_Time { get; set; }
        public string Attachment { get; set; }
    }

    public class NoticeList
    {
        public List<NoticeInfo> AList { get; set; }
    }

    public class PageObject
    {
        public NoticeList Qp { get; set; }
    }
}
