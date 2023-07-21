using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.Model;

namespace BAWebLab2.Infrastructure.Repositories.IRepository
{
    /// <summary>class interface của ReportVehicleSpeedViolationRepository, chứa các hàm cần cho báo cáo vi phạm tốc độ</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    public interface IReportVehicleSpeedViolationRepository : IGenericRepository<User>
    {
        /// <summary>lấy danh sách vehicle</summary>
        /// <typeparam name="Vehicles">đối tượng vehicle</typeparam>
        /// <returns>danh sách vehicle</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified>
        MultipleResult<Vehicles> GetVehicles();

        /// <summary>lấy dữ liệu báo báo vi phạm tốc độ</summary>
        /// <typeparam name="ResultReportSpeed">đối tượng chứa kết quả báo cáo</typeparam>
        /// <param name="input">chứa tham số báo cáo</param>
        /// <returns>list dữ liệu báo cáo</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified>
        StoreResult<ResultReportSpeed> GetDataReport(InputReportSpeed input );
    }
}
