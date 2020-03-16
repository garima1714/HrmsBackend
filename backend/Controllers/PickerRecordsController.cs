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
    [Route("[controller]")]
    [ApiController]
    public class PickerRecordsController : ControllerBase
    {
        HRMSContext db = new HRMSContext();
        // GET: api/PickerRecords
        [HttpGet]
        [Authorize]
        public IActionResult Get()
        {
            try
            {
                var q = (from pd in db.EditTimesheet
                         select new
                         {
                            pd.EmpId,
                            pd.Customer,
                            pd.Project,
                            pd.Task,
                            pd.Company
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

        // GET: api/PickerRecords/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/PickerRecords
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/PickerRecords/5
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
