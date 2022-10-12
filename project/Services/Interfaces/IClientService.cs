using System;
using SD.Models.Requests;
using SD.Models.Responses;

namespace SD.Services.Interfaces
{
    public interface IClientService
    {
        public string Create(ClientRequest client);

        string Delete(int id);

        ClientResponse GetbyId(int id);

        string Update(ClientRequest client, ClientRequest newClient);
    }
}

