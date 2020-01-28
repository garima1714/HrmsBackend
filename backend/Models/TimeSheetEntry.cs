using System;
using System.Collections.Generic;

namespace backend.Models
{
    public partial class TimeSheetEntry
    {
        public int EmpId { get; set; }
        public string Customer { get; set; }
        public string Task { get; set; }
        public string Project { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }

        public TimeSheet Emp { get; set; }
    }
}
