using System;
using System.Collections.Generic;

namespace backend.Models
{
    public partial class TimeSheet
    {
        public int EmpId { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public int? Id { get; set; }

        public TimeSheetEntry TimeSheetEntry { get; set; }
        public TimeSheetItem TimeSheetItem { get; set; }
    }
}
