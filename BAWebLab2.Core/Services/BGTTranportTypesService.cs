using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;

namespace BAWebLab2.Core.Services
{
    /// <summary>service của BGTTranportTypes</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public class BGTTranportTypesService : IBGTTranportTypesService
    {
        private readonly IBGTTranportTypesRepository _bGTTranportTypesRepository;

        public BGTTranportTypesService(IBGTTranportTypesRepository bGTTranportTypesRepository)
        { 
            _bGTTranportTypesRepository = bGTTranportTypesRepository;
        }

        /// <summary>lấy tất cả</summary>
        /// <returns>ienumerable tất cả BGTTranportTypes</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        public IEnumerable<BGTTranportTypes> GetAll()
        {
            return _bGTTranportTypesRepository.GetAll();
        }
    }
}
