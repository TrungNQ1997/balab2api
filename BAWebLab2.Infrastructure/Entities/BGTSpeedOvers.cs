using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Entities
{
    [Table("BGT.SpeedOvers")]
    public class BGTSpeedOvers
    {

        public long FK_VehicleID { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public int? FK_CompanyID { get; set; }

        public int? VelocityGps { get; set; }

        public int? VelocityAllow { get; set; }

        public double? StartLongitude { get; set; }

        public double? StartLatitude { get; set; }

        public double? EndLongitude { get; set; }

        public double? EndLatitude { get; set; }

        public DateTime CreatedDate { get; set; }

        public string DriverName { get; set; }

        public string DriverLicense { get; set; }

        public byte SynCount { get; set; }

        public int? VelocityAverage { get; set; }

        public string Description { get; set; }

        public int? TotalTime { get; set; }

        public DateTime? LastUpdateHighway { get; set; }

        public int? FK_LandmarkHighwayID { get; set; }

        public double? Distances { get; set; }

        public double? StartKm { get; set; }

        public double? EndKm { get; set; }

    }




}
