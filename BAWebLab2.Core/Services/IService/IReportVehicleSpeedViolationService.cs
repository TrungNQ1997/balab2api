﻿using BAWebLab2.Entities;
using BAWebLab2.Infrastructure.Models;
using BAWebLab2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.Services.IService
{
    public interface   IReportVehicleSpeedViolationService
    {
        public StoreResult<Vehicles> GetVehicles();

        public StoreResult<ResultReportSpeed> GetDataReport(InputReportSpeed input);
    }
}
