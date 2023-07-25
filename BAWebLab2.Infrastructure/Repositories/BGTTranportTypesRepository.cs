using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Repositories
{
    public class BGTTranportTypesRepository:GenericRepository<BGTTranportTypes>, IBGTTranportTypesRepository
    {
        private readonly BADbContext _bADbContext;
        public BGTTranportTypesRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

    }
}
