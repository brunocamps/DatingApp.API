using System.Threading.Tasks;
using DatingApp.API.DTOs;
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
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request

            //convert to lowercase
            userForRegisterDto.Username = userForRegisterDto.Username.ToLower();
            //check if username is already taken
            if (await _repo.UserExists(userForRegisterDto.Username))
                return BadRequest("Username already exists!"); //extended in the class

            //create user
            var userToCreate = new User
            {
                Username = userForRegisterDto.Username //all info receiveng 
                //password needs to be created from our repo instaed
            };

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password );

            return StatusCode(201);           
        }

    }
}