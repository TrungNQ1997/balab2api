using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace BAWebLab2.LibCommon
{
    public class LibCommon
    {
        public static string HashMD5(string text)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder hashSb = new StringBuilder();
            foreach (byte b in hash)
            {
                hashSb.Append(b.ToString("X2"));
            }
            return hashSb.ToString();
        }

        public static void WriteLog(string text)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" \n  \n " + DateTime.Now.ToString() + "   " + text);

            File.AppendAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Log/log.txt", sb.ToString());
            sb.Clear();
        }

    }
}
