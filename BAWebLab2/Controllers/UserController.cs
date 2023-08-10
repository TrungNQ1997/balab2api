using Microsoft.AspNetCore.Mvc;
using BAWebLab2.Service;
using System.Net;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using BAWebLab2.Model;
using BAWebLab2.Entities;
using BAWebLab2.Core.LibCommon;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using log4net;

/// <summary>lớp nhận request từ client, api phân hệ người dùng với tiền tố là user</summary>
/// <Modified>
/// Name Date Comments
/// trungnq3 7/12/2023 created
/// </Modified>
[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    private readonly ILog _logger = LogManager.GetLogger(typeof(UserController));

    public UserController(IUserService userService)
    { 
        _userService = userService;
    }

    /// <summary>lấy danh sách người dùng có phân trang và tìm kiếm theo điều kiện</summary>
    /// <param name="data">The data. đối tượng chứa thông tin tìm kiếm</param>
    /// <returns>trạng thái thực hiện store và tổng phần tử của list và 1 list đối tượng select theo offset pagesize và page number thỏa mãn điều kiện tìm kiếm</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    [HttpPost("getListUserFilter")]
    public IActionResult GetListUsersFilter([FromBody] InputSearchList data)
    {
        ApiResponse<UserModel> response = new ApiResponse<UserModel>();
        try
        {
            if (ValidInputSearch(data, ref response))
            {
                var result = _userService.GetListUsersFilter(data);
                if (result.Error == false)
                {
                    response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                    response.Message.Add(HttpStatusCode.OK.ToString());
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                    response.Data = result;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError,"error when get data " + ex.Message, "data " + JsonConvert.SerializeObject(data) + ex.ToString(), ref response,_logger);
        }
        return Ok(response);
    }

    /// <summary>api kiểm tra đăng nhập vào hệ thống</summary>
    /// <param name="data">The data.
    /// truyền vào đối tượng chứa tài khoản và mật khẩu của user</param>
    /// <returns>trạng thái có cho đăng nhập hay không? , thông tin của user đăng nhập</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    [HttpPost("login")]
    public IActionResult Login([FromBody] InputLogin data)
    {
        ApiResponse<UserModel> response = new ApiResponse<UserModel>();
        try
        {
            if (ValidLogin(data, ref response))
            {
                var result = _userService.Login(data);
                if (result.Error == false)
                {
                    response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                    response.Message.Add(HttpStatusCode.OK.ToString());
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                    response.Data = result;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError,"error when login " +ex.Message, "data " + JsonConvert.SerializeObject(data) + ex.ToString(), ref response, _logger);
        }
        return Ok(response);
    }

    /// <summary>api check đăng nhập và ghet quyền theo menuid</summary>
    /// <param name="data">The data.
    ///   đối tượng chứa token, menuid</param>
    /// <returns>đăng nhập có hợp lệ không?, list quyền theo user và menuid</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    [HttpPost("checklogingetrole")]
    public IActionResult CheckLoginAndRole([FromBody] InputLogin data)
    {
        ApiResponse<UserRole> response = new ApiResponse<UserRole>();
        try
        {
            if (ValidCheckLoginAndRole(data, ref response))
            {
                var result = _userService.CheckLoginAndRole(data);
                if (result.Error == false)
                {
                    response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                    response.Message.Add(HttpStatusCode.OK.ToString());
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                    response.Data = result;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError, "error when check login get role " + ex.Message, "data " + JsonConvert.SerializeObject(data) + ex.ToString(), ref response, _logger);
        }
        return Ok(response);
    }

    /// <summary>get list quyền theo user và menuid</summary>
    /// <param name="data">The data.
    /// chứa user_id và menuid</param>
    /// <returns>list quyền theo user và menuid</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    [HttpPost("getrole")]
    public IActionResult GetRole([FromBody] InputLogin data)
    {
        ApiResponse<UserRole> response = new ApiResponse<UserRole>();
        try
        {
            if (ValidGetRole(data, ref response))
            {
                var result = _userService.GetRole(data);
                if (result.Error == false)
                {
                    response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                    response.Message.Add(HttpStatusCode.OK.ToString());
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                    response.Data = result;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError,"error when get role " + ex.Message, "data " + JsonConvert.SerializeObject(data) + ex.ToString(), ref response,_logger);
        }
        return Ok(response); 
    }

    /// <summary>api thêm người dùng</summary>
    /// <param name="data">The data.
    /// chứa các thuộc tính của user muốn thêm</param>
    /// <returns>trạng thái có thêm thành công không?</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    [HttpPost("adduser")]
    public IActionResult AddUser([FromBody] User data)
    {
        ApiResponse<int> response = new ApiResponse<int>();
        try
        {
            if (ValidUserAdd(data, ref response))
            {
                var result = _userService.AddUser(data);
                if (result.Error == false)
                {
                    response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                    response.Message.Add(HttpStatusCode.OK.ToString());
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                    response.Data = result;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError,"error add user " + ex.Message, "data " + JsonConvert.SerializeObject(data) + " " + ex.ToString(), ref response, _logger);
        }
        return Ok(response);

    }

    /// <summary>api sửa người dùng</summary>
    /// <param name="data">The data.
    /// chứa các thuộc tính của user cần sửa</param>
    /// <returns>trạng thái sửa người dùng</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    [HttpPost("edituser")]
    public IActionResult EditUser([FromBody] User data)
    {
        var result = new StoreResult<int>();
        ApiResponse<int> response = new ApiResponse<int>();
        try
        {
            if (ValidUserEdit(data, ref response))
            {
                result = _userService.EditUser(data);
                if (result.Error == false)
                {
                    response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                    response.Message.Add(HttpStatusCode.OK.ToString());
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                    response.Data = result;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError,"error when edit user " + ex.Message, "data " + JsonConvert.SerializeObject(data) + " " + ex.ToString(), ref response, _logger);
        }
        return Ok(response);

    }

    /// <summary>api đổi mật khẩu người dùng</summary>
    /// <param name="data">The data.
    /// chứa mật khẩu cũ và mật khẩu mới</param>
    /// <returns>trạng thái đổi mật khẩu</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    [HttpPost("changepass")]
    public IActionResult ChangePass([FromBody] InputLogin data)
    {
        var result = new StoreResult<int>();
        ApiResponse<int> response = new ApiResponse<int>();
        try
        {
            if (ValidChangePass(data, ref response))
            {
                result = _userService.ChangePass(data);
                if (result.Error == false)
                {
                    response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                    response.Message.Add(HttpStatusCode.OK.ToString());
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                    response.Data = result;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError,"error when change pass " +ex.Message, "data " + JsonConvert.SerializeObject(data) + " " + ex.ToString(), ref response, _logger);
        }

        return Ok(response);

    }

    /// <summary>api xóa người dùng</summary>
    /// <param name="data">The data.
    /// đối tượng chứa user_id và list id của danh sách user muốn xóa</param>
    /// <returns>trạng thái xóa user</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/12/2023 created
    /// </Modified>
    [HttpPost("deleteuser")]
    public IActionResult DeleteUser([FromBody] InputDelete data)
    {
        ApiResponse<int> response = new ApiResponse<int>();
        try
        {
            if (ValidDeleteUser(data, ref response))
            {
                var result = _userService.DeleteUser(data);
                if (result.Error == false)
                {
                    response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                    response.Message.Add(HttpStatusCode.OK.ToString());
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                    response.Data = result;
                }
            }
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError,"error when delete user " + ex.Message, "data " + JsonConvert.SerializeObject(data) + " " + ex.ToString(), ref response, _logger);
        }
        return Ok(response);

    }

    /// <summary>kiểm tra dữ liệu thêm user</summary>
    /// <param name="user">dữ liệu thêm user</param>
    /// <param name="response">đối tượng nhận kiểm tra dữ liệu user</param>
    /// <returns>true - không lỗi, false- có lỗi</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/8/2023 created
    /// </Modified>
    private bool ValidUserAdd(User user, ref ApiResponse<int> response)
    {
        var valid = true;
        var validUsername = FormatDataHelper.CheckValidPropertyRegex(user.Username, "Username", ref response, FormatDataHelper.regexUsername);
        var validPhone = FormatDataHelper.CheckValidPropertyRegex(user.Phone, "Phone", ref response, FormatDataHelper.regexPhone);
        var validMail = FormatDataHelper.ValidMail(user.Email, ref response);
        var validBirthday = FormatDataHelper.ValidBirthday(user.Birthday, ref response); 
        var validPass = FormatDataHelper.CheckValidPropertyRegex(user.Password, "Password", ref response, FormatDataHelper.regexPass);
        var validFullName = FormatDataHelper.CheckNullOrEmptyString(user.FullName, "FullName", ref response);

        if (!(validBirthday && validFullName && validMail && validPass && validPhone && validUsername))
        {
            valid = false;
        }
        return valid;

    }

    /// <summary>kiểm tra dữ liệu sửa user</summary>
    /// <param name="user">dữ liệu sửa user</param>
    /// <param name="response">đối tượng nhận kiểm tra dữ liệu user</param>
    /// <returns>true - không lỗi, false - có lỗi</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/8/2023 created
    /// </Modified>
    private bool ValidUserEdit(User user, ref ApiResponse<int> response)
    {
        var valid = true;
        var validUsername = FormatDataHelper.CheckValidPropertyRegex(user.Username, "Username", ref response, FormatDataHelper.regexUsername);
        var validPhone = FormatDataHelper.CheckValidPropertyRegex(user.Phone, "Phone", ref response, FormatDataHelper.regexPhone);
        var validMail = FormatDataHelper.ValidMail(user.Email, ref response);
        var validBirthday = FormatDataHelper.ValidBirthday(user.Birthday, ref response);
        var validFullName = FormatDataHelper.CheckNullOrEmptyString(user.FullName, "FullName", ref response);

        if (!(validBirthday && validFullName && validMail && validPhone && validUsername))
        {
            valid = false;
        }
        return valid;

    }

    /// <summary>kiểm tra dữ liệu đầu vào khi tìm kiếm list user</summary>
    /// <param name="input">dữ liệu  tìm kiếm list user</param>
    /// <param name="response">đối tượng nhận kiểm tra dữ liệu tìm list user</param>
    /// <returns>true - không lỗi, false - có lỗi</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/8/2023 created
    /// </Modified>
    private bool ValidInputSearch(InputSearchList input, ref ApiResponse<UserModel> response)
    {
        var valid = true;
        if (input.DayFrom.HasValue && input.DayTo.HasValue)
        {
            if (input.DayFrom > input.DayTo)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "DayFrom bigger than DayTo", "data " + JsonConvert.SerializeObject(input) + " " + "DayFrom bigger than DayTo", ref response, _logger);
            }
        }
         
        if (response.Message.Count > 0)
        {
            valid = false;
        }
        return valid;

    }

    /// <summary>kiểm tra dữ liệu đầu vào khi gọi api CheckLoginAndRole</summary>
    /// <param name="input">dữ liệu đầu vào</param>
    /// <param name="response">đối tượng nhận kiểm tra dữ liệu</param>
    /// <returns>true - không lỗi, false - có lỗi</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/8/2023 created
    /// </Modified>
    private bool ValidCheckLoginAndRole(InputLogin input, ref ApiResponse<UserRole> response)
    {
        var valid = true;
        if (input.Token.IsNullOrEmpty())
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "wrong token", "data " + JsonConvert.SerializeObject(input) + " wrong token", ref response, _logger);
        }
        FormatDataHelper.CheckParseIntString(input.MenuId, "Menuid", ref response);
        if (response.Message.Count > 0)
        {
            valid = false;
        }
        return valid;
    }

    /// <summary>kiểm tra dữ liệu đầu vào khi gọi api user/login</summary>
    /// <param name="data">dữ liệu đầu vào</param>
    /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
    /// <returns>true - không lỗi, false - có lỗi</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/8/2023 created
    /// </Modified>
    private bool ValidLogin(InputLogin data, ref ApiResponse<UserModel> response)
    {
        var valid = true;
        var validUsername = FormatDataHelper.CheckNullOrEmptyString(data.Username, "Username", ref response);
        var validPassword = FormatDataHelper.CheckNullOrEmptyString(data.Password, "Password", ref response);

        if (!data.IsRemember.HasValue)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "null IsRemember", "data " + JsonConvert.SerializeObject(data) + " null IsRemember", ref response, _logger);
        }
        if (response.Message.Count > 0)
        {
            valid = false;
        }
        return valid;
    }

    /// <summary>kiểm tra dữ liệu đầu vào khi gọi api user/getRole</summary>
    /// <param name="data">dữ liệu đầu vào</param>
    /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
    /// <returns>true - không lỗi, false - có lỗi</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/8/2023 created
    /// </Modified>
    private bool ValidGetRole(InputLogin data, ref ApiResponse<UserRole> response)
    {
        var valid = true;
        var validMenuId = FormatDataHelper.CheckParseIntString(data.MenuId, "MenuId", ref response);
        var validUserId = FormatDataHelper.CheckNullOrEmptyString(data.UserId, "userId", ref response);
        if (!(validMenuId && validUserId))
        {
            valid = false;
        }
        return valid;

    }

    /// <summary>kiểm tra dữ liệu đầu vào  khi gọi api User/ChangePass</summary>
    /// <param name="data">dữ liệu đầu vào</param>
    /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
    /// <returns>true - không lỗi, false - có lỗi</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/8/2023 created
    /// </Modified>
    private bool ValidChangePass(InputLogin data, ref ApiResponse<int> response)
    {
        var valid = true; 
        var validPass = FormatDataHelper.CheckValidPropertyRegex(data.Password, "Password", ref response, FormatDataHelper.regexPass);
        var validPassOld = FormatDataHelper.CheckValidPropertyRegex(data.PasswordOld, "PasswordOld", ref response, FormatDataHelper.regexPass);
        if (!(validPass && validPassOld))
        {
            valid = false;
        }
        return valid;
    }

    /// <summary>kiểm tra dữ liệu đầu vào  khi gọi api user/validDeleteUser</summary>
    /// <param name="data">dữ liệu đầu vào</param>
    /// <param name="response">đối tượng nhận kết quả kiểm tra dữ liệu</param>
    /// <returns>true - không lỗi, false - có lỗi</returns>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 8/8/2023 created
    /// </Modified>
    private bool ValidDeleteUser(InputDelete data, ref ApiResponse<int> response)
    {
        var valid = true;
        if (data.Users.Count == 0)
        {
            LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError, "empty list User delete", "data " + JsonConvert.SerializeObject(data) + " empty list user delete", ref response, _logger);
        }
        FormatDataHelper.CheckNullOrEmptyString(data.UserId, "userId", ref response);
        if (response.Message.Count > 0)
        {
            valid = false;
        }
        return valid;
    }

}
