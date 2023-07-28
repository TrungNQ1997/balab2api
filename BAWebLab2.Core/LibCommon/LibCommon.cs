using log4net;
using Newtonsoft.Json;
using StackExchange.Redis;
using System.Security.Cryptography;
using System.Text;

namespace BAWebLab2
{
    /// <summary>class chứa các hàm chung</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class LibCommon
    {
        private static readonly ILog _logger = LogManager.GetLogger(typeof(LibCommon));
          private static readonly Lazy<ConnectionMultiplexer> lazyConnection = new Lazy<ConnectionMultiplexer>(() =>
        {
            return ConnectionMultiplexer.Connect("127.0.0.1:6379");
        });
        private static ConnectionMultiplexer Connection => lazyConnection.Value;
 
        /// <summary>mã hóa md5 1 chuỗi</summary>
        /// <param name="text">The text.
        /// chuỗi cần mã hóa</param>
        /// <returns>chuỗi đã mã hóa</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public static string HashMD5(string text)
        {
            MD5 md5 = MD5.Create();
            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(text));
            StringBuilder hashSb = new StringBuilder();
            foreach (byte b in hash)
            {
                hashSb.Append(b.ToString("X2"));
            }
            return hashSb.ToString();
        }

        /// <summary>parse Strings thành  list&lt;long&gt;</summary>
        /// <param name="text">string đầu vào</param>
        /// <returns>list&lt;long&gt; đã parse</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/27/2023 created
        /// </Modified>
        public static List<long>? StringToListLong(string text)
        {
            var listVehicleID = new List<long>();
            if (!string.IsNullOrEmpty(text))
            {
                listVehicleID = text?.Split(',')?.Select(long.Parse)?.ToList();
            }
            return listVehicleID;
        }

        /// <summary>ghi lỗi vào log</summary>
        /// <param name="error">chuỗi lỗi</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/28/2023 created
        /// </Modified>
        public static void LogError(string error)
        {
            _logger.Error(error );

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
        public static void PushDataToCache(object? data, TimeSpan time, string key)
        {
            var db = Connection.GetDatabase();
            var cachedDataString = JsonConvert.SerializeObject(data);
            var newDataToCache = Encoding.UTF8.GetBytes(cachedDataString);
 
            db.StringSet(key, newDataToCache, time);
 
        }

        /// <summary>lấy dữ liệu từ redis cache.</summary>
        /// <typeparam name="T">ép kiểu data cần lấy</typeparam>
        /// <param name="key">key của data trong redis cache</param>
        /// <returns>data đã ép kiểu</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/28/2023 created
        /// </Modified>
        public static T  GetRedisCache<T>(string key)
        {
            var db = Connection.GetDatabase();
            var serializedValue = db.StringGet(key); 
            if (!serializedValue.IsNullOrEmpty)
            {
                return JsonConvert.DeserializeObject<T>(serializedValue); 
            } 
            return default;
        }


    }
}
