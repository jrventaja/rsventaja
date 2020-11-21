using RSVentaja.Domain.Repository;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RSVentaja.Repository.Static
{
    public class CrypRepository : ICrypRepository
    {
        private string _sha1Key;

        public CrypRepository(string sha1Key)
        {
            _sha1Key = sha1Key;
        }

        public string SignData(string data)
        {
            var encoding = new UTF8Encoding();
            var keyBytes = encoding.GetBytes(_sha1Key);
            var messageBytes = encoding.GetBytes(data);
            using (var hmacsha1 = new HMACSHA1(keyBytes))
            {
                var hashMessage = hmacsha1.ComputeHash(messageBytes);
                return RemoveSpecialCharacters(Convert.ToBase64String(hashMessage));
            }
        }

        private string RemoveSpecialCharacters(string str)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in str)
            {
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_')
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }
    }
}
