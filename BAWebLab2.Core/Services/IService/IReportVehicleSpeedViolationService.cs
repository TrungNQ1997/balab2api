using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;

namespace BAWebLab2.Core.Services.IService
{
    public interface   IReportVehicleSpeedViolationService
    {
        public StoreResult<Vehicles> GetVehicles();

        public StoreResult<ResultReportSpeed> GetDataReport(InputReportSpeed input);
    }
}
