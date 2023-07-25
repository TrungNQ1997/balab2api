using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using BAWebLab2.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using System.Net;

namespace BAWebLab2.Controllers
{
    /// <summary>class nhận request từ client của báo cáo vi phạm tốc độ phương tiện</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    [Route("reportVehicleSpeedViolation")]
    [ApiController]
    public class ReportVehicleSpeedViolationController:ControllerBase
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
            Request.Headers.TryGetValue("CompanyID", out StringValues headerValue);
            var comID = int.Parse(headerValue.ToString());
            var result = _reportVehicleSpeedViolationService.GetVehicles(comID);
            ApiResponse<Vehicles> response = new ApiResponse<Vehicles>();

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

        /// <summary>api lấy số liệu báo cáo vi phạm tốc độ phương tiện</summary>
        /// <param name="input">đối tượng chứa tham số cần của báo cáo: từ ngày, đến ngày, danh sách xe</param>
        /// <returns>số liệu báo cáo</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified>
        [HttpPost("getDataReport")]
        public IActionResult GetDataReport([FromBody] InputSearchList input )
        { 
            Request.Headers.TryGetValue("CompanyID", out StringValues headerValue);
            var comID = int.Parse(headerValue.ToString());
            var result = _reportVehicleSpeedViolationService.GetDataReport(input, comID);
            ApiResponse<ResultReportSpeed> response = new ApiResponse<ResultReportSpeed>();

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


    }
}
