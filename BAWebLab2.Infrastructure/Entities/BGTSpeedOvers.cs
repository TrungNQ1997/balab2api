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

        /// <summary>Van toc vi pham (van toc thuc cua xe luc vi pham)</summary>
        /// <value>The velocity GPS.</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/28/2023 created
        /// </Modified>
        public int? VelocityGps { get; set; }

        /// <summary>Gets or sets Van toc gioi han cho phep (Thay doi theo tung cung duong)</summary>
        /// <value>Van toc gioi han cho phep (Thay doi theo tung cung duong)</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/28/2023 created
        /// </Modified>
        public int? VelocityAllow { get; set; }

        public double? StartKm { get; set; }

        public double? EndKm { get; set; }

    }




}
