using FoltDelivery.Model;
using FoltDelivery.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;


namespace FoltDelivery.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IConfiguration _configuration;

        public UserController(IUserService userService, IConfiguration configuration)
        {
            _userService = userService;
            _configuration = configuration;
        }

        [HttpGet]
        public List<User> Get()
        {
            //return new string[] { "value1", "value2" };
            return _userService.GetAllUsers();
        }

        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate([FromBody] User userDTO)
        {
            var user = _userService.GetUsingCredentials(userDTO.Username, userDTO.Password);

            if (user == null) return Unauthorized();


            //return Ok(new JwtDto()
            //{
            //    User = user,
            //    Token = new
            //    {
            //        token = new JwtSecurityTokenHandler().WriteToken(token),
            //        expires = token.ValidTo
            //    }
            //});
            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(null);
        }

        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
