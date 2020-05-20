using System;
using System.Collections.Generic;

namespace backend.Models
{
    public partial class Timesheetentryv2
    {
        public string Customer { get; set; }
        public string Task { get; set; }
        public int Id { get; set; }
        public string Project { get; set; }
        public string Company { get; set; }
        public int Timestampid { get; set; }
    }
}
