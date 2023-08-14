using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;
using Microsoft.Extensions.Configuration;
using System.Configuration;

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
		private readonly IConfiguration _configuration;
		public VehiclesRepository(BADbContext bADbContext, IConfiguration configuration)
            : base(bADbContext,configuration)
        {
            _bADbContext = bADbContext;
			_configuration = configuration;
		}

    }
}
