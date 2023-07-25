﻿using BAWebLab2.Entities;
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
    public class BGTVehicleTransportTypesRepository:GenericRepository<BGTVehicleTransportTypes>, IBGTVehicleTransportTypesRepository
    {
        private readonly BADbContext _bADbContext;
        public BGTVehicleTransportTypesRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

    }
}
