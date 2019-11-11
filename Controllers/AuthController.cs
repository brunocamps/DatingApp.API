using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DatingApp.API.DTOs;
using DatingApp.API.Models;
using DatingApp.API.Models.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DatingApp.API.Controllers
{   
    //[Authorize] //check
    [Route("api/[controller]")] //Route
    [ApiController] //
    public class AuthController : ControllerBase
    {
        //inject our newly created repository into this
        private readonly IAuthRepository _repo;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository repo, IConfiguration config)
        {
            _config = config;
            _repo = repo;

        }
        //[AllowAnonymous] //check
        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        {
            //validate request -> already through [APIController]

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

            var createdUser = await _repo.Register(userToCreate, userForRegisterDto.Password);

            return StatusCode(201);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForRegisterDto userForLoginDto)
        {
            //1. Checks if the username and pwd matches the one in the db
            var userFromRepo = await _repo.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);


            //check if there's anything inside our userfromRepo
            if (userFromRepo == null)
                return Unauthorized();

            //token is going to contain:
            //user's id and user's username. 

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userFromRepo.Id.ToString()),//specify NameIdentifier as userFromRepo.Id
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            //sign the token to see if it's valid 
            //sign the key
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            //Now that we have our key, we can generate our signing credentials

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1), //24 hours expiration    
                SigningCredentials = creds

            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor); //pass in the tokenDescriptor

            return Ok(new {

                token = tokenHandler.WriteToken(token)

            });
            //14:48




        }

    }
}