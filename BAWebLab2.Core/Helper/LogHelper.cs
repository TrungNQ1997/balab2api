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
        /// <param name="log">ILog được khỏi tạo từ class muốn log</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/28/2023 created
        /// </Modified>
        public static void LogErrorInClass(string error, ILog log)
        {
            log.Error(error);
        }

        /// <summary>ghi lỗi vào log và push lỗi vào dữ liệu trả về</summary>
        /// <typeparam name="T">kiểu đối tượng trả về list</typeparam>
        /// <param name="statusCode">mã lỗi theo http status code.</param>
        /// <param name="error">string lỗi</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
        /// <param name="log">ILog được khỏi tạo từ class muốn log</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public static void LogAndSetResponseErrorInClass<T>(HttpStatusCode statusCode, string errorDisplay, string errorLog, ref ApiResponse<T> response, ILog log)
        {
            log.Error(errorLog);
            response.Message.Add(errorDisplay);
            response.StatusCode = ((int)statusCode).ToString();
        }

        /// <summary>ghi lỗi vào log và gán lỗi vào kết quả trả về</summary>
        /// <typeparam name="T">kiểu đối tượng api trả về</typeparam>
        /// <param name="errorDisplay">lỗi hiển thị với người dùng</param>
        /// <param name="errorLog">lỗi log vào file</param>
        /// <param name="response">đối tượng nhận kết quả trả về người dùng</param>
        /// <param name="log">ILog được khởi tạo từ class cần log</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified>
        public static void LogAndSetResponseStoreErrorInClass<T>(string errorDisplay, string errorLog, ref StoreResult<T> response, ILog log)
        {
            log.Error(errorLog);
            response.Message.Add(errorDisplay);
        }

    }
}
