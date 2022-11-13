using System;
using SD.Models.Repositories.Interfaces;
using SD.Models.Request;
using SD.Models.Response;
using SD.Models.Security;
using SD.Repos.IRepository;
using SD.Services.Interfaces;
using SD.Utils.Passwords;
using SD_lib.Entity.Models;

namespace SD.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IAuthRepository _repository;



        public AuthService(ILogger<AuthService> logger, IAuthRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }



        public AccountResponse LogIn(int id, string password)
        {
            var data = _repository.GetById(id);

            if(!PasswordGenerator.VerifyPassoword(password, data.PasswordSalt, data.PasswordHash))
            {
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

            throw new Exception("password not corrected");
        }

        public string Register(AccountRequest account, string password)
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
    }
}

