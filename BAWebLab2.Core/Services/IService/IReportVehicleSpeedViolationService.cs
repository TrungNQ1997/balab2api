using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;

namespace BAWebLab2.Core.Services.IService
{
    /// <summary>class interface của ReportVehicleSpeedViolationService, các service cần cho báo cáo vi phạm tốc độ phương tiện</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    public interface   IReportVehicleSpeedViolationService
    {
        /// <summary>lấy danh sách vehicle</summary>
        /// <returns>danh sách vehicle</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified>
        public StoreResult<Vehicles> GetVehicles();

        /// <summary>lấy dữ liệu báo cáo vi phạm tốc độ phương tiện</summary>
        /// <param name="input">đối tượng chứa các tham số báo cáo cần</param>
        /// <returns>dữ liệu báo cáo</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified>
        public StoreResult<ResultReportSpeed> GetDataReport(InputReportSpeed input);
    }
}
