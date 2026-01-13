using E_Commerce_APIs.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace E_Commerce_APIs.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpPost("signup", Name = "SignUp New User")]
        public async Task<IActionResult> Signup([FromBody] User)
        {

        }
    }
}
