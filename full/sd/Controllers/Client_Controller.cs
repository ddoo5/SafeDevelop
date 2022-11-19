using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SD.Models.Repositories.Interfaces;
using SD.Models.Requests;
using SD.Services.Interfaces;

namespace SD.Controllers
{
    [Authorize]
    [Route("api/client")]
    [ApiController]
    public class Client_Controller : Controller
    {
        private readonly ILogger<Client_Controller> _logger;
        private readonly IClientService _service;



        public Client_Controller(ILogger<Client_Controller> logger, IClientService service)
        {
            _logger = logger;
            _service = service;
        }



        [HttpPost("create")]
        public IActionResult Create([FromBody] string surname, string name, string patronymic)
        {
            _logger.LogInformation("");

            return Created("", _service.Create(surname, name, patronymic));
        }


        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery] int id)
        {
            _logger.LogInformation("");

            return Ok(_service.GetbyId(id));
        }


        [HttpPost("update")]
        public IActionResult Update([FromBody] ClientRequest client, [FromQuery]ClientRequest newClientData)
        {
            _logger.LogInformation("");

            return Ok(_service.Update(client, newClientData));
        }


        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] int id)
        {
            _logger.LogInformation("");

            return Ok(_service.Delete(id));
        }
    }
}
