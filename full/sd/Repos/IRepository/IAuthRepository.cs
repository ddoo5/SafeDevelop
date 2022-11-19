using System;
using SD.Models.Repositories.Main;
using SD_lib.Entity.Models;

namespace SD.Repos.IRepository
{
    public interface IAuthRepository
    {
        Account GetById(int id);
        int Create(Account data);
        Account GetByEmail(string email);
    }
}

