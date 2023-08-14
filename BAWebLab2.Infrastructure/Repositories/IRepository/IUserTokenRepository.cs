using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Infrastructure.Repository.IRepository;

namespace BAWebLab2.Infrastructure.Repositories.IRepository
{
    public interface IUserTokenRepository : IGenericRepository<UserToken>
    {
        /// <summary>Fakes dữ liệu test token</summary>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified>
        public void FakeData();

        /// <summary>kiểm tra token có tồn tại</summary>
        /// <param name="userToken">chứ thông tin user token.</param>
        /// <returns>true - token thỏa mãn, false - token sai</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified>
        public bool CheckExistToken(UserToken userToken);
    }
}
