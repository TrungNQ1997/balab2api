using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Model;
using BAWebLab2.Repository;
using Microsoft.EntityFrameworkCore;

namespace BAWebLab2.Infrastructure.Repositories
{
    public class ReportVehicleSpeedViolationRepository : GenericRepository<User>, IReportVehicleSpeedViolationRepository
    {
        private readonly BADbContext _bADbContext;
        public ReportVehicleSpeedViolationRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

        public MultipleResult<Vehicles> GetVehicles<Vehicles>()
        {
            var myObject = new MultipleResult<Vehicles>();


            using (var connection = _context.Database.GetDbConnection())
            {

                myObject.ListPrimary = _context.Vehicles.Cast<Vehicles>().ToList();

            }
            return myObject;
        }

        public StoreResult<ResultReportSpeed> GetDataReport<ResultReportSpeed>(InputReportSpeed input)
        {
             
            var result = new StoreResult<ResultReportSpeed>();
            var arrVehicle = input.TextSearch.Split(',');

            using (var connection = _context.Database.GetDbConnection())
            {
                 
                var bgtSpeed = _context.BGTSpeedOvers.Where(m => m.StartTime >= input.BirthdayFrom
                && m.EndTime <= input.BirthdayTo && m.FK_CompanyID == 15076 && ((input.TextSearch ==null || input.TextSearch =="") ||   arrVehicle.Contains(m.FK_VehicleID.ToString()) )).Select(k => new
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

                var bGTTranportTypes = _context.BGTTranportTypes;
                var bGTVehicleTransportTypes = _context.BGTVehicleTransportTypes ;
                var vehicle = _context.Vehicles ;
                 
                var activityGroup = activity.GroupBy(m => m.VehicleID).Select(k => new
                {
                    VehicleID = k.Key,
                    SumActivityTime = k.Sum(p => p.ActivityTime),
                    TotalKm =  k.Sum(p => p.TotalKmGps),
                }

                   );
                 
                var bgtSpeedGroup
                    = bgtSpeed.GroupBy(m => m.FK_VehicleID).ToList().Select(k => 
                    {
                        int sum1 = k.Count(l => (l.VelocityAllow + 5) <= l.VelocityGps && (l.VelocityAllow + 10) > l.VelocityGps);
                        int sum2 = k.Count(l => (l.VelocityAllow + 10) <= l.VelocityGps && (l.VelocityAllow + 20) > l.VelocityGps);
                        int sum3 = k.Count(l => (l.VelocityAllow + 20) <= l.VelocityGps && (l.VelocityAllow + 35) > l.VelocityGps);
                        int sum4 = k.Count(l => (l.VelocityAllow + 35) < l.VelocityGps);
                        var sumTime = k.Sum(p =>  p.EndTime == null? 0 :  (((DateTime)p.EndTime).Subtract(p.StartTime == null ? (DateTime)p.EndTime : (DateTime)p.StartTime).TotalMinutes));
                        double? sumKm = k.Sum(m => (m.EndKm - m.StartKm));
                        return new 
                        {
                            VehicleID = k.Key,
                            Sum1 = sum1,
                            Sum2 = sum2,
                            Sum3 = sum3,
                            Sum4 = sum4,
                            SumTotal = sum1 + sum2 + sum3 + sum4,
                            ViolateKm = sumKm,
                            ViolateTime = Convert.ToInt32(Math.Round(sumTime, 0))

                        };
                         
                    });
                 
                var stt = 1;
                var final =
                  from speed in bgtSpeedGroup
                  join acti in activityGroup on speed.VehicleID equals acti.VehicleID
                  join vehicleTransportTypes in bGTVehicleTransportTypes on speed.VehicleID equals vehicleTransportTypes.FK_VehicleID
                  join tranportTypes in bGTTranportTypes on vehicleTransportTypes.FK_TransportTypeID equals tranportTypes.PK_TransportTypeID
                  join vehi in vehicle on speed.VehicleID equals vehi.PK_VehicleID
                  orderby vehi.PrivateCode
                  select new BAWebLab2.Infrastructure.Models.ResultReportSpeed
                  {
                      Stt = stt++,
                      VehicleID = speed.VehicleID,
                      Sum1 = speed.Sum1,
                      Sum2 = speed.Sum2,
                      Sum3 = speed.Sum3,
                      Sum4 = speed.Sum4,
                      SumTotal = speed.SumTotal,
                      ViolatePer100Km = Math.Round(((double)acti.TotalKm > 1000 ? (speed.SumTotal * 1000 / (double)acti.TotalKm) : speed.SumTotal), 2),
                       
                      ViolateKm = Math.Round(speed.ViolateKm.HasValue ? (double)speed.ViolateKm : 0, 2),
                      TotalKm = Math.Round(acti.TotalKm.HasValue ? (double)acti.TotalKm : 0, 2),
                      PercentRate = Math.Round(acti.TotalKm == 0 ? 0 : (double)(speed.ViolateKm * 100 / acti.TotalKm), 2),

                      ViolateTime = speed.ViolateTime,
                      TotalTime = acti.SumActivityTime,
                      PercentRateTime = Math.Round(acti.SumActivityTime == 0 ? 0 : (double)((double)speed.ViolateTime * 100 / acti.SumActivityTime), 2),
                      PrivateCode = vehi.PrivateCode,
                      TransportTypeName = tranportTypes.DisplayName
                       
                  }
                  
                  ;
                
                result.List = final.Cast<ResultReportSpeed>().Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize).ToList();
                result.Count = final.Count();

            }
            return result;
        }



    }
}
