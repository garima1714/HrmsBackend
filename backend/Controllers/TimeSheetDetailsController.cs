using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using backend.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TimeSheetDetailsController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        // GET: api/TimeSheetDetails
        [HttpGet]
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
                    
                    a.TimeSheetItem.Hours,
                    a.TimeSheetEntry.Customer,
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

        // GET: api/TimeSheetDetails/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TimeSheetDetails
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/TimeSheetDetails/5
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
