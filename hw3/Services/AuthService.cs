using System;
using SD.Models.Repositories.Interfaces;
using SD.Models.Request;
using SD.Models.Response;
using SD.Models.Security;
using SD.Repos.IRepository;
using SD.Services.Interfaces;
using SD.Utils.Passwords;
using SD_lib.Entity.Models;
using SD_lib.Tokens.Services.Interfaces;

//add validation for eentered requests

//немного запутался в задании(делать по методичке или по видео станислава?), нам надо создать пользователя в бд либо просто токен для авторизации, либо отдельный 'admin_controller'?(оставил 2 варианта) если токен с пользователем, то надо переписывать классы; валидацию, думаю, сделаю на 7^ом задании(при помощи fluent validator проще, чем руками)

namespace SD.Services
{
    public class AuthService : IAuthService
    {
        private readonly ILogger<AuthService> _logger;
        private readonly IAuthRepository _repository;
        private readonly ITokenService _repositoryToken;



        public AuthService(ILogger<AuthService> logger, IAuthRepository repository, ITokenService repositoryToken)
        {
            _logger = logger;
            _repository = repository;
            _repositoryToken = repositoryToken;
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


        public AccountResponse LogIn(string email, string password)
        {
            var data = _repository.GetByEmail(email);

            if(!PasswordGenerator.VerifyPassoword(password, data.PasswordSalt, data.PasswordHash))
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


        public string LogInByToken(string user, string token)
        {
            var response = _repositoryToken.Authenticate(user, token);

            return response;
        }


    }
}

