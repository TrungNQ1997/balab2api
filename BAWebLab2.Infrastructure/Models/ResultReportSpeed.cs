using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Models
{
    public class ResultReportSpeed
    {

        public ResultReportSpeed() {
        
        }
        public int Stt { get; set; }

        public string PrivateCode { get; set; }

        public string TransportTypeName { get; set; }

        public long VehicleID { get; set; }

        public int Sum1 { get; set; }
        public int Sum2 { get; set; }

        public int Sum3 { get; set; }


        public int Sum4 { get; set; }

        public int SumTotal { get; set; }

        public Double ViolatePer100Km { get; set; }

        public Double? ViolateKm { get; set; }

        public Double? TotalKm { get; set; }

        public Double PercentRate { get; set; }

        public DateTime ViolateTime { get; set; }

        public int? TotalTime { get; set; }

        public Double PercentRateTime { get; set; }
    }
}
