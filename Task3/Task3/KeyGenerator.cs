using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Task3
{
    static class KeyGenerator
    {
        public static byte[] GenerateHMACKey()
        {
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] HMACKey = new byte[16];
                rng.GetBytes(HMACKey);
                return HMACKey;
            }
        }

        public static byte[] GenerateHMACSHA256(byte[] HMACKey, byte[] salt)
        {
            using (HMACSHA256 hasher = new HMACSHA256(HMACKey))
            {
                return hasher.ComputeHash(salt);
            }
        }

        public static string CreateStrFromByteArr(byte[] array) => string.Concat(Array.ConvertAll(array, b => b.ToString("X2")));
       
    }
}
