using System;
using System.Collections.Generic;

namespace backend.Models
{
    public partial class TimeSheetItem
    {
        public int EmpId { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public TimeSpan? Hours { get; set; }

        public TimeSheet Emp { get; set; }
    }
}
