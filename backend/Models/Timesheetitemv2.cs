using System;
using System.Collections.Generic;

namespace backend.Models
{
    public partial class Timesheetitemv2
    {
        public string Status { get; set; }
        public string EmpId { get; set; }
        public int TimestampId { get; set; }
        public DateTime Date { get; set; }
        public string Submittedto { get; set; }
        public int Hours { get; set; }
        public DateTime ToDate { get; set; }
        public DateTime FromDate { get; set; }
    }
}
