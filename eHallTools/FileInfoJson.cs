using System.Collections.Generic;

namespace eHallTools
{
    public class FileInfo
    {
        public string FileName { get; set; }
        public string FileToken { get; set; }
        public string FileSize { get; set; }
    }

    public class FileList
    {
        public List<FileInfo> AList { get; set; }
    }
}
