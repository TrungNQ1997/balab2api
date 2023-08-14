
using BAWebLab2.Model;
using BAWebLab2.Entities;
using Dapper;
using System.Data;
using BAWebLab2.Service;
using log4net;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Core.LibCommon;
using BAWebLab2.Infrastructure.Entities;
using BAWebLab2.Core.Services.IService;

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
        private readonly ILog _logger;
        private readonly IUserTokenService _userTokenService;

        public UserService(IUserRepository userRepository, IUserTokenService userTokenService)
        {
            _logger = LogManager.GetLogger(typeof(UserService));
            _userTokenService = userTokenService;
            _userRepository = userRepository;
        }

        /// <summary>parce input lấy tham số truyền vào store lấy danh sách user</summary>
        /// <param name="input">đối tượng chứa tham số truyền vào store</param>
        /// <param name="userToken">thông tin user và token</param>
        /// <returns>length của list và list select theo offset thỏa mãn điều kiện filter</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified> 
        public StoreResult<UserModel> GetListUsersFilter(InputSearchList input, UserToken userToken)
        {
            var result = new StoreResult<UserModel>();
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                if (_userTokenService.FakeDataAndCheckToken(userToken))
                {
                    parameters.Add("userId", input.UserId);
                    parameters.Add("pageNumber", input.PageNumber);
                    parameters.Add("pageSize", input.PageSize);
                    parameters.Add("textSearch", input.TextSearch);
                    parameters.Add("BirthdayFrom", input.DayFrom);
                    parameters.Add("birthdayTo", input.DayTo);
                    parameters.Add("gioiTinhSearch", input.SexSearch);
                    parameters.Add("count", 0, DbType.Int64, ParameterDirection.Output);

                    var resultStore = _userRepository.CallStoredProcedure<UserModel>("BAWeb_User_GetUserInfo", ref parameters);

                    var list = resultStore.ListPrimary;
                    var count = parameters.Get<long>("count");
                    result.List = list;
                    result.Count = (int)count;
                    result.Error = false;
                }
                else
                {
                    var error = "wrong user token ";
                    LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
                }
            }
            catch (Exception ex)
            {
                result.Message.Add(ex.Message);
                result.Error = true;
                _logger.Error(ex.ToString());
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
                parameters.Add("pass", FormatDataHelper.HashMD5(input.Password));
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
                result.Message.Add(ex.Message);
                _logger.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>api kiểm tra đăng nhập theo token và get quyền theo menu_id</summary>
        /// <param name="input">đối tượng chứa token và menu_id</param>
        /// <param name="userToken">thông tin user và token</param>
        /// <returns>đăng nhập có hợp lệ không, user có phải admin không, danh sách quyền</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserRole> CheckLoginAndRole(InputLogin input, UserToken userToken)
        {
            var result = new StoreResult<UserRole>();
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                if (_userTokenService.FakeDataAndCheckToken(userToken))
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
                else
                {
                    var error = "wrong user token ";
                    LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
                }
            }
            catch (Exception ex)
            {
                result.Message.Add(ex.Message);
                result.Error = true;
                _logger.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>lấy danh sách quyền và quyền admin của user</summary>
        /// <param name="input">chứa user_id và menu_id</param>
        /// <param name="userToken">thông tin user và token</param>
        /// <returns>list quyền và trạng thái quyền admin</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<UserRole> GetRole(InputLogin input, UserToken userToken)
        {
            var result = new StoreResult<UserRole>();

            DynamicParameters parameters = new DynamicParameters();
            try
            {
                if (_userTokenService.FakeDataAndCheckToken(userToken))
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
                else
                {
                    var error = "wrong user token ";
                    LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message.Add(ex.Message);
                _logger.Error(ex.ToString());
            }
            return result;
        }


        /// <summary>sửa người dùng</summary>
        /// <param name="user">đối tượng đã sửa</param>
        /// <param name="userToken">thông tin user và token</param>
        /// <returns>trạng thái sửa</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> EditUser(User user, UserToken userToken)
        {
            var result = new StoreResult<int>();
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                if (_userTokenService.FakeDataAndCheckToken(userToken))
                {
                    _userRepository.Update(user);
                }
                else
                {
                    var error = "wrong user token ";
                    LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message.Add(ex.Message);
                _logger.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>đổi mật khẩu user đang đăng nhập</summary>
        /// <param name="input">đối tượng chứa pass cũ và mới</param>
        /// <param name="userToken">thông tin user và token</param>
        /// <returns>trạng thái đổi mật khẩu thành công không</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> ChangePass(InputLogin input, UserToken userToken)
        {
            var result = new StoreResult<int>();
            DynamicParameters parameters = new DynamicParameters();
            try
            {
                if (_userTokenService.FakeDataAndCheckToken(userToken))
                {
                    parameters.Add("password", FormatDataHelper.HashMD5(input.Password));
                    parameters.Add("passwordOld", FormatDataHelper.HashMD5(input.PasswordOld));
                    parameters.Add("userId", int.Parse(input.UserId));
                    parameters.Add("username", input.Username);

                    parameters.Add("ret", 0, DbType.Int64, ParameterDirection.Output);

                    var resultStore = _userRepository.CallStoredProcedure<int>("BAWeb_User_UpdatePassUserInfo", ref parameters);
                    var ret = parameters.Get<long>("ret");

                    result.Error = ret == 0 ? false : true;
                }
                else
                {
                    var error = "wrong user token ";
                    LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message.Add(ex.Message);
                _logger.Error(ex.ToString());
            }
            return result;
        }

        /// <summary>xóa nhiều user</summary>
        /// <param name="input">đối tượng chứa list id user cần xóa và id user gọi api xóa</param>
        /// <param name="userToken">thông tin user và token</param>
        /// <returns>kết quả thực thi store xóa user</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> DeleteUser(InputDelete input, UserToken userToken)
        {
            var result = new StoreResult<int>();
            try
            {
                if (_userTokenService.FakeDataAndCheckToken(userToken))
                {
                    result = _userRepository.DeleteUser(input);
                }
                else
                {
                    var error = "wrong user token ";
                    LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
                }
                if (!result.List.Contains(0))
                {
                    result.Error = true;
                }
            }
            catch (Exception ex)
            {
                var error = "error delete user ";
                LogHelper.LogAndSetResponseStoreErrorInClass(error, error + ex.ToString(), ref result, _logger);
            }
            return result;
        }

        /// <summary>thêm người dùng</summary>
        /// <param name="user">user muốn thêm</param>
        /// <param name="userToken">thông tin user và token</param>
        /// <returns>trạng thái thêm mới</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public StoreResult<int> AddUser(User user, UserToken userToken)
        {
            var result = new StoreResult<int>();
            try
            {
                if (_userTokenService.FakeDataAndCheckToken(userToken))
                {
                    user.Password = FormatDataHelper.HashMD5(user.Password);

                    _userRepository.Add(user);
                }
                else
                {
                    var error = "wrong user token ";
                    LogHelper.LogAndSetResponseStoreErrorInClass(error, error, ref result, _logger);
                }
            }
            catch (Exception ex)
            {
                result.Error = true;
                result.Message.Add(ex.Message);
                _logger.Error(ex.ToString());
            }
            return result;
        }

    }
}
