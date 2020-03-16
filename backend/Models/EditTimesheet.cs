using System;
using System.Collections.Generic;

namespace backend.Models
{
    public partial class EditTimesheet
    {
        public int EmpId { get; set; }
        public string Customer { get; set; }
        public string Project { get; set; }
        public string Task { get; set; }
        public string Company { get; set; }
    }
}
