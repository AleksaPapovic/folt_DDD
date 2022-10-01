using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using FoltDelivery.API.Service;
using FoltDelivery.Domain.Aggregates.CustomerAggregate;
using FoltDelivery.API.DTO;
using FoltDelivery.Core.Authorization;


namespace FoltDelivery.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        private readonly IConfiguration _configuration;

        private readonly IJwtUtils _iJwtUtils;

        private readonly IMapper _mapper;

        public UserController(IUserService userService, IConfiguration configuration, IMapper mapper, IJwtUtils iJwtUtils)
        {
            _userService = userService;
            _configuration = configuration;
            _mapper = mapper;
            _iJwtUtils = iJwtUtils;
        }

        [AllowAnonymous]
        [HttpGet]
        public List<User> GetAll()
        {
            return _userService.GetAllUsers();
        }


        [AllowAnonymous]
        [HttpGet]
        [Route("principal")]
        public User GetPrincipal()
        {
            var token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = _iJwtUtils.ValidateJwtToken(token);
            if (userId != null) return _userService.GetById(userId.Value);
            return null;
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        public UserDTO Register(UserDTO userDTO)
        {
            return _mapper.Map<UserDTO>(_userService.Register(userDTO));
        }

        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public AuthenticateResponseDTO Login(AuthenticateRequestDTO authDTO)
        {
            return _userService.Authenticate(authDTO);
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        public IActionResult Logout()
        {
            return Ok(null);
        }

        [AllowAnonymous]
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [AllowAnonymous]
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
