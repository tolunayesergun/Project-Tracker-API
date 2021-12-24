using System.Security.Cryptography;
using System.Text;

namespace ProjectTracker_API.Helpers
{
    public static class CryptoHelper
    {
        public static string CryptString(string uncryptedString)
        {
            if (uncryptedString == null) return null;
            var crypt = SHA256.Create();

            byte[] array = Encoding.UTF8.GetBytes(uncryptedString);

            array = crypt.ComputeHash(array);

            StringBuilder cryptedString = new StringBuilder();

            foreach (byte ba in array)
            {
                cryptedString.Append(ba.ToString("x2").ToLower());
            }

            return cryptedString.ToString();
        }
    }
}