using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services.IService
{
    public interface IUserTokenService
    { 
        public bool FakeDataAndCheckToken(UserToken input);
    }
}
