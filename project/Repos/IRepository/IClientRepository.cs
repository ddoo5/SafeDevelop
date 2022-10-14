using System;
using SD.Models.Repositories.Main;
using SD_lib.Entity.Models;

namespace SD.Models.Repositories.Interfaces
{
    public interface IClientRepository : IRepository<Client, int>
    {
    }
}

