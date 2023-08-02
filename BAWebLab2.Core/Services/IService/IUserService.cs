using BAWebLab2.Model;
using BAWebLab2.Entities;

namespace BAWebLab2.Service
{
    /// <summary>class interface của UserService</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public interface IUserService
    {

        /// <summary>parce input lấy tham số truyền vào store lấy danh sách user</summary>
        /// <param name="input">đối tượng chứa tham số truyền vào store</param>
        /// <returns>length của list và list select theo offset thỏa mãn điều kiện filter</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserModel> GetListUsersFilter(InputSearchList input);

        /// <summary>lấy danh sách quyền và quyền admin của user</summary>
        /// <param name="input">chứa user_id và menu_id</param>
        /// <returns>list quyền và trạng thái quyền admin</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserRole> GetRole(InputLogin input);

        /// <summary>api kiểm tra username, pass có hợp lệ không</summary>
        /// <param name="input">đối tượng chứa username, pass</param>
        /// <returns>có đăng nhập hợp lệ không, thông tin user đăng nhập</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserModel> Login(InputLogin input);

        /// <summary>thêm user</summary>
        /// <param name="input"> user cần thêm</param>
        /// <returns>đối tượng chứa trạng thái thêm thành công hay không</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> AddUser(User input);

        /// <summary>api kiểm tra đăng nhập theo token và get quyền theo menu_id</summary>
        /// <param name="input">đối tượng chứa token và menu_id</param>
        /// <returns>đăng nhập có hợp lệ không, user có phải admin không, danh sách quyền</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserRole> CheckLoginAndRole(InputLogin input);


        /// <summary>sửa user</summary>
        /// <param name="input">đối tượng chứa thông tin user sửa</param>
        /// <returns>trạng thái sửa thành công không</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> EditUser(User input);

        /// <summary>đổi mật khẩu user đang đăng nhập</summary>
        /// <param name="input">đối tượng chứa pass cũ và mới</param>
        /// <returns>trạng thái đổi mật khẩu thành công không</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> ChangePass(InputLogin input);

        /// <summary>xóa nhiều user</summary>
        /// <param name="input">đối tượng chứa list id user cần xóa và id user gọi api xóa</param>
        /// <returns>kết quả thực thi store xóa user</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> DeleteUser(InputDelete input);
    }
}
