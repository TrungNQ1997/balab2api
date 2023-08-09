using BAWebLab2.Model;
using log4net;
using System.Net;

namespace BAWebLab2.Core.LibCommon
{
    /// <summary>class xử lí log</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/2/2023 created
    /// </Modified>
    public class LogHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LogHelper));
        /// <summary>ghi lỗi vào log</summary>
        /// <param name="error">chuỗi lỗi</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/28/2023 created
        /// </Modified>
        public static void LogError(string error)
        {
            _logger.Error(error); 
        }

        /// <summary>ghi lỗi vào log và push lỗi vào dữ liệu trả về</summary>
        /// <typeparam name="T">kiểu đối tượng trả về list</typeparam>
        /// <param name="statusCode">mã lỗi theo http status code.</param>
        /// <param name="error">string lỗi</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static void LogAndSetResponseError<T>(HttpStatusCode statusCode, string error, ref ApiResponse<T> response)
        {
            LogError(error);
            response.Message.Add(error);
            response.StatusCode = ((int)statusCode).ToString();
        }

    }
}
