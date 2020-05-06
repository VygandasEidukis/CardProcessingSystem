using System.Security.Cryptography;
using System.Text;
using System.Windows.Data;

namespace Server.Model
{
    public class RandomTextGenerator
    {
        /// <summary>
        /// Generate random array of characters
        /// </summary>
        /// <param name="length">1-8 text length</param>
        /// <returns>randomly generated string with <param name="length"/> size</returns>
        public static string Generate(int length)
        {
            if(length < 1 || length > 8)
                throw new ValueUnavailableException();

            char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
            byte[] data = new byte[length];

            using (RNGCryptoServiceProvider crypto = new RNGCryptoServiceProvider())
            {
                crypto.GetBytes(data);
            }

            StringBuilder result = new StringBuilder(length);
            foreach (byte b in data)
            {
                result.Append(chars[b % (chars.Length)]);
            }

            return result.ToString();
        }
    }
}
