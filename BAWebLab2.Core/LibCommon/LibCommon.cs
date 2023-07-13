using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace BAWebLab2.LibCommon
{
    /// <summary>class chứa các hàm chung</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class LibCommon
    {
        /// <summary>mã hóa md5 1 chuỗi</summary>
        /// <param name="text">The text.
        /// chuỗi cần mã hóa</param>
        /// <returns>chuỗi đã mã hóa</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
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

        /// <summary>ghi vào file log.txt</summary>
        /// <param name="text">nội dung cần ghi</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public static void WriteLog(string text)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append(" \n  \n " + DateTime.Now.ToString() + "   " + text);

            File.AppendAllText(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + "/Log/log.txt", sb.ToString());
            sb.Clear();
        }

    }
}
