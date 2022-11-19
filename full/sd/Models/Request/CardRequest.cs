using System;
using SD_lib.Entity.Models;

namespace SD
{
    public class CardRequest
    {
        public string? CardNumber { get; set; }

        public string? Name { get; set; }

        public int CVV { get; set; }
    }
}

