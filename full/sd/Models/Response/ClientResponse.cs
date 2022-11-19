using System;
using SD_lib.Entity.Models;

namespace SD.Models.Responses
{
    public class ClientResponse
    {
        public string Surname { get; set; } = null;
        public string Name { get; set; } = null;
        public string Patronymic { get; set; } = null;
        public virtual ICollection<Card> ClientCards { get; set; } = new HashSet<Card>();
    }
}

