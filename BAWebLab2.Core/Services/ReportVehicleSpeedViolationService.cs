using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Model;

namespace BAWebLab2.Core.Services
{
    /// <summary>class implement của IReportVehicleSpeedViolationService, chứa các service báo cáo vi phạm tốc độ cần</summary>
    /// <Modified>
    /// Name Date Comments
    /// trungnq3 7/20/2023 created
    /// </Modified>
    public class ReportVehicleSpeedViolationService : IReportVehicleSpeedViolationService
    {

        private readonly IReportVehicleSpeedViolationRepository _reportVehicleSpeedViolationRepository;

        public ReportVehicleSpeedViolationService(IReportVehicleSpeedViolationRepository reportVehicleSpeedViolationRepository)
        {

            _reportVehicleSpeedViolationRepository = reportVehicleSpeedViolationRepository;
        }

        /// <summary>lấy danh sách vehicle</summary>
        /// <returns>danh sách vehicle</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified> 
        public StoreResult<Vehicles> GetVehicles()
        {
            var result = new StoreResult<Vehicles>();
             
            try
            {
                 
                var resultStore = _reportVehicleSpeedViolationRepository.GetVehicles();

                var list = resultStore.ListPrimary.Where(m => m.FK_CompanyID == 15076).ToList();
                result.List = list;
                
                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }

            return result;
        }

        /// <summary>lấy dữ liệu báo cáo vi phạm tốc độ phương tiện</summary>
        /// <param name="input">đối tượng chứa các tham số báo cáo cần</param>
        /// <returns>dữ liệu báo cáo</returns>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/20/2023 created
        /// </Modified> 
        public StoreResult<ResultReportSpeed> GetDataReport(InputReportSpeed input)
        {
            var result = new StoreResult<ResultReportSpeed>();


            try
            {
                 
                result = _reportVehicleSpeedViolationRepository.GetDataReport(input );
                //result.Count = multi.ListPrimary.Count();
                //result.List = multi.ListPrimary.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize).ToList(); 
                //result.List = multi.ListPrimary.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize).ToList();
                

                result.Error = false;
            }
            catch (Exception ex)
            {
                result.Message = ex.Message;
                result.Error = true;
                LibCommon.LibCommon.WriteLog(ex.ToString());
            }

            return result;
        }


    }
}
