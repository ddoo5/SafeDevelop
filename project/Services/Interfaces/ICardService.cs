using System;
using SD.Models.Requests;
using SD.Models.Responses;

namespace SD
{
    public interface ICardService
    {
        public string Create(CardRequest card);

        string Delete(int cardNumber);

        CardResponse GetbyId(int cardNumber);

        string Update(CardRequest client, CardRequest newClient);
    }
}

