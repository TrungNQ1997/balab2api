using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;

namespace BAWebLab2.Infrastructure.Repositories
{
    /// <summary>Repository của BGTVehicleTransportTypes</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public class BGTVehicleTransportTypesRepository : GenericRepository<BGTVehicleTransportTypes>, IBGTVehicleTransportTypesRepository
    {
        private readonly BADbContext _bADbContext;
        public BGTVehicleTransportTypesRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

    }
}
