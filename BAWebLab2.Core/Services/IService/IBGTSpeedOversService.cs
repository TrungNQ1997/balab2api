using BAWebLab2.Entities;
using System.Linq.Expressions;

namespace BAWebLab2.Core.Services.IService
{
    /// <summary>class interface của BGTSpeedOversService</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public interface IBGTSpeedOversService
    {
        /// <summary>lấy inumerable theo companyId và trạng thái chưa xóa</summary>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable sau khi đã lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        IEnumerable<BGTSpeedOvers> GetByCompanyID(int companyID);

        /// <summary>tìm theo điều  kiện</summary>
        /// <param name="expression">điều kiện lọc</param>
        /// <returns>ienumerable sau khi lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        IEnumerable<BGTSpeedOvers> Find(Expression<Func<BGTSpeedOvers, bool>> expression);
    }
}
