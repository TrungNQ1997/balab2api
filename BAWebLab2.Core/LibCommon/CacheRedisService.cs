using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace BAWebLab2.Core.LibCommon
{
    public class CacheRedisService
    {
        private static IDistributedCache _cache { get; set; }

        public CacheRedisService(IDistributedCache cache)
        {
            _cache = cache;
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
        public   void PushDataToCache(object? data, TimeSpan time, string key)
        {
            //var db = Connection.GetDatabase();

            var cachedDataString = JsonConvert.SerializeObject(data);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);
            _cache.Set(key, newDataToCache, new DistributedCacheEntryOptions() { AbsoluteExpirationRelativeToNow = time });
            //db.StringSet(key, newDataToCache, time);

        }
         
        /// <summary>lấy dữ liệu từ redis cache.</summary>
        /// <typeparam name="T">ép kiểu data cần lấy</typeparam>
        /// <param name="key">key của data trong redis cache</param>
        /// <returns>data đã ép kiểu</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/28/2023 created
        /// </Modified>
        public   T GetRedisCache<T>(string key)
        {
            //var db = Connection.GetDatabase();
            var serializedValue = _cache.Get(key);
            if (!serializedValue.IsNullOrEmpty())
            {
                return JsonConvert.DeserializeObject<T>(Encoding.UTF8.GetString(serializedValue));
            }
            return default;
        }

        public string CreateKeyByModule(string moduleName, int companyId)
        {
            return $"{companyId}:_{moduleName}:";
        }

        public string CreateKeyReport(string moduleName, int companyId, InputSearchList input)
        {
            var keyBasic = $"{companyId}:_{moduleName}:";
            var keyInput = $"_{input.DayFrom.ToString()}_{input.DayTo.ToString()}_{FormatDataService.HashMD5(input.TextSearch)}";
            keyInput = keyInput.Replace(':', ';');
            var keyList = keyBasic + "_List:" + keyInput;
            return keyList;
        }

    }
}
