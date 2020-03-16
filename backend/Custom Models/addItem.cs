using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Custom_Models
{
    public class addItem
    {
        public int EmpId { get; set; }
        public string Customer { get; set; }
        public string Task { get; set; }
        public string Project { get; set; }
        public string Company { get; set; }
        public string Hours { get; set; }

        public string Status { get; set; }

    }
}
