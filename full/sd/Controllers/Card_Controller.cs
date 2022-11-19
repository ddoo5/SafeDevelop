using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SD.Models.Repositories.Interfaces;
using SD.Models.Requests;
using SD.Services.Interfaces;
using SD_lib.Entity.Models;

namespace SD.Controllers
{
    [Authorize]
    [Route("api/card")]
    [ApiController]
    public class Card_Controller : Controller
    {
        private readonly ILogger<Card_Controller> _logger;
        private readonly ICardService _service;



        public Card_Controller(ILogger<Card_Controller> logger, ICardService repository)
        {
            _logger = logger;
            _service = repository;
        }



        [HttpPost("create")]
        public IActionResult Create([FromQuery] string name,[FromQuery] string cardnumber, [FromQuery] int cvv)
        {
            return Created("", _service.Create(cardnumber, cvv, name));
        }


        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery] string cardNumber)
        {
            _logger.LogInformation("");

            return Ok(_service.GetbyId(cardNumber));
        }


        [HttpPost("update")]
        public IActionResult Update([FromBody] CardRequest oldCard, [FromQuery] CardRequest newClientData)
        {
            _logger.LogInformation("");

            return Ok(_service.Update(oldCard, newClientData));
        }


        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery]string cardNumber)
        {
            _logger.LogInformation("");

            return Ok(_service.Delete(cardNumber));
        }
    }
}

