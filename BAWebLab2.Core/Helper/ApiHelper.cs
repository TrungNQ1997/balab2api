using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Model;
using log4net;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System.Net;

namespace BAWebLab2.Core.LibCommon
{
    /// <summary>class chứa hàm chung xử lí dữ liệu api</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/2/2023 created
    /// </Modified>
    public class ApiHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ApiHelper));
        private static readonly string secretKey = "2b7e151628aed2a6abf7158809cf4f3c"; // Khóa AES 256 bits (32 bytes)
        private static readonly string iv = "000102030405060708090a0b0c0d0e0f"; // IV 128 bits (16 bytes)
        public static string HeaderNameSecurity = "SecurityData";


        /// <summary>lấy giá trị  header trong request</summary>
        /// <param name="request">request chưa header</param>
        /// <param name="key">key của header cần lấy</param>
        /// <returns>giá trị của header</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/31/2023 created
        /// </Modified>
        public static string GetHeader(HttpRequest request, string key)
        {
            var value = "";
            try
            {
                request.Headers.TryGetValue(key, out StringValues headerValue);
                value = headerValue.ToString();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("key " + JsonConvert.SerializeObject(key) + " error " + ex.ToString(), _logger);
            }
            return value;
        }

        /// <summary>giải mã header</summary>
        /// <param name="request">request từ client</param>
        /// <param name="key">key cần lấy trong header</param>
        /// <param name="valid">biến kiểm tra header, true - header đúng, false - header sai</param>
        /// <returns>đối tượng userToken sau khi giải mã</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified>
        public static UserToken DeCryptionHeader(HttpRequest request, string key, ref bool valid)
        {
            var objReturn = new UserToken();
            var objParce = new UserToken();
            try
            {
                var securityData = GetHeader(request, key);
                objParce = FormatDataHelper.DeCryptionUserToken(secretKey, iv, securityData);
            }
            catch (Exception ex)
            {
                _logger.Error(ex.ToString());
            }
            if (objParce == null)
            {
                valid = false;
            }
            else
            {
                if (objParce.Token.IsNullOrEmpty())
                {
                    valid = false;
                }
                else
                {
                    objReturn = objParce;
                    valid = true;
                }
            }
            return objReturn;
        }

        /// <summary>kiểm tra  header của request</summary>
        /// <typeparam name="T">kiểu đối tượng trả về list</typeparam>
        /// <param name="request">request nhận từ client</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <returns>true - không lỗi, false - có lỗi</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static bool CheckValidHeader<T>(HttpRequest request, ref ApiResponse<T> response)
        {
            var valid = true;
            try
            {
                var comID = int.Parse(ApiHelper.GetHeader(request, "CompanyID"));

            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "null CompanyID", "error in CheckValidHeader error format header CompanyID exception " + ex.ToString(), ref response, _logger);
                valid = false;
            }

            return valid;
        }

        /// <summary>kiểm tra header bảo mật</summary>
        /// <typeparam name="T">kiểu đối tượng api trả về</typeparam>
        /// <param name="request">request từ client</param>
        /// <param name="headerName">tên của header.</param>
        /// <param name="response">đối tượng nhận kết quả trả về</param>
        /// <returns>true - có header thỏa mãn, false - header null</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified>
        public static bool CheckNullSecurityHeader<T>(HttpRequest request, string headerName, ref ApiResponse<T> response)
        {
            var valid = false;
            try
            {
                var validSecuHeader = false;
                var userToken = ApiHelper.DeCryptionHeader(request, headerName, ref validSecuHeader);
                if (userToken == null)
                {
                    valid = false;
                }
                else
                {
                    valid = true;
                }
            }
            catch (Exception ex)
            {
                valid = false;
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "error header security", "error check null Security Header  name " + JsonConvert.SerializeObject(headerName) + " error " + ex.ToString(), ref response, _logger);
            }
            return valid;
        }

    }
}
