using Azure;
using BAWebLab2.Core.Services;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using log4net;
using Newtonsoft.Json;
using System.Net;

namespace BAWebLab2.Core.LibCommon
{
    /// <summary>class xử lí báo cáo</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/2/2023 created
    /// </Modified>
    public class ReportHelper
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ReportVehicleSpeedViolationService));

        /// <summary>phân trang báo cáo</summary>
        /// <typeparam name="T">kiểu dữ liệu list báo cáo</typeparam>
        /// <param name="input">tham số truyền vào báo cáo</param>
        /// <param name="list">list chưa phân trang</param>
        /// <returns>list đã phân trang</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/2/2023 created
        /// </Modified>
        public static IEnumerable<T> PagingIEnumerable<T>(int pageNumber, int pageSize, IEnumerable<T> list)
        {
            return list.Skip((pageNumber - 1) * pageSize).Take(pageSize);
        }

        /// <summary>kiểm tra từ ngày đến ngày báo cáo</summary>
        /// <param name="dateFrom">từ ngày</param>
        /// <param name="dateTo">đến ngày</param>
        /// <param name="response">đối tượng nhận kết quả check</param>
        /// <returns>true - không lỗi, false - có lỗi</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/10/2023 created
        /// </Modified>
        public static bool CheckDayReport(DateTime dateFrom, DateTime dateTo, ref ApiResponse<ResultReportSpeed> response)
        {
            var valid = true;
            if (dateFrom > dateTo)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "DayFrom bigger than DayTo", "data dateFrom " + JsonConvert.SerializeObject(dateFrom) + " dateTo " + JsonConvert.SerializeObject(dateTo) + " " + " DayFrom bigger than DayTo", ref response, _logger);
            }
            if (((dateTo - dateFrom)).TotalDays > 60)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "DayFrom must not be more than 60 days from DayTo", "data dateFrom " + JsonConvert.SerializeObject(dateFrom) + " dateTo " + JsonConvert.SerializeObject(dateTo) + " " + " DayFrom must not be more than 60 days from DayTo", ref response, _logger);
            }
            return valid;
        }

    }
}
