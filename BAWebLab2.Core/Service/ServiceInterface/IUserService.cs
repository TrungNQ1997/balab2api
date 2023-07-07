using BAWebLab2.DTO;
using BAWebLab2.Entity;
using BAWebLab2.Infrastructure.DTO;

namespace BAWebLab2.Business
{
    public interface IUserService
    {
        /// <summary>parce input lấy tham số truyền vào store lấy danh sách user</summary>
        /// <param name="input">đối tượng chứa tham số truyền vào store</param>
        /// <returns>length của list và list select theo offset thỏa mãn điều kiện filter</returns>

        public StoreResultDTO<UserDTO> GetListUsersFilter(InputSearchListDTO input);


          /// <summary>lấy danh sách quyền và quyền admin của user</summary>
        /// <param name="input">chứa user_id và menu_id</param>
        /// <returns>list quyền và trạng thái quyền admin</returns>
        public StoreResultDTO<UserRole> GetRole(InputLoginDTO input);
        /// <summary>api kiểm tra username, pass có hợp lệ không</summary>
        /// <param name="input">đối tượng chứa username, pass</param>
        /// <returns>có đăng nhập hợp lệ không, thông tin user đăng nhập</returns>

        public StoreResultDTO<UserDTO> Login(InputLoginDTO input);
        /// <summary>thêm user</summary>
        /// <param name="input"> user cần thêm</param>
        /// <returns>đối tượng chứa trạng thái thêm thành công hay không</returns>

        public StoreResultDTO<int> AddUser(User input);
        /// <summary>api kiểm tra đăng nhập theo token và get quyền theo menu_id</summary>
        /// <param name="input">đối tượng chứa token và menu_id</param>
        /// <returns>đăng nhập có hợp lệ không, user có phải admin không, danh sách quyền</returns>

        public StoreResultDTO<UserRole> CheckLoginAndRole(InputLoginDTO input);
        /// <summary>sửa user</summary>
        /// <param name="input">đối tượng chứa thông tin user sửa</param>
        /// <returns>trạng thái sửa thành công không</returns>

        public StoreResultDTO<int> EditUser(User input);
        /// <summary>đổi mật khẩu user đang đăng nhập</summary>
        /// <param name="input">đối tượng chứa pass cũ và mới</param>
        /// <returns>trạng thái đổi mật khẩu thành công không</returns>

        public StoreResultDTO<int> ChangePass(InputLoginDTO input);
        /// <summary>xóa nhiều user</summary>
        /// <param name="input">đối tượng chứa list id user cần xóa và id user gọi api xóa</param>
        /// <returns>kết quả thực thi store xóa user</returns>

        public StoreResultDTO<int> DeleteUser(InputDeleteDTO input);
    }
}
