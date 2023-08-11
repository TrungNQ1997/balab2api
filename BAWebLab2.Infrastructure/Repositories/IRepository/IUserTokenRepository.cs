using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Infrastructure.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Repositories.IRepository
{
    public interface IUserTokenRepository:IGenericRepository<UserToken>
    {
        //public new void Add(UserToken userToken);
        public void FakeData();

        public bool CheckExistToken(UserToken userToken);
    }
}
