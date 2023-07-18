using BAWebLab2.Core.Services.IService;
using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

                //var e = list.Skip(20).Take(10).ToList().OrderBy(m=>m.Username).OrderBy(mbox=>);


                //var count = parameters.Get<long>("count");
                result.List = list;
                //result.Count = (int)count;


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
                 
                var resultStore = _reportVehicleSpeedViolationRepository.GetDataReport<ResultReportSpeed>(input);

                //var list = resultStore.ListPrimary.Where(m => m.FK_CompanyID == 15076).ToList();

                //var e = list.Skip(20).Take(10).ToList().OrderBy(m=>m.Username).OrderBy(mbox=>);
                 
                //var count = parameters.Get<long>("count");
                result.List = resultStore.ListPrimary;
                //result.Count = (int)count;


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
