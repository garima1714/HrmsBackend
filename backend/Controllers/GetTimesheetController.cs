using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace backend.Controllers
{
    
    [ApiController]
    public class GetTimesheetController : ControllerBase
    {
        // GET api/values
        HRMSContext db = new HRMSContext();
        [Route("sheet")]
        [HttpGet]
        public IActionResult Get([FromQuery]string empid)
        {
            try
            {
                var draft = 0;
                var pending = 0;
                var approved = 0;
                var rejected = 0;
                Timesheetv2 employe = new Timesheetv2();
                var empExist = db.Timesheetv2.FirstOrDefault(item => item.EmpId == empid);
                if (empExist != null)
                {
                    var list = (from item in db.Timesheetv2
                                join timeSheetItem in db.Timesheetitemv2
                                on empid equals timeSheetItem.EmpId
                                from employee in db.Timesheetv2
                                where timeSheetItem.EmpId == employee.EmpId
                                select new
                                {
                                    employee.EmpName,
                                    timeSheetItem.EmpId,
                                    timeSheetItem.Status,
                                    timeSheetItem.FromDate,
                                    timeSheetItem.ToDate,
                                }).Distinct().OrderBy(d => d.FromDate); 
                    foreach (var i in list)
                    {
                        if (i.Status == "draft")
                        {
                            draft++;
                        }
                        else if (i.Status == "pending")
                        {
                            pending++;
                        }
                        else if (i.Status == "approved")
                        {
                            approved++;
                        }
                        else if (i.Status == "rejected")
                        {
                            rejected++;
                        }
                    }
                    IDictionary<string, int> count = new Dictionary<string, int>
                {
                    { "draft", draft },
                    { "submitted", rejected },
                    { "pending", pending },
                    { "approved", approved }
                };
                    return Ok(new
                    {
                        statusCode = 200,
                        message = "done",
                        data = list,

                    });
                }
                return BadRequest(new {
                    statusCode = 400,
                    message = "error",
                    data = "string",
                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    Status = 500,
                    Message = e
                });
            }
        }

        [Route("sheet/inbound")]
        [HttpGet]
        public IActionResult GetInBound([FromQuery]string empid,DateTime fromDate)
        {
            
            try
            {
                var draft = 0;
                var pending = 0;
                var approved = 0;
                var rejected = 0;
                Timesheetitemv2 employe = new Timesheetitemv2();
                var empExist = db.Timesheetv2.FirstOrDefault(item => item.EmpId == empid);
                if (empExist != null)
                {
                    var singleItem = db.Timesheetitemv2.FirstOrDefault(a => ((a.EmpId == empid)&&(a.FromDate == fromDate)));
                    var list = (from timeSheetItem in db.Timesheetitemv2
                                join data in db.Timesheetentryv2
                                on timeSheetItem.TimestampId equals data.Timestampid
                                where timeSheetItem.FromDate == fromDate
                                select new
                                {

                                    timeSheetItem.Status,
                                    timeSheetItem.FromDate,
                                    timeSheetItem.ToDate,
                                    timeSheetItem.Date,
                                    data.Company,
                                    data.Customer,
                                    data.Project,
                                    data.Task,
                                    timeSheetItem.Hours,
                                    timeSheetItem.EmpId
                                });
                    var status = singleItem.Status;
                   
                    foreach (var i in list)
                    {
                        if (i.Status == "draft")
                        {
                            draft++;
                        }
                        else if (i.Status == "pending")
                        {
                            pending++;
                        }
                        else if (i.Status == "approved")
                        {
                            approved++;
                        }
                        else if (i.Status == "rejected")
                        {
                            rejected++;
                        }
                    }
                    IDictionary<string, int> count = new Dictionary<string, int>
                {
                    { "draft", draft },
                    { "submitted", rejected },
                    { "pending", pending },
                    { "approved", approved }
                };
                    return Ok(new
                    {
                        statusCode = 200,
                        message = "done",
                        status,
                        empId = empExist.EmpId,
                        empName = empExist.EmpName,
                        data = list,

                    });
                }
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Error",
                    empId = empExist.EmpId,
                    empName = empExist.EmpName,
                    data = "string",

                });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    data = "string",
                });
            }
        }

        [Route("sheet/approvals")]
        [HttpGet]
        public IActionResult ApprovalsList([FromQuery]string empid)
        {
            try
            {
                    var list = (from item in db.Timesheetv2
                                join timeSheetItem in db.Timesheetitemv2
                                on empid equals timeSheetItem.Submittedto
                                from employee in db.Timesheetv2
                                where ((timeSheetItem.EmpId== employee.EmpId)&&(timeSheetItem.Status=="pending")|| (timeSheetItem.EmpId == employee.EmpId) && (timeSheetItem.Status == "approved")|| (timeSheetItem.EmpId == employee.EmpId) && (timeSheetItem.Status == "rejected"))
                                select new
                                {
                                    employee.EmpName,
                                    timeSheetItem.EmpId,
                                    timeSheetItem.Status,
                                    timeSheetItem.FromDate,
                                    timeSheetItem.ToDate,
                                }).Distinct();
          
                    return Ok(new
                    {
                        statusCode = 200,
                        message = "done",
                        data = list,

                    });
            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Error",
                    data = "string",
                });
            }
        }


        [Route("sheet/decision")]
        [HttpPut]
        public IActionResult ApproveOrReject([FromQuery]string empid, DateTime fromDate,  string option)
        {
            try
            {
                if (option == "approve")
                {
                    List<Timesheetitemv2> list = (from p in db.Timesheetitemv2
                                where ((p.EmpId == empid) && (p.FromDate == fromDate))
                                select p).ToList();
                    foreach(Timesheetitemv2 item in list)
                    {
                        item.Status = "approved";
                    }
                    db.SaveChanges();
                    return Ok(new
                    {
                        statusCode = 200,
                        message = "done",
                    });
                }
                else
                {
                    List<Timesheetitemv2> list = (from p in db.Timesheetitemv2
                                                  where ((p.EmpId == empid) && (p.FromDate == fromDate))
                                                  select p).ToList();
                    foreach (Timesheetitemv2 item in list)
                    {
                        item.Status = "rejected";
                    }
                    db.SaveChanges();
                    db.SaveChanges();
                    return Ok(new
                    {
                        statusCode = 200,
                        message = "done",
                    });
                }

            }
            catch (Exception e)
            {
                return BadRequest(new
                {
                    statusCode = 400,
                    message = "Error",
                    data = "string",
                });
            }
        }
        //[HttpGet]
        ////[Authorize]
        //public IActionResult Get()
        //{
        //    try
        //    {
        //        var q = (from pd in db.TimeSheet
        //                 join od in db.TimeSheetEntry on pd.EmpId equals od.EmpId
        //                 join ct in db.TimeSheetItem
        //                 on new { a = od.EmpId } equals new { a = ct.EmpId }
        //                 orderby od.EmpId
        //                 select new
        //                 {
        //                     pd.Id,
        //                     pd.EmpId,
        //                     pd.EmployeeName,
        //                     od.Customer,
        //                     od.Task,
        //                     od.Project,
        //                     ct.Day,
        //                     ct.Hours,
        //                     od.Status,
        //                     ct.To,
        //                     ct.From,
        //                     od.Company,
        //                     ct.Date,
        //                 }).ToList();

        //        return Ok(new {
        //            statusCode = 200,
        //            message = "done",
        //            data = q
        //        });

        //        //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,q);
        //        ////return new HttpResponseMessage(HttpStatusCode.OK,HttpReq);
        //        //// return Request.CreateResponse<string>(HttpStatusCode.OK,q);
        //        //return response;
        //    }
        //    catch (Exception e)
        //    {
        //        //  return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //        Console.WriteLine(BadRequest(new { error = e }));
        //        return Ok(new
        //        {
        //            Status = 500,
        //            Message = "unauthorized"
        //        });
        //    }
        //}

        // GET api/values/5
        //[HttpGet("{id}")]
        //[Authorize]
        //public ActionResult<JsonResult> Get(int id)
        //{
        //    // return "value";
        //    try
        //    {
        //        var q = (from pd in db.TimeSheet
        //                 join od in db.TimeSheetEntry on pd.EmpId equals od.EmpId
        //                 join ct in db.TimeSheetItem
        //                 on new { a = od.EmpId } equals new { a = ct.EmpId }
        //                 orderby od.EmpId
        //                 select new
        //                 {
        //                     pd.EmpId,
        //                     pd.EmployeeName,
        //                     od.Customer,
        //                     od.Task,
        //                     od.Project,
        //                     od.Company,
        //                     ct.Day,
        //                     ct.Hours,
        //                 }).ToList();
        //        return Ok(q);
        //    }
        //    catch (Exception e)
        //    {
        //        return Ok(BadRequest(new { error = e }));
        //        // Console.WriteLine(BadRequest(new { error = e }));
        //    }
        //}

        //// POST api/values
        //[HttpPost]
        //[Authorize]
        //public void Post(TimeSheetMain value)
        //{
        //    TimeSheetMain ts = new TimeSheetMain();
        //    int id = GetHashCode();
        //    ts.tab1 = new TimeSheet();
        //    ts.tab2 = new TimeSheetEntry();
        //    ts.tab3 = new TimeSheetItem();
        //    try
        //    {
        //        ts.tab1.EmpId = value.tab1.EmpId;
        //        ts.tab1.EmployeeName = value.tab1.EmployeeName;
        //        ts.tab1.Status = value.tab1.Status;
        //        ts.tab1.Id = id;
        //        ts.tab2.EmpId = value.tab1.EmpId;
        //        ts.tab2.Customer = value.tab2.Customer;
        //        ts.tab2.Project = value.tab2.Project;
        //        ts.tab2.Task = value.tab2.Task;
        //        ts.tab2.EmployeeName = value.tab2.EmployeeName;
        //        ts.tab2.Status = value.tab1.Status;
        //        ts.tab2.Company = value.tab2.Company;
        //        ts.tab2.Id = id;
        //        ts.tab3.EmpId = value.tab1.EmpId;
        //        ts.tab3.Date = value.tab3.Date;
        //        ts.tab3.Day = value.tab3.Day;
        //        ts.tab3.Hours = value.tab3.Hours;
        //        ts.tab3.From = value.tab3.From;
        //        ts.tab3.To = value.tab3.To;
        //        ts.tab3.Id = id;
        //        db.TimeSheet.Add(ts.tab1);
        //        db.TimeSheetEntry.Add(ts.tab2);
        //        db.TimeSheetItem.Add(ts.tab3);
        //        db.SaveChanges();
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(BadRequest(new { error = e }));
        //    }

        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
