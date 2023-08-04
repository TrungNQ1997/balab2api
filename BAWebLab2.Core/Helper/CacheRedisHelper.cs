using BAWebLab2.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Configuration;
using System.Text;

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
        private readonly ConnectionMultiplexer _redisConnection;



        public CacheRedisHelper(IDistributedCache cache, IConfiguration configuration)
        {
            _cache = cache;
            _configuration = configuration;
            _redisConnection = ConnectionMultiplexer.Connect(_configuration["RedisCacheServerUrl"]);

        }

        public void AddEnumerableToSortedSet<T>(string key, IEnumerable<T> data)
        {
            IDatabase redisDb = _redisConnection.GetDatabase();
            var sortedSetEntries = new List<SortedSetEntry>();
            //sortedSetEntries.Add(new SortedSetEntry(data.ToString(), 1));
            for (var i = 0; i < data.Count(); i++)//foreach (T item in data)
            {
                sortedSetEntries.Add(new SortedSetEntry(data.ElementAt(i).ToString(), i));
            }

            redisDb.SortedSetAdd(key, sortedSetEntries.ToArray());
        }

        public IEnumerable<T> GetSortedSetMembers<T>(string key, Order order = Order.Ascending, long skip = 0, long take = -1)
        {
            IDatabase redisDb = _redisConnection.GetDatabase();
            RedisValue[] redisValues = redisDb.SortedSetRangeByScore(key, order: order, skip: skip, take: take);
            var e = redisValues.AsEnumerable().Cast<T>();
            return e;
        }


        public void Dispose()
        {
            _redisConnection.Dispose();
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
