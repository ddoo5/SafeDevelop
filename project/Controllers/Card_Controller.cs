using Microsoft.AspNetCore.Mvc;
using SD.Models.Repositories.Interfaces;
using SD.Models.Requests;
using SD_lib.Entity.Models;

namespace SD.Controllers
{
    [Route("api/card")]
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
        public IActionResult Create([FromBody] CardRequest card)
        {
            return Ok(_service.Create(card));
        }


        [HttpGet("getbyid")]
        public IActionResult GetById([FromQuery] int cardNumber)
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
        public IActionResult Delete([FromQuery]int cardNumber)
        {
            _logger.LogInformation("");

            return Ok(_service.Delete(cardNumber));
        }
    }
}

