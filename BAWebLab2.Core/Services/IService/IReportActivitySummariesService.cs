using BAWebLab2.Entities;
using System.Linq.Expressions;

namespace BAWebLab2.Core.Services.IService
{
    /// <summary>class interface của ReportActivitySummariesService</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public interface IReportActivitySummariesService
    {
        /// <summary>tìm theo điều kiện</summary>
        /// <param name="expression">điều kiện lọc</param>
        /// <returns>ienumerable sau khi đã lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        IEnumerable<ReportActivitySummaries> Find(Expression<Func<ReportActivitySummaries, bool>> expression);
    }
}
