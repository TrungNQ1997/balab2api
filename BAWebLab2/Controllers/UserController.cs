using Microsoft.AspNetCore.Mvc;
using BAWebLab2.Service;
using System.Net;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using BAWebLab2.Model;
using BAWebLab2.Entities;
using System.Text.RegularExpressions;
using BAWebLab2.Core.LibCommon;
using Microsoft.IdentityModel.Tokens;
using Azure;

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
                    response.Message = HttpStatusCode.OK.ToString();
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message = HttpStatusCode.InternalServerError.ToString();
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
                    response.Message = HttpStatusCode.OK.ToString();
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message = HttpStatusCode.InternalServerError.ToString();
                    response.Data = result;
                }
            }
        } catch (Exception ex) {
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
        try { 
        if (ValidCheckLoginAndRole(data, ref response))
        { 
            var result = _userService.CheckLoginAndRole(data); 
            if (result.Error == false)
            {
                response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                response.Message = HttpStatusCode.OK.ToString();
                response.Data = result;
            }
            else
            {
                response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                response.Message = HttpStatusCode.InternalServerError.ToString();
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

        var result = _userService.GetRole(data);

        ApiResponse<UserRole> response = new ApiResponse<UserRole>();

        if (result.Error == false)
        {
            response.StatusCode = ((int)HttpStatusCode.OK).ToString();
            response.Message = HttpStatusCode.OK.ToString();
            response.Data = result;
        }
        else
        {
            response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
            response.Message = HttpStatusCode.InternalServerError.ToString();
            response.Data = result;
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
                    response.Message = HttpStatusCode.OK.ToString();
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message = HttpStatusCode.InternalServerError.ToString();
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
                    response.Message = HttpStatusCode.OK.ToString();
                    response.Data = result;
                }
                else
                {
                    response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                    response.Message = HttpStatusCode.InternalServerError.ToString();
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
        result = _userService.ChangePass(data);
        if (result.Error == false)
        {
            response.StatusCode = ((int)HttpStatusCode.OK).ToString();
            response.Message = HttpStatusCode.OK.ToString();
            response.Data = result;
        }
        else
        {
            response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
            response.Message = HttpStatusCode.InternalServerError.ToString();
            response.Data = result;
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
        var result = _userService.DeleteUser(data);
        ApiResponse<int> response = new ApiResponse<int>();

        if (result.Error == false)
        {
            response.StatusCode = ((int)HttpStatusCode.OK).ToString();
            response.Message = HttpStatusCode.OK.ToString();
            response.Data = result;
        }
        else
        {
            response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
            response.Message = HttpStatusCode.InternalServerError.ToString();
            response.Data = result;
        }

        return Ok(response);

    }

    private bool ValidUsername(string username, ref ApiResponse<int> response)
    {
        if (username.IsNullOrEmpty() || !Regex.IsMatch(username, @"^[a-zA-Z0-9]{1,50}$"))
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong username", ref response);
            return false;
        }
        return true;
    }

    private bool ValidPhone(string? phone, ref ApiResponse<int> response)
    {
        if (phone.IsNullOrEmpty() || !Regex.IsMatch(phone, @"^[0-9]{1,10}$"))
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong phone", ref response);
            return false;
        }
        return true;
    }

    private bool ValidMail(string? mail, ref ApiResponse<int> response)
    {
        if (mail is not null)
        {
            if (!Regex.IsMatch(mail, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,200}$"))
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong email", ref response);
                return false;
            }
        }
        return true;
    }

    private bool ValidBirthday(DateTime? birthday, ref ApiResponse<int> response)
    {
        if (birthday is null)
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "null birthday", ref response);
            return false;
        }
        else
        {
            if (DateTime.Now.Year - birthday.Value.Year < 18)
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "user not 18 years old", ref response);
                return false;
            }
        }
        return true;
    }

    private bool ValidPass(string? pass, ref ApiResponse<int> response)
    {
        if (pass.IsNullOrEmpty() || !Regex.IsMatch(pass, @"^[a-zA-Z0-9]{6,100}$"))
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong pass", ref response);
            return false;
        }
        return true;
    }

    private bool ValidFullName(string? fullName, ref ApiResponse<int> response)
    {
        if (fullName.IsNullOrEmpty())
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "empty fullname", ref response);
            return false;
        }
        return true;
    }

    private bool ValidUserAdd(User user, ref ApiResponse<int> response)
    {
        var validUsername = ValidUsername(user.Username, ref response);
        var validPhone = ValidPhone(user.Phone, ref response);
        var validMail = ValidMail(user.Email, ref response);
        var validBirthday = ValidBirthday(user.Birthday, ref response);
        var validPass = ValidPass(user.Password, ref response);
        var validFullName = ValidFullName(user.FullName, ref response);

        if (validBirthday && validFullName && validMail && validPass && validPhone && validUsername)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool ValidUserEdit(User user, ref ApiResponse<int> response)
    {
        var validUsername = ValidUsername(user.Username, ref response);
        var validPhone = ValidPhone(user.Phone, ref response);
        var validMail = ValidMail(user.Email, ref response);
        var validBirthday = ValidBirthday(user.Birthday, ref response);
        var validFullName = ValidFullName(user.FullName, ref response);

        if (validBirthday && validFullName && validMail && validPhone && validUsername)
        {
            return true;
        }
        else
        {
            return false;
        }

    }

    private bool ValidInputSearch(InputSearchList input, ref ApiResponse<UserModel> response)
    {
        if (input.DayFrom.HasValue && input.DayTo.HasValue)
        {
            if (input.DayFrom > input.DayTo)
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "DayFrom bigger than DayTo", ref response);
                return false;
            }
        }

        try
        {
            int.Parse(input.UserId);
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong userid", ref response);
            return false;
        }

        return true;
    }

    private bool ValidCheckLoginAndRole(InputLogin input, ref ApiResponse<UserRole> response)
    {
        if (input.Token.IsNullOrEmpty())
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong token", ref response);
            return false;
        }

        try
        {
            int.Parse(input.MenuId);
        }
        catch (Exception ex)
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong MenuId", ref response);
            return false;
        }

        return true;
    }

    private bool ValidLogin(InputLogin data, ref ApiResponse<UserModel> response)
    {
        if (data.Username.IsNullOrEmpty())
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "null Username", ref response);
            return false;
        }
        if (data.Password.IsNullOrEmpty())
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "null Password", ref response);
            return false;
        }
        if (!data.IsRemember.HasValue)
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "null IsRemember", ref response);
            return false;
        }
        return true;
    }
}
