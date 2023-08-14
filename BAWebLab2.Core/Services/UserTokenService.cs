using BAWebLab2.Core.LibCommon;
using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Model;
using Dapper;
using log4net;
using System.Data;

namespace BAWebLab2.Core.Services
{
    public class UserTokenService : IUserTokenService
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly ILog _logger;
        public UserTokenService(IUserTokenRepository userTokenRepository, IUserRepository userRepository)
        {
            _logger = LogManager.GetLogger(typeof(UserTokenService));

            _userTokenRepository = userTokenRepository;
            _userRepository = userRepository;
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

        public bool CheckTokenUser(UserToken input)
        {
            var valid = false;
            try
            {
                var result = new StoreResult<UserRole>();

                DynamicParameters parameters = new DynamicParameters();

                parameters.Add("userId", input.UserID);
                parameters.Add("token", input.Token);
                parameters.Add("isValid", 0, DbType.Boolean, ParameterDirection.Output);

                var resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWeb_User_Check_Token", ref parameters);

                valid = parameters.Get<bool>("isValid");

            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("error when CheckTokenUser error " + ex.ToString(), _logger);
            }
            return valid;
        }

    }
}
