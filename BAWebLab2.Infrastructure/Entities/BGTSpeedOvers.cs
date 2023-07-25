using System.ComponentModel.DataAnnotations.Schema;

namespace BAWebLab2.Entities
{
    /// <summary>class map đối tượng với bảng BGT.SpeedOvers trong database</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    [Table("BGT.SpeedOvers")]
    public class BGTSpeedOvers
    {

        public long FK_VehicleID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? FK_CompanyID { get; set; }

        public int? VelocityGps { get; set; }

        public int? VelocityAllow { get; set; }

         public double? StartKm { get; set; }

        public double? EndKm { get; set; }

    }




}
