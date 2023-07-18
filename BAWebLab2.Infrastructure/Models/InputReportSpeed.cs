using BAWebLab2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Models
{
    public class InputReportSpeed:InputSearchList
    {
        public string? TimeFrom { get; set; }
        public string? TimeTo { get; set; }
    }
}
