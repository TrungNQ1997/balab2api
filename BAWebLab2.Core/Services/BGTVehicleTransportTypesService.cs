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
    public class BGTVehicleTransportTypesService: IBGTVehicleTransportTypesService
    {
        private readonly IBGTVehicleTransportTypesRepository _bGTVehicleTransportTypesRepository;

        public BGTVehicleTransportTypesService(IBGTVehicleTransportTypesRepository bGTVehicleTransportTypesRepository)
        {

            _bGTVehicleTransportTypesRepository = bGTVehicleTransportTypesRepository;
        }

        public IEnumerable<BGTVehicleTransportTypes> GetAll()
        {
            return _bGTVehicleTransportTypesRepository.GetAll();
        }
    }
}
