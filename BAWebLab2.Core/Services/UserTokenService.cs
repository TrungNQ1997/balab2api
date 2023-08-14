using BAWebLab2.Core.Services.IService;
using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using log4net;

namespace BAWebLab2.Core.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly ILog _logger;
        public UserTokenService(IUserTokenRepository userTokenRepository)
        {
            _logger = LogManager.GetLogger(typeof(UserTokenService));

            _userTokenRepository = userTokenRepository;
        }

        /// <summary>Fakes dữ liệu test và kiểm tra token tồn tại</summary>
        /// <param name="input">chứa thông tin user và token</param>
        /// <returns>true - token thỏa mãn, false - token sai</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified> 
        public bool FakeDataAndCheckToken(UserToken input)
        {
            var valid = false;
            //fake data
            _userTokenRepository.FakeData();

            //check data
            valid = _userTokenRepository.CheckExistToken(input);
            return valid;
        }

    }
}
