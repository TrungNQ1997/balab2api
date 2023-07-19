using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.Model;

namespace BAWebLab2.Infrastructure.Repositories.IRepository
{
    public interface IReportVehicleSpeedViolationRepository : IGenericRepository<User>
    {
        MultipleResult<Vehicles> GetVehicles<Vehicles>();

        StoreResult<ResultReportSpeed> GetDataReport<ResultReportSpeed>(InputReportSpeed input);
    }
}
