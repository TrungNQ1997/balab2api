using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Repositories
{
    public class UserTokenRepository:GenericRepository<UserToken>, IUserTokenRepository
    {
        private readonly BADbContext _bADbContext;

        public UserTokenRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

        public void FakeData()
        {
            if (_bADbContext.UserTokens.Count() == 0)
            {
                _bADbContext.UserTokens.Add(new UserToken() { CompanyID = 15076, UserID = 1, Token = "EF4A9073-58AB-4D2D-93C1-6936093EE015" }); 
            }
        }

        public bool CheckExistToken(UserToken userToken)
        {
            var exist = false;
            if(_bADbContext.UserTokens.Where(m=>m.Token == userToken.Token && m.CompanyID == userToken.CompanyID && m.UserID == userToken.UserID).Count() > 0)
            {
                exist = true;
            }
            return exist;
        }


        }
}
