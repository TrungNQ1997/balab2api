using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Model;

namespace BAWebLab2.Core.Services
{
    public class ReportVehicleSpeedViolationService : IReportVehicleSpeedViolationService
    {

        private readonly IReportVehicleSpeedViolationRepository _reportVehicleSpeedViolationRepository;

        public ReportVehicleSpeedViolationService(IReportVehicleSpeedViolationRepository reportVehicleSpeedViolationRepository)
        {

            _reportVehicleSpeedViolationRepository = reportVehicleSpeedViolationRepository;
        }

        public StoreResult<Vehicles> GetVehicles()
        {
            var result = new StoreResult<Vehicles>();
             
            try
            {
                 
                var resultStore = _reportVehicleSpeedViolationRepository.GetVehicles<Vehicles>();

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

        public StoreResult<ResultReportSpeed> GetDataReport(InputReportSpeed input)
        {
            var result = new StoreResult<ResultReportSpeed>();


            try
            {
                 
                 result = _reportVehicleSpeedViolationRepository.GetDataReport<ResultReportSpeed>(input);
                 
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
