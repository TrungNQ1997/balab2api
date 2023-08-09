using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAWebLab2.Infrastructure.Models
{
    public class InputReport 
    {
        public int PageNumber { get; set; }

        /// <summary>Gets or sets số dòng mỗi trang</summary>
        /// <value>số dòng mỗi trang</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public int PageSize { get; set; }

        public DateTime DayTo { get; set; }

        /// <summary>Gets or sets ngày  từ</summary>
        /// <value>tìm kiếm  theo ngày  từ</value>
        /// <Modified>
        /// Name Date Comments
        /// trungnq3 7/12/2023 created
        /// </Modified>
        public DateTime DayFrom { get; set; }

        public List<long> VehicleSearch { get; set; }

        public InputReport() { 
        VehicleSearch = new List<long>();
        }
    }
}
