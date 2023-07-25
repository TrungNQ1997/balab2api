using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services
{
    public class ReportActivitySummariesService:IReportActivitySummariesService
    {
        private readonly IReportActivitySummariesRepository _reportActivitySummariesRepository;

        public ReportActivitySummariesService(IReportActivitySummariesRepository reportActivitySummariesRepository)
        {

            _reportActivitySummariesRepository = reportActivitySummariesRepository;
        }
         
        public IEnumerable<ReportActivitySummaries> Find(Expression<Func<ReportActivitySummaries, bool>> expression)
        {
            return _reportActivitySummariesRepository.Find(expression);
        }
    }
}
