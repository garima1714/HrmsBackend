using System;
using System.Collections.Generic;

namespace backend.Models
{
    public partial class TimeSheetItem
    {
        public int EmpId { get; set; }
        public string Date { get; set; }
        public string Day { get; set; }
        public string Hours { get; set; }
        public string To { get; set; }
        public string From { get; set; }
        public int? Id { get; set; }

        public TimeSheet Emp { get; set; }
    }
}
