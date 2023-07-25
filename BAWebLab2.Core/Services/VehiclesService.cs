using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services
{
    public class VehiclesService:IVehiclesService
    {
        private readonly IVehiclesRepository _vehiclesRepository;

        public VehiclesService(IVehiclesRepository vehiclesRepository)
        {

            _vehiclesRepository = vehiclesRepository;
        }

        public IEnumerable<Vehicles> Find(Expression<Func<Vehicles, bool>> expression)
        {
            return _vehiclesRepository.Find(expression);
        }
    }
}
