using System;
using SD.Models.Requests;
using SD.Models.Responses;

namespace SD.Services.Interfaces
{
    public interface ICardService
    {
        public string Create(string cardnumber, int cvv, string name);

        string Delete(string cardNumber);

        CardResponse GetbyId(string cardNumber);

        string Update(CardRequest client, CardRequest newClient);
    }
}

