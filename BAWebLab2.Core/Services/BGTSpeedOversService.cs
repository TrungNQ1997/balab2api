using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using System.Linq.Expressions;

namespace BAWebLab2.Core.Services
{
    /// <summary>service của BGTSpeedOvers</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public class BGTSpeedOversService : IBGTSpeedOversService
    {
        private readonly IBGTSpeedOversRepository _bGTSpeedOversRepository;

        public BGTSpeedOversService(IBGTSpeedOversRepository bGTSpeedOversRepository)
        {

            _bGTSpeedOversRepository = bGTSpeedOversRepository;
        }

        /// <summary>lấy inumerable theo companyId và trạng thái chưa xóa</summary>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable sau khi đã lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
         public IEnumerable<BGTSpeedOvers> GetByCompanyID(int companyID)
        {
            return _bGTSpeedOversRepository.Find(m=>m.FK_CompanyID == companyID);
        }

        /// <summary>tìm theo điều  kiện</summary>
        /// <param name="expression">điều kiện lọc</param>
        /// <returns>ienumerable sau khi lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        public IEnumerable<BGTSpeedOvers> Find(Expression<Func<BGTSpeedOvers, bool>> expression) {
            return _bGTSpeedOversRepository.Find(expression);
        }
    }
}
