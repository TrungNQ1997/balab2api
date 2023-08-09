using Microsoft.AspNetCore.Mvc;
using BAWebLab2.Service;
using System.Net;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using BAWebLab2.Model;
using BAWebLab2.Entities;
using BAWebLab2.Core.LibCommon;
using Microsoft.IdentityModel.Tokens;

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
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, ex.ToString(), ref response);
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
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, ex.ToString(), ref response);
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
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, ex.ToString(), ref response);
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
            if (validGetRole(data, ref response))
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
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, ex.ToString(), ref response);
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
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, ex.ToString(), ref response);
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
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, ex.ToString(), ref response);
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
            if (validChangePass(data, ref response))
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
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, ex.ToString(), ref response);
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
            if (validDeleteUser(data, ref response))
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
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, ex.ToString(), ref response);
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
        var validUsername = FormatDataHelper.ValidUsername(user.Username, ref response);
        var validPhone = FormatDataHelper.ValidPhone(user.Phone, ref response);
        var validMail = FormatDataHelper.ValidMail(user.Email, ref response);
        var validBirthday = FormatDataHelper.ValidBirthday(user.Birthday, ref response);
        var validPass = FormatDataHelper.checkValidPass(user.Password, "Password", ref response);
        var validFullName = FormatDataHelper.checkNullOrEmptyString(user.FullName, "FullName", ref response);// ValidFullName(user.FullName, ref response);

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
        var validUsername = FormatDataHelper.ValidUsername(user.Username, ref response);
        var validPhone = FormatDataHelper.ValidPhone(user.Phone, ref response);
        var validMail = FormatDataHelper.ValidMail(user.Email, ref response);
        var validBirthday = FormatDataHelper.ValidBirthday(user.Birthday, ref response);
        var validFullName = FormatDataHelper.checkNullOrEmptyString(user.FullName, "FullName", ref response);

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
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "DayFrom bigger than DayTo", ref response);
            }
        }

        try
        {
            int.Parse(input.UserId);
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong userid", ref response);
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
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong token", ref response);
        }
        FormatDataHelper.checkParseIntString(input.MenuId, "Menuid", ref response);
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
        var validUsername = FormatDataHelper.checkNullOrEmptyString(data.Username, "Username", ref response);
        var validPassword = FormatDataHelper.checkNullOrEmptyString(data.Password, "Password", ref response);

        if (!data.IsRemember.HasValue)
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "null IsRemember", ref response);
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
    private bool validGetRole(InputLogin data, ref ApiResponse<UserRole> response)
    {
        var valid = true;
        var validMenuId = FormatDataHelper.checkParseIntString(data.MenuId, "MenuId", ref response);
        var validUserId = FormatDataHelper.checkNullOrEmptyString(data.UserId, "userId", ref response);
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
    private bool validChangePass(InputLogin data, ref ApiResponse<int> response)
    {
        var valid = true;
        var validPass = FormatDataHelper.checkValidPass(data.Password, "Password", ref response);
        var validPassOld = FormatDataHelper.checkValidPass(data.PasswordOld, "PasswordOld", ref response);
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
    private bool validDeleteUser(InputDelete data, ref ApiResponse<int> response)
    {
        var valid = true;
        if (data.Users.Count == 0)
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError, "empty list user delete", ref response);
        }
        FormatDataHelper.checkNullOrEmptyString(data.UserId, "userId", ref response);
        if (response.Message.Count > 0)
        {
            valid = false;
        }
        return valid;
    }

}
