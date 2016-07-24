using LostHobbit.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LostHobbit.Services
{
    public class Security : ISecurity
    {
        public string Hash(string hash)
        {
            var sha1 = System.Security.Cryptography.SHA1.Create();
            var sha1data = sha1.ComputeHash(Encoding.UTF8.GetBytes(hash));
            return Convert.ToBase64String(sha1data);
        }
    }
}
