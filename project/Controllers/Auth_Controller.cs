using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SD.Models.Request;
using SD.Services.Interfaces;

namespace SD.Controllers
{
    [Route("api/auth")]
    public class Auth_Controller : Controller
    {
        private readonly ILogger<Auth_Controller> _logger;
        private readonly IAuthService _service;



        public Auth_Controller(ILogger<Auth_Controller> logger, IAuthService service)
        {
            _logger = logger;
            _service = service;
        }



        [HttpPost("create")]
        public IActionResult Register([FromBody]AccountRequest request, [FromBody]string password)
        {
            return Ok(_service.Register(request, password));
        }


        [HttpGet("getbyid")]
        public IActionResult Login([FromQuery]int id, [FromQuery]string password)
        {
            return Ok(_service.LogIn(id, password));
        }
    }
}

