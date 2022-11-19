using System;
namespace SD.Models.Response
{
    public class AccountResponse
    {
        public int AccountId { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }

        public string Email { get; set; }

        public bool IsBanned { get; set; }
    }
}

