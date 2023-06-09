using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json.Linq;
using BAWebLab2.Business;
using System.Net.WebSockets;
using System.Net;
using Microsoft.AspNetCore.Cors;

using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

[Route("user")]
[ApiController]
public class UserController : ControllerBase
{
    
    private readonly IUserBusiness _userBusiness;

    //private readonly UserRepository _userRepository;


    public UserController(
        //UserRepository userRepository
        IUserBusiness userBusiness
        )
    {

        _userBusiness = userBusiness;
    }
    //[Microsoft.AspNetCore.Mvc.Route("getListUserFilter")]
    [HttpPost("getListUserFilter")]
    [EnableCors]
    public   IActionResult GetAllProducts([Microsoft.AspNetCore.Mvc.FromBody] JsonDocument data  )
    {
        //var json = JToken.Parse(string_json);
        var t = _userBusiness.getAllUser(data);
        var result = new ObjectResult(t)
        {
            StatusCode = (int)HttpStatusCode.OK
            
        };

        Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:43295");
        Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

        return result;
    }



    //[System.Web.Http.Route("login")]
    [HttpPost("login")]
    [EnableCors]
    public IActionResult Login([System.Web.Http.FromBody] JsonDocument data)
    {
        //var json = JToken.Parse(string_json);
        var t = _userBusiness.login(data);

        
        //if (int.Parse(t.GetType().GetProperty("result").GetValue(t, null).ToString()) != 0)
        //{
        //    result.StatusCode = (int)HttpStatusCode.BadRequest;
        //}    else
        //{

        //}

        var result = new ObjectResult(t)
        {
            StatusCode = (int)HttpStatusCode.OK

        };

        Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:43295");
        Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

        return result;
    }



    [HttpPost("adduser")]
    [EnableCors]
    public IActionResult addUser([FromBody] JsonDocument data)
    {
        //var json = JToken.Parse(string_json);
        var t = _userBusiness.addUser(data);


        //if (int.Parse(t.GetType().GetProperty("result").GetValue(t, null).ToString()) != 0)
        //{
        //    result.StatusCode = (int)HttpStatusCode.BadRequest;
        //}    else
        //{

        //}

        var result = new ObjectResult(t)
        {
            StatusCode = (int)HttpStatusCode.OK

        };

        Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:43295");
        Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

        return result;
    }


    [HttpPost("edituser")]
    [EnableCors]
    public IActionResult editUser([FromBody] JsonDocument data)
    {
        //var json = JToken.Parse(string_json);
        var t = _userBusiness.editUser(data);


        //if (int.Parse(t.GetType().GetProperty("result").GetValue(t, null).ToString()) != 0)
        //{
        //    result.StatusCode = (int)HttpStatusCode.BadRequest;
        //}    else
        //{

        //}

        var result = new ObjectResult(t)
        {
            StatusCode = (int)HttpStatusCode.OK

        };

        Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:43295");
        Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

        return result;
    }

    [HttpPost("deleteuser")]
    [EnableCors]
    public IActionResult deleteUser([FromBody] JsonDocument data)
    {
        //var json = JToken.Parse(string_json);
        var t = _userBusiness.deleteUser(data);


        //if (int.Parse(t.GetType().GetProperty("result").GetValue(t, null).ToString()) != 0)
        //{
        //    result.StatusCode = (int)HttpStatusCode.BadRequest;
        //}    else
        //{

        //}

        var result = new ObjectResult(t)
        {
            StatusCode = (int)HttpStatusCode.OK

        };

        Response.Headers.Add("Access-Control-Allow-Origin", "http://localhost:43295");
        Response.Headers.Add("Access-Control-Allow-Methods", "GET, POST, PUT, DELETE");
        Response.Headers.Add("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");

        return result;
    }



}
