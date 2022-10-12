using System;
using SD.Models.Repositories.Interfaces;
using SD.Models.Requests;
using SD.Models.Responses;
using SD.Services.Interfaces;
using SD_lib.Entity.Models;

namespace SD.Services
{
    public class ClientService : IClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IClientRepository _repository;



        public ClientService(ILogger<ClientService> logger, IClientRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        public string Create(ClientRequest client)
        {
            _logger.LogInformation(1, "Method create launched");

            try
            {
                if (client.Name.Length < 50 && client.Surname.Length < 50 && client.Patronymic.Length < 50)
                {
                    if (client.Name != null && client.Surname != null && client.Patronymic != null)
                    {
                            var data = new Client
                            {
                                Name = client.Name,
                                Surname = client.Surname,
                                Patronymic = client.Patronymic
                                //ClientCards = null     todo: add cards
                            };

                            var result = _repository.Create(data);

                            if (result == 1)
                            {
                                _logger.LogInformation(1, "Method create successfully ended");

                                return "client created";
                            }
                    }
                }
                else
                {
                    _logger.LogError("Method create ended with error in data", client.Name, client.Surname, client.Patronymic);

                    return $"Please, check your data, something entered wrong:\n" +
                        $"Name: {client.Name}\n" +
                        $"Surname: {client.Surname}\n" +
                        $"Patronymic: {client.Patronymic}";
                }
            }
            catch(Exception a)
            {
                return $"I caught an exception:\n {a.Message} \n. Try to reload api";
            }

            return "";
        }


        public string Delete(int id)
        {
            _repository.Delete(id);

            return "deleted";
        }


        public ClientResponse GetbyId(int id)
        {
            var client = _repository.GetbyId(id);
            var clientResponse = new ClientResponse
            {
                Surname = client.Surname,
                Name = client.Name,
                Patronymic = client.Patronymic,
                ClientCards = client.ClientCards
            };

            return clientResponse;
        }


        public string Update(ClientRequest client, ClientRequest newData)
        {
            var clientUpdateOld = new Client
            {
                Surname = client.Surname,
                Name = client.Name,
                Patronymic = client.Patronymic
            };

            var clientUpdateNew = new Client
            {
                Surname = newData.Surname,
                Name = newData.Name,
                Patronymic = newData.Patronymic
            };


            _repository.Update(clientUpdateOld, clientUpdateNew);

            return "updated";
        }
    }
}

