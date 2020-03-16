using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/sort/[controller]")]
    [ApiController]
    public class ViewTimeSheetController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        // GET: api/sortItems\
        [HttpGet("ID/{empid}", Name = "GetL")]
        [Authorize]
        public IActionResult Get(int empid,string fromDate,string toDate)
        {
            //TimeSheetItem timeSheetItem = new TimeSheetItem();

            //string start1 = timeSheetItem.From;
            // DateTime fdate = Convert.ToDateTime(fromDate);
            // DateTime tDate = Convert.ToDateTime(toDate);
            //string end1 = timeSheetItem.To;
            CultureInfo provider = new CultureInfo("en-US");
            string validformats = "M/d/yyyy";
            DateTime fDate = DateTime.ParseExact(fromDate, validformats, provider);
            DateTime tDate = DateTime.ParseExact(toDate, validformats, provider);
            //DateTime fDate = DateTime.ParseExact(fromDate, validformats, provider);
            //DateTime fDate = DateTime.ParseExact(fromDate, "MM/dd/yyyy", provider);
            //DateTime tDate = DateTime.ParseExact(toDate, "MM/dd/yyyy", provider);
            try
            {
                var q = (from pd in db.TimeSheet
                         join od in db.TimeSheetEntry on pd.EmpId equals od.EmpId
                         join ct in db.TimeSheetItem
                         on new { a = od.EmpId } equals new { a = ct.EmpId }
                         where empid == od.EmpId
                         where fDate >= (DateTime.ParseExact(ct.From, validformats, provider)) &&
                          tDate <= (DateTime.ParseExact(ct.To, validformats, provider))
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
                             pd.Status,
                             ct.To,
                             ct.From,
                             od.Company,
                             ct.Date,
                         }).OrderBy(x => x.Day).Take(20).ToList();



                return Ok(new
                {
                    statusCode = 200,
                    message = "done",
                    data = q
                });

            }

            //HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.OK,q);
            ////return new HttpResponseMessage(HttpStatusCode.OK,HttpReq);
            //// return Request.CreateResponse<string>(HttpStatusCode.OK,q);
            //return response;

            catch (Exception e)
            {
                //  return new HttpResponseMessage(HttpStatusCode.BadRequest);
                return Ok(new
                {
                    Status = 500,
                    Message = e
                });
            }

        }

        // GET: api/sortItems/5
        [HttpGet("{id}")]
        //public string Get(int id)
        //{
        //    return "value";
        //}

        // POST: api/sortItems
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/sortItems/5
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
