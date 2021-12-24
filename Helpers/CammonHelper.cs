using System;
using System.Linq;

namespace ProjectTracker_API.Helpers
{
    public static class CammonHelper
    {
        private static readonly Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string RandomInteger(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public static string RandomCode(int stringLength,int intLength)
        {
            return RandomString(stringLength) + RandomInteger(intLength);
        }
    }
}
