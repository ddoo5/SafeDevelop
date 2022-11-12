using System;
using System.Security.Principal;
using SD.Models.Repositories.Interfaces;
using SD.Models.Request;
using SD.Models.Response;
using SD.Models.Responses.Failures;
using SD.Models.Security;
using SD.Repos.IRepository;
using SD.Services.Interfaces;
using SD.Utils.Passwords;
using SD.Validation.Services;
using SD_lib.Entity.Models;
using SD_lib.Tokens.Services.Interfaces;



namespace SD.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IAuthRepository _repository;
        private readonly ITokenService _repositoryToken;
        private readonly IValidationServiceAccount _validation;




        public AuthService(ILogger<AuthService> logger, IAuthRepository repository, ITokenService repositoryToken, IValidationServiceAccount validation)
        {
            _logger = logger;
            _repository = repository;
            _repositoryToken = repositoryToken;
            _validation = validation;
        }



        public string Register(string name, string surname, string email, string password)
        {
            AccountRequest account = new AccountRequest {
                Password = password,
                Name = name,
                Surname = surname,
                Email = email
            };
            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(account);

            if (failures.Count == 0)
            {
                try
                {
                    HashResponse passData = new()
                    {
                        PasswordSalt = PasswordGenerator.CreatePasswordHash(password).passwordSalt
                    };
                    passData.PasswordHash = PasswordGenerator.GetPassHash(passData.PasswordSalt, password);


                    Account data = new()
                    {
                        Email = account.Email,
                        Name = account.Name,
                        Surname = account.Surname,
                        IsBanned = false,
                        PasswordHash = passData.PasswordHash,
                        PasswordSalt = passData.PasswordSalt,
                    };

                    _repository.Create(data);

                    return "created";
                }
                catch(Exception a)
                {
                    return $"Exception: {a.Message}";
                }
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return s;
        }


        public AccountResponse LogIn(string email, string password)
        {
            AccountRequest account = new() {
                Email= email,
                Password = password,
                Name="AutoFill",
                Surname= "AutoFill" };

            IReadOnlyList<IOperationFailure> failures = _validation.ValidateEntity(account);


            if (failures.Count == 0)
            {
                try
                {
                    var data = _repository.GetByEmail(email);

                    if (!PasswordGenerator.VerifyPassoword(password, data.PasswordSalt, data.PasswordHash))
                    {
                        return null;
                    }

                    AccountResponse response = new AccountResponse()
                    {
                        AccountId = data.AccountId,
                        Name = data.Name,
                        Surname = data.Surname,
                        Email = data.Email,
                        IsBanned = data.IsBanned
                    };

                    return response;
                }
                catch(Exception a)
                {
                    return new AccountResponse
                    {                          //unusual solution
                        Name = $"Exception: {a.Message}",
                        Surname = $"Exception: {a.Message}",
                        Email = $"Exception: {a.Message}",
                        IsBanned = false };
                }
            }

            var s = string.Join("\n", failures.Select(e => $"{e.Code}   {e.Description}\n"));
            return new AccountResponse {
                Name = s,
                Surname = s,
                Email = s,
                IsBanned = false };
        }


        public string LogInByToken(string user, string token)
        {
            var response = _repositoryToken.Authenticate(user, token);

            return response;
        }


    }
}

