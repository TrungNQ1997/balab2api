using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;

namespace BAWebLab2.Core.Services
{
    /// <summary>service của BGTVehicleTransportTypes</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public class BGTVehicleTransportTypesService : IBGTVehicleTransportTypesService
    {
        private readonly IBGTVehicleTransportTypesRepository _bGTVehicleTransportTypesRepository;

        public BGTVehicleTransportTypesService(IBGTVehicleTransportTypesRepository bGTVehicleTransportTypesRepository)
        { 
            _bGTVehicleTransportTypesRepository = bGTVehicleTransportTypesRepository;
        }

        /// <summary>lấy ienumerable&lt;BGTVehicleTransportTypes&gt; theo mã công ty và trạng thái chưa xóa</summary>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable sau khi đã lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        public IEnumerable<BGTVehicleTransportTypes> GetByCompanyID(int companyID)
        {
            return _bGTVehicleTransportTypesRepository.Find(m => m.FK_CompanyID == companyID);
        }
    }
}
