using System.ComponentModel.DataAnnotations.Schema;

namespace BAWebLab2.Entities
{
    /// <summary>class map đối tượng với bảng BGT.VehicleTransportTypes trong database</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    [Table("BGT.VehicleTransportTypes")]
    public class BGTVehicleTransportTypes
    {
        public long FK_VehicleID { get; set; }

        public int FK_TransportTypeID { get; set; }

        public int FK_CompanyID { get; set; }

        public bool? IsDeleted { get; set; }
    }
}
