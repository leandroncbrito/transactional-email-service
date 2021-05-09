using System;
using System.Security.Cryptography;

namespace TransactionalEmail.Consumer.Helpers
{
    public static class TokenHelper
    {
        public static string Generate()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();

            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);

            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }
}
