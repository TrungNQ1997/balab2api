using BAWebLab2.Model;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace BAWebLab2.Core.LibCommon
{
    /// <summary>class format các kiểu dữ liệu</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/2/2023 created
    /// </Modified>
    public class FormatDataHelper
    {
        private readonly IConfiguration _configuration;

        public FormatDataHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>làm tròn số theo cấu hình config trong appsettings.json</summary>
        /// <param name="number">số cần làm tròn</param>
        /// <returns>số đã làm tròn</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/2/2023 created
        /// </Modified>
        public double RoundDouble(double? number)
        {
            double result = 0;
            string? setting2 = _configuration["NumberRoundDecimal"];
            if (number is not null) 
            {
                result = Math.Round((double)number, int.Parse(setting2 is null ? "0" : setting2));
            }
            return result;
        }

        /// <summary>đổi số phút thành định dạng giờ hh:mm</summary>
        /// <param name="number">số phút</param>
        /// <returns>string định dạng hh:mm</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/2/2023 created
        /// </Modified>
        public static string NumberMinuteToStringHour(double? number)
        {
            string stringReturn = "";
            if (number is not null)
            {
                stringReturn = (Math.Floor(((double)number / 60))).ToString().PadLeft(2, '0') + ":" + (Math.Ceiling((double)number % 60)).ToString().PadLeft(2, '0');
            }
            if (stringReturn == "00:00")
            {
                stringReturn = "";
            }
            return stringReturn;
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

            if (listVehicleID is null)
            {
                listVehicleID = new List<long>();
            }

            return listVehicleID;
        }

        /// <summary>kiểm tra  string null hoặc rỗng</summary>
        /// <typeparam name="T">kiểu đối tượng trả về list</typeparam>
        /// <param name="text">chuỗi muốn kiểm tra</param>
        /// <param name="property">tên thuộc tính</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <returns>true - không lỗi, false - có lỗi và ghi log</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static bool checkNullOrEmptyString<T>(string? text, string property, ref ApiResponse<T> response)
        {
            var valid = true;
            if (string.IsNullOrEmpty(text))
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "null " + property, ref response);
                valid = false;
            }
            return valid;
        }

        /// <summary>kiểm tra parse string to int</summary>
        /// <typeparam name="T">kiểu đối tượng trả về list</typeparam>
        /// <param name="text">chuỗi cần kiểm tra</param>
        /// <param name="property">tên thuộc tính</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <returns>true - không lỗi, false - có lỗi</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static bool checkParseIntString<T>(string? text, string property, ref ApiResponse<T> response)
        {
            var valid = true;
            try
            {
                int.Parse(text);
            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong " + property, ref response);
                valid = false;
            }
            return valid;
        }

        /// <summary>kiểm tra mật khẩu thỏa mãn yêu cầu</summary>
        /// <param name="pass">mật khẩu</param>
        /// <param name="property">tên thuộc tính</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <returns>true - không lỗi, false - có lỗi</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static bool checkValidPass(string? pass, string property, ref ApiResponse<int> response)
        {
            var valid = true;
            if (pass.IsNullOrEmpty() || !Regex.IsMatch(pass, @"^[a-zA-Z0-9]{6,100}$"))
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong " + property, ref response);
                valid = false;
            }
            return valid;
        }

        /// <summary>kiểm tra username thỏa mãn điều kiện</summary>
        /// <param name="username">username.</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <returns>true - không lỗi, false - có lỗi</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static bool ValidUsername(string username, ref ApiResponse<int> response)
        {
            var valid = true;
            if (username.IsNullOrEmpty() || !Regex.IsMatch(username, @"^[a-zA-Z0-9]{1,50}$"))
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong username", ref response);
                valid = false;
            }
            return valid;
        }

        /// <summary>kiểm tra số điện thoại thỏa mãn điều kiện</summary>
        /// <param name="phone">string số điện thoại</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <returns>true - không lỗi, false - có lỗi</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static bool ValidPhone(string? phone, ref ApiResponse<int> response)
        {
            var valid = true;
            if (phone.IsNullOrEmpty() || !Regex.IsMatch(phone, @"^[0-9]{1,10}$"))
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong phone", ref response);
                valid = false;
            }
            return valid;
        }

        /// <summary>kiểm tra mail có thỏa mãn điều kiện</summary>
        /// <param name="mail">string mail.</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <returns>true - không lỗi, false - có lỗi</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static bool ValidMail(string? mail, ref ApiResponse<int> response)
        {
            var valid = true;
            if (!mail.IsNullOrEmpty())
            {
                if (!Regex.IsMatch(mail, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,200}$"))
                {
                    LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong email", ref response);
                    valid = false;
                }
            }
            return valid;
        }

        /// <summary>kiểm tra ngày sinh null và đủ 18 tuổi</summary>
        /// <param name="birthday">ngày sinh</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <returns>true - không lỗi, false - có lỗi</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static bool ValidBirthday(DateTime? birthday, ref ApiResponse<int> response)
        {
            var valid = true;
            if (birthday is null)
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "null birthday", ref response);
                valid = false;
            }
            else
            {
                if (DateTime.Now.Year - birthday.Value.Year < 18)
                {
                    LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "user not 18 years old", ref response);
                    valid = false;
                }
            }
            return valid;
        }


    }
}
