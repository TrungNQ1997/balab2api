using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services
{
    public class BGTTranportTypesService:IBGTTranportTypesService
    {
        private readonly IBGTTranportTypesRepository _bGTTranportTypesRepository;

        public BGTTranportTypesService(IBGTTranportTypesRepository bGTTranportTypesRepository)
        {

            _bGTTranportTypesRepository = bGTTranportTypesRepository;
        }

        public IEnumerable<BGTTranportTypes> GetAll()
        {
            return _bGTTranportTypesRepository.GetAll();
        }
    }
}
