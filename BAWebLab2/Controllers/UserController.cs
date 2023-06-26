using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using BAWebLab2.Business;
using System.Net;

using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;
using BAWebLab2.DTO;

[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    
    private readonly IUserBusiness _userBusiness;
     
    public UserController(IUserBusiness userBusiness)
    {

        _userBusiness = userBusiness;
    }
    
    [HttpPost("getListUserFilter")]
    
    public   IActionResult GetAllProducts([Microsoft.AspNetCore.Mvc.FromBody] JsonDocument data  )
    {
        
        var result = _userBusiness.GetAllUser(data);
        ApiResponse response = new ApiResponse();
       
        if(result.GetType() == typeof(System.Dynamic.ExpandoObject))
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
     
    [HttpPost("login")]
    
    public IActionResult Login([System.Web.Http.FromBody] JsonDocument data)
    {
         
        var result = _userBusiness.Login(data);
         
        ApiResponse response = new ApiResponse();

        if (result.GetType() == typeof(System.Dynamic.ExpandoObject))
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
     
    [HttpPost("checklogingetrole")]
    
    public IActionResult CheckLoginAndRole([System.Web.Http.FromBody] JsonDocument data)
    {
        
        var result = _userBusiness.CheckLoginAndRole(data);


        ApiResponse response = new ApiResponse();

        if (result.GetType() == typeof(System.Dynamic.ExpandoObject))
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

    [HttpPost("getrole")]
    
    public IActionResult GetRole([System.Web.Http.FromBody] JsonDocument data)
    {

        var result = _userBusiness.GetRole(data);

        ApiResponse response = new ApiResponse();

        if (result.GetType() == typeof(System.Dynamic.ExpandoObject))
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
     
    [HttpPost("adduser")]
    
    public IActionResult AddUser([FromBody] JsonDocument data)
    {
         
        var result = _userBusiness.AddUser(data);
        ApiResponse response = new ApiResponse();

        if (result.GetType() == typeof(System.Dynamic.ExpandoObject))
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
     
    [HttpPost("edituser")]
   
    public IActionResult EditUser([FromBody] JsonDocument data)
    {
        var result = new object();
         
        ApiResponse response = new ApiResponse();
        result = _userBusiness.EditUser(data);
        if (result.GetType() == typeof(System.Dynamic.ExpandoObject))
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
     
    [HttpPost("changepass")]
     
    public IActionResult ChangePass([FromBody] JsonDocument data)
    {
        var result = new object();
        ApiResponse response = new ApiResponse();
        result = _userBusiness.ChangePass(data);
        if (result.GetType() == typeof(System.Dynamic.ExpandoObject))
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
     
    [HttpPost("deleteuser")]
     
    public IActionResult DeleteUser([FromBody] JsonDocument data)
    {
         var result = _userBusiness.DeleteUser(data);
        ApiResponse response = new ApiResponse();

        if (result.GetType() == typeof(System.Dynamic.ExpandoObject))
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
