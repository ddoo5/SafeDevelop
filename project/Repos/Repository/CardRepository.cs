using System;
using SD.Models.Repositories.Interfaces;
using SD_lib.DB;
using SD_lib.Entity.Models;

namespace SD.Models.Repositories
{
    public class CardRepository : ICardRepository
    {
        private readonly SD_libContext _connection;



        public CardRepository(SD_libContext connection)
        {
            _connection = connection;
        }



        public int Create(Card data)
        {
            _connection.Cards.Add(data);
            _connection.SaveChangesAsync();

            return 1;
        }


        public bool Delete(int cardNumber)
        {
            var client = _connection.Cards.Where(x => x.CardNumber == cardNumber).FirstOrDefault();
            _connection.Cards.Remove(client);
            _connection.SaveChangesAsync();

            return true;
        }


        public Card GetbyId(int cardNumber)
        {
            return _connection.Cards.Where(x => x.CardNumber == cardNumber).FirstOrDefault();
        }


        public bool Update(Card data, Card newData)
        {
            Card updatingPerson = _connection.Cards.Where(x => x.CardNumber == data.CardNumber && x.Name == data.Name).FirstOrDefault();

            if (data.Name != null)
                updatingPerson.Name = newData.Name;

            if (data.CardNumber != null)
                updatingPerson.CardNumber = newData.CardNumber;

            if (data.CVV != null)
                updatingPerson.CVV = newData.CVV;

            _connection.SaveChangesAsync();

            return true;
        }
    }
}

