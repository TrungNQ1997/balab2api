using BAWebLab2.Entity;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.DAL.Repository.IRepository
{
    public interface   IUserRepository : IGenericRepository<User>
    {
      public  List<User> GetAllUsers1(DynamicParameters param);
    }
}
