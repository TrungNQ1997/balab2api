namespace BAWebLab2.Infrastructure.Models
{
    /// <summary>class chứa kết quả trả về của báo cáo vi phạm tốc độ phương tiện</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    public class ResultReportSpeed
    {

        public ResultReportSpeed() {
        
        }

        public string PrivateCode { get; set; }

        public string TransportTypeName { get; set; }

        public long VehicleID { get; set; }

        public int Sum5To10 { get; set; }

        public int Sum10To20 { get; set; }

        public int Sum20To35 { get; set; }
         
        public int SumFrom35 { get; set; }

        public int SumTotal { get; set; }

        public Double? ViolatePer100Km { get; set; }

        public Double? ViolateKm { get; set; }

        public Double? TotalKm { get; set; }

        public Double? PercentRate { get; set; }

        public double ViolateTime { get; set; }

        public string ViolateTimeText { get; set; }

        public int? TotalTime { get; set; }

        public string TotalTimeText { get; set; }

        public Double? PercentRateTime { get; set; }
    }
}
