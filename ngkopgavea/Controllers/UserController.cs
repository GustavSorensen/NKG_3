using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ngkopgavea.Models;
using static BCrypt.Net.BCrypt;
using Microsoft.AspNetCore.Authorization;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ngkopgavea.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UnitOfWork unit;

        public UserController(UnitOfWork unit)
        {
            this.unit = unit;
        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]UserDTO model)
        {
            var user = unit.UserRepository.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }
        [HttpPost]
        public async Task<ActionResult<UserDTO>> Register([FromBody] UserDTO userDTO)
        {
            bool exist = await unit.UserRepository.Get(userDTO.Username);
            if(!exist)
            {
                var user = new User()
                {
                    Username = userDTO.Username,
                    Password = HashPassword(userDTO.Password),
                };
                userDTO.Password = user.Password;
                await unit.UserRepository.Add(user);
                return Ok(userDTO);
            }
            return BadRequest("User already exists");
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = unit.UserRepository.Get();
            return Ok(users);
        }
    }
}
