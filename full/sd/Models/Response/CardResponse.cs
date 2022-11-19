using System;
using SD_lib.Entity.Models;

namespace SD
{
    public class CardResponse
    {
        public string CardNumber { get; set; }

        public string? Name { get; set; }

        public int CVV { get; set; }

        public DateTime ExpirationDate { get; set; }

        public virtual Client? Holder { get; set; }
    }
}

