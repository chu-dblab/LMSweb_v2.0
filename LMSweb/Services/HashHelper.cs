using System.Security.Cryptography;
using System.Text;

namespace LMSweb.Services
{
    public static class HashHelper
    {
        /// <summary>
        /// MD5雜湊加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string MD5Hash(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            StringBuilder sb;
            using (MD5 md5 = MD5.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(input);

                //進行MD5雜湊加密
                byte[] encryption = md5.ComputeHash(byteArray);

                sb = new StringBuilder();

                for (int i = 0; i < encryption.Length; i++)
                {
                    //或"X2" /"x2" format each one as a hexadecimal string
                    //X (十六進位格式規範)
                    //https://learn.microsoft.com/zh-tw/dotnet/standard/base-types/standard-numeric-format-strings#hexadecimal-format-specifier-x
                    sb.Append(encryption[i].ToString("x2"));
                }
            }
            return sb.ToString();
        }

        /// <summary>
        /// SHA256雜湊加密
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static string SHA256Hash(string input)
        {
            if (string.IsNullOrEmpty(input)) return string.Empty;
            StringBuilder sb;
            using (SHA256 sha256 = SHA256.Create())
            {
                //將字串轉為Byte[]
                byte[] byteArray = Encoding.UTF8.GetBytes(input);

                //進行SHA256雜湊加密
                byte[] encryption = sha256.ComputeHash(byteArray);

                sb = new StringBuilder();

                foreach (var item in encryption)
                {
                    sb.Append(item.ToString("x2"));
                }
            }
            return sb.ToString();
        }
    }
}
