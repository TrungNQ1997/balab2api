using BAWebLab2.Model;
using Dapper;
using System.Linq.Expressions;

namespace BAWebLab2.Infrastructure.Repository.IRepository
{
    /// <summary>class interface của GenericRepository</summary>
    /// <typeparam name="T">kiểu đối tượng truyền vào</typeparam>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public interface IGenericRepository<T> where T : class
    {
        /// <summary>Get đối tượng bằng id</summary>
        /// <param name="id">id đối tượng</param>
        /// <returns>đối tượng muốn lấy</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        T GetById(int id);
         
        /// <summary>lấy tất cả đối tượng</summary>
        /// <returns>danh sách đối tượng</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        IEnumerable<T> GetAll();

        /// <summary>tìm theo điều kiện</summary>
        /// <param name="expression">điều kiện</param>
        /// <returns>list thỏa mãn điều kiện</returns>
        ///   <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);

        /// <summary>thêm mới đối tượng</summary>
        /// <param name="entity">đối tượng muốn thêm</param>
        ///   <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        void Add(T entity);

        /// <summary>sửa đối tượng</summary>
        /// <param name="entity">đối tượng muốn sửa</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        void Update(T entity);

        /// <summary>thêm nhiều đối tượng</summary>
        /// <param name="entities">danh sách đối tượng</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        void AddRange(IEnumerable<T> entities);

        /// <summary>xóa đối tượng</summary>
        /// <param name="entity">đối tượng cần xóa</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        void Remove(T entity);

        /// <summary>xóa nhiều đối tượng</summary>
        /// <param name="entities">danh sách đối tượng</param>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>gọi thủ tục</summary>
        /// <typeparam name="T1">kiểu đối tượng trả về</typeparam>
        /// <param name="storedProcedureName">tên store</param>
        /// <param name="param">list tham số store</param>
        /// <returns>đối tượng chứa danh sách trả về</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        MultipleResult<T1> CallStoredProcedure<T1>(string storedProcedureName, ref DynamicParameters param);
    }
}
