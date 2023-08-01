using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using BAWebLab2;
using Newtonsoft.Json;
using BAWebLab2.Core.LibCommon;

namespace BAWebLab2.Core.Services
{
    /// <summary>class implement của IReportVehicleSpeedViolationService, chứa các service báo cáo vi phạm tốc độ cần</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    public class ReportVehicleSpeedViolationService : IReportVehicleSpeedViolationService
    { 
        private readonly IBGTSpeedOversService _bGTSpeedOversService;
        private readonly IVehiclesService _vehiclesService;
        private readonly IBGTTranportTypesService _bGTTranportTypesService;
        private readonly IBGTVehicleTransportTypesService _bGTVehicleTransportTypesService;
        private readonly IReportActivitySummariesService _reportActivitySummariesService;
        private readonly CacheRedisService _cache;
        private readonly FormatDataService _formatService;

        public ReportVehicleSpeedViolationService(
            IBGTSpeedOversService bGTSpeedOversService, IReportActivitySummariesService reportActivitySummariesService,
            IVehiclesService vehiclesService, IBGTTranportTypesService bGTTranportTypesService,
            IBGTVehicleTransportTypesService bGTVehicleTransportTypesService,
            CacheRedisService cache, FormatDataService formatService)
        {
            
            _cache = cache;
            _formatService = formatService;
            _bGTSpeedOversService = bGTSpeedOversService;
            _vehiclesService = vehiclesService;
            _bGTTranportTypesService = bGTTranportTypesService;
            _bGTVehicleTransportTypesService = bGTVehicleTransportTypesService;
            _reportActivitySummariesService = reportActivitySummariesService;
        }

        /// <summary>lấy danh sách vehicle</summary>
        /// <param name="companyID">mã công ty</param>
        /// <returns>danh sách vehicle</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified>
        public StoreResult<Vehicles> GetVehicles(int companyID)
        {
            var result = new StoreResult<Vehicles>();
         //var e =    _formatService.RoundDouble(3.423432);
            try
            {
                var key = _cache.CreateKeyByModule("Vehicle", companyID);

                var listCache = _cache.GetRedisCache<IEnumerable<Vehicles>>(key);

                if (listCache != null)
                {
                    result.List = listCache.Cast<Vehicles>().ToList();
                } else
                {
                    var list = _vehiclesService.FindByCompanyID(companyID).OrderBy(m => m.PrivateCode).ThenBy(o => o.PK_VehicleID);
                    _cache.PushDataToCache(list, TimeSpan.FromMinutes(5), key);
                     
                    result.List = list.Cast<Vehicles>().ToList();
                }

               

                result.Error = false;
            }
            catch (Exception ex)
            {
                
                result.Message = ex.Message;
                result.Error = true;
                LogService.LogError(ex.ToString());
            }

            return result;
        }

        /// <summary>lấy dữ liệu báo cáo vi phạm tốc độ phương tiện</summary>
        /// <param name="input">đối tượng chứa các tham số báo cáo cần</param>
        /// <param name="companyID">mã công ty</param>
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
                var final = GetListCacheOrDB(input,companyID, ref result); 
                result.List = final; 
                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
                LogService.LogError(ex.ToString());
                
            }

            return result;
        }


        /// <summary>tính toán dữ liệu, phân trang</summary>
        /// <param name="input">tham số tìm kiếm</param>
        /// <param name="ienum">ienumerable sau khi đã join</param>
        /// <returns>list kết quả báo cáo đã phân trang</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        private List<ResultReportSpeed> CalDataAndPaging(InputSearchList input, IEnumerable<ResultReportSpeed> ienumReport)
        {
            var listReturn = (ReportService.PagingIEnumerable<ResultReportSpeed>(input,ienumReport)).Select(m =>
            {
                var violateTimeText = FormatDataService.NumberMinuteToStringHour(m.ViolateTime); 

                var totalTimeText = FormatDataService.NumberMinuteToStringHour(m.TotalTime);

                return new ResultReportSpeed
                {

                    VehicleID = m.VehicleID,
                    Sum5To10 = m.Sum5To10,
                    Sum10To20 = m.Sum10To20,
                    Sum20To35 = m.Sum20To35,
                    SumFrom35 = m.SumFrom35,
                    SumTotal = m.SumTotal,
                    ViolatePer100Km = m.TotalKm == 0 || m.TotalKm == null ? 0 : _formatService.RoundDouble(m.TotalKm > 1000 ? (m.SumTotal * 1000 / (double)m.TotalKm) : m.SumTotal), 

                    ViolateKm = m.ViolateKm,
                    TotalKm = m.TotalKm,
                    PercentRate =  m.TotalKm == 0 || m.TotalKm == null ? 0 :  _formatService.RoundDouble((m.ViolateKm * 100 / m.TotalKm)), //(double)(m.ViolateKm * 100 / m.TotalKm), 2),
                    ViolateTimeText = violateTimeText ,
                    TotalTimeText = totalTimeText ,
                    PercentRateTime =  m.TotalTime == 0 || m.TotalTime == null ? 0 : _formatService.RoundDouble( ( m.ViolateTime * 100 / m.TotalTime)),
                    PrivateCode = m.PrivateCode,
                    TransportTypeName = m.TransportTypeName

                };
            
            }).ToList();
            return listReturn;
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
        private List<ResultReportSpeed> GetListCacheOrDB(InputSearchList input, int companyID, ref StoreResult<ResultReportSpeed> storeResult)
        {
            // tạo key redis
            var keyList = _cache.CreateKeyReport("ReportSpeed", companyID, input);
            var listReturn = new List<ResultReportSpeed>();
            var listCache = _cache.GetRedisCache<IEnumerable<ResultReportSpeed>>(keyList);
            IEnumerable<ResultReportSpeed> results;
            
            // check có cache không?
            // đã có cache thì phân trang và trả về list
            if (listCache != null) 
            { 
                storeResult.Count = listCache.Count();
                listReturn = CalDataAndPaging(input, listCache);
                
            }
            // chưa có cache thì get lại db và lưu lại ienumable vào cache
            else
            {
                results = GetIEnumerableAfterJoin(input, companyID); 
                _cache.PushDataToCache(results, TimeSpan.FromMinutes(5), keyList);
                storeResult.Count = results.Count();
                var list = CalDataAndPaging(input, results); 
                listReturn = list;
            }
           return listReturn;
        }

        /// <summary>lấy  ienumerable sau khi join 5 bảng</summary>
        /// <param name="input">tham số tìm kiếm</param>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable&lt;ResultReportSpeed&gt; sau khi join</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/26/2023 created
        /// </Modified>
        private IEnumerable<ResultReportSpeed> GetIEnumerableAfterJoin(InputSearchList input, int companyID)
        {
            // parce string truyền vào để lấy list id vehicle
            var listVehicleID = FormatDataService.StringToListLong(input.TextSearch);

            // lấy ra 5 bảng cần join đã lọc theo tham số đầu vào
            var bGTTranportTypes = _bGTTranportTypesService.GetAll();
            var bGTVehicleTransportTypes = _bGTVehicleTransportTypesService.GetByCompanyID(companyID);
            var vehicles = _vehiclesService.FindByCompanyID(companyID); 
            var activityGroup = GetReportActivityGroup(input , listVehicleID, companyID);
            var bgtSpeedGroup = GetSpeedGroup(input, listVehicleID, companyID);

            // join 5 bảng lấy dữ liệu tổng hợp
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
                 ViolateKm =  _formatService.RoundDouble(speed.ViolateKm),
                 TotalKm = _formatService.RoundDouble(acti.TotalKmGps),
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

        /// <summary>lấy  ienumerable sau khi join 5 bảng viết kiểu lambda</summary>
        /// <param name="input">tham số tìm kiếm</param>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable&lt;ResultReportSpeed&gt; sau khi join</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/26/2023 created
        /// </Modified>
        private IEnumerable<ResultReportSpeed> GetIEnumerableAfterJoinlambda(InputSearchList input, int companyID)
        {
            var listVehicleID = FormatDataService.StringToListLong(input.TextSearch);
            var bGTTranportTypes = _bGTTranportTypesService.GetAll();
            var bGTVehicleTransportTypes = _bGTVehicleTransportTypesService.GetByCompanyID(companyID);
            var vehicles = _vehiclesService.FindByCompanyID(companyID);

            var activityGroup = GetReportActivityGroup(input, listVehicleID, companyID);
            var bgtSpeedGroup = GetSpeedGroup(input, listVehicleID, companyID);
            var final =
             bgtSpeedGroup
              .GroupJoin(activityGroup,
              speed => speed.VehicleID,
              acti => acti.FK_VehicleID,
              (speed, acti) => new { acti = acti.DefaultIfEmpty(), speed = speed })
              .SelectMany(m=>m.acti,(group, acti)=> new {acti = acti, speed = group.speed})
               
              .GroupJoin(bGTVehicleTransportTypes,
              actiSpeed => actiSpeed.speed.VehicleID,
              vehiTranType => vehiTranType.FK_VehicleID,
              (actiSpeed, vehiTranType) => new { actiSpeed = actiSpeed, vehiTranType = vehiTranType.DefaultIfEmpty() })
              .SelectMany(m => m.vehiTranType, (group, vehiTranType) => new { actiSpeed = group.actiSpeed, vehiTranType = vehiTranType })
               
              .GroupJoin(bGTTranportTypes,
              actiSpeedVehiTranType => actiSpeedVehiTranType.vehiTranType == null ? 0 : actiSpeedVehiTranType.vehiTranType.FK_TransportTypeID,
              tranType => tranType.PK_TransportTypeID,
              (actiSpeedVehiTranType, tranType) => new { actiSpeedVehiTranType = actiSpeedVehiTranType, tranType = tranType })
              .SelectMany(m=>m.tranType, (actiSpeedVehiTranType, tranType)=> new { actiSpeedVehiTranType = actiSpeedVehiTranType.actiSpeedVehiTranType,tranType = tranType} )

              .GroupJoin(vehicles,
               all => all.actiSpeedVehiTranType.actiSpeed.speed.VehicleID,
               vehi => vehi.PK_VehicleID,
               (all, vehi) => new { all = all, vehi = vehi.DefaultIfEmpty() })
              .SelectMany(m=>m.vehi, (all, vehi)=> new { all = all.all , vehi = vehi} )

               .OrderBy(m => m.vehi?.PrivateCode).Select( k=> 
                 new ResultReportSpeed
                 {
                     VehicleID = k.all.actiSpeedVehiTranType.actiSpeed.speed.VehicleID,
                     Sum5To10 = k.all.actiSpeedVehiTranType.actiSpeed.speed.Sum5To10,
                     Sum10To20 = k.all.actiSpeedVehiTranType.actiSpeed.speed.Sum10To20,
                     Sum20To35 = k.all.actiSpeedVehiTranType.actiSpeed.speed.Sum20To35,
                     SumFrom35 = k.all.actiSpeedVehiTranType.actiSpeed.speed.SumFrom35,
                     SumTotal = k.all.actiSpeedVehiTranType.actiSpeed.speed.SumTotal,
                     ViolateKm = Math.Round(k.all.actiSpeedVehiTranType.actiSpeed.speed.ViolateKm.HasValue ? (double)k.all.actiSpeedVehiTranType.actiSpeed.speed.ViolateKm : 0, 2),
                     TotalKm = k.all.actiSpeedVehiTranType.actiSpeed.acti == null ? 0 :  Math.Round(k.all.actiSpeedVehiTranType.actiSpeed.acti.TotalKmGps.HasValue ? (double)k.all.actiSpeedVehiTranType.actiSpeed.acti.TotalKmGps : 0, 2),
                     ViolatePer100Km = 0,
                     PercentRate = 0,
                     PercentRateTime = 0,

                     ViolateTime = k.all.actiSpeedVehiTranType.actiSpeed.speed.ViolateTime,
                     TotalTime = k.all.actiSpeedVehiTranType.actiSpeed.acti == null ? 0 : k.all.actiSpeedVehiTranType.actiSpeed.acti.ActivityTime,

                     PrivateCode = k.vehi?.PrivateCode,
                     TransportTypeName = k.all.tranType.DisplayName
                 }
                 );
 
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
        private IEnumerable<ReportActivitySummaries> GetReportActivityGroup(InputSearchList input, List<long> arrVehicle,int companyID)
        {
            IEnumerable<ReportActivitySummaries> activity;
            // lọc bảng activity theo tham số đầu vào
            if (string.IsNullOrEmpty(input.TextSearch))
            {
                activity = _reportActivitySummariesService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
            && m.EndTime <= input.DayTo);
            }
            else
            {
                activity = _reportActivitySummariesService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
           && m.EndTime <= input.DayTo &&   (arrVehicle).Contains(m.FK_VehicleID));
            }

            // gom nhóm bảng activity theo FK_VehicleID
            var activityGroup = activity.GroupBy(m => m.FK_VehicleID).Select(k => new ReportActivitySummaries
            {
                FK_VehicleID = k.Key,
                ActivityTime = k.Sum(p => p.ActivityTime),
                TotalKmGps = k.Sum(p => p.TotalKmGps),
            });
            return activityGroup;
        }

        /// <summary>lấy ienumerable của speedOver sau khi đã lọc theo tham số đầu vào</summary>
        /// <param name="input">tham số tìm kiếm</param>
        /// <param name="arrVehicle">list id vehicle.</param>
        /// <param name="companyID">mã công ty</param>
        /// <returns>danh sách đã lọc</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        private IEnumerable<BGTSpeedOvers> GetSpeedFilter(InputSearchList input, List<long> arrVehicle, int companyID)
        {
            IEnumerable<BGTSpeedOvers> bgtSpeed;
            if (string.IsNullOrEmpty(input.TextSearch))
            {
                bgtSpeed = _bGTSpeedOversService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
             && m.EndTime <= input.DayTo);
            }
            else
            {
                bgtSpeed = _bGTSpeedOversService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
             && m.EndTime <= input.DayTo && arrVehicle.Contains(m.FK_VehicleID));
            }
            return bgtSpeed;
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
        private IEnumerable<ResultReportSpeed> GetSpeedGroup(InputSearchList input, List<long> arrVehicle, int companyID)
        {
            var bgtSpeed = GetSpeedFilter(input, arrVehicle, companyID);
             
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
