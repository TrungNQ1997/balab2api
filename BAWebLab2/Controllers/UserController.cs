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
using System.Web.Http;
using HttpPostAttribute = Microsoft.AspNetCore.Mvc.HttpPostAttribute;

[RoutePrefix("user")]
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
    [Microsoft.AspNetCore.Mvc.Route("getListUserFilter")]
    [HttpPost]
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



    [Microsoft.AspNetCore.Mvc.Route("login")]
    [HttpPost]
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


    //[HttpGet("{id}")]
    //public async Task<ActionResult<Product>> GetProductById(int id)
    //{
    //    var product = await _userRepository.GetProductById(id);
    //    if (product == null)
    //    {
    //        return NotFound();
    //    }
    //    return product;
    //}

    // Thêm các phương thức API khác tùy ý
}
