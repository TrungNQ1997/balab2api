using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;

namespace BAWebLab2.Infrastructure.Repositories
{
    public class VehiclesRepository:GenericRepository<Vehicles> , IVehiclesRepository
    {
        private readonly BADbContext _bADbContext;
        public VehiclesRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

    }
}
