using BAWebLab2.Core.LibCommon;
using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Infrastructure.Repositories;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using System.IdentityModel.Tokens.Jwt;

using Microsoft.IdentityModel.Tokens;
using BAWebLab2.Model;
using Dapper;
using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services
{
    public class UserTokenService:IUserTokenService
    {
        private readonly IUserTokenRepository _userTokenRepository;
        private readonly ILog _logger;
        public UserTokenService(IUserTokenRepository userTokenRepository)
        {
            _logger = LogManager.GetLogger(typeof(UserTokenService));

            _userTokenRepository = userTokenRepository;
        }
         
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
