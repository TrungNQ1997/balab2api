﻿using BAWebLab2.Core.LibCommon;
using BAWebLab2.Core.Services;
using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using log4net;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
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
        private readonly string secretKey = "2d94c6a1f3e8c6b8";

        private readonly IReportVehicleSpeedViolationService _reportVehicleSpeedViolationService;
        private static readonly ILog _logger = LogManager.GetLogger(typeof(ReportVehicleSpeedViolationController));

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
                if (ApiHelper.CheckNullSecurityHeader(Request, ApiHelper.HeaderNameSecurity, ref response))
                {
                    var validSecuHeader = false;
                    var userToken = ApiHelper.DeCryptionHeader(Request, ApiHelper.HeaderNameSecurity, ref validSecuHeader);
                    
                        var result = _reportVehicleSpeedViolationService.GetVehicles(userToken);
                        if (result.Error == false)
                        {
                            response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                            response.Message.Add(HttpStatusCode.OK.ToString());
                            response.Data = result;
                        }
                        else
                        {
                            response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                            response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                            response.Data = result;
                        }
 
                } else
                {
                    LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "null header security", "null header security", ref response, _logger);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError, "error when get data " + ex.Message, ex.ToString(), ref response, _logger);
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
        public IActionResult GetDataReport([FromBody] InputReport input)
        {
            ApiResponse<ResultReportSpeed> response = new ApiResponse<ResultReportSpeed>();
            try
            {
                var valid = ValidGetDataReport(input, ref response); 
                if (valid)
                {
                    var validSecuHeader = false;
                    var userToken = ApiHelper.DeCryptionHeader(Request, ApiHelper.HeaderNameSecurity, ref validSecuHeader);
                    var result = _reportVehicleSpeedViolationService.GetDataReport(input, userToken);

                    if (result.Error == false)
                    {
                        response.StatusCode = ((int)HttpStatusCode.OK).ToString();
                        response.Message.Add(HttpStatusCode.OK.ToString());
                        response.Data = result;
                    }
                    else
                    {
                        response.StatusCode = ((int)HttpStatusCode.InternalServerError).ToString();
                        response.Message.Add(HttpStatusCode.InternalServerError.ToString());
                        response.Data = result;
                    } 
                }
                else
                {
                    LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.BadRequest, "null header security", "null header security", ref response, _logger);
                }
            }
            catch (Exception ex)
            {
                LogHelper.LogAndSetResponseErrorInClass(HttpStatusCode.InternalServerError,"error when get data " + ex.Message, "data " + JsonConvert.SerializeObject(input) + " " + ex.ToString(), ref response, _logger);
            }
            return Ok(response);
        }

        /// <summary>kiểm tra tham số đầu vào báo cáo</summary>
        /// <param name="input">tham số đầu vào báo cáo</param>
        /// <param name="response">đối tượng nhận kết quả kiểm tra</param>
        /// <returns>kết quả kiểm tra</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 8/8/2023 created
        /// </Modified>
        private bool ValidGetDataReport(InputReport input, ref ApiResponse<ResultReportSpeed> response)
        {
            var valid = true;
            ApiHelper.CheckNullSecurityHeader(Request,ApiHelper.HeaderNameSecurity, ref response);
            ReportHelper.CheckDayReport(input.DayFrom,input.DayTo,ref response);
            
            if (response.Message.Count > 0)
            {
                valid = false;
            }
            return valid;
        }

     
    }
}
