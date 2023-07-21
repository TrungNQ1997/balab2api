using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Model;
using BAWebLab2.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace BAWebLab2.Infrastructure.Repositories
{
    /// <summary>class implement của IReportVehicleSpeedViolationRepository, chứa các hàm cần cho báo cáo vi phạm tốc độ</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    public class ReportVehicleSpeedViolationRepository : GenericRepository<User>, IReportVehicleSpeedViolationRepository
    {
        private readonly BADbContext _bADbContext;
        public ReportVehicleSpeedViolationRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

        /// <summary>lấy danh sách vehicle</summary>
        /// <returns>danh sách vehicle</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified> 
        public MultipleResult<Vehicles> GetVehicles()
        {
            var myObject = new MultipleResult<Vehicles>();


            using (var connection = _context.Database.GetDbConnection())
            {

                myObject.ListPrimary = _context.Vehicles.Where(m => m.FK_CompanyID == 15076 && m.IsDeleted == false).OrderBy(m => m.PrivateCode).ThenBy(o => o.PK_VehicleID).Cast<Vehicles>().ToList();

            }
            return myObject;
        }

        /// <summary>lấy dữ liệu báo báo vi phạm tốc độ</summary>
        /// <param name="input">chứa tham số báo cáo</param>
        /// <returns>list dữ liệu báo cáo</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified> 
        public StoreResult<ResultReportSpeed> GetDataReport(InputReportSpeed input)
        {
            var result = new StoreResult<ResultReportSpeed>();
            var multi = new MultipleResult<ResultReportSpeed>();
            var arrVehicle = input.TextSearch.Split(',');

            using (var connection = _context.Database.GetDbConnection())
            {

                var bgtSpeed = _context.BGTSpeedOvers.Where(m => m.StartTime >= input.BirthdayFrom
                && m.EndTime <= input.BirthdayTo && m.FK_CompanyID == 15076 && ((input.TextSearch == null || input.TextSearch == "") || arrVehicle.Contains(m.FK_VehicleID.ToString()))).Select(k => new
                {
                    FK_VehicleID = k.FK_VehicleID,
                    VelocityAllow = k.VelocityAllow,
                    VelocityGps = k.VelocityGps,
                    StartKm = k.StartKm,
                    StartTime = k.StartTime,
                    EndKm = k.EndKm,
                    EndTime = k.EndTime,

                });

                var activity = _context.ReportActivitySummaries.Where(m => m.StartTime >= input.BirthdayFrom
               && m.EndTime <= input.BirthdayTo && m.FK_CompanyID == 15076 && ((input.TextSearch == null || input.TextSearch == "") || arrVehicle.Contains(m.FK_VehicleID.ToString()))).Select(k => new
               {
                   VehicleID = k.FK_VehicleID,
                   ActivityTime = k.ActivityTime,
                   TotalKmGps = k.TotalKmGps,
               });

                //var query = from leftItem in bgtSpeed
                //            join rightItem in activity
                //            on new {  a1 = leftItem.FK_VehicleID, a2 = leftItem.StartKm} equals new { a1 =  rightItem.VehicleID, a2 = rightItem.TotalKmGps}  into group1
                //            from r1 in group1.DefaultIfEmpty()
                //            select new
                //            {   
                //                LeftItem = leftItem.StartTime,
                //                RightItem = r1.ActivityTime // sẽ là null nếu không có cặp phù hợp trong bảng phải
                //            };


                var bGTTranportTypes = _context.BGTTranportTypes;
                var bGTVehicleTransportTypes = _context.BGTVehicleTransportTypes.Where(m => m.FK_CompanyID == 15076);
                var vehicles = _context.Vehicles.Where(m => m.FK_CompanyID == 15076 && m.IsDeleted == false);

                var activityGroup = activity.GroupBy(m => m.VehicleID).Select(k => new
                {
                    VehicleID = k.Key,
                    SumActivityTime = k.Sum(p => p.ActivityTime),
                    TotalKm = k.Sum(p => p.TotalKmGps),
                }

                   );

                var bgtSpeedGroup
                    = bgtSpeed.GroupBy(m => m.FK_VehicleID).ToList().Select(k =>
                    {
                        int sum1 = k.Count(l => (l.VelocityAllow + 5) <= l.VelocityGps && (l.VelocityAllow + 10) > l.VelocityGps);
                        int sum2 = k.Count(l => (l.VelocityAllow + 10) <= l.VelocityGps && (l.VelocityAllow + 20) > l.VelocityGps);
                        int sum3 = k.Count(l => (l.VelocityAllow + 20) <= l.VelocityGps && (l.VelocityAllow + 35) > l.VelocityGps);
                        int sum4 = k.Count(l => (l.VelocityAllow + 35) < l.VelocityGps);
                        int sumTotal = sum1 + sum2 + sum3 + sum4;
                        var sumTime = sumTotal == 0 ? 0 : k.Where(p => p.VelocityGps >= (p.VelocityAllow + 5)).Sum(p => p.EndTime == null ? 0 : (((DateTime)p.EndTime).Subtract(p.StartTime).TotalSeconds));
                        double? sumKm = sumTotal == 0 ? 0 : k.Where(p => p.VelocityGps >= (p.VelocityAllow + 5)).Sum(m => (m.EndKm - m.StartKm));
                        return new
                        {
                            VehicleID = k.Key,
                            Sum1 = sum1,
                            Sum2 = sum2,
                            Sum3 = sum3,
                            Sum4 = sum4,
                            SumTotal = sumTotal,
                            ViolateKm = sumKm,
                            ViolateTime = Convert.ToDouble(Math.Round(sumTime / 60, 2)),

                        };

                    });

                var final =
                 (from speed in bgtSpeedGroup
                  join acti in activityGroup on speed.VehicleID equals acti.VehicleID
                  join vehicleTransportTypes in bGTVehicleTransportTypes on speed.VehicleID equals vehicleTransportTypes.FK_VehicleID
                  join tranportTypes in bGTTranportTypes on vehicleTransportTypes.FK_TransportTypeID equals tranportTypes.PK_TransportTypeID
                  join vehicle in vehicles on speed.VehicleID equals vehicle.PK_VehicleID

                  orderby vehicle.PrivateCode
                  select new { speed = speed, vehicleTransportTypes = vehicleTransportTypes, acti = acti, tranportTypes = tranportTypes, vehicle = vehicle })
                  .Select((item, Index) => new
                  {
                      Stt = Index + 1,
                      VehicleID = item.speed.VehicleID,
                      Sum1 = item.speed.Sum1,
                      Sum2 = item.speed.Sum2,
                      Sum3 = item.speed.Sum3,
                      Sum4 = item.speed.Sum4,
                      SumTotal = item.speed.SumTotal,
                      //ViolatePer100Km = Math.Round(((double)item.acti.TotalKm > 1000 ? (item.speed.SumTotal * 1000 / (double)item.acti.TotalKm) : item.speed.SumTotal), 2),

                      ViolateKm = Math.Round(item.speed.ViolateKm.HasValue ? (double)item.speed.ViolateKm : 0, 2),
                      TotalKm = Math.Round(item.acti.TotalKm.HasValue ? (double)item.acti.TotalKm : 0, 2),
                      //PercentRate = Math.Round(item.acti.TotalKm == 0 ? 0 : (double)(item.speed.ViolateKm * 100 / item.acti.TotalKm), 2),
                      //PercentRateTime = Math.Round(item.acti.SumActivityTime == 0 ? 0 : (double)((double)item.speed.ViolateTime * 100 / item.acti.SumActivityTime), 2),

                      ViolatePer100Km = 0,

                      //ViolateKm = 0,
                      //TotalKm = 0,
                      PercentRate = 0,
                      PercentRateTime = 0,

                      ViolateTime = item.speed.ViolateTime,
                      TotalTime = item.acti.SumActivityTime,

                      PrivateCode = item.vehicle.PrivateCode,
                      TransportTypeName = item.tranportTypes.DisplayName

                  });

                result.Count = final.Count();
                result.List = final.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize).Select(m =>
                {
                    var violateTimeText = (Math.Floor((m.ViolateTime / 60))).ToString().PadLeft(2, '0') + ":" + (Math.Ceiling(m.ViolateTime % 60)).ToString().PadLeft(2, '0');
                    var totalTimeText = (Math.Floor((decimal)(m.TotalTime / 60))).ToString().PadLeft(2, '0') + ":" + (Math.Ceiling((decimal)m.TotalTime % 60)).ToString().PadLeft(2, '0');
                    return new ResultReportSpeed
                    {
                        Stt = m.Stt,
                        VehicleID = m.VehicleID,
                        Sum1 = m.Sum1,
                        Sum2 = m.Sum2,
                        Sum3 = m.Sum3,
                        Sum4 = m.Sum4,
                        SumTotal = m.SumTotal,
                        ViolatePer100Km = Math.Round(((double)m.TotalKm > 1000 ? (m.SumTotal * 1000 / (double)m.TotalKm) : m.SumTotal), 2),

                        ViolateKm = m.ViolateKm,
                        TotalKm = m.TotalKm,
                        PercentRate = Math.Round(m.TotalKm == 0 ? 0 : (double)(m.ViolateKm * 100 / m.TotalKm), 2),

                        ViolateTime = m.ViolateTime,
                        ViolateTimeText = violateTimeText == "00:00" ? "" : violateTimeText,
                        TotalTime = m.TotalTime,
                        TotalTimeText = totalTimeText == "00:00" ? "" : totalTimeText,
                        PercentRateTime = Math.Round(m.TotalTime == 0 ? 0 : (double)((double)m.ViolateTime * 100 / m.TotalTime), 2),
                        PrivateCode = m.PrivateCode,
                        TransportTypeName = m.TransportTypeName

                    };
                }).ToList();

                //multi.ListPrimary = final.Cast<ResultReportSpeed>().ToList();

            }
            return result;
        }

    }
}
