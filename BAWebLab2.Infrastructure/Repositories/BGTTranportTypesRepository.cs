using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;

namespace BAWebLab2.Infrastructure.Repositories
{
    /// <summary>Repository của BGTTranportTypes</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public class BGTTranportTypesRepository : GenericRepository<BGTTranportTypes>, IBGTTranportTypesRepository
    {
        private readonly BADbContext _bADbContext;
        public BGTTranportTypesRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

    }
}
