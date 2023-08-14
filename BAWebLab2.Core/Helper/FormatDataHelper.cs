using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Model;
using log4net;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;
using System.Net.WebSockets;
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
        private static readonly ILog _logger = LogManager.GetLogger(typeof(FormatDataHelper));

        public FormatDataHelper(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static string regexPass = @"^[a-zA-Z0-9]{6,100}$";
        public static string regexUsername = @"^[a-zA-Z0-9]{1,50}$";
        public static string regexPhone = @"^[0-9]{1,10}$";
        public static string regexMail = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,200}$";

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
            try
            {
                string? setting2 = _configuration["NumberRoundDecimal"];
                if (number is not null)
                {
                    result = Math.Round((double)number, int.Parse(setting2 is null ? "0" : setting2));
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("data " + JsonConvert.SerializeObject(number) + " error at RoundDouble " + ex.ToString(), _logger);
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
            try
            {
                if (number is not null)
                {
                    stringReturn = (Math.Floor(((double)number / 60))).ToString().PadLeft(2, '0') + ":" + (Math.Ceiling((double)number % 60)).ToString().PadLeft(2, '0');
                }
                if (stringReturn == "00:00")
                {
                    stringReturn = "";
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("error in NumberMinuteToStringHour data " + JsonConvert.SerializeObject(number) + " " + ex.ToString(), _logger);
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
            var StrReturn = "";
            try
            {
                MD5 md5 = MD5.Create();
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
                StringBuilder hashSb = new StringBuilder();
                foreach (byte b in hash)
                {
                    hashSb.Append(b.ToString("X2"));
                }
                StrReturn = hashSb.ToString();
            }
            catch (Exception ex)
            {
                _logger.Error("error in HashMD5 data " + JsonConvert.SerializeObject(text) + " " + ex.ToString());
            }
            return StrReturn;
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
        public static bool CheckNullOrEmptyString<T>(string? text, string property, ref ApiResponse<T> response)
        {
            var valid = true;
            try
            {
                if (string.IsNullOrEmpty(text))
                {
                    LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "null " + JsonConvert.SerializeObject(property), " in CheckNullOrEmptyString data " + JsonConvert.SerializeObject(text) + " null " + property, ref response, _logger);
                    valid = false;
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "null property " + JsonConvert.SerializeObject(property), "value " + JsonConvert.SerializeObject(text) + " property " + JsonConvert.SerializeObject(property) + " " + ex.ToString(), ref response, _logger);
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
        public static bool CheckParseIntString<T>(string? text, string property, ref ApiResponse<T> response)
        {
            var valid = true;
            try
            {
                int.Parse(text);
            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "error format " + JsonConvert.SerializeObject(property), "error in CheckParseIntString data " + JsonConvert.SerializeObject(text) + " wrong " + property + " " + ex.ToString(), ref response, _logger);
                valid = false;
            }
            return valid;
        }

        /// <summary>kiểm tra thuộc tính theo regex</summary>
        /// <param name="value">giá trị</param>
        /// <param name="property">tên thuộc tính</param>
        /// <param name="response">đối tượng lưu kết quả trả về</param>
        /// <param name="pattern">pattern để check regex</param>
        /// <returns>true - giá trị thỏa mãn regex, false - giá trị sai định dạng, log bug và add error vào response</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/9/2023 created
        /// </Modified>
        public static bool CheckValidPropertyRegex(string? value, string property, ref ApiResponse<int> response, string pattern)
        {
            var valid = true;
            try
            {
                if (value.IsNullOrEmpty())
                {
                    LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "null " + JsonConvert.SerializeObject(property), "data " + JsonConvert.SerializeObject(value) +
                  " pattern " + JsonConvert.SerializeObject(pattern) + " property " + JsonConvert.SerializeObject(property) + " at CheckValidPropertyRegex empty " + JsonConvert.SerializeObject(property), ref response, _logger);

                    valid = false;
                }
                else
                {
                    if (!Regex.IsMatch(value, pattern))
                    {
                        LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "error format " + JsonConvert.SerializeObject(property), "data " + JsonConvert.SerializeObject(value) +
                  " pattern " + JsonConvert.SerializeObject(pattern) + " property " + JsonConvert.SerializeObject(property) + " at CheckValidPropertyRegex error format " + JsonConvert.SerializeObject(property), ref response, _logger);
                        valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, " error " + JsonConvert.SerializeObject(property), "data " + JsonConvert.SerializeObject(value) +
                    " pattern " + JsonConvert.SerializeObject(pattern) + " property " + JsonConvert.SerializeObject(property) + " " + ex.ToString(), ref response, _logger);
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
            try
            {
                if (!mail.IsNullOrEmpty())
                {
                    if (!Regex.IsMatch(mail, FormatDataHelper.regexMail))
                    {
                        LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "error format email", "error format email value " + JsonConvert.SerializeObject(mail), ref response, _logger);
                        valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "error format email", "data " + JsonConvert.SerializeObject(mail) + " error " + ex.ToString(), ref response, _logger);
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
            try
            {
                if (birthday is null)
                {
                    LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "null birthday", "null birthday value " + JsonConvert.SerializeObject(birthday), ref response, _logger);
                    valid = false;
                }
                else
                {
                    if (DateTime.Now.Year - birthday.Value.Year < 18)
                    {
                        LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "user not 18 years old", "user not 18 years old, data " + JsonConvert.SerializeObject(birthday), ref response, _logger);
                        valid = false;
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "error birthday", "data " + JsonConvert.SerializeObject(birthday) + " error " + ex.ToString(), ref response, _logger);
            }
            return valid;
        }

        /// <summary>giải mã chuỗi bảo mật</summary>
        /// <param name="secretKey">key bảo mật</param>
        /// <param name="iv">iv bảo mật</param>
        /// <param name="securityData">chuỗi mã hóa</param>
        /// <returns>đối tượng userToken sau khi giải mã</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified>
        public static UserToken? DeCryptionUserToken(string secretKey, string iv, string securityData)
        {
            var objParce = new UserToken();
            try
            {
                var encryptedPayload = Convert.FromBase64String(securityData);
                using (var aesAlg = Aes.Create())
                {
                    aesAlg.Key = FormatDataHelper.StringToByteArray(secretKey);
                    aesAlg.IV = FormatDataHelper.StringToByteArray(iv);

                    using (var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV))
                    {
                        using (var msDecrypt = new MemoryStream(encryptedPayload))
                        {
                            using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                            {
                                using (var srDecrypt = new StreamReader(csDecrypt))
                                {
                                    var decryptedPayload = srDecrypt.ReadToEnd();
                                    objParce = JsonConvert.DeserializeObject<UserToken>(decryptedPayload);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("error when DeCryption UserToken " + ex.ToString(), _logger);
            }
            return objParce;
        }

        /// <summary>chuyển string ra dạng byte</summary>
        /// <param name="hex">chuỗi cần chuyển</param>
        /// <returns>byte sau khi chuyển</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified>
        public static byte[] StringToByteArray(string hex)
        {
            byte[] byReturn = new byte[1];
            try
            {
                byReturn = Enumerable.Range(0, hex.Length)
                                 .Where(x => x % 2 == 0)
                                 .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                                 .ToArray();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("error when convert String To Byte Array ,error " + ex.ToString(), _logger);
            }
            return byReturn;
        }

    }
}
