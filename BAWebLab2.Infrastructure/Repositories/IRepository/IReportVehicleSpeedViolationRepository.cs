using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repository.IRepository;
using BAWebLab2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Repositories.IRepository
{
    public interface IReportVehicleSpeedViolationRepository : IGenericRepository<User>
    {
        MultipleResult<Vehicles> GetVehicles<Vehicles>();
        MultipleResult<ResultReportSpeed> GetDataReport<ResultReportSpeed>(InputReportSpeed input);
    }
}
