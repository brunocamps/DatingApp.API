using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{
   // [A]
   // Route: http://localhost:5000/api/values
    [Route("api/[controller]")] //Route
    [ApiController]
    public class ValuesController : ControllerBase
    {
        //actions inside the controller
        //Purpose: REST API. 
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            //throw new Exception("Test Exception"); //then, code below has no chance of being reached
            return new string[] { "value1", "value2" }; //this is seen in our browser
        }

        // GET api/values/5
        
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
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
