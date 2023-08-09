using BAWebLab2.Entities;
using BAWebLab2.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
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
            request.Headers.TryGetValue(key, out StringValues headerValue);
            return headerValue.ToString(); 
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
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "error format header CompanyID", ref response);
                valid = false;
            }
           return valid;
        }

    }
}
