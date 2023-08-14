using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace BAWebLab2.Infrastructure.Repositories
{
    public class UserTokenRepository : GenericRepository<UserToken>, IUserTokenRepository
    {
        private readonly BADbContext _bADbContext;
		private readonly IConfiguration _configuration;
		public UserTokenRepository(BADbContext bADbContext, IConfiguration configuration)
            : base(bADbContext, configuration)
        {
            _bADbContext = bADbContext;
			_configuration = configuration;
		}

        /// <summary>Fakes dữ liệu test token</summary>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified> 
        public void FakeData()
        {
            if (_bADbContext.UserTokens.Count() == 0)
            {
                _bADbContext.UserTokens.Add(new UserToken() { CompanyID = 15076, UserID = 1, Token = "EF4A9073-58AB-4D2D-93C1-6936093EE015", ExpiredDate = DateTime.Now.AddDays(1).Date });
            }
        }

        /// <summary>kiểm tra token có tồn tại</summary>
        /// <param name="userToken">chứ thông tin user token.</param>
        /// <returns>true - token thỏa mãn, false - token sai</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/14/2023 created
        /// </Modified> 
        public bool CheckExistToken(UserToken userToken)
        {
            var exist = false;
            if (_bADbContext.UserTokens.Where(m => m.Token == userToken.Token && m.CompanyID == userToken.CompanyID
            && m.UserID == userToken.UserID && userToken.ExpiredDate > DateTime.Now &&
            userToken.ExpiredDate == m.ExpiredDate).Count() > 0)
            {
                exist = true;
            }
            return exist;
        }


    }
}
