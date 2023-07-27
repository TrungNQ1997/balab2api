using BAWebLab2.Entities;

namespace BAWebLab2.Core.Services.IService
{
    /// <summary>class interface của BGTVehicleTransportTypesService</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public interface IBGTVehicleTransportTypesService
    {

        /// <summary>lấy ienumerable&lt;BGTVehicleTransportTypes&gt; theo mã công ty và trạng thái chưa xóa</summary>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable sau khi đã lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        IEnumerable<BGTVehicleTransportTypes> GetByCompanyID(int companyID);
    }
}
