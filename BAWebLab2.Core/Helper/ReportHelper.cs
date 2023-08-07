using BAWebLab2.Model;

namespace BAWebLab2.Core.LibCommon
{
    /// <summary>class xử lí báo cáo</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/2/2023 created
    /// </Modified>
    public class ReportHelper
    {
        /// <summary>phân trang báo cáo</summary>
        /// <typeparam name="T">kiểu dữ liệu list báo cáo</typeparam>
        /// <param name="input">tham số truyền vào báo cáo</param>
        /// <param name="list">list chưa phân trang</param>
        /// <returns>list đã phân trang</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/2/2023 created
        /// </Modified>
        public static IEnumerable<T> PagingIEnumerable<T>(InputSearchList input, IEnumerable<T> list)
        {
            return list.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize);
        }
         
    }
}
