using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using DatingApp.API.Models.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace DatingApp.API.Controllers
{
   // [A]
   // Route: http://localhost:5000/api/values
    [Authorize] //everything inside must be an authorized request
    [Route("api/[controller]")] //Route
    [ApiController] //
    public class ValuesController : ControllerBase //view support comes frm angular app
    {
        private readonly DataContext _context;

        public ValuesController(DataContext context)
        {
            _context = context;
        }
        //actions inside the controller
        //Purpose: REST API. 
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> GetValues() //IActionResult: returns HTTP
        {
            //throw new Exception("Test Exception"); //then, code below has no chance of being reached
            //return new string[] { "value1", "value2" }; //this is seen in our browser
            var values = await _context.Values.ToListAsync();
            return Ok(values);
        }

        // GET api/values/5
        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetValue(int id)
        {
            var value = await  _context.Values.FirstOrDefaultAsync(x => x.Id == id); //x represents the value we're returning
            return Ok(value);
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
