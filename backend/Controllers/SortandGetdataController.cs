using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class SortandGetdataController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        // GET: api/SortandGetdata
        //[HttpGet]
        //public IActionResult Get(int empid)
        //{

        //    //TimeSheetItem timeSheetItem = new TimeSheetItem();

        //    //string start1 = timeSheetItem.From;
        //    //DateTime start = Convert.ToDateTime(start1);
        //    //string end1 = timeSheetItem.To;
        //    //DateTime end = Convert.ToDateTime(end1);
        //    CultureInfo provider = CultureInfo.InvariantCulture;
        //    //DateTime fDate = DateTime.ParseExact(fromDate, "MM/dd/yyyy", provider);
        //    //DateTime tDate = DateTime.ParseExact(toDate, "MM/dd/yyyy", provider);
        //    try
        //    {               
        //            var q = (from pd in db.TimeSheet
        //                     join od in db.TimeSheetEntry on pd.EmpId equals od.EmpId
        //                     join ct in db.TimeSheetItem
        //                     on new { a = od.EmpId } equals new { a = ct.EmpId }
        //                     where empid == od.EmpId
        //                     //where fDate >= (DateTime.ParseExact(ct.From, "MM/dd/yyyy", provider)) &&
        //                      //tDate <= (DateTime.ParseExact(ct.To, "MM/dd/yyyy", provider))
        //                     select new
        //                     {
        //                         pd.Id,
        //                         pd.EmpId,
        //                         pd.EmployeeName,
        //                         od.Customer,
        //                         od.Task,
        //                         od.Project,
        //                         ct.Day,
        //                         ct.Hours,
        //                         pd.Status,
        //                         ct.To,
        //                         ct.From,
        //                         od.Company,
        //                         ct.Date,
        //                     }).OrderBy(x => x.Day).Take(20).ToList();

                

        //        return Ok(new
        //            {
        //                statusCode = 200,
        //                message = "done",
        //                data = q
        //            });
               
        //    }

        //    //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,q);
        //    ////return new HttpResponseMessage(HttpStatusCode.OK,HttpReq);
        //    //// return Request.CreateResponse<string>(HttpStatusCode.OK,q);
        //    //return response;

        //    catch (Exception e)
        //    {
        //        //  return new HttpResponseMessage(HttpStatusCode.BadRequest);
        //        return Ok(new
        //        {
        //            Status = 500,
        //            Message = e
        //        });
        //    }
            
        //}


        //GET: api/SortandGetdata/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/SortandGetdata
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/SortandGetdata/5
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
