using BAWebLab2.Infrastructure.Entities;

namespace BAWebLab2.Core.Services.IService
{
    public interface IUserTokenService
    {
        /// <summary>Fakes dữ liệu test và kiểm tra token tồn tại</summary>
        /// <param name="input">chứa thông tin user và token</param>
        /// <returns>true - token thỏa mãn, false - token sai</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified>
        public bool FakeDataAndCheckToken(UserToken input);

        /// <summary>kiểm tra token user phân hệ user</summary>
        /// <param name="input">thông tin user và token</param>
        /// <returns>true - token đúng, false- token sai</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/15/2023 created
        /// </Modified>
        public bool CheckTokenUser(UserToken input);
    }
}
