using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Entities
{
    [Table("Vehicle.Vehicles")]
    public class Vehicles
    {
        
       public long PK_VehicleID { get; set; }

        public int FK_CompanyID { get; set; }

        public string VehiclePlate { get; set; }

        public string PrivateCode { get; set; }

        public bool IsDeleted { get; set; }

    }
}
