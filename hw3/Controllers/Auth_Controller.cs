using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SD.Models.Request;
using SD.Services.Interfaces;

namespace SD.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class Auth_Controller : ControllerBase
    {
        private readonly ILogger<Auth_Controller> _logger;
        private readonly IAuthService _service;



        public Auth_Controller(ILogger<Auth_Controller> logger, IAuthService service)
        {
            _logger = logger;
            _service = service;
        }


        [Authorize]
        [HttpPost("register")]
        public IActionResult Register([FromBody]AccountRequest request, [FromQuery]string password)
        {
            var response = _service.Register(request, password);

            return Ok(response);
        }

        [Authorize]
        [HttpGet("login")]
        public IActionResult Login([FromQuery]string email, [FromQuery]string password)
        {
            var response = _service.LogIn(email, password);

            if (response == null)
                return Unauthorized();

            return Ok(response);
        }


        [AllowAnonymous]
        [HttpGet("logintoken")]
        public IActionResult LoginByToken([FromQuery] string user, [FromQuery] string password)
        {
            var response = _service.LogInByToken(user, password);

            if (response == null)
                return Unauthorized();

            return Ok($"Bearer {response}");
        }
    }
}

