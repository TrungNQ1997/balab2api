using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BAWebLab2.Business;
using System.Net;

using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using BAWebLab2.DTO;
using BAWebLab2.Entity;

/// <summary>api phân hệ người dùng với tiền tố là user/</summary>
[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    
    private readonly IUserBusiness _userBusiness;
     
    public UserController(IUserBusiness userBusiness)
    {

        _userBusiness = userBusiness;
    }

    /// <summary>Gets all users.
    /// lấy danh sách người dùng có phân trang và tìm kiếm theo điều kiện</summary>
    /// <param name="data">The data.
    /// dữ liệu truyền vào từ body dạng json</param>
    /// <returns>trạng thái thực hiện store và tổng phần tử của list và 1 list đối tượng select theo offset pagesize và page number thỏa mãn điều kiện tìm kiếm  </returns>
    [HttpPost("getListUserFilter")]
    
    public   IActionResult GetAllUsers([Microsoft.AspNetCore.Mvc.FromBody] JsonDocument data  )
    {
        
        var result = _userBusiness.GetAllUser(data);
        ApiResponse<User> response = new ApiResponse<User>();
       
        if(result.is_error == false)
        {
            response.StatusCode = ((int)HttpStatusCode.OK).ToString();
            response.Message = HttpStatusCode.OK.ToString();
            response.Data = result;
        } else
        {
            response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
            response.Message = HttpStatusCode.InternalServerError.ToString();
            response.Data = result;
        }
        
        return Ok(response);
    }

    /// <summary>api kiểm tra đăng nhập vào hệ thống</summary>
    /// <param name="data">The data.
    /// truyền vào json chứa tài khoản và mật khẩu của user</param>
    /// <returns>trạng thái có cho đăng nhập hay không? , thông tin của user đăng nhập</returns>
    [HttpPost("login")]
    
    public IActionResult Login([System.Web.Http.FromBody] JsonDocument data)
    {
         
        var result = _userBusiness.Login(data);
         
        ApiResponse<User> response = new ApiResponse<User>();

        if (result.is_error == false)
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
    /// dữ liệu dạng json chứa token, menuid</param>
    /// <returns>đăng nhập có hợp lệ không?, list quyền theo user và menuid</returns>
    [HttpPost("checklogingetrole")]
    
    public IActionResult CheckLoginAndRole([System.Web.Http.FromBody] JsonDocument data)
    {
        
        var result = _userBusiness.CheckLoginAndRole(data);


        ApiResponse<UserRole> response = new ApiResponse<UserRole>();

        if (result.is_error == false)
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
    [HttpPost("getrole")]
    
    public IActionResult GetRole([System.Web.Http.FromBody] JsonDocument data)
    {

        var result = _userBusiness.GetRole(data);

        ApiResponse<UserRole> response = new ApiResponse<UserRole>();

        if (result.is_error == false)
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
    [HttpPost("adduser")]
    
    public IActionResult AddUser([FromBody] JsonDocument data)
    {
         
        var result = _userBusiness.AddUser(data);
        ApiResponse<int> response = new ApiResponse<int>();

        if (result.is_error == false)
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

    /// <summary>api sửa người dùng</summary>
    /// <param name="data">The data.
    /// chứa các thuộc tính của user cần sửa</param>
    /// <returns>trạng thái sửa người dùng</returns>
    [HttpPost("edituser")]
   
    public IActionResult EditUser([FromBody] JsonDocument data)
    {
        var result = new StoreResult<int>();
         
        ApiResponse<int> response = new ApiResponse<int>();
        result = _userBusiness.EditUser(data);
        if (result.is_error == false)
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

    /// <summary>api đổi mật khẩu người dùng</summary>
    /// <param name="data">The data.
    /// chứa mật khẩu cũ và mật khẩu mới</param>
    /// <returns>trạng thái đổi mật khẩu</returns>
    [HttpPost("changepass")]
     
    public IActionResult ChangePass([FromBody] JsonDocument data)
    {
        var result = new StoreResult<int>();
        ApiResponse<int> response = new ApiResponse<int>();
        result = _userBusiness.ChangePass(data);
        if (result.is_error == false)
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
    /// json chứa user_id và list id của danh sách user muốn xóa</param>
    /// <returns>trạng thái xóa user</returns>
    [HttpPost("deleteuser")]
     
    public IActionResult DeleteUser([FromBody] JsonDocument data)
    {
         var result = _userBusiness.DeleteUser(data);
        ApiResponse<int> response = new ApiResponse<int>();

        if (result.is_error == false)
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



}
