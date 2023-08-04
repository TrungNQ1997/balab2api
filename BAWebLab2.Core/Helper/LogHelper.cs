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

        public static void LogAndSetResponseError<T>(HttpStatusCode statusCode, string error, ref ApiResponse<T> response)
        {
            LogError(error);
            response.Message = error;
            response.StatusCode = ((int)statusCode).ToString();
        }

    }
}
