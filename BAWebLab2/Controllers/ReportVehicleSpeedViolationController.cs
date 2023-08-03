using BAWebLab2.Core.LibCommon;
using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using Microsoft.AspNetCore.Mvc;
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
                LogHelper.LogError(ex.ToString());
                response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                response.Message = ex.Message;
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
                LogHelper.LogError(ex.ToString());
                response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                response.Message = ex.Message;
            }
            return Ok(response);
        }


    }
}
