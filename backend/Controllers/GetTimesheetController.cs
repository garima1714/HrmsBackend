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
    [Route("TimeSheet/GetTimeSheets")]
    [ApiController]
    public class GetTimesheetController : ControllerBase
    {
        // GET api/values
        HRMSContext db = new HRMSContext();
        [HttpGet]
        //[Authorize]
        public IActionResult Get()
        {
            try
            {
                var q = (from pd in db.TimeSheet
                         join od in db.TimeSheetEntry on pd.EmpId equals od.EmpId
                         join ct in db.TimeSheetItem
                         on new { a = od.EmpId } equals new { a = ct.EmpId }
                         orderby od.EmpId
                         select new
                         {
                             pd.Id,
                             pd.EmpId,
                             pd.EmployeeName,
                             od.Customer,
                             od.Task,
                             od.Project,
                             ct.Day,
                             ct.Hours,
                             od.Status,
                             ct.To,
                             ct.From,
                             od.Company,
                             ct.Date,
                         }).ToList();

                return Ok(new {
                    statusCode = 200,
                    message = "done",
                    data = q
                });
                
                //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,q);
                ////return new HttpResponseMessage(HttpStatusCode.OK,HttpReq);
                //// return Request.CreateResponse<string>(HttpStatusCode.OK,q);
                //return response;
            }
            catch (Exception e)
            {
                //  return new HttpResponseMessage(HttpStatusCode.BadRequest);
                Console.WriteLine(BadRequest(new { error = e }));
                return Ok(new
                {
                    Status = 500,
                    Message = "unauthorized"
                });
            }
        }

        // GET api/values/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<JsonResult> Get(int id)
        {
            // return "value";
            try
            {
                var q = (from pd in db.TimeSheet
                         join od in db.TimeSheetEntry on pd.EmpId equals od.EmpId
                         join ct in db.TimeSheetItem
                         on new { a = od.EmpId } equals new { a = ct.EmpId }
                         orderby od.EmpId
                         select new
                         {
                             pd.EmpId,
                             pd.EmployeeName,
                             od.Customer,
                             od.Task,
                             od.Project,
                             od.Company,
                             ct.Day,
                             ct.Hours,
                         }).ToList();
                return Ok(q);
            }
            catch (Exception e)
            {
                return Ok(BadRequest(new { error = e }));
                // Console.WriteLine(BadRequest(new { error = e }));
            }
        }

        // POST api/values
        [HttpPost]
        [Authorize]
        public void Post(TimeSheetMain value)
        {
            TimeSheetMain ts = new TimeSheetMain();
            int id = GetHashCode();
            ts.tab1 = new TimeSheet();
            ts.tab2 = new TimeSheetEntry();
            ts.tab3 = new TimeSheetItem();
            try
            {
                ts.tab1.EmpId = value.tab1.EmpId;
                ts.tab1.EmployeeName = value.tab1.EmployeeName;
                ts.tab1.Status = value.tab1.Status;
                ts.tab1.Id = id;
                ts.tab2.EmpId = value.tab1.EmpId;
                ts.tab2.Customer = value.tab2.Customer;
                ts.tab2.Project = value.tab2.Project;
                ts.tab2.Task = value.tab2.Task;
                ts.tab2.EmployeeName = value.tab2.EmployeeName;
                ts.tab2.Status = value.tab1.Status;
                ts.tab2.Company = value.tab2.Company;
                ts.tab2.Id = id;
                ts.tab3.EmpId = value.tab1.EmpId;
                ts.tab3.Date = value.tab3.Date;
                ts.tab3.Day = value.tab3.Day;
                ts.tab3.Hours = value.tab3.Hours;
                ts.tab3.From = value.tab3.From;
                ts.tab3.To = value.tab3.To;
                ts.tab3.Id = id;
                db.TimeSheet.Add(ts.tab1);
                db.TimeSheetEntry.Add(ts.tab2);
                db.TimeSheetItem.Add(ts.tab3);
                db.SaveChanges();
            }
            catch (Exception e)
            {
                Console.WriteLine(BadRequest(new { error = e }));
            }

        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
