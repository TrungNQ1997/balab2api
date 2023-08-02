using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;

namespace BAWebLab2.Infrastructure.Repositories
{
    /// <summary>class repository của Vehicle</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/2/2023 created
    /// </Modified>
    public class VehiclesRepository : GenericRepository<Vehicles>, IVehiclesRepository
    {
        private readonly BADbContext _bADbContext;
        public VehiclesRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

    }
}
