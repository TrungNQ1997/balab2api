using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Entities
{
    [Table("BGT.VehicleTransportTypes")]
    public class BGTVehicleTransportTypes
    {
        public long FK_VehicleID { get; set; }

        public int FK_TransportTypeID { get; set; }

        public int FK_CompanyID { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
