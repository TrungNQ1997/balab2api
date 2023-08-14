﻿using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Repository;
using Microsoft.Extensions.Configuration;
using System.Configuration;

namespace BAWebLab2.Infrastructure.Repositories
{
    /// <summary>Repository của BGTSpeedOvers</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/27/2023 created
    /// </Modified>
    public class BGTSpeedOversRepository : GenericRepository<BGTSpeedOvers>, IBGTSpeedOversRepository
    {
        private readonly BADbContext _bADbContext;
		private readonly IConfiguration _configuration;
		public BGTSpeedOversRepository(BADbContext bADbContext, IConfiguration configuration)
            : base(bADbContext, configuration)
        {
            _bADbContext = bADbContext;
			_configuration = configuration;
		}
    }
}
