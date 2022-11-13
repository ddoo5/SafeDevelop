using System;
namespace SD.Models.Security
{
    public class HashResponse
    {
        public string PasswordHash { get; set; }

        public string PasswordSalt { get; set; }
    }
}

