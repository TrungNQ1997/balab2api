using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Entities
{
    [Table("Report.ActivitySummaries")]
    public class ReportActivitySummaries
    {
        public long FK_VehicleID { get; set; }

        public DateTime FK_Date { get; set; }

        public int FK_CompanyID { get; set; }

        public double? TotalKmGps { get; set; }

        public int? ActivityTime { get; set; }

        public int? StopCount { get; set; }

        public int? SpeedOverCount { get; set; }

        public DateTime? StartTime { get; set; }

        public DateTime? EndTime { get; set; }

        public double? KmOfSpeedOver { get; set; }

        public int? KmOfPulseMechanical { get; set; }

        public double? StartLongitude { get; set; }

        public double? StartLatitude { get; set; }

        public double? EndLatitude { get; set; }

        public double? EndLongitude { get; set; }

        public double? TargetLongitude { get; set; }

        public double? TargetLatitude { get; set; }

        public double? SpeedOverDistance { get; set; }

        public int? ResetCount { get; set; }

        public int? OpenDoorPickupCount { get; set; }

        public int? OpenDoorMoveCount { get; set; }

        public int? SuddenAccelerationCount { get; set; }

        public int? SuddenDecelerationCount { get; set; }

        public int? MinutesOfManchineOn { get; set; }

        public int? MinutesOfAirConditioningOn { get; set; }

        public int? MinutesOfAirConditioningOnwhenStop { get; set; }

        public int? MinutesOfManchineOnWhenStop { get; set; }

        public int? MinutesOfLossConnect { get; set; }

        public int? MinutesOfLossGps { get; set; }

        public int? TotalTimeStop { get; set; }

        public double? TotalKmAirConditioningOn { get; set; }

        public int? Vmax { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime? UpdateDate { get; set; }

        public int? TotalTimeStopMachineOn { get; set; }

        public int? TotalTimeStopMachineOff { get; set; }

        public byte? ChangeType { get; set; }

    }
}
