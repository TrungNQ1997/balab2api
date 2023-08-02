using log4net;

namespace BAWebLab2.Core.LibCommon
{
    /// <summary>class xử lí log</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/2/2023 created
    /// </Modified>
    public class LogService
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LogService));
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

    }
}
