using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.Model;
using BAWebLab2.Entities;
using Dapper; 
using System.Data;
using BAWebLab2.Service;

namespace BAWebLab2.Core.Services
{
    /// <summary>class implement của IUserService để xử lí dữ liệu từ client thành các param đầu vào</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
         
        public UserService(IUserRepository userRepository)
        {

            _userRepository = userRepository;
        }
         
        /// <summary>parce input lấy tham số truyền vào store lấy danh sách user</summary>
        /// <param name="input">đối tượng chứa tham số truyền vào store</param>
        /// <returns>length của list và list select theo offset thỏa mãn điều kiện filter</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified> 
        public StoreResult<UserModel> GetListUsersFilter(InputSearchList input)
        {

            var result = new StoreResult<UserModel>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {

                parameters.Add("userId", int.Parse(input.UserId));
                parameters.Add("pageNumber", input.PageNumber);
                parameters.Add("pageSize", input.PageSize);
                parameters.Add("textSearch", input.TextSearch);
                parameters.Add("BirthdayFrom", input.BirthdayFrom);
                parameters.Add("birthdayTo", input.BirthdayTo);
                parameters.Add("gioiTinhSearch", input.GioiTinhSearch);
                parameters.Add("count", 0, DbType.Int64, ParameterDirection.Output);

                var resultStore = _userRepository.CallStoredProcedure<UserModel>("BAWeb_User_GetUserInfo", ref parameters);
                
                var list = resultStore.ListPrimary;
                 
                //var e = list.Skip(20).Take(10).ToList().OrderBy(m=>m.Username).OrderBy(mbox=>);
                var t = list.SingleOrDefault(m => m.Username.Contains("11"));


                var count = parameters.Get<long>("count");
                result.List = list;
                result.Count = (int)count;

                
                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }

            return result;
        }


        public StoreResult<Vehicles> GetVehicles()
        {

            var result = new StoreResult<Vehicles>();

            
            try
            {

               
                var resultStore = _userRepository.GetVehicles<Vehicles>();

                var list = resultStore.ListPrimary.Where(m=>m.FK_CompanyID== 15076).ToList();

                //var e = list.Skip(20).Take(10).ToList().OrderBy(m=>m.Username).OrderBy(mbox=>);
                 

                //var count = parameters.Get<long>("count");
                result.List = list;
                //result.Count = (int)count;


                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }

            return result;
        }



        /// <summary>api kiểm tra username, pass có hợp lệ không</summary>
        /// <param name="input">đối tượng chứa username, pass</param>
        /// <returns>có đăng nhập hợp lệ không, thông tin user đăng nhập</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserModel> Login(InputLogin input)
        {

            var result = new StoreResult<UserModel>();

            DynamicParameters parameters = new DynamicParameters();

            try
            {
                parameters.Add("username", input.Username);
                parameters.Add("pass", LibCommon.LibCommon.HashMD5(input.Password));
                parameters.Add("isRemember", input.IsRemember);
                parameters.Add("ret", 0, DbType.Int64, ParameterDirection.Output);

                var resultStore = _userRepository.CallStoredProcedure<UserModel>("BAWeb_User_Login", ref parameters);

                result.List = resultStore.ListPrimary;
                var ret = parameters.Get<long>("ret");
                result.Count = (int)ret;
                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        /// <summary>api kiểm tra đăng nhập theo token và get quyền theo menu_id</summary>
        /// <param name="input">đối tượng chứa token và menu_id</param>
        /// <returns>đăng nhập có hợp lệ không, user có phải admin không, danh sách quyền</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserRole> CheckLoginAndRole(InputLogin input)
        {

            var result = new StoreResult<UserRole>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("token", input.Token);

                parameters.Add("menuId", int.Parse(input.MenuId));
                parameters.Add("ret", 0, DbType.Int64, ParameterDirection.Output);
                parameters.Add("isAdmin", false, DbType.Boolean, ParameterDirection.Output);

                var resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWeb_User_CheckTokenLoginGetRole", ref parameters);

                var isAdmin = parameters.Get<bool>("isAdmin");
                var ret = parameters.Get<long>("ret");
                result.List = resultStore.ListPrimary;
               
                result.Error = ret == 0 ? true : false;
                result.Admin = isAdmin;
                  
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        /// <summary>lấy danh sách quyền và quyền admin của user</summary>
        /// <param name="input">chứa user_id và menu_id</param>
        /// <returns>list quyền và trạng thái quyền admin</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserRole> GetRole(InputLogin input)
        {

            var result = new StoreResult<UserRole>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("userId", input.UserId);

                parameters.Add("menuId", int.Parse(input.MenuId));
                parameters.Add("isAdmin", 0, DbType.Boolean, ParameterDirection.Output);

                var resultStore = _userRepository.CallStoredProcedure<UserRole>("BAWeb_User_GetRole", ref parameters);

                var isAdmin = parameters.Get<bool>("isAdmin");

                result.Admin = isAdmin;

                result.List = resultStore.ListPrimary;
                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }


        /// <summary>sửa người dùng</summary>
        /// <param name="user">đối tượng đã sửa</param>
        /// <returns>trạng thái sửa</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> EditUser(User user)
        {

            var result = new StoreResult<int>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                user.Password = LibCommon.LibCommon.HashMD5(user.Password);
                _userRepository.Update(user);
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        /// <summary>đổi mật khẩu user đang đăng nhập</summary>
        /// <param name="input">đối tượng chứa pass cũ và mới</param>
        /// <returns>trạng thái đổi mật khẩu thành công không</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> ChangePass(InputLogin input)
        {

            var result = new StoreResult<int>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                parameters.Add("password", LibCommon.LibCommon.HashMD5(input.Password));
                parameters.Add("passwordOld", LibCommon.LibCommon.HashMD5(input.PasswordOld));
                parameters.Add("userId", int.Parse(input.UserId));
                parameters.Add("username", input.Username);

                parameters.Add("ret", 0, DbType.Int64, ParameterDirection.Output);

                var resultStore = _userRepository.CallStoredProcedure<int>("BAWeb_User_UpdatePassUserInfo", ref parameters);
                var ret = parameters.Get<long>("ret");
               
                result.Error = ret == 0 ? false : true;
                
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

        /// <summary>xóa nhiều user</summary>
        /// <param name="input">đối tượng chứa list id user cần xóa và id user gọi api xóa</param>
        /// <returns>kết quả thực thi store xóa user</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> DeleteUser(InputDelete input)
        {

            var result = new StoreResult<int>();
            result = _userRepository.DeleteUser(input);
            if (!result.List.Contains(0))
            {
                result.Error = true;
            }

            return result;
        }

        /// <summary>thêm người dùng</summary>
        /// <param name="user">user muốn thêm</param>
        /// <returns>trạng thái thêm mới</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> AddUser(User user)
        {

            var result = new StoreResult<int>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {

                user.Password = LibCommon.LibCommon.HashMD5(user.Password);

                _userRepository.Add(user);
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message = ex.Message;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }
            return result;
        }

    }
}
