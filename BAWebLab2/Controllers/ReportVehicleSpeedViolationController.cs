using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using BAWebLab2.Service;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace BAWebLab2.Controllers
{
    [Route("reportVehicleSpeedViolation")]
    [ApiController]
    public class ReportVehicleSpeedViolationController:ControllerBase
    {

        private readonly IReportVehicleSpeedViolationService _reportVehicleSpeedViolationService;

        public ReportVehicleSpeedViolationController(IReportVehicleSpeedViolationService reportVehicleSpeedViolationService)
        {

            _reportVehicleSpeedViolationService = reportVehicleSpeedViolationService;
        }
         
        [HttpGet("getVehicles")]
        public IActionResult GetVehicles()
        {

            var result = _reportVehicleSpeedViolationService.GetVehicles();
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

        [HttpPost("getDataReport")]
        public IActionResult GetDataReport([FromBody] InputReportSpeed input )
        {

            var result = _reportVehicleSpeedViolationService.GetDataReport(input);
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
