using BAWebLab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services.IService
{
    public interface IReportActivitySummariesService
    {
        IEnumerable<ReportActivitySummaries> Find(Expression<Func<ReportActivitySummaries, bool>> expression);
    }
}
