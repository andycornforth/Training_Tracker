using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.Security;

namespace Business
{
    public static class Encryption
    {
        public static string GeneratePassword()
        {
            var password = Membership.GeneratePassword(50, 0);
            password = Regex.Replace(password, @"[^a-zA-Z0-9]", m => "");
            return password.Substring(0, 10);
        }

        public static string HashWithSalt(string text)
        {
            var sHashWithSalt = text + "DidierDrogba";
            var saltedHashBytes = Encoding.UTF8.GetBytes(sHashWithSalt);
            var algorithm = new SHA256Managed();
            var hash = algorithm.ComputeHash(saltedHashBytes);
            return Convert.ToBase64String(hash);
        }
    }
}
