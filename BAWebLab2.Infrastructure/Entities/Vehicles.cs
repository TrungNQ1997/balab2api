using System.ComponentModel.DataAnnotations.Schema;

namespace BAWebLab2.Entities
{
    /// <summary>class map đối tượng với bảng Vehicle.Vehicles trong database</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
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
