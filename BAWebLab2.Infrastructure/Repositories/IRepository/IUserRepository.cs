using BAWebLab2.Model;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Repository.IRepository;

namespace BAWebLab2.Infrastructure.Repositories.IRepository
{
    /// <summary>class interface của UserRepository chứa hàm xử lí của phân hệ user</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public interface   IUserRepository : IGenericRepository<User>
    {
         
        /// <summary>xóa danh sách user</summary>
        /// <param name="input">đối tượng chứa danh sách user cần xóa, id user thực hiện xóa</param>
        /// <returns>chuỗi kết quả xóa: 0- thành công, khác 0- thất bại</returns>
        ///   <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> DeleteUser(InputDelete input);

        /// <summary>thêm mới đối tượng</summary>
        /// <param name="user">user muốn thêm</param>
        ///   <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public new void Add(User user);

        /// <summary>sửa đối tượng</summary>
        /// <param name="user">user muốn sửa</param>
        ///   <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public new void Update(User user);
 
    }
}
