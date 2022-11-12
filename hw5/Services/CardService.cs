using System.Security.Principal;
using SD.Models.Repositories.Interfaces;
using SD.Models.Responses;
using SD.Models.Responses.Failures;
using SD.Services.Interfaces;
using SD.Validation.Services;
using SD_lib.Entity.Models;


namespace SD.Services
{
    public class CardService : ICardService
    {
        private readonly ILogger<CardService> _logger;
        private readonly ICardRepository _repository;
        private readonly IValidationServiceCard _validation;



        public CardService(ILogger<CardService> logger, ICardRepository repository, IValidationServiceCard validation)
        {
            _logger = logger;
            _repository = repository;
            _validation = validation;

        }



        public string Create(string cardnumber, int cvv, string name)
        {
            CardRequest card = new CardRequest {
                Name = name,
                CardNumber = cardnumber,
                CVV = cvv
            };

            _logger.LogInformation(1, "Method create launched");

            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(card);

            if (failures.Count == 0)
            {
                try
                {
                        var data = new Card
                        {
                            CardNumber = card.CardNumber.Replace(" ", ""),
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
                    return $"Exception: {a.Message}";
                }
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return s;
        }


        public string Delete(string cardNumber)
        {
            CardRequest card = new() {
                CardNumber = cardNumber.ToString(),
                CVV = 000,
                Name = "AutoField" };
            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(card);

            if (failures.Count == 0)
            {
                try
                {
                    _repository.Delete(Convert.ToInt32(cardNumber.Replace(" ", "")));
                    return "deleted";

                }
                catch (Exception a)
                {
                    return $"Exception: {a.Message}";
                }
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return s;
        }


        public CardResponse GetbyId(string cardNumber)
        {
            CardRequest cardr = new()
            {
                CardNumber = cardNumber,
                CVV = 123,
                Name = "AutoField"
            };
            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(cardr);

            if (failures.Count == 0)
            {
                try
                {
                    var card = _repository.GetbyId(Convert.ToInt32(cardNumber.Replace(" ", "")));
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
                catch (Exception a)
                {
                    string ss = $"Exception: {a.Message}";

                    return new CardResponse { Name = ss, CardNumber = ss, CVV = 000, ExpirationDate = DateTime.UtcNow };
                }
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return new CardResponse { Name = s, CardNumber = s, CVV = 000, ExpirationDate = DateTime.UtcNow};
        }


        public string Update(CardRequest card, CardRequest newCard)
        {
            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(card);

            if (failures.Count == 0)
            {
                IReadOnlyList<IOperationFailure> secondFailures = _validation.ValidateEntity(newCard);

                if (secondFailures.Count == 0)
                {
                    try
                    {
                        var clientUpdateOld = new Card
                        {
                            CardNumber = card.CardNumber.Replace(" ",""),
                            Name = card.Name,
                            CVV = card.CVV
                        };

                        var clientUpdateNew = new Card
                        {
                            CardNumber = newCard.CardNumber.Replace(" ", ""),
                            Name = newCard.Name,
                            CVV = newCard.CVV
                        };


                        _repository.Update(clientUpdateOld, clientUpdateNew);
                        return "updated";

                    }
                    catch (Exception a)
                    {
                        return $"Exception: {a.Message}";
                    }
                }
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return s;
        }
    }
}
