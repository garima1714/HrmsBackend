using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using backend.Models;
using backend.Custom_Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net;

namespace backend.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    public class SubmitTimeSheetController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        [Route("save/sheet")]
        [HttpPost]
        public IActionResult Save(AddTimeSheet items,[FromQuery] string save) 
        {
            //Module items = itemsData.Data;
            //var itemsObject = await request.Content.;
            //AddTimeSheet items = JsonConvert.DeserializeObject<AddTimeSheet>(request);
            try
            {
                Timesheetv2 timeSheet = new Timesheetv2();
                var existingEmployeee = db.Timesheetv2.FirstOrDefault(item => items.empId == item.EmpId);
                if (existingEmployeee == null)
                {
                    timeSheet.EmpId = items.empId;
                    timeSheet.EmpName = items.empName;
                    db.Add(timeSheet);
                    db.SaveChanges();
                }
                foreach (TimeSheetData value in items.Data)
                {
                    Timesheetitemv2 timeSheetItem = new Timesheetitemv2();
                    Timesheetentryv2 timeSheetEntry = new Timesheetentryv2();
                    timeSheetItem.Hours = value.hours;
                    timeSheetItem.EmpId = items.empId;
                    timeSheetItem.Status = save;
                    timeSheetItem.Submittedto = value.submittedto;
                    timeSheetItem.ToDate = items.toDate;
                    timeSheetItem.FromDate = items.fromDate;
                    timeSheetItem.Date = value.date;
                    db.Add(timeSheetItem);
                    db.SaveChanges();
                    int index = timeSheetItem.TimestampId;

                    timeSheetEntry.Customer = value.customer;
                    timeSheetEntry.Company = value.company;
                    timeSheetEntry.Task = value.task;
                    timeSheetEntry.Project = value.project;
                    timeSheetEntry.Timestampid = index;

                    db.Add(timeSheetEntry);
                    db.SaveChanges();
                }
                return Ok(new {value = 200 });
            }
            catch (Exception e)
            {
                Console.Write(e);
                var s = e;
                return BadRequest(new {error = e });
            }
            
        }

        //[Route("submit/sheets")]
        //[HttpPost]
        //public IActionResult submit([FromHeader] Entry cred, [FromBody] AddTimeSheet value)
        //{
        //    try
        //    {
        //        Timesheetv2 timeSheet = new Timesheetv2();
        //        Timesheetitemv2 timeSheetItem = new Timesheetitemv2();
        //        Timesheetentryv2 timeSheetEntry = new Timesheetentryv2();

        //        var existingEmployeee = db.Timesheetv2.FirstOrDefault(item => cred.EmpId == item.EmpId);
        //        if (existingEmployeee == null)
        //        {
        //            timeSheet.EmpId = cred.EmpId;
        //            timeSheet.EmpName = cred.EmpName;
        //            db.Add(timeSheet);
        //            db.SaveChanges();
        //        }

        //        timeSheetItem.Hours = value.Hours;
        //        timeSheetItem.EmpId = cred.EmpId;
        //        timeSheetItem.Status = "Submitted";
        //        timeSheetItem.Submittedto = value.Submittedto;
        //        timeSheetItem.ToDate = cred.ToDate;
        //        timeSheetItem.FromDate = cred.FromDate;
        //        timeSheetItem.Date = value.Date;
        //        db.Add(timeSheetItem);
        //        db.SaveChanges();
        //        int index = timeSheetItem.TimestampId;

        //        timeSheetEntry.Customer = value.Customer;
        //        timeSheetEntry.Company = value.Company;
        //        timeSheetEntry.Task = value.Task;
        //        timeSheetEntry.Project = value.Project;
        //        timeSheetEntry.Timestampid = index;

        //        db.Add(timeSheetEntry);
        //        db.SaveChanges();


        //    }
        //    catch (Exception e)
        //    {
        //        Console.Write(e);
        //    }
        //    return Ok();
        //}
    }
}