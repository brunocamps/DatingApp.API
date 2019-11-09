using System.Threading.Tasks;
using DatingApp.API.Models;
using DatingApp.API.Models.Data;
using Microsoft.AspNetCore.Mvc;

namespace DatingApp.API.Controllers
{

    [Route("api/[controller]")] //Route
    [ApiController] //
    public class AuthController : ControllerBase 
    {
        //inject our newly created repository into this
        private readonly IAuthRepository _repo;
        public AuthController(IAuthRepository repo)
        {
            _repo = repo;

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(string username, string password)
        {
            //validate request

            //convert to lowercase
            username = username.ToLower();
            //check if username is already taken
            if (await _repo.UserExists(username))
                return BadRequest("Username already exists!"); //extended in the class

            //create user
            var userToCreate = new User
            {
                Username = username //all info receiveng 
                //password needs to be created from our repo instaed
            };

            var createdUser = await _repo.Register(userToCreate, password);

            return StatusCode(201);           
        }

    }
}