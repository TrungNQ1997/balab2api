using BAWebLab2.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Core.LibCommon
{
    public class ReportService
    {
        public static IEnumerable<T> PagingIEnumerable<T>(InputSearchList input, IEnumerable<T> list) {
            return list.Skip((input.PageNumber - 1) * input.PageSize).Take(input.PageSize);
        }  
    }
}
