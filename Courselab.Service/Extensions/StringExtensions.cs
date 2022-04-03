using System.Security.Cryptography;
using System.Text;

namespace Courselab.Service.Extensions
{
    public static class StringExtenstions
    {
        public static string EncodeInSha256(this string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder sOutput = new StringBuilder(bytes.Length);
                for (int i = 0; i < bytes.Length; i++)
                {
                    sOutput.Append(bytes[i].ToString("X2"));
                }
                return sOutput.ToString();
            }
        }
    }
}
