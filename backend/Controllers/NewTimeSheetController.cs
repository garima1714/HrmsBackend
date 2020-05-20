using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Custom_Models;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace backend.Controllers
{
    [Route("TimeSheet/AddTimeSheet")]
    [ApiController]
    public class NewTimeSheetController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        // GET: api/addItem
        [HttpGet]
        [Authorize]
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
                             pd.EmpId,
                             od.Customer,
                             od.Task,
                             od.Project,
                             ct.Hours,
                             od.Company,
                         }).ToList();

                return Ok(new
                {
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

        // GET: api/addItem/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/addItem
        [HttpPost]
        //[Authorize]
        public IActionResult Post([FromForm]addvalue value)
         {
            TimeSheet timeSheet = new TimeSheet();
            TimeSheetItem timeSheetItem = new TimeSheetItem();
            TimeSheetEntry timeSheetEntry = new TimeSheetEntry();
            try
            {
                //TimeSheet obj = JsonConvert.DeserializeObject<TimeSheet>(value);

                timeSheet.EmpId = value.EmpId;
                timeSheetItem.Hours = value.Hours;
                timeSheetItem.EmpId = value.EmpId;
                timeSheetEntry.Customer = value.Customer;
                timeSheetEntry.Company = value.Company;
                timeSheetEntry.Task = value.Task;
                timeSheetEntry.Project = value.Project;
                timeSheetEntry.EmpId = value.EmpId;
                timeSheetEntry.Status = "pending";

                timeSheetItem.Date = "null";
                timeSheetItem.From = "null";
                timeSheetItem.To = "null";
                timeSheet.EmployeeName = "null";
                timeSheetEntry.EmployeeName = timeSheet.EmployeeName;
                timeSheet.Id = 1;
                timeSheetEntry.Id = timeSheet.Id;
                timeSheetItem.Id = 1;




                db.TimeSheet.Add(timeSheet);
                db.TimeSheetItem.Add(timeSheetItem);
                db.TimeSheetEntry.Add(timeSheetEntry);
                db.SaveChanges();
                return Ok(new
                {
                    StatusCode = 200,
                    Message = "done",
                  //  data = q
                });
            }
            catch(Exception e)
            {
                Console.WriteLine(e);
                return Ok(new
                {
                    StatusCode = 500,
                    Message = "unauthorized"
                });
            }
        }

        // PUT: api/addItem/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
