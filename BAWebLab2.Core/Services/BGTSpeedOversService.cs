using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services
{
    public class BGTSpeedOversService : IBGTSpeedOversService
    {
        private readonly IBGTSpeedOversRepository _bGTSpeedOversRepository;

        public BGTSpeedOversService(IBGTSpeedOversRepository bGTSpeedOversRepository)
        {

            _bGTSpeedOversRepository = bGTSpeedOversRepository;
        }

        public IEnumerable<BGTSpeedOvers> GetByCompanyID(int companyID)
        {
            return _bGTSpeedOversRepository.Find(m=>m.FK_CompanyID == companyID);
        }

        public IEnumerable<BGTSpeedOvers> Find(Expression<Func<BGTSpeedOvers, bool>> expression) {
            return _bGTSpeedOversRepository.Find(expression);
        }
    }
}
