using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ngkopgavea.Models;
using ngkopgavea.RepositoryPattern;
using System.Linq;


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

        [HttpGet]
        public IActionResult GetAll()
        {
            var users = unit.UserRepository.Get();
            return Ok(users);
        }
    }
}
