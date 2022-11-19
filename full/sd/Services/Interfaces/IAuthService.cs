using System;
using SD.Models.Request;
using SD.Models.Response;

namespace SD.Services.Interfaces
{
    public interface IAuthService
    {
        string Register(string name, string surname, string email, string password);
        AccountResponse LogIn(string email, string password);
        string LogInByToken(string user, string token);
    }
}

