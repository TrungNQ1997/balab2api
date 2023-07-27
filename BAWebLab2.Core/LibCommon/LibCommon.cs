using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Collections.Generic;
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

        /// <summary>parse Strings thành  list&lt;long&gt;</summary>
        /// <param name="text">string đầu vào</param>
        /// <returns>list&lt;long&gt; đã parse</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        public static List<long>? StringToListLong(string text)
        {
            var listVehicleID = new List<long>();
            if (!string.IsNullOrEmpty(text))
            {
                listVehicleID = text?.Split(',')?.Select(long.Parse)?.ToList();
            }
            return listVehicleID;
        }
          
    }
}
