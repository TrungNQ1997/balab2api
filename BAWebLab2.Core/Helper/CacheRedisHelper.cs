using BAWebLab2.Model;
using CachingFramework.Redis.Contracts.RedisObjects;
using CachingFramework.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using CachingFramework.Redis.Contracts;
using BAWebLab2.Infrastructure.Models;
using log4net;

namespace BAWebLab2.Core.LibCommon
{
    /// <summary>class xử lí reidis cache</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/2/2023 created
    /// </Modified>
    public class CacheRedisHelper
    {
        private static IDistributedCache _cache { get; set; }
        private readonly IConfiguration _configuration;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(CacheRedisHelper));

        public CacheRedisHelper(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
        }

        /// <summary>thêm  enumerable vào sorted set.</summary>
        /// <typeparam name="T">kiểu dữ liệu đối tượng trả về</typeparam>
        /// <param name="key">key redis</param>
        /// <param name="data">dữ liệu muốn thêm vào redis</param>
        /// <param name="time">thời gian hết hạn</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/7/2023 created
        /// </Modified>
        public void AddEnumerableToSortedSet<T>(string key, IEnumerable<T> data, TimeSpan time)
        {
            try
            {
                int i = 1;
                var context = new RedisContext(_configuration["RedisCacheServerUrl"]);
                IRedisSortedSet<T> sortedSet = context.Collections.GetRedisSortedSet<T>(key);
                sortedSet.AddRange(data.Select((m) => new SortedMember<T>(i, m)
                {
                    Value = m,
                    Score = i++
                }));
                sortedSet.TimeToLive = time;
                context.Dispose();
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("data " + JsonConvert.SerializeObject(data) + " key " + JsonConvert.SerializeObject(key) + " error " + ex.ToString(), _logger);
            }
        }

        /// <summary>lấy sorted set</summary>
        /// <typeparam name="T">kiểu đối tượng lưu vào sorted set</typeparam>
        /// <param name="key">key redis</param>
        /// <returns>nếu có dữ liệu trả về ienumerable&lt;T&gt;, không có trả về null</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/7/2023 created
        /// </Modified>
        public IEnumerable<T>? GetSortedSetMembers<T>(string key)
        {
            IEnumerable<T>? result = null;
            try
            {
                var context = new RedisContext(_configuration["RedisCacheServerUrl"]);
                if (context.Cache.KeyExists(key))
                {
                    result = context.Collections.GetRedisSortedSet<T>(key);
                }
                else
                {
                    result = null;
                };
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("key " + JsonConvert.SerializeObject(key) + " error " + ex.ToString(), _logger);
            }
            return result;
        }

        /// <summary>lấy ra sorted set và phân trang</summary>
        /// <typeparam name="T">kiểu đối tượng đẩy vào sorted set</typeparam>
        /// <param name="key">key redis</param>
        /// <param name="input">tham số đầu và phân trang</param>
        /// <param name="storeResult">đối tượng chứa biến count toàn bộ danh sách chưa phân trang</param>
        /// <returns>ienumerable đã phân trang</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/7/2023 created
        /// </Modified>
        public IEnumerable<T>? GetSortedSetMembersPaging<T>(string key, InputReport input)
        {
            IEnumerable<T>? result = null;
            try
            {
                var context = new RedisContext(_configuration["RedisCacheServerUrl"]);
                if (context.Cache.KeyExists(key))
                {
                    var beginIndex = ((input.PageNumber - 1) * input.PageSize) + 1;
                    result = context.Collections.GetRedisSortedSet<T>(key).GetRangeByScore(beginIndex, beginIndex + input.PageSize - 1).Select(m => m.Value);
                }
                else
                {
                    result = null;
                };
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("key " + JsonConvert.SerializeObject(key) + " input " + JsonConvert.SerializeObject(input) + " error " + ex.ToString(), _logger);
            }
            return result;
        }

        /// <summary>đẩy data vào redis cache</summary>
        /// <param name="data">The data.
        /// cần cache</param>
        /// <param name="time">The time.
        /// thời gian lưu cache</param>
        /// <param name="key">key lưu cache</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        public void PushDataToCache(object? data, TimeSpan time, string key)
        {
            try
            {
                var cachedDataString = JsonConvert.SerializeObject(data);
                var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);
                _cache.Set(key, newDataToCache, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = time });
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("data " + JsonConvert.SerializeObject(data) + " key " + JsonConvert.SerializeObject(key) + " TimeSpan " + time.ToString() + " error " + ex.ToString(), _logger);
            }
        }

        /// <summary>lấy dữ liệu từ redis cache.</summary>
        /// <typeparam name="T">ép kiểu data cần lấy</typeparam>
        /// <param name="key">key của data trong redis cache</param>
        /// <returns>data đã ép kiểu</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/28/2023 created
        /// </Modified>
        public T? GetRedisCache<T>(string key)
        {
            var objReturn = default(T?);
            try
            {
                var serializedValue = _cache.Get(key);
                objReturn = serializedValue is null ? default : JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(serializedValue));
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("key " + JsonConvert.SerializeObject(key) + " error " + ex.ToString(), _logger);
            }
            return objReturn;
        }

        /// <summary>tạo key redis theo medule</summary>
        /// <param name="moduleName">tên module</param>
        /// <param name="companyId">tên công ty</param>
        /// <returns>key redis</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/2/2023 created
        /// </Modified>
        public string CreateKeyByModule(string moduleName, int companyId)
        {
            return $"{companyId}:_{moduleName}:";
        }

        /// <summary>tạo key redis theo báo cáo</summary>
        /// <param name="moduleName">mã báo cáo</param>
        /// <param name="companyId">mã công ty</param>
        /// <param name="input">tham số đầu vào của báo cáo</param>
        /// <returns>key redis</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/2/2023 created
        /// </Modified>
        public string CreateKeyReport(string moduleName, int companyId, InputReport input)
        {
            var keyReturn = "";
            try
            {
                var keyBasic = $"{companyId}:_{moduleName}:"; CreateKeyByModule(null, 1);
                var keyInput = $"_{input.DayFrom.ToString()}_{input.DayTo.ToString()}_{FormatDataHelper.HashMD5(JsonConvert.SerializeObject(input.VehicleSearch))}";
                keyInput = keyInput.Replace(':', ';');
                keyReturn = keyBasic + "_List:" + keyInput;
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("moduleName " + JsonConvert.SerializeObject(moduleName) + " companyId " + JsonConvert.SerializeObject(companyId) + " input " + JsonConvert.SerializeObject(input) + " error " + ex.ToString(), _logger);
            }
            return keyReturn;
        }

        /// <summary>tạo key redis báo cáo count.</summary>
        /// <param name="moduleName">tên báo cáo</param>
        /// <param name="companyId">mã công ty</param>
        /// <param name="input">tham số đầu vào báo cáo</param>
        /// <returns>key</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        public string CreateKeyReportCount(string moduleName, int companyId, InputReport input)
        {
            var keyReturn = "";
            try
            {
                var keyBasic = $"{companyId}:_{moduleName}:";
                var keyInput = $"_{input.DayFrom.ToString()}_{input.DayTo.ToString()}_{FormatDataHelper.HashMD5(JsonConvert.SerializeObject(input.VehicleSearch))}";
                keyInput = keyInput.Replace(':', ';');
                keyReturn = keyBasic + "_Count:" + keyInput;
            }
            catch (Exception ex)
            {
                LogHelper.LogErrorInClass("moduleName " + JsonConvert.SerializeObject(moduleName) + " companyId " + JsonConvert.SerializeObject(companyId) + " input " + JsonConvert.SerializeObject(input) + " error " + ex.ToString(), _logger);
            }
            return keyReturn;
        }

    }
}
