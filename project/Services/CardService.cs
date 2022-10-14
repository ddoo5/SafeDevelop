using SD.Models.Repositories.Interfaces;
using SD.Models.Responses;
using SD_lib.Entity.Models;

namespace SD.Services
{
    public class CardService : ICardService
    {
        private readonly ILogger<CardService> _logger;
        private readonly ICardRepository _repository;



        public CardService(ILogger<CardService> logger, ICardRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }




        public string Create(CardRequest card)
        {
            _logger.LogInformation(1, "Method create launched");

            try
            {
                        var data = new Card
                        {
                            CardNumber = card.CardNumber,
                            CVV = card.CVV,
                            Name = card.Name,
                            ExpirationDate = DateTime.UtcNow.AddYears(4),
                            //Holder = null    todo: add client
                        };

                        var result = _repository.Create(data);

                        if (result == 1)
                        {
                            _logger.LogInformation(1, "Method create successfully ended");

                            return "client created";
                        }
            }
            catch (Exception a)
            {
                return $"I caught an exception:\n {a.Message} \n. Try to reload api";
            }

            return "";
        }

        public string Delete(int cardNumber)
        {
            _repository.Delete(cardNumber);

            return "deleted";
        }

        public CardResponse GetbyId(int cardNumber)
        {
            var card = _repository.GetbyId(cardNumber);
            var cardResponse = new CardResponse
            {
                CardNumber = card.CardNumber,
                Name = card.Name,
                ExpirationDate = card.ExpirationDate,
                CVV = card.CVV,
                Holder = card.Holder
            };

            return cardResponse;
        }

        public string Update(CardRequest card, CardRequest newCard)
        {
            var clientUpdateOld = new Card
            {
                CardNumber = card.CardNumber,
                Name = card.Name,
                CVV = card.CVV
            };

            var clientUpdateNew = new Card
            {
                CardNumber = newCard.CardNumber,
                Name = newCard.Name,
                CVV = newCard.CVV
            };


            _repository.Update(clientUpdateOld, clientUpdateNew);

            return "updated";
        }
    }
}

