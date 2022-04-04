using System.Security.Cryptography;
using System.Text;

namespace RatepAPI.Class
{
    class Security
    {
        public static String StringToSHA256(String value)
        {
            StringBuilder Sb = new StringBuilder();

            using (SHA256 hash = SHA256Managed.Create())
            {
                Encoding enc = Encoding.UTF8;
                Byte[] result = hash.ComputeHash(enc.GetBytes(value));

                foreach (Byte b in result)
                    Sb.Append(b.ToString("x2"));
            }

            return Sb.ToString();
        }

        public static string GenerateToken(string login, string password)
        {
            string CryptLogin = StringToSHA256(login);
            string CryptPassword = StringToSHA256(password);
            string CryptDate = StringToSHA256(DateTime.Now.ToString("dd.MM.yyyy"));

            return $"{CryptLogin}{CryptPassword}{CryptDate}";
        }
    }
}
