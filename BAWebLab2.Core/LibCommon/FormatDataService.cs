using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.LibCommon
{
    public class FormatDataService
    {
        private readonly IConfiguration _configuration;

        public FormatDataService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public  double RoundDouble(double? number)
        {
            string setting2 = _configuration is null ? "0" : _configuration["NumberRoundDecimal"];

            if (number is null)
            {
                return 0;
            } else
            {
                return Math.Round((double)number, int.Parse(setting2 is null ? "0": setting2));
            } 
        }

        public decimal RoundDecimal(decimal? number)
        {
            string setting2 = (_configuration)["NumberRoundDecimal"];

            if (number is null)
            {
                return 0;
            }
            else
            {
                return Math.Round((decimal)number, int.Parse(setting2));
            }
        }

        public static string NumberMinuteToStringHour(double? number)
        {
            if (number is null)
            {
                return "";
            } else
            { 
                return (Math.Floor(((double)number / 60))).ToString().PadLeft(2, '0') + ":" + (Math.Ceiling((double)number % 60)).ToString().PadLeft(2, '0');
            }
        }


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
        public static List<long> StringToListLong(string? text)
        {
            var listVehicleID = new List<long>();
            if (!string.IsNullOrEmpty(text))
            {
                listVehicleID = text?.Split(',')?.Select(long.Parse)?.ToList();
            }

            if(listVehicleID is null)
            {
                listVehicleID = new List<long>();
            }

            return listVehicleID;
        }

    }
}
