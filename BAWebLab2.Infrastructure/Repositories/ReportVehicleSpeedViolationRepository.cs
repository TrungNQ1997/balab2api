using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.DataContext;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Infrastructure.Repositories.IRepository;
using BAWebLab2.Model;
using BAWebLab2.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Repositories
{
    public class ReportVehicleSpeedViolationRepository: GenericRepository<User>, IReportVehicleSpeedViolationRepository
    {
        private readonly BADbContext _bADbContext;
        public ReportVehicleSpeedViolationRepository(BADbContext bADbContext)
            : base(bADbContext)
        {
            _bADbContext = bADbContext;
        }

        public MultipleResult<Vehicles> GetVehicles<Vehicles>()
        {
            var myObject = new MultipleResult<Vehicles>();


            using (var connection = _context.Database.GetDbConnection())
            {

                myObject.ListPrimary = _context.Vehicles.Cast<Vehicles>().ToList();

            }
            return myObject;
        }

        public MultipleResult<ResultReportSpeed> GetDataReport<ResultReportSpeed>(InputReportSpeed input)
        {
            var myObject = new MultipleResult<ResultReportSpeed>();

             
            using (var connection = _context.Database.GetDbConnection())
            {


                var bgtSpeed = _context.BGTSpeedOvers.Cast<BGTSpeedOvers>().ToList().Where(m => m.StartTime >= input.BirthdayFrom
                && m.EndTime <= input.BirthdayTo && m.FK_CompanyID == 15076);

                var activity = _context.ReportActivitySummaries.Cast<ReportActivitySummaries>().ToList().Where(m => m.StartTime >= input.BirthdayFrom
               && m.EndTime <= input.BirthdayTo && m.FK_CompanyID == 15076);

                var bGTTranportTypes = _context.BGTTranportTypes.Cast<BGTTranportTypes>().ToList();
                var bGTVehicleTransportTypes = _context.BGTVehicleTransportTypes.Cast<BGTVehicleTransportTypes>().ToList();

 


               var activityGroup = activity.GroupBy(m=>m.FK_VehicleID).Select(k=> new
               {
                   vehi = k.Key,
                   SumActivityTime = k.Sum(p=>p.ActivityTime),
                   TotalKm = k.Sum(p => p.TotalKmGps),
               }
                   
                   );



                var bgtSpeedGroup
                    = bgtSpeed.GroupBy(m => m.FK_VehicleID).Select(k =>
                    {
                        int sum1 = k.Count(l => (l.VelocityAllow + 5) <= l.VelocityGps && (l.VelocityAllow + 10) > l.VelocityGps);
                        int sum2 = k.Count(l => (l.VelocityAllow + 10) <= l.VelocityGps && (l.VelocityAllow + 20) > l.VelocityGps);
                        int sum3 = k.Count(l => (l.VelocityAllow + 20) <= l.VelocityGps && (l.VelocityAllow + 35) > l.VelocityGps);
                        int sum4 = k.Count(l => (l.VelocityAllow + 35) < l.VelocityGps);
                        //var sumTime = k.Sum(p=>(p.StartTime.Subtract(p.EndTime == null? p.StartTime :p.StartTime).     ));
                        double? sumKm = k.Sum(m=> (m.EndKm -  m.StartKm));
                        return new BAWebLab2.Infrastructure.Models.ResultReportSpeed
                        {
                            VehicleID = k.Key,
                            Sum1 = sum1,
                            Sum2 = sum2,
                            Sum3 = sum3,
                            Sum4 = sum4,
                            SumTotal = sum1 + sum2 + sum3 + sum4,
                            ViolateKm = sumKm,

                        };


                         }
                    
                    ).ToList();


                var t =
                   from speed in bgtSpeedGroup
                   join  acti in activityGroup on speed.VehicleID equals acti.vehi
                   into EmployeeAddressGroup //Performing LINQ Group Join
                   from  TotalKm in EmployeeAddressGroup.DefaultIfEmpty().ToList()
                    


                   join vehicleTransportTypes in bGTVehicleTransportTypes on speed.VehicleID equals vehicleTransportTypes.FK_VehicleID
                   join tranportTypes in bGTTranportTypes on vehicleTransportTypes.FK_TransportTypeID equals tranportTypes.PK_TransportTypeID
                   
                   select new BAWebLab2.Infrastructure.Models.ResultReportSpeed
                   {
                       VehicleID = speed.VehicleID,
                        Sum1 = speed.Sum1,
                            Sum2 = speed.Sum2,
                       Sum3 = speed.Sum3,
                       Sum4 = speed.Sum4,
                       SumTotal = speed.SumTotal,
                       ViolateKm = speed.ViolateKm,
                       TotalKm = TotalKm.TotalKm,
                       TotalTime = TotalKm.SumActivityTime,
                       
                       TransportTypeName = tranportTypes.DisplayName


                   };


                myObject.ListPrimary = t.Cast<ResultReportSpeed>().ToList();


            }
            return myObject;
        }



    }
}
