using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace backend.Custom_Models
{

    //public class AddTimeSheet
    //{
    //    //public string data { get; set; }

    //    public Module Data { get; set; }


    //}

    public class AddTimeSheet
    {

        public string empId { get; set; }
        public string empName { get; set; }
        public DateTime toDate { get; set; }
        public DateTime fromDate { get; set; }
        public string status { get; set; }
        public ICollection<TimeSheetData> Data { get; set; }
    }

    public partial class TimeSheetData
    {
        public string customer { get; set; }
        public string task { get; set; }
        public string project { get; set; }
        public string company { get; set; }
        public DateTime date { get; set; }
        public string submittedto { get; set; }
        public int hours { get; set; }

    }
    //public class  AddTimeSheet
    //{
    //    //public string data { get; set; }

    //    public Module Data { get; set; }


    //}

    //public partial class Module
    //{

    //    public string EmpId { get; set; }
    //    public string EmpName { get; set; }
    //    public DateTime ToDate { get; set; }
    //    public DateTime FromDate { get; set; }
    //    public string Status { get; set; }
    //    public ICollection<TimeSheetData> Data { get; set; }
    //}

    //public partial class TimeSheetData
    //{
    //    public string Customer { get; set; }
    //    public string Task { get; set; }
    //    public string Project { get; set; }
    //    public string Company { get; set; }
    //    public DateTime Date { get; set; }
    //    public string Submittedto { get; set; }
    //    public int Hours { get; set; }

    //}
}
