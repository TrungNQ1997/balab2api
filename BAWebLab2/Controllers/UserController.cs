using Microsoft.AspNetCore.Mvc;
using BAWebLab2.Service;
using System.Net; 
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using BAWebLab2.Model;
using BAWebLab2.Entities ;
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
        var result = _userService.GetListUsersFilter(data);
        ApiResponse<UserModel> response = new ApiResponse<UserModel>();

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
        var result = _userService.Login(data);

        ApiResponse<UserModel> response = new ApiResponse<UserModel>();

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

        var result = _userService.CheckLoginAndRole(data);


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
        if (ValidUser(data, ref response))
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
        if (ValidUser(data, ref response))
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

     private bool ValidUser(User user, ref ApiResponse<int> response)
    { 
        if (user.Username.IsNullOrEmpty() || !Regex.IsMatch(user.Username, @"^[a-zA-Z0-9]{1,50}$"))
        { 
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest,"wrong username", ref response);
            return false;
        }
        if (user.Phone.IsNullOrEmpty() || !Regex.IsMatch(user.Phone, @"^[0-9]{1,10}$"))
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong phone", ref response);
            return false;
        }
        if(user.Email is not null)
        {
            if(!Regex.IsMatch(user.Email, @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,200}$"))
            {
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong email", ref response);
                return false;
            }
        }
        if(user.Birthday is null)
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "null birthday", ref response);
            return false;
        } else
        {
            if(DateTime.Now.Year -  user.Birthday.Value.Year < 18){
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "user not 18 years old", ref response);
                return false;
            }
        }
        if (user.Password.IsNullOrEmpty() || !Regex.IsMatch(user.Password, @"^[a-zA-Z0-9]{6,100}$"))
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "wrong pass", ref response);
            return false;
        }
        if (user.FullName.IsNullOrEmpty())
        {
            LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "empty fullname", ref response);
            return false;
        }
            return true;
    }

    //private bool ValidUserEdit(User user, ref ApiResponse<int> response)
    //{

    //}


}
