using System.Security.Cryptography;
using System.Text;

namespace TAD_Security
{
    public static class TAD_Security
    {
        public static string GetHash(this string pass)
        {
            pass += "@194375qpalzm";
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] data = Encoding.ASCII.GetBytes(pass);
            data = x.ComputeHash(data);
            string encPass = Encoding.ASCII.GetString(data);
            return encPass;
        }
    }
}