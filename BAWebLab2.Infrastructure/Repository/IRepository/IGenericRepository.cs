using BAWebLab2.DTO.DTO;
using Dapper;
using System.Linq.Expressions;

namespace BAWebLab2.DAL.Repository.IRepository
{
	public interface IGenericRepository<T> where T : class
    {
        /// <summary>Get đối tượng bằng id</summary>
        /// <param name="id">id đối tượng</param>
        /// <returns>đối tượng muốn lấy</returns>
        T GetById(int id);
        /// <summary>lấy tất cả đối tượng</summary>
        /// <returns>list tất cả đối tượng</returns>
        IEnumerable<T> GetAll();
        /// <summary>tìm theo điều kiện</summary>
        /// <param name="expression">điều kiện</param>
        /// <returns>list thỏa mãn điều kiện</returns>
        IEnumerable<T> Find(Expression<Func<T, bool>> expression);
        /// <summary>thêm mới đối tượng</summary>
        /// <param name="entity">đối tượng muốn thêm</param>
        void Add(T entity);
        /// <summary>sửa đối tượng</summary>
        /// <param name="entity">đối tượng muốn sửa</param>
        void Update(T entity);
        /// <summary>thêm nhiều đối tượng</summary>
        /// <param name="entities">danh sách đối tượng</param>
        void AddRange(IEnumerable<T> entities);
        /// <summary>xóa đối tượng</summary>
        /// <param name="entity">đối tượng cần xóa</param>
        void Remove(T entity);
        /// <summary>xóa nhiều đối tượng</summary>
        /// <param name="entities">danh sách đối tượng</param>
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>gọi thủ tục</summary>
        /// <typeparam name="T1">kiểu đối tượng trả về</typeparam>
        /// <param name="storedProcedureName">tên store</param>
        /// <param name="param">list tham số store</param>
        /// <returns>đối tượng chứa danh sách trả về</returns>
        MultipleResultDTO<T1> CallStoredProcedure<T1>(string storedProcedureName, ref DynamicParameters param);
    }
}
