using System;
using SD.Repos.IRepository;
using SD_lib.DB;
using SD_lib.Entity.Models;

namespace SD.Models.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly SD_libContext _connection;



        public AuthRepository(SD_libContext connection)
        {
            _connection = connection;
        }



        public Account GetById(int id)
        {
            return _connection.Accounts.Where(x => x.AccountId == id).FirstOrDefault();
        }


        public Account GetByEmail(string email)
        {
            return _connection.Accounts.Where(x => x.Email == email).FirstOrDefault();
        }


        public int Create(Account data)
        {
            _connection.Accounts.Add(data);
            _connection.SaveChangesAsync();

            return 1;
        }
    }
}

