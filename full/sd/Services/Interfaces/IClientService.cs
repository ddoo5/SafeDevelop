using System;
using SD.Models.Requests;
using SD.Models.Responses;

namespace SD.Services.Interfaces
{
    public interface IClientService
    {
        public string Create(string surname, string name, string patronymic);

        string Delete(int id);

        ClientResponse GetbyId(int id);

        string Update(ClientRequest client, ClientRequest newClient);
    }
}

