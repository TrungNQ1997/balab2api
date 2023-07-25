using BAWebLab2.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services.IService
{
    public interface IBGTTranportTypesService
    {
        IEnumerable<BGTTranportTypes> GetAll();
    }
}
