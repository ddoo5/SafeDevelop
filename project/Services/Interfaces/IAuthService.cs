using System;
using SD.Models.Request;
using SD.Models.Response;

namespace SD.Services.Interfaces
{
    public interface IAuthService
    {
        string Register(AccountRequest account, string password);
        AccountResponse LogIn(int id, string password);
    }
}

