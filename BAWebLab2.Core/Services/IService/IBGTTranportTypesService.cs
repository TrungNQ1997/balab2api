using BAWebLab2.Entities;

namespace BAWebLab2.Core.Services.IService
{
    /// <summary>class interface của BGTTranportTypesService</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public interface IBGTTranportTypesService
    {
        /// <summary>lấy tất cả</summary>
        /// <returns>ienumerable tất cả BGTTranportTypes</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        IEnumerable<BGTTranportTypes> GetAll();
    }
}
