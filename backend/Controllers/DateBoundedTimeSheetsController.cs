using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("TimeSheet/BoundedTimeSheets")]
    [ApiController]
    public class DateBoundedTimeSheetsController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        // GET: api/TimeSheetDetailView
        [HttpGet]
        [Authorize]
        public ActionResult<IEnumerable<string>> Get(int id, string to, string from, string day)
        {
            try
            {
                var itm = db.TimeSheet.Where(e => e.EmpId == id && e.TimeSheetItem.To == to && e.TimeSheetItem.From == from && e.TimeSheetItem.Day == day)
                .Select(a => new
                {
                    a.EmpId,
                    a.EmployeeName,
                    a.TimeSheetItem.To,
                    a.TimeSheetItem.From,
                    a.TimeSheetItem.Day,
                    a.TimeSheetItem.Hours,
                    a.TimeSheetEntry.Customer,
                    a.TimeSheetEntry.Company,
                    a.TimeSheetEntry.Project,
                    a.TimeSheetEntry.Task,
                    a.Status,
                }).ToList();
                Console.WriteLine(itm);
                return Ok(itm);
            }
            catch (Exception e)
            {
                return Ok(BadRequest(new { error = e }));
                //Console.WriteLine(BadRequest(new { error = e }));
            }
        }

        // GET: api/TimeSheetDetailView/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TimeSheetDetailView
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/TimeSheetDetailView/5
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
