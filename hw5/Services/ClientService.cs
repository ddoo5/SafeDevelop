using System;
using SD.Models.Repositories.Interfaces;
using SD.Models.Requests;
using SD.Models.Responses;
using SD.Models.Responses.Failures;
using SD.Services.Interfaces;
using SD.Validation.Services;
using SD_lib.Entity.Models;


namespace SD.Services
{
    public class ClientService : IClientService
    {
        private readonly ILogger<ClientService> _logger;
        private readonly IClientRepository _repository;
        private readonly IValidationServiceClient _validation;



        public ClientService(ILogger<ClientService> logger, IClientRepository repository, IValidationServiceClient validation)
        {
            _logger = logger;
            _repository = repository;
            _validation = validation;
        }



        public string Create(string surname, string name, string patronymic)     //add validation
        {
            _logger.LogInformation(1, "Method create launched");

            ClientRequest client = new ClientRequest {
            Name = name,
            Surname = surname,
            Patronymic = patronymic
            };

            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(client);

            if (failures.Count == 0)
            {
                try
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
                catch (Exception a)
                {
                    return $"I caught an exception:\n {a.Message} \n. Try to reload api";
                }
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return s;
        }


        public string Delete(int id)
        {
            var client = _repository.GetbyId(id);

            ClientRequest clientRequest = new ClientRequest
            {
                Name = client.Name,
                Surname = client.Surname,
                Patronymic = client.Patronymic
            };

            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(clientRequest);

            if (failures.Count == 0)
            {
                _repository.Delete(id);
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return s;
        }


        public ClientResponse GetbyId(int id)
        {
            var client = _repository.GetbyId(id);

            ClientRequest clientRequest = new ClientRequest
            {
                Name = client.Name,
                Surname = client.Surname,
                Patronymic = client.Patronymic
            };

            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(clientRequest);

            if (failures.Count == 0)
            {
                var clientResponse = new ClientResponse
                {
                    Surname = client.Surname,
                    Name = client.Name,
                    Patronymic = client.Patronymic,
                    ClientCards = client.ClientCards
                };

                return clientResponse;
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return new ClientResponse { Name = s, Patronymic = s, Surname = s};
        }


        public string Update(ClientRequest client, ClientRequest newData)     //add validation
        {
            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(client);

            if (failures.Count == 0)
            {
                IReadOnlyList<IOperationFailure> secondFailures = _validation.ValidateEntity(newData);

                if (secondFailures.Count == 0)
                {
                    try
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

