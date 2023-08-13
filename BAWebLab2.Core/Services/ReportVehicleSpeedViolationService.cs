using BAWebLab2.Core.LibCommon;
using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using log4net;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;

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
		private readonly IUserTokenService _userTokenService;
        private readonly CacheRedisHelper _cacheHelper;
        private readonly FormatDataHelper _formatDataHelper;
        private readonly ILog _logger = LogManager.GetLogger(typeof(ReportVehicleSpeedViolationService));

        public ReportVehicleSpeedViolationService(
            IBGTSpeedOversService bGTSpeedOversService, IReportActivitySummariesService reportActivitySummariesService,
            IVehiclesService vehiclesService, IBGTTranportTypesService bGTTranportTypesService,
            IUserTokenService userTokenService,
            IBGTVehicleTransportTypesService bGTVehicleTransportTypesService,
            CacheRedisHelper cacheHelper, FormatDataHelper formatDataHelper)
        {
            _cacheHelper = cacheHelper;
            _formatDataHelper = formatDataHelper;
            _bGTSpeedOversService = bGTSpeedOversService;
            _userTokenService = userTokenService;
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
        public StoreResult<Vehicles> GetVehicles( UserToken userToken)
        {
            var result = new StoreResult<Vehicles>(); 
            try
            {
                if (_userTokenService.FakeDataAndCheckToken(userToken))
                {
                    // tạo key redis cache
                    var key = _cacheHelper.CreateKeyByModule("Vehicle", userToken.CompanyID);
                    var ienumerable = _cacheHelper.GetSortedSetMembers<Vehicles>(key);

                    // check xem đã có cache vehicle chưa
                    // có rồi thì trả về list cache
                    if (ienumerable is not null)
                    {
                        result.iEnumerable = ienumerable.OrderBy(m => m.PrivateCode);
                    }
                    // chưa có thì lấy trong db, lưu vào cache sau đó trả về list
                    else
                    {
                        var list = _vehiclesService.FindByCompanyID(userToken.CompanyID).OrderBy(m => m.PrivateCode).ThenBy(o => o.PK_VehicleID);
                        result.iEnumerable = list;
                        _cacheHelper.AddEnumerableToSortedSet<Vehicles>(key, list, TimeSpan.FromMinutes(5));
                    }
                    result.Error = false;
                } else
                {
                    var error = "wrong user token ";
                    LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
                }
            }
            catch (Exception ex)
            {
                result.Message.Add(ex.Message);
                result.Error = true;
                LogHelper.LogErrorInClass("data userToken " + JsonConvert.SerializeObject(userToken) + " error " + ex.ToString(), _logger);
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
        public StoreResult<ResultReportSpeed> GetDataReport(InputReport input, UserToken userToken)
        {
            var result = new StoreResult<ResultReportSpeed>();
            try
            {
				if (_userTokenService.FakeDataAndCheckToken(userToken))
				{
					var final = GetListCacheOrDB(input, userToken.CompanyID, ref result);
                result.iEnumerable = final;
                result.Error = false;
				}
				else
				{
					var error = "wrong user token ";
					LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
				}
			}
            catch (Exception ex)
            {
                result.Message.Add(ex.Message);
                result.Error = true;
                LogHelper.LogErrorInClass("data inputReport " + JsonConvert.SerializeObject(input) + " companyId " + JsonConvert.SerializeObject(userToken.CompanyID) +" error "+ ex.ToString(),_logger);
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
        private IEnumerable<ResultReportSpeed> CalData(IEnumerable<ResultReportSpeed> ienumReport)
        {
            IEnumerable<ResultReportSpeed> listReturn = new List<ResultReportSpeed>();
            try
            {
                listReturn = ienumReport.Select(m =>
                {
                    var violateTimeText = FormatDataHelper.NumberMinuteToStringHour(m.ViolateTime);
                    var totalTimeText = FormatDataHelper.NumberMinuteToStringHour(m.TotalTime);

                    return new ResultReportSpeed
                    {
                        VehicleID = m.VehicleID,
                        Sum5To10 = m.Sum5To10,
                        Sum10To20 = m.Sum10To20,
                        Sum20To35 = m.Sum20To35,
                        SumFrom35 = m.SumFrom35,
                        SumTotal = m.SumTotal,
                        ViolatePer100Km = m.TotalKm == 0 || m.TotalKm == null ? 0 : _formatDataHelper.RoundDouble(m.TotalKm > 1000 ? (m.SumTotal * 1000 / (double)m.TotalKm) : m.SumTotal),
                        ViolateKm = m.ViolateKm,
                        TotalKm = m.TotalKm,
                        PercentRate = m.TotalKm == 0 || m.TotalKm == null ? 0 : _formatDataHelper.RoundDouble((m.ViolateKm * 100 / m.TotalKm)),
                        ViolateTimeText = violateTimeText,
                        TotalTimeText = totalTimeText,
                        PercentRateTime = m.TotalTime == 0 || m.TotalTime == null ? 0 : _formatDataHelper.RoundDouble((m.ViolateTime * 100 / m.TotalTime)),
                        PrivateCode = m.PrivateCode,
                        TransportTypeName = m.TransportTypeName
                    };

                });
            } catch(Exception ex)
            {
                LogHelper.LogErrorInClass("error when CalData report " + ex.ToString(), _logger);
            }
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
        private IEnumerable<ResultReportSpeed> GetListCacheOrDB(InputReport input, int companyID, ref StoreResult<ResultReportSpeed> storeResult)
        {
            IEnumerable<ResultReportSpeed> iEnumerableReturn = new List<ResultReportSpeed>();
            try
            {
                // tạo key redis
                var keyList = _cacheHelper.CreateKeyReport("ReportSpeed", companyID, input);
                var keyCount = _cacheHelper.CreateKeyReportCount("ReportSpeed", companyID, input);
                var listCache = _cacheHelper.GetSortedSetMembersPaging<ResultReportSpeed>(keyList, input);

                // check có cache không?
                // đã có cache thì phân trang và trả về list
                if (listCache is not null)
                {
                    storeResult.Count = _cacheHelper.GetRedisCache<int>(keyCount);
					iEnumerableReturn = CalData(listCache);
                }
                // chưa có cache thì get lại db và lưu lại ienumable vào cache
                else
                {
                    var listJoin = GetIEnumerableAfterJoin(input, companyID);
                    _cacheHelper.AddEnumerableToSortedSet(keyList, listJoin, TimeSpan.FromMinutes(5));
                    storeResult.Count = listJoin.Count();
                    _cacheHelper.PushDataToCache(storeResult.Count, TimeSpan.FromMinutes(5), keyCount);
                    var listPaged = ReportHelper.PagingIEnumerable(input.PageNumber, input.PageSize, listJoin);
					iEnumerableReturn = CalData(listPaged);
                }
            } catch(Exception ex)
            {
                var error = " error when Get List Cache Or DB "; 
				LogHelper.LogErrorInClass(error+ ex.ToString(),_logger);
            }
            return iEnumerableReturn;
        }

        /// <summary>lấy  ienumerable sau khi join 5 bảng</summary>
        /// <param name="input">tham số tìm kiếm</param>
        /// <param name="companyID">mã công ty</param>
        /// <returns>ienumerable&lt;ResultReportSpeed&gt; sau khi join</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/26/2023 created
        /// </Modified>
        private IEnumerable<ResultReportSpeed> GetIEnumerableAfterJoin(InputReport input, int companyID)
        {
			IEnumerable<ResultReportSpeed> iEnumerableReturn = new List<ResultReportSpeed>();
            try
            {
                // lấy ra 5 bảng cần join đã lọc theo tham số đầu vào
                var bGTTranportTypes = _bGTTranportTypesService.GetAll();
                var bGTVehicleTransportTypes = _bGTVehicleTransportTypesService.GetByCompanyID(companyID);
                var vehicles = _vehiclesService.FindByCompanyID(companyID);
                var activityGroup = GetReportActivityGroup(input, input.VehicleSearch, companyID);
                var bgtSpeedGroup = GetSpeedGroup(input, input.VehicleSearch, companyID);

				// left join và check null 5 bảng lấy dữ liệu tổng hợp
				iEnumerableReturn =
                    (from speed in (bgtSpeedGroup is null ? new List<ResultReportSpeed>() : bgtSpeedGroup)

                     join acti in (activityGroup is null ? new List<ReportActivitySummaries>() : activityGroup) on speed?.VehicleID equals acti?.FK_VehicleID into speedActis
                     from speedActi in speedActis.DefaultIfEmpty()

                     join vehicleTransportTypes in (bGTVehicleTransportTypes is null ? new List<BGTVehicleTransportTypes>() : bGTVehicleTransportTypes) on speed?.VehicleID equals vehicleTransportTypes?.FK_VehicleID into speedVehiTranTypes
                     from speedVehiTranType in speedVehiTranTypes.DefaultIfEmpty()

                     join tranportTypes in (bGTTranportTypes is null ? new List<BGTTranportTypes>() : bGTTranportTypes) on speedVehiTranType?.FK_TransportTypeID equals tranportTypes?.PK_TransportTypeID into tranportTypesSpeeds
                     from tranportTypesSpeed in tranportTypesSpeeds.DefaultIfEmpty()

                     join vehicle in (vehicles is null ? new List<Vehicles>() : vehicles) on speed?.VehicleID equals vehicle?.PK_VehicleID

                     orderby vehicle?.PrivateCode
                     select new ResultReportSpeed
                     {
                         VehicleID = speed?.VehicleID,
                         Sum5To10 = speed?.Sum5To10,
                         Sum10To20 = speed?.Sum10To20,
                         Sum20To35 = speed?.Sum20To35,
                         SumFrom35 = speed?.SumFrom35,
                         SumTotal = speed?.SumTotal,
                         ViolateKm = _formatDataHelper.RoundDouble(speed?.ViolateKm),
                         TotalKm = _formatDataHelper.RoundDouble(speedActi?.TotalKmGps),
                         ViolatePer100Km = 0,
                         PercentRate = 0,
                         PercentRateTime = 0,
                         ViolateTime = speed?.ViolateTime,
                         TotalTime = speedActi?.ActivityTime,
                         PrivateCode = vehicle?.PrivateCode,
                         TransportTypeName = tranportTypesSpeed?.DisplayName
                     }).OrderBy(m => m?.PrivateCode);
            } catch (Exception ex)
            {
                LogHelper.LogErrorInClass("error when GetIEnumerableAfterJoin " + ex.ToString(), _logger);
            }
            return iEnumerableReturn;
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
        private IEnumerable<ReportActivitySummaries> GetReportActivityGroup(InputReport input, List<long> arrVehicle, int companyID)
        {
			IEnumerable<ReportActivitySummaries> activityGroup = new List<ReportActivitySummaries>();
            try
            {
                IEnumerable<ReportActivitySummaries> activity;
                // lọc bảng activity theo tham số đầu vào
                if (arrVehicle.Count == 0)
                {
                    activity = _reportActivitySummariesService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
                && m.EndTime <= input.DayTo);
                }
                else
                {
                    activity = _reportActivitySummariesService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
               && m.EndTime <= input.DayTo && (arrVehicle).Contains(m.FK_VehicleID));
                }

                // gom nhóm bảng activity theo FK_VehicleID
                activityGroup = activity.GroupBy(m => m.FK_VehicleID).Select(k => new ReportActivitySummaries
                {
                    FK_VehicleID = k.Key,
                    ActivityTime = k.Sum(p => p.ActivityTime),
                    TotalKmGps = k.Sum(p => p.TotalKmGps),
                });
            } catch(Exception ex)
            {
                LogHelper.LogErrorInClass("error when GetReportActivityGroup " + ex.ToString(), _logger);
            }
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
        private IEnumerable<BGTSpeedOvers> GetSpeedFilter(InputReport input, List<long> arrVehicle, int companyID)
        {
            IEnumerable<BGTSpeedOvers> bgtSpeed = new List<BGTSpeedOvers>();
            try
            {
                if (arrVehicle.Count == 0)
                {
                    bgtSpeed = _bGTSpeedOversService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
                 && m.EndTime <= input.DayTo);
                }
                else
                {
                    bgtSpeed = _bGTSpeedOversService.Find(m => m.FK_CompanyID == companyID && m.StartTime >= input.DayFrom
                 && m.EndTime <= input.DayTo && arrVehicle.Contains(m.FK_VehicleID));
                }
            } catch(Exception ex)
            {
                LogHelper.LogErrorInClass("error when GetSpeedFilter " + ex.ToString(),_logger);
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
        private IEnumerable<ResultReportSpeed> GetSpeedGroup(InputReport input, List<long> arrVehicle, int companyID)
        {
            IEnumerable<ResultReportSpeed> bgtSpeedGroup = new List<ResultReportSpeed>();
            try
            {
                var bgtSpeed = GetSpeedFilter(input, arrVehicle, companyID);

                 bgtSpeedGroup
                   = bgtSpeed.GroupBy(m => m.FK_VehicleID).ToList().Select(k =>
                   {
                       // lấy tổng số lầm vi phạm tốc độ từ 5 đến 10km/h
                       int sum5To10 = k.Count(l => (l.VelocityAllow + 5) <= l.VelocityGps && (l.VelocityAllow + 10) > l.VelocityGps);

                       // lấy tổng số lầm vi phạm tốc độ từ 10 đến 20km/h
                       int sum10To20 = k.Count(l => (l.VelocityAllow + 10) <= l.VelocityGps && (l.VelocityAllow + 20) > l.VelocityGps);

                       // lấy tổng số lầm vi phạm tốc độ từ 20 đến 35km/h
                       int sum20To35 = k.Count(l => (l.VelocityAllow + 20) <= l.VelocityGps && (l.VelocityAllow + 35) > l.VelocityGps);

                       // lấy tổng số lầm vi phạm tốc độ quá 35km/h
                       int sumFrom35 = k.Count(l => (l.VelocityAllow + 35) < l.VelocityGps);
                       int sumTotal = sum5To10 + sum10To20 + sum20To35 + sumFrom35;

                       // lấy tổng thời gian vi phạm = tổng (EndTime - StartTime)
                       var sumTime = sumTotal == 0 ? 0 : k.Where(p => p.VelocityGps >= (p.VelocityAllow + 5)).Sum(p => p.EndTime == null ? 0 : (((DateTime)p.EndTime).Subtract(p.StartTime).TotalSeconds));

                       // lấy tổng quãng đường vi phạm = tổng (EndKm - StartKm)
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
            } catch(Exception ex)
            {
                LogHelper.LogErrorInClass("error when GetSpeedGroup " + ex.ToString(),_logger);
            }
            return bgtSpeedGroup;

        }

    }
}
