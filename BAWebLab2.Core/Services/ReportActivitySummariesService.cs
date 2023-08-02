using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using System.Linq.Expressions;

namespace BAWebLab2.Core.Services
{
    /// <summary>service của ReportActivitySummaries</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public class ReportActivitySummariesService : IReportActivitySummariesService
    {
        private readonly IReportActivitySummariesRepository _reportActivitySummariesRepository;

        public ReportActivitySummariesService(IReportActivitySummariesRepository reportActivitySummariesRepository)
        {

            _reportActivitySummariesRepository = reportActivitySummariesRepository;
        }

        /// <summary>tìm theo điều kiện</summary>
        /// <param name="expression">điều kiện lọc</param>
        /// <returns>ienumerable sau khi đã lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        public IEnumerable<ReportActivitySummaries> Find(Expression<Func<ReportActivitySummaries, bool>> expression)
        {
            return _reportActivitySummariesRepository.Find(expression);
        }
    }
}
