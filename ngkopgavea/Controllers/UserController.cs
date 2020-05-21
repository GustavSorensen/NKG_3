using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;
using ngkopgavea;
using ngkopgavea.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ngkopgavea.Hubs;
using static BCrypt.Net.BCrypt;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ngkopgavea.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUnitOfWork unit;
        private readonly JsonSerializerSettings serializerSettings = new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects };

        public UserController(IUnitOfWork unit)
        {
            this.unit = unit;
        }

        [HttpPost("login")]
        public async Task<ActionResult> Authenticate([FromBody]UserDTO model)
        {
            var user = await unit.UserRepository.Authenticate(model.Username, model.Password);

            if (user == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult> Register([FromBody] UserDTO userDTO)
        {
            User exist = await unit.UserRepository.Get(userDTO.Username);
            if(exist == null)
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
            return BadRequest(new { message = "User already exists" });
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var users = await unit.UserRepository.Get();
            string json = JsonConvert.SerializeObject(users, Formatting.Indented, serializerSettings);
            return Ok(json);
        }
    }
}
