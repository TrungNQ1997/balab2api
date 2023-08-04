using BAWebLab2.Core.LibCommon;
using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace BAWebLab2.Controllers
{
    /// <summary>class nhận request từ client của báo cáo vi phạm tốc độ phương tiện</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    [Route("reportVehicleSpeedViolation")]
    [ApiController]
    public class ReportVehicleSpeedViolationController : ControllerBase
    {

        private readonly IReportVehicleSpeedViolationService _reportVehicleSpeedViolationService;

        public ReportVehicleSpeedViolationController(IReportVehicleSpeedViolationService reportVehicleSpeedViolationService)
        {

            _reportVehicleSpeedViolationService = reportVehicleSpeedViolationService;
        }

        /// <summary>api lấy danh sách vehicle</summary>
        /// <returns>danh sách vehicle</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified>
        [HttpGet("getVehicles")]
        public IActionResult GetVehicles()
        {
            ApiResponse<Vehicles> response = new ApiResponse<Vehicles>(); 
            if (ApiHelper.CheckValidHeader(Request, ref response))
            {
                try
                {
                    var comID = int.Parse(ApiHelper.GetHeader(Request, "CompanyID"));
                    var result = _reportVehicleSpeedViolationService.GetVehicles(comID);


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
                catch (Exception ex)
                { 
                    LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError,ex.ToString(), ref response); 
                }

            }


            return Ok(response);
        }

        /// <summary>api lấy số liệu báo cáo vi phạm tốc độ phương tiện</summary>
        /// <param name="input">đối tượng chứa tham số cần của báo cáo: từ ngày, đến ngày, danh sách xe</param>
        /// <returns>số liệu báo cáo</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified>
        [HttpPost("getDataReport")]
        public IActionResult GetDataReport([FromBody] InputSearchList input)
        {
            ApiResponse<ResultReportSpeed> response = new ApiResponse<ResultReportSpeed>();
            var valid = validGetDataReport(input, ref response);
            if (valid)
            {
                try
                {
                    var comID = int.Parse(ApiHelper.GetHeader(Request, "CompanyID"));
                    var result = _reportVehicleSpeedViolationService.GetDataReport(input, comID);


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
                catch (Exception ex)
                {
                    LogHelper.LogAndSetResponseError(HttpStatusCode.InternalServerError,ex.ToString(),ref response); 
                }
            }
            return Ok(response);
        }
 
        private bool validGetDataReport(InputSearchList input, ref ApiResponse<ResultReportSpeed> response)
        { 
            if (!ApiHelper.CheckValidHeader(Request, ref response))
            { 
                return false;
            }
            if (input.DayFrom is null)
            {  
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "Null DayFrom input", ref response);
                return false;
            }
            if (input.DayTo is null)
            { 
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "Null DayTo input", ref response); 
                return false;
            }
            if (input.DayFrom > input.DayTo)
            {  
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "DayFrom bigger than DayTo", ref response);
                return false;
            }
            if (((input.DayTo - input.DayFrom)).Value.TotalDays > 60)
            {  
                LogHelper.LogAndSetResponseError(HttpStatusCode.BadRequest, "DayFrom must not be more than 60 days from DayTo", ref response);
                return false;
            }
            return true;
        }

    }
}
