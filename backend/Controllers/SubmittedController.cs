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
    public class SubmittedController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        // GET: api/Submitted
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            try
            {
                var itm = db.TimeSheet.Where(e => e.Status == "Submitted")
                .Select(a => new {
                    a.EmpId,
                    a.EmployeeName,
                    a.TimeSheetItem.Day,
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

        // GET: api/Submitted/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Submitted
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Submitted/5
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
