using System;
using System.Security.Cryptography;
using System.Text;
using Castle.DynamicProxy.Generators.Emitters.SimpleAST;

namespace SD.Utils.Passwords
{
    public class PasswordGenerator
    {
        private const string SecretWord = "RS6&6^&6KVeTA7bh7HOI4N$65ZWc0yP%l6B1K8R";

        
        public static (string passwordSalt, string passowrdHash) CreatePasswordHash(string password)
        {
            var randomPassword = GenerateRandomPass();
            byte[] bytes = Encoding.ASCII.GetBytes(randomPassword);

            string passwordSalt = Convert.ToBase64String(bytes);
            string passwordHash = GetPassHash(passwordSalt, password);

            return (passwordSalt, passwordHash);
        }


        public static bool VerifyPassoword(string password, string passwordSalt, string passwordHash)
        {
            return GetPassHash(passwordSalt, password) == passwordHash;
        }


        public static string GetPassHash(string passwordSalt, string password)
        {
            password = $"{password}~{passwordSalt}~{SecretWord}";
            byte[] bytes = Encoding.UTF8.GetBytes(password);

            SHA512 sha512 = new SHA512Managed();
            byte[] passwordHash = sha512.ComputeHash(bytes);

            return Convert.ToBase64String(passwordHash);
        }


        /// <summary>
        /// Code was taken from
        /// </summary>
        /// <see cref="https://github.com/ddoo5/PC">PC</see>
        private static string GenerateRandomPass()
        {
            string password = "";
            int x;
            Random rnd = new();
            int _randomLenght = rnd.Next(20, 40);    //достаточно безопасно?

            List<char> _lowlet = new List<char>();
            _lowlet.AddRange("abcdefghijklmnopqrstuvwxyz");

            List<char> _caplet = new List<char>();
            _caplet.AddRange("ABCDEFGHIJKLMNOPQRSTUVWXYZ");

            List<char> _sign = new List<char>();
            _sign.AddRange("-'^*~@/!#$%&");

            List<char> _numbers = new List<char>();
            _numbers.AddRange("1234567890");

            char RandomLowlet()
            {
                x = rnd.Next(0, _lowlet.Count);
                char c = _lowlet[x];

                return c;
            }

            char RandomCaplet()
            {
                x = rnd.Next(0, _caplet.Count);
                char c = _caplet[x];

                return c;
            }

            char RandomSign()
            {
                x = rnd.Next(0, _sign.Count);
                char c = _sign[x];

                return c;
            }

            char RandomNumber()
            {
                x = rnd.Next(0, _numbers.Count);
                char c = _numbers[x];

                return c;
            }

            char Randomizer(int q)
            {
                char letter = 'x';
                int r = rnd.Next(0, q + 1);

                if (q == 4)
                {
                    r = rnd.Next(0, q - 1);
                    switch (r)
                    {
                        case 0:
                            letter = RandomLowlet();
                            break;
                        case 1:
                            letter = RandomCaplet();
                            break;
                        case 2:
                            letter = RandomSign();
                            break;
                    }
                }
                else
                {
                    switch (r)
                    {
                        case 0:
                            letter = RandomLowlet();
                            break;
                        case 1:
                            letter = RandomCaplet();
                            break;
                        case 2:
                            letter = RandomNumber();
                            break;
                        case 3:
                            letter = RandomSign();
                            break;
                    }
                }
                return letter;
            }

            for (int i = 0; i < _randomLenght; i++)
            {
                char f = Randomizer(3);
                password += f;
            }

            return password;
        }
    }
}

