using System;
using System.Net;
using SD.Models.Repositories.Interfaces;
using SD.Models.Requests;
using SD_lib.DB;
using SD_lib.Entity.Models;

namespace SD.Models.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly SD_libContext _connection;



        public ClientRepository(SD_libContext connection)
        {
            _connection = connection;
        }



        public int Create(Client data)
        {
            _connection.Clients.Add(data);
            _connection.SaveChangesAsync();

            return 1;
        }


        public bool Delete(int id)
        {
            var client = _connection.Clients.Where(x => x.ClientId == id).FirstOrDefault();
            _connection.Clients.Remove(client);
            _connection.SaveChangesAsync();

            return true;
        }


        public Client GetbyId(int id)
        {
            return _connection.Clients.Where(x => x.ClientId == id).FirstOrDefault();
        }


        public bool Update(Client data, Client newData)
        {
            Client updatingPerson = _connection.Clients.Where(x => x.Name == data.Name && x.Surname == data.Surname && x.Patronymic == data.Patronymic).FirstOrDefault();

            if (data.Name != null)
                updatingPerson.Name = newData.Name;

            if (data.Surname != null)
                updatingPerson.Surname = newData.Surname;

            if (data.Patronymic != null)
                updatingPerson.Patronymic = newData.Patronymic;

            _connection.SaveChangesAsync();

            return true;
        }
    }
}

