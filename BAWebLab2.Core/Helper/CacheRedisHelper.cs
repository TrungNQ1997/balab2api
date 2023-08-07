using BAWebLab2.Model;
using CachingFramework.Redis.Contracts.RedisObjects;
using CachingFramework.Redis;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;
using CachingFramework.Redis.Contracts;
using BAWebLab2.Infrastructure.Models;

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
            var context = new RedisContext(_configuration["RedisCacheServerUrl"]);
            if (context.Cache.KeyExists(key))
            {
                return context.Collections.GetRedisSortedSet<T>(key);
               
            } else
            {
                return null;
            }; 
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
        public IEnumerable<T>? GetSortedSetMembersPaging<T>(string key, InputSearchList input, ref StoreResult<ResultReportSpeed> storeResult)
        {
            var context = new RedisContext(_configuration["RedisCacheServerUrl"]);
            if (context.Cache.KeyExists(key))
            {
                IRedisSortedSet<T> sortedSet = context.Collections.GetRedisSortedSet<T>(key);
                storeResult.Count = sortedSet.Count(); 
                return sortedSet.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize);
            }
            else
            {
                storeResult.Count = 0;
                return null;
            }; 
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
            var cachedDataString = JsonConvert.SerializeObject(data);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);
            _cache.Set(key, newDataToCache, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = time }); 
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
            var serializedValue = _cache.Get(key); 
            return serializedValue is null ? default : JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(serializedValue)); 
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
        public string CreateKeyReport(string moduleName, int companyId, InputSearchList input)
        {
            var keyBasic = $"{companyId}:_{moduleName}:";
            var keyInput = $"_{input.DayFrom.ToString()}_{input.DayTo.ToString()}_{FormatDataHelper.HashMD5(input.TextSearch is null ? "" : input.TextSearch)}";
            keyInput = keyInput.Replace(':', ';');
            var keyList = keyBasic + "_List:" + keyInput;
            return keyList;
        }

    }
}
