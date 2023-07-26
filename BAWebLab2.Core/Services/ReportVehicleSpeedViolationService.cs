using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Model;
using log4net;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace BAWebLab2.Core.Services
{
    /// <summary>class implement của IReportVehicleSpeedViolationService, chứa các service báo cáo vi phạm tốc độ cần</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    public class ReportVehicleSpeedViolationService : IReportVehicleSpeedViolationService
    {
        private readonly ILog _logger;

        private readonly IBGTSpeedOversService _bGTSpeedOversService;
        private readonly IVehiclesService _vehiclesService;
        private readonly IBGTTranportTypesService _bGTTranportTypesService;
        private readonly IBGTVehicleTransportTypesService _bGTVehicleTransportTypesService;
        private readonly IReportActivitySummariesService _reportActivitySummariesService;

        private readonly IDistributedCache _cache;
        
        public ReportVehicleSpeedViolationService(
            IBGTSpeedOversService bGTSpeedOversService, IReportActivitySummariesService reportActivitySummariesService,
            IVehiclesService vehiclesService, IBGTTranportTypesService bGTTranportTypesService,
            IBGTVehicleTransportTypesService bGTVehicleTransportTypesService, IDistributedCache cache)
        {
            _cache = cache;
            _logger = LogManager.GetLogger(typeof(ReportVehicleSpeedViolationService));
            _bGTSpeedOversService = bGTSpeedOversService;
            _vehiclesService = vehiclesService;
            _bGTTranportTypesService = bGTTranportTypesService;
            _bGTVehicleTransportTypesService = bGTVehicleTransportTypesService;
            _reportActivitySummariesService = reportActivitySummariesService;
        }

        /// <summary>lấy danh sách vehicle</summary>
        /// <returns>danh sách vehicle</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified> 
        public StoreResult<Vehicles> GetVehicles(int companyID)
        {
            var result = new StoreResult<Vehicles>();

            try
            {

                var list = _vehiclesService.Find(m => m.FK_CompanyID == companyID && m.IsDeleted == false).OrderBy(m => m.PrivateCode).ThenBy(o => o.PK_VehicleID).Cast<Vehicles>().ToList();
 
                result.List = list;

                result.Error = false;
            }
            catch (Exception ex)
            {
                
                result.Message = ex.Message;
                result.Error = true;
                _logger.Error(ex.ToString());
            }

            return result;
        }

        /// <summary>lấy dữ liệu báo cáo vi phạm tốc độ phương tiện</summary>
        /// <param name="input">đối tượng chứa các tham số báo cáo cần</param>
        /// <returns>dữ liệu báo cáo</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified> 
        public StoreResult<ResultReportSpeed> GetDataReport(InputSearchList input, int companyID)
        { 
            var result = new StoreResult<ResultReportSpeed>();

            try
            {
                var t = GetAllkeys();
                _logger.Warn(t.ToString());
                var final = GetIEnumerableCacheOrDB(input,companyID);
                result.Count = final.Count();
                result.List = final.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize).Select(m =>
                {
                    var violateTimeText = (Math.Floor((m.ViolateTime / 60))).ToString().PadLeft(2, '0') + ":" + (Math.Ceiling(m.ViolateTime % 60)).ToString().PadLeft(2, '0');
                    var totalTimeText = (Math.Floor((decimal)(m.TotalTime / 60))).ToString().PadLeft(2, '0') + ":" + (Math.Ceiling((decimal)m.TotalTime % 60)).ToString().PadLeft(2, '0');
                    return new ResultReportSpeed
                    {

                        VehicleID = m.VehicleID,
                        Sum5To10 = m.Sum5To10,
                        Sum10To20 = m.Sum10To20,
                        Sum20To35 = m.Sum20To35,
                        SumFrom35 = m.SumFrom35,
                        SumTotal = m.SumTotal,
                        ViolatePer100Km = Math.Round(((double)m.TotalKm > 1000 ? (m.SumTotal * 1000 / (double)m.TotalKm) : m.SumTotal), 2),

                        ViolateKm = m.ViolateKm,
                        TotalKm = m.TotalKm,
                        PercentRate = Math.Round(m.TotalKm == 0 ? 0 : (double)(m.ViolateKm * 100 / m.TotalKm), 2),
                         ViolateTimeText = violateTimeText == "00:00" ? "" : violateTimeText,
                          TotalTimeText = totalTimeText == "00:00" ? "" : totalTimeText,
                        PercentRateTime = Math.Round(m.TotalTime == 0 ? 0 : (double)((double)m.ViolateTime * 100 / m.TotalTime), 2),
                        PrivateCode = m.PrivateCode,
                        TransportTypeName = m.TransportTypeName

                    };
                }).ToList();

                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
                _logger.Error(ex.ToString());
                
            }

            return result;
        }


        public List<string> GetAllkeys()
        {
            List<string> listKeys = new List<string>();
            using (ConnectionMultiplexer redis = ConnectionMultiplexer.Connect("127.0.0.1:6379,allowAdmin=true"))
            {
                var keys = redis.GetServer("127.0.0.1", 6379).Keys();
                listKeys.AddRange(keys.Select(key => (string)key).ToList());

            }

            return listKeys;
        }

        /// <summary>check và lấy ienumerable từ  cache hoặc database.</summary>
        /// <param name="input">tham số tìm kiếm gửi từ client</param>
        /// <param name="companyID">mã công ty</param>
        /// <returns>
        ///   <br />
        /// </returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/26/2023 created
        /// </Modified>
        public IEnumerable<ResultReportSpeed> GetIEnumerableCacheOrDB(InputSearchList input, int companyID)
        {
            var key =  $"{companyID}_ReportSpeed_{input.DayFrom.ToString()}_{input.DayTo.ToString()}";
            key = key.Replace(':', ';');
            var cachedData = _cache.Get(key);
            var listVehicleID = new List<long>();
            if (!string.IsNullOrEmpty(input.TextSearch))
            {
                listVehicleID = input.TextSearch?.Split(',')?.Select(long.Parse)?.ToList();
            }
            
            IEnumerable<ResultReportSpeed> results;
            
            if (cachedData != null)
            {
                var cachedDataString = Encoding.UTF8.GetString(cachedData);
                 
                 results =    JsonConvert.DeserializeObject<IEnumerable<ResultReportSpeed>>(cachedDataString);
                 
            }
            else
            {
                results = GetIEnumerableAfterJoin(input, companyID);
                var cacheOptions = new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
                };
                
                var cachedDataString = JsonConvert.SerializeObject(results);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);

                _cache.Set(key, newDataToCache, cacheOptions);
                //return list;
            }
            if (string.IsNullOrEmpty(input.TextSearch))
            {
                return results;
            }
            else
            {
                return results.Where(m => listVehicleID.Contains(m.VehicleID));
            }
        }

        /// <summary>lấy  ienumerable sau khi join 5 bảng</summary>
        /// <param name="input">tham số tìm kiếm</param>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable&lt;ResultReportSpeed&gt; sau khi join</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/26/2023 created
        /// </Modified>
        public IEnumerable<ResultReportSpeed> GetIEnumerableAfterJoin(InputSearchList input, int companyID)
        {
            var bGTTranportTypes = _bGTTranportTypesService.GetAll();
            var bGTVehicleTransportTypes = _bGTVehicleTransportTypesService.GetAll();
            var vehicles = _vehiclesService.Find(m => m.FK_CompanyID == companyID && m.IsDeleted == false);

            var arrVehicle = input.TextSearch.Split(',');
            //var numbers = input.TextSearch?.Split(',')?.Select(long.Parse)?.ToList();
            var activityGroup = GetReportActivityGroup(input , arrVehicle,companyID);
            var bgtSpeedGroup = GetSpeedGroup(input, arrVehicle,companyID);
            var final =
             (from speed in bgtSpeedGroup
              join acti in activityGroup on speed.VehicleID equals acti.FK_VehicleID
              join vehicleTransportTypes in bGTVehicleTransportTypes on speed.VehicleID equals vehicleTransportTypes.FK_VehicleID
              join tranportTypes in bGTTranportTypes on vehicleTransportTypes.FK_TransportTypeID equals tranportTypes.PK_TransportTypeID
              join vehicle in vehicles on speed.VehicleID equals vehicle.PK_VehicleID

              orderby vehicle.PrivateCode
              select new ResultReportSpeed
              {
                  VehicleID = speed.VehicleID,
                  Sum5To10 = speed.Sum5To10,
                  Sum10To20 = speed.Sum10To20,
                  Sum20To35 = speed.Sum20To35,
                  SumFrom35 = speed.SumFrom35,
                  SumTotal = speed.SumTotal,
                  ViolateKm = Math.Round(speed.ViolateKm.HasValue ? (double)speed.ViolateKm : 0, 2),
                  TotalKm = Math.Round(acti.TotalKmGps.HasValue ? (double)acti.TotalKmGps : 0, 2),
                  ViolatePer100Km = 0,
                  PercentRate = 0,
                  PercentRateTime = 0,

                  ViolateTime = speed.ViolateTime,
                  TotalTime = acti.ActivityTime,

                  PrivateCode = vehicle.PrivateCode,
                  TransportTypeName = tranportTypes.DisplayName
              });
            return final;
        }


        /// <summary>lấy group của bảng report activity .</summary>
        /// <param name="input">tham số tìm kiếm</param>
        /// <param name="arrVehicle">list id vehicle</param>
        /// <param name="companyID">mã công ty</param>
        /// <returns> IEnumerable&lt;ReportActivitySummaries&gt; sau khi đã group</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/26/2023 created
        /// </Modified>
        public IEnumerable<ReportActivitySummaries> GetReportActivityGroup(InputSearchList input, string[]? arrVehicle,int companyID)
        { 
            var activity = _reportActivitySummariesService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
             && m.EndTime <= input.DayTo );

            var activityGroup = activity.GroupBy(m => m.FK_VehicleID).Select(k => new ReportActivitySummaries
            {
                FK_VehicleID = k.Key,
                ActivityTime = k.Sum(p => p.ActivityTime),
                TotalKmGps = k.Sum(p => p.TotalKmGps),
            });
            return activityGroup;
        }

        /// <summary>lấy group của bảng  speed over</summary>
        /// <param name="input">tham số tìm kiếm</param>
        /// <param name="arrVehicle">list id vehicle</param>
        /// <param name="companyID">mã công ty</param>
        /// <returns> IEnumerable&lt;ResultReportSpeed&gt; sau khi đã group</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/26/2023 created
        /// </Modified>
        public IEnumerable<ResultReportSpeed> GetSpeedGroup(InputSearchList input, string[]? arrVehicle, int companyID)
        { 
            var bgtSpeed = _bGTSpeedOversService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
              && m.EndTime <= input.DayTo );
            var bgtSpeedGroup
               = bgtSpeed.GroupBy(m => m.FK_VehicleID).ToList().Select(k =>
               {
                   int sum5To10 = k.Count(l => (l.VelocityAllow + 5) <= l.VelocityGps && (l.VelocityAllow + 10) > l.VelocityGps);
                   int sum10To20 = k.Count(l => (l.VelocityAllow + 10) <= l.VelocityGps && (l.VelocityAllow + 20) > l.VelocityGps);
                   int sum20To35 = k.Count(l => (l.VelocityAllow + 20) <= l.VelocityGps && (l.VelocityAllow + 35) > l.VelocityGps);
                   int sumFrom35 = k.Count(l => (l.VelocityAllow + 35) < l.VelocityGps);
                   int sumTotal = sum5To10 + sum10To20 + sum20To35 + sumFrom35;
                   var sumTime = sumTotal == 0 ? 0 : k.Where(p => p.VelocityGps >= (p.VelocityAllow + 5)).Sum(p => p.EndTime == null ? 0 : (((DateTime)p.EndTime).Subtract(p.StartTime).TotalSeconds));
                   double? sumKm = sumTotal == 0 ? 0 : k.Where(p => p.VelocityGps >= (p.VelocityAllow + 5)).Sum(m => (m.EndKm - m.StartKm));
                   return new ResultReportSpeed
                   {
                       VehicleID = k.Key,
                       Sum5To10 = sum5To10,
                       Sum10To20 = sum10To20,
                       Sum20To35 = sum20To35,
                       SumFrom35 = sumFrom35,
                       SumTotal = sumTotal,
                       ViolateKm = sumKm,
                       ViolateTime = Convert.ToDouble(Math.Round(sumTime / 60, 2)),

                   };

               });
            return bgtSpeedGroup;

        }

    }
}
