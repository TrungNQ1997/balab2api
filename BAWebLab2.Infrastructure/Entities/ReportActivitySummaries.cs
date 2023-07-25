using System.ComponentModel.DataAnnotations.Schema;

namespace BAWebLab2.Entities
{
    /// <summary>class map đối tượng với bảng Report.ActivitySummaries trong database </summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    [Table("Report.ActivitySummaries")]
    public class ReportActivitySummaries
    {
        public long FK_VehicleID { get; set; }

        public DateTime FK_Date { get; set; }

        public int FK_CompanyID { get; set; }

        public double? TotalKmGps { get; set; }

        public int? ActivityTime { get; set; }

       public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        
    }
}
