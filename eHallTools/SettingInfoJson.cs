using System;
using System.Collections.Generic;
using System.Text;

namespace eHallTools
{
    public class Setting
    {
        public bool RememberPassword { get; set; }
        public bool AutoLogin { get; set; }
        public int? SelectedUniversityIndex { get; set; }
        public string StudentId { get; set; }
        public string Password { get; set; }
    }
}
