using BAWebLab2.Entities;

namespace BAWebLab2.Core.Services.IService
{
    /// <summary>class interface của VehiclesService</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public interface IVehiclesService
    {

        /// <summary>tìm theo mã công ty và trạng thái chưa xóa</summary>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable đã lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        IEnumerable<Vehicles> FindByCompanyID(int companyID);
    }
}
